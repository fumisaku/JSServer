<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F510_UP数確定
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
        Me.DGV_UP数 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LB_出場組数 = New System.Windows.Forms.Label()
        Me.LB_UP予定数 = New System.Windows.Forms.Label()
        Me.LB_UP数 = New System.Windows.Forms.Label()
        Me.PB_次へ = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.PB_同点決勝 = New System.Windows.Forms.Button()
        CType(Me.DGV_UP数, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LB_区分名
        '
        Me.LB_区分名.AutoSize = True
        Me.LB_区分名.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分名.ForeColor = System.Drawing.Color.Blue
        Me.LB_区分名.Location = New System.Drawing.Point(60, 21)
        Me.LB_区分名.Name = "LB_区分名"
        Me.LB_区分名.Size = New System.Drawing.Size(82, 24)
        Me.LB_区分名.TabIndex = 5
        Me.LB_区分名.Text = "区分名"
        '
        'DGV_UP数
        '
        Me.DGV_UP数.AllowUserToAddRows = False
        Me.DGV_UP数.AllowUserToDeleteRows = False
        Me.DGV_UP数.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_UP数.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_UP数.Location = New System.Drawing.Point(85, 81)
        Me.DGV_UP数.Name = "DGV_UP数"
        Me.DGV_UP数.ReadOnly = True
        Me.DGV_UP数.RowHeadersVisible = False
        Me.DGV_UP数.RowHeadersWidth = 51
        Me.DGV_UP数.RowTemplate.Height = 24
        Me.DGV_UP数.Size = New System.Drawing.Size(631, 476)
        Me.DGV_UP数.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(742, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 24)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "出場組数"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(742, 139)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 24)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "UP予定数"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(742, 213)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 24)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "UP数"
        '
        'LB_出場組数
        '
        Me.LB_出場組数.AutoSize = True
        Me.LB_出場組数.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_出場組数.ForeColor = System.Drawing.Color.Blue
        Me.LB_出場組数.Location = New System.Drawing.Point(865, 94)
        Me.LB_出場組数.Name = "LB_出場組数"
        Me.LB_出場組数.Size = New System.Drawing.Size(34, 24)
        Me.LB_出場組数.TabIndex = 10
        Me.LB_出場組数.Text = "組"
        '
        'LB_UP予定数
        '
        Me.LB_UP予定数.AutoSize = True
        Me.LB_UP予定数.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_UP予定数.ForeColor = System.Drawing.Color.Blue
        Me.LB_UP予定数.Location = New System.Drawing.Point(865, 139)
        Me.LB_UP予定数.Name = "LB_UP予定数"
        Me.LB_UP予定数.Size = New System.Drawing.Size(34, 24)
        Me.LB_UP予定数.TabIndex = 11
        Me.LB_UP予定数.Text = "組"
        '
        'LB_UP数
        '
        Me.LB_UP数.AutoSize = True
        Me.LB_UP数.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_UP数.ForeColor = System.Drawing.Color.Blue
        Me.LB_UP数.Location = New System.Drawing.Point(865, 213)
        Me.LB_UP数.Name = "LB_UP数"
        Me.LB_UP数.Size = New System.Drawing.Size(34, 24)
        Me.LB_UP数.TabIndex = 12
        Me.LB_UP数.Text = "組"
        '
        'PB_次へ
        '
        Me.PB_次へ.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_次へ.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_次へ.Location = New System.Drawing.Point(771, 343)
        Me.PB_次へ.Name = "PB_次へ"
        Me.PB_次へ.Size = New System.Drawing.Size(148, 49)
        Me.PB_次へ.TabIndex = 22
        Me.PB_次へ.Text = "ヒート作成へ"
        Me.PB_次へ.UseVisualStyleBackColor = True
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(83, 593)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 21
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'PB_同点決勝
        '
        Me.PB_同点決勝.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_同点決勝.Location = New System.Drawing.Point(771, 475)
        Me.PB_同点決勝.Name = "PB_同点決勝"
        Me.PB_同点決勝.Size = New System.Drawing.Size(160, 52)
        Me.PB_同点決勝.TabIndex = 23
        Me.PB_同点決勝.Text = "同点決勝へ"
        Me.PB_同点決勝.UseVisualStyleBackColor = True
        '
        'F510_UP数確定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1006, 723)
        Me.Controls.Add(Me.PB_同点決勝)
        Me.Controls.Add(Me.PB_次へ)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.LB_UP数)
        Me.Controls.Add(Me.LB_UP予定数)
        Me.Controls.Add(Me.LB_出場組数)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DGV_UP数)
        Me.Controls.Add(Me.LB_区分名)
        Me.Name = "F510_UP数確定"
        Me.Text = "F510_UP数確定"
        CType(Me.DGV_UP数, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LB_区分名 As Label
    Friend WithEvents DGV_UP数 As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents LB_出場組数 As Label
    Friend WithEvents LB_UP予定数 As Label
    Friend WithEvents LB_UP数 As Label
    Friend WithEvents PB_次へ As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents PB_同点決勝 As Button
End Class
