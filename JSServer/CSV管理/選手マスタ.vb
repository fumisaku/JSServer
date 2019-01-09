Public Class 選手マスタ

    Private 背番号()
    Private リーダー会員番号()
    Private リーダー氏名()
    Private リーダーフリガナ()
    Private リーダー表記名()





    ''' コンストラクタ
    ''' 
    Sub New()




    End Sub

    ''' 読込み
    ''' 
    Private Sub FileRead(filepath As String, filename As String)

        'ファイルの存在チェック
        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            MsgBox("ファイル「" & filepath & filename & "」はありません")

            Exit Sub
        End If


        ' StreamReader の新しいインスタンスを生成する
        Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)

        ' 読み込んだ結果をすべて格納するための変数を宣言する
        Dim stResult As String = String.Empty

        ' 読み込みできる文字がなくなるまで繰り返す
        While (cReader.Peek() >= 0)
            ' ファイルを 1 行ずつ読み込む
            Dim stBuffer As String = cReader.ReadLine()
            ' 読み込んだものを追加で格納する
            stResult &= stBuffer & System.Environment.NewLine
        End While

        ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
        cReader.Close()

        ' 結果を表示する
        MessageBox.Show(stResult)

    End Sub


End Class
