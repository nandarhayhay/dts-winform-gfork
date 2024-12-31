Imports System
Imports System.Threading
Imports Nufarm.Domain
Imports DTSProjects.GonNonPODist
Imports System.Configuration
Imports System.Globalization
Imports NufarmBussinesRules.common.Helper

Public Class OtherProduct
    Private PageIndex As Int32 = 0, PageSize As Integer = 0, TotalIndex As Integer = 0, RowCount As Integer = 0
    Private m_DataType As NufarmBussinesRules.common.Helper.DataTypes = NufarmBussinesRules.common.Helper.DataTypes.VarChar
    Private m_Criteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private OriginalCriteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private isUndoingCriteria As Boolean = False : Friend CMain As Main = Nothing
    Private isLoadingRow As Boolean = False
    Private IsSavingData As Boolean = False
    Private IsHOUser As Boolean = NufarmBussinesRules.SharedClass.IsUserHO
    Private IsSystemAdmin As Boolean = False
    Private OriginalData As New NufarmBussinesRules.common.dataTManager()
    Private hasLoadGrid As Boolean = False
    Private hasLoadForm As Boolean = False
    Private SFG As StateFillingGrid
    Private m_clsOther As NufarmBussinesRules.Brandpack.OtherProduct = Nothing
    Private pnlEntryOpen As System.Drawing.Size = New System.Drawing.Size(844, 213)
    Private NDomOtherProd As Nufarm.Domain.OtherProduct
    Private ODomOtherProd As Nufarm.Domain.OtherProduct
    Private Mode As SaveMode = SaveMode.None
    Private ReadOnly Property clsOther() As NufarmBussinesRules.Brandpack.OtherProduct
        Get
            If m_clsOther Is Nothing Then
                m_clsOther = New NufarmBussinesRules.Brandpack.OtherProduct()
            End If
            Return m_clsOther
        End Get
    End Property
    Private Enum StateFillingMCB
        Filling
        HasFilled
    End Enum
    Private Enum StateFillingGrid
        HasFilled
        Filling
    End Enum
    Private Sub SetOriginalCriteria()
        Select Case Me.AdvancedTManager1.btnCriteria.CompareOperator
            Case CompareOperator.BeginWith
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
            Case CompareOperator.EndWith
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
            Case CompareOperator.Equal
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
            Case CompareOperator.Greater
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
            Case CompareOperator.GreaterOrEqual
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
            Case CompareOperator.In
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.In
            Case CompareOperator.Less
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Less
            Case CompareOperator.LessOrEqual
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
            Case CompareOperator.Like
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
            Case CompareOperator.NotEqual
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
            Case CompareOperator.Between
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
        End Select
    End Sub
    Private Sub UndoCriteria()
        Me.isUndoingCriteria = True
        With Me.AdvancedTManager1
            Select Case Me.OriginalCriteria
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
                    .btnCriteria.Text = "|*"
                    .btnCriteria.CompareOperator = CompareOperator.BeginWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                    .btnCriteria.Text = "*|"
                    .btnCriteria.CompareOperator = CompareOperator.EndWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                    .btnCriteria.Text = "="
                    .btnCriteria.CompareOperator = CompareOperator.Equal
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                    .btnCriteria.Text = ">"
                    .btnCriteria.CompareOperator = CompareOperator.Greater
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                    .btnCriteria.Text = ">="
                    .btnCriteria.CompareOperator = CompareOperator.GreaterOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.In
                    .btnCriteria.Text = "*|*"
                    .btnCriteria.CompareOperator = CompareOperator.In
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.In
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                    .btnCriteria.Text = "<"
                    .btnCriteria.CompareOperator = CompareOperator.Less
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                    .btnCriteria.Text = "<="
                    .btnCriteria.CompareOperator = CompareOperator.LessOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                    .btnCriteria.Text = "*.*"
                    .btnCriteria.CompareOperator = CompareOperator.Like
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
                    .btnCriteria.Text = "<>"
                    .btnCriteria.CompareOperator = CompareOperator.NotEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
                    .btnCriteria.Text = "<>"
                    .btnCriteria.CompareOperator = CompareOperator.Between
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
            End Select
        End With
        Me.isUndoingCriteria = False
    End Sub
    Private Sub FormatDataGrid()
        For Each col As Janus.Windows.GridEX.GridEXColumn In AdvancedTManager1.GridEX1.RootTable.Columns
            If col.Type Is Type.GetType("System.Int32") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                If col.Key.Contains("IDApp") Then
                    col.Visible = False : col.FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                    col.ShowInFieldChooser = False
                End If
            ElseIf col.Type Is Type.GetType("System.Decimal") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FormatString = "#,##0.000"
            ElseIf col.Key.Contains("IDApp") Or col.Key = "FkCode" Then
                col.Visible = False : col.FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                col.ShowInFieldChooser = False
            End If
            If col.Type Is Type.GetType("System.Boolean") Then
                col.EditType = Janus.Windows.GridEX.EditType.CheckBox
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
            End If
            If col.Key = "ROW_NUM" Then : col.Caption = "NO." : col.ShowInFieldChooser = False : End If
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            col.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
            If col.DataMember = "InActive" Then
                col.EditType = Janus.Windows.GridEX.EditType.CheckBox
            Else
                col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
        Next
        Me.AdvancedTManager1.GridEX1.AutoSizeColumns()
    End Sub
    Private Sub GetStateChecked(ByVal item As DevComponents.DotNetBar.ButtonItem)
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar2.Items
            If Not item1.Name = item.Name Then
                item1.Checked = False
            Else
                item = item1
                item.Checked = True
            End If
        Next
    End Sub
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = DirectCast(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnRenameColumn"
                    Dim MC As New ManipulateColumn()
                    MC.ShowInTaskbar = False
                    MC.grid = Me.AdvancedTManager1.GridEX1
                    MC.FillcomboColumn()
                    MC.ManipulateColumnName = "Rename"
                    MC.TopMost = True
                    MC.Show(Me.Bar2)
                Case "btnShowFieldChooser"
                    Me.AdvancedTManager1.GridEX1.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = AdvancedTManager1.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog()
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.AdvancedTManager1.GridEX1
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                        Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                    End If
                    If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Me.PrintPreviewDialog1.Document.Print()
                    End If
                Case "btnPageSettings"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
                Case "btnCustomFilter"
                    Me.FilterEditor1.Visible = True
                    Me.FilterEditor1.SortFieldList = False
                    Me.FilterEditor1.SourceControl = Me.AdvancedTManager1.GridEX1
                    Me.AdvancedTManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.AdvancedTManager1.GridEX1.RemoveFilters()
                    Me.GetStateChecked(btnCustomFilter)
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.AdvancedTManager1.GridEX1.RemoveFilters()
                    Me.AdvancedTManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.GetStateChecked(Me.btnFilterEqual)
                Case "btnAddNew"
                    Me.AddNewData()
                    'If Me.MustReload Then
                    '    Me.ReloadOpener()
                    'End If
                Case "btnSave"
                    If Not Me.ValidData() Then : Me.Cursor = Cursors.Default : Return : End If
                    Me.NDomOtherProd = New NuFarm.Domain.OtherProduct()
                    With Me.NDomOtherProd
                        If Me.Mode = SaveMode.Update Then
                            .IDApp = Me.ODomOtherProd.IDApp
                        End If
                        .ItemName = Me.txtItemName.Text.Trim()
                        .Unit1 = Me.txtUnit1.Text.Trim()
                        .Unit2 = Me.txtUnit2.Text.Trim()
                        .Vol1 = CDec(Me.txtVol1.Value)
                        .Vol2 = CDec(Me.txtVol2.Value)
                        .UOM = Me.txtUOM.Text.Trim()
                        .Remark = Me.txtRemark.Text.Trim()
                        .CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
                        .ModifiedDate = NufarmBussinesRules.User.UserLogin.UserName
                        .Dev_Qty = Me.txtDevQTy.Value
                    End With
                    If Not Me.HasChangedData() Then
                        Me.Cursor = Cursors.Default : Return
                    End If
                    Me.Cursor = Cursors.WaitCursor
                    Me.SavedData(False)
                    Me.GetData()
                    Me.enabledEntry(False)
                    Me.btnAddNew.Enabled = True
                Case "btnExport"
                    Me.SaveFileDialog1.Title = "Define the location File"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                    Me.SaveFileDialog1.RestoreDirectory = True
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.AdvancedTManager1.GridEX1
                        Me.GridEXExporter1.IncludeExcelProcessingInstruction = True
                        Me.GridEXExporter1.IncludeFormatStyle = True
                        Me.GridEXExporter1.Export(FS)
                        FS.Flush()
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnEdit"
                    'If Me.pnlEntry.Height > 0 Then
                    '    If Not Me.btnEdit.Checked Then : Me.btnEdit.Checked = True : End If
                    'End If
                    If Me.AdvancedTManager1.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        Me.Cursor = Cursors.WaitCursor
                        Dim IDApp As Integer = AdvancedTManager1.GridEX1.GetValue("IDApp")
                        Me.PullAndShowData(IDApp)
                        Me.enabledEntry(True)
                        Me.btnSave.Enabled = True
                        Me.btnRefresh.Enabled = False
                    End If
                    If Me.pnlEntry.Height > 0 Then
                        If Not Me.btnEdit.Checked Then : Me.btnEdit.Checked = True : End If
                    End If
                Case "btnRefresh"
                    Me.Cursor = Cursors.WaitCursor
                    Me.GetData()
                    Me.SFG = StateFillingGrid.HasFilled
            End Select
            Me.isLoadingRow = False
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
            Me.isLoadingRow = False
            Me.SFG = StateFillingGrid.HasFilled
        End Try
    End Sub
    Private Sub enabledEntry(ByVal isEnabled As Boolean)
        Me.txtItemName.Enabled = isEnabled
        Me.txtRemark.Enabled = isEnabled
        Me.txtUnit1.Enabled = isEnabled
        Me.txtUnit2.Enabled = isEnabled
        Me.txtUOM.Enabled = isEnabled
        Me.txtVol1.Enabled = isEnabled
        Me.txtVol2.Enabled = isEnabled
        Me.txtDevQTy.Enabled = isEnabled
    End Sub
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.OtherProduct Then
                Me.btnAddNew.Visible = True
            Else
                Me.btnAddNew.Visible = False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.OtherProduct Then
                Me.btnEdit.Visible = True
                Me.AdvancedTManager1.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.btnEdit.Visible = False
            End If
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.OtherProduct Then
                Me.AdvancedTManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.AdvancedTManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
        Else
            Me.btnEdit.Visible = True
            Me.AdvancedTManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Me.AdvancedTManager1.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.btnAddNew.Visible = True
        End If
    End Sub
    Private Function ValidData()
        'txtItemName
        'txtUnit1
        'txtVol1
        'txtUnit2
        'txtVol2
        'txtUOM
        'txtRemark
        If Me.txtItemName.Text = "" Then
            Me.baseTooltip.Show("Please enter the Item Name", Me.txtItemName, 2000)
            Me.txtItemName.Focus()
            Return False
        ElseIf Me.txtUnit1.Text = "" Then
            Me.baseTooltip.Show("Please enter UNIT 1", Me.txtUnit1, 2000)
            Me.txtUnit1.Focus()
            Return False
        ElseIf Me.txtVol1.Text = "" Then
            Me.baseTooltip.Show("Please enter VOLUME 1", Me.txtVol1, 2000)
            Me.txtVol1.Focus()
            Return False
        ElseIf Me.txtUOM.Text = "" Then
            Me.baseTooltip.Show("Please enter Unit OF Meassure", Me.txtUOM, 2000)
            Me.txtUOM.Focus()
            Return False
        ElseIf Me.txtDevQTy.Text = "" Then
            Me.baseTooltip.Show("Please enter Devided Quantity", Me.txtDevQTy, 2000)
        End If
        Return True
    End Function
    Private Sub txtItemName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtVol2.KeyDown, txtVol1.KeyDown, txtUOM.KeyDown, txtUnit2.KeyDown, txtUnit1.KeyDown, txtRemark.KeyDown, txtItemName.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Not Me.ValidData() Then
                    Return
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function getStringCriteriaSearch() As String
        Dim strResult As String = "LIKE"
        Select Case Me.m_Criteria
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
                strResult = "BeginWith"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                strResult = "EndWith"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                strResult = "Equal"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                strResult = "Greater"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                strResult = "GreaterOrEqual"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.In
                strResult = "In"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                strResult = "Less"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                strResult = "LessOrEqual"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                strResult = "LIKE"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
                strResult = "NotEqual"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.NotIn
                strResult = "NotIn"
            Case NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
                strResult = "Between"
        End Select
        Return strResult
    End Function
    Friend Sub GetData()
        If Not Me.SFG = StateFillingGrid.Filling Then : Me.SFG = StateFillingGrid.Filling : End If
        RowCount = 0
        Dim SearchString As String = Me.AdvancedTManager1.txtSearch.Text.Trim(), SearchBy As String = Me.AdvancedTManager1.cbCategory.Text
        If SearchBy = "DEVIDED_QTY" Then
            SearchBy = "DEVIDED_QUANTITY"
        End If
        If (Me.AdvancedTManager1.cbCategory.Text = "") Then
            SearchBy = "ITEM"
        End If
        If Me.AdvancedTManager1.txtMaxRecord.Text <> "" Then
            If CInt(Me.AdvancedTManager1.txtMaxRecord.Text) < CInt(Me.AdvancedTManager1.cbPageSize.Text) Then
                Me.PageSize = CInt(Me.AdvancedTManager1.txtMaxRecord.Text)
            Else
                Me.PageSize = CInt(Me.AdvancedTManager1.cbPageSize.Text)
            End If
        Else
            Me.PageSize = CInt(Me.AdvancedTManager1.cbPageSize.Text)
        End If
        'Dim MaxRecord As Integer = 0
        Dim strCriteriaSearch As String = getStringCriteriaSearch()
        ''If Me.AdvancedTManager1.txtMaxRecord.Text <> "" Then
        ''    MaxRecord = Convert.ToInt32(Me.AdvancedTManager1.txtMaxRecord.Text.Trim())
        ''End If
        With Me.OriginalData
            .CriteriaSearch = strCriteriaSearch
            .SearchBy = SearchBy
            .SearchValue = SearchString
            Me.RunQuery(.SearchValue, .SearchBy)
        End With
        If AdvancedTManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.AdvancedTManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.AdvancedTManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub
    Friend Sub LoadData()
        ''isi autofill textbox unit & volume

        Me.SFG = StateFillingGrid.Filling

        Me.PageIndex = 1 : Me.PageSize = 1000 : Me.AdvancedTManager1.cbPageSize.Text = "1000"

        Me.AdvancedTManager1.cbCategory.Items.Clear() : Me.AdvancedTManager1.btnCriteria.Text = "*.*"
        Me.AdvancedTManager1.btnCriteria.Text = "*.*" : Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
        Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
        With Me.OriginalData
            .CriteriaSearch = "LIKE"
            If Me.AdvancedTManager1.txtMaxRecord.Text = "" Then
                .MaxRecord = 0 : Else : .MaxRecord = Convert.ToInt32(Me.AdvancedTManager1.txtMaxRecord.Text.Trim())
            End If
            If Me.AdvancedTManager1.txtSearch.Text = "" Then
                .SearchValue = ""
            Else
                .SearchValue = RTrim(Me.AdvancedTManager1.txtSearch.Text)
            End If
            If Me.AdvancedTManager1.cbCategory.Text = "" Then
                .SearchBy = "ITEM"
            ElseIf Me.AdvancedTManager1.cbCategory.Text = "U O M" Then
                .SearchBy = "UnitOfMeasure"
            Else
                .SearchBy = Me.AdvancedTManager1.cbCategory.Text
            End If
            If .SearchBy = "DEVIDED_QTY" Then
                .SearchBy = "DEVIDED_QUANTITY"
            End If
            Me.RunQuery(.SearchValue, .SearchBy)
        End With

        If AdvancedTManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.AdvancedTManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.AdvancedTManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub
    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String)
        Dim Dv As DataView = Nothing : Me.Isloadingrow = True

        Dv = Me.clsOther.PopulateQuery(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
        If Not IsNothing(Dv) Then
            If (Dv.Count > 0) Then
                Me.AdvancedTManager1.GridEX1.SetDataBinding(Dv, "")
                If Not Me.hasLoadGrid Then
                    Me.AdvancedTManager1.GridEX1.RetrieveStructure()
                    Me.FormatDataGrid()
                    Me.hasLoadGrid = True
                End If
            Else
                Me.AdvancedTManager1.GridEX1.SetDataBinding(Nothing, "")
            End If
        Else
            Me.AdvancedTManager1.GridEX1.SetDataBinding(Nothing, "")
        End If

    End Sub

    Private Function SavedData(ByVal mustCloseConnection As Boolean) As Boolean

        Me.clsOther.saveData(NDomOtherProd, Me.Mode, mustCloseConnection)
        Me.ODomOtherProd = Me.NDomOtherProd

    End Function
    Private Sub ClearEntryData()
        Me.ClearControl(Me.pnlEntry)
        Me.txtVol1.Text = ""
        Me.txtVol2.Text = ""
        Me.txtDevQTy.Text = ""
    End Sub
    Private Sub AddNewData()
        Me.pnlEntry.Size = Me.pnlEntryOpen
        Me.btnRefresh.Enabled = False
        Me.btnSave.Enabled = True
        Me.btnEdit.Enabled = False
        Me.btnEdit.Checked = False
        Me.ClearEntryData()
        Me.NDomOtherProd = New Nufarm.Domain.OtherProduct()
        Me.ODomOtherProd = New Nufarm.Domain.OtherProduct()
        Me.enabledEntry(True)
        Me.btnAddNew.Enabled = False
        'Me.ClearControl(Me.pnlEntry)
        Me.txtVol1.Text = ""
        Me.txtVol2.Text = ""
        Me.txtItemName.Focus()
        Me.Mode = SaveMode.Insert
    End Sub
    Private Function HasChangedData()
        If NDomOtherProd.ItemName <> ODomOtherProd.ItemName Then
            Return True
        ElseIf NDomOtherProd.Unit1 <> ODomOtherProd.Unit1 Then
            Return True
        ElseIf NDomOtherProd.Unit2 <> ODomOtherProd.Unit2 Then
            Return True
        ElseIf NDomOtherProd.UOM <> ODomOtherProd.UOM Then
            Return True
        ElseIf NDomOtherProd.Vol1 <> ODomOtherProd.Vol1 Then
            Return True
        ElseIf NDomOtherProd.Vol2 <> ODomOtherProd.Vol2 Then
            Return True
        ElseIf NDomOtherProd.Remark <> ODomOtherProd.Remark Then
            Return True
        ElseIf NDomOtherProd.Dev_Qty <> ODomOtherProd.Dev_Qty Then
            Return True
        End If
        Return False
    End Function
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If IsNothing(Me.NDomOtherProd) Then
                Me.NDomOtherProd = New Nufarm.Domain.OtherProduct()
            End If
            With Me.NDomOtherProd
                .ItemName = Me.txtItemName.Text.Trim()
                .Unit1 = Me.txtUnit1.Text.Trim()
                .Unit2 = Me.txtUnit2.Text.Trim()
                .Vol1 = CDec(Me.txtVol1.Value)
                .Vol2 = CDec(Me.txtVol2.Value)
                .UOM = Me.txtUOM.Text.Trim()
                .Remark = Me.txtRemark.Text.Trim()
                .CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
                .ModifiedDate = NufarmBussinesRules.User.UserLogin.UserName
                .Dev_Qty = Me.txtDevQTy.Value
            End With
            If Me.HasChangedData() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    If Me.ValidData() Then
                        Me.SavedData(False)
                        Me.GetData()
                    End If
                End If
            End If
            Me.pnlEntry.Size = New Size(Me.pnlEntry.Width, 0)
            Me.btnRefresh.Enabled = True
            Me.btnSave.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnEdit.Checked = False
            Me.ClearEntryData()
            Me.NDomOtherProd = New Nufarm.Domain.OtherProduct()
            Me.ODomOtherProd = New Nufarm.Domain.OtherProduct()
            'Me.enabledEntry(False)
            Me.btnAddNew.Enabled = True
            'Me.Mode = SaveMode.Insert
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnClose_Click")
        End Try
    End Sub

    Private Sub OtherProduct_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            If IsNothing(NufarmBussinesRules.SharedClass.tblUOM) Then
                Dim SettingConfig As New NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting()
                SettingConfig.FillDataUOM(False)
            ElseIf NufarmBussinesRules.SharedClass.tblUOM.Rows.Count <= 0 Then
                Dim SettingConfig As New NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting()
                SettingConfig.FillDataUOM(False)
            End If
            Dim colUOM As New AutoCompleteStringCollection()
            For Each row As DataRow In NufarmBussinesRules.SharedClass.tblUOM.Rows
                colUOM.Add(row("UnitOfMeasure"))
            Next

            Me.txtUOM.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Me.txtUOM.AutoCompleteSource = AutoCompleteSource.CustomSource
            Me.txtUOM.AutoCompleteCustomSource = colUOM

            If IsNothing(NufarmBussinesRules.SharedClass.unit1) Then
                Dim SettingConfig As New NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting()
                SettingConfig.FillDataUnit(False)
            ElseIf NufarmBussinesRules.SharedClass.unit1.Rows.Count <= 0 Then
                Dim SettingConfig As New NufarmBussinesRules.SettingDTS.RefBussinesRulesSetting()
                SettingConfig.FillDataUnit(False)
            End If
            Dim colUnit1 As New AutoCompleteStringCollection()
            For Each row As DataRow In NufarmBussinesRules.SharedClass.unit1.Rows
                colUnit1.Add(row("UNIT1"))
            Next

            Me.txtUnit1.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Me.txtUnit1.AutoCompleteSource = AutoCompleteSource.CustomSource
            Me.txtUnit1.AutoCompleteCustomSource = colUnit1

            Dim colUnit2 As New AutoCompleteStringCollection()
            For Each row As DataRow In NufarmBussinesRules.SharedClass.unit2.Rows
                colUnit2.Add(row("UNIT2"))
            Next

            Me.txtUnit2.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Me.txtUnit2.AutoCompleteSource = AutoCompleteSource.CustomSource
            Me.txtUnit2.AutoCompleteCustomSource = colUnit2

            Me.ReadAccess()
            Me.LoadData()
            'Me.NDomOtherProd = New NuFarm.Domain.OtherProduct()
            'Me.ODomOtherProd = New NuFarm.Domain.OtherProduct()
            Me.pnlEntry.Height = 0
            With Me.AdvancedTManager1
                .cbCategory.Items.Add("ITEM")
                .cbCategory.Items.Add("DESCRIPTION")
                .cbCategory.Items.Add("UNIT1")
                .cbCategory.Items.Add("VOL1")
                .cbCategory.Items.Add("UNIT2")
                .cbCategory.Items.Add("VOL2")
                .cbCategory.Items.Add("IsRowMat")
                .cbCategory.Items.Add("U O M")
                .cbCategory.Items.Add("DEVIDED_QTY")
            End With
            Me.btnSave.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnAddNew.Enabled = True
            Me.btnRefresh.Enabled = True

            Me.SFG = StateFillingGrid.HasFilled
            Me.hasLoadForm = True
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled
            Me.hasLoadForm = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OtherProduct_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If Me.pnlEntry.Height > 0 Then
            With Me.NDomOtherProd
                .ItemName = Me.txtItemName.Text.Trim()
                .Unit1 = Me.txtUnit1.Text.Trim()
                .Unit2 = Me.txtUnit2.Text.Trim()
                .Vol1 = CDec(Me.txtVol1.Value)
                .Vol2 = CDec(Me.txtVol2.Value)
                .UOM = Me.txtUOM.Text.Trim()
                .Remark = Me.txtRemark.Text.Trim()
                .CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
                .ModifiedDate = NufarmBussinesRules.User.UserLogin.UserName
                .Dev_Qty = Me.txtDevQTy.Value
            End With
            If Me.HasChangedData() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    Try
                        Me.SavedData(True)
                    Catch ex As Exception
                        Me.ShowMessageError(ex.Message)
                    End Try
                End If
            End If
        End If
    End Sub
    Private Sub AdvancedTManager1_ButonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedTManager1.ButonClick
        Try
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            If (TypeOf (sender) Is Button) Then
                Me.PageIndex = 1
            ElseIf (TypeOf (sender) Is Janus.Windows.EditControls.UIButton) Then
                Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                    Case "btnGoFirst" : Me.PageIndex = 1 : Case "btnGoPrevios" : Me.PageIndex -= 1
                    Case "btnNext" : Me.PageIndex += 1 : Case "btnGoLast" : Me.PageIndex = Me.TotalIndex
                End Select
            End If
            Me.GetData()
            Me.SetOriginalCriteria()
        Catch ex As Exception
            Me.UndoCriteria()
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.SFG = StateFillingGrid.HasFilled
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AdvancedTManager1_ChangesCriteria(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedTManager1.ChangesCriteria
        Try
            If Not Me.SFG = StateFillingGrid.HasFilled Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.AdvancedTManager1.txtSearch.Enabled = True
            If Me.isUndoingCriteria Then : Return : End If
            Select Case Me.AdvancedTManager1.btnCriteria.CompareOperator
                Case CompareOperator.BeginWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
                Case CompareOperator.EndWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                Case CompareOperator.Equal
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                Case CompareOperator.Greater
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                Case CompareOperator.GreaterOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                Case CompareOperator.In
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.In
                Case CompareOperator.Less
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                Case CompareOperator.LessOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                Case CompareOperator.Like
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                Case CompareOperator.NotEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
                Case CompareOperator.Between
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
                    Me.AdvancedTManager1.txtSearch.Text = ""
                    Me.AdvancedTManager1.txtSearch.Enabled = False
                    Me.setVisibleDateControl(True)
                Case Else
                    Throw New Exception("Please select another operator")
            End Select
            Me.AdvancedTManager1_ButonClick(Me.AdvancedTManager1.btnSearch, New EventArgs())
            Me.SetOriginalCriteria()
        Catch ex As Exception
            Me.UndoCriteria()
            Me.SFG = StateFillingGrid.HasFilled
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Criteria_Changed")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub SetStatusRecord()
        Me.TotalIndex = 0
        Me.AdvancedTManager1.lblResult.Text = String.Format("Found {0} Record(s)", RowCount.ToString())
        If (RowCount <> 0) Then
            Me.TotalIndex = RowCount / Me.PageSize
            If (RowCount - (TotalIndex * PageSize) > 0) Then
                TotalIndex += 1
            End If
        End If
        AdvancedTManager1.lblPosition.Text = String.Format("Page {0} Of {1} page(s)", Me.PageIndex, Me.TotalIndex)
    End Sub
    Private Sub setVisibleDateControl(ByVal isVisible As Boolean)
        AdvancedTManager1.lblFrom.Visible = isVisible
        AdvancedTManager1.lblUntil.Visible = isVisible
        AdvancedTManager1.dtPicFrom.Visible = isVisible
        AdvancedTManager1.dtPicUntil.Visible = isVisible
    End Sub
    Private Sub SetButtonStatus()
        Me.AdvancedTManager1.btnGoFirst.Enabled = Me.PageIndex <> 1
        Me.AdvancedTManager1.btnGoPrevios.Enabled = Me.PageIndex <> 1
        Me.AdvancedTManager1.btnNext.Enabled = Me.PageIndex <> TotalIndex : If TotalIndex <= 0 Then : AdvancedTManager1.btnNext.Enabled = False : End If
        Me.AdvancedTManager1.btnGoLast.Enabled = Me.PageIndex <> TotalIndex : If TotalIndex <= 0 Then : AdvancedTManager1.btnGoLast.Enabled = False : End If
        If (AdvancedTManager1.GridEX1.DataSource Is Nothing) Then
            AdvancedTManager1.btnGoFirst.Enabled = False : AdvancedTManager1.btnGoLast.Enabled = False
            AdvancedTManager1.btnNext.Enabled = False : AdvancedTManager1.btnGoLast.Enabled = False
        ElseIf AdvancedTManager1.GridEX1.RecordCount <= 0 Then
            AdvancedTManager1.btnGoFirst.Enabled = False : AdvancedTManager1.btnGoLast.Enabled = False
            AdvancedTManager1.btnNext.Enabled = False : AdvancedTManager1.btnGoLast.Enabled = False
        ElseIf AdvancedTManager1.GridEX1.RecordCount <= 0 Then
        End If
    End Sub
    Private Sub AdvancedTManager1_CmbSelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedTManager1.CmbSelectedIndexChanged
        Try
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            If AdvancedTManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Select Case Me.AdvancedTManager1.cbCategory.Text
                Case "ITEM", "DESCRIPTION", "UNIT1", "UNIT2", "U O M"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
                Case "IsRowMat"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.Boolean
                Case "VOL1", "VOL2"
                    Me.m_DataType = DataTypes.Decimal
                Case "DEVIDED_QTY"
                    Me.m_DataType = DataTypes.Decimal
            End Select
            Me.AdvancedTManager1_ButonClick(Me.AdvancedTManager1.btnSearch, New EventArgs())
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled
            Me.LogMyEvent(ex.Message, Me.Name + "_TManager1_CmbSelectedIndexChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AdvancedTManager1_DeleteGridRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles AdvancedTManager1.DeleteGridRecord
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim ocreatedDate As Object = Me.AdvancedTManager1.GridEX1.GetValue("CreatedDate")
            If Not IsNothing(ocreatedDate) And Not IsDBNull(ocreatedDate) Then
                Dim createdDate As System.DateTime = Convert.ToDateTime(Me.AdvancedTManager1.GridEX1.GetValue("CreatedDate"))
                Dim nDay As Integer = DateDiff(DateInterval.Day, createdDate, NufarmBussinesRules.SharedClass.ServerDate)
                If CMain.IsSystemAdministrator Or Me.IsHOUser Then
                Else
                    Me.ShowMessageInfo(Me.MessageCantDeleteData & vbCrLf & "Data after 3 days left is unable to delete")
                    Me.Cursor = Cursors.Default
                    e.Cancel = True
                    Return
                End If
            End If
            Dim IDApp As Object = Me.AdvancedTManager1.GridEX1.GetValue("IDApp")
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage & vbCrLf & "Operation can not be undone") = Windows.Forms.DialogResult.No Then
                Me.Cursor = Cursors.Default
                e.Cancel = True
                Return
            End If

            Dim boolSucced As Boolean = Me.clsOther.Delete(IDApp, True)
            If boolSucced Then : e.Cancel = False
            Else : e.Cancel = True
            End If
            'cek apakah ada IDApp detial di GON_SEPARATED_DETAIL
            Me.AdvancedTManager1.GridEX1.UpdateData()
            If (Me.AdvancedTManager1.GridEX1.RecordCount <= 0) Then
                If Me.TotalIndex > 1 Then
                    If Me.PageIndex > 1 Then
                        Me.PageIndex -= 1
                    End If
                End If
                Me.SetStatusRecord()
                Me.SetButtonStatus()
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            e.Cancel = True : Me.Cursor = Cursors.Default : Me.LogMyEvent(ex.Message, Me.Name + "_AdvancedTManager1_DeleteGridRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AdvancedTManager1_GridCurrentCell_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedTManager1.GridCurrentCell_Changed
        Try
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            If Not Me.hasLoadForm Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.RecordCount <= 0 Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
                Me.btnEdit.Enabled = False
                Me.btnEdit.Checked = False
                Return
            End If
            ''check hak akses
            'get createdDate
            Me.btnEdit.Enabled = True
        Catch ex As Exception
            If Me.SFG = StateFillingGrid.Filling Then
                Me.SFG = StateFillingGrid.HasFilled
            End If
            'Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, "_AdvancedTManager1_GridCurrentCell_Changed")
        End Try
    End Sub
    Private Sub PullAndShowData(ByVal IDapp As Integer)
        Me.pnlEntry.Size = Me.pnlEntryOpen
        Me.ClearEntryData()
        Me.ODomOtherProd = New NuFarm.Domain.OtherProduct()
        With Me.ODomOtherProd
            .IDApp = Me.AdvancedTManager1.GridEX1.GetValue("IDApp")
            .ItemName = Me.AdvancedTManager1.GridEX1.GetValue("ITEM")
            Me.txtItemName.Text = .ItemName
            .Remark = Me.AdvancedTManager1.GridEX1.GetValue("DESCRIPTION")
            Me.txtRemark.Text = .Remark
            .Unit1 = Me.AdvancedTManager1.GridEX1.GetValue("UNIT1")
            Me.txtUnit1.Text = .Unit1
            .Unit2 = Me.AdvancedTManager1.GridEX1.GetValue("UNIT2")
            Me.txtUnit2.Text = .Unit2
            .Vol1 = Me.AdvancedTManager1.GridEX1.GetValue("VOL1")
            Me.txtVol1.Value = .Vol1
            .Vol2 = Me.AdvancedTManager1.GridEX1.GetValue("VOL2")
            Me.txtVol2.Value = .Vol2
            .UOM = Me.AdvancedTManager1.GridEX1.GetValue("UnitOfMeasure")
            Me.txtUOM.Text = .UOM
            .Dev_Qty = Me.AdvancedTManager1.GridEX1.GetValue("DEVIDED_QTY")
            Me.txtDevQTy.Value = .Dev_Qty

        End With
        Me.Mode = SaveMode.Update
    End Sub
    Private Sub AdvancedTManager1_GridDoubleClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancedTManager1.GridDoubleClicked
        Try
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.RecordCount <= 0 Then : Return : End If
            If Me.AdvancedTManager1.GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim IDApp As Integer = Me.AdvancedTManager1.GridEX1.GetValue("IDApp")
            'Dim PONumber As Object = AdvancedTManager1.GridEX1.GetValue("PO_NUMBER")
            Me.PullAndShowData(IDApp)
            Me.enabledEntry(False)
            Me.btnSave.Enabled = False
            Me.btnAddNew.Enabled = True
            Me.btnRefresh.Enabled = False
            Me.btnEdit.Checked = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub AdvancedTManager1_TetxBoxKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles AdvancedTManager1.TetxBoxKeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.Cursor = Cursors.WaitCursor
                Me.AdvancedTManager1_ButonClick(Me.AdvancedTManager1.btnSearch, New EventArgs())
            End If
        Catch ex As Exception
            Me.UndoCriteria()
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.SFG = StateFillingGrid.HasFilled
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AdvancedTManager1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles AdvancedTManager1.CellUpdated
        Try
            If e.Column.Key = "InActive" Then
                'update data
                Me.Cursor = Cursors.WaitCursor
                Me.clsOther.UpdateInActive(CBool(Me.AdvancedTManager1.GridEX1.GetValue(e.Column)), Me.AdvancedTManager1.GridEX1.GetValue("IDApp"))
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception
            Me.AdvancedTManager1.GridEX1.CancelCurrentEdit()
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
