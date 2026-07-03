Public Class xl_ジャッジ分析_2

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
        新シート名 = "ジャッジ分析_2"  '新シート名はGLOBAL変数

        xlNewSheet.name = 新シート名
        xlSheet = xlNewSheet

        Dim 分析結果 = Nothing
        _00_初期化()
        _01_分析(ジャッジ分析データ, 分析結果)
        _02_記入(分析結果)


        _11_タイトル設定()

    End Sub

    Private Sub _00_初期化()
        '===========================
        '概要　シートをクリア
        '入力　なし
        '出力　なし
        '===========================

        xlSheet.Range("B6:F17").ClearContents()
        'xlSheet.Rows("20:161").hidden = False
        'xlSheet.Columns("A:BK").hidden = False

    End Sub


    Private Sub _01_分析(ByVal ジャッジ分析データ, ByRef 分析結果_)
        '===========================
        '概要　ジャッジ毎　PCS毎の平均乖離度を計算する。
        '入力　ジャッジ分析データ
        '出力　分析結果
        '===========================

        '分析結果
        '0列 ジャッジ名
        '1列～4列 乖離度の平均値

        '5列～8列 乖離度の合計
        '9列～12列 件数

        Dim i

        Dim 分析結果(12, 12)
        分析結果(0, 1) = "TQ"
        分析結果(0, 2) = "MM"
        分析結果(0, 3) = "PS"
        分析結果(0, 4) = "CP"

        For i = 1 To UBound(ジャッジ分析データ)
            If ジャッジ分析データ(i, 1) <> "" Then
                分析結果(ジャッジ分析データ(i, 10), 0) = ジャッジ分析データ(i, 11)  'ジャッジ名

                If ジャッジ分析データ(i, 12) = "TQ" Then
                    分析結果(ジャッジ分析データ(i, 10), 5) = 分析結果(ジャッジ分析データ(i, 10), 5) + ジャッジ分析データ(i, 15)
                    分析結果(ジャッジ分析データ(i, 10), 9) = 分析結果(ジャッジ分析データ(i, 10), 9) + 1
                ElseIf ジャッジ分析データ(i, 12) = "MM" Then
                    分析結果(ジャッジ分析データ(i, 10), 6) = 分析結果(ジャッジ分析データ(i, 10), 6) + ジャッジ分析データ(i, 15)
                    分析結果(ジャッジ分析データ(i, 10), 10) = 分析結果(ジャッジ分析データ(i, 10), 10) + 1

                ElseIf ジャッジ分析データ(i, 12) = "PS" Then
                    分析結果(ジャッジ分析データ(i, 10), 7) = 分析結果(ジャッジ分析データ(i, 10), 7) + ジャッジ分析データ(i, 15)
                    分析結果(ジャッジ分析データ(i, 10), 11) = 分析結果(ジャッジ分析データ(i, 10), 11) + 1

                ElseIf ジャッジ分析データ(i, 12) = "CP" Then
                    分析結果(ジャッジ分析データ(i, 10), 8) = 分析結果(ジャッジ分析データ(i, 10), 8) + ジャッジ分析データ(i, 15)
                    分析結果(ジャッジ分析データ(i, 10), 12) = 分析結果(ジャッジ分析データ(i, 10), 12) + 1
                End If
            End If
        Next i

        '平均値の算出
        Dim j
        For j = 1 To 12
            If 分析結果(j, 9) IsNot Nothing Then
                分析結果(j, 1) = 分析結果(j, 5) / 分析結果(j, 9)
            End If
            If 分析結果(j, 10) IsNot Nothing Then
                分析結果(j, 2) = 分析結果(j, 6) / 分析結果(j, 10)
            End If
            If 分析結果(j, 11) IsNot Nothing Then
                分析結果(j, 3) = 分析結果(j, 7) / 分析結果(j, 11)
            End If
            If 分析結果(j, 12) IsNot Nothing Then
                分析結果(j, 4) = 分析結果(j, 8) / 分析結果(j, 12)
            End If
        Next j
        分析結果_ = 分析結果

    End Sub


    Private Sub _02_記入(ByVal 分析結果)
        '===========================
        '概要　シートにデータを記入
        '入力　分析結果
        '出力 　なし
        '===========================

        xlSheet.Range("B5:F17") = 分析結果

    End Sub

    Private Sub _11_タイトル設定()
        '===========================
        '概要　シートのタイトルを設定
        '入力　なし
        '出力　なし
        '===========================

        'xlSheet.Range("B2").Value = "Solo Dance(" & CompCSV.種目名称_E(CompCSV.Getソロ種目記号(ソロ種目番号_c)) & ")"

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
