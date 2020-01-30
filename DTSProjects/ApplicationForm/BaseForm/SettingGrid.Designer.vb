<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingGrid
    Inherits DevComponents.DotNetBar.Office2007Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingGrid))
        Me.cmbAgregate = New System.Windows.Forms.ComboBox
        Me.cmbColumn = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmbFitColumns = New System.Windows.Forms.ComboBox
        Me.chkOriginMargin = New System.Windows.Forms.CheckBox
        Me.chkExpand = New System.Windows.Forms.CheckBox
        Me.chkRepeatHeader = New System.Windows.Forms.CheckBox
        Me.chkTranslate = New System.Windows.Forms.CheckBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.XpWatermark1 = New SteepValley.Windows.Forms.XPWatermark
        Me.cmbGridLineStyle = New System.Windows.Forms.ComboBox
        Me.cmbGridLine = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.chkkRecordNavigator = New System.Windows.Forms.CheckBox
        Me.chkTotalRow = New System.Windows.Forms.CheckBox
        Me.chkRowHeaders = New System.Windows.Forms.CheckBox
        Me.chkAlternatingColumn = New System.Windows.Forms.CheckBox
        Me.grpPrintingDocument = New Janus.Windows.EditControls.UIGroupBox
        Me.txtFooterDistance = New System.Windows.Forms.TextBox
        Me.txtHeaderDistance = New System.Windows.Forms.TextBox
        Me.txtPageFooterLeft = New System.Windows.Forms.TextBox
        Me.txtPageFooterRight = New System.Windows.Forms.TextBox
        Me.txtPageHeaderLeft = New System.Windows.Forms.TextBox
        Me.txtPageFooterCenter = New System.Windows.Forms.TextBox
        Me.txtPageHeaderRight = New System.Windows.Forms.TextBox
        Me.txtPageHeaderCenter = New System.Windows.Forms.TextBox
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grpLine = New Janus.Windows.EditControls.UIGroupBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.cmbVisualStyle = New System.Windows.Forms.ComboBox
        Me.cmbColorScheme = New System.Windows.Forms.ComboBox
        Me.UiGroupBox2 = New Janus.Windows.EditControls.UIGroupBox
        Me.cmbTotalRowPosition = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.cmbGroupInterval = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.chkGroupbyBox = New System.Windows.Forms.CheckBox
        Me.GroupBox2.SuspendLayout()
        CType(Me.grpPrintingDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPrintingDocument.SuspendLayout()
        CType(Me.grpLine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLine.SuspendLayout()
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbAgregate
        '
        Me.cmbAgregate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgregate.FormattingEnabled = True
        Me.cmbAgregate.Location = New System.Drawing.Point(172, 35)
        Me.cmbAgregate.Name = "cmbAgregate"
        Me.cmbAgregate.Size = New System.Drawing.Size(142, 21)
        Me.cmbAgregate.TabIndex = 3
        '
        'cmbColumn
        '
        Me.cmbColumn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbColumn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColumn.FormattingEnabled = True
        Me.cmbColumn.Location = New System.Drawing.Point(8, 35)
        Me.cmbColumn.Name = "cmbColumn"
        Me.cmbColumn.Size = New System.Drawing.Size(158, 21)
        Me.cmbColumn.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(172, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Agregate Function"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(8, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Column"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.cmbFitColumns)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 174)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(127, 39)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Fit Columns"
        '
        'cmbFitColumns
        '
        Me.cmbFitColumns.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbFitColumns.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFitColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbFitColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFitColumns.FormattingEnabled = True
        Me.cmbFitColumns.Items.AddRange(New Object() {"NoFit", "Zooming", "SizingColumns"})
        Me.cmbFitColumns.Location = New System.Drawing.Point(3, 16)
        Me.cmbFitColumns.Name = "cmbFitColumns"
        Me.cmbFitColumns.Size = New System.Drawing.Size(121, 21)
        Me.cmbFitColumns.TabIndex = 21
        '
        'chkOriginMargin
        '
        Me.chkOriginMargin.AutoSize = True
        Me.chkOriginMargin.BackColor = System.Drawing.Color.Transparent
        Me.chkOriginMargin.Location = New System.Drawing.Point(13, 158)
        Me.chkOriginMargin.Name = "chkOriginMargin"
        Me.chkOriginMargin.Size = New System.Drawing.Size(99, 17)
        Me.chkOriginMargin.TabIndex = 19
        Me.chkOriginMargin.Text = "Origin at margin"
        Me.chkOriginMargin.UseVisualStyleBackColor = False
        '
        'chkExpand
        '
        Me.chkExpand.AutoSize = True
        Me.chkExpand.BackColor = System.Drawing.Color.Transparent
        Me.chkExpand.Location = New System.Drawing.Point(13, 135)
        Me.chkExpand.Name = "chkExpand"
        Me.chkExpand.Size = New System.Drawing.Size(118, 17)
        Me.chkExpand.TabIndex = 18
        Me.chkExpand.Text = "Expand Far Column"
        Me.chkExpand.UseVisualStyleBackColor = False
        '
        'chkRepeatHeader
        '
        Me.chkRepeatHeader.AutoSize = True
        Me.chkRepeatHeader.BackColor = System.Drawing.Color.Transparent
        Me.chkRepeatHeader.Location = New System.Drawing.Point(13, 112)
        Me.chkRepeatHeader.Name = "chkRepeatHeader"
        Me.chkRepeatHeader.Size = New System.Drawing.Size(99, 17)
        Me.chkRepeatHeader.TabIndex = 17
        Me.chkRepeatHeader.Text = "Repeat Header"
        Me.chkRepeatHeader.UseVisualStyleBackColor = False
        '
        'chkTranslate
        '
        Me.chkTranslate.AutoSize = True
        Me.chkTranslate.BackColor = System.Drawing.Color.Transparent
        Me.chkTranslate.Location = New System.Drawing.Point(13, 89)
        Me.chkTranslate.Name = "chkTranslate"
        Me.chkTranslate.Size = New System.Drawing.Size(135, 17)
        Me.chkTranslate.TabIndex = 3
        Me.chkTranslate.Text = "Grid Color Black /white"
        Me.chkTranslate.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(369, 159)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(86, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Page Footer Left"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(144, 158)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(93, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Page Footer Right"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(147, 93)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(99, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Page Footer Center"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(368, 91)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Page Header Left"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(368, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Page Header Right"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(147, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(121, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Page Header Center"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(80, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 33)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Footer Distance"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(10, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 33)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Header Distance"
        '
        'XpWatermark1
        '
        Me.XpWatermark1.BackColor = System.Drawing.Color.Transparent
        Me.XpWatermark1.Gamma = 1.0!
        Me.XpWatermark1.Image = CType(resources.GetObject("XpWatermark1.Image"), System.Drawing.Image)
        Me.XpWatermark1.Location = New System.Drawing.Point(12, 35)
        Me.XpWatermark1.Name = "XpWatermark1"
        Me.XpWatermark1.Opacity = 0.5!
        Me.XpWatermark1.Size = New System.Drawing.Size(78, 87)
        Me.XpWatermark1.TabIndex = 2
        Me.XpWatermark1.TransparentColor.High = System.Drawing.Color.Empty
        Me.XpWatermark1.TransparentColor.Low = System.Drawing.Color.Empty
        '
        'cmbGridLineStyle
        '
        Me.cmbGridLineStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGridLineStyle.FormattingEnabled = True
        Me.cmbGridLineStyle.Location = New System.Drawing.Point(148, 79)
        Me.cmbGridLineStyle.Name = "cmbGridLineStyle"
        Me.cmbGridLineStyle.Size = New System.Drawing.Size(136, 21)
        Me.cmbGridLineStyle.TabIndex = 4
        '
        'cmbGridLine
        '
        Me.cmbGridLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGridLine.FormattingEnabled = True
        Me.cmbGridLine.Location = New System.Drawing.Point(148, 35)
        Me.cmbGridLine.Name = "cmbGridLine"
        Me.cmbGridLine.Size = New System.Drawing.Size(136, 21)
        Me.cmbGridLine.TabIndex = 2
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(145, 61)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(75, 13)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Grid Line Style"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(145, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 13)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Grid Line"
        '
        'chkkRecordNavigator
        '
        Me.chkkRecordNavigator.AutoSize = True
        Me.chkkRecordNavigator.BackColor = System.Drawing.Color.Transparent
        Me.chkkRecordNavigator.Location = New System.Drawing.Point(98, 138)
        Me.chkkRecordNavigator.Name = "chkkRecordNavigator"
        Me.chkkRecordNavigator.Size = New System.Drawing.Size(138, 17)
        Me.chkkRecordNavigator.TabIndex = 4
        Me.chkkRecordNavigator.Text = "Show Record navigator"
        Me.chkkRecordNavigator.UseVisualStyleBackColor = False
        '
        'chkTotalRow
        '
        Me.chkTotalRow.AutoSize = True
        Me.chkTotalRow.BackColor = System.Drawing.Color.Transparent
        Me.chkTotalRow.Location = New System.Drawing.Point(243, 138)
        Me.chkTotalRow.Name = "chkTotalRow"
        Me.chkTotalRow.Size = New System.Drawing.Size(122, 17)
        Me.chkTotalRow.TabIndex = 5
        Me.chkTotalRow.Text = "Show Grid Total row"
        Me.chkTotalRow.UseVisualStyleBackColor = False
        '
        'chkRowHeaders
        '
        Me.chkRowHeaders.AutoSize = True
        Me.chkRowHeaders.BackColor = System.Drawing.Color.Transparent
        Me.chkRowHeaders.Location = New System.Drawing.Point(370, 138)
        Me.chkRowHeaders.Name = "chkRowHeaders"
        Me.chkRowHeaders.Size = New System.Drawing.Size(116, 17)
        Me.chkRowHeaders.TabIndex = 6
        Me.chkRowHeaders.Text = "Show Row Header"
        Me.chkRowHeaders.UseVisualStyleBackColor = False
        '
        'chkAlternatingColumn
        '
        Me.chkAlternatingColumn.AutoSize = True
        Me.chkAlternatingColumn.BackColor = System.Drawing.Color.Transparent
        Me.chkAlternatingColumn.Location = New System.Drawing.Point(490, 138)
        Me.chkAlternatingColumn.Name = "chkAlternatingColumn"
        Me.chkAlternatingColumn.Size = New System.Drawing.Size(119, 17)
        Me.chkAlternatingColumn.TabIndex = 7
        Me.chkAlternatingColumn.Text = "Alternating Columns"
        Me.chkAlternatingColumn.UseVisualStyleBackColor = False
        '
        'grpPrintingDocument
        '
        Me.grpPrintingDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.grpPrintingDocument.Controls.Add(Me.txtFooterDistance)
        Me.grpPrintingDocument.Controls.Add(Me.txtHeaderDistance)
        Me.grpPrintingDocument.Controls.Add(Me.txtPageFooterLeft)
        Me.grpPrintingDocument.Controls.Add(Me.txtPageFooterRight)
        Me.grpPrintingDocument.Controls.Add(Me.txtPageHeaderLeft)
        Me.grpPrintingDocument.Controls.Add(Me.txtPageFooterCenter)
        Me.grpPrintingDocument.Controls.Add(Me.txtPageHeaderRight)
        Me.grpPrintingDocument.Controls.Add(Me.txtPageHeaderCenter)
        Me.grpPrintingDocument.Controls.Add(Me.GroupBox2)
        Me.grpPrintingDocument.Controls.Add(Me.chkOriginMargin)
        Me.grpPrintingDocument.Controls.Add(Me.Label3)
        Me.grpPrintingDocument.Controls.Add(Me.chkExpand)
        Me.grpPrintingDocument.Controls.Add(Me.Label4)
        Me.grpPrintingDocument.Controls.Add(Me.chkRepeatHeader)
        Me.grpPrintingDocument.Controls.Add(Me.Label5)
        Me.grpPrintingDocument.Controls.Add(Me.chkTranslate)
        Me.grpPrintingDocument.Controls.Add(Me.Label6)
        Me.grpPrintingDocument.Controls.Add(Me.Label7)
        Me.grpPrintingDocument.Controls.Add(Me.Label8)
        Me.grpPrintingDocument.Controls.Add(Me.Label9)
        Me.grpPrintingDocument.Controls.Add(Me.Label10)
        Me.grpPrintingDocument.ForeColor = System.Drawing.SystemColors.WindowText
        Me.grpPrintingDocument.ImageIndex = 0
        Me.grpPrintingDocument.ImageList = Me.ImageList1
        Me.grpPrintingDocument.Location = New System.Drawing.Point(97, 161)
        Me.grpPrintingDocument.Name = "grpPrintingDocument"
        Me.grpPrintingDocument.Size = New System.Drawing.Size(619, 228)
        Me.grpPrintingDocument.TabIndex = 8
        Me.grpPrintingDocument.Text = "Grid Printing Document"
        Me.grpPrintingDocument.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtFooterDistance
        '
        Me.txtFooterDistance.Location = New System.Drawing.Point(75, 59)
        Me.txtFooterDistance.Name = "txtFooterDistance"
        Me.txtFooterDistance.Size = New System.Drawing.Size(51, 20)
        Me.txtFooterDistance.TabIndex = 28
        '
        'txtHeaderDistance
        '
        Me.txtHeaderDistance.Location = New System.Drawing.Point(10, 59)
        Me.txtHeaderDistance.Name = "txtHeaderDistance"
        Me.txtHeaderDistance.Size = New System.Drawing.Size(51, 20)
        Me.txtHeaderDistance.TabIndex = 27
        '
        'txtPageFooterLeft
        '
        Me.txtPageFooterLeft.Location = New System.Drawing.Point(371, 174)
        Me.txtPageFooterLeft.Multiline = True
        Me.txtPageFooterLeft.Name = "txtPageFooterLeft"
        Me.txtPageFooterLeft.Size = New System.Drawing.Size(234, 48)
        Me.txtPageFooterLeft.TabIndex = 26
        '
        'txtPageFooterRight
        '
        Me.txtPageFooterRight.Location = New System.Drawing.Point(148, 172)
        Me.txtPageFooterRight.Multiline = True
        Me.txtPageFooterRight.Name = "txtPageFooterRight"
        Me.txtPageFooterRight.Size = New System.Drawing.Size(214, 50)
        Me.txtPageFooterRight.TabIndex = 25
        '
        'txtPageHeaderLeft
        '
        Me.txtPageHeaderLeft.Location = New System.Drawing.Point(371, 107)
        Me.txtPageHeaderLeft.Multiline = True
        Me.txtPageHeaderLeft.Name = "txtPageHeaderLeft"
        Me.txtPageHeaderLeft.Size = New System.Drawing.Size(234, 49)
        Me.txtPageHeaderLeft.TabIndex = 24
        '
        'txtPageFooterCenter
        '
        Me.txtPageFooterCenter.Location = New System.Drawing.Point(148, 107)
        Me.txtPageFooterCenter.Multiline = True
        Me.txtPageFooterCenter.Name = "txtPageFooterCenter"
        Me.txtPageFooterCenter.Size = New System.Drawing.Size(214, 50)
        Me.txtPageFooterCenter.TabIndex = 23
        '
        'txtPageHeaderRight
        '
        Me.txtPageHeaderRight.Location = New System.Drawing.Point(371, 38)
        Me.txtPageHeaderRight.Multiline = True
        Me.txtPageHeaderRight.Name = "txtPageHeaderRight"
        Me.txtPageHeaderRight.Size = New System.Drawing.Size(234, 50)
        Me.txtPageHeaderRight.TabIndex = 22
        '
        'txtPageHeaderCenter
        '
        Me.txtPageHeaderCenter.Location = New System.Drawing.Point(150, 40)
        Me.txtPageHeaderCenter.Multiline = True
        Me.txtPageHeaderCenter.Name = "txtPageHeaderCenter"
        Me.txtPageHeaderCenter.Size = New System.Drawing.Size(212, 50)
        Me.txtPageHeaderCenter.TabIndex = 21
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Preview.ico")
        '
        'grpLine
        '
        Me.grpLine.Controls.Add(Me.Label16)
        Me.grpLine.Controls.Add(Me.Label15)
        Me.grpLine.Controls.Add(Me.cmbVisualStyle)
        Me.grpLine.Controls.Add(Me.cmbColorScheme)
        Me.grpLine.Controls.Add(Me.cmbGridLineStyle)
        Me.grpLine.Controls.Add(Me.Label11)
        Me.grpLine.Controls.Add(Me.Label12)
        Me.grpLine.Controls.Add(Me.cmbGridLine)
        Me.grpLine.ForeColor = System.Drawing.SystemColors.WindowText
        Me.grpLine.Location = New System.Drawing.Point(424, 28)
        Me.grpLine.Name = "grpLine"
        Me.grpLine.Size = New System.Drawing.Size(290, 105)
        Me.grpLine.TabIndex = 9
        Me.grpLine.Text = "Style"
        Me.grpLine.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(4, 61)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(73, 13)
        Me.Label16.TabIndex = 8
        Me.Label16.Text = "Color Scheme"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(3, 18)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(61, 13)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "Visual Style"
        '
        'cmbVisualStyle
        '
        Me.cmbVisualStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVisualStyle.FormattingEnabled = True
        Me.cmbVisualStyle.Location = New System.Drawing.Point(6, 35)
        Me.cmbVisualStyle.Name = "cmbVisualStyle"
        Me.cmbVisualStyle.Size = New System.Drawing.Size(136, 21)
        Me.cmbVisualStyle.TabIndex = 6
        '
        'cmbColorScheme
        '
        Me.cmbColorScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColorScheme.FormattingEnabled = True
        Me.cmbColorScheme.Location = New System.Drawing.Point(6, 78)
        Me.cmbColorScheme.Name = "cmbColorScheme"
        Me.cmbColorScheme.Size = New System.Drawing.Size(136, 21)
        Me.cmbColorScheme.TabIndex = 5
        '
        'UiGroupBox2
        '
        Me.UiGroupBox2.Controls.Add(Me.cmbTotalRowPosition)
        Me.UiGroupBox2.Controls.Add(Me.Label14)
        Me.UiGroupBox2.Controls.Add(Me.cmbGroupInterval)
        Me.UiGroupBox2.Controls.Add(Me.Label13)
        Me.UiGroupBox2.Controls.Add(Me.cmbAgregate)
        Me.UiGroupBox2.Controls.Add(Me.cmbColumn)
        Me.UiGroupBox2.Controls.Add(Me.Label1)
        Me.UiGroupBox2.Controls.Add(Me.Label2)
        Me.UiGroupBox2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.UiGroupBox2.Location = New System.Drawing.Point(99, 27)
        Me.UiGroupBox2.Name = "UiGroupBox2"
        Me.UiGroupBox2.Size = New System.Drawing.Size(319, 105)
        Me.UiGroupBox2.TabIndex = 10
        Me.UiGroupBox2.Text = "grid Column"
        Me.UiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'cmbTotalRowPosition
        '
        Me.cmbTotalRowPosition.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbTotalRowPosition.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbTotalRowPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTotalRowPosition.FormattingEnabled = True
        Me.cmbTotalRowPosition.Location = New System.Drawing.Point(9, 78)
        Me.cmbTotalRowPosition.Name = "cmbTotalRowPosition"
        Me.cmbTotalRowPosition.Size = New System.Drawing.Size(157, 21)
        Me.cmbTotalRowPosition.TabIndex = 13
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 62)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(96, 13)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Total Row Position"
        '
        'cmbGroupInterval
        '
        Me.cmbGroupInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGroupInterval.FormattingEnabled = True
        Me.cmbGroupInterval.Location = New System.Drawing.Point(172, 78)
        Me.cmbGroupInterval.Name = "cmbGroupInterval"
        Me.cmbGroupInterval.Size = New System.Drawing.Size(141, 21)
        Me.cmbGroupInterval.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(170, 62)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(111, 13)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Default Group Interval"
        '
        'chkGroupbyBox
        '
        Me.chkGroupbyBox.AutoSize = True
        Me.chkGroupbyBox.Location = New System.Drawing.Point(618, 138)
        Me.chkGroupbyBox.Name = "chkGroupbyBox"
        Me.chkGroupbyBox.Size = New System.Drawing.Size(90, 17)
        Me.chkGroupbyBox.TabIndex = 11
        Me.chkGroupbyBox.Text = "Group By box"
        Me.chkGroupbyBox.UseVisualStyleBackColor = True
        '
        'SettingGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(726, 400)
        Me.Controls.Add(Me.chkGroupbyBox)
        Me.Controls.Add(Me.UiGroupBox2)
        Me.Controls.Add(Me.grpLine)
        Me.Controls.Add(Me.grpPrintingDocument)
        Me.Controls.Add(Me.chkAlternatingColumn)
        Me.Controls.Add(Me.chkRowHeaders)
        Me.Controls.Add(Me.chkTotalRow)
        Me.Controls.Add(Me.chkkRecordNavigator)
        Me.Controls.Add(Me.XpWatermark1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SettingGrid"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Setting Personal Grid"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grpPrintingDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPrintingDocument.ResumeLayout(False)
        Me.grpPrintingDocument.PerformLayout()
        CType(Me.grpLine, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLine.ResumeLayout(False)
        Me.grpLine.PerformLayout()
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox2.ResumeLayout(False)
        Me.UiGroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkOriginMargin As System.Windows.Forms.CheckBox
    Friend WithEvents chkExpand As System.Windows.Forms.CheckBox
    Friend WithEvents chkRepeatHeader As System.Windows.Forms.CheckBox
    Friend WithEvents chkTranslate As System.Windows.Forms.CheckBox
    Friend WithEvents cmbFitColumns As System.Windows.Forms.ComboBox
    Friend WithEvents chkkRecordNavigator As System.Windows.Forms.CheckBox
    Friend WithEvents chkTotalRow As System.Windows.Forms.CheckBox
    Friend WithEvents chkRowHeaders As System.Windows.Forms.CheckBox
    Friend WithEvents chkAlternatingColumn As System.Windows.Forms.CheckBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents XpWatermark1 As SteepValley.Windows.Forms.XPWatermark
    Private WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Private WithEvents cmbAgregate As System.Windows.Forms.ComboBox
    Private WithEvents cmbColumn As System.Windows.Forms.ComboBox
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents cmbGridLineStyle As System.Windows.Forms.ComboBox
    Private WithEvents cmbGridLine As System.Windows.Forms.ComboBox
    Private WithEvents grpPrintingDocument As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents UiGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents cmbGroupInterval As System.Windows.Forms.ComboBox
    Private WithEvents grpLine As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents txtPageFooterLeft As System.Windows.Forms.TextBox
    Friend WithEvents txtPageFooterRight As System.Windows.Forms.TextBox
    Friend WithEvents txtPageHeaderLeft As System.Windows.Forms.TextBox
    Friend WithEvents txtPageHeaderRight As System.Windows.Forms.TextBox
    Friend WithEvents txtPageHeaderCenter As System.Windows.Forms.TextBox
    Private WithEvents txtPageFooterCenter As System.Windows.Forms.TextBox
    Friend WithEvents txtFooterDistance As System.Windows.Forms.TextBox
    Friend WithEvents txtHeaderDistance As System.Windows.Forms.TextBox
    Private WithEvents chkGroupbyBox As System.Windows.Forms.CheckBox
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbTotalRowPosition As System.Windows.Forms.ComboBox
    Private WithEvents cmbColorScheme As System.Windows.Forms.ComboBox
    Private WithEvents cmbVisualStyle As System.Windows.Forms.ComboBox
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
End Class
