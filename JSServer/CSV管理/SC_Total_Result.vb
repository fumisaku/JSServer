Public Class SC_Total_Result

    Public 出場組数 As Integer
    Public 種目記号(10) As String

    Public 順位表記() As Integer
    Public 背番号() As String
    Public リーダ名() As String
    Public パートナー名() As String
    Public 所属() As String
    Public TotalPoints() As String
    Public 種目点数_01() As String
    Public 種目点数_02() As String
    Public 種目点数_03() As String
    Public 種目点数_04() As String
    Public 種目点数_05() As String

    Private ReadOnly filepath As String

    Const File文字列 = "_Total_Result.csv"

    Public 登録済みレコード数 As Integer

    ''' コンストラクタ
    ''' 

    Sub New(filepath_ As String)

        登録済みレコード数 = 0

        filepath = filepath_

        'FileRead()


    End Sub

    Public Function 読み込み(区分番号 As String, ラウンド番号 As String)

        Dim rc = FileRead(区分番号, ラウンド番号)

        Return rc

    End Function

    ''' 読込み
    ''' 
    Private Function FileRead(区分番号 As String, ラウンド番号 As String)

        登録済みレコード数 = 0

        Dim filename As String

        filename = "SC_" & 区分番号 & "_" & ラウンド番号 & File文字列

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

                登録済みレコード数 = 登録済みレコード数 + 1

                'ファイルの１行目はヘッダーなので読み込まない
                If 登録済みレコード数 = 1 Then

                ElseIf 登録済みレコード数 = 2 Then
                    '2行目はヘッダー
                    Dim arBuffer = stBuffer.Split(",")

                    出場組数 = arBuffer(1)

                    For b = 2 To UBound(arBuffer) - 1
                        種目記号(b - 1) = arBuffer(b)
                    Next b

                    ReDim 順位表記(出場組数)
                    ReDim 背番号(出場組数)
                    ReDim リーダ名(出場組数)
                    ReDim パートナー名(出場組数)
                    ReDim 所属(出場組数)
                    ReDim TotalPoints(出場組数)
                    ReDim 種目点数_01(出場組数)
                    ReDim 種目点数_02(出場組数)
                    ReDim 種目点数_03(出場組数)
                    ReDim 種目点数_04(出場組数)
                    ReDim 種目点数_05(出場組数)



                ElseIf 登録済みレコード数 >= 2 Then

                    Dim arBuffer = stBuffer.Split(",")

                    順位表記(登録済みレコード数 - 2) = CInt(arBuffer(1))
                    背番号(登録済みレコード数 - 2) = arBuffer(2)
                    リーダ名(登録済みレコード数 - 2) = arBuffer(3)
                    パートナー名(登録済みレコード数 - 2) = arBuffer(4)
                    所属(登録済みレコード数 - 2) = arBuffer(5)
                    TotalPoints(登録済みレコード数 - 2) = CStr(Format(CDbl(arBuffer(6)), "0.000"))
                    種目点数_01(登録済みレコード数 - 2) = arBuffer(7)
                    種目点数_02(登録済みレコード数 - 2) = arBuffer(8)
                    種目点数_03(登録済みレコード数 - 2) = arBuffer(9)
                    種目点数_04(登録済みレコード数 - 2) = arBuffer(10)
                    種目点数_05(登録済みレコード数 - 2) = arBuffer(11)

                End If




            End While


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If


        Return 登録済みレコード数



    End Function





End Class
