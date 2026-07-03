Public Class F111_区分設定

    Private CB_カテゴリ_リスト(5, 2)
    'Private ラウンド番号リスト(12) As String
    Private ラウンド番号一覧() As String

    Private 更新FLAG As Boolean

    Private 区分番号 As String


    'コンストラクタ
    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。


        CB_カテゴリ_リスト(1, 1) = "S"
        CB_カテゴリ_リスト(1, 2) = "スタンダード"

        CB_カテゴリ_リスト(2, 1) = "L"
        CB_カテゴリ_リスト(2, 2) = "ラテン"

        CB_カテゴリ_リスト(3, 1) = "10"
        CB_カテゴリ_リスト(3, 2) = "10ダンス"

        CB_カテゴリ_リスト(4, 1) = "総合"
        CB_カテゴリ_リスト(4, 2) = "総合結果"

        CB_カテゴリ_リスト(5, 1) = "Oth"
        CB_カテゴリ_リスト(5, 2) = "その他"


        'CB_カテゴリに項目追加
        For i = 1 To UBound(CB_カテゴリ_リスト)
            Me.CB_カテゴリ.Items.Add(CB_カテゴリ_リスト(i, 2))
        Next

    End Sub



    Private Sub F111_区分設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False



        'CheckBox列を追加する
        Dim CHKBOX_ラウンド As New DataGridViewCheckBoxColumn
        CHKBOX_ラウンド.TrueValue = True
        CHKBOX_ラウンド.FalseValue = False
        Me.DGV_ラウンド.Columns.Add(CHKBOX_ラウンド)
        Me.DGV_ラウンド.Columns(0).HeaderText = "開催"

        DGV_ラウンド更新(区分番号)

    End Sub

    '指定された区分番号のデータを表示する
    Public Sub データ表示(区分番号_ As String)

        区分番号 = 区分番号_

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim 区分C As B_区分 = Nothing
        区分C = マスタデータ.B_区分マスタ.Get区分C(区分番号)

        Me.LB_区分番号.Text = 区分番号

        If 区分C IsNot Nothing Then

            Me.TB_区分記号.Text = 区分C.区分記号
            Me.TB_区分名.Text = 区分C.区分名
            Me.TB_区分表記名.Text = 区分C.区分表記名
            Me.CB_カテゴリ.Text = Getカテゴリ名(区分C.カテゴリ)
            Me.TB_審判G.Text = 区分C.担当審判グループ
            Me.TB_選手マスタ.Text = 区分C.使用する選手マスタ

            'エントリー数のカウント
            Dim エントリー数 As Integer = 0
            Dim 出場数 As Integer = 0
            For i = 1 To マスタデータ.選手マスタ.登録済み選手数
                If Trim(マスタデータ.選手マスタ.選手リスト(i).エントリー区分(CInt(区分番号))） <> "" And
                   マスタデータ.選手マスタ.選手リスト(i).エントリー区分(CInt(区分番号)) <> "0" Then
                    エントリー数 = エントリー数 + 1
                    If マスタデータ.選手マスタ.選手リスト(i).エントリー区分(CInt(区分番号)) <> "9" Then
                        出場数 = 出場数 + 1
                    End If
                End If
            Next i

            Me.LB_エントリー数.Text = "エントリー数 " & エントリー数 & "組　　出場数 " & 出場数 & "組　（欠場 " & エントリー数 - 出場数 & "組）"
        End If


        'DGV_ラウンド更新(区分番号)






        'マスタデータの開放
        マスタデータ = Nothing

    End Sub

    Private Sub DGV_ラウンド更新(区分番号 As String)


        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ


        'データクリア
        Me.DGV_ラウンド.DataSource = Nothing
        Me.DGV_ラウンド.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_ラウンド.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_ラウンド.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable


        'CheckBox列を追加する
        '==> FormLoadイベントに移動



        tbl.Columns.Add(New DataColumn("ラウンド", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("採点方式", GetType(String)))  'コンボボックス
        tbl.Columns.Add(New DataColumn("審判G", GetType(String)))
        ' tbl.Columns.Add(New DataColumn("出場組数", GetType(String)))
        tbl.Columns.Add(New DataColumn("Heat数", GetType(String)))
        tbl.Columns.Add(New DataColumn("UP数", GetType(String)))
        tbl.Columns.Add(New DataColumn("ヒート割方式", GetType(String)))　 'コンボボックス
        tbl.Columns.Add(New DataColumn("リアル", GetType(String)))　　　　　 'コンボボックス
        tbl.Columns.Add(New DataColumn("Cali最大値", GetType(Double)))
        tbl.Columns.Add(New DataColumn("Cali最小値", GetType(Double)))
        tbl.Columns.Add(New DataColumn("種目設定", GetType(String)))   'Read Only


        'コンボボックスの設定
        ' 採点方式
        Dim CB_採点方式 As New DataGridViewComboBoxColumn()
        'ComboBoxのリストに表示する項目を設定する
        CB_採点方式.Items.Add("チェック法")
        CB_採点方式.Items.Add("順位法")

        Dim 採点方式 As Object = Nothing
        Get_採点方式一覧(マスタデータ.Z_システム設定.システムPath, 採点方式)

        For i = 0 To UBound(採点方式)
            CB_採点方式.Items.Add(採点方式(i))
        Next i

        'CB_採点方式.Items.Add("AJS30J")
        'CB_採点方式.Items.Add("BJS10J")

        Dim CB_ヒート割方式 As New DataGridViewComboBoxColumn()
        'ComboBoxのリストに表示する項目を設定する
        CB_ヒート割方式.Items.Add("横割り")
        CB_ヒート割方式.Items.Add("縦割り")
        CB_ヒート割方式.Items.Add("シャッフル")

        Dim CB_リアル As New DataGridViewComboBoxColumn()
        'ComboBoxのリストに表示する項目を設定する
        CB_リアル.Items.Add(" ")
        CB_リアル.Items.Add("N")

        'ラウンド名の設定


        ReDim ラウンド番号一覧(19)
        ラウンド番号一覧(1) = "010"
        ラウンド番号一覧(2) = "01R"
        ラウンド番号一覧(3) = "011"
        ラウンド番号一覧(4) = "020"
        ラウンド番号一覧(5) = "021"
        ラウンド番号一覧(6) = "030"
        ラウンド番号一覧(7) = "031"
        ラウンド番号一覧(8) = "040"
        ラウンド番号一覧(9) = "041"
        ラウンド番号一覧(10) = "050"
        ラウンド番号一覧(11) = "051"
        ラウンド番号一覧(12) = "090"
        ラウンド番号一覧(13) = "091"
        ラウンド番号一覧(14) = "100"
        ラウンド番号一覧(15) = "101"
        ラウンド番号一覧(16) = "200"
        ラウンド番号一覧(17) = "201"
        ラウンド番号一覧(18) = "300"
        ラウンド番号一覧(19) = "400"


        Dim 行数 As Integer = 0
        For i = 1 To UBound(ラウンド番号一覧)

            Dim ラウンドC As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号一覧(i))


            If Strings.Right(ラウンド番号一覧(i), 1) = "0" Or Strings.Right(ラウンド番号一覧(i), 1) = "R" Then
                '同点決勝以外

                tbl.Rows.Add()
                行数 = 行数 + 1
                tbl.Rows(行数 - 1).Item(0) = マスタデータ.Get_ラウンド名(ラウンド番号一覧(i))
                tbl.Rows(行数 - 1).Item("ラウンド番号") = ラウンド番号一覧(i)

            Else
                '同点決勝の時は、ラウンドがある時だけ表示する。

                If ラウンドC IsNot Nothing Then
                    tbl.Rows.Add()
                    行数 = 行数 + 1
                    tbl.Rows(行数 - 1).Item(0) = マスタデータ.Get_ラウンド名(ラウンド番号一覧(i))
                    tbl.Rows(行数 - 1).Item("ラウンド番号") = ラウンド番号一覧(i)

                End If
            End If




            If ラウンドC IsNot Nothing Then
                tbl.Rows(行数 - 1).Item("ラウンド") = マスタデータ.Get_ラウンド名(ラウンド番号一覧(i))
                tbl.Rows(行数 - 1).Item("ラウンド番号") = ラウンド番号一覧(i)
                tbl.Rows(行数 - 1).Item("採点方式") = ラウンドC.採点方式
                tbl.Rows(行数 - 1).Item("審判G") = ラウンドC.担当審判グループ
                ' tbl.Rows(行数 - 1).Item("出場組数") = ラウンドC.出場組数
                tbl.Rows(行数 - 1).Item("Heat数") = ラウンドC.ヒート数
                tbl.Rows(行数 - 1).Item("UP数") = ラウンドC.UP予定数
                tbl.Rows(行数 - 1).Item("ヒート割方式") = ラウンドC.ヒート割方式
                tbl.Rows(行数 - 1).Item("リアル") = ラウンドC.リアルタイムFLAG
                tbl.Rows(行数 - 1).Item("Cali最大値") = ラウンドC.CaliMax
                tbl.Rows(行数 - 1).Item("Cali最小値") = ラウンドC.CaliMin

                Dim flag As Boolean = False
                For s = 1 To マスタデータ.D_種目マスタ.登録済みレコード数
                    If マスタデータ.D_種目マスタ.リスト(s).区分番号 = 区分番号 And
                       マスタデータ.D_種目マスタ.リスト(s).ラウンド番号 = ラウンドC.ラウンド番号 Then
                        flag = True
                        s = マスタデータ.D_種目マスタ.登録済みレコード数
                    End If
                Next s
                If flag = True Then
                    tbl.Rows(行数 - 1).Item("種目設定") = "有"
                Else
                    tbl.Rows(行数 - 1).Item("種目設定") = "無"
                End If

            Else
                '該当のラウンドが無いとき
                If Strings.Right(ラウンド番号一覧(i), 1) = "0" Or Strings.Right(ラウンド番号一覧(i), 1) = "R" Then
                    '同点決勝以外
                    tbl.Rows(行数 - 1).Item("ラウンド") = マスタデータ.Get_ラウンド名(ラウンド番号一覧(i))

                End If

            End If

                ラウンドC = Nothing

        Next i


        '// DataGridViewにデータセットを設定
        Me.DGV_ラウンド.DataSource = tbl

        '***DataGridViewにコンボボックスを設定

        '表示する列の名前を設定する
        CB_採点方式.DataPropertyName = Me.DGV_ラウンド.Columns("採点方式").DataPropertyName
        CB_ヒート割方式.DataPropertyName = Me.DGV_ラウンド.Columns("ヒート割方式").DataPropertyName
        CB_リアル.DataPropertyName = Me.DGV_ラウンド.Columns("リアル").DataPropertyName

        '現在採点方式列が存在している位置に挿入する
        Me.DGV_ラウンド.Columns.Insert(Me.DGV_ラウンド.Columns("採点方式").Index, CB_採点方式)
        '今までの採点方式列を削除する
        Me.DGV_ラウンド.Columns.Remove("採点方式")
        '挿入した列の名前を「採点方式」とする
        CB_採点方式.Name = "採点方式"
        '列のタイトルを設定
        Me.DGV_ラウンド.Columns("採点方式").HeaderText = "採点方式"

        '現在採点方式列が存在している位置に挿入する
        Me.DGV_ラウンド.Columns.Insert(Me.DGV_ラウンド.Columns("ヒート割方式").Index, CB_ヒート割方式)
        '今までの採点方式列を削除する
        Me.DGV_ラウンド.Columns.Remove("ヒート割方式")
        '挿入した列の名前を「採点方式」とする
        CB_ヒート割方式.Name = "ヒート割方式"
        '列のタイトルを設定
        Me.DGV_ラウンド.Columns("ヒート割方式").HeaderText = "ヒート割方式"

        '現在採点方式列が存在している位置に挿入する
        Me.DGV_ラウンド.Columns.Insert(Me.DGV_ラウンド.Columns("リアル").Index, CB_リアル)
        '今までの採点方式列を削除する
        Me.DGV_ラウンド.Columns.Remove("リアル")
        '挿入した列の名前を「採点方式」とする
        CB_リアル.Name = "リアル"
        '列のタイトルを設定
        Me.DGV_ラウンド.Columns("リアル").HeaderText = "リアル"

        '表示スタイルを、ComboBox形式に変更
        CB_採点方式.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
        CB_ヒート割方式.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
        CB_リアル.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox

        '選択されたセルだけ コンボボックスを表示
        CB_採点方式.DisplayStyleForCurrentCellOnly = True
        CB_ヒート割方式.DisplayStyleForCurrentCellOnly = True
        CB_リアル.DisplayStyleForCurrentCellOnly = True

        '===列幅の自動調整
        Me.DGV_ラウンド.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_ラウンド.AllowUserToResizeColumns = True

        Me.DGV_ラウンド.Columns(0).Width = 100
        Me.DGV_ラウンド.Columns(1).Width = 100


        For i = 1 To Me.DGV_ラウンド.RowCount

            Dim ラウンド番号 = Me.DGV_ラウンド.Rows(i - 1).Cells("ラウンド番号").Value

            Dim ラウンドC As C_ラウンド = Nothing
            If ラウンド番号 IsNot DBNull.Value Then
                ラウンドC = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号)
            End If

            If ラウンドC IsNot Nothing Then
                Me.DGV_ラウンド(0, i - 1).Value = True
            Else
                Me.DGV_ラウンド(0, i - 1).Value = False
            End If

        Next i


        'マスタデータの開放
        マスタデータ = Nothing

        '並び替えができないようにする ソート禁止
        For Each c As DataGridViewColumn In Me.DGV_ラウンド.Columns
            c.SortMode = DataGridViewColumnSortMode.NotSortable
        Next c

        'ラウンド設定列は読込み専用
        Me.DGV_ラウンド.Columns("種目設定").ReadOnly = True


        'ラウンド番号は非表示
        Me.DGV_ラウンド.Columns("ラウンド番号").Visible = False

    End Sub

    Private Sub Get_採点方式一覧(ByVal パス名, ByRef 採点方式)
        '採点方式の名前をパスからファイルを取得する。
        '入力 システムパス
        '出力 採点方式 配列

        Dim file As String() = System.IO.Directory.GetFiles(パス名, "MJ_*.csv")    ', System.IO.SearchOption.AllDirectories)

        ReDim 採点方式(UBound(file))

        For i = 0 To UBound(file)
            Dim l As Integer = file(i).Length - 7 - パス名.length - 1
            採点方式(i) = file(i).Substring(パス名.length + 4, l)
        Next i

    End Sub



    'マスタに登録されているカテゴリIDを渡すとカテゴリ名を返す
    Private Function Getカテゴリ名(カテゴリID As String) As String
        Dim rc As String = ""

        For i = 1 To UBound(CB_カテゴリ_リスト)
            If CB_カテゴリ_リスト(i, 1) = カテゴリID Then
                rc = CB_カテゴリ_リスト(i, 2)
                i = UBound(CB_カテゴリ_リスト)
            End If
        Next i

        Return rc
    End Function

    'セルが変更されたら直ぐにコミットする
    Private Sub DGV_ラウンド_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DGV_ラウンド.CurrentCellDirtyStateChanged

        If Me.DGV_ラウンド.CurrentCellAddress.X = 0 AndAlso Me.DGV_ラウンド.IsCurrentCellDirty Then
            'コミットする
            Me.DGV_ラウンド.CommitEdit(DataGridViewDataErrorContexts.Commit)

        End If

        更新FLAG = True
    End Sub

    'チェックボックスが変わった時の対応'
    Private Sub DGV_ラウンド_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DGV_ラウンド.CellValueChanged
        '列のインデックスを確認する
        If e.ColumnIndex = 0 And e.RowIndex >= 0 Then

            ' MessageBox.Show(String.Format("{0}行目のチェックボックスが{1}に変わりました。", e.RowIndex, DGV_ラウンド(e.ColumnIndex, e.RowIndex).Value))

            'チェックボックスが外れたら、色をグレーに変更
            If DGV_ラウンド(e.ColumnIndex, e.RowIndex).Value = False Then
                Me.DGV_ラウンド.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGray
            Else
                Me.DGV_ラウンド.Rows(e.RowIndex).DefaultCellStyle.BackColor = Nothing

            End If

            '準決勝・決勝のチェックボックスが付いて値が設定されていない場合は、デフォルト値をセット
            If DGV_ラウンド(e.ColumnIndex, e.RowIndex).Value = True Then
                If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("ラウンド").Value = "準決勝" Then

                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("採点方式").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("採点方式").Value = "AJS30J"
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("審判G").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("審判G").Value = Me.TB_審判G.Text
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Heat数").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Heat数").Value = 2
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("UP数").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("UP数").Value = 6
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("ヒート割方式").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("ヒート割方式").Value = "シャッフル"
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("リアル").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("リアル").Value = ""
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Cali最大値").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Cali最大値").Value = 10
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Cali最小値").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Cali最小値").Value = 0
                    End If


                ElseIf Me.DGV_ラウンド.Rows(e.RowIndex).Cells("ラウンド").Value = "決勝" Then


                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("採点方式").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("採点方式").Value = "AJS30J"
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("審判G").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("審判G").Value = Me.TB_審判G.Text
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Heat数").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Heat数").Value = 1
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("UP数").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("UP数").Value = 6
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("ヒート割方式").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("ヒート割方式").Value = "シャッフル"
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("リアル").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("リアル").Value = ""
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Cali最大値").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Cali最大値").Value = 10
                    End If
                    If Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Cali最小値").Value Is DBNull.Value Then
                        Me.DGV_ラウンド.Rows(e.RowIndex).Cells("Cali最小値").Value = 0
                    End If

                End If

            End If
        End If


    End Sub

    Private Sub PB_種目設定_Click(sender As Object, e As EventArgs) Handles PB_種目設定.Click

        Dim 選択_ラウンド行番号 As Integer = Me.DGV_ラウンド.CurrentRow.Index


        If Me.DGV_ラウンド(0, 選択_ラウンド行番号).Value = False Then
            MsgBox("選択されたラウンドの設定がありません。")
            Exit Sub
        End If

        Try
            Dim 選択ラウンド番号 As String = Me.DGV_ラウンド.Rows(選択_ラウンド行番号).Cells("ラウンド番号").Value

            If 選択ラウンド番号 <> "" Then

                Dim F112 As F112_競技種目設定
                F112 = New F112_競技種目設定
                F112.データ表示(区分番号, 選択ラウンド番号）
                F112.ShowDialog()

            End If

        Catch
            MsgBox("種目設定の前に保存してください。")   'これが呼ばれることはない。F112の中でCatchされる。

        End Try

    End Sub

    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'B_区分一覧も更新
        C_区分更新(マスタデータ)



        'C_ラウンドマスタから該当のKey項目を一旦Delete
        マスタデータ.C_ラウンドマスタ.Deleteレコード(区分番号)

        'DGVの値をAdd
        Dim ラウンドC As C_ラウンド

        For i = 0 To Me.DGV_ラウンド.RowCount - 1

            'チェックボックスが付いているものだけ書き込む
            If Me.DGV_ラウンド.Rows(i).Cells(0).Value = True Then

                '入力チェック

                If Me.DGV_ラウンド.Rows(i).Cells("採点方式").Value Is DBNull.Value Then
                    MsgBox("採点方式を選択してください。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_ラウンド.Rows(i).Cells("審判G").Value Is DBNull.Value Then
                    MsgBox("審判Gを入力してください。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_ラウンド.Rows(i).Cells("Heat数").Value Is DBNull.Value Then
                    MsgBox("Heat数を入力してください。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_ラウンド.Rows(i).Cells("UP数").Value Is DBNull.Value Then
                    MsgBox("UP数を入力してください。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_ラウンド.Rows(i).Cells("ヒート割方式").Value Is DBNull.Value Then
                    MsgBox("ヒート割方式を選択してください。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_ラウンド.Rows(i).Cells("リアル").Value Is DBNull.Value Then
                    Me.DGV_ラウンド.Rows(i).Cells("リアル").Value = ""
                End If
                If Me.DGV_ラウンド.Rows(i).Cells("Cali最大値").Value Is DBNull.Value Then
                    Me.DGV_ラウンド.Rows(i).Cells("Cali最大値").Value = 10
                End If
                If Me.DGV_ラウンド.Rows(i).Cells("Cali最小値").Value Is DBNull.Value Then
                    Me.DGV_ラウンド.Rows(i).Cells("Cali最小値").Value = 0
                End If



                ラウンドC = New C_ラウンド

                ラウンドC.区分番号 = 区分番号.PadLeft(2, "0")
                ラウンドC.ラウンド番号 = Me.DGV_ラウンド.Rows(i).Cells("ラウンド番号").Value     'ラウンド番号リスト(i + 1).PadLeft(3, "0")
                ラウンドC.採点方式 = Me.DGV_ラウンド.Rows(i).Cells("採点方式").Value
                ラウンドC.担当審判グループ = Me.DGV_ラウンド.Rows(i).Cells("審判G").Value
                ラウンドC.ヒート数 = Me.DGV_ラウンド.Rows(i).Cells("Heat数").Value
                ラウンドC.UP予定数 = Me.DGV_ラウンド.Rows(i).Cells("UP数").Value
                ラウンドC.ヒート割方式 = Me.DGV_ラウンド.Rows(i).Cells("ヒート割方式").Value
                ラウンドC.リアルタイムFLAG = Me.DGV_ラウンド.Rows(i).Cells("リアル").Value
                ラウンドC.CaliMax = Me.DGV_ラウンド.Rows(i).Cells("Cali最大値").Value
                ラウンドC.CaliMin = Me.DGV_ラウンド.Rows(i).Cells("Cali最小値").Value


                マスタデータ.C_ラウンドマスタ.登録(ラウンドC)



                '採点進行マスターのリアルフラグを更新
                Dim 採点進行 As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンドC.ラウンド番号)
                If 採点進行 IsNot Nothing Then
                    採点進行.リアルタイムFLAG = ラウンドC.リアルタイムFLAG
                    マスタデータ.T_採点進行管理.登録(採点進行)
                    採点進行 = Nothing
                End If

            End If

        Next i


        マスタデータ = Nothing

        データ表示(区分番号)

        DGV_ラウンド更新(区分番号)

        更新FLAG = False







    End Sub

    Private Sub C_区分更新(マスタデータ As マスタデータ)

        Dim 区分C As B_区分
        区分C = New B_区分

        区分C.区分番号 = Me.LB_区分番号.Text
        区分C.区分記号 = Me.TB_区分記号.Text
        区分C.区分名 = Me.TB_区分名.Text
        区分C.区分表記名 = Me.TB_区分表記名.Text

        Select Case Me.CB_カテゴリ.Text
            Case "スタンダード"
                区分C.カテゴリ = "S"
            Case "ラテン"
                区分C.カテゴリ = "L"
            Case "10ダンス"
                区分C.カテゴリ = "10"
            Case "総合結果"
                区分C.カテゴリ = "総合"
            Case "その他"
                区分C.カテゴリ = "Oth"
            Case Else
                MsgBox("カテゴリを正しく選択してください。「" & Me.CB_カテゴリ.Text & "」は無効です。")
                Exit Sub
        End Select

        区分C.担当審判グループ = Me.TB_審判G.Text
        区分C.使用する選手マスタ = CStr(Me.TB_選手マスタ.Text).PadLeft(2, "0")


        マスタデータ.B_区分マスタ.登録(区分C)

        区分C = Nothing

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

End Class