<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DistributorAgreement
    Inherits DTSProjects.BaseForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DistributorAgreement))
        Dim MultiColumnCombo1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim chkComboDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grpData = New System.Windows.Forms.GroupBox
        Me.grpDate = New System.Windows.Forms.GroupBox
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.colFlag = New System.Windows.Forms.ColumnHeader
        Me.colStartPeriode = New System.Windows.Forms.ColumnHeader
        Me.colEndPeriode = New System.Windows.Forms.ColumnHeader
        Me.grpStartAndDate = New System.Windows.Forms.GroupBox
        Me.dtPicStart = New System.Windows.Forms.DateTimePicker
        Me.dtPicEnd = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.grpDistributor = New System.Windows.Forms.GroupBox
        Me.MultiColumnCombo1 = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.btnAddDistributor = New Janus.Windows.EditControls.UIButton
        Me.chkComboDistributor = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.btnSearch = New DTSProjects.ButtonSearch
        Me.tbProgressive = New System.Windows.Forms.TabControl
        Me.tbProgByVolume = New System.Windows.Forms.TabPage
        Me.tbPeriodic = New Janus.Windows.UI.Tab.UITab
        Me.tbPeriode = New Janus.Windows.UI.Tab.UITabPage
        Me.dgvPeriodic = New System.Windows.Forms.DataGridView
        Me.UNIQUE_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AGREEMENT_NO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.UP_TO_PCT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PRGSV_DISC_PCT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.QSY_DISC_FLAG = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tbYearly = New Janus.Windows.UI.Tab.UITabPage
        Me.dgvYearly = New System.Windows.Forms.DataGridView
        Me.UNIQUE_ID1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AGREEMEN_NO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.UP_TO_PCT_Y = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PRGSV_DISC_PCT_Y = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.QSY_DISC_FLAG_Y = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tbProgByValue = New System.Windows.Forms.TabPage
        Me.tbPeriodicVal = New Janus.Windows.UI.Tab.UITab
        Me.TbPeriodeVal = New Janus.Windows.UI.Tab.UITabPage
        Me.dgvPeriodicVal = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tbYearlyVal = New Janus.Windows.UI.Tab.UITabPage
        Me.dgvYearlyVal = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grpPeriod = New System.Windows.Forms.GroupBox
        Me.rdbFMP = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbSemeterly = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbQuarterly = New Janus.Windows.EditControls.UIRadioButton
        Me.btnChekExisting = New Janus.Windows.EditControls.UIButton
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx
        Me.btnAdd = New Janus.Windows.EditControls.UIButton
        Me.btnSave = New Janus.Windows.EditControls.UIButton
        Me.btnCLose = New Janus.Windows.EditControls.UIButton
        Me.chkAgreementGroup = New System.Windows.Forms.CheckBox
        Me.grpAgDescription = New System.Windows.Forms.GroupBox
        Me.txtAgrementDescription = New System.Windows.Forms.TextBox
        Me.txtAgreementNumber = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.grpGrid = New System.Windows.Forms.GroupBox
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnRemoveFilter = New DevComponents.DotNetBar.ButtonItem
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnRenameColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnView = New DevComponents.DotNetBar.ButtonItem
        Me.btnCardView = New DevComponents.DotNetBar.ButtonItem
        Me.btnSingleCard = New DevComponents.DotNetBar.ButtonItem
        Me.btnTableView = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSettings = New DevComponents.DotNetBar.ButtonItem
        Me.ButtonItem1 = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnDefaultFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.ExpandablePanel1 = New DevComponents.DotNetBar.ExpandablePanel
        Me.txtSearchAgreement = New System.Windows.Forms.TextBox
        Me.btnXCategoryFindAgreement = New DevComponents.DotNetBar.ButtonX
        Me.btnbyAgreementNO = New DevComponents.DotNetBar.ButtonItem
        Me.btnbyRangedDate = New DevComponents.DotNetBar.ButtonItem
        Me.btnAplyRange = New Janus.Windows.EditControls.UIButton
        Me.dtPicFilterEnd = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicFilterStart = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grpData.SuspendLayout()
        Me.grpDate.SuspendLayout()
        Me.grpStartAndDate.SuspendLayout()
        Me.grpDistributor.SuspendLayout()
        CType(Me.MultiColumnCombo1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbProgressive.SuspendLayout()
        Me.tbProgByVolume.SuspendLayout()
        CType(Me.tbPeriodic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbPeriodic.SuspendLayout()
        Me.tbPeriode.SuspendLayout()
        CType(Me.dgvPeriodic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbYearly.SuspendLayout()
        CType(Me.dgvYearly, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbProgByValue.SuspendLayout()
        CType(Me.tbPeriodicVal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbPeriodicVal.SuspendLayout()
        Me.TbPeriodeVal.SuspendLayout()
        CType(Me.dgvPeriodicVal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbYearlyVal.SuspendLayout()
        CType(Me.dgvYearlyVal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPeriod.SuspendLayout()
        Me.PanelEx2.SuspendLayout()
        Me.grpAgDescription.SuspendLayout()
        Me.grpGrid.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExpandablePanel1.SuspendLayout()
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
        Me.ImageList1.Images.SetKeyName(8, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(9, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(10, "TextEdit.png")
        Me.ImageList1.Images.SetKeyName(11, "Export.bmp")
        Me.ImageList1.Images.SetKeyName(12, "printer.ico")
        Me.ImageList1.Images.SetKeyName(13, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(14, "View.jpg")
        Me.ImageList1.Images.SetKeyName(15, "duit.ico")
        Me.ImageList1.Images.SetKeyName(16, "DB_Refresh.ico")
        Me.ImageList1.Images.SetKeyName(17, "PageSetup.BMP")
        Me.ImageList1.Images.SetKeyName(18, "Search.png")
        '
        'grpData
        '
        Me.grpData.Controls.Add(Me.grpDate)
        Me.grpData.Controls.Add(Me.grpDistributor)
        Me.grpData.Controls.Add(Me.tbProgressive)
        Me.grpData.Controls.Add(Me.grpPeriod)
        Me.grpData.Controls.Add(Me.btnChekExisting)
        Me.grpData.Controls.Add(Me.PanelEx2)
        Me.grpData.Controls.Add(Me.chkAgreementGroup)
        Me.grpData.Controls.Add(Me.grpAgDescription)
        Me.grpData.Controls.Add(Me.txtAgreementNumber)
        Me.grpData.Controls.Add(Me.Label2)
        Me.grpData.Dock = System.Windows.Forms.DockStyle.Right
        Me.grpData.Location = New System.Drawing.Point(750, 0)
        Me.grpData.Name = "grpData"
        Me.grpData.Size = New System.Drawing.Size(408, 644)
        Me.grpData.TabIndex = 4
        Me.grpData.TabStop = False
        '
        'grpDate
        '
        Me.grpDate.Controls.Add(Me.ListView1)
        Me.grpDate.Controls.Add(Me.grpStartAndDate)
        Me.grpDate.Location = New System.Drawing.Point(3, 230)
        Me.grpDate.Name = "grpDate"
        Me.grpDate.Size = New System.Drawing.Size(402, 200)
        Me.grpDate.TabIndex = 7
        Me.grpDate.TabStop = False
        Me.grpDate.Text = "Date And Flag Periode"
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colFlag, Me.colStartPeriode, Me.colEndPeriode})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(3, 58)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(396, 139)
        Me.ListView1.TabIndex = 5
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'colFlag
        '
        Me.colFlag.Text = "Flag"
        Me.colFlag.Width = 67
        '
        'colStartPeriode
        '
        Me.colStartPeriode.Text = "Start Periode"
        Me.colStartPeriode.Width = 136
        '
        'colEndPeriode
        '
        Me.colEndPeriode.Text = "End Periode"
        Me.colEndPeriode.Width = 164
        '
        'grpStartAndDate
        '
        Me.grpStartAndDate.Controls.Add(Me.dtPicStart)
        Me.grpStartAndDate.Controls.Add(Me.dtPicEnd)
        Me.grpStartAndDate.Controls.Add(Me.Label3)
        Me.grpStartAndDate.Controls.Add(Me.Label4)
        Me.grpStartAndDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpStartAndDate.Location = New System.Drawing.Point(3, 16)
        Me.grpStartAndDate.Name = "grpStartAndDate"
        Me.grpStartAndDate.Size = New System.Drawing.Size(396, 42)
        Me.grpStartAndDate.TabIndex = 4
        Me.grpStartAndDate.TabStop = False
        Me.grpStartAndDate.Text = "Start End Periode"
        '
        'dtPicStart
        '
        Me.dtPicStart.CustomFormat = "dd MMMM yyyy"
        Me.dtPicStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtPicStart.Location = New System.Drawing.Point(43, 14)
        Me.dtPicStart.Name = "dtPicStart"
        Me.dtPicStart.Size = New System.Drawing.Size(142, 20)
        Me.dtPicStart.TabIndex = 2
        '
        'dtPicEnd
        '
        Me.dtPicEnd.CustomFormat = "dd MMMM yyyy"
        Me.dtPicEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtPicEnd.Location = New System.Drawing.Point(220, 14)
        Me.dtPicEnd.Name = "dtPicEnd"
        Me.dtPicEnd.Size = New System.Drawing.Size(163, 20)
        Me.dtPicEnd.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 15)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Start"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(188, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 15)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "End"
        '
        'grpDistributor
        '
        Me.grpDistributor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpDistributor.Controls.Add(Me.MultiColumnCombo1)
        Me.grpDistributor.Controls.Add(Me.btnAddDistributor)
        Me.grpDistributor.Controls.Add(Me.chkComboDistributor)
        Me.grpDistributor.Controls.Add(Me.btnSearch)
        Me.grpDistributor.Location = New System.Drawing.Point(6, 60)
        Me.grpDistributor.Name = "grpDistributor"
        Me.grpDistributor.Size = New System.Drawing.Size(391, 39)
        Me.grpDistributor.TabIndex = 2
        Me.grpDistributor.TabStop = False
        Me.grpDistributor.Text = "Distributor Name"
        '
        'MultiColumnCombo1
        '
        Me.MultiColumnCombo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MultiColumnCombo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        MultiColumnCombo1_DesignTimeLayout.LayoutString = resources.GetString("MultiColumnCombo1_DesignTimeLayout.LayoutString")
        Me.MultiColumnCombo1.DesignTimeLayout = MultiColumnCombo1_DesignTimeLayout
        Me.MultiColumnCombo1.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.MultiColumnCombo1.DisplayMember = "DISTRIBUTOR_NAME"
        Me.MultiColumnCombo1.Location = New System.Drawing.Point(56, 17)
        Me.MultiColumnCombo1.Name = "MultiColumnCombo1"
        Me.MultiColumnCombo1.SelectedIndex = -1
        Me.MultiColumnCombo1.SelectedItem = Nothing
        Me.MultiColumnCombo1.Size = New System.Drawing.Size(307, 20)
        Me.MultiColumnCombo1.TabIndex = 0
        Me.MultiColumnCombo1.ValueMember = "DISTRIBUTOR_ID"
        Me.MultiColumnCombo1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnAddDistributor
        '
        Me.btnAddDistributor.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAddDistributor.Enabled = False
        Me.btnAddDistributor.Location = New System.Drawing.Point(6, 15)
        Me.btnAddDistributor.Name = "btnAddDistributor"
        Me.btnAddDistributor.Size = New System.Drawing.Size(44, 23)
        Me.btnAddDistributor.TabIndex = 2
        Me.btnAddDistributor.Text = "&Add"
        Me.btnAddDistributor.ToolTipText = "Add Group distributor"
        Me.btnAddDistributor.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'chkComboDistributor
        '
        Me.chkComboDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        chkComboDistributor_DesignTimeLayout.LayoutString = resources.GetString("chkComboDistributor_DesignTimeLayout.LayoutString")
        Me.chkComboDistributor.DesignTimeLayout = chkComboDistributor_DesignTimeLayout
        Me.chkComboDistributor.DropDownDisplayMember = "DISTRIBUTOR_NAME"
        Me.chkComboDistributor.DropDownValueMember = "DISTRIBUTOR_ID"
        Me.chkComboDistributor.Location = New System.Drawing.Point(56, 17)
        Me.chkComboDistributor.Name = "chkComboDistributor"
        Me.chkComboDistributor.SaveSettings = False
        Me.chkComboDistributor.Size = New System.Drawing.Size(307, 20)
        Me.chkComboDistributor.TabIndex = 1
        Me.chkComboDistributor.ValuesDataMember = Nothing
        Me.chkComboDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(369, 17)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(16, 20)
        Me.btnSearch.TabIndex = 1
        '
        'tbProgressive
        '
        Me.tbProgressive.Controls.Add(Me.tbProgByVolume)
        Me.tbProgressive.Controls.Add(Me.tbProgByValue)
        Me.tbProgressive.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tbProgressive.Location = New System.Drawing.Point(3, 430)
        Me.tbProgressive.Name = "tbProgressive"
        Me.tbProgressive.SelectedIndex = 0
        Me.tbProgressive.Size = New System.Drawing.Size(402, 176)
        Me.tbProgressive.TabIndex = 21
        '
        'tbProgByVolume
        '
        Me.tbProgByVolume.Controls.Add(Me.tbPeriodic)
        Me.tbProgByVolume.Location = New System.Drawing.Point(4, 22)
        Me.tbProgByVolume.Name = "tbProgByVolume"
        Me.tbProgByVolume.Padding = New System.Windows.Forms.Padding(3)
        Me.tbProgByVolume.Size = New System.Drawing.Size(394, 150)
        Me.tbProgByVolume.TabIndex = 0
        Me.tbProgByVolume.Text = "Progressive By Volume"
        Me.tbProgByVolume.UseVisualStyleBackColor = True
        '
        'tbPeriodic
        '
        Me.tbPeriodic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbPeriodic.FlatBorderColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.tbPeriodic.Location = New System.Drawing.Point(3, 3)
        Me.tbPeriodic.Name = "tbPeriodic"
        Me.tbPeriodic.Size = New System.Drawing.Size(388, 144)
        Me.tbPeriodic.TabIndex = 13
        Me.tbPeriodic.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.tbPeriode, Me.tbYearly})
        Me.tbPeriodic.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.Office2003
        '
        'tbPeriode
        '
        Me.tbPeriode.Controls.Add(Me.dgvPeriodic)
        Me.tbPeriode.Location = New System.Drawing.Point(1, 21)
        Me.tbPeriode.Name = "tbPeriode"
        Me.tbPeriode.Size = New System.Drawing.Size(386, 122)
        Me.tbPeriode.TabStop = True
        Me.tbPeriode.Text = "Periodic Discount"
        '
        'dgvPeriodic
        '
        Me.dgvPeriodic.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dgvPeriodic.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvPeriodic.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.dgvPeriodic.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPeriodic.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPeriodic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPeriodic.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.UNIQUE_ID, Me.AGREEMENT_NO, Me.UP_TO_PCT, Me.PRGSV_DISC_PCT, Me.QSY_DISC_FLAG})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPeriodic.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvPeriodic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPeriodic.EnableHeadersVisualStyles = False
        Me.dgvPeriodic.GridColor = System.Drawing.SystemColors.InactiveCaption
        Me.dgvPeriodic.Location = New System.Drawing.Point(0, 0)
        Me.dgvPeriodic.Name = "dgvPeriodic"
        Me.dgvPeriodic.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPeriodic.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvPeriodic.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPeriodic.Size = New System.Drawing.Size(386, 122)
        Me.dgvPeriodic.StandardTab = True
        Me.dgvPeriodic.TabIndex = 10
        Me.dgvPeriodic.VirtualMode = True
        '
        'UNIQUE_ID
        '
        Me.UNIQUE_ID.DataPropertyName = "UNIQUE_ID"
        Me.UNIQUE_ID.HeaderText = "ID"
        Me.UNIQUE_ID.Name = "UNIQUE_ID"
        Me.UNIQUE_ID.Visible = False
        '
        'AGREEMENT_NO
        '
        Me.AGREEMENT_NO.DataPropertyName = "AGREEMENT_NO"
        Me.AGREEMENT_NO.HeaderText = "AGREEMENT_NO"
        Me.AGREEMENT_NO.Name = "AGREEMENT_NO"
        Me.AGREEMENT_NO.Visible = False
        '
        'UP_TO_PCT
        '
        Me.UP_TO_PCT.DataPropertyName = "UP_TO_PCT"
        DataGridViewCellStyle2.Format = "N4"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.UP_TO_PCT.DefaultCellStyle = DataGridViewCellStyle2
        Me.UP_TO_PCT.HeaderText = "More Than %"
        Me.UP_TO_PCT.MaxInputLength = 11
        Me.UP_TO_PCT.Name = "UP_TO_PCT"
        Me.UP_TO_PCT.Width = 120
        '
        'PRGSV_DISC_PCT
        '
        Me.PRGSV_DISC_PCT.DataPropertyName = "PRGSV_DISC_PCT"
        DataGridViewCellStyle3.Format = "N4"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.PRGSV_DISC_PCT.DefaultCellStyle = DataGridViewCellStyle3
        Me.PRGSV_DISC_PCT.HeaderText = "Discount"
        Me.PRGSV_DISC_PCT.MaxInputLength = 11
        Me.PRGSV_DISC_PCT.Name = "PRGSV_DISC_PCT"
        '
        'QSY_DISC_FLAG
        '
        Me.QSY_DISC_FLAG.DataPropertyName = "QSY_DISC_FLAG"
        Me.QSY_DISC_FLAG.HeaderText = "FLAG"
        Me.QSY_DISC_FLAG.Name = "QSY_DISC_FLAG"
        Me.QSY_DISC_FLAG.Visible = False
        '
        'tbYearly
        '
        Me.tbYearly.Controls.Add(Me.dgvYearly)
        Me.tbYearly.Location = New System.Drawing.Point(1, 21)
        Me.tbYearly.Name = "tbYearly"
        Me.tbYearly.Size = New System.Drawing.Size(386, 198)
        Me.tbYearly.TabStop = True
        Me.tbYearly.Text = "Yearly discount"
        '
        'dgvYearly
        '
        Me.dgvYearly.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dgvYearly.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvYearly.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.dgvYearly.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvYearly.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvYearly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvYearly.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.UNIQUE_ID1, Me.AGREEMEN_NO, Me.UP_TO_PCT_Y, Me.PRGSV_DISC_PCT_Y, Me.QSY_DISC_FLAG_Y})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvYearly.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgvYearly.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvYearly.EnableHeadersVisualStyles = False
        Me.dgvYearly.GridColor = System.Drawing.SystemColors.InactiveCaption
        Me.dgvYearly.Location = New System.Drawing.Point(0, 0)
        Me.dgvYearly.MultiSelect = False
        Me.dgvYearly.Name = "dgvYearly"
        Me.dgvYearly.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvYearly.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvYearly.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvYearly.Size = New System.Drawing.Size(386, 198)
        Me.dgvYearly.StandardTab = True
        Me.dgvYearly.TabIndex = 11
        '
        'UNIQUE_ID1
        '
        Me.UNIQUE_ID1.DataPropertyName = "UNIQUE_ID"
        Me.UNIQUE_ID1.HeaderText = "ID"
        Me.UNIQUE_ID1.Name = "UNIQUE_ID1"
        Me.UNIQUE_ID1.Visible = False
        '
        'AGREEMEN_NO
        '
        Me.AGREEMEN_NO.DataPropertyName = "AGREEMENT_NO"
        Me.AGREEMEN_NO.HeaderText = "AGREEMEN_NO"
        Me.AGREEMEN_NO.Name = "AGREEMEN_NO"
        Me.AGREEMEN_NO.Visible = False
        '
        'UP_TO_PCT_Y
        '
        Me.UP_TO_PCT_Y.DataPropertyName = "UP_TO_PCT"
        DataGridViewCellStyle7.Format = "N4"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.UP_TO_PCT_Y.DefaultCellStyle = DataGridViewCellStyle7
        Me.UP_TO_PCT_Y.HeaderText = "More Than %"
        Me.UP_TO_PCT_Y.MaxInputLength = 11
        Me.UP_TO_PCT_Y.Name = "UP_TO_PCT_Y"
        Me.UP_TO_PCT_Y.Width = 120
        '
        'PRGSV_DISC_PCT_Y
        '
        Me.PRGSV_DISC_PCT_Y.DataPropertyName = "PRGSV_DISC_PCT"
        DataGridViewCellStyle8.Format = "N4"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.PRGSV_DISC_PCT_Y.DefaultCellStyle = DataGridViewCellStyle8
        Me.PRGSV_DISC_PCT_Y.HeaderText = "Discount"
        Me.PRGSV_DISC_PCT_Y.MaxInputLength = 11
        Me.PRGSV_DISC_PCT_Y.Name = "PRGSV_DISC_PCT_Y"
        '
        'QSY_DISC_FLAG_Y
        '
        Me.QSY_DISC_FLAG_Y.DataPropertyName = "QSY_DISC_FLAG"
        Me.QSY_DISC_FLAG_Y.HeaderText = "QSY_DISC_FLAG"
        Me.QSY_DISC_FLAG_Y.Name = "QSY_DISC_FLAG_Y"
        Me.QSY_DISC_FLAG_Y.Visible = False
        '
        'tbProgByValue
        '
        Me.tbProgByValue.Controls.Add(Me.tbPeriodicVal)
        Me.tbProgByValue.Location = New System.Drawing.Point(4, 22)
        Me.tbProgByValue.Name = "tbProgByValue"
        Me.tbProgByValue.Padding = New System.Windows.Forms.Padding(3)
        Me.tbProgByValue.Size = New System.Drawing.Size(394, 150)
        Me.tbProgByValue.TabIndex = 1
        Me.tbProgByValue.Text = "Progressive By Value"
        Me.tbProgByValue.UseVisualStyleBackColor = True
        '
        'tbPeriodicVal
        '
        Me.tbPeriodicVal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbPeriodicVal.FlatBorderColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.tbPeriodicVal.Location = New System.Drawing.Point(3, 3)
        Me.tbPeriodicVal.Name = "tbPeriodicVal"
        Me.tbPeriodicVal.Size = New System.Drawing.Size(388, 144)
        Me.tbPeriodicVal.TabIndex = 14
        Me.tbPeriodicVal.TabPages.AddRange(New Janus.Windows.UI.Tab.UITabPage() {Me.TbPeriodeVal, Me.tbYearlyVal})
        Me.tbPeriodicVal.VisualStyle = Janus.Windows.UI.Tab.TabVisualStyle.Office2003
        '
        'TbPeriodeVal
        '
        Me.TbPeriodeVal.Controls.Add(Me.dgvPeriodicVal)
        Me.TbPeriodeVal.Location = New System.Drawing.Point(1, 21)
        Me.TbPeriodeVal.Name = "TbPeriodeVal"
        Me.TbPeriodeVal.Size = New System.Drawing.Size(386, 122)
        Me.TbPeriodeVal.TabStop = True
        Me.TbPeriodeVal.Text = "Periodic Discount"
        '
        'dgvPeriodicVal
        '
        Me.dgvPeriodicVal.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dgvPeriodicVal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvPeriodicVal.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.dgvPeriodicVal.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPeriodicVal.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvPeriodicVal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPeriodicVal.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5})
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPeriodicVal.DefaultCellStyle = DataGridViewCellStyle14
        Me.dgvPeriodicVal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPeriodicVal.EnableHeadersVisualStyles = False
        Me.dgvPeriodicVal.GridColor = System.Drawing.SystemColors.InactiveCaption
        Me.dgvPeriodicVal.Location = New System.Drawing.Point(0, 0)
        Me.dgvPeriodicVal.Name = "dgvPeriodicVal"
        Me.dgvPeriodicVal.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPeriodicVal.RowHeadersDefaultCellStyle = DataGridViewCellStyle15
        Me.dgvPeriodicVal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPeriodicVal.Size = New System.Drawing.Size(386, 122)
        Me.dgvPeriodicVal.StandardTab = True
        Me.dgvPeriodicVal.TabIndex = 10
        Me.dgvPeriodicVal.VirtualMode = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "IDApp"
        Me.DataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "AGREEMENT_NO"
        Me.DataGridViewTextBoxColumn2.HeaderText = "AGREEMENT_NO"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Visible = False
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "UP_TO_PCT"
        DataGridViewCellStyle12.Format = "N4"
        DataGridViewCellStyle12.NullValue = Nothing
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridViewTextBoxColumn3.HeaderText = "More Than %"
        Me.DataGridViewTextBoxColumn3.MaxInputLength = 11
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "PRGSV_DISC_PCT"
        DataGridViewCellStyle13.Format = "N4"
        DataGridViewCellStyle13.NullValue = Nothing
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridViewTextBoxColumn4.HeaderText = "Discount"
        Me.DataGridViewTextBoxColumn4.MaxInputLength = 11
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "QSY_DISC_FLAG"
        Me.DataGridViewTextBoxColumn5.HeaderText = "FLAG"
        Me.DataGridViewTextBoxColumn5.MaxInputLength = 2
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 90
        '
        'tbYearlyVal
        '
        Me.tbYearlyVal.Controls.Add(Me.dgvYearlyVal)
        Me.tbYearlyVal.Location = New System.Drawing.Point(1, 21)
        Me.tbYearlyVal.Name = "tbYearlyVal"
        Me.tbYearlyVal.Size = New System.Drawing.Size(386, 198)
        Me.tbYearlyVal.TabStop = True
        Me.tbYearlyVal.Text = "Yearly discount"
        '
        'dgvYearlyVal
        '
        Me.dgvYearlyVal.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dgvYearlyVal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvYearlyVal.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.dgvYearlyVal.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvYearlyVal.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.dgvYearlyVal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvYearlyVal.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.DataGridViewTextBoxColumn10})
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvYearlyVal.DefaultCellStyle = DataGridViewCellStyle19
        Me.dgvYearlyVal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvYearlyVal.EnableHeadersVisualStyles = False
        Me.dgvYearlyVal.GridColor = System.Drawing.SystemColors.InactiveCaption
        Me.dgvYearlyVal.Location = New System.Drawing.Point(0, 0)
        Me.dgvYearlyVal.MultiSelect = False
        Me.dgvYearlyVal.Name = "dgvYearlyVal"
        Me.dgvYearlyVal.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvYearlyVal.RowHeadersDefaultCellStyle = DataGridViewCellStyle20
        Me.dgvYearlyVal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvYearlyVal.Size = New System.Drawing.Size(386, 198)
        Me.dgvYearlyVal.StandardTab = True
        Me.dgvYearlyVal.TabIndex = 11
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "IDApp"
        Me.DataGridViewTextBoxColumn6.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Visible = False
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "AGREEMENT_NO"
        Me.DataGridViewTextBoxColumn7.HeaderText = "AGREEMEN_NO"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.Visible = False
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "UP_TO_PCT"
        DataGridViewCellStyle17.Format = "N4"
        DataGridViewCellStyle17.NullValue = Nothing
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle17
        Me.DataGridViewTextBoxColumn8.HeaderText = "More Than %"
        Me.DataGridViewTextBoxColumn8.MaxInputLength = 11
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Width = 120
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "PRGSV_DISC_PCT"
        DataGridViewCellStyle18.Format = "N4"
        DataGridViewCellStyle18.NullValue = Nothing
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle18
        Me.DataGridViewTextBoxColumn9.HeaderText = "Discount"
        Me.DataGridViewTextBoxColumn9.MaxInputLength = 11
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "QSY_DISC_FLAG"
        Me.DataGridViewTextBoxColumn10.HeaderText = "QSY_DISC_FLAG"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Visible = False
        '
        'grpPeriod
        '
        Me.grpPeriod.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpPeriod.Controls.Add(Me.rdbFMP)
        Me.grpPeriod.Controls.Add(Me.rdbSemeterly)
        Me.grpPeriod.Controls.Add(Me.rdbQuarterly)
        Me.grpPeriod.Location = New System.Drawing.Point(7, 104)
        Me.grpPeriod.Name = "grpPeriod"
        Me.grpPeriod.Size = New System.Drawing.Size(390, 40)
        Me.grpPeriod.TabIndex = 3
        Me.grpPeriod.TabStop = False
        Me.grpPeriod.Text = "Period Treatment"
        '
        'rdbFMP
        '
        Me.rdbFMP.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbFMP.Location = New System.Drawing.Point(190, 16)
        Me.rdbFMP.Name = "rdbFMP"
        Me.rdbFMP.Size = New System.Drawing.Size(146, 18)
        Me.rdbFMP.TabIndex = 2
        Me.rdbFMP.Text = "Four Months Periode"
        Me.rdbFMP.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbSemeterly
        '
        Me.rdbSemeterly.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbSemeterly.Location = New System.Drawing.Point(87, 16)
        Me.rdbSemeterly.Name = "rdbSemeterly"
        Me.rdbSemeterly.Size = New System.Drawing.Size(82, 18)
        Me.rdbSemeterly.TabIndex = 1
        Me.rdbSemeterly.Text = "Semesterly"
        Me.rdbSemeterly.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'rdbQuarterly
        '
        Me.rdbQuarterly.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbQuarterly.Location = New System.Drawing.Point(6, 16)
        Me.rdbQuarterly.Name = "rdbQuarterly"
        Me.rdbQuarterly.Size = New System.Drawing.Size(70, 18)
        Me.rdbQuarterly.TabIndex = 0
        Me.rdbQuarterly.Text = "Quarterly"
        Me.rdbQuarterly.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnChekExisting
        '
        Me.btnChekExisting.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnChekExisting.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnChekExisting.ImageIndex = 18
        Me.btnChekExisting.ImageList = Me.ImageList1
        Me.btnChekExisting.Location = New System.Drawing.Point(300, 34)
        Me.btnChekExisting.Name = "btnChekExisting"
        Me.btnChekExisting.Size = New System.Drawing.Size(98, 22)
        Me.btnChekExisting.TabIndex = 16
        Me.btnChekExisting.Text = "Check Existing"
        Me.btnChekExisting.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx2.Controls.Add(Me.btnAdd)
        Me.PanelEx2.Controls.Add(Me.btnSave)
        Me.PanelEx2.Controls.Add(Me.btnCLose)
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx2.Location = New System.Drawing.Point(3, 606)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(402, 35)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 20
        '
        'btnAdd
        '
        Me.btnAdd.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAdd.ImageIndex = 1
        Me.btnAdd.ImageList = Me.ImageList1
        Me.btnAdd.Location = New System.Drawing.Point(8, 6)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(79, 25)
        Me.btnAdd.TabIndex = 14
        Me.btnAdd.Text = "&Add New"
        Me.btnAdd.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnSave
        '
        Me.btnSave.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnSave.ImageIndex = 13
        Me.btnSave.ImageList = Me.ImageList1
        Me.btnSave.Location = New System.Drawing.Point(194, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(97, 26)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "&Save Changes"
        Me.btnSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnCLose
        '
        Me.btnCLose.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnCLose.ImageIndex = 0
        Me.btnCLose.ImageList = Me.ImageList1
        Me.btnCLose.Location = New System.Drawing.Point(305, 5)
        Me.btnCLose.Name = "btnCLose"
        Me.btnCLose.Size = New System.Drawing.Size(92, 26)
        Me.btnCLose.TabIndex = 12
        Me.btnCLose.Text = "Cancel/&Close"
        Me.btnCLose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'chkAgreementGroup
        '
        Me.chkAgreementGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkAgreementGroup.AutoSize = True
        Me.chkAgreementGroup.Location = New System.Drawing.Point(13, 14)
        Me.chkAgreementGroup.Name = "chkAgreementGroup"
        Me.chkAgreementGroup.Size = New System.Drawing.Size(109, 17)
        Me.chkAgreementGroup.TabIndex = 17
        Me.chkAgreementGroup.Text = "Agreement Group"
        Me.chkAgreementGroup.UseVisualStyleBackColor = True
        '
        'grpAgDescription
        '
        Me.grpAgDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpAgDescription.Controls.Add(Me.txtAgrementDescription)
        Me.grpAgDescription.Location = New System.Drawing.Point(8, 162)
        Me.grpAgDescription.Name = "grpAgDescription"
        Me.grpAgDescription.Size = New System.Drawing.Size(389, 58)
        Me.grpAgDescription.TabIndex = 6
        Me.grpAgDescription.TabStop = False
        Me.grpAgDescription.Text = "Agreement Description"
        '
        'txtAgrementDescription
        '
        Me.txtAgrementDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAgrementDescription.Location = New System.Drawing.Point(3, 16)
        Me.txtAgrementDescription.MaxLength = 50
        Me.txtAgrementDescription.Multiline = True
        Me.txtAgrementDescription.Name = "txtAgrementDescription"
        Me.txtAgrementDescription.Size = New System.Drawing.Size(383, 39)
        Me.txtAgrementDescription.TabIndex = 7
        '
        'txtAgreementNumber
        '
        Me.txtAgreementNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAgreementNumber.Location = New System.Drawing.Point(134, 34)
        Me.txtAgreementNumber.MaxLength = 25
        Me.txtAgreementNumber.Name = "txtAgreementNumber"
        Me.txtAgreementNumber.Size = New System.Drawing.Size(160, 20)
        Me.txtAgreementNumber.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Agreement Number"
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.CausesValidation = False
        Me.ExpandableSplitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.ExpandableSplitter1.ExpandableControl = Me.grpData
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
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(744, 0)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(6, 644)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 5
        Me.ExpandableSplitter1.TabStop = False
        '
        'grpGrid
        '
        Me.grpGrid.Controls.Add(Me.GridEX1)
        Me.grpGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpGrid.Location = New System.Drawing.Point(0, 122)
        Me.grpGrid.Name = "grpGrid"
        Me.grpGrid.Size = New System.Drawing.Size(744, 522)
        Me.grpGrid.TabIndex = 7
        Me.grpGrid.TabStop = False
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.CardCaptionPrefix = "Agreement No :"
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(3, 16)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Size = New System.Drawing.Size(738, 503)
        Me.GridEX1.TabIndex = 0
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'btnFilter
        '
        Me.btnFilter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnFilter.ImageIndex = 3
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCustomFilter, Me.btnRemoveFilter})
        Me.btnFilter.Text = "Filter Row"
        '
        'btnCustomFilter
        '
        Me.btnCustomFilter.Name = "btnCustomFilter"
        Me.btnCustomFilter.Text = "Custom Filter"
        '
        'btnRemoveFilter
        '
        Me.btnRemoveFilter.Name = "btnRemoveFilter"
        Me.btnRemoveFilter.Text = "Remove Filter"
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "DotNetBar Bar (Bar2)"
        Me.Bar2.AccessibleName = "DotNetBar Bar"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar
        Me.Bar2.BarType = DevComponents.DotNetBar.eBarType.MenuBar
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.ButtonItem1, Me.btnExport})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.MenuBar = True
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(744, 24)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 19
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 2
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnColumn, Me.btnSettingGrid, Me.btnPrint, Me.btnView, Me.btnRefresh, Me.btnPageSettings})
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
        '
        'btnShowFieldChooser
        '
        Me.btnShowFieldChooser.Name = "btnShowFieldChooser"
        Me.btnShowFieldChooser.Text = "Show Field Chooser"
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
        Me.btnPrint.ImageIndex = 12
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Text = "Print data Grid"
        '
        'btnView
        '
        Me.btnView.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnView.ImageIndex = 14
        Me.btnView.Name = "btnView"
        Me.btnView.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCardView, Me.btnSingleCard, Me.btnTableView})
        Me.btnView.Text = "View"
        '
        'btnCardView
        '
        Me.btnCardView.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnCardView.Name = "btnCardView"
        Me.btnCardView.Text = "Card View"
        '
        'btnSingleCard
        '
        Me.btnSingleCard.Name = "btnSingleCard"
        Me.btnSingleCard.Text = "Sigle Card View"
        '
        'btnTableView
        '
        Me.btnTableView.Name = "btnTableView"
        Me.btnTableView.Text = "Table View"
        '
        'btnRefresh
        '
        Me.btnRefresh.ImageIndex = 16
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5)
        Me.btnRefresh.Text = "RefreshDataGrid"
        '
        'btnPageSettings
        '
        Me.btnPageSettings.ImageIndex = 17
        Me.btnPageSettings.Name = "btnPageSettings"
        Me.btnPageSettings.Text = "Page Settings"
        '
        'ButtonItem1
        '
        Me.ButtonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.ButtonItem1.ImageIndex = 3
        Me.ButtonItem1.Name = "ButtonItem1"
        Me.ButtonItem1.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCustFilter, Me.btnDefaultFilter})
        Me.ButtonItem1.Text = "Filter Row"
        '
        'btnCustFilter
        '
        Me.btnCustFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnCustFilter.Name = "btnCustFilter"
        Me.btnCustFilter.Text = "Custom Filter"
        '
        'btnDefaultFilter
        '
        Me.btnDefaultFilter.Name = "btnDefaultFilter"
        Me.btnDefaultFilter.Text = "Default"
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 11
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export Data"
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Equal
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 24)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(16, 45)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(744, 45)
        Me.FilterEditor1.SortFieldList = False
        Me.FilterEditor1.SourceControl = Me.GridEX1
        Me.FilterEditor1.Visible = False
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
        Me.GridEXPrintDocument1.GridEX = Me.GridEX1
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.GridEX = Me.GridEX1
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
        Me.ExpandablePanel1.Controls.Add(Me.txtSearchAgreement)
        Me.ExpandablePanel1.Controls.Add(Me.btnXCategoryFindAgreement)
        Me.ExpandablePanel1.Controls.Add(Me.btnAplyRange)
        Me.ExpandablePanel1.Controls.Add(Me.dtPicFilterEnd)
        Me.ExpandablePanel1.Controls.Add(Me.dtPicFilterStart)
        Me.ExpandablePanel1.Controls.Add(Me.Label5)
        Me.ExpandablePanel1.Controls.Add(Me.Label1)
        Me.ExpandablePanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ExpandablePanel1.Location = New System.Drawing.Point(0, 69)
        Me.ExpandablePanel1.Name = "ExpandablePanel1"
        Me.ExpandablePanel1.Size = New System.Drawing.Size(744, 53)
        Me.ExpandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.ExpandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel1.Style.GradientAngle = 90
        Me.ExpandablePanel1.TabIndex = 20
        Me.ExpandablePanel1.TitleHeight = 17
        Me.ExpandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal
        Me.ExpandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel1.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel1.TitleText = "Filter Master data"
        '
        'txtSearchAgreement
        '
        Me.txtSearchAgreement.Location = New System.Drawing.Point(147, 22)
        Me.txtSearchAgreement.Multiline = True
        Me.txtSearchAgreement.Name = "txtSearchAgreement"
        Me.txtSearchAgreement.Size = New System.Drawing.Size(350, 22)
        Me.txtSearchAgreement.TabIndex = 22
        '
        'btnXCategoryFindAgreement
        '
        Me.btnXCategoryFindAgreement.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnXCategoryFindAgreement.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.btnXCategoryFindAgreement.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Color
        Me.btnXCategoryFindAgreement.Location = New System.Drawing.Point(12, 22)
        Me.btnXCategoryFindAgreement.Name = "btnXCategoryFindAgreement"
        Me.btnXCategoryFindAgreement.Size = New System.Drawing.Size(116, 23)
        Me.btnXCategoryFindAgreement.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnbyAgreementNO, Me.btnbyRangedDate})
        Me.btnXCategoryFindAgreement.TabIndex = 7
        Me.btnXCategoryFindAgreement.Text = "Find or filter data"
        '
        'btnbyAgreementNO
        '
        Me.btnbyAgreementNO.Checked = True
        Me.btnbyAgreementNO.GlobalItem = False
        Me.btnbyAgreementNO.Name = "btnbyAgreementNO"
        Me.btnbyAgreementNO.Text = "By Agreement NO"
        '
        'btnbyRangedDate
        '
        Me.btnbyRangedDate.GlobalItem = False
        Me.btnbyRangedDate.Name = "btnbyRangedDate"
        Me.btnbyRangedDate.Text = "By Range Date"
        '
        'btnAplyRange
        '
        Me.btnAplyRange.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAplyRange.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnAplyRange.Location = New System.Drawing.Point(503, 22)
        Me.btnAplyRange.Name = "btnAplyRange"
        Me.btnAplyRange.Size = New System.Drawing.Size(63, 20)
        Me.btnAplyRange.TabIndex = 5
        Me.btnAplyRange.Text = "Apply filter"
        Me.btnAplyRange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'dtPicFilterEnd
        '
        Me.dtPicFilterEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFilterEnd.CustomFormat = "dd MMMM yyyy"
        Me.dtPicFilterEnd.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicFilterEnd.DropDownCalendar.Name = ""
        Me.dtPicFilterEnd.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFilterEnd.Location = New System.Drawing.Point(358, 22)
        Me.dtPicFilterEnd.Name = "dtPicFilterEnd"
        Me.dtPicFilterEnd.ShowTodayButton = False
        Me.dtPicFilterEnd.Size = New System.Drawing.Size(139, 20)
        Me.dtPicFilterEnd.TabIndex = 4
        Me.dtPicFilterEnd.Value = New Date(2016, 3, 29, 0, 0, 0, 0)
        Me.dtPicFilterEnd.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicFilterStart
        '
        Me.dtPicFilterStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFilterStart.CustomFormat = "dd MMMM yyyy"
        Me.dtPicFilterStart.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicFilterStart.DropDownCalendar.Name = ""
        Me.dtPicFilterStart.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFilterStart.Location = New System.Drawing.Point(179, 23)
        Me.dtPicFilterStart.Name = "dtPicFilterStart"
        Me.dtPicFilterStart.ShowTodayButton = False
        Me.dtPicFilterStart.Size = New System.Drawing.Size(141, 20)
        Me.dtPicFilterStart.TabIndex = 3
        Me.dtPicFilterStart.Value = New Date(2016, 3, 29, 0, 0, 0, 0)
        Me.dtPicFilterStart.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(326, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(26, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "End"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(144, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Start"
        '
        'DistributorAgreement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1158, 644)
        Me.Controls.Add(Me.grpGrid)
        Me.Controls.Add(Me.ExpandablePanel1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Controls.Add(Me.ExpandableSplitter1)
        Me.Controls.Add(Me.grpData)
        Me.Name = "DistributorAgreement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Distributor Agreement"
        Me.grpData.ResumeLayout(False)
        Me.grpData.PerformLayout()
        Me.grpDate.ResumeLayout(False)
        Me.grpStartAndDate.ResumeLayout(False)
        Me.grpStartAndDate.PerformLayout()
        Me.grpDistributor.ResumeLayout(False)
        Me.grpDistributor.PerformLayout()
        CType(Me.MultiColumnCombo1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbProgressive.ResumeLayout(False)
        Me.tbProgByVolume.ResumeLayout(False)
        CType(Me.tbPeriodic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbPeriodic.ResumeLayout(False)
        Me.tbPeriode.ResumeLayout(False)
        CType(Me.dgvPeriodic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbYearly.ResumeLayout(False)
        CType(Me.dgvYearly, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbProgByValue.ResumeLayout(False)
        CType(Me.tbPeriodicVal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbPeriodicVal.ResumeLayout(False)
        Me.TbPeriodeVal.ResumeLayout(False)
        CType(Me.dgvPeriodicVal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbYearlyVal.ResumeLayout(False)
        CType(Me.dgvYearlyVal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPeriod.ResumeLayout(False)
        Me.PanelEx2.ResumeLayout(False)
        Me.grpAgDescription.ResumeLayout(False)
        Me.grpAgDescription.PerformLayout()
        Me.grpGrid.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExpandablePanel1.ResumeLayout(False)
        Me.ExpandablePanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private ImageList1 As System.Windows.Forms.ImageList
    Private grpData As System.Windows.Forms.GroupBox
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents grpGrid As System.Windows.Forms.GroupBox
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents MultiColumnCombo1 As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents btnSearch As DTSProjects.ButtonSearch
    Private grpDistributor As System.Windows.Forms.GroupBox
    Private grpPeriod As System.Windows.Forms.GroupBox
    Private WithEvents txtAgreementNumber As System.Windows.Forms.TextBox
    Private Label2 As System.Windows.Forms.Label
    Private grpAgDescription As System.Windows.Forms.GroupBox
    Private WithEvents txtAgrementDescription As System.Windows.Forms.TextBox
    Private grpDate As System.Windows.Forms.GroupBox
    Private Label4 As System.Windows.Forms.Label
    Private Label3 As System.Windows.Forms.Label
    Private WithEvents dtPicEnd As System.Windows.Forms.DateTimePicker
    Private WithEvents dtPicStart As System.Windows.Forms.DateTimePicker
    Private WithEvents dgvPeriodic As System.Windows.Forms.DataGridView
    Private WithEvents btnSave As Janus.Windows.EditControls.UIButton
    Private WithEvents btnCLose As Janus.Windows.EditControls.UIButton
    Private WithEvents tbPeriode As Janus.Windows.UI.Tab.UITabPage
    Private WithEvents tbYearly As Janus.Windows.UI.Tab.UITabPage
    Private WithEvents tbPeriodic As Janus.Windows.UI.Tab.UITab
    Private WithEvents dgvYearly As System.Windows.Forms.DataGridView
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRemoveFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRenameColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents ButtonItem1 As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents rdbSemeterly As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbQuarterly As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents btnView As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCardView As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSingleCard As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnTableView As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents btnDefaultFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents chkComboDistributor As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Private WithEvents btnAddDistributor As Janus.Windows.EditControls.UIButton
    Private WithEvents chkAgreementGroup As System.Windows.Forms.CheckBox
    Private WithEvents btnChekExisting As Janus.Windows.EditControls.UIButton
    Private WithEvents btnAdd As Janus.Windows.EditControls.UIButton
    Private WithEvents ExpandablePanel1 As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents dtPicFilterEnd As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents dtPicFilterStart As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents btnAplyRange As Janus.Windows.EditControls.UIButton
    Private WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Private WithEvents ListView1 As System.Windows.Forms.ListView
    Private WithEvents grpStartAndDate As System.Windows.Forms.GroupBox
    Private WithEvents colFlag As System.Windows.Forms.ColumnHeader
    Private WithEvents colStartPeriode As System.Windows.Forms.ColumnHeader
    Private WithEvents colEndPeriode As System.Windows.Forms.ColumnHeader
    Friend WithEvents UNIQUE_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AGREEMENT_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UP_TO_PCT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PRGSV_DISC_PCT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QSY_DISC_FLAG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UNIQUE_ID1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AGREEMEN_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UP_TO_PCT_Y As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PRGSV_DISC_PCT_Y As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QSY_DISC_FLAG_Y As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents tbProgressive As System.Windows.Forms.TabControl
    Private WithEvents tbProgByVolume As System.Windows.Forms.TabPage
    Private WithEvents tbProgByValue As System.Windows.Forms.TabPage
    Private WithEvents tbPeriodicVal As Janus.Windows.UI.Tab.UITab
    Private WithEvents TbPeriodeVal As Janus.Windows.UI.Tab.UITabPage
    Private WithEvents dgvPeriodicVal As System.Windows.Forms.DataGridView
    Private WithEvents tbYearlyVal As Janus.Windows.UI.Tab.UITabPage
    Private WithEvents dgvYearlyVal As System.Windows.Forms.DataGridView
    Private WithEvents btnXCategoryFindAgreement As DevComponents.DotNetBar.ButtonX
    Private WithEvents btnbyAgreementNO As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnbyRangedDate As DevComponents.DotNetBar.ButtonItem
    Private WithEvents txtSearchAgreement As System.Windows.Forms.TextBox
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents rdbFMP As Janus.Windows.EditControls.UIRadioButton

End Class
