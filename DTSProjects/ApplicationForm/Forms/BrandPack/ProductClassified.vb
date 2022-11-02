Public Class ProductClassified
    Private tClass As DataTable = Nothing
    Private TBrandClass As DataTable = Nothing
    Private TBrand As DataTable = Nothing
    Friend CMain As Main = Nothing
    Private _clsBrandClass As NufarmBussinesRules.Brandpack.ProductClassified
    Private ReadOnly Property ClsBrandClass() As NufarmBussinesRules.Brandpack.ProductClassified
        Get
            If IsNothing(_clsBrandClass) Then
                _clsBrandClass = New NufarmBussinesRules.Brandpack.ProductClassified()
            End If
            Return _clsBrandClass
        End Get
    End Property
    Private Sub AddConditionalFormating()
        Dim fc As New Janus.Windows.GridEX.GridEXFormatCondition(Me.GridEX1.RootTable.Columns("INACTIVE"), Janus.Windows.GridEX.ConditionOperator.Equal, True)
        fc.FormatStyle.ForeColor = Color.Red
        fc.AllowMerge = True
        Me.GridEX1.RootTable.FormatConditions.Add(fc)
    End Sub
    Private IsLoadingRow As Boolean = False
    Private Function HasChangedData() As Boolean
        If IsNothing(TBrandClass) Then : Return False : End If
        Dim InsertedRows() As DataRow = TBrandClass.Select("", "", Data.DataViewRowState.Added)
        Dim UpdatedRows() As DataRow = TBrandClass.Select("", "", Data.DataViewRowState.ModifiedOriginal)
        Dim DeletedRows() As DataRow = TBrandClass.Select("", "", Data.DataViewRowState.Deleted)
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
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.ProductClassified Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            Me.btnAddNew.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.ProductClassified
            If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.ProductClassified Then
                Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            End If
        End If
    End Sub
    Private Sub LoadData()
        If Not Me.IsLoadingRow Then : Me.IsLoadingRow = True : End If
        Me.TBrandClass = Me.ClsBrandClass.getBrandClass(False)
        Me.TBrand = Me.ClsBrandClass.getBrand(False)
        Me.tClass = Me.ClsBrandClass.getClass(True)
        Me.GridEX1.SetDataBinding(TBrandClass.DefaultView, "")
        Me.GridEX1.DropDowns("BRAND").SetDataBinding(TBrand.DefaultView, "")
        Me.GridEX1.DropDowns("CLASS").SetDataBinding(tClass.DefaultView, "")
        AddConditionalFormating()
    End Sub
    Private Sub ProductClassified_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'GET DATA PRODUCT CLASS
            Cursor = Cursors.WaitCursor
            Me.IsLoadingRow = True
            Me.LoadData()
            IsLoadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            IsLoadingRow = False
            Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_ProductClassified_Load")
        End Try
        ReadAccess()
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
        Me.GridEX1.MoveToNewRecord()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.HasChangedData() Then
            Me.Cursor = Cursors.WaitCursor
            Try
                Me.ClsBrandClass.SaveBrandClass(Me.TBrandClass, False)
                Me.LoadData()
                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                IsLoadingRow = False
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                IsLoadingRow = False
                Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnSave_Click")
            End Try
        End If
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        If Me.HasChangedData() Then
            If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                Try
                    Cursor = Cursors.WaitCursor
                    Me.IsLoadingRow = True
                    Me.ClsBrandClass.SaveBrandClass(Me.TBrandClass, True)
                    Me.Close()
                    Me.Cursor = Cursors.Default
                Catch ex As Exception
                    Me.ShowMessageError(ex.Message)
                    Me.Cursor = Cursors.Default
                End Try
            End If
        End If
    End Sub

    Private Sub ProductClassified_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.IsLoadingRow Then : Return : End If
        If Me.HasChangedData() Then
            If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                Try
                    Cursor = Cursors.WaitCursor
                    Me.ClsBrandClass.SaveBrandClass(Me.TBrandClass, True)
                    Me.Cursor = Cursors.Default
                Catch ex As Exception
                    Me.ShowMessageError(ex.Message)
                    Me.Cursor = Cursors.Default
                End Try
            End If
        End If
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
                GridEX1.RootTable.Columns(0).Visible = True
                GridEX1.RootTable.Columns("1BRAND_ID").Visible = True
                GridEX1.RootTable.Columns("1CLASS_ID").Visible = True
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
                GridEX1.RootTable.Columns(0).Visible = False
                GridEX1.RootTable.Columns("1BRAND_ID").Visible = False
                GridEX1.RootTable.Columns("1CLASS_ID").Visible = False
                Me.Cursor = Cursors.Default
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        Try
            'check class
            Dim ClassID As Object = Me.GridEX1.GetValue("CLASS_ID")
            If (IsNothing(ClassID) Or IsDBNull(ClassID)) Then
                Me.ShowMessageInfo("Please define class ID") : e.Cancel = True : Return
            End If
            Dim BrandID As Object = Me.GridEX1.GetValue("BRAND_ID")
            If (IsNothing(BrandID) Or IsDBNull(BrandID)) Then
                Me.ShowMessageInfo("Please define Brand ID") : e.Cancel = True : Return
            End If
            'check di dataview
            Cursor = Cursors.WaitCursor
            Dim DV As DataView = CType(Me.GridEX1.DataSource, DataView).ToTable().Copy().DefaultView
            DV.Sort = "BRAND_CLASS_ID"
            Dim brandClassID As String = BrandID + "|" + ClassID
            Dim Index As Integer = DV.Find(brandClassID)
            If Index <> -1 Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted) : e.Cancel = True : Cursor = Cursors.Default : Return
            End If
            'check di database
            If Me.ClsBrandClass.HasExistsBrandClassID(brandClassID, True) Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted) : e.Cancel = True : Cursor = Cursors.Default : Return
            End If
            IsLoadingRow = True
            Me.GridEX1.SetValue("BRAND_CLASS_ID", brandClassID)
            Me.GridEX1.SetValue("CLASS_NAME", Me.GridEX1.DropDowns("CLASS").GetValue("CLASS_NAME"))
            Me.GridEX1.SetValue("INACTIVE", False)
            Me.GridEX1.SetValue("CREATE_BY", NufarmBussinesRules.User.UserLogin.UserName)
            Me.GridEX1.SetValue("CREATE_DATE", NufarmBussinesRules.SharedClass.ServerDate)
            Me.Cursor = Cursors.Default
            IsLoadingRow = False

        Catch ex As Exception
            IsLoadingRow = False
            Me.ShowMessageError(ex.Message)
            e.Cancel = True
        End Try
    End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        If IsLoadingRow Then : Return : End If
        If (e.Column.Key = "CLASS_ID") Or (e.Column.Key = "BRAND_ID") Then
            If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.GridEX1.CancelCurrentEdit()
            End If
            Return
        End If
    End Sub

    Private Sub GridEX1_DeletingRecords(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.DeletingRecords

        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
            e.Cancel = True : Return
        End If
        e.Cancel = False
    End Sub

    Private Sub GridEX1_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RecordsDeleted
        Me.GridEX1.UpdateData()
    End Sub

    Private Sub GridEX1_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RecordAdded
        Me.IsLoadingRow = False
        Me.GridEX1.UpdateData()
    End Sub
End Class