<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Distributor_Include
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Distributor_Include))
        Dim mcbTMCPMRT_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbTMPKPP_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbTM_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim ChkDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbBrandPack_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbProgram_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.grpMain = New Janus.Windows.EditControls.UIGroupBox
        Me.txtDescriptions = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.chkCPMRT = New System.Windows.Forms.CheckBox
        Me.chkCPR = New System.Windows.Forms.CheckBox
        Me.chkCP = New System.Windows.Forms.CheckBox
        Me.chkHK = New System.Windows.Forms.CheckBox
        Me.chkRPK = New System.Windows.Forms.CheckBox
        Me.chkDK = New System.Windows.Forms.CheckBox
        Me.chkPKPP = New System.Windows.Forms.CheckBox
        Me.tbType = New DevComponents.DotNetBar.TabControl
        Me.TabControlPanel1 = New DevComponents.DotNetBar.TabControlPanel
        Me.txtTargetPCT = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtTargetQTY = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.txtGiven = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.chkStepping = New Janus.Windows.EditControls.UICheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.tbiRPK = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.TabControlPanel9 = New DevComponents.DotNetBar.TabControlPanel
        Me.mcbTMCPMRT = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.Label24 = New System.Windows.Forms.Label
        Me.chkTargetDistributorCPMRT = New System.Windows.Forms.CheckBox
        Me.txtTargetCPMRT = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtGivenCPMRT = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.tbCPMRT = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.TabControlPanel5 = New DevComponents.DotNetBar.TabControlPanel
        Me.tbChildPKPP = New DevComponents.DotNetBar.TabControl
        Me.TabControlPanel7 = New DevComponents.DotNetBar.TabControlPanel
        Me.chkTargetDistributorPKPP = New System.Windows.Forms.CheckBox
        Me.mcbTMPKPP = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtTargetPKPP = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtGivenPKPP = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.tbValume = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.TabControlPanel8 = New DevComponents.DotNetBar.TabControlPanel
        Me.txtTargetValuePKPP = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtBonusValuePKPP = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.tbValue = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.tbiPKPP = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.TabControlPanel2 = New DevComponents.DotNetBar.TabControlPanel
        Me.mcbTM = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.Label21 = New System.Windows.Forms.Label
        Me.chkTargetDistributor = New System.Windows.Forms.CheckBox
        Me.chkSpesialDiscountCPD = New System.Windows.Forms.CheckBox
        Me.txtTargetCP = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtGivenCP = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.tbiCP = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.TabControlPanel6 = New DevComponents.DotNetBar.TabControlPanel
        Me.txtTargetCPR = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtGivenCPR = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.tbiCPR = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.TabControlPanel3 = New DevComponents.DotNetBar.TabControlPanel
        Me.txtTargetDK = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtGivenDK = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.tbiDK = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.TabControlPanel4 = New DevComponents.DotNetBar.TabControlPanel
        Me.txtPrice = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtTargetHK = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.tbiHK = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.mcbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.ChkDistributor = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.btnFilterDistributor = New DTSProjects.ButtonSearch
        Me.btnFilterBrandPack = New DTSProjects.ButtonSearch
        Me.btnFilterProgram = New DTSProjects.ButtonSearch
        Me.btnChekExisting = New Janus.Windows.EditControls.UIButton
        Me.mcbBrandPack = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.mcbProgram = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.dtPicEnd = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicStart = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.ButtonEntry1 = New DTSProjects.ButtonEntry
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        CType(Me.grpMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMain.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        CType(Me.tbType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbType.SuspendLayout()
        Me.TabControlPanel1.SuspendLayout()
        Me.TabControlPanel9.SuspendLayout()
        CType(Me.mcbTMCPMRT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlPanel5.SuspendLayout()
        CType(Me.tbChildPKPP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbChildPKPP.SuspendLayout()
        Me.TabControlPanel7.SuspendLayout()
        CType(Me.mcbTMPKPP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlPanel8.SuspendLayout()
        Me.TabControlPanel2.SuspendLayout()
        CType(Me.mcbTM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlPanel6.SuspendLayout()
        Me.TabControlPanel3.SuspendLayout()
        Me.TabControlPanel4.SuspendLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbProgram, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpMain
        '
        Me.grpMain.Controls.Add(Me.txtDescriptions)
        Me.grpMain.Controls.Add(Me.Label23)
        Me.grpMain.Controls.Add(Me.PanelEx1)
        Me.grpMain.Controls.Add(Me.tbType)
        Me.grpMain.Controls.Add(Me.mcbDistributor)
        Me.grpMain.Controls.Add(Me.ChkDistributor)
        Me.grpMain.Controls.Add(Me.btnFilterDistributor)
        Me.grpMain.Controls.Add(Me.btnFilterBrandPack)
        Me.grpMain.Controls.Add(Me.btnFilterProgram)
        Me.grpMain.Controls.Add(Me.btnChekExisting)
        Me.grpMain.Controls.Add(Me.mcbBrandPack)
        Me.grpMain.Controls.Add(Me.mcbProgram)
        Me.grpMain.Controls.Add(Me.dtPicEnd)
        Me.grpMain.Controls.Add(Me.dtPicStart)
        Me.grpMain.Controls.Add(Me.Label1)
        Me.grpMain.Controls.Add(Me.Label5)
        Me.grpMain.Controls.Add(Me.ButtonEntry1)
        Me.grpMain.Controls.Add(Me.Label4)
        Me.grpMain.Controls.Add(Me.Label3)
        Me.grpMain.Controls.Add(Me.Label2)
        Me.grpMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpMain.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpMain.Location = New System.Drawing.Point(0, 0)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(507, 392)
        Me.grpMain.TabIndex = 1
        Me.grpMain.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtDescriptions
        '
        Me.txtDescriptions.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtDescriptions.Location = New System.Drawing.Point(15, 148)
        Me.txtDescriptions.MaxLength = 200
        Me.txtDescriptions.Multiline = True
        Me.txtDescriptions.Name = "txtDescriptions"
        Me.txtDescriptions.Size = New System.Drawing.Size(482, 46)
        Me.txtDescriptions.TabIndex = 39
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(14, 130)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(79, 15)
        Me.Label23.TabIndex = 38
        Me.Label23.Text = "Descriptions"
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.Controls.Add(Me.chkCPMRT)
        Me.PanelEx1.Controls.Add(Me.chkCPR)
        Me.PanelEx1.Controls.Add(Me.chkCP)
        Me.PanelEx1.Controls.Add(Me.chkHK)
        Me.PanelEx1.Controls.Add(Me.chkRPK)
        Me.PanelEx1.Controls.Add(Me.chkDK)
        Me.PanelEx1.Controls.Add(Me.chkPKPP)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx1.Location = New System.Drawing.Point(3, 215)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(501, 28)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.Color = System.Drawing.SystemColors.MenuBar
        Me.PanelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 37
        '
        'chkCPMRT
        '
        Me.chkCPMRT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCPMRT.Location = New System.Drawing.Point(272, 4)
        Me.chkCPMRT.Name = "chkCPMRT"
        Me.chkCPMRT.Size = New System.Drawing.Size(70, 21)
        Me.chkCPMRT.TabIndex = 6
        Me.chkCPMRT.Text = "CP(RM/T)"
        Me.chkCPMRT.UseVisualStyleBackColor = True
        '
        'chkCPR
        '
        Me.chkCPR.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCPR.Location = New System.Drawing.Point(204, 5)
        Me.chkCPR.Name = "chkCPR"
        Me.chkCPR.Size = New System.Drawing.Size(63, 19)
        Me.chkCPR.TabIndex = 5
        Me.chkCPR.Text = "CP(RD)"
        Me.chkCPR.UseVisualStyleBackColor = True
        '
        'chkCP
        '
        Me.chkCP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCP.Location = New System.Drawing.Point(136, 4)
        Me.chkCP.Name = "chkCP"
        Me.chkCP.Size = New System.Drawing.Size(70, 21)
        Me.chkCP.TabIndex = 4
        Me.chkCP.Text = "CP(D)"
        Me.chkCP.UseVisualStyleBackColor = True
        '
        'chkHK
        '
        Me.chkHK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHK.Location = New System.Drawing.Point(407, 4)
        Me.chkHK.Name = "chkHK"
        Me.chkHK.Size = New System.Drawing.Size(40, 21)
        Me.chkHK.TabIndex = 3
        Me.chkHK.Text = "HK"
        Me.chkHK.UseVisualStyleBackColor = True
        '
        'chkRPK
        '
        Me.chkRPK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRPK.Location = New System.Drawing.Point(12, 4)
        Me.chkRPK.Name = "chkRPK"
        Me.chkRPK.Size = New System.Drawing.Size(59, 21)
        Me.chkRPK.TabIndex = 0
        Me.chkRPK.Text = "DPRD"
        Me.chkRPK.UseVisualStyleBackColor = True
        '
        'chkDK
        '
        Me.chkDK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDK.Location = New System.Drawing.Point(350, 4)
        Me.chkDK.Name = "chkDK"
        Me.chkDK.Size = New System.Drawing.Size(40, 21)
        Me.chkDK.TabIndex = 2
        Me.chkDK.Text = "DK"
        Me.chkDK.UseVisualStyleBackColor = True
        '
        'chkPKPP
        '
        Me.chkPKPP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPKPP.Location = New System.Drawing.Point(73, 4)
        Me.chkPKPP.Name = "chkPKPP"
        Me.chkPKPP.Size = New System.Drawing.Size(51, 21)
        Me.chkPKPP.TabIndex = 1
        Me.chkPKPP.Text = "PKPP"
        Me.chkPKPP.UseVisualStyleBackColor = True
        '
        'tbType
        '
        Me.tbType.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.tbType.CanReorderTabs = True
        Me.tbType.Controls.Add(Me.TabControlPanel1)
        Me.tbType.Controls.Add(Me.TabControlPanel9)
        Me.tbType.Controls.Add(Me.TabControlPanel5)
        Me.tbType.Controls.Add(Me.TabControlPanel2)
        Me.tbType.Controls.Add(Me.TabControlPanel6)
        Me.tbType.Controls.Add(Me.TabControlPanel3)
        Me.tbType.Controls.Add(Me.TabControlPanel4)
        Me.tbType.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tbType.Location = New System.Drawing.Point(3, 243)
        Me.tbType.Name = "tbType"
        Me.tbType.SelectedTabFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tbType.SelectedTabIndex = 0
        Me.tbType.Size = New System.Drawing.Size(501, 106)
        Me.tbType.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document
        Me.tbType.TabIndex = 35
        Me.tbType.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox
        Me.tbType.Tabs.Add(Me.tbiRPK)
        Me.tbType.Tabs.Add(Me.tbiCP)
        Me.tbType.Tabs.Add(Me.tbCPMRT)
        Me.tbType.Tabs.Add(Me.tbiPKPP)
        Me.tbType.Tabs.Add(Me.tbiCPR)
        Me.tbType.Tabs.Add(Me.tbiDK)
        Me.tbType.Tabs.Add(Me.tbiHK)
        Me.tbType.Text = "Type"
        '
        'TabControlPanel1
        '
        Me.TabControlPanel1.Controls.Add(Me.txtTargetPCT)
        Me.TabControlPanel1.Controls.Add(Me.txtTargetQTY)
        Me.TabControlPanel1.Controls.Add(Me.txtGiven)
        Me.TabControlPanel1.Controls.Add(Me.Label8)
        Me.TabControlPanel1.Controls.Add(Me.Label7)
        Me.TabControlPanel1.Controls.Add(Me.chkStepping)
        Me.TabControlPanel1.Controls.Add(Me.Label6)
        Me.TabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel1.Location = New System.Drawing.Point(0, 23)
        Me.TabControlPanel1.Name = "TabControlPanel1"
        Me.TabControlPanel1.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel1.Size = New System.Drawing.Size(501, 83)
        Me.TabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.TabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(199, Byte), Integer))
        Me.TabControlPanel1.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel1.Style.GradientAngle = 90
        Me.TabControlPanel1.TabIndex = 1
        Me.TabControlPanel1.TabItem = Me.tbiRPK
        Me.TabControlPanel1.Visible = False
        '
        'txtTargetPCT
        '
        Me.txtTargetPCT.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetPCT.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetPCT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTargetPCT.Location = New System.Drawing.Point(294, 35)
        Me.txtTargetPCT.MaxLength = 6
        Me.txtTargetPCT.Name = "txtTargetPCT"
        Me.txtTargetPCT.Size = New System.Drawing.Size(98, 20)
        Me.txtTargetPCT.TabIndex = 8
        Me.txtTargetPCT.Text = "0.00"
        Me.txtTargetPCT.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtTargetPCT.Visible = False
        Me.txtTargetPCT.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtTargetQTY
        '
        Me.txtTargetQTY.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetQTY.DecimalDigits = 3
        Me.txtTargetQTY.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetQTY.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTargetQTY.ImageList = Me.ImageList1
        Me.txtTargetQTY.Location = New System.Drawing.Point(86, 60)
        Me.txtTargetQTY.Name = "txtTargetQTY"
        Me.txtTargetQTY.Size = New System.Drawing.Size(119, 20)
        Me.txtTargetQTY.TabIndex = 7
        Me.txtTargetQTY.Text = "1.000"
        Me.txtTargetQTY.Value = New Decimal(New Integer() {1000, 0, 0, 196608})
        Me.txtTargetQTY.Visible = False
        Me.txtTargetQTY.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
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
        Me.ImageList1.Images.SetKeyName(11, "duit.ico")
        Me.ImageList1.Images.SetKeyName(12, "Search.png")
        '
        'txtGiven
        '
        Me.txtGiven.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGiven.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGiven.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGiven.Location = New System.Drawing.Point(86, 34)
        Me.txtGiven.MaxLength = 6
        Me.txtGiven.Name = "txtGiven"
        Me.txtGiven.Size = New System.Drawing.Size(119, 20)
        Me.txtGiven.TabIndex = 6
        Me.txtGiven.Text = "0.00"
        Me.txtGiven.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtGiven.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(224, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 15)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Target %"
        Me.Label8.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(7, 62)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 15)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Target QTY"
        Me.Label7.Visible = False
        '
        'chkStepping
        '
        Me.chkStepping.BackColor = System.Drawing.Color.Transparent
        Me.chkStepping.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStepping.Location = New System.Drawing.Point(294, 60)
        Me.chkStepping.Name = "chkStepping"
        Me.chkStepping.Size = New System.Drawing.Size(67, 22)
        Me.chkStepping.TabIndex = 9
        Me.chkStepping.Text = "Stepping"
        Me.chkStepping.Visible = False
        Me.chkStepping.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(7, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 15)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Given %"
        '
        'tbiRPK
        '
        Me.tbiRPK.AttachedControl = Me.TabControlPanel1
        Me.tbiRPK.Name = "tbiRPK"
        Me.tbiRPK.Text = "DPRD"
        '
        'TabControlPanel9
        '
        Me.TabControlPanel9.Controls.Add(Me.mcbTMCPMRT)
        Me.TabControlPanel9.Controls.Add(Me.Label24)
        Me.TabControlPanel9.Controls.Add(Me.chkTargetDistributorCPMRT)
        Me.TabControlPanel9.Controls.Add(Me.txtTargetCPMRT)
        Me.TabControlPanel9.Controls.Add(Me.txtGivenCPMRT)
        Me.TabControlPanel9.Controls.Add(Me.Label25)
        Me.TabControlPanel9.Controls.Add(Me.Label26)
        Me.TabControlPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel9.Location = New System.Drawing.Point(0, 23)
        Me.TabControlPanel9.Name = "TabControlPanel9"
        Me.TabControlPanel9.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel9.Size = New System.Drawing.Size(501, 83)
        Me.TabControlPanel9.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabControlPanel9.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.TabControlPanel9.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel9.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(199, Byte), Integer))
        Me.TabControlPanel9.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel9.Style.GradientAngle = 90
        Me.TabControlPanel9.TabIndex = 7
        Me.TabControlPanel9.TabItem = Me.tbCPMRT
        '
        'mcbTMCPMRT
        '
        Me.mcbTMCPMRT.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbTMCPMRT_DesignTimeLayout.LayoutString = resources.GetString("mcbTMCPMRT_DesignTimeLayout.LayoutString")
        Me.mcbTMCPMRT.DesignTimeLayout = mcbTMCPMRT_DesignTimeLayout
        Me.mcbTMCPMRT.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbTMCPMRT.DisplayMember = "SHIP_TO_ID"
        Me.mcbTMCPMRT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbTMCPMRT.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbTMCPMRT.Location = New System.Drawing.Point(326, 59)
        Me.mcbTMCPMRT.Name = "mcbTMCPMRT"
        Me.mcbTMCPMRT.SelectedIndex = -1
        Me.mcbTMCPMRT.SelectedItem = Nothing
        Me.mcbTMCPMRT.Size = New System.Drawing.Size(164, 20)
        Me.mcbTMCPMRT.TabIndex = 39
        Me.mcbTMCPMRT.ValueMember = "SHIP_TO_ID"
        Me.mcbTMCPMRT.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(280, 62)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(40, 14)
        Me.Label24.TabIndex = 38
        Me.Label24.Text = "TM_ID"
        '
        'chkTargetDistributorCPMRT
        '
        Me.chkTargetDistributorCPMRT.AutoSize = True
        Me.chkTargetDistributorCPMRT.BackColor = System.Drawing.Color.Transparent
        Me.chkTargetDistributorCPMRT.Location = New System.Drawing.Point(327, 36)
        Me.chkTargetDistributorCPMRT.Name = "chkTargetDistributorCPMRT"
        Me.chkTargetDistributorCPMRT.Size = New System.Drawing.Size(167, 19)
        Me.chkTargetDistributorCPMRT.TabIndex = 37
        Me.chkTargetDistributorCPMRT.Text = "Target To Distributor only"
        Me.chkTargetDistributorCPMRT.UseVisualStyleBackColor = False
        '
        'txtTargetCPMRT
        '
        Me.txtTargetCPMRT.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetCPMRT.DecimalDigits = 3
        Me.txtTargetCPMRT.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetCPMRT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTargetCPMRT.ImageList = Me.ImageList1
        Me.txtTargetCPMRT.Location = New System.Drawing.Point(81, 59)
        Me.txtTargetCPMRT.Name = "txtTargetCPMRT"
        Me.txtTargetCPMRT.Size = New System.Drawing.Size(184, 20)
        Me.txtTargetCPMRT.TabIndex = 34
        Me.txtTargetCPMRT.Text = "0.000"
        Me.txtTargetCPMRT.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtTargetCPMRT.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtGivenCPMRT
        '
        Me.txtGivenCPMRT.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGivenCPMRT.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGivenCPMRT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGivenCPMRT.Location = New System.Drawing.Point(81, 36)
        Me.txtGivenCPMRT.MaxLength = 6
        Me.txtGivenCPMRT.Name = "txtGivenCPMRT"
        Me.txtGivenCPMRT.Size = New System.Drawing.Size(66, 20)
        Me.txtGivenCPMRT.TabIndex = 33
        Me.txtGivenCPMRT.Text = "0.00"
        Me.txtGivenCPMRT.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtGivenCPMRT.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(5, 62)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(69, 15)
        Me.Label25.TabIndex = 36
        Me.Label25.Text = "Target QTY"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(5, 38)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(50, 15)
        Me.Label26.TabIndex = 35
        Me.Label26.Text = "Given %"
        '
        'tbCPMRT
        '
        Me.tbCPMRT.AttachedControl = Me.TabControlPanel9
        Me.tbCPMRT.Name = "tbCPMRT"
        Me.tbCPMRT.Text = "CP(RM/T)"
        '
        'TabControlPanel5
        '
        Me.TabControlPanel5.Controls.Add(Me.tbChildPKPP)
        Me.TabControlPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel5.Location = New System.Drawing.Point(0, 23)
        Me.TabControlPanel5.Name = "TabControlPanel5"
        Me.TabControlPanel5.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel5.Size = New System.Drawing.Size(501, 83)
        Me.TabControlPanel5.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabControlPanel5.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.TabControlPanel5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel5.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(199, Byte), Integer))
        Me.TabControlPanel5.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel5.Style.GradientAngle = 90
        Me.TabControlPanel5.TabIndex = 5
        Me.TabControlPanel5.TabItem = Me.tbiPKPP
        '
        'tbChildPKPP
        '
        Me.tbChildPKPP.CanReorderTabs = True
        Me.tbChildPKPP.Controls.Add(Me.TabControlPanel7)
        Me.tbChildPKPP.Controls.Add(Me.TabControlPanel8)
        Me.tbChildPKPP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbChildPKPP.Location = New System.Drawing.Point(1, 1)
        Me.tbChildPKPP.Name = "tbChildPKPP"
        Me.tbChildPKPP.SelectedTabFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tbChildPKPP.SelectedTabIndex = 1
        Me.tbChildPKPP.Size = New System.Drawing.Size(499, 81)
        Me.tbChildPKPP.TabIndex = 33
        Me.tbChildPKPP.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox
        Me.tbChildPKPP.Tabs.Add(Me.tbValume)
        Me.tbChildPKPP.Tabs.Add(Me.tbValue)
        Me.tbChildPKPP.Text = "TabControl1"
        '
        'TabControlPanel7
        '
        Me.TabControlPanel7.Controls.Add(Me.chkTargetDistributorPKPP)
        Me.TabControlPanel7.Controls.Add(Me.mcbTMPKPP)
        Me.TabControlPanel7.Controls.Add(Me.Label22)
        Me.TabControlPanel7.Controls.Add(Me.txtTargetPKPP)
        Me.TabControlPanel7.Controls.Add(Me.Label15)
        Me.TabControlPanel7.Controls.Add(Me.txtGivenPKPP)
        Me.TabControlPanel7.Controls.Add(Me.Label14)
        Me.TabControlPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel7.Location = New System.Drawing.Point(0, 26)
        Me.TabControlPanel7.Name = "TabControlPanel7"
        Me.TabControlPanel7.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel7.Size = New System.Drawing.Size(499, 55)
        Me.TabControlPanel7.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabControlPanel7.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabControlPanel7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel7.Style.BorderColor.Color = System.Drawing.SystemColors.ControlDark
        Me.TabControlPanel7.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel7.Style.GradientAngle = 90
        Me.TabControlPanel7.TabIndex = 1
        Me.TabControlPanel7.TabItem = Me.tbValume
        '
        'chkTargetDistributorPKPP
        '
        Me.chkTargetDistributorPKPP.AutoSize = True
        Me.chkTargetDistributorPKPP.BackColor = System.Drawing.Color.Transparent
        Me.chkTargetDistributorPKPP.Location = New System.Drawing.Point(241, 3)
        Me.chkTargetDistributorPKPP.Name = "chkTargetDistributorPKPP"
        Me.chkTargetDistributorPKPP.Size = New System.Drawing.Size(167, 19)
        Me.chkTargetDistributorPKPP.TabIndex = 35
        Me.chkTargetDistributorPKPP.Text = "Target To Distributor only"
        Me.chkTargetDistributorPKPP.UseVisualStyleBackColor = False
        '
        'mcbTMPKPP
        '
        Me.mcbTMPKPP.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbTMPKPP_DesignTimeLayout.LayoutString = resources.GetString("mcbTMPKPP_DesignTimeLayout.LayoutString")
        Me.mcbTMPKPP.DesignTimeLayout = mcbTMPKPP_DesignTimeLayout
        Me.mcbTMPKPP.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbTMPKPP.DisplayMember = "SHIP_TO_ID"
        Me.mcbTMPKPP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbTMPKPP.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbTMPKPP.Location = New System.Drawing.Point(239, 26)
        Me.mcbTMPKPP.Name = "mcbTMPKPP"
        Me.mcbTMPKPP.SelectedIndex = -1
        Me.mcbTMPKPP.SelectedItem = Nothing
        Me.mcbTMPKPP.Size = New System.Drawing.Size(245, 20)
        Me.mcbTMPKPP.TabIndex = 34
        Me.mcbTMPKPP.ValueMember = "SHIP_TO_ID"
        Me.mcbTMPKPP.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(193, 29)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(40, 14)
        Me.Label22.TabIndex = 33
        Me.Label22.Text = "TM_ID"
        '
        'txtTargetPKPP
        '
        Me.txtTargetPKPP.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetPKPP.DecimalDigits = 3
        Me.txtTargetPKPP.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetPKPP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTargetPKPP.ImageList = Me.ImageList1
        Me.txtTargetPKPP.Location = New System.Drawing.Point(89, 25)
        Me.txtTargetPKPP.Name = "txtTargetPKPP"
        Me.txtTargetPKPP.Size = New System.Drawing.Size(98, 20)
        Me.txtTargetPKPP.TabIndex = 30
        Me.txtTargetPKPP.Text = "0.000"
        Me.txtTargetPKPP.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtTargetPKPP.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(14, 5)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(50, 15)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "Given %"
        '
        'txtGivenPKPP
        '
        Me.txtGivenPKPP.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGivenPKPP.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGivenPKPP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGivenPKPP.Location = New System.Drawing.Point(11, 24)
        Me.txtGivenPKPP.MaxLength = 6
        Me.txtGivenPKPP.Name = "txtGivenPKPP"
        Me.txtGivenPKPP.Size = New System.Drawing.Size(52, 20)
        Me.txtGivenPKPP.TabIndex = 29
        Me.txtGivenPKPP.Text = "0.00"
        Me.txtGivenPKPP.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtGivenPKPP.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(92, 6)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(69, 15)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "Target QTY"
        '
        'tbValume
        '
        Me.tbValume.AttachedControl = Me.TabControlPanel7
        Me.tbValume.Name = "tbValume"
        Me.tbValume.Text = "Volume"
        '
        'TabControlPanel8
        '
        Me.TabControlPanel8.Controls.Add(Me.txtTargetValuePKPP)
        Me.TabControlPanel8.Controls.Add(Me.Label19)
        Me.TabControlPanel8.Controls.Add(Me.txtBonusValuePKPP)
        Me.TabControlPanel8.Controls.Add(Me.Label20)
        Me.TabControlPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel8.Location = New System.Drawing.Point(0, 26)
        Me.TabControlPanel8.Name = "TabControlPanel8"
        Me.TabControlPanel8.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel8.Size = New System.Drawing.Size(499, 55)
        Me.TabControlPanel8.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabControlPanel8.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabControlPanel8.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel8.Style.BorderColor.Color = System.Drawing.SystemColors.ControlDark
        Me.TabControlPanel8.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel8.Style.GradientAngle = 90
        Me.TabControlPanel8.TabIndex = 2
        Me.TabControlPanel8.TabItem = Me.tbValue
        '
        'txtTargetValuePKPP
        '
        Me.txtTargetValuePKPP.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetValuePKPP.DecimalDigits = 2
        Me.txtTargetValuePKPP.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetValuePKPP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTargetValuePKPP.ImageList = Me.ImageList1
        Me.txtTargetValuePKPP.Location = New System.Drawing.Point(278, 3)
        Me.txtTargetValuePKPP.Name = "txtTargetValuePKPP"
        Me.txtTargetValuePKPP.Size = New System.Drawing.Size(135, 20)
        Me.txtTargetValuePKPP.TabIndex = 34
        Me.txtTargetValuePKPP.Text = "0.00"
        Me.txtTargetValuePKPP.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtTargetValuePKPP.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(33, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(76, 15)
        Me.Label19.TabIndex = 35
        Me.Label19.Text = "Bonus value"
        '
        'txtBonusValuePKPP
        '
        Me.txtBonusValuePKPP.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtBonusValuePKPP.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtBonusValuePKPP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBonusValuePKPP.Location = New System.Drawing.Point(116, 2)
        Me.txtBonusValuePKPP.MaxLength = 100
        Me.txtBonusValuePKPP.Name = "txtBonusValuePKPP"
        Me.txtBonusValuePKPP.Size = New System.Drawing.Size(72, 20)
        Me.txtBonusValuePKPP.TabIndex = 33
        Me.txtBonusValuePKPP.Text = "0.00"
        Me.txtBonusValuePKPP.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtBonusValuePKPP.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(198, 5)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(69, 15)
        Me.Label20.TabIndex = 36
        Me.Label20.Text = "Target QTY"
        '
        'tbValue
        '
        Me.tbValue.AttachedControl = Me.TabControlPanel8
        Me.tbValue.Name = "tbValue"
        Me.tbValue.Text = "Value"
        '
        'tbiPKPP
        '
        Me.tbiPKPP.AttachedControl = Me.TabControlPanel5
        Me.tbiPKPP.Name = "tbiPKPP"
        Me.tbiPKPP.Text = "PKPP"
        '
        'TabControlPanel2
        '
        Me.TabControlPanel2.Controls.Add(Me.mcbTM)
        Me.TabControlPanel2.Controls.Add(Me.Label21)
        Me.TabControlPanel2.Controls.Add(Me.chkTargetDistributor)
        Me.TabControlPanel2.Controls.Add(Me.chkSpesialDiscountCPD)
        Me.TabControlPanel2.Controls.Add(Me.txtTargetCP)
        Me.TabControlPanel2.Controls.Add(Me.txtGivenCP)
        Me.TabControlPanel2.Controls.Add(Me.Label10)
        Me.TabControlPanel2.Controls.Add(Me.Label9)
        Me.TabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel2.Location = New System.Drawing.Point(0, 23)
        Me.TabControlPanel2.Name = "TabControlPanel2"
        Me.TabControlPanel2.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel2.Size = New System.Drawing.Size(501, 83)
        Me.TabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.TabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(199, Byte), Integer))
        Me.TabControlPanel2.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel2.Style.GradientAngle = 90
        Me.TabControlPanel2.TabIndex = 2
        Me.TabControlPanel2.TabItem = Me.tbiCP
        '
        'mcbTM
        '
        Me.mcbTM.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbTM_DesignTimeLayout.LayoutString = resources.GetString("mcbTM_DesignTimeLayout.LayoutString")
        Me.mcbTM.DesignTimeLayout = mcbTM_DesignTimeLayout
        Me.mcbTM.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbTM.DisplayMember = "SHIP_TO_ID"
        Me.mcbTM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbTM.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbTM.Location = New System.Drawing.Point(328, 59)
        Me.mcbTM.Name = "mcbTM"
        Me.mcbTM.SelectedIndex = -1
        Me.mcbTM.SelectedItem = Nothing
        Me.mcbTM.Size = New System.Drawing.Size(164, 20)
        Me.mcbTM.TabIndex = 32
        Me.mcbTM.ValueMember = "SHIP_TO_ID"
        Me.mcbTM.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(284, 61)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(40, 14)
        Me.Label21.TabIndex = 31
        Me.Label21.Text = "TM_ID"
        '
        'chkTargetDistributor
        '
        Me.chkTargetDistributor.AutoSize = True
        Me.chkTargetDistributor.BackColor = System.Drawing.Color.Transparent
        Me.chkTargetDistributor.Location = New System.Drawing.Point(329, 36)
        Me.chkTargetDistributor.Name = "chkTargetDistributor"
        Me.chkTargetDistributor.Size = New System.Drawing.Size(167, 19)
        Me.chkTargetDistributor.TabIndex = 30
        Me.chkTargetDistributor.Text = "Target To Distributor only"
        Me.chkTargetDistributor.UseVisualStyleBackColor = False
        '
        'chkSpesialDiscountCPD
        '
        Me.chkSpesialDiscountCPD.AutoSize = True
        Me.chkSpesialDiscountCPD.BackColor = System.Drawing.Color.Transparent
        Me.chkSpesialDiscountCPD.Location = New System.Drawing.Point(156, 37)
        Me.chkSpesialDiscountCPD.Name = "chkSpesialDiscountCPD"
        Me.chkSpesialDiscountCPD.Size = New System.Drawing.Size(121, 19)
        Me.chkSpesialDiscountCPD.TabIndex = 29
        Me.chkSpesialDiscountCPD.Text = "Spesial Discount"
        Me.chkSpesialDiscountCPD.UseVisualStyleBackColor = False
        '
        'txtTargetCP
        '
        Me.txtTargetCP.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetCP.DecimalDigits = 3
        Me.txtTargetCP.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetCP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTargetCP.ImageList = Me.ImageList1
        Me.txtTargetCP.Location = New System.Drawing.Point(83, 59)
        Me.txtTargetCP.Name = "txtTargetCP"
        Me.txtTargetCP.Size = New System.Drawing.Size(185, 20)
        Me.txtTargetCP.TabIndex = 26
        Me.txtTargetCP.Text = "0.000"
        Me.txtTargetCP.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtTargetCP.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtGivenCP
        '
        Me.txtGivenCP.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGivenCP.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGivenCP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGivenCP.Location = New System.Drawing.Point(83, 36)
        Me.txtGivenCP.MaxLength = 6
        Me.txtGivenCP.Name = "txtGivenCP"
        Me.txtGivenCP.Size = New System.Drawing.Size(66, 20)
        Me.txtGivenCP.TabIndex = 25
        Me.txtGivenCP.Text = "0.00"
        Me.txtGivenCP.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtGivenCP.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(7, 62)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 15)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "Target QTY"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(7, 38)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 15)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "Given %"
        '
        'tbiCP
        '
        Me.tbiCP.AttachedControl = Me.TabControlPanel2
        Me.tbiCP.Name = "tbiCP"
        Me.tbiCP.Text = "CP(D)"
        '
        'TabControlPanel6
        '
        Me.TabControlPanel6.Controls.Add(Me.txtTargetCPR)
        Me.TabControlPanel6.Controls.Add(Me.txtGivenCPR)
        Me.TabControlPanel6.Controls.Add(Me.Label17)
        Me.TabControlPanel6.Controls.Add(Me.Label18)
        Me.TabControlPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel6.Location = New System.Drawing.Point(0, 23)
        Me.TabControlPanel6.Name = "TabControlPanel6"
        Me.TabControlPanel6.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel6.Size = New System.Drawing.Size(501, 83)
        Me.TabControlPanel6.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabControlPanel6.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.TabControlPanel6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel6.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(199, Byte), Integer))
        Me.TabControlPanel6.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel6.Style.GradientAngle = 90
        Me.TabControlPanel6.TabIndex = 6
        Me.TabControlPanel6.TabItem = Me.tbiCPR
        '
        'txtTargetCPR
        '
        Me.txtTargetCPR.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetCPR.DecimalDigits = 3
        Me.txtTargetCPR.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetCPR.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTargetCPR.ImageList = Me.ImageList1
        Me.txtTargetCPR.Location = New System.Drawing.Point(240, 18)
        Me.txtTargetCPR.Name = "txtTargetCPR"
        Me.txtTargetCPR.Size = New System.Drawing.Size(135, 20)
        Me.txtTargetCPR.TabIndex = 30
        Me.txtTargetCPR.Text = "0.000"
        Me.txtTargetCPR.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtTargetCPR.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtGivenCPR
        '
        Me.txtGivenCPR.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGivenCPR.DecimalDigits = 3
        Me.txtGivenCPR.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGivenCPR.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGivenCPR.FormatString = "#,##0.000"
        Me.txtGivenCPR.Location = New System.Drawing.Point(78, 17)
        Me.txtGivenCPR.MaxLength = 7
        Me.txtGivenCPR.Name = "txtGivenCPR"
        Me.txtGivenCPR.Size = New System.Drawing.Size(72, 20)
        Me.txtGivenCPR.TabIndex = 29
        Me.txtGivenCPR.Text = "0.000"
        Me.txtGivenCPR.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtGivenCPR.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(163, 20)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(69, 15)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "Target QTY"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(13, 17)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(50, 15)
        Me.Label18.TabIndex = 31
        Me.Label18.Text = "Given %"
        '
        'tbiCPR
        '
        Me.tbiCPR.AttachedControl = Me.TabControlPanel6
        Me.tbiCPR.Name = "tbiCPR"
        Me.tbiCPR.Text = "CP(R)"
        '
        'TabControlPanel3
        '
        Me.TabControlPanel3.Controls.Add(Me.txtTargetDK)
        Me.TabControlPanel3.Controls.Add(Me.txtGivenDK)
        Me.TabControlPanel3.Controls.Add(Me.Label11)
        Me.TabControlPanel3.Controls.Add(Me.Label12)
        Me.TabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel3.Location = New System.Drawing.Point(0, 23)
        Me.TabControlPanel3.Name = "TabControlPanel3"
        Me.TabControlPanel3.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel3.Size = New System.Drawing.Size(501, 83)
        Me.TabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabControlPanel3.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.TabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel3.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(199, Byte), Integer))
        Me.TabControlPanel3.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel3.Style.GradientAngle = 90
        Me.TabControlPanel3.TabIndex = 3
        Me.TabControlPanel3.TabItem = Me.tbiDK
        '
        'txtTargetDK
        '
        Me.txtTargetDK.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetDK.DecimalDigits = 3
        Me.txtTargetDK.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetDK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTargetDK.ImageList = Me.ImageList1
        Me.txtTargetDK.Location = New System.Drawing.Point(239, 16)
        Me.txtTargetDK.Name = "txtTargetDK"
        Me.txtTargetDK.Size = New System.Drawing.Size(135, 20)
        Me.txtTargetDK.TabIndex = 30
        Me.txtTargetDK.Text = "0.000"
        Me.txtTargetDK.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtTargetDK.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtGivenDK
        '
        Me.txtGivenDK.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGivenDK.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtGivenDK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGivenDK.Location = New System.Drawing.Point(77, 16)
        Me.txtGivenDK.MaxLength = 6
        Me.txtGivenDK.Name = "txtGivenDK"
        Me.txtGivenDK.Size = New System.Drawing.Size(72, 20)
        Me.txtGivenDK.TabIndex = 29
        Me.txtGivenDK.Text = "0.00"
        Me.txtGivenDK.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtGivenDK.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(162, 18)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 15)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "Target QTY"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(12, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 15)
        Me.Label12.TabIndex = 31
        Me.Label12.Text = "Given %"
        '
        'tbiDK
        '
        Me.tbiDK.AttachedControl = Me.TabControlPanel3
        Me.tbiDK.Name = "tbiDK"
        Me.tbiDK.Text = "DK"
        '
        'TabControlPanel4
        '
        Me.TabControlPanel4.Controls.Add(Me.txtPrice)
        Me.TabControlPanel4.Controls.Add(Me.Label16)
        Me.TabControlPanel4.Controls.Add(Me.txtTargetHK)
        Me.TabControlPanel4.Controls.Add(Me.Label13)
        Me.TabControlPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel4.Location = New System.Drawing.Point(0, 23)
        Me.TabControlPanel4.Name = "TabControlPanel4"
        Me.TabControlPanel4.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel4.Size = New System.Drawing.Size(501, 83)
        Me.TabControlPanel4.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabControlPanel4.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.TabControlPanel4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel4.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(199, Byte), Integer))
        Me.TabControlPanel4.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel4.Style.GradientAngle = 90
        Me.TabControlPanel4.TabIndex = 4
        Me.TabControlPanel4.TabItem = Me.tbiHK
        '
        'txtPrice
        '
        Me.txtPrice.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtPrice.Location = New System.Drawing.Point(294, 18)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(145, 21)
        Me.txtPrice.TabIndex = 34
        Me.txtPrice.Text = "0.00"
        Me.txtPrice.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtPrice.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(236, 20)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(37, 15)
        Me.Label16.TabIndex = 33
        Me.Label16.Text = "Price"
        '
        'txtTargetHK
        '
        Me.txtTargetHK.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetHK.DecimalDigits = 3
        Me.txtTargetHK.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.txtTargetHK.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTargetHK.ImageList = Me.ImageList1
        Me.txtTargetHK.Location = New System.Drawing.Point(83, 18)
        Me.txtTargetHK.Name = "txtTargetHK"
        Me.txtTargetHK.Size = New System.Drawing.Size(135, 20)
        Me.txtTargetHK.TabIndex = 30
        Me.txtTargetHK.Text = "0.000"
        Me.txtTargetHK.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtTargetHK.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(6, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(69, 15)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "Target QTY"
        '
        'tbiHK
        '
        Me.tbiHK.AttachedControl = Me.TabControlPanel4
        Me.tbiHK.Name = "tbiHK"
        Me.tbiHK.Text = "HK"
        '
        'mcbDistributor
        '
        Me.mcbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbDistributor_DesignTimeLayout.LayoutString = resources.GetString("mcbDistributor_DesignTimeLayout.LayoutString")
        Me.mcbDistributor.DesignTimeLayout = mcbDistributor_DesignTimeLayout
        Me.mcbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.mcbDistributor.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbDistributor.Location = New System.Drawing.Point(124, 71)
        Me.mcbDistributor.Name = "mcbDistributor"
        Me.mcbDistributor.SelectedIndex = -1
        Me.mcbDistributor.SelectedItem = Nothing
        Me.mcbDistributor.Size = New System.Drawing.Size(345, 20)
        Me.mcbDistributor.TabIndex = 34
        Me.mcbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.mcbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ChkDistributor
        '
        Me.ChkDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        ChkDistributor_DesignTimeLayout.LayoutString = resources.GetString("ChkDistributor_DesignTimeLayout.LayoutString")
        Me.ChkDistributor.DesignTimeLayout = ChkDistributor_DesignTimeLayout
        Me.ChkDistributor.DropDownDisplayMember = "DISTRIBUTOR_NAME"
        Me.ChkDistributor.DropDownValueMember = "DISTRIBUTOR_ID"
        Me.ChkDistributor.Location = New System.Drawing.Point(124, 71)
        Me.ChkDistributor.Name = "ChkDistributor"
        Me.ChkDistributor.SaveSettings = False
        Me.ChkDistributor.Size = New System.Drawing.Size(345, 21)
        Me.ChkDistributor.TabIndex = 33
        Me.ChkDistributor.ValuesDataMember = Nothing
        Me.ChkDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnFilterDistributor
        '
        Me.btnFilterDistributor.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterDistributor.Location = New System.Drawing.Point(475, 71)
        Me.btnFilterDistributor.Name = "btnFilterDistributor"
        Me.btnFilterDistributor.Size = New System.Drawing.Size(21, 20)
        Me.btnFilterDistributor.TabIndex = 32
        '
        'btnFilterBrandPack
        '
        Me.btnFilterBrandPack.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterBrandPack.Location = New System.Drawing.Point(475, 44)
        Me.btnFilterBrandPack.Name = "btnFilterBrandPack"
        Me.btnFilterBrandPack.Size = New System.Drawing.Size(21, 18)
        Me.btnFilterBrandPack.TabIndex = 31
        '
        'btnFilterProgram
        '
        Me.btnFilterProgram.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterProgram.Location = New System.Drawing.Point(475, 15)
        Me.btnFilterProgram.Name = "btnFilterProgram"
        Me.btnFilterProgram.Size = New System.Drawing.Size(22, 23)
        Me.btnFilterProgram.TabIndex = 30
        '
        'btnChekExisting
        '
        Me.btnChekExisting.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChekExisting.ImageIndex = 12
        Me.btnChekExisting.ImageList = Me.ImageList1
        Me.btnChekExisting.Location = New System.Drawing.Point(332, 124)
        Me.btnChekExisting.Name = "btnChekExisting"
        Me.btnChekExisting.Size = New System.Drawing.Size(137, 21)
        Me.btnChekExisting.TabIndex = 3
        Me.btnChekExisting.Text = "Check Existing "
        Me.btnChekExisting.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'mcbBrandPack
        '
        Me.mcbBrandPack.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbBrandPack_DesignTimeLayout.LayoutString = resources.GetString("mcbBrandPack_DesignTimeLayout.LayoutString")
        Me.mcbBrandPack.DesignTimeLayout = mcbBrandPack_DesignTimeLayout
        Me.mcbBrandPack.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbBrandPack.DisplayMember = "BRANDPACK_NAME"
        Me.mcbBrandPack.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbBrandPack.Location = New System.Drawing.Point(124, 42)
        Me.mcbBrandPack.Name = "mcbBrandPack"
        Me.mcbBrandPack.SelectedIndex = -1
        Me.mcbBrandPack.SelectedItem = Nothing
        Me.mcbBrandPack.Size = New System.Drawing.Size(345, 20)
        Me.mcbBrandPack.TabIndex = 1
        Me.mcbBrandPack.ValueMember = "BRANDPACK_ID"
        Me.mcbBrandPack.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'mcbProgram
        '
        Me.mcbProgram.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbProgram_DesignTimeLayout.LayoutString = resources.GetString("mcbProgram_DesignTimeLayout.LayoutString")
        Me.mcbProgram.DesignTimeLayout = mcbProgram_DesignTimeLayout
        Me.mcbProgram.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbProgram.DisplayMember = "PROGRAM_ID"
        Me.mcbProgram.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mcbProgram.Location = New System.Drawing.Point(124, 15)
        Me.mcbProgram.Name = "mcbProgram"
        Me.mcbProgram.SelectedIndex = -1
        Me.mcbProgram.SelectedItem = Nothing
        Me.mcbProgram.Size = New System.Drawing.Size(345, 20)
        Me.mcbProgram.TabIndex = 0
        Me.mcbProgram.ValueMember = "PROGRAM_ID"
        Me.mcbProgram.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'dtPicEnd
        '
        Me.dtPicEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicEnd.CustomFormat = "dd MMMM yyyy"
        Me.dtPicEnd.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        Me.dtPicEnd.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        '
        '
        '
        Me.dtPicEnd.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicEnd.DropDownCalendar.Name = ""
        Me.dtPicEnd.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicEnd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtPicEnd.IsNullDate = True
        Me.dtPicEnd.Location = New System.Drawing.Point(332, 100)
        Me.dtPicEnd.Name = "dtPicEnd"
        Me.dtPicEnd.ShowTodayButton = False
        Me.dtPicEnd.Size = New System.Drawing.Size(137, 20)
        Me.dtPicEnd.TabIndex = 5
        Me.dtPicEnd.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicStart
        '
        Me.dtPicStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicStart.CustomFormat = "dd MMMM yyyy"
        Me.dtPicStart.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        Me.dtPicStart.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        '
        '
        '
        Me.dtPicStart.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicStart.DropDownCalendar.Name = ""
        Me.dtPicStart.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicStart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtPicStart.IsNullDate = True
        Me.dtPicStart.Location = New System.Drawing.Point(124, 100)
        Me.dtPicStart.Name = "dtPicStart"
        Me.dtPicStart.ShowTodayButton = False
        Me.dtPicStart.Size = New System.Drawing.Size(144, 20)
        Me.dtPicStart.TabIndex = 4
        Me.dtPicStart.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(271, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 15)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "End Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 15)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Start Date"
        '
        'ButtonEntry1
        '
        Me.ButtonEntry1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ButtonEntry1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonEntry1.Location = New System.Drawing.Point(3, 349)
        Me.ButtonEntry1.Name = "ButtonEntry1"
        Me.ButtonEntry1.Size = New System.Drawing.Size(501, 40)
        Me.ButtonEntry1.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 15)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Program ID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 15)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Distributor"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "BrandPack Name"
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 392)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(507, 269)
        Me.GridEX1.TabIndex = 0
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.MaskColor = System.Drawing.Color.Transparent
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Distributor_Include
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(507, 661)
        Me.ControlBox = False
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.grpMain)
        Me.Name = "Distributor_Include"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data Entry Sales Program BrandPack for Distributor"
        CType(Me.grpMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.tbType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbType.ResumeLayout(False)
        Me.TabControlPanel1.ResumeLayout(False)
        Me.TabControlPanel1.PerformLayout()
        Me.TabControlPanel9.ResumeLayout(False)
        Me.TabControlPanel9.PerformLayout()
        CType(Me.mcbTMCPMRT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlPanel5.ResumeLayout(False)
        CType(Me.tbChildPKPP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbChildPKPP.ResumeLayout(False)
        Me.TabControlPanel7.ResumeLayout(False)
        Me.TabControlPanel7.PerformLayout()
        CType(Me.mcbTMPKPP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlPanel8.ResumeLayout(False)
        Me.TabControlPanel8.PerformLayout()
        Me.TabControlPanel2.ResumeLayout(False)
        Me.TabControlPanel2.PerformLayout()
        CType(Me.mcbTM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlPanel6.ResumeLayout(False)
        Me.TabControlPanel6.PerformLayout()
        Me.TabControlPanel3.ResumeLayout(False)
        Me.TabControlPanel3.PerformLayout()
        Me.TabControlPanel4.ResumeLayout(False)
        Me.TabControlPanel4.PerformLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbProgram, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpMain As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ButtonEntry1 As DTSProjects.ButtonEntry
    Friend WithEvents dtPicEnd As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicStart As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents btnChekExisting As Janus.Windows.EditControls.UIButton
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents mcbBrandPack As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents mcbProgram As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents chkStepping As Janus.Windows.EditControls.UICheckBox
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTargetPCT As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtGiven As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents btnFilterDistributor As DTSProjects.ButtonSearch
    Friend WithEvents btnFilterBrandPack As DTSProjects.ButtonSearch
    Friend WithEvents btnFilterProgram As DTSProjects.ButtonSearch
    Friend WithEvents ChkDistributor As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents mcbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents TabControlPanel3 As DevComponents.DotNetBar.TabControlPanel
    Private WithEvents tbiDK As DevComponents.DotNetBar.TabItem
    Friend WithEvents TabControlPanel1 As DevComponents.DotNetBar.TabControlPanel
    Private WithEvents tbiRPK As DevComponents.DotNetBar.TabItem
    Friend WithEvents TabControlPanel2 As DevComponents.DotNetBar.TabControlPanel
    Private WithEvents tbiCP As DevComponents.DotNetBar.TabItem
    Friend WithEvents TabControlPanel4 As DevComponents.DotNetBar.TabControlPanel
    Private WithEvents tbiHK As DevComponents.DotNetBar.TabItem
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtGivenCP As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtTargetCP As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chkPKPP As System.Windows.Forms.CheckBox
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtTargetDK As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtGivenDK As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtTargetQTY As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtTargetHK As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents chkCP As System.Windows.Forms.CheckBox
    Friend WithEvents TabControlPanel5 As DevComponents.DotNetBar.TabControlPanel
    Friend WithEvents txtTargetPKPP As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtGivenPKPP As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents tbiPKPP As DevComponents.DotNetBar.TabItem
    Friend WithEvents txtPrice As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkRPK As System.Windows.Forms.CheckBox
    Friend WithEvents chkHK As System.Windows.Forms.CheckBox
    Friend WithEvents chkDK As System.Windows.Forms.CheckBox
    Friend WithEvents tbType As DevComponents.DotNetBar.TabControl
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents chkCPR As System.Windows.Forms.CheckBox
    Friend WithEvents TabControlPanel6 As DevComponents.DotNetBar.TabControlPanel
    Friend WithEvents tbiCPR As DevComponents.DotNetBar.TabItem
    Friend WithEvents txtTargetCPR As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtGivenCPR As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TabControlPanel7 As DevComponents.DotNetBar.TabControlPanel
    Private WithEvents tbValume As DevComponents.DotNetBar.TabItem
    Private WithEvents tbChildPKPP As DevComponents.DotNetBar.TabControl
    Friend WithEvents TabControlPanel8 As DevComponents.DotNetBar.TabControlPanel
    Private WithEvents tbValue As DevComponents.DotNetBar.TabItem
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtBonusValuePKPP As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtTargetValuePKPP As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents chkSpesialDiscountCPD As System.Windows.Forms.CheckBox
    Friend WithEvents mcbTM As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents chkTargetDistributor As System.Windows.Forms.CheckBox
    Friend WithEvents mcbTMPKPP As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtDescriptions As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents chkTargetDistributorPKPP As System.Windows.Forms.CheckBox
    Friend WithEvents chkCPMRT As System.Windows.Forms.CheckBox
    Friend WithEvents TabControlPanel9 As DevComponents.DotNetBar.TabControlPanel
    Friend WithEvents tbCPMRT As DevComponents.DotNetBar.TabItem
    Friend WithEvents mcbTMCPMRT As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents chkTargetDistributorCPMRT As System.Windows.Forms.CheckBox
    Friend WithEvents txtTargetCPMRT As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents txtGivenCPMRT As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label

End Class
