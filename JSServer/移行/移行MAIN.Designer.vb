<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 移行MAIN
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
        Me.PB_移行開始 = New System.Windows.Forms.Button()
        Me.TB_NJパス = New System.Windows.Forms.TextBox()
        Me.TB_競技会NO = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PB_競技結果取込み = New System.Windows.Forms.Button()
        Me.LB区分番号 = New System.Windows.Forms.Label()
        Me.TB_区分番号 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'PB_移行開始
        '
        Me.PB_移行開始.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_移行開始.Location = New System.Drawing.Point(53, 299)
        Me.PB_移行開始.Name = "PB_移行開始"
        Me.PB_移行開始.Size = New System.Drawing.Size(169, 59)
        Me.PB_移行開始.TabIndex = 0
        Me.PB_移行開始.Text = "移行開始"
        Me.PB_移行開始.UseVisualStyleBackColor = True
        '
        'TB_NJパス
        '
        Me.TB_NJパス.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_NJパス.Location = New System.Drawing.Point(129, 56)
        Me.TB_NJパス.Name = "TB_NJパス"
        Me.TB_NJパス.Size = New System.Drawing.Size(544, 27)
        Me.TB_NJパス.TabIndex = 1
        '
        'TB_競技会NO
        '
        Me.TB_競技会NO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_競技会NO.Location = New System.Drawing.Point(129, 116)
        Me.TB_競技会NO.Name = "TB_競技会NO"
        Me.TB_競技会NO.Size = New System.Drawing.Size(141, 27)
        Me.TB_競技会NO.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DodgerBlue
        Me.Label1.Location = New System.Drawing.Point(21, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "NJパス"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DodgerBlue
        Me.Label2.Location = New System.Drawing.Point(21, 123)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "競技会NO"
        '
        'PB_競技結果取込み
        '
        Me.PB_競技結果取込み.Location = New System.Drawing.Point(482, 299)
        Me.PB_競技結果取込み.Name = "PB_競技結果取込み"
        Me.PB_競技結果取込み.Size = New System.Drawing.Size(168, 59)
        Me.PB_競技結果取込み.TabIndex = 5
        Me.PB_競技結果取込み.Text = "競技結果取込み"
        Me.PB_競技結果取込み.UseVisualStyleBackColor = True
        '
        'LB区分番号
        '
        Me.LB区分番号.AutoSize = True
        Me.LB区分番号.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB区分番号.ForeColor = System.Drawing.Color.DodgerBlue
        Me.LB区分番号.Location = New System.Drawing.Point(421, 119)
        Me.LB区分番号.Name = "LB区分番号"
        Me.LB区分番号.Size = New System.Drawing.Size(93, 20)
        Me.LB区分番号.TabIndex = 7
        Me.LB区分番号.Text = "区分番号"
        '
        'TB_区分番号
        '
        Me.TB_区分番号.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_区分番号.Location = New System.Drawing.Point(529, 112)
        Me.TB_区分番号.Name = "TB_区分番号"
        Me.TB_区分番号.Size = New System.Drawing.Size(141, 27)
        Me.TB_区分番号.TabIndex = 6
        '
        '移行MAIN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.LB区分番号)
        Me.Controls.Add(Me.TB_区分番号)
        Me.Controls.Add(Me.PB_競技結果取込み)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TB_競技会NO)
        Me.Controls.Add(Me.TB_NJパス)
        Me.Controls.Add(Me.PB_移行開始)
        Me.Name = "移行MAIN"
        Me.Text = "移行MAIN"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PB_移行開始 As Button
    Friend WithEvents TB_NJパス As TextBox
    Friend WithEvents TB_競技会NO As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PB_競技結果取込み As Button
    Friend WithEvents LB区分番号 As Label
    Friend WithEvents TB_区分番号 As TextBox
End Class
