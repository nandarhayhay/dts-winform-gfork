<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Acceptance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Acceptance))
        Dim cmbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX2_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX3_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.cmbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.btnApply = New Janus.Windows.EditControls.UIButton
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnRenameColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSettings = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterEqual = New DevComponents.DotNetBar.ButtonItem
        Me.btnAddNew = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnEditOA = New DevComponents.DotNetBar.ButtonItem
        Me.btnChooseOA = New DevComponents.DotNetBar.ButtonItem
        Me.ItemContainer1 = New DevComponents.DotNetBar.ItemContainer
        Me.cmbOA = New DevComponents.DotNetBar.ComboBoxItem
        Me.btnApplyEdit = New DevComponents.DotNetBar.ButtonItem
        Me.btnCurrentYear = New DevComponents.DotNetBar.ButtonItem
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.GridEX2 = New Janus.Windows.GridEX.GridEX
        Me.NavigationPane1 = New DevComponents.DotNetBar.NavigationPane
        Me.NavigationPanePanel4 = New DevComponents.DotNetBar.NavigationPanePanel
        Me.ExpandablePanel3 = New DevComponents.DotNetBar.ExpandablePanel
        Me.rdbUncategorized = New System.Windows.Forms.RadioButton
        Me.rdbCBD = New System.Windows.Forms.RadioButton
        Me.rdbDR = New System.Windows.Forms.RadioButton
        Me.rdbDD = New System.Windows.Forms.RadioButton
        Me.btnOtherDiscount = New DevComponents.DotNetBar.ButtonItem
        Me.pnlOABrandPack = New DevComponents.DotNetBar.NavigationPanePanel
        Me.ExpandablePanel4 = New DevComponents.DotNetBar.ExpandablePanel
        Me.NavigationPanePanel3 = New DevComponents.DotNetBar.NavigationPanePanel
        Me.ExpandablePanel7 = New DevComponents.DotNetBar.ExpandablePanel
        Me.btnProjectDiscount = New DevComponents.DotNetBar.ButtonItem
        Me.NavigationPanePanel2 = New DevComponents.DotNetBar.NavigationPanePanel
        Me.ExpandablePanel6 = New DevComponents.DotNetBar.ExpandablePanel
        Me.rdbCPDAuto = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbGivenDKN = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbCPMRT = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbSpecialCPD = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbGivenCPR = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbGiven_DK = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbGiven_PKPP = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbGiven_CP = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbMarketingTarget = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbMarketingStepping = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbGiven = New Janus.Windows.EditControls.UIRadioButton
        Me.btnPnlMarketingDiscount = New DevComponents.DotNetBar.ButtonItem
        Me.NavigationPanePanel1 = New DevComponents.DotNetBar.NavigationPanePanel
        Me.ExpandablePanel5 = New DevComponents.DotNetBar.ExpandablePanel
        Me.rdbYearlyDiscount = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbSemesterly = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbQuarterlyDiscount = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbGivenDiscount = New Janus.Windows.EditControls.UIRadioButton
        Me.btnPnlAgreement = New DevComponents.DotNetBar.ButtonItem
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.Bar3 = New DevComponents.DotNetBar.Bar
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.GridEXPrintDocument2 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.PageSetupDialog2 = New System.Windows.Forms.PageSetupDialog
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.PrintPreviewDialog2 = New System.Windows.Forms.PrintPreviewDialog
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.grpRangeDate = New Janus.Windows.EditControls.UIGroupBox
        Me.btnAplyRange = New Janus.Windows.EditControls.UIButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.GridEX3 = New Janus.Windows.GridEX.GridEX
        Me.spliterOA = New DevComponents.DotNetBar.ExpandableSplitter
        Me.PanelEx1.SuspendLayout()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Bar2.SuspendLayout()
        CType(Me.cmbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEX2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NavigationPane1.SuspendLayout()
        Me.NavigationPanePanel4.SuspendLayout()
        Me.ExpandablePanel3.SuspendLayout()
        Me.pnlOABrandPack.SuspendLayout()
        Me.NavigationPanePanel3.SuspendLayout()
        Me.NavigationPanePanel2.SuspendLayout()
        Me.ExpandablePanel6.SuspendLayout()
        Me.NavigationPanePanel1.SuspendLayout()
        Me.ExpandablePanel5.SuspendLayout()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRangeDate.SuspendLayout()
        CType(Me.GridEX3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "")
        Me.ImageList1.Images.SetKeyName(9, "")
        Me.ImageList1.Images.SetKeyName(10, "")
        Me.ImageList1.Images.SetKeyName(11, "")
        Me.ImageList1.Images.SetKeyName(12, "")
        Me.ImageList1.Images.SetKeyName(13, "")
        Me.ImageList1.Images.SetKeyName(14, "")
        Me.ImageList1.Images.SetKeyName(15, "")
        Me.ImageList1.Images.SetKeyName(16, "")
        Me.ImageList1.Images.SetKeyName(17, "")
        Me.ImageList1.Images.SetKeyName(18, "Brand.ico")
        Me.ImageList1.Images.SetKeyName(19, "program.ico")
        Me.ImageList1.Images.SetKeyName(20, "Agreement.ICO")
        Me.ImageList1.Images.SetKeyName(21, "Project.ico")
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.Bar2)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(1024, 28)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
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
        Me.PanelEx1.TabIndex = 6
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar2 (Bar2)"
        Me.Bar2.AccessibleName = "Bar2"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaptionText
        Me.Bar2.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaption
        Me.Bar2.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.Bar2.Controls.Add(Me.cmbDistributor)
        Me.Bar2.Controls.Add(Me.btnApply)
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.DockSide = DevComponents.DotNetBar.eDockSide.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnAddNew, Me.btnExport, Me.btnRefresh, Me.btnEditOA})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1024, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 20
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'cmbDistributor
        '
        Me.cmbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        cmbDistributor_DesignTimeLayout.LayoutString = resources.GetString("cmbDistributor_DesignTimeLayout.LayoutString")
        Me.cmbDistributor.DesignTimeLayout = cmbDistributor_DesignTimeLayout
        Me.cmbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.cmbDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.cmbDistributor.Location = New System.Drawing.Point(483, 2)
        Me.cmbDistributor.Name = "cmbDistributor"
        Me.cmbDistributor.SelectedIndex = -1
        Me.cmbDistributor.SelectedItem = Nothing
        Me.cmbDistributor.Size = New System.Drawing.Size(252, 23)
        Me.cmbDistributor.TabIndex = 2
        Me.cmbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.cmbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnApply
        '
        Me.btnApply.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnApply.ImageIndex = 3
        Me.btnApply.ImageList = Me.ImageList1
        Me.btnApply.Location = New System.Drawing.Point(741, 2)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnApply.Size = New System.Drawing.Size(90, 20)
        Me.btnApply.TabIndex = 1
        Me.btnApply.Text = "Apply Filter"
        Me.btnApply.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
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
        Me.btnFilterEqual.Tooltip = "use this filter to filter data in data grid directly from datagrid with Contains " & _
            "criteria"
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
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 14
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "E&xport Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
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
        'btnEditOA
        '
        Me.btnEditOA.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnEditOA.ImageIndex = 5
        Me.btnEditOA.Name = "btnEditOA"
        Me.btnEditOA.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnChooseOA})
        Me.btnEditOA.Text = "Edit O A"
        '
        'btnChooseOA
        '
        Me.btnChooseOA.Name = "btnChooseOA"
        Me.btnChooseOA.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ItemContainer1})
        Me.btnChooseOA.Text = "Choose OA"
        '
        'ItemContainer1
        '
        Me.ItemContainer1.MinimumSize = New System.Drawing.Size(0, 0)
        Me.ItemContainer1.Name = "ItemContainer1"
        Me.ItemContainer1.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.cmbOA, Me.btnApplyEdit})
        '
        'cmbOA
        '
        Me.cmbOA.ComboWidth = 200
        Me.cmbOA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
        Me.cmbOA.ItemHeight = 14
        Me.cmbOA.Name = "cmbOA"
        '
        'btnApplyEdit
        '
        Me.btnApplyEdit.Name = "btnApplyEdit"
        Me.btnApplyEdit.Text = "Edit"
        '
        'btnCurrentYear
        '
        Me.btnCurrentYear.Name = "btnCurrentYear"
        Me.btnCurrentYear.Text = "Current Year"
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(160, 277)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(864, 336)
        Me.GridEX1.TabIndex = 7
        Me.GridEX1.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEX1.TableHeaderFormatStyle.FontSize = 9.0!
        Me.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEX1.TotalRowFormatStyle.FontSize = 8.0!
        Me.GridEX1.TotalRowFormatStyle.FontUnderline = Janus.Windows.GridEX.TriState.[True]
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'GridEX2
        '
        Me.GridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX2.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX2.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX2_DesignTimeLayout.LayoutString = resources.GetString("GridEX2_DesignTimeLayout.LayoutString")
        Me.GridEX2.DesignTimeLayout = GridEX2_DesignTimeLayout
        Me.GridEX2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GridEX2.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX2.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX2.GroupByBoxVisible = False
        Me.GridEX2.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX2.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX2.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX2.ImageList = Me.ImageList1
        Me.GridEX2.Location = New System.Drawing.Point(160, 620)
        Me.GridEX2.Name = "GridEX2"
        Me.GridEX2.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX2.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX2.RecordNavigator = True
        Me.GridEX2.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX2.Size = New System.Drawing.Size(864, 122)
        Me.GridEX2.TabIndex = 8
        Me.GridEX2.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEX2.TableHeaderFormatStyle.FontSize = 9.0!
        Me.GridEX2.TableHeaderFormatStyle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.GridEX2.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX2.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEX2.TotalRowFormatStyle.FontSize = 8.0!
        Me.GridEX2.TotalRowFormatStyle.FontUnderline = Janus.Windows.GridEX.TriState.[True]
        Me.GridEX2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.GridEX2.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'NavigationPane1
        '
        Me.NavigationPane1.CanCollapse = True
        Me.NavigationPane1.Controls.Add(Me.NavigationPanePanel4)
        Me.NavigationPane1.Controls.Add(Me.pnlOABrandPack)
        Me.NavigationPane1.Controls.Add(Me.NavigationPanePanel3)
        Me.NavigationPane1.Controls.Add(Me.NavigationPanePanel2)
        Me.NavigationPane1.Controls.Add(Me.NavigationPanePanel1)
        Me.NavigationPane1.Dock = System.Windows.Forms.DockStyle.Left
        Me.NavigationPane1.Images = Me.ImageList1
        Me.NavigationPane1.ItemPaddingBottom = 2
        Me.NavigationPane1.ItemPaddingTop = 2
        Me.NavigationPane1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnPnlAgreement, Me.btnPnlMarketingDiscount, Me.btnProjectDiscount, Me.btnOtherDiscount})
        Me.NavigationPane1.Location = New System.Drawing.Point(0, 28)
        Me.NavigationPane1.Name = "NavigationPane1"
        Me.NavigationPane1.Padding = New System.Windows.Forms.Padding(1)
        Me.NavigationPane1.Size = New System.Drawing.Size(160, 714)
        Me.NavigationPane1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.NavigationPane1.TabIndex = 11
        '
        '
        '
        Me.NavigationPane1.TitlePanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.NavigationPane1.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.NavigationPane1.TitlePanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NavigationPane1.TitlePanel.Location = New System.Drawing.Point(1, 1)
        Me.NavigationPane1.TitlePanel.Name = "panelTitle"
        Me.NavigationPane1.TitlePanel.Size = New System.Drawing.Size(158, 24)
        Me.NavigationPane1.TitlePanel.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.NavigationPane1.TitlePanel.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.NavigationPane1.TitlePanel.Style.Border = DevComponents.DotNetBar.eBorderType.RaisedInner
        Me.NavigationPane1.TitlePanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.NavigationPane1.TitlePanel.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Top) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.NavigationPane1.TitlePanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.NavigationPane1.TitlePanel.Style.GradientAngle = 90
        Me.NavigationPane1.TitlePanel.Style.MarginLeft = 4
        Me.NavigationPane1.TitlePanel.TabIndex = 0
        Me.NavigationPane1.TitlePanel.Text = "Other Discount"
        '
        'NavigationPanePanel4
        '
        Me.NavigationPanePanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.NavigationPanePanel4.Controls.Add(Me.ExpandablePanel3)
        Me.NavigationPanePanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NavigationPanePanel4.Location = New System.Drawing.Point(1, 25)
        Me.NavigationPanePanel4.Name = "NavigationPanePanel4"
        Me.NavigationPanePanel4.ParentItem = Me.btnOtherDiscount
        Me.NavigationPanePanel4.Size = New System.Drawing.Size(158, 656)
        Me.NavigationPanePanel4.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.NavigationPanePanel4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.NavigationPanePanel4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.NavigationPanePanel4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.NavigationPanePanel4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.NavigationPanePanel4.Style.GradientAngle = 90
        Me.NavigationPanePanel4.TabIndex = 6
        '
        'ExpandablePanel3
        '
        Me.ExpandablePanel3.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel3.Controls.Add(Me.rdbUncategorized)
        Me.ExpandablePanel3.Controls.Add(Me.rdbCBD)
        Me.ExpandablePanel3.Controls.Add(Me.rdbDR)
        Me.ExpandablePanel3.Controls.Add(Me.rdbDD)
        Me.ExpandablePanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExpandablePanel3.ExpandButtonVisible = False
        Me.ExpandablePanel3.Location = New System.Drawing.Point(0, 0)
        Me.ExpandablePanel3.Name = "ExpandablePanel3"
        Me.ExpandablePanel3.Size = New System.Drawing.Size(158, 656)
        Me.ExpandablePanel3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ExpandablePanel3.Style.BackColor2.Color = System.Drawing.SystemColors.MenuBar
        Me.ExpandablePanel3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel3.Style.GradientAngle = 90
        Me.ExpandablePanel3.TabIndex = 10
        Me.ExpandablePanel3.TitleHeight = 18
        Me.ExpandablePanel3.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel3.TitleStyle.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ExpandablePanel3.TitleStyle.BackColor2.Color = System.Drawing.SystemColors.MenuBar
        Me.ExpandablePanel3.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel3.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel3.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel3.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel3.TitleText = "Current View"
        '
        'rdbUncategorized
        '
        Me.rdbUncategorized.AutoSize = True
        Me.rdbUncategorized.Dock = System.Windows.Forms.DockStyle.Top
        Me.rdbUncategorized.Location = New System.Drawing.Point(0, 69)
        Me.rdbUncategorized.Name = "rdbUncategorized"
        Me.rdbUncategorized.Size = New System.Drawing.Size(158, 17)
        Me.rdbUncategorized.TabIndex = 4
        Me.rdbUncategorized.TabStop = True
        Me.rdbUncategorized.Text = "Uncategorized"
        Me.rdbUncategorized.UseVisualStyleBackColor = True
        '
        'rdbCBD
        '
        Me.rdbCBD.AutoSize = True
        Me.rdbCBD.Dock = System.Windows.Forms.DockStyle.Top
        Me.rdbCBD.Location = New System.Drawing.Point(0, 52)
        Me.rdbCBD.Name = "rdbCBD"
        Me.rdbCBD.Size = New System.Drawing.Size(158, 17)
        Me.rdbCBD.TabIndex = 3
        Me.rdbCBD.TabStop = True
        Me.rdbCBD.Text = "CBD"
        Me.rdbCBD.UseVisualStyleBackColor = True
        '
        'rdbDR
        '
        Me.rdbDR.AutoSize = True
        Me.rdbDR.Dock = System.Windows.Forms.DockStyle.Top
        Me.rdbDR.Location = New System.Drawing.Point(0, 35)
        Me.rdbDR.Name = "rdbDR"
        Me.rdbDR.Size = New System.Drawing.Size(158, 17)
        Me.rdbDR.TabIndex = 2
        Me.rdbDR.TabStop = True
        Me.rdbDR.Text = "D R"
        Me.rdbDR.UseVisualStyleBackColor = True
        '
        'rdbDD
        '
        Me.rdbDD.AutoSize = True
        Me.rdbDD.Dock = System.Windows.Forms.DockStyle.Top
        Me.rdbDD.Location = New System.Drawing.Point(0, 18)
        Me.rdbDD.Name = "rdbDD"
        Me.rdbDD.Size = New System.Drawing.Size(158, 17)
        Me.rdbDD.TabIndex = 1
        Me.rdbDD.TabStop = True
        Me.rdbDD.Text = "D D"
        Me.rdbDD.UseVisualStyleBackColor = True
        '
        'btnOtherDiscount
        '
        Me.btnOtherDiscount.Checked = True
        Me.btnOtherDiscount.Image = CType(resources.GetObject("btnOtherDiscount.Image"), System.Drawing.Image)
        Me.btnOtherDiscount.ImageFixedSize = New System.Drawing.Size(16, 16)
        Me.btnOtherDiscount.Name = "btnOtherDiscount"
        Me.btnOtherDiscount.OptionGroup = "navBar"
        Me.btnOtherDiscount.Text = "Other Discount"
        '
        'pnlOABrandPack
        '
        Me.pnlOABrandPack.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.pnlOABrandPack.Controls.Add(Me.ExpandablePanel4)
        Me.pnlOABrandPack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlOABrandPack.Location = New System.Drawing.Point(1, 1)
        Me.pnlOABrandPack.Name = "pnlOABrandPack"
        Me.pnlOABrandPack.ParentItem = Nothing
        Me.pnlOABrandPack.Size = New System.Drawing.Size(158, 680)
        Me.pnlOABrandPack.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlOABrandPack.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.pnlOABrandPack.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.pnlOABrandPack.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.pnlOABrandPack.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.pnlOABrandPack.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.pnlOABrandPack.Style.GradientAngle = 90
        Me.pnlOABrandPack.TabIndex = 2
        '
        'ExpandablePanel4
        '
        Me.ExpandablePanel4.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExpandablePanel4.ExpandButtonVisible = False
        Me.ExpandablePanel4.Location = New System.Drawing.Point(0, 0)
        Me.ExpandablePanel4.Name = "ExpandablePanel4"
        Me.ExpandablePanel4.Size = New System.Drawing.Size(158, 680)
        Me.ExpandablePanel4.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandablePanel4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.ExpandablePanel4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel4.Style.GradientAngle = 90
        Me.ExpandablePanel4.TabIndex = 8
        Me.ExpandablePanel4.TitleHeight = 18
        Me.ExpandablePanel4.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel4.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandablePanel4.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel4.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel4.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel4.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel4.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel4.TitleText = "Current View"
        '
        'NavigationPanePanel3
        '
        Me.NavigationPanePanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.NavigationPanePanel3.Controls.Add(Me.ExpandablePanel7)
        Me.NavigationPanePanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NavigationPanePanel3.Location = New System.Drawing.Point(1, 1)
        Me.NavigationPanePanel3.Name = "NavigationPanePanel3"
        Me.NavigationPanePanel3.ParentItem = Me.btnProjectDiscount
        Me.NavigationPanePanel3.Size = New System.Drawing.Size(158, 680)
        Me.NavigationPanePanel3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.NavigationPanePanel3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.NavigationPanePanel3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.NavigationPanePanel3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.NavigationPanePanel3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.NavigationPanePanel3.Style.GradientAngle = 90
        Me.NavigationPanePanel3.TabIndex = 5
        '
        'ExpandablePanel7
        '
        Me.ExpandablePanel7.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExpandablePanel7.ExpandButtonVisible = False
        Me.ExpandablePanel7.Location = New System.Drawing.Point(0, 0)
        Me.ExpandablePanel7.Name = "ExpandablePanel7"
        Me.ExpandablePanel7.Size = New System.Drawing.Size(158, 680)
        Me.ExpandablePanel7.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel7.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ExpandablePanel7.Style.BackColor2.Color = System.Drawing.SystemColors.MenuBar
        Me.ExpandablePanel7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel7.Style.GradientAngle = 90
        Me.ExpandablePanel7.TabIndex = 9
        Me.ExpandablePanel7.TitleHeight = 18
        Me.ExpandablePanel7.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel7.TitleStyle.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ExpandablePanel7.TitleStyle.BackColor2.Color = System.Drawing.SystemColors.MenuBar
        Me.ExpandablePanel7.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel7.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel7.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel7.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel7.TitleText = "Current View"
        '
        'btnProjectDiscount
        '
        Me.btnProjectDiscount.ImageFixedSize = New System.Drawing.Size(16, 16)
        Me.btnProjectDiscount.ImageIndex = 21
        Me.btnProjectDiscount.Name = "btnProjectDiscount"
        Me.btnProjectDiscount.OptionGroup = "navBar"
        Me.btnProjectDiscount.Text = "Project Discount"
        Me.btnProjectDiscount.Visible = False
        '
        'NavigationPanePanel2
        '
        Me.NavigationPanePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.NavigationPanePanel2.Controls.Add(Me.ExpandablePanel6)
        Me.NavigationPanePanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NavigationPanePanel2.Location = New System.Drawing.Point(1, 1)
        Me.NavigationPanePanel2.Name = "NavigationPanePanel2"
        Me.NavigationPanePanel2.ParentItem = Me.btnPnlMarketingDiscount
        Me.NavigationPanePanel2.Size = New System.Drawing.Size(158, 680)
        Me.NavigationPanePanel2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.NavigationPanePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.NavigationPanePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.NavigationPanePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.NavigationPanePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.NavigationPanePanel2.Style.GradientAngle = 90
        Me.NavigationPanePanel2.TabIndex = 4
        '
        'ExpandablePanel6
        '
        Me.ExpandablePanel6.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel6.Controls.Add(Me.rdbCPDAuto)
        Me.ExpandablePanel6.Controls.Add(Me.rdbGivenDKN)
        Me.ExpandablePanel6.Controls.Add(Me.rdbCPMRT)
        Me.ExpandablePanel6.Controls.Add(Me.rdbSpecialCPD)
        Me.ExpandablePanel6.Controls.Add(Me.rdbGivenCPR)
        Me.ExpandablePanel6.Controls.Add(Me.rdbGiven_DK)
        Me.ExpandablePanel6.Controls.Add(Me.rdbGiven_PKPP)
        Me.ExpandablePanel6.Controls.Add(Me.rdbGiven_CP)
        Me.ExpandablePanel6.Controls.Add(Me.rdbMarketingTarget)
        Me.ExpandablePanel6.Controls.Add(Me.rdbMarketingStepping)
        Me.ExpandablePanel6.Controls.Add(Me.rdbGiven)
        Me.ExpandablePanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExpandablePanel6.ExpandButtonVisible = False
        Me.ExpandablePanel6.Location = New System.Drawing.Point(0, 0)
        Me.ExpandablePanel6.Name = "ExpandablePanel6"
        Me.ExpandablePanel6.Size = New System.Drawing.Size(158, 680)
        Me.ExpandablePanel6.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel6.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ExpandablePanel6.Style.BackColor2.Color = System.Drawing.SystemColors.MenuBar
        Me.ExpandablePanel6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel6.Style.GradientAngle = 90
        Me.ExpandablePanel6.TabIndex = 9
        Me.ExpandablePanel6.TitleHeight = 18
        Me.ExpandablePanel6.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel6.TitleStyle.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ExpandablePanel6.TitleStyle.BackColor2.Color = System.Drawing.SystemColors.MenuBar
        Me.ExpandablePanel6.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel6.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel6.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel6.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel6.TitleText = "Current View"
        '
        'rdbCPDAuto
        '
        Me.rdbCPDAuto.Location = New System.Drawing.Point(18, 265)
        Me.rdbCPDAuto.Name = "rdbCPDAuto"
        Me.rdbCPDAuto.Size = New System.Drawing.Size(107, 16)
        Me.rdbCPDAuto.TabIndex = 14
        Me.rdbCPDAuto.Text = "CP(D)AUTO"
        Me.rdbCPDAuto.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbGivenDKN
        '
        Me.rdbGivenDKN.Location = New System.Drawing.Point(18, 233)
        Me.rdbGivenDKN.Name = "rdbGivenDKN"
        Me.rdbGivenDKN.Size = New System.Drawing.Size(104, 24)
        Me.rdbGivenDKN.TabIndex = 13
        Me.rdbGivenDKN.Text = "Given_DK(N)"
        Me.rdbGivenDKN.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbCPMRT
        '
        Me.rdbCPMRT.Location = New System.Drawing.Point(18, 113)
        Me.rdbCPMRT.Name = "rdbCPMRT"
        Me.rdbCPMRT.Size = New System.Drawing.Size(104, 23)
        Me.rdbCPMRT.TabIndex = 12
        Me.rdbCPMRT.Text = "CP(R M/T)"
        Me.rdbCPMRT.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbSpecialCPD
        '
        Me.rdbSpecialCPD.Location = New System.Drawing.Point(18, 84)
        Me.rdbSpecialCPD.Name = "rdbSpecialCPD"
        Me.rdbSpecialCPD.Size = New System.Drawing.Size(104, 23)
        Me.rdbSpecialCPD.TabIndex = 11
        Me.rdbSpecialCPD.Text = "CP(D)S"
        Me.rdbSpecialCPD.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbGivenCPR
        '
        Me.rdbGivenCPR.Location = New System.Drawing.Point(18, 143)
        Me.rdbGivenCPR.Name = "rdbGivenCPR"
        Me.rdbGivenCPR.Size = New System.Drawing.Size(104, 23)
        Me.rdbGivenCPR.TabIndex = 10
        Me.rdbGivenCPR.Text = "Given_CP(R)"
        Me.rdbGivenCPR.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbGiven_DK
        '
        Me.rdbGiven_DK.Location = New System.Drawing.Point(18, 203)
        Me.rdbGiven_DK.Name = "rdbGiven_DK"
        Me.rdbGiven_DK.Size = New System.Drawing.Size(104, 24)
        Me.rdbGiven_DK.TabIndex = 9
        Me.rdbGiven_DK.Text = "Given_DK"
        Me.rdbGiven_DK.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbGiven_PKPP
        '
        Me.rdbGiven_PKPP.Location = New System.Drawing.Point(18, 176)
        Me.rdbGiven_PKPP.Name = "rdbGiven_PKPP"
        Me.rdbGiven_PKPP.Size = New System.Drawing.Size(104, 18)
        Me.rdbGiven_PKPP.TabIndex = 8
        Me.rdbGiven_PKPP.Text = "Given_PKPP"
        Me.rdbGiven_PKPP.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbGiven_CP
        '
        Me.rdbGiven_CP.Location = New System.Drawing.Point(18, 54)
        Me.rdbGiven_CP.Name = "rdbGiven_CP"
        Me.rdbGiven_CP.Size = New System.Drawing.Size(104, 23)
        Me.rdbGiven_CP.TabIndex = 7
        Me.rdbGiven_CP.Text = "Given_CP(D)"
        Me.rdbGiven_CP.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbMarketingTarget
        '
        Me.rdbMarketingTarget.Location = New System.Drawing.Point(18, 295)
        Me.rdbMarketingTarget.Name = "rdbMarketingTarget"
        Me.rdbMarketingTarget.Size = New System.Drawing.Size(107, 16)
        Me.rdbMarketingTarget.TabIndex = 6
        Me.rdbMarketingTarget.Text = "Target Discount"
        Me.rdbMarketingTarget.Visible = False
        Me.rdbMarketingTarget.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbMarketingStepping
        '
        Me.rdbMarketingStepping.Location = New System.Drawing.Point(18, 325)
        Me.rdbMarketingStepping.Name = "rdbMarketingStepping"
        Me.rdbMarketingStepping.Size = New System.Drawing.Size(107, 16)
        Me.rdbMarketingStepping.TabIndex = 5
        Me.rdbMarketingStepping.Text = "Stepping Discount"
        Me.rdbMarketingStepping.Visible = False
        Me.rdbMarketingStepping.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbGiven
        '
        Me.rdbGiven.Location = New System.Drawing.Point(18, 28)
        Me.rdbGiven.Name = "rdbGiven"
        Me.rdbGiven.Size = New System.Drawing.Size(107, 16)
        Me.rdbGiven.TabIndex = 4
        Me.rdbGiven.Text = "Given DPRD"
        Me.rdbGiven.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnPnlMarketingDiscount
        '
        Me.btnPnlMarketingDiscount.ImageFixedSize = New System.Drawing.Size(16, 16)
        Me.btnPnlMarketingDiscount.ImageIndex = 19
        Me.btnPnlMarketingDiscount.Name = "btnPnlMarketingDiscount"
        Me.btnPnlMarketingDiscount.OptionGroup = "navBar"
        Me.btnPnlMarketingDiscount.Text = "Sales Discount"
        '
        'NavigationPanePanel1
        '
        Me.NavigationPanePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.NavigationPanePanel1.Controls.Add(Me.ExpandablePanel5)
        Me.NavigationPanePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NavigationPanePanel1.Location = New System.Drawing.Point(1, 1)
        Me.NavigationPanePanel1.Name = "NavigationPanePanel1"
        Me.NavigationPanePanel1.ParentItem = Me.btnPnlAgreement
        Me.NavigationPanePanel1.Size = New System.Drawing.Size(158, 680)
        Me.NavigationPanePanel1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.NavigationPanePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.NavigationPanePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.NavigationPanePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.NavigationPanePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.NavigationPanePanel1.Style.GradientAngle = 90
        Me.NavigationPanePanel1.TabIndex = 3
        '
        'ExpandablePanel5
        '
        Me.ExpandablePanel5.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel5.Controls.Add(Me.rdbYearlyDiscount)
        Me.ExpandablePanel5.Controls.Add(Me.rdbSemesterly)
        Me.ExpandablePanel5.Controls.Add(Me.rdbQuarterlyDiscount)
        Me.ExpandablePanel5.Controls.Add(Me.rdbGivenDiscount)
        Me.ExpandablePanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExpandablePanel5.ExpandButtonVisible = False
        Me.ExpandablePanel5.Location = New System.Drawing.Point(0, 0)
        Me.ExpandablePanel5.Name = "ExpandablePanel5"
        Me.ExpandablePanel5.Size = New System.Drawing.Size(158, 680)
        Me.ExpandablePanel5.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel5.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ExpandablePanel5.Style.BackColor2.Color = System.Drawing.SystemColors.MenuBar
        Me.ExpandablePanel5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel5.Style.GradientAngle = 90
        Me.ExpandablePanel5.TabIndex = 9
        Me.ExpandablePanel5.TitleHeight = 18
        Me.ExpandablePanel5.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel5.TitleStyle.BackColor1.Color = System.Drawing.SystemColors.MenuBar
        Me.ExpandablePanel5.TitleStyle.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ExpandablePanel5.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel5.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel5.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel5.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel5.TitleText = "Current View"
        '
        'rdbYearlyDiscount
        '
        Me.rdbYearlyDiscount.Location = New System.Drawing.Point(11, 112)
        Me.rdbYearlyDiscount.Name = "rdbYearlyDiscount"
        Me.rdbYearlyDiscount.Size = New System.Drawing.Size(132, 16)
        Me.rdbYearlyDiscount.TabIndex = 4
        Me.rdbYearlyDiscount.Text = "Yearly Discount"
        Me.rdbYearlyDiscount.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbSemesterly
        '
        Me.rdbSemesterly.Location = New System.Drawing.Point(11, 81)
        Me.rdbSemesterly.Name = "rdbSemesterly"
        Me.rdbSemesterly.Size = New System.Drawing.Size(132, 16)
        Me.rdbSemesterly.TabIndex = 3
        Me.rdbSemesterly.Text = "Semesterly Discount"
        Me.rdbSemesterly.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbQuarterlyDiscount
        '
        Me.rdbQuarterlyDiscount.Location = New System.Drawing.Point(11, 53)
        Me.rdbQuarterlyDiscount.Name = "rdbQuarterlyDiscount"
        Me.rdbQuarterlyDiscount.Size = New System.Drawing.Size(107, 16)
        Me.rdbQuarterlyDiscount.TabIndex = 2
        Me.rdbQuarterlyDiscount.Text = "Quarterly Discount"
        Me.rdbQuarterlyDiscount.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbGivenDiscount
        '
        Me.rdbGivenDiscount.Location = New System.Drawing.Point(11, 27)
        Me.rdbGivenDiscount.Name = "rdbGivenDiscount"
        Me.rdbGivenDiscount.Size = New System.Drawing.Size(107, 16)
        Me.rdbGivenDiscount.TabIndex = 1
        Me.rdbGivenDiscount.Text = "Given Discount"
        Me.rdbGivenDiscount.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnPnlAgreement
        '
        Me.btnPnlAgreement.ImageFixedSize = New System.Drawing.Size(16, 16)
        Me.btnPnlAgreement.ImageIndex = 20
        Me.btnPnlAgreement.Name = "btnPnlAgreement"
        Me.btnPnlAgreement.OptionGroup = "navBar"
        Me.btnPnlAgreement.Text = "Agreement Discount"
        '
        'Bar1
        '
        Me.Bar1.Location = New System.Drawing.Point(386, 346)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(45, 25)
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar1.TabIndex = 10
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'Bar3
        '
        Me.Bar3.Location = New System.Drawing.Point(493, 657)
        Me.Bar3.Name = "Bar3"
        Me.Bar3.Size = New System.Drawing.Size(49, 25)
        Me.Bar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar3.TabIndex = 11
        Me.Bar3.TabStop = False
        Me.Bar3.Text = "Bar3"
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.IncludeChildTables = True
        Me.GridEXExporter1.IncludeFormatStyle = False
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.GridEX = Me.GridEX1
        Me.GridEXPrintDocument1.PageFooterFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEXPrintDocument1.PageFooterFormatStyle.FontSize = 10.0!
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'GridEXPrintDocument2
        '
        Me.GridEXPrintDocument2.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument2.PageFooterFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEXPrintDocument2.PageFooterFormatStyle.FontSize = 10.0!
        Me.GridEXPrintDocument2.PageHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEXPrintDocument2.PrintCellBackground = False
        Me.GridEXPrintDocument2.PrintHierarchical = True
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
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
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Contains
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(160, 28)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(0, 0)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(864, 29)
        Me.FilterEditor1.SortFieldList = False
        '
        'grpRangeDate
        '
        Me.grpRangeDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grpRangeDate.Controls.Add(Me.btnAplyRange)
        Me.grpRangeDate.Controls.Add(Me.Label2)
        Me.grpRangeDate.Controls.Add(Me.Label1)
        Me.grpRangeDate.Controls.Add(Me.dtpicUntil)
        Me.grpRangeDate.Controls.Add(Me.dtPicFrom)
        Me.grpRangeDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpRangeDate.Location = New System.Drawing.Point(160, 57)
        Me.grpRangeDate.Name = "grpRangeDate"
        Me.grpRangeDate.Size = New System.Drawing.Size(864, 43)
        Me.grpRangeDate.TabIndex = 23
        Me.grpRangeDate.Text = "OA RANGE DATE"
        Me.grpRangeDate.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'btnAplyRange
        '
        Me.btnAplyRange.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAplyRange.Location = New System.Drawing.Point(687, 12)
        Me.btnAplyRange.Name = "btnAplyRange"
        Me.btnAplyRange.Size = New System.Drawing.Size(75, 23)
        Me.btnAplyRange.TabIndex = 4
        Me.btnAplyRange.Text = "Apply filter"
        Me.btnAplyRange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(425, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "UNTIL"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(171, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "FROM"
        '
        'dtpicUntil
        '
        Me.dtpicUntil.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtpicUntil.Checked = False
        Me.dtpicUntil.CustomFormat = "dd MMMM yyyy"
        Me.dtpicUntil.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpicUntil.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtpicUntil.DropDownCalendar.FirstMonth = New Date(2008, 5, 1, 0, 0, 0, 0)
        Me.dtpicUntil.DropDownCalendar.Name = ""
        Me.dtpicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtpicUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtpicUntil.Location = New System.Drawing.Point(473, 13)
        Me.dtpicUntil.Name = "dtpicUntil"
        Me.dtpicUntil.ShowTodayButton = False
        Me.dtpicUntil.Size = New System.Drawing.Size(200, 20)
        Me.dtpicUntil.TabIndex = 1
        Me.dtpicUntil.Value = New Date(2008, 5, 26, 0, 0, 0, 0)
        Me.dtpicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicFrom
        '
        Me.dtPicFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFrom.Checked = False
        Me.dtPicFrom.CustomFormat = "dd MMMM yyyy"
        Me.dtPicFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicFrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFrom.DropDownCalendar.FirstMonth = New Date(2008, 5, 1, 0, 0, 0, 0)
        Me.dtPicFrom.DropDownCalendar.Name = ""
        Me.dtPicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicFrom.Location = New System.Drawing.Point(215, 13)
        Me.dtPicFrom.Name = "dtPicFrom"
        Me.dtPicFrom.ShowTodayButton = False
        Me.dtPicFrom.Size = New System.Drawing.Size(200, 20)
        Me.dtPicFrom.TabIndex = 0
        Me.dtPicFrom.Value = New Date(2008, 5, 26, 0, 0, 0, 0)
        Me.dtPicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ExpandableSplitter1.ExpandableControl = Me.GridEX2
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
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(160, 613)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(864, 7)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 0
        Me.ExpandableSplitter1.TabStop = False
        '
        'GridEX3
        '
        Me.GridEX3.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX3.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX3.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX3.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX3.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX3_DesignTimeLayout.LayoutString = resources.GetString("GridEX3_DesignTimeLayout.LayoutString")
        Me.GridEX3.DesignTimeLayout = GridEX3_DesignTimeLayout
        Me.GridEX3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GridEX3.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX3.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX3.GroupByBoxVisible = False
        Me.GridEX3.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX3.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX3.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX3.ImageList = Me.ImageList1
        Me.GridEX3.Location = New System.Drawing.Point(160, 100)
        Me.GridEX3.Name = "GridEX3"
        Me.GridEX3.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX3.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX3.RecordNavigator = True
        Me.GridEX3.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX3.Size = New System.Drawing.Size(864, 170)
        Me.GridEX3.TabIndex = 25
        Me.GridEX3.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.GridEX3.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'spliterOA
        '
        Me.spliterOA.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.spliterOA.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.spliterOA.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.spliterOA.Dock = System.Windows.Forms.DockStyle.Top
        Me.spliterOA.ExpandableControl = Me.GridEX3
        Me.spliterOA.ExpandFillColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.spliterOA.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.spliterOA.ExpandLineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.spliterOA.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.spliterOA.GripDarkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.spliterOA.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.spliterOA.GripLightColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.spliterOA.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.spliterOA.HotBackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.spliterOA.HotBackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.spliterOA.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2
        Me.spliterOA.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.spliterOA.HotExpandFillColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.spliterOA.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.spliterOA.HotExpandLineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.spliterOA.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.spliterOA.HotGripDarkColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.spliterOA.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.spliterOA.HotGripLightColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.spliterOA.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.spliterOA.Location = New System.Drawing.Point(160, 270)
        Me.spliterOA.Name = "spliterOA"
        Me.spliterOA.Size = New System.Drawing.Size(864, 7)
        Me.spliterOA.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.spliterOA.TabIndex = 26
        Me.spliterOA.TabStop = False
        '
        'Acceptance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1024, 742)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.spliterOA)
        Me.Controls.Add(Me.GridEX3)
        Me.Controls.Add(Me.ExpandableSplitter1)
        Me.Controls.Add(Me.grpRangeDate)
        Me.Controls.Add(Me.GridEX2)
        Me.Controls.Add(Me.Bar1)
        Me.Controls.Add(Me.Bar3)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.NavigationPane1)
        Me.Controls.Add(Me.PanelEx1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Acceptance"
        Me.Text = "ORDER ACCEPTANCE"
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Bar2.ResumeLayout(False)
        Me.Bar2.PerformLayout()
        CType(Me.cmbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEX2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NavigationPane1.ResumeLayout(False)
        Me.NavigationPanePanel4.ResumeLayout(False)
        Me.ExpandablePanel3.ResumeLayout(False)
        Me.ExpandablePanel3.PerformLayout()
        Me.pnlOABrandPack.ResumeLayout(False)
        Me.NavigationPanePanel3.ResumeLayout(False)
        Me.NavigationPanePanel2.ResumeLayout(False)
        Me.ExpandablePanel6.ResumeLayout(False)
        Me.NavigationPanePanel1.ResumeLayout(False)
        Me.ExpandablePanel5.ResumeLayout(False)
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRangeDate.ResumeLayout(False)
        Me.grpRangeDate.PerformLayout()
        CType(Me.GridEX3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRenameColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCurrentYear As DevComponents.DotNetBar.ButtonItem
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents GridEX2 As Janus.Windows.GridEX.GridEX
    Private WithEvents NavigationPane1 As DevComponents.DotNetBar.NavigationPane
    Private WithEvents pnlOABrandPack As DevComponents.DotNetBar.NavigationPanePanel
    Private WithEvents NavigationPanePanel1 As DevComponents.DotNetBar.NavigationPanePanel
    Private WithEvents btnPnlAgreement As DevComponents.DotNetBar.ButtonItem
    Private WithEvents NavigationPanePanel2 As DevComponents.DotNetBar.NavigationPanePanel
    Private WithEvents btnPnlMarketingDiscount As DevComponents.DotNetBar.ButtonItem
    Private WithEvents NavigationPanePanel3 As DevComponents.DotNetBar.NavigationPanePanel
    Private WithEvents btnProjectDiscount As DevComponents.DotNetBar.ButtonItem
    Private WithEvents ExpandablePanel4 As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents ExpandablePanel5 As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents ExpandablePanel7 As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents ExpandablePanel6 As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents rdbSemesterly As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbQuarterlyDiscount As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbGivenDiscount As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbMarketingTarget As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbMarketingStepping As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbGiven As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents PageSetupDialog2 As System.Windows.Forms.PageSetupDialog
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents GridEXPrintDocument2 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents NavigationPanePanel4 As DevComponents.DotNetBar.NavigationPanePanel
    Private WithEvents ExpandablePanel3 As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents btnOtherDiscount As DevComponents.DotNetBar.ButtonItem
    Private WithEvents rdbYearlyDiscount As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents btnApply As Janus.Windows.EditControls.UIButton
    Private WithEvents cmbOA As DevComponents.DotNetBar.ComboBoxItem
    Private WithEvents btnChooseOA As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnEditOA As DevComponents.DotNetBar.ButtonItem
    Private WithEvents ItemContainer1 As DevComponents.DotNetBar.ItemContainer
    Private WithEvents btnApplyEdit As DevComponents.DotNetBar.ButtonItem
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents PrintPreviewDialog2 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents grpRangeDate As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents btnAplyRange As Janus.Windows.EditControls.UIButton
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents dtpicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Bar3 As DevComponents.DotNetBar.Bar
    Private WithEvents rdbGiven_DK As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbGiven_PKPP As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbGiven_CP As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents GridEX3 As Janus.Windows.GridEX.GridEX
    Private WithEvents spliterOA As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents cmbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents rdbGivenCPR As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbSpecialCPD As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbCPMRT As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbGivenDKN As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbCPDAuto As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbUncategorized As System.Windows.Forms.RadioButton
    Private WithEvents rdbCBD As System.Windows.Forms.RadioButton
    Private WithEvents rdbDR As System.Windows.Forms.RadioButton
    Private WithEvents rdbDD As System.Windows.Forms.RadioButton

End Class
