<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GONWithoutPOMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GONWithoutPOMaster))
        Me.AdvancedTManager1 = New DTSProjects.AdvancedTManager
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OnlyWinGonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PendingGONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.POStatusCompletedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
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
        Me.btnAddNew = New DevComponents.DotNetBar.ButtonItem
        Me.btnEdit = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrintSPPB = New DevComponents.DotNetBar.ButtonItem
        Me.btnCurrentSelection = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrintcustoms = New DevComponents.DotNetBar.ButtonItem
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.btnVarFG = New DevComponents.DotNetBar.ButtonItem
        Me.btnSampleUnreg = New DevComponents.DotNetBar.ButtonItem
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AdvancedTManager1
        '
        Me.AdvancedTManager1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.AdvancedTManager1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdvancedTManager1.Location = New System.Drawing.Point(0, 54)
        Me.AdvancedTManager1.Name = "AdvancedTManager1"
        Me.AdvancedTManager1.Size = New System.Drawing.Size(1024, 423)
        Me.AdvancedTManager1.TabIndex = 0
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
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnExport, Me.btnRefresh, Me.btnAddNew, Me.btnEdit, Me.btnPrintSPPB})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1024, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 24
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
        'btnAddNew
        '
        Me.btnAddNew.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnAddNew.ImageIndex = 15
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnVarFG, Me.btnSampleUnreg})
        Me.btnAddNew.Text = "Add New"
        '
        'btnEdit
        '
        Me.btnEdit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnEdit.ImageIndex = 3
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Text = "Edit Row"
        Me.btnEdit.Tooltip = "Edit selected row in datagrid "
        '
        'btnPrintSPPB
        '
        Me.btnPrintSPPB.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrintSPPB.ImageIndex = 9
        Me.btnPrintSPPB.Name = "btnPrintSPPB"
        Me.btnPrintSPPB.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCurrentSelection, Me.btnPrintcustoms})
        Me.btnPrintSPPB.Text = "Printing SPPB"
        '
        'btnCurrentSelection
        '
        Me.btnCurrentSelection.Name = "btnCurrentSelection"
        Me.btnCurrentSelection.Text = "Current Sell"
        '
        'btnPrintcustoms
        '
        Me.btnPrintcustoms.Name = "btnPrintcustoms"
        Me.btnPrintcustoms.Text = "Customs"
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
        Me.FilterEditor1.Size = New System.Drawing.Size(1024, 29)
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.IncludeFormatStyle = False
        '
        'btnVarFG
        '
        Me.btnVarFG.Name = "btnVarFG"
        Me.btnVarFG.Text = "Various FG"
        '
        'btnSampleUnreg
        '
        Me.btnSampleUnreg.Name = "btnSampleUnreg"
        Me.btnSampleUnreg.Text = "Sample Product"
        '
        'GONWithoutPOMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1024, 477)
        Me.Controls.Add(Me.AdvancedTManager1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Name = "GONWithoutPOMaster"
        Me.Text = "NON SALES DISTRIBUTOR GON"
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AdvancedTManager1 As DTSProjects.AdvancedTManager
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
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
    Private WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnEdit As DevComponents.DotNetBar.ButtonItem
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnlyWinGonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PendingGONToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents POStatusCompletedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPrintSPPB As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCurrentSelection As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrintcustoms As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnVarFG As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnSampleUnreg As DevComponents.DotNetBar.ButtonItem

End Class
