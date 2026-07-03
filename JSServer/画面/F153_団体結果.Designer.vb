<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F153_団体結果
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
        Me.DGV_結果 = New System.Windows.Forms.DataGridView()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.LB_団体区分名 = New System.Windows.Forms.Label()
        Me.PB_区分結果 = New System.Windows.Forms.Button()
        Me.PB_CSV書き出し = New System.Windows.Forms.Button()
        CType(Me.DGV_結果, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_結果
        '
        Me.DGV_結果.AllowUserToAddRows = False
        Me.DGV_結果.AllowUserToDeleteRows = False
        Me.DGV_結果.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_結果.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_結果.Location = New System.Drawing.Point(63, 83)
        Me.DGV_結果.Name = "DGV_結果"
        Me.DGV_結果.ReadOnly = True
        Me.DGV_結果.RowHeadersVisible = False
        Me.DGV_結果.RowHeadersWidth = 51
        Me.DGV_結果.RowTemplate.Height = 24
        Me.DGV_結果.Size = New System.Drawing.Size(1166, 586)
        Me.DGV_結果.TabIndex = 0
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(63, 713)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 20
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'LB_団体区分名
        '
        Me.LB_団体区分名.AutoSize = True
        Me.LB_団体区分名.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_団体区分名.Location = New System.Drawing.Point(59, 32)
        Me.LB_団体区分名.Name = "LB_団体区分名"
        Me.LB_団体区分名.Size = New System.Drawing.Size(75, 24)
        Me.LB_団体区分名.TabIndex = 22
        Me.LB_団体区分名.Text = "Label1"
        '
        'PB_区分結果
        '
        Me.PB_区分結果.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_区分結果.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_区分結果.Location = New System.Drawing.Point(446, 713)
        Me.PB_区分結果.Name = "PB_区分結果"
        Me.PB_区分結果.Size = New System.Drawing.Size(141, 49)
        Me.PB_区分結果.TabIndex = 23
        Me.PB_区分結果.Text = "区分結果"
        Me.PB_区分結果.UseVisualStyleBackColor = True
        '
        'PB_CSV書き出し
        '
        Me.PB_CSV書き出し.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_CSV書き出し.Location = New System.Drawing.Point(807, 713)
        Me.PB_CSV書き出し.Name = "PB_CSV書き出し"
        Me.PB_CSV書き出し.Size = New System.Drawing.Size(197, 49)
        Me.PB_CSV書き出し.TabIndex = 24
        Me.PB_CSV書き出し.Text = "CSV書き出し"
        Me.PB_CSV書き出し.UseVisualStyleBackColor = True
        '
        'F153_団体結果
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1298, 794)
        Me.Controls.Add(Me.PB_CSV書き出し)
        Me.Controls.Add(Me.PB_区分結果)
        Me.Controls.Add(Me.LB_団体区分名)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV_結果)
        Me.Name = "F153_団体結果"
        Me.Text = "F153_団体結果"
        CType(Me.DGV_結果, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGV_結果 As DataGridView
    Friend WithEvents PB_戻る As Button
    Friend WithEvents LB_団体区分名 As Label
    Friend WithEvents PB_区分結果 As Button
    Friend WithEvents PB_CSV書き出し As Button
End Class
