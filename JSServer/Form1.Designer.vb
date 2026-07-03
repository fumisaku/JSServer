<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.PB_進行管理 = New System.Windows.Forms.Button()
        Me.PB_StartGM = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PB_フォルダ = New System.Windows.Forms.Button()
        Me.LB_競技会名 = New System.Windows.Forms.Label()
        Me.LB_競技会NO = New System.Windows.Forms.Label()
        Me.TB_フォルダ = New System.Windows.Forms.TextBox()
        Me.PB_司会進行 = New System.Windows.Forms.Button()
        Me.PB_支援システムデータ移行 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.PB_移行 = New System.Windows.Forms.Button()
        Me.PB_進行詳細 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(885, 428)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(108, 35)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "詳細点数"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(875, 325)
        Me.Button3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(107, 55)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "印刷"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button4.Location = New System.Drawing.Point(292, 205)
        Me.Button4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(192, 52)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "区分一覧"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button5.Location = New System.Drawing.Point(291, 275)
        Me.Button5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(193, 48)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "進行設定"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button6.Location = New System.Drawing.Point(292, 381)
        Me.Button6.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(192, 52)
        Me.Button6.TabIndex = 5
        Me.Button6.Text = "選手マスタ"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button7.Location = New System.Drawing.Point(292, 138)
        Me.Button7.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(192, 52)
        Me.Button7.TabIndex = 6
        Me.Button7.Text = "競技会設定"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button8.Location = New System.Drawing.Point(292, 459)
        Me.Button8.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(192, 52)
        Me.Button8.TabIndex = 7
        Me.Button8.Text = "ジャッジマスタ"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'PB_進行管理
        '
        Me.PB_進行管理.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_進行管理.Location = New System.Drawing.Point(541, 138)
        Me.PB_進行管理.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_進行管理.Name = "PB_進行管理"
        Me.PB_進行管理.Size = New System.Drawing.Size(195, 52)
        Me.PB_進行管理.TabIndex = 8
        Me.PB_進行管理.Text = "進行管理"
        Me.PB_進行管理.UseVisualStyleBackColor = True
        '
        'PB_StartGM
        '
        Me.PB_StartGM.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_StartGM.ForeColor = System.Drawing.Color.RoyalBlue
        Me.PB_StartGM.Location = New System.Drawing.Point(41, 138)
        Me.PB_StartGM.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_StartGM.Name = "PB_StartGM"
        Me.PB_StartGM.Size = New System.Drawing.Size(172, 68)
        Me.PB_StartGM.TabIndex = 9
        Me.PB_StartGM.Text = "Start GM"
        Me.PB_StartGM.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(256, 23)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "統合競技会管理システム"
        '
        'PB_フォルダ
        '
        Me.PB_フォルダ.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_フォルダ.Location = New System.Drawing.Point(41, 68)
        Me.PB_フォルダ.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_フォルダ.Name = "PB_フォルダ"
        Me.PB_フォルダ.Size = New System.Drawing.Size(147, 51)
        Me.PB_フォルダ.TabIndex = 11
        Me.PB_フォルダ.Text = "フォルダ設定"
        Me.PB_フォルダ.UseVisualStyleBackColor = True
        '
        'LB_競技会名
        '
        Me.LB_競技会名.AutoSize = True
        Me.LB_競技会名.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_競技会名.ForeColor = System.Drawing.Color.Blue
        Me.LB_競技会名.Location = New System.Drawing.Point(413, 40)
        Me.LB_競技会名.Name = "LB_競技会名"
        Me.LB_競技会名.Size = New System.Drawing.Size(79, 23)
        Me.LB_競技会名.TabIndex = 13
        Me.LB_競技会名.Text = "Label2"
        '
        'LB_競技会NO
        '
        Me.LB_競技会NO.AutoSize = True
        Me.LB_競技会NO.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_競技会NO.ForeColor = System.Drawing.Color.Blue
        Me.LB_競技会NO.Location = New System.Drawing.Point(413, 9)
        Me.LB_競技会NO.Name = "LB_競技会NO"
        Me.LB_競技会NO.Size = New System.Drawing.Size(79, 23)
        Me.LB_競技会NO.TabIndex = 14
        Me.LB_競技会NO.Text = "Label2"
        '
        'TB_フォルダ
        '
        Me.TB_フォルダ.Font = New System.Drawing.Font("MS UI Gothic", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TB_フォルダ.Location = New System.Drawing.Point(211, 68)
        Me.TB_フォルダ.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_フォルダ.Multiline = True
        Me.TB_フォルダ.Name = "TB_フォルダ"
        Me.TB_フォルダ.ReadOnly = True
        Me.TB_フォルダ.Size = New System.Drawing.Size(772, 46)
        Me.TB_フォルダ.TabIndex = 15
        '
        'PB_司会進行
        '
        Me.PB_司会進行.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_司会進行.Location = New System.Drawing.Point(548, 428)
        Me.PB_司会進行.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_司会進行.Name = "PB_司会進行"
        Me.PB_司会進行.Size = New System.Drawing.Size(195, 52)
        Me.PB_司会進行.TabIndex = 16
        Me.PB_司会進行.Text = "司会進行"
        Me.PB_司会進行.UseVisualStyleBackColor = True
        Me.PB_司会進行.Visible = False
        '
        'PB_支援システムデータ移行
        '
        Me.PB_支援システムデータ移行.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_支援システムデータ移行.Location = New System.Drawing.Point(291, 612)
        Me.PB_支援システムデータ移行.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_支援システムデータ移行.Name = "PB_支援システムデータ移行"
        Me.PB_支援システムデータ移行.Size = New System.Drawing.Size(193, 74)
        Me.PB_支援システムデータ移行.TabIndex = 17
        Me.PB_支援システムデータ移行.Text = "支援システムデータ移行"
        Me.PB_支援システムデータ移行.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(873, 545)
        Me.Button2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(81, 32)
        Me.Button2.TabIndex = 18
        Me.Button2.Text = "HTMLヒート"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(873, 595)
        Me.Button9.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(81, 34)
        Me.Button9.TabIndex = 19
        Me.Button9.Text = "FTP"
        Me.Button9.UseVisualStyleBackColor = True
        Me.Button9.Visible = False
        '
        'PB_移行
        '
        Me.PB_移行.Location = New System.Drawing.Point(548, 612)
        Me.PB_移行.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_移行.Name = "PB_移行"
        Me.PB_移行.Size = New System.Drawing.Size(195, 46)
        Me.PB_移行.TabIndex = 20
        Me.PB_移行.Text = "旧システム移行"
        Me.PB_移行.UseVisualStyleBackColor = True
        '
        'PB_進行詳細
        '
        Me.PB_進行詳細.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_進行詳細.Location = New System.Drawing.Point(548, 275)
        Me.PB_進行詳細.Margin = New System.Windows.Forms.Padding(4)
        Me.PB_進行詳細.Name = "PB_進行詳細"
        Me.PB_進行詳細.Size = New System.Drawing.Size(188, 48)
        Me.PB_進行詳細.TabIndex = 21
        Me.PB_進行詳細.Text = "進行詳細"
        Me.PB_進行詳細.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(852, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 23)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Ver1.02.39"
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(881, 249)
        Me.Button10.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(95, 48)
        Me.Button10.TabIndex = 23
        Me.Button10.Text = "Button10"
        Me.Button10.UseVisualStyleBackColor = True
        Me.Button10.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1005, 722)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PB_進行詳細)
        Me.Controls.Add(Me.PB_移行)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.PB_支援システムデータ移行)
        Me.Controls.Add(Me.PB_司会進行)
        Me.Controls.Add(Me.TB_フォルダ)
        Me.Controls.Add(Me.LB_競技会NO)
        Me.Controls.Add(Me.LB_競技会名)
        Me.Controls.Add(Me.PB_フォルダ)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PB_StartGM)
        Me.Controls.Add(Me.PB_進行管理)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents PB_進行管理 As Button
    Friend WithEvents PB_StartGM As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents PB_フォルダ As Button
    Friend WithEvents LB_競技会名 As Label
    Friend WithEvents LB_競技会NO As Label
    Friend WithEvents TB_フォルダ As TextBox
    Friend WithEvents PB_司会進行 As Button
    Friend WithEvents PB_支援システムデータ移行 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents PB_移行 As Button
    Friend WithEvents PB_進行詳細 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Button10 As Button
End Class
