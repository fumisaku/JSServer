Public Class XL_NJ_Graph_J

    '===========================
    '概要　EXCELレーダーチャートを作成する。新ファイル名のファイルにシートを追加する。
    '使い方　Newのあと、シート作成メソッドを実行する。
    '===========================
    Private マスタデータ As マスタデータ


    Private 新シート名
    Private xlSheet As Object
    Private xlChart As Object



    'Private 系列名(4)
    Private 系列名(3)
    Private グラフ(12)


    Public Sub シート作成(ByVal xlNewSheet, ByVal マスタデータ_, ByVal 区分番号, ByVal ラウンド番号)
        '===========================
        '概要　元FileのGraphシートを新Fileにコピーし、グラフを作成する。ジャッジ毎に1シート。
        '入力　新File名,DanceCSV,CompCSV,  種目記号, ソロ種目番号, ジャッジ番号
        '出力　なし（Graphシート）
        '===========================

        マスタデータ = マスタデータ_

        '新シート名を作成
        新シート名 = "G_" + マスタデータ.B_区分マスタ.Get区分表記名(区分番号)        '新シート名はGLOBAL変数


        xlNewSheet.name = 新シート名
        xlSheet = xlNewSheet

        'Global変数の設定

        _00_初期化()
        _01_グラフの作成(区分番号)



        _11_タイトル設定(区分番号, ラウンド番号)


    End Sub

    Private Sub _00_初期化()
        '===========================
        '概要　系列名の定義
        '入力　なし
        '出力　なし
        '===========================

        系列名(0) = "[TQ]"
        系列名(1) = "[MM]"
        系列名(2) = "[PS]"
        系列名(3) = "[CP]"
        '系列名(4) = "[CP]"

        グラフ(1) = "グラフ 1"
        グラフ(2) = "グラフ 2"
        グラフ(3) = "グラフ 3"
        グラフ(4) = "グラフ 4"
        グラフ(5) = "グラフ 5"
        グラフ(6) = "グラフ 6"
        グラフ(7) = "グラフ 7"
        グラフ(8) = "グラフ 8"
        グラフ(9) = "グラフ 9"
        グラフ(10) = "グラフ 10"
        グラフ(11) = "グラフ 11"
        グラフ(12) = "グラフ 12"

    End Sub


    Private Sub _01_グラフの作成(区分番号)
        '===========================
        '概要　グラフを作成 
        '入力　なし
        '出力　なし
        '===========================
        Dim 値, ジャッジ番号

        Dim 種目C As D_種目 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, "400", 1)
        Dim 担当審判グループ As Integer = 種目C.担当審判グループ
        Dim 審判員数 = 0
        Dim 審判員数woRef = 0

        For j = 1 To マスタデータ.審判員マスタ.Get_登録済み審判員数
            If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "0" And
               マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "" Then
                審判員数 = 審判員数 + 1
            End If
            If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "0" And
               マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "" And
                マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "R" Then
                審判員数woRef = 審判員数woRef + 1
            End If

        Next j

        'ジャッジリストの作成
        Dim ジャッジリスト() As Integer
        ReDim ジャッジリスト(審判員数woRef)
        Dim jj As Integer = 0

        For j = 1 To マスタデータ.審判員マスタ.Get_登録済み審判員数
            If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "0" And
               マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "" And
                マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "R" Then
                jj = jj + 1
                ジャッジリスト(jj) = j
            End If
        Next j




        For ジャッジ番号 = 1 To 審判員数woRef
            xlChart = xlSheet.ChartObjects(グラフ(ジャッジ番号))

            'タイトルの設定
            With xlChart.Chart
                .HasTitle = True
                .ChartTitle.Characters.Text = マスタデータ.審判員マスタ.審判員リスト(ジャッジリスト(ジャッジ番号)).ジャッジ表記名
            End With

            '（１）SeriesCollection(1)に平均値を設定

            '系列名の設定
            xlSheet.ChartObjects(グラフ(ジャッジ番号)).Chart.SeriesCollection(1).XValues = 系列名


            '値の設定
            ' "=Solo_Q!R4C20:R5C24"
            '  =ジャッジ分析_2!R6C3:R6C6
            値 = "=ジャッジ分析_2!R" & CStr(ジャッジ番号 + 5) & "C3:R" & CStr(ジャッジ番号 + 5) & "C6"

            xlSheet.ChartObjects(グラフ(ジャッジ番号)).Chart.SeriesCollection(1).Values = 値

            '値の名前の設定
            xlSheet.ChartObjects(グラフ(ジャッジ番号)).Chart.SeriesCollection(1).Name = マスタデータ.審判員マスタ.審判員リスト(ジャッジリスト(ジャッジ番号)).ジャッジ表記名




            '目盛の設定
            xlSheet.ChartObjects(グラフ(ジャッジ番号)).Chart.Axes(2).MinimumScale = 0.7
            xlSheet.ChartObjects(グラフ(ジャッジ番号)).Chart.Axes(2).MaximumScale = 1.3


        Next ジャッジ番号


        For ジャッジ番号 = 審判員数woRef + 1 To 12
            xlSheet.ChartObjects(グラフ(ジャッジ番号)).delete()
        Next


    End Sub




    Private Sub _11_タイトル設定(区分番号, ラウンド番号)
        '===========================
        '概要　シートのタイトルを設定
        '入力　なし
        '出力　なし
        '===========================

        Dim ラウンド名 As String
        If ラウンド番号 = "ALL" Then
            ラウンド名 = "Total"
        Else
            ラウンド名 = マスタデータ.Get_ラウンド名(ラウンド番号)
        End If

        '中央ヘッダー
        xlSheet.PageSetup.CenterHeader = "&""Times New Roman,太字 斜体""&18 " +
            マスタデータ.A_競技会マスタ.競技会名 + Chr(10) +
            "The Result of " + マスタデータ.B_区分マスタ.Get区分表記名(区分番号) + " " + ラウンド名


        '右ヘッダー
        xlSheet.PageSetup.RightHeader = "&""Times New Roman,太字 斜体""&18 " _
            + マスタデータ.A_競技会マスタ.開催日

    End Sub
End Class
