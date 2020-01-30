<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReport))
        Dim mcbBrandPack_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim mcbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.UiGroupBox3 = New Janus.Windows.EditControls.UIGroupBox
        Me.btnRemove = New Janus.Windows.EditControls.UIButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnApllyFilter = New Janus.Windows.EditControls.UIButton
        Me.btnFilterBrandPack = New DTSProjects.ButtonSearch
        Me.btnFilterDistributor = New DTSProjects.ButtonSearch
        Me.UiGroupBox6 = New Janus.Windows.EditControls.UIGroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.rdbPO = New System.Windows.Forms.RadioButton
        Me.rdbOA = New System.Windows.Forms.RadioButton
        Me.dtPicUntil = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.dtPicFrom = New Janus.Windows.CalendarCombo.CalendarCombo
        Me.UiGroupBox5 = New Janus.Windows.EditControls.UIGroupBox
        Me.mcbBrandPack = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.UiGroupBox4 = New Janus.Windows.EditControls.UIGroupBox
        Me.mcbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.UiGroupBox2 = New Janus.Windows.EditControls.UIGroupBox
        Me.rdbDistributor = New Janus.Windows.EditControls.UIRadioButton
        Me.rdbOAReport = New Janus.Windows.EditControls.UIRadioButton
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.UiGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox3.SuspendLayout()
        CType(Me.UiGroupBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox6.SuspendLayout()
        CType(Me.UiGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox5.SuspendLayout()
        CType(Me.mcbBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UiGroupBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox4.SuspendLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.Rebar
        Me.UiGroupBox1.Controls.Add(Me.UiGroupBox3)
        Me.UiGroupBox1.Controls.Add(Me.UiGroupBox2)
        Me.UiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UiGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(1028, 103)
        Me.UiGroupBox1.TabIndex = 0
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'UiGroupBox3
        '
        Me.UiGroupBox3.Controls.Add(Me.btnRemove)
        Me.UiGroupBox3.Controls.Add(Me.btnApllyFilter)
        Me.UiGroupBox3.Controls.Add(Me.btnFilterBrandPack)
        Me.UiGroupBox3.Controls.Add(Me.btnFilterDistributor)
        Me.UiGroupBox3.Controls.Add(Me.UiGroupBox6)
        Me.UiGroupBox3.Controls.Add(Me.UiGroupBox5)
        Me.UiGroupBox3.Controls.Add(Me.UiGroupBox4)
        Me.UiGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UiGroupBox3.Location = New System.Drawing.Point(211, 8)
        Me.UiGroupBox3.Name = "UiGroupBox3"
        Me.UiGroupBox3.Size = New System.Drawing.Size(814, 92)
        Me.UiGroupBox3.TabIndex = 2
        Me.UiGroupBox3.Text = "Filter"
        Me.UiGroupBox3.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnRemove
        '
        Me.btnRemove.ImageIndex = 1
        Me.btnRemove.ImageList = Me.ImageList1
        Me.btnRemove.Location = New System.Drawing.Point(690, 61)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(100, 23)
        Me.btnRemove.TabIndex = 6
        Me.btnRemove.Text = "Remove Filter"
        Me.btnRemove.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Filter 48 h p.ico")
        Me.ImageList1.Images.SetKeyName(1, "Delete.ico")
        '
        'btnApllyFilter
        '
        Me.btnApllyFilter.ImageIndex = 0
        Me.btnApllyFilter.ImageList = Me.ImageList1
        Me.btnApllyFilter.Location = New System.Drawing.Point(690, 25)
        Me.btnApllyFilter.Name = "btnApllyFilter"
        Me.btnApllyFilter.Size = New System.Drawing.Size(100, 23)
        Me.btnApllyFilter.TabIndex = 5
        Me.btnApllyFilter.Text = "Aplly Filter"
        Me.btnApllyFilter.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnFilterBrandPack
        '
        Me.btnFilterBrandPack.Location = New System.Drawing.Point(645, 28)
        Me.btnFilterBrandPack.Name = "btnFilterBrandPack"
        Me.btnFilterBrandPack.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterBrandPack.TabIndex = 4
        '
        'btnFilterDistributor
        '
        Me.btnFilterDistributor.Location = New System.Drawing.Point(283, 29)
        Me.btnFilterDistributor.Name = "btnFilterDistributor"
        Me.btnFilterDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnFilterDistributor.TabIndex = 3
        '
        'UiGroupBox6
        '
        Me.UiGroupBox6.Controls.Add(Me.Label2)
        Me.UiGroupBox6.Controls.Add(Me.Label1)
        Me.UiGroupBox6.Controls.Add(Me.rdbPO)
        Me.UiGroupBox6.Controls.Add(Me.rdbOA)
        Me.UiGroupBox6.Controls.Add(Me.dtPicUntil)
        Me.UiGroupBox6.Controls.Add(Me.dtPicFrom)
        Me.UiGroupBox6.Location = New System.Drawing.Point(3, 53)
        Me.UiGroupBox6.Name = "UiGroupBox6"
        Me.UiGroupBox6.Size = New System.Drawing.Size(633, 39)
        Me.UiGroupBox6.TabIndex = 2
        Me.UiGroupBox6.Text = "Range date by"
        Me.UiGroupBox6.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(396, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Until"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(150, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "From"
        '
        'rdbPO
        '
        Me.rdbPO.Location = New System.Drawing.Point(75, 14)
        Me.rdbPO.Name = "rdbPO"
        Me.rdbPO.Size = New System.Drawing.Size(51, 19)
        Me.rdbPO.TabIndex = 3
        Me.rdbPO.TabStop = True
        Me.rdbPO.Text = "PO"
        Me.rdbPO.UseVisualStyleBackColor = True
        '
        'rdbOA
        '
        Me.rdbOA.Checked = True
        Me.rdbOA.Location = New System.Drawing.Point(8, 16)
        Me.rdbOA.Name = "rdbOA"
        Me.rdbOA.Size = New System.Drawing.Size(51, 19)
        Me.rdbOA.TabIndex = 2
        Me.rdbOA.TabStop = True
        Me.rdbOA.Text = "OA"
        Me.rdbOA.UseVisualStyleBackColor = True
        '
        'dtPicUntil
        '
        Me.dtPicUntil.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicUntil.DropDownCalendar.Name = ""
        Me.dtPicUntil.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicUntil.Location = New System.Drawing.Point(434, 13)
        Me.dtPicUntil.Name = "dtPicUntil"
        Me.dtPicUntil.ShowTodayButton = False
        Me.dtPicUntil.Size = New System.Drawing.Size(188, 20)
        Me.dtPicUntil.TabIndex = 1
        Me.dtPicUntil.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'dtPicFrom
        '
        Me.dtPicFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.[Long]
        '
        '
        '
        Me.dtPicFrom.DropDownCalendar.Name = ""
        Me.dtPicFrom.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        Me.dtPicFrom.Location = New System.Drawing.Point(188, 13)
        Me.dtPicFrom.Name = "dtPicFrom"
        Me.dtPicFrom.ShowTodayButton = False
        Me.dtPicFrom.Size = New System.Drawing.Size(186, 20)
        Me.dtPicFrom.TabIndex = 0
        Me.dtPicFrom.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Office2007
        '
        'UiGroupBox5
        '
        Me.UiGroupBox5.Controls.Add(Me.mcbBrandPack)
        Me.UiGroupBox5.Location = New System.Drawing.Point(305, 12)
        Me.UiGroupBox5.Name = "UiGroupBox5"
        Me.UiGroupBox5.Size = New System.Drawing.Size(331, 37)
        Me.UiGroupBox5.TabIndex = 1
        Me.UiGroupBox5.Text = "BrandPack name"
        Me.UiGroupBox5.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'mcbBrandPack
        '
        Me.mcbBrandPack.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbBrandPack_DesignTimeLayout.LayoutString = resources.GetString("mcbBrandPack_DesignTimeLayout.LayoutString")
        Me.mcbBrandPack.DesignTimeLayout = mcbBrandPack_DesignTimeLayout
        Me.mcbBrandPack.DisplayMember = "BRANDPACK_NAME"
        Me.mcbBrandPack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mcbBrandPack.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbBrandPack.Location = New System.Drawing.Point(3, 16)
        Me.mcbBrandPack.Name = "mcbBrandPack"
        Me.mcbBrandPack.SelectedIndex = -1
        Me.mcbBrandPack.SelectedItem = Nothing
        Me.mcbBrandPack.Size = New System.Drawing.Size(325, 20)
        Me.mcbBrandPack.TabIndex = 0
        Me.mcbBrandPack.ValueMember = "BRANDPACK_ID"
        Me.mcbBrandPack.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UiGroupBox4
        '
        Me.UiGroupBox4.Controls.Add(Me.mcbDistributor)
        Me.UiGroupBox4.Location = New System.Drawing.Point(4, 11)
        Me.UiGroupBox4.Name = "UiGroupBox4"
        Me.UiGroupBox4.Size = New System.Drawing.Size(273, 37)
        Me.UiGroupBox4.TabIndex = 0
        Me.UiGroupBox4.Text = "Distributor"
        Me.UiGroupBox4.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'mcbDistributor
        '
        Me.mcbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbDistributor_DesignTimeLayout.LayoutString = resources.GetString("mcbDistributor_DesignTimeLayout.LayoutString")
        Me.mcbDistributor.DesignTimeLayout = mcbDistributor_DesignTimeLayout
        Me.mcbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.mcbDistributor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mcbDistributor.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.mcbDistributor.Location = New System.Drawing.Point(3, 16)
        Me.mcbDistributor.Name = "mcbDistributor"
        Me.mcbDistributor.SelectedIndex = -1
        Me.mcbDistributor.SelectedItem = Nothing
        Me.mcbDistributor.Size = New System.Drawing.Size(267, 20)
        Me.mcbDistributor.TabIndex = 0
        Me.mcbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.mcbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UiGroupBox2
        '
        Me.UiGroupBox2.Controls.Add(Me.rdbDistributor)
        Me.UiGroupBox2.Controls.Add(Me.rdbOAReport)
        Me.UiGroupBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.UiGroupBox2.Location = New System.Drawing.Point(3, 8)
        Me.UiGroupBox2.Name = "UiGroupBox2"
        Me.UiGroupBox2.Size = New System.Drawing.Size(208, 92)
        Me.UiGroupBox2.TabIndex = 2
        Me.UiGroupBox2.Text = "Type Report"
        Me.UiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'rdbDistributor
        '
        Me.rdbDistributor.Location = New System.Drawing.Point(15, 53)
        Me.rdbDistributor.Name = "rdbDistributor"
        Me.rdbDistributor.Size = New System.Drawing.Size(104, 23)
        Me.rdbDistributor.TabIndex = 4
        Me.rdbDistributor.Text = "Distributor Report"
        '
        'rdbOAReport
        '
        Me.rdbOAReport.Location = New System.Drawing.Point(15, 17)
        Me.rdbOAReport.Name = "rdbOAReport"
        Me.rdbOAReport.Size = New System.Drawing.Size(157, 23)
        Me.rdbOAReport.TabIndex = 3
        Me.rdbOAReport.Text = "Order Acceptance Report"
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 103)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.ShowRefreshButton = False
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1028, 643)
        Me.CrystalReportViewer1.TabIndex = 1
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'frmReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmReport"
        Me.Text = "PO & OA Distributor"
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        CType(Me.UiGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox3.ResumeLayout(False)
        CType(Me.UiGroupBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox6.ResumeLayout(False)
        Me.UiGroupBox6.PerformLayout()
        CType(Me.UiGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox5.ResumeLayout(False)
        Me.UiGroupBox5.PerformLayout()
        CType(Me.mcbBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UiGroupBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox4.ResumeLayout(False)
        Me.UiGroupBox4.PerformLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents UiGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents UiGroupBox3 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents rdbDistributor As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents rdbOAReport As Janus.Windows.EditControls.UIRadioButton
    Private WithEvents UiGroupBox6 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents UiGroupBox4 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents UiGroupBox5 As Janus.Windows.EditControls.UIGroupBox
    Private WithEvents mcbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents mcbBrandPack As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents btnRemove As Janus.Windows.EditControls.UIButton
    Private WithEvents btnApllyFilter As Janus.Windows.EditControls.UIButton
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents btnFilterBrandPack As DTSProjects.ButtonSearch
    Private WithEvents btnFilterDistributor As DTSProjects.ButtonSearch
    Private WithEvents rdbPO As System.Windows.Forms.RadioButton
    Private WithEvents rdbOA As System.Windows.Forms.RadioButton
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents dtPicUntil As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents dtPicFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Private WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
