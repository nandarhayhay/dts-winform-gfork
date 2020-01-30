<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CPDAuto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CPDAuto))
        Dim grdProgressive_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdTermDisc_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
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
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.pnlEntry = New System.Windows.Forms.Panel
        Me.grpdesc = New Janus.Windows.EditControls.UIGroupBox
        Me.txtDescriptions = New System.Windows.Forms.TextBox
        Me.btnClose = New Janus.Windows.EditControls.UIButton
        Me.grdProgressive = New Janus.Windows.GridEX.GridEX
        Me.grdTermDisc = New Janus.Windows.GridEX.GridEX
        Me.grdBrand = New Janus.Windows.GridEX.GridEX
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label2 = New System.Windows.Forms.Label
        Me.plGridData = New System.Windows.Forms.Panel
        Me.TManager1 = New DTSProjects.AdvancedTManager
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEntryAndFilter.SuspendLayout()
        Me.pnlEntry.SuspendLayout()
        CType(Me.grpdesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpdesc.SuspendLayout()
        CType(Me.grdProgressive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTermDisc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdBrand, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ImageList1.Images.SetKeyName(20, "Close.ico")
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
        Me.Bar2.Size = New System.Drawing.Size(1148, 25)
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
        Me.btnSave.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS)
        Me.btnSave.Text = "Save"
        '
        'pnlEntryAndFilter
        '
        Me.pnlEntryAndFilter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlEntryAndFilter.Controls.Add(Me.FilterEditor1)
        Me.pnlEntryAndFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEntryAndFilter.Location = New System.Drawing.Point(0, 25)
        Me.pnlEntryAndFilter.Name = "pnlEntryAndFilter"
        Me.pnlEntryAndFilter.Size = New System.Drawing.Size(1148, 48)
        Me.pnlEntryAndFilter.TabIndex = 24
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
        Me.FilterEditor1.MinSize = New System.Drawing.Size(16, 43)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(1144, 43)
        Me.FilterEditor1.SortFieldList = False
        Me.FilterEditor1.Visible = False
        '
        'pnlEntry
        '
        Me.pnlEntry.Controls.Add(Me.grpdesc)
        Me.pnlEntry.Controls.Add(Me.btnClose)
        Me.pnlEntry.Controls.Add(Me.grdProgressive)
        Me.pnlEntry.Controls.Add(Me.grdTermDisc)
        Me.pnlEntry.Controls.Add(Me.grdBrand)
        Me.pnlEntry.Controls.Add(Me.dtPicUntil)
        Me.pnlEntry.Controls.Add(Me.Label1)
        Me.pnlEntry.Controls.Add(Me.dtPicFrom)
        Me.pnlEntry.Controls.Add(Me.Label2)
        Me.pnlEntry.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEntry.Location = New System.Drawing.Point(0, 0)
        Me.pnlEntry.Name = "pnlEntry"
        Me.pnlEntry.Size = New System.Drawing.Size(1144, 256)
        Me.pnlEntry.TabIndex = 5
        '
        'grpdesc
        '
        Me.grpdesc.Controls.Add(Me.txtDescriptions)
        Me.grpdesc.Location = New System.Drawing.Point(10, 59)
        Me.grpdesc.Name = "grpdesc"
        Me.grpdesc.Size = New System.Drawing.Size(200, 75)
        Me.grpdesc.TabIndex = 9
        Me.grpdesc.Text = "ProgramName/Description"
        Me.grpdesc.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtDescriptions
        '
        Me.txtDescriptions.Location = New System.Drawing.Point(6, 19)
        Me.txtDescriptions.Multiline = True
        Me.txtDescriptions.Name = "txtDescriptions"
        Me.txtDescriptions.Size = New System.Drawing.Size(188, 50)
        Me.txtDescriptions.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.ImageIndex = 20
        Me.btnClose.ImageList = Me.ImageList1
        Me.btnClose.Location = New System.Drawing.Point(1067, 226)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 27)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "&Close"
        Me.btnClose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'grdProgressive
        '
        Me.grdProgressive.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdProgressive.AllowColumnDrag = False
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
        Me.grdProgressive.Location = New System.Drawing.Point(616, 5)
        Me.grdProgressive.Name = "grdProgressive"
        Me.grdProgressive.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdProgressive.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdProgressive.RecordNavigator = True
        Me.grdProgressive.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdProgressive.Size = New System.Drawing.Size(265, 175)
        Me.grdProgressive.TabIndex = 7
        Me.grdProgressive.TableHeaderFormatStyle.BackColor = System.Drawing.Color.Maroon
        Me.grdProgressive.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.grdProgressive.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdProgressive.UpdateOnLeave = False
        Me.grdProgressive.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdProgressive.WatermarkImage.Image = CType(resources.GetObject("grdProgressive.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdProgressive.WatermarkImage.MaskColor = System.Drawing.Color.Transparent
        Me.grdProgressive.WatermarkImage.WashColor = System.Drawing.Color.Transparent
        '
        'grdTermDisc
        '
        Me.grdTermDisc.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdTermDisc.AllowColumnDrag = False
        Me.grdTermDisc.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdTermDisc.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.grdTermDisc.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        grdTermDisc_DesignTimeLayout.LayoutString = resources.GetString("grdTermDisc_DesignTimeLayout.LayoutString")
        Me.grdTermDisc.DesignTimeLayout = grdTermDisc_DesignTimeLayout
        Me.grdTermDisc.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdTermDisc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdTermDisc.GroupByBoxVisible = False
        Me.grdTermDisc.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdTermDisc.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdTermDisc.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdTermDisc.Location = New System.Drawing.Point(889, 5)
        Me.grdTermDisc.Name = "grdTermDisc"
        Me.grdTermDisc.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdTermDisc.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdTermDisc.RecordNavigator = True
        Me.grdTermDisc.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdTermDisc.Size = New System.Drawing.Size(262, 175)
        Me.grdTermDisc.TabIndex = 6
        Me.grdTermDisc.TableHeaderFormatStyle.BackColor = System.Drawing.Color.Maroon
        Me.grdTermDisc.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.grdTermDisc.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdTermDisc.UpdateOnLeave = False
        Me.grdTermDisc.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdTermDisc.WatermarkImage.Image = CType(resources.GetObject("grdTermDisc.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdTermDisc.WatermarkImage.MaskColor = System.Drawing.Color.Transparent
        Me.grdTermDisc.WatermarkImage.WashColor = System.Drawing.Color.Transparent
        '
        'grdBrand
        '
        Me.grdBrand.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdBrand.AllowColumnDrag = False
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
        Me.grdBrand.Location = New System.Drawing.Point(218, 5)
        Me.grdBrand.Name = "grdBrand"
        Me.grdBrand.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdBrand.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdBrand.RecordNavigator = True
        Me.grdBrand.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdBrand.Size = New System.Drawing.Size(389, 248)
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
        'plGridData
        '
        Me.plGridData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.plGridData.Controls.Add(Me.TManager1)
        Me.plGridData.Controls.Add(Me.pnlEntry)
        Me.plGridData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.plGridData.Location = New System.Drawing.Point(0, 73)
        Me.plGridData.Name = "plGridData"
        Me.plGridData.Size = New System.Drawing.Size(1148, 425)
        Me.plGridData.TabIndex = 25
        '
        'TManager1
        '
        Me.TManager1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TManager1.Location = New System.Drawing.Point(0, 256)
        Me.TManager1.Name = "TManager1"
        Me.TManager1.Size = New System.Drawing.Size(1144, 165)
        Me.TManager1.TabIndex = 31
        '
        'CPDAuto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1148, 498)
        Me.Controls.Add(Me.plGridData)
        Me.Controls.Add(Me.pnlEntryAndFilter)
        Me.Controls.Add(Me.Bar2)
        Me.Name = "CPDAuto"
        Me.Text = "CPD AUTO(Discount Progressive)"
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEntryAndFilter.ResumeLayout(False)
        Me.pnlEntry.ResumeLayout(False)
        Me.pnlEntry.PerformLayout()
        CType(Me.grpdesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpdesc.ResumeLayout(False)
        Me.grpdesc.PerformLayout()
        CType(Me.grdProgressive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTermDisc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdBrand, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnEditRow As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSave As DevComponents.DotNetBar.ButtonItem
    Private WithEvents pnlEntryAndFilter As System.Windows.Forms.Panel
    Private WithEvents pnlEntry As System.Windows.Forms.Panel
    Private WithEvents grdTermDisc As Janus.Windows.GridEX.GridEX
    Private WithEvents grdBrand As Janus.Windows.GridEX.GridEX
    Private WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents grdProgressive As Janus.Windows.GridEX.GridEX
    Public WithEvents btnClose As Janus.Windows.EditControls.UIButton
    Private WithEvents plGridData As System.Windows.Forms.Panel
    Private WithEvents TManager1 As DTSProjects.AdvancedTManager
    Private WithEvents grpdesc As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents txtDescriptions As System.Windows.Forms.TextBox

End Class
