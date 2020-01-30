Public Class PlantationManager
    Private m_Criteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private OriginalCriteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private isUndoingCriteria As Boolean = False
    Private m_DataType As NufarmBussinesRules.common.Helper.DataTypes = NufarmBussinesRules.common.Helper.DataTypes.VarChar
    Private Mode As NufarmBussinesRules.common.Helper.SaveMode = NufarmBussinesRules.common.Helper.SaveMode.Insert
    Private IsloadedForm As Boolean = False : Private Isloadingrow As Boolean = True
    Private PageIndex As Int32 = 0, PageSize As Integer = 0, TotalIndex As Integer = 0, RowCount As Integer = 0
    Private isLoadingCombo As Boolean = False
    Private OriginalData As New NufarmBussinesRules.common.dataTManager()
    Friend CMain As Main = Nothing
    Private IsSavingData As Boolean = False
    Private clsPlantation As New NufarmBussinesRules.Plantation.Plantation()
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.PlantationPrice = True Then
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If (NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PlantationPrice = True) And (NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.PlantationPrice = True) Then
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_INSERT.PlantationPrice = True Then
                Me.btnAddNew.Visible = True
                Me.btnSave.Visible = True
            Else
                Me.btnAddNew.Visible = False
                Me.btnSave.Visible = False
            End If
        End If
    End Sub

    Private Function IsValid() As Boolean
        If Me.txtEstate.Text = "" Then
            Me.baseTooltip.Show("Please enter Plantation name", Me.txtEstate, 2500) : Me.txtEstate.Focus() : Return False
            'ElseIf Me.mcbTerritory.Value Is Nothing Or Me.mcbTerritory.SelectedIndex <= -1 Then
            '    Me.baseTooltip.Show("Please define territory", Me.mcbTerritory, 2500) : Me.mcbTerritory.Focus() : Return False
            'ElseIf Me.chkTerritory.CheckedValues Is Nothing Then
            '    Me.baseTooltip.Show("Please define territory", chkTerritory, 2500) : Me.chkTerritory.Focus() : Return False
            'ElseIf Me.chkTerritory.CheckedValues.Length <= 0 Then
            '    Me.baseTooltip.Show("Please define territory", chkTerritory, 2500) : Me.chkTerritory.Focus() : Return False
        End If
        Return True
    End Function
    Friend Sub LoadData()
        isLoadingCombo = True : Me.IsloadedForm = True
        Dim DV As DataView = Me.clsPlantation.GetTerritory("")
        'Me.FillCombo(Me.mcbTerritory, DV)
        Me.FilChekedCombo(DV)
        With Me.TManager1.cbCategory
            .Items.Add("PLANTATION_NAME") : .Items.Add("GROUP_NAME") : .Items.Add("TERRITORY_AREA")
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
                .SearchBy = "PLANTATION_NAME"
            ElseIf Me.TManager1.cbCategory.Text = "GROUP_NAME" Then
                .SearchBy = "PLANT_GROUP_NAME"
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
            SearchBy = "PLANTATION_NAME"
        ElseIf Me.TManager1.cbCategory.Text = "GROUP_NAME" Then : SearchBy = "PLANT_GROUP_NAME"
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
    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String, ByVal HasChangeCriteriaSearch As Boolean)
        Dim Dv As DataView = Nothing : Me.Isloadingrow = True

        Dv = Me.clsPlantation.PopulateQuery(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, _
         Me.m_Criteria, Me.m_DataType, HasChangeCriteriaSearch)
        Me.Isloadingrow = True
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
        'Me.TManager1.GridEX1.AutoSizeColumns()
        Me.TManager1.GridEX1.RootTable.Columns("TERRITORY_ID").Visible = False
        Me.TManager1.GridEX1.RootTable.Columns("GROUP_ID").Visible = False
        Me.TManager1.GridEX1.RootTable.Columns("DESCRIPTIONS").Visible = False
        'TManager1.GridEX1.RootTable.Columns("BRANDPACK_ID").Visible = False
        'TManager1.GridEX1.RootTable.Columns("BRANDPACK_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        'TManager1.GridEX1.RootTable.Columns(0).Visible = False
        TManager1.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        'TManager1.GridEX1.RootTable.Columns(0).Visible = False
        TManager1.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        TManager1.GridEX1.RootTable.Caption = "List Plantation"
        TManager1.GridEX1.TableHeaderFormatStyle.ForeColor = Color.Maroon
        TManager1.GridEX1.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
        If (Me.FilterEditor1.Visible) And (Not IsNothing(Me.FilterEditor1.SourceControl)) Then
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
        Else
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        End If
        Me.TManager1.GridEX1.AutoSizeColumns(Me.TManager1.GridEX1.RootTable)
        Me.TManager1.GridEX1.Refresh()
    End Sub
    Private Sub FillCombo(ByVal cmb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal Dv As DataView)
        Me.isLoadingCombo = True : cmb.Text = ""
        cmb.SetDataBinding(Dv, "") : cmb.DropDownList.RetrieveStructure()
        If Dv Is Nothing Then : Me.isLoadingCombo = False : Return : End If
        Select Case cmb.Name
            Case "mcbTerritory"
                cmb.DisplayMember = "TERRITORY_AREA" : cmb.ValueMember = "TERRITORY_ID"
            Case "mcbGroupPlantation"
                cmb.DisplayMember = "GROUP_NAME" : cmb.ValueMember = "GROUP_ID"
                cmb.DropDownList.Columns("DESCRIPTIONS").Visible = False
        End Select
        cmb.DroppedDown = True
        For Each col As Janus.Windows.GridEX.GridEXColumn In cmb.DropDownList.Columns
            col.AutoSize()
        Next
        cmb.Value = Nothing
        cmb.DroppedDown = False
        Me.isLoadingCombo = False
    End Sub
    Private Sub FilChekedCombo(ByVal Dv As DataView)
        Me.isLoadingCombo = True : Me.chkTerritory.Text = ""
        Me.chkTerritory.DropDownDataSource = Dv
        If Dv Is Nothing Then : Me.isLoadingCombo = False : Return : End If
        'Select Case cmb.Name
        '    Case "mcbTerritory"
        '        cmb.DisplayMember = "TERRITORY_AREA" : cmb.ValueMember = "TERRITORY_ID"
        '    Case "mcbGroupPlantation"
        '        cmb.DisplayMember = "GROUP_NAME" : cmb.ValueMember = "GROUP_ID"
        '        cmb.DropDownList.Columns("DESCRIPTIONS").Visible = False
        'End Select 

        Me.chkTerritory.DropDownList.RetrieveStructure()
        Me.chkTerritory.DropDownList.Columns("TERRITORY_ID").ShowRowSelector = True
        'Me.chkTerritory.DropDownList.Columns("TERRITORY_ID").UseHeaderSelector = True
        Me.chkTerritory.DroppedDown = True

        Me.chkTerritory.DropDownDisplayMember = "TERRITORY_AREA"
        Me.chkTerritory.DropDownValueMember = "TERRITORY_ID"

        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.chkTerritory.DropDownList.Columns
            col.AutoSize()
        Next
        Me.chkTerritory.Values = Nothing : Me.chkTerritory.Text = ""
        Me.chkTerritory.DroppedDown = False
        Me.isLoadingCombo = False
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
                    .btnCriteria.CompareOperators = NuFarm.Common.GUI.ToggleButton.CompareOperator.BeginWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                    .btnCriteria.Text = "*|"
                    .btnCriteria.CompareOperators = NuFarm.Common.GUI.ToggleButton.CompareOperator.EndWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                    .btnCriteria.Text = "="
                    .btnCriteria.CompareOperators = NuFarm.Common.GUI.ToggleButton.CompareOperator.Equal
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                    .btnCriteria.Text = ">"
                    .btnCriteria.CompareOperators = NuFarm.Common.GUI.ToggleButton.CompareOperator.Greater
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                    .btnCriteria.Text = ">="
                    .btnCriteria.CompareOperators = NuFarm.Common.GUI.ToggleButton.CompareOperator.GreaterOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.In
                    .btnCriteria.Text = "*|*"
                    .btnCriteria.CompareOperators = NuFarm.Common.GUI.ToggleButton.CompareOperator.In
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.In
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                    .btnCriteria.Text = "<"
                    .btnCriteria.CompareOperators = NuFarm.Common.GUI.ToggleButton.CompareOperator.Less
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                    .btnCriteria.Text = "<="
                    .btnCriteria.CompareOperators = NuFarm.Common.GUI.ToggleButton.CompareOperator.LessOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                    .btnCriteria.Text = "*.*"
                    .btnCriteria.CompareOperators = NuFarm.Common.GUI.ToggleButton.CompareOperator.Like
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                Case NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
                    .btnCriteria.Text = "<>"
                    .btnCriteria.CompareOperators = NuFarm.Common.GUI.ToggleButton.CompareOperator.NotEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
            End Select
        End With
        Me.isUndoingCriteria = False
    End Sub
    Private Sub SetOriginalCriteria()
        Select Case Me.TManager1.btnCriteria.CompareOperators
            Case NuFarm.Common.GUI.ToggleButton.CompareOperator.BeginWith
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
            Case NuFarm.Common.GUI.ToggleButton.CompareOperator.EndWith
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
            Case NuFarm.Common.GUI.ToggleButton.CompareOperator.Equal
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
            Case NuFarm.Common.GUI.ToggleButton.CompareOperator.Greater
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
            Case NuFarm.Common.GUI.ToggleButton.CompareOperator.GreaterOrEqual
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
            Case NuFarm.Common.GUI.ToggleButton.CompareOperator.In
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.In
            Case NuFarm.Common.GUI.ToggleButton.CompareOperator.Less
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Less
            Case NuFarm.Common.GUI.ToggleButton.CompareOperator.LessOrEqual
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
            Case NuFarm.Common.GUI.ToggleButton.CompareOperator.Like
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
            Case NuFarm.Common.GUI.ToggleButton.CompareOperator.NotEqual
                Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.NotEqual
        End Select
    End Sub
    Private Sub TManager1_ChangesCriteria(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.ChangesCriteria
        Try
            If IsloadedForm Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            If Me.isUndoingCriteria Then : Return : End If
            Select Case Me.TManager1.btnCriteria.CompareOperators
                Case NuFarm.Common.GUI.ToggleButton.CompareOperator.BeginWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.BeginWith
                Case NuFarm.Common.GUI.ToggleButton.CompareOperator.EndWith
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.EndWith
                Case NuFarm.Common.GUI.ToggleButton.CompareOperator.Equal
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Equal
                Case NuFarm.Common.GUI.ToggleButton.CompareOperator.Greater
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Greater
                Case NuFarm.Common.GUI.ToggleButton.CompareOperator.GreaterOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.GreaterOrEqual
                Case NuFarm.Common.GUI.ToggleButton.CompareOperator.In
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.In
                Case NuFarm.Common.GUI.ToggleButton.CompareOperator.Less
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Less
                Case NuFarm.Common.GUI.ToggleButton.CompareOperator.LessOrEqual
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.LessOrEqual
                Case NuFarm.Common.GUI.ToggleButton.CompareOperator.Like
                    Me.m_Criteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
                Case NuFarm.Common.GUI.ToggleButton.CompareOperator.NotEqual
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
            If IsLoadingCombo Then : Return : End If
            If Me.IsloadedForm Then : Return : End If
            Me.Cursor = Cursors.WaitCursor : If TManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            Select Case Me.TManager1.cbCategory.Text
                Case "DISTRIBUTOR_NAME", "BRANDPACK_NAME", "PLANTATION"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
                Case "START_DATE"
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
    Private Sub ButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles TManager1.ButonClick
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
    Private Sub ClearEntry()
        Me.txtEstate.Text = "" : Me.lblPlantationID.Text = "<< AutoGenerated >>"
        Me.isLoadingCombo = True
        Me.chkTerritory.UncheckAll() : Me.chkTerritory.Text = ""
        Me.mcbGroupPlantation.Value = Nothing
        Me.mcbGroupPlantation.ReadOnly = False
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.isLoadingCombo = True
            ClearEntry() : Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
            Me.btnSave.Text = "&Insert"
        Catch ex As Exception

        Finally
            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert : Me.isLoadingCombo = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub InflateData()
        Me.lblPlantationID.Text = Me.TManager1.GridEX1.GetValue("PLANTATION_ID").ToString()
        Me.txtEstate.Text = Me.TManager1.GridEX1.GetValue("PLANTATION_NAME").ToString()
        If (Not IsNothing(Me.TManager1.GridEX1.GetValue("TERRITORY_ID"))) And (Not IsDBNull(Me.TManager1.GridEX1.GetValue("TERRITORY_ID"))) Then
            Me.chkTerritory.UncheckAll()
            Dim val(1) As String
            val(0) = Me.TManager1.GridEX1.GetValue("TERRITORY_ID").ToString()
            Me.chkTerritory.CheckedValues = val
        Else : Me.chkTerritory.UncheckAll()
        End If
        Me.mcbGroupPlantation.Value = Nothing : Me.mcbGroupPlantation.Text = ""
        If Not IsNothing(Me.TManager1.GridEX1.GetValue("GROUP_ID")) And Not IsDBNull(Me.TManager1.GridEX1.GetValue("GROUP_ID")) Then
            Me.mcbGroupPlantation.Value = Me.TManager1.GridEX1.GetValue("GROUP_ID")
        End If
        'ISI group plantation berdasarkan data di grid ex
        If Me.mcbGroupPlantation.SelectedIndex <= -1 Then
            If Not Me.TManager1.GridEX1.GetValue("GROUP_ID") Is DBNull.Value Then
                Dim dtTable As DataTable = Nothing
                If Not IsNothing(Me.mcbGroupPlantation.DataSource) Then
                    dtTable = CType(Me.mcbGroupPlantation.DataSource, DataView).ToTable()
                    dtTable.Rows.Clear()
                Else
                    dtTable = New DataTable("t_Group_Plantation")
                    With dtTable
                        .Columns.Add("GROUP_ID", Type.GetType("System.String"))
                        .Columns.Add("GROUP_NAME", Type.GetType("System.String"))
                        .Columns.Add("DESCRIPTIONS", Type.GetType("System.String"))
                    End With
                End If
                Dim Drow As DataRow = dtTable.NewRow()
                Drow("GROUP_ID") = Me.TManager1.GridEX1.GetValue("GROUP_ID")
                Drow("GROUP_NAME") = Me.TManager1.GridEX1.GetValue("GROUP_NAME")
                Drow("DESCRIPTIONS") = Me.TManager1.GridEX1.GetValue("DESCRIPTIONS")
                dtTable.Rows.Add(Drow) : dtTable.AcceptChanges()
                Me.FillCombo(Me.mcbGroupPlantation, dtTable.DefaultView())
                Me.mcbGroupPlantation.Value = Me.TManager1.GridEX1.GetValue("GROUP_ID")
            End If
        End If
        'Me.mcbGroupPlantation.ReadOnly = True
        Me.chkTerritory.ReadOnly = False
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Not Me.IsValid() Then : Return : End If
            With Me.clsPlantation
                .PlantationName = RTrim(Me.txtEstate.Text)
                If Not IsNothing(Me.mcbGroupPlantation.Value) And Me.mcbGroupPlantation.SelectedIndex <> -1 Then
                    .PlantGroupID = Me.mcbGroupPlantation.Value.ToString()
                End If
                .ListTerritory = New List(Of String)
                If Not IsNothing(Me.chkTerritory.CheckedValues) Then
                    If Me.chkTerritory.CheckedValues.Length > 0 Then
                        Dim ListTerritory As New List(Of String)
                        For i As Integer = 0 To Me.chkTerritory.CheckedValues.Length - 1
                            If Not IsNothing(Me.chkTerritory.CheckedValues().GetValue(i)) Then
                                ListTerritory.Add(Me.chkTerritory.CheckedValues().GetValue(i).ToString())
                            End If
                        Next
                        .ListTerritory = ListTerritory
                    End If
                End If
                '.TerritoryID = Me.mcbTerritory.Value.ToString()

                Dim plantID As String = ""

                If Me.lblPlantationID.Text <> "<< AutoGenerated >>" And Me.lblPlantationID.Text <> "" Then
                    plantID = Me.lblPlantationID.Text
                    .SaveData(Me.Mode, False, plantID)
                Else
                    .SaveData(Mode, False)
                End If
            End With
            Me.IsSavingData = True : Me.ClearEntry()
            Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnSave_Click")
        Finally
            Me.IsSavingData = False : Me.Isloadingrow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.Enter
        Me.AcceptButton = Me.TManager1.btnSearch
    End Sub

    Private Sub Panel1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel1.Enter
        Me.AcceptButton = Me.btnSave
    End Sub

    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = CType(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnPageSettings"
                    Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                    Me.PageSetupDialog1.ShowDialog(Me)
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
                Case "btnSettingGrid"
                    Dim SetGrid As New SettingGrid()
                    SetGrid.Grid = Me.TManager1.GridEX1
                    SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                    SetGrid.ShowDialog(Me)
                Case "btnShowFieldChooser"
                    Me.TManager1.GridEX1.ShowFieldChooser(Me)
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.TManager1.GridEX1.RemoveFilters()
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                Case "btnCustomFilter"
                    Me.FilterEditor1.SourceControl = Me.TManager1.GridEX1
                    Me.TManager1.GridEX1.RemoveFilters()
                    Me.FilterEditor1.Visible = True
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
                Case "btnExport"
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
                    Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
                Case "btnCustomFilter"
                    Me.FilterEditor1.SourceControl = Me.TManager1.GridEX1
                    Me.TManager1.GridEX1.RemoveFilters()
                    Me.FilterEditor1.Visible = True
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
            End Select
        Catch ex As Exception
            Me.Isloadingrow = False : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub btnSearchGroupPlantation_btnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchGroupPlantation.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim DV As DataView = Me.clsPlantation.GetGroupPlantation(Me.mcbGroupPlantation.Text)
            Me.FillCombo(Me.mcbGroupPlantation, DV)
            Dim itemCount As Integer = Me.mcbGroupPlantation.DropDownList.RecordCount
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_btnSearchGroupPlantation_btnClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mcbTerritory_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.isLoadingCombo Then : Return : End If
            If Me.chkTerritory.CheckedValues Is Nothing Then : Return : End If
            If Me.chkTerritory.CheckedValues.Length <= 0 Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            ''CARI DATA GROUP PLANTATION yang mana territory nya yang di pilih di mcb ini
            Dim ListTerritory As New List(Of String)
            For i As Integer = 0 To Me.chkTerritory.CheckedValues.Length - 1
                If Not ListTerritory.Contains(Me.chkTerritory.CheckedValues().GetValue(i).ToString()) Then
                    ListTerritory.Add(Me.chkTerritory.CheckedValues().GetValue(i).ToString())
                End If
            Next

            Dim DV As DataView = Me.clsPlantation.GetGroupPlantation("", ListTerritory)

            Me.FillCombo(Me.mcbGroupPlantation, DV) : chkTerritory.ReadOnly = False
        Catch ex As Exception
            Me.isLoadingCombo = False : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_mcbTerritory_ValueChange")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtEstate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEstate.TextChanged
        If Me.txtEstate.Text = "" Then
            Me.chkTerritory.ReadOnly = True : Me.mcbGroupPlantation.ReadOnly = True
            Me.isLoadingCombo = True : Me.chkTerritory.UncheckAll() : Me.chkTerritory.Text = "" : Me.mcbGroupPlantation.Value = Nothing
            Me.mcbGroupPlantation.Text = ""
            Me.btnSearchGroupPlantation.Enabled = False : Me.btnSave.Enabled = False
            Me.isLoadingCombo = False
        Else
            Me.chkTerritory.ReadOnly = False : Me.mcbGroupPlantation.ReadOnly = False
            Me.btnSearchGroupPlantation.Enabled = True : Me.btnSave.Enabled = True
            Me.AcceptButton = Me.btnSave
        End If

    End Sub

    Private Sub TManager1_DeleteGridRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles TManager1.DeleteGridRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim boolSucced As Boolean = Me.clsPlantation.Delete(Me.TManager1.GridEX1.GetValue("PLANTATION_ID").ToString())
            If boolSucced Then : e.Cancel = False
            Else : e.Cancel = True
            End If
            Me.TManager1.GridEX1.UpdateData()
            If (Me.TManager1.GridEX1.RecordCount <= 0) Then
                Me.txtEstate.Text = "" : Me.lblPlantationID.Text = "<< AutoGenerated >>"
                Me.chkTerritory.UncheckAll() : Me.chkTerritory.Text = """"
                Me.mcbGroupPlantation.Value = Nothing : Me.mcbGroupPlantation.Text = ""
            End If
        Catch ex As Exception
            e.Cancel = True : Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_TManager1_DeleteGridRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TManager1_GridCurrentCell_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles TManager1.GridCurrentCell_Changed
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
            If Me.TManager1.GridEX1.RecordCount > 0 Then
                If Me.TManager1.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                    Me.InflateData() : Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
                    Me.btnSave.Text = "&Update"
                Else
                    Me.isLoadingCombo = True : Me.lblPlantationID.Text = ""
                    Me.txtEstate.Text = "" : Me.chkTerritory.UncheckAll() : Me.chkTerritory.Text = "" : Me.mcbGroupPlantation.Value = Nothing : Me.mcbGroupPlantation.Text = ""
                    Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                    Me.btnSave.Text = "&Insert"
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, "_TManager1_GridCurrentCell_Changed")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PlantationManager_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsPlantation) Then
                Me.clsPlantation.Dispose(True)
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnNew_PicClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.PicClick
        Try
            Me.Cursor = Cursors.WaitCursor
            'CHECK Apakah territory sudah di plilih / belum
            'If Me.mcbTerritory.Value Is Nothing Then
            '    Me.mcbTerritory.DroppedDown = True : Me.mcbTerritory.Focus() : Me.baseTooltip.Show("Please define territory", Me.mcbTerritory, 2500) : Return
            'End If
            If Me.chkTerritory.CheckedValues Is Nothing Then
                Me.baseTooltip.Show("Please define territory", Me.chkTerritory, 2500) : Return
            ElseIf Me.chkTerritory.CheckedValues.Length <= 0 Then
                Me.baseTooltip.Show("Please define territory", Me.chkTerritory, 2500) : Return
            End If
            Dim dt As New DataTable("T_Result") : dt.Clear()
            Dim frmPlantGroup As New PlantationGroup()
            Dim dgResult As DialogResult = Windows.Forms.DialogResult.None
            While dgResult = Windows.Forms.DialogResult.None
                dgResult = frmPlantGroup.ShowDialog(dt)
            End While
            If dgResult = Windows.Forms.DialogResult.OK Then
                Dim DV As DataView = dt.DefaultView()
                Me.mcbGroupPlantation.ReadOnly = False
                Me.FillCombo(Me.mcbGroupPlantation, DV)
                Me.mcbGroupPlantation.Value = dt.Rows(0)("GROUP_ID")
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + " _btnNew_PicClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PlantationManager_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
        Me.mcbGroupPlantation.ReadOnly = True : Me.chkTerritory.ReadOnly = True
        Me.btnSave.Enabled = False : Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert

        Me.TManager1.GridEX1.AutoSizeColumns(Me.TManager1.GridEX1.RootTable)
        Me.IsloadedForm = False : Me.Isloadingrow = False
        'Me.TManager1.GridEX1.Refresh()
        Me.ReadAccess()
    End Sub
End Class