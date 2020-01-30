<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReachingTargetKios
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
        Dim mcbProgramID_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReachingTargetKios))
        Me.lblCreatedBy = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.mcbProgramID = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.rdbDPRDM = New System.Windows.Forms.RadioButton
        Me.rdbDPRDS = New System.Windows.Forms.RadioButton
        Me.lblEndDate = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblProgramName = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblStartDate = New System.Windows.Forms.Label
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ExplorerBar1 = New DevComponents.DotNetBar.ExplorerBar
        Me.xpProgramID = New SteepValley.Windows.Forms.ThemedControls.XPTaskBox
        Me.btnSearchProgramID = New DTSProjects.ButtonSearch
        Me.xpDPRDType = New SteepValley.Windows.Forms.ThemedControls.XPTaskBox
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        CType(Me.mcbProgramID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ExplorerBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExplorerBar1.SuspendLayout()
        Me.xpProgramID.SuspendLayout()
        Me.xpDPRDType.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCreatedBy
        '
        Me.lblCreatedBy.AutoSize = True
        Me.lblCreatedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCreatedBy.Location = New System.Drawing.Point(8, 111)
        Me.lblCreatedBy.Name = "lblCreatedBy"
        Me.lblCreatedBy.Size = New System.Drawing.Size(2, 15)
        Me.lblCreatedBy.TabIndex = 40
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(9, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 19)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "CreatedBy"
        '
        'mcbProgramID
        '
        Me.mcbProgramID.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbProgramID_DesignTimeLayout.LayoutString = resources.GetString("mcbProgramID_DesignTimeLayout.LayoutString")
        Me.mcbProgramID.DesignTimeLayout = mcbProgramID_DesignTimeLayout
        Me.mcbProgramID.Location = New System.Drawing.Point(8, 64)
        Me.mcbProgramID.Name = "mcbProgramID"
        Me.mcbProgramID.SelectedIndex = -1
        Me.mcbProgramID.SelectedItem = Nothing
        Me.mcbProgramID.Size = New System.Drawing.Size(149, 20)
        Me.mcbProgramID.TabIndex = 38
        Me.mcbProgramID.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'rdbDPRDM
        '
        Me.rdbDPRDM.Location = New System.Drawing.Point(83, 47)
        Me.rdbDPRDM.Name = "rdbDPRDM"
        Me.rdbDPRDM.Size = New System.Drawing.Size(65, 24)
        Me.rdbDPRDM.TabIndex = 35
        Me.rdbDPRDM.Text = "DPRDM"
        Me.rdbDPRDM.UseVisualStyleBackColor = True
        '
        'rdbDPRDS
        '
        Me.rdbDPRDS.Location = New System.Drawing.Point(7, 47)
        Me.rdbDPRDS.Name = "rdbDPRDS"
        Me.rdbDPRDS.Size = New System.Drawing.Size(70, 26)
        Me.rdbDPRDS.TabIndex = 34
        Me.rdbDPRDS.Text = "DPRDS"
        Me.rdbDPRDS.UseVisualStyleBackColor = True
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.BackColor = System.Drawing.Color.Transparent
        Me.lblEndDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEndDate.Location = New System.Drawing.Point(7, 256)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(2, 15)
        Me.lblEndDate.TabIndex = 31
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(9, 187)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "StartDate"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(8, 240)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "End Date"
        '
        'lblProgramName
        '
        Me.lblProgramName.AutoSize = True
        Me.lblProgramName.BackColor = System.Drawing.Color.Transparent
        Me.lblProgramName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProgramName.Location = New System.Drawing.Point(7, 158)
        Me.lblProgramName.Name = "lblProgramName"
        Me.lblProgramName.Size = New System.Drawing.Size(2, 15)
        Me.lblProgramName.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(7, 141)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Program Name"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(10, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Program ID"
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.BackColor = System.Drawing.Color.Transparent
        Me.lblStartDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStartDate.Location = New System.Drawing.Point(7, 204)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(2, 15)
        Me.lblStartDate.TabIndex = 28
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.HeaderFormatStyle.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.GridEX1.HeaderFormatStyle.BackColorGradient = System.Drawing.SystemColors.ActiveBorder
        Me.GridEX1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.GridEX1.Location = New System.Drawing.Point(185, 0)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(681, 484)
        Me.GridEX1.TabIndex = 19
        Me.GridEX1.TableHeaderFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.GridEX1.TableHeaderFormatStyle.ForeColor = System.Drawing.Color.Maroon
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.ThemedAreas = Janus.Windows.GridEX.ThemedArea.None
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        '
        'ExplorerBar1
        '
        Me.ExplorerBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.ExplorerBar1.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.ExplorerBar1.BackStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ExplorerBarBackground2
        Me.ExplorerBar1.BackStyle.BackColorGradientAngle = 90
        Me.ExplorerBar1.BackStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ExplorerBarBackground
        Me.ExplorerBar1.Controls.Add(Me.xpProgramID)
        Me.ExplorerBar1.Controls.Add(Me.xpDPRDType)
        Me.ExplorerBar1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ExplorerBar1.GroupImages = Nothing
        Me.ExplorerBar1.Images = Nothing
        Me.ExplorerBar1.Location = New System.Drawing.Point(0, 0)
        Me.ExplorerBar1.Name = "ExplorerBar1"
        Me.ExplorerBar1.Size = New System.Drawing.Size(180, 484)
        Me.ExplorerBar1.TabIndex = 20
        Me.ExplorerBar1.Text = "ExplorerBar1"
        Me.ExplorerBar1.ThemeAware = True
        '
        'xpProgramID
        '
        Me.xpProgramID.BackColor = System.Drawing.Color.Transparent
        Me.xpProgramID.Controls.Add(Me.lblCreatedBy)
        Me.xpProgramID.Controls.Add(Me.lblEndDate)
        Me.xpProgramID.Controls.Add(Me.Label2)
        Me.xpProgramID.Controls.Add(Me.lblStartDate)
        Me.xpProgramID.Controls.Add(Me.mcbProgramID)
        Me.xpProgramID.Controls.Add(Me.Label6)
        Me.xpProgramID.Controls.Add(Me.btnSearchProgramID)
        Me.xpProgramID.Controls.Add(Me.Label1)
        Me.xpProgramID.Controls.Add(Me.lblProgramName)
        Me.xpProgramID.Controls.Add(Me.Label4)
        Me.xpProgramID.Controls.Add(Me.Label5)
        Me.xpProgramID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xpProgramID.HeaderText = "ProgramID"
        Me.xpProgramID.Location = New System.Drawing.Point(0, 80)
        Me.xpProgramID.Name = "xpProgramID"
        Me.xpProgramID.Padding = New System.Windows.Forms.Padding(4, 44, 4, 4)
        Me.xpProgramID.Size = New System.Drawing.Size(180, 404)
        Me.xpProgramID.TabIndex = 0
        Me.xpProgramID.ThemeFormat.BodyColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.xpProgramID.ThemeFormat.BodyFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.xpProgramID.ThemeFormat.BodyTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.xpProgramID.ThemeFormat.BorderColor = System.Drawing.Color.White
        Me.xpProgramID.ThemeFormat.ChevronDown = CType(resources.GetObject("resource.ChevronDown"), System.Drawing.Bitmap)
        Me.xpProgramID.ThemeFormat.ChevronDownHighlight = CType(resources.GetObject("resource.ChevronDownHighlight"), System.Drawing.Bitmap)
        Me.xpProgramID.ThemeFormat.ChevronUp = CType(resources.GetObject("resource.ChevronUp"), System.Drawing.Bitmap)
        Me.xpProgramID.ThemeFormat.ChevronUpHighlight = CType(resources.GetObject("resource.ChevronUpHighlight"), System.Drawing.Bitmap)
        Me.xpProgramID.ThemeFormat.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.xpProgramID.ThemeFormat.HeaderTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.xpProgramID.ThemeFormat.HeaderTextHighlightColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.xpProgramID.ThemeFormat.LeftHeaderColor = System.Drawing.Color.White
        Me.xpProgramID.ThemeFormat.RightHeaderColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        '
        'btnSearchProgramID
        '
        Me.btnSearchProgramID.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchProgramID.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearchProgramID.Location = New System.Drawing.Point(160, 64)
        Me.btnSearchProgramID.Name = "btnSearchProgramID"
        Me.btnSearchProgramID.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchProgramID.TabIndex = 32
        '
        'xpDPRDType
        '
        Me.xpDPRDType.BackColor = System.Drawing.Color.Transparent
        Me.xpDPRDType.Controls.Add(Me.rdbDPRDS)
        Me.xpDPRDType.Controls.Add(Me.rdbDPRDM)
        Me.xpDPRDType.Dock = System.Windows.Forms.DockStyle.Top
        Me.xpDPRDType.HeaderText = "DPRD Type"
        Me.xpDPRDType.Icon = CType(resources.GetObject("xpDPRDType.Icon"), System.Drawing.Icon)
        Me.xpDPRDType.Location = New System.Drawing.Point(0, 0)
        Me.xpDPRDType.Name = "xpDPRDType"
        Me.xpDPRDType.Padding = New System.Windows.Forms.Padding(4, 44, 4, 4)
        Me.xpDPRDType.Size = New System.Drawing.Size(180, 80)
        Me.xpDPRDType.TabIndex = 1
        Me.xpDPRDType.ThemeFormat.BodyColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.xpDPRDType.ThemeFormat.BodyFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.xpDPRDType.ThemeFormat.BodyTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.xpDPRDType.ThemeFormat.BorderColor = System.Drawing.Color.White
        Me.xpDPRDType.ThemeFormat.ChevronDown = CType(resources.GetObject("resource.ChevronDown1"), System.Drawing.Bitmap)
        Me.xpDPRDType.ThemeFormat.ChevronDownHighlight = CType(resources.GetObject("resource.ChevronDownHighlight1"), System.Drawing.Bitmap)
        Me.xpDPRDType.ThemeFormat.ChevronUp = CType(resources.GetObject("resource.ChevronUp1"), System.Drawing.Bitmap)
        Me.xpDPRDType.ThemeFormat.ChevronUpHighlight = CType(resources.GetObject("resource.ChevronUpHighlight1"), System.Drawing.Bitmap)
        Me.xpDPRDType.ThemeFormat.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.xpDPRDType.ThemeFormat.HeaderTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.xpDPRDType.ThemeFormat.HeaderTextHighlightColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.xpDPRDType.ThemeFormat.LeftHeaderColor = System.Drawing.Color.White
        Me.xpDPRDType.ThemeFormat.RightHeaderColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.ExpandableControl = Me.ExplorerBar1
        Me.ExpandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ExpandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ExpandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.ExpandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ExpandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2
        Me.ExpandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.ExpandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ExpandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(180, 0)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(5, 484)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 21
        Me.ExpandableSplitter1.TabStop = False
        '
        'ReachingTargetKios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.ExpandableSplitter1)
        Me.Controls.Add(Me.ExplorerBar1)
        Me.Name = "ReachingTargetKios"
        Me.Size = New System.Drawing.Size(866, 484)
        CType(Me.mcbProgramID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ExplorerBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExplorerBar1.ResumeLayout(False)
        Me.xpProgramID.ResumeLayout(False)
        Me.xpProgramID.PerformLayout()
        Me.xpDPRDType.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents lblStartDate As System.Windows.Forms.Label
    Private WithEvents lblProgramName As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents lblEndDate As System.Windows.Forms.Label
    Private WithEvents btnSearchProgramID As DTSProjects.ButtonSearch
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents lblCreatedBy As System.Windows.Forms.Label
    Private WithEvents mcbProgramID As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents xpProgramID As SteepValley.Windows.Forms.ThemedControls.XPTaskBox
    Private WithEvents ExplorerBar1 As DevComponents.DotNetBar.ExplorerBar
    Private WithEvents xpDPRDType As SteepValley.Windows.Forms.ThemedControls.XPTaskBox
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Friend WithEvents rdbDPRDS As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDPRDM As System.Windows.Forms.RadioButton

End Class
