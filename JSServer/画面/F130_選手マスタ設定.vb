Public Class F130_選手マスタ設定

    Private 更新FLAG As Boolean
    Private 更新FLAGリスト(20) As Boolean

    Private DGVリスト(20) As DataGridView


    Public Sub New()


        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。


    End Sub

    Private Sub F130_選手マスタ設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False


        タブ追加()
        選手マスタ読込()
    End Sub

    Private Sub タブ追加()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ


        '選手マスタの数を確定
        Dim リスト番号リスト() As String = Nothing
        Dim リスト番号数 As Integer = マスタデータ.選手マスタ.Get_リスト番号(リスト番号リスト)

        'ReDim DGVリスト(リスト番号数)

        'ReDim 更新FLAGリスト(リスト番号数)
        更新FLAG = False


        'タブページ DGVの追加
        For m = 1 To リスト番号数

            更新FLAGリスト(m) = False

            'タブページを追加

            Dim hTabPage As New System.Windows.Forms.TabPage()
            hTabPage.Name = "TP_" & CStr(m).PadLeft(2, "0")
            hTabPage.Text = リスト番号リスト(m)  'タブタイトル


            Me.TC_選手マスタ.TabPages.Add(hTabPage)

            Dim hDGV As New System.Windows.Forms.DataGridView

            hDGV.Dock = DockStyle.Fill
            hDGV.Name = "DGV_" & CStr(m).PadLeft(2, "0")
            hDGV.BackgroundColor = Color.White
            DGVリスト(m) = hDGV

            Me.TC_選手マスタ.TabPages(Me.TC_選手マスタ.TabCount - 1).Controls.Add(hDGV)


            'イベントハンドらの登録
            AddHandler DGVリスト(m).CurrentCellDirtyStateChanged, AddressOf Me.OnStateChange

            '===列幅の自動調整
            DGVリスト(m).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DGVリスト(m).AllowUserToResizeColumns = True

            'ヘッダー行の高さを設定
            DGVリスト(m).AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells

            'DGVのデフォルトフォント変更
            DGVリスト(m).DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)
            DGVリスト(m).DefaultCellStyle.NullValue = ""
            DGVリスト(m).DefaultCellStyle.DataSourceNullValue = ""


        Next m

        'タブコントロールのフォント設定
        Me.TC_選手マスタ.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Bold)

    End Sub


    Private Sub 選手マスタ読込()

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ


        'DGVの設定
        For d = 1 To UBound(DGVリスト)

            If DGVリスト(d) IsNot Nothing Then

                'クリア
                DGVリスト(d).DataSource = Nothing
                DGVリスト(d).Rows.Clear()


                '// データテーブルの作成
                Dim tbl As New DataTable

                tbl.Columns.Add(New DataColumn("背番号", GetType(String)))
                tbl.Columns.Add(New DataColumn("L氏名", GetType(String)))
                tbl.Columns.Add(New DataColumn("Lフリガナ", GetType(String)))

                tbl.Columns.Add(New DataColumn("P氏名", GetType(String)))
                tbl.Columns.Add(New DataColumn("Pフリガナ", GetType(String)))
                tbl.Columns.Add(New DataColumn("所属名", GetType(String)))

                tbl.Columns.Add(New DataColumn("L表記名", GetType(String)))
                tbl.Columns.Add(New DataColumn("L会員No", GetType(String)))
                tbl.Columns.Add(New DataColumn("L所属Code", GetType(String)))
                tbl.Columns.Add(New DataColumn("L所属名", GetType(String)))

                tbl.Columns.Add(New DataColumn("P表記名", GetType(String)))
                tbl.Columns.Add(New DataColumn("P会員No", GetType(String)))
                tbl.Columns.Add(New DataColumn("P所属Code", GetType(String)))
                tbl.Columns.Add(New DataColumn("P所属名", GetType(String)))


                '区分数
                For k = 1 To マスタデータ.B_区分マスタ.登録済みレコード数
                    tbl.Columns.Add(New DataColumn(CStr(マスタデータ.B_区分マスタ.リスト(k).区分番号).PadLeft(2, "0"), GetType(String)))
                Next k

                'データ追加
                Dim idx As Integer
                For i = 1 To マスタデータ.選手マスタ.登録済み選手数

                    'リスト番号が一致していたら
                    If マスタデータ.選手マスタ.選手リスト(i).List番号.PadLeft(2, "0") = CStr(d).PadLeft(2, "0") Then
                        tbl.Rows.Add()
                        idx = tbl.Rows.Count - 1
                        tbl.Rows(idx).Item("背番号") = マスタデータ.選手マスタ.選手リスト(i).背番号

                        tbl.Rows(idx).Item("L氏名") = マスタデータ.選手マスタ.選手リスト(i).リーダー氏名
                        tbl.Rows(idx).Item("L表記名") = マスタデータ.選手マスタ.選手リスト(i).リーダー表記名
                        tbl.Rows(idx).Item("Lフリガナ") = マスタデータ.選手マスタ.選手リスト(i).リーダーフリガナ
                        tbl.Rows(idx).Item("L会員No") = マスタデータ.選手マスタ.選手リスト(i).リーダー会員番号
                        tbl.Rows(idx).Item("L所属Code") = マスタデータ.選手マスタ.選手リスト(i).リーダーサークルコード
                        tbl.Rows(idx).Item("L所属名") = マスタデータ.選手マスタ.選手リスト(i).リーダー所属名

                        tbl.Rows(idx).Item("P氏名") = マスタデータ.選手マスタ.選手リスト(i).パートナ氏名
                        tbl.Rows(idx).Item("P表記名") = マスタデータ.選手マスタ.選手リスト(i).パートナ表記名
                        tbl.Rows(idx).Item("Pフリガナ") = マスタデータ.選手マスタ.選手リスト(i).パートナフリガナ
                        tbl.Rows(idx).Item("P会員No") = マスタデータ.選手マスタ.選手リスト(i).パートナ会員番号
                        tbl.Rows(idx).Item("P所属Code") = マスタデータ.選手マスタ.選手リスト(i).パートナサークルコード
                        tbl.Rows(idx).Item("P所属名") = マスタデータ.選手マスタ.選手リスト(i).パートナ所属名

                        tbl.Rows(idx).Item("所属名") = マスタデータ.選手マスタ.選手リスト(i).カップル所属名


                        For k = 1 To マスタデータ.B_区分マスタ.登録済みレコード数
                            tbl.Rows(idx).Item(CStr(マスタデータ.B_区分マスタ.リスト(k).区分番号).PadLeft(2, "0")) = マスタデータ.選手マスタ.選手リスト(i).エントリー区分(k)

                        Next k
                    End If
                Next i


                '// DataGridViewにデータセットを設定
                DGVリスト(d).DataSource = tbl


                'DGVのエントリー区分の色付け
                For k = 1 To マスタデータ.B_区分マスタ.登録済みレコード数

                    If マスタデータ.B_区分マスタ.Get区分C(CStr(マスタデータ.B_区分マスタ.リスト(k).区分番号).PadLeft(2, "0")) IsNot Nothing Then

                        If マスタデータ.B_区分マスタ.Get区分C(CStr(マスタデータ.B_区分マスタ.リスト(k).区分番号).PadLeft(2, "0")).使用する選手マスタ = CStr(d).PadLeft(2, "0") Then
                            '使用される場合
                            DGVリスト(d).Columns(CStr(マスタデータ.B_区分マスタ.リスト(k).区分番号).PadLeft(2, "0")).DefaultCellStyle.BackColor = Color.White

                        Else
                            '使用されない場合
                            DGVリスト(d).Columns(CStr(マスタデータ.B_区分マスタ.リスト(k).区分番号).PadLeft(2, "0")).DefaultCellStyle.BackColor = Color.Gray
                        End If
                    End If
                Next k


            End If


        Next d




    End Sub

    '保存ボタンを押した時の動作
    Private Sub PB_保存_Click(sender As Object, e As EventArgs) Handles PB_保存.Click

        Dim 表示テキスト As String = ""

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        For l = 1 To UBound(更新FLAGリスト)

            If 更新FLAGリスト(l) = True Then

                '選手マスタから該当のKey項目を一旦Delete
                マスタデータ.選手マスタ.Deleteレコード(CStr(l).PadLeft(2, "0"))

                Dim 選手C As 選手

                For i = 0 To Me.DGVリスト(l).RowCount - 1

                    If Me.DGVリスト(l).Rows(i).Cells("背番号").Value IsNot Nothing Then

                        選手C = New 選手

                        選手C.背番号 = Me.DGVリスト(l).Rows(i).Cells("背番号").Value

                        選手C.リーダー氏名 = Me.DGVリスト(l).Rows(i).Cells("L氏名").Value.ToString
                        選手C.リーダー表記名 = Me.DGVリスト(l).Rows(i).Cells("L表記名").Value.ToString
                        選手C.リーダーフリガナ = Me.DGVリスト(l).Rows(i).Cells("Lフリガナ").Value.ToString
                        選手C.リーダー会員番号 = Me.DGVリスト(l).Rows(i).Cells("L会員No").Value.ToString
                        選手C.リーダーサークルコード = Me.DGVリスト(l).Rows(i).Cells("L所属Code").Value.ToString
                        選手C.リーダー所属名 = Me.DGVリスト(l).Rows(i).Cells("L所属名").Value.ToString

                        選手C.パートナ氏名 = Me.DGVリスト(l).Rows(i).Cells("P氏名").Value.ToString
                        選手C.パートナ表記名 = Me.DGVリスト(l).Rows(i).Cells("P表記名").Value.ToString
                        選手C.パートナフリガナ = Me.DGVリスト(l).Rows(i).Cells("Pフリガナ").Value.ToString
                        選手C.パートナ会員番号 = Me.DGVリスト(l).Rows(i).Cells("P会員No").Value.ToString
                        選手C.パートナサークルコード = Me.DGVリスト(l).Rows(i).Cells("P所属Code").Value.ToString
                        選手C.パートナ所属名 = Me.DGVリスト(l).Rows(i).Cells("P所属名").Value.ToString

                        選手C.カップル所属名 = Me.DGVリスト(l).Rows(i).Cells("所属名").Value.ToString

                        For k = 1 To マスタデータ.B_区分マスタ.登録済みレコード数
                            選手C.エントリー区分(k) = Me.DGVリスト(l).Rows(i).Cells(CStr(マスタデータ.B_区分マスタ.リスト(k).区分番号).PadLeft(2, "0")).Value.ToString

                        Next k

                        マスタデータ.選手マスタ.選手登録(l, 選手C)
                    End If
                Next i

                表示テキスト = 表示テキスト & l & " "

                更新FLAGリスト(l) = False

            End If
        Next l


        マスタデータ = Nothing

        If 表示テキスト = "" Then
            MsgBox("保存するものはありませんでした。”)

        Else
            MsgBox("リスト番号 " & 表示テキスト & "を登録しました。”)

        End If

        選手マスタ読込()

        更新FLAG = False

    End Sub


    'セルが変更されたら
    Private Sub OnStateChange(ByVal sender As Object, ByVal e As EventArgs)

        'Dim dgv As DataGridView = sender

        'If Me.DGV_競技種目.CurrentCellAddress.X = 0 AndAlso Me.DGV_競技種目.IsCurrentCellDirty Then
        'コミットする
        'Me.DGV_競技種目.CommitEdit(DataGridViewDataErrorContexts.Commit)
        'End If

        更新FLAGリスト(CInt(Strings.Right(sender.name, 2))) = True

        更新FLAG = True

    End Sub


    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click

        Me.Close()

    End Sub

    'フォーム閉じるボタンが押された時
    Private Sub Me_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If 更新FLAG = True Then
            If MsgBox("保存せずに終了しても良いですか？", vbOKCancel) = vbOK Then
                更新FLAG = False
                Me.Close()
            Else
                '閉じるをキャンセル
                e.Cancel = True
            End If
        End If

    End Sub

    Private Sub PB_マスター追加_Click(sender As Object, e As EventArgs) Handles PB_マスター追加.Click

        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        'タブ数の取得
        Dim タブ数 As Integer = TC_選手マスタ.TabCount

        Dim m As Integer = タブ数 + 1

        更新FLAGリスト(m) = False

        'タブページを追加

        Dim hTabPage As New System.Windows.Forms.TabPage()
        hTabPage.Name = "TP_" & CStr(m).PadLeft(2, "0")
        hTabPage.Text = CStr(m).PadLeft(2, "0") 'タブタイトル


        Me.TC_選手マスタ.TabPages.Add(hTabPage)

        Dim hDGV As New System.Windows.Forms.DataGridView

        hDGV.Dock = DockStyle.Fill
        hDGV.Name = "DGV_" & CStr(m).PadLeft(2, "0")
        hDGV.BackgroundColor = Color.White
        DGVリスト(m) = hDGV

        Me.TC_選手マスタ.TabPages(Me.TC_選手マスタ.TabCount - 1).Controls.Add(hDGV)


        'イベントハンドらの登録
        AddHandler DGVリスト(m).CurrentCellDirtyStateChanged, AddressOf Me.OnStateChange

        '===列幅の自動調整
        DGVリスト(m).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DGVリスト(m).AllowUserToResizeColumns = True

        'ヘッダー行の高さを設定
        DGVリスト(m).AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells


        'DGVのデフォルトフォント変更
        DGVリスト(m).DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 11, FontStyle.Regular)
        DGVリスト(m).DefaultCellStyle.NullValue = ""
        DGVリスト(m).DefaultCellStyle.DataSourceNullValue = ""


        'DGVの追加


        'DGVの設定

        'クリア
        DGVリスト(m).DataSource = Nothing
        DGVリスト(m).Rows.Clear()

        '// データテーブルの作成
        Dim tbl As New DataTable
        tbl.Columns.Add(New DataColumn("背番号", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("L氏名", GetType(String)))
        tbl.Columns.Add(New DataColumn("Lフリガナ", GetType(String)))

        tbl.Columns.Add(New DataColumn("P氏名", GetType(String)))
        tbl.Columns.Add(New DataColumn("Pフリガナ", GetType(String)))
        tbl.Columns.Add(New DataColumn("所属名", GetType(String)))

        tbl.Columns.Add(New DataColumn("L表記名", GetType(String)))
        tbl.Columns.Add(New DataColumn("L会員No", GetType(String)))
        tbl.Columns.Add(New DataColumn("L所属Code", GetType(String)))
        tbl.Columns.Add(New DataColumn("L所属名", GetType(String)))

        tbl.Columns.Add(New DataColumn("P表記名", GetType(String)))
        tbl.Columns.Add(New DataColumn("P会員No", GetType(String)))
        tbl.Columns.Add(New DataColumn("P所属Code", GetType(String)))
        tbl.Columns.Add(New DataColumn("P所属名", GetType(String)))


        '区分数
        For k = 1 To マスタデータ.B_区分マスタ.登録済みレコード数
            tbl.Columns.Add(New DataColumn(CStr(マスタデータ.B_区分マスタ.リスト(k).区分番号).PadLeft(2, "0"), GetType(String)))
        Next k

        'データ追加


        '// DataGridViewにデータセットを設定
        DGVリスト(m).DataSource = tbl

        マスタデータ = Nothing

    End Sub
End Class