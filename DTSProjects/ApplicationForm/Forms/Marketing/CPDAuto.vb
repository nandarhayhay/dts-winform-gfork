Imports Nufarm.Domain
Imports NufarmBussinesRules.Program
Public Class CPDAuto
    Private m_Criteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private OriginalCriteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private isUndoingCriteria As Boolean = False
    Private m_DataType As NufarmBussinesRules.common.Helper.DataTypes = NufarmBussinesRules.common.Helper.DataTypes.VarChar
    Private Mode As NufarmBussinesRules.common.Helper.SaveMode = NufarmBussinesRules.common.Helper.SaveMode.None
    Private hasLoadForm As Boolean = False
    Private Isloadingrow As Boolean = True
    Private PageIndex As Int32 = 0, PageSize As Integer = 0, TotalIndex As Integer = 0, RowCount As Integer = 0
    Private isLoadingCombo As Boolean = False
    Private OriginalData As New NufarmBussinesRules.common.dataTManager()
    Friend CMain As Main = Nothing
    Private IsSavingData As Boolean = False
    Private m_clsProgram As NufarmBussinesRules.Program.BrandPackInclude = Nothing
    Private pnlentryHeight As Integer = 0
    Private DVProgressive As DataView = Nothing
    Private DVBrand As DataView = Nothing
    Private DVBrandGrid As DataView = Nothing
    Private _DCPDAuto As New DCPDAuto()
    Private _clsCPDAuto As BRCPDAuto
    Private DefautStartPeriode As Object
    Private DefaultEndPeriode As Object
    'Private IDApp As Integer = 0
    Private WithEvents frmLookUP As LookUp
    Private listBrandPack As New List(Of String)
    Private tblProduct As DataTable = Nothing
    Public ReadOnly Property clsCPDAuto() As BRCPDAuto
        Get
            If IsNothing(Me._clsCPDAuto) Then
                Me._clsCPDAuto = New BRCPDAuto()
            End If
            Return _clsCPDAuto
        End Get
    End Property
    Private Function HasChangeData() As Boolean
        'check table Product
        Dim BFind As Boolean = False
        Dim Rows() As DataRow = Nothing
        If Not IsNothing(Me._DCPDAuto.TProduct) Then
            Rows = _DCPDAuto.TProduct.Select("", "", Data.DataViewRowState.Added Or Data.DataViewRowState.ModifiedOriginal Or Data.DataViewRowState.Deleted)
            BFind = (Rows.Length > 0)
        End If
        If BFind Then : Return True : End If
        If Not IsNothing(Me._DCPDAuto.TDiscProgress) Then
            Rows = _DCPDAuto.TDiscProgress.Select("", "", Data.DataViewRowState.Added Or Data.DataViewRowState.ModifiedOriginal Or Data.DataViewRowState.Deleted)
            BFind = (Rows.Length > 0)
        End If
        If BFind Then : Return True : End If
        If Not IsNothing(Me._DCPDAuto.TDiscTerms) Then
            Rows = _DCPDAuto.TDiscTerms.Select("", "", Data.DataViewRowState.Added Or Data.DataViewRowState.ModifiedOriginal Or Data.DataViewRowState.Deleted)
            BFind = (Rows.Length > 0)
        End If
        Return BFind
    End Function
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.CPDAuto Then
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            Else
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_INSERT.CPDAuto Then
                Me.btnAddNew.Visible = False
            Else
                Me.btnAddNew.Visible = True
                Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.CPDAuto Then
                Me.btnEditRow.Visible = False
            Else
                Me.btnEditRow.Visible = True
            End If
        End If
    End Sub
    Private Function ValidateData() As Boolean
        If Me.dtPicFrom.Text = "" Then
            Me.baseTooltip.Show("Please define start periode", Me.dtPicFrom, 2500) : Me.dtPicFrom.Focus() : Return False
        ElseIf Me.dtPicUntil.Text = "" Then
            Me.baseTooltip.Show("Please define start periode", Me.dtPicUntil, 2500) : Me.dtPicFrom.Focus() : Return False
        End If
        Dim dtStartVal As New DateTime(Me.dtPicUntil.Value.AddYears(-1).Year, 8, 1)
        If Convert.ToDateTime(dtPicFrom.Value.ToShortDateString()) <> Convert.ToDateTime(dtStartVal.ToShortDateString()) Then
            Me.baseTooltip.Show("Please enter valid start periode", Me.dtPicFrom, 2500) : Me.dtPicFrom.Focus() : Return False
        End If
        If Me.txtDescriptions.Text = String.Empty Or Me.txtDescriptions.Text.Length <= 3 Then
            Me.baseTooltip.Show("Please enter program description", Me.txtDescriptions, 2500) : Me.txtDescriptions.Focus() : Return False
        End If
        If Me.grdBrand.DataSource Is Nothing Then : Me.baseTooltip.Show("Please enter product list", Me.grdBrand, 2500) : Me.grdBrand.Focus() : Return False : End If
        If Me.grdBrand.RecordCount <= 0 Then : Me.baseTooltip.Show("Please enter product list", Me.grdBrand, 2500) : Me.grdBrand.Focus() : Return False : End If

        If Me.grdProgressive.DataSource Is Nothing Then : Me.baseTooltip.Show("Please enter progressive discount", Me.grdProgressive, 2500) : Me.grdProgressive.Focus() : Return False : End If
        If Me.grdProgressive.RecordCount <= 0 Then : Me.baseTooltip.Show("Please enter progressive discount", Me.grdProgressive, 2500) : Me.grdProgressive.Focus() : Return False : End If
        Return True
    End Function
    Private Function SaveData() As Boolean
        If Me.pnlEntry.Height <= 0 Then : Return False : End If
        If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None Then : Return False : End If
        If Not Me.ValidateData Then
            Return False
        End If
        Me._DCPDAuto.StartPeriode = Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString())
        Me._DCPDAuto.EndPeriode = Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString())
        Me._DCPDAuto.DescriptionApp = Me.txtDescriptions.Text.TrimStart().TrimEnd()
        Me.clsCPDAuto.SaveData(Me._DCPDAuto, Mode, False)

        'me.clsCPDAuto.SaveData(me._DCPDAuto,mode,
        'me.clsCPDAuto.SaveData(me._DCPDAuto,me.Mode,
        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None
        Return True
    End Function
    Private Sub ExportData(ByVal grid As Janus.Windows.GridEX.GridEX)
        Dim SV As New SaveFileDialog()
        With SV
            .Title = "Define the location File"
            .InitialDirectory = System.Environment.SpecialFolder.MyDocuments
            .RestoreDirectory = True
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
    Private Sub ClearEntry()
        If Not Me.Isloadingrow Then : Me.Isloadingrow = True : End If
        Me.grdBrand.SetDataBinding(Nothing, "")
        Me.grdProgressive.SetDataBinding(Nothing, "")
        Me.grdTermDisc.SetDataBinding(Nothing, "")
        Me._DCPDAuto = New DCPDAuto()
        Me.dtPicFrom.Value = Me.DefautStartPeriode
        Me.dtPicUntil.Value = Me.DefaultEndPeriode
        Me.txtDescriptions.Text = ""
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
        Me.TManager1.GridEX1.RootTable.Columns("ROW_NUM").Caption = "NO"
        Me.TManager1.GridEX1.RootTable.Columns("ROW_NUM").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

        Me.TManager1.GridEX1.RootTable.Columns("IDApp").Visible = False
        Me.TManager1.GridEX1.RootTable.Columns("IDApp").ShowInFieldChooser = False

        If (Me.FilterEditor1.Visible) And (Not IsNothing(Me.FilterEditor1.SourceControl)) Then
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
        Else
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        End If
        Me.TManager1.GridEX1.RootTable.Columns("CreatedDate").Caption = "CREATED_DATE"
        Me.TManager1.GridEX1.RootTable.Columns("CreatedBy").Caption = "CREATED_BY"
        Me.TManager1.GridEX1.AutoSizeColumns(Me.TManager1.GridEX1.RootTable)
    End Sub

    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String, ByVal HasChangeCriteriaSearch As Boolean)
        Dim Dv As DataView = Nothing : Me.Isloadingrow = True
        Dv = Me.clsCPDAuto.PopulateQuery(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, _
         Me.m_Criteria, Me.m_DataType)

        If Not IsNothing(Dv) Then
            If (Dv.Count > 0) Then
                Me.TManager1.GridEX1.DataSource = Dv : Me.TManager1.GridEX1.RetrieveStructure() : Me.FormatDataGrid()
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
        If Me.hasLoadForm = True Then
            Me.Isloadingrow = False
        End If
    End Sub
    Friend Sub LoadData()
        isLoadingCombo = True : Isloadingrow = True
        'get DefaultStartDate
        Dim Periode() As Object = Me.clsCPDAuto.getDefaultStartDate(False)
        Me.tblProduct = Me.clsCPDAuto.getActiveProduct(False)
        Me.dtPicFrom.Value = Periode(0)
        Me.dtPicUntil.Value = Periode(1)
        Me.DefautStartPeriode = Convert.ToDateTime(Periode(0))
        Me.DefaultEndPeriode = Convert.ToDateTime(Periode(1))
        With Me.TManager1.cbCategory
            .Items.Add("PROGRAM_DESC") : .Items.Add("START_PERIODE") : .Items.Add("END_PERIODE")
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
                .SearchBy = "PROGRAM_DESC"
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
            SearchBy = "PROGRAM_DESC"
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
    Private Sub InflateData()
        If Not Me.Isloadingrow Then : Me.Isloadingrow = True : End If
        Me._DCPDAuto.IDApp = Convert.ToInt32(Me.TManager1.GridEX1.GetValue("IDApp"))
        Me._DCPDAuto.StartPeriode = Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("START_PERIODE"))
        Me._DCPDAuto.EndPeriode = Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("END_PERIODE"))
        Me.txtDescriptions.Text = Me.TManager1.GridEX1.GetValue("PROGRAM_DESC").ToString()
        'get dataset
        Dim Ds As DataSet = Me.clsCPDAuto.getDS(Me._DCPDAuto.IDApp, True)
        With Me._DCPDAuto

            .TProduct = Ds.Tables("PRODUCT_INCLUDED").Copy()
            .TProduct.AcceptChanges()
            Dim DV As DataView = Me.tblProduct.DefaultView()
            Dim DV1 As DataView = .TProduct.DefaultView()
            DV.Sort = "BRANDPACK_NAME ASC"
            Me.grdBrand.SetDataBinding(DV1, "")
            Me.grdBrand.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdBrand.DropDowns("BRANDPACK").SetDataBinding(DV, "")
            'Me.grdBrand.DropDowns("BRANDPACK").Columns("BRANDPACK_ID").ShowRowSelector = True
            'Me.grdBrand.DropDowns("BRANDPACK").Columns("BRANDPACK_ID").UseHeaderSelector = True

            .TDiscProgress = Ds.Tables("DISCOUNT_PROGRESSIVE").Copy()
            .TDiscProgress.AcceptChanges()
            Me.grdProgressive.SetDataBinding(.TDiscProgress.DefaultView(), "")
            Me.grdProgressive.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True

            .TDiscTerms = Ds.Tables("TERMS_OF_PO").Copy()
            .TDiscTerms.AcceptChanges()
            Me.grdTermDisc.SetDataBinding(.TDiscTerms.DefaultView(), "")
            Me.grdTermDisc.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
        End With

    End Sub
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = DirectCast(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnShowFieldChooser"
                    If Not IsNothing(Me.TManager1.GridEX1.DataSource) Then
                        If Me.TManager1.GridEX1.RootTable.Columns.Count > 0 Then
                            Me.TManager1.GridEX1.ShowFieldChooser()
                        End If
                    End If
                Case "btnSettingGrid"
                    If Not IsNothing(Me.TManager1.GridEX1.DataSource) Then
                        If Me.TManager1.GridEX1.RootTable.Columns.Count > 0 Then
                            Dim SetGrid As New SettingGrid()
                            SetGrid.Grid = Me.TManager1.GridEX1
                            SetGrid.ShowDialog()
                        End If
                    End If
                Case "btnCustomFilter"
                    If Not IsNothing(Me.TManager1.GridEX1.DataSource) Then
                        If Me.TManager1.GridEX1.RootTable.Columns.Count > 0 Then
                            Me.pnlEntryAndFilter.Visible = True
                            Me.FilterEditor1.Visible = True
                            Me.FilterEditor1.SortFieldList = False
                            Me.FilterEditor1.SourceControl = Me.TManager1.GridEX1
                            Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                            Me.TManager1.GridEX1.RemoveFilters()
                            'Me.TManager1.BringToFront()
                            Me.plGridData.BringToFront()
                        End If
                    End If
                Case "btnFilterEqual"
                    If Not IsNothing(Me.TManager1.GridEX1.DataSource) Then
                        If Me.TManager1.GridEX1.RootTable.Columns.Count > 0 Then
                            Me.pnlEntryAndFilter.Visible = False
                            Me.FilterEditor1.SourceControl = Nothing
                            Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.TManager1.GridEX1.RemoveFilters()
                        End If
                    End If
                Case "btnExport"
                    If Not IsNothing(Me.TManager1.GridEX1.DataSource) Then
                        If Me.TManager1.GridEX1.RootTable.Columns.Count > 0 Then
                            ExportData(Me.TManager1.GridEX1)
                        End If
                    End If
                Case "btnRefresh"
                    If Me.btnAddNew.Checked = True Or Me.btnEditRow.Checked = True Then
                        Me.ShowMessageInfo("Please close entry area to refresh/reload data")
                        Return
                    End If
                    'reload Data
                    Me.GetData() : Me.SetOriginalCriteria()
                Case "btnAddNew"
                    Me.Isloadingrow = True
                    If Me.btnAddNew.Checked Then
                        Me.ClearEntry()
                        Me.pnlEntry.Height = 0
                        Me.btnAddNew.Checked = False
                        Me.btnEditRow.Checked = False

                        Me.btnSave.Enabled = False
                        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None
                    Else
                        Me.pnlEntry.Height = Me.pnlentryHeight
                        Me.ClearEntry()
                        ''get table penginputan
                        Dim DS As DataSet = Me.clsCPDAuto.getDS(0, True)
                        With Me._DCPDAuto

                            .TProduct = DS.Tables("PRODUCT_INCLUDED").Copy()
                            .TProduct.AcceptChanges()
                            Dim DV As DataView = Me.tblProduct.DefaultView()
                            Dim DV1 As DataView = .TProduct.DefaultView()
                            DV.Sort = "BRANDPACK_NAME ASC"
                            Me.grdBrand.SetDataBinding(DV1, "")
                            Me.grdBrand.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.grdBrand.DropDowns("BRANDPACK").SetDataBinding(DV, "")
                            'Me.grdBrand.DropDowns("BRANDPACK").Columns("BRANDPACK_ID").ShowRowSelector = True
                            'Me.grdBrand.DropDowns("BRANDPACK").Columns("BRANDPACK_ID").UseHeaderSelector = True

                            .TDiscProgress = DS.Tables("DISCOUNT_PROGRESSIVE").Copy()
                            .TDiscProgress.AcceptChanges()
                            Me.grdProgressive.SetDataBinding(.TDiscProgress.DefaultView(), "")
                            Me.grdProgressive.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True

                            .TDiscTerms = DS.Tables("TERMS_OF_PO").Copy()
                            .TDiscTerms.AcceptChanges()
                            Me.grdTermDisc.SetDataBinding(.TDiscTerms.DefaultView(), "")
                            Me.grdTermDisc.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                        End With
                        Me.btnAddNew.Checked = True
                        Me.btnEditRow.Checked = False
                        Me.dtPicFrom.ReadOnly = False
                        Me.dtPicUntil.ReadOnly = False
                        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                        Me.btnSave.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.CPDAuto
                        Me.btnEditRow.Enabled = False
                    End If

                Case "btnEditRow"
                    Me.Isloadingrow = True
                    If Me.btnEditRow.Checked Then
                        Me.ClearEntry()
                        Me.pnlEntry.Height = 0
                        Me.btnAddNew.Checked = False
                        Me.btnEditRow.Checked = False
                        Me.btnSave.Enabled = False
                        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None
                        Me.Cursor = Cursors.Default
                        Return
                    Else
                        If Me.TManager1.GridEX1.DataSource Is Nothing Then
                            Me.Isloadingrow = True : Me.ClearEntry() : Me.btnEditRow.Checked = False
                            Me.Isloadingrow = False : Me.Cursor = Cursors.Default : Return
                        End If
                        If Me.TManager1.GridEX1.RecordCount <= 0 Then
                            Me.Isloadingrow = True : Me.ClearEntry() : Me.btnEditRow.Checked = False
                            Me.Isloadingrow = False : Me.Cursor = Cursors.Default : Return
                        End If
                        If Me.TManager1.GridEX1.RecordCount > 0 Then
                            If Me.TManager1.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                                Me.pnlEntry.Height = Me.pnlentryHeight
                                Me.InflateData() : Me.btnEditRow.Checked = True : Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
                            Else
                                Me.Isloadingrow = True : Me.ClearEntry() : Me.btnEditRow.Checked = False
                                Me.Isloadingrow = False : Me.Cursor = Cursors.Default : Return
                            End If
                        End If
                        Me.btnAddNew.Checked = False
                    End If
                    Me.btnSave.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.CPDAuto
                    Me.Isloadingrow = False
                Case "btnSave"
                    If Me.pnlEntry.Height <> 0 Then
                        If Not Me.SaveData() Then : Me.Isloadingrow = False
                            Me.Cursor = Cursors.Default
                            Return : End If
                        Me.PageIndex = IIf((Me.TotalIndex <= 0), 1, Me.TotalIndex)
                        Me.Isloadingrow = True
                        Me.GetData()
                        Me.ClearEntry()
                    End If
            End Select
            Me.Isloadingrow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
            Me.Isloadingrow = False
        End Try
    End Sub

    Private Sub grdBrand_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdBrand.DeletingRecord
        If Me.Isloadingrow Then : Return : End If
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim FKCOde As Object = Me.grdBrand.GetValue("FKCode")
            Dim BrandPackID As String = Me.grdBrand.GetValue("BRANDPACK_ID").ToString()
            If Not IsNothing(FKCOde) And Not IsDBNull(FKCOde) Then
                If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
                    If Me.clsCPDAuto.hasGeneratedDisc(BrandPackID, CInt(FKCOde), True) Then  ''chek keberadaaan data di saving discount
                        e.Cancel = True
                        Me.Cursor = Cursors.Default : Me.ShowMessageInfo(Me.MessageDataHasExisted) : Return
                    End If
                End If
            End If
            e.Cancel = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            e.Cancel = True
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try

    End Sub

    Private Sub grdBrand_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdBrand.AddingRecord
        Try
            'check apakah data sudah ada di dataview 
            If Me.grdBrand.GetValue("BRANDPACK_ID") Is DBNull.Value Or Me.grdBrand.GetValue("BRANDPACK_ID") Is Nothing Then
                Me.ShowMessageInfo("Please define branpack") : e.Cancel = True : Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim BrandPackID As String = Me.grdBrand.GetValue("BRANDPACK_ID").ToString()
            Dim row() As DataRow = Me._DCPDAuto.TProduct.Select("BRANDPACK_ID = '" & BrandPackID & "'", "", Data.DataViewRowState.Added Or Data.DataViewRowState.OriginalRows Or Data.DataViewRowState.CurrentRows Or Data.DataViewRowState.Unchanged)
            If row.Length > 0 Then
                Me.ShowMessageInfo("Product has existed !") : e.Cancel = True : Me.Cursor = Cursors.Default : Return
            End If
            Me.grdBrand.SetValue("CreatedDate", NufarmBussinesRules.SharedClass.ServerDate)
            Me.grdBrand.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName)
            e.Cancel = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            e.Cancel = True
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub

    Private Sub grdProgressive_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdProgressive.AddingRecord
        If Me.grdProgressive.DataSource Is Nothing Then : Return : End If
        If Me.Isloadingrow Then : Return : End If
        If Not Me.hasLoadForm Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor
            ''check morethan equal value
            If IsNothing(Me.grdProgressive.GetValue("UP_TO_PCT")) Or IsDBNull(Me.grdProgressive.GetValue("UP_TO_PCT")) Then
                Me.ShowMessageInfo("Please enter MoreThanOrExqual terms")
                e.Cancel = True : Cursor = Cursors.Default : Return
            ElseIf Convert.ToDecimal(Me.grdProgressive.GetValue("UP_TO_PCT")) <= 0 Then
                Me.ShowMessageInfo("Please enter value for MoreThanOrExqual terms")
                e.Cancel = True : Cursor = Cursors.Default : Return
            End If
            ''chek discount
            If IsNothing(Me.grdProgressive.GetValue("DISCOUNT")) Or IsDBNull(Me.grdProgressive.GetValue("DISCOUNT")) Then
                Me.ShowMessageInfo("Please enter the value for discount")
                e.Cancel = True : Cursor = Cursors.Default : Return
            ElseIf Convert.ToDecimal(Me.grdProgressive.GetValue("DISCOUNT")) <= 0 Then
                Me.ShowMessageInfo("Please enter the value for discount")
                e.Cancel = True : Cursor = Cursors.Default : Return
            End If
            'Dim DecUptoPct As Integer = Convert.ToDecimal(Me.grdProgressive.GetValue("UP_TO_PCT"))
            'Dim decDiscount As Decimal = Convert.ToDecimal(Me.grdProgressive.GetValue("DISCOUNT"))
            'Dim DV As DataView = CType(grdProgressive.DataSource, DataView).ToTable().Copy().DefaultView()
            'DV.RowFilter = "UP_TO_PCT = " & DecUptoPct.ToString() & " AND DISCOUNT = " & decDiscount.ToString()
            'If DV.Count > 0 Then
            '    Me.ShowMessageInfo(Me.MessageDataHasExisted) : e.Cancel = True : Cursor = Cursors.Default : Return
            'End If
            Me.grdProgressive.SetValue("CreatedDate", NufarmBussinesRules.SharedClass.ServerDate)
            Me.grdProgressive.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName)
            e.Cancel = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageError(ex.Message)
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdProgressive_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdProgressive.DeletingRecord
        If Me.grdProgressive.DataSource Is Nothing Then : Return : End If
        If Me.Isloadingrow Then : Return : End If
        If Not Me.hasLoadForm Then : Return : End If
        If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
            Try
                Dim CreatedDate As System.DateTime = Convert.ToDateTime(Me.grdTermDisc.GetValue("CreatedDate"))
                If DateDiff(DateInterval.Day, CreatedDate, DateTime.Now, FirstDayOfWeek.System, FirstWeekOfYear.System) > 3 Then
                    e.Cancel = True
                    Me.ShowMessageError("Can not delete data over 3 days input") : e.Cancel = True : Return
                End If
                Cursor = Cursors.WaitCursor
                Dim FKCode As Integer = Convert.ToInt32(Me.grdTermDisc.GetValue("FKCode"))
                If Me.clsCPDAuto.hasGeneratedDisc(String.Empty, FKCode, False) Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData) : e.Cancel = True : Cursor = Cursors.Default : Return
                End If
                If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True : Cursor = Cursors.Default : Return
                End If
                e.Cancel = False
                Cursor = Cursors.Default
            Catch ex As Exception
                e.Cancel = True
                Me.ShowMessageError(ex.Message)
                Cursor = Cursors.Default
            End Try
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub grdProgressive_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdProgressive.RecordAdded
        grdProgressive.UpdateData()
    End Sub

    Private Sub grdProgressive_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdProgressive.RecordsDeleted
        grdProgressive.UpdateData()
    End Sub

    Private Sub grdTermDisc_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdTermDisc.AddingRecord
        If Me.grdTermDisc.DataSource Is Nothing Then : Return : End If
        If Me.Isloadingrow Then : Return : End If
        If Not Me.hasLoadForm Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor
            ''check morethan equal value
            If IsNothing(Me.grdTermDisc.GetValue("MAX_TO_DATE")) Or IsDBNull(Me.grdTermDisc.GetValue("MAX_TO_DATE")) Then
                Me.ShowMessageInfo("Please enter MoreThanOrExqual terms")
                e.Cancel = True : Cursor = Cursors.Default : Return
            ElseIf Convert.ToInt32(Me.grdTermDisc.GetValue("MAX_TO_DATE")) <= 0 Then
                Me.ShowMessageInfo("Please enter value for MoreThanOrExqual terms")
                e.Cancel = True : Cursor = Cursors.Default : Return
            End If
            ''chek discount
            If IsNothing(Me.grdTermDisc.GetValue("DISCOUNT")) Or IsDBNull(Me.grdTermDisc.GetValue("DISCOUNT")) Then
                Me.ShowMessageInfo("Please enter the value for discount")
                e.Cancel = True : Cursor = Cursors.Default : Return
            ElseIf Convert.ToDecimal(Me.grdTermDisc.GetValue("DISCOUNT")) <= 0 Then
                Me.ShowMessageInfo("Please enter the value for discount")
                e.Cancel = True : Cursor = Cursors.Default : Return
            End If
            'Dim intMoreThan As Integer = Convert.ToInt32(Me.grdTermDisc.GetValue("MAX_TO_DATE"))
            'Dim decDiscount As Decimal = Convert.ToDecimal(Me.grdTermDisc.GetValue("DISCOUNT"))
            'Dim DV As DataView = CType(grdTermDisc.DataSource, DataView).ToTable().Copy().DefaultView()
            'DV.RowFilter = "MAX_TO_DATE = " & intMoreThan.ToString() & " AND DISCOUNT = " & decDiscount.ToString()
            'If DV.Count > 0 Then
            '    Me.ShowMessageInfo(Me.MessageDataHasExisted) : e.Cancel = True : Cursor = Cursors.Default : Return
            'End If
            Me.grdTermDisc.SetValue("CreatedDate", NufarmBussinesRules.SharedClass.ServerDate)
            Me.grdTermDisc.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName)
            e.Cancel = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageError(ex.Message)
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdTermDisc_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdTermDisc.CellUpdated
        If Me.grdTermDisc.DataSource Is Nothing Then : Return : End If
        If Me.Isloadingrow Then : Return : End If
        If Not Me.hasLoadForm Then : Return : End If
        If Me.grdTermDisc.RecordCount <= 0 Then : Return : End If
        If Me.grdTermDisc.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
            Try
                '==========UNCOMMENT THIS AFTER MAINTAINANCE ===============
                If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
                    Dim CreatedDate As System.DateTime = Convert.ToDateTime(Me.grdTermDisc.GetValue("CreatedDate"))
                    Cursor = Cursors.WaitCursor
                    Dim FKCode As Integer = Convert.ToInt32(Me.grdTermDisc.GetValue("FKCode"))
                    If Me.clsCPDAuto.hasGeneratedDisc(String.Empty, FKCode, False) Then
                        grdTermDisc.CancelCurrentEdit() : Me.ShowMessageInfo(Me.MessageCantDeleteData) : Cursor = Cursors.Default : Return
                    End If
                    '=================================================================
                End If
                Me.grdTermDisc.UpdateData()
                Cursor = Cursors.Default
            Catch ex As Exception
                grdTermDisc.CancelCurrentEdit()
                Me.ShowMessageError(ex.Message)
                Cursor = Cursors.Default
            End Try
        End If
    End Sub

    Private Sub grdProgressive_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdProgressive.CellUpdated
        If Me.grdProgressive.DataSource Is Nothing Then : Return : End If
        If Me.Isloadingrow Then : Return : End If
        If Not Me.hasLoadForm Then : Return : End If
        If Me.grdProgressive.RecordCount <= 0 Then : Return : End If
        If Me.grdProgressive.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
            Try
                '===========================UNCOMMENT THIS AFTER MAINTENANCE================================
                If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
                    Dim CreatedDate As System.DateTime = Convert.ToDateTime(Me.grdProgressive.GetValue("CreatedDate"))
                    Cursor = Cursors.WaitCursor
                    Dim FKCode As Integer = Convert.ToInt32(Me.grdProgressive.GetValue("FKCode"))
                    If Me.clsCPDAuto.hasGeneratedDisc(String.Empty, FKCode, False) Then
                        grdProgressive.CancelCurrentEdit() : Me.ShowMessageInfo(Me.MessageDataCantChanged) : Cursor = Cursors.Default : Return
                    End If
                End If
                '=====================================================================================
                Me.grdProgressive.UpdateData()
                Cursor = Cursors.Default
            Catch ex As Exception
                grdProgressive.CancelCurrentEdit()
                Me.ShowMessageError(ex.Message)
                Cursor = Cursors.Default
            End Try
        End If
    End Sub

    Private Sub grdTermDisc_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdTermDisc.DeletingRecord
        If Me.grdProgressive.DataSource Is Nothing Then : Return : End If
        If Me.Isloadingrow Then : Return : End If
        If Not Me.hasLoadForm Then : Return : End If
        If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
            Try
                '================== UNCOMMENT THIS AFTER MAINTENANCE =============================
                Dim CreatedDate As System.DateTime = Convert.ToDateTime(Me.grdTermDisc.GetValue("CreatedDate"))
                If DateDiff(DateInterval.Day, CreatedDate, DateTime.Now, FirstDayOfWeek.System, FirstWeekOfYear.System) > 3 Then
                    e.Cancel = True
                    Me.ShowMessageError("Can not delete data over 3 days input") : e.Cancel = True : Return
                End If
                Cursor = Cursors.WaitCursor
                Dim FKCode As Integer = Convert.ToInt32(Me.grdTermDisc.GetValue("FKCode"))
                If Me.clsCPDAuto.hasGeneratedDisc(String.Empty, FKCode, False) Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData) : e.Cancel = True : Cursor = Cursors.Default : Return
                End If
                If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True : Cursor = Cursors.Default : Return
                End If
                '==========================================================
                e.Cancel = False
                Cursor = Cursors.Default
            Catch ex As Exception
                e.Cancel = True
                Me.ShowMessageError(ex.Message)
                Cursor = Cursors.Default
            End Try
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub grdTermDisc_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdTermDisc.RecordAdded
        grdTermDisc.UpdateData()
    End Sub

    Private Sub grdTermDisc_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdTermDisc.RecordsDeleted
        grdTermDisc.UpdateData()
    End Sub

    Private Sub grdBrand_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdBrand.RecordAdded
        grdBrand.UpdateData()
    End Sub

    Private Sub grdBrand_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdBrand.RecordsDeleted
        grdBrand.UpdateData()
    End Sub

    Private Sub TManager1_ButonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.ButonClick
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
            If Not hasLoadForm Then : Return : End If
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
            Me.TManager1_ButonClick(Me.TManager1.btnSearch, New EventArgs())
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
            If Not Me.hasLoadForm Then : Return : End If
            Me.Cursor = Cursors.WaitCursor : If TManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            Select Case Me.TManager1.cbCategory.Text
                Case "PROGRAM_DESC"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
                Case "START_PERIODE"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.DateTime
                Case "END_PERIODE"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.DateTime
                    'Case "AVGPRICE_PL"
                    '    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.Decimal
            End Select
            Me.TManager1_ButonClick(Me.TManager1.btnSearch, New EventArgs())
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_TManager1_CmbSelectedIndexChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_DeleteGridRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles TManager1.DeleteGridRecord
        Try
            Dim CreatedDate As System.DateTime = Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("CreatedDate"))
            If DateDiff(DateInterval.Day, CreatedDate, DateTime.Now, FirstDayOfWeek.System, FirstWeekOfYear.System) > 3 Then
                e.Cancel = True
                Me.ShowMessageError("Can not delete data over 3 days input") : e.Cancel = True : Return
            End If
            Cursor = Cursors.WaitCursor
            Dim IDApp As Integer = Convert.ToInt32(Me.TManager1.GridEX1.GetValue("IDApp"))
            If Me.clsCPDAuto.hasGeneratedDisc(String.Empty, IDApp, False) Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData) : e.Cancel = True : Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True : Return
            End If
            Me.clsCPDAuto.DeleteCPDAuto(IDApp, False)
            Me.ClearEntry()
            Me.btnEditRow.Checked = False : Me.btnSave.Checked = False
            Cursor = Cursors.Default
            e.Cancel = False
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageError(ex.Message)
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub TManager1_GridCurrentCell_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.GridCurrentCell_Changed
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.Isloadingrow Then : Return : End If
            If Me.TManager1.GridEX1.DataSource Is Nothing Then
                Me.Isloadingrow = True : Me.isLoadingCombo = True : Me.ClearEntry()
                Me.Isloadingrow = False : Me.isLoadingCombo = False : Return
            End If
            If Me.TManager1.GridEX1.RecordCount <= 0 Then
                Me.Isloadingrow = True : Me.isLoadingCombo = True : Me.ClearEntry()
                Me.Isloadingrow = False : Me.isLoadingCombo = False : Return
            End If
            'Me.Isloadingrow = True
            If Me.TManager1.GridEX1.RecordCount > 0 Then
                If Me.TManager1.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                    'Me.pnlEntryAndFilter.Height = Me.pnlentryHeight
                    'Me.InflateData()
                    'Me.btnAddNew.Checked = False
                    'Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
                    Me.btnEditRow.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.CPDAuto
                    'Else
                    '    Me.ClearEntry()
                    '    Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                End If
            End If
            'Me.Isloadingrow = False
            'isLoadingCombo = False
            'Me.btnEditRow.Enabled = True
        Catch ex As Exception
            isLoadingCombo = False
            'Me.btnEditRow.Checked = False
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
                Me.TManager1_ButonClick(Me.TManager1.btnSearch, New EventArgs())
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

    Private Sub TManager1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.Enter
        Me.AcceptButton = Me.TManager1.btnSearch
    End Sub

    Private Sub CPDAuto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.pnlentryHeight = Me.pnlEntry.Height

            Me.LoadData()
            ''fil dataview
            Me.ReadAccess()
            Me.pnlEntry.Height = 0
            Me.btnSave.Enabled = False
            Me.btnEditRow.Enabled = False
            Me.pnlEntryAndFilter.Visible = False
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
        End Try
        Me.hasLoadForm = True

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub CPDAuto_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.Isloadingrow Then : Return : End If
        If Me.pnlEntry.Height > 0 Then
            If Me.HasChangeData() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        Me.SaveData()
                    Catch ex As Exception
                        Me.ShowMessageError(ex.Message)
                    End Try
                    Me.Cursor = Cursors.Default
                End If
            End If
        End If
    End Sub

    Private Sub grdBrand_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdBrand.CurrentCellChanged
        If Me.Isloadingrow Then : Return : End If
        If Not Me.hasLoadForm Then : Return : End If
        If Me.grdBrand.DataSource Is Nothing Then : Return : End If
        If Me.grdBrand.RecordCount <= 0 Then : Return : End If
        If Me.grdBrand.GetRow().RowType <> Janus.Windows.GridEX.RowType.NewRecord And Me.grdBrand.GetRow().RowType <> Janus.Windows.GridEX.RowType.Record Then
            Return
        End If
        If Me.grdBrand.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
            Me.grdBrand.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdBrand.SelectCurrentCellText()
        ElseIf Me.grdBrand.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
            Me.grdBrand.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
        End If
    End Sub

    Private Sub grdBrand_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdBrand.ColumnButtonClick
        If e.Column.Key = "BRANDPACK_ID" Then
            Me.Cursor = Cursors.WaitCursor
            If IsNothing(Me.frmLookUP) OrElse Me.frmLookUP.IsDisposed Then
                Me.frmLookUP = New LookUp()
                With Me.frmLookUP
                    .watermarkText = "Enter BrandPackName to search for"
                    Dim DV As DataView = Me.tblProduct.DefaultView()
                    DV.RowFilter = ""
                    DV.Sort = "BRANDPACK_NAME ASC"
                    ''fill datagrid.Grid.SetDataBinding(DV, "")
                    .Grid.SetDataBinding(DV, "")
                    .Grid.RetrieveStructure()
                    .Grid.RootTable.Columns(0).ShowRowSelector = True
                    .Grid.RootTable.Columns(0).UseHeaderSelector = True
                    '.Grid.AutoSizeColumns()
                End With
                AddHandler frmLookUP.SearchKeyDown, AddressOf SearchBrandPack
                AddHandler frmLookUP.OkClick, AddressOf SearchOKClick
                frmLookUP.ShowDialog(Me)
            End If
            Me.Cursor = Cursors.Default
        End If
    End Sub
    Private Sub SearchBrandPack(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Me.Cursor = Cursors.WaitCursor

        If Not IsNothing(Me.frmLookUP.Grid.DataSource) Then
            frmLookUP.Grid.RemoveFilters()
            Dim DV As DataView = CType(Me.frmLookUP.Grid.DataSource, DataView)

            DV.RowFilter = "BRANDPACK_NAME LIKE '%" & frmLookUP.txtSearch.Text.TrimEnd().TrimStart() & "%'"
            For i As Integer = 0 To Me.frmLookUP.Grid.RecordCount - 1
                frmLookUP.Grid.MoveToRowIndex(i)
                Dim BrandPackID As String = frmLookUP.Grid.GetValue("BRANDPACK_ID").ToString()
                If Me.listBrandPack.Contains(BrandPackID) Then
                    frmLookUP.Grid.CurrentRow.CheckState = Janus.Windows.GridEX.RowCheckState.Checked
                Else
                    frmLookUP.Grid.CurrentRow.CheckState = Janus.Windows.GridEX.RowCheckState.Unchecked
                End If
            Next
        End If

        Cursor = Cursors.Default
    End Sub
    Private Sub SearchOKClick(ByVal sender As Object, ByVal e As EventArgs)
        If IsNothing(frmLookUP.Grid.DataSource) Then : Return : End If
        If frmLookUP.Grid.RecordCount <= 0 Then : Return : End If
        If frmLookUP.Grid.GetCheckedRows().Length <= 0 Then : Return : End If
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.Isloadingrow = True
            Dim FKCode As Object = DBNull.Value
            If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
                FKCode = Me._DCPDAuto.IDApp
            End If
            Dim DV As DataView = Me._DCPDAuto.TProduct.DefaultView()
            DV.Sort = "BRANDPACK_ID"
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.frmLookUP.Grid.GetCheckedRows()
                'dim index as Integer = D
                With Me.frmLookUP
                    .Grid.MoveTo(row)
                    .Grid.SelectCurrentCellText()
                    Dim BrandPackID As String = .Grid.GetValue("BRANDPACK_ID").ToString()
                    Dim index As Integer = DV.Find(BrandPackID)
                    If index <= -1 Then 'data belum ada
                        Dim drv As DataRowView = DV.AddNew()
                        drv("BRANDPACK_ID") = .Grid.GetValue("BRANDPACK_ID")
                        drv("CreatedDate") = NufarmBussinesRules.SharedClass.ServerDate
                        drv("CreatedBy") = NufarmBussinesRules.User.UserLogin.UserName
                        drv.EndEdit()
                    End If
                End With
            Next
            Me.grdBrand.SetDataBinding(DV, "")
            DV.Sort = "BRANDPACK_NAME ASC"
            Me.Isloadingrow = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        End Try
    End Sub
    Private Sub frmLookUP_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles frmLookUP.FormClosing
        RemoveHandler frmLookUP.SearchKeyDown, AddressOf SearchBrandPack
        RemoveHandler frmLookUP.OkClick, AddressOf SearchOKClick
        Me.frmLookUP = Nothing
    End Sub

    'Private Sub frmLookUP_gridRowChecked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles frmLookUP.gridRowChecked
    '    If e.Row.CheckState = Janus.Windows.GridEX.RowCheckState.Checked Then
    '        'check apakah 
    '        If Not Me.listBrandPack.Contains(e.Row.Cells("BRANDPACK_ID").Value.ToString()) Then
    '            Me.listBrandPack.Add(e.Row.Cells("BRANDPACK_ID").Value.ToString())
    '        End If
    '    Else
    '        If Me.listBrandPack.Contains(e.Row.Cells("BRANDPACK_ID").Value.ToString()) Then
    '            Me.listBrandPack.Remove(e.Row.Cells("BRANDPACK_ID").Value.ToString())
    '        End If
    '    End If
    'End Sub

    Private Sub pnlEntry_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlEntry.Enter
        Me.AcceptButton = Nothing
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Cursor = Cursors.WaitCursor
        If Me.HasChangeData() Then
            If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                Try
                    Me.SaveData()
                    Me.PageIndex = Me.TotalIndex
                    Me.Isloadingrow = True
                    Me.GetData()
                Catch ex As Exception
                    Me.ShowMessageError(ex.Message)
                End Try
            End If
        End If
        Me.ClearEntry()
        Me.pnlEntry.Height = 0
        Me.btnAddNew.Checked = False
        Me.btnEditRow.Checked = False
        Me.btnSave.Enabled = False
        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None
        Me.Isloadingrow = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TManager1_GridDoubleClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles TManager1.GridDoubleClicked
        Try
            If Me.TManager1.GridEX1.DataSource Is Nothing Then
                Return
            End If
            If Me.TManager1.GridEX1.RecordCount <= 0 Then : Return : End If
            If Me.TManager1.GridEX1.SelectedItems Is Nothing Then : Return : End If
            If Me.TManager1.GridEX1.GetRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            'inflate data 
            Me.pnlEntry.Height = Me.pnlentryHeight
            Me.InflateData()
            Me.btnAddNew.Checked = False
            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
            Me.Isloadingrow = False
            isLoadingCombo = False
            Me.btnEditRow.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.CPDAuto
            Me.btnEditRow.Checked = True
            Me.btnSave.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.CPDAuto
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Isloadingrow = False
            isLoadingCombo = False
            Me.btnEditRow.Enabled = False
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdProgressive_InputMaskError(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.InputMaskErrorEventArgs) Handles grdProgressive.InputMaskError
        Me.baseTooltip.Show("Input format error for " & e.Text & "Please enter valid value", grdProgressive, 2500) : Me.grdTermDisc.MoveToNewRecord()
        e.Action = Janus.Windows.GridEX.InputMaskErrorAction.CancelUpdate
    End Sub

    Private Sub grdTermDisc_InputMaskError(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.InputMaskErrorEventArgs) Handles grdTermDisc.InputMaskError
        Me.baseTooltip.Show("Input format error for " & e.Text & "Please enter valid value", Me.grdTermDisc, 2500) : Me.grdTermDisc.MoveToNewRecord()
        e.Action = Janus.Windows.GridEX.InputMaskErrorAction.CancelUpdate ' Janus.Windows.GridEX.InputMaskErrorAction.DiscardChanges
    End Sub
End Class
