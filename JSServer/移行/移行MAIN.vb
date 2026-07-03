Public Class 移行MAIN
    Private Sub 移行MAIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private NJ_Comp As NJ_Comp_C
    Private NJ_Judge As NJ_Judge_C
    Private NJ_Member As NJ_Member_C
    Private CompSF As CompCSV_C
    Private CompFF As CompCSV_C

    Private DanceCSV_SF(5) As DanceCSV_C



    Private DanceCSV_FF(5) As DanceCSV_C


    Private マスタデータ As マスタデータ

    Private 区分番号 As String

    Private Sub PB_移行開始_Click(sender As Object, e As EventArgs) Handles PB_移行開始.Click

        区分番号 = Me.TB_区分番号.Text

        Dim NJパス As String = Me.TB_NJパス.Text & "\"

        NJ_Comp = New NJ_Comp_C
        NJ_Comp.Fileread(NJパス)

        NJ_Judge = New NJ_Judge_C
        NJ_Judge.Fileread(NJパス)

        NJ_Member = New NJ_Member_C
        NJ_Member.Fileread(NJパス)

        CompSF = New CompCSV_C
        CompSF.ReadFile(NJパス, "Comp_SF.csv")

        CompFF = New CompCSV_C
        CompFF.ReadFile(NJパス, "Comp_FF.csv")

        Dim DパスSF As String = NJパス & "\Dancedata\SF\"
        Dim DパスFF As String = NJパス & "\Dancedata\FF\"

        '=====DanceCSVの読み込みと、初期処理
        For d = 1 To CompSF.Get種目数
            DanceCSV_SF(d) = New DanceCSV_C
            DanceCSV_SF(d).読み込み(CompSF, CompSF.Get総合種目記号(d), DパスSF)
        Next d

        For d = 1 To CompFF.Get種目数
            DanceCSV_FF(d) = New DanceCSV_C
            DanceCSV_FF(d).読み込み(CompFF, CompFF.Get総合種目記号(d), DパスFF)
        Next d


        'Dim パス名 As String = "C:\Data\NewServer\" & Me.TB_競技会NO.Text

        マスタデータ = New マスタデータ
        'マスタデータ.Z_システム設定.CompFilePath登録(パス名)

        Try

            選手マスタ作成()
            ジャッジマスタ作成（）
            A_競技会マスタ作成()
            B_区分マスタ作成()
            C_ラウンドマスタ作成()
            D_種目マスタ作成()
            E_ヒート表作成()
            F_審判担当PCSマスタ作成()

            S_採点結果作成()

            T_採点進行管理作成()
            U_進行管理作成()

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        MsgBox("終了")

    End Sub

    Private Sub PB_競技結果取込み_Click(sender As Object, e As EventArgs) Handles PB_競技結果取込み.Click

        区分番号 = Me.TB_区分番号.Text



        Dim NJパス As String = Me.TB_NJパス.Text & "\"

        NJ_Comp = New NJ_Comp_C
        NJ_Comp.Fileread(NJパス)

        NJ_Judge = New NJ_Judge_C
        NJ_Judge.Fileread(NJパス)

        NJ_Member = New NJ_Member_C
        NJ_Member.Fileread(NJパス)

        CompSF = New CompCSV_C
        CompSF.ReadFile(NJパス, "Comp_SF.csv")

        CompFF = New CompCSV_C
        CompFF.ReadFile(NJパス, "Comp_FF.csv")

        Dim DパスSF As String = NJパス & "\Dancedata\SF\"
        Dim DパスFF As String = NJパス & "\Dancedata\FF\"

        '=====DanceCSVの読み込みと、初期処理
        For d = 1 To CompSF.Get種目数
            DanceCSV_SF(d) = New DanceCSV_C
            DanceCSV_SF(d).読み込み(CompSF, CompSF.Get総合種目記号(d), DパスSF)
        Next d

        For d = 1 To CompFF.Get種目数
            DanceCSV_FF(d) = New DanceCSV_C
            DanceCSV_FF(d).読み込み(CompFF, CompFF.Get総合種目記号(d), DパスFF)
        Next d


        'Dim パス名 As String = "C:\Data\NewServer\" & Me.TB_競技会NO.Text

        マスタデータ = New マスタデータ
        'マスタデータ.Z_システム設定.CompFilePath登録(パス名)

        Try

            E_ヒート表作成()
            F_審判担当PCSマスタ作成()

            S_採点結果作成()

            T_採点進行管理作成()
            U_進行管理作成()

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        MsgBox("終了")

    End Sub


    Private Sub 選手マスタ作成()

        Dim 選手リスト = NJ_Member.Get_リスト()

        For i = 1 To UBound(選手リスト)

            If 選手リスト(i, 1) <> "" Then

                Dim 選手 As 選手
                選手 = New 選手

                選手.List番号 = "01"
                選手.背番号 = 選手リスト(i, 1)
                選手.リーダー氏名 = 選手リスト(i, 2)
                選手.リーダー表記名 = 選手リスト(i, 2)
                選手.リーダーフリガナ = 選手リスト(i, 3)
                選手.リーダー所属名 = 選手リスト(i, 6)

                選手.パートナ氏名 = 選手リスト(i, 4)
                選手.パートナ表記名 = 選手リスト(i, 4)
                選手.パートナフリガナ = 選手リスト(i, 5)
                選手.パートナ所属名 = 選手リスト(i, 6)

                選手.カップル所属名 = 選手リスト(i, 6)

                選手.エントリー区分(1) = "1"

                マスタデータ.選手マスタ.選手登録("01", 選手)

            End If
        Next i

    End Sub

    Private Sub ジャッジマスタ作成（）

        Dim 審判リスト = NJ_Judge.Get_リスト

        For j = 1 To UBound(審判リスト)

            If 審判リスト(j, 2) <> "" Then

                Dim 審判 As 審判
                審判 = New 審判

                審判.ジャッジ記号 = 審判リスト(j, 2)
                審判.ジャッジ氏名 = 審判リスト(j, 5)
                審判.ジャッジ表記名 = 審判リスト(j, 5)
                審判.言語 = "J"
                If 審判リスト(j, 1) = "D" Then
                    審判.審判チーム(1) = "R"
                Else
                    審判.審判チーム(1) = "1"
                End If

                マスタデータ.審判員マスタ.審判員登録(審判)

            End If

        Next j

    End Sub

    Private Sub A_競技会マスタ作成()

        マスタデータ.A_競技会マスタ.公認競技会NO = Me.TB_競技会NO.Text
        マスタデータ.A_競技会マスタ.競技会名 = CompFF.競技会名１日本語
        マスタデータ.A_競技会マスタ.開催日 = CompFF.競技会開催日
        'マスタデータ.A_競技会マスタ.開催場所 = CompFF.

        マスタデータ.A_競技会マスタ.登録()

    End Sub


    Private Sub B_区分マスタ作成()

        Dim 区分 As B_区分
        区分 = New B_区分

        区分.区分番号 = 区分番号
        区分.区分名 = CompFF.競技会名２日本語
        区分.区分表記名 = CompFF.競技会名２日本語
        区分.使用する選手マスタ = "01"
        区分.担当審判グループ = 1

        区分.カテゴリ = CompFF.GetSL区分

        マスタデータ.B_区分マスタ.登録(区分)


    End Sub

    Private Sub C_ラウンドマスタ作成()

        Dim ラウンド As C_ラウンド
        ラウンド = New C_ラウンド

        ラウンド.区分番号 = 区分番号
        ラウンド.ラウンド番号 = "200"
        ラウンド.採点方式 = "AJS30J"
        ラウンド.担当審判グループ = 1
        ラウンド.ヒート数 = 2
        ラウンド.UP予定数 = 6
        ラウンド.CaliMax = CompSF.Get_キャリHigh
        ラウンド.CaliMin = CompSF.Get_キャリLow
        ラウンド.ヒート割方式 = "シャッフル"

        マスタデータ.C_ラウンドマスタ.登録(ラウンド)

        ラウンド.ラウンド番号 = "400"
        ラウンド.ヒート数 = 1
        ラウンド.UP予定数 = 6
        ラウンド.CaliMax = CompFF.Get_キャリHigh
        ラウンド.CaliMin = CompFF.Get_キャリLow

        マスタデータ.C_ラウンドマスタ.登録(ラウンド)

    End Sub

    Private Sub D_種目マスタ作成()

        For i = 1 To CompSF.Get種目数
            Dim 種目 As D_種目
            種目 = New D_種目

            種目.区分番号 = 区分番号
            種目.ラウンド番号 = "200"
            種目.種目順 = i
            種目.種目記号 = CompSF.Get総合種目記号(i)
            種目.SG種別 = CompSF.SG判別(i)
            種目.ヒート数 = 2
            種目.担当審判グループ = 1
            種目.CaliMax = CompSF.Get_キャリHigh
            種目.CaliMin = CompSF.Get_キャリLow

            マスタデータ.D_種目マスタ.登録(種目)

        Next i

        For i = 1 To CompFF.Get種目数
            Dim 種目 As D_種目
            種目 = New D_種目

            種目.区分番号 = 区分番号
            種目.ラウンド番号 = "400"
            種目.種目順 = i
            種目.種目記号 = CompFF.Get総合種目記号(i)
            種目.SG種別 = CompFF.SG判別(i)
            If 種目.SG種別 = "G" Then
                種目.ヒート数 = 1
            Else
                種目.ヒート数 = CompFF.決勝進出者数
            End If
            種目.担当審判グループ = 1
            種目.CaliMax = CompFF.Get_キャリHigh
            種目.CaliMin = CompFF.Get_キャリLow

            マスタデータ.D_種目マスタ.登録(種目)

        Next i

    End Sub

    Private Sub E_ヒート表作成()

        Dim ヒート表SF = Nothing
        CompSF.Get_ヒート表_背番号順(ヒート表SF)

        マスタデータ.E_ヒート表マスタ.Read(区分番号, "200")

        For i = 1 To UBound(ヒート表SF)

            If ヒート表SF(i, 0) IsNot Nothing Then
                Dim ヒート表 As E_ヒート表
                ヒート表 = New E_ヒート表

                ヒート表.背番号 = ヒート表SF(i, 0)
                For d = 1 To 5
                    ヒート表.ヒート番号(d) = ヒート表SF(i, d)
                Next d

                マスタデータ.E_ヒート表マスタ.登録(ヒート表, 区分番号, "200")
            End If

        Next i

        Dim ヒート表FF = Nothing
        CompFF.Get_ヒート表_背番号順(ヒート表FF)

        マスタデータ.E_ヒート表マスタ.Read(区分番号, "400")

        For i = 1 To UBound(ヒート表FF)

            If ヒート表FF(i, 0) IsNot Nothing Then

                Dim ヒート表 As E_ヒート表
                ヒート表 = New E_ヒート表

                ヒート表.背番号 = ヒート表FF(i, 0)
                For d = 1 To 5
                    ヒート表.ヒート番号(d) = ヒート表FF(i, d)
                Next d

                マスタデータ.E_ヒート表マスタ.登録(ヒート表, 区分番号, "400")
            End If

        Next i


    End Sub

    Private Sub F_審判担当PCSマスタ作成()

        マスタデータ.F_審判担当PCSマスタ.Read(区分番号, "200")
        For j = 1 To CompSF.ジャッジ人数
            Dim PCS As F_審判担当PCS
            PCS = New F_審判担当PCS

            PCS.ジャッジ記号 = CompSF.審判員記号(j)

            For d = 1 To 5
                PCS.担当PCS番号(d) = CompSF.審判担当PCS(j, 2 * (d - 1) + 1)
            Next d

            マスタデータ.F_審判担当PCSマスタ.登録(PCS)

        Next j


        マスタデータ.F_審判担当PCSマスタ.Read(区分番号, "400")
        For j = 1 To CompFF.ジャッジ人数
            Dim PCS As F_審判担当PCS
            PCS = New F_審判担当PCS

            PCS.ジャッジ記号 = CompFF.審判員記号(j)

            Dim 選手数 As Integer = CompFF.決勝進出者数

            If CompFF.SG判別(1) = "S" Then
                PCS.担当PCS番号(1) = CompFF.審判担当PCS(j, 1)  'S
                PCS.担当PCS番号(2) = CompFF.審判担当PCS(j, 7)  'G
                PCS.担当PCS番号(3) = CompFF.審判担当PCS(j, 8)   'G
                PCS.担当PCS番号(4) = CompFF.審判担当PCS(j, 9)   'S
                PCS.担当PCS番号(5) = CompFF.審判担当PCS(j, 15)    'G
            Else
                PCS.担当PCS番号(1) = CompFF.審判担当PCS(j, 1)  'G
                PCS.担当PCS番号(2) = CompFF.審判担当PCS(j, 2)  'S
                PCS.担当PCS番号(3) = CompFF.審判担当PCS(j, 8)   'G
                PCS.担当PCS番号(4) = CompFF.審判担当PCS(j, 9)   'S
                PCS.担当PCS番号(5) = CompFF.審判担当PCS(j, 15)    'G

            End If

            マスタデータ.F_審判担当PCSマスタ.登録(PCS)

        Next j


    End Sub

    Private Sub S_採点結果作成()

        Dim 採点結果F As S_採点結果
        採点結果F = New S_採点結果(マスタデータ.Z_システム設定.Comp_filepath)


        For j = 1 To CompSF.ジャッジ人数 + CompSF.減点対象ジャッジ人数

            For d = 1 To 5

                Dim 採点 As S_採点
                採点 = New S_採点

                For s = 1 To CompSF.決勝進出者数


                    採点.背番号 = CompSF.背番号(s)
                    If マスタデータ.審判員マスタ.Get_審判Class(CompSF.審判員記号(j)).審判チーム(1) <> "R" Then
                        For p = 1 To 4
                            採点.点数(p) = DanceCSV_SF(d).ソロPCS詳細_素点(s, p, j)
                        Next p
                    End If


                    If マスタデータ.審判員マスタ.Get_審判Class(CompSF.審判員記号(j)).審判チーム(1) = "R" Then

                        For r = 1 To 10
                            採点.減点(r) = DanceCSV_SF(d).ソロ一般減点詳細_2(s, 1, r)
                        Next r
                    End If

                    採点結果F.Read(区分番号, "200", CompSF.Get総合種目記号(d), CompSF.審判員記号(j))

                    採点結果F.登録(採点)

                Next s


            Next d

        Next j



        For j = 1 To CompSF.ジャッジ人数 + CompSF.減点対象ジャッジ人数

            For d = 1 To 5

                Dim 採点 As S_採点
                採点 = New S_採点

                For s = 1 To CompFF.決勝進出者数


                    採点.背番号 = CompFF.背番号(s)

                    If マスタデータ.審判員マスタ.Get_審判Class(CompFF.審判員記号(j)).審判チーム(1) <> "R" Then
                        For p = 1 To 4
                            採点.点数(p) = DanceCSV_FF(d).ソロPCS詳細_素点(s, p, j)
                        Next p
                    End If

                    If マスタデータ.審判員マスタ.Get_審判Class(CompFF.審判員記号(j)).審判チーム(1) = "R" Then
                        For r = 1 To 10
                            採点.減点(r) = DanceCSV_FF(d).ソロ一般減点詳細_2(s, 1, r)
                        Next r
                    End If

                    採点結果F.Read(区分番号, "400", CompFF.Get総合種目記号(d), CompFF.審判員記号(j))

                    採点結果F.登録(採点)

                Next s


            Next d

        Next j

    End Sub

    Private Sub T_採点進行管理作成()

        Dim 採点進行 As T_採点進行
        採点進行 = New T_採点進行

        採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, "200")

        '採点進行.競技番号 = "001"
        '採点進行.競技番号枝番 = "00”
        '採点進行.区分番号 = 区分番号
        '採点進行.ラウンド番号 = "200"
        採点進行.ステータス = "採点済み"

        マスタデータ.T_採点進行管理.登録(採点進行)


        採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, "400")

        '採点進行.競技番号 = "002"
        '採点進行.競技番号枝番 = "00”
        '採点進行.区分番号 = 区分番号
        '採点進行.ラウンド番号 = "400"
        採点進行.ステータス = "採点済み"

        マスタデータ.T_採点進行管理.登録(採点進行)

        採点進行 = Nothing

    End Sub

    Private Sub U_進行管理作成()

        Dim 採点進行 As T_採点進行
        採点進行 = New T_採点進行

        採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, "200")


        For d = 1 To 5
            マスタデータ.E_ヒート表マスタ.Read(区分番号, "200")
            For h = 1 To マスタデータ.E_ヒート表マスタ.Getヒート数(d)
                Dim 進行 As U_進行
                進行 = New U_進行

                進行 = マスタデータ.U_進行管理.Get_進行(採点進行.競技番号, 採点進行.競技番号枝番, d, h)

                '進行.競技番号 = "001"
                ' 進行.競技番号枝番 = "00"
                '進行.種目順 = d
                '進行.ヒート番号 = h
                進行.ステータス = "全審判送信済み”

                マスタデータ.U_進行管理.登録(進行)
            Next h
        Next d



        採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, "400")


        For d = 1 To 5
            マスタデータ.E_ヒート表マスタ.Read(区分番号, "400")
            For h = 1 To マスタデータ.E_ヒート表マスタ.Getヒート数(d)
                Dim 進行 As U_進行
                進行 = New U_進行

                進行 = マスタデータ.U_進行管理.Get_進行(採点進行.競技番号, 採点進行.競技番号枝番, d, h)

                '進行.競技番号 = "002"
                '進行.競技番号枝番 = "00"
                '進行.種目順 = d
                '進行.ヒート番号 = h
                進行.ステータス = "全審判送信済み”

                マスタデータ.U_進行管理.登録(進行)
            Next h
        Next d


        採点進行 = Nothing

    End Sub


End Class