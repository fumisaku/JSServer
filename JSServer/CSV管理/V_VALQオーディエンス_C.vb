Imports JSServer.V_VALQオーディエンス_C.採点_C

Public Class V_VALQオーディエンス_C
    'バルカーカップのオーディエンス採点詳細

    'Private Const filename As String = "V_VAL.csv"

    Private ラウンド番号 As String

    Public AUD採点 As 採点_C = Nothing

    Public AUD最高点 As Decimal = 0

    Sub New(filePath As String, ラウンド番号_ As String)

        ラウンド番号 = ラウンド番号_
        ReadCSV(filePath)

    End Sub

    Public Function Get_採点(背番号) As 採点詳細_C

        If AUD採点 Is Nothing Then
            Return Nothing
        End If
        For i As Integer = 1 To AUD採点.選手採点.Length - 1
            If AUD採点.選手採点(i).背番号 = 背番号 Then
                Return AUD採点.選手採点(i)
            End If
        Next
        Return Nothing


    End Function




    'CSVファイルから読み込む
    '第1列は背番号
    '第2列はオーディエンス得点
    '第3列はオーディエンス順位
    'ヘッダー行の　4列目に"採点方式"

    'オーディエンス順位は1～の整数

    'CSVからの読み込み　ファイルの文字コードはShiftJIS
    Private Sub ReadCSV(filePath As String)

        Dim filename = "V_VAL_" & ラウンド番号 & ".csv"

        'ファイルの存在確認
        If Not System.IO.File.Exists(filePath & "\" & filename) Then
            'Throw New Exception("CSVファイルが見つかりません: " & filePath)
            Exit Sub
        End If

        Dim lines As String() = System.IO.File.ReadAllLines(filePath & "\" & filename, System.Text.Encoding.GetEncoding("shift_jis"))

        Dim result As New List(Of V_VALQオーディエンス_C)
        ' ヘッダー行の確認
        If lines.Length = 0 Then
            'Throw New Exception("CSVファイルが空です。")
            Exit Sub
        End If
        Dim header As String() = lines(0).Split(","c)

        AUD採点 = New 採点_C(lines.Length)

        ' データ行の読み込み
        For i As Integer = 1 To lines.Length - 1
            Dim columns As String() = lines(i).Split(","c)
            If columns.Length < 3 Then
                'Continue For ' 不完全な行はスキップ
            End If

            AUD採点.選手採点(i).背番号 = columns(0).Trim()

            Dim TEMP得点 As String = columns(1).Trim()
            If TEMP得点 = "" Then
                TEMP得点 = "0"
            End If

            'TEMP得点が数字かどうかを確認
            If Not IsNumeric(TEMP得点) Then
                TEMP得点 = "0"
            End If

            AUD採点.選手採点(i).AUD得点 = Decimal.Parse(TEMP得点)

            If AUD最高点 < Decimal.Parse(TEMP得点) Then
                AUD最高点 = Decimal.Parse(TEMP得点)
            End If


            Dim TEMP順位 As String = columns(2).Trim()
            If TEMP順位 = "" Then
                TEMP順位 = "0"
            End If
            'TEMP順位が数字かどうかを確認
            If Not IsNumeric(TEMP順位) Then
                TEMP順位 = "0"
            End If
            AUD採点.選手採点(i).AUD順位 = Integer.Parse(TEMP順位)

        Next

    End Sub

    Public Class 採点_C


        Public 選手採点() As 採点詳細_C

        Sub New(選手数)
            ReDim 選手採点(選手数)
            For i As Integer = 1 To 選手数
                選手採点(i) = New 採点詳細_C
            Next
        End Sub

        Public Class 採点詳細_C
            Public Property 背番号() As String
            Public Property AUD得点() As Decimal
            Public Property AUD順位() As Integer


        End Class



    End Class




End Class
