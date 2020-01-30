<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEntryAdjPKD
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
        Dim mcbDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEntryAdjPKD))
        Dim mcbBrandPack_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim ChkDistributor_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtPicStartPeriode = New System.Windows.Forms.DateTimePicker
        Me.dtPicEndPeriode = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.mcbDistributor = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.mcbBrandPack = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.txtAdjusmentName = New System.Windows.Forms.TextBox
        Me.cmbAdjustmentFor = New System.Windows.Forms.ComboBox
        Me.txtMaxQty = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnCLose = New Janus.Windows.EditControls.UIButton
        Me.btnSave = New Janus.Windows.EditControls.UIButton
        Me.pnlPrint = New DevComponents.DotNetBar.PanelEx
        Me.btnSearchDistributor = New DTSProjects.ButtonSearch
        Me.btnSearchBrandPack = New DTSProjects.ButtonSearch
        Me.Label8 = New System.Windows.Forms.Label
        Me.rdbPerDistributor = New System.Windows.Forms.RadioButton
        Me.rdbGroupDistributor = New System.Windows.Forms.RadioButton
        Me.ChkDistributor = New Janus.Windows.GridEX.EditControls.CheckedComboBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mcbBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPrint.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "START"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "END"
        '
        'dtPicStartPeriode
        '
        Me.dtPicStartPeriode.CustomFormat = "dd MMMM yyyy"
        Me.dtPicStartPeriode.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtPicStartPeriode.Location = New System.Drawing.Point(139, 9)
        Me.dtPicStartPeriode.Name = "dtPicStartPeriode"
        Me.dtPicStartPeriode.Size = New System.Drawing.Size(147, 20)
        Me.dtPicStartPeriode.TabIndex = 2
        '
        'dtPicEndPeriode
        '
        Me.dtPicEndPeriode.CustomFormat = "dd MMMM yyyy"
        Me.dtPicEndPeriode.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtPicEndPeriode.Location = New System.Drawing.Point(139, 38)
        Me.dtPicEndPeriode.Name = "dtPicEndPeriode"
        Me.dtPicEndPeriode.Size = New System.Drawing.Size(147, 20)
        Me.dtPicEndPeriode.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 169)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "DISTRIBUTOR"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 193)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "BRANDPACK"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 138)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "TYPE OF ADJUSTMENT"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 220)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "MAX QTY"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(20, 85)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(114, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "ADJUSTMENT NAME"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtPicEndPeriode)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtPicStartPeriode)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(411, 70)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Periode"
        '
        'mcbDistributor
        '
        Me.mcbDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbDistributor_DesignTimeLayout.LayoutString = resources.GetString("mcbDistributor_DesignTimeLayout.LayoutString")
        Me.mcbDistributor.DesignTimeLayout = mcbDistributor_DesignTimeLayout
        Me.mcbDistributor.DisplayMember = "DISTRIBUTOR_NAME"
        Me.mcbDistributor.Location = New System.Drawing.Point(152, 165)
        Me.mcbDistributor.Name = "mcbDistributor"
        Me.mcbDistributor.SelectedIndex = -1
        Me.mcbDistributor.SelectedItem = Nothing
        Me.mcbDistributor.Size = New System.Drawing.Size(249, 20)
        Me.mcbDistributor.TabIndex = 10
        Me.mcbDistributor.ValueMember = "DISTRIBUTOR_ID"
        Me.mcbDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'mcbBrandPack
        '
        Me.mcbBrandPack.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        mcbBrandPack_DesignTimeLayout.LayoutString = resources.GetString("mcbBrandPack_DesignTimeLayout.LayoutString")
        Me.mcbBrandPack.DesignTimeLayout = mcbBrandPack_DesignTimeLayout
        Me.mcbBrandPack.DisplayMember = "BRANDPACK_NAME"
        Me.mcbBrandPack.Location = New System.Drawing.Point(152, 192)
        Me.mcbBrandPack.Name = "mcbBrandPack"
        Me.mcbBrandPack.SelectedIndex = -1
        Me.mcbBrandPack.SelectedItem = Nothing
        Me.mcbBrandPack.Size = New System.Drawing.Size(249, 20)
        Me.mcbBrandPack.TabIndex = 11
        Me.mcbBrandPack.ValueMember = "BRANDPACK_ID"
        Me.mcbBrandPack.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtAdjusmentName
        '
        Me.txtAdjusmentName.Location = New System.Drawing.Point(153, 82)
        Me.txtAdjusmentName.Name = "txtAdjusmentName"
        Me.txtAdjusmentName.Size = New System.Drawing.Size(271, 20)
        Me.txtAdjusmentName.TabIndex = 12
        '
        'cmbAdjustmentFor
        '
        Me.cmbAdjustmentFor.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.cmbAdjustmentFor.FormattingEnabled = True
        Me.cmbAdjustmentFor.Items.AddRange(New Object() {"RETAILER PROGRAM", "DPD"})
        Me.cmbAdjustmentFor.Location = New System.Drawing.Point(153, 135)
        Me.cmbAdjustmentFor.Name = "cmbAdjustmentFor"
        Me.cmbAdjustmentFor.Size = New System.Drawing.Size(120, 21)
        Me.cmbAdjustmentFor.TabIndex = 13
        '
        'txtMaxQty
        '
        Me.txtMaxQty.FormatString = "#,##0.000"
        Me.txtMaxQty.Location = New System.Drawing.Point(152, 218)
        Me.txtMaxQty.Name = "txtMaxQty"
        Me.txtMaxQty.Size = New System.Drawing.Size(91, 20)
        Me.txtMaxQty.TabIndex = 14
        Me.txtMaxQty.Text = "0.000"
        Me.txtMaxQty.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        Me.txtMaxQty.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
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
        Me.btnCLose.Location = New System.Drawing.Point(345, 4)
        Me.btnCLose.Name = "btnCLose"
        Me.btnCLose.Size = New System.Drawing.Size(90, 22)
        Me.btnCLose.TabIndex = 2
        Me.btnCLose.Text = "&Cancel/Close"
        Me.btnCLose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnSave.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnSave.ImageIndex = 10
        Me.btnSave.ImageList = Me.ImageList1
        Me.btnSave.Location = New System.Drawing.Point(261, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(76, 22)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "&Save"
        Me.btnSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'pnlPrint
        '
        Me.pnlPrint.CanvasColor = System.Drawing.SystemColors.Control
        Me.pnlPrint.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.pnlPrint.Controls.Add(Me.btnSave)
        Me.pnlPrint.Controls.Add(Me.btnCLose)
        Me.pnlPrint.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPrint.Location = New System.Drawing.Point(0, 249)
        Me.pnlPrint.Name = "pnlPrint"
        Me.pnlPrint.Size = New System.Drawing.Size(438, 30)
        Me.pnlPrint.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlPrint.Style.BackColor1.Color = System.Drawing.SystemColors.MenuBar
        Me.pnlPrint.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlPrint.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.pnlPrint.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.pnlPrint.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.pnlPrint.Style.GradientAngle = 90
        Me.pnlPrint.TabIndex = 25
        '
        'btnSearchDistributor
        '
        Me.btnSearchDistributor.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchDistributor.Location = New System.Drawing.Point(407, 167)
        Me.btnSearchDistributor.Name = "btnSearchDistributor"
        Me.btnSearchDistributor.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchDistributor.TabIndex = 31
        '
        'btnSearchBrandPack
        '
        Me.btnSearchBrandPack.BackColor = System.Drawing.Color.Transparent
        Me.btnSearchBrandPack.Location = New System.Drawing.Point(407, 193)
        Me.btnSearchBrandPack.Name = "btnSearchBrandPack"
        Me.btnSearchBrandPack.Size = New System.Drawing.Size(17, 18)
        Me.btnSearchBrandPack.TabIndex = 32
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(21, 112)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(117, 13)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "APPLY ADJUSTMENT"
        '
        'rdbPerDistributor
        '
        Me.rdbPerDistributor.AutoSize = True
        Me.rdbPerDistributor.Location = New System.Drawing.Point(153, 110)
        Me.rdbPerDistributor.Name = "rdbPerDistributor"
        Me.rdbPerDistributor.Size = New System.Drawing.Size(124, 17)
        Me.rdbPerDistributor.TabIndex = 34
        Me.rdbPerDistributor.TabStop = True
        Me.rdbPerDistributor.Text = "PER DISTRIBUTOR"
        Me.rdbPerDistributor.UseVisualStyleBackColor = True
        '
        'rdbGroupDistributor
        '
        Me.rdbGroupDistributor.AutoSize = True
        Me.rdbGroupDistributor.Location = New System.Drawing.Point(283, 110)
        Me.rdbGroupDistributor.Name = "rdbGroupDistributor"
        Me.rdbGroupDistributor.Size = New System.Drawing.Size(141, 17)
        Me.rdbGroupDistributor.TabIndex = 35
        Me.rdbGroupDistributor.TabStop = True
        Me.rdbGroupDistributor.Text = "GROUP DISTRIBUTOR"
        Me.rdbGroupDistributor.UseVisualStyleBackColor = True
        '
        'ChkDistributor
        '
        Me.ChkDistributor.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        ChkDistributor_DesignTimeLayout.LayoutString = resources.GetString("ChkDistributor_DesignTimeLayout.LayoutString")
        Me.ChkDistributor.DesignTimeLayout = ChkDistributor_DesignTimeLayout
        Me.ChkDistributor.DropDownDisplayMember = "DISTRIBUTOR_NAME"
        Me.ChkDistributor.DropDownValueMember = "DISTRIBUTOR_ID"
        Me.ChkDistributor.Location = New System.Drawing.Point(152, 165)
        Me.ChkDistributor.Name = "ChkDistributor"
        Me.ChkDistributor.SaveSettings = False
        Me.ChkDistributor.Size = New System.Drawing.Size(249, 20)
        Me.ChkDistributor.TabIndex = 36
        Me.ChkDistributor.ValuesDataMember = Nothing
        Me.ChkDistributor.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'frmEntryAdjPKD
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.CancelButton = Me.btnCLose
        Me.ClientSize = New System.Drawing.Size(438, 279)
        Me.Controls.Add(Me.ChkDistributor)
        Me.Controls.Add(Me.rdbGroupDistributor)
        Me.Controls.Add(Me.rdbPerDistributor)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnSearchBrandPack)
        Me.Controls.Add(Me.btnSearchDistributor)
        Me.Controls.Add(Me.pnlPrint)
        Me.Controls.Add(Me.txtMaxQty)
        Me.Controls.Add(Me.cmbAdjustmentFor)
        Me.Controls.Add(Me.txtAdjusmentName)
        Me.Controls.Add(Me.mcbBrandPack)
        Me.Controls.Add(Me.mcbDistributor)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Name = "frmEntryAdjPKD"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Entry Master Form Adjustment"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.mcbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mcbBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPrint.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents mcbDistributor As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents txtAdjusmentName As System.Windows.Forms.TextBox
    Friend WithEvents cmbAdjustmentFor As System.Windows.Forms.ComboBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents btnCLose As Janus.Windows.EditControls.UIButton
    Private WithEvents btnSave As Janus.Windows.EditControls.UIButton
    Private WithEvents pnlPrint As DevComponents.DotNetBar.PanelEx
    Friend WithEvents dtPicStartPeriode As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtPicEndPeriode As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtMaxQty As Janus.Windows.GridEX.EditControls.NumericEditBox
    Friend WithEvents mcbBrandPack As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Private WithEvents btnSearchDistributor As DTSProjects.ButtonSearch
    Private WithEvents btnSearchBrandPack As DTSProjects.ButtonSearch
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents rdbPerDistributor As System.Windows.Forms.RadioButton
    Friend WithEvents ChkDistributor As Janus.Windows.GridEX.EditControls.CheckedComboBox
    Protected Friend WithEvents rdbGroupDistributor As System.Windows.Forms.RadioButton
End Class
