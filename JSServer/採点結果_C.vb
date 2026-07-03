Imports System
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class 採点結果_C

    Public マスタデータ As マスタデータ

    Public 種目数 As Integer   'ブレイキンではラウンド数のこと
    Public 種目記号()

    Public 採点方式 As String

    Public 区分番号 As String
    Public ラウンド番号 As String


    'Public 審判員数 As Integer  審判員は種目毎に異なる可能性があるため
    'Public 審判記号() As String

    Public 種目() As 種目結果

    '===総合結果用
    Public 出場選手数 As Integer
    Public 背番号() As String

    Public 総合得点() As Decimal
    Public 総合順位番号() As Integer '同点無しの連番
    Public 総合順位表記() As Integer '同点有り

    'ブレイキンのみ使用
    Public WIN数() As Integer
    Public 勝ちFLAG() As Integer  '勝った方が１
    Public 総合順位点() As Decimal

    '順位法用
    Public スケーティング結果_総合選手() As スケーティング結果_総合選手

    Public Sub New(区分番号_ As String, ラウンド番号_ As String)

        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_

        マスタデータ = New マスタデータ
        マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
        マスタデータ.F_審判担当PCSマスタ.Read(区分番号, ラウンド番号)
        'マスタデータ.J_新審判設定.Set_新審判基準VER("AJS30J")


        出場選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(1, 0, 背番号)

        採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        If Strings.Left(採点方式, 3) = "BJS" Or Strings.Left(採点方式, 3) = "AJS" Or Strings.Left(採点方式, 4) = "BJPR" Then

            マスタデータ.J_新審判設定.Set_新審判基準VER(採点方式)
        End If

        ReDim 種目記号(10)

        種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号)

        Dim s As Integer
        ReDim 種目(種目数)
        For s = 1 To 種目数
            種目(s) = New 種目結果(区分番号, ラウンド番号, s, マスタデータ)

        Next s

        '総合結果の準備
        ReDim 総合得点(出場選手数)
        ReDim 総合順位番号(出場選手数)
        ReDim 総合順位表記(出場選手数)

        総合結果更新()
    End Sub

    '種目結果を受けて総合結果を更新する
    Public Sub 総合結果更新()

        If Strings.Left(採点方式, 3) = "BJS" Then

            ReDim WIN数(出場選手数)
            ReDim 勝ちFLAG(出場選手数)

            'ブレイキンの時       '




            '総合得点の計算
            For s = 1 To 出場選手数
                Dim 選手総合得点 As Double = 0
                For d = 1 To 種目数
                    選手総合得点 = 選手総合得点 + 種目(d).選手(s).種目得点
                Next d
                総合得点(s) = 選手総合得点
            Next s

            'WIN数の算出　　同点の時はどちらにもカウントする
            For d = 1 To 種目数
                Dim 最高点 As Double = 0
                Dim 最高点選手番号 As Integer = 0

                For s = 1 To 出場選手数
                    If 最高点 < 種目(d).選手(s).種目得点 Then
                        最高点 = 種目(d).選手(s).種目得点
                    End If
                Next s

                If 最高点 > 0 Then
                    For s = 1 To 出場選手数
                        If 最高点 = 種目(d).選手(s).種目得点 Then
                            WIN数(s) = WIN数(s) + 1
                        End If
                    Next s
                End If

            Next d

            '順位の計算
            If マスタデータ.J_新審判設定.勝敗方式 = "R" Then
                'ラウンド制の時
                '2つ勝った方が勝ち
                総合順位確定_ラウンド数判定()
            Else
                '点数が大きい方が勝ち
                総合順位確定()

            End If



            'ブレイキン勝ち負け判定　勝ちFLAG()　に１を付ける

            If マスタデータ.J_新審判設定.勝敗方式 = "xxx" Then
                '



            ElseIf 種目数 = 1 Then
                'ポイント制で、種目数（ラウンド数）が１の時（つまり、FISE広島の予選用）


            Else
                'ラウンド制の時も同じ
                '2つ勝った方が勝ち

                'ポイント制の時　　
                '2ラウンド以上終了して、総合順位が高い方がいたら 1を付ける。同点の時は１はつけない。
                Dim 勝者選手番号(2) As Integer
                Dim 勝者数 As Integer = 0
                For s = 1 To 出場選手数
                    If 総合順位表記(s) = 1 Then
                        If 種目(1).選手(s).種目得点 > 0 And 種目(2).選手(s).種目得点 > 0 Then
                            勝者数 = 勝者数 + 1
                            勝者選手番号(勝者数) = s
                        End If
                    End If
                Next s

                If 勝者数 = 1 Then
                    '勝ち決定
                    勝ちFLAG(勝者選手番号(1)) = 1
                Else
                    '同点がいるとき　又は　まだ結果が出ていない時

                End If

            End If


        ElseIf 採点方式 = "順位法" Then

            総合順位確定_順位法()


        ElseIf Strings.Left(採点方式, 4) = "BJPR" Then

            ReDim 総合順位点(出場選手数)


            '総合得点の計算   種目毎に選手が違うケースがある。。。
            For s = 1 To 出場選手数
                Dim 選手総合得点 As Double = 0
                For d = 1 To 種目数
                    For ds = 1 To 種目(d).選手数
                        If 種目(d).選手(ds).背番号 = 背番号(s) Then
                            選手総合得点 = 選手総合得点 + 種目(d).選手(ds).種目得点

                            ds = 種目(d).選手数
                        End If
                    Next ds
                Next d
                総合得点(s) = 選手総合得点
            Next s


            総合順位確定_BJPRE()

        Else


            '総合得点の計算   種目毎に選手が違うケースがある。。。
            For s = 1 To 出場選手数
                Dim 選手総合得点 As Double = 0
                For d = 1 To 種目数
                    For ds = 1 To 種目(d).選手数
                        If 種目(d).選手(ds).背番号 = 背番号(s) Then
                            選手総合得点 = 選手総合得点 + 種目(d).選手(ds).種目得点

                            ds = 種目(d).選手数
                        End If
                    Next ds
                Next d
                総合得点(s) = 選手総合得点
            Next s

            '順位の計算
            総合順位確定()

        End If



    End Sub

    '総合順位番号を渡すと、該当選手の選手番号を返す 該当が無い時は0を返す
    Public Function Get選手番号(総合順位番号_) As Integer
        Dim rc As Integer = 0

        For i = 1 To UBound(総合順位番号)
            If 総合順位番号(i) = 総合順位番号_ Then

                rc = i
                i = UBound(総合順位番号)
            End If

        Next i

        Return rc
    End Function


    Private Sub 総合順位確定()
        ' 総合得点 を使って順位を確定し、以下の項目に値を入れる
        ' 総合順位番号 As Integer '同点無しの連番
        ' 総合順位表記 As Integer '同点有り

        Dim 選手番号リスト() As Integer
        Dim 得点リスト() As Double

        ReDim 選手番号リスト(出場選手数)
        ReDim 得点リスト(出場選手数)

        'Tempの得点リストの作成
        Dim Temp得点リスト() As Double
        ReDim Temp得点リスト(出場選手数)

        For s = 1 To 出場選手数
            Temp得点リスト(s) = 総合得点(s)
        Next s


        Dim 最大得点選手番号 As Integer = 0
        Dim 最大得点 As Double

        '並べ替え
        For 降順 = 1 To 出場選手数
            最大得点 = -1
            For s = 1 To 出場選手数
                If 最大得点 < Temp得点リスト(s) Then
                    最大得点 = Temp得点リスト(s)
                    最大得点選手番号 = s
                End If
            Next s
            選手番号リスト(降順) = 最大得点選手番号
            得点リスト(降順) = 最大得点
            Temp得点リスト(最大得点選手番号) = -1
        Next 降順

        Dim 前の選手の得点 As Double = -1
        Dim 前の選手の順位 As Integer = 0
        For 降順 = 1 To 出場選手数
            総合順位番号(選手番号リスト(降順)) = 降順
            If 前の選手の得点 = 得点リスト(降順) Then
                '同点の時
                総合順位表記(選手番号リスト(降順)) = 前の選手の順位
            Else
                '同点じゃない時
                総合順位表記(選手番号リスト(降順)) = 降順
            End If
            前の選手の順位 = 総合順位表記(選手番号リスト(降順))
            前の選手の得点 = 得点リスト(降順)
        Next 降順


        If マスタデータ.J_新審判設定.同点処理 <> "0" And 採点方式 <> "チェック法" Then
            AJS同点処理()
        End If


    End Sub

    Private Sub 総合順位確定_ラウンド数判定()
        ' WIN数 を使って順位を確定し、以下の項目に値を入れる
        ' 総合順位番号 As Integer '同点無しの連番
        ' 総合順位表記 As Integer '同点有り

        Dim 選手番号リスト() As Integer
        Dim 得点リスト() As Double

        ReDim 選手番号リスト(出場選手数)
        ReDim 得点リスト(出場選手数)

        Dim tempWIN数リスト（）
        ReDim tempWIN数リスト（出場選手数）


        For s = 1 To 出場選手数
            tempWIN数リスト(s) = WIN数(s)
        Next s


        '並べ替え
        Dim 最高WIN数 As Integer = 0
        Dim 最高WIN数選手番号 As Integer = 0

        For 降順 = 1 To 出場選手数
            最高WIN数 = -1
            For s = 1 To 出場選手数
                If 最高WIN数 < tempWIN数リスト(s) Then
                    最高WIN数 = tempWIN数リスト(s)
                    最高WIN数選手番号 = s
                End If
            Next s
            選手番号リスト(降順) = 最高WIN数選手番号
            得点リスト(降順) = 最高WIN数
            tempWIN数リスト(最高WIN数選手番号) = -1
        Next 降順

        Dim 前の選手の得点 As Double = -1
        Dim 前の選手の順位 As Integer = 0
        For 降順 = 1 To 出場選手数
            総合順位番号(選手番号リスト(降順)) = 降順
            If 前の選手の得点 = 得点リスト(降順) Then
                '同点の時
                総合順位表記(選手番号リスト(降順)) = 前の選手の順位
            Else
                '同点じゃない時
                総合順位表記(選手番号リスト(降順)) = 降順
            End If
            前の選手の順位 = 総合順位表記(選手番号リスト(降順))
            前の選手の得点 = 得点リスト(降順)
        Next 降順



    End Sub

    Private Sub 総合順位確定_順位法()

        Dim スケーティング As スケーティング_総合_C
        スケーティング = New スケーティング_総合_C

        Dim ジャッジ人数 As Integer = 種目(1).審判員数woRef  '1種目目のジャッジ人数を設定

        Dim スケーティング結果単科(種目数) As スケーティング結果_単科
        For d = 1 To 種目数
            スケーティング結果単科(d) = New スケーティング結果_単科(出場選手数, ジャッジ人数)
        Next d


        ReDim スケーティング結果_総合選手(出場選手数)

        For s = 1 To 出場選手数

            スケーティング結果_総合選手(s) = New スケーティング結果_総合選手(種目数 * ジャッジ人数)


            For d = 1 To 種目数
                スケーティング結果単科(d).スケーティング結果_選手(s).背番号 = 種目(d).選手(s).背番号
                スケーティング結果単科(d).スケーティング結果_選手(s).順位 = 種目(d).選手(s).スケーティング結果_選手.順位
                スケーティング結果単科(d).スケーティング結果_選手(s).順位点数 = 種目(d).選手(s).スケーティング結果_選手.順位点数

                For j = 1 To ジャッジ人数
                    スケーティング結果単科(d).スケーティング結果_選手(s).ソート後点数(j) = 種目(d).選手(s).スケーティング結果_選手.ソート後点数(j)
                Next j

            Next d


        Next s


        スケーティング.総合スケーティング計算(出場選手数, ジャッジ人数, 種目数, スケーティング結果単科)

        '結果を出力
        For s = 1 To 出場選手数

            総合順位表記(s) = スケーティング.スケーティング結果_総合.スケーティング結果_総合選手(s).順位    '同点有り
            スケーティング結果_総合選手(s) = スケーティング.スケーティング結果_総合.スケーティング結果_総合選手(s)

        Next s


        '同点無の連番を　総合順位番号に埋める
        Dim 順位番号 As Integer = 1

        For 順位 = 0 To 出場選手数
            For s = 1 To 出場選手数

                If 総合順位表記(s) = 順位 Then
                    総合順位番号(s) = 順位番号
                    順位番号 = 順位番号 + 1
                End If

            Next s
        Next 順位


        スケーティング = Nothing


    End Sub

    Private Sub 総合順位確定_BJPRE()
        ' ジャッジが採点した順位から順位点を使って小さい方から順位を確定し、以下の項目に値を入れる
        ' 同点の時は、ジャッジが採点した得点合計大きい方を勝者とする。

        ' 総合順位番号 As Integer '同点無しの連番
        ' 総合順位表記 As Integer '同点有り

        '総合順位点 as decimal
        '  種目().選手結果().種目順位点　as decimal
        '  種目().選手結果().審判().順位点 　as decimal


        '===BJPR用=　ジャッジ順位点の算出====
        Dim ジャッジ毎得点() As Decimal
        Dim ジャッジ毎順位点() As Decimal

        'Dim 順位点() As Decimal   'これが順位点
        ' ReDim 順位点(出場選手数)

        '種目順位点を一旦クリア
        For d = 1 To 種目数
            For s = 1 To 出場選手数
                種目(d).選手(s).種目順位点 = 0
            Next s
        Next d



        For d = 1 To 種目数
            For j = 1 To 種目(d).審判員数

                '
                ReDim ジャッジ毎得点(出場選手数)

                For s = 1 To 出場選手数
                    ジャッジ毎得点(s) = 種目(d).選手(s).審判(j).素点
                Next s

                '====ソート　ジャッジ毎得点を基に、ジャッジ毎順位点を算出
                ReDim ジャッジ毎順位点(出場選手数)

                Dim 順位 As Integer = 1

                Do While 順位 <= 出場選手数
                    Dim 最大値 As Decimal = 0
                    Dim 人数 As Integer = 0

                    '最大値を探す
                    For s = 1 To 出場選手数
                        If ジャッジ毎得点(s) > 最大値 Then
                            最大値 = ジャッジ毎得点(s)
                        End If
                    Next s

                    '最大値と同じ点数を持っている選手に順位を付けて、その得点を -10にする。
                    For s = 1 To 出場選手数
                        If ジャッジ毎得点(s) = 最大値 Then
                            ジャッジ毎順位点(s) = 順位
                            人数 = 人数 + 1
                            ジャッジ毎得点(s) = -10
                        End If
                    Next s

                    '次の順位
                    順位 = 順位 + 人数
                Loop

                'ジャッジ毎順位点を、順位点に入れる
                For s = 1 To 出場選手数
                    '順位点(s) = 順位点(s) + ジャッジ毎順位点(s)

                    種目(d).選手(s).種目順位点 = 種目(d).選手(s).種目順位点 + ジャッジ毎順位点(s)

                    種目(d).選手(s).審判(j).順位点 = ジャッジ毎順位点(s)

                Next s


            Next j


        Next d
        '===============

        '総合順位点の作成
        'まずはクリア
        For s = 1 To 出場選手数
            総合順位点(s) = 0
        Next s



        For s = 1 To 出場選手数
            Dim 選手総合順位点 As Double = 0
            For d = 1 To 種目数
                For ds = 1 To 種目(d).選手数
                    If 種目(d).選手(ds).背番号 = 背番号(s) Then
                        選手総合順位点 = 選手総合順位点 + 種目(d).選手(ds).種目順位点

                        ds = 種目(d).選手数
                    End If
                Next ds
            Next d
            総合順位点(s) = 選手総合順位点
        Next s


        '====順位計算

        'PR1用　Temp順位点リストの作成　
        Dim Temp順位点リスト() As Double
        ReDim Temp順位点リスト(出場選手数)

        For s = 1 To 出場選手数
            Temp順位点リスト(s) = 総合順位点(s)
        Next s

        Dim 総合順位 As Integer = 1

        Do While 総合順位 <= 出場選手数

            '最小順位点を見つける
            Dim 最小順位点 As Decimal = 10000
            For s = 1 To 出場選手数
                If 最小順位点 > Temp順位点リスト(s) Then
                    最小順位点 = Temp順位点リスト(s)
                End If
            Next s

            '最小順位点を持っている選手が何人いるか数える
            Dim 最小順位点保持人数 As Integer = 0
            Dim 最小順位点保持選手No As Integer = 0
            For s = 1 To 出場選手数
                If 最小順位点 = Temp順位点リスト(s) Then
                    最小順位点保持人数 = 最小順位点保持人数 + 1
                    最小順位点保持選手No = s
                End If
            Next s

            If 最小順位点保持人数 = 1 Then

                'もし一人だったら
                総合順位表記(最小順位点保持選手No) = 総合順位
                総合順位番号(最小順位点保持選手No) = 総合順位

                Temp順位点リスト(最小順位点保持選手No) = 10000

                総合順位 = 総合順位 + 1
            Else

                'もし2人以上だったら

                'PR2用　得点Tempを作成する
                Dim Temp得点リスト() As Double
                ReDim Temp得点リスト(最小順位点保持人数)

                Dim 得点Temp選手NO() As Integer
                ReDim 得点Temp選手NO(最小順位点保持人数)

                Dim 同点人数 As Integer = 1
                For s = 1 To 出場選手数
                    If 最小順位点 = Temp順位点リスト(s) Then
                        Temp得点リスト(同点人数) = 総合得点(s)
                        得点Temp選手NO(同点人数) = s

                        同点人数 = 同点人数 + 1
                    End If
                Next s

                Dim 同点処理開始時総合順位 = 総合順位

                Do While 総合順位 < 同点処理開始時総合順位 + 最小順位点保持人数

                    '最大得点を見つける
                    Dim 最大得点 As Decimal = -10

                    For j = 1 To 最小順位点保持人数
                        If 最大得点 < Temp得点リスト(j) Then
                            最大得点 = Temp得点リスト(j)
                        End If
                    Next j

                    '最大得点を持っている選手が何人いるか数える
                    Dim 最大得点保持人数 As Integer = 0
                    Dim 最大得点保持選手No As Integer = 0
                    For j = 1 To 最小順位点保持人数
                        If 最大得点 = Temp得点リスト(j) Then
                            最大得点保持人数 = 最大得点保持人数 + 1
                            最大得点保持選手No = j
                        End If
                    Next j



                    If 最大得点保持人数 = 1 Then

                        'もし一人だったら
                        総合順位表記(得点Temp選手NO(最大得点保持選手No)) = 総合順位
                        総合順位番号(得点Temp選手NO(最大得点保持選手No)) = 総合順位

                        Temp得点リスト(最大得点保持選手No) = -10
                        Temp順位点リスト(得点Temp選手NO(最大得点保持選手No)) = 10000


                        総合順位 = 総合順位 + 1

                    Else

                        'もし2人以上だったら  Priorite 3 （上位2人の順位点の小さい方が勝者）

                        '上位2人の順位点リストの作成
                        Dim 上位2人順位点リスト() As Decimal
                        ReDim 上位2人順位点リスト(最大得点保持人数)

                        Dim 上位2人順位点リスト選手No() As Integer
                        ReDim 上位2人順位点リスト選手No(最大得点保持人数)

                        '比較対象者の選手番号リストを作成
                        Dim 上位2人順位点同点人数 As Integer = 0
                        For j = 1 To 最小順位点保持人数
                            If 最大得点 = Temp得点リスト(j) Then

                                上位2人順位点同点人数 = 上位2人順位点同点人数 + 1
                                上位2人順位点リスト選手No(上位2人順位点同点人数) = 得点Temp選手NO(j)　　　'←対象者の選手番号リスト
                            End If
                        Next j

                        '比較対象者の　上位2人順位点リストを作成
                        For k = 1 To 上位2人順位点同点人数

                            Dim 対象選手番号 As Integer = 上位2人順位点リスト選手No(k)

                            Dim PR3_最小点 As Decimal = 10000　　'3人のジャッジの中で一番小さい点


                            'TEMPリストの作成
                            Dim PR3_Temp順位点リスト() As Decimal
                            ReDim PR3_Temp順位点リスト(種目(1).審判員数)

                            For j = 1 To 種目(1).審判員数

                                PR3_Temp順位点リスト(j) = 種目(1).選手(対象選手番号).審判(j).順位点

                                If PR3_最小点 > 種目(1).選手(対象選手番号).審判(j).順位点 Then

                                    PR3_最小点 = 種目(1).選手(対象選手番号).審判(j).順位点

                                End If
                            Next j

                            '最小点の保持者人数をカウント
                            Dim PR3_最小点_採点審判員数 As Integer = 0
                            Dim PR3_最小点_採点審判員No As Integer = 0


                            For j = 1 To 種目(1).審判員数
                                If PR3_最小点 = PR3_Temp順位点リスト(j) Then
                                    PR3_最小点_採点審判員数 = PR3_最小点_採点審判員数 + 1
                                    PR3_最小点_採点審判員No = j
                                End If
                            Next j

                            If PR3_最小点_採点審判員数 >= 2 Then

                                '最小点が2人以上いるときは、同じ点数を2倍すれば、上位2人の順位点合計になる
                                上位2人順位点リスト(k) = PR3_最小点 + PR3_最小点

                            ElseIf PR3_最小点_採点審判員数 = 1 Then

                                '最小点が一人しかいない時は2人目の審判員を探す
                                上位2人順位点リスト(k) = PR3_最小点   'まずは1人目を足し込む

                                PR3_Temp順位点リスト(PR3_最小点_採点審判員No) = 1000


                                '最小点を除いたリストから、次の最小点を探す。
                                PR3_最小点 = 10000
                                For j = 1 To 種目(1).審判員数
                                    If PR3_最小点 > PR3_Temp順位点リスト(j) Then
                                        PR3_最小点 = PR3_Temp順位点リスト(j)
                                    End If
                                Next j

                                上位2人順位点リスト(k) = 上位2人順位点リスト(k) + PR3_最小点   '2人目を足し込む

                            End If
                        Next k



                        '上位2人の順位点リスト の最小点を見つける
                        Dim PR3_上位2人の順位点_最小点 As Decimal = 10000
                        Dim PR3_上位2人の順位点_最小点_保持選手No As Integer = 0

                        For i = 1 To 最大得点保持人数
                            If PR3_上位2人の順位点_最小点 > 上位2人順位点リスト(i) Then
                                PR3_上位2人の順位点_最小点 = 上位2人順位点リスト(i)
                                PR3_上位2人の順位点_最小点_保持選手No = i
                            End If
                        Next i


                        '上位2人の順位点リスト の最小点を持っている人数を数える
                        Dim PR3_上位2人の順位点_最小点_保持選手数 As Integer = 0
                        For i = 1 To 最大得点保持人数
                            If PR3_上位2人の順位点_最小点 = 上位2人順位点リスト(i) Then
                                PR3_上位2人の順位点_最小点_保持選手数 = PR3_上位2人の順位点_最小点_保持選手数 + 1
                            End If
                        Next i



                        '上位2人の順位点リスト の最小点を持っている人数が1人だったら、順位確定
                        If PR3_上位2人の順位点_最小点_保持選手数 = 1 Then

                            総合順位表記(上位2人順位点リスト選手No(PR3_上位2人の順位点_最小点_保持選手No)) = 総合順位
                            総合順位番号(上位2人順位点リスト選手No(PR3_上位2人の順位点_最小点_保持選手No)) = 総合順位

                            'PR3用をクリア
                            上位2人順位点リスト(PR3_上位2人の順位点_最小点_保持選手No) = 10000

                            'PR2用クリア
                            For pr2 = 1 To 最小順位点保持人数
                                If 得点Temp選手NO(pr2) = 上位2人順位点リスト選手No(PR3_上位2人の順位点_最小点_保持選手No) Then
                                    Temp得点リスト(pr2) = -10
                                End If
                            Next pr2

                            'PR1用をクリア
                            Temp順位点リスト(上位2人順位点リスト選手No(PR3_上位2人の順位点_最小点_保持選手No)) = 10000


                            総合順位 = 総合順位 + 1

                        Else

                            '上位2人の順位点リスト の最小点を持っている人数が２人以上だったら、同点
                            Dim PR3_同点人数 As Integer = 0
                            For i = 1 To 最大得点保持人数
                                If PR3_上位2人の順位点_最小点 = 上位2人順位点リスト(i) Then



                                    総合順位表記(上位2人順位点リスト選手No(i)) = 総合順位
                                    総合順位番号(上位2人順位点リスト選手No(i)) = 総合順位 + PR3_同点人数

                                    'PR3用をクリア
                                    上位2人順位点リスト(i) = 10000

                                    'PR2用クリア
                                    For pr2 = 1 To 最小順位点保持人数
                                        If 得点Temp選手NO(pr2) = 上位2人順位点リスト選手No(i) Then
                                            Temp得点リスト(pr2) = -10
                                        End If
                                    Next pr2


                                    'PR1用をクリア
                                    Temp順位点リスト(上位2人順位点リスト選手No(i)) = 10000

                                    PR3_同点人数 = PR3_同点人数 + 1

                                End If
                            Next i

                            総合順位 = 総合順位 + PR3_同点人数


                        End If


                    End If


                Loop

            End If
        Loop



    End Sub

    Private Sub BJPREの旧バージョン()


        'Tempの得点リストの作成
        Dim Temp得点リスト() As Double
        ReDim Temp得点リスト(出場選手数)

        For s = 1 To 出場選手数
            Temp得点リスト(s) = 総合得点(s)
        Next s

        Dim 総合順位 As Integer = 1


        Do While 総合順位 <= 出場選手数
            '最大得点を見つける
            Dim 最大得点 As Decimal = -10
            For s = 1 To 出場選手数
                If 最大得点 < Temp得点リスト(s) Then
                    最大得点 = Temp得点リスト(s)
                End If
            Next s

            '最大得点を持っている選手が何人いるか数える
            Dim 最大得点保持人数 As Integer = 0
            Dim 最大得点保持選手No As Integer = 0
            For s = 1 To 出場選手数
                If 最大得点 = Temp得点リスト(s) Then
                    最大得点保持人数 = 最大得点保持人数 + 1
                    最大得点保持選手No = s
                End If
            Next s

            If 最大得点保持人数 = 1 Then
                'もし一人だったら
                総合順位表記(最大得点保持選手No) = 総合順位
                総合順位番号(最大得点保持選手No) = 総合順位

                Temp得点リスト(最大得点保持選手No) = -10

                総合順位 = 総合順位 + 1
            Else
                'もし2人以上だったら

                '順位点Tempを作成する
                Dim 順位点Temp() As Decimal
                ReDim 順位点Temp(最大得点保持人数)

                Dim 順位点Temp選手NO() As Integer
                ReDim 順位点Temp選手NO(最大得点保持人数)

                Dim 同点人数 As Integer = 1
                For s = 1 To 出場選手数
                    If 最大得点 = Temp得点リスト(s) Then
                        順位点Temp(同点人数) = 総合順位点(s)
                        順位点Temp選手NO(同点人数) = s

                        同点人数 = 同点人数 + 1
                    End If
                Next s

                Dim 同点処理開始時総合順位 = 総合順位

                Do While 総合順位 < 同点処理開始時総合順位 + 最大得点保持人数
                    '最小順位点を見つける
                    Dim 最小順位点 As Decimal = 1000

                    For j = 1 To 最大得点保持人数
                        If 最小順位点 > 順位点Temp(j) Then
                            最小順位点 = 順位点Temp(j)
                        End If
                    Next j

                    '最小順位点を持っている選手が何人いるか数える
                    Dim 最小順位点保持人数 As Integer = 0
                    Dim 最小順位点保持選手No As Integer = 0
                    For j = 1 To 最大得点保持人数
                        If 最小順位点 = 順位点Temp(j) Then
                            最小順位点保持人数 = 最小順位点保持人数 + 1
                            最小順位点保持選手No = j
                        End If
                    Next j


                    If 最小順位点保持人数 = 1 Then
                        'もし一人だったら
                        総合順位表記(順位点Temp選手NO(最小順位点保持選手No)) = 総合順位
                        総合順位番号(順位点Temp選手NO(最小順位点保持選手No)) = 総合順位

                        Temp得点リスト(順位点Temp選手NO(最小順位点保持選手No)) = -10
                        順位点Temp(最小順位点保持選手No) = 10000


                        総合順位 = 総合順位 + 1

                    Else
                        'もし2人以上だったら 同点にする

                        Dim 順位点同点人数 As Integer = 0
                        For j = 1 To 最大得点保持人数
                            If 最小順位点 = 順位点Temp(j) Then

                                総合順位表記(順位点Temp選手NO(j)) = 総合順位
                                総合順位番号(順位点Temp選手NO(j)) = 総合順位 + 順位点同点人数

                                順位点同点人数 = 順位点同点人数 + 1
                                Temp得点リスト(順位点Temp選手NO(j)) = -10
                                順位点Temp(j) = 10000

                            End If
                        Next j

                        総合順位 = 総合順位 + 順位点同点人数

                    End If
                Loop

            End If
        Loop





    End Sub


    Private Sub AJS同点処理()
        '総合順位表記に同点があった時に、同点処理を行う

        Dim 最大出場選手数 As Integer = 200
        Dim 同点選手番号リスト() As Integer

        Dim 同点選手() As 同点選手PCS

        Dim PCS数 As Integer
        Dim 種目記号リスト() = Nothing

        PCS数 = マスタデータ.J_新審判設定.GetPCS数 * 種目数
        'PCS数は、　4つのPCS×種目数(5) =２０


        '同点を探す
        For 順位 = 1 To 出場選手数

            ReDim 同点選手番号リスト(最大出場選手数）
            Dim 同点者数 As Integer = 0
            For s = 1 To 出場選手数
                If 総合順位表記(s) = 順位 Then
                    同点者数 = 同点者数 + 1
                    同点選手番号リスト(同点者数) = s
                End If
            Next s

            If 同点者数 >= 2 Then
                '同点があった時

                '同点選手クラスを 同点選手人数分作成

                ReDim 同点選手(同点者数)  'as 同点選手クラス

                For d = 1 To 同点者数
                    同点選手(d) = New 同点選手PCS(PCS数)
                    同点選手(d).背番号 = 種目(1).選手(同点選手番号リスト(d)).背番号

                    '同点選手クラスを 同点選手人数分作成
                    Dim pcs_c As Integer = 0
                    For syumoku = 1 To 種目数
                        For p = 1 To マスタデータ.J_新審判設定.GetPCS数
                            同点選手(d).PCS得点(pcs_c) = 種目(syumoku).選手(同点選手番号リスト(d)).種目各PCS得点(p)
                            pcs_c = pcs_c + 1

                            'pcs_c は、０から１９まで入る

                        Next p
                    Next syumoku

                    'PCS採点のソート
                    Array.Sort(同点選手(d).PCS得点)
                    '降順に並べ替え
                    Array.Reverse(同点選手(d).PCS得点)
                    'ソート後は配列番号 0　から始まるので注意

                Next d

                '再スケーティング

                'まず、20/2 + 1  =  11番目  (配列では10番）の比較をする。　同点だったら下まで見る
                Dim 比較対象PCS配列番号 = PCS数 \ 2 + 1

                PCS再スケーティング(同点選手, 順位, 比較対象PCS配列番号, PCS数)

                '2020/11/16 ここで同点結果を総合順位に書き戻さないと、同点が複数出た時に、前の同点処理が消えてしまう。

                For d = 1 To 同点者数
                    '背番号から選手番号を探す
                    Dim TEMP選手番号 As Integer = 0
                    For tt = 1 To 出場選手数
                        If 背番号(tt) = 同点選手(d).背番号 Then
                            '総合順位表記を更新

                            '確定順位が0だった時は、同点の時なので、そのままにする。
                            If 同点選手(d).確定順位 > 0 Then
                                総合順位表記(tt) = 同点選手(d).確定順位
                                総合順位番号(tt) = 同点選手(d).確定順位
                                tt = 出場選手数
                            End If


                        End If
                    Next tt
                Next d

                '同点があった時　ここまで
            End If

        Next 順位

    End Sub


    '===========================
    '概要　
    '入力　同点選手リスト()配列　,  開始順位,  比較対象PCS配列番号, 全PCS数（5種目X4PCSで、２０）
    '出力　同点選手リスト().確定順位 に確定順位を記入
    '　　　さらに同点がある場合は、同点選手リスト().確定順位　はゼロのまま
    '===========================

    Private Sub PCS再スケーティング(ByVal 同点選手リスト() As 同点選手PCS, ByVal 開始順位 As Integer, ByVal 比較対象PCS配列番号 As Integer, ByVal 全PCS数 As Integer）


        Dim 最大値選手リスト() As Integer
        ReDim 最大値選手リスト(UBound(同点選手リスト))


        '比較対象選手リストの作成
        Dim 比較対象選手リスト() As Integer
        ReDim 比較対象選手リスト(UBound(同点選手リスト))

        For s = 1 To UBound(同点選手リスト)
            If 同点選手リスト(s).確定順位 = 0 Then
                比較対象選手リスト(s) = 1
            End If
        Next s



        Dim PCS比較同点者数 As Integer = PCS同点対象者数(同点選手リスト, 比較対象選手リスト, 比較対象PCS配列番号, 最大値選手リスト)
        If PCS比較同点者数 = 1 Then
            '最大値が1人しかいない時は、順位確定

            '対象選手の確定
            For s = 1 To UBound(最大値選手リスト)
                If 最大値選手リスト(s) = 1 Then
                    同点選手リスト(s).確定順位 = 開始順位
                    比較対象選手リスト(s) = 0
                    s = UBound(最大値選手リスト）
                End If
            Next s

            '残った選手については、再度確認
            PCS再スケーティング(同点選手リスト, 開始順位 + 1, 比較対象PCS配列番号, 全PCS数)

        Else
            '最大値が2人以上いる場合は、PCS配列をを下がって比較

            For pcs配列番号 = 比較対象PCS配列番号 + 1 To 全PCS数

                '対象選手の確定
                Dim ss As Integer = 0
                For s = 1 To UBound(同点選手リスト)
                    If 最大値選手リスト(s) = 1 Then
                        比較対象選手リスト(s) = 1
                    Else
                        比較対象選手リスト(s) = 0
                    End If
                Next s


                PCS比較同点者数 = PCS同点対象者数(同点選手リスト, 比較対象選手リスト, pcs配列番号, 最大値選手リスト)

                If PCS比較同点者数 = 1 Then
                    '最大値が1人しかいない時は、順位確定

                    '対象選手の確定
                    For s = 1 To UBound(最大値選手リスト)
                        If 最大値選手リスト(s) = 1 Then
                            同点選手リスト(s).確定順位 = 開始順位
                            s = UBound(最大値選手リスト）
                        End If
                    Next s
                    pcs配列番号 = 全PCS数

                    '残った選手については、再度確認
                    PCS再スケーティング(同点選手リスト, 開始順位 + 1, 比較対象PCS配列番号, 全PCS数)


                Else
                    '最大値が2人以上いる場合は、PCS配列をを下がって比較

                End If


            Next pcs配列番号


        End If



    End Sub


    '===========================
    '概要　PCS配列(20)の、PCS番号を指定して、最大の値を持っている選手が何人居るかを返す
    '入力  同点選手リスト()配列、
    '      比較対象選手()配列：　 同点選手リスト配列の中で、今回比較対象となる選手が分かる配列 １が立っているものが対象
    '      比較対象PCS配列番号
    '出力　最大値選手リスト()配列  1:最大値を持つ　0:NO　　同点選手リストの配列番号と一致
    '戻り値 最大値を持つ選手の数 1ならば同点は無し
    '======================
    Private Function PCS同点対象者数(ByVal 同点選手リスト() As 同点選手PCS, ByVal 比較対象選手() As Integer, ByVal 比較対象PCS配列番号 As Integer,
                                     ByRef 最大値選手リスト() As Integer)
        Dim 同点者数 As Integer = 0
        Dim 最大値 As Double = -1

        '最大値を確定
        For s = 1 To UBound(同点選手リスト)
            If 比較対象選手(s) = 1 Then
                If 同点選手リスト(s).PCS得点(比較対象PCS配列番号) > 最大値 Then
                    最大値 = 同点選手リスト(s).PCS得点(比較対象PCS配列番号)
                End If
            End If
        Next s

        '最大値を持っている人数を集計
        For s = 1 To UBound(同点選手リスト)
            If 比較対象選手(s) = 1 Then
                If 同点選手リスト(s).PCS得点(比較対象PCS配列番号) = 最大値 Then
                    同点者数 = 同点者数 + 1
                    最大値選手リスト(s) = 1
                Else
                    最大値選手リスト(s) = 0
                End If
            End If
        Next s
        Return 同点者数

    End Function


    '===========================
    '概要　採点結果の合成処理
    '入力  採点結果_C、
    '        自分に、入力に指定された採点管理_C　を足し込んで、戻り値として返す。
    '戻り値　合成済みの 採点結果_C
    '======================
    Public Function 合成(総合区分番号 As String, 総合ラウンド番号 As String, 追加採点結果_C As 採点結果_C) As 採点結果_C


        Dim rc As 採点結果_C = Nothing
        rc = New 採点結果_C(総合区分番号, 総合ラウンド番号)



        'Public 種目数 As Integer   'ブレイキンではラウンド数のこと
        'Public 種目記号()


        'まずは自分の種目数、種目記号をコピー
        rc.種目数 = 種目数
        ReDim rc.種目記号(rc.種目数)

        For d = 1 To 種目数
            rc.種目記号(d) = 種目記号(d)
        Next d

        '追加分を追加 種目数、種目記号
        For ad = 1 To 追加採点結果_C.種目数   'add

            Dim Find_Flag = False
            For nd = 1 To rc.種目数           'new
                If rc.種目記号(nd) = 追加採点結果_C.種目記号(ad) Then

                    Find_Flag = True
                    nd = rc.種目数
                End If
            Next nd

            '初めての種目は追加
            If Find_Flag = False Then

                rc.種目数 = rc.種目数 + 1
                ReDim Preserve rc.種目記号(rc.種目数)
                rc.種目記号(rc.種目数) = 追加採点結果_C.種目記号(ad)
            End If
        Next ad



        'Public 採点方式 As String

        rc.採点方式 = 採点方式


        'Public 区分番号 As String
        'Public ラウンド番号 As String


        'Public 種目() As 種目結果

        Dim s As Integer
        ReDim rc.種目(rc.種目数)
        For s = 1 To Me.種目数
            'rc.種目(s) = New 種目結果(総合区分番号, 総合ラウンド番号, s, マスタデータ)
            rc.種目(s) = Me.種目(s)
        Next s



        '===総合結果用
        'Public 出場選手数 As Integer
        'Public 背番号() As String

        'まずは自分の選手をコピー
        rc.出場選手数 = 出場選手数
        ReDim rc.背番号(rc.出場選手数)

        For s = 1 To 出場選手数
            rc.背番号(s) = 背番号(s)
        Next s

        For a_s = 1 To 追加採点結果_C.出場選手数

            Dim Find_Flag = False
            For n_s = 1 To rc.出場選手数
                If rc.背番号(n_s) = 追加採点結果_C.背番号(a_s) Then

                    Find_Flag = True
                    n_s = rc.出場選手数
                End If
            Next n_s

            If Find_Flag = False Then
                rc.出場選手数 = rc.出場選手数 + 1
                ReDim Preserve rc.背番号(rc.出場選手数)
                rc.背番号(rc.出場選手数) = 追加採点結果_C.背番号(a_s)
            End If

        Next a_s


        '種目結果    種目の統合はこれでOKだが、同じ種目の統合はこれではできない。。。。
        ReDim rc.種目(rc.種目数)
        For d = 1 To 種目数
            rc.種目(d) = 種目(d)
        Next d

        Dim a_d = 1
        For d = 種目数 + 1 To rc.種目数
            rc.種目(d) = 追加採点結果_C.種目(a_d)
            a_d = a_d + 1
        Next d



        'Public 総合得点() As Decimal
        'Public 総合順位番号() As Integer '同点無しの連番
        'Public 総合順位表記() As Integer '同点有り


        '総合結果の準備
        ReDim rc.総合得点(rc.出場選手数)
        ReDim rc.総合順位番号(rc.出場選手数)
        ReDim rc.総合順位表記(rc.出場選手数)

        rc.総合結果更新()



        'ブレイキンのみ使用
        'Public WIN数() As Integer
        'Public 勝ちFLAG() As Integer  '勝った方が１

        '順位法用
        'Public スケーティング結果_総合選手() As スケーティング結果_総合選手




        Return rc


    End Function





    Public Sub JSON書き出し()


        Dim filename As String = "S_採点結果_" & 区分番号 & "_" & ラウンド番号 & ".json"
        Dim filepath As String = マスタデータ.Z_システム設定.Comp_filepath


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

    Public Function JSON読み込み() As 採点結果_C

        Dim rc As 採点結果_C = Nothing

        Dim filepath As String = マスタデータ.Z_システム設定.Comp_filepath
        Dim filename As String = "S_採点結果_" & 区分番号 & "_" & ラウンド番号 & ".json"


        ''JSON読み込み用
        Dim jText As String = String.Empty


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した


            ''ファイルからJSONを読み込む
            ' Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)

            Dim fs As New System.IO.FileStream(filepath & "\" & filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite)
            'FileStreamを基にしたStringReaderのインスタンスを作成
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("shift_jis")

            Dim sr As New System.IO.StreamReader(fs, enc)

            'ファイルの内容をすべて読み込む
            jText = sr.ReadToEnd()



            'jText = cReader.ReadToEnd


            rc = JsonConvert.DeserializeObject(Of 採点結果_C)(jText)




            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            'cReader.Close()

            '閉じる
            'srを閉じれば、基になるfsも閉じられる
            sr.Close()


            'rc.filepath = filepath
        End If


        Return rc
    End Function



End Class


'*****************************************************************************
'   種目　クラス
'*****************************************************************************


Public Class 種目結果

    Public マスタデータ As マスタデータ

    Public 種目記号 As String

    Public 選手() As 選手結果

    Public 区分番号 As String
    Public ラウンド番号 As String
    Public 種目順 As String

    Public 審判員数 As Integer   'レフリーを含む
    Public 審判員数woRef As Integer
    Public ジャッジ記号リスト() As String
    Public ジャッジ表記名リスト() As String
    Public ジャッジRoleリスト() As String

    Public PCS担当ジャッジ数(10) As Integer

    Public レフリー番号 As Integer

    '種目結果
    Public 選手数 As Integer
    'Public 背番号() As String


    Public Sub New(区分番号_ As String, ラウンド番号_ As String, 種目順_ As Integer, マスタデータ_ As マスタデータ)

        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_
        種目順 = 種目順_
        マスタデータ = マスタデータ_


        Dim 背番号リスト(1) As String

        選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(種目順, 0, 背番号リスト)

        If 選手数 > 0 Then


            Dim 種目C As D_種目 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目順)

            If 種目C Is Nothing Then

            Else
                種目記号 = 種目C.種目記号

            End If

            Dim 担当審判グループ As Integer = 種目C.担当審判グループ
            審判員数 = 0
            審判員数woRef = 0

            For j = 1 To マスタデータ.審判員マスタ.Get_登録済み審判員数
                If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "0" And
                   マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "" Then
                    審判員数 = 審判員数 + 1
                End If
                If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "0" And
                   マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "" And
                    マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "R" Then
                    審判員数woRef = 審判員数woRef + 1
                End If

            Next j

            ReDim ジャッジ記号リスト(審判員数)
            ReDim ジャッジ表記名リスト(審判員数)
            ReDim ジャッジRoleリスト(審判員数)     '1, L, R  Lは審判長


            審判員数 = 0
            For j = 1 To マスタデータ.審判員マスタ.Get_登録済み審判員数
                If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "0" And
                   マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "" Then
                    審判員数 = 審判員数 + 1
                    ジャッジ記号リスト(審判員数) = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ記号
                    ジャッジ表記名リスト(審判員数) = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ表記名
                    ジャッジRoleリスト(審判員数) = マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ)
                End If
            Next j

            Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)


            'PCS担当ジャッジ数の確認 各種目1ヒート目のジャッジ数で確定する
            'Dim 曲番号 As Integer = マスタデータ.E_ヒート表マスタ.Get_曲番号(種目順, 1)
            For pcs = 0 To 9
                For j = 1 To マスタデータ.F_審判担当PCSマスタ.登録済みレコード数
                    If InStr(マスタデータ.F_審判担当PCSマスタ.リスト(j).担当PCS番号(種目順), CStr(pcs)) > 0 Then
                        If pcs = 0 Then
                            PCS担当ジャッジ数(10) = PCS担当ジャッジ数(10) + 1
                        Else
                            PCS担当ジャッジ数(pcs) = PCS担当ジャッジ数(pcs) + 1
                        End If
                    End If
                Next j
            Next pcs





            '選手結果のインスタンス作成
            Dim s As Integer

            ReDim 選手(選手数)

            For s = 1 To 選手数
                選手(s) = New 選手結果(背番号リスト(s), 審判員数, Me)

            Next s

            'レフリー番号の確定
            'レフリーの検索  選手(1)で検索する
            レフリー番号 = 0
            For j = 1 To UBound(選手(1).審判)
                If 選手(1).審判(j).ジャッジRole = "R" Then
                    レフリー番号 = j
                    j = UBound(選手(1).審判)
                End If
            Next j





            'ジャッジPCS採点対象(pcs) にフラグをたてる
            For pcs = 1 To 10
                For j = 1 To マスタデータ.F_審判担当PCSマスタ.登録済みレコード数
                    Dim jj As Integer '審判番号を探す
                    Dim ジャッジ番号 As Integer = 0
                    For jj = 1 To UBound(選手(1).審判)
                        If マスタデータ.F_審判担当PCSマスタ.リスト(j).ジャッジ記号 = 選手(1).審判(jj).ジャッジ記号 Then
                            ジャッジ番号 = jj
                            jj = UBound(選手(1).審判)
                        End If
                    Next jj

                    If ジャッジ番号 <> 0 Then
                        If pcs = 10 Then  'PCS番号が１０の時

                            If InStr(マスタデータ.F_審判担当PCSマスタ.リスト(j).担当PCS番号(種目順), "0") Then
                                '担当PCS番号に該当のPCS番号があったとき

                                '全ての選手クラスの審判クラスのジャッジPCS採点対象に１を入れる
                                For s = 1 To 選手数
                                    選手(s).審判(ジャッジ番号).ジャッジPCS採点対象(pcs) = 1
                                Next s

                            End If
                        Else  'PCS番号が１－９の時
                            If InStr(マスタデータ.F_審判担当PCSマスタ.リスト(j).担当PCS番号(種目順), CStr(pcs)) Then
                                '担当PCS番号に該当のPCS番号があったとき

                                '全ての選手クラスの審判クラスのジャッジPCS採点対象に１を入れる
                                For s = 1 To 選手数
                                    選手(s).審判(ジャッジ番号).ジャッジPCS採点対象(pcs) = 1
                                Next s

                            End If

                        End If
                    End If
                Next j
            Next pcs


            'ファイル読込み
            更新（）

        End If




    End Sub

    '採点ファイルの再読込み　種目毎
    Public Sub 更新()


        Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)


        'ファイルの読込み
        For j = 1 To 審判員数
            If 採点方式 = "チェック法" Then
                FileRead_JSON(区分番号, ラウンド番号, 種目記号, ジャッジ記号リスト(j))
            ElseIf 採点方式 = "順位法" Then
                FileRead_JSON(区分番号, ラウンド番号, 種目記号, ジャッジ記号リスト(j))
            ElseIf Strings.Left(採点方式, 4) = "BJS2" Then
                FileRead_BR2_JSON(区分番号, ラウンド番号, 種目記号, ジャッジ記号リスト(j))
            ElseIf Strings.Left(採点方式, 4) = "BJS3" Then
                FileRead_BR2_JSON(区分番号, ラウンド番号, 種目記号, ジャッジ記号リスト(j))
            ElseIf Strings.Left(採点方式, 4) = "BJPR" Then
                FileRead_JSON(区分番号, ラウンド番号, 種目記号, ジャッジ記号リスト(j))
            Else
                FileRead(区分番号, ラウンド番号, 種目記号, ジャッジ記号リスト(j))
            End If

        Next j



        '採点集計
        If Strings.Left(採点方式, 3) = "AJS" Then
            For s = 1 To UBound(選手)
                選手(s).採点_PCS_AJS30J()
            Next s

        ElseIf Strings.Left(採点方式, 4) = "BJS2" Then
            For s = 1 To UBound(選手)
                選手(s).採点_PCS_BJS20J()
            Next s

        ElseIf Strings.Left(採点方式, 4) = "BJS3" Then
            For s = 1 To UBound(選手)
                選手(s).採点_PCS_BJS20J()
            Next s


        ElseIf Strings.Left(採点方式, 3) = "BJS" Then
            For s = 1 To UBound(選手)
                選手(s).採点_PCS_BJS10J()
            Next s


        ElseIf Strings.Left(採点方式, 4) = "BJPR" Then
            For s = 1 To UBound(選手)
                選手(s).採点_PCS_BJPRE()
            Next s


        ElseIf 採点方式 = "チェック法" Then
            For s = 1 To UBound(選手)
                選手(s).採点_チェック法()
            Next s

        ElseIf 採点方式 = "順位法" Then
            For s = 1 To UBound(選手)
                選手(s).採点_順位法()
            Next s
        Else



        End If


        If 採点方式 = "順位法" Then
            '順位法はスケーティングで決定
            種目順位確定_順位法()


        Else

            '順位確定
            種目順位確定()

        End If



        'SendFLAGの更新


        For s = 1 To UBound(選手)

            '審判FLAGの検索
            Dim SENDFLAG As Boolean = True
            For j = 1 To UBound(選手(s).審判)   '審判員数は、その種目の担当審判 0 ブランク　では無い人数（レフリー含む）
                If 選手(s).審判(j).SendFlag <> 1 Then
                    SENDFLAG = False
                    j = UBound(選手(s).審判)
                End If
            Next j

            If SENDFLAG = True And UBound(選手(s).審判) > 0 Then
                選手(s).選手SEND済FLAG = True
            Else
                選手(s).選手SEND済FLAG = False
            End If
        Next s






    End Sub

    Private Sub 種目順位確定()
        ' 選手(s).種目得点 を使って順位を確定し、以下の項目に値を入れる
        ' 選手(s).種目順位番号 As Integer '同点無しの連番
        ' 選手(s).種目順位表記 As Integer '同点有り

        Dim 選手番号リスト() As Integer
        Dim 得点リスト() As Double

        ReDim 選手番号リスト(UBound(選手）)
        ReDim 得点リスト(UBound(選手))

        'Tempの得点リストの作成
        Dim Temp得点リスト() As Double
        ReDim Temp得点リスト(UBound(選手))

        For s = 1 To UBound(選手)
            Temp得点リスト(s) = 選手(s).種目得点
        Next s


        Dim 最大得点選手番号 As Integer = 0
        Dim 最大得点 As Double

        '並べ替え
        For 降順 = 1 To UBound(選手)
            最大得点 = -1
            For s = 1 To UBound(選手)
                If 最大得点 < Temp得点リスト(s) Then
                    最大得点 = Temp得点リスト(s)
                    最大得点選手番号 = s
                End If
            Next s
            選手番号リスト(降順) = 最大得点選手番号
            得点リスト(降順) = 最大得点
            Temp得点リスト(最大得点選手番号) = -1
        Next 降順

        Dim 前の選手の得点 As Double = -1
        Dim 前の選手の順位 As Integer = 0
        For 降順 = 1 To UBound(選手)
            選手(選手番号リスト(降順)).種目順位番号 = 降順
            If 前の選手の得点 = 得点リスト(降順) Then
                '同点の時
                選手(選手番号リスト(降順)).種目順位表記 = 前の選手の順位
            Else
                '同点じゃない時
                選手(選手番号リスト(降順)).種目順位表記 = 降順
            End If
            前の選手の順位 = 選手(選手番号リスト(降順)).種目順位表記
            前の選手の得点 = 得点リスト(降順)
        Next 降順

    End Sub

    Private Sub 種目順位確定_順位法()


        Dim スケーティング As スケーティング単科_C
        スケーティング = New スケーティング単科_C(選手数, 審判員数woRef)



        スケーティング.ORG_スケーティング.ジャッジ過半数 = Math.Ceiling((審判員数woRef + 1) / 2)  '切り上げ

        For s = 1 To 選手数
            スケーティング.ORG_スケーティング.ORG_スケーティング_選手素点(s).背番号 = 選手(s).背番号
            For j = 1 To 審判員数woRef
                スケーティング.ORG_スケーティング.ORG_スケーティング_選手素点(s).選手素点(j) = 選手(s).審判(j).素点
            Next j
        Next s


        スケーティング.単科スケーティング計算()


        '結果の挿入
        For s = 1 To 選手数

            選手(s).種目順位表記 = スケーティング.スケーティング結果.スケーティング結果_選手(s).順位

            選手(s).スケーティング結果_選手 = スケーティング.スケーティング結果.スケーティング結果_選手(s)
        Next s


        '同点無の連番を　総合順位番号に埋める
        Dim 順位番号 As Integer = 1

        For 順位 = 0 To 選手数
            For s = 1 To 選手数

                If 選手(s).種目順位表記 = 順位 Then
                    選手(s).種目順位番号 = 順位番号
                    順位番号 = 順位番号 + 1
                End If

            Next s
        Next 順位



        スケーティング = Nothing


    End Sub



    ''' 採点ファイルの読込み
    ''' 読み込んで、選手().審判（）.PCS素点 に格納する
    Private Sub FileRead(区分番号 As String, ラウンド番号 As String, 種目記号 As String, ジャッジ記号 As String)

        Dim filename As String

        'Dim filepath = "C:\Users\IBM_ADMIN\Box Sync\90_JDSF\08_新審判基準委員会\40_総合支援システム\TestData\123456"
        Dim filepath = マスタデータ.Z_システム設定.Comp_filepath

        区分番号 = String.Format("{0:D2}", CInt(区分番号))

        filename = "S_" & 区分番号 & "_" & ラウンド番号 & "_" & 種目記号 & "_" & ジャッジ記号 & ".csv"

        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない

            '該当種目、全選手　該当審判の採点をクリアする
            For i = 1 To UBound(選手)

                '該当のジャッジクラスを探す
                For j = 1 To UBound(選手(i).審判)
                    If ジャッジ記号 = 選手(i).審判(j).ジャッジ記号 Then

                        選手(i).審判(j).SendFlag = 0

                        'PCS素点の登録
                        For p = 1 To 10
                            選手(i).審判(j).PCS素点(p) = 0
                        Next p

                        '減点の登録
                        For g = 1 To 20
                            選手(i).審判(j).減点素点(g) = 0
                        Next g
                    End If
                Next j

            Next i




        Else
            'ファイルが存在した

            'すでに書き込みでオープン中のファイルもオープンするために FileShare.ReadWriteを指定する
            Dim fs As New FileStream(filepath & "\" & filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)

            ' StreamReader の新しいインスタンスを生成する
            Dim cReader As New System.IO.StreamReader(fs, System.Text.Encoding.Default)

            ' 読み込んだ結果をすべて格納するための変数を宣言する
            Dim stResult As String = String.Empty


            ' 読み込みできる文字がなくなるまで繰り返す

            While (cReader.Peek() >= 0)
                ' ファイルを 1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()

                'ファイルの１行目はヘッダーなので読み込まない
                If Left(stBuffer, 3) = "背番号" Then

                Else
                    '読み込んだ１行分のデータを登録する


                    'カンマでセパレート
                    Dim arBuffer = stBuffer.Split(",")

                    If UBound(arBuffer) >= 1 Then

                        Dim _背番号 = arBuffer(0)

                        '選手クラスの中で該当背番号の選手クラスを探す
                        For i = 1 To UBound(選手)
                            If _背番号 = 選手(i).背番号 Then

                                '該当のジャッジクラスを探す
                                For j = 1 To UBound(選手(i).審判)
                                    If ジャッジ記号 = 選手(i).審判(j).ジャッジ記号 Then

                                        選手(i).審判(j).SendFlag = arBuffer(1)

                                        'PCS素点の登録
                                        For p = 1 To 10
                                            選手(i).審判(j).PCS素点(p) = arBuffer(p + 1)
                                        Next p

                                        '減点の登録
                                        For g = 1 To 20
                                            選手(i).審判(j).減点素点(g) = arBuffer(11 + g)
                                        Next g


                                        j = UBound(選手(i).審判)
                                    End If

                                Next j

                                i = UBound(選手)
                            End If

                        Next i

                    End If

                End If

            End While


            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()

            'fsも閉じる
            fs.Close()


        End If



    End Sub

    Private Sub FileRead_JSON(区分番号 As String, ラウンド番号 As String, 種目記号 As String, ジャッジ記号 As String)

        '===========================
        '概要　チェック法・順位法等　JSONファイルの読み込み  '   　 
        '入力　
        '出力　選手(i).審判(j).素点・備考 へのデータセット
        '      
        '===========================

        Dim filepath = マスタデータ.Z_システム設定.Comp_filepath

        区分番号 = String.Format("{0:D2}", CInt(区分番号))

        Dim ジャッジ結果 As S_採点結果_J
        ジャッジ結果 = New S_採点結果_J(filepath)

        ジャッジ結果.区分番号 = 区分番号
        ジャッジ結果.ラウンド番号 = ラウンド番号
        ジャッジ結果.種目記号 = 種目記号
        ジャッジ結果.ジャッジ記号 = ジャッジ記号


        ジャッジ結果 = ジャッジ結果.新JSON読み込み()
        If ジャッジ結果 Is Nothing Then
            ジャッジ結果 = New S_採点結果_J(filepath)

            ジャッジ結果.区分番号 = 区分番号
            ジャッジ結果.ラウンド番号 = ラウンド番号
            ジャッジ結果.種目記号 = 種目記号
            ジャッジ結果.ジャッジ記号 = ジャッジ記号

        Else


        End If



        ジャッジ結果.Set_filepath(filepath)

        Dim 選手数 As Integer = ジャッジ結果.Get_選手数


        If 選手数 > 0 Then


            '選手クラスの中で該当背番号の選手クラスを探す
            For s = 1 To 選手数
                For i = 1 To UBound(選手)

                    If ジャッジ結果.S_採点結果_選手_J(s).背番号 = 選手(i).背番号 Then


                        '該当のジャッジクラスを探す
                        For j = 1 To UBound(選手(i).審判)
                            If ジャッジ記号 = 選手(i).審判(j).ジャッジ記号 Then

                                If Strings.Left(ジャッジ結果.採点方式, 4) = "BJPR" Then
                                    'BJPRの時はヒート毎SENDなので、選手毎にSENDFLAGを見る
                                    選手(i).審判(j).SendFlag = ジャッジ結果.S_採点結果_選手_J(s).SEND_FLAG

                                Else
                                    選手(i).審判(j).SendFlag = ジャッジ結果.SEND_FLAG

                                End If


                                '得点の登録
                                選手(i).審判(j).素点 = ジャッジ結果.S_採点結果_選手_J(s).点数

                                '備考の登録
                                選手(i).審判(j).備考 = ジャッジ結果.S_採点結果_選手_J(s).備考


                                j = UBound(選手(i).審判)
                            End If

                        Next j

                        i = UBound(選手)
                    End If

                Next i

            Next s


        End If



    End Sub


    Private Sub FileRead_BR2_JSON(区分番号 As String, ラウンド番号 As String, 種目記号 As String, ジャッジ記号 As String)

        '===========================
        '概要　BJS20J　用　JSONファイルの読み込み  '   　 
        '入力　
        '出力　選手(i).審判(j).素点・備考 へのデータセット
        '      
        '===========================

        Dim filepath = マスタデータ.Z_システム設定.Comp_filepath

        区分番号 = String.Format("{0:D2}", CInt(区分番号))

        Dim ジャッジ結果 As S_採点結果_BR2_J
        ジャッジ結果 = New S_採点結果_BR2_J(filepath)
        ジャッジ結果.区分番号 = 区分番号
        ジャッジ結果.ラウンド番号 = ラウンド番号
        ジャッジ結果.種目記号 = 種目記号
        ジャッジ結果.ジャッジ記号 = ジャッジ記号

        Dim 選手数 As Integer = 0

        'If 種目順 = "2" Then
        If CInt(種目順) <= マスタデータ.J_新審判設定.勝敗ラウンド数 And CInt(種目順) Mod 2 = 0 Then

            ジャッジ結果.種目記号 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, CInt(種目順) - 1).種目記号
            選手数 = ジャッジ結果.JSON読み込み()
            ジャッジ結果.種目記号 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, CInt(種目順) - 1).種目記号
        Else
            選手数 = ジャッジ結果.JSON読み込み()
        End If



        If 選手数 > 0 Then


            '選手クラスの中で該当背番号の選手クラスを探す
            For s = 1 To 選手数
                For i = 1 To UBound(選手)

                    If ジャッジ結果.BR2_種目結果_J(1).BR2_選手結果_J(s).背番号 = 選手(i).背番号 Then


                        '該当のジャッジクラスを探す
                        For j = 1 To UBound(選手(i).審判)
                            If ジャッジ記号 = 選手(i).審判(j).ジャッジ記号 Then

                                選手(i).審判(j).SendFlag = ジャッジ結果.SEND_FLAG

                                For p = 1 To ジャッジ結果.PCS数
                                    '得点の登録
                                    'If 種目順 = 2 Then
                                    If CInt(種目順) <= マスタデータ.J_新審判設定.勝敗ラウンド数 And CInt(種目順) Mod 2 = 0 Then
                                        選手(i).審判(j).PCS素点(p) = ジャッジ結果.BR2_種目結果_J(2).BR2_選手結果_J(s).PCS得点(p).PCS素点

                                    Else
                                        選手(i).審判(j).PCS素点(p) = ジャッジ結果.BR2_種目結果_J(1).BR2_選手結果_J(s).PCS得点(p).PCS素点
                                    End If
                                Next p

                                For r = 1 To ジャッジ結果.減点項目数
                                    '減点の登録
                                    'If 種目順 = 2 Then
                                    If CInt(種目順) <= マスタデータ.J_新審判設定.勝敗ラウンド数 And CInt(種目順) Mod 2 = 0 Then
                                        選手(i).審判(j).減点素点(r) = ジャッジ結果.BR2_種目結果_J(2).BR2_選手結果_J(s).減点(r).減点
                                    Else
                                        選手(i).審判(j).減点素点(r) = ジャッジ結果.BR2_種目結果_J(1).BR2_選手結果_J(s).減点(r).減点
                                    End If
                                Next r


                                j = UBound(選手(i).審判)
                            End If

                        Next j

                        i = UBound(選手)
                    End If

                Next i

            Next s


        End If



    End Sub



End Class

'*****************************************************************************
'   選手結果　クラス
'*****************************************************************************


Public Class 選手結果

    Private 種目Class As 種目結果


    Public 背番号 As String

    Public 種目得点 As Decimal  '種目毎、選手毎の得点（PCS+減点）
    Public 種目順位番号 As Integer '同点無しの連番
    Public 種目順位表記 As Integer '同点有り


    Public 種目各PCS得点(10) As Decimal
    Public 種目各減点(20)
    Public 失格FLAG As Boolean

    Public 選手SEND済FLAG As Boolean

    Public 審判() As 審判員結果

    Public 種目順位点 As Decimal ' ブレイキンセレクション用


    'スケーティング用
    Public スケーティング結果_選手 As スケーティング結果_選手



    Public Sub New(背番号_ As String, 審判員数 As Integer, 種目Class_ As 種目結果)

        背番号 = 背番号_
        種目Class = 種目Class_

        選手SEND済FLAG = False

        ReDim 審判(審判員数)

        For i = 1 To 審判員数
            審判(i) = New 審判員結果(種目Class.ジャッジ記号リスト(i), 種目Class.ジャッジ表記名リスト(i), 種目Class.ジャッジRoleリスト(i))
        Next i




    End Sub

    Public Sub 採点_PCS_AJS30J()
        '===========================
        '概要　AJS3.0でのPCS採点。  １選手の10PCS の結果     '   　 
        '入力　審判Class(各審判員のPCS採点、ジャッジ人数
        '出力　種目各PCS得点(10)  
        '      1PCS毎：    PCS採点(ジャッジごと）, 除外リスト, 乖離度リスト
        '===========================

        'PCSの採点
        Dim PCS採点() As Double  '審判Class分作成する　　レフリーも含む

        Dim 除外範囲 = 1.5



        For pcs = 1 To 10

            If 種目Class.PCS担当ジャッジ数(pcs) > 0 Then


                'PCSの採点配列を作成
                ReDim PCS採点(UBound(審判))

                For j = 1 To UBound(審判)
                    If 審判(j).ジャッジPCS採点対象(pcs) = 1 Then
                        PCS採点(j) = 審判(j).PCS素点(pcs)
                    End If
                Next j

                'PCS採点のソート
                Array.Sort(PCS採点)
                '降順に並べ替え
                Array.Reverse(PCS採点)

                'ソート後は配列番号 0　から始まるので注意

                '採点対象審判員数から中間値を算出
                Dim 採点対象審判員人数 As Integer = 種目Class.PCS担当ジャッジ数(pcs)
                Dim 中間点 As Double = 0

                If (採点対象審判員人数 Mod 2) = 0 Then
                    '審判員が偶数
                    中間点 = 中間点 + PCS採点(採点対象審判員人数 \ 2 - 1)
                    中間点 = 中間点 + PCS採点(採点対象審判員人数 \ 2 + 1 - 1)
                    中間点 = 中間点 / 2
                Else
                    '審判員が偶数
                    中間点 = PCS採点(採点対象審判員人数 \ 2 + 1 - 1)
                End If

                '除外対象の確定とPCS得点の集計
                Dim PCS合計点 As Double = 0
                Dim 範囲内ジャッジ人数 As Integer = 0
                Dim 乖離度 As Double = 0
                For j = 1 To UBound(審判)
                    If 審判(j).ジャッジPCS採点対象(pcs) = 1 Then
                        If (審判(j).PCS素点(pcs) > 除外範囲 + 中間点 Or 審判(j).PCS素点(pcs) < 中間点 - 除外範囲) Then
                            '採点対象で、除外範囲を超えていたら
                            審判(j).PCS無効FLAG(pcs) = True
                        Else
                            '採点対象で、除外範囲を超えていない場合
                            PCS合計点 = PCS合計点 + 審判(j).PCS素点(pcs)
                            範囲内ジャッジ人数 = 範囲内ジャッジ人数 + 1
                            審判(j).PCS無効FLAG(pcs) = False
                        End If

                        乖離度 = 0
                        If 中間点 >= 審判(j).PCS素点(pcs) Then
                            乖離度 = (1 / (1 + (中間点 - 審判(j).PCS素点(pcs)) * (中間点 - 審判(j).PCS素点(pcs))))
                        Else
                            乖離度 = (1 / (1 + (審判(j).PCS素点(pcs) - 中間点) * (審判(j).PCS素点(pcs) - 中間点)))
                        End If
                        審判(j).PCS乖離度(pcs) = 乖離度
                    End if
                Next j


                If 範囲内ジャッジ人数 > 0 Then
                    '通常のばあい
                    種目各PCS得点(pcs) = PCS合計点 / 範囲内ジャッジ人数

                    '小数点４桁目を四捨五入     Ver1.02.19で追加
                    種目各PCS得点(pcs) = Math.Round(種目各PCS得点(pcs), 3, MidpointRounding.AwayFromZero)



                Else
                    '全員が範囲外の場合
                    For j = 1 To UBound(審判)
                        If 審判(j).ジャッジPCS採点対象(pcs) = 1 Then
                            '採点対象での場合
                            PCS合計点 = PCS合計点 + 審判(j).PCS素点(pcs)
                            範囲内ジャッジ人数 = 範囲内ジャッジ人数 + 1
                        End If
                    Next j
                    種目各PCS得点(pcs) = PCS合計点 / 範囲内ジャッジ人数

                    '小数点４桁目を四捨五入　　　Ver1.02.19で追加
                    種目各PCS得点(pcs) = Math.Round(種目各PCS得点(pcs), 3, MidpointRounding.AwayFromZero)


                End If


            End If
        Next pcs

        '減点の計算

        'レフリーがいるか確認
        If 種目Class.レフリー番号 <> 0 Then
            For d = 1 To 20
                種目各減点(d) = 審判(種目Class.レフリー番号).減点素点(d)
            Next d

            '失格判定
            If 審判(種目Class.レフリー番号).減点素点(1) = 1 Then
                失格FLAG = True
            Else
                失格FLAG = False
            End If
        End If




        '選手得点の計算
        種目得点 = 0
        For pcs = 1 To 10
            If 種目Class.PCS担当ジャッジ数(pcs) > 0 Then

                'PCS倍率の取得
                Dim 倍率 As Double = 種目Class.マスタデータ.J_新審判設定.PCS設定(pcs).倍率

                種目得点 = 種目得点 + (種目各PCS得点(pcs) * 倍率)
            End If
        Next pcs

        '===減点を足しこみ
        For d = 2 To 10
            種目得点 = 種目得点 + 種目各減点(d)
        Next d

        '減点が多すぎてマイナスになった時は０にする
        If 種目得点 < 0 Then
            種目得点 = 0
        End If


        '失格判定
        If 失格FLAG = True Then
            種目得点 = 0
        End If


        '表示用の合計計算
        For j = 1 To UBound(審判)
            審判(j).表示用PCS合計点 = 0
            審判(j).表示用減点合計点 = 0
            For pcs = 1 To 10
                審判(j).表示用PCS合計点 = 審判(j).表示用PCS合計点 + 審判(j).PCS素点(pcs)
            Next pcs
            For 減点番号 = 2 To 20
                審判(j).表示用減点合計点 = 審判(j).表示用減点合計点 + 審判(j).減点素点(減点番号)
            Next 減点番号
        Next j

    End Sub




    Public Sub 採点_PCS_BJS10J()
        '===========================
        'ブレイキン　
        '概要　BJS1.0でのPCS採点。  １選手の10PCS の結果     '   　 
        '入力　審判Class(各審判員のPCS採点、ジャッジ人数
        '出力　種目各PCS得点(10)  
        '      1PCS毎：    PCS採点(ジャッジごと）, 除外リスト, 乖離度リスト
        '===========================

        'PCSの採点
        Dim PCS採点() As Double  '審判Class分作成する　　レフリーも含む

        Dim 除外範囲 = 1.5



        For pcs = 1 To 10

            If 種目Class.PCS担当ジャッジ数(pcs) > 0 Then


                'PCSの採点配列を作成
                ReDim PCS採点(UBound(審判))

                For j = 1 To UBound(審判)
                    If 審判(j).ジャッジPCS採点対象(pcs) = 1 Then
                        PCS採点(j) = 審判(j).PCS素点(pcs)
                    End If
                Next j

                'PCS採点のソート
                Array.Sort(PCS採点)
                '降順に並べ替え
                Array.Reverse(PCS採点)

                'ソート後は配列番号 0　から始まるので注意

                '採点対象審判員数から中間値を算出
                Dim 採点対象審判員人数 As Integer = 種目Class.PCS担当ジャッジ数(pcs)
                Dim 中間点 As Double = 0

                If (採点対象審判員人数 Mod 2) = 0 Then
                    '審判員が偶数
                    中間点 = 中間点 + PCS採点(採点対象審判員人数 \ 2 - 1)
                    中間点 = 中間点 + PCS採点(採点対象審判員人数 \ 2 + 1 - 1)
                    中間点 = 中間点 / 2
                Else
                    '審判員が偶数
                    中間点 = PCS採点(採点対象審判員人数 \ 2 + 1 - 1)
                End If

                '除外対象の確定とPCS得点の集計
                Dim PCS合計点 As Double = 0
                Dim 範囲内ジャッジ人数 As Integer = 0
                Dim 乖離度 As Double = 0
                For j = 1 To UBound(審判)
                    If 審判(j).ジャッジPCS採点対象(pcs) = 1 Then
                        If (審判(j).PCS素点(pcs) > 除外範囲 + 中間点 Or 審判(j).PCS素点(pcs) < 中間点 - 除外範囲) Then
                            '採点対象で、除外範囲を超えていたら
                            審判(j).PCS無効FLAG(pcs) = True
                        Else
                            '採点対象で、除外範囲を超えていない場合
                            PCS合計点 = PCS合計点 + 審判(j).PCS素点(pcs)
                            範囲内ジャッジ人数 = 範囲内ジャッジ人数 + 1
                            審判(j).PCS無効FLAG(pcs) = False
                        End If

                        乖離度 = 0
                        If 中間点 >= 審判(j).PCS素点(pcs) Then
                            乖離度 = (1 / (1 + (中間点 - 審判(j).PCS素点(pcs)) * (中間点 - 審判(j).PCS素点(pcs))))
                        Else
                            乖離度 = (1 / (1 + (審判(j).PCS素点(pcs) - 中間点) * (審判(j).PCS素点(pcs) - 中間点)))
                        End If
                        審判(j).PCS乖離度(pcs) = 乖離度
                    End If
                Next j


                If 範囲内ジャッジ人数 > 0 Then
                    '通常のばあい
                    種目各PCS得点(pcs) = PCS合計点 / 範囲内ジャッジ人数
                Else
                    '全員が範囲外の場合
                    For j = 1 To UBound(審判)
                        If 審判(j).ジャッジPCS採点対象(pcs) = 1 Then
                            '採点対象での場合
                            PCS合計点 = PCS合計点 + 審判(j).PCS素点(pcs)
                            範囲内ジャッジ人数 = 範囲内ジャッジ人数 + 1
                        End If
                    Next j
                    種目各PCS得点(pcs) = PCS合計点 / 範囲内ジャッジ人数
                End If


            End If
        Next pcs

        '減点の計算

        '
        '２人以上減点している場合には採用
        Dim 判定人数 As Integer = 2   'この人数以上減点・加点している場合には採用

        For d = 1 To 20

            '初期化
            種目各減点(d) = 0

            '加点・減点を判定
            Dim 最大値 As Double = 0
            Dim 最小値 As Double = 0

            For j = 1 To UBound(審判)
                If 最大値 < 審判(j).減点素点(d) Then
                    最大値 = 審判(j).減点素点(d)
                End If

                If 最小値 > 審判(j).減点素点(d) Then
                    最小値 = 審判(j).減点素点(d)
                End If
            Next j

            If 最大値 > 0 Then    '加点の場合
                Dim 判定値 As Double = 最大値

                While 判定値 > 0

                    '判定値を入れた人が判定人数(2人)以上いるか確認
                    Dim 人数 As Integer = 0
                    For j = 1 To UBound(審判)
                        If 判定値 <= 審判(j).減点素点(d) Then
                            人数 = 人数 + 1
                        End If
                    Next j

                    If 人数 >= 判定人数 Then
                        種目各減点(d) = 判定値

                        判定値 = 0

                    Else  '判定人数以上いない場合
                        '次の判定値を探す
                        Dim 次判定値 As Double = 0
                        For j = 1 To UBound(審判)
                            If 審判(j).減点素点(d) < 判定値 And 審判(j).減点素点(d) > 0 And 審判(j).減点素点(d) > 次判定値 Then
                                次判定値 = 審判(j).減点素点(d)
                            End If
                        Next j

                        If 次判定値 > 0 Then
                            判定値 = 次判定値
                        Else
                            判定値 = 0
                        End If

                    End If

                End While

            ElseIf 最小値 < 0 Then '減点の場合
                Dim 判定値 As Double = 最小値

                While 判定値 < 0

                    '判定値を入れた人が判定人数(2人)以上いるか確認
                    Dim 人数 As Integer = 0
                    For j = 1 To UBound(審判)
                        If 判定値 >= 審判(j).減点素点(d) Then
                            人数 = 人数 + 1
                        End If
                    Next j

                    If 人数 >= 判定人数 Then
                        種目各減点(d) = 判定値

                        判定値 = 0
                    Else  '判定人数以上いない場合
                        '次の判定値を探す
                        Dim 次判定値 As Double = 0
                        For j = 1 To UBound(審判)
                            If 審判(j).減点素点(d) > 判定値 And 審判(j).減点素点(d) < 0 And 審判(j).減点素点(d) < 次判定値 Then
                                次判定値 = 審判(j).減点素点(d)
                            End If
                        Next j

                        If 次判定値 < 0 Then
                            判定値 = 次判定値
                        Else
                            判定値 = 0
                        End If

                    End If

                End While

            End If

        Next d



        '失格判定
        If 種目各減点(1) = 1 Then
            失格FLAG = True
        Else
            失格FLAG = False
        End If




        '選手得点の計算
        種目得点 = 0
        For pcs = 1 To 10
            If 種目Class.PCS担当ジャッジ数(pcs) > 0 Then

                'PCS倍率の取得
                Dim 倍率 As Double = 種目Class.マスタデータ.J_新審判設定.PCS設定(pcs).倍率

                種目得点 = 種目得点 + (種目各PCS得点(pcs) * 倍率)
            End If
        Next pcs

        '===減点を足しこみ
        For d = 2 To 10
            種目得点 = 種目得点 + 種目各減点(d)
        Next d

        '減点が多すぎてマイナスになった時は０にする
        If 種目得点 < 0 Then
            種目得点 = 0
        End If


        '失格判定
        If 失格FLAG = True Then
            種目得点 = 0
        End If


        '表示用の合計計算
        For j = 1 To UBound(審判)
            審判(j).表示用PCS合計点 = 0
            審判(j).表示用減点合計点 = 0
            For pcs = 1 To 10
                審判(j).表示用PCS合計点 = 審判(j).表示用PCS合計点 + 審判(j).PCS素点(pcs)
            Next pcs
            For 減点番号 = 2 To 20
                審判(j).表示用減点合計点 = 審判(j).表示用減点合計点 + 審判(j).減点素点(減点番号)
            Next 減点番号
        Next j

    End Sub

    Public Sub 採点_PCS_BJS20J()
        '===========================
        'ブレイキン　
        '概要　BJS2.0でのPCS採点。  １選手の10PCS の結果     '   　 
        '入力　審判Class(各審判員のPCS採点、ジャッジ人数
        '出力　種目各PCS得点(10)  
        '      1PCS毎：    PCS採点(ジャッジごと）, 除外リスト, 乖離度リスト
        '===========================

        'PCSの採点
        Dim PCS採点() As Double  '審判Class分作成する　　レフリーも含む

        'Dim 除外範囲 As Decimal = 0.15   '15%
        Dim 除外範囲 As Decimal = 1   '100%   除外しない。　2021/08/26修正 


        For pcs = 1 To 種目Class.マスタデータ.J_新審判設定.GetPCS数

            Dim PCS除外範囲 As Decimal = 除外範囲 * 種目Class.マスタデータ.J_新審判設定.PCS設定(pcs).PCS最大値

            If 種目Class.PCS担当ジャッジ数(pcs) > 0 Then


                'PCSの採点配列を作成
                ReDim PCS採点(UBound(審判))

                For j = 1 To UBound(審判)
                    If 審判(j).ジャッジPCS採点対象(pcs) = 1 Then
                        PCS採点(j) = 審判(j).PCS素点(pcs)
                    End If
                Next j

                'PCS採点のソート
                Array.Sort(PCS採点)
                '降順に並べ替え
                Array.Reverse(PCS採点)

                'ソート後は配列番号 0　から始まるので注意

                '採点対象審判員数から中間値を算出
                Dim 採点対象審判員人数 As Integer = 種目Class.PCS担当ジャッジ数(pcs)
                Dim 中間点 As Double = 0

                If (採点対象審判員人数 Mod 2) = 0 Then
                    '審判員が偶数
                    中間点 = 中間点 + PCS採点(採点対象審判員人数 \ 2 - 1)
                    中間点 = 中間点 + PCS採点(採点対象審判員人数 \ 2 + 1 - 1)
                    中間点 = 中間点 / 2
                Else
                    '審判員が偶数
                    中間点 = PCS採点(採点対象審判員人数 \ 2 + 1 - 1)
                End If

                '除外対象の確定とPCS得点の集計
                Dim PCS合計点 As Double = 0
                Dim 範囲内ジャッジ人数 As Integer = 0
                Dim 乖離度 As Double = 0
                For j = 1 To UBound(審判)
                    If 審判(j).ジャッジPCS採点対象(pcs) = 1 Then
                        If (審判(j).PCS素点(pcs) > PCS除外範囲 + 中間点 Or 審判(j).PCS素点(pcs) < 中間点 - PCS除外範囲) Then
                            '採点対象で、除外範囲を超えていたら
                            審判(j).PCS無効FLAG(pcs) = True
                        Else
                            '採点対象で、除外範囲を超えていない場合
                            PCS合計点 = PCS合計点 + 審判(j).PCS素点(pcs)
                            範囲内ジャッジ人数 = 範囲内ジャッジ人数 + 1
                            審判(j).PCS無効FLAG(pcs) = False
                        End If

                        乖離度 = 0
                        If 中間点 >= 審判(j).PCS素点(pcs) Then
                            乖離度 = (1 / (1 + (中間点 - 審判(j).PCS素点(pcs)) * (中間点 - 審判(j).PCS素点(pcs))))
                        Else
                            乖離度 = (1 / (1 + (審判(j).PCS素点(pcs) - 中間点) * (審判(j).PCS素点(pcs) - 中間点)))
                        End If
                        審判(j).PCS乖離度(pcs) = 乖離度
                    End If
                Next j


                If 範囲内ジャッジ人数 > 0 Then
                    '通常のばあい
                    種目各PCS得点(pcs) = PCS合計点 / 範囲内ジャッジ人数

                    '小数点４桁目を四捨五入
                    種目各PCS得点(pcs) = Math.Round(種目各PCS得点(pcs), 3, MidpointRounding.AwayFromZero)

                Else
                    '全員が範囲外の場合
                    For j = 1 To UBound(審判)
                        If 審判(j).ジャッジPCS採点対象(pcs) = 1 Then
                            '採点対象での場合
                            PCS合計点 = PCS合計点 + 審判(j).PCS素点(pcs)
                            範囲内ジャッジ人数 = 範囲内ジャッジ人数 + 1
                        End If
                    Next j
                    種目各PCS得点(pcs) = PCS合計点 / 範囲内ジャッジ人数

                    '小数点４桁目を四捨五入
                    種目各PCS得点(pcs) = Math.Round(種目各PCS得点(pcs), 3, MidpointRounding.AwayFromZero)
                End If


            End If
        Next pcs

        '減点の計算

        '
        '２人以上減点している場合には採用
        Dim 判定人数 As Integer = 2   'この人数以上減点・加点している場合には採用

        For d = 1 To 20

            '初期化
            種目各減点(d) = 0

            '加点・減点を判定
            Dim 最大値 As Double = 0
            Dim 最小値 As Double = 0

            For j = 1 To UBound(審判)
                If 最大値 < 審判(j).減点素点(d) Then
                    最大値 = 審判(j).減点素点(d)
                End If

                If 最小値 > 審判(j).減点素点(d) Then
                    最小値 = 審判(j).減点素点(d)
                End If
            Next j

            If 最大値 > 0 Then    '加点の場合
                Dim 判定値 As Double = 最大値

                While 判定値 > 0

                    '判定値を入れた人が判定人数(2人)以上いるか確認
                    Dim 人数 As Integer = 0
                    For j = 1 To UBound(審判)
                        If 判定値 <= 審判(j).減点素点(d) Then
                            人数 = 人数 + 1
                        End If
                    Next j

                    If 人数 >= 判定人数 Then
                        種目各減点(d) = 判定値

                        判定値 = 0

                    Else  '判定人数以上いない場合
                        '次の判定値を探す
                        Dim 次判定値 As Double = 0
                        For j = 1 To UBound(審判)
                            If 審判(j).減点素点(d) < 判定値 And 審判(j).減点素点(d) > 0 And 審判(j).減点素点(d) > 次判定値 Then
                                次判定値 = 審判(j).減点素点(d)
                            End If
                        Next j

                        If 次判定値 > 0 Then
                            判定値 = 次判定値
                        Else
                            判定値 = 0
                        End If

                    End If

                End While

            ElseIf 最小値 < 0 Then '減点の場合
                Dim 判定値 As Double = 最小値

                While 判定値 < 0

                    '判定値を入れた人が判定人数(2人)以上いるか確認
                    Dim 人数 As Integer = 0
                    For j = 1 To UBound(審判)
                        If 判定値 >= 審判(j).減点素点(d) Then
                            人数 = 人数 + 1
                        End If
                    Next j

                    If 人数 >= 判定人数 Then
                        種目各減点(d) = 判定値

                        判定値 = 0
                    Else  '判定人数以上いない場合
                        '次の判定値を探す
                        Dim 次判定値 As Double = 0
                        For j = 1 To UBound(審判)
                            If 審判(j).減点素点(d) > 判定値 And 審判(j).減点素点(d) < 0 And 審判(j).減点素点(d) < 次判定値 Then
                                次判定値 = 審判(j).減点素点(d)
                            End If
                        Next j

                        If 次判定値 < 0 Then
                            判定値 = 次判定値
                        Else
                            判定値 = 0
                        End If

                    End If

                End While

            End If

        Next d



        '失格判定
        If 種目各減点(1) = 1 Then
            失格FLAG = True
        Else
            失格FLAG = False
        End If




        '選手得点の計算
        種目得点 = 0
        For pcs = 1 To 10
            If 種目Class.PCS担当ジャッジ数(pcs) > 0 Then

                If 種目Class.マスタデータ.J_新審判設定.PCS設定(pcs) Is Nothing Then

                    '
                    MsgBox("PCS番号:" & pcs & " がジャッジの担当PCSとして設定されていますが、" & 種目Class.マスタデータ.J_新審判設定.新審判基準VER & "では、PCS数は" & 種目Class.マスタデータ.J_新審判設定.GetPCS数 & "となっています。新審判設定ファイルの整合性を確認してください。")

                Else
                    'PCS倍率の取得
                    Dim 倍率 As Double = 種目Class.マスタデータ.J_新審判設定.PCS設定(pcs).倍率

                    種目得点 = 種目得点 + (種目各PCS得点(pcs) * 倍率)

                End If


            End If
        Next pcs

        '===減点を足しこみ
        For d = 2 To 10
            種目得点 = 種目得点 + 種目各減点(d)
        Next d

        '減点が多すぎてマイナスになった時は０にする
        If 種目得点 < 0 Then
            種目得点 = 0
        End If


        '失格判定
        If 失格FLAG = True Then
            種目得点 = 0
        End If


        '表示用の合計計算
        For j = 1 To UBound(審判)
            審判(j).表示用PCS合計点 = 0
            審判(j).表示用減点合計点 = 0
            For pcs = 1 To 10
                審判(j).表示用PCS合計点 = 審判(j).表示用PCS合計点 + 審判(j).PCS素点(pcs)
            Next pcs
            For 減点番号 = 2 To 20
                審判(j).表示用減点合計点 = 審判(j).表示用減点合計点 + 審判(j).減点素点(減点番号)
            Next 減点番号
        Next j

    End Sub



    Public Sub 採点_PCS_BJPRE()
        '===========================
        'ブレイキン　プレセレクション
        '概要　プレセレクション　1人　0点から10点　ジャッジの平均点  '   　 
        '入力　審判Class(各審判員のPCS採点、ジャッジ人数
        '出力　種目各PCS得点(10)  
        '      1PCS毎：    PCS採点(ジャッジごと）, 除外リスト, 乖離度リスト
        '===========================

        '選手得点の計算
        種目得点 = 0

        For j = 1 To UBound(審判)
            種目得点 = 種目得点 + 審判(j).素点
        Next j

        Dim 審判員数 As Integer = UBound(審判)

        If 審判員数 <> 0 Then
            種目得点 = 種目得点 / 審判員数

            '小数点4桁目を四捨五入
            種目得点 = Math.Round(種目得点, 3, MidpointRounding.AwayFromZero)
        End If






    End Sub


    Public Sub 採点_チェック法()
        '===========================
        '概要　チェック法の結果     '   　 
        '入力　審判Class(各審判員の採点、ジャッジ人数
        '出力　種目得点  
        '===========================


        '選手得点の計算
        種目得点 = 0

        For j = 1 To UBound(審判)
            種目得点 = 種目得点 + 審判(j).素点
        Next j


    End Sub


    Public Sub 採点_順位法()
        '===========================
        '概要　スケーティングの結果     '   　 
        '入力　ジャッジ人数
        '出力　
        '===========================

        スケーティング結果_選手 = New スケーティング結果_選手(UBound(審判))


    End Sub



End Class

Public Class 審判員結果

    Public ジャッジ記号 As String
    Public ジャッジ表記名 As String
    Public ジャッジRole As String

    Public ジャッジPCS採点対象(10) As Integer  '1　PCS採点対象

    Public SendFlag As Integer

    Public PCS素点(10) As Double
    Public 減点素点(20) As Double

    Public PCS乖離度(10) As Double
    Public PCS無効FLAG(10) As Boolean

    Public 表示用PCS合計点 As Double  '１０個のPCSを単純に合計した点数
    Public 表示用減点合計点 As Double '１９個の減点を合計した点数

    Public 素点 As Decimal 'Integer  'チェック法・順位法用、ブレイキンプレセレクション用
    Public 備考 As String  'チェック法・順位法用

    Public 順位点 As Decimal   'ブレイキンプレセレクション用



    Public Sub New(ジャッジ記号_ As String, ジャッジ表記名_ As String, ジャッジRole_ As String)
        ジャッジ記号 = ジャッジ記号_
        ジャッジ表記名 = ジャッジ表記名_
        ジャッジRole = ジャッジRole_
    End Sub

End Class


'AJSでの、総合結果再スケーティング用クラス
Public Class 同点選手PCS

    Public 背番号 As String
    Public PCS得点() As Double

    Public 確定順位 As Integer
    Public 確定順位表記 As Integer

    Public Sub New(ByVal PCS数 As Integer)

        ReDim PCS得点(PCS数)
        確定順位 = 0
        確定順位表記 = 0

    End Sub

End Class
