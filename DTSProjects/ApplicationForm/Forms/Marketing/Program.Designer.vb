<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Program
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Program))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX2_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX3_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnAplyrange = New Janus.Windows.EditControls.UIButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
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
        Me.btnMarketingProgram = New DevComponents.DotNetBar.ButtonItem
        Me.btnMarketingBrandPack = New DevComponents.DotNetBar.ButtonItem
        Me.btnBrandPackDistributor = New DevComponents.DotNetBar.ButtonItem
        Me.btnStepingDiscount = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnEdit = New DevComponents.DotNetBar.ButtonItem
        Me.btnEditMarketingProgram = New DevComponents.DotNetBar.ButtonItem
        Me.btnEditMarketingBrandPack = New DevComponents.DotNetBar.ButtonItem
        Me.btnEditBrandPackDistributor = New DevComponents.DotNetBar.ButtonItem
        Me.btnEditStepingDiscount = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.grpProgBrandPack = New Janus.Windows.EditControls.UIGroupBox
        Me.XpCaption2 = New SteepValley.Windows.Forms.XPCaption
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.grpDistributor = New Janus.Windows.EditControls.UIGroupBox
        Me.GridEX2 = New Janus.Windows.GridEX.GridEX
        Me.ExpandableSplitter2 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.GridEX3 = New Janus.Windows.GridEX.GridEX
        Me.XpCaption1 = New SteepValley.Windows.Forms.XPCaption
        Me.Bar3 = New DevComponents.DotNetBar.Bar
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.GridEXPrintDocument2 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PrintPreviewDialog2 = New System.Windows.Forms.PrintPreviewDialog
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.PageSetupDialog2 = New System.Windows.Forms.PageSetupDialog
        Me.UiCheckBox1 = New Janus.Windows.EditControls.UICheckBox
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Bar2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpProgBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProgBrandPack.SuspendLayout()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDistributor.SuspendLayout()
        CType(Me.GridEX2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEX3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Equal
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 25)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(0, 0)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(962, 33)
        Me.FilterEditor1.SortFieldList = False
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar2 (Bar2)"
        Me.Bar2.AccessibleName = "Bar2"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaption
        Me.Bar2.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaptionText
        Me.Bar2.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.Bar2.Controls.Add(Me.btnAplyrange)
        Me.Bar2.Controls.Add(Me.Label2)
        Me.Bar2.Controls.Add(Me.Label1)
        Me.Bar2.Controls.Add(Me.dtPicUntil)
        Me.Bar2.Controls.Add(Me.dtPicFrom)
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnAddNew, Me.btnExport, Me.btnEdit, Me.btnRefresh})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(962, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 20
        Me.Bar2.TabStop = False
        '
        'btnAplyrange
        '
        Me.btnAplyrange.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAplyrange.ImageIndex = 3
        Me.btnAplyrange.ImageList = Me.ImageList1
        Me.btnAplyrange.Location = New System.Drawing.Point(848, 2)
        Me.btnAplyrange.Name = "btnAplyrange"
        Me.btnAplyrange.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnAplyrange.Size = New System.Drawing.Size(90, 20)
        Me.btnAplyrange.TabIndex = 5
        Me.btnAplyrange.Text = "Apply Filter"
        Me.btnAplyrange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(661, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "End"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(477, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Start"
        '
        'dtPicUntil
        '
        Me.dtPicUntil.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicUntil.CustomFormat = "dd MMMM yyyy"
        Me.dtPicUntil.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicUntil.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicUntil.DropDownCalendar.Name = ""
        Me.dtPicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicUntil.Location = New System.Drawing.Point(692, 1)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(141, 23)
        Me.dtPicUntil.TabIndex = 1
        Me.dtPicUntil.Value = New Date(2010, 7, 3, 0, 0, 0, 0)
        Me.dtPicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicFrom
        '
        Me.dtPicFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFrom.CustomFormat = "dd MMMM yyyy"
        Me.dtPicFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicFrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFrom.DropDownCalendar.Name = ""
        Me.dtPicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicFrom.Location = New System.Drawing.Point(513, 1)
        Me.dtPicFrom.Name = "dtPicFrom"
        Me.dtPicFrom.ShowTodayButton = False
        Me.dtPicFrom.Size = New System.Drawing.Size(143, 23)
        Me.dtPicFrom.TabIndex = 0
        Me.dtPicFrom.Value = New Date(2014, 9, 18, 0, 0, 0, 0)
        Me.dtPicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
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
        Me.btnAddNew.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnMarketingProgram, Me.btnMarketingBrandPack, Me.btnBrandPackDistributor, Me.btnStepingDiscount})
        Me.btnAddNew.Text = "Add New"
        Me.btnAddNew.Tooltip = "add new item data"
        '
        'btnMarketingProgram
        '
        Me.btnMarketingProgram.Name = "btnMarketingProgram"
        Me.btnMarketingProgram.Text = "Sales  Program"
        '
        'btnMarketingBrandPack
        '
        Me.btnMarketingBrandPack.Name = "btnMarketingBrandPack"
        Me.btnMarketingBrandPack.Text = "BrandPack for  Sales Program"
        '
        'btnBrandPackDistributor
        '
        Me.btnBrandPackDistributor.Name = "btnBrandPackDistributor"
        Me.btnBrandPackDistributor.Text = "Distributor for BrandPack Program"
        '
        'btnStepingDiscount
        '
        Me.btnStepingDiscount.Name = "btnStepingDiscount"
        Me.btnStepingDiscount.Text = "Stepping Discount BrandPack for Distributor"
        Me.btnStepingDiscount.Visible = False
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 14
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
        '
        'btnEdit
        '
        Me.btnEdit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnEdit.ImageIndex = 10
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnEditMarketingProgram, Me.btnEditMarketingBrandPack, Me.btnEditBrandPackDistributor, Me.btnEditStepingDiscount})
        Me.btnEdit.Text = "Edit Row"
        Me.btnEdit.Tooltip = "Edit selected row in datagrid "
        '
        'btnEditMarketingProgram
        '
        Me.btnEditMarketingProgram.Name = "btnEditMarketingProgram"
        Me.btnEditMarketingProgram.Text = "Sales Program"
        '
        'btnEditMarketingBrandPack
        '
        Me.btnEditMarketingBrandPack.Name = "btnEditMarketingBrandPack"
        Me.btnEditMarketingBrandPack.Text = "BrandPack for  Sales Program"
        '
        'btnEditBrandPackDistributor
        '
        Me.btnEditBrandPackDistributor.Name = "btnEditBrandPackDistributor"
        Me.btnEditBrandPackDistributor.Text = "Distributor for BrandPack Program"
        '
        'btnEditStepingDiscount
        '
        Me.btnEditStepingDiscount.Name = "btnEditStepingDiscount"
        Me.btnEditStepingDiscount.Text = "Stepping Discount BrandPack for Distributor"
        Me.btnEditStepingDiscount.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 15
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.Tooltip = "Reload data and refresh grid"
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
        Me.GridEX1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(3, 25)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(956, 364)
        Me.GridEX1.TabIndex = 4
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'grpProgBrandPack
        '
        Me.grpProgBrandPack.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grpProgBrandPack.Controls.Add(Me.GridEX1)
        Me.grpProgBrandPack.Controls.Add(Me.XpCaption2)
        Me.grpProgBrandPack.Controls.Add(Me.Bar1)
        Me.grpProgBrandPack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpProgBrandPack.Location = New System.Drawing.Point(0, 71)
        Me.grpProgBrandPack.Name = "grpProgBrandPack"
        Me.grpProgBrandPack.Size = New System.Drawing.Size(962, 392)
        Me.grpProgBrandPack.TabIndex = 5
        Me.grpProgBrandPack.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'XpCaption2
        '
        Me.XpCaption2.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpCaption2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XpCaption2.InactiveGradientHighColor = System.Drawing.Color.LightSteelBlue
        Me.XpCaption2.InactiveGradientLowColor = System.Drawing.Color.WhiteSmoke
        Me.XpCaption2.InactiveTextColor = System.Drawing.Color.DimGray
        Me.XpCaption2.Location = New System.Drawing.Point(3, 8)
        Me.XpCaption2.Name = "XpCaption2"
        Me.XpCaption2.Size = New System.Drawing.Size(956, 17)
        Me.XpCaption2.TabIndex = 7
        Me.XpCaption2.Text = "Data Sales Program (with BrandPack detail)"
        '
        'Bar1
        '
        Me.Bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.Bar1.Location = New System.Drawing.Point(412, 119)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(39, 375)
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar1.TabIndex = 5
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'grpDistributor
        '
        Me.grpDistributor.Controls.Add(Me.GridEX2)
        Me.grpDistributor.Controls.Add(Me.ExpandableSplitter2)
        Me.grpDistributor.Controls.Add(Me.GridEX3)
        Me.grpDistributor.Controls.Add(Me.XpCaption1)
        Me.grpDistributor.Controls.Add(Me.Bar3)
        Me.grpDistributor.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpDistributor.Location = New System.Drawing.Point(0, 470)
        Me.grpDistributor.Name = "grpDistributor"
        Me.grpDistributor.Size = New System.Drawing.Size(962, 272)
        Me.grpDistributor.TabIndex = 6
        Me.grpDistributor.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'GridEX2
        '
        Me.GridEX2.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX2.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX2.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX2.Cursor = System.Windows.Forms.Cursors.Default
        Me.GridEX2.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX2_DesignTimeLayout.LayoutString = resources.GetString("GridEX2_DesignTimeLayout.LayoutString")
        Me.GridEX2.DesignTimeLayout = GridEX2_DesignTimeLayout
        Me.GridEX2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX2.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX2.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX2.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX2.GridLineColor = System.Drawing.SystemColors.WindowFrame
        Me.GridEX2.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.GridEX2.GroupByBoxVisible = False
        Me.GridEX2.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX2.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX2.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX2.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.GridEX2.ImageList = Me.ImageList1
        Me.GridEX2.Location = New System.Drawing.Point(3, 25)
        Me.GridEX2.Name = "GridEX2"
        Me.GridEX2.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX2.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX2.RecordNavigator = True
        Me.GridEX2.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX2.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.Size = New System.Drawing.Size(946, 244)
        Me.GridEX2.TabIndex = 4
        Me.GridEX2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX2.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ExpandableSplitter2
        '
        Me.ExpandableSplitter2.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.ExpandableSplitter2.ExpandableControl = Me.GridEX3
        Me.ExpandableSplitter2.Expanded = False
        Me.ExpandableSplitter2.ExpandFillColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter2.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter2.ExpandLineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ExpandableSplitter2.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter2.GripDarkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ExpandableSplitter2.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter2.GripLightColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter2.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter2.HotBackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.ExpandableSplitter2.HotBackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ExpandableSplitter2.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2
        Me.ExpandableSplitter2.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.ExpandableSplitter2.HotExpandFillColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter2.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter2.HotExpandLineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ExpandableSplitter2.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter2.HotGripDarkColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter2.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter2.HotGripLightColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter2.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter2.Location = New System.Drawing.Point(949, 25)
        Me.ExpandableSplitter2.Name = "ExpandableSplitter2"
        Me.ExpandableSplitter2.Size = New System.Drawing.Size(10, 244)
        Me.ExpandableSplitter2.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter2.TabIndex = 8
        Me.ExpandableSplitter2.TabStop = False
        Me.ExpandableSplitter2.Visible = False
        '
        'GridEX3
        '
        Me.GridEX3.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX3.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridEX3.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX3_DesignTimeLayout.LayoutString = resources.GetString("GridEX3_DesignTimeLayout.LayoutString")
        Me.GridEX3.DesignTimeLayout = GridEX3_DesignTimeLayout
        Me.GridEX3.Dock = System.Windows.Forms.DockStyle.Right
        Me.GridEX3.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX3.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridEX3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX3.GroupByBoxVisible = False
        Me.GridEX3.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX3.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX3.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX3.ImageList = Me.ImageList1
        Me.GridEX3.Location = New System.Drawing.Point(664, 25)
        Me.GridEX3.Name = "GridEX3"
        Me.GridEX3.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX3.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX3.RecordNavigator = True
        Me.GridEX3.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridEX3.Size = New System.Drawing.Size(295, 244)
        Me.GridEX3.TabIndex = 7
        Me.GridEX3.Visible = False
        Me.GridEX3.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX3.WatermarkImage.Image = CType(resources.GetObject("GridEX3.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX3.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'XpCaption1
        '
        Me.XpCaption1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpCaption1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XpCaption1.InactiveGradientHighColor = System.Drawing.Color.LightSteelBlue
        Me.XpCaption1.InactiveGradientLowColor = System.Drawing.Color.WhiteSmoke
        Me.XpCaption1.InactiveTextColor = System.Drawing.Color.DimGray
        Me.XpCaption1.Location = New System.Drawing.Point(3, 8)
        Me.XpCaption1.Name = "XpCaption1"
        Me.XpCaption1.Size = New System.Drawing.Size(956, 17)
        Me.XpCaption1.TabIndex = 6
        Me.XpCaption1.Text = "BrandPack (with Distributor detail)"
        '
        'Bar3
        '
        Me.Bar3.DockSide = DevComponents.DotNetBar.eDockSide.Bottom
        Me.Bar3.Location = New System.Drawing.Point(422, 174)
        Me.Bar3.Name = "Bar3"
        Me.Bar3.Size = New System.Drawing.Size(52, 25)
        Me.Bar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar3.TabIndex = 5
        Me.Bar3.TabStop = False
        Me.Bar3.Text = "Bar3"
        '
        'GridEXPrintDocument2
        '
        Me.GridEXPrintDocument2.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument2.GridEX = Me.GridEX2
        Me.GridEXPrintDocument2.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument2.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument2.PrintCellBackground = False
        Me.GridEXPrintDocument2.PrintHierarchical = True
        '
        'PrintPreviewDialog2
        '
        Me.PrintPreviewDialog2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog2.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog2.Document = Me.GridEXPrintDocument2
        Me.PrintPreviewDialog2.Enabled = True
        Me.PrintPreviewDialog2.Icon = CType(resources.GetObject("PrintPreviewDialog2.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog2.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog2.Visible = False
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
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.GridEX = Me.GridEX1
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.GridEX = Me.GridEX1
        Me.GridEXExporter1.IncludeFormatStyle = False
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'PageSetupDialog2
        '
        Me.PageSetupDialog2.Document = Me.GridEXPrintDocument2
        '
        'UiCheckBox1
        '
        Me.UiCheckBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.UiCheckBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UiCheckBox1.Location = New System.Drawing.Point(0, 58)
        Me.UiCheckBox1.Name = "UiCheckBox1"
        Me.UiCheckBox1.Size = New System.Drawing.Size(962, 13)
        Me.UiCheckBox1.TabIndex = 9
        Me.UiCheckBox1.Text = "Show All"
        Me.UiCheckBox1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ExpandableSplitter1.ExpandableControl = Me.grpDistributor
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
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(0, 463)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(962, 7)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 10
        Me.ExpandableSplitter1.TabStop = False
        '
        'Program
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(962, 742)
        Me.Controls.Add(Me.grpProgBrandPack)
        Me.Controls.Add(Me.UiCheckBox1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Controls.Add(Me.ExpandableSplitter1)
        Me.Controls.Add(Me.grpDistributor)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Program"
        Me.Text = "SALES  PROGRAM"
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Bar2.ResumeLayout(False)
        Me.Bar2.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpProgBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProgBrandPack.ResumeLayout(False)
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDistributor.ResumeLayout(False)
        CType(Me.GridEX2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEX3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents Bar2 As DevComponents.DotNetBar.Bar
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
    Private WithEvents btnEdit As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents grpProgBrandPack As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents grpDistributor As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents GridEX2 As Janus.Windows.GridEX.GridEX
    Private WithEvents btnMarketingProgram As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnMarketingBrandPack As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnBrandPackDistributor As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnStepingDiscount As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnEditMarketingProgram As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnEditMarketingBrandPack As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnEditBrandPackDistributor As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnEditStepingDiscount As DevComponents.DotNetBar.ButtonItem
    Private WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Private WithEvents Bar3 As DevComponents.DotNetBar.Bar
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents GridEXPrintDocument2 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PrintPreviewDialog2 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents PageSetupDialog2 As System.Windows.Forms.PageSetupDialog
    Private WithEvents XpCaption2 As SteepValley.Windows.Forms.XPCaption
    Private WithEvents UiCheckBox1 As Janus.Windows.EditControls.UICheckBox
    Private WithEvents XpCaption1 As SteepValley.Windows.Forms.XPCaption
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents ExpandableSplitter2 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents GridEX3 As Janus.Windows.GridEX.GridEX
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents btnAplyrange As Janus.Windows.EditControls.UIButton
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor

End Class
