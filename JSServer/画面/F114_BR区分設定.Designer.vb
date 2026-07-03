<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F114_BR区分設定
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
        Me.DGV_BR区分一覧 = New System.Windows.Forms.DataGridView()
        Me.PB_更新 = New System.Windows.Forms.Button()
        Me.PB_保存 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.PB_結果クリア = New System.Windows.Forms.Button()
        CType(Me.DGV_BR区分一覧, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_BR区分一覧
        '
        Me.DGV_BR区分一覧.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_BR区分一覧.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_BR区分一覧.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_BR区分一覧.Location = New System.Drawing.Point(58, 55)
        Me.DGV_BR区分一覧.Name = "DGV_BR区分一覧"
        Me.DGV_BR区分一覧.RowHeadersWidth = 51
        Me.DGV_BR区分一覧.RowTemplate.Height = 24
        Me.DGV_BR区分一覧.Size = New System.Drawing.Size(853, 519)
        Me.DGV_BR区分一覧.TabIndex = 0
        '
        'PB_更新
        '
        Me.PB_更新.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_更新.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_更新.Location = New System.Drawing.Point(479, 608)
        Me.PB_更新.Name = "PB_更新"
        Me.PB_更新.Size = New System.Drawing.Size(153, 49)
        Me.PB_更新.TabIndex = 20
        Me.PB_更新.Text = "更新"
        Me.PB_更新.UseVisualStyleBackColor = True
        '
        'PB_保存
        '
        Me.PB_保存.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_保存.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_保存.Location = New System.Drawing.Point(253, 608)
        Me.PB_保存.Name = "PB_保存"
        Me.PB_保存.Size = New System.Drawing.Size(148, 49)
        Me.PB_保存.TabIndex = 19
        Me.PB_保存.Text = "保存"
        Me.PB_保存.UseVisualStyleBackColor = True
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(56, 608)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 18
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'PB_結果クリア
        '
        Me.PB_結果クリア.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_結果クリア.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_結果クリア.Location = New System.Drawing.Point(777, 608)
        Me.PB_結果クリア.Name = "PB_結果クリア"
        Me.PB_結果クリア.Size = New System.Drawing.Size(134, 49)
        Me.PB_結果クリア.TabIndex = 21
        Me.PB_結果クリア.Text = "結果クリア"
        Me.PB_結果クリア.UseVisualStyleBackColor = True
        '
        'F114_BR区分設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(990, 669)
        Me.Controls.Add(Me.PB_結果クリア)
        Me.Controls.Add(Me.PB_更新)
        Me.Controls.Add(Me.PB_保存)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV_BR区分一覧)
        Me.Name = "F114_BR区分設定"
        Me.Text = "F114_BR区分設定"
        CType(Me.DGV_BR区分一覧, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGV_BR区分一覧 As DataGridView
    Friend WithEvents PB_更新 As Button
    Friend WithEvents PB_保存 As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents PB_結果クリア As Button
End Class
