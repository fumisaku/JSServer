Imports System.ComponentModel
Imports System.Windows.Forms

' <summary>
' ProgressDialogクラスのコンストラクタ
' </summary>
' <param name="caption">タイトルバーに表示するテキスト</param>
' <param name="doWorkHandler">バックグラウンドで実行するメソッド</param>
' <param name="argument">doWorkで取得できるパラメータ</param>

Public Class FP01_進捗状況

    Inherits Form

    Private Sub FP01_進捗状況_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    ''' <summary>
    ''' バックグラウンド処理の進行状況を表示するフォーム
    ''' </summary>

    Public Sub New(ByVal caption As String,
                   ByVal doWork As DoWorkEventHandler,
                   ByVal argument As Object)
        InitializeComponent()

        '初期設定
        Me.workerArgument = argument
        Me.Text = caption
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.ShowInTaskbar = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.ControlBox = False
        Me.CancelButton = Me.cancelAsyncButton
        Me.MessageLabel.Text = ""
        Me.ProgressBar1.Minimum = 0
        Me.ProgressBar1.Maximum = 100
        Me.ProgressBar1.Value = 0
        Me.cancelAsyncButton.Text = "キャンセル"
        Me.cancelAsyncButton.Enabled = True
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True

        'イベント
        AddHandler Me.Shown, New EventHandler(AddressOf ProgressDialog_Shown)
        AddHandler Me.cancelAsyncButton.Click, New EventHandler(AddressOf cancelAsyncButton_Click)
        AddHandler Me.BackgroundWorker1.DoWork, doWork
        AddHandler Me.BackgroundWorker1.ProgressChanged, New ProgressChangedEventHandler(AddressOf backgroundWorker1_ProgressChanged)
        AddHandler Me.BackgroundWorker1.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf backgroundWorker1_RunWorkerCompleted)


    End Sub

    ''' <summary>
    ''' ProgressDialogクラスのコンストラクタ
    ''' </summary>
    Public Sub New(ByVal formTitle As String,
                   ByVal doWorkHandler As DoWorkEventHandler)
        Me.New(formTitle, doWorkHandler, Nothing)
    End Sub

    Private workerArgument As Object = Nothing

    Private _result As Object = Nothing
    ''' <summary>
    ''' DoWorkイベントハンドラで設定された結果
    ''' </summary>
    Public ReadOnly Property Result() As Object
        Get
            Return Me._result
        End Get
    End Property

    Private _error As Exception = Nothing
    ''' <summary>
    ''' バックグラウンド処理中に発生したエラー
    ''' </summary>
    Public ReadOnly Property [Error]() As Exception
        Get
            Return Me._error
        End Get
    End Property

    ''' <summary>
    ''' 進行状況ダイアログで使用しているBackgroundWorkerクラス
    ''' </summary>
    Public ReadOnly Property BackgroundWorker() As BackgroundWorker
        Get
            Return Me.BackgroundWorker1
        End Get
    End Property

    'フォームが表示されたときにバックグラウンド処理を開始
    Private Sub ProgressDialog_Shown(ByVal sender As Object,
                                     ByVal e As EventArgs)
        Me.BackgroundWorker1.RunWorkerAsync(Me.workerArgument)
    End Sub

    'キャンセルボタンが押されたとき
    Private Sub cancelAsyncButton_Click(ByVal sender As Object,
                                        ByVal e As EventArgs)
        cancelAsyncButton.Enabled = False
        BackgroundWorker1.CancelAsync()
    End Sub

    'ReportProgressメソッドが呼び出されたとき
    Private Sub backgroundWorker1_ProgressChanged(ByVal sender As Object,
                                        ByVal e As ProgressChangedEventArgs)
        'プログレスバーの値を変更する
        If e.ProgressPercentage < Me.ProgressBar1.Minimum Then
            Me.ProgressBar1.Value = Me.ProgressBar1.Minimum
        ElseIf Me.ProgressBar1.Maximum < e.ProgressPercentage Then
            Me.ProgressBar1.Value = Me.ProgressBar1.Maximum
        Else
            Me.ProgressBar1.Value = e.ProgressPercentage
        End If
        'メッセージのテキストを変更する
        Me.MessageLabel.Text = DirectCast(e.UserState, String)
    End Sub

    'バックグラウンド処理が終了したとき
    Private Sub backgroundWorker1_RunWorkerCompleted(ByVal sender As Object,
                                     ByVal e As RunWorkerCompletedEventArgs)
        If e.Error IsNot Nothing Then
            MessageBox.Show(Me, "エラー",
                            "エラーが発生しました。" & vbCrLf & vbCrLf &
                                e.Error.Message, MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
            Me._error = e.Error
            Me.DialogResult = DialogResult.Abort
        ElseIf e.Cancelled Then
            Me.DialogResult = DialogResult.Cancel
        Else
            Me._result = e.Result
            Me.DialogResult = DialogResult.OK
        End If

        Me.Close()
    End Sub

End Class