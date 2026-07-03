
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F300_印刷画面
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
        Me.LB_区分名 = New System.Windows.Forms.Label()
        Me.LB_現ラウンド = New System.Windows.Forms.Label()
        Me.PN_現 = New System.Windows.Forms.Panel()
        Me.PB_現R結果印刷 = New System.Windows.Forms.Button()
        Me.PB_現Rヒート表印刷 = New System.Windows.Forms.Button()
        Me.PB_分析 = New System.Windows.Forms.Button()
        Me.PB_H3_HTML = New System.Windows.Forms.Button()
        Me.PB_F1 = New System.Windows.Forms.Button()
        Me.PB_入賞者 = New System.Windows.Forms.Button()
        Me.PB_詳細 = New System.Windows.Forms.Button()
        Me.PB_PT = New System.Windows.Forms.Button()
        Me.PB_H3 = New System.Windows.Forms.Button()
        Me.PB_H2 = New System.Windows.Forms.Button()
        Me.PB_H1 = New System.Windows.Forms.Button()
        Me.PN_次 = New System.Windows.Forms.Panel()
        Me.PB_次Rヒート表印刷 = New System.Windows.Forms.Button()
        Me.PB_H3次_HTML = New System.Windows.Forms.Button()
        Me.PB_F1次 = New System.Windows.Forms.Button()
        Me.PB_H3次 = New System.Windows.Forms.Button()
        Me.PB_H2次 = New System.Windows.Forms.Button()
        Me.PB_H1次 = New System.Windows.Forms.Button()
        Me.LB_次ラウンド = New System.Windows.Forms.Label()
        Me.PB_印刷 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PB_印刷設定 = New System.Windows.Forms.Button()
        Me.PB_SFTP = New System.Windows.Forms.Button()
        Me.PB_分析全体 = New System.Windows.Forms.Button()
        Me.PB_INIT = New System.Windows.Forms.Button()
        Me.PN_現.SuspendLayout()
        Me.PN_次.SuspendLayout()
        Me.SuspendLayout()
        '
        'LB_区分名
        '
        Me.LB_区分名.AutoSize = True
        Me.LB_区分名.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_区分名.ForeColor = System.Drawing.Color.Blue
        Me.LB_区分名.Location = New System.Drawing.Point(51, 32)
        Me.LB_区分名.Name = "LB_区分名"
        Me.LB_区分名.Size = New System.Drawing.Size(79, 23)
        Me.LB_区分名.TabIndex = 5
        Me.LB_区分名.Text = "区分名"
        '
        'LB_現ラウンド
        '
        Me.LB_現ラウンド.AutoSize = True
        Me.LB_現ラウンド.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_現ラウンド.ForeColor = System.Drawing.Color.Blue
        Me.LB_現ラウンド.Location = New System.Drawing.Point(3, 15)
        Me.LB_現ラウンド.Name = "LB_現ラウンド"
        Me.LB_現ラウンド.Size = New System.Drawing.Size(110, 23)
        Me.LB_現ラウンド.TabIndex = 6
        Me.LB_現ラウンド.Text = "現ラウンド"
        '
        'PN_現
        '
        Me.PN_現.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PN_現.Controls.Add(Me.PB_現R結果印刷)
        Me.PN_現.Controls.Add(Me.PB_現Rヒート表印刷)
        Me.PN_現.Controls.Add(Me.PB_分析)
        Me.PN_現.Controls.Add(Me.PB_H3_HTML)
        Me.PN_現.Controls.Add(Me.PB_F1)
        Me.PN_現.Controls.Add(Me.PB_入賞者)
        Me.PN_現.Controls.Add(Me.PB_詳細)
        Me.PN_現.Controls.Add(Me.PB_PT)
        Me.PN_現.Controls.Add(Me.PB_H3)
        Me.PN_現.Controls.Add(Me.PB_H2)
        Me.PN_現.Controls.Add(Me.PB_H1)
        Me.PN_現.Controls.Add(Me.LB_現ラウンド)
        Me.PN_現.Location = New System.Drawing.Point(53, 99)
        Me.PN_現.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PN_現.Name = "PN_現"
        Me.PN_現.Size = New System.Drawing.Size(899, 276)
        Me.PN_現.TabIndex = 8
        '
        'PB_現R結果印刷
        '
        Me.PB_現R結果印刷.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_現R結果印刷.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PB_現R結果印刷.Location = New System.Drawing.Point(492, 42)
        Me.PB_現R結果印刷.Margin = New System.Windows.Forms.Padding(4)
        Me.PB_現R結果印刷.Name = "PB_現R結果印刷"
        Me.PB_現R結果印刷.Size = New System.Drawing.Size(363, 52)
        Me.PB_現R結果印刷.TabIndex = 17
        Me.PB_現R結果印刷.Text = "現ラウンド  結果印刷"
        Me.PB_現R結果印刷.UseVisualStyleBackColor = True
        '
        'PB_現Rヒート表印刷
        '
        Me.PB_現Rヒート表印刷.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_現Rヒート表印刷.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PB_現Rヒート表印刷.Location = New System.Drawing.Point(39, 42)
        Me.PB_現Rヒート表印刷.Margin = New System.Windows.Forms.Padding(4)
        Me.PB_現Rヒート表印刷.Name = "PB_現Rヒート表印刷"
        Me.PB_現Rヒート表印刷.Size = New System.Drawing.Size(355, 52)
        Me.PB_現Rヒート表印刷.TabIndex = 16
        Me.PB_現Rヒート表印刷.Text = "現ラウンド ヒート表印刷"
        Me.PB_現Rヒート表印刷.UseVisualStyleBackColor = True
        '
        'PB_分析
        '
        Me.PB_分析.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_分析.Location = New System.Drawing.Point(752, 202)
        Me.PB_分析.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_分析.Name = "PB_分析"
        Me.PB_分析.Size = New System.Drawing.Size(103, 46)
        Me.PB_分析.TabIndex = 15
        Me.PB_分析.Text = "分析"
        Me.PB_分析.UseVisualStyleBackColor = True
        '
        'PB_H3_HTML
        '
        Me.PB_H3_HTML.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_H3_HTML.Location = New System.Drawing.Point(291, 192)
        Me.PB_H3_HTML.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_H3_HTML.Name = "PB_H3_HTML"
        Me.PB_H3_HTML.Size = New System.Drawing.Size(103, 56)
        Me.PB_H3_HTML.TabIndex = 14
        Me.PB_H3_HTML.Text = "Heat表" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "HTML"
        Me.PB_H3_HTML.UseVisualStyleBackColor = True
        '
        'PB_F1
        '
        Me.PB_F1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_F1.Location = New System.Drawing.Point(39, 192)
        Me.PB_F1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_F1.Name = "PB_F1"
        Me.PB_F1.Size = New System.Drawing.Size(103, 48)
        Me.PB_F1.TabIndex = 13
        Me.PB_F1.Text = "F1"
        Me.PB_F1.UseVisualStyleBackColor = True
        '
        'PB_入賞者
        '
        Me.PB_入賞者.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_入賞者.Location = New System.Drawing.Point(492, 124)
        Me.PB_入賞者.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_入賞者.Name = "PB_入賞者"
        Me.PB_入賞者.Size = New System.Drawing.Size(108, 62)
        Me.PB_入賞者.TabIndex = 12
        Me.PB_入賞者.Text = "入賞者" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "名簿"
        Me.PB_入賞者.UseVisualStyleBackColor = True
        '
        'PB_詳細
        '
        Me.PB_詳細.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_詳細.Location = New System.Drawing.Point(752, 124)
        Me.PB_詳細.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_詳細.Name = "PB_詳細"
        Me.PB_詳細.Size = New System.Drawing.Size(103, 62)
        Me.PB_詳細.TabIndex = 11
        Me.PB_詳細.Text = "詳細" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "HTML"
        Me.PB_詳細.UseVisualStyleBackColor = True
        '
        'PB_PT
        '
        Me.PB_PT.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_PT.Location = New System.Drawing.Point(623, 124)
        Me.PB_PT.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_PT.Name = "PB_PT"
        Me.PB_PT.Size = New System.Drawing.Size(103, 48)
        Me.PB_PT.TabIndex = 10
        Me.PB_PT.Text = "Point"
        Me.PB_PT.UseVisualStyleBackColor = True
        '
        'PB_H3
        '
        Me.PB_H3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_H3.Location = New System.Drawing.Point(291, 124)
        Me.PB_H3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_H3.Name = "PB_H3"
        Me.PB_H3.Size = New System.Drawing.Size(103, 48)
        Me.PB_H3.TabIndex = 9
        Me.PB_H3.Text = "H3"
        Me.PB_H3.UseVisualStyleBackColor = True
        '
        'PB_H2
        '
        Me.PB_H2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_H2.Location = New System.Drawing.Point(163, 124)
        Me.PB_H2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_H2.Name = "PB_H2"
        Me.PB_H2.Size = New System.Drawing.Size(103, 48)
        Me.PB_H2.TabIndex = 8
        Me.PB_H2.Text = "H2"
        Me.PB_H2.UseVisualStyleBackColor = True
        '
        'PB_H1
        '
        Me.PB_H1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_H1.Location = New System.Drawing.Point(39, 124)
        Me.PB_H1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_H1.Name = "PB_H1"
        Me.PB_H1.Size = New System.Drawing.Size(103, 48)
        Me.PB_H1.TabIndex = 7
        Me.PB_H1.Text = "H1"
        Me.PB_H1.UseVisualStyleBackColor = True
        '
        'PN_次
        '
        Me.PN_次.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PN_次.Controls.Add(Me.PB_次Rヒート表印刷)
        Me.PN_次.Controls.Add(Me.PB_H3次_HTML)
        Me.PN_次.Controls.Add(Me.PB_F1次)
        Me.PN_次.Controls.Add(Me.PB_H3次)
        Me.PN_次.Controls.Add(Me.PB_H2次)
        Me.PN_次.Controls.Add(Me.PB_H1次)
        Me.PN_次.Controls.Add(Me.LB_次ラウンド)
        Me.PN_次.Location = New System.Drawing.Point(59, 414)
        Me.PN_次.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PN_次.Name = "PN_次"
        Me.PN_次.Size = New System.Drawing.Size(493, 292)
        Me.PN_次.TabIndex = 9
        '
        'PB_次Rヒート表印刷
        '
        Me.PB_次Rヒート表印刷.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_次Rヒート表印刷.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PB_次Rヒート表印刷.Location = New System.Drawing.Point(51, 64)
        Me.PB_次Rヒート表印刷.Margin = New System.Windows.Forms.Padding(4)
        Me.PB_次Rヒート表印刷.Name = "PB_次Rヒート表印刷"
        Me.PB_次Rヒート表印刷.Size = New System.Drawing.Size(355, 52)
        Me.PB_次Rヒート表印刷.TabIndex = 15
        Me.PB_次Rヒート表印刷.Text = "次ラウンド ヒート表印刷"
        Me.PB_次Rヒート表印刷.UseVisualStyleBackColor = True
        '
        'PB_H3次_HTML
        '
        Me.PB_H3次_HTML.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_H3次_HTML.Location = New System.Drawing.Point(303, 211)
        Me.PB_H3次_HTML.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_H3次_HTML.Name = "PB_H3次_HTML"
        Me.PB_H3次_HTML.Size = New System.Drawing.Size(103, 56)
        Me.PB_H3次_HTML.TabIndex = 14
        Me.PB_H3次_HTML.Text = "Heat表" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "HTML"
        Me.PB_H3次_HTML.UseVisualStyleBackColor = True
        '
        'PB_F1次
        '
        Me.PB_F1次.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_F1次.Location = New System.Drawing.Point(51, 211)
        Me.PB_F1次.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_F1次.Name = "PB_F1次"
        Me.PB_F1次.Size = New System.Drawing.Size(103, 48)
        Me.PB_F1次.TabIndex = 14
        Me.PB_F1次.Text = "F1"
        Me.PB_F1次.UseVisualStyleBackColor = True
        '
        'PB_H3次
        '
        Me.PB_H3次.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_H3次.Location = New System.Drawing.Point(303, 152)
        Me.PB_H3次.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_H3次.Name = "PB_H3次"
        Me.PB_H3次.Size = New System.Drawing.Size(103, 48)
        Me.PB_H3次.TabIndex = 12
        Me.PB_H3次.Text = "H3"
        Me.PB_H3次.UseVisualStyleBackColor = True
        '
        'PB_H2次
        '
        Me.PB_H2次.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_H2次.Location = New System.Drawing.Point(175, 152)
        Me.PB_H2次.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_H2次.Name = "PB_H2次"
        Me.PB_H2次.Size = New System.Drawing.Size(103, 48)
        Me.PB_H2次.TabIndex = 11
        Me.PB_H2次.Text = "H2"
        Me.PB_H2次.UseVisualStyleBackColor = True
        '
        'PB_H1次
        '
        Me.PB_H1次.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_H1次.Location = New System.Drawing.Point(51, 152)
        Me.PB_H1次.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_H1次.Name = "PB_H1次"
        Me.PB_H1次.Size = New System.Drawing.Size(103, 48)
        Me.PB_H1次.TabIndex = 10
        Me.PB_H1次.Text = "H1"
        Me.PB_H1次.UseVisualStyleBackColor = True
        '
        'LB_次ラウンド
        '
        Me.LB_次ラウンド.AutoSize = True
        Me.LB_次ラウンド.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LB_次ラウンド.ForeColor = System.Drawing.Color.Blue
        Me.LB_次ラウンド.Location = New System.Drawing.Point(3, 18)
        Me.LB_次ラウンド.Name = "LB_次ラウンド"
        Me.LB_次ラウンド.Size = New System.Drawing.Size(110, 23)
        Me.LB_次ラウンド.TabIndex = 6
        Me.LB_次ラウンド.Text = "次ラウンド"
        '
        'PB_印刷
        '
        Me.PB_印刷.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_印刷.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.PB_印刷.Location = New System.Drawing.Point(678, 414)
        Me.PB_印刷.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_印刷.Name = "PB_印刷"
        Me.PB_印刷.Size = New System.Drawing.Size(172, 88)
        Me.PB_印刷.TabIndex = 13
        Me.PB_印刷.Text = "まとめ印刷"
        Me.PB_印刷.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Label1.Location = New System.Drawing.Point(655, 514)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(220, 40)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "現ラウンドの結果と次ラウン" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "ドのヒート表を印刷します。"
        '
        'PB_印刷設定
        '
        Me.PB_印刷設定.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_印刷設定.Location = New System.Drawing.Point(792, 36)
        Me.PB_印刷設定.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_印刷設定.Name = "PB_印刷設定"
        Me.PB_印刷設定.Size = New System.Drawing.Size(139, 48)
        Me.PB_印刷設定.TabIndex = 15
        Me.PB_印刷設定.Text = "印刷設定"
        Me.PB_印刷設定.UseVisualStyleBackColor = True
        '
        'PB_SFTP
        '
        Me.PB_SFTP.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_SFTP.Location = New System.Drawing.Point(657, 580)
        Me.PB_SFTP.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_SFTP.Name = "PB_SFTP"
        Me.PB_SFTP.Size = New System.Drawing.Size(221, 62)
        Me.PB_SFTP.TabIndex = 16
        Me.PB_SFTP.Text = "サーバーへのUP"
        Me.PB_SFTP.UseVisualStyleBackColor = True
        '
        'PB_分析全体
        '
        Me.PB_分析全体.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_分析全体.Location = New System.Drawing.Point(657, 660)
        Me.PB_分析全体.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_分析全体.Name = "PB_分析全体"
        Me.PB_分析全体.Size = New System.Drawing.Size(221, 46)
        Me.PB_分析全体.TabIndex = 17
        Me.PB_分析全体.Text = "分析全体"
        Me.PB_分析全体.UseVisualStyleBackColor = True
        '
        'PB_INIT
        '
        Me.PB_INIT.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PB_INIT.Location = New System.Drawing.Point(883, 580)
        Me.PB_INIT.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PB_INIT.Name = "PB_INIT"
        Me.PB_INIT.Size = New System.Drawing.Size(107, 62)
        Me.PB_INIT.TabIndex = 18
        Me.PB_INIT.Text = "競技会一覧初期化"
        Me.PB_INIT.UseVisualStyleBackColor = True
        '
        'F300_印刷画面
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1005, 736)
        Me.Controls.Add(Me.PB_INIT)
        Me.Controls.Add(Me.PB_分析全体)
        Me.Controls.Add(Me.PB_SFTP)
        Me.Controls.Add(Me.PB_印刷設定)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PB_印刷)
        Me.Controls.Add(Me.PN_次)
        Me.Controls.Add(Me.PN_現)
        Me.Controls.Add(Me.LB_区分名)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "F300_印刷画面"
        Me.Text = "F300_印刷画面"
        Me.PN_現.ResumeLayout(False)
        Me.PN_現.PerformLayout()
        Me.PN_次.ResumeLayout(False)
        Me.PN_次.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LB_区分名 As Label
    Friend WithEvents LB_現ラウンド As Label
    Friend WithEvents PN_現 As Panel
    Friend WithEvents PN_次 As Panel
    Friend WithEvents LB_次ラウンド As Label
    Friend WithEvents PB_詳細 As Button
    Friend WithEvents PB_PT As Button
    Friend WithEvents PB_H3 As Button
    Friend WithEvents PB_H2 As Button
    Friend WithEvents PB_H1 As Button
    Friend WithEvents PB_H3次 As Button
    Friend WithEvents PB_H2次 As Button
    Friend WithEvents PB_H1次 As Button
    Friend WithEvents PB_入賞者 As Button
    Friend WithEvents PB_印刷 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents PB_F1 As Button
    Friend WithEvents PB_F1次 As Button
    Friend WithEvents PB_印刷設定 As Button
    Friend WithEvents PB_H3_HTML As Button
    Friend WithEvents PB_H3次_HTML As Button
    Friend WithEvents PB_SFTP As Button
    Friend WithEvents PB_分析 As Button
    Friend WithEvents PB_分析全体 As Button
    Friend WithEvents PB_INIT As Button
    Friend WithEvents PB_次Rヒート表印刷 As Button
    Friend WithEvents PB_現R結果印刷 As Button
    Friend WithEvents PB_現Rヒート表印刷 As Button
End Class
