Public Class F154_団体区分結果

    Private 区分番号 As String

    Private 団体区分番号 As String


    Private 対象区分番号() As String
    Private 対象区分_NO(19) As Integer

    Public File有り As Boolean

    Private Sub F154_団体区分結果_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Public Sub New(区分番号_ As String, 団体区分番号_ As String)

        File有り = True

        区分番号 = 区分番号_
        団体区分番号 = 団体区分番号_

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        DGV更新()

    End Sub

    Private Sub DGV更新()

        Dim マスタデータ = New マスタデータ

        Dim SC_J_区分結果 As SC_J_区分結果
        SC_J_区分結果 = New SC_J_区分結果(マスタデータ.Z_システム設定.Comp_filepath)
        SC_J_区分結果.区分番号 = 区分番号
        SC_J_区分結果 = SC_J_区分結果.JSON読み込み

        '区分結果が無い時
        If SC_J_区分結果 Is Nothing Then
            File有り = False
            Exit Sub
        End If


        Me.LB_区分名.Text = 区分番号 & " " & マスタデータ.B_区分マスタ.Get区分表記名(区分番号)

        Dim 団体結果_J As 団体結果_J
        団体結果_J = New 団体結果_J(マスタデータ.Z_システム設定.Comp_filepath)
        団体結果_J.団体区分番号 = 団体区分番号
        団体結果_J = 団体結果_J.JSON読み込み()

        If 団体結果_J Is Nothing Then
            MsgBox("団体結果が存在しません。")
            Exit Sub
        End If


        '対象区分の
        対象区分番号 = SC_J_区分結果.Get_ラウンド番号一覧



        '対象区分_NO は、実際に開催された区分の r値がセットされる
        For r = 1 To SC_J_区分結果.ラウンド数
            For rNo = 1 To UBound(対象区分番号)
                If SC_J_区分結果.SC_J_区分結果_ラウンド設定(r).ラウンド番号 = 対象区分番号(rNo) Then
                    対象区分_NO(rNo) = r
                    rNo = UBound(対象区分番号)
                End If
            Next rNo
        Next r



        'データクリア
        Me.DGV_区分結果.DataSource = Nothing
        Me.DGV_区分結果.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_区分結果.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)


        '列タイトル設定
        ' １列目 区分番号
        ' ２列目 区分記号
        ' ３列目 区分名
        ' ４列目 カテゴリ
        ' ５列目 審判G


        '// データテーブルの作成
        Dim tbl As New DataTable
        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("得点", GetType(Decimal)))
        tbl.Columns.Add(New DataColumn("総合順位", GetType(Integer)))　　　'団体用
        tbl.Columns.Add(New DataColumn("ラウンド", GetType(String)))
        tbl.Columns.Add(New DataColumn("選手M番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("背番号", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("リーダー名", GetType(String)))
        tbl.Columns.Add(New DataColumn("パートナー名", GetType(String)))
        tbl.Columns.Add(New DataColumn("カップル所属", GetType(String)))

        tbl.Columns.Add(New DataColumn("R1", GetType(String)))
        tbl.Columns.Add(New DataColumn("ReDance", GetType(String)))
        tbl.Columns.Add(New DataColumn("R1同", GetType(String)))
        tbl.Columns.Add(New DataColumn("R2", GetType(String)))
        tbl.Columns.Add(New DataColumn("R2同", GetType(String)))
        tbl.Columns.Add(New DataColumn("R3", GetType(String)))
        tbl.Columns.Add(New DataColumn("R3同", GetType(String)))
        tbl.Columns.Add(New DataColumn("R4", GetType(String)))
        tbl.Columns.Add(New DataColumn("R4同", GetType(String)))
        tbl.Columns.Add(New DataColumn("R5", GetType(String)))
        tbl.Columns.Add(New DataColumn("R5同", GetType(String)))
        tbl.Columns.Add(New DataColumn("最終", GetType(String)))
        tbl.Columns.Add(New DataColumn("最終同", GetType(String)))
        tbl.Columns.Add(New DataColumn("準々", GetType(String)))
        tbl.Columns.Add(New DataColumn("準々同", GetType(String)))
        tbl.Columns.Add(New DataColumn("SF", GetType(String)))
        tbl.Columns.Add(New DataColumn("SF同", GetType(String)))
        tbl.Columns.Add(New DataColumn("下位決", GetType(String)))
        tbl.Columns.Add(New DataColumn("Final", GetType(String)))

        For s = 1 To SC_J_区分結果.選手数

            If SC_J_区分結果.SC_J_区分結果_選手(s).No <> 0 Then

                tbl.Rows.Add()
                Dim idx = tbl.Rows.Count - 1
                tbl.Rows(idx).Item("No") = SC_J_区分結果.SC_J_区分結果_選手(s).No
                tbl.Rows(idx).Item("総合順位") = SC_J_区分結果.SC_J_区分結果_選手(s).総合順位
                tbl.Rows(idx).Item("ラウンド") = SC_J_区分結果.SC_J_区分結果_選手(s).ラウンド
                tbl.Rows(idx).Item("選手M番号") = SC_J_区分結果.SC_J_区分結果_選手(s).選手M番号
                If IsNumeric(SC_J_区分結果.SC_J_区分結果_選手(s).背番号) Then
                    tbl.Rows(idx).Item("背番号") = CInt(SC_J_区分結果.SC_J_区分結果_選手(s).背番号)
                Else
                    MsgBox("背番号「" & SC_J_区分結果.SC_J_区分結果_選手(s).背番号 & "」は数値ではないため、999として表示します。")
                    tbl.Rows(idx).Item("背番号") = 999
                End If
                tbl.Rows(idx).Item("リーダー名") = SC_J_区分結果.SC_J_区分結果_選手(s).リーダー名
                tbl.Rows(idx).Item("パートナー名") = SC_J_区分結果.SC_J_区分結果_選手(s).パートナー名
                tbl.Rows(idx).Item("カップル所属") = SC_J_区分結果.SC_J_区分結果_選手(s).カップル所属


                For rr = 1 To UBound(対象区分番号)
                    If 対象区分_NO(rr) <> 0 Then
                        tbl.Rows(idx).Item(8 + rr) = 変換(SC_J_区分結果, rr, s)
                    End If
                Next rr

                'For rr = 1 To UBound(対象区分_NO)
                'If 対象区分_NO(rr) <> 0 Then
                'tbl.Rows(idx).Item(8 + rr) = 変換(SC_J_区分結果, rr, s)
                'End If
                'Next rr

                '団体用追加
                tbl.Rows(idx).Item("得点") = 団体結果_J.Get_得点(区分番号, SC_J_区分結果.SC_J_区分結果_選手(s).背番号）



            End If

        Next s



        '// DataGridViewにデータセットを設定
        Me.DGV_区分結果.DataSource = tbl






        '==列を非表示
        Me.DGV_区分結果.Columns("選手M番号").Visible = False

        For r = 1 To 19
            If 対象区分_NO(r) <> 0 Then
                Me.DGV_区分結果.Columns(8 + r).Visible = True
            Else
                Me.DGV_区分結果.Columns(8 + r).Visible = False
            End If
        Next r



        '===列幅の自動調整
        Me.DGV_区分結果.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_区分結果.AllowUserToResizeColumns = True

        '===行の高さの自動設定
        Me.DGV_区分結果.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders








        マスタデータ = Nothing
        SC_J_区分結果 = Nothing


    End Sub

    Private Function 変換(SC_J_区分結果 As SC_J_区分結果, rNo As Integer, 選手番号 As Integer) As String

        Dim rc As String = ""

        If SC_J_区分結果.SC_J_区分結果_選手(選手番号).SC_J_区分結果_選手_ラウンド結果(rNo).順位 = 0 Then
            rc = ""
        Else
            If SC_J_区分結果.SC_J_区分結果_ラウンド設定(対象区分_NO(rNo)).採点方式 = "順位法" Then
                rc = SC_J_区分結果.SC_J_区分結果_選手(選手番号).SC_J_区分結果_選手_ラウンド結果(rNo).順位
            Else
                rc = SC_J_区分結果.SC_J_区分結果_選手(選手番号).SC_J_区分結果_選手_ラウンド結果(rNo).得点
            End If
        End If
        Return rc

    End Function



End Class