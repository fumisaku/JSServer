
Public Class F520_同点決勝設定

    Private マスタデータ As マスタデータ

    Private 対象区分番号 As String
    Private 対象ラウンド番号 As String
    Private 同点決勝ラウンド番号 As String

    Private UP予定数 As Integer

    Private 対象選手背番号() As String

    Private Sub F520_同点決勝設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False

    End Sub


    Public Sub 設定(区分番号 As String, 元ラウンド番号 As String, 対象選手背番号_() As String, UP予定数_ As Integer)

        マスタデータ = New マスタデータ()

        対象区分番号 = 区分番号
        対象ラウンド番号 = 元ラウンド番号
        同点決勝ラウンド番号 = 対象ラウンド番号.Substring(0, 2) & "1"

        対象選手背番号 = 対象選手背番号_
        UP予定数 = UP予定数_


        '画面表示
        Me.LB_区分.Text = 対象区分番号 & " " & マスタデータ.B_区分マスタ.Get区分表記名(対象区分番号)
        Me.LB_ラウンド名.Text = "「" & マスタデータ.Get_ラウンド名(対象ラウンド番号) & "」の同点決勝"
        Me.LB_出場組数.Text = "出場組数:「" & UBound(対象選手背番号) & "」　 UP数:「" & UP予定数 & "」"

        'ラウンド設定項目

        'コンボボックスの値設定
        CB_採点方式.Items.Add("チェック法")
        CB_採点方式.Items.Add("順位法")

        Dim 採点方式 As Object = Nothing
        Get_採点方式一覧(マスタデータ.Z_システム設定.システムPath, 採点方式)
        For i = 0 To UBound(採点方式)
            CB_採点方式.Items.Add(採点方式(i))
        Next i

        CB_ヒート割方式.Items.Add("横割り")
        CB_ヒート割方式.Items.Add("縦割り")
        CB_ヒート割方式.Items.Add("シャッフル")

        'ComboBoxのリストに表示する項目を設定する
        Me.CB_リアルFLAG.Items.Add(" ")
        Me.CB_リアルFLAG.Items.Add("N")

        'テキストBOX
        Me.TB_審判G.Text = マスタデータ.B_区分マスタ.Get区分C(対象区分番号).担当審判グループ
        Me.TB_Heat数.Text = "1"
        Me.TB_UP数.Text = UP予定数
        Me.CB_ヒート割方式.Text = "横割り"
        Me.CB_リアルFLAG.Text = " "
        Me.TB_Cali最大値.Text = マスタデータ.C_ラウンドマスタ.GetラウンドClass(対象区分番号, 対象ラウンド番号).CaliMax
        Me.TB_Cali最小値.Text = マスタデータ.C_ラウンドマスタ.GetラウンドClass(対象区分番号, 対象ラウンド番号).CaliMin



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

    Private Sub PB_ラウンド設定_Click(sender As Object, e As EventArgs) Handles PB_ラウンド設定.Click


        'B_区分一覧も更新
        'C_区分更新(マスタデータ)

        'C_ラウンドマスタから該当のKey項目を一旦Delete

        'DGVの値をAdd
        Dim ラウンドC As C_ラウンド


        '入力チェック

        If Me.CB_採点方式.Text = "" Then
            MsgBox("採点方式を選択してください。")
            マスタデータ = Nothing
            Exit Sub
        End If
        If Me.TB_審判G.Text = "" Then
            MsgBox("審判Gを入力してください。")
            マスタデータ = Nothing
            Exit Sub
        End If
        If Me.TB_Heat数.Text = "" Then
            MsgBox("Heat数を入力してください。")
            マスタデータ = Nothing
            Exit Sub
        End If
        If Me.TB_UP数.Text = "" Then
            MsgBox("UP数を入力してください。")
            マスタデータ = Nothing
            Exit Sub
        End If
        If Me.CB_ヒート割方式.Text = "" Then
            MsgBox("ヒート割方式を選択してください。")
            マスタデータ = Nothing
            Exit Sub
        End If
        If Me.CB_ヒート割方式.Text = "" Then
            Me.CB_ヒート割方式.Text = ""
        End If
        If Me.TB_Cali最大値.Text = "" Then
            Me.TB_Cali最大値.Text = 10
        End If
        If Me.TB_Cali最小値.Text Then
            Me.TB_Cali最小値.Text = 0
        End If



        ラウンドC = New C_ラウンド


        ラウンドC.区分番号 = 対象区分番号
        ラウンドC.ラウンド番号 = 同点決勝ラウンド番号
        ラウンドC.採点方式 = Me.CB_採点方式.Text
        ラウンドC.担当審判グループ = Me.TB_審判G.Text
        ラウンドC.ヒート数 = Me.TB_Heat数.Text
        ラウンドC.UP予定数 = Me.TB_UP数.Text
        ラウンドC.ヒート割方式 = Me.CB_ヒート割方式.Text
        ラウンドC.リアルタイムFLAG = Me.CB_リアルFLAG.Text
        ラウンドC.CaliMax = Me.TB_Cali最大値.Text
        ラウンドC.CaliMin = Me.TB_Cali最小値.Text


        マスタデータ.C_ラウンドマスタ.登録(ラウンドC)



        '採点進行マスターのリアルフラグを更新
        Dim 採点進行 As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(対象区分番号, ラウンドC.ラウンド番号)
        If 採点進行 IsNot Nothing Then
            採点進行.リアルタイムFLAG = ラウンドC.リアルタイムFLAG
            マスタデータ.T_採点進行管理.登録(採点進行)
            採点進行 = Nothing
        End If

        '進行管理を作成
        Dim 採点進行C As T_採点進行 = Nothing
        採点進行C = New T_採点進行

        採点進行C.競技番号 = マスタデータ.T_採点進行管理.Get_競技番号_枝番(対象区分番号, 対象ラウンド番号).Substring(0, 3)
        採点進行C.競技番号枝番 = "01"
        採点進行C.区分番号 = 対象区分番号
        採点進行C.ラウンド番号 = 同点決勝ラウンド番号

        Dim ORG_採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(採点進行C.区分番号, 採点進行C.ラウンド番号)

        If ORG_採点進行C IsNot Nothing Then
            採点進行C.リアルタイムFLAG = ORG_採点進行C.リアルタイムFLAG
            採点進行C.ステータス = ORG_採点進行C.ステータス
        Else
            採点進行C.リアルタイムFLAG = Me.CB_リアルFLAG.Text
            採点進行C.ステータス = ""
        End If

        マスタデータ.T_採点進行管理.登録(採点進行C)
        採点進行C = Nothing


        '種目設定画面の表示
        Dim F112 As F112_競技種目設定
        F112 = New F112_競技種目設定
        F112.データ表示(対象区分番号, 同点決勝ラウンド番号）
        F112.ShowDialog()


    End Sub

    Private Sub PB_ヒート表作成_Click(sender As Object, e As EventArgs) Handles PB_ヒート表作成.Click

        'F511 ヒート表作成画面の表示
        Dim F511 As F511_ヒート表作成
        F511 = New F511_ヒート表作成

        F511.設定(対象区分番号, 同点決勝ラウンド番号, 対象選手背番号)

        F511.ShowDialog()


    End Sub
End Class