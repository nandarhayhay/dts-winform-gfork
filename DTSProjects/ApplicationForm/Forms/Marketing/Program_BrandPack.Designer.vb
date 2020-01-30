<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Program_BrandPack
    Inherits DTSProjects.BaseForm

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
        Dim mcbOriginalBrandPack_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Program_BrandPack))
        Dim mcbBrandPack_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbProgram_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.lblItem = New System.Windows.Forms.Label
        Me.mcbOriginalBrandPack = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.mcbBrandPack = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.btnFilterBrandPack = New DTSProjects.ButtonSearch
        Me.btnFilterProgram = New DTSProjects.ButtonSearch
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtPicEnd = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicStart = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnChekExisting = New Janus.Windows.EditControls.UIButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.mcbProgram = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.ButtonEntry1 = New DTSProjects.ButtonEntry
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.mcbOriginalBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbProgram, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.Controls.Add(Me.lblItem)
        Me.UiGroupBox1.Controls.Add(Me.mcbOriginalBrandPack)
        Me.UiGroupBox1.Controls.Add(Me.mcbBrandPack)
        Me.UiGroupBox1.Controls.Add(Me.btnFilterBrandPack)
        Me.UiGroupBox1.Controls.Add(Me.btnFilterProgram)
        Me.UiGroupBox1.Controls.Add(Me.Label4)
        Me.UiGroupBox1.Controls.Add(Me.Label1)
        Me.UiGroupBox1.Controls.Add(Me.dtPicEnd)
        Me.UiGroupBox1.Controls.Add(Me.dtPicStart)
        Me.UiGroupBox1.Controls.Add(Me.Label3)
        Me.UiGroupBox1.Controls.Add(Me.Label2)
        Me.UiGroupBox1.Controls.Add(Me.btnChekExisting)
        Me.UiGroupBox1.Controls.Add(Me.mcbProgram)
        Me.UiGroupBox1.Controls.Add(Me.ButtonEntry1)
        Me.UiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UiGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(495, 165)
        Me.UiGroupBox1.TabIndex = 0
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'lblItem
        '
        Me.lblItem.Location = New System.Drawing.Point(296, 103)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(195, 16)
        Me.lblItem.TabIndex = 21
        '
        'mcbOriginalBrandPack
        '
        Me.mcbOriginalBrandPack.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbOriginalBrandPack_DesignTimeLayout.LayoutString = resources.GetString("mcbOriginalBrandPack_DesignTimeLayout.LayoutString")
        Me.mcbOriginalBrandPack.DesignTimeLayout = mcbOriginalBrandPack_DesignTimeLayout
        Me.mcbOriginalBrandPack.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbOriginalBrandPack.DisplayMember = "BRANDPACK_NAME"
        Me.mcbOriginalBrandPack.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbOriginalBrandPack.Location = New System.Drawing.Point(124, 37)
        Me.mcbOriginalBrandPack.Name = "mcbOriginalBrandPack"
        Me.mcbOriginalBrandPack.SelectedIndex = -1
        Me.mcbOriginalBrandPack.SelectedItem = Nothing
        Me.mcbOriginalBrandPack.Size = New System.Drawing.Size(336, 20)
        Me.mcbOriginalBrandPack.TabIndex = 20
        Me.mcbOriginalBrandPack.ValueMember = "BRANDPACK_ID"
        Me.mcbOriginalBrandPack.Visible = False
        Me.mcbOriginalBrandPack.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'mcbBrandPack
        '
        Me.mcbBrandPack.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbBrandPack_DesignTimeLayout.LayoutString = resources.GetString("mcbBrandPack_DesignTimeLayout.LayoutString")
        Me.mcbBrandPack.DesignTimeLayout = mcbBrandPack_DesignTimeLayout
        Me.mcbBrandPack.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbBrandPack.DropDownDisplayMember = "BRANDPACK_NAME"
        Me.mcbBrandPack.DropDownValueMember = "BRANDPACK_ID"
        Me.mcbBrandPack.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbBrandPack.Location = New System.Drawing.Point(124, 37)
        Me.mcbBrandPack.Name = "mcbBrandPack"
        Me.mcbBrandPack.SaveSettings = False
        Me.mcbBrandPack.SettingsKey = "mcbBrandPack"
        Me.mcbBrandPack.Size = New System.Drawing.Size(336, 20)
        Me.mcbBrandPack.TabIndex = 19
        Me.mcbBrandPack.ValuesDataMember = Nothing
        Me.mcbBrandPack.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnFilterBrandPack
        '
        Me.btnFilterBrandPack.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterBrandPack.Location = New System.Drawing.Point(466, 40)
        Me.btnFilterBrandPack.Name = "btnFilterBrandPack"
        Me.btnFilterBrandPack.Size = New System.Drawing.Size(21, 20)
        Me.btnFilterBrandPack.TabIndex = 18
        '
        'btnFilterProgram
        '
        Me.btnFilterProgram.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterProgram.Location = New System.Drawing.Point(466, 14)
        Me.btnFilterProgram.Name = "btnFilterProgram"
        Me.btnFilterProgram.Size = New System.Drawing.Size(21, 20)
        Me.btnFilterProgram.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 15)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "BrandPack Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 15)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Program ID"
        '
        'dtPicEnd
        '
        Me.dtPicEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicEnd.CustomFormat = "dd MMMM yyyy"
        Me.dtPicEnd.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        Me.dtPicEnd.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        '
        '
        '
        Me.dtPicEnd.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicEnd.DropDownCalendar.Name = ""
        Me.dtPicEnd.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicEnd.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicEnd.IsNullDate = True
        Me.dtPicEnd.Location = New System.Drawing.Point(124, 89)
        Me.dtPicEnd.Name = "dtPicEnd"
        Me.dtPicEnd.ShowTodayButton = False
        Me.dtPicEnd.Size = New System.Drawing.Size(149, 20)
        Me.dtPicEnd.TabIndex = 14
        Me.dtPicEnd.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicStart
        '
        Me.dtPicStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicStart.CustomFormat = "dd MMMM yyyy"
        Me.dtPicStart.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        Me.dtPicStart.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        '
        '
        '
        Me.dtPicStart.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicStart.DropDownCalendar.Name = ""
        Me.dtPicStart.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicStart.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicStart.IsNullDate = True
        Me.dtPicStart.Location = New System.Drawing.Point(124, 63)
        Me.dtPicStart.Name = "dtPicStart"
        Me.dtPicStart.ShowTodayButton = False
        Me.dtPicStart.Size = New System.Drawing.Size(149, 20)
        Me.dtPicStart.TabIndex = 13
        Me.dtPicStart.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 15)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "End Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Start Date"
        '
        'btnChekExisting
        '
        Me.btnChekExisting.ImageIndex = 11
        Me.btnChekExisting.ImageList = Me.ImageList1
        Me.btnChekExisting.Location = New System.Drawing.Point(353, 65)
        Me.btnChekExisting.Name = "btnChekExisting"
        Me.btnChekExisting.Size = New System.Drawing.Size(107, 21)
        Me.btnChekExisting.TabIndex = 10
        Me.btnChekExisting.Text = "Check Existing "
        Me.btnChekExisting.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Cancel.ico")
        Me.ImageList1.Images.SetKeyName(1, "Add.bmp")
        Me.ImageList1.Images.SetKeyName(2, "Grid.bmp")
        Me.ImageList1.Images.SetKeyName(3, "Filter 48 h p.ico")
        Me.ImageList1.Images.SetKeyName(4, "Customize.png")
        Me.ImageList1.Images.SetKeyName(5, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(6, "Save.png")
        Me.ImageList1.Images.SetKeyName(7, "Delete.ico")
        Me.ImageList1.Images.SetKeyName(8, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(9, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(10, "TextEdit.png")
        Me.ImageList1.Images.SetKeyName(11, "Search.png")
        '
        'mcbProgram
        '
        Me.mcbProgram.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbProgram_DesignTimeLayout.LayoutString = resources.GetString("mcbProgram_DesignTimeLayout.LayoutString")
        Me.mcbProgram.DesignTimeLayout = mcbProgram_DesignTimeLayout
        Me.mcbProgram.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.mcbProgram.DisplayMember = "PROGRAM_ID"
        Me.mcbProgram.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbProgram.Location = New System.Drawing.Point(124, 12)
        Me.mcbProgram.Name = "mcbProgram"
        Me.mcbProgram.SelectedIndex = -1
        Me.mcbProgram.SelectedItem = Nothing
        Me.mcbProgram.Size = New System.Drawing.Size(336, 20)
        Me.mcbProgram.TabIndex = 1
        Me.mcbProgram.ValueMember = "PROGRAM_ID"
        Me.mcbProgram.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ButtonEntry1
        '
        Me.ButtonEntry1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ButtonEntry1.Location = New System.Drawing.Point(3, 122)
        Me.ButtonEntry1.Name = "ButtonEntry1"
        Me.ButtonEntry1.Size = New System.Drawing.Size(489, 40)
        Me.ButtonEntry1.TabIndex = 0
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 165)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(495, 452)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Program_BrandPack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(495, 617)
        Me.ControlBox = False
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Name = "Program_BrandPack"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data Entry Program BrandPack Included "
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.UiGroupBox1.PerformLayout()
        CType(Me.mcbOriginalBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbProgram, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents ButtonEntry1 As DTSProjects.ButtonEntry
    Private WithEvents btnChekExisting As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtPicEnd As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicStart As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnFilterBrandPack As DTSProjects.ButtonSearch
    Friend WithEvents btnFilterProgram As DTSProjects.ButtonSearch
    Friend WithEvents mcbProgram As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents mcbBrandPack As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Friend WithEvents mcbOriginalBrandPack As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents lblItem As System.Windows.Forms.Label

End Class
