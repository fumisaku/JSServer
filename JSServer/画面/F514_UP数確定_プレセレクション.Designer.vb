<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F514_UP数確定_プレセレクション
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
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.LB_UP数 = New System.Windows.Forms.Label()
        Me.LB_UP予定数 = New System.Windows.Forms.Label()
        Me.LB_出場組数 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DGV_UP数 = New System.Windows.Forms.DataGridView()
        Me.LB_区分名 = New System.Windows.Forms.Label()
        Me.PB_確定 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        CType(Me.DGV_UP数, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(89, 666)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 30
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'LB_UP数
        '
        Me.LB_UP数.AutoSize = True
        Me.LB_UP数.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_UP数.ForeColor = System.Drawing.Color.Blue
        Me.LB_UP数.Location = New System.Drawing.Point(1023, 229)
        Me.LB_UP数.Name = "LB_UP数"
        Me.LB_UP数.Size = New System.Drawing.Size(34, 24)
        Me.LB_UP数.TabIndex = 29
        Me.LB_UP数.Text = "組"
        '
        'LB_UP予定数
        '
        Me.LB_UP予定数.AutoSize = True
        Me.LB_UP予定数.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_UP予定数.ForeColor = System.Drawing.Color.Blue
        Me.LB_UP予定数.Location = New System.Drawing.Point(1023, 155)
        Me.LB_UP予定数.Name = "LB_UP予定数"
        Me.LB_UP予定数.Size = New System.Drawing.Size(34, 24)
        Me.LB_UP予定数.TabIndex = 28
        Me.LB_UP予定数.Text = "組"
        '
        'LB_出場組数
        '
        Me.LB_出場組数.AutoSize = True
        Me.LB_出場組数.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_出場組数.ForeColor = System.Drawing.Color.Blue
        Me.LB_出場組数.Location = New System.Drawing.Point(1023, 110)
        Me.LB_出場組数.Name = "LB_出場組数"
        Me.LB_出場組数.Size = New System.Drawing.Size(34, 24)
        Me.LB_出場組数.TabIndex = 27
        Me.LB_出場組数.Text = "組"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(900, 229)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 24)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "UP数"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(900, 155)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 24)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "UP予定数"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(900, 110)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 24)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "出場組数"
        '
        'DGV_UP数
        '
        Me.DGV_UP数.AllowUserToAddRows = False
        Me.DGV_UP数.AllowUserToDeleteRows = False
        Me.DGV_UP数.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_UP数.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_UP数.Location = New System.Drawing.Point(89, 88)
        Me.DGV_UP数.Name = "DGV_UP数"
        Me.DGV_UP数.RowHeadersVisible = False
        Me.DGV_UP数.RowHeadersWidth = 51
        Me.DGV_UP数.RowTemplate.Height = 24
        Me.DGV_UP数.Size = New System.Drawing.Size(770, 541)
        Me.DGV_UP数.TabIndex = 23
        '
        'LB_区分名
        '
        Me.LB_区分名.AutoSize = True
        Me.LB_区分名.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分名.ForeColor = System.Drawing.Color.Blue
        Me.LB_区分名.Location = New System.Drawing.Point(64, 28)
        Me.LB_区分名.Name = "LB_区分名"
        Me.LB_区分名.Size = New System.Drawing.Size(82, 24)
        Me.LB_区分名.TabIndex = 22
        Me.LB_区分名.Text = "区分名"
        '
        'PB_確定
        '
        Me.PB_確定.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_確定.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_確定.Location = New System.Drawing.Point(904, 387)
        Me.PB_確定.Name = "PB_確定"
        Me.PB_確定.Size = New System.Drawing.Size(215, 95)
        Me.PB_確定.TabIndex = 31
        Me.PB_確定.Text = "確定"
        Me.PB_確定.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Info
        Me.TextBox1.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(887, 526)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(276, 166)
        Me.TextBox1.TabIndex = 32
        Me.TextBox1.Text = "予選通過者の「UP」欄に「1」を入力して確定ボタンを押してください。"
        '
        'F514_UP数確定_プレセレクション
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1194, 772)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.PB_確定)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.LB_UP数)
        Me.Controls.Add(Me.LB_UP予定数)
        Me.Controls.Add(Me.LB_出場組数)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DGV_UP数)
        Me.Controls.Add(Me.LB_区分名)
        Me.Name = "F514_UP数確定_プレセレクション"
        Me.Text = "F514_UP数確定_プレセレクション"
        CType(Me.DGV_UP数, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PB_戻る As Button
    Friend WithEvents LB_UP数 As Label
    Friend WithEvents LB_UP予定数 As Label
    Friend WithEvents LB_出場組数 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents DGV_UP数 As DataGridView
    Friend WithEvents LB_区分名 As Label
    Friend WithEvents PB_確定 As Button
    Friend WithEvents TextBox1 As TextBox
End Class
