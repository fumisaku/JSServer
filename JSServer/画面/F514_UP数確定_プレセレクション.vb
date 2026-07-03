Public Class F514_UP数確定_プレセレクション


    Private 採点結果 As 採点結果_C
    Private 区分番号, ラウンド番号 As String

    Private UP予定数 As Integer
    Private UP数 As Integer

    Private Sub F514_UP数確定_プレセレクション_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '表示位置の設定
        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        行色付け()
    End Sub

    Public Sub 設定(区分番号_ As String, ラウンド番号_ As String, 採点結果_ As 採点結果_C)

        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_
        採点結果 = 採点結果_


        Dim 人数 As Integer = 0

        '===BJPR用=　ジャッジ順位点の算出====
        Dim ジャッジ毎得点() As Decimal
        Dim ジャッジ毎順位点() As Decimal

        Dim 順位点() As Decimal   'これが順位点　　⇒　採点結果,総合順位点に置き換え済。よっては今は使用していない


        ReDim 順位点(採点結果.出場選手数)


        For d = 1 To 採点結果.種目数
            For j = 1 To 採点結果.種目(d).審判員数

                '
                ReDim ジャッジ毎得点(採点結果.出場選手数)

                For s = 1 To 採点結果.出場選手数
                    ジャッジ毎得点(s) = 採点結果.種目(d).選手(s).審判(j).素点
                Next s

                '====ソート　ジャッジ毎得点を基に、ジャッジ毎順位点を算出
                ReDim ジャッジ毎順位点(採点結果.出場選手数)

                Dim 順位 As Integer = 1

                Do While 順位 < 採点結果.出場選手数
                    Dim 最大値 As Decimal = 0
                    人数 = 0

                    '最大値を探す
                    For s = 1 To 採点結果.出場選手数
                        If ジャッジ毎得点(s) > 最大値 Then
                            最大値 = ジャッジ毎得点(s)
                        End If
                    Next s

                    '最大値と同じ点数を持っている選手に順位を付けて、その得点を -10にする。
                    For s = 1 To 採点結果.出場選手数
                        If ジャッジ毎得点(s) = 最大値 Then
                            ジャッジ毎順位点(s) = 順位
                            人数 = 人数 + 1
                            ジャッジ毎得点(s) = -10
                        End If
                    Next s

                    '次の順位
                    順位 = 順位 + 人数
                Loop

                'ジャッジ毎順位点を、順位点に入れる
                For s = 1 To 採点結果.出場選手数
                    順位点(s) = 順位点(s) + ジャッジ毎順位点(s)
                Next s


            Next j

        Next d
        '===============



        'ラベルの設定
        Me.LB_区分名.Text = 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & " " & 採点結果.マスタデータ.Get_ラウンド名(ラウンド番号)

        Me.LB_出場組数.Text = 採点結果.出場選手数 & " 組"

        UP予定数 = 採点結果.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).UP予定数
        Me.LB_UP予定数.Text = UP予定数 & " 組"


        'データクリア
        Me.DGV_UP数.DataSource = Nothing
        Me.DGV_UP数.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_UP数.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_UP数.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)

        'DGVのデフォルト配置を真ん中にする
        Me.DGV_UP数.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        '// データテーブルの作成
        Dim tbl As New DataTable
        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("選手No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("選手名", GetType(String)))
        tbl.Columns.Add(New DataColumn("得点", GetType(Double)))
        tbl.Columns.Add(New DataColumn("J順位点", GetType(Double)))
        tbl.Columns.Add(New DataColumn("順位", GetType(Double)))
        ' tbl.Columns.Add(New DataColumn("累計", GetType(Double)))
        tbl.Columns.Add(New DataColumn("UP", GetType(Integer)))


        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ




        Dim 行番号 As Integer = 0
        人数 = 0

        For 順位 = 1 To 採点結果.出場選手数


            For t = 1 To 採点結果.出場選手数
                If 順位 = 採点結果.総合順位番号(t) Then

                    人数 = 人数 + 1

                    tbl.Rows.Add()
                    tbl.Rows(行番号).Item("No") = 行番号 + 1
                    tbl.Rows(行番号).Item("選手No") = 採点結果.背番号(t)

                    Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 採点結果.背番号(t))
                    tbl.Rows(行番号).Item("選手名") = 選手.リーダー表記名

                    tbl.Rows(行番号).Item("得点") = Format(CDbl(採点結果.総合得点(t)), "0.000")
                    'tbl.Rows(行番号).Item("J順位点") = Format(CDbl(順位点(t)), "0")
                    tbl.Rows(行番号).Item("J順位点") = Format(CDbl(採点結果.総合順位点(t)), "0")
                    tbl.Rows(行番号).Item("順位") = 採点結果.総合順位表記(t)
                    '  tbl.Rows(行番号).Item("累計") = 人数

                    If UP予定数 >= 採点結果.総合順位表記(t) Then
                        tbl.Rows(行番号).Item("UP") = 1
                    Else
                        tbl.Rows(行番号).Item("UP") = 0
                    End If

                    行番号 = 行番号 + 1
                End If
            Next t

        Next 順位



        '// DataGridViewにデータセットを設定
        Me.DGV_UP数.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_UP数.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_UP数.AllowUserToResizeColumns = True

    End Sub

    Private Sub 行色付け()

        UP数 = 0
        For i = 0 To Me.DGV_UP数.RowCount - 1
            If CInt(Me.DGV_UP数.Rows(i).Cells("UP").Value) = 1 Then
                UP数 = UP数 + 1
            End If
        Next i

        If UP数 = UP予定数 Then
            Me.LB_UP数.Text = Me.LB_UP予定数.Text & " 一致"
            Me.LB_UP数.ForeColor = Color.Blue

        ElseIf UP数 < UP予定数 Then
            Me.LB_UP数.Text = UP数 & " 不足"
            Me.LB_UP数.ForeColor = Color.Red

        Else
            Me.LB_UP数.Text = UP数 & " オーバー"
            Me.LB_UP数.ForeColor = Color.Red


        End If


        '====UP数の色設定
        Dim 人数 As Integer = 0
        For i = 0 To Me.DGV_UP数.RowCount - 1

            If CInt(Me.DGV_UP数.Rows(i).Cells("UP").Value) = 1 Then
                人数 = 人数 + 1
            End If

            If CInt(Me.DGV_UP数.Rows(i).Cells("UP").Value) = 1 And 人数 <= UP予定数 Then

                Me.DGV_UP数.Rows(i).DefaultCellStyle.BackColor = Color.Cyan

            ElseIf CInt(Me.DGV_UP数.Rows(i).Cells("UP").Value) = 1 And 人数 <= UP数 Then

                Me.DGV_UP数.Rows(i).DefaultCellStyle.BackColor = Color.Pink
            Else
                Me.DGV_UP数.Rows(i).DefaultCellStyle.BackColor = Color.Empty

            End If



        Next i


    End Sub


    Private Sub PB_確定_Click(sender As Object, e As EventArgs) Handles PB_確定.Click

        'ラウンド番号　xx9  にUP者のヒート表を作成する。


        If MsgBox("UP数は" & UP数 & "ですね？", vbYesNo) = vbYes Then


            '背番号リストの作成
            Dim 背番号リスト() As String
            ReDim 背番号リスト(UP数)

            Dim 人数 As Integer = 0

            For i = 0 To Me.DGV_UP数.RowCount - 1
                For s = 1 To UBound(採点結果.背番号)
                    If Me.DGV_UP数.Rows(i).Cells("選手No").Value = 採点結果.背番号(s) Then

                        If Me.DGV_UP数.Rows(i).Cells("UP").Value = 1 Then
                            人数 = 人数 + 1
                            背番号リスト(人数) = 採点結果.背番号(s)
                        End If


                        s = UBound(採点結果.背番号)
                    End If
                Next s
            Next i

            'ヒート表作成
            Dim マスタデータ = New マスタデータ

            Dim 新ラウンド番号 As String = Strings.Left(ラウンド番号, 2) & "9"

            '古いヒート表を消す
            マスタデータ.E_ヒート表マスタ.Deleteレコード(区分番号, 新ラウンド番号)



            For i = 1 To UP数
                Dim ヒートC = New E_ヒート表

                ヒートC.背番号 = 背番号リスト(i)
                ヒートC.ヒート番号(1) = 1

                マスタデータ.E_ヒート表マスタ.登録(ヒートC, 区分番号, 新ラウンド番号)

            Next i




            'ステータス更新
            Dim 現採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

            現採点進行C.ステータス = "採点済み"
            マスタデータ.T_採点進行管理.登録(現採点進行C)

            マスタデータ = Nothing


        End If

        Me.Close()
    End Sub




    Private Sub PB_次へ_Click(sender As Object, e As EventArgs) 'Handles PB_次へ.Click


        '次ランド番号の確定
        Dim 次ラウンドC As C_ラウンド = 採点結果.マスタデータ.C_ラウンドマスタ.Get_次ラウンドClass(区分番号, ラウンド番号)
        Dim 次ラウンド番号 As String = 次ラウンドC.ラウンド番号


        '同点決勝の場合は元予選で確定済みの選手情報を取得
        '　ラウンド番号の下一桁が １か２
        Dim 既確定人数 As Integer = 0
        Dim 既確定背番号リスト() As String = Nothing

        If ラウンド番号.Substring(2, 1) = "1" Or ラウンド番号.Substring(2, 1) = "2" Then

            Dim 前ラウンド番号 As String = ラウンド番号.Substring(0, 2) & "0"
            Dim 前採点結果 As 採点結果_C
            前採点結果 = New 採点結果_C(区分番号, 前ラウンド番号)

            Dim 前UP予定数 = 前採点結果.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, 前ラウンド番号).UP予定数

            '総合順位表記の何位の人が、同点決勝対象者だったか
            Dim 対象総合順位表示 As Integer = 0
            For i = 1 To 前採点結果.出場選手数
                If 前採点結果.総合順位表記(i) < 前UP予定数 And 前採点結果.総合順位表記(i) > 対象総合順位表示 Then
                    対象総合順位表示 = 前採点結果.総合順位表記(i)
                End If
            Next i

            'その総合順位表記よりも前の人が何人いるか（既に次予選に進出が確定している人は何人か？）
            For i = 1 To 前採点結果.出場選手数
                If 前採点結果.総合順位表記(i) < 対象総合順位表示 Then
                    既確定人数 = 既確定人数 + 1
                End If
            Next i

            '既確定背番号リストの作成
            If 既確定人数 > 0 Then
                ReDim 既確定背番号リスト(既確定人数）
            End If

            Dim k = 0
            For i = 1 To 前採点結果.出場選手数
                If 前採点結果.総合順位表記(i) < 対象総合順位表示 Then
                    k = k + 1
                    既確定背番号リスト(k) = 前採点結果.背番号(i)
                End If
            Next i

        End If




        '背番号リストの作成
        Dim 背番号リスト() As String
        ReDim 背番号リスト(UP数 + 既確定人数)

        Dim 人数 As Integer = 0
        For i = 1 To 採点結果.出場選手数
            If 採点結果.総合順位表記(i) <= UP数 Then
                人数 = 人数 + 1
                背番号リスト(人数) = 採点結果.背番号(i)
            End If
        Next i

        '同点決勝の時は追加する
        If 既確定人数 > 0 Then
            For i = 1 To 既確定人数
                人数 = 人数 + 1
                背番号リスト(人数) = 既確定背番号リスト(i)
            Next i
        End If

        'F511 ヒート表作成画面の表示
        Dim F511 As F511_ヒート表作成
        F511 = New F511_ヒート表作成

        F511.設定(区分番号, 次ラウンド番号, 背番号リスト)

        F511.ShowDialog()


    End Sub

    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click
        Me.Close()
    End Sub


    'セルが更新されたら
    Private Sub DGV_UP数_CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DGV_UP数.CellValueChanged

        行色付け()

        'If e.RowIndex >= 0 Then

        'If Me.DGV_UP数(6, e.RowIndex).Value = 1 Then   'UP
        '                Me.DGV_UP数.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Cyan
        'Else
        '               Me.DGV_UP数.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Empty
        'End If


        'End If
    End Sub


    'セルがさわれたらコミットする。
    Private Sub DGV_UP数_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DGV_UP数.CurrentCellDirtyStateChanged
        If DGV_UP数.CurrentCellAddress.X = 0 AndAlso
            DGV_UP数.IsCurrentCellDirty Then
            'コミットする
            DGV_UP数.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub


End Class