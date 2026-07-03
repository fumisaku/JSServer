Public Class VAL_1_素点

    'バルカーカップ用CSV作成クラス
    'CSV1  素点  


    Public Sub CreateCSV(採点結果 As 採点結果_V2)
        '===========================
        '概要　VAL1_POINT.csvを作成する
        '入力　採点結果
        '出力　なし
        '===========================

        Dim filepath As String = 採点結果.マスタデータ.Z_システム設定.Comp_filepath

        Dim filename As String

        If 採点結果.ラウンド番号 = "200" Then

            filename = "VAL1_POINT_SF.csv"

        ElseIf 採点結果.ラウンド番号 = "400" Then

            filename = "VAL1_POINT_FF.csv"

        Else

            filename = "VAL1_POINT_" & 採点結果.ラウンド番号 & ".csv"

        End If


        '無効審判クラスの取得
        Dim 無効審判 As G_審判無効 = New G_審判無効(filepath, 採点結果.区分番号, 採点結果.ラウンド番号)


        Dim データ(1000) As String

        Dim i As Integer
        Dim 行数 As Integer = 0

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(採点結果.区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        Dim PCS数 As Integer = 採点結果.マスタデータ.J_新審判設定.GetPCS数

        'ヘッダー行　１
        データ(行数) = ",,審査員⇒"

        For p = 1 To PCS数
            For j = 1 To 採点結果.種目(1).審判員数
                If 採点結果.種目(1).審判員(j).ジャッジタイプ <> "R" And 採点結果.種目(1).審判員(j).ジャッジタイプ <> "T" Then
                    '担当PCS番号にPCS番号が一致する場合
                    If InStr(採点結果.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(採点結果.種目(1).審判員(j).ジャッジ記号).担当PCS番号(1), CStr(p)) Then
                        データ(行数) = データ(行数) & "," & 採点結果.種目(1).審判員(j).ジャッジ記号
                    End If
                End If
            Next j
        Next p

        行数 = 行数 + 1



        'ヘッダー行　２
        データ(行数) = "選手通し番号,ペア名１,ペア名２"
        For p = 1 To PCS数
            For j = 1 To 採点結果.種目(1).審判員数
                If 採点結果.種目(1).審判員(j).ジャッジタイプ <> "R" And 採点結果.種目(1).審判員(j).ジャッジタイプ <> "T" Then
                    '担当PCS番号にPCS番号が一致する場合
                    If InStr(採点結果.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(採点結果.種目(1).審判員(j).ジャッジ記号).担当PCS番号(1), CStr(p)) Then
                        データ(行数) = データ(行数) & "," & 採点結果.マスタデータ.J_新審判設定.PCS設定(p).PCS項目名
                    End If
                End If
            Next j
        Next p

        If 採点結果.ラウンド番号 = "400" Then
            データ(行数) = データ(行数) & ",規程違反,加点"

        Else
            データ(行数) = データ(行数) & ",規程違反"

        End If

        行数 = 行数 + 1


        'データ行   ---  背番号,ペア名1,ペア名2,PCS1,PCS2,...

        For s = 1 To 採点結果.出場選手数

            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 採点結果.背番号(s))


            データ(行数) = 採点結果.背番号(s) & "," & 選手.リーダー表記名 & "," & 選手.パートナ表記名

            For p = 1 To PCS数
                For j = 1 To 採点結果.種目(1).審判員数
                    If 採点結果.種目(1).審判員(j).ジャッジタイプ <> "R" And 採点結果.種目(1).審判員(j).ジャッジタイプ <> "T" Then
                        '担当PCS番号にPCS番号が一致する場合
                        If InStr(採点結果.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(採点結果.種目(1).審判員(j).ジャッジ記号).担当PCS番号(1), CStr(p)) Then
                            'PCS得点を取得

                            If 無効審判 IsNot Nothing Then
                                If 無効審判.無効判定(採点結果.背番号(s), 採点結果.種目(1).審判員(j).ジャッジ記号) = 1 Then
                                    '無効審判の場合は空欄とする
                                    データ(行数) = データ(行数) & ","
                                Else
                                    データ(行数) = データ(行数) & "," & 採点結果.種目(1).選手結果(s).審判員結果(j).PCS素点(p).PCS素点.ToString("0.00")
                                End If
                            Else
                                データ(行数) = データ(行数) & "," & 採点結果.種目(1).選手結果(s).審判員結果(j).PCS素点(p).PCS素点.ToString("0.00")

                            End If
                        End If
                    End If
                Next j
            Next p

            If 採点結果.ラウンド番号 = "400" Then
                データ(行数) = データ(行数) & ",0,0"    '規程違反,加点
            Else
                データ(行数) = データ(行数) & ",0"    '規程違反
            End If


            行数 = 行数 + 1


        Next s


        ファイル書出し(filepath, Filename, データ)

    End Sub

    ' str: 判定対象の文字列
    ' keyword: 探したい文字列
    ' 戻り値: 含まれていればTrue、含まれていなければFalse
    Private Function InStr(str As String, keyword As String) As Boolean
        Return str.IndexOf(keyword) >= 0
    End Function

    Private Sub ファイル書出し(ByVal パス名 As String, ByVal File名 As String, ByVal データ() As String)
        '===========================
        '概要　CSVを作成する
        '入力　新File名,データ
        '出力　なし
        '===========================


        Dim NewFilename As String = パス名 & "\" & File名


        'ファイル書き出し
        If System.IO.Directory.Exists(パス名) Then
            'フォルダーは存在している
        Else
            'フォルダーが存在しないので新規作成
            System.IO.Directory.CreateDirectory(パス名)
        End If


        Dim Writer As New IO.StreamWriter(NewFilename, False, System.Text.Encoding.Default)

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
