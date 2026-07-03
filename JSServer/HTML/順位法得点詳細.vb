Public Class 順位法得点詳細

    '順位法の得点詳細をHTMLで書き出す

    Public Sub New()


    End Sub

    Public Sub CreateHTML(採点結果 As 採点結果_C, LOCAL指定 As String)

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
        i = i + 1 : データ(i) = "         	 <td Class=""data01"">" & 採点結果.採点方式 & "</td>"

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

        i = i + 1 : データ(i) = "	        <th>決定理由</th>"
        i = i + 1 : データ(i) = "	        <th>上位加算</th>"
        i = i + 1 : データ(i) = "	        <th>再スケ過半数</th>"
        i = i + 1 : データ(i) = "	        <th>再スケ多数決</th>"
        i = i + 1 : データ(i) = "	        <th>再スケ上位加算</th>"
        i = i + 1 : データ(i) = "	        <th>再スケ下位比較</th>"

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

                    i = i + 1 : データ(i) = "         <td rowspan=""2"" class=""Rank""  >" & 採点結果.スケーティング結果_総合選手(s).規程9_合計点 & "</td>"
                    For d = 1 To 採点結果.種目数
                        i = i + 1 : データ(i) = "         <td rowspan=""2"" style=""text-align: center;""  >" & 採点結果.種目(d).選手(s).スケーティング結果_選手.順位点数 & "</td>"
                    Next d

                    i = i + 1 : データ(i) = "         <td rowspan=""2"" style=""text-align: center;""  >" & 採点結果.スケーティング結果_総合選手(s).決定根拠 & "</td>"
                    i = i + 1 : データ(i) = "         <td rowspan=""2"" style=""text-align: center;""  >" & 採点結果.スケーティング結果_総合選手(s).規程10b_上位加算 & "</td>"
                    i = i + 1 : データ(i) = "         <td rowspan=""2"" style=""text-align: center;""  >" & 採点結果.スケーティング結果_総合選手(s).規程5_過半数順位 & "</td>"
                    i = i + 1 : データ(i) = "         <td rowspan=""2"" style=""text-align: center;""  >" & 採点結果.スケーティング結果_総合選手(s).規程6_多数決 & "</td>"
                    i = i + 1 : データ(i) = "         <td rowspan=""2"" style=""text-align: center;""  >" & 採点結果.スケーティング結果_総合選手(s).規程7a_上位加算 & "</td>"
                    i = i + 1 : データ(i) = "         <td rowspan=""2"" style=""text-align: center;""  >" & 採点結果.スケーティング結果_総合選手(s).規程7b_下位比較 & "</td>"



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
        Dim PCS数 As Integer = 採点結果.マスタデータ.J_新審判設定.GetPCS数
        Dim 減点数 As Integer = 採点結果.マスタデータ.J_新審判設定.Get減点項目数
        Dim 審判員数woR As Integer = 0   'レフリーを除く審判員数
        For j = 1 To 採点結果.種目(1).審判員数
            If 採点結果.種目(1).選手(1).審判(j).ジャッジRole = "1" Or 採点結果.種目(1).選手(1).審判(j).ジャッジRole = "L" Then
                審判員数woR = 審判員数woR + 1
            End If

        Next j

        For d = 1 To 採点結果.種目数
            i = i + 1 : データ(i) = "<p Class=""pagechange"">"    '改ページ
            If 言語 = "E" Then
                i = i + 1 : データ(i) = "<h3> " & 採点結果.マスタデータ.Z_システム設定.Get_種目名称(採点結果.種目記号(d)).種目名_E & "  Detail Result </h3>"
            Else
                i = i + 1 : データ(i) = "<h3> " & 採点結果.マスタデータ.Z_システム設定.Get_種目名称(採点結果.種目記号(d)).種目名_E & "  詳細結果 </h3>"
            End If




        Next d




        i = i + 1 : データ(i) = "<table class=""table_det"">"
        'タイトル1行目
        i = i + 1 : データ(i) = "   <thead>"
        If 採点結果.ラウンド番号 <> "300" And 採点結果.ラウンド番号 <> "400" Then
            i = i + 1 : データ(i) = "   <th colspan=""4"" style=""background-color: #ccffcc;""></th>"
        Else
            i = i + 1 : データ(i) = "   <th colspan=""3"" style=""background-color: #ccffcc;""></th>"
        End If
        For d = 1 To 採点結果.種目数
            i = i + 1 : データ(i) = "   <th colspan=""" & 審判員数woR & """ style=""text-align: center;"">" & 採点結果.マスタデータ.Z_システム設定.Get_種目名称(採点結果.種目記号(d)).種目名_E & " </th>"
        Next d

        'タイトル2行目
        i = i + 1 : データ(i) = "     <tr>"
        i = i + 1 : データ(i) = "      <th>No</th>"
        i = i + 1 : データ(i) = "      <th>Point</th>"

        If 採点結果.ラウンド番号 <> "300" And 採点結果.ラウンド番号 <> "400" Then
            i = i + 1 : データ(i) = "      <th>UP</th>"
        End If

        i = i + 1 : データ(i) = "      <th>Rank</th>"
        For d = 1 To 採点結果.種目数
            For j = 1 To 審判員数woR
                i = i + 1 : データ(i) = "   <th>" & StrConv(採点結果.種目(1).選手(1).審判(j).ジャッジ記号, VbStrConv.Narrow) & "</th> "
            Next j
        Next d

        i = i + 1 : データ(i) = "     </tr>"
        i = i + 1 : データ(i) = "   </thead>"



        'データ行
        '選手毎の詳細得点
        i = i + 1 : データ(i) = " <tbody>"
        For s = 1 To 採点結果.出場選手数

            i = i + 1 : データ(i) = "   <tr>"

            If 採点結果.ラウンド番号 <> "300" And 採点結果.ラウンド番号 <> "400" Then
                If 次ラウンド無し = False Then
                    If UP検索(採点結果.背番号(s), 採点結果) = "UP" Then
                        i = i + 1 : データ(i) = "   <td class=""data02B"" >" & 採点結果.背番号(s) & "</td>"
                        i = i + 1 : データ(i) = "   <td class=""data02B"" >" & 採点結果.総合得点(s) & "</td>"
                        i = i + 1 : データ(i) = "   <td class=""Country"">UP</td>"
                        i = i + 1 : データ(i) = "   <td class=""data02B"" >" & 採点結果.総合順位表記(s) & "</td>"

                    Else
                        i = i + 1 : データ(i) = "   <td  >" & 採点結果.背番号(s) & "</td>"
                        i = i + 1 : データ(i) = "   <td class=""data02"" >" & 採点結果.総合得点(s) & "</td>"
                        i = i + 1 : データ(i) = "   <td ></td>"
                        i = i + 1 : データ(i) = "   <td class=""data02"" >" & 採点結果.総合順位表記(s) & "</td>"
                    End If
                Else
                    i = i + 1 : データ(i) = "   <td  >" & 採点結果.背番号(s) & "</td>"
                    i = i + 1 : データ(i) = "   <td class=""data02"" >" & 採点結果.総合得点(s) & "</td>"
                    i = i + 1 : データ(i) = "   <td ></td>"
                    i = i + 1 : データ(i) = "   <td class=""data02"" >" & 採点結果.総合順位表記(s) & "</td>"
                End If
            Else

                i = i + 1 : データ(i) = "   <td  >" & 採点結果.背番号(s) & "</td>"
                i = i + 1 : データ(i) = "   <td class=""data02"" >" & 採点結果.総合得点(s) & "</td>"
                i = i + 1 : データ(i) = "   <td class=""data02"" >" & 採点結果.総合順位表記(s) & "</td>"

            End If





            For d = 1 To 採点結果.種目数
                For j = 1 To 審判員数woR
                    If 採点結果.種目(d).選手(s).審判(j).素点 = 1 Then
                        i = i + 1 : データ(i) = "   <td class=""data02B"" >＊</td>"
                    Else
                        i = i + 1 : データ(i) = "   <td style=""text-align: center;"" >ー</td>"
                    End If
                Next j
            Next d

            i = i + 1 : データ(i) = "</tr>"

        Next s

        i = i + 1 : データ(i) = " </tbody>"
        i = i + 1 : データ(i) = "</table>"


        '＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝



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
            If 採点結果.種目(1).選手(1).審判(j).ジャッジRole = "R" Then

                If 言語 = "E" Then
                    i = i + 1 : データ(i) = "<h3> " & "Referee " & 採点結果.種目(1).選手(1).審判(j).ジャッジ表記名 & "</h3>"

                Else
                    i = i + 1 : データ(i) = "<h3> " & "レフリー " & 採点結果.種目(1).選手(1).審判(j).ジャッジ表記名 & "</h3>"
                End If

            Else
                i = i + 1 : データ(i) = "<h3> " & StrConv(採点結果.種目(1).選手(1).審判(j).ジャッジ記号, VbStrConv.Narrow) & " " & 採点結果.種目(1).選手(1).審判(j).ジャッジ表記名 & "</h3>"

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
    Private Function UP検索(背番号 As String, 採点結果 As 採点結果_C) As String
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
