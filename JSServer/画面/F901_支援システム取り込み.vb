Imports System.ComponentModel
Imports System.Windows.Forms


Public Class F901_支援システム取り込み

    Private Sub F901_支援システム取り込み_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub setパス名(競技会パス名 As String)

        Me.TB_競技会パス.Text = 競技会パス名

    End Sub


    Private Sub PB_JSパス_Click(sender As Object, e As EventArgs) Handles PB_JSパス.Click

        'FolderBrowserDialogクラスのインスタンスを作成
        Dim fbd As New FolderBrowserDialog

        '上部に表示する説明テキストを指定する
        fbd.Description = "フォルダを指定してください。"
        'ルートフォルダを指定する
        'デフォルトでDesktop
        fbd.RootFolder = Environment.SpecialFolder.Desktop
        '最初に選択するフォルダを指定する
        'RootFolder以下にあるフォルダである必要がある


        If Me.TB_支援システムパス.Text = "" Then
            fbd.SelectedPath = "C:\Windows"

        Else
            fbd.SelectedPath = Me.TB_支援システムパス.Text
        End If



        'ユーザーが新しいフォルダを作成できるようにする
        'デフォルトでTrue
        fbd.ShowNewFolderButton = False

        'ダイアログを表示する
        If fbd.ShowDialog(Me) = DialogResult.OK Then
            '選択されたフォルダを表示する

            Me.TB_支援システムパス.Text = fbd.SelectedPath

        End If

    End Sub

    Private Sub PB_競技会パス_Click(sender As Object, e As EventArgs) Handles PB_競技会パス.Click

        'FolderBrowserDialogクラスのインスタンスを作成
        Dim fbd As New FolderBrowserDialog

        '上部に表示する説明テキストを指定する
        fbd.Description = "フォルダを指定してください。"
        'ルートフォルダを指定する
        'デフォルトでDesktop
        fbd.RootFolder = Environment.SpecialFolder.Desktop
        '最初に選択するフォルダを指定する
        'RootFolder以下にあるフォルダである必要がある


        If Me.TB_競技会パス.Text = "" Then
            fbd.SelectedPath = "C:\Windows"

        Else
            fbd.SelectedPath = Me.TB_競技会パス.Text
        End If



        'ユーザーが新しいフォルダを作成できるようにする
        'デフォルトでTrue
        fbd.ShowNewFolderButton = True

        'ダイアログを表示する
        If fbd.ShowDialog(Me) = DialogResult.OK Then
            '選択されたフォルダを表示する

            Me.TB_競技会パス.Text = fbd.SelectedPath

        End If

    End Sub

    Private Sub PB_競技会データ_Click(sender As Object, e As EventArgs) Handles PB_競技会データ.Click



        'ProgressDialogオブジェクトを作成する
        Dim pd As New FP01_進捗状況("競技会データ取り込み実施中", New DoWorkEventHandler(AddressOf 競技会データ取り込み), 100)

        '進行状況ダイアログを表示する
        Dim result As DialogResult = pd.ShowDialog(Me)
        '結果を取得する
        If result = DialogResult.Cancel Then
            MessageBox.Show("キャンセルされました")
        ElseIf result = DialogResult.Abort Then
            'エラー情報を取得する
            Dim ex As Exception = pd.Error
            MessageBox.Show("エラー: " + ex.Message)
        ElseIf result = DialogResult.OK Then
            '結果を取得する
            Dim stopTime As Integer = CInt(pd.Result)
            MessageBox.Show("成功しました: " & stopTime.ToString())
        End If

        '後始末
        pd.Dispose()


        MsgBox("終了")

    End Sub

    Private Sub 競技会データ取り込み(ByVal sender As Object, ByVal e As DoWorkEventArgs)


        '===進捗ダイアログ初期処理、====
        Dim bw As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        'パラメータを取得する
        Dim stopTime As Integer = CInt(e.Argument)
        '===進捗ダイアログ初期処理、ここまで====


        Dim JS_変換 As JS_変換_C
        JS_変換 = New JS_変換_C(Me.TB_支援システムパス.Text, Me.TB_競技会パス.Text)

        JS_変換.変換(bw, e)

        JS_変換 = Nothing



        '結果を設定する　進捗ダイアログ
        e.Result = stopTime * 100

    End Sub


    Private Sub PB_Member_Click(sender As Object, e As EventArgs) Handles PB_Member.Click




        'ProgressDialogオブジェクトを作成する
        Dim pd As New FP01_進捗状況("出場者取り込み実施中", New DoWorkEventHandler(AddressOf 出場者取り込み), 100)

        '進行状況ダイアログを表示する
        Dim result As DialogResult = pd.ShowDialog(Me)
        '結果を取得する
        If result = DialogResult.Cancel Then
            MessageBox.Show("キャンセルされました")
        ElseIf result = DialogResult.Abort Then
            'エラー情報を取得する
            Dim ex As Exception = pd.Error
            MessageBox.Show("エラー: " + ex.Message)
        ElseIf result = DialogResult.OK Then
            '結果を取得する
            Dim stopTime As Integer = CInt(pd.Result)
            MessageBox.Show("成功しました: " & stopTime.ToString())
        End If

        '後始末
        pd.Dispose()




        MsgBox("終了")


    End Sub

    Private Sub 出場者取り込み(ByVal sender As Object, ByVal e As DoWorkEventArgs)


        '===進捗ダイアログ初期処理、====
        Dim bw As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        'パラメータを取得する
        Dim stopTime As Integer = CInt(e.Argument)
        '===進捗ダイアログ初期処理、ここまで====



        Dim JS_変換 As JS_変換_C
        JS_変換 = New JS_変換_C(Me.TB_支援システムパス.Text, Me.TB_競技会パス.Text)

        JS_変換.選手マスタ変換(bw, e)

        JS_変換 = Nothing




        '結果を設定する　進捗ダイアログ
        e.Result = stopTime * 100

    End Sub



    Private Sub PB_ジャッジ_Click(sender As Object, e As EventArgs) Handles PB_ジャッジ.Click

    End Sub

    Private Sub PB_結果取り込み_Click(sender As Object, e As EventArgs) Handles PB_結果取り込み.Click

        If Me.TB_支援システムパス.Text = "" Then

            MsgBox("支援システムパス名を入力してください。"）
            Exit Sub

        End If


        'If MsgBox("ヒート表と結果を全て取り込みますがいいですか？", vbYesNo) = vbYes Then

        'Dim JS_変換 As JS_変換_C
        'JS_変換 = New JS_変換_C(Me.TB_支援システムパス.Text, Me.TB_競技会パス.Text)

        'Dim マスタデータ = New マスタデータ

        'For r = 1 To マスタデータ.C_ラウンドマスタ.登録済みレコード数
        'JS_変換.Heat結果変換(マスタデータ.C_ラウンドマスタ.リスト(r).区分番号, マスタデータ.C_ラウンドマスタ.リスト(r).ラウンド番号)
        'Next r

        'JS_変換 = Nothing

        'Else

        Dim F902 As F902_支援システム集計
        F902 = New F902_支援システム集計(Me.TB_支援システムパス.Text, Me.TB_競技会パス.Text)
        F902.Show()


        'End If

    End Sub
End Class