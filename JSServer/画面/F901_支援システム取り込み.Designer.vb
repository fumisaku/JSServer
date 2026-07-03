<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F901_支援システム取り込み
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
        Me.PB_JSパス = New System.Windows.Forms.Button()
        Me.TB_支援システムパス = New System.Windows.Forms.TextBox()
        Me.TB_競技会パス = New System.Windows.Forms.TextBox()
        Me.PB_競技会パス = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PB_Member = New System.Windows.Forms.Button()
        Me.PB_ジャッジ = New System.Windows.Forms.Button()
        Me.PB_競技会データ = New System.Windows.Forms.Button()
        Me.PB_結果取り込み = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'PB_JSパス
        '
        Me.PB_JSパス.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_JSパス.Location = New System.Drawing.Point(107, 52)
        Me.PB_JSパス.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_JSパス.Name = "PB_JSパス"
        Me.PB_JSパス.Size = New System.Drawing.Size(85, 59)
        Me.PB_JSパス.TabIndex = 0
        Me.PB_JSパス.Text = "フォルダ選択"
        Me.PB_JSパス.UseVisualStyleBackColor = True
        '
        'TB_支援システムパス
        '
        Me.TB_支援システムパス.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_支援システムパス.Location = New System.Drawing.Point(199, 69)
        Me.TB_支援システムパス.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_支援システムパス.Name = "TB_支援システムパス"
        Me.TB_支援システムパス.Size = New System.Drawing.Size(753, 27)
        Me.TB_支援システムパス.TabIndex = 1
        '
        'TB_競技会パス
        '
        Me.TB_競技会パス.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_競技会パス.Location = New System.Drawing.Point(199, 161)
        Me.TB_競技会パス.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_競技会パス.Name = "TB_競技会パス"
        Me.TB_競技会パス.Size = New System.Drawing.Size(753, 27)
        Me.TB_競技会パス.TabIndex = 3
        '
        'PB_競技会パス
        '
        Me.PB_競技会パス.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_競技会パス.Location = New System.Drawing.Point(107, 145)
        Me.PB_競技会パス.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_競技会パス.Name = "PB_競技会パス"
        Me.PB_競技会パス.Size = New System.Drawing.Size(85, 59)
        Me.PB_競技会パス.TabIndex = 2
        Me.PB_競技会パス.Text = "フォルダ選択"
        Me.PB_競技会パス.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(53, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(172, 20)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "支援システムパス名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(53, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 20)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "競技会パス名"
        '
        'PB_Member
        '
        Me.PB_Member.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_Member.Location = New System.Drawing.Point(504, 298)
        Me.PB_Member.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_Member.Name = "PB_Member"
        Me.PB_Member.Size = New System.Drawing.Size(219, 55)
        Me.PB_Member.TabIndex = 6
        Me.PB_Member.Text = "出場者一覧取込み"
        Me.PB_Member.UseVisualStyleBackColor = True
        '
        'PB_ジャッジ
        '
        Me.PB_ジャッジ.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_ジャッジ.Location = New System.Drawing.Point(504, 358)
        Me.PB_ジャッジ.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_ジャッジ.Name = "PB_ジャッジ"
        Me.PB_ジャッジ.Size = New System.Drawing.Size(219, 55)
        Me.PB_ジャッジ.TabIndex = 7
        Me.PB_ジャッジ.Text = "審判員取込み"
        Me.PB_ジャッジ.UseVisualStyleBackColor = True
        Me.PB_ジャッジ.Visible = False
        '
        'PB_競技会データ
        '
        Me.PB_競技会データ.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_競技会データ.Location = New System.Drawing.Point(107, 298)
        Me.PB_競技会データ.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_競技会データ.Name = "PB_競技会データ"
        Me.PB_競技会データ.Size = New System.Drawing.Size(221, 55)
        Me.PB_競技会データ.TabIndex = 8
        Me.PB_競技会データ.Text = "競技会データ取込み"
        Me.PB_競技会データ.UseVisualStyleBackColor = True
        '
        'PB_結果取り込み
        '
        Me.PB_結果取り込み.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_結果取り込み.Location = New System.Drawing.Point(107, 544)
        Me.PB_結果取り込み.Name = "PB_結果取り込み"
        Me.PB_結果取り込み.Size = New System.Drawing.Size(310, 63)
        Me.PB_結果取り込み.TabIndex = 9
        Me.PB_結果取り込み.Text = "ヒート表/結果取り込み"
        Me.PB_結果取り込み.UseVisualStyleBackColor = True
        '
        'F901_支援システム取り込み
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1005, 722)
        Me.Controls.Add(Me.PB_結果取り込み)
        Me.Controls.Add(Me.PB_競技会データ)
        Me.Controls.Add(Me.PB_ジャッジ)
        Me.Controls.Add(Me.PB_Member)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TB_競技会パス)
        Me.Controls.Add(Me.PB_競技会パス)
        Me.Controls.Add(Me.TB_支援システムパス)
        Me.Controls.Add(Me.PB_JSパス)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "F901_支援システム取り込み"
        Me.Text = "F901_支援システム取り込み"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PB_JSパス As Button
    Friend WithEvents TB_支援システムパス As TextBox
    Friend WithEvents TB_競技会パス As TextBox
    Friend WithEvents PB_競技会パス As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PB_Member As Button
    Friend WithEvents PB_ジャッジ As Button
    Friend WithEvents PB_競技会データ As Button
    Friend WithEvents PB_結果取り込み As Button
End Class
