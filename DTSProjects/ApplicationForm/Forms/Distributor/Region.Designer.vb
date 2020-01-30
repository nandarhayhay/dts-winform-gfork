<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Region
    Inherits DTSProjects.BaseBigForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If MyBase.MayDisposed = False Then
            While MyBase.MayDisposed = False
                If MyBase.MayDisposed = True Then
                    Exit While
                End If
            End While
        End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Region))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.btnRegional = New DevComponents.DotNetBar.ButtonItem
        Me.btnTerritory = New DevComponents.DotNetBar.ButtonItem
        Me.btnTerritoryManager = New DevComponents.DotNetBar.ButtonItem
        Me.btnFS = New DevComponents.DotNetBar.ButtonItem
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.grpViewMode = New System.Windows.Forms.GroupBox
        Me.btnCLose = New Janus.Windows.EditControls.UIButton
        Me.btnSave = New Janus.Windows.EditControls.UIButton
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnModifiedAdded = New DevComponents.DotNetBar.ButtonItem
        Me.btnModifiedOriginal = New DevComponents.DotNetBar.ButtonItem
        Me.btnDelete = New DevComponents.DotNetBar.ButtonItem
        Me.btnCurrent = New DevComponents.DotNetBar.ButtonItem
        Me.btnUnchaigned = New DevComponents.DotNetBar.ButtonItem
        Me.btnOriginalRows = New DevComponents.DotNetBar.ButtonItem
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnRemoveFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnRename = New DevComponents.DotNetBar.ButtonItem
        Me.btnFieldChooser = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnView = New DevComponents.DotNetBar.ButtonItem
        Me.btnCardView = New DevComponents.DotNetBar.ButtonItem
        Me.btnSingleCard = New DevComponents.DotNetBar.ButtonItem
        Me.btnTableView = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.btnAddNew = New DevComponents.DotNetBar.ButtonItem
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpViewMode.SuspendLayout()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Bar1
        '
        Me.Bar1.ColorScheme.BarBackground = System.Drawing.SystemColors.InactiveCaption
        Me.Bar1.ColorScheme.BarBackground2 = System.Drawing.SystemColors.InactiveCaptionText
        Me.Bar1.ColorScheme.DockSiteBackColorGradientAngle = 0
        Me.Bar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar1.FadeEffect = True
        Me.Bar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnRegional, Me.btnTerritory, Me.btnTerritoryManager, Me.btnFS})
        Me.Bar1.Location = New System.Drawing.Point(0, 0)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(795, 25)
        Me.Bar1.Stretch = True
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar1.TabIndex = 3
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'btnRegional
        '
        Me.btnRegional.Name = "btnRegional"
        Me.btnRegional.Text = "Regional Manager"
        '
        'btnTerritory
        '
        Me.btnTerritory.Name = "btnTerritory"
        Me.btnTerritory.Text = "Territory_Area"
        '
        'btnTerritoryManager
        '
        Me.btnTerritoryManager.Name = "btnTerritoryManager"
        Me.btnTerritoryManager.Text = "Territory Manager"
        '
        'btnFS
        '
        Me.btnFS.Name = "btnFS"
        Me.btnFS.Text = "Field Supervisor"
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
        Me.ImageList1.Images.SetKeyName(5, "Save.png")
        Me.ImageList1.Images.SetKeyName(6, "Delete.ico")
        Me.ImageList1.Images.SetKeyName(7, "Export.bmp")
        Me.ImageList1.Images.SetKeyName(8, "printer.ico")
        Me.ImageList1.Images.SetKeyName(9, "View.jpg")
        Me.ImageList1.Images.SetKeyName(10, "SaveAllHS.png")
        Me.ImageList1.Images.SetKeyName(11, "Cancel and Close.ico")
        Me.ImageList1.Images.SetKeyName(12, "DB_Refresh.ico")
        '
        'GridEX1
        '
        Me.GridEX1.AllowRemoveColumns = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        Me.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        GridEX1_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive
        Me.GridEX1.ImageList = Me.ImageList1
        Me.GridEX1.Location = New System.Drawing.Point(0, 25)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.RowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.GridEX1.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Size = New System.Drawing.Size(718, 426)
        Me.GridEX1.TabIndex = 0
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'grpViewMode
        '
        Me.grpViewMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.grpViewMode.Controls.Add(Me.btnCLose)
        Me.grpViewMode.Controls.Add(Me.btnSave)
        Me.grpViewMode.Controls.Add(Me.Bar2)
        Me.grpViewMode.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpViewMode.Location = New System.Drawing.Point(0, 451)
        Me.grpViewMode.Name = "grpViewMode"
        Me.grpViewMode.Size = New System.Drawing.Size(795, 44)
        Me.grpViewMode.TabIndex = 5
        Me.grpViewMode.TabStop = False
        Me.grpViewMode.Text = " Data View Mode"
        '
        'btnCLose
        '
        Me.btnCLose.ImageIndex = 11
        Me.btnCLose.ImageList = Me.ImageList1
        Me.btnCLose.Location = New System.Drawing.Point(648, 13)
        Me.btnCLose.Name = "btnCLose"
        Me.btnCLose.Size = New System.Drawing.Size(112, 27)
        Me.btnCLose.TabIndex = 3
        Me.btnCLose.Text = "Cancel And Close"
        Me.btnCLose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnSave
        '
        Me.btnSave.ImageIndex = 10
        Me.btnSave.ImageList = Me.ImageList1
        Me.btnSave.Location = New System.Drawing.Point(541, 13)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(101, 27)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save Changes"
        Me.btnSave.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "Bar1 (Bar1)"
        Me.Bar2.AccessibleName = "Bar1"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar2.BarType = DevComponents.DotNetBar.eBarType.MenuBar
        Me.Bar2.DockSide = DevComponents.DotNetBar.eDockSide.Bottom
        Me.Bar2.FadeEffect = True
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnModifiedAdded, Me.btnModifiedOriginal, Me.btnDelete, Me.btnCurrent, Me.btnUnchaigned, Me.btnOriginalRows})
        Me.Bar2.Location = New System.Drawing.Point(7, 15)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(524, 25)
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar2.TabIndex = 0
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnModifiedAdded
        '
        Me.btnModifiedAdded.Name = "btnModifiedAdded"
        Me.btnModifiedAdded.Text = "ModifiedAdded"
        Me.btnModifiedAdded.Tooltip = "this button will show any datarow who's been added / modified ,before being saved" & _
            " to server "
        '
        'btnModifiedOriginal
        '
        Me.btnModifiedOriginal.Name = "btnModifiedOriginal"
        Me.btnModifiedOriginal.Text = "ModifiedOriginal"
        Me.btnModifiedOriginal.Tooltip = "this button wil show any datarow  who's been modified in original rows before bei" & _
            "ng saved to server"
        '
        'btnDelete
        '
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Text = "Deleted"
        Me.btnDelete.Tooltip = "this button will show any deleted rows, before being saved to server"
        '
        'btnCurrent
        '
        Me.btnCurrent.Name = "btnCurrent"
        Me.btnCurrent.Text = "Current"
        Me.btnCurrent.Tooltip = "this button will show all currently datarows  (who's been modified,added,deleted " & _
            "and unchaigned) before being saved to server"
        '
        'btnUnchaigned
        '
        Me.btnUnchaigned.Name = "btnUnchaigned"
        Me.btnUnchaigned.Text = "Unchaigned"
        Me.btnUnchaigned.Tooltip = "this button will show only datarow(s) who's unchaigned"
        '
        'btnOriginalRows
        '
        Me.btnOriginalRows.Checked = True
        Me.btnOriginalRows.Name = "btnOriginalRows"
        Me.btnOriginalRows.Text = "OriginalRows"
        Me.btnOriginalRows.Tooltip = "this button will show all currrently data in server"
        '
        'ItemPanel1
        '
        Me.ItemPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        '
        '
        '
        Me.ItemPanel1.BackgroundStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(158, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ItemPanel1.BackgroundStyle.BackColor2 = System.Drawing.SystemColors.MenuBar
        Me.ItemPanel1.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderBottomWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.ItemPanel1.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderLeftWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderRightWidth = 1
        Me.ItemPanel1.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.ItemPanel1.BackgroundStyle.BorderTopWidth = 1
        Me.ItemPanel1.BackgroundStyle.PaddingBottom = 1
        Me.ItemPanel1.BackgroundStyle.PaddingLeft = 1
        Me.ItemPanel1.BackgroundStyle.PaddingRight = 1
        Me.ItemPanel1.BackgroundStyle.PaddingTop = 1
        Me.ItemPanel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.ItemPanel1.Images = Me.ImageList1
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnView, Me.btnRefresh, Me.btnAddNew})
        Me.ItemPanel1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical
        Me.ItemPanel1.Location = New System.Drawing.Point(718, 25)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(77, 426)
        Me.ItemPanel1.TabIndex = 6
        Me.ItemPanel1.Text = "ItemPanel1"
        '
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 2
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnSettingGrid, Me.btnRemoveFilter, Me.btnRename, Me.btnFieldChooser, Me.btnExport, Me.btnPrint})
        Me.btnGrid.Text = "Grid"
        '
        'btnSettingGrid
        '
        Me.btnSettingGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSettingGrid.ImageIndex = 4
        Me.btnSettingGrid.Name = "btnSettingGrid"
        Me.btnSettingGrid.Text = "SettingGrid"
        '
        'btnRemoveFilter
        '
        Me.btnRemoveFilter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRemoveFilter.ImageIndex = 6
        Me.btnRemoveFilter.Name = "btnRemoveFilter"
        Me.btnRemoveFilter.Text = "Remove Filter"
        '
        'btnRename
        '
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Text = "Rename Column"
        '
        'btnFieldChooser
        '
        Me.btnFieldChooser.Name = "btnFieldChooser"
        Me.btnFieldChooser.Text = "ShowFieldChooser"
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 7
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Text = "Export DataGrid"
        '
        'btnPrint
        '
        Me.btnPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrint.ImageIndex = 8
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Text = "Print DataGrid"
        '
        'btnView
        '
        Me.btnView.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnView.ImageIndex = 9
        Me.btnView.Name = "btnView"
        Me.btnView.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCardView, Me.btnSingleCard, Me.btnTableView})
        Me.btnView.Text = "View"
        '
        'btnCardView
        '
        Me.btnCardView.Name = "btnCardView"
        Me.btnCardView.Text = "Card View"
        '
        'btnSingleCard
        '
        Me.btnSingleCard.Name = "btnSingleCard"
        Me.btnSingleCard.Text = "Single Card View"
        '
        'btnTableView
        '
        Me.btnTableView.Name = "btnTableView"
        Me.btnTableView.Text = "Table View"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 12
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Text = "Refresh"
        '
        'btnAddNew
        '
        Me.btnAddNew.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnAddNew.ImageIndex = 1
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Text = "Add New"
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
        Me.GridEXPrintDocument1.GridEX = Me.GridEX1
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PrintHierarchical = True
        '
        'GridEXExporter1
        '
        Me.GridEXExporter1.GridEX = Me.GridEX1
        Me.GridEXExporter1.IncludeFormatStyle = False
        '
        'Region
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(795, 495)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.ItemPanel1)
        Me.Controls.Add(Me.grpViewMode)
        Me.Controls.Add(Me.Bar1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Region"
        Me.Text = "RSM/TM"
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpViewMode.ResumeLayout(False)
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Private WithEvents btnRegional As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnTerritoryManager As DevComponents.DotNetBar.ButtonItem
    Private WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents grpViewMode As System.Windows.Forms.GroupBox
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnModifiedAdded As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnModifiedOriginal As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnDelete As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCurrent As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnUnchaigned As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnOriginalRows As DevComponents.DotNetBar.ButtonItem
    Private WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Private WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRename As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnView As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCardView As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSingleCard As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnTableView As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRemoveFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCLose As Janus.Windows.EditControls.UIButton
    Private WithEvents btnSave As Janus.Windows.EditControls.UIButton
    Private WithEvents btnFieldChooser As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnTerritory As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFS As DevComponents.DotNetBar.ButtonItem

End Class
