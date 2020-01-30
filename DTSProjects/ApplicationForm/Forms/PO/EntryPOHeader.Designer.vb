<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EntryPOHeader
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EntryPOHeader))
        Dim mcbProject_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdPO_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtPicRefDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.btnChekExisting = New Janus.Windows.EditControls.UIButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPOReference = New System.Windows.Forms.TextBox
        Me.grpDistributor = New Janus.Windows.EditControls.UIGroupBox
        Me.grpProject = New Janus.Windows.EditControls.UIGroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtProjectRefDate = New System.Windows.Forms.TextBox
        Me.txtProjectName = New System.Windows.Forms.TextBox
        Me.mcbProject = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.btnFilterProject = New DTSProjects.ButtonSearch
        Me.txtRegionalArea = New System.Windows.Forms.TextBox
        Me.mcbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.btnFilterDistributor = New DTSProjects.ButtonSearch
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtTerritoryArea = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtDistributorPhone = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtDistributorContact = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.grdPO = New Janus.Windows.GridEX.GridEX
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.btnInsert = New Janus.Windows.EditControls.UIButton
        Me.btnAddNew = New Janus.Windows.EditControls.UIButton
        Me.btnClose = New Janus.Windows.EditControls.UIButton
        Me.pnlPencapaian = New DevComponents.DotNetBar.ExpandablePanel
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.dtpicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.btnAplyFilter = New Janus.Windows.EditControls.UIButton
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.grpDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDistributor.SuspendLayout()
        CType(Me.grpProject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProject.SuspendLayout()
        CType(Me.mcbProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        Me.pnlPencapaian.SuspendLayout()
        Me.PanelEx2.SuspendLayout()
        Me.SuspendLayout()
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UiGroupBox1.Controls.Add(Me.Label2)
        Me.UiGroupBox1.Controls.Add(Me.dtPicRefDate)
        Me.UiGroupBox1.Controls.Add(Me.btnChekExisting)
        Me.UiGroupBox1.Controls.Add(Me.Label1)
        Me.UiGroupBox1.Controls.Add(Me.txtPOReference)
        Me.UiGroupBox1.Location = New System.Drawing.Point(8, 12)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(286, 126)
        Me.UiGroupBox1.TabIndex = 12
        Me.UiGroupBox1.Text = "PO Header"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "PO_ Date"
        '
        'dtPicRefDate
        '
        Me.dtPicRefDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicRefDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        Me.dtPicRefDate.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        '
        '
        '
        Me.dtPicRefDate.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicRefDate.DropDownCalendar.Name = ""
        Me.dtPicRefDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicRefDate.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicRefDate.Location = New System.Drawing.Point(88, 71)
        Me.dtPicRefDate.Name = "dtPicRefDate"
        Me.dtPicRefDate.ShowTodayButton = False
        Me.dtPicRefDate.Size = New System.Drawing.Size(190, 20)
        Me.dtPicRefDate.TabIndex = 1
        Me.dtPicRefDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'btnChekExisting
        '
        Me.btnChekExisting.ImageIndex = 15
        Me.btnChekExisting.ImageList = Me.ImageList1
        Me.btnChekExisting.Location = New System.Drawing.Point(9, 97)
        Me.btnChekExisting.Name = "btnChekExisting"
        Me.btnChekExisting.Size = New System.Drawing.Size(119, 23)
        Me.btnChekExisting.TabIndex = 2
        Me.btnChekExisting.Text = "Check Existing PO"
        Me.btnChekExisting.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "PO_REF_NO"
        '
        'txtPOReference
        '
        Me.txtPOReference.Location = New System.Drawing.Point(88, 31)
        Me.txtPOReference.MaxLength = 22
        Me.txtPOReference.Name = "txtPOReference"
        Me.txtPOReference.Size = New System.Drawing.Size(190, 20)
        Me.txtPOReference.TabIndex = 0
        '
        'grpDistributor
        '
        Me.grpDistributor.BackColor = System.Drawing.Color.Transparent
        Me.grpDistributor.Controls.Add(Me.grpProject)
        Me.grpDistributor.Controls.Add(Me.txtRegionalArea)
        Me.grpDistributor.Controls.Add(Me.mcbDistributor)
        Me.grpDistributor.Controls.Add(Me.btnFilterDistributor)
        Me.grpDistributor.Controls.Add(Me.Label7)
        Me.grpDistributor.Controls.Add(Me.txtTerritoryArea)
        Me.grpDistributor.Controls.Add(Me.Label6)
        Me.grpDistributor.Controls.Add(Me.txtDistributorPhone)
        Me.grpDistributor.Controls.Add(Me.Label5)
        Me.grpDistributor.Controls.Add(Me.txtDistributorContact)
        Me.grpDistributor.Controls.Add(Me.Label4)
        Me.grpDistributor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDistributor.Location = New System.Drawing.Point(300, 12)
        Me.grpDistributor.Name = "grpDistributor"
        Me.grpDistributor.Size = New System.Drawing.Size(416, 126)
        Me.grpDistributor.TabIndex = 11
        Me.grpDistributor.Text = "Distributor"
        Me.grpDistributor.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grpProject
        '
        Me.grpProject.Controls.Add(Me.Label8)
        Me.grpProject.Controls.Add(Me.Label3)
        Me.grpProject.Controls.Add(Me.txtProjectRefDate)
        Me.grpProject.Controls.Add(Me.txtProjectName)
        Me.grpProject.Controls.Add(Me.mcbProject)
        Me.grpProject.Controls.Add(Me.btnFilterProject)
        Me.grpProject.Location = New System.Drawing.Point(466, 9)
        Me.grpProject.Name = "grpProject"
        Me.grpProject.Size = New System.Drawing.Size(11, 120)
        Me.grpProject.TabIndex = 23
        Me.grpProject.Text = "Project"
        Me.grpProject.Visible = False
        Me.grpProject.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 78)
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
        Me.Label3.Location = New System.Drawing.Point(6, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Project Name"
        '
        'txtProjectRefDate
        '
        Me.txtProjectRefDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtProjectRefDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProjectRefDate.Location = New System.Drawing.Point(6, 94)
        Me.txtProjectRefDate.Name = "txtProjectRefDate"
        Me.txtProjectRefDate.ReadOnly = True
        Me.txtProjectRefDate.Size = New System.Drawing.Size(175, 20)
        Me.txtProjectRefDate.TabIndex = 18
        '
        'txtProjectName
        '
        Me.txtProjectName.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtProjectName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProjectName.Location = New System.Drawing.Point(6, 55)
        Me.txtProjectName.Name = "txtProjectName"
        Me.txtProjectName.ReadOnly = True
        Me.txtProjectName.Size = New System.Drawing.Size(175, 20)
        Me.txtProjectName.TabIndex = 17
        '
        'mcbProject
        '
        Me.mcbProject.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbProject_DesignTimeLayout.LayoutString = resources.GetString("mcbProject_DesignTimeLayout.LayoutString")
        Me.mcbProject.DesignTimeLayout = mcbProject_DesignTimeLayout
        Me.mcbProject.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbProject.DisplayMember = "PROJECT_NAME"
        Me.mcbProject.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbProject.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbProject.Location = New System.Drawing.Point(6, 16)
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
        Me.btnFilterProject.Location = New System.Drawing.Point(171, 16)
        Me.btnFilterProject.Name = "btnFilterProject"
        Me.btnFilterProject.Size = New System.Drawing.Size(21, 22)
        Me.btnFilterProject.TabIndex = 15
        '
        'txtRegionalArea
        '
        Me.txtRegionalArea.BackColor = System.Drawing.SystemColors.Window
        Me.txtRegionalArea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegionalArea.Location = New System.Drawing.Point(198, 101)
        Me.txtRegionalArea.Name = "txtRegionalArea"
        Me.txtRegionalArea.ReadOnly = True
        Me.txtRegionalArea.Size = New System.Drawing.Size(209, 20)
        Me.txtRegionalArea.TabIndex = 22
        '
        'mcbDistributor
        '
        Me.mcbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbDistributor_DesignTimeLayout.LayoutString = resources.GetString("mcbDistributor_DesignTimeLayout.LayoutString")
        Me.mcbDistributor.DesignTimeLayout = mcbDistributor_DesignTimeLayout
        Me.mcbDistributor.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.mcbDistributor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbDistributor.Location = New System.Drawing.Point(10, 19)
        Me.mcbDistributor.Name = "mcbDistributor"
        Me.mcbDistributor.SelectedIndex = -1
        Me.mcbDistributor.SelectedItem = Nothing
        Me.mcbDistributor.Size = New System.Drawing.Size(370, 20)
        Me.mcbDistributor.TabIndex = 0
        Me.mcbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.mcbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnFilterDistributor
        '
        Me.btnFilterDistributor.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterDistributor.Location = New System.Drawing.Point(386, 19)
        Me.btnFilterDistributor.Name = "btnFilterDistributor"
        Me.btnFilterDistributor.Size = New System.Drawing.Size(21, 20)
        Me.btnFilterDistributor.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(194, 84)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Regional Area"
        '
        'txtTerritoryArea
        '
        Me.txtTerritoryArea.BackColor = System.Drawing.SystemColors.Window
        Me.txtTerritoryArea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTerritoryArea.Location = New System.Drawing.Point(198, 61)
        Me.txtTerritoryArea.Name = "txtTerritoryArea"
        Me.txtTerritoryArea.ReadOnly = True
        Me.txtTerritoryArea.Size = New System.Drawing.Size(209, 20)
        Me.txtTerritoryArea.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(194, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Territory Area"
        '
        'txtDistributorPhone
        '
        Me.txtDistributorPhone.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistributorPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistributorPhone.Location = New System.Drawing.Point(9, 101)
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
        Me.Label5.Location = New System.Drawing.Point(7, 85)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Phone"
        '
        'txtDistributorContact
        '
        Me.txtDistributorContact.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistributorContact.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistributorContact.Location = New System.Drawing.Point(10, 61)
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
        Me.Label4.Location = New System.Drawing.Point(8, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Contact person"
        '
        'grdPO
        '
        Me.grdPO.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPO.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdPO.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.grdPO.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        grdPO_DesignTimeLayout.LayoutString = resources.GetString("grdPO_DesignTimeLayout.LayoutString")
        Me.grdPO.DesignTimeLayout = grdPO_DesignTimeLayout
        Me.grdPO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPO.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdPO.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdPO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdPO.GroupByBoxVisible = False
        Me.grdPO.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdPO.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdPO.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdPO.Location = New System.Drawing.Point(0, 48)
        Me.grdPO.Name = "grdPO"
        Me.grdPO.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdPO.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdPO.RecordNavigator = True
        Me.grdPO.Size = New System.Drawing.Size(708, 259)
        Me.grdPO.TabIndex = 0
        Me.grdPO.UpdateOnLeave = False
        Me.grdPO.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdPO.WatermarkImage.Image = CType(resources.GetObject("grdPO.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdPO.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.btnInsert)
        Me.PanelEx1.Controls.Add(Me.btnAddNew)
        Me.PanelEx1.Controls.Add(Me.btnClose)
        Me.PanelEx1.Location = New System.Drawing.Point(8, 141)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(708, 40)
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
        Me.PanelEx1.TabIndex = 0
        '
        'btnInsert
        '
        Me.btnInsert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInsert.ImageIndex = 9
        Me.btnInsert.ImageList = Me.ImageList1
        Me.btnInsert.Location = New System.Drawing.Point(510, 7)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(81, 27)
        Me.btnInsert.TabIndex = 1
        Me.btnInsert.Text = "&Insert"
        Me.btnInsert.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnAddNew
        '
        Me.btnAddNew.ImageIndex = 1
        Me.btnAddNew.ImageList = Me.ImageList1
        Me.btnAddNew.Location = New System.Drawing.Point(3, 8)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(81, 26)
        Me.btnAddNew.TabIndex = 0
        Me.btnAddNew.Text = "Add &New"
        Me.btnAddNew.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.ImageKey = "Cancel.ico"
        Me.btnClose.ImageList = Me.ImageList1
        Me.btnClose.Location = New System.Drawing.Point(597, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(102, 27)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "&Cancel/Close"
        Me.btnClose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'pnlPencapaian
        '
        Me.pnlPencapaian.CanvasColor = System.Drawing.SystemColors.Control
        Me.pnlPencapaian.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.pnlPencapaian.Controls.Add(Me.grdPO)
        Me.pnlPencapaian.Controls.Add(Me.PanelEx2)
        Me.pnlPencapaian.Location = New System.Drawing.Point(8, 187)
        Me.pnlPencapaian.Name = "pnlPencapaian"
        Me.pnlPencapaian.Size = New System.Drawing.Size(708, 307)
        Me.pnlPencapaian.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlPencapaian.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.pnlPencapaian.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.pnlPencapaian.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.pnlPencapaian.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.pnlPencapaian.Style.GradientAngle = 90
        Me.pnlPencapaian.TabIndex = 25
        Me.pnlPencapaian.TitleHeight = 15
        Me.pnlPencapaian.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlPencapaian.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.pnlPencapaian.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.pnlPencapaian.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.pnlPencapaian.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.pnlPencapaian.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.pnlPencapaian.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.pnlPencapaian.TitleStyle.GradientAngle = 90
        Me.pnlPencapaian.TitleText = " "
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx2.Controls.Add(Me.Label9)
        Me.PanelEx2.Controls.Add(Me.Label10)
        Me.PanelEx2.Controls.Add(Me.dtpicUntil)
        Me.PanelEx2.Controls.Add(Me.dtPicFrom)
        Me.PanelEx2.Controls.Add(Me.btnAplyFilter)
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx2.Location = New System.Drawing.Point(0, 15)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(708, 33)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(323, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "UNTIL"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(66, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 16)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "FROM"
        '
        'dtpicUntil
        '
        Me.dtpicUntil.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtpicUntil.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtpicUntil.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtpicUntil.DropDownCalendar.FirstMonth = New Date(2008, 5, 1, 0, 0, 0, 0)
        Me.dtpicUntil.DropDownCalendar.Name = ""
        Me.dtpicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtpicUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtpicUntil.Location = New System.Drawing.Point(368, 7)
        Me.dtpicUntil.Name = "dtpicUntil"
        Me.dtpicUntil.ShowTodayButton = False
        Me.dtpicUntil.Size = New System.Drawing.Size(200, 20)
        Me.dtpicUntil.TabIndex = 1
        Me.dtpicUntil.Value = New Date(2008, 5, 26, 0, 0, 0, 0)
        Me.dtpicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicFrom
        '
        Me.dtPicFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicFrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFrom.DropDownCalendar.FirstMonth = New Date(2008, 5, 1, 0, 0, 0, 0)
        Me.dtPicFrom.DropDownCalendar.Name = ""
        Me.dtPicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicFrom.Location = New System.Drawing.Point(118, 7)
        Me.dtPicFrom.Name = "dtPicFrom"
        Me.dtPicFrom.ShowTodayButton = False
        Me.dtPicFrom.Size = New System.Drawing.Size(200, 20)
        Me.dtPicFrom.TabIndex = 0
        Me.dtPicFrom.Value = New Date(2008, 5, 26, 0, 0, 0, 0)
        Me.dtPicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'btnAplyFilter
        '
        Me.btnAplyFilter.Location = New System.Drawing.Point(574, 7)
        Me.btnAplyFilter.Name = "btnAplyFilter"
        Me.btnAplyFilter.Size = New System.Drawing.Size(75, 20)
        Me.btnAplyFilter.TabIndex = 8
        Me.btnAplyFilter.Text = "Apply filter"
        Me.btnAplyFilter.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'EntryPOHeader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(723, 509)
        Me.Controls.Add(Me.pnlPencapaian)
        Me.Controls.Add(Me.PanelEx1)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Controls.Add(Me.grpDistributor)
        Me.Name = "EntryPOHeader"
        Me.Text = "EntryPOHeader"
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.UiGroupBox1.PerformLayout()
        CType(Me.grpDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDistributor.ResumeLayout(False)
        Me.grpDistributor.PerformLayout()
        CType(Me.grpProject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProject.ResumeLayout(False)
        Me.grpProject.PerformLayout()
        CType(Me.mcbProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.pnlPencapaian.ResumeLayout(False)
        Me.PanelEx2.ResumeLayout(False)
        Me.PanelEx2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents dtPicRefDate As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents btnChekExisting As Janus.Windows.EditControls.UIButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents txtPOReference As System.Windows.Forms.TextBox
    Private WithEvents grpDistributor As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents grpProject As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents txtProjectRefDate As System.Windows.Forms.TextBox
    Private WithEvents txtProjectName As System.Windows.Forms.TextBox
    Private WithEvents mcbProject As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents btnFilterProject As DTSProjects.ButtonSearch
    Private WithEvents txtRegionalArea As System.Windows.Forms.TextBox
    Private WithEvents mcbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents btnFilterDistributor As DTSProjects.ButtonSearch
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents txtTerritoryArea As System.Windows.Forms.TextBox
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents txtDistributorPhone As System.Windows.Forms.TextBox
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents txtDistributorContact As System.Windows.Forms.TextBox
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents grdPO As Janus.Windows.GridEX.GridEX
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Public WithEvents btnInsert As Janus.Windows.EditControls.UIButton
    Public WithEvents btnAddNew As Janus.Windows.EditControls.UIButton
    Public WithEvents btnClose As Janus.Windows.EditControls.UIButton
    Private WithEvents pnlPencapaian As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Private WithEvents btnAplyFilter As Janus.Windows.EditControls.UIButton
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents dtpicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
End Class
