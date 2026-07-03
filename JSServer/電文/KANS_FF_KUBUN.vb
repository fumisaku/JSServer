'決勝入賞者一覧用の区分一覧を返す
Public Class KANS_FF_KUBUN


    Public 総区分数 As Integer

    Private 区分一覧(,)
    '1: 区分番号
    '2: 区分記号
    '3: 区分名
    '4: 区分表記名
    '5: ステータス

    Function Create電文(端末名 As String, マスタデータ As マスタデータ) As String()

        '全区分数の算出 ブレイキンはグループでカウント
        総区分数 = 区分一覧の作成(マスタデータ)



        Dim 電文区分数 As Integer = 100

        Dim 全レコード数 As Integer
        If 総区分数 Mod 電文区分数 = 0 Then
            全レコード数 = 総区分数 \ 電文区分数
        Else
            全レコード数 = 総区分数 \ 電文区分数 + 1
        End If

        'JK
        'KANS_FF_KUBUN
        '送信元端末名
        '全レコード数
        '当レコード番号
        '総区分数
        '区分数(当電文)

        Dim Denbun() As String
        ReDim Denbun(全レコード数)



        Dim レコード番号 As Integer

        For レコード番号 = 1 To 全レコード数
            Denbun(レコード番号) = "JK,KANS_FF_KUBUN,"
            Denbun(レコード番号) = Denbun(レコード番号) & 端末名 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 全レコード数 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & レコード番号 & ","
            Denbun(レコード番号) = Denbun(レコード番号) & 総区分数 & ","

            Dim 電文毎の進行区分数 As Integer = 0

            If 総区分数 / 電文区分数 > レコード番号 Then
                電文毎の進行区分数 = 電文区分数
            ElseIf 総区分数 / 電文区分数 = レコード番号 Then
                電文毎の進行区分数 = 総区分数 Mod 電文区分数
            Else
                電文毎の進行区分数 = 総区分数
            End If

            Denbun（レコード番号） = Denbun(レコード番号) & 電文毎の進行区分数 & ","     '電文毎の進行区分数   


        Next レコード番号


        For s = 1 To 総区分数

            Dim 電文No As Integer
            If s Mod 電文区分数 = 0 Then
                電文No = s \ 電文区分数
            Else
                電文No = s \ 電文区分数 + 1
            End If

            Denbun（電文No） = Denbun(電文No) & 区分一覧(s, 1) & ","  '区分番号
            Denbun（電文No） = Denbun(電文No) & 区分一覧(s, 2) & ","  '区分記号
            Denbun（電文No） = Denbun(電文No) & 区分一覧(s, 3) & ","  '区分名
            Denbun（電文No） = Denbun(電文No) & 区分一覧(s, 4) & ","  '区分表記名
            Denbun（電文No） = Denbun(電文No) & 区分一覧(s, 5) & ","  'ステータス


        Next s


        Return Denbun

    End Function

    Private Function 区分一覧の作成(マスタデータ As マスタデータ) As Integer
        '区分数を返す

        ReDim 区分一覧(マスタデータ.B_区分マスタ.登録済みレコード数, 5)
        '1: 区分番号
        '2: 区分記号
        '3: 区分名
        '4: 区分表記名
        '5: ステータス
        Dim Z As Integer = 0

        'まずはブレイキンから確認
        マスタデータ.BR_カテゴリマスタ.FileRead()
        マスタデータ.T_採点進行管理.FileRead()

        'ブレイキンのグループID
        Dim ブレイキングループ(20, 2) As String
        '1列目  グループID
        '2列目  "OK" or ""   既に区分一覧に記帳したかしていないか


        If マスタデータ.BR_カテゴリマスタ.登録済みレコード数 > 0 Then
            For b = 1 To マスタデータ.BR_カテゴリマスタ.登録済みレコード数
                ブレイキングループ(b, 1) = マスタデータ.BR_カテゴリマスタ.リスト(b).カテゴリ番号
            Next b
        End If


        For k = 1 To マスタデータ.B_区分マスタ.登録済みレコード数

            Dim 区分 As B_区分 = マスタデータ.B_区分マスタ.リスト(k)

            'ブレイキンかどうかの判定
            Dim ブレイキンFLAG As String = ""   '該当する場合はカテゴリ番号が入る

            For b = 1 To UBound(マスタデータ.BR_グループマスタ.リスト)
                If マスタデータ.BR_グループマスタ.リスト(b) IsNot Nothing Then
                    If マスタデータ.BR_グループマスタ.リスト(b).区分番号 = 区分.区分番号 Then
                        ブレイキンFLAG = マスタデータ.BR_グループマスタ.リスト(b).カテゴリ番号
                        b = UBound(マスタデータ.BR_グループマスタ.リスト)
                    End If
                End If
            Next b

            If ブレイキンFLAG <> "" Then
                'ブレイキンの場合

                For bb = 1 To UBound(ブレイキングループ)
                    If ブレイキンFLAG = ブレイキングループ(bb, 1) Then
                        If ブレイキングループ(bb, 2) = "" Then

                            Z = Z + 1
                            区分一覧(Z, 1) = ブレイキンFLAG
                            区分一覧(Z, 2) = 区分.区分記号
                            区分一覧(Z, 3) = マスタデータ.BR_カテゴリマスタ.Getカテゴリ名(ブレイキンFLAG)
                            区分一覧(Z, 4) = マスタデータ.BR_カテゴリマスタ.Getカテゴリ名(ブレイキンFLAG)

                            'ステータスの確認
                            区分一覧(Z, 5) = マスタデータ.T_採点進行管理.Get_採点進行Class(マスタデータ.BR_グループマスタ.Get決勝区分番号(ブレイキンFLAG), "400").ステータス


                            ブレイキングループ(bb, 2) = "OK"   '作成済みを記帳する
                        End If
                        bb = UBound(ブレイキングループ)
                    End If
                Next bb
            Else
                'ブレイキンではない通常の場合

                Z = Z + 1
                区分一覧(Z, 1) = 区分.区分番号
                区分一覧(Z, 2) = 区分.区分記号
                区分一覧(Z, 3) = 区分.区分名
                区分一覧(Z, 4) = 区分.区分表記名

                'ステータスの確認
                If マスタデータ.T_採点進行管理.Get_採点進行Class(区分.区分番号, "400") IsNot Nothing Then
                    区分一覧(Z, 5) = マスタデータ.T_採点進行管理.Get_採点進行Class(区分.区分番号, "400").ステータス
                Else
                    区分一覧(Z, 5) = ""
                End If


            End If
        Next k

        Return Z

    End Function



End Class
