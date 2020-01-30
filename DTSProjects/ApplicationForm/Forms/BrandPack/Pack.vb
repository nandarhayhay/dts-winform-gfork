Public Class Pack

#Region " Deklarasi "
    Private clsPack As NufarmBussinesRules.Brandpack.Pack
    Private isLoadingRow As Boolean = False
    Friend CMain As Main
#End Region

#Region " Sub "
    Private Sub GetStateChecked(ByVal item As DevComponents.DotNetBar.ButtonItem)
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar1.Items
            If Not item1.Name = item.Name Then
                item1.Checked = False
            Else
                item1.Checked = True
            End If
        Next
    End Sub
    Private Sub FillValueList()
        'AMBIL DATA DI ACCPACK 
        Dim VList() As String = {"TABLET", "LITRE", "MILILITRE", "KILOGRAM", "GRAM", "BAGS", "SACHET"}
        Dim ColUnit As Janus.Windows.GridEX.GridEXColumn = Me.GridEX1.RootTable.Columns("UNIT")
        ColUnit.EditType = Janus.Windows.GridEX.EditType.Combo
        ColUnit.AutoComplete = True : ColUnit.HasValueList = True
        Dim ValueListUnit As Janus.Windows.GridEX.GridEXValueListItemCollection = ColUnit.ValueList
        ValueListUnit.PopulateValueList(VList, "UNIT")
        ColUnit.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        ColUnit.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    End Sub
    Private Sub LoadData()
        Me.clsPack = New NufarmBussinesRules.Brandpack.Pack()
        Me.clsPack.CreateDataViewAllPack()
    End Sub
    Private Sub BindGrid(ByVal dtView As DataView)
        Me.isLoadingRow = True
        Me.GridEX1.SetDataBinding(dtView, "")
        Me.GridEX1.RootTable.Columns("PACK_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("PACK_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("CREATE_BY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("CREATE_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.GridEX1.RootTable.Columns("MODIFY_BY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("MODIFY_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.GridEX1.RootTable.Columns("PACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.FillValueList()
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.isLoadingRow = False
    End Sub
    Private Function GetLastID() As String
        Dim LastID As String = ""
        'Me.clsPack.ViewPack.RowFilter = ""
        LastID = Me.GridEX1.GetValue(0).ToString()
        'LastID = Me.clsPack.ViewPack.Item(Me.clsPack.ViewPack.Count - 1).Item("PACK_ID").ToString()
        Return LastID
    End Function
    Private Sub LockedColumCreateModify()
        For Each item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If (item.DataMember = "CREATE_BY") Or (item.DataMember = "CREATE_DATE") _
            Or (item.DataMember = "MODIFY_BY") Or (item.DataMember = "MODIFY_DATE") Then
                item.EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
        Next
    End Sub
    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PACK = True Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If (NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PACK = True) And (NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PACK = True) Then
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PACK = True Then
                Me.btnAddNew.Visible = False
                Me.grpViewMode.Text = "Data View Mode"
                Me.Bar1.Visible = True
                Me.btnSaveChanges.Enabled = True
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PACK = True Then
                Me.btnAddNew.Visible = False
                Me.grpViewMode.Text = "Data View Mode"
                Me.Bar1.Visible = True
                Me.btnSaveChanges.Enabled = True
            Else
                Me.btnAddNew.Visible = False
                Me.grpViewMode.Text = ""
                Me.Bar1.Visible = False
                Me.btnSaveChanges.Enabled = False
            End If
        End If

    End Sub
#End Region

#Region " Event "
    Private Sub Pack_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If Not IsNothing(Me.clsPack) Then
                If Me.clsPack.GetDataSet().HasChanges() Then
                    If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                        Me.clsPack.SaveData(Me.clsPack.GetDataSet().GetChanges())
                        Me.ShowMessageInfo(Me.MessageSavingSucces)
                    End If
                End If
                Me.clsPack.Dispose(True)
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Brand_FormClosed")
        End Try
    End Sub
    Private Sub Pack_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.LoadData()
            Me.isLoadingRow = True
            Me.BindGrid(Me.clsPack.GetAllDataViewPack())
            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Me.FilterEditor1.Visible = False
            Me.ReadAcces()
        Catch ex As Exception
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "Pack_Load")
        Finally
            Me.CMain.StatProg = Main.StatusProgress.None : Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.isLoadingRow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            'event grid
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnCustomFilter"
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    'Dim CustFilter As New Filter()
                    'CustFilter.ReadData(Me.GridEX1.RootTable)
                    'CustFilter.Show(Me.Bar3)
                    Me.FilterEditor1.Visible = True
                    Me.GridEX1.RemoveFilters()
                Case "btnFilterEqual"
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.FilterEditor1.Visible = False
                    Me.GridEX1.RemoveFilters()
                    'Case "btnRemoveFilter"
                    '    Me.GridEX1.RemoveFilters()
                    '    Me.GridEX1.Refresh()
                Case "btnRenameColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar3)
                Case "btnHideColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Hide"
                    MC.TopMost = True
                    MC.Show(Me.Bar3)
                Case "btnShowColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Show"
                    MC.TopMost = True
                    MC.Show(Me.Bar3)
                Case "btnRefresh"
                    If Me.clsPack.GetDataSet.HasChanges() Then
                        If Me.ShowConfirmedMessage("Data has changed !" & vbCrLf & "If you continue refreshing" & vbCrLf & "All Changes will be discarded" & _
                        vbCrLf & "Continue refreshing anyway ?.") = Windows.Forms.DialogResult.No Then
                            'Me.clsBrand.SaveData(Me.clsBrand.GetDataSet().GetChanges())
                            'Me.ShowMessageInfo(Me.MessageSavingSucces)
                            Return
                        End If
                        'Me.grpLastID.Visible = False
                    End If
                    Me.LoadData()
                    Me.BindGrid(Me.clsPack.GetAllDataViewPack())
                    Me.clsPack.GetDataViewRowState("Current")
                    Me.GridEX1.Refetch()
                    Me.GetStateChecked(Me.btnAllData)
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.GridEX1.RootTable.Columns("PACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                    Me.GridEX1.MoveFirst()
                    Me.btnAddNew.Text = "Add New"
                Case "btnAddNew"
                    Select Case item.Text
                        Case "Add New"
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                            item.Text = "Cancel Changes"
                            Me.clsPack.GetAllDataViewPack.RowStateFilter = Data.DataViewRowState.CurrentRows
                            Me.BindGrid(Me.clsPack.GetAllDataViewPack())
                            Me.GetStateChecked(Me.btnCurrent)
                            'Me.lblLastID.Text = Me.clsPack.ViewPack(Me.clsPack.ViewPack.Count - 1)("PACK_ID").ToString()
                            'Me.grpLastID.Visible = True
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.RootTable.Columns("PACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.MoveToNewRecord()
                        Case "Cancel Changes"
                            If Me.clsPack.GetDataSet().HasChanges() Then
                                Me.clsPack.GetDataSet().RejectChanges()
                            End If
                            Me.BindGrid(Me.clsPack.GetDataViewRowState("Current"))
                            Me.GetStateChecked(Me.btnAllData)
                            item.Text = "Add New"
                            'Me.grpLastID.Visible = False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.RootTable.Columns("PACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                            Me.GridEX1.MoveFirst()
                    End Select
                Case "btnPrint"
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    'Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                    'Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
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
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "Pack_Bar2_ItemClick")
        End Try
    End Sub

    Private Sub btnSaveChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveChanges.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.clsPack.GetDataSet.HasChanges() Then
                If CMain.IsSystemAdministrator Then
                    If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                        Me.clsPack.SaveData(Me.clsPack.GetDataSet().GetChanges())
                        Me.ShowMessageInfo(Me.MessageSavingSucces)
                    End If
                    Me.LoadData()
                    Me.BindGrid(Me.clsPack.GetAllDataViewPack())
                    Me.clsPack.GetDataViewRowState("Current")
                    Me.GridEX1.Refetch()
                    Me.GetStateChecked(Me.btnAllData)
                Else
                    Me.ShowMessageInfo("Sorry only admin can do such operation")
                End If
                Me.btnAddNew.Text = "Add New"
            End If
            'next result
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.BindGrid(Me.clsPack.GetDataView())
            Me.btnAddNew.Text = "Add New"
            Me.LogMyEvent(ex.Message, "btnSaveChanges_Click")
        Finally
            Me.Cursor = Cursors.Default
            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
        End Try
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Try
            If Not IsNothing(Me.clsPack) Then
                If Not IsNothing(Me.clsPack.GetDataSet()) Then
                    If CMain.IsSystemAdministrator Then
                        If Me.clsPack.GetDataSet.HasChanges() Then
                            Me.clsPack.GetDataSet.RejectChanges()
                        End If
                    End If
                End If
            End If
            Me.Dispose(True)
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_btnCLose_Click")
        End Try
    End Sub

    'Private Sub GridEX1_CellEdited(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellEdited
    '    Try
    '        If e.Column.Key = "PACK_NAME" Then
    '            Me.GridEX1.SearchColumn = Me.GridEX1.RootTable.Columns("PACK_NAME")
    '            If Me.GridEX1.Find(Me.GridEX1.RootTable.Columns("PACK_NAME"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.GridEX1.GetValue("PACK_NAME"), -1, 1) Then
    '                Me.ShowMessageInfo("PACK_NAME Has Existed !." & vbCrLf & "We Suggest PACK_NAME not Equal" _
    '                 & vbCrLf & "You can delete the row by moving to modified Added DataView" & vbCrLf & "Press Del If you want to delete.")
    '                Return
    '                'End If
    '            End If
    '        ElseIf e.Column.Key = "UNIT" Then
    '            Select Case Me.GridEX1.GetValue(e.Column.Index)
    '                Case "KILOGRAM"
    '                    Me.GridEX1.SetValue("DEVIDE_FACTOR", 1)
    '                Case "GRAM"
    '                    Me.GridEX1.SetValue("DEVIDE_FACTOR", 1000)
    '                Case "TABLET"
    '                    Me.GridEX1.SetValue("DEVIDE_FACTOR", 1)
    '                Case "LITRE"
    '                    Me.GridEX1.SetValue("DEVIDE_FACTOR", 1)
    '                Case "MILILITRE"
    '                    Me.GridEX1.SetValue("DEVIDE_FACTOR", 1000)
    '                Case "BAGS"
    '                    Me.GridEX1.SetValue("DEVIDE_FACTOR", 1)
    '            End Select
    '        End If
    '        'If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then

    '        'End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        Try
            If (Me.GridEX1.GetValue("PACK_ID") Is DBNull.Value) Or (Me.GridEX1.GetValue("PACK_ID").Equals("")) Then
                Me.ShowMessageInfo("ID Must not be NULL .!")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Me.GridEX1.MoveToNewRecord()
                Return
            End If
            If Me.clsPack.ViewPack.Find(Me.GridEX1.GetValue("PACK_ID")) <> -1 Then
                Me.ShowMessageInfo("ID has existed in DATAVIEW .!" & vbCrLf & "ID must be Unique")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Me.GridEX1.MoveToNewRecord()
                Return
            End If
            If (Me.GridEX1.GetValue("PACK_NAME") Is DBNull.Value) Or (Me.GridEX1.GetValue("PACK_NAME").Equals("")) Then
                Me.ShowMessageInfo("PACK_NAME Must not be NULL .!")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Me.GridEX1.MoveToNewRecord()
                Return
            End If
            If (IsDBNull(Me.GridEX1.GetValue("QUANTITY"))) Or (Me.GridEX1.GetValue("QUANTITY") = 0) Then
                Me.ShowMessageInfo("Please suply Quantity")
                Me.GridEX1.MoveToNewRecord()
                Return
            End If
            If (Me.GridEX1.GetValue("DIVIDE_FACTOR") Is DBNull.Value) Or (Me.GridEX1.GetValue("DIVIDE_FACTOR") = 0) Then
                Me.ShowMessageInfo("Please Suply Devide_Factor")
                Me.GridEX1.MoveToNewRecord()
                Return
            End If
            If (Me.GridEX1.GetValue("UNIT") = CObj("")) Or (Me.GridEX1.GetValue("UNIT") Is DBNull.Value) Then
                Me.ShowMessageInfo("Please suply Unit")
                Me.GridEX1.MoveToNewRecord()
                Return
            End If
            Me.isLoadingRow = True
            Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("CREATE_DATE"), NufarmBussinesRules.SharedClass.ServerDate())
            Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("CREATE_BY"), NufarmBussinesRules.User.UserLogin.UserName)
            Me.GridEX1.SetValue("IsActive", True) : Me.GridEX1.SetValue("IsObsolete", False)
            Me.isLoadingRow = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        Try
            If Me.isLoadingRow Then : Return : End If
            'If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            If e.Column.Key = "PACK_NAME" Then
                If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
                    Me.ShowMessageInfo("PACK_NAME must not be NULL")
                    Me.GridEX1.CancelCurrentEdit()
                    Me.GridEX1.Refetch()
                    Return
                End If
            End If
            Me.isLoadingRow = True
            If e.Column.Key = "PACK_NAME" Then
                Me.GridEX1.SearchColumn = Me.GridEX1.RootTable.Columns("PACK_NAME")
                If Me.GridEX1.Find(Me.GridEX1.RootTable.Columns("PACK_NAME"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.GridEX1.GetValue("PACK_NAME"), -1, 1) Then
                    Me.ShowMessageInfo("PACK_NAME Has Existed !." & vbCrLf & "We Suggest PACK_NAME not Equal" _
                     & vbCrLf & "You can delete the row by moving to modified Added DataView" & vbCrLf & "Press Del If you want to delete.")
                    Return
                End If
            ElseIf e.Column.Key = "UNIT" Then
                Select Case Me.GridEX1.GetValue(e.Column.Index)
                    Case "KILOGRAM"
                        Me.GridEX1.SetValue("DEVIDE_FACTOR", 1)
                    Case "GRAM"
                        Me.GridEX1.SetValue("DEVIDE_FACTOR", 1000)
                    Case "TABLET"
                        Me.GridEX1.SetValue("DEVIDE_FACTOR", 1)
                    Case "LITRE"
                        Me.GridEX1.SetValue("DEVIDE_FACTOR", 1)
                    Case "MILILITRE"
                        Me.GridEX1.SetValue("DEVIDE_FACTOR", 1000)
                    Case "BAGS"
                        Me.GridEX1.SetValue("DEVIDE_FACTOR", 1)
                End Select
                Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("MODIFY_BY"), NufarmBussinesRules.User.UserLogin.UserName)
                Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("MODIFY_DATE"), NufarmBussinesRules.SharedClass.ServerDate())
            End If
            If Not Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                Me.GridEX1.UpdateData()
            End If

        Catch ex As Exception
            Me.GridEX1.CancelCurrentEdit()
        Finally
            Me.isLoadingRow = False : Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub Bar1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            If Not IsNothing(Me.clsPack) Then
                Select Case item.Text
                    Case "Added"
                        Me.BindGrid(Me.clsPack.GetDataViewRowState("ModifiedAdded"))
                        'Me.lblLastID.Text = Me.GetLastID()
                        'Me.grpLastID.Visible = False
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                        'Me.FilterEditor1.Visible = False
                        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Case "ModifiedOriginal"
                        Me.BindGrid(Me.clsPack.GetDataViewRowState("ModifiedOriginal"))
                        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        'Me.grpLastID.Visible = False
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Me.GridEX1.RootTable.Columns("PACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                    Case "Deleted"
                        Me.BindGrid(Me.clsPack.GetDataViewRowState("Deleted"))
                        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        ''Me.grpLastID.Visible = False
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                        'Me.FilterEditor1.Visible = False
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        'Me.FilterEditor1.Visible = False
                    Case "Current"
                        Me.BindGrid(Me.clsPack.GetDataViewRowState("Current"))
                        If Me.btnAddNew.Text = "Cancel Changes" Then
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            ''Me.grpLastID.Visible = True
                        Else
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            ''Me.grpLastID.Visible = False
                        End If
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                        ''Me.lblLastID.Text = Me.GetLastID()
                    Case "Unchaigned"
                        Me.BindGrid(Me.clsPack.GetDataViewRowState("Unchaigned"))
                        'Me.grpLastID.Visible = False
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        'Me.FilterEditor1.Visible = False
                        'Case "OriginalRows"
                        '    Me.BindGrid(Me.clsPack.GetDataViewRowState("OriginalRows"))
                        '    'Me.grpLastID.Visible = False
                        '    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        '    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                        '    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        '    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        '    Me.GridEX1.RootTable.Columns("PACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                        'Me.FilterEditor1.Visible = False
                    Case "All Data"
                        'Me.BindGrid(Me.clsPack.GetAllDataViewPack())
                        Me.BindGrid(Me.clsPack.GetDataViewRowState("Current"))
                        If Not CMain.IsSystemAdministrator Then
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        Else
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        End If
                End Select
                Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Pack_Bar1_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub GridEX1_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Not (e.Row.RowType = Janus.Windows.GridEX.RowType.Record) Then
                Return
            End If
            'If Me.clsPack.ViewPack().RowStateFilter = Data.DataViewRowState.OriginalRows _
            'Or Me.clsPack.ViewPack().RowStateFilter = Data.DataViewRowState.CurrentRows _
            'Or Me.clsPack.ViewPack().RowStateFilter = Data.DataViewRowState.Unchanged Then
            '    'check keberadaan data anak di server
            '    'jika ada return dan refect
            '    'Me.MessageCantDeleteData
            '    If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then : Return : End If
            '    If (Me.clsPack.HasReferencedData(Me.GridEX1.GetValue("PACK_ID").ToString())) > 0 Then
            '        Me.ShowMessageInfo(Me.MessageCantDeleteData) : e.Cancel = True : Me.GridEX1.Refetch()
            '        Me.GridEX1.SelectCurrentCellText() : Return
            '    End If
            '    If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
            '        e.Cancel = True : Me.GridEX1.Refetch() : Me.GridEX1.SelectCurrentCellText() : Return
            '    Else
            '        e.Cancel = False
            '        Me.GridEX1.UpdateData()
            '    End If
            'End If
            'If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then : Return : End If
            If (Me.clsPack.HasReferencedData(Me.GridEX1.GetValue("PACK_ID").ToString())) > 0 Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData) : e.Cancel = True : Me.GridEX1.Refetch()
                Me.GridEX1.SelectCurrentCellText() : Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True : Me.GridEX1.Refetch() : Me.GridEX1.SelectCurrentCellText() : Return
            Else
                e.Cancel = False
                Me.GridEX1.UpdateData()
            End If
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub GridEX1_RecordsDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.RecordsDeleted
    '    Me.GridEX1.UpdateData()
    'End Sub
    'Private Sub GridEX1_RecordAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.RecordAdded
    '    Me.clsPack.ViewPack.RowStateFilter = Data.DataViewRowState.CurrentRows
    '    Me.clsPack.ViewPack.Sort = "PACK_ID"
    'End Sub
    Private Sub GridEX1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            If Me.isLoadingRow Then : Return : End If
            If Me.GridEX1.SelectedItems.Count <= 0 Then : Return : End If
            If Me.clsPack.GetAllDataViewPack.RowStateFilter = Data.DataViewRowState.CurrentRows Then
                If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                    Me.GridEX1.RootTable.Columns("PACK_ID").EditType = Janus.Windows.GridEX.EditType.TextBox
                Else
                    Me.GridEX1.RootTable.Columns("PACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Else
                Me.GridEX1.RootTable.Columns("PACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
        Catch ex As Exception

        End Try

    End Sub
#End Region


End Class
