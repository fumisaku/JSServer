Public Class KANS_JUDGE


    Public 総ジャッジ人数 As Integer

    Function Create電文(端末名 As String, マスタデータ As マスタデータ, 区分番号 As String, ラウンド番号 As String) As String()

        '総ジャッジ人数の算出
        総ジャッジ人数 = マスタデータ.審判員マスタ.Get_登録済み審判員数




        Dim 電文基準数 As Integer = 100

        Dim 全レコード数 As Integer
        If 総ジャッジ人数 Mod 電文基準数 = 0 Then
            全レコード数 = 総ジャッジ人数 \ 電文基準数
        Else
            全レコード数 = 総ジャッジ人数 \ 電文基準数 + 1
        End If


        Dim Denbun() As String
        ReDim Denbun(全レコード数)


        Dim レコード番号 As Integer

        Dim 区分 As B_区分
        区分 = マスタデータ.B_区分マスタ.Get区分C(区分番号)

        Dim ラウンド As C_ラウンド
        ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号)


        For レコード番号 = 1 To 全レコード数
            Denbun(レコード番号) = "JK,KANS_JUDGE,"
            Denbun(レコード番号) = Denbun(レコード番号) & 端末名 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 全レコード数 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & レコード番号 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 総ジャッジ人数 & ","

            Dim 電文毎の件数 As Integer = 0

            If 総ジャッジ人数 / 電文基準数 > レコード番号 Then
                電文毎の件数 = 電文基準数
            ElseIf 総ジャッジ人数 / 電文基準数 = レコード番号 Then
                電文毎の件数 = 総ジャッジ人数 Mod 電文基準数
            Else
                電文毎の件数 = 総ジャッジ人数
            End If

            Denbun(レコード番号) = Denbun(レコード番号) & 電文毎の件数 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 区分番号 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 区分.区分表記名 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & マスタデータ.Get_ラウンド名(ラウンド番号) & ","

        Next レコード番号

        Dim 担当審判G As Integer = マスタデータ.C_ラウンドマスタ.Get担当審判グループ(区分番号, ラウンド番号)


        For j = 1 To 総ジャッジ人数

            Dim ジャッジ As 審判
            ジャッジ = マスタデータ.審判員マスタ.審判員リスト(j)

            Dim 電文No As Integer
            If j Mod 電文基準数 = 0 Then
                電文No = j \ 電文基準数
            Else
                電文No = j \ 電文基準数 + 1
            End If

            If ジャッジ.審判チーム(担当審判G) <> "" Then
                Denbun（電文No） = Denbun(電文No) & "1" & ","
                Denbun（電文No） = Denbun(電文No) & ジャッジ.審判チーム(担当審判G) & ","
            Else
                Denbun（電文No） = Denbun(電文No) & "0" & ","
                Denbun（電文No） = Denbun(電文No) & ","
            End If


            Denbun（電文No） = Denbun(電文No) & ジャッジ.ジャッジ記号 & ","
            Denbun（電文No） = Denbun(電文No) & ジャッジ.ジャッジ表記名 & ","
            Denbun（電文No） = Denbun(電文No) & ジャッジ.ジャッジフリガナ & ","
            Denbun（電文No） = Denbun(電文No) & ジャッジ.ジャッジ所属 & ","


        Next j


        Return Denbun

    End Function



End Class
