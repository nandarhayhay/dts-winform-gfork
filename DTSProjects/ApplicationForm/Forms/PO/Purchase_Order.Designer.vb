<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Purchase_Order
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
        Dim grdPOBrandPack_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Purchase_Order))
        Dim mcbProject_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbPOHeader_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim MultiColumnCombo1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.btnSearchPOHeader = New DTSProjects.ButtonSearch
        Me.grpPOBrandPack = New Janus.Windows.EditControls.UIGroupBox
        Me.grdPOBrandPack = New Janus.Windows.GridEX.GridEX
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grpPurchaseOrder = New Janus.Windows.EditControls.UIGroupBox
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.grpProject = New Janus.Windows.EditControls.UIGroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtProjectRefDate = New System.Windows.Forms.TextBox
        Me.txtProjectName = New System.Windows.Forms.TextBox
        Me.mcbProject = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.btnFilterProject = New DTSProjects.ButtonSearch
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.mcbPOHeader = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.btnNewPOHeader = New DTSProjects.AddNew
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtPicRefDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.btnChekExisting = New Janus.Windows.EditControls.UIButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPOReference = New System.Windows.Forms.TextBox
        Me.grpDistributor = New Janus.Windows.EditControls.UIGroupBox
        Me.txtRegionalArea = New System.Windows.Forms.TextBox
        Me.MultiColumnCombo1 = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.btnFilterDistributor = New DTSProjects.ButtonSearch
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtTerritoryArea = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtDistributorPhone = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtDistributorContact = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.XpLine1 = New SteepValley.Windows.Forms.XPLine
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.btnAddNew = New Janus.Windows.EditControls.UIButton
        Me.SavingChanges1 = New DTSProjects.SavingChanges
        CType(Me.grpPOBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPOBrandPack.SuspendLayout()
        CType(Me.grdPOBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpPurchaseOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPurchaseOrder.SuspendLayout()
        Me.XpGradientPanel1.SuspendLayout()
        CType(Me.grpProject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProject.SuspendLayout()
        CType(Me.mcbProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.mcbPOHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDistributor.SuspendLayout()
        CType(Me.MultiColumnCombo1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSearchPOHeader
        '
        Me.btnSearchPOHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchPOHeader.Location = New System.Drawing.Point(311, 34)
        Me.btnSearchPOHeader.Name = "btnSearchPOHeader"
        Me.btnSearchPOHeader.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchPOHeader.TabIndex = 16
        Me.btnSearchPOHeader.Visible = False
        '
        'grpPOBrandPack
        '
        Me.grpPOBrandPack.BackColor = System.Drawing.Color.Transparent
        Me.grpPOBrandPack.Controls.Add(Me.grdPOBrandPack)
        Me.grpPOBrandPack.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpPOBrandPack.Location = New System.Drawing.Point(3, 259)
        Me.grpPOBrandPack.Name = "grpPOBrandPack"
        Me.grpPOBrandPack.Size = New System.Drawing.Size(774, 239)
        Me.grpPOBrandPack.TabIndex = 1
        Me.grpPOBrandPack.Text = "PO Detail"
        Me.grpPOBrandPack.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grdPOBrandPack
        '
        Me.grdPOBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPOBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPOBrandPack.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.grdPOBrandPack.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        grdPOBrandPack_DesignTimeLayout.LayoutString = resources.GetString("grdPOBrandPack_DesignTimeLayout.LayoutString")
        Me.grdPOBrandPack.DesignTimeLayout = grdPOBrandPack_DesignTimeLayout
        Me.grdPOBrandPack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPOBrandPack.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdPOBrandPack.GroupByBoxVisible = False
        Me.grdPOBrandPack.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdPOBrandPack.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdPOBrandPack.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdPOBrandPack.ImageList = Me.ImageList1
        Me.grdPOBrandPack.Location = New System.Drawing.Point(3, 16)
        Me.grdPOBrandPack.Name = "grdPOBrandPack"
        Me.grdPOBrandPack.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdPOBrandPack.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdPOBrandPack.RecordNavigator = True
        Me.grdPOBrandPack.Size = New System.Drawing.Size(768, 220)
        Me.grdPOBrandPack.TabIndex = 0
        Me.grdPOBrandPack.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPOBrandPack.UpdateOnLeave = False
        Me.grdPOBrandPack.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdPOBrandPack.WatermarkImage.Image = CType(resources.GetObject("grdPOBrandPack.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdPOBrandPack.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
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
        Me.ImageList1.Images.SetKeyName(12, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(13, "printer.ico")
        Me.ImageList1.Images.SetKeyName(14, "Export.bmp")
        Me.ImageList1.Images.SetKeyName(15, "Search.png")
        '
        'grpPurchaseOrder
        '
        Me.grpPurchaseOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.grpPurchaseOrder.Controls.Add(Me.XpGradientPanel1)
        Me.grpPurchaseOrder.Controls.Add(Me.XpLine1)
        Me.grpPurchaseOrder.Controls.Add(Me.grpPOBrandPack)
        Me.grpPurchaseOrder.Controls.Add(Me.PanelEx1)
        Me.grpPurchaseOrder.Location = New System.Drawing.Point(0, 7)
        Me.grpPurchaseOrder.Name = "grpPurchaseOrder"
        Me.grpPurchaseOrder.Size = New System.Drawing.Size(780, 533)
        Me.grpPurchaseOrder.TabIndex = 2
        Me.grpPurchaseOrder.Text = "Purchase Order"
        Me.grpPurchaseOrder.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.BackColor = System.Drawing.Color.Transparent
        Me.XpGradientPanel1.Controls.Add(Me.grpProject)
        Me.XpGradientPanel1.Controls.Add(Me.UiGroupBox1)
        Me.XpGradientPanel1.Controls.Add(Me.grpDistributor)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XpGradientPanel1.EndColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.XpGradientPanel1.Location = New System.Drawing.Point(3, 16)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(774, 233)
        Me.XpGradientPanel1.StartColor = System.Drawing.SystemColors.MenuBar
        Me.XpGradientPanel1.TabIndex = 10
        '
        'grpProject
        '
        Me.grpProject.Controls.Add(Me.Label8)
        Me.grpProject.Controls.Add(Me.Label3)
        Me.grpProject.Controls.Add(Me.txtProjectRefDate)
        Me.grpProject.Controls.Add(Me.txtProjectName)
        Me.grpProject.Controls.Add(Me.mcbProject)
        Me.grpProject.Controls.Add(Me.btnFilterProject)
        Me.grpProject.Location = New System.Drawing.Point(360, 138)
        Me.grpProject.Name = "grpProject"
        Me.grpProject.Size = New System.Drawing.Size(407, 87)
        Me.grpProject.TabIndex = 23
        Me.grpProject.Text = "PO for Project"
        Me.grpProject.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 42)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Project Ref Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(192, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Project Name"
        '
        'txtProjectRefDate
        '
        Me.txtProjectRefDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtProjectRefDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProjectRefDate.Location = New System.Drawing.Point(7, 61)
        Me.txtProjectRefDate.Name = "txtProjectRefDate"
        Me.txtProjectRefDate.ReadOnly = True
        Me.txtProjectRefDate.Size = New System.Drawing.Size(392, 20)
        Me.txtProjectRefDate.TabIndex = 18
        '
        'txtProjectName
        '
        Me.txtProjectName.BackColor = System.Drawing.SystemColors.Window
        Me.txtProjectName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProjectName.Location = New System.Drawing.Point(188, 30)
        Me.txtProjectName.Name = "txtProjectName"
        Me.txtProjectName.ReadOnly = True
        Me.txtProjectName.Size = New System.Drawing.Size(211, 20)
        Me.txtProjectName.TabIndex = 17
        '
        'mcbProject
        '
        Me.mcbProject.BackColor = System.Drawing.SystemColors.InactiveCaption
        mcbProject_DesignTimeLayout.LayoutString = resources.GetString("mcbProject_DesignTimeLayout.LayoutString")
        Me.mcbProject.DesignTimeLayout = mcbProject_DesignTimeLayout
        Me.mcbProject.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbProject.DisplayMember = "PROJECT_NAME"
        Me.mcbProject.FlatBorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.mcbProject.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbProject.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbProject.Location = New System.Drawing.Point(7, 16)
        Me.mcbProject.Name = "mcbProject"
        Me.mcbProject.SelectedIndex = -1
        Me.mcbProject.SelectedItem = Nothing
        Me.mcbProject.Size = New System.Drawing.Size(160, 20)
        Me.mcbProject.TabIndex = 0
        Me.mcbProject.ValueMember = "PROJ_REF_NO"
        Me.mcbProject.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnFilterProject
        '
        Me.btnFilterProject.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterProject.Location = New System.Drawing.Point(168, 17)
        Me.btnFilterProject.Name = "btnFilterProject"
        Me.btnFilterProject.Size = New System.Drawing.Size(18, 22)
        Me.btnFilterProject.TabIndex = 15
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UiGroupBox1.Controls.Add(Me.mcbPOHeader)
        Me.UiGroupBox1.Controls.Add(Me.btnSearchPOHeader)
        Me.UiGroupBox1.Controls.Add(Me.btnNewPOHeader)
        Me.UiGroupBox1.Controls.Add(Me.Label2)
        Me.UiGroupBox1.Controls.Add(Me.dtPicRefDate)
        Me.UiGroupBox1.Controls.Add(Me.btnChekExisting)
        Me.UiGroupBox1.Controls.Add(Me.Label1)
        Me.UiGroupBox1.Controls.Add(Me.txtPOReference)
        Me.UiGroupBox1.Location = New System.Drawing.Point(5, 6)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(348, 219)
        Me.UiGroupBox1.TabIndex = 10
        Me.UiGroupBox1.Text = "PO Header"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'mcbPOHeader
        '
        Me.mcbPOHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbPOHeader_DesignTimeLayout.LayoutString = resources.GetString("mcbPOHeader_DesignTimeLayout.LayoutString")
        Me.mcbPOHeader.DesignTimeLayout = mcbPOHeader_DesignTimeLayout
        Me.mcbPOHeader.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbPOHeader.Location = New System.Drawing.Point(127, 32)
        Me.mcbPOHeader.Name = "mcbPOHeader"
        Me.mcbPOHeader.SelectedIndex = -1
        Me.mcbPOHeader.SelectedItem = Nothing
        Me.mcbPOHeader.Size = New System.Drawing.Size(182, 20)
        Me.mcbPOHeader.TabIndex = 17
        Me.mcbPOHeader.Visible = False
        Me.mcbPOHeader.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnNewPOHeader
        '
        Me.btnNewPOHeader.Location = New System.Drawing.Point(329, 34)
        Me.btnNewPOHeader.Name = "btnNewPOHeader"
        Me.btnNewPOHeader.Size = New System.Drawing.Size(17, 16)
        Me.btnNewPOHeader.TabIndex = 15
        Me.btnNewPOHeader.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "PO Reference Date"
        '
        'dtPicRefDate
        '
        Me.dtPicRefDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicRefDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicRefDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        Me.dtPicRefDate.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        '
        '
        '
        Me.dtPicRefDate.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicRefDate.DropDownCalendar.Name = ""
        Me.dtPicRefDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicRefDate.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicRefDate.IsNullDate = True
        Me.dtPicRefDate.Location = New System.Drawing.Point(127, 71)
        Me.dtPicRefDate.Name = "dtPicRefDate"
        Me.dtPicRefDate.ShowTodayButton = False
        Me.dtPicRefDate.Size = New System.Drawing.Size(215, 20)
        Me.dtPicRefDate.TabIndex = 9
        Me.dtPicRefDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'btnChekExisting
        '
        Me.btnChekExisting.ImageIndex = 15
        Me.btnChekExisting.ImageList = Me.ImageList1
        Me.btnChekExisting.Location = New System.Drawing.Point(223, 97)
        Me.btnChekExisting.Name = "btnChekExisting"
        Me.btnChekExisting.Size = New System.Drawing.Size(119, 23)
        Me.btnChekExisting.TabIndex = 8
        Me.btnChekExisting.Text = "Check Existing PO"
        Me.btnChekExisting.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "PO Reference NO"
        '
        'txtPOReference
        '
        Me.txtPOReference.Location = New System.Drawing.Point(127, 32)
        Me.txtPOReference.MaxLength = 23
        Me.txtPOReference.Name = "txtPOReference"
        Me.txtPOReference.Size = New System.Drawing.Size(182, 20)
        Me.txtPOReference.TabIndex = 0
        '
        'grpDistributor
        '
        Me.grpDistributor.BackColor = System.Drawing.Color.Transparent
        Me.grpDistributor.Controls.Add(Me.txtRegionalArea)
        Me.grpDistributor.Controls.Add(Me.MultiColumnCombo1)
        Me.grpDistributor.Controls.Add(Me.btnFilterDistributor)
        Me.grpDistributor.Controls.Add(Me.Label7)
        Me.grpDistributor.Controls.Add(Me.txtTerritoryArea)
        Me.grpDistributor.Controls.Add(Me.Label6)
        Me.grpDistributor.Controls.Add(Me.txtDistributorPhone)
        Me.grpDistributor.Controls.Add(Me.Label5)
        Me.grpDistributor.Controls.Add(Me.txtDistributorContact)
        Me.grpDistributor.Controls.Add(Me.Label4)
        Me.grpDistributor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDistributor.Location = New System.Drawing.Point(359, 7)
        Me.grpDistributor.Name = "grpDistributor"
        Me.grpDistributor.Size = New System.Drawing.Size(407, 126)
        Me.grpDistributor.TabIndex = 4
        Me.grpDistributor.Text = "Distributor"
        Me.grpDistributor.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtRegionalArea
        '
        Me.txtRegionalArea.BackColor = System.Drawing.SystemColors.Window
        Me.txtRegionalArea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegionalArea.Location = New System.Drawing.Point(202, 101)
        Me.txtRegionalArea.Name = "txtRegionalArea"
        Me.txtRegionalArea.ReadOnly = True
        Me.txtRegionalArea.Size = New System.Drawing.Size(198, 20)
        Me.txtRegionalArea.TabIndex = 22
        '
        'MultiColumnCombo1
        '
        Me.MultiColumnCombo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        MultiColumnCombo1_DesignTimeLayout.LayoutString = resources.GetString("MultiColumnCombo1_DesignTimeLayout.LayoutString")
        Me.MultiColumnCombo1.DesignTimeLayout = MultiColumnCombo1_DesignTimeLayout
        Me.MultiColumnCombo1.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.MultiColumnCombo1.DisplayMember = "DISTRIBUTOR_NAME"
        Me.MultiColumnCombo1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultiColumnCombo1.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.MultiColumnCombo1.Location = New System.Drawing.Point(6, 19)
        Me.MultiColumnCombo1.Name = "MultiColumnCombo1"
        Me.MultiColumnCombo1.SelectedIndex = -1
        Me.MultiColumnCombo1.SelectedItem = Nothing
        Me.MultiColumnCombo1.Size = New System.Drawing.Size(367, 20)
        Me.MultiColumnCombo1.TabIndex = 0
        Me.MultiColumnCombo1.ValueMember = "DISTRIBUTOR_ID"
        Me.MultiColumnCombo1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnFilterDistributor
        '
        Me.btnFilterDistributor.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterDistributor.Location = New System.Drawing.Point(379, 19)
        Me.btnFilterDistributor.Name = "btnFilterDistributor"
        Me.btnFilterDistributor.Size = New System.Drawing.Size(21, 20)
        Me.btnFilterDistributor.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(200, 84)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Regional Area"
        '
        'txtTerritoryArea
        '
        Me.txtTerritoryArea.BackColor = System.Drawing.SystemColors.Window
        Me.txtTerritoryArea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTerritoryArea.Location = New System.Drawing.Point(204, 61)
        Me.txtTerritoryArea.Name = "txtTerritoryArea"
        Me.txtTerritoryArea.ReadOnly = True
        Me.txtTerritoryArea.Size = New System.Drawing.Size(196, 20)
        Me.txtTerritoryArea.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(200, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Territory Area"
        '
        'txtDistributorPhone
        '
        Me.txtDistributorPhone.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistributorPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistributorPhone.Location = New System.Drawing.Point(6, 101)
        Me.txtDistributorPhone.Name = "txtDistributorPhone"
        Me.txtDistributorPhone.ReadOnly = True
        Me.txtDistributorPhone.Size = New System.Drawing.Size(183, 20)
        Me.txtDistributorPhone.TabIndex = 18
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(13, 85)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Phone"
        '
        'txtDistributorContact
        '
        Me.txtDistributorContact.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistributorContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistributorContact.Location = New System.Drawing.Point(6, 62)
        Me.txtDistributorContact.Name = "txtDistributorContact"
        Me.txtDistributorContact.ReadOnly = True
        Me.txtDistributorContact.Size = New System.Drawing.Size(182, 20)
        Me.txtDistributorContact.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Contact Person"
        '
        'XpLine1
        '
        Me.XpLine1.BackColor = System.Drawing.Color.Transparent
        Me.XpLine1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.XpLine1.LineColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.XpLine1.LineWidth = 2
        Me.XpLine1.Location = New System.Drawing.Point(3, 249)
        Me.XpLine1.Name = "XpLine1"
        Me.XpLine1.ShadowColor = System.Drawing.SystemColors.InactiveCaption
        Me.XpLine1.Size = New System.Drawing.Size(774, 10)
        Me.XpLine1.TabIndex = 5
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.btnAddNew)
        Me.PanelEx1.Controls.Add(Me.SavingChanges1)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx1.Location = New System.Drawing.Point(3, 498)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(774, 32)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.StyleMouseDown.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.PanelEx1.StyleMouseDown.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2
        Me.PanelEx1.StyleMouseDown.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder
        Me.PanelEx1.StyleMouseDown.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedText
        Me.PanelEx1.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.StyleMouseOver.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground
        Me.PanelEx1.StyleMouseOver.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground2
        Me.PanelEx1.StyleMouseOver.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBorder
        Me.PanelEx1.StyleMouseOver.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotText
        Me.PanelEx1.TabIndex = 7
        '
        'btnAddNew
        '
        Me.btnAddNew.ImageIndex = 1
        Me.btnAddNew.ImageList = Me.ImageList1
        Me.btnAddNew.Location = New System.Drawing.Point(3, 4)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(75, 23)
        Me.btnAddNew.TabIndex = 1
        Me.btnAddNew.Text = "Add &New"
        Me.btnAddNew.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'SavingChanges1
        '
        Me.SavingChanges1.BackColor = System.Drawing.Color.Transparent
        Me.SavingChanges1.Dock = System.Windows.Forms.DockStyle.Right
        Me.SavingChanges1.Location = New System.Drawing.Point(548, 0)
        Me.SavingChanges1.Name = "SavingChanges1"
        Me.SavingChanges1.Size = New System.Drawing.Size(226, 32)
        Me.SavingChanges1.TabIndex = 0
        '
        'Purchase_Order
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(780, 537)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpPurchaseOrder)
        Me.Name = "Purchase_Order"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "PURCHASE ORDER"
        CType(Me.grpPOBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPOBrandPack.ResumeLayout(False)
        CType(Me.grdPOBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpPurchaseOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPurchaseOrder.ResumeLayout(False)
        Me.XpGradientPanel1.ResumeLayout(False)
        CType(Me.grpProject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProject.ResumeLayout(False)
        Me.grpProject.PerformLayout()
        CType(Me.mcbProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.UiGroupBox1.PerformLayout()
        CType(Me.mcbPOHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDistributor.ResumeLayout(False)
        Me.grpDistributor.PerformLayout()
        CType(Me.MultiColumnCombo1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpPOBrandPack As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents grdPOBrandPack As Janus.Windows.GridEX.GridEX
    Private WithEvents grpPurchaseOrder As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents grpDistributor As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents txtRegionalArea As System.Windows.Forms.TextBox
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents txtTerritoryArea As System.Windows.Forms.TextBox
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents txtDistributorPhone As System.Windows.Forms.TextBox
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents txtDistributorContact As System.Windows.Forms.TextBox
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents btnFilterProject As DTSProjects.ButtonSearch
    Private WithEvents btnFilterDistributor As DTSProjects.ButtonSearch
    Private WithEvents mcbProject As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents XpLine1 As SteepValley.Windows.Forms.XPLine
    Private WithEvents SavingChanges1 As DTSProjects.SavingChanges
    Private WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Private WithEvents grpProject As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents txtProjectName As System.Windows.Forms.TextBox
    Private WithEvents txtProjectRefDate As System.Windows.Forms.TextBox
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPOReference As System.Windows.Forms.TextBox
    Private WithEvents btnChekExisting As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtPicRefDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents btnAddNew As Janus.Windows.EditControls.UIButton
    Friend WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents MultiColumnCombo1 As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents btnNewPOHeader As DTSProjects.AddNew
    Private WithEvents btnSearchPOHeader As DTSProjects.ButtonSearch
    Private WithEvents mcbPOHeader As Janus.Windows.GridEX.EditControls.MultiColumnCombo

End Class
