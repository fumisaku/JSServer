Public Class F153_団体結果

    Private 団体結果_J As 団体結果_J



    Private Sub F153_団体結果_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False




    End Sub


    Public Sub New(団体区分番号 As String)


        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。


        団体結果読込(団体区分番号)

        If 団体結果_J Is Nothing Then
            MsgBox("団体区分番号:" & 団体区分番号 & "の結果が作成されていません。")
        End If


        Me.LB_団体区分名.Text = 団体結果_J.団体区分番号 & " " & 団体結果_J.団体区分名 & "  集計結果"

        結果更新（）


    End Sub

    Private Sub 団体結果読込(団体区分番号 As String)

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ


        団体結果_J = New 団体結果_J(マスタデータ.Z_システム設定.Comp_filepath)
        団体結果_J.団体区分番号 = 団体区分番号

        団体結果_J = 団体結果_J.JSON読み込み()

        マスタデータ = Nothing
    End Sub



    Private Sub 結果更新()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ



        'データクリア
        Me.DGV_結果.DataSource = Nothing
        Me.DGV_結果.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_結果.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '列タイトル設定
        ' １列目 区分番号
        ' ２列目 区分記号
        ' ３列目 区分名
        ' ４列目 カテゴリ
        ' ５列目 審判G


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("チーム名", GetType(String)))
        tbl.Columns.Add(New DataColumn("総合順位", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("総合得点", GetType(Decimal)))

        For k = 1 To 団体結果_J.区分数
            tbl.Columns.Add(New DataColumn(団体結果_J.区分定義_J(k).区分名, GetType(Decimal)))
        Next k

        'データセット

        For t = 1 To 団体結果_J.チーム数


            tbl.Rows.Add()
            Dim idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item("チーム名") = 団体結果_J.チーム結果_J(t).チーム名
            tbl.Rows(idx).Item("総合順位") = 団体結果_J.チーム結果_J(t).順位
            tbl.Rows(idx).Item("総合得点") = 団体結果_J.チーム結果_J(t).合計点



            For k = 1 To 団体結果_J.区分数
                tbl.Rows(idx).Item(団体結果_J.区分定義_J(k).区分名) = 団体結果_J.チーム結果_J(t).区分結果_J(k).区分得点
            Next k


        Next t



        '// DataGridViewにデータセットを設定
        Me.DGV_結果.DataSource = tbl






        '==列を非表示
        'Me.DGV_結果.Columns("区分名").Visible = False



        '===列幅の自動調整
        Me.DGV_結果.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_結果.AllowUserToResizeColumns = True

        '===行の高さの自動設定
        Me.DGV_結果.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders




        マスタデータ = Nothing

    End Sub

    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click

        Me.Close()

    End Sub

    Private Sub PB_区分結果_Click(sender As Object, e As EventArgs) Handles PB_区分結果.Click


        '行が選択されていない時は何もしない
        If Me.DGV_結果.CurrentCell IsNot Nothing Then

            Dim 選択Column番号 As Integer = Me.DGV_結果.CurrentCellAddress.X

            If 選択Column番号 >= 3 Then

                Dim 区分名 As String = Me.DGV_結果.Columns(選択Column番号).HeaderCell.Value
                Dim 区分番号 As String = ""

                '区分名から区分番号を探す
                For k = 1 To 団体結果_J.区分数
                    If 団体結果_J.区分定義_J(k).区分名 = 区分名 Then
                        区分番号 = 団体結果_J.区分定義_J(k).区分番号
                        k = 団体結果_J.区分数
                    End If
                Next k

                Dim F154 = New F154_団体区分結果(区分番号, 団体結果_J.団体区分番号)
                F154.Show()


            Else
                MsgBox("表示対象区分を選択してください。")

            End If
        Else
            MsgBox("表示対象区分を選択してください。")
        End If






    End Sub

    Private Sub PB_CSV書き出し_Click(sender As Object, e As EventArgs) Handles PB_CSV書き出し.Click

        Dim rc = 団体結果_J.CSV書き出し

        If rc = 0 Then
            MsgBox("CSV書き出し成功")
        Else
            MsgBox("CSV書き出し失敗！　書き出しができませんでした。")
        End If

    End Sub
End Class