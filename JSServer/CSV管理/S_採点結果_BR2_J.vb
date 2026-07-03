Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class S_採点結果_BR2_J

    Inherits EventArgs


    Public Property 区分番号 As String
    Public Property ラウンド番号 As String




    Public Property 種目記号 As String  'ブレイキンの場合は　ここが　1R となる
    Public Property 種目名 As String
    Public Property ジャッジ記号 As String
    Public Property SEND_FLAG As String   'ソロの時は、選手の結果を見る。
    Public Property 採点方式 As String



    'ソロ用に追加
    Public Property SG種別 As String


    'ラウンド情報

    Public Property 区分名 As String
    Public Property ラウンド名 As String
    Public Property ジャッジ名 As String

    Public Property 種目番号 As Integer  'ブレイキンの場合は　ここが　1　だったら、１Rと２R、　3　だったら １つ

    Public Property 種目記号_2 As String   '　種目番号が１のときは、　ここが　2R となる


    Public Property ヒート番号 As Integer


    Public Property PCS数 As Integer
    Public Property 減点項目数 As Integer


    Public BR2_PCS設定_J() As S_採点結果_BR2_PCS_J
    Public BR2_減点設定_J() As S_採点結果_BR2_減点_J


    Public BR2_種目結果_J() As S_採点結果_BR2_種目結果_J

    Public Property 選手数 As Integer

    'Private 選手数 As Integer
    Public Property ヒート数 As Integer


    ' Public 登録済みレコード数 As Integer
    Private filepath As String


    'HELPボタン用
    Public HELP As String

    Sub New()

        選手数 = 0

        filepath = System.IO.Directory.GetCurrentDirectory()

    End Sub


    Sub New(filepath_)

        選手数 = 0

        filepath = filepath_

    End Sub

    Public Sub Set_FilePath(filepath_)

        filepath = filepath_
    End Sub


    Public Sub 選手結果初期化(出場選手数 As Integer)

        'If 種目番号 = 1 Then
        ReDim BR2_種目結果_J(2)
        BR2_種目結果_J(1) = New S_採点結果_BR2_種目結果_J(種目記号, 出場選手数, PCS数, 減点項目数)
        BR2_種目結果_J(2) = New S_採点結果_BR2_種目結果_J(種目記号_2, 出場選手数, PCS数, 減点項目数)

        'Else
        'ReDim BR2_種目結果_J(1)
        'BR2_種目結果_J(1) = New S_採点結果_BR2_種目結果_J(種目記号, 出場選手数, PCS数, 減点項目数)
        'End If


    End Sub


    Public Sub JSON書き出し()


        Dim filename As String = "S_" & 区分番号 & "_" & ラウンド番号 & "_" & 種目記号 & "_" & ジャッジ記号 & ".json"


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


    Public Function JSON読み込み() As Integer
        '選手数を返す

        Dim rc As Integer = 0

        Dim filename As String = "S_" & 区分番号 & "_" & ラウンド番号 & "_" & 種目記号 & "_" & ジャッジ記号 & ".json"


        ''JSON読み込み用
        Dim jText As String = String.Empty

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した



            ''ファイルからJSONを読み込む
            Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)


            jText = cReader.ReadToEnd

            ''文字列をJSONにデシリアライズ
            Dim Temp As S_採点結果_BR2_J = JsonConvert.DeserializeObject(Of S_採点結果_BR2_J)(jText)

            ''文字列をJSONにデシリアライズ
            'Dim jObj As Object = JsonConvert.DeserializeObject(jText)

            Me.SEND_FLAG = Temp.SEND_FLAG
            Me.採点方式 = Temp.採点方式

            Me.区分番号 = Temp.区分番号
            Me.区分名 = Temp.区分名

            Me.ラウンド番号 = Temp.ラウンド番号
            Me.ラウンド名 = Temp.ラウンド名

            Me.ジャッジ記号 = Temp.ジャッジ記号
            Me.ジャッジ名 = Temp.ジャッジ名

            Me.ヒート数 = Temp.ヒート数
            Me.ヒート番号 = Temp.ヒート番号

            Me.種目名 = Temp.種目名
            Me.種目記号 = Temp.種目記号
            Me.種目記号_2 = Temp.種目記号_2
            Me.種目番号 = Temp.種目番号

            Me.SG種別 = Temp.SG種別


            Me.選手数 = Temp.選手数
            rc = Me.選手数

            Dim MAXヒート番号 As Integer = 0

            Me.PCS数 = Temp.PCS数
            Me.減点項目数 = Temp.減点項目数


            選手結果初期化（選手数)

            Dim 同時ラウンド数 As Integer = 0
            'If 種目番号 = 1 Then
            If Me.種目記号_2 <> "" Then
                同時ラウンド数 = 2
            Else
                同時ラウンド数 = 1
            End If

            If 選手数 >= 1 Then
                For d = 1 To 同時ラウンド数

                    Me.BR2_種目結果_J(d).種目記号 = Temp.BR2_種目結果_J(d).種目記号
                    For s = 1 To 選手数  '2     
                        Me.BR2_種目結果_J(d).BR2_選手結果_J(s).背番号 = Temp.BR2_種目結果_J(d).BR2_選手結果_J(s).背番号

                        '以下2つはソロ競技専用
                        Me.BR2_種目結果_J(d).BR2_選手結果_J(s).ヒート番号 = Temp.BR2_種目結果_J(d).BR2_選手結果_J(s).ヒート番号
                        Me.BR2_種目結果_J(d).BR2_選手結果_J(s).SEND_FLAG = Temp.BR2_種目結果_J(d).BR2_選手結果_J(s).SEND_FLAG


                        For p = 1 To PCS数
                            Me.BR2_種目結果_J(d).BR2_選手結果_J(s).PCS得点(p).PCS素点 = Temp.BR2_種目結果_J(d).BR2_選手結果_J(s).PCS得点(p).PCS素点
                        Next p
                        For r = 1 To 減点項目数
                            Me.BR2_種目結果_J(d).BR2_選手結果_J(s).減点(r).減点 = Temp.BR2_種目結果_J(d).BR2_選手結果_J(s).減点(r).減点
                        Next r

                    Next s
                Next d
            End If


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()

        End If

        Return rc

    End Function

    Public Function JSONデータセット(jStr As String) As Integer
        '選手数を返す

        Dim rc As Integer = 0

        ''文字列をJSONにデシリアライズ
        Dim jObj As Object = JsonConvert.DeserializeObject(jStr)

        Dim 行数 = jObj.SelectToken("S_採点結果_選手_J").Count - 1


        Me.区分番号 = jObj.SelectToken("区分番号").value
        Me.ラウンド番号 = jObj.SelectToken("ラウンド番号").value
        Me.種目記号 = jObj.SelectToken("種目記号").value
        Me.ジャッジ記号 = jObj.SelectToken("ジャッジ記号").value


        Me.SEND_FLAG = jObj.SelectToken("SEND_FLAG").value
        Me.採点方式 = jObj.SelectToken("採点方式").value



        Me.区分名 = jObj.SelectToken("区分名").value
        Me.ラウンド名 = jObj.SelectToken("ラウンド名").value
        Me.ジャッジ名 = jObj.SelectToken("ジャッジ名").value

        Me.SG種別 = jObj.SelectToken("SG種別").value



        Me.選手数 = 行数

        Dim MAXヒート番号 As Integer = 0

        If 行数 >= 1 Then

            '行数を基に子クラスを作成

        End If

        ヒート数 = MAXヒート番号



        Return rc

    End Function


    ' Public Function Get_選手数() As Integer
    '   Return 選手数
    ' End Function

    Public Function Get_PCS点(背番号 As String, 種目順 As Integer, PCS番号 As Integer) As Decimal

        Dim rc As Decimal = 0

        For s = 1 To 選手数
            If BR2_種目結果_J(種目順).BR2_選手結果_J(s) IsNot Nothing Then
                If BR2_種目結果_J(種目順).BR2_選手結果_J(s).背番号 = 背番号 Then
                    rc = BR2_種目結果_J(種目順).BR2_選手結果_J(s).PCS得点(PCS番号).PCS素点
                    s = 選手数
                End If
            End If

        Next s

        Return rc

    End Function

    Public Function Get_減点(背番号 As String, 種目順 As Integer, 減点番号 As Integer) As Decimal

        Dim rc As Decimal = 0

        For s = 1 To 選手数
            If BR2_種目結果_J(種目順).BR2_選手結果_J(s) IsNot Nothing Then
                If BR2_種目結果_J(種目順).BR2_選手結果_J(s).背番号 = 背番号 Then
                    rc = BR2_種目結果_J(種目順).BR2_選手結果_J(s).減点(減点番号).減点
                    s = 選手数
                End If
            End If

        Next s

        Return rc

    End Function



    '背番号を渡すと、S_採点結果_選手_Jを返す
    Public Function Get_選手結果(背番号 As String, 種目順 As Integer) As S_採点結果_BR2_選手_J

        Dim rc As S_採点結果_BR2_選手_J = Nothing

        For s = 1 To 選手数
            If BR2_種目結果_J(種目順).BR2_選手結果_J(s) IsNot Nothing Then
                If BR2_種目結果_J(種目順).BR2_選手結果_J(s).背番号 = 背番号 Then
                    rc = BR2_種目結果_J(種目順).BR2_選手結果_J(s)
                    s = 選手数
                End If
            End If


        Next s

        Return rc

    End Function




End Class

Public Class S_採点結果_BR2_PCS_J

    '各PCSの設定
    Public Property PCS番号 As Integer
    'Public Property PCS名称 As String
    Public Property PCS略称 As String

    Public Property PCS最大点 As Decimal
    'Public Property PCS点数単位 As Decimal　　'0.25刻みか、1点刻みか


End Class

Public Class S_採点結果_BR2_減点_J

    '各PCSの設定
    Public Property 減点番号 As Integer
    '   Public Property 減点項目名 As String
    Public Property 減点略称 As String

    Public Property 減点値数 As Integer

    Public 減点値リスト() As Decimal

    Public Sub New(減点値数_ As Integer)
        減点値数 = 減点値数_

        ReDim 減点値リスト(減点値数)


    End Sub

End Class


Public Class S_採点結果_BR2_種目結果_J

    Public Property 種目記号 As String

    Public BR2_選手結果_J() As S_採点結果_BR2_選手_J

    Public Sub New(種目記号_ As String, 選手数 As Integer, PCS数 As Integer, 減点数 As Integer)

        種目記号 = 種目記号_

        ReDim BR2_選手結果_J(選手数)

        For s = 1 To 選手数
            BR2_選手結果_J(s) = New S_採点結果_BR2_選手_J(PCS数, 減点数)
        Next s

    End Sub

End Class


Public Class S_採点結果_BR2_選手_J


    Public Property 背番号 As String


    'ソロ用に追加
    Public Property ヒート番号 As Integer
    Public Property SEND_FLAG As String




    Public PCS得点() As S_採点結果_BR2_選手_PCS得点_J
    Public 減点() As S_採点結果_BR2_選手_減点_J


    Public Sub New(PCS数 As Integer, 減点数 As Integer)

        ReDim PCS得点(PCS数)

        For p = 1 To PCS数
            PCS得点(p) = New S_採点結果_BR2_選手_PCS得点_J
        Next p

        ReDim 減点(減点数)

        For r = 1 To 減点数
            減点(r) = New S_採点結果_BR2_選手_減点_J
        Next r


    End Sub



End Class

Public Class S_採点結果_BR2_選手_PCS得点_J

    Public Property PCS素点 As Decimal


End Class

Public Class S_採点結果_BR2_選手_減点_J

    Public Property 減点 As Decimal


End Class


