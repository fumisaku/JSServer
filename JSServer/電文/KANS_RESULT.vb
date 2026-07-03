Public Class KANS_RESULT

    Public 出場選手数 As Integer

    Function Create電文(端末名 As String, マスタデータ As マスタデータ, 区分番号 As String, ラウンド番号 As String) As String()

        Dim Denbun() As String


        'マスタデータの再読み込み
        マスタデータ.T_採点進行管理.FileRead()


        Dim SC_Total_Result As SC_Total_Result
        SC_Total_Result = New SC_Total_Result(マスタデータ.Z_システム設定.Comp_filepath)
        SC_Total_Result.読み込み(区分番号, ラウンド番号)

        If SC_Total_Result.出場組数 = 0 Then

            Return Nothing
            Exit Function

        End If

        出場選手数 = SC_Total_Result.出場組数

        '****　　次ラウンドのラウンド番号の検索
        Dim 次ラウンド As C_ラウンド = マスタデータ.C_ラウンドマスタ.Get_次ラウンドClass(区分番号, ラウンド番号)

        Dim 次ラウンド有り As Boolean = False
        If 次ラウンド IsNot Nothing Then

            マスタデータ.E_ヒート表マスタ.Read(次ラウンド.区分番号, 次ラウンド.ラウンド番号)

            次ラウンド有り = True

        Else

        End If


        Dim 電文基準数 As Integer = 100

        Dim 全レコード数 As Integer
        If 出場選手数 Mod 電文基準数 = 0 Then
            全レコード数 = 出場選手数 \ 電文基準数
        Else
            全レコード数 = 出場選手数 \ 電文基準数 + 1
        End If



        ReDim Denbun(全レコード数)


        Dim レコード番号 As Integer

        Dim 区分 As B_区分
        区分 = マスタデータ.B_区分マスタ.Get区分C(区分番号)


        For レコード番号 = 1 To 全レコード数
            Denbun(レコード番号) = "JK,KANS_RESULT,"
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
            Denbun(レコード番号) = Denbun(レコード番号) & 区分番号  & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 区分.区分表記名 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & ラウンド番号 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & マスタデータ.Get_ラウンド名(ラウンド番号) & ","
            Denbun(レコード番号) = Denbun(レコード番号) & マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号).ステータス & ","

            Dim 種目記号リスト() = Nothing
            Dim 種目数 As Integer = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)
            Denbun(レコード番号) = Denbun(レコード番号) & 種目数 & ","

            Dim 種目記号文字列 As String = ""

            For d = 1 To 種目数
                種目記号文字列 = 種目記号文字列 & 種目記号リスト(d) & ":"
            Next d

            Denbun(レコード番号) = Denbun(レコード番号) & 種目記号文字列 & ","


        Next レコード番号


        For s = 1 To 出場選手数

            Dim 選手 As 選手
            選手 = マスタデータ.選手マスタ.Get選手C_by背番号(区分.使用する選手マスタ, SC_Total_Result.背番号(s))

            Dim 電文No As Integer
            If s Mod 電文基準数 = 0 Then
                電文No = s \ 電文基準数
            Else
                電文No = s \ 電文基準数 + 1
            End If


            Denbun（電文No） = Denbun(電文No) & SC_Total_Result.背番号(s) & ","
            Denbun（電文No） = Denbun(電文No) & 選手.リーダー表記名 & ","
            Denbun（電文No） = Denbun(電文No) & 選手.リーダーフリガナ & ","
            Denbun（電文No） = Denbun(電文No) & 選手.リーダー所属名 & ","
            Denbun（電文No） = Denbun(電文No) & 選手.パートナ表記名 & ","
            Denbun（電文No） = Denbun(電文No) & 選手.パートナフリガナ & ","
            Denbun（電文No） = Denbun(電文No) & 選手.パートナ所属名 & ","
            Denbun（電文No） = Denbun(電文No) & 選手.カップル所属名 & ","

            Denbun（電文No） = Denbun(電文No) & SC_Total_Result.順位表記(s) & ","
            Denbun（電文No） = Denbun(電文No) & SC_Total_Result.TotalPoints(s) & ","
            Denbun（電文No） = Denbun(電文No) & "" & ","        'WIN数(s) & ","

            'UP FLAG
            If 次ラウンド有り = True Then
                If マスタデータ.E_ヒート表マスタ.Get_ヒート番号("1", SC_Total_Result.背番号(s)) = 0 Then
                    Denbun（電文No） = Denbun(電文No) & "" & ","
                Else
                    Denbun（電文No） = Denbun(電文No) & "1" & ","
                End If
            Else
                Denbun（電文No） = Denbun(電文No) & "" & ","
            End If


            Denbun（電文No） = Denbun(電文No) & SC_Total_Result.種目点数_01(s) & ","
            Denbun（電文No） = Denbun(電文No) & SC_Total_Result.種目点数_02(s) & ","
            Denbun（電文No） = Denbun(電文No) & SC_Total_Result.種目点数_03(s) & ","
            Denbun（電文No） = Denbun(電文No) & SC_Total_Result.種目点数_04(s) & ","
            Denbun（電文No） = Denbun(電文No) & SC_Total_Result.種目点数_05(s) & ","

            Denbun（電文No） = Denbun(電文No) & "" & ","　　'6種目目
            Denbun（電文No） = Denbun(電文No) & "" & ","　　'7種目目
            Denbun（電文No） = Denbun(電文No) & "" & ","　　'8種目目
            Denbun（電文No） = Denbun(電文No) & "" & ","　　'9種目目
            Denbun（電文No） = Denbun(電文No) & "" & ","　　'10種目目
        Next s


        Return Denbun

    End Function

End Class
