Public Class T_採点進行管理


    Public リスト(200) As T_採点進行

    Public 登録済みレコード数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "MT_ScoreProgress"


    Private 区分番号 As String
    Private ラウンド番号 As String


    ''' コンストラクタ
    ''' 
    Sub New(filepath_)

        登録済みレコード数 = 0

        filepath = filepath_

        FileRead()

    End Sub



    '' ********  メソッド *************
    ''

    '''次の採点種目(リアルタイム）を検索
    Public Function Get_次競技番号(ByVal 頭文字 As String)
        '5桁 3桁競技番号＋2桁枝番を返す
        '頭一桁をINPUT  Aフロア、Bフロアの区別
        '該当が無い場合は、ブランク"" を返す

        Dim rc As String = ""

        For i = 1 To 登録済みレコード数
            If リスト(i).リアルタイムFLAG <> "N" And リスト(i).ステータス = "ヒート表作成済み" Then
                If 頭文字 <> "" Then
                    '頭文字の指定がある場合
                    If Left(リスト(i).競技番号, 1) = 頭文字 Then
                        rc = リスト(i).競技番号 & リスト(i).競技番号枝番
                        i = 登録済みレコード数
                    End If
                Else
                    '頭文字の指定が無い場合
                    rc = リスト(i).競技番号 & リスト(i).競技番号枝番
                    i = 登録済みレコード数
                End If
            End If
        Next i


        Return rc
    End Function

    '競技番号。枝番を渡すと、区分番号、ラウンド番号を返す
    Public Sub Get_区分ラウンド番号(ByVal 競技番号 As String, ByVal 枝番 As String, ByRef 区分番号 As String, ByRef ラウンド番号 As String)

        For i = 1 To 登録済みレコード数
            If リスト(i).競技番号 = 競技番号 And リスト(i).競技番号枝番 = 枝番 Then
                区分番号 = リスト(i).区分番号
                ラウンド番号 = リスト(i).ラウンド番号
                i = 登録済みレコード数
            End If
        Next i

    End Sub

    '区分番号、ラウンド番号を渡すと、競技番号、枝番を返す
    Public Function Get_競技番号_枝番(ByVal 区分番号 As String, ByVal ラウンド番号 As String) As String
        Dim rc As String = ""

        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And リスト(i).ラウンド番号 = ラウンド番号 Then
                rc = リスト(i).競技番号 & リスト(i).競技番号枝番
                i = 登録済みレコード数
            End If
        Next i
        Return rc
    End Function


    '区分番号、ラウンド番号を渡すと T_採点進行クラスを返す
    Public Function Get_採点進行Class(区分番号 As String, ラウンド番号 As String) As T_採点進行

        FileRead()


        Dim rc As T_採点進行 = Nothing

        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And リスト(i).ラウンド番号 = ラウンド番号 Then
                rc = リスト(i)
                i = 登録済みレコード数
            End If
        Next i

        Return rc

    End Function

    '区分番号を渡すと最後に終了したラウンド番号を返す。'000'の時はまだ始まっていない
    Public Function Get_終了ラウンド(区分番号 As String) As String

        Dim rc As String = "000"

        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And リスト(i).ステータス = "採点済み" Then
                rc = リスト(i).ラウンド番号
            End If
        Next i

        Return rc

    End Function




    ''' 採点進行データをCSVに追加する、同じ競技番号の時は更新する
    ''' 
    Public Function 登録(データ As T_採点進行) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("競技番号,枝番,区分番号,ラウンド番号,リアルタイムFLAG,ステータス,開始予定,終了予定,開始実績,終了実績")


            Dim s As Integer
            Dim 登録済みFLAG As Boolean = False

            For s = 1 To 登録済みレコード数

                Dim 元記号, 新記号

                '記号が数値型の場合は、数値に変換して大小比較する。
                If IsNumeric(リスト(s).競技番号 & リスト(s).競技番号枝番) And IsNumeric(データ.競技番号 & データ.競技番号枝番) Then

                    元記号 = CInt(リスト(s).競技番号 & リスト(s).競技番号枝番)
                    新記号 = CInt(データ.競技番号 & データ.競技番号枝番)

                    'そうではない場合は、文字列のままで比較する
                Else
                    元記号 = リスト(s).競技番号 & リスト(s).競技番号枝番
                    新記号 = データ.競技番号 & データ.競技番号枝番
                End If


                If 元記号 = 新記号 Then
                    '同じ区分があった場合は、更新

                    '新しいデータを登録する。
                    sw.WriteLine(カンマ区切り(データ))
                    登録済みFLAG = True


                ElseIf 元記号 < 新記号 Then
                    '区分番号が小さい場合は、単純に登録
                    sw.WriteLine(カンマ区切り（リスト(s)))

                ElseIf 元記号 > 新記号 Then

                    If 登録済みFLAG = False Then
                        '新しいデータを登録する。
                        sw.WriteLine(カンマ区切り(データ))
                        登録済みFLAG = True
                    End If

                    sw.WriteLine(カンマ区切り(リスト(s)))

                End If

            Next s

            '背番号が一番大きい時は一番最後に追加
            If 登録済みFLAG = False Then
                sw.WriteLine(カンマ区切り(データ))
                登録済みFLAG = True
            End If

            '閉じる 
            sw.Close()

        Catch ex As Exception
            rc = 1

        End Try


        '登録が終わったら再度読み込み
        FileRead()

        Return rc

    End Function


    '開始実績時刻の記録
    Public Sub 開始実績時刻登録(区分番号, ラウンド番号)
        '既に登録済みの場合は登録しない

        FileRead()

        Dim nデータ As T_採点進行
        nデータ = New T_採点進行

        Dim 更新FLAG As Boolean = False
        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And リスト(i).ラウンド番号 = ラウンド番号 Then
                If リスト(i).開始実績.TimeOfDay.ToString = "00:00:00" Then
                    更新FLAG = True

                    nデータ.競技番号 = リスト(i).競技番号
                    nデータ.競技番号枝番 = リスト(i).競技番号枝番
                    nデータ.区分番号 = リスト(i).区分番号
                    nデータ.ラウンド番号 = リスト(i).ラウンド番号
                    nデータ.ステータス = リスト(i).ステータス
                    nデータ.リアルタイムFLAG = リスト(i).リアルタイムFLAG

                    nデータ.開始予定 = リスト(i).開始予定
                    nデータ.開始実績 = Now
                    nデータ.終了予定 = リスト(i).終了予定
                    nデータ.終了実績 = リスト(i).終了実績

                End If
                i = 登録済みレコード数
            End If
        Next i

        If 更新FLAG = True Then
            Dim rc As Integer = 登録(nデータ)
        End If
    End Sub

    '開始実績時刻の記録
    Public Sub 終了実績時刻登録(区分番号, ラウンド番号)
        '既に登録済みの場合は登録しない

        FileRead()

        Dim nデータ As T_採点進行
        nデータ = New T_採点進行

        Dim 更新FLAG As Boolean = False
        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And リスト(i).ラウンド番号 = ラウンド番号 Then
                If リスト(i).終了実績.TimeOfDay.ToString = "00:00:00" Then
                    更新FLAG = True

                    nデータ.競技番号 = リスト(i).競技番号
                    nデータ.競技番号枝番 = リスト(i).競技番号枝番
                    nデータ.区分番号 = リスト(i).区分番号
                    nデータ.ラウンド番号 = リスト(i).ラウンド番号
                    nデータ.ステータス = リスト(i).ステータス
                    nデータ.リアルタイムFLAG = リスト(i).リアルタイムFLAG

                    nデータ.開始予定 = リスト(i).開始予定
                    nデータ.開始実績 = リスト(i).開始実績
                    nデータ.終了予定 = リスト(i).終了予定
                    nデータ.終了実績 = Now

                End If
                i = 登録済みレコード数
            End If
        Next i

        If 更新FLAG = True Then
            Dim rc As Integer = 登録(nデータ)
        End If
    End Sub


    '' 書き込み
    ''
    Public Function Deleteレコード()
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("競技番号,枝番,区分番号,ラウンド番号,リアルタイムFLAG,ステータス,開始予定,終了予定,開始実績,終了実績")


            '閉じる 
            sw.Close()

        Catch ex As Exception
            rc = 1

        End Try


        '登録が終わったら再度読み込み
        FileRead()

        Return rc

    End Function


    ''' 読込み
    ''' 
    Public Sub FileRead()

        ReDim リスト(200)
        登録済みレコード数 = 0

        Dim filename As String

        filename = File頭文字列 & ".csv"

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した

            ' StreamReader の新しいインスタンスを生成する
            Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)

            ' 読み込んだ結果をすべて格納するための変数を宣言する
            Dim stResult As String = String.Empty


            ' 読み込みできる文字がなくなるまで繰り返す

            While (cReader.Peek() >= 0)
                ' ファイルを 1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()

                'ファイルの１行目はヘッダーなので読み込まない
                If Left(stBuffer, 4) = "競技番号" Then

                Else
                    '読み込んだ１行分のデータを配列に登録する
                    Addデータ(stBuffer)

                End If



            End While


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If






    End Sub

    ''' 個別の区分データをカンマ区切りの文字列に変換する
    '''
    Private Function カンマ区切り(採点進行 As T_採点進行)

        Dim line As String = ""

        line = line & 採点進行.競技番号

        line = line & "," & 採点進行.競技番号枝番
        line = line & "," & 採点進行.区分番号
        line = line & "," & 採点進行.ラウンド番号
        line = line & "," & 採点進行.リアルタイムFLAG
        line = line & "," & 採点進行.ステータス

        line = line & "," & 採点進行.開始予定
        line = line & "," & 採点進行.終了予定
        line = line & "," & 採点進行.開始実績
        line = line & "," & 採点進行.終了実績

        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りのデータを、配列に登録する
    '''
    Private Sub Addデータ(データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim No As Integer = 登録済みレコード数 + 1

        If UBound(arBuffer) >= 4 Then

            リスト(No) = New T_採点進行

            リスト(No).競技番号 = arBuffer(0)
            リスト(No).競技番号枝番 = arBuffer(1)
            リスト(No).区分番号 = arBuffer(2)
            リスト(No).ラウンド番号 = arBuffer(3)
            リスト(No).リアルタイムFLAG = arBuffer(4)
            リスト(No).ステータス = arBuffer(5)

            If UBound(arBuffer) >= 8 Then
                リスト(No).開始予定 = arBuffer(6)
                リスト(No).終了予定 = arBuffer(7)
                リスト(No).開始実績 = arBuffer(8)
                リスト(No).終了実績 = arBuffer(9)
            End If
        End If


            '登録済み審判員数をカウントアップ
            登録済みレコード数 = 登録済みレコード数 + 1

    End Sub

End Class
