Public Class F501_得点詳細_C

    '得点詳細を表示する
    '区分番号、ラウンド番号

    Private 採点結果 As 採点結果_C

    Private 採点結果_V2 As 採点結果_V2


    Public 区分番号, ラウンド番号 As String

    Private DGVリスト() As DataGridView
    Private タブページ(10) As TabPage

    Private PCS数 As Integer
    Private PCSリスト() As String

    Private LOG As LOG_C
    Private LOGLEVEL As Integer = 1



    Private Sub F501_得点詳細_C_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)


        If LOGLEVEL >= 4 Then
            LOG = New LOG_C
            LOG.CreateFile()
            LOG.SetLogLevel(4)

            '  Me.Invoke(LogDelegate, New Object() {})
        End If

    End Sub


    'デリゲート宣言
    Delegate Sub AddLogDelegate(ByVal srt As String, ByVal LOGLEVEL As Integer)
    'デリゲート宣言をデータ型とした変数を作成
    Private LogDelegate As New AddLogDelegate(AddressOf WriteLog)


    Private Sub WriteLog(str As String, LOG_LEVEL As Integer)

        Try
            LOG.LogAdd(str, LOG_LEVEL)

        Catch ex As Exception

        End Try

    End Sub


    Public Sub 設定(区分番号_ As String, ラウンド番号_ As String, 採点結果_ As 採点結果_C)

        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_
        採点結果 = 採点結果_

        Dim 採点方式 As String = 採点結果.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)


        Me.LB_区分名.Text = 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & " " & 採点結果.マスタデータ.Get_ラウンド名(ラウンド番号) & " 採点方式【" & 採点方式 & "】"



        Dim 種目記号リスト() = Nothing
        Dim 種目数 = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        ReDim DGVリスト(種目数)

        Dim temp As Control()
        For i = 1 To 10
            'TabPages を配列に格納
            temp = Me.Controls.Find("TabPage" & i, True)
            タブページ(i) = CType(temp(0), TabPage)


            'DGVを配列に格納
            If i <= 種目数 Then
                Dim s As String = String.Format("{0:D2}", i)
                temp = Me.Controls.Find("DGV_" & s, True)
                DGVリスト(i) = CType(temp(0), DataGridView)
            End If
        Next i


        For i = 10 To 1 Step -1
            If i > 種目数 Then
                Me.TabControl_詳細.TabPages.Remove(タブページ(i))
            Else
                Me.TabControl_詳細.TabPages.Insert(i - 1, タブページ(i))
            End If
        Next i


        Me.TabControl_詳細.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Bold)


        'キャリブレーション設定
        Me.TB_CaliMin.Text = Format(採点結果.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).CaliMin, "0.00")
        Me.TB_CaliMax.Text = Format(採点結果.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).CaliMax, "0.00")



        'Me.CB_自動更新.Checked = True
        '自動更新開始()

        Me.CB_自動更新.Checked = False
        自動更新終了()



        更新()


    End Sub

    Public Sub 設定_V2(区分番号_ As String, ラウンド番号_ As String, 採点結果_V2_ As 採点結果_V2)

        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_
        採点結果_V2 = 採点結果_V2_

        Dim 採点方式 As String = 採点結果_V2.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)


        Me.LB_区分名.Text = 採点結果_V2.マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & " " & 採点結果_V2.マスタデータ.Get_ラウンド名(ラウンド番号) & " 採点方式【" & 採点方式 & "】"



        Dim 種目記号リスト() = Nothing
        Dim 種目数 = 採点結果_V2.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        ReDim DGVリスト(種目数)

        Dim temp As Control()
        For i = 1 To 10
            'TabPages を配列に格納
            temp = Me.Controls.Find("TabPage" & i, True)
            タブページ(i) = CType(temp(0), TabPage)


            'DGVを配列に格納
            If i <= 種目数 Then
                Dim s As String = String.Format("{0:D2}", i)
                temp = Me.Controls.Find("DGV_" & s, True)
                DGVリスト(i) = CType(temp(0), DataGridView)
            End If
        Next i


        For i = 10 To 1 Step -1
            If i > 種目数 Then
                Me.TabControl_詳細.TabPages.Remove(タブページ(i))
            Else
                Me.TabControl_詳細.TabPages.Insert(i - 1, タブページ(i))
            End If
        Next i


        Me.TabControl_詳細.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Bold)


        'キャリブレーション設定
        Me.TB_CaliMin.Text = Format(採点結果_V2.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).CaliMin, "0.00")
        Me.TB_CaliMax.Text = Format(採点結果_V2.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).CaliMax, "0.00")



        'Me.CB_自動更新.Checked = True
        '自動更新開始()

        Me.CB_自動更新.Checked = False
        自動更新終了()



        更新_V2()


    End Sub



    Public Sub 更新()

        If 採点結果 Is Nothing Then
            更新_V2()
            Exit Sub
        End If


        If PCS数 > 0 Then
            If LOGLEVEL >= 4 Then
                If Me.InvokeRequired Then
                    Me.Invoke(LogDelegate, New Object() {"F501更新開始", 4})
                Else
                    WriteLog("F501更新開始", 4)
                End If
            End If

        End If

        Dim 種目記号リスト() = Nothing
        Dim 種目数 = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        Dim 採点方式 As String = 採点結果.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        If Strings.Left(採点方式, 3) = "AJS" Then

            Me.PB_欠場.Visible = False

            For i = 1 To 種目数
                種目別データ更新_AJS30J(i)
            Next i

            総合結果更新_AJS30J()

        ElseIf Strings.Left(採点方式, 4) = "BJS2" Or Strings.Left(採点方式, 4) = "BJS3" Then


            Me.PB_欠場.Visible = True


            For i = 1 To 種目数
                種目別データ更新_BJS10J(i)
            Next i

            総合結果更新_BJS30J()

            Me.Label4.Visible = False
            Me.Label5.Visible = False
            Me.TB_CaliMin.Visible = False
            Me.TB_CaliMax.Visible = False
            Me.PB_Cali設定.Visible = False


        ElseIf Strings.Left(採点方式, 3) = "BJS" Then

            Me.PB_欠場.Visible = True


            For i = 1 To 種目数
                種目別データ更新_BJS10J(i)
            Next i

            総合結果更新_BJS30J()


        ElseIf Strings.Left(採点方式, 4) = "BJPR" Then

            Me.PB_欠場.Visible = True

            For i = 1 To 種目数
                種目別データ更新_チェック法(i)
            Next i

            総合結果更新_チェック法()

            Me.Label4.Visible = False
            Me.Label5.Visible = False
            Me.TB_CaliMin.Visible = False
            Me.TB_CaliMax.Visible = False
            Me.PB_Cali設定.Visible = False


        ElseIf 採点方式 = "チェック法" Then

            Me.PB_欠場.Visible = True

            For i = 1 To 種目数
                種目別データ更新_チェック法(i)
            Next i

            総合結果更新_チェック法()

            Me.Label4.Visible = False
            Me.Label5.Visible = False
            Me.TB_CaliMin.Visible = False
            Me.TB_CaliMax.Visible = False
            Me.PB_Cali設定.Visible = False

        ElseIf 採点方式 = "順位法" Then

            Me.PB_欠場.Visible = True

            For i = 1 To 種目数
                種目別データ更新_順位法(i)
            Next i

            総合結果更新_順位法()

            Me.Label4.Visible = False
            Me.Label5.Visible = False
            Me.TB_CaliMin.Visible = False
            Me.TB_CaliMax.Visible = False
            Me.PB_Cali設定.Visible = False


        Else

        End If



        Dim SC_J_詳細結果 As SC_J_詳細結果
        SC_J_詳細結果 = New SC_J_詳細結果(採点結果.マスタデータ.Z_システム設定.Comp_filepath)
        SC_J_詳細結果.データ設定(採点結果)
        SC_J_詳細結果.JSON書き出し()



    End Sub

    Public Sub 更新_V2()

        If PCS数 > 0 Then
            If LOGLEVEL >= 4 Then
                If Me.InvokeRequired Then
                    Me.Invoke(LogDelegate, New Object() {"F501更新開始", 4})
                Else
                    WriteLog("F501更新開始", 4)
                End If
            End If

        End If

        Dim 種目記号リスト() = Nothing
        Dim 種目数 = 採点結果_V2.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        Dim 採点方式 As String = 採点結果_V2.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        If Strings.Left(採点方式, 3) = "PDJ" Then

            Me.PB_欠場.Visible = False

            For i = 1 To 種目数
                '種目別データ更新_AJS30J(i)
                種目別データ更新_PDJ10J(i, False, Nothing)
            Next i

            総合結果更新_PDJ10J()

        ElseIf Strings.Left(採点方式, 3) = "VAL" Then

            Me.PB_欠場.Visible = False

            For i = 1 To 種目数
                種目別データ更新_PDJ10J(i, False, Nothing)
            Next i

            総合結果更新_PDJ10J()

        Else

        End If



        'Dim SC_J_詳細結果 As SC_J_詳細結果
        'SC_J_詳細結果 = New SC_J_詳細結果(採点結果.マスタデータ.Z_システム設定.Comp_filepath)
        'SC_J_詳細結果.データ設定(採点結果_V2)
        'SC_J_詳細結果.JSON書き出し()



    End Sub


    Public Sub リアル更新(ジャッジ結果_J As S_採点結果_V2_J)

        If CB_リアル更新.Checked = True Then


            Dim 種目番号 = ジャッジ結果_J.種目番号

            Dim 種目記号リスト() = Nothing
            Dim 種目数 = 採点結果_V2.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

            Dim 採点方式 As String = 採点結果_V2.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

            If Strings.Left(採点方式, 3) = "PDJ" Or Strings.Left(採点方式, 3) = "VAL" Then

                Me.PB_欠場.Visible = False

                '種目別データ更新_PDJ10J(種目番号, True, ジャッジ結果_J)
                種目別結果リアル更新(ジャッジ結果_J)

                '総合結果更新_PDJ10J()

            Else

            End If

        End If



    End Sub

    Private Sub 総合結果更新_AJS30J()

        '総合結果を更新
        採点結果.総合結果更新()

        'データクリア
        Me.DGV_総合.DataSource = Nothing
        Me.DGV_総合.Rows.Clear()


        '===列幅の自動調整
        Me.DGV_総合.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_総合.AllowUserToResizeColumns = True

        'DGVのデフォルトフォント変更
        Me.DGV_総合.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)


        '列タイトル設定
        ' １列目 NO
        ' ２列目 リーダ名
        ' ３列目 パートナー名
        ' ４列目 所属
        ' ５列目 ヒート番号
        ' ６列目 合計点
        ' ７列目 順位
        ' ８列目～１ｘ列目　ジャッジ人数＋減点ジャッジ
        '　PCS ４つ


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("LName", GetType(String)))
        tbl.Columns.Add(New DataColumn("PName", GetType(String)))
        tbl.Columns.Add(New DataColumn("Coutry", GetType(String)))
        tbl.Columns.Add(New DataColumn("Total", GetType(Double)))
        tbl.Columns.Add(New DataColumn("Place", GetType(Integer)))

        '種目列の追加
        Dim 種目記号リスト() = Nothing
        Dim 種目数 = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        For s = 1 To 種目数
            tbl.Columns.Add(New DataColumn(種目記号リスト(s), GetType(String)))
        Next s

        'データ行の追加
        Dim idx As Integer

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        For s = 1 To 採点結果.出場選手数

            '順位順に表示
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果.出場選手数
                If 採点結果.総合順位番号(t) = s Then
                    選手番号 = t
                    t = 採点結果.出場選手数
                End If
            Next t




            Dim 背番号 As String = 採点結果.背番号(選手番号)

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            tbl.Rows.Add()
            idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item(0) = 背番号 '背番号
            tbl.Rows(idx).Item(1) = 選手.リーダー表記名 'リーダ名
            tbl.Rows(idx).Item(2) = 選手.パートナ表記名 'パートナー名
            tbl.Rows(idx).Item(3) = 選手.カップル所属名 '所属
            tbl.Rows(idx).Item(4) = 採点結果.総合得点(選手番号).ToString("0.000") '合計点
            tbl.Rows(idx).Item(5) = 採点結果.総合順位表記(選手番号).ToString() '順位

            '種目毎の得点
            For d = 1 To 種目数
                tbl.Rows(idx).Item(5 + d) = 採点結果.種目(d).選手(選手番号).種目得点.ToString("0.000") '得点
            Next d

        Next s


        '// DataGridViewにデータセットを設定
        Me.DGV_総合.DataSource = tbl


        '列幅の設定

        '===列幅の自動調整
        Me.DGV_総合.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_総合.AllowUserToResizeColumns = True


        '合計点は右寄せ
        Me.DGV_総合.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '合計点は小数点以下3桁表示
        Me.DGV_総合.Columns("Total").DefaultCellStyle.Format = "0.000"


        '順位は真ん中寄席
        Me.DGV_総合.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'あとは右寄せ
        For i = 6 To 5 + 種目数
            Me.DGV_総合.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next i



        'SCファイルの作成
        SCファイル作成_総合()


    End Sub

    Private Sub 総合結果更新_BJS30J()

        '総合結果を更新
        採点結果.総合結果更新()

        'データクリア
        Me.DGV_総合.DataSource = Nothing
        Me.DGV_総合.Rows.Clear()


        '===列幅の自動調整
        Me.DGV_総合.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_総合.AllowUserToResizeColumns = True

        'DGVのデフォルトフォント変更
        Me.DGV_総合.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)


        '列タイトル設定
        ' １列目 NO
        ' ２列目 リーダ名
        ' ３列目 パートナー名
        ' ４列目 所属」
        ' ５列目 合計点
        ' ６列目 WINS数
        ' ７列目 順位
        ' ８列目～１ｘ列目　種目別点数
        '　PCS ４つ


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("LName", GetType(String)))
        tbl.Columns.Add(New DataColumn("PName", GetType(String)))
        tbl.Columns.Add(New DataColumn("Coutry", GetType(String)))
        tbl.Columns.Add(New DataColumn("Total", GetType(Double)))
        tbl.Columns.Add(New DataColumn("Win#", GetType(Double)))
        tbl.Columns.Add(New DataColumn("Place", GetType(Integer)))

        '種目列の追加
        Dim 種目記号リスト() = Nothing
        Dim 種目数 = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        For s = 1 To 種目数
            tbl.Columns.Add(New DataColumn(種目記号リスト(s), GetType(String)))
        Next s

        'データ行の追加
        Dim idx As Integer

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        For s = 1 To 採点結果.出場選手数

            '順位順に表示
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果.出場選手数
                If 採点結果.総合順位番号(t) = s Then
                    選手番号 = t
                    t = 採点結果.出場選手数
                End If
            Next t




            Dim 背番号 As String = 採点結果.背番号(選手番号)

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            tbl.Rows.Add()
            idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item(0) = 背番号 '背番号
            tbl.Rows(idx).Item(1) = 選手.リーダー表記名 'リーダ名
            tbl.Rows(idx).Item(2) = 選手.パートナ表記名 'パートナー名
            tbl.Rows(idx).Item(3) = 選手.カップル所属名 '所属
            tbl.Rows(idx).Item(4) = 採点結果.総合得点(選手番号).ToString("0.000") '合計点
            tbl.Rows(idx).Item(5) = 採点結果.WIN数(選手番号)     'WIN数
            tbl.Rows(idx).Item(6) = 採点結果.総合順位表記(選手番号).ToString() '順位

            '種目毎の得点
            For d = 1 To 種目数
                tbl.Rows(idx).Item(6 + d) = 採点結果.種目(d).選手(選手番号).種目得点.ToString("0.000") '得点
            Next d

        Next s


        '// DataGridViewにデータセットを設定
        Me.DGV_総合.DataSource = tbl


        '列幅の設定

        '===列幅の自動調整
        Me.DGV_総合.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_総合.AllowUserToResizeColumns = True


        '合計点は右寄せ
        Me.DGV_総合.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '合計点は小数点以下3桁表示
        Me.DGV_総合.Columns("Total").DefaultCellStyle.Format = "0.000"


        'WINS数は真ん中寄席
        Me.DGV_総合.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '順位は真ん中寄席
        Me.DGV_総合.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'あとは右寄せ
        For i = 7 To 6 + 種目数
            Me.DGV_総合.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next i



        'SCファイルの作成
        SCファイル作成_総合()


    End Sub


    Private Sub 総合結果更新_チェック法()

        '総合結果を更新
        採点結果.総合結果更新()

        'データクリア
        Me.DGV_総合.DataSource = Nothing
        Me.DGV_総合.Rows.Clear()


        '===列幅の自動調整
        Me.DGV_総合.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_総合.AllowUserToResizeColumns = True

        'DGVのデフォルトフォント変更
        Me.DGV_総合.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)


        '列タイトル設定
        ' １列目 NO
        ' ２列目 リーダ名
        ' ３列目 パートナー名
        ' ４列目 所属
        ' ５列目 ヒート番号
        ' ６列目 合計点
        ' ７列目 順位
        ' ８列目～１ｘ列目　ジャッジ人数＋減点ジャッジ
        '　PCS ４つ


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("LName", GetType(String)))
        tbl.Columns.Add(New DataColumn("PName", GetType(String)))
        tbl.Columns.Add(New DataColumn("Coutry", GetType(String)))
        tbl.Columns.Add(New DataColumn("Total", GetType(Double)))
        tbl.Columns.Add(New DataColumn("Place", GetType(Integer)))

        '種目列の追加
        Dim 種目記号リスト() = Nothing
        Dim 種目数 = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        For s = 1 To 種目数
            tbl.Columns.Add(New DataColumn(種目記号リスト(s), GetType(String)))
        Next s

        'データ行の追加
        Dim idx As Integer

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        For s = 1 To 採点結果.出場選手数

            '順位順に表示
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果.出場選手数
                If 採点結果.総合順位番号(t) = s Then
                    選手番号 = t
                    t = 採点結果.出場選手数
                End If
            Next t




            Dim 背番号 As String = 採点結果.背番号(選手番号)

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            tbl.Rows.Add()
            idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item(0) = 背番号 '背番号
            tbl.Rows(idx).Item(1) = 選手.リーダー表記名 'リーダ名
            tbl.Rows(idx).Item(2) = 選手.パートナ表記名 'パートナー名
            tbl.Rows(idx).Item(3) = 選手.カップル所属名 '所属
            tbl.Rows(idx).Item(4) = 採点結果.総合得点(選手番号).ToString '合計点
            tbl.Rows(idx).Item(5) = 採点結果.総合順位表記(選手番号).ToString() '順位

            '種目毎の得点
            For d = 1 To 種目数
                tbl.Rows(idx).Item(5 + d) = 採点結果.種目(d).選手(選手番号).種目得点.ToString '得点
            Next d

        Next s


        '// DataGridViewにデータセットを設定
        Me.DGV_総合.DataSource = tbl


        '列幅の設定

        '===列幅の自動調整
        Me.DGV_総合.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_総合.AllowUserToResizeColumns = True


        '合計点は右寄せ
        Me.DGV_総合.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        If Strings.Left(採点結果.採点方式, 4) = "BJPR" Then
            '合計点は表示
            Me.DGV_総合.Columns("Total").DefaultCellStyle.Format = "0.000"
        Else
            '合計点は表示
            Me.DGV_総合.Columns("Total").DefaultCellStyle.Format = "0"
        End If


        '順位は真ん中寄席
        Me.DGV_総合.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'あとは右寄せ
        For i = 6 To 5 + 種目数
            Me.DGV_総合.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            If Strings.Left(採点結果.採点方式, 4) = "BJPR" Then
                Me.DGV_総合.Columns(i).DefaultCellStyle.Format = "0.000"
            End If


        Next i



        'SCファイルの作成
        SCファイル作成_総合()


    End Sub


    Private Sub 総合結果更新_順位法()

        '総合結果を更新
        採点結果.総合結果更新()

        'データクリア
        Me.DGV_総合.DataSource = Nothing
        Me.DGV_総合.Rows.Clear()


        '===列幅の自動調整
        Me.DGV_総合.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_総合.AllowUserToResizeColumns = True

        'DGVのデフォルトフォント変更
        Me.DGV_総合.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)


        '列タイトル設定
        ' １列目 NO
        ' ２列目 リーダ名
        ' ３列目 パートナー名
        ' ４列目 所属
        ' ５列目 ヒート番号
        ' ６列目 合計点
        ' ７列目 順位
        ' ８列目～１ｘ列目　ジャッジ人数＋減点ジャッジ
        '　PCS ４つ


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("LName", GetType(String)))
        tbl.Columns.Add(New DataColumn("PName", GetType(String)))
        tbl.Columns.Add(New DataColumn("Coutry", GetType(String)))
        tbl.Columns.Add(New DataColumn("Total", GetType(Decimal)))
        tbl.Columns.Add(New DataColumn("Place", GetType(Integer)))

        '種目列の追加
        Dim 種目記号リスト() = Nothing
        Dim 種目数 = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        For s = 1 To 種目数
            tbl.Columns.Add(New DataColumn(種目記号リスト(s), GetType(String)))
        Next s

        tbl.Columns.Add(New DataColumn("決定根拠", GetType(String)))
        tbl.Columns.Add(New DataColumn("多数決", GetType(String)))
        tbl.Columns.Add(New DataColumn("上位加算", GetType(String)))
        tbl.Columns.Add(New DataColumn("再スケ_過半数", GetType(String)))
        tbl.Columns.Add(New DataColumn("再スケ_多数決", GetType(String)))
        tbl.Columns.Add(New DataColumn("再スケ_上位加算", GetType(String)))
        tbl.Columns.Add(New DataColumn("再スケ_下位比較", GetType(String)))






        'データ行の追加
        Dim idx As Integer

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        For s = 1 To 採点結果.出場選手数

            '順位順に表示
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果.出場選手数
                If 採点結果.総合順位番号(t) = s Then
                    選手番号 = t
                    t = 採点結果.出場選手数
                End If
            Next t




            Dim 背番号 As String = 採点結果.背番号(選手番号)

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            tbl.Rows.Add()
            idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item(0) = 背番号 '背番号
            tbl.Rows(idx).Item(1) = 選手.リーダー表記名 'リーダ名
            tbl.Rows(idx).Item(2) = 選手.パートナ表記名 'パートナー名
            tbl.Rows(idx).Item(3) = 選手.カップル所属名 '所属
            tbl.Rows(idx).Item(4) = 採点結果.スケーティング結果_総合選手(選手番号).規程9_合計点     '合計点
            tbl.Rows(idx).Item(5) = 採点結果.総合順位表記(選手番号).ToString() '順位

            '種目毎の得点
            For d = 1 To 種目数
                tbl.Rows(idx).Item(5 + d) = 採点結果.種目(d).選手(選手番号).スケーティング結果_選手.順位点数.ToString '得点　順位点数
            Next d

            tbl.Rows(idx).Item(6 + 種目数) = 採点結果.スケーティング結果_総合選手(選手番号).決定根拠
            tbl.Rows(idx).Item(7 + 種目数) = 採点結果.スケーティング結果_総合選手(選手番号).規程10a_多数決
            tbl.Rows(idx).Item(8 + 種目数) = 採点結果.スケーティング結果_総合選手(選手番号).規程10b_上位加算
            tbl.Rows(idx).Item(9 + 種目数) = 採点結果.スケーティング結果_総合選手(選手番号).規程5_過半数順位
            tbl.Rows(idx).Item(10 + 種目数) = 採点結果.スケーティング結果_総合選手(選手番号).規程6_多数決
            tbl.Rows(idx).Item(11 + 種目数) = 採点結果.スケーティング結果_総合選手(選手番号).規程7a_上位加算
            tbl.Rows(idx).Item(12 + 種目数) = 採点結果.スケーティング結果_総合選手(選手番号).規程7b_下位比較



        Next s


        '// DataGridViewにデータセットを設定
        Me.DGV_総合.DataSource = tbl


        '列幅の設定

        '===列幅の自動調整
        Me.DGV_総合.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_総合.AllowUserToResizeColumns = True


        '合計点は右寄せ
        Me.DGV_総合.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '合計点は表示
        'Me.DGV_総合.Columns("Total").DefaultCellStyle.Format = "0"

        '順位は太字
        Me.DGV_総合.Columns("Place").DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Bold)

        '順位は真ん中寄席
        Me.DGV_総合.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'あとは右寄せ
        For i = 6 To 5 + 種目数
            Me.DGV_総合.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next i



        'SCファイルの作成
        SCファイル作成_総合()


    End Sub

    Private Sub 総合結果更新_PDJ10J()

        '総合結果を更新
        採点結果_V2.総合結果更新()

        'データクリア
        Me.DGV_総合.DataSource = Nothing
        Me.DGV_総合.Rows.Clear()


        '===列幅の自動調整
        Me.DGV_総合.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_総合.AllowUserToResizeColumns = True

        'DGVのデフォルトフォント変更
        Me.DGV_総合.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)


        '列タイトル設定
        ' １列目 NO
        ' ２列目 リーダ名
        ' ３列目 パートナー名
        ' ４列目 所属
        ' ５列目 ヒート番号
        ' ６列目 合計点
        ' ７列目 順位
        ' ８列目～１ｘ列目　ジャッジ人数＋減点ジャッジ
        '　PCS ４つ


        '// データテーブルの作成
        Dim tbl As New DataTable

        'tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("No", GetType(String)))
        tbl.Columns.Add(New DataColumn("LName", GetType(String)))
        tbl.Columns.Add(New DataColumn("PName", GetType(String)))
        tbl.Columns.Add(New DataColumn("Coutry", GetType(String)))
        tbl.Columns.Add(New DataColumn("Total", GetType(Double)))
        tbl.Columns.Add(New DataColumn("Place", GetType(Integer)))

        '種目列の追加
        Dim 種目記号リスト() = Nothing
        Dim 種目数 = 採点結果_V2.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        For s = 1 To 種目数
            tbl.Columns.Add(New DataColumn(種目記号リスト(s), GetType(String)))
        Next s

        'データ行の追加
        Dim idx As Integer

        Dim 区分 As B_区分 = 採点結果_V2.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        For s = 1 To 採点結果_V2.出場選手数

            '順位順に表示
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果_V2.出場選手数
                If 採点結果_V2.総合順位番号(t) = s Then
                    選手番号 = t
                    t = 採点結果_V2.出場選手数
                End If
            Next t




            Dim 背番号 As String = 採点結果_V2.背番号(選手番号)

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果_V2.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            tbl.Rows.Add()
            idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item(0) = 背番号 '背番号
            tbl.Rows(idx).Item(1) = 選手.リーダー表記名 'リーダ名
            tbl.Rows(idx).Item(2) = 選手.パートナ表記名 'パートナー名
            tbl.Rows(idx).Item(3) = 選手.カップル所属名 '所属
            tbl.Rows(idx).Item(4) = 採点結果_V2.総合得点(選手番号).ToString("0.000") '合計点
            tbl.Rows(idx).Item(5) = 採点結果_V2.総合順位表記(選手番号).ToString() '順位

            '種目毎の得点
            For d = 1 To 種目数
                tbl.Rows(idx).Item(5 + d) = 採点結果_V2.種目(d).選手結果(選手番号).種目得点.ToString("0.000") '得点
            Next d

        Next s


        '// DataGridViewにデータセットを設定
        Me.DGV_総合.DataSource = tbl


        '列幅の設定

        '===列幅の自動調整
        Me.DGV_総合.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_総合.AllowUserToResizeColumns = True


        '合計点は右寄せ
        Me.DGV_総合.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '合計点は小数点以下3桁表示
        Me.DGV_総合.Columns("Total").DefaultCellStyle.Format = "0.000"


        '順位は真ん中寄席
        Me.DGV_総合.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'あとは右寄せ
        For i = 6 To 5 + 種目数
            Me.DGV_総合.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next i



        'SCファイルの作成
        SCファイル作成_総合_V2()


    End Sub



    Private Sub 種目別データ更新_AJS30J(ByVal 種目番号 As Integer)


        'データクリア
        DGVリスト(種目番号).DataSource = Nothing
        DGVリスト(種目番号).Rows.Clear()

        'DGVのデフォルトフォント変更
        DGVリスト(種目番号).DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)


        'イベントハンドらの登録
        AddHandler DGVリスト(種目番号).CellFormatting, AddressOf Me.DGV_種目_CellFormatting


        '採点結果の更新
        採点結果.種目(種目番号).更新()

        '種目数の確定

        'ジャッジ人数の確定（ジャッジ＋減点ジャッジ）
        Dim ジャッジ人数 As Integer
        ジャッジ人数 = 採点結果.種目(種目番号).審判員数


        '１～５種目
        'タブタイトル設定（種目名）
        Dim タブタイトル

        タブタイトル = 採点結果.種目(種目番号).種目記号

        Dim 種目 As D_種目 = 採点結果.種目(種目番号).マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目番号)
        If 種目.SG種別 = "S" Then
            タブタイトル = タブタイトル & "(Solo)"
        ElseIf 種目.SG種別 = "G" Then
            タブタイトル = タブタイトル & "(Group)"
        ElseIf 種目.SG種別 = "D" Then
            タブタイトル = タブタイトル & "(Duel)"
        End If


        タブページ(種目番号).Name = タブタイトル
        タブページ(種目番号).Text = タブタイトル

        'DGVを先に作らないと色が変わらないので、これを追加　--- うまくいかない
        'タブページ(種目番号).Visible = True

        '列タイトル設定
        ' １列目 NO
        ' ２列目 リーダ名
        '削除 ３列目 パートナー名
        '削除 ４列目 所属
        ' ３列目 ヒート番号
        ' ４列目 合計点
        ' ５列目 順位
        ' ６列目～１ｘ列目　ジャッジ人数＋減点ジャッジ
        '　PCS ４つ


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("LName", GetType(String)))
        'tbl.Columns.Add(New DataColumn("PName", GetType(String)))
        'tbl.Columns.Add(New DataColumn("Coutry", GetType(String)))
        tbl.Columns.Add(New DataColumn("Heat", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("Total", GetType(Double)))
        tbl.Columns.Add(New DataColumn("Place", GetType(Integer)))


        'ジャッジ列の追加
        Dim j
        For j = 1 To ジャッジ人数
            If 採点結果.種目(種目番号).選手(1).審判(j).ジャッジRole <> "R" Then

                'ジャッジ記号
                tbl.Columns.Add(New DataColumn(採点結果.種目(種目番号).選手(1).審判(j).ジャッジ記号, GetType(Double)))
            End If
        Next j

        '減点ジャッジ列の追加
        tbl.Columns.Add(New DataColumn("Ref", GetType(Double)))

        'PCS列の追加
        PCS数 = 採点結果.種目(種目番号).マスタデータ.J_新審判設定.GetPCS数
        ReDim PCSリスト(PCS数)
        For pcs番号 = 1 To PCS数
            Dim PCS名 = 採点結果.種目(種目番号).マスタデータ.J_新審判設定.PCS設定(pcs番号).PCS項目名
            tbl.Columns.Add(New DataColumn(PCS名, GetType(Double)))

            PCSリスト(pcs番号) = PCS名

        Next pcs番号



        '行データ表示
        Dim i, 背番号, リーダ名, パートナー名, 所属, ヒート番号, 合計点, 順位
        Dim idx

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        '順位順に表示
        For s = 1 To 採点結果.種目(種目番号).選手数

            '順位が s の選手を探す
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果.種目(種目番号).選手数
                If 採点結果.種目(種目番号).選手(t).種目順位番号 = s Then
                    選手番号 = t
                    t = 採点結果.種目(種目番号).選手数
                End If
            Next t


            背番号 = 採点結果.種目(種目番号).選手(選手番号).背番号

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            リーダ名 = 選手.リーダー表記名
            パートナー名 = 選手.パートナ表記名
            所属 = 選手.カップル所属名
            ヒート番号 = 採点結果.マスタデータ.E_ヒート表マスタ.Get_ヒート番号(種目番号, 背番号)
            合計点 = Format(CDbl(採点結果.種目(種目番号).選手(選手番号).種目得点), "0.000")
            順位 = 採点結果.種目(種目番号).選手(選手番号).種目順位表記

            tbl.Rows.Add()
            idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item(0) = 背番号
            tbl.Rows(idx).Item(1) = リーダ名

            'tbl.Rows(idx).Item(2) = パートナー名
            'tbl.Rows(idx).Item(3) = 所属
            tbl.Rows(idx).Item(2) = ヒート番号
            tbl.Rows(idx).Item(3) = 合計点
            tbl.Rows(idx).Item(4) = 順位


            'Me.DGV_01.Rows.Add(背番号, リーダ名, パートナー名, 所属, ヒート番号, 合計点, 順位)
            'ジャッジごとのPCS詳細

            Dim jj As Integer = 1
            For j = 1 To 採点結果.種目(種目番号).審判員数

                If 採点結果.種目(種目番号).選手(選手番号).審判(j).ジャッジRole <> "R" Then

                    tbl.Rows(idx).Item(4 + jj) = 採点結果.種目(種目番号).選手(選手番号).審判(j).表示用PCS合計点.ToString("0.00")

                    jj = jj + 1
                End If

            Next j

            If 採点結果.種目(種目番号).レフリー番号 <> 0 Then
                tbl.Rows(idx).Item(4 + 採点結果.種目(種目番号).審判員数woRef + 1) = 採点結果.種目(種目番号).選手(選手番号).審判(採点結果.種目(種目番号).レフリー番号).表示用減点合計点
            Else
                tbl.Rows(idx).Item(4 + 採点結果.種目(種目番号).審判員数woRef + 1) = 0
            End If


            '各PCSの値
            Dim PCS
            For PCS = 1 To PCS数
                tbl.Rows(idx).Item(4 + 採点結果.種目(種目番号).審判員数woRef + 1 + PCS) = 採点結果.種目(種目番号).選手(選手番号).種目各PCS得点(PCS).ToString("0.000")
            Next PCS


        Next s




        '// DataGridViewにデータセットを設定
        DGVリスト(種目番号).DataSource = tbl


        '列幅の設定

        '===列幅の自動調整
        Me.DGVリスト(種目番号).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGVリスト(種目番号).AllowUserToResizeColumns = True



        For i = 0 To 4 + 採点結果.種目(種目番号).審判員数woRef + 1 + PCS数
            DGVリスト(種目番号).Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Next i

        'ヒート４と順位６は真ん中寄せ 合計点（５）は右寄せ
        DGVリスト(種目番号).Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGVリスト(種目番号).Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVリスト(種目番号).Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '小数点以下3桁表示
        DGVリスト(種目番号).Columns("Total").DefaultCellStyle.Format = "0.000"

        '　　ジャッジ列
        For j = 1 To ジャッジ人数
            Dim ジャッジ記号 As String = 採点結果.種目(種目番号).選手(1).審判(j).ジャッジ記号
            If 採点結果.種目(種目番号).選手(1).審判(j).ジャッジRole <> "R" Then
                DGVリスト(種目番号).Columns(ジャッジ記号).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DGVリスト(種目番号).Columns(ジャッジ記号).DefaultCellStyle.Format = "0.00"
            End If
        Next j

        '　PCS列
        For p = 1 To PCS数
            DGVリスト(種目番号).Columns(PCSリスト(p)).DefaultCellStyle.Format = "0.000"
        Next p


        'あとは右寄せ
        For i = 7 To 4 + 採点結果.種目(種目番号).審判員数woRef + 1 + PCS数
            DGVリスト(種目番号).Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next i
        'i = tbl.Rows(1).Item("LName").ToString()

        '色の変更
        'SendFlagの確認
        For DGVs = 1 To Me.DGVリスト(種目番号).RowCount
            '背番号
            For 結果s = 1 To 採点結果.種目(種目番号).選手数
                背番号 = 採点結果.種目(種目番号).選手(結果s).背番号

                If DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Value = 背番号 Then

                    If 採点結果.種目(種目番号).選手(結果s).選手SEND済FLAG = True Then                        '
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(1).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(2).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(3).Style.BackColor = Color.Cyan
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(4).Style.BackColor = Color.Cyan
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(5).Style.BackColor = Color.Cyan
                    Else
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(1).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(2).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(3).Style.BackColor = Nothing
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(4).Style.BackColor = Nothing
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(5).Style.BackColor = Nothing

                    End If

                End If

            Next 結果s


        Next DGVs




        '列の非表示設定


        'SCファイルの作成
        SCファイル作成_種目別(種目番号, 種目)


    End Sub







    Private Sub 種目別データ更新_BJS10J(ByVal 種目番号 As Integer)


        'データクリア
        DGVリスト(種目番号).DataSource = Nothing
        DGVリスト(種目番号).Rows.Clear()

        'DGVのデフォルトフォント変更
        DGVリスト(種目番号).DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)


        'イベントハンドらの登録
        AddHandler DGVリスト(種目番号).CellFormatting, AddressOf Me.DGV_種目_CellFormatting


        '採点結果の更新
        採点結果.種目(種目番号).更新()

        '種目数の確定

        'ジャッジ人数の確定（ジャッジ＋減点ジャッジ）
        Dim ジャッジ人数 As Integer
        ジャッジ人数 = 採点結果.種目(種目番号).審判員数


        'ヒート表の読み込み
        採点結果.マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)


        '１～５種目
        'タブタイトル設定（種目名）
        Dim タブタイトル

        タブタイトル = 採点結果.種目(種目番号).種目記号

        Dim 種目 As D_種目 = 採点結果.種目(種目番号).マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目番号)
        If 種目.SG種別 = "S" Then
            タブタイトル = タブタイトル & "(Solo)"
        ElseIf 種目.SG種別 = "G" Then
            タブタイトル = タブタイトル & "(Group)"
        ElseIf 種目.SG種別 = "D" Then
            タブタイトル = タブタイトル & "(Duel)"
        End If


        タブページ(種目番号).Name = タブタイトル
        タブページ(種目番号).Text = タブタイトル

        'DGVを先に作らないと色が変わらないので、これを追加　--- うまくいかない
        'タブページ(種目番号).Visible = True

        '列タイトル設定
        ' １列目 NO
        ' ２列目 リーダ名
        '削除 ３列目 パートナー名
        '削除 ４列目 所属
        ' ３列目 ヒート番号
        ' ４列目 合計点
        ' ５列目 順位
        ' ６列目～１ｘ列目　ジャッジ人数
        '　PCS ４つ
        '　減点５つ

        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("LName", GetType(String)))
        'tbl.Columns.Add(New DataColumn("PName", GetType(String)))
        'tbl.Columns.Add(New DataColumn("Coutry", GetType(String)))
        tbl.Columns.Add(New DataColumn("Heat", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("Total", GetType(Double)))
        tbl.Columns.Add(New DataColumn("Place", GetType(Integer)))


        'ジャッジ列の追加
        Dim j
        For j = 1 To ジャッジ人数
            If 採点結果.種目(種目番号).選手(1).審判(j).ジャッジRole <> "R" Then

                'ジャッジ記号
                tbl.Columns.Add(New DataColumn(採点結果.種目(種目番号).選手(1).審判(j).ジャッジ記号, GetType(Double)))
            End If
        Next j

        '減点ジャッジ列の追加
        'tbl.Columns.Add(New DataColumn("Ref", GetType(Double)))

        'PCS列の追加
        PCS数 = 採点結果.種目(種目番号).マスタデータ.J_新審判設定.GetPCS数
        ReDim PCSリスト(PCS数)
        For pcs番号 = 1 To PCS数
            Dim PCS名 = 採点結果.種目(種目番号).マスタデータ.J_新審判設定.PCS設定(pcs番号).PCS項目名
            tbl.Columns.Add(New DataColumn(PCS名, GetType(Double)))

            PCSリスト(pcs番号) = PCS名

        Next pcs番号

        '減点列の追加
        Dim 減点項目数 As Integer = 採点結果.種目(種目番号).マスタデータ.J_新審判設定.Get減点項目数
        Dim 減点リスト(減点項目数)
        For 減点番号 = 1 To 減点項目数
            Dim 減点項目名 As String = 採点結果.種目(種目番号).マスタデータ.J_新審判設定.減点設定(減点番号).減点項目名
            tbl.Columns.Add(New DataColumn(減点項目名, GetType(Double)))

            減点リスト(減点番号) = 減点項目名
        Next 減点番号




        '行データ表示
        Dim i, 背番号, リーダ名, パートナー名, 所属, ヒート番号, 合計点, 順位
        Dim idx

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        '順位順に表示
        For s = 1 To 採点結果.種目(種目番号).選手数

            '順位が s の選手を探す
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果.種目(種目番号).選手数
                If 採点結果.種目(種目番号).選手(t).種目順位番号 = s Then
                    選手番号 = t
                    t = 採点結果.種目(種目番号).選手数
                End If
            Next t


            背番号 = 採点結果.種目(種目番号).選手(選手番号).背番号

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            リーダ名 = 選手.リーダー表記名
            パートナー名 = 選手.パートナ表記名
            所属 = 選手.カップル所属名
            ヒート番号 = 採点結果.マスタデータ.E_ヒート表マスタ.Get_ヒート番号(種目番号, 背番号)
            合計点 = Format(CDbl(採点結果.種目(種目番号).選手(選手番号).種目得点), "0.000")
            順位 = 採点結果.種目(種目番号).選手(選手番号).種目順位表記

            tbl.Rows.Add()
            idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item(0) = 背番号
            tbl.Rows(idx).Item(1) = リーダ名

            'tbl.Rows(idx).Item(2) = パートナー名
            'tbl.Rows(idx).Item(3) = 所属
            tbl.Rows(idx).Item(2) = ヒート番号
            tbl.Rows(idx).Item(3) = 合計点
            tbl.Rows(idx).Item(4) = 順位


            'Me.DGV_01.Rows.Add(背番号, リーダ名, パートナー名, 所属, ヒート番号, 合計点, 順位)
            'ジャッジごとのPCS詳細

            Dim jj As Integer = 1
            For j = 1 To 採点結果.種目(種目番号).審判員数

                If 採点結果.種目(種目番号).選手(選手番号).審判(j).ジャッジRole <> "R" Then

                    tbl.Rows(idx).Item(4 + jj) = 採点結果.種目(種目番号).選手(選手番号).審判(j).表示用PCS合計点.ToString("0.00")

                    jj = jj + 1
                End If

            Next j

            'If 採点結果.種目(種目番号).レフリー番号 <> 0 Then
            ' tbl.Rows(idx).Item(4 + 採点結果.種目(種目番号).審判員数woRef + 1) = 採点結果.種目(種目番号).選手(選手番号).審判(採点結果.種目(種目番号).レフリー番号).表示用減点合計点
            'Else
            'tbl.Rows(idx).Item(4 + 採点結果.種目(種目番号).審判員数woRef + 1) = 0
            'End If


            '各PCSの値
            Dim PCS
            For PCS = 1 To PCS数
                tbl.Rows(idx).Item(4 + 採点結果.種目(種目番号).審判員数woRef + PCS) = 採点結果.種目(種目番号).選手(選手番号).種目各PCS得点(PCS).ToString("0.000")
            Next PCS

            '各減点の値
            For 減点番号 = 1 To 減点項目数
                tbl.Rows(idx).Item(4 + 採点結果.種目(種目番号).審判員数woRef + PCS数 + 減点番号) = 採点結果.種目(種目番号).選手(選手番号).種目各減点(減点番号)　　'.ToString("0.000")
            Next 減点番号


        Next s




        '// DataGridViewにデータセットを設定
        DGVリスト(種目番号).Rows.Clear()
        DGVリスト(種目番号).DataSource = tbl


        '列幅の設定

        '===列幅の自動調整
        Me.DGVリスト(種目番号).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGVリスト(種目番号).AllowUserToResizeColumns = True



        For i = 0 To 4 + 採点結果.種目(種目番号).審判員数woRef + 1 + PCS数
            DGVリスト(種目番号).Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Next i

        'ヒート４と順位６は真ん中寄せ 合計点（５）は右寄せ
        DGVリスト(種目番号).Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGVリスト(種目番号).Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVリスト(種目番号).Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '小数点以下3桁表示
        DGVリスト(種目番号).Columns("Total").DefaultCellStyle.Format = "0.000"

        '　　ジャッジ列
        For j = 1 To ジャッジ人数
            Dim ジャッジ記号 As String = 採点結果.種目(種目番号).選手(1).審判(j).ジャッジ記号
            If 採点結果.種目(種目番号).選手(1).審判(j).ジャッジRole <> "R" Then
                DGVリスト(種目番号).Columns(ジャッジ記号).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DGVリスト(種目番号).Columns(ジャッジ記号).DefaultCellStyle.Format = "0.00"
            End If
        Next j

        '　PCS列
        For p = 1 To PCS数
            DGVリスト(種目番号).Columns(PCSリスト(p)).DefaultCellStyle.Format = "0.000"
        Next p


        'あとは右寄せ
        For i = 7 To 4 + 採点結果.種目(種目番号).審判員数woRef + 1 + PCS数
            DGVリスト(種目番号).Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next i
        'i = tbl.Rows(1).Item("LName").ToString()

        '色の変更
        'SendFlagの確認
        For DGVs = 1 To Me.DGVリスト(種目番号).RowCount
            '背番号
            For 結果s = 1 To 採点結果.種目(種目番号).選手数
                背番号 = 採点結果.種目(種目番号).選手(結果s).背番号

                If DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Value = 背番号 Then

                    If 採点結果.種目(種目番号).選手(結果s).選手SEND済FLAG = True Then                        '
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(1).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(2).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(3).Style.BackColor = Color.Cyan
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(4).Style.BackColor = Color.Cyan
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(5).Style.BackColor = Color.Cyan
                    Else
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(1).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(2).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(3).Style.BackColor = Nothing
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(4).Style.BackColor = Nothing
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(5).Style.BackColor = Nothing

                    End If

                End If

            Next 結果s


        Next DGVs




        '列の非表示設定


        'SCファイルの作成

        If 採点結果.マスタデータ.D_種目マスタ.Get_SG種別表記名(区分番号, ラウンド番号, 種目番号) = "ソロ" Then
            'ソロの時はSCファイル_ブレイキンは作成しない

        Else
            SCファイル作成_ブレイキン()

        End If

        SCファイル作成_種目別(種目番号, 種目)


    End Sub



    Private Sub 種目別データ更新_チェック法(ByVal 種目番号 As Integer)


        'データクリア
        DGVリスト(種目番号).DataSource = Nothing
        DGVリスト(種目番号).Rows.Clear()

        'DGVのデフォルトフォント変更
        DGVリスト(種目番号).DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)


        'イベントハンドらの登録
        AddHandler DGVリスト(種目番号).CellFormatting, AddressOf Me.DGV_種目_CellFormatting


        '採点結果の更新
        採点結果.種目(種目番号).更新()

        '種目数の確定

        'ジャッジ人数の確定（ジャッジ＋減点ジャッジ）
        Dim ジャッジ人数 As Integer
        ジャッジ人数 = 採点結果.種目(種目番号).審判員数


        '１～５種目
        'タブタイトル設定（種目名）
        Dim タブタイトル

        タブタイトル = 採点結果.種目(種目番号).種目記号

        Dim 種目 As D_種目 = 採点結果.種目(種目番号).マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目番号)
        If 種目.SG種別 = "S" Then
            タブタイトル = タブタイトル & "(Solo)"
        ElseIf 種目.SG種別 = "G" Then
            タブタイトル = タブタイトル & "(Group)"
        ElseIf 種目.SG種別 = "D" Then
            タブタイトル = タブタイトル & "(Duel)"
        End If


        タブページ(種目番号).Name = タブタイトル
        タブページ(種目番号).Text = タブタイトル

        'DGVを先に作らないと色が変わらないので、これを追加　--- うまくいかない
        'タブページ(種目番号).Visible = True

        '列タイトル設定
        ' １列目 NO
        ' ２列目 リーダ名
        '削除 ３列目 パートナー名
        '削除 ４列目 所属
        ' ３列目 ヒート番号
        ' ４列目 合計点
        ' ５列目 順位
        ' ６列目～１ｘ列目　ジャッジ人数＋減点ジャッジ
        '　PCS ４つ


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("LName", GetType(String)))
        'tbl.Columns.Add(New DataColumn("PName", GetType(String)))
        'tbl.Columns.Add(New DataColumn("Coutry", GetType(String)))
        tbl.Columns.Add(New DataColumn("Heat", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("Total", GetType(Double)))
        tbl.Columns.Add(New DataColumn("Place", GetType(Integer)))


        'ジャッジ列の追加
        Dim j
        For j = 1 To ジャッジ人数
            If 採点結果.種目(種目番号).選手(1).審判(j).ジャッジRole <> "R" Then

                'ジャッジ記号
                tbl.Columns.Add(New DataColumn(採点結果.種目(種目番号).選手(1).審判(j).ジャッジ記号, GetType(Decimal)))
            End If
        Next j



        '行データ表示
        Dim i, 背番号, リーダ名, パートナー名, 所属, ヒート番号, 合計点, 順位
        Dim idx

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        Dim 審判点数() As Integer
        ReDim 審判点数(採点結果.種目(種目番号).審判員数)


        '順位順に表示
        For s = 1 To 採点結果.種目(種目番号).選手数

            '順位が s の選手を探す
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果.種目(種目番号).選手数
                If 採点結果.種目(種目番号).選手(t).種目順位番号 = s Then
                    選手番号 = t
                    t = 採点結果.種目(種目番号).選手数
                End If
            Next t


            背番号 = 採点結果.種目(種目番号).選手(選手番号).背番号

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            リーダ名 = 選手.リーダー表記名
            パートナー名 = 選手.パートナ表記名
            所属 = 選手.カップル所属名
            ヒート番号 = 採点結果.マスタデータ.E_ヒート表マスタ.Get_ヒート番号(種目番号, 背番号)

            If Strings.Left(採点結果.採点方式, 4) = "BJPR" Then
                合計点 = Format(CDbl(採点結果.種目(種目番号).選手(選手番号).種目得点), "0.000")

            Else
                合計点 = Format(CDbl(採点結果.種目(種目番号).選手(選手番号).種目得点), "0")
            End If

            順位 = 採点結果.種目(種目番号).選手(選手番号).種目順位表記

            tbl.Rows.Add()
            idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item(0) = 背番号
            tbl.Rows(idx).Item(1) = リーダ名

            'tbl.Rows(idx).Item(2) = パートナー名
            'tbl.Rows(idx).Item(3) = 所属
            tbl.Rows(idx).Item(2) = ヒート番号
            tbl.Rows(idx).Item(3) = 合計点
            tbl.Rows(idx).Item(4) = 順位


            'Me.DGV_01.Rows.Add(背番号, リーダ名, パートナー名, 所属, ヒート番号, 合計点, 順位)
            'ジャッジごとのPCS詳細

            Dim jj As Integer = 1
            For j = 1 To 採点結果.種目(種目番号).審判員数


                If 採点結果.種目(種目番号).選手(選手番号).審判(j).ジャッジRole <> "R" Then

                    tbl.Rows(idx).Item(4 + jj) = 採点結果.種目(種目番号).選手(選手番号).審判(j).素点.ToString

                    審判点数(j) = 審判点数(j) + 採点結果.種目(種目番号).選手(選手番号).審判(j).素点

                    jj = jj + 1
                End If



            Next j


        Next s




        '// DataGridViewにデータセットを設定
        DGVリスト(種目番号).DataSource = tbl


        '列幅の設定

        '===列幅の自動調整
        Me.DGVリスト(種目番号).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGVリスト(種目番号).AllowUserToResizeColumns = True



        For i = 0 To 4 + 採点結果.種目(種目番号).審判員数woRef
            DGVリスト(種目番号).Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Next i

        'ヒート４と順位６は真ん中寄せ 合計点（５）は右寄せ
        DGVリスト(種目番号).Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGVリスト(種目番号).Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVリスト(種目番号).Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '小数点以下3桁表示
        If Strings.Left(採点結果.採点方式, 4) = "BJPR" Then
            DGVリスト(種目番号).Columns("Total").DefaultCellStyle.Format = "0.000"

        Else
            DGVリスト(種目番号).Columns("Total").DefaultCellStyle.Format = "0"

        End If


        Dim UP予定数 As Integer = 採点結果.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).UP予定数
        DGVリスト(種目番号).EnableHeadersVisualStyles = False

        '　　ジャッジ列
        For j = 1 To ジャッジ人数
            Dim ジャッジ記号 As String = 採点結果.種目(種目番号).選手(1).審判(j).ジャッジ記号
            If 採点結果.種目(種目番号).選手(1).審判(j).ジャッジRole <> "R" Then
                DGVリスト(種目番号).Columns(ジャッジ記号).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DGVリスト(種目番号).Columns(ジャッジ記号).DefaultCellStyle.Format = "0.00"
            End If


            If Strings.Left(採点結果.採点方式, 4) = "BJPR" Then
                '審判点数をヘッダーに記載する。
                DGVリスト(種目番号).Columns(ジャッジ記号).HeaderText = 採点結果.種目(種目番号).選手(1).審判(j).ジャッジ記号


                DGVリスト(種目番号).Columns(ジャッジ記号).HeaderCell.Style.BackColor = Color.White


            Else

                '審判点数をヘッダーに記載する。
                DGVリスト(種目番号).Columns(ジャッジ記号).HeaderText = 採点結果.種目(種目番号).選手(1).審判(j).ジャッジ記号 & "(" & 審判点数(j) & ")"

                If 審判点数(j) = UP予定数 Then
                    '色を変える
                    DGVリスト(種目番号).Columns(ジャッジ記号).HeaderCell.Style.BackColor = Color.Cyan
                ElseIf 審判点数(j) > UP予定数 Then
                    DGVリスト(種目番号).Columns(ジャッジ記号).HeaderCell.Style.BackColor = Color.LightPink
                ElseIf 審判点数(j) < UP予定数 Then
                    DGVリスト(種目番号).Columns(ジャッジ記号).HeaderCell.Style.BackColor = Color.LightYellow

                Else
                    DGVリスト(種目番号).Columns(ジャッジ記号).HeaderCell.Style.BackColor = Color.White
                End If

            End If





        Next j



        'あとは右寄せ
        For i = 7 To 4 + 採点結果.種目(種目番号).審判員数woRef
            DGVリスト(種目番号).Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next i
        'i = tbl.Rows(1).Item("LName").ToString()

        '色の変更
        'SendFlagの確認
        For DGVs = 1 To Me.DGVリスト(種目番号).RowCount
            '背番号
            For 結果s = 1 To 採点結果.種目(種目番号).選手数
                背番号 = 採点結果.種目(種目番号).選手(結果s).背番号

                If DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Value = 背番号 Then

                    If 採点結果.種目(種目番号).選手(結果s).選手SEND済FLAG = True Then                        '
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(1).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(2).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(3).Style.BackColor = Color.Cyan
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(4).Style.BackColor = Color.Cyan
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(5).Style.BackColor = Color.Cyan
                    Else
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(1).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(2).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(3).Style.BackColor = Nothing
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(4).Style.BackColor = Nothing
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(5).Style.BackColor = Nothing

                    End If

                End If

            Next 結果s


        Next DGVs






        '列の非表示設定


        'SCファイルの作成

        'SCファイルのチェック法用は別途検討

        'SCファイル作成_種目別(種目番号, 種目)


    End Sub



    Private Sub 種目別データ更新_順位法(ByVal 種目番号 As Integer)


        'データクリア
        DGVリスト(種目番号).DataSource = Nothing
        DGVリスト(種目番号).Rows.Clear()

        'DGVのデフォルトフォント変更
        DGVリスト(種目番号).DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)


        'イベントハンドらの登録
        AddHandler DGVリスト(種目番号).CellFormatting, AddressOf Me.DGV_種目_CellFormatting


        '採点結果の更新
        採点結果.種目(種目番号).更新()

        '種目数の確定

        'ジャッジ人数の確定（ジャッジ＋減点ジャッジ）
        Dim ジャッジ人数 As Integer
        ジャッジ人数 = 採点結果.種目(種目番号).審判員数


        '１～５種目
        'タブタイトル設定（種目名）
        Dim タブタイトル

        タブタイトル = 採点結果.種目(種目番号).種目記号

        Dim 種目 As D_種目 = 採点結果.種目(種目番号).マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目番号)
        If 種目.SG種別 = "S" Then
            タブタイトル = タブタイトル & "(Solo)"
        ElseIf 種目.SG種別 = "G" Then
            タブタイトル = タブタイトル & "(Group)"
        ElseIf 種目.SG種別 = "D" Then
            タブタイトル = タブタイトル & "(Duel)"
        End If


        タブページ(種目番号).Name = タブタイトル
        タブページ(種目番号).Text = タブタイトル

        'DGVを先に作らないと色が変わらないので、これを追加　--- うまくいかない
        'タブページ(種目番号).Visible = True

        '列タイトル設定
        ' １列目 NO
        ' ２列目 リーダ名
        '削除 ３列目 パートナー名
        '削除 ４列目 所属
        ' ３列目 ヒート番号
        ' ４列目 合計点　　　順位点合計
        ' ５列目 順位
        ' ６列目～１ｘ列目　ジャッジ人数＋減点ジャッジ

        '決定根拠
        '過半数
        '多数決
        '上位加算
        '下位比較


        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("LName", GetType(String)))
        'tbl.Columns.Add(New DataColumn("PName", GetType(String)))
        'tbl.Columns.Add(New DataColumn("Coutry", GetType(String)))
        tbl.Columns.Add(New DataColumn("Heat", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("Total", GetType(Decimal)))
        tbl.Columns.Add(New DataColumn("Place", GetType(Integer)))


        'ジャッジ列の追加
        Dim j
        For j = 1 To ジャッジ人数
            If 採点結果.種目(種目番号).選手(1).審判(j).ジャッジRole <> "R" Then

                'ジャッジ記号
                tbl.Columns.Add(New DataColumn(採点結果.種目(種目番号).選手(1).審判(j).ジャッジ記号, GetType(Double)))
            End If
        Next j

        tbl.Columns.Add(New DataColumn("決定根拠", GetType(String)))
        tbl.Columns.Add(New DataColumn("過半数順位", GetType(String)))
        tbl.Columns.Add(New DataColumn("多数決", GetType(String)))
        tbl.Columns.Add(New DataColumn("上位加算", GetType(String)))
        tbl.Columns.Add(New DataColumn("下位比較", GetType(String)))




        '行データ表示
        Dim i, 背番号, リーダ名, パートナー名, 所属, ヒート番号, 合計点, 順位
        Dim idx

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        Dim 審判点数() As Integer
        ReDim 審判点数(採点結果.種目(種目番号).審判員数)


        '順位順に表示
        For s = 1 To 採点結果.種目(種目番号).選手数

            '順位が s の選手を探す
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果.種目(種目番号).選手数
                If 採点結果.種目(種目番号).選手(t).種目順位番号 = s Then
                    選手番号 = t
                    t = 採点結果.種目(種目番号).選手数
                End If
            Next t


            背番号 = 採点結果.種目(種目番号).選手(選手番号).背番号

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            リーダ名 = 選手.リーダー表記名
            パートナー名 = 選手.パートナ表記名
            所属 = 選手.カップル所属名
            ヒート番号 = 採点結果.マスタデータ.E_ヒート表マスタ.Get_ヒート番号(種目番号, 背番号)
            合計点 = 採点結果.種目(種目番号).選手(選手番号).スケーティング結果_選手.順位点数
            順位 = 採点結果.種目(種目番号).選手(選手番号).種目順位表記

            tbl.Rows.Add()
            idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item(0) = 背番号
            tbl.Rows(idx).Item(1) = リーダ名

            'tbl.Rows(idx).Item(2) = パートナー名
            'tbl.Rows(idx).Item(3) = 所属
            tbl.Rows(idx).Item(2) = ヒート番号
            tbl.Rows(idx).Item(3) = 合計点
            tbl.Rows(idx).Item(4) = 順位


            'Me.DGV_01.Rows.Add(背番号, リーダ名, パートナー名, 所属, ヒート番号, 合計点, 順位)
            'ジャッジごとのPCS詳細

            Dim jj As Integer = 1
            For j = 1 To 採点結果.種目(種目番号).審判員数


                If 採点結果.種目(種目番号).選手(選手番号).審判(j).ジャッジRole <> "R" Then

                    tbl.Rows(idx).Item(4 + jj) = 採点結果.種目(種目番号).選手(選手番号).審判(j).素点.ToString

                    ' 審判点数(j) = 審判点数(j) + 採点結果.種目(種目番号).選手(選手番号).審判(j).素点

                    jj = jj + 1
                End If
            Next j

            tbl.Rows(idx).Item(4 + jj) = 採点結果.種目(種目番号).選手(選手番号).スケーティング結果_選手.決定根拠
            tbl.Rows(idx).Item(5 + jj) = 採点結果.種目(種目番号).選手(選手番号).スケーティング結果_選手.規程5_過半数順位
            tbl.Rows(idx).Item(6 + jj) = 採点結果.種目(種目番号).選手(選手番号).スケーティング結果_選手.規程6_多数決
            tbl.Rows(idx).Item(7 + jj) = 採点結果.種目(種目番号).選手(選手番号).スケーティング結果_選手.規程7a_上位加算
            tbl.Rows(idx).Item(8 + jj) = 採点結果.種目(種目番号).選手(選手番号).スケーティング結果_選手.規程7b_下位比較


        Next s




        '// DataGridViewにデータセットを設定
        DGVリスト(種目番号).DataSource = tbl


        '列幅の設定

        '===列幅の自動調整
        Me.DGVリスト(種目番号).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGVリスト(種目番号).AllowUserToResizeColumns = True



        For i = 0 To 4 + 採点結果.種目(種目番号).審判員数woRef
            DGVリスト(種目番号).Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Next i

        'ヒート４と順位６は真ん中寄せ 合計点（５）は右寄せ
        DGVリスト(種目番号).Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGVリスト(種目番号).Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVリスト(種目番号).Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '小数点以下3桁表示
        DGVリスト(種目番号).Columns("Total").DefaultCellStyle.Format = "0"


        Dim UP予定数 As Integer = 採点結果.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).UP予定数
        DGVリスト(種目番号).EnableHeadersVisualStyles = False

        '　　ジャッジ列
        For j = 1 To ジャッジ人数
            Dim ジャッジ記号 As String = 採点結果.種目(種目番号).選手(1).審判(j).ジャッジ記号
            If 採点結果.種目(種目番号).選手(1).審判(j).ジャッジRole <> "R" Then
                DGVリスト(種目番号).Columns(ジャッジ記号).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DGVリスト(種目番号).Columns(ジャッジ記号).DefaultCellStyle.Format = "0"
            End If



        Next j



        'あとは右寄せ
        For i = 7 To 4 + 採点結果.種目(種目番号).審判員数woRef
            DGVリスト(種目番号).Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next i
        'i = tbl.Rows(1).Item("LName").ToString()

        '色の変更
        'SendFlagの確認
        For DGVs = 1 To Me.DGVリスト(種目番号).RowCount
            '背番号
            For 結果s = 1 To 採点結果.種目(種目番号).選手数
                背番号 = 採点結果.種目(種目番号).選手(結果s).背番号

                If DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Value = 背番号 Then

                    If 採点結果.種目(種目番号).選手(結果s).選手SEND済FLAG = True Then                        '
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(1).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(2).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(3).Style.BackColor = Color.Cyan
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(4).Style.BackColor = Color.Cyan
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(5).Style.BackColor = Color.Cyan
                    Else
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(1).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(2).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(3).Style.BackColor = Nothing
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(4).Style.BackColor = Nothing
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(5).Style.BackColor = Nothing

                    End If

                End If

            Next 結果s


        Next DGVs


        '順位は太字
        Me.DGVリスト(種目番号).Columns("Place").DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Bold)




        '列の非表示設定


        'SCファイルの作成

        'SCファイルのチェック法用は別途検討

        'SCファイル作成_種目別(種目番号, 種目)


    End Sub


    Private Sub 種目別データ更新_PDJ10J(ByVal 種目番号 As Integer, RealFlag As Boolean, ジャッジ結果_J As S_採点結果_V2_J)


        Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & ":F501 種目別データ更新_PDJ10J開始")

        'データクリア
        DGVリスト(種目番号).DataSource = Nothing
        DGVリスト(種目番号).Rows.Clear()
        DGVリスト(種目番号).Columns.Clear()


        '自動更新停止
        DGVリスト(種目番号).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        DGVリスト(種目番号).AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        DGVリスト(種目番号).ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing


        'DGVのデフォルトフォント変更
        DGVリスト(種目番号).DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)



        '採点結果の更新
        If RealFlag = False Then
            採点結果_V2.種目(種目番号).更新()

        Else
            Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & ":採点結果 リアル更新開始")

            採点結果_V2.種目(種目番号).リアル更新(ジャッジ結果_J.ジャッジ記号, ジャッジ結果_J)

            Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & ":採点結果 リアル更新終了")

        End If


        '種目数の確定

        'ジャッジ人数の確定（ジャッジ＋減点ジャッジ）
        Dim ジャッジ人数 As Integer
        ジャッジ人数 = 採点結果_V2.種目(種目番号).審判員数


        '１～５種目
        'タブタイトル設定（種目名）
        Dim タブタイトル

        タブタイトル = 採点結果_V2.種目(種目番号).種目記号

        Dim ソロ種目記号 As String = ""  '課題用

        'Dim 種目 As D_種目 = 採点結果_V2.種目(種目番号).マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目番号)
        Dim 種目 As D_種目 = 採点結果_V2.マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目番号)

        If 種目.SG種別 = "S" Then
            タブタイトル = タブタイトル & "(Solo)"
            ソロ種目記号 = 種目.種目記号
        ElseIf 種目.SG種別 = "G" Then
            タブタイトル = タブタイトル & "(Group)"
        ElseIf 種目.SG種別 = "D" Then
            タブタイトル = タブタイトル & "(Duel)"
        End If




        タブページ(種目番号).Name = タブタイトル
        タブページ(種目番号).Text = タブタイトル

        'DGVを先に作らないと色が変わらないので、これを追加　--- うまくいかない
        'タブページ(種目番号).Visible = True

        '列タイトル設定
        ' １列目 NO
        ' ２列目 リーダ名
        '削除 ３列目 パートナー名
        '削除 ４列目 所属
        ' ３列目 ヒート番号
        ' ４列目 合計点
        ' ５列目 順位

        '６列目　TES
        '７列目　GOE

        ' ８列目～１ｘ列目　ジャッジ人数＋減点ジャッジ
        '　PCS ４つ

        ' DataSourceの設定によって、自動で列が追加されないようにする
        DGVリスト(種目番号).AutoGenerateColumns = False




        '// データテーブルの作成
        'Dim tbl As New DataTable

        'tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        'tbl.Columns.Add(New DataColumn("LName", GetType(String)))
        ''tbl.Columns.Add(New DataColumn("PName", GetType(String)))
        ''tbl.Columns.Add(New DataColumn("Coutry", GetType(String)))
        'tbl.Columns.Add(New DataColumn("Heat", GetType(Integer)))
        'tbl.Columns.Add(New DataColumn("Total", GetType(Double)))
        'tbl.Columns.Add(New DataColumn("Place", GetType(Integer)))

        'tbl.Columns.Add(New DataColumn("TES", GetType(Decimal)))
        'tbl.Columns.Add(New DataColumn("GOE", GetType(Decimal)))


        Dim col_No As New DataGridViewTextBoxColumn
        col_No.Name = "No"
        col_No.HeaderText = "No"
        col_No.DataPropertyName = "No"

        Dim col_LName As New DataGridViewTextBoxColumn
        col_LName.Name = "LName"
        col_LName.HeaderText = "LName"
        col_LName.DataPropertyName = "LName"

        Dim col_Heat As New DataGridViewTextBoxColumn
        col_Heat.Name = "Heat"
        col_Heat.HeaderText = "Heat"
        col_Heat.DataPropertyName = "Heat"

        Dim col_Total As New DataGridViewTextBoxColumn
        col_Total.Name = "Total"
        col_Total.HeaderText = "Total"
        col_Total.DataPropertyName = "Total"

        Dim col_Place As New DataGridViewTextBoxColumn
        col_Place.Name = "Place"
        col_Place.HeaderText = "Place"
        col_Place.DataPropertyName = "Place"

        Dim col_TES As New DataGridViewTextBoxColumn
        col_TES.Name = "TES"
        col_TES.HeaderText = "TES"
        col_TES.DataPropertyName = "TES"

        Dim col_GOE As New DataGridViewTextBoxColumn
        col_GOE.Name = "GOE"
        col_GOE.HeaderText = "GOE"
        col_GOE.DataPropertyName = "GOE"

        Dim col_J01 As New DataGridViewTextBoxColumn
        col_J01.Name = "J01"
        col_J01.DataPropertyName = "J01"

        Dim col_J02 As New DataGridViewTextBoxColumn
        col_J02.Name = "J02"
        col_J02.DataPropertyName = "J02"

        Dim col_J03 As New DataGridViewTextBoxColumn
        col_J03.Name = "J03"
        col_J03.DataPropertyName = "J03"

        Dim col_J04 As New DataGridViewTextBoxColumn
        col_J04.Name = "J04"
        col_J04.DataPropertyName = "J04"

        Dim col_J05 As New DataGridViewTextBoxColumn
        col_J05.Name = "J05"
        col_J05.DataPropertyName = "J05"

        Dim col_J06 As New DataGridViewTextBoxColumn
        col_J06.Name = "J06"
        col_J06.DataPropertyName = "J06"

        Dim col_J07 As New DataGridViewTextBoxColumn
        col_J07.Name = "J07"
        col_J07.DataPropertyName = "J07"

        Dim col_J08 As New DataGridViewTextBoxColumn
        col_J08.Name = "J08"
        col_J08.DataPropertyName = "J08"

        Dim col_J09 As New DataGridViewTextBoxColumn
        col_J09.Name = "J09"
        col_J09.DataPropertyName = "J09"

        Dim col_J10 As New DataGridViewTextBoxColumn
        col_J10.Name = "J10"
        col_J10.DataPropertyName = "J10"

        Dim col_J11 As New DataGridViewTextBoxColumn
        col_J11.Name = "J11"
        col_J11.DataPropertyName = "J11"

        Dim col_J12 As New DataGridViewTextBoxColumn
        col_J12.Name = "J12"
        col_J12.DataPropertyName = "J12"

        Dim col_J13 As New DataGridViewTextBoxColumn
        col_J13.Name = "J13"
        col_J13.DataPropertyName = "J13"

        Dim col_J14 As New DataGridViewTextBoxColumn
        col_J14.Name = "J14"
        col_J14.DataPropertyName = "J14"

        Dim col_J15 As New DataGridViewTextBoxColumn
        col_J15.Name = "J15"
        col_J15.DataPropertyName = "J15"

        Dim col_J16 As New DataGridViewTextBoxColumn
        col_J16.Name = "J16"
        col_J16.DataPropertyName = "J16"

        Dim col_J17 As New DataGridViewTextBoxColumn
        col_J17.Name = "J17"
        col_J17.DataPropertyName = "J17"

        Dim col_J18 As New DataGridViewTextBoxColumn
        col_J18.Name = "J18"
        col_J18.DataPropertyName = "J18"

        Dim col_J19 As New DataGridViewTextBoxColumn
        col_J19.Name = "J19"
        col_J19.DataPropertyName = "J19"

        Dim col_J20 As New DataGridViewTextBoxColumn
        col_J20.Name = "J20"
        col_J20.DataPropertyName = "J20"



        DGVリスト(種目番号).Columns.Add(col_No)
        DGVリスト(種目番号).Columns.Add(col_LName)
        DGVリスト(種目番号).Columns.Add(col_Heat)
        DGVリスト(種目番号).Columns.Add(col_Place)
        DGVリスト(種目番号).Columns.Add(col_Total)
        DGVリスト(種目番号).Columns.Add(col_TES)
        DGVリスト(種目番号).Columns.Add(col_GOE)


        'ジャッジ列の追加
        Dim j
        Dim JJ_ As Integer = 0
        For j = 1 To ジャッジ人数
            'If 採点結果_V2.種目(種目番号).選手(1).審判(j).ジャッジRole <> "R" Then
            If 採点結果_V2.種目(種目番号).審判員(j).ジャッジタイプ <> "R" _
                And 採点結果_V2.種目(種目番号).審判員(j).ジャッジタイプ <> "T" Then

                'ジャッジ記号
                'tbl.Columns.Add(New DataColumn(採点結果_V2.種目(種目番号).選手(1).審判(j).ジャッジ記号, GetType(Double)))
                ' tbl.Columns.Add(New DataColumn(採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号, GetType(Double)))
                JJ_ = JJ_ + 1
                Select Case JJ_
                    Case 1
                        col_J01.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J01)
                        DGVリスト(種目番号).Columns("J01").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J01").DefaultCellStyle.Format = "0.00"
                    Case 2
                        col_J02.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J02)
                        DGVリスト(種目番号).Columns("J02").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J02").DefaultCellStyle.Format = "0.00"
                    Case 3
                        col_J03.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J03)
                        DGVリスト(種目番号).Columns("J03").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J03").DefaultCellStyle.Format = "0.00"
                    Case 4
                        col_J04.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J04)
                        DGVリスト(種目番号).Columns("J04").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J04").DefaultCellStyle.Format = "0.00"
                    Case 5
                        col_J05.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J05)
                        DGVリスト(種目番号).Columns("J05").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J05").DefaultCellStyle.Format = "0.00"
                    Case 6
                        col_J06.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J06)
                        DGVリスト(種目番号).Columns("J06").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J06").DefaultCellStyle.Format = "0.00"
                    Case 7
                        col_J07.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J07)
                        DGVリスト(種目番号).Columns("J07").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J07").DefaultCellStyle.Format = "0.00"
                    Case 8
                        col_J08.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J08)
                        DGVリスト(種目番号).Columns("J08").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J08").DefaultCellStyle.Format = "0.00"
                    Case 9
                        col_J09.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J09)
                        DGVリスト(種目番号).Columns("J09").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J09").DefaultCellStyle.Format = "0.00"
                    Case 10
                        col_J10.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J10)
                        DGVリスト(種目番号).Columns("J10").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J10").DefaultCellStyle.Format = "0.00"
                    Case 11
                        col_J11.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J11)
                        DGVリスト(種目番号).Columns("J11").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J11").DefaultCellStyle.Format = "0.00"
                    Case 12
                        col_J12.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J12)
                        DGVリスト(種目番号).Columns("J12").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J12").DefaultCellStyle.Format = "0.00"

                    Case 13
                        col_J13.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J13)
                        DGVリスト(種目番号).Columns("J13").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J13").DefaultCellStyle.Format = "0.00"

                    Case 14
                        col_J14.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J14)
                        DGVリスト(種目番号).Columns("J14").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J14").DefaultCellStyle.Format = "0.00"

                    Case 15
                        col_J15.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J15)
                        DGVリスト(種目番号).Columns("J15").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J15").DefaultCellStyle.Format = "0.00"

                    Case 16
                        col_J16.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J16)
                        DGVリスト(種目番号).Columns("J16").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J16").DefaultCellStyle.Format = "0.00"

                    Case 17
                        col_J17.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J17)
                        DGVリスト(種目番号).Columns("J17").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J17").DefaultCellStyle.Format = "0.00"

                    Case 18
                        col_J18.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J18)
                        DGVリスト(種目番号).Columns("J18").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J18").DefaultCellStyle.Format = "0.00"

                    Case 19
                        col_J19.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J19)
                        DGVリスト(種目番号).Columns("J19").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J19").DefaultCellStyle.Format = "0.00"

                    Case 20
                        col_J20.HeaderText = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
                        DGVリスト(種目番号).Columns.Add(col_J20)
                        DGVリスト(種目番号).Columns("J20").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DGVリスト(種目番号).Columns("J20").DefaultCellStyle.Format = "0.00"


                End Select

            End If
        Next j

        '減点ジャッジ列の追加
        'tbl.Columns.Add(New DataColumn("Ref", GetType(Double)))
        Dim col_Ref As New DataGridViewTextBoxColumn
        col_Ref.Name = "Ref"
        col_Ref.HeaderText = "Ref"
        col_Ref.DataPropertyName = "Ref"

        DGVリスト(種目番号).Columns.Add(col_Ref)


        'PCS列の追加
        Dim col_PCS1 As New DataGridViewTextBoxColumn
        col_PCS1.Name = "PCS1"
        col_PCS1.DataPropertyName = "PCS1"

        Dim col_PCS2 As New DataGridViewTextBoxColumn
        col_PCS2.Name = "PCS2"
        col_PCS2.DataPropertyName = "PCS2"

        Dim col_PCS3 As New DataGridViewTextBoxColumn
        col_PCS3.Name = "PCS3"
        col_PCS3.DataPropertyName = "PCS3"

        Dim col_PCS4 As New DataGridViewTextBoxColumn
        col_PCS4.Name = "PCS4"
        col_PCS4.DataPropertyName = "PCS4"



        PCS数 = 採点結果_V2.マスタデータ.J_新審判設定.GetPCS数
        ReDim PCSリスト(PCS数)
        For pcs番号 = 1 To PCS数
            Dim PCS名 = 採点結果_V2.マスタデータ.J_新審判設定.PCS設定(pcs番号).PCS項目名
            'tbl.Columns.Add(New DataColumn(PCS名, GetType(Double)))

            Select Case pcs番号
                Case 1
                    col_PCS1.HeaderText = PCS名
                    DGVリスト(種目番号).Columns.Add(col_PCS1)
                    DGVリスト(種目番号).Columns("PCS1").DefaultCellStyle.Format = "0.000"
                Case 2
                    col_PCS2.HeaderText = PCS名
                    DGVリスト(種目番号).Columns.Add(col_PCS2)
                    DGVリスト(種目番号).Columns("PCS2").DefaultCellStyle.Format = "0.000"
                Case 3
                    col_PCS3.HeaderText = PCS名
                    DGVリスト(種目番号).Columns.Add(col_PCS3)
                    DGVリスト(種目番号).Columns("PCS3").DefaultCellStyle.Format = "0.000"
                Case 4
                    col_PCS4.HeaderText = PCS名
                    DGVリスト(種目番号).Columns.Add(col_PCS4)
                    DGVリスト(種目番号).Columns("PCS4").DefaultCellStyle.Format = "0.000"
            End Select


            PCSリスト(pcs番号) = PCS名

        Next pcs番号


        '自作クラスの作成
        Dim tbl_s() As 種目別
        ReDim Preserve tbl_s(採点結果_V2.種目(種目番号).選手数 - 1)


        '行データ表示
        Dim i, 背番号, リーダ名, パートナー名, 所属, ヒート番号, 合計点, 順位
        Dim idx

        Dim 区分 As B_区分 = 採点結果_V2.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        '順位順に表示
        For s = 1 To 採点結果_V2.種目(種目番号).選手数

            '順位が s の選手を探す
            Dim 選手番号 As Integer = 0
            For t = 1 To 採点結果_V2.種目(種目番号).選手数
                If 採点結果_V2.種目(種目番号).選手結果(t).種目順位番号 = s Then
                    選手番号 = t
                    t = 採点結果_V2.種目(種目番号).選手数
                End If
            Next t


            背番号 = 採点結果_V2.種目(種目番号).選手結果(選手番号).背番号

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果_V2.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            リーダ名 = 選手.リーダー表記名
            パートナー名 = 選手.パートナ表記名
            所属 = 選手.カップル所属名
            ヒート番号 = 採点結果_V2.マスタデータ.E_ヒート表マスタ.Get_ヒート番号(種目番号, 背番号)
            合計点 = Format(CDbl(採点結果_V2.種目(種目番号).選手結果(選手番号).種目得点), "0.000")
            順位 = 採点結果_V2.種目(種目番号).選手結果(選手番号).種目順位表記



            tbl_s(s - 1) = New 種目別(背番号)
            tbl_s(s - 1).Set__Lname(リーダ名)
            tbl_s(s - 1).Set__Heat(ヒート番号)
            tbl_s(s - 1).Set__Total(合計点)
            tbl_s(s - 1).Set__Place(順位)





            'tbl.Rows.Add()
            'idx = tbl.Rows.Count - 1
            'tbl.Rows(idx).Item(0) = 背番号
            'tbl.Rows(idx).Item(1) = リーダ名

            ''tbl.Rows(idx).Item(2) = パートナー名
            ''tbl.Rows(idx).Item(3) = 所属
            'tbl.Rows(idx).Item(2) = ヒート番号
            'tbl.Rows(idx).Item(3) = 合計点
            'tbl.Rows(idx).Item(4) = 順位


            'Me.DGV_01.Rows.Add(背番号, リーダ名, パートナー名, 所属, ヒート番号, 合計点, 順位)

            'TES
            Dim BV() As Decimal
            ReDim BV(採点結果_V2.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号))

            Dim TES As Decimal = 0
            For j = 1 To 採点結果_V2.種目(種目番号).審判員数
                If 採点結果_V2.種目(種目番号).審判員(j).ジャッジタイプ = "T" Then

                    For t = 1 To 採点結果_V2.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)
                        TES = TES + 採点結果_V2.種目(種目番号).選手結果(選手番号).TES得点(t).TES_Base
                        BV(t) = 採点結果_V2.種目(種目番号).選手結果(選手番号).TES得点(t).TES_Base

                        If BV(t) > 0 Then
                            For r = 1 To 採点結果_V2.マスタデータ.J_新審判設定.GetTES減点数
                                TES = TES + 採点結果_V2.種目(種目番号).選手結果(選手番号).TES得点(t).TES減点(r).TES減点
                                BV(t) = BV(t) + 採点結果_V2.種目(種目番号).選手結果(選手番号).TES得点(t).TES減点(r).TES減点
                            Next r
                        End If

                    Next t

                    j = 採点結果_V2.種目(種目番号).審判員数
                End If
            Next j
            'tbl.Rows(idx).Item(5) = Format(TES, "0.00")
            tbl_s(s - 1).Set__TES(Format(TES, "0.00"))


            'GOE
            Dim GOE As Decimal = 0

            For k = 1 To 採点結果_V2.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)

                If BV(k) > 0 Then
                    GOE = GOE + 採点結果_V2.種目(種目番号).選手結果(選手番号).GOE得点(k).GOE得点
                End If

            Next k
            'tbl.Rows(idx).Item(6) = Format(GOE, "0.00")
            tbl_s(s - 1).Set__GOE(Format(GOE, "0.00"))


            'ジャッジごとのPCS詳細

            Dim jj As Integer = 1
            For j = 1 To 採点結果_V2.種目(種目番号).審判員数

                If 採点結果_V2.種目(種目番号).審判員(j).ジャッジタイプ <> "R" _
                    And 採点結果_V2.種目(種目番号).審判員(j).ジャッジタイプ <> "T" Then

                    Dim PCS点 As Decimal = 0
                    For p = 1 To PCS数
                        PCS点 = PCS点 + 採点結果_V2.種目(種目番号).選手結果(選手番号).審判員結果(jj).PCS素点(p).PCS素点
                    Next p

                    'tbl.Rows(idx).Item(6 + jj) = PCS点.ToString("0.00")
                    tbl_s(s - 1).Set__Judge(jj, PCS点.ToString("0.00"))

                    jj = jj + 1
                End If

            Next j



            If 採点結果_V2.種目(種目番号).レフリー番号 <> 0 Then
                'tbl.Rows(idx).Item(6 + 採点結果_V2.種目(種目番号).Get_一般審判員数 + 1) = 採点結果_V2.種目(種目番号).選手結果(選手番号).Get_表示用一般減点
                tbl_s(s - 1).Set__Ref(採点結果_V2.種目(種目番号).選手結果(選手番号).Get_表示用一般減点)
            Else
                'tbl.Rows(idx).Item(6 + 採点結果_V2.種目(種目番号).Get_一般審判員数 + 1) = 0
                tbl_s(s - 1).Set__Ref(0)
            End If


            '各PCSの値
            Dim PCS
            For PCS = 1 To PCS数
                'tbl.Rows(idx).Item(6 + 採点結果_V2.種目(種目番号).Get_一般審判員数 + 1 + PCS) = 採点結果_V2.種目(種目番号).選手結果(選手番号).PCS得点(PCS).PCS得点  '.ToString("0.000")
                tbl_s(s - 1).Set__PCS(PCS, 採点結果_V2.種目(種目番号).選手結果(選手番号).PCS得点(PCS).PCS得点)
            Next PCS


        Next s






        '// DataGridViewにデータセットを設定
        ' DGVリスト(種目番号).DataSource = tbl
        DGVリスト(種目番号).DataSource = tbl_s

        '列幅の設定

        '===列幅の自動調整
        Me.DGVリスト(種目番号).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGVリスト(種目番号).AllowUserToResizeColumns = True



        For i = 0 To 4 + 採点結果_V2.種目(種目番号).Get_一般審判員数 + 1 + PCS数
            DGVリスト(種目番号).Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Next i

        'ヒート４と順位６は真ん中寄せ 合計点（５）は右寄せ
        DGVリスト(種目番号).Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGVリスト(種目番号).Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVリスト(種目番号).Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '小数点以下3桁表示
        DGVリスト(種目番号).Columns("Total").DefaultCellStyle.Format = "0.000"

        '　　ジャッジ列
        '   For j = 1 To ジャッジ人数
        '   Dim ジャッジ記号 As String = 採点結果_V2.種目(種目番号).審判員(j).ジャッジ記号
        '  If 採点結果_V2.種目(種目番号).審判員(j).ジャッジタイプ <> "R" And 採点結果_V2.種目(種目番号).審判員(j).ジャッジタイプ <> "T" Then
        ' DGVリスト(種目番号).Columns(ジャッジ記号).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DGVリスト(種目番号).Columns(ジャッジ記号).DefaultCellStyle.Format = "0.00"
        'End If
        'Next j

        '　PCS列
        'For p = 1 To PCS数
        ' DGVリスト(種目番号).Columns(PCSリスト(p)).DefaultCellStyle.Format = "0.000"
        'Next p


        'あとは右寄せ
        For i = 7 To 4 + 採点結果_V2.種目(種目番号).Get_一般審判員数 + 1 + PCS数
            DGVリスト(種目番号).Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next i
        'i = tbl.Rows(1).Item("LName").ToString()



        '色の変更
        'SendFlagの確認

        Dim 全SEND確認FG As Boolean = True

        For DGVs = 1 To Me.DGVリスト(種目番号).RowCount
            '背番号
            For 結果s = 1 To 採点結果_V2.種目(種目番号).選手数
                背番号 = 採点結果_V2.種目(種目番号).選手結果(結果s).背番号

                If DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Value = 背番号 Then

                    If 採点結果_V2.種目(種目番号).選手結果(結果s).SEND_FLAG = 1 Then                        '
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(1).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(2).Style.BackColor = Color.Cyan
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(3).Style.BackColor = Color.Cyan
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(4).Style.BackColor = Color.Cyan
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(5).Style.BackColor = Color.Cyan
                    Else
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(0).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(1).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(2).Style.BackColor = Nothing
                        DGVリスト(種目番号).Rows(DGVs - 1).Cells(3).Style.BackColor = Nothing
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(4).Style.BackColor = Nothing
                        'DGVリスト(種目番号).Rows(DGVs - 1).Cells(5).Style.BackColor = Nothing

                        全SEND確認FG = False
                    End If

                End If

            Next 結果s


        Next DGVs




        '列の非表示設定


        'SCファイルの作成

        Dim 採点方式 As String = 採点結果_V2.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        If Strings.Left(採点方式, 3) <> "VAL" Then

            If RealFlag = False Then
                SCファイル作成_種目別_V2(種目番号, 種目)
            Else

            End If


        End If

        '自動更新開始
        DGVリスト(種目番号).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DGVリスト(種目番号).AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        DGVリスト(種目番号).ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize




        'イベントハンドらの登録
        AddHandler DGVリスト(種目番号).CellFormatting, AddressOf Me.DGV_種目_CellFormatting



        Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & ":F501 種目別データ更新_PDJ10J終了")

        '現在のタブが該当の種目番号の時　
        If 全SEND確認FG = True And Me.TabControl_詳細.SelectedTab.TabIndex + 1 = 種目番号 Then

            Timer2.Interval = 10000   '10秒毎
            Timer2.Enabled = True

        End If

    End Sub


    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'タイマー毎に呼ばれるイベント

        If LOGLEVEL >= 4 Then
            If Me.InvokeRequired Then
                Me.Invoke(LogDelegate, New Object() {"F501_Timer2から更新呼び出し", 4})
            Else
                WriteLog("F501_Timer2から更新呼び出し", 4)
            End If
        End If

        タブ移動実施()
        Timer2.Enabled = False

    End Sub

    Private Sub タブ移動実施()

        '現在選択されているタブ
        Dim 現在タブ = Me.TabControl_詳細.SelectedTab

        '総種目数
        Dim 総タブ数 As Integer = 採点結果_V2.種目数

        If 現在タブ.TabIndex + 1 <= 総タブ数 - 1 Then
            '次のタブを選択する
            Me.TabControl_詳細.SelectTab(現在タブ.TabIndex + 1)
        End If


    End Sub



    Private Sub 種目別結果リアル更新(ジャッジ結果_J As S_採点結果_V2_J)


        Dim 種目番号 = ジャッジ結果_J.種目番号

        '採点結果更新
        Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & ":F501 採点結果 リアル更新開始")

        採点結果_V2.種目(種目番号).リアル更新(ジャッジ結果_J.ジャッジ記号, ジャッジ結果_J)

        Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & ":F501 採点結果 リアル更新終了")


        '該当ジャッジ部分を更新
        Dim ジャッジ人数 As Integer
        ジャッジ人数 = 採点結果_V2.種目(種目番号).審判員数

        Dim 背番号_列 = 0


        Dim ジャッジ列番号 As Integer = 0


        If ジャッジ結果_J.ジャッジタイプ = "J" Or ジャッジ結果_J.ジャッジタイプ = "S" Then

            '該当ジャッジの列番号を検索
            For j = 1 To DGVリスト(種目番号).ColumnCount - 1
                If DGVリスト(種目番号).Columns(j).HeaderText = ジャッジ結果_J.ジャッジ記号 Then
                    ジャッジ列番号 = j
                    j = DGVリスト(種目番号).ColumnCount
                End If
            Next j


            '該当ジャッジ列を更新
            For s_d = 0 To DGVリスト(種目番号).RowCount - 1
                For s_j = 1 To UBound(ジャッジ結果_J.S_採点結果_選手_J)
                    If DGVリスト(種目番号).Rows(s_d).Cells(背番号_列).Value = ジャッジ結果_J.S_採点結果_選手_J(s_j).背番号 Then

                        Dim PCS得点 As Decimal = 0
                        For p = 1 To ジャッジ結果_J.PCS数
                            PCS得点 = PCS得点 + ジャッジ結果_J.S_採点結果_選手_J(s_j).PCS得点_J(p).PCS素点
                        Next p
                        DGVリスト(種目番号).Rows(s_d).Cells(ジャッジ列番号).Value = PCS得点

                        s_j = UBound(ジャッジ結果_J.S_採点結果_選手_J)
                    End If
                Next s_j
            Next s_d

        ElseIf ジャッジ結果_J.ジャッジタイプ = "R" Then
            'レフリーの時

            '該当ジャッジの列番号を検索
            For j = 1 To DGVリスト(種目番号).ColumnCount - 1
                If DGVリスト(種目番号).Columns(j).HeaderText = "Ref" Then
                    ジャッジ列番号 = j
                    j = DGVリスト(種目番号).ColumnCount
                End If
            Next j

            '該当ジャッジ列を更新
            For s_d = 0 To DGVリスト(種目番号).RowCount - 1
                For s_j = 1 To UBound(ジャッジ結果_J.S_採点結果_選手_J)
                    If DGVリスト(種目番号).Rows(s_d).Cells(背番号_列).Value = ジャッジ結果_J.S_採点結果_選手_J(s_j).背番号 Then

                        Dim 減点 As Decimal = 0
                        For d = 1 To ジャッジ結果_J.減点項目数
                            減点 = 減点 + ジャッジ結果_J.S_採点結果_選手_J(s_j).減点_J(d).減点
                        Next d
                        DGVリスト(種目番号).Rows(s_d).Cells(ジャッジ列番号).Value = 減点

                        s_j = UBound(ジャッジ結果_J.S_採点結果_選手_J)
                    End If
                Next s_j
            Next s_d



        End If


        'Total列を更新
        Dim Total列番号 As Integer = 0

        For j = 1 To DGVリスト(種目番号).ColumnCount
            If DGVリスト(種目番号).Columns(j).HeaderText = "Total" Then
                Total列番号 = j
                j = DGVリスト(種目番号).ColumnCount
            End If
        Next j

        For s_d = 0 To DGVリスト(種目番号).RowCount - 1
            For s_j = 1 To UBound(採点結果_V2.種目(種目番号).選手結果)
                If DGVリスト(種目番号).Rows(s_d).Cells(背番号_列).Value = 採点結果_V2.種目(種目番号).選手結果(s_j).背番号 Then


                    DGVリスト(種目番号).Rows(s_d).Cells(Total列番号).Value = 採点結果_V2.種目(種目番号).選手結果(s_j).種目得点

                    s_j = UBound(採点結果_V2.種目(種目番号).選手結果)
                End If
            Next s_j
        Next s_d



        'ソート    DGVに自作のデータをバインドしている場合はソートができない（仕様）
        '並び替える列を決める
        'Dim sortColumn As DataGridViewColumn = DGVリスト(種目番号).Columns(Total列番号)
        '
        'Dim sortDirection As System.ComponentModel.ListSortDirection = System.ComponentModel.ListSortDirection.Ascending
        '
        'DGVリスト(種目番号).Sort(sortColumn, sortDirection)



        Console.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & ":F501 種目別結果リアル更新終了")


    End Sub

    '更新ボタンが押された時
    Private Sub PB_更新_Click(sender As Object, e As EventArgs) Handles PB_更新.Click

        If LOGLEVEL >= 4 Then
            If Me.InvokeRequired Then
                Me.Invoke(LogDelegate, New Object() {"F501_「更新ボタン」から更新呼び出し", 4})
            Else
                WriteLog("F501_「更新ボタン」から更新呼び出し", 4)
            End If
        End If


        If 採点結果 IsNot Nothing Then


            更新()

        ElseIf 採点結果_V2 IsNot Nothing Then

            更新_V2()

        End If


    End Sub


    Private Sub SCファイル作成_総合()

        'SC_Compの作成

        Dim SC_COMP
        ReDim SC_COMP(2 + 採点結果.種目(1).審判員数, 3)

        SC_COMP(1, 1) = 採点結果.マスタデータ.A_競技会マスタ.競技会名
        SC_COMP(1, 2) = 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(区分番号)
        SC_COMP(1, 3) = 採点結果.マスタデータ.Get_ラウンド名(ラウンド番号)

        SC_COMP(2, 1) = 採点結果.種目(1).審判員数

        Dim jg

        For jg = 1 To 採点結果.種目(1).審判員数
            SC_COMP(2 + jg, 1) = 採点結果.種目(1).選手(1).審判(jg).ジャッジ記号
            SC_COMP(2 + jg, 2) = 採点結果.種目(1).選手(1).審判(jg).ジャッジ表記名
            SC_COMP(2 + jg, 3) = 採点結果.マスタデータ.審判員マスタ.Get_審判Class(採点結果.種目(1).選手(1).審判(jg).ジャッジ記号).ジャッジ所属
        Next jg


        'SC_Total_Startlistの作成

        Dim SC_Total_Startlist
        ReDim SC_Total_Startlist(採点結果.出場選手数 + 1, 6)

        '１行目

        SC_Total_Startlist(1, 1) = 採点結果.出場選手数

        Dim 種目記号リスト() = Nothing
        Dim 種目数 = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        For i = 1 To 種目数
            SC_Total_Startlist(1, i + 1) = 種目記号リスト(i)
        Next i

        '２行目 以降　選手リスト

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        For s = 1 To 採点結果.出場選手数
            Dim 背番号 As String = 採点結果.背番号(s)
            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            SC_Total_Startlist(s + 1, 1) = s
            SC_Total_Startlist(s + 1, 2) = 採点結果.背番号(s)
            SC_Total_Startlist(s + 1, 3) = 選手.リーダー表記名
            SC_Total_Startlist(s + 1, 4) = 選手.パートナ表記名
            SC_Total_Startlist(s + 1, 5) = 選手.カップル所属名

        Next s

        'SC_Total_Resultの作成

        Dim SC_Total_Result
        ReDim SC_Total_Result(採点結果.出場選手数 + 1, 11)

        Dim SC_Total_Result_Temp
        ReDim SC_Total_Result_Temp(採点結果.出場選手数 + 1, 11)

        '１行目

        SC_Total_Result(1, 1) = 採点結果.出場選手数

        For i = 1 To 種目数
            SC_Total_Result(1, i + 1) = 種目記号リスト(i)
        Next i

        '２行目

        For s = 1 To 採点結果.出場選手数

            Dim 背番号 As String = 採点結果.背番号(s)
            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)


            SC_Total_Result_Temp(s + 1, 1) = 採点結果.総合順位表記(s)
            SC_Total_Result_Temp(s + 1, 2) = 採点結果.背番号(s)
            SC_Total_Result_Temp(s + 1, 3) = 選手.リーダー表記名
            SC_Total_Result_Temp(s + 1, 4) = 選手.パートナ表記名
            SC_Total_Result_Temp(s + 1, 5) = 選手.カップル所属名
            SC_Total_Result_Temp(s + 1, 6) = 採点結果.総合得点(s)

            For d = 1 To 種目数
                SC_Total_Result_Temp(s + 1, 6 + d) = 採点結果.種目(d).選手(s).種目得点.ToString("0.000")
            Next d


        Next s

        'ソート SC_Total_Result_Temp　を順位順にソート 
        Dim j, r
        j = 1
        Dim 順位 = 1
        For 順位 = 1 To 採点結果.出場選手数
            For s = 1 To 採点結果.出場選手数
                If SC_Total_Result_Temp(s + 1, 1) = 順位 Then
                    For r = 1 To 11
                        SC_Total_Result(j + 1, r) = SC_Total_Result_Temp(s + 1, r)
                    Next r
                    j = j + 1
                End If
            Next s
        Next 順位

        '====


        Call SCファイル書出し("SC_COMP.csv", 採点結果.マスタデータ.Z_システム設定.システムPath, SC_COMP)
        Call SCファイル書出し("SC_Total_Startlist.csv", 採点結果.マスタデータ.Z_システム設定.システムPath, SC_Total_Startlist)
        Call SCファイル書出し("SC_Total_Result.csv", 採点結果.マスタデータ.Z_システム設定.システムPath, SC_Total_Result)

        Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_COMP.csv", 採点結果.マスタデータ.Z_システム設定.Comp_filepath, SC_COMP)
        Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_Total_Startlist.csv", 採点結果.マスタデータ.Z_システム設定.Comp_filepath, SC_Total_Startlist)
        Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_Total_Result.csv", 採点結果.マスタデータ.Z_システム設定.Comp_filepath, SC_Total_Result)



    End Sub

    Private Sub SCファイル作成_種目別(ByVal 種目番号 As Integer, ByVal 種目 As D_種目)

        'SC_StartList と SC_Result を作成


        Dim SC_StartList
        Dim SC_Result
        ReDim SC_StartList(採点結果.出場選手数 + 1, 5)
        ReDim SC_Result(採点結果.出場選手数 + 1, 13)

        'SC_StartListの1行目

        If 採点結果.マスタデータ.Z_システム設定.言語 = "E" Then

            SC_StartList(1, 1) = 採点結果.出場選手数
            SC_StartList(1, 2) = 採点結果.マスタデータ.Z_システム設定.Get_種目名称(種目.種目記号).種目名_E    '種目名
            SC_StartList(1, 3) = 種目.SG種別  'ソロ・グループ区分

            SC_Result(1, 1) = 採点結果.出場選手数
            SC_Result(1, 2) = 採点結果.マスタデータ.Z_システム設定.Get_種目名称(種目.種目記号).種目名_E    '種目名
            SC_Result(1, 3) = 種目.SG種別    'ソロ・グループ区分

        Else

            SC_StartList(1, 1) = 採点結果.出場選手数
            SC_StartList(1, 2) = 採点結果.マスタデータ.Z_システム設定.Get_種目名称(種目.種目記号).種目名_J    '種目名
            SC_StartList(1, 3) = 種目.SG種別  'ソロ・グループ区分

            SC_Result(1, 1) = 採点結果.出場選手数
            SC_Result(1, 2) = 採点結果.マスタデータ.Z_システム設定.Get_種目名称(種目.種目記号).種目名_J     '種目名
            SC_Result(1, 3) = 種目.SG種別    'ソロ・グループ区分
        End If


        'SC_StartListの登録 2行目以降

        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ


        For i = 1 To 採点結果.出場選手数


            Dim 背番号 As String = 採点結果.種目(種目番号).選手(i).背番号

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            Dim リーダ名 = 選手.リーダー表記名
            Dim パートナー名 = 選手.パートナ表記名
            Dim 所属 = 選手.カップル所属名
            Dim ヒート番号 = 採点結果.マスタデータ.E_ヒート表マスタ.Get_ヒート番号(種目番号, 背番号)
            Dim 合計点 = 採点結果.種目(種目番号).選手(i).種目得点.ToString("0.000")
            Dim 順位 = 採点結果.種目(種目番号).選手(i).種目順位表記



            'SC_StartListの登録

            If 種目.SG種別 = "S" Then
                'ソロのときは演技順
                SC_StartList(ヒート番号 + 1, 1) = ヒート番号
                SC_StartList(ヒート番号 + 1, 2) = 背番号
                SC_StartList(ヒート番号 + 1, 3) = リーダ名
                SC_StartList(ヒート番号 + 1, 4) = パートナー名
                SC_StartList(ヒート番号 + 1, 5) = 所属
            ElseIf 種目.SG種別 = "G" Then
                '全員競技の時は背番号順
                SC_StartList(i + 1, 1) = ヒート番号
                SC_StartList(i + 1, 2) = 背番号
                SC_StartList(i + 1, 3) = リーダ名
                SC_StartList(i + 1, 4) = パートナー名
                SC_StartList(i + 1, 5) = 所属
            Else
                'マッチ(Duel)の時

            End If

            'SC_Resultの登録
            Dim 順位番号 As Integer = 採点結果.種目(種目番号).選手(i).種目順位番号

            SC_Result(順位番号 + 1, 1) = ヒート番号
            SC_Result(順位番号 + 1, 2) = 順位
            SC_Result(順位番号 + 1, 3) = 背番号
            SC_Result(順位番号 + 1, 4) = リーダ名
            SC_Result(順位番号 + 1, 5) = パートナー名
            SC_Result(順位番号 + 1, 6) = 所属
            SC_Result(順位番号 + 1, 7) = 合計点
            SC_Result(順位番号 + 1, 8) = Format(採点結果.種目(種目番号).選手(i).種目各PCS得点(1), "#.##0")
            SC_Result(順位番号 + 1, 9) = Format(採点結果.種目(種目番号).選手(i).種目各PCS得点(2), "#.##0")
            SC_Result(順位番号 + 1, 10) = Format(採点結果.種目(種目番号).選手(i).種目各PCS得点(3), "#.##0")
            SC_Result(順位番号 + 1, 11) = Format(採点結果.種目(種目番号).選手(i).種目各PCS得点(4), "#.##0")

            If 採点結果.種目(種目番号).レフリー番号 <> 0 Then
                SC_Result(順位番号 + 1, 12) = 採点結果.種目(種目番号).選手(i).審判(採点結果.種目(種目番号).レフリー番号).表示用減点合計点   '減点
            Else
                SC_Result(順位番号 + 1, 12) = 0
            End If

            '==減点項目名の表示
            Dim 減点Text As String = ""
            Dim 減点数 As Integer = 0
            Dim 減点合計点 As Double = 0

            For r = 1 To 20
                If 減点数 = 2 Then
                    減点Text = 減点Text & "etc"
                    r = 20
                ElseIf 採点結果.種目(種目番号).選手(i).種目各減点(r) <> 0 Then
                    減点Text = 減点Text & "," & 採点結果.マスタデータ.J_新審判設定.減点設定(r).減点項目名
                    減点数 = 減点数 + 1

                    If 採点結果.種目(種目番号).選手(i).種目各減点(r) < 0 Then
                        減点合計点 = 減点合計点 + 採点結果.種目(種目番号).選手(i).種目各減点(r)
                    Else
                        減点合計点 = 0
                    End If
                End If

            Next r




            'If 採点結果.種目(種目番号).選手(i).失格FLAG = True Then
            '減点Text = "Lost(失格)"
            'Else
            'For r = 2 To 20
            'If 減点数 = 2 Then
            '減点Text = 減点Text & "etc"
            'r = 20
            'ElseIf 採点結果.種目(種目番号).選手(i).種目各減点(r) <> 0 Then
            '減点Text = 減点Text & "," & 採点結果.マスタデータ.J_新審判設定.減点設定(r).減点項目名
            '減点数 = 減点数 + 1

            '減点合計点 = 減点合計点 + 採点結果.種目(種目番号).選手(i).種目各減点(r)
            'End If
            'Next r

            'End If

            If Strings.Left(採点結果.採点方式, 3) = "BJS" Then
                'BJSの時は、減点をここで設定する。　AJSの時も本当はこれでもいいかも。。。。
                SC_Result(順位番号 + 1, 12) = 減点合計点
            End If

            SC_Result(順位番号 + 1, 13) = 減点Text

        Next i


        'SC_Fileの書き出し
        Call SCファイル書出し("SC_0" & 種目番号 & "_Startlist.csv", 採点結果.マスタデータ.Z_システム設定.システムPath, SC_StartList)
        Call SCファイル書出し("SC_0" & 種目番号 & "_Result.csv", 採点結果.マスタデータ.Z_システム設定.システムPath, SC_Result)


        Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_0" & 種目番号 & "_Startlist.csv", 採点結果.マスタデータ.Z_システム設定.Comp_filepath, SC_StartList)
        Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_0" & 種目番号 & "_Result.csv", 採点結果.マスタデータ.Z_システム設定.Comp_filepath, SC_Result)

    End Sub

    Private Sub SCファイル作成_総合_V2()   'PDJ用

        'SC_Compの作成

        Dim SC_COMP
        ReDim SC_COMP(2 + 採点結果_V2.種目(1).審判員数, 3)

        SC_COMP(1, 1) = 採点結果_V2.マスタデータ.A_競技会マスタ.競技会名
        SC_COMP(1, 2) = 採点結果_V2.マスタデータ.B_区分マスタ.Get区分表記名(区分番号)
        SC_COMP(1, 3) = 採点結果_V2.マスタデータ.Get_ラウンド名(ラウンド番号)

        SC_COMP(2, 1) = 採点結果_V2.種目(1).審判員数

        Dim jg

        For jg = 1 To 採点結果_V2.種目(1).審判員数
            SC_COMP(2 + jg, 1) = 採点結果_V2.種目(1).選手結果(1).審判員結果(jg).ジャッジ記号
            SC_COMP(2 + jg, 2) = 採点結果_V2.種目(1).審判員(jg).ジャッジ表記名
            SC_COMP(2 + jg, 3) = 採点結果_V2.マスタデータ.審判員マスタ.Get_審判Class(採点結果_V2.種目(1).選手結果(1).審判員結果(jg).ジャッジ記号).ジャッジ所属
        Next jg


        'SC_Total_Startlistの作成

        Dim SC_Total_Startlist
        ReDim SC_Total_Startlist(採点結果_V2.出場選手数 + 1, 6)

        '１行目

        SC_Total_Startlist(1, 1) = 採点結果_V2.出場選手数

        Dim 種目記号リスト() = Nothing
        Dim 種目数 = 採点結果_V2.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        For i = 1 To 種目数
            SC_Total_Startlist(1, i + 1) = 種目記号リスト(i)
        Next i

        '２行目 以降　選手リスト

        Dim 区分 As B_区分 = 採点結果_V2.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        For s = 1 To 採点結果_V2.出場選手数
            Dim 背番号 As String = 採点結果_V2.背番号(s)
            '選手Classを取得
            Dim 選手 As 選手 = 採点結果_V2.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            SC_Total_Startlist(s + 1, 1) = s
            SC_Total_Startlist(s + 1, 2) = 採点結果_V2.背番号(s)
            SC_Total_Startlist(s + 1, 3) = 選手.リーダー表記名
            SC_Total_Startlist(s + 1, 4) = 選手.パートナ表記名
            SC_Total_Startlist(s + 1, 5) = 選手.カップル所属名

        Next s

        'SC_Total_Resultの作成

        Dim SC_Total_Result
        ReDim SC_Total_Result(採点結果_V2.出場選手数 + 1, 11)

        Dim SC_Total_Result_Temp
        ReDim SC_Total_Result_Temp(採点結果_V2.出場選手数 + 1, 11)

        '１行目

        SC_Total_Result(1, 1) = 採点結果_V2.出場選手数

        For i = 1 To 種目数
            SC_Total_Result(1, i + 1) = 種目記号リスト(i)
        Next i

        '２行目

        For s = 1 To 採点結果_V2.出場選手数

            Dim 背番号 As String = 採点結果_V2.背番号(s)
            '選手Classを取得
            Dim 選手 As 選手 = 採点結果_V2.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)


            SC_Total_Result_Temp(s + 1, 1) = 採点結果_V2.総合順位表記(s)
            SC_Total_Result_Temp(s + 1, 2) = 採点結果_V2.背番号(s)
            SC_Total_Result_Temp(s + 1, 3) = 選手.リーダー表記名
            SC_Total_Result_Temp(s + 1, 4) = 選手.パートナ表記名
            SC_Total_Result_Temp(s + 1, 5) = 選手.カップル所属名
            SC_Total_Result_Temp(s + 1, 6) = 採点結果_V2.総合得点(s)

            For d = 1 To 種目数
                SC_Total_Result_Temp(s + 1, 6 + d) = 採点結果_V2.種目(d).選手結果(s).種目得点.ToString("0.000")
            Next d


        Next s

        'ソート SC_Total_Result_Temp　を順位順にソート 
        Dim j, r
        j = 1
        Dim 順位 = 1
        For 順位 = 1 To 採点結果_V2.出場選手数
            For s = 1 To 採点結果_V2.出場選手数
                If SC_Total_Result_Temp(s + 1, 1) = 順位 Then
                    For r = 1 To 11
                        SC_Total_Result(j + 1, r) = SC_Total_Result_Temp(s + 1, r)
                    Next r
                    j = j + 1
                End If
            Next s
        Next 順位

        '====


        Call SCファイル書出し("SC_COMP.csv", 採点結果_V2.マスタデータ.Z_システム設定.システムPath, SC_COMP)
        Call SCファイル書出し("SC_Total_Startlist.csv", 採点結果_V2.マスタデータ.Z_システム設定.システムPath, SC_Total_Startlist)
        Call SCファイル書出し("SC_Total_Result.csv", 採点結果_V2.マスタデータ.Z_システム設定.システムPath, SC_Total_Result)

        Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_COMP.csv", 採点結果_V2.マスタデータ.Z_システム設定.Comp_filepath, SC_COMP)
        Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_Total_Startlist.csv", 採点結果_V2.マスタデータ.Z_システム設定.Comp_filepath, SC_Total_Startlist)
        Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_Total_Result.csv", 採点結果_V2.マスタデータ.Z_システム設定.Comp_filepath, SC_Total_Result)



    End Sub

    Private Sub SCファイル作成_種目別_V2(ByVal 種目番号 As Integer, ByVal 種目 As D_種目)　　　'PDJ用

        'SC_StartList と SC_Result を作成


        Dim SC_StartList
        Dim SC_Result
        ReDim SC_StartList(採点結果_V2.出場選手数 + 1, 5)
        ReDim SC_Result(採点結果_V2.出場選手数 + 1, 14)

        'SC_StartListの1行目

        If 採点結果_V2.マスタデータ.Z_システム設定.言語 = "E" Then

            SC_StartList(1, 1) = 採点結果_V2.出場選手数
            SC_StartList(1, 2) = 採点結果_V2.マスタデータ.Z_システム設定.Get_種目名称(種目.種目記号).種目名_E    '種目名
            SC_StartList(1, 3) = 種目.SG種別  'ソロ・グループ区分

            SC_Result(1, 1) = 採点結果_V2.出場選手数
            SC_Result(1, 2) = 採点結果_V2.マスタデータ.Z_システム設定.Get_種目名称(種目.種目記号).種目名_E    '種目名
            SC_Result(1, 3) = 種目.SG種別    'ソロ・グループ区分

        Else

            SC_StartList(1, 1) = 採点結果_V2.出場選手数
            SC_StartList(1, 2) = 採点結果_V2.マスタデータ.Z_システム設定.Get_種目名称(種目.種目記号).種目名_J    '種目名
            SC_StartList(1, 3) = 種目.SG種別  'ソロ・グループ区分

            SC_Result(1, 1) = 採点結果_V2.出場選手数
            SC_Result(1, 2) = 採点結果_V2.マスタデータ.Z_システム設定.Get_種目名称(種目.種目記号).種目名_J     '種目名
            SC_Result(1, 3) = 種目.SG種別    'ソロ・グループ区分
        End If


        'PDの時は、種目に関わらず課題数を３にしたい。GDの時は０としたい。
        Dim ソロ種目記号 As String = ""
        For d = 1 To 採点結果_V2.種目数
            Dim 種目_TEMP As D_種目 = 採点結果_V2.マスタデータ.D_種目マスタ.Get_種目Class(採点結果_V2.区分番号, 採点結果_V2.ラウンド番号, d)

            If 種目_TEMP.SG種別 = "S" Then
                ソロ種目記号 = 種目_TEMP.種目記号
            End If
        Next d


        'SC_StartListの登録 2行目以降

        Dim 区分 As B_区分 = 採点結果_V2.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ


        For i = 1 To 採点結果_V2.出場選手数


            Dim 背番号 As String = 採点結果_V2.種目(種目番号).選手結果(i).背番号

            '選手Classを取得
            Dim 選手 As 選手 = 採点結果_V2.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            Dim リーダ名 = 選手.リーダー表記名
            Dim パートナー名 = 選手.パートナ表記名
            Dim 所属 = 選手.カップル所属名
            Dim ヒート番号 = 採点結果_V2.マスタデータ.E_ヒート表マスタ.Get_ヒート番号(種目番号, 背番号)
            Dim 合計点 = 採点結果_V2.種目(種目番号).選手結果(i).種目得点.ToString("0.000")
            Dim 順位 = 採点結果_V2.種目(種目番号).選手結果(i).種目順位表記


            'TES合計点
            Dim TES得点 As Decimal = 0
            For k = 1 To 採点結果_V2.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)

                If 採点結果_V2.種目(種目番号).選手結果(i).TES得点(k).TES_Base > 0 Then

                    TES得点 = TES得点 + 採点結果_V2.種目(種目番号).選手結果(i).TES得点(k).TES_Base

                    For fp = 1 To 採点結果_V2.マスタデータ.J_新審判設定.GetTES減点数
                        TES得点 = TES得点 + 採点結果_V2.種目(種目番号).選手結果(i).TES得点(k).TES減点(fp).TES減点
                    Next fp

                End If

                TES得点 = TES得点 + 採点結果_V2.種目(種目番号).選手結果(i).GOE得点(k).GOE得点
            Next k



            'SC_StartListの登録

            If 種目.SG種別 = "S" Then
                'ソロのときは演技順
                SC_StartList(ヒート番号 + 1, 1) = ヒート番号
                SC_StartList(ヒート番号 + 1, 2) = 背番号
                SC_StartList(ヒート番号 + 1, 3) = リーダ名
                SC_StartList(ヒート番号 + 1, 4) = パートナー名
                SC_StartList(ヒート番号 + 1, 5) = 所属
            ElseIf 種目.SG種別 = "G" Then
                '全員競技の時は背番号順
                SC_StartList(i + 1, 1) = ヒート番号
                SC_StartList(i + 1, 2) = 背番号
                SC_StartList(i + 1, 3) = リーダ名
                SC_StartList(i + 1, 4) = パートナー名
                SC_StartList(i + 1, 5) = 所属
            Else
                'マッチ(Duel)の時 背番号順
                SC_StartList(i + 1, 1) = ヒート番号
                SC_StartList(i + 1, 2) = 背番号
                SC_StartList(i + 1, 3) = リーダ名
                SC_StartList(i + 1, 4) = パートナー名
                SC_StartList(i + 1, 5) = 所属

            End If

            'SC_Resultの登録
            Dim 順位番号 As Integer = 採点結果_V2.種目(種目番号).選手結果(i).種目順位番号

            SC_Result(順位番号 + 1, 1) = ヒート番号
            SC_Result(順位番号 + 1, 2) = 順位
            SC_Result(順位番号 + 1, 3) = 背番号
            SC_Result(順位番号 + 1, 4) = リーダ名
            SC_Result(順位番号 + 1, 5) = パートナー名
            SC_Result(順位番号 + 1, 6) = 所属
            SC_Result(順位番号 + 1, 7) = 合計点
            SC_Result(順位番号 + 1, 8) = Format(採点結果_V2.種目(種目番号).選手結果(i).PCS得点(1).PCS得点, "#.##0")
            SC_Result(順位番号 + 1, 9) = Format(採点結果_V2.種目(種目番号).選手結果(i).PCS得点(2).PCS得点, "#.##0")
            SC_Result(順位番号 + 1, 10) = Format(採点結果_V2.種目(種目番号).選手結果(i).PCS得点(3).PCS得点, "#.##0")
            SC_Result(順位番号 + 1, 11) = Format(採点結果_V2.種目(種目番号).選手結果(i).PCS得点(4).PCS得点, "#.##0")


            '==減点項目名の表示
            Dim 減点Text As String = ""
            Dim 減点数 As Integer = 0
            Dim 減点合計点 As Double = 0

            Dim 減点番号 As Integer = 0



            For r = 1 To 採点結果_V2.マスタデータ.J_新審判設定.Get減点項目数

                If 採点結果_V2.マスタデータ.J_新審判設定.減点設定(r).SGM種別.Contains(種目.SG種別) Then
                    減点番号 = 減点番号 + 1


                    If 採点結果_V2.種目(種目番号).選手結果(i).一般減点(減点番号).一般減点 <> 0 Then

                        '減点の集計
                        If 採点結果_V2.種目(種目番号).選手結果(i).一般減点(減点番号).一般減点 < 0 Then
                            減点合計点 = 減点合計点 + 採点結果_V2.種目(種目番号).選手結果(i).一般減点(減点番号).一般減点
                        Else
                            減点合計点 = 0
                        End If

                        '減点テキスト
                        If 減点数 = 2 Then
                            減点Text = 減点Text & "etc"

                        ElseIf 減点数 >= 3 Then

                        Else
                            減点Text = 減点Text & "," & 採点結果_V2.マスタデータ.J_新審判設定.減点設定(r).減点項目名

                        End If

                        減点数 = 減点数 + 1

                    End If
                End If

            Next r





            'If 採点結果_V2.種目(種目番号).選手結果(i).失格FLAG = True Then
            '減点Text = "Lost(失格)"
            'Else
            'For r = 1 To 採点結果_V2.マスタデータ.J_新審判設定.Get減点項目数
            'If 採点結果_V2.種目(種目番号).選手結果(i).一般減点(r).一般減点 <> 0 Then
            '
            '減点合計点 = 減点合計点 + 採点結果_V2.種目(種目番号).選手結果(i).一般減点(r).一般減点

            '減点数 = 減点数 + 1
            '
            '            If 減点数 = 1 Then

            '            Dim rrr = 1
            '           For rr = 2 To 採点結果_V2.マスタデータ.J_新審判設定.Get減点項目数
            '          If 採点結果_V2.マスタデータ.J_新審判設定.減点設定(rr).SGM種別.Contains(種目.SG種別) Then
            '         rrr = rrr + 1

            '         If rrr = r Then
            '        減点Text = 採点結果_V2.マスタデータ.J_新審判設定.減点設定(rr).減点項目名

            '        rr = 採点結果_V2.マスタデータ.J_新審判設定.Get減点項目数
            '       End If
            '            End If
            '
            '       Next rr

            '           ElseIf 減点数 = 2 Then

            '           減点Text = 減点Text & "etc"

            '           End If

            '           End If
            '      Next r

            '          End If


            SC_Result(順位番号 + 1, 12) = 減点合計点

            SC_Result(順位番号 + 1, 13) = 減点Text


            SC_Result(順位番号 + 1, 14) = TES得点.ToString("0.000")

        Next i


        'SC_Fileの書き出し
        Call SCファイル書出し("SC_0" & 種目番号 & "_Startlist.csv", 採点結果_V2.マスタデータ.Z_システム設定.システムPath, SC_StartList)
        Call SCファイル書出し("SC_0" & 種目番号 & "_Result.csv", 採点結果_V2.マスタデータ.Z_システム設定.システムPath, SC_Result)


        Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_0" & 種目番号 & "_Startlist.csv", 採点結果_V2.マスタデータ.Z_システム設定.Comp_filepath, SC_StartList)
        Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_0" & 種目番号 & "_Result.csv", 採点結果_V2.マスタデータ.Z_システム設定.Comp_filepath, SC_Result)

    End Sub



    Private Sub SCファイル作成_ブレイキン()

        '総合結果を更新
        採点結果.総合結果更新()

        If 採点結果.出場選手数 > 2 Then
            'ブレイキンの対戦は2人　それ以上の場合はエラー
            MsgBox("ソロ・グループ種別を確認してください。ブレイキンの「対戦」になっていますが、「ソロ」では無いですか？" & 採点結果.出場選手数 & "名の出場者になっています。"）
        End If




        '****SC_B_Resultの作成　詳細結果

        'Dim SC_B_Result(3, 62)
        'Dim SC_B_Result(3, 80)  '4ラウンド分に変更
        Dim SC_B_Result(3, 98)  '5ラウンド分に変更

        Dim カテゴリ番号 = 採点結果.マスタデータ.BR_グループマスタ.Getカテゴリ番号(採点結果.区分番号, 採点結果.ラウンド番号)

        If カテゴリ番号 = "" Then
            MsgBox("区分番号「" & 採点結果.区分番号 & "」 ラウンド番号「" & 採点結果.ラウンド番号 & "」のカテゴリ番号が設定されていません。")
            Exit Sub
        End If

        Dim 種目記号リスト() = Nothing

        SC_B_Result(1, 1) = 採点結果.マスタデータ.A_競技会マスタ.競技会名　　　　　　　　　　'競技会名
        SC_B_Result(1, 2) = 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(区分番号)　　 　'区分名
        SC_B_Result(1, 3) = 採点結果.マスタデータ.BR_カテゴリマスタ.Getカテゴリ表記名(カテゴリ番号)　　　 'カテゴリ名
        SC_B_Result(1, 4) = 採点結果.マスタデータ.Get_ラウンド名(ラウンド番号)    　　　　　 'ラウンド名　
        SC_B_Result(1, 5) = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)   '全ラウンド数

        '終了ラウンド数（種目数）の検索

        Dim 採点進行 As T_採点進行
        採点進行 = 採点結果.マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

        Dim 競技番号 As String = 採点進行.競技番号
        Dim 競技番号枝番 As String = 採点進行.競技番号枝番

        採点進行 = Nothing

        採点結果.マスタデータ.U_進行管理.FileRead()

        Dim 終了ラウンド数 As Integer = 0
        For 種目順 = 1 To 4　　　'ここはなぜ４？　種目数じゃなくていいんだっけ？
            Dim 進行 As U_進行
            進行 = 採点結果.マスタデータ.U_進行管理.Get_進行(競技番号, 競技番号枝番, 種目順, 1)
            If 進行 IsNot Nothing Then
                If 進行.ステータス = "全審判送信済み" Then
                    終了ラウンド数 = 終了ラウンド数 + 1
                End If
            End If
        Next 種目順

        SC_B_Result(1, 6) = 終了ラウンド数              '終了ラウンド数
        SC_B_Result(1, 7) = 採点結果.マスタデータ.J_新審判設定.勝敗方式   '勝敗方式 P:ポイント制　R:ラウンド制
        SC_B_Result(1, 8) = 採点結果.マスタデータ.J_新審判設定.勝敗ラウンド数  '勝敗ラウンド数 全ラウンド数


        '2行目から
        Dim 区分 As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        '赤・青反転のケースを探す。
        Dim 赤青反転FLAG As Boolean = False
        If 採点結果.マスタデータ.E_ヒート表マスタ.Get_ヒート番号(1, 採点結果.背番号(1)) = 2 Then
            赤青反転FLAG = True
        Else

        End If



        For s = 1 To 採点結果.出場選手数

            Dim 背番号 As String = 採点結果.背番号(s)
            '選手Classを取得
            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号)

            Dim h As Integer = 0
            If 赤青反転FLAG = True Then
                If s = 1 Then
                    h = 2
                Else
                    h = 1
                End If
            Else
                h = s
            End If


            SC_B_Result(h + 1, 1) = h   '演技順
            'SC_B_Result(h + 1, 1) = 採点結果.マスタデータ.E_ヒート表マスタ.Get_ヒート番号(1, 背番号)  '演技順　ー＞１Rのヒート番号

            SC_B_Result(h + 1, 2) = 採点結果.総合順位表記(s)  '順位
            SC_B_Result(h + 1, 3) = 背番号 　　　'背番号
            SC_B_Result(h + 1, 4) = 選手.リーダー表記名 'リーダー名
            SC_B_Result(h + 1, 5) = 選手.パートナ表記名 'パートナー名
            SC_B_Result(h + 1, 6) = 選手.カップル所属名  '所属

            If 採点結果.マスタデータ.J_新審判設定.勝敗方式 = "P" Then    '勝敗方式 P:ポイント制　R:ラウンド制
                SC_B_Result(h + 1, 7) = 採点結果.勝ちFLAG(s) '勝ちFLAG
            Else
                SC_B_Result(h + 1, 7) = 採点結果.WIN数(s) 'WIN数
            End If


            SC_B_Result(h + 1, 8) = 採点結果.総合得点(s)  '総合得点

            For r = 1 To 採点結果.種目数   'ラウンド(種目のこと）

                SC_B_Result(h + 1, 9 + 18 * (r - 1)) = 採点結果.種目(r).選手(s).種目得点  'Total 得点
                For PCS = 1 To 5
                    SC_B_Result(h + 1, 9 + 18 * (r - 1) + PCS) = 採点結果.種目(r).選手(s).種目各PCS得点(PCS)   'PCS１－５
                Next PCS
                For 減点 = 1 To 6
                    If 採点結果.マスタデータ.J_新審判設定.減点設定(減点) IsNot Nothing Then

                        SC_B_Result(h + 1, 15 + 18 * (r - 1) + 2 * (減点 - 1)) = 採点結果.マスタデータ.J_新審判設定.減点設定(減点).減点項目名   '減点項目名１－６
                        SC_B_Result(h + 1, 16 + 18 * (r - 1) + 2 * (減点 - 1)) = 採点結果.種目(r).選手(s).種目各減点(減点)   '減点１－６
                    End If

                Next 減点
            Next r

        Next s



        '******SC_BG_Startlist の作成

        Dim SC_BG_Startlist
        ReDim SC_BG_Startlist(33, 8)

        '1行目
        SC_BG_Startlist(1, 1) = 採点結果.マスタデータ.A_競技会マスタ.競技会名
        SC_BG_Startlist(1, 2) = 採点結果.マスタデータ.B_区分マスタ.Get区分表記名(区分番号)     '区分名

        '2行目以降は選手リスト

        Dim 初ラウンド番号 As String = ""
        初ラウンド番号 = 採点結果.マスタデータ.BR_グループマスタ.Get_初ラウンド番号(カテゴリ番号)

        Dim i As Integer = 1
        For kk = 1 To 採点結果.マスタデータ.BR_グループマスタ.登録済みレコード数
            If 採点結果.マスタデータ.BR_グループマスタ.リスト(kk).カテゴリ番号 = カテゴリ番号 And
                  採点結果.マスタデータ.BR_グループマスタ.リスト(kk).ラウンド番号 = 初ラウンド番号 Then



                Dim 背番号リスト() As String = Nothing
                採点結果.マスタデータ.E_ヒート表マスタ.Read(採点結果.マスタデータ.BR_グループマスタ.リスト(kk).区分番号, 初ラウンド番号)

                採点結果.マスタデータ.E_ヒート表マスタ.Get_背番号リスト("1", 0, 背番号リスト)     '2021/7/16 ヒート番号を１－＞０（全員分）に変更

                If 背番号リスト Is Nothing Then
                    MsgBox("区分番号:" & 採点結果.マスタデータ.BR_グループマスタ.リスト(kk).区分番号 & " ラウンド番号:" & 初ラウンド番号 & "のヒート表が作成されていません。"）
                    Exit Sub
                End If


                Dim 選手番号 As Integer = 0
                For ss = 1 To UBound(背番号リスト)

                    If i > 33 Then
                        MsgBox("ブレイキンカテゴリの最大登録選手数(33人)を超えています。カテゴリを分けて登録してください。対象カテゴリ:" & 採点結果.マスタデータ.BR_カテゴリマスタ.Getカテゴリ表記名(カテゴリ番号))

                    End If


                    SC_BG_Startlist(i, 1) = カテゴリ番号
                    SC_BG_Startlist(i, 2) = 採点結果.マスタデータ.BR_カテゴリマスタ.Getカテゴリ表記名(カテゴリ番号)    'カテゴリ名
                    SC_BG_Startlist(i, 3) = 採点結果.マスタデータ.BR_グループマスタ.リスト(kk).区分番号

                    選手番号 = 選手番号 + 1
                    SC_BG_Startlist(i, 4) = 選手番号    '選手番号
                    SC_BG_Startlist(i, 5) = 背番号リスト(ss)

                    Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号リスト(ss))
                    SC_BG_Startlist(i, 6) = 選手.リーダー表記名 'リーダー名
                    SC_BG_Startlist(i, 7) = 選手.パートナ表記名 'パートナー名
                    SC_BG_Startlist(i, 8) = 選手.カップル所属名  '所属

                    i = i + 1

                Next ss

            End If
        Next kk



        '******SC_BR_Result の作成
        Dim SC_BR As SC_BR_Result

        SC_BR = New SC_BR_Result

        SC_BR.カテゴリ番号 = カテゴリ番号
        SC_BR.カテゴリ名 = 採点結果.マスタデータ.BR_カテゴリマスタ.Getカテゴリ表記名(カテゴリ番号)
        SC_BR.区分番号 = 区分番号
        SC_BR.ラウンド番号 = ラウンド番号

        '勝者判定
        Dim 勝者背番号 As String = "0"
        If 採点結果.マスタデータ.J_新審判設定.勝敗方式 = "R" Then
            'ラウンド制　★要再検討　’ラウンドの時は、WIN数で判定。同点の時は、総合点で判定。
            '           Win数が2以上でかつWIN数が多い方が勝ち

            'MAX_WIN数を算出
            Dim MAX_WIN数 As Integer = 0
            For s = 1 To 採点結果.出場選手数
                If 採点結果.WIN数(s) > MAX_WIN数 Then
                    MAX_WIN数 = 採点結果.WIN数(s)
                End If
            Next s

            '同点人数を算出

            If MAX_WIN数 >= 2 Then
                Dim MAX_Win数の人数 As Integer = 0
                For s = 1 To 採点結果.出場選手数
                    If 採点結果.WIN数(s) = MAX_WIN数 Then
                        '勝者背番号 = 採点結果.背番号(s)
                        MAX_Win数の人数 = MAX_Win数の人数 + 1
                    End If
                Next s

                If MAX_Win数の人数 = 1 Then '勝者が決まった場合
                    For s = 1 To 採点結果.出場選手数
                        If 採点結果.WIN数(s) = MAX_WIN数 Then
                            勝者背番号 = 採点結果.背番号(s)
                        End If
                    Next s
                End If

            End If






            '総合得点は見ない

            'If Win2の人数 >= 2 Then
            '同点があった時
            'Dim 勝者点数 As Double = 0
            'For s = 1 To 採点結果.出場選手数
            'If 勝者点数 < 採点結果.総合得点(s) Then
            '勝者点数 = 採点結果.総合得点(s)
            'End If
            '   Next s

            'For s = 1 To 採点結果.出場選手数
            'If 勝者点数 = 採点結果.総合得点(s) Then
            '勝者背番号 = 採点結果.背番号(s)
            '
            'ただ、総合点数まで同じだった場合は、判定できない。
            ' End If
            '    Next s
            'End If

        Else
            'ポイント制、総合点で判定。勝ちFLAGで判定

            For s = 1 To 採点結果.出場選手数
                If 採点結果.勝ちFLAG(s) = 1 Then
                    勝者背番号 = 採点結果.背番号(s)
                End If
            Next s


            'Dim 勝者点数 As Double = 0
            'For s = 1 To 採点結果.出場選手数
            ' If 勝者点数 < 採点結果.総合得点(s) Then
            ' 勝者点数 = 採点結果.総合得点(s)
            'End If
            '   Next s

            'For s = 1 To 採点結果.出場選手数
            ' If 勝者点数 = 採点結果.総合得点(s) And 勝者点数 > 0 Then
            '勝者背番号 = 採点結果.背番号(s)
            '
            'ただ、総合点数が同点だった場合は、判定できない。
            'End If
            '   Next s
        End If




        If 勝者背番号 = "0" Then

        Else
            SC_BR.勝者背番号 = 勝者背番号


            Dim 勝者選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 勝者背番号)

            SC_BR.勝者リーダー名 = 勝者選手.リーダー表記名
            SC_BR.勝者パートナー名 = 勝者選手.パートナ表記名
            SC_BR.勝者所属 = 勝者選手.カップル所属名

        End If


        Dim 勝者点数結果 As Double = 0
        Dim 勝者WIN数 As Integer = 0
        Dim 敗者点数結果 As Double = 0
        Dim 敗者WIN数 As Integer = 0

        For s = 1 To 採点結果.出場選手数
            If 採点結果.背番号(s) = 勝者背番号 Then
                勝者点数結果 = 採点結果.総合得点(s)
                勝者WIN数 = 採点結果.WIN数(s)
            Else
                敗者点数結果 = 採点結果.総合得点(s)
                敗者WIN数 = 採点結果.WIN数(s)
            End If
        Next s

        SC_BR.勝者点数 = 勝者点数結果
        SC_BR.勝者WIN数 = 勝者WIN数
        SC_BR.敗者点数 = 敗者点数結果
        SC_BR.敗者WIN数 = 敗者WIN数

        If 勝者背番号 = "0" Then

        Else
            Dim SC_BR_Resultマスタ As SC_BR_Resultマスタ
            SC_BR_Resultマスタ = New SC_BR_Resultマスタ(採点結果.マスタデータ.Z_システム設定.システムPath)
            SC_BR_Resultマスタ.登録(SC_BR)
            SC_BR_Resultマスタ = Nothing
        End If

        '====


        Call SCファイル書出し("SC_B_Result.csv", 採点結果.マスタデータ.Z_システム設定.システムPath, SC_B_Result)
        Call SCファイル書出し("SC_BG_Startlist_" & カテゴリ番号 & ".csv", 採点結果.マスタデータ.Z_システム設定.システムPath, SC_BG_Startlist)


        'Call SCファイル書出し("SC_Total_Result.csv", 採点結果.マスタデータ.Z_システム設定.システムPath, SC_Total_Result)

        'Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_COMP.csv", 採点結果.マスタデータ.Z_システム設定.Comp_filepath, SC_COMP)
        'Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_Total_Startlist.csv", 採点結果.マスタデータ.Z_システム設定.Comp_filepath, SC_Total_Startlist)
        'Call SCファイル書出し("SC_" & 区分番号 & "_" & ラウンド番号 & "_Total_Result.csv", 採点結果.マスタデータ.Z_システム設定.Comp_filepath, SC_Total_Result)



    End Sub





    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim AJS得点詳細 As AJS得点詳細
        AJS得点詳細 = New AJS得点詳細
        AJS得点詳細.CreateHTML(採点結果, "LOCAL")
        AJS得点詳細.CreateHTML(採点結果, "REMOTE")

    End Sub

    Public Event 結果決定(ByVal sender As Object, ByVal e As 結果決定EventArgs)
    Public Event Push確定ボタン(ByVal sender As Object, ByVal e As 結果決定EventArgs)

    Private Sub PB_UP数確定_Click(sender As Object, e As EventArgs) Handles PB_UP数確定.Click

        If MsgBox("本当に結果を確定して良いですか？", vbYesNo) = vbNo Then
            Exit Sub
        End If



        Dim 採点方式 As String ' = 採点結果.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        If 採点結果 IsNot Nothing Then
            採点方式 = 採点結果.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)
        Else
            採点方式 = 採点結果_V2.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        End If



        If Strings.Left(採点方式, 3) = "BJS" Then

            Dim カテゴリ番号 = 採点結果.マスタデータ.BR_グループマスタ.Getカテゴリ番号(採点結果.区分番号, 採点結果.ラウンド番号)

            If 採点結果.マスタデータ.BR_カテゴリマスタ.Getラウンド方式名(カテゴリ番号) = "S" Then
                'ソロ競技の時

                Dim F510 As F510_UP数確定
                F510 = New F510_UP数確定

                F510.設定(区分番号, ラウンド番号, 採点結果)
                F510.ShowDialog()


                '次ラウンドが「ヒート表作成済み」となっていたら、現ラウンドは「採点済み」に変更する。

                Dim マスタデータ As マスタデータ
                マスタデータ = New マスタデータ

                Dim 次ラウンドC As C_ラウンド = マスタデータ.C_ラウンドマスタ.Get_次ラウンドClass(区分番号, ラウンド番号)
                Dim 次ラウンド番号 As String = 次ラウンドC.ラウンド番号


                Dim 次採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, 次ラウンド番号)

                If 次採点進行C.ステータス = "ヒート表作成済み" Then

                    Dim 現採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

                    現採点進行C.ステータス = "採点済み"
                    マスタデータ.T_採点進行管理.登録(現採点進行C)

                End If


                マスタデータ = Nothing


            Else
                'トーナメント方式の時 ラウンド方式＝「T」
                'ブレイキンの時は、採点済みにして、次ラウンドのヒート表、PCS表を作成する。次ラウンドヒートに2組以上居れば、ヒート表作成済みに変更する。


                Dim マスタデータ As マスタデータ
                マスタデータ = New マスタデータ

                '勝者・敗者を確定
                Dim 勝者背番号 As String = "0"
                Dim 敗者背番号 As String = "0"

                For s = 1 To 採点結果.出場選手数
                    If 採点結果.勝ちFLAG(s) = 1 Then
                        勝者背番号 = 採点結果.背番号(s)
                    End If
                Next s

                If 勝者背番号 <> "0" Then
                    '結果が出た時
                    For s = 1 To 採点結果.出場選手数
                        If 採点結果.勝ちFLAG(s) = 0 Then
                            敗者背番号 = 採点結果.背番号(s)
                        End If
                    Next s

                    Dim 採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

                    採点進行C.ステータス = "採点済み"
                    マスタデータ.T_採点進行管理.登録(採点進行C)

                    MsgBox("勝者は " & 勝者背番号 & " 番。ステータスを「採点済み」に変更しました")

                ElseIf 採点結果.マスタデータ.J_新審判設定.勝敗ラウンド数 = 2 Then
                    '同点とするとき。リーグ戦（2ラウンド制）

                    If MsgBox("同点です。同点として終了して良いですか？", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                        '同点として処理を終了する
                        '同点なので、次ヒートは作成しない。

                        Dim 採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

                        採点進行C.ステータス = "採点済み"
                        マスタデータ.T_採点進行管理.登録(採点進行C)

                        MsgBox("同点として、ステータスを「採点済み」に変更しました")
                        Exit Sub


                    Else
                        '同点では処理終了しない。
                        Exit Sub

                    End If



                Else

                    MsgBox("結果が確定していません。")
                    Exit Sub    '2020/10/04 追加。
                End If



                '次のヒート票に登録
                Dim 次勝ラウンド番号 As String = ""
                Dim 次勝区分番号 = マスタデータ.BR_グループマスタ.Get勝者区分ラウンド(区分番号, ラウンド番号, 次勝ラウンド番号)

                If 次勝区分番号 <> "" And 次勝ラウンド番号 <> "" Then
                    Dim ヒートC As E_ヒート表
                    ヒートC = New E_ヒート表

                    ヒートC.背番号 = 勝者背番号

                    Dim 種目記号リスト() = Nothing
                    Dim 次種目数 = マスタデータ.D_種目マスタ.Get_種目数(次勝区分番号, 次勝ラウンド番号, 種目記号リスト)

                    For d = 1 To 次種目数
                        ヒートC.ヒート番号(d) = 1
                    Next d

                    マスタデータ.E_ヒート表マスタ.Read(次勝区分番号, 次勝ラウンド番号)
                    マスタデータ.E_ヒート表マスタ.登録(ヒートC, 次勝区分番号, 次勝ラウンド番号)

                    'PCS担当表を作成

                    '古いPCS表を消す
                    マスタデータ.F_審判担当PCSマスタ.Deleteレコード(次勝区分番号, 次勝ラウンド番号)

                    Dim 審判G As Integer = マスタデータ.C_ラウンドマスタ.Get担当審判グループ(次勝区分番号, 次勝ラウンド番号)

                    For j = 1 To マスタデータ.審判員マスタ.登録済み審判員数
                        If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判G) = "1" Or マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判G) = "L" Then

                            Dim PCS_C As F_審判担当PCS
                            PCS_C = New F_審判担当PCS

                            PCS_C.ジャッジ記号 = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ記号


                            If 採点結果.マスタデータ.J_新審判設定.GetPCS数 = 3 Then

                                For d = 1 To 次種目数
                                    PCS_C.担当PCS番号(d) = "123"
                                Next d

                            ElseIf 採点結果.マスタデータ.J_新審判設定.GetPCS数 = 4 Then


                                For d = 1 To 次種目数
                                    PCS_C.担当PCS番号(d) = "1234"
                                Next d

                            End If



                            '区分番号とラウンド番号を渡すだけ　読込みは失敗するはず
                            マスタデータ.F_審判担当PCSマスタ.Read(次勝区分番号, 次勝ラウンド番号)

                            If マスタデータ.F_審判担当PCSマスタ.登録(PCS_C) = 0 Then

                            Else
                                MsgBox(次勝区分番号 & " " & 次勝ラウンド番号 & "のPCS担当表の作成に失敗しました。")
                            End If
                        End If
                    Next j



                    'ヒートに2組以上いたら、ヒート表作成済みに変更
                    If マスタデータ.E_ヒート表マスタ.登録済みレコード数 >= 2 Then
                        '進行マスターをヒート表作成済みに変更
                        Dim 採点進行 As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(次勝区分番号, 次勝ラウンド番号)
                        採点進行.ステータス = "ヒート表作成済み"
                        マスタデータ.T_採点進行管理.登録(採点進行)
                    End If
                End If


                Dim 次負ラウンド番号 As String = ""
                Dim 次負区分番号 = マスタデータ.BR_グループマスタ.Get敗者区分ラウンド(区分番号, ラウンド番号, 次負ラウンド番号)

                If 次負区分番号 <> "" And 次負ラウンド番号 <> "" Then
                    Dim ヒートC As E_ヒート表
                    ヒートC = New E_ヒート表

                    ヒートC.背番号 = 敗者背番号

                    Dim 種目記号リスト() = Nothing
                    Dim 次種目数 = マスタデータ.D_種目マスタ.Get_種目数(次負区分番号, 次負ラウンド番号, 種目記号リスト)

                    For d = 1 To 次種目数
                        ヒートC.ヒート番号(d) = 1
                    Next d

                    マスタデータ.E_ヒート表マスタ.Read(次負区分番号, 次負ラウンド番号)
                    マスタデータ.E_ヒート表マスタ.登録(ヒートC, 次負区分番号, 次負ラウンド番号)


                    'PCS担当表を作成

                    '古いPCS表を消す
                    マスタデータ.F_審判担当PCSマスタ.Deleteレコード(次負区分番号, 次負ラウンド番号)

                    Dim 審判G As Integer = マスタデータ.C_ラウンドマスタ.Get担当審判グループ(次負区分番号, 次負ラウンド番号)

                    For j = 1 To マスタデータ.審判員マスタ.登録済み審判員数
                        If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判G) = "1" Or マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判G) = "L" Then

                            Dim PCS_C As F_審判担当PCS
                            PCS_C = New F_審判担当PCS

                            PCS_C.ジャッジ記号 = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ記号


                            If 採点結果.マスタデータ.J_新審判設定.GetPCS数 = 4 Then
                                For d = 1 To 次種目数
                                    PCS_C.担当PCS番号(d) = "1234"
                                Next d


                            ElseIf 採点結果.マスタデータ.J_新審判設定.GetPCS数 = 3 Then
                                For d = 1 To 次種目数
                                    PCS_C.担当PCS番号(d) = "123"
                                Next d

                            End If



                            '区分番号とラウンド番号を渡すだけ　読込みは失敗するはず
                            マスタデータ.F_審判担当PCSマスタ.Read(次負区分番号, 次負ラウンド番号)

                            If マスタデータ.F_審判担当PCSマスタ.登録(PCS_C) = 0 Then

                            Else
                                MsgBox(次負区分番号 & " " & 次負ラウンド番号 & "のPCS担当表の作成に失敗しました。")
                            End If
                        End If
                    Next j



                    'ヒートに2組以上いたら、ヒート表作成済みに変更
                    If マスタデータ.E_ヒート表マスタ.登録済みレコード数 >= 2 Then
                        '進行マスターをヒート表作成済みに変更
                        Dim 採点進行 As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(次負区分番号, 次負ラウンド番号)
                        採点進行.ステータス = "ヒート表作成済み"
                        マスタデータ.T_採点進行管理.登録(採点進行)
                    End If

                End If

                マスタデータ = Nothing

            End If

        ElseIf Strings.Left(採点方式, 4) = "BJPR" Then
            'ブレイキンプレセレクションの時

            Dim F514 = New F514_UP数確定_プレセレクション

            F514.設定(区分番号, ラウンド番号, 採点結果)
            F514.ShowDialog()


            'BJPRの時は、F514の画面の確定ボタンで　確定する

            'ステータス更新
            'Dim マスタデータ = New マスタデータ

            'Dim 現採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

            '現採点進行C.ステータス = "採点済み"
            'マスタデータ.T_採点進行管理.登録(現採点進行C)

            'マスタデータ = Nothing


        ElseIf ラウンド番号 = "400" Or ラウンド番号 = "300" Then
            '決勝、下位決勝のとき

            'T_採点進行管理を更新

            Dim マスタデータ As マスタデータ
            マスタデータ = New マスタデータ
            Dim 採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

            採点進行C.ステータス = "採点済み"
            マスタデータ.T_採点進行管理.登録(採点進行C)

            マスタデータ = Nothing

            Mファイル書出し()


            'イベント発生
            Dim e1 As 結果決定EventArgs
            e1 = New 結果決定EventArgs(区分番号, ラウンド番号)
            RaiseEvent 結果決定(Me, e1)



            If ラウンド番号 = "400" Then


                If Strings.Left(採点方式, 3) = "VAL" Then

                Else

                    '入賞者名簿を2枚印刷
                    Dim F300 = New F300_印刷画面
                    F300.設定(区分番号, ラウンド番号)
                    F300.入賞者_印刷_単票("司会")
                    F300.入賞者_印刷_単票("賞状")

                    If MsgBox("まとめ印刷を行いますか？", vbYesNo) = vbYes Then

                        If MsgBox("インターネットに結果をUPしますか？", vbYesNo) = vbYes Then
                            'まとめ印刷
                            F300.まとめ印刷(True)
                        Else
                            'まとめ印刷
                            F300.まとめ印刷(False)
                        End If

                    End If



                    F300 = Nothing
                End If

            End If

        Else

            Dim F510 As F510_UP数確定
            F510 = New F510_UP数確定

            If 採点結果 IsNot Nothing Then
                F510.設定(区分番号, ラウンド番号, 採点結果)

            Else
                F510.設定_V2(区分番号, ラウンド番号, 採点結果_V2)

            End If

            F510.ShowDialog()


            '次ラウンドが「ヒート表作成済み」となっていたら、現ラウンドは「採点済み」に変更する。

            Dim マスタデータ As マスタデータ
            マスタデータ = New マスタデータ

            Dim 次ラウンドC As C_ラウンド = マスタデータ.C_ラウンドマスタ.Get_次ラウンドClass(区分番号, ラウンド番号)

            If 次ラウンドC IsNot Nothing Then

                Dim 次ラウンド番号 As String = 次ラウンドC.ラウンド番号


                Dim 次採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, 次ラウンド番号)

                If 次採点進行C.ステータス = "ヒート表作成済み" Then

                    Dim 現採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

                    現採点進行C.ステータス = "採点済み"
                    マスタデータ.T_採点進行管理.登録(現採点進行C)

                End If

            Else
                '次ラウンドが定義されていない時


            End If


            マスタデータ = Nothing



        End If

        'イベント発生
        Dim e2 As 結果決定EventArgs
        e2 = New 結果決定EventArgs(区分番号, ラウンド番号)
        RaiseEvent Push確定ボタン(Me, e2)


    End Sub


    Private Sub SCファイル書出し(ByVal File名, ByVal パス名, ByVal データ)
        '===========================
        '概要　SC_ｘｘｘｘCSVファイルを作成する(SC_xxxxxx.csv)
        '入力　新File名,データ
        '出力　なし（CSVファイル）
        '===========================


        Dim NewFilename As String = パス名 & "\" & File名

        '_00_初期化()

        'ファイル書き出し
        Try
            Dim Writer As New IO.StreamWriter(NewFilename, False, System.Text.Encoding.GetEncoding("shift_jis"))

            Dim i, j

            For i = LBound(データ, 1) To UBound(データ, 1)
                For j = LBound(データ, 2) To UBound(データ, 2)
                    Writer.Write(データ(i, j))
                    Writer.Write(",")
                Next j
                Writer.WriteLine()
            Next i
            Writer.Close()
        Catch ex As Exception
            ' ファイル書き込みエラーは無視（処理を継続）
            Debug.WriteLine($"SCファイル書き出しエラー: {File名} - {ex.Message}")
        End Try

    End Sub


    Private Sub Mファイル書出し()
        '===========================
        '=概要：有田さんに依頼された順位ファイルの書き出し機能
        '   M_xx_7_01_zz.dat
        '       xx:区分番号　 　　
        '       zz:種目番号   01:Waltz 02:Tango... 06:Samba 07:ChaCha 10:Jive

        '       7: 決勝
        '       01: ジャッジ番号　--- 当システムは01で固定
        '===========================

        If ラウンド番号 = "400" Then

            Dim パス名 As String '= 採点結果.マスタデータ.Z_システム設定.システムPath



            If 採点結果 IsNot Nothing Then
                'パス名 = 採点結果.マスタデータ.Z_システム設定.システムPath

                If 採点結果.マスタデータ.Z_システム設定.Mファイルパス <> "" Then
                    パス名 = 採点結果.マスタデータ.Z_システム設定.Mファイルパス
                Else
                    パス名 = 採点結果.マスタデータ.Z_システム設定.システムPath
                End If

                For d = 1 To 採点結果.種目数

                    Dim File名 As String = "M_" & 区分番号 & "_7_01_" & 種目記号変換toJDSF種目番号(採点結果.種目記号(d)) & ".dat"

                    Dim NewFilename As String = パス名 & "\" & File名

                    'ファイル書き出し
                    Dim Writer As New IO.StreamWriter(NewFilename, False, System.Text.Encoding.GetEncoding("shift_jis"))

                    For s = 1 To 採点結果.出場選手数
                        Writer.WriteLine(採点結果.総合順位表記(s))

                    Next s

                    Writer.Close()


                Next d
            Else
                'パス名 = 採点結果_V2.マスタデータ.Z_システム設定.システムPath

                If 採点結果_V2.マスタデータ.Z_システム設定.Mファイルパス <> "" Then
                    パス名 = 採点結果_V2.マスタデータ.Z_システム設定.Mファイルパス
                Else
                    パス名 = 採点結果_V2.マスタデータ.Z_システム設定.システムPath
                End If


                For d = 1 To 採点結果_V2.種目数

                    Dim File名 As String = "M_" & 区分番号 & "_7_01_" & 種目記号変換toJDSF種目番号(採点結果_V2.種目記号(d)) & ".dat"

                    Dim NewFilename As String = パス名 & "\" & File名

                    'ファイル書き出し
                    Dim Writer As New IO.StreamWriter(NewFilename, False, System.Text.Encoding.GetEncoding("shift_jis"))

                    For s = 1 To 採点結果_V2.出場選手数
                        Writer.WriteLine(採点結果_V2.総合順位表記(s))

                    Next s

                    Writer.Close()


                Next d

            End If





            End If

    End Sub




    'タイマー処理
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'タイマー毎に呼ばれるイベント

        If LOGLEVEL >= 4 Then
            If Me.InvokeRequired Then
                Me.Invoke(LogDelegate, New Object() {"F501_Timer１から更新呼び出し", 4})
            Else
                WriteLog("F501_Timer１から更新呼び出し", 4)
            End If
        End If


        更新()

    End Sub

    Private Sub 自動更新開始()

        TB_ステータス.Text = "自動更新中"
        TB_ステータス.BackColor = Color.LightBlue

        Timer1.Interval = 6000   '6秒毎
        Timer1.Enabled = True

    End Sub

    Private Sub CB_自動更新_CheckedChanged(sender As Object, e As EventArgs) Handles CB_自動更新.CheckedChanged
        If CB_自動更新.Checked = True Then
            自動更新開始()
        Else
            自動更新終了()
        End If

    End Sub

    Private Sub 自動更新終了()

        TB_ステータス.Text = "自動更新停止中"
        TB_ステータス.BackColor = Color.Pink

        Timer1.Enabled = False
    End Sub



    Private Sub PB_Cali設定_Click(sender As Object, e As EventArgs) Handles PB_Cali設定.Click
        '設定されたキャリブレーションをファイルに書き出して、更新する

        Dim CaliMax As Double = CDbl(Me.TB_CaliMax.Text)
        Dim CaliMin As Double = CDbl(Me.TB_CaliMin.Text)

        Dim 種目記号リスト() = Nothing

        Dim 種目数 As Integer
        If 採点結果 IsNot Nothing Then

            種目数 = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)


            For d = 1 To 種目数
                Dim 種目 As D_種目 = 採点結果.マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, d)
                種目.CaliMax = CaliMax
                種目.CaliMin = CaliMin

                採点結果.マスタデータ.D_種目マスタ.登録(種目)

                種目 = Nothing
            Next d

            If LOGLEVEL >= 4 Then
                If Me.InvokeRequired Then
                    Me.Invoke(LogDelegate, New Object() {"F501_Cali設定ボタンから更新呼び出し", 4})
                Else
                    WriteLog("F501_Cali設定ボタンから更新呼び出し", 4)
                End If
            End If


            更新（）



        Else

            種目数 = 採点結果_V2.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)


            For d = 1 To 種目数
                Dim 種目 As D_種目 = 採点結果_V2.マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, d)
                種目.CaliMax = CaliMax
                種目.CaliMin = CaliMin

                採点結果_V2.マスタデータ.D_種目マスタ.登録(種目)

                種目 = Nothing
            Next d

            If LOGLEVEL >= 4 Then
                If Me.InvokeRequired Then
                    Me.Invoke(LogDelegate, New Object() {"F501_Cali設定ボタンから更新呼び出し", 4})
                Else
                    WriteLog("F501_Cali設定ボタンから更新呼び出し", 4)
                End If
            End If


            更新_V2（）



        End If





    End Sub


    'セルの色付け等
    Private Sub DGV_種目_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs)


        '1人のジャッジが担当するPCS数を計算

        Dim 担当PCS数 As Integer = 0

        Dim 採点方式 As String

        If 採点結果 IsNot Nothing Then

            採点方式 = 採点結果.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)


        ElseIf 採点結果_V2 IsNot Nothing Then

            採点方式 = 採点結果_V2.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        End If


        If Strings.Left(採点方式, 3) = "AJS" Then
            担当PCS数 = 2
        ElseIf Strings.Left(採点方式, 3) = "BJS" Then
            '担当PCS数 = 4
            担当PCS数 = 採点結果.マスタデータ.J_新審判設定.GetPCS数
        ElseIf Strings.Left(採点方式, 3) = "PDJ" Then
            担当PCS数 = 2
            '担当PCS数 = 採点結果_V2.マスタデータ.J_新審判設定.GetPCS数
        Else
            担当PCS数 = 1
        End If



        If CInt(Strings.Right(sender.name, 2)) = Me.TabControl_詳細.SelectedIndex + 1 Then

            Dim dgv As DataGridView = CType(sender, DataGridView)

            'Caliの値設定
            Dim CaliMin As Double = 0
            Dim CaliMax As Double = 0
            If Me.TB_CaliMin.Text = "" Then
                CaliMin = 0
            Else
                CaliMin = CDbl(Me.TB_CaliMin.Text)
            End If
            If Me.TB_CaliMax.Text = "" Then
                CaliMax = 0
            Else
                CaliMax = CDbl(Me.TB_CaliMax.Text)
            End If

            Dim ジャッジ人数 As Integer '= 採点結果.種目(CInt(Strings.Right(sender.name, 2))).審判員数

            If 採点結果 IsNot Nothing Then
                ジャッジ人数 = 採点結果.種目(CInt(Strings.Right(sender.name, 2))).審判員数

            ElseIf 採点結果_V2 IsNot Nothing Then

                ジャッジ人数 = 採点結果_V2.種目(CInt(Strings.Right(sender.name, 2))).審判員数

            End If


            If dgv.Columns(e.ColumnIndex).Name = "Ref" AndAlso TypeOf e.Value Is Double Then

                'レフリー列
                Dim val As Double = CDbl(e.Value)
                'セルの値により、背景色を変更する
                If val < 0 Then
                    e.CellStyle.BackColor = Color.Yellow
                End If

            ElseIf Strings.Left(採点方式, 3) = "PDJ" Then
                'PDJの時
                If Strings.Left(dgv.Columns(e.ColumnIndex).Name, 1) = "J" Then
                    'ジャッジ列
                    Dim val As Decimal = CDec(e.Value)
                    If val < (CaliMin * 担当PCS数) - 2.0 Then
                        e.CellStyle.BackColor = Color.Pink
                    ElseIf val < (CaliMin * 担当PCS数) Then
                        e.CellStyle.BackColor = Color.Yellow
                    ElseIf val > (CaliMax * 担当PCS数) + 2.0 Then
                        e.CellStyle.BackColor = Color.Pink
                    ElseIf val > (CaliMax * 担当PCS数) Then
                        e.CellStyle.BackColor = Color.Yellow
                    End If
                ElseIf dgv.Columns(e.ColumnIndex).Name = "Ref" Then
                    'レフリー列
                    Dim val As Decimal = CDec(e.Value)
                    'セルの値により、背景色を変更する
                    If val < 0 Then
                        e.CellStyle.BackColor = Color.Yellow
                    End If

                End If

            ElseIf Strings.Left(採点方式, 3) = "VAL" Then
                'バルカーの時


                If Strings.Left(dgv.Columns(e.ColumnIndex).Name, 1) = "J" Then

                    If 採点結果_V2.種目(CInt(Strings.Right(sender.name, 2))).審判員(CInt(Strings.Right(dgv.Columns(e.ColumnIndex).Name, 2))).ジャッジタイプ = "J" Then
                        'ジャッジは3倍
                        担当PCS数 = 3

                        'ジャッジ列
                        Dim val As Decimal = CDec(e.Value)
                        If val < (CaliMin * 担当PCS数) - 2.0 * 担当PCS数 Then
                            e.CellStyle.BackColor = Color.Pink
                        ElseIf val < (CaliMin * 担当PCS数) Then
                            e.CellStyle.BackColor = Color.Yellow
                        ElseIf val > (CaliMax * 担当PCS数) + 2.0 * 担当PCS数 Then
                            e.CellStyle.BackColor = Color.Pink
                        ElseIf val > (CaliMax * 担当PCS数) Then
                            e.CellStyle.BackColor = Color.Yellow
                        End If

                    Else
                        '特別審査員
                        担当PCS数 = 1

                        'ジャッジ列
                        Dim val As Decimal = CDec(e.Value)
                        If val < (CaliMin * 担当PCS数) - 2.0 Then
                            e.CellStyle.BackColor = Color.Pink
                        ElseIf val < (CaliMin * 担当PCS数) Then
                            e.CellStyle.BackColor = Color.Yellow
                        ElseIf val > (CaliMax * 担当PCS数) + 2.0 Then
                            e.CellStyle.BackColor = Color.Pink
                        ElseIf val > (CaliMax * 担当PCS数) Then
                            e.CellStyle.BackColor = Color.Yellow
                        End If

                    End If


                ElseIf dgv.Columns(e.ColumnIndex).Name = "Ref" Then
                        'レフリー列
                        Dim val As Decimal = CDec(e.Value)
                    'セルの値により、背景色を変更する
                    If val < 0 Then
                        e.CellStyle.BackColor = Color.Yellow
                    End If

                End If



            ElseIf (Strings.Left(採点方式, 4) = "BJS2" Or Strings.Left(採点方式, 4) = "BJS3") And (e.ColumnIndex >= 5 And e.ColumnIndex < 5 + ジャッジ人数) AndAlso TypeOf e.Value Is Double Then
                'BJS2.0の時の　ジャッジ列

                '0点だったら赤
                Dim val As Double = CDbl(e.Value)
                If val <= 0 Then
                    e.CellStyle.BackColor = Color.Pink
                Else
                    e.CellStyle.BackColor = Color.White
                End If

            ElseIf Strings.Left(採点方式, 4) = "BJPR" And e.ColumnIndex >= 5 AndAlso TypeOf e.Value Is Double Then
                'BJPRの時　
                '0点だったら黄色
                Dim val As Double = CDbl(e.Value)
                If val <= 0 Then
                    e.CellStyle.BackColor = Color.Yellow
                Else
                    e.CellStyle.BackColor = Color.White
                End If


            ElseIf (e.ColumnIndex >= 5 And e.ColumnIndex < 5 + ジャッジ人数) AndAlso TypeOf e.Value Is Double Then
                'ジャッジ列
                Dim val As Double = CDbl(e.Value)
                If val < (CaliMin * 担当PCS数) - 2.0 Then
                    e.CellStyle.BackColor = Color.Pink
                ElseIf val < (CaliMin * 担当PCS数) Then
                    e.CellStyle.BackColor = Color.Yellow
                ElseIf val > (CaliMax * 担当PCS数) + 2.0 Then
                    e.CellStyle.BackColor = Color.Pink
                ElseIf val > (CaliMax * 担当PCS数) Then
                    e.CellStyle.BackColor = Color.Yellow
                End If


            ElseIf Strings.Left(採点方式, 3) = "BJS" And e.ColumnIndex >= (5 + ジャッジ人数 + 担当PCS数) AndAlso TypeOf e.Value Is Double Then
                'BJS2の時の加減点項目
                Dim val As Double = CDbl(e.Value)
                If val < 0 Then
                    e.CellStyle.BackColor = Color.Yellow
                ElseIf val > 0 Then
                    e.CellStyle.BackColor = Color.Blue
                End If


            ElseIf (Strings.Left(採点方式, 4) = "BJS2" Or Strings.Left(採点方式, 4) = "BJS3 ") And e.ColumnIndex >= 5 + ジャッジ人数 AndAlso TypeOf e.Value Is Double Then
                'BJS2.0の時の　PCSの値

                '0点だったら赤
                Dim val As Double = CDbl(e.Value)
                If val <= 0 Then
                    e.CellStyle.BackColor = Color.Pink
                Else
                    e.CellStyle.BackColor = Color.White
                End If



            ElseIf e.ColumnIndex >= 5 + ジャッジ人数 AndAlso TypeOf e.Value Is Double And Strings.Left(採点方式, 3) <> "BJS" Then

                '各PCS 合計列
                Dim val As Double = CDbl(e.Value)
                If val < CaliMin - 1.0 Then
                    e.CellStyle.BackColor = Color.Pink
                ElseIf val < CaliMin Then
                    e.CellStyle.BackColor = Color.Yellow
                ElseIf val > CaliMax + 1.0 Then
                    e.CellStyle.BackColor = Color.Pink
                ElseIf val > CaliMax Then
                    e.CellStyle.BackColor = Color.Yellow
                End If

            Else


            End If

        End If

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl_詳細.SelectedIndexChanged

        ' 更新()


    End Sub


    '画面を開いた時に、種目詳細の色付けを実現するためのコード
    Private Sub TabxVisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl_詳細.VisibleChanged
        'タブが始めて表示された時に呼び出される

        If TabControl_詳細.Visible = True Then

            'タブページを一旦すべてVisible =true にしないと、2タブ目以降が更新されないため
            For Each tpg As TabPage In TabControl_詳細.TabPages
                tpg.Visible = True
            Next

            If LOGLEVEL >= 4 Then
                If Me.InvokeRequired Then
                    Me.Invoke(LogDelegate, New Object() {"F501_TabVisibleChangeから更新呼び出し", 4})
                Else
                    WriteLog("F501_TabVisibleChangeから更新呼び出し", 4)
                End If
            End If


            If 採点結果 IsNot Nothing Then
                更新()

            ElseIf 採点結果_V2 IsNot Nothing Then
                更新_V2()

            End If


        End If

    End Sub




    Private Sub PB_欠場_Click(sender As Object, e As EventArgs) Handles PB_欠場.Click
        'ブレイキン用　欠場があった場合の緊急処理
        Dim F513 As F513_結果入力
        F513 = New F513_結果入力

        F513.設定(区分番号, ラウンド番号)
        F513.Show()


    End Sub



    Private Function 種目記号変換toJDSF種目番号(種目記号 As String) As String
        '種目記号 を渡すと、JDSF種目番号 01-10を返す

        Dim rc As String = ""

        Select Case 種目記号
            Case "W"
                rc = "01"
            Case "T"
                rc = "02"
            Case "V"
                rc = "03"
            Case "F"
                rc = "04"
            Case "Q"
                rc = "05"
            Case "S"
                rc = "06"
            Case "C"
                rc = "07"
            Case "R"
                rc = "08"
            Case "P"
                rc = "09"
            Case "J"
                rc = "10"

        End Select

        Return rc

    End Function


End Class

Public Class 種目別

    Public Property No As String
    Public Property LName As String

    Public Property Heat As Integer
    Public Property Total As Decimal
    Public Property Place As Decimal
    Public Property TES As Decimal
    Public Property GOE As Decimal

    Public Property J01 As Decimal
    Public Property J02 As Decimal
    Public Property J03 As Decimal
    Public Property J04 As Decimal
    Public Property J05 As Decimal
    Public Property J06 As Decimal
    Public Property J07 As Decimal
    Public Property J08 As Decimal
    Public Property J09 As Decimal
    Public Property J10 As Decimal
    Public Property J11 As Decimal
    Public Property J12 As Decimal
    Public Property J13 As Decimal
    Public Property J14 As Decimal
    Public Property J15 As Decimal
    Public Property J16 As Decimal
    Public Property J17 As Decimal
    Public Property J18 As Decimal
    Public Property J19 As Decimal
    Public Property J20 As Decimal

    Public Property Ref As Decimal

    Public Property PCS1 As Decimal
    Public Property PCS2 As Decimal
    Public Property PCS3 As Decimal
    Public Property PCS4 As Decimal


    Public Sub New(ByVal No As String)
        Me.No = No
    End Sub

    Public Sub Set__Lname(LName_ As String)
        LName = LName_
    End Sub
    Public Sub Set__Heat(Heat_ As Integer)
        Heat = Heat_
    End Sub
    Public Sub Set__Total(Total_ As Decimal)
        Total = Total_
    End Sub
    Public Sub Set__Place(Place_ As Decimal)
        Place = Place_
    End Sub
    Public Sub Set__TES(TES_ As Decimal)
        TES = TES_
    End Sub
    Public Sub Set__GOE(GOE_ As Decimal)
        GOE = GOE_
    End Sub
    Public Sub Set__Judge(NO As Integer, Value As Decimal)
        Select Case NO
            Case 1
                J01 = Value
            Case 2
                J02 = Value
            Case 3
                J03 = Value
            Case 4
                J04 = Value
            Case 5
                J05 = Value
            Case 6
                J06 = Value
            Case 7
                J07 = Value
            Case 8
                J08 = Value
            Case 9
                J09 = Value
            Case 10
                J10 = Value
            Case 11
                J11 = Value
            Case 12
                J12 = Value
            Case 13
                J13 = Value
            Case 14
                J14 = Value
            Case 15
                J15 = Value
            Case 16
                J16 = Value
            Case 17
                J17 = Value
            Case 18
                J18 = Value
            Case 19
                J19 = Value
            Case 20
                J20 = Value
        End Select
    End Sub

    Public Sub Set__Ref(Ref_ As Decimal)
        Ref = Ref_
    End Sub

    Public Sub Set__PCS(NO As Integer, Value As Decimal)
        Select Case NO
            Case 1
                PCS1 = Value
            Case 2
                PCS2 = Value
            Case 3
                PCS3 = Value
            Case 4
                PCS4 = Value
        End Select
    End Sub


End Class


