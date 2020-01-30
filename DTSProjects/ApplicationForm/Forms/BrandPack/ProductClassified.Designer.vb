<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProductClassified
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
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProductClassified))
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.XpGradientPanel2 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.btnAddNew = New Janus.Windows.EditControls.UIButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnCLose = New Janus.Windows.EditControls.UIButton
        Me.btnSave = New Janus.Windows.EditControls.UIButton
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XpGradientPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.NextCell
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.Location = New System.Drawing.Point(0, 0)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(649, 548)
        Me.GridEX1.TabIndex = 6
        Me.GridEX1.UpdateOnLeave = False
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'XpGradientPanel2
        '
        Me.XpGradientPanel2.Controls.Add(Me.btnAddNew)
        Me.XpGradientPanel2.Controls.Add(Me.btnCLose)
        Me.XpGradientPanel2.Controls.Add(Me.btnSave)
        Me.XpGradientPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.XpGradientPanel2.Location = New System.Drawing.Point(0, 548)
        Me.XpGradientPanel2.Name = "XpGradientPanel2"
        Me.XpGradientPanel2.Size = New System.Drawing.Size(649, 35)
        Me.XpGradientPanel2.TabIndex = 7
        '
        'btnAddNew
        '
        Me.btnAddNew.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnAddNew.ImageIndex = 0
        Me.btnAddNew.ImageList = Me.ImageList1
        Me.btnAddNew.Location = New System.Drawing.Point(7, 7)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(75, 22)
        Me.btnAddNew.TabIndex = 0
        Me.btnAddNew.Text = "Add &New"
        Me.btnAddNew.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
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
        Me.ImageList1.Images.SetKeyName(10, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(11, "Cancel and Close.ico")
        Me.ImageList1.Images.SetKeyName(12, "Search.png")
        '
        'btnCLose
        '
        Me.btnCLose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnCLose.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnCLose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCLose.ImageIndex = 6
        Me.btnCLose.ImageList = Me.ImageList1
        Me.btnCLose.Location = New System.Drawing.Point(576, 6)
        Me.btnCLose.Name = "btnCLose"
        Me.btnCLose.Size = New System.Drawing.Size(70, 22)
        Me.btnCLose.TabIndex = 2
        Me.btnCLose.Text = "&Cancel"
        Me.btnCLose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnSave.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnSave.ImageIndex = 10
        Me.btnSave.ImageList = Me.ImageList1
        Me.btnSave.Location = New System.Drawing.Point(469, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(101, 22)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "&Save Changes"
        Me.btnSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'ProductClassified
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCLose
        Me.ClientSize = New System.Drawing.Size(649, 583)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.XpGradientPanel2)
        Me.Name = "ProductClassified"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Classifying products"
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XpGradientPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents XpGradientPanel2 As SteepValley.Windows.Forms.XPGradientPanel
    Friend WithEvents btnAddNew As Janus.Windows.EditControls.UIButton
    Private WithEvents btnCLose As Janus.Windows.EditControls.UIButton
    Private WithEvents btnSave As Janus.Windows.EditControls.UIButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
End Class
