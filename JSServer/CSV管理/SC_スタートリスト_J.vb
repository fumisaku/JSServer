Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class SC_スタートリスト_J


    '結果表示Display に送るデータ
    Implements System.ICloneable

    Public Property T01_競技番号 As String
    Public Property T02_競技番号枝番 As String

    Public Property T03_区分番号 As String

    Public Property T04_ラウンド番号 As String

    Public Property T05_リアルタイムFLAG As String
    Public Property T06_ステータス As String
    Public Property T07_開始予定 As String
    Public Property T08_終了予定 As String
    Public Property T09_開始実績 As String
    Public Property T10_終了実績 As String

    '====B 区分
    Public Property B03_区分名 As String
    Public Property B04_区分表記名 As String
    Public Property B05_カテゴリ As String
    Public Property B06_担当審判グループ As String
    Public Property B07_使用する選手マスタ As String

    '==== C ラウンド
    Public Property C10_ラウンド名 As String
    Public Property C11_ラウンド名E As String

    Public Property C02_採点方式 As String
    Public Property C03_担当審判グループ As String
    Public Property C04_ヒート数 As String
    Public Property C05_UP予定数 As String
    Public Property C06_キャリブレーション最高 As String
    Public Property C07_キャリブレーション最低 As String
    Public Property C08_リアルタイムFLAG As String
    Public Property C09_ヒート割方式 As String


    Public D00_種目() As SC_スタートリスト_種目_J

    Public J00_ジャッジ() As SC_スタートリスト_ジャッジ


    Public AJ01_PCS設定() As SC_スタートリスト_AJ01_PCS設定
    Public AJ02_減点設定() As SC_スタートリスト_AJ02_減点設定
    Public AJ03_課題S設定() As SC_スタートリスト_AJ03_課題設定
    Public AJ04_TES減点設定() As SC_スタートリスト_AJ04_TES減点設定



    Public Sub データセット(マスタデータ As マスタデータ, 区分番号 As String, ラウンド番号 As String)

        Dim T進行 As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

        T01_競技番号 = T進行.競技番号
        T02_競技番号枝番 = T進行.競技番号枝番
        T03_区分番号 = T進行.区分番号
        T04_ラウンド番号 = T進行.ラウンド番号
        T05_リアルタイムFLAG = T進行.リアルタイムFLAG
        T06_ステータス = T進行.ステータス
        T07_開始予定 = T進行.開始予定
        T08_終了予定 = T進行.終了予定
        T09_開始実績 = T進行.開始実績
        T10_終了実績 = T進行.終了実績

        Dim 区分 As B_区分 = マスタデータ.B_区分マスタ.Get区分C(T03_区分番号)

        B03_区分名 = 区分.区分名
        B04_区分表記名 = 区分.区分表記名
        B05_カテゴリ = 区分.カテゴリ
        B06_担当審判グループ = 区分.担当審判グループ
        B07_使用する選手マスタ = 区分.使用する選手マスタ

        Dim ラウンド As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(T03_区分番号, T04_ラウンド番号)

        C10_ラウンド名 = マスタデータ.Get_ラウンド名(T04_ラウンド番号)
        C11_ラウンド名E = マスタデータ.Get_ラウンド名_E(T04_ラウンド番号)

        C02_採点方式 = ラウンド.採点方式
        C03_担当審判グループ = ラウンド.担当審判グループ
        C04_ヒート数 = ラウンド.ヒート数
        C05_UP予定数 = ラウンド.UP予定数
        C06_キャリブレーション最高 = ラウンド.CaliMax
        C07_キャリブレーション最低 = ラウンド.CaliMin
        C08_リアルタイムFLAG = ラウンド.リアルタイムFLAG
        C09_ヒート割方式 = ラウンド.ヒート割方式


        Dim ソロ種目記号 As String = ""  '課題フィガー用

        Dim D00 As Integer = 0
        For D = 1 To 10

            マスタデータ.D_種目マスタ.FileRead()
            Dim 種目 As D_種目 = マスタデータ.D_種目マスタ.Get_種目Class(T03_区分番号, T04_ラウンド番号, D)

            If 種目 IsNot Nothing Then
                D00 = D00 + 1

                ReDim Preserve D00_種目(D00)
                D00_種目(D00) = New SC_スタートリスト_種目_J

                D00_種目(D00).D01_種目順 = 種目.種目順
                D00_種目(D00).D02_種目記号 = 種目.種目記号
                D00_種目(D00).D02_種目名J = マスタデータ.Z_システム設定.Get_種目名称(D00_種目(D00).D02_種目記号).種目名_J
                D00_種目(D00).D02_種目名E = マスタデータ.Z_システム設定.Get_種目名称(D00_種目(D00).D02_種目記号).種目名_E
                D00_種目(D00).D03_ソログループ種別 = 種目.SG種別
                D00_種目(D00).D04_ヒート数 = 種目.ヒート数   　　　　　　　'要修正
                D00_種目(D00).D05_担当審判グループ = 種目.担当審判グループ
                D00_種目(D00).D06_キャリブレーション最高 = 種目.CaliMax
                D00_種目(D00).D07_キャリブレーション最低 = 種目.CaliMin


                If 種目.SG種別 = "S" Then
                    ソロ種目記号 = 種目.種目記号
                End If

                If マスタデータ.E_ヒート表マスタ.FileCheck(T03_区分番号, T04_ラウンド番号) = True Then

                    マスタデータ.E_ヒート表マスタ.Read(T03_区分番号, T04_ラウンド番号)

                    'ヒート表がある時はヒート数をヒート表の数で上書き。こちらが正しいので
                    D00_種目(D00).D04_ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(D00_種目(D00).D01_種目順)

                    For h = 1 To D00_種目(D00).D04_ヒート数

                        ReDim Preserve D00_種目(D00).E00_ヒート(h)
                        D00_種目(D00).E00_ヒート(h) = New SC_スタートリスト_種目_ヒート_J

                        Dim 背番号リスト() = Nothing
                        マスタデータ.E_ヒート表マスタ.Get_背番号リスト(D00_種目(D00).D01_種目順, h, 背番号リスト)


                        D00_種目(D00).E00_ヒート(h).E01_ヒート番号 = h


                        For s = 1 To UBound(背番号リスト)

                            If 背番号リスト(s) <> "" Then

                                ReDim Preserve D00_種目(D00).E00_ヒート(h).S00_選手(s)
                                D00_種目(D00).E00_ヒート(h).S00_選手(s) = New SC_スタートリスト_種目_ヒート_選手_J

                                Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(B07_使用する選手マスタ, 背番号リスト(s))

                                D00_種目(D00).E00_ヒート(h).S00_選手(s).S01_ヒート番号 = h
                                D00_種目(D00).E00_ヒート(h).S00_選手(s).S02_背番号 = 選手.背番号

                                D00_種目(D00).E00_ヒート(h).S00_選手(s).S03_L氏名 = 選手.リーダー氏名
                                D00_種目(D00).E00_ヒート(h).S00_選手(s).S04_Lフリガナ = 選手.リーダーフリガナ
                                D00_種目(D00).E00_ヒート(h).S00_選手(s).S05_L表記名 = 選手.リーダー表記名
                                D00_種目(D00).E00_ヒート(h).S00_選手(s).S06_L所属名 = 選手.リーダー所属名

                                D00_種目(D00).E00_ヒート(h).S00_選手(s).S07_P氏名 = 選手.パートナ氏名
                                D00_種目(D00).E00_ヒート(h).S00_選手(s).S08_Pフリガナ = 選手.パートナフリガナ
                                D00_種目(D00).E00_ヒート(h).S00_選手(s).S09_P表記名 = 選手.パートナ表記名
                                D00_種目(D00).E00_ヒート(h).S00_選手(s).S10_P所属名 = 選手.パートナ所属名

                                D00_種目(D00).E00_ヒート(h).S00_選手(s).S11_所属名 = 選手.カップル所属名

                            End If
                        Next s

                        Dim 進行 As U_進行 = マスタデータ.U_進行管理.Get_進行(T01_競技番号, T02_競技番号枝番, D00_種目(D00).D01_種目順, h)

                        D00_種目(D00).E00_ヒート(h).U03_ステータス = 進行.ステータス
                        D00_種目(D00).E00_ヒート(h).U04_採点終了時刻 = 進行.採点終了時刻

                    Next h

                End If

            End If

        Next D

        Dim 審判記号リスト() = Nothing
        マスタデータ.審判員マスタ.Get_審判員記号(B06_担当審判グループ, 審判記号リスト)

        Dim J00 As Integer = 0
        For j = 1 To UBound(審判記号リスト)

            Dim 審判 As 審判 = マスタデータ.審判員マスタ.Get_審判Class(審判記号リスト(j))

            ReDim Preserve J00_ジャッジ(j)
            J00_ジャッジ(j) = New SC_スタートリスト_ジャッジ

            J00_ジャッジ(j).J01_ジャッジ記号 = 審判.ジャッジ記号
            J00_ジャッジ(j).J02_ジャッジ氏名 = 審判.ジャッジ表記名
            J00_ジャッジ(j).J03_ジャッジフリガナ = 審判.ジャッジフリガナ
            J00_ジャッジ(j).J04_ジャッジ所属 = 審判.ジャッジ所属
            J00_ジャッジ(j).J05_ジャッジ言語 = 審判.言語
            J00_ジャッジ(j).J06_ジャッジRole = 審判.審判チーム(B06_担当審判グループ)

        Next j


        '=====AJS設定

        マスタデータ.J_新審判設定.Set_新審判基準VER(C02_採点方式)

        For p = 1 To マスタデータ.J_新審判設定.GetPCS数
            ReDim Preserve AJ01_PCS設定(p)
            AJ01_PCS設定(p) = New SC_スタートリスト_AJ01_PCS設定

            AJ01_PCS設定(p).P01_PCS番号 = p
            AJ01_PCS設定(p).P02_PCS略称 = マスタデータ.J_新審判設定.PCS設定(p).PCS項目名

        Next

        For r = 1 To マスタデータ.J_新審判設定.Get減点項目数
            ReDim Preserve AJ02_減点設定(r)
            AJ02_減点設定(r) = New SC_スタートリスト_AJ02_減点設定

            AJ02_減点設定(r).R01_減点番号 = r
            AJ02_減点設定(r).R02_減点略称 = マスタデータ.J_新審判設定.減点設定(r).減点項目名
            AJ02_減点設定(r).R03_減点略称E = マスタデータ.J_新審判設定.減点設定(r).減点項目名英

        Next r

        For k = 1 To マスタデータ.J_新審判設定.Get課題数(ソロ種目記号)
            ReDim Preserve AJ03_課題S設定(k)
            AJ03_課題S設定(k) = New SC_スタートリスト_AJ03_課題設定

            AJ03_課題S設定(k).K01_課題番号 = k
            AJ03_課題S設定(k).K02_フィガー名 = マスタデータ.J_新審判設定.Getフィガー名(ソロ種目記号, k)

        Next k

        For t = 1 To マスタデータ.J_新審判設定.GetTES減点数
            ReDim Preserve AJ04_TES減点設定(t)
            AJ04_TES減点設定(t) = New SC_スタートリスト_AJ04_TES減点設定

            AJ04_TES減点設定(t).T01_減点番号 = t
            AJ04_TES減点設定(t).T02_減点略称 = マスタデータ.J_新審判設定.TES減点設定(t).減点略称


        Next t


    End Sub



    Public Sub JSON書き出し(ByVal filepath As String)


        Dim filename As String = "SC_StartList_" & T03_区分番号 & "_" & T04_ラウンド番号 & ".json"


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



    Public Function JSON読み込み(ByVal filepath As String) As SC_スタートリスト_J


        Dim rc As SC_スタートリスト_J = Nothing

        Dim filename As String = "SC_StartList_" & T03_区分番号 & "_" & T04_ラウンド番号 & ".json"


        ''JSON読み込み用
        Dim jText As String = String.Empty


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した


            ''ファイルからJSONを読み込む
            Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)

            jText = cReader.ReadToEnd

            rc = JsonConvert.DeserializeObject(Of SC_スタートリスト_J)(jText)

            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()

        End If

        Return rc


    End Function




    Public Class SC_スタートリスト_種目_J

        Public Property D01_種目順 As Integer
        Public Property D02_種目記号 As String

        Public Property D02_種目名J As String
        Public Property D02_種目名E As String


        Public Property D03_ソログループ種別 As String
        Public Property D04_ヒート数 As Integer
        Public Property D05_担当審判グループ As String
        Public Property D06_キャリブレーション最高 As Decimal
        Public Property D07_キャリブレーション最低 As Decimal

        Public E00_ヒート() As SC_スタートリスト_種目_ヒート_J

    End Class

    Public Class SC_スタートリスト_種目_ヒート_J

        Public Property E01_ヒート番号 As Integer

        Public Property U03_ステータス As String
        Public Property U04_採点終了時刻 As String

        Public S00_選手() As SC_スタートリスト_種目_ヒート_選手_J

    End Class

    Public Class SC_スタートリスト_種目_ヒート_選手_J

        Public Property S01_ヒート番号 As Integer
        Public Property S02_背番号 As String
        Public Property S03_L氏名 As String
        Public Property S04_Lフリガナ As String
        Public Property S05_L表記名 As String
        Public Property S06_L所属名 As String
        Public Property S07_P氏名 As String
        Public Property S08_Pフリガナ As String
        Public Property S09_P表記名 As String
        Public Property S10_P所属名 As String
        Public Property S11_所属名 As String
    End Class

    Public Class SC_スタートリスト_ジャッジ

        Public Property J01_ジャッジ記号 As String
        Public Property J02_ジャッジ氏名 As String
        Public Property J03_ジャッジフリガナ As String
        Public Property J04_ジャッジ所属 As String
        Public Property J05_ジャッジ言語 As String
        Public Property J06_ジャッジRole As String



    End Class


    Public Class SC_スタートリスト_AJ01_PCS設定

        Public Property P01_PCS番号 As Integer
        Public Property P02_PCS略称 As String

    End Class

    Public Class SC_スタートリスト_AJ02_減点設定

        Public Property R01_減点番号 As Integer
        Public Property R02_減点略称 As String
        Public Property R03_減点略称E As String

    End Class

    Public Class SC_スタートリスト_AJ03_課題設定
        Public Property K01_課題番号 As Integer
        Public Property K02_フィガー名 As String

    End Class


    Public Class SC_スタートリスト_AJ04_TES減点設定
        Public Property T01_減点番号 As Integer
        Public Property T02_減点略称 As String

    End Class


    Public Function Clone() As Object Implements ICloneable.Clone
        Throw New NotImplementedException()
    End Function

End Class
