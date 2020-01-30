<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingReportGrid
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
        Me.chkReference = New System.Windows.Forms.CheckBox
        Me.chkPODisproByBrand = New System.Windows.Forms.CheckBox
        Me.chkPODispro = New System.Windows.Forms.CheckBox
        Me.XpGradientPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.chkReference)
        Me.XpGradientPanel1.Controls.Add(Me.chkPODisproByBrand)
        Me.XpGradientPanel1.Controls.Add(Me.chkPODispro)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XpGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(646, 331)
        Me.XpGradientPanel1.TabIndex = 0
        Me.XpGradientPanel1.Watermark = Global.DTSProjects.My.Resources.Resources.Splash
        '
        'chkReference
        '
        Me.chkReference.AutoSize = True
        Me.chkReference.BackColor = System.Drawing.Color.Transparent
        Me.chkReference.Location = New System.Drawing.Point(13, 81)
        Me.chkReference.Name = "chkReference"
        Me.chkReference.Size = New System.Drawing.Size(324, 17)
        Me.chkReference.TabIndex = 4
        Me.chkReference.Text = "Refer DTS PO(RUN_NUMBER) to Reference invoice(AccPac)"
        Me.chkReference.UseVisualStyleBackColor = False
        '
        'chkPODisproByBrand
        '
        Me.chkPODisproByBrand.AutoSize = True
        Me.chkPODisproByBrand.BackColor = System.Drawing.Color.Transparent
        Me.chkPODisproByBrand.Location = New System.Drawing.Point(13, 49)
        Me.chkPODisproByBrand.Name = "chkPODisproByBrand"
        Me.chkPODisproByBrand.Size = New System.Drawing.Size(560, 17)
        Me.chkPODisproByBrand.TabIndex = 3
        Me.chkPODisproByBrand.Text = "Set Report Summary Dispro By Brand to invoice (Achievement must atleast once have" & _
            " been (re)computed before)"
        Me.chkPODisproByBrand.UseVisualStyleBackColor = False
        '
        'chkPODispro
        '
        Me.chkPODispro.AutoSize = True
        Me.chkPODispro.BackColor = System.Drawing.Color.Transparent
        Me.chkPODispro.Location = New System.Drawing.Point(13, 17)
        Me.chkPODispro.Name = "chkPODispro"
        Me.chkPODispro.Size = New System.Drawing.Size(555, 17)
        Me.chkPODispro.TabIndex = 2
        Me.chkPODispro.Text = "Set Report PO and Dispro Distributor to Invoice(Achievement must atleast once hav" & _
            "e been (re)computed before)"
        Me.chkPODispro.UseVisualStyleBackColor = False
        '
        'SettingReportGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.XpGradientPanel1)
        Me.Name = "SettingReportGrid"
        Me.Size = New System.Drawing.Size(646, 331)
        Me.XpGradientPanel1.ResumeLayout(False)
        Me.XpGradientPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents chkPODispro As System.Windows.Forms.CheckBox
    Private WithEvents chkPODisproByBrand As System.Windows.Forms.CheckBox
    Private WithEvents chkReference As System.Windows.Forms.CheckBox

End Class
