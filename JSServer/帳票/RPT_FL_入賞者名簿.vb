Public Class RPT_FL_入賞者名簿

    Public Sub 印刷(ByVal 採点結果 As 採点結果_C, ByVal 配布先() As String)

        'ヒート表が作成されているか確認


        Dim RPT As RPT_R2_横マスタ01
        RPT = New RPT_R2_横マスタ01(採点結果.マスタデータ.Z_システム設定.言語)

        '===パラメータ作成
        'パラメータクラスの作成
        Dim Parm As RPT_Parm_H2
        Parm = New RPT_Parm_H2


        共通Parm設定(採点結果.区分番号, 採点結果.ラウンド番号, 採点結果.マスタデータ, Parm)


        If 採点結果.マスタデータ.Z_システム設定.言語 = "E" Then
            Parm.タイトル = "The Final Result"
        Else
            Parm.タイトル = "入賞者名簿"
        End If


        For h = 1 To 20
            Parm.ヒートText(h) = ""
        Next h


        Dim 行数 As Integer = 1  '１～２０

        Dim 選手マスタ番号 As String = 採点結果.マスタデータ.B_区分マスタ.Get区分C(採点結果.区分番号).使用する選手マスタ

        Dim 種目記号リスト() = Nothing

        '種目の色付け
        For s = 1 To 採点結果.マスタデータ.D_種目マスタ.Get_種目数(採点結果.区分番号, 採点結果.ラウンド番号, 種目記号リスト)
            Parm.競技種目_色(s) = "Orange"  '  "Yellow"
            Parm.競技種目2_色(s) = "Orange"  ' "Yellow"
        Next s


        Parm.ヒート数 = ""

        '58文字
        '順位(4) + 2
        '背番号(6) + 2
        '氏名20 + 2
        'フリガナ20 +2
        '所属26 + 2
        '得点 6 + 1
        If 採点結果.マスタデータ.Z_システム設定.言語 = "E" Then
            Parm.ヒートText(1) = "Place     No  Name                                        Counrty                     Points"
        Else
            Parm.ヒートText(1) = "順位  背番号  氏名                  フリガナ              所属                          得点"

        End If


        'クリア
        For i = 2 To 24
            Parm.ヒートText(i) = ""
        Next i

        行数 = 2
        For i = 1 To 採点結果.出場選手数
            Dim 採点結果選手番号 As Integer = 採点結果.Get選手番号(i)

            Parm.ヒートText(行数) = Parm.ヒートText(行数) & CStr(採点結果.総合順位表記(採点結果選手番号)).PadLeft(3) & "   "
            Parm.ヒートText(行数) = Parm.ヒートText(行数) & 採点結果.背番号(採点結果選手番号).PadLeft(5) & "   "

            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタ番号, 採点結果.背番号(採点結果選手番号))

            Parm.ヒートText(行数) = Parm.ヒートText(行数) & PadRight(選手.リーダー氏名, 22)
            Parm.ヒートText(行数) = Parm.ヒートText(行数) & PadRight(選手.リーダーフリガナ, 22)
            Parm.ヒートText(行数) = Parm.ヒートText(行数) & PadRight(選手.リーダー所属名, 28)

            'Parm.ヒートText(行数) = Parm.ヒートText(行数) & CStr(Format(採点結果.総合得点(採点結果選手番号), "##0.000")).PadLeft(6) & " UP"
            Parm.ヒートText(行数) = Parm.ヒートText(行数) & CStr(Format(採点結果.総合得点(採点結果選手番号), "##0.000")).PadLeft(6)

            Parm.ヒートText(行数 + 1) = Parm.ヒートText(行数 + 1) & " ".PadLeft(14)
            Parm.ヒートText(行数 + 1) = Parm.ヒートText(行数 + 1) & PadRight(選手.パートナ氏名, 22)
            Parm.ヒートText(行数 + 1) = Parm.ヒートText(行数 + 1) & PadRight(選手.パートナフリガナ, 22)
            Parm.ヒートText(行数 + 1) = Parm.ヒートText(行数 + 1) & PadRight(選手.パートナ所属名, 28)

            行数 = 行数 + 2
        Next i


        '印刷用パラメータの設定

        印刷実行(RPT, Parm, 配布先)
        'クリア
        For i = 2 To 20
            Parm.ヒートText(i) = ""
        Next i

        RPT = Nothing


    End Sub

    Public Sub 印刷_V2(ByVal 採点結果 As 採点結果_V2, ByVal 配布先() As String)

        'ヒート表が作成されているか確認


        Dim RPT As RPT_R2_横マスタ01
        RPT = New RPT_R2_横マスタ01(採点結果.マスタデータ.Z_システム設定.言語)

        '===パラメータ作成
        'パラメータクラスの作成
        Dim Parm As RPT_Parm_H2
        Parm = New RPT_Parm_H2


        共通Parm設定(採点結果.区分番号, 採点結果.ラウンド番号, 採点結果.マスタデータ, Parm)


        If 採点結果.マスタデータ.Z_システム設定.言語 = "E" Then
            Parm.タイトル = "The Final Result"
        Else
            Parm.タイトル = "入賞者名簿"
        End If


        For h = 1 To 20
            Parm.ヒートText(h) = ""
        Next h


        Dim 行数 As Integer = 1  '１～２０

        Dim 選手マスタ番号 As String = 採点結果.マスタデータ.B_区分マスタ.Get区分C(採点結果.区分番号).使用する選手マスタ

        Dim 種目記号リスト() = Nothing

        '種目の色付け
        For s = 1 To 採点結果.マスタデータ.D_種目マスタ.Get_種目数(採点結果.区分番号, 採点結果.ラウンド番号, 種目記号リスト)
            Parm.競技種目_色(s) = "Orange"  '  "Yellow"
            Parm.競技種目2_色(s) = "Orange"  ' "Yellow"
        Next s


        Parm.ヒート数 = ""

        '58文字
        '順位(4) + 2
        '背番号(6) + 2
        '氏名20 + 2
        'フリガナ20 +2
        '所属26 + 2
        '得点 6 + 1
        If 採点結果.マスタデータ.Z_システム設定.言語 = "E" Then
            Parm.ヒートText(1) = "Place     No  Name                                        Counrty                     Points"
        Else
            Parm.ヒートText(1) = "順位  背番号  氏名                  フリガナ              所属                          得点"

        End If


        'クリア
        For i = 2 To 24
            Parm.ヒートText(i) = ""
        Next i

        行数 = 2
        For i = 1 To 採点結果.出場選手数
            Dim 採点結果選手番号 As Integer = 採点結果.Get選手番号(i)

            Parm.ヒートText(行数) = Parm.ヒートText(行数) & CStr(採点結果.総合順位表記(採点結果選手番号)).PadLeft(3) & "   "
            Parm.ヒートText(行数) = Parm.ヒートText(行数) & 採点結果.背番号(採点結果選手番号).PadLeft(5) & "   "

            Dim 選手 As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタ番号, 採点結果.背番号(採点結果選手番号))

            Parm.ヒートText(行数) = Parm.ヒートText(行数) & PadRight(選手.リーダー氏名, 22)
            Parm.ヒートText(行数) = Parm.ヒートText(行数) & PadRight(選手.リーダーフリガナ, 22)
            Parm.ヒートText(行数) = Parm.ヒートText(行数) & PadRight(選手.リーダー所属名, 28)

            'Parm.ヒートText(行数) = Parm.ヒートText(行数) & CStr(Format(採点結果.総合得点(採点結果選手番号), "##0.000")).PadLeft(6) & " UP"
            Parm.ヒートText(行数) = Parm.ヒートText(行数) & CStr(Format(採点結果.総合得点(採点結果選手番号), "##0.000")).PadLeft(6)

            Parm.ヒートText(行数 + 1) = Parm.ヒートText(行数 + 1) & " ".PadLeft(14)
            Parm.ヒートText(行数 + 1) = Parm.ヒートText(行数 + 1) & PadRight(選手.パートナ氏名, 22)
            Parm.ヒートText(行数 + 1) = Parm.ヒートText(行数 + 1) & PadRight(選手.パートナフリガナ, 22)
            Parm.ヒートText(行数 + 1) = Parm.ヒートText(行数 + 1) & PadRight(選手.パートナ所属名, 28)

            行数 = 行数 + 2
        Next i


        '印刷用パラメータの設定

        印刷実行(RPT, Parm, 配布先)
        'クリア
        For i = 2 To 20
            Parm.ヒートText(i) = ""
        Next i

        RPT = Nothing


    End Sub

    Private Sub 印刷実行(RPT As RPT_R2_横マスタ01, Parm As RPT_Parm_H2, 配布先() As String）


        '配布先セット
        For pp = 0 To UBound(配布先）

            If 配布先(pp) <> "" Then
                Parm.配布先 = 配布先(pp)

                '印刷実行
                RPT.SetParm(Parm)
                RPT.印刷実行()

            End If

        Next pp


    End Sub


    Private Sub 共通Parm設定(ByVal 区分番号, ByVal ラウンド番号, ByVal マスタデータ_, ByVal Parm)

        Dim マスタデータ As マスタデータ
        マスタデータ = マスタデータ_


        'Parm.タイトル = "得点一覧表"
        Parm.区分名 = マスタデータ.T_採点進行管理.Get_競技番号_枝番(区分番号, ラウンド番号） & " " & マスタデータ.B_区分マスタ.Get区分表記名(区分番号)


        'デフォルトラウンド名
        If マスタデータ.Z_システム設定.言語 = "E" Then
            Parm.ラウンド区分(1) = ""
            Parm.ラウンド区分(2) = ""
            Parm.ラウンド区分(3) = ""
            Parm.ラウンド区分(4) = ""
            Parm.ラウンド区分(5) = ""
            Parm.ラウンド区分(6) = ""
            Parm.ラウンド区分(7) = ""
        Else
            Parm.ラウンド区分(1) = "１次予選"
            Parm.ラウンド区分(2) = "２次予選"
            Parm.ラウンド区分(3) = "３次予選"
            Parm.ラウンド区分(4) = "４次予選"
            Parm.ラウンド区分(5) = "準決勝"
            Parm.ラウンド区分(6) = "下位決勝"
            Parm.ラウンド区分(7) = "上位決勝"
        End If

        Parm.ラウンド区分_色(1) = "White"
        Parm.ラウンド区分_色(2) = "White"
        Parm.ラウンド区分_色(3) = "White"
        Parm.ラウンド区分_色(4) = "White"
        Parm.ラウンド区分_色(5) = "White"
        Parm.ラウンド区分_色(6) = "White"
        Parm.ラウンド区分_色(7) = "White"



        Dim ラウンド名 As String
        If マスタデータ.Z_システム設定.言語 = "E" Then
            ラウンド名 = マスタデータ.Get_ラウンド名_E(ラウンド番号)
        Else
            ラウンド名 = マスタデータ.Get_ラウンド名(ラウンド番号)
        End If

        Select Case Strings.Left(ラウンド番号, 2)
            Case "01"
                Parm.ラウンド区分(1) = ラウンド名
                Parm.ラウンド区分_色(1) = "Orange"  '  "Yellow"
            Case "02"
                Parm.ラウンド区分(2) = ラウンド名
                Parm.ラウンド区分_色(2) = "Orange"  '  "Yellow"
            Case "03"
                Parm.ラウンド区分(3) = ラウンド名
                Parm.ラウンド区分_色(3) = "Orange"  '  "Yellow"
            Case "04"
                Parm.ラウンド区分(4) = ラウンド名
                Parm.ラウンド区分_色(4) = "Orange"  '  "Yellow"
            Case "05"
                Parm.ラウンド区分(4) = ラウンド名
                Parm.ラウンド区分_色(4) = "Orange"  '  "Yellow"
            Case "09"
                Parm.ラウンド区分(4) = ラウンド名
                Parm.ラウンド区分_色(4) = "Orange"  '  "Yellow"
            Case "10"
                Parm.ラウンド区分(4) = ラウンド名
                Parm.ラウンド区分_色(4) = "Orange"  '  "Yellow"
            Case "20"
                Parm.ラウンド区分(5) = ラウンド名
                Parm.ラウンド区分_色(5) = "Orange"  ' "Yellow"
            Case "30"
                Parm.ラウンド区分(6) = ラウンド名
                Parm.ラウンド区分_色(6) = "Orange"  ' "Yellow"
            Case "40"
                Parm.ラウンド区分(7) = ラウンド名
                Parm.ラウンド区分_色(7) = "Orange"  '  "Yellow"
        End Select

        Dim ラウンドClass As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号)

        'Parm.ヒート数 = "2"

        If マスタデータ.Z_システム設定.言語 = "E" Then
            Parm.出場組数 = CStr(マスタデータ.C_ラウンドマスタ.出場組数(区分番号, ラウンド番号)) & " Couples"
        Else
            Parm.出場組数 = "出場  " & CStr(マスタデータ.C_ラウンドマスタ.出場組数(区分番号, ラウンド番号)) & "組"
        End If

        If ラウンド番号 = "300" Or ラウンド番号 = "400" Then
            Parm.ピックアップ数 = ""
        Else
            If マスタデータ.Z_システム設定.言語 = "E" Then
                Parm.ピックアップ数 = "Pickup " & ラウンドClass.UP予定数 & " Couples"
            Else
                Parm.ピックアップ数 = "ピックアップ " & ラウンドClass.UP予定数 & " 組"

            End If

        End If

        Parm.採点方式 = ラウンドClass.Get_採点方式名()

        Dim 種目記号リスト() = Nothing
        Dim 種目数 As Integer = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        For s = 1 To 5
            If s <= 種目数 Then
                Parm.競技種目(s) = 種目記号リスト(s)
                If マスタデータ.Z_システム設定.言語 = "E" Then
                    Parm.競技種目2(s) = マスタデータ.D_種目マスタ.Get_SG種別表記名_E(区分番号, ラウンド番号, s)
                Else
                    Parm.競技種目2(s) = マスタデータ.D_種目マスタ.Get_SG種別表記名(区分番号, ラウンド番号, s)
                End If

                Parm.競技種目_色(s) = "White"
                Parm.競技種目2_色(s) = "White"
            Else
                Parm.競技種目(s) = ""
                Parm.競技種目2(s) = ""
                Parm.競技種目_色(s) = "White"
                Parm.競技種目2_色(s) = "White"

            End If

        Next s

    End Sub


    'Byte単位で文字を取得する関数
    Function PadRight(ByVal st As String, ByVal len As Integer) As String
        Dim len0 As Integer
        len0 = LenB(st)

        If len0 > len Then
            Return Strings.Left(st, len)
        Else
            Return LTrim(st) & Space(len - len0)
        End If
    End Function

    ''' -----------------------------------------------------------------------------------------
    ''' <summary>
    '''     半角 1 バイト、全角 2 バイトとして、指定された文字列のバイト数を返します。</summary>
    ''' <param name="stTarget">
    '''     バイト数取得の対象となる文字列。</param>
    ''' <returns>
    '''     半角 1 バイト、全角 2 バイトでカウントされたバイト数。</returns>
    ''' -----------------------------------------------------------------------------------------
    Private Shared Function LenB(ByVal stTarget As String) As Integer
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(stTarget)
    End Function
End Class
