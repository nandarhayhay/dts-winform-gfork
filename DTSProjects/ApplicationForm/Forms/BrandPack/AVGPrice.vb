Imports System.Data
Imports NufarmBussinesRules.Brandpack
Imports NuFarm.Domain.AVGPrice
Public Class AVGPrice
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
    Private m_clsAvgPrice As AveragePrice
    Private pnlentryHeight As Integer
    Private DV As DataView = Nothing
    Private hasLoadForm As Boolean = False
    Private DVBrand As DataView = Nothing
    Private ReadOnly Property ClsAGPrice() As AveragePrice
        Get
            If IsNothing(Me.m_clsAvgPrice) Then
                Me.m_clsAvgPrice = New NufarmBussinesRules.Brandpack.AveragePrice()
            End If
            Return Me.m_clsAvgPrice
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
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog()
                Case "btnCustomFilter"
                    Me.FilterEditor1.Visible = True
                    Me.FilterEditor1.SortFieldList = False
                    Me.FilterEditor1.SourceControl = Me.TManager1.GridEX1
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    Me.TManager1.GridEX1.RemoveFilters()
                    Me.TManager1.BringToFront()
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.FilterEditor1.SourceControl = Nothing
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.TManager1.GridEX1.RemoveFilters()
                Case "btnExport"
                    ExportData(Me.TManager1.GridEX1)
                Case "btnRefresh"
                    'If (Me.btnAddNew.Checked = True) Or (Me.pnlEntryAndFilter.Height = Me.pnlentryHeight) Then
                    '    Me.ShowMessageInfo("Please close entry area to refresh/reload data") : Me.Cursor = Cursors.Default
                    '    Return
                    'End If
                    'reload Data

                    Me.GetData() : Me.SetOriginalCriteria()
                Case "btnAddNew"
                    Me.Isloadingrow = True
                    If Me.btnAddNew.Checked Then
                        Me.ClearEntry()
                        Me.pnlEntryAndFilter.Height = 0
                        Me.btnAddNew.Checked = False
                        Me.txtAvgPrice.ReadOnly = True
                    Else
                        Me.pnlEntryAndFilter.Height = Me.pnlentryHeight
                        Me.ClearEntry()
                        Me.chkBrand.DropDownDataSource = Me.DVBrand
                        Me.chkBrand.DropDownDataMember = ""
                        Me.chkBrand.DropDownDisplayMember = "BRAND_NAME"
                        Me.chkBrand.DropDownValueMember = "BRAND_ID"
                        Me.btnAddNew.Checked = True
                        Me.dtPicFrom.ReadOnly = False
                        'Me.chkBrand.ReadOnly = False
                        Me.txtAvgPrice.ReadOnly = False
                        Me.dtPicFrom.Focus()
                        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                    End If
                    Me.grpgrpAvgPrice.Enabled = True
                    Me.btnSave.Enabled = True
                Case "btnSave"
                    If Me.pnlEntryAndFilter.Height <> 0 Then
                        If Me.SaveData() Then
                            Me.PageIndex = Me.TotalIndex
                            Me.Isloadingrow = True
                            Me.GetData() : Me.ClearEntry()
                        End If
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
    Friend Sub LoadData()
        isLoadingCombo = True
        With Me.TManager1.cbCategory
            .Items.Add("BRAND_NAME") : .Items.Add("START_PERIODE") : .Items.Add("AVGPRICE_FM") : .Items.Add("AVGPRICE_PL")
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
                .SearchBy = "BR.BRAND_NAME"
            ElseIf Me.TManager1.cbCategory.Text = "BRAND_NAME" Then
                .SearchBy = "BR.BRAND_NAME"
            Else
                Dim SearchBy As String = ""
                Select Case Me.TManager1.cbCategory.Text
                    Case "BRAND_NAME"
                        SearchBy = "BR.BRAND_NAME"
                    Case "START_DATE"
                        SearchBy = "AVG.START_DATE"
                    Case "AVGPRICE"
                        SearchBy = "AVG.AVGPRICE"
                    Case "AVGPRICE_PL"
                        SearchBy = "AVG.AVGPRICE_PL"
                End Select
                .SearchBy = Me.TManager1.cbCategory.Text
            End If
            Me.RunQuery(.SearchValue, .SearchBy)
        End With
        If TManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
        'isi chkbrand 
        'Me.chkBrand.DropDownValueMember = "BRAND_ID"
        'Me.chkBrand.DropDownList.DisplayMember = "BRAND_NAME"
        'Me.chkBrand.DropDownList.ValueMember = "BRAND_ID"
        'Me.chkBrand.DropDownDisplayMember = "BRAND_NAME"
        'Me.chkBrand.DropDownList.SetDataBinding(Me.DVBrand, "")
        'Me.chkBrand.SetDataBinding(DVBrand, "")
        'Me.chkBrand.DropDownDisplayMember = "BRAND_NAME"
        'Me.chkBrand.DropDownValueMember = "BRAND_ID"
        'Me.chkBrand.ValueItemDataMember = "BRAND_ID"
        'Me.chkBrand.DropDownList.DisplayMember = "BRAND_NAME"
        'Me.chkBrand.DropDownList.ValueMember = "BRAND_ID"

    End Sub

    Private Sub InflateData()
        If Not Me.Isloadingrow Then : Me.Isloadingrow = True : End If
        'check apakah createdDate > 3 hari
        Me.dtPicFrom.Value = Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("START_PERIODE"))
        Me.txtAvgPrice.Value = Convert.ToDecimal(Me.TManager1.GridEX1.GetValue("AVGPRICE_FM"))
        Me.txtAVGpricePL.Value = Convert.ToDecimal(Me.TManager1.GridEX1.GetValue("AVGPRICE_PL"))
        If IsNothing(Me.chkBrand.DropDownDataSource) Then
            Me.chkBrand.DropDownDataSource = Me.DVBrand
            Me.chkBrand.DropDownDataMember = ""
            Me.chkBrand.DropDownDisplayMember = "BRAND_NAME"
            Me.chkBrand.DropDownValueMember = "BRAND_ID"
        ElseIf Me.chkBrand.DropDownList.RecordCount <= 0 Then
            Me.chkBrand.DropDownDataSource = Me.DVBrand
            Me.chkBrand.DropDownDataMember = ""
            Me.chkBrand.DropDownDisplayMember = "BRAND_NAME"
            Me.chkBrand.DropDownValueMember = "BRAND_ID"
        End If
        Me.chkBrand.DropDownList().Select()
        Me.chkBrand.CheckedValues = New Object() {Me.TManager1.GridEX1.GetValue("BRAND_ID")}
        Me.dtPicFrom.ReadOnly = True
        Me.txtAvgPrice.ReadOnly = True
        'Me.chkBrand.ReadOnly = True
        Me.btnSave.Enabled = False
    End Sub
    Private Function SaveData() As Boolean
        If IsNothing(Me.chkBrand.CheckedValues()) Then
            Me.baseTooltip.Show("Please define brand", Me.chkBrand, 2500) : Me.chkBrand.Focus() : Return False
        End If
        If Me.chkBrand.CheckedValues().Length <= 0 Then
            Me.baseTooltip.Show("Please define brand", Me.chkBrand, 2500) : Me.chkBrand.Focus() : Return False
        ElseIf Me.txtAvgPrice.Value <= 0 Then
            Me.baseTooltip.Show("Please define Avg Price", Me.txtAvgPrice, 2500) : Me.txtAvgPrice.Focus() : Return False
        End If
        Dim OAvg As New Nufarm.Domain.AVGPrice
        With OAvg
            .AvgPrice_FM = Convert.ToDecimal(Me.txtAvgPrice.Value)
            .AvgPrice_PL = Convert.ToDecimal(Me.txtAVGpricePL.Value)
            Dim ListBrandID As New List(Of String)
            For i As Integer = 0 To Me.chkBrand.CheckedValues.Length - 1
                Dim BrandID As String = Me.chkBrand.CheckedValues().GetValue(i).ToString()
                If Not ListBrandID.Contains(BrandID) Then
                    ListBrandID.Add(BrandID)
                End If
            Next
            .ListBrandID = ListBrandID
            .StartPeriode = Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString())
        End With
        Me.ClsAGPrice.SaveData(OAvg, False)
        Return True
    End Function
    Private Sub ClearEntry()
        Me.chkBrand.UncheckAll()
        Me.txtAvgPrice.Value = 0
        Me.txtAVGpricePL.Value = 0
    End Sub
    Friend Sub GetData()
        Dim SearchString As String = Me.TManager1.txtSearch.Text.Trim(), SearchBy As String = Me.TManager1.cbCategory.Text
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
        'Dim Ischanged As Boolean = False
        'If Me.OriginalData.SearchBy <> SearchBy Then
        '    Ischanged = True
        'ElseIf Me.OriginalData.CriteriaSearch <> strCriteriaSearch Then
        '    Ischanged = True
        'ElseIf Me.OriginalData.MaxRecord <> MaxRecord Then
        '    Ischanged = True
        '    'ElseIf Me.OriginalData.SearchValue <> RTrim(Me.TManager1.txtSearch.Text) Then
        '    '    Ischanged = True
        'ElseIf Me.IsSavingData Then
        '    Ischanged = True
        'End If
        'set originaldata
        With Me.OriginalData
            .CriteriaSearch = strCriteriaSearch
            .MaxRecord = MaxRecord
            .SearchBy = SearchBy
            .SearchValue = SearchString
            Me.RunQuery(.SearchValue, .SearchBy)
        End With
        If TManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()

        'isi chkbrand
        'Me.chkBrand.DropDownValueMember = "BRAND_ID"
        'Me.chkBrand.DropDownDisplayMember = "BRAND_NAME"
        'Me.chkBrand.DropDownList.SetDataBinding(Me.DVBrand, "")

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
        If (Me.FilterEditor1.Visible) And (Not IsNothing(Me.FilterEditor1.SourceControl)) Then
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
        Else
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        End If
        Me.TManager1.GridEX1.AutoSizeColumns(Me.TManager1.GridEX1.RootTable)
        Me.TManager1.GridEX1.RootTable.Columns("IDApp").Visible = False
        Me.TManager1.GridEX1.RootTable.Columns("ROW_NUM").Caption = "NO"
        Me.TManager1.GridEX1.RootTable.Columns("BRAND_ID").Visible = False
        Me.TManager1.GridEX1.RootTable.Columns("CreatedDate").Visible = False
    End Sub

    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String)
        Dim Dv As DataView = Nothing : Me.Isloadingrow = True

        Dv = Me.ClsAGPrice.PopulateQuery(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType, Me.DVBrand)
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
    Private Sub ExportData(ByVal grid As Janus.Windows.GridEX.GridEX)
        Dim SV As New SaveFileDialog()
        With SV
            .Title = "Define the location of File"
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

    Private Sub AVGPrice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.pnlentryHeight = Me.pnlEntry.Height

            Me.LoadData()
            'Me.chkBrand.SetDataBinding(DVBrand, "")
            'Me.chkBrand.DropDownDisplayMember = "BRAND_NAME"
            'Me.chkBrand.DropDownValueMember = "BRAND_ID"
            'Me.chkBrand.ValueItemDataMember = "BRAND_ID"
            ''fil dataview
            Me.ReadAccess()
            Me.pnlEntryAndFilter.Height = 0
            Me.btnSave.Enabled = False
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.ShowMessageError(ex.Message)
        Finally
            isLoadingCombo = False
            Me.IsloadedForm = True
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.AVGPrice Then
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            Else
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_INSERT.AVGPrice Then
                Me.btnAddNew.Visible = False
            Else
                Me.btnAddNew.Visible = True
                Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
            End If
        End If
    End Sub

    Private Sub TManager1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.Enter
        Me.AcceptButton = Me.TManager1.btnSearch
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
            If Not Me.IsloadedForm Then : Return : End If
            Me.Cursor = Cursors.WaitCursor : If TManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            Select Case Me.TManager1.cbCategory.Text
                Case "BRAND_NAME"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
                Case "START_PERIODE"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.DateTime
                Case "AVGPRICE_FM"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.Decimal
                Case "AVGPRICE_PL"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.Decimal
            End Select
            Me.TManager1_ButonClick(Me.TManager1.btnSearch, New EventArgs())
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_TManager1_CmbSelectedIndexChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
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
            Me.Isloadingrow = True
            If Me.TManager1.GridEX1.RecordCount > 0 Then
                If Me.TManager1.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                    Me.pnlEntryAndFilter.Height = Me.pnlentryHeight
                    Me.InflateData()
                    Me.btnAddNew.Checked = False
                    Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
                Else
                    Me.ClearEntry()
                    Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                End If
            End If
            Me.Isloadingrow = False
            isLoadingCombo = False
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

    Private Sub TManager1_DeleteGridRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles TManager1.DeleteGridRecord
        Try
            Dim CreatedDate As System.DateTime = Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("CreatedDate"))
            If DateDiff(DateInterval.Day, CreatedDate, DateTime.Now, FirstDayOfWeek.System, FirstWeekOfYear.System) > 3 Then
                e.Cancel = True
                Me.ShowMessageError("Can not delete data over 3 days input") : Return
            End If
            Cursor = Cursors.WaitCursor
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.Yes Then
                Me.ClsAGPrice.Delete(Convert.ToInt32(Me.TManager1.GridEX1.GetValue("IDApp")), False)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageError(ex.Message)
        End Try
        Cursor = Cursors.Default
    End Sub
End Class
