Public Class PlantationPrice
    Private m_clsPrice As NufarmBussinesRules.Brandpack.DistributorPriceHistory
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
    Private ListOriginalPriceTag As New List(Of String)
    Private IsChangedValueFromBrandPack As Boolean = False
    Private IsSelecttedRowFromGrid As Boolean = False
    Friend CMain As Main = Nothing
    Dim ChekedBrandPackIDS As New List(Of String)
    Private ReadOnly Property clsPrice() As NufarmBussinesRules.Brandpack.DistributorPriceHistory
        Get
            If (Me.m_clsPrice Is Nothing) Then
                Me.m_clsPrice = New NufarmBussinesRules.Brandpack.DistributorPriceHistory()
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
                Me.btnNewKebun.Enabled = False
            Else
                Me.btnAddNew.Enabled = True
                Me.btnNewKebun.Enabled = True
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
        Me.chkDistributor.UncheckAll() : Me.chkDistributor.CheckedValues = Nothing
        Me.chkDistributor.Text = ""
        Me.mcbPlantation.SetDataBinding(Nothing, "")
        Me.mcbPlantation.Value = Nothing
        Me.mcbPlantation.Text = ""
        Me.btnNewKebun.PictureBox1.Image = Me.btnNewKebun.ImageList1.Images(0)
        Me.txtPrice.Value = 0
        Me.dtPicStartDate.Value = NufarmBussinesRules.SharedClass.ServerDate
        Me.IsLoadingCombo = False
    End Sub
    Private Sub InflateData()
        Dim BRANDPACK_ID As String = TManager1.GridEX1.GetValue("BRANDPACK_ID").ToString()
        Dim PLANTATION_ID As String = TManager1.GridEX1.GetValue("PLANTATION_ID").ToString()
        Dim START_DATE As DateTime = Convert.ToDateTime(TManager1.GridEX1.GetValue("START_DATE"))
        Dim END_DATE As Object = Nothing
        If Me.TManager1.GridEX1.RootTable.Columns.Contains("END_DATE") Then
            END_DATE = Me.TManager1.GridEX1.GetValue("END_DATE")
        End If
        Dim PRICE As Decimal = Convert.ToDecimal(TManager1.GridEX1.GetValue("PRICE"))
        Me.mcbBrandPack.Value = BRANDPACK_ID
        Me.chkDistributor.UncheckAll()
        If Not IsNothing(Me.TManager1.GridEX1.GetValue("IncludeDPD")) And Not IsDBNull(Me.TManager1.GridEX1.GetValue("IncludeDPD")) Then
            Me.chkIncludeDPD.Checked = CBool(Me.TManager1.GridEX1.GetValue("IncludeDPD"))
        Else
            Me.chkIncludeDPD.Checked = False
        End If
        Me.IsLoadingCombo = True
        Me.chkDistributor.DropDownDataSource = Me.clsPrice.getDistributor(BRANDPACK_ID)
        With Me.chkDistributor
            .Text = ""
            .DropDownList.RetrieveStructure()
            .DropDownList.Columns("DISTRIBUTOR_ID").ShowRowSelector = True
            .DropDownList.Columns("DISTRIBUTOR_ID").UseHeaderSelector = True
            If IsChangedValueFromBrandPack Then
                .DroppedDown = True
            End If
            .DropDownList.Columns("DISTRIBUTOR_ID").AutoSize()
            .DropDownList.Columns("DISTRIBUTOR_NAME").AutoSize()
            .DropDownDisplayMember = "DISTRIBUTOR_NAME" : .DropDownValueMember = "DISTRIBUTOR_ID"
            .DroppedDown = False
        End With

        Me.chkDistributor.CheckedValues = Me.clsPrice.GetDistributorsByBrandPack(BRANDPACK_ID, PLANTATION_ID, START_DATE, END_DATE)
        Me.chkDistributor.Update()
        'System.Threading.Thread.Sleep(200)
        Dim ListDistributor As New List(Of String)
        Me.ListOriginalPriceTag.Clear()
        'Dim DVCopyOriginal As DataView = CType(Me.TManager1.GridEX1.DataSource, DataView).ToTable().Copy().DefaultView()
        For i As Integer = 0 To Me.chkDistributor.CheckedValues.Length - 1
            ListDistributor.Add(Me.chkDistributor.CheckedValues.GetValue(i).ToString())
            'Dim StartDate As String = "", EndDate As String = ""
            'If Not IsNothing(START_DATE) Then
            '    StartDate = START_DATE.Day.ToString() + "/" + START_DATE.Month.ToString + "/" + START_DATE.Year.ToString()
            'End If
            'If Not IsNothing(END_DATE) And Not IsDBNull(END_DATE) Then
            '    EndDate = Convert.ToDateTime(END_DATE).Day.ToString() + "/" + Convert.ToDateTime(END_DATE).Month.ToString + "/" + Convert.ToDateTime(END_DATE).Year.ToString()
            'End If

            'If Not IsNothing(END_DATE) And Not IsDBNull(END_DATE) Then
            '    PRICE_TAG = BRANDPACK_ID + "|" + StartDate + "|" + EndDate + "|" + ListDistributor(i).ToString() + "|" + PLANTATION_ID
            'Else
            '    PRICE_TAG = BRANDPACK_ID + "|" + StartDate + "|" + ListDistributor(i).ToString() + "|" + PLANTATION_ID
            'End If
            'Dim PRICE_TAG As String = ""
            'If Not ListOriginalPriceTag.Contains(PRICE_TAG) Then
            '    ListOriginalPriceTag.Add(PRICE_TAG)
            'End If
        Next
        'DVCopyOriginal.RowFilter = "PLANTATION_ID = '" & Me.TManager1.GridEX1.GetValue("PLANTATION_ID").ToString() & "'"
        'If DVCopyOriginal.Count > 0 Then
        '    For i As Integer = 0 To DVCopyOriginal.Count - 1
        '        Dim PRICE_TAG As String = DVCopyOriginal(i)("PRICE_TAG").ToString()
        '        If Not ListOriginalPriceTag.Contains(PRICE_TAG) Then
        '            ListOriginalPriceTag.Add(PRICE_TAG)
        '        End If
        '    Next
        'End If
        Dim PRICE_TAG As String = Me.TManager1.GridEX1.GetValue("PRICE_TAG").ToString() 'DVCopyOriginal(i)("PRICE_TAG").ToString()
        If Not ListOriginalPriceTag.Contains(PRICE_TAG) Then
            ListOriginalPriceTag.Add(PRICE_TAG)
        End If
        Dim DV As DataView = Me.clsPrice.GetPlantation(ListDistributor)
        Me.FillComboPlantation(DV)
        Me.IsLoadingCombo = True
        Me.mcbPlantation.Value = PLANTATION_ID
        Me.dtPicStartDate.Value = START_DATE
        Me.txtPrice.Value = PRICE
        If Not IsNothing(END_DATE) And Not IsDBNull(END_DATE) Then
            Me.dtPicEndDate.Value = Convert.ToDateTime(END_DATE)
        End If
        If Not IsNothing(Me.TManager1.GridEX1.GetValue("IncludeDPD")) And Not IsDBNull(Me.TManager1.GridEX1.GetValue("IncludeDPD")) Then
            Me.chkIncludeDPD.Checked = Convert.ToBoolean(Me.TManager1.GridEX1.GetValue("IncludeDPD"))
        Else
            Me.chkIncludeDPD.Checked = False
        End If
        Me.chkDistributor.ReadOnly = True
        Me.mcbBrandPack.ReadOnly = True
        Me.mcbPlantation.ReadOnly = True
        Dim DistributorID As String = Me.TManager1.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()

        '==============================================================================================================
        If Me.clsPrice.HasReferencedDataPO(PLANTATION_ID, DistributorID, BRANDPACK_ID, START_DATE, END_DATE) Then
            Me.txtPrice.ReadOnly = True
            Me.dtPicEndDate.ReadOnly = True
            Me.dtPicStartDate.ReadOnly = True
        Else
            Me.txtPrice.ReadOnly = False
            Me.dtPicEndDate.ReadOnly = False
            Me.dtPicStartDate.ReadOnly = False
        End If

        Me.chkDistributor.DroppedDown = False
        Application.DoEvents()
        ' ==========================================================================

        Me.IsLoadingCombo = False
    End Sub
    Private Sub FillComboDistributor(ByVal SearchString As String)
        Me.IsLoadingCombo = True
        Dim EndDate As Object = Nothing
        If Me.dtPicEndDate.Text <> "" Then
            EndDate = Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString())
        End If
        Me.chkDistributor.DropDownDataSource = Me.clsPrice.getDistributor(SearchString, Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()), EndDate, Me.mcbBrandPack.Value.ToString())
        With Me.chkDistributor
            .Text = ""
            .DropDownList.RetrieveStructure()
            .DropDownList.Columns("DISTRIBUTOR_ID").ShowRowSelector = True
            .DropDownList.Columns("DISTRIBUTOR_ID").UseHeaderSelector = True
            If IsChangedValueFromBrandPack Then
                .DroppedDown = True
            End If
            .DropDownList.Columns("DISTRIBUTOR_ID").AutoSize()
            .DropDownList.Columns("DISTRIBUTOR_NAME").AutoSize()
            .DropDownDisplayMember = "DISTRIBUTOR_NAME" : .DropDownValueMember = "DISTRIBUTOR_ID"
            .DroppedDown = False
        End With
        Me.IsLoadingCombo = False
    End Sub
    Private Sub FillComboBrandPack(ByVal SearchString As String)
        Me.IsLoadingCombo = True
        Me.mcbBrandPack.DataSource = Me.clsPrice.GetBrandPack(SearchString)
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
    Private Sub FillComboPlantation(ByVal DV As DataView)
        Me.IsLoadingCombo = True
        With Me.mcbPlantation
            .Text = ""
            .DataSource = DV
            If IsNothing(DV) Then : Me.IsLoadingCombo = False : Return : End If
            .DropDownList.RetrieveStructure()
            .DroppedDown = True
            .DisplayMember = "PLANTATION_NAME" : .ValueMember = "PLANTATION_ID"
            .DropDownList.AutoSizeColumns()
            .DroppedDown = False
        End With
        Me.IsLoadingCombo = False
    End Sub
    Friend Sub LoadData()
        IsLoadingCombo = True : Me.IsloadedForm = True
        Me.dtPicStartDate.MaxDate = NufarmBussinesRules.SharedClass.ServerDate
        Me.FillComboBrandPack("")
        'Me.FillComboDistributor("")
        With Me.TManager1.cbCategory
            .Items.Add("BRANDPACK_NAME") : .Items.Add("DISTRIBUTOR_NAME")
            .Items.Add("PLANTATION_NAME") : .Items.Add("START_DATE")
        End With
        Me.PageIndex = 1 : Me.PageSize = 500 : Me.TManager1.cbPageSize.Text = "500"
        Me.TManager1.btnCriteria.Text = "*.*" : Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
        Me.OriginalCriteria = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
        Me.RunQuery("", "BRANDPACK_NAME")
        If TManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub
    Friend Sub GetData()
        RowCount = 0
        Dim SearchString As String = Me.TManager1.txtSearch.Text, SearchBy As String = Me.TManager1.cbCategory.Text
        If (Me.TManager1.cbCategory.Text = "") Then
            SearchBy = "BRANDPACK_NAME"
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
    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String)
        RowCount = 0 : Dim Dv As DataView = Nothing : Me.Isloadingrow = True
        Dv = Me.clsPrice.PopulateQuery(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, _
         Me.m_Criteria, Me.m_DataType)
        If Not IsNothing(Dv) Then
            If (Dv.Count > 0) Then
                Me.TManager1.GridEX1.DataSource = Dv : Me.TManager1.GridEX1.RetrieveStructure()
                Me.FormatDataGrid()
                'If Not Me.IsloadedForm Then

                'Else
                '    Me.TManager1.GridEX1.RootTable.Columns("IncludeDPD").ShowRowSelector = True
                '    Me.TManager1.GridEX1.RootTable.Columns("IncludeDPD").UseHeaderSelector = True
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
        Me.TManager1.GridEX1.RootTable.Columns("IncludeDPD").UseHeaderSelector = True
        Me.TManager1.GridEX1.RootTable.Columns("IDApp").Caption = "NO."
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
            col.AutoSize()
        Next
        TManager1.GridEX1.RootTable.Columns("BRANDPACK_ID").Visible = False
        TManager1.GridEX1.RootTable.Columns("PLANTATION_ID").Visible = False
        TManager1.GridEX1.RootTable.Columns("DISTRIBUTOR_ID").Visible = False
        TManager1.GridEX1.RootTable.Columns("PRICE_TAG").Visible = False
        TManager1.GridEX1.RootTable.Columns("BRANDPACK_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        TManager1.GridEX1.RootTable.Columns(0).Visible = False
        TManager1.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        TManager1.GridEX1.RootTable.Columns(0).Visible = False
        TManager1.GridEX1.TableHeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
        TManager1.GridEX1.RootTable.Caption = "List Price by Plantation"
        TManager1.GridEX1.TableHeaderFormatStyle.ForeColor = Color.Maroon
        TManager1.GridEX1.TableHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
        If (Me.FilterEditor1.Visible) And (Not IsNothing(Me.FilterEditor1.SourceControl)) Then
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.None
        Else
            TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        End If
        Me.TManager1.CHKSelectAll.Visible = True
    End Sub

    Private Function IsValid() As Boolean
        If IsNothing(Me.mcbBrandPack.Value) Then
            Me.baseTooltip.Show("Please define Brandpack", Me.mcbBrandPack, 2500) : Me.mcbBrandPack.Focus() : Return False
        End If
        If Me.mcbBrandPack.SelectedIndex <= -1 Then
            Me.baseTooltip.Show("Please define Brandpack", Me.mcbBrandPack, 2500) : Me.mcbBrandPack.Focus() : Return False
        End If
        If IsNothing(Me.chkDistributor.CheckedValues()) Then
            Me.baseTooltip.Show("Please define Distributor", Me.chkDistributor, 2500) : Me.chkDistributor.Focus() : Return False
        End If
        If Me.chkDistributor.CheckedValues.Length <= 0 Then
            Me.baseTooltip.Show("Please define Distributor", Me.chkDistributor, 2500) : Me.chkDistributor.Focus() : Return False
        End If
        If Me.mcbPlantation.Value Is Nothing Or Me.mcbPlantation.SelectedIndex <= -1 Then
            Me.baseTooltip.Show("Please define Plantation", Me.mcbPlantation, 2500) : Me.mcbPlantation.Focus() : Return False
        End If
        'CHECK end_date
        If Me.dtPicEndDate.Value < Me.dtPicStartDate.Value Then
            Me.baseTooltip.Show("Price periode is not valid", Me.dtPicStartDate, 2500) : Return False
        End If
        If Me.txtPrice.Value Is Nothing Then
            Me.baseTooltip.Show("Please define Price value", Me.txtPrice, 2500) : Return False
        End If
        If Me.txtPrice.Value <= 0 Then
            Me.baseTooltip.Show("Please define Price value", Me.txtPrice, 2500) : Return False
        End If
        'check ID distributor di ID PLANTATION
       
        Return True
    End Function
    Private Sub GridCurrentCell_Changed(ByVal sender As Object, ByVal e As EventArgs) Handles TManager1.GridCurrentCell_Changed
        Try
            Me.Cursor = Cursors.WaitCursor
            'Application.DoEvents()
            If Me.Isloadingrow Then
                Me.btnNewKebun.PictureBox1.Image = Me.btnNewKebun.ImageList1.Images(0)
                Me.btnNewKebun.isEditMode = False
                Return
            Else
                Me.btnNewKebun.PictureBox1.Image = Me.btnNewKebun.ImageList1.Images(1)
                Me.btnNewKebun.isEditMode = True
            End If
            'Application.DoEvents()
            If Me.TManager1.GridEX1.DataSource Is Nothing Then : Return : End If
            If (Me.TManager1.GridEX1.SelectedItems.Count <= 0) Then : Return : End If
            If Not Me.TManager1.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                'clear control
                Me.IsLoadingCombo = True
                Me.mcbBrandPack.Value = Nothing
                Me.chkDistributor.UncheckAll()
                Me.mcbPlantation.Value = Nothing
                Me.txtPrice.Value = 0
                Me.chkIncludeDPD.Checked = False
                Me.ListOriginalPriceTag.Clear()
                Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                Me.btnSave.Text = "&Save"
                Return
            End If
            Me.IsSelecttedRowFromGrid = True
            Me.InflateData()
            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
            Me.btnSave.Text = "&Update"

            'ambil listOkriginalPriceTag

            'dim BRANDPACK_ID AS String = ME.mcbBrandPack.Value.ToString();
            'For i As Integer = 0 To Me.chkDistributor.CheckedValues.Length - 1
            '    ListDistributors.Add(Me.chkDistributor.CheckedValues.GetValue(i).ToString())
            'Next
            'Dim PRICE_TAG As String = BRANDPACK_ID + "|" + Me.START_DATE + "|" + ListDistributors(i).ToString() _
            '                  + "|" + Me.PLANTATION_ID
        Catch ex As Exception
            Me.btnNewKebun.PictureBox1.Image = Me.btnNewKebun.ImageList1.Images(0)
            Me.btnNewKebun.isEditMode = False
            Me.LogMyEvent(ex.Message, Me.Name + "_GridCurrentCell_Changed")
            'Me.ShowMessageInfo(ex.Message)
        Finally
            Me.IsSelecttedRowFromGrid = False : Me.IsLoadingCombo = False : Me.Cursor = Cursors.Default
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
            Me.ClearText()
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

    Private Sub TManager1_TextBoxKeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TManager1.TextBoxKeyPress

        If (Not Char.IsDigit(e.KeyChar)) And (Not Char.IsControl(e.KeyChar)) Then
            e.Handled = True : Return
        End If
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
                    Me.ButtonClick(Me.TManager1.btnSearch, New EventArgs())
            End Select
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SpesialPrice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
        Catch ex As Exception
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, Me.Name + "_SpesialPrice_Load")
            Me.LogMyEvent(ex.Message, Me.Name + "_SpesialPrice_Load")
        Finally
            Me.IsLoadingCombo = False
            Try
                Me.FormatDataGrid() : Isloadingrow = False : Me.IsloadedForm = False
            Catch ex As Exception
            End Try
            Me.TManager1.GridEX1.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelectionSameTable
            CMain.FormLoading = Main.StatusForm.HasLoaded : CMain.StatProg = Main.StatusProgress.None
            Me.ReadAccess()
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SpesialPrice_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Me.Cursor = Cursors.WaitCursor
        Me.IsLoadingCombo = True
        Me.mcbBrandPack.Text = "" : Me.mcbBrandPack.Value = Nothing
        Me.chkDistributor.UncheckAll() : Me.chkDistributor.Text = ""
        Me.mcbPlantation.Value = Nothing : Me.mcbPlantation.Text = ""
        Me.txtPrice.Value = 0
        Me.btnNewKebun.isEditMode = False
        Me.btnNewKebun.PictureBox1.Image = Me.btnNewKebun.ImageList1.Images(0)
        Me.chkIncludeDPD.Checked = False

        Me.chkDistributor.ReadOnly = False
        Me.mcbBrandPack.ReadOnly = False
        Me.mcbPlantation.ReadOnly = False

        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert

        Me.IsLoadingCombo = False
        Me.btnSave.Text = "&Save"
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Me.Cursor = Cursors.WaitCursor
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
            If Not Me.IsValid Then
                Return
            End If
            With Me.clsPrice
                Dim BrandPackID As String = Me.mcbBrandPack.Value.ToString()
                Dim PlantationID As String = Me.mcbPlantation.Value.ToString()
                .BRANDPACK_ID = Me.mcbBrandPack.Value.ToString()
                .BRANDPACK_NAME = Me.mcbBrandPack.Text.TrimStart().TrimEnd()
                .PRICE = Me.txtPrice.Value
                .PLANTATION_ID = Me.mcbPlantation.Value.ToString()
                .PLANTATION_NAME = Me.mcbPlantation.Text.TrimEnd().TrimStart()
                .MustIncludeDPD = Me.chkIncludeDPD.Checked
                Dim StartDate As String = Me.dtPicStartDate.Value.Day.ToString() + "/" + _
                Me.dtPicStartDate.Value.Month.ToString + "/" + Me.dtPicStartDate.Value.Year.ToString()
                Dim EndDate As String = ""
                If Not IsNothing(Me.dtPicEndDate.Value) Then
                    EndDate = Me.dtPicEndDate.Value.Day.ToString() + "/" + _
                    Me.dtPicEndDate.Value.Month.ToString() & "/" & Me.dtPicEndDate.Value.Year.ToString()
                End If
                ''PRICE TAG DI CLASS DI GENERATNYA
                .START_DATE = StartDate
                .END_DATE = EndDate
                Dim ListDistributor As New List(Of String) : Dim ListOriginalPriceTag1 As New List(Of String)
                If (Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update) Then
                    For i As Integer = 0 To ListOriginalPriceTag.Count - 1
                        Dim BFind As Boolean = False
                        Dim splitValue() As String = ListOriginalPriceTag(i).Split("|".ToCharArray())
                        For i1 As Integer = 0 To splitValue.Length - 1
                            If (splitValue(i1).ToString() = Me.TManager1.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()) Then
                                ListOriginalPriceTag1.Add(ListOriginalPriceTag(i))
                                BFind = True : Exit For
                            End If
                        Next
                        'If Not BFind Then
                        '    If (splitValue(3).ToString() = Me.TManager1.GridEX1.GetValue("DISTRIBUTOR_ID").ToString()) Then
                        '        ListOriginalPriceTag1.Add(ListOriginalPriceTag(i))
                        '    End If
                        'End If
                    Next
                    ListDistributor.Add(Me.TManager1.GridEX1.GetValue("DISTRIBUTOR_ID").ToString())
                    .SaveData(Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()), Me.dtPicEndDate.Value.ToShortDateString(), _
                    NufarmBussinesRules.common.Helper.SaveMode.Update, ListDistributor, ListOriginalPriceTag1)
                Else
                    For i As Integer = 0 To Me.chkDistributor.CheckedValues.Length - 1
                        ListDistributor.Add(Me.chkDistributor.CheckedValues.GetValue(i).ToString())
                    Next
                    .SaveData(Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()), Me.dtPicEndDate.Value.ToShortDateString(), _
                    NufarmBussinesRules.common.Helper.SaveMode.Insert, ListDistributor)
                End If
                'set page index jadi total index
                'If TotalIndex <= 0 Then
                '    Me.PageIndex = 1
                'Else
                '    Me.PageIndex = Me.TotalIndex
                'End If
                Me.PageIndex = 1
                Me.Isloadingrow = True
                Me.GetData() : Me.SetOriginalCriteria() : Me.ListOriginalPriceTag.Clear()
                'FILTER DATA BERDASARKAN DATA YANG DI INPUT
                Dim DV As DataView = CType(Me.TManager1.GridEX1.DataSource, DataView)
                DV.RowFilter = "BRANDPACK_ID = '" & BrandPackID & "' AND PLANTATION_ID = '" & PlantationID & "'"
                Me.TManager1.GridEX1.DataSource = DV : Me.TManager1.GridEX1.RetrieveStructure()
                If Not Me.IsloadedForm Then : Me.FormatDataGrid() : End If
                Dim fc As New Janus.Windows.GridEX.GridEXFilterCondition()
                fc.Value1 = PlantationID
                fc.ConditionOperator = Janus.Windows.GridEX.ConditionOperator.Equal
                fc.Column = Me.TManager1.GridEX1.RootTable.Columns("PLANTATION_ID")
                Me.TManager1.GridEX1.RootTable.ApplyFilter(fc)
                'Me.TManager1.GridEX1.RootTable.Columns("PLANTATION_ID"), Janus.Windows.GridEX.ConditionOperator.Equal, PlantationID)
            End With
            Me.btnAddNew_Click(Me.btnAddNew, New EventArgs())
            If Not IsNothing(Me.TManager1.GridEX1.DataSource) Then
                If Me.TManager1.GridEX1.RecordCount > 0 Then
                    Me.TManager1.GridEX1.MoveToRowIndex(-1)
                End If
            End If
            Me.Isloadingrow = False
            Me.ShowMessageInfo(Me.MessageSavingSucces)
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_ btnSave_Click")
            Me.ShowMessageInfo(ex.Message)
            Me.Isloadingrow = False
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnNewKebun_PicClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewKebun.PicClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim ListDistributors As New List(Of String)
            Dim DtTable As New DataTable("T_Plantation")

            Dim dtDistributor As DataTable = DirectCast(Me.chkDistributor.DropDownDataSource, DataView).Table.Copy()
            Dim dvDistributor As DataView = dtDistributor.DefaultView()
            dvDistributor.Sort = "DISTRIBUTOR_ID ASC"
            'check distributornya
            If Not IsNothing(Me.chkDistributor.CheckedValues) Then
                If Me.chkDistributor.CheckedValues.Length <= 0 Then
                    Me.ShowMessageInfo("Please define distributors to hold plantation") : Return
                End If
            Else
                Me.ShowMessageInfo("Please define distributors to hold plantation") : Return
            End If
            For i As Integer = 0 To dvDistributor.Count - 1
                Dim BFind As Boolean = False
                For i1 As Integer = 0 To Me.chkDistributor.CheckedValues.Length - 1
                    If (Me.chkDistributor.CheckedValues.GetValue(i1).Equals(dvDistributor(i)("DISTRIBUTOR_ID"))) Then
                        BFind = True
                    End If
                Next
                If Not BFind Then
                    dvDistributor(i).Delete()
                    i -= 1
                Else
                    ListDistributors.Add(dvDistributor(i)("DISTRIBUTOR_ID").ToString())
                End If
                If i = dvDistributor.Count - 1 Then
                    Exit For
                End If
            Next

            If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert Then
                Dim Pl As New Plantation()
                With Pl
                    .Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                    .LoadData(dvDistributor)
                    If (.ShowDialog(DtTable) = Windows.Forms.DialogResult.OK) Then
                        'getplantation
                        If ListDistributors.Count > 0 Then
                            'Dim DV As DataView = Me.clsPrice.GetPlantation(ListDistributors)
                            Me.FillComboPlantation(DtTable.DefaultView())
                            Me.mcbPlantation.Value = DtTable.Rows(0)("PLANTATION_ID")
                        End If
                    End If
                End With
            Else
                Dim Pl As Plantation
                If Me.mcbPlantation.Value Is Nothing Then : Return : End If
                If Me.chkDistributor.CheckedValues Is Nothing Then : Return : End If
                If Me.chkDistributor.CheckedValues.Length <= 0 Then : Return : End If
                Pl = New Plantation()
                With Pl
                    .PlantationID = Me.mcbPlantation.Value.ToString()
                    .lblPlantationID.Text = .PlantationID
                    .Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
                    .LoadData(dvDistributor)
                    If (.ShowDialog(DtTable) = Windows.Forms.DialogResult.OK) Then
                        Dim DV As DataView = Me.clsPrice.GetPlantation(ListDistributors)
                        Me.FillComboPlantation(DV)
                        Me.mcbPlantation.Value = .PlantationID
                    End If
                End With
            End If
            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
            Me.btnNewKebun.PictureBox1.Image = Me.btnNewKebun.ImageList1.Images(0)
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_btnNewKebun_PicClick")
            Me.ShowMessageInfo(ex.Message)
            Me.btnNewKebun.PictureBox1.Image = Me.btnNewKebun.ImageList1.Images(0)
            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub chkDistributor_CheckedValuesChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDistributor.CheckedValuesChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If IsLoadingCombo Then : Return : End If
            'If isSearchingDistributor Then : Return : End If
            Dim ListDistributors As New List(Of String)
            If Me.chkDistributor.CheckedValues Is Nothing Then
                Me.FillComboPlantation(Nothing) : Return
            End If
            If Me.chkDistributor.CheckedValues.Length <= 0 Then
                Me.FillComboPlantation(Nothing) : Return
            End If
            For i As Integer = 0 To Me.chkDistributor.CheckedValues.Length - 1
                ListDistributors.Add(Me.chkDistributor.CheckedValues.GetValue(i).ToString())
            Next
            If ListDistributors.Count > 0 Then
                Dim DV As DataView = Me.clsPrice.GetPlantation(ListDistributors)
                Me.FillComboPlantation(DV)
            End If
            If Me.mcbPlantation.DropDownList.RecordCount > 0 Then
                Me.mcbPlantation.DroppedDown = True
            End If
            'Me.isSearchingDistributor = False
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_chkDistributor_CheckedValuesChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSearchDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mcbBrandPack.Value Is Nothing Then
                Me.baseTooltip.Show("Please define BrandPack first", Me.mcbBrandPack, 2500)
                Me.mcbBrandPack.Focus() : Return
            End If
            Me.FillComboDistributor(Me.chkDistributor.Text)
            Dim ItemCount As Integer = Me.chkDistributor.DropDownList.RecordCount()
            Me.ShowMessageInfo(ItemCount.ToString & " item(s) found")
            Me.chkDistributor.Focus()
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_btnSearchDistributor_btnClick")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSeacrhBrandPack_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeacrhBrandPack.btnClick
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

    Private Sub mcbBrandPack_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbBrandPack.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.IsLoadingCombo Then : Return : End If
            If Me.mcbBrandPack.Text = "" Then : Return : End If
            If Me.mcbBrandPack.Value Is Nothing Then : Return : End If
            If Me.mcbBrandPack.SelectedIndex <= -1 Then : Return : End If
            If Not Me.IsSelecttedRowFromGrid Then : Me.IsChangedValueFromBrandPack = True : End If
            Dim EndDate As Object = Nothing

            'get distributor whose PKD has the brandpack choosed
            'Me.clsPrice.getDistributor("", Convert.ToDateTime(Me.dtPicStartDate.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), Me.mcbBrandPack.Value.ToString())
            Me.FillComboDistributor("")
            'Me.chkDistributor_CheckedValuesChanged(Me.chkDistributor, New EventArgs())

        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_mcbBrandPack_ValueChanged")
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.IsChangedValueFromBrandPack = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub mcbPlantation_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mcbPlantation.KeyPress
    '    Me.isSearchingDistributor = True
    'End Sub
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

    Private Sub btnSearchPlantation_btnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchPlantation.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim DV As DataView = Nothing
            Using PL As New NufarmBussinesRules.Plantation.Plantation
                PL.GetPlantation(Me.mcbPlantation.Text, DV)
            End Using
            Me.FillComboPlantation(DV)
            Dim ItemCount As Integer = Me.mcbPlantation.DropDownList.RecordCount()
            Me.ShowMessageInfo(ItemCount.ToString() & " item(s) found")
        Catch ex As Exception

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
            Dim EndDate As Object = Nothing
            If (Not IsNothing(Me.TManager1.GridEX1.GetValue("END_DATE")) And Not IsDBNull(Me.TManager1.GridEX1.GetValue("END_DATE"))) Then
                EndDate = Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("END_DATE"))
            End If
            Dim BSucced As Boolean = Me.clsPrice.DeletePlantationPrice(Me.TManager1.GridEX1.GetValue("PRICE_TAG").ToString(), _
            Me.TManager1.GridEX1.GetValue("DISTRIBUTOR_ID").ToString(), Me.TManager1.GridEX1.GetValue("BRANDPACK_ID").ToString(), Me.TManager1.GridEX1.GetValue("PLANTATION_ID").ToString(), _
            Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("START_DATE")), EndDate)
            If Not BSucced Then
                e.Cancel = True
                Return
            Else
                e.Cancel = False
            End If
            Me.mcbBrandPack.Text = "" : Me.mcbPlantation.Text = "" : Me.txtPrice.Value = 0 : Me.dtPicStartDate.Value = DateTime.Now
            Me.chkIncludeDPD.Checked = False
            Me.chkDistributor.UncheckAll()
        Catch ex As Exception
            e.Cancel = True : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

   
    Private Sub mcbPlantation_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbPlantation.ValueChanged
        Try
            If Me.IsLoadingCombo Then : Return : End If
            If (Me.mcbPlantation.Text = "") Then : Return : End If
            If (Me.mcbPlantation.Value Is Nothing) Then : Return : End If
            If Me.mcbPlantation.SelectedItem Is Nothing Then : Return : End If
            If Me.mcbBrandPack.SelectedIndex <= -1 Then : Return : End If
            'get top 1 price value by distributor and brandpack and plantation ID
            Me.Cursor = Cursors.WaitCursor
            If IsNothing(Me.mcbBrandPack.Value) Then
                Me.mcbPlantation.Text = "" : Me.mcbBrandPack.Value = Nothing : Me.baseTooltip.Show("Please define BrandPack", Me.mcbBrandPack, 2500) : Me.mcbBrandPack.Focus() : Return
            ElseIf Me.mcbBrandPack.Text = "" Then
                Me.mcbPlantation.Text = "" : Me.mcbBrandPack.Value = Nothing : Me.baseTooltip.Show("Please define BrandPack", Me.mcbBrandPack, 2500) : Me.mcbBrandPack.Focus() : Return
            ElseIf IsNothing(Me.chkDistributor.CheckedValues()) Then
                Me.mcbPlantation.Text = "" : Me.mcbBrandPack.Value = Nothing : Me.baseTooltip.Show("Please define Distributor", Me.chkDistributor, 2500) : Me.chkDistributor.Focus() : Return
            ElseIf Me.chkDistributor.CheckedValues.Length <= 0 Then
                Me.mcbPlantation.Text = "" : Me.mcbBrandPack.Value = Nothing : Me.baseTooltip.Show("Please define Distributor", Me.chkDistributor, 2500) : Me.chkDistributor.Focus() : Return
            End If

            Dim ListDistributors As New List(Of String)
            For i As Integer = 0 To Me.chkDistributor.CheckedValues.Length - 1
                ListDistributors.Add(Me.chkDistributor.CheckedValues.GetValue(i).ToString())
            Next
            Dim price As Decimal = Me.clsPrice.getPrice(ListDistributors, Me.mcbBrandPack.Value.ToString(), Me.mcbPlantation.Value.ToString())
            Me.txtPrice.Value = price
            Me.txtPrice.SelectAll()
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
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

   
    Private Sub TManager1_gridKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TManager1.gridKeyDown
        If e.KeyCode = Keys.F2 Then
            Try
                If Me.ShowConfirmedMessage("Are you sure you want to cheklist all ?" & vbCrLf & "Operation can not be undone") = Windows.Forms.DialogResult.No Then
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                Dim listPriceTag As New List(Of String)
                Me.Isloadingrow = True
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.TManager1.GridEX1.GetDataRows()
                    Dim resVal As Object = row.Cells("IncludeDPD").Value
                    Dim PriceTag As String = row.Cells("PRICE_TAG").Value.ToString()
                    If Not IsNothing(resVal) And Not IsDBNull(resVal) Then
                        If CBool(resVal) = False Then
                            If Not listPriceTag.Contains(PriceTag) Then
                                listPriceTag.Add(PriceTag)
                                row.BeginEdit()
                                row.Cells("IncludeDPD").Value = True
                                row.EndEdit()
                            End If
                        End If
                    Else
                        If Not listPriceTag.Contains(PriceTag) Then
                            listPriceTag.Add(PriceTag)
                            row.BeginEdit()
                            row.Cells("IncludeDPD").Value = True
                            row.EndEdit()
                        End If
                    End If
                Next
                Me.clsPrice.UpdateIncludedDPD(listPriceTag, True)
                Me.TManager1.GridEX1.UpdateData()
                Me.Isloadingrow = False
            Catch ex As Exception
                Me.Isloadingrow = False : Me.ShowMessageInfo(ex.Message)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End If
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
End Class
