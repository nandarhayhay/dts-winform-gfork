<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Stepping_Discount
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Stepping_Discount))
        Dim MultiColumnCombo3_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.MultiColumnCombo3 = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.grpEdit = New Janus.Windows.EditControls.UIGroupBox
        Me.txtBrandPackName = New System.Windows.Forms.TextBox
        Me.txtProgramName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnFilterDistributor = New DTSProjects.ButtonSearch
        Me.Label3 = New System.Windows.Forms.Label
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ButtonEntry1 = New DTSProjects.ButtonEntry
        CType(Me.MultiColumnCombo3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpEdit.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'MultiColumnCombo3
        '
        Me.MultiColumnCombo3.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        MultiColumnCombo3_DesignTimeLayout.LayoutString = resources.GetString("MultiColumnCombo3_DesignTimeLayout.LayoutString")
        Me.MultiColumnCombo3.DesignTimeLayout = MultiColumnCombo3_DesignTimeLayout
        Me.MultiColumnCombo3.DisabledBackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.MultiColumnCombo3.DisplayMember = "DISTRIBUTOR_NAME"
        Me.MultiColumnCombo3.HideSelection = False
        Me.MultiColumnCombo3.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.MultiColumnCombo3.Location = New System.Drawing.Point(77, 12)
        Me.MultiColumnCombo3.Name = "MultiColumnCombo3"
        Me.MultiColumnCombo3.SelectedIndex = -1
        Me.MultiColumnCombo3.SelectedItem = Nothing
        Me.MultiColumnCombo3.Size = New System.Drawing.Size(267, 20)
        Me.MultiColumnCombo3.TabIndex = 25
        Me.MultiColumnCombo3.ValueMember = "PROG_BRANDPACK_DIST_ID"
        Me.MultiColumnCombo3.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpEdit
        '
        Me.grpEdit.Controls.Add(Me.txtBrandPackName)
        Me.grpEdit.Controls.Add(Me.txtProgramName)
        Me.grpEdit.Controls.Add(Me.Label2)
        Me.grpEdit.Controls.Add(Me.Label1)
        Me.grpEdit.Controls.Add(Me.btnFilterDistributor)
        Me.grpEdit.Controls.Add(Me.Label3)
        Me.grpEdit.Controls.Add(Me.MultiColumnCombo3)
        Me.grpEdit.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpEdit.Location = New System.Drawing.Point(0, 0)
        Me.grpEdit.Name = "grpEdit"
        Me.grpEdit.Size = New System.Drawing.Size(377, 40)
        Me.grpEdit.TabIndex = 27
        Me.grpEdit.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtBrandPackName
        '
        Me.txtBrandPackName.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtBrandPackName.Location = New System.Drawing.Point(120, 69)
        Me.txtBrandPackName.Multiline = True
        Me.txtBrandPackName.Name = "txtBrandPackName"
        Me.txtBrandPackName.ReadOnly = True
        Me.txtBrandPackName.Size = New System.Drawing.Size(251, 20)
        Me.txtBrandPackName.TabIndex = 36
        '
        'txtProgramName
        '
        Me.txtProgramName.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtProgramName.Location = New System.Drawing.Point(121, 46)
        Me.txtProgramName.Multiline = True
        Me.txtProgramName.Name = "txtProgramName"
        Me.txtProgramName.ReadOnly = True
        Me.txtProgramName.Size = New System.Drawing.Size(251, 20)
        Me.txtProgramName.TabIndex = 35
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 13)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "BRANDPACK_NAME"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "PROGRAM_NAME"
        '
        'btnFilterDistributor
        '
        Me.btnFilterDistributor.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterDistributor.Location = New System.Drawing.Point(349, 13)
        Me.btnFilterDistributor.Name = "btnFilterDistributor"
        Me.btnFilterDistributor.Size = New System.Drawing.Size(21, 21)
        Me.btnFilterDistributor.TabIndex = 32
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 15)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Distributor"
        '
        'GridEX1
        '
        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.None
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.Location = New System.Drawing.Point(0, 40)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(377, 273)
        Me.GridEX1.TabIndex = 29
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ButtonEntry1
        '
        Me.ButtonEntry1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ButtonEntry1.Location = New System.Drawing.Point(0, 283)
        Me.ButtonEntry1.Name = "ButtonEntry1"
        Me.ButtonEntry1.Size = New System.Drawing.Size(377, 30)
        Me.ButtonEntry1.TabIndex = 30
        '
        'Stepping_Discount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(377, 313)
        Me.ControlBox = False
        Me.Controls.Add(Me.ButtonEntry1)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.grpEdit)
        Me.Name = "Stepping_Discount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data Entry For Stepping Discount for Sales Program for Distributor"
        CType(Me.MultiColumnCombo3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpEdit.ResumeLayout(False)
        Me.grpEdit.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents MultiColumnCombo3 As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents btnFilterDistributor As DTSProjects.ButtonSearch
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ButtonEntry1 As DTSProjects.ButtonEntry
    Friend WithEvents txtBrandPackName As System.Windows.Forms.TextBox
    Friend WithEvents txtProgramName As System.Windows.Forms.TextBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpEdit As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX

End Class
