<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGM01_端末一覧
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
        Me.DGV_JS一覧 = New System.Windows.Forms.DataGridView()
        Me.PB_更新 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LB_端末数 = New System.Windows.Forms.Label()
        CType(Me.DGV_JS一覧, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_JS一覧
        '
        Me.DGV_JS一覧.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_JS一覧.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_JS一覧.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_JS一覧.Location = New System.Drawing.Point(41, 57)
        Me.DGV_JS一覧.Name = "DGV_JS一覧"
        Me.DGV_JS一覧.RowHeadersWidth = 51
        Me.DGV_JS一覧.RowTemplate.Height = 24
        Me.DGV_JS一覧.Size = New System.Drawing.Size(712, 268)
        Me.DGV_JS一覧.TabIndex = 0
        '
        'PB_更新
        '
        Me.PB_更新.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_更新.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_更新.Location = New System.Drawing.Point(600, 361)
        Me.PB_更新.Name = "PB_更新"
        Me.PB_更新.Size = New System.Drawing.Size(153, 49)
        Me.PB_更新.TabIndex = 22
        Me.PB_更新.Text = "更新"
        Me.PB_更新.UseVisualStyleBackColor = True
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(41, 361)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 21
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(539, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 20)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "接続端末数:"
        '
        'LB_端末数
        '
        Me.LB_端末数.AutoSize = True
        Me.LB_端末数.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_端末数.Location = New System.Drawing.Point(669, 34)
        Me.LB_端末数.Name = "LB_端末数"
        Me.LB_端末数.Size = New System.Drawing.Size(0, 20)
        Me.LB_端末数.TabIndex = 24
        '
        'FGM01_端末一覧
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.LB_端末数)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PB_更新)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV_JS一覧)
        Me.Name = "FGM01_端末一覧"
        Me.Text = "FGM01_端末一覧"
        CType(Me.DGV_JS一覧, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGV_JS一覧 As DataGridView
    Friend WithEvents PB_更新 As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents LB_端末数 As Label
End Class
