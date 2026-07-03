Public Class 審判員マスタ

    Public 審判員リスト(100) As 審判

    Public 登録済み審判員数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "M_Judge"

    ''' コンストラクタ
    ''' 
    Sub New(filepath_)

        登録済み審判員数 = 0

        filepath = filepath_

        FileRead()

    End Sub


    '' ********  メソッド *************
    ''

    ''' 審判データをCSVに追加する、同じ審判記号の時は更新する
    ''' 
    Public Function 審判員登録(審判員データ As 審判) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("ジャッジ記号,ジャッジ氏名,ジャッジ会員番号,フリガナ,ジャッジ表記名,所属,言語,審判員チーム01,02,03,04,05")


            Dim s As Integer
            Dim 登録済みFLAG As Boolean = False

            For s = 1 To 登録済み審判員数

                Dim 元ジャッジ記号, 新ジャッジ記号

                '背番号が数値型の場合は、数値に変換して大小比較する。
                If IsNumeric(審判員リスト(s).ジャッジ記号) And IsNumeric(審判員データ.ジャッジ記号) Then
                    元ジャッジ記号 = CInt(審判員リスト(s).ジャッジ記号)
                    新ジャッジ記号 = CInt(審判員データ.ジャッジ記号)
                    'そうではない場合は、文字列のままで比較する
                Else
                    元ジャッジ記号 = 審判員リスト(s).ジャッジ記号
                    新ジャッジ記号 = 審判員データ.ジャッジ記号
                End If


                If 元ジャッジ記号 = 新ジャッジ記号 Then
                    '同じ背番号があった場合は、更新

                    '新しいデータを登録する。
                    sw.WriteLine(審判員カンマ区切り(審判員データ))
                    登録済みFLAG = True


                ElseIf 元ジャッジ記号 < 新ジャッジ記号 Then
                    '背番号が小さい場合は、単純に登録
                    sw.WriteLine(審判員カンマ区切り(審判員リスト(s)))

                ElseIf 元ジャッジ記号 > 新ジャッジ記号 Then

                    If 登録済みFLAG = False Then
                        '新しいデータを登録する。
                        sw.WriteLine(審判員カンマ区切り(審判員データ))
                        登録済みFLAG = True
                    End If

                    sw.WriteLine(審判員カンマ区切り(審判員リスト(s)))

                End If

            Next s

            '背番号が一番大きい時は一番最後に追加
            If 登録済みFLAG = False Then
                sw.WriteLine(審判員カンマ区切り(審判員データ))
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


    Public Function Get_ジャッジ表記名(ジャッジ記号 As String) As String

        Dim i As Integer
        Dim rc As String = ""

        For i = 1 To 登録済み審判員数
            If 審判員リスト(i).ジャッジ記号 = ジャッジ記号 Then
                rc = 審判員リスト(i).ジャッジ表記名
                i = 登録済み審判員数
            End If
        Next i

        Return rc

    End Function

    'Keyを渡すとジャッジクラスを返す
    Public Function Get_審判Class(ジャッジ記号 As String) As 審判

        Dim i As Integer
        Dim rc As 審判 = Nothing

        For i = 1 To 登録済み審判員数
            If 審判員リスト(i).ジャッジ記号 = ジャッジ記号 Then
                rc = 審判員リスト(i)
                i = 登録済み審判員数
            End If
        Next i

        Return rc

    End Function


    'Keyを渡すとレコード番号を返す
    Public Function Get_レコード番号(ジャッジ記号 As String) As Integer

        Dim i As Integer
        Dim rc As Integer = 0

        For i = 1 To 登録済み審判員数
            If 審判員リスト(i).ジャッジ記号 = ジャッジ記号 Then
                rc = i
                i = 登録済み審判員数
            End If
        Next i

        Return rc

    End Function

    '登録済み審判員数を返す
    Public Function Get_登録済み審判員数()

        Dim rc As Integer = 登録済み審判員数

        Return rc


    End Function

    '審判員人数を返す
    Public Function Get_審判員数(審判G As Integer) As Integer

        Dim rc As Integer = 0

        For i = 1 To 登録済み審判員数
            If 審判員リスト(i).審判チーム(審判G) = "1" Or 審判員リスト(i).審判チーム(審判G) = "L" Then
                rc = rc + 1
            End If
        Next i

        Return rc


    End Function

    '審判員記号リストを返す。（１，L,R 全て含む）
    'rcで返すのは、該当人数

    Public Function Get_審判員記号(審判G As Integer, ByRef 審判記号リスト() As String)

        Dim rc As Integer = 0

        For j = 1 To 登録済み審判員数
            If 審判員リスト(j).審判チーム(審判G) <> "0" And 審判員リスト(j).審判チーム(審判G) <> "" Then
                rc = rc + 1

                ReDim Preserve 審判記号リスト(rc)
                審判記号リスト(rc) = 審判員リスト(j).ジャッジ記号

            End If
        Next j

        Return rc

    End Function





    '' 書き込み
    ''
    Public Function Deleteレコード() As Integer

        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("ジャッジ記号,ジャッジ氏名,ジャッジ会員番号,フリガナ,ジャッジ表記名,所属,言語,審判員チーム01,02,03,04,05")


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

        ReDim 審判員リスト(100)

        登録済み審判員数 = 0


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
                If Left(stBuffer, 4) = "ジャッジ" Then

                Else
                    '読み込んだ１行分のデータを配列に登録する
                    Add審判員データ(stBuffer)
                    登録済み審判員数 = 登録済み審判員数 + 1
                End If



            End While


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If






    End Sub

    ''' 個別の審判員データをカンマ区切りの文字列に変換する
    '''
    Private Function 審判員カンマ区切り(審判 As 審判)

        Dim line As String = ""

        line = line & 審判.ジャッジ記号

        line = line & "," & 審判.ジャッジ氏名
        line = line & "," & 審判.ジャッジ会員番号
        line = line & "," & 審判.ジャッジフリガナ
        line = line & "," & 審判.ジャッジ表記名
        line = line & "," & 審判.ジャッジ所属
        line = line & "," & 審判.言語

        Dim i As Integer

        For i = 1 To 99
            line = line & "," & 審判.審判チーム(i)
        Next i

        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りの審判員データを、配列に登録する
    '''
    Private Sub Add審判員データ(データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim 審判員番号 As Integer = 登録済み審判員数 + 1

        If UBound(arBuffer) >= 7 Then

            審判員リスト(審判員番号) = New 審判

            審判員リスト(審判員番号).ジャッジ記号 = arBuffer(0)
            審判員リスト(審判員番号).ジャッジ氏名 = arBuffer(1)
            審判員リスト(審判員番号).ジャッジ会員番号 = arBuffer(2)
            審判員リスト(審判員番号).ジャッジフリガナ = arBuffer(3)
            審判員リスト(審判員番号).ジャッジ表記名 = arBuffer(4)
            審判員リスト(審判員番号).ジャッジ所属 = arBuffer(5)
            審判員リスト(審判員番号).言語 = arBuffer(6)

        End If

        Dim k As Integer
        For k = 1 To 99
            If UBound(arBuffer) > 6 + k Then
                審判員リスト(審判員番号).審判チーム(k) = arBuffer(6 + k)
            End If
        Next k


        '登録済み審判員数をカウントアップ
        '登録済み審判員数 = 登録済み審判員数 + 1

    End Sub

End Class
