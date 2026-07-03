Public Class KANS_MU_Progress


    Public 総ヒート数 As Integer

    Function Create電文(端末名 As String, マスタデータ As マスタデータ, 区分番号 As String, ラウンド番号 As String) As String()


        'U_進行管理をリロード
        マスタデータ.U_進行管理.FileRead()

        '総ヒート数の算出
        Dim 種目記号() = Nothing
        Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号)

        Dim 種目毎ヒート数() As Integer
        ReDim 種目毎ヒート数(種目数）

        マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)

        Dim Minヒート数 As Integer = 1000
        Dim Maxヒート数 As Integer = 0
        総ヒート数 = 0

        For s = 1 To 種目数
            Dim 種目ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(s)

            総ヒート数 = 総ヒート数 + 種目ヒート数
            種目毎ヒート数(s) = 種目ヒート数

            If Minヒート数 > 種目ヒート数 Then
                Minヒート数 = 種目ヒート数
            End If
            If Maxヒート数 < 種目ヒート数 Then
                Maxヒート数 = 種目ヒート数
            End If
        Next s



        Dim 電文ヒート数 As Integer = 100

        Dim 全レコード数 As Integer
        If 総ヒート数 Mod 電文ヒート数 = 0 Then
            全レコード数 = 総ヒート数 \ 電文ヒート数
        Else
            全レコード数 = 総ヒート数 \ 電文ヒート数 + 1
        End If

        'JK
        'KANS_MB_KUBUN
        '送信元端末名
        '全レコード数
        '当レコード番号
        '総進行区分数
        '進行区分数(当電文)

        Dim Denbun() As String
        ReDim Denbun(全レコード数)


        Dim レコード番号 As Integer

        Dim 区分 As B_区分
        区分 = マスタデータ.B_区分マスタ.Get区分C(区分番号)

        Dim ラウンド As C_ラウンド
        ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号)


        For レコード番号 = 1 To 全レコード数
            Denbun(レコード番号) = "JK,KANS_MU_Progress,"
            Denbun(レコード番号) = Denbun(レコード番号) & 端末名 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 全レコード数 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & レコード番号 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 総ヒート数 & ","

            Dim 電文毎のヒート数 As Integer = 0

            If 総ヒート数 / 電文ヒート数 > レコード番号 Then
                電文毎のヒート数 = 電文ヒート数
            ElseIf 総ヒート数 / 電文ヒート数 = レコード番号 Then
                電文毎のヒート数 = 総ヒート数 Mod 電文ヒート数
            Else
                電文毎のヒート数 = 総ヒート数
            End If

            Denbun（レコード番号） = Denbun(レコード番号) & 電文毎のヒート数 & ","     '電文毎の進行区分数   


            Denbun（レコード番号） = Denbun(レコード番号) & マスタデータ.E_ヒート表マスタ.登録済みレコード数 & ","  '出場選手数


            If Minヒート数 = Maxヒート数 Then
                Denbun（レコード番号） = Denbun(レコード番号) & Maxヒート数 & ","  'ヒート数
            ElseIf Minヒート数 < Maxヒート数 Then
                Denbun（レコード番号） = Denbun(レコード番号) & Minヒート数 & "～" & Maxヒート数 & ","  'ヒート数
            End If

            Denbun（レコード番号） = Denbun(レコード番号) & ラウンド.UP予定数 & ","
            Denbun（レコード番号） = Denbun(レコード番号) & ラウンド.採点方式 & ","
            Denbun（レコード番号） = Denbun(レコード番号) & ラウンド.ヒート割方式 & ","

        Next レコード番号


        Dim 採点進行 As T_採点進行
        採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)


        For s = 1 To 総ヒート数


            Dim 電文No As Integer
            If s Mod 電文ヒート数 = 0 Then
                電文No = s \ 電文ヒート数
            Else
                電文No = s \ 電文ヒート数 + 1
            End If


            Denbun（電文No） = Denbun(電文No) & 採点進行.競技番号 & ","
            Denbun（電文No） = Denbun(電文No) & 採点進行.競技番号枝番 & ","
            Denbun（電文No） = Denbun(電文No) & 区分番号 & ","
            Denbun（電文No） = Denbun(電文No) & 区分.区分名 & ","
            Denbun（電文No） = Denbun(電文No) & 区分.区分表記名 & ","
            Denbun（電文No） = Denbun(電文No) & 区分.カテゴリ & ","
            Denbun（電文No） = Denbun(電文No) & ラウンド番号 & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.Get_ラウンド名(ラウンド番号) & ","

            Dim 種目順 As Integer = 0
            Dim 累計ヒート数 As Integer = 0
            Dim 種目ヒート番号 As Integer = 0
            For d = 1 To 種目数
                累計ヒート数 = 累計ヒート数 + 種目毎ヒート数(d)
                If s <= 累計ヒート数 Then
                    種目順 = d

                    If 種目順 = 1 Then
                        種目ヒート番号 = s
                    Else
                        種目ヒート番号 = s
                        For dd = 1 To 種目順 - 1
                            種目ヒート番号 = 種目ヒート番号 - 種目毎ヒート数(dd)
                        Next dd
                    End If
                    d = 種目数
                End If
            Next d

            Denbun（電文No） = Denbun(電文No) & 種目順 & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目順).種目記号 & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目順).SG種別 & ","
            Denbun（電文No） = Denbun(電文No) & 種目ヒート番号 & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.U_進行管理.Get_進行(採点進行.競技番号, 採点進行.競技番号枝番, 種目順, 種目ヒート番号).ステータス & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.U_進行管理.Get_進行(採点進行.競技番号, 採点進行.競技番号枝番, 種目順, 種目ヒート番号).採点終了時刻 & ","
            Denbun（電文No） = Denbun(電文No) & "" & ","    '選手係OK
            Denbun（電文No） = Denbun(電文No) & "" & ","    'ジャッジOK

            Dim 背番号リスト() As String = Nothing
            マスタデータ.E_ヒート表マスタ.Get_背番号リスト(種目順, 種目ヒート番号, 背番号リスト)

            For 選手 = 1 To UBound(背番号リスト)
                Denbun（電文No） = Denbun(電文No) & 背番号リスト(選手) & " "
            Next 選手
            Denbun（電文No） = Denbun(電文No) & ","


        Next s


        Return Denbun

    End Function


End Class
