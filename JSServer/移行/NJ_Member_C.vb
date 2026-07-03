Public Class NJ_Member_C
    '// エントリー情報

    Const Filename = "NJ_Member.csv"

    Private リスト(1000, 8)

    Const 列_背番号 = 1
    Const 列_リーダー名_漢字 = 2
    Const 列_リーダー名_カナ = 3
    Const 列_パートナー名_漢字 = 4
    Const 列_パートナー名_カナ = 5
    Const 列_所属 = 6
    Const 列_リーダー名_英字 = 7
    Const 列_パートナー名_英字 = 8


    'Private 背番号()
    'Private リーダー名_漢字()
    'Private リーダー名_カナ()
    'Private パートナー名_漢字()
    'Private パートナー名_カナ()
    'Private 所属()
    'Private リーダー名_英語()
    'Private パートナー名_英語()


    Private 出場区分(,)

    Public Sub Fileread(ByVal パス名)

        '///ファイルから読み込んでリストにデータをセット

        Dim 列数 = 7

        Dim fileNo As Integer = FreeFile()
        Dim i, j As Integer
        Dim ReadLine As String
        Dim ReadLine_s()
        ReDim ReadLine_s(列数)
        ReDim リスト(1000, 8)

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

    Public Function Get_選手データ(ByVal 背番号_)
        '該当の背番号が無い時はブランクを返す

        Dim i, j
        Dim RC(8)

        For i = 1 To UBound(リスト)
            If 背番号_ = リスト(i, 列_背番号) Then

                For j = 1 To 8
                    RC(j) = リスト(i, j)
                Next j

                i = UBound(リスト)
            End If

        Next i
        Get_選手データ = RC
    End Function


End Class
