<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_GM
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.CB_進行番号 = New System.Windows.Forms.ComboBox()
        Me.LB_LOG = New System.Windows.Forms.ListBox()
        Me.DGV_種目 = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DGV_ヒート = New System.Windows.Forms.DataGridView()
        Me.H_No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.H_背番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PB_全JS = New System.Windows.Forms.Button()
        Me.PB_次ヒート = New System.Windows.Forms.Button()
        Me.LB_区分名 = New System.Windows.Forms.Label()
        Me.LB_ラウンド = New System.Windows.Forms.Label()
        Me.PB_選択JS = New System.Windows.Forms.Button()
        Me.PB_タイマー = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.RB_Up = New System.Windows.Forms.RadioButton()
        Me.RB_Down = New System.Windows.Forms.RadioButton()
        Me.ヒート = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.種目 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ジャッジ名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ジャッジ記号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DGV_ジャッジ = New System.Windows.Forms.DataGridView()
        Me.CB_カウントDown = New System.Windows.Forms.ComboBox()
        Me.LB_減点確認中 = New System.Windows.Forms.Label()
        Me.PB_GOSTOP = New System.Windows.Forms.Button()
        Me.PB_区分更新 = New System.Windows.Forms.Button()
        Me.PB_前ヒート = New System.Windows.Forms.Button()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.PB_終了 = New System.Windows.Forms.Button()
        Me.LB_現在種目ヒート = New System.Windows.Forms.Label()
        Me.PB_Refresh = New System.Windows.Forms.Button()
        Me.PB_JS確認 = New System.Windows.Forms.Button()
        Me.PB_関連一覧 = New System.Windows.Forms.Button()
        CType(Me.DGV_種目, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_ヒート, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_ジャッジ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CB_進行番号
        '
        Me.CB_進行番号.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CB_進行番号.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_進行番号.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CB_進行番号.FormattingEnabled = True
        Me.CB_進行番号.Location = New System.Drawing.Point(9, 43)
        Me.CB_進行番号.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CB_進行番号.Name = "CB_進行番号"
        Me.CB_進行番号.Size = New System.Drawing.Size(321, 24)
        Me.CB_進行番号.TabIndex = 0
        '
        'LB_LOG
        '
        Me.LB_LOG.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_LOG.FormattingEnabled = True
        Me.LB_LOG.ItemHeight = 12
        Me.LB_LOG.Location = New System.Drawing.Point(9, 605)
        Me.LB_LOG.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.LB_LOG.MaximumSize = New System.Drawing.Size(1000, 100)
        Me.LB_LOG.Name = "LB_LOG"
        Me.LB_LOG.Size = New System.Drawing.Size(258, 52)
        Me.LB_LOG.TabIndex = 10
        '
        'DGV_種目
        '
        Me.DGV_種目.AllowUserToAddRows = False
        Me.DGV_種目.AllowUserToDeleteRows = False
        Me.DGV_種目.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_種目.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_種目.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.Dance})
        Me.DGV_種目.Location = New System.Drawing.Point(9, 70)
        Me.DGV_種目.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DGV_種目.MultiSelect = False
        Me.DGV_種目.Name = "DGV_種目"
        Me.DGV_種目.RowHeadersVisible = False
        Me.DGV_種目.RowHeadersWidth = 51
        Me.DGV_種目.RowTemplate.Height = 24
        Me.DGV_種目.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_種目.Size = New System.Drawing.Size(115, 180)
        Me.DGV_種目.TabIndex = 12
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.MinimumWidth = 6
        Me.No.Name = "No"
        Me.No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.No.Width = 30
        '
        'Dance
        '
        Me.Dance.HeaderText = "種目"
        Me.Dance.MinimumWidth = 6
        Me.Dance.Name = "Dance"
        Me.Dance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Dance.Width = 125
        '
        'DGV_ヒート
        '
        Me.DGV_ヒート.AllowUserToAddRows = False
        Me.DGV_ヒート.AllowUserToDeleteRows = False
        Me.DGV_ヒート.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_ヒート.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_ヒート.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_ヒート.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.H_No, Me.H_背番号})
        Me.DGV_ヒート.Location = New System.Drawing.Point(128, 70)
        Me.DGV_ヒート.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DGV_ヒート.MultiSelect = False
        Me.DGV_ヒート.Name = "DGV_ヒート"
        Me.DGV_ヒート.RowHeadersVisible = False
        Me.DGV_ヒート.RowHeadersWidth = 51
        Me.DGV_ヒート.RowTemplate.Height = 24
        Me.DGV_ヒート.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_ヒート.Size = New System.Drawing.Size(202, 180)
        Me.DGV_ヒート.TabIndex = 13
        '
        'H_No
        '
        Me.H_No.HeaderText = "No"
        Me.H_No.MinimumWidth = 6
        Me.H_No.Name = "H_No"
        Me.H_No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.H_No.Width = 30
        '
        'H_背番号
        '
        Me.H_背番号.HeaderText = "背番号"
        Me.H_背番号.MinimumWidth = 6
        Me.H_背番号.Name = "H_背番号"
        Me.H_背番号.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.H_背番号.Width = 200
        '
        'PB_全JS
        '
        Me.PB_全JS.Font = New System.Drawing.Font("MS UI Gothic", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_全JS.Location = New System.Drawing.Point(9, 252)
        Me.PB_全JS.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_全JS.Name = "PB_全JS"
        Me.PB_全JS.Size = New System.Drawing.Size(87, 24)
        Me.PB_全JS.TabIndex = 15
        Me.PB_全JS.Text = "全JS変更"
        Me.PB_全JS.UseVisualStyleBackColor = True
        '
        'PB_次ヒート
        '
        Me.PB_次ヒート.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_次ヒート.Location = New System.Drawing.Point(277, 253)
        Me.PB_次ヒート.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_次ヒート.Name = "PB_次ヒート"
        Me.PB_次ヒート.Size = New System.Drawing.Size(52, 22)
        Me.PB_次ヒート.TabIndex = 16
        Me.PB_次ヒート.Text = "次ヒート"
        Me.PB_次ヒート.UseVisualStyleBackColor = True
        '
        'LB_区分名
        '
        Me.LB_区分名.AutoSize = True
        Me.LB_区分名.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分名.ForeColor = System.Drawing.Color.Blue
        Me.LB_区分名.Location = New System.Drawing.Point(9, 7)
        Me.LB_区分名.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LB_区分名.Name = "LB_区分名"
        Me.LB_区分名.Size = New System.Drawing.Size(126, 16)
        Me.LB_区分名.TabIndex = 17
        Me.LB_区分名.Text = "Not Approcable"
        '
        'LB_ラウンド
        '
        Me.LB_ラウンド.AutoSize = True
        Me.LB_ラウンド.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_ラウンド.ForeColor = System.Drawing.Color.Blue
        Me.LB_ラウンド.Location = New System.Drawing.Point(9, 23)
        Me.LB_ラウンド.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LB_ラウンド.Name = "LB_ラウンド"
        Me.LB_ラウンド.Size = New System.Drawing.Size(0, 16)
        Me.LB_ラウンド.TabIndex = 18
        '
        'PB_選択JS
        '
        Me.PB_選択JS.Font = New System.Drawing.Font("MS UI Gothic", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_選択JS.Location = New System.Drawing.Point(100, 251)
        Me.PB_選択JS.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_選択JS.Name = "PB_選択JS"
        Me.PB_選択JS.Size = New System.Drawing.Size(101, 24)
        Me.PB_選択JS.TabIndex = 19
        Me.PB_選択JS.Text = "選択JS変更"
        Me.PB_選択JS.UseVisualStyleBackColor = True
        '
        'PB_タイマー
        '
        Me.PB_タイマー.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_タイマー.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_タイマー.Location = New System.Drawing.Point(5, 663)
        Me.PB_タイマー.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_タイマー.Name = "PB_タイマー"
        Me.PB_タイマー.Size = New System.Drawing.Size(144, 56)
        Me.PB_タイマー.TabIndex = 20
        Me.PB_タイマー.Text = "  99:99"
        Me.PB_タイマー.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'RB_Up
        '
        Me.RB_Up.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RB_Up.AutoSize = True
        Me.RB_Up.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RB_Up.Location = New System.Drawing.Point(184, 662)
        Me.RB_Up.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RB_Up.Name = "RB_Up"
        Me.RB_Up.Size = New System.Drawing.Size(79, 16)
        Me.RB_Up.TabIndex = 21
        Me.RB_Up.TabStop = True
        Me.RB_Up.Text = "カウントUP"
        Me.RB_Up.UseVisualStyleBackColor = True
        '
        'RB_Down
        '
        Me.RB_Down.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RB_Down.AutoSize = True
        Me.RB_Down.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RB_Down.Location = New System.Drawing.Point(184, 681)
        Me.RB_Down.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RB_Down.Name = "RB_Down"
        Me.RB_Down.Size = New System.Drawing.Size(99, 16)
        Me.RB_Down.TabIndex = 22
        Me.RB_Down.TabStop = True
        Me.RB_Down.Text = "カウントDOWN"
        Me.RB_Down.UseVisualStyleBackColor = True
        '
        'ヒート
        '
        Me.ヒート.HeaderText = "ヒート"
        Me.ヒート.MinimumWidth = 6
        Me.ヒート.Name = "ヒート"
        Me.ヒート.ReadOnly = True
        Me.ヒート.Width = 70
        '
        '種目
        '
        Me.種目.HeaderText = "種目"
        Me.種目.MinimumWidth = 6
        Me.種目.Name = "種目"
        Me.種目.ReadOnly = True
        Me.種目.Width = 60
        '
        'ジャッジ名
        '
        Me.ジャッジ名.HeaderText = "ジャッジ名"
        Me.ジャッジ名.MinimumWidth = 6
        Me.ジャッジ名.Name = "ジャッジ名"
        Me.ジャッジ名.ReadOnly = True
        Me.ジャッジ名.Width = 150
        '
        'ジャッジ記号
        '
        Me.ジャッジ記号.HeaderText = "No"
        Me.ジャッジ記号.MinimumWidth = 6
        Me.ジャッジ記号.Name = "ジャッジ記号"
        Me.ジャッジ記号.ReadOnly = True
        Me.ジャッジ記号.Width = 40
        '
        'DGV_ジャッジ
        '
        Me.DGV_ジャッジ.AllowUserToAddRows = False
        Me.DGV_ジャッジ.AllowUserToDeleteRows = False
        Me.DGV_ジャッジ.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_ジャッジ.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_ジャッジ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_ジャッジ.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ジャッジ記号, Me.ジャッジ名, Me.種目, Me.ヒート})
        Me.DGV_ジャッジ.Location = New System.Drawing.Point(9, 303)
        Me.DGV_ジャッジ.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DGV_ジャッジ.Name = "DGV_ジャッジ"
        Me.DGV_ジャッジ.RowHeadersVisible = False
        Me.DGV_ジャッジ.RowHeadersWidth = 51
        Me.DGV_ジャッジ.RowTemplate.Height = 24
        Me.DGV_ジャッジ.Size = New System.Drawing.Size(323, 294)
        Me.DGV_ジャッジ.TabIndex = 11
        '
        'CB_カウントDown
        '
        Me.CB_カウントDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CB_カウントDown.FormattingEnabled = True
        Me.CB_カウントDown.Location = New System.Drawing.Point(219, 704)
        Me.CB_カウントDown.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CB_カウントDown.Name = "CB_カウントDown"
        Me.CB_カウントDown.Size = New System.Drawing.Size(92, 20)
        Me.CB_カウントDown.TabIndex = 23
        '
        'LB_減点確認中
        '
        Me.LB_減点確認中.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LB_減点確認中.AutoSize = True
        Me.LB_減点確認中.BackColor = System.Drawing.SystemColors.Info
        Me.LB_減点確認中.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_減点確認中.Location = New System.Drawing.Point(18, 737)
        Me.LB_減点確認中.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LB_減点確認中.Name = "LB_減点確認中"
        Me.LB_減点確認中.Size = New System.Drawing.Size(130, 24)
        Me.LB_減点確認中.TabIndex = 24
        Me.LB_減点確認中.Text = "減点確認中"
        '
        'PB_GOSTOP
        '
        Me.PB_GOSTOP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_GOSTOP.BackColor = System.Drawing.Color.Red
        Me.PB_GOSTOP.Font = New System.Drawing.Font("MS UI Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_GOSTOP.ForeColor = System.Drawing.Color.White
        Me.PB_GOSTOP.Location = New System.Drawing.Point(174, 727)
        Me.PB_GOSTOP.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_GOSTOP.Name = "PB_GOSTOP"
        Me.PB_GOSTOP.Size = New System.Drawing.Size(149, 44)
        Me.PB_GOSTOP.TabIndex = 25
        Me.PB_GOSTOP.Text = "進行STOP"
        Me.PB_GOSTOP.UseVisualStyleBackColor = False
        '
        'PB_区分更新
        '
        Me.PB_区分更新.Font = New System.Drawing.Font("MS UI Gothic", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_区分更新.Location = New System.Drawing.Point(272, 14)
        Me.PB_区分更新.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_区分更新.Name = "PB_区分更新"
        Me.PB_区分更新.Size = New System.Drawing.Size(58, 25)
        Me.PB_区分更新.TabIndex = 26
        Me.PB_区分更新.Text = "更新"
        Me.PB_区分更新.UseVisualStyleBackColor = True
        '
        'PB_前ヒート
        '
        Me.PB_前ヒート.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_前ヒート.Location = New System.Drawing.Point(220, 253)
        Me.PB_前ヒート.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_前ヒート.Name = "PB_前ヒート"
        Me.PB_前ヒート.Size = New System.Drawing.Size(52, 22)
        Me.PB_前ヒート.TabIndex = 27
        Me.PB_前ヒート.Text = "前ヒート"
        Me.PB_前ヒート.UseVisualStyleBackColor = True
        '
        'Timer2
        '
        '
        'PB_終了
        '
        Me.PB_終了.Font = New System.Drawing.Font("MS UI Gothic", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_終了.Location = New System.Drawing.Point(11, 280)
        Me.PB_終了.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_終了.Name = "PB_終了"
        Me.PB_終了.Size = New System.Drawing.Size(85, 21)
        Me.PB_終了.TabIndex = 28
        Me.PB_終了.Text = "JS終了"
        Me.PB_終了.UseVisualStyleBackColor = True
        '
        'LB_現在種目ヒート
        '
        Me.LB_現在種目ヒート.AutoSize = True
        Me.LB_現在種目ヒート.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_現在種目ヒート.ForeColor = System.Drawing.Color.Blue
        Me.LB_現在種目ヒート.Location = New System.Drawing.Point(177, 283)
        Me.LB_現在種目ヒート.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LB_現在種目ヒート.Name = "LB_現在種目ヒート"
        Me.LB_現在種目ヒート.Size = New System.Drawing.Size(58, 16)
        Me.LB_現在種目ヒート.TabIndex = 29
        Me.LB_現在種目ヒート.Text = "Label1"
        '
        'PB_Refresh
        '
        Me.PB_Refresh.Location = New System.Drawing.Point(117, 280)
        Me.PB_Refresh.Name = "PB_Refresh"
        Me.PB_Refresh.Size = New System.Drawing.Size(55, 21)
        Me.PB_Refresh.TabIndex = 30
        Me.PB_Refresh.Text = "JS更新"
        Me.PB_Refresh.UseVisualStyleBackColor = True
        '
        'PB_JS確認
        '
        Me.PB_JS確認.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_JS確認.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_JS確認.Location = New System.Drawing.Point(271, 605)
        Me.PB_JS確認.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_JS確認.Name = "PB_JS確認"
        Me.PB_JS確認.Size = New System.Drawing.Size(58, 26)
        Me.PB_JS確認.TabIndex = 31
        Me.PB_JS確認.Text = "JS確認"
        Me.PB_JS確認.UseVisualStyleBackColor = True
        '
        'PB_関連一覧
        '
        Me.PB_関連一覧.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_関連一覧.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_関連一覧.Location = New System.Drawing.Point(271, 634)
        Me.PB_関連一覧.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_関連一覧.Name = "PB_関連一覧"
        Me.PB_関連一覧.Size = New System.Drawing.Size(58, 26)
        Me.PB_関連一覧.TabIndex = 32
        Me.PB_関連一覧.Text = "関連"
        Me.PB_関連一覧.UseVisualStyleBackColor = True
        '
        'F_GM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(338, 771)
        Me.Controls.Add(Me.PB_関連一覧)
        Me.Controls.Add(Me.PB_JS確認)
        Me.Controls.Add(Me.PB_Refresh)
        Me.Controls.Add(Me.LB_現在種目ヒート)
        Me.Controls.Add(Me.PB_終了)
        Me.Controls.Add(Me.PB_前ヒート)
        Me.Controls.Add(Me.PB_区分更新)
        Me.Controls.Add(Me.PB_GOSTOP)
        Me.Controls.Add(Me.LB_減点確認中)
        Me.Controls.Add(Me.CB_カウントDown)
        Me.Controls.Add(Me.RB_Down)
        Me.Controls.Add(Me.RB_Up)
        Me.Controls.Add(Me.PB_タイマー)
        Me.Controls.Add(Me.PB_選択JS)
        Me.Controls.Add(Me.LB_ラウンド)
        Me.Controls.Add(Me.LB_区分名)
        Me.Controls.Add(Me.PB_次ヒート)
        Me.Controls.Add(Me.PB_全JS)
        Me.Controls.Add(Me.DGV_ヒート)
        Me.Controls.Add(Me.DGV_種目)
        Me.Controls.Add(Me.DGV_ジャッジ)
        Me.Controls.Add(Me.LB_LOG)
        Me.Controls.Add(Me.CB_進行番号)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "F_GM"
        Me.Text = "F_GM"
        CType(Me.DGV_種目, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_ヒート, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_ジャッジ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CB_進行番号 As ComboBox
    Friend WithEvents LB_LOG As ListBox
    Friend WithEvents DGV_種目 As DataGridView
    Friend WithEvents DGV_ヒート As DataGridView
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents Dance As DataGridViewTextBoxColumn
    Friend WithEvents H_No As DataGridViewTextBoxColumn
    Friend WithEvents H_背番号 As DataGridViewTextBoxColumn
    Friend WithEvents PB_全JS As Button
    Friend WithEvents PB_次ヒート As Button
    Friend WithEvents LB_区分名 As Label
    Friend WithEvents LB_ラウンド As Label
    Friend WithEvents PB_選択JS As Button
    Friend WithEvents PB_タイマー As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents RB_Up As RadioButton
    Friend WithEvents RB_Down As RadioButton
    Friend WithEvents ヒート As DataGridViewTextBoxColumn
    Friend WithEvents 種目 As DataGridViewTextBoxColumn
    Friend WithEvents ジャッジ名 As DataGridViewTextBoxColumn
    Friend WithEvents ジャッジ記号 As DataGridViewTextBoxColumn
    Friend WithEvents DGV_ジャッジ As DataGridView
    Friend WithEvents CB_カウントDown As ComboBox
    Friend WithEvents LB_減点確認中 As Label
    Friend WithEvents PB_GOSTOP As Button
    Friend WithEvents PB_区分更新 As Button
    Friend WithEvents PB_前ヒート As Button
    Friend WithEvents Timer2 As Timer
    Friend WithEvents PB_終了 As Button
    Friend WithEvents LB_現在種目ヒート As Label
    Friend WithEvents PB_Refresh As Button
    Friend WithEvents PB_JS確認 As Button
    Friend WithEvents PB_関連一覧 As Button
End Class
