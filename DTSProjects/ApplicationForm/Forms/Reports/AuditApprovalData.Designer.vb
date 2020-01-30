<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AuditApprovalData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AuditApprovalData))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.btnTargetPKD = New DevComponents.DotNetBar.ButtonItem
        Me.btnAdjusmentPKD = New DevComponents.DotNetBar.ButtonItem
        Me.btnPriceFM = New DevComponents.DotNetBar.ButtonItem
        Me.btnPricePL = New DevComponents.DotNetBar.ButtonItem
        Me.btnAveragePrice = New DevComponents.DotNetBar.ButtonItem
        Me.btnSalesProgram = New DevComponents.DotNetBar.ButtonItem
        Me.btnDPD = New DevComponents.DotNetBar.ButtonItem
        Me.btnCPDAuto = New DevComponents.DotNetBar.ButtonItem
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSettings = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterEqual = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.LabelItem2 = New DevComponents.DotNetBar.LabelItem
        Me.btnSetPeriodePKD = New DevComponents.DotNetBar.ButtonItem
        Me.LabelItem1 = New DevComponents.DotNetBar.LabelItem
        Me.lblSetFlag1 = New DevComponents.DotNetBar.LabelItem
        Me.cmbFlag = New DevComponents.DotNetBar.ComboBoxItem
        Me.ItemS1 = New DevComponents.Editors.ComboItem
        Me.ItemS2 = New DevComponents.Editors.ComboItem
        Me.ItemQ1 = New DevComponents.Editors.ComboItem
        Me.ItemQ2 = New DevComponents.Editors.ComboItem
        Me.ItemQ3 = New DevComponents.Editors.ComboItem
        Me.ItemQ4 = New DevComponents.Editors.ComboItem
        Me.ItemY = New DevComponents.Editors.ComboItem
        Me.LabelItem3 = New DevComponents.DotNetBar.LabelItem
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ReloadF5ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExportF7ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnlyApprovedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnlyNotApprovedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.grpMainReport = New System.Windows.Forms.GroupBox
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.chkCheckAll = New System.Windows.Forms.CheckBox
        Me.btnApprove = New Janus.Windows.EditControls.UIButton
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.grpMainReport.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
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
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnTargetPKD, Me.btnAdjusmentPKD, Me.btnPriceFM, Me.btnPricePL, Me.btnAveragePrice, Me.btnSalesProgram, Me.btnDPD, Me.btnCPDAuto})
        Me.ItemPanel1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical
        Me.ItemPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(100, 453)
        Me.ItemPanel1.TabIndex = 4
        Me.ItemPanel1.Text = "ItemPanel1"
        '
        'btnTargetPKD
        '
        Me.btnTargetPKD.Name = "btnTargetPKD"
        Me.btnTargetPKD.Text = "Target DPD"
        '
        'btnAdjusmentPKD
        '
        Me.btnAdjusmentPKD.Name = "btnAdjusmentPKD"
        Me.btnAdjusmentPKD.Text = "Adjustment DPD"
        '
        'btnPriceFM
        '
        Me.btnPriceFM.Name = "btnPriceFM"
        Me.btnPriceFM.Text = "Price FM"
        '
        'btnPricePL
        '
        Me.btnPricePL.Name = "btnPricePL"
        Me.btnPricePL.Text = "Price PL"
        '
        'btnAveragePrice
        '
        Me.btnAveragePrice.Name = "btnAveragePrice"
        Me.btnAveragePrice.Text = "Average Price"
        '
        'btnSalesProgram
        '
        Me.btnSalesProgram.Name = "btnSalesProgram"
        Me.btnSalesProgram.Text = "Sales Program"
        '
        'btnDPD
        '
        Me.btnDPD.Name = "btnDPD"
        Me.btnDPD.Text = "DPD"
        '
        'btnCPDAuto
        '
        Me.btnCPDAuto.Name = "btnCPDAuto"
        Me.btnCPDAuto.Text = "CPD Auto"
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
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
        Me.ImageList1.Images.SetKeyName(17, "AttachmentHS.png")
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
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnExport, Me.LabelItem2, Me.btnSetPeriodePKD, Me.LabelItem1, Me.lblSetFlag1, Me.cmbFlag, Me.LabelItem3})
        Me.Bar2.Location = New System.Drawing.Point(100, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(991, 27)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 23
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 2
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnShowFieldChooser, Me.btnSettingGrid, Me.btnPrint, Me.btnPageSettings})
        Me.btnGrid.Text = "Grid"
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
        Me.btnSettingGrid.Tooltip = "show / remove column by draging  / dropping any column in datagrid"
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
            "etting defined by yourseef"
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
        Me.btnFilterEqual.Tooltip = "use this filter to filter data in data grid directly from datagrid with equal cri" & _
            "teria"
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 14
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "E&xport Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
        '
        'LabelItem2
        '
        Me.LabelItem2.BorderType = DevComponents.DotNetBar.eBorderType.None
        Me.LabelItem2.Enabled = False
        Me.LabelItem2.Name = "LabelItem2"
        Me.LabelItem2.Text = "||"
        '
        'btnSetPeriodePKD
        '
        Me.btnSetPeriodePKD.Name = "btnSetPeriodePKD"
        Me.btnSetPeriodePKD.Text = "Set Periode (PKD)"
        '
        'LabelItem1
        '
        Me.LabelItem1.BorderType = DevComponents.DotNetBar.eBorderType.None
        Me.LabelItem1.Enabled = False
        Me.LabelItem1.Name = "LabelItem1"
        Me.LabelItem1.Text = "||"
        '
        'lblSetFlag1
        '
        Me.lblSetFlag1.BorderType = DevComponents.DotNetBar.eBorderType.None
        Me.lblSetFlag1.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.lblSetFlag1.Enabled = False
        Me.lblSetFlag1.Name = "lblSetFlag1"
        Me.lblSetFlag1.Text = "Set Flag"
        '
        'cmbFlag
        '
        Me.cmbFlag.ComboWidth = 45
        Me.cmbFlag.ItemHeight = 16
        Me.cmbFlag.Items.AddRange(New Object() {Me.ItemS1, Me.ItemS2, Me.ItemQ1, Me.ItemQ2, Me.ItemQ3, Me.ItemQ4, Me.ItemY})
        Me.cmbFlag.Name = "cmbFlag"
        '
        'ItemS1
        '
        Me.ItemS1.Text = "S1"
        '
        'ItemS2
        '
        Me.ItemS2.Text = "S2"
        '
        'ItemQ1
        '
        Me.ItemQ1.Text = "Q1"
        '
        'ItemQ2
        '
        Me.ItemQ2.Text = "Q2"
        '
        'ItemQ3
        '
        Me.ItemQ3.Text = "Q3"
        '
        'ItemQ4
        '
        Me.ItemQ4.Text = "Q4"
        '
        'ItemY
        '
        Me.ItemY.Text = "Y"
        '
        'LabelItem3
        '
        Me.LabelItem3.BorderType = DevComponents.DotNetBar.eBorderType.None
        Me.LabelItem3.Enabled = False
        Me.LabelItem3.Name = "LabelItem3"
        Me.LabelItem3.Text = "||"
        '
        'GridEX1
        '
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AutoEdit = True
        Me.GridEX1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX1.ContextMenuStrip = Me.ContextMenuStrip1
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
        Me.GridEX1.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Location = New System.Drawing.Point(3, 16)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Size = New System.Drawing.Size(985, 348)
        Me.GridEX1.TabIndex = 5
        Me.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReloadF5ToolStripMenuItem, Me.ExportF7ToolStripMenuItem, Me.OnlyApprovedToolStripMenuItem, Me.OnlyNotApprovedToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(169, 92)
        '
        'ReloadF5ToolStripMenuItem
        '
        Me.ReloadF5ToolStripMenuItem.Name = "ReloadF5ToolStripMenuItem"
        Me.ReloadF5ToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.ReloadF5ToolStripMenuItem.Text = "Reload(F5)"
        '
        'ExportF7ToolStripMenuItem
        '
        Me.ExportF7ToolStripMenuItem.Name = "ExportF7ToolStripMenuItem"
        Me.ExportF7ToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.ExportF7ToolStripMenuItem.Text = "Export (F7)"
        '
        'OnlyApprovedToolStripMenuItem
        '
        Me.OnlyApprovedToolStripMenuItem.Name = "OnlyApprovedToolStripMenuItem"
        Me.OnlyApprovedToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.OnlyApprovedToolStripMenuItem.Text = "Only Approved"
        '
        'OnlyNotApprovedToolStripMenuItem
        '
        Me.OnlyNotApprovedToolStripMenuItem.Checked = True
        Me.OnlyNotApprovedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.OnlyNotApprovedToolStripMenuItem.Name = "OnlyNotApprovedToolStripMenuItem"
        Me.OnlyNotApprovedToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.OnlyNotApprovedToolStripMenuItem.Text = "Not yet Approved"
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.Transparent
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Equal
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(100, 27)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(0, 0)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(991, 29)
        Me.FilterEditor1.SortFieldList = False
        '
        'grpMainReport
        '
        Me.grpMainReport.Controls.Add(Me.GridEX1)
        Me.grpMainReport.Controls.Add(Me.PanelEx1)
        Me.grpMainReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpMainReport.Location = New System.Drawing.Point(100, 56)
        Me.grpMainReport.Name = "grpMainReport"
        Me.grpMainReport.Size = New System.Drawing.Size(991, 397)
        Me.grpMainReport.TabIndex = 25
        Me.grpMainReport.TabStop = False
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.chkCheckAll)
        Me.PanelEx1.Controls.Add(Me.btnApprove)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx1.Location = New System.Drawing.Point(3, 364)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(985, 30)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 6
        '
        'chkCheckAll
        '
        Me.chkCheckAll.AutoSize = True
        Me.chkCheckAll.Location = New System.Drawing.Point(3, 6)
        Me.chkCheckAll.Name = "chkCheckAll"
        Me.chkCheckAll.Size = New System.Drawing.Size(71, 17)
        Me.chkCheckAll.TabIndex = 1
        Me.chkCheckAll.Text = "Check All"
        Me.chkCheckAll.UseVisualStyleBackColor = True
        '
        'btnApprove
        '
        Me.btnApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApprove.ImageList = Me.ImageList1
        Me.btnApprove.Location = New System.Drawing.Point(894, 4)
        Me.btnApprove.Name = "btnApprove"
        Me.btnApprove.Size = New System.Drawing.Size(85, 23)
        Me.btnApprove.TabIndex = 0
        Me.btnApprove.Text = "Approve"
        Me.btnApprove.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'AuditApprovalData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1091, 453)
        Me.Controls.Add(Me.grpMainReport)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Controls.Add(Me.ItemPanel1)
        Me.Name = "AuditApprovalData"
        Me.Text = "(Audit) Approval Form"
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.grpMainReport.ResumeLayout(False)
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Private WithEvents btnPriceFM As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAveragePrice As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSalesProgram As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnTargetPKD As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAdjusmentPKD As DevComponents.DotNetBar.ButtonItem
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnDPD As DevComponents.DotNetBar.ButtonItem
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents grpMainReport As System.Windows.Forms.GroupBox
    Private WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Private WithEvents btnApprove As Janus.Windows.EditControls.UIButton
    Private WithEvents btnSetPeriodePKD As DevComponents.DotNetBar.ButtonItem
    Private WithEvents chkCheckAll As System.Windows.Forms.CheckBox
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents lblSetFlag1 As DevComponents.DotNetBar.LabelItem
    Private WithEvents cmbFlag As DevComponents.DotNetBar.ComboBoxItem
    Private WithEvents ItemS1 As DevComponents.Editors.ComboItem
    Private WithEvents ItemS2 As DevComponents.Editors.ComboItem
    Private WithEvents ItemQ1 As DevComponents.Editors.ComboItem
    Private WithEvents ItemQ2 As DevComponents.Editors.ComboItem
    Private WithEvents ItemQ3 As DevComponents.Editors.ComboItem
    Private WithEvents ItemQ4 As DevComponents.Editors.ComboItem
    Private WithEvents ItemY As DevComponents.Editors.ComboItem
    Friend WithEvents LabelItem2 As DevComponents.DotNetBar.LabelItem
    Friend WithEvents LabelItem1 As DevComponents.DotNetBar.LabelItem
    Friend WithEvents LabelItem3 As DevComponents.DotNetBar.LabelItem
    Private WithEvents btnPricePL As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExportF7ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnlyApprovedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnlyNotApprovedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ReloadF5ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnCPDAuto As DevComponents.DotNetBar.ButtonItem

End Class
