<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SPPBEntry
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SPPBEntry))
        Dim grdGoneSequence_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbTransporter_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbOA_REF_NO_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.XpGradientPanel2 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.btnAddNew = New Janus.Windows.EditControls.UIButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnCLose = New Janus.Windows.EditControls.UIButton
        Me.btnSave = New Janus.Windows.EditControls.UIButton
        Me.grdGoneSequence = New Janus.Windows.GridEX.GridEX
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.grpGon = New Janus.Windows.EditControls.UIGroupBox
        Me.btnFilterTransporter = New DTSProjects.ButtonSearch
        Me.grpTransporter = New Janus.Windows.EditControls.UIGroupBox
        Me.mcbTransporter = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.txtGon_No = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmbGonSequence = New Janus.Windows.EditControls.UIComboBox
        Me.dtPicGonDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.grpSPPB = New Janus.Windows.EditControls.UIGroupBox
        Me.btnChekExisting = New Janus.Windows.EditControls.UIButton
        Me.dtPicSPPBDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtSPPBNo = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.grpPO = New Janus.Windows.EditControls.UIGroupBox
        Me.grpReleaseSPPB = New Janus.Windows.EditControls.UIGroupBox
        Me.dtPicRelease = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.btnFilterOAREfNo = New DTSProjects.ButtonSearch
        Me.btnFilterDistributor = New DTSProjects.ButtonSearch
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.grpDetail = New Janus.Windows.EditControls.UIGroupBox
        Me.txtPODate = New System.Windows.Forms.TextBox
        Me.txtPoREfNo = New System.Windows.Forms.TextBox
        Me.lbOARefNo = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.mcbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.mcbOA_REF_NO = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.XpGradientPanel2.SuspendLayout()
        CType(Me.grdGoneSequence, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XpGradientPanel1.SuspendLayout()
        CType(Me.grpGon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGon.SuspendLayout()
        CType(Me.grpTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTransporter.SuspendLayout()
        CType(Me.mcbTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpSPPB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSPPB.SuspendLayout()
        CType(Me.grpPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPO.SuspendLayout()
        CType(Me.grpReleaseSPPB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReleaseSPPB.SuspendLayout()
        CType(Me.grpDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDetail.SuspendLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbOA_REF_NO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'XpGradientPanel2
        '
        Me.XpGradientPanel2.Controls.Add(Me.btnAddNew)
        Me.XpGradientPanel2.Controls.Add(Me.btnCLose)
        Me.XpGradientPanel2.Controls.Add(Me.btnSave)
        Me.XpGradientPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.XpGradientPanel2.Location = New System.Drawing.Point(0, 447)
        Me.XpGradientPanel2.Name = "XpGradientPanel2"
        Me.XpGradientPanel2.Size = New System.Drawing.Size(964, 35)
        Me.XpGradientPanel2.TabIndex = 4
        '
        'btnAddNew
        '
        Me.btnAddNew.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAddNew.ImageIndex = 0
        Me.btnAddNew.ImageList = Me.ImageList1
        Me.btnAddNew.Location = New System.Drawing.Point(7, 7)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(75, 22)
        Me.btnAddNew.TabIndex = 0
        Me.btnAddNew.Text = "Add &New"
        Me.btnAddNew.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
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
        Me.ImageList1.Images.SetKeyName(10, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(11, "Cancel and Close.ico")
        Me.ImageList1.Images.SetKeyName(12, "Search.png")
        '
        'btnCLose
        '
        Me.btnCLose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnCLose.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnCLose.ImageIndex = 11
        Me.btnCLose.ImageList = Me.ImageList1
        Me.btnCLose.Location = New System.Drawing.Point(891, 6)
        Me.btnCLose.Name = "btnCLose"
        Me.btnCLose.Size = New System.Drawing.Size(70, 22)
        Me.btnCLose.TabIndex = 2
        Me.btnCLose.Text = "&Cancel"
        Me.btnCLose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnSave.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnSave.ImageIndex = 10
        Me.btnSave.ImageList = Me.ImageList1
        Me.btnSave.Location = New System.Drawing.Point(784, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(101, 22)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "&Save Changes"
        Me.btnSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'grdGoneSequence
        '
        Me.grdGoneSequence.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdGoneSequence.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdGoneSequence_DesignTimeLayout.LayoutString = resources.GetString("grdGoneSequence_DesignTimeLayout.LayoutString")
        Me.grdGoneSequence.DesignTimeLayout = grdGoneSequence_DesignTimeLayout
        Me.grdGoneSequence.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdGoneSequence.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.grdGoneSequence.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdGoneSequence.GroupByBoxVisible = False
        Me.grdGoneSequence.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdGoneSequence.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdGoneSequence.ImageList = Me.ImageList1
        Me.grdGoneSequence.Location = New System.Drawing.Point(0, 197)
        Me.grdGoneSequence.Name = "grdGoneSequence"
        Me.grdGoneSequence.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdGoneSequence.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdGoneSequence.RecordNavigator = True
        Me.grdGoneSequence.Size = New System.Drawing.Size(964, 250)
        Me.grdGoneSequence.TabIndex = 0
        Me.grdGoneSequence.UpdateOnLeave = False
        Me.grdGoneSequence.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdGoneSequence.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.grpGon)
        Me.XpGradientPanel1.Controls.Add(Me.grpSPPB)
        Me.XpGradientPanel1.Controls.Add(Me.grpPO)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpGradientPanel1.EndColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.XpGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(964, 197)
        Me.XpGradientPanel1.StartColor = System.Drawing.SystemColors.InactiveCaption
        Me.XpGradientPanel1.TabIndex = 0
        Me.XpGradientPanel1.Watermark = CType(resources.GetObject("XpGradientPanel1.Watermark"), System.Drawing.Image)
        '
        'grpGon
        '
        Me.grpGon.BackColor = System.Drawing.Color.Transparent
        Me.grpGon.Controls.Add(Me.btnFilterTransporter)
        Me.grpGon.Controls.Add(Me.grpTransporter)
        Me.grpGon.Controls.Add(Me.txtGon_No)
        Me.grpGon.Controls.Add(Me.Label9)
        Me.grpGon.Controls.Add(Me.cmbGonSequence)
        Me.grpGon.Controls.Add(Me.dtPicGonDate)
        Me.grpGon.Controls.Add(Me.Label5)
        Me.grpGon.Controls.Add(Me.Label6)
        Me.grpGon.Location = New System.Drawing.Point(334, 112)
        Me.grpGon.Name = "grpGon"
        Me.grpGon.Size = New System.Drawing.Size(627, 79)
        Me.grpGon.TabIndex = 15
        Me.grpGon.Text = "GON SEQUENCE"
        Me.grpGon.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnFilterTransporter
        '
        Me.btnFilterTransporter.Location = New System.Drawing.Point(603, 38)
        Me.btnFilterTransporter.Name = "btnFilterTransporter"
        Me.btnFilterTransporter.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterTransporter.TabIndex = 12
        '
        'grpTransporter
        '
        Me.grpTransporter.Controls.Add(Me.mcbTransporter)
        Me.grpTransporter.Location = New System.Drawing.Point(269, 11)
        Me.grpTransporter.Name = "grpTransporter"
        Me.grpTransporter.Size = New System.Drawing.Size(330, 49)
        Me.grpTransporter.TabIndex = 11
        Me.grpTransporter.Text = "Transporter"
        Me.grpTransporter.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'mcbTransporter
        '
        Me.mcbTransporter.AutoComplete = False
        Me.mcbTransporter.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbTransporter_DesignTimeLayout.LayoutString = resources.GetString("mcbTransporter_DesignTimeLayout.LayoutString")
        Me.mcbTransporter.DesignTimeLayout = mcbTransporter_DesignTimeLayout
        Me.mcbTransporter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.mcbTransporter.Location = New System.Drawing.Point(3, 26)
        Me.mcbTransporter.MaxLength = 50
        Me.mcbTransporter.Name = "mcbTransporter"
        Me.mcbTransporter.SelectedIndex = -1
        Me.mcbTransporter.SelectedItem = Nothing
        Me.mcbTransporter.Size = New System.Drawing.Size(324, 20)
        Me.mcbTransporter.TabIndex = 5
        Me.mcbTransporter.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtGon_No
        '
        Me.txtGon_No.Location = New System.Drawing.Point(167, 13)
        Me.txtGon_No.Name = "txtGon_No"
        Me.txtGon_No.Size = New System.Drawing.Size(96, 20)
        Me.txtGon_No.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(115, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 13)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "GON NO"
        '
        'cmbGonSequence
        '
        Me.cmbGonSequence.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbGonSequence.HoverMode = Janus.Windows.EditControls.HoverMode.Highlight
        Me.cmbGonSequence.Location = New System.Drawing.Point(78, 15)
        Me.cmbGonSequence.Name = "cmbGonSequence"
        Me.cmbGonSequence.Size = New System.Drawing.Size(34, 20)
        Me.cmbGonSequence.StateStyles.FormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbGonSequence.TabIndex = 0
        Me.cmbGonSequence.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'dtPicGonDate
        '
        Me.dtPicGonDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicGonDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicGonDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicGonDate.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicGonDate.DropDownCalendar.Name = ""
        Me.dtPicGonDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicGonDate.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicGonDate.Location = New System.Drawing.Point(78, 40)
        Me.dtPicGonDate.Name = "dtPicGonDate"
        Me.dtPicGonDate.ShowTodayButton = False
        Me.dtPicGonDate.Size = New System.Drawing.Size(185, 20)
        Me.dtPicGonDate.TabIndex = 1
        Me.dtPicGonDate.Value = New Date(2013, 12, 22, 0, 0, 0, 0)
        Me.dtPicGonDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 14)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "GON_DATE"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 14)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "GS_NO"
        '
        'grpSPPB
        '
        Me.grpSPPB.BackColor = System.Drawing.Color.Transparent
        Me.grpSPPB.Controls.Add(Me.btnChekExisting)
        Me.grpSPPB.Controls.Add(Me.dtPicSPPBDate)
        Me.grpSPPB.Controls.Add(Me.Label8)
        Me.grpSPPB.Controls.Add(Me.txtSPPBNo)
        Me.grpSPPB.Controls.Add(Me.Label7)
        Me.grpSPPB.Location = New System.Drawing.Point(334, 8)
        Me.grpSPPB.Name = "grpSPPB"
        Me.grpSPPB.Size = New System.Drawing.Size(627, 100)
        Me.grpSPPB.TabIndex = 14
        Me.grpSPPB.Text = "SPPB"
        Me.grpSPPB.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnChekExisting
        '
        Me.btnChekExisting.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnChekExisting.ImageIndex = 12
        Me.btnChekExisting.ImageList = Me.ImageList1
        Me.btnChekExisting.Location = New System.Drawing.Point(343, 70)
        Me.btnChekExisting.Name = "btnChekExisting"
        Me.btnChekExisting.Size = New System.Drawing.Size(130, 22)
        Me.btnChekExisting.TabIndex = 2
        Me.btnChekExisting.Text = "Check Existing SPPB"
        Me.btnChekExisting.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'dtPicSPPBDate
        '
        Me.dtPicSPPBDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicSPPBDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicSPPBDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicSPPBDate.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicSPPBDate.DropDownCalendar.Name = ""
        Me.dtPicSPPBDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicSPPBDate.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicSPPBDate.Location = New System.Drawing.Point(288, 41)
        Me.dtPicSPPBDate.Name = "dtPicSPPBDate"
        Me.dtPicSPPBDate.ShowTodayButton = False
        Me.dtPicSPPBDate.Size = New System.Drawing.Size(185, 20)
        Me.dtPicSPPBDate.TabIndex = 1
        Me.dtPicSPPBDate.Value = New Date(2013, 12, 22, 0, 0, 0, 0)
        Me.dtPicSPPBDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(285, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "SPPB_DATE"
        '
        'txtSPPBNo
        '
        Me.txtSPPBNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSPPBNo.Location = New System.Drawing.Point(6, 40)
        Me.txtSPPBNo.MaxLength = 15
        Me.txtSPPBNo.Name = "txtSPPBNo"
        Me.txtSPPBNo.Size = New System.Drawing.Size(257, 20)
        Me.txtSPPBNo.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "SPPB_NO"
        '
        'grpPO
        '
        Me.grpPO.BackColor = System.Drawing.Color.Transparent
        Me.grpPO.Controls.Add(Me.grpReleaseSPPB)
        Me.grpPO.Controls.Add(Me.btnFilterOAREfNo)
        Me.grpPO.Controls.Add(Me.btnFilterDistributor)
        Me.grpPO.Controls.Add(Me.Label1)
        Me.grpPO.Controls.Add(Me.Label2)
        Me.grpPO.Controls.Add(Me.grpDetail)
        Me.grpPO.Controls.Add(Me.mcbDistributor)
        Me.grpPO.Controls.Add(Me.mcbOA_REF_NO)
        Me.grpPO.Location = New System.Drawing.Point(7, 8)
        Me.grpPO.Name = "grpPO"
        Me.grpPO.Size = New System.Drawing.Size(321, 183)
        Me.grpPO.TabIndex = 11
        Me.grpPO.Text = "PO"
        Me.grpPO.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grpReleaseSPPB
        '
        Me.grpReleaseSPPB.Controls.Add(Me.dtPicRelease)
        Me.grpReleaseSPPB.Location = New System.Drawing.Point(10, 127)
        Me.grpReleaseSPPB.Name = "grpReleaseSPPB"
        Me.grpReleaseSPPB.Size = New System.Drawing.Size(303, 49)
        Me.grpReleaseSPPB.TabIndex = 5
        Me.grpReleaseSPPB.Text = "Release SPPB"
        Me.grpReleaseSPPB.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'dtPicRelease
        '
        Me.dtPicRelease.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicRelease.Checked = False
        Me.dtPicRelease.CustomFormat = "dd MMMM yyyy"
        Me.dtPicRelease.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicRelease.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicRelease.DropDownCalendar.Name = ""
        Me.dtPicRelease.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicRelease.Location = New System.Drawing.Point(8, 20)
        Me.dtPicRelease.Name = "dtPicRelease"
        Me.dtPicRelease.ShowCheckBox = True
        Me.dtPicRelease.Size = New System.Drawing.Size(274, 20)
        Me.dtPicRelease.TabIndex = 0
        Me.dtPicRelease.Value = New Date(2013, 12, 22, 0, 0, 0, 0)
        Me.dtPicRelease.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'btnFilterOAREfNo
        '
        Me.btnFilterOAREfNo.Location = New System.Drawing.Point(297, 35)
        Me.btnFilterOAREfNo.Name = "btnFilterOAREfNo"
        Me.btnFilterOAREfNo.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterOAREfNo.TabIndex = 4
        '
        'btnFilterDistributor
        '
        Me.btnFilterDistributor.Location = New System.Drawing.Point(296, 13)
        Me.btnFilterDistributor.Name = "btnFilterDistributor"
        Me.btnFilterDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterDistributor.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "DISTRIBUTOR"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "OA_REF_NO"
        '
        'grpDetail
        '
        Me.grpDetail.BackColor = System.Drawing.Color.Transparent
        Me.grpDetail.Controls.Add(Me.txtPODate)
        Me.grpDetail.Controls.Add(Me.txtPoREfNo)
        Me.grpDetail.Controls.Add(Me.lbOARefNo)
        Me.grpDetail.Controls.Add(Me.Label3)
        Me.grpDetail.Location = New System.Drawing.Point(10, 58)
        Me.grpDetail.Name = "grpDetail"
        Me.grpDetail.Size = New System.Drawing.Size(303, 63)
        Me.grpDetail.TabIndex = 2
        Me.grpDetail.Text = "INFO PO "
        Me.grpDetail.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtPODate
        '
        Me.txtPODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPODate.Location = New System.Drawing.Point(84, 37)
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.ReadOnly = True
        Me.txtPODate.Size = New System.Drawing.Size(198, 20)
        Me.txtPODate.TabIndex = 3
        '
        'txtPoREfNo
        '
        Me.txtPoREfNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPoREfNo.Location = New System.Drawing.Point(84, 11)
        Me.txtPoREfNo.Name = "txtPoREfNo"
        Me.txtPoREfNo.ReadOnly = True
        Me.txtPoREfNo.Size = New System.Drawing.Size(198, 20)
        Me.txtPoREfNo.TabIndex = 2
        '
        'lbOARefNo
        '
        Me.lbOARefNo.AutoSize = True
        Me.lbOARefNo.Location = New System.Drawing.Point(5, 39)
        Me.lbOARefNo.Name = "lbOARefNo"
        Me.lbOARefNo.Size = New System.Drawing.Size(57, 13)
        Me.lbOARefNo.TabIndex = 1
        Me.lbOARefNo.Text = "PO_DATE"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "PO_REF_NO"
        '
        'mcbDistributor
        '
        Me.mcbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbDistributor_DesignTimeLayout.LayoutString = resources.GetString("mcbDistributor_DesignTimeLayout.LayoutString")
        Me.mcbDistributor.DesignTimeLayout = mcbDistributor_DesignTimeLayout
        Me.mcbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.mcbDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbDistributor.Location = New System.Drawing.Point(94, 11)
        Me.mcbDistributor.Name = "mcbDistributor"
        Me.mcbDistributor.SelectedIndex = -1
        Me.mcbDistributor.SelectedItem = Nothing
        Me.mcbDistributor.Size = New System.Drawing.Size(198, 20)
        Me.mcbDistributor.TabIndex = 0
        Me.mcbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.mcbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'mcbOA_REF_NO
        '
        Me.mcbOA_REF_NO.AutoComplete = False
        Me.mcbOA_REF_NO.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbOA_REF_NO_DesignTimeLayout.LayoutString = resources.GetString("mcbOA_REF_NO_DesignTimeLayout.LayoutString")
        Me.mcbOA_REF_NO.DesignTimeLayout = mcbOA_REF_NO_DesignTimeLayout
        Me.mcbOA_REF_NO.DisplayMember = "OA_REF_NO"
        Me.mcbOA_REF_NO.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbOA_REF_NO.Location = New System.Drawing.Point(94, 35)
        Me.mcbOA_REF_NO.Name = "mcbOA_REF_NO"
        Me.mcbOA_REF_NO.SelectedIndex = -1
        Me.mcbOA_REF_NO.SelectedItem = Nothing
        Me.mcbOA_REF_NO.Size = New System.Drawing.Size(198, 20)
        Me.mcbOA_REF_NO.TabIndex = 1
        Me.mcbOA_REF_NO.ValueMember = "OA_REF_NO"
        Me.mcbOA_REF_NO.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'SPPBEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(964, 482)
        Me.Controls.Add(Me.grdGoneSequence)
        Me.Controls.Add(Me.XpGradientPanel2)
        Me.Controls.Add(Me.XpGradientPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SPPBEntry"
        Me.Text = "Entry Data SPPB"
        Me.XpGradientPanel2.ResumeLayout(False)
        CType(Me.grdGoneSequence, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XpGradientPanel1.ResumeLayout(False)
        CType(Me.grpGon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGon.ResumeLayout(False)
        Me.grpGon.PerformLayout()
        CType(Me.grpTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTransporter.ResumeLayout(False)
        Me.grpTransporter.PerformLayout()
        CType(Me.mcbTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpSPPB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSPPB.ResumeLayout(False)
        Me.grpSPPB.PerformLayout()
        CType(Me.grpPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPO.ResumeLayout(False)
        Me.grpPO.PerformLayout()
        CType(Me.grpReleaseSPPB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpReleaseSPPB.ResumeLayout(False)
        Me.grpReleaseSPPB.PerformLayout()
        CType(Me.grpDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDetail.ResumeLayout(False)
        Me.grpDetail.PerformLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbOA_REF_NO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents grdGoneSequence As Janus.Windows.GridEX.GridEX
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents grpDetail As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents lbOARefNo As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mcbOA_REF_NO As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents mcbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents btnCLose As Janus.Windows.EditControls.UIButton
    Private WithEvents btnSave As Janus.Windows.EditControls.UIButton
    Friend WithEvents txtSPPBNo As System.Windows.Forms.TextBox
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents grpPO As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents dtPicSPPBDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents btnAddNew As Janus.Windows.EditControls.UIButton
    Private WithEvents grpGon As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents dtPicGonDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents cmbGonSequence As Janus.Windows.EditControls.UIComboBox
    Private WithEvents btnChekExisting As Janus.Windows.EditControls.UIButton
    Friend WithEvents txtGon_No As System.Windows.Forms.TextBox
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents XpGradientPanel2 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents txtPODate As System.Windows.Forms.TextBox
    Private WithEvents txtPoREfNo As System.Windows.Forms.TextBox
    Private WithEvents grpSPPB As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnFilterOAREfNo As DTSProjects.ButtonSearch
    Private WithEvents btnFilterDistributor As DTSProjects.ButtonSearch
    Private WithEvents grpReleaseSPPB As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents dtPicRelease As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents grpTransporter As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents mcbTransporter As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents btnFilterTransporter As DTSProjects.ButtonSearch

End Class
