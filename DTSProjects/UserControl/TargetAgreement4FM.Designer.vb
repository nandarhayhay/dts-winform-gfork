<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TargetAgreement4FM
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
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtPicModUntilDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicModFromDate = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.ControlContainerItem1 = New DevComponents.DotNetBar.ControlContainerItem
        Me.cmbDPDtypeItemNufarm = New DevComponents.Editors.ComboItem
        Me.cmbDPDtypeItemRoundup = New DevComponents.Editors.ComboItem
        Me.ControlContainerItem2 = New DevComponents.DotNetBar.ControlContainerItem
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.btnAplyRange = New Janus.Windows.EditControls.UIButton
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbDPDType = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX1_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.Location = New System.Drawing.Point(0, 43)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(772, 497)
        Me.GridEX1.TabIndex = 2
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(199, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "EndDate"
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
        Me.dtPicModUntilDate.Location = New System.Drawing.Point(250, 13)
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
        Me.dtPicModFromDate.Location = New System.Drawing.Point(60, 12)
        Me.dtPicModFromDate.Name = "dtPicModFromDate"
        Me.dtPicModFromDate.Size = New System.Drawing.Size(134, 20)
        Me.dtPicModFromDate.TabIndex = 0
        Me.dtPicModFromDate.Value = New Date(2016, 11, 24, 0, 0, 0, 0)
        Me.dtPicModFromDate.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'ControlContainerItem1
        '
        Me.ControlContainerItem1.AllowItemResize = False
        Me.ControlContainerItem1.Control = Nothing
        Me.ControlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways
        Me.ControlContainerItem1.Name = "ControlContainerItem1"
        '
        'cmbDPDtypeItemNufarm
        '
        Me.cmbDPDtypeItemNufarm.Text = "Nufarm Product"
        '
        'cmbDPDtypeItemRoundup
        '
        Me.cmbDPDtypeItemRoundup.Text = "Roundup"
        '
        'ControlContainerItem2
        '
        Me.ControlContainerItem2.AllowItemResize = False
        Me.ControlContainerItem2.Control = Nothing
        Me.ControlContainerItem2.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways
        Me.ControlContainerItem2.Name = "ControlContainerItem2"
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.btnAplyRange)
        Me.PanelEx1.Controls.Add(Me.UiGroupBox1)
        Me.PanelEx1.Controls.Add(Me.cmbDPDType)
        Me.PanelEx1.Controls.Add(Me.Label1)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(772, 43)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 25
        '
        'btnAplyRange
        '
        Me.btnAplyRange.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAplyRange.ButtonStyle = Janus.Windows.EditControls.ButtonStyle.Button
        Me.btnAplyRange.Location = New System.Drawing.Point(617, 12)
        Me.btnAplyRange.Name = "btnAplyRange"
        Me.btnAplyRange.Size = New System.Drawing.Size(70, 20)
        Me.btnAplyRange.TabIndex = 27
        Me.btnAplyRange.Text = "Apply filter"
        Me.btnAplyRange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.Controls.Add(Me.Label2)
        Me.UiGroupBox1.Controls.Add(Me.dtPicModUntilDate)
        Me.UiGroupBox1.Controls.Add(Me.Label3)
        Me.UiGroupBox1.Controls.Add(Me.dtPicModFromDate)
        Me.UiGroupBox1.Location = New System.Drawing.Point(191, 0)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(420, 39)
        Me.UiGroupBox1.TabIndex = 26
        Me.UiGroupBox1.Text = "DPD Periode"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "StartDate"
        '
        'cmbDPDType
        '
        Me.cmbDPDType.FormattingEnabled = True
        Me.cmbDPDType.Items.AddRange(New Object() {"Roundup 4 Months Periode", "Nufarm 4 Months Periode"})
        Me.cmbDPDType.Location = New System.Drawing.Point(64, 5)
        Me.cmbDPDType.Name = "cmbDPDType"
        Me.cmbDPDType.Size = New System.Drawing.Size(121, 21)
        Me.cmbDPDType.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "DPD type"
        '
        'TargetAgreement4FM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.PanelEx1)
        Me.Name = "TargetAgreement4FM"
        Me.Size = New System.Drawing.Size(772, 540)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.UiGroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ControlContainerItem1 As DevComponents.DotNetBar.ControlContainerItem
    Friend WithEvents ControlContainerItem2 As DevComponents.DotNetBar.ControlContainerItem
    Private WithEvents cmbDPDtypeItemNufarm As DevComponents.Editors.ComboItem
    Private WithEvents cmbDPDtypeItemRoundup As DevComponents.Editors.ComboItem
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents cmbDPDType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents btnAplyRange As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtPicModUntilDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicModFromDate As Janus.Windows.CalendarCombo.CalendarCombo

End Class
