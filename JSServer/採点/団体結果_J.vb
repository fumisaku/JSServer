Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class 団体結果_J

    Public Property 団体区分番号 As String
    Public Property 団体区分名 As String


    Public Property チーム数 As Integer
    Public Property 区分数 As Integer

    Public 区分定義_J() As 区分定義配列_J   'このJSONで定義されている区分番号の一覧


    Public チーム結果_J() As 団体結果_チーム_J

    Private filepath As String
    Sub New(filepath_)

        チーム数 = 0

        filepath = filepath_

    End Sub


    Public Function チーム追加(チーム名 As String) As Integer
        '追加したチームINDを返す

        Dim rc As Integer = 0

        ReDim Preserve チーム結果_J(チーム数 + 1)
        チーム結果_J(チーム数 + 1) = New 団体結果_チーム_J

        チーム結果_J(チーム数 + 1).No = チーム数 + 1
        チーム結果_J(チーム数 + 1).チーム名 = チーム名

        'チームの下の区分を追加
        ReDim チーム結果_J(チーム数 + 1).区分結果_J(区分数)
        チーム結果_J(チーム数 + 1).区分数 = 区分数

        For k = 1 To 区分数
            チーム結果_J(チーム数 + 1).区分結果_J(k) = New 団体結果_チーム_J.団体結果_チーム_区分_J
            チーム結果_J(チーム数 + 1).区分結果_J(k).区分番号 = 区分定義_J(k).区分番号

        Next k


        チーム数 = チーム数 + 1

        rc = チーム数

        Return rc

    End Function

    Public Function 区分追加(区分番号 As String, 区分名 As String, 区分記号 As String, 団体採点方式 As String, 倍率 As Decimal) As Integer

        '区分を追加するときには、すべてのチーム分を追加する。

        Dim rc As Integer = 0

        ReDim Preserve 区分定義_J(区分数 + 1)
        区分定義_J(区分数 + 1) = New 区分定義配列_J
        区分定義_J(区分数 + 1).区分番号 = 区分番号
        区分定義_J(区分数 + 1).区分記号 = 区分記号
        区分定義_J(区分数 + 1).区分名 = 区分名
        区分定義_J(区分数 + 1).団体採点方式 = 団体採点方式
        区分定義_J(区分数 + 1).倍率 = 倍率

        For t = 1 To チーム数
            ReDim Preserve チーム結果_J(t).区分結果_J(区分数 + 1)
            チーム結果_J(t).区分結果_J(区分数 + 1) = New 団体結果_チーム_J.団体結果_チーム_区分_J
            チーム結果_J(t).区分結果_J(区分数 + 1).区分番号 = 区分番号

            チーム結果_J(t).区分数 = 区分数 + 1
        Next t

        区分数 = 区分数 + 1

        rc = 区分数

        Return rc

    End Function

    Public Function Get_区分IND(区分番号 As String, 区分名 As String, 区分記号 As String, 団体採点方式 As String, 倍率 As Decimal) As Integer
        '区分結果の　IND番号を返す、存在しない場合は区分を追加して、返す

        Dim rc As Integer = 0

        Dim FindFlag As Boolean = False
        For k = 1 To 区分数
            If 区分定義_J(k).区分番号 = 区分番号 Then
                rc = k
                k = 区分数
                FindFlag = True
            End If
        Next k


        If FindFlag = False Then
            区分数 = 区分追加(区分番号, 区分名, 区分記号, 団体採点方式, 倍率)
            rc = 区分数
        End If

        Return rc

    End Function

    Public Sub 集計()
        '設定された情報を基に総合得点　総合順位を計算する


        For t = 1 To チーム数
            Dim チーム得点 As Decimal = 0

            For k = 1 To 区分数
                チーム得点 = チーム得点 + チーム結果_J(t).区分結果_J(k).区分得点
            Next k

            チーム結果_J(t).合計点 = チーム得点
        Next t






        '総合順位の算出
        Dim 総合順位 As Integer = 0
        Dim 確定チーム数 As Integer = 0
        Dim 総合得点() As Decimal
        Dim 総合得点IND() As Integer

        ReDim 総合得点(チーム数)
        ReDim 総合得点IND(チーム数)

        For t = 1 To チーム数
            総合得点(t) = チーム結果_J(t).合計点
            総合得点IND(t) = t
        Next t

        '並べ替え
        Array.Sort(総合得点, 総合得点IND)

        Dim 前者得点 As Decimal = 0
        Dim 前者順位 As Integer = 0
        Dim カウンタ As Integer = 1
        For t = チーム数 To 1 Step -1

            If 総合得点(t) = 前者得点 Then
                チーム結果_J(総合得点IND(t)).順位 = 前者順位

            Else
                チーム結果_J(総合得点IND(t)).順位 = カウンタ

                前者得点 = 総合得点(t)
                前者順位 = カウンタ
                カウンタ = カウンタ + 1
            End If

        Next t




    End Sub

    Public Sub 区分クリア(区分番号)
        '区分の得点をクリアする。

        Dim 区分IND As Integer = 0

        For k = 1 To 区分数
            If 区分定義_J(k).区分番号 = 区分番号 Then
                区分IND = k
                k = 区分数
            End If
        Next k


        For t = 1 To チーム数
            チーム結果_J(t).区分結果_J(区分IND).区分得点 = 0

            For s = 1 To チーム結果_J(t).区分結果_J(区分IND).選手数
                チーム結果_J(t).区分結果_J(区分IND).選手結果_J(s).選手M番号 = ""
                チーム結果_J(t).区分結果_J(区分IND).選手結果_J(s).背番号 = ""
                チーム結果_J(t).区分結果_J(区分IND).選手結果_J(s).リーダー表記名 = ""
                チーム結果_J(t).区分結果_J(区分IND).選手結果_J(s).パートナー表記名 = ""
                チーム結果_J(t).区分結果_J(区分IND).選手結果_J(s).得点 = 0
            Next s
            チーム結果_J(t).区分結果_J(区分IND).選手数 = 0

        Next t



    End Sub

    Public Function Get_得点(区分番号 As String, 背番号 As String) As Decimal

        Dim rc As Decimal = 0

        For t = 1 To チーム数
            For k = 1 To 区分数
                If チーム結果_J(t).区分結果_J(k).区分番号 = 区分番号 Then

                    For s = 1 To チーム結果_J(t).区分結果_J(k).選手数
                        If チーム結果_J(t).区分結果_J(k).選手結果_J(s).背番号 = 背番号 Then
                            rc = チーム結果_J(t).区分結果_J(k).選手結果_J(s).得点

                            s = チーム結果_J(t).区分結果_J(k).選手数
                            t = チーム数
                        End If
                    Next s

                    k = 区分数
                End If
            Next k
        Next t

        Return rc


    End Function


    Public Sub JSON書き出し()


        Dim filename As String = "S_団体結果_" & 団体区分番号 & ".json"


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)

        ''元のファイルに出力する
        Using writer = New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.GetEncoding("shift-jis"))
            writer.WriteLine(jText)
        End Using

    End Sub


    Public Function Get_JSON文字列() As String


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)


        Return jText

    End Function

    Public Function JSON読み込み() As 団体結果_J

        Dim rc As 団体結果_J = Nothing

        Dim filename As String = "S_団体結果_" & 団体区分番号 & ".json"


        ''JSON読み込み用
        Dim jText As String = String.Empty


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した


            ''ファイルからJSONを読み込む
            ' Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)

            Dim fs As New System.IO.FileStream(filepath & "\" & filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite)
            'FileStreamを基にしたStringReaderのインスタンスを作成
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("shift_jis")

            Dim sr As New System.IO.StreamReader(fs, enc)

            'ファイルの内容をすべて読み込む
            jText = sr.ReadToEnd()



            'jText = cReader.ReadToEnd


            rc = JsonConvert.DeserializeObject(Of 団体結果_J)(jText)




            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            'cReader.Close()

            '閉じる
            'srを閉じれば、基になるfsも閉じられる
            sr.Close()


            rc.filepath = filepath
        End If


        Return rc
    End Function


    Public Function CSV書き出し()

        Dim rc As Integer = 0


        Dim filename As String = "SC_団体結果_" & 団体区分番号 & ".csv"


        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            Dim csvtext As String = "チーム名,総合順位,総合得点,"
            For k = 1 To 区分数
                csvtext = csvtext & 区分定義_J(k).区分名 & ","
            Next k

            sw.WriteLine(csvtext)


            'データを書き出し

            For 順位 = 1 To チーム数
                For t = 1 To チーム数
                    If チーム結果_J(t).順位 = 順位 Then
                        csvtext = ""

                        csvtext = チーム結果_J(t).チーム名 & ","
                        csvtext = csvtext & チーム結果_J(t).順位 & ","
                        csvtext = csvtext & チーム結果_J(t).合計点 & ","

                        For k = 1 To 区分数
                            csvtext = csvtext & チーム結果_J(t).区分結果_J(k).区分得点 & ","
                        Next k

                        sw.WriteLine(csvtext)
                    End If

                Next t
            Next 順位

            '閉じる 
            sw.Close()

        Catch ex As Exception
            rc = 1

        End Try

        Return rc



    End Function


    Public Class 区分定義配列_J
        Public Property 区分番号 As String
        Public Property 区分記号 As String
        Public Property 区分名 As String
        Public Property 団体採点方式 As String
        Public Property 倍率 As Decimal

    End Class


    Public Class 団体結果_チーム_J

        Public Property No As String
        Public Property チーム名 As String
        Public Property 合計点 As Decimal
        Public Property 順位 As Decimal

        Public Property 区分数 As Integer

        Public 区分結果_J() As 団体結果_チーム_区分_J

        Public Sub New()

        End Sub


        Class 団体結果_チーム_区分_J

            Public Property 区分番号 As String
            Public Property 区分得点 As Decimal
            Public Property 選手数 As Integer

            Public 選手結果_J() As 団体結果_チーム_区分_選手_J

            Public Sub 初期設定(MAX選手数 As Integer)

                ReDim 選手結果_J(MAX選手数)
                For t = 1 To MAX選手数
                    選手結果_J(t) = New 団体結果_チーム_区分_選手_J
                Next t

            End Sub

            Public Sub 選手結果追加(選手M番号 As Integer, 背番号 As String, L表記名 As String, P表記名 As String, 得点 As Decimal)


                Dim FindFlag As Boolean = False
                For s = 1 To 選手数
                    If 選手結果_J(s).背番号 = 背番号 Then
                        選手結果_J(s).選手M番号 = 選手M番号
                        選手結果_J(s).背番号 = 背番号
                        選手結果_J(s).リーダー表記名 = L表記名
                        選手結果_J(s).パートナー表記名 = P表記名
                        選手結果_J(s).得点 = 得点
                        s = 選手数
                        FindFlag = True
                    End If
                Next s

                If FindFlag = False Then
                    ReDim Preserve 選手結果_J(選手数 + 1)
                    選手結果_J(選手数 + 1) = New 団体結果_チーム_区分_選手_J
                    選手結果_J(選手数 + 1).選手M番号 = 選手M番号
                    選手結果_J(選手数 + 1).背番号 = 背番号
                    選手結果_J(選手数 + 1).リーダー表記名 = L表記名
                    選手結果_J(選手数 + 1).パートナー表記名 = P表記名
                    選手結果_J(選手数 + 1).得点 = 得点

                    選手数 = 選手数 + 1
                End If

            End Sub


            Class 団体結果_チーム_区分_選手_J

                Public Property 選手M番号 As String
                Public Property 背番号 As String
                Public Property リーダー表記名 As String
                Public Property パートナー表記名 As String
                Public Property 得点 As Decimal


            End Class

        End Class
    End Class







End Class
