<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F100_競技会設定
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
        Me.DGV_競技会設定 = New System.Windows.Forms.DataGridView()
        Me.PB_保存 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        CType(Me.DGV_競技会設定, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_競技会設定
        '
        Me.DGV_競技会設定.AllowUserToAddRows = False
        Me.DGV_競技会設定.AllowUserToDeleteRows = False
        Me.DGV_競技会設定.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_競技会設定.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_競技会設定.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_競技会設定.Location = New System.Drawing.Point(59, 74)
        Me.DGV_競技会設定.Name = "DGV_競技会設定"
        Me.DGV_競技会設定.RowHeadersVisible = False
        Me.DGV_競技会設定.RowTemplate.Height = 24
        Me.DGV_競技会設定.Size = New System.Drawing.Size(859, 284)
        Me.DGV_競技会設定.TabIndex = 0
        '
        'PB_保存
        '
        Me.PB_保存.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_保存.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_保存.Location = New System.Drawing.Point(367, 380)
        Me.PB_保存.Name = "PB_保存"
        Me.PB_保存.Size = New System.Drawing.Size(148, 49)
        Me.PB_保存.TabIndex = 18
        Me.PB_保存.Text = "保存"
        Me.PB_保存.UseVisualStyleBackColor = True
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(59, 380)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 17
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'F100_競技会設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(986, 450)
        Me.Controls.Add(Me.PB_保存)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV_競技会設定)
        Me.Name = "F100_競技会設定"
        Me.Text = "F100_競技会設定"
        CType(Me.DGV_競技会設定, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGV_競技会設定 As DataGridView
    Friend WithEvents PB_保存 As Button
    Friend WithEvents PB_戻る As Button
End Class
