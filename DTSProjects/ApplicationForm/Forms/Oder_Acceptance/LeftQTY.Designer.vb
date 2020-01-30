<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LeftQTY
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
        Me.lblQTY = New System.Windows.Forms.Label
        Me.txtQty = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.lblUnit = New System.Windows.Forms.Label
        Me.lblRemainding = New System.Windows.Forms.Label
        Me.lnkOK = New System.Windows.Forms.LinkLabel
        Me.lnkCancel = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'lblQTY
        '
        Me.lblQTY.AutoSize = True
        Me.lblQTY.BackColor = System.Drawing.Color.Transparent
        Me.lblQTY.Location = New System.Drawing.Point(4, 43)
        Me.lblQTY.Name = "lblQTY"
        Me.lblQTY.Size = New System.Drawing.Size(46, 13)
        Me.lblQTY.TabIndex = 0
        Me.lblQTY.Text = "Quantity"
        '
        'txtQty
        '
        Me.txtQty.FormatString = "#,##0.000"
        Me.txtQty.Location = New System.Drawing.Point(55, 40)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(100, 20)
        Me.txtQty.TabIndex = 1
        Me.txtQty.Text = "0,000"
        Me.txtQty.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtQty.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblUnit
        '
        Me.lblUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblUnit.Location = New System.Drawing.Point(161, 43)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(114, 15)
        Me.lblUnit.TabIndex = 2
        '
        'lblRemainding
        '
        Me.lblRemainding.BackColor = System.Drawing.Color.Transparent
        Me.lblRemainding.Location = New System.Drawing.Point(7, 63)
        Me.lblRemainding.Name = "lblRemainding"
        Me.lblRemainding.Size = New System.Drawing.Size(268, 20)
        Me.lblRemainding.TabIndex = 3
        '
        'lnkOK
        '
        Me.lnkOK.AutoSize = True
        Me.lnkOK.BackColor = System.Drawing.Color.Transparent
        Me.lnkOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkOK.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkOK.Location = New System.Drawing.Point(198, 92)
        Me.lnkOK.Name = "lnkOK"
        Me.lnkOK.Size = New System.Drawing.Size(22, 13)
        Me.lnkOK.TabIndex = 4
        Me.lnkOK.TabStop = True
        Me.lnkOK.Text = "OK"
        '
        'lnkCancel
        '
        Me.lnkCancel.AutoSize = True
        Me.lnkCancel.BackColor = System.Drawing.Color.Transparent
        Me.lnkCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkCancel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkCancel.Location = New System.Drawing.Point(226, 92)
        Me.lnkCancel.Name = "lnkCancel"
        Me.lnkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lnkCancel.TabIndex = 5
        Me.lnkCancel.TabStop = True
        Me.lnkCancel.Text = "Cancel"
        '
        'LeftQTY
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Salmon
        Me.BackColor2 = System.Drawing.Color.SandyBrown
        Me.ClientSize = New System.Drawing.Size(278, 112)
        Me.Controls.Add(Me.lnkCancel)
        Me.Controls.Add(Me.lnkOK)
        Me.Controls.Add(Me.lblRemainding)
        Me.Controls.Add(Me.lblUnit)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.lblQTY)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "LeftQTY"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents lblQTY As System.Windows.Forms.Label
    Friend WithEvents txtQty As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents lblRemainding As System.Windows.Forms.Label
    Friend WithEvents lnkOK As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkCancel As System.Windows.Forms.LinkLabel
End Class
