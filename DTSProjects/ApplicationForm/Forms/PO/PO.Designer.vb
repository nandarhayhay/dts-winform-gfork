<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PO
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PO))
        Dim grdPurchaseOrder_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim cmbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnApply = New DevComponents.DotNetBar.ButtonItem
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.grdPurchaseOrder = New Janus.Windows.GridEX.GridEX
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnEdit = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnManagePO = New DevComponents.DotNetBar.ButtonItem
        Me.btnPOHeader = New DevComponents.DotNetBar.ButtonItem
        Me.btnPOHeaderAndDetail = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterEqual = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSettings = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnRenameColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.UiButton1 = New Janus.Windows.EditControls.UIButton
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.ExpandablePanel2 = New DevComponents.DotNetBar.ExpandablePanel
        Me.grpRangeDate = New Janus.Windows.EditControls.UIGroupBox
        Me.btnXCategoryFindPO = New DevComponents.DotNetBar.ButtonX
        Me.btnbyPO = New DevComponents.DotNetBar.ButtonItem
        Me.btnbyRangedDate = New DevComponents.DotNetBar.ButtonItem
        Me.btnByRangeDateAndCancelPO = New DevComponents.DotNetBar.ButtonItem
        Me.btnByRangeDateAndPlantation = New DevComponents.DotNetBar.ButtonItem
        Me.txtSearchPO = New System.Windows.Forms.TextBox
        Me.btnAplyRange = New Janus.Windows.EditControls.UIButton
        Me.lblPOOrEndDate = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.pnlPencapaian = New DevComponents.DotNetBar.ExpandablePanel
        Me.grdPencapaian = New Janus.Windows.GridEX.GridEX
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.btnAplyFilterPencapaian = New Janus.Windows.EditControls.UIButton
        Me.ButtonSearch1 = New DTSProjects.ButtonSearch
        Me.cmbFlag = New Janus.Windows.EditControls.UIComboBox
        Me.cmbAgreement = New Janus.Windows.EditControls.UIComboBox
        Me.lblFlag = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.cmbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.GridEXPrintDocument2 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PrintPreviewDialog2 = New System.Windows.Forms.PrintPreviewDialog
        Me.PageSetupDialog2 = New System.Windows.Forms.PageSetupDialog
        CType(Me.grdPurchaseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExpandablePanel2.SuspendLayout()
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRangeDate.SuspendLayout()
        Me.pnlPencapaian.SuspendLayout()
        CType(Me.grdPencapaian, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Bar2.SuspendLayout()
        CType(Me.cmbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'btnApply
        '
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Text = "Apply Filter Drop Down"
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.GridEX = Me.grdPurchaseOrder
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'grdPurchaseOrder
        '
        Me.grdPurchaseOrder.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPurchaseOrder.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPurchaseOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdPurchaseOrder.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.grdPurchaseOrder.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        grdPurchaseOrder_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdPurchaseOrder.DesignTimeLayout = grdPurchaseOrder_DesignTimeLayout
        Me.grdPurchaseOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPurchaseOrder.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdPurchaseOrder.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdPurchaseOrder.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.grdPurchaseOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrder.GroupByBoxFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.grdPurchaseOrder.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdPurchaseOrder.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdPurchaseOrder.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdPurchaseOrder.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPurchaseOrder.ImageList = Me.ImageList1
        Me.grdPurchaseOrder.Location = New System.Drawing.Point(0, 364)
        Me.grdPurchaseOrder.Name = "grdPurchaseOrder"
        Me.grdPurchaseOrder.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdPurchaseOrder.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdPurchaseOrder.RecordNavigator = True
        Me.grdPurchaseOrder.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdPurchaseOrder.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPurchaseOrder.Size = New System.Drawing.Size(979, 378)
        Me.grdPurchaseOrder.TabIndex = 2
        Me.grdPurchaseOrder.TableHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPurchaseOrder.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdPurchaseOrder.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPurchaseOrder.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPurchaseOrder.TotalRowFormatStyle.Font = New System.Drawing.Font("Verdana", 8.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.grdPurchaseOrder.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.grdPurchaseOrder.TotalRowFormatStyle.FontSize = 8.0!
        Me.grdPurchaseOrder.TotalRowFormatStyle.FontUnderline = Janus.Windows.GridEX.TriState.[True]
        Me.grdPurchaseOrder.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.grdPurchaseOrder.WatermarkImage.Image = CType(resources.GetObject("grdPurchaseOrder.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdPurchaseOrder.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
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
        'GridEXExporter1
        '
        Me.GridEXExporter1.GridEX = Me.grdPurchaseOrder
        Me.GridEXExporter1.IncludeFormatStyle = False
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
        'btnEdit
        '
        Me.btnEdit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnEdit.ImageIndex = 10
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Text = "&Edit Row"
        Me.btnEdit.Tooltip = "Edit selected row in datagrid "
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 14
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "E&xport Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
        '
        'btnManagePO
        '
        Me.btnManagePO.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnManagePO.Name = "btnManagePO"
        Me.btnManagePO.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlN)
        Me.btnManagePO.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnPOHeader, Me.btnPOHeaderAndDetail})
        Me.btnManagePO.Text = "Manage PO"
        Me.btnManagePO.Tooltip = "add new item data"
        '
        'btnPOHeader
        '
        Me.btnPOHeader.Name = "btnPOHeader"
        Me.btnPOHeader.Text = "Header"
        '
        'btnPOHeaderAndDetail
        '
        Me.btnPOHeaderAndDetail.Name = "btnPOHeaderAndDetail"
        Me.btnPOHeaderAndDetail.Text = "PO Header and Detail"
        '
        'btnFilterEqual
        '
        Me.btnFilterEqual.Name = "btnFilterEqual"
        Me.btnFilterEqual.Text = "Default"
        Me.btnFilterEqual.Tooltip = "use this filter to filter data in data grid directly from datagrid with equal cri" & _
            "teria"
        '
        'btnCustomFilter
        '
        Me.btnCustomFilter.Name = "btnCustomFilter"
        Me.btnCustomFilter.Text = "Custom Filter"
        Me.btnCustomFilter.Tooltip = "use this filter to filter data in datagrid  by your own criteria"
        '
        'btnFilter
        '
        Me.btnFilter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnFilter.ImageIndex = 3
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCustomFilter, Me.btnFilterEqual})
        Me.btnFilter.Text = "Filter Row"
        '
        'btnPageSettings
        '
        Me.btnPageSettings.ImageIndex = 16
        Me.btnPageSettings.Name = "btnPageSettings"
        Me.btnPageSettings.Text = "Page Settings"
        Me.btnPageSettings.Tooltip = "setting datagrid page ,use this setting if you want to print datagrid with page s" & _
            "etting defined by yourseef"
        '
        'btnPrint
        '
        Me.btnPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrint.ImageIndex = 13
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Text = "Print data Grid"
        Me.btnPrint.Tooltip = "print datagrid,all visible rows in datagrid will be printed"
        '
        'btnSettingGrid
        '
        Me.btnSettingGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSettingGrid.ImageIndex = 4
        Me.btnSettingGrid.Name = "btnSettingGrid"
        Me.btnSettingGrid.Text = "Setting Grid"
        Me.btnSettingGrid.Tooltip = "show / remove column by draging  / dropping any column in datagrid"
        '
        'btnShowFieldChooser
        '
        Me.btnShowFieldChooser.Name = "btnShowFieldChooser"
        Me.btnShowFieldChooser.Text = "Show Field Chooser"
        Me.btnShowFieldChooser.Tooltip = "show / remove column by draging  / dropping any column in datagrid"
        '
        'btnRenameColumn
        '
        Me.btnRenameColumn.Name = "btnRenameColumn"
        Me.btnRenameColumn.Text = "Rename Column"
        Me.btnRenameColumn.Tooltip = "use this button to  rename any column header defined by yourself"
        '
        'btnColumn
        '
        Me.btnColumn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnColumn.ImageIndex = 11
        Me.btnColumn.Name = "btnColumn"
        Me.btnColumn.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnRenameColumn, Me.btnShowFieldChooser})
        Me.btnColumn.Text = "Grid Column"
        '
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 2
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnColumn, Me.btnSettingGrid, Me.btnPrint, Me.btnPageSettings})
        Me.btnGrid.Text = "Grid"
        '
        'UiButton1
        '
        Me.UiButton1.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.UiButton1.ImageIndex = 3
        Me.UiButton1.ImageList = Me.ImageList1
        Me.UiButton1.Location = New System.Drawing.Point(748, 1)
        Me.UiButton1.Name = "UiButton1"
        Me.UiButton1.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.UiButton1.Size = New System.Drawing.Size(90, 20)
        Me.UiButton1.TabIndex = 4
        Me.UiButton1.Text = "Apply Filter"
        Me.UiButton1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Contains
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 26)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(0, 0)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(979, 29)
        '
        'ExpandablePanel2
        '
        Me.ExpandablePanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel2.Controls.Add(Me.grdPurchaseOrder)
        Me.ExpandablePanel2.Controls.Add(Me.grpRangeDate)
        Me.ExpandablePanel2.Controls.Add(Me.pnlPencapaian)
        Me.ExpandablePanel2.Controls.Add(Me.FilterEditor1)
        Me.ExpandablePanel2.Controls.Add(Me.Bar2)
        Me.ExpandablePanel2.Controls.Add(Me.Bar1)
        Me.ExpandablePanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExpandablePanel2.ExpandButtonVisible = False
        Me.ExpandablePanel2.Location = New System.Drawing.Point(0, 0)
        Me.ExpandablePanel2.Name = "ExpandablePanel2"
        Me.ExpandablePanel2.ShowFocusRectangle = True
        Me.ExpandablePanel2.Size = New System.Drawing.Size(979, 742)
        Me.ExpandablePanel2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandablePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.ExpandablePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel2.Style.GradientAngle = 90
        Me.ExpandablePanel2.TabIndex = 1
        Me.ExpandablePanel2.TitleHeight = 1
        Me.ExpandablePanel2.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel2.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandablePanel2.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel2.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel2.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel2.TitleStyle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExpandablePanel2.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel2.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel2.TitleText = "Data"
        '
        'grpRangeDate
        '
        Me.grpRangeDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grpRangeDate.Controls.Add(Me.btnXCategoryFindPO)
        Me.grpRangeDate.Controls.Add(Me.txtSearchPO)
        Me.grpRangeDate.Controls.Add(Me.btnAplyRange)
        Me.grpRangeDate.Controls.Add(Me.lblPOOrEndDate)
        Me.grpRangeDate.Controls.Add(Me.Label1)
        Me.grpRangeDate.Controls.Add(Me.dtpicUntil)
        Me.grpRangeDate.Controls.Add(Me.dtPicFrom)
        Me.grpRangeDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpRangeDate.Location = New System.Drawing.Point(0, 322)
        Me.grpRangeDate.Name = "grpRangeDate"
        Me.grpRangeDate.Size = New System.Drawing.Size(979, 42)
        Me.grpRangeDate.TabIndex = 22
        Me.grpRangeDate.Text = "PO RANGE DATE"
        Me.grpRangeDate.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnXCategoryFindPO
        '
        Me.btnXCategoryFindPO.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnXCategoryFindPO.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.btnXCategoryFindPO.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Color
        Me.btnXCategoryFindPO.Location = New System.Drawing.Point(12, 13)
        Me.btnXCategoryFindPO.Name = "btnXCategoryFindPO"
        Me.btnXCategoryFindPO.Size = New System.Drawing.Size(136, 23)
        Me.btnXCategoryFindPO.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnbyPO, Me.btnbyRangedDate, Me.btnByRangeDateAndCancelPO, Me.btnByRangeDateAndPlantation})
        Me.btnXCategoryFindPO.TabIndex = 6
        Me.btnXCategoryFindPO.Text = "Find or filter data"
        '
        'btnbyPO
        '
        Me.btnbyPO.GlobalItem = False
        Me.btnbyPO.Name = "btnbyPO"
        Me.btnbyPO.Text = "By PO Number"
        '
        'btnbyRangedDate
        '
        Me.btnbyRangedDate.Checked = True
        Me.btnbyRangedDate.GlobalItem = False
        Me.btnbyRangedDate.Name = "btnbyRangedDate"
        Me.btnbyRangedDate.Text = "By Range Date"
        '
        'btnByRangeDateAndCancelPO
        '
        Me.btnByRangeDateAndCancelPO.GlobalItem = False
        Me.btnByRangeDateAndCancelPO.Name = "btnByRangeDateAndCancelPO"
        Me.btnByRangeDateAndCancelPO.Text = "By Range Date and Canceled PO"
        '
        'btnByRangeDateAndPlantation
        '
        Me.btnByRangeDateAndPlantation.GlobalItem = False
        Me.btnByRangeDateAndPlantation.Name = "btnByRangeDateAndPlantation"
        Me.btnByRangeDateAndPlantation.Text = "By Range Date And Plantation"
        '
        'txtSearchPO
        '
        Me.txtSearchPO.Location = New System.Drawing.Point(499, 13)
        Me.txtSearchPO.Name = "txtSearchPO"
        Me.txtSearchPO.Size = New System.Drawing.Size(200, 20)
        Me.txtSearchPO.TabIndex = 5
        Me.txtSearchPO.Visible = False
        '
        'btnAplyRange
        '
        Me.btnAplyRange.Location = New System.Drawing.Point(721, 12)
        Me.btnAplyRange.Name = "btnAplyRange"
        Me.btnAplyRange.Size = New System.Drawing.Size(75, 23)
        Me.btnAplyRange.TabIndex = 4
        Me.btnAplyRange.Text = "Apply filter"
        Me.btnAplyRange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'lblPOOrEndDate
        '
        Me.lblPOOrEndDate.AutoSize = True
        Me.lblPOOrEndDate.BackColor = System.Drawing.Color.Transparent
        Me.lblPOOrEndDate.Location = New System.Drawing.Point(425, 17)
        Me.lblPOOrEndDate.Name = "lblPOOrEndDate"
        Me.lblPOOrEndDate.Size = New System.Drawing.Size(39, 13)
        Me.lblPOOrEndDate.TabIndex = 3
        Me.lblPOOrEndDate.Text = "UNTIL"
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
        Me.dtpicUntil.Location = New System.Drawing.Point(499, 13)
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
        'pnlPencapaian
        '
        Me.pnlPencapaian.CanvasColor = System.Drawing.SystemColors.Control
        Me.pnlPencapaian.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.pnlPencapaian.Controls.Add(Me.grdPencapaian)
        Me.pnlPencapaian.Controls.Add(Me.PanelEx1)
        Me.pnlPencapaian.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPencapaian.Location = New System.Drawing.Point(0, 55)
        Me.pnlPencapaian.Name = "pnlPencapaian"
        Me.pnlPencapaian.Size = New System.Drawing.Size(979, 267)
        Me.pnlPencapaian.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlPencapaian.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.pnlPencapaian.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.pnlPencapaian.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.pnlPencapaian.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.pnlPencapaian.Style.GradientAngle = 90
        Me.pnlPencapaian.TabIndex = 24
        Me.pnlPencapaian.TitleHeight = 15
        Me.pnlPencapaian.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlPencapaian.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.pnlPencapaian.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.pnlPencapaian.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.pnlPencapaian.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.pnlPencapaian.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.pnlPencapaian.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.pnlPencapaian.TitleStyle.GradientAngle = 90
        Me.pnlPencapaian.TitleText = " "
        '
        'grdPencapaian
        '
        Me.grdPencapaian.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdPencapaian.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPencapaian.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdPencapaian.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.grdPencapaian.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPencapaian.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdPencapaian.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPencapaian.GroupByBoxInfoFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.grdPencapaian.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdPencapaian.GroupRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.grdPencapaian.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdPencapaian.GroupTotalRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.grdPencapaian.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdPencapaian.HeaderFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.grdPencapaian.ImageList = Me.ImageList1
        Me.grdPencapaian.Location = New System.Drawing.Point(0, 48)
        Me.grdPencapaian.Name = "grdPencapaian"
        Me.grdPencapaian.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdPencapaian.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdPencapaian.RecordNavigator = True
        Me.grdPencapaian.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdPencapaian.Size = New System.Drawing.Size(979, 219)
        Me.grdPencapaian.TabIndex = 1
        Me.grdPencapaian.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPencapaian.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.grdPencapaian.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.btnAplyFilterPencapaian)
        Me.PanelEx1.Controls.Add(Me.ButtonSearch1)
        Me.PanelEx1.Controls.Add(Me.cmbFlag)
        Me.PanelEx1.Controls.Add(Me.cmbAgreement)
        Me.PanelEx1.Controls.Add(Me.lblFlag)
        Me.PanelEx1.Controls.Add(Me.Label3)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx1.Location = New System.Drawing.Point(0, 15)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(979, 33)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 4
        '
        'btnAplyFilterPencapaian
        '
        Me.btnAplyFilterPencapaian.Location = New System.Drawing.Point(805, 5)
        Me.btnAplyFilterPencapaian.Name = "btnAplyFilterPencapaian"
        Me.btnAplyFilterPencapaian.Size = New System.Drawing.Size(75, 23)
        Me.btnAplyFilterPencapaian.TabIndex = 8
        Me.btnAplyFilterPencapaian.Text = "Apply filter"
        Me.btnAplyFilterPencapaian.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'ButtonSearch1
        '
        Me.ButtonSearch1.Location = New System.Drawing.Point(508, 7)
        Me.ButtonSearch1.Name = "ButtonSearch1"
        Me.ButtonSearch1.Size = New System.Drawing.Size(17, 18)
        Me.ButtonSearch1.TabIndex = 7
        '
        'cmbFlag
        '
        Me.cmbFlag.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.cmbFlag.HoverMode = Janus.Windows.EditControls.HoverMode.Highlight
        Me.cmbFlag.ItemsFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.cmbFlag.Location = New System.Drawing.Point(570, 6)
        Me.cmbFlag.MaxDropDownItems = 20
        Me.cmbFlag.Name = "cmbFlag"
        Me.cmbFlag.Size = New System.Drawing.Size(210, 20)
        Me.cmbFlag.TabIndex = 6
        Me.cmbFlag.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'cmbAgreement
        '
        Me.cmbAgreement.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.cmbAgreement.HoverMode = Janus.Windows.EditControls.HoverMode.Highlight
        Me.cmbAgreement.ItemsFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.cmbAgreement.Location = New System.Drawing.Point(234, 6)
        Me.cmbAgreement.MaxDropDownItems = 20
        Me.cmbAgreement.Name = "cmbAgreement"
        Me.cmbAgreement.Size = New System.Drawing.Size(267, 20)
        Me.cmbAgreement.StateStyles.FormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbAgreement.TabIndex = 5
        Me.cmbAgreement.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'lblFlag
        '
        Me.lblFlag.AutoSize = True
        Me.lblFlag.Location = New System.Drawing.Point(537, 9)
        Me.lblFlag.Name = "lblFlag"
        Me.lblFlag.Size = New System.Drawing.Size(27, 13)
        Me.lblFlag.TabIndex = 4
        Me.lblFlag.Text = "Flag"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(135, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Agreement NO"
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar2 (Bar2)"
        Me.Bar2.AccessibleName = "Bar2"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.BarType = DevComponents.DotNetBar.eBarType.MenuBar
        Me.Bar2.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaption
        Me.Bar2.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaptionText
        Me.Bar2.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.Bar2.Controls.Add(Me.UiButton1)
        Me.Bar2.Controls.Add(Me.cmbDistributor)
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnManagePO, Me.btnExport, Me.btnEdit, Me.btnRefresh})
        Me.Bar2.Location = New System.Drawing.Point(0, 1)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(979, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 20
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'cmbDistributor
        '
        Me.cmbDistributor.AutoComplete = False
        Me.cmbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        cmbDistributor_DesignTimeLayout.LayoutString = resources.GetString("cmbDistributor_DesignTimeLayout.LayoutString")
        Me.cmbDistributor.DesignTimeLayout = cmbDistributor_DesignTimeLayout
        Me.cmbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.cmbDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.cmbDistributor.Location = New System.Drawing.Point(465, 1)
        Me.cmbDistributor.Name = "cmbDistributor"
        Me.cmbDistributor.SelectedIndex = -1
        Me.cmbDistributor.SelectedItem = Nothing
        Me.cmbDistributor.Size = New System.Drawing.Size(277, 23)
        Me.cmbDistributor.TabIndex = 3
        Me.cmbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.cmbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Bar1
        '
        Me.Bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.Bar1.Location = New System.Drawing.Point(499, 434)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(69, 193)
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar1.TabIndex = 3
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'GridEXPrintDocument2
        '
        Me.GridEXPrintDocument2.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument2.GridEX = Me.grdPencapaian
        Me.GridEXPrintDocument2.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument2.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument2.PrintCellBackground = False
        Me.GridEXPrintDocument2.PrintHierarchical = True
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
        'PageSetupDialog2
        '
        Me.PageSetupDialog2.Document = Me.GridEXPrintDocument2
        '
        'PO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(979, 742)
        Me.Controls.Add(Me.ExpandablePanel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "PURCHASE ORDER"
        CType(Me.grdPurchaseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExpandablePanel2.ResumeLayout(False)
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRangeDate.ResumeLayout(False)
        Me.grpRangeDate.PerformLayout()
        Me.pnlPencapaian.ResumeLayout(False)
        CType(Me.grdPencapaian, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Bar2.ResumeLayout(False)
        Me.Bar2.PerformLayout()
        CType(Me.cmbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents btnApply As DevComponents.DotNetBar.ButtonItem
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents grdPurchaseOrder As Janus.Windows.GridEX.GridEX
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnEdit As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnManagePO As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRenameColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents UiButton1 As Janus.Windows.EditControls.UIButton
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents ExpandablePanel2 As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents grpRangeDate As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Private WithEvents lblPOOrEndDate As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents btnAplyRange As Janus.Windows.EditControls.UIButton
    Private WithEvents cmbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents dtpicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents pnlPencapaian As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents grdPencapaian As Janus.Windows.GridEX.GridEX
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Private WithEvents lblFlag As System.Windows.Forms.Label
    Private WithEvents cmbFlag As Janus.Windows.EditControls.UIComboBox
    Private WithEvents cmbAgreement As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents ButtonSearch1 As DTSProjects.ButtonSearch
    Private WithEvents btnAplyFilterPencapaian As Janus.Windows.EditControls.UIButton
    Friend WithEvents GridEXPrintDocument2 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PrintPreviewDialog2 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents PageSetupDialog2 As System.Windows.Forms.PageSetupDialog
    Private WithEvents btnPOHeaderAndDetail As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPOHeader As DevComponents.DotNetBar.ButtonItem
    Private WithEvents txtSearchPO As System.Windows.Forms.TextBox
    Private WithEvents btnXCategoryFindPO As DevComponents.DotNetBar.ButtonX
    Private WithEvents btnbyPO As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnbyRangedDate As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnByRangeDateAndCancelPO As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnByRangeDateAndPlantation As DevComponents.DotNetBar.ButtonItem

End Class
