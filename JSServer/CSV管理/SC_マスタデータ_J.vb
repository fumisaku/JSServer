Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class SC_マスタデータ_J

    '結果表示Display に送るデータ


    Implements System.ICloneable

    Public Property A01_公認競技会NO As String
    Public Property A02_競技会名 As String
    Public Property A03_開催日 As String
    Public Property A04_主催団体 As String
    Public Property A05_開催場所 As String

    Public T00_採点進行管理() As SC_マスタ_採点進行管理_J


    Public Sub データセット(マスタデータ As マスタデータ)

        A01_公認競技会NO = マスタデータ.A_競技会マスタ.公認競技会NO
        A02_競技会名 = マスタデータ.A_競技会マスタ.競技会名
        A03_開催日 = マスタデータ.A_競技会マスタ.開催日
        A04_主催団体 = マスタデータ.A_競技会マスタ.主催団体
        A05_開催場所 = マスタデータ.A_競技会マスタ.開催場所


        マスタデータ.T_採点進行管理.FileRead()

        For T00 = 1 To UBound(マスタデータ.T_採点進行管理.リスト)

            If マスタデータ.T_採点進行管理.リスト(T00) IsNot Nothing Then


                If マスタデータ.T_採点進行管理.リスト(T00).競技番号 <> "" Then

                    ReDim Preserve T00_採点進行管理(T00)
                    T00_採点進行管理(T00) = New SC_マスタ_採点進行管理_J

                    T00_採点進行管理(T00).T01_競技番号 = マスタデータ.T_採点進行管理.リスト(T00).競技番号
                    T00_採点進行管理(T00).T02_競技番号枝番 = マスタデータ.T_採点進行管理.リスト(T00).競技番号枝番
                    T00_採点進行管理(T00).T03_区分番号 = マスタデータ.T_採点進行管理.リスト(T00).区分番号
                    T00_採点進行管理(T00).T04_ラウンド番号 = マスタデータ.T_採点進行管理.リスト(T00).ラウンド番号
                    T00_採点進行管理(T00).T05_リアルタイムFLAG = マスタデータ.T_採点進行管理.リスト(T00).リアルタイムFLAG
                    T00_採点進行管理(T00).T06_ステータス = マスタデータ.T_採点進行管理.リスト(T00).ステータス
                    T00_採点進行管理(T00).T07_開始予定 = マスタデータ.T_採点進行管理.リスト(T00).開始予定
                    T00_採点進行管理(T00).T08_終了予定 = マスタデータ.T_採点進行管理.リスト(T00).終了予定
                    T00_採点進行管理(T00).T09_開始実績 = マスタデータ.T_採点進行管理.リスト(T00).開始実績
                    T00_採点進行管理(T00).T10_終了実績 = マスタデータ.T_採点進行管理.リスト(T00).終了実績

                    Dim 区分 As B_区分 = マスタデータ.B_区分マスタ.Get区分C(T00_採点進行管理(T00).T03_区分番号)
                    T00_採点進行管理(T00).B01_区分番号 = 区分.区分番号
                    T00_採点進行管理(T00).B02_区分記号 = 区分.区分記号
                    T00_採点進行管理(T00).B03_区分名 = 区分.区分名
                    T00_採点進行管理(T00).B04_区分表記名 = 区分.区分表記名
                    T00_採点進行管理(T00).B05_カテゴリ = 区分.カテゴリ
                    T00_採点進行管理(T00).B06_担当審判グループ = 区分.担当審判グループ
                    T00_採点進行管理(T00).B07_使用する選手マスタ = 区分.使用する選手マスタ

                    Dim ラウンド As C_ラウンド = マスタデータ.C_ラウンドマスタ.GetラウンドClass(T00_採点進行管理(T00).T03_区分番号, T00_採点進行管理(T00).T04_ラウンド番号)
                    T00_採点進行管理(T00).C01_ラウンド番号 = ラウンド.ラウンド番号
                    T00_採点進行管理(T00).C01_ラウンド名 = マスタデータ.Get_ラウンド名(T00_採点進行管理(T00).C01_ラウンド番号)
                    T00_採点進行管理(T00).C02_採点方式 = ラウンド.採点方式
                    T00_採点進行管理(T00).C03_担当審判グループ = ラウンド.担当審判グループ
                    T00_採点進行管理(T00).C04_ヒート数 = ラウンド.ヒート数
                    T00_採点進行管理(T00).C05_UP予定数 = ラウンド.UP予定数
                    T00_採点進行管理(T00).C06_キャリブレーション最高 = ラウンド.CaliMax
                    T00_採点進行管理(T00).C07_キャリブレーション最低 = ラウンド.CaliMin
                    T00_採点進行管理(T00).C08_リアルタイムFLAG = ラウンド.リアルタイムFLAG
                    T00_採点進行管理(T00).C09_ヒート割方式 = ラウンド.ヒート割方式



                    '該当競技番号、競技枝番の　U進行を探す

                    Dim U00 As Integer = 0

                    マスタデータ.U_進行管理.FileRead()

                    For U = 1 To UBound(マスタデータ.U_進行管理.リスト)

                        If マスタデータ.U_進行管理.リスト(U) IsNot Nothing Then


                            If T00_採点進行管理(T00).T01_競技番号 = マスタデータ.U_進行管理.リスト(U).競技番号 And
                       T00_採点進行管理(T00).T02_競技番号枝番 = マスタデータ.U_進行管理.リスト(U).競技番号枝番 Then


                                U00 = U00 + 1

                                ReDim Preserve T00_採点進行管理(T00).U00_進行管理(U00)
                                T00_採点進行管理(T00).U00_進行管理(U00) = New SC_マスタ_採点進行管理_進行管理_J

                                T00_採点進行管理(T00).U00_進行管理(U00).U01_種目順 = マスタデータ.U_進行管理.リスト(U).種目順
                                T00_採点進行管理(T00).U00_進行管理(U00).U02_ヒート番号 = マスタデータ.U_進行管理.リスト(U).ヒート番号
                                T00_採点進行管理(T00).U00_進行管理(U00).U03_ステータス = マスタデータ.U_進行管理.リスト(U).ステータス
                                T00_採点進行管理(T00).U00_進行管理(U00).U04_採点終了時刻 = マスタデータ.U_進行管理.リスト(U).採点終了時刻

                                Dim 種目 As D_種目 = マスタデータ.D_種目マスタ.Get_種目Class(T00_採点進行管理(T00).B01_区分番号,
                                                                                         T00_採点進行管理(T00).C01_ラウンド番号,
                                                                                         T00_採点進行管理(T00).U00_進行管理(U00).U01_種目順)

                                T00_採点進行管理(T00).U00_進行管理(U00).D01_種目順 = 種目.種目順
                                T00_採点進行管理(T00).U00_進行管理(U00).D02_種目記号 = 種目.種目記号
                                T00_採点進行管理(T00).U00_進行管理(U00).D03_ソログループ種別 = 種目.SG種別
                                T00_採点進行管理(T00).U00_進行管理(U00).D04_ヒート数 = 種目.ヒート数
                                T00_採点進行管理(T00).U00_進行管理(U00).D05_担当審判グループ = 種目.担当審判グループ
                                T00_採点進行管理(T00).U00_進行管理(U00).D06_キャリブレーション最高 = 種目.CaliMax
                                T00_採点進行管理(T00).U00_進行管理(U00).D07_キャリブレーション最低 = 種目.CaliMin

                            End If
                        End If

                    Next U

                End If
            End If


        Next T00


    End Sub




    Public Sub JSON書き出し(ByVal filepath As String)


        Dim filename As String = "SC_Master.json"


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



    Public Function JSON読み込み(ByVal filepath As String) As SC_マスタデータ_J


        Dim rc As SC_マスタデータ_J = Nothing

        Dim filename As String = "SC_Master.json"


        ''JSON読み込み用
        Dim jText As String = String.Empty


        If Dir(filepath & "\" & filename).ToUpper <> filename.ToUpper Then
            'ファイルが存在しない


        Else
            'ファイルが存在した


            ''ファイルからJSONを読み込む
            Dim cReader As New System.IO.StreamReader(filepath & "\" & filename, System.Text.Encoding.Default)

            jText = cReader.ReadToEnd

            rc = JsonConvert.DeserializeObject(Of SC_マスタデータ_J)(jText)

            ' cReader を閉じる (正しくは オブジェクトの破棄を保証する を参照)
            cReader.Close()

        End If

        Return rc


    End Function



    Public Class SC_マスタ_採点進行管理_J

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


        Public Property B01_区分番号 As String
        Public Property B02_区分記号 As String
        Public Property B03_区分名 As String
        Public Property B04_区分表記名 As String
        Public Property B05_カテゴリ As String
        Public Property B06_担当審判グループ As Integer
        Public Property B07_使用する選手マスタ As String


        Public Property C01_ラウンド番号 As String
        Public Property C01_ラウンド名 As String

        Public Property C02_採点方式 As String
        Public Property C03_担当審判グループ As Integer
        Public Property C04_ヒート数 As Integer
        Public Property C05_UP予定数 As Integer
        Public Property C06_キャリブレーション最高 As Decimal
        Public Property C07_キャリブレーション最低 As Decimal
        Public Property C08_リアルタイムFLAG As String
        Public Property C09_ヒート割方式 As String
        Public Property D00_種目 As String

        Public U00_進行管理() As SC_マスタ_採点進行管理_進行管理_J

    End Class

    Public Class SC_マスタ_採点進行管理_進行管理_J

        Public Property U01_種目順 As Integer
        Public Property U02_ヒート番号 As Integer
        Public Property U03_ステータス As String
        Public Property U04_採点終了時刻 As String


        Public Property D01_種目順 As Integer
        Public Property D02_種目記号 As String
        Public Property D03_ソログループ種別 As String
        Public Property D04_ヒート数 As Integer
        Public Property D05_担当審判グループ As Integer
        Public Property D06_キャリブレーション最高 As Decimal
        Public Property D07_キャリブレーション最低 As Decimal


    End Class



    Public Function Clone() As Object Implements ICloneable.Clone
        Return Me.MemberwiseClone()
    End Function


End Class
