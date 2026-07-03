<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F150_団体区分設定
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
        Me.PB_保存 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.DGV_区分一覧 = New System.Windows.Forms.DataGridView()
        Me.LB_団体区分名 = New System.Windows.Forms.Label()
        Me.PB_団体集計方法設定 = New System.Windows.Forms.Button()
        Me.PB_団体集計 = New System.Windows.Forms.Button()
        CType(Me.DGV_区分一覧, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PB_保存
        '
        Me.PB_保存.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_保存.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_保存.Location = New System.Drawing.Point(241, 500)
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
        Me.PB_戻る.Location = New System.Drawing.Point(58, 500)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 15
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'DGV_区分一覧
        '
        Me.DGV_区分一覧.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_区分一覧.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_区分一覧.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_区分一覧.Location = New System.Drawing.Point(37, 73)
        Me.DGV_区分一覧.Name = "DGV_区分一覧"
        Me.DGV_区分一覧.RowHeadersWidth = 51
        Me.DGV_区分一覧.RowTemplate.Height = 24
        Me.DGV_区分一覧.Size = New System.Drawing.Size(1043, 411)
        Me.DGV_区分一覧.TabIndex = 14
        '
        'LB_団体区分名
        '
        Me.LB_団体区分名.AutoSize = True
        Me.LB_団体区分名.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_団体区分名.Location = New System.Drawing.Point(43, 25)
        Me.LB_団体区分名.Name = "LB_団体区分名"
        Me.LB_団体区分名.Size = New System.Drawing.Size(75, 24)
        Me.LB_団体区分名.TabIndex = 17
        Me.LB_団体区分名.Text = "Label1"
        '
        'PB_団体集計方法設定
        '
        Me.PB_団体集計方法設定.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_団体集計方法設定.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_団体集計方法設定.Location = New System.Drawing.Point(442, 500)
        Me.PB_団体集計方法設定.Name = "PB_団体集計方法設定"
        Me.PB_団体集計方法設定.Size = New System.Drawing.Size(300, 49)
        Me.PB_団体集計方法設定.TabIndex = 18
        Me.PB_団体集計方法設定.Text = "団体集計方法設定"
        Me.PB_団体集計方法設定.UseVisualStyleBackColor = True
        '
        'PB_団体集計
        '
        Me.PB_団体集計.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_団体集計.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_団体集計.Location = New System.Drawing.Point(897, 500)
        Me.PB_団体集計.Name = "PB_団体集計"
        Me.PB_団体集計.Size = New System.Drawing.Size(183, 49)
        Me.PB_団体集計.TabIndex = 19
        Me.PB_団体集計.Text = "団体集計"
        Me.PB_団体集計.UseVisualStyleBackColor = True
        '
        'F150_団体区分設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1117, 598)
        Me.Controls.Add(Me.PB_団体集計)
        Me.Controls.Add(Me.PB_団体集計方法設定)
        Me.Controls.Add(Me.LB_団体区分名)
        Me.Controls.Add(Me.PB_保存)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV_区分一覧)
        Me.Name = "F150_団体区分設定"
        Me.Text = "F150_団体区分設定"
        CType(Me.DGV_区分一覧, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PB_保存 As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents DGV_区分一覧 As DataGridView
    Friend WithEvents LB_団体区分名 As Label
    Friend WithEvents PB_団体集計方法設定 As Button
    Friend WithEvents PB_団体集計 As Button
End Class
