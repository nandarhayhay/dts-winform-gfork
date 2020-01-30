<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingMisccelaneous
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.chkComputeQSY = New System.Windows.Forms.CheckBox
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDeleteSMS = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.chkDeleteSMSPO = New System.Windows.Forms.CheckBox
        Me.chkReadBrandPackIDAgreement = New System.Windows.Forms.CheckBox
        Me.chkReadBrandPack = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCancelPO = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtViewSMSPO = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.chkViewCancelPO = New System.Windows.Forms.CheckBox
        Me.chkViewSMSLog = New System.Windows.Forms.CheckBox
        Me.chkQSYOA = New System.Windows.Forms.CheckBox
        Me.XpGradientPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.chkComputeQSY)
        Me.XpGradientPanel1.Controls.Add(Me.CheckBox1)
        Me.XpGradientPanel1.Controls.Add(Me.Label3)
        Me.XpGradientPanel1.Controls.Add(Me.txtDeleteSMS)
        Me.XpGradientPanel1.Controls.Add(Me.chkDeleteSMSPO)
        Me.XpGradientPanel1.Controls.Add(Me.chkReadBrandPackIDAgreement)
        Me.XpGradientPanel1.Controls.Add(Me.chkReadBrandPack)
        Me.XpGradientPanel1.Controls.Add(Me.Label2)
        Me.XpGradientPanel1.Controls.Add(Me.txtCancelPO)
        Me.XpGradientPanel1.Controls.Add(Me.Label1)
        Me.XpGradientPanel1.Controls.Add(Me.txtViewSMSPO)
        Me.XpGradientPanel1.Controls.Add(Me.chkViewCancelPO)
        Me.XpGradientPanel1.Controls.Add(Me.chkViewSMSLog)
        Me.XpGradientPanel1.Controls.Add(Me.chkQSYOA)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XpGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(651, 409)
        Me.XpGradientPanel1.StartColor = System.Drawing.SystemColors.InactiveBorder
        Me.XpGradientPanel1.TabIndex = 1
        Me.XpGradientPanel1.Watermark = Global.DTSProjects.My.Resources.Resources.Splash
        '
        'chkComputeQSY
        '
        Me.chkComputeQSY.AutoSize = True
        Me.chkComputeQSY.BackColor = System.Drawing.Color.Transparent
        Me.chkComputeQSY.Checked = True
        Me.chkComputeQSY.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkComputeQSY.Location = New System.Drawing.Point(13, 204)
        Me.chkComputeQSY.Name = "chkComputeQSY"
        Me.chkComputeQSY.Size = New System.Drawing.Size(538, 17)
        Me.chkComputeQSY.TabIndex = 15
        Me.chkComputeQSY.Text = "Compute Discount QSY for distributor to single achievement not to sharring target" & _
            " if it does not achieve target"
        Me.chkComputeQSY.UseVisualStyleBackColor = False
        Me.chkComputeQSY.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(-15, -15)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(81, 17)
        Me.CheckBox1.TabIndex = 14
        Me.CheckBox1.Text = "CheckBox1"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(311, 182)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(288, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Month(s) until today, to reduce unneeded or redundent data"
        '
        'txtDeleteSMS
        '
        Me.txtDeleteSMS.Location = New System.Drawing.Point(257, 178)
        Me.txtDeleteSMS.Name = "txtDeleteSMS"
        Me.txtDeleteSMS.Size = New System.Drawing.Size(44, 20)
        Me.txtDeleteSMS.TabIndex = 12
        Me.txtDeleteSMS.Text = "1"
        Me.txtDeleteSMS.Value = 1
        Me.txtDeleteSMS.ValueType = Janus.Windows.GridEX.NumericEditValueType.Int32
        Me.txtDeleteSMS.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'chkDeleteSMSPO
        '
        Me.chkDeleteSMSPO.AutoSize = True
        Me.chkDeleteSMSPO.BackColor = System.Drawing.Color.Transparent
        Me.chkDeleteSMSPO.Location = New System.Drawing.Point(13, 179)
        Me.chkDeleteSMSPO.Name = "chkDeleteSMSPO"
        Me.chkDeleteSMSPO.Size = New System.Drawing.Size(241, 17)
        Me.chkDeleteSMSPO.TabIndex = 11
        Me.chkDeleteSMSPO.Text = "Delete SMS Log after status being sent within"
        Me.chkDeleteSMSPO.UseVisualStyleBackColor = False
        '
        'chkReadBrandPackIDAgreement
        '
        Me.chkReadBrandPackIDAgreement.AutoSize = True
        Me.chkReadBrandPackIDAgreement.BackColor = System.Drawing.Color.Transparent
        Me.chkReadBrandPackIDAgreement.Checked = True
        Me.chkReadBrandPackIDAgreement.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkReadBrandPackIDAgreement.Location = New System.Drawing.Point(13, 146)
        Me.chkReadBrandPackIDAgreement.Name = "chkReadBrandPackIDAgreement"
        Me.chkReadBrandPackIDAgreement.Size = New System.Drawing.Size(628, 17)
        Me.chkReadBrandPackIDAgreement.TabIndex = 10
        Me.chkReadBrandPackIDAgreement.Text = "Use Brand with the same ID with Accpac in Agreement Relation Form(only applies Br" & _
            "and which status is active but not obsolete)"
        Me.chkReadBrandPackIDAgreement.UseVisualStyleBackColor = False
        '
        'chkReadBrandPack
        '
        Me.chkReadBrandPack.AutoSize = True
        Me.chkReadBrandPack.BackColor = System.Drawing.Color.Transparent
        Me.chkReadBrandPack.Location = New System.Drawing.Point(13, 113)
        Me.chkReadBrandPack.Name = "chkReadBrandPack"
        Me.chkReadBrandPack.Size = New System.Drawing.Size(383, 17)
        Me.chkReadBrandPack.TabIndex = 9
        Me.chkReadBrandPack.Text = "Only Read BrandPack with the Same ID with Accpac when Processing O A"
        Me.chkReadBrandPack.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(389, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Month(s) until today"
        '
        'txtCancelPO
        '
        Me.txtCancelPO.Location = New System.Drawing.Point(326, 80)
        Me.txtCancelPO.Name = "txtCancelPO"
        Me.txtCancelPO.Size = New System.Drawing.Size(57, 20)
        Me.txtCancelPO.TabIndex = 7
        Me.txtCancelPO.Text = "3"
        Me.txtCancelPO.Value = 3
        Me.txtCancelPO.ValueType = Janus.Windows.GridEX.NumericEditValueType.Int32
        Me.txtCancelPO.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(389, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "until to day"
        '
        'txtViewSMSPO
        '
        Me.txtViewSMSPO.Location = New System.Drawing.Point(298, 47)
        Me.txtViewSMSPO.Name = "txtViewSMSPO"
        Me.txtViewSMSPO.Size = New System.Drawing.Size(85, 20)
        Me.txtViewSMSPO.TabIndex = 5
        Me.txtViewSMSPO.Text = "14"
        Me.txtViewSMSPO.Value = 14
        Me.txtViewSMSPO.ValueType = Janus.Windows.GridEX.NumericEditValueType.Int32
        Me.txtViewSMSPO.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'chkViewCancelPO
        '
        Me.chkViewCancelPO.AutoSize = True
        Me.chkViewCancelPO.BackColor = System.Drawing.Color.Transparent
        Me.chkViewCancelPO.Checked = True
        Me.chkViewCancelPO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkViewCancelPO.Enabled = False
        Me.chkViewCancelPO.Location = New System.Drawing.Point(13, 81)
        Me.chkViewCancelPO.Name = "chkViewCancelPO"
        Me.chkViewCancelPO.Size = New System.Drawing.Size(311, 17)
        Me.chkViewCancelPO.TabIndex = 4
        Me.chkViewCancelPO.Text = "View PO distributor  in Cancel PO Form, with PO Date within "
        Me.chkViewCancelPO.UseVisualStyleBackColor = False
        '
        'chkViewSMSLog
        '
        Me.chkViewSMSLog.AutoSize = True
        Me.chkViewSMSLog.BackColor = System.Drawing.Color.Transparent
        Me.chkViewSMSLog.Checked = True
        Me.chkViewSMSLog.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkViewSMSLog.Enabled = False
        Me.chkViewSMSLog.Location = New System.Drawing.Point(13, 49)
        Me.chkViewSMSLog.Name = "chkViewSMSLog"
        Me.chkViewSMSLog.Size = New System.Drawing.Size(267, 17)
        Me.chkViewSMSLog.TabIndex = 3
        Me.chkViewSMSLog.Text = "SMS can be sent to distributor with PO Date within "
        Me.chkViewSMSLog.UseVisualStyleBackColor = False
        '
        'chkQSYOA
        '
        Me.chkQSYOA.AutoSize = True
        Me.chkQSYOA.BackColor = System.Drawing.Color.Transparent
        Me.chkQSYOA.Location = New System.Drawing.Point(13, 17)
        Me.chkQSYOA.Name = "chkQSYOA"
        Me.chkQSYOA.Size = New System.Drawing.Size(632, 17)
        Me.chkQSYOA.TabIndex = 2
        Me.chkQSYOA.Text = "Search Agreement discount QSY from Achievement form, after being generated(BrandP" & _
            "ackID DTS = BrandPackCode AccPac)  "
        Me.chkQSYOA.UseVisualStyleBackColor = False
        '
        'SettingMisccelaneous
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.XpGradientPanel1)
        Me.Name = "SettingMisccelaneous"
        Me.Size = New System.Drawing.Size(651, 409)
        Me.XpGradientPanel1.ResumeLayout(False)
        Me.XpGradientPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents chkViewSMSLog As System.Windows.Forms.CheckBox
    Private WithEvents chkQSYOA As System.Windows.Forms.CheckBox
    Private WithEvents chkViewCancelPO As System.Windows.Forms.CheckBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents chkReadBrandPack As System.Windows.Forms.CheckBox
    Private WithEvents chkReadBrandPackIDAgreement As System.Windows.Forms.CheckBox
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents txtDeleteSMS As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents chkDeleteSMSPO As System.Windows.Forms.CheckBox
    Private WithEvents txtCancelPO As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents txtViewSMSPO As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Private WithEvents chkComputeQSY As System.Windows.Forms.CheckBox

End Class
