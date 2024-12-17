<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OtherProduct
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OtherProduct))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSettings = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterEqual = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnAddNew = New DevComponents.DotNetBar.ButtonItem
        Me.btnEdit = New DevComponents.DotNetBar.ButtonItem
        Me.btnSave = New DevComponents.DotNetBar.ButtonItem
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.AdvancedTManager1 = New DTSProjects.AdvancedTManager
        Me.pnlEntry = New System.Windows.Forms.Panel
        Me.btnClose = New System.Windows.Forms.Button
        Me.txtUOM = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtVol2 = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtVol1 = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.txtUnit1 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtUnit2 = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.lblRemark = New System.Windows.Forms.Label
        Me.txtItemName = New System.Windows.Forms.TextBox
        Me.lblItemName = New System.Windows.Forms.Label
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.txtDevQTy = New Janus.Windows.GridEX.EditControls.NumericEditBox
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlEntry.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Grid.bmp")
        Me.ImageList1.Images.SetKeyName(1, "Filter 48 h p.ico")
        Me.ImageList1.Images.SetKeyName(2, "Customize.png")
        Me.ImageList1.Images.SetKeyName(3, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(4, "Save.png")
        Me.ImageList1.Images.SetKeyName(5, "TextEdit.png")
        Me.ImageList1.Images.SetKeyName(6, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(7, "gridColumn.png")
        Me.ImageList1.Images.SetKeyName(8, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(9, "printer.ico")
        Me.ImageList1.Images.SetKeyName(10, "Export.bmp")
        Me.ImageList1.Images.SetKeyName(11, "DB_Refresh.ico")
        Me.ImageList1.Images.SetKeyName(12, "PageSetup.BMP")
        Me.ImageList1.Images.SetKeyName(13, "generator.ico")
        Me.ImageList1.Images.SetKeyName(14, "Discount.ICO")
        Me.ImageList1.Images.SetKeyName(15, "Save.png")
        Me.ImageList1.Images.SetKeyName(16, "Add.png")
        Me.ImageList1.Images.SetKeyName(17, "cancel_doc.ico")
        Me.ImageList1.Images.SetKeyName(18, "Add.bmp")
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.IncludeFormatStyle = False
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar2 (Bar2)"
        Me.Bar2.AccessibleName = "Bar2"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaption
        Me.Bar2.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaptionText
        Me.Bar2.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnExport, Me.btnRefresh, Me.btnAddNew, Me.btnEdit, Me.btnSave})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(844, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 26
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 0
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnColumn, Me.btnSettingGrid, Me.btnPrint, Me.btnPageSettings})
        Me.btnGrid.Text = "Grid"
        '
        'btnColumn
        '
        Me.btnColumn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnColumn.ImageIndex = 7
        Me.btnColumn.Name = "btnColumn"
        Me.btnColumn.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnShowFieldChooser})
        Me.btnColumn.Text = "Grid Column"
        '
        'btnShowFieldChooser
        '
        Me.btnShowFieldChooser.Name = "btnShowFieldChooser"
        Me.btnShowFieldChooser.Text = "Show Field Chooser"
        Me.btnShowFieldChooser.Tooltip = "show / remove column by draging  / dropping any column in datagrid"
        '
        'btnSettingGrid
        '
        Me.btnSettingGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSettingGrid.ImageIndex = 2
        Me.btnSettingGrid.Name = "btnSettingGrid"
        Me.btnSettingGrid.Text = "Setting Grid"
        Me.btnSettingGrid.Tooltip = "use this item to show grid by your own needs ,can also for defining printing grid" & _
            ""
        '
        'btnPrint
        '
        Me.btnPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrint.ImageIndex = 9
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Text = "Print data Grid"
        Me.btnPrint.Tooltip = "print datagrid,all visible rows in datagrid will be printed"
        '
        'btnPageSettings
        '
        Me.btnPageSettings.ImageIndex = 12
        Me.btnPageSettings.Name = "btnPageSettings"
        Me.btnPageSettings.Text = "Page Settings"
        Me.btnPageSettings.Tooltip = "setting datagrid page ,use this setting if you want to print datagrid with page s" & _
            "etting defined by yourseef "
        '
        'btnFilter
        '
        Me.btnFilter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnFilter.ImageIndex = 1
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCustomFilter, Me.btnFilterEqual})
        Me.btnFilter.Text = "Filter Row"
        '
        'btnCustomFilter
        '
        Me.btnCustomFilter.Name = "btnCustomFilter"
        Me.btnCustomFilter.Text = "Custom Filter"
        Me.btnCustomFilter.Tooltip = "use this filter to filter data in datagrid  by your own criteria"
        '
        'btnFilterEqual
        '
        Me.btnFilterEqual.Name = "btnFilterEqual"
        Me.btnFilterEqual.Text = "Default"
        Me.btnFilterEqual.Tooltip = "use this filter to filter data in data grid directly from datagrid with contains " & _
            "criteria"
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 10
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 11
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.Tooltip = "Reload all possible data row  and refresh grid"
        '
        'btnAddNew
        '
        Me.btnAddNew.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnAddNew.ImageIndex = 18
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Text = "Add New"
        '
        'btnEdit
        '
        Me.btnEdit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnEdit.ImageIndex = 6
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Text = "Edit"
        '
        'btnSave
        '
        Me.btnSave.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSave.ImageIndex = 8
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS)
        Me.btnSave.Text = "Save"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.AdvancedTManager1)
        Me.Panel1.Controls.Add(Me.pnlEntry)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(844, 495)
        Me.Panel1.TabIndex = 27
        '
        'AdvancedTManager1
        '
        Me.AdvancedTManager1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdvancedTManager1.Location = New System.Drawing.Point(0, 213)
        Me.AdvancedTManager1.Name = "AdvancedTManager1"
        Me.AdvancedTManager1.Size = New System.Drawing.Size(844, 282)
        Me.AdvancedTManager1.TabIndex = 0
        '
        'pnlEntry
        '
        Me.pnlEntry.Controls.Add(Me.txtDevQTy)
        Me.pnlEntry.Controls.Add(Me.Label1)
        Me.pnlEntry.Controls.Add(Me.btnClose)
        Me.pnlEntry.Controls.Add(Me.txtUOM)
        Me.pnlEntry.Controls.Add(Me.Label2)
        Me.pnlEntry.Controls.Add(Me.txtVol2)
        Me.pnlEntry.Controls.Add(Me.txtVol1)
        Me.pnlEntry.Controls.Add(Me.txtUnit1)
        Me.pnlEntry.Controls.Add(Me.Label6)
        Me.pnlEntry.Controls.Add(Me.txtUnit2)
        Me.pnlEntry.Controls.Add(Me.Label5)
        Me.pnlEntry.Controls.Add(Me.Label4)
        Me.pnlEntry.Controls.Add(Me.Label3)
        Me.pnlEntry.Controls.Add(Me.txtRemark)
        Me.pnlEntry.Controls.Add(Me.lblRemark)
        Me.pnlEntry.Controls.Add(Me.txtItemName)
        Me.pnlEntry.Controls.Add(Me.lblItemName)
        Me.pnlEntry.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEntry.Location = New System.Drawing.Point(0, 0)
        Me.pnlEntry.Name = "pnlEntry"
        Me.pnlEntry.Size = New System.Drawing.Size(844, 213)
        Me.pnlEntry.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(106, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.ImageIndex = 17
        Me.btnClose.ImageList = Me.ImageList1
        Me.btnClose.Location = New System.Drawing.Point(759, 154)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "&Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtUOM
        '
        Me.txtUOM.Location = New System.Drawing.Point(80, 135)
        Me.txtUOM.MaxLength = 10
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.Size = New System.Drawing.Size(124, 20)
        Me.txtUOM.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 138)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "U O M"
        '
        'txtVol2
        '
        Me.txtVol2.DecimalDigits = 3
        Me.txtVol2.Location = New System.Drawing.Point(79, 109)
        Me.txtVol2.Name = "txtVol2"
        Me.txtVol2.Size = New System.Drawing.Size(125, 20)
        Me.txtVol2.TabIndex = 4
        Me.txtVol2.Text = "0.000"
        Me.txtVol2.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        '
        'txtVol1
        '
        Me.txtVol1.DecimalDigits = 3
        Me.txtVol1.Location = New System.Drawing.Point(80, 57)
        Me.txtVol1.Name = "txtVol1"
        Me.txtVol1.Size = New System.Drawing.Size(124, 20)
        Me.txtVol1.TabIndex = 2
        Me.txtVol1.Text = "0.000"
        Me.txtVol1.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        '
        'txtUnit1
        '
        Me.txtUnit1.Location = New System.Drawing.Point(80, 31)
        Me.txtUnit1.MaxLength = 30
        Me.txtUnit1.Name = "txtUnit1"
        Me.txtUnit1.Size = New System.Drawing.Size(142, 20)
        Me.txtUnit1.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "VOLUME 2"
        '
        'txtUnit2
        '
        Me.txtUnit2.Location = New System.Drawing.Point(80, 83)
        Me.txtUnit2.MaxLength = 30
        Me.txtUnit2.Name = "txtUnit2"
        Me.txtUnit2.Size = New System.Drawing.Size(142, 20)
        Me.txtUnit2.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "UNIT 2"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "VOLUME"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "UNIT 1"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(80, 161)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(368, 20)
        Me.txtRemark.TabIndex = 6
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.Location = New System.Drawing.Point(12, 164)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(44, 13)
        Me.lblRemark.TabIndex = 2
        Me.lblRemark.Text = "Remark"
        '
        'txtItemName
        '
        Me.txtItemName.Location = New System.Drawing.Point(80, 5)
        Me.txtItemName.MaxLength = 150
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Size = New System.Drawing.Size(368, 20)
        Me.txtItemName.TabIndex = 0
        '
        'lblItemName
        '
        Me.lblItemName.AutoSize = True
        Me.lblItemName.Location = New System.Drawing.Point(12, 8)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.Size = New System.Drawing.Size(58, 13)
        Me.lblItemName.TabIndex = 0
        Me.lblItemName.Text = "Item Name"
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.Transparent
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Equal
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(0, 25)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(0, 0)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(844, 29)
        Me.FilterEditor1.Visible = False
        '
        'txtDevQTy
        '
        Me.txtDevQTy.DecimalDigits = 3
        Me.txtDevQTy.Location = New System.Drawing.Point(79, 187)
        Me.txtDevQTy.Name = "txtDevQTy"
        Me.txtDevQTy.Size = New System.Drawing.Size(125, 20)
        Me.txtDevQTy.TabIndex = 16
        Me.txtDevQTy.Text = "0.000"
        Me.txtDevQTy.Value = New Decimal(New Integer() {0, 0, 0, 196608})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 190)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Devided_qty"
        '
        'OtherProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(844, 549)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Name = "OtherProduct"
        Me.Text = "Others Product"
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.pnlEntry.ResumeLayout(False)
        Me.pnlEntry.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlEntry As System.Windows.Forms.Panel
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Friend WithEvents AdvancedTManager1 As DTSProjects.AdvancedTManager
    Private WithEvents txtItemName As System.Windows.Forms.TextBox
    Private WithEvents txtUnit1 As System.Windows.Forms.TextBox
    Private WithEvents txtUnit2 As System.Windows.Forms.TextBox
    Private WithEvents txtVol1 As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents txtRemark As System.Windows.Forms.TextBox
    Private WithEvents txtVol2 As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents lblRemark As System.Windows.Forms.Label
    Private WithEvents lblItemName As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents txtUOM As System.Windows.Forms.TextBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents btnClose As System.Windows.Forms.Button
    Private WithEvents btnSave As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnEdit As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents txtDevQTy As Janus.Windows.GridEX.EditControls.NumericEditBox
    Private WithEvents Label1 As System.Windows.Forms.Label

End Class
