Public Class BJS得点詳細_V2


    'BJSの得点詳細をHTMLで書き出す

    Private データ(10000) As String
    Private i As Integer

    Private 対象区分数 As Integer
    Private 対象区分リスト(20) As String
    Private 対象ラウンド番号 As String
    Private 対象採点結果(20) As 採点結果_C

    Private 対象カテゴリ番号 As String

    Public Sub New()


    End Sub

    Public Sub CreateHTML(採点結果 As 採点結果_C, LOCAL指定 As String)


        '対象区分リストの作成
        対象ラウンド番号 = 採点結果.ラウンド番号

        対象カテゴリ番号 = 採点結果.マスタデータ.BR_グループマスタ.Getカテゴリ番号(採点結果.区分番号, 採点結果.ラウンド番号)

        Dim グループ As BR_グループ


        対象区分数 = 0
        For i = 1 To 採点結果.マスタデータ.BR_グループマスタ.登録済みレコード数
            グループ = 採点結果.マスタデータ.BR_グループマスタ.リスト（i)


            If 対象カテゴリ番号 = グループ.カテゴリ番号 And 対象ラウンド番号 = グループ.ラウンド番号 Then

                対象区分数 = 対象区分数 + 1
                対象区分リスト(対象区分数) = グループ.区分番号

                対象採点結果(対象区分数) = New 採点結果_C(グループ.区分番号, 対象ラウンド番号)

            End If

            If 対象区分数 = 20 Then
                MsgBox("対象区分数数が最大値２０を超えました。")
                Exit Sub
            End If
        Next i



        Dim 言語 As String = 採点結果.マスタデータ.Z_システム設定.言語



        i = 0

        If LOCAL指定 = "LOCAL" Then
            データ(i) = "<link rel=""stylesheet"" type=""text/css"" href=""result.css"">"
        Else
            データ(i) = ""
        End If

        スタイル設定()

        共通タイトル作成(言語, 採点結果)

        総合結果表作成(言語, 採点結果)


        For k = 1 To 対象区分数   '区分数分ループ
            Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(対象採点結果(k).区分番号)

            If 区分 IsNot Nothing Then

                If 対象採点結果(k).種目数 >= 1 Then

                    If 言語 = "E" Then   '区分のタイトル
                        i = i + 1 : データ(i) = "<h3 style=""color: blue""> ■ " & 区分.区分表記名 & "  Detail Result ■</h3>"
                    Else
                        i = i + 1 : データ(i) = "<h3 style=""color: blue""> ■ " & 区分.区分表記名 & "  詳細結果 ■</h3>"
                    End If


                    詳細結果表作成(言語, 対象採点結果(k))
                End If

            End If
        Next k



        'ジャッジ名を表示
        'i = i + 1 : データ(i) = "<p Class=""pagechange"">"    '改ページ
        'If 言語 = "E" Then
        'i = i + 1 : データ(i) = "<h3> " & "  Adjudicators </h3>"
        'Else
        'i = i + 1 : データ(i) = "<h3> " & "  Judge </h3>"
        'End If

        'i = i + 1 : データ(i) = ""

        'For j = 1 To 採点結果.種目(1).審判員数
        'ジャッジ記号を半角に変換
        'If 採点結果.種目(1).選手(1).審判(j).ジャッジRole = "R" Then

        'If 言語 = "E" Then
        'i = i + 1 : データ(i) = "<h3> " & "Referee " & 採点結果.種目(1).選手(1).審判(j).ジャッジ表記名 & "</h3>"

        'Else
        'i = i + 1 : データ(i) = "<h3> " & "レフリー " & 採点結果.種目(1).選手(1).審判(j).ジャッジ表記名 & "</h3>"
        'End If

        'Else
        'i = i + 1 : データ(i) = "<h3> " & StrConv(採点結果.種目(1).選手(1).審判(j).ジャッジ記号, VbStrConv.Narrow) & " " & 採点結果.種目(1).選手(1).審判(j).ジャッジ表記名 & "</h3>"

        'End If

        'Next j






        Dim filename As String = "Result_" & 採点結果.区分番号 & "_" & 採点結果.ラウンド番号 & ".html"

        If LOCAL指定 = "LOCAL" Then

            ファイル書出し(採点結果.マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult", filename, データ)
        Else
            ファイル書出し(採点結果.マスタデータ.Z_システム設定.HTML_filepath, filename, データ)

        End If

    End Sub


    Private Sub スタイル設定()

        i = i + 1 : データ(i) = "<style type=""text/css"">"

        i = i + 1 : データ(i) = ""
        i = i + 1 : データ(i) = "th.Red {"
        i = i + 1 : データ(i) = "		text-align: Center; "
        i = i + 1 : データ(i) = " 		font-weight: borld;"
        i = i + 1 : データ(i) = " 		color: #FFFFFF;"
        i = i + 1 : データ(i) = " 		font-size: 140%;"
        i = i + 1 : データ(i) = " 		background-color: #DC143C;"
        i = i + 1 : データ(i) = " 		padding: 0.3em ;"
        i = i + 1 : データ(i) = "		}		"
        i = i + 1 : データ(i) = "th.Blue {"
        i = i + 1 : データ(i) = "		text-align: Center; "
        i = i + 1 : データ(i) = " 		font-weight: borld;"
        i = i + 1 : データ(i) = " 		color: #FFFFFF;"
        i = i + 1 : データ(i) = " 		font-size: 140%;"
        i = i + 1 : データ(i) = " 		background-color: #0000CD;"
        i = i + 1 : データ(i) = " 		padding: 0.3em ;"
        i = i + 1 : データ(i) = "		}		"
        i = i + 1 : データ(i) = "th.Green {"
        i = i + 1 : データ(i) = "		text-align: Center; "
        i = i + 1 : データ(i) = " 		font-weight: borld;"
        i = i + 1 : データ(i) = " 		color: #000000;"
        i = i + 1 : データ(i) = " 		font-size: 140%;"
        i = i + 1 : データ(i) = " 		background-color: #ccffcc;"
        i = i + 1 : データ(i) = " 		padding: 0.3em ;"
        i = i + 1 : データ(i) = "		}		"
        i = i + 1 : データ(i) = "th.White {"
        i = i + 1 : データ(i) = "		text-align: Center; "
        i = i + 1 : データ(i) = " 		font-weight: borld;"
        i = i + 1 : データ(i) = " 		color: #000000;"
        i = i + 1 : データ(i) = " 		font-size: 140%;"
        i = i + 1 : データ(i) = " 		background-color: #FFFFFF;"
        i = i + 1 : データ(i) = " 		padding: 0.3em ;"
        i = i + 1 : データ(i) = "		}		"
        i = i + 1 : データ(i) = ""
        i = i + 1 : データ(i) = "td.Yellow {"
        i = i + 1 : データ(i) = " 		text-align: Center; "
        i = i + 1 : データ(i) = " 		font-weight: normal;"
        i = i + 1 : データ(i) = " 		color: #000000;"
        i = i + 1 : データ(i) = " 		font-size: 100%;"
        i = i + 1 : データ(i) = " 		background-color: #ffffcc;"
        i = i + 1 : データ(i) = " 		padding: 0.3em ;"
        i = i + 1 : データ(i) = "}"
        i = i + 1 : データ(i) = "td.Green {"
        i = i + 1 : データ(i) = " 		text-align: Center; "
        i = i + 1 : データ(i) = " 		font-weight: normal;"
        i = i + 1 : データ(i) = " 		color: #000000;"
        i = i + 1 : データ(i) = " 		font-size: 100%;"
        i = i + 1 : データ(i) = " 		background-color: #ccffcc;"
        i = i + 1 : データ(i) = " 		padding: 0.3em ;"
        i = i + 1 : データ(i) = "}"
        i = i + 1 : データ(i) = "td.data_Pink {"
        i = i + 1 : データ(i) = " 		text-align: Center; "
        i = i + 1 : データ(i) = " 		font-weight: normal;"
        i = i + 1 : データ(i) = " 		color: #000000;"
        i = i + 1 : データ(i) = " 		font-size: 100%;"
        i = i + 1 : データ(i) = " 		background-color: #FFDDFF;"
        i = i + 1 : データ(i) = " 		padding: 0.3em ;"
        i = i + 1 : データ(i) = "}"
        i = i + 1 : データ(i) = "td.data_Red {"
        i = i + 1 : データ(i) = " 		text-align: Center; "
        i = i + 1 : データ(i) = " 		font-weight: normal;"
        i = i + 1 : データ(i) = " 		color: #FFFFFF;"
        i = i + 1 : データ(i) = " 		font-size: 100%;"
        i = i + 1 : データ(i) = " 		background-color: #DC143C;"
        i = i + 1 : データ(i) = " 		padding: 0.3em ;"
        i = i + 1 : データ(i) = "}"
        i = i + 1 : データ(i) = "td.data_Blue {"
        i = i + 1 : データ(i) = " 		text-align: Center; "
        i = i + 1 : データ(i) = " 		font-weight: normal;"
        i = i + 1 : データ(i) = " 		color: #FFFFFF;"
        i = i + 1 : データ(i) = " 		font-size: 100%;"
        i = i + 1 : データ(i) = " 		background-color: #0000CD;"
        i = i + 1 : データ(i) = " 		padding: 0.3em ;"
        i = i + 1 : データ(i) = "}"
        i = i + 1 : データ(i) = "td.data_Cyan {"
        i = i + 1 : データ(i) = " 		text-align: Center; "
        i = i + 1 : データ(i) = " 		font-weight: normal;"
        i = i + 1 : データ(i) = " 		color: #000000;"
        i = i + 1 : データ(i) = " 		font-size: 100%;"
        i = i + 1 : データ(i) = " 		background-color: #00FFFF;"
        i = i + 1 : データ(i) = " 		padding: 0.3em ;"
        i = i + 1 : データ(i) = "}"
        i = i + 1 : データ(i) = "td.data {"
        i = i + 1 : データ(i) = " 		text-align: Center; "
        i = i + 1 : データ(i) = " 		font-weight: normal;"
        i = i + 1 : データ(i) = " 		color: #000000;"
        i = i + 1 : データ(i) = " 		font-size: 100%;"
        i = i + 1 : データ(i) = " 		background-color: #FFFFFF;"
        i = i + 1 : データ(i) = " 		padding: 0.3em ;"
        i = i + 1 : データ(i) = "}"
        i = i + 1 : データ(i) = "</style>"



    End Sub



    Private Sub 共通タイトル作成(言語 As String, 採点結果 As 採点結果_C)

        '共通タイトル作成

        i = i + 1 : データ(i) = "<table Class=""table_coomon"" width=""100%"">"
        i = i + 1 : データ(i) = " <thead>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "              <td colspan = ""7"" Class=""Title"" >【Point Detail】 </td> "
        Else
            i = i + 1 : データ(i) = "              <td colspan = ""7"" Class=""Title"" >【詳細得点表】 </td> "
        End If

        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "     </thead>"
        i = i + 1 : データ(i) = "     <tbody>"
        i = i + 1 : データ(i) = "        <tr>"
        i = i + 1 : データ(i) = "              <td colspan = ""7"" Class=""Compname""> " & 採点結果.マスタデータ.A_競技会マスタ.競技会名 & " </t>"
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "              <td Class=""Title01"">Category</td> "
        Else
            i = i + 1 : データ(i) = "              <td Class=""Title01"">区分</td> "
        End If
        'i = i + 1 : データ(i) = "          	   <td colspan = ""6"" Class=""Kubun"" >" & 採点結果.マスタデータ.T_採点進行管理.Get_競技番号_枝番(採点結果.区分番号, 採点結果.ラウンド番号) & " " & 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(採点結果.区分番号) & "</td>"
        i = i + 1 : データ(i) = "          	   <td colspan = ""6"" Class=""Kubun"" >" & 採点結果.マスタデータ.T_採点進行管理.Get_競技番号_枝番(採点結果.区分番号, 採点結果.ラウンド番号) & " " & 採点結果.マスタデータ.BR_カテゴリマスタ.Getカテゴリ名(対象カテゴリ番号) & "</td>"
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "             <td Class=""Title01"">Round</td>"
        Else
            i = i + 1 : データ(i) = "             <td Class=""Title01"">ラウンド</td>"
        End If
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "          	  <td colspan = ""6"" Class=""round"" >" & 採点結果.マスタデータ.Get_ラウンド名_E(採点結果.ラウンド番号） & "</td>"
        Else
            i = i + 1 : データ(i) = "          	  <td colspan = ""6"" Class=""round"" >" & 採点結果.マスタデータ.Get_ラウンド名(採点結果.ラウンド番号） & "</td>"
        End If

        i = i + 1 : データ(i) = "       </tr>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "             <td colspan = ""2"" Class=""Title01"">Round Info.</td>"
            i = i + 1 : データ(i) = "      	      <td colspan = ""5"" Class=""Title01"">Dance</td> "
        Else
            i = i + 1 : データ(i) = "             <td colspan = ""2"" Class=""Title01"">ラウンド情報</td>"
            i = i + 1 : データ(i) = "      	      <td colspan = ""5"" Class=""Title01""></td> "
        End If
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "        <tr>"
        i = i + 1 : データ(i) = "             <td Class=""data01""></td>"     'ヒート数　★

        '出場選手数のカウント
        Dim 出場選手数 As Integer = 0

        For k = 1 To 対象区分数
            出場選手数 = 出場選手数 + 対象採点結果(k).出場選手数
        Next k


        If 言語 = "E" Then
            i = i + 1 : データ(i) = "          	  <td Class=""data01""> " & 出場選手数 & " Couples</td>"
        Else
            i = i + 1 : データ(i) = "          	  <td Class=""data01""> 出場選手数　" & 出場選手数 & "</td>"
        End If
        For d = 1 To 採点結果.種目数
            i = i + 1 : データ(i) = "             <td Class=""data02B"">" & "</td>"
        Next d
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "        <tr>"
        i = i + 1 : データ(i) = "             <td Class=""data01""  ></td>"   'UP数 ★
        i = i + 1 : データ(i) = "         	 <td Class=""data01"">" & 採点結果.採点方式 & "</td>"

        For d = 1 To 採点結果.種目数
            If 言語 = "E" Then
                i = i + 1 : データ(i) = "        	 <td Class=""data02B"">" & "</td>"
            Else
                i = i + 1 : データ(i) = "        	 <td Class=""data02B"">" & "</td>"
            End If
        Next d

        i = i + 1 : データ(i) = "        </tr>”
        i = i + 1 : データ(i) = "     </tbody>”
        i = i + 1 : データ(i) = "</table>”

        '共通タイトル作成　ここまで

    End Sub






    Private Sub 総合結果表作成(言語 As String, 採点結果 As 採点結果_C)


        Dim 次ラウンド無し As Boolean = False


        '****　　次ラウンドのヒート表を読み込む UPの確定のため
        '****　　次ラウンドのラウンド番号の検索
        Dim 次勝ラウンド番号 As String = ""
        Dim 次勝区分番号 = 採点結果.マスタデータ.BR_グループマスタ.Get勝者区分ラウンド(採点結果.区分番号, 採点結果.ラウンド番号, 次勝ラウンド番号)

        If 次勝区分番号 <> "" And 次勝ラウンド番号 <> "" Then

            'ヒート表の検索
            If 採点結果.マスタデータ.E_ヒート表マスタ.FileCheck(次勝区分番号, 次勝ラウンド番号) = False Then

                次ラウンド無し = True

            Else
                'ヒート表ファイルの読込み
                採点結果.マスタデータ.E_ヒート表マスタ.Read(次勝区分番号, 次勝ラウンド番号)
            End If

        Else
            次ラウンド無し = True

        End If

        '**** ここまで









        '総合データ行
        For k = 1 To 対象区分数   '区分数分ループ

            Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(対象採点結果(k).区分番号)

            If 区分 IsNot Nothing Then

                Dim 選手マスタLIST番号 = 区分.使用する選手マスタ
                Dim 種目記号リスト() = Nothing
                Dim 種目数 = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(区分.区分番号, 対象採点結果(k).ラウンド番号, 種目記号リスト)

                採点結果.マスタデータ.E_ヒート表マスタ.Read(区分.区分番号, 対象採点結果(k).ラウンド番号)

                Dim 背番号リスト() = Nothing
                採点結果.マスタデータ.E_ヒート表マスタ.Get_背番号リスト(1, 1, 背番号リスト)

                Dim 背番号_red As String = "0"
                Dim 背番号_blue As String = "0"

                If UBound(背番号リスト) = 1 Then
                    '1ヒート1人しかいない時　　--- 1ヒートが赤　2ヒートが青
                    背番号_red = 背番号リスト(1)

                    採点結果.マスタデータ.E_ヒート表マスタ.Get_背番号リスト(1, 2, 背番号リスト)

                    If UBound(背番号リスト) >= 1 Then
                        背番号_blue = 背番号リスト(1)
                    Else

                    End If
                ElseIf UBound(背番号リスト) >= 2 Then
                    '1ヒートに2人いるとき
                    背番号_red = 背番号リスト(1)
                    背番号_blue = 背番号リスト(2)
                End If



                Dim 選手_Red As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号_red)
                Dim 選手_Blue As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号_blue)

                i = i + 1 : データ(i) = "<table class=""table1""  >"

                '総合タイトル
                i = i + 1 : データ(i) = "     <thead>"
                i = i + 1 : データ(i) = "        <tr>"
                i = i + 1 : データ(i) = "	        <th colspan=""2"">" & 区分.区分番号 & " " & 区分.区分名 & "</th>"
                i = i + 1 : データ(i) = "	        <th Class=""Red"" width=""100"">" & 選手_Red.リーダー表記名 & "</th>"
                i = i + 1 : データ(i) = "	        <th Class=""Blue"" width=""100"">" & 選手_Blue.リーダー表記名 & "</th>"
                i = i + 1 : データ(i) = "	        <th rowspan=""2""></th>"
                i = i + 1 : データ(i) = "	        <th colspan=""4"" Class=""Red"">" & 背番号_red & " " & 選手_Red.リーダー表記名 & "(" & 選手_Red.カップル所属名 & ")" & "</th>"
                i = i + 1 : データ(i) = "	        <th Class=""Green"">総合得点</th>"

                i = i + 1 : データ(i) = "	        <th rowspan=""2""></th>"
                i = i + 1 : データ(i) = "	        <th colspan=""4"" Class=""Blue"">" & 背番号_blue & " " & 選手_Blue.リーダー表記名 & "(" & 選手_Blue.カップル所属名 & ")" & "</th>"
                i = i + 1 : データ(i) = "	        <th Class=""Green"">総合得点</th>"
                i = i + 1 : データ(i) = "        </tr>"

                i = i + 1 : データ(i) = "        <tr>"
                i = i + 1 : データ(i) = "	        <th colspan=""2"" Class=""Green"">合計</th>"

                '赤が勝った時
                If 対象採点結果(k).勝ちFLAG(1) = 1 Or (UP検索(対象採点結果(k).背番号(1), 採点結果) = "UP" And 次ラウンド無し = False) Then
                    i = i + 1 : データ(i) = "	        <th class=""Red"">" & 対象採点結果(k).総合得点(1).ToString("0.000") & "</th>"
                    i = i + 1 : データ(i) = "	        <th Class=""White"">" & 対象採点結果(k).総合得点(2).ToString("0.000") & "</th>"
                Else
                    i = i + 1 : データ(i) = "	        <th class=""White"">" & 対象採点結果(k).総合得点(1).ToString("0.000") & "</th>"
                    i = i + 1 : データ(i) = "	        <th Class=""Blue"">" & 対象採点結果(k).総合得点(2).ToString("0.000") & "</th>"
                End If

                i = i + 1 : データ(i) = "	        <th  Class=""Green"">評価基準</th>"
                i = i + 1 : データ(i) = "	        <th width=""100"" Class=""Green"">技術</th>"
                i = i + 1 : データ(i) = "	        <th width=""100"" Class=""Green"">表現</th>"
                i = i + 1 : データ(i) = "	        <th width=""100"" Class=""Green"">総合性</th>"

                If 対象採点結果(k).勝ちFLAG(1) = 1 Or (UP検索(対象採点結果(k).背番号(1), 採点結果) = "UP" And 次ラウンド無し = False) Then
                    '赤が勝った時
                    i = i + 1 : データ(i) = "	        <th class=""Red"" width=""100"">" & 対象採点結果(k).総合得点(1).ToString("0.000") & "</th>"
                Else
                    i = i + 1 : データ(i) = "	        <th class=""White"" width=""100"">" & 対象採点結果(k).総合得点(1).ToString("0.000") & "</th>"
                End If

                i = i + 1 : データ(i) = "	        <th  Class=""Green"">評価基準</th>"
                i = i + 1 : データ(i) = "	        <th width=""100"" Class=""Green"">技術</th>"
                i = i + 1 : データ(i) = "	        <th width=""100"" Class=""Green"">表現</th>"
                i = i + 1 : データ(i) = "	        <th width=""100"" Class=""Green"">総合性</th>"

                If 対象採点結果(k).勝ちFLAG(1) = 1 Or (UP検索(対象採点結果(k).背番号(1), 採点結果) = "UP" And 次ラウンド無し = False) Then
                    '赤が勝った時
                    i = i + 1 : データ(i) = "	        <th class=""White"" width=""100"">" & 対象採点結果(k).総合得点(2).ToString("0.000") & "</th>"
                Else
                    i = i + 1 : データ(i) = "	        <th class=""Blue"" width=""100"">" & 対象採点結果(k).総合得点(2).ToString("0.000") & "</th>"
                End If

                i = i + 1 : データ(i) = "        </tr>"
                i = i + 1 : データ(i) = "     </thead>"


                'ラウンド毎の結果表示

                i = i + 1 : データ(i) = "     <tbody>"

                For d = 1 To 対象採点結果(k).種目数

                    i = i + 1 : データ(i) = "       <tr>"
                    i = i + 1 : データ(i) = "         <td colspan=""16""></td>"
                    i = i + 1 : データ(i) = "       </tr>"

                    'ジャッジ毎の点数
                    Dim ジャッジ人数 = 対象採点結果(k).種目(1).審判員数


                    '最大点、最小点を算出
                    Dim MAX = New MAX(2, 3)
                    Dim MIN = New MAX(2, 3)


                    '初期値
                    For s = 1 To 2  '選手数
                        For p = 1 To 3   'PCS数
                            MAX.選手(s).PCS(p).点数 = 0
                            MIN.選手(s).PCS(p).点数 = 1000
                        Next p
                    Next s


                    For s = 1 To 2
                        For p = 1 To 3
                            For j = 1 To ジャッジ人数
                                If 対象採点結果(k).種目(d).選手(s).審判(j).PCS素点(p) > MAX.選手(s).PCS(p).点数 Then
                                    MAX.選手(s).PCS(p).点数 = 対象採点結果(k).種目(d).選手(s).審判(j).PCS素点(p)
                                End If

                                If 対象採点結果(k).種目(d).選手(s).審判(j).PCS素点(p) < MIN.選手(s).PCS(p).点数 Then
                                    MIN.選手(s).PCS(p).点数 = 対象採点結果(k).種目(d).選手(s).審判(j).PCS素点(p)
                                End If
                            Next j
                        Next p
                    Next s
                    '最大点、最小点を算出　ここまで



                    For j = 1 To ジャッジ人数

                        i = i + 1 : データ(i) = "       <tr>"
                        If j = 1 Then
                            i = i + 1 : データ(i) = "         <td rowspan=""" & ジャッジ人数 + 2 & """ class=""Yellow"">" & 対象採点結果(k).種目記号(d) & "</td>"
                        End If

                        i = i + 1 : データ(i) = "         <td class=""Yellow"">" & 対象採点結果(k).種目(d).選手(1).審判(j).ジャッジ表記名 & "</td>"

                        Dim 赤得点 As Decimal = 対象採点結果(k).種目(d).選手(1).審判(j).表示用PCS合計点
                        Dim 青得点 As Decimal = 対象採点結果(k).種目(d).選手(2).審判(j).表示用PCS合計点

                        If 赤得点 > 青得点 Then
                            i = i + 1 : データ(i) = "         <td class=""data_Red"">" & 赤得点 & "</td>"
                            i = i + 1 : データ(i) = "         <td class=""data"">" & 青得点 & "</td>"

                        ElseIf 赤得点 < 青得点 Then
                            i = i + 1 : データ(i) = "         <td class=""data"">" & 赤得点 & "</td>"
                            i = i + 1 : データ(i) = "         <td class=""data_Blue"">" & 青得点 & "</td>"

                        Else
                            i = i + 1 : データ(i) = "         <td class=""data"">" & 赤得点 & "</td>"
                            i = i + 1 : データ(i) = "         <td class=""data"">" & 青得点 & "</td>"

                        End If

                        If j = 1 Then
                            i = i + 1 : データ(i) = "         <td rowspan=""" & ジャッジ人数 + 2 & """></td>"
                        End If

                        '赤の詳細得点

                        i = i + 1 : データ(i) = "         <td class=""Yellow"">" & 対象採点結果(k).種目(d).選手(1).審判(j).ジャッジ記号 & "</td>"

                        For p = 1 To 3
                            Dim 得点 As Decimal = 対象採点結果(k).種目(d).選手(1).審判(j).PCS素点(p)

                            If MAX.選手(1).PCS(p).点数 = 得点 Then
                                i = i + 1 : データ(i) = "         <td class=""data"">" & 得点 & "△</td>"
                            ElseIf MIN.選手(1).PCS(p).点数 = 得点 Then
                                i = i + 1 : データ(i) = "         <td class=""data"">" & 得点 & "▼</td>"
                            Else
                                i = i + 1 : データ(i) = "         <td class=""data"">" & 得点 & "</td>"
                            End If
                        Next p


                        If 赤得点 > 青得点 Then
                            i = i + 1 : データ(i) = "         <td class=""data_Red"">" & 赤得点 & "</td>"
                        Else
                            i = i + 1 : データ(i) = "         <td class=""data"">" & 赤得点 & "</td>"
                        End If
                        If j = 1 Then
                            i = i + 1 : データ(i) = "         <td rowspan=""" & ジャッジ人数 + 2 & """></td>"
                        End If

                        '青の詳細得点
                        i = i + 1 : データ(i) = "         <td class=""Yellow"">" & 対象採点結果(k).種目(d).選手(1).審判(j).ジャッジ記号 & "</td>"

                        For p = 1 To 3
                            Dim 得点 As Decimal = 対象採点結果(k).種目(d).選手(2).審判(j).PCS素点(p)

                            If MAX.選手(2).PCS(p).点数 = 得点 Then
                                i = i + 1 : データ(i) = "         <td class=""data"">" & 得点 & "△</td>"
                            ElseIf MIN.選手(2).PCS(p).点数 = 得点 Then
                                i = i + 1 : データ(i) = "         <td class=""data"">" & 得点 & "▼</td>"
                            Else
                                i = i + 1 : データ(i) = "         <td class=""data"">" & 得点 & "</td>"
                            End If
                        Next p



                        If 赤得点 > 青得点 Then
                            i = i + 1 : データ(i) = "         <td class=""data"">" & 青得点 & "</td>"
                        ElseIf 赤得点 < 青得点 Then
                            i = i + 1 : データ(i) = "         <td class=""data_Blue"">" & 青得点 & "</td>"
                        Else
                            i = i + 1 : データ(i) = "         <td class=""data"">" & 青得点 & "</td>"
                                End If


                                i = i + 1 : データ(i) = "       </tr>"

                    Next j

                        '＝＝＝＝＝加点減点行

                        i = i + 1 : データ(i) = "       　<tr>"
                    i = i + 1 : データ(i) = "       　　<td class=""Green"">減点/加点</td>"

                    Dim 減点_Red As Decimal = 0
                    Dim 減点_Blue As Decimal = 0
                    For r = 1 To UBound(対象採点結果(k).種目(d).選手(1).種目各減点)
                        減点_Red = 減点_Red + 対象採点結果(k).種目(d).選手(1).種目各減点(r)
                    Next r
                    For r = 1 To UBound(対象採点結果(k).種目(d).選手(2).種目各減点)
                        減点_Blue = 減点_Blue + 対象採点結果(k).種目(d).選手(2).種目各減点(r)
                    Next r

                    i = i + 1 : データ(i) = "       　　<td class=""Green"">" & 減点_Red & "</td>"
                    i = i + 1 : データ(i) = "       　　<td class=""Green"">" & 減点_Blue & "</td>"

                    i = i + 1 : データ(i) = "       　　<td class=""Green"">減点/加点</td>"
                    i = i + 1 : データ(i) = "       　　<td colspan=""3""  class=""Green"">" & 減点_Red & "</td>"
                    i = i + 1 : データ(i) = "       　　<td class=""Green"">" & 減点_Red & "</td>"

                    i = i + 1 : データ(i) = "       　　<td class=""Green"">減点/加点</td>"
                    i = i + 1 : データ(i) = "       　　<td colspan=""3""  class=""Green"">" & 減点_Blue & "</td>"
                    i = i + 1 : データ(i) = "       　　<td class=""Green"">" & 減点_Blue & "</td>"

                    i = i + 1 : データ(i) = "       </tr>"

                    '＝＝＝＝＝トータル行

                    i = i + 1 : データ(i) = "       　<tr>"
                    i = i + 1 : データ(i) = "       　　<td class=""Green"">Total</td>"

                    Dim 赤種目得点 = 対象採点結果(k).種目(d).選手(1).種目得点
                    Dim 青種目得点 = 対象採点結果(k).種目(d).選手(2).種目得点

                    If 赤種目得点 > 青種目得点 Then
                        i = i + 1 : データ(i) = "	        <td class=""data_Red""><b>" & 赤種目得点.ToString("0.000") & "</b></td>"
                        i = i + 1 : データ(i) = "	        <td class=""data""><b>" & 青種目得点.ToString("0.000") & "</b></td>"
                    ElseIf 赤種目得点 < 青種目得点 Then
                        i = i + 1 : データ(i) = "	        <td class=""data""><b>" & 赤種目得点.ToString("0.000") & "</b></td>"
                        i = i + 1 : データ(i) = "	        <td class=""data_Blue""><b>" & 青種目得点.ToString("0.000") & "</b></td>"
                    Else
                        '同点の時
                        i = i + 1 : データ(i) = "	        <td class=""data""><b>" & 赤種目得点.ToString("0.000") & "</b></td>"
                        i = i + 1 : データ(i) = "	        <td class=""data""><b>" & 青種目得点.ToString("0.000") & "</b></td>"
                    End If

                    i = i + 1 : データ(i) = "       　　<td class=""Green"">平均</td>"



                    Dim PCS平均 As Decimal = 0
                    For p = 1 To 3
                        Dim PCS合計 As Decimal = 0
                        For j = 1 To 対象採点結果(k).種目(1).審判員数
                            PCS合計 = PCS合計 + 対象採点結果(k).種目(d).選手(1).審判(j).PCS素点(p)
                        Next j
                        PCS合計 = PCS合計 / ジャッジ人数       '2022/08/25  ジャッジ人数を３から可変に変更
                        i = i + 1 : データ(i) = "       　　<td class=""Green"">" & PCS合計.ToString("0.000") & "</td>"

                        PCS平均 = PCS平均 + PCS合計
                    Next p

                    PCS平均 = PCS平均 / 3　　　　　　　'2022/08/25  ジャッジ人数を３から可変に変更　　ここはジャッジ人数ではなくPCS数
                    i = i + 1 : データ(i) = "       　　<td class=""Green"">" & PCS平均.ToString("0.000") & "</td>"


                    i = i + 1 : データ(i) = "       　　<td class=""Green"">平均</td>"

                    PCS平均 = 0
                    For p = 1 To 3
                        Dim PCS合計 As Decimal = 0
                        For j = 1 To 対象採点結果(k).種目(1).審判員数
                            PCS合計 = PCS合計 + 対象採点結果(k).種目(d).選手(2).審判(j).PCS素点(p)
                        Next j
                        PCS合計 = PCS合計 / ジャッジ人数　　　　　'2022/08/25  ジャッジ人数を３から可変に変更
                        i = i + 1 : データ(i) = "       　　<td class=""Green"">" & PCS合計.ToString("0.000") & "</td>"

                        PCS平均 = PCS平均 + PCS合計
                    Next p

                    PCS平均 = PCS平均 / 3　　　　　'2022/08/25  ジャッジ人数を３から可変に変更　　ここはジャッジ人数ではなくPCS数　
                    i = i + 1 : データ(i) = "       　　<td class=""Green"">" & PCS平均.ToString("0.000") & "</td>"


                Next d


                i = i + 1 : データ(i) = "     </tbody>"
            End If

        Next k

        i = i + 1 : データ(i) = "</table>"


        i = i + 1 : データ(i) = ""


    End Sub


    Private Sub 詳細結果表作成(言語 As String, 採点結果 As 採点結果_C)

        Dim PCS数 As Integer = 採点結果.マスタデータ.J_新審判設定.GetPCS数
        Dim 減点数 As Integer = 採点結果.マスタデータ.J_新審判設定.Get減点項目数
        Dim 審判員数woR As Integer = 0   'レフリーを除く審判員数
        For j = 1 To 採点結果.種目(1).審判員数
            If 採点結果.種目(1).選手(1).審判(j).ジャッジRole = "1" Or 採点結果.種目(1).選手(1).審判(j).ジャッジRole = "L" Then
                審判員数woR = 審判員数woR + 1
            End If

        Next j

        For d = 1 To 採点結果.種目数

            If 採点結果.種目(d).選手(1).種目得点 > 0 Or 採点結果.種目(d).選手(1).失格FLAG = True Then

                i = i + 1 : データ(i) = "<p Class=""pagechange"">"    '改ページ



                If 言語 = "E" Then
                    i = i + 1 : データ(i) = "<h3> " & 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(採点結果.区分番号) & " " & 採点結果.マスタデータ.Z_システム設定.Get_種目名称(採点結果.種目記号(d)).種目名_E & "  Detail Result </h3>"
                Else
                    i = i + 1 : データ(i) = "<h3> " & 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(採点結果.区分番号) & " " & 採点結果.マスタデータ.Z_システム設定.Get_種目名称(採点結果.種目記号(d)).種目名_E & "  詳細結果 </h3>"
                End If

                i = i + 1 : データ(i) = "<table class=""table_det"">"

                'タイトル1行目
                i = i + 1 : データ(i) = "   <thead>"
                i = i + 1 : データ(i) = "   <th colspan=""2"" style=""background-color: #ccffcc;""></th>"
                i = i + 1 : データ(i) = "   <th colspan=""" & PCS数 * 2 & """style=""text-align: center;"">PCS</th>"
                i = i + 1 : データ(i) = "   <th colspan=""" & 減点数 & """style=""text-align: center;"">Reduction</th>"
                i = i + 1 : データ(i) = "   <th colspan=""2"" style=""text-align: center;"">Points</th>"
                i = i + 1 : データ(i) = "   <th colspan=""2"" style=""text-align: center;"">Total</th>"

                'タイトル2行目
                i = i + 1 : データ(i) = "     <tr>"
                i = i + 1 : データ(i) = "      <th>No</th>"
                i = i + 1 : データ(i) = "      <th>Judge</th>"
                For p = 1 To PCS数
                    i = i + 1 : データ(i) = "      <th colspan=""2"">" & 採点結果.マスタデータ.J_新審判設定.PCS設定(p).PCS項目名 & "</th>"
                    ' i = i + 1 : データ(i) = "      <th> </th>"
                Next p
                For r = 1 To 減点数
                    If 言語 = "E" Then
                        i = i + 1 : データ(i) = "      <th class=""redname"">" & 採点結果.マスタデータ.J_新審判設定.減点設定(r).減点項目名英 & "</th>"
                    Else
                        i = i + 1 : データ(i) = "      <th class=""redname"">" & 採点結果.マスタデータ.J_新審判設定.減点設定(r).減点項目名 & "</th>"

                    End If
                Next r
                i = i + 1 : データ(i) = "      <th>PCS</th>"
                i = i + 1 : データ(i) = "      <th>Redu.</th>"
                i = i + 1 : データ(i) = "      <th>Total</th>"
                i = i + 1 : データ(i) = "      <th>RANK</th>"

                i = i + 1 : データ(i) = "     </tr>"
                i = i + 1 : データ(i) = "   </thead>"

                '選手毎の詳細得点
                i = i + 1 : データ(i) = " <tbody>"
                For s = 1 To 採点結果.出場選手数

                    i = i + 1 : データ(i) = "   <tr>"
                    i = i + 1 : データ(i) = "   <td rowspan=""" & 審判員数woR + 1 & """>" & 採点結果.背番号(s) & "</td>"


                    Dim jj As Integer = 1

                    For j = 1 To 採点結果.種目(1).審判員数

                        If 採点結果.種目(1).選手(1).審判(j).ジャッジRole <> "R" Then

                            If jj >= 2 Then
                                i = i + 1 : データ(i) = "   <tr>"
                            End If


                            'ジャッジ記号を半角に変換
                            i = i + 1 : データ(i) = "   <td class=""judge"">" & StrConv(採点結果.種目(1).選手(1).審判(j).ジャッジ記号, VbStrConv.Narrow) & "</td> "

                            For p = 1 To PCS数
                                If 採点結果.種目(d).選手(s).審判(j).ジャッジPCS採点対象(p) = 1 Then
                                    '採点対象PCSの時
                                    If 採点結果.種目(d).選手(s).審判(j).PCS無効FLAG(p) = True Then
                                        'PCS1.5以上離れているとき
                                        i = i + 1 : データ(i) = "<td class=""pcs"" style=""background-color: #FFE4E1;"" >" & 採点結果.種目(d).選手(s).審判(j).PCS素点(p).ToString("0.00") & "</td>"
                                        i = i + 1 : データ(i) = "<td class=""pcs2""  style=""background-color: #FFE4E1;"">N/A</td>"
                                    Else
                                        i = i + 1 : データ(i) = "<td class=""pcs"">" & 採点結果.種目(d).選手(s).審判(j).PCS素点(p).ToString("0.00") & "</td>"
                                        i = i + 1 : データ(i) = "<td class=""pcs2""></td>"
                                    End If
                                Else
                                    '採点対象外PCSの時はハイフン
                                    i = i + 1 : データ(i) = "<td class=""pcs"">-</td>"
                                    i = i + 1 : データ(i) = "<td class=""pcs2""></td>"
                                End If
                            Next p


                            For r = 1 To 減点数
                                If 採点結果.種目(d).選手(s).審判(j).減点素点(r) = 0 Then
                                    '減点なし
                                    i = i + 1 : データ(i) = "<td  class=""redu"">0</td>"
                                Else
                                    '減点あり
                                    i = i + 1 : データ(i) = "<td  class=""redu"" style= ""color: #cc0000;""　>" & 採点結果.種目(d).選手(s).審判(j).減点素点(r) & "</td>"
                                End If
                            Next r

                            If j = 1 Then
                                'PCS合計点
                                Dim PCS得点 As Double = 0
                                Dim 減点合計 As Double = 0
                                For pp = 1 To PCS数

                                    'PCS倍率の取得
                                    Dim 倍率 As Double = 採点結果.マスタデータ.J_新審判設定.PCS設定(pp).倍率

                                    PCS得点 = PCS得点 + (採点結果.種目(d).選手(s).種目各PCS得点(pp) * 倍率)
                                Next pp
                                For rr = 2 To 減点数
                                    減点合計 = 減点合計 + 採点結果.種目(d).選手(s).種目各減点(rr)
                                Next rr
                                i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""pcs"">" & PCS得点.ToString("0.000") & "</td>"
                                '減点合計点
                                i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""redu"">" & 減点合計 & "</td>"

                                'Total
                                i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""pcstotal"">" & 採点結果.種目(d).選手(s).種目得点.ToString("0.000") & "</td>"
                                '順位
                                i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""rank"">" & 採点結果.種目(d).選手(s).種目順位表記 & "</td>"

                                i = i + 1 : データ(i) = "   </tr>"

                            End If


                            jj = jj + 1
                        End If
                    Next j



                    'PCSの合計点
                    i = i + 1 : データ(i) = "   <tr>"
                    i = i + 1 : データ(i) = "  <td class=""Judge"">Total</td>"

                    For p = 1 To PCS数

                        'PCS倍率の取得
                        Dim 倍率 As Double = 採点結果.マスタデータ.J_新審判設定.PCS設定(p).倍率

                        If 倍率 = 1 Then
                            i = i + 1 : データ(i) = "<td colspan=""2"" class=""pcs""> " & 採点結果.種目(d).選手(s).種目各PCS得点(p).ToString("0.000") & "</td>"

                        Else
                            Dim PCS点数 As Double = 採点結果.種目(d).選手(s).種目各PCS得点(p) * 倍率
                            i = i + 1 : データ(i) = "<td colspan=""2"" class=""pcs""> " & PCS点数.ToString("0.000") & " (x" & 倍率.ToString("0.0") & ")</td>"

                        End If

                    Next p

                    '減点の点数
                    For r = 1 To 減点数
                        i = i + 1 : データ(i) = "<td  class=""redu"" 　>" & 採点結果.種目(d).選手(s).種目各減点(r) & "</td>"

                    Next r


                    i = i + 1 : データ(i) = "   </tr>"

                Next s

                i = i + 1 : データ(i) = "</table>"

            End If

        Next d


    End Sub




    Private Sub ファイル書出し(ByVal パス名 As String, ByVal File名 As String, ByVal データ() As String)
        '===========================
        '概要　HTMLを作成する(SC_xxxxxx.csv)
        '入力　新File名,データ
        '出力　なし
        '===========================


        Dim NewFilename As String = パス名 & "\" & File名

        '_00_初期化()

        'ファイル書き出し
        If System.IO.Directory.Exists(パス名) Then
            'フォルダーは存在している
        Else
            'フォルダーが存在しないので新規作成
            System.IO.Directory.CreateDirectory(パス名)
        End If





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

    '背番号を渡すして、次ラウンドのヒート表に背番号がある時は"UP"を返す。無い時は"  "を返す
    '条件 ヒート表マスタのReadが終わっていること
    Private Function UP検索(背番号 As String, 採点結果 As 採点結果_C) As String


        Dim 採点結果Temp As 採点結果_C

        採点結果Temp = 採点結果

        '****　　次ラウンドのヒート表を読み込む UPの確定のため
        '****　　次ラウンドのラウンド番号の検索
        Dim 次勝ラウンド番号 As String = ""
        Dim 次勝区分番号 = 採点結果Temp.マスタデータ.BR_グループマスタ.Get勝者区分ラウンド(採点結果.区分番号, 採点結果.ラウンド番号, 次勝ラウンド番号)

        If 次勝区分番号 <> "" And 次勝ラウンド番号 <> "" Then

            'ヒート表の検索
            If 採点結果Temp.マスタデータ.E_ヒート表マスタ.FileCheck(次勝区分番号, 次勝ラウンド番号) = False Then

                '次ラウンド無し = True

            Else
                'ヒート表ファイルの読込み
                採点結果Temp.マスタデータ.E_ヒート表マスタ.Read(次勝区分番号, 次勝ラウンド番号)
            End If

        Else
            '次ラウンド無し = True

        End If




        Dim rc As String = "  "

        For t = 1 To 採点結果Temp.マスタデータ.E_ヒート表マスタ.登録済みレコード数
            If CInt(採点結果Temp.マスタデータ.E_ヒート表マスタ.リスト(t).背番号) = CInt(背番号) Then
                rc = "UP"
                t = 採点結果Temp.マスタデータ.E_ヒート表マスタ.登録済みレコード数
            End If
        Next t

        Return rc
    End Function


    Private Class MAX

        Public 選手() As M選手

        Sub New(選手数, PCS数)

            ReDim 選手(選手数)
            For s = 1 To 選手数
                選手(s) = New M選手(PCS数)
            Next s


        End Sub
    End Class

    Private Class MIN

        Public 選手() As M選手

        Sub New(選手数, PCS数)

            ReDim 選手(選手数)
            For s = 1 To 選手数
                選手(s) = New M選手(PCS数)
            Next s

        End Sub
    End Class

    Private Class M選手

        Public PCS() As MPCS

        Sub New(PCS数)

            ReDim PCS(PCS数)
            For p = 1 To PCS数
                PCS(p) = New MPCS
            Next p

        End Sub
    End Class

    Private Class MPCS

        Public 点数 As Decimal

    End Class


End Class

