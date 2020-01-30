Public Class Classification

    Private tBrandClass As DataTable = Nothing
    Private _clsBrandClass As NufarmBussinesRules.Brandpack.Brand = Nothing
    Friend CMain As Main = Nothing
    Private ReadOnly Property ClssBrandClass() As NufarmBussinesRules.Brandpack.Brand
        Get
            If _clsBrandClass Is Nothing Then
                _clsBrandClass = New NufarmBussinesRules.Brandpack.Brand()
            End If
            Return _clsBrandClass
        End Get
    End Property
    Private IsLoadingRow As Boolean = False
    Private Function HasChangedData() As Boolean
        If IsNothing(tBrandClass) Then : Return False : End If
        Dim InsertedRows() As DataRow = tBrandClass.Select("", "", Data.DataViewRowState.Added)
        Dim UpdatedRows() As DataRow = tBrandClass.Select("", "", Data.DataViewRowState.ModifiedOriginal)
        Dim DeletedRows() As DataRow = tBrandClass.Select("", "", Data.DataViewRowState.Deleted)
        If Not IsNothing(InsertedRows) Then
            If InsertedRows.Length > 0 Then
                Return True
            End If
        End If
        If Not IsNothing(UpdatedRows) Then
            If UpdatedRows.Length > 0 Then : Return True : End If
        End If
        If Not IsNothing(DeletedRows) Then
            If DeletedRows.Length > 0 Then : Return True : End If
        End If
        Return False
    End Function
    Private Sub LoadData(ByVal mustCloseConnection As Boolean)
        If Not Me.IsLoadingRow Then : IsLoadingRow = True : End If

        Me.tBrandClass = ClssBrandClass.getClass(mustCloseConnection)
        Me.GridEX1.SetDataBinding(tBrandClass.DefaultView, "")
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
    End Sub

    Private Sub Classification_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            LoadData(True)
            Cursor = Cursors.Default
            IsLoadingRow = False
        Catch ex As Exception
            Cursor = Cursors.Default
            IsLoadingRow = False
            Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_ProductClass_Load")
        End Try
        ReadAccess()

        CMain.FormLoading = Main.StatusForm.HasLoaded
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Me.HasChangedData() Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor

            Me.ClssBrandClass.SaveChangesClass(Me.tBrandClass, False)
            Me.LoadData(True)
            Me.IsLoadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            IsLoadingRow = False
            Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnSave_Click")
        End Try
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        If Not IsNothing(Me.tBrandClass) Then
            If HasChangedData() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        IsLoadingRow = True
                        ClssBrandClass.SaveChangesClass(Me.tBrandClass, True)
                        Me.Close()

                    Catch ex As Exception
                        Me.ShowMessageError(ex.Message)
                        Me.Cursor = Cursors.Default
                    End Try

                End If
            End If
        End If
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Classification Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            Me.btnAddNew.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Classification
            If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Classification Then
                Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            Me.btnSave.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.Classification Or NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.Classification _
            Or NufarmBussinesRules.User.Privilege.ALLOW_DELETE.Classification

        End If
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
        Me.GridEX1.MoveToNewRecord()
    End Sub

    Private Sub Classification_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If IsLoadingRow Then : Return : End If
        If Not IsNothing(Me.tBrandClass) Then
            If HasChangedData() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        ClssBrandClass.SaveChangesClass(Me.tBrandClass, True)
                    Catch ex As Exception

                    End Try

                End If
            End If
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        Try
            'check di database
            Me.Cursor = Cursors.WaitCursor
            'check classID
            If (IsNothing(Me.GridEX1.GetValue("CLASS_ID")) Or (Me.GridEX1.GetValue("CLASS_ID") Is DBNull.Value)) Then
                Me.ShowMessageInfo("Please enter Class_ID")
                e.Cancel = True : Return
            ElseIf String.IsNullOrEmpty(Me.GridEX1.GetValue("CLASS_ID").ToString()) Then
                e.Cancel = True : Return
            End If
            ''CLASS NAME
            If (IsNothing(Me.GridEX1.GetValue("CLASS_NAME")) Or (Me.GridEX1.GetValue("CLASS_NAME") Is DBNull.Value)) Then
                Me.ShowMessageInfo("Please enter Class_ID")
                e.Cancel = True : Return
            ElseIf String.IsNullOrEmpty(Me.GridEX1.GetValue("CLASS_NAME").ToString()) Then
                e.Cancel = True : Return
            End If

            If Me.ClssBrandClass.hasExistsClassID(Me.GridEX1.GetValue("CLASS_ID").ToString(), True) Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted) : Return
            End If
            'SET INACTIV
            IsLoadingRow = True
            Me.GridEX1.SetValue("INACTIVE", False)
            Me.GridEX1.SetValue("CREATE_BY", NufarmBussinesRules.User.UserLogin.UserName)
            Me.GridEX1.SetValue("CREATE_DATE", NufarmBussinesRules.SharedClass.ServerDate())
            IsLoadingRow = False
            Cursor = Cursors.Default
        Catch ex As Exception
            e.Cancel = True
            Cursor = Cursors.Default
            IsLoadingRow = False
            Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_AddingRecord")
        End Try
    End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        Try
            If IsLoadingRow Then : Return : End If
            If e.Column.Key = "CLASS_ID" Then
                If Me.GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
                    Return
                Else
                    Me.GridEX1.CancelCurrentEdit()
                End If
            End If
        Catch ex As Exception
            GridEX1.CancelCurrentEdit()
            Cursor = Cursors.Default
            IsLoadingRow = False
            Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_CellUpdated")
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        ''check referenced data
        Try
            Dim ClassID As String = Me.GridEX1.GetValue("CLASS_ID").ToString()
            If Me.ClssBrandClass.hasReferencedClassID(ClassID, True) Then
                e.Cancel = True
                Me.ShowMessageInfo(Me.MessageCantDeleteData) : Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True : Return
            End If
            e.Cancel = False
        Catch ex As Exception
            e.Cancel = True
            Cursor = Cursors.Default
            IsLoadingRow = False
            Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        End Try
    End Sub

    Private Sub GridEX1_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RecordsDeleted
        Me.GridEX1.UpdateData()
    End Sub

    Private Sub GridEX1_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RecordAdded
        Me.IsLoadingRow = False
        Me.GridEX1.UpdateData()
    End Sub

    Private Sub GridEX1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        If e.KeyCode = Keys.F7 Then
            Try
                Dim FE As New Janus.Windows.GridEX.Export.GridEXExporter()
                Me.Cursor = Cursors.WaitCursor
                FE.IncludeChildTables = False
                FE.IncludeHeaders = True
                FE.SheetName = "Product_Classified"
                FE.IncludeFormatStyle = False
                FE.IncludeExcelProcessingInstruction = True
                FE.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                Dim SD As New SaveFileDialog()
                SD.OverwritePrompt = True
                SD.DefaultExt = ".xls"
                SD.Filter = "All Files|*.*"
                SD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                If SD.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    Using FS As New System.IO.FileStream(SD.FileName, IO.FileMode.Create)
                        FE.GridEX = Me.GridEX1
                        FE.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & SD.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                End If
                Me.Cursor = Cursors.Default
            Catch ex As Exception

            End Try

        End If
    End Sub
End Class