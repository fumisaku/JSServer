<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F130_選手マスタ設定
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
        Me.TC_選手マスタ = New System.Windows.Forms.TabControl()
        Me.PB_保存 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.PB_マスター追加 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TC_選手マスタ
        '
        Me.TC_選手マスタ.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TC_選手マスタ.Location = New System.Drawing.Point(40, 68)
        Me.TC_選手マスタ.Name = "TC_選手マスタ"
        Me.TC_選手マスタ.SelectedIndex = 0
        Me.TC_選手マスタ.Size = New System.Drawing.Size(1167, 659)
        Me.TC_選手マスタ.TabIndex = 0
        '
        'PB_保存
        '
        Me.PB_保存.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_保存.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_保存.Location = New System.Drawing.Point(389, 733)
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
        Me.PB_戻る.Location = New System.Drawing.Point(81, 733)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 15
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'PB_マスター追加
        '
        Me.PB_マスター追加.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_マスター追加.Location = New System.Drawing.Point(159, 20)
        Me.PB_マスター追加.Name = "PB_マスター追加"
        Me.PB_マスター追加.Size = New System.Drawing.Size(170, 42)
        Me.PB_マスター追加.TabIndex = 17
        Me.PB_マスター追加.Text = "マスター追加"
        Me.PB_マスター追加.UseVisualStyleBackColor = True
        '
        'F130_選手マスタ設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1273, 786)
        Me.Controls.Add(Me.PB_マスター追加)
        Me.Controls.Add(Me.PB_保存)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.TC_選手マスタ)
        Me.Name = "F130_選手マスタ設定"
        Me.Text = "F130_選手マスタ設定"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TC_選手マスタ As TabControl
    Friend WithEvents PB_保存 As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents PB_マスター追加 As Button
End Class
