<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SPPBManager
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim UiComboBoxItem1 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem2 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem3 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim UiComboBoxItem4 As Janus.Windows.EditControls.UIComboBoxItem = New Janus.Windows.EditControls.UIComboBoxItem
        Dim grdHeader_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdDetail_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.xpgFilterDate = New SteepValley.Windows.Forms.XPGradientPanel
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.btnCatPO = New DevComponents.DotNetBar.ButtonItem
        Me.btnCatSPPB = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustom = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilteDate = New Janus.Windows.EditControls.UIButton
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.lblFrom = New System.Windows.Forms.Label
        Me.dtpicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.lblUntil = New System.Windows.Forms.Label
        Me.cmbFilterBy = New Janus.Windows.EditControls.UIComboBox
        Me.txtFind = New System.Windows.Forms.TextBox
        Me.grdHeader = New Janus.Windows.GridEX.GridEX
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.pnlDetail = New System.Windows.Forms.Panel
        Me.grdDetail = New Janus.Windows.GridEX.GridEX
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.GetDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SalesReportSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SalesReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SPPBEntryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditGONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PrintGONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.btnPrintGonCurSell = New System.Windows.Forms.ToolStripMenuItem
        Me.btnPrinCustoms = New System.Windows.Forms.ToolStripMenuItem
        Me.chkFilter = New System.Windows.Forms.CheckBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnPrintSPPB = New DevComponents.DotNetBar.ButtonItem
        Me.btnCurrentSelection = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrintcustoms = New DevComponents.DotNetBar.ButtonItem
        Me.xpgFilterDate.SuspendLayout()
        CType(Me.grdHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.pnlDetail.SuspendLayout()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'xpgFilterDate
        '
        Me.xpgFilterDate.Controls.Add(Me.ItemPanel1)
        Me.xpgFilterDate.Controls.Add(Me.btnFilteDate)
        Me.xpgFilterDate.Controls.Add(Me.dtPicUntil)
        Me.xpgFilterDate.Controls.Add(Me.lblFrom)
        Me.xpgFilterDate.Controls.Add(Me.dtpicFrom)
        Me.xpgFilterDate.Controls.Add(Me.lblUntil)
        Me.xpgFilterDate.Controls.Add(Me.cmbFilterBy)
        Me.xpgFilterDate.Controls.Add(Me.txtFind)
        Me.xpgFilterDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.xpgFilterDate.EndColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.xpgFilterDate.Gradient = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.xpgFilterDate.Location = New System.Drawing.Point(0, 0)
        Me.xpgFilterDate.Name = "xpgFilterDate"
        Me.xpgFilterDate.Size = New System.Drawing.Size(909, 28)
        Me.xpgFilterDate.StartColor = System.Drawing.SystemColors.MenuBar
        Me.xpgFilterDate.TabIndex = 29
        '
        'ItemPanel1
        '
        '
        '
        '
        Me.ItemPanel1.BackgroundStyle.BackColor = System.Drawing.SystemColors.MenuBar
        Me.ItemPanel1.BackgroundStyle.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
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
        Me.ItemPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCatPO, Me.btnCatSPPB, Me.btnCustom})
        Me.ItemPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(226, 28)
        Me.ItemPanel1.TabIndex = 26
        Me.ItemPanel1.Text = "ItemPanel1"
        '
        'btnCatPO
        '
        Me.btnCatPO.Name = "btnCatPO"
        Me.btnCatPO.Text = "Purchase Order Date"
        '
        'btnCatSPPB
        '
        Me.btnCatSPPB.Checked = True
        Me.btnCatSPPB.Name = "btnCatSPPB"
        Me.btnCatSPPB.Text = "SPPB Date"
        '
        'btnCustom
        '
        Me.btnCustom.Name = "btnCustom"
        Me.btnCustom.Text = "Custom"
        '
        'btnFilteDate
        '
        Me.btnFilteDate.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnFilteDate.ImageIndex = 3
        Me.btnFilteDate.Location = New System.Drawing.Point(698, 4)
        Me.btnFilteDate.Name = "btnFilteDate"
        Me.btnFilteDate.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnFilteDate.Size = New System.Drawing.Size(85, 20)
        Me.btnFilteDate.TabIndex = 2
        Me.btnFilteDate.Text = "Apply Filter"
        Me.btnFilteDate.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'dtPicUntil
        '
        Me.dtPicUntil.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicUntil.CustomFormat = "dd MMMM yyyy"
        Me.dtPicUntil.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicUntil.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicUntil.DropDownCalendar.FirstMonth = New Date(2022, 12, 1, 0, 0, 0, 0)
        Me.dtPicUntil.DropDownCalendar.Name = ""
        Me.dtPicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicUntil.Location = New System.Drawing.Point(504, 4)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(174, 20)
        Me.dtPicUntil.TabIndex = 4
        Me.dtPicUntil.Value = New Date(2014, 3, 17, 0, 0, 0, 0)
        Me.dtPicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(233, 7)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(38, 14)
        Me.lblFrom.TabIndex = 1
        Me.lblFrom.Text = "FROM"
        '
        'dtpicFrom
        '
        Me.dtpicFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtpicFrom.CustomFormat = "dd MMMM yyyy"
        Me.dtpicFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpicFrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtpicFrom.DropDownCalendar.FirstMonth = New Date(2022, 12, 1, 0, 0, 0, 0)
        Me.dtpicFrom.DropDownCalendar.Name = ""
        Me.dtpicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtpicFrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtpicFrom.Location = New System.Drawing.Point(293, 4)
        Me.dtpicFrom.Name = "dtpicFrom"
        Me.dtpicFrom.ShowTodayButton = False
        Me.dtpicFrom.Size = New System.Drawing.Size(157, 20)
        Me.dtpicFrom.TabIndex = 2
        Me.dtpicFrom.Value = New Date(2014, 3, 17, 0, 0, 0, 0)
        Me.dtpicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'lblUntil
        '
        Me.lblUntil.BackColor = System.Drawing.Color.Transparent
        Me.lblUntil.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUntil.Location = New System.Drawing.Point(457, 8)
        Me.lblUntil.Name = "lblUntil"
        Me.lblUntil.Size = New System.Drawing.Size(41, 16)
        Me.lblUntil.TabIndex = 3
        Me.lblUntil.Text = "UNTIL"
        '
        'cmbFilterBy
        '
        Me.cmbFilterBy.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        UiComboBoxItem1.FormatStyle.Alpha = 0
        UiComboBoxItem1.IsSeparator = False
        UiComboBoxItem1.Text = "DISTRIBUTOR"
        UiComboBoxItem1.Value = "DISTRIBUTOR"
        UiComboBoxItem2.FormatStyle.Alpha = 0
        UiComboBoxItem2.IsSeparator = False
        UiComboBoxItem2.Text = "PO_NUMBER"
        UiComboBoxItem2.Value = "PO_NUMBER"
        UiComboBoxItem3.FormatStyle.Alpha = 0
        UiComboBoxItem3.IsSeparator = False
        UiComboBoxItem3.Text = "SPPB_NUMBER"
        UiComboBoxItem3.Value = "SPPB_NUMBER"
        UiComboBoxItem4.FormatStyle.Alpha = 0
        UiComboBoxItem4.IsSeparator = False
        UiComboBoxItem4.Text = "GON_NUMBER"
        UiComboBoxItem4.Value = "GON_NUMBER"
        Me.cmbFilterBy.Items.AddRange(New Janus.Windows.EditControls.UIComboBoxItem() {UiComboBoxItem1, UiComboBoxItem2, UiComboBoxItem3, UiComboBoxItem4})
        Me.cmbFilterBy.ItemsFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbFilterBy.Location = New System.Drawing.Point(293, 4)
        Me.cmbFilterBy.Name = "cmbFilterBy"
        Me.cmbFilterBy.Size = New System.Drawing.Size(152, 20)
        Me.cmbFilterBy.StateStyles.FormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbFilterBy.TabIndex = 32
        Me.cmbFilterBy.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'txtFind
        '
        Me.txtFind.Location = New System.Drawing.Point(510, 4)
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(141, 20)
        Me.txtFind.TabIndex = 33
        '
        'grdHeader
        '
        Me.grdHeader.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdHeader.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdHeader.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdHeader.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdHeader_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdHeader.DesignTimeLayout = grdHeader_DesignTimeLayout
        Me.grdHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdHeader.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdHeader.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdHeader.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdHeader.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdHeader.GroupByBoxVisible = False
        Me.grdHeader.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdHeader.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdHeader.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdHeader.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdHeader.Location = New System.Drawing.Point(0, 0)
        Me.grdHeader.Name = "grdHeader"
        Me.grdHeader.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdHeader.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdHeader.Size = New System.Drawing.Size(909, 218)
        Me.grdHeader.TabIndex = 28
        Me.grdHeader.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdHeader.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.grdHeader)
        Me.pnlMain.Controls.Add(Me.ExpandableSplitter1)
        Me.pnlMain.Controls.Add(Me.pnlDetail)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 28)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(909, 497)
        Me.pnlMain.TabIndex = 30
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ExpandableSplitter1.ExpandableControl = Me.pnlDetail
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
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(0, 218)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(909, 4)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 31
        Me.ExpandableSplitter1.TabStop = False
        '
        'pnlDetail
        '
        Me.pnlDetail.Controls.Add(Me.grdDetail)
        Me.pnlDetail.Controls.Add(Me.chkFilter)
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlDetail.Location = New System.Drawing.Point(0, 222)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(909, 275)
        Me.pnlDetail.TabIndex = 30
        '
        'grdDetail
        '
        Me.grdDetail.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdDetail.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdDetail.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdDetail.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.grdDetail.ContextMenuStrip = Me.ContextMenuStrip1
        grdDetail_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdDetail.DesignTimeLayout = grdDetail_DesignTimeLayout
        Me.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdDetail.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdDetail.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdDetail.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdDetail.GroupByBoxVisible = False
        Me.grdDetail.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdDetail.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdDetail.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdDetail.Location = New System.Drawing.Point(0, 17)
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdDetail.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdDetail.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdDetail.Size = New System.Drawing.Size(909, 258)
        Me.grdDetail.TabIndex = 29
        Me.grdDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdDetail.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GetDataToolStripMenuItem, Me.EditGONToolStripMenuItem, Me.PrintGONToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(129, 70)
        '
        'GetDataToolStripMenuItem
        '
        Me.GetDataToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalesReportSummaryToolStripMenuItem, Me.SalesReportToolStripMenuItem, Me.SPPBEntryToolStripMenuItem})
        Me.GetDataToolStripMenuItem.Name = "GetDataToolStripMenuItem"
        Me.GetDataToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.GetDataToolStripMenuItem.Text = "Get Data"
        '
        'SalesReportSummaryToolStripMenuItem
        '
        Me.SalesReportSummaryToolStripMenuItem.Name = "SalesReportSummaryToolStripMenuItem"
        Me.SalesReportSummaryToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.SalesReportSummaryToolStripMenuItem.Text = "Sales Report Summary"
        '
        'SalesReportToolStripMenuItem
        '
        Me.SalesReportToolStripMenuItem.Name = "SalesReportToolStripMenuItem"
        Me.SalesReportToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.SalesReportToolStripMenuItem.Text = "Sales Report Detail"
        '
        'SPPBEntryToolStripMenuItem
        '
        Me.SPPBEntryToolStripMenuItem.Name = "SPPBEntryToolStripMenuItem"
        Me.SPPBEntryToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.SPPBEntryToolStripMenuItem.Text = "SPPB Entry"
        '
        'EditGONToolStripMenuItem
        '
        Me.EditGONToolStripMenuItem.Name = "EditGONToolStripMenuItem"
        Me.EditGONToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.EditGONToolStripMenuItem.Text = "Edit GON"
        '
        'PrintGONToolStripMenuItem
        '
        Me.PrintGONToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPrintGonCurSell, Me.btnPrinCustoms})
        Me.PrintGONToolStripMenuItem.Name = "PrintGONToolStripMenuItem"
        Me.PrintGONToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.PrintGONToolStripMenuItem.Text = "Print GON"
        '
        'btnPrintGonCurSell
        '
        Me.btnPrintGonCurSell.Name = "btnPrintGonCurSell"
        Me.btnPrintGonCurSell.Size = New System.Drawing.Size(168, 22)
        Me.btnPrintGonCurSell.Text = "Current Sellection"
        '
        'btnPrinCustoms
        '
        Me.btnPrinCustoms.Name = "btnPrinCustoms"
        Me.btnPrinCustoms.Size = New System.Drawing.Size(168, 22)
        Me.btnPrinCustoms.Text = "Customs"
        '
        'chkFilter
        '
        Me.chkFilter.AutoSize = True
        Me.chkFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkFilter.Location = New System.Drawing.Point(0, 0)
        Me.chkFilter.Name = "chkFilter"
        Me.chkFilter.Size = New System.Drawing.Size(909, 17)
        Me.chkFilter.TabIndex = 30
        Me.chkFilter.Text = "Filter Detail with header(when the row on header data is selected)"
        Me.chkFilter.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ShowAlways = True
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'btnPrintSPPB
        '
        Me.btnPrintSPPB.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrintSPPB.ImageIndex = 9
        Me.btnPrintSPPB.Name = "btnPrintSPPB"
        Me.btnPrintSPPB.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCurrentSelection, Me.btnPrintcustoms})
        Me.btnPrintSPPB.Text = "Printing SPPB"
        '
        'btnCurrentSelection
        '
        Me.btnCurrentSelection.Name = "btnCurrentSelection"
        Me.btnCurrentSelection.Text = "Current Sell"
        '
        'btnPrintcustoms
        '
        Me.btnPrintcustoms.Name = "btnPrintcustoms"
        Me.btnPrintcustoms.Text = "Customs"
        '
        'SPPBManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.xpgFilterDate)
        Me.Name = "SPPBManager"
        Me.Size = New System.Drawing.Size(909, 525)
        Me.xpgFilterDate.ResumeLayout(False)
        Me.xpgFilterDate.PerformLayout()
        CType(Me.grdHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlDetail.PerformLayout()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lblFrom As System.Windows.Forms.Label
    Private WithEvents lblUntil As System.Windows.Forms.Label
    Friend WithEvents btnFilteDate As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtpicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents grdHeader As Janus.Windows.GridEX.GridEX
    Private WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Private WithEvents btnCatPO As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCatSPPB As DevComponents.DotNetBar.ButtonItem
    Private WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents grdDetail As Janus.Windows.GridEX.GridEX
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents pnlDetail As System.Windows.Forms.Panel
    Friend WithEvents xpgFilterDate As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents cmbFilterBy As Janus.Windows.EditControls.UIComboBox
    Private WithEvents btnCustom As DevComponents.DotNetBar.ButtonItem
    Private WithEvents chkFilter As System.Windows.Forms.CheckBox
    Private WithEvents txtFind As System.Windows.Forms.TextBox
    Private WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents GetDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalesReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SPPBEntryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditGONToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalesReportSummaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintGONToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPrintGonCurSell As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPrinCustoms As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPrintSPPB As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCurrentSelection As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrintcustoms As DevComponents.DotNetBar.ButtonItem

End Class
