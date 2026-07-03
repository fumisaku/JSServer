Imports System.IO

Public Class J_新審判設定

    Public PCS設定(10) As J_PCS設定
    Public 減点設定(20) As J_減点設定
    Public PCS担当() As J_PCS担当
    Public 既定種目順(10) As J_既定種目順

    '課題フィガー用
    Private 課題設定(30) As J_課題設定
    'Private 課題設定(30) As String　'フィガー名
    Public TES設定(3)    'Base点
    Public TES減点設定(10) As J_TES減点設定


    Public 同点処理 As String    '1:同点処理有  0:同点処理なし(同点とする）
    Public 勝敗方式 As String    'ブレイキン用　P:ポイント制　　R:ラウンド制 
    Public 勝敗ラウンド数 As Integer  'ブレイキン用　何ラウンド制か？

    Public 新審判基準VER As String

    Public PCS担当_設定数 As Integer

    Private ReadOnly filepath As String

    Const File頭文字列 = "MJ_"

    ''' コンストラクタ
    ''' 
    Sub New(filepath_)



        filepath = filepath_

        'FileRead()

        '初期値の設定
        勝敗方式 = "P"

    End Sub


    '' ********  メソッド *************
    ''
    Public Sub Set_新審判基準VER(_新審判基準VER As String)

        新審判基準VER = _新審判基準VER
        FileRead()


    End Sub

    '****** Public メソッド *******

    'PCS名を渡すとPCS番号を返す
    Public Function GetPCS番号(PCS名 As String) As Integer
        Dim rc As Integer = 0

        Dim i As Integer

        For i = 1 To UBound(PCS設定)
            If PCS設定(i).PCS項目名 = PCS名 Then
                rc = i
                i = UBound(PCS設定)
            End If

        Next i

        Return rc

    End Function

    'PCS数を返す
    Public Function GetPCS数() As Integer
        Dim rc As Integer = 0
        For i = 1 To UBound(PCS設定)
            If PCS設定(i) IsNot Nothing Then
                rc = rc + 1
            End If
        Next i

        Return rc
    End Function

    'PCS数を返す
    Public Function Get減点項目数() As Integer
        Dim rc As Integer = 0
        For i = 1 To UBound(減点設定)
            If 減点設定(i) IsNot Nothing Then
                rc = rc + 1
            End If
        Next i

        Return rc
    End Function

    '種目記号を渡すと課題数数を返す
    Public Function Get課題数(種目記号 As String) As Integer

        Dim rc As Integer = 0

        If 種目記号 = "" Then
            '種目記号が""の時は、MAX課題件数を返す。

            Dim 課題数L(10) As Integer
            Dim 種目記号L(10) As String

            For i = 1 To UBound(課題設定)

                If 課題設定(i) IsNot Nothing Then

                    Dim find_flag = False
                    For f = 1 To UBound(種目記号L)
                        If 課題設定(i).種目記号 = 種目記号L(f) Then
                            課題数L(f) = 課題数L(f) + 1
                            f = UBound(種目記号L)
                            find_flag = True
                        End If
                    Next f

                    If find_flag = False Then
                        For f = 1 To UBound(種目記号L)
                            If 課題数L(f) = 0 Then
                                課題数L(f) = 1
                                種目記号L(f) = 課題設定(i).種目記号
                                f = UBound(種目記号L)
                            End If
                        Next f
                    End If
                End If
            Next i

            Dim MAX課題数 As Integer = 0
            For f = 1 To UBound(種目記号L)
                If MAX課題数 < 課題数L(f) Then
                    MAX課題数 = 課題数L(f)
                End If
            Next f

            rc = MAX課題数

        Else
            For i = 1 To UBound(課題設定)

                If 課題設定(i) IsNot Nothing Then
                    If 課題設定(i).種目記号 = 種目記号 Then
                        rc = rc + 1
                    End If
                End If

            Next i
        End If


        Return rc
    End Function

    Public Function Getフィガー名(種目記号 As String, 課題番号 As Integer)

        Dim rc As String = ""

        Dim count As Integer = 0
        For i = 1 To UBound(課題設定)
            If 課題設定(i) IsNot Nothing Then
                If 課題設定(i).種目記号 = 種目記号 Then

                    count = count + 1

                    If count = 課題番号 Then

                        rc = 課題設定(i).フィガー名
                        i = UBound(課題設定)
                    End If
                End If
            End If
        Next i


        Return rc

    End Function


    Public Function GetTES減点数() As Integer
        Dim rc As Integer = 0
        For i = 1 To UBound(TES減点設定)
            If TES減点設定(i) IsNot Nothing Then
                rc = rc + 1
            End If
        Next i

        Return rc
    End Function


    'SL区分と種目番号を渡すと 既定種目順Classを返す
    Public Function Get既定種目順C(SL区分 As String, 種目番号 As Integer) As J_既定種目順
        Dim rc As J_既定種目順 = Nothing

        For i = 1 To UBound(既定種目順)
            If 既定種目順(i).SL区分 = SL区分 And 既定種目順(i).種目番号 = 種目番号 Then

                rc = 既定種目順(i)
                i = UBound(既定種目順)

            End If

        Next i

        Return rc

    End Function





    '****** Private メソッド *******


    ''' 読込み
    ''' 
    Private Sub FileRead()

        Dim filename As String

        filename = File頭文字列 & 新審判基準VER & ".csv"

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない

            MsgBox(filename & "が存在しません。")
        Else
            'ファイルが存在した


            '初期化　ここから
            ReDim PCS設定(10)
            ReDim 減点設定(20)
            ' PCS担当() 
            ReDim 既定種目順(10)

            '課題フィガー用
            ReDim 課題設定(30)
            ReDim TES設定(3)    'Base点
            ReDim TES減点設定(10)


            '初期化　ここまで


            ' StreamReader の新しいインスタンスを生成する
            Using stream As New FileStream(filepath & "\" & filename, FileMode.Open, FileAccess.Read)

                Dim cReader As New System.IO.StreamReader(stream, System.Text.Encoding.Default)

                ' 読み込んだ結果をすべて格納するための変数を宣言する
                Dim stResult() As String

                '全行を取得し改行で区切り
                stResult = Split(cReader.ReadToEnd, vbCrLf)

                '行数を取得
                Dim 行数 As Integer = stResult.Count - 1

                ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
                cReader.Close()

                Dim i, j, p As Integer
                Dim stTemp

                Dim k As Integer = 0
                Dim kk As Integer = 0
                Dim TES課題NO As Integer = 0

                For i = 0 To 行数
                    Select Case stResult(i)
                        Case "[PCS設定]"
                            p = 1
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then

                                    stTemp = Split(stResult(j), ",")
                                    If IsNumeric(stTemp(0)) Then

                                        '1桁目はPCS番号
                                        p = stTemp(0)

                                        Me.PCS設定(p) = New J_PCS設定 With {
                                            .PCS項目名 = stTemp(1),
                                            .倍率 = stTemp(2)
                                        }

                                        If UBound(stTemp) >= 3 Then
                                            Me.PCS設定(p).PCS説明1 = stTemp(3)
                                        End If

                                        If Strings.Left(新審判基準VER, 4) = "BJS2" Or Strings.Left(新審判基準VER, 4) = "BJS3" Then
                                            If UBound(stTemp) >= 4 Then
                                                Me.PCS設定(p).PCS最大値 = stTemp(4)
                                            End If
                                        Else
                                            If UBound(stTemp) >= 4 Then
                                                Me.PCS設定(p).PCS説明2 = stTemp(4)
                                            End If
                                        End If

                                        If UBound(stTemp) >= 5 Then
                                            Me.PCS設定(p).PCS説明3 = stTemp(5)
                                        End If
                                        If UBound(stTemp) >= 6 Then
                                            Me.PCS設定(p).PCS説明4 = stTemp(6)
                                        End If
                                        If UBound(stTemp) >= 7 Then
                                            Me.PCS設定(p).PCS説明5 = stTemp(7)
                                        End If

                                    End If

                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[担当PCS]"
                            PCS担当_設定数 = 0

                            ReDim PCS担当(行数)
                            p = 1
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then

                                    stTemp = Split(stResult(j), ",")
                                    If IsNumeric(stTemp(0)) Then

                                        PCS担当(p) = New J_PCS担当
                                        PCS担当(p).Solo_Flag = CInt(stTemp(0))
                                        PCS担当(p).Group_Flag = CInt(stTemp(1))
                                        PCS担当(p).Duel_Flag = CInt(stTemp(2))

                                        PCS担当(p).担当PCS = stTemp(3)

                                        p = p + 1

                                        PCS担当_設定数 = PCS担当_設定数 + 1

                                    End If

                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j



                        Case "[減点設定]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then

                                    stTemp = Split(stResult(j), ",")
                                    If IsNumeric(stTemp(0)) Then

                                        '1桁目は減点番号
                                        p = stTemp(0)
                                        If UBound(stTemp) >= 6 Then

                                            Me.減点設定(p) = New J_減点設定 With {
                                                .減点項目名 = stTemp(1),
                                                .減点項目名英 = stTemp(2),
                                                .SGM種別 = stTemp(3),
                                                .最初の減点 = stTemp(4),
                                                .最大値 = stTemp(5),
                                                .ステップ = stTemp(6)
                                            }
                                        End If

                                    End If

                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[既定種目順]"
                            Dim t As Integer = 1
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then

                                    stTemp = Split(stResult(j), ",")
                                    If stTemp(0) = "S" Or stTemp(0) = "L" Then

                                        既定種目順(t) = New J_既定種目順
                                        既定種目順(t).SL区分 = stTemp(0)
                                        既定種目順(t).種目番号 = CInt(stTemp(1))
                                        既定種目順(t).種目記号 = stTemp(2)
                                        既定種目順(t).SG種別 = stTemp(3)

                                        t = t + 1
                                    End If

                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[同点処理]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then

                                    stTemp = Split(stResult(j), ",")
                                    同点処理 = stTemp(0)

                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[勝敗方式]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then

                                    stTemp = Split(stResult(j), ",")
                                    勝敗方式 = stTemp(0)

                                    If IsNumeric(stTemp(1)) Then
                                        勝敗ラウンド数 = CInt（stTemp(1)）
                                    Else
                                        MsgBox(filename & "に、ブレイキンの勝敗ラウンド数が設定されていません。")
                                    End If



                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[課題設定]"
                            k = 1
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then


                                    stTemp = Split(stResult(j), ",")


                                    '1桁目は種目記号　2桁目はフィガー名

                                    If UBound(stTemp) >= 1 Then

                                        Me.課題設定(k) = New J_課題設定 With {
                                               .種目記号 = stTemp(0),
                                               .フィガー名 = stTemp(1)
                                              }
                                    End If




                                    k = k + 1
                                    '課題設定(k) = stResult(j)


                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[TES設定]"

                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then

                                    kk = kk + 1
                                    TES設定(kk) = stResult(j)


                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j

                        Case "[TES減点設定]"
                            For j = i + 1 To 行数
                                If Left(stResult(j), 2) <> "//" And Left(stResult(j), 1) <> "[" And stResult(j) <> "" Then

                                    stTemp = Split(stResult(j), ",")

                                    '1桁目は減点番号

                                    If UBound(stTemp) >= 3 Then
                                        TES課題NO = TES課題NO + 1

                                        Me.TES減点設定(TES課題NO) = New J_TES減点設定 With {
                                              .減点略称 = stTemp(0),
                                               .初期値 = stTemp(1),
                                               .STEP値 = stTemp(2),
                                               .MAX値 = stTemp(3)
                                            }
                                    End If



                                ElseIf Left(stResult(j), 1) = "[" Then
                                    j = 行数
                                End If
                            Next j


                    End Select


                Next i


            End Using


        End If





    End Sub


End Class
