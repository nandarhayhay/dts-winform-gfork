Public Class BrandPackHistory

#Region " Deklarasi "
    Private clsBPHistory As NufarmBussinesRules.Brandpack.BrandPack
    Private GridSelect As Activegrid
    Private WhileSaving As Saving
    Private SD As SelectDropDown
    Private HasLoad As Boolean
    Private WithEvents EP As EditPrice
    Private SetVal As SetValue = SetValue.None
    Friend CMain As Main = Nothing
#End Region

#Region " Enum "
    Private Enum Saving
        Waiting
        Failed
        Succes
    End Enum
    Private Enum Activegrid
        grdBrandPack
        grdPriceHistory
    End Enum
    Private Enum SelectDropDown
        PACK_NAME
        BRAND_NAME
    End Enum
    Private Enum SetValue
        None
        Updating
    End Enum
#End Region

#Region " Sub Procedure "

    Private Sub ReadAcces()
        If Not CMain.IsSystemAdministrator Then
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.BRANDPACK_PRICEHISTORY = True Then
                Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If

            If (NufarmBussinesRules.User.Privilege.ALLOW_INSERT.BRANDPACK_PRICEHISTORY = True) And (NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.BRANDPACK_PRICEHISTORY = True) Then
                Me.btnAddNew.Visible = True
                Me.grpViewMode.Text = "Data View Mode"
                Me.Bar1.Visible = True
                Me.btnSave.Enabled = True
                Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_INSERT.BRANDPACK_PRICEHISTORY = True Then
                Me.btnAddNew.Visible = True
                Me.grpViewMode.Text = "Data View Mode"
                Me.Bar1.Visible = True
                Me.btnSave.Enabled = True
            ElseIf NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.BRANDPACK_PRICEHISTORY = True Then
                Me.btnAddNew.Visible = False
                Me.grpViewMode.Text = "Data View Mode"
                Me.Bar1.Visible = True
                Me.btnSave.Enabled = True
                Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Else
                Me.btnAddNew.Visible = False
                Me.grpViewMode.Text = ""
                Me.Bar1.Visible = False
                Me.btnSave.Enabled = False
                Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            End If
        End If
    End Sub
    Friend Sub InitializeData()
        Me.HasLoad = False
        Me.LoadData()
    End Sub
    Private Sub RefreshData()
        Me.LoadData()
        Me.Refresh()
    End Sub
    'Private Sub LockedColumCreateModify(ByVal GRD As Janus.Windows.GridEX.GridEX)
    '    For Each item As Janus.Windows.GridEX.GridEXColumn In GRD.RootTable.Columns
    '        If (item.DataMember = "CREATE_BY") Or (item.DataMember = "CREATE_DATE") _
    '        Or (item.DataMember = "MODIFY_BY") Or (item.DataMember = "MODIFY_DATE") Then
    '            item.EditType = Janus.Windows.GridEX.EditType.NoEdit
    '        End If
    '    Next
    'End Sub
    Private Sub Bindgrid(ByVal dtView As DataView, ByVal Grd As Janus.Windows.GridEX.GridEX)
        Me.SetVal = SetValue.Updating
        Grd.SetDataBinding(dtView, "")
        Me.SetVal = SetValue.None
    End Sub
    Private Sub LoadData()
        Me.clsBPHistory = New NufarmBussinesRules.Brandpack.BrandPack()
        If Me.HasLoad Then : Me.clsBPHistory.FetchDataSet(False)
        Else : Me.clsBPHistory.FetchDataSet(True)
        End If

        'BINDING GRID BRANDPACK
        Me.Bindgrid(Me.clsBPHistory.GetDataViewBrandPack, Me.grdBrandPack)
        Me.grdPriceHistory.DropDowns("BrandPack").SetDataBinding(Me.clsBPHistory.ViewDropDownPrice(), "")

        Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
        Me.grdBrandPack.DropDowns("PACK").SetDataBinding(Me.clsBPHistory.GetDataViewPack, "")
        Me.grdBrandPack.DropDowns("Brand").SetDataBinding(Me.clsBPHistory.GetDataViewBrand(), "")
        Me.grdBrandPack.RootTable.Columns("BRANDPACK_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdBrandPack.RootTable.Columns("CREATE_BY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdBrandPack.RootTable.Columns("CREATE_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.grdBrandPack.RootTable.Columns("MODIFY_BY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdBrandPack.RootTable.Columns("MODIFY_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.grdBrandPack.RootTable.Columns("BRANDPACK_ID").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
        Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
        Me.grdBrandPack.RootTable.Columns("BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdBrandPack.RootTable.Columns("BRANDPACK_NAME").AutoSize()
        Me.grdBrandPack.RootTable.Columns("BRANDPACK_NAME").EditType = Janus.Windows.GridEX.EditType.NoEdit
        'Me.LockedColumCreateModify(Me.grdBrandPack)

        'BINDING GRID PRICE HISTORY
        Me.Bindgrid(Me.clsBPHistory.GetDataViewPrice, Me.grdPriceHistory)
        Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
        Me.grdPriceHistory.RootTable.Columns("PRICE").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPriceHistory.RootTable.Columns("START_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.grdPriceHistory.RootTable.Columns("CREATE_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.grdPriceHistory.RootTable.Columns("CREATE_BY").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPriceHistory.RootTable.Columns("MODIFY_BY").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.grdPriceHistory.RootTable.Columns("MODIFY_DATE").FilterEditType = Janus.Windows.GridEX.FilterEditType.CalendarCombo
        Me.grdPriceHistory.RootTable.Columns("PRICE_TAG").FilterEditType = Janus.Windows.GridEX.FilterEditType.Combo
        Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
        Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
        Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
        Me.grdPriceHistory.RootTable.Columns("PRICE_TAG").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdPriceHistory.RootTable.Columns("START_DATE").DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()
        'Me.grdPriceHistory.RootTable.Columns("START_DATE").EditType = Janus.Windows.GridEX.EditType.NoEdit
        'Me.grdPriceHistory.RootTable.Columns("BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.MultiColumnCombo
        'Me.LockedColumCreateModify(Me.grdPriceHistory)
        Me.GetStateChecked(Me.btnOriginalRows)
    End Sub
    Private Sub SaveData()
        'Me.clsBPHistory.SaveChanges(Me.clsBPHistory.GetDataSet().GetChanges())
        Me.clsBPHistory.SaveData(Me.clsBPHistory.GetDataSet())
    End Sub
    Private Sub GetStateChecked(ByVal item As DevComponents.DotNetBar.ButtonItem)
        For Each item1 As DevComponents.DotNetBar.ButtonItem In Me.Bar1.Items
            If Not item1.Name = item.Name Then
                item1.Checked = False
            Else
                item = item1
                item.Checked = True
            End If
        Next
    End Sub
#End Region

#Region " Event "

#Region " Event Form "
    Private Sub BrandPackHistory_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.clsBPHistory) Then
                If Not IsNothing(Me.clsBPHistory.GetDataSet()) Then
                    If Me.clsBPHistory.GetDataSet().HasChanges() Then
                        If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                            'Me.ShowProgress()
                            Me.clsBPHistory.SaveData(Me.clsBPHistory.GetDataSet.GetChanges())
                            'Me.CloseProgres()
                            Me.ShowMessageInfo(Me.MessageSavingSucces)
                        End If
                    End If
                End If
            End If
            Me.SetVal = SetValue.Updating
            Me.clsBPHistory.Dispose(True)
            Me.clsBPHistory = Nothing
        Catch ex As Exception
            'Me.CloseProgres()
            'Me.ShowMessageError(ex.Message)
            'Me.LogMyEvent(ex.Message, "BrandPackHistory_FormClosed")
            'Me.Cursor = Cursors.Default
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BrandPackHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            'Me.LoadData()
            Me.GridSelect = Activegrid.grdBrandPack
            Me.FilterEditor1.Visible = False
            Me.grdBrandPack_Enter(Me.grdBrandPack, New System.EventArgs())
            AddHandler btnSave.Click, AddressOf Timer1_Tick
            Me.ReadAcces()
        Catch ex As Exception
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, "BrandPackHistory_Load")
        Finally
            Me.CMain.FormLoading = Main.StatusForm.HasLoaded : Me.CMain.StatProg = Main.StatusProgress.None : Me.HasLoad = True : Me.Cursor = Cursors.Default

        End Try

    End Sub
#End Region

#Region " Bar Item "
    Private Sub Bar1_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar1.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = DirectCast(sender, DevComponents.DotNetBar.BaseItem)
            Select Case Me.GridSelect
                Case Activegrid.grdBrandPack
                    Select Case item.Text
                        Case "Added"
                            'Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "ModifiedAdded")
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "ModifiedAdded"), Me.grdBrandPack)
                            'Me.Bindgrid(Me.clsBrand.GetDataViewRowState("ModifiedAdded"))
                            Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.FilterEditor1.Visible = False
                            Me.grdBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "ModifiedOriginal"
                            'Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "ModifiedOriginal")
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "ModifiedOriginal"), Me.grdBrandPack)
                            Me.grdBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.grdBrandPack.RootTable.Columns("BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                            Me.FilterEditor1.Visible = False
                        Case "Deleted"
                            'Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "Deleted")
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "Deleted"), Me.grdBrandPack)
                            Me.grdBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.FilterEditor1.Visible = False
                            Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "Current"
                            'Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "Current")
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "Current"), Me.grdBrandPack)
                            If Me.btnAddNew.Text = "Cancel Add" Then
                                Me.grdBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                'Me.grpLastID.Visible = True
                            Else
                                Me.grdBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                                'Me.grpLastID.Visible = False
                            End If
                            Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                        Case "Unchaigned"
                            'Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "Unchaigned")
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "Unchaigned"), Me.grdBrandPack)
                            Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.grdBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.FilterEditor1.Visible = False
                        Case "All Data(s)"
                            'Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "Current")
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewBrandPack, "Current"), Me.grdBrandPack)
                            Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.grdBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdBrandPack.RootTable.Columns("BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit

                            Me.FilterEditor1.Visible = False
                            'Case "All Data"
                            '    Me.Bindgrid(Me.clsBPHistory.getAllDataViewBrandPack(), Me.grdBrandPack)
                            '    Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            '    Me.grdBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            '    Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            '    Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            '    Me.FilterEditor1.Visible = False
                    End Select
                Case Activegrid.grdPriceHistory
                    Select Case item.Text
                        Case "Added"
                            'Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewPrice, "ModifiedAdded")
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewPrice, "ModifiedAdded"), Me.grdPriceHistory)
                            'Me.Bindgrid(Me.clsBrand.GetDataViewRowState("ModifiedAdded"))
                            Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.FilterEditor1.Visible = False
                            Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "ModifiedOriginal"
                            'Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewPrice, "ModifiedOriginal")
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewPrice, "ModifiedOriginal"), Me.grdPriceHistory)
                            Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.grdPriceHistory.RootTable.Columns("PRICE_TAG").EditType = Janus.Windows.GridEX.EditType.NoEdit
                            Me.FilterEditor1.Visible = False
                        Case "Deleted"
                            'Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewPrice, "Deleted")
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewPrice, "Deleted"), Me.grdPriceHistory)
                            Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            'Me.grpLastID.Visible = False
                            Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.FilterEditor1.Visible = False
                            Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case "Current"
                            'Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewPrice, "Current")
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewPrice, "Current"), Me.grdPriceHistory)
                            If Me.btnAddNew.Text = "Cancel Add" Then
                                Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                'Me.grpLastID.Visible = True
                            Else
                                Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                                'Me.grpLastID.Visible = False
                            End If
                            Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                        Case "Unchaigned"
                            'Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewPrice, "Unchaigned")
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewRowState(Me.clsBPHistory.GetDataViewPrice, "Unchaigned"), Me.grdPriceHistory)
                            Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.FilterEditor1.Visible = False
                        Case "All Data(s)"
                            'Me.clsBPHistory.GetDataViewPrice().RowFilter = ""
                            Me.clsBPHistory.GetDataViewPrice().RowStateFilter = Data.DataViewRowState.CurrentRows
                            Me.Bindgrid(Me.clsBPHistory.GetDataViewPrice, Me.grdPriceHistory)
                            Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            Me.grdPriceHistory.RootTable.Columns("PRICE_TAG").EditType = Janus.Windows.GridEX.EditType.NoEdit
                            'If Not NufarmBussinesRules.User.UserLogin.IsAdmin Then

                            'Else
                            '    Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                            '    Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                            '    Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            '    Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                            '    Me.grdPriceHistory.RootTable.Columns("PRICE_TAG").EditType = Janus.Windows.GridEX.EditType.NoEdit
                            'End If

                            Me.FilterEditor1.Visible = False
                            'Case "All Data"
                            '    Me.clsBPHistory.GetDataViewPrice().RowFilter = ""
                            '    Me.clsBPHistory.GetDataViewPrice().RowStateFilter = Data.DataViewRowState.OriginalRows
                            '    Me.Bindgrid(Me.clsBPHistory.GetDataViewPrice, Me.grdPriceHistory)
                            '    Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                            '    Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                            '    Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                            '    Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                            '    Me.grdPriceHistory.RootTable.Columns("PRICE_TAG").EditType = Janus.Windows.GridEX.EditType.NoEdit
                            '    Me.FilterEditor1.Visible = False
                    End Select
            End Select
            Me.GetStateChecked(CType(item, DevComponents.DotNetBar.ButtonItem))
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_BrandPack_Bar1_ItemClick(")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Bar2_ItemClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bar2.ItemClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim item As DevComponents.DotNetBar.BaseItem = DirectCast(sender, DevComponents.DotNetBar.BaseItem)
            Select Case item.Name
                Case "btnSettingGrid"
                    Select Case Me.GridSelect
                        Case Activegrid.grdBrandPack
                            Dim SetGrid As New SettingGrid()
                            SetGrid.Grid = Me.grdBrandPack
                            SetGrid.GridExPrintDock = Me.GridEXPrintDocument1
                            SetGrid.ShowDialog(Me)
                        Case Activegrid.grdPriceHistory
                            Dim SetGrid As New SettingGrid()
                            SetGrid.Grid = Me.grdPriceHistory
                            SetGrid.GridExPrintDock = Me.GridEXPrintDocument2
                            SetGrid.ShowDialog(Me)
                    End Select
                Case "btnCustomFilter"
                    Select Case Me.GridSelect
                        Case Activegrid.grdBrandPack
                            Me.FilterEditor1.SourceControl = Me.grdBrandPack
                            Me.grdBrandPack.RemoveFilters()
                            Me.FilterEditor1.Visible = True
                            Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.None
                        Case Activegrid.grdPriceHistory
                            Me.FilterEditor1.SourceControl = Me.grdPriceHistory
                            Me.grdPriceHistory.RemoveFilters()
                            Me.FilterEditor1.Visible = True
                            Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.None
                    End Select
                Case "btnFilterEqual"
                    Select Case Me.GridSelect
                        Case Activegrid.grdBrandPack
                            Me.FilterEditor1.Visible = False
                            Me.grdBrandPack.RemoveFilters()
                            Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                        Case Activegrid.grdPriceHistory
                            Me.FilterEditor1.Visible = False
                            Me.grdPriceHistory.RemoveFilters()
                            Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                    End Select
                Case "btnRemoveFilter"
                    Select Case Me.GridSelect
                        Case Activegrid.grdBrandPack
                            Me.grdBrandPack.RemoveFilters()
                            Me.grdBrandPack.Refresh()
                        Case Activegrid.grdPriceHistory
                            Me.grdPriceHistory.RemoveFilters()
                            Me.grdPriceHistory.Refresh()
                    End Select
                Case "btnRenameColumn"
                    Select Case Me.GridSelect
                        Case Activegrid.grdBrandPack
                            Dim MC As New ManipulateColumn()
                            MC.ShowInTaskbar = False
                            MC.grid = Me.grdBrandPack
                            MC.FillcomboColumn()
                            MC.ManipulateColumnName = "Rename"
                            MC.TopMost = True
                            MC.Show(Me.Bar4, True)
                        Case Activegrid.grdPriceHistory
                            Dim MC As New ManipulateColumn()
                            MC.ShowInTaskbar = False
                            MC.grid = Me.grdPriceHistory
                            MC.FillcomboColumn()
                            MC.ManipulateColumnName = "Rename"
                            MC.TopMost = True
                            MC.Show(Me.Bar3, True)
                    End Select
                Case "btnAddNew"
                    Select Case item.Text
                        Case "Add New"
                            Select Case Me.GridSelect
                                Case Activegrid.grdBrandPack
                                    Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter = Data.DataViewRowState.CurrentRows
                                    Me.Bindgrid(Me.clsBPHistory.GetDataViewBrandPack(), Me.grdBrandPack)
                                    Me.grdBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                    Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.grdBrandPack.RootTable.Columns("BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    Me.grdBrandPack.RootTable.Columns("BRANDPACK_NAME").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    'Me.grdBrandPack.RootTable.Columns("BRANDPACK_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                                    Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.grdBrandPack.MoveToNewRecord()
                                    'Me.LockedColumCreateModify(Me.grdBrandPack)
                                    item.Text = "Cancel"
                                Case Activegrid.grdPriceHistory
                                    Me.clsBPHistory.GetDataViewPrice().RowStateFilter = Data.DataViewRowState.CurrentRows
                                    Me.Bindgrid(Me.clsBPHistory.GetDataViewPrice(), Me.grdPriceHistory)
                                    'Me.lblLastID.Text = Me.clsBrand.ViewBrand(Me.clsBrand.ViewBrand.Count - 1)("BRAND_ID").ToString()
                                    'Me.grpLastID.Visible = True
                                    Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.None
                                    Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.grdPriceHistory.RootTable.Columns("PRICE_TAG").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.grdPriceHistory.MoveToNewRecord()
                                    'Me.LockedColumCreateModify(Me.grdPriceHistory)
                                    item.Text = "Cancel"
                            End Select
                            Me.GetStateChecked(Me.btnCurrent)
                        Case "Cancel"
                            Select Case Me.GridSelect
                                Case Activegrid.grdBrandPack
                                    If Not IsNothing(Me.clsBPHistory.GetDataSet()) Then
                                        If Me.clsBPHistory.GetDataSet.HasChanges() Then
                                            If Me.ShowConfirmedMessage("Data has changed !" & vbCrLf & "If you continue canceling " & vbCrLf & _
                                            "All changes will be discarded" & vbCrLf & _
                                            "Continue canceling the changes ?") = Windows.Forms.DialogResult.No Then : Return : End If
                                        End If
                                    End If
                                    If Me.clsBPHistory.GetDataSet().HasChanges() Then
                                        Me.clsBPHistory.GetDataSet().RejectChanges()
                                    End If
                                    Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter = Data.DataViewRowState.OriginalRows
                                    Me.Bindgrid(Me.clsBPHistory.GetDataViewBrandPack(), Me.grdBrandPack)
                                    item.Text = "Add New"
                                    'Me.grpLastID.Visible = False
                                    Me.grdBrandPack.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                                    Me.FilterEditor1.Visible = False
                                    Me.grdBrandPack.MoveFirst()
                                    Me.grdBrandPack.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.grdBrandPack.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.grdBrandPack.RootTable.Columns("BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    Me.grdBrandPack.RootTable.Columns("BRANDPACK_NAME").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    'Me.grdBrandPack.RootTable.Columns("BRANDPACK_NAME").FilterEditType = Janus.Windows.GridEX.FilterEditType.NoEdit
                                    'Me.LockedColumCreateModify(Me.grdBrandPack)
                                    item.Text = "Add New"
                                Case Activegrid.grdPriceHistory
                                    If Me.clsBPHistory.GetDataSet().HasChanges() Then
                                        Me.clsBPHistory.GetDataSet().RejectChanges()
                                    End If
                                    Me.clsBPHistory.GetDataViewPrice().RowStateFilter = Data.DataViewRowState.OriginalRows
                                    Me.Bindgrid(Me.clsBPHistory.GetDataViewPrice(), Me.grdPriceHistory)
                                    'Me.grpLastID.Visible = False
                                    Me.grdPriceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
                                    Me.FilterEditor1.Visible = False
                                    Me.grdPriceHistory.MoveFirst()
                                    Me.grdPriceHistory.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                                    Me.grdPriceHistory.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
                                    Me.grdPriceHistory.RootTable.Columns("PRICE_TAG").EditType = Janus.Windows.GridEX.EditType.NoEdit
                                    'Me.LockedColumCreateModify(Me.grdPriceHistory)
                                    item.Text = "Add New"
                            End Select
                            Me.GetStateChecked(Me.btnAllData)
                    End Select
                
                Case "btnPrint"
                    Select Case Me.GridSelect
                        Case Activegrid.grdBrandPack
                            Me.GridEXPrintDocument1.GridEX = Me.grdBrandPack
                            Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
                            'Me.PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                            If Not IsNothing(Me.PageSetupDialog1.PageSettings) Then
                                Me.PrintPreviewDialog1.Document.DefaultPageSettings = Me.PageSetupDialog1.PageSettings
                            End If
                            If Me.PrintPreviewDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Me.PrintPreviewDialog1.Document.Print()
                            End If
                        Case Activegrid.grdPriceHistory
                            Me.GridEXPrintDocument2.GridEX = Me.grdPriceHistory
                            Me.PrintPreviewDialog2.Document = Me.GridEXPrintDocument2
                            'Me.PrintPreviewDialog1.SetBounds(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height)
                            If Not IsNothing(Me.PageSetupDialog2.PageSettings) Then
                                Me.PrintPreviewDialog2.Document.DefaultPageSettings = Me.PageSetupDialog2.PageSettings
                            End If
                            If Me.PrintPreviewDialog2.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Me.PrintPreviewDialog2.Document.Print()
                            End If
                    End Select
                Case "btnPageSetting"
                    Select Case Me.GridSelect
                        Case Activegrid.grdBrandPack
                            Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
                            Me.PageSetupDialog1.ShowDialog(Me)
                        Case Activegrid.grdPriceHistory
                            Me.PageSetupDialog2.Document = Me.GridEXPrintDocument2
                            Me.PageSetupDialog2.ShowDialog(Me)
                    End Select
                Case "btnFieldChooser"
                    Select Case Me.GridSelect
                        Case Activegrid.grdBrandPack
                            Me.grdBrandPack.ShowFieldChooser(Me)
                        Case Activegrid.grdPriceHistory
                            Me.grdPriceHistory.ShowFieldChooser(Me)
                    End Select
                    'next result
                Case "btnExport"
                    Select Case Me.GridSelect
                        Case Activegrid.grdBrandPack
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.grdBrandPack
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Case Activegrid.grdPriceHistory
                            Me.SaveFileDialog1.OverwritePrompt = True
                            Me.SaveFileDialog1.DefaultExt = ".xls"
                            Me.SaveFileDialog1.Filter = "All Files|*.*"
                            Me.SaveFileDialog1.InitialDirectory = "C:\"
                            If Me.SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Dim FS As New System.IO.FileStream(Me.SaveFileDialog1.FileName, IO.FileMode.Create)
                                Me.GridEXExporter1.GridEX = Me.grdPriceHistory
                                Me.GridEXExporter1.Export(FS)
                                FS.Close()
                                MessageBox.Show("Data Exported to " & Me.SaveFileDialog1.FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                    End Select
                    'next result
                Case "btnRefresh"
                    If Not IsNothing(Me.clsBPHistory.GetDataSet()) Then
                        If Me.clsBPHistory.GetDataSet().HasChanges() Then
                            If Me.ShowConfirmedMessage(Me.MessageRefreshData) = Windows.Forms.DialogResult.Yes Then
                                Me.clsBPHistory.GetDataSet().RejectChanges()
                                Me.RefreshData()
                                'Me.btnAddNew.Text = "Cancel Add"
                                CType(Me.Bar2.Items("btnAddNew"), DevComponents.DotNetBar.ButtonItem).Text = "Cancel Add"
                                Application.DoEvents()
                                Me.Bar2.Update()
                                Return
                            Else
                                Return
                            End If
                        Else
                            Me.RefreshData()
                        End If
                    Else
                        Me.RefreshData()
                    End If
                    If Me.txtFilterDropDownBrandPack.Text <> "" Then
                        Me.btnAplyBrandPack_Click(Me.btnAplyBrandPack, New EventArgs())
                    End If
                    If Me.txtFilterDropDownPH.Text <> "" Then
                        Me.btnApplyDropDownPH_Click(Me.btnApplyDropDownPH, New EventArgs())
                    End If
            End Select
        Catch ex As Exception
            Me.WhileSaving = Saving.Failed
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Bar2_ItemClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region " Buttton "
    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Try
            If Not IsNothing(Me.clsBPHistory) Then
                If Not IsNothing(Me.clsBPHistory.GetDataSet()) Then
                    If Me.clsBPHistory.GetDataSet().HasChanges() Then
                        Me.clsBPHistory.GetDataSet.RejectChanges()
                    End If
                End If
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.clsBPHistory.GetDataSet.HasChanges() Then
                If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                    Me.WhileSaving = Saving.Waiting
                    Me.ShowProgress()
                    Me.SaveData()
                    Me.WhileSaving = Saving.Succes
                End If
                Me.btnAddNew.Text = "Add New"
            End If
        Catch ex As Exception
            Me.WhileSaving = Saving.Failed
            If Me.clsBPHistory.GetDataSet.HasChanges() Then
                Me.clsBPHistory.GetDataSet.RejectChanges()
            End If
            Me.ShowMessageError(ex.Message)
            Me.btnAddNew.Text = "Add New"
            Me.LogMyEvent(ex.Message, "btnSaveChanges_Click")
        Finally
            Me.Cursor = Cursors.Default
            'Me.GridEX1.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False
        End Try
    End Sub
    Private Sub btnAplyBrandPack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyBrandPack.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbBrandNameBrandPack.Checked = True Then
                'Me.clsBPHistory.GetDataViewBrand().RowFilter = "BRAND_NAME LIKE '%" + Me.txtFilterDropDownBrandPack.Text + "%'"
                Me.clsBPHistory.GetDataBrand(Me.txtFilterDropDownBrandPack.Text)
                Me.SetVal = SetValue.Updating
                Me.grdBrandPack.DropDowns("Brand").SetDataBinding(Me.clsBPHistory.GetDataViewBrand(), "")
            ElseIf Me.rdbPackNameBrandpack.Checked = True Then
                'Me.clsBPHistory.GetDataViewPack().RowFilter = "PACK_NAME LIKE '%" + Me.txtFilterDropDownBrandPack.Text + "%'"
                Me.clsBPHistory.GetDataPack(Me.txtFilterDropDownBrandPack.Text)
                Me.SetVal = SetValue.Updating
                Me.grdBrandPack.DropDowns("Pack").SetDataBinding(Me.clsBPHistory.GetDataViewPack(), "")
            Else
                Me.ShowMessageInfo("Please Define The Drop Down filter")
                '        Return
            End If
            'Me.grdBrandPack.Refetch()
            Me.SetVal = SetValue.None
            'Me.grdBrandPack.Refresh()
        Catch ex As Exception
            Me.SetVal = SetValue.None
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnApplyDropDownPH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyDropDownPH.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.rdbBrandNamePH.Checked = True Then
                'Me.clsBPHistory.ViewDropDownPrice().RowFilter = "BRAND_NAME LIKE '%" + Me.txtFilterDropDownPH.Text + "%'"
                Me.clsBPHistory.GetDataBrandPackDropDown(Me.txtFilterDropDownPH.Text, NufarmBussinesRules.Brandpack.BrandPack.CategorySearch.Brand)
                Me.SetVal = SetValue.Updating
                Me.grdPriceHistory.DropDowns("BrandPack").SetDataBinding(Me.clsBPHistory.ViewDropDownPrice(), "")
            ElseIf Me.rdbPackNamePH.Checked = True Then
                'Me.clsBPHistory.ViewDropDownPrice().RowFilter = "PACK_NAME LIKE '%" + Me.txtFilterDropDownPH.Text + "%'"
                Me.clsBPHistory.GetDataBrandPackDropDown(Me.txtFilterDropDownPH.Text, NufarmBussinesRules.Brandpack.BrandPack.CategorySearch.Pack)
                Me.SetVal = SetValue.Updating
                Me.grdPriceHistory.DropDowns("BrandPack").SetDataBinding(Me.clsBPHistory.ViewDropDownPrice(), "")
            Else
                Me.ShowMessageInfo("Please Define The Drop Down filter")
                '        Return
            End If
            Me.grdPriceHistory.Refetch()
        Catch ex As Exception
        Finally
            Me.SetVal = SetValue.Updating : Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#Region " Progress Saving "
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Application.DoEvents()
        If Me.WhileSaving = Saving.Succes Then
            For i As Integer = 0 To Me.ResultRandom
                For index As Integer = 1 To 30
                    MyBase.ST.Refresh()
                    MyBase.ST.Label1.Refresh()
                    MyBase.ST.PictureBox1.Refresh()
                Next
            Next
            Me.RefreshData()
            Me.CloseProgres(True)
        ElseIf Me.WhileSaving = Saving.Failed Then
            Me.CloseProgres(False)
        End If
    End Sub
#End Region

#Region " Grid Ex "

#Region " GrdBrandpack "

    Private Sub grdBrandPack_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdBrandPack.CellUpdated
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.HasLoad Then : Return : End If
            If Me.SetVal = SetValue.Updating Then : Return : End If
            Me.GridSelect = Activegrid.grdBrandPack
            For Each item As Janus.Windows.GridEX.GridEXColumn In Me.grdBrandPack.RootTable.Columns
                If (e.Column.Key = "BRANDPACK_ID") Or (e.Column.Key = "BRANPACK_NAME") Or ( _
                    e.Column.Key = "CREATE_DATE") Or (e.Column.Key = "CREATE_BY") Or ( _
                    e.Column.Key = "MODIFY_BY") Or (e.Column.Key = "MODIFY_DATE") Or (e.Column.Key = "IsActive") Or (e.Column.Key = "IsObsolete") Then
                    Return
                End If
            Next
            If Me.grdBrandPack.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                If (Me.grdBrandPack.GetValue("PACK_ID") Is DBNull.Value) Or _
                      (Me.grdBrandPack.GetValue("PACK_ID") Is Nothing) Or (Me.grdBrandPack.GetValue("BRAND_ID") Is DBNull.Value) Or _
                     (Me.grdBrandPack.GetValue("BRAND_ID") Is Nothing) Then
                    Me.SetVal = SetValue.Updating
                    Me.grdBrandPack.SetValue(Me.grdBrandPack.RootTable.Columns("BRANDPACK_NAME"), Nothing)
                    Me.grdBrandPack.SetValue("UNIT", DBNull.Value)
                    Me.grdBrandPack.SetValue("DEVIDED_QUANTITY", DBNull.Value)
                    Me.grdBrandPack.SetValue("CREATE_DATE", DBNull.Value)
                    Me.grdBrandPack.SetValue("CREATE_BY", DBNull.Value)
                    Me.SetVal = SetValue.None
                    Return
                End If
            End If
            If Me.grdBrandPack.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                If (e.Column.Key = "BRAND_ID") Or (e.Column.Key = "PACK_ID") Then
                    If Me.clsBPHistory.HasReferenceData(Me.grdBrandPack.GetValue("BRANDPACK_ID").ToString()) Then
                        ''CHEK REFERENCE DATA BRANDPACK
                        Me.ShowMessageInfo(Me.MessageDataCantChanged)
                        Me.grdBrandPack.CancelCurrentEdit()
                        Return
                    End If
                    If (Me.grdBrandPack.GetValue(e.Column) Is DBNull.Value) Or (Me.grdBrandPack.GetValue(e.Column) Is Nothing) Then
                        Me.ShowMessageInfo("Please define Pack_ID/Brand_ID") : Me.grdBrandPack.CancelCurrentEdit() : Return
                    End If
                End If
            End If
            Dim BrandID As String = Me.grdBrandPack.GetValue("BRAND_ID").ToString()
            Dim PackId As String = Me.grdBrandPack.GetValue("PACK_ID").ToString()
            Dim IndexBrand As Integer = -1, IndexPack As Integer = -1

            Dim BrandPackID As String = BrandID + "" + PackId
            Dim PackName As String = "", BrandName As String = ""
            IndexBrand = Me.clsBPHistory.GetDataViewBrand.Find(BrandID)
            If IndexBrand <> -1 Then
                BrandName = Me.clsBPHistory.GetDataViewBrand(IndexBrand)("BRAND_NAME").ToString()
            Else
                Me.ShowMessageInfo("Brand can not be found" & vbCrLf & "Please refresh data") : Me.grdBrandPack.CancelCurrentEdit() : Return
            End If
            IndexPack = Me.clsBPHistory.GetDataViewPack.Find(PackId)
            If IndexPack <> -1 Then
                PackName = Me.clsBPHistory.GetDataViewPack(IndexPack)("PACK_NAME").ToString()
            Else
                Me.ShowMessageInfo("Pack can not be found" & vbCrLf & "Please refresh data") : Me.grdBrandPack.CancelCurrentEdit() : Return
            End If
            If Me.clsBPHistory.GetDataViewPack(IndexPack)("DEVIDE_FACTOR") Is DBNull.Value Or Me.clsBPHistory.GetDataViewPack(IndexPack)("DEVIDE_FACTOR") Is Nothing Then
                Me.ShowMessageInfo("Define Factor for Pack Name is null") : Me.grdBrandPack.CancelCurrentEdit() : Return
            ElseIf CInt(Me.clsBPHistory.GetDataViewPack(IndexPack)("DEVIDE_FACTOR")) <= 0 Then
                Me.ShowMessageInfo("Define Factor for Pack Name  can not be 0 or <= 0") : Me.grdBrandPack.CancelCurrentEdit() : Return
            End If
            If String.IsNullOrEmpty(Me.clsBPHistory.GetDataViewPack(IndexPack)("UNIT")) Or Me.clsBPHistory.GetDataViewPack(IndexPack)("UNIT") Is Nothing Then
                Me.ShowMessageInfo("Unit for Pack Name is null") : Me.grdBrandPack.CancelCurrentEdit() : Return
            End If
            If Me.clsBPHistory.GetDataViewPack(IndexPack)("QUANTITY") Is DBNull.Value Or Me.clsBPHistory.GetDataViewPack(IndexPack)("QUANTITY") Is Nothing Then
                Me.ShowMessageInfo("Quantity for Pack Name is null") : Me.grdBrandPack.CancelCurrentEdit() : Return
            End If
            Dim BrandPackName As String = BrandName + " @ " + PackName
            Me.SetVal = SetValue.Updating
            Me.grdBrandPack.SetValue("BRANDPACK_ID", BrandPackID)
            Me.grdBrandPack.SetValue("BRANDPACK_NAME", BrandPackName)
            If Me.grdBrandPack.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                Me.SetVal = SetValue.None : Return
            End If
            If e.Column.DataMember = "DEVIDED_QUANTITY" Or e.Column.DataMember = "UNIT" Then
                If Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter = Data.DataViewRowState.ModifiedOriginal Or Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter = Data.DataViewRowState.OriginalRows Then
                    Me.grdBrandPack.SetValue(Me.grdBrandPack.RootTable.Columns("MODIFY_BY"), NufarmBussinesRules.User.UserLogin.UserName)
                    Me.grdBrandPack.SetValue(Me.grdBrandPack.RootTable.Columns("MODIFY_DATE"), NufarmBussinesRules.SharedClass.ServerDate())
                End If
                Me.grdBrandPack.UpdateData()
                Return
            End If
            Dim DF As Integer = CInt(Me.clsBPHistory.GetDataViewPack(IndexPack)("DEVIDE_FACTOR"))
            Dim UNIT As String = Me.clsBPHistory.GetDataViewPack(IndexPack)("UNIT").ToString()
            Dim QUANTITY As Integer = CInt(Me.clsBPHistory.GetDataViewPack(IndexPack)("QUANTITY"))
            Dim DQ As Decimal = Convert.ToDecimal(QUANTITY / DF)
            Me.grdBrandPack.SetValue("DEVIDED_QUANTITY", DQ)
            Me.grdBrandPack.SetValue("UNIT", UNIT)
            If Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter = Data.DataViewRowState.ModifiedOriginal Or Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter = Data.DataViewRowState.OriginalRows Then
                Me.grdBrandPack.SetValue(Me.grdBrandPack.RootTable.Columns("MODIFY_BY"), NufarmBussinesRules.User.UserLogin.UserName)
                Me.grdBrandPack.SetValue(Me.grdBrandPack.RootTable.Columns("MODIFY_DATE"), NufarmBussinesRules.SharedClass.ServerDate())
            End If
            Me.grdBrandPack.UpdateData() : Me.SetVal = SetValue.None
        Catch ex As Exception
            Me.grdBrandPack.CancelCurrentEdit() : Me.SetVal = SetValue.None : MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    
    Private Sub grdBrandPack_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdBrandPack.Enter
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.GridSelect = Activegrid.grdBrandPack
            Me.grpBrandPack.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.ExplorerBarBackground
            Me.grpPH.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.Default
            Me.grpBrandPack.Text = "Active"
            Me.grpPH.Text = ""
            Dim B As Boolean = Me.FilterEditor1.Visible
            If B Then
                Me.FilterEditor1.SourceControl = Me.grdBrandPack
            End If

            If Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter = (Data.DataViewRowState.Added Or Data.DataViewRowState.ModifiedCurrent) Then
                Me.GetStateChecked(Me.btnModifiedAdded)
            End If
            If Me.grdBrandPack.DataSource Is Me.clsBPHistory.getAllDataViewBrandPack() Then
                Me.GetStateChecked(Me.btnAllData)
            Else
                Select Case Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter
                    Case Data.DataViewRowState.Added
                        Me.GetStateChecked(Me.btnModifiedAdded)
                    Case Data.DataViewRowState.CurrentRows
                        Me.GetStateChecked(Me.btnCurrent)
                    Case Data.DataViewRowState.Deleted
                        Me.GetStateChecked(Me.btnDelete)
                    Case Data.DataViewRowState.ModifiedCurrent
                        Me.GetStateChecked(Me.btnModifiedAdded)
                    Case Data.DataViewRowState.ModifiedOriginal
                        Me.GetStateChecked(Me.btnModifiedOriginal)
                    Case Data.DataViewRowState.OriginalRows
                        Me.GetStateChecked(Me.btnOriginalRows)
                    Case Data.DataViewRowState.Unchanged
                        Me.GetStateChecked(Me.btnUnchaigned)
                End Select
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub grdBrandPack_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdBrandPack.AddingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SetVal = SetValue.Updating Then : Return : End If
            If (Me.grdBrandPack.GetValue("BRAND_ID") Is DBNull.Value) Or (Me.grdBrandPack.GetValue("BRAND_ID").Equals("")) Then
                Me.ShowMessageInfo("BRAND_ID Must not be NULL .!")
                Me.grdBrandPack.CancelCurrentEdit()
                'Me.grdBrandPack.Refetch()
                Me.grdBrandPack.MoveToNewRecord()
                Return
            End If
            If (Me.grdBrandPack.GetValue("PACK_ID") Is DBNull.Value) Or (Me.grdBrandPack.GetValue("PACK_ID").Equals("")) Then
                Me.ShowMessageInfo("PACK_ID Must not be NULL .!")
                Me.grdBrandPack.CancelCurrentEdit()
                'Me.grdBrandPack.Refetch()
                Me.grdBrandPack.MoveToNewRecord()
                Return
            End If
            If (Me.grdBrandPack.GetValue("BRANDPACK_NAME") Is DBNull.Value) Or (Me.grdBrandPack.GetValue("BRANDPACK_NAME").Equals("")) Then
                Me.ShowMessageInfo("BRANDPACK_NAME Must not be NULL .!")
                Me.grdBrandPack.CancelCurrentEdit()
                'Me.grdBrandPack.Refetch()
                Me.grdBrandPack.MoveToNewRecord()
                Return
            End If
            If (Me.grdBrandPack.GetValue("BRANDPACK_ID") Is DBNull.Value) Or (Me.grdBrandPack.GetValue("BRANDPACK_ID").Equals("")) Then
                Me.ShowMessageInfo("BRANDPACK_ID Must not be NULL .!")
                Me.grdBrandPack.CancelCurrentEdit()
                'Me.grdBrandPack.Refetch()
                Me.grdBrandPack.MoveToNewRecord()
                Return
            End If
            If Me.clsBPHistory.GetDataViewBrandPack.Find(Me.grdBrandPack.GetValue("BRANDPACK_ID")) <> -1 Then
                Me.ShowMessageInfo("BRANDPACK_ID has existed in DATAVIEW .!" & vbCrLf & "ID must be Unique")
                Me.grdBrandPack.CancelCurrentEdit()
                'Me.grdBrandPack.Refetch()
                Me.grdBrandPack.MoveToNewRecord()
                Return
            End If
            Me.SetVal = SetValue.Updating
            Me.grdBrandPack.SetValue(Me.grdBrandPack.RootTable.Columns("CREATE_DATE"), NufarmBussinesRules.SharedClass.ServerDate())
            Me.grdBrandPack.SetValue(Me.grdBrandPack.RootTable.Columns("CREATE_BY"), NufarmBussinesRules.User.UserLogin.UserName)
            'me.grdBrandPack.SetValue(me.grdBrandPack.SetValue("MULTIPLY_FACTOR",
            Dim DF As Integer = CInt(Me.grdBrandPack.DropDowns("PACK").GetValue("DEVIDE_FACTOR"))
            Dim UNIT As String = Me.grdBrandPack.DropDowns("PACK").GetValue("UNIT").ToString()
            Dim QUANTITY As Integer = CInt(Me.grdBrandPack.DropDowns("PACK").GetValue("QUANTITY"))
            Dim DQ As Decimal = CDec(QUANTITY / DF)
            Me.grdBrandPack.SetValue("DEVIDED_QUANTITY", DQ)
            Me.grdBrandPack.SetValue("UNIT", UNIT)
            Me.SetVal = SetValue.None
        Catch ex As Exception
            Me.SetVal = SetValue.None
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub grdBrandPack_RecordsDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBrandPack.RecordsDeleted
        Me.grdBrandPack.UpdateData()
        Me.GridSelect = Activegrid.grdBrandPack
    End Sub
    Private Sub grdBrandPack_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdBrandPack.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not (e.Row.RowType = Janus.Windows.GridEX.RowType.Record) Then
                Return
            End If
            Me.GridSelect = Activegrid.grdBrandPack
            If Not CMain.IsSystemAdministrator Then
                If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.BRANDPACK_PRICEHISTORY = True Then
                    If Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter = Data.DataViewRowState.OriginalRows _
                                Or Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter = Data.DataViewRowState.CurrentRows _
                                Or Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter = Data.DataViewRowState.Unchanged Then
                        'check keberadaan data anak di server
                        'jika ada return dan refect
                        'Me.MessageCantDeleteData
                        If (Me.clsBPHistory.HasReferenceData(Me.grdBrandPack.GetValue("BRANDPACK_ID").ToString())) > 0 Then
                            Me.ShowMessageInfo(Me.MessageCantDeleteData)
                            e.Cancel = True
                            'Me.grdBrandPack.Refetch()
                            'Me.grdBrandPack.SelectCurrentCellText()
                            Return
                        End If
                        If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                            e.Cancel = True
                            'Me.grdBrandPack.Refetch()
                            'Me.grdBrandPack.SelectCurrentCellText()
                        End If
                    End If
                Else
                    e.Cancel = True
                End If
            Else
                If (Me.clsBPHistory.HasReferenceData(Me.grdBrandPack.GetValue("BRANDPACK_ID").ToString())) > 0 Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    e.Cancel = True
                    'Me.grdBrandPack.Refetch()
                    'Me.grdBrandPack.SelectCurrentCellText()
                    Return
                End If
                If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True
                    'Me.grdBrandPack.Refetch()
                    'Me.grdBrandPack.SelectCurrentCellText()
                End If
            End If
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdBrandPack_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBrandPack.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.HasLoad = False Then
                Return
            End If
            If Me.SetVal = SetValue.Updating Then : Return : End If
            If Me.grdBrandPack.DataSource Is Nothing Then : Return : End If
            Me.GridSelect = Activegrid.grdBrandPack
            If Me.clsBPHistory.GetDataViewBrandPack().RowStateFilter = Data.DataViewRowState.CurrentRows Then
                Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                'If Me.grdBrandPack.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then

                '    Return
                'Else
                '    Me.grdBrandPack.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                'End If
            End If
            Me.grdBrandPack.RootTable.Columns("BRANDPACK_NAME").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdBrandPack.RootTable.Columns("BRANDPACK_ID").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#Region " Grd Price Histrory "
    Private Sub grdPriceHistory_AddingRecord(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles grdPriceHistory.AddingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.grdPriceHistory.GetValue("PRICE") Is Nothing Then
                Me.ShowMessageInfo("PRICE Must not be NULL .!")
                Me.grdPriceHistory.CancelCurrentEdit()
                'Me.grdPriceHistory.Refetch()
                Me.grdPriceHistory.MoveToNewRecord()
                Return
            End If
            If (Me.grdPriceHistory.GetValue("PRICE") Is DBNull.Value) Or (Me.grdPriceHistory.GetValue("PRICE").Equals("")) Then
                Me.ShowMessageInfo("PRICE Must not be NULL .!")
                Me.grdPriceHistory.CancelCurrentEdit()
                'Me.grdPriceHistory.Refetch()
                Me.grdPriceHistory.MoveToNewRecord()
                Return
            End If
            'Check Value STARTDATE IF IS NULL
            If Me.grdPriceHistory.GetValue("START_DATE") Is Nothing Then
                Me.ShowMessageInfo("START_DATE Must not be NULL .!")
                Me.grdPriceHistory.CancelCurrentEdit()
                'Me.grdPriceHistory.Refetch()
                Me.grdPriceHistory.MoveToNewRecord()
                Return
            End If
            If Me.grdPriceHistory.GetValue("START_DATE") Is DBNull.Value Then
                Me.ShowMessageInfo("START_DATE Must not be NULL .!")
                Me.grdPriceHistory.CancelCurrentEdit()
                'Me.grdPriceHistory.Refetch()
                Me.grdPriceHistory.MoveToNewRecord()
                Return
            End If
            'Chek Startdate if > from now
            If CDate(Me.grdPriceHistory.GetValue("START_DATE")) < NufarmBussinesRules.SharedClass.ServerDate() Then
                Me.ShowMessageInfo("START_DATE Must be greater/equal from now .!")
                Me.grdPriceHistory.CancelCurrentEdit()
                'Me.grdPriceHistory.Refetch()
                Me.grdPriceHistory.MoveToNewRecord()
                Return
            End If
            'CHECK BRANDPACK_ID
            If Me.grdPriceHistory.GetValue("BRANDPACK_ID") Is Nothing Then
                Me.ShowMessageInfo("BRANDPACK_ID Must not be NULL .!")
                Me.grdPriceHistory.CancelCurrentEdit()
                'Me.grdPriceHistory.Refetch()
                Me.grdPriceHistory.MoveToNewRecord()
                Return
            End If
            If (Me.grdPriceHistory.GetValue("BRANDPACK_ID") Is DBNull.Value) Or Me.grdPriceHistory.GetValue("BRANDPACK_ID").Equals("") Then
                Me.ShowMessageInfo("BRANDPACK_ID Must not be NULL .!")
                Me.grdPriceHistory.CancelCurrentEdit()
                'Me.grdPriceHistory.Refetch()
                Me.grdPriceHistory.MoveToNewRecord()
                Return
            End If
            'CHECK PRICE_TAG IN DATASOURCE
            Dim StartDate As String = Convert.ToDateTime(Me.grdPriceHistory.GetValue("START_DATE")).ToShortDateString()
            Dim BrandPack_Id As String = Me.grdPriceHistory.GetValue("BRANDPACK_ID").ToString()
            Dim Price_Tag As String = BrandPack_Id + "|" + StartDate
            If Me.clsBPHistory.GetDataViewPrice().Find(Price_Tag) <> -1 Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
                Me.grdPriceHistory.CancelCurrentEdit()
                'Me.grdPriceHistory.Refetch()
                Me.grdPriceHistory.MoveToNewRecord()
                Return
            End If
            'sesudah benar semua
            'isi column create date / create_by
            Me.SetVal = SetValue.Updating
            Me.grdPriceHistory.SetValue("CREATE_DATE", CDate(NufarmBussinesRules.SharedClass.ServerDate()))
            Me.grdPriceHistory.SetValue("CREATE_BY", NufarmBussinesRules.User.UserLogin.UserName)
            Me.grdPriceHistory.SetValue("PRICE_TAG", Price_Tag)
            'Me.grdPriceHistory.SetValue("PRICE_TAG", Price_Tag)
            Me.SetVal = SetValue.None
        Catch ex As Exception
            Me.SetVal = SetValue.None
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub grdPriceHistory_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPriceHistory.Enter
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.GridSelect = Activegrid.grdPriceHistory
            Me.grpPH.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.ExplorerBarBackground
            Me.grpBrandPack.BackgroundStyle = Janus.Windows.EditControls.BackgroundStyle.Default
            Me.grpPH.Text = "Active"
            Me.grpBrandPack.Text = ""
            Dim B As Boolean = Me.FilterEditor1.Visible
            If B Then
                Me.FilterEditor1.SourceControl = Me.grdPriceHistory
            End If
            If Me.clsBPHistory.GetDataViewPrice().RowStateFilter = (Data.DataViewRowState.Added Or Data.DataViewRowState.ModifiedCurrent) Then
                Me.GetStateChecked(Me.btnModifiedAdded)
            End If
            Select Case Me.clsBPHistory.GetDataViewPrice().RowStateFilter
                Case Data.DataViewRowState.Added
                    Me.GetStateChecked(Me.btnModifiedAdded)
                Case Data.DataViewRowState.CurrentRows
                    Me.GetStateChecked(Me.btnCurrent)
                Case Data.DataViewRowState.Deleted
                    Me.GetStateChecked(Me.btnDelete)
                Case Data.DataViewRowState.ModifiedCurrent
                    Me.GetStateChecked(Me.btnModifiedAdded)
                Case Data.DataViewRowState.ModifiedOriginal
                    Me.GetStateChecked(Me.btnModifiedOriginal)
                Case Data.DataViewRowState.OriginalRows
                    Me.GetStateChecked(Me.btnOriginalRows)
                Case Data.DataViewRowState.Unchanged
                    Me.GetStateChecked(Me.btnUnchaigned)
            End Select
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub grdPriceHistory_InputMaskError(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.InputMaskErrorEventArgs) Handles grdPriceHistory.InputMaskError
        Try
            Me.ShowMessageInfo("Please Suply with Valid Value")
            Me.grdPriceHistory.CancelCurrentEdit()
            Me.grdPriceHistory.Refetch()
            Me.GridSelect = Activegrid.grdPriceHistory
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdPriceHistory_RecordsDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPriceHistory.RecordsDeleted
        Try
            Me.grdPriceHistory.UpdateData()
            Me.GridSelect = Activegrid.grdPriceHistory
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdPriceHistory_DropDown(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdPriceHistory.DropDown
        Try
            If e.Column.DataMember = "START_DATE" Then
                e.Column.DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()
                e.Column.LimitToList = True
            End If
            Me.GridSelect = Activegrid.grdPriceHistory
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdPriceHistory_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdPriceHistory.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not (e.Row.RowType = Janus.Windows.GridEX.RowType.Record) Then
                Return
            End If
            If Me.clsBPHistory.GetDataViewPrice().RowStateFilter = Data.DataViewRowState.OriginalRows _
            Or Me.clsBPHistory.GetDataViewPrice().RowStateFilter = Data.DataViewRowState.CurrentRows _
            Or Me.clsBPHistory.GetDataViewPrice().RowStateFilter = Data.DataViewRowState.Unchanged Then
                'check keberadaan data anak di server
                'jika ada return dan refetch
                If Me.grdPriceHistory.Row <= -1 Then
                    Return
                End If
                If Me.clsBPHistory.HasReferenceDataPriceHistory(e.Row.Cells("BRANDPACK_ID").Value, Convert.ToDateTime(e.Row.Cells("START_DATE").Value)) = True Then
                    Me.ShowMessageInfo(Me.MessageCantDeleteData)
                    e.Cancel = True
                    Return
                End If
                If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                    e.Cancel = True : Return
                End If
                e.Cancel = False : Me.grdPriceHistory.UpdateData()
            End If
            Me.GridSelect = Activegrid.grdPriceHistory
        Catch ex As Exception
            e.Cancel = True
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
  
    Private Sub grdPriceHistory_Error(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ErrorEventArgs) Handles grdPriceHistory.Error
        Try
            Me.ShowMessageInfo("Data Can not be Updated due to " & e.Exception.Message)
            Me.grdPriceHistory.CancelCurrentEdit()
            'Me.grdPriceHistory.Refetch()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdPriceHistory_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdPriceHistory.MouseDoubleClick
        Try
            If Me.clsBPHistory.GetDataViewPrice().RowStateFilter = Data.DataViewRowState.OriginalRows _
          Or Me.clsBPHistory.GetDataViewPrice().RowStateFilter = Data.DataViewRowState.CurrentRows _
          Or Me.clsBPHistory.GetDataViewPrice().RowStateFilter = Data.DataViewRowState.Unchanged Then
                If Me.clsBPHistory.HasReferenceDataPriceHistory(Me.grdPriceHistory.GetValue("BRANDPACK_ID"), CDate(Me.grdPriceHistory.GetValue("START_DATE"))) = True Then
                    Me.ShowMessageInfo(Me.MessageDataCantChanged)
                    'e.Cancel = True
                    'Me.grdPriceHistory.Refetch()
                    'Me.grdPriceHistory.SelectCurrentCellText()
                    Return
                End If
                If Me.grdPriceHistory.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                    Me.EP = New EditPrice()
                    Me.EP.StartPosition = FormStartPosition.CenterScreen
                    EP.txtPrice.Value = Me.grdPriceHistory.GetValue("PRICE")
                    EP.txtBrandPack.Text = Me.grdPriceHistory.GetRow().Cells("BRANDPACK_ID").Text
                    EP.dtPicStartDate.Value = Convert.ToDateTime(Me.grdPriceHistory.GetValue("START_DATE"))
                    EP.StartDate = Convert.ToDateTime(Me.grdPriceHistory.GetValue("START_DATE"))
                    Me.EP.ShowDialog(Me)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdPriceHistory_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPriceHistory.CurrentCellChanged
        Try
            If Not Me.HasLoad Then : Return : End If
            If Me.SetVal = SetValue.Updating Then : Return : End If
            If Me.grdPriceHistory.DataSource Is Nothing Then : Return : End If
            If Me.grdPriceHistory.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                If Me.clsBPHistory.GetDataViewPrice().RowStateFilter = Data.DataViewRowState.CurrentRows Then
                    Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
                    Me.grdBrandPack.RootTable.Columns("PRICE_TAG").EditType = Janus.Windows.GridEX.EditType.NoEdit
                Else
                    Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                End If
            Else
                Me.grdPriceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#End Region
    Private Sub EP_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles EP.OKClick
        Try
            If (Me.EP.txtPrice.Value = 0) Or (Me.EP.txtPrice.Value Is Nothing) Then
                Return
            Else
                Me.SetVal = SetValue.Updating
                Me.grdPriceHistory.SetValue("PRICE", Me.EP.txtPrice.Value)
                Me.grdPriceHistory.SetValue("START_DATE", Convert.ToDateTime(Me.EP.dtPicStartDate.Value.ToShortDateString()))
                Dim myDate As DateTime = Convert.ToDateTime(Me.EP.dtPicStartDate.Value.ToShortDateString())
                Dim strDate As String = myDate.Day.ToString() + "/" + myDate.Month.ToString() + "/" + myDate.Year.ToString()
                Dim Price_Tag As Object = Me.grdPriceHistory.GetValue("BRANDPACK_ID").ToString() & "|" & strDate
                Me.grdPriceHistory.SetValue("PRICE_TAG", Price_Tag)
                Me.grdPriceHistory.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                Me.grdPriceHistory.SetValue("MODIFY_DATE", NufarmBussinesRules.SharedClass.ServerDate())
                Me.grdPriceHistory.UpdateData() : Me.EP.Close() : Me.SetVal = SetValue.None
            End If
        Catch ex As Exception
            Me.SetVal = SetValue.None
        Finally

        End Try
    End Sub
    Private Sub txtFilterDropDownBrandPack_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFilterDropDownBrandPack.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.btnAplyBrandPack_Click(Me.btnAplyBrandPack, New EventArgs())
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtFilterDropDownPH_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFilterDropDownPH.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.btnApplyDropDownPH_Click(Me.btnApplyDropDownPH, New EventArgs())
        End If
    End Sub
#End Region

End Class
