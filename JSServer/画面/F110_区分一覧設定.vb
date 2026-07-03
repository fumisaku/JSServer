Public Class F110_区分一覧設定

    Private 更新FLAG As Boolean
    Private CB_カテゴリ_リスト(5, 2)

    'コンストラクタ
    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。


    End Sub

    Private Sub F110_区分一覧設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False


        区分一覧更新（）


    End Sub

    'DGV_区分一覧を更新する
    Private Sub 区分一覧更新()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ



        'データクリア
        Me.DGV_区分一覧.DataSource = Nothing
        Me.DGV_区分一覧.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_区分一覧.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)


        '列タイトル設定
        ' １列目 区分番号
        ' ２列目 区分記号
        ' ３列目 区分名
        ' ４列目 カテゴリ
        ' ５列目 審判G
        ' ６列目 選手M
        ' ７列目 
        ' ８列目～１ｘ列目　ジャッジ人数＋減点ジャッジ
        '　PCS ４つ


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分記号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分名", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分表記名", GetType(String)))
        tbl.Columns.Add(New DataColumn("カテゴリ", GetType(String)))
        tbl.Columns.Add(New DataColumn("審判G", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("選手M", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド設定", GetType(String)))

        For i = 1 To マスタデータ.B_区分マスタ.登録済みレコード数

            tbl.Rows.Add()
            Dim idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item("区分番号") = マスタデータ.B_区分マスタ.リスト(i).区分番号
            tbl.Rows(idx).Item("区分記号") = マスタデータ.B_区分マスタ.リスト(i).区分記号
            tbl.Rows(idx).Item("区分名") = マスタデータ.B_区分マスタ.リスト(i).区分名
            tbl.Rows(idx).Item("区分表記名") = マスタデータ.B_区分マスタ.リスト(i).区分表記名


            Select Case マスタデータ.B_区分マスタ.リスト(i).カテゴリ
                Case "S"
                    tbl.Rows(idx).Item("カテゴリ") = "スタンダード"
                Case "L"
                    tbl.Rows(idx).Item("カテゴリ") = "ラテン"

                Case "10"
                    tbl.Rows(idx).Item("カテゴリ") = "10ダンス"
                Case "団体"
                    tbl.Rows(idx).Item("カテゴリ") = "団体結果"

                Case "総合"
                    tbl.Rows(idx).Item("カテゴリ") = "総合結果"

                Case "Oth"
                    tbl.Rows(idx).Item("カテゴリ") = "その他"

                Case Else
                    tbl.Rows(idx).Item("カテゴリ") = "”
            End Select



            tbl.Rows(idx).Item("審判G") = マスタデータ.B_区分マスタ.リスト(i).担当審判グループ
            tbl.Rows(idx).Item("選手M") = マスタデータ.B_区分マスタ.リスト(i).使用する選手マスタ

            'ラウンドの設定がされているか’有無で表示
            Dim Flag As Boolean = False
            For r = 1 To マスタデータ.C_ラウンドマスタ.登録済みレコード数
                If マスタデータ.C_ラウンドマスタ.リスト(r).区分番号 = マスタデータ.B_区分マスタ.リスト(i).区分番号 Then
                    Flag = True
                    r = マスタデータ.C_ラウンドマスタ.登録済みレコード数
                End If
            Next r

            If Flag = True Then
                tbl.Rows(idx).Item("ラウンド設定") = "有"
            Else
                tbl.Rows(idx).Item("ラウンド設定") = "無"
            End If
        Next i


        '// DataGridViewにデータセットを設定
        Me.DGV_区分一覧.DataSource = tbl



        'コンボボックスの設定
        ' カテゴリ
        Dim CB_カテゴリ As New DataGridViewComboBoxColumn()
        'ComboBoxのリストに表示する項目を設定する
        CB_カテゴリ.Items.Add("スタンダード")
        CB_カテゴリ.Items.Add("ラテン")
        CB_カテゴリ.Items.Add("10ダンス")
        CB_カテゴリ.Items.Add("団体結果")
        CB_カテゴリ.Items.Add("総合結果")
        CB_カテゴリ.Items.Add("その他")
        CB_カテゴリ.Items.Add("")


        '表示する列の名前を設定する
        CB_カテゴリ.DataPropertyName = Me.DGV_区分一覧.Columns("カテゴリ").DataPropertyName

        '現在列が存在している位置に挿入する
        Me.DGV_区分一覧.Columns.Insert(Me.DGV_区分一覧.Columns("カテゴリ").Index, CB_カテゴリ)
        '今までの列を削除する
        Me.DGV_区分一覧.Columns.Remove("カテゴリ")
        '挿入した列の名前を変更する
        CB_カテゴリ.Name = "カテゴリ"
        '列のタイトルを設定
        Me.DGV_区分一覧.Columns("カテゴリ").HeaderText = "カテゴリ"


        '表示スタイルを、ComboBox形式に変更
        CB_カテゴリ.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox


        '選択されたセルだけ コンボボックスを表示
        CB_カテゴリ.DisplayStyleForCurrentCellOnly = True






        '===列幅の自動調整
        Me.DGV_区分一覧.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_区分一覧.AllowUserToResizeColumns = True

        '===行の高さの自動設定
        Me.DGV_区分一覧.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders

        'ラウンド設定列は読込み専用
        Me.DGV_区分一覧.Columns("ラウンド設定").ReadOnly = True


        マスタデータ = Nothing

    End Sub

    Private Sub PB_編集_Click(sender As Object, e As EventArgs) Handles PB_編集.Click

        '行が選択されていない時は何もしない
        If Me.DGV_区分一覧.CurrentRow IsNot Nothing Then

            Dim 選択_区分番号 As String = Me.DGV_区分一覧.CurrentRow.Cells(0).Value

            If 選択_区分番号 <> "" Then

                If Me.DGV_区分一覧.CurrentRow.Cells("カテゴリ").Value = "団体結果" Then
                    Dim F150 As F150_団体区分設定
                    F150 = New F150_団体区分設定(選択_区分番号, Me.DGV_区分一覧.CurrentRow.Cells(3).Value)
                    F150.Show()

                ElseIf Me.DGV_区分一覧.CurrentRow.Cells("カテゴリ").Value = "総合結果" Then
                    Dim F160 As F160_総合結果設定
                    F160 = New F160_総合結果設定(選択_区分番号)
                    F160.Show()


                Else
                    Dim F111 As F111_区分設定
                    F111 = New F111_区分設定
                    F111.データ表示(選択_区分番号)
                    F111.Show()


                End If


            End If
        Else
            MsgBox("編集対象行を選択してください。")
        End If

    End Sub



    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'B_区分マスタのレコード削除
        マスタデータ.B_区分マスタ.Deleteレコード()

        Dim 区分C As B_区分

        For i = 0 To Me.DGV_区分一覧.RowCount - 1

            If Me.DGV_区分一覧.Rows(i).Cells("区分番号").Value IsNot Nothing Then

                '入力チェック
                If Me.DGV_区分一覧.Rows(i).Cells("区分番号").Value Is DBNull.Value Then
                    MsgBox("区分番号が入力されていません。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_区分一覧.Rows(i).Cells("区分記号").Value Is DBNull.Value Then
                    Me.DGV_区分一覧.Rows(i).Cells("区分記号").Value = ""
                End If
                If Me.DGV_区分一覧.Rows(i).Cells("区分名").Value Is DBNull.Value Then
                    MsgBox("区分名が入力されていません。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_区分一覧.Rows(i).Cells("区分表記名").Value Is DBNull.Value Then
                    Me.DGV_区分一覧.Rows(i).Cells("区分表記名").Value = Me.DGV_区分一覧.Rows(i).Cells("区分名").Value
                End If
                If Me.DGV_区分一覧.Rows(i).Cells("カテゴリ").Value Is DBNull.Value Then
                    MsgBox("カテゴリが入力されていません。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_区分一覧.Rows(i).Cells("審判G").Value Is DBNull.Value Then
                    Me.DGV_区分一覧.Rows(i).Cells("審判G").Value = 1
                End If
                If Me.DGV_区分一覧.Rows(i).Cells("選手M").Value Is DBNull.Value Then
                    Me.DGV_区分一覧.Rows(i).Cells("選手M").Value = ""
                End If

                区分C = New B_区分

                区分C.区分番号 = CStr(Me.DGV_区分一覧.Rows(i).Cells("区分番号").Value).PadLeft(2, "0")
                区分C.区分記号 = Me.DGV_区分一覧.Rows(i).Cells("区分記号").Value
                区分C.区分名 = Me.DGV_区分一覧.Rows(i).Cells("区分名").Value
                区分C.区分表記名 = Me.DGV_区分一覧.Rows(i).Cells("区分表記名").Value

                '区分C.カテゴリ = Me.DGV_区分一覧.Rows(i).Cells("カテゴリ").Value

                Select Case Me.DGV_区分一覧.Rows(i).Cells("カテゴリ").Value
                    Case "スタンダード"
                        区分C.カテゴリ = "S"
                    Case "ラテン"
                        区分C.カテゴリ = "L"
                    Case "10ダンス"
                        区分C.カテゴリ = "10"
                    Case "団体結果"
                        区分C.カテゴリ = "団体"
                    Case "総合結果"
                        区分C.カテゴリ = "総合"
                    Case "その他"
                        区分C.カテゴリ = "Oth"
                    Case Else
                        MsgBox("カテゴリを正しく選択してください。「" & Me.DGV_区分一覧.Rows(i).Cells("カテゴリ").Value & "」は無効です。")
                        Exit Sub
                End Select

                区分C.担当審判グループ = Me.DGV_区分一覧.Rows(i).Cells("審判G").Value
                区分C.使用する選手マスタ = CStr(Me.DGV_区分一覧.Rows(i).Cells("選手M").Value).PadLeft(2, "0")

                マスタデータ.B_区分マスタ.登録(区分C)
            End If
        Next i

        区分C = Nothing
        マスタデータ = Nothing


        区分一覧更新（）

        更新FLAG = False

    End Sub


    'セルが変更されたら
    Private Sub DGV_区分一覧_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DGV_区分一覧.CurrentCellDirtyStateChanged


        'If Me.DGV_競技種目.CurrentCellAddress.X = 0 AndAlso Me.DGV_競技種目.IsCurrentCellDirty Then
        'コミットする
        'Me.DGV_競技種目.CommitEdit(DataGridViewDataErrorContexts.Commit)
        'End If

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
        区分一覧更新（）
    End Sub

    Private Sub PB_ブレイキン設定_Click(sender As Object, e As EventArgs) Handles PB_ブレイキン設定.Click

        Dim F113_BRカテゴリ設定 As F113_BRカテゴリ設定
        F113_BRカテゴリ設定 = New F113_BRカテゴリ設定

        F113_BRカテゴリ設定.Show()


    End Sub

    Private Sub PB_総合結果作成_Click(sender As Object, e As EventArgs) Handles PB_総合結果作成.Click


        '行が選択されていない時は何もしない
        If Me.DGV_区分一覧.CurrentRow IsNot Nothing Then

            Dim 選択_区分番号 As String = Me.DGV_区分一覧.CurrentRow.Cells(0).Value

            If 選択_区分番号 <> "" Then

                Dim マスタデータ As マスタデータ
                マスタデータ = New マスタデータ


                Dim SC_J_区分結果 As SC_J_区分結果
                SC_J_区分結果 = New SC_J_区分結果(マスタデータ.Z_システム設定.Comp_filepath)
                SC_J_区分結果.集計(選択_区分番号)
                SC_J_区分結果.JSON書き出し()

                マスタデータ = Nothing


                Dim F530 As F530_区分結果表示
                F530 = New F530_区分結果表示(選択_区分番号)
                F530.Show()

            End If
        Else
            MsgBox("対象区分を選択してください。")
        End If


    End Sub


End Class
