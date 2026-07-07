Public Class Form1


    Private 選手マスタ As 選手マスタ

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        初期設定()

    End Sub

    Private Sub 初期設定()


        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Me.TB_フォルダ.Text = マスタデータ.Z_システム設定.Comp_filepath
        Me.LB_競技会NO.Text = マスタデータ.A_競技会マスタ.公認競技会NO
        Me.LB_競技会名.Text = マスタデータ.A_競技会マスタ.競技会名


        マスタデータ = Nothing

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'テスト用

        Dim 採点結果 As 採点結果_C
        採点結果 = New 採点結果_C("01", "400")

        Dim F501_採点詳細 As F501_得点詳細_C
        F501_採点詳細 = New F501_得点詳細_C
        F501_採点詳細.設定("01", "400", 採点結果)
        F501_採点詳細.Show()


        Dim LOG As LOG_C
        LOG = New LOG_C
        LOG.CreateFile()

        Dim F_GM As F_GM
        F_GM = New F_GM

        F_GM.Show()

        F_GM.Set_LOG(LOG)



        Dim パス名 = "C:\Users\IBM_ADMIN\Box Sync\90_JDSF\08_新審判基準委員会\40_総合支援システム\TestData\2018MIKA_S"

        Dim マスタ As J_新審判設定
        マスタ = New J_新審判設定(パス名)

        マスタ.Set_新審判基準VER("AJS30J")

        MsgBox(マスタ.減点設定(1).減点項目名)



        '        選手マスタ = New 選手マスタ

        'MsgBox(選手マスタ.選手リスト(1).リーダー氏名)


        'Dim 選手 As 選手
        '選手 = New 選手

        '選手.背番号 = "24"
        '選手.リーダー氏名 = "加藤　文彦"
        '選手.パートナ氏名 = "加藤　花子"
        '選手.カップル所属名 = "千葉県"
        '選手.エントリー区分(1) = "2"

        ' 選手マスタ.選手登録(1, 選手)


    End Sub

    ' Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    'Dim JSパス As String = "C:\Users\IBM_ADMIN\Box Sync\90_JDSF\08_新審判基準委員会\40_総合支援システム\TestData\181117A"
    'Dim Newパス As String = "C:\Users\IBM_ADMIN\Box Sync\90_JDSF\08_新審判基準委員会\40_総合支援システム\TestData\2018MIKA_S"

    'Dim JS_変換 As JS_変換_C
    '   JS_変換 = New JS_変換_C(JSパス, Newパス)

    '  JS_変換.変換()


    'End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click




        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim RPT As RPT_F1_決勝進出者名簿
        RPT = New RPT_F1_決勝進出者名簿

        Dim 配布先リスト(20) As String
        配布先リスト(1) = "掲示"
        配布先リスト(2) = "司会"
        '       配布先リスト(3) = "選手係"
        '      配布先リスト(4) = "選手係"

        ' Dim 採点結果 As 採点結果_C
        '採点結果 = New 採点結果_C("01", "400")

        'RPT.印刷(採点結果, 配布先リスト)

        RPT.印刷("01", "400", マスタデータ, 配布先リスト)


    End Sub


    Sub PT印刷（）


        'パラメータクラスの作成
        Dim Parm As RPT_Parm_H2
        Parm = New RPT_Parm_H2

        Parm.タイトル = "得点一覧表"
        Parm.区分名 = "全日本選手権スタンダード"
        Parm.ラウンド区分(1) = "一次予選"
        Parm.ラウンド区分(2) = "二次予選"
        Parm.ラウンド区分(3) = "三次予選"
        Parm.ラウンド区分(4) = "四次予選"
        Parm.ラウンド区分(5) = "準決勝"
        Parm.ラウンド区分(6) = "下位決勝"
        Parm.ラウンド区分(7) = "上位決勝"
        Parm.ラウンド区分_色(1) = "White"
        Parm.ラウンド区分_色(2) = "White"
        Parm.ラウンド区分_色(3) = "White"
        Parm.ラウンド区分_色(4) = "White"
        Parm.ラウンド区分_色(5) = "White"
        Parm.ラウンド区分_色(6) = "White"
        Parm.ラウンド区分_色(7) = "Yellow"

        Parm.ヒート数 = "2"
        Parm.出場組数 = "12"
        Parm.ピックアップ数 = "6"
        Parm.採点方式 = "AJS30J"

        Parm.競技種目(1) = "F"
        Parm.競技種目(2) = "W"
        Parm.競技種目(3) = "T"
        Parm.競技種目(4) = "V"
        Parm.競技種目(5) = "Q"

        Parm.競技種目2(1) = "ソロ"
        Parm.競技種目2(2) = "全員"
        Parm.競技種目2(3) = "全員"
        Parm.競技種目2(4) = "ソロ"
        Parm.競技種目2(5) = "全員"

        Parm.競技種目_色(1) = "Yellow"
        Parm.競技種目_色(2) = "Yellow"
        Parm.競技種目_色(3) = "Yellow"
        Parm.競技種目_色(4) = "Yellow"
        Parm.競技種目_色(5) = "Yellow"

        Parm.競技種目2_色(1) = "Yellow"
        Parm.競技種目2_色(2) = "Yellow"
        Parm.競技種目2_色(3) = "Yellow"
        Parm.競技種目2_色(4) = "Yellow"
        Parm.競技種目2_色(5) = "Yellow"


        Parm.配布先 = "選手係"

        '======

        Parm.ヒートText(1) = "背番号     合計点      順位     結果        W          T          V          F          Q"
        Parm.ヒートText(2) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(3) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(4) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(5) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(6) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(7) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(8) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(9) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(10) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(11) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(12) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(13) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(14) = "   999" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(15) = "    15" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(16) = "    16" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(17) = "    17" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(18) = "    18" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(19) = "    19" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"
        Parm.ヒートText(20) = "    20" & "     " & "275.999" & "     " & "  1 " & "     " & " UP " & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88" & "     " & "888.88"



        '======

        '===印刷指示
        Dim RPT_H2_ヒート表 As RPT_R2_横マスタ01
        RPT_H2_ヒート表 = New RPT_R2_横マスタ01

        RPT_H2_ヒート表.SetParm(Parm)
        RPT_H2_ヒート表.印刷実行()

        RPT_H2_ヒート表 = Nothing


    End Sub



    Sub H3印刷（）

        'パラメータクラスの作成
        Dim Parm As RPT_Parm_H2
        Parm = New RPT_Parm_H2

        Parm.タイトル = "出場者連絡票"
        Parm.区分名 = "全日本選手権スタンダード"
        Parm.ラウンド区分(1) = "一次予選"
        Parm.ラウンド区分(2) = "二次予選"
        Parm.ラウンド区分(3) = "三次予選"
        Parm.ラウンド区分(4) = "四次予選"
        Parm.ラウンド区分(5) = "準決勝"
        Parm.ラウンド区分(6) = "下位決勝"
        Parm.ラウンド区分(7) = "上位決勝"
        Parm.ラウンド区分_色(1) = "White"
        Parm.ラウンド区分_色(2) = "White"
        Parm.ラウンド区分_色(3) = "White"
        Parm.ラウンド区分_色(4) = "White"
        Parm.ラウンド区分_色(5) = "White"
        Parm.ラウンド区分_色(6) = "White"
        Parm.ラウンド区分_色(7) = "Yellow"

        Parm.ヒート数 = "2"
        Parm.出場組数 = "12"
        Parm.ピックアップ数 = "6"
        Parm.採点方式 = "AJS30J"

        Parm.競技種目(1) = "F"
        Parm.競技種目(2) = "W"
        Parm.競技種目(3) = "T"
        Parm.競技種目(4) = "V"
        Parm.競技種目(5) = "Q"

        Parm.競技種目2(1) = "ソロ"
        Parm.競技種目2(2) = "全員"
        Parm.競技種目2(3) = "全員"
        Parm.競技種目2(4) = "ソロ"
        Parm.競技種目2(5) = "全員"

        Parm.競技種目_色(1) = "Yellow"
        Parm.競技種目_色(2) = "Yellow"
        Parm.競技種目_色(3) = "Yellow"
        Parm.競技種目_色(4) = "Yellow"
        Parm.競技種目_色(5) = "Yellow"

        Parm.競技種目2_色(1) = "Yellow"
        Parm.競技種目2_色(2) = "Yellow"
        Parm.競技種目2_色(3) = "Yellow"
        Parm.競技種目2_色(4) = "Yellow"
        Parm.競技種目2_色(5) = "Yellow"


        Parm.配布先 = "選手係"

        '======

        Parm.ヒートText(1) = "Waltz"
        Parm.ヒートText(2) = "    " & " 1 Heat" & "  " & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999"
        Parm.ヒートText(3) = "    " & " 2 Heat" & "  " & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999"
        Parm.ヒートText(4) = "Tango"
        Parm.ヒートText(5) = "    " & " 1 Heat" & "  " & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99"
        Parm.ヒートText(6) = "    " & " 2 Heat" & "  " & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999"
        Parm.ヒートText(7) = "    " & " 2 Heat" & "  " & "   1" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(8) = "SlowFoxTrot"
        Parm.ヒートText(9) = "    " & " 1 Heat" & "  " & " 999" & " 999" & " 999" & " 999" & " 999" & " 999"
        Parm.ヒートText(10) = "    " & " 2 Heat" & "  " & "   1" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10" & "   1" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(11) = "    " & " 2 Heat" & "  " & "   2" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(12) = "    " & " 2 Heat" & "  " & "   3" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(13) = "    " & " 2 Heat" & "  " & "   4" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(14) = "    " & " 2 Heat" & "  " & "   5" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(15) = "    " & " 2 Heat" & "  " & "   6" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(16) = "    " & " 2 Heat" & "  " & "   7" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(17) = "    " & " 2 Heat" & "  " & "   8" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(18) = "    " & " 2 Heat" & "  " & "   9" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(19) = "    " & " 2 Heat" & "  " & "  10" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(20) = "    " & " 2 Heat" & "  " & "  11" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"



        '======

        '===印刷指示
        Dim RPT_H3_ヒート表 As RPT_R3_縦マスタ01
        RPT_H3_ヒート表 = New RPT_R3_縦マスタ01

        RPT_H3_ヒート表.SetParm(Parm)
        RPT_H3_ヒート表.印刷実行()

        RPT_H3_ヒート表 = Nothing


    End Sub


    Sub H2印刷（）

        'パラメータクラスの作成
        Dim Parm As RPT_Parm_H2
        Parm = New RPT_Parm_H2

        Parm.タイトル = "出場者連絡票"
        Parm.区分名 = "全日本選手権スタンダード"
        Parm.ラウンド区分(1) = "一次予選"
        Parm.ラウンド区分(2) = "二次予選"
        Parm.ラウンド区分(3) = "三次予選"
        Parm.ラウンド区分(4) = "四次予選"
        Parm.ラウンド区分(5) = "準決勝"
        Parm.ラウンド区分(6) = "下位決勝"
        Parm.ラウンド区分(7) = "上位決勝"
        Parm.ラウンド区分_色(1) = "White"
        Parm.ラウンド区分_色(2) = "White"
        Parm.ラウンド区分_色(3) = "White"
        Parm.ラウンド区分_色(4) = "White"
        Parm.ラウンド区分_色(5) = "White"
        Parm.ラウンド区分_色(6) = "White"
        Parm.ラウンド区分_色(7) = "Yellow"

        Parm.ヒート数 = "2"
        Parm.出場組数 = "12"
        Parm.ピックアップ数 = "6"
        Parm.採点方式 = "AJS30J"

        Parm.競技種目(1) = "F"
        Parm.競技種目(2) = "W"
        Parm.競技種目(3) = "T"
        Parm.競技種目(4) = "V"
        Parm.競技種目(5) = "Q"

        Parm.競技種目2(1) = "ソロ"
        Parm.競技種目2(2) = "全員"
        Parm.競技種目2(3) = "全員"
        Parm.競技種目2(4) = "ソロ"
        Parm.競技種目2(5) = "全員"

        Parm.競技種目_色(1) = "Yellow"
        Parm.競技種目_色(2) = "Yellow"
        Parm.競技種目_色(3) = "Yellow"
        Parm.競技種目_色(4) = "Yellow"
        Parm.競技種目_色(5) = "Yellow"

        Parm.競技種目2_色(1) = "Yellow"
        Parm.競技種目2_色(2) = "Yellow"
        Parm.競技種目2_色(3) = "Yellow"
        Parm.競技種目2_色(4) = "Yellow"
        Parm.競技種目2_色(5) = "Yellow"


        Parm.配布先 = "選手係"

        '======

        Parm.ヒートText(1) = "Waltz"
        Parm.ヒートText(2) = "    " & " 1 Heat" & "  " & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999"
        Parm.ヒートText(3) = "    " & " 2 Heat" & "  " & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999"
        Parm.ヒートText(4) = "Tango"
        Parm.ヒートText(5) = "    " & " 1 Heat" & "  " & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99" & "  99"
        Parm.ヒートText(6) = "    " & " 2 Heat" & "  " & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999" & " 999"
        Parm.ヒートText(7) = "    " & " 2 Heat" & "  " & "   1" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(8) = "SlowFoxTrot"
        Parm.ヒートText(9) = "    " & " 1 Heat" & "  " & " 999" & " 999" & " 999" & " 999" & " 999" & " 999"
        Parm.ヒートText(10) = "    " & " 2 Heat" & "  " & "   1" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10" & "   1" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(11) = "    " & " 2 Heat" & "  " & "   2" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(12) = "    " & " 2 Heat" & "  " & "   3" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(13) = "    " & " 2 Heat" & "  " & "   4" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(14) = "    " & " 2 Heat" & "  " & "   5" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(15) = "    " & " 2 Heat" & "  " & "   6" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(16) = "    " & " 2 Heat" & "  " & "   7" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(17) = "    " & " 2 Heat" & "  " & "   8" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(18) = "    " & " 2 Heat" & "  " & "   9" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(19) = "    " & " 2 Heat" & "  " & "  10" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"
        Parm.ヒートText(20) = "    " & " 2 Heat" & "  " & "  11" & "   2" & "   3" & "   4" & "   5" & "   6" & "   7" & "   8" & "   9" & "  10"



        '======

        '===印刷指示
        Dim RPT_H2_ヒート表 As RPT_R2_横マスタ01
        RPT_H2_ヒート表 = New RPT_R2_横マスタ01

        RPT_H2_ヒート表.SetParm(Parm)
        RPT_H2_ヒート表.印刷実行()

        RPT_H2_ヒート表 = Nothing


    End Sub


    Sub H1印刷()


        'パラメータクラスの作成
        Dim Parm As RPT_Parm_H1
        Parm = New RPT_Parm_H1

        Parm.タイトル = "出場者連絡票"
        Parm.区分名 = "全日本選手権スタンダード"
        Parm.ラウンド区分(1) = "一次予選"
        Parm.ラウンド区分(2) = "二次予選"
        Parm.ラウンド区分(3) = "三次予選"
        Parm.ラウンド区分(4) = "四次予選"
        Parm.ラウンド区分(5) = "準決勝"
        Parm.ラウンド区分(6) = "下位決勝"
        Parm.ラウンド区分(7) = "上位決勝"
        Parm.ラウンド区分_色(1) = "White"
        Parm.ラウンド区分_色(2) = "White"
        Parm.ラウンド区分_色(3) = "White"
        Parm.ラウンド区分_色(4) = "White"
        Parm.ラウンド区分_色(5) = "White"
        Parm.ラウンド区分_色(6) = "White"
        Parm.ラウンド区分_色(7) = "Yellow"

        Parm.ヒート数 = "2"
        Parm.出場組数 = "12"
        Parm.ピックアップ数 = "6"
        Parm.採点方式 = "AJS30J"

        Parm.競技種目(1) = "F"
        Parm.競技種目(2) = "W"
        Parm.競技種目(3) = "T"
        Parm.競技種目(4) = "V"
        Parm.競技種目(5) = "Q"

        Parm.競技種目2(1) = "ソロ"
        Parm.競技種目2(2) = "全員"
        Parm.競技種目2(3) = "全員"
        Parm.競技種目2(4) = "ソロ"
        Parm.競技種目2(5) = "全員"

        Parm.競技種目_色(1) = "Yellow"
        Parm.競技種目_色(2) = "Yellow"
        Parm.競技種目_色(3) = "Yellow"
        Parm.競技種目_色(4) = "Yellow"
        Parm.競技種目_色(5) = "Yellow"

        Parm.競技種目2_色(1) = "Yellow"
        Parm.競技種目2_色(2) = "Yellow"
        Parm.競技種目2_色(3) = "Yellow"
        Parm.競技種目2_色(4) = "Yellow"
        Parm.競技種目2_色(5) = "Yellow"


        Parm.配布先 = "選手係"

        '======

        For h = 1 To 5
            For i = 1 To 20

                Parm.ヒート(h).背番号(i) = i * h
                Parm.ヒート(h).選手名(i) = "選" & i * h

            Next i
        Next h

        Parm.ヒート番号(1) = "1"
        Parm.ヒート番号(2) = "2"
        Parm.ヒート番号(3) = "3"
        Parm.ヒート番号(4) = "4"
        Parm.ヒート番号(5) = "5"


        '======

        '===印刷指示
        Dim RPT_H1_ヒート表 As RPT_R1_ヒート表マスタ
        RPT_H1_ヒート表 = New RPT_R1_ヒート表マスタ

        RPT_H1_ヒート表.SetParm(Parm)
        RPT_H1_ヒート表.印刷実行()

        RPT_H1_ヒート表 = Nothing



    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim F110 As F110_区分一覧設定
        F110 = New F110_区分一覧設定

        F110.Show()


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim F120 As F120_採点進行設定
        F120 = New F120_採点進行設定

        F120.Show()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim F130 As F130_選手マスタ設定
        F130 = New F130_選手マスタ設定

        F130.Show()

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Dim F100 As F100_競技会設定
        F100 = New F100_競技会設定

        F100.Show()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim F140 As F140_ジャッジ設定
        F140 = New F140_ジャッジ設定

        F140.Show()
    End Sub


    'F200からの結果決定イベントを受信する。
    Private WithEvents F200 As F200_採点進行確認
    Private Sub F200_結果決定(ByVal sender As Object, ByVal e As 結果決定EventArgs) Handles F200.結果決定

        If F_GM IsNot Nothing Then
            F_GM.SEND_結果決定通知(e.区分番号, e.ラウンド番号)
        End If
    End Sub

    'F501で確定ボタンが押されたら、F200からPush確定ボタン イベントを受信する
    Private Sub F200_Push確定ボタン(ByVal sender As Object, ByVal e As 結果決定EventArgs) Handles F200.Push確定ボタン

        '関連端末に最新の区分一覧を送る
        If F_GM IsNot Nothing Then
            F_GM.SEND_MB_KUBUN()
        End If

        '終了実績を T_採点進行管理に記録する。
        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        マスタデータ.T_採点進行管理.終了実績時刻登録(e.区分番号, e.ラウンド番号)

        マスタデータ = Nothing

    End Sub


    Private Sub PB_進行管理_Click(sender As Object, e As EventArgs) Handles PB_進行管理.Click

        F200 = New F200_採点進行確認

        F200.Show()


    End Sub

    Private WithEvents F_GM As F_GM
    Private Sub PB_StartGM_Click(sender As Object, e As EventArgs) Handles PB_StartGM.Click

        ' 設定ファイル(Z_System.csv)の [ログレベル] を読み込む
        ' 記述がない場合は LogLevel=0（ログ出力なし）
        Dim システム設定 As New マスタデータ
        Dim logLevel As Integer = システム設定.Z_システム設定.LogLevel

        Dim LOG As LOG_C
        LOG = New LOG_C

        If logLevel > 0 Then
            ' ログレベルが設定されている場合のみファイルを作成・出力
            LOG.CreateFile()
            LOG.SetLogLevel(logLevel)
        End If

        F_GM = New F_GM

        ' Set_LOG を Show() より前に呼ぶことで、起動時イベント内でも LOG が有効になる
        F_GM.Set_LOG(LOG)

        F_GM.Show()



    End Sub

    Private Sub 全ジャッジ送信済みイベント(ByVal sender As Object, ByVal e As System.EventArgs) Handles F_GM.全ジャッジ送信済みイベント

        '全ジャッジ送信済みイベントを受信したら、F501を更新する
        Try

            If F200 IsNot Nothing Then
                F200.F501_更新実行()
            End If

        Catch ex As Exception
            'MsgBox("F501結果画面の更新に失敗しました。")
        End Try


    End Sub


    Private Sub SendTempリアル更新(ByVal sender As Object, ByVal ジャッジ結果_J As S_採点結果_V2_J) Handles F_GM.SendTempイベント


        'Send TempイベントをGMから受信したら、F200->F501 のリアル更新を行う

        Try
            If F200 IsNot Nothing Then
                F200.F501_リアル更新実行(ジャッジ結果_J)
            End If

        Catch ex As Exception

        End Try


    End Sub



    Private Sub PB_フォルダ_Click(sender As Object, e As EventArgs) Handles PB_フォルダ.Click

        'FolderBrowserDialogクラスのインスタンスを作成
        Dim fbd As New FolderBrowserDialog

        '上部に表示する説明テキストを指定する
        fbd.Description = "フォルダを指定してください。"
        'ルートフォルダを指定する
        'デフォルトでDesktop
        fbd.RootFolder = Environment.SpecialFolder.Desktop
        '最初に選択するフォルダを指定する
        'RootFolder以下にあるフォルダである必要がある

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        If マスタデータ.Z_システム設定.Comp_filepath = "" Then
            fbd.SelectedPath = "C:\Windows"

        Else
            fbd.SelectedPath = マスタデータ.Z_システム設定.Comp_filepath
        End If


        'ユーザーが新しいフォルダを作成できるようにする
        'デフォルトでTrue
        fbd.ShowNewFolderButton = True

        'ダイアログを表示する
        If fbd.ShowDialog(Me) = DialogResult.OK Then
            '選択されたフォルダを表示する

            マスタデータ.Z_システム設定.CompFilePath登録(fbd.SelectedPath)

            Me.TB_フォルダ.Text = fbd.SelectedPath


            初期設定()
        End If


        マスタデータ = Nothing
    End Sub

    Private Sub PB_司会進行_Click(sender As Object, e As EventArgs) Handles PB_司会進行.Click

        Dim F600 As F600_進行管理
        F600 = New F600_進行管理

        F600.Show()

    End Sub

    Private Sub PB_支援システムデータ移行_Click(sender As Object, e As EventArgs) Handles PB_支援システムデータ移行.Click

        Dim F901 As F901_支援システム取り込み
        F901 = New F901_支援システム取り込み

        F901.setパス名(Me.TB_フォルダ.Text)

        F901.Show()


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Dim H3ヒート As H3ヒート表
        H3ヒート = New H3ヒート表

        H3ヒート.CreateHTML("01", "400", "LOCAL")



        Dim ラウンドHTML As ラウンド一覧
        ラウンドHTML = New ラウンド一覧

        ラウンドHTML.CreateHTML("01", "LOCAL")



        Dim 区分一覧HTML As 区分一覧HTML
        区分一覧HTML = New 区分一覧HTML

        区分一覧HTML.CreateHTML("LOCAL")

        MsgBox("終了")
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click


        Dim LocalPath As String = "C:\DATA\NewServer\19TT01\result\"
        Dim RemotePath As String = "/web/adm.jdsf.jp/www/result/"


        Dim Server名 As String = "59.106.222.159"  '本番サーバー
        Dim User As String = "sakura"
        Dim pass As String = "y4XdE3jH"


        Dim _sftpService As SftpService
        _sftpService = New SftpService(Server名, 8822, User, pass)

        _sftpService.Synchronize(LocalPath, RemotePath)


    End Sub

    Private Sub PB_移行_Click(sender As Object, e As EventArgs) Handles PB_移行.Click

        Dim 移行 As 移行MAIN
        移行 = New 移行MAIN

        移行.Show()

    End Sub

    'フォーム閉じるボタンが押された時
    Private Sub Me_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If MsgBox("本当に終了しても良いですか？", vbOKCancel) = vbOK Then

        Else
            '閉じるをキャンセル
            e.Cancel = True

        End If

    End Sub

    Private Sub PB_進行詳細_Click(sender As Object, e As EventArgs) Handles PB_進行詳細.Click


        Dim F602 As F602_進行詳細
        F602 = New F602_進行詳細

        F602.Show()
        F602.更新()


    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        Dim 採点結果 As 採点結果_C
        採点結果 = New 採点結果_C("01", "400")

        Dim JA_分析 As JA分析_C
        JA_分析 = New JA分析_C


        'JA_分析.JA_CSVファイル作成(採点結果)
        JA_分析.分析EXCELの作成("01", "ALL")


    End Sub


End Class
