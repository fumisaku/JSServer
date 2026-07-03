Public Class JS_DFile_C

    '// Dファイル

    Public 区分
    Public ラウンドNO
    Public ラウンド名
    Public 種目数
    Private 種目リスト(10)
    Public 審判員数

    Private ヒート     '// 選手数分
    Private 背番号リスト     '// 選手数分
    Private 詳細点数   '// 選手数分　×　種目数
    Private 合計点数   '// 選手数分
    Private 順位       '// 選手数分


    Public スケーティングFLAG


    Private Sub データセット(リスト)

        Dim 行数 = UBound(リスト)

        スケーティングFLAG = 0

        ReDim ヒート(行数)
        ReDim 背番号リスト(行数)
        ReDim 合計点数(行数)
        ReDim 順位(行数)

        '====（１）種目数/審判員数============
        種目数 = 0
        For d = 1 To 10
            種目リスト(d) = Trim(StrConv(MidB(StrConv(リスト(0), VbStrConv.None), 14 + d, 1), VbStrConv.None))

            If 種目リスト(d) = "1" Then
                種目数 = 種目数 + 1
            End If
        Next d

        ReDim 詳細点数(行数, 種目数)

        If リスト(0).ToString.Length = 28 Then
            審判員数 = CInt(StrConv(MidB(StrConv(リスト(0), VbStrConv.None), 28, 1), VbStrConv.None))


        ElseIf リスト(0).ToString.Length = 29 Then

            審判員数 = CInt(StrConv(MidB(StrConv(リスト(0), VbStrConv.None), 28, 2), VbStrConv.None))


        End If



        '====（２）詳細点数============
        For i = 1 To UBound(リスト) - 1
            背番号リスト(i) = CInt(StrConv(MidB(StrConv(リスト(i), VbStrConv.None), 1, 3), VbStrConv.None))



            If 背番号リスト(i) <> 0 Then
                合計点数(i) = 0
                ヒート(i) = ((i - 1) \ 20) + 1
                For d = 1 To 種目数
                    詳細点数(i, d) = StrConv(MidB(StrConv(リスト(i), VbStrConv.None), 5 + (d - 1) * (審判員数 + 1), 審判員数), VbStrConv.None)

                    For s = 1 To 審判員数
                        If StrConv(MidB(StrConv(リスト(i), VbStrConv.None), 5 + (d - 1) * (審判員数 + 1) + (s - 1), 1), VbStrConv.None) = "1" Then

                            合計点数(i) = 合計点数(i) + 1
                        End If

                        If StrConv(MidB(StrConv(リスト(i), VbStrConv.None), 5 + (d - 1) * (審判員数 + 1) + (s - 1), 1), VbStrConv.None) = "2" Then


                            スケーティングFLAG = 1
                        End If
                    Next s
                Next d
            End If
        Next i

        '====（３）順位====================
        Dim 順位_1 = 1
        Dim 人数 = 0
        For p = 種目数 * 審判員数 To 0 Step -1
            For i = 1 To UBound(順位)
                If 合計点数(i) = p And 背番号リスト(i) <> 0 Then
                    順位(i) = 順位_1
                    人数 = 人数 + 1
                End If
            Next i

            順位_1 = 人数 + 1
        Next p



        '====（４）スケーティング =========





    End Sub


    Function Fileread(パス名, 区分NO, ラウンドNO_)

        Dim buf As String
        Dim FSO As Object
        Dim リスト

        Dim rc As Integer = 0

        区分 = 区分NO
        ラウンドNO = ラウンドNO_

        Dim Filename As String = "D__" & 区分NO & "_" & ラウンドNO_ & ".Dat"
        '区分NO 01, 02 ...
        'ラウンドNO_ 1,2,3 ... 16...

        If Dir(パス名 & "\" & Filename) <> "" Then  'ファイルが存在しない場合は無視

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

            RC = 1  'ファイル無し
        End If

        Fileread = RC

    End Function


    Public Sub Get_種目リスト(種目リスト_)
        種目リスト_ = 種目リスト
    End Sub

    Public Sub Get_背番号(ByRef 背番号_)
        背番号_ = 背番号リスト
    End Sub
    Public Sub Get_合計点数(合計点数_)
        合計点数_ = 合計点数
    End Sub
    Public Sub Get_順位(順位_)
        順位_ = 順位
    End Sub

    Public Function Get_詳細点数(背番号, 種目番号, ジャッジ番号)

        Dim rc As Integer = 0

        Dim No As Integer = 0
        For s = 1 To UBound(背番号リスト)
            If 背番号リスト(s) = 背番号 Then
                No = s
                s = UBound(背番号リスト)
            End If
        Next s

        If No > 0 Then

            Dim 文字列 = MidB(詳細点数(No, 種目番号), ジャッジ番号, 1)


            If 文字列 = " " Or 文字列 = "" Then
                rc = 0
            Else
                rc = CInt(文字列)
            End If


        End If


        Return rc

    End Function





    '///// バイト単位での文字列切り出し関数

    '//'' -----------------------------------------------------------------------------------------
    '//'' <summary>
    '//''     文字列の左端から指定したバイト数分の文字列を返します。</summary>
    '//'' <param name="stTarget">
    '//''     取り出す元になる文字列。<param>
    '//'' <param name="iByteSize">
    '//''     取り出すバイト数。</param>
    '//' <returns>
    '//''     左端から指定されたバイト数分の文字列。</returns>
    ''' -----------------------------------------------------------------------------------------
    Public Shared Function LeftB(ByVal stTarget As String, ByVal iByteSize As Integer) As String
        Return MidB(stTarget, 1, iByteSize)
    End Function


    ''' -----------------------------------------------------------------------------------------
    ''' <summary>
    '''     文字列の指定されたバイト位置以降のすべての文字列を返します。</summary>
    ''' <param name="stTarget">
    '''     取り出す元になる文字列。</param>
    ''' <param name="iStart">
    '''     取り出しを開始する位置。</param>
    ''' <returns>
    '''     指定されたバイト位置以降のすべての文字列。</returns>
    ''' -----------------------------------------------------------------------------------------
    Public Shared Function MidB(ByVal stTarget As String, ByVal iStart As Integer) As String
        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Dim btBytes As Byte() = hEncoding.GetBytes(stTarget)

        Return hEncoding.GetString(btBytes, iStart - 1, btBytes.Length - iStart + 1)
    End Function

    ''' -----------------------------------------------------------------------------------------
    ''' <summary>
    '''     文字列の指定されたバイト位置から、指定されたバイト数分の文字列を返します。</summary>
    ''' <param name="stTarget">
    '''     取り出す元になる文字列。</param>
    ''' <param name="iStart">
    '''     取り出しを開始する位置。</param>
    ''' <param name="iByteSize">
    '''     取り出すバイト数。</param>
    ''' <returns>
    '''     指定されたバイト位置から指定されたバイト数分の文字列。</returns>
    ''' -----------------------------------------------------------------------------------------
    Public Shared Function MidB _
    (ByVal stTarget As String, ByVal iStart As Integer, ByVal iByteSize As Integer) As String
        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Dim btBytes As Byte() = hEncoding.GetBytes(stTarget)

        Return hEncoding.GetString(btBytes, iStart - 1, iByteSize)
    End Function


    ''' -----------------------------------------------------------------------------------------
    ''' <summary>
    '''     文字列の右端から指定されたバイト数分の文字列を返します。</summary>
    ''' <param name="stTarget">
    '''     取り出す元になる文字列。</param>
    ''' <param name="iByteSize">
    '''     取り出すバイト数。</param>
    ''' <returns>
    '''     右端から指定されたバイト数分の文字列。</returns>
    ''' -----------------------------------------------------------------------------------------
    Public Shared Function RightB(ByVal stTarget As String, ByVal iByteSize As Integer) As String
        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Dim btBytes As Byte() = hEncoding.GetBytes(stTarget)

        Return hEncoding.GetString(btBytes, btBytes.Length - iByteSize, iByteSize)
    End Function



End Class
