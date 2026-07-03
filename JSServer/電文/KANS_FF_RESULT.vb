Public Class KANS_FF_RESULT


    Public 総件数 As Integer


    Function Create電文(端末名 As String, マスタデータ As マスタデータ, 区分番号_ As String) As String()

        Dim Denbun() As String


        'マスタデータの再読み込み
        マスタデータ.T_採点進行管理.FileRead()


        '区分番号がブレイキンのカテゴリかどうか判定

        Dim 区分番号 As String
        If マスタデータ.BR_グループマスタ.Get決勝区分番号(区分番号_) <> "" Then
            区分番号 = マスタデータ.BR_グループマスタ.Get決勝区分番号(区分番号_)
        Else
            区分番号 = 区分番号_
        End If


        Dim SC_Total_Result As SC_Total_Result
        SC_Total_Result = New SC_Total_Result(マスタデータ.Z_システム設定.Comp_filepath)

        'セミファイナルの結果読み込み
        SC_Total_Result.読み込み(区分番号, "300")
        Dim SF出場者数 As Integer = SC_Total_Result.出場組数

        総件数 = 0
        If SF出場者数 > 0 Then
            総件数 = 総件数 + SF出場者数
        End If


        'ファイナルの結果読み込み
        SC_Total_Result.読み込み(区分番号, "400")
        Dim FF出場者数 As Integer = SC_Total_Result.出場組数

        If FF出場者数 > 0 Then
            総件数 = 総件数 + FF出場者数
        End If


        If FF出場者数 = 0 Then

            Return Nothing
            Exit Function

        End If



        Dim 電文基準数 As Integer = 100

        Dim 全レコード数 As Integer
        If 総件数 Mod 電文基準数 = 0 Then
            全レコード数 = 総件数 \ 電文基準数
        Else
            全レコード数 = 総件数 \ 電文基準数 + 1
        End If



        ReDim Denbun(全レコード数)


        Dim レコード番号 As Integer

        Dim 区分 As B_区分
        区分 = マスタデータ.B_区分マスタ.Get区分C(区分番号)


        For レコード番号 = 1 To 全レコード数
            Denbun(レコード番号) = "JK,KANS_FF_RESULT,"
            Denbun(レコード番号) = Denbun(レコード番号) & 端末名 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 全レコード数 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & レコード番号 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 総件数 & ","


            Dim 電文毎の件数 As Integer = 0

            If 総件数 / 電文基準数 > レコード番号 Then
                電文毎の件数 = 電文基準数
            ElseIf 総件数 / 電文基準数 = レコード番号 Then
                電文毎の件数 = 総件数 Mod 電文基準数
            Else
                電文毎の件数 = 総件数
            End If

            Denbun(レコード番号) = Denbun(レコード番号) & 電文毎の件数 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 区分番号 & ","

            If マスタデータ.B_区分マスタ.Get区分表記名(区分番号) <> "" Then
                Denbun（レコード番号） = Denbun(レコード番号) & 区分.区分表記名 & ","
            Else
                'ブレイキンの時
                Denbun（レコード番号） = Denbun(レコード番号) & マスタデータ.BR_カテゴリマスタ.Getカテゴリ表記名(区分番号) & ","
            End If



        Next レコード番号

        '決勝分
        For s = 1 To FF出場者数

            Dim 選手 As 選手
            選手 = マスタデータ.選手マスタ.Get選手C_by背番号(区分.使用する選手マスタ, SC_Total_Result.背番号(s))

            Dim 電文No As Integer
            If s Mod 電文基準数 = 0 Then
                電文No = s \ 電文基準数
            Else
                電文No = s \ 電文基準数 + 1
            End If

            Denbun（電文No） = Denbun(電文No) & "決勝" & ","  'ラウンド名
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


            Denbun（電文No） = Denbun(電文No) & ","　　　'勝FLAG  不要


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



        '下位決勝分

        If SF出場者数 > 0 Then
            SC_Total_Result.読み込み(区分番号, "300")

            For s = 1 To SF出場者数

                Dim 選手 As 選手
                選手 = マスタデータ.選手マスタ.Get選手C_by背番号(区分.使用する選手マスタ, SC_Total_Result.背番号(s))

                Dim 電文No As Integer
                If s Mod 電文基準数 = 0 Then
                    電文No = s \ 電文基準数
                Else
                    電文No = s \ 電文基準数 + 1
                End If

                Denbun（電文No） = Denbun(電文No) & "下位決勝" & ","  'ラウンド名
                Denbun（電文No） = Denbun(電文No) & SC_Total_Result.背番号(s) & ","
                Denbun（電文No） = Denbun(電文No) & 選手.リーダー表記名 & ","
                Denbun（電文No） = Denbun(電文No) & 選手.リーダーフリガナ & ","
                Denbun（電文No） = Denbun(電文No) & 選手.リーダー所属名 & ","
                Denbun（電文No） = Denbun(電文No) & 選手.パートナ表記名 & ","
                Denbun（電文No） = Denbun(電文No) & 選手.パートナフリガナ & ","
                Denbun（電文No） = Denbun(電文No) & 選手.パートナ所属名 & ","
                Denbun（電文No） = Denbun(電文No) & 選手.カップル所属名 & ","

                Denbun（電文No） = Denbun(電文No) & SC_Total_Result.順位表記(s) + FF出場者数 & ","
                Denbun（電文No） = Denbun(電文No) & SC_Total_Result.TotalPoints(s) & ","
                Denbun（電文No） = Denbun(電文No) & "" & ","        'WIN数(s) & ","


                Denbun（電文No） = Denbun(電文No) & ","   '勝FLAG  不要


                Denbun（電文No） = Denbun(電文No) & SC_Total_Result.種目点数_01(s) & ","
                Denbun（電文No） = Denbun(電文No) & SC_Total_Result.種目点数_02(s) & ","
                Denbun（電文No） = Denbun(電文No) & SC_Total_Result.種目点数_03(s) & ","
                Denbun（電文No） = Denbun(電文No) & SC_Total_Result.種目点数_04(s) & ","
                Denbun（電文No） = Denbun(電文No) & SC_Total_Result.種目点数_05(s) & ","

                Denbun（電文No） = Denbun(電文No) & "" & ","  '6種目目
                Denbun（電文No） = Denbun(電文No) & "" & ","  '7種目目
                Denbun（電文No） = Denbun(電文No) & "" & ","  '8種目目
                Denbun（電文No） = Denbun(電文No) & "" & ","  '9種目目
                Denbun（電文No） = Denbun(電文No) & "" & ","  '10種目目
            Next s

        End If


        Return Denbun

    End Function


    Function Create電文_OLD(端末名 As String, マスタデータ As マスタデータ, 区分番号_ As String) As String()

        '区分番号がブレイキンのカテゴリかどうか判定

        Dim 区分番号 As String
        If マスタデータ.BR_グループマスタ.Get決勝区分番号(区分番号_) <> "" Then
            区分番号 = マスタデータ.BR_グループマスタ.Get決勝区分番号(区分番号_)
        Else
            区分番号 = 区分番号_
        End If

        Dim 採点結果 As 採点結果_C
        採点結果 = New 採点結果_C(区分番号, "300")

        総件数 = 0
        If 採点結果.出場選手数 > 0 Then
            総件数 = 総件数 + 採点結果.出場選手数
        End If

        採点結果 = New 採点結果_C(区分番号, "400")
        If 採点結果.出場選手数 > 0 Then
            総件数 = 総件数 + 採点結果.出場選手数
        End If



        Dim 電文毎件数 As Integer = 100

        Dim 全レコード数 As Integer
        If 総件数 Mod 電文毎件数 = 0 Then
            全レコード数 = 総件数 \ 電文毎件数
        Else
            全レコード数 = 総件数 \ 電文毎件数 + 1
        End If

        'JK
        'KANS_FF_RESULT
        '送信元端末名
        '全レコード数
        '当レコード番号
        '総区分数
        '区分数(当電文)

        Dim Denbun() As String
        ReDim Denbun(全レコード数)



        Dim レコード番号 As Integer

        For レコード番号 = 1 To 全レコード数
            Denbun(レコード番号) = "JK,KANS_FF_RESULT" & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 端末名 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 全レコード数 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & レコード番号 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 総件数 & ","

            Dim 電文毎の進行区分数 As Integer = 0

            If 総件数 / 電文毎件数 > レコード番号 Then
                電文毎の進行区分数 = 電文毎件数
            ElseIf 総件数 / 電文毎件数 = レコード番号 Then
                電文毎の進行区分数 = 総件数 Mod 電文毎件数
            Else
                電文毎の進行区分数 = 総件数
            End If

            Denbun（レコード番号） = Denbun(レコード番号) & 電文毎の進行区分数 & ","     '電文毎の件数   
            Denbun（レコード番号） = Denbun(レコード番号) & 区分番号 & ","

            If マスタデータ.B_区分マスタ.Get区分表記名(区分番号) <> "" Then
                Denbun（レコード番号） = Denbun(レコード番号) & マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & ","
            Else
                'ブレイキンの時
                Denbun（レコード番号） = Denbun(レコード番号) & マスタデータ.BR_カテゴリマスタ.Getカテゴリ表記名(区分番号) & ","
            End If

        Next レコード番号


        Dim 選手マスタ番号 As String = マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ

        Dim 電文No As Integer

        Dim 決勝進出者数 = 採点結果.出場選手数

        '決勝分
        For 順位 = 1 To 採点結果.出場選手数

            If 順位 Mod 電文毎件数 = 0 Then
                電文No = 順位 \ 電文毎件数
            Else
                電文No = 順位 \ 電文毎件数 + 1
            End If

            For s = 1 To 総件数
                If 採点結果.総合順位番号(s) = 順位 Then

                    Denbun（電文No） = Denbun(電文No) & "決勝" & ","  'ラウンド名
                    Denbun（電文No） = Denbun(電文No) & 採点結果.背番号(s) & ","

                    Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタ番号, 採点結果.背番号(s))

                    Denbun（電文No） = Denbun(電文No) & 選手.リーダー表記名 & ","
                    Denbun（電文No） = Denbun(電文No) & 選手.リーダーフリガナ & ","
                    Denbun（電文No） = Denbun(電文No) & 選手.リーダー所属名 & ","
                    Denbun（電文No） = Denbun(電文No) & 選手.パートナ表記名 & ","
                    Denbun（電文No） = Denbun(電文No) & 選手.パートナフリガナ & ","
                    Denbun（電文No） = Denbun(電文No) & 選手.パートナ所属名 & ","
                    Denbun（電文No） = Denbun(電文No) & 選手.カップル所属名 & ","

                    Denbun（電文No） = Denbun(電文No) & 採点結果.総合順位表記(s) & ","
                    Denbun（電文No） = Denbun(電文No) & 採点結果.総合得点(s).ToString("0.000") & ","

                    If 採点結果.WIN数 IsNot Nothing Then
                        Denbun（電文No） = Denbun(電文No) & 採点結果.WIN数(s) & ","
                    Else
                        Denbun（電文No） = Denbun(電文No) & ","
                    End If

                    If 採点結果.勝ちFLAG IsNot Nothing Then
                        Denbun（電文No） = Denbun(電文No) & 採点結果.勝ちFLAG(s) & ","
                    Else
                        Denbun（電文No） = Denbun(電文No) & ","
                    End If


                    For 種目 = 1 To 10
                        If 種目 <= 採点結果.種目数 Then
                            Denbun（電文No） = Denbun(電文No) & 採点結果.種目(種目).選手(s).種目得点 & ","
                        Else
                            Denbun（電文No） = Denbun(電文No) & "0" & ","
                        End If
                    Next 種目
                    s = 総件数
                End If
            Next s
        Next 順位

        '下位決勝分
        採点結果 = New 採点結果_C(区分番号, "300")
        If 採点結果.出場選手数 > 0 Then
            For 順位 = 1 To 採点結果.出場選手数

                If (順位 + 決勝進出者数) Mod 電文毎件数 = 0 Then
                    電文No = (順位 + 決勝進出者数) \ 電文毎件数
                Else
                    電文No = (順位 + 決勝進出者数) \ 電文毎件数 + 1
                End If

                For s = 1 To 総件数
                    If 採点結果.総合順位番号(s) = 順位 Then

                        Denbun（電文No） = Denbun(電文No) & "下位決勝" & ","  'ラウンド名
                        Denbun（電文No） = Denbun(電文No) & 採点結果.背番号(s) & ","

                        Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタ番号, 採点結果.背番号(s))

                        Denbun（電文No） = Denbun(電文No) & 選手.リーダー表記名 & ","
                        Denbun（電文No） = Denbun(電文No) & 選手.リーダーフリガナ & ","
                        Denbun（電文No） = Denbun(電文No) & 選手.リーダー所属名 & ","
                        Denbun（電文No） = Denbun(電文No) & 選手.パートナ表記名 & ","
                        Denbun（電文No） = Denbun(電文No) & 選手.パートナフリガナ & ","
                        Denbun（電文No） = Denbun(電文No) & 選手.パートナ所属名 & ","
                        Denbun（電文No） = Denbun(電文No) & 選手.カップル所属名 & ","

                        Denbun（電文No） = Denbun(電文No) & 採点結果.総合順位表記(s) + 決勝進出者数 & ","
                        Denbun（電文No） = Denbun(電文No) & 採点結果.総合得点(s) & ","
                        Denbun（電文No） = Denbun(電文No) & 採点結果.WIN数(s) & ","
                        Denbun（電文No） = Denbun(電文No) & 採点結果.勝ちFLAG(s) & ","

                        For 種目 = 1 To 10
                            If 種目 <= 採点結果.種目数 Then
                                Denbun（電文No） = Denbun(電文No) & 採点結果.種目(種目).選手(s).種目得点 & ","
                            Else
                                Denbun（電文No） = Denbun(電文No) & "0" & ","
                            End If
                        Next 種目
                        s = 総件数
                    End If
                Next s
            Next 順位

        End If

        Return Denbun

    End Function


End Class
