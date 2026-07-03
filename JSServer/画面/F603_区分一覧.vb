Public Class F603_区分一覧

    Private Sub F603_区分一覧_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False

        DGV区分一覧表示()
    End Sub

    Private Sub DGV区分一覧表示()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'データクリア
        Me.DGV_区分一覧.DataSource = Nothing
        Me.DGV_区分一覧.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_区分一覧.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_区分一覧.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable



        tbl.Columns.Add(New DataColumn("区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("決勝結果", GetType(String)))　　　'ボタン
        tbl.Columns.Add(New DataColumn("区分記号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分名", GetType(String)))
        tbl.Columns.Add(New DataColumn("カテゴリ", GetType(String)))


        For i = 1 To マスタデータ.B_区分マスタ.登録済みレコード数

            tbl.Rows.Add()
            Dim idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item("区分番号") = マスタデータ.B_区分マスタ.リスト(i).区分番号

            If マスタデータ.T_採点進行管理.Get_採点進行Class(マスタデータ.B_区分マスタ.リスト(i).区分番号, "400").ステータス = "採点済み" Then
                tbl.Rows(idx).Item("決勝結果") = "決勝結果"
            Else
                tbl.Rows(idx).Item("決勝結果") = ""
            End If
            tbl.Rows(idx).Item("区分記号") = マスタデータ.B_区分マスタ.リスト(i).区分記号
            tbl.Rows(idx).Item("区分名") = マスタデータ.B_区分マスタ.リスト(i).区分表記名
            tbl.Rows(idx).Item("カテゴリ") = マスタデータ.B_区分マスタ.リスト(i).カテゴリ

        Next i




        '// DataGridViewにデータセットを設定
        Me.DGV_区分一覧.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_区分一覧.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_区分一覧.AllowUserToResizeColumns = True

        Me.DGV_区分一覧.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells



        '====決勝結果ボタンの表示

        Dim PB_決勝結果 As New DataGridViewButtonColumn()
        '表示する列の名前を設定する
        PB_決勝結果.DataPropertyName = Me.DGV_区分一覧.Columns("決勝結果").DataPropertyName

        '現在列が存在している位置に挿入する
        Me.DGV_区分一覧.Columns.Insert(Me.DGV_区分一覧.Columns("決勝結果").Index, PB_決勝結果)
        '今までの列を削除する
        Me.DGV_区分一覧.Columns.Remove("決勝結果")
        '挿入した列の名前を「決勝結果」とする
        PB_決勝結果.Name = "決勝結果"
        '列のタイトルを設定
        Me.DGV_区分一覧.Columns("決勝結果").HeaderText = "決勝結果"



        マスタデータ = Nothing

    End Sub

    '不要なボタンを非表示（ぬりつぶし））
    Private Sub DGV_区分一覧_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles DGV_区分一覧.CellPainting

        If Me.DGV_区分一覧.Columns(e.ColumnIndex).Name = "決勝結果" Then
            If CStr(e.Value) = "" Then
                Dim selected As Boolean = DataGridViewElementStates.Selected = (e.State And DataGridViewElementStates.Selected)
                e.PaintBackground(e.CellBounds, selected)
                e.Handled = True
            End If
        End If
    End Sub



    '決勝結果ボタン CellContentClickイベントハンドラ
    Private Sub DGV_区分一覧_CellContentClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DGV_区分一覧.CellContentClick

        Dim dgv As DataGridView = CType(sender, DataGridView)
        '"Button"列ならば、ボタンがクリックされた
        If dgv.Columns(e.ColumnIndex).Name = "決勝結果" Then
            'MessageBox.Show((e.RowIndex.ToString() + "行のボタンがクリックされました。"))

            Dim 区分番号 As String = dgv.Rows(e.RowIndex).Cells("区分番号").Value

            Dim F601 As F601_名簿
            F601 = New F601_名簿
            F601.決勝結果表示(区分番号, "400")
            F601.Show()

        End If



    End Sub

    Private Sub PB_閉じる_Click(sender As Object, e As EventArgs) Handles PB_閉じる.Click
        Me.Dispose()
    End Sub



End Class