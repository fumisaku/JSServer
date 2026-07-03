Public Class E_ヒート表マスタ

    Public リスト(100) As E_ヒート表

    Public 登録済みレコード数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "H_"

    Private 区分番号 As String
    Private ラウンド番号 As String

    ''' コンストラクタ
    ''' 
    Sub New(filepath_)

        登録済みレコード数 = 0

        filepath = filepath_

        'FileRead()

    End Sub





    '' ********  メソッド *************
    ''

    ''' 区分番号とラウンド番号を指定して、ヒート表を読み込む
    ''' 
    Public Sub Read(_区分番号 As String, _ラウンド番号 As String)

        '区分番号 = _区分番号

        区分番号 = String.Format("{0:D2}", CInt(_区分番号))
        ラウンド番号 = _ラウンド番号

        FileRead()

    End Sub


    ''' 区分データをCSVに追加する、同じ区分番号・ラウンド番号の時は更新する
    ''' 
    Public Function 登録(データ As E_ヒート表, 区分番号 As String, ラウンド番号 As String) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & 区分番号 & "_" & ラウンド番号 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("背番号,種目1,種目2,種目3,種目4,種目5,種目6,種目7,種目8,種目9,種目10")


            Dim s As Integer
            Dim 登録済みFLAG As Boolean = False

            For s = 1 To 登録済みレコード数

                Dim 元記号, 新記号

                '記号が数値型の場合は、数値に変換して大小比較する。
                If IsNumeric(リスト(s).背番号) And IsNumeric(データ.背番号) Then
                    元記号 = CInt(リスト(s).背番号)
                    新記号 = CInt(データ.背番号)
                    'そうではない場合は、文字列のままで比較する
                Else
                    元記号 = リスト(s).背番号
                    新記号 = データ.背番号
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
        Read(区分番号, ラウンド番号)

        Return rc

    End Function

    '' ファイルの存在チェック
    Public Function FileCheck(区分番号 As String, ラウンド番号 As String) As Boolean
        '存在している場合は、True   存在していない場合は、Falseを返す

        Dim filename As String
        Dim rc As Boolean = False

        区分番号 = String.Format("{0:D2}", CInt(区分番号))

        filename = File頭文字列 & 区分番号 & "_" & ラウンド番号 & ".csv"

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない
            rc = False
        Else
            'ファイルが存在した
            rc = True

        End If

        Return rc

    End Function


    Public Function Getヒート数(種目順 As String) As Integer
        '種目順 01～10 を渡すと、その種目のヒート数を返す
        '該当種目順が無いときは 0を返す。


        Dim rc As Integer = 0
        Dim MAXヒート数 As Integer = 0

        For i = 1 To 登録済みレコード数
            If リスト(i).ヒート番号(CInt(種目順)) > MAXヒート数 Then
                MAXヒート数 = リスト(i).ヒート番号(CInt(種目順))
            End If
        Next i

        rc = MAXヒート数

        Return rc

    End Function

    '該当のヒートの背番号リストを配列で返す
    '背番号リストの人数を戻り値で返す。
    Public Function Get_背番号リスト(種目順 As String, ヒート番号 As Integer, ByRef 背番号リスト() As String) As Integer
        'ヒート番号が0の時は、全員を返す


        Dim rc As Integer = 0
        Dim SList_temp(200) As String

        Dim i As Integer

        If ヒート番号 = 0 Then
            '全ヒートの場合
            For i = 1 To 登録済みレコード数
                rc = rc + 1
                SList_temp(rc) = リスト(i).背番号
            Next i
        Else
            'ヒート指定の場合
            For i = 1 To 登録済みレコード数
                If リスト(i).ヒート番号(CInt(種目順)) = ヒート番号 Then
                    rc = rc + 1
                    SList_temp(rc) = リスト(i).背番号
                End If
            Next i

        End If


        If rc > 0 Then
            ReDim 背番号リスト(rc)
            For i = 1 To rc
                背番号リスト(i) = SList_temp(i)
            Next i

        End If

        Return rc

    End Function

    '種目順とヒート番号を渡すと、そのラウンドの何曲目かを返す
    Public Function Get_曲番号(種目順 As String, ヒート番号 As Integer) As Integer

        Dim rc As Integer = 0

        Dim i As Integer
        If CInt(種目順) = 1 Then
            rc = ヒート番号
        Else
            For i = 1 To CInt(種目順) - 1
                rc = rc + Getヒート数(i)
            Next i

            rc = rc + ヒート番号

        End If

        Return rc

    End Function

    '種目順と背番号を渡すと、ヒート番号を返す
    Public Function Get_ヒート番号(種目順 As String, 背番号 As String) As Integer

        Dim rc As Integer = 0

        For i = 1 To 登録済みレコード数
            If リスト(i).背番号 = 背番号 Then
                rc = リスト(i).ヒート番号(種目順)
                i = 登録済みレコード数
            End If
        Next i


        Return rc

    End Function



    '' 書き込み
    ''
    Public Function Deleteレコード(区分番号 As String, ラウンド番号 As String)

        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & 区分番号 & "_" & ラウンド番号 & ".csv"

        Dim rc As Integer = 0


        Try

            If FileCheck(区分番号, ラウンド番号) = True Then
                System.IO.File.Delete(filepath & "\" & filename)
            End If


        Catch ex As Exception
            rc = 1

        End Try


        Return rc

    End Function



    ''' 読込み
    ''' 
    Private Sub FileRead()

        Dim filename As String
        Dim rc As Boolean = False

        filename = File頭文字列 & 区分番号 & "_" & ラウンド番号 & ".csv"

        ReDim リスト(100)
        登録済みレコード数 = 0

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない
            rc = False

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
                If Left(stBuffer, 3) = "背番号" Then

                Else
                    '読み込んだ１行分のデータを配列に登録する
                    Addデータ(stBuffer)
                    '登録済みレコード数 = 登録済みレコード数 + 1
                End If



            End While


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If






    End Sub

    ''' 個別の区分データをカンマ区切りの文字列に変換する
    '''
    Private Function カンマ区切り(ヒート表 As E_ヒート表)

        Dim line As String = ""

        line = line & ヒート表.背番号

        Dim i As Integer

        For i = 1 To 10
            line = line & "," & ヒート表.ヒート番号(i)
        Next i

        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りのヒートデータを、配列に登録する
    '''
    Private Sub Addデータ(データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim No As Integer = 登録済みレコード数 + 1

        If UBound(arBuffer) >= 1 Then

            リスト(No) = New E_ヒート表

            リスト(No).背番号 = arBuffer(0)

            Dim k As Integer
            For k = 1 To 10
                If UBound(arBuffer) >= k Then
                    If IsNumeric(arBuffer(k)) Then
                        リスト(No).ヒート番号(k) = arBuffer(k)
                    Else
                        k = 10
                    End If
                End If
            Next k

        End If


        '登録済み審判員数をカウントアップ
        登録済みレコード数 = 登録済みレコード数 + 1

    End Sub


End Class
