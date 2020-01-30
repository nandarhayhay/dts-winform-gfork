<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AVGPrice
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
        Dim chkBrand_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AVGPrice))
        Me.grpBrand = New Janus.Windows.EditControls.UIGroupBox
        Me.chkBrand = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.pnlEntry = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.grpgrpAvgPrice = New Janus.Windows.EditControls.UIGroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtAVGpricePL = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtAvgPrice = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.plGridData = New System.Windows.Forms.Panel
        Me.TManager1 = New DTSProjects.AdvancedTManager
        Me.pnlEntryAndFilter = New System.Windows.Forms.Panel
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterEqual = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnAddNew = New DevComponents.DotNetBar.ButtonItem
        Me.btnSave = New DevComponents.DotNetBar.ButtonItem
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        CType(Me.grpBrand, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBrand.SuspendLayout()
        Me.pnlEntry.SuspendLayout()
        CType(Me.grpgrpAvgPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpgrpAvgPrice.SuspendLayout()
        Me.plGridData.SuspendLayout()
        Me.pnlEntryAndFilter.SuspendLayout()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpBrand
        '
        Me.grpBrand.Controls.Add(Me.chkBrand)
        Me.grpBrand.Location = New System.Drawing.Point(220, 6)
        Me.grpBrand.Name = "grpBrand"
        Me.grpBrand.Size = New System.Drawing.Size(301, 48)
        Me.grpBrand.TabIndex = 3
        Me.grpBrand.Text = "Brand"
        Me.grpBrand.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'chkBrand
        '
        Me.chkBrand.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        chkBrand_DesignTimeLayout.LayoutString = resources.GetString("chkBrand_DesignTimeLayout.LayoutString")
        Me.chkBrand.DesignTimeLayout = chkBrand_DesignTimeLayout
        Me.chkBrand.Location = New System.Drawing.Point(6, 19)
        Me.chkBrand.Name = "chkBrand"
        Me.chkBrand.SaveSettings = False
        Me.chkBrand.Size = New System.Drawing.Size(289, 20)
        Me.chkBrand.TabIndex = 9
        Me.chkBrand.ValuesDataMember = Nothing
        Me.chkBrand.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'pnlEntry
        '
        Me.pnlEntry.Controls.Add(Me.grpBrand)
        Me.pnlEntry.Controls.Add(Me.Label1)
        Me.pnlEntry.Controls.Add(Me.dtPicFrom)
        Me.pnlEntry.Controls.Add(Me.grpgrpAvgPrice)
        Me.pnlEntry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEntry.Location = New System.Drawing.Point(0, 45)
        Me.pnlEntry.Name = "pnlEntry"
        Me.pnlEntry.Size = New System.Drawing.Size(909, 74)
        Me.pnlEntry.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Start Date"
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
        Me.dtPicFrom.DropDownCalendar.FirstMonth = New Date(2015, 8, 1, 0, 0, 0, 0)
        Me.dtPicFrom.DropDownCalendar.Name = ""
        Me.dtPicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicFrom.Location = New System.Drawing.Point(69, 23)
        Me.dtPicFrom.Name = "dtPicFrom"
        Me.dtPicFrom.ShowTodayButton = False
        Me.dtPicFrom.Size = New System.Drawing.Size(145, 20)
        Me.dtPicFrom.TabIndex = 3
        Me.dtPicFrom.Value = New Date(2014, 9, 18, 0, 0, 0, 0)
        Me.dtPicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'grpgrpAvgPrice
        '
        Me.grpgrpAvgPrice.Controls.Add(Me.Label3)
        Me.grpgrpAvgPrice.Controls.Add(Me.Label2)
        Me.grpgrpAvgPrice.Controls.Add(Me.txtAVGpricePL)
        Me.grpgrpAvgPrice.Controls.Add(Me.txtAvgPrice)
        Me.grpgrpAvgPrice.Location = New System.Drawing.Point(527, 6)
        Me.grpgrpAvgPrice.Name = "grpgrpAvgPrice"
        Me.grpgrpAvgPrice.Size = New System.Drawing.Size(351, 48)
        Me.grpgrpAvgPrice.TabIndex = 2
        Me.grpgrpAvgPrice.Text = "Average Price"
        Me.grpgrpAvgPrice.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(179, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "PL"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "FM"
        '
        'txtAVGpricePL
        '
        Me.txtAVGpricePL.Location = New System.Drawing.Point(202, 19)
        Me.txtAVGpricePL.Name = "txtAVGpricePL"
        Me.txtAVGpricePL.Size = New System.Drawing.Size(138, 20)
        Me.txtAVGpricePL.TabIndex = 1
        Me.txtAVGpricePL.Text = "0.00"
        Me.txtAVGpricePL.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'txtAvgPrice
        '
        Me.txtAvgPrice.Location = New System.Drawing.Point(39, 19)
        Me.txtAvgPrice.Name = "txtAvgPrice"
        Me.txtAvgPrice.Size = New System.Drawing.Size(130, 20)
        Me.txtAvgPrice.TabIndex = 0
        Me.txtAvgPrice.Text = "0.00"
        Me.txtAvgPrice.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Equal
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 0)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(16, 45)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(909, 45)
        Me.FilterEditor1.SortFieldList = False
        Me.FilterEditor1.Visible = False
        '
        'plGridData
        '
        Me.plGridData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.plGridData.Controls.Add(Me.TManager1)
        Me.plGridData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.plGridData.Location = New System.Drawing.Point(0, 148)
        Me.plGridData.Name = "plGridData"
        Me.plGridData.Size = New System.Drawing.Size(913, 331)
        Me.plGridData.TabIndex = 27
        '
        'TManager1
        '
        Me.TManager1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TManager1.Location = New System.Drawing.Point(0, 0)
        Me.TManager1.Name = "TManager1"
        Me.TManager1.Size = New System.Drawing.Size(909, 327)
        Me.TManager1.TabIndex = 31
        '
        'pnlEntryAndFilter
        '
        Me.pnlEntryAndFilter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlEntryAndFilter.Controls.Add(Me.pnlEntry)
        Me.pnlEntryAndFilter.Controls.Add(Me.FilterEditor1)
        Me.pnlEntryAndFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEntryAndFilter.Location = New System.Drawing.Point(0, 25)
        Me.pnlEntryAndFilter.Name = "pnlEntryAndFilter"
        Me.pnlEntryAndFilter.Size = New System.Drawing.Size(913, 123)
        Me.pnlEntryAndFilter.TabIndex = 26
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
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 2
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnShowFieldChooser, Me.btnSettingGrid})
        Me.btnGrid.Text = "Grid"
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
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnExport, Me.btnRefresh, Me.btnAddNew, Me.btnSave})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(913, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 25
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
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
        'btnAddNew
        '
        Me.btnAddNew.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnAddNew.ImageIndex = 1
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Text = "Add New"
        '
        'btnSave
        '
        Me.btnSave.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSave.ImageIndex = 6
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS)
        Me.btnSave.Text = "Save"
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'AVGPrice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(913, 479)
        Me.Controls.Add(Me.plGridData)
        Me.Controls.Add(Me.pnlEntryAndFilter)
        Me.Controls.Add(Me.Bar2)
        Me.Name = "AVGPrice"
        Me.Text = "AVG Price"
        CType(Me.grpBrand, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBrand.ResumeLayout(False)
        Me.grpBrand.PerformLayout()
        Me.pnlEntry.ResumeLayout(False)
        Me.pnlEntry.PerformLayout()
        CType(Me.grpgrpAvgPrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpgrpAvgPrice.ResumeLayout(False)
        Me.grpgrpAvgPrice.PerformLayout()
        Me.plGridData.ResumeLayout(False)
        Me.pnlEntryAndFilter.ResumeLayout(False)
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpBrand As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents pnlEntry As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents grpgrpAvgPrice As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents plGridData As System.Windows.Forms.Panel
    Private WithEvents TManager1 As DTSProjects.AdvancedTManager
    Private WithEvents pnlEntryAndFilter As System.Windows.Forms.Panel
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSave As DevComponents.DotNetBar.ButtonItem
    Private WithEvents txtAvgPrice As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents txtAVGpricePL As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents chkBrand As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument

End Class
