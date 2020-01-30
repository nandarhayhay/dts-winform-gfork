<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiscAgreement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DiscAgreement))
        Dim cmbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnShowAgreement = New DevComponents.DotNetBar.ButtonX
        Me.btnMustgenerate = New DevComponents.DotNetBar.ButtonItem
        Me.btnQ1 = New DevComponents.DotNetBar.ButtonItem
        Me.btnQ2 = New DevComponents.DotNetBar.ButtonItem
        Me.btnQ3 = New DevComponents.DotNetBar.ButtonItem
        Me.btnQ4 = New DevComponents.DotNetBar.ButtonItem
        Me.btnS1 = New DevComponents.DotNetBar.ButtonItem
        Me.btnS2 = New DevComponents.DotNetBar.ButtonItem
        Me.btnYear = New DevComponents.DotNetBar.ButtonItem
        Me.btngeneratedAgreement = New DevComponents.DotNetBar.ButtonItem
        Me.btnHQ1 = New DevComponents.DotNetBar.ButtonItem
        Me.btnHQ2 = New DevComponents.DotNetBar.ButtonItem
        Me.btnHQ3 = New DevComponents.DotNetBar.ButtonItem
        Me.btnHQ4 = New DevComponents.DotNetBar.ButtonItem
        Me.btnHS1 = New DevComponents.DotNetBar.ButtonItem
        Me.btnHS2 = New DevComponents.DotNetBar.ButtonItem
        Me.btnHYear = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterAgreement = New DTSProjects.ButtonSearch
        Me.cmbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
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
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnCompute = New DevComponents.DotNetBar.ButtonItem
        Me.rdbCDPeriods1 = New DevComponents.DotNetBar.ButtonItem
        Me.rdbCDPeriods2 = New DevComponents.DotNetBar.ButtonItem
        Me.rdbCDPeriods3 = New DevComponents.DotNetBar.ButtonItem
        Me.rdbCDPeriods4 = New DevComponents.DotNetBar.ButtonItem
        Me.rdbCDYearly = New DevComponents.DotNetBar.ButtonItem
        Me.btnViewCompute = New DevComponents.DotNetBar.ButtonItem
        Me.btnPeriods1 = New DevComponents.DotNetBar.ButtonItem
        Me.btnPeriods2 = New DevComponents.DotNetBar.ButtonItem
        Me.btnPeriods3 = New DevComponents.DotNetBar.ButtonItem
        Me.btnPeriods4 = New DevComponents.DotNetBar.ButtonItem
        Me.btnYearly = New DevComponents.DotNetBar.ButtonItem
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Bar2.SuspendLayout()
        CType(Me.cmbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar2 (Bar2)"
        Me.Bar2.AccessibleName = "Bar2"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaption
        Me.Bar2.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaptionText
        Me.Bar2.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.Bar2.Controls.Add(Me.btnShowAgreement)
        Me.Bar2.Controls.Add(Me.btnFilterAgreement)
        Me.Bar2.Controls.Add(Me.cmbDistributor)
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnExport, Me.btnRefresh, Me.btnCompute, Me.btnViewCompute})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1028, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 21
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnShowAgreement
        '
        Me.btnShowAgreement.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnShowAgreement.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.btnShowAgreement.Location = New System.Drawing.Point(828, 2)
        Me.btnShowAgreement.Name = "btnShowAgreement"
        Me.btnShowAgreement.Size = New System.Drawing.Size(100, 20)
        Me.btnShowAgreement.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnMustgenerate, Me.btngeneratedAgreement})
        Me.btnShowAgreement.TabIndex = 6
        Me.btnShowAgreement.Text = "Show Agreement"
        '
        'btnMustgenerate
        '
        Me.btnMustgenerate.GlobalItem = False
        Me.btnMustgenerate.Name = "btnMustgenerate"
        Me.btnMustgenerate.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnQ1, Me.btnQ2, Me.btnQ3, Me.btnQ4, Me.btnS1, Me.btnS2, Me.btnYear})
        Me.btnMustgenerate.Text = "Should be generated"
        '
        'btnQ1
        '
        Me.btnQ1.Name = "btnQ1"
        Me.btnQ1.Text = "Q1"
        '
        'btnQ2
        '
        Me.btnQ2.Name = "btnQ2"
        Me.btnQ2.Text = "Q2"
        '
        'btnQ3
        '
        Me.btnQ3.Name = "btnQ3"
        Me.btnQ3.Text = "Q3"
        '
        'btnQ4
        '
        Me.btnQ4.Name = "btnQ4"
        Me.btnQ4.Text = "Q4"
        '
        'btnS1
        '
        Me.btnS1.Name = "btnS1"
        Me.btnS1.Text = "S1"
        '
        'btnS2
        '
        Me.btnS2.Name = "btnS2"
        Me.btnS2.Text = "S2"
        '
        'btnYear
        '
        Me.btnYear.Name = "btnYear"
        Me.btnYear.Text = "YEAR"
        '
        'btngeneratedAgreement
        '
        Me.btngeneratedAgreement.GlobalItem = False
        Me.btngeneratedAgreement.Name = "btngeneratedAgreement"
        Me.btngeneratedAgreement.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnHQ1, Me.btnHQ2, Me.btnHQ3, Me.btnHQ4, Me.btnHS1, Me.btnHS2, Me.btnHYear})
        Me.btngeneratedAgreement.Text = "Generated Agreement"
        '
        'btnHQ1
        '
        Me.btnHQ1.Name = "btnHQ1"
        Me.btnHQ1.Text = "Q1"
        '
        'btnHQ2
        '
        Me.btnHQ2.Name = "btnHQ2"
        Me.btnHQ2.Text = "Q2"
        '
        'btnHQ3
        '
        Me.btnHQ3.Name = "btnHQ3"
        Me.btnHQ3.Text = "Q3"
        '
        'btnHQ4
        '
        Me.btnHQ4.Name = "btnHQ4"
        Me.btnHQ4.Text = "Q4"
        '
        'btnHS1
        '
        Me.btnHS1.Name = "btnHS1"
        Me.btnHS1.Text = "S1"
        '
        'btnHS2
        '
        Me.btnHS2.Name = "btnHS2"
        Me.btnHS2.Text = "S2"
        '
        'btnHYear
        '
        Me.btnHYear.Name = "btnHYear"
        Me.btnHYear.Text = "YEAR"
        '
        'btnFilterAgreement
        '
        Me.btnFilterAgreement.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterAgreement.Location = New System.Drawing.Point(806, 3)
        Me.btnFilterAgreement.Name = "btnFilterAgreement"
        Me.btnFilterAgreement.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterAgreement.TabIndex = 5
        '
        'cmbDistributor
        '
        Me.cmbDistributor.AllowDrop = True
        Me.cmbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        cmbDistributor_DesignTimeLayout.LayoutString = resources.GetString("cmbDistributor_DesignTimeLayout.LayoutString")
        Me.cmbDistributor.DesignTimeLayout = cmbDistributor_DesignTimeLayout
        Me.cmbDistributor.DisplayMember = "AGREEMENT_NO"
        Me.cmbDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.cmbDistributor.Location = New System.Drawing.Point(561, 1)
        Me.cmbDistributor.Name = "cmbDistributor"
        Me.cmbDistributor.SelectedIndex = -1
        Me.cmbDistributor.SelectedItem = Nothing
        Me.cmbDistributor.Size = New System.Drawing.Size(239, 23)
        Me.cmbDistributor.TabIndex = 4
        Me.cmbDistributor.ValueMember = "AGREEMENT_NO"
        Me.cmbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
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
        Me.btnSettingGrid.ImageIndex = 2
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
        Me.btnPageSettings.ImageIndex = 13
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
        'btnCompute
        '
        Me.btnCompute.Name = "btnCompute"
        Me.btnCompute.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.rdbCDPeriods1, Me.rdbCDPeriods2, Me.rdbCDPeriods3, Me.rdbCDPeriods4, Me.rdbCDYearly})
        Me.btnCompute.Text = "Compute Discount"
        '
        'rdbCDPeriods1
        '
        Me.rdbCDPeriods1.Name = "rdbCDPeriods1"
        '
        'rdbCDPeriods2
        '
        Me.rdbCDPeriods2.Name = "rdbCDPeriods2"
        '
        'rdbCDPeriods3
        '
        Me.rdbCDPeriods3.Name = "rdbCDPeriods3"
        '
        'rdbCDPeriods4
        '
        Me.rdbCDPeriods4.Name = "rdbCDPeriods4"
        '
        'rdbCDYearly
        '
        Me.rdbCDYearly.Name = "rdbCDYearly"
        Me.rdbCDYearly.Text = "Yearly Discount"
        '
        'btnViewCompute
        '
        Me.btnViewCompute.Name = "btnViewCompute"
        Me.btnViewCompute.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnPeriods1, Me.btnPeriods2, Me.btnPeriods3, Me.btnPeriods4, Me.btnYearly})
        Me.btnViewCompute.Text = "View Computed Discount"
        '
        'btnPeriods1
        '
        Me.btnPeriods1.Name = "btnPeriods1"
        '
        'btnPeriods2
        '
        Me.btnPeriods2.Name = "btnPeriods2"
        '
        'btnPeriods3
        '
        Me.btnPeriods3.Name = "btnPeriods3"
        '
        'btnPeriods4
        '
        Me.btnPeriods4.Name = "btnPeriods4"
        '
        'btnYearly
        '
        Me.btnYearly.Name = "btnYearly"
        Me.btnYearly.Text = "Yearly Discount"
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Contains
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 25)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(16, 45)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(1028, 45)
        Me.FilterEditor1.SortFieldList = False
        Me.FilterEditor1.SourceControl = Me.GridEX1
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 70)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(1028, 676)
        Me.GridEX1.TabIndex = 23
        Me.GridEX1.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Bar1
        '
        Me.Bar1.Location = New System.Drawing.Point(386, 163)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(75, 24)
        Me.Bar1.TabIndex = 25
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.RestoreDirectory = True
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.GridEX = Me.GridEX1
        Me.GridEXPrintDocument1.PageFooterFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEXPrintDocument1.PageFooterFormatStyle.FontSize = 10.0!
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.FontSize = 10.0!
        Me.GridEXPrintDocument1.RepeatHeaders = False
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
        'GridEXExporter1
        '
        Me.GridEXExporter1.GridEX = Me.GridEX1
        Me.GridEXExporter1.IncludeFormatStyle = False
        Me.GridEXExporter1.SheetName = "Disc_Agreement"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'DiscAgreement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Controls.Add(Me.Bar1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DiscAgreement"
        Me.Text = "Discount  Agreement"
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Bar2.ResumeLayout(False)
        Me.Bar2.PerformLayout()
        CType(Me.cmbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
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
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents cmbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents btnPeriods3 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnYearly As DevComponents.DotNetBar.ButtonItem
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents Timer1 As System.Windows.Forms.Timer
    Private WithEvents btnPeriods1 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPeriods2 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPeriods4 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCompute As DevComponents.DotNetBar.ButtonItem
    Private WithEvents rdbCDPeriods1 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents rdbCDPeriods2 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents rdbCDPeriods3 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents rdbCDPeriods4 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents rdbCDYearly As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnViewCompute As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowAgreement As DevComponents.DotNetBar.ButtonX
    Private WithEvents btnMustgenerate As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterAgreement As DTSProjects.ButtonSearch
    Private WithEvents btngeneratedAgreement As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnQ1 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnQ2 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnQ3 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnQ4 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnS1 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnS2 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnYear As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnHQ1 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnHQ2 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnHQ3 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnHQ4 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnHS1 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnHS2 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnHYear As DevComponents.DotNetBar.ButtonItem
End Class
