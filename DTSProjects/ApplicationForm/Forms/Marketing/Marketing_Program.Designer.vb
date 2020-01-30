<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Marketing_Program
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Marketing_Program))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.txtProgramName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnChekExisting = New Janus.Windows.EditControls.UIButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.dtPicEnd = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicStart = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.txtProgramID = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ButtonEntry1 = New DTSProjects.ButtonEntry
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.Controls.Add(Me.txtProgramName)
        Me.UiGroupBox1.Controls.Add(Me.Label4)
        Me.UiGroupBox1.Controls.Add(Me.btnChekExisting)
        Me.UiGroupBox1.Controls.Add(Me.dtPicEnd)
        Me.UiGroupBox1.Controls.Add(Me.dtPicStart)
        Me.UiGroupBox1.Controls.Add(Me.txtProgramID)
        Me.UiGroupBox1.Controls.Add(Me.Label3)
        Me.UiGroupBox1.Controls.Add(Me.Label2)
        Me.UiGroupBox1.Controls.Add(Me.Label1)
        Me.UiGroupBox1.Controls.Add(Me.ButtonEntry1)
        Me.UiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UiGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(425, 184)
        Me.UiGroupBox1.TabIndex = 1
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtProgramName
        '
        Me.txtProgramName.Location = New System.Drawing.Point(103, 49)
        Me.txtProgramName.MaxLength = 30
        Me.txtProgramName.Name = "txtProgramName"
        Me.txtProgramName.Size = New System.Drawing.Size(310, 20)
        Me.txtProgramName.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 15)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Program ID"
        '
        'btnChekExisting
        '
        Me.btnChekExisting.ImageIndex = 11
        Me.btnChekExisting.ImageList = Me.ImageList1
        Me.btnChekExisting.Location = New System.Drawing.Point(264, 22)
        Me.btnChekExisting.Name = "btnChekExisting"
        Me.btnChekExisting.Size = New System.Drawing.Size(149, 21)
        Me.btnChekExisting.TabIndex = 1
        Me.btnChekExisting.Text = "Check Existing"
        Me.btnChekExisting.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
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
        Me.ImageList1.Images.SetKeyName(11, "Search.png")
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
        Me.dtPicEnd.Location = New System.Drawing.Point(103, 118)
        Me.dtPicEnd.Name = "dtPicEnd"
        Me.dtPicEnd.ShowTodayButton = False
        Me.dtPicEnd.Size = New System.Drawing.Size(149, 20)
        Me.dtPicEnd.TabIndex = 4
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
        Me.dtPicStart.Location = New System.Drawing.Point(103, 82)
        Me.dtPicStart.Name = "dtPicStart"
        Me.dtPicStart.ShowTodayButton = False
        Me.dtPicStart.Size = New System.Drawing.Size(149, 20)
        Me.dtPicStart.TabIndex = 3
        Me.dtPicStart.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'txtProgramID
        '
        Me.txtProgramID.Location = New System.Drawing.Point(103, 22)
        Me.txtProgramID.MaxLength = 15
        Me.txtProgramID.Name = "txtProgramID"
        Me.txtProgramID.Size = New System.Drawing.Size(155, 20)
        Me.txtProgramID.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "End Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Start Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Program Name"
        '
        'ButtonEntry1
        '
        Me.ButtonEntry1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ButtonEntry1.Location = New System.Drawing.Point(3, 144)
        Me.ButtonEntry1.Name = "ButtonEntry1"
        Me.ButtonEntry1.Size = New System.Drawing.Size(419, 37)
        Me.ButtonEntry1.TabIndex = 5
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
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 184)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(425, 314)
        Me.GridEX1.TabIndex = 0
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'Marketing_Program
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(425, 498)
        Me.ControlBox = False
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Name = "Marketing_Program"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data Entry Program Registering"
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.UiGroupBox1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents ButtonEntry1 As DTSProjects.ButtonEntry
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtProgramID As System.Windows.Forms.TextBox
    Private WithEvents btnChekExisting As Janus.Windows.EditControls.UIButton
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents dtPicEnd As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents dtPicStart As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents txtProgramName As System.Windows.Forms.TextBox

End Class
