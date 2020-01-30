<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSendingData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSendingData))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.XpGradientPanel2 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.ProgresBar1 = New Janus.Windows.EditControls.UIProgressBar
        Me.lblResult = New System.Windows.Forms.Label
        Me.btnCancel = New Janus.Windows.EditControls.UIButton
        Me.btnOK = New Janus.Windows.EditControls.UIButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.btnViewResult = New Janus.Windows.EditControls.UIButton
        Me.txtSearch = New WatermarkTextBox.WaterMarkTextBox
        Me.chkCheckListAll = New System.Windows.Forms.CheckBox
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.stvSendData = New SteepValley.Windows.Forms.ThemedControls.XPTaskBox
        Me.rdbRegional = New System.Windows.Forms.RadioButton
        Me.rdbTerritory = New System.Windows.Forms.RadioButton
        Me.rdbPriviledgeFS = New System.Windows.Forms.RadioButton
        Me.rdbPriviledgeTM = New System.Windows.Forms.RadioButton
        Me.rdbPriviledgePM = New System.Windows.Forms.RadioButton
        Me.rdbPriviledgeRSM = New System.Windows.Forms.RadioButton
        Me.rdbDistributor = New System.Windows.Forms.RadioButton
        Me.rdbAgreement = New System.Windows.Forms.RadioButton
        Me.rdbBrandPack = New System.Windows.Forms.RadioButton
        Me.rdbPack = New System.Windows.Forms.RadioButton
        Me.rdbBrand = New System.Windows.Forms.RadioButton
        Me.stvSendPersons = New SteepValley.Windows.Forms.ThemedControls.XPTaskBox
        Me.rdbFS = New System.Windows.Forms.RadioButton
        Me.rdbAdmin = New System.Windows.Forms.RadioButton
        Me.rdbPM = New System.Windows.Forms.RadioButton
        Me.rdbRSM = New System.Windows.Forms.RadioButton
        Me.rdbTM = New System.Windows.Forms.RadioButton
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.XpGradientPanel2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.XpGradientPanel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.stvSendData.SuspendLayout()
        Me.stvSendPersons.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(794, 589)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.XpGradientPanel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(5, 555)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 29)
        Me.Panel1.TabIndex = 0
        '
        'XpGradientPanel2
        '
        Me.XpGradientPanel2.Controls.Add(Me.ProgresBar1)
        Me.XpGradientPanel2.Controls.Add(Me.lblResult)
        Me.XpGradientPanel2.Controls.Add(Me.btnCancel)
        Me.XpGradientPanel2.Controls.Add(Me.btnOK)
        Me.XpGradientPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.XpGradientPanel2.Location = New System.Drawing.Point(0, -4)
        Me.XpGradientPanel2.Name = "XpGradientPanel2"
        Me.XpGradientPanel2.Size = New System.Drawing.Size(784, 33)
        Me.XpGradientPanel2.TabIndex = 19
        '
        'ProgresBar1
        '
        Me.ProgresBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgresBar1.Location = New System.Drawing.Point(157, 10)
        Me.ProgresBar1.Name = "ProgresBar1"
        Me.ProgresBar1.ShowPercentage = True
        Me.ProgresBar1.Size = New System.Drawing.Size(463, 18)
        Me.ProgresBar1.TabIndex = 8
        Me.ProgresBar1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'lblResult
        '
        Me.lblResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblResult.BackColor = System.Drawing.Color.Black
        Me.lblResult.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblResult.Location = New System.Drawing.Point(157, 10)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(125, 14)
        Me.lblResult.TabIndex = 7
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(706, 7)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(626, 7)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&OK"
        Me.btnOK.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ListView1)
        Me.Panel2.Controls.Add(Me.XpGradientPanel1)
        Me.Panel2.Controls.Add(Me.ExpandableSplitter1)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(5, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(784, 542)
        Me.Panel2.TabIndex = 1
        '
        'ListView1
        '
        Me.ListView1.CheckBoxes = True
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.Location = New System.Drawing.Point(152, 28)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(632, 514)
        Me.ListView1.TabIndex = 2
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.btnViewResult)
        Me.XpGradientPanel1.Controls.Add(Me.txtSearch)
        Me.XpGradientPanel1.Controls.Add(Me.chkCheckListAll)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpGradientPanel1.EndColor = System.Drawing.SystemColors.MenuBar
        Me.XpGradientPanel1.Location = New System.Drawing.Point(152, 0)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(632, 28)
        Me.XpGradientPanel1.StartColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.XpGradientPanel1.TabIndex = 5
        '
        'btnViewResult
        '
        Me.btnViewResult.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnViewResult.Location = New System.Drawing.Point(550, 3)
        Me.btnViewResult.Name = "btnViewResult"
        Me.btnViewResult.Size = New System.Drawing.Size(79, 23)
        Me.btnViewResult.TabIndex = 5
        Me.btnViewResult.Text = "View Result"
        Me.btnViewResult.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSearch.Location = New System.Drawing.Point(80, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(451, 20)
        Me.txtSearch.TabIndex = 3
        Me.txtSearch.WaterMarkColor = System.Drawing.Color.Gray
        Me.txtSearch.WaterMarkText = "Enter data to search"
        '
        'chkCheckListAll
        '
        Me.chkCheckListAll.AutoSize = True
        Me.chkCheckListAll.BackColor = System.Drawing.Color.Transparent
        Me.chkCheckListAll.Location = New System.Drawing.Point(8, 6)
        Me.chkCheckListAll.Name = "chkCheckListAll"
        Me.chkCheckListAll.Size = New System.Drawing.Size(65, 17)
        Me.chkCheckListAll.TabIndex = 4
        Me.chkCheckListAll.Text = "Chek All"
        Me.chkCheckListAll.UseVisualStyleBackColor = False
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.ExpandableControl = Me.Panel3
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
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(147, 0)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(5, 542)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 1
        Me.ExpandableSplitter1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.stvSendData)
        Me.Panel3.Controls.Add(Me.stvSendPersons)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(147, 542)
        Me.Panel3.TabIndex = 0
        '
        'stvSendData
        '
        Me.stvSendData.BackColor = System.Drawing.Color.Transparent
        Me.stvSendData.Controls.Add(Me.rdbRegional)
        Me.stvSendData.Controls.Add(Me.rdbTerritory)
        Me.stvSendData.Controls.Add(Me.rdbPriviledgeFS)
        Me.stvSendData.Controls.Add(Me.rdbPriviledgeTM)
        Me.stvSendData.Controls.Add(Me.rdbPriviledgePM)
        Me.stvSendData.Controls.Add(Me.rdbPriviledgeRSM)
        Me.stvSendData.Controls.Add(Me.rdbDistributor)
        Me.stvSendData.Controls.Add(Me.rdbAgreement)
        Me.stvSendData.Controls.Add(Me.rdbBrandPack)
        Me.stvSendData.Controls.Add(Me.rdbPack)
        Me.stvSendData.Controls.Add(Me.rdbBrand)
        Me.stvSendData.Cursor = System.Windows.Forms.Cursors.Hand
        Me.stvSendData.HeaderText = "Data to create"
        Me.stvSendData.Highlighted = True
        Me.stvSendData.Icon = CType(resources.GetObject("stvSendData.Icon"), System.Drawing.Icon)
        Me.stvSendData.Location = New System.Drawing.Point(0, 207)
        Me.stvSendData.Name = "stvSendData"
        Me.stvSendData.Padding = New System.Windows.Forms.Padding(4, 44, 4, 4)
        Me.stvSendData.Size = New System.Drawing.Size(147, 332)
        Me.stvSendData.TabIndex = 7
        Me.stvSendData.ThemeFormat.BodyColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.stvSendData.ThemeFormat.BodyFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.stvSendData.ThemeFormat.BodyTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.stvSendData.ThemeFormat.BorderColor = System.Drawing.Color.White
        Me.stvSendData.ThemeFormat.ChevronDown = CType(resources.GetObject("resource.ChevronDown"), System.Drawing.Bitmap)
        Me.stvSendData.ThemeFormat.ChevronDownHighlight = CType(resources.GetObject("resource.ChevronDownHighlight"), System.Drawing.Bitmap)
        Me.stvSendData.ThemeFormat.ChevronUp = CType(resources.GetObject("resource.ChevronUp"), System.Drawing.Bitmap)
        Me.stvSendData.ThemeFormat.ChevronUpHighlight = CType(resources.GetObject("resource.ChevronUpHighlight"), System.Drawing.Bitmap)
        Me.stvSendData.ThemeFormat.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.stvSendData.ThemeFormat.HeaderTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.stvSendData.ThemeFormat.HeaderTextHighlightColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.stvSendData.ThemeFormat.LeftHeaderColor = System.Drawing.Color.White
        Me.stvSendData.ThemeFormat.RightHeaderColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        '
        'rdbRegional
        '
        Me.rdbRegional.AutoSize = True
        Me.rdbRegional.Location = New System.Drawing.Point(21, 284)
        Me.rdbRegional.Name = "rdbRegional"
        Me.rdbRegional.Size = New System.Drawing.Size(92, 17)
        Me.rdbRegional.TabIndex = 13
        Me.rdbRegional.TabStop = True
        Me.rdbRegional.Text = "Regional Area"
        Me.rdbRegional.UseVisualStyleBackColor = True
        '
        'rdbTerritory
        '
        Me.rdbTerritory.AutoSize = True
        Me.rdbTerritory.Location = New System.Drawing.Point(21, 307)
        Me.rdbTerritory.Name = "rdbTerritory"
        Me.rdbTerritory.Size = New System.Drawing.Size(85, 17)
        Me.rdbTerritory.TabIndex = 12
        Me.rdbTerritory.TabStop = True
        Me.rdbTerritory.Text = "TerritoryArea"
        Me.rdbTerritory.UseVisualStyleBackColor = True
        '
        'rdbPriviledgeFS
        '
        Me.rdbPriviledgeFS.AutoSize = True
        Me.rdbPriviledgeFS.Location = New System.Drawing.Point(21, 261)
        Me.rdbPriviledgeFS.Name = "rdbPriviledgeFS"
        Me.rdbPriviledgeFS.Size = New System.Drawing.Size(87, 17)
        Me.rdbPriviledgeFS.TabIndex = 11
        Me.rdbPriviledgeFS.TabStop = True
        Me.rdbPriviledgeFS.Text = "Priviledge FS"
        Me.rdbPriviledgeFS.UseVisualStyleBackColor = True
        '
        'rdbPriviledgeTM
        '
        Me.rdbPriviledgeTM.AutoSize = True
        Me.rdbPriviledgeTM.Location = New System.Drawing.Point(21, 234)
        Me.rdbPriviledgeTM.Name = "rdbPriviledgeTM"
        Me.rdbPriviledgeTM.Size = New System.Drawing.Size(90, 17)
        Me.rdbPriviledgeTM.TabIndex = 10
        Me.rdbPriviledgeTM.TabStop = True
        Me.rdbPriviledgeTM.Text = "Priviledge TM"
        Me.rdbPriviledgeTM.UseVisualStyleBackColor = True
        '
        'rdbPriviledgePM
        '
        Me.rdbPriviledgePM.AutoSize = True
        Me.rdbPriviledgePM.Location = New System.Drawing.Point(21, 207)
        Me.rdbPriviledgePM.Name = "rdbPriviledgePM"
        Me.rdbPriviledgePM.Size = New System.Drawing.Size(90, 17)
        Me.rdbPriviledgePM.TabIndex = 9
        Me.rdbPriviledgePM.TabStop = True
        Me.rdbPriviledgePM.Text = "Priviledge PM"
        Me.rdbPriviledgePM.UseVisualStyleBackColor = True
        '
        'rdbPriviledgeRSM
        '
        Me.rdbPriviledgeRSM.AutoSize = True
        Me.rdbPriviledgeRSM.Location = New System.Drawing.Point(21, 180)
        Me.rdbPriviledgeRSM.Name = "rdbPriviledgeRSM"
        Me.rdbPriviledgeRSM.Size = New System.Drawing.Size(98, 17)
        Me.rdbPriviledgeRSM.TabIndex = 8
        Me.rdbPriviledgeRSM.TabStop = True
        Me.rdbPriviledgeRSM.Text = "Priviledge RSM"
        Me.rdbPriviledgeRSM.UseVisualStyleBackColor = True
        '
        'rdbDistributor
        '
        Me.rdbDistributor.AutoSize = True
        Me.rdbDistributor.Location = New System.Drawing.Point(21, 152)
        Me.rdbDistributor.Name = "rdbDistributor"
        Me.rdbDistributor.Size = New System.Drawing.Size(72, 17)
        Me.rdbDistributor.TabIndex = 7
        Me.rdbDistributor.TabStop = True
        Me.rdbDistributor.Text = "Distributor"
        Me.rdbDistributor.UseVisualStyleBackColor = True
        '
        'rdbAgreement
        '
        Me.rdbAgreement.AutoSize = True
        Me.rdbAgreement.Location = New System.Drawing.Point(21, 127)
        Me.rdbAgreement.Name = "rdbAgreement"
        Me.rdbAgreement.Size = New System.Drawing.Size(104, 17)
        Me.rdbAgreement.TabIndex = 6
        Me.rdbAgreement.TabStop = True
        Me.rdbAgreement.Text = "Agreement(PKD)"
        Me.rdbAgreement.UseVisualStyleBackColor = True
        '
        'rdbBrandPack
        '
        Me.rdbBrandPack.AutoSize = True
        Me.rdbBrandPack.Location = New System.Drawing.Point(21, 101)
        Me.rdbBrandPack.Name = "rdbBrandPack"
        Me.rdbBrandPack.Size = New System.Drawing.Size(81, 17)
        Me.rdbBrandPack.TabIndex = 5
        Me.rdbBrandPack.TabStop = True
        Me.rdbBrandPack.Text = "Brand Pack"
        Me.rdbBrandPack.UseVisualStyleBackColor = True
        '
        'rdbPack
        '
        Me.rdbPack.AutoSize = True
        Me.rdbPack.Location = New System.Drawing.Point(21, 76)
        Me.rdbPack.Name = "rdbPack"
        Me.rdbPack.Size = New System.Drawing.Size(50, 17)
        Me.rdbPack.TabIndex = 4
        Me.rdbPack.TabStop = True
        Me.rdbPack.Text = "Pack"
        Me.rdbPack.UseVisualStyleBackColor = True
        '
        'rdbBrand
        '
        Me.rdbBrand.AutoSize = True
        Me.rdbBrand.Location = New System.Drawing.Point(21, 51)
        Me.rdbBrand.Name = "rdbBrand"
        Me.rdbBrand.Size = New System.Drawing.Size(53, 17)
        Me.rdbBrand.TabIndex = 3
        Me.rdbBrand.TabStop = True
        Me.rdbBrand.Text = "Brand"
        Me.rdbBrand.UseVisualStyleBackColor = True
        '
        'stvSendPersons
        '
        Me.stvSendPersons.BackColor = System.Drawing.Color.Transparent
        Me.stvSendPersons.Controls.Add(Me.rdbFS)
        Me.stvSendPersons.Controls.Add(Me.rdbAdmin)
        Me.stvSendPersons.Controls.Add(Me.rdbPM)
        Me.stvSendPersons.Controls.Add(Me.rdbRSM)
        Me.stvSendPersons.Controls.Add(Me.rdbTM)
        Me.stvSendPersons.Cursor = System.Windows.Forms.Cursors.Hand
        Me.stvSendPersons.Dock = System.Windows.Forms.DockStyle.Top
        Me.stvSendPersons.HeaderText = "Type"
        Me.stvSendPersons.Highlighted = True
        Me.stvSendPersons.Icon = CType(resources.GetObject("stvSendPersons.Icon"), System.Drawing.Icon)
        Me.stvSendPersons.Location = New System.Drawing.Point(0, 0)
        Me.stvSendPersons.Name = "stvSendPersons"
        Me.stvSendPersons.Padding = New System.Windows.Forms.Padding(4, 44, 4, 4)
        Me.stvSendPersons.Size = New System.Drawing.Size(147, 201)
        Me.stvSendPersons.TabIndex = 2
        Me.stvSendPersons.ThemeFormat.BodyColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.stvSendPersons.ThemeFormat.BodyFont = New System.Drawing.Font("Tahoma", 8.0!)
        Me.stvSendPersons.ThemeFormat.BodyTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.stvSendPersons.ThemeFormat.BorderColor = System.Drawing.Color.White
        Me.stvSendPersons.ThemeFormat.ChevronDown = CType(resources.GetObject("resource.ChevronDown1"), System.Drawing.Bitmap)
        Me.stvSendPersons.ThemeFormat.ChevronDownHighlight = CType(resources.GetObject("resource.ChevronDownHighlight1"), System.Drawing.Bitmap)
        Me.stvSendPersons.ThemeFormat.ChevronUp = CType(resources.GetObject("resource.ChevronUp1"), System.Drawing.Bitmap)
        Me.stvSendPersons.ThemeFormat.ChevronUpHighlight = CType(resources.GetObject("resource.ChevronUpHighlight1"), System.Drawing.Bitmap)
        Me.stvSendPersons.ThemeFormat.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.stvSendPersons.ThemeFormat.HeaderTextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.stvSendPersons.ThemeFormat.HeaderTextHighlightColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.stvSendPersons.ThemeFormat.LeftHeaderColor = System.Drawing.Color.White
        Me.stvSendPersons.ThemeFormat.RightHeaderColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(240, Byte), Integer))
        '
        'rdbFS
        '
        Me.rdbFS.AutoSize = True
        Me.rdbFS.Location = New System.Drawing.Point(24, 159)
        Me.rdbFS.Name = "rdbFS"
        Me.rdbFS.Size = New System.Drawing.Size(100, 17)
        Me.rdbFS.TabIndex = 7
        Me.rdbFS.TabStop = True
        Me.rdbFS.Text = "Field Supervisor"
        Me.rdbFS.UseVisualStyleBackColor = True
        '
        'rdbAdmin
        '
        Me.rdbAdmin.Enabled = False
        Me.rdbAdmin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.rdbAdmin.ImageIndex = 1
        Me.rdbAdmin.Location = New System.Drawing.Point(24, 129)
        Me.rdbAdmin.Name = "rdbAdmin"
        Me.rdbAdmin.Size = New System.Drawing.Size(69, 26)
        Me.rdbAdmin.TabIndex = 6
        Me.rdbAdmin.TabStop = True
        Me.rdbAdmin.Text = "Admin"
        Me.rdbAdmin.UseVisualStyleBackColor = True
        '
        'rdbPM
        '
        Me.rdbPM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.rdbPM.ImageIndex = 1
        Me.rdbPM.Location = New System.Drawing.Point(24, 101)
        Me.rdbPM.Name = "rdbPM"
        Me.rdbPM.Size = New System.Drawing.Size(50, 28)
        Me.rdbPM.TabIndex = 5
        Me.rdbPM.TabStop = True
        Me.rdbPM.Text = "PM"
        Me.rdbPM.UseVisualStyleBackColor = True
        '
        'rdbRSM
        '
        Me.rdbRSM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.rdbRSM.ImageIndex = 1
        Me.rdbRSM.Location = New System.Drawing.Point(24, 76)
        Me.rdbRSM.Name = "rdbRSM"
        Me.rdbRSM.Size = New System.Drawing.Size(50, 25)
        Me.rdbRSM.TabIndex = 4
        Me.rdbRSM.TabStop = True
        Me.rdbRSM.Text = "RSM"
        Me.rdbRSM.UseVisualStyleBackColor = True
        '
        'rdbTM
        '
        Me.rdbTM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.rdbTM.ImageIndex = 1
        Me.rdbTM.Location = New System.Drawing.Point(24, 48)
        Me.rdbTM.Name = "rdbTM"
        Me.rdbTM.Size = New System.Drawing.Size(50, 27)
        Me.rdbTM.TabIndex = 3
        Me.rdbTM.TabStop = True
        Me.rdbTM.Text = "TM"
        Me.rdbTM.UseVisualStyleBackColor = True
        '
        'FrmSendingData
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(794, 589)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = True
        Me.Name = "FrmSendingData"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Explore data to send"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.XpGradientPanel2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.XpGradientPanel1.ResumeLayout(False)
        Me.XpGradientPanel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.stvSendData.ResumeLayout(False)
        Me.stvSendData.PerformLayout()
        Me.stvSendPersons.ResumeLayout(False)
        Me.stvSendPersons.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Private WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents ListView1 As System.Windows.Forms.ListView
    Private WithEvents btnCancel As Janus.Windows.EditControls.UIButton
    Private WithEvents btnOK As Janus.Windows.EditControls.UIButton
    Private WithEvents stvSendPersons As SteepValley.Windows.Forms.ThemedControls.XPTaskBox
    Private WithEvents rdbAdmin As System.Windows.Forms.RadioButton
    Private WithEvents rdbPM As System.Windows.Forms.RadioButton
    Private WithEvents rdbRSM As System.Windows.Forms.RadioButton
    Private WithEvents rdbTM As System.Windows.Forms.RadioButton
    Private WithEvents stvSendData As SteepValley.Windows.Forms.ThemedControls.XPTaskBox
    Private WithEvents rdbAgreement As System.Windows.Forms.RadioButton
    Private WithEvents rdbBrandPack As System.Windows.Forms.RadioButton
    Private WithEvents rdbPack As System.Windows.Forms.RadioButton
    Private WithEvents rdbBrand As System.Windows.Forms.RadioButton
    Private WithEvents rdbDistributor As System.Windows.Forms.RadioButton
    Private WithEvents txtSearch As WatermarkTextBox.WaterMarkTextBox
    Friend WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents chkCheckListAll As System.Windows.Forms.CheckBox
    Friend WithEvents lblResult As System.Windows.Forms.Label
    Private WithEvents XpGradientPanel2 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents btnViewResult As Janus.Windows.EditControls.UIButton
    Private WithEvents ProgresBar1 As Janus.Windows.EditControls.UIProgressBar
    Private WithEvents rdbPriviledgeTM As System.Windows.Forms.RadioButton
    Private WithEvents rdbPriviledgePM As System.Windows.Forms.RadioButton
    Private WithEvents rdbPriviledgeRSM As System.Windows.Forms.RadioButton
    Private WithEvents rdbPriviledgeFS As System.Windows.Forms.RadioButton
    Private WithEvents rdbFS As System.Windows.Forms.RadioButton
    Private WithEvents rdbRegional As System.Windows.Forms.RadioButton
    Private WithEvents rdbTerritory As System.Windows.Forms.RadioButton

End Class
