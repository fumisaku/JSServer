Public Class CompCSV_C

    Private CompCSV_s(1, 1)
    Const Max行数 = 300
    Const Max列数 = 191

    Private I_決勝進出者数 As Integer
    Private I_ジャッジ人数 As Integer
    Private I_採点対象ジャッジ人数 As Integer
    Private I_減点対象ジャッジ人数 As Integer
    Private I_ソロ演技順(50, 5) As Integer  '==最大50人、最大ソロは5種目

    Private I_ヒート表_ヒート順(50, 10, 5)  '==最大50ヒート,最大8組/Heat  ×５種目
    '０行目 (0,0,x)  :ラウンド記号
    '０行目 (0,1,x)  :種目記号
    '０行目 (0,2,x)  :出場組数
    '０行目 (0,3,x)  :ヒート数
    '０行目 (0,4,x)  :ソロ/全員種別
    Private I_ヒート表_背番号順(50, 5)   '==最大50組,最大５種目

    Private 採点方式

    Private 競技Noリスト(10, 7)
    Private 競技種目数, ソロ種目数, 全員種目数 As Integer


    Private I_ジャッジ割(20, 50)  'ジャッジ割 配列　2018/12/06　追加



    Private ファイル名
    Public パス名


    Public Function ReadFile(ByVal filepath As String, ByVal filename As String)
        '概要   CompCSVの読み込み
        'Input  ファイルのパス
        'Return 結果(0:成功,1:失敗）

        パス名 = filepath
        If filename = "" Then
            filename = "comp.csv"
        End If
        'Dim fileName As String = "comp.csv"

        If CompCSVの読み込み(filepath, filename) = 0 Then
            ソロ演技順の準備()
            ReadFile = 0 '==ファイル読み込み成功
        Else
            ReadFile = 1 '==ファイル読み込み失敗
        End If

        ファイル名 = filename
    End Function



    Public Sub Get_ヒート表_ヒート順(ByRef ヒート表_ヒート順)
        ヒート表_ヒート順 = I_ヒート表_ヒート順

    End Sub
    Public Sub Get_ヒート表_背番号順(ByRef ヒート表_背番号順)
        ヒート表_背番号順 = I_ヒート表_背番号順

    End Sub


    Private Function CompCSV(ByVal i, ByVal j)
        Return CompCSV_s(i, j)
    End Function

    Function 背番号(ByVal 選手番号)
        '概要   選手番号を渡すと選手の背番号を返す
        'Input  選手番号(1～8)
        'Return 背番号

        Return CompCSV_s(選手番号 + 1, 1)
    End Function
    Function リーダー名_J(ByVal 選手番号)
        '概要   選手番号を渡すとリーダーの日本語名を返す
        'Input  選手番号(1～8)
        'Return リーダーの名前日本語

        Return CompCSV_s(選手番号 + 1, 2)
    End Function
    Function リーダー名_カナ(ByVal 選手番号)
        '概要   選手番号を渡すとリーダーの日本語名カナを返す
        'Input  選手番号(1～8)
        'Return リーダーの名前日本語カナ

        Return CompCSV_s(選手番号 + 1, 3)
    End Function



    Function パートナー名_J(ByVal 選手番号)
        '概要   選手番号を渡すとパートナーの日本語名を返す
        'Input  選手番号(1～8)
        'Return パートナーの名前日本語

        Return CompCSV_s(選手番号 + 1, 5)
    End Function
    Function パートナー名_カナ(ByVal 選手番号)
        '概要   選手番号を渡すとパートナーの日本語名カナを返す
        'Input  選手番号(1～8)
        'Return パートナーの名前日本語カナ

        Return CompCSV_s(選手番号 + 1, 6)
    End Function


    Function リーダー名_E(ByVal 選手番号)
        '概要   選手番号を渡すとリーダーの英語名を返す
        'Input  選手番号(1～8)
        'Return リーダーの名前英語

        Return CompCSV_s(選手番号 + 1, 4)
    End Function
    Function パートナー名_E(ByVal 選手番号)
        '概要   選手番号を渡すとパートナーの英語名を返す
        'Input  選手番号(1～8)
        'Return パートナーの名前英語

        Return CompCSV_s(選手番号 + 1, 7)
    End Function

    Function 所属(ByVal 選手番号)
        '概要   選手番号を渡すと選手の所属名を返す
        'Input  選手番号(1～8)
        'Return 選手の所属

        Return CompCSV_s(選手番号 + 1, 8)
    End Function
    Function FLAG(ByVal 選手番号)
        '概要   選手番号を渡すと選手の所属国の国旗のファイル名を返す
        'Input  選手番号(1～8)
        'Return 選手の所属国の国旗のファイル名

        '当面は、所属の名前を返す＝＝＝＝＝＝
        Return 所属(選手番号)
    End Function
    Function FLAG審判(ByVal 審判員番号)
        '概要   審判員番号を渡すと審判員の所属国の国旗のファイル名を返す
        'Input  審判員番号(1～１４)
        'Return 審判員の所属国の国旗のファイル名

        '当面は、所属の名前を返す＝＝＝＝＝＝
        Return 審判員所属(審判員番号)
    End Function


    Function 審判員記号(ByVal ジャッジ番号)
        '概要   ジャッジ番号を渡すとジャッジ記号を返す
        'Input  ジャッジ番号(1～13)
        'Return ジャッジ記号(A～S)

        Return CompCSV_s(1 + 決勝進出者数() + ジャッジ番号, 3)

    End Function

    Function 審判長判別(ByVal ジャッジ番号)
        '概要   ジャッジ番号を渡すと審判長かどうかを返す
        'Input  ジャッジ番号(1～13)
        'Return 1:審判長　　0:一般審判員

        Return CompCSV_s(1 + 決勝進出者数() + ジャッジ番号, 4)

    End Function

    Function 審判員名_J(ByVal ジャッジ番号)
        '概要   ジャッジ番号を渡すと審判員名（日本語名）を返す
        'Input  ジャッジ番号(1～13)
        'Return ジャッジ名

        Return CompCSV_s(1 + 決勝進出者数() + ジャッジ番号, 5)

    End Function

    Function 審判員名(ByVal ジャッジ番号)
        '概要   ジャッジ番号を渡すと審判員名を返す
        'Input  ジャッジ番号(1～13)
        'Return ジャッジ名

        Return CompCSV_s(1 + 決勝進出者数() + ジャッジ番号, 6)

    End Function

    Function 審判員所属(ByVal ジャッジ番号)
        '概要   ジャッジ番号を渡すと審判員所属を返す
        'Input  ジャッジ番号(1～13)
        'Return ジャッジ名

        Return CompCSV_s(1 + 決勝進出者数() + ジャッジ番号, 8)

    End Function

    Function 審判担当PCS(ByVal ジャッジ番号, ByVal 曲番号)
        '概要   ジャッジ番号と、曲番号を渡すと審判員が担当するPCS番号を返す
        'Input  ジャッジ番号(1～13),曲番号(1～40)
        'Return PCS番号(1～4)
        'V2.1で追加

        Return CompCSV_s(1 + 決勝進出者数() + ジャッジ番号, 8 + 曲番号)

    End Function


    Function 技術判定員記号(ByVal 技術判定員番号)
        '概要   技術判定員番号を渡すと技術判定員記号を返す
        'Input  技術判定員番号(1～13)
        'Return 技術判定員記号(A～S)

        Dim i
        Dim 開始点 = 0

        If 技術判定員人数() >= 1 Then
            For i = 1 To 100
                If CompCSV_s(i, 1) = "T" Then
                    開始点 = i
                    i = 100
                End If
            Next i
            Return CompCSV_s(開始点 - 1 + 技術判定員番号, 2)
        Else
            Return ""
        End If

    End Function

    Function 技術判定員名(ByVal 技術判定員番号)
        '概要   技術判定員番号を渡すと技術判定員名を返す
        'Input  技術判定員番号(1～13)
        'Return 技術判定員名

        Dim i
        Dim 開始点 = 0

        If 技術判定員人数() >= 1 Then
            For i = 1 To 100
                If CompCSV_s(i, 1) = "T" Then
                    開始点 = i
                    i = 100
                End If
            Next i
            Return CompCSV_s(開始点 - 1 + 技術判定員番号, 6)
        Else
            Return ""
        End If

    End Function
    Function 技術判定員名_J(ByVal 技術判定員番号)
        '概要   技術判定員番号を渡すと技術判定員名を返す
        'Input  技術判定員番号(1～13)
        'Return 技術判定員名

        Dim i
        Dim 開始点 = 0

        If 技術判定員人数() >= 1 Then
            For i = 1 To 100
                If CompCSV_s(i, 1) = "T" Then
                    開始点 = i
                    i = 100
                End If
            Next i
            Return CompCSV_s(開始点 - 1 + 技術判定員番号, 5)
        Else
            Return ""
        End If

    End Function


    Function 区分_旧()
        '概要　採点区分を返す　（この機能は当面使用しない）
        Dim i
        For i = 1 To 100
            If CompCSV_s(i, 1) = "M" Then
                Return CompCSV_s(i, 3)
            End If
        Next
        Return "01"
    End Function
    Function 区分()
        '概要　採点区分を返す

        区分 = 採点方式

    End Function
    Sub Set_区分(ByVal 区分_s)
        '概要　採点区分をセットする。Resultiniからの設定

        採点方式 = 区分_s

    End Sub



    Function ジャッジ人数()
        '概要   CompCSVに設定されたジャッジの人数を返す
        '       （採点対象外、減点対象審判も含む）
        'Input  なし
        'Return ジャッジ人数

        Return I_ジャッジ人数
    End Function
    Function 採点対象ジャッジ人数()
        '概要   採点対象のジャッジの人数を返す
        '       （採点対象外、減点対象審判は含まない）
        'Input  なし
        'Return 採点対象ジャッジ人数

        Return I_採点対象ジャッジ人数
    End Function

    Function 減点対象ジャッジ人数()
        '概要   減点対象のジャッジの人数を返す
        'Input  なし
        'Return 減点対象ジャッジ人数

        Return I_減点対象ジャッジ人数
    End Function
    Function 技術判定員人数()
        '概要   技術判定員の人数を返す
        'Input  なし
        'Return 技術判定員数

        Return CInt(CompCSV(1, 10))
    End Function

    Function 決勝進出者数()
        '概要   決勝進出者数を返す
        'Input  なし
        'Return 決勝進出者数

        Return I_決勝進出者数
    End Function

    Function 選手番号(ByVal 背番号)
        '概要   背番号を渡すと選手番号を返す。
        'Input 背番号
        'output 選手番号

        Dim i As Integer
        Dim 選手番号_s = 0

        For i = 2 To 決勝進出者数() + 2
            If CompCSV_s(i, 1) = 背番号 Then
                選手番号_s = i - 1
                i = 決勝進出者数() + 2
            End If
        Next i

        Return 選手番号_s


    End Function

    Function ヒート番号(ByVal 選手番号, ByVal 種目番号)
        '概要   ヒートを返す。
        'Input  演技順(1～8), 種目番号(1～5)
        'Return ヒート番号(1～8)

        Dim H

        H = CompCSV_s(選手番号 + 1, 9 + (種目番号 - 1) * 22)

        Return H

    End Function


    Function ソロ演技順(ByVal 演技順, ByVal ソロ種目番号)
        '概要   ソロ競技の選手番号を返す。（xx番目に演技する選手の番号）
        'Input  演技順(1～8), ソロ種目番号(1～5)
        'Return 選手番号(1～8)

        'I_ソロ演技順()は以下の内容
        '第1列　選手番号(1～8)  ※背番号順の選手番号
        '第2列　ソロ種目番号(1～5)


        Return I_ソロ演技順(演技順, ソロ種目番号)
    End Function

    Function 採点対象フラグ(ByVal ジャッジ番号)
        Return CompCSV_s(1 + 決勝進出者数() + ジャッジ番号, 7)
    End Function
    Function 競技会名１日本語()
        Return CompCSV_s(1, 1)
    End Function
    Function 競技会名２日本語()
        Return CompCSV_s(1, 2)
    End Function
    Function 競技会名３日本語()
        Return CompCSV_s(1, 3)
    End Function
    Function 競技会名１英語()
        Return CompCSV_s(1, 4)
    End Function
    Function 競技会名２英語()
        Return CompCSV_s(1, 5)
    End Function
    Function 競技会名３英語()
        Return CompCSV_s(1, 6)
    End Function
    Function 競技会開催日()
        Return CompCSV_s(1, 7)
    End Function



    Function 種目名称_E_OLD(ByVal 種目記号)
        '概要   種目記号を渡すと英語の種目名称を返す
        'Input  種目記号(W～J)
        'Return 種目名称(Waltz～Jive)

        Dim 種目名 As String

        種目名 = ""

        Select Case 種目記号
            Case "W"
                種目名 = "Waltz"
            Case "T"
                種目名 = "Tango"
            Case "F"
                種目名 = "Slow Foxtrot"
            Case "Q"
                種目名 = "Qickstep"
            Case "V"
                種目名 = "Viennese Waltz"
            Case "S"
                種目名 = "Samba"
            Case "R"
                種目名 = "Rumba"
            Case "C"
                種目名 = "Cha Cha Cha"
            Case "P"
                種目名 = "Paso Doble"
            Case "J"
                種目名 = "Jive"

        End Select

        Return 種目名
    End Function

    Function 種目名称_J_old(ByVal 種目記号)
        '概要   種目記号を渡すと日本語の種目名称を返す
        'Input  種目記号(W～J)
        'Return 種目名称(ワルツ～ジャイブ)

        Dim 種目名 As String

        種目名 = ""

        Select Case 種目記号
            Case "W"
                種目名 = "ワルツ"
            Case "T"
                種目名 = "タンゴ"
            Case "F"
                種目名 = "スローフォックストロット"
            Case "Q"
                種目名 = "クイックステップ"
            Case "V"
                種目名 = "ヴィエニーズワルツ"
            Case "S"
                種目名 = "サンバ"
            Case "R"
                種目名 = "ルンバ"
            Case "C"
                種目名 = "チャチャチャ"
            Case "P"
                種目名 = "パソドブレ"
            Case "J"
                種目名 = "ジャイブ"

        End Select

        Return 種目名
    End Function

    Function JDSF種目番号(ByVal 種目記号)
        '概要   種目記号を渡すと有田さんシステム用の種目番号を返す
        'Input  種目記号(W～J)
        'Return JDSF種目番号(01～10)

        Dim J種目 As String

        J種目 = "00"

        Select Case 種目記号
            Case "W"
                J種目 = "01"
            Case "T"
                J種目 = "02"
            Case "F"
                J種目 = "04"
            Case "Q"
                J種目 = "05"
            Case "V"
                J種目 = "03"
            Case "S"
                J種目 = "06"
            Case "R"
                J種目 = "08"
            Case "C"
                J種目 = "07"
            Case "P"
                J種目 = "09"
            Case "J"
                J種目 = "10"
        End Select

        Return J種目
    End Function

    Function ソロ競技種目名_Jxxxxxx(ByVal 種目番号)
        '未使用 2011/9/8

        Dim 種目名 As String

        種目名 = ""

        Select Case CompCSV_s(1, 12 + (種目番号 - 1) * 12)
            Case "W"
                種目名 = "ワルツ"
            Case "T"
                種目名 = "タンゴ"
            Case "F"
                種目名 = "スローフォックストロット"
            Case "Q"
                種目名 = "クイックステップ"
            Case "V"
                種目名 = "ヴェニーズワルツ"
            Case "S"
                種目名 = "サンバ"
            Case "R"
                種目名 = "ルンバ"
            Case "C"
                種目名 = "チャチャチャ"
            Case "P"
                種目名 = "パソドブレ"
            Case "J"
                種目名 = "ジャイブ"

        End Select

        Return 種目名
    End Function



    Private Function CompCSVの読み込み(ByVal filepath As String, ByVal fileName As String)
        '概要   Comp.csvを読み込み、構造体に保存する。
        'Input  なし
        'Return なし

        '名前空間:  Microsoft.VisualBasic   モジュール:  FileSystem
        'Dim fileName As String = "comp.csv"    'ファイルのパス
        Dim fileNo As Integer = FreeFile()              'ファイル番号を取得
        Dim I As Integer
        Dim J As Integer
        Dim ReadLine As String
        Dim ReadLine_s(Max列数) As String

        ReDim CompCSV_s(Max行数, Max列数)

        'ファイルの存在チェック
        If Dir(filepath & fileName).ToUpper <> fileName.ToUpper Then
            MsgBox("ファイル「" & filepath & fileName & "」はありません")

            CompCSVの読み込み = 1 'ファイルの読み込み失敗の返り値
            Exit Function
        End If

        'ファイルを入力モードで開く
        FileOpen(fileNo, filepath & fileName, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)

        '==1行目の読み込み
        ReadLine = LineInput(fileNo)
        ReadLine_s = Split(ReadLine, ",")

        For J = 1 To UBound(ReadLine_s)
            CompCSV_s(1, J) = ReadLine_s(J - 1)
        Next J

        '==選手データの読み込み
        ReadLine = LineInput(fileNo)
        ReadLine_s = Split(ReadLine, ",")
        I_決勝進出者数 = 0

        Do Until ReadLine_s(0) = "J"
            For J = 1 To UBound(ReadLine_s)
                CompCSV_s(I_決勝進出者数 + 2, J) = ReadLine_s(J - 1)
            Next J
            ReadLine = LineInput(fileNo)
            ReadLine_s = Split(ReadLine, ",")
            I_決勝進出者数 = I_決勝進出者数 + 1
        Loop

        '==残りデータの読み込み
        I = 0
        Do Until EOF(fileNo)
            For J = 1 To UBound(ReadLine_s) + 1
                CompCSV_s(I_決勝進出者数 + 2 + I, J) = ReadLine_s(J - 1)
            Next J
            ReadLine = LineInput(fileNo)
            ReadLine_s = Split(ReadLine, ",")
            I = I + 1
        Loop
        For J = 1 To UBound(ReadLine_s)
            CompCSV_s(I_決勝進出者数 + 2 + I, J) = ReadLine_s(J - 1)
        Next J

        FileClose(fileNo)               'ファイルを閉じる

        '===ジャッジ人数の定義
        I_ジャッジ人数 = CompCSV_s(1, 9)


        '===採点対象ジャッジ人数の定義
        Dim Counter = 0

        For I = 1 To I_ジャッジ人数
            If CompCSV_s(1 + I_決勝進出者数 + I, 7) = 1 Then

                '=== 2018/12/06 追加
                For J = 1 To 48
                    I_ジャッジ割(Counter, J) = CompCSV_s(1 + I_決勝進出者数 + I, J + 8)
                Next J
                '==== ここまで
                Counter = Counter + 1
            End If
        Next
        I_採点対象ジャッジ人数 = Counter

        '===減点対象ジャッジ人数の定義
        I_減点対象ジャッジ人数 = 0
        For I = 1 To 50
            If CompCSV_s(I, 1) = "D" Then
                I_減点対象ジャッジ人数 = I_減点対象ジャッジ人数 + 1
            End If
        Next I

        競技Noの設定()

        ヒート表の準備()
        CompCSVの読み込み = 0 'ファイルの読み込み成功の返り値

    End Function
    Private Sub 競技Noの設定()

        '第1列 全員/ソロ種別(G or S)
        '第2列 種目記号(W～J)
        '第3列 競技No
        '第4列 ソロ競技No
        '第5列 全員競技No
        '第6列 種目順(ソロと全員の合計)

        Dim i

        '===競技Noリストの作成
        For i = 1 To 10
            '全員競技は"A"として、CompCSVに入っているので、"G"に変換する。
            If CompCSV_s(1, 11 + (i - 1) * 2) = "A" Then
                競技Noリスト(i, 1) = "G"
            Else
                競技Noリスト(i, 1) = CompCSV_s(1, 11 + (i - 1) * 2)
            End If
            競技Noリスト(i, 2) = CompCSV_s(1, 12 + (i - 1) * 2)
            競技Noリスト(i, 3) = i
        Next i

        Dim Counter_ソロ = 1
        Dim Counter_全員 = 1

        '===競技Noリストの更新
        For i = 1 To 10
            If 競技Noリスト(i, 1) = "S" Then
                競技Noリスト(i, 4) = Counter_ソロ
                Counter_ソロ = Counter_ソロ + 1
            End If
            If 競技Noリスト(i, 1) = "G" Then
                競技Noリスト(i, 5) = Counter_全員
                Counter_全員 = Counter_全員 + 1
            End If
            If 競技Noリスト(i, 1) = "" Then
                競技種目数 = i - 1
                i = 10
            End If
        Next i

        '===競技Noリストの更新（第６列）
        Dim 総合種目数 = 1, j = 0
        Dim 発見FLAG

        競技Noリスト(1, 6) = 1

        For i = 2 To 10
            発見FLAG = 0
            For j = 1 To i - 1
                If 競技Noリスト(j, 2) = 競技Noリスト(i, 2) Then
                    発見FLAG = 1
                End If
            Next j
            If 発見FLAG = 1 Then
                競技Noリスト(i, 6) = 競技Noリスト(j, 6)
            Else
                総合種目数 = 総合種目数 + 1
                競技Noリスト(i, 6) = 総合種目数
            End If

        Next i


    End Sub


    Private Sub ソロ演技順の準備()
        '概要   I_ソロ演技順を完成する。
        '第1列　ソロの演技順
        '第2列　ソロ種目番号(1～5)
        '   値  選手番号(1～8)  ※背番号順の選手番号

        Dim i As Integer
        Dim I_ソロ種目番号 As Integer
        Dim 演技順

        For I_ソロ種目番号 = 1 To 5
            If 競技Noリスト(I_ソロ種目番号, 1) = "S" Then
                'ソロの時
                For i = 1 To I_決勝進出者数


                    演技順 = CInt(CompCSV(i + 1, 9 + 22 * (I_ソロ種目番号 - 1)))
                    I_ソロ演技順(演技順, I_ソロ種目番号) = i

                    'Select Case CInt(CompCSV(i + 1, 9 + 22 * (I_ソロ種目番号 - 1)))  '==ソロの演技順
                    '    Case 1
                    'I_ソロ演技順(1, I_ソロ種目番号) = i
                    '    Case 2
                    'I_ソロ演技順(2, I_ソロ種目番号) = i
                    '    Case 3
                    'I_ソロ演技順(3, I_ソロ種目番号) = i
                    '    Case 4
                    'I_ソロ演技順(4, I_ソロ種目番号) = i
                    '    Case 5
                    'I_ソロ演技順(5, I_ソロ種目番号) = i
                    '    Case 6
                    'I_ソロ演技順(6, I_ソロ種目番号) = i
                    '    Case 7
                    'I_ソロ演技順(7, I_ソロ種目番号) = i
                    '    Case 8
                    'I_ソロ演技順(8, I_ソロ種目番号) = i
                    'End Select
                Next i
            Else
                'グループの時
                For i = 1 To I_決勝進出者数
                    I_ソロ演技順(i, I_ソロ種目番号) = i
                Next i

            End If
        Next I_ソロ種目番号
    End Sub

    Private Sub ヒート表の準備()
        '概要   ヒート表を完成する。
        'I_ヒート表_ヒート順(50, 10, 5)  '==最大50ヒート,最大8組/Heat  ×５種目
        '０行目 (0,0,x)  :ラウンド記号
        '０行目 (0,1,x)  :種目記号
        '０行目 (0,2,x)  :出場組数
        '０行目 (0,3,x)  :ヒート数
        '０行目 (0,4,x)  :ソロ/全員種別

        '０列目　ヒート毎の出場選手

        'I_ヒート表_背番号順(50, 5)   '==最大50組,最大５種目
        '０列目　背番号

        Dim 種目, 選手, ヒート番号, ヒート毎の出場選手数

        For 種目 = 1 To Get競技区分数()
            I_ヒート表_ヒート順(0, 0, 種目) = ""   'ラウンド記号
            I_ヒート表_ヒート順(0, 1, 種目) = 競技Noリスト(種目, 2)  '種目記号
            I_ヒート表_ヒート順(0, 2, 種目) = 決勝進出者数()         '出場組数
            I_ヒート表_ヒート順(0, 3, 種目) = 0   'ヒート数
            I_ヒート表_ヒート順(0, 4, 種目) = 競技Noリスト(種目, 1)  '全員/ソロ種別(G or S)

            For 選手 = 1 To 決勝進出者数()
                ヒート番号 = CInt(CompCSV(選手 + 1, 9 + 22 * (種目 - 1)))  '==ソロの演技順

                'MAXヒート数の確認
                If I_ヒート表_ヒート順(0, 3, 種目) < ヒート番号 Then
                    I_ヒート表_ヒート順(0, 3, 種目) = ヒート番号
                End If


                ヒート毎の出場選手数 = I_ヒート表_ヒート順(ヒート番号, 0, 種目)
                'If ヒート毎の出場選手数 = "" Then
                'ヒート毎の出場選手数 = 0
                'I_ヒート表_ヒート順(ヒート番号, 0, 種目) = 0
                'End If
                I_ヒート表_ヒート順(ヒート番号, ヒート毎の出場選手数 + 1, 種目) = CompCSV(選手 + 1, 1)  '背番号
                I_ヒート表_ヒート順(ヒート番号, 0, 種目) = I_ヒート表_ヒート順(ヒート番号, 0, 種目) + 1  'ヒート毎の出場選手数を足しこみ


                I_ヒート表_背番号順(選手, 0) = CompCSV(選手 + 1, 1)  '背番号
                I_ヒート表_背番号順(選手, 種目) = ヒート番号

            Next 選手

        Next 種目

    End Sub



    'GetSL区分()
    '概要   Standard、Latinの区別を返す。
    'Input　なし
    'Return "S" or "L"
    Public Function GetSL区分()

        Dim SL区分 = "S"
        If 競技Noリスト(1, 2) = "C" Or
               競技Noリスト(1, 2) = "S" Or
               競技Noリスト(1, 2) = "R" Or
               競技Noリスト(1, 2) = "P" Or
               競技Noリスト(1, 2) = "J" Then
            SL区分 = "L"
        End If

        Return SL区分

    End Function

    'Get競技区分数()
    '概要   競技区分の数を返す。（全員/ソロを合わせた数）
    'Input　なし
    'Return 1～10 
    Public Function Get競技区分数()

        Dim i
        Dim 区分数 = 0

        For i = 1 To 10
            If 競技Noリスト(i, 2) = "" Then
                区分数 = i - 1
                i = 10
            End If
        Next i

        Return 区分数

    End Function

    'Get競技区分_種目記号(ByVal 競技区分No)
    '概要   競技区分の種目名を返す
    'Input　競技区分No（1～10）
    'Return 種目記号
    Public Function Get競技区分_種目記号(ByVal 競技区分No)

        Return 競技Noリスト(競技区分No, 2)

    End Function

    'Getソロ種目順(ByVal 競技区分No As Integer)
    '概要   全体の区分No(1～10)を渡すと、ソロの種目順を返す。
    'Input  全体の区分No(1～10)
    'Return ソロの種目順(1～5)
    Public Function ソロ種目順(ByVal 競技区分No As Integer)

        Return 競技Noリスト(競技区分No, 4)

    End Function

    'Get全員種目順(ByVal 競技区分No As Integer)
    '概要   全体の区分No(1～10)を渡すと、全員の種目順を返す。
    'Input  全体の区分No(1～10)
    'Return 全員の種目順(1～5)
    Public Function 全員種目順(ByVal 競技区分No As Integer)

        Return 競技Noリスト(競技区分No, 5)

    End Function



    'Getソロ種目記号(ByVal ソロ種目順 As Integer)
    '概要   ソロの種目順（1～5）を渡すと、種目記号を返す。
    'Input  ソロの種目順(1～5)
    'Return 種目記号
    Public Function Getソロ種目記号(ByVal ソロ種目順 As Integer)
        '概要   ソロの種目順（1～5）を渡すと、種目記号を返す。
        'Input  ソロの種目順(1～5)　
        'Return 種目記号

        Dim i, counter As Integer
        Dim 種目記号 As String

        種目記号 = ""  '初期化
        counter = 1

        For i = 1 To 10
            If 競技Noリスト(i, 1) = "S" Then
                If counter = ソロ種目順 Then
                    種目記号 = 競技Noリスト(i, 2)
                    i = 10
                End If
                counter = counter + 1
            End If
        Next


        Getソロ種目記号 = 種目記号

    End Function

    'Get全員種目記号(ByVal 全員競技種目順 As Integer)
    '概要   全員競技の種目順（1～5）を渡すと、種目記号を返す。
    'Input  全員競技の種目順(1～5)
    'Return 種目記号
    Public Function Get全員種目記号(ByVal 全員競技種目順 As Integer)
        '概要   全員競技の種目順（1～5）を渡すと、種目記号を返す。
        'Input  全員競技の種目順(1～5)
        'Return 種目記号

        Dim i, counter As Integer
        Dim 種目記号 As String

        種目記号 = ""  '初期化
        counter = 1

        For i = 1 To 10
            If 競技Noリスト(i, 1) = "G" Then
                If counter = 全員競技種目順 Then
                    種目記号 = 競技Noリスト(i, 2)
                    i = 10
                End If
                counter = counter + 1
            End If
        Next

        Get全員種目記号 = 種目記号

    End Function

    'Get総合種目番号(ByVal 競技区分No As Integer)
    '概要   競技区分No（1～10）を渡すと、総合種目順（1～5）を返す。
    'Input  競技区分(1～10)
    'Return 総合種目順（1～5）
    Public Function Get総合種目番号(ByVal 競技区分No As Integer)

        Return 競技Noリスト(競技区分No, 6)

    End Function

    'Get総合種目記号(ByVal 種目順 As Integer)
    '概要   種目順（1～5）を渡すと、総合種目記号を返す。DanceCSVｘの番号に使用
    'Input  種目順(1～5)
    'Return 種目記号
    Public Function Get総合種目記号(ByVal 種目順 As Integer)
        '概要   種目順（1～5）を渡すと、総合種目記号を返す。DanceCSVｘの番号に使用
        'Input  種目順(1～5)
        'Return 種目記号

        Dim i, j, counter, 発見Flag As Integer
        Dim 種目リスト(6)

        '==種目リストの作成
        counter = 1
        For i = 1 To Get種目数()  '競技Noリストのサーチ
            発見Flag = 0
            For j = 1 To 5   '種目リストのサーチ
                If 種目リスト(j) = 競技Noリスト(i, 2) Then
                    発見Flag = 1
                End If
            Next j

            If 発見Flag = 0 Then
                種目リスト(counter) = 競技Noリスト(i, 2)
                counter = counter + 1
            End If
        Next i

        If 種目順 <= 5 Then

            Get総合種目記号 = 種目リスト(種目順)

        Else
            Get総合種目記号 = ""
        End If

    End Function

    'Get競技No(ByVal 全員ソロ種別 As String, ByVal 種目記号 As String)
    '概要   全員競技かソロ競技の種別と、種目記号を渡すと、競技Noを返す。
    'Input  全員/ソロの種別("G" or "S")
    '       種目記号("W","T" ....)
    'Return 競技No（1～10）ソロと全員競技を合わせた順番
    Public Function Get競技No(ByVal 全員ソロ種別 As String, ByVal 種目記号 As String)

        Dim i As Integer
        Dim 競技No As Integer

        競技No = 0  '初期化

        For i = 1 To 10
            If 競技Noリスト(i, 1) = 全員ソロ種別 And
               競技Noリスト(i, 2) = 種目記号 Then

                競技No = 競技Noリスト(i, 3)
                i = 10
            End If
        Next

        Get競技No = 競技No
    End Function

    'Getソロ競技No(ByVal 種目記号 As String)
    '概要   種目記号を渡すと、ソロ競技の種目順を返す。
    'Input  種目記号("W","T" ....)
    'Return ソロの競技No（1～5）ソロだけの順番
    Public Function Getソロ競技No(ByVal 種目記号 As String)

        Dim i, counter As Integer
        Dim 競技No As Integer

        競技No = 0  '初期化
        counter = 1

        For i = 1 To 10
            If 競技Noリスト(i, 1) = "S" Then
                If 競技Noリスト(i, 2) = 種目記号 Then
                    競技No = counter
                    i = 10
                End If
                counter = counter + 1
            End If
        Next

        Getソロ競技No = 競技No
    End Function

    Public Function Get全員競技No(ByVal 種目記号 As String)
        '概要   種目記号を渡すと、全員競技の種目順を返す。
        'Input  種目記号("W","T" ....)
        'Return 全員の競技No（1～5）全員だけの順番

        Dim i, counter As Integer
        Dim 競技No As Integer

        競技No = 0  '初期化
        counter = 1

        For i = 1 To 10
            If 競技Noリスト(i, 1) = "G" Then
                If 競技Noリスト(i, 2) = 種目記号 Then
                    競技No = counter
                    i = 10
                End If
                counter = counter + 1
            End If
        Next

        Get全員競技No = 競技No
    End Function
    Public Function Getソロ種目数()
        '概要   ソロの種目数（1～5）を返す
        'Input  なし
        'Return ソロの種目数（1～5）を返す

        Dim i, counter As Integer
        counter = 0

        For i = 1 To 10
            If 競技Noリスト(i, 1) = "S" Then
                counter = counter + 1
            End If
        Next i
        Getソロ種目数 = counter
    End Function
    Public Function Get全員種目数()
        '概要   全員競技の種目数（1～5）を返す
        'Input  なし
        'Return 全員競技の種目数（1～5）を返す

        Dim i, counter As Integer
        counter = 0

        For i = 1 To 10
            If 競技Noリスト(i, 1) = "G" Then
                counter = counter + 1
            End If
        Next i
        Get全員種目数 = counter
    End Function

    Public Function Get種目数()
        '概要   ソロと全員競技の種目数（1～5）を返す
        'Input  なし
        'Return 種目数（1～5）を返す

        Dim 種目数, i, j, FLAG
        Dim 種目リスト(6)

        種目数 = 0

        For i = 1 To 10
            FLAG = 0
            For j = i - 1 To 1 Step -1
                If 競技Noリスト(i, 2) = 競技Noリスト(j, 2) Then
                    FLAG = 1
                End If
            Next j
            If FLAG = 0 And 競技Noリスト(i, 2) <> "" Then
                種目数 = 種目数 + 1
                種目リスト(種目数) = 競技Noリスト(i, 2)
            End If
        Next i

        Return 種目数

    End Function

    Public Function SG判別(ByVal 競技No As Integer)
        '概要   競技Noを渡すと、ソロSか全員競技Gかの区分を返す
        'Input  競技No（1～10）
        'Return SGの判別を返す(S:ソロ　G:全員競技)

        SG判別 = 競技Noリスト(競技No, 1)

    End Function

    Public Function Get種目記号(ByVal 競技No As Integer)
        '概要   競技Noを渡すと、その種目記号を返す
        'Input  競技No（1～10）
        'Return 種目記号(W,T,F...)

        Get種目記号 = 競技Noリスト(競技No, 2)

    End Function

    Public Function Get採点システム区分()
        '概要   採点システム区分返す
        'Input  なし
        'Return I(ｱｲ):IDSF方式
        '       0:JDSF方式(フェーズ0)
        '       1:JDSF方式(フェーズ1)
        '       2:JDSF方式(フェーズ2)
        '       3:JDSF方式（2015年～ V2.1対応）

        Dim i
        Dim 区分 = ""
        For i = 1 To UBound(CompCSV_s)
            If CompCSV_s(i, 1) = "M" Then
                区分 = CompCSV_s(i, 2)
                i = UBound(CompCSV_s)
            End If
        Next i

        Get採点システム区分 = 区分

    End Function

    Public Function Get_キャリLow()
        '概要   キャリブレーションの最低点を返す
        'Input  なし
        'Return 

        Dim i
        Dim 値 = ""
        For i = 1 To UBound(CompCSV_s)
            If CompCSV_s(i, 1) = "M" Then
                値 = CompCSV_s(i, 4)
                i = UBound(CompCSV_s)
            End If
        Next i

        Get_キャリLow = 値


    End Function

    Public Function Get_キャリHigh()
        '概要   キャリブレーションの最高点を返す
        'Input  なし
        'Return 

        Dim i
        Dim 値 = ""
        For i = 1 To UBound(CompCSV_s)
            If CompCSV_s(i, 1) = "M" Then
                値 = CompCSV_s(i, 5)
                i = UBound(CompCSV_s)
            End If
        Next i

        Get_キャリHigh = 値


    End Function


    Public Function Get_PCS単位()
        '概要   PCSの最小点数単位を返す
        'Input  なし
        'Return 

        Dim i
        Dim 値 = ""
        For i = 1 To UBound(CompCSV_s)
            If CompCSV_s(i, 1) = "M" Then
                値 = CompCSV_s(i, 7)
                i = UBound(CompCSV_s)
            End If
        Next i

        Get_PCS単位 = 値


    End Function


    Public Function Getラウンド()
        '概要   ラウンド種別を返す
        'Input  
        'Return 決勝"FF", 準決勝"SF", 最終予選"QF"

        Dim 記号 = "FF"

        If Mid(ファイル名, 6, 2) = "QF" Then
            記号 = "QF"
        ElseIf Mid(ファイル名, 6, 2) = "SF" Then
            記号 = "SF"
        Else
            記号 = "FF"
        End If


        Getラウンド = 記号

    End Function

    Public Function ReadFile2(ByVal filepath As String, ByVal filename As String)
        '概要   CompCSVの読み込み
        'Input  ファイルのパス
        'Return 結果(0:成功,1:失敗）
        'Dim fileName As String = "comp.csv"

        If CompCSVの読み込み(filepath, filename) = 0 Then
            'ソロ演技順の準備()
            ReadFile2 = 0 '==ファイル読み込み成功
        Else
            ReadFile2 = 1 '==ファイル読み込み失敗
        End If

    End Function

End Class
