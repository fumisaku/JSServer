Public Class F100_競技会設定

    Private 更新FLAG As Boolean

    Private Sub F100_競技会設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False


        表示()
    End Sub

    Private Sub 表示()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'データクリア
        Me.DGV_競技会設定.DataSource = Nothing
        Me.DGV_競技会設定.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_競技会設定.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_競技会設定.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("項目", GetType(String)))
        tbl.Columns.Add(New DataColumn("設定内容", GetType(String)))

        Dim 行数 As Integer = 0

        tbl.Rows.Add()
        tbl.Rows(行数).Item("項目") = "公認競技会NO"
        tbl.Rows(行数).Item("設定内容") = マスタデータ.A_競技会マスタ.公認競技会NO
        行数 = 行数 + 1

        tbl.Rows.Add()
        tbl.Rows(行数).Item("項目") = "競技会名"
        tbl.Rows(行数).Item("設定内容") = マスタデータ.A_競技会マスタ.競技会名
        行数 = 行数 + 1

        tbl.Rows.Add()
        tbl.Rows(行数).Item("項目") = "開催日"
        tbl.Rows(行数).Item("設定内容") = マスタデータ.A_競技会マスタ.開催日
        行数 = 行数 + 1

        tbl.Rows.Add()
        tbl.Rows(行数).Item("項目") = "主催団体"
        tbl.Rows(行数).Item("設定内容") = マスタデータ.A_競技会マスタ.主催団体
        行数 = 行数 + 1

        tbl.Rows.Add()
        tbl.Rows(行数).Item("項目") = "開催場所"
        tbl.Rows(行数).Item("設定内容") = マスタデータ.A_競技会マスタ.開催場所
        行数 = 行数 + 1



        '// DataGridViewにデータセットを設定
        Me.DGV_競技会設定.DataSource = tbl



        '===列幅の自動調整
        Me.DGV_競技会設定.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        '==列幅を画面にあわせる
        Me.DGV_競技会設定.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnsMode.Fill

        Me.DGV_競技会設定.AllowUserToResizeColumns = True

        '===行高さの自動調整
        Me.DGV_競技会設定.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        'カラム0 は更新不可とする
        Me.DGV_競技会設定.Columns(0).ReadOnly = True


        'マスタデータの開放
        マスタデータ = Nothing

        '並び替えができないようにする ソート禁止
        For Each c As DataGridViewColumn In Me.DGV_競技会設定.Columns
            c.SortMode = DataGridViewColumnSortMode.NotSortable
        Next c


    End Sub



    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        '一旦Delete
        マスタデータ.A_競技会マスタ.Deleteレコード()

        If Me.DGV_競技会設定.Rows(0).Cells("設定内容").Value Is DBNull.Value Then
            MsgBox("公認競技会NOがブランクです。")
            Exit Sub
        Else
            マスタデータ.A_競技会マスタ.公認競技会NO = Me.DGV_競技会設定.Rows(0).Cells("設定内容").Value
        End If

        If Me.DGV_競技会設定.Rows(1).Cells("設定内容").Value Is DBNull.Value Then
            MsgBox("競技会名がブランクです。")
            Exit Sub
        Else
            マスタデータ.A_競技会マスタ.競技会名 = Me.DGV_競技会設定.Rows(1).Cells("設定内容").Value
        End If

        If Me.DGV_競技会設定.Rows(2).Cells("設定内容").Value Is DBNull.Value Then
            MsgBox("開催日がブランクです。")
            Exit Sub
        Else
            マスタデータ.A_競技会マスタ.開催日 = Me.DGV_競技会設定.Rows(2).Cells("設定内容").Value
        End If

        If Me.DGV_競技会設定.Rows(3).Cells("設定内容").Value Is DBNull.Value Then
            MsgBox("主催団体がブランクです。")
            Exit Sub
        Else
            マスタデータ.A_競技会マスタ.主催団体 = Me.DGV_競技会設定.Rows(3).Cells("設定内容").Value
        End If

        If Me.DGV_競技会設定.Rows(4).Cells("設定内容").Value Is DBNull.Value Then
            MsgBox("開催場所がブランクです。")
            Exit Sub
        Else
            マスタデータ.A_競技会マスタ.開催場所 = Me.DGV_競技会設定.Rows(4).Cells("設定内容").Value
        End If



        マスタデータ.A_競技会マスタ.登録()


        表示()

        マスタデータ = Nothing
        更新FLAG = False
    End Sub


    'セルが変更されたら
    Private Sub DGV_競技会設定_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DGV_競技会設定.CurrentCellDirtyStateChanged


        'If Me.DGV_競技種目.CurrentCellAddress.X = 0 AndAlso Me.DGV_競技種目.IsCurrentCellDirty Then
        'コミットする
        'Me.DGV_競技種目.CommitEdit(DataGridViewDataErrorContexts.Commit)
        'End If

        更新FLAG = True

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