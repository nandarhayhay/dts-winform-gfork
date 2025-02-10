<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Distributor
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
        Dim DISTRIBUTOR_IDLabel As System.Windows.Forms.Label
        Dim DISTRIBUTOR_NAMELabel As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label8 As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim Label11 As System.Windows.Forms.Label
        Dim Label12 As System.Windows.Forms.Label
        Dim Label13 As System.Windows.Forms.Label
        Dim Label14 As System.Windows.Forms.Label
        Dim Label15 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Distributor))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbHolding_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbTerritoryID_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnRename = New DevComponents.DotNetBar.ButtonItem
        Me.btnFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSettings = New DevComponents.DotNetBar.ButtonItem
        Me.btnAddItem = New DevComponents.DotNetBar.ButtonItem
        Me.btnSave = New DevComponents.DotNetBar.ButtonItem
        Me.btnCancel = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnView = New DevComponents.DotNetBar.ButtonItem
        Me.btnViewTable = New DevComponents.DotNetBar.ButtonItem
        Me.btnCardView = New DevComponents.DotNetBar.ButtonItem
        Me.btnSingleCard = New DevComponents.DotNetBar.ButtonItem
        Me.grpEdit = New Janus.Windows.EditControls.UIGroupBox
        Me.txtAltImail = New System.Windows.Forms.TextBox
        Me.txtEmailAddress = New System.Windows.Forms.TextBox
        Me.txtContactMobile1 = New System.Windows.Forms.TextBox
        Me.dtPicJonDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.txtResponsiblePerson = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtMaxDistributor = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.dtPicBirtDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.ButtonSearch2 = New DTSProjects.ButtonSearch
        Me.ButtonSearch1 = New DTSProjects.ButtonSearch
        Me.mcbHolding = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.mcbTerritoryID = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.txtContactFax = New System.Windows.Forms.TextBox
        Me.txtContactPhone = New System.Windows.Forms.TextBox
        Me.txtContactPerson = New System.Windows.Forms.TextBox
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.txtNPWP = New System.Windows.Forms.TextBox
        Me.txtContactMobile = New System.Windows.Forms.TextBox
        Me.txtDistributorName = New System.Windows.Forms.TextBox
        Me.txtDistributorID = New System.Windows.Forms.TextBox
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        DISTRIBUTOR_IDLabel = New System.Windows.Forms.Label
        DISTRIBUTOR_NAMELabel = New System.Windows.Forms.Label
        Label3 = New System.Windows.Forms.Label
        Label4 = New System.Windows.Forms.Label
        Label5 = New System.Windows.Forms.Label
        Label6 = New System.Windows.Forms.Label
        Label7 = New System.Windows.Forms.Label
        Label9 = New System.Windows.Forms.Label
        Label8 = New System.Windows.Forms.Label
        Label10 = New System.Windows.Forms.Label
        Label11 = New System.Windows.Forms.Label
        Label12 = New System.Windows.Forms.Label
        Label13 = New System.Windows.Forms.Label
        Label14 = New System.Windows.Forms.Label
        Label15 = New System.Windows.Forms.Label
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpEdit.SuspendLayout()
        CType(Me.mcbHolding, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbTerritoryID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XpGradientPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DISTRIBUTOR_IDLabel
        '
        DISTRIBUTOR_IDLabel.AutoSize = True
        DISTRIBUTOR_IDLabel.BackColor = System.Drawing.Color.Transparent
        DISTRIBUTOR_IDLabel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DISTRIBUTOR_IDLabel.Location = New System.Drawing.Point(37, 17)
        DISTRIBUTOR_IDLabel.Name = "DISTRIBUTOR_IDLabel"
        DISTRIBUTOR_IDLabel.Size = New System.Drawing.Size(101, 15)
        DISTRIBUTOR_IDLabel.TabIndex = 0
        DISTRIBUTOR_IDLabel.Text = "DISTRIBUTOR ID:"
        '
        'DISTRIBUTOR_NAMELabel
        '
        DISTRIBUTOR_NAMELabel.AutoSize = True
        DISTRIBUTOR_NAMELabel.BackColor = System.Drawing.Color.Transparent
        DISTRIBUTOR_NAMELabel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DISTRIBUTOR_NAMELabel.Location = New System.Drawing.Point(37, 42)
        DISTRIBUTOR_NAMELabel.Name = "DISTRIBUTOR_NAMELabel"
        DISTRIBUTOR_NAMELabel.Size = New System.Drawing.Size(123, 15)
        DISTRIBUTOR_NAMELabel.TabIndex = 2
        DISTRIBUTOR_NAMELabel.Text = "DISTRIBUTOR NAME:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Location = New System.Drawing.Point(408, 124)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(107, 13)
        Label3.TabIndex = 4
        Label3.Text = "CONTACT MOBILE1"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label4.Location = New System.Drawing.Point(406, 19)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(83, 15)
        Label4.TabIndex = 10
        Label4.Text = "MAX DISC/OA"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Location = New System.Drawing.Point(408, 48)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(40, 13)
        Label5.TabIndex = 8
        Label5.Text = "NPWP"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.BackColor = System.Drawing.Color.Transparent
        Label6.Location = New System.Drawing.Point(408, 75)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(106, 13)
        Label6.TabIndex = 14
        Label6.Text = "CONTACT PERSON"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Location = New System.Drawing.Point(56, 274)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(59, 13)
        Label7.TabIndex = 12
        Label7.Text = "ADDRESS"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.BackColor = System.Drawing.Color.Transparent
        Label9.Location = New System.Drawing.Point(406, 100)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(99, 13)
        Label9.TabIndex = 16
        Label9.Text = "CONTACT PHONE"
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.BackColor = System.Drawing.Color.Transparent
        Label8.Location = New System.Drawing.Point(409, 177)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(27, 13)
        Label8.TabIndex = 18
        Label8.Text = "FAX"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.BackColor = System.Drawing.Color.Transparent
        Label10.Location = New System.Drawing.Point(37, 150)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(56, 13)
        Label10.TabIndex = 22
        Label10.Text = "HOLDING"
        '
        'Label11
        '
        Label11.AutoSize = True
        Label11.BackColor = System.Drawing.Color.Transparent
        Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label11.Location = New System.Drawing.Point(37, 176)
        Label11.Name = "Label11"
        Label11.Size = New System.Drawing.Size(109, 15)
        Label11.TabIndex = 23
        Label11.Text = "TERRITORY_AREA"
        '
        'Label12
        '
        Label12.AutoSize = True
        Label12.Location = New System.Drawing.Point(37, 97)
        Label12.Name = "Label12"
        Label12.Size = New System.Drawing.Size(63, 13)
        Label12.TabIndex = 33
        Label12.Text = "JOIN DATE"
        '
        'Label13
        '
        Label13.AutoSize = True
        Label13.BackColor = System.Drawing.Color.Transparent
        Label13.Location = New System.Drawing.Point(407, 152)
        Label13.Name = "Label13"
        Label13.Size = New System.Drawing.Size(107, 13)
        Label13.TabIndex = 35
        Label13.Text = "CONTACT MOBILE2"
        '
        'Label14
        '
        Label14.AutoSize = True
        Label14.Location = New System.Drawing.Point(50, 213)
        Label14.Name = "Label14"
        Label14.Size = New System.Drawing.Size(73, 13)
        Label14.TabIndex = 38
        Label14.Text = "Email Address"
        '
        'Label15
        '
        Label15.AutoSize = True
        Label15.Location = New System.Drawing.Point(50, 240)
        Label15.Name = "Label15"
        Label15.Size = New System.Drawing.Size(76, 13)
        Label15.TabIndex = 40
        Label15.Text = "Alternatif Email"
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
        Me.ImageList1.Images.SetKeyName(7, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(8, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(9, "TextEdit.png")
        Me.ImageList1.Images.SetKeyName(10, "DB_Refresh.ico")
        Me.ImageList1.Images.SetKeyName(11, "Search.png")
        Me.ImageList1.Images.SetKeyName(12, "Distributor registering.ico")
        Me.ImageList1.Images.SetKeyName(13, "View.jpg")
        Me.ImageList1.Images.SetKeyName(14, "printer.ico")
        Me.ImageList1.Images.SetKeyName(15, "Export.bmp")
        Me.ImageList1.Images.SetKeyName(16, "PageSetup.BMP")
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.CardCaptionPrefix = "Distributor => "
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 374)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.RowFormatStyle.ForeColor = System.Drawing.SystemColors.InfoText
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(820, 353)
        Me.GridEX1.TabIndex = 0
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ItemPanel1
        '
        '
        '
        '
        Me.ItemPanel1.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ItemPanel1.BackgroundStyle.BackColor2 = System.Drawing.SystemColors.MenuBar
        Me.ItemPanel1.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderBottomWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.ItemPanel1.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderLeftWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderRightWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderTopWidth = 1
        Me.ItemPanel1.BackgroundStyle.PaddingBottom = 1
        Me.ItemPanel1.BackgroundStyle.PaddingLeft = 1
        Me.ItemPanel1.BackgroundStyle.PaddingRight = 1
        Me.ItemPanel1.BackgroundStyle.PaddingTop = 1
        Me.ItemPanel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.ItemPanel1.Images = Me.ImageList1
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnAddItem, Me.btnSave, Me.btnCancel, Me.btnRefresh, Me.btnView})
        Me.ItemPanel1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical
        Me.ItemPanel1.Location = New System.Drawing.Point(820, 45)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(77, 682)
        Me.ItemPanel1.TabIndex = 1
        Me.ItemPanel1.Text = "ItemPanel1"
        '
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 2
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnSettingGrid, Me.btnFilter, Me.btnRename, Me.btnFieldChooser, Me.btnExport, Me.btnPrint, Me.btnPageSettings})
        Me.btnGrid.Text = "Grid"
        '
        'btnSettingGrid
        '
        Me.btnSettingGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSettingGrid.ImageIndex = 4
        Me.btnSettingGrid.Name = "btnSettingGrid"
        Me.btnSettingGrid.Text = "SettingGrid"
        '
        'btnFilter
        '
        Me.btnFilter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnFilter.ImageIndex = 3
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Text = "Custom Filter"
        '
        'btnRename
        '
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Text = "Rename Column"
        '
        'btnFieldChooser
        '
        Me.btnFieldChooser.Name = "btnFieldChooser"
        Me.btnFieldChooser.Text = "Show Field Chooser"
        '
        'btnExport
        '
        Me.btnExport.ImageIndex = 15
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export DataGrid"
        '
        'btnPrint
        '
        Me.btnPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrint.ImageIndex = 14
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Text = "Print DataGrid"
        '
        'btnPageSettings
        '
        Me.btnPageSettings.ImageIndex = 16
        Me.btnPageSettings.Name = "btnPageSettings"
        Me.btnPageSettings.Text = "Page Settings"
        '
        'btnAddItem
        '
        Me.btnAddItem.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnAddItem.ImageIndex = 1
        Me.btnAddItem.Name = "btnAddItem"
        Me.btnAddItem.Text = "Add Item"
        '
        'btnSave
        '
        Me.btnSave.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSave.ImageIndex = 6
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Me.btnCancel.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnCancel.ImageIndex = 0
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Text = "Cancel"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 10
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Text = "Refresh"
        '
        'btnView
        '
        Me.btnView.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnView.ImageIndex = 13
        Me.btnView.Name = "btnView"
        Me.btnView.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnViewTable, Me.btnCardView, Me.btnSingleCard})
        Me.btnView.Text = "View"
        '
        'btnViewTable
        '
        Me.btnViewTable.Name = "btnViewTable"
        Me.btnViewTable.Text = "Table View"
        '
        'btnCardView
        '
        Me.btnCardView.Name = "btnCardView"
        Me.btnCardView.Text = "Card View"
        '
        'btnSingleCard
        '
        Me.btnSingleCard.Name = "btnSingleCard"
        Me.btnSingleCard.Text = "Single Card View"
        '
        'grpEdit
        '
        Me.grpEdit.BackColor = System.Drawing.Color.Transparent
        Me.grpEdit.Controls.Add(Label15)
        Me.grpEdit.Controls.Add(Me.txtAltImail)
        Me.grpEdit.Controls.Add(Label14)
        Me.grpEdit.Controls.Add(Me.txtEmailAddress)
        Me.grpEdit.Controls.Add(Label13)
        Me.grpEdit.Controls.Add(Me.txtContactMobile1)
        Me.grpEdit.Controls.Add(Me.dtPicJonDate)
        Me.grpEdit.Controls.Add(Label12)
        Me.grpEdit.Controls.Add(Me.txtResponsiblePerson)
        Me.grpEdit.Controls.Add(Me.Label2)
        Me.grpEdit.Controls.Add(Me.txtMaxDistributor)
        Me.grpEdit.Controls.Add(Me.dtPicBirtDate)
        Me.grpEdit.Controls.Add(Me.Label1)
        Me.grpEdit.Controls.Add(Me.ButtonSearch2)
        Me.grpEdit.Controls.Add(Me.ButtonSearch1)
        Me.grpEdit.Controls.Add(Label11)
        Me.grpEdit.Controls.Add(Label10)
        Me.grpEdit.Controls.Add(Me.mcbHolding)
        Me.grpEdit.Controls.Add(Me.mcbTerritoryID)
        Me.grpEdit.Controls.Add(Label8)
        Me.grpEdit.Controls.Add(Me.txtContactFax)
        Me.grpEdit.Controls.Add(Label9)
        Me.grpEdit.Controls.Add(Me.txtContactPhone)
        Me.grpEdit.Controls.Add(Label6)
        Me.grpEdit.Controls.Add(Me.txtContactPerson)
        Me.grpEdit.Controls.Add(Label7)
        Me.grpEdit.Controls.Add(Me.txtAddress)
        Me.grpEdit.Controls.Add(Label4)
        Me.grpEdit.Controls.Add(Label5)
        Me.grpEdit.Controls.Add(Me.txtNPWP)
        Me.grpEdit.Controls.Add(Label3)
        Me.grpEdit.Controls.Add(Me.txtContactMobile)
        Me.grpEdit.Controls.Add(DISTRIBUTOR_NAMELabel)
        Me.grpEdit.Controls.Add(Me.txtDistributorName)
        Me.grpEdit.Controls.Add(DISTRIBUTOR_IDLabel)
        Me.grpEdit.Controls.Add(Me.txtDistributorID)
        Me.grpEdit.Image = CType(resources.GetObject("grpEdit.Image"), System.Drawing.Image)
        Me.grpEdit.Location = New System.Drawing.Point(12, 5)
        Me.grpEdit.Name = "grpEdit"
        Me.grpEdit.Size = New System.Drawing.Size(733, 318)
        Me.grpEdit.TabIndex = 6
        Me.grpEdit.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtAltImail
        '
        Me.txtAltImail.Location = New System.Drawing.Point(176, 235)
        Me.txtAltImail.MaxLength = 150
        Me.txtAltImail.Multiline = True
        Me.txtAltImail.Name = "txtAltImail"
        Me.txtAltImail.Size = New System.Drawing.Size(349, 21)
        Me.txtAltImail.TabIndex = 39
        '
        'txtEmailAddress
        '
        Me.txtEmailAddress.Location = New System.Drawing.Point(176, 208)
        Me.txtEmailAddress.MaxLength = 150
        Me.txtEmailAddress.Multiline = True
        Me.txtEmailAddress.Name = "txtEmailAddress"
        Me.txtEmailAddress.Size = New System.Drawing.Size(349, 21)
        Me.txtEmailAddress.TabIndex = 37
        '
        'txtContactMobile1
        '
        Me.txtContactMobile1.Location = New System.Drawing.Point(518, 148)
        Me.txtContactMobile1.MaxLength = 20
        Me.txtContactMobile1.Name = "txtContactMobile1"
        Me.txtContactMobile1.Size = New System.Drawing.Size(177, 20)
        Me.txtContactMobile1.TabIndex = 36
        '
        'dtPicJonDate
        '
        Me.dtPicJonDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicJonDate.BorderStyle = Janus.Windows.CalendarCombo.BorderStyle.Flat
        Me.dtPicJonDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicJonDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicJonDate.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicJonDate.DropDownCalendar.Name = ""
        Me.dtPicJonDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicJonDate.Location = New System.Drawing.Point(176, 93)
        Me.dtPicJonDate.Name = "dtPicJonDate"
        Me.dtPicJonDate.ShowTodayButton = False
        Me.dtPicJonDate.Size = New System.Drawing.Size(182, 20)
        Me.dtPicJonDate.TabIndex = 34
        Me.dtPicJonDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'txtResponsiblePerson
        '
        Me.txtResponsiblePerson.Location = New System.Drawing.Point(176, 66)
        Me.txtResponsiblePerson.MaxLength = 50
        Me.txtResponsiblePerson.Name = "txtResponsiblePerson"
        Me.txtResponsiblePerson.Size = New System.Drawing.Size(182, 20)
        Me.txtResponsiblePerson.TabIndex = 32
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(37, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(133, 13)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "RESPONSIBLE_PERSON"
        '
        'txtMaxDistributor
        '
        Me.txtMaxDistributor.DecimalDigits = 3
        Me.txtMaxDistributor.Location = New System.Drawing.Point(518, 17)
        Me.txtMaxDistributor.Name = "txtMaxDistributor"
        Me.txtMaxDistributor.Size = New System.Drawing.Size(78, 20)
        Me.txtMaxDistributor.TabIndex = 30
        Me.txtMaxDistributor.Text = "0.000"
        Me.txtMaxDistributor.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtMaxDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'dtPicBirtDate
        '
        Me.dtPicBirtDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicBirtDate.BorderStyle = Janus.Windows.CalendarCombo.BorderStyle.Flat
        Me.dtPicBirtDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicBirtDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicBirtDate.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicBirtDate.DropDownCalendar.Name = ""
        Me.dtPicBirtDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicBirtDate.Location = New System.Drawing.Point(176, 122)
        Me.dtPicBirtDate.Name = "dtPicBirtDate"
        Me.dtPicBirtDate.ShowTodayButton = False
        Me.dtPicBirtDate.Size = New System.Drawing.Size(182, 20)
        Me.dtPicBirtDate.TabIndex = 29
        Me.dtPicBirtDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(37, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "BIRTH DATE"
        '
        'ButtonSearch2
        '
        Me.ButtonSearch2.BackColor = System.Drawing.Color.Transparent
        Me.ButtonSearch2.Location = New System.Drawing.Point(364, 178)
        Me.ButtonSearch2.Name = "ButtonSearch2"
        Me.ButtonSearch2.Size = New System.Drawing.Size(22, 17)
        Me.ButtonSearch2.TabIndex = 27
        '
        'ButtonSearch1
        '
        Me.ButtonSearch1.BackColor = System.Drawing.Color.Transparent
        Me.ButtonSearch1.Location = New System.Drawing.Point(364, 150)
        Me.ButtonSearch1.Name = "ButtonSearch1"
        Me.ButtonSearch1.Size = New System.Drawing.Size(22, 19)
        Me.ButtonSearch1.TabIndex = 26
        '
        'mcbHolding
        '
        Me.mcbHolding.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.mcbHolding.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        mcbHolding_DesignTimeLayout.LayoutString = resources.GetString("mcbHolding_DesignTimeLayout.LayoutString")
        Me.mcbHolding.DesignTimeLayout = mcbHolding_DesignTimeLayout
        Me.mcbHolding.DisplayMember = "DISTRIBUTOR_NAME"
        Me.mcbHolding.Location = New System.Drawing.Point(176, 148)
        Me.mcbHolding.Name = "mcbHolding"
        Me.mcbHolding.SelectedIndex = -1
        Me.mcbHolding.SelectedItem = Nothing
        Me.mcbHolding.Size = New System.Drawing.Size(182, 20)
        Me.mcbHolding.TabIndex = 2
        Me.mcbHolding.ValueMember = "DISTRIBUTOR_ID"
        Me.mcbHolding.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'mcbTerritoryID
        '
        Me.mcbTerritoryID.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.mcbTerritoryID.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        mcbTerritoryID_DesignTimeLayout.LayoutString = resources.GetString("mcbTerritoryID_DesignTimeLayout.LayoutString")
        Me.mcbTerritoryID.DesignTimeLayout = mcbTerritoryID_DesignTimeLayout
        Me.mcbTerritoryID.DisplayMember = "TERRITORY_AREA"
        Me.mcbTerritoryID.FlatBorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.mcbTerritoryID.Location = New System.Drawing.Point(176, 176)
        Me.mcbTerritoryID.Name = "mcbTerritoryID"
        Me.mcbTerritoryID.SelectedIndex = -1
        Me.mcbTerritoryID.SelectedItem = Nothing
        Me.mcbTerritoryID.Size = New System.Drawing.Size(182, 20)
        Me.mcbTerritoryID.TabIndex = 3
        Me.mcbTerritoryID.ValueMember = "TERRITORY_ID"
        Me.mcbTerritoryID.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtContactFax
        '
        Me.txtContactFax.Location = New System.Drawing.Point(518, 177)
        Me.txtContactFax.MaxLength = 20
        Me.txtContactFax.Name = "txtContactFax"
        Me.txtContactFax.Size = New System.Drawing.Size(177, 20)
        Me.txtContactFax.TabIndex = 9
        '
        'txtContactPhone
        '
        Me.txtContactPhone.Location = New System.Drawing.Point(518, 95)
        Me.txtContactPhone.MaxLength = 20
        Me.txtContactPhone.Name = "txtContactPhone"
        Me.txtContactPhone.Size = New System.Drawing.Size(143, 20)
        Me.txtContactPhone.TabIndex = 8
        '
        'txtContactPerson
        '
        Me.txtContactPerson.Location = New System.Drawing.Point(518, 69)
        Me.txtContactPerson.MaxLength = 30
        Me.txtContactPerson.Name = "txtContactPerson"
        Me.txtContactPerson.Size = New System.Drawing.Size(144, 20)
        Me.txtContactPerson.TabIndex = 7
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(176, 264)
        Me.txtAddress.MaxLength = 150
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(519, 46)
        Me.txtAddress.TabIndex = 6
        '
        'txtNPWP
        '
        Me.txtNPWP.Location = New System.Drawing.Point(518, 40)
        Me.txtNPWP.MaxLength = 30
        Me.txtNPWP.Name = "txtNPWP"
        Me.txtNPWP.Size = New System.Drawing.Size(144, 20)
        Me.txtNPWP.TabIndex = 4
        '
        'txtContactMobile
        '
        Me.txtContactMobile.Location = New System.Drawing.Point(519, 120)
        Me.txtContactMobile.MaxLength = 20
        Me.txtContactMobile.Name = "txtContactMobile"
        Me.txtContactMobile.Size = New System.Drawing.Size(176, 20)
        Me.txtContactMobile.TabIndex = 10
        '
        'txtDistributorName
        '
        Me.txtDistributorName.Location = New System.Drawing.Point(176, 40)
        Me.txtDistributorName.MaxLength = 50
        Me.txtDistributorName.Name = "txtDistributorName"
        Me.txtDistributorName.Size = New System.Drawing.Size(182, 20)
        Me.txtDistributorName.TabIndex = 1
        '
        'txtDistributorID
        '
        Me.txtDistributorID.Location = New System.Drawing.Point(176, 15)
        Me.txtDistributorID.MaxLength = 10
        Me.txtDistributorID.Name = "txtDistributorID"
        Me.txtDistributorID.Size = New System.Drawing.Size(78, 20)
        Me.txtDistributorID.TabIndex = 0
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.Transparent
        Me.FilterEditor1.ColorScheme = "FilterEditorColorScheme"
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Contains
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 0)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(16, 45)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(897, 45)
        Me.FilterEditor1.SortFieldList = False
        Me.FilterEditor1.SourceControl = Me.GridEX1
        Me.FilterEditor1.Visible = False
        '
        'Bar1
        '
        Me.Bar1.Location = New System.Drawing.Point(347, 380)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(13, 25)
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar1.TabIndex = 7
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
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
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.grpEdit)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpGradientPanel1.EndColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.XpGradientPanel1.Location = New System.Drawing.Point(0, 45)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(820, 329)
        Me.XpGradientPanel1.StartColor = System.Drawing.SystemColors.MenuBar
        Me.XpGradientPanel1.TabIndex = 9
        '
        'Distributor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(897, 727)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.XpGradientPanel1)
        Me.Controls.Add(Me.ItemPanel1)
        Me.Controls.Add(Me.Bar1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Distributor"
        Me.Text = "Distributor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpEdit.ResumeLayout(False)
        Me.grpEdit.PerformLayout()
        CType(Me.mcbHolding, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbTerritoryID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XpGradientPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAddItem As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSave As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCancel As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRename As DevComponents.DotNetBar.ButtonItem
    Private WithEvents grpEdit As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents txtDistributorID As System.Windows.Forms.TextBox
    Private WithEvents txtDistributorName As System.Windows.Forms.TextBox
    Private WithEvents mcbHolding As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents mcbTerritoryID As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents txtContactFax As System.Windows.Forms.TextBox
    Private WithEvents txtContactPhone As System.Windows.Forms.TextBox
    Private WithEvents txtContactPerson As System.Windows.Forms.TextBox
    Private WithEvents txtAddress As System.Windows.Forms.TextBox
    Private WithEvents txtNPWP As System.Windows.Forms.TextBox
    Private WithEvents txtContactMobile As System.Windows.Forms.TextBox
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents btnView As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnViewTable As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCardView As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSingleCard As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents ButtonSearch1 As DTSProjects.ButtonSearch
    Private WithEvents ButtonSearch2 As DTSProjects.ButtonSearch
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents dtPicBirtDate As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents txtMaxDistributor As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents txtResponsiblePerson As System.Windows.Forms.TextBox
    Private WithEvents dtPicJonDate As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents txtContactMobile1 As System.Windows.Forms.TextBox
    Private WithEvents txtEmailAddress As System.Windows.Forms.TextBox
    Private WithEvents txtAltImail As System.Windows.Forms.TextBox

End Class
