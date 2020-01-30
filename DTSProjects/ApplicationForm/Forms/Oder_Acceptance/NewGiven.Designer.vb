<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewGiven
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewGiven))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX1_DesignTimeLayout_Reference_0 As Janus.Windows.Common.Layouts.JanusLayoutReference = New Janus.Windows.Common.Layouts.JanusLayoutReference("GridEXLayoutData.RootTable.Columns.Column0.ButtonImage")
        Dim mcbBrandName_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbAgreement_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.ButtonEntry1 = New DTSProjects.ButtonEntry
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.mcbBrandName = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.mcbAgreement = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.dtPicstart = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.txtGiven = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtGivenDescription = New Janus.Windows.GridEX.EditControls.EditBox
        Me.btnFilterAgreement = New DTSProjects.ButtonSearch
        Me.btnFilterBrandName = New DTSProjects.ButtonSearch
        Me.RefreshData1 = New DTSProjects.RefreshData
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbBrandName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbAgreement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "AGREEMENT_NO"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "BRAND_NAME"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "START_DATE"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 97)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "GIVEN_DISC"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(116, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "GIVEN DESCRIPTION"
        '
        'ButtonEntry1
        '
        Me.ButtonEntry1.Location = New System.Drawing.Point(1, 142)
        Me.ButtonEntry1.Name = "ButtonEntry1"
        Me.ButtonEntry1.Size = New System.Drawing.Size(542, 39)
        Me.ButtonEntry1.TabIndex = 5
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
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX1_DesignTimeLayout_Reference_0.Instance = CType(resources.GetObject("GridEX1_DesignTimeLayout_Reference_0.Instance"), Object)
        GridEX1_DesignTimeLayout.LayoutReferences.AddRange(New Janus.Windows.Common.Layouts.JanusLayoutReference() {GridEX1_DesignTimeLayout_Reference_0})
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 187)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(546, 241)
        Me.GridEX1.TabIndex = 6
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'mcbBrandName
        '
        Me.mcbBrandName.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbBrandName_DesignTimeLayout.LayoutString = resources.GetString("mcbBrandName_DesignTimeLayout.LayoutString")
        Me.mcbBrandName.DesignTimeLayout = mcbBrandName_DesignTimeLayout
        Me.mcbBrandName.DisplayMember = "BRAND_NAME"
        Me.mcbBrandName.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbBrandName.Location = New System.Drawing.Point(135, 40)
        Me.mcbBrandName.Name = "mcbBrandName"
        Me.mcbBrandName.SelectedIndex = -1
        Me.mcbBrandName.SelectedItem = Nothing
        Me.mcbBrandName.Size = New System.Drawing.Size(289, 20)
        Me.mcbBrandName.TabIndex = 1
        Me.mcbBrandName.ValueMember = "BRAND_ID"
        Me.mcbBrandName.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'mcbAgreement
        '
        Me.mcbAgreement.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbAgreement_DesignTimeLayout.LayoutString = resources.GetString("mcbAgreement_DesignTimeLayout.LayoutString")
        Me.mcbAgreement.DesignTimeLayout = mcbAgreement_DesignTimeLayout
        Me.mcbAgreement.DisplayMember = "AGREEMENT_NO"
        Me.mcbAgreement.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbAgreement.Location = New System.Drawing.Point(135, 16)
        Me.mcbAgreement.Name = "mcbAgreement"
        Me.mcbAgreement.SelectedIndex = -1
        Me.mcbAgreement.SelectedItem = Nothing
        Me.mcbAgreement.Size = New System.Drawing.Size(289, 20)
        Me.mcbAgreement.TabIndex = 0
        Me.mcbAgreement.ValueMember = "AGREEMENT_NO"
        Me.mcbAgreement.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'dtPicstart
        '
        Me.dtPicstart.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicstart.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicstart.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicstart.DropDownCalendar.Name = ""
        Me.dtPicstart.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicstart.HoverMode = Janus.Windows.CalendarCombo.HoverMode.Highlight
        Me.dtPicstart.Location = New System.Drawing.Point(135, 66)
        Me.dtPicstart.Name = "dtPicstart"
        Me.dtPicstart.ShowTodayButton = False
        Me.dtPicstart.Size = New System.Drawing.Size(160, 20)
        Me.dtPicstart.TabIndex = 11
        Me.dtPicstart.Value = New Date(2008, 10, 21, 0, 0, 0, 0)
        Me.dtPicstart.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'txtGiven
        '
        Me.txtGiven.Location = New System.Drawing.Point(135, 94)
        Me.txtGiven.MaxLength = 6
        Me.txtGiven.Name = "txtGiven"
        Me.txtGiven.Size = New System.Drawing.Size(65, 20)
        Me.txtGiven.TabIndex = 3
        Me.txtGiven.Text = "0,00"
        Me.txtGiven.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtGiven.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtGivenDescription
        '
        Me.txtGivenDescription.Location = New System.Drawing.Point(135, 117)
        Me.txtGivenDescription.MaxLength = 150
        Me.txtGivenDescription.Name = "txtGivenDescription"
        Me.txtGivenDescription.Size = New System.Drawing.Size(399, 20)
        Me.txtGivenDescription.TabIndex = 4
        '
        'btnFilterAgreement
        '
        Me.btnFilterAgreement.Location = New System.Drawing.Point(431, 16)
        Me.btnFilterAgreement.Name = "btnFilterAgreement"
        Me.btnFilterAgreement.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterAgreement.TabIndex = 12
        '
        'btnFilterBrandName
        '
        Me.btnFilterBrandName.Location = New System.Drawing.Point(431, 41)
        Me.btnFilterBrandName.Name = "btnFilterBrandName"
        Me.btnFilterBrandName.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterBrandName.TabIndex = 13
        '
        'RefreshData1
        '
        Me.RefreshData1.Location = New System.Drawing.Point(90, 153)
        Me.RefreshData1.Name = "RefreshData1"
        Me.RefreshData1.Size = New System.Drawing.Size(99, 23)
        Me.RefreshData1.TabIndex = 14
        '
        'NewGiven
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(546, 428)
        Me.Controls.Add(Me.RefreshData1)
        Me.Controls.Add(Me.btnFilterBrandName)
        Me.Controls.Add(Me.btnFilterAgreement)
        Me.Controls.Add(Me.txtGivenDescription)
        Me.Controls.Add(Me.txtGiven)
        Me.Controls.Add(Me.dtPicstart)
        Me.Controls.Add(Me.mcbAgreement)
        Me.Controls.Add(Me.mcbBrandName)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.ButtonEntry1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "NewGiven"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Given Agreement"
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbBrandName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbAgreement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents ButtonEntry1 As DTSProjects.ButtonEntry
    Private WithEvents dtPicstart As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents txtGiven As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents txtGivenDescription As Janus.Windows.GridEX.EditControls.EditBox
    Private WithEvents mcbBrandName As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents mcbAgreement As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents btnFilterAgreement As DTSProjects.ButtonSearch
    Friend WithEvents btnFilterBrandName As DTSProjects.ButtonSearch
    Friend WithEvents RefreshData1 As DTSProjects.RefreshData
End Class
