<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Accrue
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
        Dim chkDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Accrue))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.grpTypeOfFlag = New Janus.Windows.EditControls.UIGroupBox
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.grpFlag = New Janus.Windows.EditControls.UIGroupBox
        Me.rdbQuarter = New System.Windows.Forms.RadioButton
        Me.rdbSemester = New System.Windows.Forms.RadioButton
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.UiGroupBox2 = New Janus.Windows.EditControls.UIGroupBox
        Me.chkDistributor = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.btnSearchDistributor = New DTSProjects.ButtonSearch
        Me.btnApplyFilter = New Janus.Windows.EditControls.UIButton
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label4 = New System.Windows.Forms.Label
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.grpTypeOfFlag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        CType(Me.grpFlag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFlag.SuspendLayout()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpTypeOfFlag
        '
        Me.grpTypeOfFlag.Location = New System.Drawing.Point(3, 3)
        Me.grpTypeOfFlag.Name = "grpTypeOfFlag"
        Me.grpTypeOfFlag.Size = New System.Drawing.Size(863, 55)
        Me.grpTypeOfFlag.TabIndex = 8
        Me.grpTypeOfFlag.Text = "Range date by Agreement"
        Me.grpTypeOfFlag.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.grpFlag)
        Me.PanelEx1.Controls.Add(Me.UiGroupBox1)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(873, 68)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 9
        '
        'grpFlag
        '
        Me.grpFlag.Controls.Add(Me.rdbQuarter)
        Me.grpFlag.Controls.Add(Me.rdbSemester)
        Me.grpFlag.Location = New System.Drawing.Point(9, 5)
        Me.grpFlag.Name = "grpFlag"
        Me.grpFlag.Size = New System.Drawing.Size(86, 53)
        Me.grpFlag.TabIndex = 8
        Me.grpFlag.Text = "Type of Flag"
        Me.grpFlag.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'rdbQuarter
        '
        Me.rdbQuarter.AutoSize = True
        Me.rdbQuarter.Checked = True
        Me.rdbQuarter.Location = New System.Drawing.Point(7, 32)
        Me.rdbQuarter.Name = "rdbQuarter"
        Me.rdbQuarter.Size = New System.Drawing.Size(60, 17)
        Me.rdbQuarter.TabIndex = 1
        Me.rdbQuarter.TabStop = True
        Me.rdbQuarter.Text = "Quarter"
        Me.rdbQuarter.UseVisualStyleBackColor = True
        '
        'rdbSemester
        '
        Me.rdbSemester.AutoSize = True
        Me.rdbSemester.Location = New System.Drawing.Point(7, 14)
        Me.rdbSemester.Name = "rdbSemester"
        Me.rdbSemester.Size = New System.Drawing.Size(69, 17)
        Me.rdbSemester.TabIndex = 0
        Me.rdbSemester.Text = "Semester"
        Me.rdbSemester.UseVisualStyleBackColor = True
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.Controls.Add(Me.UiGroupBox2)
        Me.UiGroupBox1.Controls.Add(Me.btnApplyFilter)
        Me.UiGroupBox1.Controls.Add(Me.dtPicUntil)
        Me.UiGroupBox1.Controls.Add(Me.Label3)
        Me.UiGroupBox1.Controls.Add(Me.dtPicFrom)
        Me.UiGroupBox1.Controls.Add(Me.Label4)
        Me.UiGroupBox1.Location = New System.Drawing.Point(102, 7)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(764, 51)
        Me.UiGroupBox1.TabIndex = 7
        Me.UiGroupBox1.Text = "Range date by Agreement"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'UiGroupBox2
        '
        Me.UiGroupBox2.Controls.Add(Me.chkDistributor)
        Me.UiGroupBox2.Controls.Add(Me.btnSearchDistributor)
        Me.UiGroupBox2.Location = New System.Drawing.Point(446, 8)
        Me.UiGroupBox2.Name = "UiGroupBox2"
        Me.UiGroupBox2.Size = New System.Drawing.Size(222, 37)
        Me.UiGroupBox2.TabIndex = 6
        Me.UiGroupBox2.Text = "Distributor"
        Me.UiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'chkDistributor
        '
        Me.chkDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        chkDistributor_DesignTimeLayout.LayoutString = resources.GetString("chkDistributor_DesignTimeLayout.LayoutString")
        Me.chkDistributor.DesignTimeLayout = chkDistributor_DesignTimeLayout
        Me.chkDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.chkDistributor.Location = New System.Drawing.Point(4, 13)
        Me.chkDistributor.Name = "chkDistributor"
        Me.chkDistributor.SaveSettings = False
        Me.chkDistributor.Size = New System.Drawing.Size(193, 20)
        Me.chkDistributor.TabIndex = 2
        Me.chkDistributor.ValuesDataMember = Nothing
        Me.chkDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnSearchDistributor
        '
        Me.btnSearchDistributor.Location = New System.Drawing.Point(201, 14)
        Me.btnSearchDistributor.Name = "btnSearchDistributor"
        Me.btnSearchDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchDistributor.TabIndex = 1
        '
        'btnApplyFilter
        '
        Me.btnApplyFilter.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnApplyFilter.Icon = CType(resources.GetObject("btnApplyFilter.Icon"), System.Drawing.Icon)
        Me.btnApplyFilter.ImageIndex = 3
        Me.btnApplyFilter.Location = New System.Drawing.Point(674, 21)
        Me.btnApplyFilter.Name = "btnApplyFilter"
        Me.btnApplyFilter.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnApplyFilter.Size = New System.Drawing.Size(83, 20)
        Me.btnApplyFilter.TabIndex = 5
        Me.btnApplyFilter.Text = "Apply Filter"
        Me.btnApplyFilter.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
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
        Me.dtPicUntil.DropDownCalendar.FirstMonth = New Date(2009, 4, 1, 0, 0, 0, 0)
        Me.dtPicUntil.DropDownCalendar.Name = ""
        Me.dtPicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicUntil.Location = New System.Drawing.Point(253, 21)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(189, 20)
        Me.dtPicUntil.TabIndex = 4
        Me.dtPicUntil.Value = New Date(2009, 4, 3, 0, 0, 0, 0)
        Me.dtPicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label3.Location = New System.Drawing.Point(224, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Until"
        '
        'dtPicFrom
        '
        Me.dtPicFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFrom.CustomFormat = "dd MMMM yyyy"
        Me.dtPicFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtPicFrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFrom.DropDownCalendar.FirstMonth = New Date(2009, 4, 1, 0, 0, 0, 0)
        Me.dtPicFrom.DropDownCalendar.Name = ""
        Me.dtPicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicFrom.Location = New System.Drawing.Point(29, 21)
        Me.dtPicFrom.Name = "dtPicFrom"
        Me.dtPicFrom.ShowTodayButton = False
        Me.dtPicFrom.Size = New System.Drawing.Size(189, 20)
        Me.dtPicFrom.TabIndex = 2
        Me.dtPicFrom.Value = New Date(2009, 4, 3, 0, 0, 0, 0)
        Me.dtPicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Location = New System.Drawing.Point(-1, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "From"
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
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 68)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(873, 460)
        Me.GridEX1.TabIndex = 10
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Timer1
        '
        Me.Timer1.Interval = 700
        '
        'Accrue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.PanelEx1)
        Me.Controls.Add(Me.grpTypeOfFlag)
        Me.Name = "Accrue"
        Me.Size = New System.Drawing.Size(873, 528)
        CType(Me.grpTypeOfFlag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.grpFlag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFlag.ResumeLayout(False)
        Me.grpFlag.PerformLayout()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.UiGroupBox1.PerformLayout()
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox2.ResumeLayout(False)
        Me.UiGroupBox2.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents UiGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents btnSearchDistributor As DTSProjects.ButtonSearch
    Private WithEvents grpFlag As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents rdbQuarter As System.Windows.Forms.RadioButton
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents grpTypeOfFlag As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnApplyFilter As Janus.Windows.EditControls.UIButton
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents chkDistributor As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Private WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents rdbSemester As System.Windows.Forms.RadioButton
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo

End Class
