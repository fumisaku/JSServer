Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class 団体集計方法_J

    '団体の各区分毎の集計方法を設定する。

    Public Property 集計方法名 As String
    Public Property 倍率 As Decimal
    Public Property 上位ポジション数 As Integer　　　'0の時は全て
    Public Property 点数重複加算 As Boolean  '1次予選の点数と2次予選の点数を重複して加算していく方式
    Public Property 同点_点数按分 As Boolean　'順位 同点時に点数を按分するか、順位点をそのまま付けるか


    Public Property 設定ラウンド数 As Integer

    Public ラウンド_J() As 団体集計方法_ラウンド_J


    Private ReadOnly filepath As String
    Sub New(filepath_)

        設定ラウンド数 = 0

        filepath = filepath_

    End Sub

    Public Sub ラウンド_J_初期化(設定ラウンド数_)

        設定ラウンド数 = 設定ラウンド数_

        ReDim ラウンド_J(設定ラウンド数)

        For r = 1 To 設定ラウンド数
            ラウンド_J(r) = New 団体集計方法_ラウンド_J
        Next r

    End Sub


    Public Sub JSON書き出し()


        Dim filename As String = "J_団体集計方法_J_" & 集計方法名 & ".json"


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)

        ''元のファイルに出力する
        Using writer = New System.IO.StreamWriter(filepath & "\" & filename, False, System.Text.Encoding.GetEncoding("shift-jis"))
            writer.WriteLine(jText)
        End Using

    End Sub

    Public Function Get_JSON文字列() As String


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)


        Return jText

    End Function

    Public Function JSON読み込み() As 団体集計方法_J

        Dim rc As 団体集計方法_J = Nothing

        Dim filename As String = "J_団体集計方法_J_" & 集計方法名 & ".json"


        ''JSON読み込み用
        Dim jText As String = String.Empty


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した


            ''ファイルからJSONを読み込む
            Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)


            jText = cReader.ReadToEnd


            rc = JsonConvert.DeserializeObject(Of 団体集計方法_J)(jText)




            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If


        Return rc
    End Function

    Public Function Get_素点(ラウンド名 As String, 総合順位 As Decimal, SC_J_区分結果 As SC_J_区分結果) As Decimal
        '最終進出ラウンド名と、総合順位を渡すと得点を返す


        Dim rc As Decimal = 0

        If ラウンド名 IsNot Nothing Then


            For r = 1 To 設定ラウンド数

                If Strings.Right(ラウンド_J(r).ラウンド名, 1) = "位" Then
                    If Strings.Replace(ラウンド_J(r).ラウンド名, "位", "") = CStr(総合順位) Then

                        Select Case ラウンド_J(r).方式
                            Case "固定点"
                                rc = ラウンド_J(r).順位点

                            Case "順位点"
                                rc = ラウンド_J(r).順位点 - 総合順位

                                If rc < ラウンド_J(r).最低点 Then
                                    rc = ラウンド_J(r).最低点
                                End If

                        End Select

                        r = 設定ラウンド数

                    End If

                ElseIf ラウンド_J(r).ラウンド名 = "準決勝の１つ前" And
                       ラウンド名 = SC_J_区分結果.準決勝の１つ前ラウンド名 Then

                    Select Case ラウンド_J(r).方式
                        Case "固定点"
                            rc = ラウンド_J(r).順位点

                        Case "順位点"
                            rc = ラウンド_J(r).順位点 - 総合順位

                            If rc < ラウンド_J(r).最低点 Then
                                rc = ラウンド_J(r).最低点
                            End If

                    End Select

                    r = 設定ラウンド数


                ElseIf ラウンド_J(r).ラウンド名 = "準決勝の２つ前" And
                        ラウンド名 = SC_J_区分結果.準決勝の２つ前ラウンド名 Then

                    Select Case ラウンド_J(r).方式
                        Case "固定点"
                            rc = ラウンド_J(r).順位点

                        Case "順位点"
                            rc = ラウンド_J(r).順位点 - 総合順位

                            If rc < ラウンド_J(r).最低点 Then
                                rc = ラウンド_J(r).最低点
                            End If

                    End Select

                    r = 設定ラウンド数


                ElseIf ラウンド_J(r).ラウンド名 = ラウンド名 Then

                    Select Case ラウンド_J(r).方式
                        Case "固定点"
                            rc = ラウンド_J(r).順位点

                        Case "順位点"
                            rc = ラウンド_J(r).順位点 - 総合順位

                            If rc < ラウンド_J(r).最低点 Then
                                rc = ラウンド_J(r).最低点
                            End If

                    End Select


                    r = 設定ラウンド数

                End If
            Next r


        End If


        rc = rc * 倍率

        Return rc

    End Function


End Class

Public Class 団体集計方法_ラウンド_J

    Public Property ラウンド名 As String

    Public Property 方式 As String　　'"固定" or "順位点"

    Public Property 順位点 As Decimal   '順位点ー順位

    Public Property 最低点 As Decimal   '順位が低くても、該当ラウンドに進出した場合最低この点数を獲得する。



End Class
