Public Class ラウンド一覧

    '区分毎のラウンド一覧をHTMLで書き出す

    Public Sub New()


    End Sub

    Public Sub CreateHTML(区分番号 As String, LOCAL指定 As String)

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ


        Dim 言語 As String = マスタデータ.Z_システム設定.言語

        'Dim 区分 As B_区分 = マスタデータ.B_区分マスタ.Get区分C(区分番号)

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
            i = i + 1 : データ(i) = "              <td colspan = ""7"" Class=""Title"" >【競技会結果速報　ラウンド選択】 </td> "
        End If

        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "     </thead>"
        i = i + 1 : データ(i) = "     <tbody>"
        i = i + 1 : データ(i) = "        <tr>"
        i = i + 1 : データ(i) = "              <td colspan = ""7"" Class=""Compname""> " & マスタデータ.A_競技会マスタ.競技会名 & " </t>"
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "              <td Class=""Title01"">Category</td> "
        Else
            i = i + 1 : データ(i) = "              <td Class=""Title01"">区分</td> "
        End If



        If マスタデータ.BR_グループマスタ.登録済みレコード数 > 0 Then

            If マスタデータ.BR_グループマスタ.Getカテゴリ番号(区分番号, "010") <> "" Then
                'ブレイキンの時
                i = i + 1 : データ(i) = "          	   <td colspan = ""6"" Class=""Kubun"" >" & マスタデータ.BR_カテゴリマスタ.Getカテゴリ名(マスタデータ.BR_グループマスタ.Getカテゴリ番号(区分番号, "010")) & "</td>"

            Else
                'ブレイキン以外 10ダンスの時
                i = i + 1 : データ(i) = "          	   <td colspan = ""6"" Class=""Kubun"" >" & 区分番号 & " " & マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & "</td>"

            End If
        Else
            'ブレイキン以外 10ダンスの時
            i = i + 1 : データ(i) = "          	   <td colspan = ""6"" Class=""Kubun"" >" & 区分番号 & " " & マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & "</td>"

        End If


        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "     </tbody>”
        i = i + 1 : データ(i) = "</table>”

        '共通タイトル作成　ここまで



        'ラウンド表
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "<h2>Rounds </h2>”
        Else
            i = i + 1 : データ(i) = "<h2>ラウンド</h2>”
        End If
        i = i + 1 : データ(i) = "<table class=""table1""  width=""100%""  >"

        'タイトル
        i = i + 1 : データ(i) = "     <thead>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "	        <th>Round</th>"
            i = i + 1 : データ(i) = "	        <th>Couples#</th>"
            i = i + 1 : データ(i) = "	        <th>UP#</th>"
            i = i + 1 : データ(i) = "	        <th>Starting List</th>"
            i = i + 1 : データ(i) = "	        <th>Result</th>"
        Else
            i = i + 1 : データ(i) = "	        <th>ラウンド</th>"
            i = i + 1 : データ(i) = "	        <th>出場組数</th>"
            i = i + 1 : データ(i) = "	        <th>UP数</th>"
            i = i + 1 : データ(i) = "	        <th>ヒート表</th>"
            i = i + 1 : データ(i) = "	        <th>結果</th>"
        End If

        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "     </thead>"







        'ラウンドデータ行
        i = i + 1 : データ(i) = "     <tbody>"

        For r = 1 To マスタデータ.C_ラウンドマスタ.登録済みレコード数
            If マスタデータ.C_ラウンドマスタ.リスト(r).区分番号 = 区分番号 Then

                Dim ラウンド As C_ラウンド = マスタデータ.C_ラウンドマスタ.リスト(r)


                '===========ブレイキンの対応============
                'ブレイキンかどうかの判定
                If マスタデータ.BR_グループマスタ.登録済みレコード数 > 0 Then
                    If マスタデータ.BR_グループマスタ.Getカテゴリ番号(区分番号, ラウンド.ラウンド番号) <> "" Then
                        'ブレイキンの時　　BRグループマスタに登録があるか無いかで判定

                        i = i + 1 : データ(i) = "        <tr>"
                        If 言語 = "E" Then
                            i = i + 1 : データ(i) = "         <td class=""No"" >" & マスタデータ.Get_ラウンド名_E(ラウンド.ラウンド番号) & "</td>"
                        Else
                            i = i + 1 : データ(i) = "         <td class=""No"" >" & マスタデータ.Get_ラウンド名(ラウンド.ラウンド番号) & "</td>"
                        End If

                        Select Case ラウンド.ラウンド番号
                            Case "010"
                                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "8" & "</td>"
                                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "4" & "</td>"
                            Case "200"
                                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "4" & "</td>"
                                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "2" & "</td>"
                            Case "300"
                                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "2" & "</td>"
                                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "" & "</td>"
                            Case "400"
                                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "2" & "</td>"
                                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "" & "</td>"
                            Case Else
                                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & マスタデータ.C_ラウンドマスタ.出場組数(区分番号, ラウンド.ラウンド番号) & "</td>"
                                i = i + 1 : データ(i) = "         <td class=""Data01"" >" & ラウンド.UP予定数 & "</td>"
                        End Select


                    Else
                        'ブレイキンではないとき　10ダンスの時

                        i = i + 1 : データ(i) = "        <tr>"
                        If 言語 = "E" Then
                            i = i + 1 : データ(i) = "         <td class=""No"" >" & マスタデータ.Get_ラウンド名_E(ラウンド.ラウンド番号) & "</td>"
                        Else
                            i = i + 1 : データ(i) = "         <td class=""No"" >" & マスタデータ.Get_ラウンド名(ラウンド.ラウンド番号) & "</td>"
                        End If
                        i = i + 1 : データ(i) = "         <td class=""Data01"" >" & マスタデータ.C_ラウンドマスタ.出場組数(区分番号, ラウンド.ラウンド番号) & "</td>"

                        i = i + 1 : データ(i) = "         <td class=""Data01"" >" & ラウンド.UP予定数 & "</td>"



                    End If
                Else
                    'ブレイキンではないとき　10ダンスの時  上と同じ

                    i = i + 1 : データ(i) = "        <tr>"
                    If 言語 = "E" Then
                        i = i + 1 : データ(i) = "         <td class=""No"" >" & マスタデータ.Get_ラウンド名_E(ラウンド.ラウンド番号) & "</td>"
                    Else
                        i = i + 1 : データ(i) = "         <td class=""No"" >" & マスタデータ.Get_ラウンド名(ラウンド.ラウンド番号) & "</td>"
                    End If
                    i = i + 1 : データ(i) = "         <td class=""Data01"" >" & マスタデータ.C_ラウンドマスタ.出場組数(区分番号, ラウンド.ラウンド番号) & "</td>"

                    i = i + 1 : データ(i) = "         <td class=""Data01"" >" & ラウンド.UP予定数 & "</td>"

                End If


                '===========ブレイキンの対応=　ここまで===========




                Dim Heat_file名 As String = "Heat_" & 区分番号 & "_" & ラウンド.ラウンド番号 & ".html"
                    If FileCheck(マスタデータ.Z_システム設定.HTML_filepath, Heat_file名) = True Then

                        If 言語 = "E" Then
                            i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "<a href = """ & Heat_file名 & """ > StartingList</a>" & "</td>"
                        Else
                            i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "<a href = """ & Heat_file名 & """ > ヒート表</a>" & "</td>"
                        End If
                    Else
                        i = i + 1 : データ(i) = "<td class=""Data01"" >   </td> "
                    End If

                    Dim result_file名 As String = "Result_" & 区分番号 & "_" & ラウンド.ラウンド番号 & ".html"
                    If FileCheck(マスタデータ.Z_システム設定.HTML_filepath, result_file名) = True Then
                        If 言語 = "E" Then
                            i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "<a href = """ & result_file名 & """ > Result</a>" & "</td>"

                        Else
                            i = i + 1 : データ(i) = "         <td class=""Data01"" >" & "<a href = """ & result_file名 & """ > 結果</a>" & "</td>"
                        End If

                    Else
                        i = i + 1 : データ(i) = "<td class=""Data01"" >   </td> "
                    End If



                    i = i + 1 : データ(i) = "        </tr>"



                End If
        Next r


        i = i + 1 : データ(i) = "     </tbody>"
        i = i + 1 : データ(i) = "</table>"


        i = i + 1 : データ(i) = ""



        Dim filename As String = "Round_" & 区分番号 & ".html"


        If LOCAL指定 = "LOCAL" Then

            ファイル書出し(マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult", filename, データ)
        Else
            ファイル書出し(マスタデータ.Z_システム設定.HTML_filepath, filename, データ)

        End If

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

    End Sub


End Class
