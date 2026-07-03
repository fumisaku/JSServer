<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F152_団体集計
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
        Me.LB_団体区分名 = New System.Windows.Forms.Label()
        Me.PB_更新 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.DGV_区分一覧 = New System.Windows.Forms.DataGridView()
        Me.PB_区分結果作成 = New System.Windows.Forms.Button()
        Me.PB_区分結果確認 = New System.Windows.Forms.Button()
        Me.PB_団体集計 = New System.Windows.Forms.Button()
        Me.PB_団体結果表示 = New System.Windows.Forms.Button()
        Me.PB_全チェック = New System.Windows.Forms.Button()
        CType(Me.DGV_区分一覧, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LB_団体区分名
        '
        Me.LB_団体区分名.AutoSize = True
        Me.LB_団体区分名.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_団体区分名.Location = New System.Drawing.Point(53, 21)
        Me.LB_団体区分名.Name = "LB_団体区分名"
        Me.LB_団体区分名.Size = New System.Drawing.Size(75, 24)
        Me.LB_団体区分名.TabIndex = 21
        Me.LB_団体区分名.Text = "Label1"
        '
        'PB_更新
        '
        Me.PB_更新.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_更新.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_更新.Location = New System.Drawing.Point(229, 537)
        Me.PB_更新.Name = "PB_更新"
        Me.PB_更新.Size = New System.Drawing.Size(148, 49)
        Me.PB_更新.TabIndex = 20
        Me.PB_更新.Text = "更新"
        Me.PB_更新.UseVisualStyleBackColor = True
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(46, 537)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 19
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
        Me.DGV_区分一覧.Location = New System.Drawing.Point(47, 105)
        Me.DGV_区分一覧.Name = "DGV_区分一覧"
        Me.DGV_区分一覧.RowHeadersWidth = 51
        Me.DGV_区分一覧.RowTemplate.Height = 24
        Me.DGV_区分一覧.Size = New System.Drawing.Size(1043, 411)
        Me.DGV_区分一覧.TabIndex = 18
        '
        'PB_区分結果作成
        '
        Me.PB_区分結果作成.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_区分結果作成.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_区分結果作成.Location = New System.Drawing.Point(482, 537)
        Me.PB_区分結果作成.Name = "PB_区分結果作成"
        Me.PB_区分結果作成.Size = New System.Drawing.Size(202, 49)
        Me.PB_区分結果作成.TabIndex = 22
        Me.PB_区分結果作成.Text = "区分結果作成"
        Me.PB_区分結果作成.UseVisualStyleBackColor = True
        '
        'PB_区分結果確認
        '
        Me.PB_区分結果確認.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_区分結果確認.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_区分結果確認.Location = New System.Drawing.Point(482, 600)
        Me.PB_区分結果確認.Name = "PB_区分結果確認"
        Me.PB_区分結果確認.Size = New System.Drawing.Size(202, 49)
        Me.PB_区分結果確認.TabIndex = 23
        Me.PB_区分結果確認.Text = "区分結果確認"
        Me.PB_区分結果確認.UseVisualStyleBackColor = True
        '
        'PB_団体集計
        '
        Me.PB_団体集計.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_団体集計.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_団体集計.Location = New System.Drawing.Point(888, 537)
        Me.PB_団体集計.Name = "PB_団体集計"
        Me.PB_団体集計.Size = New System.Drawing.Size(202, 49)
        Me.PB_団体集計.TabIndex = 24
        Me.PB_団体集計.Text = "団体集計"
        Me.PB_団体集計.UseVisualStyleBackColor = True
        '
        'PB_団体結果表示
        '
        Me.PB_団体結果表示.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_団体結果表示.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_団体結果表示.Location = New System.Drawing.Point(888, 592)
        Me.PB_団体結果表示.Name = "PB_団体結果表示"
        Me.PB_団体結果表示.Size = New System.Drawing.Size(202, 49)
        Me.PB_団体結果表示.TabIndex = 25
        Me.PB_団体結果表示.Text = "団体結果表示"
        Me.PB_団体結果表示.UseVisualStyleBackColor = True
        '
        'PB_全チェック
        '
        Me.PB_全チェック.Location = New System.Drawing.Point(46, 59)
        Me.PB_全チェック.Name = "PB_全チェック"
        Me.PB_全チェック.Size = New System.Drawing.Size(121, 40)
        Me.PB_全チェック.TabIndex = 26
        Me.PB_全チェック.Text = "全チェック"
        Me.PB_全チェック.UseVisualStyleBackColor = True
        '
        'F152_団体集計
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1151, 661)
        Me.Controls.Add(Me.PB_全チェック)
        Me.Controls.Add(Me.PB_団体結果表示)
        Me.Controls.Add(Me.PB_団体集計)
        Me.Controls.Add(Me.PB_区分結果確認)
        Me.Controls.Add(Me.PB_区分結果作成)
        Me.Controls.Add(Me.LB_団体区分名)
        Me.Controls.Add(Me.PB_更新)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.DGV_区分一覧)
        Me.Name = "F152_団体集計"
        Me.Text = "F152_団体集計"
        CType(Me.DGV_区分一覧, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LB_団体区分名 As Label
    Friend WithEvents PB_更新 As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents DGV_区分一覧 As DataGridView
    Friend WithEvents PB_区分結果作成 As Button
    Friend WithEvents PB_区分結果確認 As Button
    Friend WithEvents PB_団体集計 As Button
    Friend WithEvents PB_団体結果表示 As Button
    Friend WithEvents PB_全チェック As Button
End Class
