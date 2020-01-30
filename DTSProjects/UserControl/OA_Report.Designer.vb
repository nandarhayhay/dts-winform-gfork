<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OA_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OA_Report))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.grpRangedate = New Janus.Windows.EditControls.UIGroupBox
        Me.btnApplyRange = New Janus.Windows.EditControls.UIButton
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.rdbPO = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbOA = New Janus.Windows.EditControls.UIRadioButton
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        CType(Me.grpRangedate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRangedate.SuspendLayout()
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
        Me.GridEX1.CardCaptionPrefix = "DISTRIBUTTOR : "
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 45)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(901, 483)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.TableHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.GridEX1.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.grpRangedate)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(901, 45)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 2
        '
        'grpRangedate
        '
        Me.grpRangedate.Controls.Add(Me.btnApplyRange)
        Me.grpRangedate.Controls.Add(Me.dtPicUntil)
        Me.grpRangedate.Controls.Add(Me.Label2)
        Me.grpRangedate.Controls.Add(Me.dtPicFrom)
        Me.grpRangedate.Controls.Add(Me.Label1)
        Me.grpRangedate.Controls.Add(Me.rdbPO)
        Me.grpRangedate.Controls.Add(Me.rdbOA)
        Me.grpRangedate.Location = New System.Drawing.Point(133, 0)
        Me.grpRangedate.Name = "grpRangedate"
        Me.grpRangedate.Size = New System.Drawing.Size(635, 42)
        Me.grpRangedate.TabIndex = 8
        Me.grpRangedate.Text = "Range date"
        Me.grpRangedate.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'btnApplyRange
        '
        Me.btnApplyRange.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnApplyRange.Icon = CType(resources.GetObject("btnApplyRange.Icon"), System.Drawing.Icon)
        Me.btnApplyRange.ImageIndex = 3
        Me.btnApplyRange.Location = New System.Drawing.Point(537, 14)
        Me.btnApplyRange.Name = "btnApplyRange"
        Me.btnApplyRange.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnApplyRange.Size = New System.Drawing.Size(80, 20)
        Me.btnApplyRange.TabIndex = 5
        Me.btnApplyRange.Text = "Apply Filter"
        Me.btnApplyRange.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'dtPicUntil
        '
        Me.dtPicUntil.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicUntil.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicUntil.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicUntil.DropDownCalendar.Name = ""
        Me.dtPicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        Me.dtPicUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicUntil.Location = New System.Drawing.Point(347, 14)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(184, 20)
        Me.dtPicUntil.TabIndex = 4
        Me.dtPicUntil.Value = New Date(2009, 8, 28, 0, 0, 0, 0)
        Me.dtPicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(308, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Until"
        '
        'dtPicFrom
        '
        Me.dtPicFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicFrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtPicFrom.DropDownCalendar.Name = ""
        Me.dtPicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        Me.dtPicFrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicFrom.Location = New System.Drawing.Point(138, 14)
        Me.dtPicFrom.Name = "dtPicFrom"
        Me.dtPicFrom.ShowTodayButton = False
        Me.dtPicFrom.Size = New System.Drawing.Size(159, 20)
        Me.dtPicFrom.TabIndex = 2
        Me.dtPicFrom.Value = New Date(2009, 8, 28, 0, 0, 0, 0)
        Me.dtPicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(101, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From"
        '
        'rdbPO
        '
        Me.rdbPO.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.rdbPO.Location = New System.Drawing.Point(54, 14)
        Me.rdbPO.Name = "rdbPO"
        Me.rdbPO.Size = New System.Drawing.Size(44, 23)
        Me.rdbPO.TabIndex = 1
        Me.rdbPO.Text = "PO"
        Me.rdbPO.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'rdbOA
        '
        Me.rdbOA.Checked = True
        Me.rdbOA.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.rdbOA.Location = New System.Drawing.Point(10, 14)
        Me.rdbOA.Name = "rdbOA"
        Me.rdbOA.Size = New System.Drawing.Size(38, 23)
        Me.rdbOA.TabIndex = 0
        Me.rdbOA.TabStop = True
        Me.rdbOA.Text = "OA"
        Me.rdbOA.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'OA_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.PanelEx1)
        Me.Name = "OA_Report"
        Me.Size = New System.Drawing.Size(901, 528)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.grpRangedate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRangedate.ResumeLayout(False)
        Me.grpRangedate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents grpRangedate As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents btnApplyRange As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rdbPO As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents rdbOA As Janus.Windows.EditControls.UIRadioButton
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents Timer1 As System.Windows.Forms.Timer

End Class
