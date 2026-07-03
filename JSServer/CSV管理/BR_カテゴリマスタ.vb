Public Class BR_カテゴリマスタ

    Public リスト(100) As BR_カテゴリ

    Public 登録済みレコード数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "MBR_Category"

    ''' コンストラクタ
    Sub New(filepath_)

        登録済みレコード数 = 0

        filepath = filepath_

        FileRead()

    End Sub



    '' ********  メソッド *************
    ''

    ''' カテゴリデータをCSVに追加する、同じカテゴリ番号の時は更新する
    ''' 
    Public Function 登録(データ As BR_カテゴリ) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("カテゴリ番号,カテゴリ名,カテゴリ表記名")


            Dim s As Integer
            Dim 登録済みFLAG As Boolean = False

            For s = 1 To 登録済みレコード数

                Dim 元記号, 新記号

                '記号が数値型の場合は、数値に変換して大小比較する。
                If IsNumeric(リスト(s).カテゴリ番号) And IsNumeric(データ.カテゴリ番号) Then
                    元記号 = CInt(リスト(s).カテゴリ番号)
                    新記号 = CInt(データ.カテゴリ番号)
                    'そうではない場合は、文字列のままで比較する
                Else
                    元記号 = リスト(s).カテゴリ番号
                    新記号 = データ.カテゴリ番号
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
    '区分Classを返す
    Public Function GetカテゴリC(カテゴリ番号 As String) As BR_カテゴリ
        Dim rc As BR_カテゴリ = Nothing

        For i = 1 To UBound(リスト)
            If リスト(i) IsNot Nothing Then

                If (リスト(i).カテゴリ番号) = (カテゴリ番号) Then
                    rc = リスト(i)
                    i = UBound(リスト)
                End If
            End If
        Next i

        Return rc
    End Function

    Public Function Getカテゴリ名(カテゴリ番号 As String) As String
        '区分番号を渡すと、カテゴリ名名を返す
        Dim rc As String = ""
        Dim i As Integer

        For i = 1 To UBound(リスト)

            If リスト(i).カテゴリ番号 IsNot Nothing Then
                If (リスト(i).カテゴリ番号) = カテゴリ番号 Then
                    rc = リスト(i).カテゴリ名
                    i = UBound(リスト)
                End If
            End If

        Next i

        Return rc

    End Function

    Public Function Getカテゴリ表記名(カテゴリ番号 As String) As String
        '区分番号を渡すと、区分表記名を返す
        Dim rc As String = ""
        Dim i As Integer

        For i = 1 To UBound(リスト)

            If リスト(i) Is Nothing Then
                MsgBox("カテゴリ番号「" & カテゴリ番号 & "」が定義されていません。")
            End If

            If リスト(i).カテゴリ番号 IsNot Nothing Then
                If (リスト(i).カテゴリ番号) = カテゴリ番号 Then
                    rc = リスト(i).カテゴリ表記名
                    i = UBound(リスト)
                End If
            End If

        Next i

        Return rc

    End Function

    Public Function Getラウンド方式名(カテゴリ番号 As String) As String
        'カテゴリ番号を渡すと、ラウンド方式を返す
        Dim rc As String = ""
        Dim i As Integer

        If リスト(1) Is Nothing Then
            MsgBox("BRカテゴリマスタが登録されていません。")
            Return rc
        End If


        For i = 1 To UBound(リスト)

            If リスト(i).カテゴリ番号 IsNot Nothing Then
                If (リスト(i).カテゴリ番号) = カテゴリ番号 Then
                    rc = リスト(i).ラウンド方式
                    i = UBound(リスト)
                End If
            End If

        Next i

        Return rc

    End Function



    '' 書き込み
    '''Keyで指定したレコードを削除する　　全部消す
    Public Function Deleteレコード()

        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & ".csv"

        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("カテゴリ番号,カテゴリ名,カテゴリ表記名")


            For s = 1 To 登録済みレコード数

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

        ReDim リスト(100)
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
                If Left(stBuffer, 4) = "カテゴリ" Then

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
    Private Function カンマ区切り(カテゴリ As BR_カテゴリ)

        Dim line As String = ""

        line = line & カテゴリ.カテゴリ番号

        line = line & "," & カテゴリ.カテゴリ名
        line = line & "," & カテゴリ.カテゴリ表記名
        line = line & "," & カテゴリ.ラウンド方式

        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りの審判員データを、配列に登録する
    '''
    Private Sub Addデータ(データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim No As Integer = 登録済みレコード数 + 1

        If UBound(arBuffer) >= 2 Then

            リスト(No) = New BR_カテゴリ

            リスト(No).カテゴリ番号 = arBuffer(0)
            リスト(No).カテゴリ名 = arBuffer(1)
            リスト(No).カテゴリ表記名 = arBuffer(2)

            If UBound(arBuffer) >= 3 Then

                リスト(No).ラウンド方式 = arBuffer(3)
            End If

        End If


            '登録済み審判員数をカウントアップ
            登録済みレコード数 = 登録済みレコード数 + 1

    End Sub





End Class
