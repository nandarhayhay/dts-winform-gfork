<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Agreement_Target
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Agreement_Target))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.ExpandablePanel1 = New DevComponents.DotNetBar.ExpandablePanel
        Me.btnAplyRange = New Janus.Windows.EditControls.UIButton
        Me.dtPicFilterEnd = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicFilterStart = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtPicModUntilDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicModFromDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnShowAll = New DevComponents.DotNetBar.ButtonItem
        Me.btnRangeDate = New DevComponents.DotNetBar.ButtonItem
        Me.ControlContainerItem1 = New DevComponents.DotNetBar.ControlContainerItem
        Me.ExpandablePanel2 = New DevComponents.DotNetBar.ExpandablePanel
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ExpandablePanel1.SuspendLayout()
        Me.ItemPanel1.SuspendLayout()
        Me.ExpandablePanel2.SuspendLayout()
        Me.SuspendLayout()
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
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 94)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(899, 392)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Timer2
        '
        Me.Timer2.Interval = 800
        '
        'ExpandablePanel1
        '
        Me.ExpandablePanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel1.Controls.Add(Me.btnAplyRange)
        Me.ExpandablePanel1.Controls.Add(Me.dtPicFilterEnd)
        Me.ExpandablePanel1.Controls.Add(Me.dtPicFilterStart)
        Me.ExpandablePanel1.Controls.Add(Me.Label5)
        Me.ExpandablePanel1.Controls.Add(Me.Label1)
        Me.ExpandablePanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ExpandablePanel1.Location = New System.Drawing.Point(0, 0)
        Me.ExpandablePanel1.Name = "ExpandablePanel1"
        Me.ExpandablePanel1.Size = New System.Drawing.Size(899, 45)
        Me.ExpandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.ExpandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel1.Style.GradientAngle = 90
        Me.ExpandablePanel1.TabIndex = 21
        Me.ExpandablePanel1.TitleHeight = 17
        Me.ExpandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal
        Me.ExpandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel1.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel1.TitleText = "Filter Master data periode PKD"
        '
        'btnAplyRange
        '
        Me.btnAplyRange.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAplyRange.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnAplyRange.Location = New System.Drawing.Point(570, 20)
        Me.btnAplyRange.Name = "btnAplyRange"
        Me.btnAplyRange.Size = New System.Drawing.Size(70, 20)
        Me.btnAplyRange.TabIndex = 5
        Me.btnAplyRange.Text = "Apply filter"
        Me.btnAplyRange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'dtPicFilterEnd
        '
        Me.dtPicFilterEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFilterEnd.CustomFormat = "dd MMMM yyyy"
        Me.dtPicFilterEnd.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicFilterEnd.DropDownCalendar.Name = ""
        Me.dtPicFilterEnd.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFilterEnd.Location = New System.Drawing.Point(401, 19)
        Me.dtPicFilterEnd.Name = "dtPicFilterEnd"
        Me.dtPicFilterEnd.ShowTodayButton = False
        Me.dtPicFilterEnd.Size = New System.Drawing.Size(157, 20)
        Me.dtPicFilterEnd.TabIndex = 4
        Me.dtPicFilterEnd.Value = New Date(2014, 1, 7, 0, 0, 0, 0)
        Me.dtPicFilterEnd.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicFilterStart
        '
        Me.dtPicFilterStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFilterStart.CustomFormat = "dd MMMM yyyy"
        Me.dtPicFilterStart.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicFilterStart.DropDownCalendar.Name = ""
        Me.dtPicFilterStart.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFilterStart.Location = New System.Drawing.Point(191, 18)
        Me.dtPicFilterStart.Name = "dtPicFilterStart"
        Me.dtPicFilterStart.ShowTodayButton = False
        Me.dtPicFilterStart.Size = New System.Drawing.Size(157, 20)
        Me.dtPicFilterStart.TabIndex = 3
        Me.dtPicFilterStart.Value = New Date(2014, 1, 7, 0, 0, 0, 0)
        Me.dtPicFilterStart.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(363, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(26, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "End"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(159, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Start"
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
        Me.ItemPanel1.Controls.Add(Me.Label3)
        Me.ItemPanel1.Controls.Add(Me.dtPicModUntilDate)
        Me.ItemPanel1.Controls.Add(Me.dtPicModFromDate)
        Me.ItemPanel1.Controls.Add(Me.Label2)
        Me.ItemPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnShowAll, Me.btnRangeDate, Me.ControlContainerItem1})
        Me.ItemPanel1.Location = New System.Drawing.Point(0, 17)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(899, 32)
        Me.ItemPanel1.TabIndex = 22
        Me.ItemPanel1.Text = "ItemPanel1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(363, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Until"
        '
        'dtPicModUntilDate
        '
        Me.dtPicModUntilDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicModUntilDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicModUntilDate.DropDownCalendar.Name = ""
        Me.dtPicModUntilDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicModUntilDate.Location = New System.Drawing.Point(400, 4)
        Me.dtPicModUntilDate.Name = "dtPicModUntilDate"
        Me.dtPicModUntilDate.Size = New System.Drawing.Size(157, 20)
        Me.dtPicModUntilDate.TabIndex = 1
        Me.dtPicModUntilDate.Value = New Date(2016, 11, 24, 0, 0, 0, 0)
        Me.dtPicModUntilDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicModFromDate
        '
        Me.dtPicModFromDate.CustomFormat = "dd MMMM yyyy"
        Me.dtPicModFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicModFromDate.DropDownCalendar.Name = ""
        Me.dtPicModFromDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicModFromDate.Location = New System.Drawing.Point(190, 4)
        Me.dtPicModFromDate.Name = "dtPicModFromDate"
        Me.dtPicModFromDate.Size = New System.Drawing.Size(157, 20)
        Me.dtPicModFromDate.TabIndex = 0
        Me.dtPicModFromDate.Value = New Date(2016, 11, 24, 0, 0, 0, 0)
        Me.dtPicModFromDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(158, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "From"
        '
        'btnShowAll
        '
        Me.btnShowAll.Name = "btnShowAll"
        Me.btnShowAll.Text = "Show All"
        '
        'btnRangeDate
        '
        Me.btnRangeDate.Name = "btnRangeDate"
        Me.btnRangeDate.Text = "Only Changed Data"
        '
        'ControlContainerItem1
        '
        Me.ControlContainerItem1.AllowItemResize = False
        Me.ControlContainerItem1.Control = Me.Label2
        Me.ControlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways
        Me.ControlContainerItem1.Name = "ControlContainerItem1"
        '
        'ExpandablePanel2
        '
        Me.ExpandablePanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.ExpandablePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ExpandablePanel2.Controls.Add(Me.ItemPanel1)
        Me.ExpandablePanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.ExpandablePanel2.Location = New System.Drawing.Point(0, 45)
        Me.ExpandablePanel2.Name = "ExpandablePanel2"
        Me.ExpandablePanel2.Size = New System.Drawing.Size(899, 49)
        Me.ExpandablePanel2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandablePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.ExpandablePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.ExpandablePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandablePanel2.Style.GradientAngle = 90
        Me.ExpandablePanel2.TabIndex = 23
        Me.ExpandablePanel2.TitleHeight = 17
        Me.ExpandablePanel2.TitleStyle.Alignment = System.Drawing.StringAlignment.Center
        Me.ExpandablePanel2.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandablePanel2.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.ExpandablePanel2.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ExpandablePanel2.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandablePanel2.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.ExpandablePanel2.TitleStyle.GradientAngle = 90
        Me.ExpandablePanel2.TitleText = "Filter Changed Data"
        '
        'Agreement_Target
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.ExpandablePanel2)
        Me.Controls.Add(Me.ExpandablePanel1)
        Me.Name = "Agreement_Target"
        Me.Size = New System.Drawing.Size(899, 486)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ExpandablePanel1.ResumeLayout(False)
        Me.ExpandablePanel1.PerformLayout()
        Me.ItemPanel1.ResumeLayout(False)
        Me.ItemPanel1.PerformLayout()
        Me.ExpandablePanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Private WithEvents ExpandablePanel1 As DevComponents.DotNetBar.ExpandablePanel
    Private WithEvents btnAplyRange As Janus.Windows.EditControls.UIButton
    Private WithEvents dtPicFilterEnd As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents dtPicFilterStart As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Private WithEvents btnShowAll As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRangeDate As DevComponents.DotNetBar.ButtonItem
    Private WithEvents dtPicModFromDate As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents dtPicModUntilDate As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ControlContainerItem1 As DevComponents.DotNetBar.ControlContainerItem
    Friend WithEvents ExpandablePanel2 As DevComponents.DotNetBar.ExpandablePanel

End Class
