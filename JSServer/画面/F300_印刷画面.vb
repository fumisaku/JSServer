Imports System.Threading

Imports System.IO.Compression   'Nuget　でMailkit をインストール
Imports OpenQA.Selenium
Imports System.Windows.Forms


Public Class F300_印刷画面

    Private 区分番号 As String
    Private 現ラウンド番号 As String
    Private 次ラウンド番号 As String

    Private 現ラウンドヒート割 As String
    Private 次ラウンドヒート割 As String

    Private 現ラウンド採点方式 As String
    Private 次ラウンド採点方式 As String


    Private マスタデータ As マスタデータ


    Private Sub F300_印刷画面_Load(sender As Object, e As EventArgs) Handles MyBase.Load


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


        If マスタデータ.C_ラウンドマスタ.Get_次ラウンドClass(区分番号, 現ラウンド番号) Is Nothing Then

            次ラウンド番号 = "000"
            次ラウンドヒート割 = ""
            次ラウンド採点方式 = ""

        Else

            Dim 次ラウンドCalss As C_ラウンド = マスタデータ.C_ラウンドマスタ.Get_次ラウンドClass(区分番号, 現ラウンド番号)

            次ラウンド番号 = 次ラウンドCalss.ラウンド番号
            次ラウンドヒート割 = 次ラウンドCalss.ヒート割方式
            次ラウンド採点方式 = 次ラウンドCalss.採点方式

            次ラウンドCalss = Nothing

        End If



        Me.LB_区分名.Text = マスタデータ.B_区分マスタ.Get区分表記名(区分番号)

        現ラウンドパネル作成()
        次ラウンドパネル作成()

    End Sub

    Private Sub 現ラウンドパネル作成()

        Me.LB_現ラウンド.Text = マスタデータ.Get_ラウンド名(現ラウンド番号) & " 採点方式【" & 現ラウンド採点方式 & "】"

        If 現ラウンド番号 = "400" Then
            Me.PB_F1.Visible = True
            Me.PB_入賞者.Visible = True

        Else
            Me.PB_F1.Visible = False
            Me.PB_入賞者.Visible = False
        End If


    End Sub

    Private Sub 次ラウンドパネル作成()

        '次ラウンドが無い時は表示しない
        If 次ラウンド番号 = "000" Then
            Me.PN_次.Visible = False

        Else

            Me.LB_次ラウンド.Text = マスタデータ.Get_ラウンド名(次ラウンド番号) & " 採点方式【" & 次ラウンド採点方式 & "】"

            If 次ラウンド番号 = "400" Then

                Me.PB_F1次.Visible = True
            Else
                Me.PB_F1次.Visible = False
            End If

        End If
    End Sub

    'H1 出場者連絡表印刷
    Private Async Sub PB_H1_Click(sender As Object, e As EventArgs) Handles PB_H1.Click

        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            'H1_印刷()
            H1_印刷_単票("単票")

        End Sub
        )
        '上記非同期処理が終わるのを待つ
        Await task
        MsgBox("H1印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す

    End Sub

    Private Sub H1_印刷()

        Dim 配布先リスト(20) As String
        マスタデータ.R_印刷設定マスタ.FileRead()
        マスタデータ.R_印刷設定マスタ.Get_配布先リスト(現ラウンド採点方式, 現ラウンド番号, "H1", 配布先リスト)


        If 現ラウンド番号 = "400" Then

            Dim RPT As RPT_F1_決勝進出者名簿
            RPT = New RPT_F1_決勝進出者名簿

            マスタデータ.R_印刷設定マスタ.Get_配布先リスト(現ラウンド採点方式, 現ラウンド番号, "F1", 配布先リスト)
            RPT.印刷(区分番号, 現ラウンド番号, マスタデータ, 配布先リスト)

            RPT = Nothing

        Else

            Dim RPT As RPT_H1_出場者連絡票
            RPT = New RPT_H1_出場者連絡票

            マスタデータ.R_印刷設定マスタ.Get_配布先リスト(現ラウンド採点方式, 現ラウンド番号, "H1", 配布先リスト)
            RPT.印刷(区分番号, 現ラウンド番号, マスタデータ, 配布先リスト)

            RPT = Nothing

        End If

    End Sub

    Private Sub H1_印刷_単票(配布先 As String)

        Dim 配布先リスト(20) As String


        If 現ラウンド番号 = "400" Then

            Dim RPT As RPT_F1_決勝進出者名簿
            RPT = New RPT_F1_決勝進出者名簿


            配布先リスト(1) = 配布先
            RPT.印刷(区分番号, 現ラウンド番号, マスタデータ, 配布先リスト)

            RPT = Nothing

        Else

            Dim RPT As RPT_H1_出場者連絡票
            RPT = New RPT_H1_出場者連絡票

            配布先リスト(1) = 配布先
            RPT.印刷(区分番号, 現ラウンド番号, マスタデータ, 配布先リスト)

            RPT = Nothing

        End If

    End Sub


    'H2 出場者連絡表印刷
    Private Async Sub PB_H2_Click(sender As Object, e As EventArgs) Handles PB_H2.Click

        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            'H2_印刷()
            H2_印刷_単票("単票")

        End Sub
        )
        '上記非同期処理が終わるのを待つ
        Await task
        MsgBox("H2印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す

    End Sub

    Private Sub H2_印刷()


        Dim RPT As RPT_H2_出場者連絡票
        RPT = New RPT_H2_出場者連絡票

        Dim 配布先リスト(20) As String
        マスタデータ.R_印刷設定マスタ.FileRead()
        マスタデータ.R_印刷設定マスタ.Get_配布先リスト(現ラウンド採点方式, 現ラウンド番号, "H2", 配布先リスト)


        RPT.印刷(区分番号, 現ラウンド番号, マスタデータ, 配布先リスト)

        RPT = Nothing

    End Sub

    Private Sub H2_印刷_単票(配布先 As String)


        Dim RPT As RPT_H2_出場者連絡票
        RPT = New RPT_H2_出場者連絡票

        Dim 配布先リスト(20) As String
        配布先リスト(1) = 配布先
        RPT.印刷(区分番号, 現ラウンド番号, マスタデータ, 配布先リスト)

        RPT = Nothing

    End Sub

    'H3 ヒート表印刷（シャッフル用）
    Private Async Sub PB_H3_Click(sender As Object, e As EventArgs) Handles PB_H3.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            'H3_印刷()
            H3_印刷_単票("単票")

        End Sub
        )
        '上記非同期処理が終わるのを待つ
        Await task
        MsgBox("H3印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す

    End Sub

    Private Sub H3_印刷()

        Dim RPT As RPT_H3_出場者ヒート表
        RPT = New RPT_H3_出場者ヒート表

        Dim 配布先リスト(20) As String
        マスタデータ.R_印刷設定マスタ.FileRead()
        マスタデータ.R_印刷設定マスタ.Get_配布先リスト(現ラウンド採点方式, 現ラウンド番号, "H3", 配布先リスト)

        RPT.印刷(区分番号, 現ラウンド番号, マスタデータ, 配布先リスト)

        RPT = Nothing

    End Sub

    Private Sub H3_印刷_単票(配布先 As String)

        Dim RPT As RPT_H3_出場者ヒート表
        RPT = New RPT_H3_出場者ヒート表

        Dim 配布先リスト(20) As String

        配布先リスト(1) = 配布先
        RPT.印刷(区分番号, 現ラウンド番号, マスタデータ, 配布先リスト)

        RPT = Nothing

    End Sub



    Private Async Sub PB_H3_HTML_Click(sender As Object, e As EventArgs) Handles PB_H3_HTML.Click

        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            H3_HTML作成()

        End Sub
        )
        '上記非同期処理が終わるのを待つ
        Await task
        MsgBox("Heat表 HTML作成終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す



    End Sub

    Private Sub H3_HTML作成()


        Dim H3ヒート As H3ヒート表
        H3ヒート = New H3ヒート表

        H3ヒート.CreateHTML(区分番号, 現ラウンド番号, "LOCAL")
        H3ヒート.CreateHTML(区分番号, 現ラウンド番号, "REMOTE")


        'ラウンド一覧更新
        Dim ラウンドHTML As ラウンド一覧
        ラウンドHTML = New ラウンド一覧
        ラウンドHTML.CreateHTML(区分番号, "LOCAL")
        ラウンドHTML.CreateHTML(区分番号, "REMOTE")

        ラウンドHTML = Nothing


        '区分一覧更新
        Dim 区分一覧HTML As 区分一覧HTML
        区分一覧HTML = New 区分一覧HTML
        区分一覧HTML.CreateHTML("LOCAL")
        区分一覧HTML.CreateHTML("REMOTE")

        区分一覧HTML = Nothing


        'ブラウザーで開く
        Dim filename As String = "Heat_" & 区分番号 & "_" & 現ラウンド番号 & ".html"

        Dim url As String = マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult\" & filename
        System.Diagnostics.Process.Start(url)


    End Sub


    'PT ポイント表印刷
    Private Async Sub PB_PT_Click(sender As Object, e As EventArgs) Handles PB_PT.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            'PT印刷()
            PT印刷_単票("単票")

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("PT印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す

    End Sub

    Private Sub PT印刷()

        If Strings.Left(現ラウンド採点方式, 3) = "VAL" Then
            'バルカーの時はCSVを作成するだけ

            Dim 採点結果_V2 As 採点結果_V2 = New 採点結果_V2(区分番号, 現ラウンド番号)

            Dim VAL_1_素点 As VAL_1_素点 = New VAL_1_素点
            VAL_1_素点.CreateCSV(採点結果_V2)


            Dim VAL_2_平均点 As VAL_2_平均点 = New VAL_2_平均点
            VAL_2_平均点.CreateCSV(採点結果_V2)

            採点結果_V2 = Nothing
            VAL_1_素点 = Nothing
            VAL_2_平均点 = Nothing

        Else

            Dim RPT As RPT_PT_点数表
            RPT = New RPT_PT_点数表

            Dim 配布先リスト(20) As String
            マスタデータ.R_印刷設定マスタ.FileRead()
            マスタデータ.R_印刷設定マスタ.Get_配布先リスト(現ラウンド採点方式, 現ラウンド番号, "PT", 配布先リスト)


            Dim 採点結果 As 採点結果_C
            採点結果 = New 採点結果_C(区分番号, 現ラウンド番号)

            RPT.印刷(採点結果, 配布先リスト)

            採点結果 = Nothing
            RPT = Nothing

        End If


    End Sub

    Private Sub PT印刷_単票(配布先 As String)


        If Strings.Left(現ラウンド採点方式, 3) = "VAL" Then
            'バルカーの時はCSVを作成するだけ

            Dim 採点結果_V2 As 採点結果_V2 = New 採点結果_V2(区分番号, 現ラウンド番号)

            Dim VAL_1_素点 As VAL_1_素点 = New VAL_1_素点
            VAL_1_素点.CreateCSV(採点結果_V2)


            Dim VAL_2_平均点 As VAL_2_平均点 = New VAL_2_平均点
            VAL_2_平均点.CreateCSV(採点結果_V2)

            採点結果_V2 = Nothing
            VAL_1_素点 = Nothing
            VAL_2_平均点 = Nothing

        Else


            Dim RPT As RPT_PT_点数表
            RPT = New RPT_PT_点数表

            Dim 配布先リスト(20) As String

            Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, 現ラウンド番号)
            配布先リスト(1) = 配布先


            If Strings.Left(採点方式, 3) = "PDJ" Or Strings.Left(採点方式, 3) = "VAL" Then

                Dim 採点結果_V2 As 採点結果_V2
                採点結果_V2 = New 採点結果_V2(区分番号, 現ラウンド番号)

                RPT.印刷_V2(採点結果_V2, 配布先リスト)

                採点結果_V2 = Nothing


            Else
                Dim 採点結果 As 採点結果_C
                採点結果 = New 採点結果_C(区分番号, 現ラウンド番号)

                RPT.印刷(採点結果, 配布先リスト)

                採点結果 = Nothing
            End If



            RPT = Nothing

        End If
    End Sub

    'Detatil 詳細ポイント表HTML 印刷
    Private Async Sub PB_詳細_Click(sender As Object, e As EventArgs) Handles PB_詳細.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            詳細_印刷()

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("Detail作成終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す


    End Sub

    Private Sub 詳細_印刷()


        Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, 現ラウンド番号)


        Dim 採点結果 As 採点結果_C
        '採点結果 = New 採点結果_C(区分番号, 現ラウンド番号)

        If Strings.Left(採点方式, 3) = "AJS" Then
            'If Strings.Left(採点結果.採点方式, 3) = "AJS" Then

            採点結果 = New 採点結果_C(区分番号, 現ラウンド番号)

            Dim AJS得点詳細 As AJS得点詳細
            AJS得点詳細 = New AJS得点詳細
            AJS得点詳細.CreateHTML(採点結果, "LOCAL")
            AJS得点詳細.CreateHTML(採点結果, "REMOTE")

            採点結果 = Nothing
            AJS得点詳細 = Nothing

        ElseIf Strings.Left(採点方式, 3) = "BJS" Then

            採点結果 = New 採点結果_C(区分番号, 現ラウンド番号)

            Dim BJS得点詳細 As BJS得点詳細
            BJS得点詳細 = New BJS得点詳細
            BJS得点詳細.CreateHTML(採点結果, "LOCAL")
            BJS得点詳細.CreateHTML(採点結果, "REMOTE")

            採点結果 = Nothing
            BJS得点詳細 = Nothing


        ElseIf 採点方式 = "チェック法" Then

            採点結果 = New 採点結果_C(区分番号, 現ラウンド番号)

            Dim チェック法得点詳細 As チェック法得点詳細
            チェック法得点詳細 = New チェック法得点詳細
            チェック法得点詳細.CreateHTML(採点結果, "LOCAL")
            チェック法得点詳細.CreateHTML(採点結果, "REMOTE")

        ElseIf Strings.Left(採点方式, 3) = "PDJ" Then

            Dim 採点結果_V2 = New 採点結果_V2(区分番号, 現ラウンド番号)

            Dim PDJ得点詳細 As PDJ得点詳細
            PDJ得点詳細 = New PDJ得点詳細
            PDJ得点詳細.CreateHTML(採点結果_V2, "LOCAL")
            PDJ得点詳細.CreateHTML(採点結果_V2, "REMOTE")

            採点結果_V2 = Nothing
            PDJ得点詳細 = Nothing


        ElseIf Strings.Left(採点方式, 3) = "VAL" Then

            Dim 採点結果_V2 = New 採点結果_V2(区分番号, 現ラウンド番号)

            Dim VAL得点詳細 As VAL得点詳細
            VAL得点詳細 = New VAL得点詳細
            VAL得点詳細.CreateHTML(採点結果_V2, "LOCAL")
            VAL得点詳細.CreateHTML(採点結果_V2, "REMOTE")

            採点結果_V2 = Nothing
            VAL得点詳細 = Nothing

        End If



        'ラウンド一覧更新
        Dim ラウンドHTML As ラウンド一覧
        ラウンドHTML = New ラウンド一覧
        ラウンドHTML.CreateHTML(区分番号, "LOCAL")
        ラウンドHTML.CreateHTML(区分番号, "REMOTE")

        ラウンドHTML = Nothing


        '区分一覧更新
        Dim 区分一覧HTML As 区分一覧HTML
        区分一覧HTML = New 区分一覧HTML
        区分一覧HTML.CreateHTML("LOCAL")
        区分一覧HTML.CreateHTML("REMOTE")

        区分一覧HTML = Nothing


        'ブラウザーで開く
        'Dim url As String = マスタデータ.Z_システム設定.HTML_filepath & "\" & "index.html"
        Dim url As String = マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult\Result_" & 区分番号 & "_" & 現ラウンド番号 & ".html"
        System.Diagnostics.Process.Start(url)

        Try
            System.Threading.Thread.Sleep(3000)
            SendKeys.Send("^p")    'Ctrl + P

            System.Threading.Thread.Sleep(2000)
            SendKeys.Send("{ENTER}")   '印刷実行

            System.Threading.Thread.Sleep(5000)
            SendKeys.Send("% ")      'Alt + blank
            SendKeys.Send("N")

        Catch ex As Exception

        End Try


    End Sub


    Public Sub HTML印刷()

        Dim url As String = マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult\Result_" & 区分番号 & "_" & 現ラウンド番号 & ".html"
        System.Diagnostics.Process.Start(url)

        'Process.Start("chrome.exe", url)

        System.Threading.Thread.Sleep(3000)
        'SendKeys.Send("{END}")

        'System.Threading.Thread.Sleep(2000)
        SendKeys.Send("^p")    'Ctrl + P

        System.Threading.Thread.Sleep(2000)
        SendKeys.Send("{ENTER}")

        System.Threading.Thread.Sleep(5000)
        SendKeys.Send("% ")      'Alt + blank
        SendKeys.Send("N")


        Exit Sub


    End Sub




    'H1 出場者連絡表印刷 次ラウンド
    Private Async Sub PB_H1次_Click(sender As Object, e As EventArgs) Handles PB_H1次.Click

        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            'H1次_印刷()
            H1次_印刷_単票("単票")

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("H1印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す


    End Sub

    Private Sub H1次_印刷()

        Dim 配布先リスト(20) As String
        マスタデータ.R_印刷設定マスタ.FileRead()



        If 次ラウンド番号 = "400" Then

            マスタデータ.R_印刷設定マスタ.Get_配布先リスト(次ラウンド採点方式, 次ラウンド番号, "F1", 配布先リスト)


            Dim RPT As RPT_F1_決勝進出者名簿
            RPT = New RPT_F1_決勝進出者名簿

            RPT.印刷(区分番号, 次ラウンド番号, マスタデータ, 配布先リスト)

            RPT = Nothing

        Else
            マスタデータ.R_印刷設定マスタ.Get_配布先リスト(次ラウンド採点方式, 次ラウンド番号, "H1", 配布先リスト)


            Dim RPT As RPT_H1_出場者連絡票
            RPT = New RPT_H1_出場者連絡票

            RPT.印刷(区分番号, 次ラウンド番号, マスタデータ, 配布先リスト)

            RPT = Nothing

        End If


    End Sub

    Private Sub H1次_印刷_単票(配布先 As String)

        Dim 配布先リスト(20) As String




        If 次ラウンド番号 = "400" Then

            配布先リスト(1) = 配布先

            Dim RPT As RPT_F1_決勝進出者名簿
            RPT = New RPT_F1_決勝進出者名簿

            RPT.印刷(区分番号, 次ラウンド番号, マスタデータ, 配布先リスト)

            RPT = Nothing

        Else
            配布先リスト(1) = 配布先

            Dim RPT As RPT_H1_出場者連絡票
            RPT = New RPT_H1_出場者連絡票

            RPT.印刷(区分番号, 次ラウンド番号, マスタデータ, 配布先リスト)

            RPT = Nothing

        End If


    End Sub


    'H2 出場者連絡表印刷　次ラウンド
    Private Async Sub PB_H2次_Click(sender As Object, e As EventArgs) Handles PB_H2次.Click

        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            'H2次_印刷()
            H2次_印刷_単票("単票")

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("H2印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す


    End Sub

    Private Sub H2次_印刷()

        Dim RPT As RPT_H2_出場者連絡票
        RPT = New RPT_H2_出場者連絡票

        Dim 配布先リスト(20) As String
        マスタデータ.R_印刷設定マスタ.FileRead()
        マスタデータ.R_印刷設定マスタ.Get_配布先リスト(次ラウンド採点方式, 次ラウンド番号, "H2", 配布先リスト)


        RPT.印刷(区分番号, 次ラウンド番号, マスタデータ, 配布先リスト)

        RPT = Nothing

    End Sub


    Private Sub H2次_印刷_単票(配布先 As String)

        Dim RPT As RPT_H2_出場者連絡票
        RPT = New RPT_H2_出場者連絡票

        Dim 配布先リスト(20) As String

        配布先リスト(1) = 配布先

        RPT.印刷(区分番号, 次ラウンド番号, マスタデータ, 配布先リスト)

        RPT = Nothing

    End Sub

    'H3 ヒート表印刷（シャッフル用）次ラウンド
    Private Async Sub PB_H3次_Click(sender As Object, e As EventArgs) Handles PB_H3次.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            'H3次_印刷()
            H3次_印刷_単票("単票")

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("H3印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す


    End Sub

    Private Sub H3次_印刷（）


        Dim RPT As RPT_H3_出場者ヒート表
        RPT = New RPT_H3_出場者ヒート表

        Dim 配布先リスト(20) As String
        マスタデータ.R_印刷設定マスタ.FileRead()
        マスタデータ.R_印刷設定マスタ.Get_配布先リスト(次ラウンド採点方式, 次ラウンド番号, "H3", 配布先リスト)


        RPT.印刷(区分番号, 次ラウンド番号, マスタデータ, 配布先リスト)

        RPT = Nothing

    End Sub

    Private Sub H3次_印刷_単票（配布先 As String）


        Dim RPT As RPT_H3_出場者ヒート表
        RPT = New RPT_H3_出場者ヒート表

        Dim 配布先リスト(20) As String

        配布先リスト(1) = 配布先

        RPT.印刷(区分番号, 次ラウンド番号, マスタデータ, 配布先リスト)

        RPT = Nothing

    End Sub


    'H3 ヒート表印刷（シャッフル用）次ラウンド
    Private Async Sub PB_H3次_HTML_Click(sender As Object, e As EventArgs) Handles PB_H3次_HTML.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            H3次_HTML作成()

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("Heat表 HTML作成終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す


    End Sub



    Private Sub H3次_HTML作成()


        Dim H3ヒート As H3ヒート表
        H3ヒート = New H3ヒート表

        H3ヒート.CreateHTML(区分番号, 次ラウンド番号, "LOCAL")
        H3ヒート.CreateHTML(区分番号, 次ラウンド番号, "REMOTE")


        'ラウンド一覧更新
        Dim ラウンドHTML As ラウンド一覧
        ラウンドHTML = New ラウンド一覧
        ラウンドHTML.CreateHTML(区分番号, "LOCAL")
        ラウンドHTML.CreateHTML(区分番号, "REMOTE")

        ラウンドHTML = Nothing


        '区分一覧更新
        Dim 区分一覧HTML As 区分一覧HTML
        区分一覧HTML = New 区分一覧HTML
        区分一覧HTML.CreateHTML("LOCAL")
        区分一覧HTML.CreateHTML("REMOTE")

        区分一覧HTML = Nothing


        'ブラウザーで開く
        Dim filename As String = "Heat_" & 区分番号 & "_" & 次ラウンド番号 & ".html"

        'Dim url As String = マスタデータ.Z_システム設定.HTML_filepath & "\" & filename
        Dim url As String = マスタデータ.Z_システム設定.Comp_filepath & "\LocalResult\" & filename
        System.Diagnostics.Process.Start(url)


    End Sub


    Private Async Sub PB_入賞者_Click(sender As Object, e As EventArgs) Handles PB_入賞者.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            '入賞者_印刷()
            入賞者_印刷_単票("単票")

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("入賞者印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す


    End Sub

    Private Sub 入賞者_印刷()

        Dim RPT As RPT_FL_入賞者名簿
        RPT = New RPT_FL_入賞者名簿

        Dim 配布先リスト(20) As String
        マスタデータ.R_印刷設定マスタ.FileRead()
        マスタデータ.R_印刷設定マスタ.Get_配布先リスト(現ラウンド採点方式, 現ラウンド番号, "FL", 配布先リスト)

        Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, 現ラウンド番号)


        If Strings.Left(採点方式, 3) = "PDJ" Or Strings.Left(採点方式, 3) = "VAL" Then
            Dim 採点結果_V2 As 採点結果_V2
            採点結果_V2 = New 採点結果_V2(区分番号, 現ラウンド番号)

            RPT.印刷_V2(採点結果_V2, 配布先リスト)
            採点結果_V2 = Nothing

        Else
            Dim 採点結果 As 採点結果_C
            採点結果 = New 採点結果_C(区分番号, 現ラウンド番号)

            RPT.印刷(採点結果, 配布先リスト)
            採点結果 = Nothing

        End If


        RPT = Nothing


    End Sub

    Public Sub 入賞者_印刷_単票(配布先 As String)

        Dim RPT As RPT_FL_入賞者名簿
        RPT = New RPT_FL_入賞者名簿

        Dim 配布先リスト(20) As String

        配布先リスト(1) = 配布先

        Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, 現ラウンド番号)

        If Strings.Left(採点方式, 3) = "PDJ" Or Strings.Left(採点方式, 3) = "VAL" Then
            Dim 採点結果_V2 As 採点結果_V2
            採点結果_V2 = New 採点結果_V2(区分番号, 現ラウンド番号)

            RPT.印刷_V2(採点結果_V2, 配布先リスト)
            採点結果_V2 = Nothing



        Else
            Dim 採点結果 As 採点結果_C
            採点結果 = New 採点結果_C(区分番号, 現ラウンド番号)

            RPT.印刷(採点結果, 配布先リスト)
            採点結果 = Nothing

        End If


        RPT = Nothing


    End Sub

    '分析EXCEL作成
    Private Async Sub PB_分析_Click(sender As Object, e As EventArgs) Handles PB_分析.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            分析シート作成()

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("分析シート作成終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す



    End Sub

    Private Sub 分析シート作成()

        Dim JA_分析 As JA分析_C
        JA_分析 = New JA分析_C


        Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, 現ラウンド番号)

        If Strings.Left(採点方式, 3) = "PDJ" Or Strings.Left(採点方式, 3) = "VAL" Then
            Dim 採点結果_V2 As 採点結果_V2
            採点結果_V2 = New 採点結果_V2(区分番号, 現ラウンド番号)

            JA_分析.JA_CSVファイル作成_V2(採点結果_V2)
            採点結果_V2 = Nothing
        Else
            Dim 採点結果 As 採点結果_C
            採点結果 = New 採点結果_C(区分番号, 現ラウンド番号)

            JA_分析.JA_CSVファイル作成(採点結果)
            採点結果 = Nothing

        End If


        JA_分析.分析EXCELの作成(区分番号, 現ラウンド番号)

        JA_分析 = Nothing


    End Sub


    Private Async Sub PB_分析全体_Click(sender As Object, e As EventArgs) Handles PB_分析全体.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            分析シートTOTAL作成()

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("分析シート全体作成終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す

    End Sub

    Private Sub 分析シートTOTAL作成()


        Dim JA_分析 As JA分析_C
        JA_分析 = New JA分析_C

        JA_分析.分析EXCELの作成(区分番号, "ALL")

    End Sub


    'まとめ印刷
    Private Async Sub PB_印刷_Click(sender As Object, e As EventArgs) Handles PB_印刷.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()


            'ここに印刷するものを書く
            'PT印刷()

            まとめ印刷_Ver2()

            詳細_印刷()

            分析シート作成()

            If 次ラウンド番号 <> "000" Then

                'H1次_印刷()
                'H2次_印刷()
                'H3次_印刷()
                H3次_HTML作成()

            End If
        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す

    End Sub

    Public Async Sub まとめ印刷(ネットアップFLAG As Boolean)    '外部からの呼び出し用


        Dim task As Task = Task.Run(
        Sub()
            'ここに印刷するものを書く
            'PT印刷()

            まとめ印刷_Ver2()

            詳細_印刷()

            分析シート作成()

            If 現ラウンド番号 = "400" Then
                分析シートTOTAL作成()
            End If

            If 次ラウンド番号 <> "000" Then

                'H1次_印刷()
                'H2次_印刷()
                'H3次_印刷()
                H3次_HTML作成()


            End If

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task

        If ネットアップFLAG = True Then
            SFTP同期()
            メールで報告()
        End If

        MsgBox("処理完了しました。")

    End Sub


    Private Sub まとめ印刷_Ver2()

        マスタデータ.R_印刷設定マスタ.FileRead()

        マスタデータ.R_印刷設定マスタ.FileRead()

        Dim 配布先名_PT As String = ""
        Dim 印刷枚数_PT As Integer = 0

        Dim 配布先名_H1 As String = ""
        Dim 配布先名_H2 As String = ""
        Dim 配布先名_H3 As String = ""
        Dim 印刷枚数_H1 As Integer = 0
        Dim 印刷枚数_H2 As Integer = 0
        Dim 印刷枚数_H3 As Integer = 0

        For h = 1 To 20

            '=============現ラウンド結果分

            配布先名_PT = ""
            印刷枚数_PT = 0


            マスタデータ.R_印刷設定マスタ.Get_配布先名(現ラウンド採点方式, 現ラウンド番号, "PT", h, 配布先名_PT, 印刷枚数_PT)

            If 印刷枚数_PT > 0 Then
                For i = 1 To 印刷枚数_PT
                    PT印刷_単票(配布先名_PT)
                Next i
            End If

            '==========================ここから次ラウンド分

            If 次ラウンド番号 <> "000" Then

                印刷枚数_H1 = 0
                印刷枚数_H2 = 0
                印刷枚数_H3 = 0

                If 次ラウンド番号 = "400" Then
                    マスタデータ.R_印刷設定マスタ.Get_配布先名(次ラウンド採点方式, 次ラウンド番号, "F1", h, 配布先名_H1, 印刷枚数_H1)
                Else
                    マスタデータ.R_印刷設定マスタ.Get_配布先名(次ラウンド採点方式, 次ラウンド番号, "H1", h, 配布先名_H1, 印刷枚数_H1)
                End If

                マスタデータ.R_印刷設定マスタ.Get_配布先名(次ラウンド採点方式, 次ラウンド番号, "H2", h, 配布先名_H2, 印刷枚数_H2)
                マスタデータ.R_印刷設定マスタ.Get_配布先名(次ラウンド採点方式, 次ラウンド番号, "H3", h, 配布先名_H3, 印刷枚数_H3)

                If 印刷枚数_H1 > 0 Then
                    For i = 1 To 印刷枚数_H1
                        H1次_印刷_単票(配布先名_H1)
                    Next i
                End If

                If 印刷枚数_H2 > 0 Then
                    For i = 1 To 印刷枚数_H2
                        H2次_印刷_単票(配布先名_H2)
                    Next i
                End If

                If 印刷枚数_H3 > 0 Then
                    For i = 1 To 印刷枚数_H3
                        H3次_印刷_単票(配布先名_H3)
                    Next i
                End If

            End If


        Next h



    End Sub



    Private Sub PB_印刷設定_Click(sender As Object, e As EventArgs) Handles PB_印刷設定.Click

        Dim F301 As F301_印刷設定
        F301 = New F301_印刷設定

        F301.ShowDialog()

        F301 = Nothing

    End Sub

    Private Async Sub PB_SFTP_Click(sender As Object, e As EventArgs) Handles PB_SFTP.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()

            SFTP同期()


            メールで報告()     '佐倉宛にZIPファイルを送信

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("サーバUP終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す


    End Sub

    Public Sub SFTP同期()     'SFTPサーバーと同期

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

    Private Sub メールで報告()

        'ZIPファイル作成

        '全部「採点済み」になっているか？
        Dim ALL採点済FLAG As Boolean = True
        For t = 1 To UBound(マスタデータ.T_採点進行管理.リスト)
            If マスタデータ.T_採点進行管理.リスト(t) IsNot Nothing Then

                If マスタデータ.T_採点進行管理.リスト(t).ステータス <> "採点済み" Then
                    ALL採点済FLAG = False
                    t = UBound(マスタデータ.T_採点進行管理.リスト)
                End If
            End If
        Next t


        If ALL採点済FLAG = False Then
            Exit Sub
        Else
            'ZIPファイルの作成

            Dim 元PATH As String = マスタデータ.Z_システム設定.Comp_filepath
            Dim 宛先PATH As String = System.IO.Directory.GetParent(元PATH).ToString

            Dim 競技会NO As String = マスタデータ.A_競技会マスタ.公認競技会NO


            'ファイル削除  (存在しなくてもエラーにならない）
            Dim finfo As New System.IO.FileInfo(宛先PATH & "\" & 競技会NO & ".zip")
            finfo.Delete()

            'ZIPファイルの作成
            ZipFile.CreateFromDirectory(元PATH,
                                    宛先PATH & "\" & 競技会NO & ".zip",
                                    CompressionLevel.Optimal,
                                    True,
                                    System.Text.Encoding.GetEncoding("Shift_JIS"))



            'メール送信
            メール送信(マスタデータ.A_競技会マスタ.公認競技会NO,
                       マスタデータ.A_競技会マスタ.競技会名,
                       宛先PATH & "\" & 競技会NO & ".zip")


        End If






    End Sub

    Private Sub メール送信(競技会NO As String, 競技会名 As String, 添付ファイルPATH As String)


        Try

            Dim MailHost = "smtp.gmail.com"
            Dim MailPort = 587
            Dim UserName = "fumisaku@gmail.com"
            'Dim PassWord = "GOGO39ra"
            Dim PassWord = "joqcygofxdlbsohv"

            Dim msg = New MimeKit.MimeMessage()
            msg.From.Add(New MimeKit.MailboxAddress("JDSF_AJS", "fumisaku@gmail.com")) '送信元メールアドレス
            'msg.To.Add(New MimeKit.MailboxAddress("test2", "fumihiko.sakura@jdsf.or.jp")) '送信先メールアドレス
            msg.To.Add(New MimeKit.MailboxAddress("佐倉 文彦", "fumisaku@gmail.com")) '送信先メールアドレス
            ' msg.Cc.Add() 'Cc用
            ' msg.Bcc.Add() 'Bcc用
            msg.Subject = "競技会結果報告 " & 競技会NO & " " & 競技会名 'タイトル
            Dim text = New MimeKit.TextPart(MimeKit.Text.TextFormat.Plain)
            text.Text = "競技会が終了しました" + vbCrLf + " " ' 本文
            'msg.Body = text

            '添付ファイル
            Dim Filepath = 添付ファイルPATH
            Dim attachment = New MimeKit.MimePart

            attachment.Content = New MimeKit.MimeContent(IO.File.OpenRead(Filepath))
            attachment.ContentDisposition = text.ContentDisposition
            attachment.ContentTransferEncoding = MimeKit.ContentEncoding.Base64
            attachment.FileName = IO.Path.GetFileName(Filepath)

            Dim multipart = New MimeKit.Multipart

            multipart.Add(text)
            multipart.Add(attachment)

            msg.Body = multipart




            Using client = New MailKit.Net.Smtp.SmtpClient()
                Try
                    Console.WriteLine("メール送信 start")
                    client.Connect(MailHost, MailPort,
                    MailKit.Security.SecureSocketOptions.Auto) '接続
                    client.Authenticate(UserName, PassWord) '認証
                    client.Send(msg) '送信
                    client.Disconnect(True) '切断
                    Console.WriteLine("メール送信 end")
                Catch ex As Exception
                    MsgBox(ex.ToString())
                End Try
            End Using


        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try


    End Sub


    Private Async Sub PB_現Rヒート表印刷_Click(sender As Object, e As EventArgs) Handles PB_現Rヒート表印刷.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()


            'ここに印刷するものを書く

            'H1_印刷()
            'H2_印刷()
            'H3_印刷()

            現Rヒート表印刷_Ver2()

            H3_HTML作成()

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す

    End Sub


    Public Async Sub 現ヒート表印刷(ネットアップFLAG As Boolean)    '外部からの呼び出し用


        '非同期処理開始
        Dim task As Task = Task.Run(
        Sub()


            'ここに印刷するものを書く

            'H1_印刷()
            'H2_印刷()
            'H3_印刷()

            現Rヒート表印刷_Ver2()

            H3_HTML作成()

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task


        If ネットアップFLAG = True Then
            SFTP同期()
        End If


    End Sub


    Public Async Sub 現ヒート表印刷_OLD()    '外部からの呼び出し用


        '非同期処理開始
        Dim task As Task = Task.Run(
        Sub()


            'ここに印刷するものを書く

            'H1_印刷()
            'H2_印刷()
            'H3_印刷()

            現Rヒート表印刷_Ver2()

            H3_HTML作成()

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task


    End Sub

    Private Sub 現Rヒート表印刷_Ver2()



        マスタデータ.R_印刷設定マスタ.FileRead()

        Dim 配布先名_H1 As String = ""
        Dim 配布先名_H2 As String = ""
        Dim 配布先名_H3 As String = ""
        Dim 印刷枚数_H1 As Integer = 0
        Dim 印刷枚数_H2 As Integer = 0
        Dim 印刷枚数_H3 As Integer = 0

        For h = 1 To 20

            印刷枚数_H1 = 0
            印刷枚数_H2 = 0
            印刷枚数_H3 = 0

            If 現ラウンド番号 = "400" Then
                マスタデータ.R_印刷設定マスタ.Get_配布先名(現ラウンド採点方式, 現ラウンド番号, "F1", h, 配布先名_H1, 印刷枚数_H1)
            Else
                マスタデータ.R_印刷設定マスタ.Get_配布先名(現ラウンド採点方式, 現ラウンド番号, "H1", h, 配布先名_H1, 印刷枚数_H1)
            End If


            マスタデータ.R_印刷設定マスタ.Get_配布先名(現ラウンド採点方式, 現ラウンド番号, "H2", h, 配布先名_H2, 印刷枚数_H2)
            マスタデータ.R_印刷設定マスタ.Get_配布先名(現ラウンド採点方式, 現ラウンド番号, "H3", h, 配布先名_H3, 印刷枚数_H3)

            If 印刷枚数_H1 > 0 Then
                For i = 1 To 印刷枚数_H1
                    H1_印刷_単票(配布先名_H1)
                Next i
            End If

            If 印刷枚数_H2 > 0 Then
                For i = 1 To 印刷枚数_H2
                    H2_印刷_単票(配布先名_H2)
                Next i
            End If

            If 印刷枚数_H3 > 0 Then
                For i = 1 To 印刷枚数_H3
                    H3_印刷_単票(配布先名_H3)
                Next i
            End If


        Next h


    End Sub


    Private Async Sub PB_現R結果印刷_Click(sender As Object, e As EventArgs) Handles PB_現R結果印刷.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()


            'ここに印刷するものを書く
            'PT印刷()

            現R結果印刷()

            詳細_印刷()

        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す

    End Sub

    Private Sub 現R結果印刷()


        マスタデータ.R_印刷設定マスタ.FileRead()

        Dim 配布先名 As String = ""
        Dim 印刷枚数_PT As Integer = 0

        For h = 1 To 20

            配布先名 = ""
            印刷枚数_PT = 0

            マスタデータ.R_印刷設定マスタ.Get_配布先名(現ラウンド採点方式, 現ラウンド番号, "PT", h, 配布先名, 印刷枚数_PT)

            If 印刷枚数_PT > 0 Then
                For i = 1 To 印刷枚数_PT
                    PT印刷_単票(配布先名)
                Next i
            End If


        Next h


    End Sub



    Private Async Sub PB_次Rヒート表印刷_Click(sender As Object, e As EventArgs) Handles PB_次Rヒート表印刷.Click


        '非同期処理開始
        DirectCast(sender, Button).Enabled = False        'ボタンを選択できないようにする
        Dim task As Task = Task.Run(
        Sub()


            'ここに印刷するものを書く

            If 次ラウンド番号 <> "000" Then

                'H1次_印刷()
                'H2次_印刷()
                'H3次_印刷()

                次Rヒート表印刷_Ver2()

                H3次_HTML作成()

            End If
        End Sub
        ）
        '上記非同期処理が終わるのを待つ

        Await task
        MsgBox("印刷終了")
        DirectCast(sender, Button).Enabled = True 'ボタンを選択できるように戻す


    End Sub

    Private Sub 次Rヒート表印刷_Ver2()



        マスタデータ.R_印刷設定マスタ.FileRead()

        Dim 配布先名_H1 As String = ""
        Dim 配布先名_H2 As String = ""
        Dim 配布先名_H3 As String = ""
        Dim 印刷枚数_H1 As Integer = 0
        Dim 印刷枚数_H2 As Integer = 0
        Dim 印刷枚数_H3 As Integer = 0

        For h = 1 To 20


            印刷枚数_H1 = 0
            印刷枚数_H2 = 0
            印刷枚数_H3 = 0

            If 次ラウンド番号 = "400" Then
                マスタデータ.R_印刷設定マスタ.Get_配布先名(次ラウンド採点方式, 次ラウンド番号, "F1", h, 配布先名_H1, 印刷枚数_H1)
            Else
                マスタデータ.R_印刷設定マスタ.Get_配布先名(次ラウンド採点方式, 次ラウンド番号, "H1", h, 配布先名_H1, 印刷枚数_H1)
            End If

            マスタデータ.R_印刷設定マスタ.Get_配布先名(次ラウンド採点方式, 次ラウンド番号, "H2", h, 配布先名_H2, 印刷枚数_H2)
            マスタデータ.R_印刷設定マスタ.Get_配布先名(次ラウンド採点方式, 次ラウンド番号, "H3", h, 配布先名_H3, 印刷枚数_H3)

            If 印刷枚数_H1 > 0 Then
                For i = 1 To 印刷枚数_H1
                    H1次_印刷_単票(配布先名_H1)
                Next i
            End If

            If 印刷枚数_H2 > 0 Then
                For i = 1 To 印刷枚数_H2
                    H2次_印刷_単票(配布先名_H2)
                Next i
            End If

            If 印刷枚数_H3 > 0 Then
                For i = 1 To 印刷枚数_H3
                    H3次_印刷_単票(配布先名_H3)
                Next i
            End If


        Next h


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        HTML印刷()
    End Sub

End Class