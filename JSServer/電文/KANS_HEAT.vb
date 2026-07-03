Public Class KANS_HEAT


    Public 出場選手数 As Integer

    Function Create電文(端末名 As String, マスタデータ As マスタデータ, 区分番号 As String, ラウンド番号 As String, 種目順 As Integer, ヒート番号 As Integer) As String()

        '総ヒート数の算出
        Dim 種目記号() = Nothing
        Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号)

        出場選手数 = 0

        Dim 背番号リスト() As String = Nothing

        マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)

        Dim 種目 As D_種目 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目順)

        If 種目.SG種別 = "S" Then
            'ソロの時は全員分を送る。
            出場選手数 = マスタデータ.E_ヒート表マスタ.登録済みレコード数

            '背番号リストも全員分作成する（ヒート順）
            ReDim 背番号リスト(出場選手数)
            Dim 背番号リストTEMP() As String = Nothing
            For i = 1 To 出場選手数
                マスタデータ.E_ヒート表マスタ.Get_背番号リスト(種目順, i, 背番号リストTEMP)
                背番号リスト(i) = 背番号リストTEMP(1)
            Next i
        Else
            'ソロ以外の時はその分を送る。
            出場選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(CStr(種目順), ヒート番号, 背番号リスト)
        End If

        Dim 電文基準数 As Integer = 100

        Dim 全レコード数 As Integer
        If 出場選手数 Mod 電文基準数 = 0 Then
            全レコード数 = 出場選手数 \ 電文基準数
        Else
            全レコード数 = 出場選手数 \ 電文基準数 + 1
        End If


        Dim Denbun() As String
        ReDim Denbun(全レコード数)


        Dim レコード番号 As Integer

        Dim 区分 As B_区分
        区分 = マスタデータ.B_区分マスタ.Get区分C(区分番号)

        Dim ラウンド As C_ラウンド
        ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号)


        For レコード番号 = 1 To 全レコード数
            Denbun(レコード番号) = "JK,KANS_HEAT,"
            Denbun(レコード番号) = Denbun(レコード番号) & 端末名 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 全レコード数 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & レコード番号 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 出場選手数 & ","

            Dim 電文毎の件数 As Integer = 0

            If 出場選手数 / 電文基準数 > レコード番号 Then
                電文毎の件数 = 電文基準数
            ElseIf 出場選手数 / 電文基準数 = レコード番号 Then
                電文毎の件数 = 出場選手数 Mod 電文基準数
            Else
                電文毎の件数 = 出場選手数
            End If

            Denbun(レコード番号) = Denbun(レコード番号) & 電文毎の件数 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 区分.区分表記名 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & ラウンド番号 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & マスタデータ.Z_システム設定.Get_種目名称(種目記号(種目順)).種目名_J & ","
            Denbun(レコード番号) = Denbun(レコード番号) & ヒート番号 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 種目.SG種別 & ","
            For d = 1 To UBound(種目記号)
                Denbun(レコード番号) = Denbun(レコード番号) & 種目記号(d) & ":"
            Next d
            Denbun(レコード番号) = Denbun(レコード番号) & ","

        Next レコード番号


        For s = 1 To 出場選手数

            Dim 選手 As 選手
            選手 = マスタデータ.選手マスタ.Get選手C_by背番号(区分.使用する選手マスタ, 背番号リスト(s))

            Dim 電文No As Integer
            If s Mod 電文基準数 = 0 Then
                電文No = s \ 電文基準数
            Else
                電文No = s \ 電文基準数 + 1
            End If


            Denbun（電文No） = Denbun(電文No) & 背番号リスト(s) & ","
            Denbun（電文No） = Denbun(電文No) & 選手.リーダー表記名 & ","
            Denbun（電文No） = Denbun(電文No) & 選手.リーダーフリガナ & ","
            Denbun（電文No） = Denbun(電文No) & 選手.リーダー所属名 & ","
            Denbun（電文No） = Denbun(電文No) & 選手.パートナ表記名 & ","
            Denbun（電文No） = Denbun(電文No) & 選手.パートナフリガナ & ","
            Denbun（電文No） = Denbun(電文No) & 選手.パートナ所属名 & ","
            Denbun（電文No） = Denbun(電文No) & 選手.カップル所属名 & ","

            For d = 1 To 種目数
                Denbun（電文No） = Denbun(電文No) & マスタデータ.E_ヒート表マスタ.Get_ヒート番号(d, 背番号リスト(s)) & ":"
            Next d
            Denbun（電文No） = Denbun(電文No) & ","

            Denbun（電文No） = Denbun(電文No) & "" & ","　　　'選手係OK


        Next s


        Return Denbun

    End Function



End Class
