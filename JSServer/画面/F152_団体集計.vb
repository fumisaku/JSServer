Imports System.ComponentModel
Imports System.Windows.Forms


Public Class F152_団体集計


    Private 団体区分番号 As String
    Private 団体区分表記名 As String

    Private Sub F152_団体集計_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False


        区分一覧更新（）

    End Sub

    Public Sub New(団体区分番号_ As String, 団体区分表記名_ As String)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        団体区分番号 = 団体区分番号_
        団体区分表記名 = 団体区分表記名_

        Me.LB_団体区分名.Text = 団体区分番号 & " " & 団体区分表記名

    End Sub


    'DGV_区分一覧を更新する
    Private Sub 区分一覧更新()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'イベントハンドらの登録
        AddHandler Me.DGV_区分一覧.CellFormatting, AddressOf Me.DGV_区分一覧_CellFormatting


        'データクリア
        Me.DGV_区分一覧.DataSource = Nothing
        Me.DGV_区分一覧.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_区分一覧.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '列タイトル設定
        ' １列目 区分番号
        ' ２列目 区分記号
        ' ３列目 区分名
        ' ４列目 カテゴリ
        ' ５列目 審判G


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("対象", GetType(Boolean)))
        tbl.Columns.Add(New DataColumn("区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分記号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分名", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分表記名", GetType(String)))
        tbl.Columns.Add(New DataColumn("カテゴリ", GetType(String)))
        tbl.Columns.Add(New DataColumn("団体集計方法", GetType(String)))
        tbl.Columns.Add(New DataColumn("競技ステータス", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分結果S", GetType(String)))




        For i = 1 To マスタデータ.K_団体区分マスタ.登録済みレコード数

            If マスタデータ.K_団体区分マスタ.リスト(i).団体区分番号 = 団体区分番号 Then

                If マスタデータ.K_団体区分マスタ.リスト(i).団体集計方法 <> "" Then


                    tbl.Rows.Add()
                    Dim idx = tbl.Rows.Count - 1
                    tbl.Rows(idx).Item("対象") = False
                    tbl.Rows(idx).Item("区分番号") = マスタデータ.K_団体区分マスタ.リスト(i).区分番号

                    Dim B区分 As B_区分
                    B区分 = マスタデータ.B_区分マスタ.Get区分C(マスタデータ.K_団体区分マスタ.リスト(i).区分番号)

                    tbl.Rows(idx).Item("区分記号") = B区分.区分記号
                    tbl.Rows(idx).Item("区分名") = B区分.区分名
                    tbl.Rows(idx).Item("区分表記名") = B区分.区分表記名


                    Select Case B区分.カテゴリ
                        Case "S"
                            tbl.Rows(idx).Item("カテゴリ") = "スタンダード"
                        Case "L"
                            tbl.Rows(idx).Item("カテゴリ") = "ラテン"

                        Case "10"
                            tbl.Rows(idx).Item("カテゴリ") = "10ダンス"

                        Case "総合"
                            tbl.Rows(idx).Item("カテゴリ") = "総合結果"

                        Case "団体"
                            tbl.Rows(idx).Item("カテゴリ") = "団体結果"

                        Case "Oth"
                            tbl.Rows(idx).Item("カテゴリ") = "その他"

                        Case Else
                            tbl.Rows(idx).Item("カテゴリ") = "”
                    End Select


                    tbl.Rows(idx).Item("団体集計方法") = マスタデータ.K_団体区分マスタ.リスト(i).団体集計方法

                    Dim 終了ラウンド番号 As String = マスタデータ.T_採点進行管理.Get_終了ラウンド(マスタデータ.K_団体区分マスタ.リスト(i).区分番号)

                    If 終了ラウンド番号 = "000" Then
                        tbl.Rows(idx).Item("競技ステータス") = "開始前"
                    ElseIf 終了ラウンド番号 = "400" Then
                        tbl.Rows(idx).Item("競技ステータス") = "終了"
                    Else
                        tbl.Rows(idx).Item("競技ステータス") = マスタデータ.Get_ラウンド名(終了ラウンド番号) & "終了"
                    End If

                    '区分結果ファイルの読み込み
                    Dim SC_J_区分結果 = New SC_J_区分結果(マスタデータ.Z_システム設定.Comp_filepath)
                    SC_J_区分結果.区分番号 = マスタデータ.K_団体区分マスタ.リスト(i).区分番号
                    SC_J_区分結果 = SC_J_区分結果.JSON読み込み()

                    If SC_J_区分結果 IsNot Nothing Then
                        tbl.Rows(idx).Item("区分結果S") = SC_J_区分結果.Get_確定ラウンド() & "済"
                    Else
                        tbl.Rows(idx).Item("区分結果S") = "区分結果未作成"
                    End If




                End If

            End If

        Next i



        '// DataGridViewにデータセットを設定
        Me.DGV_区分一覧.DataSource = tbl






        '==列を非表示
        Me.DGV_区分一覧.Columns("区分名").Visible = False



        '===列幅の自動調整
        Me.DGV_区分一覧.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_区分一覧.AllowUserToResizeColumns = True

        '===行の高さの自動設定
        Me.DGV_区分一覧.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders




        マスタデータ = Nothing

    End Sub



    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click

        Me.Close()

    End Sub

    Private Sub PB_区分結果作成_Click(sender As Object, e As EventArgs) Handles PB_区分結果作成.Click



        'ProgressDialogオブジェクトを作成する
        Dim pd As New FP01_進捗状況("区分結果作成中", New DoWorkEventHandler(AddressOf 区分結果作成), 100)

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

    'DoWorkイベントハンドラ
    Private Sub 区分結果作成(ByVal sender As Object, ByVal e As DoWorkEventArgs)

        '===進捗ダイアログ初期処理、====
        Dim bw As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        'パラメータを取得する
        Dim stopTime As Integer = CInt(e.Argument)
        '===進捗ダイアログ初期処理、ここまで====




        '===チェックボックスで選択されたものを対象になる、====
        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim count As Integer = 0

        Dim 全数 As Integer = 0

        For Each dr As DataGridViewRow In Me.DGV_区分一覧.Rows

            If dr.Cells("対象").Value = True Then
                全数 = 全数 + 1
            End If
        Next


        For Each dr As DataGridViewRow In Me.DGV_区分一覧.Rows

            If dr.Cells("対象").Value = True Then

                If dr.Cells("区分番号").Value IsNot DBNull.Value Then

                    'キャンセルされたか調べる ==進捗ダイアログ
                    If bw.CancellationPending Then
                        'キャンセルされたとき
                        e.Cancel = True
                        Return
                    End If


                    Dim SC_J_区分結果 As SC_J_区分結果
                    SC_J_区分結果 = New SC_J_区分結果(マスタデータ.Z_システム設定.Comp_filepath)
                    SC_J_区分結果.集計(dr.Cells("区分番号").Value)
                    SC_J_区分結果.JSON書き出し()

                    count = count + 1
                End If
            End If

            'ProgressChangedイベントハンドラを呼び出し、進捗ダイアログ
            'コントロールの表示を変更する
            Dim 終了P As Decimal = （count / 全数） * 100

            bw.ReportProgress(終了P, 終了P.ToString("0.#") & "% 終了しました")


        Next

        マスタデータ = Nothing

        MsgBox(count & "件の区分結果を作成しました。")




        '結果を設定する　進捗ダイアログ
        e.Result = stopTime * 100


    End Sub



    Private Sub PB_更新_Click(sender As Object, e As EventArgs) Handles PB_更新.Click
        区分一覧更新()
    End Sub

    Private Sub PB_区分結果確認_Click(sender As Object, e As EventArgs) Handles PB_区分結果確認.Click

        '行が選択されていない時は何もしない
        If Me.DGV_区分一覧.CurrentRow IsNot Nothing Then

            Dim 選択_区分番号 As String = Me.DGV_区分一覧.CurrentRow.Cells("区分番号").Value

            If 選択_区分番号 <> "" Then

                Dim F530 As F530_区分結果表示
                F530 = New F530_区分結果表示(選択_区分番号)

                If F530.File有り = True Then
                    F530.Show()
                Else
                    MsgBox("選択された区分は、まだ区分結果ファイルが作成されていません。")

                End If


            End If
        Else
            MsgBox("対象区分を選択してください。")
        End If



    End Sub



    'セルの色付け等
    Private Sub DGV_区分一覧_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs)



        Dim dgv As DataGridView = CType(sender, DataGridView)


        If dgv.Columns(e.ColumnIndex).Name = "競技ステータス" AndAlso TypeOf e.Value Is String Then


            If e.Value = "終了" Then
                e.CellStyle.BackColor = Color.Cyan
            Else
                e.CellStyle.BackColor = Color.White
            End If


        ElseIf dgv.Columns(e.ColumnIndex).Name = "区分結果S" AndAlso TypeOf e.Value Is String Then

            If e.Value = "決勝済" Then
                e.CellStyle.BackColor = Color.Cyan
            Else
                e.CellStyle.BackColor = Color.White

            End If


        End If



    End Sub

    Private Sub PB_団体集計_Click(sender As Object, e As EventArgs) Handles PB_団体集計.Click



        'ProgressDialogオブジェクトを作成する
        Dim pd As New FP01_進捗状況("団体集計中", New DoWorkEventHandler(AddressOf 団体集計メイン), 100)

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

    Private Sub 団体集計メイン(ByVal sender As Object, ByVal e As DoWorkEventArgs)

        '===進捗ダイアログ初期処理、====
        Dim bw As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        'パラメータを取得する
        Dim stopTime As Integer = CInt(e.Argument)
        '===進捗ダイアログ初期処理、ここまで====




        '===チェックボックスで選択されたものを対象になる、====
        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ
        Dim 全数 As Integer = 0
        Dim count As Integer = 0
        Dim 区分実施数 As Integer = 0

        For Each dr As DataGridViewRow In Me.DGV_区分一覧.Rows
            If dr.Cells("対象").Value = True Then
                全数 = 全数 + 1
            End If
        Next
        全数 = 全数 * 2


        For Each dr As DataGridViewRow In Me.DGV_区分一覧.Rows

            If dr.Cells("対象").Value = True Then
                If dr.Cells("区分番号").Value IsNot DBNull.Value Then

                    'キャンセルされたか調べる ==進捗ダイアログ
                    If bw.CancellationPending Then
                        'キャンセルされたとき
                        e.Cancel = True
                        Return
                    End If



                    Dim SC_J_区分結果 As SC_J_区分結果
                    SC_J_区分結果 = New SC_J_区分結果(マスタデータ.Z_システム設定.Comp_filepath)
                    SC_J_区分結果.集計(dr.Cells("区分番号").Value)
                    SC_J_区分結果.JSON書き出し()

                    count = count + 1
                    区分実施数 = 区分実施数 + 1


                    'ProgressChangedイベントハンドラを呼び出し、進捗ダイアログ
                    'コントロールの表示を変更する
                    Dim 終了P As Decimal = （count / 全数） * 100

                    bw.ReportProgress(終了P, 終了P.ToString("0.#") & "% 終了しました")
                End If

            End If
        Next

        '団体結果_Jの作成
        Dim 団体結果_J = New 団体結果_J(マスタデータ.Z_システム設定.Comp_filepath)
        団体結果_J.団体区分番号 = 団体区分番号

        団体結果_J = 団体結果_J.JSON読み込み()

        If 団体結果_J Is Nothing Then
            団体結果_J = New 団体結果_J(マスタデータ.Z_システム設定.Comp_filepath)
            団体結果_J.団体区分番号 = 団体区分番号
            団体結果_J.団体区分名 = 団体区分表記名
            '団体結果_J.初期設定()

            '団体結果に　区分を追加
            For Each dr As DataGridViewRow In Me.DGV_区分一覧.Rows


                If dr.Cells("区分番号").Value IsNot DBNull.Value Then
                        If dr.Cells("区分番号").Value IsNot Nothing Then

                            If dr.Cells("カテゴリ").Value <> "団体結果" Then

                                'キャンセルされたか調べる ==進捗ダイアログ
                                If bw.CancellationPending Then
                                    'キャンセルされたとき
                                    e.Cancel = True
                                    Return
                                End If


                                Dim 倍率 As Decimal = マスタデータ.K_団体区分マスタ.Get区分C(dr.Cells("区分番号").Value).倍率
                            団体結果_J.区分追加(dr.Cells("区分番号").Value, dr.Cells("区分表記名").Value, dr.Cells("区分記号").Value, dr.Cells("団体集計方法").Value, 倍率)

                        End If
                        End If
                    End If


            Next

        Else


        End If


        For Each dr As DataGridViewRow In Me.DGV_区分一覧.Rows

            If dr.Cells("対象").Value = True Then
                If dr.Cells("区分番号").Value IsNot DBNull.Value Then
                    団体集計_区分(dr.Cells("区分番号").Value, dr.Cells("団体集計方法").Value, 団体結果_J)


                    'ProgressChangedイベントハンドラを呼び出し、進捗ダイアログ
                    'コントロールの表示を変更する
                    count = count + 1
                    Dim 終了P As Decimal = （count / 全数） * 100
                    bw.ReportProgress(終了P, 終了P.ToString("0.#") & "% 終了しました")
                End If

            End If
        Next



        マスタデータ = Nothing

        MsgBox(count & "件の区分結果を作成しました。")



        '結果を設定する　進捗ダイアログ
        e.Result = stopTime * 100

    End Sub



    Private Sub 団体集計_区分(ByVal 区分番号 As String, ByVal 団体採点方式 As String, ByRef 団体結果_J As 団体結果_J)
        '区分番号　と、採点方式 を渡すと、SC_J_区分結果ファイルを読み込んで、　団体結果_J に記入して、書き出す。。
        '

        Dim マスタデータ = New マスタデータ

        '団体集計方法_Jの準備
        Dim 団体集計方法_J = New 団体集計方法_J(マスタデータ.Z_システム設定.システムPath)
        団体集計方法_J.集計方法名 = 団体採点方式
        団体集計方法_J = 団体集計方法_J.JSON読み込み()

        If 団体集計方法_J Is Nothing Then
            MsgBox("エラー：" & 団体採点方式 & " が定義されていません。")
            Exit Sub
        End If


        'SC_J_区分結果の準備
        Dim SC_J_区分結果 = New SC_J_区分結果(マスタデータ.Z_システム設定.Comp_filepath)
        SC_J_区分結果.区分番号 = 区分番号
        SC_J_区分結果 = SC_J_区分結果.JSON読み込み

        If SC_J_区分結果 Is Nothing Then
            MsgBox("エラー：区分" & 区分番号 & " の区分結果が作成されていません。")
            Exit Sub
        End If

        '当該区分の選手毎の得点計算
        Dim 団体集計_区分選手得点() As 団体集計_区分選手得点
        ReDim 団体集計_区分選手得点(SC_J_区分結果.選手数)
        For s = 1 To SC_J_区分結果.選手数
            団体集計_区分選手得点(s) = New 団体集計_区分選手得点
        Next s

        Dim チームCount() As チームCount
        ReDim チームCount(SC_J_区分結果.選手数)
        For t = 1 To SC_J_区分結果.選手数
            チームCount(t) = New チームCount
        Next t


        Dim 倍率 As Decimal = マスタデータ.K_団体区分マスタ.Get区分C(区分番号).倍率


        For s = 1 To SC_J_区分結果.選手数

            If SC_J_区分結果.SC_J_区分結果_選手(s).ラウンド IsNot Nothing Then

                '同点決勝の時はラウンド名は、本選のラウンド名を入力する必要がある。団体集計設定に同決ラウンドの点数は設定されていないため
                Dim 進出ラウンド名 As String = ""
                If Strings.Right(SC_J_区分結果.Get_ラウンド番号(SC_J_区分結果.SC_J_区分結果_選手(s).ラウンド), 1) = "1" Then
                    進出ラウンド名 = マスタデータ.Get_ラウンド名(Strings.Left(SC_J_区分結果.Get_ラウンド番号(SC_J_区分結果.SC_J_区分結果_選手(s).ラウンド), 2) & "0")
                Else
                    進出ラウンド名 = SC_J_区分結果.SC_J_区分結果_選手(s).ラウンド
                End If


                Dim 素点 As Decimal = 団体集計方法_J.Get_素点(進出ラウンド名, SC_J_区分結果.SC_J_区分結果_選手(s).総合順位, SC_J_区分結果)


                Dim 発見FLAG As Boolean = False
                    Dim 登録件数 As Integer = 0
                    '最大ポジション数の確認
                    For t = 1 To UBound(チームCount)
                        If チームCount(t).チーム名 = SC_J_区分結果.SC_J_区分結果_選手(s).カップル所属 Then
                            チームCount(t).カウント = チームCount(t).カウント + 1

                        If チームCount(t).カウント > 団体集計方法_J.上位ポジション数 And 団体集計方法_J.上位ポジション数 <> 0 Then
                            素点 = 0
                        End If

                        発見FLAG = True
                            t = UBound(チームCount)
                        End If

                        If チームCount(t).チーム名 = "" Then
                            登録件数 = t
                            t = UBound(チームCount)
                        End If
                    Next t

                    If 発見FLAG = False Then
                        チームCount(登録件数).チーム名 = SC_J_区分結果.SC_J_区分結果_選手(s).カップル所属
                        チームCount(登録件数).カウント = 1
                    End If

                    '団体集計_区分選手得点に挿入
                    団体集計_区分選手得点(s).選手M番号 = マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ
                    団体集計_区分選手得点(s).背番号 = SC_J_区分結果.SC_J_区分結果_選手(s).背番号
                    団体集計_区分選手得点(s).団体得点 = 素点 * 倍率
                    団体集計_区分選手得点(s).順位 = SC_J_区分結果.SC_J_区分結果_選手(s).総合順位
                    団体集計_区分選手得点(s).チーム名 = SC_J_区分結果.SC_J_区分結果_選手(s).カップル所属

                End If

        Next s



        '団体結果_Jへ書き込む
        Dim 区分C As B_区分 = マスタデータ.B_区分マスタ.Get区分C(区分番号)

        Dim 区分IND As Integer = 団体結果_J.Get_区分IND(区分番号, 区分C.区分表記名, 区分C.区分記号, 団体採点方式, 倍率)


        'まず既存区分の得点をクリア
        団体結果_J.区分クリア（区分番号）




        '得点加算
        For s = 1 To UBound(団体集計_区分選手得点)

            Dim チームFindFlag As Boolean = False

            For t = 1 To 団体結果_J.チーム数
                If 団体結果_J.チーム結果_J(t).チーム名 = 団体集計_区分選手得点(s).チーム名 Then
                    チームFindFlag = True

                    団体結果_J.チーム結果_J(t).区分結果_J(区分IND).区分得点 = 団体結果_J.チーム結果_J(t).区分結果_J(区分IND).区分得点 + 団体集計_区分選手得点(s).団体得点

                    Dim 選手C = マスタデータ.選手マスタ.Get選手C_by背番号(団体集計_区分選手得点(s).選手M番号, 団体集計_区分選手得点(s).背番号)
                    If 選手C IsNot Nothing Then
                        団体結果_J.チーム結果_J(t).区分結果_J(区分IND).選手結果追加(選手C.List番号, 選手C.背番号, 選手C.リーダー表記名, 選手C.パートナ表記名, 団体集計_区分選手得点(s).団体得点)
                    End If

                    選手C = Nothing
                    t = 団体結果_J.チーム数
                End If
            Next t

            If チームFindFlag = False Then
                If 団体集計_区分選手得点(s).チーム名 IsNot Nothing Then

                    Dim チームIND = 団体結果_J.チーム追加(団体集計_区分選手得点(s).チーム名)

                    団体結果_J.チーム結果_J(チームIND).区分結果_J(区分IND).区分得点 = 団体結果_J.チーム結果_J(チームIND).区分結果_J(区分IND).区分得点 + 団体集計_区分選手得点(s).団体得点

                    Dim 選手C = マスタデータ.選手マスタ.Get選手C_by背番号(団体集計_区分選手得点(s).選手M番号, 団体集計_区分選手得点(s).背番号)

                    If 選手C IsNot Nothing Then
                        団体結果_J.チーム結果_J(チームIND).区分結果_J(区分IND).選手結果追加(選手C.List番号, 選手C.背番号, 選手C.リーダー表記名, 選手C.パートナ表記名, 団体集計_区分選手得点(s).団体得点)

                    End If

                    選手C = Nothing
                End If



            End If


        Next s

        団体結果_J.集計()


        団体結果_J.JSON書き出し()


        SC_J_区分結果 = Nothing


        団体集計方法_J = Nothing

        マスタデータ = Nothing


    End Sub


    Class 団体集計_区分選手得点
        Public Property 選手M番号 As String
        Public Property 背番号 As String
        Public Property 順位 As Integer
        Public Property 団体得点 As Decimal
        Public Property チーム名 As String

        Public Property リーダー名 As String
        Public Property パートナー名 As String



    End Class

    Class チームCount
        Public Property チーム名 As String
        Public Property カウント As Integer


    End Class

    Private Sub PB_団体結果表示_Click(sender As Object, e As EventArgs) Handles PB_団体結果表示.Click

        Dim F153 As F153_団体結果
        F153 = New F153_団体結果(団体区分番号)
        F153.Show()


    End Sub


    Private チェック済みFLAG As Boolean
    Private Sub PB_全チェック_Click_1(sender As Object, e As EventArgs) Handles PB_全チェック.Click


        If チェック済みFLAG = False Then
            For Each dr As DataGridViewRow In Me.DGV_区分一覧.Rows
                If dr.Cells("区分番号").Value IsNot Nothing Then
                    dr.Cells("対象").Value = True
                End If
            Next


            チェック済みFLAG = True
        Else
            For Each dr As DataGridViewRow In Me.DGV_区分一覧.Rows

                dr.Cells("対象").Value = False
            Next
            チェック済みFLAG = False

        End If

    End Sub
End Class

