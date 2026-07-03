Imports System.Threading
Public Class F302_BRHTML作成


    Private 区分番号 As String
    Private 現ラウンド番号 As String
    Private 次ラウンド番号 As String

    Private 現ラウンドヒート割 As String
    Private 次ラウンドヒート割 As String

    Private 現ラウンド採点方式 As String
    Private 次ラウンド採点方式 As String


    Private マスタデータ As マスタデータ

    Private Sub F302_BRHTML作成_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False

    End Sub

    Public Sub 設定(区分番号_ As String, 現ラウンド番号_ As String)

        マスタデータ = New マスタデータ

        区分番号 = 区分番号_
        現ラウンド番号 = 現ラウンド番号_

        現ラウンドヒート割 = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, 現ラウンド番号).ヒート割方式
        現ラウンド採点方式 = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, 現ラウンド番号).採点方式




        Me.LB_区分名.Text = マスタデータ.B_区分マスタ.Get区分表記名(区分番号)


    End Sub


    Private Async Sub PB_詳細_Click(sender As Object, e As EventArgs) Handles PB_詳細.Click



        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            詳細_印刷(区分番号, 現ラウンド番号, False)

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("Detail作成終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す



    End Sub


    Private Sub 詳細_印刷(対象区分番号 As String, 対象ラウンド番号 As String, 一括FLAG As Boolean)


        Dim 採点結果 As 採点結果_C
        採点結果 = New 採点結果_C(対象区分番号, 対象ラウンド番号)

        If Strings.Left(採点結果.採点方式, 3) = "AJS" Then


            Dim AJS得点詳細 As AJS得点詳細
            AJS得点詳細 = New AJS得点詳細
            AJS得点詳細.CreateHTML(採点結果, "LOCAL")
            AJS得点詳細.CreateHTML(採点結果, "REMOTE")

            採点結果 = Nothing
            AJS得点詳細 = Nothing

        ElseIf Strings.Left(採点結果.採点方式, 3) = "BJS" Then

            'ブレイキンのカテゴリのラウンド方式が"S"の時は、ソロ順位を表示する。
            Dim 対象カテゴリ番号 = マスタデータ.BR_グループマスタ.Getカテゴリ番号(対象区分番号, 対象ラウンド番号)
            If 対象カテゴリ番号 Is Nothing Then
                MsgBox("区分:" & 対象区分番号 & " ラウンド:" & 対象ラウンド番号 & " のブレイキンカテゴリが登録されていません。")
                Exit Sub
            End If


            If 採点結果.マスタデータ.BR_カテゴリマスタ.Getラウンド方式名(対象カテゴリ番号) = "S" Then

                Dim BJS得点詳細ソロ As BJS得点詳細ソロ
                BJS得点詳細ソロ = New BJS得点詳細ソロ
                BJS得点詳細ソロ.CreateHTML(採点結果, "LOCAL")
                BJS得点詳細ソロ.CreateHTML(採点結果, "REMOTE")
                BJS得点詳細ソロ = Nothing

            Else

                Dim BJS得点詳細 As BJS得点詳細_V2
                BJS得点詳細 = New BJS得点詳細_V2
                BJS得点詳細.CreateHTML(採点結果, "LOCAL")
                BJS得点詳細.CreateHTML(採点結果, "REMOTE")
                BJS得点詳細 = Nothing

            End If

            採点結果 = Nothing


        ElseIf 採点結果.採点方式 = "チェック法" Then

            Dim チェック法得点詳細 As チェック法得点詳細
            チェック法得点詳細 = New チェック法得点詳細
            チェック法得点詳細.CreateHTML(採点結果, "LOCAL")
            チェック法得点詳細.CreateHTML(採点結果, "REMOTE")



        ElseIf Strings.Left(採点結果.採点方式, 4) = "BJPR" Then

            Dim BJS得点詳細ソロ As BJS得点詳細ソロ
            BJS得点詳細ソロ = New BJS得点詳細ソロ
            BJS得点詳細ソロ.CreateHTML(採点結果, "LOCAL")
            BJS得点詳細ソロ.CreateHTML(採点結果, "REMOTE")
            BJS得点詳細ソロ = Nothing


        End If



        'ラウンド一覧更新
        Dim ラウンドHTML As ラウンド一覧
        ラウンドHTML = New ラウンド一覧
        ラウンドHTML.CreateHTML(対象区分番号, "LOCAL")
        ラウンドHTML.CreateHTML(対象区分番号, "REMOTE")

        ラウンドHTML = Nothing


        '区分一覧更新
        Dim 区分一覧HTML As 区分一覧HTML
        区分一覧HTML = New 区分一覧HTML
        区分一覧HTML.CreateHTML("LOCAL")
        区分一覧HTML.CreateHTML("REMOTE")

        区分一覧HTML = Nothing


        'ブラウザーで開く
        'Dim url As String = マスタデータ.Z_システム設定.HTML_filepath & "\" & "index.html"

        If 一括FLAG = False Then

            Dim url As String = マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult\Result_" & 区分番号 & "_" & 現ラウンド番号 & ".html"
            System.Diagnostics.Process.Start(url)

        Else


        End If



    End Sub

    Private Async Sub PB_一括_Click(sender As Object, e As EventArgs) Handles PB_一括.Click
        'ステータスが「採点済み」の区分で、区分番号の下一桁が「１」のものを全て作成する。
        '採点方式が BJSのものだけ


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            'ブレイキンのグループ一覧から、カテゴリ毎、ラウンド毎（区分は１つだけ）を選択して作成する。
            Dim 作成済みカテゴリ(100) As String  'カテゴリ番号&ラウンド番号
            Dim 作成済み数 As Integer = 0

            For g = 1 To マスタデータ.BR_グループマスタ.登録済みレコード数

                Dim findFlag As Boolean = False
                For k = 1 To 作成済み数
                    If 作成済みカテゴリ(k) = マスタデータ.BR_グループマスタ.リスト(g).カテゴリ番号 & マスタデータ.BR_グループマスタ.リスト(g).ラウンド番号 Then
                        '作成済みの時は何もしない

                        findFlag = True
                        k = 作成済み数
                    End If
                Next k

                If findFlag = False Then
                    If マスタデータ.T_採点進行管理.Get_採点進行Class(マスタデータ.BR_グループマスタ.リスト(g).区分番号, マスタデータ.BR_グループマスタ.リスト(g).ラウンド番号).ステータス = "採点済み" Then
                        If マスタデータ.C_ラウンドマスタ.Get採点方式(マスタデータ.BR_グループマスタ.リスト(g).区分番号, マスタデータ.BR_グループマスタ.リスト(g).ラウンド番号).Substring(0, 3) = "BJS" Then

                            詳細_印刷(マスタデータ.BR_グループマスタ.リスト(g).区分番号, マスタデータ.BR_グループマスタ.リスト(g).ラウンド番号, True)


                            作成済み数 = 作成済み数 + 1
                            作成済みカテゴリ(作成済み数) = マスタデータ.BR_グループマスタ.リスト(g).カテゴリ番号 & マスタデータ.BR_グループマスタ.リスト(g).ラウンド番号
                        End If

                    End If
                End If



            Next g





            '    For t = 1 To マスタデータ.T_採点進行管理.登録済みレコード数
            '    If マスタデータ.T_採点進行管理.リスト(t).ステータス = "採点済み" And
            '    マスタデータ.T_採点進行管理.リスト(t).区分番号.Substring(1, 1) = "1" Then

            '    If マスタデータ.C_ラウンドマスタ.Get採点方式(マスタデータ.T_採点進行管理.リスト(t).区分番号, マスタデータ.T_採点進行管理.リスト(t).ラウンド番号).Substring(0, 3) = "BJS" Then

            '    詳細_印刷(マスタデータ.T_採点進行管理.リスト(t).区分番号, マスタデータ.T_採点進行管理.リスト(t).ラウンド番号, True)

            '    End If

            '    End If
            '   Next t




        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("一括HTML作成終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す


        Dim url As String = マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult\index.html"
        System.Diagnostics.Process.Start(url)


    End Sub





    Private Async Sub PB_SFTP_Click(sender As Object, e As EventArgs) Handles PB_SFTP.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = task.Run(
        Sub()

            SFTP同期()

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("サーバUP終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す


    End Sub

    Private Sub SFTP同期()

        Dim SFTP As SFTP_C
        SFTP = New SFTP_C

        SFTP.Sync()

        SFTP = Nothing


    End Sub

    Private Sub PB_INIT_Click(sender As Object, e As EventArgs) Handles PB_INIT.Click
        '競技会一覧の初期バージョンをサーバーへUPLOAD

        Dim 競技会一覧HTML As 競技会一覧HTML
        競技会一覧HTML = New 競技会一覧HTML

        競技会一覧HTML.CreateHTML("REMOTE", "初期化")

        競技会一覧HTML = Nothing

    End Sub

    Private Async Sub PB_一覧CSV作成_Click(sender As Object, e As EventArgs) Handles PB_一覧CSV作成.Click

        'ブレイキンの区分一覧をCSVで作成する。
        '採点方式が BJSのものだけ


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            Dim CSV() As 一覧CSV
            ReDim CSV(マスタデータ.T_採点進行管理.登録済みレコード数)

            CSV(0) = New 一覧CSV
            CSV(0).競技会NO = マスタデータ.A_競技会マスタ.公認競技会NO
            CSV(0).競技会名 = マスタデータ.A_競技会マスタ.競技会名
            CSV(0).開催日 = マスタデータ.A_競技会マスタ.開催日
            CSV(0).主催団体 = マスタデータ.A_競技会マスタ.主催団体
            CSV(0).開催場所 = マスタデータ.A_競技会マスタ.開催場所



            For t = 1 To マスタデータ.T_採点進行管理.登録済みレコード数
                If マスタデータ.C_ラウンドマスタ.Get採点方式(マスタデータ.T_採点進行管理.リスト(t).区分番号, マスタデータ.T_採点進行管理.リスト(t).ラウンド番号).Substring(0, 3) = "BJS" Then

                    CSV(t) = New 一覧CSV

                    CSV(t).競技NO = マスタデータ.T_採点進行管理.リスト(t).競技番号
                    CSV(t).競技枝番 = マスタデータ.T_採点進行管理.リスト(t).競技番号枝番

                    Dim カテゴリ番号 As String = マスタデータ.BR_グループマスタ.Getカテゴリ番号(マスタデータ.T_採点進行管理.リスト(t).区分番号, マスタデータ.T_採点進行管理.リスト(t).ラウンド番号)
                    Dim BRカテゴリ As BR_カテゴリ = マスタデータ.BR_カテゴリマスタ.GetカテゴリC(カテゴリ番号)
                    Dim BRグループ As BR_グループ = マスタデータ.BR_グループマスタ.GetグループC(カテゴリ番号)

                    CSV(t).カテゴリNO = BRカテゴリ.カテゴリ番号
                    CSV(t).カテゴリ名 = BRカテゴリ.カテゴリ表記名

                    Dim 区分 As B_区分 = マスタデータ.B_区分マスタ.Get区分C(マスタデータ.T_採点進行管理.リスト(t).区分番号)
                    Dim ラウンド As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分.区分番号, マスタデータ.T_採点進行管理.リスト(t).ラウンド番号)

                    CSV(t).区分NO = 区分.区分番号
                    CSV(t).区分名 = 区分.区分表記名
                    CSV(t).ラウンドNO = ラウンド.ラウンド番号
                    CSV(t).ラウンド名 = マスタデータ.Get_ラウンド名(ラウンド.ラウンド番号)
                    CSV(t).ラウンド名2 = マスタデータ.BR_グループマスタ.Getラウンド表記名(カテゴリ番号, 区分.区分番号, ラウンド.ラウンド番号)

                    'CSV(t).ジャッジGP = ラウンド.担当審判グループ

                    CSV(t).ジャッジGP = マスタデータ.D_種目マスタ.Get_種目Class(区分.区分番号, ラウンド.ラウンド番号, 1).担当審判グループ

                    Dim 審判員記号リスト() = Nothing
                    マスタデータ.審判員マスタ.Get_審判員記号(CSV(t).ジャッジGP, 審判員記号リスト)

                    Dim ジャッジ名 As String = ""
                    For j = 1 To UBound(審判員記号リスト)
                        ジャッジ名 = ジャッジ名 & "(" & 審判員記号リスト(j) & ") " & マスタデータ.審判員マスタ.Get_ジャッジ表記名(審判員記号リスト(j)) & " "

                    Next j

                    CSV(t).ジャッジ名 = ジャッジ名


                    CSV(t).採点方式 = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分.区分番号, ラウンド.ラウンド番号).採点方式
                    マスタデータ.J_新審判設定.Set_新審判基準VER(CSV(t).採点方式)
                    CSV(t).勝敗ラウンド数 = マスタデータ.J_新審判設定.勝敗ラウンド数

                    Dim 種目記号リスト() = Nothing
                    Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分.区分番号, ラウンド.ラウンド番号, 種目記号リスト)
                    CSV(t).全ラウンド数 = 種目数

                    'PCS設定確認
                    CSV(t).PCS確認 = "OK PCS"
                    If マスタデータ.F_審判担当PCSマスタ.FileCheck(区分.区分番号, ラウンド.ラウンド番号) Then

                    Else
                        CSV(t).PCS確認 = "NG PCS未設定"
                    End If
                    マスタデータ.F_審判担当PCSマスタ.Read(区分.区分番号, ラウンド.ラウンド番号)

                    For j = 1 To UBound(審判員記号リスト)
                        Dim 審判PCS As F_審判担当PCS = マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員記号リスト(j))

                        If 審判PCS IsNot Nothing Then
                            For d = 1 To 種目数
                                If 審判PCS.担当PCS番号(d) = "123" Then

                                Else
                                    CSV(t).PCS確認 = "NG PCS設定誤り " & 審判員記号リスト(j)
                                End If
                            Next d
                        Else
                            CSV(t).PCS確認 = "NG PCS未設定 " & 審判員記号リスト(j)
                        End If
                    Next j



                    If マスタデータ.E_ヒート表マスタ.FileCheck(区分.区分番号, ラウンド.ラウンド番号) = False Then
                        'ヒート表未作成

                    Else
                        'ヒート表ファイルの読込み
                        マスタデータ.E_ヒート表マスタ.Read(区分.区分番号, ラウンド.ラウンド番号)

                        Dim 背番号リスト(1) As String

                        '赤
                        Dim ヒート番号 As Integer = 1
                        Dim 出場選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(1, ヒート番号, 背番号リスト)

                        CSV(t).赤背番号 = 背番号リスト(1)

                        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ
                        Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, CSV(t).赤背番号)

                        CSV(t).赤選手名 = 選手.リーダー表記名
                        CSV(t).赤所属名 = 選手.カップル所属名
                        CSV(t).赤ヒート番号 = ヒート番号


                        '青
                        ヒート番号 = 2
                        出場選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(1, ヒート番号, 背番号リスト)

                        CSV(t).青背番号 = 背番号リスト(1)

                        選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, CSV(t).青背番号)

                        CSV(t).青選手名 = 選手.リーダー表記名
                        CSV(t).青所属名 = 選手.カップル所属名
                        CSV(t).青ヒート番号 = ヒート番号

                    End If


                End If

            Next t

            'CSVファイルに書き出し

            CSV書き出し(CSV)


        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("一覧CSV作成終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す


    End Sub

    Private Function CSV書き出し(CSV() As 一覧CSV) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = "対戦表" & ".csv"

        Dim filepath As String = マスタデータ.Z_システム設定.Comp_filepath

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)



            'Dim s As Integer
            Dim 登録済みFLAG As Boolean = False


            sw.WriteLine("競技会NO,'" & CSV(0).競技会NO)
            sw.WriteLine("競技会名," & CSV(0).競技会名)
            sw.WriteLine("開催日," & CSV(0).開催日)
            sw.WriteLine("主催団体," & CSV(0).主催団体）
            sw.WriteLine("開催場所," & CSV(0).開催場所)

            sw.WriteLine()

            'ヘッダーを書き出し
            sw.WriteLine("競技NO,競技NO枝番,カテゴリNO,カテゴリ名,区分,区分名,ラウンドNO,ラウンド名,ラウンド名２,ジャッジGP,ジャッジ名, PCS設定確認,採点方式,勝敗ラウンド数,全ラウンド数,赤背番号,赤選手名,赤所属名,赤ヒート番号,青背番号,青選手名,青所属名,青ヒート番号")


            For t = 1 To UBound(CSV)

                If CSV(t) IsNot Nothing Then

                    Dim writeText As String = ""

                    writeText = writeText & "'" & CSV(t).競技NO & ","
                    writeText = writeText & "'" & CSV(t).競技枝番 & ","
                    writeText = writeText & "'" & CSV(t).カテゴリNO & ","
                    writeText = writeText & CSV(t).カテゴリ名 & ","
                    writeText = writeText & "'" & CSV(t).区分NO & ","
                    writeText = writeText & CSV(t).区分名 & ","
                    writeText = writeText & "'" & CSV(t).ラウンドNO & ","
                    writeText = writeText & CSV(t).ラウンド名 & ","
                    writeText = writeText & CSV(t).ラウンド名2 & ","
                    writeText = writeText & "'" & CSV(t).ジャッジGP & ","
                    writeText = writeText & CSV(t).ジャッジ名 & ","
                    writeText = writeText & CSV(t).PCS確認 & ","
                    writeText = writeText & CSV(t).採点方式 & ","
                    writeText = writeText & CSV(t).勝敗ラウンド数 & ","
                    writeText = writeText & CSV(t).全ラウンド数 & ","

                    writeText = writeText & CSV(t).赤背番号 & ","
                    writeText = writeText & CSV(t).赤選手名 & ","
                    writeText = writeText & CSV(t).赤所属名 & ","
                    writeText = writeText & CSV(t).赤ヒート番号 & ","

                    writeText = writeText & CSV(t).青背番号 & ","
                    writeText = writeText & CSV(t).青選手名 & ","
                    writeText = writeText & CSV(t).青所属名 & ","
                    writeText = writeText & CSV(t).青ヒート番号 & ","


                    sw.WriteLine(writeText)

                End If

            Next t


            '閉じる 
            sw.Close()

        Catch ex As Exception
            rc = 1

        End Try


        Return rc

    End Function

    Private Class 一覧CSV

        '0行目だけ使用
        Public 競技会NO As String
        Public 競技会名 As String
        Public 開催日 As String
        Public 主催団体 As String
        Public 開催場所 As String


        '1行目以降使用
        Public 競技NO As String
        Public 競技枝番 As String
        Public カテゴリNO As String
        Public カテゴリ名 As String
        Public 区分NO As String
        Public 区分名 As String
        Public ラウンドNO As String
        Public ラウンド名 As String
        Public ラウンド名2 As String
        Public ジャッジGP As String
        Public ジャッジ名 As String
        Public PCS確認 As String

        Public 採点方式 As String
        Public 勝敗ラウンド数 As Integer
        Public 全ラウンド数 As Integer

        Public 赤背番号 As String
        Public 赤選手名 As String
        Public 赤所属名 As String
        Public 赤ヒート番号 As Integer
        Public 赤得点 As Decimal
        Public 赤WIN As String

        Public 青背番号 As String
        Public 青選手名 As String
        Public 青所属名 As String
        Public 青ヒート番号 As Integer
        Public 青得点 As Decimal
        Public 青WIN As String

        Public 勝to区分 As String
        Public 勝toラウンド As String
        Public 負to区分 As String
        Public 負toラウンド As String

        Public 赤1R点数 As Decimal
        Public 赤2R点数 As Decimal
        Public 赤3R点数 As Decimal
        Public 赤4R点数 As Decimal
        Public 赤5R点数 As Decimal

        Public 青1R点数 As Decimal
        Public 青2R点数 As Decimal
        Public 青3R点数 As Decimal
        Public 青4R点数 As Decimal
        Public 青5R点数 As Decimal

    End Class

End Class