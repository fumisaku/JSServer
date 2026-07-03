<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F111_区分設定
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
        Me.TB_区分記号 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LB_区分番号 = New System.Windows.Forms.Label()
        Me.TB_区分名 = New System.Windows.Forms.TextBox()
        Me.TB_区分表記名 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CB_カテゴリ = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TB_審判G = New System.Windows.Forms.TextBox()
        Me.TB_選手マスタ = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DGV_ラウンド = New System.Windows.Forms.DataGridView()
        Me.PB_種目設定 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.PB_保存 = New System.Windows.Forms.Button()
        Me.LB_エントリー数 = New System.Windows.Forms.Label()
        CType(Me.DGV_ラウンド, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TB_区分記号
        '
        Me.TB_区分記号.Font = New System.Drawing.Font("MS UI Gothic", 14.0!)
        Me.TB_区分記号.Location = New System.Drawing.Point(141, 38)
        Me.TB_区分記号.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_区分記号.Name = "TB_区分記号"
        Me.TB_区分記号.Size = New System.Drawing.Size(81, 31)
        Me.TB_区分記号.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "区分記号"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(32, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "区分番号"
        '
        'LB_区分番号
        '
        Me.LB_区分番号.AutoSize = True
        Me.LB_区分番号.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分番号.Location = New System.Drawing.Point(137, 9)
        Me.LB_区分番号.Name = "LB_区分番号"
        Me.LB_区分番号.Size = New System.Drawing.Size(31, 20)
        Me.LB_区分番号.TabIndex = 3
        Me.LB_区分番号.Text = "01"
        '
        'TB_区分名
        '
        Me.TB_区分名.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_区分名.Location = New System.Drawing.Point(380, 2)
        Me.TB_区分名.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_区分名.Name = "TB_区分名"
        Me.TB_区分名.Size = New System.Drawing.Size(455, 30)
        Me.TB_区分名.TabIndex = 4
        '
        'TB_区分表記名
        '
        Me.TB_区分表記名.Font = New System.Drawing.Font("MS UI Gothic", 14.0!)
        Me.TB_区分表記名.Location = New System.Drawing.Point(380, 38)
        Me.TB_区分表記名.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_区分表記名.Name = "TB_区分表記名"
        Me.TB_区分表記名.Size = New System.Drawing.Size(455, 31)
        Me.TB_区分表記名.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(253, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "区分名"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(253, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(114, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "区分表記名"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(32, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 20)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "カテゴリ"
        '
        'CB_カテゴリ
        '
        Me.CB_カテゴリ.Font = New System.Drawing.Font("MS UI Gothic", 14.0!)
        Me.CB_カテゴリ.FormattingEnabled = True
        Me.CB_カテゴリ.Location = New System.Drawing.Point(141, 84)
        Me.CB_カテゴリ.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CB_カテゴリ.Name = "CB_カテゴリ"
        Me.CB_カテゴリ.Size = New System.Drawing.Size(175, 31)
        Me.CB_カテゴリ.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(371, 88)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(159, 20)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "担当審判グループ"
        '
        'TB_審判G
        '
        Me.TB_審判G.Font = New System.Drawing.Font("MS UI Gothic", 14.0!)
        Me.TB_審判G.Location = New System.Drawing.Point(548, 84)
        Me.TB_審判G.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_審判G.Name = "TB_審判G"
        Me.TB_審判G.Size = New System.Drawing.Size(81, 31)
        Me.TB_審判G.TabIndex = 11
        '
        'TB_選手マスタ
        '
        Me.TB_選手マスタ.Font = New System.Drawing.Font("MS UI Gothic", 14.0!)
        Me.TB_選手マスタ.Location = New System.Drawing.Point(749, 84)
        Me.TB_選手マスタ.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_選手マスタ.Name = "TB_選手マスタ"
        Me.TB_選手マスタ.Size = New System.Drawing.Size(81, 31)
        Me.TB_選手マスタ.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(645, 88)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 20)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "選手マスタ"
        '
        'DGV_ラウンド
        '
        Me.DGV_ラウンド.AllowUserToAddRows = False
        Me.DGV_ラウンド.AllowUserToDeleteRows = False
        Me.DGV_ラウンド.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_ラウンド.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_ラウンド.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_ラウンド.Location = New System.Drawing.Point(36, 158)
        Me.DGV_ラウンド.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DGV_ラウンド.Name = "DGV_ラウンド"
        Me.DGV_ラウンド.RowHeadersVisible = False
        Me.DGV_ラウンド.RowHeadersWidth = 51
        Me.DGV_ラウンド.RowTemplate.Height = 24
        Me.DGV_ラウンド.Size = New System.Drawing.Size(899, 403)
        Me.DGV_ラウンド.TabIndex = 14
        '
        'PB_種目設定
        '
        Me.PB_種目設定.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_種目設定.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_種目設定.Location = New System.Drawing.Point(357, 571)
        Me.PB_種目設定.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_種目設定.Name = "PB_種目設定"
        Me.PB_種目設定.Size = New System.Drawing.Size(149, 52)
        Me.PB_種目設定.TabIndex = 15
        Me.PB_種目設定.Text = "種目設定"
        Me.PB_種目設定.UseVisualStyleBackColor = True
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(680, 571)
        Me.PB_戻る.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(149, 52)
        Me.PB_戻る.TabIndex = 16
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'PB_保存
        '
        Me.PB_保存.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_保存.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_保存.Location = New System.Drawing.Point(36, 571)
        Me.PB_保存.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_保存.Name = "PB_保存"
        Me.PB_保存.Size = New System.Drawing.Size(149, 52)
        Me.PB_保存.TabIndex = 17
        Me.PB_保存.Text = "保存"
        Me.PB_保存.UseVisualStyleBackColor = True
        '
        'LB_エントリー数
        '
        Me.LB_エントリー数.AutoSize = True
        Me.LB_エントリー数.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_エントリー数.Location = New System.Drawing.Point(32, 122)
        Me.LB_エントリー数.Name = "LB_エントリー数"
        Me.LB_エントリー数.Size = New System.Drawing.Size(107, 20)
        Me.LB_エントリー数.TabIndex = 18
        Me.LB_エントリー数.Text = "エントリー数"
        '
        'F111_区分設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(973, 634)
        Me.Controls.Add(Me.LB_エントリー数)
        Me.Controls.Add(Me.PB_保存)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.PB_種目設定)
        Me.Controls.Add(Me.DGV_ラウンド)
        Me.Controls.Add(Me.TB_選手マスタ)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TB_審判G)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CB_カテゴリ)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TB_区分表記名)
        Me.Controls.Add(Me.TB_区分名)
        Me.Controls.Add(Me.LB_区分番号)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TB_区分記号)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "F111_区分設定"
        Me.Text = "F111_区分設定"
        CType(Me.DGV_ラウンド, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TB_区分記号 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LB_区分番号 As Label
    Friend WithEvents TB_区分名 As TextBox
    Friend WithEvents TB_区分表記名 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents CB_カテゴリ As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TB_審判G As TextBox
    Friend WithEvents TB_選手マスタ As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents DGV_ラウンド As DataGridView
    Friend WithEvents PB_種目設定 As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents PB_保存 As Button
    Friend WithEvents LB_エントリー数 As Label
End Class
