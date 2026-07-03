Public Class F140_ジャッジ設定

    Private 更新FLAG As Boolean
    Private 審判グループ数 As Integer


    Private Sub F140_ジャッジ設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        更新()
        ジャッジ数集計()
    End Sub

    Private Sub 更新()


        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        審判グループ数 = マスタデータ.B_区分マスタ.Get_審判グループ数

        'データクリア
        Me.DGV_ジャッジ.DataSource = Nothing
        Me.DGV_ジャッジ.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_ジャッジ.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_ジャッジ.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable
        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("記号", GetType(String)))
        tbl.Columns.Add(New DataColumn("氏名", GetType(String)))
        tbl.Columns.Add(New DataColumn("会員番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("フリガナ", GetType(String)))
        tbl.Columns.Add(New DataColumn("表記名", GetType(String)))
        tbl.Columns.Add(New DataColumn("所属", GetType(String)))
        tbl.Columns.Add(New DataColumn("言語", GetType(String)))

        For g = 1 To 審判グループ数
            tbl.Columns.Add(New DataColumn(CStr(g), GetType(String)))
        Next g

        For j = 1 To マスタデータ.審判員マスタ.Get_登録済み審判員数

            tbl.Rows.Add()
            tbl.Rows(j - 1).Item("No") = j
            tbl.Rows(j - 1).Item("記号") = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ記号
            tbl.Rows(j - 1).Item("氏名") = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ氏名
            tbl.Rows(j - 1).Item("会員番号") = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ会員番号
            tbl.Rows(j - 1).Item("フリガナ") = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジフリガナ
            tbl.Rows(j - 1).Item("表記名") = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ表記名
            tbl.Rows(j - 1).Item("所属") = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ所属
            tbl.Rows(j - 1).Item("言語") = マスタデータ.審判員マスタ.審判員リスト(j).言語


            For g = 1 To 審判グループ数
                tbl.Rows(j - 1).Item(CStr(g)) = マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(g)
            Next g

        Next j



        '// DataGridViewにデータセットを設定
        Me.DGV_ジャッジ.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_ジャッジ.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_ジャッジ.AllowUserToResizeColumns = True

        'Ｎｏ列のFontを変更
        Me.DGV_ジャッジ.Columns("No").DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)

        マスタデータ = Nothing

    End Sub

    '
    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click


        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'B_区分マスタのレコード削除
        マスタデータ.審判員マスタ.Deleteレコード()

        Dim 審判C As 審判

        For i = 0 To Me.DGV_ジャッジ.RowCount - 1

            If Me.DGV_ジャッジ.Rows(i).Cells("記号").Value IsNot Nothing Then
                審判C = New 審判

                審判C.ジャッジ記号 = Me.DGV_ジャッジ.Rows(i).Cells("記号").Value.ToString
                審判C.ジャッジ氏名 = Me.DGV_ジャッジ.Rows(i).Cells("氏名").Value.ToString
                審判C.ジャッジフリガナ = Me.DGV_ジャッジ.Rows(i).Cells("フリガナ").Value.ToString
                審判C.ジャッジ会員番号 = Me.DGV_ジャッジ.Rows(i).Cells("会員番号").Value.ToString

                If Me.DGV_ジャッジ.Rows(i).Cells("表記名").Value.ToString = "" Then
                    審判C.ジャッジ表記名 = Me.DGV_ジャッジ.Rows(i).Cells("氏名").Value.ToString
                Else
                    審判C.ジャッジ表記名 = Me.DGV_ジャッジ.Rows(i).Cells("表記名").Value.ToString
                End If
                If Me.DGV_ジャッジ.Rows(i).Cells("言語").Value.ToString = "" Then
                    審判C.言語 = "J"
                Else
                    審判C.言語 = StrConv(Me.DGV_ジャッジ.Rows(i).Cells("言語").Value.ToString, VbStrConv.Narrow)
                End If

                For g = 1 To 審判グループ数
                    審判C.審判チーム(g) = StrConv(Me.DGV_ジャッジ.Rows(i).Cells(CStr(g)).Value.ToString, VbStrConv.Narrow)
                Next g

                マスタデータ.審判員マスタ.審判員登録(審判C)
            End If
        Next i

        審判C = Nothing
        マスタデータ = Nothing


        更新（）
        ジャッジ数集計()

        更新FLAG = False


    End Sub



    'セルが変更されたら
    Private Sub DGV_ジャッジ_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DGV_ジャッジ.CurrentCellDirtyStateChanged


        'If Me.DGV_競技種目.CurrentCellAddress.X = 0 AndAlso Me.DGV_競技種目.IsCurrentCellDirty Then
        'コミットする
        'Me.DGV_競技種目.CommitEdit(DataGridViewDataErrorContexts.Commit)
        'End If

        更新FLAG = True

        ジャッジ数集計()


    End Sub

    Private Sub ジャッジ数集計()
        'ジャッジ人数を集計して、ヘッダーに表示する

        '列ごとの集計
        For g = 1 To 審判グループ数
            Dim ジャッジ数 As Integer = 0
            Dim レフリー数 As Integer = 0
            For i = 0 To Me.DGV_ジャッジ.RowCount - 2
                If Me.DGV_ジャッジ.Rows(i).Cells(CStr(g)).Value.ToString = "1" Or Me.DGV_ジャッジ.Rows(i).Cells(CStr(g)).Value.ToString = "L" Then
                    ジャッジ数 = ジャッジ数 + 1
                End If
                If Me.DGV_ジャッジ.Rows(i).Cells(CStr(g)).Value.ToString = "R" Then
                    レフリー数 = レフリー数 + 1
                End If

            Next i
            If レフリー数 = 0 Then
                Me.DGV_ジャッジ.Columns(CStr(g)).HeaderText = CStr(g) & Environment.NewLine & "(" & ジャッジ数 & ")"
            Else
                Me.DGV_ジャッジ.Columns(CStr(g)).HeaderText = CStr(g) & Environment.NewLine & "(" & ジャッジ数 & "+" & レフリー数 & ")"

            End If
        Next g

    End Sub


    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click

        Me.Close()

    End Sub

    'フォーム閉じるボタンが押された時
    Private Sub Me_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If 更新FLAG = True Then
            If MsgBox("保存せずに終了しても良いですか？", vbOKCancel) = vbOK Then
                更新FLAG = False
                Me.Close()
            Else
                '閉じるをキャンセル
                e.Cancel = True
            End If
        End If

    End Sub


End Class