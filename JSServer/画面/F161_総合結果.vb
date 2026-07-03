Public Class F161_総合結果

    Private 総合区分番号 As String
    Private 総合ラウンド番号 As String

    Private 総合結果 As 総合結果_J

    Private Sub F161_総合結果_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Public Sub New(総合区分番号_ As String, 総合ラウンド番号_ As String）

        総合区分番号 = 総合区分番号_
        総合ラウンド番号 = 総合ラウンド番号_

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。


        総合結果 = New 総合結果_J
        総合結果 = 総合結果.JSON読み込み(総合区分番号, 総合ラウンド番号)

        結果更新（）


    End Sub


    Private Sub 結果更新()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        Me.LB_総合区分名.Text = 総合区分番号 & " " & マスタデータ.B_区分マスタ.Get区分表記名(総合区分番号) & "  集計結果"




        'データクリア
        Me.DGV_結果.DataSource = Nothing
        Me.DGV_結果.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_結果.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)


        '列タイトル設定
        ' １列目 順位
        ' ２列目 総合得点
        ' ３列目 背番号
        ' ４列目 リーダー名
        ' ５列目 リーダー名フリガナ
        ' ６列目 リーダー所属
        ' ７列目 パートナー名
        ' ８列目 パートナー名フリガナ
        ' ９列目 パートナー所属
        ' １０列目 カップル所属
        '11列目以降　区分毎の得点



        '// データテーブルの作成
        Dim tbl As New DataTable

        tbl.Columns.Add(New DataColumn("総合順位", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("総合得点", GetType(Decimal)))
        tbl.Columns.Add(New DataColumn("背番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("L氏名", GetType(String)))
        tbl.Columns.Add(New DataColumn("Lフリガナ", GetType(String)))
        tbl.Columns.Add(New DataColumn("L所属", GetType(String)))
        tbl.Columns.Add(New DataColumn("P氏名", GetType(String)))
        tbl.Columns.Add(New DataColumn("Pフリガナ", GetType(String)))
        tbl.Columns.Add(New DataColumn("P所属", GetType(String)))
        tbl.Columns.Add(New DataColumn("カップル所属", GetType(String)))


        For d = 1 To 総合結果.区分種目数
            tbl.Columns.Add(New DataColumn(総合結果.区分定義(d).種目記号, GetType(Decimal)))
        Next d

        'データセット

        Dim 区分 As B_区分 = マスタデータ.B_区分マスタ.Get区分C(総合結果.区分定義(1).区分番号)

        If 区分 Is Nothing Then
            MsgBox("区分番号:" & 総合結果.区分定義(1).区分番号 & "が定義されていません。")
            Exit Sub
        End If

        Dim リスト番号 As String = 区分.使用する選手マスタ
        If リスト番号 = "" Then
            MsgBox("区分番号:" & 総合結果.区分定義(1).区分番号 & "の選手マスタ番号が設定されていません。")
            Exit Sub
        End If


        For s = 1 To 総合結果.選手数


            tbl.Rows.Add()
            Dim idx = tbl.Rows.Count - 1
            tbl.Rows(idx).Item("総合順位") = 総合結果.選手結果(s).総合順位
            tbl.Rows(idx).Item("総合得点") = 総合結果.選手結果(s).総合得点
            tbl.Rows(idx).Item("背番号") = 総合結果.選手結果(s).背番号

            Dim 選手 As 選手
            選手 = マスタデータ.選手マスタ.Get選手C_by背番号(リスト番号, 総合結果.選手結果(s).背番号)
            tbl.Rows(idx).Item("L氏名") = 選手.リーダー表記名
            tbl.Rows(idx).Item("Lフリガナ") = 選手.リーダーフリガナ
            tbl.Rows(idx).Item("L所属") = 選手.リーダー所属名

            tbl.Rows(idx).Item("P氏名") = 選手.パートナ表記名
            tbl.Rows(idx).Item("Pフリガナ") = 選手.パートナフリガナ
            tbl.Rows(idx).Item("P所属") = 選手.パートナ所属名

            tbl.Rows(idx).Item("カップル所属") = 選手.カップル所属名



            For k = 1 To 総合結果.区分種目数
                tbl.Rows(idx).Item(総合結果.区分定義(k).種目記号) = 総合結果.選手結果(s).区分種目結果(k).区分得点
            Next k


        Next s



        '// DataGridViewにデータセットを設定
        Me.DGV_結果.DataSource = tbl






        '==列を非表示
        'Me.DGV_結果.Columns("区分名").Visible = False



        '===列幅の自動調整
        Me.DGV_結果.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_結果.AllowUserToResizeColumns = True

        '===行の高さの自動設定
        Me.DGV_結果.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders




        マスタデータ = Nothing

    End Sub

    Private Sub PB_CSV書き出し_Click(sender As Object, e As EventArgs) Handles PB_CSV書き出し.Click

    End Sub

    Private Sub PB_区分結果_Click(sender As Object, e As EventArgs) Handles PB_区分結果.Click
        Dim rc = 総合結果.CSV書き出し

        If rc = 0 Then
            MsgBox("CSV書き出し成功")
        Else
            MsgBox("CSV書き出し失敗！　書き出しができませんでした。")
        End If
    End Sub

    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click
        Me.Close()

    End Sub


End Class