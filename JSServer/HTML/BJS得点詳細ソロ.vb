Public Class BJS得点詳細ソロ
    'ブレイキンのソロ順位（プレセレクション）の表示

    'BJSの得点詳細をHTMLで書き出す

    Private データ(10000) As String
    Private i As Integer

    Private 対象区分数 As Integer
    Private 対象区分リスト(50) As String
    Private 対象ラウンド番号 As String
    Private 対象採点結果(50) As 採点結果_C

    Private 対象カテゴリ番号 As String

    Public Sub New()


    End Sub


    Public Sub CreateHTML(採点結果 As 採点結果_C, LOCAL指定 As String)



        Dim 言語 As String = 採点結果.マスタデータ.Z_システム設定.言語


        '=======

        'BJPRE用　順位点の計算
        順位点計算(採点結果)


        i = 0

        If LOCAL指定 = "LOCAL" Then
            データ(i) = "<link rel=""stylesheet"" type=""text/css"" href=""result.css"">"
        Else
            データ(i) = ""
        End If


        共通タイトル作成(言語, 採点結果)

        総合結果表作成(言語, 採点結果)



        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(採点結果.区分番号)

        If 区分 IsNot Nothing Then

            If 採点結果.種目数 >= 1 Then

                If 言語 = "E" Then   '区分のタイトル
                    i = i + 1 : データ(i) = "<h3 style=""color: blue""> ■ " & 区分.区分表記名 & "  Detail Result ■</h3>"
                Else
                    i = i + 1 : データ(i) = "<h3 style=""color: blue""> ■ " & 区分.区分表記名 & "  詳細結果 ■</h3>"
                End If


                詳細結果表作成(言語, 採点結果)
            End If

        End If



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

    Public Sub CreateHTML＿OLD(採点結果 As 採点結果_C, LOCAL指定 As String)


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

            If 対象区分数 = 50 Then
                MsgBox("対象区分数数が最大値５０を超えました。")
                Exit Sub
            End If
        Next i



        Dim 言語 As String = 採点結果.マスタデータ.Z_システム設定.言語



        'ソロの結果を作成  ====これは不要なはず、なぜこの機能を作ったのか不明。。。


        Dim ソロ順位() As ソロ順位_C = Nothing
        Dim 出場選手数 As Integer = 0

        '   ソロ順位に背番号と点数を入力
        For k = 1 To 対象区分数   '区分数分ループ
            Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(対象採点結果(k).区分番号)

            If 区分 IsNot Nothing Then

                For s = 1 To 対象採点結果(k).出場選手数

                    出場選手数 = 出場選手数 + 1

                    If ソロ順位 Is Nothing Then
                        ReDim ソロ順位(1)
                    Else
                        ReDim Preserve ソロ順位(UBound(ソロ順位) + 1)
                    End If

                    ソロ順位(出場選手数) = New ソロ順位_C

                    ソロ順位(出場選手数).背番号 = 対象採点結果(k).背番号(s)
                    ソロ順位(出場選手数).得点 = 対象採点結果(k).総合得点(s)

                Next s
            End If
        Next k

        '  順位を入力

        Dim Temp得点() As Decimal
        ReDim Temp得点(出場選手数)

        Dim 最大得点 As Decimal = -1
        Dim 最大得点選手番号 As Integer = 0
        Dim 順位番号 As Double = 1
        Dim 順位番号_同点有り As Double = 順位番号

        For s = 1 To 出場選手数
            Temp得点(s) = ソロ順位(s).得点
        Next s


        For 順位 = 1 To 出場選手数

            最大得点 = -1
            For s = 1 To 出場選手数
                If 最大得点 < Temp得点(s) Then
                    最大得点 = Temp得点(s)
                    最大得点選手番号 = s
                End If
            Next s



            '同点が無いか確認
            For s = 1 To 出場選手数
                If 最大得点 = ソロ順位(s).得点 Then
                    ソロ順位(s).順位番号 = 順位番号
                    順位番号 = 順位番号 + 1

                    ソロ順位(s).順位_同点有り = 順位番号_同点有り

                    Temp得点(s) = -1
                End If
            Next s

            順位番号_同点有り = 順位番号

            順位 = 順位番号 - 1
        Next 順位

        '=======

        'BJPRE用　順位点の計算
        順位点計算(採点結果)


        i = 0

        If LOCAL指定 = "LOCAL" Then
            データ(i) = "<link rel=""stylesheet"" type=""text/css"" href=""result.css"">"
        Else
            データ(i) = ""
        End If


        共通タイトル作成(言語, 採点結果)

        総合結果表作成(言語, 採点結果, ソロ順位)


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

    Private Class ジャッジ順位点_C
        Public ジャッジ記号
        Public ジャッジ毎得点() As Decimal
        Public ジャッジ毎順位点() As Decimal

    End Class

    Private ジャッジ順位点() As ジャッジ順位点_C

    Private 順位点() As Decimal   'これが順位点

    Private Sub 順位点計算(採点結果 As 採点結果_C)

        Dim 人数 As Integer = 0

        '===BJPR用=　ジャッジ順位点の算出====
        'Dim ジャッジ毎得点() As Decimal
        'Dim ジャッジ毎順位点() As Decimal

        'Dim 順位点() As Decimal   'これが順位点


        ReDim 順位点(採点結果.出場選手数)


        ReDim ジャッジ順位点(採点結果.種目(1).審判員数)



        For d = 1 To 採点結果.種目数
            For j = 1 To 採点結果.種目(d).審判員数

                ジャッジ順位点(j) = New ジャッジ順位点_C

                '
                ReDim ジャッジ順位点(j).ジャッジ毎得点(採点結果.出場選手数)

                For s = 1 To 採点結果.出場選手数
                    ジャッジ順位点(j).ジャッジ毎得点(s) = 採点結果.種目(d).選手(s).審判(j).素点
                Next s

                '====ソート　ジャッジ毎得点を基に、ジャッジ毎順位点を算出
                ReDim ジャッジ順位点(j).ジャッジ毎順位点(採点結果.出場選手数)

                Dim 順位 As Integer = 1

                Do While 順位 <= 採点結果.出場選手数
                    Dim 最大値 As Decimal = 0
                    人数 = 0

                    '最大値を探す
                    For s = 1 To 採点結果.出場選手数
                        If ジャッジ順位点(j).ジャッジ毎得点(s) > 最大値 Then
                            最大値 = ジャッジ順位点(j).ジャッジ毎得点(s)
                        End If
                    Next s

                    '最大値と同じ点数を持っている選手に順位を付けて、その得点を -10にする。
                    For s = 1 To 採点結果.出場選手数
                        If ジャッジ順位点(j).ジャッジ毎得点(s) = 最大値 Then
                            ジャッジ順位点(j).ジャッジ毎順位点(s) = 順位
                            人数 = 人数 + 1
                            ジャッジ順位点(j).ジャッジ毎得点(s) = -10
                        End If
                    Next s

                    '次の順位
                    順位 = 順位 + 人数
                Loop

                'ジャッジ毎順位点を、順位点に入れる
                For s = 1 To 採点結果.出場選手数
                    順位点(s) = 順位点(s) + ジャッジ順位点(j).ジャッジ毎順位点(s)
                Next s


            Next j

        Next d
        '===============

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
        i = i + 1 : データ(i) = "          	   <td colspan = ""6"" Class=""Kubun"" >" & 採点結果.マスタデータ.T_採点進行管理.Get_競技番号_枝番(採点結果.区分番号, 採点結果.ラウンド番号) & " " & 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(採点結果.区分番号) & "</td>"
        'i = i + 1 : データ(i) = "          	   <td colspan = ""6"" Class=""Kubun"" >" & 採点結果.マスタデータ.T_採点進行管理.Get_競技番号_枝番(採点結果.区分番号, 採点結果.ラウンド番号) & " " & 採点結果.マスタデータ.BR_カテゴリマスタ.Getカテゴリ名(対象カテゴリ番号) & "</td>"
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

        'ソロ競技か、対戦競技か判別
        'ソロ競技は、FISE広島の予選ラウンド

        Dim 次ラウンド無し As Boolean = False

        Dim 競技種別 As String
        If 採点結果.マスタデータ.D_種目マスタ.Get_SG種別表記名(採点結果.区分番号, 採点結果.ラウンド番号, 1) = "ソロ" Or 採点結果.マスタデータ.D_種目マスタ.Get_SG種別表記名(採点結果.区分番号, 採点結果.ラウンド番号, 1) = "全員" Then
            競技種別 = "ソロ"


            '****　　ラウンド　xx9 のヒート表を読み込む UPの確定のため
            '****　　
            Dim 結果ラウンド番号 As String = Strings.Left(採点結果.ラウンド番号, 2) & "9"

            採点結果.マスタデータ.E_ヒート表マスタ.Read(採点結果.区分番号, 結果ラウンド番号)



            '**** ここまで




        Else
            競技種別 = "対戦"


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




        End If


        i = i + 1 : データ(i) = "<table class=""table1""  width=""100%""  >"

        '総合タイトル
        i = i + 1 : データ(i) = "     <thead>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then

            If 競技種別 <> "ソロ" Then
                i = i + 1 : データ(i) = "	        <th></th>"
                i = i + 1 : データ(i) = "	        <th>Kubun</th>"
            End If
            i = i + 1 : データ(i) = "	        <th>No</th>"
            i = i + 1 : データ(i) = "	        <th>Result</th>"
            i = i + 1 : データ(i) = "	        <th>Name</th>"
            i = i + 1 : データ(i) = "	        <th>Country</th>"
            i = i + 1 : データ(i) = "	        <th>UP</th>"
            i = i + 1 : データ(i) = "	        <th>Points</th>"
            i = i + 1 : データ(i) = "	        <th>RankPoint</th>"
        Else
            If 競技種別 <> "ソロ" Then
                i = i + 1 : データ(i) = "	        <th>区分</th>"
                i = i + 1 : データ(i) = "	        <th>区分名</th>"
            End If
            i = i + 1 : データ(i) = "	        <th>選手No</th>"
            i = i + 1 : データ(i) = "	        <th>順位</th>"
            i = i + 1 : データ(i) = "	        <th>選手名</th>"
            i = i + 1 : データ(i) = "	        <th>所属</th>"
            i = i + 1 : データ(i) = "	        <th>UP</th>"
            i = i + 1 : データ(i) = "	        <th>総合得点</th>"
            i = i + 1 : データ(i) = "	        <th>順位点</th>"
        End If

        For d = 1 To 採点結果.種目数
            i = i + 1 : データ(i) = "	        <th>" & 採点結果.種目記号(d) & "</th>"
        Next d
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "     </thead>"


        '総合データ行
        '順位順に表示する。

        For 順位番号 = 1 To UBound(採点結果.総合順位番号)
            '順位番号の該当を探す
            For s = 1 To UBound(採点結果.総合順位番号)
                If 採点結果.総合順位番号(s) = 順位番号 Then

                    '背番号が見つかったら
                    i = i + 1 : データ(i) = "     <tbody>"

                    Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(採点結果.区分番号)

                    If 区分 IsNot Nothing Then
                        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

                        i = i + 1 : データ(i) = "        <tr>"


                        Dim 種目記号リスト() = Nothing
                        Dim 種目数 = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(採点結果.区分番号, 採点結果.ラウンド番号, 種目記号リスト)


                        Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 採点結果.背番号(s))



                        If UP検索(採点結果.背番号(s), 採点結果) = "UP" Then
                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & 採点結果.背番号(s) & "</td>"
                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & 採点結果.総合順位表記(s) & " </td>"  '結果 同点あり
                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & 選手.リーダー表記名 & "</td>"
                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & 選手.カップル所属名 & "</td>"
                            i = i + 1 : データ(i) = "         <td class=""TotalScore"">" & "UP" & "</td>"   'UP
                        Else
                            i = i + 1 : データ(i) = "         <td class=""data01"">" & 採点結果.背番号(s) & "</td>"
                            i = i + 1 : データ(i) = "         <td class=""pcs"">" & 採点結果.総合順位表記(s) & "</td>"  '結果
                            i = i + 1 : データ(i) = "         <td class=""name"">" & 選手.リーダー表記名 & "</td>"
                            i = i + 1 : データ(i) = "         <td class=""Country"">" & 選手.カップル所属名 & "</td>"
                            i = i + 1 : データ(i) = "         <td class=""TotalScore"">" & "</td>"   'UP
                        End If


                        i = i + 1 : データ(i) = "         <td class=""TotalScore"">" & 採点結果.総合得点(s).ToString("0.000") & "</td>"  '総合得点
                        i = i + 1 : データ(i) = "         <td class=""TotalScore"">" & 順位点(s).ToString("0") & "</td>"  '順位点

                        For r = 1 To 採点結果.種目数
                            i = i + 1 : データ(i) = "         <td class=""DanceScore"">" & 採点結果.種目(r).選手(s).種目得点.ToString("0.000") & "</td>"
                        Next r

                        i = i + 1 : データ(i) = "        </tr>"


                        i = i + 1 : データ(i) = "     </tbody>"



                        s = UBound(採点結果.総合順位番号)
                    End If
                End If

            Next s
        Next 順位番号



        i = i + 1 : データ(i) = "</table>"


        i = i + 1 : データ(i) = ""






    End Sub


    Private Sub 総合結果表作成(言語 As String, 採点結果 As 採点結果_C, ソロ順位() As ソロ順位_C)

        'ソロ競技か、対戦競技か判別
        'ソロ競技は、FISE広島の予選ラウンド

        Dim 次ラウンド無し As Boolean = False

        Dim 競技種別 As String
        If 採点結果.マスタデータ.D_種目マスタ.Get_SG種別表記名(採点結果.区分番号, 採点結果.ラウンド番号, 1) = "ソロ" Or 採点結果.マスタデータ.D_種目マスタ.Get_SG種別表記名(採点結果.区分番号, 採点結果.ラウンド番号, 1) = "全員" Then
            競技種別 = "ソロ"


            '****　　ラウンド　xx9 のヒート表を読み込む UPの確定のため
            '****　　
            Dim 結果ラウンド番号 As String = Strings.Left(採点結果.ラウンド番号, 2) & "9"

            採点結果.マスタデータ.E_ヒート表マスタ.Read(採点結果.区分番号, 結果ラウンド番号)



            '**** ここまで




        Else
            競技種別 = "対戦"


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




        End If


        i = i + 1 : データ(i) = "<table class=""table1""  width=""100%""  >"

        '総合タイトル
        i = i + 1 : データ(i) = "     <thead>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then

            If 競技種別 <> "ソロ" Then
                i = i + 1 : データ(i) = "	        <th></th>"
                i = i + 1 : データ(i) = "	        <th>Kubun</th>"
            End If
            i = i + 1 : データ(i) = "	        <th>No</th>"
            i = i + 1 : データ(i) = "	        <th>Result</th>"
            i = i + 1 : データ(i) = "	        <th>Name</th>"
            i = i + 1 : データ(i) = "	        <th>Country</th>"
            i = i + 1 : データ(i) = "	        <th>UP</th>"
            i = i + 1 : データ(i) = "	        <th>Points</th>"
            i = i + 1 : データ(i) = "	        <th>RankPoint</th>"
        Else
            If 競技種別 <> "ソロ" Then
                i = i + 1 : データ(i) = "	        <th>区分</th>"
                i = i + 1 : データ(i) = "	        <th>区分名</th>"
            End If
            i = i + 1 : データ(i) = "	        <th>背番号</th>"
            i = i + 1 : データ(i) = "	        <th>結果</th>"
            i = i + 1 : データ(i) = "	        <th>選手名</th>"
            i = i + 1 : データ(i) = "	        <th>所属</th>"
            i = i + 1 : データ(i) = "	        <th>UP</th>"
            i = i + 1 : データ(i) = "	        <th>総合得点</th>"
            i = i + 1 : データ(i) = "	        <th>順位点</th>"
        End If

        For d = 1 To 採点結果.種目数
            i = i + 1 : データ(i) = "	        <th>" & 採点結果.種目記号(d) & "</th>"
        Next d
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "     </thead>"


        '総合データ行
        '順位順に表示する。


        For 順位 = 1 To UBound(採点結果.総合順位番号)
            For 選手番号 = 1 To UBound(ソロ順位)

                If ソロ順位(選手番号).順位番号 = 順位 Then
                    Dim 背番号 As String = ソロ順位(選手番号).背番号

                    '該当の区分を見つける
                    For k = 1 To 対象区分数
                        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(対象採点結果(k).区分番号)
                        If 区分 IsNot Nothing Then
                            For s = 1 To 対象採点結果(k).出場選手数
                                If 対象採点結果(k).背番号(s) = ソロ順位(選手番号).背番号 Then

                                    '背番号が見つかったら
                                    i = i + 1 : データ(i) = "     <tbody>"


                                    Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

                                    i = i + 1 : データ(i) = "        <tr>"

                                    If 競技種別 <> "ソロ" Then
                                        i = i + 1 : データ(i) = "         <td rowspan=""1"" class=""pcs"">" & 対象採点結果(k).区分番号 & "</td>"
                                        i = i + 1 : データ(i) = "         <td rowspan=""1"" class=""pcs"">" & 区分.区分表記名 & "</td>"
                                    End If


                                    Dim 種目記号リスト() = Nothing
                                    Dim 種目数 = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(対象採点結果(k).区分番号, 採点結果.ラウンド番号, 種目記号リスト)



                                    Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 対象採点結果(k).背番号(s))


                                    If 競技種別 <> "ソロ" Then

                                        '勝ち負け判定　
                                        If 対象採点結果(k).勝ちFLAG(s) = 1 Or (UP検索(対象採点結果(k).背番号(s), 採点結果) = "UP" And 次ラウンド無し = False) Then
                                            '勝った時
                                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & 対象採点結果(k).背番号(s) & "</td>"
                                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & ソロ順位(選手番号).順位_同点有り & " </td>"  '結果
                                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & 選手.リーダー表記名 & "</td>"
                                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & 選手.カップル所属名 & "</td>"

                                        Else
                                            '負けた時
                                            i = i + 1 : データ(i) = "         <td class=""data01"">" & 対象採点結果(k).背番号(s) & "</td>"
                                            i = i + 1 : データ(i) = "         <td class=""data01"">" & ソロ順位(選手番号).順位_同点有り & "</td>"  '結果
                                            i = i + 1 : データ(i) = "         <td class=""data01"">" & 選手.リーダー表記名 & "</td>"
                                            i = i + 1 : データ(i) = "         <td class=""data01"">" & 選手.カップル所属名 & "</td>"

                                        End If

                                    Else
                                        'ソロの時
                                        If UP検索(対象採点結果(k).背番号(s), 採点結果) = "UP" Then
                                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & 対象採点結果(k).背番号(s) & "</td>"
                                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & ソロ順位(選手番号).順位_同点有り & " </td>"  '結果
                                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & 選手.リーダー表記名 & "</td>"
                                            i = i + 1 : データ(i) = "         <td class=""data02B"">" & 選手.カップル所属名 & "</td>"
                                            i = i + 1 : データ(i) = "         <td class=""TotalScore"">" & "UP" & "</td>"   'UP
                                        Else
                                            i = i + 1 : データ(i) = "         <td class=""data01"">" & 対象採点結果(k).背番号(s) & "</td>"
                                            i = i + 1 : データ(i) = "         <td class=""pcs"">" & ソロ順位(選手番号).順位_同点有り & "</td>"  '結果
                                            i = i + 1 : データ(i) = "         <td class=""name"">" & 選手.リーダー表記名 & "</td>"
                                            i = i + 1 : データ(i) = "         <td class=""Country"">" & 選手.カップル所属名 & "</td>"
                                            i = i + 1 : データ(i) = "         <td class=""TotalScore"">" & "</td>"   'UP
                                        End If

                                    End If


                                    'i = i + 1 : データ(i) = "         <td class=""TotalScore"">" & "</td>"  '勝った数  対象採点結果(k).WIN数(s)



                                    i = i + 1 : データ(i) = "         <td class=""TotalScore"">" & 対象採点結果(k).総合得点(s).ToString("0.000") & "</td>"  '総合得点
                                    i = i + 1 : データ(i) = "         <td class=""TotalScore"">" & 順位点(s).ToString("0") & "</td>"  '順位点

                                    For r = 1 To 対象採点結果(k).種目数
                                        i = i + 1 : データ(i) = "         <td class=""DanceScore"">" & 対象採点結果(k).種目(r).選手(s).種目得点.ToString("0.000") & "</td>"
                                    Next r

                                    i = i + 1 : データ(i) = "        </tr>"


                                    i = i + 1 : データ(i) = "     </tbody>"

                                    s = 対象採点結果(k).出場選手数
                                    k = 対象区分数

                                End If
                            Next s
                        End If
                    Next k
                End If
            Next 選手番号

        Next 順位


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
                i = i + 1 : データ(i) = "   <th colspan=""" & PCS数 * 2 & """style=""text-align: center;"">得点</th>"
                'i = i + 1 : データ(i) = "   <th colspan=""" & 減点数 & """style=""text-align: center;"">Reduction</th>"
                i = i + 1 : データ(i) = "   <th colspan=""1"" style=""text-align: center;"">Points</th>"
                i = i + 1 : データ(i) = "   <th colspan=""2"" style=""text-align: center;"">Total</th>"

                'タイトル2行目
                i = i + 1 : データ(i) = "     <tr>"
                i = i + 1 : データ(i) = "      <th>No</th>"
                i = i + 1 : データ(i) = "      <th>Judge</th>"
                For p = 1 To PCS数
                    'i = i + 1 : データ(i) = "      <th colspan=""2"">" & 採点結果.マスタデータ.J_新審判設定.PCS設定(p).PCS項目名 & "</th>"
                    i = i + 1 : データ(i) = "      <th colspan=""1"">" & "得点" & "</th>"
                    i = i + 1 : データ(i) = "      <th colspan=""1"">" & "(順位点)" & "</th>"
                    ' i = i + 1 : データ(i) = "      <th> </th>"
                Next p
                For r = 1 To 減点数
                    If 言語 = "E" Then
                        i = i + 1 : データ(i) = "      <th class=""redname"">" & 採点結果.マスタデータ.J_新審判設定.減点設定(r).減点項目名英 & "</th>"
                    Else
                        i = i + 1 : データ(i) = "      <th class=""redname"">" & 採点結果.マスタデータ.J_新審判設定.減点設定(r).減点項目名 & "</th>"

                    End If
                Next r
                'i = i + 1 : データ(i) = "      <th>PCS</th>"
                'i = i + 1 : データ(i) = "      <th>Redu.</th>"
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
                                        'i = i + 1 : データ(i) = "<td class=""pcs"" style=""background-color: #FFE4E1;"" >" & 採点結果.種目(d).選手(s).審判(j).PCS素点(p).ToString("0.00") & "</td>"
                                        i = i + 1 : データ(i) = "<td class=""pcs"" style=""background-color: #FFE4E1;"" >" & 採点結果.種目(d).選手(s).審判(j).素点.ToString("0.00") & "</td>"
                                        i = i + 1 : データ(i) = "<td class=""pcs2""  style=""background-color: #FFE4E1;"">N/A</td>"
                                    Else
                                        'i = i + 1 : データ(i) = "<td class=""pcs"">" & 採点結果.種目(d).選手(s).審判(j).PCS素点(p).ToString("0.00") & "</td>"
                                        i = i + 1 : データ(i) = "<td class=""pcs"">" & 採点結果.種目(d).選手(s).審判(j).素点.ToString("0.00") & "</td>"
                                        i = i + 1 : データ(i) = "<td class=""pcs2"">" & ジャッジ順位点(j).ジャッジ毎順位点(s) & "</td>"
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
                                'i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""pcs"">" & PCS得点.ToString("0.000") & "</td>"
                                '減点合計点
                                'i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""redu"">" & 減点合計 & "</td>"

                                'Total
                                i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""pcstotal"">" & 採点結果.種目(d).選手(s).種目得点.ToString("0.000") & "</td>"
                                '順位
                                'i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""rank"">" & 採点結果.種目(d).選手(s).種目順位番号 & "</td>"

                                'ソロは1種目なので。。。
                                i = i + 1 : データ(i) = "<td rowspan=""" & 審判員数woR + 1 & """ class=""rank"">" & 採点結果.総合順位表記(s) & "</td>"

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
                            'i = i + 1 : データ(i) = "<td colspan=""2"" class=""pcs""> " & 採点結果.種目(d).選手(s).種目各PCS得点(p).ToString("0.000") & "</td>"
                            i = i + 1 : データ(i) = "<td colspan=""1"" class=""pcs""> " & 採点結果.種目(d).選手(s).種目得点.ToString("0.000") & "</td>"
                            i = i + 1 : データ(i) = "<td colspan=""1"" class=""pcs""> " & 順位点(s).ToString("0") & "</td>"

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
        Dim rc As String = "  "

        For t = 1 To 採点結果.マスタデータ.E_ヒート表マスタ.登録済みレコード数
            If CInt(採点結果.マスタデータ.E_ヒート表マスタ.リスト(t).背番号) = CInt(背番号) Then
                rc = "UP"
                t = 採点結果.マスタデータ.E_ヒート表マスタ.登録済みレコード数
            End If
        Next t

        Return rc
    End Function


    Private Class ソロ順位_C

        Public 背番号 As String
        Public 得点 As Decimal
        Public 順位番号 As Double  '同点でもシリアルに順番を振る　同点の可能性があるので、Doubleで定義
        Public 順位_同点有り As Double  '同点の時は同じ順位で表記、Doubleで定義

    End Class

End Class
