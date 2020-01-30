<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cancel_PO
    Inherits DTSProjects.BaseBigForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Cancel_PO))
        Dim CmbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.RefreshData1 = New DTSProjects.RefreshData
        Me.btnRelease = New Janus.Windows.EditControls.UIButton
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.grpDistributor = New Janus.Windows.EditControls.UIGroupBox
        Me.btnFilterDistributor = New DTSProjects.ButtonSearch
        Me.CmbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        Me.XpGradientPanel1.SuspendLayout()
        CType(Me.grpDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDistributor.SuspendLayout()
        CType(Me.CmbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEX1
        '
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 51)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(913, 351)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
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
        Me.ImageList1.Images.SetKeyName(11, "cancel_doc.ico")
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.RefreshData1)
        Me.PanelEx1.Controls.Add(Me.btnRelease)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx1.Location = New System.Drawing.Point(0, 402)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(913, 32)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 2
        '
        'RefreshData1
        '
        Me.RefreshData1.Location = New System.Drawing.Point(6, 4)
        Me.RefreshData1.Name = "RefreshData1"
        Me.RefreshData1.Size = New System.Drawing.Size(99, 23)
        Me.RefreshData1.TabIndex = 1
        '
        'btnRelease
        '
        Me.btnRelease.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRelease.ImageIndex = 11
        Me.btnRelease.ImageList = Me.ImageList1
        Me.btnRelease.Location = New System.Drawing.Point(822, 5)
        Me.btnRelease.Name = "btnRelease"
        Me.btnRelease.Size = New System.Drawing.Size(85, 23)
        Me.btnRelease.TabIndex = 0
        Me.btnRelease.Text = "&Release PO"
        Me.btnRelease.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Controls.Add(Me.grpDistributor)
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.XpGradientPanel1.EndColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.XpGradientPanel1.Location = New System.Drawing.Point(0, 0)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(913, 51)
        Me.XpGradientPanel1.StartColor = System.Drawing.SystemColors.InactiveCaption
        Me.XpGradientPanel1.TabIndex = 3
        '
        'grpDistributor
        '
        Me.grpDistributor.BackColor = System.Drawing.Color.Transparent
        Me.grpDistributor.Controls.Add(Me.btnFilterDistributor)
        Me.grpDistributor.Controls.Add(Me.CmbDistributor)
        Me.grpDistributor.Icon = CType(resources.GetObject("grpDistributor.Icon"), System.Drawing.Icon)
        Me.grpDistributor.Location = New System.Drawing.Point(339, 4)
        Me.grpDistributor.Name = "grpDistributor"
        Me.grpDistributor.Size = New System.Drawing.Size(273, 43)
        Me.grpDistributor.TabIndex = 1
        Me.grpDistributor.Text = "Distributor"
        Me.grpDistributor.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003
        '
        'btnFilterDistributor
        '
        Me.btnFilterDistributor.Location = New System.Drawing.Point(249, 20)
        Me.btnFilterDistributor.Name = "btnFilterDistributor"
        Me.btnFilterDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterDistributor.TabIndex = 1
        '
        'CmbDistributor
        '
        Me.CmbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        CmbDistributor_DesignTimeLayout.LayoutString = resources.GetString("CmbDistributor_DesignTimeLayout.LayoutString")
        Me.CmbDistributor.DesignTimeLayout = CmbDistributor_DesignTimeLayout
        Me.CmbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.CmbDistributor.Location = New System.Drawing.Point(21, 18)
        Me.CmbDistributor.Name = "CmbDistributor"
        Me.CmbDistributor.SelectedIndex = -1
        Me.CmbDistributor.SelectedItem = Nothing
        Me.CmbDistributor.Size = New System.Drawing.Size(224, 20)
        Me.CmbDistributor.TabIndex = 0
        Me.CmbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.CmbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Cancel_PO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(913, 434)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.XpGradientPanel1)
        Me.Controls.Add(Me.PanelEx1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Cancel_PO"
        Me.Text = "CANCELLING PURCHASE ORDER"
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.XpGradientPanel1.ResumeLayout(False)
        CType(Me.grpDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDistributor.ResumeLayout(False)
        Me.grpDistributor.PerformLayout()
        CType(Me.CmbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Private WithEvents btnRelease As Janus.Windows.EditControls.UIButton
    Friend WithEvents RefreshData1 As DTSProjects.RefreshData
    Private WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents grpDistributor As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents CmbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents btnFilterDistributor As DTSProjects.ButtonSearch

End Class
