Public Class マスタデータ

    Public Z_システム設定 As Z_システム設定


    Public A_競技会マスタ As A_競技会マスタ
    Public B_区分マスタ As B_区分マスタ
    Public C_ラウンドマスタ As C_ラウンドマスタ
    Public D_種目マスタ As D_種目マスタ
    Public E_ヒート表マスタ As E_ヒート表マスタ
    Public F_審判担当PCSマスタ As F_審判担当PCSマスタ

    Public J_新審判設定 As J_新審判設定

    Public T_採点進行管理 As T_採点進行管理
    Public U_進行管理 As U_進行管理

    Public R_印刷設定マスタ As R_印刷設定マスタ

    Public BM_ブックマーク As BM_ブックマーク



    Public 審判員マスタ As 審判員マスタ
    Public 選手マスタ As 選手マスタ

    'ブレイキン用
    Public BR_カテゴリマスタ As BR_カテゴリマスタ
    Public BR_グループマスタ As BR_グループマスタ

    '団体結果用
    Public K_団体区分マスタ As K_団体区分マスタ

    '総合結果用
    Public K_総合結果設定 As K_総合結果設定




    Private パス名 As String

    'コンストラクタ
    Sub New()

        Z_システム設定 = New Z_システム設定
        パス名 = Z_システム設定.Comp_filepath


        A_競技会マスタ = New A_競技会マスタ(パス名)
        B_区分マスタ = New B_区分マスタ(パス名)
        C_ラウンドマスタ = New C_ラウンドマスタ(パス名)
        D_種目マスタ = New D_種目マスタ(パス名)
        E_ヒート表マスタ = New E_ヒート表マスタ(パス名)
        F_審判担当PCSマスタ = New F_審判担当PCSマスタ(パス名)

        J_新審判設定 = New J_新審判設定(Z_システム設定.システムPath)　　　　 'システムパスに変更が必要

        審判員マスタ = New 審判員マスタ(パス名)
        選手マスタ = New 選手マスタ(パス名)

        T_採点進行管理 = New T_採点進行管理(パス名)
        U_進行管理 = New U_進行管理(パス名)

        R_印刷設定マスタ = New R_印刷設定マスタ(Z_システム設定.システムPath)    'システムパスに変更が必要

        BR_カテゴリマスタ = New BR_カテゴリマスタ(パス名)
        BR_グループマスタ = New BR_グループマスタ(パス名)

        BM_ブックマーク = New BM_ブックマーク(パス名)

        K_団体区分マスタ = New K_団体区分マスタ(パス名)
        K_総合結果設定 = New K_総合結果設定(パス名)

    End Sub

    'コンストラクタ　　パス名を指定する場合（支援システムデータ登録等）
    Sub New(競技会パス名 As String)

        Z_システム設定 = New Z_システム設定
        パス名 = 競技会パス名


        A_競技会マスタ = New A_競技会マスタ(パス名)
        B_区分マスタ = New B_区分マスタ(パス名)
        C_ラウンドマスタ = New C_ラウンドマスタ(パス名)
        D_種目マスタ = New D_種目マスタ(パス名)
        E_ヒート表マスタ = New E_ヒート表マスタ(パス名)
        F_審判担当PCSマスタ = New F_審判担当PCSマスタ(パス名)

        J_新審判設定 = New J_新審判設定(Z_システム設定.システムPath)　　　　 'システムパスに変更が必要

        審判員マスタ = New 審判員マスタ(パス名)
        選手マスタ = New 選手マスタ(パス名)

        T_採点進行管理 = New T_採点進行管理(パス名)
        U_進行管理 = New U_進行管理(パス名)

        R_印刷設定マスタ = New R_印刷設定マスタ(Z_システム設定.システムPath)    'システムパスに変更が必要


        BR_カテゴリマスタ = New BR_カテゴリマスタ(パス名)
        BR_グループマスタ = New BR_グループマスタ(パス名)

        BM_ブックマーク = New BM_ブックマーク(パス名)

        K_団体区分マスタ = New K_団体区分マスタ(パス名)
        K_総合結果設定 = New K_総合結果設定(パス名)



    End Sub



    '*************メソッド ***********************

    '区分番号とラウンド番号を渡すとステータスを返す
    Public Function ヒート表準備OK(区分番号 As String, ラウンド番号 As String) As Integer
        '０：　存在しない
        '１：　ヒート表作成前
        '２：　ヒート表作成済み
        '３：　採点済み

        Dim rc As Integer = 0

        E_ヒート表マスタ = New E_ヒート表マスタ(パス名)
        If E_ヒート表マスタ.FileCheck(区分番号, ラウンド番号) = True Then
            'ヒート表が存在した
            rc = 2
        Else
            'ヒート表が存在しない
            rc = 1
        End If

        Return rc

    End Function


    Public Function Get_ラウンド名(ラウンド番号 As String)
        Dim rc As String = ""

        Select Case ラウンド番号
            Case "010"
                rc = "１次予選"
            Case "01R"
                rc = "リダンス"
            Case "011"
                rc = "同点決勝(R1)"
            Case "020"
                rc = "２次予選"
            Case "021"
                rc = "同点決勝(R2)"
            Case "030"
                rc = "３次予選"
            Case "031"
                rc = "同点決勝(R3)"
            Case "040"
                rc = "４次予選"
            Case "041"
                rc = "同点決勝(R4)"
            Case "050"
                rc = "５次予選"
            Case "051"
                rc = "同点決勝(R5)"
            Case "090"
                rc = "最終予選"
            Case "091"
                rc = "同点決勝(最予)"
            Case "100"
                rc = "準々決勝"
            Case "101"
                rc = "同点決勝(QF)"
            Case "200"
                rc = "準決勝"
            Case "201"
                rc = "同点決勝(SF)"
            Case "300"
                rc = "下位決勝"
            Case "400"
                rc = "決勝"

            Case Else
                rc = ラウンド番号

        End Select


        Return rc

    End Function


    Public Function Get_ラウンド名_E(ラウンド番号 As String)
        Dim rc As String = ""

        Select Case ラウンド番号
            Case "010"
                rc = "1R"
            Case "01R"
                rc = "ReDance"
            Case "01T"
                rc = "TieR"
            Case "020"
                rc = "2R"
            Case "030"
                rc = "3R"
            Case "040"
                rc = "4R"
            Case "050"
                rc = "5R"
            Case "090"
                rc = "最終予選"
            Case "09T"
                rc = "同点決勝(最予)"
            Case "100"
                rc = "QtrF"
            Case "10T"
                rc = "TieR(QF)"
            Case "200"
                rc = "SemiF"
            Case "20T"
                rc = "TieR(SF)"
            Case "300"
                rc = "Lower Final"
            Case "400"
                rc = "Final"


        End Select


        Return rc

    End Function



End Class
