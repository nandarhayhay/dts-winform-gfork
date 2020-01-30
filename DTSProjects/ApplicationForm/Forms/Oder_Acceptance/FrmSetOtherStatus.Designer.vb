<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSetOtherStatus
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
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSetOtherStatus))
        Me.lnkCancel = New System.Windows.Forms.LinkLabel
        Me.lnkOK = New System.Windows.Forms.LinkLabel
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.lblResult = New System.Windows.Forms.Label
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lnkCancel
        '
        Me.lnkCancel.AutoSize = True
        Me.lnkCancel.BackColor = System.Drawing.Color.Transparent
        Me.lnkCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkCancel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.lnkCancel.Location = New System.Drawing.Point(263, 166)
        Me.lnkCancel.Name = "lnkCancel"
        Me.lnkCancel.Size = New System.Drawing.Size(40, 13)
        Me.lnkCancel.TabIndex = 8
        Me.lnkCancel.TabStop = True
        Me.lnkCancel.Text = "Cancel"
        '
        'lnkOK
        '
        Me.lnkOK.AutoSize = True
        Me.lnkOK.BackColor = System.Drawing.Color.Transparent
        Me.lnkOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkOK.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.lnkOK.Location = New System.Drawing.Point(235, 166)
        Me.lnkOK.Name = "lnkOK"
        Me.lnkOK.Size = New System.Drawing.Size(22, 13)
        Me.lnkOK.TabIndex = 7
        Me.lnkOK.TabStop = True
        Me.lnkOK.Text = "OK"
        '
        'GridEX1
        '
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(16, 41)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(287, 112)
        Me.GridEX1.TabIndex = 12
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblResult
        '
        Me.lblResult.BackColor = System.Drawing.Color.Transparent
        Me.lblResult.ForeColor = System.Drawing.Color.Red
        Me.lblResult.Location = New System.Drawing.Point(16, 162)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(213, 19)
        Me.lblResult.TabIndex = 13
        '
        'FrmSetOtherStatus
        '
        Me.AcceptButton = Me.lnkOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.BackColor2 = System.Drawing.SystemColors.MenuBar
        Me.CancelButton = Me.lnkCancel
        Me.ClientSize = New System.Drawing.Size(315, 190)
        Me.Controls.Add(Me.lblResult)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.lnkCancel)
        Me.Controls.Add(Me.lnkOK)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "FrmSetOtherStatus"
        Me.ShowCloseButton = False
        Me.ShowIcon = False
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lnkCancel As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkOK As System.Windows.Forms.LinkLabel
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblResult As System.Windows.Forms.Label
End Class
