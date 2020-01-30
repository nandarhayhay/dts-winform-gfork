<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageDPRD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManageDPRD))
        Me.NavigationPane1 = New DevComponents.DotNetBar.NavigationPane
        Me.pnlAttachment = New DevComponents.DotNetBar.NavigationPanePanel
        Me.rdbAchievementDPRDM = New System.Windows.Forms.RadioButton
        Me.rdbPO = New System.Windows.Forms.RadioButton
        Me.rdbReaching = New System.Windows.Forms.RadioButton
        Me.rdbDPRD = New System.Windows.Forms.RadioButton
        Me.rdbKios = New System.Windows.Forms.RadioButton
        Me.btnAttachment = New DevComponents.DotNetBar.ButtonItem
        Me.pnlViewData = New DevComponents.DotNetBar.NavigationPanePanel
        Me.rdbViewAchievementDPRDM = New System.Windows.Forms.RadioButton
        Me.rdbViewReachingTargetKios = New System.Windows.Forms.RadioButton
        Me.rdbViewPOKios = New System.Windows.Forms.RadioButton
        Me.rdbnViewKios = New System.Windows.Forms.RadioButton
        Me.btnViewData = New DevComponents.DotNetBar.ButtonItem
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnPageSettings = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterEqual = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnRenameColumn = New DevComponents.DotNetBar.ButtonItem
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        Me.XpGradientPanel1 = New SteepValley.Windows.Forms.XPGradientPanel
        Me.NavigationPane1.SuspendLayout()
        Me.pnlAttachment.SuspendLayout()
        Me.pnlViewData.SuspendLayout()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NavigationPane1
        '
        Me.NavigationPane1.CanCollapse = True
        Me.NavigationPane1.Controls.Add(Me.pnlAttachment)
        Me.NavigationPane1.Controls.Add(Me.pnlViewData)
        Me.NavigationPane1.Dock = System.Windows.Forms.DockStyle.Left
        Me.NavigationPane1.Images = Me.ImageList1
        Me.NavigationPane1.ItemPaddingBottom = 2
        Me.NavigationPane1.ItemPaddingTop = 2
        Me.NavigationPane1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnAttachment, Me.btnViewData})
        Me.NavigationPane1.Location = New System.Drawing.Point(0, 25)
        Me.NavigationPane1.Name = "NavigationPane1"
        Me.NavigationPane1.NavigationBarHeight = 102
        Me.NavigationPane1.Padding = New System.Windows.Forms.Padding(1)
        Me.NavigationPane1.Size = New System.Drawing.Size(139, 717)
        Me.NavigationPane1.TabIndex = 0
        '
        '
        '
        Me.NavigationPane1.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.NavigationPane1.TitlePanel.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NavigationPane1.TitlePanel.Location = New System.Drawing.Point(1, 1)
        Me.NavigationPane1.TitlePanel.Name = "panelTitle"
        Me.NavigationPane1.TitlePanel.Size = New System.Drawing.Size(137, 19)
        Me.NavigationPane1.TitlePanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.NavigationPane1.TitlePanel.Style.BackColor2.Color = System.Drawing.SystemColors.InactiveCaption
        Me.NavigationPane1.TitlePanel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.NavigationPane1.TitlePanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.NavigationPane1.TitlePanel.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Top) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.NavigationPane1.TitlePanel.Style.ForeColor.Color = System.Drawing.Color.Black
        Me.NavigationPane1.TitlePanel.Style.GradientAngle = 90
        Me.NavigationPane1.TitlePanel.Style.MarginLeft = 4
        Me.NavigationPane1.TitlePanel.TabIndex = 0
        Me.NavigationPane1.TitlePanel.Text = "Attachment"
        '
        'pnlAttachment
        '
        Me.pnlAttachment.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.pnlAttachment.Controls.Add(Me.rdbAchievementDPRDM)
        Me.pnlAttachment.Controls.Add(Me.rdbPO)
        Me.pnlAttachment.Controls.Add(Me.rdbReaching)
        Me.pnlAttachment.Controls.Add(Me.rdbDPRD)
        Me.pnlAttachment.Controls.Add(Me.rdbKios)
        Me.pnlAttachment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAttachment.Location = New System.Drawing.Point(1, 20)
        Me.pnlAttachment.Name = "pnlAttachment"
        Me.pnlAttachment.ParentItem = Me.btnAttachment
        Me.pnlAttachment.Size = New System.Drawing.Size(137, 594)
        Me.pnlAttachment.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlAttachment.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlAttachment.Style.BackColor2.Color = System.Drawing.SystemColors.MenuBar
        Me.pnlAttachment.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.pnlAttachment.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.pnlAttachment.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.pnlAttachment.Style.GradientAngle = 90
        Me.pnlAttachment.TabIndex = 2
        '
        'rdbAchievementDPRDM
        '
        Me.rdbAchievementDPRDM.AutoSize = True
        Me.rdbAchievementDPRDM.Location = New System.Drawing.Point(4, 122)
        Me.rdbAchievementDPRDM.Name = "rdbAchievementDPRDM"
        Me.rdbAchievementDPRDM.Size = New System.Drawing.Size(127, 17)
        Me.rdbAchievementDPRDM.TabIndex = 4
        Me.rdbAchievementDPRDM.TabStop = True
        Me.rdbAchievementDPRDM.Text = "AchievementDPRDM"
        Me.rdbAchievementDPRDM.UseVisualStyleBackColor = True
        '
        'rdbPO
        '
        Me.rdbPO.AutoSize = True
        Me.rdbPO.Location = New System.Drawing.Point(5, 157)
        Me.rdbPO.Name = "rdbPO"
        Me.rdbPO.Size = New System.Drawing.Size(71, 17)
        Me.rdbPO.TabIndex = 3
        Me.rdbPO.TabStop = True
        Me.rdbPO.Text = "Nota Kios"
        Me.rdbPO.UseVisualStyleBackColor = True
        '
        'rdbReaching
        '
        Me.rdbReaching.AutoSize = True
        Me.rdbReaching.Location = New System.Drawing.Point(4, 89)
        Me.rdbReaching.Name = "rdbReaching"
        Me.rdbReaching.Size = New System.Drawing.Size(128, 17)
        Me.rdbReaching.TabIndex = 2
        Me.rdbReaching.TabStop = True
        Me.rdbReaching.Text = "Achievement DPRDS"
        Me.rdbReaching.UseVisualStyleBackColor = True
        '
        'rdbDPRD
        '
        Me.rdbDPRD.AutoSize = True
        Me.rdbDPRD.Location = New System.Drawing.Point(5, 51)
        Me.rdbDPRD.Name = "rdbDPRD"
        Me.rdbDPRD.Size = New System.Drawing.Size(117, 17)
        Me.rdbDPRD.TabIndex = 1
        Me.rdbDPRD.TabStop = True
        Me.rdbDPRD.Text = "Agreement DPRDS"
        Me.rdbDPRD.UseVisualStyleBackColor = True
        '
        'rdbKios
        '
        Me.rdbKios.AutoSize = True
        Me.rdbKios.Location = New System.Drawing.Point(5, 13)
        Me.rdbKios.Name = "rdbKios"
        Me.rdbKios.Size = New System.Drawing.Size(71, 17)
        Me.rdbKios.TabIndex = 0
        Me.rdbKios.TabStop = True
        Me.rdbKios.Text = "Data Kios"
        Me.rdbKios.UseVisualStyleBackColor = True
        '
        'btnAttachment
        '
        Me.btnAttachment.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnAttachment.Checked = True
        Me.btnAttachment.ImageIndex = 17
        Me.btnAttachment.Name = "btnAttachment"
        Me.btnAttachment.OptionGroup = "navBar"
        Me.btnAttachment.Text = "Attachment"
        '
        'pnlViewData
        '
        Me.pnlViewData.Controls.Add(Me.rdbViewAchievementDPRDM)
        Me.pnlViewData.Controls.Add(Me.rdbViewReachingTargetKios)
        Me.pnlViewData.Controls.Add(Me.rdbViewPOKios)
        Me.pnlViewData.Controls.Add(Me.rdbnViewKios)
        Me.pnlViewData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlViewData.Location = New System.Drawing.Point(1, 1)
        Me.pnlViewData.Name = "pnlViewData"
        Me.pnlViewData.ParentItem = Me.btnViewData
        Me.pnlViewData.Size = New System.Drawing.Size(137, 613)
        Me.pnlViewData.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlViewData.Style.BackColor1.Color = System.Drawing.SystemColors.InactiveCaption
        Me.pnlViewData.Style.BackColor2.Color = System.Drawing.SystemColors.InactiveCaptionText
        Me.pnlViewData.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.pnlViewData.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.pnlViewData.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.pnlViewData.Style.GradientAngle = 90
        Me.pnlViewData.TabIndex = 3
        '
        'rdbViewAchievementDPRDM
        '
        Me.rdbViewAchievementDPRDM.AutoSize = True
        Me.rdbViewAchievementDPRDM.Location = New System.Drawing.Point(6, 137)
        Me.rdbViewAchievementDPRDM.Name = "rdbViewAchievementDPRDM"
        Me.rdbViewAchievementDPRDM.Size = New System.Drawing.Size(130, 17)
        Me.rdbViewAchievementDPRDM.TabIndex = 3
        Me.rdbViewAchievementDPRDM.TabStop = True
        Me.rdbViewAchievementDPRDM.Text = "Achievement DPRDM"
        Me.rdbViewAchievementDPRDM.UseVisualStyleBackColor = True
        '
        'rdbViewReachingTargetKios
        '
        Me.rdbViewReachingTargetKios.AutoSize = True
        Me.rdbViewReachingTargetKios.Location = New System.Drawing.Point(5, 93)
        Me.rdbViewReachingTargetKios.Name = "rdbViewReachingTargetKios"
        Me.rdbViewReachingTargetKios.Size = New System.Drawing.Size(131, 17)
        Me.rdbViewReachingTargetKios.TabIndex = 2
        Me.rdbViewReachingTargetKios.TabStop = True
        Me.rdbViewReachingTargetKios.Text = "Achievement  DPRDS"
        Me.rdbViewReachingTargetKios.UseVisualStyleBackColor = True
        '
        'rdbViewPOKios
        '
        Me.rdbViewPOKios.AutoSize = True
        Me.rdbViewPOKios.Location = New System.Drawing.Point(6, 51)
        Me.rdbViewPOKios.Name = "rdbViewPOKios"
        Me.rdbViewPOKios.Size = New System.Drawing.Size(71, 17)
        Me.rdbViewPOKios.TabIndex = 1
        Me.rdbViewPOKios.TabStop = True
        Me.rdbViewPOKios.Text = "Nota Kios"
        Me.rdbViewPOKios.UseVisualStyleBackColor = True
        '
        'rdbnViewKios
        '
        Me.rdbnViewKios.AutoSize = True
        Me.rdbnViewKios.Location = New System.Drawing.Point(7, 12)
        Me.rdbnViewKios.Name = "rdbnViewKios"
        Me.rdbnViewKios.Size = New System.Drawing.Size(45, 17)
        Me.rdbnViewKios.TabIndex = 0
        Me.rdbnViewKios.TabStop = True
        Me.rdbnViewKios.Text = "Kios"
        Me.rdbnViewKios.UseVisualStyleBackColor = True
        '
        'btnViewData
        '
        Me.btnViewData.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnViewData.Image = CType(resources.GetObject("btnViewData.Image"), System.Drawing.Image)
        Me.btnViewData.Name = "btnViewData"
        Me.btnViewData.OptionGroup = "navBar"
        Me.btnViewData.Text = "View Data"
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
        Me.ImageList1.Images.SetKeyName(8, "TextEdit.png")
        Me.ImageList1.Images.SetKeyName(9, "CK.ico.ico")
        Me.ImageList1.Images.SetKeyName(10, "EDIT.ICO")
        Me.ImageList1.Images.SetKeyName(11, "gridColumn.png")
        Me.ImageList1.Images.SetKeyName(12, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(13, "printer.ico")
        Me.ImageList1.Images.SetKeyName(14, "Export.bmp")
        Me.ImageList1.Images.SetKeyName(15, "DB_Refresh.ico")
        Me.ImageList1.Images.SetKeyName(16, "PageSetup.BMP")
        Me.ImageList1.Images.SetKeyName(17, "AttachmentHS.png")
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar2 (Bar2)"
        Me.Bar2.AccessibleName = "Bar2"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.BarType = DevComponents.DotNetBar.eBarType.MenuBar
        Me.Bar2.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaption
        Me.Bar2.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaptionText
        Me.Bar2.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.FadeEffect = True
        Me.Bar2.Images = Me.ImageList1
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnFilter, Me.btnExport})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1028, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 21
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 2
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnShowFieldChooser, Me.btnSettingGrid, Me.btnPrint, Me.btnPageSettings})
        Me.btnGrid.Text = "Grid"
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
        Me.btnSettingGrid.ImageIndex = 4
        Me.btnSettingGrid.Name = "btnSettingGrid"
        Me.btnSettingGrid.Text = "Setting Grid"
        Me.btnSettingGrid.Tooltip = "show / remove column by draging  / dropping any column in datagrid"
        '
        'btnPrint
        '
        Me.btnPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrint.ImageIndex = 13
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Text = "Print data Grid"
        Me.btnPrint.Tooltip = "print datagrid,all visible rows in datagrid will be printed"
        '
        'btnPageSettings
        '
        Me.btnPageSettings.ImageIndex = 16
        Me.btnPageSettings.Name = "btnPageSettings"
        Me.btnPageSettings.Text = "Page Settings"
        Me.btnPageSettings.Tooltip = "setting datagrid page ,use this setting if you want to print datagrid with page s" & _
            "etting defined by yourseef"
        '
        'btnFilter
        '
        Me.btnFilter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnFilter.ImageIndex = 3
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
        Me.btnFilterEqual.Tooltip = "use this filter to filter data in data grid directly from datagrid with equal cri" & _
            "teria"
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 14
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "E&xport Data"
        Me.btnExport.Tooltip = "export data in datagrid to excel format"
        '
        'btnRenameColumn
        '
        Me.btnRenameColumn.Name = "btnRenameColumn"
        Me.btnRenameColumn.Text = "Rename Column"
        Me.btnRenameColumn.Tooltip = "use this button to  rename any column header defined by yourself"
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(398, 298)
        Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.SizingColumns
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintCellBackground = False
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'FilterEditor1
        '
        Me.FilterEditor1.AutoApply = True
        Me.FilterEditor1.AutomaticHeightResize = True
        Me.FilterEditor1.BackColor = System.Drawing.Color.Transparent
        Me.FilterEditor1.DefaultConditionOperator = Janus.Data.ConditionOperator.Equal
        Me.FilterEditor1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterEditor1.InnerAreaStyle = Janus.Windows.UI.Dock.PanelInnerAreaStyle.UseFormatStyle
        Me.FilterEditor1.Location = New System.Drawing.Point(139, 25)
        Me.FilterEditor1.MinSize = New System.Drawing.Size(0, 0)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(889, 29)
        Me.FilterEditor1.SortFieldList = False
        '
        'XpGradientPanel1
        '
        Me.XpGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XpGradientPanel1.EndColor = System.Drawing.SystemColors.Menu
        Me.XpGradientPanel1.Location = New System.Drawing.Point(139, 54)
        Me.XpGradientPanel1.Name = "XpGradientPanel1"
        Me.XpGradientPanel1.Size = New System.Drawing.Size(889, 688)
        Me.XpGradientPanel1.StartColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.XpGradientPanel1.TabIndex = 22
        '
        'frmManageDPRD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 742)
        Me.Controls.Add(Me.XpGradientPanel1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.NavigationPane1)
        Me.Controls.Add(Me.Bar2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmManageDPRD"
        Me.Text = "Manage DPR\D System"
        Me.NavigationPane1.ResumeLayout(False)
        Me.pnlAttachment.ResumeLayout(False)
        Me.pnlAttachment.PerformLayout()
        Me.pnlViewData.ResumeLayout(False)
        Me.pnlViewData.PerformLayout()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnlViewData As DevComponents.DotNetBar.NavigationPanePanel
    Friend WithEvents pnlAttachment As DevComponents.DotNetBar.NavigationPanePanel
    Private WithEvents NavigationPane1 As DevComponents.DotNetBar.NavigationPane
    Private WithEvents btnAttachment As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnViewData As DevComponents.DotNetBar.ButtonItem
    Private WithEvents rdbReaching As System.Windows.Forms.RadioButton
    Private WithEvents rdbPO As System.Windows.Forms.RadioButton
    Private WithEvents rdbDPRD As System.Windows.Forms.RadioButton
    Private WithEvents rdbKios As System.Windows.Forms.RadioButton
    Private WithEvents rdbViewReachingTargetKios As System.Windows.Forms.RadioButton
    Private WithEvents rdbnViewKios As System.Windows.Forms.RadioButton
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRenameColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPageSettings As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor
    Private WithEvents XpGradientPanel1 As SteepValley.Windows.Forms.XPGradientPanel
    Private WithEvents rdbAchievementDPRDM As System.Windows.Forms.RadioButton
    Private WithEvents rdbViewAchievementDPRDM As System.Windows.Forms.RadioButton
    Private WithEvents rdbViewPOKios As System.Windows.Forms.RadioButton
End Class
