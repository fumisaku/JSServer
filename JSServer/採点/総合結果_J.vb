Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class 総合結果_J

    Public Property 総合区分番号 As String
    Public Property 総合ラウンド番号 As String
    Public Property 選手Mリスト番号 As String

    Public Property 選手数 As Integer
    Public Property 区分種目数 As Integer


    Public 選手結果() As 総合結果_選手結果_J
    Public 区分定義() As 区分定義_J


    Public Sub New()
        ReDim 選手結果(選手数)
        ReDim 区分定義(区分種目数)

    End Sub

    Public Sub JSON書き出し()

        Dim マスタデータ = New マスタデータ
        Dim filepath = マスタデータ.Z_システム設定.Comp_filepath

        Dim filename As String = "S_総合結果_" & 総合区分番号 & "_" & 総合ラウンド番号 & ".json"


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)

        ''元のファイルに出力する
        Using writer = New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.GetEncoding("shift-jis"))
            writer.WriteLine(jText)
        End Using

        マスタデータ = Nothing


    End Sub


    Public Function Get_JSON文字列() As String


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)


        Return jText

    End Function

    Public Function JSON読み込み(総合区分番号_ As String, 総合ラウンド番号_ As String) As 総合結果_J

        総合区分番号 = 総合区分番号_
        総合ラウンド番号 = 総合ラウンド番号_

        Dim マスタデータ = New マスタデータ
        Dim filepath = マスタデータ.Z_システム設定.Comp_filepath

        Dim rc As 総合結果_J = Nothing

        Dim filename As String = "S_総合結果_" & 総合区分番号 & "_" & 総合ラウンド番号 & ".json"


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


            rc = JsonConvert.DeserializeObject(Of 総合結果_J)(jText)




            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            'cReader.Close()

            '閉じる
            'srを閉じれば、基になるfsも閉じられる
            sr.Close()


            'rc.filepath = filepath
        End If

        マスタデータ = Nothing

        Return rc

    End Function


    Public Function CSV書き出し()

        Dim マスタデータ = New マスタデータ
        Dim filepath = マスタデータ.Z_システム設定.Comp_filepath

        Dim rc As Integer = 0


        Dim filename As String = "SC_総合結果_" & 総合区分番号 & "_" & 総合ラウンド番号 & ".csv"


        Try
            'ファイルを上書きし、Shift JISで書き込む 
            Dim sw As New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.Default)

            'ヘッダーを書き出し
            Dim csvtext As String = "背番号,総合順位,総合得点,"
            For k = 1 To 区分種目数
                csvtext = csvtext & 区分定義(k).種目記号 & ","
            Next k

            sw.WriteLine(csvtext)


            'データを書き出し

            For 順位 = 1 To 選手数
                For t = 1 To 選手数
                    If 選手結果(t).総合順位 = 順位 Then
                        csvtext = ""

                        csvtext = 選手結果(t).背番号 & ","
                        csvtext = csvtext & 選手結果(t).総合順位 & ","
                        csvtext = csvtext & 選手結果(t).総合得点 & ","

                        For k = 1 To 区分種目数
                            csvtext = csvtext & 選手結果(t).区分種目結果(k).区分得点 & ","
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

        マスタデータ = Nothing


        Return rc



    End Function


    Public Sub Add_区分定義(区分番号 As String, 種目記号 As String, ラウンド番号 As String)

        Dim マスタデータ = New マスタデータ

        ReDim Preserve 区分定義(区分種目数 + 1)

        区分定義(区分種目数 + 1) = New 区分定義_J

        区分定義(区分種目数 + 1).区分番号 = 区分番号
        区分定義(区分種目数 + 1).区分記号 = マスタデータ.B_区分マスタ.Get区分C(区分番号).区分記号
        区分定義(区分種目数 + 1).区分名 = マスタデータ.B_区分マスタ.Get区分C(区分番号).区分表記名
        区分定義(区分種目数 + 1).種目記号 = 種目記号
        区分定義(区分種目数 + 1).ラウンド番号 = ラウンド番号



        For s = 1 To UBound(選手結果)
            If 選手結果(s) IsNot Nothing Then

                ReDim Preserve 選手結果(s).区分種目結果(区分種目数 + 1)
                選手結果(s).区分種目結果(区分種目数 + 1) = New 総合結果_選手_区分種目結果_J

                選手結果(s).区分種目結果(区分種目数 + 1).区分番号 = 区分番号
                選手結果(s).区分種目結果(区分種目数 + 1).種目記号 = 種目記号
            End If

        Next s

        区分種目数 = 区分種目数 + 1

        マスタデータ = Nothing

    End Sub

    Public Sub add_選手結果(背番号 As String, 総合順位 As String, 総合得点 As String)

        ReDim Preserve 選手結果(選手数 + 1)
        選手結果(選手数 + 1) = New 総合結果_選手結果_J

        選手結果(選手数 + 1).背番号 = 背番号
        選手結果(選手数 + 1).総合順位 = 総合順位
        選手結果(選手数 + 1).総合得点 = 総合得点

        ReDim Preserve 選手結果(選手数 + 1).区分種目結果(区分種目数)
        For d = 1 To 区分種目数
            選手結果(選手数 + 1).区分種目結果(d) = New 総合結果_選手_区分種目結果_J

            選手結果(選手数 + 1).区分種目結果(d).区分番号 = 区分定義(d).区分番号
            選手結果(選手数 + 1).区分種目結果(d).種目記号 = 区分定義(d).種目記号

        Next d

        選手数 = 選手数 + 1

    End Sub

    Public Class 区分定義_J
        Public Property 区分番号 As String
        Public Property 区分記号 As String
        Public Property 区分名 As String
        Public Property 種目記号 As String

        Public Property ラウンド番号 As String


    End Class

    Public Class 総合結果_選手結果_J

        Public Property 背番号 As String
        Public Property 総合順位 As String
        Public Property 総合得点 As String


        Public 区分種目結果() As 総合結果_選手_区分種目結果_J

    End Class

    Public Class 総合結果_選手_区分種目結果_J

        Public Property 区分番号 As String
        Public Property 種目記号 As String

        Public Property 区分得点 As Decimal


    End Class

End Class




