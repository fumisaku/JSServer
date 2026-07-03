Public Class F112_競技種目設定

    Private 区分番号 As String
    Private ラウンド番号 As String

    Private 更新FLAG As Boolean

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub

    Private Sub F112_競技種目設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False

        更新FLAG = False

        Try
            DGV更新(区分番号, ラウンド番号)
        Catch

            MsgBox("種目設定の前に保存してください。")
            'F111で保存せずに種目設定を押すと、DGV更新でエラーになるため。
        End Try


    End Sub

    Public Sub データ表示(区分番号_ As String, ラウンド番号_ As String)

        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_



    End Sub



    Private Sub DGV更新(区分番号 As String, ラウンド番号 As String)

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        '区分名　ラウンド名表示
        Me.LB_区分番号.Text = 区分番号
        Me.LB_区分名.Text = マスタデータ.B_区分マスタ.Get区分C(区分番号).区分名
        Me.LB_ラウンド名.Text = マスタデータ.Get_ラウンド名(ラウンド番号)


        'キャリブレーション
        Me.TB_CaliMax.Text = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).CaliMax
        Me.TB_CaliMin.Text = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).CaliMin

        'データクリア
        Me.DGV_競技種目.DataSource = Nothing
        Me.DGV_競技種目.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_競技種目.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_競技種目.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable



        tbl.Columns.Add(New DataColumn("種目順", GetType(String)))
        tbl.Columns.Add(New DataColumn("種目記号", GetType(String)))
        tbl.Columns.Add(New DataColumn("ソロ・グループ種別", GetType(String)))
        tbl.Columns.Add(New DataColumn("ヒート数", GetType(String)))
        tbl.Columns.Add(New DataColumn("審判G", GetType(String)))
        tbl.Columns.Add(New DataColumn("Cali最大値", GetType(Double)))
        tbl.Columns.Add(New DataColumn("Cali最小値", GetType(Double)))

        Dim CB_SG種別 As New DataGridViewComboBoxColumn()
        'ComboBoxのリストに表示する項目を設定する
        CB_SG種別.Items.Add("全員")
        CB_SG種別.Items.Add("ソロ")
        CB_SG種別.Items.Add("対戦")



        For 種目順 = 1 To 11
            Dim 種目C As D_種目 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目順)

            If 種目C IsNot Nothing Then
                tbl.Rows.Add()
                tbl.Rows(種目順 - 1).Item("種目順") = 種目順
                tbl.Rows(種目順 - 1).Item("種目記号") = 種目C.種目記号

                If 種目C.SG種別 = "S" Then
                    tbl.Rows(種目順 - 1).Item("ソロ・グループ種別") = "ソロ"
                ElseIf 種目C.SG種別 = "G" Then
                    tbl.Rows(種目順 - 1).Item("ソロ・グループ種別") = "全員"
                ElseIf 種目C.SG種別 = "D" Then
                    tbl.Rows(種目順 - 1).Item("ソロ・グループ種別") = "対戦"
                End If


                tbl.Rows(種目順 - 1).Item("ヒート数") = 種目C.ヒート数
                tbl.Rows(種目順 - 1).Item("審判G") = 種目C.担当審判グループ
                tbl.Rows(種目順 - 1).Item("Cali最大値") = 種目C.CaliMax
                tbl.Rows(種目順 - 1).Item("Cali最小値") = 種目C.CaliMin
            Else

                種目順 = 11
            End If

        Next 種目順

        '初期値のセット
        If マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 1) Is Nothing Then


            'デフォルト値のセット
            Dim 区分C As B_区分 = マスタデータ.B_区分マスタ.Get区分C(区分番号)
            Dim ラウンドC As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号)
            If Strings.Left(ラウンドC.採点方式, 3) = "BJS" Then

                'ブレイキンの場合

                MsgBox("保存されたデータが無いため、Defaultでデータを作成します。")

                Dim 種目(5) As String
                種目(1) = "1R"
                種目(2) = "2R"
                種目(3) = "3R"
                種目(4) = "4R"
                種目(5) = "5R"

                マスタデータ.J_新審判設定.Set_新審判基準VER(ラウンドC.採点方式)
                Dim 種目数 As Integer = マスタデータ.J_新審判設定.勝敗ラウンド数 * 2 - 1   '勝敗ラウンド数 x 2 -1  : 勝敗ラウンド数が２の時は、3種目。　　３の時は5種目になる。

                If 種目数 > 5 Then  '種目数の最大は５に設定
                    種目数 = 5
                End If

                For 種目順 = 1 To 種目数

                    tbl.Rows.Add()
                    tbl.Rows(種目順 - 1).Item("種目順") = 種目順

                    tbl.Rows(種目順 - 1).Item("種目記号") = 種目(種目順)


                    tbl.Rows(種目順 - 1).Item("ソロ・グループ種別") = "対戦"

                    tbl.Rows(種目順 - 1).Item("ヒート数") = 2
                    tbl.Rows(種目順 - 1).Item("審判G") = ラウンドC.担当審判グループ
                    tbl.Rows(種目順 - 1).Item("Cali最大値") = ラウンドC.CaliMax
                    tbl.Rows(種目順 - 1).Item("Cali最小値") = ラウンドC.CaliMin

                Next 種目順

                更新FLAG = True




            ElseIf ラウンド番号 = "200" Then '準決勝

                MsgBox("保存されたデータが無いため、Defaultでデータを作成します。")

                Dim S_種目(5) As String
                S_種目(1) = "W"
                S_種目(2) = "T"
                S_種目(3) = "V"
                S_種目(4) = "F"
                S_種目(5) = "Q"

                Dim L_種目(5) As String
                L_種目(1) = "S"
                L_種目(2) = "C"
                L_種目(3) = "R"
                L_種目(4) = "P"
                L_種目(5) = "J"


                For 種目順 = 1 To 5

                    tbl.Rows.Add()
                    tbl.Rows(種目順 - 1).Item("種目順") = 種目順

                    If 区分C.カテゴリ = "S" Then
                        tbl.Rows(種目順 - 1).Item("種目記号") = S_種目(種目順)
                    ElseIf 区分C.カテゴリ = "L" Then
                        tbl.Rows(種目順 - 1).Item("種目記号") = L_種目(種目順)
                    End If


                    tbl.Rows(種目順 - 1).Item("ソロ・グループ種別") = "全員"

                    tbl.Rows(種目順 - 1).Item("ヒート数") = ラウンドC.ヒート数
                    tbl.Rows(種目順 - 1).Item("審判G") = ラウンドC.担当審判グループ
                    tbl.Rows(種目順 - 1).Item("Cali最大値") = ラウンドC.CaliMax
                    tbl.Rows(種目順 - 1).Item("Cali最小値") = ラウンドC.CaliMin

                Next 種目順

                更新FLAG = True
            ElseIf ラウンド番号 = "400" And ラウンドC.採点方式 = "AJS30J" Then '決勝

                MsgBox("保存されたデータが無いため、Defaultでデータを作成します。")

                マスタデータ.J_新審判設定.Set_新審判基準VER("AJS30J")

                For 種目順 = 1 To 5

                    tbl.Rows.Add()
                    tbl.Rows(種目順 - 1).Item("種目順") = 種目順

                    Dim 既定種目順C As J_既定種目順 = マスタデータ.J_新審判設定.Get既定種目順C(区分C.カテゴリ, 種目順)

                    tbl.Rows(種目順 - 1).Item("種目記号") = 既定種目順C.種目記号

                    Select Case 既定種目順C.SG種別
                        Case "S"
                            tbl.Rows(種目順 - 1).Item("ソロ・グループ種別") = "ソロ"
                            tbl.Rows(種目順 - 1).Item("ヒート数") = 6
                        Case "G"
                            tbl.Rows(種目順 - 1).Item("ソロ・グループ種別") = "全員"
                            tbl.Rows(種目順 - 1).Item("ヒート数") = 1
                        Case "D"
                            tbl.Rows(種目順 - 1).Item("ソロ・グループ種別") = "対戦"
                            tbl.Rows(種目順 - 1).Item("ヒート数") = 3
                    End Select

                    tbl.Rows(種目順 - 1).Item("審判G") = ラウンドC.担当審判グループ
                    tbl.Rows(種目順 - 1).Item("Cali最大値") = ラウンドC.CaliMax
                    tbl.Rows(種目順 - 1).Item("Cali最小値") = ラウンドC.CaliMin

                Next 種目順

                更新FLAG = True




            ElseIf ラウンド番号 = "400" And Strings.Left(ラウンドC.採点方式, 3) = "PDJ" Then '決勝

                MsgBox("保存されたデータが無いため、Defaultでデータを作成します。")

                マスタデータ.J_新審判設定.Set_新審判基準VER(ラウンドC.採点方式)

                For 種目順 = 1 To 5

                    tbl.Rows.Add()
                    tbl.Rows(種目順 - 1).Item("種目順") = 種目順

                    Dim 既定種目順C As J_既定種目順 = マスタデータ.J_新審判設定.Get既定種目順C(区分C.カテゴリ, 種目順)

                    If 既定種目順C Is Nothing Then
                        '既定の種目が定義されていない場合

                        MsgBox("既定の種目が定義されていません。")

                    Else

                        tbl.Rows(種目順 - 1).Item("種目記号") = 既定種目順C.種目記号

                        Select Case 既定種目順C.SG種別
                            Case "S"
                                tbl.Rows(種目順 - 1).Item("ソロ・グループ種別") = "ソロ"
                                tbl.Rows(種目順 - 1).Item("ヒート数") = 6
                            Case "G"
                                tbl.Rows(種目順 - 1).Item("ソロ・グループ種別") = "全員"
                                tbl.Rows(種目順 - 1).Item("ヒート数") = 1
                            Case "D"
                                tbl.Rows(種目順 - 1).Item("ソロ・グループ種別") = "対戦"
                                tbl.Rows(種目順 - 1).Item("ヒート数") = 3
                        End Select

                        tbl.Rows(種目順 - 1).Item("審判G") = ラウンドC.担当審判グループ
                        tbl.Rows(種目順 - 1).Item("Cali最大値") = ラウンドC.CaliMax
                        tbl.Rows(種目順 - 1).Item("Cali最小値") = ラウンドC.CaliMin

                    End If
                Next 種目順

                更新FLAG = True




            End If



        End If




            '// DataGridViewにデータセットを設定
            Me.DGV_競技種目.DataSource = tbl


        '***DataGridViewにコンボボックスを設定

        '表示する列の名前を設定する
        CB_SG種別.DataPropertyName = Me.DGV_競技種目.Columns("ソロ・グループ種別").DataPropertyName

        '現在採点方式列が存在している位置に挿入する
        Me.DGV_競技種目.Columns.Insert(Me.DGV_競技種目.Columns("ソロ・グループ種別").Index, CB_SG種別)
        '今までの採点方式列を削除する
        Me.DGV_競技種目.Columns.Remove("ソロ・グループ種別")
        '挿入した列の名前を「採点方式」とする
        CB_SG種別.Name = "ソロ・グループ種別"
        '列のタイトルを設定
        Me.DGV_競技種目.Columns("ソロ・グループ種別").HeaderText = "ソロ・グループ種別"


        '表示スタイルを、ComboBox形式に変更
        CB_SG種別.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox


        '選択されたセルだけ コンボボックスを表示
        CB_SG種別.DisplayStyleForCurrentCellOnly = True



        '===列幅の自動調整
        Me.DGV_競技種目.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_競技種目.AllowUserToResizeColumns = True

        '===行の高さの自動設定
        Me.DGV_競技種目.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders


        'マスタデータの開放
        マスタデータ = Nothing

        '並び替えができないようにする ソート禁止
        For Each c As DataGridViewColumn In Me.DGV_競技種目.Columns
            c.SortMode = DataGridViewColumnSortMode.NotSortable
        Next c



    End Sub

    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'D_種目マスタから該当のKey項目をDelete
        マスタデータ.D_種目マスタ.Deleteレコード(区分番号, ラウンド番号)

        'DGVの値をAdd
        Dim 種目C As D_種目

        For i = 0 To Me.DGV_競技種目.RowCount - 1

            If Me.DGV_競技種目.Rows(i).Cells("種目順").Value IsNot Nothing Then

                '入力チェック
                If Me.DGV_競技種目.Rows(i).Cells("種目記号").Value Is DBNull.Value Then
                    MsgBox("種目記号を入力してください。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_競技種目.Rows(i).Cells("ソロ・グループ種別").Value Is DBNull.Value Then
                    MsgBox("ソロ・グループ種別を選択してください。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_競技種目.Rows(i).Cells("ヒート数").Value Is DBNull.Value Then
                    MsgBox("ヒート数を入力してください。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_競技種目.Rows(i).Cells("審判G").Value Is DBNull.Value Then
                    MsgBox("審判Gを入力してください。")
                    マスタデータ = Nothing
                    Exit Sub
                End If
                If Me.DGV_競技種目.Rows(i).Cells("Cali最大値").Value Is DBNull.Value Then
                    Me.DGV_競技種目.Rows(i).Cells("Cali最大値").Value = 10
                End If
                If Me.DGV_競技種目.Rows(i).Cells("Cali最小値").Value Is DBNull.Value Then
                    Me.DGV_競技種目.Rows(i).Cells("Cali最小値").Value = 0
                End If


                種目C = New D_種目

                種目C.区分番号 = CStr(区分番号).PadLeft(2, "0")
                種目C.ラウンド番号 = ラウンド番号.PadLeft(3, "0")
                種目C.種目順 = Me.DGV_競技種目.Rows(i).Cells("種目順").Value
                種目C.種目記号 = Me.DGV_競技種目.Rows(i).Cells("種目記号").Value

                If Me.DGV_競技種目.Rows(i).Cells("ソロ・グループ種別").Value = "ソロ" Then
                    種目C.SG種別 = "S"
                ElseIf Me.DGV_競技種目.Rows(i).Cells("ソロ・グループ種別").Value = "全員" Then
                    種目C.SG種別 = "G"
                ElseIf Me.DGV_競技種目.Rows(i).Cells("ソロ・グループ種別").Value = "対戦" Then
                    種目C.SG種別 = "D"
                End If

                種目C.ヒート数 = Me.DGV_競技種目.Rows(i).Cells("ヒート数").Value
                種目C.担当審判グループ = Me.DGV_競技種目.Rows(i).Cells("審判G").Value
                種目C.CaliMax = Me.DGV_競技種目.Rows(i).Cells("Cali最大値").Value
                種目C.CaliMin = Me.DGV_競技種目.Rows(i).Cells("Cali最小値").Value

                マスタデータ.D_種目マスタ.登録(種目C)
            End If

        Next i



        DGV更新(区分番号, ラウンド番号)

        'U_進行を更新
        マスタデータ.U_進行管理.更新()



        更新FLAG = False

        種目C = Nothing
        マスタデータ = Nothing


    End Sub

    'セルが変更されたら
    Private Sub DGV_ラウンド_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DGV_競技種目.CurrentCellDirtyStateChanged

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

    Private Sub PB_反映_Click(sender As Object, e As EventArgs) Handles PB_反映.Click

        For i = 0 To Me.DGV_競技種目.RowCount - 1
            If Me.DGV_競技種目.Rows(i).Cells("種目順").Value IsNot Nothing Then
                Me.DGV_競技種目.Rows(i).Cells("Cali最大値").Value = CDbl(Me.TB_CaliMax.Text)
                Me.DGV_競技種目.Rows(i).Cells("Cali最小値").Value = CDbl(Me.TB_CaliMin.Text)
            End If

        Next i


    End Sub
End Class