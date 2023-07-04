<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GonDetailData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GonDetailData))
        Dim grdHeader_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim UiComboBoxItem1 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem2 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem3 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnlyWinGonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PendingGONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.POStatusCompletedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.PageSetupDialog2 = New System.Windows.Forms.PageSetupDialog
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.grdHeader = New Janus.Windows.GridEX.GridEX
        Me.xpgFilterDate = New SteepValley.Windows.Forms.XPGradientPanel
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.btnCatSPPB = New DevComponents.DotNetBar.ButtonItem
        Me.btnCatGON = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustom = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilteDate = New Janus.Windows.EditControls.UIButton
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.lblFrom = New System.Windows.Forms.Label
        Me.dtpicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.lblUntil = New System.Windows.Forms.Label
        Me.cmbFilterBy = New Janus.Windows.EditControls.UIComboBox
        Me.txtFind = New System.Windows.Forms.TextBox
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.grdHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xpgFilterDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
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
        Me.ImageList1.Images.SetKeyName(15, "Add.bmp")
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowDataToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(131, 26)
        '
        'ShowDataToolStripMenuItem
        '
        Me.ShowDataToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnlyWinGonToolStripMenuItem, Me.AllToolStripMenuItem, Me.PendingGONToolStripMenuItem, Me.POStatusCompletedToolStripMenuItem})
        Me.ShowDataToolStripMenuItem.Name = "ShowDataToolStripMenuItem"
        Me.ShowDataToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.ShowDataToolStripMenuItem.Text = "Show Data"
        '
        'OnlyWinGonToolStripMenuItem
        '
        Me.OnlyWinGonToolStripMenuItem.Checked = True
        Me.OnlyWinGonToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.OnlyWinGonToolStripMenuItem.Name = "OnlyWinGonToolStripMenuItem"
        Me.OnlyWinGonToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.OnlyWinGonToolStripMenuItem.Text = "Only With Gon"
        '
        'AllToolStripMenuItem
        '
        Me.AllToolStripMenuItem.Name = "AllToolStripMenuItem"
        Me.AllToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.AllToolStripMenuItem.Text = "All"
        '
        'PendingGONToolStripMenuItem
        '
        Me.PendingGONToolStripMenuItem.Name = "PendingGONToolStripMenuItem"
        Me.PendingGONToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.PendingGONToolStripMenuItem.Text = "Pending GON"
        '
        'POStatusCompletedToolStripMenuItem
        '
        Me.POStatusCompletedToolStripMenuItem.Name = "POStatusCompletedToolStripMenuItem"
        Me.POStatusCompletedToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.POStatusCompletedToolStripMenuItem.Text = "PO Status Completed"
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
        'PageSetupDialog2
        '
        Me.PageSetupDialog2.Document = Me.GridEXPrintDocument1
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.IncludeFormatStyle = False
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Contains
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 25)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(0, 0)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(856, 29)
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
        Me.Bar2.TabIndex = 26
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grdHeader)
        Me.Panel1.Controls.Add(Me.xpgFilterDate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(856, 399)
        Me.Panel1.TabIndex = 27
        '
        'grdHeader
        '
        Me.grdHeader.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdHeader.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdHeader.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdHeader_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdHeader.DesignTimeLayout = grdHeader_DesignTimeLayout
        Me.grdHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdHeader.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdHeader.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdHeader.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdHeader.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdHeader.GroupByBoxVisible = False
        Me.grdHeader.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdHeader.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdHeader.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdHeader.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdHeader.Location = New System.Drawing.Point(0, 28)
        Me.grdHeader.Name = "grdHeader"
        Me.grdHeader.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdHeader.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdHeader.Size = New System.Drawing.Size(856, 371)
        Me.grdHeader.TabIndex = 30
        Me.grdHeader.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdHeader.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'xpgFilterDate
        '
        Me.xpgFilterDate.Controls.Add(Me.ItemPanel1)
        Me.xpgFilterDate.Controls.Add(Me.btnFilteDate)
        Me.xpgFilterDate.Controls.Add(Me.dtPicUntil)
        Me.xpgFilterDate.Controls.Add(Me.lblFrom)
        Me.xpgFilterDate.Controls.Add(Me.dtpicFrom)
        Me.xpgFilterDate.Controls.Add(Me.lblUntil)
        Me.xpgFilterDate.Controls.Add(Me.cmbFilterBy)
        Me.xpgFilterDate.Controls.Add(Me.txtFind)
        Me.xpgFilterDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.xpgFilterDate.EndColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.xpgFilterDate.Gradient = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.xpgFilterDate.Location = New System.Drawing.Point(0, 0)
        Me.xpgFilterDate.Name = "xpgFilterDate"
        Me.xpgFilterDate.Size = New System.Drawing.Size(856, 28)
        Me.xpgFilterDate.StartColor = System.Drawing.SystemColors.MenuBar
        Me.xpgFilterDate.TabIndex = 31
        '
        'ItemPanel1
        '
        '
        '
        '
        Me.ItemPanel1.BackgroundStyle.BackColor = System.Drawing.SystemColors.MenuBar
        Me.ItemPanel1.BackgroundStyle.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
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
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCatSPPB, Me.btnCatGON, Me.btnCustom})
        Me.ItemPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(195, 28)
        Me.ItemPanel1.TabIndex = 26
        Me.ItemPanel1.Text = "ItemPanel1"
        '
        'btnCatSPPB
        '
        Me.btnCatSPPB.Name = "btnCatSPPB"
        Me.btnCatSPPB.Text = "SPPB Date"
        '
        'btnCatGON
        '
        Me.btnCatGON.Name = "btnCatGON"
        Me.btnCatGON.Text = "GON Date"
        '
        'btnCustom
        '
        Me.btnCustom.Checked = True
        Me.btnCustom.Name = "btnCustom"
        Me.btnCustom.Text = "Custom"
        '
        'btnFilteDate
        '
        Me.btnFilteDate.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnFilteDate.ImageIndex = 3
        Me.btnFilteDate.Location = New System.Drawing.Point(672, 4)
        Me.btnFilteDate.Name = "btnFilteDate"
        Me.btnFilteDate.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnFilteDate.Size = New System.Drawing.Size(85, 20)
        Me.btnFilteDate.TabIndex = 2
        Me.btnFilteDate.Text = "Apply Filter"
        Me.btnFilteDate.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
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
        Me.dtPicUntil.DropDownCalendar.FirstMonth = New Date(2022, 12, 1, 0, 0, 0, 0)
        Me.dtPicUntil.DropDownCalendar.Name = ""
        Me.dtPicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicUntil.Location = New System.Drawing.Point(478, 4)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(174, 20)
        Me.dtPicUntil.TabIndex = 4
        Me.dtPicUntil.Value = New Date(2014, 3, 17, 0, 0, 0, 0)
        Me.dtPicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(207, 7)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(38, 14)
        Me.lblFrom.TabIndex = 1
        Me.lblFrom.Text = "FROM"
        '
        'dtpicFrom
        '
        Me.dtpicFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtpicFrom.CustomFormat = "dd MMMM yyyy"
        Me.dtpicFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpicFrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtpicFrom.DropDownCalendar.FirstMonth = New Date(2022, 12, 1, 0, 0, 0, 0)
        Me.dtpicFrom.DropDownCalendar.Name = ""
        Me.dtpicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtpicFrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtpicFrom.Location = New System.Drawing.Point(267, 4)
        Me.dtpicFrom.Name = "dtpicFrom"
        Me.dtpicFrom.ShowTodayButton = False
        Me.dtpicFrom.Size = New System.Drawing.Size(157, 20)
        Me.dtpicFrom.TabIndex = 2
        Me.dtpicFrom.Value = New Date(2014, 3, 17, 0, 0, 0, 0)
        Me.dtpicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'lblUntil
        '
        Me.lblUntil.BackColor = System.Drawing.Color.Transparent
        Me.lblUntil.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUntil.Location = New System.Drawing.Point(431, 8)
        Me.lblUntil.Name = "lblUntil"
        Me.lblUntil.Size = New System.Drawing.Size(74, 14)
        Me.lblUntil.TabIndex = 3
        Me.lblUntil.Text = "UNTIL"
        '
        'cmbFilterBy
        '
        Me.cmbFilterBy.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        UiComboBoxItem1.FormatStyle.Alpha = 0
        UiComboBoxItem1.IsSeparator = False
        UiComboBoxItem1.Text = "PO_NUMBER"
        UiComboBoxItem1.Value = "PO_NUMBER"
        UiComboBoxItem2.FormatStyle.Alpha = 0
        UiComboBoxItem2.IsSeparator = False
        UiComboBoxItem2.Text = "SPPB_NUMBER"
        UiComboBoxItem2.Value = "SPPB_NUMBER"
        UiComboBoxItem3.FormatStyle.Alpha = 0
        UiComboBoxItem3.IsSeparator = False
        UiComboBoxItem3.Text = "GON_NUMBER"
        UiComboBoxItem3.Value = "GON_NUMBER"
        Me.cmbFilterBy.Items.AddRange(New Janus.Windows.EditControls.UIComboBoxItem() {UiComboBoxItem1, UiComboBoxItem2, UiComboBoxItem3})
        Me.cmbFilterBy.ItemsFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbFilterBy.Location = New System.Drawing.Point(269, 4)
        Me.cmbFilterBy.Name = "cmbFilterBy"
        Me.cmbFilterBy.Size = New System.Drawing.Size(152, 20)
        Me.cmbFilterBy.StateStyles.FormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbFilterBy.TabIndex = 32
        Me.cmbFilterBy.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'txtFind
        '
        Me.txtFind.Location = New System.Drawing.Point(510, 4)
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(141, 20)
        Me.txtFind.TabIndex = 33
        '
        'GonDetailData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(856, 453)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Name = "GonDetailData"
        Me.Text = "Detail GON Data"
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xpgFilterDate.ResumeLayout(False)
        Me.xpgFilterDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnlyWinGonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PendingGONToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents POStatusCompletedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents PageSetupDialog2 As System.Windows.Forms.PageSetupDialog
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grdHeader As Janus.Windows.GridEX.GridEX
    Friend WithEvents xpgFilterDate As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Private WithEvents btnCatGON As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCatSPPB As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustom As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnFilteDate As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtpicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents lblUntil As System.Windows.Forms.Label
    Private WithEvents cmbFilterBy As Janus.Windows.EditControls.UIComboBox
    Private WithEvents txtFind As System.Windows.Forms.TextBox

End Class
