<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F602_進行詳細
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
        Me.DGV_進行詳細 = New System.Windows.Forms.DataGridView()
        Me.PB_閉じる = New System.Windows.Forms.Button()
        CType(Me.DGV_進行詳細, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_進行詳細
        '
        Me.DGV_進行詳細.AllowUserToAddRows = False
        Me.DGV_進行詳細.AllowUserToDeleteRows = False
        Me.DGV_進行詳細.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_進行詳細.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_進行詳細.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_進行詳細.Location = New System.Drawing.Point(34, 68)
        Me.DGV_進行詳細.Name = "DGV_進行詳細"
        Me.DGV_進行詳細.RowHeadersVisible = False
        Me.DGV_進行詳細.RowTemplate.Height = 24
        Me.DGV_進行詳細.Size = New System.Drawing.Size(897, 562)
        Me.DGV_進行詳細.TabIndex = 0
        '
        'PB_閉じる
        '
        Me.PB_閉じる.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_閉じる.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_閉じる.Location = New System.Drawing.Point(34, 650)
        Me.PB_閉じる.Name = "PB_閉じる"
        Me.PB_閉じる.Size = New System.Drawing.Size(146, 52)
        Me.PB_閉じる.TabIndex = 4
        Me.PB_閉じる.Text = "閉じる"
        Me.PB_閉じる.UseVisualStyleBackColor = True
        '
        'F602_進行詳細
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1006, 723)
        Me.Controls.Add(Me.PB_閉じる)
        Me.Controls.Add(Me.DGV_進行詳細)
        Me.Name = "F602_進行詳細"
        Me.Text = "F602_進行詳細"
        CType(Me.DGV_進行詳細, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGV_進行詳細 As DataGridView
    Friend WithEvents PB_閉じる As Button
End Class
