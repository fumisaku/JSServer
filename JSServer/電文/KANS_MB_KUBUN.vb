Public Class KANS_MB_KUBUN

    Public 競技番号() As String
    Public 競技枝番() As String

    Public 区分番号() As String
    Public 区分記号() As String
    Public 区分名() As String
    Public 区分表記名() As String

    Public ラウンド番号() As String
    Public ラウンド表記名() As String

    Public カテゴリ() As String
    Public 担当審判グループ() As String
    Public 選手マスタ番号() As String

    Public ステータス() As String

    Public 開始予定時刻() As Date
    Public 終了予定時刻() As Date
    Public 開始実績時刻() As Date
    Public 終了実績時刻() As Date

    Public 総進行区分数 As Integer


    Sub New(総進行区分数_ As Integer)

        総進行区分数 = 総進行区分数_

        ReDim 競技番号(総進行区分数)
        ReDim 競技枝番(総進行区分数)

        ReDim 区分番号(総進行区分数)
        ReDim 区分記号(総進行区分数)
        ReDim 区分名(総進行区分数)
        ReDim 区分表記名（総進行区分数)
        ReDim ラウンド番号(総進行区分数)
        ReDim ラウンド表記名(総進行区分数)

        ReDim カテゴリ(総進行区分数)
        ReDim 担当審判グループ(総進行区分数)
        ReDim 選手マスタ番号(総進行区分数)

        ReDim ステータス(総進行区分数)

        ReDim 開始予定時刻(総進行区分数)
        ReDim 終了予定時刻(総進行区分数)
        ReDim 開始実績時刻(総進行区分数)
        ReDim 終了実績時刻(総進行区分数)


    End Sub


    Function Create電文(端末名 As String, マスタデータ As マスタデータ) As String()


        Dim 電文区分数 As Integer = 100

        Dim 全レコード数 As Integer
        If 総進行区分数 Mod 電文区分数 = 0 Then
            全レコード数 = 総進行区分数 \ 電文区分数
        Else
            全レコード数 = 総進行区分数 \ 電文区分数 + 1
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

        For レコード番号 = 1 To 全レコード数
            Denbun(レコード番号) = "JK,KANS_MB_KUBUN,"
            Denbun(レコード番号) = Denbun(レコード番号) & 端末名 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 全レコード数 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & レコード番号 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 総進行区分数 & ","

            Dim 電文毎の進行区分数 As Integer = 0

            If 総進行区分数 / 電文区分数 > レコード番号 Then
                電文毎の進行区分数 = 電文区分数
            ElseIf 総進行区分数 / 電文区分数 = レコード番号 Then
                電文毎の進行区分数 = 総進行区分数 Mod 電文区分数
            Else
                電文毎の進行区分数 = 総進行区分数
            End If

            Denbun（レコード番号） = Denbun(レコード番号) & 電文毎の進行区分数 & ","     '電文毎の進行区分数   


        Next レコード番号


        For s = 1 To 総進行区分数

            Dim 区分 As B_区分
            区分 = マスタデータ.B_区分マスタ.Get区分C(マスタデータ.T_採点進行管理.リスト(s).区分番号)

            Dim ラウンド As C_ラウンド
            ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(マスタデータ.T_採点進行管理.リスト(s).区分番号, マスタデータ.T_採点進行管理.リスト(s).ラウンド番号)

            Dim 電文No As Integer
            If s Mod 電文区分数 = 0 Then
                電文No = s \ 電文区分数
            Else
                電文No = s \ 電文区分数 + 1
            End If

            Denbun（電文No） = Denbun(電文No) & マスタデータ.T_採点進行管理.リスト(s).競技番号 & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.T_採点進行管理.リスト(s).競技番号枝番 & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.T_採点進行管理.リスト(s).区分番号 & ","


            Denbun（電文No） = Denbun(電文No) & 区分.区分記号 & ","
            Denbun（電文No） = Denbun(電文No) & 区分.区分名 & ","
            Denbun（電文No） = Denbun(電文No) & 区分.区分表記名 & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.T_採点進行管理.リスト(s).ラウンド番号 & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.Get_ラウンド名(マスタデータ.T_採点進行管理.リスト(s).ラウンド番号) & ","
            Denbun（電文No） = Denbun(電文No) & 区分.カテゴリ & ","
            Denbun（電文No） = Denbun(電文No) & ラウンド.担当審判グループ & ","
            Denbun（電文No） = Denbun(電文No) & 区分.使用する選手マスタ & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.T_採点進行管理.リスト(s).ステータス & ","

            Denbun（電文No） = Denbun(電文No) & マスタデータ.T_採点進行管理.リスト(s).開始予定 & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.T_採点進行管理.リスト(s).終了予定 & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.T_採点進行管理.リスト(s).開始実績 & ","
            Denbun（電文No） = Denbun(電文No) & マスタデータ.T_採点進行管理.リスト(s).終了実績 & ","

        Next s

        For レコード番号 = 1 To 全レコード数
            Denbun（レコード番号） = Denbun(レコード番号) & マスタデータ.Z_システム設定.Timer_D & ","

            For i = 1 To 9
                Denbun（レコード番号） = Denbun(レコード番号) & マスタデータ.Z_システム設定.Timer(i) & ","

            Next i
        Next レコード番号

        Return Denbun

    End Function


End Class
