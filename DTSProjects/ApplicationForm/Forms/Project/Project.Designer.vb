<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Project
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Project))
        Dim mcbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim MultiColumnCombo1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdBrandPack_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.grpGrid = New Janus.Windows.EditControls.UIGroupBox
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicfrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnAplyFilter = New Janus.Windows.EditControls.UIButton
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnSearchDistributor = New DTSProjects.ButtonSearch
        Me.mcbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
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
        Me.btnEdit = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.LabelItem1 = New DevComponents.DotNetBar.LabelItem
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.btnApply = New DevComponents.DotNetBar.ButtonItem
        Me.grpEntryData = New Janus.Windows.EditControls.UIGroupBox
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.grpProjectHeader = New Janus.Windows.EditControls.UIGroupBox
        Me.dtPicEndDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtPicStartDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.MultiColumnCombo1 = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.txtProjRefNo = New System.Windows.Forms.TextBox
        Me.btnSearchEntryDistributor = New DTSProjects.ButtonSearch
        Me.Label3 = New System.Windows.Forms.Label
        Me.grpProjectAddress = New Janus.Windows.EditControls.UIGroupBox
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtProjName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grpGridEx2 = New Janus.Windows.EditControls.UIGroupBox
        Me.grdBrandPack = New Janus.Windows.GridEX.GridEX
        Me.txtSearch = New WatermarkTextBox.WaterMarkTextBox
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.btnSave = New Janus.Windows.EditControls.UIButton
        Me.btnCLose = New Janus.Windows.EditControls.UIButton
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        CType(Me.grpGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGrid.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx2.SuspendLayout()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Bar2.SuspendLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpEntryData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpEntryData.SuspendLayout()
        CType(Me.grpProjectHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProjectHeader.SuspendLayout()
        CType(Me.MultiColumnCombo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpProjectAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProjectAddress.SuspendLayout()
        CType(Me.grpGridEx2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGridEx2.SuspendLayout()
        CType(Me.grdBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpGrid
        '
        Me.grpGrid.Controls.Add(Me.GridEX1)
        Me.grpGrid.Controls.Add(Me.PanelEx2)
        Me.grpGrid.Controls.Add(Me.Bar1)
        Me.grpGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpGrid.Location = New System.Drawing.Point(0, 303)
        Me.grpGrid.Name = "grpGrid"
        Me.grpGrid.Size = New System.Drawing.Size(987, 443)
        Me.grpGrid.TabIndex = 0
        Me.grpGrid.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'GridEX1
        '
        Me.GridEX1.AllowChildTableGroups = True
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(3, 40)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.SelectOnExpand = False
        Me.GridEX1.Size = New System.Drawing.Size(981, 400)
        Me.GridEX1.TabIndex = 0
        Me.GridEX1.TableHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
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
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx2.Controls.Add(Me.dtPicUntil)
        Me.PanelEx2.Controls.Add(Me.dtPicfrom)
        Me.PanelEx2.Controls.Add(Me.Label5)
        Me.PanelEx2.Controls.Add(Me.Label4)
        Me.PanelEx2.Controls.Add(Me.btnAplyFilter)
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx2.Location = New System.Drawing.Point(3, 8)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(981, 32)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 27
        '
        'dtPicUntil
        '
        Me.dtPicUntil.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicUntil.CustomFormat = "dd MMMM yyyy"
        Me.dtPicUntil.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicUntil.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicUntil.DropDownCalendar.DayHeadersFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicUntil.DropDownCalendar.DaysFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicUntil.DropDownCalendar.Name = ""
        Me.dtPicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicUntil.Location = New System.Drawing.Point(501, 5)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.Size = New System.Drawing.Size(170, 20)
        Me.dtPicUntil.TabIndex = 13
        Me.dtPicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicfrom
        '
        Me.dtPicfrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicfrom.CustomFormat = "dd MMMM yyyy"
        Me.dtPicfrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicfrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicfrom.DropDownCalendar.DayHeadersFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicfrom.DropDownCalendar.DaysFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicfrom.DropDownCalendar.Name = ""
        Me.dtPicfrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicfrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicfrom.Location = New System.Drawing.Point(291, 4)
        Me.dtPicfrom.Name = "dtPicfrom"
        Me.dtPicfrom.Size = New System.Drawing.Size(170, 20)
        Me.dtPicfrom.TabIndex = 12
        Me.dtPicfrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(467, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(28, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Until"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(255, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "From"
        '
        'btnAplyFilter
        '
        Me.btnAplyFilter.Location = New System.Drawing.Point(677, 3)
        Me.btnAplyFilter.Name = "btnAplyFilter"
        Me.btnAplyFilter.Size = New System.Drawing.Size(75, 23)
        Me.btnAplyFilter.TabIndex = 9
        Me.btnAplyFilter.Text = "Apply filter"
        Me.btnAplyFilter.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Bar1
        '
        Me.Bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.Bar1.Location = New System.Drawing.Point(271, 167)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(46, 99)
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar1.TabIndex = 2
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar2 (Bar2)"
        Me.Bar2.AccessibleName = "Bar2"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.Controls.Add(Me.btnSearchDistributor)
        Me.Bar2.Controls.Add(Me.mcbDistributor)
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnAddNew, Me.btnExport, Me.btnEdit, Me.btnRefresh, Me.LabelItem1})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(987, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 19
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnSearchDistributor
        '
        Me.btnSearchDistributor.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchDistributor.Location = New System.Drawing.Point(804, 2)
        Me.btnSearchDistributor.Name = "btnSearchDistributor"
        Me.btnSearchDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchDistributor.TabIndex = 8
        '
        'mcbDistributor
        '
        Me.mcbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbDistributor_DesignTimeLayout.LayoutString = resources.GetString("mcbDistributor_DesignTimeLayout.LayoutString")
        Me.mcbDistributor.DesignTimeLayout = mcbDistributor_DesignTimeLayout
        Me.mcbDistributor.Location = New System.Drawing.Point(546, 0)
        Me.mcbDistributor.Name = "mcbDistributor"
        Me.mcbDistributor.SelectedIndex = -1
        Me.mcbDistributor.SelectedItem = Nothing
        Me.mcbDistributor.Size = New System.Drawing.Size(252, 23)
        Me.mcbDistributor.TabIndex = 0
        Me.mcbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
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
        Me.btnAddNew.Text = "Add New"
        Me.btnAddNew.Tooltip = "add new item data"
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
        Me.btnEdit.Text = "Edit Row"
        Me.btnEdit.Tooltip = "Edit selected row in datagrid "
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
        'LabelItem1
        '
        Me.LabelItem1.BorderType = DevComponents.DotNetBar.eBorderType.None
        Me.LabelItem1.Name = "LabelItem1"
        Me.LabelItem1.Text = "Distributor"
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.Transparent
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Contains
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 25)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(16, 45)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(987, 46)
        Me.FilterEditor1.SourceControl = Me.GridEX1
        '
        'btnApply
        '
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Text = "Apply Filter Drop Down"
        '
        'grpEntryData
        '
        Me.grpEntryData.Controls.Add(Me.ExpandableSplitter1)
        Me.grpEntryData.Controls.Add(Me.grpGridEx2)
        Me.grpEntryData.Controls.Add(Me.grpProjectHeader)
        Me.grpEntryData.Controls.Add(Me.PanelEx1)
        Me.grpEntryData.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpEntryData.Location = New System.Drawing.Point(0, 71)
        Me.grpEntryData.Name = "grpEntryData"
        Me.grpEntryData.Size = New System.Drawing.Size(987, 232)
        Me.grpEntryData.TabIndex = 25
        Me.grpEntryData.Text = "Add or Edit data"
        Me.grpEntryData.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.ExpandableControl = Me.grpProjectHeader
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
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(428, 16)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(3, 176)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 29
        Me.ExpandableSplitter1.TabStop = False
        '
        'grpProjectHeader
        '
        Me.grpProjectHeader.Controls.Add(Me.dtPicEndDate)
        Me.grpProjectHeader.Controls.Add(Me.Label6)
        Me.grpProjectHeader.Controls.Add(Me.dtPicStartDate)
        Me.grpProjectHeader.Controls.Add(Me.MultiColumnCombo1)
        Me.grpProjectHeader.Controls.Add(Me.txtProjRefNo)
        Me.grpProjectHeader.Controls.Add(Me.btnSearchEntryDistributor)
        Me.grpProjectHeader.Controls.Add(Me.Label3)
        Me.grpProjectHeader.Controls.Add(Me.grpProjectAddress)
        Me.grpProjectHeader.Controls.Add(Me.Label9)
        Me.grpProjectHeader.Controls.Add(Me.txtProjName)
        Me.grpProjectHeader.Controls.Add(Me.Label2)
        Me.grpProjectHeader.Controls.Add(Me.Label1)
        Me.grpProjectHeader.Dock = System.Windows.Forms.DockStyle.Left
        Me.grpProjectHeader.Location = New System.Drawing.Point(3, 16)
        Me.grpProjectHeader.Name = "grpProjectHeader"
        Me.grpProjectHeader.Size = New System.Drawing.Size(425, 176)
        Me.grpProjectHeader.TabIndex = 28
        Me.grpProjectHeader.Text = "Project header"
        Me.grpProjectHeader.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'dtPicEndDate
        '
        Me.dtPicEndDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicEndDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicEndDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        Me.dtPicEndDate.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        '
        '
        '
        Me.dtPicEndDate.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicEndDate.DropDownCalendar.Name = ""
        Me.dtPicEndDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicEndDate.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicEndDate.Location = New System.Drawing.Point(210, 74)
        Me.dtPicEndDate.Name = "dtPicEndDate"
        Me.dtPicEndDate.Size = New System.Drawing.Size(203, 20)
        Me.dtPicEndDate.TabIndex = 29
        Me.dtPicEndDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(227, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 15)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "End Date"
        '
        'dtPicStartDate
        '
        Me.dtPicStartDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicStartDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicStartDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        Me.dtPicStartDate.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        '
        '
        '
        Me.dtPicStartDate.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicStartDate.DropDownCalendar.Name = ""
        Me.dtPicStartDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicStartDate.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicStartDate.Location = New System.Drawing.Point(8, 74)
        Me.dtPicStartDate.Name = "dtPicStartDate"
        Me.dtPicStartDate.Size = New System.Drawing.Size(196, 20)
        Me.dtPicStartDate.TabIndex = 24
        Me.dtPicStartDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'MultiColumnCombo1
        '
        Me.MultiColumnCombo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        MultiColumnCombo1_DesignTimeLayout.LayoutString = resources.GetString("MultiColumnCombo1_DesignTimeLayout.LayoutString")
        Me.MultiColumnCombo1.DesignTimeLayout = MultiColumnCombo1_DesignTimeLayout
        Me.MultiColumnCombo1.DisplayMember = "DISTRIBUTOR_NAME"
        Me.MultiColumnCombo1.Location = New System.Drawing.Point(76, 104)
        Me.MultiColumnCombo1.Name = "MultiColumnCombo1"
        Me.MultiColumnCombo1.SelectedIndex = -1
        Me.MultiColumnCombo1.SelectedItem = Nothing
        Me.MultiColumnCombo1.Size = New System.Drawing.Size(318, 20)
        Me.MultiColumnCombo1.TabIndex = 8
        Me.MultiColumnCombo1.ValueMember = "DISTRIBUTOR_ID"
        Me.MultiColumnCombo1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtProjRefNo
        '
        Me.txtProjRefNo.Location = New System.Drawing.Point(6, 30)
        Me.txtProjRefNo.MaxLength = 15
        Me.txtProjRefNo.Name = "txtProjRefNo"
        Me.txtProjRefNo.Size = New System.Drawing.Size(113, 20)
        Me.txtProjRefNo.TabIndex = 0
        '
        'btnSearchEntryDistributor
        '
        Me.btnSearchEntryDistributor.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchEntryDistributor.Location = New System.Drawing.Point(399, 105)
        Me.btnSearchEntryDistributor.Name = "btnSearchEntryDistributor"
        Me.btnSearchEntryDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchEntryDistributor.TabIndex = 27
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Start Date"
        '
        'grpProjectAddress
        '
        Me.grpProjectAddress.Controls.Add(Me.txtAddress)
        Me.grpProjectAddress.Location = New System.Drawing.Point(2, 125)
        Me.grpProjectAddress.Name = "grpProjectAddress"
        Me.grpProjectAddress.Size = New System.Drawing.Size(417, 51)
        Me.grpProjectAddress.TabIndex = 26
        Me.grpProjectAddress.Text = "Proeject Address"
        Me.grpProjectAddress.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtAddress
        '
        Me.txtAddress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAddress.Location = New System.Drawing.Point(3, 16)
        Me.txtAddress.MaxLength = 100
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(411, 32)
        Me.txtAddress.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1, 105)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 17)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "Distributor"
        '
        'txtProjName
        '
        Me.txtProjName.Location = New System.Drawing.Point(125, 31)
        Me.txtProjName.MaxLength = 50
        Me.txtProjName.Name = "txtProjName"
        Me.txtProjName.Size = New System.Drawing.Size(294, 20)
        Me.txtProjName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(130, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Project Name"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Project Ref No"
        '
        'grpGridEx2
        '
        Me.grpGridEx2.Controls.Add(Me.grdBrandPack)
        Me.grpGridEx2.Controls.Add(Me.txtSearch)
        Me.grpGridEx2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpGridEx2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpGridEx2.Location = New System.Drawing.Point(428, 16)
        Me.grpGridEx2.Name = "grpGridEx2"
        Me.grpGridEx2.Size = New System.Drawing.Size(556, 176)
        Me.grpGridEx2.TabIndex = 23
        Me.grpGridEx2.Text = "Project Brandpack Detail"
        Me.grpGridEx2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grdBrandPack
        '
        Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdBrandPack_DesignTimeLayout.LayoutString = resources.GetString("grdBrandPack_DesignTimeLayout.LayoutString")
        Me.grdBrandPack.DesignTimeLayout = grdBrandPack_DesignTimeLayout
        Me.grdBrandPack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdBrandPack.GroupByBoxVisible = False
        Me.grdBrandPack.Location = New System.Drawing.Point(3, 37)
        Me.grdBrandPack.Name = "grdBrandPack"
        Me.grdBrandPack.Size = New System.Drawing.Size(550, 136)
        Me.grdBrandPack.TabIndex = 5
        Me.grdBrandPack.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSearch.Location = New System.Drawing.Point(3, 17)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(550, 20)
        Me.txtSearch.TabIndex = 4
        Me.txtSearch.WaterMarkColor = System.Drawing.Color.Gray
        Me.txtSearch.WaterMarkText = "Enter data to search"
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.btnSave)
        Me.PanelEx1.Controls.Add(Me.btnCLose)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx1.Location = New System.Drawing.Point(3, 192)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(981, 37)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 26
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnSave.ImageIndex = 12
        Me.btnSave.ImageList = Me.ImageList1
        Me.btnSave.Location = New System.Drawing.Point(765, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(101, 24)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "&Save Changes"
        Me.btnSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnCLose
        '
        Me.btnCLose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCLose.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnCLose.ImageIndex = 0
        Me.btnCLose.ImageList = Me.ImageList1
        Me.btnCLose.Location = New System.Drawing.Point(872, 8)
        Me.btnCLose.Name = "btnCLose"
        Me.btnCLose.Size = New System.Drawing.Size(106, 24)
        Me.btnCLose.TabIndex = 10
        Me.btnCLose.Text = "&Cancel / Hide"
        Me.btnCLose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.GridEX = Me.GridEX1
        Me.GridEXExporter1.IncludeFormatStyle = False
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.GridEX = Me.GridEX1
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'Project
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(987, 746)
        Me.Controls.Add(Me.grpGrid)
        Me.Controls.Add(Me.grpEntryData)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Project"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PROJECT "
        CType(Me.grpGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGrid.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx2.ResumeLayout(False)
        Me.PanelEx2.PerformLayout()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Bar2.ResumeLayout(False)
        Me.Bar2.PerformLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpEntryData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpEntryData.ResumeLayout(False)
        CType(Me.grpProjectHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProjectHeader.ResumeLayout(False)
        Me.grpProjectHeader.PerformLayout()
        CType(Me.MultiColumnCombo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpProjectAddress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProjectAddress.ResumeLayout(False)
        Me.grpProjectAddress.PerformLayout()
        CType(Me.grpGridEx2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGridEx2.ResumeLayout(False)
        Me.grpGridEx2.PerformLayout()
        CType(Me.grdBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpGrid As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRenameColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCLose As Janus.Windows.EditControls.UIButton
    Private WithEvents btnSave As Janus.Windows.EditControls.UIButton
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents txtAddress As System.Windows.Forms.TextBox
    Private WithEvents txtProjRefNo As System.Windows.Forms.TextBox
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents MultiColumnCombo1 As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents btnEdit As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnApply As DevComponents.DotNetBar.ButtonItem
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents grpGridEx2 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents dtPicStartDate As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents grpEntryData As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents txtProjName As System.Windows.Forms.TextBox
    Private WithEvents grpProjectAddress As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Private WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents btnAplyFilter As Janus.Windows.EditControls.UIButton
    Private WithEvents dtPicfrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents LabelItem1 As DevComponents.DotNetBar.LabelItem
    Private WithEvents btnSearchDistributor As DTSProjects.ButtonSearch
    Private WithEvents btnSearchEntryDistributor As DTSProjects.ButtonSearch
    Private WithEvents mcbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents grpProjectHeader As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents txtSearch As WatermarkTextBox.WaterMarkTextBox
    Private WithEvents grdBrandPack As Janus.Windows.GridEX.GridEX
    Private WithEvents dtPicEndDate As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label6 As System.Windows.Forms.Label

End Class
