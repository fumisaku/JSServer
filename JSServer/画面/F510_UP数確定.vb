Public Class F510_UP数確定

    Private 採点結果 As 採点結果_C
    Private 採点結果_V2 As 採点結果_V2

    Private 区分番号, ラウンド番号 As String

    Private UP予定数 As Integer
    Private UP数 As Integer

    Private Sub F510_UP数確定_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '表示位置の設定
        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        行色付け()
    End Sub

    Public Sub 設定(区分番号_ As String, ラウンド番号_ As String, 採点結果_ As 採点結果_C)

        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_
        採点結果 = 採点結果_

        'ラベルの設定
        Me.LB_区分名.Text = 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & " " & 採点結果.マスタデータ.Get_ラウンド名(ラウンド番号)

        Me.LB_出場組数.Text = 採点結果.出場選手数 & " 組"

        UP予定数 = 採点結果.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).UP予定数
        Me.LB_UP予定数.Text = UP予定数 & " 組"


        'データクリア
        Me.DGV_UP数.DataSource = Nothing
        Me.DGV_UP数.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_UP数.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_UP数.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)

        'DGVのデフォルト配置を真ん中にする
        Me.DGV_UP数.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        '// データテーブルの作成
        Dim tbl As New DataTable
        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("得点", GetType(Double)))
        tbl.Columns.Add(New DataColumn("組数", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("累計", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("割合", GetType(String)))


        Dim 行番号 As Integer = 0
        For 順位 = 1 To 採点結果.出場選手数

            Dim 人数 As Integer = 0
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果.出場選手数
                If 順位 = 採点結果.総合順位表記(t) Then
                    人数 = 人数 + 1
                    選手番号 = t
                End If
            Next t

            If 人数 > 0 Then
                tbl.Rows.Add()
                tbl.Rows(行番号).Item("No") = 行番号 + 1
                tbl.Rows(行番号).Item("得点") = 採点結果.総合得点(選手番号)
                tbl.Rows(行番号).Item("組数") = 人数
                Dim 累計 As Integer = 0
                If 行番号 > 0 Then
                    累計 = CInt(tbl.Rows(行番号 - 1).Item("累計")） + 人数
                Else
                    累計 = 人数
                End If
                tbl.Rows(行番号).Item("累計") = 累計
                tbl.Rows(行番号).Item("割合") = Format(累計 / 採点結果.出場選手数 * 100, "##0.00") & "%"


                行番号 = 行番号 + 1

            End If

        Next 順位



        '// DataGridViewにデータセットを設定
        Me.DGV_UP数.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_UP数.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_UP数.AllowUserToResizeColumns = True

    End Sub

    Public Sub 設定_V2(区分番号_ As String, ラウンド番号_ As String, 採点結果_ As 採点結果_V2)

        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_
        採点結果_V2 = 採点結果_

        'ラベルの設定
        Me.LB_区分名.Text = 採点結果_V2.マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & " " & 採点結果_V2.マスタデータ.Get_ラウンド名(ラウンド番号)

        Me.LB_出場組数.Text = 採点結果_V2.出場選手数 & " 組"

        UP予定数 = 採点結果_V2.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).UP予定数
        Me.LB_UP予定数.Text = UP予定数 & " 組"


        'データクリア
        Me.DGV_UP数.DataSource = Nothing
        Me.DGV_UP数.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_UP数.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_UP数.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)

        'DGVのデフォルト配置を真ん中にする
        Me.DGV_UP数.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        '// データテーブルの作成
        Dim tbl As New DataTable
        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("得点", GetType(Double)))
        tbl.Columns.Add(New DataColumn("組数", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("累計", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("割合", GetType(String)))


        Dim 行番号 As Integer = 0
        For 順位 = 1 To 採点結果_V2.出場選手数

            Dim 人数 As Integer = 0
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果_V2.出場選手数
                If 順位 = 採点結果_V2.総合順位表記(t) Then
                    人数 = 人数 + 1
                    選手番号 = t
                End If
            Next t

            If 人数 > 0 Then
                tbl.Rows.Add()
                tbl.Rows(行番号).Item("No") = 行番号 + 1
                tbl.Rows(行番号).Item("得点") = 採点結果_V2.総合得点(選手番号)
                tbl.Rows(行番号).Item("組数") = 人数
                Dim 累計 As Integer = 0
                If 行番号 > 0 Then
                    累計 = CInt(tbl.Rows(行番号 - 1).Item("累計")） + 人数
                Else
                    累計 = 人数
                End If
                tbl.Rows(行番号).Item("累計") = 累計
                tbl.Rows(行番号).Item("割合") = Format(累計 / 採点結果_V2.出場選手数 * 100, "##0.00") & "%"


                行番号 = 行番号 + 1

            End If

        Next 順位



        '// DataGridViewにデータセットを設定
        Me.DGV_UP数.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_UP数.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_UP数.AllowUserToResizeColumns = True

    End Sub

    Private Sub 行色付け()


        '====UP数の色設定

        Me.LB_UP数.Text = Me.LB_UP予定数.Text & " 一致"
        UP数 = UP予定数
        For i = 0 To Me.DGV_UP数.RowCount - 1

            Me.DGV_UP数.Rows(i).DefaultCellStyle.BackColor = Color.Cyan

            If CInt(Me.DGV_UP数.Rows(i).Cells("累計").Value) >= UP予定数 Then

                Me.DGV_UP数.Rows(i).DefaultCellStyle.BackColor = Color.Cyan

                If CInt(Me.DGV_UP数.Rows(i).Cells("累計").Value) > UP予定数 Then
                    '予定数より大きい時
                    Me.DGV_UP数.Rows(i).DefaultCellStyle.BackColor = Color.Yellow

                    UP数 = Me.DGV_UP数.Rows(i).Cells("累計").Value
                    Me.LB_UP数.Text = UP数 & " 組 +" & UP数 - UP予定数 & "組"
                    Me.LB_UP数.ForeColor = Color.Yellow
                End If

                i = Me.DGV_UP数.RowCount
            End If

        Next i


    End Sub

    Private Sub PB_次へ_Click(sender As Object, e As EventArgs) Handles PB_次へ.Click


        '次ランド番号の確定
        Dim 次ラウンドC As C_ラウンド

        If 採点結果 IsNot Nothing Then
            次ラウンドC = 採点結果.マスタデータ.C_ラウンドマスタ.Get_次ラウンドClass(区分番号, ラウンド番号)
        Else
            次ラウンドC = 採点結果_V2.マスタデータ.C_ラウンドマスタ.Get_次ラウンドClass(区分番号, ラウンド番号)
        End If



        Dim 次ラウンド番号 As String = 次ラウンドC.ラウンド番号


        '同点決勝の場合は元予選で確定済みの選手情報を取得
        '　ラウンド番号の下一桁が １か２
        Dim 既確定人数 As Integer = 0
        Dim 既確定背番号リスト() As String = Nothing

        If ラウンド番号.Substring(2, 1) = "1" Or ラウンド番号.Substring(2, 1) = "2" Then

            Dim 前ラウンド番号 As String = ラウンド番号.Substring(0, 2) & "0"
            Dim 前採点結果 As 採点結果_C
            前採点結果 = New 採点結果_C(区分番号, 前ラウンド番号)

            Dim 前UP予定数 = 前採点結果.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, 前ラウンド番号).UP予定数

            '総合順位表記の何位の人が、同点決勝対象者だったか
            Dim 対象総合順位表示 As Integer = 0
            For i = 1 To 前採点結果.出場選手数
                If 前採点結果.総合順位表記(i) < 前UP予定数 And 前採点結果.総合順位表記(i) > 対象総合順位表示 Then
                    対象総合順位表示 = 前採点結果.総合順位表記(i)
                End If
            Next i

            'その総合順位表記よりも前の人が何人いるか（既に次予選に進出が確定している人は何人か？）
            For i = 1 To 前採点結果.出場選手数
                If 前採点結果.総合順位表記(i) < 対象総合順位表示 Then
                    既確定人数 = 既確定人数 + 1
                End If
            Next i

            '既確定背番号リストの作成
            If 既確定人数 > 0 Then
                ReDim 既確定背番号リスト(既確定人数）
            End If

            Dim k = 0
            For i = 1 To 前採点結果.出場選手数
                If 前採点結果.総合順位表記(i) < 対象総合順位表示 Then
                    k = k + 1
                    既確定背番号リスト(k) = 前採点結果.背番号(i)
                End If
            Next i

        End If




        '背番号リストの作成
        Dim 背番号リスト() As String
        ReDim 背番号リスト(UP数 + 既確定人数)

        Dim 人数 As Integer = 0

        If 採点結果 IsNot Nothing Then
            For i = 1 To 採点結果.出場選手数
                If 採点結果.総合順位表記(i) <= UP数 Then
                    人数 = 人数 + 1
                    背番号リスト(人数) = 採点結果.背番号(i)
                End If
            Next i

        Else
            For i = 1 To 採点結果_V2.出場選手数
                If 採点結果_V2.総合順位表記(i) <= UP数 Then
                    人数 = 人数 + 1
                    背番号リスト(人数) = 採点結果_V2.背番号(i)
                End If
            Next i
        End If


        '同点決勝の時は追加する
        If 既確定人数 > 0 Then
            For i = 1 To 既確定人数
                人数 = 人数 + 1
                背番号リスト(人数) = 既確定背番号リスト(i)
            Next i
        End If

        'F511 ヒート表作成画面の表示
        Dim F511 As F511_ヒート表作成
        F511 = New F511_ヒート表作成

        F511.設定(区分番号, 次ラウンド番号, 背番号リスト)

        F511.ShowDialog()


    End Sub

    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click
        Me.Close()
    End Sub

    Private Sub PB_同点決勝_Click(sender As Object, e As EventArgs) Handles PB_同点決勝.Click
        '同点決勝画面F520 に遷移


        '同点決勝対象者　背番号リストの作成
        Dim 背番号リスト() As String

        '総合順位表記の何位の人が、同点決勝対象者か
        Dim 対象総合順位表示 As Integer = 0

        If 採点結果 IsNot Nothing Then
            For i = 1 To 採点結果.出場選手数
                If 採点結果.総合順位表記(i) < UP予定数 And 採点結果.総合順位表記(i) > 対象総合順位表示 Then
                    対象総合順位表示 = 採点結果.総合順位表記(i)
                End If
            Next i

        Else
            For i = 1 To 採点結果_V2.出場選手数
                If 採点結果_V2.総合順位表記(i) < UP予定数 And 採点結果_V2.総合順位表記(i) > 対象総合順位表示 Then
                    対象総合順位表示 = 採点結果_V2.総合順位表記(i)
                End If
            Next i
        End If


        If 対象総合順位表示 = 0 Then
            MsgBox("同点決勝の対象ではありません。")
            Exit Sub
        End If


        'その総合順位表記の人が何人いるか（同点決勝進出者数は何人か？）
        Dim 人数 As Integer = 0

        If 採点結果 IsNot Nothing Then
            For i = 1 To 採点結果.出場選手数
                If 採点結果.総合順位表記(i) = 対象総合順位表示 Then
                    人数 = 人数 + 1
                End If
            Next i
        Else
            For i = 1 To 採点結果_V2.出場選手数
                If 採点結果_V2.総合順位表記(i) = 対象総合順位表示 Then
                    人数 = 人数 + 1
                End If
            Next i
        End If


        If 人数 = 0 Then
            MsgBox("同点決勝の対象者はいません。")
            Exit Sub
        End If


        '同点決勝対象の背番号リストを作成 
        ReDim 背番号リスト(人数)
        Dim c As Integer = 0

        If 採点結果 IsNot Nothing Then
            For i = 1 To 採点結果.出場選手数
                If 採点結果.総合順位表記(i) = 対象総合順位表示 Then
                    c = c + 1
                    背番号リスト(c) = 採点結果.背番号(i)
                End If
            Next i
        Else
            For i = 1 To 採点結果_V2.出場選手数
                If 採点結果_V2.総合順位表記(i) = 対象総合順位表示 Then
                    c = c + 1
                    背番号リスト(c) = 採点結果_V2.背番号(i)
                End If
            Next i
        End If



        '同点決勝画面の表示
        Dim F520 As F520_同点決勝設定
        F520 = New F520_同点決勝設定

        F520.設定(区分番号, ラウンド番号, 背番号リスト, UP予定数 - (対象総合順位表示 - 1))

        F520.Show()


    End Sub


    '行が選択されたら
    Private Sub DGV_UP数_RowEnter(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DGV_UP数.RowEnter

        If e.RowIndex >= 0 Then

            Dim 選択行番号 As String = Me.DGV_UP数(2, e.RowIndex).Value


        End If


    End Sub

End Class