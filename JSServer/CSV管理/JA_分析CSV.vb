Imports System.IO
Public Class JA_分析CSV

    Public リスト(100) As JA_分析

    Public 登録済みレコード数 As Integer
    Private ReadOnly filepath As String

    Const File頭文字列 = "JA_"


    ''' コンストラクタ
    ''' 
    Sub New(filepath_)

        登録済みレコード数 = 0

        filepath = filepath_

        'FileRead()


    End Sub


    Public Function 登録(データ() As JA_分析, 競技会番号 As String, 区分番号 As String, ラウンド番号 As String) As Integer
        '成功した場合は、0   失敗した場合は、1以上を返す

        Dim filename As String = File頭文字列 & 競技会番号 & "_" & 区分番号 & "_" & ラウンド番号 & ".csv"



        Dim rc As Integer = 0

        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            sw.WriteLine("開催日,競技会名,区分名,SL区分,ラウンド記号,種目記号,SG区分,背番号,リーダー名,パートナー名,ジャッジ番号,ジャッジ名,PCS名,ジャッジ点数,乖離度,乖離度絶対値,ジャッジ人数,選手PCS得点")


            For s = 0 To UBound(データ)
                If データ(s) IsNot Nothing Then
                    sw.WriteLine(カンマ区切り(データ(s)))
                End If
            Next s


            '閉じる 
            sw.Close()

        Catch ex As Exception
            rc = 1
            MsgBox(ex.ToString)
        End Try

        Return rc

    End Function



    '' 書き込み
    ''



    ''' 読込み
    ''' 
    Public Function FileRead(競技会番号 As String, 区分番号 As String, ラウンド番号 As String) As Integer

        ReDim リスト(5000)
        登録済みレコード数 = 0

        Dim filename As String = File頭文字列 & 競技会番号 & "_" & 区分番号 & "_" & ラウンド番号 & ".csv"


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
                If Left(stBuffer, 3) = "開催日" Then

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
    Private Function カンマ区切り(分析 As JA_分析)

        Dim line As String = ""

        line = 分析.開催日

        line = line & "," & 分析.競技会名
        line = line & "," & 分析.区分名
        line = line & "," & 分析.SL区分
        line = line & "," & 分析.ラウンド記号
        line = line & "," & 分析.種目記号
        line = line & "," & 分析.SG区分
        line = line & "," & 分析.背番号
        line = line & "," & 分析.リーダー名
        line = line & "," & 分析.パートナー名
        line = line & "," & 分析.ジャッジ番号
        line = line & "," & 分析.ジャッジ名
        line = line & "," & 分析.PCS名
        line = line & "," & 分析.ジャッジ点数
        line = line & "," & 分析.乖離度
        line = line & "," & 分析.乖離度絶対値
        line = line & "," & 分析.ジャッジ人数
        line = line & "," & 分析.選手PCS得点


        Return line


    End Function



    ''' ファイルから読み込んだ カンマ区切りのデータを、配列に登録する
    '''
    Private Sub Addデータ(データ As String)


        'カンマでセパレート
        Dim arBuffer = データ.Split(",")
        Dim No As Integer = 登録済みレコード数 + 1

        If UBound(arBuffer) >= 4 Then

            リスト(No) = New JA_分析

            リスト(No).開催日 = arBuffer(0)
            リスト(No).競技会名 = arBuffer(1)
            リスト(No).区分名 = arBuffer(2)
            リスト(No).SL区分 = arBuffer(3)
            リスト(No).ラウンド記号 = arBuffer(4)
            リスト(No).種目記号 = arBuffer(5)
            リスト(No).SG区分 = arBuffer(6)
            リスト(No).背番号 = arBuffer(7)
            リスト(No).リーダー名 = arBuffer(8)
            リスト(No).パートナー名 = arBuffer(9)
            リスト(No).ジャッジ番号 = arBuffer(10)
            リスト(No).ジャッジ名 = arBuffer(11)
            リスト(No).PCS名 = arBuffer(12)
            リスト(No).ジャッジ点数 = arBuffer(13)
            リスト(No).乖離度 = arBuffer(14)
            リスト(No).乖離度絶対値 = arBuffer(15)
            リスト(No).ジャッジ人数 = arBuffer(16)
            リスト(No).選手PCS得点 = arBuffer(17)

        End If



        '登録済み数をカウントアップ
        登録済みレコード数 = 登録済みレコード数 + 1

    End Sub

    Public Sub Get_データ(ByRef 分析データ(,))

        ReDim 分析データ(登録済みレコード数, 18)

        Dim k As Integer = 1

        For i = 1 To 登録済みレコード数
            If リスト(i).開催日 IsNot Nothing Then

                分析データ(k, 0) = リスト(i).開催日
                分析データ(k, 1) = リスト(i).競技会名
                分析データ(k, 2) = リスト(i).区分名
                分析データ(k, 3) = リスト(i).SL区分
                分析データ(k, 4) = リスト(i).ラウンド記号
                分析データ(k, 5) = リスト(i).種目記号
                分析データ(k, 6) = リスト(i).SG区分
                分析データ(k, 7) = リスト(i).背番号
                分析データ(k, 8) = リスト(i).リーダー名
                分析データ(k, 9) = リスト(i).パートナー名
                分析データ(k, 10) = リスト(i).ジャッジ番号
                分析データ(k, 11) = リスト(i).ジャッジ名
                分析データ(k, 12) = リスト(i).PCS名
                分析データ(k, 13) = リスト(i).ジャッジ点数
                分析データ(k, 14) = リスト(i).乖離度
                分析データ(k, 15) = リスト(i).乖離度絶対値
                分析データ(k, 16) = リスト(i).ジャッジ人数
                分析データ(k, 17) = リスト(i).選手PCS得点

                k = k + 1
            End If
        Next i


    End Sub

End Class
