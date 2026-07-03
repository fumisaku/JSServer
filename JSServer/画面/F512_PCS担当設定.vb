Public Class F512_PCS担当設定

    'ジャッジの担当PCSを設定する
    Private 更新FLAG As Boolean = False

    Private マスタデータ As マスタデータ
    Private 区分番号, ラウンド番号 As String

    Private カウンター As Integer

    Private Sub F512_PCS担当設定_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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


        '採点方式がAJSではない時は、メッセージを出して、クローズする。

        If マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) = "順位法" Or
           マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) = "チェック法" Then

            MsgBox("このラウンドの採点方式は、" & マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) & "ですので、PCSの設定は不要です。")
            Me.Close()
            Exit Sub
        End If

        マスタデータ.J_新審判設定.Set_新審判基準VER(マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号))

        項目設定()
        PCS表作成(True)



    End Sub


    Private Sub 項目設定()

        '区分名
        'ラベルの設定
        Me.LB_区分名.Text = マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & " " & マスタデータ.Get_ラウンド名(ラウンド番号)

        'Me.LB_出場組数.Text = UBound(背番号リスト) & " 組"  'ヒート表作成後に移動

        Me.LB_採点方式.Text = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

    End Sub


    Private Sub PCS表作成(ファイル読込FLAG As Boolean)

        'ファイル読込FLAG がTrueの時は、ファイルを検索に行く。
        'falseの時は、ファイルは検索に行かない


        'データクリア
        Me.DGV_PCS.DataSource = Nothing
        Me.DGV_PCS.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_PCS.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 14, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_PCS.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)

        'DGVのデフォルト配置を真ん中にする
        Me.DGV_PCS.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.DGV_PCS.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        '// データテーブルの作成
        Dim tbl As New DataTable

        Dim 種目記号リスト() = Nothing
        Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("記号", GetType(String)))
        tbl.Columns.Add(New DataColumn("ジャッジ名", GetType(String)))

        For d = 1 To 種目数
            tbl.Columns.Add(New DataColumn(種目記号リスト(d), GetType(String)))
        Next d

        tbl.Columns.Add(New DataColumn("Check", GetType(String)))



        '====データ行の追加
        Dim 行番号 As Integer = 0

        '  ジャッジ人数の確認
        Dim 審判グループ番号 As Integer = マスタデータ.C_ラウンドマスタ.Get担当審判グループ(区分番号, ラウンド番号）

        Dim 審判員数 As Integer = 0
        For j = 1 To マスタデータ.審判員マスタ.登録済み審判員数
            If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判グループ番号) = "1" Or
               マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判グループ番号) = "L" Or
               マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判グループ番号) = "S" Then    'バルカーの特別審査

                審判員数 = 審判員数 + 1

                tbl.Rows.Add()
                tbl.Rows(行番号).Item("No") = 審判員数
                tbl.Rows(行番号).Item("記号") = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ記号
                tbl.Rows(行番号).Item("ジャッジ名") = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ表記名

                行番号 = 行番号 + 1
            End If
        Next j


        '// DataGridViewにデータセットを設定
        Me.DGV_PCS.DataSource = tbl


        '===列幅の自動調整
        Me.DGV_PCS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        For d = 1 To 種目数
            '  Me.DGV_PCS.Columns(d + 2).Width = 50
        Next d
        Me.DGV_PCS.AllowUserToResizeColumns = True


        'ヘッダーにSolo Group 種別を記入
        For d = 1 To 種目数
            Dim SG種別 As String = "(" & マスタデータ.D_種目マスタ.Get_SG種別表記名(区分番号, ラウンド番号, d) & ")"

            Me.DGV_PCS.Columns(d + 2).HeaderText = Me.DGV_PCS.Columns(d + 2).HeaderText & Environment.NewLine & SG種別
        Next d

        'Me.DGV_ジャッジ.Columns(CStr(g)).HeaderText = CStr(g) & Environment.NewLine & "(" & ジャッジ数 & ")"

        PCS表作成2(ファイル読込FLAG)

    End Sub

    Private Sub PCS表作成2(ファイル読込FLAG As Boolean)
        '担当PCS番号を更新する

        If マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) = "VALQ25S" Then
            'バルカーカップ2025の時

            If ファイル読込FLAG = True Then
                If マスタデータ.F_審判担当PCSマスタ.FileCheck(区分番号, ラウンド番号) = True Then
                    マスタデータ.F_審判担当PCSマスタ.Read(区分番号, ラウンド番号)
                    'ファイルからマッピング
                    PCS_Fromファイル()
                Else
                    '
                    カウンター = 0
                    PCS_VALQUA()
                End If
            Else
                '
                カウンター = 0
                PCS_VALQUA()
            End If


        Else
            'バルカーカップ以外の時

            If ファイル読込FLAG = True Then
                If マスタデータ.F_審判担当PCSマスタ.FileCheck(区分番号, ラウンド番号) = True Then
                    マスタデータ.F_審判担当PCSマスタ.Read(区分番号, ラウンド番号)
                    'ファイルからマッピング
                    PCS_Fromファイル()
                Else
                    '乱数で設定
                    カウンター = 0
                    PCS_From乱数()
                End If
            Else
                '乱数で設定
                カウンター = 0
                PCS_From乱数()
            End If

        End If

    End Sub

    Private Sub PCS_Fromファイル()
        'ファイルからPCSを設定する

        Dim 種目記号リスト() = Nothing
        Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)


        For i = 0 To DGV_PCS.RowCount - 1
            If DGV_PCS.Rows(i).Cells("記号").Value Is Nothing Or
                DGV_PCS.Rows(i).Cells("記号").Value Is DBNull.Value Then
            Else
                Dim 記号 As String = DGV_PCS.Rows(i).Cells("記号").Value
                Dim 審判P As F_審判担当PCS = マスタデータ.F_審判担当PCSマスタ.Get_審判担当PCSClass(記号)

                If 審判P Is Nothing Then
                    MsgBox("ジャッジ記号「" & 記号 & "」の担当PCSがPCSマスタに存在しません。")

                Else
                    For d = 1 To 種目数
                        DGV_PCS.Rows(i).Cells(種目記号リスト(d)).Value = 審判P.担当PCS番号(d)

                    Next d

                End If

            End If
        Next i

    End Sub


    Private Sub PCS_From乱数()
        '乱数でPCSを設定する

        Dim 種目記号リスト() = Nothing
        Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        'PCS数
        Dim PCS数 As Integer = マスタデータ.J_新審判設定.GetPCS数


        'ジャッジ人数の確認
        Dim 審判員数 As Integer = 0
        For i = 0 To DGV_PCS.RowCount - 1
            If DGV_PCS.Rows(i).Cells("記号").Value Is Nothing Or
                DGV_PCS.Rows(i).Cells("記号").Value Is DBNull.Value Then
            Else
                審判員数 = 審判員数 + 1
            End If
        Next i

        For d = 1 To 種目数

            'PCS組合わせ
            Dim PCS組合せ数 As Integer = 0
            Dim PCS組合せ() As String = Nothing

            'PCS組合わせ数の確認
            Select Case マスタデータ.D_種目マスタ.Get_SG種別表記名(区分番号, ラウンド番号, d)
                Case "ソロ"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Solo_Flag = 1 Then
                            PCS組合せ数 = PCS組合せ数 + 1
                        End If
                    Next i
                Case "全員"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Group_Flag = 1 Then
                            PCS組合せ数 = PCS組合せ数 + 1
                        End If
                    Next i

                Case "対戦"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Duel_Flag = 1 Then
                            PCS組合せ数 = PCS組合せ数 + 1
                        End If
                    Next i
            End Select

            'PCS組合わせ配列の作成
            ReDim PCS組合せ(PCS組合せ数)
            Dim p As Integer = 0

            Select Case マスタデータ.D_種目マスタ.Get_SG種別表記名(区分番号, ラウンド番号, d)
                Case "ソロ"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Solo_Flag = 1 Then
                            p = p + 1
                            PCS組合せ(p) = マスタデータ.J_新審判設定.PCS担当(i).担当PCS
                        End If
                    Next i
                Case "全員"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Group_Flag = 1 Then
                            p = p + 1
                            PCS組合せ(p) = マスタデータ.J_新審判設定.PCS担当(i).担当PCS
                        End If
                    Next i

                Case "対戦"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Duel_Flag = 1 Then
                            p = p + 1
                            PCS組合せ(p) = マスタデータ.J_新審判設定.PCS担当(i).担当PCS
                        End If
                    Next i
            End Select


            Dim PCS配列() = Nothing

            '乱数発生用のカウンタ
            Dim ミリ秒 = d + CInt(DateTime.Now.ToString("fff"))
            Dim ヒート配列() = Nothing
            ヒート乱数(審判員数, PCS組合せ数, PCS配列, ミリ秒)

            Dim j = 0
            For i = 0 To DGV_PCS.RowCount - 1
                If DGV_PCS.Rows(i).Cells("記号").Value Is Nothing Or
                    DGV_PCS.Rows(i).Cells("記号").Value Is DBNull.Value Then
                Else
                    j = j + 1
                    DGV_PCS.Rows(i).Cells(種目記号リスト(d)).value = PCS組合せ(PCS配列(j))

                End If
            Next i

        Next d
        '

        'チェック
        'PCS数 でチェック
        Dim アサインFLAG() As Boolean

        If 種目数 >= 4 Then  '４種目以上の時だけチェックする
            For i = 0 To DGV_PCS.RowCount - 1

                ReDim アサインFLAG(PCS数)
                For p = 1 To PCS数
                    アサインFLAG(p) = False
                Next p


                If DGV_PCS.Rows(i).Cells("記号").Value Is Nothing Or
                        DGV_PCS.Rows(i).Cells("記号").Value Is DBNull.Value Then
                Else
                    'ジャッジ毎に検索
                    Dim 記号 As String = DGV_PCS.Rows(i).Cells("記号").Value

                    For d = 1 To 種目数
                        For p = 1 To PCS数
                            If Strings.InStr(DGV_PCS.Rows(i).Cells(d + 2).Value, CStr(p)) > 0 Then
                                アサインFLAG(p) = True
                            End If
                        Next p
                    Next d

                    '検索結果を確認
                    For p = 1 To PCS数
                        If アサインFLAG(p) = False Then
                            カウンター = カウンター + 1
                            If カウンター = 100 Then
                                MsgBox("１００回実行済み")
                                DGV_PCS.Rows(i).Cells("Check").Value = CStr(p)

                            Else
                                PCS_From乱数()

                            End If
                        End If
                    Next p
                End If
            Next i
        End If


    End Sub


    Private Sub PCS_VALQUA()
        'バルカーカップ用のPCSを設定する

        Dim 種目記号リスト() = Nothing
        Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        'PCS数
        Dim PCS数 As Integer = マスタデータ.J_新審判設定.GetPCS数



        Dim 審判グループ番号 As Integer = マスタデータ.C_ラウンドマスタ.Get担当審判グループ(区分番号, ラウンド番号）


        'ジャッジ人数の確認
        Dim 審判員数 As Integer = 0
        For i = 0 To DGV_PCS.RowCount - 1
            If DGV_PCS.Rows(i).Cells("記号").Value Is Nothing Or
                DGV_PCS.Rows(i).Cells("記号").Value Is DBNull.Value Then
            Else
                審判員数 = 審判員数 + 1
            End If
        Next i


        For d = 1 To 種目数


            'PCS組合わせ
            Dim PCS組合せ数 As Integer = 0
            Dim PCS組合せ() As String = Nothing


            'PCS組合わせ数の確認
            Select Case マスタデータ.D_種目マスタ.Get_SG種別表記名(区分番号, ラウンド番号, d)
                Case "ソロ"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Solo_Flag = 1 Then
                            PCS組合せ数 = PCS組合せ数 + 1
                        End If
                    Next i
                Case "全員"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Group_Flag = 1 Then
                            PCS組合せ数 = PCS組合せ数 + 1
                        End If
                    Next i

                Case "対戦"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Duel_Flag = 1 Then
                            PCS組合せ数 = PCS組合せ数 + 1
                        End If
                    Next i
            End Select


            'PCS組合わせ配列の作成
            ReDim PCS組合せ(PCS組合せ数)
            Dim p As Integer = 0

            Select Case マスタデータ.D_種目マスタ.Get_SG種別表記名(区分番号, ラウンド番号, d)
                Case "ソロ"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Solo_Flag = 1 Then
                            p = p + 1
                            PCS組合せ(p) = マスタデータ.J_新審判設定.PCS担当(i).担当PCS
                        End If
                    Next i
                Case "全員"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Group_Flag = 1 Then
                            p = p + 1
                            PCS組合せ(p) = マスタデータ.J_新審判設定.PCS担当(i).担当PCS
                        End If
                    Next i

                Case "対戦"
                    For i = 1 To マスタデータ.J_新審判設定.PCS担当_設定数
                        If マスタデータ.J_新審判設定.PCS担当(i).Duel_Flag = 1 Then
                            p = p + 1
                            PCS組合せ(p) = マスタデータ.J_新審判設定.PCS担当(i).担当PCS
                        End If
                    Next i
            End Select

            Dim j = 0
            For i = 0 To DGV_PCS.RowCount - 1
                If DGV_PCS.Rows(i).Cells("記号").Value Is Nothing Or
                    DGV_PCS.Rows(i).Cells("記号").Value Is DBNull.Value Then
                Else
                    j = j + 1


                    If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判グループ番号) = "1" Then
                        'ジャッジタイプが"1"の時 通常ジャッジ

                        DGV_PCS.Rows(i).Cells(種目記号リスト(d)).value = PCS組合せ(1)

                    ElseIf マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(審判グループ番号) = "S" Then
                        'ジャッジタイプが"S"の時 特別審査
                        DGV_PCS.Rows(i).Cells(種目記号リスト(d)).value = PCS組合せ(2)

                    End If


                End If
            Next i

        Next d
        '



    End Sub

    Private Sub ヒート乱数(ByVal 人数, ByVal PCS数, ByRef 配列, ByVal カウンタ)
        '===========================
        '概要　ヒート乱数の発生 人数とヒート数を指定すると、配列に１～ヒート数の乱数を入れて返す。
        '入力　人数   
        '出力　配列()
        '===========================
        Dim cRandom As New System.Random(カウンタ)

        Dim Group配列(), 乱数配列(), i
        ReDim Group配列(人数)
        ReDim 乱数配列(人数)

        ReDim 配列(人数)

        For i = 1 To 人数
            Group配列(i) = i Mod PCS数 + 1
            乱数配列(i) = cRandom.Next(512)
        Next i

        '並べ替え
        Array.Sort(乱数配列, Group配列)

        '結果の挿入
        For i = 1 To 人数
            配列(i) = Group配列(i)
        Next i


    End Sub


    Private Sub PB_戻る_Click(sender As Object, e As EventArgs) Handles PB_戻る.Click

        Me.Close()

    End Sub

    Private Sub PB_確定_Click(sender As Object, e As EventArgs) Handles PB_確定.Click


        'PCS表作成
        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ

        '古いPCS表を消す
        マスタデータ.F_審判担当PCSマスタ.Deleteレコード(区分番号, ラウンド番号)

        'PCSファイルの作成
        Dim 種目記号リスト() = Nothing
        Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)

        Dim 人数 As Integer = 0
        For i = 0 To Me.DGV_PCS.RowCount - 1
            If DGV_PCS.Rows(i).Cells("記号").Value Is Nothing Or
               DGV_PCS.Rows(i).Cells("記号").Value Is DBNull.Value Then
            Else
                人数 = 人数 + 1
            End If
        Next i

        Dim RC As Integer = 1

        For i = 0 To DGV_PCS.RowCount - 1
            If DGV_PCS.Rows(i).Cells("記号").Value Is Nothing Or
               DGV_PCS.Rows(i).Cells("記号").Value Is DBNull.Value Then
            Else
                Dim PCS_C As F_審判担当PCS
                PCS_C = New F_審判担当PCS

                PCS_C.ジャッジ記号 = DGV_PCS.Rows(i).Cells("記号").Value

                For d = 1 To 種目数
                    PCS_C.担当PCS番号(d) = DGV_PCS.Rows(i).Cells(種目記号リスト(d)).Value
                Next d

                '区分番号とラウンド番号を渡すだけ　読込みは失敗するはず
                マスタデータ.F_審判担当PCSマスタ.Read(区分番号, ラウンド番号)

                RC = マスタデータ.F_審判担当PCSマスタ.登録(PCS_C)

            End If
        Next i






        If RC = 0 Then
            'まとめ印刷
            If CB_まとめ印刷.Checked = True Then

                Dim F300 = New F300_印刷画面

                '前ラウンドを確認
                マスタデータ.C_ラウンドマスタ.FileRead()
                Dim 前ラウンド As C_ラウンド = マスタデータ.C_ラウンドマスタ.Get_前ラウンドClass(区分番号, ラウンド番号)

                If 前ラウンド Is Nothing Then
                    '次ラウンドのヒート表のみ印刷



                    F300.設定(区分番号, ラウンド番号)

                    If CB_インターネット.Checked = True Then
                        F300.現ヒート表印刷(True)
                    Else
                        F300.現ヒート表印刷(False)

                    End If


                Else
                    '前ラウンドの結果と、次ラウンドのヒート表をまとめ印刷


                    F300.設定(区分番号, 前ラウンド.ラウンド番号)


                    If CB_インターネット.Checked = True Then
                        F300.まとめ印刷(True)
                    Else
                        F300.まとめ印刷(False)
                    End If


                End If


                F300 = Nothing

            Else
                MsgBox("保存しました。")

            End If


        Else
            MsgBox("保存に失敗しました。")
        End If

        マスタデータ = Nothing

        Me.Close()


    End Sub

    Private Sub PB_再ヒート割り_Click(sender As Object, e As EventArgs) Handles PB_再ヒート割り.Click

        PCS表作成(False)

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