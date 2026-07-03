Imports System
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class 採点結果_V2

    Public マスタデータ As マスタデータ



    Public Property 区分番号 As String
    Public Property ラウンド番号 As String

    Public Property 採点方式 As String

    '===総合結果用
    Public Property 出場選手数 As Integer
    Public 背番号() As String

    Public 総合得点() As Decimal
    Public 総合順位番号() As Integer '同点無しの連番
    Public 総合順位表記() As Integer '同点有り

    'ブレイキンのみ使用
    Public Property WIN数() As Integer
    Public Property 勝ちFLAG() As Integer  '勝った方が１
    Public Property 総合順位点() As Decimal

    '順位法用
    Public スケーティング結果_総合選手() As スケーティング結果_総合選手


    Public Property 種目数 As Integer   'ブレイキンではラウンド数のこと
    Public Property 種目記号()



    '種目結果 の定義
    Public 種目() As 種目結果_C


    '************************************************
    '　　 総合結果　メソッド
    '************************************************

    Public Sub New（）
        '作るだけで何もしない

    End Sub


    Public Sub New(区分番号_ As String, ラウンド番号_ As String)

        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_

        マスタデータ = New マスタデータ
        マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
        マスタデータ.F_審判担当PCSマスタ.Read(区分番号, ラウンド番号)

        出場選手数 = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(1, 0, 背番号)

        採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

        If Strings.Left(採点方式, 3) = "BJS" Or Strings.Left(採点方式, 3) = "AJS" Or Strings.Left(採点方式, 4) = "BJPR" Or Strings.Left(採点方式, 3) = "PDJ" Or Strings.Left(採点方式, 3) = "VAL" Then

            マスタデータ.J_新審判設定.Set_新審判基準VER(採点方式)
        End If



        種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号)

        ReDim 種目(種目数)
        If 種目数 >= 1 Then
            For d = 1 To 種目数

                種目(d) = New 種目結果_C(d, Me)

            Next d
        End If


        '総合結果の準備
        ReDim 総合得点(出場選手数)
        ReDim 総合順位番号(出場選手数)
        ReDim 総合順位表記(出場選手数)

        総合結果更新()



    End Sub

    Public Sub 総合結果更新()

        If Strings.Left(採点方式, 3) = "PDJ" Or Strings.Left(採点方式, 3) = "VAL" Then

            '総合得点の計算   種目毎に選手が違うケースがある。。。
            For s = 1 To 出場選手数
                Dim 選手総合得点 As Double = 0
                For d = 1 To 種目数
                    For ds = 1 To 種目(d).選手数
                        If 種目(d).選手結果(ds).背番号 = 背番号(s) Then
                            選手総合得点 = 選手総合得点 + 種目(d).選手結果(ds).種目得点

                            ds = 種目(d).選手数
                        End If
                    Next ds
                Next d
                総合得点(s) = 選手総合得点
            Next s

            '順位の計算
            総合順位確定()

        End If


        JSON書き出し()

    End Sub

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


        If Strings.Left(採点方式, 3) = "VAL" Then
            VAL同点処理()

        ElseIf マスタデータ.J_新審判設定.同点処理 <> "0" And 採点方式 <> "チェック法" Then
            AJS同点処理()
        End If


    End Sub

    Private Sub VAL同点処理()

        'だいたい上手く行っているが、６，６，８　とならなければならないところが、６，６，７となってしまう。。。

        Dim 最大出場選手数 As Integer = 200
        Dim 同点選手番号リスト() As Integer
        Dim 同点選手() As 同点選手PCS
        Dim PCS数 As Integer = マスタデータ.J_新審判設定.GetPCS数 * 種目数 ' 比較するPCS得点の数

        ' 現在の順位を追跡
        Dim 現在順位 As Integer = 1

        For 順位 = 1 To 出場選手数
            ReDim 同点選手番号リスト(最大出場選手数)
            Dim 同点者数 As Integer = 0

            現在順位 = 順位

            ' 同順位の選手をリストアップ
            For s = 1 To 出場選手数
                If 総合順位表記(s) = 順位 Then
                    同点者数 += 1
                    同点選手番号リスト(同点者数) = s
                End If
            Next

            ' 同点者が2人以上の場合
            If 同点者数 >= 2 Then
                ReDim 同点選手(同点者数)

                ' 同点選手クラスを作成
                For i = 1 To 同点者数
                    同点選手(i) = New 同点選手PCS(PCS数)
                    同点選手(i).背番号 = 背番号(同点選手番号リスト(i))

                    ' PCS得点を設定
                    For p = 1 To PCS数
                        同点選手(i).PCS得点(p) = 種目(1).選手結果(同点選手番号リスト(i)).PCS得点(p).PCS得点
                    Next
                Next

                ' PCS比較を行い順位を確定
                For i = 1 To 同点者数 - 1
                    For j = i + 1 To 同点者数
                        Dim 比較結果 As Integer = 比較PCS得点(同点選手(i), 同点選手(j), PCS数)
                        If 比較結果 = -1 Then
                            ' 順位を入れ替え
                            Dim temp As 同点選手PCS = 同点選手(i)
                            同点選手(i) = 同点選手(j)
                            同点選手(j) = temp
                        End If
                    Next
                Next

                ' 完全同点グループごとに順位を設定
                Dim グループ開始順位 As Integer = 現在順位
                Dim 完全同点 As Boolean = True

                Dim 完全同点人数 As Integer = 1

                For i = 1 To 同点者数
                    If i > 1 Then
                        ' 前の選手と比較して完全同点か確認
                        If 比較PCS得点(同点選手(i - 1), 同点選手(i), PCS数) <> 0 Then
                            完全同点 = False
                        Else
                            完全同点人数 = 完全同点人数 + 1
                        End If
                    End If

                    ' 完全同点が崩れた場合、次の順位に進む
                    If Not 完全同点 Then
                        'グループ開始順位 += 1
                        グループ開始順位 += 完全同点人数
                        完全同点 = True
                    End If

                    ' 確定順位を設定
                    同点選手(i).確定順位 = 現在順位
                    同点選手(i).確定順位表記 = グループ開始順位
                    現在順位 += 1
                Next

                ' 現在順位を更新
                現在順位 = グループ開始順位 + 1

                ' 総合順位表記と総合順位番号に反映
                For i = 1 To 同点者数
                    For s = 1 To 出場選手数
                        If 背番号(s) = 同点選手(i).背番号 Then
                            総合順位表記(s) = 同点選手(i).確定順位表記
                            総合順位番号(s) = 同点選手(i).確定順位
                        End If
                    Next
                Next
            ElseIf 同点者数 = 1 Then
                ' 同点者がいない場合、順位をそのまま設定
                For s = 1 To 出場選手数
                    If 総合順位表記(s) = 順位 Then
                        '総合順位表記(s) = 現在順位
                        '総合順位番号(s) = 現在順位
                    End If
                Next
                現在順位 += 1
            End If
        Next
    End Sub

    ' 同点選手のPCS得点を比較する関数
    Private Function 比較PCS得点(選手A As 同点選手PCS, 選手B As 同点選手PCS, PCS数 As Integer) As Integer
        For p = 1 To PCS数
            If 選手A.PCS得点(p) > 選手B.PCS得点(p) Then
                Return 1 ' 選手Aが上位
            ElseIf 選手A.PCS得点(p) < 選手B.PCS得点(p) Then
                Return -1 ' 選手Bが上位
            End If
        Next
        Return 0 ' 完全に同点
    End Function





    Private Sub VAL同点処理＿OLD()
        '総合順位表記に同点があった時に、同点処理を行う

        Dim 最大出場選手数 As Integer = 200
        Dim 同点選手番号リスト() As Integer

        Dim 同点選手() As 同点選手PCS

        Dim PCS数 As Integer
        Dim 種目記号リスト() = Nothing

        PCS数 = マスタデータ.J_新審判設定.GetPCS数 * 種目数

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
                    同点選手(d).背番号 = 種目(1).選手結果(同点選手番号リスト(d)).背番号

                    '同点選手クラスを 同点選手人数分作成

                    For p = 1 To マスタデータ.J_新審判設定.GetPCS数
                        同点選手(d).PCS得点(p) = 種目(1).選手結果(同点選手番号リスト(d)).PCS得点(p).PCS得点
                    Next p


                    'PCS採点のソート
                    Array.Sort(同点選手(d).PCS得点)
                    '降順に並べ替え
                    Array.Reverse(同点選手(d).PCS得点)
                    'ソート後は配列番号 0　から始まるので注意

                Next d

                '同点選手(ds).確定順位に順位を入れる

                '同点選手(ds).PCS得点(1) が上の人を上位の順位にする
                'さらに同点だったら、同点選手(ds).PCS得点(2)が上の人を上位の順位にする
                'さらに同点だったら、同点選手(ds).PCS得点(3)が上の人を上位の順位にする
                'そこも同点だったら、同順位とする

                'PCS1の最高点を探す
                Dim PCS1最高点 As Decimal = 0

                For ds = 1 To UBound(同点選手)
                    If PCS1最高点 < 同点選手(ds).PCS得点(0) Then
                        PCS1最高点 = 同点選手(ds).PCS得点(0)
                    End If
                Next ds

                Dim 同点者数_1 As Integer = 0
                For ds = 1 To UBound(同点選手)
                    If 同点選手(ds).PCS得点(0) = PCS1最高点 Then
                        '最高点の人は、順位確定
                        同点選手(ds).確定順位 = 順位
                        同点者数_1 = 同点者数_1 + 1
                    End If
                Next ds

                If 同点者数_1 >= 2 Then

                    'さらに同点があった時
                    'PCS2の最高点を探す
                    Dim PCS2最高点 As Decimal = 0
                    For ds = 1 To UBound(同点選手)
                        If 同点選手(ds).確定順位 = 0 Then
                            If PCS2最高点 < 同点選手(ds).PCS得点(1) Then
                                PCS2最高点 = 同点選手(ds).PCS得点(1)
                            End If
                        End If
                    Next ds
                    Dim 同点者数_2 As Integer = 0
                    For ds = 1 To UBound(同点選手)
                        If 同点選手(ds).確定順位 = 0 Then
                            If 同点選手(ds).PCS得点(1) = PCS2最高点 Then
                                '最高点の人は、順位確定
                                同点選手(ds).確定順位 = 順位
                                同点者数_2 = 同点者数_2 + 1
                            End If
                        End If
                    Next ds
                    If 同点者数_2 >= 2 Then
                        'さらに同点があった時
                        'PCS3の最高点を探す
                        Dim PCS3最高点 As Decimal = 0
                        For ds = 1 To UBound(同点選手)
                            If 同点選手(ds).確定順位 = 0 Then
                                If PCS3最高点 < 同点選手(ds).PCS得点(2) Then
                                    PCS3最高点 = 同点選手(ds).PCS得点(2)
                                End If
                            End If
                        Next ds
                        Dim 同点者数_3 As Integer = 0
                        For ds = 1 To UBound(同点選手)
                            If 同点選手(ds).確定順位 = 0 Then
                                If 同点選手(ds).PCS得点(2) = PCS3最高点 Then
                                    '最高点の人は、順位確定
                                    同点選手(ds).確定順位 = 順位
                                    同点者数_3 = 同点者数_3 + 1
                                End If
                            End If
                        Next ds
                        'さらに同点があった時は、同順位とする

                    End If
                End If




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
                    同点選手(d).背番号 = 種目(1).選手結果(同点選手番号リスト(d)).背番号

                    '同点選手クラスを 同点選手人数分作成
                    Dim pcs_c As Integer = 0
                    For syumoku = 1 To 種目数
                        For p = 1 To マスタデータ.J_新審判設定.GetPCS数
                            同点選手(d).PCS得点(pcs_c) = 種目(syumoku).選手結果(同点選手番号リスト(d)).PCS得点(p).PCS得点
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



    Public Sub JSON書き出し()


        Dim filename As String = "S_採点結果_" & 区分番号 & "_" & ラウンド番号 & ".json"
        Dim filepath As String = マスタデータ.Z_システム設定.Comp_filepath


        ''JSONにシリアライズする
        Dim jText = JsonConvert.SerializeObject(Me, Formatting.Indented)

        'マスターデータを削除する（サイズが大きくなるので）
        Dim jobj = JObject.Parse(jText)
        jobj.Remove("マスタデータ")

        jText = jobj.ToString


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

    Public Function JSON読み込み(filepath As String, 区分番号_ As String, ラウンド番号_ As String) As 採点結果_V2

        Dim rc As 採点結果_V2 = Nothing

        'Dim filepath As String = マスタデータ.Z_システム設定.Comp_filepath
        Dim filename As String = "S_採点結果_" & 区分番号_ & "_" & ラウンド番号_ & ".json"


        ''JSON読み込み用
        Dim jText As String = String.Empty


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した


            ''ファイルからJSONを読み込む
            Dim fs As New System.IO.FileStream(filepath & "\" & filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite)

            'FileStreamを基にしたStringReaderのインスタンスを作成
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("shift_jis")

            Dim sr As New System.IO.StreamReader(fs, enc)

            'ファイルの内容をすべて読み込む
            jText = sr.ReadToEnd()



            rc = JsonConvert.DeserializeObject(Of 採点結果_V2)(jText)

            '閉じる
            'srを閉じれば、基になるfsも閉じられる
            sr.Close()


            'rc.filepath = filepath
        End If

        Return rc

    End Function

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


    '****************************************************************
    '
    '    種目結果
    '
    '****************************************************************

    Public Class 種目結果_C

        Private Oya As 採点結果_V2

        Public Property 種目順 As String
        Public Property 種目記号 As String



        Public Property 審判員数 As Integer   'レフリーを含む

        Public 審判員() As 審判員_C

        Public Property レフリー番号 As Integer   'レフリーがいない場合は0 




        '種目結果
        Public Property 選手数 As Integer

        Public 選手結果() As 選手結果_C


        '************************************************
        '　　 種目結果　メソッド
        '************************************************
        Public Sub New（）

        End Sub


        Public Sub New(種目順_ As Integer, Oya_ As 採点結果_V2)

            Oya = Oya_


            種目順 = 種目順_


            レフリー番号 = 0

            Dim 背番号リスト(1) As String

            選手数 = Oya.マスタデータ.E_ヒート表マスタ.Get_背番号リスト(種目順, 0, 背番号リスト)

            If 選手数 > 0 Then


                Dim 種目C As D_種目 = Oya.マスタデータ.D_種目マスタ.Get_種目Class(Oya.区分番号, Oya.ラウンド番号, 種目順)

                If 種目C Is Nothing Then

                Else
                    種目記号 = 種目C.種目記号
                End If



                '審判設定

                Dim 担当審判グループ As Integer = 種目C.担当審判グループ
                審判員数 = 0

                For j = 1 To Oya.マスタデータ.審判員マスタ.Get_登録済み審判員数
                    If Oya.マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "0" And
                   Oya.マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(担当審判グループ) <> "" Then
                        審判員数 = 審判員数 + 1
                    End If
                Next j

                If 審判員数 > 0 Then

                    審判員_初期化(審判員数)

                    Dim j As Integer = 0
                    For jj = 1 To Oya.マスタデータ.審判員マスタ.Get_登録済み審判員数
                        If Oya.マスタデータ.審判員マスタ.審判員リスト(jj).審判チーム(担当審判グループ) <> "0" And
                           Oya.マスタデータ.審判員マスタ.審判員リスト(jj).審判チーム(担当審判グループ) <> "" Then

                            j = j + 1
                            審判員(j).ジャッジ記号 = Oya.マスタデータ.審判員マスタ.審判員リスト(jj).ジャッジ記号
                            審判員(j).ジャッジ表記名 = Oya.マスタデータ.審判員マスタ.審判員リスト(jj).ジャッジ表記名

                            'レフリー番号を判定
                            If Oya.マスタデータ.審判員マスタ.審判員リスト(jj).審判チーム(担当審判グループ) = "R" Then

                                レフリー番号 = j
                                審判員(j).ジャッジタイプ = Oya.マスタデータ.審判員マスタ.審判員リスト(jj).審判チーム(担当審判グループ)

                            ElseIf Oya.マスタデータ.審判員マスタ.審判員リスト(jj).審判チーム(担当審判グループ) = "T" Then

                                審判員(j).ジャッジタイプ = Oya.マスタデータ.審判員マスタ.審判員リスト(jj).審判チーム(担当審判グループ)

                            ElseIf Oya.マスタデータ.審判員マスタ.審判員リスト(jj).審判チーム(担当審判グループ) = "S" Then

                                審判員(j).ジャッジタイプ = Oya.マスタデータ.審判員マスタ.審判員リスト(jj).審判チーム(担当審判グループ)

                            Else
                                審判員(j).ジャッジタイプ = "J"

                                ' 審判員(j).ジャッジタイプ = Oya.マスタデータ.審判員マスタ.審判員リスト(jj).審判チーム(担当審判グループ)

                            End If






                        End If
                    Next jj

                End If



                '選手結果のインスタンス作成
                Dim s As Integer

                ReDim 選手結果(選手数)

                For s = 1 To 選手数
                    選手結果(s) = New 選手結果_C(背番号リスト(s), Me)

                Next s


                'ファイル読込み
                更新（）

            End If





        End Sub


        '採点ファイルの再読込み　種目毎
        Public Sub 更新()




            'ファイルの読込み
            For j = 1 To 審判員数
                If Strings.Left(Oya.採点方式, 3) = "PDJ" Then
                    FileRead_JSON_V2(Oya.区分番号, Oya.ラウンド番号, 種目記号, 審判員(j).ジャッジ記号)
                ElseIf Oya.採点方式 = "チェック法" Then
                    FileRead_JSON_V2(Oya.区分番号, Oya.ラウンド番号, 種目記号, 審判員(j).ジャッジ記号)
                ElseIf Oya.採点方式 = "順位法" Then
                    FileRead_JSON_V2(Oya.区分番号, Oya.ラウンド番号, 種目記号, 審判員(j).ジャッジ記号)
                ElseIf Strings.Left(Oya.採点方式, 4) = "BJS3" Then
                    '     FileRead_BR2_JSON_V2(Oya.区分番号, Oya.ラウンド番号, 種目記号, 審判員(j).ジャッジ記号)
                ElseIf Strings.Left(Oya.採点方式, 4) = "BJPR" Then
                    '    FileRead_JSON_V2(Oya.区分番号, Oya.ラウンド番号, 種目記号, 審判員(j).ジャッジ記号)
                ElseIf Strings.Left(Oya.採点方式, 3) = "VAL" Then
                    FileRead_JSON_V2(Oya.区分番号, Oya.ラウンド番号, 種目記号, 審判員(j).ジャッジ記号)
                Else

                End If

            Next j



            '採点集計
            If Strings.Left(Oya.採点方式, 5) = "PDJ10" Then
                For s = 1 To 選手数
                    選手結果(s).採点_PDJ10J()
                Next s

            ElseIf Strings.Left(Oya.採点方式, 5) = "PDJ20" Then
                For s = 1 To 選手数
                    選手結果(s).採点_PDJ20J()
                Next s

            ElseIf Strings.Left(Oya.採点方式, 5) = "PDJ30" Then
                For s = 1 To 選手数
                    選手結果(s).採点_PDJ20J()
                Next s

            ElseIf Strings.Left(Oya.採点方式, 3) = "VAL" Then
                'Other得点の初期化
                Dim Other項目数 As Integer = 0

                If Strings.Left(Oya.採点方式, 3) = "VAL" And Oya.ラウンド番号 = "400" Then
                    Other項目数 = 2
                ElseIf Strings.Left(Oya.採点方式, 3) = "VAL" And Oya.ラウンド番号 = "200" Then
                    Other項目数 = 1
                End If


                For s = 1 To 選手数
                    If Other項目数 > 0 Then
                        選手結果(s).初期化Other得点(Other項目数)
                    End If
                    選手結果(s).採点_VAL()
                Next s

                '選手の集計が終わったところでPCSの換算を実施する

                'PCSの最高点を探す
                Dim PCS最高点 As Double = 0
                For s = 1 To 選手数
                    If 選手結果(s).Other得点(1).実得点 > PCS最高点 Then
                        PCS最高点 = 選手結果(s).Other得点(1).実得点
                    End If
                Next s

                'PCS換算値を設定する
                If Oya.ラウンド番号 = "200" Then
                    For s = 1 To 選手数
                        If PCS最高点 = 0 Then
                            選手結果(s).Other得点(1).結果得点 = 0
                        Else
                            選手結果(s).Other得点(1).結果得点 = (選手結果(s).Other得点(1).実得点 / PCS最高点) * 30
                        End If

                        選手結果(s).種目得点 = 選手結果(s).種目得点 + 選手結果(s).Other得点(1).結果得点 'PCS換算

                        If 選手結果(s).種目得点 < 0 Then
                            選手結果(s).種目得点 = 0
                        End If
                        If 選手結果(s).失格FLAG = True Then
                            選手結果(s).種目得点 = 0
                        End If
                    Next s

                ElseIf Oya.ラウンド番号 = "400" Then
                    For s = 1 To 選手数
                        If PCS最高点 = 0 Then
                            選手結果(s).Other得点(1).結果得点 = 0
                        Else
                            選手結果(s).Other得点(1).結果得点 = (選手結果(s).Other得点(1).実得点 / PCS最高点) * 60

                        End If

                        選手結果(s).種目得点 = 選手結果(s).種目得点 + 選手結果(s).Other得点(1).結果得点　'PCS換算
                        選手結果(s).種目得点 = 選手結果(s).種目得点 + 選手結果(s).Other得点(2).結果得点  'オーディエンス換算

                        If 選手結果(s).種目得点 < 0 Then
                            選手結果(s).種目得点 = 0
                        End If
                        If 選手結果(s).失格FLAG = True Then
                            選手結果(s).種目得点 = 0
                        End If
                    Next s
                End If




            End If



            'SendFLAGの更新

            For s = 1 To 選手数

                '審判FLAGの検索
                Dim SENDFLAG As Boolean = True
                For j = 1 To 審判員数     '審判員数は、その種目の担当審判 0 ブランク　では無い人数（レフリー含む）

                    'ソロ競技で、課題数が1以上ある時は、T（技術判定員）も対象にする。
                    If Me.審判員(j).ジャッジタイプ = "T" Then

                        Dim 種目 As D_種目 = Oya.マスタデータ.D_種目マスタ.Get_種目Class(Oya.区分番号, Oya.ラウンド番号, 種目順)
                        If 種目.SG種別 = "S" And Oya.マスタデータ.J_新審判設定.Get課題数(種目.種目記号) >= 1 Then
                            If 選手結果(s).審判員結果(j).SEND_FLAG <> 1 Then
                                SENDFLAG = False
                                j = 審判員数
                            End If

                        End If

                    Else
                        If 選手結果(s).審判員結果(j).SEND_FLAG <> 1 Then
                            SENDFLAG = False
                            j = 審判員数
                        End If

                    End If



                Next j

                If SENDFLAG = True And 審判員数 > 0 Then
                    選手結果(s).SEND_FLAG = 1
                Else
                    選手結果(s).SEND_FLAG = 0
                End If
            Next s



            '種目順位計算
            種目順位確定()

        End Sub


        Public Sub リアル更新(ジャッジ記号 As String, ジャッジ結果 As S_採点結果_V2_J)

            '１人分のジャッジ結果をもらって、再集計する。


            'Dim filepath = Oya.マスタデータ.Z_システム設定.Comp_filepath
            'Dim ジャッジ結果 = New S_採点結果_V2_J(filepath)


            'もし、


            Dim 選手数 As Integer = ジャッジ結果.選手数

            If 選手数 > 0 Then


                '選手クラスの中で該当背番号の選手クラスを探す
                For s = 1 To 選手数
                    For i = 1 To UBound(選手結果)

                        If ジャッジ結果.S_採点結果_選手_J(s).背番号 = 選手結果(i).背番号 Then

                            '該当のジャッジクラスを探す
                            For j = 1 To 審判員数
                                If ジャッジ結果.ジャッジ記号 = 選手結果(i).審判員結果(j).ジャッジ記号 Then


                                    If Strings.Left(ジャッジ結果.採点方式, 4) = "BJPR" Then

                                        'BJPRの時はヒート毎SENDなので、選手毎にSENDFLAGを見る
                                        選手結果(i).審判員結果(j).SEND_FLAG = ジャッジ結果.S_採点結果_選手_J(s).SEND_FLAG


                                    ElseIf Strings.Left(ジャッジ結果.採点方式, 3) = "PDJ" Then

                                        'PDJの時はヒート毎SENDなので、選手毎にSENDFLAGを見る
                                        選手結果(i).審判員結果(j).SEND_FLAG = ジャッジ結果.S_採点結果_選手_J(s).SEND_FLAG


                                        'ジャッジ結果をセット

                                        If ジャッジ結果.ジャッジタイプ = "J" Or ジャッジ結果.ジャッジタイプ = "S" Then

                                            '一般ジャッジの場合

                                            'PCS
                                            If ジャッジ結果.PCS数 > 0 Then

                                                For p = 1 To ジャッジ結果.PCS数

                                                    Dim Heat As Integer = ジャッジ結果.S_採点結果_選手_J(s).ヒート番号

                                                    '担当PCS
                                                    選手結果(i).審判員結果(j).PCS素点(p).PCS採点対象FLAG = ジャッジ結果.担当PCS_J(Heat).担当PCS(p).担当

                                                    'PSC採点
                                                    選手結果(i).審判員結果(j).PCS素点(p).PCS素点 = ジャッジ結果.S_採点結果_選手_J(s).PCS得点_J(p).PCS素点
                                                Next p
                                            End If

                                            'GOE
                                            If ジャッジ結果.課題数 > 0 Then

                                                For k = 1 To ジャッジ結果.課題数

                                                    選手結果(i).審判員結果(j).GOE素点(k).GOE素点 = ジャッジ結果.S_採点結果_選手_J(s).GOE_J(k).GOE

                                                Next k

                                            End If



                                        ElseIf ジャッジ結果.ジャッジタイプ = "R" Then

                                            'レフリーの場合

                                            '一般減点
                                            If ジャッジ結果.減点項目数 > 0 Then
                                                For r = 1 To ジャッジ結果.減点項目数
                                                    選手結果(i).審判員結果(j).一般減点素点(r).一般減点素点 = ジャッジ結果.S_採点結果_選手_J(s).減点_J(r).減点
                                                Next r
                                            End If



                                        ElseIf ジャッジ結果.ジャッジタイプ = "T" Then

                                            '技術判定員の場合

                                            'TES
                                            If ジャッジ結果.課題数 > 0 And ジャッジ結果.TES減点数 > 0 Then

                                                For k = 1 To ジャッジ結果.課題数

                                                    'TES　Base
                                                    選手結果(i).審判員結果(j).TES素点(k).TES_Base = ジャッジ結果.S_採点結果_選手_J(s).TES_J(k).Base

                                                    'TES 減点
                                                    For r = 1 To ジャッジ結果.TES減点数

                                                        選手結果(i).審判員結果(j).TES素点(k).TES減点(r).TES減点 = ジャッジ結果.S_採点結果_選手_J(s).TES_J(k).TES減点_J(r).TES減点

                                                    Next r

                                                Next k

                                            End If

                                        End If



                                    Else
                                        'チェック法　順位法の時

                                        '種目毎SEND

                                        選手結果(i).審判員結果(j).SEND_FLAG = ジャッジ結果.SEND_FLAG


                                        '得点の登録
                                        選手結果(i).審判員結果(j).素点 = ジャッジ結果.S_採点結果_選手_J(s).点数

                                        '備考の登録
                                        選手結果(i).審判員結果(j).備考 = ジャッジ結果.S_採点結果_選手_J(s).備考


                                    End If



                                    j = 審判員数
                                End If

                            Next j

                            i = 選手数
                        End If

                    Next i

                Next s


            End If




            '採点集計
            If Strings.Left(Oya.採点方式, 5) = "PDJ10" Then
                For s = 1 To 選手数
                    選手結果(s).採点_PDJ10J()
                Next s

            ElseIf Strings.Left(Oya.採点方式, 5) = "PDJ20" Then
                For s = 1 To 選手数
                    選手結果(s).採点_PDJ20J()
                Next s

            ElseIf Strings.Left(Oya.採点方式, 5) = "PDJ30" Then
                For s = 1 To 選手数
                    選手結果(s).採点_PDJ20J()
                Next s

            ElseIf Strings.Left(Oya.採点方式, 3) = "VAL" Then
                For s = 1 To 選手数
                    選手結果(s).採点_VAL()
                Next s


            Else



            End If


            'Send Flagの更新はしない



            '種目順位計算
            '種目順位確定()


        End Sub



        Public Sub 審判員_初期化(審判員数)

            If 審判員数 > 0 Then

                ReDim 審判員(審判員数)

                For j = 1 To 審判員数
                    審判員(j) = New 審判員_C
                Next j

            End If

        End Sub

        Private Sub FileRead_JSON_V2(区分番号 As String, ラウンド番号 As String, 種目記号 As String, ジャッジ記号 As String)

            '===========================
            '概要　PDJ用 JSONファイル V2の読み込み  '   　 
            '入力　
            '出力　選手(i).審判(j).素点・備考 へのデータセット
            '      
            '===========================

            Dim filepath = Oya.マスタデータ.Z_システム設定.Comp_filepath

            区分番号 = String.Format("{0:D2}", CInt(区分番号))

            Dim ジャッジ結果 = New S_採点結果_V2_J(filepath)

            ジャッジ結果.区分番号 = 区分番号
            ジャッジ結果.ラウンド番号 = ラウンド番号
            ジャッジ結果.現種目記号 = 種目記号
            ジャッジ結果.ジャッジ記号 = ジャッジ記号


            ジャッジ結果 = ジャッジ結果.JSON読み込み()


            If ジャッジ結果 Is Nothing Then
                ジャッジ結果 = New S_採点結果_V2_J(filepath)

                ジャッジ結果.区分番号 = 区分番号
                ジャッジ結果.ラウンド番号 = ラウンド番号
                ジャッジ結果.現種目記号 = 種目記号
                ジャッジ結果.ジャッジ記号 = ジャッジ記号

            Else


            End If





            ジャッジ結果.Set_filepath(filepath)

            Dim 選手数 As Integer = ジャッジ結果.選手数

            If 選手数 > 0 Then


                '選手クラスの中で該当背番号の選手クラスを探す
                For s = 1 To 選手数
                    For i = 1 To UBound(選手結果)

                        If ジャッジ結果.S_採点結果_選手_J(s).背番号 = 選手結果(i).背番号 Then

                            '該当のジャッジクラスを探す
                            For j = 1 To 審判員数
                                If ジャッジ結果.ジャッジ記号 = 選手結果(i).審判員結果(j).ジャッジ記号 Then


                                    If Strings.Left(ジャッジ結果.採点方式, 4) = "BJPR" Then

                                        'BJPRの時はヒート毎SENDなので、選手毎にSENDFLAGを見る
                                        選手結果(i).審判員結果(j).SEND_FLAG = ジャッジ結果.S_採点結果_選手_J(s).SEND_FLAG


                                    ElseIf Strings.Left(ジャッジ結果.採点方式, 3) = "PDJ" Or Strings.Left(ジャッジ結果.採点方式, 3) = "VAL" Then

                                        'PDJの時はヒート毎SENDなので、選手毎にSENDFLAGを見る
                                        選手結果(i).審判員結果(j).SEND_FLAG = ジャッジ結果.S_採点結果_選手_J(s).SEND_FLAG


                                        'ジャッジ結果をセット

                                        If ジャッジ結果.ジャッジタイプ = "J" Or ジャッジ結果.ジャッジタイプ = "S" Then

                                            '一般ジャッジの場合

                                            'PCS
                                            If ジャッジ結果.PCS数 > 0 Then

                                                For p = 1 To ジャッジ結果.PCS数

                                                    Dim Heat As Integer = ジャッジ結果.S_採点結果_選手_J(s).ヒート番号

                                                    '担当PCS
                                                    選手結果(i).審判員結果(j).PCS素点(p).PCS採点対象FLAG = ジャッジ結果.担当PCS_J(Heat).担当PCS(p).担当

                                                    'PSC採点
                                                    選手結果(i).審判員結果(j).PCS素点(p).PCS素点 = ジャッジ結果.S_採点結果_選手_J(s).PCS得点_J(p).PCS素点
                                                Next p
                                            End If

                                            'GOE
                                            If ジャッジ結果.課題数 > 0 Then

                                                For k = 1 To ジャッジ結果.課題数

                                                    選手結果(i).審判員結果(j).GOE素点(k).GOE素点 = ジャッジ結果.S_採点結果_選手_J(s).GOE_J(k).GOE

                                                Next k

                                            End If



                                        ElseIf ジャッジ結果.ジャッジタイプ = "R" Then

                                            'レフリーの場合

                                            '一般減点
                                            If ジャッジ結果.減点項目数 > 0 Then
                                                For r = 1 To ジャッジ結果.減点項目数
                                                    選手結果(i).審判員結果(j).一般減点素点(r).一般減点素点 = ジャッジ結果.S_採点結果_選手_J(s).減点_J(r).減点
                                                Next r
                                            End If



                                        ElseIf ジャッジ結果.ジャッジタイプ = "T" Then

                                            '技術判定員の場合

                                            'TES
                                            If ジャッジ結果.課題数 > 0 And ジャッジ結果.TES減点数 > 0 Then

                                                For k = 1 To ジャッジ結果.課題数

                                                    'TES　Base
                                                    選手結果(i).審判員結果(j).TES素点(k).TES_Base = ジャッジ結果.S_採点結果_選手_J(s).TES_J(k).Base

                                                    'TES 減点
                                                    For r = 1 To ジャッジ結果.TES減点数

                                                        選手結果(i).審判員結果(j).TES素点(k).TES減点(r).TES減点 = ジャッジ結果.S_採点結果_選手_J(s).TES_J(k).TES減点_J(r).TES減点

                                                    Next r

                                                Next k

                                            End If

                                        End If




                                    Else
                                        'チェック法　順位法の時

                                        '種目毎SEND

                                        選手結果(i).審判員結果(j).SEND_FLAG = ジャッジ結果.SEND_FLAG


                                        '得点の登録
                                        選手結果(i).審判員結果(j).素点 = ジャッジ結果.S_採点結果_選手_J(s).点数

                                        '備考の登録
                                        選手結果(i).審判員結果(j).備考 = ジャッジ結果.S_採点結果_選手_J(s).備考


                                    End If



                                    j = 審判員数
                                End If

                            Next j

                            i = 選手数
                        End If

                    Next i

                Next s


            End If



        End Sub


        Public Function Get_一般審判員数() As Integer
            'レフリー、技術判定員 以外の人数

            Dim rc As Integer = 0
            For j = 1 To 審判員数
                If 審判員(j).ジャッジタイプ = "J" Or 審判員(j).ジャッジタイプ = "S" Then
                    rc = rc + 1
                End If
            Next j

            Return rc

        End Function

        Private Sub 種目順位確定()
            ' 選手結果(s).種目得点 を使って順位を確定し、以下の項目に値を入れる
            ' 選手結果(s).種目順位番号 As Integer '同点無しの連番
            ' 選手結果(s).種目順位表記 As Integer '同点有り

            Dim 選手番号リスト() As Integer
            Dim 得点リスト() As Double

            ReDim 選手番号リスト(選手数)
            ReDim 得点リスト(選手数)


            'Tempの得点リストの作成
            Dim Temp得点リスト() As Decimal
            ReDim Temp得点リスト(選手数)

            For s = 1 To 選手数
                Temp得点リスト(s) = 選手結果(s).種目得点
            Next s


            Dim 最大得点選手番号 As Integer = 0
            Dim 最大得点 As Decimal

            '並べ替え
            For 降順 = 1 To 選手数
                最大得点 = -1
                For s = 1 To 選手数
                    If 最大得点 < Temp得点リスト(s) Then
                        最大得点 = Temp得点リスト(s)
                        最大得点選手番号 = s
                    End If
                Next s
                選手番号リスト(降順) = 最大得点選手番号
                得点リスト(降順) = 最大得点
                Temp得点リスト(最大得点選手番号) = -1
            Next 降順


            Dim 前の選手の得点 As Decimal = -1
            Dim 前の選手の順位 As Integer = 0
            For 降順 = 1 To 選手数
                選手結果(選手番号リスト(降順)).種目順位番号 = 降順
                If 前の選手の得点 = 得点リスト(降順) Then
                    '同点の時
                    選手結果(選手番号リスト(降順)).種目順位表記 = 前の選手の順位
                Else
                    '同点じゃない時
                    選手結果(選手番号リスト(降順)).種目順位表記 = 降順
                End If
                前の選手の順位 = 選手結果(選手番号リスト(降順)).種目順位表記
                前の選手の得点 = 得点リスト(降順)
            Next 降順

        End Sub




        '****************************************************************
        '
        '    選手結果
        '
        '****************************************************************

        Public Class 選手結果_C

            Private Oya As 種目結果_C


            Public Property 背番号 As String

            '結果
            Public Property 種目得点 As Decimal
            Public Property 種目順位番号 As Integer
            Public Property 種目順位表記 As Decimal


            '詳細結果
            Public TES得点() As TES得点_C

            Public PCS得点() As PCS得点_C

            Public GOE得点() As GOE得点_C

            Public 一般減点() As 一般減点_C

            Public Other得点() As Other得点_C     'バルカーでのオーディエンスの得点など


            Public Property 失格FLAG As Boolean

            Public Property SEND_FLAG As Integer    '種目毎SEND


            Private ソロ種目記号 As String '課題フィガー用

            'ブレイキンプレのみ
            Public Property 種目順位点

            'スケーティング用
            Public スケーティング結果_選手 As スケーティング結果_選手


            Public 審判員結果() As 審判員結果_C


            Public 無効審判 As G_審判無効

            '************************************************
            '　　 選手結果　メソッド
            '************************************************
            Public Sub New()
                '作るだけ
            End Sub


            Public Sub New(背番号_ As String, Oya_ As 種目結果_C)

                Oya = Oya_

                背番号 = 背番号_

                'ソロ種目記号の検索
                For d = 1 To Oya.Oya.種目数
                    Dim 種目_TEMP As D_種目 = Oya.Oya.マスタデータ.D_種目マスタ.Get_種目Class(Oya.Oya.区分番号, Oya.Oya.ラウンド番号, d)

                    If 種目_TEMP.SG種別 = "S" Then
                        ソロ種目記号 = 種目_TEMP.種目記号
                    End If
                Next d



                '初期化

                'TESの設定
                If Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号) > 0 Then
                    初期化TES(Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号), Oya.Oya.マスタデータ.J_新審判設定.GetTES減点数)
                End If


                'PCSの設定
                If Oya.Oya.マスタデータ.J_新審判設定.GetPCS数 > 0 Then
                    初期化PCS(Oya.Oya.マスタデータ.J_新審判設定.GetPCS数)
                End If


                '一般減点の初期化
                If Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数 > 0 Then
                    初期化一般減点(Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数)
                End If

                'GOEの設定
                If Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号) > 0 Then
                    初期化GOE(Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号))

                End If

                'Other得点の初期化
                Dim Other項目数 As Integer = 0

                If Strings.Left(Oya.Oya.採点方式, 3) = "VAL" And Oya.Oya.ラウンド番号 = "400" Then
                    Other項目数 = 2
                ElseIf Strings.Left(Oya.Oya.採点方式, 3) = "VAL" And Oya.Oya.ラウンド番号 = "200" Then
                    Other項目数 = 1
                End If


                If Other項目数 > 0 Then
                    初期化Other得点(Other項目数)
                End If



                '審判員結果の初期化
                If Oya.審判員数 > 0 Then
                    ReDim 審判員結果(Oya.審判員数)
                End If

                For j = 1 To Oya.審判員数
                    審判員結果(j) = New 審判員結果_C(Oya.審判員(j).ジャッジ記号, Me)
                Next j


            End Sub


            Public Sub 初期化TES(課題数, TES減点項目数)

                If 課題数 > 0 Then

                    ReDim TES得点(課題数)
                    For p = 1 To 課題数
                        TES得点(p) = New TES得点_C(TES減点項目数)
                    Next p
                End If

            End Sub


            Public Sub 初期化PCS(PCS数)

                If PCS数 > 0 Then

                    ReDim PCS得点(PCS数)
                    For p = 1 To PCS数
                        PCS得点(p) = New PCS得点_C
                    Next p
                End If

            End Sub

            Public Sub 初期化GOE(課題数)

                If 課題数 > 0 Then

                    ReDim GOE得点(課題数)
                    For k = 1 To 課題数
                        GOE得点(k) = New GOE得点_C
                    Next k
                End If

            End Sub


            Public Sub 初期化一般減点(減点項目数)

                If 減点項目数 > 0 Then
                    ReDim 一般減点(減点項目数)
                    For r = 1 To 減点項目数

                        一般減点(r) = New 一般減点_C
                    Next r

                End If
            End Sub

            Public Sub 初期化Other得点(項目数)

                If 項目数 > 0 Then
                    ReDim Other得点(項目数)
                    For o = 1 To 項目数

                        Other得点(o) = New Other得点_C
                    Next o

                End If
            End Sub


            Public Sub 採点_PDJ10J()
                '===========================
                '概要　PDJ1.0JでのPCS採点。  １選手の10PCS の結果     '   　 
                '入力　審判結果(各審判員の採点結果)
                '出力　TES, PCS得点　一般減点 失格FLAG　　　および乖離度
                '===========================

                Dim 除外範囲 As Decimal = 1.5

                Dim BV() As Decimal
                ReDim BV(Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号))


                'TESの採点
                If Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号) > 0 Then

                    For j = 1 To Oya.審判員数

                        '技術判定員
                        If Oya.審判員(j).ジャッジタイプ = "T" Then


                            For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)



                                'TES Base
                                TES得点(k).TES_Base = 審判員結果(j).TES素点(k).TES_Base
                                BV(k) = 審判員結果(j).TES素点(k).TES_Base

                                'TES減点
                                For r = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetTES減点数

                                    If 審判員結果(j).SEND_FLAG = 1 Then
                                        TES得点(k).TES減点(r).TES減点 = 審判員結果(j).TES素点(k).TES減点(r).TES減点
                                    End If
                                    TES得点(k).TES減点(r).TES減点項目名 = Oya.Oya.マスタデータ.J_新審判設定.TES減点設定(r).減点略称

                                    BV(k) = BV(k) + 審判員結果(j).TES素点(k).TES減点(r).TES減点
                                Next r

                            Next k

                            j = Oya.審判員数
                        End If
                    Next j

                    'ソロ競技ではない時は、基礎点として、TES_Baseを設定する。
                    If Oya.Oya.マスタデータ.D_種目マスタ.Get_種目Class(Oya.Oya.区分番号, Oya.Oya.ラウンド番号, Oya.種目順).SG種別 <> "S" Then

                        For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)
                            'TES Base
                            TES得点(k).TES_Base = Oya.Oya.マスタデータ.J_新審判設定.TES設定(k)
                            BV(k) = Oya.Oya.マスタデータ.J_新審判設定.TES設定(k)

                        Next k

                    End If





                    'GOE
                    'GOE は、課題フィガー毎に全審判員の付与した点数のうち最高点と最低点を削除した残りの平均値を
                    'その選手の得点とする。 但し、BV=0 と判定された場合は自動的に GOE=0 と設定され

                    Dim TEMP_GOE得点リスト() As Decimal
                    Dim 一般ジャッジ人数 As Integer = 0

                    For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)

                        ReDim TEMP_GOE得点リスト(Oya.Get_一般審判員数)

                        一般ジャッジ人数 = 0
                        For j = 1 To Oya.審判員数

                            If Oya.審判員(j).ジャッジタイプ = "J" Then

                                一般ジャッジ人数 = 一般ジャッジ人数 + 1
                                If 審判員結果(j).SEND_FLAG = 1 Then
                                    TEMP_GOE得点リスト(一般ジャッジ人数) = 審判員結果(j).GOE素点(k).GOE素点
                                End If

                            End If

                        Next j

                        Dim Sort後_TEMP_GOE得点リスト = 配列ソート(TEMP_GOE得点リスト, "昇順")

                        'Min, Maxを除いた平均値
                        Dim Min As Decimal = Sort後_TEMP_GOE得点リスト(1)
                        Dim Max As Decimal = Sort後_TEMP_GOE得点リスト(UBound(TEMP_GOE得点リスト))

                        Dim AVE As Decimal = 0
                        For j = 2 To UBound(TEMP_GOE得点リスト) - 1
                            AVE = AVE + Sort後_TEMP_GOE得点リスト(j)
                        Next j

                        GOE得点(k).GOE得点 = AVE / (UBound(TEMP_GOE得点リスト) - 2)


                        'BVが0点（又はマイナス）の時は、GOEは0点にする。
                        If BV(k) <= 0 Then
                            GOE得点(k).GOE得点 = 0
                        End If

                    Next k






                End If

                'PCSの採点
                If Oya.Oya.マスタデータ.J_新審判設定.GetPCS数 > 0 Then

                    Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Read(Oya.Oya.区分番号, Oya.Oya.ラウンド番号)

                    For pcs = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetPCS数

                        '該当PCSの担当ジャッジ数を確認 
                        ' 2023/4/10 ジャッジから戻って来たファイルでの確認ではなく、あらかじめ設定された人数で確認する
                        Dim 該当PCS担当ジャッジ数 As Integer = 0
                        For j = 1 To Oya.審判員数

                            If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号) IsNot Nothing Then

                                If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号).担当PCS番号(Oya.種目順).Contains(CStr(pcs)) = True Then
                                    該当PCS担当ジャッジ数 = 該当PCS担当ジャッジ数 + 1
                                End If

                            End If


                            ' If 審判員結果(j).PCS素点(pcs).PCS採点対象FLAG = True Then
                            '該当PCS担当ジャッジ数 = 該当PCS担当ジャッジ数 + 1
                            'End If
                        Next j



                        If 該当PCS担当ジャッジ数 > 0 Then


                            'PCSの採点配列を作成
                            Dim PCS採点配列() As Decimal
                            ReDim PCS採点配列(Oya.審判員数)

                            For j = 1 To Oya.審判員数
                                If 審判員結果(j).PCS素点(pcs).PCS採点対象FLAG = True Then

                                    If 審判員結果(j).SEND_FLAG = 1 Then
                                        PCS採点配列(j) = 審判員結果(j).PCS素点(pcs).PCS素点
                                    Else
                                        PCS採点配列(j) = 0
                                    End If

                                End If
                            Next j



                            'PCS採点を降順でソート
                            PCS採点配列 = 配列ソート(PCS採点配列, "降順")


                            '採点対象審判員数から中間値を算出


                            Dim 中間点 As Double = 0

                            If (該当PCS担当ジャッジ数 Mod 2) = 0 Then
                                '審判員が偶数
                                中間点 = 中間点 + PCS採点配列(該当PCS担当ジャッジ数 \ 2)
                                中間点 = 中間点 + PCS採点配列(該当PCS担当ジャッジ数 \ 2 + 1)
                                中間点 = 中間点 / 2
                            Else
                                '審判員が奇数
                                中間点 = PCS採点配列(該当PCS担当ジャッジ数 \ 2 + 1)
                            End If


                            '除外対象の確定とPCS得点の集計

                            Dim PCS合計点 As Decimal = 0
                            Dim 範囲内ジャッジ人数 As Integer = 0
                            Dim 乖離度 As Decimal = 0

                            For j = 1 To Oya.審判員数
                                'If 審判員結果(j).PCS素点(pcs).PCS採点対象FLAG = True Then

                                If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号) IsNot Nothing Then

                                    If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号).担当PCS番号(Oya.種目順).Contains(CStr(pcs)) = True Then


                                        If 審判員結果(j).PCS素点(pcs).PCS素点 > (除外範囲 + 中間点) Or
                                       審判員結果(j).PCS素点(pcs).PCS素点 < (中間点 - 除外範囲) Then

                                            '採点対象で、除外範囲を超えていたら
                                            審判員結果(j).PCS素点(pcs).PCS無効FLAG = True

                                        Else
                                            '採点対象で、除外範囲を超えていない場合

                                            If 審判員結果(j).SEND_FLAG = 1 Then
                                                PCS合計点 = PCS合計点 + 審判員結果(j).PCS素点(pcs).PCS素点
                                            End If

                                            範囲内ジャッジ人数 = 範囲内ジャッジ人数 + 1
                                            審判員結果(j).PCS素点(pcs).PCS無効FLAG = False

                                            'まだSENDしていない時

                                        End If

                                        '乖離度の算出
                                        乖離度 = 0

                                        If 中間点 >= 審判員結果(j).PCS素点(pcs).PCS素点 Then
                                            乖離度 = (1 / (1 + (中間点 - 審判員結果(j).PCS素点(pcs).PCS素点) * (中間点 - 審判員結果(j).PCS素点(pcs).PCS素点)))
                                        Else
                                            乖離度 = (1 / (1 + (審判員結果(j).PCS素点(pcs).PCS素点 - 中間点) * (審判員結果(j).PCS素点(pcs).PCS素点 - 中間点)))
                                        End If
                                        審判員結果(j).PCS素点(pcs).乖離度 = 乖離度

                                    End If
                                End If
                                ' End If

                            Next j


                            'PCS得点の算出
                            If 範囲内ジャッジ人数 > 0 Then
                                '通常のばあい

                                PCS得点(pcs).PCS得点 = PCS合計点 / 範囲内ジャッジ人数

                                '小数点４桁目を四捨五入     Ver1.02.19で追加
                                PCS得点(pcs).PCS得点 = Math.Round(PCS得点(pcs).PCS得点, 3, MidpointRounding.AwayFromZero)


                            Else
                                '全員が範囲外の場合

                                For j = 1 To Oya.審判員数
                                    If 審判員結果(j).PCS素点(pcs).PCS採点対象FLAG = True Then

                                        '採点対象での場合
                                        If 審判員結果(j).SEND_FLAG = 1 Then
                                            PCS合計点 = PCS合計点 + 審判員結果(j).PCS素点(pcs).PCS素点
                                            '範囲内ジャッジ人数 = 範囲内ジャッジ人数 + 1
                                        End If


                                    End If
                                Next j

                                PCS得点(pcs).PCS得点 = PCS合計点 / 該当PCS担当ジャッジ数

                                '小数点４桁目を四捨五入　　　Ver1.02.19で追加
                                PCS得点(pcs).PCS得点 = Math.Round(PCS得点(pcs).PCS得点, 3, MidpointRounding.AwayFromZero)


                            End If

                        End If
                    Next pcs

                End If




                '減点の計算

                'レフリーがいるか確認
                If Oya.レフリー番号 <> 0 Then

                    If Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数 > 0 Then
                        '通常減点
                        For d = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数
                            一般減点(d).一般減点 = 審判員結果(Oya.レフリー番号).一般減点素点(d).一般減点素点
                        Next d

                        '失格判定
                        If 審判員結果(Oya.レフリー番号).一般減点素点(1).一般減点素点 = 1 Then
                            失格FLAG = True
                        Else
                            失格FLAG = False

                        End If
                    End If

                End If




                '選手得点の計算
                種目得点 = 0

                'TESの計算
                Dim TES As Decimal = 0
                Dim GOE As Decimal = 0

                For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)

                    If BV(k) <= 0 Then
                        'BVが0点又はマイナスと判定されたとき
                        'TESは0点とする。

                    Else

                        'TES Base
                        TES = TES + TES得点(k).TES_Base

                        'TES減点
                        For r = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetTES減点数
                            TES = TES + TES得点(k).TES減点(r).TES減点
                        Next r

                        'GOEの点数
                        GOE = GOE + GOE得点(k).GOE得点

                    End If







                Next k


                種目得点 = 種目得点 + TES + GOE


                For pcs = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetPCS数

                    'PCS倍率の取得
                    Dim 倍率 As Double = Oya.Oya.マスタデータ.J_新審判設定.PCS設定(pcs).倍率

                    '種目得点の算出
                    種目得点 = 種目得点 + (PCS得点(pcs).PCS得点 * 倍率)

                Next pcs



                '===減点を足しこみ
                For d = 2 To Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数
                    種目得点 = 種目得点 + 一般減点(d).一般減点
                Next d


                '減点が多すぎてマイナスになった時は０にする
                If 種目得点 < 0 Then
                    種目得点 = 0
                End If


                '失格判定
                If 失格FLAG = True Then
                    種目得点 = 0
                End If


                '表示用の合計計算  ---->  必要であれば関数で対応しよう。

                'For j = 1 To Oya.審判員数

                ' 審判(j).表示用PCS合計点 = 0
                ' 審判(j).表示用減点合計点 = 0
                'For pcs = 1 To 10
                '審判(j).表示用PCS合計点 = 審判(j).表示用PCS合計点 + 審判(j).PCS素点(pcs)
                'Next pcs
                'For 減点番号 = 2 To 20
                '審判(j).表示用減点合計点 = 審判(j).表示用減点合計点 + 審判(j).減点素点(減点番号)
                'Next 減点番号
                'Next j





            End Sub


            Public Sub 採点_PDJ20J()
                '===========================
                '概要　PDJ2.0JでのPCS採点。  １選手の10PCS の結果     PCSは乖離度で計算 　 
                '入力　審判結果(各審判員の採点結果)
                '出力　TES, PCS得点　一般減点 失格FLAG　　　および乖離度    
                '===========================

                Dim 除外範囲 As Decimal = 1.5

                Dim BV() As Decimal
                ReDim BV(Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号))


                'TESの採点
                If Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号) > 0 Then

                    For j = 1 To Oya.審判員数

                        '技術判定員
                        If Oya.審判員(j).ジャッジタイプ = "T" Then


                            For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)



                                'TES Base
                                TES得点(k).TES_Base = 審判員結果(j).TES素点(k).TES_Base
                                BV(k) = 審判員結果(j).TES素点(k).TES_Base

                                'TES減点
                                For r = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetTES減点数

                                    If 審判員結果(j).SEND_FLAG = 1 Then
                                        TES得点(k).TES減点(r).TES減点 = 審判員結果(j).TES素点(k).TES減点(r).TES減点
                                    End If
                                    TES得点(k).TES減点(r).TES減点項目名 = Oya.Oya.マスタデータ.J_新審判設定.TES減点設定(r).減点略称

                                    BV(k) = BV(k) + 審判員結果(j).TES素点(k).TES減点(r).TES減点
                                Next r

                            Next k

                            j = Oya.審判員数
                        End If
                    Next j

                    'ソロ競技ではない時は、基礎点として、TES_Baseを設定する。
                    If Oya.Oya.マスタデータ.D_種目マスタ.Get_種目Class(Oya.Oya.区分番号, Oya.Oya.ラウンド番号, Oya.種目順).SG種別 <> "S" Then

                        For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)
                            'TES Base
                            TES得点(k).TES_Base = Oya.Oya.マスタデータ.J_新審判設定.TES設定(k)
                            BV(k) = Oya.Oya.マスタデータ.J_新審判設定.TES設定(k)

                        Next k

                    End If





                    'GOE
                    'GOE は、課題フィガー毎に全審判員の付与した点数のうち最高点と最低点を削除した残りの平均値を
                    'その選手の得点とする。 但し、BV=0 と判定された場合は自動的に GOE=0 と設定され

                    Dim TEMP_GOE得点リスト() As Decimal
                    Dim 一般ジャッジ人数 As Integer = 0

                    For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)

                        ReDim TEMP_GOE得点リスト(Oya.Get_一般審判員数)

                        一般ジャッジ人数 = 0
                        For j = 1 To Oya.審判員数

                            If Oya.審判員(j).ジャッジタイプ = "J" Then

                                一般ジャッジ人数 = 一般ジャッジ人数 + 1
                                If 審判員結果(j).SEND_FLAG = 1 Then
                                    TEMP_GOE得点リスト(一般ジャッジ人数) = 審判員結果(j).GOE素点(k).GOE素点
                                End If

                            End If

                        Next j

                        Dim Sort後_TEMP_GOE得点リスト = 配列ソート(TEMP_GOE得点リスト, "昇順")

                        'Min, Maxを除いた平均値
                        Dim Min As Decimal = Sort後_TEMP_GOE得点リスト(1)
                        Dim Max As Decimal = Sort後_TEMP_GOE得点リスト(UBound(TEMP_GOE得点リスト))

                        Dim AVE As Decimal = 0
                        For j = 2 To UBound(TEMP_GOE得点リスト) - 1
                            AVE = AVE + Sort後_TEMP_GOE得点リスト(j)
                        Next j

                        GOE得点(k).GOE得点 = AVE / (UBound(TEMP_GOE得点リスト) - 2)


                        'BVが0点（又はマイナス）の時は、GOEは0点にする。
                        If BV(k) <= 0 Then
                            GOE得点(k).GOE得点 = 0
                        End If

                    Next k






                End If

                'PCSの採点
                If Oya.Oya.マスタデータ.J_新審判設定.GetPCS数 > 0 Then

                    Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Read(Oya.Oya.区分番号, Oya.Oya.ラウンド番号)

                    For pcs = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetPCS数

                        '該当PCSの担当ジャッジ数を確認 
                        ' 2023/4/10 ジャッジから戻って来たファイルでの確認ではなく、あらかじめ設定された人数で確認する
                        Dim 該当PCS担当ジャッジ数 As Integer = 0
                        For j = 1 To Oya.審判員数

                            If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号) IsNot Nothing Then

                                If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号).担当PCS番号(Oya.種目順).Contains(CStr(pcs)) = True Then
                                    該当PCS担当ジャッジ数 = 該当PCS担当ジャッジ数 + 1
                                End If

                            End If


                            ' If 審判員結果(j).PCS素点(pcs).PCS採点対象FLAG = True Then
                            '該当PCS担当ジャッジ数 = 該当PCS担当ジャッジ数 + 1
                            'End If
                        Next j



                        If 該当PCS担当ジャッジ数 > 0 Then


                            'PCSの採点配列を作成
                            Dim PCS採点配列() As Decimal
                            ReDim PCS採点配列(Oya.審判員数)

                            For j = 1 To Oya.審判員数
                                If 審判員結果(j).PCS素点(pcs).PCS採点対象FLAG = True Then

                                    If 審判員結果(j).SEND_FLAG = 1 Then
                                        PCS採点配列(j) = 審判員結果(j).PCS素点(pcs).PCS素点
                                    Else
                                        PCS採点配列(j) = 0
                                    End If

                                End If
                            Next j



                            'PCS採点を降順でソート
                            PCS採点配列 = 配列ソート(PCS採点配列, "降順")


                            '採点対象審判員数から中間値を算出


                            Dim 中間点 As Double = 0

                            If (該当PCS担当ジャッジ数 Mod 2) = 0 Then
                                '審判員が偶数
                                中間点 = 中間点 + PCS採点配列(該当PCS担当ジャッジ数 \ 2)
                                中間点 = 中間点 + PCS採点配列(該当PCS担当ジャッジ数 \ 2 + 1)
                                中間点 = 中間点 / 2
                            Else
                                '審判員が奇数
                                中間点 = PCS採点配列(該当PCS担当ジャッジ数 \ 2 + 1)
                            End If


                            '除外対象の確定とPCS得点の集計

                            Dim PCS合計点 As Decimal = 0
                            Dim 範囲内ジャッジ人数 As Integer = 0
                            Dim 乖離度 As Decimal = 0

                            For j = 1 To Oya.審判員数
                                'If 審判員結果(j).PCS素点(pcs).PCS採点対象FLAG = True Then

                                If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号) IsNot Nothing Then

                                    If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号).担当PCS番号(Oya.種目順).Contains(CStr(pcs)) = True Then


                                        If 審判員結果(j).PCS素点(pcs).PCS素点 > (除外範囲 + 中間点) Or
                                       審判員結果(j).PCS素点(pcs).PCS素点 < (中間点 - 除外範囲) Then

                                            '採点対象で、除外範囲を超えていたら
                                            審判員結果(j).PCS素点(pcs).PCS無効FLAG = True

                                        Else
                                            '採点対象で、除外範囲を超えていない場合

                                            If 審判員結果(j).SEND_FLAG = 1 Then
                                                PCS合計点 = PCS合計点 + 審判員結果(j).PCS素点(pcs).PCS素点
                                            End If

                                            範囲内ジャッジ人数 = 範囲内ジャッジ人数 + 1
                                            審判員結果(j).PCS素点(pcs).PCS無効FLAG = False

                                            'まだSENDしていない時

                                        End If

                                        '乖離度の算出
                                        乖離度 = 0

                                        If 中間点 >= 審判員結果(j).PCS素点(pcs).PCS素点 Then
                                            乖離度 = (1 / (1 + (中間点 - 審判員結果(j).PCS素点(pcs).PCS素点) * (中間点 - 審判員結果(j).PCS素点(pcs).PCS素点)))
                                        Else
                                            乖離度 = (1 / (1 + (審判員結果(j).PCS素点(pcs).PCS素点 - 中間点) * (審判員結果(j).PCS素点(pcs).PCS素点 - 中間点)))
                                        End If
                                        審判員結果(j).PCS素点(pcs).乖離度 = 乖離度

                                    End If
                                End If
                                ' End If

                            Next j


                            'PCS得点の算出

                            'PDJ2.0J 2023/11/28 修正  PCSを乖離度で計算するよう変更

                            '① 乖離度の合計
                            '② 乖離度 x 審判点数 の合計
                            '③　PCS得点 =  ② ÷　①
                            '④ PCS倍率の反映

                            Dim 乖離度合計 As Decimal = 0
                            Dim 乖離度PCS合計 As Decimal = 0

                            For j = 1 To Oya.審判員数
                                If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号) IsNot Nothing Then

                                    If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号).担当PCS番号(Oya.種目順).Contains(CStr(pcs)) = True Then

                                        '①
                                        乖離度合計 = 乖離度合計 + 審判員結果(j).PCS素点(pcs).乖離度
                                        '②
                                        乖離度PCS合計 = 乖離度PCS合計 + (審判員結果(j).PCS素点(pcs).PCS素点 * 審判員結果(j).PCS素点(pcs).乖離度)

                                    End If
                                End If
                            Next j


                            '③
                            If 乖離度合計 <> 0 Then
                                PCS得点(pcs).PCS得点 = 乖離度PCS合計 / 乖離度合計
                                PCS得点(pcs).PCS得点 = Math.Round(PCS得点(pcs).PCS得点, 3, MidpointRounding.AwayFromZero)
                            End If


                            '④
                            Dim 倍率 As Decimal = Oya.Oya.マスタデータ.J_新審判設定.PCS設定(pcs).倍率
                            PCS得点(pcs).PCS得点 = PCS得点(pcs).PCS得点 * 倍率



                        End If
                    Next pcs

                End If




                '減点の計算

                'レフリーがいるか確認
                If Oya.レフリー番号 <> 0 Then

                    If Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数 > 0 Then
                        '通常減点

                        失格FLAG = False

                        For d = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数
                            一般減点(d).一般減点 = 審判員結果(Oya.レフリー番号).一般減点素点(d).一般減点素点

                            '失格判定
                            If 審判員結果(Oya.レフリー番号).一般減点素点(d).一般減点素点 = 1 Then
                                失格FLAG = True
                            Else
                                '失格FLAG = False
                            End If


                        Next d

                    End If

                End If




                '選手得点の計算
                種目得点 = 0

                'TESの計算
                Dim TES As Decimal = 0
                Dim GOE As Decimal = 0

                For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)

                    If BV(k) <= 0 Then
                        'BVが0点又はマイナスと判定されたとき
                        'TESは0点とする。

                    Else

                        'TES Base
                        TES = TES + TES得点(k).TES_Base

                        'TES減点
                        For r = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetTES減点数
                            TES = TES + TES得点(k).TES減点(r).TES減点
                        Next r

                        'GOEの点数
                        GOE = GOE + GOE得点(k).GOE得点

                    End If







                Next k


                種目得点 = 種目得点 + TES + GOE


                For pcs = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetPCS数

                    'PCS倍率の取得
                    Dim 倍率 As Double = Oya.Oya.マスタデータ.J_新審判設定.PCS設定(pcs).倍率

                    '種目得点の算出
                    種目得点 = 種目得点 + (PCS得点(pcs).PCS得点)

                Next pcs



                '===減点を足しこみ
                For d = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数
                    種目得点 = 種目得点 + 一般減点(d).一般減点
                Next d


                '減点が多すぎてマイナスになった時は０にする
                If 種目得点 < 0 Then
                    種目得点 = 0
                End If


                '失格判定
                If 失格FLAG = True Then
                    種目得点 = 0
                End If


                '表示用の合計計算  ---->  必要であれば関数で対応しよう。

                'For j = 1 To Oya.審判員数

                ' 審判(j).表示用PCS合計点 = 0
                ' 審判(j).表示用減点合計点 = 0
                'For pcs = 1 To 10
                '審判(j).表示用PCS合計点 = 審判(j).表示用PCS合計点 + 審判(j).PCS素点(pcs)
                'Next pcs
                'For 減点番号 = 2 To 20
                '審判(j).表示用減点合計点 = 審判(j).表示用減点合計点 + 審判(j).減点素点(減点番号)
                'Next 減点番号
                'Next j





            End Sub


            Public Sub 採点_VAL()
                '===========================
                '概要　バルカーカップ用のPCS採点。  １選手の10PCS の結果     PCSは乖離度で計算 　 
                '入力　審判結果(各審判員の採点結果)
                '出力　TES, PCS得点　一般減点 失格FLAG　　　および乖離度    
                '===========================


                Dim BV() As Decimal
                ReDim BV(Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号))


                'オーディエンスの採点読込



                Dim AUD結果 As V_VALQオーディエンス_C = New V_VALQオーディエンス_C(Oya.Oya.マスタデータ.Z_システム設定.Comp_filepath, Oya.Oya.ラウンド番号)

                'オーディエンスの採点
                'Ohter得点の項目名に　オーディエンスと設定する
                'Other得点の得点に、オーディエンスの得点を設定する

                If Oya.Oya.ラウンド番号 = "400" Then

                    Other得点(2).項目名 = "オーディエンス"
                    Dim AUD最高点 As Decimal = AUD結果.AUD最高点

                    Dim AUD採点詳細 As V_VALQオーディエンス_C.採点_C.採点詳細_C = AUD結果.Get_採点(背番号)

                    If AUD採点詳細 IsNot Nothing Then
                        Other得点(2).実得点 = AUD結果.Get_採点(背番号).AUD得点
                        Other得点(2).結果得点 = AUD結果.Get_採点(背番号).AUD得点 / AUD最高点 * 60
                    Else
                        Other得点(2).実得点 = 0
                    End If

                End If


                '審判無効の読込

                無効審判 = New G_審判無効(Oya.Oya.マスタデータ.Z_システム設定.Comp_filepath, Oya.Oya.区分番号, Oya.Oya.ラウンド番号)



                'TESの採点
                If Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号) > 0 Then

                    For j = 1 To Oya.審判員数

                        '技術判定員
                        If Oya.審判員(j).ジャッジタイプ = "T" Then


                            For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)



                                'TES Base
                                TES得点(k).TES_Base = 審判員結果(j).TES素点(k).TES_Base
                                BV(k) = 審判員結果(j).TES素点(k).TES_Base

                                'TES減点
                                For r = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetTES減点数

                                    If 審判員結果(j).SEND_FLAG = 1 Then
                                        TES得点(k).TES減点(r).TES減点 = 審判員結果(j).TES素点(k).TES減点(r).TES減点
                                    End If
                                    TES得点(k).TES減点(r).TES減点項目名 = Oya.Oya.マスタデータ.J_新審判設定.TES減点設定(r).減点略称

                                    BV(k) = BV(k) + 審判員結果(j).TES素点(k).TES減点(r).TES減点
                                Next r

                            Next k

                            j = Oya.審判員数
                        End If
                    Next j

                    'ソロ競技ではない時は、基礎点として、TES_Baseを設定する。
                    If Oya.Oya.マスタデータ.D_種目マスタ.Get_種目Class(Oya.Oya.区分番号, Oya.Oya.ラウンド番号, Oya.種目順).SG種別 <> "S" Then

                        For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)
                            'TES Base
                            TES得点(k).TES_Base = Oya.Oya.マスタデータ.J_新審判設定.TES設定(k)
                            BV(k) = Oya.Oya.マスタデータ.J_新審判設定.TES設定(k)

                        Next k

                    End If





                    'GOE
                    'GOE は、課題フィガー毎に全審判員の付与した点数のうち最高点と最低点を削除した残りの平均値を
                    'その選手の得点とする。 但し、BV=0 と判定された場合は自動的に GOE=0 と設定され

                    Dim TEMP_GOE得点リスト() As Decimal
                    Dim 一般ジャッジ人数 As Integer = 0

                    For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)

                        ReDim TEMP_GOE得点リスト(Oya.Get_一般審判員数)

                        一般ジャッジ人数 = 0
                        For j = 1 To Oya.審判員数

                            If Oya.審判員(j).ジャッジタイプ = "J" Then

                                一般ジャッジ人数 = 一般ジャッジ人数 + 1
                                If 審判員結果(j).SEND_FLAG = 1 Then
                                    TEMP_GOE得点リスト(一般ジャッジ人数) = 審判員結果(j).GOE素点(k).GOE素点
                                End If

                            End If

                        Next j

                        Dim Sort後_TEMP_GOE得点リスト = 配列ソート(TEMP_GOE得点リスト, "昇順")

                        'Min, Maxを除いた平均値
                        Dim Min As Decimal = Sort後_TEMP_GOE得点リスト(1)
                        Dim Max As Decimal = Sort後_TEMP_GOE得点リスト(UBound(TEMP_GOE得点リスト))

                        Dim AVE As Decimal = 0
                        For j = 2 To UBound(TEMP_GOE得点リスト) - 1
                            AVE = AVE + Sort後_TEMP_GOE得点リスト(j)
                        Next j

                        GOE得点(k).GOE得点 = AVE / (UBound(TEMP_GOE得点リスト) - 2)


                        'BVが0点（又はマイナス）の時は、GOEは0点にする。
                        If BV(k) <= 0 Then
                            GOE得点(k).GOE得点 = 0
                        End If

                    Next k






                End If

                'PCSの採点
                If Oya.Oya.マスタデータ.J_新審判設定.GetPCS数 > 0 Then

                    Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Read(Oya.Oya.区分番号, Oya.Oya.ラウンド番号)

                    For pcs = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetPCS数

                        '該当PCSの担当ジャッジ数を確認 
                        ' 2023/4/10 ジャッジから戻って来たファイルでの確認ではなく、あらかじめ設定された人数で確認する
                        'ただし、無効審判リストに入っている審判はカウントしない
                        Dim 該当PCS担当ジャッジ数 As Integer = 0

                        For j = 1 To Oya.審判員数

                            If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号) IsNot Nothing Then

                                If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号).担当PCS番号(Oya.種目順).Contains(CStr(pcs)) = True Then
                                    該当PCS担当ジャッジ数 = 該当PCS担当ジャッジ数 + 1
                                End If

                            End If

                        Next j

                        '無効審判数を引く
                        Dim 無効審判数 As Integer = 0
                        If 無効審判.無効_詳細 IsNot Nothing Then
                            For s = 1 To UBound(無効審判.無効_詳細)
                                If 無効審判.無効_詳細(s).背番号 = 背番号 Then
                                    For j = 1 To UBound(無効審判.無効_詳細(s).ジャッジ詳細)
                                        'この選手に対して無効審判が設定されている
                                        If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(無効審判.無効_詳細(s).ジャッジ詳細(j).ジャッジ記号) IsNot Nothing Then
                                            If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(無効審判.無効_詳細(s).ジャッジ詳細(j).ジャッジ記号).担当PCS番号(Oya.種目順).Contains(CStr(pcs)) = True Then
                                                If 無効審判.無効_詳細(s).ジャッジ詳細(j).無効FLAG = 1 Then
                                                    無効審判数 = 無効審判数 + 1
                                                End If
                                            End If
                                        End If
                                    Next j
                                    s = UBound(無効審判.無効_詳細)
                                End If
                            Next s
                        End If


                        該当PCS担当ジャッジ数 = 該当PCS担当ジャッジ数 - 無効審判数


                        If 該当PCS担当ジャッジ数 > 0 Then


                            'PCSの採点配列を作成
                            Dim PCS採点配列() As Decimal
                            ReDim PCS採点配列(Oya.審判員数)

                            For j = 1 To Oya.審判員数
                                If 審判員結果(j).PCS素点(pcs).PCS採点対象FLAG = True Then

                                    If 審判員結果(j).SEND_FLAG = 1 Then

                                        If 無効審判.無効判定(背番号, 審判員結果(j).ジャッジ記号) = 0 Then
                                            PCS採点配列(j) = 審判員結果(j).PCS素点(pcs).PCS素点
                                        Else
                                            '無効審判の場合は0点とする
                                            PCS採点配列(j) = 0
                                        End If

                                    Else
                                        PCS採点配列(j) = 0
                                    End If

                                End If
                            Next j



                            'PCS採点を降順でソート
                            PCS採点配列 = 配列ソート(PCS採点配列, "降順")

                            Dim MAX点 As Decimal = PCS採点配列(1)
                            Dim MIN点 As Decimal = PCS採点配列(該当PCS担当ジャッジ数)

                            Dim MAX点ジャッジ As String = ""
                            Dim MIN点ジャッジ As String = ""


                            '採点対象審判員数から中間値を算出


                            Dim 中間点 As Double = 0

                            If (該当PCS担当ジャッジ数 Mod 2) = 0 Then
                                '審判員が偶数
                                中間点 = 中間点 + PCS採点配列(該当PCS担当ジャッジ数 \ 2)
                                中間点 = 中間点 + PCS採点配列(該当PCS担当ジャッジ数 \ 2 + 1)
                                中間点 = 中間点 / 2
                            Else
                                '審判員が奇数
                                中間点 = PCS採点配列(該当PCS担当ジャッジ数 \ 2 + 1)
                            End If


                            '除外対象の確定とPCS得点の集計

                            Dim PCS合計点 As Decimal = 0
                            Dim 範囲内ジャッジ人数 As Integer = 0
                            Dim 乖離度 As Decimal = 0

                            For j = 1 To Oya.審判員数

                                If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号) IsNot Nothing Then

                                    If Oya.Oya.マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(審判員結果(j).ジャッジ記号).担当PCS番号(Oya.種目順).Contains(CStr(pcs)) = True Then

                                        If 無効審判.無効判定(背番号, 審判員結果(j).ジャッジ記号) = 0 Then




                                            If MAX点ジャッジ = "" And 審判員結果(j).PCS素点(pcs).PCS素点 = MAX点 Then

                                                MAX点ジャッジ = 審判員結果(j).ジャッジ記号

                                                '採点対象で、MAX点だったら超えていたら
                                                審判員結果(j).PCS素点(pcs).PCS無効FLAG = True

                                            ElseIf MIN点ジャッジ = "" And 審判員結果(j).PCS素点(pcs).PCS素点 = MIN点 Then

                                                MIN点ジャッジ = 審判員結果(j).ジャッジ記号
                                                '採点対象で、MIN点だったら超えていたら
                                                審判員結果(j).PCS素点(pcs).PCS無効FLAG = True


                                            Else
                                                '採点対象で、MIN点でもMAX点でもない場合

                                                If 審判員結果(j).SEND_FLAG = 1 Then
                                                    PCS合計点 = PCS合計点 + 審判員結果(j).PCS素点(pcs).PCS素点
                                                End If

                                                範囲内ジャッジ人数 = 範囲内ジャッジ人数 + 1
                                                審判員結果(j).PCS素点(pcs).PCS無効FLAG = False



                                            End If

                                            '乖離度の算出
                                            乖離度 = 0

                                            If 中間点 >= 審判員結果(j).PCS素点(pcs).PCS素点 Then
                                                乖離度 = (1 / (1 + (中間点 - 審判員結果(j).PCS素点(pcs).PCS素点) * (中間点 - 審判員結果(j).PCS素点(pcs).PCS素点)))
                                            Else
                                                乖離度 = (1 / (1 + (審判員結果(j).PCS素点(pcs).PCS素点 - 中間点) * (審判員結果(j).PCS素点(pcs).PCS素点 - 中間点)))
                                            End If
                                            審判員結果(j).PCS素点(pcs).乖離度 = 乖離度

                                        End If

                                    End If
                                End If
                                ' End If

                            Next j


                            'PCS得点の算出

                            '上下カットした残りの平均値

                            PCS得点(pcs).PCS得点 = PCS合計点 / 範囲内ジャッジ人数
                            PCS得点(pcs).PCS得点 = Math.Round(PCS得点(pcs).PCS得点, 3, MidpointRounding.AwayFromZero)


                            Dim 倍率 As Decimal = Oya.Oya.マスタデータ.J_新審判設定.PCS設定(pcs).倍率
                            PCS得点(pcs).PCS得点 = PCS得点(pcs).PCS得点 * 倍率


                        End If
                    Next pcs

                End If




                '減点の計算

                'レフリーがいるか確認
                If Oya.レフリー番号 <> 0 Then

                    If Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数 > 0 Then
                        '通常減点

                        失格FLAG = False

                        For d = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数
                            一般減点(d).一般減点 = 審判員結果(Oya.レフリー番号).一般減点素点(d).一般減点素点

                            '失格判定
                            If 審判員結果(Oya.レフリー番号).一般減点素点(d).一般減点素点 = 1 Then
                                失格FLAG = True
                            Else
                                '失格FLAG = False
                            End If


                        Next d

                    End If

                End If




                '選手得点の計算
                種目得点 = 0

                'TESの計算
                Dim TES As Decimal = 0
                Dim GOE As Decimal = 0

                For k = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)

                    If BV(k) <= 0 Then
                        'BVが0点又はマイナスと判定されたとき
                        'TESは0点とする。

                    Else

                        'TES Base
                        TES = TES + TES得点(k).TES_Base

                        'TES減点
                        For r = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetTES減点数
                            TES = TES + TES得点(k).TES減点(r).TES減点
                        Next r

                        'GOEの点数
                        GOE = GOE + GOE得点(k).GOE得点

                    End If

                Next k


                種目得点 = 種目得点 + TES + GOE



                'PCSの実得点
                Other得点(1).項目名 = "VAL用PCS"


                For pcs = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetPCS数

                    'PCS倍率の取得
                    Dim 倍率 As Double = Oya.Oya.マスタデータ.J_新審判設定.PCS設定(pcs).倍率

                    Other得点(1).実得点 = Other得点(1).実得点 + (PCS得点(pcs).PCS得点)


                Next pcs

                '換算をするためには最高点を取得しなければならないが、全員の採点が終わってからじゃないと計算できない

                '種目得点の算出    種目得点にはPCSの実点数は入れない
                '種目得点 = 種目得点 + 


                '===減点を足しこみ
                For d = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数
                    種目得点 = 種目得点 + 一般減点(d).一般減点
                Next d


                '減点が多すぎてマイナスになった時は０にする
                If 種目得点 < 0 Then
                    種目得点 = 0
                End If


                '失格判定
                If 失格FLAG = True Then
                    種目得点 = 0
                End If


                '表示用の合計計算  ---->  必要であれば関数で対応しよう。

                'For j = 1 To Oya.審判員数

                ' 審判(j).表示用PCS合計点 = 0
                ' 審判(j).表示用減点合計点 = 0
                'For pcs = 1 To 10
                '審判(j).表示用PCS合計点 = 審判(j).表示用PCS合計点 + 審判(j).PCS素点(pcs)
                'Next pcs
                'For 減点番号 = 2 To 20
                '審判(j).表示用減点合計点 = 審判(j).表示用減点合計点 + 審判(j).減点素点(減点番号)
                'Next 減点番号
                'Next j






            End Sub



            Public Function Get_表示用PCS合計点()

                Dim rc As Decimal = 0

                If Oya.Oya.マスタデータ.J_新審判設定.GetPCS数 > 0 Then

                    For p = 1 To Oya.Oya.マスタデータ.J_新審判設定.GetPCS数

                        rc = rc + PCS得点(p).PCS得点

                    Next p
                End If


                Return rc

            End Function

            Public Function Get_表示用一般減点()

                Dim rc As Decimal = 0

                If Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数 > 0 Then

                    For r = 1 To Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数   '減点番号１は、失格判定なので除外する

                        If 一般減点(r).一般減点 < 0 Then
                            rc = rc + 一般減点(r).一般減点
                        End If

                    Next r
                End If


                Return rc

            End Function

            Private Function 配列ソート(ORG_ As Array, ソートタイプ As String) As Object
                '配列のソート
                '入力 : ORG配列,  ソートタイプ (昇順、降順）
                '出力:  New配列 （配列番号は１から開始）


                Dim rc As Array = ORG_


                Array.Sort(rc, 1, UBound(rc))

                If ソートタイプ = "降順" Then
                    Array.Reverse(rc, 1, UBound(rc))
                End If


                Return rc

            End Function



            '****************************************************************
            '
            '    審判員結果
            '
            '****************************************************************

            Public Class 審判員結果_C

                Private Oya As 選手結果_C

                Public Property ジャッジ記号 As String

                Public TES素点() As TES得点_C  '課題数分

                Public PCS素点() As PCS素点_C　　'PCS数分

                Public GOE素点() As GOE素点_C    '課題数分

                Public 一般減点素点() As 一般減点素点_C


                Public SEND_FLAG As Integer   'ヒート毎SEND


                Public Property 素点 As Decimal
                Public Property 備考 As String


                'BRプレのみで使用
                Public Property 順位点 As Decimal

                '************************************************
                '　　 審判員結果　メソッド
                '************************************************

                Public Sub New()
                    '作るだけ

                End Sub

                Public Sub New(ジャッジ記号_ As String, Oya_ As 選手結果_C)

                    Oya = Oya_

                    ジャッジ記号 = ジャッジ記号_

                    '初期化

                    'TESの設定
                    If Oya.Oya.Oya.マスタデータ.J_新審判設定.Get課題数(Oya.ソロ種目記号) > 0 Then
                        初期化TES素点(Oya.Oya.Oya.マスタデータ.J_新審判設定.Get課題数(Oya.ソロ種目記号), Oya.Oya.Oya.マスタデータ.J_新審判設定.GetTES減点数)
                    End If


                    'PCSの設定
                    If Oya.Oya.Oya.マスタデータ.J_新審判設定.GetPCS数 > 0 Then
                        初期化PCS素点(Oya.Oya.Oya.マスタデータ.J_新審判設定.GetPCS数)
                    End If


                    '一般減点の初期化
                    If Oya.Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数 > 0 Then
                        初期化一般減点素点(Oya.Oya.Oya.マスタデータ.J_新審判設定.Get減点項目数)
                    End If

                    'GOEの設定
                    If Oya.Oya.Oya.マスタデータ.J_新審判設定.Get課題数(Oya.ソロ種目記号) > 0 Then
                        初期化GOE素点(Oya.Oya.Oya.マスタデータ.J_新審判設定.Get課題数(Oya.ソロ種目記号))
                    End If



                End Sub


                Public Sub 初期化TES素点(課題数, TES減点項目数)

                    If 課題数 > 0 Then

                        ReDim TES素点(課題数)
                        For p = 1 To 課題数
                            TES素点(p) = New TES得点_C(TES減点項目数)
                        Next p
                    End If

                End Sub

                Public Sub 初期化PCS素点(PCS数)

                    If PCS数 > 0 Then
                        ReDim PCS素点(PCS数)
                        For p = 1 To PCS数
                            PCS素点(p) = New PCS素点_C
                        Next p

                    End If

                End Sub

                Public Sub 初期化GOE素点(課題数)

                    If 課題数 > 0 Then

                        ReDim GOE素点(課題数)
                        For k = 1 To 課題数
                            GOE素点(k) = New GOE素点_C()
                        Next k
                    End If

                End Sub

                Public Sub 初期化一般減点素点(減点項目数)

                    If 減点項目数 > 0 Then
                        ReDim 一般減点素点(減点項目数)

                        For r = 1 To 減点項目数
                            一般減点素点(r) = New 一般減点素点_C

                        Next r

                    End If

                End Sub


                '************************************************
                '　　子クラス定義　for 審判員結果
                '************************************************



                Public Class PCS素点_C

                    Public Property PCS採点対象FLAG As Boolean
                    Public Property PCS素点 As Decimal
                    Public Property 乖離度 As Decimal

                    Public Property PCS無効FLAG As Boolean

                End Class

                Public Class GOE素点_C

                    Public Property GOE素点 As Decimal

                    Public Property GOE無効FLAG As Boolean

                End Class

                Public Class 一般減点素点_C

                    Public Property 一般減点素点 As Decimal


                End Class



            End Class  '審判員結果結果クラスのEND


            Public Class TES得点_C

                Public Property TES_Base As Decimal

                Public TES減点() As TES減点_C

                Public Sub New(TES減点数)

                    If TES減点数 > 0 Then

                        ReDim TES減点(TES減点数)
                        For r = 1 To TES減点数
                            TES減点(r) = New TES減点_C
                        Next r

                    End If
                End Sub

            End Class

            '************************************************
            '　　子クラス定義　for 選手結果クラス
            '************************************************


            Public Class TES減点_C
                '=== 選手結果と審判員結果で共有
                Public Property TES減点項目名 As String
                Public Property TES減点 As Decimal

            End Class

            Public Class PCS得点_C

                Public Property PCS得点 As Decimal

            End Class

            Public Class GOE得点_C

                Public Property GOE得点 As Decimal

            End Class

            Public Class 一般減点_C

                Public Property 一般減点 As Decimal

            End Class


            Public Class Other得点_C
                Public Property 項目名 As String

                Public Property 実得点 As Decimal
                Public Property 結果得点 As Decimal
                Public Property 順位 As Decimal

            End Class


        End Class    '選手結果クラスのEND


        '************************************************
        '　　子クラス定義　for 種目結果クラス
        '************************************************


        Public Class 審判員_C
            Public Property ジャッジ記号 As String
            Public Property ジャッジ表記名 As String

            Public Property ジャッジタイプ As String

        End Class


    End Class　 '種目結果クラスのEND


End Class
