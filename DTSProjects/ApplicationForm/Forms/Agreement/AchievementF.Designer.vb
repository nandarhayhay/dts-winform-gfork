<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AchievementF
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AchievementF))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim UiComboBoxItem1 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem2 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem3 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim chkDistributors_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX2_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.cmbRoundUpByPackSize = New System.Windows.Forms.ToolStripMenuItem
        Me.cmbRoundupByCategory = New System.Windows.Forms.ToolStripMenuItem
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
        Me.cmbDPDType = New DevComponents.DotNetBar.ComboBoxItem
        Me.cmbItemUnchoosed = New DevComponents.Editors.ComboItem
        Me.DPDTypeNufarm = New DevComponents.Editors.ComboItem
        Me.DPDTypeRoundUp = New DevComponents.Editors.ComboItem
        Me.btnFlag = New DevComponents.DotNetBar.ButtonItem
        Me.btnRecomputeF1 = New DevComponents.DotNetBar.ButtonItem
        Me.btnRecomputeF2 = New DevComponents.DotNetBar.ButtonItem
        Me.btnRecomputeF3 = New DevComponents.DotNetBar.ButtonItem
        Me.cmbAchRoundup = New DevComponents.Editors.ComboItem
        Me.cmbAchNufarm = New DevComponents.Editors.ComboItem
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ExpandablePanel1 = New DevComponents.DotNetBar.ExpandablePanel
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.btnGeneratedDPD = New DevComponents.DotNetBar.ButtonItem
        Me.btnPerAchOnly = New DevComponents.DotNetBar.ButtonItem
        Me.cmbFlag = New Janus.Windows.EditControls.UIComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.chkDistributors = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.btnAplyRange = New Janus.Windows.EditControls.UIButton
        Me.btnSearchAgreement = New DTSProjects.ButtonSearch
        Me.mcbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.btnSearchDistributor = New DTSProjects.ButtonSearch
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.pnlCheckBox = New System.Windows.Forms.Panel
        Me.ChkFilterDetail = New System.Windows.Forms.CheckBox
        Me.GridEX2 = New Janus.Windows.GridEX.GridEX
        Me.ExpandableSplitter2 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExpandablePanel1.SuspendLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnlCheckBox.SuspendLayout()
        CType(Me.GridEX2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.IncludeFormatStyle = False
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
        Me.ImageList1.Images.SetKeyName(6, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(7, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(8, "gridColumn.png")
        Me.ImageList1.Images.SetKeyName(9, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(10, "printer.ico")
        Me.ImageList1.Images.SetKeyName(11, "Export.bmp")
        Me.ImageList1.Images.SetKeyName(12, "DB_Refresh.ico")
        Me.ImageList1.Images.SetKeyName(13, "PageSetup.BMP")
        Me.ImageList1.Images.SetKeyName(14, "generator.ico")
        Me.ImageList1.Images.SetKeyName(15, "Discount.ICO")
        Me.ImageList1.Images.SetKeyName(16, "Flag.ico")
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
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowDataToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(131, 26)
        '
        'ShowDataToolStripMenuItem
        '
        Me.ShowDataToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmbRoundUpByPackSize, Me.cmbRoundupByCategory})
        Me.ShowDataToolStripMenuItem.Name = "ShowDataToolStripMenuItem"
        Me.ShowDataToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.ShowDataToolStripMenuItem.Text = "Show Data"
        '
        'cmbRoundUpByPackSize
        '
        Me.cmbRoundUpByPackSize.Name = "cmbRoundUpByPackSize"
        Me.cmbRoundUpByPackSize.Size = New System.Drawing.Size(244, 22)
        Me.cmbRoundUpByPackSize.Text = "Achievement R By PackSize"
        '
        'cmbRoundupByCategory
        '
        Me.cmbRoundupByCategory.Name = "cmbRoundupByCategory"
        Me.cmbRoundupByCategory.Size = New System.Drawing.Size(244, 22)
        Me.cmbRoundupByCategory.Text = "Achievement R By Size Category"
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
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnExport, Me.btnRefresh, Me.cmbDPDType, Me.btnFlag})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1151, 27)
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
        Me.btnColumn.ImageIndex = 8
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
        Me.btnPrint.ImageIndex = 10
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Text = "Print data Grid"
        Me.btnPrint.Tooltip = "print datagrid,all visible rows in datagrid will be printed"
        '
        'btnPageSettings
        '
        Me.btnPageSettings.ImageIndex = 13
        Me.btnPageSettings.Name = "btnPageSettings"
        Me.btnPageSettings.Text = "Page Settings"
        Me.btnPageSettings.Tooltip = "setting datagrid page ,use this setting if you want to print datagrid with page e" & _
            "ditting defined by yourself "
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
        Me.btnExport.ImageIndex = 11
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 12
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.Tooltip = "Reload all possible data row  and refresh grid"
        '
        'cmbDPDType
        '
        Me.cmbDPDType.AlwaysShowCaption = True
        Me.cmbDPDType.ComboWidth = 110
        Me.cmbDPDType.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmbDPDType.ItemHeight = 16
        Me.cmbDPDType.Items.AddRange(New Object() {Me.cmbItemUnchoosed, Me.DPDTypeNufarm, Me.DPDTypeRoundUp})
        Me.cmbDPDType.Name = "cmbDPDType"
        Me.cmbDPDType.PreventEnterBeep = True
        '
        'cmbItemUnchoosed
        '
        Me.cmbItemUnchoosed.Text = "<==SELECT==>"
        Me.cmbItemUnchoosed.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'DPDTypeNufarm
        '
        Me.DPDTypeNufarm.Text = "Nufarm"
        '
        'DPDTypeRoundUp
        '
        Me.DPDTypeRoundUp.Text = "ROUNDUP"
        '
        'btnFlag
        '
        Me.btnFlag.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnFlag.ImageIndex = 16
        Me.btnFlag.Name = "btnFlag"
        Me.btnFlag.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnRecomputeF1, Me.btnRecomputeF2, Me.btnRecomputeF3})
        Me.btnFlag.Text = "(Re)Compute Flag"
        '
        'btnRecomputeF1
        '
        Me.btnRecomputeF1.Name = "btnRecomputeF1"
        Me.btnRecomputeF1.Text = "(Re)Compute F1"
        '
        'btnRecomputeF2
        '
        Me.btnRecomputeF2.Name = "btnRecomputeF2"
        Me.btnRecomputeF2.Text = "(Re)Compute F2"
        '
        'btnRecomputeF3
        '
        Me.btnRecomputeF3.Name = "btnRecomputeF3"
        Me.btnRecomputeF3.Text = "(Re)Compute F3"
        '
        'cmbAchRoundup
        '
        Me.cmbAchRoundup.Text = "ROUNDUP"
        '
        'cmbAchNufarm
        '
        Me.cmbAchNufarm.Text = "Nufarm Product"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GridEX1)
        Me.Panel1.Controls.Add(Me.ExpandablePanel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 72)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1151, 210)
        Me.Panel1.TabIndex = 24
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GridLines = Janus.Windows.GridEX.GridLines.Horizontal
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 51)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(1151, 159)
        Me.GridEX1.TabIndex = 38
        Me.GridEX1.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ExpandablePanel1
        '
        Me.ExpandablePanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel1.Controls.Add(Me.ItemPanel1)
        Me.ExpandablePanel1.Controls.Add(Me.cmbFlag)
        Me.ExpandablePanel1.Controls.Add(Me.Label3)
        Me.ExpandablePanel1.Controls.Add(Me.chkDistributors)
        Me.ExpandablePanel1.Controls.Add(Me.btnAplyRange)
        Me.ExpandablePanel1.Controls.Add(Me.btnSearchAgreement)
        Me.ExpandablePanel1.Controls.Add(Me.mcbDistributor)
        Me.ExpandablePanel1.Controls.Add(Me.btnSearchDistributor)
        Me.ExpandablePanel1.Controls.Add(Me.Label1)
        Me.ExpandablePanel1.Controls.Add(Me.Label2)
        Me.ExpandablePanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ExpandablePanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExpandablePanel1.Name = "ExpandablePanel1"
        Me.ExpandablePanel1.Size = New System.Drawing.Size(1151, 51)
        Me.ExpandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.ExpandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel1.Style.GradientAngle = 90
        Me.ExpandablePanel1.TabIndex = 37
        Me.ExpandablePanel1.TitleHeight = 16
        Me.ExpandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal
        Me.ExpandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel1.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel1.TitleText = "Agreement Distributor"
        '
        'ItemPanel1
        '
        '
        '
        '
        Me.ItemPanel1.BackgroundStyle.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ItemPanel1.BackgroundStyle.BackColor2 = System.Drawing.Color.Transparent
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
        Me.ItemPanel1.Images = Me.ImageList1
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGeneratedDPD, Me.btnPerAchOnly})
        Me.ItemPanel1.Location = New System.Drawing.Point(794, 18)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(197, 28)
        Me.ItemPanel1.TabIndex = 9
        Me.ItemPanel1.Text = "ItemPanel1"
        '
        'btnGeneratedDPD
        '
        Me.btnGeneratedDPD.Name = "btnGeneratedDPD"
        Me.btnGeneratedDPD.Text = "Generated DPD"
        '
        'btnPerAchOnly
        '
        Me.btnPerAchOnly.Checked = True
        Me.btnPerAchOnly.Name = "btnPerAchOnly"
        Me.btnPerAchOnly.Text = "% Achivement Only"
        '
        'cmbFlag
        '
        Me.cmbFlag.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        UiComboBoxItem1.FormatStyle.Alpha = 0
        UiComboBoxItem1.IsSeparator = False
        UiComboBoxItem1.Text = "4 MONTHS PERIODE 1"
        UiComboBoxItem1.Value = "4 MONTHS PERIODE 1"
        UiComboBoxItem2.FormatStyle.Alpha = 0
        UiComboBoxItem2.IsSeparator = False
        UiComboBoxItem2.Text = "4 MONTHS PERIODE 2"
        UiComboBoxItem2.Value = "4 MONTHS PERIODE 1"
        UiComboBoxItem3.FormatStyle.Alpha = 0
        UiComboBoxItem3.IsSeparator = False
        UiComboBoxItem3.Text = "4 MONTHS PERIODE 3"
        UiComboBoxItem3.Value = "4 MONTHS PERIODE 3"
        Me.cmbFlag.Items.AddRange(New Janus.Windows.EditControls.UIComboBoxItem() {UiComboBoxItem1, UiComboBoxItem2, UiComboBoxItem3})
        Me.cmbFlag.Location = New System.Drawing.Point(43, 20)
        Me.cmbFlag.Name = "cmbFlag"
        Me.cmbFlag.Size = New System.Drawing.Size(146, 20)
        Me.cmbFlag.TabIndex = 10
        Me.cmbFlag.Text = "<< Choose Flag >>"
        Me.cmbFlag.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Flag"
        '
        'chkDistributors
        '
        Me.chkDistributors.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        chkDistributors_DesignTimeLayout.LayoutString = resources.GetString("chkDistributors_DesignTimeLayout.LayoutString")
        Me.chkDistributors.DesignTimeLayout = chkDistributors_DesignTimeLayout
        Me.chkDistributors.DropDownDisplayMember = "AGREEMENT_NO"
        Me.chkDistributors.DropDownValueMember = "AGREEMENT_NO"
        Me.chkDistributors.Location = New System.Drawing.Point(558, 22)
        Me.chkDistributors.Name = "chkDistributors"
        Me.chkDistributors.SaveSettings = False
        Me.chkDistributors.Size = New System.Drawing.Size(208, 20)
        Me.chkDistributors.TabIndex = 8
        Me.chkDistributors.ValuesDataMember = Nothing
        Me.chkDistributors.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnAplyRange
        '
        Me.btnAplyRange.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAplyRange.Location = New System.Drawing.Point(1021, 24)
        Me.btnAplyRange.Name = "btnAplyRange"
        Me.btnAplyRange.Size = New System.Drawing.Size(75, 18)
        Me.btnAplyRange.TabIndex = 7
        Me.btnAplyRange.Text = "Apply filter"
        Me.btnAplyRange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnSearchAgreement
        '
        Me.btnSearchAgreement.Location = New System.Drawing.Point(770, 22)
        Me.btnSearchAgreement.Name = "btnSearchAgreement"
        Me.btnSearchAgreement.Size = New System.Drawing.Size(18, 18)
        Me.btnSearchAgreement.TabIndex = 5
        '
        'mcbDistributor
        '
        Me.mcbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbDistributor_DesignTimeLayout.LayoutString = resources.GetString("mcbDistributor_DesignTimeLayout.LayoutString")
        Me.mcbDistributor.DesignTimeLayout = mcbDistributor_DesignTimeLayout
        Me.mcbDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbDistributor.Location = New System.Drawing.Point(280, 19)
        Me.mcbDistributor.Name = "mcbDistributor"
        Me.mcbDistributor.SelectedIndex = -1
        Me.mcbDistributor.SelectedItem = Nothing
        Me.mcbDistributor.Size = New System.Drawing.Size(180, 20)
        Me.mcbDistributor.TabIndex = 1
        Me.mcbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnSearchDistributor
        '
        Me.btnSearchDistributor.Location = New System.Drawing.Point(462, 21)
        Me.btnSearchDistributor.Name = "btnSearchDistributor"
        Me.btnSearchDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchDistributor.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(195, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "DISTRIBUTOR"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(483, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "AGREEMENT"
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Contains
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 27)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(16, 45)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(1151, 45)
        Me.FilterEditor1.SortFieldList = False
        Me.FilterEditor1.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlCheckBox)
        Me.Panel2.Controls.Add(Me.GridEX2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 286)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1151, 167)
        Me.Panel2.TabIndex = 25
        '
        'pnlCheckBox
        '
        Me.pnlCheckBox.Controls.Add(Me.ChkFilterDetail)
        Me.pnlCheckBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCheckBox.Location = New System.Drawing.Point(0, 0)
        Me.pnlCheckBox.Name = "pnlCheckBox"
        Me.pnlCheckBox.Size = New System.Drawing.Size(1151, 26)
        Me.pnlCheckBox.TabIndex = 28
        '
        'ChkFilterDetail
        '
        Me.ChkFilterDetail.AutoSize = True
        Me.ChkFilterDetail.Location = New System.Drawing.Point(3, 5)
        Me.ChkFilterDetail.Name = "ChkFilterDetail"
        Me.ChkFilterDetail.Size = New System.Drawing.Size(287, 17)
        Me.ChkFilterDetail.TabIndex = 0
        Me.ChkFilterDetail.Text = "Filter Detail with header(row on header data is selected)"
        Me.ChkFilterDetail.UseVisualStyleBackColor = True
        '
        'GridEX2
        '
        Me.GridEX2.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX2.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.AutoEdit = True
        Me.GridEX2.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX2.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX2_DesignTimeLayout.LayoutString = resources.GetString("GridEX2_DesignTimeLayout.LayoutString")
        Me.GridEX2.DesignTimeLayout = GridEX2_DesignTimeLayout
        Me.GridEX2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX2.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX2.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX2.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.GridEX2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX2.GridLines = Janus.Windows.GridEX.GridLines.Horizontal
        Me.GridEX2.GroupByBoxVisible = False
        Me.GridEX2.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX2.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX2.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX2.ImageList = Me.ImageList1
        Me.GridEX2.Location = New System.Drawing.Point(0, 0)
        Me.GridEX2.Name = "GridEX2"
        Me.GridEX2.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX2.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX2.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.Size = New System.Drawing.Size(1151, 167)
        Me.GridEX2.TabIndex = 27
        Me.GridEX2.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEX2.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX2.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX2.WatermarkImage.Image = CType(resources.GetObject("GridEX2.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX2.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ExpandableSplitter2
        '
        Me.ExpandableSplitter2.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ExpandableSplitter2.ExpandableControl = Me.Panel2
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
        Me.ExpandableSplitter2.Location = New System.Drawing.Point(0, 282)
        Me.ExpandableSplitter2.Name = "ExpandableSplitter2"
        Me.ExpandableSplitter2.Size = New System.Drawing.Size(1151, 4)
        Me.ExpandableSplitter2.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter2.TabIndex = 28
        Me.ExpandableSplitter2.TabStop = False
        '
        'AchievementF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1151, 453)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ExpandableSplitter2)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Name = "AchievementF"
        Me.Text = "4 Months Periode Achievement"
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExpandablePanel1.ResumeLayout(False)
        Me.ExpandablePanel1.PerformLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.pnlCheckBox.ResumeLayout(False)
        Me.pnlCheckBox.PerformLayout()
        CType(Me.GridEX2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents Timer1 As System.Windows.Forms.Timer
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Private WithEvents ShowDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmbRoundUpByPackSize As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmbRoundupByCategory As System.Windows.Forms.ToolStripMenuItem
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
    Private WithEvents btnFlag As DevComponents.DotNetBar.ButtonItem

    Private WithEvents btnRecomputeF1 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRecomputeF2 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRecomputeF3 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Friend WithEvents ExpandablePanel1 As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents cmbFlag As Janus.Windows.EditControls.UIComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents chkDistributors As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Private WithEvents btnAplyRange As Janus.Windows.EditControls.UIButton
    Private WithEvents btnSearchAgreement As DTSProjects.ButtonSearch
    Private WithEvents mcbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents btnSearchDistributor As DTSProjects.ButtonSearch
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents GridEX2 As Janus.Windows.GridEX.GridEX
    Private WithEvents ExpandableSplitter2 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents cmbAchRoundup As DevComponents.Editors.ComboItem
    Private WithEvents cmbAchNufarm As DevComponents.Editors.ComboItem
    Private WithEvents pnlCheckBox As System.Windows.Forms.Panel
    Private WithEvents ChkFilterDetail As System.Windows.Forms.CheckBox
    Private WithEvents cmbDPDType As DevComponents.DotNetBar.ComboBoxItem
    Private WithEvents cmbItemUnchoosed As DevComponents.Editors.ComboItem
    Private WithEvents DPDTypeNufarm As DevComponents.Editors.ComboItem
    Private WithEvents DPDTypeRoundUp As DevComponents.Editors.ComboItem
    Private WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Private WithEvents btnGeneratedDPD As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPerAchOnly As DevComponents.DotNetBar.ButtonItem

End Class
