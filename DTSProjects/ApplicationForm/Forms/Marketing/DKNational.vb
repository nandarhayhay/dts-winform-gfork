
Public Class DKNational
    Private m_Criteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private OriginalCriteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private isUndoingCriteria As Boolean = False
    Private m_DataType As NufarmBussinesRules.common.Helper.DataTypes = NufarmBussinesRules.common.Helper.DataTypes.VarChar
    Private Mode As NufarmBussinesRules.common.Helper.SaveMode = NufarmBussinesRules.common.Helper.SaveMode.None
    Private IsloadedForm As Boolean = False : Private Isloadingrow As Boolean = True
    Private PageIndex As Int32 = 0, PageSize As Integer = 0, TotalIndex As Integer = 0, RowCount As Integer = 0
    Private isLoadingCombo As Boolean = False
    Private OriginalData As New NufarmBussinesRules.common.dataTManager()
    Friend CMain As Main = Nothing
    Private IsSavingData As Boolean = False
    Private m_clsProgram As NufarmBussinesRules.Program.BrandPackInclude = Nothing
    Private DS As DataSet
    Private pnlentryHeight As Integer = 0
    Private DVProgressive As DataView = Nothing
    Private DVBrand As DataView = Nothing
    Private DVBrandGrid As DataView = Nothing
    Private ODKN As NuFarm.Domain.DKN
    Private IDAppSDS As Integer = 0
    Private hasLoadForm As Boolean = False
    Private seedProductToGive As ProductToGive = ProductToGive.None
    Private mustReloadQuery As Boolean = False
    Private Enum ProductToGive
        None
        AllProduct
        CertainBrand
    End Enum
    Private seedProductRule As ProductRule = ProductRule.None
    Private Enum ProductRule
        None
        toAllProduct
        toCertainProduct
    End Enum
    Private ReadOnly Property clsProgram() As NufarmBussinesRules.Program.BrandPackInclude
        Get
            If IsNothing(m_clsProgram) Then
                m_clsProgram = New NufarmBussinesRules.Program.BrandPackInclude()
            End If
            Return m_clsProgram
        End Get
    End Property
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = DirectCast(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnShowFieldChooser"
                    Me.TManager1.GridEX1.ShowFieldChooser()
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.TManager1.GridEX1
                    SetGrid.ShowDialog()
                Case "btnCustomFilter"
                    Me.FilterEditor1.Visible = True
                    Me.FilterEditor1.SortFieldList = False
                    Me.FilterEditor1.SourceControl = Me.TManager1.GridEX1
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.TManager1.GridEX1.RemoveFilters()
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.FilterEditor1.SourceControl = Nothing
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.TManager1.GridEX1.RemoveFilters()
                Case "btnExport"
                    ExportData(Me.TManager1.GridEX1)
                Case "btnRefresh"
                    If Me.btnAddNew.Checked = True Or Me.btnEditRow.Checked = True Then
                        Me.ShowMessageInfo("Please close entry area to refresh/reload data")
                        Return
                    End If
                    'reload Data
                    Me.GetData() : Me.SetOriginalCriteria()
                    Me.mustReloadQuery = False
                Case "btnAddNew"
                    Me.Isloadingrow = True
                    If Me.btnAddNew.Checked Then
                        Me.ClearEntry(True)
                        Me.pnlEntryAndFilter.Height = 0
                        Me.btnAddNew.Checked = False
                    Else
                        Me.pnlEntryAndFilter.Height = Me.pnlentryHeight
                        Me.ClearEntry(True)
                        Me.btnAddNew.Checked = True
                        Me.btnEditRow.Checked = False
                        Me.dtPicFrom.ReadOnly = False
                        Me.dtPicUntil.ReadOnly = False
                        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                    End If
                    Me.grpTypeDiscount.Enabled = True
                    Me.grpProductToGive.Enabled = True
                Case "btnEditRow"
                    Me.Isloadingrow = True
                    If Me.btnEditRow.Checked Then
                        Me.ClearEntry(True)
                        Me.pnlEntryAndFilter.Height = 0
                        Me.btnEditRow.Checked = False : Me.Cursor = Cursors.Default : Return
                    Else
                        If Me.TManager1.GridEX1.DataSource Is Nothing Then
                            Me.Isloadingrow = True : Me.ClearEntry(True) : Me.btnEditRow.Checked = False
                            Me.Isloadingrow = False : Me.Cursor = Cursors.Default : Return
                        End If
                        If Me.TManager1.GridEX1.RecordCount <= 0 Then
                            Me.Isloadingrow = True : Me.ClearEntry(True) : Me.btnEditRow.Checked = False
                            Me.Isloadingrow = False : Me.Cursor = Cursors.Default : Return
                        End If
                        If Me.TManager1.GridEX1.RecordCount > 0 Then
                            If Me.TManager1.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                                Me.pnlEntryAndFilter.Height = Me.pnlentryHeight
                                Me.InflateData() : Me.btnEditRow.Checked = True : Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
                            Else
                                Me.Isloadingrow = True : Me.ClearEntry(True) : Me.btnEditRow.Checked = False
                                Me.Isloadingrow = False : Me.Cursor = Cursors.Default : Return
                            End If
                        End If
                        Me.btnEditRow.Checked = True : Me.btnAddNew.Checked = False
                    End If
                    Me.btnSave.Visible = True

                    Me.Isloadingrow = False
                Case "btnSave"
                    If Me.pnlEntryAndFilter.Height <> 0 Then : Me.SaveData() : End If
            End Select
            Me.Isloadingrow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
            Me.Isloadingrow = False
        End Try
    End Sub

    Private Function SaveData() As Boolean
        If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None Then
            Return False
        End If
        If Me.rdbAllProduct.Checked = False And Me.rdbCertainProducts.Checked = False Then
            Me.baseTooltip.Show("Please define product to support", Me.grpProductToGive, 2500) : Me.grpProductToGive.Focus()
            Return False
        End If
        If Me.rdbOneRuleDiscToAllBrand.Checked = False And Me.rdbRuleDiscToCertainBrand.Checked = False Then
            Me.baseTooltip.Show("Please define the rule", Me.grpTypeDiscount, 2500) : Me.grpTypeDiscount.Focus()
            Return False
        End If
        If Me.rdbCertainProducts.Checked Then
            If Me.grdBrand.RecordCount <= 0 Then
                Me.baseTooltip.Show("Please define brand(s)", Me.grdBrand, 2500) : Me.grdBrand.Focus() : Me.grdBrand.MoveToNewRecord() : Return False
            End If
        End If
        If Me.rdbRuleDiscToCertainBrand.Checked Then
            Dim rowFilter = "BRAND_ID = '' OR BRAND_ID IS NULL"
            Dim oriRowFilter = Me.DVProgressive.RowFilter
            Me.DVProgressive.RowFilter = ""
            Dim DVDummy As DataView = Me.DVProgressive.ToTable().Copy().DefaultView
            Me.DVProgressive.RowFilter = oriRowFilter
            DVDummy.RowFilter = rowFilter
            If DVDummy.Count > 0 Then
                Me.baseTooltip.Show("Please enter discount for every brand", Me.grdBrand, 2500) : Me.grdBrand.Focus() : Return False
            End If
            For Each rowView As DataRowView In Me.DVBrandGrid
                Dim BrandID As String = rowView("BRAND_ID").ToString()
                DVDummy.RowFilter = "BRAND_ID = '" & BrandID & "'"
                If DVDummy.Count <= 0 Then
                    Me.baseTooltip.Show("Please enter discount for every brand", Me.grdBrand, 2500) : Me.grdBrand.Focus() : Return False
                End If
            Next
        End If
        If Me.DS.HasChanges() Then
            Me.ODKN = New NuFarm.Domain.DKN()
            If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
                Me.ODKN.IDApp = Me.IDAppSDS
            End If
            Me.ODKN.CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
            Me.ODKN.CreatedDate = NufarmBussinesRules.SharedClass.ServerDate
            Me.ODKN.StartDate = Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString())
            Me.ODKN.EndDate = Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())
            Me.ODKN.ProductToGive = IIf(Me.rdbAllProduct.Checked, "All Products", "Certain Products")
            Me.ODKN.ProductRule = IIf(Me.rdbOneRuleDiscToAllBrand.Checked, "All Products", "Certain Products")
            Me.clsProgram.SaveDKN(Me.DS, Mode, ODKN, False)
            ''reload data
            Me.mustReloadQuery = True
            Me.PageIndex = 1
            Me.GetData() : Me.SetOriginalCriteria()
        End If
        If Not Me.Isloadingrow Then : Me.Isloadingrow = True : End If
        Me.ClearEntry(True)
        If NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DKNational Then
            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
        Else
            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None
        End If
        Me.Isloadingrow = False
    End Function
    Private Sub ExportData(ByVal grid As Janus.Windows.GridEX.GridEX)
        Dim SV As New SaveFileDialog()
        With SV
            .Title = "Define the location File"
            .InitialDirectory = System.Environment.SpecialFolder.MyDocuments
            .OverwritePrompt = True
            .DefaultExt = ".xls"
            .FileName = "All Files|*.*"
        End With
        If SV.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim FS As New System.IO.FileStream(SV.FileName, IO.FileMode.Create)
            Dim Exporter As New Janus.Windows.GridEX.Export.GridEXExporter()
            Exporter.GridEX = grid
            Exporter.Export(FS)
            FS.Close()
            MessageBox.Show("Data Exported to " & SV.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DKNational Then
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            Else
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DKNational Then
                Me.btnAddNew.Visible = False
            Else
                Me.btnAddNew.Visible = True
                Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DKNational Then
                Me.btnEditRow.Visible = False
            Else
                Me.btnEditRow.Visible = True
            End If
        End If
    End Sub
    Private Sub ClearEntry(ByVal disposeGrid As Boolean)
        Me.ResetDateValue()
        Me.rdbAllProduct.Checked = False
        Me.rdbCertainProducts.Checked = False
        Me.rdbOneRuleDiscToAllBrand.Checked = False
        Me.rdbRuleDiscToCertainBrand.Checked = False
        If disposeGrid Then
            Me.grdBrand.DataSource = Nothing
            Me.grdBrand.DataSource = Nothing
            Me.grdProgressive.DataSource = Nothing
            If Not IsNothing(Me.DS) Then
                Me.DS.Dispose()
            End If
            Me.DS = Nothing
        End If
    End Sub

    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String, ByVal HasChangeCriteriaSearch As Boolean)
        Dim Dv As DataView = Nothing : Me.Isloadingrow = True

        Dv = Me.clsProgram.PopulateQuery(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, _
         Me.m_Criteria, Me.m_DataType, HasChangeCriteriaSearch, Me.mustReloadQuery)

        If Not IsNothing(Dv) Then
            If (Dv.Count > 0) Then
                Me.TManager1.GridEX1.DataSource = Dv : Me.TManager1.GridEX1.RetrieveStructure() : Me.FormatDataGrid()
                'If Not Me.IsloadedForm Then

                'End If
            Else
                If Not IsNothing(Me.TManager1.GridEX1.RootTable) Then
                    TManager1.GridEX1.RootTable.Columns.Clear()
                End If
            End If
        Else
            If Not IsNothing(Me.TManager1.GridEX1.RootTable) Then
                Me.TManager1.GridEX1.RootTable.Columns.Clear()
            End If
        End If

        If Me.IsloadedForm = False Then
            Me.Isloadingrow = False
        End If
    End Sub

    Private Sub FormatDataGrid()
        For Each col As Janus.Windows.GridEX.GridEXColumn In TManager1.GridEX1.RootTable.Columns
            If col.Type Is Type.GetType("System.Int32") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            ElseIf col.Type Is Type.GetType("System.Decimal") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FormatString = "#,##0.00"
            ElseIf col.Type Is Type.GetType("System.DateTime") Then
                col.FormatString = "dd MMMM yyyy"
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            End If
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
        Next
        Me.TManager1.GridEX1.RootTable.Columns("FKApp_SDS").Visible = False
        Me.TManager1.GridEX1.RootTable.Columns("FKApp_SDB").Visible = False

        Me.TManager1.GridEX1.RootTable.Columns("FKApp_SDS").ShowInFieldChooser = False
        Me.TManager1.GridEX1.RootTable.Columns("FKApp_SDB").ShowInFieldChooser = False

        Me.TManager1.GridEX1.RootTable.Groups.Add(New Janus.Windows.GridEX.GridEXGroup(Me.TManager1.GridEX1.RootTable.Columns("PERIODE")))
        Me.TManager1.GridEX1.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.TManager1.GridEX1.GroupByBoxVisible = False
        Me.TManager1.GridEX1.RootTable.Columns("PERIODE").Visible = False
        If (Me.FilterEditor1.Visible) And (Not IsNothing(Me.FilterEditor1.SourceControl)) Then
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
        Else
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        End If
        Me.TManager1.GridEX1.AutoSizeColumns(Me.TManager1.GridEX1.RootTable)
    End Sub

    Private Sub SetButtonStatus()
        Me.TManager1.btnGoFirst.Enabled = Me.PageIndex <> 1
        Me.TManager1.btnGoPrevios.Enabled = Me.PageIndex <> 1
        Me.TManager1.btnNext.Enabled = Me.PageIndex <> TotalIndex : If TotalIndex <= 0 Then : TManager1.btnNext.Enabled = False : End If
        Me.TManager1.btnGoLast.Enabled = Me.PageIndex <> TotalIndex : If TotalIndex <= 0 Then : TManager1.btnGoLast.Enabled = False : End If
        If (TManager1.GridEX1.DataSource Is Nothing) Then
            TManager1.btnGoFirst.Enabled = False : TManager1.btnGoLast.Enabled = False
            TManager1.btnNext.Enabled = False : TManager1.btnGoLast.Enabled = False
        ElseIf TManager1.GridEX1.RecordCount <= 0 Then
            TManager1.btnGoFirst.Enabled = False : TManager1.btnGoLast.Enabled = False
            TManager1.btnNext.Enabled = False : TManager1.btnGoLast.Enabled = False
        ElseIf TManager1.GridEX1.RecordCount <= 0 Then
        End If
    End Sub
    Private Sub SetStatusRecord()
        Me.TotalIndex = 0
        Me.TManager1.lblResult.Text = String.Format("Found {0} Record(s)", RowCount.ToString())
        If (RowCount <> 0) Then
            Me.TotalIndex = RowCount / Me.PageSize

            If (RowCount - (TotalIndex * PageSize) > 0) Then
                TotalIndex += 1
            End If
            If Me.TotalIndex <= 1 Then : Me.TotalIndex = 1 : End If
        End If
        TManager1.lblPosition.Text = String.Format("Page {0} Of {1} page(s)", Me.PageIndex, Me.TotalIndex)
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
        End Select
        Return strResult
    End Function
    Private Sub UndoCriteria()
        Me.isUndoingCriteria = True
        With Me.TManager1
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
            End Select
        End With
        Me.isUndoingCriteria = False
    End Sub
    Private Sub SetOriginalCriteria()
        Select Case Me.TManager1.btnCriteria.CompareOperator
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
        End Select
    End Sub
    Private Sub ResetDateValue()
        Dim EndDateCurAgree = Me.clsProgram.getEndDateCurrentAgreement(False)
        If Not IsNothing(EndDateCurAgree) Then
            Me.dtPicUntil.Value = Convert.ToDateTime(EndDateCurAgree)
        End If
        Me.dtPicFrom.MaxDate = Me.dtPicUntil.Value
        Me.dtPicFrom.Value = Me.dtPicUntil.Value.AddMonths(-6)
    End Sub
    Friend Sub LoadData()
        isLoadingCombo = True : Me.IsloadedForm = True
        ResetDateValue()
        With Me.TManager1.cbCategory
            .Items.Add("BRAND_NAME") : .Items.Add("START_DATE") : .Items.Add("END_DATE")
        End With
        Me.PageIndex = 1 : Me.PageSize = 500 : Me.TManager1.cbPageSize.Text = "500"
        Me.TManager1.btnCriteria.Text = "*.*"
        Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
        Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
        Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
        With Me.OriginalData
            .CriteriaSearch = "LIKE"
            If Me.TManager1.txtMaxRecord.Text = "" Then
                .MaxRecord = 0 : Else : .MaxRecord = Convert.ToInt32(Me.TManager1.txtMaxRecord.Text.Trim())
            End If
            If Me.TManager1.txtSearch.Text = "" Then
                .SearchValue = ""
            Else
                .SearchValue = RTrim(Me.TManager1.txtSearch.Text)
            End If
            If Me.TManager1.cbCategory.Text = "" Then
                .SearchBy = "BRAND_NAME"
            ElseIf Me.TManager1.cbCategory.Text = "BRAND_NAME" Then
                .SearchBy = "BRAND_NAME"
            Else
                .SearchBy = Me.TManager1.cbCategory.Text
            End If
            Me.RunQuery(.SearchValue, .SearchBy, True)
        End With
        If TManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub
    Friend Sub GetData()
        Dim SearchString As String = Me.TManager1.txtSearch.Text, SearchBy As String = Me.TManager1.cbCategory.Text
        If (Me.TManager1.cbCategory.Text = "") Then
            SearchBy = "BRAND_NAME"
        ElseIf Me.TManager1.cbCategory.Text = "BRAND_NAME" Then : SearchBy = "BRAND_NAME"
        Else : SearchBy = Me.TManager1.cbCategory.Text
        End If
        If Me.TManager1.txtMaxRecord.Text <> "" Then
            If CInt(Me.TManager1.txtMaxRecord.Text) < CInt(Me.TManager1.cbPageSize.Text) Then
                Me.PageSize = CInt(Me.TManager1.txtMaxRecord.Text)
            Else
                Me.PageSize = CInt(Me.TManager1.cbPageSize.Text)
            End If
        Else
            Me.PageSize = CInt(Me.TManager1.cbPageSize.Text)
        End If
        'chek apakah originaldata sudah berubah / belum
        Dim MaxRecord As Integer = 0, strCriteriaSearch As String = getStringCriteriaSearch()
        If Me.TManager1.txtMaxRecord.Text <> "" Then
            MaxRecord = Convert.ToInt32(Me.TManager1.txtMaxRecord.Text.Trim())
        End If
        Dim Ischanged As Boolean = False
        If Me.OriginalData.SearchBy <> SearchBy Then
            Ischanged = True
        ElseIf Me.OriginalData.CriteriaSearch <> strCriteriaSearch Then
            Ischanged = True
        ElseIf Me.OriginalData.MaxRecord <> MaxRecord Then
            Ischanged = True
            'ElseIf Me.OriginalData.SearchValue <> RTrim(Me.TManager1.txtSearch.Text) Then
            '    Ischanged = True
        ElseIf Me.IsSavingData Then
            Ischanged = True
        End If
        'set originaldata
        With Me.OriginalData
            .CriteriaSearch = strCriteriaSearch
            .MaxRecord = MaxRecord
            .SearchBy = SearchBy
            .SearchValue = RTrim(Me.TManager1.txtSearch.Text)
            Me.RunQuery(.SearchValue, .SearchBy, Ischanged)
        End With
        If TManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub

    Private Function hasChangedBrandOrProgData() As Boolean
        If Not IsNothing(Me.DS) Then
            If Me.DS.HasChanges() Then
                Return True
            End If
        End If

    End Function
    Private Sub InflateData()
        If Not Me.Isloadingrow Then : Me.Isloadingrow = True : End If
        Me.ClearEntry(False)
        Me.IDAppSDS = Convert.ToInt32(Me.TManager1.GridEX1.GetValue("FKApp_SDS"))
        Dim StartDate As DateTime = Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("START_DATE"))
        Dim EndDate As DateTime = Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("END_DATE"))
        Me.dtPicFrom.Value = StartDate
        Me.dtPicUntil.Value = EndDate
        Dim Periode As String = String.Format("{0:dd/MM/yyyy}-{1:dd/MM/yyyy}", StartDate, EndDate)
        If Me.clsProgram.hasDataUsed(Periode, "", False) Then
            Me.dtPicFrom.ReadOnly = True
            Me.dtPicUntil.ReadOnly = True
        Else
            Me.dtPicFrom.ReadOnly = False
            Me.dtPicUntil.ReadOnly = False
        End If
        If Me.TManager1.GridEX1.GetValue("APPLY_FOR_PRODUCTS").ToString() = "All Products" Then
            Me.rdbAllProduct.Checked = True
        ElseIf Me.TManager1.GridEX1.GetValue("APPLY_FOR_PRODUCTS").ToString() = "Certain Products" Then
            Me.rdbCertainProducts.Checked = True
        End If
        Me.DS = New DataSet("DSSkema")
        If Me.TManager1.GridEX1.GetValue("APPLY_DISCOUNT_RULE").ToString() = "All Products" Then
            Me.rdbOneRuleDiscToAllBrand.Checked = True
        ElseIf Me.TManager1.GridEX1.GetValue("APPLY_DISCOUNT_RULE").ToString() = "Certain Products" Then
            Me.rdbRuleDiscToCertainBrand.Checked = True
        End If
        If Not IsNothing(Me.DS) Then
            Me.DS.RejectChanges()
        End If
        Me.DS = New DataSet("DSSkema") : Me.DS.Clear()
        If Me.TManager1.GridEX1.GetValue("BRAND_NAME").ToString = "ALL_BRAND" Then
            'unabledkan gridBrand
            Me.grdBrand.Enabled = False
        Else
            'getBrand by IDApp SDS
            Dim dtTableBrandGrid As DataTable = Me.clsProgram.getDataBrand(Me.IDAppSDS, False)
            DS.Tables.Add(dtTableBrandGrid)
        End If
        Dim dtTableBProgDisc As DataTable = Me.clsProgram.getDataProgressive(Me.IDAppSDS, False)
        Me.DS.Tables.Add(dtTableBProgDisc)
        Me.DS.AcceptChanges()
        Me.DVProgressive = Me.DS.Tables("T_Progressive").DefaultView
        Me.DVBrand = Me.clsProgram.getDataBrand(Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), False).DefaultView
        Me.grdProgressive.SetDataBinding(DVProgressive, "")
        Me.grdProgressive.DropDowns("BRAND").SetDataBinding(Me.DVBrand, "")
        'get brand+

        If DS.Tables.Contains("T_BrandGrid") Then
            Me.DVBrandGrid = Me.DS.Tables("T_BrandGrid").DefaultView
            Me.grdBrand.SetDataBinding(DVBrandGrid, "")
            Me.grdBrand.DropDowns("Brand").SetDataBinding(DVBrand, "")
            Me.dtPicFrom.ReadOnly = True
            Me.dtPicUntil.ReadOnly = True
        End If
        Me.grpProductToGive.Enabled = False
        Me.grpTypeDiscount.Enabled = False
    End Sub
    Private Sub grdBrand_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdBrand.CurrentCellChanged
        Try
            'filter grid progressive
            If Me.Isloadingrow Then : Return : End If
            If Not Me.hasLoadForm Then : Return : End If
            If Me.grdBrand.DataSource Is Nothing Then : Return : End If
            If Me.grdBrand.RecordCount <= 0 Then : Return : End If
            If Me.grdBrand.SelectedItems Is Nothing Then : Return : End If
            Isloadingrow = True
            If Me.rdbRuleDiscToCertainBrand.Checked Then
                If Me.grdBrand.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If Not IsNothing(Me.DVProgressive) Then
                        DVProgressive.RowFilter = "BRAND_ID = '" & Me.grdBrand.GetValue("BRAND_ID").ToString() & "'"
                    End If
                    Me.grdBrand.RootTable.Columns("BRAND_ID").Selectable = False
                ElseIf Me.grdBrand.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                    If Not IsNothing(Me.DVProgressive) Then
                        Me.DVProgressive.RowFilter = ""
                    End If
                    Me.grdBrand.RootTable.Columns("BRAND_ID").Selectable = True
                End If
                Me.grdBrand.RootTable.Columns("BRAND_ID").Visible = True
            End If
            Isloadingrow = False
        Catch ex As Exception
            Isloadingrow = False
            Me.Cursor = Cursors.WaitCursor
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub

    Private Sub grdBrand_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdBrand.AddingRecord
        Try
            Me.grdBrand.SetValue("CreatedDate", NufarmBussinesRules.SharedClass.ServerDate)
            Me.grdBrand.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName)
            '.check apakah ada data brand untuk periode 
            Dim BrandID As String = Me.grdBrand.GetValue("BRAND_ID").ToString()
            Dim StartDate As DateTime = Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString())
            Dim EndDate As DateTime = Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())
            If Me.clsProgram.hasDataBrand(BrandID, StartDate, EndDate) Then
                Me.ShowMessageInfo("Current Brand has held  other program in the same periode")
                e.Cancel = True
            End If
            If Me.rdbOneRuleDiscToAllBrand.Checked = False And Me.rdbRuleDiscToCertainBrand.Checked = False Then
                Me.baseTooltip.Show("Please define rule", Me.grpTypeDiscount, 2500) : Me.grpTypeDiscount.Focus() : e.Cancel = True : Cursor = Cursors.Default : Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdProgressive_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdProgressive.AddingRecord
        Try
            Cursor = Cursors.WaitCursor
            Dim IsValidData As Boolean = True
            Dim upToPCt As Object = Me.grdProgressive.GetValue("MIN_TO_DATE")
            Dim discount As Object = Me.grdProgressive.GetValue("DISCOUNT")
            If IsNothing(upToPCt) Or IsDBNull(upToPCt) Then
                IsValidData = False
            ElseIf Convert.ToDecimal(upToPCt) <= 0 Then
                IsValidData = False
            End If
            If IsNothing(discount) Or IsDBNull(discount) Then
                IsValidData = False
            ElseIf Convert.ToDecimal(discount) <= 0 Then
                IsValidData = False
            End If
            If Not IsValidData Then
                Me.ShowMessageInfo("Please enter valid data")
                Cursor = Cursors.Default
                e.Cancel = True : Return
            End If
            ''check rule
            If Me.rdbOneRuleDiscToAllBrand.Checked = False And Me.rdbRuleDiscToCertainBrand.Checked = False Then
                Me.baseTooltip.Show("Please define rule", Me.grpTypeDiscount, 2500) : Me.grpTypeDiscount.Focus() : e.Cancel = True : Cursor = Cursors.Default : Return
            End If
            'check brandID
            Dim BrandID As String = ""
            If Me.rdbRuleDiscToCertainBrand.Checked Then
                If Not IsNothing(Me.grdBrand.DataSource) Then
                    If Me.grdBrand.RecordCount > 0 Then
                        If Not IsNothing(Me.grdBrand.SelectedItems) Then
                            If Me.grdBrand.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                                BrandID = Me.grdBrand.GetValue("BRAND_ID").ToString()
                            End If
                        End If
                    End If
                End If
                If BrandID = "" Then
                    e.Cancel = True
                    Me.ShowMessageError("Please select every brand for every rule entered") : Cursor = Cursors.Default : Return
                End If
            End If
            If BrandID <> "" Then
                Me.grdProgressive.SetValue("BRAND_ID", BrandID)
            End If
            Me.grdProgressive.SetValue("CreatedDate", NufarmBussinesRules.SharedClass.ServerDate)
            Me.grdProgressive.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName)
            'Me.grdBrand.SetValue("BRAND_ID", BrandID)
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdProgressive_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdProgressive.CellUpdated
        Try
            If Me.Isloadingrow Then : Return : End If
            If Me.grdProgressive.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                'check apakah data sudah dipakai
                Cursor = Cursors.WaitCursor
                Dim Periode As String = String.Format("{0:dd/MM/yyyy}-{1:dd/MM/yyyy}", Convert.ToDateTime(Me.dtPicFrom.Value), Convert.ToDateTime(Me.dtPicUntil.Value))
                'if me.clsProgram.hasDataUsed(Periode,me.grdProgressive.GetValue("BRAND_ID"
                Dim brandID As String = IIf(IsNothing(Me.grdProgressive.GetValue("BRAND_ID")), "", Me.grdProgressive.GetValue("BRAND_ID").ToString())
                If Me.clsProgram.hasDataUsed(Periode, brandID, True) Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    grdProgressive.CancelCurrentEdit()
                End If
                Me.grdProgressive.UpdateData()
            End If
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdProgressive_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdProgressive.DeletingRecord
        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Return
        End If
        If Me.grdProgressive.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            'check apakah data sudah dipakai
            Cursor = Cursors.WaitCursor
            Dim Periode As String = String.Format("{0:dd/MM/yyyy}-{1:dd/MM/yyyy}", Convert.ToDateTime(Me.dtPicFrom.Value), Convert.ToDateTime(Me.dtPicUntil.Value))
            'if me.clsProgram.hasDataUsed(Periode,me.grdProgressive.GetValue("BRAND_ID"
            Dim brandID As String = IIf(IsNothing(Me.grdProgressive.GetValue("BRAND_ID")), "", Me.grdProgressive.GetValue("BRAND_ID").ToString())
            If Me.clsProgram.hasDataUsed(Periode, brandID, True) Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                grdProgressive.CancelCurrentEdit()
            End If
            'Me.grdProgressive.UpdateData()
        End If
        Cursor = Cursors.Default

        e.Cancel = False
    End Sub

    Private Sub grdBrand_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdBrand.DeletingRecord
        Try
            Cursor = Cursors.WaitCursor
            Dim brandID As String = Me.grdBrand.GetValue("BRAND_ID").ToString()
            Dim Periode As String = String.Format("{0:dd/MM/yyyy}-{1:dd/MM/yyyy}", Convert.ToDateTime(Me.dtPicFrom.Value), Convert.ToDateTime(Me.dtPicUntil.Value))
            If Me.clsProgram.hasDataUsed(Periode, brandID, True) Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData) : e.Cancel = True : Me.Cursor = Cursors.Default : Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                Me.Cursor = Cursors.Default : e.Cancel = True
                Return
            End If
            If Not IsNothing(Me.grdProgressive.DataSource) Then
                If Not IsNothing(Me.DVProgressive) Then
                    Dim rows() As DataRow = Me.DS.Tables("T_Progressive").Select("BRAND_ID = '" & brandID & "'")
                    If rows.Length > 0 Then
                        For Each row As DataRow In rows
                            row.Delete()
                        Next
                    End If
                End If
            End If
            e.Cancel = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdData_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.TManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            If Me.TManager1.GridEX1.RecordCount <= 0 Then : Return : End If
            If Me.TManager1.GridEX1.SelectedItems Is Nothing Then : Return : End If
            If Me.TManager1.GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
                Me.ClearEntry(True)
                Return
            End If
            If Me.pnlEntry.Height = 0 Then
                Me.pnlEntry.Height = Me.pnlentryHeight
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.ButonClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If (TypeOf (sender) Is Button) Then
                Me.PageIndex = 1
            ElseIf (TypeOf (sender) Is Janus.Windows.EditControls.UIButton) Then
                Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                    Case "btnGoFirst" : Me.PageIndex = 1 : Case "btnGoPrevios" : Me.PageIndex -= 1
                    Case "btnNext" : Me.PageIndex += 1 : Case "btnGoLast" : Me.PageIndex = Me.TotalIndex
                End Select
            End If
            Me.GetData() : Me.SetOriginalCriteria()
        Catch ex As Exception
            Me.UndoCriteria() : MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_ChangesCriteria(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.ChangesCriteria
        Try
            If Not IsloadedForm Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            If Me.isUndoingCriteria Then : Return : End If
            Select Case Me.TManager1.btnCriteria.CompareOperator
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
            End Select
            Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
            'Me.SetOriginalCriteria()
        Catch ex As Exception
            Me.UndoCriteria()
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Criteria_Changed")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_CmbSelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.CmbSelectedIndexChanged
        Try
            If isLoadingCombo Then : Return : End If
            If Not Me.IsloadedForm Then : Return : End If
            Me.Cursor = Cursors.WaitCursor : If TManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            Select Case Me.TManager1.cbCategory.Text
                Case "BRAND_NAME"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
                Case "START_DATE", "END_DATE"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.DateTime
            End Select
            Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_TManager1_CmbSelectedIndexChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_DeleteGridRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles TManager1.DeleteGridRecord
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim FKApp_SDB As Integer = 0
            ''IIf(IsDBNull(Me.TManager1.GridEX1.GetValue("FKApp_SDB")), 0, Convert.ToInt32(Me.TManager1.GridEX1.GetValue("FKApp_SDB")))
            If IsDBNull(Me.TManager1.GridEX1.GetValue("FKApp_SDB")) Or IsNothing(Me.TManager1.GridEX1.GetValue("FKApp_SDB")) Then
            Else
                FKApp_SDB = Convert.ToInt32(Me.TManager1.GridEX1.GetValue("FKApp_SDB"))
            End If
            Dim FKApp_SDS As Integer = Convert.ToInt32(Me.TManager1.GridEX1.GetValue("FKApp_SDS"))
            If ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes Then
                Me.clsProgram.deleteDK(FKApp_SDS, FKApp_SDB)
                Me.TManager1.GridEX1.UpdateData()
                If (Me.TManager1.GridEX1.RecordCount <= 1) Then
                    Me.ClearEntry(True)
                Else

                End If
            Else
                e.Cancel = True : Return
            End If
            Me.btnEditRow.Checked = False
            Me.btnAddNew.Checked = False
            Me.mustReloadQuery = True
        Catch ex As Exception
            e.Cancel = True : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_TManager1_DeleteGridRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_GridCurrentCell_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.GridCurrentCell_Changed
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.Isloadingrow Then : Return : End If
            If Me.TManager1.GridEX1.DataSource Is Nothing Then
                Me.Isloadingrow = True : Me.isLoadingCombo = True : Me.ClearEntry(True)
                Me.Isloadingrow = False : Me.isLoadingCombo = False : Return
            End If
            If Me.TManager1.GridEX1.RecordCount <= 0 Then
                Me.Isloadingrow = True : Me.isLoadingCombo = True : Me.ClearEntry(True)
                Me.Isloadingrow = False : Me.isLoadingCombo = False : Return
            End If
            Me.Isloadingrow = True
            If Me.TManager1.GridEX1.RecordCount > 0 Then
                If Me.TManager1.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                    Me.pnlEntryAndFilter.Height = Me.pnlentryHeight
                    Me.InflateData()
                    Me.btnAddNew.Checked = False
                    Me.btnEditRow.Checked = True
                    Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
                Else
                    Me.ClearEntry(True)
                    Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                End If
            End If
            Me.Isloadingrow = False
            isLoadingCombo = False
        Catch ex As Exception
            isLoadingCombo = False
            Me.btnEditRow.Checked = False
            Me.Isloadingrow = False
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, "_TManager1_GridCurrentCell_Changed")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_gridKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TManager1.gridKeyDown
        If e.KeyCode = Keys.F7 Then
            Cursor = Cursors.WaitCursor
            Me.ExportData(Me.TManager1.GridEX1)
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub TManager1_TetxBoxKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TManager1.TetxBoxKeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                Me.Cursor = Cursors.WaitCursor
                Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_TextBoxKeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TManager1.TextBoxKeyPress
        If (Not Char.IsDigit(e.KeyChar)) And (Not Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub pnlEntry_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlEntry.Enter
        Me.AcceptButton = Nothing
    End Sub

    Private Sub TManager1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.Enter
        Me.AcceptButton = Me.TManager1.btnSearch
    End Sub

    Private Sub DKNational_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Isloadingrow = True
        If Not IsNothing(Me.DS) Then
            If Me.DS.HasChanges() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    Try
                        Cursor = Cursors.WaitCursor
                        Me.SaveData()
                    Catch ex As Exception
                    Finally
                        Cursor = Cursors.Default
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub DKNational_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.pnlentryHeight = Me.pnlEntry.Height

            Me.LoadData()
            ''fil dataview
            Me.ReadAccess()
            Me.pnlEntryAndFilter.Height = 0
            isLoadingCombo = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        Finally
            Me.hasLoadForm = True
        End Try
    End Sub

    Private Sub grdBrand_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdBrand.RecordAdded
        If Not Me.IsloadedForm Then : Return : End If
        Me.grdBrand.UpdateData()
    End Sub

    Private Sub grdProgressive_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdProgressive.RecordAdded
        If Not Me.IsloadedForm Then : Return : End If
        Me.grdProgressive.UpdateData()
    End Sub

    Private Sub grdProgressive_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdProgressive.RecordsDeleted
        Me.grdProgressive.UpdateData()
    End Sub

    Private Sub grdBrand_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdBrand.RecordsDeleted
        Me.grdBrand.UpdateData()
    End Sub
    Private Sub CreateTablebrand()
        Dim dtTableBrandGrid As New DataTable("T_BrandGrid")
        With dtTableBrandGrid
            .Columns.Add("IDApp", Type.GetType("System.Int32"))
            .Columns.Add("FKApp", Type.GetType("System.Int32"))
            .Columns.Add("BRAND_ID", Type.GetType("System.String"))
            .Columns.Add("CreatedDate", Type.GetType("System.DateTime"))
            .Columns.Add("CreatedBy", Type.GetType("System.String"))
            dtTableBrandGrid.AcceptChanges()
        End With
        If IsNothing(Me.DS) Then
            Me.DS = New DataSet("DSSkema")
            Me.DS.Tables.Add(dtTableBrandGrid)
        Else
            If Not Me.DS.Tables.Contains("T_BrandGrid") Then
                Me.DS.Tables.Add(dtTableBrandGrid)
            End If
        End If
        Me.DVBrandGrid = DS.Tables("T_BrandGrid").DefaultView
        Me.DS.AcceptChanges()
    End Sub
    Private Sub CreateTableProgDisc()
        Dim dtTableProgDisc As New DataTable("T_Progressive")
        With dtTableProgDisc
            .Columns.Add("IDApp", Type.GetType("System.Int32"))
            .Columns.Add("MIN_TO_DATE", Type.GetType("System.Int32"))
            .Columns.Add("DISCOUNT", Type.GetType("System.Decimal"))
            .Columns.Add("BRAND_ID", Type.GetType("System.String"))
            .Columns.Add("FKApp_SDS", Type.GetType("System.Int32"))
            .Columns.Add("FKApp_SDB", Type.GetType("System.Int32"))
            .Columns.Add("CreatedDate", Type.GetType("System.DateTime"))
            .Columns.Add("CreatedBy", Type.GetType("System.String"))
            .Columns.Add("ModifiedDate", Type.GetType("System.DateTime"))
            .Columns.Add("ModifiedBy", Type.GetType("System.String"))
        End With
        dtTableProgDisc.AcceptChanges()
        If IsNothing(Me.DS) Then
            Me.DS = New DataSet("DSSkema")
            Me.DS.Tables.Add(dtTableProgDisc)
        Else
            If Not Me.DS.Tables.Contains("T_Progressive") Then
                Me.DS.Tables.Add(dtTableProgDisc)
            End If
        End If
        Me.DVProgressive = DS.Tables("T_Progressive").DefaultView
        Me.DS.AcceptChanges()
    End Sub
    Private Sub rdbAllProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAllProduct.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.Isloadingrow = True
            If Not IsNothing(Me.DS) Then
                If Me.DS.HasChanges Then
                    If Me.ShowConfirmedMessage("Data has changed !" & vbCrLf & "If you continue, all changes will be discarded" & vbCrLf & "Discard all changes ?") = Windows.Forms.DialogResult.Yes Then
                        Me.grdBrand.Enabled = False
                        Me.Isloadingrow = True
                        'If Not IsNothing(Me.DVBrandGrid) Then
                        '    Me.DVBrandGrid.Table.RejectChanges()
                        'End If
                        'If Not IsNothing(Me.DVProgressive) Then
                        '    Me.DVProgressive.Table.RejectChanges()
                        'End If
                        Me.DS.Clear() : Me.DS.AcceptChanges()
                    Else
                        Me.rdbAllProduct.Checked = False
                        If seedProductToGive = ProductToGive.CertainBrand Then
                            Me.rdbCertainProducts.Checked = True
                        End If
                    End If
                Else
                    Me.grdBrand.Enabled = False
                    Me.grdBrand.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.CreateTableProgDisc()
                    Me.grdProgressive.SetDataBinding(Me.DVProgressive, "")
                End If
            Else
                Me.grdBrand.Enabled = False
                Me.grdBrand.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                Me.CreateTableProgDisc()
                Me.grdProgressive.SetDataBinding(Me.DVProgressive, "")
            End If
            'Me.rdbOneRuleDiscToAllBrand.Checked = False
            'Me.rdbRuleDiscToCertainBrand.Checked = False
            'Me.seedProductRule = ProductRule.None
            If rdbAllProduct.Checked Then
                Me.seedProductToGive = ProductToGive.AllProduct
            End If
            Me.Isloadingrow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Isloadingrow = False
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub rdbCertainProducts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCertainProducts.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.Isloadingrow = True
            If Not IsNothing(Me.DS) Then
                If Me.DS.HasChanges Then
                    If Me.ShowConfirmedMessage("Data has changed !" & vbCrLf & "If you continue, all changes will be discarded" & vbCrLf & "Discard all changes ?") = Windows.Forms.DialogResult.Yes Then
                        'If Not IsNothing(Me.DVBrandGrid) Then
                        '    Me.DS.Clear() : Me.DS.AcceptChanges()
                        'End If
                        'If Not IsNothing(Me.DVProgressive) Then
                        '    Me.DVProgressive.Table.RejectChanges()
                        'End If
                        Me.DS.Clear() : Me.DS.AcceptChanges()
                    Else
                        Me.rdbCertainProducts.Checked = False
                        If Me.seedProductToGive = ProductToGive.AllProduct Then
                            Me.rdbAllProduct.Checked = True
                        End If
                    End If
                Else
                    Me.grdBrand.Enabled = True
                    '' fill grid brand
                    'get databrand
                    Me.DVBrand = Me.clsProgram.getDataBrand(Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), False).DefaultView
                    Me.CreateTablebrand()
                    Me.CreateTableProgDisc()
                    Me.grdProgressive.SetDataBinding(Me.DVProgressive, "")
                    Me.grdProgressive.DropDowns("BRAND").SetDataBinding(Me.DVBrand, "")
                    Me.grdBrand.SetDataBinding(DVBrandGrid, "")
                    Me.grdBrand.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.grdBrand.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.grdBrand.DropDowns("Brand").SetDataBinding(Me.DVBrand, "")
                    Me.grdBrand.Focus()
                    Me.grdBrand.MoveToNewRecord()
                End If
            Else
                Me.grdBrand.Enabled = True
                '' fill grid brand
                'get databrand
                Me.DVBrand = Me.clsProgram.getDataBrand(Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), False).DefaultView
                Me.CreateTablebrand()
                Me.CreateTableProgDisc()
                Me.grdProgressive.SetDataBinding(Me.DVProgressive, "")
                Me.grdBrand.SetDataBinding(DVBrandGrid, "")
                Me.grdBrand.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grdBrand.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True

                Me.grdBrand.DropDowns("Brand").SetDataBinding(Me.DVBrand, "")
                Me.grdBrand.Focus()
                Me.grdBrand.MoveToNewRecord()
            End If

            'Me.rdbOneRuleDiscToAllBrand.Checked = False
            'Me.rdbRuleDiscToCertainBrand.Checked = False
            'Me.seedProductRule = ProductRule.None
            If Me.rdbCertainProducts.Checked Then
                Me.seedProductToGive = ProductToGive.CertainBrand
            End If

            Me.Cursor = Cursors.Default
            Me.Isloadingrow = False
        Catch ex As Exception
            Me.Isloadingrow = False
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try

    End Sub

    'Private Sub grpTypeDiscount_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpTypeDiscount.Enter
    '    If Not Me.hasLoadForm Then : Return : End If
    '    If Me.rdbAllProduct.Checked = False And Me.rdbCertainProducts.Checked = False Then
    '        Me.baseTooltip.Show("Please define product to support", Me.grpProductToGive, 2500) : Me.grpProductToGive.Focus() : Return
    '    End If
    'End Sub

    Private Sub rdbOneRuleDiscToAllBrand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbOneRuleDiscToAllBrand.Click

        If Not IsNothing(Me.DS) Then
            If Me.DS.HasChanges Then
                If Not IsNothing(Me.DS.Tables("T_Progressive").GetChanges()) Then
                    If Me.ShowConfirmedMessage("Data has changed !" & vbCrLf & "If you continue, all changes will be discarded" & vbCrLf & "Discard all changes ?") = Windows.Forms.DialogResult.Yes Then
                        If Not IsNothing(Me.DVProgressive) Then
                            Me.DVProgressive.Table.Clear() : Me.DVProgressive.Table.AcceptChanges()
                        End If
                    Else
                        If Me.seedProductRule = ProductRule.toCertainProduct Then
                            Me.rdbRuleDiscToCertainBrand.Checked = True
                        End If
                    End If
                End If
            End If
        End If
        If Me.rdbOneRuleDiscToAllBrand.Checked Then
            Me.seedProductRule = ProductRule.toAllProduct
        End If
        Me.Isloadingrow = False
    End Sub

    Private Sub rdbRuleDiscToCertainBrand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbRuleDiscToCertainBrand.Click
        If Me.rdbAllProduct.Checked Then
            Me.baseTooltip.Show("certain brand/All brand only for certain product", Me.grpTypeDiscount, 2500)
            rdbRuleDiscToCertainBrand.Checked = False
            If Me.seedProductRule = ProductRule.toAllProduct Then
                Me.rdbOneRuleDiscToAllBrand.Checked = True
            End If
            Me.grpTypeDiscount.Focus()
            Return
        ElseIf Me.rdbAllProduct.Checked = False And Me.rdbCertainProducts.Checked = False Then
            Me.baseTooltip.Show("Please choose product to support", Me.grpProductToGive, 2500) : Me.grpProductToGive.Focus()
            Me.rdbRuleDiscToCertainBrand.Checked = False
            Return
        End If
        If Not IsNothing(Me.DS) Then
            If Me.DS.HasChanges Then
                If Not IsNothing(Me.DS.Tables("T_Progressive").GetChanges()) Then
                    If Me.ShowConfirmedMessage("Data has changed !" & vbCrLf & "If you continue, all changes will be discarded" & vbCrLf & "Discard all changes ?") = Windows.Forms.DialogResult.Yes Then
                        If Not IsNothing(Me.DVProgressive) Then
                            Me.DVProgressive.Table.Clear() : Me.DVProgressive.Table.AcceptChanges() ' Me.DVProgressive.Table.RejectChanges()
                        End If
                    Else
                        If Me.seedProductRule = ProductRule.toAllProduct Then
                            Me.rdbOneRuleDiscToAllBrand.Checked = True
                        End If
                    End If
                End If
            End If
        End If
        If Me.rdbRuleDiscToCertainBrand.Checked Then
            Me.seedProductRule = ProductRule.toCertainProduct
        End If
        Me.Isloadingrow = False
    End Sub

    Private Sub grdProgressive_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdProgressive.KeyDown
        If e.KeyCode = Keys.F2 Then
            If Not IsNothing(Me.DVProgressive) Then
                Me.DVProgressive.RowFilter = ""
            End If
        ElseIf e.KeyCode = Keys.F7 Then
            Me.ExportData(Me.grdProgressive)
        End If
    End Sub

    Private Sub grdBrand_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdBrand.KeyDown
        If e.KeyCode = Keys.F7 Then
            Me.ExportData(Me.grdBrand)
        End If
    End Sub

    Private Sub grdBrand_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdBrand.CellUpdated
        If Me.Isloadingrow Then : Return : End If
        If Me.grdBrand.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
            Me.grdBrand.CancelCurrentEdit()
        End If

    End Sub
End Class
