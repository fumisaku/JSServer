
Public Class JS_Comp_区分_C


    Public 区分NO
    Public 区分名
    Public SL区分
    Public 開催種目
    Public 区分名2
    Public リダンスFLAG


    Private ヒート数(,)
    Private UP数

    Public 競技番号_区分

    Public 担当審判チーム

    Public 区分記号


    Const MAX区分数 = 40
    Const MAXラウンド数 = 7

    Public Sub 初期化()

        ReDim 区分NO(MAX区分数)
        ReDim 区分名(MAX区分数)
        ReDim SL区分(MAX区分数)
        ReDim 開催種目(MAX区分数, 10)  '10種目 WTVFQSCRPJ
        ReDim 区分名2(MAX区分数)
        ReDim リダンスFLAG(MAX区分数)

        ReDim ヒート数(MAX区分数, MAXラウンド数)
        ReDim UP数(MAX区分数, MAXラウンド数)

        ReDim 競技番号_区分(MAX区分数, MAXラウンド数)

        ReDim 担当審判チーム(MAX区分数, MAXラウンド数)

        ReDim 区分記号(MAX区分数)

    End Sub

    Public Function Getヒート数(区分NO As Integer, ラウンドNO As Integer, 支援元PATH As String)


        Dim rc As Integer = 0

        If ラウンドNO <= 9 Then

            If UBound(ヒート数, 2) >= ラウンドNO Then

                rc = ヒート数(区分NO, ラウンドNO)

            End If
        Else
            '同点決勝がある場合

            Dim JS_HFile = New JS_HFile_C()
            If JS_HFile.Fileread(支援元PATH, String.Format("{0:D2}", 区分NO), ラウンドNO) = 0 Then
                'Hファイルがあった時

                rc = JS_HFile.ヒート数

            End If


        End If



        Return rc

    End Function

    Public Function GetUP予定数(区分NO As Integer, ラウンドNO As Integer)
        Dim rc As Integer = 0

        rc = UP数(区分NO, ラウンドNO)

        Return rc

    End Function



    Public Sub Set_区分NO(ByVal 区分No_, ByVal No)
        区分NO(No) = 区分No_
    End Sub

    Public Sub Set_区分名(ByVal 区分名_, ByVal No)
        区分名(No) = 区分名_
    End Sub

    Public Sub Set_SL区分(ByVal SL区分_, ByVal No)
        SL区分(No) = SL区分_
    End Sub

    Public Sub Set_開催種目(ByVal 開催種目_, ByVal No, ByVal 種目番号)
        開催種目(No, 種目番号) = 開催種目_
    End Sub

    Public Sub Set_区分名2(ByVal 区分名2_, ByVal No)
        区分名2(No) = 区分名2_
    End Sub
    Public Sub Set_リダンスFLAG(ByVal リダンスFLAG_, ByVal No)
        リダンスFLAG(No) = リダンスFLAG_
    End Sub



    Public Sub Set_ヒート数(ByVal ヒート数_, ByVal No, ByVal ラウンドNO)

        If ラウンドNO <= 10 Then
            ヒート数(No, ラウンドNO) = ヒート数_
        End If

    End Sub

    Public Sub Set_UP数(ByVal UP数_, ByVal No, ByVal ラウンドNO)
        If ラウンドNO <= 10 Then
            UP数(No, ラウンドNO) = UP数_
        End If

    End Sub

    Public Sub Set_競技番号_区分(ByVal 競技番号_区分_, ByVal No, ByVal ラウンドNO)

        If ラウンドNO <= 10 Then
            競技番号_区分(No, ラウンドNO) = 競技番号_区分_
        End If

    End Sub

    Public Sub Set_担当審判チーム(ByVal 担当審判チーム_, ByVal No, ByVal ラウンドNO)

        If ラウンドNO <= 10 Then
            担当審判チーム(No, ラウンドNO) = 担当審判チーム_
        Else
            '同点決勝の時は、本選と同じ審判チーム
            'Dim RNO As String = Strings.Right(CStr(ラウンドNO), 1)

            '担当審判チーム(No, RNO) = 担当審判チーム_

        End If
    End Sub


    Public Sub Set_区分記号(ByVal 区分記号_, ByVal No)
        区分記号(No) = 区分記号_
    End Sub


End Class

