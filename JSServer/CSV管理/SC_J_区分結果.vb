Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class SC_J_区分結果


    Private ReadOnly filepath As String
    Public Property 選手数 As Integer
    Public Property ラウンド数 As Integer

    Public SC_J_区分結果_選手() As SC_J_区分結果_選手

    Public SC_J_区分結果_ラウンド設定() As SC_J_区分結果_ラウンド設定

    Public Property 区分番号 As String
    Public Property 区分名 As String

    Public Property 準決勝の１つ前ラウンド名 As String
    Public Property 準決勝の２つ前ラウンド名 As String



    Private ラウンド番号一覧() As String


    Sub New(filepath_)

        選手数 = 0

        filepath = filepath_

        ReDim ラウンド番号一覧(19)
        ラウンド番号一覧(1) = "010"
        ラウンド番号一覧(2) = "01R"
        ラウンド番号一覧(3) = "011"
        ラウンド番号一覧(4) = "020"
        ラウンド番号一覧(5) = "021"
        ラウンド番号一覧(6) = "030"
        ラウンド番号一覧(7) = "031"
        ラウンド番号一覧(8) = "040"
        ラウンド番号一覧(9) = "041"
        ラウンド番号一覧(10) = "050"
        ラウンド番号一覧(11) = "051"
        ラウンド番号一覧(12) = "090"
        ラウンド番号一覧(13) = "091"
        ラウンド番号一覧(14) = "100"
        ラウンド番号一覧(15) = "101"
        ラウンド番号一覧(16) = "200"
        ラウンド番号一覧(17) = "201"
        ラウンド番号一覧(18) = "300"
        ラウンド番号一覧(19) = "400"



    End Sub

    Public Function Get_ラウンド番号一覧() As Array

        Return ラウンド番号一覧

    End Function

    Public Sub 集計(区分番号_ As String)

        '区分毎の集計

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Me.区分番号 = 区分番号_
        Me.区分名 = マスタデータ.B_区分マスタ.Get区分表記名(区分番号)

        Dim 選手M番号 As String = マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ

        'ラウンド数参集つ
        ラウンド数 = 0
        ReDim SC_J_区分結果_ラウンド設定(19)
        For r = 1 To 19
            SC_J_区分結果_ラウンド設定(r) = New SC_J_区分結果_ラウンド設定
        Next r



        '選手数を算出
        選手数 = マスタデータ.選手マスタ.Get_出場選手数(区分番号)

        'For i = 1 To UBound(マスタデータ.選手マスタ.選手リスト)
        'If マスタデータ.選手マスタ.選手リスト(i) IsNot Nothing Then
        'If マスタデータ.選手マスタ.選手リスト(i).List番号 = 選手M番号 Then
        '選手数 = 選手数 + 1
        'End If
        'End If
        'Next i




        '選手数を基に子クラスを作成
        ReDim SC_J_区分結果_選手(選手数)
        For i = 1 To 選手数
            SC_J_区分結果_選手(i） = New SC_J_区分結果_選手(UBound(ラウンド番号一覧))
        Next i

        Dim 連番 As Integer = 0

        'ラウンド毎のの集計

        For r = UBound(ラウンド番号一覧) To 1 Step -1
            連番 = ラウンド集計(選手M番号, ラウンド番号一覧(r), 連番)
        Next r




        'ヒート表だけ作成されている場合に、点数を０点にして、進出ラウンドを更新する。
        For r = 1 To ラウンド数
            '採点済みがFlaseで、
            If SC_J_区分結果_ラウンド設定(r).採点済みFLAG = False Then

                'ヒート表が作成されているか？
                If マスタデータ.E_ヒート表マスタ.FileCheck(区分番号, SC_J_区分結果_ラウンド設定(r).ラウンド番号) Then
                    '採点はされていないが、ヒート表は作成されている状態

                    マスタデータ.E_ヒート表マスタ.Read(区分番号, SC_J_区分結果_ラウンド設定(r).ラウンド番号)
                    Dim 背番号リスト() = Nothing
                    マスタデータ.E_ヒート表マスタ.Get_背番号リスト(1, 0, 背番号リスト)

                    Dim rIND As Integer = 0
                    For rr = 1 To UBound(ラウンド番号一覧)
                        If ラウンド番号一覧(rr) = SC_J_区分結果_ラウンド設定(r).ラウンド番号 Then
                            rIND = rr
                            rr = UBound(ラウンド番号一覧)
                        End If
                    Next rr


                    For s = 1 To 選手数

                        For hs = 1 To UBound(背番号リスト)
                            If SC_J_区分結果_選手(s).背番号 = 背番号リスト(hs) Then

                                SC_J_区分結果_選手(s).ラウンド = SC_J_区分結果_ラウンド設定(r).ラウンド名

                                SC_J_区分結果_選手(s).SC_J_区分結果_選手_ラウンド結果(rIND).順位 = 1
                                SC_J_区分結果_選手(s).SC_J_区分結果_選手_ラウンド結果(rIND).得点 = 0
                                SC_J_区分結果_選手(s).SC_J_区分結果_選手_ラウンド結果(rIND).ラウンド番号 = ラウンド番号一覧(rIND)
                                SC_J_区分結果_選手(s).SC_J_区分結果_選手_ラウンド結果(rIND).ラウンド名 = マスタデータ.Get_ラウンド名(ラウンド番号一覧(rIND))

                                hs = UBound(背番号リスト)
                            End If
                        Next hs


                    Next s

                End If
            Else
                r = ラウンド数
            End If
        Next r


        '準決勝の1つ前のラウンドを探す

        For r = 1 To 19
            If Strings.Right(SC_J_区分結果_ラウンド設定(r).ラウンド番号, 1) = "0" And
               Strings.Left(SC_J_区分結果_ラウンド設定(r).ラウンド番号, 1) <> "4" And Strings.Left(SC_J_区分結果_ラウンド設定(r).ラウンド番号, 1) <> "3" And Strings.Left(SC_J_区分結果_ラウンド設定(r).ラウンド番号, 1) <> "2" Then

                準決勝の１つ前ラウンド名 = マスタデータ.Get_ラウンド名(SC_J_区分結果_ラウンド設定(r).ラウンド番号）
                r = 19

            End If
        Next r

        '準決勝の２つ前のラウンドを探す

        For r = 1 To 19
            If Strings.Right(SC_J_区分結果_ラウンド設定(r).ラウンド番号, 1) = "0" And
               Strings.Left(SC_J_区分結果_ラウンド設定(r).ラウンド番号, 1) <> "4" And Strings.Left(SC_J_区分結果_ラウンド設定(r).ラウンド番号, 1) <> "3" And Strings.Left(SC_J_区分結果_ラウンド設定(r).ラウンド番号, 1) <> "2" And
              準決勝の１つ前ラウンド名 <> SC_J_区分結果_ラウンド設定(r).ラウンド名 Then

                準決勝の２つ前ラウンド名 = マスタデータ.Get_ラウンド名(SC_J_区分結果_ラウンド設定(r).ラウンド番号)
                r = 19

            End If
        Next r



        マスタデータ = Nothing


    End Sub

    Private Function ラウンド集計(選手M番号 As String, ラウンド番号 As String, 連番 As Integer) As Integer

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Dim ラウンド名 = マスタデータ.Get_ラウンド名(ラウンド番号)

        マスタデータ = Nothing


        Dim 採点結果 As 採点結果_C
        採点結果 = New 採点結果_C(区分番号, ラウンド番号)

        Dim 採点進行C = 採点結果.マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

        If 採点進行C IsNot Nothing Then

            'ラウンド設定
            ラウンド数 = ラウンド数 + 1
            SC_J_区分結果_ラウンド設定(ラウンド数).No = ラウンド数
            SC_J_区分結果_ラウンド設定(ラウンド数).ラウンド番号 = ラウンド番号
            SC_J_区分結果_ラウンド設定(ラウンド数).ラウンド名 = 採点結果.マスタデータ.Get_ラウンド名(ラウンド番号)
            SC_J_区分結果_ラウンド設定(ラウンド数).採点方式 = 採点結果.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)


            If 採点進行C.ステータス = "採点済み" Then

                SC_J_区分結果_ラウンド設定(ラウンド数).採点済みFLAG = True

            Else

                SC_J_区分結果_ラウンド設定(ラウンド数).採点済みFLAG = False

                採点結果 = Nothing
                Return 連番

            End If

        Else
            採点結果 = Nothing
            Return 連番

        End If


        Dim 開始順位 As Integer = 連番

        Dim 前の選手の得点 As Decimal = 0
        Dim 前の選手の順位 As Integer = 0

        If 採点結果.出場選手数 > 0 Then
            '決勝の結果がある場合

            For 順位 = 1 To 採点結果.出場選手数
                For s = 1 To 採点結果.出場選手数
                    If 採点結果.総合順位番号(s) = 順位 Then

                        '既存のデータを探しに行く
                        Dim 発見 As Integer = 0
                        For k = 1 To 連番
                            If SC_J_区分結果_選手(k).背番号 = 採点結果.背番号(s) Then
                                発見 = k
                                k = UBound(SC_J_区分結果_選手)
                            End If
                        Next k

                        If 発見 > 0 Then
                            '既に見つかっていた選手

                            順位得点登録(ラウンド番号, ラウンド名, 発見, 採点結果.総合得点(s), 採点結果.総合順位表記(s)) '同点あり


                        Else
                            '最初の選手
                            連番 = 連番 + 1

                            SC_J_区分結果_選手(連番).No = 連番
                            SC_J_区分結果_選手(連番).ラウンド = 採点結果.マスタデータ.Get_ラウンド名(ラウンド番号)

                            Dim 選手の得点 As Decimal = 0
                            If 採点結果.マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) = "順位法" Then
                                選手の得点 = 採点結果.総合順位表記(s)
                            Else
                                選手の得点 = 採点結果.総合得点(s)
                            End If


                            If 前の選手の得点 = 選手の得点 Then
                                '同点の時
                                SC_J_区分結果_選手(連番).総合順位 = 前の選手の順位
                            Else
                                SC_J_区分結果_選手(連番).総合順位 = 連番
                                前の選手の順位 = 連番
                                前の選手の得点 = 選手の得点
                            End If


                            'If Strings.Right(ラウンド番号, 1) = "1" Then
                            '同点決勝の場合　　１位から順に順位がつくため　開始順位を足し込み
                            'SC_J_区分結果_選手(連番).総合順位 = 開始順位 + 採点結果.総合順位表記(s)   '連番   採点結果.総合順位表記(s)

                            'Else
                            'SC_J_区分結果_選手(連番).総合順位 = 採点結果.総合順位表記(s)   '連番   採点結果.総合順位表記(s)

                            'End If


                            SC_J_区分結果_選手(連番).背番号 = 採点結果.背番号(s)

                            Dim 選手C As 選手 = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手M番号, 採点結果.背番号(s))

                            SC_J_区分結果_選手(連番).リーダー名 = 選手C.リーダー表記名
                            SC_J_区分結果_選手(連番).パートナー名 = 選手C.パートナ表記名
                            SC_J_区分結果_選手(連番).カップル所属 = 選手C.カップル所属名

                            順位得点登録(ラウンド番号, ラウンド名, 連番, 採点結果.総合得点(s), 採点結果.総合順位表記(s)) '同点あり


                        End If

                        s = 採点結果.出場選手数

                    End If
                Next s
            Next 順位


        End If


        採点結果 = Nothing

        Return 連番

    End Function

    Private Sub 順位得点登録(ラウンド番号 As String, ラウンド名 As String, 登録番号 As Integer, 得点 As Decimal, 順位 As Integer)


        Dim ラウンド番号一覧IND As Integer = 0
        For r = 1 To UBound(ラウンド番号一覧)
            If ラウンド番号一覧(r) = ラウンド番号 Then

                ラウンド番号一覧IND = r
                r = UBound(ラウンド番号一覧)
            End If

        Next r

        If ラウンド番号一覧IND = 0 Then
            MsgBox("指定されたラウンド番号「" & ラウンド番号 & "」は存在しません")
            Exit Sub
        End If

        SC_J_区分結果_選手(登録番号).SC_J_区分結果_選手_ラウンド結果(ラウンド番号一覧IND).ラウンド番号 = ラウンド番号
        SC_J_区分結果_選手(登録番号).SC_J_区分結果_選手_ラウンド結果(ラウンド番号一覧IND).ラウンド名 = ラウンド名
        SC_J_区分結果_選手(登録番号).SC_J_区分結果_選手_ラウンド結果(ラウンド番号一覧IND).得点 = 得点
        SC_J_区分結果_選手(登録番号).SC_J_区分結果_選手_ラウンド結果(ラウンド番号一覧IND).順位 = 順位



    End Sub

    Public Sub JSON書き出し()


        Dim filename As String = "SC_J_区分結果_" & 区分番号 & ".json"


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



    Public Function JSON読み込み() As SC_J_区分結果

        Dim rc As SC_J_区分結果 = Nothing

        Dim filename As String = "SC_J_区分結果_" & 区分番号 & ".json"


        ''JSON読み込み用
        Dim jText As String = String.Empty


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した


            ''ファイルからJSONを読み込む
            Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)


            jText = cReader.ReadToEnd


            rc = JsonConvert.DeserializeObject(Of SC_J_区分結果)(jText)




            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If


        Return rc
    End Function


    'メソッド

    '集計が終わったラウンド名を返す。
    Public Function Get_確定ラウンド() As String


        Dim rc As String = ""

        For r = ラウンド数 To 1 Step -1
            If SC_J_区分結果_ラウンド設定(r).採点済みFLAG = True Then
                rc = SC_J_区分結果_ラウンド設定(r).ラウンド名
            End If
        Next r

        Return rc

    End Function

    Public Function Get_ラウンド番号(ラウンド名 As String) As String
        'ラウンド名を渡すと、そのラウンド番号を返す
        Dim rc As String = ""

        For r = 1 To ラウンド数
            If SC_J_区分結果_ラウンド設定(r).ラウンド名 = ラウンド名 Then

                rc = SC_J_区分結果_ラウンド設定(r).ラウンド番号
                r = ラウンド数
            End If

        Next r

        Return rc

    End Function


End Class

Public Class SC_J_区分結果_選手

    Public Property No As Integer  '連番

    Public Property 選手M番号 As String
    Public Property 背番号 As String
    Public Property リーダー名 As String
    Public Property パートナー名 As String
    Public Property カップル所属 As String


    Public Property 総合順位 As Integer
    Public Property ラウンド As String

    Public Property ラウンド数 As Integer


    Public SC_J_区分結果_選手_ラウンド結果() As SC_J_区分結果_選手_ラウンド結果



    'Public Property R1得点 As Decimal
    'Public Property R1順位 As Decimal
    'Public Property リダンス得点 As Decimal
    'Public Property リダンス順位 As Decimal
    'Public Property R1同決得点 As Decimal
    'Public Property R1同決順位 As Decimal

    'Public Property R2得点 As Decimal
    'Public Property R2順位 As Decimal
    'Public Property R2同決得点 As Decimal
    'Public Property R2同決順位 As Decimal
    'Public Property R3得点 As Decimal
    'Public Property R3順位 As Decimal
    'Public Property R3同決得点 As Decimal
    'Public Property R3同決順位 As Decimal
    'Public Property R4得点 As Decimal
    'Public Property R4順位 As Decimal
    'Public Property R4同決得点 As Decimal
    'Public Property R4同決順位 As Decimal
    'Public Property R5得点 As Decimal
    'Public Property R5順位 As Decimal
    'Public Property R5同決得点 As Decimal
    'Public Property R5同決順位 As Decimal
    '
    'Public Property 最終得点 As Decimal
    'Public Property 最終順位 As Decimal
    'Public Property 最終同決得点 As Decimal
    'Public Property 最終同決順位 As Decimal

    'Public Property 準々得点 As Decimal
    'Public Property 準々順位 As Decimal
    'Public Property 準々同決得点 As Decimal
    ' Public Property 準々同決順位 As Decimal

    'Public Property SF得点 As Decimal
    'Public Property SF順位 As Decimal
    'Public Property SF同決得点 As Decimal
    'Public Property SF同決順位 As Decimal

    'Public Property 下位決勝得点 As Decimal
    'Public Property 下位決勝順位 As Decimal

    'Public Property 上位決勝得点 As Decimal
    'Public Property 上位決勝順位 As Decimal



    Public Sub New(ラウンド数_ As Integer)

        ラウンド数 = ラウンド数_

        ReDim SC_J_区分結果_選手_ラウンド結果(ラウンド数)

        For r = 1 To ラウンド数
            SC_J_区分結果_選手_ラウンド結果(r) = New SC_J_区分結果_選手_ラウンド結果

        Next r


    End Sub

End Class

Public Class SC_J_区分結果_選手_ラウンド結果

    Public Property ラウンド番号 As String
    Public Property ラウンド名 As String
    Public Property 得点 As Decimal
    Public Property 順位 As Decimal


End Class

Public Class SC_J_区分結果_ラウンド設定

    Public Property No As Integer

    Public Property ラウンド番号 As String
    Public Property ラウンド名 As String
    Public Property 採点方式 As String
    Public Property 採点済みFLAG As Boolean


End Class
