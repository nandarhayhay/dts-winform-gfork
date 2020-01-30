<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RecapNasionalDPRD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RecapNasionalDPRD))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.grpRangeDate = New Janus.Windows.EditControls.UIGroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnApplyRange = New Janus.Windows.EditControls.UIButton
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicfrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ExplorerBar1 = New DevComponents.DotNetBar.ExplorerBar
        Me.grDPRDType = New DevComponents.DotNetBar.ExplorerBarGroupItem
        Me.btnSitemDPRDM = New DevComponents.DotNetBar.ButtonItem
        Me.btnSItemDPRDS = New DevComponents.DotNetBar.ButtonItem
        Me.grReportType = New DevComponents.DotNetBar.ExplorerBarGroupItem
        Me.btnSSummaryReportView = New DevComponents.DotNetBar.ButtonItem
        Me.btnSDetailReportView = New DevComponents.DotNetBar.ButtonItem
        Me.grdDetailReportBy = New DevComponents.DotNetBar.ExplorerBarGroupItem
        Me.btnSItemByNational = New DevComponents.DotNetBar.ButtonItem
        Me.btnByRegional = New DevComponents.DotNetBar.ButtonItem
        Me.btnByTerritorial = New DevComponents.DotNetBar.ButtonItem
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.XpGradientPanel1.SuspendLayout()
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRangeDate.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ExplorerBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.grpRangeDate)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpGradientPanel1.EndColor = System.Drawing.SystemColors.MenuBar
        Me.XpGradientPanel1.Location = New System.Drawing.Point(152, 0)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(614, 56)
        Me.XpGradientPanel1.StartColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.XpGradientPanel1.TabIndex = 1
        '
        'grpRangeDate
        '
        Me.grpRangeDate.BackColor = System.Drawing.Color.Transparent
        Me.grpRangeDate.Controls.Add(Me.Label2)
        Me.grpRangeDate.Controls.Add(Me.btnApplyRange)
        Me.grpRangeDate.Controls.Add(Me.dtPicUntil)
        Me.grpRangeDate.Controls.Add(Me.dtPicfrom)
        Me.grpRangeDate.Controls.Add(Me.Label1)
        Me.grpRangeDate.Location = New System.Drawing.Point(25, 3)
        Me.grpRangeDate.Name = "grpRangeDate"
        Me.grpRangeDate.Size = New System.Drawing.Size(564, 47)
        Me.grpRangeDate.TabIndex = 0
        Me.grpRangeDate.Text = "Periode"
        Me.grpRangeDate.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(239, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Until"
        '
        'btnApplyRange
        '
        Me.btnApplyRange.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnApplyRange.Icon = CType(resources.GetObject("btnApplyRange.Icon"), System.Drawing.Icon)
        Me.btnApplyRange.ImageIndex = 3
        Me.btnApplyRange.Location = New System.Drawing.Point(472, 16)
        Me.btnApplyRange.Name = "btnApplyRange"
        Me.btnApplyRange.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnApplyRange.Size = New System.Drawing.Size(80, 20)
        Me.btnApplyRange.TabIndex = 9
        Me.btnApplyRange.Text = "Apply Filter"
        Me.btnApplyRange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'dtPicUntil
        '
        Me.dtPicUntil.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicUntil.BorderStyle = Janus.Windows.CalendarCombo.BorderStyle.None
        Me.dtPicUntil.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicUntil.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicUntil.DropDownCalendar.FirstMonth = New Date(2009, 4, 1, 0, 0, 0, 0)
        Me.dtPicUntil.DropDownCalendar.Name = ""
        Me.dtPicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        Me.dtPicUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicUntil.Location = New System.Drawing.Point(273, 17)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(193, 20)
        Me.dtPicUntil.TabIndex = 8
        Me.dtPicUntil.Value = New Date(2009, 4, 3, 0, 0, 0, 0)
        Me.dtPicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        '
        'dtPicfrom
        '
        Me.dtPicfrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicfrom.BorderStyle = Janus.Windows.CalendarCombo.BorderStyle.None
        Me.dtPicfrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicfrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicfrom.DropDownCalendar.FirstMonth = New Date(2009, 4, 1, 0, 0, 0, 0)
        Me.dtPicfrom.DropDownCalendar.Name = ""
        Me.dtPicfrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        Me.dtPicfrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicfrom.Location = New System.Drawing.Point(43, 16)
        Me.dtPicfrom.Name = "dtPicfrom"
        Me.dtPicfrom.ShowTodayButton = False
        Me.dtPicfrom.Size = New System.Drawing.Size(192, 20)
        Me.dtPicfrom.TabIndex = 7
        Me.dtPicfrom.Value = New Date(2009, 4, 3, 0, 0, 0, 0)
        Me.dtPicfrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(7, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "From"
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX1_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.Location = New System.Drawing.Point(152, 56)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Size = New System.Drawing.Size(614, 410)
        Me.GridEX1.TabIndex = 2
        Me.GridEX1.UpdateOnLeave = False
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ExplorerBar1
        '
        Me.ExplorerBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.ExplorerBar1.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.ExplorerBar1.BackStyle.BackColor = System.Drawing.SystemColors.MenuBar
        Me.ExplorerBar1.BackStyle.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ExplorerBar1.BackStyle.BackColorGradientAngle = 90
        Me.ExplorerBar1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ExplorerBar1.GroupImages = Nothing
        Me.ExplorerBar1.Groups.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.grDPRDType, Me.grReportType, Me.grdDetailReportBy})
        Me.ExplorerBar1.Images = Nothing
        Me.ExplorerBar1.Location = New System.Drawing.Point(0, 0)
        Me.ExplorerBar1.Name = "ExplorerBar1"
        Me.ExplorerBar1.Size = New System.Drawing.Size(147, 466)
        Me.ExplorerBar1.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.OliveGreen
        Me.ExplorerBar1.TabIndex = 4
        Me.ExplorerBar1.Text = "ExplorerBar1"
        Me.ExplorerBar1.ThemeAware = True
        '
        'grDPRDType
        '
        '
        '
        '
        Me.grDPRDType.BackStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.grDPRDType.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.grDPRDType.BackStyle.BorderBottomWidth = 1
        Me.grDPRDType.BackStyle.BorderColor = System.Drawing.Color.White
        Me.grDPRDType.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.grDPRDType.BackStyle.BorderLeftWidth = 1
        Me.grDPRDType.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.grDPRDType.BackStyle.BorderRightWidth = 1
        Me.grDPRDType.Expanded = True
        Me.grDPRDType.Image = Global.DTSProjects.My.Resources.Resources.DPRD
        Me.grDPRDType.Name = "grDPRDType"
        Me.grDPRDType.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Blue
        Me.grDPRDType.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnSitemDPRDM, Me.btnSItemDPRDS})
        Me.grDPRDType.Text = "DPRD Type"
        '
        '
        '
        Me.grDPRDType.TitleHotStyle.BackColor = System.Drawing.Color.White
        Me.grDPRDType.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.grDPRDType.TitleHotStyle.CornerDiameter = 3
        Me.grDPRDType.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grDPRDType.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grDPRDType.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        '
        '
        Me.grDPRDType.TitleStyle.BackColor = System.Drawing.Color.White
        Me.grDPRDType.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.grDPRDType.TitleStyle.CornerDiameter = 3
        Me.grDPRDType.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grDPRDType.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grDPRDType.TitleStyle.TextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        '
        'btnSitemDPRDM
        '
        Me.btnSitemDPRDM.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSitemDPRDM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSitemDPRDM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnSitemDPRDM.HotFontUnderline = True
        Me.btnSitemDPRDM.HotForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSitemDPRDM.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None
        Me.btnSitemDPRDM.Name = "btnSitemDPRDM"
        Me.btnSitemDPRDM.PopupSide = DevComponents.DotNetBar.ePopupSide.Left
        Me.btnSitemDPRDM.Text = "DPRDM"
        '
        'btnSItemDPRDS
        '
        Me.btnSItemDPRDS.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSItemDPRDS.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSItemDPRDS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnSItemDPRDS.HotFontUnderline = True
        Me.btnSItemDPRDS.HotForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSItemDPRDS.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None
        Me.btnSItemDPRDS.Name = "btnSItemDPRDS"
        Me.btnSItemDPRDS.PopupSide = DevComponents.DotNetBar.ePopupSide.Left
        Me.btnSItemDPRDS.Text = "DPRDS"
        '
        'grReportType
        '
        '
        '
        '
        Me.grReportType.BackStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.grReportType.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.grReportType.BackStyle.BorderBottomWidth = 1
        Me.grReportType.BackStyle.BorderColor = System.Drawing.Color.White
        Me.grReportType.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.grReportType.BackStyle.BorderLeftWidth = 1
        Me.grReportType.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.grReportType.BackStyle.BorderRightWidth = 1
        Me.grReportType.Expanded = True
        Me.grReportType.Image = Global.DTSProjects.My.Resources.Resources.CurrentView
        Me.grReportType.Name = "grReportType"
        Me.grReportType.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Blue
        Me.grReportType.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnSSummaryReportView, Me.btnSDetailReportView})
        Me.grReportType.Text = "Report View"
        '
        '
        '
        Me.grReportType.TitleHotStyle.BackColor = System.Drawing.Color.White
        Me.grReportType.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.grReportType.TitleHotStyle.CornerDiameter = 3
        Me.grReportType.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grReportType.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grReportType.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        '
        '
        Me.grReportType.TitleStyle.BackColor = System.Drawing.Color.White
        Me.grReportType.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.grReportType.TitleStyle.CornerDiameter = 3
        Me.grReportType.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grReportType.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grReportType.TitleStyle.TextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        '
        'btnSSummaryReportView
        '
        Me.btnSSummaryReportView.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSSummaryReportView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSSummaryReportView.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnSSummaryReportView.HotFontUnderline = True
        Me.btnSSummaryReportView.HotForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSSummaryReportView.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None
        Me.btnSSummaryReportView.Name = "btnSSummaryReportView"
        Me.btnSSummaryReportView.PopupSide = DevComponents.DotNetBar.ePopupSide.Left
        Me.btnSSummaryReportView.Text = "Summary"
        '
        'btnSDetailReportView
        '
        Me.btnSDetailReportView.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSDetailReportView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSDetailReportView.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnSDetailReportView.HotFontUnderline = True
        Me.btnSDetailReportView.HotForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSDetailReportView.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None
        Me.btnSDetailReportView.Name = "btnSDetailReportView"
        Me.btnSDetailReportView.PopupSide = DevComponents.DotNetBar.ePopupSide.Left
        Me.btnSDetailReportView.Text = "Detail"
        '
        'grdDetailReportBy
        '
        '
        '
        '
        Me.grdDetailReportBy.BackStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.grdDetailReportBy.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.grdDetailReportBy.BackStyle.BorderBottomWidth = 1
        Me.grdDetailReportBy.BackStyle.BorderColor = System.Drawing.Color.White
        Me.grdDetailReportBy.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.grdDetailReportBy.BackStyle.BorderLeftWidth = 1
        Me.grdDetailReportBy.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.grdDetailReportBy.BackStyle.BorderRightWidth = 1
        Me.grdDetailReportBy.Expanded = True
        Me.grdDetailReportBy.Image = Global.DTSProjects.My.Resources.Resources.Grid
        Me.grdDetailReportBy.Name = "grdDetailReportBy"
        Me.grdDetailReportBy.StockStyle = DevComponents.DotNetBar.eExplorerBarStockStyle.Blue
        Me.grdDetailReportBy.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnSItemByNational, Me.btnByRegional, Me.btnByTerritorial})
        Me.grdDetailReportBy.Text = "Group Report by"
        '
        '
        '
        Me.grdDetailReportBy.TitleHotStyle.BackColor = System.Drawing.Color.White
        Me.grdDetailReportBy.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.grdDetailReportBy.TitleHotStyle.CornerDiameter = 3
        Me.grdDetailReportBy.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grdDetailReportBy.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grdDetailReportBy.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        '
        '
        Me.grdDetailReportBy.TitleStyle.BackColor = System.Drawing.Color.White
        Me.grdDetailReportBy.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.grdDetailReportBy.TitleStyle.CornerDiameter = 3
        Me.grdDetailReportBy.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grdDetailReportBy.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded
        Me.grdDetailReportBy.TitleStyle.TextColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        '
        'btnSItemByNational
        '
        Me.btnSItemByNational.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSItemByNational.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSItemByNational.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnSItemByNational.HotFontUnderline = True
        Me.btnSItemByNational.HotForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSItemByNational.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None
        Me.btnSItemByNational.Name = "btnSItemByNational"
        Me.btnSItemByNational.PopupSide = DevComponents.DotNetBar.ePopupSide.Left
        Me.btnSItemByNational.Text = "By National"
        '
        'btnByRegional
        '
        Me.btnByRegional.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnByRegional.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnByRegional.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnByRegional.HotFontUnderline = True
        Me.btnByRegional.HotForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnByRegional.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None
        Me.btnByRegional.Name = "btnByRegional"
        Me.btnByRegional.PopupSide = DevComponents.DotNetBar.ePopupSide.Left
        Me.btnByRegional.Text = "by Regional"
        '
        'btnByTerritorial
        '
        Me.btnByTerritorial.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnByTerritorial.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnByTerritorial.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.btnByTerritorial.HotFontUnderline = True
        Me.btnByTerritorial.HotForeColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnByTerritorial.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None
        Me.btnByTerritorial.Name = "btnByTerritorial"
        Me.btnByTerritorial.PopupSide = DevComponents.DotNetBar.ePopupSide.Left
        Me.btnByTerritorial.Text = "byTerritorial"
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.ExpandableControl = Me.ExplorerBar1
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
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(5, 466)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 5
        Me.ExpandableSplitter1.TabStop = False
        '
        'RecapNasionalDPRD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.XpGradientPanel1)
        Me.Controls.Add(Me.ExpandableSplitter1)
        Me.Controls.Add(Me.ExplorerBar1)
        Me.Name = "RecapNasionalDPRD"
        Me.Size = New System.Drawing.Size(766, 466)
        Me.XpGradientPanel1.ResumeLayout(False)
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRangeDate.ResumeLayout(False)
        Me.grpRangeDate.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ExplorerBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Friend WithEvents grpRangeDate As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents btnApplyRange As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicfrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents ExplorerBar1 As DevComponents.DotNetBar.ExplorerBar
    Private WithEvents grDPRDType As DevComponents.DotNetBar.ExplorerBarGroupItem
    Private WithEvents btnSitemDPRDM As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSItemDPRDS As DevComponents.DotNetBar.ButtonItem
    Private WithEvents grdDetailReportBy As DevComponents.DotNetBar.ExplorerBarGroupItem
    Private WithEvents grReportType As DevComponents.DotNetBar.ExplorerBarGroupItem
    Private WithEvents btnSItemByNational As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnByRegional As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnByTerritorial As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSSummaryReportView As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSDetailReportView As DevComponents.DotNetBar.ButtonItem
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX

End Class
