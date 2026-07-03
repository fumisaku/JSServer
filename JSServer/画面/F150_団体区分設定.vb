Public Class F150_団体区分設定

    Private 更新FLAG As Boolean

    Private 団体区分番号 As String
    Private 団体区分表記名 As String

    Private Sub F150_団体区分設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load


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


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分記号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分名", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分表記名", GetType(String)))
        tbl.Columns.Add(New DataColumn("カテゴリ", GetType(String)))
        tbl.Columns.Add(New DataColumn("団体集計方法", GetType(String)))
        tbl.Columns.Add(New DataColumn("倍率", GetType(Decimal)))




        'Kマスターがある時

        For i = 1 To マスタデータ.B_区分マスタ.登録済みレコード数


            tbl.Rows.Add()
            Dim idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item("区分番号") = マスタデータ.B_区分マスタ.リスト(i).区分番号

            'K_区分を検索
            Dim K団体区分 As K_団体区分 = Nothing
            For k = 1 To マスタデータ.K_団体区分マスタ.登録済みレコード数
                If マスタデータ.K_団体区分マスタ.リスト(k).団体区分番号 = 団体区分番号 And
                        マスタデータ.K_団体区分マスタ.リスト(k).区分番号 = マスタデータ.B_区分マスタ.リスト(i).区分番号 Then
                    'Kマスターがある時

                    K団体区分 = マスタデータ.K_団体区分マスタ.リスト(k)
                    k = マスタデータ.K_団体区分マスタ.登録済みレコード数
                End If
            Next k




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

                Case "総合"
                    tbl.Rows(idx).Item("カテゴリ") = "総合結果"

                Case "団体"
                    tbl.Rows(idx).Item("カテゴリ") = "団体結果"

                Case "Oth"
                    tbl.Rows(idx).Item("カテゴリ") = "その他"

                Case Else
                    tbl.Rows(idx).Item("カテゴリ") = "”
            End Select

            If K団体区分 Is Nothing Then
                tbl.Rows(idx).Item("団体集計方法") = ""
                tbl.Rows(idx).Item("倍率") = 1
            Else
                tbl.Rows(idx).Item("団体集計方法") = K団体区分.団体集計方法
                tbl.Rows(idx).Item("倍率") = K団体区分.倍率
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
        CB_カテゴリ.Items.Add("総合結果")
        CB_カテゴリ.Items.Add("団体結果")
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



        'コンボボックスの設定
        ' 団体集計方法
        Dim CB_団体集計方法 As New DataGridViewComboBoxColumn()
        'ComboBoxのリストに表示する項目を設定する

        Dim 団体集計方法 As Object = Nothing
        Get_団体集計方式一覧(マスタデータ.Z_システム設定.システムPath, 団体集計方法)

        For i = 0 To UBound(団体集計方法)
            CB_団体集計方法.Items.Add(団体集計方法(i))
        Next i

        '表示する列の名前を設定する
        CB_団体集計方法.DataPropertyName = Me.DGV_区分一覧.Columns("団体集計方法").DataPropertyName

        '現在列が存在している位置に挿入する
        Me.DGV_区分一覧.Columns.Insert(Me.DGV_区分一覧.Columns("団体集計方法").Index, CB_団体集計方法)
        '今までの列を削除する
        Me.DGV_区分一覧.Columns.Remove("団体集計方法")
        '挿入した列の名前を変更する
        CB_団体集計方法.Name = "団体集計方法"
        '列のタイトルを設定
        Me.DGV_区分一覧.Columns("団体集計方法").HeaderText = "団体集計方法"


        '表示スタイルを、ComboBox形式に変更
        CB_団体集計方法.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox


        '選択されたセルだけ コンボボックスを表示
        CB_団体集計方法.DisplayStyleForCurrentCellOnly = True





        '==列を非表示
        Me.DGV_区分一覧.Columns("区分名").Visible = False


        '===列幅の自動調整
        Me.DGV_区分一覧.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_区分一覧.AllowUserToResizeColumns = True

        '===行の高さの自動設定
        Me.DGV_区分一覧.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders




        マスタデータ = Nothing

    End Sub



    Private Sub Get_団体集計方式一覧(ByVal パス名, ByRef 採点方式)
        '団体集計方式の名前をパスからファイルを取得する。
        '入力 システムパス
        '出力 採点方式 配列

        Dim file As String() = System.IO.Directory.GetFiles(パス名, "J_団体集計方法_J_*.json")    ', System.IO.SearchOption.AllDirectories)

        ReDim 採点方式(UBound(file))

        For i = 0 To UBound(file)
            Dim l As Integer = file(i).Length - 7 - パス名.length - 1 - 9
            採点方式(i) = file(i).Substring(パス名.length + 4 + 8, l)
        Next i

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

    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim 区分C As K_団体区分

        For i = 0 To Me.DGV_区分一覧.RowCount - 1

            If Me.DGV_区分一覧.Rows(i).Cells("区分番号").Value IsNot Nothing Then

                '入力チェック
                If Me.DGV_区分一覧.Rows(i).Cells("区分番号").Value Is DBNull.Value Then
                    MsgBox("区分番号が入力されていません。")
                    マスタデータ = Nothing
                    Exit Sub
                End If


                区分C = New K_団体区分
                区分C.団体区分番号 = 団体区分番号
                区分C.区分番号 = CStr(Me.DGV_区分一覧.Rows(i).Cells("区分番号").Value).PadLeft(2, "0")
                If Me.DGV_区分一覧.Rows(i).Cells("団体集計方法").Value Is DBNull.Value Then
                    区分C.団体集計方法 = ""

                Else
                    区分C.団体集計方法 = Me.DGV_区分一覧.Rows(i).Cells("団体集計方法").Value
                End If
                区分C.倍率 = Me.DGV_区分一覧.Rows(i).Cells("倍率").Value

                マスタデータ.K_団体区分マスタ.登録(区分C)
            End If
        Next i

        区分C = Nothing
        マスタデータ = Nothing


        区分一覧更新（）

        更新FLAG = False

    End Sub

    Private Sub PB_団体集計方法設定_Click(sender As Object, e As EventArgs) Handles PB_団体集計方法設定.Click

        '行が選択されていない時は何もしない
        If Me.DGV_区分一覧.CurrentRow IsNot Nothing Then

            Dim 選択_区分番号 As String = Me.DGV_区分一覧.CurrentRow.Cells(0).Value

            If 選択_区分番号 <> "" Then

                If Me.DGV_区分一覧.CurrentRow.Cells("団体集計方法").Value <> "" Then


                    Dim 団体集計方法名 As String = Me.DGV_区分一覧.CurrentRow.Cells("団体集計方法").Value

                    Dim F151 As F151_団体集計方法設定
                    F151 = New F151_団体集計方法設定(団体集計方法名)
                    F151.Show()
                Else

                End If


            End If
        Else
            MsgBox("編集対象行を選択してください。")
        End If


    End Sub

    Private Sub PB_団体集計_Click(sender As Object, e As EventArgs) Handles PB_団体集計.Click

        Dim F152 As F152_団体集計
        F152 = New F152_団体集計(団体区分番号, 団体区分表記名)
        F152.Show()



    End Sub
End Class