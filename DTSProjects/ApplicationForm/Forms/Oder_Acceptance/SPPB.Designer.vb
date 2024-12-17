<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SPPB
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SPPB))
        Dim cmbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnSearchDistributor = New DTSProjects.ButtonSearch
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
        Me.btnAddNew = New DevComponents.DotNetBar.ButtonItem
        Me.btnNewSPPB = New DevComponents.DotNetBar.ButtonItem
        Me.btnGonAfterSignedByDistributor = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnEditSPPB = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowData = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowSPPB = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowReturningGON = New DevComponents.DotNetBar.ButtonItem
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.xpgData = New SteepValley.Windows.Forms.XPGradientPanel
        Me.btnDailyGON = New DevComponents.DotNetBar.ButtonItem
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
        Me.ImageList1.Images.SetKeyName(19, "Item.ICO")
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar2 (Bar2)"
        Me.Bar2.AccessibleName = "Bar2"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaptionText
        Me.Bar2.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaption
        Me.Bar2.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.Bar2.Controls.Add(Me.btnSearchDistributor)
        Me.Bar2.Controls.Add(Me.cmbDistributor)
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnAddNew, Me.btnExport, Me.btnRefresh, Me.btnEditSPPB, Me.btnShowData})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1078, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 22
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnSearchDistributor
        '
        Me.btnSearchDistributor.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchDistributor.Location = New System.Drawing.Point(785, 2)
        Me.btnSearchDistributor.Name = "btnSearchDistributor"
        Me.btnSearchDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchDistributor.TabIndex = 2
        '
        'cmbDistributor
        '
        Me.cmbDistributor.AutoComplete = False
        Me.cmbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        cmbDistributor_DesignTimeLayout.LayoutString = resources.GetString("cmbDistributor_DesignTimeLayout.LayoutString")
        Me.cmbDistributor.DesignTimeLayout = cmbDistributor_DesignTimeLayout
        Me.cmbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.cmbDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.cmbDistributor.Location = New System.Drawing.Point(577, 0)
        Me.cmbDistributor.Name = "cmbDistributor"
        Me.cmbDistributor.SelectedIndex = -1
        Me.cmbDistributor.SelectedItem = Nothing
        Me.cmbDistributor.Size = New System.Drawing.Size(202, 23)
        Me.cmbDistributor.TabIndex = 0
        Me.cmbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.cmbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
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
        Me.btnAddNew.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnNewSPPB, Me.btnGonAfterSignedByDistributor})
        Me.btnAddNew.Text = "Add New"
        Me.btnAddNew.Tooltip = "add new item data"
        '
        'btnNewSPPB
        '
        Me.btnNewSPPB.Name = "btnNewSPPB"
        Me.btnNewSPPB.Text = "SPPB"
        '
        'btnGonAfterSignedByDistributor
        '
        Me.btnGonAfterSignedByDistributor.Name = "btnGonAfterSignedByDistributor"
        Me.btnGonAfterSignedByDistributor.Text = "Returning GON"
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
        'btnEditSPPB
        '
        Me.btnEditSPPB.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnEditSPPB.ImageIndex = 5
        Me.btnEditSPPB.Name = "btnEditSPPB"
        Me.btnEditSPPB.Text = "Edit Row"
        '
        'btnShowData
        '
        Me.btnShowData.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnShowData.ImageIndex = 19
        Me.btnShowData.Name = "btnShowData"
        Me.btnShowData.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnShowSPPB, Me.btnShowReturningGON, Me.btnDailyGON})
        Me.btnShowData.Text = "ShowData"
        '
        'btnShowSPPB
        '
        Me.btnShowSPPB.Name = "btnShowSPPB"
        Me.btnShowSPPB.Text = "SPBB"
        '
        'btnShowReturningGON
        '
        Me.btnShowReturningGON.Name = "btnShowReturningGON"
        Me.btnShowReturningGON.Text = "Returning GON"
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
        Me.FilterEditor1.MinSize = New System.Drawing.Size(16, 45)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(1078, 46)
        Me.FilterEditor1.SortFieldList = False
        Me.FilterEditor1.Visible = False
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.IncludeFormatStyle = False
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
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
        'Bar1
        '
        Me.Bar1.Location = New System.Drawing.Point(299, 96)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(75, 25)
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar1.TabIndex = 24
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'xpgData
        '
        Me.xpgData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xpgData.EndColor = System.Drawing.SystemColors.MenuBar
        Me.xpgData.Location = New System.Drawing.Point(0, 71)
        Me.xpgData.Name = "xpgData"
        Me.xpgData.Size = New System.Drawing.Size(1078, 382)
        Me.xpgData.StartColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.xpgData.TabIndex = 28
        Me.xpgData.Watermark = Global.DTSProjects.My.Resources.Resources.Splash
        '
        'btnDailyGON
        '
        Me.btnDailyGON.Name = "btnDailyGON"
        Me.btnDailyGON.Text = "Daily GON"
        '
        'SPPB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1078, 453)
        Me.Controls.Add(Me.xpgData)
        Me.Controls.Add(Me.Bar1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SPPB"
        Me.Text = "SPPB"
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Bar2.ResumeLayout(False)
        Me.Bar2.PerformLayout()
        CType(Me.cmbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents xpgData As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Friend WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Friend WithEvents cmbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnEditSPPB As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnGonAfterSignedByDistributor As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnNewSPPB As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowData As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowSPPB As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnShowReturningGON As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSearchDistributor As DTSProjects.ButtonSearch
    Friend WithEvents btnDailyGON As DevComponents.DotNetBar.ButtonItem

End Class
