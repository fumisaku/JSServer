Public Class D_種目マスタ

    Public リスト(3000) As D_種目

    Public 登録済みレコード数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "MD_Syumoku"

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
    Public Function 登録(データ As D_種目) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("区分番号,ラウンド番号,種目順,種目記号,SG種別,ヒート数,担当審判グループ,CaliMax,CaliMin")


            Dim s As Integer
            Dim 登録済みFLAG As Boolean = False

            For s = 1 To 登録済みレコード数

                Dim 元記号, 新記号

                '記号が数値型の場合は、数値に変換して大小比較する。
                If IsNumeric(リスト(s).区分番号 & リスト(s).ラウンド番号 & リスト(s).種目順) And
                   IsNumeric(データ.区分番号 & データ.ラウンド番号 & データ.種目順) Then
                    元記号 = CInt(リスト(s).区分番号 & リスト(s).ラウンド番号 & リスト(s).種目順)
                    新記号 = CInt(データ.区分番号 & データ.ラウンド番号 & データ.種目順)
                    'そうではない場合は、文字列のままで比較する
                Else
                    元記号 = リスト(s).区分番号 & リスト(s).ラウンド番号 & リスト(s).種目順
                    新記号 = データ.区分番号 & データ.ラウンド番号 & データ.種目順
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

    Public Function Get_種目Class(区分番号 As String, ラウンド番号 As String, 種目順 As String) As D_種目
        'Keyを渡すと該当の種目Classを返す　無い時はNothing
        Dim rc As D_種目 = Nothing

        Dim i As Integer

        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And
               リスト(i).ラウンド番号 = ラウンド番号 And
               CInt(リスト(i).種目順） = CInt(種目順） Then

                rc = リスト(i)
                i = 登録済みレコード数
            End If
        Next i

        Return rc

    End Function

    Public Function Get_SG種別表記名(区分番号 As String, ラウンド番号 As String, 種目順 As String) As String
        'Keyを渡すと該当の種目、SG種別を日本語で返す
        Dim rc As String = ""

        Dim SG種別 As String = ""

        Dim i As Integer

        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And
               リスト(i).ラウンド番号 = ラウンド番号 And
               CInt(リスト(i).種目順） = CInt(種目順） Then

                SG種別 = リスト(i).SG種別
                i = 登録済みレコード数
            End If
        Next i

        Select Case SG種別
            Case "S"
                rc = "ソロ"
            Case "G"
                rc = "全員"
            Case "D"
                rc = "対戦"
        End Select


        Return rc

    End Function

    Public Function Get_SG種別表記名_E(区分番号 As String, ラウンド番号 As String, 種目順 As String) As String
        'Keyを渡すと該当の種目、SG種別を英語で返す
        Dim rc As String = ""

        Dim SG種別 As String = ""

        Dim i As Integer

        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And
               リスト(i).ラウンド番号 = ラウンド番号 And
               CInt(リスト(i).種目順） = CInt(種目順） Then

                SG種別 = リスト(i).SG種別
                i = 登録済みレコード数
            End If
        Next i

        Select Case SG種別
            Case "S"
                rc = "Solo"
            Case "G"
                rc = "Group"
            Case "D"
                rc = "Duel"
        End Select


        Return rc

    End Function

    Public Function Get_レコード番号(区分番号 As String, ラウンド番号 As String, 種目順 As String) As Integer
        'Keyを渡すとレコード番号を返す

        Dim i As Integer
        Dim rc As Integer = 0


        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And
               リスト(i).ラウンド番号 = ラウンド番号 And
               CInt(リスト(i).種目順） = CInt(種目順） Then

                rc = i
                i = 登録済みレコード数
            End If
        Next i

        Return rc

    End Function

    'Get種目Classを返す
    Public Function Get_種目Class(区分番号 As String, ラウンド番号 As String, 種目順 As Integer) As D_種目
        'Keyを渡すとレコード番号を返す

        Dim i As Integer
        Dim rc As D_種目 = Nothing


        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And
               リスト(i).ラウンド番号 = ラウンド番号 And
               CInt(リスト(i).種目順） = CInt(種目順） Then

                rc = リスト(i)
                i = 登録済みレコード数
            End If
        Next i

        Return rc

    End Function


    Public Function Get_種目順(区分番号 As String, ラウンド番号 As String, 種目記号 As String) As String
        '種目記号を渡すと種目順を返す

        Dim i As Integer
        Dim rc As String = ""

        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And
               リスト(i).ラウンド番号 = ラウンド番号 And
               リスト(i).種目記号 = 種目記号 Then

                rc = リスト(i).種目順
                i = 登録済みレコード数
            End If
        Next i

        Return rc

    End Function

    Public Function Get_種目数(ByVal 区分番号 As String, ByVal ラウンド番号 As String, ByRef 種目記号リスト As Array) As Integer
        '区分番号、ラウンド番号を渡すと、種目数を返す。

        Dim i As Integer
        Dim rc As Integer = 0


        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And
               リスト(i).ラウンド番号 = ラウンド番号 Then

                rc = rc + 1
            End If
        Next i

        Dim 種目リスト_()
        ReDim 種目リスト_(rc)

        rc = 0
        For i = 1 To 登録済みレコード数
            If リスト(i).区分番号 = 区分番号 And
               リスト(i).ラウンド番号 = ラウンド番号 Then

                rc = rc + 1
                種目リスト_(rc) = リスト(i).種目記号
            End If
        Next i

        種目記号リスト = 種目リスト_

        Return rc

    End Function


    '' 書き込み
    'Keyで指定したレコードを削除する
    Public Function Deleteレコード(区分番号 As String, ラウンド番号 As String)

        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("区分番号,ラウンド番号,種目順,種目記号,SG種別,ヒート数,担当審判グループ,CaliMax,CaliMin")


            Dim s As Integer

            For s = 1 To 登録済みレコード数

                If リスト(s).区分番号 = 区分番号 And リスト(s).ラウンド番号 = ラウンド番号 Then
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

        'もう1回読み込む
        FileRead()

        Return rc

    End Function


    ''' 読込み
    ''' 
    Public Sub FileRead()

        ReDim リスト(3000)
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
                    登録済みレコード数 = 登録済みレコード数 + 1
                End If



            End While


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If






    End Sub

    ''' 個別の区分データをカンマ区切りの文字列に変換する
    '''
    Private Function カンマ区切り(種目 As D_種目)

        Dim line As String = ""

        line = line & 種目.区分番号

        line = line & "," & 種目.ラウンド番号
        line = line & "," & 種目.種目順
        line = line & "," & 種目.種目記号
        line = line & "," & 種目.SG種別
        line = line & "," & 種目.ヒート数.ToString
        line = line & "," & 種目.担当審判グループ.ToString
        line = line & "," & 種目.CaliMax.ToString
        line = line & "," & 種目.CaliMin.ToString

        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りの審判員データを、配列に登録する
    '''
    Private Sub Addデータ(データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim No As Integer = 登録済みレコード数 + 1

        If UBound(arBuffer) >= 8 Then

            リスト(No) = New D_種目

            Dim 区分番号 = String.Format("{0:D2}", arBuffer(0))

            リスト(No).区分番号 = 区分番号
            リスト(No).ラウンド番号 = arBuffer(1)
            リスト(No).種目順 = arBuffer(2)
            リスト(No).種目記号 = arBuffer(3)
            リスト(No).SG種別 = arBuffer(4)
            リスト(No).ヒート数 = arBuffer(5)
            リスト(No).担当審判グループ = arBuffer(6)
            リスト(No).CaliMax = arBuffer(7)
            リスト(No).CaliMin = arBuffer(8)

        End If


        '登録済み審判員数をカウントアップ
        '登録済みレコード数 = 登録済みレコード数 + 1

    End Sub


End Class
