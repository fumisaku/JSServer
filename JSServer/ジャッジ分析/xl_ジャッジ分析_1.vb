Public Class xl_ジャッジ分析_1


    '===========================
    '概要　EXCELのSolo結果を作成する。新ファイル名のファイルにシートを追加する。
    '使い方　Newのあと、シート作成メソッドを実行する。
    '===========================

    Private マスタデータ As マスタデータ

    Private 新シート名

    Private 区分番号 As String
    Private ラウンド番号 As String

    Private xlSheet As Object




    Public Sub シート作成(ByVal xlNewSheet, ByVal xlNewFilename, ByVal マスタデータ_, ByVal ジャッジ分析データ, ByVal 区分番号_, ByVal ラウンド番号_)
        '===========================
        '概要　元FileのSoloシートを新Fileにコピーし、DanceCSVの値を埋める。
        '入力　元File名,新File名,CompCSV
        '出力　なし（JS_Heat1）
        '      区分毎に１枚印刷する。
        '===========================


        マスタデータ = マスタデータ_

        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_



        '新シート名を作成
        新シート名 = "ジャッジ分析_1"  '新シート名はGLOBAL変数

        xlNewSheet.name = 新シート名
        xlSheet = xlNewSheet

        Dim 分析結果1 = Nothing
        Dim 分析結果2 = Nothing
        _00_初期化()
        _01_分析_01(ジャッジ分析データ, 分析結果1)
        _02_分析_02_好感度(ジャッジ分析データ, 分析結果2)
        _03_記入_01(分析結果1)
        _04_記入_02(分析結果2)

        _11_タイトル設定()

    End Sub

    Private Sub _00_初期化()
        '===========================
        '概要　シートをクリア
        '入力　なし
        '出力　なし
        '===========================

        xlSheet.Range("B7:D19").ClearContents()
        'xlSheet.Rows("20:161").hidden = False
        'xlSheet.Columns("A:BK").hidden = False

    End Sub

    Private Sub _01_分析_01(ByVal ジャッジ分析データ, ByRef 分析結果_)
        '===========================
        '概要　ジャッジ毎の乖離度を計算する。
        '入力　ジャッジ分析データ
        '出力　分析結果
        '===========================

        '分析結果
        '0列 ジャッジNO
        '1列 ジャッジ名
        '2列 乖離度の平均値

        '3列 乖離度の合計
        '4列 件数


        Dim i

        Dim 分析結果(12, 12)
        分析結果(0, 0) = "ジャッジNO"
        分析結果(0, 1) = "ジャッジ名"
        分析結果(0, 2) = "適合度(%)"

        For i = 1 To UBound(ジャッジ分析データ)
            If ジャッジ分析データ(i, 1) IsNot Nothing Then
                分析結果(ジャッジ分析データ(i, 10), 0) = ジャッジ分析データ(i, 10)  'ジャッジNO
                分析結果(ジャッジ分析データ(i, 10), 1) = ジャッジ分析データ(i, 11)  'ジャッジ名

                分析結果(ジャッジ分析データ(i, 10), 3) = 分析結果(ジャッジ分析データ(i, 10), 3) + ジャッジ分析データ(i, 14)
                分析結果(ジャッジ分析データ(i, 10), 4) = 分析結果(ジャッジ分析データ(i, 10), 4) + 1

            End If
        Next i

        '平均値の算出
        Dim j
        For j = 1 To 12
            If 分析結果(j, 4) IsNot Nothing Then
                分析結果(j, 2) = 分析結果(j, 3) / 分析結果(j, 4)
            End If
        Next j


        'ソート
        Dim 分析結果ソート後(12, 12)
        Dim 最大値 As Double = 0
        Dim 最大値NO = 0
        分析結果ソート後(0, 0) = "NO"
        分析結果ソート後(0, 1) = "ジャッジ名"
        分析結果ソート後(0, 2) = "適合度(%)"

        For j = 1 To 12
            最大値 = 0
            最大値NO = 0
            For i = 1 To 12
                If 分析結果(i, 2) IsNot Nothing Then
                    If 分析結果(i, 2) > 最大値 Then
                        最大値 = 分析結果(i, 2)
                        最大値NO = i
                    End If
                End If
            Next i

            If 最大値NO > 0 Then
                分析結果ソート後(j, 0) = j  'NO
                分析結果ソート後(j, 1) = 分析結果(最大値NO, 1)  'ジャッジ名
                分析結果ソート後(j, 2) = 分析結果(最大値NO, 2)  '適合度

                分析結果(最大値NO, 2) = 0
            End If
        Next j

        分析結果_ = 分析結果ソート後


    End Sub

    Private Sub _02_分析_02_好感度(ByVal ジャッジ分析データ, ByRef 分析結果_)
        '===========================
        '概要　ジャッジ、選手の好感度を計算する。
        '入力　ジャッジ分析データ
        '出力　分析結果
        '===========================

        '分析結果
        '0列 ジャッジNO
        '1列 ジャッジ名
        '2列 選手名
        '3列 係数

        '4列 合計　(絶対値の合計）
        '5列 件数


        Dim i, j, FIND_FLAG
        'Dim 行数 = CompCSV.決勝進出者数 * CompCSV.ジャッジ人数
        Dim 行数 = UBound(ジャッジ分析データ)

        Dim 最終行 = 1

        Dim 分析結果(,)
        ReDim 分析結果(行数, 5)
        分析結果(0, 0) = "ジャッジNO"
        分析結果(0, 1) = "ジャッジ名"
        分析結果(0, 2) = "選手名（リーダー）"
        分析結果(0, 3) = "係数"

        For i = 1 To UBound(ジャッジ分析データ)
            If ジャッジ分析データ(i, 1) IsNot Nothing Then

                FIND_FLAG = 0
                For j = 1 To UBound(分析結果)
                    If 分析結果(j, 1) & 分析結果(j, 2) = ジャッジ分析データ(i, 11) & ジャッジ分析データ(i, 8) Then
                        FIND_FLAG = j
                        j = UBound(分析結果)
                    End If
                Next j

                If FIND_FLAG <> 0 Then
                    分析結果(FIND_FLAG, 0) = ジャッジ分析データ(i, 10)  'ジャッジNO
                    分析結果(FIND_FLAG, 1) = ジャッジ分析データ(i, 11)  'ジャッジ名
                    分析結果(FIND_FLAG, 2) = ジャッジ分析データ(i, 8)  '選手名（リーダー）

                    分析結果(FIND_FLAG, 4) = 分析結果(FIND_FLAG, 4) + ジャッジ分析データ(i, 15)  '乖離度
                    分析結果(FIND_FLAG, 5) = 分析結果(FIND_FLAG, 5) + 1
                Else
                    分析結果(最終行, 0) = ジャッジ分析データ(i, 10)  'ジャッジNO
                    分析結果(最終行, 1) = ジャッジ分析データ(i, 11)  'ジャッジ名
                    分析結果(最終行, 2) = ジャッジ分析データ(i, 8)  '選手名（リーダー）

                    分析結果(最終行, 4) = ジャッジ分析データ(i, 15)  '乖離度
                    分析結果(最終行, 5) = 1
                    最終行 = 最終行 + 1
                End If

            End If
        Next i

        '平均値の算出
        For i = 1 To UBound(分析結果)
            If 分析結果(i, 5) IsNot Nothing Then
                分析結果(i, 3) = 分析結果(i, 4) / 分析結果(i, 5)
            End If
        Next i


        'ソート

        Dim 分析結果ソート後(行数, 4)
        Dim 最大値 As Double = 0
        Dim 最大値NO As Integer = 0
        分析結果ソート後(0, 0) = "NO"
        分析結果ソート後(0, 1) = "ジャッジ名"
        分析結果ソート後(0, 2) = "選手名（リーダー）"
        分析結果ソート後(0, 3) = "係数"

        For j = 1 To 行数
            最大値 = 0
            最大値NO = 0
            For i = 1 To 行数
                If 分析結果(i, 3) IsNot Nothing Then
                    If 分析結果(i, 3) > 最大値 Then
                        最大値 = 分析結果(i, 3)
                        最大値NO = i
                    End If
                End If
            Next i

            If 最大値NO > 0 Then
                分析結果ソート後(j, 0) = j  'NO
                分析結果ソート後(j, 1) = 分析結果(最大値NO, 1)  'ジャッジ名
                分析結果ソート後(j, 2) = 分析結果(最大値NO, 2)  '選手名（リーダー）
                分析結果ソート後(j, 3) = 分析結果(最大値NO, 3)  '係数

                分析結果(最大値NO, 3) = 0
            End If
        Next j

        分析結果_ = 分析結果ソート後


    End Sub

    Private Sub _03_記入_01(ByVal 分析結果)
        '===========================
        '概要　シートにデータを記入
        '入力　分析結果
        '出力 　なし
        '===========================

        xlSheet.Range("B7:D19") = 分析結果

    End Sub

    Private Sub _04_記入_02(ByVal 分析結果)
        '===========================
        '概要　シートにデータを記入
        '入力　分析結果
        '出力 　なし
        '===========================

        '好感度
        Dim i, j
        Dim 結果(10, 3)
        j = 0
        For i = 1 To 10
            If 分析結果(i, 3) > 1 Then
                結果(j, 0) = 分析結果(i, 1)   'ジャッジ名
                結果(j, 1) = 分析結果(i, 2)   '選手名
                結果(j, 2) = 分析結果(i, 3)   '係数
                j = j + 1
            End If
        Next i

        xlSheet.Range("C28:E37") = 結果

        '逆好感度
        ReDim 結果(10, 3)
        j = 0
        For i = UBound(分析結果) To 1 Step -1
            If 分析結果(i, 3) < 1 And 分析結果(i, 3) IsNot Nothing Then
                結果(j, 0) = 分析結果(i, 1)   'ジャッジ名
                結果(j, 1) = 分析結果(i, 2)   '選手名
                結果(j, 2) = 分析結果(i, 3)   '係数
                j = j + 1
            End If
            If j >= 10 Then
                i = 0
            End If
        Next i

        xlSheet.Range("I28:K37") = 結果




    End Sub



    Private Sub _11_タイトル設定()
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

        '左ヘッダー
        xlSheet.PageSetup.LeftHeader = "&""Times New Roman,太字 斜体""&18 " + "JDSF"

    End Sub



End Class
