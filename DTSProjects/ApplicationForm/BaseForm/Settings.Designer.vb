<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Settings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Settings))
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.btnReportgrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnMiscellaneous = New DevComponents.DotNetBar.ButtonItem
        Me.XpGradientPanel2 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.btnCancel = New Janus.Windows.EditControls.UIButton
        Me.btnOK = New Janus.Windows.EditControls.UIButton
        Me.XpTaskBox1 = New SteepValley.Windows.Forms.ThemedControls.XPTaskBox
        Me.btnPercDPDToValue = New DevComponents.DotNetBar.ButtonItem
        Me.XpGradientPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ItemPanel1
        '
        '
        '
        '
        Me.ItemPanel1.BackgroundStyle.BackColor = System.Drawing.Color.White
        Me.ItemPanel1.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderBottomWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.ItemPanel1.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderLeftWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderRightWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderTopWidth = 1
        Me.ItemPanel1.BackgroundStyle.PaddingBottom = 1
        Me.ItemPanel1.BackgroundStyle.PaddingLeft = 1
        Me.ItemPanel1.BackgroundStyle.PaddingRight = 1
        Me.ItemPanel1.BackgroundStyle.PaddingTop = 1
        Me.ItemPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ItemPanel1.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnReportgrid, Me.btnMiscellaneous, Me.btnPercDPDToValue})
        Me.ItemPanel1.ItemSpacing = 3
        Me.ItemPanel1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical
        Me.ItemPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(111, 492)
        Me.ItemPanel1.TabIndex = 0
        Me.ItemPanel1.Text = "ItemPanel1"
        '
        'btnReportgrid
        '
        Me.btnReportgrid.BeginGroup = True
        Me.btnReportgrid.Name = "btnReportgrid"
        Me.btnReportgrid.Text = "Report Grid"
        '
        'btnMiscellaneous
        '
        Me.btnMiscellaneous.Name = "btnMiscellaneous"
        Me.btnMiscellaneous.Text = "Miscellaneous"
        '
        'XpGradientPanel2
        '
        Me.XpGradientPanel2.Controls.Add(Me.btnCancel)
        Me.XpGradientPanel2.Controls.Add(Me.btnOK)
        Me.XpGradientPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.XpGradientPanel2.Location = New System.Drawing.Point(111, 459)
        Me.XpGradientPanel2.Name = "XpGradientPanel2"
        Me.XpGradientPanel2.Size = New System.Drawing.Size(653, 33)
        Me.XpGradientPanel2.TabIndex = 20
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(575, 7)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(495, 7)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&OK"
        Me.btnOK.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'XpTaskBox1
        '
        Me.XpTaskBox1.BackColor = System.Drawing.Color.Transparent
        Me.XpTaskBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XpTaskBox1.HeaderText = ""
        Me.XpTaskBox1.Location = New System.Drawing.Point(111, 0)
        Me.XpTaskBox1.Name = "XpTaskBox1"
        Me.XpTaskBox1.Padding = New System.Windows.Forms.Padding(4, 44, 4, 4)
        Me.XpTaskBox1.Size = New System.Drawing.Size(653, 459)
        Me.XpTaskBox1.TabIndex = 21
        Me.XpTaskBox1.ThemeFormat.BodyColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.XpTaskBox1.ThemeFormat.BodyFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XpTaskBox1.ThemeFormat.BodyTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.XpTaskBox1.ThemeFormat.BorderColor = System.Drawing.Color.White
        Me.XpTaskBox1.ThemeFormat.ChevronDown = CType(resources.GetObject("resource.ChevronDown"), System.Drawing.Bitmap)
        Me.XpTaskBox1.ThemeFormat.ChevronDownHighlight = CType(resources.GetObject("resource.ChevronDownHighlight"), System.Drawing.Bitmap)
        Me.XpTaskBox1.ThemeFormat.ChevronUp = CType(resources.GetObject("resource.ChevronUp"), System.Drawing.Bitmap)
        Me.XpTaskBox1.ThemeFormat.ChevronUpHighlight = CType(resources.GetObject("resource.ChevronUpHighlight"), System.Drawing.Bitmap)
        Me.XpTaskBox1.ThemeFormat.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XpTaskBox1.ThemeFormat.HeaderTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.XpTaskBox1.ThemeFormat.HeaderTextHighlightColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.XpTaskBox1.ThemeFormat.LeftHeaderColor = System.Drawing.Color.White
        Me.XpTaskBox1.ThemeFormat.RightHeaderColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        '
        'btnPercDPDToValue
        '
        Me.btnPercDPDToValue.Name = "btnPercDPDToValue"
        Me.btnPercDPDToValue.Text = "% Ach DPD to Value"
        '
        'Settings
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(764, 492)
        Me.Controls.Add(Me.XpTaskBox1)
        Me.Controls.Add(Me.XpGradientPanel2)
        Me.Controls.Add(Me.ItemPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = True
        Me.Name = "Settings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.XpGradientPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Private WithEvents btnReportgrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnMiscellaneous As DevComponents.DotNetBar.ButtonItem
    Private WithEvents XpGradientPanel2 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents btnCancel As Janus.Windows.EditControls.UIButton
    Private WithEvents btnOK As Janus.Windows.EditControls.UIButton
    Private WithEvents XpTaskBox1 As SteepValley.Windows.Forms.ThemedControls.XPTaskBox
    Private WithEvents btnPercDPDToValue As DevComponents.DotNetBar.ButtonItem
End Class
