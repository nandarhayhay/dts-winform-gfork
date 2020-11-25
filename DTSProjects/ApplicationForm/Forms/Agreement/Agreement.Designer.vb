<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Agreement
    Inherits DTSProjects.BaseBigForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If MyBase.MayDisposed = False Then
            While MyBase.MayDisposed = False
                If MyBase.MayDisposed = True Then
                    Exit While
                End If
            End While
        End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Agreement))
        Dim grdAgreement_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grdAgreement = New Janus.Windows.GridEX.GridEX
        Me.chkShowAll = New Janus.Windows.EditControls.UICheckBox
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnAplyrange = New Janus.Windows.EditControls.UIButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnRename = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSettings = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterEqual = New DevComponents.DotNetBar.ButtonItem
        Me.btnManage = New DevComponents.DotNetBar.ButtonItem
        Me.btnDistAgreement = New DevComponents.DotNetBar.ButtonItem
        Me.btnAgreeDetail = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.Bar3 = New DevComponents.DotNetBar.Bar
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        CType(Me.grdAgreement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Bar2.SuspendLayout()
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
        Me.ImageList1.Images.SetKeyName(12, "printer.ico")
        Me.ImageList1.Images.SetKeyName(13, "Export.bmp")
        Me.ImageList1.Images.SetKeyName(14, "TextEdit.png")
        Me.ImageList1.Images.SetKeyName(15, "View.jpg")
        Me.ImageList1.Images.SetKeyName(16, "DB_Refresh.ico")
        Me.ImageList1.Images.SetKeyName(17, "PageSetup.BMP")
        '
        'grdAgreement
        '
        Me.grdAgreement.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAgreement.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAgreement.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdAgreement.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        grdAgreement_DesignTimeLayout.LayoutString = resources.GetString("grdAgreement_DesignTimeLayout.LayoutString")
        Me.grdAgreement.DesignTimeLayout = grdAgreement_DesignTimeLayout
        Me.grdAgreement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAgreement.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdAgreement.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdAgreement.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdAgreement.GroupByBoxVisible = False
        Me.grdAgreement.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdAgreement.GroupRowFormatStyle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.grdAgreement.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdAgreement.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdAgreement.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.grdAgreement.Hierarchical = True
        Me.grdAgreement.ImageList = Me.ImageList1
        Me.grdAgreement.Location = New System.Drawing.Point(0, 91)
        Me.grdAgreement.Name = "grdAgreement"
        Me.grdAgreement.RecordNavigator = True
        Me.grdAgreement.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdAgreement.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAgreement.Size = New System.Drawing.Size(1028, 469)
        Me.grdAgreement.TabIndex = 3
        Me.grdAgreement.TableHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAgreement.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAgreement.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.grdAgreement.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'chkShowAll
        '
        Me.chkShowAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.chkShowAll.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkShowAll.Location = New System.Drawing.Point(0, 70)
        Me.chkShowAll.Name = "chkShowAll"
        Me.chkShowAll.Size = New System.Drawing.Size(1028, 21)
        Me.chkShowAll.TabIndex = 21
        Me.chkShowAll.Text = "Show all"
        Me.chkShowAll.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
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
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnManage, Me.btnExport, Me.btnRefresh})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1028, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 20
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnAplyrange
        '
        Me.btnAplyrange.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAplyrange.ImageIndex = 3
        Me.btnAplyrange.ImageList = Me.ImageList1
        Me.btnAplyrange.Location = New System.Drawing.Point(780, 2)
        Me.btnAplyrange.Name = "btnAplyrange"
        Me.btnAplyrange.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnAplyrange.Size = New System.Drawing.Size(90, 20)
        Me.btnAplyrange.TabIndex = 10
        Me.btnAplyrange.Text = "Apply Filter"
        Me.btnAplyrange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(593, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 15)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "End"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(409, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 15)
        Me.Label1.TabIndex = 8
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
        Me.dtPicUntil.Location = New System.Drawing.Point(630, 2)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(141, 23)
        Me.dtPicUntil.TabIndex = 7
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
        Me.dtPicFrom.Location = New System.Drawing.Point(445, 2)
        Me.dtPicFrom.Name = "dtPicFrom"
        Me.dtPicFrom.ShowTodayButton = False
        Me.dtPicFrom.Size = New System.Drawing.Size(143, 23)
        Me.dtPicFrom.TabIndex = 6
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
        Me.btnColumn.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnRename, Me.btnShowFieldChooser})
        Me.btnColumn.Text = "Grid Column"
        '
        'btnRename
        '
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Text = "Rename Column"
        Me.btnRename.Tooltip = "use this button to  rename any column header defined by yourself"
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
        Me.btnPrint.ImageIndex = 12
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Text = "Print data Grid"
        Me.btnPrint.Tooltip = "print datagrid,all visible rows in datagrid will be printed"
        '
        'btnPageSettings
        '
        Me.btnPageSettings.ImageIndex = 17
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
        Me.btnFilterEqual.Tooltip = "use this filter to filter data in data grid directly from datagrid with Contains " & _
            "criteria"
        '
        'btnManage
        '
        Me.btnManage.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnManage.Name = "btnManage"
        Me.btnManage.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnDistAgreement, Me.btnAgreeDetail})
        Me.btnManage.Text = "Manage Data"
        '
        'btnDistAgreement
        '
        Me.btnDistAgreement.Name = "btnDistAgreement"
        Me.btnDistAgreement.Text = "Distributor Agreement"
        '
        'btnAgreeDetail
        '
        Me.btnAgreeDetail.Name = "btnAgreeDetail"
        Me.btnAgreeDetail.Text = "Agreement Detail"
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 13
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 16
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.Tooltip = "Reload data and refresh grid"
        '
        'Bar3
        '
        Me.Bar3.Location = New System.Drawing.Point(362, 144)
        Me.Bar3.Name = "Bar3"
        Me.Bar3.Size = New System.Drawing.Size(38, 25)
        Me.Bar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar3.TabIndex = 4
        Me.Bar3.TabStop = False
        Me.Bar3.Text = "Bar3"
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
        Me.GridEXPrintDocument1.GridEX = Me.grdAgreement
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.GridEX = Me.grdAgreement
        Me.GridEXExporter1.IncludeFormatStyle = False
        Me.GridEXExporter1.SheetName = "Distributor Agreement"
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
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
        Me.FilterEditor1.SourceControl = Me.grdAgreement
        '
        'Agreement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 560)
        Me.Controls.Add(Me.grdAgreement)
        Me.Controls.Add(Me.chkShowAll)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Controls.Add(Me.Bar3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Agreement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AGREEMENT"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdAgreement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Bar2.ResumeLayout(False)
        Me.Bar2.PerformLayout()
        CType(Me.Bar3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents grdAgreement As Janus.Windows.GridEX.GridEX
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRename As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnManage As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnDistAgreement As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAgreeDetail As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents Bar3 As DevComponents.DotNetBar.Bar
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents chkShowAll As Janus.Windows.EditControls.UICheckBox
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents btnAplyrange As Janus.Windows.EditControls.UIButton
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo
End Class
