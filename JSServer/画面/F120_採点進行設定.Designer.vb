<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F120_採点進行設定
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
        Me.DGV_採点進行管理 = New System.Windows.Forms.DataGridView()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.PB_表示切替 = New System.Windows.Forms.Button()
        Me.PB_保存 = New System.Windows.Forms.Button()
        CType(Me.DGV_採点進行管理, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_採点進行管理
        '
        Me.DGV_採点進行管理.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_採点進行管理.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_採点進行管理.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_採点進行管理.Location = New System.Drawing.Point(40, 74)
        Me.DGV_採点進行管理.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DGV_採点進行管理.Name = "DGV_採点進行管理"
        Me.DGV_採点進行管理.RowTemplate.Height = 24
        Me.DGV_採点進行管理.Size = New System.Drawing.Size(792, 238)
        Me.DGV_採点進行管理.TabIndex = 0
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(40, 316)
        Me.PB_戻る.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(106, 39)
        Me.PB_戻る.TabIndex = 12
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'PB_表示切替
        '
        Me.PB_表示切替.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_表示切替.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_表示切替.Location = New System.Drawing.Point(620, 316)
        Me.PB_表示切替.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_表示切替.Name = "PB_表示切替"
        Me.PB_表示切替.Size = New System.Drawing.Size(106, 39)
        Me.PB_表示切替.TabIndex = 13
        Me.PB_表示切替.Text = "表示切替"
        Me.PB_表示切替.UseVisualStyleBackColor = True
        '
        'PB_保存
        '
        Me.PB_保存.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_保存.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_保存.Location = New System.Drawing.Point(272, 316)
        Me.PB_保存.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PB_保存.Name = "PB_保存"
        Me.PB_保存.Size = New System.Drawing.Size(111, 39)
        Me.PB_保存.TabIndex = 14
        Me.PB_保存.Text = "保存"
        Me.PB_保存.UseVisualStyleBackColor = True
        '
        'F120_採点進行設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(889, 360)
        Me.Controls.Add(Me.PB_保存)
        Me.Controls.Add(Me.PB_表示切替)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV_採点進行管理)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "F120_採点進行設定"
        Me.Text = "F120_採点進行設定"
        CType(Me.DGV_採点進行管理, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGV_採点進行管理 As DataGridView
    Friend WithEvents PB_戻る As Button
    Friend WithEvents PB_表示切替 As Button
    Friend WithEvents PB_保存 As Button
End Class
