Public Class F151_団体集計方法設定
    Private Sub F151_団体集計方法設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub New(団体集計方法名 As String)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        DGV作成(団体集計方法名)

    End Sub


    Private Sub DGV作成(団体集計方法名 As String)

        Dim マスタデータ = New マスタデータ
        'ファイルの読み込み
        Dim 団体集計方法_J = New 団体集計方法_J(マスタデータ.Z_システム設定.システムPath)
        団体集計方法_J.集計方法名 = 団体集計方法名
        団体集計方法_J = 団体集計方法_J.JSON読み込み()


        Me.TB_集計方法名.Text = 団体集計方法名
        Me.TB_倍率.Text = 団体集計方法_J.倍率
        Me.TB_上位ポジション数.Text = 団体集計方法_J.上位ポジション数

        If 団体集計方法_J.同点_点数按分 = True Then
            Me.RB_同点按分_Yes.Checked = True
        Else
            Me.RB_同点按分_No.Checked = True
        End If

        If 団体集計方法_J.点数重複加算 = True Then
            Me.RB_重複加算_Yes.Checked = True
        Else
            Me.RB_重複加算_No.Checked = True

        End If


        'データクリア
        Me.DGV.DataSource = Nothing
        Me.DGV.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("ラウンド名", GetType(String)))
        tbl.Columns.Add(New DataColumn("方式", GetType(String)))
        tbl.Columns.Add(New DataColumn("順位点", GetType(Decimal)))
        tbl.Columns.Add(New DataColumn("最低点", GetType(Decimal)))


        If 団体集計方法_J Is Nothing Then
            '新規


        Else
            '読込
            For r = 1 To 団体集計方法_J.設定ラウンド数
                tbl.Rows.Add()
                tbl.Rows(r - 1).Item("ラウンド名") = 団体集計方法_J.ラウンド_J(r).ラウンド名
                tbl.Rows(r - 1).Item("方式") = 団体集計方法_J.ラウンド_J(r).方式
                tbl.Rows(r - 1).Item("順位点") = 団体集計方法_J.ラウンド_J(r).順位点
                tbl.Rows(r - 1).Item("最低点") = 団体集計方法_J.ラウンド_J(r).最低点
            Next r
        End If



        '// DataGridViewにデータセットを設定
        Me.DGV.DataSource = tbl


        '===列幅の自動調整
        Me.DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.DGV.AllowUserToResizeColumns = True

        マスタデータ = Nothing
        団体集計方法_J = Nothing



    End Sub


    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click
        Me.Close()
    End Sub

    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim マスタデータ = New マスタデータ

        Dim 団体集計方法_J = New 団体集計方法_J(マスタデータ.Z_システム設定.システムPath)

        If Me.TB_集計方法名.Text <> "" Then
            団体集計方法_J.集計方法名 = Me.TB_集計方法名.Text
        Else
            MsgBox("集計方法名を入力してください。")
            Exit Sub
        End If

        If Me.TB_倍率.Text <> "" Then
            If IsNumeric(Me.TB_倍率.Text) Then
                団体集計方法_J.倍率 = CDec(Me.TB_倍率.Text)
            Else
                MsgBox("倍率には数値を入力してください。")
                Exit Sub
            End If
        Else
            MsgBox("倍率を入力してください。")
            Exit Sub
        End If

        If Me.TB_上位ポジション数.Text <> "" Then
            If IsNumeric(Me.TB_上位ポジション数.Text) Then
                団体集計方法_J.上位ポジション数 = CInt(Me.TB_上位ポジション数.Text)
            Else
                MsgBox("上位ポジション数には数値を入力してください。")
                Exit Sub
            End If
        Else
            MsgBox("上位ポジション数を入力してください。全て対象にする場合は0を入力してください。")
            Exit Sub
        End If

        If Me.RB_重複加算_Yes.Checked = True Then
            団体集計方法_J.点数重複加算 = True
        Else
            団体集計方法_J.点数重複加算 = False
        End If

        If Me.RB_同点按分_Yes.Checked = True Then
            団体集計方法_J.同点_点数按分 = True
        Else
            団体集計方法_J.同点_点数按分 = False
        End If

        '行数を算出
        Dim 設定ラウンド数 As Integer = 0
        For Each dr As DataGridViewRow In Me.DGV.Rows
            If IsDBNull(dr.Cells("ラウンド名").Value) = False Then
                If dr.Cells("ラウンド名").Value <> "" And dr.Cells("ラウンド名").Value <> " " Then
                    設定ラウンド数 = 設定ラウンド数 + 1
                End If
            End If
        Next

        Dim r As Integer = 0
        団体集計方法_J.ラウンド_J_初期化(設定ラウンド数)
        For Each dr As DataGridViewRow In Me.DGV.Rows
            If IsDBNull(dr.Cells("ラウンド名").Value) = False Then
                If dr.Cells("ラウンド名").Value <> "" And dr.Cells("ラウンド名").Value <> " " Then
                    r = r + 1
                    団体集計方法_J.ラウンド_J(r).ラウンド名 = dr.Cells("ラウンド名").Value
                    団体集計方法_J.ラウンド_J(r).方式 = dr.Cells("方式").Value
                    団体集計方法_J.ラウンド_J(r).順位点 = dr.Cells("順位点").Value
                    団体集計方法_J.ラウンド_J(r).最低点 = dr.Cells("最低点").Value

                End If
            End If
        Next


        団体集計方法_J.JSON書き出し()




        マスタデータ = Nothing


    End Sub
End Class