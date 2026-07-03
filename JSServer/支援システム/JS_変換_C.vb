Imports System.ComponentModel
Imports System.Windows.Forms

Public Class JS_変換_C

    Public マスタデータ As マスタデータ

    Public 元パス As String
    Public 新パス As String
    Public JS_Comp As JS_Comp_C
    Public JS_Member As JS_Member_C

    Public Sub New(元パス_ As String, 新パス_ As String)
        元パス = 元パス_
        新パス = 新パス_

        マスタデータ = New マスタデータ(新パス)


    End Sub

    Public Sub 変換(bw As BackgroundWorker, e As DoWorkEventArgs)


        JS_Comp = New JS_Comp_C
        JS_Comp.Fileread(元パス)

        'JS_Member = New JS_Member_C
        'JS_Member.Fileread(元パス)


        A_競技会マスタ作成()
        B_区分マスタ作成()

        Dim JS区分番号 As String

        For k = 1 To JS_Comp.区分数

            'キャンセルされたか調べる ==進捗ダイアログ
            If bw.CancellationPending Then
                'キャンセルされたとき
                e.Cancel = True
                Return
            End If



            JS区分番号 = String.Format("{0:D2}", CInt(JS_Comp.区分情報.区分NO(k)))

            C_ラウンドマスタ登録(JS区分番号)
            For r = 1 To 16  'ラウンド番号
                If JS_Comp.区分情報.Getヒート数(JS区分番号, r, 元パス) > 0 Then
                    D_種目マスタ登録(JS区分番号, r)
                End If
            Next r





            'ProgressChangedイベントハンドラを呼び出し、進捗ダイアログ
            'コントロールの表示を変更する
            Dim 終了P As Decimal = （k / JS_Comp.区分数） * 100

            bw.ReportProgress(終了P, 終了P.ToString("0.#") & "% 終了しました")

        Next k

        U_進行管理変換()


        T_採点進行管理変換()


        '選手マスタ変換()
        審判マスタ変換()

    End Sub

    Private Sub A_競技会マスタ作成()

        マスタデータ.A_競技会マスタ.filepath = 新パス

        マスタデータ.A_競技会マスタ.公認競技会NO = JS_Comp.競技会NO
        マスタデータ.A_競技会マスタ.競技会名 = JS_Comp.大会名称
        マスタデータ.A_競技会マスタ.開催日 = JS_Comp.開催日付
        マスタデータ.A_競技会マスタ.主催団体 = JS_Comp.主催団体
        マスタデータ.A_競技会マスタ.開催場所 = JS_Comp.開催会場

        If マスタデータ.A_競技会マスタ.登録() <> 0 Then
            MsgBox("A_競技会マスタの登録に失敗しました。")
        End If

    End Sub

    Private Sub B_区分マスタ作成()

        Dim 区分 As B_区分


        '区分数を確認
        For k = 1 To JS_Comp.区分数

            区分 = New B_区分

            区分.区分番号 = String.Format("{0:D2}", CInt(JS_Comp.区分情報.区分NO(k)))
            区分.区分記号 = JS_Comp.区分情報.区分記号(k)
            区分.区分名 = JS_Comp.区分情報.区分名(k)
            If JS_Comp.区分情報.区分名2(k) = "" Then
                区分.区分表記名 = JS_Comp.区分情報.区分名(k)
            Else
                区分.区分表記名 = JS_Comp.区分情報.区分名2(k)
            End If


            If JS_Comp.区分情報.SL区分(k) = 1 Then
                区分.カテゴリ = "S"
            Else
                区分.カテゴリ = "L"
            End If


            区分.担当審判グループ = JS_Comp.区分情報.担当審判チーム(k, 1)

            'If JS_Comp.Mod値 > 99 Then
            ' 区分.使用する選手マスタ = "02"   '02とは限らない。。。。

            'リスト番号 = (s \ JS_Comp.Mod値) + 1
            'Else
            'とりあえず、すべて01で登録する。
            '選手マスター登録時に更新する。

            区分.使用する選手マスタ = "01"
            'End If
            マスタデータ.B_区分マスタ.FileRead()
            If マスタデータ.B_区分マスタ.登録(区分) <> 0 Then
                MsgBox("B_区分マスタの登録に失敗しました" & 区分.区分番号)
            End If

            'C_ラウンドマスタ登録(区分.区分番号)

        Next k

    End Sub

    Private Sub C_ラウンドマスタ登録(区分No As String)

        Dim ラウンド As C_ラウンド

        For r = 1 To 16      ' ７までしか設定されていない競技会がある模様。   
            ラウンド = New C_ラウンド

            If JS_Comp.区分情報.Getヒート数(CInt(区分No), r, 元パス) > 0 Then
                ラウンド.区分番号 = 区分No
                ラウンド.ラウンド番号 = ラウンド番号変換(r)
                If r = 7 Or r > 10 Then    '決勝と、　同点決勝は順位法という前提
                    ラウンド.採点方式 = "順位法"
                Else
                    ラウンド.採点方式 = "チェック法"
                End If

                If r <= 10 Then
                    ラウンド.担当審判グループ = JS_Comp.区分情報.担当審判チーム(区分No, r)
                    ラウンド.UP予定数 = JS_Comp.区分情報.GetUP予定数(CInt(区分No), r)

                Else
                    Dim RNO As String = Strings.Right(CStr(r), 1)
                    ラウンド.担当審判グループ = JS_Comp.区分情報.担当審判チーム(区分No, RNO)
                    ラウンド.UP予定数 = 0

                End If

                ラウンド.リアルタイムFLAG = ""
                ラウンド.CaliMax = 10
                ラウンド.CaliMin = 0


                マスタデータ.C_ラウンドマスタ.FileRead()
                If マスタデータ.C_ラウンドマスタ.登録(ラウンド) <> 0 Then
                    MsgBox("C_ラウンドマスタの登録に失敗しました。" & 区分No & "_" & r)
                End If
            End If
        Next r

    End Sub

    Private Sub D_種目マスタ登録(区分No As String, JSラウンド番号 As Integer)
        Dim 種目 As D_種目

        If JSラウンド番号 <= 10 Then

            Dim Temp種目順 As Integer = 1
            For d = 1 To 10
                If JS_Comp.区分情報.開催種目(CInt(区分No), d) <> "" And JS_Comp.区分情報.開催種目(CInt(区分No), d) <> " " Then
                    If CInt(JS_Comp.区分情報.開催種目(CInt(区分No), d)) <= CInt(JSラウンド番号) Then

                        種目 = New D_種目
                        種目.区分番号 = 区分No
                        種目.ラウンド番号 = ラウンド番号変換(JSラウンド番号)

                        種目.種目順 = Temp種目順
                        Temp種目順 = Temp種目順 + 1

                        'If JS_Comp.区分情報.SL区分(CInt(区分No)) = "1" Then
                        ' 種目.種目順 = Temp種目順
                        'Temp種目順 = Temp種目順 + 1
                        'Else 'ラテンの時
                        '   種目.種目順 = d - 5
                        'End If
                        種目.種目記号 = 種目記号変換(d)
                        種目.SG種別 = "G"
                        種目.ヒート数 = JS_Comp.区分情報.Getヒート数(CInt(区分No）, JSラウンド番号, 元パス)
                        種目.担当審判グループ = JS_Comp.区分情報.担当審判チーム(CInt(区分No）, JSラウンド番号)
                        種目.CaliMax = 10
                        種目.CaliMin = 0

                        マスタデータ.D_種目マスタ.FileRead()
                        If マスタデータ.D_種目マスタ.登録(種目） <> 0 Then
                            MsgBox("D_種目マスタの登録に失敗しました。" & 区分No & "_" & JSラウンド番号 & "_" & d)
                        End If

                    ElseIf JS_Comp.区分情報.開催種目(CInt(区分No), d) = 9 And
                   JSラウンド番号 >= 5 Then  '最終予選から

                        種目 = New D_種目
                        種目.区分番号 = 区分No
                        種目.ラウンド番号 = ラウンド番号変換(JSラウンド番号)

                        種目.種目順 = Temp種目順
                        Temp種目順 = Temp種目順 + 1

                        'If JS_Comp.区分情報.SL区分(CInt(区分No)) = "1" Then
                        '種目.種目順 = d
                        'Else 'ラテンの時
                        '   種目.種目順 = d - 5
                        ' End If

                        種目.種目記号 = 種目記号変換(d)
                        種目.SG種別 = "G"
                        種目.ヒート数 = JS_Comp.区分情報.Getヒート数(CInt(区分No）, JSラウンド番号, 元パス)
                        種目.担当審判グループ = JS_Comp.区分情報.担当審判チーム(CInt(区分No）, JSラウンド番号)
                        種目.CaliMax = 10
                        種目.CaliMin = 0


                        マスタデータ.D_種目マスタ.FileRead()
                        If マスタデータ.D_種目マスタ.登録(種目） <> 0 Then
                            MsgBox("D_種目マスタの登録に失敗しました。" & 区分No & "_" & JSラウンド番号 & "_" & d)
                        End If

                    End If
                End If
            Next d

        Else
            '同点決勝の場合は、元の種目リストをコピーする

            Dim 元種目 = New D_種目
            Dim 元ラウンド番号 As String = Strings.Right(CStr(JSラウンド番号), 1)  '元のラウンド番号
            Dim 種目記号リスト() = Nothing

            Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分No, ラウンド番号変換(元ラウンド番号), 種目記号リスト)

            For d = 1 To 種目数

                種目 = New D_種目
                種目.区分番号 = 区分No
                種目.ラウンド番号 = ラウンド番号変換(JSラウンド番号)
                種目.種目順 = d
                種目.種目記号 = 種目記号リスト(d)
                種目.SG種別 = "G"
                種目.ヒート数 = JS_Comp.区分情報.Getヒート数(CInt(区分No）, JSラウンド番号, 元パス)
                種目.担当審判グループ = JS_Comp.区分情報.担当審判チーム(CInt(区分No）, 元ラウンド番号)
                種目.CaliMax = 10
                種目.CaliMin = 0

                マスタデータ.D_種目マスタ.FileRead()
                If マスタデータ.D_種目マスタ.登録(種目） <> 0 Then
                    MsgBox("D_種目マスタの登録に失敗しました。" & 区分No & "_" & JSラウンド番号 & "_" & d)
                End If


            Next d

        End If


    End Sub

    Private Sub T_採点進行管理変換()

        Dim 採点進行 As T_採点進行

        For k = 1 To JS_Comp.区分数
            Dim JS区分番号 = JS_Comp.区分情報.区分NO(k)

            For r = 1 To 7
                If CInt(JS_Comp.区分情報.競技番号_区分(JS区分番号, r)) <> 0 Then
                    採点進行 = New T_採点進行

                    採点進行.競技番号 = String.Format("{0:D3}", CInt(JS_Comp.区分情報.競技番号_区分(k, r)))
                    採点進行.競技番号枝番 = "00"
                    採点進行.区分番号 = String.Format("{0:D2}", CInt(JS区分番号))
                    採点進行.ラウンド番号 = ラウンド番号変換(r)
                    採点進行.リアルタイムFLAG = ""
                    採点進行.ステータス = "準備前"

                    マスタデータ.T_採点進行管理.FileRead()
                    If マスタデータ.T_採点進行管理.登録(採点進行) <> 0 Then
                        MsgBox("T_採点進行管理の登録に失敗しました。" & 採点進行.競技番号)
                    End If

                End If
            Next r

            '同点決勝分
            For r = 11 To 16
                Dim JS_HFile = New JS_HFile_C()
                If JS_HFile.Fileread(元パス, String.Format("{0:D2}", JS区分番号), r) = 0 Then
                    'Hファイルがあった時

                    Dim RNO As String = Strings.Right(CStr(r), 1)  '元のラウンド番号

                    Dim 元採点進行 = New T_採点進行
                    元採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(String.Format("{0:D2}", CInt(JS区分番号)), ラウンド番号変換(RNO))


                    採点進行 = New T_採点進行

                    採点進行.競技番号 = 元採点進行.競技番号
                    採点進行.競技番号枝番 = "01"
                    採点進行.区分番号 = String.Format("{0:D2}", CInt(JS区分番号))
                    採点進行.ラウンド番号 = ラウンド番号変換(r)
                    採点進行.リアルタイムFLAG = ""
                    採点進行.ステータス = "準備前"

                    マスタデータ.T_採点進行管理.FileRead()
                    If マスタデータ.T_採点進行管理.登録(採点進行) <> 0 Then
                        MsgBox("T_採点進行管理の登録に失敗しました。" & 採点進行.競技番号)
                    End If

                    元採点進行 = Nothing

                End If

                JS_HFile = Nothing

            Next r

        Next k


    End Sub


    Private Sub U_進行管理変換()

        Dim 進行 As U_進行

        For k = 1 To JS_Comp.区分数
            Dim JS区分番号 = JS_Comp.区分情報.区分NO(k)

            For r = 1 To 7
                If CInt(JS_Comp.区分情報.競技番号_区分(JS区分番号, r)) <> 0 Then
                    進行 = New U_進行

                    進行.競技番号 = String.Format("{0:D3}", CInt(JS_Comp.区分情報.競技番号_区分(k, r)))
                    進行.競技番号枝番 = "00"

                    '種目のループ
                    Dim 種目記号リスト() = Nothing
                    'マスタデータ.D_種目マスタ.FileRead()

                    For d = 1 To マスタデータ.D_種目マスタ.Get_種目数(String.Format("{0:D2}", CInt(JS区分番号)), ラウンド番号変換(r), 種目記号リスト)
                        進行.種目順 = d

                        'ヒート数のループ
                        Dim 種目 = マスタデータ.D_種目マスタ.Get_種目Class(String.Format("{0:D2}", CInt(JS区分番号)), ラウンド番号変換(r), d)
                        For h = 1 To 種目.ヒート数
                            進行.ヒート番号 = h

                            進行.ステータス = "準備前"

                            マスタデータ.U_進行管理.FileRead()
                            If マスタデータ.U_進行管理.登録(進行) <> 0 Then
                                MsgBox("U_進行管理の登録に失敗しました。" & 進行.競技番号)
                            End If
                        Next h
                    Next d

                End If
            Next r
        Next k


    End Sub


    Private Function ラウンド番号変換(JSラウンド番号 As Integer) As String
        Dim rc As String = ""

        Select Case JSラウンド番号
            Case 1
                rc = "010"
            Case 2
                rc = "020"
            Case 3
                rc = "030"
            Case 4
                rc = "040"
            Case 5
                rc = "050"
            Case 6
                rc = "200"
            Case 7
                rc = "400"

            Case 11
                rc = "011"
            Case 12
                rc = "021"
            Case 13
                rc = "031"
            Case 14
                rc = "041"
            Case 15
                rc = "051"
            Case 16
                rc = "201"

        End Select

        Return rc

    End Function

    Private Function JSラウンド番号変換(ラウンド番号 As Integer) As String
        Dim rc As String = ""

        Select Case ラウンド番号
            Case "010"
                rc = 1
            Case "020"
                rc = 2
            Case "030"
                rc = 3
            Case "040"
                rc = 4
            Case "050"
                rc = 5
            Case "200"
                rc = 6
            Case "400"
                rc = 7

            Case "011"
                rc = 11
            Case "021"
                rc = 12
            Case "031"
                rc = 13
            Case "041"
                rc = 14
            Case "051"
                rc = 15
            Case "201"
                rc = 16


        End Select

        Return rc

    End Function


    Private Function 種目記号変換(種目番号 As Integer) As String
        '種目番号 1-10 を渡すと、種目記号を返す

        Dim rc As String = ""

        Select Case 種目番号
            Case 1
                rc = "W"
            Case 2
                rc = "T"
            Case 3
                rc = "V"
            Case 4
                rc = "F"
            Case 5
                rc = "Q"
            Case 6
                rc = "S"
            Case 7
                rc = "C"
            Case 8
                rc = "R"
            Case 9
                rc = "P"
            Case 10
                rc = "J"
        End Select

        Return rc

    End Function

    Public Sub 選手マスタ変換(bw As BackgroundWorker, e As DoWorkEventArgs)

        JS_Comp = New JS_Comp_C
        JS_Comp.Fileread(元パス)

        JS_Member = New JS_Member_C
        JS_Member.Fileread(元パス)

        Dim 区分データ As B_区分


        Dim 選手 As 選手
        Dim リスト番号 As Integer = 0

        'For l = 1 To (1000 / JS_Comp.Mod値)
        'リスト番号 = String.Format("{0:D2}", l)
        For s = 1 To 1000



            'キャンセルされたか調べる ==進捗ダイアログ
            If bw.CancellationPending Then
                'キャンセルされたとき
                e.Cancel = True
                Return
            End If



            If JS_Member.リーダー名_漢字(s) <> "" Then

                選手 = New 選手

                リスト番号 = (s \ JS_Comp.Mod値) + 1

                Dim 背番号 = CInt(JS_Member.背番号(s)) Mod JS_Comp.Mod値

                選手.背番号 = 背番号
                選手.リーダー氏名 = JS_Member.リーダー名_漢字(s)
                選手.リーダーフリガナ = JS_Member.リーダー名_カナ(s)
                選手.リーダー表記名 = JS_Member.リーダー名_漢字(s)
                選手.リーダー所属名 = JS_Member.所属(s)

                選手.パートナ氏名 = JS_Member.パートナー名_漢字(s)
                選手.パートナフリガナ = JS_Member.パートナー名_カナ(s)
                選手.パートナ表記名 = JS_Member.パートナー名_漢字(s)
                選手.パートナ所属名 = JS_Member.所属(s)
                選手.カップル所属名 = JS_Member.所属(s)

                For k = 1 To 40
                    選手.エントリー区分(k) = JS_Member.出場区分(s, k)

                    '区分マスターの選手リスト番号を更新
                    If JS_Member.出場区分(s, k) <> "" And JS_Member.出場区分(s, k) <> " " Then
                        区分データ = マスタデータ.B_区分マスタ.Get区分C(k)

                        If 区分データ IsNot Nothing Then
                            区分データ.使用する選手マスタ = リスト番号.ToString("00")
                            マスタデータ.B_区分マスタ.登録(区分データ)
                        End If
                        区分データ = Nothing
                    End If
                Next k

                マスタデータ.選手マスタ.FileRead()
                If マスタデータ.選手マスタ.選手登録(リスト番号, 選手） <> 0 Then
                    MsgBox("選手マスタの登録に失敗しました。" & JS_Member.背番号(s))
                End If


            End If




            'ProgressChangedイベントハンドラを呼び出し、進捗ダイアログ
            'コントロールの表示を変更する
            Dim 終了P As Decimal = （s / 1000） * 100

            bw.ReportProgress(終了P, 終了P.ToString("0.#") & "% 終了しました")

        Next s
        'Next l


    End Sub

    Private Sub 審判マスタ変換()
        Dim 審判 As 審判

        For j = 1 To 50
            If JS_Comp.審判員情報.審判記号(j) <> "" Then
                審判 = New 審判
                審判.ジャッジ記号 = JS_Comp.審判員情報.審判記号(j)
                審判.ジャッジ氏名 = JS_Comp.審判員情報.審判員名(j)
                '審判.ジャッジフリガナ = 
                審判.ジャッジ表記名 = JS_Comp.審判員情報.審判員名(j)
                '審判.ジャッジ所属
                審判.言語 = "J"
                For t = 1 To 25
                    If JS_Comp.審判員情報.審判員担当チーム(j, t) = "0" Then
                        審判.審判チーム(t) = ""
                    Else
                        審判.審判チーム(t) = "1"
                    End If
                Next t

                マスタデータ.審判員マスタ.FileRead()
                If マスタデータ.審判員マスタ.審判員登録(審判) <> 0 Then
                    MsgBox("審判マスタの登録に失敗しました。" & JS_Comp.審判員情報.審判記号(j))
                End If

            End If

        Next j

    End Sub


    '========================ヒート表と結果の変換

    Public Function Heat結果変換(区分番号 As String, ラウンド番号 As String)
        'INPUT 区分番号( ex "11", "12" )
        '      ラウンド番号 ( ex "010",  "400" )
        'OUTPUT  エラーなし  RC=0,   ヒート表無 RC=1   結果無 RC=2

        Dim RC = 0

        Dim JS_HFile As JS_HFile_C
        JS_HFile = New JS_HFile_C
        Dim RC_H = JS_HFile.Fileread(元パス, 区分番号, JSラウンド番号変換(ラウンド番号))

        If RC_H = 0 Then
            'ファイルがあったとき

            Dim ヒート表_背番号順(,) = Nothing
            JS_HFile.Getヒート表_背番号順(ヒート表_背番号順)


            Dim 種目記号リスト() = Nothing
            Dim 種目数 As Integer = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

            Dim E_ヒート表マスタ = New E_ヒート表マスタ(マスタデータ.Z_システム設定.Comp_filepath)
            E_ヒート表マスタ.Read(区分番号, ラウンド番号)


            If 種目数 > 0 Then



                For s = 1 To JS_HFile.出場組数

                    Dim Eヒート = New E_ヒート表
                    Eヒート.背番号 = ヒート表_背番号順(s, 0)
                    For d = 1 To 種目数
                        Eヒート.ヒート番号(d) = ヒート表_背番号順(s, 1)
                    Next d

                    E_ヒート表マスタ.登録(Eヒート, 区分番号, ラウンド番号)
                    E_ヒート表マスタ.Read(区分番号, ラウンド番号)
                Next s

            End If


            '結果ファイルの作成
            Dim JS_DFile = New JS_DFile_C
            Dim RC_D = JS_DFile.Fileread(元パス, 区分番号, JSラウンド番号変換(ラウンド番号))

            If RC_D = 0 Then

                '結果ファイルがあった時


                'ジャッジリスト取得

                Dim ジャッジ数 As Integer = 0
                Dim ジャッジ記号リスト() As String
                ReDim ジャッジ記号リスト(JS_DFile.審判員数)

                For j = 1 To マスタデータ.審判員マスタ.Get_登録済み審判員数
                    If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(マスタデータ.C_ラウンドマスタ.Get担当審判グループ(区分番号, ラウンド番号)) <> "" Then
                        ジャッジ数 = ジャッジ数 + 1
                        ジャッジ記号リスト(ジャッジ数) = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ記号
                    End If
                Next j


                '採点結果の記入

                Dim S_採点結果_J = New S_採点結果_J(マスタデータ.Z_システム設定.Comp_filepath)

                S_採点結果_J.区分番号 = 区分番号
                S_採点結果_J.ラウンド番号 = ラウンド番号
                S_採点結果_J.採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)


                S_採点結果_J.総種目数 = JS_DFile.種目数

                For d = 1 To JS_DFile.種目数
                    S_採点結果_J.種目記号 = 種目記号リスト(d)

                    For j = 1 To ジャッジ数

                        S_採点結果_J.ジャッジ記号 = ジャッジ記号リスト(j)


                        S_採点結果_J.選手結果初期化(E_ヒート表マスタ.登録済みレコード数)

                        For s = 1 To E_ヒート表マスタ.登録済みレコード数
                            S_採点結果_J.S_採点結果_選手_J(s).背番号 = E_ヒート表マスタ.リスト(s).背番号
                            S_採点結果_J.S_採点結果_選手_J(s).点数 = JS_DFile.Get_詳細点数(E_ヒート表マスタ.リスト(s).背番号, d, j)
                            S_採点結果_J.S_採点結果_選手_J(s).ヒート番号 = E_ヒート表マスタ.Get_ヒート番号(d, E_ヒート表マスタ.リスト(s).背番号)
                        Next s

                        S_採点結果_J.JSON書き出し()

                    Next j
                Next d
            Else

                '結果ファイルが無い時
                RC = 2

            End If






        Else
            'ヒート表無し

            RC = 1

        End If

        Return RC

    End Function



End Class
