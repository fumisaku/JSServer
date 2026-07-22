Public Class U_進行管理


    Public リスト(1000) As U_進行

    Public 登録済みレコード数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "MU_Progress"



    ''' コンストラクタ
    ''' 
    Sub New(filepath_)

        登録済みレコード数 = 0

        filepath = filepath_

        FileRead()

    End Sub


    '' ********  メソッド *************
    ''
    'U_進行を返す
    Public Function Get_進行(競技番号 As String, 競技番号枝番 As String, 種目順 As Integer, ヒート番号 As Integer) As U_進行

        Dim rc As U_進行 = Nothing

        For i = 1 To 登録済みレコード数
            If CInt(リスト(i).競技番号) = CInt(競技番号) And
               CInt(リスト(i).競技番号枝番) = CInt(競技番号枝番) And
               CInt(リスト(i).種目順) = 種目順 And
               CInt(リスト(i).ヒート番号) = ヒート番号 Then

                rc = リスト(i)


                i = 登録済みレコード数
            End If
        Next i

        Return rc

    End Function

    '進行番号を渡すとU_進行クラスを返す
    Public Function Get_進行by進行番号(進行番号) As U_進行
        Dim rc As U_進行 = Nothing

        rc = リスト(進行番号)

        Return rc
    End Function

    '現在のU_進行を返す
    Public Function Get_現在進行() As U_進行
        '現在の進行は、最初に「ヒート表作成済み」となっている区分

        Dim rc As U_進行 = Nothing

        For i = 1 To 登録済みレコード数
            If リスト(i).ステータス = "ヒート表作成済み" Then

                rc = リスト(i)

                i = 登録済みレコード数
            End If
        Next i

        If rc Is Nothing And リスト(1) IsNot Nothing Then
            rc = リスト(1)
        End If

        Return rc

    End Function


    '現在のU_進行を返す
    Public Function Get_次進行(現在競技番号 As String, 現在競技枝番 As String) As U_進行
        '現在の競技番号、競技番号枝番を渡すと、次の進行を返す

        Dim rc As U_進行 = Nothing
        Dim 検索開始番号 As Integer = 0

        For i = 1 To 登録済みレコード数
            If CInt(リスト(i).競技番号) = CInt(現在競技番号) And CInt(リスト(i).競技番号枝番) = CInt(現在競技枝番） Then
                検索開始番号 = i
                i = 登録済みレコード数
            End If
        Next i

        For i = 検索開始番号 To 登録済みレコード数
            If CInt(リスト(i).競技番号) <> CInt(現在競技番号) Or CInt(リスト(i).競技番号枝番) <> CInt(現在競技枝番) Then
                rc = リスト(i)

                i = 登録済みレコード数

            End If
        Next i



        If rc Is Nothing And リスト(1) IsNot Nothing Then
            rc = リスト(1)
        End If

        Return rc

    End Function


    ''' 採点進行データをCSVに追加する、同じ競技番号の時は更新する
    '''
    Public Function 登録(データ As U_進行) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        ' 回避策2: ファイルロック競合に備え、最大5回リトライする
        Dim retryCount As Integer = 0
        Do
            Try
                'ファイルを上書きし、Shift JISで書き込む
                Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

                'ヘッダーを書き出し
                sw.WriteLine("競技番号,枝番,種目順,ヒート番号,ステータス,採点終了時刻")


                Dim s As Integer
                Dim 登録済みFLAG As Boolean = False

                For s = 1 To 登録済みレコード数

                    Dim 元記号, 新記号
                    Dim 元種目順, 新種目順, 元ヒート番号, 新ヒート番号 As Integer

                    '記号が数値型の場合は、数値に変換して大小比較する。
                    If IsNumeric(リスト(s).競技番号 & リスト(s).競技番号枝番) And IsNumeric(データ.競技番号 & データ.競技番号枝番) Then

                        元記号 = CInt(リスト(s).競技番号 & リスト(s).競技番号枝番)
                        新記号 = CInt(データ.競技番号 & データ.競技番号枝番)

                        元種目順 = リスト(s).種目順
                        新種目順 = データ.種目順

                        元ヒート番号 = リスト(s).ヒート番号
                        新ヒート番号 = データ.ヒート番号

                        'そうではない場合は、文字列のままで比較する
                    Else
                        元記号 = リスト(s).競技番号 & リスト(s).競技番号枝番
                        新記号 = データ.競技番号 & データ.競技番号枝番
                    End If


                    If 元記号 = 新記号 And 元種目順 = 新種目順 And 元ヒート番号 = 新ヒート番号 Then
                        '同じ区分があった場合は、更新

                        '新しいデータを登録する。
                        sw.WriteLine(カンマ区切り(データ))
                        登録済みFLAG = True

                    ElseIf 元記号 = 新記号 And 元種目順 < 新種目順 Then
                        '区分は一緒だけど種目順が小さい場合は単純に登録
                        sw.WriteLine(カンマ区切り（リスト(s)))

                    ElseIf 元記号 = 新記号 And 元種目順 = 新種目順 And 元ヒート番号 < 新ヒート番号 Then
                        '区分と種目順は一緒だけどヒート番号が小さい場合は単純に登録
                        sw.WriteLine(カンマ区切り（リスト(s)))

                    ElseIf 元記号 = 新記号 And 元種目順 > 新種目順 Then

                        If 登録済みFLAG = False Then
                            '新しいデータを登録する。
                            sw.WriteLine(カンマ区切り(データ))
                            登録済みFLAG = True
                        End If

                        sw.WriteLine(カンマ区切り(リスト(s)))

                    ElseIf 元記号 = 新記号 And 元種目順 = 新種目順 And 元ヒート番号 > 新ヒート番号 Then

                        If 登録済みFLAG = False Then
                            '新しいデータを登録する。
                            sw.WriteLine(カンマ区切り(データ))
                            登録済みFLAG = True
                        End If

                        sw.WriteLine(カンマ区切り(リスト(s)))



                    ElseIf 元記号 < 新記号 Then
                        '区分番号が小さい場合は、単純に登録
                        sw.WriteLine(カンマ区切り（リスト(s)))

                    ElseIf 元記号 > 新記号 Then

                        If 登録済みFLAG = False Then
                            '新しいデータを登録する。
                            sw.WriteLine(カンマ区切り(データ))
                            登録済みFLAG = True
                        End If

                        sw.WriteLine(カンマ区切り(リスト(s)))

                    End If

                Next s

                '背番号が一番大きい時は一番最後に追加
                If 登録済みFLAG = False Then
                    sw.WriteLine(カンマ区切り(データ))
                    登録済みFLAG = True
                End If

                '閉じる
                sw.Close()

                rc = 0
                Exit Do  ' 書き込み成功 → ループ終了

            Catch ex As System.IO.IOException When retryCount < 5
                ' 回避策2: 別スレッドのファイルロック競合 → 50ms 待ってリトライ
                retryCount += 1
                System.Threading.Thread.Sleep(50)

            Catch ex As Exception
                rc = 1
                Exit Do

            End Try
        Loop


        '登録が終わったら再度読み込み
        FileRead()

        Return rc

    End Function



    '' 書き込み
    ''



    ''' 読込み
    ''' 
    Public Sub FileRead()

        ReDim リスト(1000)
        登録済みレコード数 = 0

        Dim filename As String

        filename = File頭文字列 & ".csv"

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した

            ' 回避策1: FileShare.ReadWrite を指定し、書き込み中でも読み込めるようにする
            Dim fs As New System.IO.FileStream(
                filepath & "\" & filename,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read,
                System.IO.FileShare.ReadWrite)
            Dim cReader As New System.IO.StreamReader(fs, System.Text.Encoding.Default)

            ' 読み込んだ結果をすべて格納するための変数を宣言する
            Dim stResult As String = String.Empty


            ' 読み込みできる文字がなくなるまで繰り返す

            While (cReader.Peek() >= 0)
                ' ファイルを 1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()

                'ファイルの１行目はヘッダーなので読み込まない
                If Left(stBuffer, 4) = "競技番号" Then

                Else
                    '読み込んだ１行分のデータを配列に登録する
                    Addデータ(stBuffer)

                End If



            End While


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If






    End Sub

    '''Keyで指定したレコードを削除する　　全部消す
    Public Function Deleteレコード()

        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("競技番号,枝番,種目順,ヒート番号,ステータス,採点終了時刻")


            For s = 1 To 登録済みレコード数

            Next s

            '閉じる 
            sw.Close()

        Catch ex As Exception
            rc = 1

        End Try

        '登録が終わったら再度読み込み
        FileRead()

        Return rc

    End Function


    ''' 個別の区分データをカンマ区切りの文字列に変換する
    '''
    Private Function カンマ区切り(進行 As U_進行)

        Dim line As String = ""

        line = line & 進行.競技番号

        line = line & "," & 進行.競技番号枝番
        line = line & "," & 進行.種目順
        line = line & "," & 進行.ヒート番号
        line = line & "," & 進行.ステータス
        line = line & "," & 進行.採点終了時刻


        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りの審判員データを、配列に登録する
    '''
    Private Sub Addデータ(データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim No As Integer = 登録済みレコード数 + 1

        If UBound(arBuffer) >= 4 Then

            リスト(No) = New U_進行

            リスト(No).競技番号 = arBuffer(0)
            リスト(No).競技番号枝番 = arBuffer(1)
            リスト(No).種目順 = arBuffer(2)
            リスト(No).ヒート番号 = arBuffer(3)
            リスト(No).ステータス = arBuffer(4)
            リスト(No).採点終了時刻 = arBuffer(5)

            リスト(No).進行番号 = No

        End If


        '登録済み審判員数をカウントアップ
        登録済みレコード数 = 登録済みレコード数 + 1

    End Sub


    '進行管理ファイルの更新
    'マスタデータを元に、進行管理ファイルを更新する。
    Public Sub 更新()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim 旧マスタデータ As マスタデータ
        旧マスタデータ = New マスタデータ

        '一旦全て消す
        マスタデータ.U_進行管理.Deleteレコード()

        For d = 1 To マスタデータ.D_種目マスタ.登録済みレコード数

            Dim 新進行C As U_進行
            新進行C = New U_進行

            Dim 採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(マスタデータ.D_種目マスタ.リスト(d).区分番号, マスタデータ.D_種目マスタ.リスト(d).ラウンド番号)

            If 採点進行C IsNot Nothing Then


                Dim ヒート数 As Integer = マスタデータ.D_種目マスタ.リスト(d).ヒート数

                Dim 採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(マスタデータ.D_種目マスタ.リスト(d).区分番号, マスタデータ.D_種目マスタ.リスト(d).ラウンド番号)
                If Strings.Left(採点方式, 4) = "BJS2" Then
                    ヒート数 = 1
                End If

                For h = 1 To ヒート数

                    Dim 現在進行C As U_進行 = 旧マスタデータ.U_進行管理.Get_進行(採点進行C.競技番号, 採点進行C.競技番号枝番, マスタデータ.D_種目マスタ.リスト(d).種目順, h)

                    新進行C.競技番号 = 採点進行C.競技番号
                    新進行C.競技番号枝番 = 採点進行C.競技番号枝番
                    新進行C.種目順 = マスタデータ.D_種目マスタ.リスト(d).種目順
                    新進行C.ヒート番号 = h

                    If 現在進行C Is Nothing Then
                        If マスタデータ.E_ヒート表マスタ.FileCheck(採点進行C.区分番号, 採点進行C.ラウンド番号) = True Then
                            新進行C.ステータス = "ヒート表作成済み"
                            新進行C.採点終了時刻 = ""
                        Else
                            新進行C.ステータス = "準備前"
                            新進行C.採点終了時刻 = ""
                        End If

                    Else
                        If 現在進行C.ステータス = "" Or 現在進行C.ステータス = "準備前" Then
                            If マスタデータ.E_ヒート表マスタ.FileCheck(採点進行C.区分番号, 採点進行C.ラウンド番号) = True Then
                                新進行C.ステータス = "ヒート表作成済み"
                                新進行C.採点終了時刻 = ""
                            Else
                                新進行C.ステータス = 現在進行C.ステータス
                                新進行C.採点終了時刻 = 現在進行C.採点終了時刻
                            End If
                        Else
                            新進行C.ステータス = 現在進行C.ステータス
                            新進行C.採点終了時刻 = 現在進行C.採点終了時刻
                        End If
                    End If


                    マスタデータ.U_進行管理.登録(新進行C)

                Next h

            End If

            新進行C = Nothing
        Next d

        マスタデータ = Nothing
        旧マスタデータ = Nothing

    End Sub



End Class
