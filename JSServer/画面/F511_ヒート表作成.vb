Public Class F511_ヒート表作成

    'Private 採点結果 As 採点結果_C
    Private マスタデータ As マスタデータ
    Private 区分番号, ラウンド番号 As String

    Private ヒート割方式 As String

    Private Sub F511_ヒート表作成_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '表示位置の設定

        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(355, 0)

    End Sub

    Public Sub 設定(区分番号_ As String, ラウンド番号_ As String, 背番号リスト_() As String)

        '説明
        '(1) 背番号リスト() is Nothing の時は、ヒート表の確認 -- ファイルの中身を表示する
        '(2) 背番号リストがある時は、ファイルがあればファイルを表示する
        '(3) ファイルと、背番号リストの 背番号が違う時は、背番号リストを優先するが、ユーザーに確認させる
        '    


        区分番号 = 区分番号_
        ラウンド番号 = ラウンド番号_

        マスタデータ = New マスタデータ


        項目設定()
        ヒート数表作成()
        ヒート表作成(背番号リスト_, False)

        If Strings.Left(Me.LB_採点方式.Text, 3) = "BJS" Then
            If マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 1).SG種別 = "S" Then
                '1種目目（１ラウンド目）がソロの時は表示しない

                Me.LB_BR注意.Visible = False

            Else
                Me.LB_BR注意.Visible = True
            End If
        Else
            Me.LB_BR注意.Visible = False
        End If


        マスタデータ = Nothing
    End Sub

    Private Sub 項目設定()

        'コンボボックスの初期値設定
        Me.CB_ヒート割方式.Items.Add("横割り")
        Me.CB_ヒート割方式.Items.Add("縦割り")
        Me.CB_ヒート割方式.Items.Add("シャッフル")

        Me.CB_ヒート割方式.Text = マスタデータ.C_ラウンドマスタ.GetラウンドClass(区分番号, ラウンド番号).ヒート割方式

        '区分名
        'ラベルの設定
        Me.LB_区分名.Text = マスタデータ.B_区分マスタ.Get区分表記名(区分番号) & " " & マスタデータ.Get_ラウンド名(ラウンド番号)

        'Me.LB_出場組数.Text = UBound(背番号リスト) & " 組"  'ヒート表作成後に移動

        Me.LB_採点方式.Text = マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号)

    End Sub



    Private Sub ヒート数表作成()


        'データクリア
        Me.DGV_ヒート数.DataSource = Nothing
        Me.DGV_ヒート数.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_ヒート数.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_ヒート数.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)

        'DGVのデフォルト配置を真ん中にする
        Me.DGV_ヒート数.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.DGV_ヒート数.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        '// データテーブルの作成
        Dim tbl As New DataTable

        Dim 種目記号リスト() = Nothing
        Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)


        For d = 1 To 種目数
            tbl.Columns.Add(New DataColumn(種目記号リスト(d), GetType(String)))
        Next d

        Dim 行番号 As Integer = 0
        tbl.Rows.Add()
        For d = 1 To 種目数
            tbl.Rows(行番号).Item(種目記号リスト(d)) = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, d).ヒート数
        Next d


        '// DataGridViewにデータセットを設定
        Me.DGV_ヒート数.DataSource = tbl


        '===列幅の自動調整
        'Me.DGV_ヒート数.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        For d = 1 To 種目数
            Me.DGV_ヒート数.Columns(d - 1).Width = 73
        Next d
        Me.DGV_ヒート数.AllowUserToResizeColumns = True


        'ヘッダーにSolo Group 種別を記入
        For d = 1 To 種目数
            Dim SG種別 As String = "(" & マスタデータ.D_種目マスタ.Get_SG種別表記名(区分番号, ラウンド番号, d) & ")"

            Me.DGV_ヒート数.Columns(d - 1).HeaderText = Me.DGV_ヒート数.Columns(d - 1).HeaderText & Environment.NewLine & SG種別
        Next d

        'Me.DGV_ジャッジ.Columns(CStr(g)).HeaderText = CStr(g) & Environment.NewLine & "(" & ジャッジ数 & ")"

    End Sub

    Private Sub ヒート表作成(背番号リスト() As String, 再作成Flag As Boolean)

        '再作成FLAGがTrueの時はファイルから読みこまない

        If マスタデータ Is Nothing Then
            マスタデータ = New マスタデータ
        End If

        '=======ヒートファイルの存在から処理方法を確定
        If 背番号リスト Is Nothing Then
            If マスタデータ.E_ヒート表マスタ.FileCheck(区分番号, ラウンド番号) = True Then
                'ファイルから作成
                マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号）
                ヒート表作成2(Nothing)
            Else
                'エラーで終了
                MsgBox("ヒート表を作成してください。")
                '新規にて入力でヒート表作成
                ヒート表作成2(Nothing)
            End If

        Else  '背番号リストが存在する時
            If 再作成Flag = True Then
                'ヒート表再作成ボタンが押された時
                ヒート表作成2(背番号リスト)

            Else
                '新規の時

                '背番号リストが存在する時
                If マスタデータ.E_ヒート表マスタ.FileCheck(区分番号, ラウンド番号) = True Then
                    マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号）

                    '背番号リストとファイルの背番号を確認
                    If 背番号リストチェック(背番号リスト, マスタデータ.E_ヒート表マスタ) = True Then
                        'ファイルから読込み
                        MsgBox("設定済みのヒート表が存在します。")
                        ヒート表作成2(Nothing)
                    Else
                        If MsgBox("作成済みのヒート表と背番号が一致していません。新規にヒート表を作成しますか？", vbYesNo) = vbYes Then
                            ' 通常のヒート表作成ケース
                            ヒート表作成2(背番号リスト)

                        Else
                            ' ファイルから読込み
                            ヒート表作成2(Nothing)
                        End If

                    End If

                Else  'ファイルが存在しない時
                    '通常のヒート表作成ケース
                    ヒート表作成2(背番号リスト)

                End If
            End If
        End If


    End Sub

    Private Sub ヒート表作成2(背番号リスト() As String)
        '背番号リストが Nothing の時は、HeatFileから作成する
        '    false の時は、背番号リストを元に、新規に作成する

        Dim F背番号リスト() = Nothing
        Dim F人数 = マスタデータ.E_ヒート表マスタ.リスト


        'データクリア
        Me.DGV_ヒート表.DataSource = Nothing
        Me.DGV_ヒート表.Rows.Clear()

        'DGVのデフォルトフォント変更
        Me.DGV_ヒート表.DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)
        'ヘッダーのフォント指定
        Me.DGV_ヒート表.ColumnHeadersDefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 12, FontStyle.Regular)

        'DGVのデフォルト配置を真ん中にする
        Me.DGV_ヒート表.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.DGV_ヒート表.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        '// データテーブルの作成
        Dim tbl As New DataTable
        tbl.Columns.Add(New DataColumn("No", GetType(Integer)))
        tbl.Columns.Add(New DataColumn("背番号", GetType(String)))
        tbl.Columns.Add(New DataColumn("名前", GetType(String)))

        Dim 種目記号リスト() = Nothing
        Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)


        For d = 1 To 種目数
            tbl.Columns.Add(New DataColumn(種目記号リスト(d), GetType(String)))
        Next d

        Dim 選手マスタLIST番号 As String = マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ

        Dim 行番号 As Integer = 0

        Dim 出場選手数 As Integer = 0
        If 背番号リスト Is Nothing Then
            'ファイルから読み込む
            出場選手数 = マスタデータ.E_ヒート表マスタ.登録済みレコード数
        Else
            出場選手数 = UBound(背番号リスト)
        End If

        For i = 1 To 出場選手数
            tbl.Rows.Add()
            tbl.Rows(行番号).Item("No") = 行番号 + 1

            If 背番号リスト Is Nothing Then
                'ファイルから読み込む
                tbl.Rows(行番号).Item("背番号") = マスタデータ.E_ヒート表マスタ.リスト(i).背番号
            Else
                tbl.Rows(行番号).Item("背番号") = 背番号リスト(i)
            End If

            If マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, tbl.Rows(行番号).Item("背番号")) IsNot Nothing Then

                tbl.Rows(行番号).Item("名前") = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, tbl.Rows(行番号).Item("背番号")).リーダー表記名
            Else
                MsgBox("背番号 " & tbl.Rows(行番号).Item("背番号") & " 番の選手が、" & 選手マスタLIST番号 & "のマスタに存在しません。”)
            End If

            行番号 = 行番号 + 1
        Next i


        '選手毎のヒート番号を設定

        For d = 1 To 種目数

            If 背番号リスト Is Nothing Then
                'ファイルから読み込む
                出場選手数 = マスタデータ.E_ヒート表マスタ.登録済みレコード数

                For i = 1 To 出場選手数
                    tbl.Rows(i - 1).Item(種目記号リスト(d)) = マスタデータ.E_ヒート表マスタ.Get_ヒート番号(d, マスタデータ.E_ヒート表マスタ.リスト(i).背番号)
                Next i

            Else '背番号リストを元に新規にヒート表を　作成する
                出場選手数 = UBound(背番号リスト)

                Dim ヒート数 As Integer
                If InStr(Me.DGV_ヒート数.Columns(d - 1).HeaderText, "ソロ") > 0 Then
                    ヒート数 = 出場選手数
                    Me.DGV_ヒート数.Rows(0).Cells(種目記号リスト(d)).Value = 出場選手数

                ElseIf InStr(Me.DGV_ヒート数.Columns(d - 1).HeaderText, "対戦") > 0 Then
                    '対戦の時のヒート数は、出場選手数÷2 余った時はヒート数を追加する
                    ヒート数 = Math.Ceiling(出場選手数 / 2)
                    Me.DGV_ヒート数.Rows(0).Cells(種目記号リスト(d)).Value = ヒート数

                Else
                    ヒート数 = Me.DGV_ヒート数.Rows(0).Cells(種目記号リスト(d)).Value
                End If


                If Me.CB_ヒート割方式.Text = "シャッフル" Then

                        If ヒート数 = 1 Then

                            For i = 1 To 出場選手数
                                tbl.Rows(i - 1).Item(種目記号リスト(d)) = 1
                            Next i

                        Else

                            '乱数発生用のカウンタ
                            Dim ミリ秒 = d + CInt(DateTime.Now.ToString("fff"))
                            Dim ヒート配列() = Nothing
                            ヒート乱数(出場選手数, ヒート数, ヒート配列, ミリ秒)

                            For i = 1 To 出場選手数
                                tbl.Rows(i - 1).Item(種目記号リスト(d)) = ヒート配列(i)
                            Next i
                        End If



                    ElseIf Me.CB_ヒート割方式.Text = "横割り" Then

                        If ヒート数 = 1 Then

                            For i = 1 To 出場選手数
                                tbl.Rows(i - 1).Item(種目記号リスト(d)) = 1
                            Next i

                        Else
                            Dim ヒート当り組数 As Integer = 出場選手数 / ヒート数
                            Dim 余りヒート数 As Integer = 出場選手数 Mod ヒート数

                            Dim ヒート番号 As Integer = 1
                            For i = 1 To 出場選手数

                                tbl.Rows(i - 1).Item(種目記号リスト(d)) = ヒート番号

                                If (i Mod (ヒート当り組数 + 1) = 0 And ヒート番号 <= 余りヒート数) Or
                          (i Mod (ヒート当り組数) = 0 And ヒート番号 > 余りヒート数) Then

                                    ヒート番号 = ヒート番号 + 1
                                End If
                            Next i

                        End If
                    ElseIf Me.CB_ヒート割方式.Text = "縦割り" Then

                        If ヒート数 = 1 Then

                            For i = 1 To 出場選手数
                                tbl.Rows(i - 1).Item(種目記号リスト(d)) = 1
                            Next i

                        Else
                            Dim ヒート当り組数 As Integer = 出場選手数 / ヒート数

                            Dim ヒート番号 As Integer = 1
                            For i = 1 To 出場選手数

                                ヒート番号 = i Mod ヒート数
                                If ヒート番号 = 0 Then
                                    ヒート番号 = ヒート数
                                End If
                                tbl.Rows(i - 1).Item(種目記号リスト(d)) = ヒート番号

                            Next i

                        End If
                    End If

                End If

        Next d






        '// DataGridViewにデータセットを設定
        Me.DGV_ヒート表.DataSource = tbl


        '===列幅の自動調整
        'Me.DGV_ヒート表.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGV_ヒート表.RowHeadersWidth = 25
        Me.DGV_ヒート表.Columns("No").Width = 50
        Me.DGV_ヒート表.Columns("背番号").Width = 80
        Me.DGV_ヒート表.Columns("名前").Width = 140
        Me.DGV_ヒート表.Columns("名前").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        For d = 1 To 種目数
            Me.DGV_ヒート表.Columns(d + 2).Width = 73
        Next d


        Me.DGV_ヒート表.AllowUserToResizeColumns = True

        'ラベルの設定
        Me.LB_出場組数.Text = 出場選手数.ToString & " 組"


        'イベントハンドらの登録
        AddHandler DGV_ヒート表.CurrentCellDirtyStateChanged, AddressOf Me.OnStateChange




    End Sub

    Private Function 背番号リストチェック(背番号リスト() As String, ヒート表マスタClass As E_ヒート表マスタ) As Boolean
        Dim rc As Boolean = True

        '人数の確認
        Dim 人数 As Integer = UBound(背番号リスト)
        If 人数 <> ヒート表マスタClass.登録済みレコード数 Then
            rc = False
        Else
            For i = 1 To 人数
                Dim FindFlag As Boolean = False
                For f = 1 To 人数
                    If 背番号リスト(i) = ヒート表マスタClass.リスト(f).背番号 Then
                        FindFlag = True
                        f = 人数
                    End If
                Next f

                If FindFlag = False Then
                    rc = False
                    i = 人数
                End If

            Next i
        End If


        Return rc
    End Function


    Private Sub PB_再ヒート割り_Click(sender As Object, e As EventArgs) Handles PB_再ヒート割り.Click

        '既存のDGVから背番号リストを作成する
        Dim 背番号リストTemp() As Object = Nothing
        'Dim 背番号リストTemp() As String = Nothing

        Dim 人数 As Integer = 0
        For i = 0 To DGV_ヒート表.RowCount - 1
            If DGV_ヒート表.Rows(i).Cells("背番号").Value Is Nothing Or
               DGV_ヒート表.Rows(i).Cells("背番号").Value Is DBNull.Value Then

            Else
                人数 = 人数 + 1
            End If
        Next i

        ReDim 背番号リストTemp(人数)

        For i = 0 To DGV_ヒート表.RowCount - 1
            If DGV_ヒート表.Rows(i).Cells("背番号").Value Is Nothing Or
               DGV_ヒート表.Rows(i).Cells("背番号").Value Is DBNull.Value Then

            Else

                If IsNumeric(DGV_ヒート表.Rows(i).Cells("背番号").Value) Then
                    背番号リストTemp(i + 1) = CInt(DGV_ヒート表.Rows(i).Cells("背番号").Value)
                Else
                    背番号リストTemp(i + 1) = DGV_ヒート表.Rows(i).Cells("背番号").Value.ToString()
                End If

            End If
        Next i

        Array.Sort(背番号リストTemp)

        Dim 背番号リスト() As String
        ReDim 背番号リスト(人数)

        For s = 1 To 人数
            背番号リスト(s) = 背番号リストTemp(s).ToString
        Next s

        ヒート表作成(背番号リスト, True)
    End Sub

    Private Sub ヒート乱数(ByVal 人数, ByVal ヒート数, ByRef 配列, ByVal カウンタ)
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
            Group配列(i) = i Mod ヒート数 + 1
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


        'ヒート表作成
        Dim マスタデータ As マスタデータ
        マスタデータ = New マスタデータ





        'ヒートファイルの作成
        Dim 種目記号リスト() = Nothing
        Dim 種目数 = マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)


        'ヒート番号セットのチェック
        For i = 0 To DGV_ヒート表.RowCount - 1
            If DGV_ヒート表.Rows(i).Cells("背番号").Value Is Nothing Or
               DGV_ヒート表.Rows(i).Cells("背番号").Value Is DBNull.Value Then
            Else
                For d = 1 To 種目数

                    If DGV_ヒート表.Rows(i).Cells(種目記号リスト(d)).Value Is Nothing Or
                       DGV_ヒート表.Rows(i).Cells(種目記号リスト(d)).Value Is DBNull.Value Then

                        MsgBox("ヒート番号が指定されていません。行-種目:" & i + 1 & "-" & d)
                        Exit Sub
                    Else

                    End If
                Next d
            End If
        Next i

        'ヒート番号チェック
        For d = 1 To 種目数

            Dim ヒート割当数 As New Dictionary(Of Integer, Integer)
            For i = 0 To DGV_ヒート表.RowCount - 1
                If DGV_ヒート表.Rows(i).Cells("背番号").Value Is Nothing Or
                       DGV_ヒート表.Rows(i).Cells("背番号").Value Is DBNull.Value Then
                    Continue For
                End If

                Dim ヒート番号 As Integer = CInt(DGV_ヒート表.Rows(i).Cells(種目記号リスト(d)).Value)
                If ヒート割当数.ContainsKey(ヒート番号) Then
                    ヒート割当数(ヒート番号) += 1
                Else
                    ヒート割当数(ヒート番号) = 1
                End If
            Next i

            If InStr(Me.DGV_ヒート数.Columns(d - 1).HeaderText, "ソロ") > 0 Then
                'ソロの時 
                '各ヒート毎に選手は最大1名だけ

                For Each kvp In ヒート割当数
                    If kvp.Value > 1 Then
                        MsgBox(d & "種目目 ソロ種目で、ヒート " & kvp.Key & " に複数の選手が割り当てられています。")
                        Exit Sub
                    End If
                Next


            ElseIf InStr(Me.DGV_ヒート数.Columns(d - 1).HeaderText, "対戦") > 0 Then
                '対戦の時
                '各ヒート毎に選手は最大2名まで

                For Each kvp In ヒート割当数
                    If kvp.Value > 2 Then
                        If MsgBox(d & "種目目 デュエル種目で、ヒート " & kvp.Key & " に3名以上の選手が割り当てられています。このまま進めますか？", vbYesNo + vbQuestion) = vbYes Then

                        Else
                            Exit Sub
                        End If
                    End If
                Next

            Else
                'グループ競技の時
                '1ヒート当たりの人数は最大8名まで

                For Each kvp In ヒート割当数
                    If kvp.Value > 8 Then
                        MsgBox(d & "種目目 全員種目で、ヒート " & kvp.Key & " に9名以上の選手が割り当てられています。")
                        Exit Sub
                    End If
                Next



            End If
        Next d





        '古いヒート表を消す
        マスタデータ.E_ヒート表マスタ.Deleteレコード(区分番号, ラウンド番号)


        Dim 人数 As Integer = 0
        For i = 0 To DGV_ヒート表.RowCount - 1
            If DGV_ヒート表.Rows(i).Cells("背番号").Value Is Nothing Or
               DGV_ヒート表.Rows(i).Cells("背番号").Value Is DBNull.Value Then
            Else
                人数 = 人数 + 1
            End If
        Next i

        For i = 0 To DGV_ヒート表.RowCount - 1
            If DGV_ヒート表.Rows(i).Cells("背番号").Value Is Nothing Or
               DGV_ヒート表.Rows(i).Cells("背番号").Value Is DBNull.Value Then
            Else
                Dim ヒートC As E_ヒート表
                ヒートC = New E_ヒート表

                ヒートC.背番号 = DGV_ヒート表.Rows(i).Cells("背番号").Value
                For d = 1 To 種目数
                    ヒートC.ヒート番号(d) = DGV_ヒート表.Rows(i).Cells(種目記号リスト(d)).Value
                Next d

                マスタデータ.E_ヒート表マスタ.登録(ヒートC, 区分番号, ラウンド番号)

            End If
        Next i


        '進行マスターをヒート表作成済みに変更
        Dim 採点進行 As T_採点進行 = マスタデータ.T_採点進行管理.Get_採点進行Class(区分番号, ラウンド番号)
        採点進行.ステータス = "ヒート表作成済み"
        マスタデータ.T_採点進行管理.登録(採点進行)


        'D_種目マスタを更新　ヒート数
        For d = 1 To 種目数

            Dim 現在種目C As D_種目 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, d)
            現在種目C.ヒート数 = Me.DGV_ヒート数.Rows(0).Cells(現在種目C.種目記号).Value
            マスタデータ.D_種目マスタ.登録(現在種目C)
        Next d

        'U_進行管理を更新
        マスタデータ.U_進行管理.更新()

        'AJSだったら、PCSの確定画面に移動
        If マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) <> "順位法" And
           マスタデータ.C_ラウンドマスタ.Get採点方式(区分番号, ラウンド番号) <> "チェック法" Then

            Dim F512 As F512_PCS担当設定
            F512 = New F512_PCS担当設定

            F512.設定(区分番号, ラウンド番号)
            F512.Show()
            F512.Activate()
            'Me.Close()

        End If


        'チェック・順位だったら、印刷に移動


        マスタデータ = Nothing

    End Sub


    'セルが変更されたら
    Private Sub OnStateChange(ByVal sender As Object, ByVal e As EventArgs)

        'Dim dgv As DataGridView = sender

        'If Me.DGV_競技種目.CurrentCellAddress.X = 0 AndAlso Me.DGV_競技種目.IsCurrentCellDirty Then
        'コミットする
        'Me.DGV_競技種目.CommitEdit(DataGridViewDataErrorContexts.Commit)
        'End If

    End Sub

    'CellValidatingイベントハンドラ 
    Private Sub DGV_ヒート表_CellValidating(ByVal sender As Object, ByVal e As DataGridViewCellValidatingEventArgs) _
        Handles DGV_ヒート表.CellValidating

        Dim dgv As DataGridView = DirectCast(sender, DataGridView)

        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = dgv.NewRowIndex OrElse Not dgv.IsCurrentCellDirty Then
            'Exit Sub
        End If

        Dim 背番号1, 背番号2, c, r

        If dgv.Columns(e.ColumnIndex).Name = "背番号" Then

            背番号1 = Me.DGV_ヒート表.CurrentCell.Value      '編集前の値
            背番号2 = Me.DGV_ヒート表.CurrentCell.EditedFormattedValue  '編集後の値

            c = Me.DGV_ヒート表.CurrentCell.ColumnIndex
            r = Me.DGV_ヒート表.CurrentCell.RowIndex


            '選手名を検索
            If マスタデータ Is Nothing Then
                マスタデータ = New マスタデータ
            End If

            Dim 選手マスタLIST番号 As String = マスタデータ.B_区分マスタ.Get区分C(区分番号).使用する選手マスタ

            Dim 選手C As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手マスタLIST番号, 背番号2)

            Me.DGV_ヒート表.Rows(r).Cells("No").Value = r + 1

            If 選手C Is Nothing Then
                Me.DGV_ヒート表.Rows(r).Cells("名前").Value = ""
            Else
                Me.DGV_ヒート表.Rows(r).Cells("名前").Value = 選手C.リーダー表記名
            End If

        選手C = Nothing
        End If
    End Sub



    'CellValidatedイベントハンドラ 
    Private Sub DGV_ヒート表_CellValidated(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) _
        Handles DGV_ヒート表.CellValidated

        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        'エラーテキストを消す 
        dgv.Rows(e.RowIndex).ErrorText = Nothing
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