Public Class F602_進行詳細


    Private Sub F602_進行詳細_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False

    End Sub


    Public Sub 更新()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ



        'データクリア
        Me.DGV_進行詳細.DataSource = Nothing
        Me.DGV_進行詳細.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_進行詳細.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Bold)
        'ヘッダーのフォント指定
        Me.DGV_進行詳細.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 10, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable
        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("ステータス", GetType(String)))
        tbl.Columns.Add(New DataColumn("競技NO", GetType(String)))
        tbl.Columns.Add(New DataColumn("枝番", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分NO", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分名", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド番号", GetType(String)))  '非表示
        tbl.Columns.Add(New DataColumn("ラウンド", GetType(String)))
        tbl.Columns.Add(New DataColumn("種目順", GetType(Integer)))    '非表示
        tbl.Columns.Add(New DataColumn("種目記号", GetType(String)))
        tbl.Columns.Add(New DataColumn("ヒート番号", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("ヒート表", GetType(String)))　　　'ボタン

        tbl.Columns.Add(New DataColumn("終了時刻", GetType(String)))



        For s = 1 To マスタデータ.U_進行管理.登録済みレコード数

            Dim 区分番号 As String = ""
            Dim ラウンド番号 As String = ""
            Dim 進行 As U_進行 = マスタデータ.U_進行管理.リスト(s)
            マスタデータ.T_採点進行管理.Get_区分ラウンド番号(進行.競技番号, 進行.競技番号枝番, 区分番号, ラウンド番号)

            tbl.Rows.Add()
            tbl.Rows(s - 1).Item("No") = 進行.進行番号
            tbl.Rows(s - 1).Item("ステータス") = 進行.ステータス
            tbl.Rows(s - 1).Item("競技NO") = 進行.競技番号
            tbl.Rows(s - 1).Item("枝番") = 進行.競技番号枝番
            tbl.Rows(s - 1).Item("区分NO") = 区分番号
            tbl.Rows(s - 1).Item("区分名") = マスタデータ.B_区分マスタ.Get区分表記名(区分番号)
            tbl.Rows(s - 1).Item("ラウンド番号") = ラウンド番号
            tbl.Rows(s - 1).Item("ラウンド") = マスタデータ.Get_ラウンド名(ラウンド番号)
            tbl.Rows(s - 1).Item("種目順") = 進行.種目順
            tbl.Rows(s - 1).Item("種目記号") = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 進行.種目順).種目記号
            tbl.Rows(s - 1).Item("ヒート番号") = 進行.ヒート番号

            If 進行.ステータス = "ヒート表作成済み" Or 進行.ステータス = "全審判送信済み" Then

                tbl.Rows(s - 1).Item("ヒート表") = "ヒート表"
            Else
                tbl.Rows(s - 1).Item("ヒート表") = ""
            End If
            tbl.Rows(s - 1).Item("終了時刻") = 進行.採点終了時刻


        Next s



        '// DataGridViewにデータセットを設定
        Me.DGV_進行詳細.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_進行詳細.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_進行詳細.AllowUserToResizeColumns = True

        Me.DGV_進行詳細.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        'Ｎｏ列のFontを変更
        Me.DGV_進行詳細.Columns("No").DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)

        Me.DGV_進行詳細.Columns("ラウンド番号").Visible = False
        Me.DGV_進行詳細.Columns("種目順").Visible = False


        マスタデータ = Nothing


        '色付け
        For i = 0 To Me.DGV_進行詳細.RowCount - 1
            If Me.DGV_進行詳細.Rows(i).Cells("ステータス").Value = "全審判送信済み" Then
                Me.DGV_進行詳細.Rows(i).DefaultCellStyle.BackColor = Color.LightGray
            Else
                Me.DGV_進行詳細.Rows(i).DefaultCellStyle.BackColor = Nothing
            End If

        Next i


        Dim PB_ヒート表 As New DataGridViewButtonColumn()
        '表示する列の名前を設定する
        PB_ヒート表.DataPropertyName = Me.DGV_進行詳細.Columns("ヒート表").DataPropertyName

        '現在ヒート表列が存在している位置に挿入する
        Me.DGV_進行詳細.Columns.Insert(Me.DGV_進行詳細.Columns("ヒート表").Index, PB_ヒート表)
        '今までのヒート表列を削除する
        Me.DGV_進行詳細.Columns.Remove("ヒート表")
        '挿入した列の名前を「ヒート表」とする
        PB_ヒート表.Name = "ヒート表"
        '列のタイトルを設定
        Me.DGV_進行詳細.Columns("ヒート表").HeaderText = "ヒート表"


    End Sub



    '====セルの結合処理　ここから
    ' 指定したセルと1つ上のセルの値を比較
    Function IsTheSameCellValue(ByVal column As Integer, ByVal row As Integer) As Boolean


        Dim cell1 As DataGridViewCell = DGV_進行詳細(column, row)
        Dim cell2 As DataGridViewCell = DGV_進行詳細(column, row - 1)

        If cell1 Is DBNull.Value Or cell2 Is DBNull.Value Then
            Return False
        End If

        If cell1.Value Is Nothing Or cell2.Value Is Nothing Then
            Return False
        End If

        Dim CellText1 As String = DGV_進行詳細(2, row).Value.ToString & DGV_進行詳細(3, row).Value.ToString
        Dim CellText2 As String = DGV_進行詳細(2, row - 1).Value.ToString & DGV_進行詳細(3, row - 1).Value.ToString

        ' ここでは文字列としてセルの値を比較
        If CellText1 = CellText2 Then
            Return True
        Else
            Return False
        End If

    End Function

    ' DataGridViewのCellFormattingイベント・ハンドラ
    Sub dgv_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGV_進行詳細.CellFormatting

        ' 1行目については何もしない
        If e.RowIndex = 0 Then
            Return
        End If

        'カラム番号が、2-7 以外のときはなにもしない
        If e.ColumnIndex < 2 Or e.ColumnIndex > 7 Then
            Return
        End If

        If IsTheSameCellValue(e.ColumnIndex, e.RowIndex) Then
            e.Value = ""
            e.FormattingApplied = True ' 以降の書式設定は不要
        End If
    End Sub

    ' DataGridViewのCellPaintingイベント・ハンドラ
    Sub dgv_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DGV_進行詳細.CellPainting

        ' セルの下側の境界線を「境界線なし」に設定
        e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None

        ' 1行目や列ヘッダ、行ヘッダの場合は何もしない
        If e.RowIndex < 1 Or e.ColumnIndex < 0 Then
            Return
        End If


        If IsTheSameCellValue(e.ColumnIndex, e.RowIndex) Then

            ' セルの上側の境界線を「境界線なし」に設定
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        Else
            ' セルの上側の境界線を既定の境界線に設定
            e.AdvancedBorderStyle.Top = DGV_進行詳細.AdvancedCellBorderStyle.Top
        End If


    End Sub


    '不要なボタンを非表示（ぬりつぶし））
    Private Sub DGV_進行詳細_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles DGV_進行詳細.CellPainting

        If Me.DGV_進行詳細.Columns(e.ColumnIndex).Name = "ヒート表" Then
            If CStr(e.Value) = "" Then
                Dim selected As Boolean = DataGridViewElementStates.Selected = (e.State And DataGridViewElementStates.Selected)
                e.PaintBackground(e.CellBounds, selected)
                e.Handled = True
            End If
        End If
    End Sub


    'ヒート表ボタン CellContentClickイベントハンドラ
    Private Sub DGV_進行詳細_CellContentClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DGV_進行詳細.CellContentClick

        Dim dgv As DataGridView = CType(sender, DataGridView)
        '"Button"列ならば、ボタンがクリックされた
        If dgv.Columns(e.ColumnIndex).Name = "ヒート表" Then
            'MessageBox.Show((e.RowIndex.ToString() + "行のボタンがクリックされました。"))

            Dim 競技番号 As String = dgv.Rows(e.RowIndex).Cells("競技NO").Value
            Dim 競技番号枝番 As String = dgv.Rows(e.RowIndex).Cells("枝番").Value
            Dim 種目順 As Integer = dgv.Rows(e.RowIndex).Cells("種目順").Value
            Dim ヒート番号 As Integer = dgv.Rows(e.RowIndex).Cells("ヒート番号").Value

            Dim F601 As F601_名簿
            F601 = New F601_名簿
            F601.名簿表示(競技番号, 競技番号枝番, 種目順, ヒート番号)
            F601.Show()

        End If



    End Sub





    Private Sub PB_閉じる_Click(sender As Object, e As EventArgs) Handles PB_閉じる.Click
        Me.Dispose()
    End Sub
End Class