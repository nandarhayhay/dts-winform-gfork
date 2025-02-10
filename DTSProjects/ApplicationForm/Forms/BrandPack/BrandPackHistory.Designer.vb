<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BrandPackHistory
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
        Dim grdBrandPack_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BrandPackHistory))
        Dim grdPriceHistory_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.grdBrandPack = New Janus.Windows.GridEX.GridEX
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Bar4 = New DevComponents.DotNetBar.Bar
        Me.grdPriceHistory = New Janus.Windows.GridEX.GridEX
        Me.Bar3 = New DevComponents.DotNetBar.Bar
        Me.grpViewMode = New System.Windows.Forms.GroupBox
        Me.btnCLose = New Janus.Windows.EditControls.UIButton
        Me.btnSave = New Janus.Windows.EditControls.UIButton
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.btnModifiedAdded = New DevComponents.DotNetBar.ButtonItem
        Me.btnModifiedOriginal = New DevComponents.DotNetBar.ButtonItem
        Me.btnDelete = New DevComponents.DotNetBar.ButtonItem
        Me.btnCurrent = New DevComponents.DotNetBar.ButtonItem
        Me.btnUnchaigned = New DevComponents.DotNetBar.ButtonItem
        Me.btnOriginalRows = New DevComponents.DotNetBar.ButtonItem
        Me.btnAllData = New DevComponents.DotNetBar.ButtonItem
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.ButtonItem1 = New DevComponents.DotNetBar.ButtonItem
        Me.btnColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnRenameColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSetting = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterEqual = New DevComponents.DotNetBar.ButtonItem
        Me.btnAddNew = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnPackName = New DevComponents.DotNetBar.ButtonItem
        Me.btnBrandName = New DevComponents.DotNetBar.ButtonItem
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PrintPreviewDialog2 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXPrintDocument2 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.grpBrandPack = New Janus.Windows.EditControls.UIGroupBox
        Me.grpFilterDropDownBrandPack = New Janus.Windows.EditControls.UIGroupBox
        Me.btnAplyBrandPack = New Janus.Windows.EditControls.UIButton
        Me.txtFilterDropDownBrandPack = New Janus.Windows.GridEX.EditControls.EditBox
        Me.rdbBrandNameBrandPack = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbPackNameBrandpack = New Janus.Windows.EditControls.UIRadioButton
        Me.grpDropDownPH = New Janus.Windows.EditControls.UIGroupBox
        Me.btnApplyDropDownPH = New Janus.Windows.EditControls.UIButton
        Me.txtFilterDropDownPH = New Janus.Windows.GridEX.EditControls.EditBox
        Me.rdbPackNamePH = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbBrandNamePH = New Janus.Windows.EditControls.UIRadioButton
        Me.grpPH = New Janus.Windows.EditControls.UIGroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.PageSetupDialog2 = New System.Windows.Forms.PageSetupDialog
        CType(Me.grdBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPriceHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpViewMode.SuspendLayout()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBrandPack.SuspendLayout()
        CType(Me.grpFilterDropDownBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFilterDropDownBrandPack.SuspendLayout()
        CType(Me.grpDropDownPH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDropDownPH.SuspendLayout()
        CType(Me.grpPH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPH.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdBrandPack
        '
        Me.grdBrandPack.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdBrandPack.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdBrandPack.CardCaptionPrefix = "BRANDPACK_ID >>"
        Me.grdBrandPack.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.grdBrandPack.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        grdBrandPack_DesignTimeLayout.LayoutString = resources.GetString("grdBrandPack_DesignTimeLayout.LayoutString")
        Me.grdBrandPack.DesignTimeLayout = grdBrandPack_DesignTimeLayout
        Me.grdBrandPack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdBrandPack.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdBrandPack.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdBrandPack.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdBrandPack.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdBrandPack.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdBrandPack.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdBrandPack.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.grdBrandPack.ImageList = Me.ImageList1
        Me.grdBrandPack.Location = New System.Drawing.Point(3, 53)
        Me.grdBrandPack.Name = "grdBrandPack"
        Me.grdBrandPack.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdBrandPack.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdBrandPack.RecordNavigator = True
        Me.grdBrandPack.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdBrandPack.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdBrandPack.Size = New System.Drawing.Size(580, 499)
        Me.grdBrandPack.TabIndex = 0
        Me.grdBrandPack.UpdateOnLeave = False
        Me.grdBrandPack.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdBrandPack.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
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
        Me.ImageList1.Images.SetKeyName(7, "TextEdit.png")
        Me.ImageList1.Images.SetKeyName(8, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(9, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(10, "gridColumn.png")
        Me.ImageList1.Images.SetKeyName(11, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(12, "printer.ico")
        Me.ImageList1.Images.SetKeyName(13, "Export.bmp")
        Me.ImageList1.Images.SetKeyName(14, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(15, "DB_Refresh.ico")
        Me.ImageList1.Images.SetKeyName(16, "Pack.ICO")
        Me.ImageList1.Images.SetKeyName(17, "Brand.ico")
        Me.ImageList1.Images.SetKeyName(18, "PageSetup.BMP")
        '
        'Bar4
        '
        Me.Bar4.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.Bar4.Location = New System.Drawing.Point(265, 220)
        Me.Bar4.Name = "Bar4"
        Me.Bar4.Size = New System.Drawing.Size(35, 57)
        Me.Bar4.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar4.TabIndex = 19
        Me.Bar4.TabStop = False
        Me.Bar4.Text = "Bar4"
        Me.Bar4.Visible = False
        '
        'grdPriceHistory
        '
        Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdPriceHistory.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPriceHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdPriceHistory.CardCaptionPrefix = "BRANDPACK_ID >>"
        Me.grdPriceHistory.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdPriceHistory_DesignTimeLayout.LayoutString = resources.GetString("grdPriceHistory_DesignTimeLayout.LayoutString")
        Me.grdPriceHistory.DesignTimeLayout = grdPriceHistory_DesignTimeLayout
        Me.grdPriceHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdPriceHistory.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdPriceHistory.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdPriceHistory.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdPriceHistory.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdPriceHistory.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdPriceHistory.ImageList = Me.ImageList1
        Me.grdPriceHistory.Location = New System.Drawing.Point(3, 53)
        Me.grdPriceHistory.Name = "grdPriceHistory"
        Me.grdPriceHistory.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdPriceHistory.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdPriceHistory.RecordNavigator = True
        Me.grdPriceHistory.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdPriceHistory.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPriceHistory.Size = New System.Drawing.Size(413, 499)
        Me.grdPriceHistory.TabIndex = 0
        Me.grdPriceHistory.UpdateOnLeave = False
        Me.grdPriceHistory.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdPriceHistory.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Bar3
        '
        Me.Bar3.DockSide = DevComponents.DotNetBar.eDockSide.Right
        Me.Bar3.Location = New System.Drawing.Point(145, 138)
        Me.Bar3.Name = "Bar3"
        Me.Bar3.Size = New System.Drawing.Size(33, 25)
        Me.Bar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar3.TabIndex = 18
        Me.Bar3.TabStop = False
        Me.Bar3.Text = "Bar3"
        Me.Bar3.Visible = False
        '
        'grpViewMode
        '
        Me.grpViewMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grpViewMode.Controls.Add(Me.btnCLose)
        Me.grpViewMode.Controls.Add(Me.btnSave)
        Me.grpViewMode.Controls.Add(Me.Bar1)
        Me.grpViewMode.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpViewMode.Location = New System.Drawing.Point(0, 644)
        Me.grpViewMode.Name = "grpViewMode"
        Me.grpViewMode.Size = New System.Drawing.Size(1014, 44)
        Me.grpViewMode.TabIndex = 7
        Me.grpViewMode.TabStop = False
        Me.grpViewMode.Text = " Data View Mode"
        '
        'btnCLose
        '
        Me.btnCLose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCLose.ImageIndex = 0
        Me.btnCLose.ImageList = Me.ImageList1
        Me.btnCLose.Location = New System.Drawing.Point(896, 11)
        Me.btnCLose.Name = "btnCLose"
        Me.btnCLose.Size = New System.Drawing.Size(112, 27)
        Me.btnCLose.TabIndex = 3
        Me.btnCLose.Text = "Cancel And Close"
        Me.btnCLose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnSave
        '
        Me.btnSave.ImageIndex = 11
        Me.btnSave.ImageList = Me.ImageList1
        Me.btnSave.Location = New System.Drawing.Point(790, 11)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(101, 27)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save Changes"
        Me.btnSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Bar1
        '
        Me.Bar1.AccessibleDescription = "Bar1 (Bar1)"
        Me.Bar1.AccessibleName = "Bar1"
        Me.Bar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar1.BarType = DevComponents.DotNetBar.eBarType.MenuBar
        Me.Bar1.DockSide = DevComponents.DotNetBar.eDockSide.Bottom
        Me.Bar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnModifiedAdded, Me.btnModifiedOriginal, Me.btnDelete, Me.btnCurrent, Me.btnUnchaigned, Me.btnOriginalRows, Me.btnAllData})
        Me.Bar1.Location = New System.Drawing.Point(343, 13)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(441, 25)
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar1.TabIndex = 0
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'btnModifiedAdded
        '
        Me.btnModifiedAdded.Name = "btnModifiedAdded"
        Me.btnModifiedAdded.Text = "Added"
        Me.btnModifiedAdded.Tooltip = "this button will show any datarow who's been added / modified ,before being saved" & _
            " to server"
        '
        'btnModifiedOriginal
        '
        Me.btnModifiedOriginal.Name = "btnModifiedOriginal"
        Me.btnModifiedOriginal.Text = "ModifiedOriginal"
        Me.btnModifiedOriginal.Tooltip = "this button wil show any datarow  who's been modified in original rows before bei" & _
            "ng saved to server"
        '
        'btnDelete
        '
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Text = "Deleted"
        Me.btnDelete.Tooltip = "this button will show any deleted rows, before being saved to server"
        '
        'btnCurrent
        '
        Me.btnCurrent.Name = "btnCurrent"
        Me.btnCurrent.Text = "Current"
        Me.btnCurrent.Tooltip = "this button will show all currently datarows  (who's been modified,added,deleted " & _
            "and unchaigned) before being saved to server"
        '
        'btnUnchaigned
        '
        Me.btnUnchaigned.Name = "btnUnchaigned"
        Me.btnUnchaigned.Text = "Unchaigned"
        Me.btnUnchaigned.Tooltip = "this button will show only datarow(s) who's unchaigned"
        '
        'btnOriginalRows
        '
        Me.btnOriginalRows.Name = "btnOriginalRows"
        Me.btnOriginalRows.Text = "All Data(s)"
        Me.btnOriginalRows.Tooltip = "this button will show all original data currently in server,but have no child ref" & _
            "erenced data"
        '
        'btnAllData
        '
        Me.btnAllData.Name = "btnAllData"
        Me.btnAllData.Text = "All Data"
        Me.btnAllData.Tooltip = "this button will show all currrently data in server"
        Me.btnAllData.Visible = False
        '
        'Bar2
        '
        Me.Bar2.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaption
        Me.Bar2.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaptionText
        Me.Bar2.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem1, Me.btnFilter, Me.btnAddNew, Me.btnExport, Me.btnRefresh})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1014, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 17
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'ButtonItem1
        '
        Me.ButtonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.ButtonItem1.ImageIndex = 2
        Me.ButtonItem1.Name = "ButtonItem1"
        Me.ButtonItem1.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnColumn, Me.btnSettingGrid, Me.btnPrint, Me.btnPageSetting})
        Me.ButtonItem1.Text = "Grid"
        '
        'btnColumn
        '
        Me.btnColumn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnColumn.ImageIndex = 10
        Me.btnColumn.Name = "btnColumn"
        Me.btnColumn.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnRenameColumn, Me.btnFieldChooser})
        Me.btnColumn.Text = "Grid Column"
        '
        'btnRenameColumn
        '
        Me.btnRenameColumn.Name = "btnRenameColumn"
        Me.btnRenameColumn.Text = "Rename Column"
        '
        'btnFieldChooser
        '
        Me.btnFieldChooser.Name = "btnFieldChooser"
        Me.btnFieldChooser.Text = "Show Field Chooser"
        '
        'btnSettingGrid
        '
        Me.btnSettingGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSettingGrid.ImageIndex = 4
        Me.btnSettingGrid.Name = "btnSettingGrid"
        Me.btnSettingGrid.Text = "Setting Grid"
        '
        'btnPrint
        '
        Me.btnPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrint.ImageIndex = 13
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Text = "Print data Grid"
        '
        'btnPageSetting
        '
        Me.btnPageSetting.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPageSetting.ImageIndex = 18
        Me.btnPageSetting.Name = "btnPageSetting"
        Me.btnPageSetting.Text = "PageSettings"
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
        '
        'btnFilterEqual
        '
        Me.btnFilterEqual.Name = "btnFilterEqual"
        Me.btnFilterEqual.Text = "Default"
        '
        'btnAddNew
        '
        Me.btnAddNew.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnAddNew.ImageIndex = 1
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Text = "Add New"
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 14
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export Data"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 15
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Text = "Refresh"
        '
        'btnPackName
        '
        Me.btnPackName.ImageIndex = 16
        Me.btnPackName.Name = "btnPackName"
        Me.btnPackName.Text = "PACK_NAME"
        '
        'btnBrandName
        '
        Me.btnBrandName.ImageIndex = 17
        Me.btnBrandName.Name = "btnBrandName"
        Me.btnBrandName.Text = "BRAND_NAME"
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.IncludeFormatStyle = False
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'PrintPreviewDialog2
        '
        Me.PrintPreviewDialog2.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog2.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog2.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog2.Enabled = True
        Me.PrintPreviewDialog2.Icon = CType(resources.GetObject("PrintPreviewDialog2.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog2.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog2.Visible = False
        '
        'GridEXPrintDocument2
        '
        Me.GridEXPrintDocument2.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument2.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument2.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument2.PrintHierarchical = True
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.FilterEditor1.ColorScheme = "FilterEditorColorScheme"
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Contains
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 25)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(16, 45)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(1014, 45)
        Me.FilterEditor1.SortFieldList = False
        Me.FilterEditor1.Visible = False
        '
        'grpBrandPack
        '
        Me.grpBrandPack.Controls.Add(Me.grdBrandPack)
        Me.grpBrandPack.Controls.Add(Me.grpFilterDropDownBrandPack)
        Me.grpBrandPack.Controls.Add(Me.Bar4)
        Me.grpBrandPack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpBrandPack.Location = New System.Drawing.Point(3, 16)
        Me.grpBrandPack.Name = "grpBrandPack"
        Me.grpBrandPack.Size = New System.Drawing.Size(586, 555)
        Me.grpBrandPack.TabIndex = 20
        Me.grpBrandPack.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grpFilterDropDownBrandPack
        '
        Me.grpFilterDropDownBrandPack.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grpFilterDropDownBrandPack.Controls.Add(Me.btnAplyBrandPack)
        Me.grpFilterDropDownBrandPack.Controls.Add(Me.txtFilterDropDownBrandPack)
        Me.grpFilterDropDownBrandPack.Controls.Add(Me.rdbBrandNameBrandPack)
        Me.grpFilterDropDownBrandPack.Controls.Add(Me.rdbPackNameBrandpack)
        Me.grpFilterDropDownBrandPack.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpFilterDropDownBrandPack.Location = New System.Drawing.Point(3, 8)
        Me.grpFilterDropDownBrandPack.Name = "grpFilterDropDownBrandPack"
        Me.grpFilterDropDownBrandPack.Size = New System.Drawing.Size(580, 45)
        Me.grpFilterDropDownBrandPack.TabIndex = 20
        Me.grpFilterDropDownBrandPack.Text = "Drop down Filter"
        Me.grpFilterDropDownBrandPack.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnAplyBrandPack
        '
        Me.btnAplyBrandPack.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAplyBrandPack.Location = New System.Drawing.Point(474, 16)
        Me.btnAplyBrandPack.Name = "btnAplyBrandPack"
        Me.btnAplyBrandPack.Size = New System.Drawing.Size(70, 21)
        Me.btnAplyBrandPack.TabIndex = 3
        Me.btnAplyBrandPack.Text = "Apply"
        Me.btnAplyBrandPack.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtFilterDropDownBrandPack
        '
        Me.txtFilterDropDownBrandPack.Location = New System.Drawing.Point(214, 15)
        Me.txtFilterDropDownBrandPack.Name = "txtFilterDropDownBrandPack"
        Me.txtFilterDropDownBrandPack.Size = New System.Drawing.Size(254, 20)
        Me.txtFilterDropDownBrandPack.TabIndex = 2
        Me.txtFilterDropDownBrandPack.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'rdbBrandNameBrandPack
        '
        Me.rdbBrandNameBrandPack.Location = New System.Drawing.Point(6, 16)
        Me.rdbBrandNameBrandPack.Name = "rdbBrandNameBrandPack"
        Me.rdbBrandNameBrandPack.Size = New System.Drawing.Size(89, 19)
        Me.rdbBrandNameBrandPack.TabIndex = 1
        Me.rdbBrandNameBrandPack.Text = "Brand Name"
        Me.rdbBrandNameBrandPack.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbPackNameBrandpack
        '
        Me.rdbPackNameBrandpack.Location = New System.Drawing.Point(101, 16)
        Me.rdbPackNameBrandpack.Name = "rdbPackNameBrandpack"
        Me.rdbPackNameBrandpack.Size = New System.Drawing.Size(91, 19)
        Me.rdbPackNameBrandpack.TabIndex = 0
        Me.rdbPackNameBrandpack.Text = "Pack Name"
        Me.rdbPackNameBrandpack.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'grpDropDownPH
        '
        Me.grpDropDownPH.Controls.Add(Me.btnApplyDropDownPH)
        Me.grpDropDownPH.Controls.Add(Me.txtFilterDropDownPH)
        Me.grpDropDownPH.Controls.Add(Me.rdbPackNamePH)
        Me.grpDropDownPH.Controls.Add(Me.rdbBrandNamePH)
        Me.grpDropDownPH.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpDropDownPH.Location = New System.Drawing.Point(3, 8)
        Me.grpDropDownPH.Name = "grpDropDownPH"
        Me.grpDropDownPH.Size = New System.Drawing.Size(413, 45)
        Me.grpDropDownPH.TabIndex = 21
        Me.grpDropDownPH.Text = "Drop down Filter"
        Me.grpDropDownPH.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnApplyDropDownPH
        '
        Me.btnApplyDropDownPH.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnApplyDropDownPH.Location = New System.Drawing.Point(343, 17)
        Me.btnApplyDropDownPH.Name = "btnApplyDropDownPH"
        Me.btnApplyDropDownPH.Size = New System.Drawing.Size(63, 20)
        Me.btnApplyDropDownPH.TabIndex = 3
        Me.btnApplyDropDownPH.Text = "Apply"
        Me.btnApplyDropDownPH.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'txtFilterDropDownPH
        '
        Me.txtFilterDropDownPH.Location = New System.Drawing.Point(184, 17)
        Me.txtFilterDropDownPH.Name = "txtFilterDropDownPH"
        Me.txtFilterDropDownPH.Size = New System.Drawing.Size(153, 20)
        Me.txtFilterDropDownPH.TabIndex = 2
        Me.txtFilterDropDownPH.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        '
        'rdbPackNamePH
        '
        Me.rdbPackNamePH.Location = New System.Drawing.Point(98, 19)
        Me.rdbPackNamePH.Name = "rdbPackNamePH"
        Me.rdbPackNamePH.Size = New System.Drawing.Size(80, 19)
        Me.rdbPackNamePH.TabIndex = 1
        Me.rdbPackNamePH.Text = "Pack Name"
        Me.rdbPackNamePH.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbBrandNamePH
        '
        Me.rdbBrandNamePH.Location = New System.Drawing.Point(6, 18)
        Me.rdbBrandNamePH.Name = "rdbBrandNamePH"
        Me.rdbBrandNamePH.Size = New System.Drawing.Size(85, 19)
        Me.rdbBrandNamePH.TabIndex = 0
        Me.rdbBrandNamePH.Text = "Brand Name"
        Me.rdbBrandNamePH.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'grpPH
        '
        Me.grpPH.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grpPH.Controls.Add(Me.grdPriceHistory)
        Me.grpPH.Controls.Add(Me.grpDropDownPH)
        Me.grpPH.Controls.Add(Me.Bar3)
        Me.grpPH.Dock = System.Windows.Forms.DockStyle.Right
        Me.grpPH.Location = New System.Drawing.Point(592, 16)
        Me.grpPH.Name = "grpPH"
        Me.grpPH.Size = New System.Drawing.Size(419, 555)
        Me.grpPH.TabIndex = 22
        Me.grpPH.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.grpBrandPack)
        Me.GroupBox1.Controls.Add(Me.ExpandableSplitter1)
        Me.GroupBox1.Controls.Add(Me.grpPH)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 70)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1014, 574)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.ExpandableSplitter1.ExpandableControl = Me.grpPH
        Me.ExpandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ExpandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ExpandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(151, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.ExpandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.ExpandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2
        Me.ExpandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.ExpandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ExpandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(589, 16)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(3, 555)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 23
        Me.ExpandableSplitter1.TabStop = False
        '
        'BrandPackHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.CancelButton = Me.btnCLose
        Me.ClientSize = New System.Drawing.Size(1014, 688)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.grpViewMode)
        Me.Controls.Add(Me.Bar2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BrandPackHistory"
        Me.Text = "BrandPack + Price"
        CType(Me.grdBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPriceHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpViewMode.ResumeLayout(False)
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBrandPack.ResumeLayout(False)
        CType(Me.grpFilterDropDownBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFilterDropDownBrandPack.ResumeLayout(False)
        Me.grpFilterDropDownBrandPack.PerformLayout()
        CType(Me.grpDropDownPH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDropDownPH.ResumeLayout(False)
        Me.grpDropDownPH.PerformLayout()
        CType(Me.grpPH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPH.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grdBrandPack As Janus.Windows.GridEX.GridEX
    Private WithEvents grdPriceHistory As Janus.Windows.GridEX.GridEX
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRenameColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCLose As Janus.Windows.EditControls.UIButton
    Private WithEvents btnSave As Janus.Windows.EditControls.UIButton
    Private WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Private WithEvents btnDelete As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCurrent As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnModifiedAdded As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnModifiedOriginal As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnUnchaigned As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnOriginalRows As DevComponents.DotNetBar.ButtonItem
    Private WithEvents grpViewMode As System.Windows.Forms.GroupBox
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents Bar3 As DevComponents.DotNetBar.Bar
    Private WithEvents Bar4 As DevComponents.DotNetBar.Bar
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PrintPreviewDialog2 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXPrintDocument2 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPackName As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnBrandName As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAllData As DevComponents.DotNetBar.ButtonItem
    Private WithEvents grpBrandPack As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents grpFilterDropDownBrandPack As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents rdbBrandNameBrandPack As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbPackNameBrandpack As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents grpDropDownPH As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents btnApplyDropDownPH As Janus.Windows.EditControls.UIButton
    Private WithEvents txtFilterDropDownPH As Janus.Windows.GridEX.EditControls.EditBox
    Private WithEvents rdbPackNamePH As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbBrandNamePH As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents btnAplyBrandPack As Janus.Windows.EditControls.UIButton
    Private WithEvents txtFilterDropDownBrandPack As Janus.Windows.GridEX.EditControls.EditBox
    Private WithEvents grpPH As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents ButtonItem1 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSetting As DevComponents.DotNetBar.ButtonItem
    Private WithEvents PageSetupDialog2 As System.Windows.Forms.PageSetupDialog
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter

End Class
