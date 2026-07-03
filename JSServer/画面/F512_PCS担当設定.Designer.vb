<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F512_PCS担当設定
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
        Me.DGV_PCS = New System.Windows.Forms.DataGridView()
        Me.PB_確定 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.PB_再ヒート割り = New System.Windows.Forms.Button()
        Me.LB_採点方式 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LB_区分名 = New System.Windows.Forms.Label()
        Me.CB_まとめ印刷 = New System.Windows.Forms.CheckBox()
        Me.CB_インターネット = New System.Windows.Forms.CheckBox()
        CType(Me.DGV_PCS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_PCS
        '
        Me.DGV_PCS.AllowUserToAddRows = False
        Me.DGV_PCS.AllowUserToDeleteRows = False
        Me.DGV_PCS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_PCS.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_PCS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_PCS.Location = New System.Drawing.Point(71, 117)
        Me.DGV_PCS.Name = "DGV_PCS"
        Me.DGV_PCS.RowHeadersVisible = False
        Me.DGV_PCS.RowHeadersWidth = 51
        Me.DGV_PCS.RowTemplate.Height = 24
        Me.DGV_PCS.Size = New System.Drawing.Size(860, 453)
        Me.DGV_PCS.TabIndex = 0
        '
        'PB_確定
        '
        Me.PB_確定.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_確定.Font = New System.Drawing.Font("MS UI Gothic", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_確定.ForeColor = System.Drawing.Color.Blue
        Me.PB_確定.Location = New System.Drawing.Point(737, 602)
        Me.PB_確定.Name = "PB_確定"
        Me.PB_確定.Size = New System.Drawing.Size(194, 92)
        Me.PB_確定.TabIndex = 26
        Me.PB_確定.Text = "確定"
        Me.PB_確定.UseVisualStyleBackColor = True
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(71, 619)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 25
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'PB_再ヒート割り
        '
        Me.PB_再ヒート割り.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PB_再ヒート割り.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_再ヒート割り.Location = New System.Drawing.Point(776, 55)
        Me.PB_再ヒート割り.Name = "PB_再ヒート割り"
        Me.PB_再ヒート割り.Size = New System.Drawing.Size(155, 44)
        Me.PB_再ヒート割り.TabIndex = 24
        Me.PB_再ヒート割り.Text = "再シャッフル"
        Me.PB_再ヒート割り.UseVisualStyleBackColor = True
        '
        'LB_採点方式
        '
        Me.LB_採点方式.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_採点方式.AutoSize = True
        Me.LB_採点方式.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_採点方式.ForeColor = System.Drawing.Color.Blue
        Me.LB_採点方式.Location = New System.Drawing.Point(212, 75)
        Me.LB_採点方式.Name = "LB_採点方式"
        Me.LB_採点方式.Size = New System.Drawing.Size(65, 23)
        Me.LB_採点方式.TabIndex = 29
        Me.LB_採点方式.Text = "xxxxx"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(78, 75)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 23)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "採点方式"
        '
        'LB_区分名
        '
        Me.LB_区分名.AutoSize = True
        Me.LB_区分名.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分名.ForeColor = System.Drawing.Color.Blue
        Me.LB_区分名.Location = New System.Drawing.Point(78, 25)
        Me.LB_区分名.Name = "LB_区分名"
        Me.LB_区分名.Size = New System.Drawing.Size(79, 23)
        Me.LB_区分名.TabIndex = 27
        Me.LB_区分名.Text = "区分名"
        '
        'CB_まとめ印刷
        '
        Me.CB_まとめ印刷.AutoSize = True
        Me.CB_まとめ印刷.Checked = True
        Me.CB_まとめ印刷.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CB_まとめ印刷.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CB_まとめ印刷.Location = New System.Drawing.Point(517, 602)
        Me.CB_まとめ印刷.Name = "CB_まとめ印刷"
        Me.CB_まとめ印刷.Size = New System.Drawing.Size(128, 27)
        Me.CB_まとめ印刷.TabIndex = 30
        Me.CB_まとめ印刷.Text = "まとめ印刷"
        Me.CB_まとめ印刷.UseVisualStyleBackColor = True
        '
        'CB_インターネット
        '
        Me.CB_インターネット.AutoSize = True
        Me.CB_インターネット.Checked = True
        Me.CB_インターネット.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CB_インターネット.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CB_インターネット.Location = New System.Drawing.Point(517, 650)
        Me.CB_インターネット.Name = "CB_インターネット"
        Me.CB_インターネット.Size = New System.Drawing.Size(195, 27)
        Me.CB_インターネット.TabIndex = 31
        Me.CB_インターネット.Text = "インターネット公開"
        Me.CB_インターネット.UseVisualStyleBackColor = True
        '
        'F512_PCS担当設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1006, 723)
        Me.Controls.Add(Me.CB_インターネット)
        Me.Controls.Add(Me.CB_まとめ印刷)
        Me.Controls.Add(Me.LB_採点方式)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LB_区分名)
        Me.Controls.Add(Me.PB_確定)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.PB_再ヒート割り)
        Me.Controls.Add(Me.DGV_PCS)
        Me.Name = "F512_PCS担当設定"
        Me.Text = "F512_PCS担当設定"
        CType(Me.DGV_PCS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGV_PCS As DataGridView
    Friend WithEvents PB_確定 As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents PB_再ヒート割り As Button
    Friend WithEvents LB_採点方式 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents LB_区分名 As Label
    Friend WithEvents CB_まとめ印刷 As CheckBox
    Friend WithEvents CB_インターネット As CheckBox
End Class
