<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F110_区分一覧設定
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
        Me.DGV_区分一覧 = New System.Windows.Forms.DataGridView()
        Me.PB_編集 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.PB_保存 = New System.Windows.Forms.Button()
        Me.PB_更新 = New System.Windows.Forms.Button()
        Me.PB_ブレイキン設定 = New System.Windows.Forms.Button()
        Me.PB_総合結果作成 = New System.Windows.Forms.Button()
        CType(Me.DGV_区分一覧, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_区分一覧
        '
        Me.DGV_区分一覧.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_区分一覧.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_区分一覧.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_区分一覧.Location = New System.Drawing.Point(52, 77)
        Me.DGV_区分一覧.Name = "DGV_区分一覧"
        Me.DGV_区分一覧.RowHeadersWidth = 51
        Me.DGV_区分一覧.RowTemplate.Height = 24
        Me.DGV_区分一覧.Size = New System.Drawing.Size(1043, 435)
        Me.DGV_区分一覧.TabIndex = 0
        '
        'PB_編集
        '
        Me.PB_編集.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_編集.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_編集.Location = New System.Drawing.Point(442, 528)
        Me.PB_編集.Name = "PB_編集"
        Me.PB_編集.Size = New System.Drawing.Size(153, 49)
        Me.PB_編集.TabIndex = 1
        Me.PB_編集.Text = "編集"
        Me.PB_編集.UseVisualStyleBackColor = True
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(73, 528)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 12
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'PB_保存
        '
        Me.PB_保存.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_保存.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_保存.Location = New System.Drawing.Point(256, 528)
        Me.PB_保存.Name = "PB_保存"
        Me.PB_保存.Size = New System.Drawing.Size(148, 49)
        Me.PB_保存.TabIndex = 13
        Me.PB_保存.Text = "保存"
        Me.PB_保存.UseVisualStyleBackColor = True
        '
        'PB_更新
        '
        Me.PB_更新.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_更新.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_更新.Location = New System.Drawing.Point(941, 528)
        Me.PB_更新.Name = "PB_更新"
        Me.PB_更新.Size = New System.Drawing.Size(153, 49)
        Me.PB_更新.TabIndex = 14
        Me.PB_更新.Text = "更新"
        Me.PB_更新.UseVisualStyleBackColor = True
        '
        'PB_ブレイキン設定
        '
        Me.PB_ブレイキン設定.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_ブレイキン設定.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_ブレイキン設定.Location = New System.Drawing.Point(918, 22)
        Me.PB_ブレイキン設定.Name = "PB_ブレイキン設定"
        Me.PB_ブレイキン設定.Size = New System.Drawing.Size(176, 41)
        Me.PB_ブレイキン設定.TabIndex = 15
        Me.PB_ブレイキン設定.Text = "ブレイキン設定"
        Me.PB_ブレイキン設定.UseVisualStyleBackColor = True
        '
        'PB_総合結果作成
        '
        Me.PB_総合結果作成.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_総合結果作成.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_総合結果作成.Location = New System.Drawing.Point(696, 528)
        Me.PB_総合結果作成.Name = "PB_総合結果作成"
        Me.PB_総合結果作成.Size = New System.Drawing.Size(202, 49)
        Me.PB_総合結果作成.TabIndex = 16
        Me.PB_総合結果作成.Text = "区分結果作成"
        Me.PB_総合結果作成.UseVisualStyleBackColor = True
        '
        'F110_区分一覧設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1146, 616)
        Me.Controls.Add(Me.PB_総合結果作成)
        Me.Controls.Add(Me.PB_ブレイキン設定)
        Me.Controls.Add(Me.PB_更新)
        Me.Controls.Add(Me.PB_保存)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.PB_編集)
        Me.Controls.Add(Me.DGV_区分一覧)
        Me.Name = "F110_区分一覧設定"
        Me.Text = "F110_区分一覧設定"
        CType(Me.DGV_区分一覧, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGV_区分一覧 As DataGridView
    Friend WithEvents PB_編集 As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents PB_保存 As Button
    Friend WithEvents PB_更新 As Button
    Friend WithEvents PB_ブレイキン設定 As Button
    Friend WithEvents PB_総合結果作成 As Button
End Class
