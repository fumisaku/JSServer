<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F113_BRカテゴリ設定
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
        Me.DGV_BRカテゴリ = New System.Windows.Forms.DataGridView()
        Me.PB_更新 = New System.Windows.Forms.Button()
        Me.PB_保存 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.PB_BR区分設定 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DGV_BRカテゴリ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_BRカテゴリ
        '
        Me.DGV_BRカテゴリ.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_BRカテゴリ.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_BRカテゴリ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_BRカテゴリ.Location = New System.Drawing.Point(37, 52)
        Me.DGV_BRカテゴリ.Name = "DGV_BRカテゴリ"
        Me.DGV_BRカテゴリ.RowHeadersWidth = 51
        Me.DGV_BRカテゴリ.RowTemplate.Height = 24
        Me.DGV_BRカテゴリ.Size = New System.Drawing.Size(1019, 298)
        Me.DGV_BRカテゴリ.TabIndex = 0
        '
        'PB_更新
        '
        Me.PB_更新.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_更新.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_更新.Location = New System.Drawing.Point(532, 375)
        Me.PB_更新.Name = "PB_更新"
        Me.PB_更新.Size = New System.Drawing.Size(153, 49)
        Me.PB_更新.TabIndex = 17
        Me.PB_更新.Text = "更新"
        Me.PB_更新.UseVisualStyleBackColor = True
        '
        'PB_保存
        '
        Me.PB_保存.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_保存.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_保存.Location = New System.Drawing.Point(271, 375)
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
        Me.PB_戻る.Location = New System.Drawing.Point(35, 375)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 15
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'PB_BR区分設定
        '
        Me.PB_BR区分設定.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_BR区分設定.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_BR区分設定.Location = New System.Drawing.Point(903, 375)
        Me.PB_BR区分設定.Name = "PB_BR区分設定"
        Me.PB_BR区分設定.Size = New System.Drawing.Size(153, 49)
        Me.PB_BR区分設定.TabIndex = 18
        Me.PB_BR区分設定.Text = "BR区分設定"
        Me.PB_BR区分設定.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(354, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(414, 24)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "ラウンド方式：「T」トーナメント 「S」ソロ順位"
        '
        'F113_BRカテゴリ設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1125, 450)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PB_BR区分設定)
        Me.Controls.Add(Me.PB_更新)
        Me.Controls.Add(Me.PB_保存)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV_BRカテゴリ)
        Me.Location = New System.Drawing.Point(918, 22)
        Me.Name = "F113_BRカテゴリ設定"
        Me.Text = "F113_BRカテゴリ設定"
        CType(Me.DGV_BRカテゴリ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGV_BRカテゴリ As DataGridView
    Friend WithEvents PB_更新 As Button
    Friend WithEvents PB_保存 As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents PB_BR区分設定 As Button
    Friend WithEvents Label1 As Label
End Class
