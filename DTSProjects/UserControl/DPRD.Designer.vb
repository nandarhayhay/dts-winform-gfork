<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DPRD
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
        Me.components = New System.ComponentModel.Container
        Dim chkDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DPRD))
        Dim mcbProgram_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdDetail_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdHeader_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim chkKios_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.pnlViewbyDistributor = New SteepValley.Windows.Forms.XPGradientPanel
        Me.grpViewBy = New Janus.Windows.EditControls.UIGroupBox
        Me.rdbPerDistributor = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbAllDistributor = New Janus.Windows.EditControls.UIRadioButton
        Me.btnFilterDistributor = New DTSProjects.ButtonSearch
        Me.chkDistributor = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.mcbProgram = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.grdDetail = New Janus.Windows.GridEX.GridEX
        Me.pnlMainCategory = New SteepValley.Windows.Forms.XPGradientPanel
        Me.XpTaskBox2 = New SteepValley.Windows.Forms.ThemedControls.XPTaskBox
        Me.rdbSumaryDPRD = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbReachingTarget = New Janus.Windows.EditControls.UIRadioButton
        Me.XpTaskBox1 = New SteepValley.Windows.Forms.ThemedControls.XPTaskBox
        Me.btnFilterProgram = New DTSProjects.ButtonSearch
        Me.lblEndDate = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblStartDate = New System.Windows.Forms.Label
        Me.btnSearchProgramID = New DTSProjects.ButtonSearch
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.grdHeader = New Janus.Windows.GridEX.GridEX
        Me.pnlDetail = New System.Windows.Forms.Panel
        Me.ExpandableSplitter2 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel
        Me.pnlViewByKios = New SteepValley.Windows.Forms.XPGradientPanel
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.rdbPerKios = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbAllKios = New Janus.Windows.EditControls.UIRadioButton
        Me.btnFilterKios = New DTSProjects.ButtonSearch
        Me.chkKios = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.pnlViewbyDistributor.SuspendLayout()
        CType(Me.grpViewBy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpViewBy.SuspendLayout()
        CType(Me.mcbProgram, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMainCategory.SuspendLayout()
        Me.XpTaskBox2.SuspendLayout()
        Me.XpTaskBox1.SuspendLayout()
        CType(Me.grdHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDetail.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlViewByKios.SuspendLayout()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewbyDistributor
        '
        Me.pnlViewbyDistributor.Controls.Add(Me.grpViewBy)
        Me.pnlViewbyDistributor.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlViewbyDistributor.EndColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.pnlViewbyDistributor.Location = New System.Drawing.Point(200, 0)
        Me.pnlViewbyDistributor.Name = "pnlViewbyDistributor"
        Me.pnlViewbyDistributor.Size = New System.Drawing.Size(607, 39)
        Me.pnlViewbyDistributor.StartColor = System.Drawing.SystemColors.InactiveCaption
        Me.pnlViewbyDistributor.TabIndex = 0
        '
        'grpViewBy
        '
        Me.grpViewBy.BackColor = System.Drawing.Color.Transparent
        Me.grpViewBy.Controls.Add(Me.rdbPerDistributor)
        Me.grpViewBy.Controls.Add(Me.rdbAllDistributor)
        Me.grpViewBy.Controls.Add(Me.btnFilterDistributor)
        Me.grpViewBy.Controls.Add(Me.chkDistributor)
        Me.grpViewBy.Icon = CType(resources.GetObject("grpViewBy.Icon"), System.Drawing.Icon)
        Me.grpViewBy.Location = New System.Drawing.Point(85, -1)
        Me.grpViewBy.Name = "grpViewBy"
        Me.grpViewBy.Size = New System.Drawing.Size(465, 36)
        Me.grpViewBy.TabIndex = 1
        Me.grpViewBy.Text = "View by"
        Me.grpViewBy.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'rdbPerDistributor
        '
        Me.rdbPerDistributor.Location = New System.Drawing.Point(95, 13)
        Me.rdbPerDistributor.Name = "rdbPerDistributor"
        Me.rdbPerDistributor.Size = New System.Drawing.Size(92, 22)
        Me.rdbPerDistributor.TabIndex = 6
        Me.rdbPerDistributor.Text = "Per Distributor"
        Me.rdbPerDistributor.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbAllDistributor
        '
        Me.rdbAllDistributor.Checked = True
        Me.rdbAllDistributor.Location = New System.Drawing.Point(7, 13)
        Me.rdbAllDistributor.Name = "rdbAllDistributor"
        Me.rdbAllDistributor.Size = New System.Drawing.Size(88, 23)
        Me.rdbAllDistributor.TabIndex = 5
        Me.rdbAllDistributor.TabStop = True
        Me.rdbAllDistributor.Text = "All Distributor"
        Me.rdbAllDistributor.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnFilterDistributor
        '
        Me.btnFilterDistributor.Location = New System.Drawing.Point(443, 14)
        Me.btnFilterDistributor.Name = "btnFilterDistributor"
        Me.btnFilterDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterDistributor.TabIndex = 4
        '
        'chkDistributor
        '
        Me.chkDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        chkDistributor_DesignTimeLayout.LayoutString = resources.GetString("chkDistributor_DesignTimeLayout.LayoutString")
        Me.chkDistributor.DesignTimeLayout = chkDistributor_DesignTimeLayout
        Me.chkDistributor.DropDownDisplayMember = "DISTRIBUTOR_NAME"
        Me.chkDistributor.DropDownValueMember = "DISTRIBUTOR_ID"
        Me.chkDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.chkDistributor.Location = New System.Drawing.Point(193, 12)
        Me.chkDistributor.Name = "chkDistributor"
        Me.chkDistributor.SaveSettings = False
        Me.chkDistributor.Size = New System.Drawing.Size(243, 20)
        Me.chkDistributor.TabIndex = 2
        Me.chkDistributor.ValuesDataMember = Nothing
        Me.chkDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'mcbProgram
        '
        Me.mcbProgram.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbProgram_DesignTimeLayout.LayoutString = resources.GetString("mcbProgram_DesignTimeLayout.LayoutString")
        Me.mcbProgram.DesignTimeLayout = mcbProgram_DesignTimeLayout
        Me.mcbProgram.DisplayMember = "PROGRAM_ID"
        Me.mcbProgram.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbProgram.Location = New System.Drawing.Point(11, 48)
        Me.mcbProgram.Name = "mcbProgram"
        Me.mcbProgram.SelectedIndex = -1
        Me.mcbProgram.SelectedItem = Nothing
        Me.mcbProgram.Size = New System.Drawing.Size(153, 20)
        Me.mcbProgram.TabIndex = 4
        Me.mcbProgram.ValueMember = "PROGRAM_ID"
        Me.mcbProgram.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grdDetail
        '
        Me.grdDetail.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdDetail.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdDetail.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdDetail_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdDetail.DesignTimeLayout = grdDetail_DesignTimeLayout
        Me.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdDetail.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.grdDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdDetail.GroupByBoxVisible = False
        Me.grdDetail.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdDetail.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdDetail.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdDetail.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdDetail.Location = New System.Drawing.Point(0, 0)
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdDetail.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdDetail.Size = New System.Drawing.Size(607, 210)
        Me.grdDetail.TabIndex = 1
        Me.grdDetail.TableHeaderFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdDetail.TableHeaderFormatStyle.ForeColor = System.Drawing.Color.Maroon
        Me.grdDetail.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdDetail.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.grdDetail.WatermarkImage.Image = CType(resources.GetObject("grdDetail.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdDetail.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'pnlMainCategory
        '
        Me.pnlMainCategory.Controls.Add(Me.XpTaskBox2)
        Me.pnlMainCategory.Controls.Add(Me.XpTaskBox1)
        Me.pnlMainCategory.Controls.Add(Me.btnSearchProgramID)
        Me.pnlMainCategory.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlMainCategory.EndColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.pnlMainCategory.Location = New System.Drawing.Point(0, 0)
        Me.pnlMainCategory.Name = "pnlMainCategory"
        Me.pnlMainCategory.Size = New System.Drawing.Size(194, 512)
        Me.pnlMainCategory.StartColor = System.Drawing.SystemColors.InactiveCaption
        Me.pnlMainCategory.TabIndex = 35
        '
        'XpTaskBox2
        '
        Me.XpTaskBox2.BackColor = System.Drawing.Color.Transparent
        Me.XpTaskBox2.Controls.Add(Me.rdbSumaryDPRD)
        Me.XpTaskBox2.Controls.Add(Me.rdbReachingTarget)
        Me.XpTaskBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpTaskBox2.HeaderText = "Category report"
        Me.XpTaskBox2.Icon = CType(resources.GetObject("XpTaskBox2.Icon"), System.Drawing.Icon)
        Me.XpTaskBox2.Location = New System.Drawing.Point(0, 178)
        Me.XpTaskBox2.Name = "XpTaskBox2"
        Me.XpTaskBox2.Padding = New System.Windows.Forms.Padding(4, 44, 4, 4)
        Me.XpTaskBox2.Size = New System.Drawing.Size(194, 124)
        Me.XpTaskBox2.TabIndex = 34
        Me.XpTaskBox2.ThemeFormat.BodyColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.XpTaskBox2.ThemeFormat.BodyFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XpTaskBox2.ThemeFormat.BodyTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.XpTaskBox2.ThemeFormat.BorderColor = System.Drawing.Color.White
        Me.XpTaskBox2.ThemeFormat.ChevronDown = CType(resources.GetObject("resource.ChevronDown"), System.Drawing.Bitmap)
        Me.XpTaskBox2.ThemeFormat.ChevronDownHighlight = CType(resources.GetObject("resource.ChevronDownHighlight"), System.Drawing.Bitmap)
        Me.XpTaskBox2.ThemeFormat.ChevronUp = CType(resources.GetObject("resource.ChevronUp"), System.Drawing.Bitmap)
        Me.XpTaskBox2.ThemeFormat.ChevronUpHighlight = CType(resources.GetObject("resource.ChevronUpHighlight"), System.Drawing.Bitmap)
        Me.XpTaskBox2.ThemeFormat.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XpTaskBox2.ThemeFormat.HeaderTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.XpTaskBox2.ThemeFormat.HeaderTextHighlightColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.XpTaskBox2.ThemeFormat.LeftHeaderColor = System.Drawing.Color.White
        Me.XpTaskBox2.ThemeFormat.RightHeaderColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        '
        'rdbSumaryDPRD
        '
        Me.rdbSumaryDPRD.Location = New System.Drawing.Point(10, 82)
        Me.rdbSumaryDPRD.Name = "rdbSumaryDPRD"
        Me.rdbSumaryDPRD.Size = New System.Drawing.Size(104, 23)
        Me.rdbSumaryDPRD.TabIndex = 1
        Me.rdbSumaryDPRD.Text = "Sumary"
        Me.rdbSumaryDPRD.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbReachingTarget
        '
        Me.rdbReachingTarget.Location = New System.Drawing.Point(10, 51)
        Me.rdbReachingTarget.Name = "rdbReachingTarget"
        Me.rdbReachingTarget.Size = New System.Drawing.Size(104, 23)
        Me.rdbReachingTarget.TabIndex = 0
        Me.rdbReachingTarget.Text = "Achievement"
        Me.rdbReachingTarget.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'XpTaskBox1
        '
        Me.XpTaskBox1.BackColor = System.Drawing.Color.Transparent
        Me.XpTaskBox1.Controls.Add(Me.mcbProgram)
        Me.XpTaskBox1.Controls.Add(Me.btnFilterProgram)
        Me.XpTaskBox1.Controls.Add(Me.lblEndDate)
        Me.XpTaskBox1.Controls.Add(Me.Label5)
        Me.XpTaskBox1.Controls.Add(Me.Label4)
        Me.XpTaskBox1.Controls.Add(Me.lblStartDate)
        Me.XpTaskBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpTaskBox1.HeaderText = "Program(DPRDS)"
        Me.XpTaskBox1.Icon = CType(resources.GetObject("XpTaskBox1.Icon"), System.Drawing.Icon)
        Me.XpTaskBox1.Location = New System.Drawing.Point(0, 0)
        Me.XpTaskBox1.Name = "XpTaskBox1"
        Me.XpTaskBox1.Padding = New System.Windows.Forms.Padding(4, 44, 4, 4)
        Me.XpTaskBox1.Size = New System.Drawing.Size(194, 178)
        Me.XpTaskBox1.TabIndex = 33
        Me.XpTaskBox1.ThemeFormat.BodyColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.XpTaskBox1.ThemeFormat.BodyFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.XpTaskBox1.ThemeFormat.BodyTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.XpTaskBox1.ThemeFormat.BorderColor = System.Drawing.Color.White
        Me.XpTaskBox1.ThemeFormat.ChevronDown = CType(resources.GetObject("resource.ChevronDown1"), System.Drawing.Bitmap)
        Me.XpTaskBox1.ThemeFormat.ChevronDownHighlight = CType(resources.GetObject("resource.ChevronDownHighlight1"), System.Drawing.Bitmap)
        Me.XpTaskBox1.ThemeFormat.ChevronUp = CType(resources.GetObject("resource.ChevronUp1"), System.Drawing.Bitmap)
        Me.XpTaskBox1.ThemeFormat.ChevronUpHighlight = CType(resources.GetObject("resource.ChevronUpHighlight1"), System.Drawing.Bitmap)
        Me.XpTaskBox1.ThemeFormat.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XpTaskBox1.ThemeFormat.HeaderTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.XpTaskBox1.ThemeFormat.HeaderTextHighlightColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.XpTaskBox1.ThemeFormat.LeftHeaderColor = System.Drawing.Color.White
        Me.XpTaskBox1.ThemeFormat.RightHeaderColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        '
        'btnFilterProgram
        '
        Me.btnFilterProgram.Location = New System.Drawing.Point(170, 49)
        Me.btnFilterProgram.Name = "btnFilterProgram"
        Me.btnFilterProgram.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterProgram.TabIndex = 1
        '
        'lblEndDate
        '
        Me.lblEndDate.BackColor = System.Drawing.Color.Transparent
        Me.lblEndDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEndDate.Location = New System.Drawing.Point(10, 133)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(168, 23)
        Me.lblEndDate.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(7, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "StartDate"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(7, 117)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "End Date"
        '
        'lblStartDate
        '
        Me.lblStartDate.BackColor = System.Drawing.Color.Transparent
        Me.lblStartDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStartDate.Location = New System.Drawing.Point(10, 89)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(168, 23)
        Me.lblStartDate.TabIndex = 2
        '
        'btnSearchProgramID
        '
        Me.btnSearchProgramID.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchProgramID.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearchProgramID.Location = New System.Drawing.Point(273, 2)
        Me.btnSearchProgramID.Name = "btnSearchProgramID"
        Me.btnSearchProgramID.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchProgramID.TabIndex = 32
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.ExpandableControl = Me.pnlMainCategory
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
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(194, 0)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(6, 512)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 36
        Me.ExpandableSplitter1.TabStop = False
        '
        'grdHeader
        '
        Me.grdHeader.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdHeader.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdHeader.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdHeader_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdHeader.DesignTimeLayout = grdHeader_DesignTimeLayout
        Me.grdHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdHeader.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdHeader.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.grdHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdHeader.GroupByBoxVisible = False
        Me.grdHeader.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdHeader.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdHeader.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdHeader.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdHeader.Location = New System.Drawing.Point(0, 0)
        Me.grdHeader.Name = "grdHeader"
        Me.grdHeader.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdHeader.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdHeader.Size = New System.Drawing.Size(607, 218)
        Me.grdHeader.TabIndex = 37
        Me.grdHeader.TableHeaderFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.grdHeader.TableHeaderFormatStyle.ForeColor = System.Drawing.Color.Maroon
        Me.grdHeader.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdHeader.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdHeader.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.grdHeader.WatermarkImage.Image = CType(resources.GetObject("grdHeader.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdHeader.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'pnlDetail
        '
        Me.pnlDetail.Controls.Add(Me.grdDetail)
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlDetail.Location = New System.Drawing.Point(200, 302)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(607, 210)
        Me.pnlDetail.TabIndex = 38
        '
        'ExpandableSplitter2
        '
        Me.ExpandableSplitter2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ExpandableSplitter2.BackColor2 = System.Drawing.Color.Empty
        Me.ExpandableSplitter2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.None
        Me.ExpandableSplitter2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.None
        Me.ExpandableSplitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ExpandableSplitter2.ExpandableControl = Me.pnlDetail
        Me.ExpandableSplitter2.ExpandFillColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter2.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.ExpandableSplitter2.ExpandLineColor = System.Drawing.SystemColors.Highlight
        Me.ExpandableSplitter2.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder
        Me.ExpandableSplitter2.GripDarkColor = System.Drawing.SystemColors.Highlight
        Me.ExpandableSplitter2.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder
        Me.ExpandableSplitter2.GripLightColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ExpandableSplitter2.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.MenuBackground
        Me.ExpandableSplitter2.HotBackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter2.HotBackColor2 = System.Drawing.Color.Empty
        Me.ExpandableSplitter2.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.None
        Me.ExpandableSplitter2.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemCheckedBackground
        Me.ExpandableSplitter2.HotExpandFillColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter2.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.ExpandableSplitter2.HotExpandLineColor = System.Drawing.SystemColors.Highlight
        Me.ExpandableSplitter2.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder
        Me.ExpandableSplitter2.HotGripDarkColor = System.Drawing.SystemColors.Highlight
        Me.ExpandableSplitter2.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder
        Me.ExpandableSplitter2.HotGripLightColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ExpandableSplitter2.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.MenuBackground
        Me.ExpandableSplitter2.Location = New System.Drawing.Point(200, 296)
        Me.ExpandableSplitter2.Name = "ExpandableSplitter2"
        Me.ExpandableSplitter2.Size = New System.Drawing.Size(607, 6)
        Me.ExpandableSplitter2.Style = DevComponents.DotNetBar.eSplitterStyle.Mozilla
        Me.ExpandableSplitter2.TabIndex = 39
        Me.ExpandableSplitter2.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.grdHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlHeader.Location = New System.Drawing.Point(200, 78)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(607, 218)
        Me.pnlHeader.TabIndex = 40
        '
        'pnlViewByKios
        '
        Me.pnlViewByKios.Controls.Add(Me.UiGroupBox1)
        Me.pnlViewByKios.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlViewByKios.EndColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.pnlViewByKios.Location = New System.Drawing.Point(200, 39)
        Me.pnlViewByKios.Name = "pnlViewByKios"
        Me.pnlViewByKios.Size = New System.Drawing.Size(607, 39)
        Me.pnlViewByKios.StartColor = System.Drawing.SystemColors.InactiveCaption
        Me.pnlViewByKios.TabIndex = 41
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UiGroupBox1.Controls.Add(Me.rdbPerKios)
        Me.UiGroupBox1.Controls.Add(Me.rdbAllKios)
        Me.UiGroupBox1.Controls.Add(Me.btnFilterKios)
        Me.UiGroupBox1.Controls.Add(Me.chkKios)
        Me.UiGroupBox1.Icon = CType(resources.GetObject("UiGroupBox1.Icon"), System.Drawing.Icon)
        Me.UiGroupBox1.Location = New System.Drawing.Point(85, -1)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(465, 36)
        Me.UiGroupBox1.TabIndex = 1
        Me.UiGroupBox1.Text = "View by"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'rdbPerKios
        '
        Me.rdbPerKios.Location = New System.Drawing.Point(95, 13)
        Me.rdbPerKios.Name = "rdbPerKios"
        Me.rdbPerKios.Size = New System.Drawing.Size(92, 22)
        Me.rdbPerKios.TabIndex = 6
        Me.rdbPerKios.Text = "Per Kios"
        Me.rdbPerKios.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbAllKios
        '
        Me.rdbAllKios.Location = New System.Drawing.Point(7, 13)
        Me.rdbAllKios.Name = "rdbAllKios"
        Me.rdbAllKios.Size = New System.Drawing.Size(88, 23)
        Me.rdbAllKios.TabIndex = 5
        Me.rdbAllKios.Text = "All Kios"
        Me.rdbAllKios.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnFilterKios
        '
        Me.btnFilterKios.Location = New System.Drawing.Point(443, 14)
        Me.btnFilterKios.Name = "btnFilterKios"
        Me.btnFilterKios.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterKios.TabIndex = 4
        '
        'chkKios
        '
        Me.chkKios.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        chkKios_DesignTimeLayout.LayoutString = resources.GetString("chkKios_DesignTimeLayout.LayoutString")
        Me.chkKios.DesignTimeLayout = chkKios_DesignTimeLayout
        Me.chkKios.DropDownDisplayMember = "Kios_Name"
        Me.chkKios.DropDownValueMember = "IDKios"
        Me.chkKios.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.chkKios.Location = New System.Drawing.Point(193, 12)
        Me.chkKios.Name = "chkKios"
        Me.chkKios.SaveSettings = False
        Me.chkKios.Size = New System.Drawing.Size(243, 20)
        Me.chkKios.TabIndex = 2
        Me.chkKios.ValuesDataMember = Nothing
        Me.chkKios.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Timer2
        '
        Me.Timer2.Interval = 800
        '
        'DPRD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlViewByKios)
        Me.Controls.Add(Me.ExpandableSplitter2)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.pnlViewbyDistributor)
        Me.Controls.Add(Me.ExpandableSplitter1)
        Me.Controls.Add(Me.pnlMainCategory)
        Me.Name = "DPRD"
        Me.Size = New System.Drawing.Size(807, 512)
        Me.pnlViewbyDistributor.ResumeLayout(False)
        CType(Me.grpViewBy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpViewBy.ResumeLayout(False)
        Me.grpViewBy.PerformLayout()
        CType(Me.mcbProgram, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMainCategory.ResumeLayout(False)
        Me.XpTaskBox2.ResumeLayout(False)
        Me.XpTaskBox1.ResumeLayout(False)
        Me.XpTaskBox1.PerformLayout()
        CType(Me.grdHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlViewByKios.ResumeLayout(False)
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.UiGroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpViewBy As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents btnFilterDistributor As DTSProjects.ButtonSearch
    Private WithEvents chkDistributor As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Private WithEvents rdbPerDistributor As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbAllDistributor As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents mcbProgram As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents grdDetail As Janus.Windows.GridEX.GridEX
    Private WithEvents btnFilterProgram As DTSProjects.ButtonSearch
    Private WithEvents lblEndDate As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents lblStartDate As System.Windows.Forms.Label
    Private WithEvents btnSearchProgramID As DTSProjects.ButtonSearch
    Private WithEvents XpTaskBox1 As SteepValley.Windows.Forms.ThemedControls.XPTaskBox
    Private WithEvents XpTaskBox2 As SteepValley.Windows.Forms.ThemedControls.XPTaskBox
    Private WithEvents rdbSumaryDPRD As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbReachingTarget As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents grdHeader As Janus.Windows.GridEX.GridEX
    Private WithEvents pnlDetail As System.Windows.Forms.Panel
    Private WithEvents ExpandableSplitter2 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents pnlViewbyDistributor As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents pnlMainCategory As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents Timer1 As System.Windows.Forms.Timer
    Private WithEvents pnlHeader As System.Windows.Forms.Panel
    Private WithEvents pnlViewByKios As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents rdbPerKios As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbAllKios As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents btnFilterKios As DTSProjects.ButtonSearch
    Private WithEvents chkKios As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer

End Class
