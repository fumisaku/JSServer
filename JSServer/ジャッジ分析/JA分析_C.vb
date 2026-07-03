Public Class JA分析_C

    Public Sub JA_CSVファイル作成(採点結果 As 採点結果_C)

        Dim 競技会番号 As String = 採点結果.マスタデータ.A_競技会マスタ.公認競技会NO
        Dim 区分番号 As String = 採点結果.区分番号
        Dim ラウンド番号 As String = 採点結果.ラウンド番号

        Dim 全件数 As Integer = 採点結果.種目(1).審判員数woRef * 採点結果.出場選手数 * 採点結果.種目数 * 採点結果.マスタデータ.J_新審判設定.GetPCS数() + 1

        Dim 選手マスタLIST番号 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ


        Dim 分析() As JA_分析
        ReDim 分析(全件数)

        Dim 件数 As Integer = 1

        Dim jj As Integer = 0

        For d = 1 To 採点結果.種目数
            For s = 1 To 採点結果.出場選手数
                jj = 0
                For j = 1 To 採点結果.種目(d).審判員数
                    If 採点結果.種目(d).選手(s).審判(j).ジャッジRole <> "R" Then

                        jj = jj + 1   'ジャッジ番号

                        For p = 1 To 採点結果.マスタデータ.J_新審判設定.GetPCS数()



                            If 採点結果.種目(d).選手(s).審判(j).PCS素点(p) > 0 Then


                                分析(件数) = New JA_分析

                                分析(件数).開催日 = 採点結果.マスタデータ.A_競技会マスタ.開催日
                                分析(件数).競技会名 = 採点結果.マスタデータ.A_競技会マスタ.競技会名
                                分析(件数).区分名 = 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(区分番号)
                                分析(件数).SL区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号).カテゴリ
                                If ラウンド番号 = "400" Then
                                    分析(件数).ラウンド記号 = "FF"
                                ElseIf ラウンド番号 = "300" Then
                                    分析(件数).ラウンド記号 = "KF"
                                ElseIf ラウンド番号 = "200" Then
                                    分析(件数).ラウンド記号 = "SF"
                                ElseIf ラウンド番号 = "100" Then
                                    分析(件数).ラウンド記号 = "QF"
                                Else
                                    分析(件数).ラウンド記号 = ラウンド番号
                                End If

                                分析(件数).種目記号 = 採点結果.種目記号(d)
                                分析(件数).SG区分 = 採点結果.マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, d).SG種別

                                Dim 背番号 = 採点結果.種目(d).選手(s).背番号

                                '選手Classを取得
                                Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

                                分析(件数).背番号 = 背番号
                                分析(件数).リーダー名 = 選手.リーダー表記名
                                分析(件数).パートナー名 = 選手.パートナ表記名
                                分析(件数).ジャッジ番号 = jj
                                分析(件数).ジャッジ名 = 採点結果.種目(d).ジャッジ表記名リスト(j)
                                分析(件数).PCS名 = 採点結果.マスタデータ.J_新審判設定.PCS設定(p).PCS項目名
                                分析(件数).ジャッジ点数 = 採点結果.種目(d).選手(s).審判(j).PCS素点(p)

                                分析(件数).ジャッジ人数 = 採点結果.種目(d).審判員数woRef
                                分析(件数).選手PCS得点 = 採点結果.種目(d).選手(s).種目各PCS得点(p)


                                Dim 乖離度, 乖離度絶対値 As Double
                                Dim 選手PCS得点 = 分析(件数).選手PCS得点
                                Dim 点数 = 分析(件数).ジャッジ点数
                                If 選手PCS得点 > 点数 Then  'ジャッジが点数を低く付けた時
                                    乖離度 = (1 / (1 + (選手PCS得点 - 点数) * (選手PCS得点 - 点数)))
                                    '乖離度 = 分析(件数).乖離度
                                    乖離度絶対値 = 乖離度
                                ElseIf 選手PCS得点 < 点数 Then
                                    乖離度 = (1 / (1 + (点数 - 選手PCS得点) * (点数 - 選手PCS得点)))
                                    '乖離度 = 分析(件数).乖離度
                                    乖離度絶対値 = 2 - 乖離度
                                Else ' 選手PCS得点=点数
                                    '乖離度 = 分析(件数).乖離度
                                    乖離度 = 1
                                    乖離度絶対値 = 1
                                End If

                                '分析(件数).乖離度 = 採点結果.種目(d).選手(s).審判(j).PCS乖離度(p)
                                分析(件数).乖離度 = 乖離度
                                分析(件数).乖離度絶対値 = 乖離度絶対値

                                '乖離度は、選手の得点とジャッジの点数の差でみることとする。（中間値からの乖離ではない）

                                件数 = 件数 + 1

                            End If

                        Next p
                    End If
                Next j
            Next s
        Next d

        'CSVファイルを作成
        Dim JAファイル As JA_分析CSV
        JAファイル = New JA_分析CSV(採点結果.マスタデータ.Z_システム設定.Comp_filepath)

        JAファイル.登録(分析, 競技会番号, 区分番号, ラウンド番号)


    End Sub

    Public Sub JA_CSVファイル作成_V2(採点結果 As 採点結果_V2)

        Dim 競技会番号 As String = 採点結果.マスタデータ.A_競技会マスタ.公認競技会NO
        Dim 区分番号 As String = 採点結果.区分番号
        Dim ラウンド番号 As String = 採点結果.ラウンド番号




        Dim 全件数 As Integer = 採点結果.種目(1).Get_一般審判員数 * 採点結果.出場選手数 * 採点結果.種目数 * 採点結果.マスタデータ.J_新審判設定.GetPCS数() + 1

        Dim 選手マスタLIST番号 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ


        Dim 分析() As JA_分析
        ReDim 分析(全件数)

        Dim 件数 As Integer = 1

        Dim jj As Integer = 0

        For d = 1 To 採点結果.種目数
            For s = 1 To 採点結果.出場選手数
                jj = 0
                For j = 1 To 採点結果.種目(d).審判員数
                    If 採点結果.種目(d).審判員(j).ジャッジタイプ <> "R" Then

                        jj = jj + 1   'ジャッジ番号

                        For p = 1 To 採点結果.マスタデータ.J_新審判設定.GetPCS数()



                            If 採点結果.種目(d).選手結果(s).審判員結果(j).PCS素点(p).PCS素点 > 0 Then


                                分析(件数) = New JA_分析

                                分析(件数).開催日 = 採点結果.マスタデータ.A_競技会マスタ.開催日
                                分析(件数).競技会名 = 採点結果.マスタデータ.A_競技会マスタ.競技会名
                                分析(件数).区分名 = 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(区分番号)
                                分析(件数).SL区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号).カテゴリ
                                If ラウンド番号 = "400" Then
                                    分析(件数).ラウンド記号 = "FF"
                                ElseIf ラウンド番号 = "300" Then
                                    分析(件数).ラウンド記号 = "KF"
                                ElseIf ラウンド番号 = "200" Then
                                    分析(件数).ラウンド記号 = "SF"
                                ElseIf ラウンド番号 = "100" Then
                                    分析(件数).ラウンド記号 = "QF"
                                Else
                                    分析(件数).ラウンド記号 = ラウンド番号
                                End If

                                分析(件数).種目記号 = 採点結果.種目記号(d)
                                分析(件数).SG区分 = 採点結果.マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, d).SG種別

                                Dim 背番号 = 採点結果.背番号(s)

                                '選手Classを取得
                                Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

                                分析(件数).背番号 = 背番号
                                分析(件数).リーダー名 = 選手.リーダー表記名
                                分析(件数).パートナー名 = 選手.パートナ表記名
                                分析(件数).ジャッジ番号 = jj
                                分析(件数).ジャッジ名 = 採点結果.種目(d).審判員(j).ジャッジ表記名
                                分析(件数).PCS名 = 採点結果.マスタデータ.J_新審判設定.PCS設定(p).PCS項目名
                                分析(件数).ジャッジ点数 = 採点結果.種目(d).選手結果(s).審判員結果(j).PCS素点(p).PCS素点

                                分析(件数).ジャッジ人数 = 採点結果.種目(d).Get_一般審判員数
                                分析(件数).選手PCS得点 = 採点結果.種目(d).選手結果(s).PCS得点(p).PCS得点


                                Dim 乖離度, 乖離度絶対値 As Double
                                Dim 選手PCS得点 = 分析(件数).選手PCS得点
                                Dim 点数 = 分析(件数).ジャッジ点数
                                If 選手PCS得点 > 点数 Then  'ジャッジが点数を低く付けた時
                                    乖離度 = (1 / (1 + (選手PCS得点 - 点数) * (選手PCS得点 - 点数)))
                                    '乖離度 = 分析(件数).乖離度
                                    乖離度絶対値 = 乖離度
                                ElseIf 選手PCS得点 < 点数 Then
                                    乖離度 = (1 / (1 + (点数 - 選手PCS得点) * (点数 - 選手PCS得点)))
                                    '乖離度 = 分析(件数).乖離度
                                    乖離度絶対値 = 2 - 乖離度
                                Else ' 選手PCS得点=点数
                                    '乖離度 = 分析(件数).乖離度
                                    乖離度 = 1
                                    乖離度絶対値 = 1
                                End If

                                '分析(件数).乖離度 = 採点結果.種目(d).選手(s).審判(j).PCS乖離度(p)
                                分析(件数).乖離度 = 乖離度
                                分析(件数).乖離度絶対値 = 乖離度絶対値

                                '乖離度は、選手の得点とジャッジの点数の差でみることとする。（中間値からの乖離ではない）

                                件数 = 件数 + 1

                            End If

                        Next p
                    End If
                Next j
            Next s
        Next d

        'CSVファイルを作成
        Dim JAファイル As JA_分析CSV
        JAファイル = New JA_分析CSV(採点結果.マスタデータ.Z_システム設定.Comp_filepath)

        JAファイル.登録(分析, 競技会番号, 区分番号, ラウンド番号)


    End Sub

    Public Sub 分析EXCELの作成(ByVal 区分番号 As String, ラウンド番号 As String)

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'ラウンド番号が"ALL" の時は全て作成する


        '===元ファイルのオープン
        Dim xlApp As Object
        Dim xlOrgBook As Object

        '/*************************/
        'Dim OrgFileName = "OrgResult_V3.0.xls"
        Dim OrgFileName = "OrgResult_V3.0.xlsx"
        '/*************************/


        Dim xlOrgSheet, xlNewSheet

        xlApp = CreateObject("Excel.Application")
        xlApp.Workbooks.Open(FileName:=マスタデータ.Z_システム設定.システムPath & "\" & OrgFileName, UpdateLinks:=0)
        xlOrgBook = xlApp.workbooks(Dir(OrgFileName))
        xlApp.Visible = True


        '===新ファイルの作成
        Dim xlNewBook As Object

        Dim FName As String

        '/*************************/
        If ラウンド番号 = "ALL" Then
            'FName = "JA_" & マスタデータ.A_競技会マスタ.公認競技会NO & "_" & 区分番号 & "_" & "Total" & ".xls"
            FName = "JA_" & マスタデータ.A_競技会マスタ.公認競技会NO & "_" & 区分番号 & "_" & "Total" & ".xlsx"

        Else
            'FName = "JA_" & マスタデータ.A_競技会マスタ.公認競技会NO & "_" & 区分番号 & "_" & ラウンド番号 & ".xls"
            FName = "JA_" & マスタデータ.A_競技会マスタ.公認競技会NO & "_" & 区分番号 & "_" & ラウンド番号 & ".xlsx"
        End If
        '/*************************/

        Dim NewFileName = マスタデータ.Z_システム設定.Comp_filepath & "\" & FName

        If Dir(NewFileName) = FName Then
            Kill(NewFileName)
        End If


        xlApp.Workbooks.Add()        '新規ブックを作成する
        xlNewBook = xlApp.ActiveWorkbook


        'CSVファイルの読み込み

        Dim ジャッジ分析データ = Nothing
        Dim ジャッジ分析データ1 = Nothing

        Dim JACSVファイル As JA_分析CSV
        JACSVファイル = New JA_分析CSV(マスタデータ.Z_システム設定.Comp_filepath)


        If ラウンド番号 <> "ALL" Then

            JACSVファイル.FileRead(マスタデータ.A_競技会マスタ.公認競技会NO, 区分番号, ラウンド番号)
            JACSVファイル.Get_データ(ジャッジ分析データ)

        Else
            'ALLの時
            If JACSVファイル.FileRead(マスタデータ.A_競技会マスタ.公認競技会NO, 区分番号, "200") > 0 Then
                JACSVファイル.Get_データ(ジャッジ分析データ)
            End If

            If JACSVファイル.FileRead(マスタデータ.A_競技会マスタ.公認競技会NO, 区分番号, "100") > 0 Then
                JACSVファイル.Get_データ(ジャッジ分析データ1)
                マージデータ(ジャッジ分析データ, ジャッジ分析データ1)
            End If

            If JACSVファイル.FileRead(マスタデータ.A_競技会マスタ.公認競技会NO, 区分番号, "300") > 0 Then
                JACSVファイル.Get_データ(ジャッジ分析データ1)

                If ジャッジ分析データ Is Nothing Then
                    MsgBox("準決勝の分析シートが作成されていません。")
                    Exit Sub
                End If
                マージデータ(ジャッジ分析データ, ジャッジ分析データ1)
            End If

            If JACSVファイル.FileRead(マスタデータ.A_競技会マスタ.公認競技会NO, 区分番号, "400") > 0 Then
                JACSVファイル.Get_データ(ジャッジ分析データ1)

                If ジャッジ分析データ Is Nothing Then
                    MsgBox("準決勝の分析シートが作成されていません。")
                    Exit Sub
                End If

                マージデータ(ジャッジ分析データ, ジャッジ分析データ1)
            End If

        End If




        '===ジャッジ分析シート２の作成 =================

        xlOrgSheet = xlOrgBook.sheets("ジャッジ分析_2")
        xlNewSheet = xlNewBook.sheets(1)

        xlOrgSheet.copy(Before:=xlNewSheet)


        Dim xlJudgeAna2 As xl_ジャッジ分析_2
        xlJudgeAna2 = New xl_ジャッジ分析_2

        xlNewSheet = xlNewBook.Sheets("ジャッジ分析_2")
        xlNewSheet.name = "ジャッジ分析_2"


        xlJudgeAna2.シート作成(xlNewSheet, NewFileName, マスタデータ, ジャッジ分析データ, 区分番号, ラウンド番号)


        '===ジャッジ分析シート１の作成 =================

        xlOrgSheet = xlOrgBook.sheets("ジャッジ分析_1")
        xlNewSheet = xlNewBook.sheets(1)

        xlOrgSheet.copy(Before:=xlNewSheet)


        Dim xlJudgeAna1 As xl_ジャッジ分析_1
        xlJudgeAna1 = New xl_ジャッジ分析_1

        xlNewSheet = xlNewBook.Sheets("ジャッジ分析_1")
        xlNewSheet.name = "ジャッジ分析_1"


        xlJudgeAna1.シート作成(xlNewSheet, NewFileName, マスタデータ, ジャッジ分析データ, 区分番号, ラウンド番号)

        '======グラフシートの作成

        xlOrgSheet = xlOrgBook.sheets("NJ_Graph")
        xlNewSheet = xlNewBook.sheets(1)

        xlOrgSheet.copy(After:=xlNewSheet)
        'xlOrgSheet.copy(Before:=xlNewBook.Sheets("Sheet1"))

        Dim xlGraph As XL_NJ_Graph_J
        xlGraph = New XL_NJ_Graph_J

        xlNewSheet = xlNewBook.Sheets("NJ_Graph")
        xlNewSheet.name = "G_" & マスタデータ.B_区分マスタ.Get区分表記名(区分番号)

        xlGraph.シート作成(xlNewSheet, マスタデータ, 区分番号, ラウンド番号)

        xlGraph = Nothing


        '===余計なシートの削除
        xlApp.DisplayAlerts = False
        xlNewBook.Sheets("Sheet1").Delete()

        Dim ws
        Dim flag

        flag = False
        For Each ws In xlApp.Worksheets
            If ws.Name = "Sheet2" Then
                flag = True
            End If
        Next ws
        If flag = True Then
            xlNewBook.Sheets("Sheet2").Delete()
        End If

        flag = False
        For Each ws In xlApp.Worksheets
            If ws.Name = "Sheet3" Then
                flag = True
            End If
        Next ws
        If flag = True Then
            xlNewBook.Sheets("Sheet3").Delete()
        End If

        xlApp.DisplayAlerts = True




        '===ファイルの保存とクローズ　2013対応版
        xlOrgBook.Close(saveChanges:=False)
        'xlNewBook.Close(saveChanges:=True)


        If xlApp.Application.Version < 12 Then
            xlNewBook.SaveAs(filename:=NewFileName, FileFormat:=43) 'xl2003まで
        Else
            'xlNewBook.SaveAs(filename:=NewFileName, FileFormat:=56) 'xlEXCEL8
            xlNewBook.SaveAs(filename:=NewFileName) 'デフォルト
        End If


        '===========印刷 ==========
        With xlNewBook.Sheets("ジャッジ分析_1").PageSetup
            .Zoom = False
            .FitToPagesWide = 1
            .FitToPagesTall = 1
        End With
        xlNewBook.Sheets("ジャッジ分析_1").PrintOut()


        xlNewBook.Sheets("G_" & マスタデータ.B_区分マスタ.Get区分表記名(区分番号)).PrintOut()

        xlNewBook.Sheets("ジャッジ分析_2").PrintOut()



        xlNewBook.Close(saveChanges:=False)


        xlApp.quit()
        xlOrgBook = Nothing
        xlNewBook = Nothing
        xlOrgSheet = Nothing
        xlNewSheet = Nothing
        xlApp = Nothing


        マスタデータ = Nothing

    End Sub


    Private Sub マージデータ(ByRef 元分析データ, ByVal 追加分析データ)

        Dim TEMP分析データ(,)
        Dim 行数, i, j, T


        行数 = UBound(元分析データ) + UBound(追加分析データ)
        ReDim TEMP分析データ(行数, 17)

        '元分析データをコピー
        T = 0
        For i = 0 To UBound(元分析データ)
            If 元分析データ(i, 0) <> "" Then
                For j = 0 To 17
                    TEMP分析データ(T, j) = 元分析データ(i, j)
                Next j
                T = T + 1
            End If
        Next i

        '追加分析データをコピー
        If T = 0 Then
            For j = 0 To 17
                TEMP分析データ(T, j) = 追加分析データ(0, j)
            Next j
            T = T + 1
        End If

        For i = 1 To UBound(追加分析データ)
            For j = 0 To 17
                TEMP分析データ(T, j) = 追加分析データ(i, j)
            Next j
            T = T + 1
        Next i

        元分析データ = TEMP分析データ


    End Sub

End Class
