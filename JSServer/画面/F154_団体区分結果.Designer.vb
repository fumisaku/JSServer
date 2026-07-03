<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F154_団体区分結果
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
        Me.LB_区分名 = New System.Windows.Forms.Label()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.DGV_区分結果 = New System.Windows.Forms.DataGridView()
        CType(Me.DGV_区分結果, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LB_区分名
        '
        Me.LB_区分名.AutoSize = True
        Me.LB_区分名.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分名.ForeColor = System.Drawing.Color.Blue
        Me.LB_区分名.Location = New System.Drawing.Point(79, 27)
        Me.LB_区分名.Name = "LB_区分名"
        Me.LB_区分名.Size = New System.Drawing.Size(88, 28)
        Me.LB_区分名.TabIndex = 24
        Me.LB_区分名.Text = "Label1"
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(84, 670)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 23
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'DGV_区分結果
        '
        Me.DGV_区分結果.AllowUserToAddRows = False
        Me.DGV_区分結果.AllowUserToDeleteRows = False
        Me.DGV_区分結果.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_区分結果.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_区分結果.Location = New System.Drawing.Point(84, 73)
        Me.DGV_区分結果.Name = "DGV_区分結果"
        Me.DGV_区分結果.RowHeadersVisible = False
        Me.DGV_区分結果.RowHeadersWidth = 51
        Me.DGV_区分結果.RowTemplate.Height = 24
        Me.DGV_区分結果.Size = New System.Drawing.Size(1025, 564)
        Me.DGV_区分結果.TabIndex = 22
        '
        'F154_団体区分結果
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1192, 742)
        Me.Controls.Add(Me.LB_区分名)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV_区分結果)
        Me.Name = "F154_団体区分結果"
        Me.Text = "F154_団体区分結果"
        CType(Me.DGV_区分結果, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LB_区分名 As Label
    Friend WithEvents PB_戻る As Button
    Friend WithEvents DGV_区分結果 As DataGridView
End Class
