Imports System.IO

Public Class Z_システム設定


    Public Comp_filepath As String
    Public システムPath As String

    Public HTML_filepath As String

    Public 種目名称() As Z_種目名

    Public 言語 As String

    Public FTPServer As String
    Public SFTPPort As Integer
    Public FTPRemotePath As String
    Public FTPUser As String
    Public FTPPass As String

    Public Timer_D As String
    Public Timer(10) As String

    Public Mファイルパス As String

    ' ログレベル（デフォルト0=ログ出力なし、1=ERR、2=WARNING、3=INFO、4=DEBUG）
    ' Z_System.csv の [ログレベル] セクションで設定。記述がない場合は 0（出力なし）
    Public LogLevel As Integer = 0


    Const File頭文字列 = "Z_System"

    ' コンストラクタ

    Sub New()

        FileRead()

    End Sub



    '' ********  メソッド *************
    ''
    Public Function Get_種目名称(種目記号 As String) As Z_種目名
        Dim rc As Z_種目名 = Nothing

        For i = 1 To UBound(種目名称)
            If 種目名称(i).種目記号 = 種目記号 Then
                rc = 種目名称(i)
                i = UBound(種目名称)
            End If

        Next i

        Return rc
    End Function

    ''' 読込み
    ''' 
    Private Sub FileRead()

        Dim filename As String
        Dim filepath As String = System.IO.Directory.GetCurrentDirectory()  '現行パス

        システムPath = filepath

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

                Dim i, j, s As Integer
                Dim 種目名称登録件数 As Integer = 0
                Dim Buff() As String

                Dim t As Integer = 1

                For i = 0 To 行数
                    Select Case stResult(i)
                        Case "[Comp_Filepath]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then
                                    Me.Comp_filepath = stResult(j)
                                    j = 行数
                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[種目名]"
                            種目名称登録件数 = 0
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then
                                    種目名称登録件数 = 種目名称登録件数 + 1
                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                            ReDim 種目名称(種目名称登録件数）

                            s = 1
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then

                                    Buff = stResult(j).Split(",")

                                    種目名称(s) = New Z_種目名

                                    種目名称(s).種目記号 = Buff(0)
                                    種目名称(s).種目名_J = Buff(1)
                                    種目名称(s).種目名_E = Buff(2)
                                    s = s + 1

                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[言語]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then
                                    Me.言語 = stResult(j)
                                    j = 行数
                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[FTP設定]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then

                                    Buff = stResult(j).Split(",")

                                    Select Case Buff(0)

                                        Case "Server"
                                            Me.FTPServer = Buff(1)
                                        Case "Port"
                                            Me.SFTPPort = Buff(1)
                                        Case "RemotePath"
                                            Me.FTPRemotePath = Buff(1)
                                        Case "User"
                                            Me.FTPUser = Buff(1)
                                        Case "Pass"
                                            Me.FTPPass = Buff(1)
                                    End Select




                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j


                        Case "[タイマー設定]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then

                                    Buff = stResult(j).Split(",")

                                    Select Case Buff(0)


                                        Case "D"
                                            Me.Timer_D = Buff(1)

                                        Case Else
                                            Me.Timer(t) = Buff(0)
                                            t = t + 1
                                    End Select


                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[Mファイルパス]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then
                                    Me.Mファイルパス = stResult(j)
                                    j = 行数
                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[ログレベル]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then
                                    Dim lv As Integer = 0
                                    If Integer.TryParse(stResult(j).Trim(), lv) Then
                                        Me.LogLevel = lv
                                    End If
                                    j = 行数
                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                    End Select


                Next i


            End Using

            Me.HTML_filepath = Me.Comp_filepath & "\Result\"

        End If






    End Sub

    Public Function CompFilePath登録(Path名 As String) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"
        Dim filepath As String = System.IO.Directory.GetCurrentDirectory()  '現行パス

        Dim rc As Integer = 0

        'まずファイルを読み込み

        ' 読み込んだ結果をすべて格納するための変数を宣言する
        Dim stResult() As String
        Dim 行数 As Integer = 0

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない

            MsgBox(filename & "が" & filepath & "に見つかりません")
            rc = 1
            Return rc
            Exit Function


        Else
            'ファイルが存在した

            ' StreamReader の新しいインスタンスを生成する
            Using stream As New FileStream(filepath & "\" & filename, FileMode.Open, FileAccess.Read)

                Dim cReader As New System.IO.StreamReader(stream, System.Text.Encoding.Default)


                '全行を取得し改行で区切り
                stResult = Split(cReader.ReadToEnd, vbCrLf)

                '行数を取得
                行数 = stResult.Count - 1

                ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
                cReader.Close()

            End Using
        End If



        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            For i = 0 To 行数
                If stResult(i) = "[Comp_Filepath]" Then
                    sw.WriteLine(stResult(i))
                    sw.WriteLine(Path名)
                    i = i + 1
                Else
                    sw.WriteLine(stResult(i))
                End If

            Next i

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
