Imports System.IO
Public Class 競技会一覧HTML


    Public Sub New()


    End Sub

    Public Sub CreateHTML(LOCAL指定 As String, 初期化 As String)

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ


        Dim 言語 As String = マスタデータ.Z_システム設定.言語


        Dim データ(10000) As String
        Dim i As Integer = 0

        Dim ORGデータ() As String = Nothing
        Dim 行数 As Integer = ORG_Fileread(ORGデータ)

        For i = 0 To 行数
            データ(i) = ORGデータ(i)
        Next i


        If LOCAL指定 = "LOCAL" Then
            i = i + 1 : データ(i) = "<link rel=""stylesheet"" type=""text/css"" href=""result.css"">"
        Else
            i = i + 1 : データ(i) = ""
        End If


        i = i + 1 : データ(i) = "<table Class=""table_coomon"" width=""100%"">"
        i = i + 1 : データ(i) = "  <thead>"
        i = i + 1 : データ(i) = "     <tr>"
        i = i + 1 : データ(i) = "       <td colspan = ""7"" Class=""Title"" >【Today's Competition】 </td> "
        i = i + 1 : データ(i) = "     </tr>"
        i = i + 1 : データ(i) = "  </thead>"
        i = i + 1 : データ(i) = "  <tbody"
        i = i + 1 : データ(i) = "     <tr>"
        i = i + 1 : データ(i) = "         <td Class=""Title01"">" & マスタデータ.A_競技会マスタ.公認競技会NO & "</td> "
        If 初期化 = "初期化" Then
            i = i + 1 : データ(i) = "         <td colspan = ""6"" Class=""Kubun"" >" & マスタデータ.A_競技会マスタ.競技会名 & " (開始前)</td> "

        Else
            i = i + 1 : データ(i) = "         <td colspan = ""6"" Class=""Kubun"" ><a href = ""../result/" & マスタデータ.A_競技会マスタ.公認競技会NO & "/index.html"">" & マスタデータ.A_競技会マスタ.競技会名 & "</td> "
        End If

        i = i + 1 : データ(i) = "     </tr>"

        i = i + 1 : データ(i) = "  </tbody>"
        i = i + 1 : データ(i) = "</table>"

        i = i + 1 : データ(i) = "</body>"
        i = i + 1 : データ(i) = "</html>"



        Dim filename As String = "index.html"


        If LOCAL指定 = "LOCAL" Then

            ファイル書出し(マスタデータ.Z_システム設定.Comp_filepath & "\", filename, データ)
        Else
            ファイル書出し(マスタデータ.Z_システム設定.Comp_filepath & "\", filename, データ)

        End If

        マスタデータ = Nothing

    End Sub



    Public Function FileCheck(filepath As String, filename As String) As Boolean
        '存在している場合は、True   存在していない場合は、Falseを返す

        Dim rc As Boolean = False


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then

            'ファイルが存在しない
            rc = False
        Else
            'ファイルが存在した
            rc = True

        End If

        Return rc

    End Function


    Private Sub ファイル書出し(ByVal パス名 As String, ByVal File名 As String, ByVal データ() As String)
        '===========================
        '概要　HTMLを作成する(SC_xxxxxx.csv)
        '入力　新File名,データ
        '出力　なし
        '===========================


        Dim NewFilename As String = パス名 & "\" & File名

        '_00_初期化()

        'ファイル書き出し
        Dim Writer As New IO.StreamWriter(NewFilename, False, System.Text.Encoding.GetEncoding("utf-8"))

        Dim i

        For i = 0 To UBound(データ)
            If データ(i) <> "" Then
                Writer.Write(データ(i))
                Writer.WriteLine()
            End If

        Next i
        Writer.Close()

    End Sub

    Private Function ORG_Fileread(ByRef データ()) As Integer
        '行数を返す
        '

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim パス名 As String = マスタデータ.Z_システム設定.システムPath
        Dim ORGファイル名 As String = "index_ORG.html"

        Dim 行数 As Integer = 0

        If Dir(パス名 & "\" & ORGファイル名).ToUpper <> ORGファイル名.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した

            ' StreamReader の新しいインスタンスを生成する
            Using stream As New FileStream(パス名 & "\" & ORGファイル名, FileMode.Open, FileAccess.Read)

                Dim cReader As New System.IO.StreamReader(stream, System.Text.Encoding.Default)

                ' 読み込んだ結果をすべて格納するための変数を宣言する
                Dim stResult() As String

                '全行を取得し改行で区切り
                stResult = Split(cReader.ReadToEnd, vbCrLf)

                '行数を取得
                行数 = stResult.Count - 1

                ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
                cReader.Close()


                データ = stResult

            End Using

        End If

        Return 行数

    End Function

End Class
