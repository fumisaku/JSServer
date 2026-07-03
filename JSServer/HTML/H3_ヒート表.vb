Public Class H3ヒート表

    'H3形式のヒート表をHTMLで書き出す

    Public Sub New()


    End Sub

    Public Sub CreateHTML(区分番号 As String, ラウンド番号 As String, LOCAL指定 As String)

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        If マスタデータ.E_ヒート表マスタ.FileCheck(区分番号, ラウンド番号) = False Then
            MsgBox("ヒート表はまだ作成されていません。")
            Exit Sub
        End If

        マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号）
        Dim 出場選手数 As Integer = マスタデータ.C_ラウンドマスタ.出場組数(区分番号, ラウンド番号)
        Dim 種目記号() = Nothing
        Dim 種目数 As Integer = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号)
        Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)


        Dim 採点方式名 As String = 採点方式
        If 採点方式 = "PDJ10A" Then
            採点方式名 = "AJS3.0J(Type D)"
        ElseIf 採点方式 = "PDJ10B" Then
            採点方式名 = "AJS3.0J"
        ElseIf 採点方式 = "PDJ20A" Then
            採点方式名 = "AJS3.1J(Type D)"
        ElseIf 採点方式 = "PDJ20B" Then
            採点方式名 = "AJS3.1J"
        End If



        H3選手リスト作成(マスタデータ, 区分番号, ラウンド番号)

        Dim 言語 As String = マスタデータ.Z_システム設定.言語

        Dim 区分 As B_区分 = マスタデータ.B_区分マスタ.Get区分C(区分番号)
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
            i = i + 1 : データ(i) = "              <td colspan = ""7"" Class=""Title"" >【Starting List】 </td> "
        Else
            i = i + 1 : データ(i) = "              <td colspan = ""7"" Class=""Title"" >【ヒート表】 </td> "
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
        i = i + 1 : データ(i) = "          	   <td colspan = ""6"" Class=""Kubun"" >" & マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分番号, ラウンド番号) & " " & マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & "</td>"
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "             <td Class=""Title01"">Round</td>"
        Else
            i = i + 1 : データ(i) = "             <td Class=""Title01"">ラウンド</td>"
        End If
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "          	  <td colspan = ""6"" Class=""round"" >" & マスタデータ.Get_ラウンド名_E(ラウンド番号） & "</td>"
        Else
            i = i + 1 : データ(i) = "          	  <td colspan = ""6"" Class=""round"" >" & マスタデータ.Get_ラウンド名(ラウンド番号） & "</td>"
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
            i = i + 1 : データ(i) = "          	  <td Class=""data01""> " & 出場選手数 & " Couples</td>"
        Else
            i = i + 1 : データ(i) = "          	  <td Class=""data01""> 出場" & 出場選手数 & "組</td>"
        End If
        For d = 1 To 種目数
            i = i + 1 : データ(i) = "             <td Class=""data02B"">" & 種目記号(d) & "</td>"
        Next d
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "        <tr>"
        i = i + 1 : データ(i) = "             <td Class=""data01""  ></td>"   'UP数 ★

        i = i + 1 : データ(i) = "         	 <td Class=""data01"">" & 採点方式名 & "</td>"

        For d = 1 To 種目数
            If 言語 = "E" Then
                i = i + 1 : データ(i) = "        	 <td Class=""data02B"">" & マスタデータ.D_種目マスタ.Get_SG種別表記名_E(区分番号, ラウンド番号, d) & "</td>"
            Else
                i = i + 1 : データ(i) = "        	 <td Class=""data02B"">" & マスタデータ.D_種目マスタ.Get_SG種別表記名(区分番号, ラウンド番号, d) & "</td>"
            End If
        Next d

        i = i + 1 : データ(i) = "        </tr>”
        i = i + 1 : データ(i) = "     </tbody>”
        i = i + 1 : データ(i) = "</table>”

        '共通タイトル作成　ここまで



        'ヒート表
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "<h2>Heat No. List  </h2>”
        Else
            i = i + 1 : データ(i) = "<h2>種目毎ヒート番号</h2>”
        End If
        i = i + 1 : データ(i) = "<table class=""table1""  width=""100%""  >"

        'タイトル
        i = i + 1 : データ(i) = "     <thead>"
        i = i + 1 : データ(i) = "        <tr>"
        If 言語 = "E" Then
            i = i + 1 : データ(i) = "	        <th>Couple</th>"
            i = i + 1 : データ(i) = "	        <th>Name</th>"
            i = i + 1 : データ(i) = "	        <th>Country</th>"
        Else
            i = i + 1 : データ(i) = "	        <th>背番号</th>"
            i = i + 1 : データ(i) = "	        <th>選手</th>"
            i = i + 1 : データ(i) = "	        <th>所属</th>"
        End If
        For d = 1 To 種目数
            i = i + 1 : データ(i) = "	        <th>" & 種目記号(d) & "</th>"
        Next d
        i = i + 1 : データ(i) = "        </tr>"
        i = i + 1 : データ(i) = "     </thead>"

        'ヒート表データ行
        i = i + 1 : データ(i) = "     <tbody>"

        '背番号順に表示
        For s = 1 To 出場選手数
            i = i + 1 : データ(i) = "        <tr>"
            i = i + 1 : データ(i) = "         <td rowspan=""2"" class=""No"" >" & 選手リストH3(s).背番号 & "</td>"
            Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 選手リストH3(s).背番号)
            i = i + 1 : データ(i) = "         <td class=""name"">" & 選手.リーダー表記名 & "</td>"
            i = i + 1 : データ(i) = "         <td rowspan=""2"" class=""Country"">" & 選手.カップル所属名 & "</td>"
            For d = 1 To 種目数
                i = i + 1 : データ(i) = "         <td rowspan=""2"" class=""No""  >" & 選手リストH3(s).種目毎ヒート番号(d) & "</td>"
            Next d
            i = i + 1 : データ(i) = "        </tr>"

            i = i + 1 : データ(i) = "        <tr>"
            i = i + 1 : データ(i) = "         <td class=""name"">" & 選手.パートナ表記名 & "</td>"
            i = i + 1 : データ(i) = "        </tr>"

        Next s


        i = i + 1 : データ(i) = "     </tbody>"
        i = i + 1 : データ(i) = "</table>"


        i = i + 1 : データ(i) = ""



        Dim filename As String = "Heat_" & 区分番号 & "_" & ラウンド番号 & ".html"



        If LOCAL指定 = "LOCAL" Then

            ファイル書出し(マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult", filename, データ)
        Else
            ファイル書出し(マスタデータ.Z_システム設定.HTML_filepath, filename, データ)

        End If

        マスタデータ = Nothing


    End Sub

    Private 選手リストH3() As H3選手
    Private Sub H3選手リスト作成(マスタデータ As マスタデータ, 区分番号 As String, ラウンド番号 As String)

        ReDim 選手リストH3(マスタデータ.C_ラウンドマスタ.出場組数(区分番号, ラウンド番号)）
        For i = 1 To UBound(選手リストH3)
            選手リストH3(i) = New H3選手
        Next i

        Dim 種目記号リスト() = Nothing

        '種目数分ループ

        For s = 1 To マスタデータ.E_ヒート表マスタ.登録済みレコード数

            選手リストH3(s).背番号 = マスタデータ.E_ヒート表マスタ.リスト(s).背番号

            For d = 1 To マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

                選手リストH3(s).種目毎ヒート番号(d) = マスタデータ.E_ヒート表マスタ.リスト(s).ヒート番号(d)
            Next d
        Next s

    End Sub


    Private Sub H3選手リスト作成2(マスタデータ As マスタデータ, 区分番号 As String, ラウンド番号 As String)

        '============背番号ごとのヒート表作成

        ReDim 選手リストH3(マスタデータ.C_ラウンドマスタ.出場組数(区分番号, ラウンド番号)）
        For i = 1 To UBound(選手リストH3)
            選手リストH3(i) = New H3選手
        Next i

        Dim 種目記号リスト() = Nothing

        '種目数分ループ
        For s = 1 To マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)
            Dim ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(s)

            'ヒート数分ループ
            For h = 1 To ヒート数

                Dim 背番号リスト() As String = Nothing
                マスタデータ.E_ヒート表マスタ.Get_背番号リスト(s, h, 背番号リスト)

                '背番号リストを検索して、選手リストH3に登録
                For i = 1 To UBound(背番号リスト)

                    Dim FindFlag As Boolean = False
                    For sh3 = 1 To UBound(選手リストH3)
                        If 背番号リスト(i) = 選手リストH3(sh3).背番号 Then
                            '既存の選手リストH3　に見つかった時は、ヒート番号を登録
                            選手リストH3(sh3).種目毎ヒート番号(s) = h
                            FindFlag = True
                            sh3 = UBound(選手リストH3)
                        End If
                    Next sh3

                    If FindFlag = False Then
                        '既存の選手リストH3　に見つからない時は、背番号ブランクを探して、新規に登録
                        For sh3 = 1 To UBound(選手リストH3)
                            If 選手リストH3(sh3).背番号 = "" Then
                                選手リストH3(sh3).背番号 = 背番号リスト(i)
                                選手リストH3(sh3).種目毎ヒート番号(s) = h

                                sh3 = UBound(選手リストH3)
                            End If

                        Next sh3

                    End If

                Next i
            Next h
        Next s

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
