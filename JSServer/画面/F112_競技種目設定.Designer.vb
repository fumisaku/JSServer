<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F112_競技種目設定
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
        Me.DGV_競技種目 = New System.Windows.Forms.DataGridView()
        Me.LB_ラウンド名 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LB_区分番号 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LB_区分名 = New System.Windows.Forms.Label()
        Me.PB_保存 = New System.Windows.Forms.Button()
        Me.PB_戻る = New System.Windows.Forms.Button()
        Me.TB_CaliMax = New System.Windows.Forms.TextBox()
        Me.TB_CaliMin = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PB_反映 = New System.Windows.Forms.Button()
        CType(Me.DGV_競技種目, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_競技種目
        '
        Me.DGV_競技種目.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_競技種目.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGV_競技種目.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_競技種目.Location = New System.Drawing.Point(50, 114)
        Me.DGV_競技種目.Name = "DGV_競技種目"
        Me.DGV_競技種目.RowTemplate.Height = 24
        Me.DGV_競技種目.Size = New System.Drawing.Size(676, 302)
        Me.DGV_競技種目.TabIndex = 0
        '
        'LB_ラウンド名
        '
        Me.LB_ラウンド名.AutoSize = True
        Me.LB_ラウンド名.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_ラウンド名.Location = New System.Drawing.Point(151, 41)
        Me.LB_ラウンド名.Name = "LB_ラウンド名"
        Me.LB_ラウンド名.Size = New System.Drawing.Size(0, 20)
        Me.LB_ラウンド名.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(46, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "ラウンド"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(268, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 20)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "区分名"
        '
        'LB_区分番号
        '
        Me.LB_区分番号.AutoSize = True
        Me.LB_区分番号.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分番号.Location = New System.Drawing.Point(151, 9)
        Me.LB_区分番号.Name = "LB_区分番号"
        Me.LB_区分番号.Size = New System.Drawing.Size(31, 20)
        Me.LB_区分番号.TabIndex = 8
        Me.LB_区分番号.Text = "01"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(46, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 20)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "区分番号"
        '
        'LB_区分名
        '
        Me.LB_区分名.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LB_区分名.AutoSize = True
        Me.LB_区分名.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分名.Location = New System.Drawing.Point(346, 9)
        Me.LB_区分名.Name = "LB_区分名"
        Me.LB_区分名.Size = New System.Drawing.Size(31, 20)
        Me.LB_区分名.TabIndex = 8
        Me.LB_区分名.Text = "01"
        '
        'PB_保存
        '
        Me.PB_保存.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_保存.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_保存.Location = New System.Drawing.Point(50, 436)
        Me.PB_保存.Name = "PB_保存"
        Me.PB_保存.Size = New System.Drawing.Size(148, 49)
        Me.PB_保存.TabIndex = 10
        Me.PB_保存.Text = "保存"
        Me.PB_保存.UseVisualStyleBackColor = True
        '
        'PB_戻る
        '
        Me.PB_戻る.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PB_戻る.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_戻る.Location = New System.Drawing.Point(585, 436)
        Me.PB_戻る.Name = "PB_戻る"
        Me.PB_戻る.Size = New System.Drawing.Size(141, 49)
        Me.PB_戻る.TabIndex = 11
        Me.PB_戻る.Text = "戻る"
        Me.PB_戻る.UseVisualStyleBackColor = True
        '
        'TB_CaliMax
        '
        Me.TB_CaliMax.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_CaliMax.Location = New System.Drawing.Point(487, 41)
        Me.TB_CaliMax.Name = "TB_CaliMax"
        Me.TB_CaliMax.Size = New System.Drawing.Size(92, 27)
        Me.TB_CaliMax.TabIndex = 12
        Me.TB_CaliMax.Text = "7.25"
        Me.TB_CaliMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TB_CaliMin
        '
        Me.TB_CaliMin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_CaliMin.Location = New System.Drawing.Point(487, 74)
        Me.TB_CaliMin.Name = "TB_CaliMin"
        Me.TB_CaliMin.Size = New System.Drawing.Size(92, 27)
        Me.TB_CaliMin.TabIndex = 13
        Me.TB_CaliMin.Text = "5.00"
        Me.TB_CaliMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(394, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 20)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Cali最大"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(394, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 20)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Cali最小"
        '
        'PB_反映
        '
        Me.PB_反映.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_反映.Location = New System.Drawing.Point(585, 41)
        Me.PB_反映.Name = "PB_反映"
        Me.PB_反映.Size = New System.Drawing.Size(96, 56)
        Me.PB_反映.TabIndex = 15
        Me.PB_反映.Text = "反映"
        Me.PB_反映.UseVisualStyleBackColor = True
        '
        'F112_競技種目設定
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(800, 491)
        Me.Controls.Add(Me.PB_反映)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TB_CaliMin)
        Me.Controls.Add(Me.TB_CaliMax)
        Me.Controls.Add(Me.PB_戻る)
        Me.Controls.Add(Me.PB_保存)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LB_区分名)
        Me.Controls.Add(Me.LB_区分番号)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LB_ラウンド名)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DGV_競技種目)
        Me.Name = "F112_競技種目設定"
        Me.Text = "F112_競技種目設定"
        CType(Me.DGV_競技種目, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGV_競技種目 As DataGridView
    Friend WithEvents LB_ラウンド名 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents LB_区分番号 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents LB_区分名 As Label
    Friend WithEvents PB_保存 As Button
    Friend WithEvents PB_戻る As Button
    Friend WithEvents TB_CaliMax As TextBox
    Friend WithEvents TB_CaliMin As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents PB_反映 As Button
End Class
