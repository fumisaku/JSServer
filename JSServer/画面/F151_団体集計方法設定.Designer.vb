<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F151_団体集計方法設定
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
        Me.TB_集計方法名 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TB_倍率 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TB_上位ポジション数 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.RB_同点按分_Yes = New System.Windows.Forms.RadioButton()
        Me.RB_同点按分_No = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.RB_重複加算_No = New System.Windows.Forms.RadioButton()
        Me.RB_重複加算_Yes = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DGV = New System.Windows.Forms.DataGridView()
        Me.PB_保存 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TB_集計方法名
        '
        Me.TB_集計方法名.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_集計方法名.Location = New System.Drawing.Point(178, 30)
        Me.TB_集計方法名.Name = "TB_集計方法名"
        Me.TB_集計方法名.Size = New System.Drawing.Size(349, 30)
        Me.TB_集計方法名.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "集計方法名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(25, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 24)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "倍率"
        '
        'TB_倍率
        '
        Me.TB_倍率.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_倍率.Location = New System.Drawing.Point(121, 88)
        Me.TB_倍率.Name = "TB_倍率"
        Me.TB_倍率.Size = New System.Drawing.Size(138, 30)
        Me.TB_倍率.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(418, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 24)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "上位"
        '
        'TB_上位ポジション数
        '
        Me.TB_上位ポジション数.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_上位ポジション数.Location = New System.Drawing.Point(482, 88)
        Me.TB_上位ポジション数.Name = "TB_上位ポジション数"
        Me.TB_上位ポジション数.Size = New System.Drawing.Size(138, 30)
        Me.TB_上位ポジション数.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(626, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 24)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "ポジション"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(25, 148)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(161, 24)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "同点 点数按分"
        '
        'RB_同点按分_Yes
        '
        Me.RB_同点按分_Yes.AutoSize = True
        Me.RB_同点按分_Yes.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RB_同点按分_Yes.Location = New System.Drawing.Point(25, 3)
        Me.RB_同点按分_Yes.Name = "RB_同点按分_Yes"
        Me.RB_同点按分_Yes.Size = New System.Drawing.Size(68, 28)
        Me.RB_同点按分_Yes.TabIndex = 7
        Me.RB_同点按分_Yes.TabStop = True
        Me.RB_同点按分_Yes.Text = "Yes"
        Me.RB_同点按分_Yes.UseVisualStyleBackColor = True
        '
        'RB_同点按分_No
        '
        Me.RB_同点按分_No.AutoSize = True
        Me.RB_同点按分_No.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RB_同点按分_No.Location = New System.Drawing.Point(113, 3)
        Me.RB_同点按分_No.Name = "RB_同点按分_No"
        Me.RB_同点按分_No.Size = New System.Drawing.Size(58, 28)
        Me.RB_同点按分_No.TabIndex = 8
        Me.RB_同点按分_No.TabStop = True
        Me.RB_同点按分_No.Text = "No"
        Me.RB_同点按分_No.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(418, 148)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(154, 24)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "点数重複加算"
        '
        'RB_重複加算_No
        '
        Me.RB_重複加算_No.AutoSize = True
        Me.RB_重複加算_No.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RB_重複加算_No.Location = New System.Drawing.Point(110, 3)
        Me.RB_重複加算_No.Name = "RB_重複加算_No"
        Me.RB_重複加算_No.Size = New System.Drawing.Size(58, 28)
        Me.RB_重複加算_No.TabIndex = 11
        Me.RB_重複加算_No.TabStop = True
        Me.RB_重複加算_No.Text = "No"
        Me.RB_重複加算_No.UseVisualStyleBackColor = True
        '
        'RB_重複加算_Yes
        '
        Me.RB_重複加算_Yes.AutoSize = True
        Me.RB_重複加算_Yes.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RB_重複加算_Yes.Location = New System.Drawing.Point(22, 3)
        Me.RB_重複加算_Yes.Name = "RB_重複加算_Yes"
        Me.RB_重複加算_Yes.Size = New System.Drawing.Size(68, 28)
        Me.RB_重複加算_Yes.TabIndex = 10
        Me.RB_重複加算_Yes.TabStop = True
        Me.RB_重複加算_Yes.Text = "Yes"
        Me.RB_重複加算_Yes.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RB_同点按分_Yes)
        Me.Panel1.Controls.Add(Me.RB_同点按分_No)
        Me.Panel1.Location = New System.Drawing.Point(192, 144)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(211, 45)
        Me.Panel1.TabIndex = 12
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RB_重複加算_No)
        Me.Panel2.Controls.Add(Me.RB_重複加算_Yes)
        Me.Panel2.Location = New System.Drawing.Point(593, 144)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 45)
        Me.Panel2.TabIndex = 13
        '
        'DGV
        '
        Me.DGV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV.Location = New System.Drawing.Point(60, 229)
        Me.DGV.Name = "DGV"
        Me.DGV.RowHeadersWidth = 51
        Me.DGV.RowTemplate.Height = 24
        Me.DGV.Size = New System.Drawing.Size(999, 341)
        Me.DGV.TabIndex = 14
        '
        'PB_保存
        '
        Me.PB_保存.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_保存.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_保存.Location = New System.Drawing.Point(911, 595)
        Me.PB_保存.Name = "PB_保存"
        Me.PB_保存.Size = New System.Drawing.Size(148, 49)
        Me.PB_保存.TabIndex = 16
        Me.PB_保存.Text = "保存"
        Me.PB_保存.UseVisualStyleBackColor = True
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(60, 595)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 15
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'F151_団体集計方法設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1120, 672)
        Me.Controls.Add(Me.PB_保存)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TB_上位ポジション数)
        Me.Controls.Add(Me.TB_倍率)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TB_集計方法名)
        Me.Name = "F151_団体集計方法設定"
        Me.Text = "F151_団体集計方法設定"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TB_集計方法名 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TB_倍率 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TB_上位ポジション数 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents RB_同点按分_Yes As RadioButton
    Friend WithEvents RB_同点按分_No As RadioButton
    Friend WithEvents Label6 As Label
    Friend WithEvents RB_重複加算_No As RadioButton
    Friend WithEvents RB_重複加算_Yes As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents DGV As DataGridView
    Friend WithEvents PB_保存 As Button
    Friend WithEvents PB_戻る As Button
End Class
