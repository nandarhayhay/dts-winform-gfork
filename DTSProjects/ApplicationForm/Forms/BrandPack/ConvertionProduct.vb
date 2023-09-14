Imports System
Imports System.Configuration
Imports System.Data
Public Class ConvertionProduct
    Private dtConv As DataTable = Nothing
    Private dtBrandPack As DataTable = Nothing
    Private m_clsBrandPack As NufarmBussinesRules.Brandpack.BrandPack = Nothing
    Private IsLoadingRow As Boolean = False
    Friend CMain As Main = Nothing
    Private IsHOUser As Boolean = NufarmBussinesRules.SharedClass.IsUserHO
    Private HasLoadForm As Boolean = False
    Private ReadOnly Property clsBrandPack() As NufarmBussinesRules.Brandpack.BrandPack
        Get
            If IsNothing(Me.m_clsBrandPack) Then
                Me.m_clsBrandPack = New NufarmBussinesRules.Brandpack.BrandPack()
            End If
            Return Me.m_clsBrandPack
        End Get
    End Property
    Private Function HasChangedData() As Boolean
        Dim ChangedData As DataTable = Me.dtConv.GetChanges()
        If Not IsNothing(ChangedData) Then
            If ChangedData.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function
    Friend Sub InitializeData()
        Me.dtBrandPack = Me.clsBrandPack.getActiveBrandPack(False)
        Me.dtConv = Me.clsBrandPack.getProdConvertion(True)
    End Sub
    Private Function saveChanges() As Boolean
        Return Me.clsBrandPack.SaveBrandPackConvertion(Me.dtConv)
        Me.GridEX1.SetDataBinding(Me.dtConv, "")
        Me.ShowMessageInfo(Me.MessageSavingSucces)
    End Function

    Private Sub ProductConvertion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.IsLoadingRow = True
            Me.GridEX1.SetDataBinding(Me.dtConv, "")
            Me.GridEX1.DropDowns(0).SetDataBinding(Me.dtBrandPack.DefaultView, "")
            Me.GridEX1.DropDowns(0).AutoSizeColumns()
            Me.ReadAccess()
            Me.IsLoadingRow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.GridEX1.Enabled = False
            Me.btnSaveChanges.Enabled = False
            Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If IsHOUser Then
                If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.ConvertionProduct Then
                    Me.btnAddNew.Enabled = True
                Else
                    Me.btnAddNew.Enabled = False
                End If
                If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.ConvertionProduct Then
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                Else
                    Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                End If
                If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.ConvertionProduct Then
                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                Else
                    Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                End If
            Else
                Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                Me.btnSaveChanges.Enabled = False
            End If
        Else
            Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
        End If
    End Sub
    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        Try
            'check UNIT1,VOL1,UNIT2,VOL2
            Me.Cursor = Cursors.WaitCursor
            Dim unit1 As Object = Me.GridEX1.GetValue("UNIT1"), unit2 As Object = Me.GridEX1.GetValue("UNIT2")
            Dim UnitOfMeasure As Object = Me.GridEX1.GetValue("UnitOfMeasure")
            Dim Vol1 As Object = Me.GridEX1.GetValue("VOL1"), Vol2 As Object = Me.GridEX1.GetValue("VOL2")
            Dim BrandPackID As Object = Me.GridEX1.GetValue("BRANDPACK_ID")
            If Not IsNothing(UnitOfMeasure) And Not IsDBNull(UnitOfMeasure) Then
                If CStr(UnitOfMeasure) = "" Then
                    Me.ShowMessageError("Please enter UnitOfMeasure(UOM)")
                    e.Cancel = True
                    Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
                End If
            Else
                Me.ShowMessageError("Please enter enter UnitOfMeasure(UOM)")
                e.Cancel = True
                Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
            End If
            If Not IsNothing(unit1) And Not IsDBNull(unit1) Then
                If CStr(unit1) = "" Then
                    Me.ShowMessageError("Please enter Unit 1")
                    e.Cancel = True
                    Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
                End If
            Else
                Me.ShowMessageError("Please enter Unit 1")
                e.Cancel = True
                Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
            End If

            If Not IsNothing(unit2) And Not IsDBNull(unit2) Then
                If CStr(unit2) = "" Then
                    Me.ShowMessageError("Please enter Unit 2")
                    e.Cancel = True
                    Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
                End If
            Else
                Me.ShowMessageError("Please enter Unit 2")
                e.Cancel = True
                Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
            End If

            If Not IsNothing(Vol1) And Not IsDBNull(Vol1) Then
                If CStr(Vol1) = "" Then
                    Me.ShowMessageError("Please enter volume 1")
                    e.Cancel = True
                    Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
                End If
            Else
                Me.ShowMessageError("Please enter volume 1")
                e.Cancel = True
                Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
            End If

            If Not IsNothing(Vol2) And Not IsDBNull(Vol2) Then
                If CStr(Vol2) = "" Then
                    Me.ShowMessageError("Please enter volume 2")
                    e.Cancel = True
                    Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
                End If
            Else
                Me.ShowMessageError("Please enter volume 2")
                e.Cancel = True
                Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
            End If
            If Not IsNothing(BrandPackID) And Not IsDBNull(BrandPackID) Then
                If CStr(BrandPackID) = "" Then
                    Me.ShowMessageError("Please enter BrandPack Name")
                    e.Cancel = True
                    Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
                End If
            Else
                Me.ShowMessageError("Please enter BrandPack Name")
                e.Cancel = True
                Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
            End If
            ''check existing data
            If Me.clsBrandPack.HasExistedProdCon(BrandPackID) Then
                Me.ShowMessageError(Me.MessageDataHasExisted)
                e.Cancel = True
                Me.GridEX1.MoveToNewRecord() : Me.GridEX1.Focus()
            End If
            Me.GridEX1.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName)
            Me.GridEX1.SetValue("CreatedDate", NufarmBussinesRules.SharedClass.ServerDate())

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        If Me.IsLoadingRow Then : Return : End If

        Try
            'check di server GON table dan GON Separated
            If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim BrandPackID As String = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
                If e.Column.Key = "INACTIVE" Then
                    Dim oINActive As Boolean = Me.GridEX1.GetValue("INACTIVE")
                    Dim INActive As Boolean = False
                    If Not IsNothing(oINActive) And Not IsDBNull(oINActive) Then
                        INActive = CBool(oINActive)
                    End If
                    If Not INActive Then
                        If Me.clsBrandPack.ProdConvHasRef(BrandPackID, Convert.ToDateTime(Me.GridEX1.GetValue("CreatedDate"))) Then
                            Me.ShowMessageError(Me.MessageDataCantChanged & vbCrLf & "Data has been used in GON OR SPPB")
                            Me.GridEX1.CancelCurrentEdit()
                            Return
                        End If
                    End If
                End If
                GridEX1.UpdateData()
            End If
        Catch ex As Exception
            Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_CellUpdated")
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            Dim BrandPackID As String = Me.GridEX1.GetValue("BRANDPACK_ID").ToString()
            If CType(e.Row.Cells("INACTIVE").Value, Boolean) = False Then
                If Me.clsBrandPack.ProdConvHasRef(BrandPackID, Convert.ToDateTime(Me.GridEX1.GetValue("CreatedDate"))) Then
                    Me.ShowMessageError(Me.MessageCantDeleteData & vbCrLf & "Data has been used in GON non PO distributor")
                    e.Cancel = True
                    Return
                End If
            Else
                Me.ShowMessageError(Me.MessageDataCantChanged & vbCrLf & "Data has been inactive")
                e.Cancel = True
                Return
            End If
        Catch ex As Exception
            Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_DeletingRecord")
        End Try
    End Sub

    Private Sub GridEX1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        If e.KeyCode = Keys.F7 Then
            Try
                Dim FE As New Janus.Windows.GridEX.Export.GridEXExporter()
                Me.Cursor = Cursors.WaitCursor
                FE.IncludeHeaders = True
                FE.SheetName = "Product_Convertion"
                FE.IncludeFormatStyle = False
                FE.IncludeExcelProcessingInstruction = True
                FE.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
                Dim SD As New SaveFileDialog()
                SD.OverwritePrompt = True
                SD.DefaultExt = ".xls"
                SD.Filter = "All Files|*.*"
                SD.RestoreDirectory = True
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
                Cursor = Cursors.Default
                Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_KeyDown")
            End Try
        ElseIf e.KeyCode = Keys.Delete Then
            Me.IsLoadingRow = True
            GridEX1.CancelCurrentEdit()
            Me.GridEX1.Delete()
            Me.IsLoadingRow = False
        End If
    End Sub

    Private Sub GridEX1_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RecordAdded
        Me.GridEX1.UpdateData()
    End Sub

    Private Sub ProductConvertion_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.IsLoadingRow Then : Return : End If
        If Me.HasChangedData() Then
            If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                Try
                    Cursor = Cursors.WaitCursor
                    Me.saveChanges()
                    Me.Cursor = Cursors.Default
                Catch ex As Exception
                    Cursor = Cursors.Default
                    Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_ProductConvertion_FormClosing")
                End Try
            End If
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If Me.HasChangedData Then
            If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                Try
                    Cursor = Cursors.WaitCursor
                    Me.saveChanges()
                    Me.IsLoadingRow = True
                    Me.Close()
                    Me.Cursor = Cursors.Default
                Catch ex As Exception
                    Cursor = Cursors.Default
                    Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnClose_Click")
                End Try
            End If
        Else
            Me.IsLoadingRow = True
            Me.Close()
        End If
    End Sub

    Private Sub btnSaveChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveChanges.Click
        Try
            Cursor = Cursors.WaitCursor
            If Me.HasChangedData Then
                Me.saveChanges()
            End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnSaveChanges_Click")
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        InitializeData()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.ConvertionProduct Then
            Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
            Me.GridEX1.MoveToNewRecord()
            Me.GridEX1.Focus()
        Else
            Me.ShowMessageInfo("Sorry you have no priviledge to do such operation")
        End If
    End Sub

    Private Sub FilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilterToolStripMenuItem.Click
        'me.GridEX1.fil
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.RemoveFilters()
    End Sub

    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        Try
            Dim FE As New Janus.Windows.GridEX.Export.GridEXExporter()
            Me.Cursor = Cursors.WaitCursor
            FE.IncludeHeaders = True
            FE.SheetName = "Product_Convertion"
            FE.IncludeFormatStyle = False
            FE.IncludeExcelProcessingInstruction = True
            FE.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows
            Dim SD As New SaveFileDialog()
            SD.OverwritePrompt = True
            SD.DefaultExt = ".xls"
            SD.Filter = "All Files|*.*"
            SD.RestoreDirectory = True
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
            Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_ExportToolStripMenuItem_Click")
        End Try
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Cursor = Cursors.Default
        If Me.HasChangedData Then
            If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                Try
                    Cursor = Cursors.WaitCursor
                    Me.saveChanges()
                    Me.IsLoadingRow = True
                    Me.Close()
                Catch ex As Exception
                    Cursor = Cursors.Default
                    Me.ShowMessageError(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnClose_Click")
                End Try
            End If
        Else
            Me.dtConv.RejectChanges()
            Me.InitializeData()
            Me.GridEX1.SetDataBinding(Me.dtConv, "")
            Me.GridEX1.DropDowns(0).SetDataBinding(Me.dtBrandPack.DefaultView, "")
            Me.GridEX1.DropDowns(0).AutoSizeColumns()
        End If
    End Sub

    'Private Sub GridEX1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
    '    If Me.IsLoadingRow Then : Return : End If
    '    If Not Me.HasLoadForm Then : Return : End If

    '    If Me.GridEX1.GetRow().RowType <> Janus.Windows.GridEX.RowType.Record Then
    '        If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.NewRecord Then

    '        End If
    '    Else
    '        Dim oINActive As Boolean = Me.GridEX1.GetValue("INACTIVE")
    '        Dim INActive As Boolean = False
    '        If Not IsNothing(oINActive) And Not IsDBNull(oINActive) Then
    '            INActive = CBool(oINActive)
    '        End If

    '    End If
    'End Sub
End Class
