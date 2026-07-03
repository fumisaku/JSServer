<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F601_名簿
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
        Me.TB_タイトル = New System.Windows.Forms.TextBox()
        Me.TB_競技名 = New System.Windows.Forms.TextBox()
        Me.DGV_名簿 = New System.Windows.Forms.DataGridView()
        Me.PB_閉じる = New System.Windows.Forms.Button()
        Me.TB_ヒート番号 = New System.Windows.Forms.TextBox()
        Me.TB_種目名 = New System.Windows.Forms.TextBox()
        CType(Me.DGV_名簿, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TB_タイトル
        '
        Me.TB_タイトル.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_タイトル.BackColor = System.Drawing.SystemColors.Info
        Me.TB_タイトル.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_タイトル.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TB_タイトル.Location = New System.Drawing.Point(220, 24)
        Me.TB_タイトル.Name = "TB_タイトル"
        Me.TB_タイトル.ReadOnly = True
        Me.TB_タイトル.Size = New System.Drawing.Size(557, 34)
        Me.TB_タイトル.TabIndex = 0
        Me.TB_タイトル.Text = "決勝進出者名簿"
        Me.TB_タイトル.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_競技名
        '
        Me.TB_競技名.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_競技名.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_競技名.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_競技名.Location = New System.Drawing.Point(121, 64)
        Me.TB_競技名.Multiline = True
        Me.TB_競技名.Name = "TB_競技名"
        Me.TB_競技名.ReadOnly = True
        Me.TB_競技名.Size = New System.Drawing.Size(740, 36)
        Me.TB_競技名.TabIndex = 1
        Me.TB_競技名.Text = "028 全日本選手権スタンダード 準決勝"
        Me.TB_競技名.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DGV_名簿
        '
        Me.DGV_名簿.AllowUserToAddRows = False
        Me.DGV_名簿.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_名簿.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_名簿.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_名簿.Location = New System.Drawing.Point(34, 157)
        Me.DGV_名簿.Name = "DGV_名簿"
        Me.DGV_名簿.RowHeadersVisible = False
        Me.DGV_名簿.RowTemplate.Height = 24
        Me.DGV_名簿.Size = New System.Drawing.Size(942, 482)
        Me.DGV_名簿.TabIndex = 2
        '
        'PB_閉じる
        '
        Me.PB_閉じる.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_閉じる.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_閉じる.Location = New System.Drawing.Point(34, 659)
        Me.PB_閉じる.Name = "PB_閉じる"
        Me.PB_閉じる.Size = New System.Drawing.Size(146, 52)
        Me.PB_閉じる.TabIndex = 3
        Me.PB_閉じる.Text = "閉じる"
        Me.PB_閉じる.UseVisualStyleBackColor = True
        '
        'TB_ヒート番号
        '
        Me.TB_ヒート番号.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_ヒート番号.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_ヒート番号.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_ヒート番号.Location = New System.Drawing.Point(587, 117)
        Me.TB_ヒート番号.Name = "TB_ヒート番号"
        Me.TB_ヒート番号.ReadOnly = True
        Me.TB_ヒート番号.Size = New System.Drawing.Size(274, 34)
        Me.TB_ヒート番号.TabIndex = 14
        Me.TB_ヒート番号.Text = "種目数 10"
        Me.TB_ヒート番号.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_種目名
        '
        Me.TB_種目名.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_種目名.BackColor = System.Drawing.Color.LemonChiffon
        Me.TB_種目名.Font = New System.Drawing.Font("MS UI Gothic", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_種目名.Location = New System.Drawing.Point(121, 117)
        Me.TB_種目名.Name = "TB_種目名"
        Me.TB_種目名.ReadOnly = True
        Me.TB_種目名.Size = New System.Drawing.Size(447, 34)
        Me.TB_種目名.TabIndex = 15
        Me.TB_種目名.Text = "種目数 10"
        '
        'F601_名簿
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1006, 723)
        Me.Controls.Add(Me.TB_種目名)
        Me.Controls.Add(Me.TB_ヒート番号)
        Me.Controls.Add(Me.PB_閉じる)
        Me.Controls.Add(Me.DGV_名簿)
        Me.Controls.Add(Me.TB_競技名)
        Me.Controls.Add(Me.TB_タイトル)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Name = "F601_名簿"
        Me.Text = "F601_名簿"
        CType(Me.DGV_名簿, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TB_タイトル As TextBox
    Friend WithEvents TB_競技名 As TextBox
    Friend WithEvents DGV_名簿 As DataGridView
    Friend WithEvents PB_閉じる As Button
    Friend WithEvents TB_ヒート番号 As TextBox
    Friend WithEvents TB_種目名 As TextBox
End Class
