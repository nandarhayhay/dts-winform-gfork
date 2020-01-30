<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ThirdParty_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ThirdParty_Report))
        Dim grdMain_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.grpRangeDate = New Janus.Windows.EditControls.UIGroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnApplyRange = New Janus.Windows.EditControls.UIButton
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicfrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdMain = New Janus.Windows.GridEX.GridEX
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.XpGradientPanel1.SuspendLayout()
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRangeDate.SuspendLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.grpRangeDate)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpGradientPanel1.EndColor = System.Drawing.SystemColors.MenuBar
        Me.XpGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(809, 59)
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
        Me.grpRangeDate.Location = New System.Drawing.Point(172, 3)
        Me.grpRangeDate.Name = "grpRangeDate"
        Me.grpRangeDate.Size = New System.Drawing.Size(576, 53)
        Me.grpRangeDate.TabIndex = 0
        Me.grpRangeDate.Text = "Range Data by SPPB_DATE"
        Me.grpRangeDate.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(270, 25)
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
        Me.btnApplyRange.Location = New System.Drawing.Point(488, 21)
        Me.btnApplyRange.Name = "btnApplyRange"
        Me.btnApplyRange.Office2007CustomColor = System.Drawing.Color.Transparent
        Me.btnApplyRange.Size = New System.Drawing.Size(80, 20)
        Me.btnApplyRange.TabIndex = 9
        Me.btnApplyRange.Text = "Aply Filter"
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
        Me.dtPicUntil.Location = New System.Drawing.Point(308, 21)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(174, 20)
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
        Me.dtPicfrom.Location = New System.Drawing.Point(57, 21)
        Me.dtPicfrom.Name = "dtPicfrom"
        Me.dtPicfrom.ShowTodayButton = False
        Me.dtPicfrom.Size = New System.Drawing.Size(194, 20)
        Me.dtPicfrom.TabIndex = 7
        Me.dtPicfrom.Value = New Date(2009, 4, 3, 0, 0, 0, 0)
        Me.dtPicfrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2003
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(14, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "From"
        '
        'grdMain
        '
        Me.grdMain.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdMain.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdMain.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdMain.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdMain_DesignTimeLayout.LayoutString = resources.GetString("grdMain_DesignTimeLayout.LayoutString")
        Me.grdMain.DesignTimeLayout = grdMain_DesignTimeLayout
        Me.grdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdMain.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdMain.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdMain.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.grdMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdMain.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdMain.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdMain.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdMain.ImageList = Me.ImageList1
        Me.grdMain.Location = New System.Drawing.Point(0, 59)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdMain.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdMain.RecordNavigator = True
        Me.grdMain.Size = New System.Drawing.Size(809, 466)
        Me.grdMain.TabIndex = 2
        Me.grdMain.TableHeaderFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdMain.TableHeaderFormatStyle.ForeColor = System.Drawing.Color.Maroon
        Me.grdMain.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdMain.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdMain.WatermarkImage.Image = CType(resources.GetObject("grdMain.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdMain.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
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
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Timer2
        '
        Me.Timer2.Interval = 800
        '
        'ThirdParty_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grdMain)
        Me.Controls.Add(Me.XpGradientPanel1)
        Me.Name = "ThirdParty_Report"
        Me.Size = New System.Drawing.Size(809, 525)
        Me.XpGradientPanel1.ResumeLayout(False)
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRangeDate.ResumeLayout(False)
        Me.grpRangeDate.PerformLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents btnApplyRange As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicfrom As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents grdMain As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpRangeDate As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer

End Class
