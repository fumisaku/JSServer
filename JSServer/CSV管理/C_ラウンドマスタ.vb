Public Class C_ラウンドマスタ


    Public リスト(500) As C_ラウンド

    Public 登録済みレコード数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "MC_Round"

    ''' コンストラクタ
    ''' 
    Sub New(filepath_)

        登録済みレコード数 = 0

        filepath = filepath_

        FileRead()

    End Sub



    '' ********  メソッド *************
    ''

    ''' 区分データをCSVに追加する、同じ区分番号・ラウンド番号の時は更新する
    ''' 
    Public Function 登録(データ As C_ラウンド) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("区分番号,ラウンド番号,採点方式,担当審判グループ,ヒート数,UP予定数,CaliMAX,CaliMin,リアルタイムFlag,ヒート割方式")


            Dim s As Integer
            Dim 登録済みFLAG As Boolean = False

            For s = 1 To 登録済みレコード数

                Dim 元記号, 新記号

                '記号が数値型の場合は、数値に変換して大小比較する。
                If IsNumeric(リスト(s).区分番号 & リスト(s).ラウンド番号) And IsNumeric(データ.区分番号 & データ.ラウンド番号) Then
                    元記号 = CInt(リスト(s).区分番号 & リスト(s).ラウンド番号)
                    新記号 = CInt(データ.区分番号 & データ.ラウンド番号)
                    'そうではない場合は、文字列のままで比較する
                Else
                    元記号 = リスト(s).区分番号 & リスト(s).ラウンド番号
                    新記号 = データ.区分番号 & データ.ラウンド番号
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


    '========Publicメソッド=========

    Public Function GetラウンドClass(区分番号 As String, ラウンド番号 As String) As C_ラウンド
        '区分番号とラウンド番号を渡すとラウンドクラスを返す
        Dim rc As C_ラウンド = Nothing
        Dim i As Integer

        For i = 1 To 登録済みレコード数

            If リスト(i).区分番号 = 区分番号 And リスト(i).ラウンド番号 = ラウンド番号 Then
                rc = リスト(i)
                i = UBound(リスト)
            End If

        Next i

        Return rc

    End Function

    Public Function 出場組数(区分番号 As String, ラウンド番号 As String) As Integer
        Dim rc As Integer = 0

        'ヒート表があれば、ヒート表の人数を返す
        'ヒート表が作成されているか確認
        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        If マスタデータ.E_ヒート表マスタ.FileCheck(区分番号, ラウンド番号) = True Then

            'ヒート表ファイルの読込み
            マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
            rc = マスタデータ.E_ヒート表マスタ.登録済みレコード数

        Else
            'ヒート表が無い場合は、前のラウンドのUP数を表示する

            '　　前のラウンドを探す
            For i = 登録済みレコード数 To 1 Step -1
                If 区分番号 = リスト(i).区分番号 And
                   リスト(i).ラウンド番号 < ラウンド番号 And
                   Strings.Right(リスト(i).ラウンド番号, 1) <> "T" Then

                    If Strings.Left(リスト(i).ラウンド番号, 2) = "01" Then

                        rc = rc + リスト(i).UP予定数   'リダンスと１次予選を合算

                        'あと、シード選手数も足さないといけない

                    Else
                        rc = リスト(i).UP予定数
                        i = 1
                    End If

                End If
            Next i

        End If

        'マスタデータ = Nothing


        Return rc
    End Function


    Public Function Get採点方式(区分番号 As String, ラウンド番号 As String) As String
        '区分番号とラウンド番号を渡すと、採点方式を返す
        Dim rc As String = ""
        Dim i As Integer

        For i = 1 To 登録済みレコード数

            If リスト(i).区分番号 = 区分番号 And リスト(i).ラウンド番号 = ラウンド番号 Then
                rc = リスト(i).採点方式
                i = UBound(リスト)
            End If

        Next i

        Return rc

    End Function

    Public Function Get担当審判グループ(区分番号 As String, ラウンド番号 As String) As Integer
        '区分番号とラウンド番号を渡すと、担当審判グループを返す
        Dim rc As Integer = 0
        Dim i As Integer

        For i = 1 To 登録済みレコード数

            If リスト(i).区分番号 = 区分番号 And リスト(i).ラウンド番号 = ラウンド番号 Then
                rc = リスト(i).担当審判グループ
                i = UBound(リスト)
            End If

        Next i

        Return rc

    End Function

    '区分番号、ラウンド番号を渡すと、次のラウンド番号（同点決勝を除く）を返す
    Public Function Get_次ラウンドClass(区分番号 As String, 現ラウンド番号 As String) As C_ラウンド

        Dim rc As C_ラウンド = Nothing

        '上から順番に探す
        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And
                Strings.Right(リスト(i).ラウンド番号, 1) = "0" And
                リスト(i).ラウンド番号 > 現ラウンド番号 Then

                rc = リスト(i)
                i = 登録済みレコード数
            End If

        Next i

        Return rc

    End Function

    '区分番号、ラウンド番号を渡すと、前のラウンド番号（同点決勝を除く）を返す
    Public Function Get_前ラウンドClass(区分番号 As String, 現ラウンド番号 As String) As C_ラウンド

        Dim rc As C_ラウンド = Nothing

        Dim 開始番号 As Integer = 0
        '上から順番に現ラウンド番号探す
        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And
                リスト(i).ラウンド番号 = 現ラウンド番号 Then

                開始番号 = i

                i = 登録済みレコード数
            End If
        Next i

        If 開始番号 = 0 Then
            Return rc  'Nothingを返す
            Exit Function
        End If

        '開始番号から下に探す
        For i = 開始番号 - 1 To 1 Step -1
            If リスト(i).区分番号 = 区分番号 And
                Strings.Right(リスト(i).ラウンド番号, 1) = "0" Then

                rc = リスト(i)
                i = 0
            End If
        Next i



        Return rc

    End Function




    '' 書き込み
    ''
    'Keyで指定したレコードを削除する
    Public Function Deleteレコード(区分番号 As String)


        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("区分番号,ラウンド番号,採点方式,担当審判グループ,ヒート数,UP予定数,CaliMAX,CaliMin,リアルタイムFlag,ヒート割方式")


            For s = 1 To 登録済みレコード数

                If リスト(s).区分番号 = 区分番号 Then
                    'Keyに一致している時は書き込まない = 削除

                Else
                    sw.WriteLine(カンマ区切り(リスト(s)))

                End If

            Next s


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

        ReDim リスト(500)
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
                If Left(stBuffer, 4) = "区分番号" Then

                Else
                    '読み込んだ１行分のデータを配列に登録する
                    Addデータ(stBuffer)
                    ' 登録済みレコード数 = 登録済みレコード数 + 1
                End If



            End While


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If






    End Sub

    ''' 個別の区分データをカンマ区切りの文字列に変換する
    '''
    Private Function カンマ区切り(ラウンド As C_ラウンド)

        Dim line As String = ""

        line = line & ラウンド.区分番号

        line = line & "," & ラウンド.ラウンド番号
        line = line & "," & ラウンド.採点方式
        line = line & "," & ラウンド.担当審判グループ.ToString
        line = line & "," & ラウンド.ヒート数.ToString
        line = line & "," & ラウンド.UP予定数.ToString
        line = line & "," & ラウンド.CaliMax.ToString
        line = line & "," & ラウンド.CaliMin.ToString
        line = line & "," & ラウンド.リアルタイムFLAG
        line = line & "," & ラウンド.ヒート割方式

        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りの審判員データを、配列に登録する
    '''
    Private Sub Addデータ(データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim No As Integer = 登録済みレコード数 + 1

        If UBound(arBuffer) >= 7 Then

            リスト(No) = New C_ラウンド

            リスト(No).区分番号 = arBuffer(0)
            リスト(No).ラウンド番号 = arBuffer(1)
            リスト(No).採点方式 = arBuffer(2)
            リスト(No).担当審判グループ = arBuffer(3)
            リスト(No).ヒート数 = arBuffer(4)
            リスト(No).UP予定数 = arBuffer(5)
            リスト(No).CaliMax = arBuffer(6)
            リスト(No).CaliMin = arBuffer(7)
            リスト(No).リアルタイムFLAG = arBuffer(8)
            リスト(No).ヒート割方式 = arBuffer(9)

        End If


        '登録済み審判員数をカウントアップ
        登録済みレコード数 = 登録済みレコード数 + 1

    End Sub




End Class
