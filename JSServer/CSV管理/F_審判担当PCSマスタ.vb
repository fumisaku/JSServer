Public Class F_審判担当PCSマスタ

    Public リスト(100) As F_審判担当PCS

    Public 登録済みレコード数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "P_"


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

    ''' 区分番号とラウンド番号を指定して、担当PCS表を読み込む
    ''' 
    Public Sub Read(_区分番号 As String, _ラウンド番号 As String)

        登録済みレコード数 = 0

        '区分番号 = _区分番号
        区分番号 = String.Format("{0:D2}", CInt(_区分番号))
        ラウンド番号 = _ラウンド番号

        FileRead()

    End Sub

    ''' 区分データをCSVに追加する、同じ区分番号・ラウンド番号の時は更新する
    ''' 
    Public Function 登録(データ As F_審判担当PCS) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & 区分番号 & "_" & ラウンド番号 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("ジャッジ記号,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20")


            Dim s As Integer
            Dim 登録済みFLAG As Boolean = False

            For s = 1 To 登録済みレコード数

                Dim 元記号 = "", 新記号 = ""

                '記号が数値型の場合は、数値に変換して大小比較する。
                If IsNumeric(リスト(s).ジャッジ記号) And IsNumeric(データ.ジャッジ記号) Then
                    元記号 = CInt(リスト(s).ジャッジ記号)
                    新記号 = CInt(データ.ジャッジ記号)

                    'そうではない場合は、文字列のままで比較する
                Else
                    元記号 = リスト(s).ジャッジ記号
                    新記号 = データ.ジャッジ記号
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



    Public Function Get_審判担当PCSClass(ジャッジ記号 As String) As F_審判担当PCS
        'Keyを渡すとクラスを返す

        Dim i As Integer
        Dim rc As F_審判担当PCS = Nothing

        For i = 1 To 登録済みレコード数
            If リスト(i).ジャッジ記号 = ジャッジ記号 Then

                rc = リスト(i)
                i = 登録済みレコード数
            End If
        Next i

        Return rc

    End Function

    '' ファイルの存在チェック
    Public Function FileCheck(区分番号 As String, ラウンド番号 As String) As Boolean
        '存在している場合は、True   存在していない場合は、Falseを返す

        Dim rc As Boolean = False

        区分番号 = String.Format("{0:D2}", CInt(区分番号))

        Dim filename As String = File頭文字列 & 区分番号 & "_" & ラウンド番号 & ".csv"

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない
            rc = False
        Else
            'ファイルが存在した
            rc = True

        End If

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

        ReDim リスト(100)
        登録済みレコード数 = 0

        Dim filename As String = File頭文字列 & 区分番号 & "_" & ラウンド番号 & ".csv"

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
    Private Function カンマ区切り(担当PCS As F_審判担当PCS)

        Dim line As String = ""

        line = line & 担当PCS.ジャッジ記号

        Dim i As Integer

        For i = 1 To 99
            line = line & "," & 担当PCS.担当PCS番号(i)
        Next i



        'line = line & "," & 担当PCS.種目記号
        'line = line & "," & 担当PCS.ヒート番号.ToString
        'line = line & "," & 担当PCS.担当PCS番号


        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りの審判員データを、配列に登録する
    '''
    Private Sub Addデータ(データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim No As Integer = 登録済みレコード数 + 1

        If UBound(arBuffer) >= 1 Then

            リスト(No) = New F_審判担当PCS

            リスト(No).ジャッジ記号 = arBuffer(0)


            'リスト(No).種目記号 = arBuffer(1)
            'リスト(No).ヒート番号 = arBuffer(2)
            'リスト(No).担当PCS番号 = arBuffer(3)

        End If

        Dim k As Integer
        For k = 1 To 99
            If UBound(arBuffer) >= k Then
                リスト(No).担当PCS番号(k) = arBuffer(k)
            End If
        Next k


        '登録済み審判員数をカウントアップ
        登録済みレコード数 = 登録済みレコード数 + 1

    End Sub



End Class
