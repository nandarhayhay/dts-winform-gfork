Public Class GeneralPricePlantation
    Private m_clsPrice As NufarmBussinesRules.Brandpack.GeneralPlantationPrice
    Private IsLoadingCombo As Boolean
    Private PageIndex As Int32 = 0, PageSize As Integer = 0, TotalIndex As Integer = 0, RowCount As Integer = 0
    Private m_DataType As NufarmBussinesRules.common.Helper.DataTypes = NufarmBussinesRules.common.Helper.DataTypes.VarChar
    Private m_Criteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private OriginalCriteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private isUndoingCriteria As Boolean = False
    Private Mode As NufarmBussinesRules.common.Helper.SaveMode = NufarmBussinesRules.common.Helper.SaveMode.Insert
    Private IsloadedForm As Boolean = False : Private Isloadingrow As Boolean = True
    'Private  TManager1.GridEX1 As Janus.Windows.GridEX.GridEX = Nothing
    'Private isSearchingDistributor As Boolean = True
    Private IsChangedValueFromBrandPack As Boolean = False
    Private ListOriginalPriceTag As New List(Of String)
    Private IsSelecttedRowFromGrid As Boolean = False
    Friend CMain As Main = Nothing
    Dim ChekedBrandPackIDS As New List(Of String)
    Private IDApp As Integer = 0
    Private ReadOnly Property clsPrice() As NufarmBussinesRules.Brandpack.GeneralPlantationPrice
        Get
            If (Me.m_clsPrice Is Nothing) Then
                Me.m_clsPrice = New NufarmBussinesRules.Brandpack.GeneralPlantationPrice()
            End If
            Return (Me.m_clsPrice)
        End Get
    End Property
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PlantationPrice Then
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            Else
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PlantationPrice Then
                Me.btnAddNew.Enabled = False
                'Me.btnNewKebun.Enabled = False
            Else
                Me.btnAddNew.Enabled = True
                'Me.btnNewKebun.Enabled = True
            End If
            If Not NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PlantationPrice Then
                Me.TManager1.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                Me.btnSave.Enabled = False
            Else
                Me.btnSave.Enabled = True
                Me.TManager1.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            End If
        End If
    End Sub
    Private Sub ClearText()
        Me.IsLoadingCombo = True
        Me.mcbBrandPack.Value = Nothing
        Me.mcbBrandPack.Text = ""
        'Me.mcbPlantation.SetDataBinding(Nothing, "")
        'Me.mcbPlantation.Value = Nothing
        'Me.mcbPlantation.Text = ""
        Me.txtPrice.Value = 0
        Me.dtPicStartDate.Value = NufarmBussinesRules.SharedClass.ServerDate
        Me.IsLoadingCombo = False
    End Sub
    Private Sub PullAndShowData()
        Dim BRANDPACK_ID As String = TManager1.GridEX1.GetValue("BRANDPACK_ID").ToString()
        'Dim PLANTATION_ID As String = TManager1.GridEX1.GetValue("PLANTATION_ID").ToString()
        Me.IDApp = Me.TManager1.GridEX1.GetValue("IDApp")
        Dim START_DATE As DateTime = Convert.ToDateTime(TManager1.GridEX1.GetValue("START_DATE"))
        Dim PRICE As Decimal = Convert.ToDecimal(TManager1.GridEX1.GetValue("PRICE"))
        Me.mcbBrandPack.Value = BRANDPACK_ID
        If Not IsNothing(Me.TManager1.GridEX1.GetValue("IncludeDPD")) And Not IsDBNull(Me.TManager1.GridEX1.GetValue("IncludeDPD")) Then
            Me.chkIncludeDPD.Checked = CBool(Me.TManager1.GridEX1.GetValue("IncludeDPD"))
        Else
            Me.chkIncludeDPD.Checked = False
        End If
        Me.IsLoadingCombo = True

        Dim PRICE_TAG As String = Me.TManager1.GridEX1.GetValue("PRICE_TAG").ToString() 'DVCopyOriginal(i)("PRICE_TAG").ToString()
        'Me.mcbPlantation.Value = PLANTATION_ID
        Me.dtPicStartDate.Value = START_DATE
        Me.txtPrice.Value = PRICE
        If Not IsNothing(Me.TManager1.GridEX1.GetValue("IncludeDPD")) And Not IsDBNull(Me.TManager1.GridEX1.GetValue("IncludeDPD")) Then
            Me.chkIncludeDPD.Checked = Convert.ToBoolean(Me.TManager1.GridEX1.GetValue("IncludeDPD"))
        Else
            Me.chkIncludeDPD.Checked = False
        End If
        Me.mcbBrandPack.ReadOnly = True
        'Me.mcbPlantation.ReadOnly = True
        '==============================================================================================================
        If Me.clsPrice.HasReferencedDataPO(PRICE_TAG, False) Then
            Me.txtPrice.ReadOnly = True
            Me.dtPicStartDate.ReadOnly = True
            Me.btnSeacrhBrandPack.Enabled = False
        Else
            Me.txtPrice.ReadOnly = False
            Me.btnSeacrhBrandPack.Enabled = True
            Me.dtPicStartDate.ReadOnly = False
        End If
        Me.IsLoadingCombo = False
    End Sub
    Private Sub FillComboBrandPack(ByVal SearchString As String)
        Me.IsLoadingCombo = True
        Me.mcbBrandPack.DataSource = Me.clsPrice.GetBrandPack(SearchString, Me.dtPicStartDate.Value)
        With Me.mcbBrandPack
            .Text = ""
            .DropDownList.RetrieveStructure()
            .DroppedDown = True
            .DisplayMember = "BRANDPACK_NAME" : .ValueMember = "BRANDPACK_ID"
            .DropDownList().Columns("BRANDPACK_ID").AutoSize()
            .DropDownList().Columns("BRANDPACK_NAME").AutoSize()
            .DroppedDown = False
        End With
        Me.IsLoadingCombo = False
    End Sub
    'Private Sub FillComboPlantation(ByVal DV As DataView)
    '    Me.IsLoadingCombo = True
    '    With Me.mcbPlantation
    '        .Text = ""
    '        .DataSource = DV
    '        If IsNothing(DV) Then : Me.IsLoadingCombo = False : Return : End If
    '        .DropDownList.RetrieveStructure()
    '        .DroppedDown = True
    '        .DisplayMember = "PLANTATION_NAME" : .ValueMember = "PLANTATION_ID"
    '        .DropDownList.AutoSizeColumns()
    '        .DroppedDown = False
    '    End With
    '    Me.IsLoadingCombo = False
    'End Sub
    Friend Sub GetData()
        RowCount = 0
        Dim SearchString As String = Me.TManager1.txtSearch.Text, SearchBy As String = Me.TManager1.cbCategory.Text
        Select Case Me.TManager1.cbCategory.Text
            Case "BRANDPACK_NAME"
                SearchBy = "BP.BRANDPACK_NAME"
            Case "START_DATE"
                SearchBy = "GPL.START_DATE"
            Case Else
                SearchBy = "BP.BRANDPACK_NAME"
        End Select
        If Me.TManager1.txtMaxRecord.Text <> "" Then
            If CInt(Me.TManager1.txtMaxRecord.Text) < CInt(Me.TManager1.cbPageSize.Text) Then
                Me.PageSize = CInt(Me.TManager1.txtMaxRecord.Text)
            Else
                Me.PageSize = CInt(Me.TManager1.cbPageSize.Text)
            End If
        Else
            Me.PageSize = CInt(Me.TManager1.cbPageSize.Text)
        End If
        Me.RunQuery(Me.TManager1.txtSearch.Text, SearchBy)
        If TManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
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
        End If
        TManager1.lblPosition.Text = String.Format("Page {0} Of {1} page(s)", Me.PageIndex, Me.TotalIndex)
    End Sub
    Private Sub FormatDataGrid()
        TManager1.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        'TManager1.GridEX1.RootTable.Columns(0).Visible = False
        TManager1.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        TManager1.GridEX1.RootTable.Caption = "GENERAL PLANTATION PRICE"
        TManager1.GridEX1.TableHeaderFormatStyle.ForeColor = Color.Maroon
        TManager1.GridEX1.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
        If (Me.FilterEditor1.Visible) And (Not IsNothing(Me.FilterEditor1.SourceControl)) Then
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
        Else
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        End If
        Me.TManager1.GridEX1.RootTable.Columns("IncludeDPD").UseHeaderSelector = True
        Me.TManager1.GridEX1.RootTable.Columns("ROW_NUM").Caption = "NO."
        For Each col As Janus.Windows.GridEX.GridEXColumn In TManager1.GridEX1.RootTable.Columns
            If col.Type Is Type.GetType("System.Int32") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            ElseIf col.Type Is Type.GetType("System.Decimal") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FormatString = "#,##0.00"
            ElseIf col.Type Is Type.GetType("System.DateTime") Then
                col.FormatString = "dd MMMM yyyy"
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            ElseIf col.Type Is Type.GetType("System.Boolean") Then
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CheckBox
            End If
            If col.Key = "IncludeDPD" Then
                col.EditType = Janus.Windows.GridEX.EditType.CheckBox
            Else
                col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            'col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            'col.AutoSize()
        Next
        TManager1.GridEX1.RootTable.Columns("BRANDPACK_ID").Visible = False
        'TManager1.GridEX1.RootTable.Columns("PLANTATION_ID").Visible = False
        TManager1.GridEX1.RootTable.Columns("PRICE_TAG").Visible = False
        TManager1.GridEX1.RootTable.Columns("BRANDPACK_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        TManager1.GridEX1.RootTable.Columns("IDApp").Visible = False
        TManager1.GridEX1.RootTable.Columns("IDApp").ShowInFieldChooser = False
        Me.TManager1.CHKSelectAll.Visible = True
        TManager1.GridEX1.AutoSizeColumns()
        Me.TManager1.GridEX1.Update()
    End Sub
    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String)
        RowCount = 0 : Dim Dv As DataView = Nothing : Me.Isloadingrow = True
        Dv = Me.clsPrice.PopulateQuery(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, Me.m_Criteria, Me.m_DataType)
        If Not IsNothing(Dv) Then
            If (Dv.Count > 0) Then
                Me.TManager1.GridEX1.DataSource = Dv : Me.TManager1.GridEX1.RetrieveStructure()
                Me.FormatDataGrid()
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
    Friend Sub LoadData()
        IsLoadingCombo = True : Me.IsloadedForm = True
        Me.dtPicStartDate.MaxDate = NufarmBussinesRules.SharedClass.ServerDate
        Me.FillComboBrandPack("")


        With Me.TManager1.cbCategory
            .Items.Add("BRANDPACK_NAME") : .Items.Add("START_DATE")
        End With
        Me.PageIndex = 1 : Me.PageSize = 500 : Me.TManager1.cbPageSize.Text = "500"
        Me.TManager1.btnCriteria.Text = "*.*" : Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
        Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
        Me.RunQuery("", "BP.BRANDPACK_NAME")
        If TManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
        'Dim DV As DataView = Nothing
        'Using PL As New NufarmBussinesRules.Plantation.Plantation
        '    PL.GetPlantation(Me.mcbPlantation.Text, DV)
        'End Using
        'Me.FillComboPlantation(DV)
    End Sub
    Private Sub setVisibleDateControl(ByVal isVisible As Boolean)
        TManager1.lblFrom.Visible = isVisible
        TManager1.lblUntil.Visible = isVisible
        TManager1.dtPicFrom.Visible = isVisible
        TManager1.dtPicUntil.Visible = isVisible
    End Sub
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
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
                    .btnCriteria.Text = "=><="
                    .btnCriteria.CompareOperator = CompareOperator.Between
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
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
            Case CompareOperator.Between
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
        End Select
    End Sub
    Private Function IsValid() As Boolean
        If IsNothing(Me.mcbBrandPack.Value) Then
            Me.baseTooltip.Show("Please define Brandpack", Me.mcbBrandPack, 2500) : Me.mcbBrandPack.Focus() : Return False
        End If
        If Me.mcbBrandPack.SelectedIndex <= -1 Then
            Me.baseTooltip.Show("Please define Brandpack", Me.mcbBrandPack, 2500) : Me.mcbBrandPack.Focus() : Return False
        End If
        'If Me.mcbPlantation.Value Is Nothing Or Me.mcbPlantation.SelectedIndex <= -1 Then
        '    Me.baseTooltip.Show("Please define Plantation", Me.mcbPlantation, 2500) : Me.mcbPlantation.Focus() : Return False
        'End If
        If Me.txtPrice.Value Is Nothing Then
            Me.baseTooltip.Show("Please define Price value", Me.txtPrice, 2500) : Return False
        End If
        If Me.txtPrice.Value <= 0 Then
            Me.baseTooltip.Show("Please define Price value", Me.txtPrice, 2500) : Return False
        End If
        'check ID distributor di ID PLANTATION
        Return True
    End Function
    Private Sub GeneralPricePlantation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.ReadAccess()
            Me.IsLoadingCombo = True
            Me.mcbBrandPack.Text = "" : Me.mcbBrandPack.Value = Nothing
            'Me.mcbPlantation.Value = Nothing : Me.mcbPlantation.Text = ""
            Me.txtPrice.Value = 0
            Me.chkIncludeDPD.Checked = False

            Me.mcbBrandPack.ReadOnly = False
            'Me.mcbPlantation.ReadOnly = False
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
            Isloadingrow = False : Me.IsloadedForm = False
            Me.IsLoadingCombo = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        End Try
    End Sub

    Private Sub GeneralPricePlantation_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsPrice) Then
                Me.clsPrice.Dispose()
            End If
        Catch
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Select Case CType(sender, DevComponents.DotNetBar.BaseItem).Name
                Case "btnShowFieldChooser"
                    Me.TManager1.GridEX1.ShowFieldChooser(Me)
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.TManager1.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnPrint"
                    Me.GridEXPrintDocument1.GridEX = Me.TManager1.GridEX1
                    Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                    'Me.PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
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
                    Me.FilterEditor1.SourceControl = Me.TManager1.GridEX1
                    Me.TManager1.GridEX1.RemoveFilters()
                    Me.FilterEditor1.Visible = True
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.TManager1.GridEX1.RemoveFilters()
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Case "btnExport"
                    Me.SaveFileDialog1.OverwritePrompt = True
                    Me.SaveFileDialog1.DefaultExt = ".xls"
                    Me.SaveFileDialog1.Filter = "All Files|*.*"
                    Me.SaveFileDialog1.InitialDirectory = "C:\"
                    If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                        Me.GridEXExporter1.GridEX = Me.TManager1.GridEX1
                        Me.GridEXExporter1.Export(FS)
                        FS.Close()
                        MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Case "btnRefresh"
                    Me.TManager1_ButonClick(Me.TManager1.btnSearch, New EventArgs())
            End Select
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
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
            Me.ClearText()
        Catch ex As Exception
            Me.UndoCriteria() : MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_ChangesCriteria(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.ChangesCriteria
        Try
            If Me.Isloadingrow Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.TManager1.txtSearch.Enabled = True
            If Me.isUndoingCriteria Then : Return : End If
            Select Case Me.TManager1.btnCriteria.CompareOperator
                Case CompareOperator.BeginWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
                    Me.setVisibleDateControl(False)
                Case CompareOperator.EndWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                    Me.setVisibleDateControl(False)
                Case CompareOperator.Equal
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                    Me.setVisibleDateControl(False)
                Case CompareOperator.Greater
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                    Me.setVisibleDateControl(False)
                Case CompareOperator.GreaterOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                    Me.setVisibleDateControl(False)
                Case CompareOperator.In
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.In
                    Me.setVisibleDateControl(False)
                Case CompareOperator.Less
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                    Me.setVisibleDateControl(False)
                Case CompareOperator.LessOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                    Me.setVisibleDateControl(False)
                Case CompareOperator.Like
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                    Me.setVisibleDateControl(False)
                Case CompareOperator.NotEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
                    Me.setVisibleDateControl(False)
                Case CompareOperator.Between
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BetWeen
                    Me.TManager1.txtSearch.Text = ""
                    Me.TManager1.txtSearch.Enabled = False
                    Me.setVisibleDateControl(True)
                Case Else
                    Throw New Exception("Please select another operator")
            End Select
            Me.TManager1_ButonClick(Me.TManager1.btnSearch, New EventArgs())
            Me.SetOriginalCriteria()
        Catch ex As Exception
            Me.UndoCriteria()
            Me.Isloadingrow = False
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Criteria_Changed")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_ChekAllDataChandge(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.ChekAllDataChandge
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.IsloadedForm Then : Return : End If
            If Me.Isloadingrow Then : Return : End If
            If Not IsNothing(Me.ChekedBrandPackIDS) Then
                Me.ChekedBrandPackIDS.Clear()
            End If
            Dim IsIncludedDPD As Boolean = False
            If Me.TManager1.CHKSelectAll.Checked Then
                IsIncludedDPD = True
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.TManager1.GridEX1.GetRows()
                    Dim PriceTag As String = row.Cells("PRICE_TAG").Value.ToString()
                    row.BeginEdit()
                    row.Cells("IncludeDPD").Value = IsIncludedDPD
                    row.EndEdit()
                    If Not Me.ChekedBrandPackIDS.Contains(PriceTag) Then
                        Me.ChekedBrandPackIDS.Add(PriceTag)
                    End If
                Next
            Else
                IsIncludedDPD = False
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.TManager1.GridEX1.GetRows()
                    Dim PriceTag As String = row.Cells("PRICE_TAG").Value.ToString()
                    If Not IsNothing(row.Cells("IncludeDPD").Value) And Not IsDBNull(row.Cells("IncludeDPD").Value) Then
                        ''update data di server
                        row.BeginEdit()
                        row.Cells("IncludeDPD").Value = IsIncludedDPD
                        row.EndEdit()
                        If Not Me.ChekedBrandPackIDS.Contains(PriceTag) Then
                            Me.ChekedBrandPackIDS.Add(PriceTag)
                        End If
                    End If
                Next
            End If
            Me.clsPrice.UpdateIncludedDPD(Me.ChekedBrandPackIDS, IsIncludedDPD)
            Me.ChekedBrandPackIDS.Clear() : Me.TManager1.GridEX1.UpdateData()
        Catch ex As Exception
            Me.ChekedBrandPackIDS.Clear() : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_CmbSelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.CmbSelectedIndexChanged
        Try
            If IsLoadingCombo Then : Return : End If
            If Me.IsloadedForm Then : Return : End If
            Me.Cursor = Cursors.WaitCursor : If TManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            Select Case Me.TManager1.cbCategory.Text
                Case "BRANDPACK_NAME", "PLANTATION"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
                Case "START_DATE"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.DateTime
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
            If Not CMain.IsSystemAdministrator Then
                If Not NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PlantationPrice Then
                    e.Cancel = True
                    Me.ShowMessageInfo("You have no access to delete data.") : Return
                End If
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True : Return
            End If
            Me.Cursor = Cursors.WaitCursor
            'Dim EndDate As Object = Nothing
            'If (Not IsNothing(Me.TManager1.GridEX1.GetValue("END_DATE")) And Not IsDBNull(Me.TManager1.GridEX1.GetValue("END_DATE"))) Then
            '    EndDate = Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("END_DATE"))
            'End If
            Dim BSucced As Boolean = Me.clsPrice.DeletePlantationPrice(Me.TManager1.GridEX1.GetValue("IDApp").ToString(), Me.TManager1.GridEX1.GetValue("PRICE_TAG"))
            If Not BSucced Then
                e.Cancel = True
                Return
            Else
                e.Cancel = False
            End If
            Me.mcbBrandPack.Text = "" : Me.txtPrice.Value = 0 : Me.dtPicStartDate.Value = DateTime.Now
            Me.chkIncludeDPD.Checked = False
        Catch ex As Exception
            e.Cancel = True : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles TManager1.CellUpdated
        Try
            If Me.IsloadedForm Then : Return : End If
            If Me.Isloadingrow Then : Return : End If
            If e.Column.Key = "IncludeDPD" Then
                Me.Cursor = Cursors.WaitCursor
                Dim IsIncludedDPD As Boolean = False
                If Not IsNothing(Me.TManager1.GridEX1.GetValue("IncludeDPD")) And Not IsNothing(Me.TManager1.GridEX1.GetValue("IncludeDPD")) Then
                    IsIncludedDPD = CBool(Me.TManager1.GridEX1.GetValue("IncludeDPD"))
                    ''update data di server
                    Dim listPriceTag As New List(Of String)
                    listPriceTag.Add(Me.TManager1.GridEX1.GetValue("PRICE_TAG").ToString())
                    Me.clsPrice.UpdateIncludedDPD(listPriceTag, IsIncludedDPD)
                End If
            End If
            Me.TManager1.GridEX1.UpdateData()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_GridCurrentCell_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.GridCurrentCell_Changed
        Try
            Me.Cursor = Cursors.WaitCursor
            'Application.DoEvents()
            If Me.TManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            If (Me.TManager1.GridEX1.SelectedItems.Count <= 0) Then : Return : End If
            If Not Me.TManager1.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                'clear control
                Me.IsLoadingCombo = True
                Me.mcbBrandPack.Value = Nothing
                'Me.mcbPlantation.Value = Nothing
                Me.txtPrice.Value = 0
                Me.chkIncludeDPD.Checked = False
                Me.ListOriginalPriceTag.Clear()
                Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                Me.btnSave.Text = "&Save"
                Return
            End If
            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
            Me.IsSelecttedRowFromGrid = True
            Me.PullAndShowData()
            Me.btnSave.Text = "&Update"
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_GridCurrentCell_Changed")
            'Me.ShowMessageInfo(ex.Message)
        Finally
            Me.IsSelecttedRowFromGrid = False : Me.IsLoadingCombo = False : Me.Cursor = Cursors.Default
        End Try
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
            e.Handled = True : Return
        End If
    End Sub

    Private Sub ButtonSearch1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeacrhBrandPack.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.FillComboBrandPack(Me.mcbBrandPack.Text)
            Dim ItemCount As Integer = Me.mcbBrandPack.DropDownList.RecordCount()
            Me.ShowMessageInfo(ItemCount.ToString() & " item(s) found")
            Me.mcbBrandPack.Focus()
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_btnSeacrhBrandPack_btnClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub btnSearchPlantation_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        Dim DV As DataView = Nothing
    '        Using PL As New NufarmBussinesRules.Plantation.Plantation
    '            PL.GetPlantation(Me.mcbPlantation.Text, DV)
    '        End Using
    '        Me.FillComboPlantation(DV)
    '        Dim ItemCount As Integer = Me.mcbPlantation.DropDownList.RecordCount()
    '        Me.ShowMessageInfo(ItemCount.ToString() & " item(s) found")
    '    Catch ex As Exception
    '        Me.ShowMessageInfo(ex.Message)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Me.Cursor = Cursors.WaitCursor
        Me.IsLoadingCombo = True
        Me.mcbBrandPack.Text = "" : Me.mcbBrandPack.Value = Nothing
        'Me.mcbPlantation.Value = Nothing : Me.mcbPlantation.Text = ""
        Me.txtPrice.Value = 0
        Me.chkIncludeDPD.Checked = False

        Me.mcbBrandPack.ReadOnly = False
        'Me.mcbPlantation.ReadOnly = False

        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert

        Me.IsLoadingCombo = False
        Me.btnSave.Text = "&Save"
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            ''check priviledge
            If Not CMain.IsSystemAdministrator Then
                Select Case Me.Mode
                    Case NufarmBussinesRules.common.Helper.SaveMode.Insert
                        If Not NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PlantationPrice Then
                            Me.ShowMessageInfo("You can not do such operation" & vbCrLf & "You have no access priviledge to insert")

                            Return
                        End If
                    Case NufarmBussinesRules.common.Helper.SaveMode.Update
                        If Not NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PlantationPrice Then
                            Me.ShowMessageInfo("You can not do such operation" & vbCrLf & "You have no access priviledge to insert")
                            Return
                        End If
                End Select
            End If
            Me.Cursor = Cursors.WaitCursor
            If Not Me.IsValid Then
                Me.Cursor = Cursors.Default : Return
            End If
            With Me.clsPrice
                Dim BrandPackID As String = Me.mcbBrandPack.Value.ToString()
                'Dim PlantationID As String = Me.mcbPlantation.Value.ToString()
                .BRANDPACK_ID = BrandPackID
                .PRICE = Me.txtPrice.Value
                If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
                    .IDApp = Me.IDApp
                End If
                '.PLANTATION_ID = PlantationID
                .MustIncludeDPD = Me.chkIncludeDPD.Checked
                Dim StartDate As String = Me.dtPicStartDate.Value.Day.ToString() + "/" + _
                Me.dtPicStartDate.Value.Month.ToString + "/" + Me.dtPicStartDate.Value.Year.ToString()
                Dim EndDate As String = ""
                Dim PRICE_TAG As String = "GP|" + BrandPackID + "|" + StartDate
                .PRICE_TAG = PRICE_TAG
                .START_DATE = Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString())
                .SaveData(Me.Mode)
                Me.PageIndex = 1
                Me.Isloadingrow = True
                Me.GetData() : Me.SetOriginalCriteria() : Me.ListOriginalPriceTag.Clear()
                'FILTER DATA BERDASARKAN DATA YANG DI INPUT
                'Dim DV As DataView = CType(Me.TManager1.GridEX1.DataSource, DataView)
                'DV.RowFilter = "BRANDPACK_ID = '" & BrandPackID & "' AND PLANTATION_ID = '" & PlantationID & "'"
                'Me.TManager1.GridEX1.DataSource = DV : Me.TManager1.GridEX1.RetrieveStructure()
                'If Not Me.IsloadedForm Then : Me.FormatDataGrid() : End If
                'Dim fc As New Janus.Windows.GridEX.GridEXFilterCondition()
                'fc.Value1 = PlantationID
                'fc.ConditionOperator = Janus.Windows.GridEX.ConditionOperator.Equal
                'fc.Column = Me.TManager1.GridEX1.RootTable.Columns("PLANTATION_ID")
                'Me.TManager1.GridEX1.RootTable.ApplyFilter(fc)
            End With
            Me.btnAddNew_Click(Me.btnAddNew, New EventArgs())
            If Not IsNothing(Me.TManager1.GridEX1.DataSource) Then
                If Me.TManager1.GridEX1.RecordCount > 0 Then
                    Me.TManager1.GridEX1.MoveToRowIndex(-1)
                End If
            End If
            Me.Isloadingrow = False
            'Me.ShowMessageInfo(Me.MessageSavingSucces)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.Isloadingrow = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
