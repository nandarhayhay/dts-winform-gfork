<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemPanelX
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemPanelX))
        Me.ItemPanel1 = New DevComponents.DotNetBar.ItemPanel
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnSettingGrid = New DevComponents.DotNetBar.ButtonItem
        Me.btnFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnRemoveFilter = New DevComponents.DotNetBar.ButtonItem
        Me.btnRename = New DevComponents.DotNetBar.ButtonItem
        Me.btnHideColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnShowColumn = New DevComponents.DotNetBar.ButtonItem
        Me.btnAddItem = New DevComponents.DotNetBar.ButtonItem
        Me.btnSave = New DevComponents.DotNetBar.ButtonItem
        Me.btnCancel = New DevComponents.DotNetBar.ButtonItem
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonItem
        Me.SuspendLayout()
        '
        'ItemPanel1
        '
        '
        '
        '
        Me.ItemPanel1.BackgroundStyle.BackColor = System.Drawing.Color.White
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
        Me.ItemPanel1.Images = Me.ImageList1
        Me.ItemPanel1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGrid, Me.btnAddItem, Me.btnSave, Me.btnCancel, Me.btnRefresh})
        Me.ItemPanel1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical
        Me.ItemPanel1.Location = New System.Drawing.Point(0, 43)
        Me.ItemPanel1.Name = "ItemPanel1"
        Me.ItemPanel1.Size = New System.Drawing.Size(77, 309)
        Me.ItemPanel1.TabIndex = 2
        Me.ItemPanel1.Text = "ItemPanel1"
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
        Me.ImageList1.Images.SetKeyName(8, "DB_Refresh.ico")
        '
        'btnGrid
        '
        Me.btnGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnGrid.ImageIndex = 2
        Me.btnGrid.Name = "btnGrid"
        Me.btnGrid.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnSettingGrid, Me.btnFilter, Me.btnRemoveFilter, Me.btnRename, Me.btnHideColumn, Me.btnShowColumn})
        Me.btnGrid.Text = "Grid"
        '
        'btnSettingGrid
        '
        Me.btnSettingGrid.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSettingGrid.ImageIndex = 4
        Me.btnSettingGrid.Name = "btnSettingGrid"
        Me.btnSettingGrid.Text = "SettingGrid"
        '
        'btnFilter
        '
        Me.btnFilter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnFilter.ImageIndex = 3
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Text = "Filter"
        '
        'btnRemoveFilter
        '
        Me.btnRemoveFilter.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRemoveFilter.ImageIndex = 7
        Me.btnRemoveFilter.Name = "btnRemoveFilter"
        Me.btnRemoveFilter.Text = "Remove Filter"
        '
        'btnRename
        '
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Text = "Rename Column"
        '
        'btnHideColumn
        '
        Me.btnHideColumn.Name = "btnHideColumn"
        Me.btnHideColumn.Text = "Hide Column"
        '
        'btnShowColumn
        '
        Me.btnShowColumn.Name = "btnShowColumn"
        Me.btnShowColumn.Text = "Show Column"
        '
        'btnAddItem
        '
        Me.btnAddItem.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnAddItem.ImageIndex = 1
        Me.btnAddItem.Name = "btnAddItem"
        Me.btnAddItem.Text = "Add Item"
        '
        'btnSave
        '
        Me.btnSave.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSave.ImageIndex = 6
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Me.btnCancel.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnCancel.ImageIndex = 0
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Text = "Cancel"
        '
        'btnRefresh
        '
        Me.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRefresh.ImageIndex = 8
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Text = "Refresh"
        '
        'ItemPanelX
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ItemPanel1)
        Me.Name = "ItemPanelX"
        Me.Size = New System.Drawing.Size(77, 352)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ItemPanel1 As DevComponents.DotNetBar.ItemPanel
    Friend WithEvents btnGrid As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnSettingGrid As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnFilter As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnAddItem As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnSave As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCancel As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnRemoveFilter As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnRename As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnHideColumn As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnShowColumn As DevComponents.DotNetBar.ButtonItem
    Private WithEvents btnRefresh As DevComponents.DotNetBar.ButtonItem

End Class
