Imports NuFarm.Domain
Imports NufarmBussinesRules.Brandpack
Public Class DiscountDDOrDR
    Private m_Criteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private OriginalCriteria As NufarmBussinesRules.common.Helper.CriteriaSearch = NufarmBussinesRules.common.Helper.CriteriaSearch.Like
    Private isUndoingCriteria As Boolean = False
    Private m_DataType As NufarmBussinesRules.common.Helper.DataTypes = NufarmBussinesRules.common.Helper.DataTypes.VarChar
    Private Mode As NufarmBussinesRules.common.Helper.SaveMode = NufarmBussinesRules.common.Helper.SaveMode.None
    Private Isloadingrow As Boolean = True
    Private PageIndex As Int32 = 0, PageSize As Integer = 0, TotalIndex As Integer = 0, RowCount As Integer = 0
    Private isLoadingCombo As Boolean = False
    Private OriginalData As New NufarmBussinesRules.common.dataTManager()
    Friend CMain As Main = Nothing
    Private IsSavingData As Boolean = False
    Private pnlentryHeight As Integer = 193
    Private DV As DataView = Nothing
    Private hasLoadForm As Boolean = False
    Private DVBrand As DataView = Nothing, DVBrandPack As DataView = Nothing, DVDistributor As DataView = Nothing, DVGroup As DataView = Nothing, DVDistListGroup As DataView = Nothing, DVProgBrand As DataView = Nothing, DVProgBrandPack As DataView = Nothing
    Private m_clsDiscountPrice As NufarmBussinesRules.Brandpack.DiscountPrice
    Private defSizeChkCerDist As System.Drawing.Size = New Size(232, 20)
    Private listCheckedBrands() As Object
    Private listCheckedBrandPacks() As Object
    Private defSizeChkCerGroupDist = New Size(232, 20)
    Private custSizeChkCerDist As System.Drawing.Size = New Size(181, 20)
    'Private listCheckedGroups As New List(Of String)
    'Private listCheckedBrands As New List(Of String)
    'Private listCheckedDist As New List(Of String)
    'Private custSizeChkCerGroupDist = New Size(156, 20)
    Private dsProgDisc As DataSet = Nothing
    Private domPrice As Nufarm.Domain.DiscPrice = New Nufarm.Domain.DiscPrice()
    Private HasRefData As Boolean = False
    Private curStartAgreement As DateTime = DateTime.Now
    Private curEndAgreement As DateTime = DateTime.Now
    Private isAddingRow As Boolean = False
    Private Enum checkedValusFrom
        None = 0
        DropDown = 1
        Other
    End Enum
    Private checkedCHK As checkedValusFrom

    Private ReadOnly Property clsDiscountPrice() As NufarmBussinesRules.Brandpack.DiscountPrice
        Get
            If m_clsDiscountPrice Is Nothing Then
                m_clsDiscountPrice = New NufarmBussinesRules.Brandpack.DiscountPrice()
            End If
            Return m_clsDiscountPrice
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
                    Me.pnlEntry.Enabled = False
                    'If Me.pnlEntryAndFilter.Height <= 0 Then
                    'Me.pnlEntry.Dock = DockStyle.None
                    'Me.pnlEntry.Height = 0
                    'Me.pnlEntryAndFilter.Height = Me.FilterEditor1.Height + 1
                    'End If

                    Me.TManager1.BringToFront()
                Case "btnFilterEqual"
                    Me.FilterEditor1.Visible = False
                    Me.FilterEditor1.SourceControl = Nothing
                    Me.TManager1.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    Me.TManager1.GridEX1.RemoveFilters()
                    Me.pnlEntry.Enabled = False
                    'If Me.pnlEntryAndFilter.Height <= 0 Then
                    '    Me.pnlEntry.Dock = DockStyle.None
                    '    Me.pnlEntry.Height = 0
                    '    Me.pnlEntryAndFilter.Height = Me.FilterEditor1.Height + 1
                    'End If
                Case "btnExport"
                    ExportData(Me.TManager1.GridEX1)
                Case "btnRefresh"
                    Me.GetData() : Me.SetOriginalCriteria()
                Case "btnAddNew"
                    Me.Isloadingrow = True
                    Me.isLoadingCombo = True
                    Me.pnlEntry.Enabled = True
                    If Me.btnAddNew.Checked Then
                        Me.ClearEntry()
                        Me.pnlEntry.Height = 0
                        'Me.pnlEntryAndFilter.Height = 0
                        'Me.pnlEntry.Dock = DockStyle.None
                        Me.btnAddNew.Checked = False
                        Me.btnSave.Enabled = False
                        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None
                        Me.listCheckedBrandPacks = Nothing
                        Me.listCheckedBrands = Nothing
                    Else
                        Me.ClearEntry()
                        DVBrand = Me.clsDiscountPrice.getBrand(0, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                        Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), New List(Of String), False)
                        'set default datetime to current date
                        Me.chkBrand.SetDataBinding(DVBrand, "")
                        Me.chkBrandPacks.SetDataBinding(Nothing, "")
                        Me.pnlEntry.Height = Me.pnlentryHeight
                        Me.dsProgDisc = Me.clsDiscountPrice.getDS(0, True)
                        Me.grdProgDisc.SetDataBinding(dsProgDisc.Tables(0).DefaultView(), "")
                        Me.FillValueList()
                        Me.grdProgDisc.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                        Me.grdProgDisc.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                        If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DiscDDorDR = True Then
                            Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Else
                            Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                        End If
                        Me.chkTargetPOPerPackSize.Checked = False
                        Me.chkTargetPOPerPackSize.Enabled = True
                        Me.chkTargetPOPerBrand.Checked = False
                        Me.chkTargetPOPerBrand.Enabled = True
                        Me.txtProgramID.Enabled = True
                        Me.btnAddNew.Checked = True
                        Me.dtPicFrom.ReadOnly = False
                        Me.dtPicEndDate.ReadOnly = False
                        Me.btnEditRow.Checked = False
                        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                        Me.btnSave.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscPrice
                        Me.btnEditRow.Enabled = False
                        Me.grpProgDisc.Enabled = True
                        Me.dtPicFrom.Value = Me.curStartAgreement
                        Me.dtPicEndDate.Value = Me.curEndAgreement
                        Me.rdbAllDist.Enabled = True
                        Me.rdbCertainDisc.Enabled = True
                        Me.rdbGroupDistributor.Enabled = True
                        Me.chkBrand.ReadOnly = False
                        Me.chkBrandPacks.ReadOnly = False
                        Me.dtPicFrom.ReadOnly = False
                        Me.chlCertainDisc.ReadOnly = False
                        Me.chkGroupDist.ReadOnly = False
                        Me.dtPicFrom.Focus()
                        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert
                    End If
                Case "btnSave"
                    If Not Me.validateData() Then
                        Me.Cursor = Cursors.Default : Return
                    End If
                    If Me.SaveData() Then
                        'Me.PageIndex = Me.TotalIndex
                        Me.Isloadingrow = True : isLoadingCombo = True
                        Me.GetData() : Me.ClearEntry()
                        Me.btnEditRow.Checked = False
                        If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
                            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None
                        End If
                    End If
                Case "btnEditRow"
                    Me.Isloadingrow = True
                    Me.isLoadingCombo = True
                    Me.pnlEntry.Enabled = True
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
                            Me.ClearEntry() : Me.btnEditRow.Checked = False
                            Me.Isloadingrow = False : Me.isLoadingCombo = False : Me.Cursor = Cursors.Default : Return
                        End If
                        If Me.TManager1.GridEX1.RecordCount <= 0 Then
                            Me.ClearEntry() : Me.btnEditRow.Checked = False
                            Me.Isloadingrow = False : Me.isLoadingCombo = False : Me.Cursor = Cursors.Default : Return
                        End If
                        If Me.TManager1.GridEX1.RecordCount > 0 Then
                            If Me.TManager1.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                                Me.pnlEntry.Height = Me.pnlentryHeight
                                Me.InflateData()
                                Me.btnSave.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DiscDDorDR
                                Me.pnlEntry.Height = Me.pnlentryHeight
                                Me.btnAddNew.Checked = False
                                Me.btnEditRow.Checked = True
                                Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
                            Else
                                Me.ClearEntry() : Me.btnEditRow.Checked = False
                                Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None
                                Me.Isloadingrow = False : Me.isLoadingCombo = False
                                Me.btnSave.Enabled = False
                                Me.Cursor = Cursors.Default
                                Return
                            End If
                        End If
                        Me.btnAddNew.Checked = False
                    End If
            End Select
            Me.Isloadingrow = False : isLoadingCombo = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            If Me.isLoadingCombo Then : Me.isLoadingCombo = False : End If
            Me.Cursor = Cursors.Default
            Me.Isloadingrow = False
        End Try
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
                Case "APPLY_DATE"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.DateTime
                Case "END_DATE"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.DateTime
                Case "APPLY_TO"
                    Me.m_DataType = NufarmBussinesRules.common.Helper.DataTypes.VarChar
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
        If Me.Isloadingrow Then : Return : End If
        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
            e.Cancel = True : Return
        End If
        Try
            Dim IDApp As Integer = CInt(Me.TManager1.GridEX1.GetValue("IDApp"))
            Cursor = Cursors.WaitCursor
            Dim listIDApp As New List(Of Integer)
            listIDApp.Add(IDApp)
            If Me.clsDiscountPrice.HasReference(listIDApp, True) Then
                Me.ShowMessageError(Me.MessageCantDeleteData) : e.Cancel = True
            End If
            e.Cancel = clsDiscountPrice.Delete(IDApp, True)
        Catch ex As Exception
            e.Cancel = True
            Me.ShowMessageError(ex.Message)
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub ClearEntry()
        Me.txtProgramID.Text = ""
        Me.chkTargetPOPerBrand.Checked = False
        Me.chkBrand.UncheckAll()
        Me.chkBrand.Text = ""
        Me.chkBrandPacks.UncheckAll()
        Me.chkBrandPacks.Text = ""
        Me.txtDescriptions.Text = ""
        Me.chlCertainDisc.UncheckAll()
        Me.chlCertainDisc.Text = ""
        Me.chkGroupDist.UncheckAll()
        Me.chkGroupDist.Text = ""
        Me.grdProgDisc.SetDataBinding(Nothing, "")
        Me.gridGroupDist.SetDataBinding(Nothing, "")
        Me.rdbCertainDisc.Checked = False
        Me.rdbGroupDistributor.Checked = False
        Me.rdbAllDist.Checked = True
        Me.chkTargetPOPerPackSize.Enabled = False
    End Sub
    Private Function validateData() As Boolean
        'cheked Brands
        If Me.txtProgramID.Text = String.Empty Then
            Me.baseTooltip.Show("Please enter program ID", txtProgramID, 2500) : Me.txtProgramID.Focus()
            Return False
        End If
        If IsNothing(Me.chkBrand.CheckedValues()) Then
            Me.baseTooltip.Show("Please mark brands", chkBrand, 2500) : Me.chkBrand.Focus()
            Return False
        ElseIf Me.chkBrand.CheckedValues().Length <= 0 Then
            Me.baseTooltip.Show("Please mark brands", chkBrand, 2500) : Me.chkBrand.Focus()
            Return False
        End If
        'descriptions
        If Me.txtDescriptions.Text.Length <= 3 Then
            Me.baseTooltip.Show("Please enter name/descriptions", Me.txtDescriptions, 2500) : Me.txtDescriptions.SelectAll() : Me.txtDescriptions.Focus()
            Return False
        End If
        ''check grdDiscount
        If Not IsNothing(Me.grdProgDisc.DataSource) Then
            If Me.grdProgDisc.RecordCount <= 0 Then
                Me.baseTooltip.Show("Please enter shema discount", Me.grdProgDisc, 2500)
                Me.grdProgDisc.Focus()
                Return False
            End If
        Else
            Me.baseTooltip.Show("Please enter shema discount", Me.grdProgDisc, 2500)
            Me.grdProgDisc.Focus()
            Return False
        End If
        If Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible Then
            ''check chkBrandpack
            If IsNothing(Me.chkBrandPacks.CheckedValues()) Then
                Me.baseTooltip.Show("Please mark brandpacks", Me.chkBrandPacks, 200)
                Me.chkBrandPacks.Focus() : Return False
            ElseIf Me.chkBrandPacks.CheckedValues.Length <= 0 Then
                Me.baseTooltip.Show("Please mark brandpacks", Me.chkBrandPacks, 200)
                Me.chkBrandPacks.Focus() : Return False
            End If
        End If
        If Me.chkTargetPOPerPackSize.Checked Then
            Me.chkBrand.Focus()
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.chkBrandPacks.DropDownList().GetCheckedRows()
                Dim BrandPackID As String = row.Cells("BRANDPACK_ID").Value.ToString()
                Dim DV As DataView = CType(Me.grdProgDisc.DataSource, DataView).ToTable().Copy().DefaultView
                DV.Sort = "BRANDPACK_ID"
                Dim f As Integer = DV.Find(BrandPackID)
                If f <= -1 Then
                    Me.baseTooltip.Show("BRANDPACK " & row.Cells("BRANDPACK_NAME").Value & " Doesn't exists in discount progressive table", Me.chkBrandPacks, 2500) : Me.chkBrandPacks.Focus() : Return False
                End If
            Next
            ''cari data dimana brand_id tidak di isi
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                If IsDBNull(row.Cells("BRANDPACK_ID").Value) Then
                    Me.baseTooltip.Show("BRANDPACK NAME can not be null in discount progressive table", Me.grdProgDisc, 2500)
                    grdProgDisc.MoveTo(row)
                    Return False
                End If
            Next
        End If
        If Me.grdProgDisc.RootTable.Columns("BRAND_ID").Visible Then
            ''check chkBrand
            If IsNothing(Me.chkBrand.CheckedValues()) Then
                Me.baseTooltip.Show("Please mark brand", Me.chkBrand, 200)
                Me.chkBrand.Focus() : Return False
            ElseIf Me.chkBrand.CheckedValues.Length <= 0 Then
                Me.baseTooltip.Show("Please mark brand", Me.chkBrand, 200)
                Me.chkBrand.Focus() : Return False
            End If
        End If
        If Me.chkTargetPOPerBrand.Checked Then
            Me.chkBrand.Focus()
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.chkBrand.DropDownList().GetCheckedRows()
                Dim BrandID As String = row.Cells("BRAND_ID").Value.ToString()
                Dim DV As DataView = CType(Me.grdProgDisc.DataSource, DataView).ToTable().Copy().DefaultView
                DV.Sort = "BRAND_ID"
                Dim f As Integer = DV.Find(BrandID)
                If f <= -1 Then
                    Me.baseTooltip.Show("BRAND " & row.Cells("BRAND_NAME").Value & " Doesn't exists in discount progressive table", Me.chkBrand, 2500) : Me.chkBrand.Focus() : Return False
                End If
            Next
            ''cari data dimana brand_id tidak di isi
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                If IsDBNull(row.Cells("BRAND_ID").Value) Then
                    Me.baseTooltip.Show("BRAND NAME can not be null in discount progressive table", Me.grdProgDisc, 2500)
                    grdProgDisc.MoveTo(row)
                    Return False
                End If
            Next
        End If
        'check double input data di grid progdisc
        Dim listID As New List(Of String)
        Dim dtDummy As DataTable = CType(Me.grdProgDisc.DataSource, DataView).ToTable().Copy()
        If Me.chkTargetPOPerPackSize.Checked Then
            'berarti ID sama BrandpackID dan flag
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                Dim Flag As String = row.Cells("TypeApp").Value.ToString()
                If Not IsNothing(row.Cells("BRANDPACK_ID").Value) And Not IsDBNull(row.Cells("BRANDPACK_ID").Value) Then
                    Dim BrandPackID As String = row.Cells("BRANDPACK_ID").Value.ToString()
                    Dim MoreThanQty As Object = row.Cells("MoreThanQty").Value
                    ''check di dataview
                    Dim ID As String = BrandPackID & Flag & MoreThanQty
                    If listID.Contains(ID) Then
                    Else
                        listID.Add(ID)
                        Dim checkedRows() As DataRow = dtDummy.Select("BRANDPACK_ID = '" & BrandPackID & "' AND TypeApp = '" & Flag & "'  AND MoreThanQty = " & MoreThanQty)
                        If checkedRows.Length > 1 Then
                            Me.ShowMessageInfo("double input data on progressive discount")
                            Cursor = Cursors.Default : Return False
                        End If
                    End If
                End If
            Next
        ElseIf Me.chkTargetPOPerBrand.Checked Then
            'berarti ID sama BrandID dan flag
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                If Not IsNothing(row.Cells("BRAND_ID").Value) And Not IsDBNull(row.Cells("BRAND_ID").Value) Then
                    Dim BrandID As String = row.Cells("BRAND_ID").Value.ToString()
                    Dim Flag As String = row.Cells("TypeApp").Value.ToString()
                    Dim MoreThanQty As Object = row.Cells("MoreThanQty").Value
                    Dim ID As String = BrandID & Flag & MoreThanQty
                    If listID.Contains(ID) Then
                    Else
                        listID.Add(ID)
                        Dim checkedRows() As DataRow = dtDummy.Select("BRAND_ID = '" & BrandID & "' AND TypeApp = '" & Flag & "'  AND MoreThanQty = " & MoreThanQty)
                        If checkedRows.Length > 1 Then
                            Me.ShowMessageInfo("double input data on progressive discount")
                            Cursor = Cursors.Default : Return False
                        End If
                    End If
                End If
            Next
        Else
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                Dim Flag As String = row.Cells("TypeApp").Value.ToString()
                ''check di dataview
                Dim MoreThanQty As Object = row.Cells("MoreThanQty").Value
                Dim ID As String = Flag & MoreThanQty
                If listID.Contains(ID) Then
                Else
                    listID.Add(ID)
                    Dim checkedRows() As DataRow = dtDummy.Select("TypeApp = '" & Flag & "'  AND MoreThanQty = " & MoreThanQty)
                    If checkedRows.Length > 1 Then
                        Me.ShowMessageInfo("double input data on progressive discount")
                        Cursor = Cursors.Default : Return False
                    End If
                End If
            Next
        End If
        'check Apply To
        If Me.rdbCertainDisc.Checked Then
            If Not IsNothing(Me.chlCertainDisc.CheckedValues()) Then
                If Me.chlCertainDisc.CheckedValues().Length <= 0 Then
                    Me.baseTooltip.Show("Please mark distributors", Me.chlCertainDisc, 2500) : Me.chlCertainDisc.Focus() : Return False
                End If
            Else
                Me.baseTooltip.Show("Please mark distributors", Me.chlCertainDisc, 2500) : Me.chlCertainDisc.Focus() : Return False
            End If
        End If
        If Me.rdbGroupDistributor.Checked Then
            If Not IsNothing(Me.chkGroupDist.CheckedValues()) Then
                If Me.chkGroupDist.CheckedValues().Length <= 0 Then
                    Me.baseTooltip.Show("Pleas mark distributor groups", Me.chkGroupDist, 2500) : Me.chkGroupDist.Focus() : Return False
                End If
            Else
                Me.baseTooltip.Show("Pleas mark distributor groups", Me.chkGroupDist, 2500) : Me.chkGroupDist.Focus() : Return False
            End If
        End If
        Return True
    End Function
    Private Function hasChanged() As Boolean  'mode harus update
        Dim B As Boolean = False
        If Not HasRefData Then
            ''check dataset
            If Not IsNothing(Me.domPrice.DsProgDesc) Then
                If Me.domPrice.DsProgDesc.HasChanges() Then
                    B = True
                End If
            End If
            'periksa listBrand
            If Not IsNothing(Me.chkBrand.CheckedValues()) Then
                If Me.chkBrand.CheckedValues().Length > 0 Then
                    ''checked brand
                    If Me.chkBrand.CheckedValues().Length <> Me.domPrice.ListBrands.Count Then
                        domPrice.HasChangedBrands = True
                        B = True
                    End If
                    For i As Integer = 0 To chkBrand.CheckedValues().Length - 1
                        If Not Me.domPrice.ListBrands.Contains(chkBrand.CheckedValues(i).ToString()) Then
                            B = True
                            domPrice.HasChangedBrands = True
                            Exit For
                        End If
                    Next
                End If
            End If
            If Me.domPrice.ListBrandPacks.Count > 0 Then
                If Not IsNothing(Me.chkBrandPacks.CheckedValues()) Then
                    If Me.chkBrandPacks.CheckedValues.Length <> Me.domPrice.ListBrandPacks.Count Then
                        B = True
                        domPrice.HasChangedBrandPacks = True
                    End If
                    For i As Integer = 0 To chkBrandPacks.CheckedValues().Length - 1
                        If Not Me.domPrice.ListBrandPacks.Contains(chkBrandPacks.CheckedValues(i).ToString()) Then
                            B = True
                            domPrice.HasChangedBrandPacks = True
                            Exit For
                        End If
                    Next
                Else
                    B = True
                    domPrice.HasChangedBrandPacks = True
                End If
            ElseIf Not IsNothing(Me.chkBrandPacks.CheckedValues()) Then
                If Me.chkBrandPacks.CheckedValues.Length > 0 Then
                    B = True
                    domPrice.HasChangedBrandPacks = True
                End If
            End If
            If Me.rdbCertainDisc.Checked Then
                If Not IsNothing(Me.chlCertainDisc.CheckedValues()) Then
                    If Me.chlCertainDisc.CheckedValues().Length <> Me.domPrice.ListDistributors.Count Then
                        B = True
                        domPrice.HasChangedDistr = True
                    End If
                    For i As Integer = 0 To Me.chlCertainDisc.CheckedValues().Length - 1
                        If Not Me.domPrice.ListDistributors.Contains(Me.chlCertainDisc.CheckedValues(i).ToString()) Then
                            B = True
                            domPrice.HasChangedDistr = True
                            Exit For
                        End If
                    Next
                ElseIf Me.domPrice.ListDistributors.Count > 0 Then
                    B = True
                    domPrice.HasChangedDistr = True
                End If
            End If
            If Me.rdbGroupDistributor.Checked Then
                If Me.chkGroupDist.CheckedValues().Length <> Me.domPrice.ListGroupDist.Count Then
                    B = True
                    domPrice.HasChangedGroups = True
                End If
                For i As Integer = 0 To Me.chkGroupDist.CheckedValues().Length - 1
                    If Not Me.domPrice.ListGroupDist.Contains(Me.chkGroupDist.CheckedValues(i).ToString()) Then
                        B = True
                        domPrice.HasChangedGroups = True
                        Exit For
                    End If
                Next
            End If
        End If
        Select Case Me.domPrice.ApplyTo
            Case "AL"
                If Not Me.rdbAllDist.Checked Then
                    If Me.rdbCertainDisc.Checked Or Me.rdbGroupDistributor.Checked Then
                        domPrice.HasChangedDistr = True
                    End If
                    Return True
                End If
            Case "CD"
                If Not Me.rdbCertainDisc.Checked Then
                    If Me.rdbAllDist.Checked Or Me.rdbGroupDistributor.Checked Then
                        domPrice.HasChangedDistr = True
                    End If
                    Return True
                End If
            Case "GD"
                If Not Me.rdbGroupDistributor.Checked Then
                    If Me.rdbCertainDisc.Checked Or Me.rdbAllDist.Checked Then
                        domPrice.HasChangedGroups = True
                    End If
                    Return True
                End If
        End Select
        If Me.domPrice.DescriptionApp <> Me.txtDescriptions.Text() Then
            Return True
        End If
        If Me.domPrice.ApplyDate.ToShortDateString() <> Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()) Then
            Return True
        End If
        If Me.domPrice.EndDate.ToShortDateString() <> Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()) Then
            Return True
        End If
        If Not IsNothing(Me.domPrice.ProgramID) And Not IsDBNull(Me.domPrice.ProgramID) Then
            If Me.domPrice.ProgramID <> Me.txtProgramID.Text.Trim() Then
                Return True
            End If
        End If
        Return B
    End Function
    Friend Sub LoadData()
        isLoadingCombo = True : Isloadingrow = True
        With Me.TManager1.cbCategory
            .Items.Add("PROGRAM_DESC") : .Items.Add("APPLY_TO") : .Items.Add("CREATED_BY") : .Items.Add("APPLY_DATE") : .Items.Add("END_DATE")
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
            Me.RunQuery(.SearchValue, .SearchBy)
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
        With Me.OriginalData
            .CriteriaSearch = strCriteriaSearch
            .MaxRecord = MaxRecord
            .SearchBy = SearchBy
            .SearchValue = RTrim(Me.TManager1.txtSearch.Text)
            Me.RunQuery(.SearchValue, .SearchBy)
        End With
        If TManager1.txtMaxRecord.Text <> "" Then
            If RowCount > CInt(Me.TManager1.txtMaxRecord.Text) Then
                RowCount = CInt(Me.TManager1.txtMaxRecord.Text)
            End If
        End If
        Me.SetStatusRecord() : Me.SetButtonStatus()
    End Sub

    Private Sub FillValueList()
        'AMBIL DATA DI ACCPACK 
        Dim VList() As String = {"DD", "DR", "CBD", "DK", "DP"}
        Dim ColDiscType As Janus.Windows.GridEX.GridEXColumn = Me.grdProgDisc.RootTable.Columns("TypeApp")
        ColDiscType.EditType = Janus.Windows.GridEX.EditType.DropDownList
        ColDiscType.AutoComplete = True : ColDiscType.HasValueList = True
        Dim ValueListUnit As Janus.Windows.GridEX.GridEXValueListItemCollection = ColDiscType.ValueList
        ValueListUnit.PopulateValueList(VList, "TypeApp")
        ColDiscType.EditTarget = Janus.Windows.GridEX.EditTarget.Value
        ColDiscType.DefaultGroupInterval = Janus.Windows.GridEX.GroupInterval.Text
    End Sub
    Private Sub FormatDataGrid()
        For Each col As Janus.Windows.GridEX.GridEXColumn In TManager1.GridEX1.RootTable.Columns
            col.FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
            If col.Type Is Type.GetType("System.Int32") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            ElseIf col.Type Is Type.GetType("System.Decimal") Then
                col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.FormatString = "#,##0.00"
            ElseIf col.Type Is Type.GetType("System.DateTime") Then
                col.FormatString = "dd MMMM yyyy"
                col.FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
            End If
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
        TManager1.GridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
        Me.TManager1.GridEX1.RootTable.Columns("CreatedDate").Caption = "CREATED_DATE"
        Me.TManager1.GridEX1.RootTable.Columns("CreatedBy").Caption = "CREATED_BY"
        Me.TManager1.GridEX1.AutoSizeColumns(Me.TManager1.GridEX1.RootTable)

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
    Private Sub InflateData()
        Dim IDApp As Integer = Me.TManager1.GridEX1.GetValue("IDApp")
        If Not Me.isLoadingCombo Then : Me.isLoadingCombo = True : End If
        Me.domPrice = New Nufarm.Domain.DiscPrice()
        With Me.domPrice
            .IDApp = IDApp
            .ApplyDate = Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("APPLY_DATE"))
            .EndDate = IIf(Me.TManager1.GridEX1.GetValue("END_DATE") Is DBNull.Value, DateTime.Now, Me.TManager1.GridEX1.GetValue("END_DATE"))
            Dim strProgID As String = ""
            If Me.TManager1.GridEX1.GetValue("PROGRAM_ID") Is DBNull.Value Or IsNothing(Me.TManager1.GridEX1.GetValue("PROGRAM_ID")) Then
                .ProgramID = DBNull.Value
            Else
                .ProgramID = Me.TManager1.GridEX1.GetValue("PROGRAM_ID").ToString()
                strProgID = .ProgramID
            End If
            'get listBrands
            Me.txtProgramID.Text = strProgID
            'reset dulu

            Me.chkTargetPOPerBrand.Checked = False
            Me.chkTargetPOPerPackSize.Checked = False

            Me.DVBrand = Me.clsDiscountPrice.getBrand(IDApp, .ApplyDate, .EndDate, .ListBrands, False)
            Me.DVBrandPack = Me.clsDiscountPrice.getBrandBrandPack(IDApp, .ApplyDate, .EndDate, .ListBrands, .ListBrandPacks, False)
            Dim checkedBrands(.ListBrands.Count - 1) As String
            .ListBrands.CopyTo(checkedBrands)
            Me.listCheckedBrands = checkedBrands
            Me.chkBrand.ReadOnly = False
            Me.chkBrand.SetDataBinding(Me.DVBrand, "")
            Me.chkBrand.CheckedValues = checkedBrands
            Dim CheckedBrandPacks(.ListBrandPacks.Count - 1) As String
            .ListBrandPacks.CopyTo(CheckedBrandPacks)
            Me.listCheckedBrandPacks = CheckedBrandPacks
            Me.chkBrandPacks.ReadOnly = False
            Me.chkBrandPacks.SetDataBinding(Me.DVBrandPack, "")
            Me.chkBrandPacks.CheckedValues = CheckedBrandPacks
            Me.dtPicFrom.Value = Convert.ToDateTime(Me.TManager1.GridEX1.GetValue("APPLY_DATE"))
            Me.dtPicEndDate.Value = IIf(Me.TManager1.GridEX1.GetValue("END_DATE") Is DBNull.Value, DateTime.Now, Me.TManager1.GridEX1.GetValue("END_DATE"))
            Select Case Me.TManager1.GridEX1.GetValue("APPLY_TO")
                Case "All Distributors"
                    Me.rdbAllDist.Checked = True
                    Me.gridGroupDist.Visible = False
                    Me.chlCertainDisc.UncheckAll()
                    Me.chlCertainDisc.Text = ""
                    Me.chlCertainDisc.ReadOnly = True
                    Me.chlCertainDisc.Size = Me.defSizeChkCerDist
                    .ApplyTo = "AL"
                Case "Certain Distributors"
                    Me.rdbCertainDisc.Checked = True
                    ''get listDistributors
                    Me.DVDistributor = Me.clsDiscountPrice.getDistributor(IDApp, .ApplyDate, .EndDate, .ListBrands, .ListBrandPacks, .ListDistributors, False)
                    Dim CheckedDists(.ListDistributors.Count - 1) As String
                    .ListDistributors.CopyTo(CheckedDists)
                    Me.chlCertainDisc.SetDataBinding(DVDistributor, "")
                    Me.chlCertainDisc.CheckedValues = CheckedDists
                    Me.gridGroupDist.Visible = False
                    Me.chlCertainDisc.Size = Me.defSizeChkCerDist
                    .ApplyTo = "CD"
                Case "Group Distributors"
                    Me.rdbGroupDistributor.Checked = True
                    Me.DVGroup = Me.clsDiscountPrice.getDistGroup(IDApp, .ListGroupDist, False)
                    Dim checkedGroups(.ListGroupDist.Count - 1) As String
                    .ListGroupDist.CopyTo(checkedGroups)
                    Me.chkGroupDist.SetDataBinding(Me.DVGroup, "")
                    Me.chkGroupDist.CheckedValues = checkedGroups
                    Me.DVDistListGroup = Me.clsDiscountPrice.getDistGroupList(False)
                    Me.gridGroupDist.SetDataBinding(Me.DVDistListGroup, "")
                    Me.gridGroupDist.Visible = True
                    Me.chlCertainDisc.Size = Me.custSizeChkCerDist
                    .ApplyTo = "GD"
                Case Else
                    Throw New Exception("Unknown type of apply to")
            End Select
            .ModifiedBy = NufarmBussinesRules.User.UserLogin.UserName
            .ModifiedDate = NufarmBussinesRules.SharedClass.ServerDate()
            .DescriptionApp = Me.TManager1.GridEX1.GetValue("PROGRAM_DESC")
            ''get ds
            .DsProgDesc = Me.clsDiscountPrice.getDS(IDApp, False)
            Me.grdProgDisc.SetDataBinding(.DsProgDesc.Tables(0).DefaultView(), "")
            Me.FillValueList()

            If IsNothing(Me.grdProgDisc.GetValue("BRAND_ID")) Then
                'Me.chkTargetPOByProduct.Checked = False
                Me.grdProgDisc.RootTable.Columns("BRAND_ID").Visible = False
                Me.chkTargetPOPerBrand.Checked = False
            ElseIf IsDBNull(Me.grdProgDisc.GetValue("BRAND_ID")) Then
                'Me.chkTargetPOByProduct.Checked = False
                Me.grdProgDisc.RootTable.Columns("BRAND_ID").Visible = False
                Me.chkTargetPOPerBrand.Checked = False
            Else
                Dim tblProgBrand As New DataTable("DISC_PROGRESSIVE_BRAND")
                If IsNothing(Me.DVProgBrand) Then
                    With tblProgBrand
                        .Columns.Add("BRAND_ID", Type.GetType("System.String"))
                        .Columns.Add("BRAND_NAME", Type.GetType("System.String"))
                    End With
                ElseIf Me.DVProgBrand.Table.Columns.Count <= 0 Then
                    With tblProgBrand
                        .Columns.Add("BRAND_ID", Type.GetType("System.String"))
                        .Columns.Add("BRANDP_NAME", Type.GetType("System.String"))
                    End With
                Else
                    tblProgBrand = Me.DVProgBrand.Table
                End If
                tblProgBrand.Rows.Clear()
                tblProgBrand.AcceptChanges()
                Dim isReadOnly = Me.chkBrand.ReadOnly
                Me.chkBrand.Focus()
                Me.chkBrand.DroppedDown = True
                For Each Jrow As Janus.Windows.GridEX.GridEXRow In chkBrand.DropDownList().GetCheckedRows()
                    Dim row As DataRow = tblProgBrand.NewRow()
                    row.BeginEdit()
                    row("BRAND_ID") = Jrow.Cells("BRAND_ID").Value
                    row("BRAND_NAME") = Jrow.Cells("BRAND_NAME").Value
                    row.EndEdit()
                    tblProgBrand.Rows.Add(row)
                Next
                Me.chkBrand.DroppedDown = False
                Me.grdProgDisc.Focus()
                Me.chkBrand.ReadOnly = isReadOnly
                Me.DVProgBrand = tblProgBrand.DefaultView()
                Me.grdProgDisc.DropDowns(1).SetDataBinding(Me.DVProgBrand, "")
                Me.grdProgDisc.RootTable.Columns("BRAND_ID").Visible = True
                Me.chkTargetPOPerBrand.Checked = True
            End If
            If .ListBrandPacks.Count > 0 Then
                If IsNothing(Me.grdProgDisc.GetValue("BRANDPACK_ID")) Then
                    'Me.chkTargetPOByProduct.Checked = False
                    Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible = False
                    Me.chkTargetPOPerPackSize.Checked = False
                ElseIf IsDBNull(Me.grdProgDisc.GetValue("BRANDPACK_ID")) Then
                    'Me.chkTargetPOByProduct.Checked = False
                    Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible = False
                    Me.chkTargetPOPerPackSize.Checked = False
                Else
                    'Me.chkTargetPOByProduct.Checked = True
                    'Me.grdProgDisc.DropDowns(0).SetDataBinding(Me.DVBrandPack, "")
                    Dim tblProgBrandPack As New DataTable("DISC_PROGRESSIVE_BRANDPACK")
                    If IsNothing(Me.DVProgBrandPack) Then
                        With tblProgBrandPack
                            .Columns.Add("BRANDPACK_ID", Type.GetType("System.String"))
                            .Columns.Add("BRANDPACK_NAME", Type.GetType("System.String"))
                        End With
                    ElseIf Me.DVProgBrandPack.Table.Columns.Count <= 0 Then
                        With tblProgBrandPack
                            .Columns.Add("BRANDPACK_ID", Type.GetType("System.String"))
                            .Columns.Add("BRANDPACK_NAME", Type.GetType("System.String"))
                        End With
                    Else
                        tblProgBrandPack = Me.DVProgBrandPack.Table
                    End If
                    tblProgBrandPack.Rows.Clear()
                    tblProgBrandPack.AcceptChanges()
                    Dim isReadOnly = Me.chkBrandPacks.ReadOnly
                    Me.chkBrandPacks.Focus()
                    Me.chkBrandPacks.DroppedDown = True
                    For Each Jrow As Janus.Windows.GridEX.GridEXRow In chkBrandPacks.DropDownList().GetCheckedRows()
                        Dim row As DataRow = tblProgBrandPack.NewRow()
                        row.BeginEdit()
                        row("BRANDPACK_ID") = Jrow.Cells("BRANDPACK_ID").Value
                        row("BRANDPACK_NAME") = Jrow.Cells("BRANDPACK_NAME").Value
                        row.EndEdit()
                        tblProgBrandPack.Rows.Add(row)
                    Next
                    Me.chkBrandPacks.DroppedDown = False
                    Me.grdProgDisc.Focus()
                    Me.chkBrandPacks.ReadOnly = isReadOnly
                    Me.DVProgBrandPack = tblProgBrandPack.DefaultView()
                    Me.grdProgDisc.DropDowns(0).SetDataBinding(Me.DVProgBrandPack, "")
                    Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible = True
                    Me.chkTargetPOPerPackSize.Checked = True
                End If
            Else
                'Me.chkTargetPOByProduct.Checked = False
                Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible = False
            End If
            Me.txtDescriptions.Text = Me.TManager1.GridEX1.GetValue("PROGRAM_DESC")
        End With
        'Mode Edit Periode harus disabled, karena terlalu kompleks logic nya
        'get listIDApp
        Dim listIDApp As New List(Of Integer)
        For i As Integer = 0 To domPrice.DsProgDesc.Tables(0).Rows.Count - 1
            Dim IDAppProgB As Object = domPrice.DsProgDesc.Tables(0).Rows(i)("IDApp")
            If Not IsNothing(IDAppProgB) And Not IsDBNull(IDAppProgB) Then
                If CInt(IDAppProgB) > 0 Then
                    listIDApp.Add(IDAppProgB)
                End If
            End If
        Next

        Dim HasRef As Boolean = Me.clsDiscountPrice.HasReference(listIDApp, True)
        Me.dtPicFrom.ReadOnly = HasRef
        Me.dtPicEndDate.ReadOnly = HasRef
        If HasRef Then
            Me.txtProgramID.Enabled = False
            Me.rdbAllDist.Enabled = False
            Me.rdbCertainDisc.Enabled = False
            Me.rdbGroupDistributor.Enabled = False
            'Me.chkBrand.ReadOnly = True
            'Me.chkBrandPacks.ReadOnly = True
            'Me.chkTargetPOByProduct.Enabled = False
            ' Me.grdProgDisc.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
            'Me.grdProgDisc.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            'Me.grdProgDisc.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            Me.chlCertainDisc.ReadOnly = True
            Me.chkGroupDist.ReadOnly = True
            'Me.btnSave.Enabled = False
            Me.chkTargetPOPerPackSize.Enabled = False
            Me.chkTargetPOPerBrand.Enabled = False
        Else
            Me.txtProgramID.Enabled = True
            Me.rdbAllDist.Enabled = True
            Me.rdbCertainDisc.Enabled = True
            Me.rdbGroupDistributor.Enabled = True
            Me.chkBrand.ReadOnly = False
            Me.chkBrandPacks.ReadOnly = False
            'Me.chkTargetPOByProduct.Enabled = True
            'Me.chkTargetPOByProduct.Enabled = True

            Me.chlCertainDisc.ReadOnly = False
            Me.chkGroupDist.ReadOnly = False
            Me.chkTargetPOPerPackSize.Enabled = Me.chkTargetPOPerPackSize.Checked
            Me.chkTargetPOPerBrand.Enabled = Me.chkTargetPOPerBrand.Checked
        End If
        Me.btnSave.Enabled = CMain.IsSystemAdministrator Or NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DiscDDorDR
        Me.grdProgDisc.AllowAddNew = IIf(NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscPrice, Janus.Windows.GridEX.InheritableBoolean.True, Janus.Windows.GridEX.InheritableBoolean.True)
        Me.grdProgDisc.AllowEdit = IIf(NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DiscDDorDR, Janus.Windows.GridEX.InheritableBoolean.True, Janus.Windows.GridEX.InheritableBoolean.True)
        Me.grdProgDisc.AllowDelete = IIf(NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DiscDDorDR, Janus.Windows.GridEX.InheritableBoolean.True, Janus.Windows.GridEX.InheritableBoolean.True)
        Me.isLoadingCombo = False
    End Sub
    Private Function SaveData() As Boolean
        Dim retval As Boolean = True
        If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert Then
            Me.domPrice = New Nufarm.Domain.DiscPrice()
            Me.domPrice.DsProgDesc = Me.dsProgDisc
        ElseIf Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
            If Not Me.hasChanged() Then
                Return False
            End If
        End If
        With domPrice
            .NameApp = Me.txtDescriptions.Text
            .ApplyDate = Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString())
            .EndDate = Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString())
            .ProgramID = Me.txtProgramID.Text.Trim()
            If rdbAllDist.Checked Then
                .ApplyTo = "AL"
                .ListDistributors = New List(Of String)
                .ListGroupDist = New List(Of String)
            ElseIf Me.rdbCertainDisc.Checked Then
                .ApplyTo = "CD"
                Dim listDistr As New List(Of String)
                For i As Integer = 0 To Me.chlCertainDisc.CheckedValues().Length - 1
                    listDistr.Add(Me.chlCertainDisc.CheckedValues()(i).ToString())
                Next
                .ListDistributors = listDistr
                If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert Then
                    .HasChangedDistr = True
                End If
                .ListGroupDist = New List(Of String)
            ElseIf Me.rdbGroupDistributor.Checked Then
                .ApplyTo = "GD"
                Dim listGrp As New List(Of String)
                For i As Integer = 0 To Me.chkGroupDist.CheckedValues().Length - 1
                    listGrp.Add(Me.chkGroupDist.CheckedValues()(i).ToString())
                Next
                .ListGroupDist = listGrp
                If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert Then
                    .HasChangedGroups = True
                End If
                .ListDistributors = New List(Of String)
            End If
            Dim listBrands As New List(Of String)
            For i As Integer = 0 To Me.chkBrand.CheckedValues().Length - 1
                listBrands.Add(Me.chkBrand.CheckedValues()(i).ToString())
            Next
            .ListBrands = listBrands
            If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert Then
                .HasChangedBrands = True
            End If
            Dim listBrandPacks As New List(Of String)

            If Not IsNothing(Me.chkBrandPacks.CheckedValues()) Then
                If Me.chkBrandPacks.CheckedValues().Length > 0 Then
                    For i As Integer = 0 To chkBrandPacks.CheckedValues().Length - 1
                        listBrandPacks.Add(chkBrandPacks.CheckedValues()(i).ToString())
                    Next
                    If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert Then
                        .HasChangedBrandPacks = True
                    End If
                End If
            End If
            .ListBrandPacks = listBrandPacks
            If Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Insert Then
                .CreatedDate = NufarmBussinesRules.SharedClass.ServerDate()
                .CreatedBy = NufarmBussinesRules.User.UserLogin.UserName
            ElseIf Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update Then
                .ModifiedBy = NufarmBussinesRules.User.UserLogin.UserName
                .ModifiedDate = NufarmBussinesRules.SharedClass.ServerDate()
            End If
        End With
        Return Me.clsDiscountPrice.SaveData(domPrice, Me.Mode, False) ''langsung load data
    End Function
    Private Sub RunQuery(ByVal SearchString As String, ByVal SearchBy As String)
        Dim Dv As DataView = Nothing : Me.Isloadingrow = True
        Dv = Me.clsDiscountPrice.PopulateQuery(SearchBy, SearchString, Me.PageIndex, Me.PageSize, Me.RowCount, _
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
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.DiscDDorDR = True Then
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.TManager1.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
            If (NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscPrice = True) And (NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DiscDDorDR = True) Then
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_INSERT.DiscPrice = True Then
                Me.btnAddNew.Visible = True
                Me.btnSave.Visible = True
            Else
                Me.btnAddNew.Visible = False
                Me.btnSave.Visible = False
            End If
        End If
    End Sub

    Private Sub DiscountDDOrDR_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.hasChanged() Then
            If Me.ShowConfirmedMessage("Are you sure you want to leave") = Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Return
            End If
        End If
    End Sub

    Private Sub DiscountPriceFM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        Try
            Me.dtPicFrom.Text = ""
            'check distributor group
            DVGroup = Me.clsDiscountPrice.getDistGroup(0, Me.domPrice.ListGroupDist, False)
            If IsNothing(Me.DVGroup) Then
                Me.gridGroupDist.Visible = False
                Me.chkGroupDist.Visible = False
                Me.rdbGroupDistributor.Visible = False
            ElseIf Me.DVGroup.Count <= 0 Then
                Me.gridGroupDist.Visible = False
                Me.chkGroupDist.Visible = False
                Me.rdbGroupDistributor.Visible = False
            Else
                Me.chlCertainDisc.Size = Me.custSizeChkCerDist
                Me.chkGroupDist.SetDataBinding(DVGroup, "")
            End If
            'get distributor
            'DVDistributor = Me.clsDiscountPrice.getDistributor(0, Me.domPrice.ListDistributors, False)
            'Me.chlCertainDisc.SetDataBinding(DVDistributor, "")
            'DVBrand = Me.clsDiscountPrice.getBrand(0, Me.domPrice.ListBrands, False)
            ''set default datetime to current date
            'Me.chkBrand.SetDataBinding(DVBrand, "")
            Dim startDate As DateTime = DateTime.Now
            Dim endDate As DateTime = DateTime.Now
            Me.clsDiscountPrice.GetCurrentAgreement(startDate, endDate, False)
            Me.dtPicFrom.Value = startDate
            Me.dtPicEndDate.Value = endDate
            Me.curStartAgreement = startDate
            Me.curEndAgreement = endDate
            'Me.dtPicFrom.Value = NufarmBussinesRules.SharedClass.ServerDate()
            Me.pnlEntry.Height = 0
            Me.FilterEditor1.Visible = False
            Me.LoadData()
            Me.isLoadingCombo = False
            Me.Isloadingrow = False
            Me.chlCertainDisc.ReadOnly = True
            Me.chkGroupDist.ReadOnly = True
            Me.btnSave.Enabled = False
            Me.btnEditRow.Enabled = False
            Me.hasLoadForm = True
            Me.checkedCHK = checkedValusFrom.DropDown
            Cursor = Cursors.Default

        Catch ex As Exception
            Me.Isloadingrow = False
            isLoadingCombo = False
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
        ReadAccess()
    End Sub
    Private Sub rdbAllDist_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAllDist.CheckedChanged
        If Me.isLoadingCombo Or Me.Isloadingrow Then : Return : End If
        Cursor = Cursors.WaitCursor
        If rdbAllDist.Checked Then
            Me.chlCertainDisc.UncheckAll()
            Me.chlCertainDisc.Text = ""
            Me.chlCertainDisc.ReadOnly = True
            Me.chkGroupDist.UncheckAll()
            Me.chkGroupDist.Text = ""
            Me.chkGroupDist.ReadOnly = True
            Me.gridGroupDist.Visible = False
        End If
        Cursor = Cursors.Default
    End Sub
    Private Sub rdbCertainDisc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCertainDisc.CheckedChanged
        If Me.isLoadingCombo Or Me.Isloadingrow Then : Return : End If
        If rdbCertainDisc.Checked Then
            If IsNothing(Me.chkBrand.CheckedValues()) Then
                Me.ShowMessageInfo("Please define brand before this") : Me.chkBrand.Focus()
                rdbCertainDisc.Checked = False
                Return
            End If
            If Me.chkBrand.CheckedValues.Length <= 0 Then
                Me.ShowMessageInfo("Please define brand before this") : rdbCertainDisc.Checked = False : Me.chkBrand.Focus() : Return
            End If
            'check Brandpacks

            Cursor = Cursors.WaitCursor
            Me.chlCertainDisc.ReadOnly = False
            ''get distributor
            Dim listCheckedBrands As New List(Of String)
            If Not IsNothing(Me.chkBrand.CheckedValues()) Then
                If Me.chkBrand.CheckedValues.Length > 0 Then
                    For i As Integer = 0 To Me.chkBrand.CheckedValues.Length - 1
                        listCheckedBrands.Add(Me.chkBrand.CheckedValues(i).ToString())
                    Next
                End If
            End If
            Dim listCheckedBrandPacks As New List(Of String)
            If Not IsNothing(Me.chkBrandPacks.CheckedValues()) Then
                If Me.chkBrandPacks.CheckedValues.Length > 0 Then
                    For i As Integer = 0 To Me.chkBrandPacks.CheckedValues.Length - 1
                        listCheckedBrandPacks.Add(Me.chkBrandPacks.CheckedValues(i).ToString())
                    Next
                End If
            End If
            Me.DVDistributor = Me.clsDiscountPrice.getDistributor(0, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), listCheckedBrands, listCheckedBrandPacks, New List(Of String), True)
            Me.chlCertainDisc.SetDataBinding(Me.DVDistributor, "")
            Me.chlCertainDisc.Focus()
            Me.chkGroupDist.UncheckAll()
            Me.chkGroupDist.Text = ""
            Me.chkGroupDist.ReadOnly = True
            Me.gridGroupDist.Visible = False
        End If
        Cursor = Cursors.Default
    End Sub
    Private Sub chkGroupDist_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGroupDist.CheckedValuesChanged
        Try
            If Me.isLoadingCombo Or Me.Isloadingrow Then : Return : End If
            If Not chkGroupDist.CheckedValues Is Nothing Then
                If chkGroupDist.CheckedValues.Length > 0 Then
                    Me.Cursor = Cursors.WaitCursor
                    Dim RowFilter As String = "DISTRIBUTOR_ID IN('"
                    Dim valGroups As Integer = Me.chkGroupDist.CheckedValues().Length
                    For i As Integer = 0 To valGroups - 1
                        RowFilter = RowFilter + chkGroupDist.CheckedValues()(i).ToString() + "'"
                        If i < valGroups - 1 Then
                            RowFilter = RowFilter + ","
                        End If
                    Next
                    RowFilter = RowFilter + ")"
                    If RowFilter <> "GRP_ID IN(')" Then
                        Me.DVDistributor.RowFilter = ""
                        Me.DVDistListGroup.RowFilter = RowFilter
                        Me.gridGroupDist.SetDataBinding(DVDistListGroup, "")
                    End If
                    Me.Cursor = Cursors.Default
                End If
            End If
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub rdbGroupDistributor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbGroupDistributor.CheckedChanged
        Cursor = Cursors.WaitCursor
        If rdbGroupDistributor.Checked Then
            Me.chlCertainDisc.UncheckAll()
            Me.chlCertainDisc.Text = ""
            Me.chlCertainDisc.ReadOnly = True
            Me.chkGroupDist.ReadOnly = False
            Try
                Me.DVDistListGroup = Me.clsDiscountPrice.getDistGroupList(True)
                Me.chkGroupDist.SetDataBinding(Me.DVGroup, "")
            Catch ex As Exception
                Me.ShowMessageError(ex.Message)
            End Try
            Me.chkGroupDist.Focus()
        End If
        Cursor = Cursors.Default
    End Sub
    Private Sub TManager1_GridCurrentCell_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.GridCurrentCell_Changed
        If Me.Isloadingrow Then : Return : End If
        If Me.Isloadingrow Then : Return : End If
        If Not Me.hasLoadForm Then : Return : End If
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
                    'If Me.pnlEntryAndFilter.Height = Me.pnlEntryParentHeight Then ''data sedang di buka di form entry
                    If Me.pnlEntry.Height = Me.pnlentryHeight Then
                        Me.InflateData()
                        Me.btnEditRow.Checked = True
                        Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
                        Me.btnAddNew.Checked = False
                        Me.Isloadingrow = False
                        isLoadingCombo = False

                    End If
                    'End If
                    TManager1.GridEX1.Focus()
                    Me.btnEditRow.Enabled = CMain.IsSystemAdministrator Or NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DiscDDorDR
                    'Me.btnSave.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DiscDDorDR
                Else
                    Me.btnEditRow.Enabled = False
                    Me.Isloadingrow = True : Me.isLoadingCombo = True : Me.ClearEntry()
                    Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None
                    Me.Isloadingrow = False : Me.isLoadingCombo = False
                    Me.btnSave.Enabled = False
                    Return
                End If
            Else
                Me.btnEditRow.Enabled = False
                Me.Isloadingrow = True : Me.isLoadingCombo = True : Me.ClearEntry()
                Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.None
                Me.Isloadingrow = False : Me.isLoadingCombo = False
                Me.btnSave.Enabled = False
                Return
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, "_TManager1_GridCurrentCell_Changed")
        Finally
            Me.Cursor = Cursors.Default
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TManager1_GridDoubleClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TManager1.GridDoubleClicked
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
            Me.InflateData()
            Me.btnAddNew.Checked = False
            Me.btnEditRow.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.DiscDDorDR
            Me.btnEditRow.Checked = True
            'Me.pnlEntryAndFilter.Height = Me.pnlEntryParentHeight
            Me.pnlEntry.Height = Me.pnlentryHeight
            'Me.pnlEntry.Dock = DockStyle.Fill
            Me.Mode = NufarmBussinesRules.common.Helper.SaveMode.Update
            Me.Isloadingrow = False
            isLoadingCombo = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Isloadingrow = False
            isLoadingCombo = False
            Me.btnEditRow.Enabled = False
            Me.ShowMessageError(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub pnlEntry_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlEntry.Enter
        Me.AcceptButton = Nothing
    End Sub

    Private Sub grdProgDisc_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdProgDisc.AddingRecord
        'check brand & programname
        Try

            Me.chkBrand.Focus()
            Me.chkBrand.DroppedDown = True
            If Me.chkBrand.DropDownList().GetCheckedRows() Is Nothing Then
                Me.ShowMessageInfo("Please mark brand before this") : e.Cancel = True
                Me.chkBrand.DroppedDown = False
                Me.chkBrand.Focus()
                Return
            ElseIf Me.chkBrand.DropDownList().GetCheckedRows.Length <= 0 Then
                Me.ShowMessageInfo("Please mark brand before this")
                e.Cancel = True
                Me.chkBrand.DroppedDown = False
                Me.chkBrand.Focus()
                Return
            End If
            Me.chkBrand.DroppedDown = False
            If Me.txtDescriptions.Text.Length <= 3 Then
                Me.ShowMessageInfo("Please enter description/program name before this") : e.Cancel = True
                Me.txtDescriptions.Focus()
                Return
            End If
            Me.grdProgDisc.Focus()
            'check all value
            Dim MoreThanQty As Object = Me.grdProgDisc.GetValue("MoreThanQty")
            Dim Disc As Object = Me.grdProgDisc.GetValue("Disc")
            Dim DiscType As Object = Me.grdProgDisc.GetValue("TypeApp")
            If IsNothing(MoreThanQty) Then
                Me.ShowMessageInfo("Please enter morethanqty value")
                e.Cancel = True
                Me.grdProgDisc.MoveToNewRecord()
                Return
            ElseIf Convert.ToDecimal(MoreThanQty) <= 0 Then
                Me.ShowMessageInfo("Please enter morethanqty value")
                e.Cancel = True
                Me.grdProgDisc.MoveToNewRecord()
                Return
            End If
            If IsNothing(Disc) Then
                Me.ShowMessageInfo("Please enter Discount value")
                e.Cancel = True
                Me.grdProgDisc.MoveToNewRecord()
            ElseIf Convert.ToDecimal(Disc) <= 0 Then
                Me.ShowMessageInfo("Please enter Discount value")
                e.Cancel = True
                Me.grdProgDisc.MoveToNewRecord()
            End If
            If IsNothing(DiscType) Then
                Me.ShowMessageInfo("Please enter Discount Type")
                e.Cancel = True
                Me.grdProgDisc.MoveToNewRecord()
            ElseIf DiscType.ToString() = "" Then
                Me.ShowMessageInfo("Please enter Discount Type")
                e.Cancel = True
                Me.grdProgDisc.MoveToNewRecord()
            End If
            If Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible Then
                If IsNothing(Me.grdProgDisc.GetValue("BRANDPACK_ID")) Then
                    Me.ShowMessageInfo("Please enter brandpack")
                    e.Cancel = True
                    Me.grdProgDisc.MoveToNewRecord()
                ElseIf Me.grdProgDisc.GetValue("BRANDPACK_ID") Is DBNull.Value Then
                    Me.grdProgDisc.MoveToNewRecord()
                    e.Cancel = True
                    Me.grdProgDisc.MoveToNewRecord()
                End If
            End If
            Me.isAddingRow = True
            If Me.CheckAvailability = True Then
                e.Cancel = True : Me.grdProgDisc.MoveToNewRecord() : Cursor = Cursors.Default : Return
            End If
            Me.grdProgDisc.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName)
            Me.grdProgDisc.SetValue("CreatedDate", NufarmBussinesRules.SharedClass.ServerDate)

        Catch ex As Exception
            Me.ShowMessageError(ex.Message) : e.Cancel = True
        End Try
        Me.isAddingRow = False
    End Sub

    Private Function CheckAvailability() As Boolean
        'check existing data
        'check all value
        If Me.grdProgDisc.RecordCount <= 0 Then
            Return False
        End If
        Dim MoreThanQty As Object = Me.grdProgDisc.GetValue("MoreThanQty")
        Dim dtDummy As DataTable = CType(Me.grdProgDisc.DataSource, DataView).ToTable.Copy()
        If Me.isAddingRow Then
            If Me.chkTargetPOPerPackSize.Checked = False And Me.chkTargetPOPerBrand.Checked = False Then 'ID == flag
                Dim Flag As String = Me.grdProgDisc.GetValue("TypeApp").ToString()
                ''check di dataview
                Dim checkedRows() As DataRow = dtDummy.Select("TypeApp = '" & Flag & "'  AND MoreThanQty = " & MoreThanQty)
                If checkedRows.Length > 0 Then
                    Me.ShowMessageInfo(Me.MessageDataHasExisted & vbCrLf & "Or double input data")
                    Return True
                End If
            ElseIf Me.chkTargetPOPerPackSize.Checked = True And Me.chkTargetPOPerBrand.Checked = False Then
                'berarti ID sama BrandpackID dan flag
                Dim BrandPackID As String = Me.grdProgDisc.GetValue("BRANDPACK_ID").ToString()
                Dim Flag As String = Me.grdProgDisc.GetValue("TypeApp").ToString()
                ''check di dataview
                Dim checkedRows() As DataRow = dtDummy.Select("BRANDPACK_ID = '" & BrandPackID & "' AND TypeApp = '" & Flag & "' AND MoreThanQty = " & MoreThanQty)
                If checkedRows.Length > 0 Then
                    Me.ShowMessageInfo(Me.MessageDataHasExisted & vbCrLf & "Or double input data")
                    Return True
                End If
            ElseIf Me.chkTargetPOPerPackSize.Checked = False And Me.chkTargetPOPerBrand.Checked = True Then
                'berarti ID sama BrandID dan flag
                Dim BrandID As String = Me.grdProgDisc.GetValue("BRAND_ID").ToString()
                Dim Flag As String = Me.grdProgDisc.GetValue("TypeApp").ToString()
                ''check di dataview
                Dim checkedRows() As DataRow = dtDummy.Select("BRAND_ID = '" & BrandID & "' AND TypeApp = '" & Flag & "'  AND MoreThanQty = " & MoreThanQty)
                If checkedRows.Length > 0 Then
                    Me.ShowMessageInfo(Me.MessageDataHasExisted & vbCrLf & "Or double input data")
                    Return True
                End If
            End If
        Else
            'check double input data di grid progdisc
            Dim listID As New List(Of String)
            If Me.chkTargetPOPerPackSize.Checked Then
                'berarti ID sama BrandpackID dan flag
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                    Dim Flag As String = row.Cells("TypeApp").Value.ToString()
                    If Not IsNothing(row.Cells("BRANDPACK_ID").Value) And Not IsDBNull(row.Cells("BRANDPACK_ID").Value) Then
                        Dim BrandPackID As String = row.Cells("BRANDPACK_ID").Value.ToString()
                        MoreThanQty = row.Cells("MoreThanQty").Value
                        ''check di dataview
                        Dim ID As String = BrandPackID & Flag & MoreThanQty
                        If listID.Contains(ID) Then
                        Else
                            listID.Add(ID)
                            Dim checkedRows() As DataRow = dtDummy.Select("BRANDPACK_ID = '" & BrandPackID & "' AND TypeApp = '" & Flag & "'  AND MoreThanQty = " & MoreThanQty)
                            If checkedRows.Length > 1 Then
                                Me.ShowMessageInfo("double input data on progressive discount")
                                Cursor = Cursors.Default : Return False
                            End If
                        End If
                    End If
                Next
            ElseIf Me.chkTargetPOPerBrand.Checked Then
                'berarti ID sama BrandID dan flag
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                    If Not IsNothing(row.Cells("BRAND_ID").Value) And Not IsDBNull(row.Cells("BRAND_ID").Value) Then
                        Dim BrandID As String = row.Cells("BRAND_ID").Value.ToString()
                        Dim Flag As String = row.Cells("TypeApp").Value.ToString()
                        MoreThanQty = row.Cells("MoreThanQty").Value
                        Dim ID As String = BrandID & Flag & MoreThanQty
                        If listID.Contains(ID) Then
                        Else
                            listID.Add(ID)
                            Dim checkedRows() As DataRow = dtDummy.Select("BRAND_ID = '" & BrandID & "' AND TypeApp = '" & Flag & "'  AND MoreThanQty = " & MoreThanQty)
                            If checkedRows.Length > 1 Then
                                Me.ShowMessageInfo("double input data on progressive discount")
                                Cursor = Cursors.Default : Return False
                            End If
                        End If
                    End If
                Next
            Else
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                    Dim Flag As String = row.Cells("TypeApp").Value.ToString()
                    ''check di dataview
                    MoreThanQty = row.Cells("MoreThanQty").Value
                    Dim ID As String = Flag & MoreThanQty
                    If listID.Contains(ID) Then
                    Else
                        listID.Add(ID)
                        Dim checkedRows() As DataRow = dtDummy.Select("TypeApp = '" & Flag & "'  AND MoreThanQty = " & MoreThanQty)
                        If checkedRows.Length > 1 Then
                            Me.ShowMessageInfo("double input data on progressive discount")
                            Cursor = Cursors.Default : Return False
                        End If
                    End If
                Next
            End If
        End If
        Return False
    End Function

    Private Sub grdProgDisc_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdProgDisc.KeyDown
        If e.KeyCode = Keys.F7 Then
            Me.ExportData(Me.grdProgDisc)
        End If
    End Sub

    Private Sub gridGroupDist_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridGroupDist.KeyDown
        If e.KeyCode = Keys.F7 Then
            Me.ExportData(Me.gridGroupDist)
        End If
    End Sub

    Private Sub grdProgDisc_RecordsDeleted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdProgDisc.RecordsDeleted
        Me.grdProgDisc.UpdateData()
    End Sub

    Private Sub grdProgDisc_RecordAdded(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdProgDisc.RecordAdded
        Me.Isloadingrow = False
        Me.grdProgDisc.UpdateData()
    End Sub

    Private Sub dtPicFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicFrom.ValueChanged
        If Me.Isloadingrow Then : Return : End If
        If Me.isLoadingCombo Then : Return : End If
        Try
            'get distributor
            'DVDistributor = Me.clsDiscountPrice.getDistributor(0, Me.domPrice.ListDistributors, False)
            'Me.chlCertainDisc.SetDataBinding(DVDistributor, "")
            Cursor = Cursors.WaitCursor
            Me.isLoadingCombo = True
            DVBrand = Me.clsDiscountPrice.getBrand(0, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                        Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), New List(Of String), False)
            'set default datetime to current date
            Me.chkBrand.SetDataBinding(DVBrand, "")
            Me.chkBrandPacks.SetDataBinding(Nothing, "")
            Me.isLoadingCombo = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Me.isLoadingCombo = False
            Cursor = Cursors.Default
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, "_dtPicFrom_ValueChanged")
        End Try
    End Sub

    Private Sub dtPicEndDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicEndDate.ValueChanged
        If Me.Isloadingrow Then : Return : End If
        If Me.isLoadingCombo Then : Return : End If
        Try
            Cursor = Cursors.WaitCursor
            'get distributor
            'DVDistributor = Me.clsDiscountPrice.getDistributor(0, Me.domPrice.ListDistributors, False)
            'Me.chlCertainDisc.SetDataBinding(DVDistributor, "")
            Me.isLoadingCombo = True
            DVBrand = Me.clsDiscountPrice.getBrand(0, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
            Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), New List(Of String), False)
            'set default datetime to current date
            Me.chkBrand.SetDataBinding(DVBrand, "")
            Me.chkBrandPacks.SetDataBinding(Nothing, "")
            Me.isLoadingCombo = False
            Cursor = Cursors.Default
            'If Me.rdbApplyDischBrand.Checked Then
            '    DVBrand = Me.clsDiscountPrice.getBrand(0, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
            '                Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), New List(Of String), False)
            '    'set default datetime to current date
            '    Me.chkBrand.DropDownDataSource = DVBrand
            '    Me.chkBrand.RetrieveStructure()
            '    Me.chkBrand.DropDownList().Columns("BRAND_ID").ShowRowSelector = True
            '    Me.chkBrand.DropDownList().AutoSizeColumns()
            '    Me.chkBrand.DropDownDataMember = "BRAND_ID"
            '    Me.chkBrand.DropDownDisplayMember = "BRAND_NAME"
            '    Me.chkBrand.DropDownValueMember = "BRAND_ID"
            'Else
            '    DVBrandPack = Me.clsDiscountPrice.getBrandBrandPack(0, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
            '                Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), New List(Of String), False)
            '    'set default datetime to current date
            '    Me.chkBrand.DropDownDataSource = DVBrandPack
            '    Me.chkBrand.RetrieveStructure()
            '    Me.chkBrand.DropDownList().Columns("BRANDPACK_ID").ShowRowSelector = True
            '    Me.chkBrand.DropDownList().AutoSizeColumns()
            '    Me.chkBrand.DropDownDataMember = "BRANDPACK_ID"
            '    Me.chkBrand.DropDownDisplayMember = "BRANDPACK_NAME"
            '    Me.chkBrand.DropDownValueMember = "BRANDPACK_ID"
            'End If
            'me.chkBrand.DropDownDataSource 
        Catch ex As Exception
            Me.isLoadingCombo = False
            Cursor = Cursors.Default
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, "_dtPicEndDate_ValueChanged")
        End Try

    End Sub
    Private Function CheckRefGridProgDiscForBrand() As Boolean
        'check gridprog disc dimana brand id tidak ada di dvprogbrand
        'bila ada brandid di gridprogdisc tetapi tidak ada di dvprogbrand hapus
        'sebelum hapus chek reference
        Dim DVCopyBrand As DataView = Me.DVBrand.Table().Copy().DefaultView
        DVCopyBrand.Sort = "BRAND_ID"
        If Me.chkTargetPOPerBrand.Checked Then
            Dim oriSorted As String = Me.DVProgBrand.Sort()
            Me.DVProgBrand.Sort = "BRAND_ID"
            Dim rowToDelete As New List(Of String)
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                Dim index As Int16 = Me.DVProgBrand.Find(row.Cells("BRAND_ID").Value)
                If index <= -1 Then
                    If Not IsNothing(row.Cells("BRAND_ID").Value) And Not IsDBNull(row.Cells("BRAND_ID").Value) Then
                        ''check reverence data
                        If Not IsNothing(row.Cells("HasRef").Value) And Not IsDBNull(row.Cells("HasRef").Value) Then
                            If CInt(row.Cells("HasRef").Value) > 0 Then
                                Dim index1 As Integer = DVCopyBrand.Find(row.Cells("BRAND_ID").Value)
                                If index1 <> -1 Then
                                    Me.ShowMessageInfo("Can not delete progressive discount" & vbCrLf & _
                                    "For Brand " & DVCopyBrand(index1)("BRAND_NAME").ToString() & vbCrLf & _
                                    "Because has child-referenced data")
                                    If Not Me.isLoadingCombo Then : Me.isLoadingCombo = True : End If
                                    'balikan data
                                    Me.chkBrand.CheckedValues = Me.listCheckedBrands
                                    Me.isLoadingCombo = False : Me.Cursor = Cursors.Default
                                    Return True
                                End If
                            End If
                        End If
                        If Not rowToDelete.Contains(row.Cells("BRAND_ID").Value) Then
                            rowToDelete.Add(row.Cells("BRAND_ID").Value)
                        End If
                    End If
                End If
            Next
            For i As Integer = 0 To rowToDelete.Count - 1
                Dim f As Integer = Me.grdProgDisc.FindAll(Me.grdProgDisc.RootTable.Columns("BRAND_ID"), Janus.Windows.GridEX.ConditionOperator.Equal, rowToDelete(i))
                If f > 0 Then
                    Me.grdProgDisc.Delete()
                End If
            Next
            Me.DVProgBrand.Sort = oriSorted
        Else
            'bila opsi tidak berdasarkan by brand, hapus semua data di grid progdisch dimana brand nya tidak bernilai null dan ''
            'DVCopy, data yang sudah di filter dan hanya untuk menghapus data mana saja yang ada di DV ori
            Dim DVCopy As DataView = CType(Me.grdProgDisc.DataSource, DataView).Table.Copy().DefaultView
            Dim DVOry As DataView = CType(Me.grdProgDisc.DataSource, DataView)
            'cari data dimana brandid di isi bukan null
            DVOry.Sort = "BRAND_ID"
            DVCopy.RowFilter = "BRAND_ID IS NOT NULL AND BRAND_ID <> ''"
            For i As Integer = 0 To DVCopy.Count - 1
                Dim index As Integer = DVOry.Find(DVCopy(i)("BRAND_ID").ToString())
                If index <> -1 Then
                    'check reverence
                    If Not IsNothing(DVOry(index)("BRAND_ID")) And Not IsDBNull(DVOry(index)("BRAND_ID")) Then
                        ''check reverence data
                        If Not IsNothing(DVOry(index)("HasRef")) And Not IsDBNull(DVOry(index)("HasRef")) Then
                            If CInt(DVOry(index)("HasRef")) > 0 Then
                                Dim index1 As Integer = DVCopyBrand.Find(DVOry(index)("BRAND_ID"))
                                If index1 <> -1 Then
                                    Me.ShowMessageInfo("Can not delete progressive discount" & vbCrLf & _
                                    "For Brand " & DVCopyBrand(index1)("BRAND_NAME").ToString() & vbCrLf & _
                                    "Because has child-referenced data")
                                    If Not Me.isLoadingCombo Then : Me.isLoadingCombo = True : End If
                                    'balikan data
                                    Me.chkBrand.CheckedValues = Me.listCheckedBrands
                                    Me.isLoadingCombo = False : Me.Cursor = Cursors.Default
                                    Return True
                                End If
                            End If
                        End If
                    End If
                    DVOry.Delete(index)
                End If
            Next
        End If
        Return False
    End Function
    Private Function CheckRefGridProgDiscForBrandPack() As Boolean
        Dim strlListCheckedBrandPacks = "IN('"
        For i As Integer = 0 To Me.DVProgBrandPack.Count - 1
            strlListCheckedBrandPacks &= Me.DVProgBrandPack(i)("BRANDPACK_ID").ToString() & "'"
            If i < Me.DVProgBrandPack.Count - 1 Then
                strlListCheckedBrandPacks &= ",'"
            End If
        Next
        strlListCheckedBrandPacks &= ")"
        Dim DVCopyBrandPack As DataView = Me.DVBrandPack.ToTable().Copy().DefaultView()
        DVCopyBrandPack.Sort = "BRANDPACK_ID"
        If Me.chkTargetPOPerPackSize.Checked Then
            Dim oriSorted As String = Me.DVProgBrandPack.Sort()
            Me.DVProgBrandPack.Sort = "BRANDPACK_ID"
            Dim rowToDelete As New List(Of String)
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                Dim index As Int16 = Me.DVProgBrandPack.Find(row.Cells("BRANDPACK_ID").Value)
                If index <= -1 Then
                    If Not IsNothing(row.Cells("BRANDPACK_ID").Value) And Not IsDBNull(row.Cells("BRANDPACK_ID").Value) Then
                        ''check reverence data
                        If Not IsNothing(row.Cells("HasRef").Value) And Not IsDBNull(row.Cells("HasRef").Value) Then
                            If CInt(row.Cells("HasRef").Value) > 0 Then
                                Dim index1 As Integer = DVCopyBrandPack.Find(row.Cells("BRANDPACK_ID").Value)
                                Me.ShowMessageInfo("Can not delete progressive discount" & vbCrLf & _
                                "For BrandPack " & DVCopyBrandPack(index1)("BRANDPACK_NAME").ToString() & vbCrLf & _
                                "Because has child-referenced data")
                                If Not Me.isLoadingCombo Then : Me.isLoadingCombo = True : End If
                                'balikan data
                                Me.chkBrandPacks.CheckedValues = Me.listCheckedBrandPacks
                                Me.isLoadingCombo = False : Me.Cursor = Cursors.Default
                                Return True
                            End If
                        End If
                        If Not rowToDelete.Contains(row.Cells("BRANDPACK_ID").Value) Then
                            rowToDelete.Add(row.Cells("BRANDPACK_ID").Value)
                        End If
                    End If
                End If
            Next
            For i As Integer = 0 To rowToDelete.Count - 1
                Dim f As Integer = Me.grdProgDisc.FindAll(Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID"), Janus.Windows.GridEX.ConditionOperator.Equal, rowToDelete(i))
                If f > 0 Then
                    Me.grdProgDisc.Delete()
                End If
            Next
            Me.DVProgBrandPack.Sort = oriSorted
        Else
            'bila opsi tidak berdasarkan by brand, hapus semua data di grid progdisch dimana brand nya tidak bernilai null dan ''

            'cari data dimana brandpack di isi bukan null
            'DVCopy data yang sudah di filter dan hanya untuk menghapus data mana saja yang ada di DV ori
            Dim DVCopy As DataView = CType(Me.grdProgDisc.DataSource, DataView).Table.Copy().DefaultView
            Dim DVOry As DataView = CType(Me.grdProgDisc.DataSource, DataView)
            DVOry.Sort = "BRANDPACK_ID"
            DVCopy.RowFilter = "BRANDPACK_ID IS NOT NULL AND BRANDPACK_ID <> ''"
            If strlListCheckedBrandPacks <> "IN(')" Then
                DVCopy.RowFilter = "BRANDPACK_ID IS NOT NULL AND BRANDPACK_ID <> '' AND BRANDPACK_ID " & strlListCheckedBrandPacks
            End If
            For i As Integer = 0 To DVCopy.Count - 1
                Dim index As Integer = DVOry.Find(DVCopy(i)("BRANDPACK_ID").ToString())
                If index <> -1 Then
                    ''check reverence
                    If Not IsNothing(DVOry(index)("BRANDPACK_ID")) And Not IsDBNull(DVOry(index)("BRANDPACK_ID")) Then
                        ''check reverence data
                        If Not IsNothing(DVOry(index)("HasRef")) And Not IsDBNull(DVOry(index)("HasRef")) Then
                            If CInt(DVOry(index)("HasRef")) > 0 Then
                                Dim index1 As Integer = DVCopyBrandPack.Find(DVOry(index)("BRANDPACK_ID"))
                                If index1 <> -1 Then
                                    Me.ShowMessageInfo("Can not delete progressive discount " & vbCrLf & _
                                    "For BrandPack " & DVCopyBrandPack(index1)("BRANDPACK_NAME").ToString() & vbCrLf & _
                                    "Because has child-referenced data")
                                    If Not Me.isLoadingCombo Then : Me.isLoadingCombo = True : End If
                                    'balikan data
                                    Me.chkBrand.CheckedValues = Me.listCheckedBrands
                                    Me.isLoadingCombo = False : Me.Cursor = Cursors.Default
                                    Return True
                                End If
                            End If
                        End If
                    End If
                    DVOry.Delete(index)
                End If
            Next
        End If
        Return False
    End Function
    Private Function CheckRefGridDisc(ByVal CheckGridBy) As Boolean
        'bila opsi tidak berdasarkan by brand, hapus semua data di grid progdisch dimana brand nya tidak bernilai null dan ''
        'cari data dimana brandpack di isi bukan null
        'DVCopy data yang sudah di filter dan hanya untuk menghapus data mana saja yang ada di DV ori
        Dim DVOry As DataView = CType(Me.grdProgDisc.DataSource, DataView)
        For i As Integer = 0 To DVOry.Count - 1
            If Not IsNothing(DVOry(i)("HasRef")) And Not IsDBNull(DVOry(i)("HasRef")) Then
                If CInt(DVOry(i)("HasRef")) Then
                    Me.ShowMessageInfo("Can not delete progressive discount" & vbCrLf & _
                    " Because has child-referenced data")
                    If Not Me.isLoadingCombo Then : Me.isLoadingCombo = True : End If
                    'balikan data
                    If CheckGridBy = "BRANDPACK" Then
                        Me.chkBrandPacks.CheckedValues = Me.listCheckedBrandPacks
                    ElseIf CheckGridBy = "BRAND" Then
                        Me.chkBrand.CheckedValues = Me.listCheckedBrands
                    End If
                    Me.isLoadingCombo = False : Me.Cursor = Cursors.Default
                    Return True
                End If
            End If
        Next
        Return False
    End Function
    Private Sub chkBrand_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBrand.CheckedValuesChanged
        If Isloadingrow Then : Return : End If
        If isLoadingCombo Then : Return : End If
        Try
            Cursor = Cursors.WaitCursor
            Me.DVBrandPack = Nothing
            Dim IsCheckedBrand As Boolean = False
            If Not IsNothing(Me.chkBrand.CheckedValues) Then
                If Me.chkBrand.CheckedValues.Length > 0 Then
                    IsCheckedBrand = True
                End If
            End If
            Me.isLoadingCombo = True
            Me.chkBrandPacks.Text = ""
            'check grid dispro
            If IsCheckedBrand Then
                Dim listCheckedBrands As New List(Of String)
                If Not IsNothing(Me.chkBrand.CheckedValues()) Then
                    If Me.chkBrand.CheckedValues.Length > 0 Then
                        For i As Integer = 0 To Me.chkBrand.CheckedValues.Length - 1
                            listCheckedBrands.Add(Me.chkBrand.CheckedValues(i).ToString())
                        Next
                    End If
                End If

                'fill dropdown
                Dim tblProgBrand As New DataTable("DISC_PROGRESSIVE_BRAND")
                If IsNothing(Me.DVProgBrand) Then
                    With tblProgBrand
                        .Columns.Add("BRAND_ID", Type.GetType("System.String"))
                        .Columns.Add("BRAND_NAME", Type.GetType("System.String"))
                    End With
                ElseIf Me.DVProgBrand.Table.Columns.Count <= 0 Then
                    With tblProgBrand
                        .Columns.Add("BRAND_ID", Type.GetType("System.String"))
                        .Columns.Add("BRAND_NAME", Type.GetType("System.String"))
                    End With
                Else
                    tblProgBrand = Me.DVProgBrand.Table
                End If
                tblProgBrand.Rows.Clear()
                tblProgBrand.AcceptChanges()
                For Each Jrow As Janus.Windows.GridEX.GridEXRow In chkBrand.DropDownList().GetCheckedRows()
                    Dim row As DataRow = tblProgBrand.NewRow()
                    row.BeginEdit()
                    row("BRAND_ID") = Jrow.Cells("BRAND_ID").Value
                    row("BRAND_NAME") = Jrow.Cells("BRAND_NAME").Value
                    row.EndEdit()
                    tblProgBrand.Rows.Add(row)
                Next
                Me.DVProgBrand = tblProgBrand.DefaultView()
                Me.grdProgDisc.DropDowns(1).SetDataBinding(Me.DVProgBrand, "")
                If Me.chkTargetPOPerBrand.Checked Then
                    Me.grdProgDisc.RootTable.Columns("BRAND_ID").Visible = True
                End If
                If Not Me.chkTargetPOPerPackSize.Checked Then
                    Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible = False
                End If
                Me.chkTargetPOPerPackSize.Enabled = False
                'check reference brandpack in grdProgDisc

                If Me.CheckRefGridProgDiscForBrand() Then : isLoadingCombo = False : Return : End If
                Dim DVProgDiscDummy As DataView = CType(Me.grdProgDisc.DataSource, DataView).Table.Copy().DefaultView()
                Dim DVBrandPackDummy As DataView = Me.clsDiscountPrice.getBrandBrandPack(0, Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), _
                Convert.ToDateTime(Me.dtPicEndDate.Value.ToShortDateString()), listCheckedBrands, New List(Of String), False)
                If Not IsNothing(Me.listCheckedBrandPacks) Then
                    If Me.listCheckedBrandPacks.Length > 0 Then
                        'cari apakah listcheck brandpack ada semua di DVBrandPackDummy
                        DVBrandPackDummy.Sort = "BRANDPACK_ID"
                        DVProgDiscDummy.Sort = "BRANDPACK_ID"
                        For i As Integer = 0 To Me.listCheckedBrandPacks.Length - 1
                            Dim index As Integer = DVBrandPackDummy.Find(listCheckedBrandPacks(i).ToString())
                            If index <= -1 Then
                                'check reference di grid prog desc
                                Dim index1 As Integer = DVProgDiscDummy.Find(listCheckedBrandPacks(i).ToString())
                                If index1 >= 0 Then
                                    If CInt(DVProgDiscDummy(index1)("HasRef")) > 0 Then
                                        Me.ShowMessageError("Can not delete progressive discount" & vbCrLf & _
                                        " For BrandPack " & DVBrandPackDummy(index)("BRANDPACK_NAME").ToString() & vbCrLf & _
                                        " Because has child-referenced data")
                                        'balikan checked values brandpack
                                        Me.chkBrandPacks.CheckedValues = Me.listCheckedBrandPacks
                                        isLoadingCombo = False : Me.Cursor = Cursors.Default : Return
                                    End If
                                End If
                            End If
                        Next
                        'hidupkan event
                        'Me.chkBrandPacks_CheckedValuesChanged(Me.chkBrandPacks, New EventArgs())
                    End If
                End If
                'set listcheckedBrandPacks
                If Me.chkTargetPOPerPackSize.Checked Then
                    Dim i As Integer = -1
                    Dim lstCheckedBrandPacks As New List(Of String)
                    DVBrandPackDummy.Sort = "BRANDPACK_ID"
                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                        Dim BrandPackID As String = row.Cells("BRANDPACK_ID").Value.ToString()
                        Dim index As Integer = DVBrandPackDummy.Find(BrandPackID)
                        If index <> -1 Then
                            If Not lstCheckedBrandPacks.Contains(BrandPackID) Then
                                lstCheckedBrandPacks.Add(BrandPackID)
                            End If
                        End If
                    Next
                    ReDim Me.listCheckedBrandPacks(-1)
                    Me.listCheckedBrandPacks = lstCheckedBrandPacks.ToArray()
                End If
                Me.DVBrandPack = DVBrandPackDummy
                Me.chkBrandPacks.SetDataBinding(DVBrandPackDummy, "")
                'hidupkan event
                Me.isLoadingCombo = False : Me.Isloadingrow = False
                Me.checkedCHK = checkedValusFrom.Other
                Me.chkBrandPacks.CheckedValues = Me.listCheckedBrandPacks
                Me.isLoadingCombo = True : Me.Isloadingrow = True
                Me.checkedCHK = checkedValusFrom.DropDown
                Me.DVBrandPack = DVBrandPackDummy
                Me.grdProgDisc.UpdateData()
            Else
                Dim DVCopy As DataView = CType(Me.grdProgDisc.DataSource, DataView)
                For i As Integer = 0 To DVCopy.Count - 1
                    'chek reference
                    If Not IsNothing(DVCopy(i)("HasRef")) And Not IsDBNull(DVCopy(i)("HasRef")) Then
                        If CInt(DVCopy(i)("HasRef")) > 0 Then
                            Me.ShowMessageError(Me.MessageCantDeleteData)
                            If Not Me.isLoadingCombo Then : Me.isLoadingCombo = True : End If
                            'balikan data
                            Me.chkBrand.CheckedValues = Me.listCheckedBrands
                            Me.isLoadingCombo = False : Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End If
                Next
                'hapus grid grid disc prog
                Me.grdProgDisc.RootTable.Columns("BRAND_ID").Visible = False
                Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible = False
                Me.chkTargetPOPerBrand.Checked = False
                Me.chkTargetPOPerPackSize.Checked = False
                Me.chkBrandPacks.SetDataBinding(Nothing, "")
                CType(Me.grdProgDisc.DataSource, DataView).Table.Rows.Clear()
                CType(Me.grdProgDisc.DataSource, DataView).Table.AcceptChanges()
            End If
            If Me.rdbCertainDisc.Checked Then
                rdbCertainDisc_CheckedChanged(Me.rdbCertainDisc, New EventArgs())
            End If
            Me.listCheckedBrands = Me.chkBrand.CheckedValues()

            Me.isLoadingCombo = False
            Me.Isloadingrow = False
            Cursor = Cursors.Default
        Catch ex As Exception
            If Not IsNothing(Me.chkBrand.DropDownDataSource) Then
                If Not IsNothing(Me.chkBrand.CheckedValues) Then
                    Me.isLoadingCombo = True
                    Me.chkBrand.CheckedValues = Me.listCheckedBrands
                End If
            End If
            Me.isLoadingCombo = False
            If Me.Isloadingrow Then : Me.Isloadingrow = False : End If
            Cursor = Cursors.Default
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, "_chkBrand_CheckedValuesChanged")
        End Try
        Me.checkedCHK = checkedValusFrom.DropDown
    End Sub

    Private Sub chkBrandPacks_CheckedValuesChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBrandPacks.CheckedValuesChanged
        If Me.Isloadingrow Then : Return : End If
        If Me.isLoadingCombo Then : Return : End If
        Try
            Dim isChecked As Boolean = False
            If Not IsNothing(Me.chkBrandPacks.CheckedValues) Then
                If Me.chkBrandPacks.CheckedValues.Length > 0 Then
                    isChecked = True
                End If
            End If
            If isChecked Then
                'fill dropdown
                Dim tblProgBrandPack As New DataTable("DISC_PROGRESSIVE_BRANDPACK")
                If IsNothing(Me.DVProgBrandPack) Then
                    With tblProgBrandPack
                        .Columns.Add("BRANDPACK_ID", Type.GetType("System.String"))
                        .Columns.Add("BRANDPACK_NAME", Type.GetType("System.String"))
                    End With
                ElseIf Me.DVProgBrandPack.Table.Columns.Count <= 0 Then
                    With tblProgBrandPack
                        .Columns.Add("BRANDPACK_ID", Type.GetType("System.String"))
                        .Columns.Add("BRANDPACK_NAME", Type.GetType("System.String"))
                    End With
                Else
                    tblProgBrandPack = Me.DVProgBrandPack.Table
                End If
                tblProgBrandPack.Rows.Clear()
                tblProgBrandPack.AcceptChanges()
                If Me.checkedCHK = checkedValusFrom.Other Then
                    Me.chkBrandPacks.Focus()
                    Me.chkBrandPacks.DroppedDown = True
                End If
                For Each Jrow As Janus.Windows.GridEX.GridEXRow In chkBrandPacks.DropDownList().GetCheckedRows()
                    Dim row As DataRow = tblProgBrandPack.NewRow()
                    row.BeginEdit()
                    row("BRANDPACK_ID") = Jrow.Cells("BRANDPACK_ID").Value
                    row("BRANDPACK_NAME") = Jrow.Cells("BRANDPACK_NAME").Value
                    row.EndEdit()
                    tblProgBrandPack.Rows.Add(row)
                Next
                If Me.checkedCHK = checkedValusFrom.DropDown Then
                    Me.chkBrandPacks.DroppedDown = False
                End If
                Me.DVProgBrandPack = tblProgBrandPack.DefaultView()
                Me.grdProgDisc.DropDowns(0).SetDataBinding(Me.DVProgBrandPack, "")
                Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible = False
                If Me.chkTargetPOPerPackSize.Checked Then
                    Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible = True
                End If
                Me.grdProgDisc.RootTable.Columns("BRAND_ID").Visible = False
                Me.isLoadingCombo = True
                Me.chkTargetPOPerBrand.Checked = False
                Me.chkTargetPOPerBrand.Enabled = False
                Me.chkTargetPOPerPackSize.Enabled = True
            Else
                Me.isLoadingCombo = True
                Me.chkTargetPOPerBrand.Enabled = True
                Me.chkTargetPOPerPackSize.Enabled = False
                Me.chkTargetPOPerPackSize.Checked = False
                Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible = False
            End If
            'check grid dispro
            If Me.grdProgDisc.RecordCount > 0 Then
                Me.isLoadingCombo = True
                If isChecked Then
                    If Me.CheckRefGridProgDiscForBrandPack() Then : Me.isLoadingCombo = False : Me.Cursor = Cursors.Default : Return : End If
                Else
                    For Each Jrow As Janus.Windows.GridEX.GridEXRow In grdProgDisc.GetRows()
                        Jrow.BeginEdit()
                        If Not IsNothing(Jrow.Cells("HasRef").Value) And Not IsDBNull(Jrow.Cells("HasRef").Value) And Not IsNothing(Jrow.Cells("BRANDPACK_ID").Value) And Not IsDBNull(Jrow.Cells("BRANDPACK_ID").Value) Then
                            If CInt(Jrow.Cells("HasRef").Value) > 0 Then
                                Me.ShowMessageError(Me.MessageCantDeleteData)
                                If Not Me.isLoadingCombo Then : Me.isLoadingCombo = True : End If
                                'balikan data
                                Me.chkBrand.CheckedValues = Me.listCheckedBrands
                                Me.isLoadingCombo = False : Me.Cursor = Cursors.Default
                                Exit Sub
                            End If
                        End If
                        Jrow.Cells("BRANDPACK_ID").Value = DBNull.Value
                        Jrow.EndEdit()
                    Next
                    grdProgDisc.UpdateData()
                    'hapus grid grid disc prog
                    'CType(Me.grdProgDisc.DataSource, DataView).Table.Rows.Clear()
                    'CType(Me.grdProgDisc.DataSource, DataView).Table.AcceptChanges()
                End If
            End If
            If Me.rdbAllDist.Checked Then : If Me.isLoadingCombo Then : Me.isLoadingCombo = False : End If : Return : End If
            If Me.rdbCertainDisc.Checked Then
                rdbCertainDisc_CheckedChanged(Me.rdbCertainDisc, New EventArgs())
            End If
            Me.listCheckedBrandPacks = Me.chkBrandPacks.CheckedValues()

            Me.isLoadingCombo = False
        Catch ex As Exception
            If Not IsNothing(Me.chkBrandPacks.DropDownDataSource) Then
                If Not IsNothing(Me.chkBrandPacks.CheckedValues) Then
                    Me.isLoadingCombo = True
                    Me.chkBrandPacks.CheckedValues = Me.listCheckedBrandPacks
                End If
            End If
            Me.isLoadingCombo = False
            Cursor = Cursors.Default
            Me.ShowMessageInfo(ex.Message) : Me.LogMyEvent(ex.Message, "_chkBrandPacks_CheckedValuesChanged")
        End Try
    End Sub
    Private Sub chkTargetPOPerPackSize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTargetPOPerPackSize.CheckedChanged
        If Me.isLoadingCombo Then : Return : End If
        If Me.Isloadingrow Then : Return : End If
        Try
            If Me.chkTargetPOPerPackSize.Checked Then
                'check chk target by brandpack
                If Me.chkTargetPOPerBrand.Checked Then
                    Me.ShowMessageInfo("can not set target po per brandpack " & vbCrLf & _
                    "while target po has been set per brand previously")
                    If Me.isLoadingCombo Then : Return : End If
                    If Me.Isloadingrow Then : Return : End If
                    Me.chkTargetPOPerPackSize.Checked = False : Return
                End If
                Dim tblProgBrandPack As New DataTable("DISC_PROGRESSIVE_BRANDPACK")
                If IsNothing(Me.DVProgBrandPack) Then
                    With tblProgBrandPack
                        .Columns.Add("BRANDPACK_ID", Type.GetType("System.String"))
                        .Columns.Add("BRANDPACK_NAME", Type.GetType("System.String"))
                    End With
                    tblProgBrandPack.AcceptChanges()
                ElseIf Me.DVProgBrandPack.Table.Columns.Count <= 0 Then
                    With tblProgBrandPack
                        .Columns.Add("BRANDPACK_ID", Type.GetType("System.String"))
                        .Columns.Add("BRANDPACK_NAME", Type.GetType("System.String"))
                    End With
                    tblProgBrandPack.AcceptChanges()
                Else
                    tblProgBrandPack = Me.DVProgBrandPack.Table
                End If
                tblProgBrandPack.Rows.Clear()
                tblProgBrandPack.AcceptChanges()
                Dim isReadOnly = Me.chkBrandPacks.ReadOnly
                Me.chkBrandPacks.ReadOnly = False
                Me.chkBrandPacks.Focus()
                Me.chkBrandPacks.DroppedDown = True
                For Each Jrow As Janus.Windows.GridEX.GridEXRow In chkBrandPacks.DropDownList().GetCheckedRows()
                    Dim row As DataRow = tblProgBrandPack.NewRow()
                    row.BeginEdit()
                    row("BRANDPACK_ID") = Jrow.Cells("BRANDPACK_ID").Value
                    row("BRANDPACK_NAME") = Jrow.Cells("BRANDPACK_NAME").Value
                    row.EndEdit()
                    tblProgBrandPack.Rows.Add(row)
                Next
                Me.chkBrandPacks.DroppedDown = False
                Me.grdProgDisc.Focus()
                Me.chkBrandPacks.ReadOnly = isReadOnly
                Me.DVProgBrandPack = tblProgBrandPack.DefaultView()
                Me.grdProgDisc.DropDowns(0).SetDataBinding(Me.DVProgBrandPack, "")
                'munculkan brandpackid
                Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible = True
            ElseIf Me.chkTargetPOPerPackSize.Checked = False Then
                'chek availability
                If Me.CheckAvailability() Then
                    Me.isLoadingCombo = True
                    Me.Isloadingrow = True
                    Me.chkTargetPOPerPackSize.Checked = True
                    Me.isLoadingCombo = False
                    Me.Isloadingrow = False
                    Me.Cursor = Cursors.Default
                    Return
                End If
                ''check data apakah sudah ada data discount by packsize
                Dim dv As DataView = CType(Me.grdProgDisc.DataSource, DataView).ToTable().Copy().DefaultView()
                dv.RowFilter = "BRANDPACK_ID IS NOT NULL AND BRANDPACK_ID <> ''"
                If dv.Count > 0 Then
                    For i As Integer = 0 To dv.Count - 1
                        'chek reference
                        If Not IsNothing(dv(i)("HasRef")) And Not IsDBNull(dv(i)("HasRef")) Then
                            If CInt(dv(i)("HasRef")) > 0 Then
                                Me.ShowMessageError(Me.MessageCantDeleteData)
                                Me.isLoadingCombo = False : Me.Cursor = Cursors.Default
                                Exit Sub
                            End If
                        End If
                    Next
                    Me.grdProgDisc.UpdateData()
                End If
                Me.grdProgDisc.RootTable.Columns("BRANDPACK_ID").Visible = False
            End If
            If Me.isLoadingCombo Then : Me.isLoadingCombo = False : End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If Me.isLoadingCombo Then : Me.isLoadingCombo = False : End If
        End Try
    End Sub

    Private Sub chkTargetPOPerBrand_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTargetPOPerBrand.CheckedChanged
        If Me.isLoadingCombo Then : Return : End If
        If Me.Isloadingrow Then : Return : End If
        Try
            If Me.chkTargetPOPerBrand.Checked Then
                If Me.chkTargetPOPerPackSize.Checked Then
                    Me.ShowMessageInfo("can not set target po per brand " & vbCrLf & _
                                 "while target po has been set per brandpack previously")
                    If Me.isLoadingCombo Then : Return : End If
                    If Me.Isloadingrow Then : Return : End If
                    Me.chkTargetPOPerBrand.Checked = False : Return
                End If

                Dim tblProgBrand As New DataTable("DISC_PROGRESSIVE_BRAND")

                If IsNothing(Me.DVProgBrand) Then
                    With tblProgBrand
                        .Columns.Add("BRAND_ID", Type.GetType("System.String"))
                        .Columns.Add("BRAND_NAME", Type.GetType("System.String"))
                    End With
                    tblProgBrand.AcceptChanges()
                ElseIf Me.DVProgBrand.Table.Columns.Count <= 0 Then
                    With tblProgBrand
                        .Columns.Add("BRAND_ID", Type.GetType("System.String"))
                        .Columns.Add("BRAND_NAME", Type.GetType("System.String"))
                    End With
                    tblProgBrand.AcceptChanges()
                Else
                    tblProgBrand = Me.DVProgBrand.Table
                End If
                tblProgBrand.Rows.Clear()
                tblProgBrand.AcceptChanges()
                Dim isReadOnly = Me.chkBrand.ReadOnly
                Me.chkBrand.ReadOnly = False
                Me.chkBrand.Focus()
                Me.chkBrand.DroppedDown = True
                For Each Jrow As Janus.Windows.GridEX.GridEXRow In chkBrand.DropDownList().GetCheckedRows()
                    Dim row As DataRow = tblProgBrand.NewRow()
                    row.BeginEdit()
                    row("BRAND_ID") = Jrow.Cells("BRAND_ID").Value
                    row("BRAND_NAME") = Jrow.Cells("BRAND_NAME").Value
                    row.EndEdit()
                    tblProgBrand.Rows.Add(row)
                Next
                Me.chkBrand.DroppedDown = False
                Me.grdProgDisc.Focus()
                Me.chkBrand.ReadOnly = isReadOnly
                Me.DVProgBrand = tblProgBrand.DefaultView()
                Me.grdProgDisc.DropDowns(1).SetDataBinding(Me.DVProgBrand, "")
                Me.grdProgDisc.RootTable.Columns("BRAND_ID").Visible = True
                Me.isLoadingCombo = True
                Me.chkBrandPacks.UncheckAll()
                Me.isLoadingCombo = False
            ElseIf Me.chkTargetPOPerBrand.Checked = False Then
                ''check data apakah sudah ada data discount by packsize
                If Me.CheckAvailability() Then
                    Me.isLoadingCombo = True
                    Me.Isloadingrow = True
                    Me.chkTargetPOPerBrand.Checked = True
                    Me.isLoadingCombo = False
                    Me.Isloadingrow = False
                    Return : End If
                Dim dv As DataView = CType(Me.grdProgDisc.DataSource, DataView).ToTable().Copy().DefaultView()
                dv.RowFilter = "BRAND_ID IS NOT NULL AND BRAND_ID <> ''"
                If dv.Count > 0 Then
                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                        If Not IsDBNull(row.Cells("BRAND_ID").Value) Then
                            'check reference
                            If Not IsNothing(row.Cells("HasRef").Value) And Not IsDBNull(row.Cells("HasRef").Value) Then
                                If CInt(row.Cells("HasRef").Value) > 0 Then
                                    Me.ShowMessageError(Me.MessageCantDeleteData)
                                    If Me.isLoadingCombo Then : Me.isLoadingCombo = False : End If
                                    Return
                                End If
                            End If
                            row.BeginEdit()
                            row.Cells("BRAND_ID").Value = DBNull.Value
                            row.EndEdit()
                        End If
                    Next
                End If
                dv.RowFilter = "BRANDPACK_ID IS NOT NULL AND BRANDPACK_ID <> ''"
                If dv.Count > 0 Then
                    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdProgDisc.GetRows()
                        If Not IsDBNull(row.Cells("BRANDPACK_ID").Value) Then
                            'check reference
                            If Not IsNothing(row.Cells("HasRef").Value) And Not IsDBNull(row.Cells("HasRef").Value) Then
                                If CInt(row.Cells("HasRef").Value) > 0 Then
                                    Me.ShowMessageError(Me.MessageCantDeleteData)
                                    If Me.isLoadingCombo Then : Me.isLoadingCombo = False : End If
                                    Return
                                End If
                            End If
                            row.BeginEdit()
                            row.Cells("BRANDPACK_ID").Value = DBNull.Value
                            row.EndEdit()
                        End If
                    Next
                    Me.grdProgDisc.UpdateData()
                End If
                Me.grdProgDisc.RootTable.Columns("BRAND_ID").Visible = False
            End If
            If Me.isLoadingCombo Then : Me.isLoadingCombo = False : End If

        Catch ex As Exception
            Me.isLoadingCombo = False
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdProgDisc_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdProgDisc.CellUpdated
        If Me.isLoadingCombo Then : Return : End If
        If Me.Isloadingrow Then : Return : End If
        If Me.grdProgDisc.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            'BRAND_ID,BRANDPACK_ID,MoreThanQty,Disc,TypeApp
            If e.Column.Key = "BRAND_ID" Or e.Column.Key = "BRANDPACK_ID" Or e.Column.Key = "MoreThanQty" Or e.Column.Key = "Disc" Or e.Column.Key = "TypeApp" Then
                If Not IsNothing(Me.grdProgDisc.GetValue("HasRef")) And Not IsDBNull(Me.grdProgDisc.GetValue("HasRef")) Then
                    If CInt(Me.grdProgDisc.GetValue("HasRef")) > 0 Then
                        Me.grdProgDisc.CancelCurrentEdit()
                        Me.ShowMessageError(Me.MessageDataCantChanged & vbCrLf & "Because has child-referenced data")
                        If Me.isLoadingCombo Then : Me.isLoadingCombo = False : End If
                        Me.Cursor = Cursors.Default
                        Return
                    End If
                End If
            End If
            Me.grdProgDisc.UpdateData()
        End If
    End Sub

    Private Sub grdProgDisc_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdProgDisc.DeletingRecord
        If Me.isLoadingCombo Then : Return : End If
        If Me.Isloadingrow Then : Return : End If
        If Me.grdProgDisc.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
            If Not IsNothing(Me.grdProgDisc.GetValue("HasRef")) And Not IsDBNull(Me.grdProgDisc.GetValue("HasRef")) Then
                If CInt(Me.grdProgDisc.GetValue("HasRef")) > 0 Then
                    Me.grdProgDisc.CancelCurrentEdit()
                    Me.ShowMessageError(Me.MessageCantDeleteData & vbCrLf & "Because has child-referenced data")
                    If Me.isLoadingCombo Then : Me.isLoadingCombo = False : End If
                    Me.Cursor = Cursors.Default
                    Return
                End If
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub
End Class
