Public Class Region

#Region "Deklarasi"
    Private clsRegion As NufarmBussinesRules.DistributorRegistering.RegionalTerritory
    Private SelectBar As BarSelect
    Private SFG As StateFillingGrid
    Friend CMain As Main = Nothing
#End Region

    Private Enum BarSelect
        Regional
        Territory
        Territory_Manager
        FieldSupervisor
    End Enum

    Private Enum StateFillingGrid
        Filling
        HasFilled
    End Enum

#Region "Sub"

    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Region = True Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Region = True And NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Region = True Then
                Me.btnAddNew.Visible = True
                Me.grpViewMode.Text = "Data View Mode"
                Me.Bar1.Visible = True
                Me.btnSave.Enabled = True
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Region = True Then
                Me.btnAddNew.Visible = False
                Me.grpViewMode.Text = "Data View Mode"
                Me.Bar1.Visible = True
                Me.btnSave.Enabled = True
            Else
                Me.btnAddNew.Visible = False
                Me.grpViewMode.Text = ""
                Me.btnSave.Enabled = False
                Me.Bar2.Visible = False
            End If
        End If

    End Sub

    Friend Sub InitializeData()
        Me.LoadData()
    End Sub

    Private Sub LoadData()
        Me.clsRegion = New NufarmBussinesRules.DistributorRegistering.RegionalTerritory()
        Me.SFG = StateFillingGrid.Filling
        Me.clsRegion.GetDataView()
    End Sub

    Private Sub GetStateCheckedBar1(ByVal item As DevComponents.DotNetBar.ButtonItem)
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar1.Items
            If Not item1.Name = item.Name Then
                item1.Checked = False
            Else
                item = item1
                item.Checked = True
            End If
        Next
    End Sub

    Private Sub GetStateCheckedBar2(ByVal item As DevComponents.DotNetBar.ButtonItem)
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar2.Items
            If Not item1.Name = item.Name Then
                item1.Checked = False
            Else
                item = item1
                item.Checked = True
            End If
        Next
    End Sub

    Private Sub Bindgrid(ByVal dtview As DataView)
        Me.SFG = StateFillingGrid.Filling
        'Me.GridEX1.SetDataBinding(dtview, "")
        Me.GridEX1.DataSource = dtview
        Me.GridEX1.RetrieveStructure()
        For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If Item.Type Is Type.GetType("System.DateTime") Then
                Item.FormatMode = Janus.Windows.GridEX.FormatMode.UseIFormattable
                Item.FormatString = "dd MMMM yyyy"
            End If
            If Item.DataMember = "CREATE_BY" Then
                Item.EditType = Janus.Windows.GridEX.EditType.NoEdit
                Item.Visible = False
            ElseIf Item.DataMember = "MODIFY_BY" Then
                Item.EditType = Janus.Windows.GridEX.EditType.NoEdit
                Item.Visible = False
            ElseIf Item.DataMember = "CREATE_DATE" Then
                Item.EditType = Janus.Windows.GridEX.EditType.NoEdit
                Item.Visible = False
            ElseIf Item.DataMember = "MODIFY_DATE" Then
                Item.EditType = Janus.Windows.GridEX.EditType.NoEdit
                Item.Visible = False
            End If
        Next
        If dtview.Table.TableName = "Territory" Then
            Me.FillCategoriesValueList()
            Me.GridEX1.RootTable.Columns("REGIONAL_ID").Caption = "REGIONAL_AREA"
            Me.GridEX1.RootTable.Columns("TERRITORY_AREA").MaxLength = 30
        ElseIf dtview.Table.TableName = "FIELD_SUPERVISOR" Then
            Me.fillCategoriesterritory()
            Me.GridEX1.RootTable.Columns("CUR_TERRITORY_ID").Caption = "TERRITORY_AREA"
        ElseIf dtview.Table.TableName = "Regional" Then
            fillCategoriesRegional()
        End If
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            col.AutoSize()
        Next
        'Me.FillFilterColumn()
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.Update()
        Me.SFG = StateFillingGrid.HasFilled

    End Sub
    Private Sub FillFilterColumn()
        For Each Item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If Item.Type Is Type.GetType("System.String") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.Int16") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.Int32") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.Int64") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.Boolean") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
            ElseIf Item.Type Is Type.GetType("System.String") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            ElseIf Item.Type Is Type.GetType("System.DateTime") Then
                Me.GridEX1.RootTable.Columns(Item.Index).FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            End If
        Next

    End Sub
    Private Sub FillCategoriesValueList()
        Me.clsRegion.ViewRegional.RowFilter = ""
        Dim DVActiveRegional As DataView = Me.clsRegion.ViewRegional().ToTable().Copy().DefaultView
        'DVActiveRegional.RowFilter = "INACTIVE = " & False
        'clear dropdowns
        Me.GridEX1.DropDowns.Clear()
        Me.GridEX1.DropDowns.Add("DRegional")
        With Me.GridEX1.DropDowns("DRegional")
            .Columns.Add(New Janus.Windows.GridEX.GridEXColumn("REGIONAL_ID"))
            .Columns.Add(New Janus.Windows.GridEX.GridEXColumn("REGIONAL_AREA"))
            .DisplayMember = "REGIONAL_AREA"
            .ValueMember = "REGIONAL_ID"
            .EditMode = Janus.Windows.GridEX.EditMode.EditOn
            .SetDataBinding(DVActiveRegional, "")
            '.RetrieveStructure()
            .Columns("REGIONAL_AREA").Width = 200
            .VisibleRows = 30
            .ReplaceValues = True
            .RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        End With
        Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.MultiColumnDropDown
        Me.GridEX1.RootTable.Columns("REGIONAL_ID").ColumnType = Janus.Windows.GridEX.ColumnType.Text
        Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditTarget = Janus.Windows.GridEX.EditTarget.Text
        Me.GridEX1.RootTable.Columns("REGIONAL_ID").CompareTarget = Janus.Windows.GridEX.ColumnCompareTarget.Text

        GridEX1.RootTable.Columns("REGIONAL_ID").DropDown = Me.GridEX1.DropDowns("DRegional")
        Me.GridEX1.DropDowns.Add("ParentTerritory")
        With Me.GridEX1.DropDowns("ParentTerritory")
            .Columns.Add(New Janus.Windows.GridEX.GridEXColumn("TERRITORY_ID"))
            .Columns.Add(New Janus.Windows.GridEX.GridEXColumn("TERRITORY_AREA"))
            .DisplayMember = "TERRITORY_AREA"
            .ValueMember = "TERRITORY_ID"
            .ReplaceValues = True
            .EditMode = Janus.Windows.GridEX.EditMode.EditOn
            .SetDataBinding(Me.clsRegion.ViewTerritory, "")
            '.RetrieveStructure()
            .Columns("TERRITORY_AREA").Width = 200
            .VisibleRows = 30
            .RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        End With
        Me.GridEX1.RootTable.Columns("PARENT_TERRITORY").EditType = Janus.Windows.GridEX.EditType.MultiColumnCombo
        Me.GridEX1.RootTable.Columns("PARENT_TERRITORY").ColumnType = Janus.Windows.GridEX.ColumnType.Text
        Me.GridEX1.RootTable.Columns("PARENT_TERRITORY").EditTarget = Janus.Windows.GridEX.EditTarget.Text
        Me.GridEX1.RootTable.Columns("PARENT_TERRITORY").CompareTarget = Janus.Windows.GridEX.ColumnCompareTarget.Text
        GridEX1.RootTable.Columns("PARENT_TERRITORY").DropDown = Me.GridEX1.DropDowns("ParentTerritory")

        Dim VList() As String = {"DR", "SP"}
        Dim colTerritoryFor As Janus.Windows.GridEX.GridEXColumn = Me.GridEX1.RootTable.Columns("TERRITORY_FOR")
        colTerritoryFor.EditType = Janus.Windows.GridEX.EditType.DropDownList
        colTerritoryFor.AutoComplete = True : colTerritoryFor.HasValueList = True
        Dim ValueListUnit As Janus.Windows.GridEX.GridEXValueListItemCollection = colTerritoryFor.ValueList
        ValueListUnit.PopulateValueList(VList, "TERRITORY_FOR")
        colTerritoryFor.EditTarget = Janus.Windows.GridEX.EditTarget.Value
        colTerritoryFor.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    End Sub
    Private Sub fillCategoriesterritory()
        Dim colTerritoryID As Janus.Windows.GridEX.GridEXColumn = Me.GridEX1.RootTable.Columns("CUR_TERRITORY_ID")
        colTerritoryID.EditType = Janus.Windows.GridEX.EditType.DropDownList
        colTerritoryID.HasValueList = True
        Dim ValueList As Janus.Windows.GridEX.GridEXValueListItemCollection = colTerritoryID.ValueList
        ValueList.PopulateValueList(Me.clsRegion.ViewTerritory(), "TERRITORY_ID", "TERRITORY_AREA")
        colTerritoryID.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        colTerritoryID.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    End Sub
    Private Sub fillCategoriesRegional()
        If Not Me.GridEX1.DropDowns.Contains("ParentRegional") Then
            Dim colGrpregional As New Janus.Windows.GridEX.GridEXDropDown()
            colGrpregional.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            colGrpregional.Key = "ParentRegional"
            Me.GridEX1.DropDowns.Add("ParentRegional")
            Me.GridEX1.DropDowns("ParentRegional").Columns.Add(New Janus.Windows.GridEX.GridEXColumn("REGIONAL_ID"))
            Me.GridEX1.DropDowns("ParentRegional").Columns.Add(New Janus.Windows.GridEX.GridEXColumn("REGIONAL_AREA"))
            Me.GridEX1.DropDowns("ParentRegional").Columns("REGIONAL_AREA").Width = 200
            Me.GridEX1.DropDowns("ParentRegional").VisibleRows = 30
            Me.GridEX1.DropDowns("ParentRegional").RowFormatStyle.BackColor = Color.FromArgb(158, 190, 245)
        End If
        Me.GridEX1.DropDowns("ParentRegional").SetDataBinding(Me.clsRegion.ViewRegional(), "")
        Me.GridEX1.DropDowns("ParentRegional").DisplayMember = "REGIONAL_AREA"
        Me.GridEX1.DropDowns("ParentRegional").ValueMember = "REGIONAL_ID"
        Me.GridEX1.RootTable.Columns("PARENT_REGIONAL").EditType = Janus.Windows.GridEX.EditType.MultiColumnDropDown
        Me.GridEX1.RootTable.Columns("PARENT_REGIONAL").ColumnType = Janus.Windows.GridEX.ColumnType.Text
        Me.GridEX1.RootTable.Columns("PARENT_REGIONAL").EditTarget = Janus.Windows.GridEX.EditTarget.Text
        Me.GridEX1.RootTable.Columns("PARENT_REGIONAL").CompareTarget = Janus.Windows.GridEX.ColumnCompareTarget.Text
        Me.GridEX1.RootTable.Columns("PARENT_REGIONAL").FilterEditType = Janus.Windows.GridEX.FilterEditType.SameAsEditType
        'Me.GridEX1.DropDowns("ParentRegional").ReplaceValues = True
        GridEX1.RootTable.Columns("PARENT_REGIONAL").DropDown = Me.GridEX1.DropDowns(0)
    End Sub
    Private Sub LockedColumCreateModify()
        For Each item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If (item.DataMember = "CREATE_BY") Or (item.DataMember = "CREATE_DATE") _
            Or (item.DataMember = "MODIFY_BY") Or (item.DataMember = "MODIFY_DATE") Then
                item.EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
        Next
    End Sub
#End Region

#Region "Event"
    Private Sub Region_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If Not IsNothing(Me.clsRegion) Then
                If Not IsNothing(Me.clsRegion.GetDataSet()) Then
                    If Me.clsRegion.GetDataSet().HasChanges() Then
                        If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                            Me.clsRegion.SaveChanges(Me.clsRegion.GetDataSet.GetChanges())
                            Me.ShowMessageInfo(Me.MessageSavingSucces)
                        End If
                    End If
                End If
            End If
            Me.clsRegion.Dispose(True)
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Brand_FormClosed")
        End Try
    End Sub

    Private Sub ItemPanel1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog()
                Case "btnRemoveFilter"
                    Me.GridEX1.RemoveFilters()
                Case "btnRename"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar1)
                Case "btnFieldChooser"
                    Me.GridEX1.ShowFieldChooser(Me)
                Case "btnExport"
                    Me.SaveFileDialog1.Title = "Define the location File"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.GridEX1
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnPrint"
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                Case "btnCardView"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.CardView
                    Me.GridEX1.RootTable.Columns(0).CardCaption = True
                Case "btnSingleCard"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.SingleCard
                Case "btnTableView"
                    Me.GridEX1.View = Janus.Windows.GridEX.View.TableView
                Case "btnAddNew"
                    Select Case Me.SelectBar
                        Case BarSelect.Regional
                            Select Case item.Text
                                Case "Add New"
                                    'Me.clsRegion.ViewRegional().RowStateFilter = Data.DataViewRowState.CurrentRows
                                    Me.Bindgrid(Me.clsRegion.ViewRegional())
                                    'Me.lblLastID.Text = Me.clsBrand.ViewBrand(Me.clsBrand.ViewBrand.Count - 1)("BRAND_ID").ToString()
                                    'Me.grpLastID.Visible = True
                                    Me.GetStateCheckedBar2(Me.btnCurrent)
                                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.GridEX1.MoveToNewRecord()
                                    item.Text = "Cancel Changes"
                                Case "Cancel Changes"
                                    If Me.clsRegion.GetDataSet().HasChanges() Then
                                        Me.clsRegion.GetDataSet().RejectChanges()
                                    End If
                                    'Me.clsRegion.ViewRegional().RowStateFilter = Data.DataViewRowState.OriginalRows
                                    Me.Bindgrid(Me.clsRegion.ViewRegional())
                                    item.Text = "Add New"
                                    Me.GetStateCheckedBar2(Me.btnOriginalRows)
                                    'Me.grpLastID.Visible = False
                                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    'Me.GridEX1.MoveFirst()
                            End Select

                        Case BarSelect.Territory
                            Select Case item.Text
                                Case "Add New"
                                    'Me.clsRegion.ViewTerritory().RowStateFilter = Data.DataViewRowState.CurrentRows
                                    Me.GetStateCheckedBar2(Me.btnCurrent)
                                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.RootTable.Columns("TERRITORY_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.GridEX1.MoveToNewRecord()
                                    item.Text = "Cancel Changes"
                                    Me.Bindgrid(Me.clsRegion.ViewTerritory())
                                    'Me.lblLastID.Text = Me.clsBrand.ViewBrand(Me.clsBrand.ViewBrand.Count - 1)("BRAND_ID").ToString()
                                    'Me.grpLastID.Visible = True

                                Case "Cancel Changes"
                                    If Me.clsRegion.GetDataSet().HasChanges() Then
                                        Me.clsRegion.GetDataSet().RejectChanges()
                                    End If
                                    'Me.clsRegion.ViewTerritory().RowStateFilter = Data.DataViewRowState.OriginalRows
                                    Me.Bindgrid(Me.clsRegion.ViewTerritory())
                                    item.Text = "Add New"
                                    Me.GetStateCheckedBar2(Me.btnOriginalRows)
                                    'Me.grpLastID.Visible = False
                                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic

                                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.RootTable.Columns("TERRITORY_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    'Me.GridEX1.MoveFirst()

                            End Select
                        Case BarSelect.Territory_Manager
                            Select Case item.Text
                                Case "Add New"
                                    'Me.clsRegion.ViewTerritoryManager().RowStateFilter = Data.DataViewRowState.CurrentRows
                                    Me.Bindgrid(Me.clsRegion.ViewTerritoryManager())
                                    'Me.lblLastID.Text = Me.clsBrand.ViewBrand(Me.clsBrand.ViewBrand.Count - 1)("BRAND_ID").ToString()
                                    'Me.grpLastID.Visible = True
                                    Me.GetStateCheckedBar2(Me.btnCurrent)
                                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.RootTable.Columns("TM_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.GridEX1.MoveToNewRecord()
                                    item.Text = "Cancel Changes"
                                Case "Cancel Changes"
                                    If Me.clsRegion.GetDataSet().HasChanges() Then
                                        Me.clsRegion.GetDataSet().RejectChanges()
                                    End If
                                    'Me.clsRegion.ViewTerritory().RowStateFilter = Data.DataViewRowState.OriginalRows
                                    Me.Bindgrid(Me.clsRegion.ViewTerritoryManager())
                                    item.Text = "Add New"
                                    Me.GetStateCheckedBar2(Me.btnOriginalRows)
                                    'Me.grpLastID.Visible = False
                                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.RootTable.Columns("TM_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    'Me.GridEX1.MoveFirst()

                            End Select
                        Case BarSelect.FieldSupervisor
                            Select Case item.Text
                                Case "Add New"
                                    'Me.clsRegion.ViewFS().RowStateFilter = Data.DataViewRowState.CurrentRows
                                    Me.Bindgrid(Me.clsRegion.ViewFS())
                                    'Me.lblLastID.Text = Me.clsBrand.ViewBrand(Me.clsBrand.ViewBrand.Count - 1)("BRAND_ID").ToString()
                                    ''Me.grpLastID.Visible = True
                                    Me.GetStateCheckedBar2(Me.btnCurrent)
                                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                                    'Me.GridEX1.RootTable.Columns("TERRITORY_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.GridEX1.MoveToNewRecord()
                                    item.Text = "Cancel Changes"
                                Case "Cancel Changes"
                                    If Me.clsRegion.GetDataSet().HasChanges() Then
                                        Me.clsRegion.GetDataSet().RejectChanges()
                                    End If
                                    'Me.clsRegion.ViewFS().RowStateFilter = Data.DataViewRowState.OriginalRows
                                    Me.Bindgrid(Me.clsRegion.ViewFS())
                                    item.Text = "Add New"
                                    Me.GetStateCheckedBar2(Me.btnOriginalRows)
                                    'Me.grpLastID.Visible = False
                                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.GridEX1.RootTable.Columns("FS_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    'Me.GridEX1.MoveFirst()

                            End Select
                    End Select
                Case "btnRefresh"
                    If Not IsNothing(Me.clsRegion) Then
                        If Not IsNothing(Me.clsRegion.GetDataSet()) Then
                            If Me.clsRegion.GetDataSet().HasChanges() Then
                                If Me.ShowConfirmedMessage("Data has changed !" & vbCrLf & "If you continue refreshing" & vbCrLf & "All Changes will be discarded" & _
                                     vbCrLf & "Continue refreshing anyway ?.") = Windows.Forms.DialogResult.No Then
                                    'Me.clsBrand.SaveData(Me.clsBrand.GetDataSet().GetChanges())
                                    'Me.ShowMessageInfo(Me.MessageSavingSucces)
                                    Return
                                Else
                                    Me.clsRegion.GetDataSet().RejectChanges()
                                End If
                            End If
                        End If
                        Me.LoadData()
                        Select Case Me.SelectBar
                            Case BarSelect.Regional
                                Me.Bindgrid(Me.clsRegion.ViewRegional())
                            Case BarSelect.Territory
                                Me.Bindgrid(Me.clsRegion.ViewTerritory())
                            Case BarSelect.Territory_Manager
                                Me.Bindgrid(Me.clsRegion.ViewTerritoryManager())
                            Case BarSelect.FieldSupervisor
                                Me.Bindgrid(Me.clsRegion.ViewFS())
                        End Select
                        Me.btnAddNew.Text = "Add New"
                        'Me.grpLastID.Visible = False
                        'Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    End If
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ItemPanel1_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
        End Try
    End Sub

    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        Try
            Select Case Me.SelectBar
                Case BarSelect.Regional
                    'CHECK REGIONAL_ID
                    If Me.GridEX1.GetValue("REGIONAL_ID") Is Nothing Then
                        Me.ShowMessageInfo("REGIONAL_ID Must not be NULL")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    ElseIf (Me.GridEX1.GetValue("REGIONAL_ID") Is DBNull.Value) Or (Me.GridEX1.GetValue("REGIONAL_ID").Equals("")) Then
                        Me.ShowMessageInfo("REGIONAL_ID Must not be NULL")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    End If
                    'CHECK REGIONAL AREA
                    If Me.GridEX1.GetValue("REGIONAL_AREA") Is Nothing Then
                        Me.ShowMessageInfo("REGIONAL_AREA Must not be NULL")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    ElseIf (Me.GridEX1.GetValue("REGIONAL_AREA") Is DBNull.Value) Or (Me.GridEX1.GetValue("REGIONAL_AREA").Equals("")) Then
                        Me.ShowMessageInfo("REGIONAL_AREA Must not be NULL")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    End If
                    'CHECK MANAGER
                    If Me.GridEX1.GetValue("MANAGER") Is Nothing Then
                        Me.ShowMessageInfo("MANAGER Must not be NULL")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    ElseIf (Me.GridEX1.GetValue("MANAGER") Is DBNull.Value) Or (Me.GridEX1.GetValue("MANAGER").Equals("")) Then
                        Me.ShowMessageInfo("MANAGER Must not be NULL")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    End If
                    'CHEK IN DATAVIEW
                    If Me.clsRegion.ViewRegional().Find(Me.GridEX1.GetValue("REGIONAL_ID")) <> -1 Then
                        Me.ShowMessageInfo("REGIONAL_ID Has existed in DATAVIEW")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    End If
                Case BarSelect.Territory
                    'CHECK TERRITORY_ID
                    If Me.GridEX1.GetValue("TERRITORY_ID") Is Nothing Then
                        Me.ShowMessageInfo("TERRITORY_ID Must not be NULL")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    ElseIf (Me.GridEX1.GetValue("TERRITORY_ID") Is DBNull.Value) Or (Me.GridEX1.GetValue("TERRITORY_ID").Equals("")) Then
                        Me.ShowMessageInfo("TERRITORY_ID Must not be NULL")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    End If
                    'CHECK TERRITORY_AREA
                    If IsNothing(Me.GridEX1.RootTable.Columns("TERRITORY_AREA")) Then
                        Me.ShowMessageInfo("TERRITORY_AREA Must not be NULL!")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    ElseIf (Me.GridEX1.GetValue("TERRITORY_AREA") Is DBNull.Value) Or (Me.GridEX1.GetValue("TERRITORY_AREA").Equals("")) Then
                        Me.ShowMessageInfo("TERRITORY_AREA Must not be NULL!")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    End If
                    'CHECK REGIONAL AREA
                    If (IsNothing(Me.GridEX1.GetValue("REGIONAL_ID"))) Or (IsDBNull(Me.GridEX1.GetValue("REGIONAL_ID"))) Then
                        Me.ShowMessageInfo("REGIONAL_ID can not be null")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    End If
                    'CHECK MANAGER
                    'If Me.GridEX1.GetValue("MANAGER") Is Nothing Then
                    '    Me.ShowMessageInfo("MANAGER Must not be NULL")
                    '    e.Cancel = True
                    '    Me.GridEX1.CancelCurrentEdit()
                    '    Me.GridEX1.Refetch()
                    '    Return
                    'ElseIf (Me.GridEX1.GetValue("MANAGER") Is DBNull.Value) Or (Me.GridEX1.GetValue("MANAGER").Equals("")) Then
                    '    Me.ShowMessageInfo("MANAGER Must not be NULL")
                    '    e.Cancel = True
                    '    Me.GridEX1.CancelCurrentEdit()
                    '    Me.GridEX1.Refetch()
                    '    Return
                    'End If
                    'CHEK IN DATAVIEW
                    If Me.clsRegion.ViewTerritory().Find(Me.GridEX1.GetValue("TERRITORY_ID")) <> -1 Then
                        Me.ShowMessageInfo("TERRITORY_ID Has existed in DATAVIEW")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    End If
                Case BarSelect.Territory_Manager
                    If (IsNothing(Me.GridEX1.GetValue("TM_ID"))) Or (IsDBNull(Me.GridEX1.GetValue("TM_ID"))) Then
                        Me.ShowMessageInfo("TM_ID can not be null")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    ElseIf (IsNothing(Me.GridEX1.GetValue("MANAGER"))) Or (IsDBNull(Me.GridEX1.GetValue("MANAGER"))) Then
                        Me.ShowMessageInfo("MANAGER NAME can not be null")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                        'ElseIf (IsNothing(Me.GridEX1.GetValue("REGIONAL_ID"))) Or (IsDBNull(Me.GridEX1.GetValue("REGIONAL_ID"))) Then
                        '    Me.ShowMessageInfo("REGIONAL_ID can not be null")
                        '    e.Cancel = True
                        '    Me.GridEX1.CancelCurrentEdit()
                        '    Me.GridEX1.Refetch()
                        '    Me.GridEX1.MoveToNewRecord()
                        '    Return
                        'CHECK DATA
                    ElseIf Me.clsRegion.ViewTerritoryManager().Find(Me.GridEX1.GetValue("TM_ID")) <> -1 Then
                        Me.ShowMessageInfo("Data has existed in dataview")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    End If
                Case BarSelect.FieldSupervisor
                    If IsNothing(Me.GridEX1.GetValue("FS_ID")) Or IsDBNull(Me.GridEX1.GetValue("FS_ID")) Then
                        Me.ShowMessageInfo("FS_ID can not be null")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    ElseIf String.IsNullOrEmpty(Me.GridEX1.GetValue("FS_ID").ToString().Trim()) Then
                        Me.ShowMessageInfo("FS_ID can not be null")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    ElseIf Me.GridEX1.GetValue("FS_ID").ToString().Length <= 4 Then
                        Me.ShowMessageInfo("invalid FS_ID ")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    ElseIf IsNothing(Me.GridEX1.GetValue("FS_NAME")) Or IsDBNull(Me.GridEX1.GetValue("FS_NAME")) Then
                        Me.ShowMessageInfo("FS_NAME can not be null")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    ElseIf String.IsNullOrEmpty(Me.GridEX1.GetValue("FS_NAME").ToString().Trim()) Then
                        Me.ShowMessageInfo("FS_NAME can not be null")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    ElseIf IsNothing(Me.GridEX1.GetValue("CUR_TERRITORY_ID")) Or IsDBNull(Me.GridEX1.GetValue("CUR_TERRITORY_ID")) Then
                        Me.ShowMessageInfo("TERRITORY can not be null")
                        e.Cancel = True
                        Me.GridEX1.CancelCurrentEdit()
                        Me.GridEX1.Refetch()
                        Me.GridEX1.MoveToNewRecord()
                        Return
                    End If

            End Select
            Me.GridEX1.SetValue("INACTIVE", False)
            Me.GridEX1.SetValue("CREATE_BY", NufarmBussinesRules.User.UserLogin.UserName)
            Me.GridEX1.SetValue("CREATE_DATE", NufarmBussinesRules.SharedClass.ServerDate())
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub GridEX1_CellEdited(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellEdited
    '    Try
    '        Select Case Me.SelectBar
    '            Case BarSelect.Regional
    '                If Me.GridEX1.Row <= -1 Then
    '                    If e.Column.Key = "REGIONAL_AREA" Then
    '                        Me.GridEX1.SearchColumn = Me.GridEX1.RootTable.Columns("REGIONAL_AREA")
    '                        If Me.GridEX1.Find(Me.GridEX1.RootTable.Columns("REGIONAL_AREA"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.GridEX1.GetValue("BRAND_NAME"), -1, 1) Then
    '                            Me.ShowMessageInfo("REGIONAL_AREA Has Existed !." & vbCrLf & "We Suggest REGIONAL_AREA not Equal" _
    '                             & vbCrLf & "You can delete the row by moving to modified Added DataView" & vbCrLf & "Press Del If you want to delete.")

    '                            'If Me.ShowConfirmedMessage("BRAND_NAME Has Existed !." & vbCrLf & "We Suggest BRAND_NAME not Equal" & vbCrLf & "Proceed to save into DataView ?.") = Windows.Forms.DialogResult.No Then
    '                            '    Me.GridEX1.MoveToRowIndex(Me.GridEX1.RecordCount - 1)
    '                            '    Me.GridEX1.MoveToNewRecord()
    '                            '    Return
    '                            'End If
    '                        End If
    '                    End If
    '                End If
    '            Case BarSelect.Territory
    '                If Me.GridEX1.Row <= -1 Then
    '                    If e.Column.Key = "TERRITORY_AREA" Then
    '                        Me.GridEX1.SearchColumn = Me.GridEX1.RootTable.Columns("TERRITORY_AREA")
    '                        If Me.GridEX1.Find(Me.GridEX1.RootTable.Columns("TERRITORY_AREA"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.GridEX1.GetValue("BRAND_NAME"), -1, 1) Then
    '                            Me.ShowMessageInfo("TERRITORY_AREA Has Existed !." & vbCrLf & "We Suggest TERRITORY_AREA not Equal" _
    '                             & vbCrLf & "You can delete the row by moving to modified Added DataView" & vbCrLf & "Press Del If you want to delete.")

    '                            'If Me.ShowConfirmedMessage("BRAND_NAME Has Existed !." & vbCrLf & "We Suggest BRAND_NAME not Equal" & vbCrLf & "Proceed to save into DataView ?.") = Windows.Forms.DialogResult.No Then
    '                            '    Me.GridEX1.MoveToRowIndex(Me.GridEX1.RecordCount - 1)
    '                            '    Me.GridEX1.MoveToNewRecord()
    '                            '    Return
    '                            'End If
    '                        End If
    '                    End If
    '                End If
    '        End Select
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        Try
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                'check apakah column nya colum territory id / bukan
                If e.Column.Key = "CUR_TERRITORY_ID" Then
                    Dim TerritoryID As String = Me.GridEX1.GetValue("CUR_TERRITORY_ID").ToString()
                    Me.GridEX1.SetValue("FS_ID", TerritoryID + "/")
                End If
            End If
            If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Select Case Me.SelectBar
                Case BarSelect.Regional
                    If Me.clsRegion.ViewRegional().RowStateFilter = Data.DataViewRowState.ModifiedOriginal Or Me.clsRegion.ViewRegional().RowStateFilter = Data.DataViewRowState.OriginalRows Then
                        If e.Column.Key = "REGIONAL_AREA" Then
                            If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
                                Me.GridEX1.CancelCurrentEdit()
                                Me.GridEX1.Refetch()
                                Return
                            End If
                        ElseIf e.Column.Key = "MANAGER" Then
                            If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
                                Me.GridEX1.CancelCurrentEdit()
                                Me.GridEX1.Refetch()
                                Return
                            End If
                        End If

                    End If
                Case BarSelect.Territory
                    If Me.clsRegion.ViewTerritory().RowStateFilter = Data.DataViewRowState.ModifiedOriginal Or Me.clsRegion.ViewTerritory().RowStateFilter = Data.DataViewRowState.OriginalRows Then
                        If e.Column.Key = "TERRITORY_AREA" Then
                            If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
                                Me.GridEX1.CancelCurrentEdit()
                                Me.GridEX1.Refetch()
                                Return
                            End If
                        ElseIf e.Column.Key = "REGIONAL_ID" Then
                            If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
                                Me.GridEX1.CancelCurrentEdit()
                                Me.GridEX1.Refetch()
                                Return
                            End If
                            'ElseIf e.Column.Key = "REGIONAL_ID" Then
                            '    If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
                            '        Me.GridEX1.CancelCurrentEdit()
                            '        Me.GridEX1.Refetch()
                            '        Return
                            '    End If
                            'ElseIf e.Column.Key = "MANAGER" Then
                            '    If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
                            '        Me.GridEX1.CancelCurrentEdit()
                            '        Me.GridEX1.Refetch()
                            '        Return
                            '    End If
                        End If
                    End If
                Case BarSelect.Territory_Manager
                    If Me.clsRegion.ViewTerritoryManager().RowStateFilter = Data.DataViewRowState.ModifiedOriginal Or Me.clsRegion.ViewTerritoryManager().RowStateFilter = Data.DataViewRowState.OriginalRows Then
                        If e.Column.Key = "MANAGER" Then
                            If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
                                Me.GridEX1.CancelCurrentEdit()
                                Me.GridEX1.Refetch()
                                Return
                            End If
                            'ElseIf e.Column.Key = "REGIONAL_ID" Then
                            '    If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
                            '        Me.GridEX1.CancelCurrentEdit()
                            '        Me.GridEX1.Refetch()
                            '        Return
                            '    End If
                            'ElseIf e.Column.Key = "MANAGER" Then
                            '    If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
                            '        Me.GridEX1.CancelCurrentEdit()
                            '        Me.GridEX1.Refetch()
                            '        Return
                            '    End If
                        End If
                    End If
                Case BarSelect.FieldSupervisor
                    If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                        If Not Me.clsRegion.ViewFS().RowStateFilter = Data.DataViewRowState.ModifiedOriginal Then
                            If e.Column.Key = "FS_ID" Or e.Column.Key = "CUR_TERRITORY_ID" Then
                                Me.ShowMessageInfo("ID can not be update") : Me.GridEX1.CancelCurrentEdit() : Return
                            End If
                        End If
                    End If
                    If Me.clsRegion.ViewFS().RowStateFilter = Data.DataViewRowState.ModifiedOriginal Or Me.clsRegion.ViewFS.RowStateFilter = Data.DataViewRowState.OriginalRows Then
                        If e.Column.Key = "FS_ID" Or e.Column.Key = "FS_NAME" Or e.Column.Key = "CUR_TERRITORY_ID" Then
                            If IsNothing(Me.GridEX1.GetValue(e.Column.Index)) Or IsDBNull(Me.GridEX1.GetValue(e.Column.Index)) Then
                                Me.GridEX1.CancelCurrentEdit()
                                Me.GridEX1.Refetch()
                                Return
                            End If
                        End If
                    End If
                    If e.Column.Key = "FS_ID" Or e.Column.Key = "FS_NAME" Then
                        If String.IsNullOrEmpty(Me.GridEX1.GetValue(e.Column.Index).ToString()) Then
                            Me.GridEX1.CancelCurrentEdit()
                            Me.GridEX1.Refetch()
                            Return
                        End If
                    End If
                    If e.Column.Key = "FS_ID" Then
                        If Me.GridEX1.GetValue("FS_ID").ToString().Length <= 4 Then
                            Me.ShowMessageInfo("invalid FS_ID")
                            Me.GridEX1.CancelCurrentEdit()
                            Me.GridEX1.Refetch()
                            Return
                        End If
                    End If
            End Select
            Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("MODIFY_BY"), NufarmBussinesRules.User.UserLogin.UserName)
            Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("MODIFY_DATE"), NufarmBussinesRules.SharedClass.ServerDate())
            Me.GridEX1.UpdateData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Not (e.Row.RowType = Janus.Windows.GridEX.RowType.Record) Then
                Return
            End If
            Me.SFG = StateFillingGrid.Filling
            Select Case Me.SelectBar
                Case BarSelect.Regional
                    If Me.clsRegion.ViewRegional().RowStateFilter = Data.DataViewRowState.OriginalRows _
                                Or Me.clsRegion.ViewRegional().RowStateFilter = Data.DataViewRowState.CurrentRows _
                                Or Me.clsRegion.ViewRegional().RowStateFilter = Data.DataViewRowState.Unchanged Then
                        'check keberadaan data anak di server
                        'jika ada return dan refect
                        'Me.MessageCantDeleteData
                        If Me.clsRegion.HasRererencedRegional(Me.GridEX1.GetValue("REGIONAL_ID").ToString()) = True Then
                            Me.ShowMessageInfo(Me.MessageCantDeleteData)
                            e.Cancel = True
                            Me.GridEX1.Refetch()
                            Me.GridEX1.SelectCurrentCellText()
                            Return
                        End If
                        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                            e.Cancel = True
                            Me.GridEX1.Refetch()
                            Me.GridEX1.SelectCurrentCellText()
                        Else
                            e.Cancel = False
                        End If
                    End If
                Case BarSelect.Territory
                    If Me.clsRegion.ViewTerritory().RowStateFilter = Data.DataViewRowState.OriginalRows _
                       Or Me.clsRegion.ViewTerritory().RowStateFilter = Data.DataViewRowState.CurrentRows _
                       Or Me.clsRegion.ViewTerritory().RowStateFilter = Data.DataViewRowState.Unchanged Then
                        'check keberadaan data anak di server
                        'jika ada return dan refect
                        'Me.MessageCantDeleteData
                        If Me.clsRegion.HasReferencedTerritory(Me.GridEX1.GetValue("TERRITORY_ID").ToString()) = True Then
                            Me.ShowMessageInfo(Me.MessageCantDeleteData)
                            e.Cancel = True
                            Me.GridEX1.Refetch()
                            Me.GridEX1.SelectCurrentCellText()
                            Return
                        End If
                        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                            e.Cancel = True
                            Me.GridEX1.Refetch()
                            Me.GridEX1.SelectCurrentCellText()
                        Else
                            e.Cancel = False
                        End If
                    End If
                Case BarSelect.Territory_Manager
                    If Me.clsRegion.ViewTerritoryManager().RowStateFilter = Data.DataViewRowState.OriginalRows _
                     Or Me.clsRegion.ViewTerritoryManager().RowStateFilter = Data.DataViewRowState.CurrentRows _
                     Or Me.clsRegion.ViewTerritoryManager().RowStateFilter = Data.DataViewRowState.Unchanged Then
                        'check keberadaan data anak di server
                        'jika ada return dan refect
                        'Me.MessageCantDeleteData
                        If Me.clsRegion.HasReferencedTerritoryManager(Me.GridEX1.GetValue("TM_ID").ToString()) = True Then
                            Me.ShowMessageInfo(Me.MessageCantDeleteData)
                            e.Cancel = True
                            Me.GridEX1.Refetch()
                            Me.GridEX1.SelectCurrentCellText()
                            Return
                        End If
                        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                            e.Cancel = True
                            Me.GridEX1.Refetch()
                            Me.GridEX1.SelectCurrentCellText()
                        Else
                            e.Cancel = False
                        End If
                    End If
                Case BarSelect.FieldSupervisor
                    If Me.clsRegion.ViewFS().RowStateFilter = Data.DataViewRowState.OriginalRows _
                    Or Me.clsRegion.ViewFS().RowStateFilter = Data.DataViewRowState.CurrentRows _
                    Or Me.clsRegion.ViewFS().RowStateFilter = Data.DataViewRowState.Unchanged Then
                        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                            e.Cancel = True
                            Me.GridEX1.Refetch()
                            Me.GridEX1.SelectCurrentCellText()
                        Else
                            e.Cancel = False
                        End If
                    End If
            End Select
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled
            e.Cancel = True
        End Try
    End Sub

    Private Sub Bar1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            If Me.btnAddNew.Text = "Cancel Changes" Then
                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            Select Case item.Name
                Case "btnRegional"
                    'Me.clsRegion.ViewRegional().RowStateFilter = Data.DataViewRowState.OriginalRows
                    If Me.clsRegion.ViewRegional().RowStateFilter = (Data.DataViewRowState.Added Or Data.DataViewRowState.ModifiedCurrent) Then
                        Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                    End If
                    Select Case Me.clsRegion.ViewRegional().RowStateFilter
                        Case Data.DataViewRowState.Added
                            Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                        Case Data.DataViewRowState.CurrentRows
                            Me.GetStateCheckedBar2(Me.btnCurrent)
                        Case Data.DataViewRowState.Deleted
                            Me.GetStateCheckedBar2(Me.btnDelete)
                        Case Data.DataViewRowState.ModifiedCurrent
                            Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                        Case Data.DataViewRowState.ModifiedOriginal
                            Me.GetStateCheckedBar2(Me.btnModifiedOriginal)
                        Case Data.DataViewRowState.OriginalRows
                            Me.GetStateCheckedBar2(Me.btnOriginalRows)
                        Case Data.DataViewRowState.Unchanged
                            Me.GetStateCheckedBar2(Me.btnUnchaigned)
                    End Select
                    Me.Bindgrid(Me.clsRegion.ViewRegional)
                    Me.SelectBar = BarSelect.Regional
                Case "btnTerritory"
                    If Me.clsRegion.ViewTerritory().RowStateFilter = (Data.DataViewRowState.Added Or Data.DataViewRowState.ModifiedCurrent) Then
                        Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                    End If
                    Select Case Me.clsRegion.ViewTerritory().RowStateFilter
                        Case Data.DataViewRowState.Added
                            Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                        Case Data.DataViewRowState.CurrentRows
                            Me.GetStateCheckedBar2(Me.btnCurrent)
                        Case Data.DataViewRowState.Deleted
                            Me.GetStateCheckedBar2(Me.btnDelete)
                        Case Data.DataViewRowState.ModifiedCurrent
                            Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                        Case Data.DataViewRowState.ModifiedOriginal
                            Me.GetStateCheckedBar2(Me.btnModifiedOriginal)
                        Case Data.DataViewRowState.OriginalRows
                            Me.GetStateCheckedBar2(Me.btnOriginalRows)
                        Case Data.DataViewRowState.Unchanged
                            Me.GetStateCheckedBar2(Me.btnUnchaigned)
                    End Select
                    Me.Bindgrid(Me.clsRegion.ViewTerritory)
                    Me.SelectBar = BarSelect.Territory
                Case "btnTerritoryManager"
                    If Me.clsRegion.ViewTerritoryManager().RowStateFilter = (Data.DataViewRowState.Added Or Data.DataViewRowState.ModifiedCurrent) Then
                        Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                    End If
                    Select Case Me.clsRegion.ViewTerritoryManager().RowStateFilter
                        Case Data.DataViewRowState.Added
                            Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                        Case Data.DataViewRowState.CurrentRows
                            Me.GetStateCheckedBar2(Me.btnCurrent)
                        Case Data.DataViewRowState.Deleted
                            Me.GetStateCheckedBar2(Me.btnDelete)
                        Case Data.DataViewRowState.ModifiedCurrent
                            Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                        Case Data.DataViewRowState.ModifiedOriginal
                            Me.GetStateCheckedBar2(Me.btnModifiedOriginal)
                        Case Data.DataViewRowState.OriginalRows
                            Me.GetStateCheckedBar2(Me.btnOriginalRows)
                        Case Data.DataViewRowState.Unchanged
                            Me.GetStateCheckedBar2(Me.btnUnchaigned)
                    End Select
                    Me.Bindgrid(Me.clsRegion.ViewTerritoryManager())
                    Me.SelectBar = BarSelect.Territory_Manager
                Case "btnFS"
                    If Me.clsRegion.ViewFS().RowStateFilter = (Data.DataViewRowState.Added Or Data.DataViewRowState.ModifiedCurrent) Then
                        Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                    End If
                    Select Case Me.clsRegion.ViewFS().RowStateFilter
                        Case Data.DataViewRowState.Added
                            Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                        Case Data.DataViewRowState.CurrentRows
                            Me.GetStateCheckedBar2(Me.btnCurrent)
                        Case Data.DataViewRowState.Deleted
                            Me.GetStateCheckedBar2(Me.btnDelete)
                        Case Data.DataViewRowState.ModifiedCurrent
                            Me.GetStateCheckedBar2(Me.btnModifiedAdded)
                        Case Data.DataViewRowState.ModifiedOriginal
                            Me.GetStateCheckedBar2(Me.btnModifiedOriginal)
                        Case Data.DataViewRowState.OriginalRows
                            Me.GetStateCheckedBar2(Me.btnOriginalRows)
                        Case Data.DataViewRowState.Unchanged
                            Me.GetStateCheckedBar2(Me.btnUnchaigned)
                    End Select
                    Me.Bindgrid(Me.clsRegion.ViewFS())
                    Me.SelectBar = BarSelect.FieldSupervisor
            End Select
            Me.FillFilterColumn()
            Me.GridEX1.RootTable.Columns(0).CardCaption = True
            Me.GetStateCheckedBar1(CType(sender, DevComponents.DotNetBar.ButtonItem))
            'Me.GetStateCheckedBar2(Me.btnOriginalRows)

        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name & "_Bar1_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case Me.SelectBar
                Case BarSelect.Regional
                    Select Case item.Text
                        Case "ModifiedAdded"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewRegional(), "ModifiedAdded")
                            Me.Bindgrid(Me.clsRegion.ViewRegional())
                            'Me.lblLastID.Text = Me.GetLastID()
                            'Me.grpLastID.Visible = False
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "ModifiedOriginal"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewRegional(), "ModifiedOriginal")
                            Me.Bindgrid(Me.clsRegion.ViewRegional())
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                        Case "Deleted"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewRegional(), "Deleted")
                            Me.Bindgrid(Me.clsRegion.ViewRegional())
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "Current"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewRegional(), "Current")
                            Me.Bindgrid(Me.clsRegion.ViewRegional())
                            If Me.btnAddNew.Text = "Cancel Changes" Then
                                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                'Me.grpLastID.Visible = True
                            Else
                                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                                'Me.grpLastID.Visible = False
                            End If
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Case "Unchaigned"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewRegional(), "Unchaigned")
                            Me.Bindgrid(Me.clsRegion.ViewRegional())
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        Case "OriginalRows"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewRegional(), "OriginalRows")
                            Me.Bindgrid(Me.clsRegion.ViewRegional())
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End Select
                Case BarSelect.Territory
                    Select Case item.Text
                        Case "ModifiedAdded"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritory(), "ModifiedAdded")
                            Me.Bindgrid(Me.clsRegion.ViewTerritory())
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "ModifiedOriginal"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritory(), "ModifiedOriginal")
                            Me.Bindgrid(Me.clsRegion.ViewTerritory())
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.RootTable.Columns("TERRITORY_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                        Case "Deleted"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritory(), "Deleted")
                            Me.Bindgrid(Me.clsRegion.ViewTerritory)
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "Current"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritory(), "Current")
                            Me.Bindgrid(Me.clsRegion.ViewTerritory)
                            If Me.btnAddNew.Text = "Cancel Changes" Then
                                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                'Me.grpLastID.Visible = True
                            Else
                                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                                'Me.grpLastID.Visible = False
                            End If
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Case "Unchaigned"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritory(), "Unchaigned")
                            Me.Bindgrid(Me.clsRegion.ViewTerritory())
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        Case "OriginalRows"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritory(), "OriginalRows")
                            Me.Bindgrid(Me.clsRegion.ViewTerritory())
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.RootTable.Columns("TERRITORY_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit

                    End Select
                Case BarSelect.Territory_Manager
                    Select Case item.Text
                        Case "ModifiedAdded"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritoryManager(), "ModifiedAdded")
                            Me.Bindgrid(Me.clsRegion.ViewTerritoryManager())
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "ModifiedOriginal"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritoryManager(), "ModifiedOriginal")
                            Me.Bindgrid(Me.clsRegion.ViewTerritoryManager())
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.RootTable.Columns("TM_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                        Case "Deleted"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritoryManager(), "Deleted")
                            Me.Bindgrid(Me.clsRegion.ViewTerritoryManager())
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "Current"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritoryManager(), "Current")
                            Me.Bindgrid(Me.clsRegion.ViewTerritoryManager())
                            If Me.btnAddNew.Text = "Cancel Changes" Then
                                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                'Me.grpLastID.Visible = True
                            Else
                                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                                'Me.grpLastID.Visible = False
                            End If
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Case "Unchaigned"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritoryManager(), "Unchaigned")
                            Me.Bindgrid(Me.clsRegion.ViewTerritoryManager())
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        Case "OriginalRows"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewTerritoryManager(), "OriginalRows")
                            Me.Bindgrid(Me.clsRegion.ViewTerritoryManager())
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.RootTable.Columns("TM_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End Select
                Case BarSelect.FieldSupervisor
                    Select Case item.Text
                        Case "ModifiedAdded"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewFS, "ModifiedAdded")
                            Me.Bindgrid(Me.clsRegion.ViewFS())
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "ModifiedOriginal"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewFS(), "ModifiedOriginal")
                            Me.Bindgrid(Me.clsRegion.ViewFS())
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.RootTable.Columns("FS_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                        Case "Deleted"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewFS(), "Deleted")
                            Me.Bindgrid(Me.clsRegion.ViewFS())
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "Current"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewFS(), "Current")
                            Me.Bindgrid(Me.clsRegion.ViewFS())
                            If Me.btnAddNew.Text = "Cancel Changes" Then
                                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                'Me.grpLastID.Visible = True
                            Else
                                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                                'Me.grpLastID.Visible = False
                            End If
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Case "Unchaigned"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewFS(), "Unchaigned")
                            Me.Bindgrid(Me.clsRegion.ViewFS())
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        Case "OriginalRows"
                            Me.clsRegion.GetDataViewRowState(Me.clsRegion.ViewFS(), "OriginalRows")
                            Me.Bindgrid(Me.clsRegion.ViewFS())
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.RootTable.Columns("FS_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End Select
            End Select
            Me.GetStateCheckedBar2(CType(sender, DevComponents.DotNetBar.ButtonItem))
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name & "_Bar2_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Try
            If Not IsNothing(Me.clsRegion) Then
                If Not IsNothing(Me.clsRegion.GetDataSet()) Then
                    If Me.clsRegion.GetDataSet.HasChanges() Then
                        Me.clsRegion.GetDataSet.RejectChanges()
                    End If
                End If
            End If
            Me.Close()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnCLose_Click")
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'BESOK LAGI SUDAH
            Me.Cursor = Cursors.WaitCursor
            If Me.clsRegion.GetDataSet().HasChanges() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    Me.clsRegion.SaveChanges(Me.clsRegion.GetDataSet().GetChanges())
                    Me.ShowMessageInfo(Me.MessageSavingSucces)
                End If
                Me.LoadData()
                'Me.grpLastID.Visible = False
                Select Case Me.SelectBar
                    Case BarSelect.Regional
                        Me.Bindgrid(Me.clsRegion.ViewRegional())
                    Case BarSelect.Territory
                        Me.Bindgrid(Me.clsRegion.ViewTerritory())
                    Case BarSelect.Territory_Manager
                        Me.Bindgrid(Me.clsRegion.ViewTerritoryManager())
                    Case BarSelect.FieldSupervisor
                        Me.Bindgrid(Me.clsRegion.ViewFS())
                End Select
                Me.GetStateCheckedBar2(Me.btnOriginalRows)
                Me.btnAddNew.Text = "Add New"
                Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Me.GridEX1.AllowDelete = IIf(NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Region, Janus.Windows.GridEX.InheritableBoolean.True, Janus.Windows.GridEX.InheritableBoolean.False)
                Me.GridEX1.AllowAddNew = IIf(NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Region, Janus.Windows.GridEX.InheritableBoolean.True, Janus.Windows.GridEX.InheritableBoolean.False)
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name & "_btnSave_Click")
        Finally
            Me.ReadAcces()
            Me.ItemPanel1.Refresh()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_RecordAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.RecordAdded
        'Select Case Me.SelectBar
        '    Case BarSelect.Regional
        '        Me.clsRegion.ViewRegional().RowStateFilter = Data.DataViewRowState.CurrentRows
        '        'Me.lblLastID.Text = Me.GetLastID()
        '        Me.clsRegion.ViewRegional().Sort = "REGIONAL_ID"
        '    Case BarSelect.Territory
        '        Me.clsRegion.ViewTerritory().RowStateFilter = Data.DataViewRowState.CurrentRows
        '        'Me.lblLastID.Text = Me.GetLastID()
        '        Me.clsRegion.ViewTerritory().Sort = "TERRITORY_ID"
        '    Case BarSelect.Territory_Manager
        '        Me.clsRegion.ViewTerritoryManager().RowStateFilter = Data.DataViewRowState.CurrentRows
        '        Me.clsRegion.ViewTerritoryManager().Sort = "TM_ID"
        'End Select
        Me.GridEX1.UpdateData()
    End Sub

    'Private Sub GridEX1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.SelectionChanged
    '    Try
    '        If Me.SFG = StateFillingGrid.Filling Then
    '            Return
    '        End If
    '        If Me.btnAddNew.Text = "Cancel Changes" Then
    '            If Not Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
    '                Dim item As New Janus.Windows.GridEX.GridEXColumn
    '                For Each item In Me.GridEX1.RootTable.Columns
    '                    item.EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                Next
    '                Return
    '            End If
    '        End If
    '        Select Case Me.SelectBar
    '            Case BarSelect.Regional
    '                If Me.clsRegion.ViewRegional().RowStateFilter = Data.DataViewRowState.CurrentRows Then
    '                    If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then
    '                        Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                        Me.GridEX1.RootTable.Columns("REGIONAL_AREA").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                        Me.GridEX1.RootTable.Columns("MANAGER").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                        Me.GridEX1.RootTable.Columns("HP").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                    Else
    '                        Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                        Me.GridEX1.RootTable.Columns("REGIONAL_AREA").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                        Me.GridEX1.RootTable.Columns("MANAGER").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                        Me.GridEX1.RootTable.Columns("HP").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                    End If
    '                Else
    '                    Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                    Me.GridEX1.RootTable.Columns("REGIONAL_AREA").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                    Me.GridEX1.RootTable.Columns("MANAGER").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                    Me.GridEX1.RootTable.Columns("HP").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                End If
    '            Case BarSelect.Territory_Manager
    '                If Me.clsRegion.ViewTerritoryManager().RowStateFilter = Data.DataViewRowState.CurrentRows Then
    '                    If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then
    '                        Me.GridEX1.RootTable.Columns("TM_ID").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                        Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.DropDownList
    '                        Me.GridEX1.RootTable.Columns("TERRITORY_ID").EditType = Janus.Windows.GridEX.EditType.DropDownList
    '                        Me.GridEX1.RootTable.Columns("MANAGER").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                        Me.GridEX1.RootTable.Columns("HP").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                    Else
    '                        Me.GridEX1.RootTable.Columns("TM_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                        Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                        Me.GridEX1.RootTable.Columns("TERRITORY_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                        Me.GridEX1.RootTable.Columns("MANAGER").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                        Me.GridEX1.RootTable.Columns("HP").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                    End If
    '                Else
    '                    Me.GridEX1.RootTable.Columns("TM_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                    Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                    Me.GridEX1.RootTable.Columns("TERRITORY_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                    Me.GridEX1.RootTable.Columns("MANAGER").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                    Me.GridEX1.RootTable.Columns("HP").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                End If
    '            Case BarSelect.Territory
    '                If Me.clsRegion.ViewTerritory().RowStateFilter = Data.DataViewRowState.CurrentRows Then
    '                    If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then
    '                        'Me.GridEX1.RootTable.Columns("TM_ID").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                        'Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.DropDownList
    '                        Me.GridEX1.RootTable.Columns("TERRITORY_ID").EditType = Janus.Windows.GridEX.EditType.TextBox

    '                        Me.GridEX1.RootTable.Columns("TERRITORY_AREA").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                        Me.GridEX1.RootTable.Columns("TERRITORY_DESCRIPTION").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                    Else
    '                        Me.GridEX1.RootTable.Columns("TERRITORY_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit

    '                        Me.GridEX1.RootTable.Columns("TERRITORY_AREA").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                        Me.GridEX1.RootTable.Columns("TERRITORY_DESCRIPTION").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                    End If
    '                Else
    '                    Me.GridEX1.RootTable.Columns("TERRITORY_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit

    '                    Me.GridEX1.RootTable.Columns("TERRITORY_AREA").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                    Me.GridEX1.RootTable.Columns("TERRITORY_DESCRIPTION").EditType = Janus.Windows.GridEX.EditType.TextBox
    '                End If
    '        End Select
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub Region_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            'Me.clsRegion.ViewRegional().RowStateFilter = Data.DataViewRowState.OriginalRows
            'Me.clsRegion.ViewTerritory().RowStateFilter = Data.DataViewRowState.Unchanged
            Me.Bindgrid(Me.clsRegion.ViewRegional)
            Me.SelectBar = BarSelect.Regional
            Me.GetStateCheckedBar1(Me.btnRegional)
            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
        Catch ex As Exception
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Anchor + "_Region_Load")
        Finally
            Me.ReadAcces()
            Me.SFG = StateFillingGrid.HasFilled
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_Error(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ErrorEventArgs) Handles GridEX1.Error
        Try
            Me.ShowMessageInfo("Data can not be updated due to " & e.Exception.Message)
            Me.GridEX1.CancelCurrentEdit()
            Me.GridEX1.Refetch()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            If Me.SFG = StateFillingGrid.Filling Then
                Return
            End If
            If Not Me.SelectBar = BarSelect.FieldSupervisor Then
                If Not CType(Me.GridEX1.DataSource, DataView).RowStateFilter = Data.DataViewRowState.Added Then
                    Me.GridEX1.RootTable.Columns(0).EditType = Janus.Windows.GridEX.EditType.NoEdit
                Else
                    Me.GridEX1.RootTable.Columns(0).EditType = Janus.Windows.GridEX.EditType.TextBox
                End If
                If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                    For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                        If col.Key = "REGIONAL_ID" Then
                            'If Me.SelectBar = BarSelect.Territory Then
                            '    Me.GridEX1.RootTable.Columns("REGIONAL_ID").EditType = Janus.Windows.GridEX.EditType.MultiColumnDropDown
                            'Else
                            '    col.EditType = Janus.Windows.GridEX.EditType.TextBox
                            'End If
                        ElseIf col.Key = "PARENT_REGIONAL" Or col.Key = "PARENT_TERRITORY" Then
                        Else
                            col.EditType = Janus.Windows.GridEX.EditType.TextBox
                        End If
                    Next
                End If
            Else
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                    If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                        If col.Key = "CUR_TERRITORY_ID" Or col.Key = "FS_ID" Then
                            Me.GridEX1.RootTable.Columns(col.Index).Selectable = False
                        Else
                            Me.GridEX1.RootTable.Columns(col.Index).Selectable = True
                        End If
                    Else
                        Me.GridEX1.RootTable.Columns(col.Index).Selectable = True
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

    Private Sub GridEX1_RecordsDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.RecordsDeleted
        Me.GridEX1.UpdateData()
        Me.SFG = StateFillingGrid.HasFilled
    End Sub
End Class
