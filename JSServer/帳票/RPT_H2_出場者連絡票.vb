Public Class RPT_H2_出場者連絡票

    Public Sub 印刷(ByVal 区分番号 As String, ByVal ラウンド番号 As String, ByVal マスタデータ As マスタデータ, ByVal 配布先() As String)

        'ヒート表が作成されているか確認
        If マスタデータ.E_ヒート表マスタ.FileCheck(区分番号, ラウンド番号) = False Then
            MsgBox("まだヒート表が作成されていません。区分番号:” & 区分番号 & " ラウンド:" & マスタデータ.Get_ラウンド名(ラウンド番号)）

            Exit Sub
        Else
            'ヒート表ファイルの読込み
            マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
        End If


        Dim RPT As RPT_R2_横マスタ01
        RPT = New RPT_R2_横マスタ01(マスタデータ.Z_システム設定.言語)


        '===パラメータ作成
        'パラメータクラスの作成
        Dim Parm As RPT_Parm_H2
        Parm = New RPT_Parm_H2


        共通Parm設定(区分番号, ラウンド番号, マスタデータ, Parm)

        Dim 全行数 As Integer = 0

        If マスタデータ.Z_システム設定.言語 = "E" Then
            Parm.タイトル = "Couples Heat No"
            全行数 = 20　　'２０のまま
        Else
            Parm.タイトル = "出場者連絡票"
            全行数 = 24　　'２０を２４に変更
        End If




        For h = 1 To 全行数
            Parm.ヒートText(h) = ""
        Next h


        Dim 行数 As Integer = 1  '１～２０  ２４に変更

        Dim 選手マスタ番号 As String = マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ

        Dim 種目記号リスト() = Nothing

        '種目の色付け,ヒート数判定
        Dim ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(1)
        Dim 全種目同ヒート数 As Boolean = True

        For s = 1 To マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)
            Parm.競技種目_色(s) = "Orange"  '"Yellow"
            Parm.競技種目2_色(s) = "Orange"  ' "Yellow"

            If ヒート数 <> マスタデータ.E_ヒート表マスタ.Getヒート数(s) Then
                全種目同ヒート数 = False
            End If
        Next s

        If 全種目同ヒート数 = True Then
            Parm.ヒート数 = CStr(ヒート数) & " Heat"
        Else
            Parm.ヒート数 = ""
        End If



        '種目数分ループ
        For s = 1 To マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

            '20行目を種目が跨るか確認
            If 行数 + マスタデータ.E_ヒート表マスタ.Getヒート数(s) > 全行数 Then
                '印刷して、行数をゼロクリア

                印刷実行(RPT, Parm, 配布先)

                行数 = 1

                For h = 1 To 全行数
                    Parm.ヒートText(h) = ""
                Next h

            End If


            Parm.ヒートText(行数) = マスタデータ.Z_システム設定.Get_種目名称(種目記号リスト(s)).種目名_E
            行数 = 行数 + 1


            ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(s)
            'ヒート数分ループ
            For h = 1 To ヒート数

                Parm.ヒートText(行数) = "    "
                Parm.ヒートText(行数) = Parm.ヒートText(行数) & Strings.Format("{0,2}", h) & " Heat" & "  "

                Dim 背番号リスト() As String = Nothing
                マスタデータ.E_ヒート表マスタ.Get_背番号リスト(s, h, 背番号リスト)

                For i = 1 To UBound(背番号リスト)
                    Parm.ヒートText(行数) = Parm.ヒートText(行数) & 背番号リスト(i).PadLeft(4)
                Next i

                行数 = 行数 + 1

            Next h

        Next s

        If 行数 > 1 Then
            印刷実行(RPT, Parm, 配布先)
        End If

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


    'Byte単位で文字を取得する関数
    Public Shared Function LeftB(ByVal str As String, ByVal byteCount As Integer) As String
        Dim hEncode As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Dim btBytes As Byte() = hEncode.GetBytes(str)

        If byteCount <= btBytes.Length Then
            Return hEncode.GetString(btBytes, 0, byteCount)
        End If

        Return str
    End Function

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
                Parm.ラウンド区分_色(1) = "Orange"  ' "Yellow"
            Case "02"
                Parm.ラウンド区分(2) = ラウンド名
                Parm.ラウンド区分_色(2) = "Orange"  ' "Yellow"
            Case "03"
                Parm.ラウンド区分(3) = ラウンド名
                Parm.ラウンド区分_色(3) = "Orange"  ' "Yellow"
            Case "04"
                Parm.ラウンド区分(4) = ラウンド名
                Parm.ラウンド区分_色(4) = "Orange"  ' "Yellow"
            Case "05"
                Parm.ラウンド区分(4) = ラウンド名
                Parm.ラウンド区分_色(4) = "Orange"  ' "Yellow"
            Case "09"
                Parm.ラウンド区分(4) = ラウンド名
                Parm.ラウンド区分_色(4) = "Orange"  ' "Yellow"
            Case "10"
                Parm.ラウンド区分(4) = ラウンド名
                Parm.ラウンド区分_色(4) = "Orange"  ' "Yellow"
            Case "20"
                Parm.ラウンド区分(5) = ラウンド名
                Parm.ラウンド区分_色(5) = "Orange"  ' "Yellow"
            Case "30"
                Parm.ラウンド区分(6) = ラウンド名
                Parm.ラウンド区分_色(6) = "Orange"  ' "Yellow"
            Case "40"
                Parm.ラウンド区分(7) = ラウンド名
                Parm.ラウンド区分_色(7) = "Orange"  ' "Yellow"
        End Select

        Dim ラウンドClass As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号)

        'Parm.ヒート数 = "2"
        If マスタデータ.Z_システム設定.言語 = "E" Then
            Parm.出場組数 = CStr(マスタデータ.C_ラウンドマスタ.出場組数(区分番号, ラウンド番号)) & "Couples"
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


End Class
