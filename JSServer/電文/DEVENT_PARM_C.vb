'DEVENT 電文に使用するパラメータ用JSONクラス
Imports System
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class DEVENT_PARM_C

    Public Sub New()


    End Sub

    '======= 項目

    Public Property 種別 As String


    Public Property PARM1 As String
    Public Property PARM2 As String
    Public Property PARM3 As String







    '=====JSON 基本機能
    Public Function Get_JSON文字列() As String


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)


        Return jText

    End Function

    Public Function Get_JSON(str As String) As DEVENT_PARM_C

        Dim rc As DEVENT_PARM_C = Nothing

        rc = JsonConvert.DeserializeObject(Of DEVENT_PARM_C)(str)

        Return rc

    End Function

    Public Sub JSON書き出し()


        Dim filename As String = "DEVENT_" & ".json"
        Dim filepath As String = ""   ' マスタデータ.Z_システム設定.Comp_filepath


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)



        ''元のファイルに出力する
        Using writer = New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.GetEncoding("shift-jis"))
            writer.WriteLine(jText)
        End Using

    End Sub


    Public Function JSON読み込み(filepath As String) As DEVENT_PARM_C

        Dim rc As DEVENT_PARM_C = Nothing


        Dim filename As String = "DEVENT_" & ".json"


        ''JSON読み込み用
        Dim jText As String = String.Empty


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した


            ''ファイルからJSONを読み込む
            Dim fs As New System.IO.FileStream(filepath & "\" & filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite)

            'FileStreamを基にしたStringReaderのインスタンスを作成
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("shift_jis")

            Dim sr As New System.IO.StreamReader(fs, enc)

            'ファイルの内容をすべて読み込む
            jText = sr.ReadToEnd()



            rc = JsonConvert.DeserializeObject(Of DEVENT_PARM_C)(jText)

            '閉じる
            'srを閉じれば、基になるfsも閉じられる
            sr.Close()


            'rc.filepath = filepath
        End If

        Return rc

    End Function


End Class
