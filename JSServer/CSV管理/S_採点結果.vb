Imports System.IO

Public Class S_採点結果


    Public リスト(100) As S_採点


    Public 登録済みレコード数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "S_"


    Private 区分番号 As String
    Private ラウンド番号 As String
    Private 種目記号 As String
    Private ジャッジ記号 As String


    ''' コンストラクタ
    ''' 
    Sub New(filepath_)

        登録済みレコード数 = 0

        filepath = filepath_

        'FileRead()


    End Sub



    '' ********  メソッド *************
    ''
    '背番号とPCS番号を渡すと、得点を返す
    Public Function Get_PCS得点(背番号 As String, PCS番号 As Integer) As Double

        Dim rc As Double = 0

        For i = 1 To 登録済みレコード数
            If リスト(i).背番号 = 背番号 Then
                rc = リスト(i).点数(PCS番号)
                i = 登録済みレコード数
            End If
        Next i

        Return rc

    End Function

    '背番号とPCS番号を渡すと、減点を返す
    Public Function Get_PCS減点(背番号 As String, 減点番号 As Integer) As Double

        Dim rc As Double = 0

        For i = 1 To 登録済みレコード数
            If リスト(i).背番号 = 背番号 Then
                rc = リスト(i).減点(減点番号)
                i = 登録済みレコード数
            End If
        Next i

        Return rc

    End Function


    ''' 区分番号とラウンド番号、ジャッジ記号を指定して、採点表を読み込む
    ''' 
    Public Function Read(_区分番号 As String, _ラウンド番号 As String, _種目記号 As String, _ジャッジ記号 As String) As Integer
        '読み込んだあと、登録済みレコード数を返す

        区分番号 = String.Format("{0:D2}", CInt(_区分番号))
        ラウンド番号 = _ラウンド番号
        種目記号 = _種目記号
        ジャッジ記号 = _ジャッジ記号

        Dim rc As Integer = FileRead()

        Return rc

    End Function

    ''' 採点データをCSVに追加する、同じ背番号の時は更新する
    ''' 
    Public Function 登録(データ As S_採点) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & 区分番号 & "_" & ラウンド番号 & "_" & 種目記号 & "_" & ジャッジ記号 & ".csv"

        FileRead()

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("背番号,SEND_FLAG,点数01,点数02,点数03,点数04,点数05,点数06,点数07,点数08,点数09,点数10,減点01,減点02,減点03,減点04,減点05,減点06,減点07,減点08,減点09,減点10,減点11,減点12,減点13,減点14,減点15,減点16,減点17,減点18,減点19,減点20")


            Dim s As Integer
            Dim 登録済みFLAG As Boolean = False

            For s = 1 To 登録済みレコード数

                Dim 元記号, 新記号

                '記号が数値型の場合は、数値に変換して大小比較する。
                '背番号が数値型に変えられる場合は、数値型に変えて比較する
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
            MsgBox("登録エラー:" & ex.ToString)
        End Try

        Return rc

    End Function



    '' 書き込み
    ''



    ''' 読込み
    ''' 
    Private Function FileRead() As Integer

        ReDim リスト(100)
        登録済みレコード数 = 0

        Dim filename As String

        filename = File頭文字列 & 区分番号 & "_" & ラウンド番号 & "_" & 種目記号 & "_" & ジャッジ記号 & ".csv"

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した

            Dim fs As FileStream
            fs = New FileStream(filepath & "\" & filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)

            ' StreamReader の新しいインスタンスを生成する
            'Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)
            Dim cReader As New System.IO.StreamReader(fs, System.Text.Encoding.Default)


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

                End If



            End While


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()

            fs = Nothing

        End If

        Return 登録済みレコード数

    End Function

    ''' 個別の区分データをカンマ区切りの文字列に変換する
    '''
    Private Function カンマ区切り(担当PCS As S_採点)

        Dim line As String = ""

        line = 担当PCS.背番号

        line = line & "," & 担当PCS.SEND_FLAG

        Dim i As Integer

        For i = 1 To 10
            line = line & "," & 担当PCS.点数(i)
        Next i

        For i = 1 To 20
            line = line & "," & 担当PCS.減点(i)
        Next i


        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りの審判員データを、配列に登録する
    '''
    Private Sub Addデータ(データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim No As Integer = 登録済みレコード数 + 1

        If UBound(arBuffer) >= 4 Then

            リスト(No) = New S_採点

            リスト(No).背番号 = arBuffer(0)
            リスト(No).SEND_FLAG = arBuffer(1)

        End If

        Dim k As Integer
        For k = 2 To 11
            If UBound(arBuffer) >= k Then
                リスト(No).点数(k - 1) = arBuffer(k)
            End If
        Next k
        For k = 1 To 20
            If UBound(arBuffer) >= 10 + k Then
                リスト(No).減点(k) = arBuffer(11 + k)
            End If
        Next k


        '登録済み審判員数をカウントアップ
        登録済みレコード数 = 登録済みレコード数 + 1

    End Sub




End Class
