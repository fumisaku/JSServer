Imports System.ComponentModel
Imports System.Windows.Forms

Public Class F902_支援システム集計


    Private マスタデータ As マスタデータ

    Private 支援システムパス As String
    Private 競技会パス As String


    Public Sub New(支援システムパス_, 競技会パス_)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        支援システムパス = 支援システムパス_
        競技会パス = 競技会パス_

    End Sub


    Private Sub F902_支援システム集計_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False

        DGV更新()

    End Sub


    Private Sub DGV更新()

        マスタデータ = New マスタデータ


        'データクリア
        Me.DGV_ラウンド一覧.DataSource = Nothing
        Me.DGV_ラウンド一覧.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_ラウンド一覧.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_ラウンド一覧.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("対象", GetType(Boolean)))
        tbl.Columns.Add(New DataColumn("競技番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("枝番", GetType(String)))
        tbl.Columns.Add(New DataColumn("ステータス", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分記号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分名", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド", GetType(String)))
        tbl.Columns.Add(New DataColumn("カテゴリ", GetType(String)))
        tbl.Columns.Add(New DataColumn("リアル", GetType(String)))
        tbl.Columns.Add(New DataColumn("Heat数", GetType(String)))
        tbl.Columns.Add(New DataColumn("UP数", GetType(String)))
        tbl.Columns.Add(New DataColumn("ヒート割", GetType(String)))
        tbl.Columns.Add(New DataColumn("種目", GetType(String)))
        tbl.Columns.Add(New DataColumn("審判G", GetType(String)))
        tbl.Columns.Add(New DataColumn("選手M", GetType(String)))

        For i = 1 To マスタデータ.T_採点進行管理.登録済みレコード数
            Dim 採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.リスト(i)

            If 採点進行C IsNot Nothing Then
                Dim 区分C As B_区分 = マスタデータ.B_区分マスタ.Get区分C(採点進行C.区分番号)
                Dim ラウンドC As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(採点進行C.区分番号, 採点進行C.ラウンド番号)

                tbl.Rows.Add()
                tbl.Rows(i - 1).Item("対象") = False
                tbl.Rows(i - 1).Item("競技番号") = 採点進行C.競技番号
                tbl.Rows(i - 1).Item("枝番") = 採点進行C.競技番号枝番
                tbl.Rows(i - 1).Item("区分番号") = 採点進行C.区分番号
                tbl.Rows(i - 1).Item("区分名") = 区分C.区分表記名
                tbl.Rows(i - 1).Item("ラウンド番号") = 採点進行C.ラウンド番号
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
        Me.DGV_ラウンド一覧.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_ラウンド一覧.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.DGV_ラウンド一覧.AllowUserToResizeColumns = True


        '==ラウンド番号列を非表示
        Me.DGV_ラウンド一覧.Columns("ラウンド番号").Visible = False
        Me.DGV_ラウンド一覧.Columns("リアル").Visible = False



    End Sub

    Private Sub PB_更新_Click(sender As Object, e As EventArgs) Handles PB_更新.Click
        DGV更新()
    End Sub

    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click
        マスタデータ = Nothing
        Me.Close()

    End Sub

    Private チェック済みFLAG As Boolean

    Private Sub PB_全チェック_Click(sender As Object, e As EventArgs) Handles PB_全チェック.Click


        If チェック済みFLAG = False Then
            For Each dr As DataGridViewRow In Me.DGV_ラウンド一覧.Rows

                dr.Cells("対象").Value = True
            Next
            チェック済みFLAG = True
        Else
            For Each dr As DataGridViewRow In Me.DGV_ラウンド一覧.Rows

                dr.Cells("対象").Value = False
            Next
            チェック済みFLAG = False

        End If





    End Sub

    Private Sub PB_取り込み_Click(sender As Object, e As EventArgs) Handles PB_取り込み.Click


        'ProgressDialogオブジェクトを作成する
        Dim pd As New FP01_進捗状況("結果取り込み実施中", New DoWorkEventHandler(AddressOf 取り込み実行), 100)

        '進行状況ダイアログを表示する
        Dim result As DialogResult = pd.ShowDialog(Me)
        '結果を取得する
        If result = DialogResult.Cancel Then
            MessageBox.Show("キャンセルされました")
        ElseIf result = DialogResult.Abort Then
            'エラー情報を取得する
            Dim ex As Exception = pd.Error
            MessageBox.Show("エラー: " + ex.Message)
        ElseIf result = DialogResult.OK Then
            '結果を取得する
            Dim stopTime As Integer = CInt(pd.Result)
            MessageBox.Show("成功しました: " & stopTime.ToString())
        End If

        '後始末
        pd.Dispose()


    End Sub

    Private Sub 取り込み実行(ByVal sender As Object, ByVal e As DoWorkEventArgs)


        '===進捗ダイアログ初期処理、====
        Dim bw As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        'パラメータを取得する
        Dim stopTime As Integer = CInt(e.Argument)
        '===進捗ダイアログ初期処理、ここまで====

        Dim 全数 As Integer = 0
        For Each dr As DataGridViewRow In Me.DGV_ラウンド一覧.Rows
            If dr.Cells("対象").Value = True Then
                全数 = 全数 + 1
            End If
        Next




        Dim JS_変換 As JS_変換_C
        JS_変換 = New JS_変換_C(支援システムパス, 競技会パス)
        Dim マスタデータ = New マスタデータ

        Dim count As Integer = 0

        For Each dr As DataGridViewRow In Me.DGV_ラウンド一覧.Rows

            'キャンセルされたか調べる ==進捗ダイアログ
            If bw.CancellationPending Then
                'キャンセルされたとき
                e.Cancel = True
                Return
            End If


            If dr.Cells("対象").Value = True Then
                Dim RC = JS_変換.Heat結果変換(dr.Cells("区分番号").Value, dr.Cells("ラウンド番号").Value)

                'ステータス更新
                If RC = 0 Then
                    '採点結果がある場合
                    Dim 採点結果 = New 採点結果_C(dr.Cells("区分番号").Value, dr.Cells("ラウンド番号").Value)


                    Dim 現採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(dr.Cells("区分番号").Value, dr.Cells("ラウンド番号").Value)

                    現採点進行C.ステータス = "採点済み"
                    マスタデータ.T_採点進行管理.登録(現採点進行C)

                    count = count + 1

                ElseIf RC = 1 Then
                    'ヒート表だけできている場合
                    Dim 現採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(dr.Cells("区分番号").Value, dr.Cells("ラウンド番号").Value)

                    現採点進行C.ステータス = "ヒート表作成済み"
                    マスタデータ.T_採点進行管理.登録(現採点進行C)

                    count = count + 1

                ElseIf RC = 2 Then
                    'ヒート表も無い場合
                    Dim 現採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(dr.Cells("区分番号").Value, dr.Cells("ラウンド番号").Value)

                    現採点進行C.ステータス = "準備前"
                    マスタデータ.T_採点進行管理.登録(現採点進行C)
                Else

                End If

            End If

            'ProgressChangedイベントハンドラを呼び出し、進捗ダイアログ
            'コントロールの表示を変更する
            Dim 終了P As Decimal = （count / 全数） * 100

            bw.ReportProgress(終了P, 終了P.ToString("0.#") & "% 終了しました")


        Next

        MsgBox(count & "件の取り込みを行いました。")


        JS_変換 = Nothing
        マスタデータ = Nothing



        '結果を設定する　進捗ダイアログ
        e.Result = stopTime * 100
    End Sub


End Class