Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading

'全体のサイズ　高さ　810

'    種目　　　180
'    じゃっじ　294

Public Class F_GM

    Const ポート番号 = 2345
    Const 関連端末ポート番号 = 2346

    Private _server As Socket
    Public WithEvents TCPServer As TCPServer
    Private WithEvents TCPServer2 As TCPServer2

    Private マスタデータ As マスタデータ

    Private 現在区分番号 As String
    Private 現在ラウンド番号 As String
    Private 現在種目順 As Integer
    Private 現在ヒート番号 As Integer

    Private 現在競技番号 As String
    Private 現在枝番 As String




    Public Client_list(100) As TCPClient
    Private 関連Client_list(100) As TCPClient

    ' LOG は Set_LOG() が呼ばれるまで空インスタンス（出力なし）を使用する
    Private LOG As LOG_C = New LOG_C

    'コンストラクタ
    Private Sub F_GM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '表示位置の設定
        Me.StartPosition = FormStartPosition.Manual
        Me.DesktopLocation = New Point(0, 0)


        TCPServer = New TCPServer

        Try
            Call TCPServer.Listen("0.0.0.0", ポート番号)
        Catch ex As Exception
            MessageBox.Show(Me, "Listenに失敗しました。" + vbLf + "(" + ex.Message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try



        'イベントハンドラを追加
        AddHandler TCPServer.AcceptedClient, AddressOf server_AcceptedClient
        'AddHandler Me.server.DisconnectedClient, AddressOf server_DisconnectClient
        'AddHandler Me.server.ReceivedData, AddressOf server_ReceivedData
        'AddHandler Me.server.LoggedinMember, AddressOf server_LoggedinMember
        'AddHandler Me.server.LoggedoutMember, AddressOf server_LoggedoutMember

        'ジャッジDGVのデフォルトフォント変更
        Me.DGV_ジャッジ.DefaultCellStyle.Font = New Font("MSゴシック", 11, FontStyle.Regular)
        Me.DGV_種目.DefaultCellStyle.Font = New Font("MSゴシック", 11, FontStyle.Regular)
        Me.DGV_ヒート.DefaultCellStyle.Font = New Font("MSゴシック", 11, FontStyle.Regular)



        マスタデータ = New マスタデータ

        競技一覧の更新(マスタデータ)

        '次の競技を取得
        Dim 競技番号_枝番 As String = マスタデータ.T_採点進行管理.Get_次競技番号("")

        DGV更新(マスタデータ, 競技番号_枝番)

        'タイマ
        タイマ初期設定()

        'DGVが選択されていない状態にする
        Me.DGV_ジャッジ.CurrentCell = Nothing
        Me.DGV_種目.CurrentCell = Nothing
        Me.DGV_ヒート.CurrentCell = Nothing

        ' Set_LOG が Show() より前に呼ばれた場合、TCPServer への LOG 設定が
        ' スキップされているため、ここで改めてセットする
        TCPServer.Set_LOG(LOG)
    End Sub

    'コンボボックスが選択された時
    Private Sub CB_進行番号_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_進行番号.SelectedIndexChanged

        Dim ItemValue = Me.CB_進行番号.SelectedIndex
        Dim ItemString = Me.CB_進行番号.SelectedItem

        現在競技番号 = Strings.Left(ItemString, 3)
        現在枝番 = Strings.Mid(ItemString, 5, 2)

        DGV更新(マスタデータ, 現在競技番号 & 現在枝番)

        'ラベルの表示
        Dim 区分番号 As String = ""
        Dim ラウンド番号 As String = ""

        マスタデータ.T_採点進行管理.Get_区分ラウンド番号(現在競技番号, 現在枝番, 区分番号, ラウンド番号)

        Me.LB_区分名.Text = マスタデータ.B_区分マスタ.Get区分表記名(区分番号)
        Me.LB_ラウンド.Text = マスタデータ.Get_ラウンド名(ラウンド番号)

        'DGVが選択されていない状態にする
        Me.DGV_ジャッジ.CurrentCell = Nothing
        Me.DGV_種目.CurrentCell = Nothing
        Me.DGV_ヒート.CurrentCell = Nothing


        'タイマー２を止める
        更新タイマーStop()


        J_LOGIN色付け()

    End Sub

    'コンボボックスの更新
    Private Sub 競技一覧の更新(マスタデータ As マスタデータ)

        マスタデータ.T_採点進行管理.FileRead()

        '一旦全て消す
        Me.CB_進行番号.Items.Clear()

        Dim 初期_競技番号_枝番 As String = マスタデータ.T_採点進行管理.Get_次競技番号("")
        Dim 初期_競技番号_item番号 As Integer = 0
        Dim Item数 As Integer = 0

        For i = 1 To マスタデータ.T_採点進行管理.登録済みレコード数
            If マスタデータ.T_採点進行管理.リスト(i).ステータス = "ヒート表作成済み" Then
                ' or マスタデータ.T_採点進行管理.リスト(i).ステータス = "採点済み" Then

                Dim 競技番号 = マスタデータ.T_採点進行管理.リスト(i).競技番号
                Dim 枝番 = マスタデータ.T_採点進行管理.リスト(i).競技番号枝番
                Dim 区分名 = マスタデータ.B_区分マスタ.Get区分表記名(マスタデータ.T_採点進行管理.リスト(i).区分番号)
                Dim ラウンド名 = マスタデータ.Get_ラウンド名(マスタデータ.T_採点進行管理.リスト(i).ラウンド番号)
                Me.CB_進行番号.Items.Add(競技番号 & "-" & 枝番 & " " & 区分名 & " " & ラウンド名)
                Item数 = Item数 + 1

                If 初期_競技番号_枝番 = 競技番号 & 枝番 Then
                    初期_競技番号_item番号 = Item数
                End If

            End If
        Next i

        Me.CB_進行番号.SelectedIndex = 初期_競技番号_item番号 - 1


    End Sub

    Private Sub 次ヒート()
        '次のヒートに更新する。

        ' 全SEND後処理済みフラグをリセット
        全SEND処理済FLAG = False
        LOG.LogAdd("次ヒート: 全SEND処理済FLAGをリセット", 4)

        Dim 種目記号リスト() = Nothing
        Dim 採点方式 As String = ""
        Dim SG種別 As String = ""

        Get次ヒート(採点方式, SG種別, 種目記号リスト)
        '現在種目順と現在ヒート番号　を更新


        'ヒート表の作成
        DGV_ヒートの更新(マスタデータ, 現在区分番号, 現在ラウンド番号, 現在種目順)

        '審判員リストの作成
        DGV_ジャッジの更新(マスタデータ, 現在区分番号, 現在ラウンド番号)


        'ジャッジの表示画面（種目・ヒート）を表示するため、
        J_LOGIN色付け()


        J_Send色付け(現在種目順, 現在ヒート番号)



        'DGVが選択されていない状態にする
        Me.DGV_ジャッジ.CurrentCell = Nothing
        Me.DGV_種目.CurrentCell = Nothing
        Me.DGV_ヒート.CurrentCell = Nothing


        '現在種目とヒート番号を更新
        Me.LB_現在種目ヒート.Text = "現在種目：" & 現在種目順 & " Heat：" & 現在ヒート番号



        'If Strings.Left(採点方式, 4) = "BJS2" And 現在種目順 = 2 And マスタデータ.D_種目マスタ.Get_種目数(現在区分番号, 現在ラウンド番号, 種目記号リスト) >= 3 Then
        If (Strings.Left(採点方式, 4) = "BJS2" Or Strings.Left(採点方式, 4) = "BJS3") And 現在種目順 < マスタデータ.D_種目マスタ.Get_種目数(現在区分番号, 現在ラウンド番号, 種目記号リスト) And 現在種目順 Mod 2 = 0 And SG種別 <> "S" Then
            'Me.Invoke(次ヒート_Delegate, New Object() {})
            次ヒート()
            '更新タイマーStart()    '本当はこちらにしたいが、10秒後にTickが呼ばれないため、やむを得ず「次ヒート」で処理する
        End If


    End Sub

    Private Sub Get次ヒート(ByRef 採点方式_, ByRef SG種別_, ByRef 種目記号リスト_())

        '次ヒート番号を取得する

        '次のヒート番号（種目番号）を確定する
        Dim 次種目番号 As Integer = 0
        Dim 次ヒート番号 As Integer = 0

        Dim 種目記号リスト() = Nothing

        Dim 採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号)


        Dim ヒート数 As Integer = 0
        ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(現在種目順)

        Dim SG種別 = マスタデータ.D_種目マスタ.Get_種目Class(現在区分番号, 現在ラウンド番号, 現在種目順).SG種別

        If (Strings.Left(採点方式, 4) = "BJS2" Or Strings.Left(採点方式, 4) = "BJS3") And SG種別 <> "S" Then
            'BJS2の時は、必ずヒート数は１  ソロは除く
            ヒート数 = 1
        End If


        If ヒート数 = 0 Then
            'エラー
        End If



        If ヒート数 > 現在ヒート番号 And 採点方式 <> "チェック法" And 採点方式 <> "順位法" Then
            'その種目の次のヒート

            '次種目番号 = CInt(現在種目順)
            現在ヒート番号 = 現在ヒート番号 + 1

        ElseIf 現在種目順 >= マスタデータ.D_種目マスタ.Get_種目数(現在区分番号, 現在ラウンド番号, 種目記号リスト) And
              現在ヒート番号 >= ヒート数 Then
            '最終種目の最終ヒートが終わったあと
            '何もしない


        Else
            '次の種目の1ヒート

            次種目番号 = CInt(現在種目順) + 1

            マスタデータ.E_ヒート表マスタ.Read(現在区分番号, 現在ラウンド番号)
            Dim 次種目ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(CStr(次種目番号))

            If 次種目ヒート数 = 0 Then
                'そのラウンドの終わり
                次ヒート番号 = 0
            Else
                次ヒート番号 = 1
                ヒート数 = 次種目ヒート数
            End If

            現在種目順 = 次種目番号
            現在ヒート番号 = 次ヒート番号

            DGV_種目色付け()
        End If


        採点方式_ = 採点方式
        SG種別_ = SG種別
        種目記号リスト_ = 種目記号リスト

    End Sub

    Private Sub 指定ヒート()

        ' 全SEND後処理済みフラグをリセット
        全SEND処理済FLAG = False
        LOG.LogAdd("指定ヒート: 全SEND処理済FLAGをリセット", 4)

        Dim 選択種目行 As Integer
        '現在選択されている行
        For Each c As DataGridViewCell In DGV_種目.SelectedCells
            選択種目行 = c.RowIndex + 1
        Next c
        Dim 選択ヒート行 As Integer
        For Each c As DataGridViewCell In DGV_ヒート.SelectedCells
            選択ヒート行 = c.RowIndex + 1
        Next c


        If 選択種目行 = 0 Then
            MsgBox("種目を選択してください。")
            Exit Sub
        End If
        If 選択ヒート行 = 0 Then
            MsgBox("ヒートを選択してください。")
            Exit Sub
        End If


        現在種目順 = 選択種目行
        現在ヒート番号 = 選択ヒート行

        Dim 種目記号リスト() = Nothing
        Dim 採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号)
        Dim SG種別 = マスタデータ.D_種目マスタ.Get_種目Class(現在区分番号, 現在ラウンド番号, 現在種目順).SG種別



        'ヒート表の作成
        DGV_ヒートの更新(マスタデータ, 現在区分番号, 現在ラウンド番号, 現在種目順)

        '審判員リストの作成
        DGV_ジャッジの更新(マスタデータ, 現在区分番号, 現在ラウンド番号)


        'ジャッジの表示画面（種目・ヒート）を表示するため、
        J_LOGIN色付け()


        J_Send色付け(現在種目順, 現在ヒート番号)



        'DGVが選択されていない状態にする
        Me.DGV_ジャッジ.CurrentCell = Nothing
        Me.DGV_種目.CurrentCell = Nothing
        Me.DGV_ヒート.CurrentCell = Nothing


        '現在種目とヒート番号を更新
        Me.LB_現在種目ヒート.Text = "現在種目：" & 現在種目順 & " Heat：" & 現在ヒート番号



        If (Strings.Left(採点方式, 4) = "BJS2" Or Strings.Left(採点方式, 4) = "BJS3") And 現在種目順 < マスタデータ.D_種目マスタ.Get_種目数(現在区分番号, 現在ラウンド番号, 種目記号リスト) And 現在種目順 Mod 2 = 0 And SG種別 <> "S" Then
            次ヒート()
        End If


    End Sub

    Private Sub 前ヒート()
        '前のヒートに更新する。

        ' 全SEND後処理済みフラグをリセット
        全SEND処理済FLAG = False
        LOG.LogAdd("前ヒート: 全SEND処理済FLAGをリセット", 4)


        '前のヒート番号（種目番号）を確定する
        Dim 前種目番号 As Integer = 0
        Dim 前ヒート番号 As Integer = 0

        Dim 種目記号リスト() = Nothing

        Dim ヒート数 As Integer = 0
        ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(現在種目順)


        If ヒート数 = 0 Then
            'エラー
        End If

        If 現在ヒート番号 > 1 Then
            'その種目の前のヒート

            現在ヒート番号 = 現在ヒート番号 - 1

        ElseIf 現在種目順 = 1 And
              現在ヒート番号 = 1 Then
            '最初種目の最初ヒートの時
            '何もしない


        Else
            '前の種目の最終ヒート

            前種目番号 = CInt(現在種目順) - 1

            マスタデータ.E_ヒート表マスタ.Read(現在区分番号, 現在ラウンド番号)
            Dim 前種目ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(CStr(前種目番号))


            If 前種目ヒート数 = 0 Then
                'そのラウンドの終わり
                前ヒート番号 = 0
            Else
                前ヒート番号 = 前種目ヒート数
                ヒート数 = 前種目ヒート数
            End If

            現在種目順 = 前種目番号
            現在ヒート番号 = 前ヒート番号

            DGV_種目色付け()
        End If


        'ヒート表の作成
        DGV_ヒートの更新(マスタデータ, 現在区分番号, 現在ラウンド番号, 現在種目順)

        '審判員リストの作成
        DGV_ジャッジの更新(マスタデータ, 現在区分番号, 現在ラウンド番号)


        'ジャッジの表示画面（種目・ヒート）を表示するため、
        J_LOGIN色付け()


        J_Send色付け(現在種目順, 現在ヒート番号)


        '現在種目とヒート番号を更新
        Me.LB_現在種目ヒート.Text = "現在種目：" & 現在種目順 & " Heat：" & 現在ヒート番号

        'DGVが選択されていない状態にする
        Me.DGV_ジャッジ.CurrentCell = Nothing
        Me.DGV_種目.CurrentCell = Nothing
        Me.DGV_ヒート.CurrentCell = Nothing

    End Sub


    'DGV全体の更新
    Private Sub DGV更新(マスタデータ As マスタデータ, 競技番号_枝番 As String)


        Dim 競技番号 As String = ""
        Dim 枝番 As String = ""
        Dim 区分番号 As String = ""
        Dim ラウンド番号 As String = ""
        Dim 区分名 As String = ""
        Dim ラウンド名 As String = ""

        If 競技番号_枝番 <> "" Then
            競技番号 = Strings.Left(競技番号_枝番, 競技番号_枝番.Length - 2)
            枝番 = Strings.Right(競技番号_枝番, 2)

            マスタデータ.T_採点進行管理.Get_区分ラウンド番号(競技番号, 枝番, 区分番号, ラウンド番号）
            区分名 = マスタデータ.B_区分マスタ.Get区分表記名(区分番号)
            ラウンド名 = マスタデータ.Get_ラウンド名(ラウンド番号)

            ' Me.CB_進行番号.Text = 競技番号 & " " & 区分名 & " " & ラウンド名

            現在区分番号 = 区分番号
            現在ラウンド番号 = ラウンド番号
            現在種目順 = 1
            現在ヒート番号 = 1


            マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)

            '種目リストの作成
            DGV_種目の更新(マスタデータ, 区分番号, ラウンド番号)

            'ヒート表の作成
            DGV_ヒートの更新(マスタデータ, 区分番号, ラウンド番号, 1)

            '審判員リストの作成
            DGV_ジャッジの更新(マスタデータ, 区分番号, ラウンド番号)

            J_Send色付け(現在種目順, 現在ヒート番号)


        End If

        Me.LB_現在種目ヒート.Text = "現在種目;" & 現在種目順 & " Heat：" & 現在ヒート番号

    End Sub

    'DGV_種目の更新
    Private Sub DGV_種目の更新(マスタデータ As マスタデータ, 区分番号 As String, ラウンド番号 As String)

        Me.DGV_種目.Rows.Clear()

        Dim 種目記号リスト() = Nothing
        For s = 1 To マスタデータ.D_種目マスタ.Get_種目数(区分番号, ラウンド番号, 種目記号リスト)
            Me.DGV_種目.Rows.Add()
            Me.DGV_種目.Rows(s - 1).Cells(0).Value = s
            Dim 種目 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, s)
            If 種目.SG種別 = "S" Then
                Me.DGV_種目.Rows(s - 1).Cells(1).Value = 種目.種目記号 & "(Solo)"
            ElseIf 種目.SG種別 = "G" Then
                Me.DGV_種目.Rows(s - 1).Cells(1).Value = 種目.種目記号 & "(Group)"
            ElseIf 種目.SG種別 = "D" Then
                Me.DGV_種目.Rows(s - 1).Cells(1).Value = 種目.種目記号 & "(Duel)"
            ElseIf 種目.種目記号 <> "" Then
                Me.DGV_種目.Rows(s - 1).Cells(1).Value = 種目.種目記号
            End If
        Next s

        DGV_種目色付け()

    End Sub

    'DGV_ヒートの更新
    Private Sub DGV_ヒートの更新(マスタデータ As マスタデータ, 区分番号 As String, ラウンド番号 As String, 種目順 As Integer)

        'クリア
        Me.DGV_ヒート.Rows.Clear()

        'マスタデータ.E_ヒート表マスタ.Read(区分番号, ラウンド番号)
        Dim 種目ヒート数 As Integer = 0

        Dim 採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号)

        Dim SG種別 = マスタデータ.D_種目マスタ.Get_種目Class(区分番号, ラウンド番号, 種目順).SG種別

        If (Strings.Left(採点方式, 4) = "BJS2" Or Strings.Left(採点方式, 4) = "BJS3") And SG種別 <> "S" Then
            'BJSの時は、必ずヒート数は１            '（ソロは除く）

            Me.DGV_ヒート.Rows.Add()

            Dim h = 1
            Me.DGV_ヒート.Rows(h - 1).Cells(0).Value = h

            Dim 背番号リスト() = Nothing

            種目ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(種目順)
            Dim 背番号 As String = ""

            For h = 1 To 種目ヒート数
                マスタデータ.E_ヒート表マスタ.Get_背番号リスト(種目順, h, 背番号リスト)
                For s = 1 To UBound(背番号リスト)
                    背番号 = 背番号 & 背番号リスト(s) & " "
                Next s
            Next h

            h = 1
            Me.DGV_ヒート.Rows(h - 1).Cells(1).Value = 背番号




        Else
            種目ヒート数 = マスタデータ.E_ヒート表マスタ.Getヒート数(種目順)

            For h = 1 To 種目ヒート数
                Me.DGV_ヒート.Rows.Add()

                Me.DGV_ヒート.Rows(h - 1).Cells(0).Value = h

                Dim 背番号リスト() = Nothing
                マスタデータ.E_ヒート表マスタ.Get_背番号リスト(種目順, h, 背番号リスト)
                Dim 背番号 As String = ""
                For s = 1 To UBound(背番号リスト)
                    背番号 = 背番号 & 背番号リスト(s) & " "
                Next s
                Me.DGV_ヒート.Rows(h - 1).Cells(1).Value = 背番号
            Next h

        End If





        DGV_ヒート色付け(種目順)

    End Sub


    'DGV_ジャッジの更新
    Private Sub DGV_ジャッジの更新(マスタデータ As マスタデータ, 区分番号 As String, ラウンド番号 As String）

        'クリア
        Me.DGV_ジャッジ.Rows.Clear()


        Dim ジャッジ数 As Integer = 0
        For j = 1 To マスタデータ.審判員マスタ.Get_登録済み審判員数

            If マスタデータ.審判員マスタ.審判員リスト(j).審判チーム(マスタデータ.C_ラウンドマスタ.Get担当審判グループ(区分番号, ラウンド番号)) <> "" Then

                Me.DGV_ジャッジ.Rows.Add()

                Me.DGV_ジャッジ.Rows(ジャッジ数).Cells(0).Value = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ記号    'ジャッジ記号

                Me.DGV_ジャッジ.Rows(ジャッジ数).Cells(1).Value = マスタデータ.審判員マスタ.審判員リスト(j).ジャッジ表記名   'ジャッジ名





                ジャッジ数 = ジャッジ数 + 1

            End If


        Next j


    End Sub


    'DGV_種目の色付け
    Private Sub DGV_種目色付け()
        '現在種目は、黄色　LightGoldenRodYellow
        '終了種目はブルー　LightSkyBlue


        マスタデータ.U_進行管理.FileRead()


        'DGVを上から検索
        For i = 0 To Me.DGV_種目.RowCount - 1
            '現在種目を黄色に
            If Me.DGV_種目.Rows(i).Cells(0).Value = 現在種目順 Then
                Me.DGV_種目.Rows(i).Cells(0).Style.BackColor = Color.LightGoldenrodYellow
                Me.DGV_種目.Rows(i).Cells(1).Style.BackColor = Color.LightGoldenrodYellow
            Else
                Me.DGV_種目.Rows(i).Cells(0).Style.BackColor = Nothing
                Me.DGV_種目.Rows(i).Cells(1).Style.BackColor = Nothing
            End If

            'ヒート数分 U_進行管理を検索する
            Dim 採点終了FLAG As Boolean = True
            Dim 種目ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(Me.DGV_種目.Rows(i).Cells(0).Value)
            For h = 1 To 種目ヒート数
                If マスタデータ.U_進行管理.Get_進行(現在競技番号, 現在枝番, Me.DGV_種目.Rows(i).Cells(0).Value, h) IsNot Nothing Then
                    If マスタデータ.U_進行管理.Get_進行(現在競技番号, 現在枝番, Me.DGV_種目.Rows(i).Cells(0).Value, h).ステータス <> "全審判送信済み" Then
                        採点終了FLAG = False
                        h = 種目ヒート数
                    End If
                End If
            Next h

            '採点終了種目をブルーに
            If 採点終了FLAG = True And 種目ヒート数 > 0 Then
                Me.DGV_種目.Rows(i).Cells(0).Style.BackColor = Color.LightSkyBlue
                Me.DGV_種目.Rows(i).Cells(1).Style.BackColor = Color.LightSkyBlue

            End If

        Next i

    End Sub

    'DGV_ヒートの色付け
    Private Sub DGV_ヒート色付け(対象種目順 As Integer)
        '現在ヒートは、黄色　LightGoldenRodYellow
        '終了ヒートはブルー　LightSkyBlue

        'DGVを上から検索
        For i = 0 To Me.DGV_ヒート.RowCount - 1
            '現在ヒートを黄色に
            If Me.DGV_ヒート.Rows(i).Cells(0).Value = 現在ヒート番号 Then
                Me.DGV_ヒート.Rows(i).Cells(0).Style.BackColor = Color.LightGoldenrodYellow
                Me.DGV_ヒート.Rows(i).Cells(1).Style.BackColor = Color.LightGoldenrodYellow
            Else
                Me.DGV_ヒート.Rows(i).Cells(0).Style.BackColor = Nothing
                Me.DGV_ヒート.Rows(i).Cells(1).Style.BackColor = Nothing

            End If

            If マスタデータ.U_進行管理.Get_進行(現在競技番号, 現在枝番, 対象種目順, Me.DGV_ヒート.Rows(i).Cells(0).Value) IsNot Nothing Then
                If マスタデータ.U_進行管理.Get_進行(現在競技番号, 現在枝番, 対象種目順, Me.DGV_ヒート.Rows(i).Cells(0).Value).ステータス = "全審判送信済み" Then
                    Me.DGV_ヒート.Rows(i).Cells(0).Style.BackColor = Color.LightSkyBlue
                    Me.DGV_ヒート.Rows(i).Cells(1).Style.BackColor = Color.LightSkyBlue

                End If
            End If

        Next i


    End Sub



    'DGVのジャッジ ログイン状態の色付け
    Private Sub J_LOGIN色付け()

        For i = 0 To Me.DGV_ジャッジ.Rows.Count - 1
            Dim ジャッジ記号 = Me.DGV_ジャッジ.Rows(i).Cells(0).Value

            Dim FindFlag As Boolean = False
            For l = 1 To UBound(Client_list)
                If Client_list(l) IsNot Nothing Then
                    If ジャッジ記号 = Client_list(l).ジャッジ記号 Then
                        If 現在ラウンド番号 = Client_list(l).ラウンド番号 And
                            現在区分番号 = Client_list(l).区分番号 Then

                            'ログイン中
                            'ログインの色付け
                            Me.DGV_ジャッジ.Rows(i).Cells(0).Style.BackColor = Color.GreenYellow
                            '種目記号、ヒート番号をセット
                            Me.DGV_ジャッジ.Rows(i).Cells(2).Value = Client_list(l).種目記号
                            Me.DGV_ジャッジ.Rows(i).Cells(3).Value = Client_list(l).ヒート番号

                            FindFlag = True
                        End If
                    End If
                End If
            Next l

            If FindFlag = False Then

                'ログインの状態の色を消す
                Me.DGV_ジャッジ.Rows(i).Cells(0).Style.BackColor = Nothing

                '種目、ヒートを消す
                Me.DGV_ジャッジ.Rows(i).Cells(2).Value = ""
                Me.DGV_ジャッジ.Rows(i).Cells(3).Value = ""


            End If

        Next i

    End Sub

    Private Sub J_HELP色付け(ジャッジ記号 As String, ONOFF As String)
        'HELPジャッジを赤く設定
        'ONOFF = "ON" or "OFF"


        For i = 0 To Me.DGV_ジャッジ.Rows.Count - 1
            If ジャッジ記号 = DGV_ジャッジ.Rows(i).Cells(0).Value Then
                If ONOFF = "ON" Then
                    'ログインの色付け
                    Me.DGV_ジャッジ.Rows(i).Cells(0).Style.BackColor = Color.Red
                Else
                    Me.DGV_ジャッジ.Rows(i).Cells(0).Style.BackColor = Color.GreenYellow
                End If
                i = Me.DGV_ジャッジ.Rows.Count - 1
            End If
        Next i

    End Sub


    'DGVジャッジ SEND状態の色付け
    Private Sub J_Send色付け(対象種目順 As Integer, 対象ヒート番号 As Integer)
        '

        'DGVに表示されているジャッジ記号リストを入手
        Dim ジャッジ記号() As String
        Dim ジャッジ人数 As Integer

        'ジャッジ人数 = Me.DGV_ジャッジ.RowCount
        ジャッジ人数 = 0
        For j = 0 To Me.DGV_ジャッジ.RowCount - 1
            If Me.DGV_ジャッジ.Rows(j).Cells(0).Value IsNot Nothing Then
                ジャッジ人数 = ジャッジ人数 + 1
            End If
        Next j

        If ジャッジ人数 = 0 Then
            'ジャッジがいないときは、抜ける
            Exit Sub
        End If

        ReDim ジャッジ記号(ジャッジ人数)
        For j = 0 To ジャッジ人数 - 1
            ジャッジ記号(j + 1) = Me.DGV_ジャッジ.Rows(j).Cells(0).Value
        Next j

        'Send状態の確認
        Dim 種目 = マスタデータ.D_種目マスタ.Get_種目Class(現在区分番号, 現在ラウンド番号, 対象種目順)
        Dim 対象種目記号 As String = 種目.種目記号

        Dim 全SENDFLAG As Boolean = True

        Dim SG種別 = マスタデータ.D_種目マスタ.Get_種目Class(現在区分番号, 現在ラウンド番号, 対象種目順).SG種別


        If マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号) = ”チェック法" Then

            Dim S_採点結果_J As S_採点結果_J
            S_採点結果_J = New S_採点結果_J(マスタデータ.Z_システム設定.Comp_filepath)
            S_採点結果_J.区分番号 = 現在区分番号
            S_採点結果_J.ラウンド番号 = 現在ラウンド番号
            S_採点結果_J.種目記号 = 対象種目記号


            For j = 1 To ジャッジ人数
                S_採点結果_J.ジャッジ記号 = ジャッジ記号(j)
                S_採点結果_J = S_採点結果_J.新JSON読み込み


                If S_採点結果_J.Get_選手数 > 0 Then
                    If S_採点結果_J.SEND_FLAG = "1" Then
                        Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Color.Cyan
                    Else
                        '未SENDのジャッジがいる時
                        Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                        全SENDFLAG = False
                    End If
                End If
            Next j


        ElseIf Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号), 4) = "BJPR" Then

            '対象ヒートの　一人目の背番号を探す
            Dim 背番号リスト() = Nothing
            マスタデータ.E_ヒート表マスタ.Read(現在区分番号, 現在ラウンド番号)
            マスタデータ.E_ヒート表マスタ.Get_背番号リスト(対象種目順, 対象ヒート番号, 背番号リスト)
            Dim 背番号 As String = 背番号リスト(1)



            For j = 1 To ジャッジ人数
                Dim S_採点結果_J As S_採点結果_J
                S_採点結果_J = New S_採点結果_J(マスタデータ.Z_システム設定.Comp_filepath)
                S_採点結果_J.区分番号 = 現在区分番号
                S_採点結果_J.ラウンド番号 = 現在ラウンド番号
                S_採点結果_J.種目記号 = 対象種目記号
                S_採点結果_J.ジャッジ記号 = ジャッジ記号(j)

                S_採点結果_J = S_採点結果_J.新JSON読み込み

                If S_採点結果_J IsNot Nothing Then

                    If S_採点結果_J.Get_選手数 > 0 Then

                        Dim 結果無FLAG As Boolean = True
                        For s = 1 To S_採点結果_J.Get_選手数
                            If S_採点結果_J.S_採点結果_選手_J(s).ヒート番号 = 対象ヒート番号 Then
                                If S_採点結果_J.S_採点結果_選手_J(s).SEND_FLAG = "1" Then

                                    Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Color.Cyan

                                Else
                                    '未SENDのジャッジがいる時
                                    Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                                    全SENDFLAG = False
                                End If
                                s = S_採点結果_J.Get_選手数
                                結果無FLAG = False
                            End If
                        Next s

                        If 結果無FLAG = True Then
                            'まだそのヒートの結果をSENDしていない場合
                            全SENDFLAG = False
                        End If

                    Else
                        '採点結果無い時
                        Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                        全SENDFLAG = False

                    End If
                Else
                    Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                    全SENDFLAG = False
                End If
            Next j


        ElseIf (Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号), 4) = "BJS2" Or
               Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号), 4) = "BJS3") Then


            Dim S_採点結果_J As S_採点結果_BR2_J
            S_採点結果_J = New S_採点結果_BR2_J(マスタデータ.Z_システム設定.Comp_filepath)
            S_採点結果_J.区分番号 = 現在区分番号
            S_採点結果_J.ラウンド番号 = 現在ラウンド番号

            マスタデータ.J_新審判設定.Set_新審判基準VER(マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号))

            If SG種別 = "S" Then
                'ソロの時

                S_採点結果_J.種目記号 = 対象種目記号

                '一人目の背番号を探す
                'Dim 背番号リスト() = Nothing
                'マスタデータ.E_ヒート表マスタ.Read(現在区分番号, 現在ラウンド番号)
                'マスタデータ.E_ヒート表マスタ.Get_背番号リスト(対象種目順, 対象ヒート番号, 背番号リスト)
                'Dim 背番号 As String = 背番号リスト(1)


                For j = 1 To ジャッジ人数
                    S_採点結果_J.ジャッジ記号 = ジャッジ記号(j)
                    If S_採点結果_J.JSON読み込み() > 0 Then

                        If S_採点結果_J.BR2_種目結果_J(対象種目順).BR2_選手結果_J(対象ヒート番号).SEND_FLAG = "1" Then
                            '範囲チェック追加
                            If j > 0 AndAlso j <= Me.DGV_ジャッジ.Rows.Count Then
                                Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Color.Cyan
                            Else
                                LOG.LogAdd($"インデックス範囲外(BR2_SEND): j={j}, Rows.Count={Me.DGV_ジャッジ.Rows.Count}", 1)
                            End If
                        Else
                            '未SENDのジャッジがいる時（範囲チェック追加）
                            If j > 0 AndAlso j <= Me.DGV_ジャッジ.Rows.Count Then
                                Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                            Else
                                LOG.LogAdd($"インデックス範囲外(BR2_未SEND): j={j}, Rows.Count={Me.DGV_ジャッジ.Rows.Count}", 1)
                            End If
                            全SENDFLAG = False
                        End If
                    Else
                        '一度もデータを送っていないジャッジが居るとき（範囲チェック追加）
                        If j > 0 AndAlso j <= Me.DGV_ジャッジ.Rows.Count Then
                            Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                        Else
                            LOG.LogAdd($"インデックス範囲外(BR2_データなし): j={j}, Rows.Count={Me.DGV_ジャッジ.Rows.Count}", 1)
                        End If
                        全SENDFLAG = False
                    End If
                Next j


            Else
                'ソロじゃ無い時


                If 対象種目順 Mod 2 = 0 And 対象種目順 <= マスタデータ.J_新審判設定.勝敗ラウンド数 Then
                    種目 = マスタデータ.D_種目マスタ.Get_種目Class(現在区分番号, 現在ラウンド番号, 対象種目順 - 1)
                    対象種目記号 = 種目.種目記号
                End If

                S_採点結果_J.種目記号 = 対象種目記号


                For j = 1 To ジャッジ人数

                    S_採点結果_J.ジャッジ記号 = ジャッジ記号(j)
                    If S_採点結果_J.JSON読み込み() > 0 Then

                        If S_採点結果_J.SEND_FLAG = "1" Then
                            '範囲チェック追加
                            If j > 0 AndAlso j <= Me.DGV_ジャッジ.Rows.Count Then
                                Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Color.Cyan
                            Else
                                LOG.LogAdd($"インデックス範囲外(BR2_非ソロ_SEND): j={j}, Rows.Count={Me.DGV_ジャッジ.Rows.Count}", 1)
                            End If
                        Else
                            '未SENDのジャッジがいる時（範囲チェック追加）
                            If j > 0 AndAlso j <= Me.DGV_ジャッジ.Rows.Count Then
                                Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                            Else
                                LOG.LogAdd($"インデックス範囲外(BR2_非ソロ_未SEND): j={j}, Rows.Count={Me.DGV_ジャッジ.Rows.Count}", 1)
                            End If
                            全SENDFLAG = False
                        End If
                    Else
                        '一度もデータを送っていないジャッジが居るとき（範囲チェック追加）
                        If j > 0 AndAlso j <= Me.DGV_ジャッジ.Rows.Count Then
                            Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                        Else
                            LOG.LogAdd($"インデックス範囲外(BR2_非ソロ_データなし): j={j}, Rows.Count={Me.DGV_ジャッジ.Rows.Count}", 1)
                        End If
                        全SENDFLAG = False
                    End If
                Next j

            End If


        ElseIf Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号), 3) = "PDJ" Or
               Strings.Left(マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号), 3) = "VAL" Then

            '一人目の背番号を探す
            Dim 背番号リスト() = Nothing
            マスタデータ.E_ヒート表マスタ.Read(現在区分番号, 現在ラウンド番号)
            マスタデータ.E_ヒート表マスタ.Get_背番号リスト(対象種目順, 対象ヒート番号, 背番号リスト)
            Dim 背番号 As String = 背番号リスト(1)


            Dim S_採点結果 As S_採点結果_V2_J

            For j = 1 To ジャッジ人数
                S_採点結果 = New S_採点結果_V2_J(マスタデータ.Z_システム設定.Comp_filepath)
                S_採点結果.Set_LOG(LOG)

                S_採点結果.区分番号 = 現在区分番号
                S_採点結果.ラウンド番号 = 現在ラウンド番号
                S_採点結果.現種目記号 = 対象種目記号
                S_採点結果.ジャッジ記号 = ジャッジ記号(j)


                'If S_採点結果.Read(現在区分番号, 現在ラウンド番号, 対象種目記号, ジャッジ記号(j)) > 0 Then

                S_採点結果 = S_採点結果.JSON読み込み()

                If S_採点結果 IsNot Nothing Then
                    '採点ファイルが無い時は処理しない
                    '採点ファイルからSENDFLAGを探す
                    Dim 背番号存在FLAG As Boolean = False


                    For s = 1 To S_採点結果.選手数
                        If 背番号 = S_採点結果.S_採点結果_選手_J(s).背番号 Then

                            'SENDFLAGの確認
                            If S_採点結果.S_採点結果_選手_J(s).SEND_FLAG = 1 Then
                                'ジャッジセルを青にする（範囲チェック追加）
                                If j > 0 AndAlso j <= Me.DGV_ジャッジ.Rows.Count Then
                                    Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Color.Cyan
                                Else
                                    LOG.LogAdd($"インデックス範囲外(SEND): j={j}, Rows.Count={Me.DGV_ジャッジ.Rows.Count}", 1)
                                End If
                            Else
                                '未SENDのジャッジがいる時（範囲チェック追加）
                                If j > 0 AndAlso j <= Me.DGV_ジャッジ.Rows.Count Then
                                    Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                                Else
                                    LOG.LogAdd($"インデックス範囲外(未SEND): j={j}, Rows.Count={Me.DGV_ジャッジ.Rows.Count}", 1)
                                End If

                                'ソロの技術判定員以外は問題無し
                                If マスタデータ.審判員マスタ.Get_審判Class(ジャッジ記号(j)).審判チーム(マスタデータ.C_ラウンドマスタ.Get担当審判グループ(現在区分番号, 現在ラウンド番号)) = "T" Then
                                    If SG種別 = "S" Then
                                        全SENDFLAG = False
                                    Else


                                    End If
                                Else
                                    全SENDFLAG = False
                                End If

                            End If
                            背番号存在FLAG = True
                            s = S_採点結果.選手数
                        End If
                    Next s

                    If 背番号存在FLAG = False Then
                        '範囲チェック追加
                        If j > 0 AndAlso j <= Me.DGV_ジャッジ.Rows.Count Then
                            Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                        Else
                            LOG.LogAdd($"インデックス範囲外(背番号不存在): j={j}, Rows.Count={Me.DGV_ジャッジ.Rows.Count}", 1)
                        End If
                        全SENDFLAG = False
                    End If

                Else
                    '範囲チェック追加
                    If j > 0 AndAlso j <= Me.DGV_ジャッジ.Rows.Count Then
                        Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                    Else
                        LOG.LogAdd($"インデックス範囲外(採点結果なし): j={j}, Rows.Count={Me.DGV_ジャッジ.Rows.Count}", 1)
                    End If
                    '採点ファイルが存在しないとき

                    '技術判定員は、ソロの時だけ無し
                    If マスタデータ.審判員マスタ.Get_審判Class(ジャッジ記号(j)).審判チーム(マスタデータ.C_ラウンドマスタ.Get担当審判グループ(現在区分番号, 現在ラウンド番号)) = "T" Then
                        If SG種別 = "S" Then
                            全SENDFLAG = False
                        Else


                        End If
                    Else
                        全SENDFLAG = False
                    End If

                End If
            Next j



        Else

            '一人目の背番号を探す
            Dim 背番号リスト() = Nothing
            マスタデータ.E_ヒート表マスタ.Read(現在区分番号, 現在ラウンド番号)
            マスタデータ.E_ヒート表マスタ.Get_背番号リスト(対象種目順, 対象ヒート番号, 背番号リスト)
            Dim 背番号 As String = 背番号リスト(1)


            Dim S_採点結果 As S_採点結果

            For j = 1 To ジャッジ人数
                S_採点結果 = New S_採点結果(マスタデータ.Z_システム設定.Comp_filepath)
                If S_採点結果.Read(現在区分番号, 現在ラウンド番号, 対象種目記号, ジャッジ記号(j)) > 0 Then
                    '採点ファイルが無い時は処理しない
                    '採点ファイルからSENDFLAGを探す
                    Dim 背番号存在FLAG As Boolean = False
                    For s = 1 To S_採点結果.登録済みレコード数
                        If 背番号 = S_採点結果.リスト(s).背番号 Then

                            'SENDFLAGの確認
                            If S_採点結果.リスト(s).SEND_FLAG = 1 Then
                                'ジャッジセルを青にする
                                Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Color.Cyan
                            Else
                                '未SENDのジャッジがいる時
                                Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                                全SENDFLAG = False
                            End If
                            背番号存在FLAG = True
                            s = S_採点結果.登録済みレコード数
                        End If
                    Next s

                    If 背番号存在FLAG = False Then
                        Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                        全SENDFLAG = False
                    End If

                Else
                    Me.DGV_ジャッジ.Rows(j - 1).Cells(1).Style.BackColor = Nothing
                    '採点ファイルが存在しないとき
                    全SENDFLAG = False
                End If
            Next j

        End If


        '全SENDを確認したら 進行管理ファイルに書き込む
        If 全SENDFLAG = True Then

            LOG.LogAdd("全SEND確定: 種目=" & 対象種目順 & " ヒート=" & 対象ヒート番号 & " 処理済FLAG=" & 全SEND処理済FLAG, 4)

            ' 2人同時SENDによる二重実行を防止
            If 全SEND処理済FLAG = True Then
                LOG.LogAdd("全SEND後処理スキップ（処理済）", 4)
                Exit Sub
            End If
            全SEND処理済FLAG = True

            マスタデータ.U_進行管理.FileRead()

            If マスタデータ.U_進行管理.Get_進行(現在競技番号, 現在枝番, 対象種目順, 対象ヒート番号) IsNot Nothing Then
                If マスタデータ.U_進行管理.Get_進行(現在競技番号, 現在枝番, 対象種目順, 対象ヒート番号).ステータス <> "全審判送信済み" Then

                    Dim 進行 As U_進行
                    進行 = New U_進行

                    進行.競技番号 = 現在競技番号
                    進行.競技番号枝番 = 現在枝番
                    進行.種目順 = 対象種目順
                    進行.ヒート番号 = 対象ヒート番号   'チェック法のときは複数ヒートをまとめて更新が必要


                    進行.ステータス = "全審判送信済み"
                    進行.採点終了時刻 = System.DateTime.Now


                    マスタデータ.U_進行管理.登録(進行)

                    進行 = Nothing

                End If
            End If

            'タイマーを止める
            Stopタイマー()


            'Dim 採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号)

            'If 採点方式 = "BJS20J" Then
            'BJS20Jの時は、すぐに次のヒートに移る

            'Me.Invoke(次ヒート_Delegate, New Object() {})

            'Else

            'If 更新タイマー実施中FLAG = False Then

            '10秒後に次ヒート
            更新タイマーStart()

            'End If

            '受信したイベントをMainに送る
            LOG.LogAdd("RaiseEvent 全ジャッジ送信済みイベント 発火", 4)
            RaiseEvent 全ジャッジ送信済みイベント(Me, New EventArgs)
            LOG.LogAdd("RaiseEvent 全ジャッジ送信済みイベント 完了", 4)

            'System.Threading.Thread.Sleep(1000)  ' UIスレッドを占有するため削除

            'End If


            ' 関連端末への送信を別スレッドで実行（UIスレッドのブロックを防ぐ）
            Dim 送信_区分番号 As String = 現在区分番号
            Dim 送信_ラウンド番号 As String = 現在ラウンド番号
            Dim 採点方式 As String = マスタデータ.C_ラウンドマスタ.Get採点方式(送信_区分番号, 送信_ラウンド番号)

            Task.Run(Sub()
                         Try
                             If Strings.Left(採点方式, 3) = "PDJ" Or Strings.Left(採点方式, 3) = "VAL" Then
                                 '結果表示端末に結果を送る
                                 SEND_JK_DANS_RESULT(送信_区分番号, 送信_ラウンド番号)
                             Else
                                 '関連端末にステータス更新の データ送信を行う MU_Progress
                                 SEND_JK_MU_Progress(送信_区分番号, 送信_ラウンド番号)
                                 '関連端末に詳細結果のデータ送信を行う
                                 SEND_JK_KANS_RESULT_J(送信_区分番号, 送信_ラウンド番号)
                             End If
                             LOG.LogAdd("関連端末への送信完了", 4)
                         Catch ex As Exception
                             LOG.LogAdd("関連端末への送信エラー: " & ex.Message, 1)
                         End Try
                     End Sub)



        End If

    End Sub

    '更新タイマー実施中FLAG
    Private 更新タイマー実施中FLAG As Boolean

    ' 全SEND後処理済みフラグ（2人同時SENDによる二重実行を防止）
    Private 全SEND処理済FLAG As Boolean = False

    '受信したイベントをMainに送る
    Public Event 全ジャッジ送信済みイベント(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Event SendTempイベント(ByVal sender As Object, ByVal ジャッジ結果_J As S_採点結果_V2_J)


    Private Sub ReceiveSendTemp(ByVal sender As Object, ByVal ジャッジ結果_J As S_採点結果_V2_J) Handles TCPServer.SendTempイベント

        RaiseEvent SendTempイベント(sender, ジャッジ結果_J)

    End Sub



    '=====更新タイマー=====
    ' 全ジャッジのSENDを確認したら、10秒後に次ヒートを実行する　→5秒に変更

    Private Sub 更新タイマーStart()
        更新タイマー実施中FLAG = True

        ' 非同期スレッドから呼ばれた場合のみ Invoke する（UIスレッド上からの呼び出しだと二重Invokeでデッドロックになるため）
        If Me.InvokeRequired Then
            Me.Invoke(Timer2_Start_Delegate, New Object() {})
        Else
            Timer2Start()
        End If
    End Sub

    Private Sub 更新タイマーStop()
        更新タイマー実施中FLAG = False

        ' 非同期スレッドから呼ばれた場合のみ Invoke する（UIスレッド上からの呼び出しだと二重Invokeでデッドロックになるため）
        If Me.InvokeRequired Then
            Me.Invoke(Timer2_Stop_Delegate, New Object() {})
        Else
            Timer2Stop()
        End If
    End Sub

    ' 'デリゲート宣言
    Delegate Sub Timer2StartDelegate()
    Delegate Sub Timer2StopDelegate()

    Delegate Sub 次ヒートDelegate()

    'デリゲート宣言をデータ型とした変数を作成
    Private Timer2_Start_Delegate As New Timer2StartDelegate(AddressOf Timer2Start)
    Private Timer2_Stop_Delegate As New Timer2StopDelegate(AddressOf Timer2Stop)

    Private 次ヒート_Delegate As New 次ヒートDelegate(AddressOf 次ヒート)


    Private Sub Timer2Start()

        Timer2.Stop()

        Dim 採点方式 = マスタデータ.C_ラウンドマスタ.Get採点方式(現在区分番号, 現在ラウンド番号)

        If (Strings.Left(採点方式, 4) = "BJS2" Or Strings.Left(採点方式, 4) = "BJS3") And 現在種目順 = 1 Then
            Timer2.Interval = 100   '0.1秒毎

        Else
            'Timer2.Interval = 10000   '10秒毎
            Timer2.Interval = 5000   '5秒毎

        End If

        Timer2.Enabled = True
        Timer2.Start()
    End Sub

    Private Sub Timer2Stop()
        Timer2.Stop()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        '10秒たったら、ここが呼び出される

        次ヒート()
        'Me.Invoke(次ヒート_Delegate, New Object() {})

        ' Timer2_Tick は UIスレッドで発火するため、Invoke ではなく直接呼び出す
        Timer2Stop()

        更新タイマー実施中FLAG = False

    End Sub


    '====更新タイマー　ここまで

    '=====DGV イベント========

    'DGV_種目がクリックされた時
    Private Sub DGV_種目_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_種目.CellContentClick

        'どの種目が選択されたか？
        Dim s As Integer = 0
        For Each r As DataGridViewRow In Me.DGV_種目.SelectedRows
            s = r.Index
        Next r

        'ヒート表の更新
        DGV_ヒートの更新(マスタデータ, 現在区分番号, 現在ラウンド番号, s + 1)




    End Sub

    'DGV_ヒートがクリックされた時
    Private Sub DGV_ヒート_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_ヒート.CellContentClick

        'どの種目が選択されたか？
        Dim s As Integer = 0
        For Each r As DataGridViewRow In Me.DGV_種目.SelectedRows
            s = r.Index
        Next r

        'どのヒートが選択されたか？
        Dim h As Integer = 0
        For Each r As DataGridViewRow In Me.DGV_ヒート.SelectedRows
            h = r.Index
        Next r


        'ジャッジ表の更新
        If s + 1 > 0 And h + 1 > 0 Then
            J_Send色付け(s + 1, h + 1)
        End If

    End Sub




    Public Sub Set_LOG(_LOG As LOG_C)

        LOG = _LOG

        ' TCPServer は F_GM_Load で初期化されるため、Set_LOG が先に呼ばれた場合はスキップ
        ' F_GM_Load 末尾で再度セットする
        If TCPServer IsNot Nothing Then
            TCPServer.Set_LOG(LOG)
        End If

    End Sub


    '=====イベント========

    'Private Sub E11_Login_Event(sender, e) Handles TCPServer.E11_LOGIN_SVR

    '   MsgBox(sender)

    'End Sub

    'クライアントを受け入れた時
    Private Sub server_AcceptedClient(ByVal sender As Object, ByVal e As ServerEventArgs)

        'デバッグ用
        'MsgBox("Accepted " & e.Client.端末名)


        'Me.UpdateClientList()
        'Me.AddLog(String.Format("({0})が接続しました。", e.Client.RemoteEndPoint.Address.ToString()), Color.Black)
    End Sub

    '関連端末用
    Private Sub server_AcceptedClient2(ByVal sender As Object, ByVal e As ServerEventArgs2)

        'デバッグ用
        'MsgBox("Accepted " & e.Client.端末名)


        'Me.UpdateClientList()
        'Me.AddLog(String.Format("({0})が接続しました。", e.Client.RemoteEndPoint.Address.ToString()), Color.Black)
    End Sub

    Private lockObj As Object = New Object()

    'クライアントからデータを受信した時
    Private Sub server_ReceivedData(ByVal sender As Object, ByVal e As ReceivedDataEventArgs) Handles TCPServer.ReceivedData
        'Dim str As String = e.Client.RemoteEndPoint.Address.ToString() + " > " + e.ReceivedString
        Dim str As String = e.ReceivedString

        'デバック用
        'MsgBox(str)

        Select Case str.Split(",")(1)
            Case "REQKUBUN"
                'クライアントリストに追加
                Add_Client(e.Client)

                ' UIスレッドで実行
                Me.Invoke(Sub()
                              Try
                                  LOG.LogAdd(e.Client.端末名 & "が接続しました", 3)
                                  J_LOGIN色付け()
                              Catch ex As Exception
                                  LOG.LogAdd("REQKUBUN処理エラー: " & ex.Message, 1)
                              End Try
                          End Sub)

            Case "REQHEAT"
                'E11_LOGIN

                Add_Client(e.Client)

                ' UIスレッドで実行
                Me.Invoke(Sub()
                              Try
                                  LOG.LogAdd(e.Client.端末名 & "で" & e.Client.ジャッジ記号 & "がログインしました。", 3)
                                  J_LOGIN色付け()
                              Catch ex As Exception
                                  LOG.LogAdd("REQHEAT処理エラー: " & ex.Message, 1)
                              End Try
                          End Sub)


            Case "REQHEAT_2"

                Add_Client(e.Client)

                ' UIスレッドで実行
                Me.Invoke(Sub()
                              Try
                                  J_LOGIN色付け()
                              Catch ex As Exception
                                  LOG.LogAdd("REQHEAT_2処理エラー: " & ex.Message, 1)
                              End Try
                          End Sub)


            Case "SNDTEMP"
                'E12_RCVTemp


                Add_Client(e.Client)

            Case "SNDTEMP_J"
                'E12_RCVTemp


                Add_Client(e.Client)


            Case "SNDTEMP_V2_J"
                'E12_RCVTemp

                Add_Client(e.Client)

            Case "SNDTEMP_BR2_J"
                'E12_RCVTemp


                Add_Client(e.Client)

            Case "SNDRESLT"
                'E13_RCV
                Add_Client(e.Client)

                ' UIスレッドで実行し、例外処理を追加（LogDelegateとの二重Invoke解消）
                Me.Invoke(Sub()
                              Try
                                  LOG.LogAdd(e.Client.端末名 & "で" & e.Client.ジャッジ記号 & "がSENDしました。", 3)
                                  SyncLock lockObj
                                      J_LOGIN色付け()  'これをしないと、DGV_ジャッジの種目とヒートが更新されない
                                      J_Send色付け(現在種目順, 現在ヒート番号)
                                  End SyncLock
                              Catch ex As Exception
                                  LOG.LogAdd("SNDRESLT処理エラー: " & ex.Message, 1)
                              End Try
                          End Sub)

                'ここでイベントを発生させて、F501を更新したい


            Case "SNDRESLT_J"
                'E13_RCV
                Add_Client(e.Client)

                ' UIスレッドで実行し、例外処理を追加（LogDelegateとの二重Invoke解消）
                Me.Invoke(Sub()
                              Try
                                  LOG.LogAdd(e.Client.端末名 & "で" & e.Client.ジャッジ記号 & "がSENDしました。", 3)
                                  SyncLock lockObj
                                      J_LOGIN色付け()  'これをしないと、DGV_ジャッジの種目とヒートが更新されない
                                      J_Send色付け(現在種目順, 現在ヒート番号)
                                  End SyncLock
                              Catch ex As Exception
                                  LOG.LogAdd("SNDRESLT_J処理エラー: " & ex.Message, 1)
                              End Try
                          End Sub)

                'ここでイベントを発生させて、F501を更新したい

            Case "SNDRESLT_V2_J"
                'E13_RCV
                Add_Client(e.Client)

                ' UIスレッドで実行し、例外処理を追加（LogDelegateとの二重Invoke解消）
                Me.Invoke(Sub()
                              Try
                                  LOG.LogAdd(e.Client.端末名 & "で" & e.Client.ジャッジ記号 & "がSENDしました。", 3)
                                  SyncLock lockObj
                                      J_LOGIN色付け()  'これをしないと、DGV_ジャッジの種目とヒートが更新されない
                                      J_Send色付け(現在種目順, 現在ヒート番号)
                                  End SyncLock
                              Catch ex As Exception
                                  LOG.LogAdd("SNDRESLT_V2_J処理エラー: " & ex.Message, 1)
                              End Try
                          End Sub)


            Case "SNDRESLT_BR2_J"
                'E13_RCV
                Add_Client(e.Client)

                ' UIスレッドで実行し、例外処理を追加（LogDelegateとの二重Invoke解消）
                Me.Invoke(Sub()
                              Try
                                  LOG.LogAdd(e.Client.端末名 & "で" & e.Client.ジャッジ記号 & "がSENDしました。", 3)
                                  SyncLock lockObj
                                      'CheckAllJudgSend_BR2(str.Split(",")(5), str.Split(",")(6))   'GMの現在競技以外でSENDされても、進行を更新するため。
                                      'TCP Clientで実装する。

                                      J_LOGIN色付け()  'これをしないと、DGV_ジャッジの種目とヒートが更新されない
                                      J_Send色付け(現在種目順, 現在ヒート番号)
                                  End SyncLock
                              Catch ex As Exception
                                  LOG.LogAdd("SNDRESLT_BR2_J処理エラー: " & ex.Message, 1)
                              End Try
                          End Sub)



            Case "REQHELP"
                'E51_ReqHelp
                Dim HELPステータス As String = str.Split(",")(10)
                If HELPステータス = "START" Then
                    'MsgBox("HELP! " & str.Split(",")(7) & "ジャッジがHELPを求めています。")
                    J_HELP色付け(str.Split(",")(7), "ON")
                    System.Media.SystemSounds.Hand.Play()
                Else
                    J_HELP色付け(str.Split(",")(7), "OFF")
                    J_LOGIN色付け()
                End If


            Case "REDUCTION"
                '減点確認中
                Dim 減点確認ステータス As String = str.Split(",")(9)
                If 減点確認ステータス = "START" Then
                    Me.Invoke(減点確認中Delegate, New Object() {"減点確認中", Color.White, Color.Red})



                Else
                    Me.Invoke(減点確認中Delegate, New Object() {"減点確認中", Color.Black, SystemColors.Info})

                End If

                '関連端末にも、ステータスを送る
                Me.SEND_JK_REDUCTION(減点確認ステータス)

            Case "REQTIMSTART"
                'Refreeからのタイマーイベント
                Dim タイマカテゴリ As String = str.Split(",")(9)
                If タイマカテゴリ = "P" Then
                    'カウントUPの時
                    StartカウントUP()

                ElseIf タイマカテゴリ = "D" Then

                    'カウントダウンの時
                    StartカウントDOWN(str.Split(",")(10))

                ElseIf タイマカテゴリ = "J" Then
                    'ジャッジタイムのスタート

                    StartJudgTime()

                Else

                    'タイマーのストップ

                    Stopタイマー()

                End If


            Case "SENDBM"
                'どこかのクライアントでブックマークが押された
                Me.Invoke(LogDelegate, New Object() {e.Client.ジャッジ記号 & "がブックマークしました"})

                Me.Invoke(減点確認中Delegate, New Object() {"減点確認中", Color.White, Color.Pink})

                Send_NOTICE_BM電文(str.Split(",")(5), str.Split(",")(6), str.Split(",")(7), str.Split(",")(8))


            Case "REQBMLIST"


            '関連端末用のメッセージ受信
            Case "KREQ_MA_COMP"

                'クライアントリストに追加
                Add_関連Client(e.Client)

                Me.Invoke(LogDelegate, New Object() {"関連" & e.Client.端末名 & "が接続しました"})
                'TCPClient2で、NOWSTATUSを返信

            Case "KREQ_MB_KUBUN”

            Case "KREQ_MU_Progress"

            Case "KREQ_FF_KUBUN”

            Case "KREQ_FF_RESULT”



            Case "KREQ_HEAT"

            Case "KREQ_JUDGE"


            Case "KREQ_RESULT"


            Case "KREQ_RESULT_J"


            Case "REQSTATUS"

                'クライアントリストに追加
                Add_関連Client(e.Client)

                Me.Invoke(LogDelegate, New Object() {"関連" & e.Client.端末名 & "が接続しました"})
                'TCPClient2で、NOWSTATUSを返信

                If Me.PB_GOSTOP.Text = "進行STOP" Then
                    Me.SEND_JK_GOSTOP("STOP")
                Else
                    Me.SEND_JK_GOSTOP("GO")
                End If


            Case "HEATSTART"
                '司会がヒート開始ボタンを押した時

                '前のヒートを終了にする ,種目の終了は加味しない(ヒート１の時は何もしない）
                If CInt(str.Split(",")(9)） > 1 Then

                    Dim 競技番号 As String = str.Split(",")(6)
                    Dim 競技番号枝番 As String = str.Split(",")(7)
                    Dim 種目順 As Integer = CInt(str.Split(",")(8))
                    Dim ヒート番号 As Integer = CInt(str.Split(",")(9)) - 1

                    If ヒート番号 > 1 Then

                        マスタデータ.U_進行管理.FileRead()
                        Dim 進行 As U_進行 = マスタデータ.U_進行管理.Get_進行(競技番号, 競技番号枝番, 種目順, ヒート番号)

                        If 進行.ステータス = "準備前" Or 進行.ステータス = "ヒート表作成済み" Then


                            進行.ステータス = "競技終了"

                            マスタデータ.U_進行管理.登録（進行）
                        End If
                    End If

                End If


            'DISP端末との通信

            Case "DREQ_MASTER_J"

                'クライアントリストに追加
                Add_関連Client(e.Client)

                Me.Invoke(LogDelegate, New Object() {"DSP" & e.Client.端末名 & "が接続しました"})

            Case "DREQ_STARTLIST_J"

                'クライアントリストに追加
                Add_関連Client(e.Client)

            Case "DREQ_RESULT_J"


            Case "DEVENT_J"

                '送信元以外のDSP端末に同じ電文を送る
                Dim DEVENT_PARM = New DEVENT_PARM_C


                If UBound(str.Split(",")) >= 5 Then

                    '配列(0)-(4)とカンマを削除する

                    Dim 配列 = str.Split(",")

                    Dim 削除文字列 As String = ""
                    For i = 0 To 4
                        削除文字列 = 削除文字列 & 配列(i) & ","
                    Next i

                    str = str.Replace(削除文字列, "")
                End If




                DEVENT_PARM = DEVENT_PARM.Get_JSON(str)


                SEND_DEVENT(e.Client.端末名, DEVENT_PARM)



            Case Else
                'MsgBox("不正電文:" & str)

                Try
                    LOG.LogAdd("不正電文:" & str, 1)
                Catch ex As Exception

                End Try

        End Select



        'Me.AddLog(str, Color.LightGray)
    End Sub




    Private Sub ReceivedData(ByVal sender As Object, ByVal e As ReceivedDataEventArgs)
        Dim str As String = e.Client.RemoteEndPoint.Address.ToString() + " > " + e.ReceivedString

        'デバッグ用
        'MsgBox(str)

        'Me.AddLog(str, Color.LightGray)
    End Sub



    'クライアントが切断した時
    Private Sub server_DisconnectedClient(ByVal sender As Object, ByVal e As ServerEventArgs) Handles TCPServer.DisconeetctedClent

        Me.Invoke(LogDelegate, New Object() {e.Client.端末名 & "が切断しました。"})

        J_LOGIN色付け()

    End Sub




    '====TCP Client配列の管理==================

    'TCP Clientを、クライアントリストに追加
    Private Sub Add_Client(cl As TCPClient)

        '同じ「区分番号」「ラウンド番号」「ジャッジ記号」のクライアントを削除する

        For i = 1 To UBound(Client_list)
            If Client_list(i) Is Nothing Then

            ElseIf Client_list(i).IsClosed = True Then

            Else
                If Client_list(i).区分番号 = cl.区分番号 And
                   Client_list(i).ラウンド番号 = cl.ラウンド番号 And
                   Client_list(i).ジャッジ記号 = cl.ジャッジ記号 Then

                    Del_Client(Client_list(i))

                End If
            End If
        Next i




        '空いているところを探してセットする
        For i = 1 To UBound(Client_list)
            If Client_list(i) Is Nothing Then
                Client_list(i) = cl
                i = UBound(Client_list)

                J_LOGIN色付け()

            ElseIf Client_list(i).IsClosed = True Then
                Client_list(i) = cl
                i = UBound(Client_list)

                J_LOGIN色付け()

            ElseIf Client_list(i).端末名 = cl.端末名 Then
                '上書き
                Client_list(i) = cl
                i = UBound(Client_list)
            End If
        Next i

    End Sub

    'TCP Clientを、クライアントリストから削除
    Private Sub Del_Client(cl As TCPClient)

        '該当のTCPClientを探して消す ジャッジ記号でマッチング
        For i = 1 To UBound(Client_list)
            If Client_list(i) IsNot Nothing Then
                If Client_list(i).ジャッジ記号 = cl.ジャッジ記号 Then
                    Client_list(i) = Nothing
                    i = UBound(Client_list)
                End If
            End If
        Next i

    End Sub

    'TCP Clientを、関連クライアントリストに追加
    Private Sub Add_関連Client(cl As TCPClient)

        '空いているところを探してセットする
        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) Is Nothing Then
                関連Client_list(i) = cl
                i = UBound(関連Client_list)

            ElseIf 関連Client_list(i).IsClosed = True Then
                関連Client_list(i) = cl
                i = UBound(関連Client_list)
            ElseIf 関連Client_list(i).端末名 = cl.端末名 Then
                '上書き
                関連Client_list(i) = cl
                i = UBound(関連Client_list)
            End If
        Next i

    End Sub

    'TCP Clientを、クライアントリストに追加
    Private Sub Del_関連Client(cl As TCPClient)

        '該当のTCPClientを探して消す ジャッジ記号でマッチング
        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i).端末名 = cl.端末名 Then
                関連Client_list(i) = Nothing
                i = UBound(関連Client_list)
            End If
        Next i

    End Sub

    '=====LOG画面に書き出し=======
    'デリゲート宣言
    Delegate Sub AddLogDelegate(ByVal srt As String)
    'デリゲート宣言をデータ型とした変数を作成
    Private LogDelegate As New AddLogDelegate(AddressOf WirteLog画面)


    Private Sub WirteLog画面(str As String)

        Dim 時刻 As String = ""
        時刻 = System.DateTime.Now.ToString("hh:mm:ss:fff")

        Me.LB_LOG.Items.Add(時刻 & " " & str)

        Me.LB_LOG.TopIndex = Me.LB_LOG.Items.Count - 1

    End Sub



    '===== 電文作成 ======




    '======テスト用

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'すべてのClientにメッセージを送る
        TCPServer.SendToAllClients("JS,ANSKUBUN,GM01,1,1,三笠宮杯,01,全日本選手権スタンダード,400,決勝,5,A,審判員１,B,審判員2,C,審判員3,D,審判員4,E,審判員5")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        'すべてのClientにメッセージを送る
        'ヒート表
        TCPServer.SendToAllClients("JS,ANSHEAT,GM01,1,1,01,全日本選手権スタンダード,400,決勝,AJS30J,B,佐倉　文彦,ジャッジ,F,Slow foxtrot,S,6,1,6,7.75,3.00,TQ,MM,PS,CP,,,,,,,13,2,,15,4,,)  ")

    End Sub

    Private Sub PB_次ヒート_Click(sender As Object, e As EventArgs) Handles PB_次ヒート.Click

        'タイマー２を止める
        更新タイマーStop()

        '次ヒート()
        指定ヒート()

    End Sub

    Private Sub PB_前ヒート_Click(sender As Object, e As EventArgs) Handles PB_前ヒート.Click

        'タイマー２を止める
        更新タイマーStop()

        前ヒート()
    End Sub


    '全JS端末の表示画面を変更する
    Private Sub PB_全JS_Click_1(sender As Object, e As EventArgs) Handles PB_全JS.Click

        Dim 選択種目行 As Integer
        '現在選択されている行
        For Each c As DataGridViewCell In DGV_種目.SelectedCells
            選択種目行 = c.RowIndex + 1
        Next c
        Dim 選択ヒート行 As Integer
        For Each c As DataGridViewCell In DGV_ヒート.SelectedCells
            選択ヒート行 = c.RowIndex + 1
        Next c

        If 選択種目行 = 0 Then
            MsgBox("種目を選択してください。")
            Exit Sub
        End If
        If 選択ヒート行 = 0 Then
            MsgBox("ヒートを選択してください。")
            Exit Sub
        End If

        '1個前のヒートの 種目番号、ヒート番号を取得

        'GMで指定した区分、ラウンドを表示しているジャッジの画面を変更
        For i = 1 To UBound(Client_list)
            If Client_list(i) IsNot Nothing Then
                If Client_list(i).区分番号 = 現在区分番号 And
                   Client_list(i).ラウンド番号 = 現在ラウンド番号 Then


                    Client_list(i).SEND_ANSHEAT(Client_list(i).ジャッジ記号, 現在区分番号, 現在ラウンド番号, CStr(選択種目行), 選択ヒート行, "現")

                End If
            End If
        Next i


        'ジャッジの表示画面（種目・ヒート）を表示するため、
        J_LOGIN色付け()


    End Sub

    '指定されたJS端末の表示画面を変更する
    Private Sub PB_選択JS_Click(sender As Object, e As EventArgs) Handles PB_選択JS.Click


        Dim 選択種目行 As Integer
        '現在選択されている行
        For Each c As DataGridViewCell In DGV_種目.SelectedCells
            選択種目行 = c.RowIndex + 1
        Next c
        Dim 選択ヒート行 As Integer
        For Each c As DataGridViewCell In DGV_ヒート.SelectedCells
            選択ヒート行 = c.RowIndex + 1
        Next c

        Dim 選択ジャッジ行 As Integer
        For Each c As DataGridViewCell In DGV_ジャッジ.SelectedCells
            選択ジャッジ行 = c.RowIndex + 1
        Next c



        If 選択種目行 = 0 Then
            MsgBox("種目を選択してください。")
            Exit Sub
        End If
        If 選択ヒート行 = 0 Then
            MsgBox("ヒートを選択してください。")
            Exit Sub
        End If

        If 選択ジャッジ行 = 0 Then
            MsgBox("ジャッジを選択してください。")
        End If

        Dim 選択ジャッジ記号 = Me.DGV_ジャッジ.Rows(選択ジャッジ行 - 1).Cells(0).Value

        '1個前のヒートの 種目番号、ヒート番号を取得

        'GMで指定した区分、ラウンドを表示しているジャッジの画面を変更
        For i = 1 To UBound(Client_list)
            If Client_list(i) IsNot Nothing Then
                If Client_list(i).区分番号 = 現在区分番号 And
                   Client_list(i).ラウンド番号 = 現在ラウンド番号 And
                   Client_list(i).ジャッジ記号 = 選択ジャッジ記号 Then

                    Client_list(i).SEND_ANSHEAT(Client_list(i).ジャッジ記号, 現在区分番号, 現在ラウンド番号, CStr(選択種目行), 選択ヒート行, "現")

                    '更新
                    'Client_list(i).種目記号 = Me.DGV_種目.Rows(選択種目行 - 1).Cells(0).Value
                    'Client_list(i).ヒート番号 = Me.DGV_ヒート.Rows(選択ヒート行 - 1).Cells(0).Value


                End If
            End If
        Next i


        'ジャッジの表示画面（種目・ヒート）を表示するため、
        J_LOGIN色付け()


    End Sub

    '全JS端末の表示画面をメインに戻す  ブレイキンのタイブレークが無い時等に使用
    Private Sub PB_終了_Click(sender As Object, e As EventArgs) Handles PB_終了.Click

        Dim 種目記号リスト() = Nothing
        Dim 種目数 As Integer = マスタデータ.D_種目マスタ.Get_種目数(現在区分番号, 現在ラウンド番号, 種目記号リスト)

        Dim ヒート数 As Integer = マスタデータ.E_ヒート表マスタ.Getヒート数(種目数)

        For i = 1 To UBound(Client_list)
            If Client_list(i) IsNot Nothing Then
                If Client_list(i).区分番号 = 現在区分番号 And
                   Client_list(i).ラウンド番号 = 現在ラウンド番号 Then

                    'Client_list(i).SEND_ANSKUBUN()
                    Client_list(i).SEND_ANSHEAT(Client_list(i).ジャッジ記号, 現在区分番号, 現在ラウンド番号, 種目数, ヒート数, "次")


                End If
            End If
        Next i


    End Sub

    'レフリー端末にNOTICE_BM電文を送る。（誰かがブックマークを押したことの通知
    Private Sub Send_NOTICE_BM電文(区分番号 As String, ラウンド番号 As String, 種目記号 As String, ヒート番号 As Integer)


        '指定された区分、ラウンドを表示しているジャッジに通知（本当はレフリーだけで良いが。。。）
        For i = 1 To UBound(Client_list)
            If Client_list(i) IsNot Nothing Then
                If Client_list(i).区分番号 = 区分番号 And
                   Client_list(i).ラウンド番号 = ラウンド番号 And
                   Client_list(i).種目記号 = 種目記号 And
                   Client_list(i).ヒート番号 = ヒート番号 Then

                    Client_list(i).SEND_NOTICEBM(区分番号, ラウンド番号, 種目記号, ヒート番号)

                End If
            End If
        Next i

    End Sub


    '関連端末にNOWSTATUSをSENDする
    Public Sub SEND_JK_NOWSTATUS()

        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                関連Client_list(i).SEND_NOWSTATUS()
            End If
        Next i
    End Sub



    '関連端末に結果決定を通知する。300 と400 で、確定ボタンが押されたとき
    Public Sub SEND_結果決定通知(区分番号 As String, ラウンド番号 As String)

        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                If Strings.Left(関連Client_list(i).端末名, 3) = "DSP" Then   '結果表示は　DSP で始まる



                Else
                    '  DSP端末以外には、結果を通知する。　DSP端末にはその前に既に結果が送られているから不要。
                    '  司会端末には、確定ボタンが押された後に結果を送信したい。

                    関連Client_list(i).SEND_RESULTOK(区分番号, ラウンド番号)
                    関連Client_list(i).SEND_DANS_マスタデータ()
                    関連Client_list(i).SEND_DANS_採点結果(区分番号, ラウンド番号)

                End If

            End If
        Next i




    End Sub



    '関連端末の区分一覧を更新する。
    Public Sub SEND_MB_KUBUN()

        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                関連Client_list(i).SEND_KANS_MB_KUBUN()
            End If
        Next i
    End Sub


    '関連端末にタイマー開始・終了をSENDする
    Public Sub SEND_JK_TIMER(タイマーカテゴリー As String, カウントダウン時間 As String)

        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                関連Client_list(i).SEND_TIMER(タイマーカテゴリー, カウントダウン時間)
            End If
        Next i
    End Sub

    '関連端末に減点確認中をSENDする
    Public Sub SEND_JK_REDUCTION(ステータス As String)

        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                関連Client_list(i).SEND_JK_REDUCTION(ステータス)
            End If
        Next i
    End Sub

    '関連端末にGO/STOP中をSENDする
    Public Sub SEND_JK_GOSTOP(ステータス As String)

        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                関連Client_list(i).SEND_GOSTOP(ステータス)
            End If
        Next i
    End Sub


    '関連端末にMU_ProgressをSENDする
    Public Sub SEND_JK_MU_Progress(区分番号 As String, ラウンド番号 As String)

        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                関連Client_list(i).SEND_KANS_MU_Progress(区分番号, ラウンド番号)
            End If
        Next i
    End Sub

    '関連端末にKANS_RESULT_JをSENDする
    Public Sub SEND_JK_KANS_RESULT_J(区分番号 As String, ラウンド番号 As String)

        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                関連Client_list(i).SEND_KANS_RESULT_J(区分番号, ラウンド番号)
                関連Client_list(i).SEND_KANS_MU_Progress(区分番号, ラウンド番号)
            End If
        Next i
    End Sub

    'MOVIE端末にMOVIE_STARTをSENDする。

    Public Sub SEND_JK_KANS_MOVIE_START()


        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                If Strings.Left(関連Client_list(i).端末名, 2) = "VS" Then   'ビデオサーバーは VS で始まる

                    Dim KANS_MOVIE_START_J = New KANS_MOVIE_START_J()
                    KANS_MOVIE_START_J.区分番号 = 現在区分番号
                    KANS_MOVIE_START_J.区分名 = マスタデータ.B_区分マスタ.Get区分表記名(現在区分番号)
                    KANS_MOVIE_START_J.ラウンド番号 = 現在ラウンド番号
                    KANS_MOVIE_START_J.ラウンド名 = マスタデータ.Get_ラウンド名(現在ラウンド番号)

                    Dim 種目 As D_種目 = マスタデータ.D_種目マスタ.Get_種目Class(現在区分番号, 現在ラウンド番号, 現在種目順)

                    KANS_MOVIE_START_J.種目記号 = 種目.種目記号
                    KANS_MOVIE_START_J.種目名 = マスタデータ.Z_システム設定.Get_種目名称(種目.種目記号).種目名_J
                    KANS_MOVIE_START_J.SG種別 = 種目.SG種別
                    KANS_MOVIE_START_J.ヒート番号 = 現在ヒート番号.ToString

                    If 種目.SG種別 = "S" Then  'ソロの時

                        Dim 背番号リスト() = Nothing
                        Dim 出場選手数 As Integer = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(現在種目順, 現在ヒート番号, 背番号リスト)
                        Dim 背番号 As String = 背番号リスト(1)  'ソロは1組だけ

                        Dim 選手LIST番号 As String = マスタデータ.B_区分マスタ.Get区分C(現在区分番号).使用する選手マスタ
                        Dim 選手 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手LIST番号, 背番号)


                        KANS_MOVIE_START_J.背番号 = 背番号
                        KANS_MOVIE_START_J.リーダー名 = 選手.リーダー表記名
                        KANS_MOVIE_START_J.パートナー名 = 選手.パートナ表記名
                        KANS_MOVIE_START_J.所属 = 選手.カップル所属名

                    ElseIf 種目.SG種別 = "Q" Then   '"D" Then  'Duel対戦の時

                        Dim 背番号リスト() = Nothing
                        Dim 出場選手数 As Integer = マスタデータ.E_ヒート表マスタ.Get_背番号リスト(現在種目順, 現在ヒート番号, 背番号リスト)
                        ' Dim 背番号 As String = 背番号リスト(1)  'ソロは1組だけ

                        Dim 選手LIST番号 As String = マスタデータ.B_区分マスタ.Get区分C(現在区分番号).使用する選手マスタ
                        Dim 選手1 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手LIST番号, 背番号リスト(1))
                        Dim 選手2 As 選手 = マスタデータ.選手マスタ.Get選手C_by背番号(選手LIST番号, 背番号リスト(2))


                        KANS_MOVIE_START_J.背番号 = 背番号リスト(1) & "_" & 背番号リスト(2)
                        KANS_MOVIE_START_J.リーダー名 = 選手1.リーダー表記名 & "_" & 選手2.リーダー表記名
                        KANS_MOVIE_START_J.パートナー名 = 選手1.パートナ表記名 & "_" & 選手2.パートナ表記名
                        KANS_MOVIE_START_J.所属 = 選手1.カップル所属名 & "_" & 選手2.カップル所属名


                    Else   'DuelとGroup


                        KANS_MOVIE_START_J.背番号 = ""
                        KANS_MOVIE_START_J.リーダー名 = ""
                        KANS_MOVIE_START_J.パートナー名 = ""
                        KANS_MOVIE_START_J.所属 = ""


                    End If

                    Dim jText As String = KANS_MOVIE_START_J.Get_JSON文字列


                    関連Client_list(i).SEND_KANS_MOVIE_START(jText)
                End If
            End If

        Next i

    End Sub

    'MOVIE端末にMOVIE_STARTをSENDする。

    Public Sub SEND_JK_KANS_MOVIE_STOP()



        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                If Strings.Left(関連Client_list(i).端末名, 2) = "VS" Then   'ビデオサーバーは VS で始まる

                    関連Client_list(i).SEND_KANS_MOVIE_STOP()

                End If
            End If
        Next i



    End Sub

    'DISP端末に採点結果をSENDする。

    Public Sub SEND_JK_DANS_RESULT(区分番号 As String, ラウンド番号 As String)


        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                If Strings.Left(関連Client_list(i).端末名, 3) = "DSP" Then   '結果表示は　DSP で始まる

                    関連Client_list(i).SEND_DANS_採点結果(区分番号, ラウンド番号)

                Else  '結果表示端末以外には、MASTERデータを再送する　　ステータス更新のため

                    関連Client_list(i).SEND_DANS_マスタデータ()
                    '関連Client_list(i).SEND_DANS_スタートリスト(区分番号, ラウンド番号)

                End If
            End If
        Next i



    End Sub

    'DISP端末にDEVENTをSENDする。
    Public Sub SEND_DEVENT(送信元名 As String, DEVENT_PARM As DEVENT_PARM_C)

        For i = 1 To UBound(関連Client_list)
            If 関連Client_list(i) IsNot Nothing Then

                If Strings.Left(関連Client_list(i).端末名, 3) = "DSP" Then   '結果表示は　DSP で始まる

                    If 関連Client_list(i).端末名 <> 送信元名 Then
                        関連Client_list(i).SEND_DEVENT_J(DEVENT_PARM)
                    End If

                End If
            End If
        Next i


    End Sub




    '=======================================
    '======タイマー処理=====================
    '=======================================

    Private Movie_Status As String   'ビデオ録画の状況

    Private TimerStartTime As Date    'タイマーの開始時刻
    Private TimerStatus As String      'タイマーのステータス P D J N
    Private CoundDownTime As TimeSpan  'カウントダウンの時間数


    Private Sub タイマ初期設定()
        TimerStatus = "N"

        Timer1.Interval = 1000   '1秒毎
        Timer1.Enabled = True

        For t = 1 To 10
            If マスタデータ.Z_システム設定.Timer(t) <> "" Then
                Me.CB_カウントDown.Items.Add(マスタデータ.Z_システム設定.Timer(t))
            End If
        Next t

        Me.CB_カウントDown.Text = マスタデータ.Z_システム設定.Timer_D

        'Me.CB_カウントDown.Items.Add("00:15")

        'Me.CB_カウントDown.Items.Add("01:00")
        'Me.CB_カウントDown.Items.Add("01:20")
        'Me.CB_カウントDown.Items.Add("01:30")
        'Me.CB_カウントDown.Items.Add("01:45")

        'Me.CB_カウントDown.Text = ("01:30")

        Me.RB_Up.Checked = True


        Movie_Status = "OFF"


    End Sub



    Private Sub PB_タイマー_Click(sender As Object, e As EventArgs) Handles PB_タイマー.Click

        If TimerStatus = "N" Then

            If Me.RB_Up.Checked = True Then
                'カウントUPの時
                StartカウントUP()

            Else
                'カウントダウンの時
                StartカウントDOWN(Me.CB_カウントDown.Text)

            End If


        ElseIf TimerStatus = "P" Or TimerStatus = "D" Then
            'ジャッジタイムのスタート

            StartJudgTime()

        ElseIf TimerStatus = "J" Then
            'タイマーのストップ

            Stopタイマー()

        End If


    End Sub

    'タイマーの開始　カウントUP
    Private Sub StartカウントUP()

        ' タイマの生成
        Timer1.Interval = 1000   '3秒毎
        Timer1.Enabled = True

        TimerStartTime = System.DateTime.Now

        AddHandler Me.Closing,
      Sub()
          ' 画面が閉じられるときに、タイマを停止して破棄
          Timer1.Enabled = False
      End Sub

        ' タイマ開始ボタンをONに（＝タイマがスタートする）
        Me.Invoke(TimerDelegate, New Object() {"P 00:00", Color.Aqua})

        '電文送信
        Send_TimStart電文("P", "")　　'ジャッジ端末用
        SEND_JK_TIMER("P", "")　　　　　'関連端末用

        TimerStatus = "P"


        'ブックマークファイルにスタートを書き込む
        ブックマーク_スタート書き込み()


        '採点進行管理で開始実績を登録する
        マスタデータ.T_採点進行管理.開始実績時刻登録(現在区分番号, 現在ラウンド番号)


        'ビデオ録画スタート
        SEND_JK_KANS_MOVIE_START()
        Movie_Status = "ON"



    End Sub

    'タイマーの開始　カウントDown
    Private Sub StartカウントDOWN(カウントダウン時間 As String)
        ' タイマの生成
        Timer1.Interval = 1000   '3秒毎
        Timer1.Enabled = True

        TimerStartTime = System.DateTime.Now

        Dim min As Integer = Strings.Split(カウントダウン時間, ":")(0)
        Dim sec As Integer = Strings.Split(カウントダウン時間, ":")(1)

        CoundDownTime = New TimeSpan(0, min, sec + 1)


        AddHandler Me.Closing,
      Sub()
          ' 画面が閉じられるときに、タイマを停止して破棄
          Timer1.Enabled = False
      End Sub

        ' タイマ開始ボタンをONに（＝タイマがスタートする）
        Me.Invoke(TimerDelegate, New Object() {"D " & カウントダウン時間, Color.Aqua})


        '電文送信
        Send_TimStart電文("D", カウントダウン時間)
        SEND_JK_TIMER("D", カウントダウン時間)



        TimerStatus = "D"

        'ブックマークファイルにスタートを書き込む
        ブックマーク_スタート書き込み()

        '採点進行管理で開始実績を登録する
        マスタデータ.T_採点進行管理.開始実績時刻登録(現在区分番号, 現在ラウンド番号)


        'ビデオ録画スタート
        SEND_JK_KANS_MOVIE_START()
        Movie_Status = "ON"


    End Sub

    'ジャッジタイマーのスタート
    Private Sub StartJudgTime()

        'Jに変える
        Timer1.Interval = 1000   '3秒毎
        Timer1.Enabled = True

        TimerStartTime = System.DateTime.Now

        Me.Invoke(TimerDelegate, New Object() {"J 00:00", Color.PeachPuff})

        '電文送信
        Send_TimStart電文("J", "")
        SEND_JK_TIMER("J", "")




        TimerStatus = "J"

        'ブックマークファイルにエンドを書き込む
        ブックマーク_エンド書き込み()


        'ビデオ録画ストップ
        If Movie_Status = "ON" Then
            SEND_JK_KANS_MOVIE_STOP()
            Movie_Status = "OFF"
        End If


    End Sub

    'タイマーのストップ
    Private Sub Stopタイマー()

        ' 非同期スレッドから呼ばれた場合のみ Invoke する（UIスレッド上からの呼び出しだと二重Invokeでデッドロックになるため）
        If Me.InvokeRequired Then
            Me.Invoke(New Action(AddressOf Stopタイマー))
            Return
        End If

        Timer更新("  00:00", SystemColors.Control)
        'Timer1.Enabled = False

        TimerStatus = "N"

        '電文送信
        Send_TimStart電文("N", "")
        SEND_JK_TIMER("N", "")

        'ビデオ録画ストップ
        If Movie_Status = "ON" Then
            SEND_JK_KANS_MOVIE_STOP()
            Movie_Status = "OFF"
        End If


    End Sub


    '=====タイマー画面の更新=======
    'デリゲート宣言
    Delegate Sub TimerUpdateDelegate(ByVal srt As String, カラー As Color)
    'デリゲート宣言をデータ型とした変数を作成
    Private TimerDelegate As New TimerUpdateDelegate(AddressOf Timer更新)



    Private Sub Timer更新(str As String, カラー As Color)

        Me.PB_タイマー.Text = str
        Me.PB_タイマー.BackColor = カラー


        If str = "D 00:00" Then
            'カウントダウンがゼロになったら、自動的にジャッジタイムのスタート
            StartJudgTime()
        End If

        'Dで、10秒以下の時はピンクに変更
        If Strings.Left(str, 4) = "D 00" Then
            If CInt(Strings.Right(str, 2)) <= 10 Then
                Me.PB_タイマー.BackColor = Color.Pink
            End If
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim str As String = ""

        If TimerStatus = "P" Then

            Dim diff = System.DateTime.Now - TimerStartTime

            Dim diff2 = Format(diff.Minutes, "00") & ":" & Format(diff.Seconds, "00")

            str = TimerStatus & " " & diff2

            Me.Invoke(TimerDelegate, New Object() {str, Color.Aqua})

        ElseIf TimerStatus = "J" Then

            Dim diff = System.DateTime.Now - TimerStartTime

            Dim diff2 = Format(diff.Minutes, "00") & ":" & Format(diff.Seconds, "00")

            str = TimerStatus & " " & diff2

            Me.Invoke(TimerDelegate, New Object() {str, Color.PeachPuff})

        ElseIf TimerStatus = "D" Then
            'カウントダウンの時

            Dim diff = CoundDownTime - (System.DateTime.Now - TimerStartTime)

            Dim diff2 = Format(diff.Minutes, "00") & ":" & Format(diff.Seconds, "00")

            str = TimerStatus & " " & diff2

            Me.Invoke(TimerDelegate, New Object() {str, Color.Aqua})

        End If



    End Sub

    Private Sub Send_TimStart電文(タイマカテゴリ As String, カウントダウン時間 As String)


        'GMで指定した区分、ラウンドを表示しているジャッジの画面を変更
        For i = 1 To UBound(Client_list)
            If Client_list(i) IsNot Nothing Then
                If Client_list(i).区分番号 = 現在区分番号 And
                   Client_list(i).ラウンド番号 = 現在ラウンド番号 Then

                    Client_list(i).SEND_TIMSTART(現在区分番号, 現在ラウンド番号, 現在種目順, 現在ヒート番号, タイマカテゴリ, カウントダウン時間)

                End If
            End If
        Next i

    End Sub

    Private Sub RB_Up_CheckedChanged(sender As Object, e As EventArgs) Handles RB_Up.CheckedChanged
        'カウントUPボタンが押された時
        Me.CB_カウントDown.Enabled = False
    End Sub

    Private Sub RB_Down_CheckedChanged(sender As Object, e As EventArgs) Handles RB_Down.CheckedChanged
        'カウントDownボタンが押された時

        Me.CB_カウントDown.Enabled = True

    End Sub

    '=====
    'デリゲート宣言
    Delegate Sub 減点確認中ボタンDelegate(ByVal srt As String, foreカラー As Color, Backカラー As Color)
    'デリゲート宣言をデータ型とした変数を作成
    Private 減点確認中Delegate As New 減点確認中ボタンDelegate(AddressOf 減点確認中更新)

    Private Sub 減点確認中更新(ByVal srt As String, foreカラー As Color, Backカラー As Color)

        Me.LB_減点確認中.Text = srt
        Me.LB_減点確認中.ForeColor = foreカラー
        Me.LB_減点確認中.BackColor = Backカラー

    End Sub


    Private Sub PB_GOSTOP_Click(sender As Object, e As EventArgs) Handles PB_GOSTOP.Click

        If Me.PB_GOSTOP.Text = "進行STOP" Then

            Me.PB_GOSTOP.Text = "進行GO"
            Me.PB_GOSTOP.BackColor = Color.Cyan
            Me.PB_GOSTOP.ForeColor = Color.Black

            Me.SEND_JK_GOSTOP("GO")

        Else
            Me.PB_GOSTOP.Text = "進行STOP"
            Me.PB_GOSTOP.BackColor = Color.Red
            Me.PB_GOSTOP.ForeColor = Color.White

            Me.SEND_JK_GOSTOP("STOP")


        End If

    End Sub

    Private Sub PB_区分更新_Click(sender As Object, e As EventArgs) Handles PB_区分更新.Click
        競技一覧の更新(マスタデータ)
    End Sub

    'フォーム閉じるボタンが押された時
    Private Sub Me_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If MsgBox("本当に終了しても良いですか？", vbOKCancel) = vbOK Then
            TCPServer.Server_Close()
            TCPServer = Nothing
            'Me.Close()
        Else
            '閉じるをキャンセル
            e.Cancel = True

        End If

    End Sub


    'クライアントにANSKUBUNを送って、ログイン画面をリフレッシュさせる
    Private Sub PB_Refresh_Click(sender As Object, e As EventArgs) Handles PB_Refresh.Click

        For i = 1 To UBound(Client_list)
            If Client_list(i) IsNot Nothing Then
                If Client_list(i).ジャッジ記号 = "" Then
                    Client_list(i).SEND_ANSKUBUN()
                End If
            End If
        Next i


    End Sub

    Private Sub PB_JS確認_Click(sender As Object, e As EventArgs) Handles PB_JS確認.Click

        Dim FGM01 As FGM01_端末一覧
        FGM01 = New FGM01_端末一覧

        FGM01.一覧更新(Client_list, Me)
        FGM01.Show()


    End Sub

    Private Sub PB_関連一覧_Click(sender As Object, e As EventArgs) Handles PB_関連一覧.Click


        Dim FGM02 As FGM02_関連端末一覧
        FGM02 = New FGM02_関連端末一覧

        FGM02.一覧更新(関連Client_list, Me)
        FGM02.Show()

    End Sub



    Private Sub ブックマーク_スタート書き込み()
        'BM_ブックマークに、タイマースタートを書き込む

        Dim BM As BM_BM
        BM = New BM_BM

        If 現在区分番号 <> "" Then

            BM.区分番号 = 現在区分番号
            BM.ラウンド番号 = 現在ラウンド番号
            BM.種目記号 = マスタデータ.D_種目マスタ.Get_種目Class(現在区分番号, 現在ラウンド番号, 現在種目順).種目記号
            BM.ヒート番号 = 現在ヒート番号
            BM.ジャッジ記号 = "START"
            BM.BM番号 = 1
            BM.時刻 = DateTime.Now.ToString("HH:mm:ss")



            マスタデータ.BM_ブックマーク.FileRead()
            マスタデータ.BM_ブックマーク.登録(BM)
        End If

    End Sub

    Private Sub ブックマーク_エンド書き込み()
        'BM_ブックマークに、タイマー終了を書き込む

        Dim BM As BM_BM
        BM = New BM_BM

        If 現在区分番号 <> "" Then
            BM.区分番号 = 現在区分番号
            BM.ラウンド番号 = 現在ラウンド番号
            BM.種目記号 = マスタデータ.D_種目マスタ.Get_種目Class(現在区分番号, 現在ラウンド番号, 現在種目順).種目記号
            BM.ヒート番号 = 現在ヒート番号
            BM.ジャッジ記号 = "END"
            BM.BM番号 = 1
            BM.時刻 = DateTime.Now.ToString("HH:mm:ss")



            マスタデータ.BM_ブックマーク.FileRead()
            マスタデータ.BM_ブックマーク.登録(BM)
        End If


    End Sub

End Class