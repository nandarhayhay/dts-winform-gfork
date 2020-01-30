<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Manager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Manager))
        Me.btnGoLast = New Janus.Windows.EditControls.UIButton
        Me.btnNext = New Janus.Windows.EditControls.UIButton
        Me.btnGoPrevios = New Janus.Windows.EditControls.UIButton
        Me.XpGradientPanel2 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.lblPosition = New System.Windows.Forms.Label
        Me.lblResult = New System.Windows.Forms.Label
        Me.btnGoFirst = New Janus.Windows.EditControls.UIButton
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.grpDate = New Janus.Windows.EditControls.UIGroupBox
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.GrpFrom = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbFromDate = New System.Windows.Forms.ComboBox
        Me.cmbFromMonth = New System.Windows.Forms.ComboBox
        Me.cmbFromYear = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.cmbUntilDate = New System.Windows.Forms.ComboBox
        Me.cmbUntilMonth = New System.Windows.Forms.ComboBox
        Me.cmbUntilYear = New System.Windows.Forms.ComboBox
        Me.grpSearchData = New Janus.Windows.EditControls.UIGroupBox
        Me.cbPageSize = New System.Windows.Forms.ComboBox
        Me.cbCategory = New System.Windows.Forms.ComboBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtMaxRecord = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.label4 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.XpGradientPanel2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDate.SuspendLayout()
        Me.XpGradientPanel1.SuspendLayout()
        Me.GrpFrom.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grpSearchData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSearchData.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnGoLast
        '
        Me.btnGoLast.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnGoLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoLast.Location = New System.Drawing.Point(320, 7)
        Me.btnGoLast.Name = "btnGoLast"
        Me.btnGoLast.Size = New System.Drawing.Size(26, 23)
        Me.btnGoLast.TabIndex = 5
        Me.btnGoLast.Text = ">>"
        Me.btnGoLast.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnNext
        '
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(289, 7)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(26, 23)
        Me.btnNext.TabIndex = 4
        Me.btnNext.Text = ">"
        Me.btnNext.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnGoPrevios
        '
        Me.btnGoPrevios.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnGoPrevios.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoPrevios.Location = New System.Drawing.Point(255, 7)
        Me.btnGoPrevios.Name = "btnGoPrevios"
        Me.btnGoPrevios.Size = New System.Drawing.Size(26, 23)
        Me.btnGoPrevios.TabIndex = 3
        Me.btnGoPrevios.Text = "<"
        Me.btnGoPrevios.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'XpGradientPanel2
        '
        Me.XpGradientPanel2.Controls.Add(Me.lblPosition)
        Me.XpGradientPanel2.Controls.Add(Me.lblResult)
        Me.XpGradientPanel2.Controls.Add(Me.btnGoLast)
        Me.XpGradientPanel2.Controls.Add(Me.btnNext)
        Me.XpGradientPanel2.Controls.Add(Me.btnGoPrevios)
        Me.XpGradientPanel2.Controls.Add(Me.btnGoFirst)
        Me.XpGradientPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.XpGradientPanel2.Location = New System.Drawing.Point(0, 461)
        Me.XpGradientPanel2.Name = "XpGradientPanel2"
        Me.XpGradientPanel2.Size = New System.Drawing.Size(729, 36)
        Me.XpGradientPanel2.TabIndex = 17
        '
        'lblPosition
        '
        Me.lblPosition.BackColor = System.Drawing.Color.Black
        Me.lblPosition.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblPosition.Location = New System.Drawing.Point(354, 7)
        Me.lblPosition.Name = "lblPosition"
        Me.lblPosition.Size = New System.Drawing.Size(180, 23)
        Me.lblPosition.TabIndex = 7
        '
        'lblResult
        '
        Me.lblResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblResult.BackColor = System.Drawing.Color.Black
        Me.lblResult.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblResult.Location = New System.Drawing.Point(540, 7)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(180, 23)
        Me.lblResult.TabIndex = 6
        '
        'btnGoFirst
        '
        Me.btnGoFirst.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnGoFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoFirst.Location = New System.Drawing.Point(222, 7)
        Me.btnGoFirst.Name = "btnGoFirst"
        Me.btnGoFirst.Size = New System.Drawing.Size(26, 23)
        Me.btnGoFirst.TabIndex = 2
        Me.btnGoFirst.Text = "<<"
        Me.btnGoFirst.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.GridLines = Janus.Windows.GridEX.GridLines.Horizontal
        Me.GridEX1.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.GridEX1.Location = New System.Drawing.Point(206, 83)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(523, 378)
        Me.GridEX1.TabIndex = 18
        Me.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.ThemedAreas = Janus.Windows.GridEX.ThemedArea.None
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        '
        'grpDate
        '
        Me.grpDate.BackColor = System.Drawing.Color.Transparent
        Me.grpDate.Controls.Add(Me.XpGradientPanel1)
        Me.grpDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.grpDate.Location = New System.Drawing.Point(0, 0)
        Me.grpDate.Name = "grpDate"
        Me.grpDate.Size = New System.Drawing.Size(199, 461)
        Me.grpDate.TabIndex = 19
        Me.grpDate.Text = "PO Date"
        Me.grpDate.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.GrpFrom)
        Me.XpGradientPanel1.Controls.Add(Me.GroupBox1)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XpGradientPanel1.EndColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.XpGradientPanel1.Location = New System.Drawing.Point(3, 16)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(193, 442)
        Me.XpGradientPanel1.StartColor = System.Drawing.SystemColors.MenuBar
        Me.XpGradientPanel1.TabIndex = 6
        '
        'GrpFrom
        '
        Me.GrpFrom.Controls.Add(Me.Label7)
        Me.GrpFrom.Controls.Add(Me.Label2)
        Me.GrpFrom.Controls.Add(Me.Label1)
        Me.GrpFrom.Controls.Add(Me.cmbFromDate)
        Me.GrpFrom.Controls.Add(Me.cmbFromMonth)
        Me.GrpFrom.Controls.Add(Me.cmbFromYear)
        Me.GrpFrom.Location = New System.Drawing.Point(3, 3)
        Me.GrpFrom.Name = "GrpFrom"
        Me.GrpFrom.Size = New System.Drawing.Size(192, 76)
        Me.GrpFrom.TabIndex = 4
        Me.GrpFrom.TabStop = False
        Me.GrpFrom.Text = "From "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(140, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(81, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Month"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Year"
        '
        'cmbFromDate
        '
        Me.cmbFromDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFromDate.FormattingEnabled = True
        Me.cmbFromDate.Location = New System.Drawing.Point(137, 33)
        Me.cmbFromDate.MaxDropDownItems = 32
        Me.cmbFromDate.Name = "cmbFromDate"
        Me.cmbFromDate.Size = New System.Drawing.Size(49, 21)
        Me.cmbFromDate.TabIndex = 2
        '
        'cmbFromMonth
        '
        Me.cmbFromMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFromMonth.FormattingEnabled = True
        Me.cmbFromMonth.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.cmbFromMonth.Location = New System.Drawing.Point(78, 33)
        Me.cmbFromMonth.MaxDropDownItems = 12
        Me.cmbFromMonth.Name = "cmbFromMonth"
        Me.cmbFromMonth.Size = New System.Drawing.Size(53, 21)
        Me.cmbFromMonth.TabIndex = 1
        '
        'cmbFromYear
        '
        Me.cmbFromYear.FormattingEnabled = True
        Me.cmbFromYear.Location = New System.Drawing.Point(3, 33)
        Me.cmbFromYear.MaxDropDownItems = 32
        Me.cmbFromYear.Name = "cmbFromYear"
        Me.cmbFromYear.Size = New System.Drawing.Size(69, 21)
        Me.cmbFromYear.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cmbUntilDate)
        Me.GroupBox1.Controls.Add(Me.cmbUntilMonth)
        Me.GroupBox1.Controls.Add(Me.cmbUntilYear)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 84)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(193, 77)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Until"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(141, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Date"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(82, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Month"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(29, 13)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "Year"
        '
        'cmbUntilDate
        '
        Me.cmbUntilDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUntilDate.FormattingEnabled = True
        Me.cmbUntilDate.Location = New System.Drawing.Point(138, 33)
        Me.cmbUntilDate.MaxDropDownItems = 32
        Me.cmbUntilDate.Name = "cmbUntilDate"
        Me.cmbUntilDate.Size = New System.Drawing.Size(49, 21)
        Me.cmbUntilDate.TabIndex = 2
        '
        'cmbUntilMonth
        '
        Me.cmbUntilMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUntilMonth.FormattingEnabled = True
        Me.cmbUntilMonth.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.cmbUntilMonth.Location = New System.Drawing.Point(79, 33)
        Me.cmbUntilMonth.MaxDropDownItems = 12
        Me.cmbUntilMonth.Name = "cmbUntilMonth"
        Me.cmbUntilMonth.Size = New System.Drawing.Size(53, 21)
        Me.cmbUntilMonth.TabIndex = 1
        '
        'cmbUntilYear
        '
        Me.cmbUntilYear.FormattingEnabled = True
        Me.cmbUntilYear.Location = New System.Drawing.Point(4, 33)
        Me.cmbUntilYear.MaxDropDownItems = 32
        Me.cmbUntilYear.Name = "cmbUntilYear"
        Me.cmbUntilYear.Size = New System.Drawing.Size(69, 21)
        Me.cmbUntilYear.TabIndex = 0
        '
        'grpSearchData
        '
        Me.grpSearchData.Controls.Add(Me.cbPageSize)
        Me.grpSearchData.Controls.Add(Me.cbCategory)
        Me.grpSearchData.Controls.Add(Me.btnSearch)
        Me.grpSearchData.Controls.Add(Me.txtMaxRecord)
        Me.grpSearchData.Controls.Add(Me.label4)
        Me.grpSearchData.Controls.Add(Me.label3)
        Me.grpSearchData.Controls.Add(Me.Label5)
        Me.grpSearchData.Controls.Add(Me.txtSearch)
        Me.grpSearchData.Controls.Add(Me.Label6)
        Me.grpSearchData.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpSearchData.Location = New System.Drawing.Point(206, 0)
        Me.grpSearchData.Name = "grpSearchData"
        Me.grpSearchData.Size = New System.Drawing.Size(523, 83)
        Me.grpSearchData.TabIndex = 20
        Me.grpSearchData.Text = "Search Data"
        Me.grpSearchData.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'cbPageSize
        '
        Me.cbPageSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPageSize.FormattingEnabled = True
        Me.cbPageSize.Items.AddRange(New Object() {"10", "20", "30", "40", "50", "100", "200", "300", "400", "500", "1000"})
        Me.cbPageSize.Location = New System.Drawing.Point(439, 49)
        Me.cbPageSize.MaxDropDownItems = 32
        Me.cbPageSize.Name = "cbPageSize"
        Me.cbPageSize.Size = New System.Drawing.Size(75, 21)
        Me.cbPageSize.TabIndex = 25
        '
        'cbCategory
        '
        Me.cbCategory.FormattingEnabled = True
        Me.cbCategory.Location = New System.Drawing.Point(218, 18)
        Me.cbCategory.Name = "cbCategory"
        Me.cbCategory.Size = New System.Drawing.Size(137, 21)
        Me.cbCategory.TabIndex = 24
        '
        'btnSearch
        '
        Me.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Location = New System.Drawing.Point(53, 46)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 23
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtMaxRecord
        '
        Me.txtMaxRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMaxRecord.Location = New System.Drawing.Point(439, 18)
        Me.txtMaxRecord.Name = "txtMaxRecord"
        Me.txtMaxRecord.Size = New System.Drawing.Size(75, 20)
        Me.txtMaxRecord.TabIndex = 22
        Me.txtMaxRecord.Text = "1.000.000.000"
        Me.txtMaxRecord.Value = 1000000000
        Me.txtMaxRecord.ValueType = Janus.Windows.GridEX.NumericEditValueType.Int32
        Me.txtMaxRecord.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'label4
        '
        Me.label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(380, 52)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(55, 13)
        Me.label4.TabIndex = 19
        Me.label4.Text = "Page Size"
        '
        'label3
        '
        Me.label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(373, 21)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(65, 13)
        Me.label3.TabIndex = 16
        Me.label3.Text = "Max Record"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(192, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(19, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "By"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(53, 19)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(133, 20)
        Me.txtSearch.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Search"
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.ExpandableControl = Me.grpDate
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
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(199, 0)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(7, 461)
        Me.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007
        Me.ExpandableSplitter1.TabIndex = 21
        Me.ExpandableSplitter1.TabStop = False
        '
        'Manager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.grpSearchData)
        Me.Controls.Add(Me.ExpandableSplitter1)
        Me.Controls.Add(Me.grpDate)
        Me.Controls.Add(Me.XpGradientPanel2)
        Me.Name = "Manager"
        Me.Size = New System.Drawing.Size(729, 497)
        Me.XpGradientPanel2.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDate.ResumeLayout(False)
        Me.XpGradientPanel1.ResumeLayout(False)
        Me.GrpFrom.ResumeLayout(False)
        Me.GrpFrom.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grpSearchData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSearchData.ResumeLayout(False)
        Me.grpSearchData.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents XpGradientPanel2 As SteepValley.Windows.Forms.XPGradientPanel
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents grpDate As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents grpSearchData As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents txtMaxRecord As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Private WithEvents GrpFrom As System.Windows.Forms.GroupBox
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbUntilDate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUntilMonth As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUntilYear As System.Windows.Forms.ComboBox
    Friend WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Friend WithEvents cmbFromYear As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFromDate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFromMonth As System.Windows.Forms.ComboBox
    Friend WithEvents lblResult As System.Windows.Forms.Label
    Friend WithEvents lblPosition As System.Windows.Forms.Label
    Friend WithEvents btnGoLast As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnNext As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnGoPrevios As Janus.Windows.EditControls.UIButton
    Friend WithEvents btnGoFirst As Janus.Windows.EditControls.UIButton
    Friend WithEvents cbPageSize As System.Windows.Forms.ComboBox
    Friend WithEvents cbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel

End Class
