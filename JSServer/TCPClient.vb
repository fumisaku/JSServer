Imports System
Imports System.Net
Imports System.Net.Sockets

Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Delegate Sub ReceivedDataEventHandler(ByVal sender As Object, ByVal e As ReceivedDataEventArgs)

Public Class TCPClient

    Private _socket As Socket
    Private _maxReceiveLength As Integer
    Private _encording As System.Text.Encoding

    Public 端末名 As String

    Public 区分番号 As String
    Public ラウンド番号 As String
    Public ジャッジ記号 As String

    Public 種目記号 As String
    Public ヒート番号 As Integer

    Private マスタデータ As マスタデータ

    Private LOG As LOG_C

    Public Sub Set_LOG(_LOG As LOG_C)

        LOG = _LOG

    End Sub

    ''' コンストラクタ
    ''' 
    Public Sub New()
        Me.Initialize()

        _socket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)


    End Sub

    Public Sub New(ByVal socket As Socket)
        Me.Initialize()

        _socket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

        Me._socket = socket
        Me._socket.SendTimeout = 5000   ' 送信タイムアウト 5秒

        マスタデータ = New マスタデータ


    End Sub

    '********* メソッド *****************************

    Private Sub Initialize()

        _maxReceiveLength = 102400    '2022/2/18 修正　1桁増やした
        _encording = System.Text.Encoding.UTF8

    End Sub


    Public Sub StartReceive()
        If Me.IsClosed Then
            Throw New ApplicationException("閉じています。")
        End If
        If Not (Me.receivedBytes Is Nothing) Then
            Throw New ApplicationException("StartReceiveがすでに呼び出されています。")
        End If

        '初期化
        Dim receiveBuffer(1023) As Byte
        Me.receivedBytes = New System.IO.MemoryStream

        '非同期受信を開始
        Me._socket.BeginReceive(receiveBuffer, 0, receiveBuffer.Length,
            SocketFlags.None,
            New AsyncCallback(AddressOf ReceiveDataCallback), receiveBuffer)
    End Sub

    'BeginReceiveのコールバック
    Private Sub ReceiveDataCallback(ByVal ar As IAsyncResult)
        '読み込んだ長さを取得
        Dim len As Integer = -1
        Try
            SyncLock Me
                len = Me._socket.EndReceive(ar)
            End SyncLock
        Catch
            'Socketを閉じた時
        End Try

        '切断されたか調べる
        If len <= 0 Then
            Me.Close()
            Return
        End If

        '受信したデータを取得する
        Dim receiveBuffer As Byte() = CType(ar.AsyncState, Byte())

        '受信したデータを蓄積する
        Me.receivedBytes.Write(receiveBuffer, 0, len)
        '最大値を超えた時は、接続を閉じる
        If Me.receivedBytes.Length > _maxReceiveLength Then
            Me.Close()
            Return
        End If
        '最後まで受信したか調べる
        If Me.receivedBytes.Length >= 2 Then
            Me.receivedBytes.Seek(-2, System.IO.SeekOrigin.End)
            If Me.receivedBytes.ReadByte() = 13 AndAlso
                Me.receivedBytes.ReadByte() = 10 Then
                '最後まで受信した時
                '受信したデータを文字列に変換
                Dim str As String = _encording.GetString(Me.receivedBytes.ToArray())
                Me.receivedBytes.Close()
                '一行ずつに分解する
                Dim startPos As Integer = 0
                Dim endPos As Integer
                While (True)
                    endPos = str.IndexOf(vbCrLf, startPos)
                    If endPos < 0 Then Exit While
                    Dim line As String = str.Substring(startPos, endPos - startPos)
                    startPos = endPos + 2

                    'イベントを発生
                    Me.OnReceivedData(New ReceivedDataEventArgs(Me, line))

                End While
                Me.receivedBytes = New System.IO.MemoryStream
            Else
                Me.receivedBytes.Seek(0, System.IO.SeekOrigin.End)
            End If
        End If

        '再び受信開始
        SyncLock Me
            If Not Me.IsClosed Then
                Me._socket.BeginReceive(receiveBuffer, 0, receiveBuffer.Length,
                    SocketFlags.None,
                    New AsyncCallback(AddressOf ReceiveDataCallback), receiveBuffer)
            End If
        End SyncLock
    End Sub


    ''' <summary>
    ''' 文字列を送信する
    ''' </summary>
    ''' <param name="str">送信する文字列</param>
    Public Sub Send(ByVal str As String)
        If Me.IsClosed Then
            Exit Sub
            'Throw New ApplicationException("閉じています。")
        End If

        '文字列をByte型配列に変換
        Dim sendBytes As Byte() = _encording.GetBytes((str + vbCrLf))

        ' 送信リトライ処理（最大2回、各5秒タイムアウト）
        Dim retryCount As Integer = 0
        Dim sendSuccess As Boolean = False

        Do While retryCount < 2 AndAlso Not sendSuccess
            Try
                SyncLock Me
                    'データを送信する
                    Me._socket.Send(sendBytes)
                End SyncLock
                sendSuccess = True

            Catch ex As SocketException
                retryCount += 1
                If retryCount < 2 Then
                    LOG.LogAdd("Send失敗（1回目）リトライします: " & Me.端末名 & " / " & ex.Message, 1)
                Else
                    LOG.LogAdd("Send失敗（2回目）送信を諦めます: " & Me.端末名 & " / " & ex.Message, 1)
                End If

            Catch ex As Exception
                LOG.LogAdd("Send失敗（予期しないエラー）送信を諦めます: " & Me.端末名 & " / " & ex.Message, 1)
                Exit Do

            End Try
        End While

        '送信データの内容を登録する
        If str.Split(",")(1) = "ANSHEAT" Then
            'ヒート表を要求してきた場合

            Me.区分番号 = str.Split(",")(5)
            Me.ラウンド番号 = str.Split(",")(7)
            Me.ジャッジ記号 = str.Split(",")(10)
            Me.種目記号 = str.Split(",")(13)
            Me.ヒート番号 = CInt(str.Split(",")(17))

        ElseIf str.Split(",")(1) = "ANSKUBUN" Then
            'ログインしてきた場合
            Me.区分番号 = ""
            Me.ラウンド番号 = ""
            Me.ジャッジ記号 = ""
            Me.種目記号 = ""
            Me.ヒート番号 = 0

        ElseIf str.Split(",")(1) = "ANSHEAT_BR2_J" Then
            'ヒート表を要求してきた場合

            Dim S_採点結果_BR2_J As S_採点結果_BR2_J
            S_採点結果_BR2_J = New S_採点結果_BR2_J(マスタデータ.Z_システム設定.Comp_filepath)

            '配列(0)-(4)とカンマを削除する

            Dim 配列 = str.Split(",")

            Dim 削除文字列 As String = ""
            For i = 0 To 4
                削除文字列 = 削除文字列 & 配列(i) & ","
            Next i

            str = str.Replace(削除文字列, "")


            S_採点結果_BR2_J = JsonConvert.DeserializeObject(Of S_採点結果_BR2_J)(str)
            S_採点結果_BR2_J.Set_FilePath(マスタデータ.Z_システム設定.Comp_filepath)

            S_採点結果_BR2_J.JSON書き出し()


            Me.区分番号 = S_採点結果_BR2_J.区分番号
            Me.ラウンド番号 = S_採点結果_BR2_J.ラウンド番号
            Me.ジャッジ記号 = S_採点結果_BR2_J.ジャッジ記号
            Me.種目記号 = S_採点結果_BR2_J.種目記号
            Me.ヒート番号 = 0

        ElseIf str.Split(",")(1) = "ANSHEAT_J" Then

            Dim S_採点結果_J = New S_採点結果_J(マスタデータ.Z_システム設定.Comp_filepath)


            '配列(0)-(4)とカンマを削除する

            Dim 配列 = str.Split(",")

            Dim 削除文字列 As String = ""
            For i = 0 To 4
                削除文字列 = 削除文字列 & 配列(i) & ","
            Next i

            str = str.Replace(削除文字列, "")
            S_採点結果_J = JsonConvert.DeserializeObject(Of S_採点結果_J)(str)
            S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)

            'S_採点結果_J.JSON書き出し()　　　　'これ必要かな？ ⇒不要　全ヒート分の結果があるのに、1ヒート分でうわがきすることになるため


            Me.区分番号 = S_採点結果_J.区分番号
            Me.ラウンド番号 = S_採点結果_J.ラウンド番号
            Me.ジャッジ記号 = S_採点結果_J.ジャッジ記号
            Me.種目記号 = S_採点結果_J.種目記号
            Me.ヒート番号 = S_採点結果_J.ヒート番号

            S_採点結果_J = Nothing



        ElseIf str.Split(",")(1) = "ANSHEAT_V2_J" Then

            Dim S_採点結果_J = New S_採点結果_V2_J(マスタデータ.Z_システム設定.Comp_filepath)
            S_採点結果_J.Set_LOG(LOG)

            '配列(0)-(4)とカンマを削除する

            Dim 配列 = str.Split(",")

            Dim 削除文字列 As String = ""
            For i = 0 To 4
                削除文字列 = 削除文字列 & 配列(i) & ","
            Next i

            str = str.Replace(削除文字列, "")

            S_採点結果_J = JsonConvert.DeserializeObject(Of S_採点結果_V2_J)(str)
            S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)


            Me.区分番号 = S_採点結果_J.区分番号
            Me.ラウンド番号 = S_採点結果_J.ラウンド番号
            Me.ジャッジ記号 = S_採点結果_J.ジャッジ記号
            Me.種目記号 = S_採点結果_J.現種目記号
            Me.ヒート番号 = S_採点結果_J.現ヒート番号

            S_採点結果_J = Nothing

        End If


    End Sub


    ''' <summary>
    ''' 切断する
    ''' </summary>
    Public Sub Close()

        Try
            SyncLock Me
                If (Me.IsClosed) Then
                    Return
                End If

                '閉じる
                Me._socket.Shutdown(SocketShutdown.Both)
                Me._socket.Close()
                Me._socket = Nothing
                If Not (Me.receivedBytes Is Nothing) Then
                    Me.receivedBytes.Close()
                    Me.receivedBytes = Nothing
                End If
            End SyncLock
            'イベントを発生
            Me.OnDisconnected(New EventArgs)

        Catch ex As Exception

            Try
                LOG.LogAdd("TCPClient.Close()でエラー 端末名:" & 端末名 & " 区分番号:" & 区分番号 & " ラウンド番号:" & ラウンド番号 & " ジャッジ記号:" & ジャッジ記号 & " 種目記号:" & 種目記号 & " ヒート番号:" & ヒート番号 & "//" & ex.ToString, 1)

            Catch ex2 As Exception

            End Try


        End Try

    End Sub

    ''' <summary>
    ''' 閉じているか
    ''' </summary>
    Public ReadOnly Property IsClosed() As Boolean
        Get
            Return Me._socket Is Nothing
        End Get
    End Property

    '*********プロパティ**********************

    ''' 受信したデータ
    Protected receivedBytes As System.IO.MemoryStream


    ''' <summary>
    ''' ローカルエンドポイント
    ''' </summary>
    Private _localEndPoint As IPEndPoint
    Public ReadOnly Property LocalEndPoint() As IPEndPoint
        Get
            Return Me._localEndPoint
        End Get
    End Property

    ''' <summary>
    ''' リモートエンドポイント
    ''' </summary>
    Private _remoteEndPoint As IPEndPoint
    Public ReadOnly Property RemoteEndPoint() As IPEndPoint
        Get
            Return Me._remoteEndPoint
        End Get
    End Property


    '********イベント **********


    ' データを受信した
    Public Event SendTempイベント(ByVal sender As Object, ByVal e As S_採点結果_V2_J)



    'Public Event E11_LOGIN As ServerEventHandler
    'Public Event E12_RCVTemp(ByVal sender As Object, ByVal e As EventArgs)
    'Public Event E13_RCV(ByVal sender As Object, ByVal e As EventArgs)
    'Public Event E19_LOGOUT As ReceivedDataEventHandler
    'Public Event E51_ReqHelp As ReceivedDataEventHandler

    Public Event ReceivedData As ReceivedDataEventHandler
    Protected Overridable Sub OnReceivedData(ByVal e As ReceivedDataEventArgs)


        Try

            Dim str As String = e.ReceivedString
            Dim 全レコード数 As Integer

            If str.Split(",")(1) = "REQKUBUN" Then
                '端末がコネクトしてきた時

                Me.端末名 = str.Split(",")(2)

                'ANSKUBUN電文を返す
                全レコード数 = SEND_ANSKUBUN(）


            ElseIf str.Split(",")(1) = "REQHEAT" Then
                'ジャッジがログインした時
                Me.端末名 = str.Split(",")(2)
                Me.区分番号 = str.Split(",")(5)
                Me.ラウンド番号 = str.Split(",")(6)
                Me.ジャッジ記号 = str.Split(",")(7)

                'ASHEAT電文を返す
                全レコード数 = SEND_ANSHEAT(ジャッジ記号, 区分番号, ラウンド番号, "00", 0, "次")

                Dim e_SVR As ServerEventArgs
                e_SVR = New ServerEventArgs(Me)
                'RaiseEvent E11_LOGIN(Me, e_SVR)


            ElseIf str.Split(",")(1) = "REQHEAT_2" Then
                'ジャッジがヒートを指定してきた時　BJPREの時
                Me.端末名 = str.Split(",")(2)
                Me.区分番号 = str.Split(",")(5)
                Me.ラウンド番号 = str.Split(",")(6)
                Me.ジャッジ記号 = str.Split(",")(7)
                Me.種目記号 = str.Split(",")(8)
                Me.ヒート番号 = CInt(str.Split(",")(9))

                '前現次 区別
                Dim 前現次_区別 As String = "次"
                If UBound(str.Split(",")) >= 10 Then
                    前現次_区別 = str.Split(",")(10)
                End If


                Dim 種目順 As Integer
                If 種目記号 = "00" Then

                    'ASHEAT電文を返す
                    全レコード数 = SEND_ANSHEAT(ジャッジ記号, 区分番号, ラウンド番号, "00", ヒート番号, "次")


                Else
                    種目順 = マスタデータ.D_種目マスタ.Get_種目順(区分番号, ラウンド番号, 種目記号)

                    'ASHEAT電文を返す
                    If 前現次_区別 = "前" Then
                        全レコード数 = SEND_ANSHEAT(ジャッジ記号, 区分番号, ラウンド番号, 種目順, ヒート番号, "前")

                    Else
                        全レコード数 = SEND_ANSHEAT(ジャッジ記号, 区分番号, ラウンド番号, 種目順, ヒート番号, "現")
                    End If
                End If


                Dim e_SVR As ServerEventArgs
                e_SVR = New ServerEventArgs(Me)
                'RaiseEvent E11_LOGIN(Me, e_SVR)


            ElseIf str.Split(",")(1) = "SNDTEMP" Then

                Me.端末名 = str.Split(",")(2)
                Me.区分番号 = str.Split(",")(5)
                Me.ラウンド番号 = str.Split(",")(6)
                Me.ジャッジ記号 = str.Split(",")(7)
                Me.種目記号 = str.Split(",")(8)
                Me.ヒート番号 = CInt(str.Split(",")(9))

                WriteResult(str, False)

                'RaiseEvent E12_RCVTemp(Me, e)

            ElseIf str.Split(",")(1) = "SNDRESLT" Then

                Me.端末名 = str.Split(",")(2)
                Me.区分番号 = str.Split(",")(5)
                Me.ラウンド番号 = str.Split(",")(6)
                Me.ジャッジ記号 = str.Split(",")(7)
                Me.種目記号 = str.Split(",")(8)
                Me.ヒート番号 = CInt(str.Split(",")(9))

                WriteResult(str, True)

                'RaiseEvent E13_RCV(Me, e)


                'ASHEAT電文を返す
                全レコード数 = SEND_ANSHEAT(ジャッジ記号, 区分番号, ラウンド番号, マスタデータ.D_種目マスタ.Get_種目順(区分番号, ラウンド番号, 種目記号), ヒート番号, "次")


            ElseIf str.Split(",")(1) = "SNDRESLT_J" Then

                Me.端末名 = str.Split(",")(2)

                Dim S_採点結果_J As S_採点結果_J
                S_採点結果_J = New S_採点結果_J(マスタデータ.Z_システム設定.Comp_filepath)

                '配列(0)-(4)とカンマを削除する

                Dim 配列 = str.Split(",")

                Dim 削除文字列 As String = ""
                For i = 0 To 4
                    削除文字列 = 削除文字列 & 配列(i) & ","
                Next i

                str = str.Replace(削除文字列, "")

                'ヒート合成　strには1ヒート分しか入っていないので、元のファイルにStrのヒート分を合成
                Dim New_S_採点結果_J = S_採点結果_J.新JSONデータセット(str)     '2022/6/2 新に変更
                New_S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)

                'S_採点結果_J に、区分、ラウンド、ジャッジ記号をセット
                S_採点結果_J = S_採点結果_J.新JSONデータセット(str)
                S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)
                S_採点結果_J = S_採点結果_J.新JSON読み込み()

                If S_採点結果_J IsNot Nothing Then
                    S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)
                    S_採点結果_J.JSON追加(New_S_採点結果_J)
                    S_採点結果_J.ヒート番号 = New_S_採点結果_J.ヒート番号
                Else
                    '最初のヒート
                    S_採点結果_J = New_S_採点結果_J.Clone
                End If

                New_S_採点結果_J = Nothing

                S_採点結果_J.JSON書き出し()


                'ASHEAT電文を返す
                Dim 種目順 As Integer = マスタデータ.D_種目マスタ.Get_種目順(S_採点結果_J.区分番号, S_採点結果_J.ラウンド番号, S_採点結果_J.種目記号)
                Dim ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(種目順)

                全レコード数 = SEND_ANSHEAT(S_採点結果_J.ジャッジ記号, S_採点結果_J.区分番号, S_採点結果_J.ラウンド番号, 種目順, S_採点結果_J.ヒート番号, "次")
                '全レコード数 = ANSHEAT作成_JSON(S_採点結果_J.ジャッジ記号, S_採点結果_J.区分番号, S_採点結果_J.ラウンド番号, マスタデータ.D_種目マスタ.Get_種目順(S_採点結果_J.区分番号, S_採点結果_J.ラウンド番号, S_採点結果_J.種目記号))


                Me.区分番号 = S_採点結果_J.区分番号
                Me.ラウンド番号 = S_採点結果_J.ラウンド番号
                Me.ジャッジ記号 = S_採点結果_J.ジャッジ記号
                Me.種目記号 = S_採点結果_J.種目記号


                If Strings.Left(S_採点結果_J.採点方式, 4) = "BJPR" Then
                    '    Me.ヒート番号 = S_採点結果_J.ヒート番号
                    'これをやると、前のヒート番号になってしまう。
                Else
                    'チェック法、順位法の時　　種目毎送信なので。。。
                    Me.ヒート番号 = 0
                End If




                S_採点結果_J = Nothing





            ElseIf str.Split(",")(1) = "SNDTEMP_J" Then

                Me.端末名 = str.Split(",")(2)
                Dim S_採点結果_J As S_採点結果_J
                S_採点結果_J = New S_採点結果_J(マスタデータ.Z_システム設定.Comp_filepath)


                '配列(0)-(4)とカンマを削除する

                Dim 配列 = str.Split(",")

                Dim 削除文字列 As String = ""
                For i = 0 To 4
                    削除文字列 = 削除文字列 & 配列(i) & ","
                Next i

                str = str.Replace(削除文字列, "")


                'ヒート合成　strには1ヒート分しか入っていないので、元のファイルにStrのヒート分を合成
                Dim New_S_採点結果_J = S_採点結果_J.新JSONデータセット(str)
                New_S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)


                'S_採点結果_J に、区分、ラウンド、ジャッジ記号をセット
                S_採点結果_J = S_採点結果_J.新JSONデータセット(str)
                S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)
                S_採点結果_J = S_採点結果_J.新JSON読み込み()


                If S_採点結果_J IsNot Nothing Then

                    S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)
                    S_採点結果_J.JSON追加(New_S_採点結果_J)
                    S_採点結果_J.ヒート番号 = New_S_採点結果_J.ヒート番号
                Else
                    '最初のヒート
                    S_採点結果_J = New_S_採点結果_J.Clone
                End If

                New_S_採点結果_J = Nothing



                S_採点結果_J.JSON書き出し()


                Me.区分番号 = S_採点結果_J.区分番号
                Me.ラウンド番号 = S_採点結果_J.ラウンド番号
                Me.ジャッジ記号 = S_採点結果_J.ジャッジ記号
                Me.種目記号 = S_採点結果_J.種目記号

                If Strings.Left(S_採点結果_J.採点方式, 4) = "BJPR" Then
                    Me.ヒート番号 = S_採点結果_J.ヒート番号
                Else
                    'チェック法、順位法の時　　種目毎送信なので。。。
                    Me.ヒート番号 = 0

                End If



                S_採点結果_J = Nothing


            ElseIf str.Split(",")(1) = "SNDRESLT_V2_J" Then

                Me.端末名 = str.Split(",")(2)

                Dim S_採点結果_J As S_採点結果_V2_J
                S_採点結果_J = New S_採点結果_V2_J(マスタデータ.Z_システム設定.Comp_filepath)
                S_採点結果_J.Set_LOG(LOG)

                '配列(0)-(4)とカンマを削除する

                Dim 配列 = str.Split(",")

                Dim 削除文字列 As String = ""
                For i = 0 To 4
                    削除文字列 = 削除文字列 & 配列(i) & ","
                Next i

                str = str.Replace(削除文字列, "")

                'ヒート合成　strには1ヒート分しか入っていないので、元のファイルにStrのヒート分を合成
                Dim New_S_採点結果_J = S_採点結果_J.新JSONデータセット(str)     '2022/6/2 新に変更
                New_S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)

                'S_採点結果_J に、区分、ラウンド、ジャッジ記号をセット
                S_採点結果_J = S_採点結果_J.新JSONデータセット(str)
                S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)
                S_採点結果_J = S_採点結果_J.JSON読み込み()

                If S_採点結果_J IsNot Nothing Then
                    S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)
                    S_採点結果_J.JSON追加(New_S_採点結果_J)
                    S_採点結果_J.現ヒート番号 = New_S_採点結果_J.現ヒート番号
                    S_採点結果_J.lastUpdate = New_S_採点結果_J.lastUpdate
                Else
                    '最初のヒート
                    S_採点結果_J = New_S_採点結果_J.Clone
                End If

                New_S_採点結果_J = Nothing

                S_採点結果_J.JSON書き出し()


                'ASHEAT電文を返す
                Dim 種目順 As Integer = マスタデータ.D_種目マスタ.Get_種目順(S_採点結果_J.区分番号, S_採点結果_J.ラウンド番号, S_採点結果_J.現種目記号)
                Dim ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(種目順)
                全レコード数 = SEND_ANSHEAT(S_採点結果_J.ジャッジ記号, S_採点結果_J.区分番号, S_採点結果_J.ラウンド番号, 種目順, S_採点結果_J.現ヒート番号, "次")
                '全レコード数 = ANSHEAT作成_JSON(S_採点結果_J.ジャッジ記号, S_採点結果_J.区分番号, S_採点結果_J.ラウンド番号, マスタデータ.D_種目マスタ.Get_種目順(S_採点結果_J.区分番号, S_採点結果_J.ラウンド番号, S_採点結果_J.種目記号))


                Me.区分番号 = S_採点結果_J.区分番号
                Me.ラウンド番号 = S_採点結果_J.ラウンド番号
                Me.ジャッジ記号 = S_採点結果_J.ジャッジ記号
                'Me.種目記号 = S_採点結果_J.現種目記号


                If Strings.Left(S_採点結果_J.採点方式, 3) = "PDJ" Or Strings.Left(S_採点結果_J.採点方式, 3) = "VAL" Then
                    '    Me.ヒート番号 = S_採点結果_J.ヒート番号
                    'これをやると、前のヒート番号になってしまう。
                Else
                    'チェック法、順位法の時　　種目毎送信なので。。。
                    Me.ヒート番号 = 0
                End If




                S_採点結果_J = Nothing



            ElseIf str.Split(",")(1) = "SNDTEMP_V2_J" Then

                Me.端末名 = str.Split(",")(2)

                Dim S_採点結果_J As S_採点結果_V2_J
                S_採点結果_J = New S_採点結果_V2_J(マスタデータ.Z_システム設定.Comp_filepath)
                S_採点結果_J.Set_LOG(LOG)

                '配列(0)-(4)とカンマを削除する

                Dim 配列 = str.Split(",")

                Dim 削除文字列 As String = ""
                For i = 0 To 4
                    削除文字列 = 削除文字列 & 配列(i) & ","
                Next i

                str = str.Replace(削除文字列, "")

                'ヒート合成　strには1ヒート分しか入っていないので、元のファイルにStrのヒート分を合成
                'V2では、全ヒート分をジャッジ端末と送受信するので、合成は不要。

                'Dim New_S_採点結果_J = S_採点結果_J.新JSONデータセット(str)     '2022/6/2 新に変更
                'New_S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)

                'S_採点結果_J に、区分、ラウンド、ジャッジ記号をセット

                S_採点結果_J = S_採点結果_J.新JSONデータセット(str)
                S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)
                'S_採点結果_J = S_採点結果_J.JSON読み込み()

                'If S_採点結果_J IsNot Nothing Then
                'S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)
                'S_採点結果_J.JSON追加(New_S_採点結果_J)
                'S_採点結果_J.現ヒート番号 = New_S_採点結果_J.現ヒート番号
                'Else
                '最初のヒート
                'S_採点結果_J = New_S_採点結果_J.Clone
                'End If

                'New_S_採点結果_J = Nothing

                S_採点結果_J.JSON書き出し()



                Me.区分番号 = S_採点結果_J.区分番号
                Me.ラウンド番号 = S_採点結果_J.ラウンド番号
                Me.ジャッジ記号 = S_採点結果_J.ジャッジ記号
                Me.種目記号 = S_採点結果_J.現種目記号


                If Strings.Left(S_採点結果_J.採点方式, 3) = "PDJ" Or Strings.Left(S_採点結果_J.採点方式, 3) = "VAL" Then
                    Me.ヒート番号 = S_採点結果_J.現ヒート番号
                    'これをやると、前のヒート番号になってしまう。
                Else
                    'チェック法、順位法の時　　種目毎送信なので。。。
                    Me.ヒート番号 = 0
                End If


                Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & ":TCPClient SendTempイベント発行")

                RaiseEvent SendTempイベント(Me, S_採点結果_J)



                S_採点結果_J = Nothing





                ElseIf str.Split(",")(1) = "SNDRESLT_BR2_J" Then

                    Me.端末名 = str.Split(",")(2)

                    Dim S_採点結果_BR2_J As S_採点結果_BR2_J
                    S_採点結果_BR2_J = New S_採点結果_BR2_J(マスタデータ.Z_システム設定.Comp_filepath)

                    '配列(0)-(4)とカンマを削除する

                    Dim 配列 = str.Split(",")

                    Dim 削除文字列 As String = ""
                    For i = 0 To 4
                        削除文字列 = 削除文字列 & 配列(i) & ","
                    Next i

                    str = str.Replace(削除文字列, "")


                    S_採点結果_BR2_J = JsonConvert.DeserializeObject(Of S_採点結果_BR2_J)(str)
                    S_採点結果_BR2_J.Set_FilePath(マスタデータ.Z_システム設定.Comp_filepath)
                    'S_採点結果_J.JSONデータセット(str)


                    S_採点結果_BR2_J.JSON書き出し()


                    'ASHEAT電文を返す
                    'Dim 種目順 As Integer = マスタデータ.D_種目マスタ.Get_種目順(S_採点結果_J.区分番号, S_採点結果_J.ラウンド番号, S_採点結果_J.種目記号)
                    Dim ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(S_採点結果_BR2_J.種目番号)

                    'Ver1.02.20  2022/1/19修正
                    'ANSHEAT時に、S_採点結果_BR2_J.種目番号　を返していたが、これはジャッジ端末の1種目目が何だったのかを表しているだけで、
                    '何ラウンド（種目）の結果が返ってきたかは表していない。
                    'そこで、種目記号_2 がブランクかどうかで、何種目の結果が返って来たかを判定し、採点が終わった種目番号を返すようにする。

                    Dim 採点終了種目番号 As Integer = 0

                    If S_採点結果_BR2_J.種目記号_2 = "" Then
                        採点終了種目番号 = S_採点結果_BR2_J.種目番号
                    Else
                        採点終了種目番号 = S_採点結果_BR2_J.種目番号 + 1
                    End If


                    Dim SG種別 As String = マスタデータ.D_種目マスタ.Get_種目Class(S_採点結果_BR2_J.区分番号, S_採点結果_BR2_J.ラウンド番号, 採点終了種目番号).SG種別
                    If SG種別 = "S" Then

                        全レコード数 = SEND_ANSHEAT(S_採点結果_BR2_J.ジャッジ記号, S_採点結果_BR2_J.区分番号, S_採点結果_BR2_J.ラウンド番号, 採点終了種目番号, S_採点結果_BR2_J.ヒート番号, "次")


                    Else
                        '全レコード数 = SEND_ANSHEAT(S_採点結果_BR2_J.ジャッジ記号, S_採点結果_BR2_J.区分番号, S_採点結果_BR2_J.ラウンド番号, S_採点結果_BR2_J.種目番号, ヒート数, "次")
                        全レコード数 = SEND_ANSHEAT(S_採点結果_BR2_J.ジャッジ記号, S_採点結果_BR2_J.区分番号, S_採点結果_BR2_J.ラウンド番号, 採点終了種目番号, ヒート数, "次")


                    End If



                    'Ver1.02.20  2022/1/19修正　ここまで


                    Me.区分番号 = S_採点結果_BR2_J.区分番号
                    Me.ラウンド番号 = S_採点結果_BR2_J.ラウンド番号
                    Me.ジャッジ記号 = S_採点結果_BR2_J.ジャッジ記号
                    'Me.種目記号 = S_採点結果_BR2_J.種目記号
                    Me.ヒート番号 = 0


                    '  'GMの現在競技以外でSENDされても、進行を更新するため。
                    CheckAllJudgSend_BR2(Me.区分番号, Me.ラウンド番号)



                    S_採点結果_BR2_J = Nothing

                ElseIf str.Split(",")(1) = "SNDTEMP_BR2_J" Then


                    Me.端末名 = str.Split(",")(2)

                    Dim S_採点結果_BR2_J As S_採点結果_BR2_J
                    'S_採点結果_BR2_J = New S_採点結果_BR2_J(マスタデータ.Z_システム設定.Comp_filepath)

                    '配列(0)-(4)とカンマを削除する

                    Dim 配列 = str.Split(",")

                    Dim 削除文字列 As String = ""
                    For i = 0 To 4
                        削除文字列 = 削除文字列 & 配列(i) & ","
                    Next i

                    str = str.Replace(削除文字列, "")


                    S_採点結果_BR2_J = JsonConvert.DeserializeObject(Of S_採点結果_BR2_J)(str)
                    S_採点結果_BR2_J.Set_FilePath(マスタデータ.Z_システム設定.Comp_filepath)


                    'S_採点結果_J.JSONデータセット(str)


                    S_採点結果_BR2_J.JSON書き出し()



                    Me.区分番号 = S_採点結果_BR2_J.区分番号
                    Me.ラウンド番号 = S_採点結果_BR2_J.ラウンド番号
                    Me.ジャッジ記号 = S_採点結果_BR2_J.ジャッジ記号
                    Me.種目記号 = S_採点結果_BR2_J.種目記号
                    Me.ヒート番号 = 0


                    S_採点結果_BR2_J = Nothing

                ElseIf str.Split(",")(1) = "REQHELP" Then


                    'ここから下は、司会端末の処理
                ElseIf str.Split(",")(1) = "REQSTATUS" Then
                    '端末がコネクトしてきた時

                    Me.端末名 = str.Split(",")(2)

                    'ANSKUBUN電文を返す
                    全レコード数 = SEND_NOWSTATUS(）

                ElseIf str.Split(",")(1) = "HEATSTART" Then
                    'ヒート開始のイベント
                    Me.端末名 = str.Split(",")(2)

                ElseIf str.Split(",")(1) = "SENDBM" Then
                    'ブックマークイベント
                    Me.端末名 = str.Split(",")(2)

                    WriteBookmark(str, False)


                ElseIf str.Split(",")(1) = "REQBMLIST" Then
                    'ブックマーク一覧の要求
                    Me.端末名 = str.Split(",")(2)

                    'ANSBMLIST電文を返す
                    全レコード数 = SEND_ANSBMLIST(str.Split(",")(5), str.Split(",")(6), str.Split(",")(7), str.Split(",")(8))


                    '***********ここから下は関連端末の処理 *****************************************************
                ElseIf str.Split(",")(1) = "KREQ_MA_COMP" Then
                    Me.端末名 = str.Split(",")(2)

                    'KANS_MA_COMP電文を返す
                    全レコード数 = SEND_KANS_MA_COMP()


                ElseIf str.Split(",")(1) = "KREQ_MB_KUBUN" Then

                    Me.端末名 = str.Split(",")(2)

                    'KANS_MB_KUBUN電文を返す
                    全レコード数 = SEND_KANS_MB_KUBUN()

                ElseIf str.Split(",")(1) = "KREQ_MU_Progress" Then

                    Me.端末名 = str.Split(",")(2)

                    Me.区分番号 = str.Split(",")(5)
                    Me.ラウンド番号 = str.Split(",")(6)

                    'KANS_MU_Progress電文を返す
                    全レコード数 = SEND_KANS_MU_Progress(区分番号, ラウンド番号)

                ElseIf str.Split(",")(1) = "KREQ_HEAT" Then

                    Me.端末名 = str.Split(",")(2)

                    Me.区分番号 = str.Split(",")(5)
                    Me.ラウンド番号 = str.Split(",")(6)
                    Dim 種目順 = str.Split(",")(7)
                    Me.ヒート番号 = CInt(str.Split(",")(8))


                    'KANS_HEAT電文を返す
                    全レコード数 = SEND_KANS_HEAT(区分番号, ラウンド番号, 種目順, ヒート番号)

                ElseIf str.Split(",")(1) = "KREQ_FF_KUBUN" Then

                    Me.端末名 = str.Split(",")(2)

                    'KANS_MB_KUBUN電文を返す
                    全レコード数 = SEND_KANS_FF_KUBUN()

                ElseIf str.Split(",")(1) = "KREQ_FF_RESULT" Then

                    Me.端末名 = str.Split(",")(2)

                    Me.区分番号 = str.Split(",")(5)

                    'KANS_MU_Progress電文を返す
                    全レコード数 = SEND_KANS_FF_RESULT(区分番号)

                ElseIf str.Split(",")(1) = "KREQ_JUDGE" Then

                    Me.端末名 = str.Split(",")(2)

                    Me.区分番号 = str.Split(",")(5)
                    Me.ラウンド番号 = str.Split(",")(6)

                    'KANS_JUDGE電文を返す
                    全レコード数 = SEND_KANS_JUDGE(区分番号, ラウンド番号)

                ElseIf str.Split(",")(1) = "KREQ_RESULT" Then

                    Me.端末名 = str.Split(",")(2)

                    Me.区分番号 = str.Split(",")(5)
                    Me.ラウンド番号 = str.Split(",")(6)

                    'KANS_RESULT電文を返す
                    全レコード数 = SEND_KANS_RESULT(区分番号, ラウンド番号)

                ElseIf str.Split(",")(1) = "KREQ_RESULT_J" Then

                    Me.端末名 = str.Split(",")(2)

                    Me.区分番号 = str.Split(",")(5)
                    Me.ラウンド番号 = str.Split(",")(6)

                    'KANS_RESULT_J電文を返す
                    全レコード数 = SEND_KANS_RESULT_J(区分番号, ラウンド番号)



                    '***********ここから下は結果表示端末の処理 *****************************************************

                ElseIf str.Split(",")(1) = "DREQ_MASTER_J" Then

                    Me.端末名 = str.Split(",")(2)



                    'DANS_MASTER_J電文を返す
                    全レコード数 = SEND_DANS_マスタデータ()


                ElseIf str.Split(",")(1) = "DREQ_STARTLIST_J" Then

                    Me.端末名 = str.Split(",")(2)
                    Me.区分番号 = str.Split(",")(5)
                    Me.ラウンド番号 = str.Split(",")(6)


                    'DANS_MASTER_J電文を返す
                    全レコード数 = SEND_DANS_スタートリスト(区分番号, ラウンド番号)


                ElseIf str.Split(",")(1) = "DREQ_RESULT_J" Then

                    Me.端末名 = str.Split(",")(2)
                    Me.区分番号 = str.Split(",")(5)
                    Me.ラウンド番号 = str.Split(",")(6)


                    'DANS_MASTER_J電文を返す
                    全レコード数 = SEND_DANS_採点結果(区分番号, ラウンド番号)


                ElseIf str.Split(",")(1) = "DEVENT_J" Then

                    Me.端末名 = str.Split(",")(2)



            End If




            RaiseEvent ReceivedData(Me, e)


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub


    ''' サーバーから切断された、あるいは切断した

    Public Event Disconnected As EventHandler
    Protected Overridable Sub OnDisconnected(ByVal e As EventArgs)
        RaiseEvent Disconnected(Me, e)
    End Sub

    '========メソッド==============================================

    '==結果書き込み
    Private Sub WriteResult(str As String, Send As Boolean)
        '電文 Str の内容を 結果ファイルに書き出す
        'send がfalseだったら、Temp

        Dim データ As S_採点

        Call マスタデータ.C_ラウンドマスタ.FileRead()
        Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        If 採点方式 = "チェック法" Then

            Dim S_採点結果_J As S_採点結果_J
            S_採点結果_J = New S_採点結果_J(マスタデータ.Z_システム設定.Comp_filepath)

            S_採点結果_J.区分番号 = 区分番号
            S_採点結果_J.ラウンド番号 = ラウンド番号
            S_採点結果_J.種目記号 = 種目記号
            S_採点結果_J.ジャッジ記号 = ジャッジ記号

            'S_採点結果_J.JSON読み込み()

            Dim jStr As String = str.Split(",")(11)

            S_採点結果_J = S_採点結果_J.新JSONデータセット(jStr)
            S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)

            S_採点結果_J.JSON書き出し()



        ElseIf 採点方式 = "順位法" Then


        ElseIf Strings.Left(採点方式, 3) = "PDJ" Then


            Dim S_採点結果_J As S_採点結果_V2_J
            S_採点結果_J = New S_採点結果_V2_J(マスタデータ.Z_システム設定.Comp_filepath)
            S_採点結果_J.Set_LOG(LOG)

            S_採点結果_J.区分番号 = 区分番号
            S_採点結果_J.ラウンド番号 = ラウンド番号
            S_採点結果_J.現種目記号 = 種目記号
            S_採点結果_J.ジャッジ記号 = ジャッジ記号

            'S_採点結果_J.JSON読み込み()

            Dim jStr As String = str.Split(",")(11)

            S_採点結果_J = S_採点結果_J.新JSONデータセット(jStr)
            S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)

            S_採点結果_J.JSON書き出し()



        ElseIf Strings.Left(採点方式, 4) = "BJPR" Then

            'BJPRの時はここは通らないはず。

            Dim S_採点結果_J As S_採点結果_J
            S_採点結果_J = New S_採点結果_J(マスタデータ.Z_システム設定.Comp_filepath)

            S_採点結果_J.区分番号 = 区分番号
            S_採点結果_J.ラウンド番号 = ラウンド番号
            S_採点結果_J.種目記号 = 種目記号
            S_採点結果_J.ジャッジ記号 = ジャッジ記号

            Dim jStr As String = str.Split(",")(11)

            S_採点結果_J = S_採点結果_J.新JSONデータセット(jStr)
            'S_採点結果_J.Set_filepath(マスタデータ.Z_システム設定.Comp_filepath)

            'S_採点結果_J.JSON書き出し()


        ElseIf Strings.Left(採点方式, 4) = "BJS2" Or Strings.Left(採点方式, 4) = "BJS3" Then


            Dim S_採点結果_J As S_採点結果_BR2_J
            S_採点結果_J = New S_採点結果_BR2_J(マスタデータ.Z_システム設定.Comp_filepath)

            S_採点結果_J.区分番号 = 区分番号
            S_採点結果_J.ラウンド番号 = ラウンド番号
            S_採点結果_J.種目記号 = 種目記号
            S_採点結果_J.ジャッジ記号 = ジャッジ記号

            'S_採点結果_J.JSON読み込み()

            Dim jStr As String = str.Split(",")(11)

            S_採点結果_J.JSONデータセット(jStr)

            S_採点結果_J.JSON書き出し()



        Else
            '新審判の場合
            Dim S_採点結果 As S_採点結果
            S_採点結果 = New S_採点結果(マスタデータ.Z_システム設定.Comp_filepath)

            S_採点結果.Read(区分番号, ラウンド番号, 種目記号, ジャッジ記号)

            データ = New S_採点


            Dim 出場選手数 As Integer = str.Split(",")(10)
            'Dim PCS数 As Integer = str.Split(",")(11)
            Dim 背番号 As String
            Dim PCS番号リスト As String
            Dim PCS番号 As Integer
            Dim 点数 As Double

            'ジャッジ種別を確認
            Dim ジャッジ区分 As String = ""

            Dim 審判担当チーム As Integer = マスタデータ.C_ラウンドマスタ.Get担当審判グループ(区分番号, ラウンド番号)
            ジャッジ区分 = マスタデータ.審判員マスタ.Get_審判Class(ジャッジ記号).審判チーム(審判担当チーム)



            If Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号), 3) = "BJS" Then
                'ブレイキンの時

                For 選手数 = 1 To 2　　 '背番号は交互に出てくるか、8パネルはすべて使うはず。

                    If 選手数 = 1 Then
                        背番号 = str.Split(",")(12)
                    Else
                        背番号 = str.Split(",")(15)
                    End If

                    If 背番号 <> "" Then

                        'For i = 12 To 35 Step 3    'UBound(str.Split(","))      3x8パネル分　で24個データがある
                        For i = 12 To 35 Step 3   'UBound(str.Split(",")) Step 3
                            '35までにしないと、減点までPCSに登録されてしまう。。。　
                            'でも、なぜかUboundで最後までみてて,三笠宮杯では問題無かった。。。 理由不明だが 2020/3/4修正


                            If 背番号 = str.Split(",")(i) Then

                                PCS番号リスト = str.Split(",")(i + 1)

                                For m = 1 To PCS番号リスト.Length
                                    'PCS番号には、1～0 の数字が入っている
                                    PCS番号 = CInt(Mid(PCS番号リスト, m, 1))

                                    点数 = str.Split(",")(i + 2)

                                    データ.点数(PCS番号) = 点数
                                Next m

                                データ.背番号 = 背番号

                                If Send = True Then
                                    データ.SEND_FLAG = 1
                                Else
                                    データ.SEND_FLAG = 0
                                End If

                                'i = i + 2
                            End If

                        Next i

                        '減点分登録
                        For i = 36 To UBound(str.Split(",")) Step 3

                            If 背番号 = str.Split(",")(i) And 背番号 <> "" Then

                                データ.背番号 = 背番号
                                データ.減点(CInt(str.Split(",")(i + 1))) = CDbl(str.Split(",")(i + 2))


                                If Send = True Then
                                    データ.SEND_FLAG = 1
                                Else
                                    データ.SEND_FLAG = 0
                                End If

                            Else


                            End If


                        Next i



                        'ファイルに書き込み
                        S_採点結果.登録(データ)

                    End If

                    '書き込んだらクリア
                    データ = New S_採点

                Next 選手数



            Else
                'ブレイキン以外の時

                If ジャッジ区分 <> "R" Then
                    '通常ジャッジの時

                    背番号 = str.Split(",")(12)
                    For i = 12 To UBound(str.Split(","))

                        If 背番号 = str.Split(",")(i) Then

                            'PCS名 = str.Split(",")(i + 1)
                            'PCS番号 = マスタデータ.J_新審判設定.GetPCS番号(PCS名)
                            PCS番号リスト = str.Split(",")(i + 1)

                            For m = 1 To PCS番号リスト.Length
                                'PCS番号には、1～0 の数字が入っている
                                PCS番号 = CInt(Mid(PCS番号リスト, m, 1))

                                点数 = str.Split(",")(i + 2)

                                データ.点数(PCS番号) = 点数
                            Next m

                            データ.背番号 = 背番号

                            If Send = True Then
                                データ.SEND_FLAG = 1
                            Else
                                データ.SEND_FLAG = 0
                            End If

                            i = i + 2
                        Else
                            'ファイルに書き込み
                            S_採点結果.登録(データ)

                            '書き込んだらクリア
                            データ = New S_採点

                            '次の背番号をセット
                            背番号 = str.Split(",")(i)

                            If 背番号 = "" Then
                                '終わり
                                i = UBound(str.Split(","))
                            Else
                                '一つ戻す
                                i = i - 1
                            End If

                        End If


                    Next i

                Else
                    '減点ジャッジの時　***************************************

                    'Sendの時はとりあえず、0点で書き込む
                    For s = 1 To 出場選手数
                        データ.背番号 = str.Split(",")(11 + s)

                        If Send = True Then
                            データ.SEND_FLAG = 1
                        Else
                            データ.SEND_FLAG = 0
                        End If

                        'ファイルに書き込み
                        S_採点結果.登録(データ)

                        '書き込んだらクリア
                        データ = New S_採点

                    Next s


                    背番号 = str.Split(",")(12 + 出場選手数)
                    For i = 12 + 出場選手数 To UBound(str.Split(","))   '背番号, 減点項目番号, 減点がそれぞれ入っている
                        If 背番号 = str.Split(",")(i) And 背番号 <> "" Then

                            データ.背番号 = 背番号
                            データ.減点(CInt(str.Split(",")(i + 1))) = CDbl(str.Split(",")(i + 2))


                            If Send = True Then
                                データ.SEND_FLAG = 1
                            Else
                                データ.SEND_FLAG = 0
                            End If

                            i = i + 2

                        Else

                            If データ.背番号 IsNot Nothing Then

                                'ファイルに書き込み
                                S_採点結果.登録(データ)

                                '書き込んだらクリア
                                データ = New S_採点

                                '次の背番号をセット
                                背番号 = str.Split(",")(i)

                                If 背番号 = "" Then
                                    '終わり
                                    i = UBound(str.Split(","))
                                Else
                                    '一つ戻す
                                    i = i - 1
                                End If
                            End If
                        End If



                    Next i




                End If
            End If


        End If

    End Sub


    '全ジャッジがSendしたか確認する。
    Public Sub CheckAllJudgSend_BR2(区分番号, ラウンド番号)


        Dim S_採点結果_J As S_採点結果_BR2_J
        S_採点結果_J = New S_採点結果_BR2_J(マスタデータ.Z_システム設定.Comp_filepath)
        S_採点結果_J.区分番号 = 区分番号
        S_採点結果_J.ラウンド番号 = ラウンド番号

        Dim 審判G As Integer = マスタデータ.C_ラウンドマスタ.Get担当審判グループ(区分番号, ラウンド番号)
        Dim 審判員数 As Integer = マスタデータ.審判員マスタ.Get_審判員数(審判G)
        Dim 審判員記号リスト() = Nothing
        マスタデータ.審判員マスタ.Get_審判員記号(審判G, 審判員記号リスト)

        Dim 種目記号リスト() = Nothing

        Dim ヒート番号 = 1 'ブレイキンではヒートは１しかないから

        For d = 1 To マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

            '２種目目は何もしない
            If d <> 2 Then

                Dim 全SENDFLAG As Boolean = True

                S_採点結果_J.種目記号 = 種目記号リスト(d)


                For j = 1 To 審判員数

                    S_採点結果_J.ジャッジ記号 = 審判員記号リスト(j)
                    If S_採点結果_J.JSON読み込み() > 0 Then

                        If S_採点結果_J.SEND_FLAG = "1" Then

                        Else
                            '未SENDのジャッジがいる時
                            全SENDFLAG = False
                            j = 審判員数
                        End If
                    Else
                        '一度もデータを送っていないジャッジが居るとき
                        全SENDFLAG = False
                        j = 審判員数
                    End If
                Next j

                If 全SENDFLAG = True Then

                    進行管理更新_BR2(区分番号, ラウンド番号, d, ヒート番号)

                    If d = 1 Then  '1ラウンドの結果がSENDされたら、2ラウンドも終わったことにする
                        進行管理更新_BR2(区分番号, ラウンド番号, 2, ヒート番号)
                    End If

                    If d = 3 Then  '３ラウンドの結果がSENDされたら、４ラウンドも終わったことにする  '2021/07/18 　Ver1.02.16 で追加
                        進行管理更新_BR2(区分番号, ラウンド番号, 4, ヒート番号)
                    End If

                End If
            End If


        Next d


    End Sub

    '全JudgeのSENDが確認できたら、ここを呼び出して更新する
    Private Sub 進行管理更新_BR2(区分番号 As String, ラウンド番号 As String, 種目順 As Integer, ヒート番号 As Integer)

        マスタデータ.T_採点進行管理.FileRead()

        Dim T_採点進行 As T_採点進行 = Nothing
        T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

        マスタデータ.U_進行管理.FileRead()

        Dim U_進行 As U_進行
        U_進行 = マスタデータ.U_進行管理.Get_進行(T_採点進行.競技番号, T_採点進行.競技番号枝番, 種目順, ヒート番号)

        If U_進行 IsNot Nothing Then
            If U_進行.ステータス <> "全審判送信済み" Then


                U_進行.ステータス = "全審判送信済み"
                U_進行.採点終了時刻 = System.DateTime.Now


                マスタデータ.U_進行管理.登録(U_進行)

                U_進行 = Nothing

            End If
        End If

        T_採点進行 = Nothing


    End Sub




    '==Send ANSKUBUN
    Public Function SEND_ANSKUBUN() As Integer
        '端末がコネクトしてきたら、電文を返す
        '全レコード数を返す

        Dim Denbun As Array = ANSKUBUN作成()
        Dim d As Integer
        Dim 全レコード数 As Integer = 0

        For d = 1 To UBound(Denbun)
            If Left(Denbun(d), 2) = "JS" Then
                全レコード数 = 全レコード数 + 1
            End If
        Next d

        If 全レコード数 > 0 Then
            For d = 1 To 全レコード数
                If Left(Denbun(d), 2) = "JS" Then
                    Send(Denbun(d))
                End If
            Next d
        End If

        Return 全レコード数


    End Function

    Private Function ANSKUBUN作成() As Array
        '採点対象の区分・ジャッジを作成
        '電文配列を返す

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim i
        Dim Denbun(100) As String  '最大100区分の電文を作成することが可能
        Dim d As Integer = 1  '1-10
        Dim 全レコード件数 As Integer = 0

        For i = 1 To マスタデータ.T_採点進行管理.登録済みレコード数
            If マスタデータ.T_採点進行管理.リスト(i).ステータス = "ヒート表作成済み" And
               マスタデータ.T_採点進行管理.リスト(i).リアルタイムFLAG <> "N" And
               全レコード件数 <= 10 Then

                Dim 区分番号 As String = マスタデータ.T_採点進行管理.リスト(i).区分番号
                Dim ラウンド番号 As String = マスタデータ.T_採点進行管理.リスト(i).ラウンド番号


                Denbun(d) = Denbun(d) & CStr(d) & ","   ' レコード番号
                Denbun(d) = Denbun(d) & マスタデータ.A_競技会マスタ.競技会名 & ","   ' 競技会名
                Denbun(d) = Denbun(d) & 区分番号 & ","   ' 区分番号
                Denbun(d) = Denbun(d) & マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & ","   ' 区分名
                Denbun(d) = Denbun(d) & ラウンド番号 & ","   ' ラウンド番号

                If マスタデータ.Z_システム設定.言語 = "E" Then
                    Denbun(d) = Denbun(d) & マスタデータ.Get_ラウンド名_E(ラウンド番号) & ","   ' ラウンド名
                Else
                    Denbun(d) = Denbun(d) & マスタデータ.Get_ラウンド名(ラウンド番号) & ","   ' ラウンド名
                End If


                Dim ジャッジ数 As Integer = 0
                Dim ジャッジ電文 As String = ""
                For j = 1 To マスタデータ.審判員マスタ.Get_登録済み審判員数

                    If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(マスタデータ.C_ラウンドマスタ.Get担当審判グループ(区分番号, ラウンド番号)) <> "" Then

                        ジャッジ電文 = ジャッジ電文 & マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ記号 & ","   'ジャッジ記号
                        ジャッジ電文 = ジャッジ電文 & マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ表記名 & ","   'ジャッジ名
                        ジャッジ数 = ジャッジ数 + 1

                    End If
                Next j

                Denbun(d) = Denbun(d) & ジャッジ数 & ","  ' ジャッジ数
                Denbun(d) = Denbun(d) & ジャッジ電文 'ジャッジ記号　ジャッジ名

                全レコード件数 = 全レコード件数 + 1
                d = d + 1

            End If
        Next i

        'ヘッダー文字列を作成
        Dim Hedder As String = ""
        Hedder = "JS,"
        Hedder = Hedder & "ANSKUBUN,"
        Hedder = Hedder & "GM01,"
        Hedder = Hedder & CStr(全レコード件数) & ","   ' 全レコード数

        For d = 1 To 全レコード件数
            Denbun(d) = Hedder & Denbun(d)
        Next d


        Return Denbun

    End Function

    '==Send ANSKUBUN
    Public Function SEND_ANSHEAT(ジャッジ記号 As String, 区分番号 As String, ラウンド番号 As String, 種目順 As String, ヒート番号 As Integer, 現次指定 As String) As Integer
        '端末がコネクトしてきたら、電文を返す
        '全レコード数を返す

        Dim Denbun As Array = ANSHEAT作成(ジャッジ記号, 区分番号, ラウンド番号, 種目順, ヒート番号, 現次指定)
        Dim d As Integer
        Dim 全レコード数 As Integer = 0

        If Denbun Is Nothing Then


        Else
            For d = 1 To UBound(Denbun)
                If Left(Denbun(d), 2) = "JS" Then
                    全レコード数 = 全レコード数 + 1
                End If
            Next d
        End If

        If 全レコード数 > 0 Then
            For d = 1 To 全レコード数
                If Left(Denbun(d), 2) = "JS" Then
                    Send(Denbun(d))
                End If
            Next d
        End If

        If 全レコード数 = 0 Then
            '終了通知を送る
            Send("JS,ENDKUBUN,GM01,1,1")

        End If

        Return 全レコード数


    End Function


    Private Function ANSHEAT作成(ジャッジ記号 As String, 区分番号 As String, ラウンド番号 As String, 種目順 As String, ヒート番号 As Integer, 現次指定 As String) As Array
        '次ヒートの電文を作成  種目順が "00"、ヒート番号が 0 の時は最初のヒートを返す。
        '次ヒートが無いときは、Denbun(1)をブランクで返す
        '電文配列を返す
        '種目順が"00"、ヒート番号が"100" の時は、SEND済みの次のヒートを返す。全部SEND済みであれば、一番最初のヒートを返す。
        '新審判のときは、ヒート毎に返す （返す電文は1個だけ）
        '通常は現次設定は"次”、GMで指定したヒートを表示させたい時は"現"が指定される。

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ


        Dim Denbun(10) As String  '最大100区分の電文を作成することが可能
        Dim d As Integer = 1  '1-10
        Dim 全レコード件数 As Integer = 0

        Dim 次種目番号 As Integer
        Dim 次ヒート番号 As Integer


        'ヒート毎
        '区分番号
        '区分名
        'ラウンド番号
        'ラウンド名
        '採点方式 = "AJS30J"
        'ジャッジ記号
        'ジャッジ名
        'ジャッジ区分 = "ジャッジ"
        '種目記号
        '種目名
        'ソロ・グループ・マッチ種別
        'ヒート数（種目の）
        'ヒート番号
        '出場選手数
        'キャリ最大値
        'キャリ最小値
        'PCS1～10(採点対象のPCS名)
        '背番号
        'PCS番号
        '点数

        '該当種目番号の全ヒート数を数える
        '==ヒートデータ読込み
        If マスタデータ.E_ヒート表マスタ.FileCheck(区分番号, ラウンド番号) = True Then
            マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)

        Else
            '該当のヒート表が存在しない ---- エラー

        End If


        Dim ヒート数 As Integer = 0
        ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(種目順)

        If ヒート数 = 0 Then
            'エラー

        End If

        '
        'ヒート番号が100, 種目順が"00"だったら、SEND済みの最後のヒートを探す
        If ヒート番号 = 100 And 種目順 = "00" Then

            Dim 種目記号リスト() = Nothing

            Dim 未SEND種目番号 As Integer = 0
            Dim 未SENDヒート番号 As Integer = 0


            Dim 種目数 As Integer = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

            For 種目番号 = 1 To 種目数

                Dim 採点結果 As S_採点結果_V2_J = New S_採点結果_V2_J(マスタデータ.Z_システム設定.Comp_filepath)

                採点結果.Set_LOG(LOG)

                採点結果.区分番号 = 区分番号
                採点結果.ラウンド番号 = ラウンド番号

                採点結果.現種目記号 = 種目記号リスト(種目番号)
                採点結果.ジャッジ記号 = ジャッジ記号
                採点結果 = 採点結果.JSON読み込み(）

                If 採点結果 IsNot Nothing Then
                    'ジャッジの結果が一度でもSendされているとき

                    For ヒート = 1 To マスタデータ.E_ヒート表マスタ.Getヒート数(種目番号)
                        'ヒートの中で、1人の選手でもSENDがあればOKと見なす。

                        For 選手 = 1 To 採点結果.選手数
                            If ヒート = 採点結果.S_採点結果_選手_J(選手).ヒート番号 Then

                                If 採点結果.S_採点結果_選手_J(選手).SEND_FLAG = "1" Then


                                Else

                                    If ヒート = 1 Then
                                        '1ヒートの時は、前の種目の最終ヒートにする

                                        未SEND種目番号 = 種目番号 - 1


                                        If 種目番号 = 1 Then
                                            '最初の種目の時は ヒート番号も種目番号も0を設定
                                            未SENDヒート番号 = 0
                                        Else
                                            '前種目の最終ヒートを算出
                                            Dim 最終ヒート番号 = マスタデータ.E_ヒート表マスタ.Getヒート数(種目番号 - 1)

                                            未SENDヒート番号 = 最終ヒート番号

                                        End If

                                    Else
                                        '同じ種目の2ヒート目以降の時は、1つ前のヒート番号を指定する

                                        未SEND種目番号 = 種目番号
                                        未SENDヒート番号 = ヒート - 1
                                    End If


                                    ヒート = マスタデータ.E_ヒート表マスタ.Getヒート数(種目番号)
                                    種目番号 = 種目数

                                End If

                                選手 = 採点結果.選手数
                            End If
                        Next 選手
                    Next ヒート


                Else
                    'ジャッジの結果が一度もSendされていないとき

                    未SEND種目番号 = 種目番号 - 1


                    If 種目番号 = 1 Then
                        '最初の種目の時は ヒート番号も種目番号も0を設定
                        未SENDヒート番号 = 0
                    Else
                        '2種目目以降の時は、前種目の最終ヒートを算出
                        Dim 最終ヒート番号 = マスタデータ.E_ヒート表マスタ.Getヒート数(種目番号 - 1)

                        未SENDヒート番号 = 最終ヒート番号
                    End If


                    種目番号 = 種目数
                End If

            Next 種目番号



            ヒート番号 = 未SENDヒート番号
            種目順 = 未SEND種目番号

            ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(種目順)

        End If




        '次のヒート番号（種目番号）を確定する
        If ヒート数 > ヒート番号 Then
            'その種目の次のヒート

            次種目番号 = CInt(種目順)
            次ヒート番号 = ヒート番号 + 1
        Else
            '次の種目の1ヒート

            次種目番号 = CInt(種目順) + 1

            マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
            Dim 次種目ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(CStr(次種目番号))

            If 次種目ヒート数 = 0 And 現次指定 = "次" Then
                'そのラウンドの終わり
                次ヒート番号 = 0

                Return Denbun
            Else
                次ヒート番号 = 1
                ヒート数 = 次種目ヒート数

            End If

        End If


        '"現の時”
        If 現次指定 = "現" Then
            次種目番号 = CInt(種目順)
            次ヒート番号 = ヒート番号
            ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(種目順)


            '「前」が指定されている時
        ElseIf 現次指定 = "前" Then
            '1つ前のヒート番号を設定する

            If ヒート番号 = 1 Then
                '1ヒートの時は、前の種目の最終ヒートにする


                If CInt(種目順) = 1 Then
                    '最初の種目の時は ヒート番号も種目番号もそのまま

                    次ヒート番号 = 1

                Else
                    '前種目の最終ヒートを算出
                    Dim 最終ヒート番号 = マスタデータ.E_ヒート表マスタ.Getヒート数(CInt(種目順) - 1)

                    次ヒート番号 = 最終ヒート番号
                    次種目番号 = CInt(種目順) - 1

                End If

            Else
                '同じ種目の2ヒート目以降の時は、1つ前のヒート番号を指定する

                次種目番号 = CInt(種目順)
                次ヒート番号 = ヒート番号 - 1
            End If


        End If



        '===順位法とチェック法の時は、JSON形式に移動
        If マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) = "チェック法" Or
           マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) = "順位法" Or
            Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号), 4) = "BJPR" Then       'ブレイキンプレセレクションの場合

            If マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) = "チェック法" Or
               マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) = "順位法" Then

                Denbun(1) = ANSHEAT作成_JSON(ジャッジ記号, 区分番号, ラウンド番号, 次種目番号, 0)    '次ヒートは　0(全ヒート）

            Else
                Denbun(1) = ANSHEAT作成_JSON(ジャッジ記号, 区分番号, ラウンド番号, 次種目番号, 次ヒート番号)

            End If



            'ヘッダー文字列を作成
            Dim Hedder As String = ""
            Hedder = "JS,"
            Hedder = Hedder & "ANSHEAT_J,"
            Hedder = Hedder & "GM01,"
            全レコード件数 = 1   '

            Hedder = Hedder & CStr(全レコード件数) & ",1,"   ' 全レコード数

            For d = 1 To 全レコード件数
                Denbun(d) = Hedder & Denbun(d)
                '改行コードを削除
                Denbun(d) = Denbun(d).Replace(Environment.NewLine, "")
            Next d



            Return Denbun
            Exit Function



        ElseIf Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号), 4) = "BJS2" Or
               Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号), 4) = "BJS3" Then
            'BJS Ver2 か Ver3 だったら JSON形式

            'Ver1.02.20  2022/1/19修正　　種目順が00 の時は、次種目番号（終了したラウンド番号）は0で返す。

            'Denbun(1) = ANSHEAT作成_BR2_JSON(ジャッジ記号, 区分番号, ラウンド番号, 次種目番号)
            Denbun(1) = ANSHEAT作成_BR2_JSON(ジャッジ記号, 区分番号, ラウンド番号, CInt(種目順), 次ヒート番号)




            'ヘッダー文字列を作成
            Dim Hedder As String = ""
            Hedder = "JS,"
            Hedder = Hedder & "ANSHEAT_BR2_J,"
            Hedder = Hedder & "GM01,"
            全レコード件数 = 1   '

            Hedder = Hedder & CStr(全レコード件数) & ",1,"   ' 全レコード数

            If Denbun(1) <> "" Then
                For d = 1 To 全レコード件数
                    Denbun(d) = Hedder & Denbun(d)
                    '改行コードを削除
                    Denbun(d) = Denbun(d).Replace(Environment.NewLine, "")
                Next d
            End If



            Return Denbun
            Exit Function

        ElseIf Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号), 3) = "PDJ" Or
           Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号), 3) = "VAL" Then
            'PDJ かVALだったら JSON形式


            Denbun(1) = ANSHEAT作成_V2_JSON(ジャッジ記号, 区分番号, ラウンド番号, 次種目番号, 次ヒート番号)




            'ヘッダー文字列を作成
            Dim Hedder As String = ""
            Hedder = "JS,"
            Hedder = Hedder & "ANSHEAT_V2_J,"
            Hedder = Hedder & "GM01,"
            全レコード件数 = 1   '

            Hedder = Hedder & CStr(全レコード件数) & ",1,"   ' 全レコード数

            If Denbun(1) <> "" Then
                For d = 1 To 全レコード件数
                    Denbun(d) = Hedder & Denbun(d)
                    '改行コードを削除
                    Denbun(d) = Denbun(d).Replace(Environment.NewLine, "")
                Next d
            End If



            Return Denbun
            Exit Function

        End If






        '出場背番号を確定する

        Denbun(d) = Denbun(d) & CStr(d) & ","   ' レコード番号
        Denbun(d) = Denbun(d) & 区分番号 & ","   ' 区分番号
        Denbun(d) = Denbun(d) & マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & ","   ' 区分名
        Denbun(d) = Denbun(d) & ラウンド番号 & ","   ' ラウンド番号
        Denbun(d) = Denbun(d) & マスタデータ.Get_ラウンド名(ラウンド番号) & ","   ' ラウンド名
        Denbun(d) = Denbun(d) & マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) & ","   ' 採点方式
        Denbun(d) = Denbun(d) & ジャッジ記号 & ","   ' ジャッジ記号

        Dim JJ As Integer = マスタデータ.審判員マスタ.Get_レコード番号(ジャッジ記号)
        Dim D_i As Integer = マスタデータ.D_種目マスタ.Get_レコード番号(区分番号, ラウンド番号, String.Format("{0:D2}", 次種目番号))
        Dim 担当審判グループ As Integer = マスタデータ.D_種目マスタ.リスト(D_i).担当審判グループ

        Denbun(d) = Denbun(d) & マスタデータ.審判員マスタ.審判員リスト(JJ).ジャッジ表記名 & ","   ' ジャッジ表記名
        'Denbun(d) = Denbun(d) & マスタデータ.審判員マスタ.審判員リスト(JJ).審判チーム(担当審判グループ) & ","   ' ジャッジ区分
        If マスタデータ.審判員マスタ.審判員リスト(JJ).審判チーム(担当審判グループ) = "1" Or マスタデータ.審判員マスタ.審判員リスト(JJ).審判チーム(担当審判グループ) = "L" Then
            Denbun(d) = Denbun(d) & "ジャッジ" & ","   ' ジャッジ区分
        ElseIf マスタデータ.審判員マスタ.審判員リスト(JJ).審判チーム(担当審判グループ) = "R" Then
            Denbun(d) = Denbun(d) & "減点" & ","   ' ジャッジ区分
        End If

        If マスタデータ.Z_システム設定.Get_種目名称(マスタデータ.D_種目マスタ.リスト(D_i).種目記号) Is Nothing Then
            MsgBox("Z_system設定ファイルに、種目名が設定されていません。種目記号:" & マスタデータ.D_種目マスタ.リスト(D_i).種目記号)
        End If


        Denbun(d) = Denbun(d) & マスタデータ.D_種目マスタ.リスト(D_i).種目記号 & ","   ' 種目記号
        Denbun(d) = Denbun(d) & マスタデータ.Z_システム設定.Get_種目名称(マスタデータ.D_種目マスタ.リスト(D_i).種目記号).種目名_E & ","   ' 種目名
        Denbun(d) = Denbun(d) & マスタデータ.D_種目マスタ.リスト(D_i).SG種別 & ","   ' ソロ グループ種別

        Denbun(d) = Denbun(d) & CStr(ヒート数) & ","   ' ヒート数（種目の）
        Denbun(d) = Denbun(d) & CStr(次ヒート番号) & ","   ' ヒート番号

        Dim 背番号リスト(1) As String
        Dim 出場選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(CStr(次種目番号), 次ヒート番号, 背番号リスト)

        Denbun(d) = Denbun(d) & CStr(出場選手数) & ","   ' 出場選手数



        '=======チェック法、順位法の時
        If マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) = "チェック法" Or
           マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) = "順位法" Then


        Else

            '=======新審判、ブレイキンの時
            '=======ここから下は新審判の時用

            'ジャッジ区分がジャッジの時
            If マスタデータ.審判員マスタ.審判員リスト(JJ).審判チーム(担当審判グループ) = "1" Or マスタデータ.審判員マスタ.審判員リスト(JJ).審判チーム(担当審判グループ) = "L" Then

                Denbun(d) = Denbun(d) & マスタデータ.D_種目マスタ.リスト(D_i).CaliMax & ","   ' Cali最大値
                Denbun(d) = Denbun(d) & マスタデータ.D_種目マスタ.リスト(D_i).CaliMin & ","   ' Cali最小値




                '=============PCS名　1から10
                ' Dim 曲番号 As Integer = マスタデータ.E_ヒート表マスタ.Get_曲番号(CStr(次種目番号), 次ヒート番号)
                マスタデータ.F_審判担当PCSマスタ.Read(区分番号, ラウンド番号)

                Dim 審判PCS As F_審判担当PCS = マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(ジャッジ記号)

                If 審判PCS Is Nothing Then
                    MsgBox("ジャッジ「" & ジャッジ記号 & "」の担当PCSが設定されていません。")
                    Return Denbun
                    Exit Function
                Else


                End If


                'PCSは、曲毎ではなく、種目毎に変わる
                Dim 担当PCS番号 As String = 審判PCS.担当PCS番号(次種目番号)
                '担当PCS番号には、1234 とか 12 とか入っている

                For i = 1 To 9
                    If 担当PCS番号.Contains(CStr(i)) Then

                        マスタデータ.J_新審判設定.Set_新審判基準VER(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号))

                        If マスタデータ.J_新審判設定.PCS設定(i) Is Nothing Then
                            MsgBox("PCS番号:" & i & "は、設定されたPCS数より大きいため処理が続行できません。新審判基準設定ファイルの整合性を確認してください。")
                            Return Denbun
                            Exit Function
                        End If


                        Dim PCS名 As String = マスタデータ.J_新審判設定.PCS設定(i).PCS項目名
                            Denbun(d) = Denbun(d) & PCS名 & ","   ' PCS名 あり

                        Else
                            Denbun(d) = Denbun(d) & ","   ' PCS名 無し
                    End If
                Next i

                If 担当PCS番号.Contains("0") Then
                    Dim PCS名 As String = マスタデータ.J_新審判設定.PCS設定(10).PCS項目名
                    Denbun(d) = Denbun(d) & PCS名 & ","   ' PCS名 あり
                Else
                    Denbun(d) = Denbun(d) & ","   ' PCS名 無し
                End If

                '=============背番号、PCS番号、過去得点 ８個
                '過去得点を読み込む
                Dim 採点結果 As S_採点結果 = New S_採点結果(マスタデータ.Z_システム設定.Comp_filepath)
                採点結果.Read(区分番号, ラウンド番号, マスタデータ.D_種目マスタ.リスト(D_i).種目記号, ジャッジ記号）


                If マスタデータ.D_種目マスタ.リスト(D_i).SG種別 = "G" Then
                    '全員競技の時
                    For i = 1 To UBound(背番号リスト)
                        Denbun(d) = Denbun(d) & 背番号リスト(i) & ","   ' 背番号
                        Denbun(d) = Denbun(d) & 担当PCS番号 & ","   ' PCS番号
                        Denbun(d) = Denbun(d) & 採点結果.Get_PCS得点(背番号リスト(i), 担当PCS番号.Substring(1, 1)) & "," '過去得点　★★★★★★★★

                    Next i


                ElseIf Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号), 3) = "BJS" Then  ' 採点方式
                    'ブレイキンの時


                    If マスタデータ.D_種目マスタ.リスト(D_i).SG種別 = "S" Then
                        '8パネル分を送るが、ソロの時は④パネル分になってしまうので、残りをブランクで埋める必要がある

                        Dim PCS数 As Integer = 担当PCS番号.Length

                        For p = 1 To PCS数
                            For i = 1 To UBound(背番号リスト)
                                Denbun(d) = Denbun(d) & 背番号リスト(i) & ","   ' 背番号
                                Denbun(d) = Denbun(d) & 担当PCS番号.Substring(p - 1, 1) & ","   ' PCS番号
                                Denbun(d) = Denbun(d) & 採点結果.Get_PCS得点(背番号リスト(i), 担当PCS番号.Substring(p - 1, 1)) & "," '過去得点　★★★★★★★★

                                Denbun(d) = Denbun(d) & ","
                                Denbun(d) = Denbun(d) & ","
                                Denbun(d) = Denbun(d) & ","

                            Next i
                        Next p

                    Else

                        Dim PCS数 As Integer = 担当PCS番号.Length

                        For p = 1 To PCS数
                            For i = 1 To UBound(背番号リスト)
                                Denbun(d) = Denbun(d) & 背番号リスト(i) & ","   ' 背番号
                                Denbun(d) = Denbun(d) & 担当PCS番号.Substring(p - 1, 1) & ","   ' PCS番号
                                Denbun(d) = Denbun(d) & 採点結果.Get_PCS得点(背番号リスト(i), 担当PCS番号.Substring(p - 1, 1)) & "," '過去得点　★★★★★★★★
                            Next i
                        Next p

                    End If

                Else
                    'ソロ、マッチの時
                    Dim PCS数 As Integer = 担当PCS番号.Length

                    For i = 1 To UBound(背番号リスト)
                        For p = 1 To PCS数
                            Denbun(d) = Denbun(d) & 背番号リスト(i) & ","   ' 背番号
                            Denbun(d) = Denbun(d) & 担当PCS番号.Substring(p - 1, 1) & ","   ' PCS番号
                            Denbun(d) = Denbun(d) & 採点結果.Get_PCS得点(背番号リスト(i), 担当PCS番号.Substring(p - 1, 1)) & "," '過去得点　★★★★★★★★

                        Next p
                    Next i

                End If

            ElseIf マスタデータ.審判員マスタ.審判員リスト(JJ).審判チーム(担当審判グループ) = "R" Then
                'レフリー 減点ジャッジの時

                'タイマー情報のセット

                Denbun(d) = Denbun(d) & マスタデータ.Z_システム設定.Timer_D & ","
                For i = 1 To 9
                    If マスタデータ.Z_システム設定.Timer(i) <> "" Then
                        Denbun(d) = Denbun(d) & マスタデータ.Z_システム設定.Timer(i) & ","
                    Else
                        Denbun(d) = Denbun(d) & ","
                    End If
                Next i

                'タイマー情報ここまで

                マスタデータ.J_新審判設定.Set_新審判基準VER(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号))

                Dim 減点項目数 As Integer = 0
                Dim 減点電文 As String = ""
                For r = 1 To 20
                    If マスタデータ.J_新審判設定.減点設定(r) IsNot Nothing Then
                        If マスタデータ.J_新審判設定.減点設定(r).減点項目名 <> "" Then
                            減点項目数 = 減点項目数 + 1

                            減点電文 = 減点電文 & マスタデータ.J_新審判設定.減点設定(r).減点項目名 & ","
                            減点電文 = 減点電文 & マスタデータ.J_新審判設定.減点設定(r).最初の減点 & ","
                            減点電文 = 減点電文 & マスタデータ.J_新審判設定.減点設定(r).ステップ & ","
                            減点電文 = 減点電文 & マスタデータ.J_新審判設定.減点設定(r).最大値 & ","
                        End If
                    End If

                Next r

                Denbun(d) = Denbun(d) & 減点項目数 & ","
                Denbun(d) = Denbun(d) & 減点電文




                '=============背番号、減点
                '過去得点を読み込む
                Dim 採点結果 As S_採点結果 = New S_採点結果(マスタデータ.Z_システム設定.Comp_filepath)
                採点結果.Read(区分番号, ラウンド番号, マスタデータ.D_種目マスタ.リスト(D_i).種目記号, ジャッジ記号）

                For i = 1 To 出場選手数
                    Denbun(d) = Denbun(d) & 背番号リスト(i) & ","   ' 背番号

                    If 採点結果.Get_PCS減点(i, 1) = 0 Then
                        Dim 減点Temp As Double = 0
                        For r = 2 To 20
                            減点Temp = 減点Temp + 採点結果.Get_PCS減点(背番号リスト(i), r)
                        Next r
                        Denbun(d) = Denbun(d) & 減点Temp & ","
                    Else
                        Denbun(d) = Denbun(d) & "LOST,"

                    End If

                Next i

                '=============背番号、減点番号、過去得点 0点ではないもの全て

                For i = 1 To UBound(背番号リスト)
                    For r = 1 To 20
                        If 採点結果.Get_PCS減点(背番号リスト(i), r) <> 0 Then
                            Denbun(d) = Denbun(d) & 背番号リスト(i) & ","   ' 背番号
                            Denbun(d) = Denbun(d) & r & ","                   ' 減点番号
                            Denbun(d) = Denbun(d) & 採点結果.Get_PCS減点(背番号リスト(i), r) & ","   '過去得点

                        End If
                    Next r
                Next i


            End If


            'ブレイキンの時は、減点も渡す
            If Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号), 3) = "BJS" Then

                マスタデータ.J_新審判設定.Set_新審判基準VER(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号))

                Dim 減点項目数 As Integer = 0
                Dim 減点電文 As String = ""
                For r = 1 To 20
                    If マスタデータ.J_新審判設定.減点設定(r) IsNot Nothing Then
                        If マスタデータ.J_新審判設定.減点設定(r).減点項目名 <> "" Then
                            減点項目数 = 減点項目数 + 1

                            減点電文 = 減点電文 & マスタデータ.J_新審判設定.減点設定(r).減点項目名 & ","
                            減点電文 = 減点電文 & マスタデータ.J_新審判設定.減点設定(r).最初の減点 & ","
                            減点電文 = 減点電文 & マスタデータ.J_新審判設定.減点設定(r).ステップ & ","
                            減点電文 = 減点電文 & マスタデータ.J_新審判設定.減点設定(r).最大値 & ","
                        End If
                    End If

                Next r

                Denbun(d) = Denbun(d) & 減点項目数 & ","
                Denbun(d) = Denbun(d) & 減点電文




                '=============背番号、減点
                '過去得点を読み込む
                Dim 採点結果 As S_採点結果 = New S_採点結果(マスタデータ.Z_システム設定.Comp_filepath)
                採点結果.Read(区分番号, ラウンド番号, マスタデータ.D_種目マスタ.リスト(D_i).種目記号, ジャッジ記号）

                For i = 1 To 出場選手数
                    Denbun(d) = Denbun(d) & 背番号リスト(i) & ","   ' 背番号

                    If 採点結果.Get_PCS減点(i, 1) = 0 Then
                        Dim 減点Temp As Double = 0
                        For r = 2 To 20
                            減点Temp = 減点Temp + 採点結果.Get_PCS減点(背番号リスト(i), r)
                        Next r
                        Denbun(d) = Denbun(d) & 減点Temp & ","
                    Else
                        Denbun(d) = Denbun(d) & "LOST,"

                    End If

                Next i

                '=============背番号、減点番号、過去得点 0点ではないもの全て

                For i = 1 To UBound(背番号リスト)
                    For r = 1 To 20
                        If 採点結果.Get_PCS減点(背番号リスト(i), r) <> 0 Then
                            Denbun(d) = Denbun(d) & 背番号リスト(i) & ","   ' 背番号
                            Denbun(d) = Denbun(d) & r & ","                   ' 減点番号
                            Denbun(d) = Denbun(d) & 採点結果.Get_PCS減点(背番号リスト(i), r) & ","   '過去得点

                        End If
                    Next r
                Next i


            End If



            'ヘッダー文字列を作成
            Dim Hedder As String = ""
            Hedder = "JS,"
            Hedder = Hedder & "ANSHEAT,"
            Hedder = Hedder & "GM01,"
            全レコード件数 = 1   '新審判の時は必ず1件

            Hedder = Hedder & CStr(全レコード件数) & ","   ' 全レコード数

            For d = 1 To 全レコード件数
                Denbun(d) = Hedder & Denbun(d)
            Next d

        End If


        Return Denbun

    End Function

    '===ANSHEAT電文作成（順位法、チェック法、ブレイキンプレセレクション）
    Private Function ANSHEAT作成_JSON(ジャッジ記号 As String, 区分番号 As String, ラウンド番号 As String, 種目順 As String, 次ヒート番号 As Integer) As String

        Dim S_採点結果_J As S_採点結果_J
        S_採点結果_J = New S_採点結果_J(マスタデータ.Z_システム設定.Comp_filepath)

        Dim 次種目番号 As Integer
        次種目番号 = CInt(種目順)



        'データのセット
        S_採点結果_J.区分番号 = 区分番号
        S_採点結果_J.区分名 = マスタデータ.B_区分マスタ.Get区分表記名(区分番号)    ' 区分名
        S_採点結果_J.ラウンド番号 = ラウンド番号
        S_採点結果_J.ラウンド名 = マスタデータ.Get_ラウンド名(ラウンド番号)
        S_採点結果_J.採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        Dim JJ As Integer = マスタデータ.審判員マスタ.Get_レコード番号(ジャッジ記号)
        Dim D_i As Integer = マスタデータ.D_種目マスタ.Get_レコード番号(区分番号, ラウンド番号, String.Format("{0:D2}", 次種目番号))
        Dim 担当審判グループ As Integer = マスタデータ.D_種目マスタ.リスト(D_i).担当審判グループ

        S_採点結果_J.種目記号 = マスタデータ.D_種目マスタ.リスト(D_i).種目記号
        S_採点結果_J.種目名 = マスタデータ.Z_システム設定.Get_種目名称(マスタデータ.D_種目マスタ.リスト(D_i).種目記号).種目名_E
        S_採点結果_J.ジャッジ記号 = ジャッジ記号
        S_採点結果_J.ジャッジ名 = マスタデータ.審判員マスタ.審判員リスト(JJ).ジャッジ表記名
        'S_採点結果_J.SEND_FLAG = SEND_FLAG

        S_採点結果_J.SG種別 = マスタデータ.D_種目マスタ.リスト(D_i).SG種別

        'クライアントヒート表用データ
        S_採点結果_J.UP予定数 = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).UP予定数
        Dim 種目記号リスト() = Nothing
        S_採点結果_J.総種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        S_採点結果_J.種目記号リスト_1 = 種目記号リスト(1)
        If S_採点結果_J.総種目数 >= 2 Then
            S_採点結果_J.種目記号リスト_2 = 種目記号リスト(2)
        Else
            S_採点結果_J.種目記号リスト_2 = ""
        End If

        If S_採点結果_J.総種目数 >= 3 Then
            S_採点結果_J.種目記号リスト_3 = 種目記号リスト(3)
        Else
            S_採点結果_J.種目記号リスト_3 = ""
        End If

        If S_採点結果_J.総種目数 >= 4 Then
            S_採点結果_J.種目記号リスト_4 = 種目記号リスト(4)
        Else
            S_採点結果_J.種目記号リスト_4 = ""
        End If

        If S_採点結果_J.総種目数 >= 5 Then
            S_採点結果_J.種目記号リスト_5 = 種目記号リスト(5)
        Else
            S_採点結果_J.種目記号リスト_5 = ""
        End If






        Dim 背番号リスト(1) As String
        マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)


        Dim 出場選手数 As Integer



        出場選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(CStr(次種目番号), 次ヒート番号, 背番号リスト)   '次ヒート番号を 0 を指定すると全ﾋｰﾄ分を返す。順位法、チェック法は必ず0

        S_採点結果_J.ヒート番号 = 次ヒート番号
        S_採点結果_J.ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(CStr(次種目番号))

        Dim 選手マスタLIST番号 = マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ


        '選手結果の初期化
        S_採点結果_J.選手結果初期化(出場選手数)

        For s = 1 To 出場選手数
            S_採点結果_J.S_採点結果_選手_J(s).背番号 = 背番号リスト(s)
            S_採点結果_J.S_採点結果_選手_J(s).ヒート番号 = マスタデータ.E_ヒート表マスタ.Get_ヒート番号(次種目番号, 背番号リスト(s))

            'ブレイキン　プレセレクション用に選手名を表示する。

            Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号リスト(s))

            S_採点結果_J.S_採点結果_選手_J(s).選手名 = Strings.Left(選手.リーダー表記名, 6)

            選手 = Nothing

        Next s


        '=============背番号、減点
        '過去得点を読み込む
        Dim 採点結果 As S_採点結果_J = New S_採点結果_J(マスタデータ.Z_システム設定.Comp_filepath)
        採点結果.区分番号 = 区分番号
        採点結果.ラウンド番号 = ラウンド番号
        採点結果.種目記号 = マスタデータ.D_種目マスタ.リスト(D_i).種目記号
        採点結果.ジャッジ記号 = ジャッジ記号
        採点結果 = 採点結果.新JSON読み込み(）



        If 採点結果 IsNot Nothing Then
            'ジャッジの結果が一度でもSendされているとき

            For s = 1 To UBound(S_採点結果_J.S_採点結果_選手_J)

                For o = 1 To UBound(採点結果.S_採点結果_選手_J)
                    If S_採点結果_J.S_採点結果_選手_J(s).背番号 = 採点結果.S_採点結果_選手_J(o).背番号 Then

                        S_採点結果_J.S_採点結果_選手_J(s).点数 = 採点結果.S_採点結果_選手_J(o).点数
                        S_採点結果_J.S_採点結果_選手_J(s).備考 = 採点結果.S_採点結果_選手_J(o).備考

                        o = UBound(採点結果.S_採点結果_選手_J)
                    End If
                Next o
            Next s

        Else
            'まだジャッジのSENDが一度も行われていない時
            For s = 1 To 出場選手数

                S_採点結果_J.S_採点結果_選手_J(s).点数 = 0
                S_採点結果_J.S_採点結果_選手_J(s).備考 = ""

            Next s

            採点結果 = Nothing

        End If




        Dim rc As String = S_採点結果_J.Get_JSON文字列

        Return rc

    End Function


    Private Function ANSHEAT作成_V2_JSON(ジャッジ記号 As String, 区分番号 As String, ラウンド番号 As String, 種目順 As String, 次ヒート番号 As Integer) As String

        Dim S_採点結果_J As S_採点結果_V2_J
        S_採点結果_J = New S_採点結果_V2_J(マスタデータ.Z_システム設定.Comp_filepath)

        S_採点結果_J.Set_LOG(LOG)

        S_採点結果_J.初期化()

        'マスタデータの再読み込み
        'キャリブレーションが途中でセットされた場合等
        マスタデータ.D_種目マスタ.FileRead()



        Dim 次種目番号 As Integer
        次種目番号 = CInt(種目順)



        'データのセット
        S_採点結果_J.区分番号 = 区分番号
        S_採点結果_J.区分名 = マスタデータ.B_区分マスタ.Get区分表記名(区分番号)    ' 区分名
        S_採点結果_J.ラウンド番号 = ラウンド番号
        S_採点結果_J.ラウンド名 = マスタデータ.Get_ラウンド名(ラウンド番号)
        S_採点結果_J.採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        Dim JJ As Integer = マスタデータ.審判員マスタ.Get_レコード番号(ジャッジ記号)
        Dim D_i As Integer = マスタデータ.D_種目マスタ.Get_レコード番号(区分番号, ラウンド番号, String.Format("{0:D2}", 次種目番号))
        Dim 担当審判グループ As Integer = マスタデータ.D_種目マスタ.リスト(D_i).担当審判グループ

        S_採点結果_J.現種目記号 = マスタデータ.D_種目マスタ.リスト(D_i).種目記号
        S_採点結果_J.種目番号 = 次種目番号
        S_採点結果_J.現種目名 = マスタデータ.Z_システム設定.Get_種目名称(マスタデータ.D_種目マスタ.リスト(D_i).種目記号).種目名_E
        S_採点結果_J.ジャッジ記号 = ジャッジ記号
        S_採点結果_J.ジャッジ名 = マスタデータ.審判員マスタ.審判員リスト(JJ).ジャッジ表記名
        'S_採点結果_J.SEND_FLAG = SEND_FLAG

        If マスタデータ.審判員マスタ.審判員リスト(JJ).審判チーム(担当審判グループ) = "1" Then
            S_採点結果_J.ジャッジタイプ = "J"
        Else
            S_採点結果_J.ジャッジタイプ = マスタデータ.審判員マスタ.審判員リスト(JJ).審判チーム(担当審判グループ)
        End If


        '技術判定員はソロしか担当しないので、最初のソロ競技を D_i にセットする

        Dim ソロ種目記号 As String = ""  'フィガー検索用

        If マスタデータ.D_種目マスタ.リスト(D_i).SG種別 = "S" Then
            ソロ種目記号 = マスタデータ.D_種目マスタ.リスト(D_i).種目記号
        Else

        End If



        If S_採点結果_J.ジャッジタイプ = "T" Then

            Dim Find_Flag As Boolean = False  '次のソロ競技があるか？

            For d = D_i To UBound(マスタデータ.D_種目マスタ.リスト)
                If マスタデータ.D_種目マスタ.リスト(d) IsNot Nothing Then

                    If マスタデータ.D_種目マスタ.リスト(d).区分番号 = 区分番号 And
                       マスタデータ.D_種目マスタ.リスト(d).ラウンド番号 = ラウンド番号 And
                       マスタデータ.D_種目マスタ.リスト(d).SG種別 = "S" Then

                        D_i = d
                        次種目番号 = マスタデータ.D_種目マスタ.リスト(d).種目順
                        S_採点結果_J.現種目記号 = マスタデータ.D_種目マスタ.リスト(D_i).種目記号
                        S_採点結果_J.現種目名 = マスタデータ.Z_システム設定.Get_種目名称(マスタデータ.D_種目マスタ.リスト(D_i).種目記号).種目名_E

                        d = UBound(マスタデータ.D_種目マスタ.リスト)

                        Find_Flag = True
                    End If
                End If
            Next d

            If Find_Flag = False Then
                'ソロ競技が終わった時

                Return Nothing

                Exit Function

            End If


        End If



        S_採点結果_J.SG種別 = マスタデータ.D_種目マスタ.リスト(D_i).SG種別

        'クライアントヒート表用データ
        S_採点結果_J.UP予定数 = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).UP予定数

        Dim 種目記号リスト() = Nothing
        S_採点結果_J.総種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        If S_採点結果_J.総種目数 > 0 Then
            ReDim S_採点結果_J.種目記号_J(S_採点結果_J.総種目数)

            For d = 1 To S_採点結果_J.総種目数
                S_採点結果_J.種目記号_J(d) = New S_採点結果_V2_J.S_採点結果_V2_種目記号_J
                S_採点結果_J.種目記号_J(d).種目番号 = d
                S_採点結果_J.種目記号_J(d).種目記号 = 種目記号リスト(d)
            Next d
        End If


        マスタデータ.J_新審判設定.Set_新審判基準VER(S_採点結果_J.採点方式)

        'PCS設定

        Dim PCS数 As Integer = マスタデータ.J_新審判設定.GetPCS数

        If S_採点結果_J.ジャッジタイプ = "J" Or S_採点結果_J.ジャッジタイプ = "S" Then
            If PCS数 > 0 Then
                ReDim S_採点結果_J.PCS設定_J(PCS数)
                S_採点結果_J.PCS数 = PCS数

                For p = 1 To PCS数
                    S_採点結果_J.PCS設定_J(p) = New S_採点結果_V2_J.S_採点結果_V2_PCS設定_J


                    S_採点結果_J.PCS設定_J(p).PCS番号 = p
                    S_採点結果_J.PCS設定_J(p).PCS略称 = マスタデータ.J_新審判設定.PCS設定(p).PCS項目名
                Next p

                'キャリブレーションの設定
                S_採点結果_J.Cali_MAX = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目順).CaliMax
                S_採点結果_J.Cali_MIN = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目順).CaliMin

            End If
        End If


        '減点設定

        Dim 減点項目数 As Integer = マスタデータ.J_新審判設定.Get減点項目数

        If S_採点結果_J.ジャッジタイプ = "R" Then
            If 減点項目数 > 0 Then
                Dim 減点NO As Integer = 0

                ReDim S_採点結果_J.減点設定_J(減点項目数)

                S_採点結果_J.減点項目数 = 減点項目数


                For r = 1 To 減点項目数

                    If Strings.InStr(マスタデータ.J_新審判設定.減点設定(r).SGM種別, S_採点結果_J.SG種別) > 0 Then
                        減点NO = 減点NO + 1

                        S_採点結果_J.減点設定_J(減点NO) = New S_採点結果_V2_J.S_採点結果_V2_減点設定_J

                        S_採点結果_J.減点設定_J(減点NO).減点番号 = r
                        S_採点結果_J.減点設定_J(減点NO).減点略称 = マスタデータ.J_新審判設定.減点設定(r).減点項目名
                        S_採点結果_J.減点設定_J(減点NO).初期値 = マスタデータ.J_新審判設定.減点設定(r).最初の減点
                        S_採点結果_J.減点設定_J(減点NO).STEP値 = マスタデータ.J_新審判設定.減点設定(r).ステップ
                        S_採点結果_J.減点設定_J(減点NO).MAX値 = マスタデータ.J_新審判設定.減点設定(r).最大値
                    End If
                Next r
            End If
        End If


        '課題設定

        '    ソロの種目記号は？


        Dim 課題数 As Integer = マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)

        If S_採点結果_J.SG種別 = "S" Then
            If S_採点結果_J.ジャッジタイプ = "J" Or S_採点結果_J.ジャッジタイプ = "T" Then

                If 課題数 > 0 Then

                    ReDim S_採点結果_J.課題設定_J(課題数)
                    S_採点結果_J.課題数 = 課題数

                    For k = 1 To 課題数

                        S_採点結果_J.課題設定_J(k) = New S_採点結果_V2_J.S_採点結果_V2_課題設定_J

                        S_採点結果_J.課題設定_J(k).課題番号 = k
                        'S_採点結果_J.課題設定_J(k).フィガー名 = マスタデータ.J_新審判設定.課題設定(k)
                        S_採点結果_J.課題設定_J(k).フィガー名 = マスタデータ.J_新審判設定.Getフィガー名(ソロ種目記号, k)
                    Next k

                End If

            End If
        End If



        'TES設定　Base点
        If S_採点結果_J.SG種別 = "S" Then
            If S_採点結果_J.ジャッジタイプ = "T" Then    'ジャッジ画面でも、Base点を表示する必要があるため
                If 課題数 > 0 Then


                    ReDim S_採点結果_J.TES設定_J(課題数)
                    For k = 1 To 課題数

                        S_採点結果_J.TES設定_J(k) = New S_採点結果_V2_J.S_採点結果_V2_TES設定_J
                        S_採点結果_J.TES設定_J(k).Base点 = マスタデータ.J_新審判設定.TES設定(k)
                    Next k

                End If
            End If
        End If

        'TES減点設定
        Dim TES減点数 As Integer = マスタデータ.J_新審判設定.GetTES減点数

        If S_採点結果_J.SG種別 = "S" Then
            If S_採点結果_J.ジャッジタイプ = "T" Then
                If TES減点数 > 0 Then

                    ReDim S_採点結果_J.TES減点_J(TES減点数)
                    S_採点結果_J.TES減点数 = TES減点数

                    For tr = 1 To TES減点数
                        S_採点結果_J.TES減点_J(tr) = New S_採点結果_V2_J.S_採点結果_V2_TES減点_J

                        S_採点結果_J.TES減点_J(tr).TES減点番号 = tr
                        S_採点結果_J.TES減点_J(tr).TES減点略称 = マスタデータ.J_新審判設定.TES減点設定(tr).減点略称
                        S_採点結果_J.TES減点_J(tr).初期値 = マスタデータ.J_新審判設定.TES減点設定(tr).初期値
                        S_採点結果_J.TES減点_J(tr).STEP値 = マスタデータ.J_新審判設定.TES減点設定(tr).STEP値
                        S_採点結果_J.TES減点_J(tr).MAX値 = マスタデータ.J_新審判設定.TES減点設定(tr).MAX値

                    Next tr


                End If
            End If
        End If

        'タイマー設定  レフリーの時だけ
        If S_採点結果_J.ジャッジタイプ = "R" Then

            S_採点結果_J.タイマー種別 = マスタデータ.Z_システム設定.Timer_D

            For i = 1 To 9
                If マスタデータ.Z_システム設定.Timer(i) = "" Then
                    S_採点結果_J.タイマー設定数 = i - 1
                End If
            Next i

            ReDim S_採点結果_J.タイマー設定(S_採点結果_J.タイマー設定数)

            For i = 1 To S_採点結果_J.タイマー設定数
                If マスタデータ.Z_システム設定.Timer(i) <> "" Then

                    S_採点結果_J.タイマー設定(i) = New S_採点結果_V2_J.S_採点結果_V2_タイマー設定_J

                    S_採点結果_J.タイマー設定(i).タイマー時間 = マスタデータ.Z_システム設定.Timer(i)

                End If
            Next i

        End If


        '選手データの作成

        Dim 背番号リスト(1) As String
        マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)

        Dim 出場選手数 As Integer
        出場選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(CStr(次種目番号), 0, 背番号リスト)   '次ヒート番号を 0 を指定すると全ﾋｰﾄ分を返す。順位法、チェック法は必ず0

        S_採点結果_J.選手数 = 出場選手数

        S_採点結果_J.現ヒート番号 = 次ヒート番号
        S_採点結果_J.ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(CStr(次種目番号))

        Dim 選手マスタLIST番号 = マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ



        'ジャッジ PCS担当     'PCSは、曲毎ではなく、種目毎に変わる
        '=============PCS名　1から10
        If Strings.Left(S_採点結果_J.採点方式, 3) = "PDJ" Or Strings.Left(S_採点結果_J.採点方式, 3) = "VAL" Then

            If S_採点結果_J.ジャッジタイプ = "J" Or S_採点結果_J.ジャッジタイプ = "S" Then

                マスタデータ.F_審判担当PCSマスタ.Read(区分番号, ラウンド番号)

                Dim 審判PCS As F_審判担当PCS = マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(ジャッジ記号)

                Dim 担当PCS番号 As String = 審判PCS.担当PCS番号(次種目番号)

                If 審判PCS Is Nothing Then
                    MsgBox("ジャッジ「" & ジャッジ記号 & "」の担当PCSが設定されていません。")

                    Return ""
                    Exit Function
                End If

                ReDim S_採点結果_J.担当PCS_J(S_採点結果_J.ヒート数)


                For h = 1 To S_採点結果_J.ヒート数

                    S_採点結果_J.担当PCS_J(h) = New S_採点結果_V2_J.S_採点結果_V2_HT担当PCS_J(S_採点結果_J.PCS数)

                    For p = 1 To S_採点結果_J.PCS数

                        If InStr(担当PCS番号, CStr(p)) Then
                            S_採点結果_J.担当PCS_J(h).担当PCS(p).担当 = True
                        Else
                            S_採点結果_J.担当PCS_J(h).担当PCS(p).担当 = False
                        End If
                    Next p

                Next h

            End If

        End If




        '選手結果の初期化
        S_採点結果_J.選手結果初期化(出場選手数)

        For s = 1 To 出場選手数
            S_採点結果_J.S_採点結果_選手_J(s).背番号 = 背番号リスト(s)
            S_採点結果_J.S_採点結果_選手_J(s).ヒート番号 = マスタデータ.E_ヒート表マスタ.Get_ヒート番号(次種目番号, 背番号リスト(s))

            'ブレイキン　プレセレクション用に選手名を表示する。
            Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号リスト(s))
            S_採点結果_J.S_採点結果_選手_J(s).選手名 = Strings.Left(選手.リーダー表記名, 6)
            選手 = Nothing


        Next s


        '過去得点を読み込む
        Dim 採点結果 As S_採点結果_V2_J = New S_採点結果_V2_J(マスタデータ.Z_システム設定.Comp_filepath)

        採点結果.Set_LOG(LOG)

        採点結果.区分番号 = 区分番号
        採点結果.ラウンド番号 = ラウンド番号
        採点結果.現種目記号 = マスタデータ.D_種目マスタ.リスト(D_i).種目記号
        採点結果.ジャッジ記号 = ジャッジ記号
        採点結果 = 採点結果.JSON読み込み(）

        If 採点結果 IsNot Nothing Then
            'ジャッジの結果が一度でもSendされているとき

            For s = 1 To UBound(S_採点結果_J.S_採点結果_選手_J)

                For o = 1 To UBound(採点結果.S_採点結果_選手_J)
                    If S_採点結果_J.S_採点結果_選手_J(s).背番号 = 採点結果.S_採点結果_選手_J(o).背番号 Then

                        S_採点結果_J.S_採点結果_選手_J(s).SEND_FLAG = 採点結果.S_採点結果_選手_J(o).SEND_FLAG
                        S_採点結果_J.S_採点結果_選手_J(s).点数 = 採点結果.S_採点結果_選手_J(o).点数
                        S_採点結果_J.S_採点結果_選手_J(s).備考 = 採点結果.S_採点結果_選手_J(o).備考

                        'PCS得点
                        If S_採点結果_J.PCS数 > 0 Then
                            For p = 1 To S_採点結果_J.PCS数
                                If 採点結果.S_採点結果_選手_J(o).PCS得点_J(p) IsNot Nothing Then

                                    S_採点結果_J.S_採点結果_選手_J(s).PCS得点_J(p).PCS素点 = 採点結果.S_採点結果_選手_J(o).PCS得点_J(p).PCS素点

                                End If
                            Next p
                        End If

                        'GOE
                        If S_採点結果_J.課題数 > 0 Then
                            For k = 1 To S_採点結果_J.課題数
                                If 採点結果.S_採点結果_選手_J(o).GOE_J IsNot Nothing Then
                                    If 採点結果.S_採点結果_選手_J(o).GOE_J(k) IsNot Nothing Then
                                        S_採点結果_J.S_採点結果_選手_J(s).GOE_J(k).GOE = 採点結果.S_採点結果_選手_J(o).GOE_J(k).GOE
                                    End If
                                End If
                            Next k
                        End If

                        '一般減点
                        If S_採点結果_J.減点項目数 > 0 Then
                            For r = 1 To S_採点結果_J.減点項目数
                                If 採点結果.S_採点結果_選手_J(o).減点_J(r) IsNot Nothing Then
                                    S_採点結果_J.S_採点結果_選手_J(s).減点_J(r).減点 = 採点結果.S_採点結果_選手_J(o).減点_J(r).減点
                                End If
                            Next r
                        End If

                        'TES Base　とTES減点
                        If S_採点結果_J.ジャッジタイプ = "T" Then
                            If S_採点結果_J.課題数 > 0 Then
                                For k = 1 To S_採点結果_J.課題数
                                    If 採点結果.S_採点結果_選手_J(o).TES_J(k) IsNot Nothing Then

                                        S_採点結果_J.S_採点結果_選手_J(s).TES_J(k).Base = 採点結果.S_採点結果_選手_J(o).TES_J(k).Base

                                        If S_採点結果_J.TES減点数 > 0 Then
                                            For tr = 1 To S_採点結果_J.TES減点数

                                                If 採点結果.S_採点結果_選手_J(o).TES_J(k).TES減点_J(tr) IsNot Nothing Then

                                                    S_採点結果_J.S_採点結果_選手_J(s).TES_J(k).TES減点_J(tr).TES減点 = 採点結果.S_採点結果_選手_J(o).TES_J(k).TES減点_J(tr).TES減点
                                                End If

                                            Next tr
                                        End If
                                    End If

                                Next k
                            End If
                        End If


                        o = UBound(採点結果.S_採点結果_選手_J)
                    End If
                Next o
            Next s

            '最終更新日をセット

            S_採点結果_J.lastUpdate = 採点結果.lastUpdate



        Else
                'まだジャッジのSENDが一度も行われていない時

                'Baseだけはデフォルトの値をセットする。あとは0点で良い。
                If S_採点結果_J.ジャッジタイプ = "T" Then
                For s = 1 To UBound(S_採点結果_J.S_採点結果_選手_J)
                    For k = 1 To S_採点結果_J.課題数

                        S_採点結果_J.S_採点結果_選手_J(s).TES_J(k).Base = S_採点結果_J.TES設定_J(k).Base点
                    Next k

                Next s
            End If

            '現在の時刻をlastupdateにセット
            ' 例: 2025-12-10 17:58:07.734
            'S_採点結果_J.lastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")

        End If



            Dim rc As String = S_採点結果_J.Get_JSON文字列

        Return rc

    End Function



    '===ANSHEAT電文作成（BRJ2）
    Private Function ANSHEAT作成_BR2_JSON(ジャッジ記号 As String, 区分番号 As String, ラウンド番号 As String, 種目順 As String, 次ヒート番号 As Integer) As String

        Dim S_採点結果_J As S_採点結果_BR2_J
        S_採点結果_J = New S_採点結果_BR2_J(マスタデータ.Z_システム設定.Comp_filepath)

        マスタデータ.J_新審判設定.Set_新審判基準VER(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号))



        Dim 次種目番号 As Integer
        Dim 次種目番号_2 As Integer = 0

        Dim 種目記号リスト() = Nothing

        'If 種目順 = 2 And マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト) > 2 Then
        '次種目番号 = 3
        'ElseIf 種目順 = 2 And マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト) = 2 Then
        'Return ""
        'Else
        '次種目番号 = CInt(種目順)
        'End If

        Dim 種目数 As Integer = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        'Ver1.02.20  2022/1/19修正　で、種目順には ジャッジが採点した最後のラウンド（種目）が入っている
        '次種目番号　には、次のラウンド番号が入る。
        '次種目番号_2  には、2ラウンド目のラウンド番号が入る。 0 の時は1ラウンドだけという意味。


        'Ver1.02.20  2022/1/19修正で以下はコメントアウト

        ' If CInt(種目順) <= マスタデータ.J_新審判設定.勝敗ラウンド数 Then　　　'2ラウンドづつ実施する前提　勝敗ラウンド数に達していない時は
        'If CInt(種目順) Mod 2 = 1 Then
        '次種目番号 = CInt(種目順)
        'Else
        '次種目番号 = CInt(種目順) + 1
        'If 次種目番号 > 種目数 Then
        'Return ""
        'End If
        'End If
        'Else
        '次種目番号 = CInt(種目順)
        'End If

        Dim SG種別 As String = ""

        If CInt(種目順) = 0 Then
            SG種別 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 1).SG種別
        Else
            SG種別 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目順).SG種別
        End If

        S_採点結果_J.SG種別 = SG種別


        If SG種別 = "S" Then
            'ソロの時は次ヒートを返す

            '◆◆次ヒート番号は次のヒート番号が入っているが、区分、ラウンド、種目順は現在の種目順が入っているという理解

            If 次ヒート番号 = 0 Then
                '最終ヒートが終わった時は次種目番号を返す。

                If CInt(種目順) + 1 > 種目数 Then
                    '最終種目だったらブランクを返す
                    Return ""
                Else
                    次種目番号 = CInt(種目順) + 1

                End If
            Else
                If CInt(種目順） = 0 Then

                    次種目番号 = 1

                Else

                    次種目番号 = CInt(種目順)


                End If


            End If

            '次の種目（ラウンド）がある場合

        Else
            If CInt(種目順) + 1 > 種目数 Then
                '最終種目だった場合は、ブランクを返す。
                Return ""
            Else
                '次の種目（ラウンド）がある場合
                次種目番号 = CInt(種目順) + 1
            End If


        End If





        If 次種目番号 + 1 <= 種目数 Then
            If 次種目番号 = マスタデータ.J_新審判設定.勝敗ラウンド数 Then
                '勝敗ラウンド数と一致しているときは

                次種目番号_2 = 0
            Else
                次種目番号_2 = 次種目番号 + 1
            End If
        Else
            次種目番号_2 = 0
        End If



        'Ver1.02.20  2022/1/19修正 ここまで


        ReDim S_採点結果_J.BR2_PCS設定_J(マスタデータ.J_新審判設定.GetPCS数)

        'PCSの情報設定
        For p = 1 To マスタデータ.J_新審判設定.GetPCS数
            S_採点結果_J.BR2_PCS設定_J(p) = New S_採点結果_BR2_PCS_J()

            S_採点結果_J.BR2_PCS設定_J(p).PCS番号 = p
            S_採点結果_J.BR2_PCS設定_J(p).PCS略称 = マスタデータ.J_新審判設定.PCS設定(p).PCS項目名
            'S_採点結果_J.BR2_PCS設定_J(p).PCS名称 = マスタデータ.J_新審判設定.PCS設定(p).PCS説明1
            S_採点結果_J.BR2_PCS設定_J(p).PCS最大点 = マスタデータ.J_新審判設定.PCS設定(p).PCS最大値
            'S_採点結果_J.BR2_PCS設定_J(p).PCS点数単位 = 1
        Next p

        S_採点結果_J.PCS数 = マスタデータ.J_新審判設定.GetPCS数

        '減点の情報設定

        ReDim S_採点結果_J.BR2_減点設定_J(マスタデータ.J_新審判設定.Get減点項目数)

        For r = 1 To マスタデータ.J_新審判設定.Get減点項目数
            S_採点結果_J.BR2_減点設定_J(r) = New S_採点結果_BR2_減点_J(マスタデータ.J_新審判設定.Get減点項目数)

            S_採点結果_J.BR2_減点設定_J(r).減点番号 = r
            S_採点結果_J.BR2_減点設定_J(r).減点略称 = マスタデータ.J_新審判設定.減点設定(r).減点項目名

            Dim 減点値数 As Integer = Math.Ceiling(マスタデータ.J_新審判設定.減点設定(r).最大値 / マスタデータ.J_新審判設定.減点設定(r).ステップ)

            S_採点結果_J.BR2_減点設定_J(r).減点値数 = 減点値数

            S_採点結果_J.BR2_減点設定_J(r).減点値リスト(0) = 0
            S_採点結果_J.BR2_減点設定_J(r).減点値リスト(1) = マスタデータ.J_新審判設定.減点設定(r).最初の減点

            For j = 2 To 減点値数

                Dim 減点 As Decimal = マスタデータ.J_新審判設定.減点設定(r).最初の減点 + マスタデータ.J_新審判設定.減点設定(r).ステップ * (j - 1)

                If 減点 < 0 Then
                    '減点の時
                    If 減点 < マスタデータ.J_新審判設定.減点設定(r).最大値 Then
                        減点 = マスタデータ.J_新審判設定.減点設定(r).最大値
                    End If
                Else
                    '加点の時
                    If 減点 > マスタデータ.J_新審判設定.減点設定(r).最大値 Then
                        減点 = マスタデータ.J_新審判設定.減点設定(r).最大値
                    End If
                End If


                S_採点結果_J.BR2_減点設定_J(r).減点値リスト(j) = 減点

            Next j

        Next r
        S_採点結果_J.減点項目数 = マスタデータ.J_新審判設定.Get減点項目数()


        'データのセット
        S_採点結果_J.区分番号 = 区分番号
        S_採点結果_J.区分名 = マスタデータ.B_区分マスタ.Get区分表記名(区分番号)    ' 区分名
        S_採点結果_J.ラウンド番号 = ラウンド番号
        S_採点結果_J.ラウンド名 = マスタデータ.Get_ラウンド名(ラウンド番号)
        S_採点結果_J.採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        Dim JJ As Integer = マスタデータ.審判員マスタ.Get_レコード番号(ジャッジ記号)
        Dim D_i As Integer = マスタデータ.D_種目マスタ.Get_レコード番号(区分番号, ラウンド番号, String.Format("{0:D2}", 次種目番号))
        If D_i = 2 Then
            ' D_i = 3
            MsgBox("D_i=2 のケースが発生しました。")
        End If

        Dim 担当審判グループ As Integer = マスタデータ.D_種目マスタ.リスト(D_i).担当審判グループ

        S_採点結果_J.種目記号 = マスタデータ.D_種目マスタ.リスト(D_i).種目記号

        S_採点結果_J.種目番号 = CInt(次種目番号）

        'Ver1.02.20  2022/1/19修正
        'Dim D_i_Next As Integer = マスタデータ.D_種目マスタ.Get_レコード番号(区分番号, ラウンド番号, String.Format("{0:D2}", 次種目番号 + 1))
        Dim D_i_Next As Integer = マスタデータ.D_種目マスタ.Get_レコード番号(区分番号, ラウンド番号, String.Format("{0:D2}", 次種目番号_2))

        'Ver1.02.20  2022/1/19修正
        'If CInt(種目順) + 1 <= マスタデータ.J_新審判設定.勝敗ラウンド数 And D_i_Next <> 0 Then  
        If 次種目番号_2 <> 0 And D_i_Next <> 0 Then
            S_採点結果_J.種目記号_2 = マスタデータ.D_種目マスタ.リスト(D_i_Next).種目記号
        End If

        S_採点結果_J.種目名 = マスタデータ.Z_システム設定.Get_種目名称(マスタデータ.D_種目マスタ.リスト(D_i).種目記号).種目名_E
        S_採点結果_J.ジャッジ記号 = ジャッジ記号
        S_採点結果_J.ジャッジ名 = マスタデータ.審判員マスタ.審判員リスト(JJ).ジャッジ表記名
        'S_採点結果_J.SEND_FLAG = SEND_FLAG


        Dim 出場選手数 As Integer = 0

        If マスタデータ.D_種目マスタ.リスト(D_i).SG種別 = "S" Then
            'ソロの時
            Dim 背番号リスト(1) As String
            マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
            出場選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(CStr(次種目番号), 0, 背番号リスト)   'ヒート番号を 0 を指定すると全ﾋｰﾄ分を返す。

            S_採点結果_J.選手数 = 出場選手数

            '選手結果の初期化
            S_採点結果_J.選手結果初期化(出場選手数)

            For s = 1 To 出場選手数
                S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).背番号 = 背番号リスト(s)
                S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).ヒート番号 = マスタデータ.E_ヒート表マスタ.Get_ヒート番号(CStr(次種目番号), 背番号リスト(s))
                S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).SEND_FLAG = "0"

            Next s

            'ソロの時は全員分を返すヒートは出場者は1人しかいない前提
            'S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(1).背番号 = 背番号リスト(1)


            S_採点結果_J.ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(CStr(次種目番号))
            S_採点結果_J.ヒート番号 = 次ヒート番号        '次に採点するヒート番号

        Else
            'ソロ以外の時


            Dim 背番号リスト(1) As String
            マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
            出場選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(CStr(次種目番号), 0, 背番号リスト)   'ヒート番号を 0 を指定すると全ﾋｰﾄ分を返す。

            S_採点結果_J.選手数 = 出場選手数

            '選手結果の初期化
            S_採点結果_J.選手結果初期化(出場選手数)


            '2人しかいない前提、もし2人とも第１ヒートだったら、番号順にする。
            Dim h As Integer = マスタデータ.E_ヒート表マスタ.Get_ヒート番号(1, 背番号リスト(1))   '1種目目のヒート番号を使う。

            If h = 2 Then
                '逆パターン　青ー赤
                S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(1).背番号 = 背番号リスト(2)
                S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(2).背番号 = 背番号リスト(1)
            Else
                '正常　赤―青パターン
                For s = 1 To 2
                    S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).背番号 = 背番号リスト(s)
                Next s
            End If

            '2ラウンド同時採点の時

            'Ver1.02.20  2022/1/19修正
            'If 次種目番号 + 1 <= マスタデータ.J_新審判設定.勝敗ラウンド数 Then
            If 次種目番号_2 <> 0 Then
                If h = 2 Then
                    '逆パターン　青ー赤
                    S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(1).背番号 = 背番号リスト(2)
                    S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(2).背番号 = 背番号リスト(1)
                Else
                    '正常　赤―青パターン
                    For s = 1 To 2
                        S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(s).背番号 = 背番号リスト(s)
                    Next s
                End If
            End If


            'If S_採点結果_J.種目番号 = 1 Then
            'For s = 1 To 2
            'S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(s).背番号 = 背番号リスト(s)
            'Next s
            'End If

        End If


        '=============背番号、減点
        '過去得点を読み込む
        Dim 採点結果 As S_採点結果_BR2_J = New S_採点結果_BR2_J(マスタデータ.Z_システム設定.Comp_filepath)
        採点結果.区分番号 = 区分番号
        採点結果.ラウンド番号 = ラウンド番号
        採点結果.種目記号 = マスタデータ.D_種目マスタ.リスト(D_i).種目記号
        採点結果.ジャッジ記号 = ジャッジ記号


        If 採点結果.JSON読み込み(） > 0 Then

            For s = 1 To 出場選手数

                Dim 選手結果 As S_採点結果_BR2_選手_J = Nothing
                '選手結果 = 採点結果.Get_選手結果(背番号リスト(s), 1)
                選手結果 = 採点結果.Get_選手結果(S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).背番号, 1)


                For p = 1 To S_採点結果_J.PCS数
                    If 選手結果 IsNot Nothing Then
                        S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).PCS得点(p).PCS素点 = 選手結果.PCS得点(p).PCS素点
                    Else
                        S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).PCS得点(p).PCS素点 = 0
                    End If
                Next p

                For r = 1 To S_採点結果_J.減点項目数
                    If 選手結果 IsNot Nothing Then
                        S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).減点(r).減点 = 選手結果.減点(r).減点
                    Else
                        S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).減点(r).減点 = 0
                    End If

                Next r

                If SG種別 = "S" Then  'ソロ以外の時も返して良いのかもしれないが、影響が不明なため返さないようにする。
                    If 選手結果 IsNot Nothing Then
                        S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).ヒート番号 = 選手結果.ヒート番号
                        S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).SEND_FLAG = 選手結果.SEND_FLAG
                    Else

                        S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).ヒート番号 = マスタデータ.E_ヒート表マスタ.Get_ヒート番号(S_採点結果_J.種目番号, S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).背番号)
                        S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).SEND_FLAG = "0"
                    End If
                End If



                S_採点結果_J.BR2_種目結果_J(1).種目記号 = マスタデータ.D_種目マスタ.リスト(D_i).種目記号

                'If 種目順 = 1 Then
                'Ver1.02.20  2022/1/19修正
                'If 次種目番号 + 1 <= マスタデータ.J_新審判設定.勝敗ラウンド数 Then
                If 次種目番号_2 <> 0 Then

                    If S_採点結果_J.BR2_種目結果_J(2).種目記号 Is Nothing Then
                        MsgBox("勝敗ラウンド数が、" & マスタデータ.J_新審判設定.勝敗ラウンド数 & "と設定されていますが、種目番号:" & 次種目番号 + 1 & "が定義されていません。”)

                    Else


                        S_採点結果_J.BR2_種目結果_J(2).種目記号 = マスタデータ.D_種目マスタ.リスト(D_i_Next).種目記号

                        '1ラウンドの時は2ラウンドの結果も入れる
                        選手結果 = 採点結果.Get_選手結果(S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).背番号, 2)

                        For p = 1 To S_採点結果_J.PCS数
                            If 選手結果 IsNot Nothing Then
                                S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(s).PCS得点(p).PCS素点 = 選手結果.PCS得点(p).PCS素点
                            Else
                                S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(s).PCS得点(p).PCS素点 = 0
                            End If
                        Next p

                        For r = 1 To S_採点結果_J.減点項目数
                            If 選手結果 IsNot Nothing Then
                                S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(s).減点(r).減点 = 選手結果.減点(r).減点
                            Else
                                S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(s).減点(r).減点 = 0
                            End If
                        Next r


                        If SG種別 = "S" Then  'ソロ以外の時も返して良いのかもしれないが、影響が不明なため返さないようにする。
                            'ソロの時は、2種目同時の採点は無いはずなので、ここは通らないソース
                            MsgBox("ソロ競技では2ラウンド同時の採点は実施できません。")

                            If 選手結果 IsNot Nothing Then
                                S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(s).ヒート番号 = 選手結果.ヒート番号
                                S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(s).SEND_FLAG = 選手結果.SEND_FLAG
                            Else
                                S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(s).ヒート番号 = マスタデータ.E_ヒート表マスタ.Get_ヒート番号(S_採点結果_J.種目番号 + 1, S_採点結果_J.BR2_種目結果_J(1).BR2_選手結果_J(s).背番号)

                                S_採点結果_J.BR2_種目結果_J(2).BR2_選手結果_J(s).SEND_FLAG = "0"
                            End If

                        End If
                    End If

                End If

            Next s
        End If


        採点結果 = Nothing

        Dim rc As String = S_採点結果_J.Get_JSON文字列

        Return rc

    End Function


    '==Send TIMSTART
    Public Function SEND_TIMSTART(区分番号 As String, ラウンド番号 As String, 種目順 As String, ヒート番号 As Integer, タイマカテゴリ As String, カウントダウン時間 As String) As Integer
        'タイマー開始の電文を送信する
        '全レコード数を返す

        Dim Denbun As String = ""
        Denbun = "JS,"
        Denbun = Denbun & "TIMSTART,"
        Denbun = Denbun & "GM01,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & 区分番号 & ","
        Denbun = Denbun & ラウンド番号 & ","
        Denbun = Denbun & 種目順 & ","
        Denbun = Denbun & ヒート番号 & ","
        Denbun = Denbun & タイマカテゴリ & ","
        Denbun = Denbun & カウントダウン時間

        Send(Denbun)

        Return 1


    End Function

    Private Sub WriteBookmark(str As String, Send As Boolean)
        '電文 Str の内容を BMファイルに書き出す
        'send がfalseだったら、Temp

        マスタデータ.BM_ブックマーク.FileRead()

        Dim BM As BM_BM
        BM = New BM_BM

        BM.区分番号 = str.Split(",")(5)
        BM.ラウンド番号 = str.Split(",")(6)
        BM.種目記号 = str.Split(",")(7)
        BM.ヒート番号 = str.Split(",")(8)
        BM.ジャッジ記号 = str.Split(",")(9)
        BM.ジャッジ名 = マスタデータ.審判員マスタ.Get_ジャッジ表記名(str.Split(",")(9)）
        BM.BM番号 = str.Split(",")(10)
        BM.時刻 = DateTime.Now.ToString("HH:mm:ss")
        BM.タイマーカテゴリ = str.Split(",")(11)
        BM.タイマー時刻 = str.Split(",")(12)

        Dim 登録件数 As Integer = マスタデータ.BM_ブックマーク.登録(BM)


    End Sub

    'NOTICEBM を送信する。
    Public Function SEND_NOTICEBM(区分番号 As String, ラウンド番号 As String, 種目記号 As String, ヒート番号 As Integer) As Integer
        'どこかの端末でブックマークが発生した場合の通知


        Dim Denbun As String = ""
        Denbun = "JS,"
        Denbun = Denbun & "NOTICEBM,"
        Denbun = Denbun & "GM01,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & 区分番号 & ","
        Denbun = Denbun & ラウンド番号 & ","
        Denbun = Denbun & 種目記号 & ","
        Denbun = Denbun & ヒート番号 & ","

        Send(Denbun)

        Return 1


    End Function

    Public Function SEND_ANSBMLIST(区分番号 As String, ラウンド番号 As String, 種目記号 As String, ヒート番号 As Integer) As Integer
        'ブックマークリスト　ANSBMLIST　を返す　

        'BM_ブックマークから該当案件を読み込み
        Dim TEMPDenbun As String = ""
        Dim 件数 As Integer = 0

        マスタデータ.BM_ブックマーク.FileRead()

        For i = 1 To マスタデータ.BM_ブックマーク.登録済みレコード数
            If マスタデータ.BM_ブックマーク.リスト(i).区分番号 = 区分番号 And
                マスタデータ.BM_ブックマーク.リスト(i).ラウンド番号 = ラウンド番号 And
                マスタデータ.BM_ブックマーク.リスト(i).種目記号 = 種目記号 And
                マスタデータ.BM_ブックマーク.リスト(i).ヒート番号 = ヒート番号 Then

                件数 = 件数 + 1
                TEMPDenbun = TEMPDenbun & マスタデータ.BM_ブックマーク.リスト(i).ジャッジ記号 & ","
                TEMPDenbun = TEMPDenbun & マスタデータ.BM_ブックマーク.リスト(i).ジャッジ名 & ","
                TEMPDenbun = TEMPDenbun & マスタデータ.BM_ブックマーク.リスト(i).BM番号 & ","
                TEMPDenbun = TEMPDenbun & マスタデータ.BM_ブックマーク.リスト(i).時刻 & ","
                TEMPDenbun = TEMPDenbun & マスタデータ.BM_ブックマーク.リスト(i).タイマーカテゴリ & ","
                TEMPDenbun = TEMPDenbun & マスタデータ.BM_ブックマーク.リスト(i).タイマー時刻 & ","

            End If
        Next i


        Dim Denbun As String = ""
        Denbun = "JS,"
        Denbun = Denbun & "ANSBMLIST,"
        Denbun = Denbun & "GM01,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & 区分番号 & ","
        Denbun = Denbun & ラウンド番号 & ","
        Denbun = Denbun & 種目記号 & ","
        Denbun = Denbun & ヒート番号 & ","

        Denbun = Denbun & 件数 & ","

        Denbun = Denbun & TEMPDenbun




        Send(Denbun)

        Return 1



    End Function



    '====ここから下は、司会端末用
    Public Function SEND_KANS_MA_COMP() As Integer
        '関連端末に競技会情報を返す

        Dim KANS_MA_COMP As KANS_MA_COMP
        KANS_MA_COMP = New KANS_MA_COMP()

        Dim denbun As String = KANS_MA_COMP.Create電文("GM01", マスタデータ)

        Send(denbun)

        KANS_MA_COMP = Nothing

        Return 1

    End Function

    Public Function SEND_KANS_MB_KUBUN() As Integer
        '関連端末に区分情報を返す

        マスタデータ.T_採点進行管理.FileRead()
        Dim 総進行区分数 As Integer = マスタデータ.T_採点進行管理.登録済みレコード数

        Dim KANS_MB_KUBUN As KANS_MB_KUBUN
        KANS_MB_KUBUN = New KANS_MB_KUBUN(総進行区分数)

        Dim denbun() = KANS_MB_KUBUN.Create電文("GM01", マスタデータ)

        For i = 1 To UBound(denbun)
            Send(denbun(i))
        Next i


        KANS_MB_KUBUN = Nothing

        Return UBound(denbun)

    End Function

    Public Function SEND_KANS_MU_Progress(ByVal 区分番号 As String, ByVal ラウンド番号 As String) As Integer
        '関連端末にヒート情報を返す



        Dim KANS_MU_Progress As KANS_MU_Progress
        KANS_MU_Progress = New KANS_MU_Progress()

        Dim denbun() = KANS_MU_Progress.Create電文("GM01", マスタデータ, 区分番号, ラウンド番号)

        For i = 1 To UBound(denbun)
            Send(denbun(i))
        Next i

        KANS_MU_Progress = Nothing

        Return UBound(denbun)

    End Function

    Public Function SEND_KANS_HEAT(ByVal 区分番号 As String, ByVal ラウンド番号 As String, 種目順 As Integer, ヒート番号 As Integer) As Integer
        '関連端末にヒート情報を返す


        Dim KANS_HEAT As KANS_HEAT
        KANS_HEAT = New KANS_HEAT()

        Dim denbun() = KANS_HEAT.Create電文("GM01", マスタデータ, 区分番号, ラウンド番号, 種目順, ヒート番号)

        For i = 1 To UBound(denbun)
            Send(denbun(i))
        Next i

        KANS_HEAT = Nothing

        Return UBound(denbun)

    End Function

    Public Function SEND_KANS_FF_KUBUN()
        '関連端末に　入賞者表示用区分一覧を

        Dim KANS_FF_KUBUN As KANS_FF_KUBUN
        KANS_FF_KUBUN = New KANS_FF_KUBUN()

        Dim denbun() = KANS_FF_KUBUN.Create電文("GM01", マスタデータ)

        For i = 1 To UBound(denbun)
            Send(denbun(i))
        Next i

        KANS_FF_KUBUN = Nothing

        Return UBound(denbun)

    End Function


    Public Function SEND_KANS_FF_RESULT(ByVal 区分番号 As String) As Integer
        '関連端末に総合結果情報を返す


        Dim KANS_FF_RESULT As KANS_FF_RESULT
        KANS_FF_RESULT = New KANS_FF_RESULT()

        Dim denbun() = KANS_FF_RESULT.Create電文("GM01", マスタデータ, 区分番号)

        For i = 1 To UBound(denbun)
            Send(denbun(i))
        Next i

        KANS_FF_RESULT = Nothing

        Return UBound(denbun)

    End Function


    Public Function SEND_KANS_RESULT(ByVal 区分番号 As String, ByVal ラウンド番号 As String) As Integer
        '関連端末に総合結果情報を返す


        Dim KANS_RESULT As KANS_RESULT
        KANS_RESULT = New KANS_RESULT()

        Dim denbun() = KANS_RESULT.Create電文("GM01", マスタデータ, 区分番号, ラウンド番号)

        For i = 1 To UBound(denbun)
            Send(denbun(i))
        Next i

        KANS_RESULT = Nothing

        Return UBound(denbun)

    End Function

    Public Function SEND_KANS_JUDGE(ByVal 区分番号 As String, ByVal ラウンド番号 As String) As Integer
        '関連端末にジャッジ情報を返す


        Dim KANS_JUDGE As KANS_JUDGE
        KANS_JUDGE = New KANS_JUDGE()

        Dim denbun() = KANS_JUDGE.Create電文("GM01", マスタデータ, 区分番号, ラウンド番号)

        For i = 1 To UBound(denbun)
            Send(denbun(i))
        Next i

        KANS_JUDGE = Nothing

        Return UBound(denbun)

    End Function

    Public Function SEND_KANS_RESULT_J(ByVal 区分番号 As String, ByVal ラウンド番号 As String) As Integer
        '関連端末に総合結果情報 JSONを返す

        Dim SC_J_詳細結果 = New SC_J_詳細結果(マスタデータ.Z_システム設定.Comp_filepath)

        SC_J_詳細結果 = SC_J_詳細結果.JSON読み込み(区分番号, ラウンド番号)

        If SC_J_詳細結果 Is Nothing Then
            '詳細結果ファイルが存在しない場合
            Dim 採点結果 = New 採点結果_C(区分番号, ラウンド番号)

            SC_J_詳細結果 = New SC_J_詳細結果(採点結果.マスタデータ.Z_システム設定.Comp_filepath)
            SC_J_詳細結果.データ設定(採点結果)
            SC_J_詳細結果.JSON書き出し()
            採点結果 = Nothing
        End If



        If SC_J_詳細結果 IsNot Nothing Then

            Dim Denbun = SC_J_詳細結果.Get_JSON文字列


            'ヘッダー文字列を作成
            Dim Hedder As String = ""
            Hedder = "JS,"
            Hedder = Hedder & "KANS_RESULT_J,"
            Hedder = Hedder & "GM01,"
            Hedder = Hedder & "1" & ",1,"   ' 全レコード数
            Hedder = Hedder & 区分番号 & ","
            Hedder = Hedder & ラウンド番号 & ","

            Denbun = Hedder & Denbun

            '改行コードを削除
            Denbun = Denbun.Replace(Environment.NewLine, "")


            Send(Denbun)

            Return 1
        Else
            'それでもSC_J_詳細結果が無い場合はReturn 0


            Return 0


        End If



    End Function




    '==Send NOWSTATUS
    Public Function SEND_NOWSTATUS() As Integer
        '端末がコネクトしてきたら、現在のステータスを返す
        '全レコード数を返す

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim 進行 As U_進行 = マスタデータ.U_進行管理.Get_現在進行()

        Dim Denbun As String = ""
        Denbun = "JK,"
        Denbun = Denbun & "NOWSTATUS,"
        Denbun = Denbun & "GM01,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & マスタデータ.A_競技会マスタ.公認競技会NO & ","   '競技会NO

        If 進行 IsNot Nothing Then

            Denbun = Denbun & 進行.競技番号 & ","   '進行番号
            Denbun = Denbun & 進行.競技番号枝番 & ","   '進行番号枝番
            Denbun = Denbun & 進行.種目順 & ","   '種目順
            Denbun = Denbun & 進行.ヒート番号    'ヒート番号

        Else
            Denbun = Denbun & "001,"   '進行番号
            Denbun = Denbun & "00,"   '進行番号枝番
            Denbun = Denbun & "1,"   '種目順
            Denbun = Denbun & "1"   'ヒート番号


        End If

        Send(Denbun)

        Return 1

    End Function



    '==Send TIMER
    Public Function SEND_TIMER(タイマカテゴリ As String, カウントダウン時間 As String) As Integer
        'タイマー開始の電文を送信する
        '全レコード数を返す

        Dim Denbun As String = ""
        Denbun = "JK,"
        Denbun = Denbun & "TIMER,"
        Denbun = Denbun & "GM01,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & タイマカテゴリ & ","
        Denbun = Denbun & カウントダウン時間

        Send(Denbun)

        Return 1


    End Function


    '==Send 減点確認中
    Public Function SEND_JK_REDUCTION(減点ステータス As String) As Integer
        'タイマー開始の電文を送信する
        '全レコード数を返す

        Dim Denbun As String = ""
        Denbun = "JK,"
        Denbun = Denbun & "JK_REDUCTION,"
        Denbun = Denbun & "GM01,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & 減点ステータス     'START or END

        Send(Denbun)

        Return 1


    End Function

    '==Send 減点確認中
    Public Function SEND_GOSTOP(GOSTOPステータス As String) As Integer
        'タイマー開始の電文を送信する
        '全レコード数を返す

        Dim Denbun As String = ""
        Denbun = "JK,"
        Denbun = Denbun & "GOSTOP,"
        Denbun = Denbun & "GM01,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & GOSTOPステータス     'GO or STOP

        Send(Denbun)

        Return 1


    End Function

    '==Send 結果決定
    Public Function SEND_RESULTOK(区分番号 As String, ラウンド番号 As String) As Integer
        'タイマー開始の電文を送信する
        '全レコード数を返す

        Dim Denbun As String = ""
        Denbun = "JK,"
        Denbun = Denbun & "RESULTOK,"
        Denbun = Denbun & "GM01,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & 区分番号 & ","
        Denbun = Denbun & ラウンド番号

        Send(Denbun)

        Return 1


    End Function


    'MOVIE STARTを返す
    Public Function SEND_KANS_MOVIE_START(Jtext As String) As Integer


        Dim Denbun As String = ""
        Denbun = "JK,"
        Denbun = Denbun & "KANS_MOVIE_START,"
        Denbun = Denbun & "GM01,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & Jtext

        Send(Denbun)

        Return 1


    End Function

    'MOVIE STARTを返す
    Public Function SEND_KANS_MOVIE_STOP() As Integer


        Dim Denbun As String = ""
        Denbun = "JK,"
        Denbun = Denbun & "KANS_MOVIE_STOP,"
        Denbun = Denbun & "GM01,"
        Denbun = Denbun & "1,"
        Denbun = Denbun & "1"

        Send(Denbun)

        Return 1


    End Function


    '結果表示端末に、マスタデータ JSONを渡す
    Public Function SEND_DANS_マスタデータ() As Integer
        '結果表示端末に、マスタデータ JSONを渡す


        Dim SCマスタデータ = New SC_マスタデータ_J

        SCマスタデータ.データセット(マスタデータ)

        If SCマスタデータ Is Nothing Then
            'マスタデータが存在しない場合

            Dim マスタデータ_2 = New マスタデータ()
            SCマスタデータ.データセット(マスタデータ_2)
            マスタデータ_2 = Nothing

        End If



        If SCマスタデータ IsNot Nothing Then

            Dim Denbun = SCマスタデータ.Get_JSON文字列


            'ヘッダー文字列を作成
            Dim Hedder As String = ""
            Hedder = "JS,"
            Hedder = Hedder & "DANS_MASTER_J,"
            Hedder = Hedder & "GM01,"
            Hedder = Hedder & "1" & ",1,"   ' 全レコード数

            Denbun = Hedder & Denbun

            '改行コードを削除
            Denbun = Denbun.Replace(Environment.NewLine, "")


            Send(Denbun)

            SCマスタデータ.JSON書き出し(マスタデータ.Z_システム設定.Comp_filepath)

            Return 1
        Else
            'それでもSCマスタデータが無い場合はReturn 0


            Return 0


        End If

    End Function


    Public Function SEND_DANS_スタートリスト(区分番号 As String, ラウンド番号 As String) As Integer
        '結果表示端末に、スタートリスト JSONを渡す


        Dim SCスタートリスト = New SC_スタートリスト_J


        SCスタートリスト.データセット(マスタデータ, 区分番号, ラウンド番号)

        If SCスタートリスト Is Nothing Then
            'マスタデータが存在しない場合

            Dim マスタデータ_2 = New マスタデータ()
            SCスタートリスト.データセット(マスタデータ_2, 区分番号, ラウンド番号)
            マスタデータ_2 = Nothing

        End If



        If SCスタートリスト IsNot Nothing Then

            Dim Denbun = SCスタートリスト.Get_JSON文字列


            'ヘッダー文字列を作成
            Dim Hedder As String = ""
            Hedder = "JS,"
            Hedder = Hedder & "DANS_STARTLIST_J,"
            Hedder = Hedder & "GM01,"
            Hedder = Hedder & "1" & ","   ' 全レコード数
            Hedder = Hedder & "1" & ","   ' 当レコード番号
            Hedder = Hedder & 区分番号 & ","
            Hedder = Hedder & ラウンド番号 & ","


            Denbun = Hedder & Denbun

            '改行コードを削除
            Denbun = Denbun.Replace(Environment.NewLine, "")

            Try
                SCスタートリスト.JSON書き出し(マスタデータ.Z_システム設定.Comp_filepath)

            Catch ex As Exception

            End Try


            Send(Denbun)



            Return 1
        Else
            'それでもSCマスタデータが無い場合はReturn 0


            Return 0


        End If

    End Function

    Public Function SEND_DANS_採点結果(区分番号 As String, ラウンド番号 As String) As Integer
        '結果表示端末に、採点結果JSONを渡す


        Dim SC採点結果 = New 採点結果_V2

        SC採点結果 = SC採点結果.JSON読み込み(マスタデータ.Z_システム設定.Comp_filepath, 区分番号, ラウンド番号)

        If SC採点結果 Is Nothing Then
            ' SC採点結果が存在しない場合

            ' SC採点結果 = New 採点結果_V2(区分番号, ラウンド番号)

            ' SC採点結果.JSON書き出し()

        End If



        If SC採点結果 IsNot Nothing Then

            Dim Denbun = SC採点結果.Get_JSON文字列


            'ヘッダー文字列を作成
            Dim Hedder As String = ""
            Hedder = "JS,"
            Hedder = Hedder & "DANS_RESULT_J,"
            Hedder = Hedder & "GM01,"
            Hedder = Hedder & "1" & ","   ' 全レコード数
            Hedder = Hedder & "1" & ","   ' 当レコード番号
            Hedder = Hedder & 区分番号 & ","
            Hedder = Hedder & ラウンド番号 & ","


            Denbun = Hedder & Denbun

            '改行コードを削除
            Denbun = Denbun.Replace(Environment.NewLine, "")


            Send(Denbun)



            Return 1
        Else
            'それでもSCマスタデータが無い場合はReturn 0


            Return 0


        End If

    End Function

    Public Function SEND_DEVENT_J(DEVENT_PARM As DEVENT_PARM_C) As Integer
        '結果表示端末に、DEVENT JSONを渡す



        If DEVENT_PARM IsNot Nothing Then

            Dim Denbun = DEVENT_PARM.Get_JSON文字列


            'ヘッダー文字列を作成
            Dim Hedder As String = ""
            Hedder = "JS,"
            Hedder = Hedder & "DEVENT_J,"
            Hedder = Hedder & "GM01,"
            Hedder = Hedder & "1" & ","   ' 全レコード数
            Hedder = Hedder & "1" & ","   ' 当レコード番号

            Denbun = Hedder & Denbun

            '改行コードを削除
            Denbun = Denbun.Replace(Environment.NewLine, "")


            Send(Denbun)



            Return 1
        Else


            Return 0


        End If

    End Function


End Class

