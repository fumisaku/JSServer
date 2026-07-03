
Public Class G_審判無効

    Private ラウンド番号 As String
    Private 区分番号 As String

    Public 無効_詳細() As G_詳細_C = Nothing

    Sub New(filePath As String, 区分番号_ As String, ラウンド番号_ As String)

        ラウンド番号 = ラウンド番号_
        区分番号 = 区分番号_

        ReadCSV(filePath)

    End Sub



    'CSVファイルから読み込む
    '第1列はヒート番号
    '第2列～　ジャッジ毎の無効FLAG
    'ヘッダー行有り

    'CSVからの読み込み　ファイルの文字コードはShiftJIS
    Private Sub ReadCSV(filePath As String)

        Dim filename = "G_VOID_" & 区分番号 & "_" & ラウンド番号 & ".csv"

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

        ReDim 無効_詳細(lines.Length - 1)

        For s = 1 To UBound(無効_詳細)
            無効_詳細(s) = New G_詳細_C(header.Length)
        Next s


        ' データ行の読み込み
        For s = 1 To UBound(無効_詳細)

            Dim columns As String() = lines(s).Split(","c)

            If columns.Length < 2 Then
                'Continue For ' 不完全な行はスキップ
            End If

            無効_詳細(s).背番号 = columns(0).Trim()

            For j = 1 To columns.Length - 1
                無効_詳細(s).ジャッジ詳細(j).ジャッジ記号 = header(j).Trim()
                無効_詳細(s).ジャッジ詳細(j).無効FLAG = Integer.Parse(columns(j).Trim())
            Next j

        Next s

    End Sub

    Public Function 無効判定(背番号 As String, ジャッジ記号 As String) As Integer
        If 無効_詳細 Is Nothing Then
            Return 0
        End If
        For s As Integer = 1 To UBound(無効_詳細)
            If 無効_詳細(s).背番号 = 背番号 Then
                For j As Integer = 1 To UBound(無効_詳細(s).ジャッジ詳細)
                    If 無効_詳細(s).ジャッジ詳細(j).ジャッジ記号 = ジャッジ記号 Then
                        Return 無効_詳細(s).ジャッジ詳細(j).無効FLAG
                    End If
                Next j
            End If
        Next s
        Return 0
    End Function




    Public Class G_詳細_C
        Public Property 背番号 As String

        Public ジャッジ詳細() As G_ジャッジ詳細_C

        Sub New(ジャッジ数 As Integer)
            ReDim ジャッジ詳細(ジャッジ数)
            For j As Integer = 1 To ジャッジ数
                ジャッジ詳細(j) = New G_ジャッジ詳細_C
            Next j

        End Sub

        Public Class G_ジャッジ詳細_C

            Public Property ジャッジ記号 As String

            Public Property 無効FLAG As Integer   '0:有効 1:無効　　　'ジャッジ人数分


        End Class



    End Class
End Class
