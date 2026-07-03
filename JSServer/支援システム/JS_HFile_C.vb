Public Class JS_HFile_C


    '// ヒート情報
    Private ヒート表
    Public ヒート数
    Public 出場組数

    Private ヒート表_背番号順


    Public Function Fileread(パス名 As String, 区分NO As String, ラウンドNO_ As String)

        Dim buf As String
        Dim FSO As Object
        Dim リスト

        Dim RC As Integer = 0

        Dim Filename As String = "H_" & 区分NO & "_" & ラウンドNO_ & ".Dat"

        If Dir(パス名 & "\" & Filename) <> "" Then

            'ファイルから読込み
            FSO = CreateObject("Scripting.FileSystemObject")
            With FSO.OpenTextFile(パス名 & "\" & Filename, 1)    '読込み専用
                buf = .ReadAll
                .Close()
            End With

            FSO = Nothing

            リスト = Split(buf, vbCrLf)

            Call データセット(リスト)
            RC = 0

        Else

            RC = 1
        End If

        Return RC


    End Function




    Private Sub データセット(リスト)

        ヒート数 = UBound(リスト)

        ReDim ヒート表(ヒート数, 20)

        出場組数 = 0

        For h = 0 To ヒート数 - 1
            For i = 1 To 20
                ヒート表(h + 1, i) = Trim(Mid(リスト(h), 4 * i - 2, 3))

                If ヒート表(h + 1, i) <> "" Then
                    出場組数 = 出場組数 + 1
                End If

            Next i
        Next h

        ヒート表変換(）

    End Sub

    Public Sub Getヒート表(ByRef ヒート表_)

        ヒート表_ = ヒート表

    End Sub

    Public Sub Getヒート表_背番号順(ByRef ヒート表_背番号順_)

        ヒート表_背番号順_ = ヒート表_背番号順

    End Sub

    Private Sub ヒート表変換()
        'ヒート表（ヒート数、番号）を、ヒート表_背番号順(番号,ヒート番号）　に変換する。

        'ヒートシャッフルには対応していない。

        Dim ヒート表TEMP
        ReDim ヒート表TEMP(ヒート数, 20)

        For h = 0 To ヒート数
            For s = 0 To 20
                ヒート表TEMP(h, s) = ヒート表(h, s)
            Next s
        Next h



        ReDim ヒート表_背番号順(出場組数, 2)

        Dim ss As Integer = 0

        For 選手 = 1 To 出場組数

            Dim 最小背番号 As Integer = 1000
            Dim 最小背番号_ヒート番号 As Integer = 0
            Dim 最小背番号_選手番号 As Integer = 0

            For h = 1 To ヒート数
                For s = 1 To 20
                    If ヒート表TEMP(h, s) <> "" Then
                        If CInt(ヒート表TEMP(h, s)） < 最小背番号 Then
                            最小背番号 = ヒート表TEMP(h, s)
                            最小背番号_ヒート番号 = h
                            最小背番号_選手番号 = s
                        End If
                    End If
                Next s
            Next h


            If 最小背番号 < 1000 Then
                ss = ss + 1
                ヒート表_背番号順(ss, 0) = 最小背番号
                ヒート表_背番号順(ss, 1) = 最小背番号_ヒート番号

                ヒート表TEMP(最小背番号_ヒート番号, 最小背番号_選手番号) = "1000"
            End If

        Next 選手

    End Sub






End Class
