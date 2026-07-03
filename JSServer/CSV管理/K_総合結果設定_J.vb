Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class K_総合結果設定_J

    Public Property 総合区分番号 As String
    Public Property 総合区分名 As String

    Public ラウンド設定_J() As 総合結果_ラウンド設定_J



    Private filepath As String

    Sub New(filepath_)
        '競技会フォルダ―に保存する。

        filepath = filepath_

    End Sub

    Public Sub 設定追加(ラウンド番号 As String, 採点方式 As String, 対象区分番号 As String, 対象ラウンド番号 As String)

        Dim FindFlag

        For r = 1 To UBound(ラウンド設定_J)


        Next r


    End Sub

    Public Sub JSON書き出し()


        Dim filename As String = "MK_総合設定_" & 総合区分番号 & ".json"


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

    Public Function JSON読み込み() As K_総合結果設定_J

        Dim rc As K_総合結果設定_J = Nothing

        Dim filename As String = "MK_総合設定_" & 総合区分番号 & ".json"


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


            rc = JsonConvert.DeserializeObject(Of K_総合結果設定_J)(jText)




            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            'cReader.Close()

            '閉じる
            'srを閉じれば、基になるfsも閉じられる
            sr.Close()


            rc.filepath = filepath
        End If


        Return rc
    End Function


End Class

Public Class 総合結果_ラウンド設定_J

    Public Property ラウンド番号 As String

    Public Property 採点方式 As String

    Public 対象区分_J() As 総合結果_対象区分_J


End Class

Public Class 総合結果_対象区分_J

    Public Property 区分番号 As String
    Public Property ラウンド番号 As String


End Class




