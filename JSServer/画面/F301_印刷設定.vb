Public Class F301_印刷設定


    Private 更新FLAG As Boolean

    Private Sub F301_印刷設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False


        更新FLAG = False

        DGV更新（）


    End Sub

    Private Sub DGV更新()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'データクリア
        Me.DGV_印刷設定.DataSource = Nothing
        Me.DGV_印刷設定.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_印刷設定.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_印刷設定.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '// データテーブルの作成
        Dim tbl As New DataTable



        tbl.Columns.Add(New DataColumn("採点方式", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド", GetType(String)))
        tbl.Columns.Add(New DataColumn("帳票記号", GetType(String)))

        For i = 1 To 20
            tbl.Columns.Add(New DataColumn("配布先_" & i, GetType(String)))
        Next i

        '1行目（ヘッダー列） を追加
        tbl.Rows.Add()
        For i = 1 To 20
            tbl.Rows(0).Item("配布先_" & i) = マスタデータ.R_印刷設定マスタ.配布先(i)
        Next i


        For s = 1 To マスタデータ.R_印刷設定マスタ.登録済みレコード数
            tbl.Rows.Add()

            tbl.Rows(s).Item("採点方式") = マスタデータ.R_印刷設定マスタ.リスト(s).採点方式
            tbl.Rows(s).Item("ラウンド") = マスタデータ.R_印刷設定マスタ.リスト(s).ラウンド
            tbl.Rows(s).Item("帳票記号") = マスタデータ.R_印刷設定マスタ.リスト(s).帳票記号

            For i = 1 To 20
                tbl.Rows(s).Item("配布先_" & i) = マスタデータ.R_印刷設定マスタ.リスト(s).印刷部数(i)
            Next i
        Next s




        '// DataGridViewにデータセットを設定
        Me.DGV_印刷設定.DataSource = tbl



        '===列幅の自動調整
        Me.DGV_印刷設定.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_印刷設定.AllowUserToResizeColumns = True



        'マスタデータの開放
        マスタデータ = Nothing

        '並び替えができないようにする ソート禁止
        'For Each c As DataGridViewColumn In Me.DGV_印刷設定.Columns
        ' c.SortMode = DataGridViewColumnSortMode.NotSortable
        'Next c



    End Sub


    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'D_種目マスタから該当のKey項目をDelete
        マスタデータ.R_印刷設定マスタ.Deleteレコード()


        'DGVの値をAdd

        '1行目を配布先に設定
        For i = 1 To 20
            マスタデータ.R_印刷設定マスタ.配布先(i) = DGV_印刷設定.Rows(0).Cells("配布先_" & i).Value
        Next i

        Dim 印刷設定C As R_印刷設定


        For i = 1 To Me.DGV_印刷設定.RowCount - 1

            If Me.DGV_印刷設定.Rows(i).Cells("採点方式").Value IsNot Nothing Then

                印刷設定C = New R_印刷設定

                印刷設定C.採点方式 = Me.DGV_印刷設定.Rows(i).Cells("採点方式").Value
                印刷設定C.ラウンド = Me.DGV_印刷設定.Rows(i).Cells("ラウンド").Value
                印刷設定C.帳票記号 = Me.DGV_印刷設定.Rows(i).Cells("帳票記号").Value

                For s = 1 To 20
                    If DGV_印刷設定.Rows(i).Cells("配布先_" & s).Value Is DBNull.Value Then
                        印刷設定C.印刷部数(s) = 0
                    ElseIf IsNumeric(DGV_印刷設定.Rows(i).Cells("配布先_" & s).Value) Then
                        印刷設定C.印刷部数(s) = DGV_印刷設定.Rows(i).Cells("配布先_" & s).Value
                    Else
                        印刷設定C.印刷部数(s) = 0
                    End If
                Next s


                マスタデータ.R_印刷設定マスタ.登録(印刷設定C)
            End If

        Next i

        印刷設定C = Nothing
        マスタデータ = Nothing


        DGV更新()

        更新FLAG = False

    End Sub

    'セルが変更されたら
    Private Sub DGV_印刷設定_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DGV_印刷設定.CurrentCellDirtyStateChanged


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