<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F513_結果入力
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
        Me.DGV_結果 = New System.Windows.Forms.DataGridView()
        Me.PB_確定 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.LB_採点方式 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LB_区分名 = New System.Windows.Forms.Label()
        CType(Me.DGV_結果, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_結果
        '
        Me.DGV_結果.AllowUserToAddRows = False
        Me.DGV_結果.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_結果.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_結果.Location = New System.Drawing.Point(84, 98)
        Me.DGV_結果.Name = "DGV_結果"
        Me.DGV_結果.RowHeadersWidth = 51
        Me.DGV_結果.RowTemplate.Height = 24
        Me.DGV_結果.Size = New System.Drawing.Size(619, 211)
        Me.DGV_結果.TabIndex = 0
        '
        'PB_確定
        '
        Me.PB_確定.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_確定.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_確定.ForeColor = System.Drawing.Color.Blue
        Me.PB_確定.Location = New System.Drawing.Point(509, 327)
        Me.PB_確定.Name = "PB_確定"
        Me.PB_確定.Size = New System.Drawing.Size(194, 92)
        Me.PB_確定.TabIndex = 28
        Me.PB_確定.Text = "確定"
        Me.PB_確定.UseVisualStyleBackColor = True
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(84, 354)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 27
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'LB_採点方式
        '
        Me.LB_採点方式.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_採点方式.AutoSize = True
        Me.LB_採点方式.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_採点方式.ForeColor = System.Drawing.Color.Blue
        Me.LB_採点方式.Location = New System.Drawing.Point(215, 57)
        Me.LB_採点方式.Name = "LB_採点方式"
        Me.LB_採点方式.Size = New System.Drawing.Size(65, 24)
        Me.LB_採点方式.TabIndex = 32
        Me.LB_採点方式.Text = "xxxxx"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(81, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 24)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "採点方式"
        '
        'LB_区分名
        '
        Me.LB_区分名.AutoSize = True
        Me.LB_区分名.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分名.ForeColor = System.Drawing.Color.Blue
        Me.LB_区分名.Location = New System.Drawing.Point(80, 18)
        Me.LB_区分名.Name = "LB_区分名"
        Me.LB_区分名.Size = New System.Drawing.Size(82, 24)
        Me.LB_区分名.TabIndex = 30
        Me.LB_区分名.Text = "区分名"
        '
        'F513_結果入力
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.LB_採点方式)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LB_区分名)
        Me.Controls.Add(Me.PB_確定)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV_結果)
        Me.Name = "F513_結果入力"
        Me.Text = "F513_結果入力"
        CType(Me.DGV_結果, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGV_結果 As DataGridView
    Friend WithEvents PB_確定 As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents LB_採点方式 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents LB_区分名 As Label
End Class
