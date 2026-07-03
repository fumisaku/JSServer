Public Class 選手マスタ

    Public 選手リスト(3000) As 選手

    Public 登録済み選手数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "M_Member_"

    ''' コンストラクタ
    ''' 
    Sub New(filepath_)

        登録済み選手数 = 0

        filepath = filepath_

        FileRead()

    End Sub

    '' ********  メソッド *************
    ''

    ''' 背番号を渡すと選手Classを返す
    Public Function Get選手C_by背番号(List番号 As String, 背番号 As String) As 選手

        Dim rc As 選手 = Nothing

        For i = 1 To 登録済み選手数
            If 選手リスト(i).List番号 = List番号 And 選手リスト(i).背番号 = 背番号 Then

                rc = 選手リスト(i)
                i = 登録済み選手数
            End If

        Next i

        Return rc

    End Function

    'リスト番号の数と一覧を返す
    Public Function Get_リスト番号(ByRef リスト番号リスト() As String)
        Dim rc As Integer = 0

        Dim Temp(99) As String

        For i = 1 To 登録済み選手数

            Dim FindFlag As Boolean = False
            For t = 1 To rc
                If 選手リスト(i).List番号.PadLeft(2, "0") = Temp(t).PadLeft(2, "0") Then
                    FindFlag = True
                    t = rc
                End If
            Next t

            If FindFlag = False Then
                'Tempリストに追加
                Temp(rc + 1) = 選手リスト(i).List番号.PadLeft(2, "0")
                rc = rc + 1
            End If

        Next i

        '返す配列を作成
        ReDim リスト番号リスト(rc)

        For r = 1 To rc
            リスト番号リスト(r) = Temp(r)
        Next r

        Array.Sort(リスト番号リスト)

        Return rc

    End Function

    '区分番号を渡すと、出場選手数を返す
    Public Function Get_出場選手数(ByVal 区分番号 As String) As Integer

        Dim rc As Integer = 0

        Dim マスタデータ = New マスタデータ

        Dim 区分リスト番号 As Integer = 0

        For k = 1 To マスタデータ.B_区分マスタ.登録済みレコード数
            If マスタデータ.B_区分マスタ.リスト(k).区分番号 = 区分番号 Then

                区分リスト番号 = k
                k = マスタデータ.B_区分マスタ.登録済みレコード数
            End If
        Next k

        Dim 選手M番号 As String = マスタデータ.B_区分マスタ.リスト(区分リスト番号).使用する選手マスタ

        For s = 1 To 登録済み選手数
            If 選手リスト(s).List番号 = 選手M番号 And
               Trim(選手リスト(s).エントリー区分(区分リスト番号)) <> "" Then
                rc = rc + 1
            End If
        Next s


        マスタデータ = Nothing

        Return rc


    End Function



    ''' 選手データをCSVに追加する、同じ背番号の時は更新する
    ''' 
    Public Function 選手登録(リスト番号 As Integer, 選手データ As 選手) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & Format(リスト番号, "00") & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("背番号,リーダー会員番号,リーダー氏名,リーダーフリガナ,リーダー表記名,リーダーサークルコード,リーダー所属名,パートナ会員番号,パートナ氏名,パートナフリガナ,パートナ表記名,パートナサークルコード,パートナ所属名,カップル所属名,区分毎のエントリ01,2,3,4,5,6")


            Dim s As Integer
            Dim 登録済みFLAG As Boolean = False

            For s = 1 To 登録済み選手数
                If 選手リスト(s).List番号.PadLeft(2, "0") = CStr(リスト番号).PadLeft(2, "0") Then

                    Dim 元背番号, 新背番号

                    '背番号が数値型の場合は、数値に変換して大小比較する。
                    If IsNumeric(選手リスト(s).背番号) And IsNumeric(選手データ.背番号) Then
                        元背番号 = CInt(選手リスト(s).背番号)
                        新背番号 = CInt(選手データ.背番号)
                        'そうではない場合は、文字列のままで比較する
                    Else
                        元背番号 = 選手リスト(s).背番号
                        新背番号 = 選手データ.背番号
                    End If


                    If 元背番号 = 新背番号 Then
                        '同じ背番号があった場合は、更新

                        '新しいデータを登録する。
                        sw.WriteLine(選手カンマ区切り(選手データ))
                        登録済みFLAG = True


                    ElseIf 元背番号 < 新背番号 Then
                        '背番号が小さい場合は、単純に登録
                        sw.WriteLine(選手カンマ区切り(選手リスト(s)))

                    ElseIf 元背番号 > 新背番号 Then

                        If 登録済みFLAG = False Then
                            '新しいデータを登録する。
                            sw.WriteLine(選手カンマ区切り(選手データ))
                            登録済みFLAG = True
                        End If

                        sw.WriteLine(選手カンマ区切り(選手リスト(s)))

                    End If
                End If
            Next s

            '背番号が一番大きい時は一番最後に追加
            If 登録済みFLAG = False Then
                sw.WriteLine(選手カンマ区切り(選手データ))
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



    '' 書き込み
    '' 'Keyで指定したレコードを削除する
    Public Function Deleteレコード(リスト番号 As String)
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & Format(リスト番号, "00") & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("背番号,リーダー会員番号,リーダー氏名,リーダーフリガナ,リーダー表記名,リーダーサークルコード,リーダー所属名,パートナ会員番号,パートナ氏名,パートナフリガナ,パートナ表記名,パートナサークルコード,パートナ所属名,カップル所属名,区分毎のエントリ01,2,3,4,5,6")


            Dim s As Integer
            Dim 登録済みFLAG As Boolean = False

            For s = 1 To 登録済み選手数
                If 選手リスト(s).List番号.PadLeft(2, "0") = リスト番号.PadLeft(2, "0") Then
                    '指定されたLIST番号の選手はファイルに書かない　= 削除

                Else
                    sw.WriteLine(選手カンマ区切り(選手リスト(s)))
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

        ReDim 選手リスト(3000)

        登録済み選手数 = 0


        Dim f As Integer
        Dim filename As String

        '選手マスタファイルは最大９９個
        For f = 1 To 99
            filename = File頭文字列 & Format(f, "00") & ".csv"

            If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
                'ファイルが存在しない

            Else
                'ファイルが存在した


                ' StreamReader の新しいインスタンスを生成する
                Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)

                ' 読み込んだ結果をすべて格納するための変数を宣言する
                Dim stResult As String = String.Empty




                Dim リスト番号 As String
                リスト番号 = Mid(filename, 10, 2)  'ファイル名から取得

                ' 読み込みできる文字がなくなるまで繰り返す
                While (cReader.Peek() >= 0)
                    ' ファイルを 1 行ずつ読み込む
                    Dim stBuffer As String = cReader.ReadLine()

                    'ファイルの１行目はヘッダーなので読み込まない
                    If Left(stBuffer, 3) = "背番号" Then

                    Else
                        '読み込んだ１行分のデータを配列に登録する
                        Add選手データ(リスト番号, stBuffer)

                    End If


                End While

                ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
                cReader.Close()

            End If

        Next f




    End Sub

    ''' 個別の選手データをカンマ区切りの文字列に変換する
    '''
    Private Function 選手カンマ区切り(選手 As 選手)

        Dim line As String = ""

        line = line & 選手.背番号

        line = line & "," & 選手.リーダー会員番号
        line = line & "," & 選手.リーダー氏名
        line = line & "," & 選手.リーダーフリガナ
        line = line & "," & 選手.リーダー表記名
        line = line & "," & 選手.リーダーサークルコード
        line = line & "," & 選手.リーダー所属名

        line = line & "," & 選手.パートナ会員番号
        line = line & "," & 選手.パートナ氏名
        line = line & "," & 選手.パートナフリガナ
        line = line & "," & 選手.パートナ表記名
        line = line & "," & 選手.パートナサークルコード
        line = line & "," & 選手.パートナ所属名

        line = line & "," & 選手.カップル所属名

        Dim i As Integer

        For i = 1 To 99
            line = line & "," & 選手.エントリー区分(i)
        Next i

        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りの選手データを、配列に登録する
    '''
    Private Sub Add選手データ(リスト番号 As String, データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim 選手番号 As Integer = 登録済み選手数 + 1

        If UBound(arBuffer) >= 12 Then

            選手リスト(選手番号) = New 選手

            選手リスト(選手番号).List番号 = リスト番号

            選手リスト(選手番号).背番号 = arBuffer(0)

            選手リスト(選手番号).リーダー会員番号 = arBuffer(1)
            選手リスト(選手番号).リーダー氏名 = arBuffer(2)
            選手リスト(選手番号).リーダーフリガナ = arBuffer(3)
            選手リスト(選手番号).リーダー表記名 = arBuffer(4)
            選手リスト(選手番号).リーダーサークルコード = arBuffer(5)
            選手リスト(選手番号).リーダー所属名 = arBuffer(6)

            選手リスト(選手番号).パートナ会員番号 = arBuffer(7)
            選手リスト(選手番号).パートナ氏名 = arBuffer(8)
            選手リスト(選手番号).パートナフリガナ = arBuffer(9)
            選手リスト(選手番号).パートナ表記名 = arBuffer(10)
            選手リスト(選手番号).パートナサークルコード = arBuffer(11)
            選手リスト(選手番号).パートナ所属名 = arBuffer(12)

            選手リスト(選手番号).カップル所属名 = arBuffer(13)


        End If

        Dim k As Integer
        For k = 1 To 99
            If UBound(arBuffer) > 13 + k Then
                選手リスト(選手番号).エントリー区分(k) = arBuffer(13 + k)
            End If
        Next k


        '登録済み選手数をカウントアップ
        登録済み選手数 = 登録済み選手数 + 1

    End Sub

End Class
