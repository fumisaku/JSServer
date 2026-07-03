Imports System
Imports System.Net
Imports System.Net.Sockets

Public Delegate Sub ReceivedDataEventHandler2(ByVal sender As Object, ByVal e As ReceivedDataEventArgs2)

Public Class TCPClient2

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

        マスタデータ = New マスタデータ


    End Sub

    '********* メソッド *****************************

    Private Sub Initialize()

        _maxReceiveLength = 1024
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
                    Me.OnReceivedData2(New ReceivedDataEventArgs2(Me, line))
                End While
                Me.receivedBytes = New System.IO.MemoryStream
            Else
                Me.receivedBytes.Seek(0, System.IO.SeekOrigin.End)
            End If
        End If

        '再び受信開始
        SyncLock Me
            Me._socket.BeginReceive(receiveBuffer, 0, receiveBuffer.Length,
                SocketFlags.None,
                New AsyncCallback(AddressOf ReceiveDataCallback), receiveBuffer)
        End SyncLock
    End Sub


    ''' <summary>
    ''' 文字列を送信する
    ''' </summary>
    ''' <param name="str">送信する文字列</param>
    Public Sub Send(ByVal str As String)
        If Me.IsClosed Then
            Throw New ApplicationException("閉じています。")
        End If

        '文字列をByte型配列に変換
        Dim sendBytes As Byte() = _encording.GetBytes((str + vbCrLf))

        SyncLock Me
            'データを送信する
            Me._socket.Send(sendBytes)

        End SyncLock



    End Sub


    ''' <summary>
    ''' 切断する
    ''' </summary>
    Public Sub Close()
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
        Me.OnDisconnected2(New EventArgs)

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



    'Public Event E11_LOGIN As ServerEventHandler
    'Public Event E12_RCVTemp(ByVal sender As Object, ByVal e As EventArgs)
    'Public Event E13_RCV(ByVal sender As Object, ByVal e As EventArgs)
    'Public Event E19_LOGOUT As ReceivedDataEventHandler
    'Public Event E51_ReqHelp As ReceivedDataEventHandler

    Public Event ReceivedData2 As ReceivedDataEventHandler2
    Protected Overridable Sub OnReceivedData2(ByVal e As ReceivedDataEventArgs2)

        Dim str As String = e.ReceivedString
        Dim 全レコード数 As Integer

        If str.Split(",")(1) = "REQSTATUS" Then
            '端末がコネクトしてきた時

            Me.端末名 = str.Split(",")(2)

            'ANSKUBUN電文を返す
            全レコード数 = SEND_NOWSTATUS(）


        ElseIf str.Split(",")(1) = "HEATSTART" Then
            'ヒート開始のイベント
            Me.端末名 = str.Split(",")(2)

        End If

        RaiseEvent ReceivedData2(Me, e)
    End Sub


    ''' サーバーから切断された、あるいは切断した

    Public Event Disconnected2 As EventHandler
    Protected Overridable Sub OnDisconnected2(ByVal e As EventArgs)
        RaiseEvent Disconnected2(Me, e)
    End Sub

    '========メソッド==============================================


    '==Send NOWSTATUS
    Private Function SEND_NOWSTATUS() As Integer
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
    Public Function SEND_REDUCTION(減点ステータス As String) As Integer
        'タイマー開始の電文を送信する
        '全レコード数を返す

        Dim Denbun As String = ""
        Denbun = "JK,"
        Denbun = Denbun & "REDUCTION,"
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



End Class

