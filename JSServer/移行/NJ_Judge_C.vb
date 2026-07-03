Public Class NJ_Judge_C

    '//   1列目　J or D
    '//   2列目　審判員記号
    '//   3列目　審判員個別記号
    '//   4列目　審判長FlAG 1 or0
    '//   5列目　審判員名(日本語）
    '//   6列目　審判員名(英語）
    '//   7列目　所属
    '//   8列目　採点対象FLAG


    Private リスト(,)
    Const Filename = "NJ_Judge.csv"

    Public Sub Fileread(ByVal パス名)

        '///ファイルから読み込んでリストにデータをセット

        Dim 列数 = 8

        Dim fileNo As Integer = FreeFile()
        Dim i, j As Integer
        Dim ReadLine As String
        Dim ReadLine_s()
        ReDim ReadLine_s(列数)
        ReDim リスト(200, 列数 + 1)

        'パス名の最後が \ だったら削除する
        パス名 = パス名.trimEnd("\")


        'ファイルの存在チェック
        If Dir(パス名 & "\" & Filename) <> Filename Then
            MsgBox("ファイル「" & パス名 & "\" & Filename & "」はありません")
            '読み込み = 1 'ファイルの読み込み失敗の返り値
            Exit Sub
        End If



        FileOpen(fileNo, パス名 & "\" & Filename, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)

        i = 1
        Do Until EOF(fileNo)
            ReadLine = LineInput(fileNo)
            ReadLine_s = Split(ReadLine, ",")
            For j = 1 To UBound(ReadLine_s) + 1
                リスト(i, j) = ReadLine_s(j - 1)
            Next j
            i = i + 1
        Loop

        FileClose(fileNo)               'ファイルを閉じる


    End Sub


    Public Function Get_リスト()

        Get_リスト = リスト

    End Function

End Class
