Public Class FGM02_関連端末一覧

    '関連端末の一覧

    Private GM As F_GM

    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Private Sub FGM02_関連端末一覧_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False


    End Sub

    Public Sub 一覧更新(TCPClient() As TCPClient, GM_ As F_GM)

        GM = GM_

        'データクリア
        Me.DGV_JS一覧.DataSource = Nothing
        Me.DGV_JS一覧.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_JS一覧.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)


        '列タイトル設定
        ' １列目 カテゴリ番号
        ' ２列目 カテゴリ名
        ' ３列目 カテゴリ表記名


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("No", GetType(String)))
        tbl.Columns.Add(New DataColumn("端末名", GetType(String)))
        '   tbl.Columns.Add(New DataColumn("区分番号", GetType(String)))
        '  tbl.Columns.Add(New DataColumn("ラウンド番号", GetType(String)))
        ' tbl.Columns.Add(New DataColumn("ジャッジ記号", GetType(String)))
        ' tbl.Columns.Add(New DataColumn("種目記号", GetType(String)))
        'tbl.Columns.Add(New DataColumn("ヒート番号", GetType(String)))

        For i = 1 To UBound(TCPClient)

            If TCPClient(i) IsNot Nothing Then

                tbl.Rows.Add()
                Dim idx = tbl.Rows.Count - 1

                tbl.Rows(idx).Item("No") = i
                tbl.Rows(idx).Item("端末名") = TCPClient(i).端末名
                '                tbl.Rows(idx).Item("区分番号") = TCPClient(i).区分番号
                '               tbl.Rows(idx).Item("ラウンド番号") = TCPClient(i).ラウンド番号
                '              tbl.Rows(idx).Item("ジャッジ記号") = TCPClient(i).ジャッジ記号
                '             tbl.Rows(idx).Item("種目記号") = TCPClient(i).種目記号
                '            tbl.Rows(idx).Item("ヒート番号") = TCPClient(i).ヒート番号

            End If
        Next i


        '// DataGridViewにデータセットを設定
        Me.DGV_JS一覧.DataSource = tbl




        '===列幅の自動調整
        Me.DGV_JS一覧.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_JS一覧.AllowUserToResizeColumns = True

        '===行の高さの自動設定
        Me.DGV_JS一覧.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders

        'ラウンド設定列は読込み専用

    End Sub


    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click

        Me.Close()

    End Sub

    Private Sub PB_更新_Click(sender As Object, e As EventArgs) Handles PB_更新.Click

        一覧更新(GM.Client_list, GM)

    End Sub

End Class