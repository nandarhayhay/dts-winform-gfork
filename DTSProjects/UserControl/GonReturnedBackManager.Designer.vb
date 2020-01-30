<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GonReturnedBackManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GonReturnedBackManager))
        Me.XpCaptionPane1 = New SteepValley.Windows.Forms.XPCaptionPane
        Me.dtpicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.btnFilteDate = New Janus.Windows.EditControls.UIButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.xpgFilterDate = New SteepValley.Windows.Forms.XPGradientPanel
        Me.XpCaptionPane1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xpgFilterDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'XpCaptionPane1
        '
        Me.XpCaptionPane1.BackColor = System.Drawing.Color.Transparent
        Me.XpCaptionPane1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        '
        '
        Me.XpCaptionPane1.CaptionControl.ActiveGradientLowColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.XpCaptionPane1.CaptionControl.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpCaptionPane1.CaptionControl.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XpCaptionPane1.CaptionControl.InactiveGradientHighColor = System.Drawing.SystemColors.MenuBar
        Me.XpCaptionPane1.CaptionControl.InactiveGradientLowColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.XpCaptionPane1.CaptionControl.InactiveTextColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.XpCaptionPane1.CaptionControl.Location = New System.Drawing.Point(1, 1)
        Me.XpCaptionPane1.CaptionControl.Name = "caption"
        Me.XpCaptionPane1.CaptionControl.Size = New System.Drawing.Size(606, 20)
        Me.XpCaptionPane1.CaptionControl.TabIndex = 0
        Me.XpCaptionPane1.CaptionControl.Text = "Filter Date by Returned Gon Date"
        Me.XpCaptionPane1.CaptionText = "Filter Date by Returned Gon Date"
        Me.XpCaptionPane1.Controls.Add(Me.dtpicFrom)
        Me.XpCaptionPane1.Controls.Add(Me.dtPicUntil)
        Me.XpCaptionPane1.Controls.Add(Me.btnFilteDate)
        Me.XpCaptionPane1.Controls.Add(Me.Label1)
        Me.XpCaptionPane1.Controls.Add(Me.Label2)
        Me.XpCaptionPane1.Location = New System.Drawing.Point(211, 6)
        Me.XpCaptionPane1.Name = "XpCaptionPane1"
        Me.XpCaptionPane1.Padding = New System.Windows.Forms.Padding(1)
        Me.XpCaptionPane1.Size = New System.Drawing.Size(610, 56)
        Me.XpCaptionPane1.TabIndex = 26
        '
        'dtpicFrom
        '
        Me.dtpicFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtpicFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtpicFrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dtpicFrom.DropDownCalendar.Name = ""
        Me.dtpicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtpicFrom.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtpicFrom.Location = New System.Drawing.Point(60, 25)
        Me.dtpicFrom.Name = "dtpicFrom"
        Me.dtpicFrom.ShowTodayButton = False
        Me.dtpicFrom.Size = New System.Drawing.Size(187, 20)
        Me.dtpicFrom.TabIndex = 2
        Me.dtpicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
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
        Me.dtPicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicUntil.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicUntil.Location = New System.Drawing.Point(306, 24)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(187, 20)
        Me.dtPicUntil.TabIndex = 4
        Me.dtPicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'btnFilteDate
        '
        Me.btnFilteDate.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnFilteDate.ImageIndex = 3
        Me.btnFilteDate.Location = New System.Drawing.Point(505, 25)
        Me.btnFilteDate.Name = "btnFilteDate"
        Me.btnFilteDate.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnFilteDate.Size = New System.Drawing.Size(85, 20)
        Me.btnFilteDate.TabIndex = 2
        Me.btnFilteDate.Text = "Apply Filter"
        Me.btnFilteDate.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "FROM"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(257, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "UNTIL"
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
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
        Me.GridEX1.GridLineColor = System.Drawing.SystemColors.Desktop
        Me.GridEX1.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.Location = New System.Drawing.Point(0, 68)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Size = New System.Drawing.Size(921, 328)
        Me.GridEX1.TabIndex = 29
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'xpgFilterDate
        '
        Me.xpgFilterDate.Controls.Add(Me.XpCaptionPane1)
        Me.xpgFilterDate.Dock = System.Windows.Forms.DockStyle.Top
        Me.xpgFilterDate.EndColor = System.Drawing.SystemColors.MenuBar
        Me.xpgFilterDate.Location = New System.Drawing.Point(0, 0)
        Me.xpgFilterDate.Name = "xpgFilterDate"
        Me.xpgFilterDate.Size = New System.Drawing.Size(921, 68)
        Me.xpgFilterDate.StartColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.xpgFilterDate.TabIndex = 30
        '
        'GonReturnedBackManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.xpgFilterDate)
        Me.Name = "GonReturnedBackManager"
        Me.Size = New System.Drawing.Size(921, 396)
        Me.XpCaptionPane1.ResumeLayout(False)
        Me.XpCaptionPane1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xpgFilterDate.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents btnFilteDate As Janus.Windows.EditControls.UIButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents xpgFilterDate As SteepValley.Windows.Forms.XPGradientPanel
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents XpCaptionPane1 As SteepValley.Windows.Forms.XPCaptionPane
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtpicFrom As Janus.Windows.CalendarCombo.CalendarCombo

End Class
