<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F302_BRHTML作成
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
        Me.PB_SFTP = New System.Windows.Forms.Button()
        Me.LB_区分名 = New System.Windows.Forms.Label()
        Me.PB_詳細 = New System.Windows.Forms.Button()
        Me.PB_一括 = New System.Windows.Forms.Button()
        Me.PB_INIT = New System.Windows.Forms.Button()
        Me.PB_一覧CSV作成 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'PB_SFTP
        '
        Me.PB_SFTP.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_SFTP.Location = New System.Drawing.Point(71, 417)
        Me.PB_SFTP.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_SFTP.Name = "PB_SFTP"
        Me.PB_SFTP.Size = New System.Drawing.Size(221, 62)
        Me.PB_SFTP.TabIndex = 19
        Me.PB_SFTP.Text = "サーバーへのUP"
        Me.PB_SFTP.UseVisualStyleBackColor = True
        '
        'LB_区分名
        '
        Me.LB_区分名.AutoSize = True
        Me.LB_区分名.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分名.ForeColor = System.Drawing.Color.Blue
        Me.LB_区分名.Location = New System.Drawing.Point(67, 46)
        Me.LB_区分名.Name = "LB_区分名"
        Me.LB_区分名.Size = New System.Drawing.Size(82, 24)
        Me.LB_区分名.TabIndex = 17
        Me.LB_区分名.Text = "区分名"
        '
        'PB_詳細
        '
        Me.PB_詳細.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_詳細.Location = New System.Drawing.Point(71, 110)
        Me.PB_詳細.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_詳細.Name = "PB_詳細"
        Me.PB_詳細.Size = New System.Drawing.Size(150, 62)
        Me.PB_詳細.TabIndex = 18
        Me.PB_詳細.Text = "個別詳細" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "HTML"
        Me.PB_詳細.UseVisualStyleBackColor = True
        '
        'PB_一括
        '
        Me.PB_一括.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_一括.Location = New System.Drawing.Point(71, 240)
        Me.PB_一括.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_一括.Name = "PB_一括"
        Me.PB_一括.Size = New System.Drawing.Size(221, 62)
        Me.PB_一括.TabIndex = 20
        Me.PB_一括.Text = "全区分一括" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "結果HTML作成"
        Me.PB_一括.UseVisualStyleBackColor = True
        '
        'PB_INIT
        '
        Me.PB_INIT.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_INIT.Location = New System.Drawing.Point(365, 417)
        Me.PB_INIT.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_INIT.Name = "PB_INIT"
        Me.PB_INIT.Size = New System.Drawing.Size(241, 62)
        Me.PB_INIT.TabIndex = 21
        Me.PB_INIT.Text = "競技会一覧初期化"
        Me.PB_INIT.UseVisualStyleBackColor = True
        '
        'PB_一覧CSV作成
        '
        Me.PB_一覧CSV作成.Location = New System.Drawing.Point(520, 115)
        Me.PB_一覧CSV作成.Name = "PB_一覧CSV作成"
        Me.PB_一覧CSV作成.Size = New System.Drawing.Size(167, 57)
        Me.PB_一覧CSV作成.TabIndex = 22
        Me.PB_一覧CSV作成.Text = "一覧CSV作成"
        Me.PB_一覧CSV作成.UseVisualStyleBackColor = True
        '
        'F302_BRHTML作成
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(720, 532)
        Me.Controls.Add(Me.PB_一覧CSV作成)
        Me.Controls.Add(Me.PB_INIT)
        Me.Controls.Add(Me.PB_一括)
        Me.Controls.Add(Me.PB_SFTP)
        Me.Controls.Add(Me.LB_区分名)
        Me.Controls.Add(Me.PB_詳細)
        Me.Name = "F302_BRHTML作成"
        Me.Text = "F302_BRHTML作成"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PB_SFTP As Button
    Friend WithEvents LB_区分名 As Label
    Friend WithEvents PB_詳細 As Button
    Friend WithEvents PB_一括 As Button
    Friend WithEvents PB_INIT As Button
    Friend WithEvents PB_一覧CSV作成 As Button
End Class
