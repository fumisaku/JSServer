Public Class F600_進行管理

    Private マスタデータ As マスタデータ

    Private 現在進行 As U_進行

    Private 前進行 As U_進行
    Private 次進行 As U_進行


    Private 現在区分番号 As String
    Private 現在ラウンド番号 As String




    Private Sub F600_進行管理_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False

        マスタデータ = New マスタデータ
        マスタデータ.U_進行管理.FileRead()
        現在進行 = マスタデータ.U_進行管理.Get_現在進行

        更新()

    End Sub

    Sub MyForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ' 選択されているセルをなくす
        Me.DGV_種目リスト.CurrentCell = Nothing
        Me.DGV_ヒート.CurrentCell = Nothing
    End Sub


    Private Sub 更新()

        '現在ステータスの取得

        マスタデータ.T_採点進行管理.FileRead()
        マスタデータ.T_採点進行管理.Get_区分ラウンド番号(現在進行.競技番号, 現在進行.競技番号枝番, 現在区分番号, 現在ラウンド番号)

        区分更新()


    End Sub

    Private Sub 区分更新()

        Me.TB_現在競技.Text = "【" & 現在進行.競技番号 & "-" & 現在進行.競技番号枝番 & "】" &
                              マスタデータ.B_区分マスタ.Get区分表記名(現在区分番号) & vbCrLf &
                              "(" & マスタデータ.Get_ラウンド名(現在ラウンド番号) & ")"

        '次の競技の表示
        Dim 次区分番号 As String = ""
        Dim 次ラウンド番号 As String = ""
        Dim 次進行 As U_進行 = マスタデータ.U_進行管理.Get_次進行(現在進行.競技番号, 現在進行.競技番号枝番)
        マスタデータ.T_採点進行管理.Get_区分ラウンド番号(次進行.競技番号, 次進行.競技番号枝番, 次区分番号, 次ラウンド番号)


        Me.TB_次競技.Text = "【" & 次進行.競技番号 & "-" & 次進行.競技番号枝番 & "】" &
                              マスタデータ.B_区分マスタ.Get区分表記名(次区分番号) & vbCrLf &
                              "(" & マスタデータ.Get_ラウンド名(次ラウンド番号) & ")"


        '次の次の競技の表示
        次区分番号 = ""
        次ラウンド番号 = ""
        次進行 = マスタデータ.U_進行管理.Get_次進行(次進行.競技番号, 次進行.競技番号枝番)
        マスタデータ.T_採点進行管理.Get_区分ラウンド番号(次進行.競技番号, 次進行.競技番号枝番, 次区分番号, 次ラウンド番号)


        Me.TB_次次競技.Text = "【" & 次進行.競技番号 & "-" & 次進行.競技番号枝番 & "】" &
                              マスタデータ.B_区分マスタ.Get区分表記名(次区分番号) & vbCrLf &
                              "(" & マスタデータ.Get_ラウンド名(次ラウンド番号) & ")"


        DGV_種目リストの更新(現在区分番号, 現在ラウンド番号)
        DGV_ヒートの更新(現在区分番号, 現在ラウンド番号, 現在進行.種目順)

        現在区分情報更新()

        前ヒート設定()
        現在ヒート設定()
        次ヒート設定()


    End Sub

    'DGV_種目の更新
    Private Sub DGV_種目リストの更新(区分番号 As String, ラウンド番号 As String)

        Me.DGV_種目リスト.DefaultCellStyle.Font = New Font("MSゴシック", 14, FontStyle.Regular)

        Me.DGV_種目リスト.Rows.Clear()


        Try
            Dim 種目記号リスト() = Nothing
            For s = 1 To マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)
                Me.DGV_種目リスト.Rows.Add()
                Me.DGV_種目リスト.Rows(s - 1).Cells(0).Value = s
                Dim 種目 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, s)
                If 種目.SG種別 = "S" Then
                    Me.DGV_種目リスト.Rows(s - 1).Cells(1).Value = 種目.種目記号 & "(ソロ)"
                ElseIf 種目.SG種別 = "G" Then
                    Me.DGV_種目リスト.Rows(s - 1).Cells(1).Value = 種目.種目記号 & "(全員)"
                ElseIf 種目.SG種別 = "D" Then
                    Me.DGV_種目リスト.Rows(s - 1).Cells(1).Value = 種目.種目記号 & "(対戦)"
                ElseIf 種目.種目記号 <> "" Then
                    Me.DGV_種目リスト.Rows(s - 1).Cells(1).Value = 種目.種目記号
                End If
            Next s

            'DGV_種目色付け()

        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try


        Me.DGV_種目リスト.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells


        For i = 0 To Me.DGV_種目リスト.RowCount - 1
            If i = 現在進行.種目順 - 1 Then
                Me.DGV_種目リスト.Rows(i).Cells(0).Style.BackColor = Color.Cyan
                Me.DGV_種目リスト.Rows(i).Cells(1).Style.BackColor = Color.Cyan

            Else
                Me.DGV_種目リスト.Rows(i).Cells(0).Style.BackColor = Nothing
                Me.DGV_種目リスト.Rows(i).Cells(1).Style.BackColor = Nothing

            End If
        Next i

        Me.DGV_種目リスト.CurrentCell = Nothing

    End Sub

    Private Sub DGV_ヒートの更新(区分番号 As String, ラウンド番号 As String, 種目順 As Integer)

        'クリア
        Me.DGV_ヒート.Rows.Clear()

        Me.DGV_ヒート.DefaultCellStyle.Font = New Font("MSゴシック", 14, FontStyle.Regular)

        マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
        For h = 1 To マスタデータ.E_ヒート表マスタ.Getヒート数(種目順)
            Me.DGV_ヒート.Rows.Add()

            Dim 背番号リスト() = Nothing
            マスタデータ.E_ヒート表マスタ.Get_背番号リスト(種目順, h, 背番号リスト)
            'Dim 背番号 As String = ""
            'For s = 1 To UBound(背番号リスト)
            '背番号 = 背番号 & 背番号リスト(s) & " "
            'Next s

            Me.DGV_ヒート.Rows(h - 1).Cells(0).Value = h & "H " & UBound(背番号リスト) & "組"

            'Me.DGV_ヒート.Rows(h - 1).Cells(1).Value = 背番号
        Next h

        'DGV_ヒート色付け(種目順)


        Me.DGV_ヒート.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells


        For i = 0 To Me.DGV_ヒート.RowCount - 1
            If i = 現在進行.ヒート番号 - 1 Then
                Me.DGV_ヒート.Rows(i).Cells(0).Style.BackColor = Color.Cyan

            Else
                Me.DGV_ヒート.Rows(i).Cells(0).Style.BackColor = Nothing
            End If
        Next i


        Me.DGV_ヒート.CurrentCell = Nothing


    End Sub

    Private Sub 現在区分情報更新()

        Me.TB_採点方式.Text = ""
        Me.TB_出場組数.Text = ""
        Me.TB_ヒート数.Text = ""
        Me.TB_UP数.Text = ""
        Me.TB_種目数.Text = ""

        Dim 現在ラウンドClass As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(現在区分番号, 現在ラウンド番号)

        Me.TB_採点方式.Text = 現在ラウンドClass.採点方式
        Me.TB_出場組数.Text = "出場 " & マスタデータ.C_ラウンドマスタ.出場組数(現在区分番号, 現在ラウンド番号) & "組"

        If 現在ラウンド番号 <> "400" And 現在ラウンド番号 <> "300" Then
            Me.TB_UP数.Text = "UP数 " & 現在ラウンドClass.UP予定数 & "組"
        End If

        'ヒート数の算出
        Dim MINヒート数 As Integer = 100
        Dim MAXヒート数 As Integer = 0
        Dim 種目記号リスト() = Nothing
        Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(現在区分番号, 現在ラウンド番号, 種目記号リスト)
        マスタデータ.E_ヒート表マスタ.Read(現在区分番号, 現在ラウンド番号)
        For d = 1 To 種目数
            If MINヒート数 > マスタデータ.E_ヒート表マスタ.Getヒート数(d) Then
                MINヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(d)
            End If
            If MAXヒート数 < マスタデータ.E_ヒート表マスタ.Getヒート数(d) Then
                MAXヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(d)
            End If
        Next d

        If MINヒート数 <> MAXヒート数 And MINヒート数 <> 100 Then
            Me.TB_ヒート数.Text = "Heat数 " & MINヒート数 & "-" & MAXヒート数 & "H"
        Else
            Me.TB_ヒート数.Text = "Heat数 " & MAXヒート数 & "H"
        End If

        Me.TB_種目数.Text = "種目数 " & 種目数

    End Sub


    Private Sub 前ヒート設定()
        Me.TB_前種目.Text = ""
        Me.TB_前ヒート番号.Text = ""
        Me.TB_前組数.Text = ""
        Me.PB_前ヒート背番号.Text = ""

        前進行 = マスタデータ.U_進行管理.Get_進行by進行番号(現在進行.進行番号 - 1)

        Dim 前区分番号 As String = ""
        Dim 前ラウンド番号 As String = ""

        If 前進行 IsNot Nothing Then


            マスタデータ.T_採点進行管理.Get_区分ラウンド番号(前進行.競技番号, 前進行.競技番号枝番, 前区分番号, 前ラウンド番号)


            Dim 種目C As D_種目 = マスタデータ.D_種目マスタ.Get_種目Class(前区分番号, 前ラウンド番号, 前進行.種目順)

            Me.TB_前種目.Text = 種目C.種目記号
            Me.TB_前ヒート番号.Text = 前進行.ヒート番号 & "H"

            Dim 背番号リスト() = Nothing
            マスタデータ.E_ヒート表マスタ.Read(前区分番号, 前ラウンド番号)
            Dim 選手数 As Integer = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(前進行.種目順, 前進行.ヒート番号, 背番号リスト)

            Me.TB_前組数.Text = 選手数 & "組"

            Dim 背番号TEXT As String = ""

            For i = 1 To 選手数
                If i = 1 Then
                    背番号TEXT = 背番号リスト(i)
                Else
                    背番号TEXT = 背番号TEXT & "   " & 背番号リスト(i)
                End If
            Next i

            If 選手数 = 1 Then
                Dim 選手マスタ番号 As String = マスタデータ.B_区分マスタ.Get区分C(前区分番号).使用する選手マスタ
                Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタ番号, 背番号リスト(1))

                背番号TEXT = 背番号TEXT & " "
                背番号TEXT = 背番号TEXT & 選手.リーダー表記名 & "・" & 選手.パートナ表記名 & vbCrLf
                背番号TEXT = 背番号TEXT & "(" & 選手.リーダー所属名 & "/" & 選手.パートナ所属名 & ")"

            End If


            Me.PB_前ヒート背番号.Text = 背番号TEXT

            If 前進行.競技番号 <> 現在進行.競技番号 Or 前進行.競技番号枝番 <> 現在進行.競技番号枝番 Then
                Me.PB_前ヒート背番号.Text = Me.PB_前ヒート背番号.Text & "   【最終ヒート】"

            End If

        End If

    End Sub



    Private Sub 現在ヒート設定()
        Me.TB_現種目.Text = ""
        Me.TB_現在ヒート番号.Text = ""
        Me.TB_現組数.Text = ""
        Me.PB_現在ヒート背番号.Text = ""

        Dim 種目C As D_種目 = マスタデータ.D_種目マスタ.Get_種目Class(現在区分番号, 現在ラウンド番号, 現在進行.種目順)

        Me.TB_現種目.Text = 種目C.種目記号
        Me.TB_現在ヒート番号.Text = 現在進行.ヒート番号 & "H"

        Dim 背番号リスト() = Nothing
        マスタデータ.E_ヒート表マスタ.Read(現在区分番号, 現在ラウンド番号)
        Dim 選手数 As Integer = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(現在進行.種目順, 現在進行.ヒート番号, 背番号リスト)

        Me.TB_現組数.Text = 選手数 & "組"

        Dim 背番号TEXT As String = ""
        For i = 1 To 選手数
            If i = 1 Then
                背番号TEXT = 背番号リスト(i)
            Else
                背番号TEXT = 背番号TEXT & "   " & 背番号リスト(i)
            End If
        Next i

        '選手数が１の時は、選手名も表示する
        If 選手数 = 1 Then
            Dim 選手マスタ番号 As String = マスタデータ.B_区分マスタ.Get区分C(現在区分番号).使用する選手マスタ
            Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタ番号, 背番号リスト(1))

            背番号TEXT = 背番号TEXT & " "
            背番号TEXT = 背番号TEXT & 選手.リーダー表記名 & "・" & 選手.パートナ表記名 & vbCrLf
            背番号TEXT = 背番号TEXT & "(" & 選手.リーダー所属名 & "/" & 選手.パートナ所属名 & ")"

        End If

        Me.PB_現在ヒート背番号.Text = 背番号TEXT


    End Sub

    Private Sub 次ヒート設定()
        Me.TB_次種目.Text = ""
        Me.TB_次ヒート番号.Text = ""
        Me.TB_次組数.Text = ""
        Me.PB_次ヒート背番号.Text = ""

        'Dim 次進行 As U_進行 = マスタデータ.U_進行管理.Get_次進行(現在進行.競技番号, 現在進行.競技番号枝番)

        次進行 = マスタデータ.U_進行管理.Get_進行by進行番号(現在進行.進行番号 + 1)

        If 次進行.競技番号 = 現在進行.競技番号 And 次進行.競技番号枝番 = 現在進行.競技番号枝番 Then
            '同じ区分が続く時

            Dim 種目C As D_種目 = マスタデータ.D_種目マスタ.Get_種目Class(現在区分番号, 現在ラウンド番号, 次進行.種目順)

            Me.TB_次種目.Text = 種目C.種目記号
            Me.TB_次ヒート番号.Text = 次進行.ヒート番号 & "H"

            Dim 背番号リスト() = Nothing
            Dim 選手数 As Integer = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(次進行.種目順, 次進行.ヒート番号, 背番号リスト)

            Me.TB_次組数.Text = 選手数 & "組"

            Dim 背番号TEXT As String = ""
            For i = 1 To 選手数
                If i = 1 Then
                    背番号TEXT = 背番号リスト(i)
                Else
                    背番号TEXT = 背番号TEXT & "   " & 背番号リスト(i)
                End If
            Next i

            If 選手数 = 1 Then
                Dim 選手マスタ番号 As String = マスタデータ.B_区分マスタ.Get区分C(現在区分番号).使用する選手マスタ
                Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタ番号, 背番号リスト(1))

                背番号TEXT = 背番号TEXT & " "
                背番号TEXT = 背番号TEXT & 選手.リーダー表記名 & "・" & 選手.パートナ表記名 & vbCrLf
                背番号TEXT = 背番号TEXT & "(" & 選手.リーダー所属名 & "/" & 選手.パートナ所属名 & ")"

            End If

            Me.PB_次ヒート背番号.Text = 背番号TEXT

        Else
            '同じ区分が続かない時
            Me.PB_現在ヒート背番号.Text = Me.PB_現在ヒート背番号.Text & "   【最終ヒート】"

            Dim 次区分番号 As String = ""
            Dim 次ラウンド番号 As String = ""
            マスタデータ.T_採点進行管理.Get_区分ラウンド番号(次進行.競技番号, 次進行.競技番号枝番, 次区分番号, 次ラウンド番号)


            Me.PB_次ヒート背番号.Text = "【" & マスタデータ.B_区分マスタ.Get区分表記名(次区分番号) & " " &
                                               マスタデータ.Get_ラウンド名(次ラウンド番号) & "】"

        End If


    End Sub




    Private Sub PB_前ヒート背番号_Click(sender As Object, e As EventArgs) Handles PB_前ヒート背番号.Click
        Dim F601 As F601_名簿
        F601 = New F601_名簿
        F601.名簿表示(前進行.競技番号, 前進行.競技番号枝番, 前進行.種目順, 前進行.ヒート番号)
        F601.Show()

    End Sub

    Private Sub PB_現在ヒート背番号_Click(sender As Object, e As EventArgs) Handles PB_現在ヒート背番号.Click
        Dim F601 As F601_名簿
        F601 = New F601_名簿
        F601.名簿表示(現在進行.競技番号, 現在進行.競技番号枝番, 現在進行.種目順, 現在進行.ヒート番号)
        F601.Show()
    End Sub

    Private Sub PB_次ヒート背番号_Click(sender As Object, e As EventArgs) Handles PB_次ヒート背番号.Click
        Dim F601 As F601_名簿
        F601 = New F601_名簿
        F601.名簿表示(次進行.競技番号, 次進行.競技番号枝番, 次進行.種目順, 次進行.ヒート番号)
        F601.Show()
    End Sub

    Private Sub PB_開始_Click(sender As Object, e As EventArgs) Handles PB_開始.Click
        '現在進行を一つ進める

        現在進行 = マスタデータ.U_進行管理.Get_進行by進行番号(現在進行.進行番号 + 1)
        更新()

    End Sub

    Private Sub PB_進行管理_Click(sender As Object, e As EventArgs) Handles PB_進行管理.Click

        Dim F602 As F602_進行詳細
        F602 = New F602_進行詳細

        F602.Show()
        F602.更新()


    End Sub

    Private Sub PB_結果表示_Click(sender As Object, e As EventArgs) Handles PB_結果表示.Click
        Dim F603 As F603_区分一覧
        F603 = New F603_区分一覧

        F603.Show()

    End Sub
End Class