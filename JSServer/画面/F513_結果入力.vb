Public Class F513_結果入力

    Private マスタデータ As マスタデータ
    Private 区分番号, ラウンド番号 As String


    Private Sub F513_結果入力_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

        Me.TopMost = True
        Me.TopMost = False

    End Sub

    Public Sub 設定(区分番号_ As String, ラウンド番号_ As String)


        マスタデータ = New マスタデータ

        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_

        項目設定()
        DGV設定()

    End Sub

    Private Sub 項目設定()

        '区分名
        'ラベルの設定
        Me.LB_区分名.Text = マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & " " & マスタデータ.Get_ラウンド名(ラウンド番号)

        Me.LB_採点方式.Text = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

    End Sub

    Private Sub DGV設定()


        'データクリア
        Me.DGV_結果.DataSource = Nothing
        Me.DGV_結果.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_結果.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_結果.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)

        'DGVのデフォルト配置を真ん中にする
        Me.DGV_結果.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.DGV_結果.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        '// データテーブルの作成
        Dim tbl As New DataTable
        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("背番号", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("名前", GetType(String)))
        tbl.Columns.Add(New DataColumn("順位", GetType(String)))



        Dim 選手マスタLIST番号 As String = マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ

        Dim 行番号 As Integer = 0

        マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
        Dim 出場選手数 As Integer = 0
        出場選手数 = マスタデータ.E_ヒート表マスタ.登録済みレコード数

        For i = 1 To 出場選手数
            tbl.Rows.Add()
            tbl.Rows(行番号).Item("No") = 行番号 + 1

            tbl.Rows(行番号).Item("背番号") = マスタデータ.E_ヒート表マスタ.リスト(i).背番号
            tbl.Rows(行番号).Item("名前") = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, tbl.Rows(行番号).Item("背番号")).リーダー表記名

            行番号 = 行番号 + 1
        Next i


        '// DataGridViewにデータセットを設定
        Me.DGV_結果.DataSource = tbl


        '===列幅の自動調整
        'Me.DGV_ヒート表.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_結果.RowHeadersWidth = 25
        Me.DGV_結果.Columns("No").Width = 50
        Me.DGV_結果.Columns("背番号").Width = 80
        Me.DGV_結果.Columns("名前").Width = 140
        Me.DGV_結果.Columns("名前").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft


        Me.DGV_結果.Columns("順位").Width = 73


        Me.DGV_結果.AllowUserToResizeColumns = True

    End Sub


    Private Sub PB_確定_Click(sender As Object, e As EventArgs) Handles PB_確定.Click


        '勝者・敗者を確定
        Dim 勝者背番号 As String = "0"
        Dim 敗者背番号 As String = "0"

        For i = 0 To DGV_結果.RowCount - 1
            If DGV_結果.Rows(i).Cells("順位").Value Is Nothing Or
               DGV_結果.Rows(i).Cells("順位").Value Is DBNull.Value Then

                MsgBox(i + 1 & "番目の順位を入力してください。")
                Exit Sub

            Else

                If DGV_結果.Rows(i).Cells("順位").Value = "1" Then

                    勝者背番号 = DGV_結果.Rows(i).Cells("背番号").Value

                ElseIf DGV_結果.Rows(i).Cells("順位").Value = "2" Then

                    敗者背番号 = DGV_結果.Rows(i).Cells("背番号").Value

                End If

            End If
        Next i




        Dim 採点進行C As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)

        採点進行C.ステータス = "採点済み"
        マスタデータ.T_採点進行管理.登録(採点進行C)

        MsgBox("勝者は " & 勝者背番号 & " 番。ステータスを「採点済み」に変更しました")

        '次のヒート票に登録  勝った方
        Dim 次勝ラウンド番号 As String = ""
        Dim 次勝区分番号 = マスタデータ.BR_グループマスタ.Get勝者区分ラウンド(区分番号, ラウンド番号, 次勝ラウンド番号)

        If 次勝区分番号 <> "" And 次勝ラウンド番号 <> "" Then
            Dim ヒートC As E_ヒート表
            ヒートC = New E_ヒート表

            ヒートC.背番号 = 勝者背番号

            Dim 種目記号リスト() = Nothing
            Dim 次種目数 = マスタデータ.D_種目マスタ.Get_種目数(次勝区分番号, 次勝ラウンド番号, 種目記号リスト)

            For d = 1 To 次種目数
                ヒートC.ヒート番号(d) = 1
            Next d

            マスタデータ.E_ヒート表マスタ.Read(次勝区分番号, 次勝ラウンド番号)
            マスタデータ.E_ヒート表マスタ.登録(ヒートC, 次勝区分番号, 次勝ラウンド番号)

            'PCS担当表を作成

            '古いPCS表を消す
            マスタデータ.F_審判担当PCSマスタ.Deleteレコード(次勝区分番号, 次勝ラウンド番号)

            Dim 審判G As Integer = マスタデータ.C_ラウンドマスタ.Get担当審判グループ(次勝区分番号, 次勝ラウンド番号)

            For j = 1 To マスタデータ.審判員マスタ.登録済み審判員数
                If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判G) = "1" Or マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判G) = "L" Then

                    Dim PCS_C As F_審判担当PCS
                    PCS_C = New F_審判担当PCS

                    PCS_C.ジャッジ記号 = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ記号


                    Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)
                    マスタデータ.J_新審判設定.Set_新審判基準VER(採点方式)
                    Dim PCS数 As Integer = マスタデータ.J_新審判設定.GetPCS数


                    For d = 1 To 次種目数

                        If PCS数 = 3 Then
                            PCS_C.担当PCS番号(d) = "123"


                        ElseIf PCS数 = 4 Then
                            PCS_C.担当PCS番号(d) = "1234"

                        ElseIf PCS数 = 0 Then
                            PCS_C.担当PCS番号(d) = ""
                            MsgBox("エラー：設定ファイルにPCSが設定されていません。採点方式:" & 採点方式)

                        Else
                            For p = 1 To PCS数
                                PCS_C.担当PCS番号(d) = PCS_C.担当PCS番号(d) & p
                            Next p
                        End If

                    Next d


                    '区分番号とラウンド番号を渡すだけ　読込みは失敗するはず
                    マスタデータ.F_審判担当PCSマスタ.Read(次勝区分番号, 次勝ラウンド番号)

                    If マスタデータ.F_審判担当PCSマスタ.登録(PCS_C) = 0 Then

                    Else
                        MsgBox(次勝区分番号 & " " & 次勝ラウンド番号 & "のPCS担当表の作成に失敗しました。")
                    End If
                End If
            Next j



            'ヒートに2組以上いたら、ヒート表作成済みに変更
            If マスタデータ.E_ヒート表マスタ.登録済みレコード数 >= 2 Then
                '進行マスターをヒート表作成済みに変更
                Dim 採点進行 As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(次勝区分番号, 次勝ラウンド番号)
                採点進行.ステータス = "ヒート表作成済み"
                マスタデータ.T_採点進行管理.登録(採点進行)
            End If

        End If



        '次のヒート票に登録  負けた方
        Dim 次負ラウンド番号 As String = ""
        Dim 次負区分番号 = マスタデータ.BR_グループマスタ.Get敗者区分ラウンド(区分番号, ラウンド番号, 次負ラウンド番号)

        If 次負区分番号 <> "" And 次負ラウンド番号 <> "" Then
            Dim ヒートC As E_ヒート表
            ヒートC = New E_ヒート表

            ヒートC.背番号 = 敗者背番号

            Dim 種目記号リスト() = Nothing
            Dim 次種目数 = マスタデータ.D_種目マスタ.Get_種目数(次勝区分番号, 次負ラウンド番号, 種目記号リスト)

            For d = 1 To 次種目数
                ヒートC.ヒート番号(d) = 1
            Next d

            マスタデータ.E_ヒート表マスタ.Read(次負区分番号, 次負ラウンド番号)
            マスタデータ.E_ヒート表マスタ.登録(ヒートC, 次負区分番号, 次負ラウンド番号)

            'PCS担当表を作成

            '古いPCS表を消す
            マスタデータ.F_審判担当PCSマスタ.Deleteレコード(次負区分番号, 次負ラウンド番号)

            Dim 審判G As Integer = マスタデータ.C_ラウンドマスタ.Get担当審判グループ(次負区分番号, 次負ラウンド番号)

            For j = 1 To マスタデータ.審判員マスタ.登録済み審判員数
                If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判G) = "1" Or マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判G) = "L" Then

                    Dim PCS_C As F_審判担当PCS
                    PCS_C = New F_審判担当PCS

                    PCS_C.ジャッジ記号 = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ記号

                    For d = 1 To 次種目数
                        PCS_C.担当PCS番号(d) = "1234"
                    Next d


                    '区分番号とラウンド番号を渡すだけ　読込みは失敗するはず
                    マスタデータ.F_審判担当PCSマスタ.Read(次負区分番号, 次負ラウンド番号)

                    If マスタデータ.F_審判担当PCSマスタ.登録(PCS_C) = 0 Then

                    Else
                        MsgBox(次負区分番号 & " " & 次負ラウンド番号 & "のPCS担当表の作成に失敗しました。")
                    End If
                End If
            Next j



            'ヒートに2組以上いたら、ヒート表作成済みに変更
            If マスタデータ.E_ヒート表マスタ.登録済みレコード数 >= 2 Then
                '進行マスターをヒート表作成済みに変更
                Dim 採点進行 As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(次負区分番号, 次負ラウンド番号)
                採点進行.ステータス = "ヒート表作成済み"
                マスタデータ.T_採点進行管理.登録(採点進行)
            End If

        End If

        'SC_BR_Result への追加

        '******SC_BG_Result の作成
        Dim SC_BR As SC_BR_Result

        SC_BR = New SC_BR_Result

        Dim カテゴリ番号 = マスタデータ.BR_グループマスタ.Getカテゴリ番号(区分番号, ラウンド番号)

        SC_BR.カテゴリ番号 = カテゴリ番号
        SC_BR.カテゴリ名 = マスタデータ.BR_カテゴリマスタ.Getカテゴリ表記名(カテゴリ番号)
        SC_BR.区分番号 = 区分番号
        SC_BR.ラウンド番号 = ラウンド番号

        Dim 区分 As B_区分 = マスタデータ.B_区分マスタ.Get区分C(区分番号)
        Dim 選手マスタLIST番号 = 区分.使用する選手マスタ

        If 勝者背番号 = "0" Then

        Else
            SC_BR.勝者背番号 = 勝者背番号

            Dim 勝者選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 勝者背番号)

            SC_BR.勝者リーダー名 = 勝者選手.リーダー表記名
            SC_BR.勝者パートナー名 = 勝者選手.パートナ表記名
            SC_BR.勝者所属 = 勝者選手.カップル所属名

        End If


        Dim 勝者点数結果 As Double = 0
        Dim 勝者WIN数 As Integer = 0
        Dim 敗者点数結果 As Double = 0
        Dim 敗者WIN数 As Integer = 0


        SC_BR.勝者点数 = 勝者点数結果
        SC_BR.勝者WIN数 = 勝者WIN数
        SC_BR.敗者点数 = 敗者点数結果
        SC_BR.敗者WIN数 = 敗者WIN数

        If 勝者背番号 = "0" Then

        Else
            Dim SC_BR_Resultマスタ As SC_BR_Resultマスタ
            SC_BR_Resultマスタ = New SC_BR_Resultマスタ(マスタデータ.Z_システム設定.システムPath)
            SC_BR_Resultマスタ.登録(SC_BR)
            SC_BR_Resultマスタ = Nothing
        End If

        '====


        マスタデータ = Nothing

        Me.Close()


    End Sub



    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click

        Me.Close()

    End Sub


    'フォーム閉じるボタンが押された時
    Private Sub Me_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing


        'If MsgBox("確定せずに終了しても良いですか？", vbOKCancel) = vbOK Then

        'Me.Close()
        'Else
        '閉じるをキャンセル
        'e.Cancel = True
        'End If


    End Sub

End Class