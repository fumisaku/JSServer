Imports System.IO

Public Class A_競技会マスタ

    Public 公認競技会NO As String
    Public 競技会名 As String
    Public 開催日 As String
    Public 主催団体 As String
    Public 開催場所 As String

    Public filepath As String

    Const File頭文字列 = "MA_Comp"

    ''' コンストラクタ
    ''' 
    Sub New(filepath_)


        filepath = filepath_

        FileRead()

    End Sub


    '' ********  メソッド *************
    ''


    ''' 読込み
    ''' 
    Private Sub FileRead()

        Dim filename As String

        filename = File頭文字列 & ".csv"

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した

            ' StreamReader の新しいインスタンスを生成する
            Using stream As New FileStream(filepath & "\" & filename, FileMode.Open, FileAccess.Read)

                Dim cReader As New System.IO.StreamReader(stream, System.Text.Encoding.Default)

                ' 読み込んだ結果をすべて格納するための変数を宣言する
                Dim stResult() As String

                '全行を取得し改行で区切り
                stResult = Split(cReader.ReadToEnd, vbCrLf)

                '行数を取得
                Dim 行数 As Integer = stResult.Count - 1

                ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
                cReader.Close()

                Dim i, j As Integer
                For i = 0 To 行数
                    Select Case stResult(i)
                        Case "[公認競技会NO]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then
                                    Me.公認競技会NO = stResult(j)
                                    j = 行数
                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[競技会名]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then
                                    Me.競技会名 = stResult(j)
                                    j = 行数
                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[開催日]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then
                                    Me.開催日 = stResult(j)
                                    j = 行数
                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j


                        Case "[主催団体]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then
                                    Me.主催団体 = stResult(j)
                                    j = 行数
                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j


                        Case "[開催場所]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then
                                    Me.開催場所 = stResult(j)
                                    j = 行数
                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j


                    End Select


                Next i


            End Using


        End If






    End Sub

    Public Function 登録() As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("[公認競技会NO]")
            sw.WriteLine(公認競技会NO)

            sw.WriteLine("[競技会名]")
            sw.WriteLine(競技会名)

            sw.WriteLine("[開催日]")
            sw.WriteLine(開催日)

            sw.WriteLine("[主催団体]")
            sw.WriteLine(主催団体)

            sw.WriteLine("[開催場所]")
            sw.WriteLine(開催場所)



            '閉じる 
            sw.Close()

        Catch ex As Exception
            rc = 1

        End Try


        '登録が終わったら再度読み込み
        FileRead()

        Return rc

    End Function

    Public Function Deleteレコード()
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し



            '閉じる 
            sw.Close()

        Catch ex As Exception
            rc = 1

        End Try


        '登録が終わったら再度読み込み
        FileRead()

        Return rc


    End Function



End Class
