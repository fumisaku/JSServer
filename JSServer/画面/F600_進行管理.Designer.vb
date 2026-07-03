<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F600_進行管理
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TB_現在競技 = New System.Windows.Forms.TextBox()
        Me.DGV_種目リスト = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.種目 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DGV_ヒート = New System.Windows.Forms.DataGridView()
        Me.ヒート = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TB_次競技 = New System.Windows.Forms.TextBox()
        Me.TB_次次競技 = New System.Windows.Forms.TextBox()
        Me.TB_タイマー = New System.Windows.Forms.TextBox()
        Me.TB_減点確認中 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TB_種目数 = New System.Windows.Forms.TextBox()
        Me.TB_採点方式 = New System.Windows.Forms.TextBox()
        Me.TB_UP数 = New System.Windows.Forms.TextBox()
        Me.TB_ヒート数 = New System.Windows.Forms.TextBox()
        Me.TB_出場組数 = New System.Windows.Forms.TextBox()
        Me.TB_GOSTOP = New System.Windows.Forms.TextBox()
        Me.Panel_ヒート表 = New System.Windows.Forms.Panel()
        Me.PB_次ヒート背番号 = New System.Windows.Forms.Button()
        Me.PB_前ヒート背番号 = New System.Windows.Forms.Button()
        Me.PB_現在ヒート背番号 = New System.Windows.Forms.Button()
        Me.TB_次組数 = New System.Windows.Forms.TextBox()
        Me.TB_前組数 = New System.Windows.Forms.TextBox()
        Me.TB_現組数 = New System.Windows.Forms.TextBox()
        Me.TB_前種目 = New System.Windows.Forms.TextBox()
        Me.TB_前ヒート番号 = New System.Windows.Forms.TextBox()
        Me.TB_次種目 = New System.Windows.Forms.TextBox()
        Me.TB_現種目 = New System.Windows.Forms.TextBox()
        Me.TB_次ヒート番号 = New System.Windows.Forms.TextBox()
        Me.PB_開始 = New System.Windows.Forms.Button()
        Me.TB_現在ヒート番号 = New System.Windows.Forms.TextBox()
        Me.PB_進行管理 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PB_結果表示 = New System.Windows.Forms.Button()
        CType(Me.DGV_種目リスト, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_ヒート, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel_ヒート表.SuspendLayout()
        Me.SuspendLayout()
        '
        'TB_現在競技
        '
        Me.TB_現在競技.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_現在競技.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_現在競技.Location = New System.Drawing.Point(20, 54)
        Me.TB_現在競技.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_現在競技.Multiline = True
        Me.TB_現在競技.Name = "TB_現在競技"
        Me.TB_現在競技.ReadOnly = True
        Me.TB_現在競技.Size = New System.Drawing.Size(247, 70)
        Me.TB_現在競技.TabIndex = 0
        Me.TB_現在競技.Text = "028" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "全日本選手権スタンダード" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "準決勝"
        '
        'DGV_種目リスト
        '
        Me.DGV_種目リスト.AllowUserToAddRows = False
        Me.DGV_種目リスト.AllowUserToDeleteRows = False
        Me.DGV_種目リスト.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DGV_種目リスト.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_種目リスト.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_種目リスト.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.種目})
        Me.DGV_種目リスト.Location = New System.Drawing.Point(20, 151)
        Me.DGV_種目リスト.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DGV_種目リスト.MultiSelect = False
        Me.DGV_種目リスト.Name = "DGV_種目リスト"
        Me.DGV_種目リスト.ReadOnly = True
        Me.DGV_種目リスト.RowHeadersVisible = False
        Me.DGV_種目リスト.RowTemplate.Height = 24
        Me.DGV_種目リスト.Size = New System.Drawing.Size(143, 195)
        Me.DGV_種目リスト.TabIndex = 1
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.ReadOnly = True
        Me.No.Width = 40
        '
        '種目
        '
        Me.種目.HeaderText = "種目"
        Me.種目.Name = "種目"
        Me.種目.ReadOnly = True
        Me.種目.Width = 140
        '
        'DGV_ヒート
        '
        Me.DGV_ヒート.AllowUserToAddRows = False
        Me.DGV_ヒート.AllowUserToDeleteRows = False
        Me.DGV_ヒート.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DGV_ヒート.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_ヒート.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_ヒート.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ヒート})
        Me.DGV_ヒート.Location = New System.Drawing.Point(167, 151)
        Me.DGV_ヒート.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DGV_ヒート.MultiSelect = False
        Me.DGV_ヒート.Name = "DGV_ヒート"
        Me.DGV_ヒート.ReadOnly = True
        Me.DGV_ヒート.RowHeadersVisible = False
        Me.DGV_ヒート.RowTemplate.Height = 24
        Me.DGV_ヒート.Size = New System.Drawing.Size(98, 195)
        Me.DGV_ヒート.TabIndex = 2
        '
        'ヒート
        '
        Me.ヒート.HeaderText = "ヒート"
        Me.ヒート.Name = "ヒート"
        Me.ヒート.ReadOnly = True
        Me.ヒート.Width = 120
        '
        'TB_次競技
        '
        Me.TB_次競技.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TB_次競技.BackColor = System.Drawing.SystemColors.Window
        Me.TB_次競技.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_次競技.Location = New System.Drawing.Point(20, 366)
        Me.TB_次競技.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_次競技.Multiline = True
        Me.TB_次競技.Name = "TB_次競技"
        Me.TB_次競技.ReadOnly = True
        Me.TB_次競技.Size = New System.Drawing.Size(247, 57)
        Me.TB_次競技.TabIndex = 3
        Me.TB_次競技.Text = "028" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "全日本選手権スタンダード" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "準決勝"
        '
        'TB_次次競技
        '
        Me.TB_次次競技.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TB_次次競技.BackColor = System.Drawing.SystemColors.Window
        Me.TB_次次競技.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_次次競技.Location = New System.Drawing.Point(20, 436)
        Me.TB_次次競技.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_次次競技.Multiline = True
        Me.TB_次次競技.Name = "TB_次次競技"
        Me.TB_次次競技.ReadOnly = True
        Me.TB_次次競技.Size = New System.Drawing.Size(247, 57)
        Me.TB_次次競技.TabIndex = 4
        Me.TB_次次競技.Text = "028" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "全日本選手権スタンダード" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "準決勝"
        '
        'TB_タイマー
        '
        Me.TB_タイマー.BackColor = System.Drawing.SystemColors.Window
        Me.TB_タイマー.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_タイマー.Location = New System.Drawing.Point(496, 10)
        Me.TB_タイマー.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_タイマー.Name = "TB_タイマー"
        Me.TB_タイマー.ReadOnly = True
        Me.TB_タイマー.Size = New System.Drawing.Size(86, 29)
        Me.TB_タイマー.TabIndex = 5
        Me.TB_タイマー.Text = "P 00:00"
        Me.TB_タイマー.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_減点確認中
        '
        Me.TB_減点確認中.BackColor = System.Drawing.SystemColors.Window
        Me.TB_減点確認中.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_減点確認中.Location = New System.Drawing.Point(610, 10)
        Me.TB_減点確認中.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_減点確認中.Name = "TB_減点確認中"
        Me.TB_減点確認中.ReadOnly = True
        Me.TB_減点確認中.Size = New System.Drawing.Size(128, 29)
        Me.TB_減点確認中.TabIndex = 6
        Me.TB_減点確認中.Text = "減点確認中"
        Me.TB_減点確認中.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.Aqua
        Me.TextBox1.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(20, 10)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(110, 29)
        Me.TextBox1.TabIndex = 7
        Me.TextBox1.Text = "オンタイム"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel1.Controls.Add(Me.TB_種目数)
        Me.Panel1.Controls.Add(Me.TB_採点方式)
        Me.Panel1.Controls.Add(Me.TB_UP数)
        Me.Panel1.Controls.Add(Me.TB_ヒート数)
        Me.Panel1.Controls.Add(Me.TB_出場組数)
        Me.Panel1.Location = New System.Drawing.Point(278, 54)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(459, 74)
        Me.Panel1.TabIndex = 8
        '
        'TB_種目数
        '
        Me.TB_種目数.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_種目数.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_種目数.Location = New System.Drawing.Point(10, 8)
        Me.TB_種目数.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_種目数.Name = "TB_種目数"
        Me.TB_種目数.ReadOnly = True
        Me.TB_種目数.Size = New System.Drawing.Size(115, 29)
        Me.TB_種目数.TabIndex = 13
        Me.TB_種目数.Text = "種目数 10"
        Me.TB_種目数.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_採点方式
        '
        Me.TB_採点方式.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_採点方式.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_採点方式.Location = New System.Drawing.Point(142, 8)
        Me.TB_採点方式.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_採点方式.Name = "TB_採点方式"
        Me.TB_採点方式.ReadOnly = True
        Me.TB_採点方式.Size = New System.Drawing.Size(147, 29)
        Me.TB_採点方式.TabIndex = 11
        Me.TB_採点方式.Text = "チェック法"
        Me.TB_採点方式.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_UP数
        '
        Me.TB_UP数.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_UP数.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_UP数.Location = New System.Drawing.Point(311, 40)
        Me.TB_UP数.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_UP数.Name = "TB_UP数"
        Me.TB_UP数.ReadOnly = True
        Me.TB_UP数.Size = New System.Drawing.Size(141, 29)
        Me.TB_UP数.TabIndex = 12
        Me.TB_UP数.Text = "UP数 50組"
        Me.TB_UP数.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_ヒート数
        '
        Me.TB_ヒート数.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_ヒート数.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_ヒート数.Location = New System.Drawing.Point(10, 40)
        Me.TB_ヒート数.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_ヒート数.Name = "TB_ヒート数"
        Me.TB_ヒート数.ReadOnly = True
        Me.TB_ヒート数.Size = New System.Drawing.Size(181, 29)
        Me.TB_ヒート数.TabIndex = 10
        Me.TB_ヒート数.Text = "Heat数 10-11 H"
        Me.TB_ヒート数.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_出場組数
        '
        Me.TB_出場組数.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_出場組数.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_出場組数.Location = New System.Drawing.Point(311, 8)
        Me.TB_出場組数.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_出場組数.Name = "TB_出場組数"
        Me.TB_出場組数.ReadOnly = True
        Me.TB_出場組数.Size = New System.Drawing.Size(141, 29)
        Me.TB_出場組数.TabIndex = 9
        Me.TB_出場組数.Text = "出場 100組"
        Me.TB_出場組数.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_GOSTOP
        '
        Me.TB_GOSTOP.BackColor = System.Drawing.Color.Aqua
        Me.TB_GOSTOP.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_GOSTOP.Location = New System.Drawing.Point(278, 10)
        Me.TB_GOSTOP.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_GOSTOP.Name = "TB_GOSTOP"
        Me.TB_GOSTOP.ReadOnly = True
        Me.TB_GOSTOP.Size = New System.Drawing.Size(126, 29)
        Me.TB_GOSTOP.TabIndex = 9
        Me.TB_GOSTOP.Text = "進行OK"
        Me.TB_GOSTOP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel_ヒート表
        '
        Me.Panel_ヒート表.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_ヒート表.AutoScroll = True
        Me.Panel_ヒート表.Controls.Add(Me.PB_次ヒート背番号)
        Me.Panel_ヒート表.Controls.Add(Me.PB_前ヒート背番号)
        Me.Panel_ヒート表.Controls.Add(Me.PB_現在ヒート背番号)
        Me.Panel_ヒート表.Controls.Add(Me.TB_次組数)
        Me.Panel_ヒート表.Controls.Add(Me.TB_前組数)
        Me.Panel_ヒート表.Controls.Add(Me.TB_現組数)
        Me.Panel_ヒート表.Controls.Add(Me.TB_前種目)
        Me.Panel_ヒート表.Controls.Add(Me.TB_前ヒート番号)
        Me.Panel_ヒート表.Controls.Add(Me.TB_次種目)
        Me.Panel_ヒート表.Controls.Add(Me.TB_現種目)
        Me.Panel_ヒート表.Controls.Add(Me.TB_次ヒート番号)
        Me.Panel_ヒート表.Controls.Add(Me.PB_開始)
        Me.Panel_ヒート表.Controls.Add(Me.TB_現在ヒート番号)
        Me.Panel_ヒート表.Location = New System.Drawing.Point(278, 151)
        Me.Panel_ヒート表.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel_ヒート表.Name = "Panel_ヒート表"
        Me.Panel_ヒート表.Size = New System.Drawing.Size(467, 341)
        Me.Panel_ヒート表.TabIndex = 10
        '
        'PB_次ヒート背番号
        '
        Me.PB_次ヒート背番号.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_次ヒート背番号.BackColor = System.Drawing.Color.LemonChiffon
        Me.PB_次ヒート背番号.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_次ヒート背番号.Location = New System.Drawing.Point(127, 261)
        Me.PB_次ヒート背番号.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_次ヒート背番号.Name = "PB_次ヒート背番号"
        Me.PB_次ヒート背番号.Size = New System.Drawing.Size(327, 91)
        Me.PB_次ヒート背番号.TabIndex = 23
        Me.PB_次ヒート背番号.Text = "999   999   999   999   999   999  999   999   999   999   999   999"
        Me.PB_次ヒート背番号.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.PB_次ヒート背番号.UseVisualStyleBackColor = False
        '
        'PB_前ヒート背番号
        '
        Me.PB_前ヒート背番号.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_前ヒート背番号.BackColor = System.Drawing.Color.LightGray
        Me.PB_前ヒート背番号.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_前ヒート背番号.Location = New System.Drawing.Point(127, 14)
        Me.PB_前ヒート背番号.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_前ヒート背番号.Name = "PB_前ヒート背番号"
        Me.PB_前ヒート背番号.Size = New System.Drawing.Size(327, 91)
        Me.PB_前ヒート背番号.TabIndex = 22
        Me.PB_前ヒート背番号.Text = "999   999   999   999   999   999  999   999   999   999   999   999"
        Me.PB_前ヒート背番号.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.PB_前ヒート背番号.UseVisualStyleBackColor = False
        '
        'PB_現在ヒート背番号
        '
        Me.PB_現在ヒート背番号.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_現在ヒート背番号.BackColor = System.Drawing.Color.LemonChiffon
        Me.PB_現在ヒート背番号.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_現在ヒート背番号.Location = New System.Drawing.Point(127, 139)
        Me.PB_現在ヒート背番号.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_現在ヒート背番号.Name = "PB_現在ヒート背番号"
        Me.PB_現在ヒート背番号.Size = New System.Drawing.Size(327, 91)
        Me.PB_現在ヒート背番号.TabIndex = 16
        Me.PB_現在ヒート背番号.Text = "999   999   999   999   999   999  999   999   999   999   999   999"
        Me.PB_現在ヒート背番号.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.PB_現在ヒート背番号.UseVisualStyleBackColor = False
        '
        'TB_次組数
        '
        Me.TB_次組数.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TB_次組数.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_次組数.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_次組数.Location = New System.Drawing.Point(56, 304)
        Me.TB_次組数.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_次組数.Multiline = True
        Me.TB_次組数.Name = "TB_次組数"
        Me.TB_次組数.ReadOnly = True
        Me.TB_次組数.Size = New System.Drawing.Size(62, 33)
        Me.TB_次組数.TabIndex = 21
        Me.TB_次組数.Text = "12組"
        Me.TB_次組数.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_前組数
        '
        Me.TB_前組数.BackColor = System.Drawing.Color.LightGray
        Me.TB_前組数.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_前組数.Location = New System.Drawing.Point(56, 58)
        Me.TB_前組数.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_前組数.Multiline = True
        Me.TB_前組数.Name = "TB_前組数"
        Me.TB_前組数.ReadOnly = True
        Me.TB_前組数.Size = New System.Drawing.Size(62, 33)
        Me.TB_前組数.TabIndex = 21
        Me.TB_前組数.Text = "12組"
        Me.TB_前組数.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_現組数
        '
        Me.TB_現組数.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_現組数.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_現組数.Location = New System.Drawing.Point(61, 188)
        Me.TB_現組数.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_現組数.Multiline = True
        Me.TB_現組数.Name = "TB_現組数"
        Me.TB_現組数.ReadOnly = True
        Me.TB_現組数.Size = New System.Drawing.Size(62, 33)
        Me.TB_現組数.TabIndex = 21
        Me.TB_現組数.Text = "12組"
        Me.TB_現組数.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_前種目
        '
        Me.TB_前種目.BackColor = System.Drawing.Color.LightGray
        Me.TB_前種目.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_前種目.Location = New System.Drawing.Point(10, 14)
        Me.TB_前種目.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_前種目.Multiline = True
        Me.TB_前種目.Name = "TB_前種目"
        Me.TB_前種目.ReadOnly = True
        Me.TB_前種目.Size = New System.Drawing.Size(48, 33)
        Me.TB_前種目.TabIndex = 20
        Me.TB_前種目.Text = "W"
        Me.TB_前種目.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_前ヒート番号
        '
        Me.TB_前ヒート番号.BackColor = System.Drawing.Color.LightGray
        Me.TB_前ヒート番号.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_前ヒート番号.Location = New System.Drawing.Point(70, 14)
        Me.TB_前ヒート番号.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_前ヒート番号.Multiline = True
        Me.TB_前ヒート番号.Name = "TB_前ヒート番号"
        Me.TB_前ヒート番号.ReadOnly = True
        Me.TB_前ヒート番号.Size = New System.Drawing.Size(48, 33)
        Me.TB_前ヒート番号.TabIndex = 19
        Me.TB_前ヒート番号.Text = "9H"
        Me.TB_前ヒート番号.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_次種目
        '
        Me.TB_次種目.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TB_次種目.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_次種目.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_次種目.Location = New System.Drawing.Point(10, 262)
        Me.TB_次種目.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_次種目.Multiline = True
        Me.TB_次種目.Name = "TB_次種目"
        Me.TB_次種目.ReadOnly = True
        Me.TB_次種目.Size = New System.Drawing.Size(48, 33)
        Me.TB_次種目.TabIndex = 17
        Me.TB_次種目.Text = "W"
        Me.TB_次種目.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_現種目
        '
        Me.TB_現種目.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_現種目.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_現種目.Location = New System.Drawing.Point(10, 140)
        Me.TB_現種目.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_現種目.Multiline = True
        Me.TB_現種目.Name = "TB_現種目"
        Me.TB_現種目.ReadOnly = True
        Me.TB_現種目.Size = New System.Drawing.Size(48, 33)
        Me.TB_現種目.TabIndex = 16
        Me.TB_現種目.Text = "W"
        Me.TB_現種目.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_次ヒート番号
        '
        Me.TB_次ヒート番号.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TB_次ヒート番号.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_次ヒート番号.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_次ヒート番号.Location = New System.Drawing.Point(70, 262)
        Me.TB_次ヒート番号.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_次ヒート番号.Multiline = True
        Me.TB_次ヒート番号.Name = "TB_次ヒート番号"
        Me.TB_次ヒート番号.ReadOnly = True
        Me.TB_次ヒート番号.Size = New System.Drawing.Size(48, 33)
        Me.TB_次ヒート番号.TabIndex = 15
        Me.TB_次ヒート番号.Text = "11H"
        Me.TB_次ヒート番号.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PB_開始
        '
        Me.PB_開始.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_開始.Location = New System.Drawing.Point(2, 184)
        Me.PB_開始.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_開始.Name = "PB_開始"
        Me.PB_開始.Size = New System.Drawing.Size(55, 40)
        Me.PB_開始.TabIndex = 13
        Me.PB_開始.Text = "開始"
        Me.PB_開始.UseVisualStyleBackColor = True
        '
        'TB_現在ヒート番号
        '
        Me.TB_現在ヒート番号.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_現在ヒート番号.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_現在ヒート番号.Location = New System.Drawing.Point(70, 140)
        Me.TB_現在ヒート番号.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TB_現在ヒート番号.Multiline = True
        Me.TB_現在ヒート番号.Name = "TB_現在ヒート番号"
        Me.TB_現在ヒート番号.ReadOnly = True
        Me.TB_現在ヒート番号.Size = New System.Drawing.Size(48, 33)
        Me.TB_現在ヒート番号.TabIndex = 12
        Me.TB_現在ヒート番号.Text = "10H"
        Me.TB_現在ヒート番号.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PB_進行管理
        '
        Me.PB_進行管理.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_進行管理.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_進行管理.Location = New System.Drawing.Point(24, 513)
        Me.PB_進行管理.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_進行管理.Name = "PB_進行管理"
        Me.PB_進行管理.Size = New System.Drawing.Size(152, 40)
        Me.PB_進行管理.TabIndex = 14
        Me.PB_進行管理.Text = "進行管理詳細へ"
        Me.PB_進行管理.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button1.Location = New System.Drawing.Point(320, 513)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(124, 40)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "審判員リスト"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label1.Location = New System.Drawing.Point(488, 494)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(279, 14)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "※ヒート表をクリックすると名簿が表示されます"
        '
        'PB_結果表示
        '
        Me.PB_結果表示.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_結果表示.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_結果表示.Location = New System.Drawing.Point(193, 513)
        Me.PB_結果表示.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_結果表示.Name = "PB_結果表示"
        Me.PB_結果表示.Size = New System.Drawing.Size(116, 40)
        Me.PB_結果表示.TabIndex = 17
        Me.PB_結果表示.Text = "結果表示"
        Me.PB_結果表示.UseVisualStyleBackColor = True
        '
        'F600_進行管理
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(754, 578)
        Me.Controls.Add(Me.PB_結果表示)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PB_進行管理)
        Me.Controls.Add(Me.Panel_ヒート表)
        Me.Controls.Add(Me.TB_GOSTOP)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.TB_減点確認中)
        Me.Controls.Add(Me.TB_タイマー)
        Me.Controls.Add(Me.TB_次次競技)
        Me.Controls.Add(Me.TB_次競技)
        Me.Controls.Add(Me.DGV_ヒート)
        Me.Controls.Add(Me.DGV_種目リスト)
        Me.Controls.Add(Me.TB_現在競技)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "F600_進行管理"
        Me.Text = "F600_進行管理"
        CType(Me.DGV_種目リスト, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_ヒート, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel_ヒート表.ResumeLayout(False)
        Me.Panel_ヒート表.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TB_現在競技 As TextBox
    Friend WithEvents DGV_種目リスト As DataGridView
    Friend WithEvents DGV_ヒート As DataGridView
    Friend WithEvents TB_次競技 As TextBox
    Friend WithEvents TB_次次競技 As TextBox
    Friend WithEvents TB_タイマー As TextBox
    Friend WithEvents TB_減点確認中 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TB_UP数 As TextBox
    Friend WithEvents TB_採点方式 As TextBox
    Friend WithEvents TB_ヒート数 As TextBox
    Friend WithEvents TB_出場組数 As TextBox
    Friend WithEvents TB_GOSTOP As TextBox
    Friend WithEvents Panel_ヒート表 As Panel
    Friend WithEvents TB_前種目 As TextBox
    Friend WithEvents TB_前ヒート番号 As TextBox
    Friend WithEvents TB_次種目 As TextBox
    Friend WithEvents TB_現種目 As TextBox
    Friend WithEvents TB_次ヒート番号 As TextBox
    Friend WithEvents PB_開始 As Button
    Friend WithEvents TB_現在ヒート番号 As TextBox
    Friend WithEvents TB_次組数 As TextBox
    Friend WithEvents TB_前組数 As TextBox
    Friend WithEvents TB_現組数 As TextBox
    Friend WithEvents PB_進行管理 As Button
    Friend WithEvents PB_次ヒート背番号 As Button
    Friend WithEvents PB_前ヒート背番号 As Button
    Friend WithEvents PB_現在ヒート背番号 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents 種目 As DataGridViewTextBoxColumn
    Friend WithEvents ヒート As DataGridViewTextBoxColumn
    Friend WithEvents TB_種目数 As TextBox
    Friend WithEvents PB_結果表示 As Button
End Class
