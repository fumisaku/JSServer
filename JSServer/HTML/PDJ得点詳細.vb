Public Class PDJ得点詳細

    'PDJの得点詳細をHTMLで書き出す

    Public Sub New()


    End Sub

    Public Sub CreateHTML(採点結果 As 採点結果_V2, LOCAL指定 As String)

        Dim 言語 As String = 採点結果.マスタデータ.Z_システム設定.言語

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(採点結果.区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

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
        i = i + 1 : データ(i) = "          	   <td colspan = ""6"" Class=""Kubun"" >" & 採点結果.マスタデータ.T_採点進行管理.Get_競技番号_枝番(採点結果.区分番号, 採点結果.ラウンド番号) & " " & 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(採点結果.区分番号) & "</td>"
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
            i = i + 1 : データ(i) = "      	      <td colspan = ""5"" Class=""Title01"">競技種目</td> "
        End If
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "        <tr>"
        i = i + 1 : データ(i) = "             <td Class=""data01""></td>"     'ヒート数　★
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "          	  <td Class=""data01""> " & 採点結果.出場選手数 & " Couples</td>"
        Else
            i = i + 1 : データ(i) = "          	  <td Class=""data01""> 出場" & 採点結果.出場選手数 & "組</td>"
        End If
        For d = 1 To 採点結果.種目数
            i = i + 1 : データ(i) = "             <td Class=""data02B"">" & 採点結果.種目記号(d) & "</td>"
        Next d
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "        <tr>"
        i = i + 1 : データ(i) = "             <td Class=""data01""  ></td>"   'UP数 ★

        Dim 採点方式名 As String = 採点結果.採点方式
        If 採点結果.採点方式 = "PDJ10A" Then
            採点方式名 = "AJS3.0J(Type D)"
        ElseIf 採点結果.採点方式 = "PDJ10B" Then
            採点方式名 = "AJS3.0J"
        ElseIf 採点結果.採点方式 = "PDJ20A" Then
            採点方式名 = "AJS3.1J(Type D)"
        ElseIf 採点結果.採点方式 = "PDJ20B" Then
            採点方式名 = "AJS3.1J"
        ElseIf 採点結果.採点方式 = "PDJ30A" Then
            採点方式名 = "AJS3.2J(Type D)"
        ElseIf 採点結果.採点方式 = "PDJ30B" Then
            採点方式名 = "AJS3.2J"


        End If


        i = i + 1 : データ(i) = "         	 <td Class=""data01"">" & 採点方式名 & "</td>"

        For d = 1 To 採点結果.種目数
            If 言語 = "E" Then
                i = i + 1 : データ(i) = "        	 <td Class=""data02B"">" & 採点結果.マスタデータ.D_種目マスタ.Get_SG種別表記名_E(採点結果.区分番号, 採点結果.ラウンド番号, d) & "</td>"
            Else
                i = i + 1 : データ(i) = "        	 <td Class=""data02B"">" & 採点結果.マスタデータ.D_種目マスタ.Get_SG種別表記名(採点結果.区分番号, 採点結果.ラウンド番号, d) & "</td>"
            End If
        Next d

        i = i + 1 : データ(i) = "        </tr>”
        i = i + 1 : データ(i) = "     </tbody>”
        i = i + 1 : データ(i) = "</table>”

        '共通タイトル作成　ここまで



        '総合結果表

        i = i + 1 : データ(i) = "<table class=""table1""  width=""100%""  >"

        '総合タイトル
        i = i + 1 : データ(i) = "     <thead>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "	        <th>Place</th>"

            If 採点結果.ラウンド番号 <> "300" And 採点結果.ラウンド番号 <> "400" Then
                i = i + 1 : データ(i) = "	        <th>UP</th>"
            End If
            i = i + 1 : データ(i) = "	        <th>No</th>"
            i = i + 1 : データ(i) = "	        <th>Name</th>"
            'i = i + 1 : データ(i) = "	        <th>Partner</th>"
            i = i + 1 : データ(i) = "	        <th>Country</th>"
            i = i + 1 : データ(i) = "	        <th>Total</th>"
        Else
            i = i + 1 : データ(i) = "	        <th>順位</th>"
            If 採点結果.ラウンド番号 <> "300" And 採点結果.ラウンド番号 <> "400" Then
                i = i + 1 : データ(i) = "	        <th>UP</th>"
            End If
            i = i + 1 : データ(i) = "	        <th>背番号</th>"
            i = i + 1 : データ(i) = "	        <th>リーダー</th>"
            'i = i + 1 : データ(i) = "	        <th>パートナー</th>"
            i = i + 1 : データ(i) = "	        <th>所属</th>"
            i = i + 1 : データ(i) = "	        <th>Total</th>"
        End If
        For d = 1 To 採点結果.種目数
            i = i + 1 : データ(i) = "	        <th>" & 採点結果.種目記号(d) & "</th>"
        Next d
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "     </thead>"

        '総合データ行
        i = i + 1 : データ(i) = "     <tbody>"

        '順位順に表示

        '****　　次ラウンドのヒート表を読み込む UPの確定のため
        '****　　次ラウンドのラウンド番号の検索
        Dim 次ラウンド As C_ラウンド = 採点結果.マスタデータ.C_ラウンドマスタ.Get_次ラウンドClass(採点結果.区分番号, 採点結果.ラウンド番号)

        Dim 次ラウンド無し As Boolean = False
        If 次ラウンド IsNot Nothing Then
            'ヒート表の検索
            If 採点結果.マスタデータ.E_ヒート表マスタ.FileCheck(採点結果.区分番号, 次ラウンド.ラウンド番号) = False Then

                次ラウンド無し = True

            Else
                'ヒート表ファイルの読込み
                採点結果.マスタデータ.E_ヒート表マスタ.Read(採点結果.区分番号, 次ラウンド.ラウンド番号)
            End If
        Else
            次ラウンド無し = True
        End If

        '**** ここまで



        For 順位 = 1 To 採点結果.出場選手数
            For s = 1 To 採点結果.出場選手数
                If 採点結果.総合順位表記(s) = 順位 Then
                    i = i + 1 : データ(i) = "        <tr>"
                    i = i + 1 : データ(i) = "         <td rowspan=""2"" class=""rank0""  >" & 順位 & "</td>"

                    If 採点結果.ラウンド番号 <> "300" And 採点結果.ラウンド番号 <> "400" Then
                        If 次ラウンド無し = False Then
                            i = i + 1 : データ(i) = "         <td rowspan=""2"" class=""Country"">" & UP検索(採点結果.背番号(s), 採点結果) & "</td>"
                        Else
                            i = i + 1 : データ(i) = "         <td rowspan=""2"" class=""Country"">" & "</td>"
                        End If

                    End If


                    i = i + 1 : データ(i) = "         <td rowspan=""2"" class=""No"" >" & 採点結果.背番号(s) & "</td>"
                    Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 採点結果.背番号(s))

                    i = i + 1 : データ(i) = "         <td class=""name"">" & 選手.リーダー表記名 & "</td>"
                    'i = i + 1 : データ(i) = "         <td>" & 選手.パートナ表記名 & "</td>"
                    i = i + 1 : データ(i) = "         <td rowspan=""2"" class=""Country"">" & 選手.カップル所属名 & "</td>"

                    i = i + 1 : データ(i) = "         <td rowspan=""2"" class=""TotalScore""  >" & 採点結果.総合得点(s).ToString("0.000") & "</td>"
                    For d = 1 To 採点結果.種目数
                        i = i + 1 : データ(i) = "         <td rowspan=""2"" class=""DanceScore""  >" & 採点結果.種目(d).選手結果(s).種目得点.ToString("0.000") & "</td>"
                    Next d
                    i = i + 1 : データ(i) = "        </tr>"

                    i = i + 1 : データ(i) = "        <tr>"
                    i = i + 1 : データ(i) = "         <td class=""name"">" & 選手.パートナ表記名 & "</td>"
                    i = i + 1 : データ(i) = "        </tr>"

                End If
            Next s
        Next 順位
        i = i + 1 : データ(i) = "     </tbody>"
        i = i + 1 : データ(i) = "</table>"


        i = i + 1 : データ(i) = ""


        '======種目毎結果
        Dim ソロ種目記号 As String = ""
        For d = 1 To 採点結果.種目数
            Dim 種目 As D_種目 = 採点結果.マスタデータ.D_種目マスタ.Get_種目Class(採点結果.区分番号, 採点結果.ラウンド番号, d)

            If 種目.SG種別 = "S" Then
                ソロ種目記号 = 種目.種目記号
            End If
        Next d


        Dim 課題数 As Integer = 採点結果.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)


        Dim PCS数 As Integer = 採点結果.マスタデータ.J_新審判設定.GetPCS数
        Dim 減点数 As Integer = 採点結果.マスタデータ.J_新審判設定.Get減点項目数
        Dim 審判員数woR As Integer = 0   'レフリーを除く審判員数

        審判員数woR = 採点結果.種目(1).Get_一般審判員数



        For d = 1 To 採点結果.種目数

            i = i + 1 : データ(i) = "<p Class=""pagechange"">"    '改ページ
            If 言語 = "E" Then
                i = i + 1 : データ(i) = "<h3> " & 採点結果.マスタデータ.Z_システム設定.Get_種目名称(採点結果.種目記号(d)).種目名_E & "  Detail Result </h3>"
            Else
                i = i + 1 : データ(i) = "<h3> " & 採点結果.マスタデータ.Z_システム設定.Get_種目名称(採点結果.種目記号(d)).種目名_E & "  詳細結果 </h3>"
            End If

            i = i + 1 : データ(i) = "<table class=""table_det"">"

            'タイトル1行目
            i = i + 1 : データ(i) = "   <thead>"
            i = i + 1 : データ(i) = "   <th colspan=""2"" style=""background-color: #ccffcc;""></th>"

            If 課題数 >= 1 Then
                i = i + 1 : データ(i) = "   <th colspan=""" & 課題数 * 3 & """style=""text-align: center;"">TES</th>"
            End If
            i = i + 1 : データ(i) = "   <th colspan=""" & PCS数 * 2 & """style=""text-align: center;"">PCS</th>"
            i = i + 1 : データ(i) = "   <th colspan=""" & 減点数 & """style=""text-align: center;"">Reduction</th>"

            If 課題数 >= 1 Then
                i = i + 1 : データ(i) = "   <th colspan=""3"" style=""text-align: center;"">Points</th>"
            Else
                i = i + 1 : データ(i) = "   <th colspan=""2"" style=""text-align: center;"">Points</th>"
            End If
            i = i + 1 : データ(i) = "   <th colspan=""2"" style=""text-align: center;"">Total</th>"

            'タイトル2行目
            i = i + 1 : データ(i) = "     <tr>"
            i = i + 1 : データ(i) = "      <th rowspan=""2"" >No</th>"
            i = i + 1 : データ(i) = "      <th rowspan=""2"" >Judge</th>"
            If 課題数 >= 1 Then
                For k = 1 To 課題数
                    i = i + 1 : データ(i) = "      <th colspan=""3"" >課題" & k & "</th>"
                Next k
            End If


            For p = 1 To PCS数
                i = i + 1 : データ(i) = "      <th colspan=""2"" rowspan=""2"">" & 採点結果.マスタデータ.J_新審判設定.PCS設定(p).PCS項目名 & "</th>"
                ' i = i + 1 : データ(i) = "      <th> </th>"
            Next p
            For r = 1 To 減点数
                If 言語 = "E" Then
                    i = i + 1 : データ(i) = "      <th class=""redname"" rowspan=""2"">" & 採点結果.マスタデータ.J_新審判設定.減点設定(r).減点項目名英 & "</th>"
                Else
                    i = i + 1 : データ(i) = "      <th class=""redname"" rowspan=""2"">" & 採点結果.マスタデータ.J_新審判設定.減点設定(r).減点項目名 & "</th>"

                End If
            Next r

            If 課題数 >= 1 Then
                i = i + 1 : データ(i) = "      <th rowspan=""2"">TES</th>"
            Else

            End If
            i = i + 1 : データ(i) = "      <th rowspan=""2"">PCS</th>"
            i = i + 1 : データ(i) = "      <th rowspan=""2"">Redu.</th>"
            i = i + 1 : データ(i) = "      <th rowspan=""2"">Total</th>"
            i = i + 1 : データ(i) = "      <th rowspan=""2"">RANK</th>"

            i = i + 1 : データ(i) = "     </tr>"



            'タイトル3行目
            i = i + 1 : データ(i) = "     <tr>"
            If 課題数 >= 1 Then
                For k = 1 To 課題数
                    i = i + 1 : データ(i) = "      <th >BV</th>"
                    i = i + 1 : データ(i) = "      <th >FP</th>"
                    i = i + 1 : データ(i) = "      <th >GOE</th>"
                Next k
            End If
            i = i + 1 : データ(i) = "     </tr>"


            i = i + 1 : データ(i) = "   </thead>"

            '選手毎の詳細得点
            i = i + 1 : データ(i) = " <tbody>"
            For s = 1 To 採点結果.出場選手数

                i = i + 1 : データ(i) = "   <tr>"
                i = i + 1 : データ(i) = "   <td rowspan=""" & 審判員数woR + 1 & """>" & 採点結果.背番号(s) & "</td>"


                Dim jj As Integer = 1
                ' For j = 1 To 審判員数woR
                For j = 1 To 採点結果.種目(1).審判員数

                    If 採点結果.種目(1).審判員(j).ジャッジタイプ <> "R" And 採点結果.種目(1).審判員(j).ジャッジタイプ <> "T" Then

                        If jj >= 2 Then
                            i = i + 1 : データ(i) = "   <tr>"
                        End If


                        'ジャッジ記号を半角に変換
                        i = i + 1 : データ(i) = "   <td class=""judge"">" & StrConv(採点結果.種目(1).審判員(j).ジャッジ記号, VbStrConv.Narrow) & "</td> "


                        'TES
                        If 課題数 >= 1 Then
                            For k = 1 To 課題数

                                If jj = 1 Then
                                    'BV
                                    Dim BV_Temp As String = 採点結果.種目(d).選手結果(s).TES得点(k).TES_Base.ToString("0.00")
                                    If BV_Temp >= "5.50" Then
                                        i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR & """ class=""redu"" >" & 採点結果.種目(d).選手結果(s).TES得点(k).TES_Base.ToString("0.00") & "<br>UP </td>"

                                    ElseIf BV_Temp <= "4.50" Then
                                        i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR & """ class=""redu"" >" & 採点結果.種目(d).選手結果(s).TES得点(k).TES_Base.ToString("0.00") & "<br>DN </td>"

                                    Else
                                        i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR & """ class=""redu"" >" & 採点結果.種目(d).選手結果(s).TES得点(k).TES_Base.ToString("0.00") & "</td>"

                                    End If


                                    'FP
                                    Dim fp_Text As String = ""
                                    For fp = 1 To 採点結果.マスタデータ.J_新審判設定.GetTES減点数
                                        If 採点結果.種目(d).選手結果(s).TES得点(k).TES減点(fp).TES減点 <> 0 Then
                                            fp_Text = fp_Text & 採点結果.種目(d).選手結果(s).TES得点(k).TES減点(fp).TES減点項目名 & "<br>"
                                            fp_Text = fp_Text & 採点結果.種目(d).選手結果(s).TES得点(k).TES減点(fp).TES減点.ToString & "<br>"
                                        End If
                                    Next fp
                                    i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR & """  class=""redu"" >" & fp_Text & "</td>"

                                End If

                                'GOE
                                i = i + 1 : データ(i) = "<td class=""pcs"" >" & 採点結果.種目(d).選手結果(s).審判員結果(j).GOE素点(k).GOE素点.ToString("0.0") & "</td>"

                            Next k
                        End If


                        'PCS

                        For p = 1 To PCS数

                            If Strings.Left(採点結果.採点方式, 5) = "PDJ10" Then

                                If 採点結果.種目(d).選手結果(s).審判員結果(j).PCS素点(p).PCS採点対象FLAG = True Then
                                    '採点対象PCSの時
                                    If 採点結果.種目(d).選手結果(s).審判員結果(j).PCS素点(p).PCS無効FLAG = True Then
                                        'PCS1.5以上離れているとき
                                        i = i + 1 : データ(i) = "<td class=""pcs"" style=""background-color: #FFE4E1;"" >" & 採点結果.種目(d).選手結果(s).審判員結果(j).PCS素点(p).PCS素点.ToString("0.00") & "</td>"
                                        i = i + 1 : データ(i) = "<td class=""pcs2""  style=""background-color: #FFE4E1;"">N/A</td>"
                                    Else
                                        i = i + 1 : データ(i) = "<td class=""pcs"">" & 採点結果.種目(d).選手結果(s).審判員結果(j).PCS素点(p).PCS素点.ToString("0.00") & "</td>"
                                        i = i + 1 : データ(i) = "<td class=""pcs2""></td>"
                                    End If
                                Else
                                    '採点対象外PCSの時はハイフン
                                    i = i + 1 : データ(i) = "<td class=""pcs"">-</td>"
                                    i = i + 1 : データ(i) = "<td class=""pcs2""></td>"
                                End If

                            ElseIf Strings.Left(採点結果.採点方式, 5) = "PDJ20" Or Strings.Left(採点結果.採点方式, 5) = "PDJ30" Then

                                If 採点結果.種目(d).選手結果(s).審判員結果(j).PCS素点(p).PCS採点対象FLAG = True Then

                                    If 採点結果.種目(d).選手結果(s).審判員結果(j).PCS素点(p).乖離度 = 1 Then
                                        '中間値（乖離度=1)の時は何も表示しない
                                        i = i + 1 : データ(i) = "<td class=""pcs"">" & 採点結果.種目(d).選手結果(s).審判員結果(j).PCS素点(p).PCS素点.ToString("0.00") & "</td>"
                                        i = i + 1 : データ(i) = "<td class=""pcs2""></td>"
                                    Else
                                        i = i + 1 : データ(i) = "<td class=""pcs"" >" & 採点結果.種目(d).選手結果(s).審判員結果(j).PCS素点(p).PCS素点.ToString("0.00") & "</td>"
                                        i = i + 1 : データ(i) = "<td class=""pcs2"">(" & 採点結果.種目(d).選手結果(s).審判員結果(j).PCS素点(p).乖離度.ToString("0.00") & ")</td>"

                                    End If
                                Else
                                    '採点対象外PCSの時はハイフン
                                    i = i + 1 : データ(i) = "<td class=""pcs"">-</td>"
                                    i = i + 1 : データ(i) = "<td class=""pcs2""></td>"

                                End If

                            End If

                        Next p

                        If jj = 1 Then

                            'ソロ、グループで減点項目数が違う

                            Dim 種目減点番号 As Integer = 0
                            Dim SG種別 As String = 採点結果.マスタデータ.D_種目マスタ.Get_種目Class(採点結果.区分番号, 採点結果.ラウンド番号, d).SG種別

                            For r = 1 To 減点数

                                If 採点結果.マスタデータ.J_新審判設定.減点設定(r).SGM種別.Contains(SG種別) Then
                                    種目減点番号 = 種目減点番号 + 1

                                    If 採点結果.種目(d).選手結果(s).一般減点(種目減点番号).一般減点 = 0 Then
                                        '減点なし
                                        i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR & """ class=""redu"">0</td>"
                                    Else
                                        '減点あり
                                        i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR & """ class=""redu"" style= ""color: #cc0000;""　>" & 採点結果.種目(d).選手結果(s).一般減点(種目減点番号).一般減点 & "</td>"
                                    End If

                                Else
                                    'その種目では減点対象項目ではない場合
                                    i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR & """ class=""redu"">0</td>"
                                End If

                            Next r

                            'TES合計点
                            Dim TES得点 As Decimal = 0
                            For k = 1 To 課題数

                                'BVが0点超の時のみ、減点も加味する。
                                If 採点結果.種目(d).選手結果(s).TES得点(k).TES_Base > 0 Then

                                    TES得点 = TES得点 + 採点結果.種目(d).選手結果(s).TES得点(k).TES_Base

                                    For fp = 1 To 採点結果.マスタデータ.J_新審判設定.GetTES減点数
                                        TES得点 = TES得点 + 採点結果.種目(d).選手結果(s).TES得点(k).TES減点(fp).TES減点
                                    Next fp
                                End If


                                TES得点 = TES得点 + 採点結果.種目(d).選手結果(s).GOE得点(k).GOE得点
                            Next k

                            'PCS合計点
                            Dim PCS得点 As Decimal = 0
                            Dim 減点合計 As Decimal = 0
                            For pp = 1 To PCS数
                                PCS得点 = PCS得点 + 採点結果.種目(d).選手結果(s).PCS得点(pp).PCS得点
                            Next pp


                            For rr = 1 To 減点数
                                If 採点結果.種目(d).選手結果(s).一般減点(rr).一般減点 < 0 Then
                                    減点合計 = 減点合計 + 採点結果.種目(d).選手結果(s).一般減点(rr).一般減点
                                End If
                            Next rr



                            'TES合計点
                            If 課題数 >= 1 Then
                                i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""pcs"">" & TES得点.ToString("0.000") & "</td>"

                            End If

                            'PCS合計点
                            i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""pcs"">" & PCS得点.ToString("0.000") & "</td>"
                            '減点合計点
                            i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""redu"">" & 減点合計 & "</td>"

                            'Total
                            i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""pcstotal"">" & 採点結果.種目(d).選手結果(s).種目得点.ToString("0.000") & "</td>"
                            '順位
                            i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""rank"">" & 採点結果.種目(d).選手結果(s).種目順位表記 & "</td>"

                            i = i + 1 : データ(i) = "   </tr>"

                        End If


                        jj = jj + 1
                    End If
                Next j



                '合計点
                i = i + 1 : データ(i) = "   <tr>"
                i = i + 1 : データ(i) = "  <td class=""Judge"">Total</td>"

                'TESの合計点
                For k = 1 To 課題数


                    i = i + 1 : データ(i) = "<td colspan=""1"" class=""pcs""> " & 採点結果.種目(d).選手結果(s).TES得点(k).TES_Base.ToString("0.000") & "</td>"

                    Dim TES減点 As Decimal = 0

                    If 採点結果.種目(d).選手結果(s).TES得点(k).TES_Base > 0 Then

                        For fp = 1 To 採点結果.マスタデータ.J_新審判設定.GetTES減点数
                            TES減点 = TES減点 + 採点結果.種目(d).選手結果(s).TES得点(k).TES減点(fp).TES減点
                        Next fp
                    End If


                    i = i + 1 : データ(i) = "<td colspan=""1"" class=""pcs""> " & TES減点.ToString("0.000") & "</td>"


                    i = i + 1 : データ(i) = "<td colspan=""1"" class=""pcs""> " & 採点結果.種目(d).選手結果(s).GOE得点(k).GOE得点.ToString("0.000") & "</td>"

                Next k


                'PCSの合計点
                For p = 1 To PCS数
                    i = i + 1 : データ(i) = "<td colspan=""2"" class=""pcs""> " & 採点結果.種目(d).選手結果(s).PCS得点(p).PCS得点.ToString("0.000") & "</td>"
                Next p

                i = i + 1 : データ(i) = "   </tr>"

            Next s

            i = i + 1 : データ(i) = "</table>"

        Next d


        'ジャッジ名を表示
        i = i + 1 : データ(i) = "<p Class=""pagechange"">"    '改ページ
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "<h3> " & "  Adjudicators </h3>"
        Else
            i = i + 1 : データ(i) = "<h3> " & "  審判員 </h3>"
        End If

        i = i + 1 : データ(i) = ""

        For j = 1 To 採点結果.種目(1).審判員数
            'ジャッジ記号を半角に変換
            If 採点結果.種目(1).審判員(j).ジャッジタイプ = "R" Then

                If 言語 = "E" Then
                    i = i + 1 : データ(i) = "<h3> " & "Referee " & 採点結果.種目(1).審判員(j).ジャッジ表記名 & "</h3>"

                Else
                    i = i + 1 : データ(i) = "<h3> " & "レフリー " & 採点結果.種目(1).審判員(j).ジャッジ表記名 & "</h3>"

                End If

            ElseIf 採点結果.種目(1).審判員(j).ジャッジタイプ = "T" Then
                i = i + 1 : データ(i) = "<h3> " & "技術判定員 " & 採点結果.種目(1).審判員(j).ジャッジ表記名 & "</h3>"


            Else
                i = i + 1 : データ(i) = "<h3> " & StrConv(採点結果.種目(1).審判員(j).ジャッジ記号, VbStrConv.Narrow) & " " & 採点結果.種目(1).審判員(j).ジャッジ表記名 & "</h3>"

            End If



        Next j






        Dim filename As String = "Result_" & 採点結果.区分番号 & "_" & 採点結果.ラウンド番号 & ".html"

        If LOCAL指定 = "LOCAL" Then

            ファイル書出し(採点結果.マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult", filename, データ)
        Else
            ファイル書出し(採点結果.マスタデータ.Z_システム設定.HTML_filepath, filename, データ)

        End If


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
    Private Function UP検索(背番号 As String, 採点結果 As 採点結果_V2) As String
        Dim rc As String = "  "

        For i = 1 To 採点結果.マスタデータ.E_ヒート表マスタ.登録済みレコード数
            If CInt(採点結果.マスタデータ.E_ヒート表マスタ.リスト(i).背番号) = CInt(背番号) Then
                rc = "UP"
                i = 採点結果.マスタデータ.E_ヒート表マスタ.登録済みレコード数
            End If
        Next i

        Return rc
    End Function

End Class
