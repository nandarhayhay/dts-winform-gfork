Public Class AreaGON
    Dim clsGonArea As New NufarmBussinesRules.OrderAcceptance.AreaGON()
    Private dS As DataSet = Nothing
    Private isLoadingRow As Boolean = False
    Private RecCOunt As Integer = 0
    Private Sub FillCategoriesValueList()
        Dim ColumnRegID As Janus.Windows.GridEX.GridEXColumn = GridEX1.RootTable.Columns("TRANSPORTATION_BY")
        ColumnRegID.EditType = Janus.Windows.GridEX.EditType.DropDownList
        'Set HasValueList property equal to true in order to be able to use the ValueList property
        ColumnRegID.HasValueList = True
        'Get the ValueList collection associated to this column
        Dim ValueList As Janus.Windows.GridEX.GridEXValueListItemCollection = ColumnRegID.ValueList
        Dim ListWays() As String = {"BY LAND", "BY SEA", "BY AIR", "BY LAND AND SEA", "BY LAND AND AIR", "BY SEA AND AIR", "BY LAND,SEA AND AIR", "UNCATEGORIZED"}
        ValueList.PopulateValueList(ListWays, "TRANSPORTATION_BY")
        ColumnRegID.EditTarget = Janus.Windows.GridEX.EditTarget.Text
        ColumnRegID.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    End Sub
    Private Sub LoadData()
        Me.isLoadingRow = True
        Me.dS = New DataSet("DS_AREA") : Me.dS.Clear()
        Me.dS.Tables.Add(Me.clsGonArea.GetDataTable(True))
        Me.GridEX1.SetDataBinding(dS.Tables(0).DefaultView(), "")
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
        Me.FillCategoriesValueList()
        Me.isLoadingRow = False
    End Sub
    Private Function CreateGONID(ByVal RecCount As Integer, ByRef VrecCount As Integer) As String
        Dim gtRecCount As Integer = RecCount
        Dim num As String = "000"
        gtRecCount += 1

        Dim X As Integer = num.Length - CStr(gtRecCount).Length
        If X <= 0 Then
            num = ""
        Else
            num = num.Remove(X - 1, CStr(gtRecCount).Length)
        End If
        num &= gtRecCount.ToString()
        VrecCount = gtRecCount
        Return num
    End Function
    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        If Me.isLoadingRow Then : Return : End If
        Try

            'bikin dummy dataview
            ''check di dataview
            Dim dummyDataView As DataView = Me.dS.Tables(0).DefaultView().ToTable().Copy().DefaultView()
            'check GON AREA
            If IsNothing(Me.GridEX1.GetValue("GON_AREA")) Or IsDBNull(Me.GridEX1.GetValue("GON_AREA")) Then
                Me.ShowMessageInfo("Please enter AREA") : e.Cancel = True : Me.GridEX1.MoveToNewRecord() : Return
            ElseIf Me.GridEX1.GetValue("GON_AREA").ToString() = String.Empty Then
                Me.ShowMessageInfo("Please enter AREA") : e.Cancel = True : Me.GridEX1.MoveToNewRecord() : Return
            End If
            'DAYS RECEIPT
            If IsNothing(Me.GridEX1.GetValue("DAYS_RECEIPT")) Or IsDBNull(Me.GridEX1.GetValue("DAYS_RECEIPT")) Then
                Me.ShowMessageInfo("Please enter DAYS_RECEIPT") : e.Cancel = True : Me.GridEX1.MoveToNewRecord() : Return
            ElseIf CInt(Me.GridEX1.GetValue("DAYS_RECEIPT")) <= 0 Then
                Me.ShowMessageInfo("Please enter DAYS_RECEIPT") : e.Cancel = True : Me.GridEX1.MoveToNewRecord() : Return
            End If

            If IsNothing(Me.GridEX1.GetValue("TRANSPORTATION_BY")) Or IsDBNull(Me.GridEX1.GetValue("TRANSPORTATION_BY")) Then
                Me.ShowMessageInfo("Please enter TRANSPORTATION_BY") : e.Cancel = True : Me.GridEX1.MoveToNewRecord() : Return
            ElseIf Me.GridEX1.GetValue("TRANSPORTATION_BY").ToString() = String.Empty Then
                Me.ShowMessageInfo("Please enter TRANSPORTATION_BY") : e.Cancel = True : Me.GridEX1.MoveToNewRecord() : Return
            End If
            dummyDataView.Sort = "AREA"
            Dim Area As String = Me.GridEX1.GetValue("GON_AREA").ToString()
            Dim Index As Integer = dummyDataView.Find(Me.GridEX1.GetValue("GON_AREA"))
            If Index <> -1 Then
                Me.ShowMessageInfo("Data has existed") : e.Cancel = True : Me.GridEX1.MoveToNewRecord() : Return
            End If
            If Me.clsGonArea.HasExistedData(Area, Nothing, False) Then
                Me.ShowMessageInfo("Data has existed") : e.Cancel = True : Me.GridEX1.MoveToNewRecord() : Return
            End If
            ''check ke database
            ''create ID
            Dim GonAReaID As String = Me.clsGonArea.CreateAreaGONID(Me.GridEX1.GetValue("GON_AREA").ToString(), Me.RecCOunt, True)
            Dim ExistID As Boolean = True
            dummyDataView.Sort = "GON_ID_AREA"
            Dim VRecCount As Integer = 0
            While ExistID = True

                ''check availablity id di database dan dataview
                Dim BFind As Boolean = Me.clsGonArea.HasExistedData(Area, GonAReaID, False)
                If Not BFind Then
                    Index = dummyDataView.Find(GonAReaID) : BFind = (Index >= 0)
                End If
                If Not BFind Then : Exit While : End If
                GonAReaID = Me.CreateGONID(Me.RecCOunt, VRecCount)
                Me.RecCOunt += 1
            End While

            Me.GridEX1.SetValue("GON_ID_AREA", GonAReaID)
            Me.GridEX1.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName)
            Me.GridEX1.SetValue("CreatedDate", NufarmBussinesRules.SharedClass.ServerDate)
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_AddingRecord")
            Me.ShowMessageError(ex.Message) : e.Cancel = True : Me.GridEX1.MoveToNewRecord()
        End Try
    End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        If IsNothing(Me.GridEX1.DataSource) Then : Return : End If
        If Me.isLoadingRow Then : Return : End If
        Try
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.DataMember = "GON_ID_AREA" Or e.Column.DataMember = "AREA" Or e.Column.DataMember = "DAYS_RECEIPT" Or e.Column.DataMember = "TRANSPORTATIO_BY" Then
                    'CHECK APAKAH data null
                    If IsDBNull(Me.GridEX1.GetValue(e.Column)) Or IsNothing(Me.GridEX1.GetValue(e.Column)) Then
                        Me.ShowMessageInfo("can not set null value") : Me.GridEX1.CancelCurrentEdit()
                    ElseIf Me.GridEX1.GetValue(e.Column).ToString() = "" Then
                        Me.ShowMessageInfo("can not set empty data") : Me.GridEX1.CancelCurrentEdit()
                    End If
                    Me.GridEX1.SetValue("ModifiedBy", NufarmBussinesRules.User.UserLogin.UserName)
                    Me.GridEX1.SetValue("ModifiedDate", NufarmBussinesRules.SharedClass.ServerDate)
                End If
                Me.GridEX1.UpdateData()
            End If
        Catch ex As Exception
            Me.GridEX1.CancelCurrentEdit()
            Me.ShowMessageInfo(ex.Message)
        End Try

    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub GridEX1_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RecordAdded
        Me.GridEX1.UpdateData()
    End Sub

    Private Sub GridEX1_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RecordsDeleted
        Me.GridEX1.UpdateData()
    End Sub

    Private Sub AreaGON_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.isLoadingRow = True
            Me.Cursor = Cursors.WaitCursor
            Me.LoadData()
            Me.FilterEditor1.Visible = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.isLoadingRow = False
            Me.LogMyEvent(ex.Message, Me.Name & "_AreaGON_Load")
            Me.ShowMessageInfo(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub SaveData()
        Me.isLoadingRow = True
        Dim dtTable As New DataTable("GON_AREA") : dtTable.Clear()
        If Not IsNothing(Me.dS.GetChanges()) Then
            Me.clsGonArea.SaveData(Me.dS.GetChanges(), True, dtTable, True)
        Else : Return
        End If
        Me.dS.Clear() : Me.dS = New DataSet("DS_AREA")
        Me.dS.Tables.Add(dtTable)
        Me.dS.AcceptChanges()

        Me.GridEX1.SetDataBinding(dS.Tables(0).DefaultView(), "")

        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
        Me.FillCategoriesValueList()

        If Me.btnAddNew.Checked = True Then
            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
            Me.FilterEditor1.Visible = False
            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
        Else
            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
        End If
        Me.isLoadingRow = False
    End Sub
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnShowFieldChooser"
                    Me.GridEX1.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog()
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.GridEX1
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                        Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    End If
                    'PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                Case "btnPageSettings"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
                Case "btnSave"
                    Me.SaveData()
                Case "btnCustomFilter"
                    Me.FilterEditor1.Visible = True
                    Me.FilterEditor1.SortFieldList = False
                    Me.FilterEditor1.SourceControl = Me.GridEX1
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.GridEX1.RemoveFilters()
                    'Me.GridEX1.Refetch()
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Case "btnAddNew"
                    Me.btnAddNew.Checked = Not Me.btnAddNew.Checked
                    If Me.btnAddNew.Checked = True Then
                        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.btnAddNew.Text = "Cancel"
                    Else
                        If Not IsNothing(Me.dS) Then
                            If Me.dS.HasChanges() Then
                                If MessageBox.Show("Are you sure you want to cancel ?" & vbCrLf & "All curently any unsaved data will be discarded", "Data has changed !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                                    Return
                                End If
                                Me.dS.RejectChanges() : Me.LoadData()
                            End If
                        End If
                        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Me.btnAddNew.Text = "Add New"
                        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                    End If
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
                Case "btnRefresh"
                    If Not IsNothing(Me.dS) Then
                        If Me.dS.HasChanges() Then
                            If MessageBox.Show("Are you sure you want to refresh" & vbCrLf & "All curently any unsaved data will be discarded", "Data has changed !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                                Return
                            End If
                            Me.dS.RejectChanges() : Me.LoadData()
                        End If
                    End If
            End Select
        Catch ex As Exception
            Me.isLoadingRow = False
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
