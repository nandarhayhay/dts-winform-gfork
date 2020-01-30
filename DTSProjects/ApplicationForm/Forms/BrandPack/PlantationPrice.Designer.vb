<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlantationPrice
    Inherits DTSProjects.BaseBigForm

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
        Dim mcbPlantation_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PlantationPrice))
        Dim chkDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbBrandPack_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.dtPicEndDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.chkIncludeDPD = New System.Windows.Forms.CheckBox
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.btnSave = New Janus.Windows.EditControls.UIButton
        Me.btnAddNew = New Janus.Windows.EditControls.UIButton
        Me.grpKebun = New Janus.Windows.EditControls.UIGroupBox
        Me.btnSearchPlantation = New DTSProjects.ButtonSearch
        Me.mcbPlantation = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.btnNewKebun = New DTSProjects.AddNew
        Me.dtPicStartDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.txtPrice = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.btnSearchDistributor = New DTSProjects.ButtonSearch
        Me.chkDistributor = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.btnSeacrhBrandPack = New DTSProjects.ButtonSearch
        Me.mcbBrandPack = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSettings = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterEqual = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.TManager1 = New DTSProjects.AdvancedTManager
        Me.Panel1.SuspendLayout()
        Me.XpGradientPanel1.SuspendLayout()
        CType(Me.grpKebun, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpKebun.SuspendLayout()
        CType(Me.mcbPlantation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dtPicEndDate)
        Me.Panel1.Controls.Add(Me.chkIncludeDPD)
        Me.Panel1.Controls.Add(Me.XpGradientPanel1)
        Me.Panel1.Controls.Add(Me.grpKebun)
        Me.Panel1.Controls.Add(Me.dtPicStartDate)
        Me.Panel1.Controls.Add(Me.txtPrice)
        Me.Panel1.Controls.Add(Me.btnSearchDistributor)
        Me.Panel1.Controls.Add(Me.chkDistributor)
        Me.Panel1.Controls.Add(Me.btnSeacrhBrandPack)
        Me.Panel1.Controls.Add(Me.mcbBrandPack)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(243, 399)
        Me.Panel1.TabIndex = 0
        '
        'dtPicEndDate
        '
        Me.dtPicEndDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicEndDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicEndDate.DropDownCalendar.Name = ""
        Me.dtPicEndDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicEndDate.Location = New System.Drawing.Point(13, 67)
        Me.dtPicEndDate.Name = "dtPicEndDate"
        Me.dtPicEndDate.Size = New System.Drawing.Size(200, 20)
        Me.dtPicEndDate.TabIndex = 16
        Me.dtPicEndDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'chkIncludeDPD
        '
        Me.chkIncludeDPD.AutoSize = True
        Me.chkIncludeDPD.Location = New System.Drawing.Point(13, 298)
        Me.chkIncludeDPD.Name = "chkIncludeDPD"
        Me.chkIncludeDPD.Size = New System.Drawing.Size(164, 17)
        Me.chkIncludeDPD.TabIndex = 15
        Me.chkIncludeDPD.Text = "Include to DPD Achievement"
        Me.chkIncludeDPD.UseVisualStyleBackColor = True
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.btnSave)
        Me.XpGradientPanel1.Controls.Add(Me.btnAddNew)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.XpGradientPanel1.EndColor = System.Drawing.SystemColors.MenuBar
        Me.XpGradientPanel1.Location = New System.Drawing.Point(0, 361)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(243, 38)
        Me.XpGradientPanel1.StartColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.XpGradientPanel1.TabIndex = 14
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(164, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "&Save"
        Me.btnSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnAddNew
        '
        Me.btnAddNew.Location = New System.Drawing.Point(13, 8)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(75, 23)
        Me.btnAddNew.TabIndex = 0
        Me.btnAddNew.Text = "&Add New"
        Me.btnAddNew.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'grpKebun
        '
        Me.grpKebun.Controls.Add(Me.btnSearchPlantation)
        Me.grpKebun.Controls.Add(Me.mcbPlantation)
        Me.grpKebun.Controls.Add(Me.btnNewKebun)
        Me.grpKebun.Location = New System.Drawing.Point(4, 189)
        Me.grpKebun.Name = "grpKebun"
        Me.grpKebun.Size = New System.Drawing.Size(232, 52)
        Me.grpKebun.TabIndex = 13
        Me.grpKebun.Text = "PLANTATION"
        Me.grpKebun.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnSearchPlantation
        '
        Me.btnSearchPlantation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchPlantation.Location = New System.Drawing.Point(193, 21)
        Me.btnSearchPlantation.Name = "btnSearchPlantation"
        Me.btnSearchPlantation.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchPlantation.TabIndex = 14
        '
        'mcbPlantation
        '
        Me.mcbPlantation.AutoComplete = False
        mcbPlantation_DesignTimeLayout.LayoutString = resources.GetString("mcbPlantation_DesignTimeLayout.LayoutString")
        Me.mcbPlantation.DesignTimeLayout = mcbPlantation_DesignTimeLayout
        Me.mcbPlantation.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbPlantation.Location = New System.Drawing.Point(8, 20)
        Me.mcbPlantation.Name = "mcbPlantation"
        Me.mcbPlantation.SelectedIndex = -1
        Me.mcbPlantation.SelectedItem = Nothing
        Me.mcbPlantation.Size = New System.Drawing.Size(182, 20)
        Me.mcbPlantation.TabIndex = 13
        Me.mcbPlantation.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnNewKebun
        '
        Me.btnNewKebun.Location = New System.Drawing.Point(213, 21)
        Me.btnNewKebun.Name = "btnNewKebun"
        Me.btnNewKebun.Size = New System.Drawing.Size(17, 16)
        Me.btnNewKebun.TabIndex = 12
        Me.btnNewKebun.Visible = False
        '
        'dtPicStartDate
        '
        Me.dtPicStartDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicStartDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicStartDate.DropDownCalendar.Name = ""
        Me.dtPicStartDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicStartDate.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicStartDate.Location = New System.Drawing.Point(12, 32)
        Me.dtPicStartDate.Name = "dtPicStartDate"
        Me.dtPicStartDate.ShowTodayButton = False
        Me.dtPicStartDate.Size = New System.Drawing.Size(202, 20)
        Me.dtPicStartDate.TabIndex = 12
        Me.dtPicStartDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(12, 270)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(142, 20)
        Me.txtPrice.TabIndex = 11
        Me.txtPrice.Text = "0,00"
        Me.txtPrice.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'btnSearchDistributor
        '
        Me.btnSearchDistributor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchDistributor.Location = New System.Drawing.Point(217, 160)
        Me.btnSearchDistributor.Name = "btnSearchDistributor"
        Me.btnSearchDistributor.Size = New System.Drawing.Size(20, 18)
        Me.btnSearchDistributor.TabIndex = 10
        '
        'chkDistributor
        '
        Me.chkDistributor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        chkDistributor_DesignTimeLayout.LayoutString = resources.GetString("chkDistributor_DesignTimeLayout.LayoutString")
        Me.chkDistributor.DesignTimeLayout = chkDistributor_DesignTimeLayout
        Me.chkDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.chkDistributor.Location = New System.Drawing.Point(12, 158)
        Me.chkDistributor.Name = "chkDistributor"
        Me.chkDistributor.SaveSettings = False
        Me.chkDistributor.Size = New System.Drawing.Size(202, 20)
        Me.chkDistributor.TabIndex = 9
        Me.chkDistributor.ValuesDataMember = Nothing
        Me.chkDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnSeacrhBrandPack
        '
        Me.btnSeacrhBrandPack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSeacrhBrandPack.Location = New System.Drawing.Point(217, 116)
        Me.btnSeacrhBrandPack.Name = "btnSeacrhBrandPack"
        Me.btnSeacrhBrandPack.Size = New System.Drawing.Size(20, 18)
        Me.btnSeacrhBrandPack.TabIndex = 8
        '
        'mcbBrandPack
        '
        Me.mcbBrandPack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.mcbBrandPack.AutoComplete = False
        mcbBrandPack_DesignTimeLayout.LayoutString = resources.GetString("mcbBrandPack_DesignTimeLayout.LayoutString")
        Me.mcbBrandPack.DesignTimeLayout = mcbBrandPack_DesignTimeLayout
        Me.mcbBrandPack.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbBrandPack.Location = New System.Drawing.Point(11, 115)
        Me.mcbBrandPack.Name = "mcbBrandPack"
        Me.mcbBrandPack.SelectedIndex = -1
        Me.mcbBrandPack.SelectedItem = Nothing
        Me.mcbBrandPack.Size = New System.Drawing.Size(203, 20)
        Me.mcbBrandPack.TabIndex = 7
        Me.mcbBrandPack.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "START DATE"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 250)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "PRICE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 142)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "DITRIBUTOR"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "BRAND PACK"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Grid.bmp")
        Me.ImageList1.Images.SetKeyName(1, "Filter 48 h p.ico")
        Me.ImageList1.Images.SetKeyName(2, "Customize.png")
        Me.ImageList1.Images.SetKeyName(3, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(4, "Save.png")
        Me.ImageList1.Images.SetKeyName(5, "TextEdit.png")
        Me.ImageList1.Images.SetKeyName(6, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(7, "gridColumn.png")
        Me.ImageList1.Images.SetKeyName(8, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(9, "printer.ico")
        Me.ImageList1.Images.SetKeyName(10, "Export.bmp")
        Me.ImageList1.Images.SetKeyName(11, "DB_Refresh.ico")
        Me.ImageList1.Images.SetKeyName(12, "PageSetup.BMP")
        Me.ImageList1.Images.SetKeyName(13, "generator.ico")
        Me.ImageList1.Images.SetKeyName(14, "Discount.ICO")
        Me.ImageList1.Images.SetKeyName(15, "Save.png")
        Me.ImageList1.Images.SetKeyName(16, "Add.png")
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar2 (Bar2)"
        Me.Bar2.AccessibleName = "Bar2"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaption
        Me.Bar2.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaptionText
        Me.Bar2.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnExport, Me.btnRefresh})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(856, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 23
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 0
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnColumn, Me.btnSettingGrid, Me.btnPrint, Me.btnPageSettings})
        Me.btnGrid.Text = "Grid"
        '
        'btnColumn
        '
        Me.btnColumn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnColumn.ImageIndex = 7
        Me.btnColumn.Name = "btnColumn"
        Me.btnColumn.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnShowFieldChooser})
        Me.btnColumn.Text = "Grid Column"
        '
        'btnShowFieldChooser
        '
        Me.btnShowFieldChooser.Name = "btnShowFieldChooser"
        Me.btnShowFieldChooser.Text = "Show Field Chooser"
        Me.btnShowFieldChooser.Tooltip = "show / remove column by draging  / dropping any column in datagrid"
        '
        'btnSettingGrid
        '
        Me.btnSettingGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSettingGrid.ImageIndex = 2
        Me.btnSettingGrid.Name = "btnSettingGrid"
        Me.btnSettingGrid.Text = "Setting Grid"
        Me.btnSettingGrid.Tooltip = "use this item to show grid by your own needs ,can also for defining printing grid" & _
            ""
        '
        'btnPrint
        '
        Me.btnPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrint.ImageIndex = 9
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Text = "Print data Grid"
        Me.btnPrint.Tooltip = "print datagrid,all visible rows in datagrid will be printed"
        '
        'btnPageSettings
        '
        Me.btnPageSettings.ImageIndex = 12
        Me.btnPageSettings.Name = "btnPageSettings"
        Me.btnPageSettings.Text = "Page Settings"
        Me.btnPageSettings.Tooltip = "setting datagrid page ,use this setting if you want to print datagrid with page s" & _
            "etting defined by yourseef "
        '
        'btnFilter
        '
        Me.btnFilter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnFilter.ImageIndex = 1
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCustomFilter, Me.btnFilterEqual})
        Me.btnFilter.Text = "Filter Row"
        '
        'btnCustomFilter
        '
        Me.btnCustomFilter.Name = "btnCustomFilter"
        Me.btnCustomFilter.Text = "Custom Filter"
        Me.btnCustomFilter.Tooltip = "use this filter to filter data in datagrid  by your own criteria"
        '
        'btnFilterEqual
        '
        Me.btnFilterEqual.Name = "btnFilterEqual"
        Me.btnFilterEqual.Text = "Default"
        Me.btnFilterEqual.Tooltip = "use this filter to filter data in data grid directly from datagrid with contains " & _
            "criteria"
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 10
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 11
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.Tooltip = "Reload all possible data row  and refresh grid"
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.Transparent
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Equal
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 25)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(0, 0)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(856, 29)
        Me.FilterEditor1.Visible = False
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.ExpandableControl = Me.Panel1
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
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(243, 54)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(6, 399)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 27
        Me.ExpandableSplitter1.TabStop = False
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.IncludeFormatStyle = False
        '
        'TManager1
        '
        Me.TManager1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TManager1.Location = New System.Drawing.Point(249, 54)
        Me.TManager1.Name = "TManager1"
        Me.TManager1.Size = New System.Drawing.Size(607, 399)
        Me.TManager1.TabIndex = 29
        '
        'PlantationPrice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(856, 453)
        Me.Controls.Add(Me.TManager1)
        Me.Controls.Add(Me.ExpandableSplitter1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PlantationPrice"
        Me.Text = "BrandPack spesial price for distributor in Price Plantation"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.XpGradientPanel1.ResumeLayout(False)
        CType(Me.grpKebun, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpKebun.ResumeLayout(False)
        Me.grpKebun.PerformLayout()
        CType(Me.mcbPlantation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents btnSeacrhBrandPack As DTSProjects.ButtonSearch
    Private WithEvents txtPrice As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents btnSearchDistributor As DTSProjects.ButtonSearch
    Private WithEvents mcbBrandPack As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents chkDistributor As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents grpKebun As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents TManager1 As DTSProjects.AdvancedTManager
    Friend WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents btnSave As Janus.Windows.EditControls.UIButton
    Private WithEvents btnAddNew As Janus.Windows.EditControls.UIButton
    Private WithEvents btnNewKebun As DTSProjects.AddNew
    Private WithEvents mcbPlantation As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents btnSearchPlantation As DTSProjects.ButtonSearch
    Private WithEvents dtPicStartDate As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents chkIncludeDPD As System.Windows.Forms.CheckBox
    Private WithEvents dtPicEndDate As Janus.Windows.CalendarCombo.CalendarCombo

End Class
