Imports System
Imports System.IO

Public Class NJ_Comp_C

    '1行～３行目
    '//   1列目　見出し
    '//   2列目　大会名１～３、日本語
    '//   3列目　大会名１～３、英語
    '４行目
    '//   1列目　見出し
    '//   2列目　会場名
    '５行目
    '//   1列目　見出し
    '//   2列目　開催日
    '6行目～12行目
    '//   1列目　種目番号
    '//   2列目　最終予選　Group or Solo
    '//   3列目　最終予選　種目記号　（W,T,...)
    '//   4列目　準決勝　Group or Solo
    '//   5列目　準決勝　種目記号　（W,T,...)
    '//   6列目　決勝　Group or Solo
    '//   7列目　決勝　種目記号　（W,T,...)
    '//   8列目  最終予選 ヒート予定数
    '//   9列目  最終予選 UP予定数
    '//   10列目  準決勝 ヒート予定数
    '//   11列目  準決勝 UP予定数
    '//   12列目  決勝 ヒート予定数　（Soloは選手数、Groupは1で固定）

    Private NJ_パス名 As String

    Private リスト(,)
    Const Filename = "NJ_Comp.csv"

    Public Sub Fileread(ByVal パス名)

        '///ファイルから読み込んでリストにデータをセット

        Dim 列数 = 12

        Dim fileNo As Integer = FreeFile()
        Dim i, j As Integer
        Dim ReadLine As String
        Dim ReadLine_s()
        ReDim ReadLine_s(列数)
        ReDim リスト(13, 列数 + 1)


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


        NJ_パス名 = パス名

    End Sub

    Public Function Get_リスト()

        Get_リスト = リスト

    End Function

End Class
