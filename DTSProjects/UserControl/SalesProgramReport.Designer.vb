<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SalesProgramReport
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
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SalesProgramReport))
        Dim GridEX2_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.btnCPD = New DevComponents.DotNetBar.ButtonItem
        Me.btnCPMRT = New DevComponents.DotNetBar.ButtonItem
        Me.btnPKPP = New DevComponents.DotNetBar.ButtonItem
        Me.btnDK = New DevComponents.DotNetBar.ButtonItem
        Me.btnCPRD = New DevComponents.DotNetBar.ButtonItem
        Me.LabelItem1 = New DevComponents.DotNetBar.LabelItem
        Me.btnSalesbyPO = New DevComponents.DotNetBar.ButtonItem
        Me.grpRangeDate = New Janus.Windows.EditControls.UIGroupBox
        Me.chkFilter = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnApplyRange = New Janus.Windows.EditControls.UIButton
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicfrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.grpParent = New System.Windows.Forms.GroupBox
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.grpChild = New System.Windows.Forms.GroupBox
        Me.GridEX2 = New Janus.Windows.GridEX.GridEX
        Me.ExpandableSplitter2 = New DevComponents.DotNetBar.ExpandableSplitter
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRangeDate.SuspendLayout()
        Me.grpParent.SuspendLayout()
        Me.grpChild.SuspendLayout()
        CType(Me.GridEX2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEX1
        '
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AutoEdit = True
        Me.GridEX1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
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
        Me.GridEX1.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Location = New System.Drawing.Point(3, 16)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Size = New System.Drawing.Size(1029, 242)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ItemPanel1
        '
        '
        '
        '
        Me.ItemPanel1.BackgroundStyle.BackColor = System.Drawing.Color.White
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
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCPD, Me.btnCPMRT, Me.btnPKPP, Me.btnDK, Me.btnCPRD, Me.LabelItem1, Me.btnSalesbyPO})
        Me.ItemPanel1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical
        Me.ItemPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(100, 518)
        Me.ItemPanel1.TabIndex = 2
        Me.ItemPanel1.Text = "ItemPanel1"
        '
        'btnCPD
        '
        Me.btnCPD.Name = "btnCPD"
        Me.btnCPD.Text = "CPD"
        '
        'btnCPMRT
        '
        Me.btnCPMRT.Name = "btnCPMRT"
        Me.btnCPMRT.Text = "CPR(M/T)"
        '
        'btnPKPP
        '
        Me.btnPKPP.Name = "btnPKPP"
        Me.btnPKPP.Text = "PKPP"
        '
        'btnDK
        '
        Me.btnDK.Name = "btnDK"
        Me.btnDK.Text = "DK"
        '
        'btnCPRD
        '
        Me.btnCPRD.Name = "btnCPRD"
        Me.btnCPRD.Text = "CPRD"
        '
        'LabelItem1
        '
        Me.LabelItem1.BorderType = DevComponents.DotNetBar.eBorderType.None
        Me.LabelItem1.Name = "LabelItem1"
        Me.LabelItem1.Text = "______________"
        '
        'btnSalesbyPO
        '
        Me.btnSalesbyPO.Name = "btnSalesbyPO"
        Me.btnSalesbyPO.Text = "Sales PO by TM"
        '
        'grpRangeDate
        '
        Me.grpRangeDate.BackColor = System.Drawing.Color.Transparent
        Me.grpRangeDate.Controls.Add(Me.chkFilter)
        Me.grpRangeDate.Controls.Add(Me.Label2)
        Me.grpRangeDate.Controls.Add(Me.btnApplyRange)
        Me.grpRangeDate.Controls.Add(Me.dtPicUntil)
        Me.grpRangeDate.Controls.Add(Me.dtPicfrom)
        Me.grpRangeDate.Controls.Add(Me.Label1)
        Me.grpRangeDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpRangeDate.Location = New System.Drawing.Point(103, 0)
        Me.grpRangeDate.Name = "grpRangeDate"
        Me.grpRangeDate.Size = New System.Drawing.Size(1035, 73)
        Me.grpRangeDate.TabIndex = 3
        Me.grpRangeDate.Text = "Range Data"
        Me.grpRangeDate.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'chkFilter
        '
        Me.chkFilter.AutoSize = True
        Me.chkFilter.BackColor = System.Drawing.Color.Transparent
        Me.chkFilter.Location = New System.Drawing.Point(16, 47)
        Me.chkFilter.Name = "chkFilter"
        Me.chkFilter.Size = New System.Drawing.Size(287, 17)
        Me.chkFilter.TabIndex = 11
        Me.chkFilter.Text = "Filter Detail with header(row on header data is selected)"
        Me.chkFilter.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(269, 24)
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
        Me.btnApplyRange.Location = New System.Drawing.Point(504, 19)
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
        Me.dtPicUntil.CustomFormat = "dd MMMM yyyy"
        Me.dtPicUntil.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicUntil.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicUntil.DropDownCalendar.FirstMonth = New Date(2009, 4, 1, 0, 0, 0, 0)
        Me.dtPicUntil.DropDownCalendar.Name = ""
        Me.dtPicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        Me.dtPicUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicUntil.Location = New System.Drawing.Point(303, 20)
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
        Me.dtPicfrom.CustomFormat = "dd MMMM yyyy"
        Me.dtPicfrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicfrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicfrom.DropDownCalendar.FirstMonth = New Date(2009, 4, 1, 0, 0, 0, 0)
        Me.dtPicfrom.DropDownCalendar.Name = ""
        Me.dtPicfrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        Me.dtPicfrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicfrom.Location = New System.Drawing.Point(57, 20)
        Me.dtPicfrom.Name = "dtPicfrom"
        Me.dtPicfrom.ShowTodayButton = False
        Me.dtPicfrom.Size = New System.Drawing.Size(208, 20)
        Me.dtPicfrom.TabIndex = 7
        Me.dtPicfrom.Value = New Date(2009, 4, 3, 0, 0, 0, 0)
        Me.dtPicfrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(16, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "From"
        '
        'grpParent
        '
        Me.grpParent.Controls.Add(Me.GridEX1)
        Me.grpParent.Controls.Add(Me.ExpandableSplitter1)
        Me.grpParent.Controls.Add(Me.grpChild)
        Me.grpParent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpParent.Location = New System.Drawing.Point(103, 73)
        Me.grpParent.Name = "grpParent"
        Me.grpParent.Size = New System.Drawing.Size(1035, 445)
        Me.grpParent.TabIndex = 4
        Me.grpParent.TabStop = False
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ExpandableSplitter1.ExpandableControl = Me.grpChild
        Me.ExpandableSplitter1.ExpandFillColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.ExpandLineColor = System.Drawing.SystemColors.ControlText
        Me.ExpandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.GripDarkColor = System.Drawing.SystemColors.ControlText
        Me.ExpandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ExpandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2
        Me.ExpandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.ExpandableSplitter1.HotExpandFillColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.HotExpandLineColor = System.Drawing.SystemColors.ControlText
        Me.ExpandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.HotGripDarkColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ExpandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(3, 258)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(1029, 3)
        Me.ExpandableSplitter1.TabIndex = 6
        Me.ExpandableSplitter1.TabStop = False
        '
        'grpChild
        '
        Me.grpChild.Controls.Add(Me.GridEX2)
        Me.grpChild.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpChild.Location = New System.Drawing.Point(3, 261)
        Me.grpChild.Name = "grpChild"
        Me.grpChild.Size = New System.Drawing.Size(1029, 181)
        Me.grpChild.TabIndex = 5
        Me.grpChild.TabStop = False
        '
        'GridEX2
        '
        Me.GridEX2.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.AutoEdit = True
        Me.GridEX2.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX2.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX2.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX2_DesignTimeLayout.LayoutString = resources.GetString("GridEX2_DesignTimeLayout.LayoutString")
        Me.GridEX2.DesignTimeLayout = GridEX2_DesignTimeLayout
        Me.GridEX2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX2.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX2.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX2.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX2.GroupByBoxVisible = False
        Me.GridEX2.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX2.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX2.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX2.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.Location = New System.Drawing.Point(3, 16)
        Me.GridEX2.Name = "GridEX2"
        Me.GridEX2.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX2.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX2.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX2.Size = New System.Drawing.Size(1023, 162)
        Me.GridEX2.TabIndex = 4
        Me.GridEX2.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX2.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX2.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX2.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ExpandableSplitter2
        '
        Me.ExpandableSplitter2.BackColor2 = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter2.ExpandableControl = Me.ItemPanel1
        Me.ExpandableSplitter2.ExpandFillColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter2.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter2.ExpandLineColor = System.Drawing.SystemColors.ControlText
        Me.ExpandableSplitter2.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter2.GripDarkColor = System.Drawing.SystemColors.ControlText
        Me.ExpandableSplitter2.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter2.GripLightColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ExpandableSplitter2.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter2.HotBackColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter2.HotBackColor2 = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter2.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2
        Me.ExpandableSplitter2.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.ExpandableSplitter2.HotExpandFillColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter2.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter2.HotExpandLineColor = System.Drawing.SystemColors.ControlText
        Me.ExpandableSplitter2.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter2.HotGripDarkColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter2.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter2.HotGripLightColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ExpandableSplitter2.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter2.Location = New System.Drawing.Point(100, 0)
        Me.ExpandableSplitter2.Name = "ExpandableSplitter2"
        Me.ExpandableSplitter2.Size = New System.Drawing.Size(3, 518)
        Me.ExpandableSplitter2.TabIndex = 5
        Me.ExpandableSplitter2.TabStop = False
        '
        'SalesProgramReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.Controls.Add(Me.grpParent)
        Me.Controls.Add(Me.grpRangeDate)
        Me.Controls.Add(Me.ExpandableSplitter2)
        Me.Controls.Add(Me.ItemPanel1)
        Me.Name = "SalesProgramReport"
        Me.Size = New System.Drawing.Size(1138, 518)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRangeDate.ResumeLayout(False)
        Me.grpRangeDate.PerformLayout()
        Me.grpParent.ResumeLayout(False)
        Me.grpChild.ResumeLayout(False)
        CType(Me.GridEX2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Private WithEvents btnCPD As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPKPP As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnDK As DevComponents.DotNetBar.ButtonItem
    Private WithEvents grpRangeDate As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicfrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents grpParent As System.Windows.Forms.GroupBox
    Private WithEvents chkFilter As System.Windows.Forms.CheckBox
    Friend WithEvents btnApplyRange As Janus.Windows.EditControls.UIButton
    Private WithEvents btnCPRD As DevComponents.DotNetBar.ButtonItem
    Private WithEvents ExpandableSplitter2 As DevComponents.DotNetBar.ExpandableSplitter
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents GridEX2 As Janus.Windows.GridEX.GridEX
    Friend WithEvents LabelItem1 As DevComponents.DotNetBar.LabelItem
    Private WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Private WithEvents grpChild As System.Windows.Forms.GroupBox
    Friend WithEvents btnSalesbyPO As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPMRT As DevComponents.DotNetBar.ButtonItem

End Class
