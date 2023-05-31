<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GonNonPODist
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
        Dim mcbCustomer_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GonNonPODist))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX1_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column2.ButtonImage")
        Dim grdGon_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim UiComboBoxItem7 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem8 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem9 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem10 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem11 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem12 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim chkProduct_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbGonArea_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbTransporter_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.grpDetail = New Janus.Windows.EditControls.UIGroupBox
        Me.txtPORefNo = New System.Windows.Forms.TextBox
        Me.dtPicPODate = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblPO_Date = New System.Windows.Forms.Label
        Me.btnFindDistributor = New DTSProjects.ButtonSearch
        Me.mcbCustomer = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.grpSPPBInfo = New Janus.Windows.EditControls.UIGroupBox
        Me.dtPicSPPBDate = New System.Windows.Forms.DateTimePicker
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtSPPBNo = New System.Windows.Forms.TextBox
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtDefShipto = New System.Windows.Forms.TextBox
        Me.grpItemDetail = New Janus.Windows.EditControls.UIGroupBox
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.grpGON = New Janus.Windows.EditControls.UIGroupBox
        Me.grpGonDetail = New Janus.Windows.EditControls.UIGroupBox
        Me.grdGon = New Janus.Windows.GridEX.GridEX
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.btnPrint = New Janus.Windows.EditControls.UIButton
        Me.btnInsert = New Janus.Windows.EditControls.UIButton
        Me.btnAddNew = New Janus.Windows.EditControls.UIButton
        Me.btnClose = New Janus.Windows.EditControls.UIButton
        Me.grpGonEntry = New Janus.Windows.EditControls.UIGroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtGonShipto = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtCustomerAddress = New System.Windows.Forms.TextBox
        Me.lblAddress = New System.Windows.Forms.Label
        Me.ChkShiptoCustomer = New System.Windows.Forms.CheckBox
        Me.txtDriverTrans = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtPolice_no_Trans = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdWarhouse = New Janus.Windows.EditControls.UIComboBox
        Me.lblWarhouse = New System.Windows.Forms.Label
        Me.chkProduct = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnFindGonArea = New DTSProjects.ButtonSearch
        Me.BtnFindTransporter = New DTSProjects.ButtonSearch
        Me.Label11 = New System.Windows.Forms.Label
        Me.mcbGonArea = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.mcbTransporter = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.txtGONNO = New System.Windows.Forms.TextBox
        Me.dtPicGONDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblCustomerName = New System.Windows.Forms.Label
        Me.txtCustomerName = New System.Windows.Forms.TextBox
        CType(Me.grpDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDetail.SuspendLayout()
        CType(Me.mcbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpSPPBInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSPPBInfo.SuspendLayout()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.grpItemDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpItemDetail.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpGON, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGON.SuspendLayout()
        CType(Me.grpGonDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGonDetail.SuspendLayout()
        CType(Me.grdGon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        CType(Me.grpGonEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGonEntry.SuspendLayout()
        CType(Me.mcbGonArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbTransporter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpDetail
        '
        Me.grpDetail.BackColor = System.Drawing.Color.Transparent
        Me.grpDetail.Controls.Add(Me.txtPORefNo)
        Me.grpDetail.Controls.Add(Me.dtPicPODate)
        Me.grpDetail.Controls.Add(Me.Label1)
        Me.grpDetail.Controls.Add(Me.lblPO_Date)
        Me.grpDetail.Location = New System.Drawing.Point(6, 16)
        Me.grpDetail.Name = "grpDetail"
        Me.grpDetail.Size = New System.Drawing.Size(336, 65)
        Me.grpDetail.TabIndex = 2
        Me.grpDetail.Text = "PO INFO "
        Me.grpDetail.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtPORefNo
        '
        Me.txtPORefNo.Location = New System.Drawing.Point(112, 12)
        Me.txtPORefNo.MaxLength = 50
        Me.txtPORefNo.Name = "txtPORefNo"
        Me.txtPORefNo.Size = New System.Drawing.Size(208, 20)
        Me.txtPORefNo.TabIndex = 14
        '
        'dtPicPODate
        '
        Me.dtPicPODate.Location = New System.Drawing.Point(112, 38)
        Me.dtPicPODate.Name = "dtPicPODate"
        Me.dtPicPODate.Size = New System.Drawing.Size(208, 20)
        Me.dtPicPODate.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "PO_DATE"
        '
        'lblPO_Date
        '
        Me.lblPO_Date.AutoSize = True
        Me.lblPO_Date.Location = New System.Drawing.Point(6, 17)
        Me.lblPO_Date.Name = "lblPO_Date"
        Me.lblPO_Date.Size = New System.Drawing.Size(75, 13)
        Me.lblPO_Date.TabIndex = 1
        Me.lblPO_Date.Text = "PO_NUMBER"
        '
        'btnFindDistributor
        '
        Me.btnFindDistributor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFindDistributor.Location = New System.Drawing.Point(715, 20)
        Me.btnFindDistributor.Name = "btnFindDistributor"
        Me.btnFindDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnFindDistributor.TabIndex = 15
        '
        'mcbCustomer
        '
        mcbCustomer_DesignTimeLayout.LayoutString = resources.GetString("mcbCustomer_DesignTimeLayout.LayoutString")
        Me.mcbCustomer.DesignTimeLayout = mcbCustomer_DesignTimeLayout
        Me.mcbCustomer.DisplayMember = "DISTRIBUTOR_NAME"
        Me.mcbCustomer.Location = New System.Drawing.Point(138, 19)
        Me.mcbCustomer.Name = "mcbCustomer"
        Me.mcbCustomer.SelectedIndex = -1
        Me.mcbCustomer.SelectedItem = Nothing
        Me.mcbCustomer.Size = New System.Drawing.Size(575, 20)
        Me.mcbCustomer.TabIndex = 13
        Me.mcbCustomer.ValueMember = "DISTRIBUTOR_ID"
        '
        'grpSPPBInfo
        '
        Me.grpSPPBInfo.Controls.Add(Me.dtPicSPPBDate)
        Me.grpSPPBInfo.Controls.Add(Me.Label9)
        Me.grpSPPBInfo.Controls.Add(Me.Label10)
        Me.grpSPPBInfo.Controls.Add(Me.txtSPPBNo)
        Me.grpSPPBInfo.Location = New System.Drawing.Point(6, 87)
        Me.grpSPPBInfo.Name = "grpSPPBInfo"
        Me.grpSPPBInfo.Size = New System.Drawing.Size(336, 66)
        Me.grpSPPBInfo.TabIndex = 3
        Me.grpSPPBInfo.Text = "SPPB Info"
        Me.grpSPPBInfo.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'dtPicSPPBDate
        '
        Me.dtPicSPPBDate.Location = New System.Drawing.Point(112, 38)
        Me.dtPicSPPBDate.Name = "dtPicSPPBDate"
        Me.dtPicSPPBDate.Size = New System.Drawing.Size(208, 20)
        Me.dtPicSPPBDate.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "SPPB_DATE"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(57, 13)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "SPPB_NO"
        '
        'txtSPPBNo
        '
        Me.txtSPPBNo.Location = New System.Drawing.Point(112, 12)
        Me.txtSPPBNo.MaxLength = 50
        Me.txtSPPBNo.Name = "txtSPPBNo"
        Me.txtSPPBNo.Size = New System.Drawing.Size(208, 20)
        Me.txtSPPBNo.TabIndex = 0
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UiGroupBox1.Controls.Add(Me.Label7)
        Me.UiGroupBox1.Controls.Add(Me.txtDefShipto)
        Me.UiGroupBox1.Controls.Add(Me.grpItemDetail)
        Me.UiGroupBox1.Controls.Add(Me.grpSPPBInfo)
        Me.UiGroupBox1.Controls.Add(Me.grpDetail)
        Me.UiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UiGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(780, 215)
        Me.UiGroupBox1.TabIndex = 4
        Me.UiGroupBox1.Text = "PO  SPPB HEADER"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 162)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "SPPB Ship TO"
        '
        'txtDefShipto
        '
        Me.txtDefShipto.Location = New System.Drawing.Point(6, 178)
        Me.txtDefShipto.MaxLength = 250
        Me.txtDefShipto.Multiline = True
        Me.txtDefShipto.Name = "txtDefShipto"
        Me.txtDefShipto.Size = New System.Drawing.Size(770, 31)
        Me.txtDefShipto.TabIndex = 6
        '
        'grpItemDetail
        '
        Me.grpItemDetail.Controls.Add(Me.GridEX1)
        Me.grpItemDetail.Location = New System.Drawing.Point(348, 12)
        Me.grpItemDetail.Name = "grpItemDetail"
        Me.grpItemDetail.Size = New System.Drawing.Size(430, 163)
        Me.grpItemDetail.TabIndex = 5
        Me.grpItemDetail.Text = "Item Detail"
        Me.grpItemDetail.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX1_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("GridEX1_DesignTimeLayout_Reference_0.Instance"), Object)
        GridEX1_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {GridEX1_DesignTimeLayout_Reference_0})
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(3, 16)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.Size = New System.Drawing.Size(424, 144)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Add.bmp")
        Me.ImageList1.Images.SetKeyName(1, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(2, "Close.ico")
        Me.ImageList1.Images.SetKeyName(3, "printer.ico")
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ExpandableSplitter1.ExpandableControl = Me.UiGroupBox1
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
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(0, 215)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(780, 5)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 16
        Me.ExpandableSplitter1.TabStop = False
        '
        'grpGON
        '
        Me.grpGON.Controls.Add(Me.grpGonDetail)
        Me.grpGON.Controls.Add(Me.grpGonEntry)
        Me.grpGON.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpGON.Location = New System.Drawing.Point(0, 220)
        Me.grpGON.Name = "grpGON"
        Me.grpGON.Size = New System.Drawing.Size(780, 411)
        Me.grpGON.TabIndex = 19
        Me.grpGON.Text = "G O N"
        Me.grpGON.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grpGonDetail
        '
        Me.grpGonDetail.Controls.Add(Me.grdGon)
        Me.grpGonDetail.Controls.Add(Me.PanelEx1)
        Me.grpGonDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpGonDetail.Location = New System.Drawing.Point(3, 230)
        Me.grpGonDetail.Name = "grpGonDetail"
        Me.grpGonDetail.Size = New System.Drawing.Size(774, 178)
        Me.grpGonDetail.TabIndex = 17
        Me.grpGonDetail.Text = "GON Detail"
        Me.grpGonDetail.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grdGon
        '
        Me.grdGon.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdGon.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdGon.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdGon_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdGon.DesignTimeLayout = grdGon_DesignTimeLayout
        Me.grdGon.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdGon.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdGon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdGon.GroupByBoxVisible = False
        Me.grdGon.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdGon.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdGon.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdGon.Location = New System.Drawing.Point(3, 16)
        Me.grdGon.Name = "grdGon"
        Me.grdGon.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdGon.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdGon.SelectedInactiveFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.grdGon.Size = New System.Drawing.Size(768, 125)
        Me.grdGon.TabIndex = 0
        Me.grdGon.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdGon.WatermarkImage.Image = CType(resources.GetObject("grdGon.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdGon.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.btnPrint)
        Me.PanelEx1.Controls.Add(Me.btnInsert)
        Me.PanelEx1.Controls.Add(Me.btnAddNew)
        Me.PanelEx1.Controls.Add(Me.btnClose)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx1.Location = New System.Drawing.Point(3, 141)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(768, 34)
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
        Me.PanelEx1.TabIndex = 4
        '
        'btnPrint
        '
        Me.btnPrint.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnPrint.ImageIndex = 3
        Me.btnPrint.ImageList = Me.ImageList1
        Me.btnPrint.Location = New System.Drawing.Point(108, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(123, 23)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "&Print Preview GON"
        Me.btnPrint.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnInsert
        '
        Me.btnInsert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInsert.ImageIndex = 1
        Me.btnInsert.ImageList = Me.ImageList1
        Me.btnInsert.Location = New System.Drawing.Point(582, 4)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(81, 27)
        Me.btnInsert.TabIndex = 1
        Me.btnInsert.Text = "&Save GON"
        Me.btnInsert.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnAddNew
        '
        Me.btnAddNew.ImageIndex = 0
        Me.btnAddNew.ImageList = Me.ImageList1
        Me.btnAddNew.Location = New System.Drawing.Point(3, 5)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(97, 25)
        Me.btnAddNew.TabIndex = 0
        Me.btnAddNew.Text = "&Add new Gon"
        Me.btnAddNew.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.ImageIndex = 2
        Me.btnClose.ImageList = Me.ImageList1
        Me.btnClose.Location = New System.Drawing.Point(684, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(81, 27)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "&Close"
        Me.btnClose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'grpGonEntry
        '
        Me.grpGonEntry.Controls.Add(Me.Label8)
        Me.grpGonEntry.Controls.Add(Me.txtGonShipto)
        Me.grpGonEntry.Controls.Add(Me.Label4)
        Me.grpGonEntry.Controls.Add(Me.btnFindDistributor)
        Me.grpGonEntry.Controls.Add(Me.mcbCustomer)
        Me.grpGonEntry.Controls.Add(Me.txtCustomerAddress)
        Me.grpGonEntry.Controls.Add(Me.lblAddress)
        Me.grpGonEntry.Controls.Add(Me.ChkShiptoCustomer)
        Me.grpGonEntry.Controls.Add(Me.txtDriverTrans)
        Me.grpGonEntry.Controls.Add(Me.Label3)
        Me.grpGonEntry.Controls.Add(Me.txtPolice_no_Trans)
        Me.grpGonEntry.Controls.Add(Me.Label2)
        Me.grpGonEntry.Controls.Add(Me.cmdWarhouse)
        Me.grpGonEntry.Controls.Add(Me.lblWarhouse)
        Me.grpGonEntry.Controls.Add(Me.chkProduct)
        Me.grpGonEntry.Controls.Add(Me.Label12)
        Me.grpGonEntry.Controls.Add(Me.btnFindGonArea)
        Me.grpGonEntry.Controls.Add(Me.BtnFindTransporter)
        Me.grpGonEntry.Controls.Add(Me.Label11)
        Me.grpGonEntry.Controls.Add(Me.mcbGonArea)
        Me.grpGonEntry.Controls.Add(Me.mcbTransporter)
        Me.grpGonEntry.Controls.Add(Me.txtGONNO)
        Me.grpGonEntry.Controls.Add(Me.dtPicGONDate)
        Me.grpGonEntry.Controls.Add(Me.Label6)
        Me.grpGonEntry.Controls.Add(Me.Label5)
        Me.grpGonEntry.Controls.Add(Me.lblCustomerName)
        Me.grpGonEntry.Controls.Add(Me.txtCustomerName)
        Me.grpGonEntry.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpGonEntry.Location = New System.Drawing.Point(3, 16)
        Me.grpGonEntry.Name = "grpGonEntry"
        Me.grpGonEntry.Size = New System.Drawing.Size(774, 214)
        Me.grpGonEntry.TabIndex = 16
        Me.grpGonEntry.Text = "GON Header"
        Me.grpGonEntry.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 156)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 13)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "GON Ship TO"
        '
        'txtGonShipto
        '
        Me.txtGonShipto.Location = New System.Drawing.Point(13, 174)
        Me.txtGonShipto.MaxLength = 250
        Me.txtGonShipto.Multiline = True
        Me.txtGonShipto.Name = "txtGonShipto"
        Me.txtGonShipto.Size = New System.Drawing.Size(723, 35)
        Me.txtGonShipto.TabIndex = 38
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "GON NO"
        '
        'txtCustomerAddress
        '
        Me.txtCustomerAddress.Location = New System.Drawing.Point(352, 19)
        Me.txtCustomerAddress.MaxLength = 150
        Me.txtCustomerAddress.Name = "txtCustomerAddress"
        Me.txtCustomerAddress.Size = New System.Drawing.Size(384, 20)
        Me.txtCustomerAddress.TabIndex = 34
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblAddress.Location = New System.Drawing.Point(301, 23)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(45, 13)
        Me.lblAddress.TabIndex = 35
        Me.lblAddress.Text = "Address"
        Me.lblAddress.Visible = False
        '
        'ChkShiptoCustomer
        '
        Me.ChkShiptoCustomer.AutoSize = True
        Me.ChkShiptoCustomer.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkShiptoCustomer.Checked = True
        Me.ChkShiptoCustomer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkShiptoCustomer.Location = New System.Drawing.Point(11, 21)
        Me.ChkShiptoCustomer.Name = "ChkShiptoCustomer"
        Me.ChkShiptoCustomer.Size = New System.Drawing.Size(113, 17)
        Me.ChkShiptoCustomer.TabIndex = 33
        Me.ChkShiptoCustomer.Text = "Ship To Distributor"
        Me.ChkShiptoCustomer.UseVisualStyleBackColor = True
        '
        'txtDriverTrans
        '
        Me.txtDriverTrans.Location = New System.Drawing.Point(466, 128)
        Me.txtDriverTrans.Name = "txtDriverTrans"
        Me.txtDriverTrans.Size = New System.Drawing.Size(270, 20)
        Me.txtDriverTrans.TabIndex = 31
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(360, 132)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Driver Trans"
        '
        'txtPolice_no_Trans
        '
        Me.txtPolice_no_Trans.Location = New System.Drawing.Point(466, 103)
        Me.txtPolice_no_Trans.Name = "txtPolice_no_Trans"
        Me.txtPolice_no_Trans.Size = New System.Drawing.Size(270, 20)
        Me.txtPolice_no_Trans.TabIndex = 29
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(360, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "POLICE_NO_Trans"
        '
        'cmdWarhouse
        '
        Me.cmdWarhouse.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        UiComboBoxItem7.FormatStyle.Alpha = 0
        UiComboBoxItem7.IsSeparator = False
        UiComboBoxItem7.Text = "---Select---"
        UiComboBoxItem7.Value = "UnSelect"
        UiComboBoxItem8.FormatStyle.Alpha = 0
        UiComboBoxItem8.IsSeparator = False
        UiComboBoxItem8.Text = "JAKARTA"
        UiComboBoxItem8.Value = "JKT"
        UiComboBoxItem9.FormatStyle.Alpha = 0
        UiComboBoxItem9.IsSeparator = False
        UiComboBoxItem9.Text = "MERAK"
        UiComboBoxItem9.Value = "MRK"
        UiComboBoxItem10.FormatStyle.Alpha = 0
        UiComboBoxItem10.IsSeparator = False
        UiComboBoxItem10.Text = "SURABAYA"
        UiComboBoxItem10.Value = "SBY"
        UiComboBoxItem11.FormatStyle.Alpha = 0
        UiComboBoxItem11.IsSeparator = False
        UiComboBoxItem11.Text = "TANGERANG"
        UiComboBoxItem11.Value = "TGR"
        UiComboBoxItem12.FormatStyle.Alpha = 0
        UiComboBoxItem12.IsSeparator = False
        UiComboBoxItem12.Text = "SERANG"
        UiComboBoxItem12.Value = "SRG"
        Me.cmdWarhouse.Items.AddRange(New Janus.Windows.EditControls.UIComboBoxItem() {UiComboBoxItem7, UiComboBoxItem8, UiComboBoxItem9, UiComboBoxItem10, UiComboBoxItem11, UiComboBoxItem12})
        Me.cmdWarhouse.ItemsFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmdWarhouse.Location = New System.Drawing.Point(466, 76)
        Me.cmdWarhouse.Name = "cmdWarhouse"
        Me.cmdWarhouse.Size = New System.Drawing.Size(270, 20)
        Me.cmdWarhouse.TabIndex = 28
        Me.cmdWarhouse.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'lblWarhouse
        '
        Me.lblWarhouse.AutoSize = True
        Me.lblWarhouse.Location = New System.Drawing.Point(360, 79)
        Me.lblWarhouse.Name = "lblWarhouse"
        Me.lblWarhouse.Size = New System.Drawing.Size(71, 13)
        Me.lblWarhouse.TabIndex = 27
        Me.lblWarhouse.Text = "WARHOUSE"
        '
        'chkProduct
        '
        Me.chkProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.chkProduct.CausesValidation = False
        chkProduct_DesignTimeLayout.LayoutString = resources.GetString("chkProduct_DesignTimeLayout.LayoutString")
        Me.chkProduct.DesignTimeLayout = chkProduct_DesignTimeLayout
        Me.chkProduct.DropDownDisplayMember = "BRANDPACK_NAME"
        Me.chkProduct.DropDownValueMember = "BRANDPACK_ID"
        Me.chkProduct.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.chkProduct.Location = New System.Drawing.Point(466, 47)
        Me.chkProduct.Name = "chkProduct"
        Me.chkProduct.SaveSettings = False
        Me.chkProduct.Size = New System.Drawing.Size(270, 20)
        Me.chkProduct.TabIndex = 4
        Me.chkProduct.ValuesDataMember = Nothing
        Me.chkProduct.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(360, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 13)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "GON Product"
        '
        'btnFindGonArea
        '
        Me.btnFindGonArea.Location = New System.Drawing.Point(329, 104)
        Me.btnFindGonArea.Name = "btnFindGonArea"
        Me.btnFindGonArea.Size = New System.Drawing.Size(17, 18)
        Me.btnFindGonArea.TabIndex = 7
        '
        'BtnFindTransporter
        '
        Me.BtnFindTransporter.Location = New System.Drawing.Point(329, 131)
        Me.BtnFindTransporter.Name = "BtnFindTransporter"
        Me.BtnFindTransporter.Size = New System.Drawing.Size(17, 18)
        Me.BtnFindTransporter.TabIndex = 6
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 104)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 13)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "GON_AREA"
        '
        'mcbGonArea
        '
        Me.mcbGonArea.AutoComplete = False
        Me.mcbGonArea.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbGonArea_DesignTimeLayout.LayoutString = resources.GetString("mcbGonArea_DesignTimeLayout.LayoutString")
        Me.mcbGonArea.DesignTimeLayout = mcbGonArea_DesignTimeLayout
        Me.mcbGonArea.DisplayMember = "AREA"
        Me.mcbGonArea.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbGonArea.Location = New System.Drawing.Point(118, 101)
        Me.mcbGonArea.Name = "mcbGonArea"
        Me.mcbGonArea.SelectedIndex = -1
        Me.mcbGonArea.SelectedItem = Nothing
        Me.mcbGonArea.Size = New System.Drawing.Size(205, 20)
        Me.mcbGonArea.TabIndex = 3
        Me.mcbGonArea.ValueMember = "GON_ID_AREA"
        Me.mcbGonArea.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'mcbTransporter
        '
        Me.mcbTransporter.AutoComplete = False
        Me.mcbTransporter.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbTransporter_DesignTimeLayout.LayoutString = resources.GetString("mcbTransporter_DesignTimeLayout.LayoutString")
        Me.mcbTransporter.DesignTimeLayout = mcbTransporter_DesignTimeLayout
        Me.mcbTransporter.DisplayMember = "TRANSPORTER_NAME"
        Me.mcbTransporter.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbTransporter.Location = New System.Drawing.Point(118, 128)
        Me.mcbTransporter.Name = "mcbTransporter"
        Me.mcbTransporter.SelectedIndex = -1
        Me.mcbTransporter.SelectedItem = Nothing
        Me.mcbTransporter.Size = New System.Drawing.Size(205, 20)
        Me.mcbTransporter.TabIndex = 2
        Me.mcbTransporter.ValueMember = "GT_ID"
        Me.mcbTransporter.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtGONNO
        '
        Me.txtGONNO.Location = New System.Drawing.Point(118, 47)
        Me.txtGONNO.MaxLength = 50
        Me.txtGONNO.Name = "txtGONNO"
        Me.txtGONNO.Size = New System.Drawing.Size(205, 20)
        Me.txtGONNO.TabIndex = 0
        '
        'dtPicGONDate
        '
        Me.dtPicGONDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicGONDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicGONDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicGONDate.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicGONDate.DropDownCalendar.FirstMonth = New Date(2022, 12, 1, 0, 0, 0, 0)
        Me.dtPicGONDate.DropDownCalendar.Name = ""
        Me.dtPicGONDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicGONDate.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicGONDate.Location = New System.Drawing.Point(118, 74)
        Me.dtPicGONDate.Name = "dtPicGONDate"
        Me.dtPicGONDate.ShowTodayButton = False
        Me.dtPicGONDate.Size = New System.Drawing.Size(205, 20)
        Me.dtPicGONDate.TabIndex = 1
        Me.dtPicGONDate.Value = New Date(2023, 1, 23, 0, 0, 0, 0)
        Me.dtPicGONDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 131)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "TRANSPORTER"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "GON_DATE"
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = True
        Me.lblCustomerName.Location = New System.Drawing.Point(135, 22)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(59, 13)
        Me.lblCustomerName.TabIndex = 2
        Me.lblCustomerName.Text = "Cust Name"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.Location = New System.Drawing.Point(197, 19)
        Me.txtCustomerName.MaxLength = 50
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(102, 20)
        Me.txtCustomerName.TabIndex = 37
        '
        'GonNonPODist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(780, 631)
        Me.Controls.Add(Me.grpGON)
        Me.Controls.Add(Me.ExpandableSplitter1)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Name = "GonNonPODist"
        CType(Me.grpDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDetail.ResumeLayout(False)
        Me.grpDetail.PerformLayout()
        CType(Me.mcbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpSPPBInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSPPBInfo.ResumeLayout(False)
        Me.grpSPPBInfo.PerformLayout()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.UiGroupBox1.PerformLayout()
        CType(Me.grpItemDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpItemDetail.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpGON, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGON.ResumeLayout(False)
        CType(Me.grpGonDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGonDetail.ResumeLayout(False)
        CType(Me.grdGon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.grpGonEntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGonEntry.ResumeLayout(False)
        Me.grpGonEntry.PerformLayout()
        CType(Me.mcbGonArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbTransporter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpDetail As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents lblPO_Date As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mcbCustomer As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents dtPicPODate As System.Windows.Forms.DateTimePicker
    Friend WithEvents grpSPPBInfo As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents dtPicSPPBDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtSPPBNo As System.Windows.Forms.TextBox
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Friend WithEvents grpGON As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents grpGonDetail As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents grdGon As Janus.Windows.GridEX.GridEX
    Private WithEvents grpGonEntry As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents cmdWarhouse As Janus.Windows.EditControls.UIComboBox
    Private WithEvents lblWarhouse As System.Windows.Forms.Label
    Friend WithEvents chkProduct As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents btnFindGonArea As DTSProjects.ButtonSearch
    Private WithEvents BtnFindTransporter As DTSProjects.ButtonSearch
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents mcbGonArea As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents mcbTransporter As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents txtGONNO As System.Windows.Forms.TextBox
    Friend WithEvents dtPicGONDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblCustomerName As System.Windows.Forms.Label
    Friend WithEvents txtPolice_no_Trans As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDriverTrans As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Public WithEvents btnInsert As Janus.Windows.EditControls.UIButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents btnAddNew As Janus.Windows.EditControls.UIButton
    Public WithEvents btnClose As Janus.Windows.EditControls.UIButton
    Friend WithEvents grpItemDetail As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtPORefNo As System.Windows.Forms.TextBox
    Friend WithEvents ChkShiptoCustomer As System.Windows.Forms.CheckBox
    Friend WithEvents txtCustomerAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents btnPrint As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnFindDistributor As DTSProjects.ButtonSearch
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDefShipto As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtGonShipto As System.Windows.Forms.TextBox

End Class
