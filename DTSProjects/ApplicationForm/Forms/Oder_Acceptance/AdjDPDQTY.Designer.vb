<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdjDPDQTY
    Inherits System.Windows.Forms.Form

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
        Me.txtQuantity = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lnkCancel = New System.Windows.Forms.LinkLabel
        Me.lnkOK = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'txtQuantity
        '
        Me.txtQuantity.DecimalDigits = 3
        Me.txtQuantity.Location = New System.Drawing.Point(73, 6)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(101, 20)
        Me.txtQuantity.TabIndex = 3
        Me.txtQuantity.Text = "0.000"
        Me.txtQuantity.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtQuantity.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.SystemColors.InfoText
        Me.Label1.Location = New System.Drawing.Point(2, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Set Quantity"
        '
        'lnkCancel
        '
        Me.lnkCancel.BackColor = System.Drawing.Color.Transparent
        Me.lnkCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lnkCancel.ImageIndex = 0
        Me.lnkCancel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkCancel.Location = New System.Drawing.Point(74, 32)
        Me.lnkCancel.Name = "lnkCancel"
        Me.lnkCancel.Size = New System.Drawing.Size(45, 18)
        Me.lnkCancel.TabIndex = 5
        Me.lnkCancel.TabStop = True
        Me.lnkCancel.Text = "Cancel"
        Me.lnkCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lnkOK
        '
        Me.lnkOK.BackColor = System.Drawing.Color.Transparent
        Me.lnkOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkOK.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkOK.Location = New System.Drawing.Point(139, 33)
        Me.lnkOK.Name = "lnkOK"
        Me.lnkOK.Size = New System.Drawing.Size(35, 17)
        Me.lnkOK.TabIndex = 4
        Me.lnkOK.TabStop = True
        Me.lnkOK.Text = "OK"
        Me.lnkOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AdjDPDQTY
        '
        Me.AcceptButton = Me.lnkOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.lnkCancel
        Me.ClientSize = New System.Drawing.Size(185, 59)
        Me.Controls.Add(Me.lnkCancel)
        Me.Controls.Add(Me.lnkOK)
        Me.Controls.Add(Me.txtQuantity)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AdjDPDQTY"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Adjusment DPD"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtQuantity As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lnkCancel As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkOK As System.Windows.Forms.LinkLabel
End Class
