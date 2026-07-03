Public Class F601_名簿

    Private マスタデータ As マスタデータ


    Private ソロFLAG As Boolean
    Private ソロヒート番号 As Integer


    Private Sub F601_名簿_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False

    End Sub


    Public Sub 決勝結果表示(区分番号, ラウンド番号)

        マスタデータ = New マスタデータ

        Me.TB_タイトル.Text = "入賞者"

        Me.TB_競技名.Text = マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & " " &
                            マスタデータ.Get_ラウンド名(ラウンド番号) & vbCrLf

        Dim 種目記号リスト() = Nothing
        Dim 種目数 As Integer = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        For d = 1 To 種目数
            Me.TB_種目名.Text = 種目記号リスト(d) & " "
        Next d

        Me.TB_ヒート番号.Text = ""


        DGV_結果名簿作成(区分番号, ラウンド番号)

        マスタデータ = Nothing


    End Sub

    '名簿を表示する
    Public Sub 名簿表示(競技番号 As String, 競技番号枝番 As String, 種目順 As Integer, ヒート番号 As Integer)

        マスタデータ = New マスタデータ

        Me.TB_タイトル.Text = "出場者名簿"
        Dim 区分番号 As String = ""
        Dim ラウンド番号 As String = ""
        マスタデータ.T_採点進行管理.Get_区分ラウンド番号(競技番号, 競技番号枝番, 区分番号, ラウンド番号)

        Me.TB_競技名.Text = 競技番号 & " " &
                            マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & " " &
                            マスタデータ.Get_ラウンド名(ラウンド番号) & vbCrLf

        Dim 種目記号リスト() = Nothing
        Dim 種目数 As Integer = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        Me.TB_種目名.Text = 種目順 & "/" & 種目数 & "種目目 " & マスタデータ.Z_システム設定.Get_種目名称(マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目順).種目記号).種目名_J

        マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
        Dim ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(種目順)
        Me.TB_ヒート番号.Text = "第 " & ヒート番号 & " ヒート/全 " & ヒート数 & "H"


        DGV_名簿作成(区分番号, ラウンド番号, 種目順, ヒート番号)

        マスタデータ = Nothing

    End Sub


    Private Sub DGV_結果名簿作成(区分番号 As String, ラウンド番号 As String)

        'データクリア
        Me.DGV_名簿.DataSource = Nothing
        Me.DGV_名簿.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_名簿.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_名簿.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable
        'tbl.Columns.Add(New DataColumn("No", GetType(Integer)))　　'名簿だけ
        tbl.Columns.Add(New DataColumn("順位", GetType(Integer)))  '結果だけ
        tbl.Columns.Add(New DataColumn("背番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("氏名", GetType(String)))
        tbl.Columns.Add(New DataColumn("フリガナ", GetType(String)))
        tbl.Columns.Add(New DataColumn("所属", GetType(String)))
        tbl.Columns.Add(New DataColumn("点数", GetType(String)))　　'結果だけ

        Dim 採点結果 As 採点結果_C
        採点結果 = New 採点結果_C(区分番号, ラウンド番号)


        Dim 行番号 As Integer = 0

        For s = 1 To 採点結果.出場選手数

            Dim 選手マスタ番号 As String = マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ
            Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタ番号, 採点結果.背番号(採点結果.Get選手番号(s)))

            tbl.Rows.Add()
            tbl.Rows(行番号).Item("順位") = 採点結果.総合順位表記(採点結果.Get選手番号(s))
            tbl.Rows(行番号).Item("背番号") = 選手.背番号
            tbl.Rows(行番号).Item("氏名") = 選手.リーダー表記名
            tbl.Rows(行番号).Item("フリガナ") = 選手.リーダーフリガナ
            tbl.Rows(行番号).Item("所属") = 選手.リーダー所属名
            tbl.Rows(行番号).Item("点数") = 採点結果.総合得点(採点結果.Get選手番号(s))


            tbl.Rows.Add()
            tbl.Rows(行番号 + 1).Item("順位") = 採点結果.総合順位表記(採点結果.Get選手番号(s))
            tbl.Rows(行番号 + 1).Item("背番号") = 選手.背番号
            tbl.Rows(行番号 + 1).Item("氏名") = 選手.パートナ表記名
            tbl.Rows(行番号 + 1).Item("フリガナ") = 選手.パートナフリガナ
            tbl.Rows(行番号 + 1).Item("所属") = 選手.パートナ所属名
            tbl.Rows(行番号 + 1).Item("点数") = 採点結果.総合得点(採点結果.Get選手番号(s))

            行番号 = 行番号 + 2

        Next s



        '// DataGridViewにデータセットを設定
        Me.DGV_名簿.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_名簿.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_名簿.AllowUserToResizeColumns = True

        Me.DGV_名簿.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        'Ｎｏ列のFontを変更
        'Me.DGV_名簿.Columns("No").DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)



    End Sub

    Private Sub DGV_名簿作成(区分番号 As String, ラウンド番号 As String, 種目順 As Integer, ヒート番号 As Integer)

        'データクリア
        Me.DGV_名簿.DataSource = Nothing
        Me.DGV_名簿.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_名簿.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_名簿.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable
        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))　　'名簿だけ
        'tbl.Columns.Add(New DataColumn("順位", GetType(Integer)))  '結果だけ
        tbl.Columns.Add(New DataColumn("背番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("氏名", GetType(String)))
        tbl.Columns.Add(New DataColumn("フリガナ", GetType(String)))
        tbl.Columns.Add(New DataColumn("所属", GetType(String)))
        'tbl.Columns.Add(New DataColumn("点数", GetType(String)))　　'結果だけ


        Dim 背番号リスト() = Nothing
        マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
        Dim 選手数 As Integer = 0

        'ソロ判定
        ソロFLAG = False
        ソロヒート番号 = ヒート番号

        If マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目順).SG種別 = "S" Then
            ソロFLAG = True
        End If

        If ソロFLAG = True Then

            選手数 = マスタデータ.C_ラウンドマスタ.出場組数(区分番号, ラウンド番号)
        Else
            選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(種目順, ヒート番号, 背番号リスト)

        End If

        Dim 行番号 As Integer = 0

        For s = 1 To 選手数

            Dim 選手マスタ番号 As String = マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ
            Dim 選手 As 選手

            If ソロFLAG = True Then
                マスタデータ.E_ヒート表マスタ.Get_背番号リスト(種目順, s, 背番号リスト)
                選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタ番号, 背番号リスト(1))

            Else
                選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタ番号, 背番号リスト(s))

            End If

            tbl.Rows.Add()
            tbl.Rows(行番号).Item("No") = s
            tbl.Rows(行番号).Item("背番号") = 選手.背番号
            tbl.Rows(行番号).Item("氏名") = 選手.リーダー表記名
            tbl.Rows(行番号).Item("フリガナ") = 選手.リーダーフリガナ
            tbl.Rows(行番号).Item("所属") = 選手.リーダー所属名

            tbl.Rows.Add()
            tbl.Rows(行番号 + 1).Item("No") = s
            tbl.Rows(行番号 + 1).Item("背番号") = 選手.背番号
            tbl.Rows(行番号 + 1).Item("氏名") = 選手.パートナ表記名
            tbl.Rows(行番号 + 1).Item("フリガナ") = 選手.パートナフリガナ
            tbl.Rows(行番号 + 1).Item("所属") = 選手.パートナ所属名

            行番号 = 行番号 + 2

        Next s



        '// DataGridViewにデータセットを設定
        Me.DGV_名簿.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_名簿.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_名簿.AllowUserToResizeColumns = True

        Me.DGV_名簿.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        'Ｎｏ列のFontを変更
        Me.DGV_名簿.Columns("No").DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


    End Sub


    Sub MyForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' 選択されているセルをなくす
        Me.DGV_名簿.CurrentCell = Nothing
    End Sub


    '====セルの結合処理　ここから
    ' 指定したセルと1つ上のセルの値を比較
    Function IsTheSameCellValue(ByVal column As Integer, ByVal row As Integer) As Boolean

        Dim cell1 As DataGridViewCell = DGV_名簿(column, row)
        Dim cell2 As DataGridViewCell = DGV_名簿(column, row - 1)

        If cell1.Value = Nothing Or cell2.Value = Nothing Then
            Return False
        End If

        'カラム番号が、0,1 以外のときはなにもしない
        If column > 1 Then
            Return False
        End If


        ' ここでは文字列としてセルの値を比較
        If cell1.Value.ToString() = cell2.Value.ToString() Then
            Return True
        Else
            Return False
        End If

    End Function

    ' DataGridViewのCellFormattingイベント・ハンドラ
    Sub dgv_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGV_名簿.CellFormatting

        ' 1行目については何もしない
        If e.RowIndex = 0 Then
            Return
        End If

        'カラム番号が、0,1 以外のときはなにもしない
        If e.ColumnIndex > 1 Then
            Return
        End If

        If IsTheSameCellValue(e.ColumnIndex, e.RowIndex) Then
            e.Value = ""
            e.FormattingApplied = True ' 以降の書式設定は不要
        End If
    End Sub

    ' DataGridViewのCellPaintingイベント・ハンドラ
    Sub dgv_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DGV_名簿.CellPainting

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
            e.AdvancedBorderStyle.Top = DGV_名簿.AdvancedCellBorderStyle.Top
        End If

        '行番号が奇数の時、セルの下側の境界線を太く設定
        If e.RowIndex Mod 2 = 0 Then
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.InsetDouble

        End If


        'ソロの時は該当のヒート番号に色をつける
        If ソロFLAG = True Then
            Me.DGV_名簿.Rows((ソロヒート番号 - 1) * 2 + 0).Cells(0).Style.BackColor = Color.Cyan
            Me.DGV_名簿.Rows((ソロヒート番号 - 1) * 2 + 0).Cells(1).Style.BackColor = Color.Cyan
            Me.DGV_名簿.Rows((ソロヒート番号 - 1) * 2 + 0).Cells(2).Style.BackColor = Color.Cyan
            Me.DGV_名簿.Rows((ソロヒート番号 - 1) * 2 + 0).Cells(3).Style.BackColor = Color.Cyan
            Me.DGV_名簿.Rows((ソロヒート番号 - 1) * 2 + 0).Cells(4).Style.BackColor = Color.Cyan

            Me.DGV_名簿.Rows((ソロヒート番号 - 1) * 2 + 1).Cells(0).Style.BackColor = Color.Cyan
            Me.DGV_名簿.Rows((ソロヒート番号 - 1) * 2 + 1).Cells(1).Style.BackColor = Color.Cyan
            Me.DGV_名簿.Rows((ソロヒート番号 - 1) * 2 + 1).Cells(2).Style.BackColor = Color.Cyan
            Me.DGV_名簿.Rows((ソロヒート番号 - 1) * 2 + 1).Cells(3).Style.BackColor = Color.Cyan
            Me.DGV_名簿.Rows((ソロヒート番号 - 1) * 2 + 1).Cells(4).Style.BackColor = Color.Cyan

        End If


    End Sub


    Private Sub PB_閉じる_Click(sender As Object, e As EventArgs) Handles PB_閉じる.Click
        Me.Dispose()
    End Sub
End Class