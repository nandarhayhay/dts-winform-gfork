<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportGrid
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportGrid))
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
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem1 = New DevComponents.DotNetBar.ButtonItem
        Me.btnDistributorReport = New DevComponents.DotNetBar.ButtonItem
        Me.btnOAReport = New DevComponents.DotNetBar.ButtonItem
        Me.btnSPPBReport = New DevComponents.DotNetBar.ButtonItem
        Me.btnThirdParty = New DevComponents.DotNetBar.ButtonItem
        Me.btnTargetAgreement = New DevComponents.DotNetBar.ButtonItem
        Me.btnPODispro = New DevComponents.DotNetBar.ButtonItem
        Me.btnPODisproByBrand = New DevComponents.DotNetBar.ButtonItem
        Me.btnDPRD = New DevComponents.DotNetBar.ButtonItem
        Me.btnAccrue = New DevComponents.DotNetBar.ButtonItem
        Me.RecapNasionalDPRD = New DevComponents.DotNetBar.ButtonItem
        Me.btnSummaryPlantation = New DevComponents.DotNetBar.ButtonItem
        Me.btnReportSales = New DevComponents.DotNetBar.ButtonItem
        Me.btnReportDDDR = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem2 = New DevComponents.DotNetBar.ButtonItem
        Me.btnCardView = New DevComponents.DotNetBar.ButtonItem
        Me.btnSingleCard = New DevComponents.DotNetBar.ButtonItem
        Me.btnTableView = New DevComponents.DotNetBar.ButtonItem
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.ExpandablePanel1 = New DevComponents.DotNetBar.ExpandablePanel
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ImageList1.Images.SetKeyName(18, "")
        Me.ImageList1.Images.SetKeyName(19, "")
        Me.ImageList1.Images.SetKeyName(20, "")
        Me.ImageList1.Images.SetKeyName(21, "")
        Me.ImageList1.Images.SetKeyName(22, "purchase_order.ICO")
        Me.ImageList1.Images.SetKeyName(23, "DPRD.ico")
        Me.ImageList1.Images.SetKeyName(24, "Plantation.ico")
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar2 (Bar2)"
        Me.Bar2.AccessibleName = "Bar2"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.BarType = DevComponents.DotNetBar.eBarType.MenuBar
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnExport, Me.btnRefresh, Me.ButtonItem1, Me.ButtonItem2})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1028, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 21
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
        Me.btnExport.Text = "Export Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 15
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.Tooltip = "Reload data and refresh grid"
        '
        'ButtonItem1
        '
        Me.ButtonItem1.Name = "ButtonItem1"
        Me.ButtonItem1.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnDistributorReport, Me.btnOAReport, Me.btnSPPBReport, Me.btnThirdParty, Me.btnTargetAgreement, Me.btnPODispro, Me.btnPODisproByBrand, Me.btnDPRD, Me.btnAccrue, Me.RecapNasionalDPRD, Me.btnSummaryPlantation, Me.btnReportSales, Me.btnReportDDDR})
        Me.ButtonItem1.Text = "Show Data"
        '
        'btnDistributorReport
        '
        Me.btnDistributorReport.ImageIndex = 17
        Me.btnDistributorReport.Name = "btnDistributorReport"
        Me.btnDistributorReport.Text = "Distributor Report"
        '
        'btnOAReport
        '
        Me.btnOAReport.ImageIndex = 18
        Me.btnOAReport.Name = "btnOAReport"
        Me.btnOAReport.Text = "OA REPORT"
        Me.btnOAReport.Visible = False
        '
        'btnSPPBReport
        '
        Me.btnSPPBReport.ImageIndex = 19
        Me.btnSPPBReport.Name = "btnSPPBReport"
        Me.btnSPPBReport.Text = "SPPB Report"
        '
        'btnThirdParty
        '
        Me.btnThirdParty.ImageIndex = 20
        Me.btnThirdParty.Name = "btnThirdParty"
        Me.btnThirdParty.Text = "PO_DO_Third_Party"
        Me.btnThirdParty.Visible = False
        '
        'btnTargetAgreement
        '
        Me.btnTargetAgreement.ImageIndex = 21
        Me.btnTargetAgreement.Name = "btnTargetAgreement"
        Me.btnTargetAgreement.Text = "Target Agreement"
        '
        'btnPODispro
        '
        Me.btnPODispro.ImageIndex = 22
        Me.btnPODispro.Name = "btnPODispro"
        Me.btnPODispro.Text = "PO and Dispro Distributor"
        '
        'btnPODisproByBrand
        '
        Me.btnPODisproByBrand.ImageIndex = 22
        Me.btnPODisproByBrand.Name = "btnPODisproByBrand"
        Me.btnPODisproByBrand.Text = "Summary Dispro By Brand"
        Me.btnPODisproByBrand.Visible = False
        '
        'btnDPRD
        '
        Me.btnDPRD.ImageIndex = 23
        Me.btnDPRD.Name = "btnDPRD"
        Me.btnDPRD.Text = "DPRD(obsolete)"
        Me.btnDPRD.Visible = False
        '
        'btnAccrue
        '
        Me.btnAccrue.ImageIndex = 17
        Me.btnAccrue.Name = "btnAccrue"
        Me.btnAccrue.Text = "Accrue"
        Me.btnAccrue.Visible = False
        '
        'RecapNasionalDPRD
        '
        Me.RecapNasionalDPRD.ImageIndex = 17
        Me.RecapNasionalDPRD.Name = "RecapNasionalDPRD"
        Me.RecapNasionalDPRD.Text = "Recap DPRD(National)"
        Me.RecapNasionalDPRD.Visible = False
        '
        'btnSummaryPlantation
        '
        Me.btnSummaryPlantation.ImageIndex = 24
        Me.btnSummaryPlantation.Name = "btnSummaryPlantation"
        Me.btnSummaryPlantation.Text = "Delivered PO by Plantation"
        '
        'btnReportSales
        '
        Me.btnReportSales.Name = "btnReportSales"
        Me.btnReportSales.Text = "SalesProgram/Sales PO by TM"
        '
        'btnReportDDDR
        '
        Me.btnReportDDDR.Name = "btnReportDDDR"
        Me.btnReportDDDR.Text = "Report DD DR"
        '
        'ButtonItem2
        '
        Me.ButtonItem2.Name = "ButtonItem2"
        Me.ButtonItem2.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCardView, Me.btnSingleCard, Me.btnTableView})
        Me.ButtonItem2.Text = "View"
        '
        'btnCardView
        '
        Me.btnCardView.Name = "btnCardView"
        Me.btnCardView.Text = "CardView"
        '
        'btnSingleCard
        '
        Me.btnSingleCard.Name = "btnSingleCard"
        Me.btnSingleCard.Text = "Single Card"
        '
        'btnTableView
        '
        Me.btnTableView.Name = "btnTableView"
        Me.btnTableView.Text = "TableView"
        '
        'FilterEditor1
        '
        Me.FilterEditor1.BackColor = System.Drawing.Color.Transparent
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Equal
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 0)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(0, 0)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(200, 100)
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
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        'ExpandablePanel1
        '
        Me.ExpandablePanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExpandablePanel1.Location = New System.Drawing.Point(0, 25)
        Me.ExpandablePanel1.Name = "ExpandablePanel1"
        Me.ExpandablePanel1.Size = New System.Drawing.Size(1028, 717)
        Me.ExpandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.ExpandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel1.Style.GradientAngle = 90
        Me.ExpandablePanel1.TabIndex = 23
        Me.ExpandablePanel1.TitleHeight = 20
        Me.ExpandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal
        Me.ExpandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel1.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel1.TitleText = "Type of Report"
        '
        'ReportGrid
        '
        Me.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1028, 742)
        Me.Controls.Add(Me.ExpandablePanel1)
        Me.Controls.Add(Me.Bar2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ReportGrid"
        Me.Text = "REPORT"
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
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
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem1 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents ButtonItem2 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCardView As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSingleCard As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnTableView As DevComponents.DotNetBar.ButtonItem

    Private WithEvents btnSPPBReport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnThirdParty As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnTargetAgreement As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPODispro As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPODisproByBrand As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnDistributorReport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnOAReport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnDPRD As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ExpandablePanel1 As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents btnAccrue As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents RecapNasionalDPRD As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSummaryPlantation As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnReportSales As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnReportDDDR As DevComponents.DotNetBar.ButtonItem

End Class
