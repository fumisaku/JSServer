Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class SC_J_詳細結果

    '各ラウンドの詳細結果を一つのJsonファイルで表示するもの。
    '司会端末や結果表示での利用を想定
    Private ReadOnly filepath As String


    Public Property 競技会NO As String
    Public Property 競技会名 As String


    Public Property 区分番号 As String
    Public Property 区分記号 As String
    Public Property 区分名 As String
    Public Property カテゴリ As String

    Public Property ラウンド番号 As String
    Public Property ラウンド名 As String
    Public Property 採点方式 As String
    Public Property UP予定数 As Integer
    Public Property UP数 As Integer


    Public Property ステータス As String  '開始前　　3-4(3種目目の4ヒートまで採点完了）　　終了

    Public 種目定義() As SC_J_詳細結果_種目定義
    Public 出場選手() As SC_J_詳細結果_出場選手
    Public ジャッジ() As SC_J_詳細結果_ジャッジ
    Public PCS設定() As SC_J_詳細結果_PCS設定
    Public 減点設定() As SC_J_詳細結果_減点設定
    Public 総合結果() As SC_J_詳細結果_総合結果



    Public Class SC_J_詳細結果_種目定義

        Public Property 種目順 As Integer
        Public Property 種目記号 As String
        Public Property 種目名 As String
        Public Property SG種別 As String
        Public Property ヒート数 As String
        Public Property CaliMax As Decimal
        Public Property CaliMin As Decimal


    End Class

    Public Class SC_J_詳細結果_出場選手
        Public Property 背番号 As String
        Public Property L会員番号 As String
        Public Property L氏名 As String
        Public Property Lフリガナ As String
        Public Property L所属 As String

        Public Property P会員番号 As String
        Public Property P氏名 As String
        Public Property Pフリガナ As String
        Public Property P所属 As String

        Public Property カップル所属 As String
    End Class

    Public Class SC_J_詳細結果_ジャッジ

        Public Property 記号 As String
        Public Property ジャッジ会員番号 As String
        Public Property ジャッジ氏名 As String
        Public Property ジャッジフリガナ As String
        Public Property ジャッジ所属 As String
        Public Property Role As String

    End Class

    Public Class SC_J_詳細結果_PCS設定

        Public Property PCS番号 As Integer
        Public Property PCS略称 As String
        Public Property 倍率 As Decimal

    End Class

    Public Class SC_J_詳細結果_減点設定

        Public Property 減点番号 As Integer
        Public Property 減点名称 As String
        Public Property 減点名称英語 As String
        Public Property 適用対象 As String  'SGM

    End Class

    Public Class SC_J_詳細結果_総合結果
        '5種目の順位

        Public Property 背番号 As String
        Public Property 順位番号 As String
        Public Property 順位表記 As String  '同点有り

        Public Property 総合得点 As Decimal

        Public Property UP As String　　　'決勝以外で使用


        Public 種目結果() As SC_J_詳細結果_総合結果_種目結果


        Public Class SC_J_詳細結果_総合結果_種目結果
            Public Property ヒート番号 As Integer

            Public Property 種目得点 As Decimal
            Public Property 種目順位番号 As Decimal
            Public Property 種目順位表記 As Decimal

            Public Property PCS合計 As Decimal
            Public Property 減点合計 As Decimal

            Public PCS詳細() As SC_J_詳細結果_総合結果_種目結果_PCS詳細
            Public 減点詳細() As SC_J_詳細結果_総合結果_種目結果_減点詳細


            Public Class SC_J_詳細結果_総合結果_種目結果_PCS詳細
                Public Property PCS番号 As Integer
                Public Property PCS得点 As Decimal

                Public ジャッジ得点() As SC_J_詳細結果_総合結果_種目結果_PCS詳細_ジャッジ得点


                Public Class SC_J_詳細結果_総合結果_種目結果_PCS詳細_ジャッジ得点

                    Public Property ジャッジ記号 As String
                    Public Property 対象 As Integer
                    Public Property 素点 As Decimal
                    Public Property 対象外FLAG As Boolean
                    Public Property 乖離度 As Decimal

                End Class

            End Class

            Public Class SC_J_詳細結果_総合結果_種目結果_減点詳細
                Public Property 減点番号 As Integer
                Public Property 減点点数 As Decimal

                Public ジャッジ得点() As SC_J_詳細結果_総合結果_種目結果_減点詳細_ジャッジ得点


                Public Class SC_J_詳細結果_総合結果_種目結果_減点詳細_ジャッジ得点
                    Public Property ジャッジ番号 As String
                    Public Property 素点 As Decimal

                End Class

            End Class


        End Class

    End Class

    '==========データ項目定義　　ここまで===========================

    Public Sub New(filepath_)

        filepath = filepath_

    End Sub


    Public Sub JSON書き出し()

        Dim filename As String = "SC_J_詳細結果_" & 区分番号 & "_" & ラウンド番号 & ".json"


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

    Public Function JSON読込み(JSON文字列) As SC_J_詳細結果

        Dim rc As SC_J_詳細結果 = Nothing


        ''JSONにデシリアライズする
        rc = JsonConvert.DeserializeObject(Of SC_J_詳細結果)(JSON文字列)

        Return rc

    End Function

    Public Function JSON読み込み(区分番号_ As String, ラウンド番号_ As String) As SC_J_詳細結果

        Dim rc As SC_J_詳細結果 = Nothing

        Dim filename As String = "SC_J_詳細結果_" & 区分番号_ & "_" & ラウンド番号_ & ".json"


        ''JSON読み込み用
        Dim jText As String = String.Empty


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した


            ''ファイルからJSONを読み込む
            Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)

            jText = cReader.ReadToEnd

            rc = JsonConvert.DeserializeObject(Of SC_J_詳細結果)(jText)

            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()


        End If


        Return rc
    End Function


    Public Sub データ設定(採点結果 As 採点結果_C)
        '採点結果を元に、当Classデータを構築

        Me.競技会NO = 採点結果.マスタデータ.A_競技会マスタ.公認競技会NO
        Me.競技会名 = 採点結果.マスタデータ.A_競技会マスタ.競技会名

        Me.区分番号 = 採点結果.区分番号
        Dim 区分C As B_区分 = 採点結果.マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Me.区分記号 = 区分C.区分記号
        Me.区分名 = 区分C.区分表記名
        Me.カテゴリ = 区分C.カテゴリ

        Dim 選手リスト番号 As String = 区分C.使用する選手マスタ

        Me.ラウンド番号 = 採点結果.ラウンド番号
        Dim ラウンドC As C_ラウンド = 採点結果.マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号)
        Me.ラウンド名 = 採点結果.マスタデータ.Get_ラウンド名(ラウンド番号)
        Me.採点方式 = ラウンドC.採点方式
        Me.UP予定数 = ラウンドC.UP予定数

        'ステータス
        '① 採点済みか？
        Dim T_採点進行 As T_採点進行
        T_採点進行 = 採点結果.マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

        If T_採点進行.ステータス = "採点済み" Then
            Me.ステータス = "採点済み"


        ElseIf Me.ステータス = "準備前" Then
            '②準備前だったら何もしない。
            Exit Sub

        Else
            'ヒート表は作られているが、まだ採点終了していない。競技途中ということ。
            Dim 競技番号 As String = T_採点進行.競技番号
            Dim 競技枝番 As String = T_採点進行.競技番号枝番

            Dim 完了種目番号 As Integer = 0
            Dim 完了ヒート番号 As Integer = 0


            採点結果.マスタデータ.U_進行管理.FileRead()
            For u = 1 To 採点結果.マスタデータ.U_進行管理.登録済みレコード数
                If 採点結果.マスタデータ.U_進行管理.リスト(u).競技番号 = 競技番号 And 採点結果.マスタデータ.U_進行管理.リスト(u).競技番号枝番 = 競技枝番 Then
                    If 採点結果.マスタデータ.U_進行管理.リスト(u).ステータス = "全審判送信済み" Then
                        完了種目番号 = 採点結果.マスタデータ.U_進行管理.リスト(u).種目順
                        完了ヒート番号 = 採点結果.マスタデータ.U_進行管理.リスト(u).ヒート番号
                    End If
                End If
            Next u

            Me.ステータス = 完了種目番号 & "-" & 完了ヒート番号

        End If





        Dim 審判G As Integer = ラウンドC.担当審判グループ

        '種目定義
        Dim 種目記号リスト() = Nothing
        Dim 種目数 As Integer = 採点結果.マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)
        ReDim Me.種目定義(種目数)
        If 種目数 > 0 Then
            For d = 1 To 種目数
                種目定義(d) = New SC_J_詳細結果_種目定義

                Dim 種目C As D_種目 = 採点結果.マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, d)

                種目定義(d).種目順 = d
                種目定義(d).種目記号 = 種目C.種目記号
                種目定義(d).種目名 = 採点結果.マスタデータ.Z_システム設定.Get_種目名称(種目C.種目記号).種目名_J
                種目定義(d).SG種別 = 種目C.SG種別
                種目定義(d).ヒート数 = 種目C.ヒート数
                種目定義(d).CaliMax = 種目C.CaliMax
                種目定義(d).CaliMin = 種目C.CaliMin

            Next d
        End If

        '出場選手
        Dim 背番号リスト() = Nothing
        Dim 出場選手数 As Integer = 採点結果.マスタデータ.E_ヒート表マスタ.Get_背番号リスト(1, 0, 背番号リスト)
        ReDim Me.出場選手(出場選手数)

        If 出場選手数 > 0 Then
            For s = 1 To 出場選手数
                Dim 選手C As 選手
                選手C = 採点結果.マスタデータ.選手マスタ.Get選手C_by背番号(選手リスト番号, 背番号リスト(s)）

                出場選手(s) = New SC_J_詳細結果_出場選手

                出場選手(s).背番号 = 背番号リスト(s)

                出場選手(s).L氏名 = 選手C.リーダー表記名
                出場選手(s).Lフリガナ = 選手C.リーダーフリガナ
                出場選手(s).L会員番号 = 選手C.リーダー会員番号
                出場選手(s).L所属 = 選手C.リーダー所属名

                出場選手(s).P氏名 = 選手C.パートナ表記名
                出場選手(s).Pフリガナ = 選手C.パートナフリガナ
                出場選手(s).P会員番号 = 選手C.パートナ会員番号
                出場選手(s).P所属 = 選手C.パートナ所属名

                出場選手(s).カップル所属 = 選手C.カップル所属名

            Next s
        End If

        'ジャッジ
        Dim ジャッジ記号リスト() = Nothing
        Dim ジャッジ人数 As Integer = 採点結果.マスタデータ.審判員マスタ.Get_審判員記号(審判G, ジャッジ記号リスト)
        ReDim Me.ジャッジ(ジャッジ人数)

        If ジャッジ人数 > 0 Then

            For j = 1 To ジャッジ人数
                Dim ジャッジC As 審判
                ジャッジC = 採点結果.マスタデータ.審判員マスタ.Get_審判Class(ジャッジ記号リスト(j))
                Me.ジャッジ(j) = New SC_J_詳細結果_ジャッジ

                Me.ジャッジ(j).記号 = ジャッジC.ジャッジ記号
                Me.ジャッジ(j).ジャッジ氏名 = ジャッジC.ジャッジ表記名
                Me.ジャッジ(j).ジャッジ会員番号 = ジャッジC.ジャッジ会員番号
                Me.ジャッジ(j).ジャッジフリガナ = ジャッジC.ジャッジフリガナ
                Me.ジャッジ(j).ジャッジ所属 = ジャッジC.ジャッジ所属
                Me.ジャッジ(j).Role = ジャッジC.審判チーム（審判G)

            Next j
        End If

        'PCS設定

        If 採点方式 = "AJS30J" Then

            Dim PCS数 = 採点結果.マスタデータ.J_新審判設定.GetPCS数()
            ReDim PCS設定(PCS数)

            For p = 1 To PCS数

                Me.PCS設定(p) = New SC_J_詳細結果_PCS設定

                PCS設定(p).PCS番号 = p
                PCS設定(p).PCS略称 = 採点結果.マスタデータ.J_新審判設定.PCS設定(p).PCS項目名
                PCS設定(p).倍率 = 採点結果.マスタデータ.J_新審判設定.PCS設定(p).倍率
            Next p
        End If


        '減点設定
        If 採点方式 = "AJS30J" Then

            Dim 減点項目数 = 採点結果.マスタデータ.J_新審判設定.Get減点項目数()
            ReDim 減点設定(減点項目数)

            For r = 1 To 減点項目数

                Me.減点設定(r) = New SC_J_詳細結果_減点設定

                減点設定(r).減点番号 = r
                減点設定(r).減点名称 = 採点結果.マスタデータ.J_新審判設定.減点設定(r).減点項目名
                減点設定(r).減点名称英語 = 採点結果.マスタデータ.J_新審判設定.減点設定(r).減点項目名英
                減点設定(r).適用対象 = 採点結果.マスタデータ.J_新審判設定.減点設定(r).SGM種別
            Next r
        End If



        '総合結果
        If 採点方式 = "AJS30J" Then

            Dim PCS数 = 採点結果.マスタデータ.J_新審判設定.GetPCS数()
            Dim 減点項目数 = 採点結果.マスタデータ.J_新審判設定.Get減点項目数()
            Dim ジャッジ人数WOレフリー As Integer = 採点結果.マスタデータ.審判員マスタ.Get_審判員数(審判G)

            'UP確認用
            Dim 次ラウンド As C_ラウンド = 採点結果.マスタデータ.C_ラウンドマスタ.Get_次ラウンドClass(区分番号, ラウンド番号)
            Dim 次ラウンド無し As Boolean = False
            Dim 次ラウンドマスタデータ = New マスタデータ

            If 次ラウンド IsNot Nothing Then
                'ヒート表の検索
                If 次ラウンドマスタデータ.E_ヒート表マスタ.FileCheck(区分番号, 次ラウンド.ラウンド番号) = False Then
                    次ラウンド無し = True
                Else
                    'ヒート表ファイルの読込み
                    次ラウンドマスタデータ.E_ヒート表マスタ.Read(区分番号, 次ラウンド.ラウンド番号)

                    '次ラウンドのヒート表が出来ているときは、UP数を上書き
                    Me.UP数 = 次ラウンドマスタデータ.E_ヒート表マスタ.登録済みレコード数

                End If
            Else
                次ラウンド無し = True
            End If



            If 出場選手数 > 0 Then

                ReDim Me.総合結果(出場選手数)

                For s = 1 To 出場選手数
                    Me.総合結果(s) = New SC_J_詳細結果_総合結果

                    Me.総合結果(s).背番号 = 採点結果.背番号(s)
                    Me.総合結果(s).順位番号 = 採点結果.総合順位番号(s)
                    Me.総合結果(s).順位表記 = 採点結果.総合順位表記(s)
                    Me.総合結果(s).総合得点 = 採点結果.総合得点(s)
                    Me.総合結果(s).UP = UP検索(採点結果.背番号(s), 次ラウンドマスタデータ)



                    ReDim Me.総合結果(s).種目結果(種目数)
                    For d = 1 To 種目数
                        Me.総合結果(s).種目結果(d) = New SC_J_詳細結果_総合結果.SC_J_詳細結果_総合結果_種目結果

                        Me.総合結果(s).種目結果(d).ヒート番号 = 採点結果.マスタデータ.E_ヒート表マスタ.Get_ヒート番号(d, 総合結果(s).背番号)
                        Me.総合結果(s).種目結果(d).種目得点 = 採点結果.種目(d).選手(s).種目得点
                        Me.総合結果(s).種目結果(d).種目順位番号 = 採点結果.種目(d).選手(s).種目順位番号
                        Me.総合結果(s).種目結果(d).種目順位表記 = 採点結果.種目(d).選手(s).種目順位表記

                        'PCS詳細

                        ReDim Me.総合結果(s).種目結果(d).PCS詳細(PCS数)
                        For p = 1 To PCS数
                            Me.総合結果(s).種目結果(d).PCS詳細(p) = New SC_J_詳細結果_総合結果.SC_J_詳細結果_総合結果_種目結果.SC_J_詳細結果_総合結果_種目結果_PCS詳細

                            Me.総合結果(s).種目結果(d).PCS詳細(p).PCS番号 = p
                            Me.総合結果(s).種目結果(d).PCS詳細(p).PCS得点 = 採点結果.種目(d).選手(s).種目各PCS得点(p)

                            ReDim Me.総合結果(s).種目結果(d).PCS詳細(p).ジャッジ得点(ジャッジ人数WOレフリー)  'レフリーを抜く
                            Dim JNO As Integer = 0
                            For j = 1 To 採点結果.種目(d).審判員数
                                If 採点結果.種目(d).選手(s).審判(j).ジャッジRole <> "R" And 採点結果.種目(d).選手(s).審判(j).ジャッジRole <> "" Then
                                    JNO = JNO + 1

                                    Me.総合結果(s).種目結果(d).PCS詳細(p).ジャッジ得点(JNO) = New SC_J_詳細結果_総合結果.SC_J_詳細結果_総合結果_種目結果.SC_J_詳細結果_総合結果_種目結果_PCS詳細.SC_J_詳細結果_総合結果_種目結果_PCS詳細_ジャッジ得点

                                    Me.総合結果(s).種目結果(d).PCS詳細(p).ジャッジ得点(JNO).ジャッジ記号 = 採点結果.種目(d).選手(s).審判(j).ジャッジ記号
                                    Me.総合結果(s).種目結果(d).PCS詳細(p).ジャッジ得点(JNO).対象 = 採点結果.種目(d).選手(s).審判(j).ジャッジPCS採点対象(p)
                                    Me.総合結果(s).種目結果(d).PCS詳細(p).ジャッジ得点(JNO).素点 = 採点結果.種目(d).選手(s).審判(j).PCS素点(p)
                                    Me.総合結果(s).種目結果(d).PCS詳細(p).ジャッジ得点(JNO).対象外FLAG = 採点結果.種目(d).選手(s).審判(j).PCS無効FLAG(p)
                                    Me.総合結果(s).種目結果(d).PCS詳細(p).ジャッジ得点(JNO).乖離度 = 採点結果.種目(d).選手(s).審判(j).PCS乖離度(p)

                                End If

                            Next j

                        Next p

                        '減点詳細
                        ReDim Me.総合結果(s).種目結果(d).減点詳細(減点項目数)

                        Dim 減点ジャッジ人数 As Integer = 0
                        For j = 1 To ジャッジ人数
                            If 採点結果.種目(d).選手(s).審判(j).ジャッジRole = "R" Then
                                減点ジャッジ人数 = 減点ジャッジ人数 + 1
                            End If
                        Next j

                        Dim 減点点数 As Decimal = 0
                        For r = 1 To 減点項目数
                            Me.総合結果(s).種目結果(d).減点詳細(r) = New SC_J_詳細結果_総合結果.SC_J_詳細結果_総合結果_種目結果.SC_J_詳細結果_総合結果_種目結果_減点詳細

                            Me.総合結果(s).種目結果(d).減点詳細(r).減点番号 = r
                            Me.総合結果(s).種目結果(d).減点詳細(r).減点点数 = 採点結果.種目(d).選手(s).種目各減点(r)

                            ReDim Me.総合結果(s).種目結果(d).減点詳細(r).ジャッジ得点(減点ジャッジ人数)  'レフリーのみ

                            Dim 減点JNO As Integer = 0
                            For j = 1 To 採点結果.種目(d).審判員数
                                If 採点結果.種目(d).選手(s).審判(j).ジャッジRole = "R" Then

                                    減点JNO = 減点JNO + 1

                                    Me.総合結果(s).種目結果(d).減点詳細(r).ジャッジ得点(減点JNO) = New SC_J_詳細結果_総合結果.SC_J_詳細結果_総合結果_種目結果.SC_J_詳細結果_総合結果_種目結果_減点詳細.SC_J_詳細結果_総合結果_種目結果_減点詳細_ジャッジ得点

                                    Me.総合結果(s).種目結果(d).減点詳細(r).ジャッジ得点(減点JNO).ジャッジ番号 = 採点結果.種目(d).選手(s).審判(j).ジャッジ記号
                                    Me.総合結果(s).種目結果(d).減点詳細(r).ジャッジ得点(減点JNO).素点 = 採点結果.種目(d).選手(s).審判(j).減点素点(r)

                                    減点点数 = 減点点数 + 採点結果.種目(d).選手(s).審判(j).減点素点(r)
                                End If


                            Next j


                        Next r


                        Me.総合結果(s).種目結果(d).減点合計 = 減点点数
                        Me.総合結果(s).種目結果(d).PCS合計 = Me.総合結果(s).種目結果(d).種目得点 + 減点点数


                    Next d

                Next s


            End If

        End If



    End Sub


    '背番号を渡すして、次ラウンドのヒート表に背番号がある時は"UP"を返す。無い時は"  "を返す
    '条件 ヒート表マスタのReadが終わっていること
    Private Function UP検索(背番号 As String, 次ラウンドマスタデータ As マスタデータ) As String
        Dim rc As String = ""

        For i = 1 To 次ラウンドマスタデータ.E_ヒート表マスタ.登録済みレコード数
            If CInt(次ラウンドマスタデータ.E_ヒート表マスタ.リスト(i).背番号) = CInt(背番号) Then
                rc = "UP"
                i = 次ラウンドマスタデータ.E_ヒート表マスタ.登録済みレコード数
            End If
        Next i

        Return rc
    End Function
End Class

