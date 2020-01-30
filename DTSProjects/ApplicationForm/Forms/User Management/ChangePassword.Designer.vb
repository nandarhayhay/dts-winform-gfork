<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangePassword
    Inherits DTSProjects.BaseForm

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangePassword))
        Me.EditBox2 = New Janus.Windows.GridEX.EditControls.EditBox
        Me.btnCancel = New Janus.Windows.EditControls.UIButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.XpLoginEntry1 = New SteepValley.Windows.Forms.XPLoginEntry
        Me.XpLoginEntry2 = New SteepValley.Windows.Forms.XPLoginEntry
        Me.lblInfo = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'EditBox2
        '
        Me.EditBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditBox2.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.EditBox2.Location = New System.Drawing.Point(68, 42)
        Me.EditBox2.MaxLength = 30
        Me.EditBox2.Name = "EditBox2"
        Me.EditBox2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.EditBox2.Size = New System.Drawing.Size(199, 23)
        Me.EditBox2.TabIndex = 3
        Me.EditBox2.Visible = False
        Me.EditBox2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnCancel
        '
        Me.btnCancel.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnCancel.ImageIndex = 1
        Me.btnCancel.ImageList = Me.ImageList1
        Me.btnCancel.Location = New System.Drawing.Point(192, 149)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(1, "Cancel.ico")
        '
        'XpLoginEntry1
        '
        Me.XpLoginEntry1.BackColor = System.Drawing.Color.Transparent
        Me.XpLoginEntry1.HasPassword = False
        Me.XpLoginEntry1.Icon = CType(resources.GetObject("XpLoginEntry1.Icon"), System.Drawing.Icon)
        Me.XpLoginEntry1.Location = New System.Drawing.Point(12, 6)
        Me.XpLoginEntry1.Name = "XpLoginEntry1"
        Me.XpLoginEntry1.Size = New System.Drawing.Size(268, 60)
        Me.XpLoginEntry1.TabIndex = 6
        Me.XpLoginEntry1.UserName = "New Password"
        '
        'XpLoginEntry2
        '
        Me.XpLoginEntry2.BackColor = System.Drawing.Color.Transparent
        Me.XpLoginEntry2.HasPassword = True
        Me.XpLoginEntry2.Icon = CType(resources.GetObject("XpLoginEntry2.Icon"), System.Drawing.Icon)
        Me.XpLoginEntry2.Location = New System.Drawing.Point(12, 73)
        Me.XpLoginEntry2.Name = "XpLoginEntry2"
        Me.XpLoginEntry2.Size = New System.Drawing.Size(268, 60)
        Me.XpLoginEntry2.TabIndex = 7
        Me.XpLoginEntry2.UserName = "Confirm Password"
        '
        'lblInfo
        '
        Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblInfo.Location = New System.Drawing.Point(0, 188)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(292, 23)
        Me.lblInfo.TabIndex = 8
        '
        'ChangePassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(292, 211)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.XpLoginEntry2)
        Me.Controls.Add(Me.EditBox2)
        Me.Controls.Add(Me.XpLoginEntry1)
        Me.Controls.Add(Me.btnCancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ChangePassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change your own password"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents EditBox2 As Janus.Windows.GridEX.EditControls.EditBox
    Private WithEvents btnCancel As Janus.Windows.EditControls.UIButton
    Private WithEvents XpLoginEntry1 As SteepValley.Windows.Forms.XPLoginEntry
    Private WithEvents XpLoginEntry2 As SteepValley.Windows.Forms.XPLoginEntry
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents lblInfo As System.Windows.Forms.Label

End Class
