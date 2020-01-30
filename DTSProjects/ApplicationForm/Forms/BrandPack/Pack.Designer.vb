<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pack
    Inherits DTSProjects.BaseForm

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
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pack))
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.grpViewMode = New System.Windows.Forms.GroupBox
        Me.btnCLose = New Janus.Windows.EditControls.UIButton
        Me.btnSaveChanges = New Janus.Windows.EditControls.UIButton
        Me.Bar1 = New DevComponents.DotNetBar.Bar
        Me.btnModifiedAdded = New DevComponents.DotNetBar.ButtonItem
        Me.btnModifiedOriginal = New DevComponents.DotNetBar.ButtonItem
        Me.btnDelete = New DevComponents.DotNetBar.ButtonItem
        Me.btnCurrent = New DevComponents.DotNetBar.ButtonItem
        Me.btnUnchaigned = New DevComponents.DotNetBar.ButtonItem
        Me.btnOriginalRows = New DevComponents.DotNetBar.ButtonItem
        Me.btnAllData = New DevComponents.DotNetBar.ButtonItem
        Me.Bar2 = New DevComponents.DotNetBar.Bar
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnCustomFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilterEqual = New DevComponents.DotNetBar.ButtonItem
        Me.btnColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnRenameColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnHideColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnAddNew = New DevComponents.DotNetBar.ButtonItem
        Me.btnExport = New DevComponents.DotNetBar.ButtonItem
        Me.btnPrint = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.Bar3 = New DevComponents.DotNetBar.Bar
        Me.FilterEditor1 = New Janus.Windows.FilterEditor.FilterEditor
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpViewMode.SuspendLayout()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEX1
        '
        Me.GridEX1.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.Both
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.GridEX1.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.ExpandedGroup
        Me.GridEX1.ImageList = Me.ImageList2
        Me.GridEX1.Location = New System.Drawing.Point(0, 70)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndStayInCurrentCell
        Me.GridEX1.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Blue
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(674, 464)
        Me.GridEX1.TabIndex = 3
        Me.GridEX1.UpdateOnLeave = False
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        Me.GridEX1.WatermarkImage.Image = CType(resources.GetObject("GridEX1.WatermarkImage.Image"), System.Drawing.Image)
        Me.GridEX1.WatermarkImage.WashMode = Janus.Windows.GridEX.WashMode.UseWashColor
        '
        'ImageList2
        '
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList2.Images.SetKeyName(0, "Cancel.ico")
        Me.ImageList2.Images.SetKeyName(1, "Add.bmp")
        Me.ImageList2.Images.SetKeyName(2, "Grid.bmp")
        Me.ImageList2.Images.SetKeyName(3, "Filter 48 h p.ico")
        Me.ImageList2.Images.SetKeyName(4, "Customize.png")
        Me.ImageList2.Images.SetKeyName(5, "EDIT.ICO")
        Me.ImageList2.Images.SetKeyName(6, "Save.png")
        Me.ImageList2.Images.SetKeyName(7, "Delete.ico")
        Me.ImageList2.Images.SetKeyName(8, "TextEdit.png")
        Me.ImageList2.Images.SetKeyName(9, "CK.ico.ico")
        Me.ImageList2.Images.SetKeyName(10, "EDIT.ICO")
        Me.ImageList2.Images.SetKeyName(11, "gridColumn.png")
        Me.ImageList2.Images.SetKeyName(12, "SaveAllHS.png")
        Me.ImageList2.Images.SetKeyName(13, "printer.ico")
        Me.ImageList2.Images.SetKeyName(14, "Export.bmp")
        Me.ImageList2.Images.SetKeyName(15, "DB_Refresh.ico")
        '
        'grpViewMode
        '
        Me.grpViewMode.Controls.Add(Me.btnCLose)
        Me.grpViewMode.Controls.Add(Me.btnSaveChanges)
        Me.grpViewMode.Controls.Add(Me.Bar1)
        Me.grpViewMode.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grpViewMode.Location = New System.Drawing.Point(0, 534)
        Me.grpViewMode.Name = "grpViewMode"
        Me.grpViewMode.Size = New System.Drawing.Size(674, 44)
        Me.grpViewMode.TabIndex = 11
        Me.grpViewMode.TabStop = False
        Me.grpViewMode.Text = " Data View Mode"
        '
        'btnCLose
        '
        Me.btnCLose.ImageIndex = 0
        Me.btnCLose.ImageList = Me.ImageList2
        Me.btnCLose.Location = New System.Drawing.Point(558, 13)
        Me.btnCLose.Name = "btnCLose"
        Me.btnCLose.Size = New System.Drawing.Size(112, 27)
        Me.btnCLose.TabIndex = 3
        Me.btnCLose.Text = "Cance&l And Close"
        Me.btnCLose.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'btnSaveChanges
        '
        Me.btnSaveChanges.ImageIndex = 12
        Me.btnSaveChanges.ImageList = Me.ImageList2
        Me.btnSaveChanges.Location = New System.Drawing.Point(451, 13)
        Me.btnSaveChanges.Name = "btnSaveChanges"
        Me.btnSaveChanges.Size = New System.Drawing.Size(101, 27)
        Me.btnSaveChanges.TabIndex = 2
        Me.btnSaveChanges.Text = "&Save Changes"
        Me.btnSaveChanges.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
        '
        'Bar1
        '
        Me.Bar1.AccessibleDescription = "Bar1 (Bar1)"
        Me.Bar1.AccessibleName = "Bar1"
        Me.Bar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar
        Me.Bar1.BarType = DevComponents.DotNetBar.eBarType.MenuBar
        Me.Bar1.DockSide = DevComponents.DotNetBar.eDockSide.Bottom
        Me.Bar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnModifiedAdded, Me.btnModifiedOriginal, Me.btnDelete, Me.btnCurrent, Me.btnUnchaigned, Me.btnOriginalRows, Me.btnAllData})
        Me.Bar1.Location = New System.Drawing.Point(7, 15)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(440, 25)
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar1.TabIndex = 0
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'btnModifiedAdded
        '
        Me.btnModifiedAdded.Name = "btnModifiedAdded"
        Me.btnModifiedAdded.Text = "Added"
        Me.btnModifiedAdded.Tooltip = "this button will show any datarow who's been added / modified ,before being saved" & _
            " to server"
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
        Me.btnOriginalRows.Name = "btnOriginalRows"
        Me.btnOriginalRows.Text = "UnUsed"
        Me.btnOriginalRows.Tooltip = "this button will show all original data currently in server,but have no child ref" & _
            "erenced data"
        Me.btnOriginalRows.Visible = False
        '
        'btnAllData
        '
        Me.btnAllData.Checked = True
        Me.btnAllData.Name = "btnAllData"
        Me.btnAllData.Text = "All Data"
        Me.btnAllData.Tooltip = "this button will show all currrently data in server"
        '
        'Bar2
        '
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Bar2.Images = Me.ImageList2
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnSettingGrid, Me.btnFilter, Me.btnColumn, Me.btnAddNew, Me.btnExport, Me.btnPrint, Me.btnRefresh})
        Me.Bar2.Location = New System.Drawing.Point(0, 0)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(674, 25)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 12
        Me.Bar2.TabStop = False
        Me.Bar2.Text = "Bar2"
        '
        'btnSettingGrid
        '
        Me.btnSettingGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSettingGrid.ImageIndex = 2
        Me.btnSettingGrid.Name = "btnSettingGrid"
        Me.btnSettingGrid.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlShiftG)
        Me.btnSettingGrid.Text = "Setting Grid"
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
        Me.btnCustomFilter.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlShiftF)
        Me.btnCustomFilter.Text = "Custom Filter"
        '
        'btnFilterEqual
        '
        Me.btnFilterEqual.Name = "btnFilterEqual"
        Me.btnFilterEqual.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlShiftD)
        Me.btnFilterEqual.Text = "Default"
        '
        'btnColumn
        '
        Me.btnColumn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnColumn.ImageIndex = 11
        Me.btnColumn.Name = "btnColumn"
        Me.btnColumn.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnRenameColumn, Me.btnHideColumn, Me.btnShowColumn})
        Me.btnColumn.Text = "Grid Column"
        '
        'btnRenameColumn
        '
        Me.btnRenameColumn.Name = "btnRenameColumn"
        Me.btnRenameColumn.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlShiftR)
        Me.btnRenameColumn.Text = "Rename Column"
        '
        'btnHideColumn
        '
        Me.btnHideColumn.Name = "btnHideColumn"
        Me.btnHideColumn.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlShiftH)
        Me.btnHideColumn.Text = "Hide Column"
        '
        'btnShowColumn
        '
        Me.btnShowColumn.Name = "btnShowColumn"
        Me.btnShowColumn.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlShiftS)
        Me.btnShowColumn.Text = "Show Column"
        '
        'btnAddNew
        '
        Me.btnAddNew.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnAddNew.ImageIndex = 1
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Text = "Add New"
        '
        'btnExport
        '
        Me.btnExport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnExport.ImageIndex = 14
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlE)
        Me.btnExport.Text = "Export Data"
        '
        'btnPrint
        '
        Me.btnPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrint.ImageIndex = 13
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP)
        Me.btnPrint.Text = "Print Data Grid"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 15
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5)
        Me.btnRefresh.Text = "Refresh"
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.GridEX = Me.GridEX1
        Me.GridEXPrintDocument1.PageFooterFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'Bar3
        '
        Me.Bar3.Location = New System.Drawing.Point(355, 300)
        Me.Bar3.Name = "Bar3"
        Me.Bar3.Size = New System.Drawing.Size(17, 25)
        Me.Bar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003
        Me.Bar3.TabIndex = 19
        Me.Bar3.TabStop = False
        Me.Bar3.Text = "Bar3"
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
        Me.FilterEditor1.MinSize = New System.Drawing.Size(16, 45)
        Me.FilterEditor1.Name = "FilterEditor1"
        Me.FilterEditor1.Office2007ColorScheme = Janus.Windows.Common.Office2007ColorScheme.[Default]
        Me.FilterEditor1.ScrollMode = Janus.Windows.UI.Dock.ScrollMode.Both
        Me.FilterEditor1.ScrollStep = 15
        Me.FilterEditor1.Size = New System.Drawing.Size(674, 45)
        Me.FilterEditor1.SortFieldList = False
        Me.FilterEditor1.SourceControl = Me.GridEX1
        '
        'Pack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(674, 578)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.FilterEditor1)
        Me.Controls.Add(Me.Bar2)
        Me.Controls.Add(Me.grpViewMode)
        Me.Controls.Add(Me.Bar3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Pack"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pack"
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpViewMode.ResumeLayout(False)
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ImageList2 As System.Windows.Forms.ImageList
    Private WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Private WithEvents btnAddNew As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnPrint As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnExport As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCustomFilter As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRenameColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnHideColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnShowColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Private WithEvents grpViewMode As System.Windows.Forms.GroupBox
    Private WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Private WithEvents btnModifiedAdded As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnModifiedOriginal As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnDelete As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCurrent As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnUnchaigned As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnOriginalRows As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnCLose As Janus.Windows.EditControls.UIButton
    Private WithEvents btnSaveChanges As Janus.Windows.EditControls.UIButton
    Private WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Private WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Private WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Private WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Private WithEvents btnFilterEqual As DevComponents.DotNetBar.ButtonItem
    Private WithEvents Bar3 As DevComponents.DotNetBar.Bar
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnAllData As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents FilterEditor1 As Janus.Windows.FilterEditor.FilterEditor

End Class
