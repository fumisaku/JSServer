Public Class R_印刷設定マスタ

    Public 配布先(20) As String

    Public リスト(300) As R_印刷設定

    Public 登録済みレコード数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "MR_Print"

    ''' コンストラクタ
    Sub New(filepath_)

        登録済みレコード数 = 0

        filepath = filepath_

        FileRead()

    End Sub



    '' ********  メソッド *************
    ''

    ''' 区分データをCSVに追加する、同じ区分記号の時は更新する
    ''' 
    Public Function 登録(データ As R_印刷設定) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            Dim ヘッダー文字列 As String = ""

            ヘッダー文字列 = "採点方式,ラウンド,帳票記号"
            For i = 1 To 20
                ヘッダー文字列 = ヘッダー文字列 & "," & 配布先(i)
            Next i
            ヘッダー文字列 = ヘッダー文字列 & ","

            sw.WriteLine(ヘッダー文字列)


            Dim s As Integer
            Dim 登録済みFLAG As Boolean = False

            For s = 1 To 登録済みレコード数

                sw.WriteLine(カンマ区切り（リスト(s)))
            Next s

            '背番号が一番大きい時は一番最後に追加
            sw.WriteLine(カンマ区切り(データ))

            '閉じる 
            sw.Close()

        Catch ex As Exception
            rc = 1
            MsgBox(ex.ToString)
        End Try

        '登録が終わったら再度読み込み
        FileRead()

        Return rc

    End Function

    '========Publicメソッド=========
    '配布先リストを返す
    Public Sub Get_配布先リスト(採点方式 As String, ラウンド番号 As String, 帳票記号 As String, ByRef 配布先リスト() As String)

        ReDim 配布先リスト(20)


        Dim 検索ラウンド As String = ""
        If ラウンド番号 = "400" Then
            検索ラウンド = "決勝"
        ElseIf Strings.Left(ラウンド番号, 1) = "0" Then
            検索ラウンド = "予選"
        ElseIf ラウンド番号 = "200" Then
            検索ラウンド = "予選"
        Else
            検索ラウンド = "決勝"
        End If



        For i = 1 To 登録済みレコード数
            If (Strings.Left(リスト(i).採点方式, 3) = Strings.Left(採点方式, 3) Or (Strings.Left(リスト(i).採点方式, 3) = "AJS" And Strings.Left(採点方式, 3) = "PDJ")) And
               リスト(i).ラウンド = 検索ラウンド And
               リスト(i).帳票記号 = 帳票記号 Then

                Dim h As Integer = 0

                For s = 1 To 20
                    If 配布先(s) <> "" Then
                        If リスト(i).印刷部数(s) > 0 Then
                            For b = 1 To リスト(i).印刷部数(s)

                                If h >= 20 Then
                                    '最大血２０になったら終わり
                                    i = 登録済みレコード数
                                    s = 20
                                    Exit For
                                End If

                                h = h + 1
                                配布先リスト(h) = 配布先(s)
                            Next b
                        End If
                    End If
                Next s

            End If

        Next i


    End Sub


    Public Sub Get_配布先名(採点方式 As String, ラウンド番号 As String, 帳票記号 As String, 配布先番号 As Integer, ByRef 配布先名 As String, ByRef 印刷枚数 As Integer)

        '配布先番号　：　１～２０
        'をわたすと、　配布先名と、印刷枚数を返す。

        Dim rc配布先名 As String = ""
        Dim rc印刷枚数 As Integer = 0



        Dim 検索ラウンド As String = ""
        If ラウンド番号 = "400" Then
            検索ラウンド = "決勝"
        ElseIf Strings.Left(ラウンド番号, 1) = "0" Then
            検索ラウンド = "予選"
        ElseIf ラウンド番号 = "200" Then
            検索ラウンド = "予選"
        Else
            検索ラウンド = "決勝"
        End If



        For i = 1 To 登録済みレコード数
            If (Strings.Left(リスト(i).採点方式, 3) = Strings.Left(採点方式, 3) Or (Strings.Left(リスト(i).採点方式, 3) = "AJS" And Strings.Left(採点方式, 3) = "PDJ")) And
               リスト(i).ラウンド = 検索ラウンド And
               リスト(i).帳票記号 = 帳票記号 Then

                If 配布先(配布先番号) <> "" Then
                    If リスト(i).印刷部数(配布先番号) > 0 Then

                        rc配布先名 = 配布先(配布先番号)
                        rc印刷枚数 = リスト(i).印刷部数(配布先番号)

                    End If
                End If

                i = 登録済みレコード数
            End If

        Next i


        配布先名 = rc配布先名
        印刷枚数 = rc印刷枚数

    End Sub



    '' 書き込み
    '''Keyで指定したレコードを削除する　　全部消す
    Public Function Deleteレコード()

        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("採点方式,ラウンド,帳票記号")


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





    ''' 読込み
    ''' 
    Public Sub FileRead()

        ReDim リスト(100)
        登録済みレコード数 = 0

        Dim filename As String

        filename = File頭文字列 & ".csv"

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した

            ' StreamReader の新しいインスタンスを生成する
            Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)

            ' 読み込んだ結果をすべて格納するための変数を宣言する
            Dim stResult As String = String.Empty


            ' 読み込みできる文字がなくなるまで繰り返す

            While (cReader.Peek() >= 0)
                ' ファイルを 1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()

                'ファイルの１行目はヘッダー。配布先に登録する
                If Left(stBuffer, 4) = "採点方式" Then

                    Dim arBuffer = stBuffer.Split(",")

                    For i = 1 To UBound(arBuffer) - 3
                        配布先(i) = arBuffer(2 + i)
                    Next i


                Else
                    '読み込んだ１行分のデータを配列に登録する
                    Addデータ(stBuffer)

                End If



            End While


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If






    End Sub

    ''' 個別の区分データをカンマ区切りの文字列に変換する
    '''
    Private Function カンマ区切り(印刷設定 As R_印刷設定)

        Dim line As String = ""

        line = line & 印刷設定.採点方式

        line = line & "," & 印刷設定.ラウンド
        line = line & "," & 印刷設定.帳票記号
        For i = 1 To 20
            line = line & "," & 印刷設定.印刷部数(i)
        Next i

        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りの審判員データを、配列に登録する
    '''
    Private Sub Addデータ(データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim No As Integer = 登録済みレコード数 + 1

        If UBound(arBuffer) >= 22 Then

            リスト(No) = New R_印刷設定

            リスト(No).採点方式 = arBuffer(0)
            リスト(No).ラウンド = arBuffer(1)
            リスト(No).帳票記号 = arBuffer(2)

            For i = 1 To 20
                リスト(No).印刷部数(i) = arBuffer(2 + i)
            Next i


        End If


        '登録済み審判員数をカウントアップ
        登録済みレコード数 = 登録済みレコード数 + 1

    End Sub


End Class
