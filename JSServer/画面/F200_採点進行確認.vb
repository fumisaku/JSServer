Public Class F200_採点進行確認


    Private マスタデータ As マスタデータ

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        'マスタデータ = New マスタデータ

    End Sub

    Private Sub F200_採点進行確認_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False



        競技番号確認画面()

    End Sub


    Private Sub 競技番号確認画面()


        'Dim マスタデータ As マスタデータ
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

        tbl.Columns.Add(New DataColumn("競技番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("枝番", GetType(String)))
        tbl.Columns.Add(New DataColumn("ステータス", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分記号", GetType(String)))
        tbl.Columns.Add(New DataColumn("区分名", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("ラウンド", GetType(String)))
        tbl.Columns.Add(New DataColumn("カテゴリ", GetType(String)))
        tbl.Columns.Add(New DataColumn("リアル", GetType(String)))
        tbl.Columns.Add(New DataColumn("Heat数", GetType(String)))
        tbl.Columns.Add(New DataColumn("UP数", GetType(String)))
        tbl.Columns.Add(New DataColumn("ヒート割", GetType(String)))
        tbl.Columns.Add(New DataColumn("種目", GetType(String)))
        tbl.Columns.Add(New DataColumn("審判G", GetType(String)))
        tbl.Columns.Add(New DataColumn("選手M", GetType(String)))

        For i = 1 To マスタデータ.T_採点進行管理.登録済みレコード数
            Dim 採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.リスト(i)

            If 採点進行C IsNot Nothing Then
                Dim 区分C As B_区分 = マスタデータ.B_区分マスタ.Get区分C(採点進行C.区分番号)
                Dim ラウンドC As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(採点進行C.区分番号, 採点進行C.ラウンド番号)


                'Ver1.02.18で追加
                If ラウンドC Is Nothing Then
                    MsgBox("競技番号:" & 採点進行C.競技番号 & "は定義されていません。進行設定を確認してください。 対象の区分番号、ラウンド番号は、" & 採点進行C.区分番号 & ":" & 採点進行C.ラウンド番号)
                    Exit Sub
                End If


                tbl.Rows.Add()
                tbl.Rows(i - 1).Item("競技番号") = 採点進行C.競技番号
                tbl.Rows(i - 1).Item("枝番") = 採点進行C.競技番号枝番
                tbl.Rows(i - 1).Item("区分番号") = 採点進行C.区分番号
                tbl.Rows(i - 1).Item("区分名") = 区分C.区分表記名
                tbl.Rows(i - 1).Item("ラウンド番号") = 採点進行C.ラウンド番号
                tbl.Rows(i - 1).Item("ラウンド") = マスタデータ.Get_ラウンド名(採点進行C.ラウンド番号)
                tbl.Rows(i - 1).Item("カテゴリ") = 区分C.カテゴリ
                tbl.Rows(i - 1).Item("リアル") = 採点進行C.リアルタイムFLAG
                tbl.Rows(i - 1).Item("Heat数") = ラウンドC.ヒート数
                tbl.Rows(i - 1).Item("UP数") = ラウンドC.UP予定数
                tbl.Rows(i - 1).Item("ヒート割") = ラウンドC.ヒート割方式

                Dim 種目記号リスト() = Nothing
                Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(採点進行C.区分番号, 採点進行C.ラウンド番号, 種目記号リスト)
                Dim 種目記号 As String = ""

                For s = 1 To 種目数
                    種目記号 = 種目記号 & 種目記号リスト(s) & " "
                Next s

                tbl.Rows(i - 1).Item("種目") = 種目記号

                tbl.Rows(i - 1).Item("審判G") = ラウンドC.担当審判グループ
                tbl.Rows(i - 1).Item("選手M") = 区分C.使用する選手マスタ
                tbl.Rows(i - 1).Item("ステータス") = 採点進行C.ステータス

                区分C = Nothing
                ラウンドC = Nothing

            End If
        Next i



        '// DataGridViewにデータセットを設定
        Me.DGV_採点進行管理.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_採点進行管理.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Me.DGV_採点進行管理.AllowUserToResizeColumns = True


        '==ラウンド番号列を非表示
        Me.DGV_採点進行管理.Columns("ラウンド番号").Visible = False


        '並び替えができないようにする ソート禁止
        'For Each c As DataGridViewColumn In Me.DGV_採点進行管理.Columns
        'c.SortMode = DataGridViewColumnSortMode.NotSortable
        'Next c

        'マスタデータ = Nothing



    End Sub


    '行が選択されたら
    Private Sub DGV_採点進行管理_RowEnter(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DGV_採点進行管理.RowEnter

        If e.RowIndex >= 0 Then

            Dim 選択区分番号 As String = Me.DGV_採点進行管理.Rows(e.RowIndex).Cells("区分番号").Value

            For i = 0 To Me.DGV_採点進行管理.RowCount - 1
                If Me.DGV_採点進行管理.Rows(i).Cells("区分番号").Value = 選択区分番号 Then
                    Me.DGV_採点進行管理.Rows(i).DefaultCellStyle.BackColor = Color.Cyan
                Else
                    Me.DGV_採点進行管理.Rows(i).DefaultCellStyle.BackColor = Nothing

                End If
            Next i

        End If

        'If Me.DGV_競技種目.CurrentCellAddress.X = 0 AndAlso Me.DGV_競技種目.IsCurrentCellDirty Then
        'コミットする
        'Me.DGV_競技種目.CommitEdit(DataGridViewDataErrorContexts.Commit)
        'End If


    End Sub




    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click

        マスタデータ = Nothing
        Me.Close()

    End Sub

    'ヒート表の確認
    Private Sub PB_ヒート表_Click(sender As Object, e As EventArgs) Handles PB_ヒート表.Click

        Dim 選択行 As Integer = -1

        'どのセルが選択されているか？
        For Each c As DataGridViewCell In Me.DGV_採点進行管理.SelectedCells
            選択行 = c.RowIndex
            Exit For
        Next c

        If 選択行 = -1 Then
            MsgBox("行を選択してください。")
            Exit Sub
        End If

        Dim 区分番号 As String = ""
        Dim ラウンド番号 As String = ""

        区分番号 = Me.DGV_採点進行管理.Rows(選択行).Cells("区分番号").Value
        ラウンド番号 = Me.DGV_採点進行管理.Rows(選択行).Cells("ラウンド番号").Value

        Dim 背番号リスト() As String = Nothing

        'ヒート表が作成されているか確認
        If マスタデータ.E_ヒート表マスタ.FileCheck(区分番号, ラウンド番号) = False And
            ラウンド番号 <> "010" Then
            If MsgBox("まだヒート表が作成されていません。新たに作成しますか？", vbYesNo) = vbYes Then


            Else
                Exit Sub
            End If
        ElseIf マスタデータ.E_ヒート表マスタ.FileCheck(区分番号, ラウンド番号) = False And
            ラウンド番号 = "010" Then

            If MsgBox("まだヒート表が作成されていません。新たに作成しますか？", vbYesNo) = vbYes Then

                '1次予選の場合は背番号リストを作成する
                Dim 区分C As B_区分 = マスタデータ.B_区分マスタ.Get区分C(区分番号)
                Dim 背番号リストTEMP(200) As String
                Dim s As Integer = 0



                For i = 1 To マスタデータ.選手マスタ.登録済み選手数
                    If マスタデータ.選手マスタ.選手リスト(i).List番号 = 区分C.使用する選手マスタ And
                        マスタデータ.選手マスタ.選手リスト(i).エントリー区分(区分C.No) = "1" Then
                        'マスタデータ.選手マスタ.選手リスト(i).エントリー区分(CInt(区分番号)) = "1" Then



                        s = s + 1
                        背番号リストTEMP(s) = マスタデータ.選手マスタ.選手リスト(i).背番号
                    End If
                Next i

                If s = 0 Then
                    '指定された選手マスタに、該当区分に出場する選手が登録されていません。
                    MsgBox("指定された選手マスタ「" & 区分C.使用する選手マスタ & "」に、該当区分に出場する選手が一人も登録されていません。")
                    Exit Sub
                End If

                ReDim 背番号リスト(s)

                For i = 1 To s
                    背番号リスト(i) = 背番号リストTEMP(i)
                Next i
            Else
                Exit Sub
            End If

        End If


        'ヒート表の表示
        Dim F511 As F511_ヒート表作成
        F511 = New F511_ヒート表作成

        F511.設定(区分番号, ラウンド番号, 背番号リスト)
        F511.Show()


    End Sub

    '結果詳細画面の表示
    Private Sub PB_結果確認_Click(sender As Object, e As EventArgs) Handles PB_結果確認.Click

        Dim 選択行 As Integer = -1

        'どのセルが選択されているか？
        For Each c As DataGridViewCell In Me.DGV_採点進行管理.SelectedCells
            選択行 = c.RowIndex
            Exit For
        Next c

        If 選択行 = -1 Then
            MsgBox("行を選択してください。")
            Exit Sub
        End If

        Dim 区分番号 As String = ""
        Dim ラウンド番号 As String = ""

        区分番号 = Me.DGV_採点進行管理.Rows(選択行).Cells("区分番号").Value
        ラウンド番号 = Me.DGV_採点進行管理.Rows(選択行).Cells("ラウンド番号").Value

        'ヒート表が作成されているか確認
        If マスタデータ.E_ヒート表マスタ.FileCheck(区分番号, ラウンド番号) = False Then
            MsgBox("まだヒート表が作成されていません。")
            Exit Sub
        End If


        Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        If Strings.Left(採点方式, 3) = "PDJ" Then

            'ヒート表が作成されている場合は結果画面を表示
            Dim 採点結果_V2 As 採点結果_V2
            採点結果_V2 = New 採点結果_V2(区分番号, ラウンド番号)


            F501_採点詳細 = New F501_得点詳細_C
            F501_採点詳細.設定_V2(区分番号, ラウンド番号, 採点結果_V2)
            F501_採点詳細.Show()

        ElseIf Strings.Left(採点方式, 3) = "VAL" Then

            'ヒート表が作成されている場合は結果画面を表示
            Dim 採点結果_V2 As 採点結果_V2
            採点結果_V2 = New 採点結果_V2(区分番号, ラウンド番号)


            F501_採点詳細 = New F501_得点詳細_C
            F501_採点詳細.設定_V2(区分番号, ラウンド番号, 採点結果_V2)
            F501_採点詳細.Show()



        Else

            'ヒート表が作成されている場合は結果画面を表示
            Dim 採点結果 As 採点結果_C
            採点結果 = New 採点結果_C(区分番号, ラウンド番号)


            F501_採点詳細 = New F501_得点詳細_C
            F501_採点詳細.設定(区分番号, ラウンド番号, 採点結果)
            F501_採点詳細.Show()


        End If


    End Sub


    Public Sub F501_更新実行()

        Me.Invoke(F501_更新Delegate実行, New Object() {})

    End Sub

    Private Sub F501_更新()


        If F501_採点詳細 IsNot Nothing Then
            If F501_採点詳細.IsDisposed = True Then  '
                MsgBox("区分番号:" & F501_採点詳細.区分番号 & "の結果詳細の更新に失敗しました。")
            Else
                F501_採点詳細.更新()
            End If
        End If
    End Sub

    ' 'デリゲート宣言
    Delegate Sub F501_更新Delegate()
    'デリゲート宣言をデータ型とした変数を作成
    Private F501_更新Delegate実行 As New F501_更新Delegate(AddressOf F501_更新)



    '====== F501のリアル更新
    Delegate Sub F501_リアル更新Delegate(ジャッジ結果_J As S_採点結果_V2_J)
    Private F501_リアル更新Delegate実行 As New F501_リアル更新Delegate(AddressOf F501_リアル更新)

    Public Sub F501_リアル更新実行(ジャッジ結果_J As S_採点結果_V2_J)

        Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & ":F200 F501リアル更新呼び出し")


        Me.Invoke(F501_リアル更新Delegate実行, New Object() {ジャッジ結果_J})

    End Sub

    Private Sub F501_リアル更新(ジャッジ結果_J As S_採点結果_V2_J)

        If F501_採点詳細 IsNot Nothing Then
            If F501_採点詳細.IsDisposed = True Then  '

            Else
                F501_採点詳細.リアル更新(ジャッジ結果_J)
            End If
        End If

    End Sub






    'F501からの結果決定イベントを受信する。
    Private WithEvents F501_採点詳細 As F501_得点詳細_C
    Private Sub F501_結果決定(ByVal sender As Object, ByVal e As 結果決定EventArgs) Handles F501_採点詳細.結果決定

        '受信したイベントをMainに送る
        RaiseEvent 結果決定(Me, e)

    End Sub

    '受信したイベントをMainに送る
    Public Event 結果決定(ByVal sender As Object, ByVal e As 結果決定EventArgs)




    'F501からのPUSH確定ボタン通知イベントを受信する。
    Private Sub F501_Push確定ボタン(ByVal sender As Object, ByVal e As 結果決定EventArgs) Handles F501_採点詳細.Push確定ボタン

        '受信したイベントをMainに送る
        RaiseEvent Push確定ボタン(Me, e)

    End Sub

    '受信したイベントをMainに送る
    Public Event Push確定ボタン(ByVal sender As Object, ByVal e As 結果決定EventArgs)




    Private Sub PB_印刷_Click(sender As Object, e As EventArgs) Handles PB_印刷.Click


        Dim 選択行 As Integer = -1

        'どのセルが選択されているか？
        For Each c As DataGridViewCell In Me.DGV_採点進行管理.SelectedCells
            選択行 = c.RowIndex
            Exit For
        Next c

        If 選択行 = -1 Then
            MsgBox("行を選択してください。")
            Exit Sub
        End If

        Dim 区分番号 As String = ""
        Dim ラウンド番号 As String = ""

        区分番号 = Me.DGV_採点進行管理.Rows(選択行).Cells("区分番号").Value
        ラウンド番号 = Me.DGV_採点進行管理.Rows(選択行).Cells("ラウンド番号").Value


        '印刷画面の表示
        'BJSの時は専用の画面を開く

        Dim 採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        If 採点方式.Substring(0, 3) = "BJS" Or 採点方式.Substring(0, 4) = "BJPR" Then
            Dim F302 As F302_BRHTML作成
            F302 = New F302_BRHTML作成

            F302.設定(区分番号, ラウンド番号)
            F302.Show()


        Else
            Dim F300 As F300_印刷画面
            F300 = New F300_印刷画面

            F300.設定(区分番号, ラウンド番号)
            F300.Show()


        End If



    End Sub

    Private Sub PB_更新_Click(sender As Object, e As EventArgs) Handles PB_更新.Click
        競技番号確認画面()
    End Sub

    Private Sub PB_ステータス戻し_Click(sender As Object, e As EventArgs) Handles PB_ステータス戻し.Click

        '「採点済み」のステータスを「ヒート表作成済み」に戻す

        Dim 選択行 As Integer = -1

        'どのセルが選択されているか？
        For Each c As DataGridViewCell In Me.DGV_採点進行管理.SelectedCells
            選択行 = c.RowIndex
            Exit For
        Next c

        If 選択行 = -1 Then
            MsgBox("行を選択してください。")
            Exit Sub
        End If

        Dim 区分番号 As String = ""
        Dim ラウンド番号 As String = ""

        区分番号 = Me.DGV_採点進行管理.Rows(選択行).Cells("区分番号").Value
        ラウンド番号 = Me.DGV_採点進行管理.Rows(選択行).Cells("ラウンド番号").Value

        'ステータスが「採点済み」だったら「ヒート表作成済み」に戻す

        Dim 採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

        If 採点進行C.ステータス = "採点済み" Then

            If MsgBox("区分番号「" & 区分番号 & "」ラウンド「" & マスタデータ.Get_ラウンド名(ラウンド番号) & "」のステータスを「ヒート表作成済み」に戻しますがいいですか？", vbYesNo) = vbYes Then

                採点進行C.ステータス = "ヒート表作成済み"
                マスタデータ.T_採点進行管理.登録(採点進行C)

            End If


        End If


    End Sub
End Class