Public Class DanceCSV_C

    '===========================
    '概要　各種目毎の結果を保持する。
    '使い方　Newのあと、読み込みメソッドを必ず1回実行する。
    '===========================
    Const 列数 = 191   '==DanceCSVの列数


    Private 設定_全員倍率
    Private 設定_全員基礎点



    Private PCS倍率1
    Private PCS倍率2
    Private PCS倍率3
    Private PCS倍率4
    Private PCS倍率5


    Private DanceCSV(,)
    Private DanceCSV2(,)

    '===クラスの定義
    Dim CompCSV As CompCSV_C


    Dim 乖離度リスト
    Dim 除外リスト


    Sub New()
        '===========================
        '概要　クラスのインスタンス作成時に1回起動
        '入力　なし
        '出力　なし
        '===========================
        ReDim DanceCSV(1200, 列数)
        ReDim DanceCSV2(150, 列数)

    End Sub

    Public Function 読み込み(ByVal CompCSV_s As CompCSV_C, ByVal 種目記号 As String, ByVal パス名 As String)
        '===========================
        '概要　ファイルを読み込んで、結果を保持する
        '入力　表示設定ファイルクラス、　種目記号("W","T"....)
        '　　　パス名(何か入っている場合はこちらを優先する。もし入っていなければ、ResultINIを使用する）
        '出力　結果(0:OK、1:1ファイル読み込み失敗,2:2ファイル読み込み失敗)
        '===========================

        'CompCSV
        CompCSV = CompCSV_s

        '=表示設定ファイル


        '====ファイル名定義
        Dim fname1 As String
        Dim fname2 As String

        Dim filepath1 = "" '= ResultIni.path_dancecsv
        Dim filepath2 = "" '= ResultIni.path_dance2csv

        If パス名 <> "" Then
            filepath1 = パス名
            filepath2 = パス名
        End If

        fname1 = "Dance_" & 種目記号 & ".csv"
        fname2 = "Dance2_" & 種目記号 & ".csv"

        Dim fileNo As Integer = FreeFile()              'ファイル番号を取得

        'ファイルの存在チェック
        If Dir(filepath1 & fname1) <> fname1 Then
            MsgBox("ファイル「" & filepath1 & fname1 & "」はありません")
            読み込み = 1 'ファイルの読み込み失敗の返り値
            Exit Function
        End If

        If Dir(filepath2 & fname2) <> fname2 Then
            MsgBox("ファイル「" & filepath2 & fname2 & "」はありません")
            読み込み = 2 'ファイルの読み込み失敗の返り値
            Exit Function
        End If

        '===Dance_x.csvの読み込み
        Dim i, j As Integer
        Dim ReadLine As String
        Dim ReadLine_s()
        ReDim ReadLine_s(列数)

        FileOpen(fileNo, filepath1 & fname1, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)

        i = 1
        Do Until EOF(fileNo)
            ReadLine = LineInput(fileNo)
            ReadLine_s = Split(ReadLine, ",")
            For j = 1 To UBound(ReadLine_s) + 1
                DanceCSV(i, j) = ReadLine_s(j - 1)
            Next j
            i = i + 1
        Loop

        FileClose(fileNo)               'ファイルを閉じる

        '===Dance2_x.csvの読み込み

        FileOpen(fileNo, filepath2 & fname2, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)

        i = 1
        Do Until EOF(fileNo)
            ReadLine = LineInput(fileNo)
            ReadLine_s = Split(ReadLine, ",")
            For j = 1 To UBound(ReadLine_s) + 1
                DanceCSV2(i, j) = ReadLine_s(j - 1)
            Next j
            i = i + 1
        Loop

        FileClose(fileNo)               'ファイルを閉じる

        読み込み = 0

        'PCS倍率のセット


        'If ResultIni.Cal_Type = "J" Then
        '設定_全員倍率 = 5.7     '====2011/01/19 Ver4.0修正　　5⇒5.7
        ' 設定_全員基礎点 = 15    '====2011/01/19 Ver4.0修正　　30⇒15
        'Else
        ' 設定_全員倍率 = 5     '====2011/12/08 Ver6.5追加　　
        '設定_全員基礎点 = 0    '====2011/12/08 Ver6.5追加　　
        'End If


    End Function

    Function ソロPCS詳細_素点(ByVal 選手番号, ByVal PCS番号, ByVal ジャッジ番号)
        '===========================
        '概要　ソロPCSの詳細得点の素点を返す
        '       但し、特別減点が有効な場合は、PCS1は0点として返す。
        '       ブランクの場合も0を返す。
        '入力　選手番号(1～8), PCS番号(1～5), ジャッジ番号(1～13)
        '出力　選手毎、PCS毎、ジャッジ毎の点数（最小単位）
        '===========================


        Dim 行数
        Dim PCSオリジナル As Double

        行数 = (選手番号 - 1) * CompCSV.ジャッジ人数 + ジャッジ番号



        If DanceCSV(行数, 60 + PCS番号) <> "" Then
            PCSオリジナル = DanceCSV(行数, 60 + PCS番号)
        Else  '===ブランクの場合は0を返す。
            PCSオリジナル = 0
        End If



        Return PCSオリジナル
    End Function


    Function ソロ一般減点詳細(ByVal 選手番号, ByVal ジャッジ番号, ByVal 減点項目No)
        '===========================
        '概要　ソロの一般減点の点数の詳細点を返す。
        '入力　選手番号(1～8),ジャッジ番号(1～13),減点項目番号(1～10)
        '出力　一般減点の詳細点数
        '===========================

        Dim 減点

        If DanceCSV(ジャッジ番号 + CompCSV.ジャッジ人数 * (選手番号 - 1), 50 + 減点項目No) = "" Then
            減点 = 0
        Else
            減点 = DanceCSV(ジャッジ番号 + CompCSV.ジャッジ人数 * (選手番号 - 1), 50 + 減点項目No)
        End If

        Return 減点

    End Function

    Function ソロ一般減点詳細_2(ByVal 選手番号, ByVal ジャッジ番号, ByVal 減点項目No)
        '===========================
        '概要　ソロの一般減点の減点対象ジャッジの詳細点（減点対象ジャッジ）を返す。
        '入力　選手番号(1～8),減点対象ジャッジ番号(1～13),減点項目番号(1～10)
        '出力　一般減点の詳細点数
        '===========================

        Dim 減点

        If DanceCSV2(ジャッジ番号 + CompCSV.減点対象ジャッジ人数 * (選手番号 - 1), 50 + 減点項目No) = "" Then
            減点 = 0
        Else
            減点 = CDbl(DanceCSV2(ジャッジ番号 + CompCSV.減点対象ジャッジ人数 * (選手番号 - 1), 50 + 減点項目No))
        End If

        Return 減点

    End Function

End Class
