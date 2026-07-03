Public Class F120_採点進行設定

    Private 更新FLAG As Boolean


    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

    End Sub


    Private Sub F120_採点進行設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False


        競技番号設定画面()
        更新FLAG = False

    End Sub


    Private Sub 競技番号設定画面()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ


        'データクリア
        Me.DGV_採点進行管理.DataSource = Nothing
        Me.DGV_採点進行管理.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_採点進行管理.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_採点進行管理.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable



        tbl.Columns.Add(New DataColumn("区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分表記名", GetType(String)))
        tbl.Columns.Add(New DataColumn("１次", GetType(String)))
        tbl.Columns.Add(New DataColumn("リダンス", GetType(String)))
        tbl.Columns.Add(New DataColumn("２次", GetType(String)))
        tbl.Columns.Add(New DataColumn("３次", GetType(String)))
        tbl.Columns.Add(New DataColumn("４次", GetType(String)))
        tbl.Columns.Add(New DataColumn("５次", GetType(String)))
        tbl.Columns.Add(New DataColumn("最終", GetType(String)))
        tbl.Columns.Add(New DataColumn("準々", GetType(String)))
        tbl.Columns.Add(New DataColumn("準決勝", GetType(String)))
        tbl.Columns.Add(New DataColumn("下位", GetType(String)))
        tbl.Columns.Add(New DataColumn("決勝", GetType(String)))




        For 区分No = 1 To マスタデータ.B_区分マスタ.登録済みレコード数
            'Dim 区分C As B_区分 = マスタデータ.B_区分マスタ.Get区分C(区分No)
            Dim 区分C As B_区分 = マスタデータ.B_区分マスタ.リスト(区分No)

            If 区分C IsNot Nothing Then
                tbl.Rows.Add()
                tbl.Rows(区分No - 1).Item("区分番号") = 区分C.区分番号
                tbl.Rows(区分No - 1).Item("区分表記名") = 区分C.区分表記名
                tbl.Rows(区分No - 1).Item("１次") = Strings.Left(マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分C.区分番号, "010"), 3)
                tbl.Rows(区分No - 1).Item("リダンス") = Strings.Left(マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分C.区分番号, "01R"), 3)
                tbl.Rows(区分No - 1).Item("２次") = Strings.Left(マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分C.区分番号, "020"), 3)
                tbl.Rows(区分No - 1).Item("３次") = Strings.Left(マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分C.区分番号, "030"), 3)
                tbl.Rows(区分No - 1).Item("４次") = Strings.Left(マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分C.区分番号, "040"), 3)
                tbl.Rows(区分No - 1).Item("５次") = Strings.Left(マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分C.区分番号, "050"), 3)
                tbl.Rows(区分No - 1).Item("最終") = Strings.Left(マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分C.区分番号, "090"), 3)
                tbl.Rows(区分No - 1).Item("準々") = Strings.Left(マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分C.区分番号, "100"), 3)
                tbl.Rows(区分No - 1).Item("準決勝") = Strings.Left(マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分C.区分番号, "200"), 3)
                tbl.Rows(区分No - 1).Item("下位") = Strings.Left(マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分C.区分番号, "300"), 3)
                tbl.Rows(区分No - 1).Item("決勝") = Strings.Left(マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分C.区分番号, "400"), 3)


            Else
                区分No = マスタデータ.B_区分マスタ.登録済みレコード数
            End If

            区分C = Nothing

        Next 区分No

        '// DataGridViewにデータセットを設定
        Me.DGV_採点進行管理.DataSource = tbl


        'ラウンドマスタに設定されていないラウンドはグレーアウトにする
        For 区分No = 1 To マスタデータ.B_区分マスタ.登録済みレコード数
            Dim 区分C As B_区分 = マスタデータ.B_区分マスタ.リスト(区分No)  '2021/7/16 修正

            If 区分C IsNot Nothing Then
                If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "010") Is Nothing Then
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("１次").Style.BackColor = Color.Gray
                Else
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("１次").Style.BackColor = Nothing
                End If
                If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "01R") Is Nothing Then
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("リダンス").Style.BackColor = Color.Gray
                Else
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("リダンス").Style.BackColor = Nothing
                End If
                If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "020") Is Nothing Then
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("２次").Style.BackColor = Color.Gray
                Else
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("２次").Style.BackColor = Nothing
                End If
                If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "030") Is Nothing Then
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("３次").Style.BackColor = Color.Gray
                Else
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("３次").Style.BackColor = Nothing
                End If
                If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "040") Is Nothing Then
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("４次").Style.BackColor = Color.Gray
                Else
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("４次").Style.BackColor = Nothing
                End If
                If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "050") Is Nothing Then
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("５次").Style.BackColor = Color.Gray
                Else
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("５次").Style.BackColor = Nothing
                End If
                If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "090") Is Nothing Then
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("最終").Style.BackColor = Color.Gray
                Else
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("最終").Style.BackColor = Nothing
                End If
                If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "100") Is Nothing Then
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("準々").Style.BackColor = Color.Gray
                Else
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("準々").Style.BackColor = Nothing
                End If
                If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "200") Is Nothing Then
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("準決勝").Style.BackColor = Color.Gray
                Else
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("準決勝").Style.BackColor = Nothing
                End If
                If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "300") Is Nothing Then
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("下位").Style.BackColor = Color.Gray
                Else
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("下位").Style.BackColor = Nothing
                End If
                If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "400") Is Nothing Then
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("決勝").Style.BackColor = Color.Gray
                Else
                    Me.DGV_採点進行管理.Rows(区分No - 1).Cells("決勝").Style.BackColor = Nothing
                End If

                '決勝が無い時は1次予選も存在しないという理論で作られたと思われるが、ブレイキンではこのケースはあるので、V1.01.15で削除
                'If マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分C.区分番号, "400") Is Nothing Then
                'Me.DGV_採点進行管理.Rows(区分No - 1).Cells("１次").Style.BackColor = Color.Gray
                'Else
                'Me.DGV_採点進行管理.Rows(区分No - 1).Cells("１次").Style.BackColor = Nothing
                'End If
            End If

            区分C = Nothing

        Next 区分No



        '===列幅の自動調整
        Me.DGV_採点進行管理.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_採点進行管理.AllowUserToResizeColumns = True



        '並び替えができないようにする ソート禁止
        For Each c As DataGridViewColumn In Me.DGV_採点進行管理.Columns
            c.SortMode = DataGridViewColumnSortMode.NotSortable
        Next c

        マスタデータ = Nothing



    End Sub

    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim ラウンド番号リスト(13) As String
        ラウンド番号リスト(1) = "010"
        ラウンド番号リスト(2) = "01R"
        ラウンド番号リスト(3) = "020"
        ラウンド番号リスト(4) = "030"
        ラウンド番号リスト(5) = "040"
        ラウンド番号リスト(6) = "050"
        ラウンド番号リスト(7) = "090"
        ラウンド番号リスト(8) = "100"
        ラウンド番号リスト(9) = "200"
        ラウンド番号リスト(10) = "300"
        ラウンド番号リスト(11) = "400"

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim ORG_マスタデータ As マスタデータ
        ORG_マスタデータ = New マスタデータ







        'T_採点進行管理からレコードをDelete
        マスタデータ.T_採点進行管理.Deleteレコード()

        'DGVの値をAdd
        Dim 採点進行C As T_採点進行 = Nothing

        For i = 0 To Me.DGV_採点進行管理.RowCount - 1

            If Me.DGV_採点進行管理.Rows(i).Cells("区分番号").Value IsNot Nothing Then

                For r = 1 To 11  'ラウンド数分ループする
                    If Me.DGV_採点進行管理(r + 1, i).Value IsNot DBNull.Value Then
                        If Me.DGV_採点進行管理(r + 1, i).Value <> "" Then
                            採点進行C = New T_採点進行


                            採点進行C.競技番号 = CStr(Me.DGV_採点進行管理(r + 1, i).Value).PadLeft(3, "0")
                            採点進行C.競技番号枝番 = "00"
                            採点進行C.区分番号 = Me.DGV_採点進行管理.Rows(i).Cells("区分番号").Value
                            採点進行C.ラウンド番号 = ラウンド番号リスト(r)

                            'V1.02．18 で追加　
                            '入力チェック　ラウンドマスターに登録されていない場合はワーニングを出す。
                            Dim ラウンドC As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(採点進行C.区分番号, 採点進行C.ラウンド番号)

                            'Ver1.02.18で追加
                            If ラウンドC Is Nothing Then
                                MsgBox("競技番号:" & 採点進行C.競技番号 & "は定義できません。 進行設定を修正してください。 対象の区分番号、ラウンド番号は、" & 採点進行C.区分番号 & ":" & 採点進行C.ラウンド番号)

                            End If




                            Dim ORG_採点進行C As T_採点進行 = ORG_マスタデータ.T_採点進行管理.Get_採点進行Class(採点進行C.区分番号, 採点進行C.ラウンド番号)

                            If ORG_採点進行C IsNot Nothing Then
                                採点進行C.リアルタイムFLAG = ORG_採点進行C.リアルタイムFLAG
                                採点進行C.ステータス = ORG_採点進行C.ステータス
                            Else
                                採点進行C.リアルタイムFLAG = ""
                                採点進行C.ステータス = ""
                            End If

                            マスタデータ.T_採点進行管理.登録(採点進行C)





                            '同点決勝分を追加
                            For k = 1 To マスタデータ.C_ラウンドマスタ.登録済みレコード数

                                If マスタデータ.C_ラウンドマスタ.リスト(k).区分番号 = Me.DGV_採点進行管理.Rows(i).Cells("区分番号").Value And
                                   マスタデータ.C_ラウンドマスタ.リスト(k).ラウンド番号.Substring(0, 2) = ラウンド番号リスト(r).Substring(0, 2) And
                                   マスタデータ.C_ラウンドマスタ.リスト(k).ラウンド番号.Substring(2, 1) <> "0" And
                               マスタデータ.C_ラウンドマスタ.リスト(k).ラウンド番号.Substring(2, 1) <> "R" Then
                                    '区分番号が同じで、ラウンド番号の下一桁が、0でもRでもない場合は、同点決勝

                                    採点進行C = New T_採点進行


                                    採点進行C.競技番号 = CStr(Me.DGV_採点進行管理(r + 1, i).Value).PadLeft(3, "0")
                                    採点進行C.競技番号枝番 = "0” & マスタデータ.C_ラウンドマスタ.リスト(k).ラウンド番号.Substring(2, 1)
                                    採点進行C.区分番号 = Me.DGV_採点進行管理.Rows(i).Cells("区分番号").Value
                                    採点進行C.ラウンド番号 = マスタデータ.C_ラウンドマスタ.リスト(k).ラウンド番号

                                    ORG_採点進行C = ORG_マスタデータ.T_採点進行管理.Get_採点進行Class(採点進行C.区分番号, 採点進行C.ラウンド番号)

                                    If ORG_採点進行C IsNot Nothing Then
                                        採点進行C.リアルタイムFLAG = ORG_採点進行C.リアルタイムFLAG
                                        採点進行C.ステータス = ORG_採点進行C.ステータス
                                    Else
                                        採点進行C.リアルタイムFLAG = ""
                                        採点進行C.ステータス = ""
                                    End If

                                    マスタデータ.T_採点進行管理.登録(採点進行C)


                                End If

                            Next k

                        End If

                    End If


                Next r

            End If

        Next i


        '2021/05/20 V1.02.14 で追加
        If 更新FLAG = True Then
            'U_進行管理を更新
            マスタデータ.U_進行管理.更新()
        End If




        採点進行C = Nothing
        マスタデータ = Nothing
        ORG_マスタデータ = Nothing

        競技番号設定画面()



        更新FLAG = False



    End Sub




    Private Sub PB_表示切替_Click(sender As Object, e As EventArgs) Handles PB_表示切替.Click

        Dim F121 As F121_採点進行確認
        F121 = New F121_採点進行確認

        F121.ShowDialog()

    End Sub


    'セルが変更されたら
    Private Sub DGV_採点進行管理_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DGV_採点進行管理.CurrentCellDirtyStateChanged

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


End Class