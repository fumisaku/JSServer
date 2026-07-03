Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class KANS_MOVIE_START_J

    Public Property 区分番号 As String
    Public Property 区分名 As String
    Public Property ラウンド番号 As String
    Public Property ラウンド名 As String
    Public Property 種目記号 As String
    Public Property 種目名 As String
    Public Property SG種別 As String
    Public Property ヒート番号 As String
    Public Property 背番号 As String
    Public Property リーダー名 As String
    Public Property パートナー名 As String
    Public Property 所属 As String



    Sub New()


    End Sub


    Public Function Get_JSON文字列() As String


        ''JSONにシリアライズする
        'Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)
        Dim jText = JsonConvert.SerializeObject(Me)


        Return jText

    End Function


    Public Function JSON読み込み(JSON文字列 As String) As KANS_MOVIE_START_J

        Dim rc As KANS_MOVIE_START_J = Nothing



        ''JSON読み込み用
        Dim jText As String = String.Empty

        'ファイルの内容をすべて読み込む
        jText = JSON文字列

        rc = JsonConvert.DeserializeObject(Of KANS_MOVIE_START_J)(jText)


        Return rc
    End Function


End Class
