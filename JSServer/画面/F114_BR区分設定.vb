Public Class F114_BR区分設定


    Private 更新FLAG As Boolean

    'コンストラクタ
    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。


    End Sub

    Private Sub F114_BR区分設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False


        BR区分一覧更新（）


    End Sub

    'DGV_BRカテゴリ一覧を更新する
    Private Sub BR区分一覧更新()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ



        'データクリア
        Me.DGV_BR区分一覧.DataSource = Nothing
        Me.DGV_BR区分一覧.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_BR区分一覧.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)


        '列タイトル設定
        ' １列目 カテゴリ番号
        ' ２列目 カテゴリ名
        ' ３列目 カテゴリ表記名


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("カテゴリ番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド表記名", GetType(String)))

        tbl.Columns.Add(New DataColumn("勝_区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("勝_ラウンド番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("負_区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("負_ラウンド番号", GetType(String)))



        For i = 1 To マスタデータ.BR_グループマスタ.登録済みレコード数

            tbl.Rows.Add()
            Dim idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item("カテゴリ番号") = マスタデータ.BR_グループマスタ.リスト(i).カテゴリ番号
            tbl.Rows(idx).Item("区分番号") = マスタデータ.BR_グループマスタ.リスト(i).区分番号
            tbl.Rows(idx).Item("ラウンド番号") = マスタデータ.BR_グループマスタ.リスト(i).ラウンド番号
            tbl.Rows(idx).Item("ラウンド表記名") = マスタデータ.BR_グループマスタ.リスト(i).ラウンド表記名

            tbl.Rows(idx).Item("勝_区分番号") = マスタデータ.BR_グループマスタ.リスト(i).勝_区分番号
            tbl.Rows(idx).Item("勝_ラウンド番号") = マスタデータ.BR_グループマスタ.リスト(i).勝_ラウンド番号
            tbl.Rows(idx).Item("負_区分番号") = マスタデータ.BR_グループマスタ.リスト(i).負_区分番号
            tbl.Rows(idx).Item("負_ラウンド番号") = マスタデータ.BR_グループマスタ.リスト(i).負_ラウンド番号


        Next i


        '// DataGridViewにデータセットを設定
        Me.DGV_BR区分一覧.DataSource = tbl




        '===列幅の自動調整
        Me.DGV_BR区分一覧.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_BR区分一覧.AllowUserToResizeColumns = True

        '===行の高さの自動設定
        Me.DGV_BR区分一覧.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders

        'ラウンド設定列は読込み専用



        マスタデータ = Nothing

    End Sub


    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'BRカテゴリのレコード削除
        マスタデータ.BR_グループマスタ.Deleteレコード()

        Dim BRグループC As BR_グループ

        For i = 0 To Me.DGV_BR区分一覧.RowCount - 1

            If Me.DGV_BR区分一覧.Rows(i).Cells("カテゴリ番号").Value IsNot Nothing Then

                '入力チェック
                If Me.DGV_BR区分一覧.Rows(i).Cells("カテゴリ番号").Value Is DBNull.Value Then
                    MsgBox("カテゴリ番号が入力されていません。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_BR区分一覧.Rows(i).Cells("区分番号").Value Is DBNull.Value Then
                    MsgBox("区分番号が入力されていません。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_BR区分一覧.Rows(i).Cells("ラウンド番号").Value Is DBNull.Value Then
                    MsgBox("ラウンド番号が入力されていません。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_BR区分一覧.Rows(i).Cells("ラウンド表記名").Value Is DBNull.Value Then
                    MsgBox("ラウンド表記名が入力されていません。")
                    マスタデータ = Nothing
                    Exit Sub
                End If


                BRグループC = New BR_グループ

                BRグループC.カテゴリ番号 = CStr(Me.DGV_BR区分一覧.Rows(i).Cells("カテゴリ番号").Value).PadLeft(2, "0")
                BRグループC.区分番号 = CStr(Me.DGV_BR区分一覧.Rows(i).Cells("区分番号").Value).PadLeft(2, "0")
                BRグループC.ラウンド番号 = CStr(Me.DGV_BR区分一覧.Rows(i).Cells("ラウンド番号").Value).PadLeft(3, "0")
                BRグループC.ラウンド表記名 = Me.DGV_BR区分一覧.Rows(i).Cells("ラウンド表記名").Value

                If Me.DGV_BR区分一覧.Rows(i).Cells("勝_区分番号").Value Is DBNull.Value Then
                    BRグループC.勝_区分番号 = ""
                Else
                    BRグループC.勝_区分番号 = Me.DGV_BR区分一覧.Rows(i).Cells("勝_区分番号").Value
                End If

                If Me.DGV_BR区分一覧.Rows(i).Cells("勝_ラウンド番号").Value Is DBNull.Value Then
                    BRグループC.勝_ラウンド番号 = ""
                Else
                    BRグループC.勝_ラウンド番号 = Me.DGV_BR区分一覧.Rows(i).Cells("勝_ラウンド番号").Value
                End If

                If Me.DGV_BR区分一覧.Rows(i).Cells("負_区分番号").Value Is DBNull.Value Then
                    BRグループC.負_区分番号 = ""
                Else
                    BRグループC.負_区分番号 = Me.DGV_BR区分一覧.Rows(i).Cells("負_区分番号").Value
                End If

                If Me.DGV_BR区分一覧.Rows(i).Cells("負_ラウンド番号").Value Is DBNull.Value Then
                    BRグループC.負_ラウンド番号 = ""
                Else
                    BRグループC.負_ラウンド番号 = Me.DGV_BR区分一覧.Rows(i).Cells("負_ラウンド番号").Value
                End If


                マスタデータ.BR_グループマスタ.登録(BRグループC)
                End If
        Next i

        BRグループC = Nothing
        マスタデータ = Nothing


        BR区分一覧更新（）

        更新FLAG = False



    End Sub


    'セルが変更されたら
    Private Sub DGV_BRカテゴリ_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DGV_BR区分一覧.CurrentCellDirtyStateChanged



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
        BR区分一覧更新()
    End Sub

    Private Sub PB_結果クリア_Click(sender As Object, e As EventArgs) Handles PB_結果クリア.Click

        'SC_BR_Result.csv をクリアする


        If MsgBox("前回大会の結果をクリアしますがいいですか？", vbYesNo) = vbYes Then
            Dim マスタデータ As マスタデータ
            マスタデータ = New マスタデータ

            Dim SC_BR_Resultマスタ As SC_BR_Resultマスタ
            SC_BR_Resultマスタ = New SC_BR_Resultマスタ(マスタデータ.Z_システム設定.システムPath)

            Dim rc = SC_BR_Resultマスタ.ファイルクリア()

            If rc = 0 Then
                MsgBox("SC_BR_Result.csvをクリアしました。")
            Else
                MsgBox("SC_BR_Result.csvのクリアに失敗しました。")
            End If


            マスタデータ = Nothing


        End If


    End Sub
End Class