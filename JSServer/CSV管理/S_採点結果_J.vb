Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class S_採点結果_J
    Implements System.ICloneable

    Public Property 区分番号 As String
    Public Property ラウンド番号 As String




    Public Property 種目記号 As String
    Public Property ジャッジ記号 As String
    Public Property SEND_FLAG As String   '種目毎SENDの時　（ヒート毎Sendには使えない。）
    Public Property 採点方式 As String


    'ヒート表用

    'ラウンド情報
    Public Property UP予定数 As Integer
    Public Property 総種目数 As Integer
    Public Property 種目記号リスト_1 As String
    Public Property 種目記号リスト_2 As String
    Public Property 種目記号リスト_3 As String
    Public Property 種目記号リスト_4 As String
    Public Property 種目記号リスト_5 As String

    Public Property 区分名 As String
    Public Property ラウンド名 As String
    Public Property ジャッジ名 As String
    Public Property 種目名 As String



    Public S_採点結果_選手_J() As S_採点結果_選手_J


    Private 選手数 As Integer
    Public ヒート数 As Integer



    '2022/6/2 ブレイキンプレセレクション用（ヒート毎SEND)に対応
    Public ヒート番号 As Integer
    Public SG種別 As String

    'Public 登録済みレコード数 As Integer
    Private filepath As String


    Sub New(filepath_)

        選手数 = 0

        filepath = filepath_

    End Sub

    Public Sub Set_filepath(filepath_)

        filepath = filepath_

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

    Public Sub JSON追加(追加分JSON As S_採点結果_J)
        'Me　に追加分JSONの選手情報を追加する
        '既に同じ背番号があれば、追加分で更新する。

        If 追加分JSON IsNot Nothing Then

            Dim Find_Flag As Boolean = False
            For a = 1 To UBound(追加分JSON.S_採点結果_選手_J)

                For m = 1 To UBound(Me.S_採点結果_選手_J)
                    If Me.S_採点結果_選手_J(m).背番号 = 追加分JSON.S_採点結果_選手_J(a).背番号 Then

                        Me.S_採点結果_選手_J(m) = 追加分JSON.S_採点結果_選手_J(a).Clone

                        Find_Flag = True
                        m = UBound(Me.S_採点結果_選手_J)
                    End If
                Next m

                If Find_Flag = False Then
                    '存在しない場合は、追加

                    ReDim Preserve Me.S_採点結果_選手_J(UBound(Me.S_採点結果_選手_J) + 1)
                    Me.S_採点結果_選手_J(UBound(Me.S_採点結果_選手_J)) = 追加分JSON.S_採点結果_選手_J(a).Clone

                End If
            Next a

            選手数 = UBound(Me.S_採点結果_選手_J)

        End If



    End Sub


    Public Function Get_JSON文字列() As String


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)


        Return jText

    End Function


    Public Function 新JSON読み込み() As S_採点結果_J


        Dim rc As S_採点結果_J = Nothing

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

            rc = JsonConvert.DeserializeObject(Of S_採点結果_J)(jText)

            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()

        End If

        If rc IsNot Nothing Then

            If rc.S_採点結果_選手_J Is Nothing Then
                rc.選手数 = 0
            Else
                rc.選手数 = UBound(rc.S_採点結果_選手_J)
            End If

        End If

        Return rc


    End Function

    Public Function JSON読み込み＿OLD() As Integer
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
            Dim jObj As Object = JsonConvert.DeserializeObject(jText)

            Dim 行数 = jObj.SelectToken("S_採点結果_選手_J").Count - 1



            Me.SEND_FLAG = jObj.SelectToken("SEND_FLAG").value
            Me.採点方式 = jObj.SelectToken("採点方式").value



            'ヒート表用
            Me.UP予定数 = jObj.SelectToken("UP予定数").value
            Me.総種目数 = jObj.SelectToken("総種目数").value
            Me.種目記号リスト_1 = jObj.SelectToken("種目記号リスト_1").value
            Me.種目記号リスト_2 = jObj.SelectToken("種目記号リスト_2").value
            Me.種目記号リスト_3 = jObj.SelectToken("種目記号リスト_3").value
            Me.種目記号リスト_4 = jObj.SelectToken("種目記号リスト_4").value
            Me.種目記号リスト_5 = jObj.SelectToken("種目記号リスト_5").value

            Me.区分名 = jObj.SelectToken("区分名").value
            Me.ラウンド名 = jObj.SelectToken("ラウンド名").value
            Me.ジャッジ名 = jObj.SelectToken("ジャッジ名").value
            Me.種目名 = jObj.SelectToken("種目名").value



            Me.選手数 = 行数

            Dim MAXヒート番号 As Integer = 0

            If 行数 >= 1 Then

                '行数を基に子クラスを作成
                ReDim S_採点結果_選手_J(行数)
                For i = 1 To 行数
                    S_採点結果_選手_J(i） = New S_採点結果_選手_J
                Next i


                For i = 1 To 行数
                    If jObj.SelectToken("S_採点結果_選手_J")(i)("背番号").value IsNot Nothing Then

                        S_採点結果_選手_J(i).背番号 = jObj.SelectToken("S_採点結果_選手_J")(i)("背番号").value
                        S_採点結果_選手_J(i).ヒート番号 = jObj.SelectToken("S_採点結果_選手_J")(i)("ヒート番号").value
                        S_採点結果_選手_J(i).点数 = jObj.SelectToken("S_採点結果_選手_J")(i)("点数").value
                        S_採点結果_選手_J(i).備考 = jObj.SelectToken("S_採点結果_選手_J")(i)("備考").value

                        If MAXヒート番号 < S_採点結果_選手_J(i).ヒート番号 Then
                            MAXヒート番号 = S_採点結果_選手_J(i).ヒート番号
                        End If

                    End If
                Next i

                rc = 行数

            End If

            ヒート数 = MAXヒート番号


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If


        Return rc

    End Function



    Public Function 新JSONデータセット(jStr As String) As S_採点結果_J

        Dim rc As S_採点結果_J = Nothing


        rc = JsonConvert.DeserializeObject(Of S_採点結果_J)(jStr)


        Return rc


    End Function




    Public Function JSONデータセット_OLD(jStr As String) As Integer
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


        'ヒート表用
        Me.UP予定数 = jObj.SelectToken("UP予定数").value
        Me.総種目数 = jObj.SelectToken("総種目数").value
        Me.種目記号リスト_1 = jObj.SelectToken("種目記号リスト_1").value
        Me.種目記号リスト_2 = jObj.SelectToken("種目記号リスト_2").value
        Me.種目記号リスト_3 = jObj.SelectToken("種目記号リスト_3").value
        Me.種目記号リスト_4 = jObj.SelectToken("種目記号リスト_4").value
        Me.種目記号リスト_5 = jObj.SelectToken("種目記号リスト_5").value

        Me.区分名 = jObj.SelectToken("区分名").value
        Me.ラウンド名 = jObj.SelectToken("ラウンド名").value
        Me.ジャッジ名 = jObj.SelectToken("ジャッジ名").value
        Me.種目名 = jObj.SelectToken("種目名").value




        Me.選手数 = 行数

        Dim MAXヒート番号 As Integer = 0

        If 行数 >= 1 Then

            '行数を基に子クラスを作成
            ReDim S_採点結果_選手_J(行数)
            For i = 1 To 行数
                S_採点結果_選手_J(i） = New S_採点結果_選手_J
            Next i


            For i = 1 To 行数
                If jObj.SelectToken("S_採点結果_選手_J")(i)("背番号").value IsNot Nothing Then

                    S_採点結果_選手_J(i).背番号 = jObj.SelectToken("S_採点結果_選手_J")(i)("背番号").value
                    S_採点結果_選手_J(i).ヒート番号 = jObj.SelectToken("S_採点結果_選手_J")(i)("ヒート番号").value
                    S_採点結果_選手_J(i).点数 = jObj.SelectToken("S_採点結果_選手_J")(i)("点数").value
                    S_採点結果_選手_J(i).備考 = jObj.SelectToken("S_採点結果_選手_J")(i)("備考").value


                    If MAXヒート番号 < S_採点結果_選手_J(i).ヒート番号 Then
                        MAXヒート番号 = S_採点結果_選手_J(i).ヒート番号
                    End If
                End If
            Next i

            rc = 行数
        End If

        ヒート数 = MAXヒート番号


        Return rc

    End Function

    Public Sub 選手結果初期化(出場選手数 As Integer)

        ReDim S_採点結果_選手_J(出場選手数)
        For i = 1 To 出場選手数
            S_採点結果_選手_J(i） = New S_採点結果_選手_J
        Next i

    End Sub


    Public Function Get_選手数() As Integer

        If S_採点結果_選手_J Is Nothing Then
            選手数 = 0
        Else
            選手数 = UBound(S_採点結果_選手_J)
        End If

        Return 選手数


    End Function

    Public Function Get_ヒート数() As Integer
        Return ヒート数
    End Function



    '背番号を渡すと、得点を返す
    Public Function Get_得点(背番号 As String) As Double

        Dim rc As Double = 0

        For i = 1 To 選手数
            If S_採点結果_選手_J(i).背番号 = 背番号 Then
                rc = S_採点結果_選手_J(i).点数
                i = 選手数
            End If
        Next i

        Return rc

    End Function

    '背番号を渡すと、備考を返す
    Public Function Get_備考(背番号 As String) As String

        Dim rc As String = ""

        For i = 1 To 選手数
            If S_採点結果_選手_J(i).背番号 = 背番号 Then
                rc = S_採点結果_選手_J(i).備考
                i = 選手数
            End If
        Next i

        Return rc

    End Function


    '背番号を渡すと、S_採点結果_選手_Jを返す
    Public Function Get_選手結果(背番号 As String) As S_採点結果_選手_J

        Dim rc As S_採点結果_選手_J = Nothing

        For i = 1 To 選手数
            If S_採点結果_選手_J(i).背番号 = 背番号 Then
                rc = S_採点結果_選手_J(i)
                i = 選手数
            End If
        Next i

        Return rc

    End Function


    '現在のチェック数を返す
    Public Function Get_現在チェック数()

        Dim rc As Integer = 0

        For i = 1 To 選手数
            If S_採点結果_選手_J(i).点数 > 0 Then
                rc = rc + 1
            End If
        Next i

        Return rc




    End Function

    Public Function Clone() As Object Implements ICloneable.Clone
        Return Me.MemberwiseClone()
    End Function
End Class

Public Class S_採点結果_選手_J
    Implements System.ICloneable

    Public Property 背番号 As String
    Public Property ヒート番号 As Integer
    Public Property 点数 As Decimal
    Public Property 備考 As String



    '2022/6/2 ブレイキンプレセレクション用に対応
    Public Property 選手名 As String


    Public Property SEND_FLAG As String  'ヒート毎SENDの時はこちらを見る


    Public Function Clone() As Object Implements ICloneable.Clone
        Return Me.MemberwiseClone()
    End Function
End Class

