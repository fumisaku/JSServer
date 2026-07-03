Public Class F160_総合結果設定

    Private 更新FLAG As Boolean

    Private 総合区分番号 As String
    Private マスタデータ As マスタデータ

    Private Sub F160_総合結果設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False


    End Sub


    Public Sub New(総合区分番号_ As String)

        総合区分番号 = 総合区分番号_

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        マスタデータ = New マスタデータ


        更新()

    End Sub



    Private Sub 更新()

        Me.LB_区分名.Text = 総合区分番号 & " " & マスタデータ.B_区分マスタ.Get区分表記名(総合区分番号)




        'データクリア
        Me.DGV.DataSource = Nothing
        Me.DGV.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)


        '列タイトル設定

        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("ラウンド番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド名", GetType(String)))
        tbl.Columns.Add(New DataColumn("対象区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("対象区分名", GetType(String)))
        tbl.Columns.Add(New DataColumn("対象ラウンド番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("対象ラウンド名", GetType(String)))


        'Kマスターがある時

        For i = 1 To マスタデータ.K_総合結果設定.登録済みレコード数

            Dim 総合設定 As K_総合設定
            総合設定 = マスタデータ.K_総合結果設定.リスト(i)

            If 総合設定.総合区分番号 = 総合区分番号 Then

                tbl.Rows.Add()
                Dim idx = tbl.Rows.Count - 1


                tbl.Rows(idx).Item("ラウンド番号") = 総合設定.総合ラウンド番号
                tbl.Rows(idx).Item("ラウンド名") = マスタデータ.Get_ラウンド名(総合設定.総合ラウンド番号)

                tbl.Rows(idx).Item("対象区分番号") = 総合設定.対象区分番号
                tbl.Rows(idx).Item("対象区分名") = マスタデータ.B_区分マスタ.Get区分表記名(総合設定.対象区分番号)

                tbl.Rows(idx).Item("対象ラウンド番号") = 総合設定.対象ラウンド番号
                tbl.Rows(idx).Item("対象ラウンド名") = マスタデータ.Get_ラウンド名(総合設定.対象ラウンド番号)


            End If

        Next i





        '// DataGridViewにデータセットを設定
        Me.DGV.DataSource = tbl




        '==列を非表示
        'Me.DGV.Columns("区分名").Visible = False


        '===列幅の自動調整
        Me.DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV.AllowUserToResizeColumns = True

        '===行の高さの自動設定
        Me.DGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders





    End Sub

    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim 総合設定C As K_総合設定

        For i = 0 To Me.DGV.RowCount - 1

            If Me.DGV.Rows(i).Cells("ラウンド番号").Value IsNot Nothing Then

                '入力チェック
                If Me.DGV.Rows(i).Cells("ラウンド番号").Value Is DBNull.Value Then
                    MsgBox("ラウンド番号が入力されていません。")
                    マスタデータ = Nothing
                    Exit Sub
                End If


                総合設定C = New K_総合設定
                総合設定C.総合区分番号 = 総合区分番号
                総合設定C.総合ラウンド番号 = Me.DGV.Rows(i).Cells("ラウンド番号").Value
                総合設定C.対象区分番号 = Me.DGV.Rows(i).Cells("対象区分番号").Value
                総合設定C.対象ラウンド番号 = Me.DGV.Rows(i).Cells("対象ラウンド番号").Value

                マスタデータ.K_総合結果設定.登録(総合設定C)
            End If
        Next i

        総合設定C = Nothing
        マスタデータ = Nothing


        更新（）

        更新FLAG = False

    End Sub

    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click

        Me.Close()

    End Sub

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

    Private Sub PB_総合結果集計_Click(sender As Object, e As EventArgs) Handles PB_総合結果集計.Click

        '行が選択されていない時は何もしない
        If Me.DGV.CurrentRow IsNot Nothing Then

            Dim 採点結果 As 採点結果_C = Nothing

            Dim 集計対象_ラウンド番号 As String = Me.DGV.CurrentRow.Cells("ラウンド番号").Value

            Dim 対象数 As Integer = 0
            For i = 0 To Me.DGV.RowCount - 1
                If Me.DGV.Rows(i).Cells("ラウンド番号").Value = 集計対象_ラウンド番号 Then


                    対象数 = 対象数 + 1

                    If 対象数 = 1 Then
                        採点結果 = New 採点結果_C(Me.DGV.Rows(i).Cells("対象区分番号").Value, Me.DGV.Rows(i).Cells("対象ラウンド番号").Value)

                    Else
                        Dim 追加採点結果 As 採点結果_C
                        追加採点結果 = New 採点結果_C(Me.DGV.Rows(i).Cells("対象区分番号").Value, Me.DGV.Rows(i).Cells("対象ラウンド番号").Value)

                        採点結果 = 採点結果.合成(総合区分番号, 集計対象_ラウンド番号, 追加採点結果)

                    End If
                End If
            Next i

            総合結果作成(採点結果)

            Dim F161 As F161_総合結果
            F161 = New F161_総合結果(総合区分番号, 集計対象_ラウンド番号)
            F161.Show()




        Else
            MsgBox("編集対象行を選択してください。")
        End If



    End Sub

    Private Sub 総合結果作成(採点結果 As 採点結果_C)

        '採点結果の内容から、総合結果を作成する。

        Dim 総合結果 = New 総合結果_J

        総合結果.総合区分番号 = 採点結果.区分番号
        総合結果.総合ラウンド番号 = 採点結果.ラウンド番号
        '総合結果.選手数 = 採点結果.出場選手数
        '総合結果.区分種目数 = 採点結果.種目数

        For d = 1 To 採点結果.種目数
            総合結果.Add_区分定義(採点結果.種目(d).区分番号, 採点結果.種目(d).種目記号, "")
        Next d

        For s = 1 To 採点結果.出場選手数
            総合結果.add_選手結果(採点結果.背番号(s), 採点結果.総合順位表記(s), 採点結果.総合得点(s))

            For d = 1 To 総合結果.区分種目数
                Dim 選手番号 As Integer = 0
                For ss = 1 To 採点結果.種目(d).選手数
                    If 採点結果.種目(d).選手(ss).背番号 = 採点結果.背番号(s) Then

                        総合結果.選手結果(s).区分種目結果(d).区分得点 = 採点結果.種目(d).選手(ss).種目得点

                        ss = 採点結果.種目(d).選手数
                    End If
                Next ss
            Next d
        Next s

        総合結果.JSON書き出し()


    End Sub

End Class