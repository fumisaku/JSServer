Public Class 区分一覧HTML


    '区分一覧をHTMLで書き出す

    Public Sub New()


    End Sub

    Public Sub CreateHTML(LOCAL指定 As String)

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ


        Dim 言語 As String = マスタデータ.Z_システム設定.言語


        Dim データ(10000) As String
        Dim i As Integer = 0

        If LOCAL指定 = "LOCAL" Then
            データ(i) = "<link rel=""stylesheet"" type=""text/css"" href=""result.css"">"
        Else
            データ(i) = ""
        End If


        '共通タイトル作成

        i = i + 1 : データ(i) = "<table Class=""table_coomon"" width=""100%"">"
        i = i + 1 : データ(i) = " <thead>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "              <td colspan = ""7"" Class=""Title"" >【Cometition Result Flash】 </td> "
        Else
            i = i + 1 : データ(i) = "              <td colspan = ""7"" Class=""Title"" >【競技結果速報】 </td> "
        End If

        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "     </thead>"
        i = i + 1 : データ(i) = "     <tbody>"
        i = i + 1 : データ(i) = "        <tr>"
        i = i + 1 : データ(i) = "              <td colspan = ""7"" Class=""Compname""> " & マスタデータ.A_競技会マスタ.競技会名 & " </t>"
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "     </tbody>”
        i = i + 1 : データ(i) = "</table>”

        '共通タイトル作成　ここまで



        'ラウンド表
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "<h2>Categories </h2>”
        Else
            i = i + 1 : データ(i) = "<h2>区分</h2>”
        End If
        i = i + 1 : データ(i) = "<table class=""table1""  width=""100%""  >"

        'タイトル
        i = i + 1 : データ(i) = "     <thead>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "	        <th>No</th>"
            i = i + 1 : データ(i) = "	        <th></th>"
            i = i + 1 : データ(i) = "	        <th>Category</th>"
        Else
            i = i + 1 : データ(i) = "	        <th>区分番号</th>"
            i = i + 1 : データ(i) = "	        <th>区分記号</th>"
            i = i + 1 : データ(i) = "	        <th>区分名</th>"
        End If

        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "     </thead>"


        '区分データ行
        i = i + 1 : データ(i) = "     <tbody>"


        'ブレイキンのグループID
        Dim ブレイキングループ(20, 2) As String
        '1列目  グループID
        '2列目  "OK" or ""   既に区分一覧に記帳したかしていないか

        For b = 1 To マスタデータ.BR_カテゴリマスタ.登録済みレコード数
            ブレイキングループ(b, 1) = マスタデータ.BR_カテゴリマスタ.リスト(b).カテゴリ番号

        Next b


        For k = 1 To マスタデータ.B_区分マスタ.登録済みレコード数

            Dim 区分 As B_区分 = マスタデータ.B_区分マスタ.リスト(k)

            'ブレイキンかどうかの判定
            Dim ブレイキンFLAG As String = ""   '該当する場合はカテゴリ番号が入る

            For b = 1 To UBound(マスタデータ.BR_グループマスタ.リスト)
                If マスタデータ.BR_グループマスタ.リスト(b) IsNot Nothing Then
                    If マスタデータ.BR_グループマスタ.リスト(b).区分番号 = 区分.区分番号 Then
                        ブレイキンFLAG = マスタデータ.BR_グループマスタ.リスト(b).カテゴリ番号
                        b = UBound(マスタデータ.BR_グループマスタ.リスト)
                    End If
                End If
            Next b

            If ブレイキンFLAG <> "" Then
                'ブレイキンの場合

                For bb = 1 To UBound(ブレイキングループ)
                    If ブレイキンFLAG = ブレイキングループ(bb, 1) Then
                        If ブレイキングループ(bb, 2) = "" Then


                            i = i + 1 : データ(i) = "        <tr>"
                            i = i + 1 : データ(i) = "         <td class=""No"" >" & ブレイキンFLAG & "</td>"
                            i = i + 1 : データ(i) = "         <td class=""Data01"" >" & 区分.区分記号 & "</td>"

                            Dim Round_file名 As String = "Round_" & 区分.区分番号 & ".html"
                            If FileCheck(マスタデータ.Z_システム設定.HTML_filepath, Round_file名) = True Then

                                If 言語 = "E" Then
                                    i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "<a href = """ & Round_file名 & """>" & マスタデータ.BR_カテゴリマスタ.Getカテゴリ名(ブレイキンFLAG) & "</a>" & "</td>"
                                Else
                                    i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "<a href = """ & Round_file名 & """>" & マスタデータ.BR_カテゴリマスタ.Getカテゴリ名(ブレイキンFLAG) & "</a>" & "</td>"
                                End If
                            Else
                                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & マスタデータ.BR_カテゴリマスタ.Getカテゴリ名(ブレイキンFLAG) & "</td>"
                            End If



                            i = i + 1 : データ(i) = "        </tr>"




                            ブレイキングループ(bb, 2) = "OK"   '作成済みを記帳する
                        End If
                        bb = UBound(ブレイキングループ)
                    End If
                Next bb
            Else
                'ブレイキンではない通常の場合



                i = i + 1 : データ(i) = "        <tr>"
                i = i + 1 : データ(i) = "         <td class=""No"" >" & 区分.区分番号 & "</td>"
                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & 区分.区分記号 & "</td>"

                Dim Round_file名 As String = "Round_" & 区分.区分番号 & ".html"
                If FileCheck(マスタデータ.Z_システム設定.HTML_filepath, Round_file名) = True Then

                    If 言語 = "E" Then
                        i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "<a href = """ & Round_file名 & """>" & 区分.区分表記名 & "</a>" & "</td>"
                    Else
                        i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "<a href = """ & Round_file名 & """>" & 区分.区分表記名 & "</a>" & "</td>"
                    End If
                Else
                    i = i + 1 : データ(i) = "         <td class=""Data01"" >" & 区分.区分表記名 & "</td>"
                End If



                i = i + 1 : データ(i) = "        </tr>"

            End If



        Next k


            i = i + 1 : データ(i) = "     </tbody>"
        i = i + 1 : データ(i) = "</table>"


        i = i + 1 : データ(i) = ""



        Dim filename As String = "index.html"



        If LOCAL指定 = "LOCAL" Then

            ファイル書出し(マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult", filename, データ)
        Else
            ファイル書出し(マスタデータ.Z_システム設定.HTML_filepath, filename, データ)

        End If


        CSSファイルコピー()

        競技会一覧作成()

        マスタデータ = Nothing


    End Sub



    Public Function FileCheck(filepath As String, filename As String) As Boolean
        '存在している場合は、True   存在していない場合は、Falseを返す

        Dim rc As Boolean = False


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then

            'ファイルが存在しない
            rc = False
        Else
            'ファイルが存在した
            rc = True

        End If

        Return rc

    End Function


    Private Sub ファイル書出し(ByVal パス名 As String, ByVal File名 As String, ByVal データ() As String)
        '===========================
        '概要　HTMLを作成する(SC_xxxxxx.csv)
        '入力　新File名,データ
        '出力　なし
        '===========================


        Dim NewFilename As String = パス名 & "\" & File名

        '_00_初期化()

        Try
            'ファイル書き出し
            Dim Writer As New IO.StreamWriter(NewFilename, False, System.Text.Encoding.GetEncoding("utf-8"))

            Dim i

            For i = 0 To UBound(データ)
                If データ(i) <> "" Then
                    Writer.Write(データ(i))
                    Writer.WriteLine()
                End If
            Next i
            Writer.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub CSSファイルコピー()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ


        'CSSファイルをコピーする
        If FileCheck(マスタデータ.Z_システム設定.HTML_filepath, "result.css") = True Then

        Else
            Try
                'もしなかったら、コピーする
                System.IO.File.Copy(マスタデータ.Z_システム設定.システムPath & "\result.css", マスタデータ.Z_システム設定.HTML_filepath & "\result.css")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

        'CSSファイルをLOCALにもコピーする
        If FileCheck(マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult", "result.css") = True Then

        Else
            Try
                'もしなかったら、コピーする
                System.IO.File.Copy(マスタデータ.Z_システム設定.システムPath & "\result.css", マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult" & "\result.css")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If



        マスタデータ = Nothing
    End Sub

    Public Sub 競技会一覧作成()

        Dim 競技会一覧HTML As 競技会一覧HTML
        競技会一覧HTML = New 競技会一覧HTML

        競技会一覧HTML.CreateHTML("REMOTE", "")

        競技会一覧HTML = Nothing



    End Sub

End Class
