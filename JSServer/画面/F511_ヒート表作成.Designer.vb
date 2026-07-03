<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F511_ヒート表作成
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
        Me.DGV_ヒート表 = New System.Windows.Forms.DataGridView()
        Me.DGV_ヒート数 = New System.Windows.Forms.DataGridView()
        Me.LB_UP数 = New System.Windows.Forms.Label()
        Me.LB_出場組数 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LB_区分名 = New System.Windows.Forms.Label()
        Me.CB_ヒート割方式 = New System.Windows.Forms.ComboBox()
        Me.PB_再ヒート割り = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LB_採点方式 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.PB_確定 = New System.Windows.Forms.Button()
        Me.LB_BR注意 = New System.Windows.Forms.Label()
        CType(Me.DGV_ヒート表, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_ヒート数, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_ヒート表
        '
        Me.DGV_ヒート表.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_ヒート表.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_ヒート表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_ヒート表.Location = New System.Drawing.Point(59, 192)
        Me.DGV_ヒート表.Name = "DGV_ヒート表"
        Me.DGV_ヒート表.RowHeadersWidth = 51
        Me.DGV_ヒート表.RowTemplate.Height = 24
        Me.DGV_ヒート表.Size = New System.Drawing.Size(907, 510)
        Me.DGV_ヒート表.TabIndex = 0
        '
        'DGV_ヒート数
        '
        Me.DGV_ヒート数.AllowUserToAddRows = False
        Me.DGV_ヒート数.AllowUserToDeleteRows = False
        Me.DGV_ヒート数.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_ヒート数.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_ヒート数.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_ヒート数.Location = New System.Drawing.Point(444, 92)
        Me.DGV_ヒート数.Name = "DGV_ヒート数"
        Me.DGV_ヒート数.RowHeadersVisible = False
        Me.DGV_ヒート数.RowHeadersWidth = 51
        Me.DGV_ヒート数.RowTemplate.Height = 24
        Me.DGV_ヒート数.Size = New System.Drawing.Size(522, 94)
        Me.DGV_ヒート数.TabIndex = 1
        '
        'LB_UP数
        '
        Me.LB_UP数.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_UP数.AutoSize = True
        Me.LB_UP数.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_UP数.ForeColor = System.Drawing.Color.Blue
        Me.LB_UP数.Location = New System.Drawing.Point(1118, 139)
        Me.LB_UP数.Name = "LB_UP数"
        Me.LB_UP数.Size = New System.Drawing.Size(34, 24)
        Me.LB_UP数.TabIndex = 16
        Me.LB_UP数.Text = "組"
        '
        'LB_出場組数
        '
        Me.LB_出場組数.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_出場組数.AutoSize = True
        Me.LB_出場組数.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_出場組数.ForeColor = System.Drawing.Color.Blue
        Me.LB_出場組数.Location = New System.Drawing.Point(1118, 94)
        Me.LB_出場組数.Name = "LB_出場組数"
        Me.LB_出場組数.Size = New System.Drawing.Size(34, 24)
        Me.LB_出場組数.TabIndex = 15
        Me.LB_出場組数.Text = "組"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(995, 139)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 24)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "UP数"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(995, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 24)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "出場組数"
        '
        'LB_区分名
        '
        Me.LB_区分名.AutoSize = True
        Me.LB_区分名.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分名.ForeColor = System.Drawing.Color.Blue
        Me.LB_区分名.Location = New System.Drawing.Point(55, 25)
        Me.LB_区分名.Name = "LB_区分名"
        Me.LB_区分名.Size = New System.Drawing.Size(82, 24)
        Me.LB_区分名.TabIndex = 12
        Me.LB_区分名.Text = "区分名"
        '
        'CB_ヒート割方式
        '
        Me.CB_ヒート割方式.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CB_ヒート割方式.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CB_ヒート割方式.FormattingEnabled = True
        Me.CB_ヒート割方式.Location = New System.Drawing.Point(1003, 238)
        Me.CB_ヒート割方式.Name = "CB_ヒート割方式"
        Me.CB_ヒート割方式.Size = New System.Drawing.Size(149, 31)
        Me.CB_ヒート割方式.TabIndex = 17
        '
        'PB_再ヒート割り
        '
        Me.PB_再ヒート割り.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_再ヒート割り.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_再ヒート割り.Location = New System.Drawing.Point(1003, 295)
        Me.PB_再ヒート割り.Name = "PB_再ヒート割り"
        Me.PB_再ヒート割り.Size = New System.Drawing.Size(155, 44)
        Me.PB_再ヒート割り.TabIndex = 18
        Me.PB_再ヒート割り.Text = "再ヒート割り"
        Me.PB_再ヒート割り.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(995, 194)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(157, 24)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "ヒート割り方式"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(865, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 24)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "採点方式"
        '
        'LB_採点方式
        '
        Me.LB_採点方式.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_採点方式.AutoSize = True
        Me.LB_採点方式.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_採点方式.ForeColor = System.Drawing.Color.Blue
        Me.LB_採点方式.Location = New System.Drawing.Point(999, 50)
        Me.LB_採点方式.Name = "LB_採点方式"
        Me.LB_採点方式.Size = New System.Drawing.Size(65, 24)
        Me.LB_採点方式.TabIndex = 20
        Me.LB_採点方式.Text = "xxxxx"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(336, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 24)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "ヒート数"
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(59, 741)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 22
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'PB_確定
        '
        Me.PB_確定.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_確定.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_確定.ForeColor = System.Drawing.Color.Blue
        Me.PB_確定.Location = New System.Drawing.Point(876, 719)
        Me.PB_確定.Name = "PB_確定"
        Me.PB_確定.Size = New System.Drawing.Size(194, 92)
        Me.PB_確定.TabIndex = 23
        Me.PB_確定.Text = "確定"
        Me.PB_確定.UseVisualStyleBackColor = True
        '
        'LB_BR注意
        '
        Me.LB_BR注意.AutoSize = True
        Me.LB_BR注意.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_BR注意.Location = New System.Drawing.Point(531, 451)
        Me.LB_BR注意.Name = "LB_BR注意"
        Me.LB_BR注意.Size = New System.Drawing.Size(355, 72)
        Me.LB_BR注意.TabIndex = 24
        Me.LB_BR注意.Text = "注意：ヒート番号「1」が赤、「2」が" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "青になります。どちらも「1」の場合は、" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "背番号が若い方が赤になります。"
        '
        'F511_ヒート表作成
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1210, 842)
        Me.Controls.Add(Me.LB_BR注意)
        Me.Controls.Add(Me.PB_確定)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.LB_採点方式)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.PB_再ヒート割り)
        Me.Controls.Add(Me.CB_ヒート割方式)
        Me.Controls.Add(Me.LB_UP数)
        Me.Controls.Add(Me.LB_出場組数)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LB_区分名)
        Me.Controls.Add(Me.DGV_ヒート数)
        Me.Controls.Add(Me.DGV_ヒート表)
        Me.Name = "F511_ヒート表作成"
        Me.Text = "F511_ヒート表作成"
        CType(Me.DGV_ヒート表, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_ヒート数, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGV_ヒート表 As DataGridView
    Friend WithEvents DGV_ヒート数 As DataGridView
    Friend WithEvents LB_UP数 As Label
    Friend WithEvents LB_出場組数 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents LB_区分名 As Label
    Friend WithEvents CB_ヒート割方式 As ComboBox
    Friend WithEvents PB_再ヒート割り As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents LB_採点方式 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents PB_戻る As Button
    Friend WithEvents PB_確定 As Button
    Friend WithEvents LB_BR注意 As Label
End Class
