<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditQTY
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
        Me.txtQTY = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.lnkOK = New System.Windows.Forms.LinkLabel
        Me.lnkCancel = New System.Windows.Forms.LinkLabel
        Me.lblResult = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Quantity"
        '
        'txtQTY
        '
        Me.txtQTY.DecimalDigits = 3
        Me.txtQTY.Location = New System.Drawing.Point(64, 42)
        Me.txtQTY.Name = "txtQTY"
        Me.txtQTY.Size = New System.Drawing.Size(123, 20)
        Me.txtQTY.TabIndex = 1
        Me.txtQTY.Text = "0,000"
        Me.txtQTY.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtQTY.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lnkOK
        '
        Me.lnkOK.AutoSize = True
        Me.lnkOK.BackColor = System.Drawing.Color.Transparent
        Me.lnkOK.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.lnkOK.Location = New System.Drawing.Point(113, 95)
        Me.lnkOK.Name = "lnkOK"
        Me.lnkOK.Size = New System.Drawing.Size(22, 13)
        Me.lnkOK.TabIndex = 2
        Me.lnkOK.TabStop = True
        Me.lnkOK.Text = "OK"
        '
        'lnkCancel
        '
        Me.lnkCancel.AutoSize = True
        Me.lnkCancel.BackColor = System.Drawing.Color.Transparent
        Me.lnkCancel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.lnkCancel.Location = New System.Drawing.Point(141, 95)
        Me.lnkCancel.Name = "lnkCancel"
        Me.lnkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lnkCancel.TabIndex = 3
        Me.lnkCancel.TabStop = True
        Me.lnkCancel.Text = "Cancel"
        '
        'lblResult
        '
        Me.lblResult.BackColor = System.Drawing.Color.Transparent
        Me.lblResult.Location = New System.Drawing.Point(15, 65)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(172, 19)
        Me.lblResult.TabIndex = 4
        '
        'EditQTY
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Salmon
        Me.BackColor2 = System.Drawing.Color.SandyBrown
        Me.ClientSize = New System.Drawing.Size(193, 117)
        Me.Controls.Add(Me.lblResult)
        Me.Controls.Add(Me.lnkCancel)
        Me.Controls.Add(Me.lnkOK)
        Me.Controls.Add(Me.txtQTY)
        Me.Controls.Add(Me.Label1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "EditQTY"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtQTY As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents lnkOK As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkCancel As System.Windows.Forms.LinkLabel
    Friend WithEvents lblResult As System.Windows.Forms.Label
End Class
