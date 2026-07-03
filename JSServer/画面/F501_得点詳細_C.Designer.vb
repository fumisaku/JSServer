<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F501_得点詳細_C
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
        Me.components = New System.ComponentModel.Container()
        Me.TabControl_詳細 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DGV_01 = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DGV_02 = New System.Windows.Forms.DataGridView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.DGV_03 = New System.Windows.Forms.DataGridView()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.DGV_04 = New System.Windows.Forms.DataGridView()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.DGV_05 = New System.Windows.Forms.DataGridView()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.DGV_06 = New System.Windows.Forms.DataGridView()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.DGV_07 = New System.Windows.Forms.DataGridView()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.DGV_08 = New System.Windows.Forms.DataGridView()
        Me.TabPage9 = New System.Windows.Forms.TabPage()
        Me.DGV_09 = New System.Windows.Forms.DataGridView()
        Me.TabPage10 = New System.Windows.Forms.TabPage()
        Me.DGV_10 = New System.Windows.Forms.DataGridView()
        Me.DGV_総合 = New System.Windows.Forms.DataGridView()
        Me.PB_更新 = New System.Windows.Forms.Button()
        Me.LB_区分名 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PB_UP数確定 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TB_ステータス = New System.Windows.Forms.TextBox()
        Me.CB_自動更新 = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TB_CaliMin = New System.Windows.Forms.TextBox()
        Me.TB_CaliMax = New System.Windows.Forms.TextBox()
        Me.PB_Cali設定 = New System.Windows.Forms.Button()
        Me.PB_欠場 = New System.Windows.Forms.Button()
        Me.CB_リアル更新 = New System.Windows.Forms.CheckBox()
        Me.TB_リアルステータス = New System.Windows.Forms.TextBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl_詳細.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DGV_01, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.DGV_02, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.DGV_03, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.DGV_04, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.DGV_05, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        CType(Me.DGV_06, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage7.SuspendLayout()
        CType(Me.DGV_07, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage8.SuspendLayout()
        CType(Me.DGV_08, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage9.SuspendLayout()
        CType(Me.DGV_09, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage10.SuspendLayout()
        CType(Me.DGV_10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_総合, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl_詳細
        '
        Me.TabControl_詳細.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl_詳細.Controls.Add(Me.TabPage1)
        Me.TabControl_詳細.Controls.Add(Me.TabPage2)
        Me.TabControl_詳細.Controls.Add(Me.TabPage3)
        Me.TabControl_詳細.Controls.Add(Me.TabPage4)
        Me.TabControl_詳細.Controls.Add(Me.TabPage5)
        Me.TabControl_詳細.Controls.Add(Me.TabPage6)
        Me.TabControl_詳細.Controls.Add(Me.TabPage7)
        Me.TabControl_詳細.Controls.Add(Me.TabPage8)
        Me.TabControl_詳細.Controls.Add(Me.TabPage9)
        Me.TabControl_詳細.Controls.Add(Me.TabPage10)
        Me.TabControl_詳細.Font = New System.Drawing.Font("MS UI Gothic", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TabControl_詳細.Location = New System.Drawing.Point(32, 354)
        Me.TabControl_詳細.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabControl_詳細.Name = "TabControl_詳細"
        Me.TabControl_詳細.SelectedIndex = 0
        Me.TabControl_詳細.Size = New System.Drawing.Size(1240, 428)
        Me.TabControl_詳細.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DGV_01)
        Me.TabPage1.Location = New System.Drawing.Point(4, 28)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage1.Size = New System.Drawing.Size(1232, 396)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DGV_01
        '
        Me.DGV_01.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_01.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGV_01.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_01.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_01.Location = New System.Drawing.Point(3, 2)
        Me.DGV_01.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_01.Name = "DGV_01"
        Me.DGV_01.RowHeadersWidth = 51
        Me.DGV_01.RowTemplate.Height = 24
        Me.DGV_01.Size = New System.Drawing.Size(1232, 390)
        Me.DGV_01.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DGV_02)
        Me.TabPage2.Location = New System.Drawing.Point(4, 28)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage2.Size = New System.Drawing.Size(1232, 396)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DGV_02
        '
        Me.DGV_02.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_02.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_02.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_02.Location = New System.Drawing.Point(3, 2)
        Me.DGV_02.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_02.Name = "DGV_02"
        Me.DGV_02.RowHeadersWidth = 51
        Me.DGV_02.RowTemplate.Height = 24
        Me.DGV_02.Size = New System.Drawing.Size(1226, 392)
        Me.DGV_02.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.DGV_03)
        Me.TabPage3.Location = New System.Drawing.Point(4, 28)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage3.Size = New System.Drawing.Size(1232, 396)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'DGV_03
        '
        Me.DGV_03.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_03.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_03.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_03.Location = New System.Drawing.Point(3, 2)
        Me.DGV_03.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_03.Name = "DGV_03"
        Me.DGV_03.RowHeadersWidth = 51
        Me.DGV_03.RowTemplate.Height = 24
        Me.DGV_03.Size = New System.Drawing.Size(1226, 392)
        Me.DGV_03.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.DGV_04)
        Me.TabPage4.Location = New System.Drawing.Point(4, 28)
        Me.TabPage4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage4.Size = New System.Drawing.Size(1232, 396)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "TabPage4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'DGV_04
        '
        Me.DGV_04.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_04.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_04.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_04.Location = New System.Drawing.Point(3, 2)
        Me.DGV_04.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_04.Name = "DGV_04"
        Me.DGV_04.RowHeadersWidth = 51
        Me.DGV_04.RowTemplate.Height = 24
        Me.DGV_04.Size = New System.Drawing.Size(1226, 392)
        Me.DGV_04.TabIndex = 0
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.DGV_05)
        Me.TabPage5.Location = New System.Drawing.Point(4, 28)
        Me.TabPage5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage5.Size = New System.Drawing.Size(1232, 396)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "TabPage5"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'DGV_05
        '
        Me.DGV_05.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_05.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_05.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_05.Location = New System.Drawing.Point(3, 2)
        Me.DGV_05.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_05.Name = "DGV_05"
        Me.DGV_05.RowHeadersWidth = 51
        Me.DGV_05.RowTemplate.Height = 24
        Me.DGV_05.Size = New System.Drawing.Size(1226, 392)
        Me.DGV_05.TabIndex = 0
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.DGV_06)
        Me.TabPage6.Location = New System.Drawing.Point(4, 28)
        Me.TabPage6.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage6.Size = New System.Drawing.Size(1232, 396)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "TabPage6"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'DGV_06
        '
        Me.DGV_06.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_06.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_06.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_06.Location = New System.Drawing.Point(3, 2)
        Me.DGV_06.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_06.Name = "DGV_06"
        Me.DGV_06.RowHeadersWidth = 51
        Me.DGV_06.RowTemplate.Height = 24
        Me.DGV_06.Size = New System.Drawing.Size(1226, 392)
        Me.DGV_06.TabIndex = 0
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.DGV_07)
        Me.TabPage7.Location = New System.Drawing.Point(4, 28)
        Me.TabPage7.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage7.Size = New System.Drawing.Size(1232, 396)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "TabPage7"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'DGV_07
        '
        Me.DGV_07.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_07.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_07.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_07.Location = New System.Drawing.Point(3, 2)
        Me.DGV_07.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_07.Name = "DGV_07"
        Me.DGV_07.RowHeadersWidth = 51
        Me.DGV_07.RowTemplate.Height = 24
        Me.DGV_07.Size = New System.Drawing.Size(1226, 392)
        Me.DGV_07.TabIndex = 0
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.DGV_08)
        Me.TabPage8.Location = New System.Drawing.Point(4, 28)
        Me.TabPage8.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage8.Size = New System.Drawing.Size(1232, 396)
        Me.TabPage8.TabIndex = 7
        Me.TabPage8.Text = "TabPage8"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'DGV_08
        '
        Me.DGV_08.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_08.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_08.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_08.Location = New System.Drawing.Point(3, 2)
        Me.DGV_08.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_08.Name = "DGV_08"
        Me.DGV_08.RowHeadersWidth = 51
        Me.DGV_08.RowTemplate.Height = 24
        Me.DGV_08.Size = New System.Drawing.Size(1226, 392)
        Me.DGV_08.TabIndex = 0
        '
        'TabPage9
        '
        Me.TabPage9.Controls.Add(Me.DGV_09)
        Me.TabPage9.Location = New System.Drawing.Point(4, 28)
        Me.TabPage9.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage9.Size = New System.Drawing.Size(1232, 396)
        Me.TabPage9.TabIndex = 8
        Me.TabPage9.Text = "TabPage9"
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'DGV_09
        '
        Me.DGV_09.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_09.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_09.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_09.Location = New System.Drawing.Point(3, 2)
        Me.DGV_09.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_09.Name = "DGV_09"
        Me.DGV_09.RowHeadersWidth = 51
        Me.DGV_09.RowTemplate.Height = 24
        Me.DGV_09.Size = New System.Drawing.Size(1226, 392)
        Me.DGV_09.TabIndex = 0
        '
        'TabPage10
        '
        Me.TabPage10.Controls.Add(Me.DGV_10)
        Me.TabPage10.Location = New System.Drawing.Point(4, 28)
        Me.TabPage10.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage10.Name = "TabPage10"
        Me.TabPage10.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPage10.Size = New System.Drawing.Size(1232, 396)
        Me.TabPage10.TabIndex = 9
        Me.TabPage10.Text = "TabPage10"
        Me.TabPage10.UseVisualStyleBackColor = True
        '
        'DGV_10
        '
        Me.DGV_10.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_10.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_10.Location = New System.Drawing.Point(3, 2)
        Me.DGV_10.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_10.Name = "DGV_10"
        Me.DGV_10.RowHeadersWidth = 51
        Me.DGV_10.RowTemplate.Height = 24
        Me.DGV_10.Size = New System.Drawing.Size(1226, 392)
        Me.DGV_10.TabIndex = 0
        '
        'DGV_総合
        '
        Me.DGV_総合.AllowUserToAddRows = False
        Me.DGV_総合.AllowUserToDeleteRows = False
        Me.DGV_総合.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_総合.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_総合.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_総合.Location = New System.Drawing.Point(36, 46)
        Me.DGV_総合.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_総合.Name = "DGV_総合"
        Me.DGV_総合.RowHeadersWidth = 51
        Me.DGV_総合.RowTemplate.Height = 24
        Me.DGV_総合.Size = New System.Drawing.Size(1032, 298)
        Me.DGV_総合.TabIndex = 2
        '
        'PB_更新
        '
        Me.PB_更新.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_更新.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_更新.Location = New System.Drawing.Point(317, 786)
        Me.PB_更新.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_更新.Name = "PB_更新"
        Me.PB_更新.Size = New System.Drawing.Size(163, 38)
        Me.PB_更新.TabIndex = 3
        Me.PB_更新.Text = "更新"
        Me.PB_更新.UseVisualStyleBackColor = True
        '
        'LB_区分名
        '
        Me.LB_区分名.AutoSize = True
        Me.LB_区分名.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分名.ForeColor = System.Drawing.Color.Blue
        Me.LB_区分名.Location = New System.Drawing.Point(36, 9)
        Me.LB_区分名.Name = "LB_区分名"
        Me.LB_区分名.Size = New System.Drawing.Size(79, 23)
        Me.LB_区分名.TabIndex = 4
        Me.LB_区分名.Text = "区分名"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(899, 11)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(157, 27)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "HTML作成"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'PB_UP数確定
        '
        Me.PB_UP数確定.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_UP数確定.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_UP数確定.Location = New System.Drawing.Point(1107, 46)
        Me.PB_UP数確定.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_UP数確定.Name = "PB_UP数確定"
        Me.PB_UP数確定.Size = New System.Drawing.Size(157, 51)
        Me.PB_UP数確定.TabIndex = 6
        Me.PB_UP数確定.Text = "UP数確定"
        Me.PB_UP数確定.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'TB_ステータス
        '
        Me.TB_ステータス.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TB_ステータス.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TB_ステータス.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_ステータス.Location = New System.Drawing.Point(67, 789)
        Me.TB_ステータス.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_ステータス.Name = "TB_ステータス"
        Me.TB_ステータス.Size = New System.Drawing.Size(229, 34)
        Me.TB_ステータス.TabIndex = 10
        Me.TB_ステータス.Text = "停止中"
        Me.TB_ステータス.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TB_ステータス.Visible = False
        '
        'CB_自動更新
        '
        Me.CB_自動更新.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CB_自動更新.AutoSize = True
        Me.CB_自動更新.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CB_自動更新.Location = New System.Drawing.Point(43, 792)
        Me.CB_自動更新.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CB_自動更新.Name = "CB_自動更新"
        Me.CB_自動更新.Size = New System.Drawing.Size(18, 17)
        Me.CB_自動更新.TabIndex = 11
        Me.CB_自動更新.UseVisualStyleBackColor = True
        Me.CB_自動更新.Visible = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(1090, 261)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 20)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Cali最小"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(1090, 227)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 20)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Cali最大"
        '
        'TB_CaliMin
        '
        Me.TB_CaliMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_CaliMin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_CaliMin.Location = New System.Drawing.Point(1190, 256)
        Me.TB_CaliMin.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_CaliMin.Name = "TB_CaliMin"
        Me.TB_CaliMin.Size = New System.Drawing.Size(92, 27)
        Me.TB_CaliMin.TabIndex = 16
        Me.TB_CaliMin.Text = "5.00"
        Me.TB_CaliMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_CaliMax
        '
        Me.TB_CaliMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_CaliMax.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_CaliMax.Location = New System.Drawing.Point(1190, 224)
        Me.TB_CaliMax.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_CaliMax.Name = "TB_CaliMax"
        Me.TB_CaliMax.Size = New System.Drawing.Size(92, 27)
        Me.TB_CaliMax.TabIndex = 15
        Me.TB_CaliMax.Text = "7.25"
        Me.TB_CaliMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PB_Cali設定
        '
        Me.PB_Cali設定.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_Cali設定.Location = New System.Drawing.Point(1108, 292)
        Me.PB_Cali設定.Name = "PB_Cali設定"
        Me.PB_Cali設定.Size = New System.Drawing.Size(164, 52)
        Me.PB_Cali設定.TabIndex = 19
        Me.PB_Cali設定.Text = "キャリブレーション" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "設定"
        Me.PB_Cali設定.UseVisualStyleBackColor = True
        '
        'PB_欠場
        '
        Me.PB_欠場.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_欠場.Font = New System.Drawing.Font("MS UI Gothic", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_欠場.Location = New System.Drawing.Point(1131, 124)
        Me.PB_欠場.Name = "PB_欠場"
        Me.PB_欠場.Size = New System.Drawing.Size(132, 50)
        Me.PB_欠場.TabIndex = 20
        Me.PB_欠場.Text = "欠場・不戦勝" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "処理"
        Me.PB_欠場.UseVisualStyleBackColor = True
        '
        'CB_リアル更新
        '
        Me.CB_リアル更新.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CB_リアル更新.AutoSize = True
        Me.CB_リアル更新.Checked = True
        Me.CB_リアル更新.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CB_リアル更新.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CB_リアル更新.Location = New System.Drawing.Point(549, 800)
        Me.CB_リアル更新.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CB_リアル更新.Name = "CB_リアル更新"
        Me.CB_リアル更新.Size = New System.Drawing.Size(18, 17)
        Me.CB_リアル更新.TabIndex = 21
        Me.CB_リアル更新.UseVisualStyleBackColor = True
        '
        'TB_リアルステータス
        '
        Me.TB_リアルステータス.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TB_リアルステータス.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TB_リアルステータス.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_リアルステータス.Location = New System.Drawing.Point(584, 792)
        Me.TB_リアルステータス.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_リアルステータス.Name = "TB_リアルステータス"
        Me.TB_リアルステータス.Size = New System.Drawing.Size(229, 34)
        Me.TB_リアルステータス.TabIndex = 22
        Me.TB_リアルステータス.Text = "リアル更新"
        Me.TB_リアルステータス.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'F501_得点詳細_C
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1316, 844)
        Me.Controls.Add(Me.TB_リアルステータス)
        Me.Controls.Add(Me.CB_リアル更新)
        Me.Controls.Add(Me.PB_欠場)
        Me.Controls.Add(Me.PB_Cali設定)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TB_CaliMin)
        Me.Controls.Add(Me.TB_CaliMax)
        Me.Controls.Add(Me.CB_自動更新)
        Me.Controls.Add(Me.TB_ステータス)
        Me.Controls.Add(Me.PB_UP数確定)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.LB_区分名)
        Me.Controls.Add(Me.PB_更新)
        Me.Controls.Add(Me.DGV_総合)
        Me.Controls.Add(Me.TabControl_詳細)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "F501_得点詳細_C"
        Me.Text = "F501_得点詳細_C"
        Me.TabControl_詳細.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DGV_01, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DGV_02, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.DGV_03, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.DGV_04, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.DGV_05, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        CType(Me.DGV_06, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage7.ResumeLayout(False)
        CType(Me.DGV_07, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage8.ResumeLayout(False)
        CType(Me.DGV_08, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage9.ResumeLayout(False)
        CType(Me.DGV_09, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage10.ResumeLayout(False)
        CType(Me.DGV_10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_総合, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl_詳細 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents DGV_01 As DataGridView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents DGV_02 As DataGridView
    Friend WithEvents DGV_03 As DataGridView
    Friend WithEvents DGV_04 As DataGridView
    Friend WithEvents DGV_05 As DataGridView
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents DGV_06 As DataGridView
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents DGV_07 As DataGridView
    Friend WithEvents TabPage8 As TabPage
    Friend WithEvents DGV_08 As DataGridView
    Friend WithEvents TabPage9 As TabPage
    Friend WithEvents DGV_09 As DataGridView
    Friend WithEvents TabPage10 As TabPage
    Friend WithEvents DGV_10 As DataGridView
    Friend WithEvents DGV_総合 As DataGridView
    Friend WithEvents PB_更新 As Button
    Friend WithEvents LB_区分名 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents PB_UP数確定 As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents TB_ステータス As TextBox
    Friend WithEvents CB_自動更新 As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TB_CaliMin As TextBox
    Friend WithEvents TB_CaliMax As TextBox
    Friend WithEvents PB_Cali設定 As Button
    Friend WithEvents PB_欠場 As Button
    Friend WithEvents CB_リアル更新 As CheckBox
    Friend WithEvents TB_リアルステータス As TextBox
    Friend WithEvents Timer2 As Timer
End Class
