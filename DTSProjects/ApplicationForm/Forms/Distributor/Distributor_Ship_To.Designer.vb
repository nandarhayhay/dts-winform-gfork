<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Distributor_Ship_To
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Distributor_Ship_To))
        Dim grdTerritory_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdTerritory_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.ButtonImage")
        Dim chkTerritory_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdShipTo_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdShipTo_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.ButtonImage")
        Me.txtTerritoryDescription = New System.Windows.Forms.TextBox
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.mnuBar = New DevComponents.DotNetBar.Bar
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnRenameColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSettings = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterDefault = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnAddNew = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.pnlTerritory = New DevComponents.DotNetBar.ExpandablePanel
        Me.grdTerritory = New Janus.Windows.GridEX.GridEX
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.grpTerritory = New System.Windows.Forms.GroupBox
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.grpShipTo = New Janus.Windows.EditControls.UIGroupBox
        Me.btnFilterShipTo = New DTSProjects.ButtonSearch
        Me.btnFilterDistributor = New DTSProjects.ButtonSearch
        Me.chkTerritory = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.mcbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.grpterritpry = New Janus.Windows.EditControls.UIGroupBox
        Me.txtTerritoryArea = New System.Windows.Forms.TextBox
        Me.txtTerritoryID = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.btnAdd = New Janus.Windows.EditControls.UIButton
        Me.btnInsert = New Janus.Windows.EditControls.UIButton
        Me.pnlShip_To = New DevComponents.DotNetBar.ExpandablePanel
        Me.grdShipTo = New Janus.Windows.GridEX.GridEX
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PrintPreviewDialog2 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXPrintDocument2 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.PageSetupDialog2 = New System.Windows.Forms.PageSetupDialog
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        CType(Me.mnuBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTerritory.SuspendLayout()
        CType(Me.grdTerritory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTerritory.SuspendLayout()
        Me.XpGradientPanel1.SuspendLayout()
        CType(Me.grpShipTo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpShipTo.SuspendLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpterritpry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpterritpry.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        Me.pnlShip_To.SuspendLayout()
        CType(Me.grdShipTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTerritoryDescription
        '
        Me.txtTerritoryDescription.Location = New System.Drawing.Point(12, 131)
        Me.txtTerritoryDescription.MaxLength = 150
        Me.txtTerritoryDescription.Multiline = True
        Me.txtTerritoryDescription.Name = "txtTerritoryDescription"
        Me.txtTerritoryDescription.Size = New System.Drawing.Size(207, 45)
        Me.txtTerritoryDescription.TabIndex = 2
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Cancel.ico")
        Me.ImageList1.Images.SetKeyName(1, "Add.bmp")
        Me.ImageList1.Images.SetKeyName(2, "Grid.bmp")
        Me.ImageList1.Images.SetKeyName(3, "Filter 48 h p.ico")
        Me.ImageList1.Images.SetKeyName(4, "Customize.png")
        Me.ImageList1.Images.SetKeyName(5, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(6, "Save.png")
        Me.ImageList1.Images.SetKeyName(7, "Delete.ico")
        Me.ImageList1.Images.SetKeyName(8, "TextEdit.png")
        Me.ImageList1.Images.SetKeyName(9, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(10, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(11, "gridColumn.png")
        Me.ImageList1.Images.SetKeyName(12, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(13, "printer.ico")
        Me.ImageList1.Images.SetKeyName(14, "Export.bmp")
        Me.ImageList1.Images.SetKeyName(15, "DB_Refresh.ico")
        Me.ImageList1.Images.SetKeyName(16, "PageSetup.BMP")
        '
        'mnuBar
        '
        Me.mnuBar.AccessibleDescription = "Bar2 (Bar2)"
        Me.mnuBar.AccessibleName = "Bar2"
        Me.mnuBar.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.mnuBar.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaption
        Me.mnuBar.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaptionText
        Me.mnuBar.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.mnuBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.mnuBar.FadeEffect = True
        Me.mnuBar.Images = Me.ImageList1
        Me.mnuBar.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnExport, Me.btnAddNew, Me.btnRefresh})
        Me.mnuBar.Location = New System.Drawing.Point(0, 0)
        Me.mnuBar.Name = "mnuBar"
        Me.mnuBar.Size = New System.Drawing.Size(1003, 25)
        Me.mnuBar.Stretch = True
        Me.mnuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.mnuBar.TabIndex = 21
        Me.mnuBar.TabStop = False
        Me.mnuBar.Text = "Bar2"
        '
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 2
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnColumn, Me.btnSettingGrid, Me.btnPrint, Me.btnPageSettings})
        Me.btnGrid.Text = "Grid"
        '
        'btnColumn
        '
        Me.btnColumn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnColumn.ImageIndex = 11
        Me.btnColumn.Name = "btnColumn"
        Me.btnColumn.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnRenameColumn, Me.btnShowFieldChooser})
        Me.btnColumn.Text = "Grid Column"
        '
        'btnRenameColumn
        '
        Me.btnRenameColumn.Name = "btnRenameColumn"
        Me.btnRenameColumn.Text = "Rename Column"
        Me.btnRenameColumn.Tooltip = "use this button to  rename any column header defined by yourself"
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
        Me.btnSettingGrid.ImageIndex = 4
        Me.btnSettingGrid.Name = "btnSettingGrid"
        Me.btnSettingGrid.Text = "Setting Grid"
        Me.btnSettingGrid.Tooltip = "use this item to show grid by your own needs ,can also for defining printing grid" & _
            ""
        '
        'btnPrint
        '
        Me.btnPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrint.ImageIndex = 13
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Text = "Print data Grid"
        Me.btnPrint.Tooltip = "print datagrid,all visible rows in datagrid will be printed"
        '
        'btnPageSettings
        '
        Me.btnPageSettings.ImageIndex = 16
        Me.btnPageSettings.Name = "btnPageSettings"
        Me.btnPageSettings.Text = "Page Settings"
        Me.btnPageSettings.Tooltip = "setting datagrid page ,use this setting if you want to print datagrid with page s" & _
            "etting defined by yourseef "
        '
        'btnFilter
        '
        Me.btnFilter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnFilter.ImageIndex = 3
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCustomFilter, Me.btnFilterDefault})
        Me.btnFilter.Text = "Filter Row"
        '
        'btnCustomFilter
        '
        Me.btnCustomFilter.Name = "btnCustomFilter"
        Me.btnCustomFilter.Text = "Custom Filter"
        Me.btnCustomFilter.Tooltip = "use this filter to filter data in datagrid  by your own criteria"
        '
        'btnFilterDefault
        '
        Me.btnFilterDefault.Name = "btnFilterDefault"
        Me.btnFilterDefault.Text = "Default"
        Me.btnFilterDefault.Tooltip = "use this filter to filter data in data grid directly from datagrid with contains " & _
            "criteria"
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 14
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
        '
        'btnAddNew
        '
        Me.btnAddNew.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnAddNew.ImageIndex = 1
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlN)
        Me.btnAddNew.Text = "Add New"
        Me.btnAddNew.Tooltip = "add new item data"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 15
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5)
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.Tooltip = "Reload data and refresh grid"
        '
        'FilterEditor1
        '
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
        Me.FilterEditor1.Size = New System.Drawing.Size(1003, 29)
        Me.FilterEditor1.Visible = False
        '
        'pnlTerritory
        '
        Me.pnlTerritory.CanvasColor = System.Drawing.SystemColors.Control
        Me.pnlTerritory.Controls.Add(Me.grdTerritory)
        Me.pnlTerritory.Controls.Add(Me.Bar1)
        Me.pnlTerritory.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTerritory.Location = New System.Drawing.Point(239, 54)
        Me.pnlTerritory.Name = "pnlTerritory"
        Me.pnlTerritory.Size = New System.Drawing.Size(764, 234)
        Me.pnlTerritory.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlTerritory.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.pnlTerritory.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.pnlTerritory.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.pnlTerritory.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.pnlTerritory.Style.GradientAngle = 90
        Me.pnlTerritory.TabIndex = 23
        Me.pnlTerritory.TitleHeight = 17
        Me.pnlTerritory.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlTerritory.TitleStyle.BackColor1.Color = System.Drawing.SystemColors.InactiveCaption
        Me.pnlTerritory.TitleStyle.BackColor2.Color = System.Drawing.SystemColors.InactiveCaptionText
        Me.pnlTerritory.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.pnlTerritory.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.pnlTerritory.TitleStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTerritory.TitleStyle.ForeColor.Color = System.Drawing.Color.FromArgb(CType(CType(8, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(114, Byte), Integer))
        Me.pnlTerritory.TitleStyle.GradientAngle = 90
        Me.pnlTerritory.TitleText = "TERRITORY AREA"
        '
        'grdTerritory
        '
        Me.grdTerritory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdTerritory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdTerritory.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdTerritory.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdTerritory.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.grdTerritory.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        grdTerritory_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdTerritory_DesignTimeLayout_Reference_0.Instance"), Object)
        'grdTerritory_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdTerritory_DesignTimeLayout_Reference_0})
        grdTerritory_DesignTimeLayout.LayoutString = resources.GetString("grdTerritory_DesignTimeLayout.LayoutString")
        Me.grdTerritory.DesignTimeLayout = grdTerritory_DesignTimeLayout
        Me.grdTerritory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdTerritory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdTerritory.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdTerritory.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdTerritory.GroupByBoxVisible = False
        Me.grdTerritory.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdTerritory.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdTerritory.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdTerritory.ImageList = Me.ImageList1
        Me.grdTerritory.Location = New System.Drawing.Point(0, 17)
        Me.grdTerritory.Name = "grdTerritory"
        Me.grdTerritory.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdTerritory.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdTerritory.RecordNavigator = True
        Me.grdTerritory.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdTerritory.Size = New System.Drawing.Size(764, 217)
        Me.grdTerritory.TabIndex = 0
        Me.grdTerritory.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.grdTerritory.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Bar1
        '
        Me.Bar1.DockSide = DevComponents.DotNetBar.eDockSide.Top
        Me.Bar1.Location = New System.Drawing.Point(296, 147)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(75, 25)
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar1.TabIndex = 1
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'grpTerritory
        '
        Me.grpTerritory.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.grpTerritory.Controls.Add(Me.XpGradientPanel1)
        Me.grpTerritory.Controls.Add(Me.PanelEx1)
        Me.grpTerritory.Dock = System.Windows.Forms.DockStyle.Left
        Me.grpTerritory.Location = New System.Drawing.Point(0, 54)
        Me.grpTerritory.Name = "grpTerritory"
        Me.grpTerritory.Size = New System.Drawing.Size(231, 601)
        Me.grpTerritory.TabIndex = 2
        Me.grpTerritory.TabStop = False
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.grpShipTo)
        Me.XpGradientPanel1.Controls.Add(Me.grpterritpry)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XpGradientPanel1.Location = New System.Drawing.Point(3, 16)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(225, 554)
        Me.XpGradientPanel1.TabIndex = 6
        '
        'grpShipTo
        '
        Me.grpShipTo.BackColor = System.Drawing.Color.Transparent
        Me.grpShipTo.Controls.Add(Me.btnFilterShipTo)
        Me.grpShipTo.Controls.Add(Me.btnFilterDistributor)
        Me.grpShipTo.Controls.Add(Me.chkTerritory)
        Me.grpShipTo.Controls.Add(Me.mcbDistributor)
        Me.grpShipTo.Controls.Add(Me.Label5)
        Me.grpShipTo.Controls.Add(Me.Label4)
        Me.grpShipTo.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpShipTo.Location = New System.Drawing.Point(0, 182)
        Me.grpShipTo.Name = "grpShipTo"
        Me.grpShipTo.Size = New System.Drawing.Size(225, 111)
        Me.grpShipTo.TabIndex = 5
        Me.grpShipTo.Text = "Ship To Territory"
        Me.grpShipTo.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnFilterShipTo
        '
        Me.btnFilterShipTo.Location = New System.Drawing.Point(202, 81)
        Me.btnFilterShipTo.Name = "btnFilterShipTo"
        Me.btnFilterShipTo.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterShipTo.TabIndex = 5
        '
        'btnFilterDistributor
        '
        Me.btnFilterDistributor.Location = New System.Drawing.Point(202, 39)
        Me.btnFilterDistributor.Name = "btnFilterDistributor"
        Me.btnFilterDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterDistributor.TabIndex = 4
        '
        'chkTerritory
        '
        Me.chkTerritory.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.chkTerritory.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        chkTerritory_DesignTimeLayout.LayoutString = resources.GetString("chkTerritory_DesignTimeLayout.LayoutString")
        Me.chkTerritory.DesignTimeLayout = chkTerritory_DesignTimeLayout
        Me.chkTerritory.DropDownDisplayMember = "TERRITORY_AREA"
        Me.chkTerritory.DropDownValueMember = "TERRITORY_ID"
        Me.chkTerritory.Location = New System.Drawing.Point(12, 82)
        Me.chkTerritory.Name = "chkTerritory"
        Me.chkTerritory.SaveSettings = False
        Me.chkTerritory.Size = New System.Drawing.Size(185, 20)
        Me.chkTerritory.TabIndex = 1
        Me.chkTerritory.ValuesDataMember = Nothing
        Me.chkTerritory.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'mcbDistributor
        '
        Me.mcbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.mcbDistributor.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        mcbDistributor_DesignTimeLayout.LayoutString = resources.GetString("mcbDistributor_DesignTimeLayout.LayoutString")
        Me.mcbDistributor.DesignTimeLayout = mcbDistributor_DesignTimeLayout
        Me.mcbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.mcbDistributor.Location = New System.Drawing.Point(12, 39)
        Me.mcbDistributor.Name = "mcbDistributor"
        Me.mcbDistributor.SelectedIndex = -1
        Me.mcbDistributor.SelectedItem = Nothing
        Me.mcbDistributor.Size = New System.Drawing.Size(185, 20)
        Me.mcbDistributor.TabIndex = 0
        Me.mcbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.mcbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(110, 22)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Ship To Territory"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Distributor"
        '
        'grpterritpry
        '
        Me.grpterritpry.BackColor = System.Drawing.Color.Transparent
        Me.grpterritpry.Controls.Add(Me.txtTerritoryDescription)
        Me.grpterritpry.Controls.Add(Me.txtTerritoryArea)
        Me.grpterritpry.Controls.Add(Me.txtTerritoryID)
        Me.grpterritpry.Controls.Add(Me.Label1)
        Me.grpterritpry.Controls.Add(Me.Label2)
        Me.grpterritpry.Controls.Add(Me.Label3)
        Me.grpterritpry.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpterritpry.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpterritpry.Location = New System.Drawing.Point(0, 0)
        Me.grpterritpry.Name = "grpterritpry"
        Me.grpterritpry.Size = New System.Drawing.Size(225, 182)
        Me.grpterritpry.TabIndex = 4
        Me.grpterritpry.Text = "Teritory Area"
        Me.grpterritpry.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtTerritoryArea
        '
        Me.txtTerritoryArea.Location = New System.Drawing.Point(12, 86)
        Me.txtTerritoryArea.MaxLength = 30
        Me.txtTerritoryArea.Name = "txtTerritoryArea"
        Me.txtTerritoryArea.Size = New System.Drawing.Size(207, 20)
        Me.txtTerritoryArea.TabIndex = 1
        '
        'txtTerritoryID
        '
        Me.txtTerritoryID.Location = New System.Drawing.Point(12, 46)
        Me.txtTerritoryID.MaxLength = 10
        Me.txtTerritoryID.Name = "txtTerritoryID"
        Me.txtTerritoryID.Size = New System.Drawing.Size(56, 20)
        Me.txtTerritoryID.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "TerritoryID"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Territory Area"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Teritory Description"
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.btnAdd)
        Me.PanelEx1.Controls.Add(Me.btnInsert)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx1.Location = New System.Drawing.Point(3, 570)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(225, 28)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.Color = System.Drawing.SystemColors.InactiveCaption
        Me.PanelEx1.Style.BackColor2.Color = System.Drawing.SystemColors.InactiveCaptionText
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.StyleMouseDown.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.PanelEx1.StyleMouseDown.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2
        Me.PanelEx1.StyleMouseDown.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder
        Me.PanelEx1.StyleMouseDown.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedText
        Me.PanelEx1.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.StyleMouseOver.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground
        Me.PanelEx1.StyleMouseOver.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground2
        Me.PanelEx1.StyleMouseOver.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBorder
        Me.PanelEx1.StyleMouseOver.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotText
        Me.PanelEx1.TabIndex = 3
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnAdd.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAdd.Location = New System.Drawing.Point(3, 3)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Add&New"
        Me.btnAdd.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnInsert
        '
        Me.btnInsert.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnInsert.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnInsert.Location = New System.Drawing.Point(153, 3)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(68, 23)
        Me.btnInsert.TabIndex = 1
        Me.btnInsert.Text = "&Insert"
        Me.btnInsert.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'pnlShip_To
        '
        Me.pnlShip_To.CanvasColor = System.Drawing.SystemColors.Control
        Me.pnlShip_To.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.pnlShip_To.Controls.Add(Me.grdShipTo)
        Me.pnlShip_To.Controls.Add(Me.Bar2)
        Me.pnlShip_To.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlShip_To.ExpandButtonVisible = False
        Me.pnlShip_To.Location = New System.Drawing.Point(239, 288)
        Me.pnlShip_To.Name = "pnlShip_To"
        Me.pnlShip_To.Size = New System.Drawing.Size(764, 367)
        Me.pnlShip_To.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlShip_To.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.pnlShip_To.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.pnlShip_To.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.pnlShip_To.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.pnlShip_To.Style.GradientAngle = 90
        Me.pnlShip_To.TabIndex = 27
        Me.pnlShip_To.TitleHeight = 15
        Me.pnlShip_To.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlShip_To.TitleStyle.BackColor1.Color = System.Drawing.SystemColors.InactiveCaption
        Me.pnlShip_To.TitleStyle.BackColor2.Color = System.Drawing.SystemColors.InactiveCaptionText
        Me.pnlShip_To.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.pnlShip_To.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.pnlShip_To.TitleStyle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlShip_To.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.pnlShip_To.TitleStyle.GradientAngle = 90
        Me.pnlShip_To.TitleText = "SHIP TO TERRITORY"
        '
        'grdShipTo
        '
        Me.grdShipTo.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdShipTo.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdShipTo.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdShipTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdShipTo.CardColumnHeaderFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.grdShipTo.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.grdShipTo.DataSource = Me.btnRefresh.SubItems
        Me.grdShipTo.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        grdShipTo_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("grdShipTo_DesignTimeLayout_Reference_0.Instance"), Object)
        'grdShipTo_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {grdShipTo_DesignTimeLayout_Reference_0})
        grdShipTo_DesignTimeLayout.LayoutString = resources.GetString("grdShipTo_DesignTimeLayout.LayoutString")
        Me.grdShipTo.DesignTimeLayout = grdShipTo_DesignTimeLayout
        Me.grdShipTo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdShipTo.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdShipTo.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdShipTo.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdShipTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdShipTo.GroupByBoxVisible = False
        Me.grdShipTo.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdShipTo.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdShipTo.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdShipTo.ImageList = Me.ImageList1
        Me.grdShipTo.Location = New System.Drawing.Point(0, 15)
        Me.grdShipTo.Name = "grdShipTo"
        Me.grdShipTo.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdShipTo.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdShipTo.RecordNavigator = True
        Me.grdShipTo.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdShipTo.Size = New System.Drawing.Size(764, 352)
        Me.grdShipTo.TabIndex = 0
        Me.grdShipTo.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.grdShipTo.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Bar2
        '
        Me.Bar2.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.Bar2.Location = New System.Drawing.Point(295, 151)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(75, 60)
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar2.TabIndex = 1
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.ExpandableControl = Me.grpTerritory
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
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(231, 54)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(8, 601)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 29
        Me.ExpandableSplitter1.TabStop = False
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.GridEX = Me.grdTerritory
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'PrintPreviewDialog2
        '
        Me.PrintPreviewDialog2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog2.ClientSize = New System.Drawing.Size(398, 298)
        Me.PrintPreviewDialog2.Document = Me.GridEXPrintDocument2
        Me.PrintPreviewDialog2.Enabled = True
        Me.PrintPreviewDialog2.Icon = CType(resources.GetObject("PrintPreviewDialog2.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog2.Name = "PrintPreviewDialog2"
        Me.PrintPreviewDialog2.Visible = False
        '
        'GridEXPrintDocument2
        '
        Me.GridEXPrintDocument2.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument2.GridEX = Me.grdShipTo
        Me.GridEXPrintDocument2.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument2.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument2.PrintCellBackground = False
        Me.GridEXPrintDocument2.PrintHierarchical = True
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.IncludeFormatStyle = False
        '
        'PageSetupDialog2
        '
        Me.PageSetupDialog2.Document = Me.GridEXPrintDocument2
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(398, 298)
        Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'Distributor_Ship_To
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1003, 655)
        Me.Controls.Add(Me.pnlShip_To)
        Me.Controls.Add(Me.pnlTerritory)
        Me.Controls.Add(Me.ExpandableSplitter1)
        Me.Controls.Add(Me.grpTerritory)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.mnuBar)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Distributor_Ship_To"
        Me.Text = "Distributor_Ship_To"
        CType(Me.mnuBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTerritory.ResumeLayout(False)
        CType(Me.grdTerritory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTerritory.ResumeLayout(False)
        Me.XpGradientPanel1.ResumeLayout(False)
        CType(Me.grpShipTo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpShipTo.ResumeLayout(False)
        Me.grpShipTo.PerformLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpterritpry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpterritpry.ResumeLayout(False)
        Me.grpterritpry.PerformLayout()
        Me.PanelEx1.ResumeLayout(False)
        Me.pnlShip_To.ResumeLayout(False)
        CType(Me.grdShipTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents mnuBar As DevComponents.DotNetBar.Bar
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRenameColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterDefault As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents pnlTerritory As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents grdTerritory As Janus.Windows.GridEX.GridEX
    Private WithEvents pnlShip_To As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents grpTerritory As System.Windows.Forms.GroupBox
    Private WithEvents grdShipTo As Janus.Windows.GridEX.GridEX
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Private WithEvents btnAdd As Janus.Windows.EditControls.UIButton
    Private WithEvents btnInsert As Janus.Windows.EditControls.UIButton
    Private WithEvents grpterritpry As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents grpShipTo As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents chkTerritory As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Private WithEvents mcbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents btnFilterShipTo As DTSProjects.ButtonSearch
    Private WithEvents btnFilterDistributor As DTSProjects.ButtonSearch
    Private WithEvents txtTerritoryID As System.Windows.Forms.TextBox
    Private WithEvents txtTerritoryArea As System.Windows.Forms.TextBox
    Private WithEvents txtTerritoryDescription As System.Windows.Forms.TextBox
    Friend WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Friend WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PrintPreviewDialog2 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents PageSetupDialog2 As System.Windows.Forms.PageSetupDialog
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXPrintDocument2 As Janus.Windows.GridEX.GridEXPrintDocument
End Class
