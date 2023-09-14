<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSPPBReport
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
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSPPBReport))
        Me.crvSPPB = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.pnlReport = New System.Windows.Forms.Panel
        Me.pnlGrid = New System.Windows.Forms.Panel
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.txtSearch = New WatermarkTextBox.WaterMarkTextBox
        Me.ExpandableSplitter1 = New DevComponents.DotNetBar.ExpandableSplitter
        Me.pnlReport.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'crvSPPB
        '
        Me.crvSPPB.ActiveViewIndex = -1
        Me.crvSPPB.AutoScroll = True
        Me.crvSPPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvSPPB.DisplayGroupTree = False
        Me.crvSPPB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvSPPB.Location = New System.Drawing.Point(0, 0)
        Me.crvSPPB.Name = "crvSPPB"
        Me.crvSPPB.SelectionFormula = ""
        Me.crvSPPB.ShowGroupTreeButton = False
        Me.crvSPPB.Size = New System.Drawing.Size(724, 531)
        Me.crvSPPB.TabIndex = 2
        Me.crvSPPB.ViewTimeSelectionFormula = ""
        '
        'pnlReport
        '
        Me.pnlReport.Controls.Add(Me.crvSPPB)
        Me.pnlReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlReport.Location = New System.Drawing.Point(237, 0)
        Me.pnlReport.Name = "pnlReport"
        Me.pnlReport.Size = New System.Drawing.Size(724, 531)
        Me.pnlReport.TabIndex = 3
        '
        'pnlGrid
        '
        Me.pnlGrid.Controls.Add(Me.GridEX1)
        Me.pnlGrid.Controls.Add(Me.txtSearch)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlGrid.Location = New System.Drawing.Point(0, 0)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Size = New System.Drawing.Size(237, 531)
        Me.pnlGrid.TabIndex = 4
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.Location = New System.Drawing.Point(0, 20)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.Size = New System.Drawing.Size(237, 511)
        Me.GridEX1.TabIndex = 2
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSearch.Location = New System.Drawing.Point(0, 0)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(237, 20)
        Me.txtSearch.TabIndex = 9
        Me.txtSearch.WaterMarkColor = System.Drawing.Color.Gray
        Me.txtSearch.WaterMarkText = "Enter SPPB Number to search"
        '
        'ExpandableSplitter1
        '
        Me.ExpandableSplitter1.BackColor2 = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.ExpandableSplitter1.ExpandableControl = Me.pnlGrid
        Me.ExpandableSplitter1.ExpandFillColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.ExpandLineColor = System.Drawing.SystemColors.ControlText
        Me.ExpandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.GripDarkColor = System.Drawing.SystemColors.ControlText
        Me.ExpandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ExpandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.ExpandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.ExpandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2
        Me.ExpandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.ExpandableSplitter1.HotExpandFillColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.HotExpandLineColor = System.Drawing.SystemColors.ControlText
        Me.ExpandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.ExpandableSplitter1.HotGripDarkColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ExpandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.ExpandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ExpandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.ExpandableSplitter1.Location = New System.Drawing.Point(237, 0)
        Me.ExpandableSplitter1.Name = "ExpandableSplitter1"
        Me.ExpandableSplitter1.Size = New System.Drawing.Size(3, 531)
        Me.ExpandableSplitter1.TabIndex = 5
        Me.ExpandableSplitter1.TabStop = False
        '
        'FrmSPPBReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(961, 531)
        Me.Controls.Add(Me.ExpandableSplitter1)
        Me.Controls.Add(Me.pnlReport)
        Me.Controls.Add(Me.pnlGrid)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.Name = "FrmSPPBReport"
        Me.ShowIcon = False
        Me.Text = "PRINTING SPPB DATA"
        Me.pnlReport.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        Me.pnlGrid.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crvSPPB As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents pnlReport As System.Windows.Forms.Panel
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents ExpandableSplitter1 As DevComponents.DotNetBar.ExpandableSplitter
    Friend WithEvents txtSearch As WatermarkTextBox.WaterMarkTextBox
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX

End Class
