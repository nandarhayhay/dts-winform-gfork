<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.XpLetterBox1 = New SteepValley.Windows.Forms.ThemedControls.XPLetterBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.XpLoginEntry2 = New SteepValley.Windows.Forms.XPLoginEntry
        Me.XpLinkedLabelIcon1 = New SteepValley.Windows.Forms.XPLinkedLabelIcon
        Me.XpLoginEntry1 = New SteepValley.Windows.Forms.XPLoginEntry
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.XpLetterBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'XpLetterBox1
        '
        Me.XpLetterBox1.BackColor = System.Drawing.Color.Transparent
        Me.XpLetterBox1.Controls.Add(Me.PictureBox1)
        Me.XpLetterBox1.Controls.Add(Me.TextBox1)
        Me.XpLetterBox1.Controls.Add(Me.XpLoginEntry2)
        Me.XpLetterBox1.Controls.Add(Me.XpLinkedLabelIcon1)
        Me.XpLetterBox1.Controls.Add(Me.XpLoginEntry1)
        Me.XpLetterBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XpLetterBox1.DrawVerticalSplitLine = True
        Me.XpLetterBox1.HeaderText = "Please login to Server"
        Me.XpLetterBox1.Location = New System.Drawing.Point(0, 0)
        Me.XpLetterBox1.Name = "XpLetterBox1"
        Me.XpLetterBox1.Padding = New System.Windows.Forms.Padding(0, 48, 0, 48)
        Me.XpLetterBox1.Size = New System.Drawing.Size(463, 261)
        Me.XpLetterBox1.TabIndex = 0
        Me.XpLetterBox1.Theme = SteepValley.Windows.Forms.ThemedControls.ThemeStyle.HomeStead
        Me.XpLetterBox1.ThemeFormat.BottomBodyColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(132, Byte), Integer))
        Me.XpLetterBox1.ThemeFormat.FooterColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.XpLetterBox1.ThemeFormat.HeaderColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.XpLetterBox1.ThemeFormat.HeaderTextColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.XpLetterBox1.ThemeFormat.HeaderTextFont = New System.Drawing.Font("Franklin Gothic Medium", 14.0!, System.Drawing.FontStyle.Bold)
        Me.XpLetterBox1.ThemeFormat.TopBodyColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(132, Byte), Integer))
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(286, 96)
        Me.TextBox1.MaxLength = 30
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(157, 20)
        Me.TextBox1.TabIndex = 4
        '
        'XpLoginEntry2
        '
        Me.XpLoginEntry2.BackColor = System.Drawing.Color.Transparent
        Me.XpLoginEntry2.HasPassword = False
        Me.XpLoginEntry2.Icon = CType(resources.GetObject("XpLoginEntry2.Icon"), System.Drawing.Icon)
        Me.XpLoginEntry2.Location = New System.Drawing.Point(234, 61)
        Me.XpLoginEntry2.Name = "XpLoginEntry2"
        Me.XpLoginEntry2.Size = New System.Drawing.Size(222, 60)
        Me.XpLoginEntry2.TabIndex = 3
        Me.XpLoginEntry2.UserName = "User Name"
        Me.XpLoginEntry2.Visible = False
        '
        'XpLinkedLabelIcon1
        '
        Me.XpLinkedLabelIcon1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.XpLinkedLabelIcon1.BackColor = System.Drawing.Color.Transparent
        Me.XpLinkedLabelIcon1.DisabledLinkColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.XpLinkedLabelIcon1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XpLinkedLabelIcon1.LinkArea = New System.Windows.Forms.LinkArea(0, 6)
        Me.XpLinkedLabelIcon1.LinkColor = System.Drawing.Color.DarkGreen
        Me.XpLinkedLabelIcon1.Location = New System.Drawing.Point(392, 194)
        Me.XpLinkedLabelIcon1.Name = "XpLinkedLabelIcon1"
        Me.XpLinkedLabelIcon1.Size = New System.Drawing.Size(51, 15)
        Me.XpLinkedLabelIcon1.TabIndex = 2
        Me.XpLinkedLabelIcon1.Text = "Cancel"
        Me.XpLinkedLabelIcon1.VisitedLinkColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        '
        'XpLoginEntry1
        '
        Me.XpLoginEntry1.BackColor = System.Drawing.Color.Transparent
        Me.XpLoginEntry1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XpLoginEntry1.HasPassword = True
        Me.XpLoginEntry1.Icon = CType(resources.GetObject("XpLoginEntry1.Icon"), System.Drawing.Icon)
        Me.XpLoginEntry1.Location = New System.Drawing.Point(235, 127)
        Me.XpLoginEntry1.Name = "XpLoginEntry1"
        Me.XpLoginEntry1.Size = New System.Drawing.Size(223, 60)
        Me.XpLoginEntry1.TabIndex = 1
        '
        'Timer1
        '
        Me.Timer1.Interval = 2000
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.DTSProjects.My.Resources.Resources.signin1
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(36, 61)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(116, 117)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(463, 261)
        Me.Controls.Add(Me.XpLetterBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.TopMost = True
        Me.XpLetterBox1.ResumeLayout(False)
        Me.XpLetterBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents XpLetterBox1 As SteepValley.Windows.Forms.ThemedControls.XPLetterBox
    Private WithEvents XpLoginEntry1 As SteepValley.Windows.Forms.XPLoginEntry
    Private WithEvents XpLinkedLabelIcon1 As SteepValley.Windows.Forms.XPLinkedLabelIcon
    Private WithEvents Timer1 As System.Windows.Forms.Timer
    Private WithEvents TextBox1 As System.Windows.Forms.TextBox
    Private WithEvents XpLoginEntry2 As SteepValley.Windows.Forms.XPLoginEntry
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
