Public Class JS_Member_C

    '// エントリー情報

    Const Filename = "SSS__MEM.dat"

    Public 背番号()
    Public リーダー名_漢字()
    Public パートナー名_漢字()
    Public リーダー名_カナ()
    Public パートナー名_カナ()
    Public 所属()

    Public リーダー会員番号()
    Public パートナー会員番号()

    Public 出場区分(,)

    Sub Fileread(ByVal パス名)

        Dim buf As String
        'Dim LastRow As Long
        Dim FSO As Object

        'ファイルから読込み
        FSO = CreateObject("Scripting.FileSystemObject")
        With FSO.OpenTextFile(パス名 & "\" & Filename, 1)    '読込み専用
            buf = .ReadAll
            .Close()
        End With

        FSO = Nothing

        'NULLが含まれている場合はブランクに修正

        'Dim bufbr = System.Text.Encoding.GetEncoding("Shift_JIS").GetBytes(buf)

        'buf = buf.Replace(vbNullChar, " ")


        Dim リスト

        リスト = Split(buf, vbCrLf)

        Call データセット(リスト)

    End Sub

    Public Sub 背番号To(ByVal 背番号_, ByRef L漢字_, ByRef P漢字_, ByRef Lカナ_, ByRef Pカナ_, ByRef 所属_, ByRef 区分リスト_)

        L漢字_ = リーダー名_漢字(背番号_)
        Lカナ_ = リーダー名_カナ(背番号_)

        P漢字_ = パートナー名_漢字(背番号_)
        Pカナ_ = パートナー名_カナ(背番号_)

        所属_ = 所属(背番号_)

        Dim 区分リスト(), i
        ReDim 区分リスト(40)
        For i = 1 To 40
            区分リスト(i) = 出場区分(背番号_, i)
        Next i
        区分リスト_ = 区分リスト


    End Sub

    Public Sub 区分To(ByVal 区分No_, ByVal 背番号リスト_)

        ReDim 背番号リスト_(UBound(背番号))
        Dim Count, i

        Count = 1
        For i = 1 To UBound(背番号)
            If 出場区分(i, 区分No_) = "1" Then
                背番号リスト_(Count) = 背番号(i)
                Count = Count + 1
            End If
        Next i

    End Sub




    Private Sub データセット(ByVal リスト)

        ReDim 背番号(UBound(リスト))
        ReDim リーダー名_漢字(UBound(リスト))
        ReDim パートナー名_漢字(UBound(リスト))
        ReDim リーダー名_カナ(UBound(リスト))
        ReDim パートナー名_カナ(UBound(リスト))

        ReDim 所属(UBound(リスト))

        ReDim リーダー会員番号(UBound(リスト))
        ReDim パートナー会員番号(UBound(リスト))

        ReDim 出場区分(UBound(リスト), 40)
        Dim i, k As Integer

        For i = 1 To 1000
            背番号(i) = i
            リーダー名_漢字(i) = Trim(StrConv(LeftB(StrConv(リスト(i - 1), VbStrConv.None), 16), VbStrConv.None))
            パートナー名_漢字(i) = Trim(StrConv(MidB(StrConv(リスト(i - 1), VbStrConv.None), 17, 16), VbStrConv.None))

            If Trim(リーダー名_漢字(i)) = "*" And Trim(パートナー名_漢字(i)) = "*" Then
                '2人とも長い名前の時
                リーダー名_漢字(i) = Trim(StrConv(MidB(StrConv(リスト(i - 1), VbStrConv.None), 97, 32), VbStrConv.None))
                パートナー名_漢字(i) = Trim(StrConv(MidB(StrConv(リスト(i - 1), VbStrConv.None), 129, 32), VbStrConv.None))
            Else
                '1人だけ長い名前の時
                If Trim(リーダー名_漢字(i)) = "*" Then
                    リーダー名_漢字(i) = Trim(StrConv(MidB(StrConv(リスト(i - 1), VbStrConv.None), 97, 32), VbStrConv.None))
                End If
                If Trim(パートナー名_漢字(i)) = "*" Then
                    パートナー名_漢字(i) = Trim(StrConv(MidB(StrConv(リスト(i - 1), VbStrConv.None), 97, 32), VbStrConv.None))
                End If
            End If


            リーダー名_カナ(i) = Trim(StrConv(LeftB(StrConv(リスト(i + 1000 - 1), VbStrConv.None), 16), VbStrConv.None))
            パートナー名_カナ(i) = Trim(StrConv(MidB(StrConv(リスト(i + 1000 - 1), VbStrConv.None), 17, 16), VbStrConv.None))

            所属(i) = Trim(StrConv(MidB(StrConv(リスト(i - 1), VbStrConv.None), 33, 22), VbStrConv.None))

            リーダー会員番号(i) = Trim(StrConv(MidB(StrConv(リスト(i + 1000 - 1), VbStrConv.None), 33, 6), VbStrConv.None))
            パートナー会員番号(i) = Trim(StrConv(MidB(StrConv(リスト(i + 1000 - 1), VbStrConv.None), 43, 6), VbStrConv.None))


            For k = 1 To 40
                出場区分(i, k) = StrConv(MidB(StrConv(リスト(i - 1), VbStrConv.None), 56 + k, 1), VbStrConv.None)
            Next k

        Next i

    End Sub

    Public Sub Get_リーダー名_漢字(ByVal リーダー名_漢字_)
        リーダー名_漢字_ = リーダー名_漢字
    End Sub

    Public Sub Get_パートナー名_漢字(ByVal パートナー名_漢字_)
        パートナー名_漢字_ = パートナー名_漢字
    End Sub

    Public Sub Get_所属(ByVal 所属_)
        所属_ = 所属
    End Sub


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

        If stTarget <> "" Then

            Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
            Dim btBytes As Byte() = hEncoding.GetBytes(stTarget)

            Return hEncoding.GetString(btBytes, iStart - 1, iByteSize)

        Else
            Return ""
        End If
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
