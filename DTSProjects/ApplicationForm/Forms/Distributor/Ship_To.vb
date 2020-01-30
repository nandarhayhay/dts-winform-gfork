Public Class Ship_To

#Region " Deklarasi "

    Private m_clsShipTo As NufarmBussinesRules.DistributorRegistering.Ship_To
    Friend CMain As Main = Nothing
    Private ReadOnly Property clsShipTo() As NufarmBussinesRules.DistributorRegistering.Ship_To
        Get
            If Me.m_clsShipTo Is Nothing Then
                Me.m_clsShipTo = New NufarmBussinesRules.DistributorRegistering.Ship_To()
            End If
            Return Me.m_clsShipTo
        End Get
    End Property

#End Region

#Region " Sub "

    Friend Sub InitializeData()
        Me.LoadData()
    End Sub

    Private Sub LoadData()
        'getdata
        Me.clsShipTo.GetDataview()
        Me.BindGrid()
    End Sub
    Private isLoadingRow As Boolean = False
    Private Sub FillcategoriesValueList()
        'Me.grdShipTo.DropDowns("Distributor").SetDataBinding(Me.clsShipTo.ViewDistributor(), "")
        'Me.grdShipTo.DropDowns("Distributor").Columns("DISTRIBUTOR_ID").AutoSize()
        'Me.grdShipTo.DropDowns("Distributor").Columns("DISTRIBUTOR_ID").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DisplayedCellsAndHeader
        'Me.grdShipTo.DropDowns("Distributor").Columns("DISTRIBUTOR_NAME").AutoSize()
        'Me.grdShipTo.DropDowns("Distributor").Columns("DISTRIBUTOR_NAME").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DisplayedCellsAndHeader

        Me.grdShipTo.DropDowns("TERRITORY").SetDataBinding(Me.clsShipTo.ViewTerritory(), "")
        'Me.grdShipTo.DropDowns("TERRITORY").Columns("TERRITORY_ID").AutoSize()
        'Me.grdShipTo.DropDowns("TERRITORY").Columns("TERRITORY_ID").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DisplayedCellsAndHeader
        'Me.grdShipTo.DropDowns("TERRITORY").Columns("TERRITORY_AREA").AutoSize()
        'Me.grdShipTo.DropDowns("TERRITORY").Columns("TERRITORY_AREA").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DisplayedCellsAndHeader

        'Dim ColumnTERID As Janus.Windows.GridEX.GridEXColumn = grdShipTo.RootTable.Columns("TERRITORY_ID")
        'ColumnTERID.Caption = "TERRITORY_AREA"
        'ColumnTERID.EditType = Janus.Windows.GridEX.EditType.NoEdit
        'ColumnTERID.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        'ColumnTERID.AutoSize()
        'ColumnTERID.AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DisplayedCellsAndHeader
        ''Set HasValueList property equal to true in order to be able to use the ValueList property
        'ColumnTERID.HasValueList = True
        ''Get the ValueList collection associated to this column
        'Dim ValueList As Janus.Windows.GridEX.GridEXValueListItemCollection = ColumnTERID.ValueList
        'ValueList.PopulateValueList(Me.clsShipTo.ViewTerritory(), "TERRITORY_ID", "TERRITORY_AREA")
        'ColumnTERID.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        'ColumnTERID.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
        'Fill the ValueList
        'ValueList.PopulateValueList(Ds.Tables("Categories").DefaultView, "CategoryID", "CategoryName")

        Me.grdShipTo.DropDowns("TM").SetDataBinding(Me.clsShipTo.ViewManager(), "")
        Me.grdShipTo.DropDowns("TM").Columns("TM_ID").AutoSize()
        Me.grdShipTo.DropDowns("TM").Columns("TM_ID").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DisplayedCellsAndHeader
        Me.grdShipTo.DropDowns("TM").Columns("MANAGER").AutoSize()
        Me.grdShipTo.DropDowns("TM").Columns("MANAGER").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DisplayedCellsAndHeader
    End Sub

    Private Sub BindGrid()
        Me.isLoadingRow = True
        Me.grdShipTo.SetDataBinding(Me.clsShipTo.ViewShip_To(), "")
        Me.FillcategoriesValueList()
        Me.isLoadingRow = False
    End Sub

    'Private Sub FillCategoryTerritoryID(ByVal DISTRIBUTOR_ID As String)
    '    'Me.clsShipTo.ViewManager().RowFilter = "DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'"
    '    'Me.grdShipTo.DropDowns("TM").SetDataBinding(Me.clsShipTo.ViewManager(), "")
    '    'Me.grdShipTo.Refresh()
    '    Dim territoryID As String = Me.clsShipTo.getTerritoryID(DISTRIBUTOR_ID)
    '    Me.grdShipTo.SetValue("TERRITORY_ID", territoryID)
    'End Sub

#End Region

#Region " Event Procedure "

    Private Sub mnuBar_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBar.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnRenameColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.grdShipTo
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar1)
                Case "btnShowFieldChooser"
                    Me.grdShipTo.ShowFieldChooser(Me)
                    'Select Case Me.SGrid
                    '    Case SelectedGrid.grdShipTo

                    '    Case SelectedGrid.grdTerritory
                    '        Me.grdTerritory.ShowFieldChooser(Me)
                    'End Select
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.grdShipTo
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    'Select Case Me.SGrid
                    '    Case SelectedGrid.grdShipTo

                    '        SetGrid.GridExPrintDock = Me.GridEXPrintDocument2
                    '    Case SelectedGrid.grdTerritory
                    '        SetGrid.Grid = Me.grdTerritory

                    'End Select
                    SetGrid.ShowDialog()
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.grdShipTo
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                        Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    End If
                    'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                    'Select Case Me.SGrid
                    '    Case SelectedGrid.grdShipTo
                    '        Me.GridEXPrintDocument2.GridEX = Me.grdShipTo
                    '        Me.PrintPreviewDialog2.Document = Me.GridEXPrintDocument2
                    '        If Not IsNothing(Me.PageSetupDialog2.PageSettings) Then
                    '            Me.PrintPreviewDialog2.Document.DefaultPageSettings = Me.PageSetupDialog2.PageSettings
                    '        End If
                    '        'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    '        If Me.PrintPreviewDialog2.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    '            Me.PrintPreviewDialog2.Document.Print()
                    '        End If
                    '    Case SelectedGrid.grdTerritory


                    'End Select

                Case "btnPageSettings"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
                    'Select Case SGrid

                    '    Case SelectedGrid.grdShipTo
                    '        Me.PageSetupDialog2.Document = Me.GridEXPrintDocument2
                    '        Me.PageSetupDialog2.ShowDialog(Me)
                    '    Case SelectedGrid.grdTerritory

                    'End Select
                Case "btnCustomFilter"
                    Me.FilterEditor1.Visible = True
                    Me.FilterEditor1.SortFieldList = False
                    Me.FilterEditor1.SourceControl = Me.grdShipTo
                    Me.grdShipTo.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.grdShipTo.RemoveFilters()
                    Me.grdShipTo.Refetch()
                    'Select Case Me.SGrid
                    '    Case SelectedGrid.grdShipTo

                    '        Me.clsShipTo.ViewShipToTerritory().RowFilter = ""

                    '        'Me.dtPicFrom.Text = ""
                    '        'Me.dtpicUntil.Text = ""
                    '        'Me.dtPicFrom.Enabled = False
                    '        'Me.dtpicUntil.Enabled = False
                    '        'Me.btnAplyRange.Enabled = False
                    '    Case SelectedGrid.grdTerritory
                    '        Me.FilterEditor1.SourceControl = Me.grdTerritory
                    '        Me.grdTerritory.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    '        Me.grdTerritory.RemoveFilters()
                    '        Me.clsShipTo.ViewTerritoryArea().RowFilter = ""
                    '        Me.grdTerritory.Refetch()
                    'End Select
                    'Me.GetStateChecked(Me.btnCustomFilter)
                Case "btnFilterDefault"
                    Me.FilterEditor1.Visible = False
                    Me.grdShipTo.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    'Select Case Me.SGrid
                    '    Case SelectedGrid.grdShipTo
                    '        Me.grdShipTo.RemoveFilters()
                    '        'Me.GetStateChecked(Me.btnFilterEqual)

                    '        'Me.btnAplyRange.Enabled = True
                    '        'Me.dtPicFrom.Enabled = True
                    '        'Me.dtpicUntil.Enabled = True
                    '    Case SelectedGrid.grdTerritory
                    '        Me.grdTerritory.RemoveFilters()
                    '        'Me.GetStateChecked(Me.btnFilterEqual)
                    '        'Me.btnAplyRange.Enabled = True
                    '        'Me.dtPicFrom.Enabled = True
                    '        'Me.dtpicUntil.Enabled = True
                    '        Me.grdTerritory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    'End Select
                Case "btnAddNew"
                    'Dim frmPurchaseOrder As New Purchase_Order()
                    'frmPurchaseOrder.ShowInTaskbar = False
                    'frmPurchaseOrder.Mode = Purchase_Order.ModeSave.Save
                    'frmPurchaseOrder.InitializeData()
                    'Me.GetStateChecked(Me.btnAddNew)
                    'frmPurchaseOrder.ShowDialog(Me)
                    'Me.ClearCheckedState()
                    'Me.RefreshData()
                    Me.btnAddNew.Checked = Not Me.btnAddNew.Checked

                    If Me.btnAddNew.Checked = True Then
                        Me.grdShipTo.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                        'Me.grdShipTo.FilterMode = Janus.Windows.GridEX.FilterMode.None

                        'Me.ExpandableSplitter1.Expanded = True
                        'Select Case Me.SGrid
                        '    Case SelectedGrid.grdShipTo
                        '        Me.grpShipTo.Visible = True
                        '        Me.grpterritpry.Visible = False
                        '        Me.SFM = StateFillingMCB.Filing
                        '        Me.mcbDistributor.Value = Nothing
                        '        Me.chkTerritory.Values = Nothing
                        '        Me.mcbDistributor.Focus()
                        '    Case SelectedGrid.grdTerritory
                        '        Me.grpterritpry.Visible = True
                        '        Me.grpShipTo.Visible = False
                        '        Me.ClearControl(Me.grpterritpry)
                        '        Me.txtTerritoryID.Enabled = True
                        '        Me.txtTerritoryID.Focus()
                        'End Select

                        'Me.btnInsert.Text = "&Insert"
                        'Me.btnInsert.Enabled = True
                        'Me.SaveMode = Mode.Save
                    Else
                        'Me.ExpandableSplitter1.Expanded = False
                        Me.grdShipTo.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.grdShipTo.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    End If

                Case "btnExport"
                    Me.SaveFileDialog1.Title = "Define the location File"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.grdShipTo
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    'Select Case Me.SGrid
                    '    Case SelectedGrid.grdShipTo

                    '    Case SelectedGrid.grdTerritory
                    '        Me.SaveFileDialog1.Title = "Define the location File"
                    '        Me.SaveFileDialog1.OverwritePrompt = True
                    '        Me.SaveFileDialog1.DefaultExt = ".xls"
                    '        Me.SaveFileDialog1.Filter = "All Files|*.*"
                    '        Me.SaveFileDialog1.InitialDirectory = "C:\"
                    '        If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    '            Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                    '            Me.GridEXExporter1.GridEX = Me.grdTerritory
                    '            Me.GridEXExporter1.Export(FS)
                    '            FS.Close()
                    '            MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        End If
                    'End Select

                Case "btnRefresh"
                    'Me.LoadData()
                    If Not IsNothing(Me.clsShipTo.GetDateSet()) Then
                        If Me.clsShipTo.GetDateSet.HasChanges() Then
                            If MessageBox.Show("Are you sure you want to refresh" & vbCrLf & "All curently any unsaved data will be discarded", "Data has changed !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                                Return
                            End If
                            Me.clsShipTo.GetDateSet().RejectChanges() : Me.LoadData()
                        End If
                    End If
            End Select
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_mnuBar_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdShipTo_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdShipTo.AddingRecord
        If Me.isLoadingRow Then : Return : End If
        Try

            'check data
            'If (IsNothing(Me.grdShipTo.GetValue("DISTRIBUTOR_ID"))) Or (IsDBNull(Me.grdShipTo.GetValue("DISTRIBUTOR_ID"))) Then
            '    Me.ShowMessageInfo("please suply distributor")
            '    e.Cancel = True
            '    Me.grdShipTo.CancelCurrentEdit()
            '    Me.grdShipTo.MoveToNewRecord()
            '    Return
            If (IsNothing(Me.grdShipTo.GetValue("TERRITORY_ID"))) Or (IsDBNull(Me.grdShipTo.GetValue("TERRITORY_ID"))) Then
                Me.ShowMessageInfo("Please suply Territory")
                e.Cancel = True
                Me.grdShipTo.CancelCurrentEdit()
                Me.grdShipTo.MoveToNewRecord()
                Return
            ElseIf (IsNothing(Me.grdShipTo.GetValue("TM_ID"))) Or (IsDBNull(Me.grdShipTo.GetValue("TM_ID"))) Then
                Me.ShowMessageInfo("Please suply Teritory_Manager")
                e.Cancel = True
                Me.grdShipTo.CancelCurrentEdit()
                Me.grdShipTo.MoveToNewRecord()
                Return
                'check dataview
            ElseIf Me.clsShipTo.ViewShip_To().Find(Me.grdShipTo.GetValue("SHIP_TO_ID")) <> -1 Then
                Me.ShowMessageInfo("Data has existed in")
                e.Cancel = True
                Me.grdShipTo.CancelCurrentEdit()
                Me.grdShipTo.MoveToNewRecord()
                Return
            End If
            Dim SHIP_TO_ID As String = Me.grdShipTo.GetValue("TERRITORY_ID").ToString() & "" & Me.grdShipTo.GetValue("TM_ID").ToString()
            Me.grdShipTo.SetValue("SHIP_TO_ID", SHIP_TO_ID)
            Me.grdShipTo.SetValue("INACTIVE", False)
            Me.grdShipTo.SetValue("CREATE_DATE", NufarmBussinesRules.SharedClass.ServerDate.ToShortDateString())
            Me.grdShipTo.SetValue("CREATE_BY", NufarmBussinesRules.User.UserLogin.UserName)
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdShipTo_AddingRecord")
        End Try
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.Close()
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.clsShipTo.GetDateSet().HasChanges() Then
                Me.clsShipTo.SaveChanges(Me.clsShipTo.GetDateSet())
                Me.ShowMessageInfo(Me.MessageSavingSucces)
                Me.clsShipTo.GetDateSet.AcceptChanges()
                'Me.LoadData()
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnSave_Click")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdShipTo_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdShipTo.CellUpdated
        If Me.isLoadingRow Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor
            If (e.Column.Key = "TERRITORY_ID") Or (e.Column.Key = "TM_ID") Then
                If Me.grdShipTo.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If (Me.grdShipTo.GetValue(e.Column) Is Nothing) Or (Me.grdShipTo.GetValue(e.Column) Is DBNull.Value) Then
                        Me.grdShipTo.CancelCurrentEdit()
                        Me.ShowMessageInfo("can not set empty value")
                        Return
                    End If
                    If e.Column.Key = "TERRITORY_ID" Then
                        If (Not IsDBNull(Me.grdShipTo.GetValue("TM_ID"))) And (Not IsNothing(Me.grdShipTo.GetValue("TM_ID"))) Then
                            Dim SHIP_TO_ID As String = Me.grdShipTo.GetValue("TERRITORY_ID").ToString() & "" & Me.grdShipTo.GetValue("TM_ID").ToString()
                            Me.grdShipTo.SetValue("SHIP_TO_ID", SHIP_TO_ID)
                        End If
                    ElseIf e.Column.Key = "TM_ID" Then
                        If (Not IsDBNull(Me.grdShipTo.GetValue("TERRITORY_ID"))) And (Not IsNothing(Me.grdShipTo.GetValue("TERRITORY_ID"))) Then
                            Dim SHIP_TO_ID As String = Me.grdShipTo.GetValue("TERRITORY_ID").ToString() & "" & Me.grdShipTo.GetValue("TM_ID").ToString()
                            Me.grdShipTo.SetValue("SHIP_TO_ID", SHIP_TO_ID)
                        End If
                    End If
                End If
                If (Me.grdShipTo.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                    Me.isLoadingRow = True : Me.grdShipTo.UpdateData() : Me.isLoadingRow = False
                End If
            End If

        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdShipTo_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdShipTo.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not NufarmBussinesRules.User.UserLogin.IsAdmin Then
                If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.SHIP_TO = True Then
                    Me.ShowMessageInfo("You have no privilege to do such operation")
                    e.Cancel = True
                    Return
                End If
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Return
            End If
            e.Cancel = False
            'Me.clsShipTo.Delete(Me.grdShipTo.GetValue("SHIP_TO_ID").ToString())
            'Me.LoadData()
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_grdShipTo_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub grdShipTo_DropDown(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdShipTo.DropDown
    '    Try
    '        If Not e.Column.Key = "DISTRIBUTOR_NAME" Then
    '            If (IsNothing(Me.grdShipTo.GetValue("DISTRIBUTOR_ID"))) Or (IsDBNull(Me.grdShipTo.GetValue("DISTRIBUTOR_ID"))) Then
    '                Me.ShowMessageInfo("Please define Distributor first")
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub Ship_To_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsShipTo) Then
                If Not IsNothing(Me.clsShipTo.GetDateSet()) Then
                    If Me.clsShipTo.GetDateSet().HasChanges() Then
                        If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                            Me.clsShipTo.SaveChanges(Me.clsShipTo.GetDateSet())
                        End If
                    End If
                End If
                Me.clsShipTo.Dispose(True)
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Ship_To_FormClosing")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdShipTo_Error(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ErrorEventArgs) Handles grdShipTo.Error
        Try
            Me.ShowMessageInfo("Data can not be updated due to " & e.ErrorMessage)
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub grdShipTo_UpdatingCell(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.UpdatingCellEventArgs) Handles grdShipTo.UpdatingCell
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        'If Not Me.grdShipTo.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
    '        '    If Me.clsShipTo.IsHasReferenced(Me.grdShipTo.GetValue("SHIP_TO_ID").ToString()) = True Then
    '        '        Me.ShowMessageInfo(Me.MessageDataCantChanged)
    '        '        e.Cancel = True
    '        '    End If
    '        'End If
    '        'If e.Column.Key = "DISTRIBUTOR_NAME" Then
    '        '    Dim DISTRIBUTOR_ID As Object = Me.grdShipTo.DropDowns("Distributor").GetValue("DISTRIBUTOR_ID")
    '        '    Me.grdShipTo.SetValue("DISTRIBUTOR_ID", DISTRIBUTOR_ID)
    '        '    Me.FillCategoryTerritoryID(DISTRIBUTOR_ID)
    '        If (e.Column.Key = "TERRITORY_ID") Or (e.Column.Key = "TM_ID") Then
    '            If Me.grdShipTo.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
    '                If (Me.grdShipTo.GetValue(e.Column) Is Nothing) Or (Me.grdShipTo.GetValue(e.Column) Is DBNull.Value) Then
    '                    e.Cancel = True
    '                    Me.ShowMessageInfo("can not set empty value")
    '                    Return
    '                End If
    '                If e.Column.Key = "TERRITORY_ID" Then
    '                    If (Not IsDBNull(Me.grdShipTo.GetValue("TM_ID"))) And (Not IsNothing(Me.grdShipTo.GetValue("TM_ID"))) Then
    '                        Dim SHIP_TO_ID As String = Me.grdShipTo.GetValue("TERRITORY_ID").ToString() & "" & Me.grdShipTo.GetValue("TM_ID").ToString()
    '                        Me.grdShipTo.SetValue("SHIP_TO_ID", SHIP_TO_ID)
    '                    End If
    '                ElseIf e.Column.Key = "TM_ID" Then
    '                    If (Not IsDBNull(Me.grdShipTo.GetValue("TERRITORY_ID"))) And (Not IsNothing(Me.grdShipTo.GetValue("TERRITORY_ID"))) Then
    '                        Dim SHIP_TO_ID As String = Me.grdShipTo.GetValue("TERRITORY_ID").ToString() & "" & Me.grdShipTo.GetValue("TM_ID").ToString()
    '                        Me.grdShipTo.SetValue("SHIP_TO_ID", SHIP_TO_ID)
    '                    End If

    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Me.ShowMessageInfo(ex.Message)
    '        Me.grdShipTo.CancelCurrentEdit()
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

#End Region

    Private Sub Ship_To_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
    End Sub
End Class
