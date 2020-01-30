<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCrystalReport
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
        Dim mcbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCrystalReport))
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.btnDistPODispro = New DevComponents.DotNetBar.ButtonItem
        Me.btnDistPODisproRL = New DevComponents.DotNetBar.ButtonItem
        Me.btnPOBrandDispro = New DevComponents.DotNetBar.ButtonItem
        Me.btnPOBrandDisproRL = New DevComponents.DotNetBar.ButtonItem
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.mcbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.btnFilterDistributor = New DTSProjects.ButtonSearch
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.btnAplyFilter = New Janus.Windows.EditControls.UIButton
        Me.lblFlag = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Bar1
        '
        Me.Bar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnDistPODispro, Me.btnDistPODisproRL, Me.btnPOBrandDispro, Me.btnPOBrandDisproRL})
        Me.Bar1.Location = New System.Drawing.Point(0, 0)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(1028, 25)
        Me.Bar1.Stretch = True
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar1.TabIndex = 0
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Sum"
        '
        'btnDistPODispro
        '
        Me.btnDistPODispro.Name = "btnDistPODispro"
        Me.btnDistPODispro.Text = "Summary Report Distributor PO Dispro"
        '
        'btnDistPODisproRL
        '
        Me.btnDistPODisproRL.Name = "btnDistPODisproRL"
        Me.btnDistPODisproRL.Text = "Summary report Distributor PO & brand(release & left)"
        '
        'btnPOBrandDispro
        '
        Me.btnPOBrandDispro.Name = "btnPOBrandDispro"
        Me.btnPOBrandDispro.Text = "Summary report PO group by Brand & Dispro"
        '
        'btnPOBrandDisproRL
        '
        Me.btnPOBrandDisproRL.Name = "btnPOBrandDisproRL"
        Me.btnPOBrandDisproRL.Text = "Summary report PO group by brand & Dispro(release & left)"
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.UiGroupBox1)
        Me.PanelEx1.Controls.Add(Me.dtPicUntil)
        Me.PanelEx1.Controls.Add(Me.dtPicFrom)
        Me.PanelEx1.Controls.Add(Me.btnAplyFilter)
        Me.PanelEx1.Controls.Add(Me.lblFlag)
        Me.PanelEx1.Controls.Add(Me.Label3)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx1.Location = New System.Drawing.Point(0, 25)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(1028, 44)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 1
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.Controls.Add(Me.mcbDistributor)
        Me.UiGroupBox1.Controls.Add(Me.btnFilterDistributor)
        Me.UiGroupBox1.Location = New System.Drawing.Point(114, 0)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(310, 39)
        Me.UiGroupBox1.TabIndex = 2
        Me.UiGroupBox1.Text = "Distributor"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'mcbDistributor
        '
        Me.mcbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        mcbDistributor_DesignTimeLayout.LayoutString = resources.GetString("mcbDistributor_DesignTimeLayout.LayoutString")
        Me.mcbDistributor.DesignTimeLayout = mcbDistributor_DesignTimeLayout
        Me.mcbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.mcbDistributor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mcbDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbDistributor.Location = New System.Drawing.Point(3, 16)
        Me.mcbDistributor.Name = "mcbDistributor"
        Me.mcbDistributor.SelectedIndex = -1
        Me.mcbDistributor.SelectedItem = Nothing
        Me.mcbDistributor.Size = New System.Drawing.Size(287, 20)
        Me.mcbDistributor.TabIndex = 17
        Me.mcbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.mcbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnFilterDistributor
        '
        Me.btnFilterDistributor.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnFilterDistributor.Location = New System.Drawing.Point(290, 16)
        Me.btnFilterDistributor.Name = "btnFilterDistributor"
        Me.btnFilterDistributor.Size = New System.Drawing.Size(17, 20)
        Me.btnFilterDistributor.TabIndex = 18
        '
        'dtPicUntil
        '
        Me.dtPicUntil.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicUntil.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicUntil.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicUntil.DropDownCalendar.Name = ""
        Me.dtPicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicUntil.Location = New System.Drawing.Point(678, 15)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.Size = New System.Drawing.Size(161, 20)
        Me.dtPicUntil.TabIndex = 16
        Me.dtPicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicFrom
        '
        Me.dtPicFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicFrom.DropDownCalendar.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtPicFrom.DropDownCalendar.Name = ""
        Me.dtPicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFrom.Location = New System.Drawing.Point(477, 15)
        Me.dtPicFrom.Name = "dtPicFrom"
        Me.dtPicFrom.Size = New System.Drawing.Size(165, 20)
        Me.dtPicFrom.TabIndex = 15
        Me.dtPicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'btnAplyFilter
        '
        Me.btnAplyFilter.Location = New System.Drawing.Point(845, 13)
        Me.btnAplyFilter.Name = "btnAplyFilter"
        Me.btnAplyFilter.Size = New System.Drawing.Size(75, 23)
        Me.btnAplyFilter.TabIndex = 14
        Me.btnAplyFilter.Text = "Apply filter"
        Me.btnAplyFilter.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'lblFlag
        '
        Me.lblFlag.AutoSize = True
        Me.lblFlag.Location = New System.Drawing.Point(646, 18)
        Me.lblFlag.Name = "lblFlag"
        Me.lblFlag.Size = New System.Drawing.Size(28, 13)
        Me.lblFlag.TabIndex = 10
        Me.lblFlag.Text = "Until"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(439, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "From"
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 69)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1028, 656)
        Me.CrystalReportViewer1.TabIndex = 2
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'frmCrystalReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1028, 725)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.PanelEx1)
        Me.Controls.Add(Me.Bar1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCrystalReport"
        Me.Text = "PO & Dispro"
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        Me.UiGroupBox1.PerformLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Private WithEvents btnDistPODispro As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnDistPODisproRL As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPOBrandDispro As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPOBrandDisproRL As DevComponents.DotNetBar.ButtonItem
    Private WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents btnAplyFilter As Janus.Windows.EditControls.UIButton
    Private WithEvents lblFlag As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnFilterDistributor As DTSProjects.ButtonSearch
    Private WithEvents mcbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer

End Class
