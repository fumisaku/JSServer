Public Class F121_採点進行確認

    Private マスタデータ As マスタデータ

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        マスタデータ = New マスタデータ

    End Sub

    Private Sub F121_採点進行確認_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False



        競技番号確認画面()
    End Sub

    Private Sub 競技番号確認画面()


        'Dim マスタデータ As マスタデータ
        'マスタデータ = New マスタデータ


        'データクリア
        Me.DGV_採点進行管理.DataSource = Nothing
        Me.DGV_採点進行管理.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_採点進行管理.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_採点進行管理.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("競技番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("枝番", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分記号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分名", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド", GetType(String)))
        tbl.Columns.Add(New DataColumn("カテゴリ", GetType(String)))
        tbl.Columns.Add(New DataColumn("リアル", GetType(String)))
        tbl.Columns.Add(New DataColumn("Heat数", GetType(String)))
        tbl.Columns.Add(New DataColumn("UP数", GetType(String)))
        tbl.Columns.Add(New DataColumn("ヒート割", GetType(String)))
        tbl.Columns.Add(New DataColumn("種目", GetType(String)))
        tbl.Columns.Add(New DataColumn("審判G", GetType(String)))
        tbl.Columns.Add(New DataColumn("選手M", GetType(String)))
        tbl.Columns.Add(New DataColumn("ステータス", GetType(String)))

        For i = 1 To マスタデータ.T_採点進行管理.登録済みレコード数
            Dim 採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.リスト(i)

            If 採点進行C IsNot Nothing Then
                Dim 区分C As B_区分 = マスタデータ.B_区分マスタ.Get区分C(採点進行C.区分番号)
                Dim ラウンドC As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(採点進行C.区分番号, 採点進行C.ラウンド番号)

                'Ver1.02.18で追加
                If ラウンドC Is Nothing Then
                    MsgBox("競技番号:" & 採点進行C.競技番号 & "は定義されていません。 進行設定を確認してください。 対象の区分番号、ラウンド番号は、" & 採点進行C.区分番号 & ":" & 採点進行C.ラウンド番号)
                    Exit Sub
                End If


                tbl.Rows.Add()
                tbl.Rows(i - 1).Item("競技番号") = 採点進行C.競技番号
                tbl.Rows(i - 1).Item("枝番") = 採点進行C.競技番号枝番
                tbl.Rows(i - 1).Item("区分番号") = 採点進行C.区分番号
                tbl.Rows(i - 1).Item("区分名") = 区分C.区分表記名
                tbl.Rows(i - 1).Item("ラウンド") = マスタデータ.Get_ラウンド名(採点進行C.ラウンド番号)
                tbl.Rows(i - 1).Item("カテゴリ") = 区分C.カテゴリ
                tbl.Rows(i - 1).Item("リアル") = 採点進行C.リアルタイムFLAG
                tbl.Rows(i - 1).Item("Heat数") = ラウンドC.ヒート数
                tbl.Rows(i - 1).Item("UP数") = ラウンドC.UP予定数
                tbl.Rows(i - 1).Item("ヒート割") = ラウンドC.ヒート割方式

                Dim 種目記号リスト() = Nothing
                Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(採点進行C.区分番号, 採点進行C.ラウンド番号, 種目記号リスト)
                Dim 種目記号 As String = ""

                For s = 1 To 種目数
                    種目記号 = 種目記号 & 種目記号リスト(s) & " "
                Next s

                tbl.Rows(i - 1).Item("種目") = 種目記号

                tbl.Rows(i - 1).Item("審判G") = ラウンドC.担当審判グループ
                tbl.Rows(i - 1).Item("選手M") = 区分C.使用する選手マスタ
                tbl.Rows(i - 1).Item("ステータス") = 採点進行C.ステータス

                区分C = Nothing
                ラウンドC = Nothing

            End If
        Next i



        '// DataGridViewにデータセットを設定
        Me.DGV_採点進行管理.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_採点進行管理.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_採点進行管理.AllowUserToResizeColumns = True



        '並び替えができないようにする ソート禁止
        'For Each c As DataGridViewColumn In Me.DGV_採点進行管理.Columns
        'c.SortMode = DataGridViewColumnSortMode.NotSortable
        'Next c

        'マスタデータ = Nothing



    End Sub


    '行が選択されたら
    Private Sub DGV_採点進行管理_RowEnter(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DGV_採点進行管理.RowEnter

        If e.RowIndex >= 0 Then

            Dim 選択区分番号 As String = Me.DGV_採点進行管理(2, e.RowIndex).Value

            For i = 0 To Me.DGV_採点進行管理.RowCount - 1
                If Me.DGV_採点進行管理(2, i).Value = 選択区分番号 Then
                    Me.DGV_採点進行管理.Rows(i).DefaultCellStyle.BackColor = Color.Cyan
                Else
                    Me.DGV_採点進行管理.Rows(i).DefaultCellStyle.BackColor = Nothing

                End If
            Next i

        End If

        'If Me.DGV_競技種目.CurrentCellAddress.X = 0 AndAlso Me.DGV_競技種目.IsCurrentCellDirty Then
        'コミットする
        'Me.DGV_競技種目.CommitEdit(DataGridViewDataErrorContexts.Commit)
        'End If


    End Sub




    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click

        マスタデータ = Nothing
        Me.Close()

    End Sub
End Class