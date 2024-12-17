<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QtyConvertion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QtyConvertion))
        Dim mcbBrandPack_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim grdMeasure_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.UiGroupBox1 = New Janus.Windows.EditControls.UIGroupBox
        Me.btnInsert = New Janus.Windows.EditControls.UIButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.UiGroupBox3 = New Janus.Windows.EditControls.UIGroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCollyPackSize = New System.Windows.Forms.TextBox
        Me.txtCollyBox = New System.Windows.Forms.TextBox
        Me.txtUnitOfMeasure = New System.Windows.Forms.TextBox
        Me.UiGroupBox2 = New Janus.Windows.EditControls.UIGroupBox
        Me.txtQtyToCalculate = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.mcbBrandPack = New Janus.Windows.GridEX.EditControls.MultiColumnCombo
        Me.ButtonSearch1 = New DTSProjects.ButtonSearch
        Me.grpGonDetail = New Janus.Windows.EditControls.UIGroupBox
        Me.grdMeasure = New Janus.Windows.GridEX.GridEX
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.UiButton1 = New Janus.Windows.EditControls.UIButton
        Me.btnPrint = New Janus.Windows.EditControls.UIButton
        Me.btnAddNew = New Janus.Windows.EditControls.UIButton
        Me.btnClose = New Janus.Windows.EditControls.UIButton
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox1.SuspendLayout()
        CType(Me.UiGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox3.SuspendLayout()
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UiGroupBox2.SuspendLayout()
        CType(Me.mcbBrandPack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grpGonDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGonDetail.SuspendLayout()
        CType(Me.grdMeasure, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UiGroupBox1
        '
        Me.UiGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.UiGroupBox1.Controls.Add(Me.btnInsert)
        Me.UiGroupBox1.Controls.Add(Me.UiGroupBox3)
        Me.UiGroupBox1.Controls.Add(Me.UiGroupBox2)
        Me.UiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UiGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.UiGroupBox1.Name = "UiGroupBox1"
        Me.UiGroupBox1.Size = New System.Drawing.Size(738, 145)
        Me.UiGroupBox1.TabIndex = 5
        Me.UiGroupBox1.Text = "Enter Quantity to see the convertion result"
        Me.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'btnInsert
        '
        Me.btnInsert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnInsert.ImageIndex = 1
        Me.btnInsert.ImageList = Me.ImageList1
        Me.btnInsert.Location = New System.Drawing.Point(645, 115)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(81, 27)
        Me.btnInsert.TabIndex = 0
        Me.btnInsert.Text = "&Insert"
        Me.btnInsert.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Add.bmp")
        Me.ImageList1.Images.SetKeyName(1, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(2, "Close.ico")
        Me.ImageList1.Images.SetKeyName(3, "printer.ico")
        Me.ImageList1.Images.SetKeyName(4, "Export.bmp")
        '
        'UiGroupBox3
        '
        Me.UiGroupBox3.Controls.Add(Me.Label3)
        Me.UiGroupBox3.Controls.Add(Me.Label2)
        Me.UiGroupBox3.Controls.Add(Me.Label1)
        Me.UiGroupBox3.Controls.Add(Me.txtCollyPackSize)
        Me.UiGroupBox3.Controls.Add(Me.txtCollyBox)
        Me.UiGroupBox3.Controls.Add(Me.txtUnitOfMeasure)
        Me.UiGroupBox3.Location = New System.Drawing.Point(360, 19)
        Me.UiGroupBox3.Name = "UiGroupBox3"
        Me.UiGroupBox3.Size = New System.Drawing.Size(366, 94)
        Me.UiGroupBox3.TabIndex = 24
        Me.UiGroupBox3.Text = "Calculation Result"
        Me.UiGroupBox3.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Colly Packsize"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Colly Box"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Unit Of Measure"
        '
        'txtCollyPackSize
        '
        Me.txtCollyPackSize.Location = New System.Drawing.Point(100, 68)
        Me.txtCollyPackSize.MaxLength = 50
        Me.txtCollyPackSize.Name = "txtCollyPackSize"
        Me.txtCollyPackSize.ReadOnly = True
        Me.txtCollyPackSize.Size = New System.Drawing.Size(260, 20)
        Me.txtCollyPackSize.TabIndex = 23
        '
        'txtCollyBox
        '
        Me.txtCollyBox.Location = New System.Drawing.Point(100, 42)
        Me.txtCollyBox.MaxLength = 50
        Me.txtCollyBox.Name = "txtCollyBox"
        Me.txtCollyBox.ReadOnly = True
        Me.txtCollyBox.Size = New System.Drawing.Size(260, 20)
        Me.txtCollyBox.TabIndex = 22
        '
        'txtUnitOfMeasure
        '
        Me.txtUnitOfMeasure.Location = New System.Drawing.Point(100, 14)
        Me.txtUnitOfMeasure.MaxLength = 50
        Me.txtUnitOfMeasure.Name = "txtUnitOfMeasure"
        Me.txtUnitOfMeasure.ReadOnly = True
        Me.txtUnitOfMeasure.Size = New System.Drawing.Size(260, 20)
        Me.txtUnitOfMeasure.TabIndex = 14
        '
        'UiGroupBox2
        '
        Me.UiGroupBox2.Controls.Add(Me.txtQtyToCalculate)
        Me.UiGroupBox2.Controls.Add(Me.mcbBrandPack)
        Me.UiGroupBox2.Controls.Add(Me.ButtonSearch1)
        Me.UiGroupBox2.Location = New System.Drawing.Point(12, 19)
        Me.UiGroupBox2.Name = "UiGroupBox2"
        Me.UiGroupBox2.Size = New System.Drawing.Size(342, 94)
        Me.UiGroupBox2.TabIndex = 23
        Me.UiGroupBox2.Text = "Enter BrandPack & Qty"
        Me.UiGroupBox2.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'txtQtyToCalculate
        '
        Me.txtQtyToCalculate.ButtonText = "Enter value"
        Me.txtQtyToCalculate.HoverMode = Janus.Windows.GridEX.HoverMode.Highlight
        Me.txtQtyToCalculate.Location = New System.Drawing.Point(15, 51)
        Me.txtQtyToCalculate.Name = "txtQtyToCalculate"
        Me.txtQtyToCalculate.Size = New System.Drawing.Size(207, 20)
        Me.txtQtyToCalculate.TabIndex = 1
        Me.txtQtyToCalculate.Text = "0.00"
        Me.txtQtyToCalculate.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        Me.txtQtyToCalculate.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'mcbBrandPack
        '
        mcbBrandPack_DesignTimeLayout.LayoutString = resources.GetString("mcbBrandPack_DesignTimeLayout.LayoutString")
        Me.mcbBrandPack.DesignTimeLayout = mcbBrandPack_DesignTimeLayout
        Me.mcbBrandPack.DisplayMember = "BRANDPACK_NAME"
        Me.mcbBrandPack.Location = New System.Drawing.Point(15, 25)
        Me.mcbBrandPack.Name = "mcbBrandPack"
        Me.mcbBrandPack.SelectedIndex = -1
        Me.mcbBrandPack.SelectedItem = Nothing
        Me.mcbBrandPack.Size = New System.Drawing.Size(298, 20)
        Me.mcbBrandPack.TabIndex = 0
        Me.mcbBrandPack.ValueMember = "BRANDPACK_ID"
        Me.mcbBrandPack.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ButtonSearch1
        '
        Me.ButtonSearch1.Location = New System.Drawing.Point(319, 25)
        Me.ButtonSearch1.Name = "ButtonSearch1"
        Me.ButtonSearch1.Size = New System.Drawing.Size(17, 18)
        Me.ButtonSearch1.TabIndex = 21
        '
        'grpGonDetail
        '
        Me.grpGonDetail.Controls.Add(Me.grdMeasure)
        Me.grpGonDetail.Controls.Add(Me.PanelEx1)
        Me.grpGonDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpGonDetail.Location = New System.Drawing.Point(0, 145)
        Me.grpGonDetail.Name = "grpGonDetail"
        Me.grpGonDetail.Size = New System.Drawing.Size(738, 321)
        Me.grpGonDetail.TabIndex = 18
        Me.grpGonDetail.Text = "Result Catch"
        Me.grpGonDetail.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        '
        'grdMeasure
        '
        Me.grdMeasure.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdMeasure.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdMeasure.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdMeasure.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        grdMeasure_DesignTimeLayout.LayoutString = resources.GetString("grdMeasure_DesignTimeLayout.LayoutString")
        Me.grdMeasure.DesignTimeLayout = grdMeasure_DesignTimeLayout
        Me.grdMeasure.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdMeasure.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grdMeasure.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdMeasure.GroupByBoxVisible = False
        Me.grdMeasure.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdMeasure.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdMeasure.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.grdMeasure.Location = New System.Drawing.Point(3, 16)
        Me.grdMeasure.Name = "grdMeasure"
        Me.grdMeasure.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.grdMeasure.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.grdMeasure.SelectedInactiveFormatStyle.BackColor = System.Drawing.SystemColors.Window
        Me.grdMeasure.Size = New System.Drawing.Size(732, 268)
        Me.grdMeasure.TabIndex = 0
        Me.grdMeasure.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.grdMeasure.WatermarkImage.Image = CType(resources.GetObject("grdMeasure.WatermarkImage.Image"), System.Drawing.Image)
        Me.grdMeasure.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.UiButton1)
        Me.PanelEx1.Controls.Add(Me.btnPrint)
        Me.PanelEx1.Controls.Add(Me.btnAddNew)
        Me.PanelEx1.Controls.Add(Me.btnClose)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx1.Location = New System.Drawing.Point(3, 284)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(732, 34)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.StyleMouseDown.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.StyleMouseDown.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground
        Me.PanelEx1.StyleMouseDown.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2
        Me.PanelEx1.StyleMouseDown.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBorder
        Me.PanelEx1.StyleMouseDown.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedText
        Me.PanelEx1.StyleMouseOver.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.StyleMouseOver.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground
        Me.PanelEx1.StyleMouseOver.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBackground2
        Me.PanelEx1.StyleMouseOver.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotBorder
        Me.PanelEx1.StyleMouseOver.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemHotText
        Me.PanelEx1.TabIndex = 4
        '
        'UiButton1
        '
        Me.UiButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UiButton1.ImageIndex = 4
        Me.UiButton1.ImageList = Me.ImageList1
        Me.UiButton1.Location = New System.Drawing.Point(543, 4)
        Me.UiButton1.Name = "UiButton1"
        Me.UiButton1.Size = New System.Drawing.Size(99, 27)
        Me.UiButton1.TabIndex = 1
        Me.UiButton1.Text = "&Export to Excel"
        Me.UiButton1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnPrint
        '
        Me.btnPrint.Appearance = Janus.Windows.UI.Appearance.FlatBorderless
        Me.btnPrint.ImageIndex = 3
        Me.btnPrint.ImageList = Me.ImageList1
        Me.btnPrint.Location = New System.Drawing.Point(113, 7)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(110, 23)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "&Print Preview"
        Me.btnPrint.VisualStyle = Janus.Windows.UI.VisualStyle.Office2003
        '
        'btnAddNew
        '
        Me.btnAddNew.ImageList = Me.ImageList1
        Me.btnAddNew.Location = New System.Drawing.Point(3, 5)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(104, 25)
        Me.btnAddNew.TabIndex = 2
        Me.btnAddNew.Text = "Clear Grid Data"
        Me.btnAddNew.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.ImageIndex = 2
        Me.btnClose.ImageList = Me.ImageList1
        Me.btnClose.Location = New System.Drawing.Point(648, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(81, 27)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "&Close"
        Me.btnClose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'QtyConvertion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(738, 466)
        Me.Controls.Add(Me.grpGonDetail)
        Me.Controls.Add(Me.UiGroupBox1)
        Me.Name = "QtyConvertion"
        CType(Me.UiGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox1.ResumeLayout(False)
        CType(Me.UiGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox3.ResumeLayout(False)
        Me.UiGroupBox3.PerformLayout()
        CType(Me.UiGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UiGroupBox2.ResumeLayout(False)
        Me.UiGroupBox2.PerformLayout()
        CType(Me.mcbBrandPack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grpGonDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGonDetail.ResumeLayout(False)
        CType(Me.grdMeasure, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents UiGroupBox1 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents txtUnitOfMeasure As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSearch1 As DTSProjects.ButtonSearch
    Private WithEvents grpGonDetail As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents grdMeasure As Janus.Windows.GridEX.GridEX
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnPrint As Janus.Windows.EditControls.UIButton
    Public WithEvents btnAddNew As Janus.Windows.EditControls.UIButton
    Public WithEvents btnClose As Janus.Windows.EditControls.UIButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents txtCollyBox As System.Windows.Forms.TextBox
    Friend WithEvents UiGroupBox2 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents UiGroupBox3 As Janus.Windows.EditControls.UIGroupBox
    Friend WithEvents txtCollyPackSize As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents btnInsert As Janus.Windows.EditControls.UIButton
    Public WithEvents UiButton1 As Janus.Windows.EditControls.UIButton
    Friend WithEvents mcbBrandPack As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents txtQtyToCalculate As Janus.Windows.GridEX.EditControls.NumericEditBox

End Class
