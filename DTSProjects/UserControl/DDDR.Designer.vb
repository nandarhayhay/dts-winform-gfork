<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DDDR
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
        Dim grpCategoryDiscount_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DDDR))
        Me.grpCategoryDiscount = New Janus.Windows.GridEX.GridEX
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.UiGroupBox2 = New Janus.Windows.EditControls.UIGroupBox
        Me.cmbApplyDiscount = New System.Windows.Forms.ComboBox
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.cmbParams = New System.Windows.Forms.ComboBox
        Me.grpRangeDate = New Janus.Windows.EditControls.UIGroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnApplyRange = New Janus.Windows.EditControls.UIButton
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicfrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.grpCategoryDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XpGradientPanel1.SuspendLayout()
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox2.SuspendLayout()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRangeDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpCategoryDiscount
        '
        Me.grpCategoryDiscount.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grpCategoryDiscount.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grpCategoryDiscount.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grpCategoryDiscount_DesignTimeLayout.LayoutString = resources.GetString("grpCategoryDiscount_DesignTimeLayout.LayoutString")
        Me.grpCategoryDiscount.DesignTimeLayout = grpCategoryDiscount_DesignTimeLayout
        Me.grpCategoryDiscount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCategoryDiscount.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grpCategoryDiscount.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grpCategoryDiscount.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.grpCategoryDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grpCategoryDiscount.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grpCategoryDiscount.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grpCategoryDiscount.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grpCategoryDiscount.ImageList = Me.ImageList1
        Me.grpCategoryDiscount.Location = New System.Drawing.Point(0, 59)
        Me.grpCategoryDiscount.Name = "grpCategoryDiscount"
        Me.grpCategoryDiscount.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grpCategoryDiscount.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grpCategoryDiscount.RecordNavigator = True
        Me.grpCategoryDiscount.Size = New System.Drawing.Size(1263, 401)
        Me.grpCategoryDiscount.TabIndex = 3
        Me.grpCategoryDiscount.TableHeaderFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold)
        Me.grpCategoryDiscount.TableHeaderFormatStyle.ForeColor = System.Drawing.Color.Maroon
        Me.grpCategoryDiscount.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grpCategoryDiscount.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grpCategoryDiscount.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003
        Me.grpCategoryDiscount.WatermarkImage.Image = CType(resources.GetObject("grpCategoryDiscount.WatermarkImage.Image"), System.Drawing.Image)
        Me.grpCategoryDiscount.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
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
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.UiGroupBox2)
        Me.XpGradientPanel1.Controls.Add(Me.UiGroupBox1)
        Me.XpGradientPanel1.Controls.Add(Me.grpRangeDate)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpGradientPanel1.EndColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.XpGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(1263, 59)
        Me.XpGradientPanel1.StartColor = System.Drawing.SystemColors.InactiveCaption
        Me.XpGradientPanel1.TabIndex = 2
        '
        'UiGroupBox2
        '
        Me.UiGroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.UiGroupBox2.Controls.Add(Me.cmbApplyDiscount)
        Me.UiGroupBox2.Location = New System.Drawing.Point(3, 4)
        Me.UiGroupBox2.Name = "UiGroupBox2"
        Me.UiGroupBox2.Size = New System.Drawing.Size(178, 49)
        Me.UiGroupBox2.TabIndex = 2
        Me.UiGroupBox2.Text = "Apply Discount to"
        Me.UiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'cmbApplyDiscount
        '
        Me.cmbApplyDiscount.FormattingEnabled = True
        Me.cmbApplyDiscount.Items.AddRange(New Object() {"CERTAIN_DISTRIBUTORS", "ALL_DISTRIBUTOR"})
        Me.cmbApplyDiscount.Location = New System.Drawing.Point(6, 18)
        Me.cmbApplyDiscount.Name = "cmbApplyDiscount"
        Me.cmbApplyDiscount.Size = New System.Drawing.Size(166, 21)
        Me.cmbApplyDiscount.TabIndex = 0
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UiGroupBox1.Controls.Add(Me.cmbParams)
        Me.UiGroupBox1.Location = New System.Drawing.Point(187, 4)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(295, 50)
        Me.UiGroupBox1.TabIndex = 1
        Me.UiGroupBox1.Text = "Parameterized Range data by"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'cmbParams
        '
        Me.cmbParams.FormattingEnabled = True
        Me.cmbParams.Items.AddRange(New Object() {"APPLY_DATE", "END_DATE", "APPLY_DATE AND END_DATE"})
        Me.cmbParams.Location = New System.Drawing.Point(6, 18)
        Me.cmbParams.Name = "cmbParams"
        Me.cmbParams.Size = New System.Drawing.Size(283, 21)
        Me.cmbParams.TabIndex = 0
        '
        'grpRangeDate
        '
        Me.grpRangeDate.BackColor = System.Drawing.Color.Transparent
        Me.grpRangeDate.Controls.Add(Me.Label2)
        Me.grpRangeDate.Controls.Add(Me.btnApplyRange)
        Me.grpRangeDate.Controls.Add(Me.dtPicUntil)
        Me.grpRangeDate.Controls.Add(Me.dtPicfrom)
        Me.grpRangeDate.Controls.Add(Me.Label1)
        Me.grpRangeDate.Location = New System.Drawing.Point(497, 4)
        Me.grpRangeDate.Name = "grpRangeDate"
        Me.grpRangeDate.Size = New System.Drawing.Size(509, 50)
        Me.grpRangeDate.TabIndex = 0
        Me.grpRangeDate.Text = "Range Data by"
        Me.grpRangeDate.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(220, 23)
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
        Me.btnApplyRange.Location = New System.Drawing.Point(423, 18)
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
        Me.dtPicUntil.Location = New System.Drawing.Point(254, 19)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(157, 20)
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
        Me.dtPicfrom.Location = New System.Drawing.Point(52, 19)
        Me.dtPicfrom.Name = "dtPicfrom"
        Me.dtPicfrom.ShowTodayButton = False
        Me.dtPicfrom.Size = New System.Drawing.Size(155, 20)
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
        'DDDR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpCategoryDiscount)
        Me.Controls.Add(Me.XpGradientPanel1)
        Me.Name = "DDDR"
        Me.Size = New System.Drawing.Size(1263, 460)
        CType(Me.grpCategoryDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XpGradientPanel1.ResumeLayout(False)
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox2.ResumeLayout(False)
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        CType(Me.grpRangeDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRangeDate.ResumeLayout(False)
        Me.grpRangeDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpCategoryDiscount As Janus.Windows.GridEX.GridEX
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Friend WithEvents grpRangeDate As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicfrom As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents cmbParams As System.Windows.Forms.ComboBox
    Friend WithEvents btnApplyRange As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents cmbApplyDiscount As System.Windows.Forms.ComboBox

End Class
