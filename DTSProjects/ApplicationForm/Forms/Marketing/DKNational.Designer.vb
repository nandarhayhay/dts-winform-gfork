<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DKNational
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DKNational))
        Dim grdProgressive_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdBrand_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterEqual = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnAddNew = New DevComponents.DotNetBar.ButtonItem
        Me.btnEditRow = New DevComponents.DotNetBar.ButtonItem
        Me.btnSave = New DevComponents.DotNetBar.ButtonItem
        Me.pnlEntryAndFilter = New System.Windows.Forms.Panel
        Me.pnlEntry = New System.Windows.Forms.Panel
        Me.grpTypeDiscount = New Janus.Windows.EditControls.UIGroupBox
        Me.rdbRuleDiscToCertainBrand = New System.Windows.Forms.RadioButton
        Me.rdbOneRuleDiscToAllBrand = New System.Windows.Forms.RadioButton
        Me.grdProgressive = New Janus.Windows.GridEX.GridEX
        Me.grdBrand = New Janus.Windows.GridEX.GridEX
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label2 = New System.Windows.Forms.Label
        Me.grpProductToGive = New Janus.Windows.EditControls.UIGroupBox
        Me.rdbCertainProducts = New System.Windows.Forms.RadioButton
        Me.rdbAllProduct = New System.Windows.Forms.RadioButton
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.plGridData = New System.Windows.Forms.Panel
        Me.TManager1 = New DTSProjects.AdvancedTManager
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEntryAndFilter.SuspendLayout()
        Me.pnlEntry.SuspendLayout()
        CType(Me.grpTypeDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTypeDiscount.SuspendLayout()
        CType(Me.grdProgressive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdBrand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpProductToGive, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProductToGive.SuspendLayout()
        Me.plGridData.SuspendLayout()
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
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnExport, Me.btnRefresh, Me.btnAddNew, Me.btnEditRow, Me.btnSave})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1009, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 22
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 2
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnShowFieldChooser, Me.btnSettingGrid})
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
        'btnEditRow
        '
        Me.btnEditRow.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnEditRow.ImageIndex = 10
        Me.btnEditRow.Name = "btnEditRow"
        Me.btnEditRow.Text = "Edit"
        '
        'btnSave
        '
        Me.btnSave.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSave.ImageIndex = 6
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Text = "Save"
        '
        'pnlEntryAndFilter
        '
        Me.pnlEntryAndFilter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlEntryAndFilter.Controls.Add(Me.pnlEntry)
        Me.pnlEntryAndFilter.Controls.Add(Me.FilterEditor1)
        Me.pnlEntryAndFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEntryAndFilter.Location = New System.Drawing.Point(0, 25)
        Me.pnlEntryAndFilter.Name = "pnlEntryAndFilter"
        Me.pnlEntryAndFilter.Size = New System.Drawing.Size(1009, 238)
        Me.pnlEntryAndFilter.TabIndex = 23
        '
        'pnlEntry
        '
        Me.pnlEntry.Controls.Add(Me.grpTypeDiscount)
        Me.pnlEntry.Controls.Add(Me.grdProgressive)
        Me.pnlEntry.Controls.Add(Me.grdBrand)
        Me.pnlEntry.Controls.Add(Me.dtPicUntil)
        Me.pnlEntry.Controls.Add(Me.Label1)
        Me.pnlEntry.Controls.Add(Me.dtPicFrom)
        Me.pnlEntry.Controls.Add(Me.Label2)
        Me.pnlEntry.Controls.Add(Me.grpProductToGive)
        Me.pnlEntry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEntry.Location = New System.Drawing.Point(0, 45)
        Me.pnlEntry.Name = "pnlEntry"
        Me.pnlEntry.Size = New System.Drawing.Size(1005, 189)
        Me.pnlEntry.TabIndex = 5
        '
        'grpTypeDiscount
        '
        Me.grpTypeDiscount.Controls.Add(Me.rdbRuleDiscToCertainBrand)
        Me.grpTypeDiscount.Controls.Add(Me.rdbOneRuleDiscToAllBrand)
        Me.grpTypeDiscount.Location = New System.Drawing.Point(10, 124)
        Me.grpTypeDiscount.Name = "grpTypeDiscount"
        Me.grpTypeDiscount.Size = New System.Drawing.Size(199, 57)
        Me.grpTypeDiscount.TabIndex = 3
        Me.grpTypeDiscount.Text = "Apply discount rule"
        Me.grpTypeDiscount.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'rdbRuleDiscToCertainBrand
        '
        Me.rdbRuleDiscToCertainBrand.AutoSize = True
        Me.rdbRuleDiscToCertainBrand.Location = New System.Drawing.Point(5, 36)
        Me.rdbRuleDiscToCertainBrand.Name = "rdbRuleDiscToCertainBrand"
        Me.rdbRuleDiscToCertainBrand.Size = New System.Drawing.Size(110, 17)
        Me.rdbRuleDiscToCertainBrand.TabIndex = 1
        Me.rdbRuleDiscToCertainBrand.TabStop = True
        Me.rdbRuleDiscToCertainBrand.Text = "to Certain Product"
        Me.rdbRuleDiscToCertainBrand.UseVisualStyleBackColor = True
        '
        'rdbOneRuleDiscToAllBrand
        '
        Me.rdbOneRuleDiscToAllBrand.AutoSize = True
        Me.rdbOneRuleDiscToAllBrand.Location = New System.Drawing.Point(5, 16)
        Me.rdbOneRuleDiscToAllBrand.Name = "rdbOneRuleDiscToAllBrand"
        Me.rdbOneRuleDiscToAllBrand.Size = New System.Drawing.Size(88, 17)
        Me.rdbOneRuleDiscToAllBrand.TabIndex = 0
        Me.rdbOneRuleDiscToAllBrand.TabStop = True
        Me.rdbOneRuleDiscToAllBrand.Text = "to All Product"
        Me.rdbOneRuleDiscToAllBrand.UseVisualStyleBackColor = True
        '
        'grdProgressive
        '
        Me.grdProgressive.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdProgressive.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdProgressive.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.grdProgressive.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        grdProgressive_DesignTimeLayout.LayoutString = resources.GetString("grdProgressive_DesignTimeLayout.LayoutString")
        Me.grdProgressive.DesignTimeLayout = grdProgressive_DesignTimeLayout
        Me.grdProgressive.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdProgressive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdProgressive.GroupByBoxVisible = False
        Me.grdProgressive.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdProgressive.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdProgressive.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdProgressive.Location = New System.Drawing.Point(586, 6)
        Me.grdProgressive.Name = "grdProgressive"
        Me.grdProgressive.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdProgressive.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdProgressive.RecordNavigator = True
        Me.grdProgressive.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdProgressive.Size = New System.Drawing.Size(268, 175)
        Me.grdProgressive.TabIndex = 6
        Me.grdProgressive.TableHeaderFormatStyle.BackColor = System.Drawing.Color.Maroon
        Me.grdProgressive.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.grdProgressive.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdProgressive.UpdateOnLeave = False
        Me.grdProgressive.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdProgressive.WatermarkImage.Image = CType(resources.GetObject("grdProgressive.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdProgressive.WatermarkImage.MaskColor = System.Drawing.Color.Transparent
        Me.grdProgressive.WatermarkImage.WashColor = System.Drawing.Color.Transparent
        '
        'grdBrand
        '
        Me.grdBrand.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdBrand.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdBrand.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.grdBrand.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        grdBrand_DesignTimeLayout.LayoutString = resources.GetString("grdBrand_DesignTimeLayout.LayoutString")
        Me.grdBrand.DesignTimeLayout = grdBrand_DesignTimeLayout
        Me.grdBrand.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdBrand.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdBrand.GroupByBoxVisible = False
        Me.grdBrand.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdBrand.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdBrand.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdBrand.Location = New System.Drawing.Point(215, 6)
        Me.grdBrand.Name = "grdBrand"
        Me.grdBrand.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdBrand.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdBrand.RecordNavigator = True
        Me.grdBrand.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdBrand.Size = New System.Drawing.Size(365, 176)
        Me.grdBrand.TabIndex = 5
        Me.grdBrand.TableHeaderFormatStyle.BackColor = System.Drawing.Color.Maroon
        Me.grdBrand.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.grdBrand.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdBrand.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdBrand.UpdateOnLeave = False
        Me.grdBrand.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdBrand.WatermarkImage.Image = CType(resources.GetObject("grdBrand.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdBrand.WatermarkImage.MaskColor = System.Drawing.Color.Transparent
        Me.grdBrand.WatermarkImage.WashColor = System.Drawing.Color.Transparent
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
        Me.dtPicUntil.Location = New System.Drawing.Point(69, 33)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(140, 20)
        Me.dtPicUntil.TabIndex = 4
        Me.dtPicUntil.Value = New Date(2014, 9, 18, 0, 0, 0, 0)
        Me.dtPicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 11)
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
        Me.dtPicFrom.DropDownCalendar.Name = ""
        Me.dtPicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicFrom.Location = New System.Drawing.Point(69, 7)
        Me.dtPicFrom.Name = "dtPicFrom"
        Me.dtPicFrom.ShowTodayButton = False
        Me.dtPicFrom.Size = New System.Drawing.Size(140, 20)
        Me.dtPicFrom.TabIndex = 3
        Me.dtPicFrom.Value = New Date(2014, 9, 18, 0, 0, 0, 0)
        Me.dtPicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "EndDate"
        '
        'grpProductToGive
        '
        Me.grpProductToGive.Controls.Add(Me.rdbCertainProducts)
        Me.grpProductToGive.Controls.Add(Me.rdbAllProduct)
        Me.grpProductToGive.Location = New System.Drawing.Point(8, 56)
        Me.grpProductToGive.Name = "grpProductToGive"
        Me.grpProductToGive.Size = New System.Drawing.Size(201, 61)
        Me.grpProductToGive.TabIndex = 2
        Me.grpProductToGive.Text = "Product(s)"
        Me.grpProductToGive.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'rdbCertainProducts
        '
        Me.rdbCertainProducts.AutoSize = True
        Me.rdbCertainProducts.Location = New System.Drawing.Point(6, 38)
        Me.rdbCertainProducts.Name = "rdbCertainProducts"
        Me.rdbCertainProducts.Size = New System.Drawing.Size(103, 17)
        Me.rdbCertainProducts.TabIndex = 1
        Me.rdbCertainProducts.Text = "Certain Products"
        Me.rdbCertainProducts.UseVisualStyleBackColor = True
        '
        'rdbAllProduct
        '
        Me.rdbAllProduct.AutoSize = True
        Me.rdbAllProduct.Location = New System.Drawing.Point(6, 17)
        Me.rdbAllProduct.Name = "rdbAllProduct"
        Me.rdbAllProduct.Size = New System.Drawing.Size(76, 17)
        Me.rdbAllProduct.TabIndex = 0
        Me.rdbAllProduct.Text = "All Product"
        Me.rdbAllProduct.UseVisualStyleBackColor = True
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
        Me.FilterEditor1.Size = New System.Drawing.Size(1005, 45)
        Me.FilterEditor1.SortFieldList = False
        Me.FilterEditor1.Visible = False
        '
        'plGridData
        '
        Me.plGridData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.plGridData.Controls.Add(Me.TManager1)
        Me.plGridData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.plGridData.Location = New System.Drawing.Point(0, 263)
        Me.plGridData.Name = "plGridData"
        Me.plGridData.Size = New System.Drawing.Size(1009, 340)
        Me.plGridData.TabIndex = 24
        '
        'TManager1
        '
        Me.TManager1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TManager1.Location = New System.Drawing.Point(0, 0)
        Me.TManager1.Name = "TManager1"
        Me.TManager1.Size = New System.Drawing.Size(1005, 336)
        Me.TManager1.TabIndex = 31
        '
        'DKNational
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1009, 603)
        Me.Controls.Add(Me.plGridData)
        Me.Controls.Add(Me.pnlEntryAndFilter)
        Me.Controls.Add(Me.Bar2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DKNational"
        Me.Text = "D K (National)"
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEntryAndFilter.ResumeLayout(False)
        Me.pnlEntry.ResumeLayout(False)
        Me.pnlEntry.PerformLayout()
        CType(Me.grpTypeDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTypeDiscount.ResumeLayout(False)
        Me.grpTypeDiscount.PerformLayout()
        CType(Me.grdProgressive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdBrand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpProductToGive, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProductToGive.ResumeLayout(False)
        Me.grpProductToGive.PerformLayout()
        Me.plGridData.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnEditRow As DevComponents.DotNetBar.ButtonItem
    Private WithEvents pnlEntryAndFilter As System.Windows.Forms.Panel
    Private WithEvents plGridData As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents grpProductToGive As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents rdbCertainProducts As System.Windows.Forms.RadioButton
    Private WithEvents rdbAllProduct As System.Windows.Forms.RadioButton
    Private WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents pnlEntry As System.Windows.Forms.Panel
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents grdBrand As Janus.Windows.GridEX.GridEX
    Private WithEvents grdProgressive As Janus.Windows.GridEX.GridEX
    Private WithEvents grpTypeDiscount As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents rdbRuleDiscToCertainBrand As System.Windows.Forms.RadioButton
    Private WithEvents rdbOneRuleDiscToAllBrand As System.Windows.Forms.RadioButton
    Private WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSave As DevComponents.DotNetBar.ButtonItem
    Private WithEvents TManager1 As DTSProjects.AdvancedTManager

End Class
