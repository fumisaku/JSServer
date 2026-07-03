Public Class JS_Comp_C


    '// Competition情報

    Const Filename = "SSS__I.dat"

    Public 競技会NO
    Public 大会名称
    Public 開催日付
    Public 主催団体
    Public 開催会場
    Public 区分数
    Public Mod値

    Public 区分情報 As JS_Comp_区分_C
    Public 審判員情報 As JS_Comp_審判員_C


    Sub Fileread(ByVal パス名)

        Dim buf As String
        '   Dim LastRow As Long
        Dim FSO As Object
        Dim リスト

        'ファイルから読込み
        FSO = CreateObject("Scripting.FileSystemObject")
        With FSO.OpenTextFile(パス名 & "\" & Filename, 1)    '読込み専用
            buf = .ReadAll
            .Close()
        End With

        FSO = Nothing

        リスト = Split(buf, vbCrLf)

        Call データセット(リスト)

    End Sub



    Private Sub データセット(ByVal リスト)

        '====（１） 競技会情報===============================
        競技会NO = Trim(リスト(1))

        大会名称 = Trim(StrConv(LeftB(StrConv(リスト(2), VbStrConv.None), 60), VbStrConv.None))
        開催日付 = Trim(StrConv(LeftB(StrConv(リスト(3), VbStrConv.None), 60), VbStrConv.None))
        主催団体 = Trim(StrConv(LeftB(StrConv(リスト(4), VbStrConv.None), 60), VbStrConv.None))
        開催会場 = Trim(StrConv(LeftB(StrConv(リスト(5), VbStrConv.None), 60), VbStrConv.None))
        '区分数 = CInt(StrConv(MidB(StrConv(リスト(10), VbStrConv.None), 34, 2), VbStrConv.None))


        Dim i, k, c

        For i = 11 To 52
            If Trim(StrConv(LeftB(StrConv(リスト(i), VbStrConv.None), 2), VbStrConv.None)) = "//" Then
                区分数 = i - 11
                i = 52
            End If
        Next i


        '====（２） 区分情報===============================

        区分情報 = New JS_Comp_区分_C

        Call 区分情報.初期化()

        For i = 1 To 区分数
            Call 区分情報.Set_区分NO(i, i)
            Call 区分情報.Set_区分名(Trim(StrConv(LeftB(StrConv(リスト(10 + i), VbStrConv.None), 40), VbStrConv.None)), i)
            Call 区分情報.Set_SL区分(Trim(StrConv(MidB(StrConv(リスト(10 + i), VbStrConv.None), 41, 1), VbStrConv.None)), i)
            For k = 1 To 10
                Call 区分情報.Set_開催種目(StrConv(MidB(StrConv(リスト(10 + i), VbStrConv.None), 43 + k, 1), VbStrConv.None), i, k)
            Next k
            Call 区分情報.Set_区分名2(Trim(StrConv(MidB(StrConv(リスト(10 + i), VbStrConv.None), 60, 40), VbStrConv.None)), i)
            Call 区分情報.Set_リダンスFLAG(StrConv(MidB(StrConv(リスト(10 + i), VbStrConv.None), 55, 1), VbStrConv.None), i)
        Next i

        '====（３） ヒート数/ピックアップ数==================
        Dim r, ヒート数, UP数
        For i = 1 To 区分数
            For r = 1 To 7 'ラウンド数
                ヒート数 = CInt(StrConv(MidB(StrConv(リスト(11 + 区分数 + i), VbStrConv.None), 9 * (r - 1) + 2, 3), VbStrConv.None))
                Call 区分情報.Set_ヒート数(ヒート数, i, r)

                UP数 = CInt(StrConv(MidB(StrConv(リスト(11 + 区分数 + i), VbStrConv.None), 9 * (r - 1) + 7, 3), VbStrConv.None))
                Call 区分情報.Set_UP数(UP数, i, r)
            Next r
        Next i

        '====（４） 競技番号==================
        Dim 競技番号
        For i = 1 To 区分数
            For r = 1 To 7 'ラウンド数
                競技番号 = CInt(StrConv(MidB(StrConv(リスト(12 + 区分数 * 2 + i), VbStrConv.None), 4 * (r - 1) + 2, 3), VbStrConv.None))
                Call 区分情報.Set_競技番号_区分(競技番号, i, r)
            Next r
        Next i

        '====（５）担当審判チーム==================
        For i = 1 To 区分数
            For r = 1 To 7 'ラウンド数
                競技番号 = CInt(StrConv(MidB(StrConv(リスト(39 + 区分数 * 3 + i), VbStrConv.None), 4 * (r - 1) + 2, 3), VbStrConv.None))
                Call 区分情報.Set_担当審判チーム(競技番号 + 1, i, r)
            Next r
        Next i

        '====（６）区分記号==================
        Dim 区分記号
        For i = 1 To 区分数
            区分記号 = StrConv(MidB(StrConv(リスト(40 + 区分数 * 4 + i), VbStrConv.None), 2, 4), VbStrConv.None)
            Call 区分情報.Set_区分記号(区分記号, i)
        Next i




        '====（１０）審判員情報==================

        審判員情報 = New JS_Comp_審判員_C
        Call 審判員情報.初期化()

        For i = 1 To 50

            Call 審判員情報.Set_審判員No(i, i)

            Dim 審判員記号
            審判員記号 = Trim(StrConv(MidB(StrConv(リスト(56 + 区分数 * 5 + i), VbStrConv.None), 1, 2), VbStrConv.None))
            If 審判員記号 <> "" Then
                Call 審判員情報.Set_審判記号(審判員記号, i)
            End If

            Dim 審判員名
            審判員名 = Trim(StrConv(MidB(StrConv(リスト(56 + 区分数 * 5 + i), VbStrConv.None), 3, 16), VbStrConv.None))
            Call 審判員情報.Set_審判員名(審判員名, i)

            Dim 審判表示名
            審判表示名 = Trim(StrConv(MidB(StrConv(リスト(56 + 区分数 * 5 + i), VbStrConv.None), 44, 6), VbStrConv.None))
            Call 審判員情報.Set_審判員表示名(審判表示名, i)

            Dim 担当チーム
            For c = 0 To 24
                担当チーム = StrConv(MidB(StrConv(リスト(56 + 区分数 * 5 + i), VbStrConv.None), 19 + c, 1), VbStrConv.None)
                Call 審判員情報.Set_審判員担当チーム(担当チーム, i, c + 1)
            Next c

        Next i

        '====（１１）Mod情報==================
        Mod値 = 1000
        For i = 56 + 区分数 * 5 To UBound(リスト)
            If リスト(i) = "//- マルチ番号設定" Then
                Mod値 = CInt(リスト(i + 1))
                i = UBound(リスト)
            End If
        Next i



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