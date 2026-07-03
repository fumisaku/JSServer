<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F520_同点決勝設定
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
        Me.PB_ラウンド設定 = New System.Windows.Forms.Button()
        Me.LB_区分 = New System.Windows.Forms.Label()
        Me.LB_ラウンド名 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LB_出場組数 = New System.Windows.Forms.Label()
        Me.LB_採点方式 = New System.Windows.Forms.Label()
        Me.CB_採点方式 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_審判G = New System.Windows.Forms.TextBox()
        Me.TB_Heat数 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TB_UP数 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CB_ヒート割方式 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CB_リアルFLAG = New System.Windows.Forms.ComboBox()
        Me.LB_リアルFLAG = New System.Windows.Forms.Label()
        Me.TB_Cali最大値 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TB_Cali最小値 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PB_ヒート表作成 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'PB_ラウンド設定
        '
        Me.PB_ラウンド設定.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_ラウンド設定.Location = New System.Drawing.Point(107, 545)
        Me.PB_ラウンド設定.Name = "PB_ラウンド設定"
        Me.PB_ラウンド設定.Size = New System.Drawing.Size(176, 56)
        Me.PB_ラウンド設定.TabIndex = 0
        Me.PB_ラウンド設定.Text = "ラウンド設定"
        Me.PB_ラウンド設定.UseVisualStyleBackColor = True
        '
        'LB_区分
        '
        Me.LB_区分.AutoSize = True
        Me.LB_区分.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分.Location = New System.Drawing.Point(45, 60)
        Me.LB_区分.Name = "LB_区分"
        Me.LB_区分.Size = New System.Drawing.Size(93, 24)
        Me.LB_区分.TabIndex = 1
        Me.LB_区分.Text = "LB_区分"
        '
        'LB_ラウンド名
        '
        Me.LB_ラウンド名.AutoSize = True
        Me.LB_ラウンド名.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_ラウンド名.Location = New System.Drawing.Point(45, 103)
        Me.LB_ラウンド名.Name = "LB_ラウンド名"
        Me.LB_ラウンド名.Size = New System.Drawing.Size(139, 24)
        Me.LB_ラウンド名.TabIndex = 2
        Me.LB_ラウンド名.Text = "LB_ラウンド名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(202, 24)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "同点決勝設定画面"
        '
        'LB_出場組数
        '
        Me.LB_出場組数.AutoSize = True
        Me.LB_出場組数.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_出場組数.Location = New System.Drawing.Point(45, 147)
        Me.LB_出場組数.Name = "LB_出場組数"
        Me.LB_出場組数.Size = New System.Drawing.Size(141, 24)
        Me.LB_出場組数.TabIndex = 4
        Me.LB_出場組数.Text = "LB_出場組数"
        '
        'LB_採点方式
        '
        Me.LB_採点方式.AutoSize = True
        Me.LB_採点方式.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_採点方式.Location = New System.Drawing.Point(83, 210)
        Me.LB_採点方式.Name = "LB_採点方式"
        Me.LB_採点方式.Size = New System.Drawing.Size(106, 24)
        Me.LB_採点方式.TabIndex = 5
        Me.LB_採点方式.Text = "採点方式"
        '
        'CB_採点方式
        '
        Me.CB_採点方式.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CB_採点方式.FormattingEnabled = True
        Me.CB_採点方式.Location = New System.Drawing.Point(217, 207)
        Me.CB_採点方式.Name = "CB_採点方式"
        Me.CB_採点方式.Size = New System.Drawing.Size(132, 31)
        Me.CB_採点方式.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(83, 248)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 24)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "審判G"
        '
        'TB_審判G
        '
        Me.TB_審判G.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_審判G.Location = New System.Drawing.Point(217, 242)
        Me.TB_審判G.Name = "TB_審判G"
        Me.TB_審判G.Size = New System.Drawing.Size(88, 30)
        Me.TB_審判G.TabIndex = 8
        Me.TB_審判G.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_Heat数
        '
        Me.TB_Heat数.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_Heat数.Location = New System.Drawing.Point(217, 276)
        Me.TB_Heat数.Name = "TB_Heat数"
        Me.TB_Heat数.Size = New System.Drawing.Size(88, 30)
        Me.TB_Heat数.TabIndex = 10
        Me.TB_Heat数.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(83, 282)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 24)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Heat数"
        '
        'TB_UP数
        '
        Me.TB_UP数.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_UP数.Location = New System.Drawing.Point(217, 312)
        Me.TB_UP数.Name = "TB_UP数"
        Me.TB_UP数.Size = New System.Drawing.Size(88, 30)
        Me.TB_UP数.TabIndex = 12
        Me.TB_UP数.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(83, 318)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 24)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "UP数"
        '
        'CB_ヒート割方式
        '
        Me.CB_ヒート割方式.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CB_ヒート割方式.FormattingEnabled = True
        Me.CB_ヒート割方式.Location = New System.Drawing.Point(217, 349)
        Me.CB_ヒート割方式.Name = "CB_ヒート割方式"
        Me.CB_ヒート割方式.Size = New System.Drawing.Size(132, 31)
        Me.CB_ヒート割方式.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(83, 352)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(135, 24)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "ヒート割方式"
        '
        'CB_リアルFLAG
        '
        Me.CB_リアルFLAG.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CB_リアルFLAG.FormattingEnabled = True
        Me.CB_リアルFLAG.Location = New System.Drawing.Point(217, 387)
        Me.CB_リアルFLAG.Name = "CB_リアルFLAG"
        Me.CB_リアルFLAG.Size = New System.Drawing.Size(88, 31)
        Me.CB_リアルFLAG.TabIndex = 16
        '
        'LB_リアルFLAG
        '
        Me.LB_リアルFLAG.AutoSize = True
        Me.LB_リアルFLAG.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_リアルFLAG.Location = New System.Drawing.Point(83, 390)
        Me.LB_リアルFLAG.Name = "LB_リアルFLAG"
        Me.LB_リアルFLAG.Size = New System.Drawing.Size(119, 24)
        Me.LB_リアルFLAG.TabIndex = 15
        Me.LB_リアルFLAG.Text = "リアルFLAG"
        '
        'TB_Cali最大値
        '
        Me.TB_Cali最大値.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_Cali最大値.Location = New System.Drawing.Point(217, 424)
        Me.TB_Cali最大値.Name = "TB_Cali最大値"
        Me.TB_Cali最大値.Size = New System.Drawing.Size(88, 30)
        Me.TB_Cali最大値.TabIndex = 18
        Me.TB_Cali最大値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(83, 430)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(119, 24)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Cali最大値"
        '
        'TB_Cali最小値
        '
        Me.TB_Cali最小値.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_Cali最小値.Location = New System.Drawing.Point(217, 461)
        Me.TB_Cali最小値.Name = "TB_Cali最小値"
        Me.TB_Cali最小値.Size = New System.Drawing.Size(88, 30)
        Me.TB_Cali最小値.TabIndex = 20
        Me.TB_Cali最小値.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(83, 467)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(119, 24)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Cali最小値"
        '
        'PB_ヒート表作成
        '
        Me.PB_ヒート表作成.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_ヒート表作成.Location = New System.Drawing.Point(506, 545)
        Me.PB_ヒート表作成.Name = "PB_ヒート表作成"
        Me.PB_ヒート表作成.Size = New System.Drawing.Size(176, 56)
        Me.PB_ヒート表作成.TabIndex = 21
        Me.PB_ヒート表作成.Text = "ヒート表作成"
        Me.PB_ヒート表作成.UseVisualStyleBackColor = True
        '
        'F520_同点決勝設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1027, 738)
        Me.Controls.Add(Me.PB_ヒート表作成)
        Me.Controls.Add(Me.TB_Cali最小値)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TB_Cali最大値)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CB_リアルFLAG)
        Me.Controls.Add(Me.LB_リアルFLAG)
        Me.Controls.Add(Me.CB_ヒート割方式)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TB_UP数)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TB_Heat数)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TB_審判G)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CB_採点方式)
        Me.Controls.Add(Me.LB_採点方式)
        Me.Controls.Add(Me.LB_出場組数)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LB_ラウンド名)
        Me.Controls.Add(Me.LB_区分)
        Me.Controls.Add(Me.PB_ラウンド設定)
        Me.Name = "F520_同点決勝設定"
        Me.Text = "F520_同点決勝設定"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PB_ラウンド設定 As Button
    Friend WithEvents LB_区分 As Label
    Friend WithEvents LB_ラウンド名 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LB_出場組数 As Label
    Friend WithEvents LB_採点方式 As Label
    Friend WithEvents CB_採点方式 As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TB_審判G As TextBox
    Friend WithEvents TB_Heat数 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TB_UP数 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents CB_ヒート割方式 As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents CB_リアルFLAG As ComboBox
    Friend WithEvents LB_リアルFLAG As Label
    Friend WithEvents TB_Cali最大値 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TB_Cali最小値 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents PB_ヒート表作成 As Button
End Class
