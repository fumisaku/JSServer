Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class S_採点結果_V2_J
    Inherits EventArgs


    Implements System.ICloneable


    Public Property 区分番号 As String
    Public Property 区分名 As String
    Public Property ラウンド番号 As String
    Public Property ラウンド名 As String
    Public Property 総種目数 As Integer

    Public 種目記号_J() As S_採点結果_V2_種目記号_J


    Public Property ジャッジタイプ As String
    Public Property ジャッジ記号 As String
    Public Property ジャッジ名 As String

    Public Property 種目番号 As Integer
    Public Property 現種目記号 As String
    Public Property 現種目名 As String


    Public Property 採点方式 As String
    Public Property SG種別 As String
    Public Property SEND方式 As String    'ヒート毎　or 種目毎
    Public Property SEND_FLAG As String  '種目毎用


    Public Property 選手数 As Integer
    Public Property ヒート数 As Integer
    Public Property 現ヒート番号 As Integer



    Public Property Cali_MAX As Double
    Public Property Cali_MIN As Double

    Public Property PCS数 As Integer
    Public PCS設定_J() As S_採点結果_V2_PCS設定_J

    Public 担当PCS_J() As S_採点結果_V2_HT担当PCS_J

    Public Property 減点項目数 As Integer
    Public 減点設定_J() As S_採点結果_V2_減点設定_J

    Public Property 課題数 As Integer
    Public 課題設定_J() As S_採点結果_V2_課題設定_J
    Public TES設定_J() As S_採点結果_V2_TES設定_J

    Public Property TES減点数 As Integer
    Public TES減点_J() As S_採点結果_V2_TES減点_J

    Public Property タイマー設定数 As Integer
    Public Property タイマー種別 As String    'D カウントダウン、　カウントアップ 等
    Public タイマー設定() As S_採点結果_V2_タイマー設定_J

    Public Property UP予定数 As Integer

    Public S_採点結果_選手_J() As S_採点結果_選手_V2_J


    Public lastUpdate As String


    Private filepath As String

    Private LOG As LOG_C

    Sub New()

        filepath = System.IO.Directory.GetCurrentDirectory()

        初期化()
    End Sub


    Sub New(filepath_)


        filepath = filepath_

    End Sub


    Public Sub Set_filepath(filepath_)

        filepath = filepath_

    End Sub

    Public Sub Set_LOG(_LOG As LOG_C)

        LOG = _LOG

    End Sub

    Sub 初期化()

        '種目記号リストの初期化
        If 総種目数 > 0 Then
            ReDim 種目記号_J(総種目数)

            For i = 1 To 総種目数
                種目記号_J(i) = New S_採点結果_V2_種目記号_J
            Next i

        End If

        'PCS設定の初期化
        If ジャッジタイプ = "J" Then
            If PCS数 > 0 Then
                ReDim PCS設定_J(PCS数)
                For i = 1 To PCS数
                    PCS設定_J(i) = New S_採点結果_V2_PCS設定_J
                Next i

            End If

            '担当PCS設定の初期化
            If ヒート数 > 0 Then
                ReDim 担当PCS_J(ヒート数)

                For i = 1 To ヒート数
                    担当PCS_J(i) = New S_採点結果_V2_HT担当PCS_J(PCS数)

                Next i
            End If


        End If

        '減点設定の初期化
        If ジャッジタイプ = "R" Then
            If 減点項目数 > 0 Then
                ReDim 減点設定_J(減点項目数)
                For i = 1 To 減点項目数
                    減点設定_J(i) = New S_採点結果_V2_減点設定_J
                Next i

            End If
        End If

        '課題設定の初期化
        If ジャッジタイプ = "J" Then
            If 課題数 > 0 Then
                ReDim 課題設定_J(課題数)

                For i = 1 To 課題数
                    課題設定_J(i) = New S_採点結果_V2_課題設定_J

                Next i
            End If

        End If

        'TES設定の初期化
        If ジャッジタイプ = "T" Then
            If 課題数 > 0 Then
                ReDim TES設定_J(課題数)

                For i = 1 To 課題数
                    TES設定_J(i) = New S_採点結果_V2_TES設定_J
                Next i
            End If

            'TES減点の初期化

            If TES減点数 > 0 Then
                ReDim TES減点_J(TES減点数)
                For i = 1 To TES減点数
                    TES減点_J(i) = New S_採点結果_V2_TES減点_J
                Next i
            End If
        End If


        '選手結果の初期化
        If 選手数 > 0 Then
            ReDim S_採点結果_選手_J(選手数)

            For i = 1 To 選手数
                S_採点結果_選手_J(i) = New S_採点結果_選手_V2_J(PCS数, 減点項目数, 課題数, TES減点数, ジャッジタイプ)

            Next i


        End If


    End Sub


    Public Sub JSON書き出し()


        Dim filename As String = "S_" & 区分番号 & "_" & ラウンド番号 & "_" & 現種目記号 & "_" & ジャッジ記号 & ".json"


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)


        Dim counter As Integer = 0
        For counter = 1 To 10
            Try

                ''元のファイルに出力する
                Using writer = New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.GetEncoding("shift-jis"))
                    writer.WriteLine(jText)
                End Using

                counter = 10

            Catch ex As Exception

                If counter < 10 Then

                    System.Threading.Thread.Sleep(200)

                Else

                    Try
                        'MsgBox(ex.ToString)
                        LOG.LogAdd("S_採点結果_V2_J.JSON書き出し 失敗 " & ex.ToString, 1)

                    Catch ex2 As Exception


                    End Try

                End If


            End Try

        Next counter


    End Sub

    Public Function Get_JSON文字列() As String


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)


        Return jText

    End Function



    Public Function JSON読み込み() As S_採点結果_V2_J


        Dim rc As S_採点結果_V2_J = Nothing

        Dim filename As String = "S_" & 区分番号 & "_" & ラウンド番号 & "_" & 現種目記号 & "_" & ジャッジ記号 & ".json"


        ''JSON読み込み用
        Dim jText As String = String.Empty


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した
            Dim lastException As Exception = Nothing
            For counter As Integer = 1 To 10

                Try
                    ''ファイルからJSONを読み込む
                    Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)

                    jText = cReader.ReadToEnd

                    rc = JsonConvert.DeserializeObject(Of S_採点結果_V2_J)(jText)

                    ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
                    cReader.Close()


                Catch ex As Exception

                    lastException = ex
                    If counter < 10 Then
                        System.Threading.Thread.Sleep(200)
                    Else

                        Try
                            'MsgBox(ex.ToString)
                            LOG.LogAdd("S_採点結果_V2_J.JSON読み込み 失敗 " & ex.ToString, 1)

                        Catch ex2 As Exception


                        End Try


                    End If

                End Try
            Next
        End If

        Return rc


    End Function



    Public Sub JSON追加(追加分JSON As S_採点結果_V2_J)
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

    Public Function 新JSONデータセット(jStr As String) As S_採点結果_V2_J

        Dim rc As S_採点結果_V2_J = Nothing


        rc = JsonConvert.DeserializeObject(Of S_採点結果_V2_J)(jStr)


        Return rc


    End Function





    Public Sub 選手結果初期化(出場選手数 As Integer)

        ReDim S_採点結果_選手_J(出場選手数)
        For i = 1 To 出場選手数
            S_採点結果_選手_J(i） = New S_採点結果_選手_V2_J(PCS数, 減点項目数, 課題数, TES減点数, ジャッジタイプ)
        Next i

    End Sub


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
    Public Function Get_選手結果(背番号 As String) As S_採点結果_選手_V2_J

        Dim rc As S_採点結果_選手_V2_J = Nothing

        For i = 1 To 選手数
            If S_採点結果_選手_J(i).背番号 = 背番号 Then
                rc = S_採点結果_選手_J(i)
                i = 選手数
            End If
        Next i

        Return rc

    End Function


    '現在のチェック数 総数を返す
    Public Function Get_現在チェック数()

        Dim rc As Integer = 0

        For i = 1 To 選手数
            If S_採点結果_選手_J(i).点数 > 0 Then
                rc = rc + 1
            End If
        Next i

        Return rc


    End Function

    Public Function Get_ヒートチェック数(ヒート番号 As Integer)
        'ヒート番号毎のチェック数を返す

        Dim rc As Integer = 0

        For i = 1 To 選手数
            If S_採点結果_選手_J(i).ヒート番号 = ヒート番号 Then
                If S_採点結果_選手_J(i).点数 > 0 Then
                    rc = rc + 1
                End If
            End If
        Next i

        Return rc


    End Function

    Public Class S_採点結果_V2_種目記号_J

        '各PCSの設定
        Public Property 種目番号 As Integer
        Public Property 種目記号 As String

    End Class
    Public Class S_採点結果_V2_PCS設定_J



        '各PCSの設定
        Public Property PCS番号 As Integer
        Public Property PCS略称 As String


    End Class

    Public Class S_採点結果_V2_HT担当PCS_J

        '各PCSの設定

        Public 担当PCS() As S_採点結果_V2_HT担当PCS_担当_J


        'ヒート後のジャッジが担当するPCSを設定
        Sub New(PCS数 As Integer)

            If PCS数 > 0 Then
                ReDim 担当PCS(PCS数)

                For i = 1 To PCS数
                    担当PCS(i) = New S_採点結果_V2_HT担当PCS_担当_J
                Next i
            End If
        End Sub

        Public Class S_採点結果_V2_HT担当PCS_担当_J
            Public Property 担当 As Boolean

        End Class
    End Class



    Public Class S_採点結果_V2_減点設定_J

        '各PCSの設定
        Public Property 減点番号 As Integer
        Public Property 減点略称 As String
        Public Property 初期値 As Decimal
        Public Property STEP値 As Decimal
        Public Property MAX値 As Decimal

    End Class
    Public Class S_採点結果_V2_課題設定_J

        '各PCSの設定
        Public Property 課題番号 As Integer
        Public Property フィガー名 As String


    End Class


    Public Class S_採点結果_V2_TES設定_J

        '各PCSの設定
        Public Property 課題番号 As Integer
        Public Property Base点 As Decimal

    End Class

    Public Class S_採点結果_V2_TES減点_J

        '各PCSの設定
        Public Property TES減点番号 As Integer
        Public Property TES減点略称 As String
        Public Property 初期値 As Decimal
        Public Property STEP値 As Decimal
        Public Property MAX値 As Decimal



    End Class

    Public Class S_採点結果_V2_タイマー設定_J

        '各タイマー設定の値
        Public Property タイマー時間 As String

    End Class


    Public Class S_採点結果_選手_V2_J

        Implements System.ICloneable



        Public Property 背番号 As String
        Public Property 選手名 As String
        Public Property ヒート番号 As Integer
        Public Property SEND_FLAG As String  'ヒート毎SENDの時はこちらを見る

        Public Property 点数 As Decimal
        Public Property 備考 As String

        Public PCS得点_J() As S_採点結果_選手_V2_PCS得点_J

        Public GOE_J() As S_採点結果_選手_V2_GOE_J

        Public 減点_J() As S_採点結果_選手_V2_減点_J

        Public TES_J() As S_採点結果_選手_V2_TES_J

        Public Other_J() as S_採点結果_選手_V2_Other_J


        Sub New(PCS数 As Integer, 減点項目数 As Integer, 課題数 As Integer, TES減点数 As Integer, ジャッジタイプ As String)

            'PCS得点の初期化

            If ジャッジタイプ = "J" Or ジャッジタイプ = "S" Then
                If PCS数 > 0 Then
                    ReDim PCS得点_J(PCS数)

                    For i = 1 To PCS数
                        PCS得点_J(i) = New S_採点結果_選手_V2_PCS得点_J
                    Next i
                End If

                'GOEの初期化


                If 課題数 > 0 Then
                    ReDim GOE_J(課題数)

                    For i = 1 To 課題数
                        GOE_J(i) = New S_採点結果_選手_V2_GOE_J
                    Next i

                End If

            End If

            '減点の初期化


            If ジャッジタイプ = "R" Then
                If 減点項目数 > 0 Then
                    ReDim 減点_J(減点項目数)

                    For i = 1 To 減点項目数
                        減点_J(i) = New S_採点結果_選手_V2_減点_J
                    Next i
                End If
            End If

            'TES得点の初期化


            If ジャッジタイプ = "T" Then
                If 課題数 > 0 Then
                    ReDim TES_J(課題数)

                    For i = 1 To 課題数
                        TES_J(i) = New S_採点結果_選手_V2_TES_J(TES減点数)
                    Next i

                End If

            End If

            'Otherの初期化
            Dim Other項目数 As Integer = 1
            ReDim Other_J(Other項目数)
            For o = 1 To Other項目数
                Other_J(o) = New S_採点結果_選手_V2_Other_J
            Next o

        End Sub




        Public Class S_採点結果_選手_V2_PCS得点_J

            Public Property PCS素点 As Decimal

        End Class

        Public Class S_採点結果_選手_V2_GOE_J

            Public Property GOE As Decimal

        End Class

        Public Class S_採点結果_選手_V2_減点_J

            Public Property 減点 As Decimal

        End Class

        Public Class S_採点結果_選手_V2_TES_J

            Public Property Base As Decimal

            Public TES減点_J() As S_採点結果_選手_V2_TES減点_J

            Public Sub New(TES減点数)

                'TES減点の初期化
                If TES減点数 IsNot Nothing Then
                    ReDim TES減点_J(TES減点数)

                    For i = 1 To TES減点数
                        TES減点_J(i) = New S_採点結果_選手_V2_TES減点_J
                    Next i
                End If

            End Sub


            Public Class S_採点結果_選手_V2_TES減点_J
                Public Property TES減点 As Decimal

            End Class


        End Class

        Public Class S_採点結果_選手_V2_Other_J

            Public 項目名 As String
            Public 得点 As Decimal
            Public 順位 As Integer

        End Class


        Public Function Clone() As Object Implements ICloneable.Clone
            Return Me.MemberwiseClone()
        End Function


    End Class

    Public Function Clone() As Object Implements ICloneable.Clone
        Return Me.MemberwiseClone()
    End Function

End Class
