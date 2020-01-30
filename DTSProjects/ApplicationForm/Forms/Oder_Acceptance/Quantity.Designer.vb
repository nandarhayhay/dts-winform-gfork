<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Quantity
    Inherits DevComponents.DotNetBar.Balloon

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.NumericEditBox1 = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.lnkOK = New System.Windows.Forms.LinkLabel
        Me.lnkCancel = New System.Windows.Forms.LinkLabel
        Me.lblCanBeRealesed = New System.Windows.Forms.Label
        Me.lblResultRealesed = New System.Windows.Forms.Label
        Me.txtAdjust1 = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.chkSetAdjustment = New System.Windows.Forms.CheckBox
        Me.grpAdjustment = New System.Windows.Forms.GroupBox
        Me.lblAdjustment2 = New System.Windows.Forms.Label
        Me.lblAdjustment1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtAdjust2 = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.grpAdjustment.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.SystemColors.InfoText
        Me.Label1.Location = New System.Drawing.Point(11, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Set Quantity"
        '
        'NumericEditBox1
        '
        Me.NumericEditBox1.DecimalDigits = 3
        Me.NumericEditBox1.Location = New System.Drawing.Point(115, 49)
        Me.NumericEditBox1.Name = "NumericEditBox1"
        Me.NumericEditBox1.Size = New System.Drawing.Size(101, 20)
        Me.NumericEditBox1.TabIndex = 1
        Me.NumericEditBox1.Text = "0.000"
        Me.NumericEditBox1.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.NumericEditBox1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lnkOK
        '
        Me.lnkOK.BackColor = System.Drawing.Color.Transparent
        Me.lnkOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkOK.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkOK.Location = New System.Drawing.Point(272, 187)
        Me.lnkOK.Name = "lnkOK"
        Me.lnkOK.Size = New System.Drawing.Size(35, 17)
        Me.lnkOK.TabIndex = 2
        Me.lnkOK.TabStop = True
        Me.lnkOK.Text = "OK"
        Me.lnkOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lnkCancel
        '
        Me.lnkCancel.BackColor = System.Drawing.Color.Transparent
        Me.lnkCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lnkCancel.ImageIndex = 0
        Me.lnkCancel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkCancel.Location = New System.Drawing.Point(207, 186)
        Me.lnkCancel.Name = "lnkCancel"
        Me.lnkCancel.Size = New System.Drawing.Size(45, 18)
        Me.lnkCancel.TabIndex = 3
        Me.lnkCancel.TabStop = True
        Me.lnkCancel.Text = "Cancel"
        Me.lnkCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCanBeRealesed
        '
        Me.lblCanBeRealesed.BackColor = System.Drawing.Color.Transparent
        Me.lblCanBeRealesed.Location = New System.Drawing.Point(14, 72)
        Me.lblCanBeRealesed.Name = "lblCanBeRealesed"
        Me.lblCanBeRealesed.Size = New System.Drawing.Size(186, 18)
        Me.lblCanBeRealesed.TabIndex = 4
        '
        'lblResultRealesed
        '
        Me.lblResultRealesed.BackColor = System.Drawing.Color.Transparent
        Me.lblResultRealesed.Location = New System.Drawing.Point(12, 94)
        Me.lblResultRealesed.Name = "lblResultRealesed"
        Me.lblResultRealesed.Size = New System.Drawing.Size(188, 20)
        Me.lblResultRealesed.TabIndex = 5
        '
        'txtAdjust1
        '
        Me.txtAdjust1.DecimalDigits = 3
        Me.txtAdjust1.Location = New System.Drawing.Point(90, 15)
        Me.txtAdjust1.Name = "txtAdjust1"
        Me.txtAdjust1.Size = New System.Drawing.Size(95, 20)
        Me.txtAdjust1.TabIndex = 6
        Me.txtAdjust1.Text = "0.000"
        Me.txtAdjust1.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtAdjust1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'chkSetAdjustment
        '
        Me.chkSetAdjustment.AutoSize = True
        Me.chkSetAdjustment.BackColor = System.Drawing.Color.Transparent
        Me.chkSetAdjustment.Location = New System.Drawing.Point(12, 90)
        Me.chkSetAdjustment.Name = "chkSetAdjustment"
        Me.chkSetAdjustment.Size = New System.Drawing.Size(97, 17)
        Me.chkSetAdjustment.TabIndex = 7
        Me.chkSetAdjustment.Text = "Set Adjustment"
        Me.chkSetAdjustment.UseVisualStyleBackColor = False
        '
        'grpAdjustment
        '
        Me.grpAdjustment.BackColor = System.Drawing.Color.Transparent
        Me.grpAdjustment.Controls.Add(Me.lblAdjustment2)
        Me.grpAdjustment.Controls.Add(Me.lblAdjustment1)
        Me.grpAdjustment.Controls.Add(Me.Label3)
        Me.grpAdjustment.Controls.Add(Me.Label2)
        Me.grpAdjustment.Controls.Add(Me.txtAdjust1)
        Me.grpAdjustment.Controls.Add(Me.txtAdjust2)
        Me.grpAdjustment.Location = New System.Drawing.Point(115, 75)
        Me.grpAdjustment.Name = "grpAdjustment"
        Me.grpAdjustment.Size = New System.Drawing.Size(192, 105)
        Me.grpAdjustment.TabIndex = 8
        Me.grpAdjustment.TabStop = False
        Me.grpAdjustment.Text = "Adjust Qty"
        '
        'lblAdjustment2
        '
        Me.lblAdjustment2.BackColor = System.Drawing.Color.Transparent
        Me.lblAdjustment2.ForeColor = System.Drawing.SystemColors.InfoText
        Me.lblAdjustment2.Location = New System.Drawing.Point(91, 81)
        Me.lblAdjustment2.Name = "lblAdjustment2"
        Me.lblAdjustment2.Size = New System.Drawing.Size(92, 13)
        Me.lblAdjustment2.TabIndex = 13
        '
        'lblAdjustment1
        '
        Me.lblAdjustment1.BackColor = System.Drawing.Color.Transparent
        Me.lblAdjustment1.ForeColor = System.Drawing.SystemColors.InfoText
        Me.lblAdjustment1.Location = New System.Drawing.Point(93, 38)
        Me.lblAdjustment1.Name = "lblAdjustment1"
        Me.lblAdjustment1.Size = New System.Drawing.Size(92, 13)
        Me.lblAdjustment1.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.SystemColors.InfoText
        Me.Label3.Location = New System.Drawing.Point(8, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Adjust 2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.SystemColors.InfoText
        Me.Label2.Location = New System.Drawing.Point(9, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Adjust 1"
        '
        'txtAdjust2
        '
        Me.txtAdjust2.DecimalDigits = 3
        Me.txtAdjust2.Location = New System.Drawing.Point(90, 54)
        Me.txtAdjust2.Name = "txtAdjust2"
        Me.txtAdjust2.Size = New System.Drawing.Size(95, 20)
        Me.txtAdjust2.TabIndex = 10
        Me.txtAdjust2.Text = "0.000"
        Me.txtAdjust2.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtAdjust2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Quantity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Salmon
        Me.BackColor2 = System.Drawing.Color.SandyBrown
        Me.ClientSize = New System.Drawing.Size(320, 220)
        Me.Controls.Add(Me.grpAdjustment)
        Me.Controls.Add(Me.chkSetAdjustment)
        Me.Controls.Add(Me.lblResultRealesed)
        Me.Controls.Add(Me.lblCanBeRealesed)
        Me.Controls.Add(Me.lnkCancel)
        Me.Controls.Add(Me.lnkOK)
        Me.Controls.Add(Me.NumericEditBox1)
        Me.Controls.Add(Me.Label1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "Quantity"
        Me.grpAdjustment.ResumeLayout(False)
        Me.grpAdjustment.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NumericEditBox1 As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lnkOK As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkCancel As System.Windows.Forms.LinkLabel
    Friend WithEvents lblCanBeRealesed As System.Windows.Forms.Label
    Friend WithEvents lblResultRealesed As System.Windows.Forms.Label
    Friend WithEvents txtAdjust1 As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents grpAdjustment As System.Windows.Forms.GroupBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAdjust2 As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents lblAdjustment2 As System.Windows.Forms.Label
    Friend WithEvents lblAdjustment1 As System.Windows.Forms.Label
    Friend WithEvents chkSetAdjustment As System.Windows.Forms.CheckBox
End Class
