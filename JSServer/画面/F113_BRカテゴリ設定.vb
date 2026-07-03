Public Class F113_BRカテゴリ設定

    Private 更新FLAG As Boolean

    'コンストラクタ
    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。


    End Sub

    Private Sub F113_BRカテゴリ設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False


        BRカテゴリ設定更新（）


    End Sub

    'DGV_BRカテゴリ一覧を更新する
    Private Sub BRカテゴリ設定更新()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ



        'データクリア
        Me.DGV_BRカテゴリ.DataSource = Nothing
        Me.DGV_BRカテゴリ.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_BRカテゴリ.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)


        '列タイトル設定
        ' １列目 カテゴリ番号
        ' ２列目 カテゴリ名
        ' ３列目 カテゴリ表記名
        ' ４列目 ラウンド方式


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("カテゴリ番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("カテゴリ名", GetType(String)))
        tbl.Columns.Add(New DataColumn("カテゴリ表記名", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド方式", GetType(String)))

        For i = 1 To マスタデータ.BR_カテゴリマスタ.登録済みレコード数

            tbl.Rows.Add()
            Dim idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item("カテゴリ番号") = マスタデータ.BR_カテゴリマスタ.リスト(i).カテゴリ番号
            tbl.Rows(idx).Item("カテゴリ名") = マスタデータ.BR_カテゴリマスタ.リスト(i).カテゴリ名
            tbl.Rows(idx).Item("カテゴリ表記名") = マスタデータ.BR_カテゴリマスタ.リスト(i).カテゴリ表記名
            tbl.Rows(idx).Item("ラウンド方式") = マスタデータ.BR_カテゴリマスタ.リスト(i).ラウンド方式

        Next i


        '// DataGridViewにデータセットを設定
        Me.DGV_BRカテゴリ.DataSource = tbl




        '===列幅の自動調整
        Me.DGV_BRカテゴリ.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_BRカテゴリ.AllowUserToResizeColumns = True

        '===行の高さの自動設定
        Me.DGV_BRカテゴリ.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders

        'ラウンド設定列は読込み専用



        マスタデータ = Nothing

    End Sub


    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'BRカテゴリのレコード削除
        マスタデータ.BR_カテゴリマスタ.Deleteレコード()

        Dim BRカテゴリC As BR_カテゴリ

        For i = 0 To Me.DGV_BRカテゴリ.RowCount - 1

            If Me.DGV_BRカテゴリ.Rows(i).Cells("カテゴリ番号").Value IsNot Nothing Then

                '入力チェック
                If Me.DGV_BRカテゴリ.Rows(i).Cells("カテゴリ番号").Value Is DBNull.Value Then
                    MsgBox("カテゴリ番号が入力されていません。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_BRカテゴリ.Rows(i).Cells("カテゴリ名").Value Is DBNull.Value Then
                    Me.DGV_BRカテゴリ.Rows(i).Cells("カテゴリ名").Value = ""
                End If
                If Me.DGV_BRカテゴリ.Rows(i).Cells("カテゴリ表記名").Value Is DBNull.Value Then
                    MsgBox("カテゴリ表記名が入力されていません。")
                    マスタデータ = Nothing
                    Exit Sub
                End If


                BRカテゴリC = New BR_カテゴリ

                BRカテゴリC.カテゴリ番号 = CStr(Me.DGV_BRカテゴリ.Rows(i).Cells("カテゴリ番号").Value).PadLeft(2, "0")
                BRカテゴリC.カテゴリ名 = Me.DGV_BRカテゴリ.Rows(i).Cells("カテゴリ名").Value
                BRカテゴリC.カテゴリ表記名 = Me.DGV_BRカテゴリ.Rows(i).Cells("カテゴリ表記名").Value
                BRカテゴリC.ラウンド方式 = Me.DGV_BRカテゴリ.Rows(i).Cells("ラウンド方式").Value


                マスタデータ.BR_カテゴリマスタ.登録(BRカテゴリC)
            End If
        Next i

        BRカテゴリC = Nothing
        マスタデータ = Nothing


        BRカテゴリ設定更新（）

        更新FLAG = False



    End Sub


    'セルが変更されたら
    Private Sub DGV_BRカテゴリ_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DGV_BRカテゴリ.CurrentCellDirtyStateChanged



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

    Private Sub PB_更新_Click(sender As Object, e As EventArgs) Handles PB_更新.Click
        BRカテゴリ設定更新()
    End Sub

    Private Sub PB_BR区分設定_Click(sender As Object, e As EventArgs) Handles PB_BR区分設定.Click

        Dim F114_BR区分設定 As F114_BR区分設定
        F114_BR区分設定 = New F114_BR区分設定

        F114_BR区分設定.Show()

    End Sub
End Class