<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F200_採点進行確認
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
        Me.PB_ヒート表 = New System.Windows.Forms.Button()
        Me.PB_結果確認 = New System.Windows.Forms.Button()
        Me.PB_印刷 = New System.Windows.Forms.Button()
        Me.PB_更新 = New System.Windows.Forms.Button()
        Me.PB_ステータス戻し = New System.Windows.Forms.Button()
        CType(Me.DGV_採点進行管理, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_採点進行管理
        '
        Me.DGV_採点進行管理.AllowUserToAddRows = False
        Me.DGV_採点進行管理.AllowUserToDeleteRows = False
        Me.DGV_採点進行管理.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_採点進行管理.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_採点進行管理.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_採点進行管理.Location = New System.Drawing.Point(37, 49)
        Me.DGV_採点進行管理.Name = "DGV_採点進行管理"
        Me.DGV_採点進行管理.ReadOnly = True
        Me.DGV_採点進行管理.RowHeadersVisible = False
        Me.DGV_採点進行管理.RowHeadersWidth = 51
        Me.DGV_採点進行管理.RowTemplate.Height = 24
        Me.DGV_採点進行管理.Size = New System.Drawing.Size(895, 538)
        Me.DGV_採点進行管理.TabIndex = 1
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(37, 671)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 13
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'PB_ヒート表
        '
        Me.PB_ヒート表.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_ヒート表.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_ヒート表.Location = New System.Drawing.Point(36, 593)
        Me.PB_ヒート表.Name = "PB_ヒート表"
        Me.PB_ヒート表.Size = New System.Drawing.Size(236, 58)
        Me.PB_ヒート表.TabIndex = 14
        Me.PB_ヒート表.Text = "ヒート表確認・作成"
        Me.PB_ヒート表.UseVisualStyleBackColor = True
        '
        'PB_結果確認
        '
        Me.PB_結果確認.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_結果確認.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_結果確認.Location = New System.Drawing.Point(296, 593)
        Me.PB_結果確認.Name = "PB_結果確認"
        Me.PB_結果確認.Size = New System.Drawing.Size(156, 58)
        Me.PB_結果確認.TabIndex = 15
        Me.PB_結果確認.Text = "結果確認"
        Me.PB_結果確認.UseVisualStyleBackColor = True
        '
        'PB_印刷
        '
        Me.PB_印刷.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_印刷.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_印刷.Location = New System.Drawing.Point(470, 593)
        Me.PB_印刷.Name = "PB_印刷"
        Me.PB_印刷.Size = New System.Drawing.Size(156, 58)
        Me.PB_印刷.TabIndex = 16
        Me.PB_印刷.Text = "帳票印刷"
        Me.PB_印刷.UseVisualStyleBackColor = True
        '
        'PB_更新
        '
        Me.PB_更新.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_更新.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_更新.Location = New System.Drawing.Point(791, 602)
        Me.PB_更新.Name = "PB_更新"
        Me.PB_更新.Size = New System.Drawing.Size(141, 49)
        Me.PB_更新.TabIndex = 17
        Me.PB_更新.Text = "更新"
        Me.PB_更新.UseVisualStyleBackColor = True
        '
        'PB_ステータス戻し
        '
        Me.PB_ステータス戻し.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_ステータス戻し.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_ステータス戻し.Location = New System.Drawing.Point(436, 662)
        Me.PB_ステータス戻し.Name = "PB_ステータス戻し"
        Me.PB_ステータス戻し.Size = New System.Drawing.Size(190, 58)
        Me.PB_ステータス戻し.TabIndex = 18
        Me.PB_ステータス戻し.Text = "ステータス戻し"
        Me.PB_ステータス戻し.UseVisualStyleBackColor = True
        '
        'F200_採点進行確認
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1006, 723)
        Me.Controls.Add(Me.PB_ステータス戻し)
        Me.Controls.Add(Me.PB_更新)
        Me.Controls.Add(Me.PB_印刷)
        Me.Controls.Add(Me.PB_結果確認)
        Me.Controls.Add(Me.PB_ヒート表)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV_採点進行管理)
        Me.Name = "F200_採点進行確認"
        Me.Text = "F200_採点進行確認"
        CType(Me.DGV_採点進行管理, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGV_採点進行管理 As DataGridView
    Friend WithEvents PB_戻る As Button
    Friend WithEvents PB_ヒート表 As Button
    Friend WithEvents PB_結果確認 As Button
    Friend WithEvents PB_印刷 As Button
    Friend WithEvents PB_更新 As Button
    Friend WithEvents PB_ステータス戻し As Button
End Class
