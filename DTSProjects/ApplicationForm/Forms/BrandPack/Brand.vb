Public Class Brand

#Region " Deklarasi "
    Dim clsBrand As NufarmBussinesRules.Brandpack.Brand
    Private IsHasLoad As Boolean = False
    Friend CMain As Main = Nothing
#End Region

#Region " Sub "

    Private Sub GetStateChecked(ByVal item As DevComponents.DotNetBar.ButtonItem)
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar1.Items
            If Not item1.Name = item.Name Then
                item1.Checked = False
            Else
                item = item1
                item.Checked = True
            End If
        Next
    End Sub

    'Private Function GetLastID() As String
    '    Dim LastID As String = ""
    '    LastID = Me.clsBrand.ViewBrand.Item(Me.clsBrand.ViewBrand.Count - 1).Item("BRAND_ID").ToString()
    '    Return LastID
    'End Function

    Private Sub LockedColumCreateModify()
        For Each item As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
            If (item.DataMember = "CREATE_BY") Or (item.DataMember = "CREATE_DATE") _
            Or (item.DataMember = "MODIFY_BY") Or (item.DataMember = "MODIFY_DATE") Then
                item.EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
        Next
    End Sub

    Private Sub LoadData()
        'If IsNothing(Me.clsBrand) Then
        '    Me.clsBrand = New NufarmBussinesRules.Brandpack.Brand()
        'End If
        Me.clsBrand = New NufarmBussinesRules.Brandpack.Brand()
        If Not Me.IsHasLoad Then : Me.clsBrand.CreateAllDataViewBrand(True)
        Else : Me.clsBrand.CreateAllDataViewBrand(False)
        End If
    End Sub

    Private Sub BindGrid(ByVal DtView As DataView)
        'If IsNothing(Me.clsBrand) Then
        '    Me.GridEX1.SetDataBinding(Me.clsBrand.GetDataView, "")
        'Else
        '    Me.LoadData()
        '    Me.GridEX1.SetDataBinding(Me.clsBrand.GetDataView, "")
        'End If
        Me.GridEX1.SetDataBinding(DtView, "")
        Me.GridEX1.RootTable.Columns("BRAND_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("BRAND_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("CREATE_BY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("CREATE_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.GridEX1.RootTable.Columns("MODIFY_BY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.GridEX1.RootTable.Columns("MODIFY_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.Default
        Me.GridEX1.RootTable.Columns("BRAND_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
        'Me.GetStateChecked(Me.btnOriginalRows)
    End Sub

    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.BRAND = True Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If (NufarmBussinesRules.User.Privilege.ALLOW_INSERT.BRAND = True) And (NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.BRAND = True) Then
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_INSERT.BRAND = True Then
                Me.btnAddNew.Visible = True
                Me.grpViewMode.Text = "Data View Mode"
                Me.Bar1.Visible = True
                Me.btnSaveChanges.Enabled = True
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.BRAND = True Then
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

    Private Sub Brand_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor : Me.LoadData() : Me.BindGrid(Me.clsBrand.ViewBrand)
            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True : Me.ReadAcces()
        Catch ex As Exception
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "Brand_Load")
        Finally
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None : Me.IsHasLoad = True : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog()
                Case "btnCustomFilter"
                    Me.FilterEditor1.SourceControl = Me.GridEX1
                    Me.FilterEditor1.Visible = True
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.GridEX1.RemoveFilters()
                Case "btnEqualFilter"
                    Me.FilterEditor1.Visible = False
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.GridEX1.RemoveFilters()
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
                    If Me.clsBrand.GetDataSet.HasChanges() Then
                        If Me.ShowConfirmedMessage("Data has changed !" & vbCrLf & "If you continue refreshing" & vbCrLf & "All Changes will be discarded" & _
                        vbCrLf & "Continue refreshing anyway ?.") = Windows.Forms.DialogResult.No Then
                            'Me.clsBrand.SaveData(Me.clsBrand.GetDataSet().GetChanges())
                            'Me.ShowMessageInfo(Me.MessageSavingSucces)
                            Return
                        End If
                        'Me.grpLastID.Visible = False
                    End If
                    Me.LoadData()
                    Me.BindGrid(Me.clsBrand.ViewBrand)
                    Me.clsBrand.GetDataViewRowState("Current") : Me.GridEX1.Refetch()
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.FilterEditor1.Visible = False
                    Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.GridEX1.RootTable.Columns("BRAND_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                    Me.GridEX1.MoveFirst()
                    Me.GetStateChecked(Me.btnAllData)
                    Me.btnAddNew.Text = "Add New"
                Case "btnAddNew"
                    Select Case item.Text
                        Case "Add New"
                            item.Text = "Cancel Changes"
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.clsBrand.ViewBrand.RowStateFilter = Data.DataViewRowState.CurrentRows
                            Me.GetStateChecked(Me.btnCurrent)
                            Me.BindGrid(Me.clsBrand.ViewBrand)

                            'Me.lblLastID.Text = Me.clsBrand.ViewBrand(Me.clsBrand.ViewBrand.Count - 1)("BRAND_ID").ToString()
                            'Me.grpLastID.Visible = True
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.RootTable.Columns("BRAND_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.MoveToNewRecord()
                        Case "Cancel Changes"
                            If Me.clsBrand.GetDataSet().HasChanges() Then
                                Me.clsBrand.GetDataSet().RejectChanges()
                            End If
                            Me.BindGrid(Me.clsBrand.GetDataViewRowState("Current"))
                            Me.GetStateChecked(Me.btnAllData)
                            item.Text = "Add New"
                            'Me.grpLastID.Visible = False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.FilterEditor1.Visible = False
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.RootTable.Columns("BRAND_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
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
                    'next result
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
            Me.LogMyEvent(ex.Message, Me.Name + "_Brand_Bar2_ItemClick")
        End Try
    End Sub

    Private Sub Bar1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar1.ItemClick
        Try
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            If Not IsNothing(Me.clsBrand) Then
                Select Case item.Text
                    Case "Added"
                        'Me.btnModifiedAdded.Checked = True

                        Me.BindGrid(Me.clsBrand.GetDataViewRowState("ModifiedAdded"))
                        'Me.lblLastID.Text = Me.GetLastID()
                        'Me.grpLastID.Visible = False
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.FilterEditor1.Visible = False
                        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        'Me.GridEX1.RootTable.Columns("BRAND_ID").EditType = Janus.Windows.GridEX.EditType.TextBox
                        'Me.GridEX1.RootTable.Columns("BRAND_NAME").EditType = Janus.Windows.GridEX.EditType.TextBox
                    Case "ModifiedOriginal"
                        Me.BindGrid(Me.clsBrand.GetDataViewRowState("ModifiedOriginal"))
                        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        'Me.grpLastID.Visible = False
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Me.GridEX1.RootTable.Columns("BRAND_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                        Me.FilterEditor1.Visible = False
                    Case "Deleted"
                        Me.BindGrid(Me.clsBrand.GetDataViewRowState("Deleted"))
                        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        'Me.grpLastID.Visible = False
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.FilterEditor1.Visible = False
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Case "Current"
                        Me.BindGrid(Me.clsBrand.GetDataViewRowState("Current"))
                        If Me.btnAddNew.Text = "Cancel Changes" Then
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            'Me.grpLastID.Visible = True
                        Else
                            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            'Me.grpLastID.Visible = False
                        End If
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                        'Me.lblLastID.Text = Me.GetLastID()
                    Case "Unchaigned"
                        Me.BindGrid(Me.clsBrand.GetDataViewRowState("Unchaigned"))
                        'Me.grpLastID.Visible = False
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.FilterEditor1.Visible = False
                    Case "OriginalRows"
                        Me.BindGrid(Me.clsBrand.GetDataViewRowState("OriginalRows"))
                        'Me.grpLastID.Visible = False
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.RootTable.Columns("BRAND_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                        Me.FilterEditor1.Visible = False
                    Case "All Data"
                        Me.BindGrid(Me.clsBrand.GetDataViewRowState("Current"))
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
                Me.LockedColumCreateModify()
                Me.GetStateChecked(CType(sender, DevComponents.DotNetBar.ButtonItem))
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "Brand_Bar1_ItemClick")
        End Try

    End Sub

    Private Sub btnSaveChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveChanges.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsBrand.GetDataSet()) Then
                If Me.clsBrand.GetDataSet.HasChanges() Then
                    If CMain.IsSystemAdministrator Then
                        If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                            Me.clsBrand.SaveData(Me.clsBrand.GetDataSet().GetChanges())
                            Me.ShowMessageInfo(Me.MessageSavingSucces)
                        End If
                        Me.LoadData()
                        Me.BindGrid(Me.clsBrand.ViewBrand)
                        Me.clsBrand.GetDataViewRowState("Current") : Me.GridEX1.Refetch()
                        Me.btnAddNew.Text = "Add New"
                    Else
                        Me.ShowMessageInfo("Sorry, you can not do such operation")
                    End If
                End If
            End If
            'next esult
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.BindGrid(Me.clsBrand.GetDataView())
            Me.btnAddNew.Text = "Add New"
            Me.LogMyEvent(ex.Message, "btnSaveChanges_Click")
        Finally
            Me.Cursor = Cursors.Default
            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
        End Try
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Try
            If CMain.IsSystemAdministrator Then
                If Not IsNothing(Me.clsBrand) Then
                    If Not IsNothing(Me.clsBrand.GetDataSet()) Then
                        If Me.clsBrand.GetDataSet.HasChanges() Then
                            Me.clsBrand.GetDataSet.RejectChanges()
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

    Private Sub GridEX1_CellEdited(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellEdited
        Try
            If Me.GridEX1.Row <= -1 Then
                If e.Column.Key = "BRAND_NAME" Then
                    Me.GridEX1.SearchColumn = Me.GridEX1.RootTable.Columns("BRAND_NAME")
                    If Me.GridEX1.Find(Me.GridEX1.RootTable.Columns("BRAND_NAME"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.GridEX1.GetValue("BRAND_NAME"), -1, 1) Then
                        Me.ShowMessageInfo("BRAND_NAME Has Existed !." & vbCrLf & "We Suggest BRAND_NAME not Equal" _
                         & vbCrLf & "You can delete the row by moving to modified Added DataView" & vbCrLf & "Press Del If you want to delete.")

                        'If Me.ShowConfirmedMessage("BRAND_NAME Has Existed !." & vbCrLf & "We Suggest BRAND_NAME not Equal" & vbCrLf & "Proceed to save into DataView ?.") = Windows.Forms.DialogResult.No Then
                        '    Me.GridEX1.MoveToRowIndex(Me.GridEX1.RecordCount - 1)
                        '    Me.GridEX1.MoveToNewRecord()
                        '    Return
                        'End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_FilterApplied(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.FilterApplied
        Me.Refresh()
        'Me.GridEX1.Refresh()
        'Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
        'Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
    End Sub

    Private Sub GridEX1_ApplyingFilter(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.ApplyingFilter
        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
    End Sub

    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        Try
            If (Me.GridEX1.GetValue("BRAND_ID") Is DBNull.Value) Or (Me.GridEX1.GetValue("BRAND_ID").Equals("")) Then
                Me.ShowMessageInfo("ID Must not be NULL .!")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Me.GridEX1.MoveToNewRecord()
                Return
            End If
            'Me.GridEX1.SearchColumn() = Me.GridEX1.RootTable.Columns("BRAND_ID")
            'If Me.lblLastID.Text = Me.GridEX1.GetValue("BRAND_ID").ToString() Then
            '    Me.ShowMessageInfo("ID has existed in DATAVIEW .!" & vbCrLf & "ID must be Unique")
            '    Me.GridEX1.CancelCurrentEdit()
            '    Me.GridEX1.Refetch()
            '    Me.GridEX1.MoveToNewRecord()
            '    Return
            'End If
            If Me.clsBrand.ViewBrand.Find(Me.GridEX1.GetValue("BRAND_ID")) <> -1 Then
                Me.ShowMessageInfo("ID has existed in DATAVIEW .!" & vbCrLf & "ID must be Unique")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Me.GridEX1.MoveToNewRecord()
                Return
            End If
            'If Me.GridEX1.Find(Me.GridEX1.RootTable.Columns("BRAND_ID"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.GridEX1.GetValue("BRAND_ID"), -1, 1) Then

            'End If
            If (Me.GridEX1.GetValue("BRAND_NAME") Is DBNull.Value) Or (Me.GridEX1.GetValue("BRAND_NAME").Equals("")) Then
                Me.ShowMessageInfo("BRAND_NAME Must not be NULL .!")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Me.GridEX1.MoveToNewRecord()
                Return
            End If
            'Me.clsBrand.ViewBrand.Sort = "BRAND_NAME"

            'If Me.clsBrand.ViewBrand.Find(Me.GridEX1.GetValue("BRAND_NAME")) <> -1 Then

            'End If
            'Me.GridEX1.SearchColumn() = Me.GridEX1.RootTable.Columns("BRAND_NAME")
            Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("CREATE_DATE"), NufarmBussinesRules.SharedClass.ServerDate())
            Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("CREATE_BY"), NufarmBussinesRules.User.UserLogin.UserName)
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub GridEX1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
    '    Try
    '        If Me.GridEX1.Row <= -1 Then
    '            Return
    '        End If
    '        If e.Column.Key = "BRAND_NAME" Then
    '            If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
    '                Me.ShowMessageInfo("BRAND_NAME must not be NULL")
    '                Me.GridEX1.CancelCurrentEdit()
    '                Me.GridEX1.Refetch()
    '                Return
    '            End If
    '        End If
    '        If Me.clsBrand.ViewBrand().RowStateFilter = Data.DataViewRowState.ModifiedOriginal Or Me.clsBrand.ViewBrand().RowStateFilter = Data.DataViewRowState.OriginalRows Then
    '            Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("MODIFY_BY"), NufarmBussinesRules.User.UserLogin.UserName)
    '            Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("MODIFY_DATE"), NufarmBussinesRules.SharedClass.ServerDate())
    '        End If
    '    Catch ex As Exception

    '    End Try

    '    'next result
    'End Sub
    Private Sub Brand_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If Not IsNothing(Me.clsBrand) Then
                If Not IsNothing(Me.clsBrand.GetDataSet()) Then
                    If Me.clsBrand.GetDataSet.HasChanges() Then
                        If CMain.IsSystemAdministrator Then
                            If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                                Me.clsBrand.SaveData(Me.clsBrand.GetDataSet().GetChanges())
                                Me.ShowMessageInfo(Me.MessageSavingSucces)
                            End If
                        End If
                    End If
                End If
                Me.clsBrand.Dispose(True)
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Brand_FormClosed")
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Not (e.Row.RowType = Janus.Windows.GridEX.RowType.Record) Then
                Return
            End If
            If Me.clsBrand.ViewBrand().RowStateFilter = Data.DataViewRowState.OriginalRows _
            Or Me.clsBrand.ViewBrand().RowStateFilter = Data.DataViewRowState.CurrentRows _
            Or Me.clsBrand.ViewBrand().RowStateFilter = Data.DataViewRowState.Unchanged Then
                'check keberadaan data anak di server
                'jika ada return dan refect
                'Me.MessageCantDeleteData
                If Me.GridEX1.Row <= -1 Then
                    Return
                End If
                If (Me.clsBrand.HasReferencedData(Me.GridEX1.GetValue("BRAND_ID").ToString())) > 0 Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    e.Cancel = True
                    Me.GridEX1.Refetch()
                    Me.GridEX1.SelectCurrentCellText()
                End If
                If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True
                    Me.GridEX1.Refetch()
                    Me.GridEX1.SelectCurrentCellText()
                    Return
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_RecordAdded(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.RecordAdded
        Try
            Me.clsBrand.ViewBrand.RowStateFilter = Data.DataViewRowState.CurrentRows
            'Me.lblLastID.Text = Me.GetLastID()
            Me.clsBrand.ViewBrand.Sort = "BRAND_ID"
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridEX1_RecordsDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.RecordsDeleted
        Try
            Me.GridEX1.Refetch()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridEX1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.SelectionChanged
        Try

            If Me.clsBrand.ViewBrand.RowStateFilter = Data.DataViewRowState.CurrentRows Then
                If Me.GridEX1.Row <= -1 Then
                    Me.GridEX1.RootTable.Columns("BRAND_ID").EditType = Janus.Windows.GridEX.EditType.TextBox
                Else
                    Me.GridEX1.RootTable.Columns("BRAND_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
                'ElseIf Me.clsBrand.ViewBrand.RowStateFilter = Data.DataViewRowState.OriginalRows Then
                '    Me.GridEX1.RootTable.Columns("BRAND_NAME").EditType = Janus.Windows.GridEX.EditType.TextBox
                '    Me.GridEX1.RootTable.Columns("BRAND_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Else
                Me.GridEX1.RootTable.Columns("BRAND_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        Try
            If Me.GridEX1.Row <= -1 Then
                Return
            End If
            If e.Column.Key = "BRAND_NAME" Then
                If (Me.GridEX1.GetValue(e.Column) Is DBNull.Value) Or (Me.GridEX1.GetValue(e.Column).Equals("")) Then
                    Me.ShowMessageInfo("BRAND_NAME must not be NULL")
                    Me.GridEX1.CancelCurrentEdit()
                    Me.GridEX1.Refetch()
                    Return
                End If
            End If
            If Me.clsBrand.ViewBrand().RowStateFilter = Data.DataViewRowState.ModifiedOriginal Or Me.clsBrand.ViewBrand().RowStateFilter = Data.DataViewRowState.OriginalRows Then
                Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("MODIFY_BY"), NufarmBussinesRules.User.UserLogin.UserName)
                Me.GridEX1.SetValue(Me.GridEX1.RootTable.Columns("MODIFY_DATE"), NufarmBussinesRules.SharedClass.ServerDate())
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

End Class
