<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Transporter
    Inherits BaseBigForm

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
        Dim Label8 As System.Windows.Forms.Label
        Dim Label9 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Dim Label7 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim DISTRIBUTOR_NAMELabel As System.Windows.Forms.Label
        Dim DISTRIBUTOR_IDLabel As System.Windows.Forms.Label
        Dim Label10 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Transporter))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSettings = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnAddNew = New DevComponents.DotNetBar.ButtonItem
        Me.btnSave = New DevComponents.DotNetBar.ButtonItem
        Me.grpEdit = New Janus.Windows.EditControls.UIGroupBox
        Me.lblAutoGenerate = New System.Windows.Forms.Label
        Me.txtPostalCode = New System.Windows.Forms.TextBox
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtResponsiblePerson = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtPicBirtDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtContactFax = New System.Windows.Forms.TextBox
        Me.txtContactPhone = New System.Windows.Forms.TextBox
        Me.txtContactPerson = New System.Windows.Forms.TextBox
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.txtNPWP = New System.Windows.Forms.TextBox
        Me.txtContactMobile = New System.Windows.Forms.TextBox
        Me.txtTransporterName = New System.Windows.Forms.TextBox
        Me.pnlEntry = New System.Windows.Forms.Panel
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlGrid = New System.Windows.Forms.Panel
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Label8 = New System.Windows.Forms.Label
        Label9 = New System.Windows.Forms.Label
        Label6 = New System.Windows.Forms.Label
        Label7 = New System.Windows.Forms.Label
        Label5 = New System.Windows.Forms.Label
        Label3 = New System.Windows.Forms.Label
        DISTRIBUTOR_NAMELabel = New System.Windows.Forms.Label
        DISTRIBUTOR_IDLabel = New System.Windows.Forms.Label
        Label10 = New System.Windows.Forms.Label
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpEdit.SuspendLayout()
        Me.pnlEntry.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label8
        '
        Label8.AutoSize = True
        Label8.BackColor = System.Drawing.Color.Transparent
        Label8.Location = New System.Drawing.Point(409, 123)
        Label8.Name = "Label8"
        Label8.Size = New System.Drawing.Size(27, 13)
        Label8.TabIndex = 18
        Label8.Text = "FAX"
        '
        'Label9
        '
        Label9.AutoSize = True
        Label9.BackColor = System.Drawing.Color.Transparent
        Label9.Location = New System.Drawing.Point(406, 70)
        Label9.Name = "Label9"
        Label9.Size = New System.Drawing.Size(99, 13)
        Label9.TabIndex = 16
        Label9.Text = "CONTACT PHONE"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.BackColor = System.Drawing.Color.Transparent
        Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label6.Location = New System.Drawing.Point(407, 46)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(120, 13)
        Label6.TabIndex = 14
        Label6.Text = "CONTACT PERSON"
        '
        'Label7
        '
        Label7.AutoSize = True
        Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label7.Location = New System.Drawing.Point(37, 187)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(66, 13)
        Label7.TabIndex = 12
        Label7.Text = "ADDRESS"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Location = New System.Drawing.Point(37, 121)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(40, 13)
        Label5.TabIndex = 8
        Label5.Text = "NPWP"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label3.Location = New System.Drawing.Point(407, 95)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(115, 13)
        Label3.TabIndex = 4
        Label3.Text = "CONTACT MOBILE"
        '
        'DISTRIBUTOR_NAMELabel
        '
        DISTRIBUTOR_NAMELabel.AutoSize = True
        DISTRIBUTOR_NAMELabel.BackColor = System.Drawing.Color.Transparent
        DISTRIBUTOR_NAMELabel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DISTRIBUTOR_NAMELabel.Location = New System.Drawing.Point(37, 43)
        DISTRIBUTOR_NAMELabel.Name = "DISTRIBUTOR_NAMELabel"
        DISTRIBUTOR_NAMELabel.Size = New System.Drawing.Size(132, 15)
        DISTRIBUTOR_NAMELabel.TabIndex = 2
        DISTRIBUTOR_NAMELabel.Text = "TRANSPORTER NAME:"
        '
        'DISTRIBUTOR_IDLabel
        '
        DISTRIBUTOR_IDLabel.AutoSize = True
        DISTRIBUTOR_IDLabel.BackColor = System.Drawing.Color.Transparent
        DISTRIBUTOR_IDLabel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DISTRIBUTOR_IDLabel.Location = New System.Drawing.Point(37, 19)
        DISTRIBUTOR_IDLabel.Name = "DISTRIBUTOR_IDLabel"
        DISTRIBUTOR_IDLabel.Size = New System.Drawing.Size(114, 15)
        DISTRIBUTOR_IDLabel.TabIndex = 0
        DISTRIBUTOR_IDLabel.Text = "TRANSPORTER_ID:"
        '
        'Label10
        '
        Label10.AutoSize = True
        Label10.BackColor = System.Drawing.Color.Transparent
        Label10.Location = New System.Drawing.Point(409, 148)
        Label10.Name = "Label10"
        Label10.Size = New System.Drawing.Size(82, 13)
        Label10.TabIndex = 36
        Label10.Text = "POSTAL CODE"
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
        Me.ImageList1.Images.SetKeyName(18, "Brand.ico")
        Me.ImageList1.Images.SetKeyName(19, "Item.ICO")
        Me.ImageList1.Images.SetKeyName(20, "Cancel and Close.ico")
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar2 (Bar2)"
        Me.Bar2.AccessibleName = "Bar2"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaptionText
        Me.Bar2.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaption
        Me.Bar2.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnExport, Me.btnRefresh, Me.btnAddNew, Me.btnSave})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(856, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 24
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
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
        Me.btnColumn.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnShowFieldChooser})
        Me.btnColumn.Text = "Grid Column"
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
            "etting defined by yourseef "
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 14
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "E&xport Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 15
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5)
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.Tooltip = "Reload data and refresh grid"
        '
        'btnAddNew
        '
        Me.btnAddNew.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnAddNew.ImageIndex = 1
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlN)
        Me.btnAddNew.Text = "Add New"
        Me.btnAddNew.Tooltip = "add new item data"
        '
        'btnSave
        '
        Me.btnSave.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSave.ImageIndex = 12
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Text = "Save Changes"
        '
        'grpEdit
        '
        Me.grpEdit.BackColor = System.Drawing.Color.Transparent
        Me.grpEdit.Controls.Add(Me.lblAutoGenerate)
        Me.grpEdit.Controls.Add(Label10)
        Me.grpEdit.Controls.Add(Me.txtPostalCode)
        Me.grpEdit.Controls.Add(Me.txtEmail)
        Me.grpEdit.Controls.Add(Me.Label4)
        Me.grpEdit.Controls.Add(Me.txtResponsiblePerson)
        Me.grpEdit.Controls.Add(Me.Label2)
        Me.grpEdit.Controls.Add(Me.dtPicBirtDate)
        Me.grpEdit.Controls.Add(Me.Label1)
        Me.grpEdit.Controls.Add(Label8)
        Me.grpEdit.Controls.Add(Me.txtContactFax)
        Me.grpEdit.Controls.Add(Label9)
        Me.grpEdit.Controls.Add(Me.txtContactPhone)
        Me.grpEdit.Controls.Add(Label6)
        Me.grpEdit.Controls.Add(Me.txtContactPerson)
        Me.grpEdit.Controls.Add(Label7)
        Me.grpEdit.Controls.Add(Me.txtAddress)
        Me.grpEdit.Controls.Add(Label5)
        Me.grpEdit.Controls.Add(Me.txtNPWP)
        Me.grpEdit.Controls.Add(Label3)
        Me.grpEdit.Controls.Add(Me.txtContactMobile)
        Me.grpEdit.Controls.Add(DISTRIBUTOR_NAMELabel)
        Me.grpEdit.Controls.Add(Me.txtTransporterName)
        Me.grpEdit.Controls.Add(DISTRIBUTOR_IDLabel)
        Me.grpEdit.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpEdit.Image = CType(resources.GetObject("grpEdit.Image"), System.Drawing.Image)
        Me.grpEdit.Location = New System.Drawing.Point(0, 0)
        Me.grpEdit.Name = "grpEdit"
        Me.grpEdit.Size = New System.Drawing.Size(856, 216)
        Me.grpEdit.TabIndex = 25
        Me.grpEdit.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblAutoGenerate
        '
        Me.lblAutoGenerate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAutoGenerate.Location = New System.Drawing.Point(176, 19)
        Me.lblAutoGenerate.Name = "lblAutoGenerate"
        Me.lblAutoGenerate.Size = New System.Drawing.Size(120, 18)
        Me.lblAutoGenerate.TabIndex = 37
        Me.lblAutoGenerate.Text = "<< AutoGenerate >>"
        '
        'txtPostalCode
        '
        Me.txtPostalCode.Location = New System.Drawing.Point(531, 145)
        Me.txtPostalCode.MaxLength = 5
        Me.txtPostalCode.Name = "txtPostalCode"
        Me.txtPostalCode.Size = New System.Drawing.Size(100, 20)
        Me.txtPostalCode.TabIndex = 10
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(176, 147)
        Me.txtEmail.MaxLength = 100
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(208, 20)
        Me.txtEmail.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(37, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "EMAIL"
        '
        'txtResponsiblePerson
        '
        Me.txtResponsiblePerson.Location = New System.Drawing.Point(176, 66)
        Me.txtResponsiblePerson.MaxLength = 100
        Me.txtResponsiblePerson.Name = "txtResponsiblePerson"
        Me.txtResponsiblePerson.Size = New System.Drawing.Size(208, 20)
        Me.txtResponsiblePerson.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(37, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(133, 13)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "RESPONSIBLE_PERSON"
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
        Me.dtPicBirtDate.Location = New System.Drawing.Point(176, 94)
        Me.dtPicBirtDate.Name = "dtPicBirtDate"
        Me.dtPicBirtDate.ShowTodayButton = False
        Me.dtPicBirtDate.Size = New System.Drawing.Size(208, 20)
        Me.dtPicBirtDate.TabIndex = 2
        Me.dtPicBirtDate.Value = New Date(2014, 2, 10, 0, 0, 0, 0)
        Me.dtPicBirtDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(37, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "BIRTH DATE"
        '
        'txtContactFax
        '
        Me.txtContactFax.Location = New System.Drawing.Point(531, 119)
        Me.txtContactFax.MaxLength = 20
        Me.txtContactFax.Name = "txtContactFax"
        Me.txtContactFax.Size = New System.Drawing.Size(276, 20)
        Me.txtContactFax.TabIndex = 9
        '
        'txtContactPhone
        '
        Me.txtContactPhone.Location = New System.Drawing.Point(531, 70)
        Me.txtContactPhone.MaxLength = 20
        Me.txtContactPhone.Name = "txtContactPhone"
        Me.txtContactPhone.Size = New System.Drawing.Size(276, 20)
        Me.txtContactPhone.TabIndex = 7
        '
        'txtContactPerson
        '
        Me.txtContactPerson.Location = New System.Drawing.Point(531, 44)
        Me.txtContactPerson.MaxLength = 100
        Me.txtContactPerson.Name = "txtContactPerson"
        Me.txtContactPerson.Size = New System.Drawing.Size(276, 20)
        Me.txtContactPerson.TabIndex = 6
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(174, 174)
        Me.txtAddress.MaxLength = 150
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(633, 35)
        Me.txtAddress.TabIndex = 5
        '
        'txtNPWP
        '
        Me.txtNPWP.Location = New System.Drawing.Point(176, 121)
        Me.txtNPWP.MaxLength = 100
        Me.txtNPWP.Name = "txtNPWP"
        Me.txtNPWP.Size = New System.Drawing.Size(208, 20)
        Me.txtNPWP.TabIndex = 3
        '
        'txtContactMobile
        '
        Me.txtContactMobile.Location = New System.Drawing.Point(531, 94)
        Me.txtContactMobile.MaxLength = 20
        Me.txtContactMobile.Name = "txtContactMobile"
        Me.txtContactMobile.Size = New System.Drawing.Size(276, 20)
        Me.txtContactMobile.TabIndex = 8
        '
        'txtTransporterName
        '
        Me.txtTransporterName.Location = New System.Drawing.Point(176, 40)
        Me.txtTransporterName.MaxLength = 100
        Me.txtTransporterName.Name = "txtTransporterName"
        Me.txtTransporterName.Size = New System.Drawing.Size(208, 20)
        Me.txtTransporterName.TabIndex = 0
        '
        'pnlEntry
        '
        Me.pnlEntry.Controls.Add(Me.pnlMain)
        Me.pnlEntry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEntry.Location = New System.Drawing.Point(0, 25)
        Me.pnlEntry.Name = "pnlEntry"
        Me.pnlEntry.Size = New System.Drawing.Size(856, 455)
        Me.pnlEntry.TabIndex = 26
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlGrid)
        Me.pnlMain.Controls.Add(Me.grpEdit)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(856, 455)
        Me.pnlMain.TabIndex = 26
        '
        'pnlGrid
        '
        Me.pnlGrid.Controls.Add(Me.GridEX1)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 216)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Size = New System.Drawing.Size(856, 239)
        Me.pnlGrid.TabIndex = 27
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
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 0)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.RowFormatStyle.ForeColor = System.Drawing.SystemColors.InfoText
        Me.GridEX1.Size = New System.Drawing.Size(856, 239)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.IncludeFormatStyle = False
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
        'Transporter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(856, 480)
        Me.Controls.Add(Me.pnlEntry)
        Me.Controls.Add(Me.Bar2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Transporter"
        Me.Text = "Transporter"
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpEdit.ResumeLayout(False)
        Me.grpEdit.PerformLayout()
        Me.pnlEntry.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnSave As DevComponents.DotNetBar.ButtonItem
    Private WithEvents grpEdit As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents txtResponsiblePerson As System.Windows.Forms.TextBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents dtPicBirtDate As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents txtContactFax As System.Windows.Forms.TextBox
    Private WithEvents txtContactPhone As System.Windows.Forms.TextBox
    Private WithEvents txtContactPerson As System.Windows.Forms.TextBox
    Private WithEvents txtAddress As System.Windows.Forms.TextBox
    Private WithEvents txtNPWP As System.Windows.Forms.TextBox
    Private WithEvents txtContactMobile As System.Windows.Forms.TextBox
    Private WithEvents txtTransporterName As System.Windows.Forms.TextBox
    Private WithEvents txtEmail As System.Windows.Forms.TextBox
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents pnlEntry As System.Windows.Forms.Panel
    Private WithEvents pnlGrid As System.Windows.Forms.Panel
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents lblAutoGenerate As System.Windows.Forms.Label
    Private WithEvents txtPostalCode As System.Windows.Forms.TextBox
End Class
