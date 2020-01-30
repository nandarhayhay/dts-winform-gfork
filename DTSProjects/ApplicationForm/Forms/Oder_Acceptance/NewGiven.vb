Public Class NewGiven

#Region " Deklarasi "
    Private clsAgreementBrand As New NufarmBussinesRules.DistributorAgreement.Include
    Private SFG As StateFillingGrid
    Private SFM As StateFillingMCB
    Private SelectedItemBrand As ItemSelected
    Private Mode As ModeSave
    Private BRANDPACK_ID As String = ""
    Private Hasload As Boolean
    Private IsRefreshing As Boolean
#End Region

#Region " Enum "

    Private Enum StateFillingGrid
        HasFilled
        Filling
        Disposing
    End Enum

    Private Enum StateFillingMCB
        HasFilled
        Filling
    End Enum

    Private Enum ItemSelected
        FromMCB
        FromGrid
    End Enum
    Private Enum ModeSave
        Save
        Update
    End Enum
#End Region

#Region " Sub Procedure "

    Private Sub InflateData()
        Me.txtGiven.Value = Me.GridEX1.GetValue("DISC_PCT")
        Me.mcbBrandName.Value = Me.GridEX1.GetValue("BRAND_ID")
        Me.mcbAgreement.Value = Me.clsAgreementBrand.getAgreeMentNO(Me.GridEX1.GetValue("AGREE_BRAND_ID").ToString())
        Me.mcbBrandName.Value = Me.GridEX1.GetValue("BRAND_ID")
        Me.dtPicstart.Text = CDate(Me.GridEX1.GetValue("START_DATE")).ToShortDateString()
        'Me.dtPicEnd.Value = CDate(Me.GridEX1.GetValue("END_DATE"))
        If Not IsNothing(Me.GridEX1.GetValue("GIVEN_DESCRIPTION")) Then
            Me.txtGivenDescription.Text = Me.GridEX1.GetValue("GIVEN_DESCRIPTION").ToString()
        End If
    End Sub


    Private Function Isvalid() As Boolean

        If Me.mcbAgreement.SelectedItem Is Nothing Then
            Me.baseTooltip.Show("Please define Agreement !", Me.mcbAgreement, 2500)
            Return False
        ElseIf Me.mcbBrandName.SelectedItem Is Nothing Then
            Me.baseTooltip.Show("Please define Brand_Name !", Me.mcbBrandName, 2500)
            Return False
        ElseIf Me.txtGiven.Value Is Nothing Then
            Me.baseTooltip.Show("Please define Discount !", Me.txtGiven, 2500)
            Return False
        ElseIf Me.dtPicstart.Text = "" Then
            Me.baseTooltip.Show("Please define Start_date !", Me.dtPicstart, 2500)
            Return False
            'ElseIf Me.dtPicEnd.Text = "" Then
            '    Me.baseTooltip.Show("Please define End_date !", Me.dtPicEnd, 2500)
            '    Return False
        End If
        Return True
    End Function

    Private Sub enabledControl()
        'Me.dtPicEnd.ReadOnly = False
        Me.dtPicstart.ReadOnly = False
        Me.txtGiven.ReadOnly = False
        Me.txtGivenDescription.ReadOnly = False
    End Sub

    Private Sub UnabledControl()
        'Me.dtPicEnd.ReadOnly = True
        Me.dtPicstart.ReadOnly = True
        Me.txtGiven.ReadOnly = True
        'Me.txtGivenDescription.ReadOnly = False
    End Sub

    Friend Sub InitializeData(Optional ByVal DISTRIBUTOR_ID As String = "", Optional ByVal BRANDPACK_ID As String = "")
        Try
            Me.clsAgreementBrand = New NufarmBussinesRules.DistributorAgreement.Include()
            Me.SFG = StateFillingGrid.Filling
            If (Not DISTRIBUTOR_ID = "") And (Not BRANDPACK_ID = "") Then
                Me.clsAgreementBrand.GetAgreement(DISTRIBUTOR_ID)
                Me.BindMulticolumnCombo(Me.mcbAgreement, Me.clsAgreementBrand.ViewAgreement())
                'For i As Integer = 0 To Me.mcbAgreement.DropDownList().RecordCount - 1
                '    Dim AGREEMENT_NO As String = Me.mcbAgreement.DropDownList().GetValue("AGREEMENT_NO").ToString()
                '    If Me.clsAgreementBrand.GetAgree_Brand_ID(AGREEMENT_NO, BRANDPACK_ID) <> "" Then
                '        'TRAP AGAR EVENT VALUE CHANGE TIDAK DIFIRE
                '        Me.mcbAgreement.Value = AGREEMENT_NO
                '        Me.mcbBrandName.Value = Me.clsAgreementBrand.Agree_Brand_ID
                '    End If
                'Next
                Me.BRANDPACK_ID = BRANDPACK_ID
            ElseIf (DISTRIBUTOR_ID <> "") And (BRANDPACK_ID = "") Then
                Me.clsAgreementBrand.GetAgreement(DISTRIBUTOR_ID)
                Me.BindMulticolumnCombo(Me.mcbAgreement, Me.clsAgreementBrand.ViewAgreement())
            Else
                Me.LoadData()
            End If
        Catch ex As Exception

        End Try
    End Sub
    
    Private Sub LoadData()
        'Me.clsAgreementBrand = New NufarmBussinesRules.DistributorAgreement.Include()
        Me.clsAgreementBrand.GetData()
        Me.BindMulticolumnCombo(Me.mcbAgreement, Me.clsAgreementBrand.ViewAgreement())
        'ME.clsAgreementBrand.GetItemBrandByAgreementNo(
    End Sub

    Private Sub BindGrid()
        Me.SFG = StateFillingGrid.Filling
        Me.GridEX1.SetDataBinding(Me.clsAgreementBrand.ViewBrandGivenHistory, "")
        Me.SFG = StateFillingGrid.HasFilled
    End Sub

    Private Sub BindMulticolumnCombo(ByVal mcb As Janus.Windows.GridEX.EditControls.MultiColumnCombo, ByVal dtView As DataView)
        Me.SFM = StateFillingMCB.Filling
        mcb.Value = Nothing
        mcb.SetDataBinding(dtView, "")
        Me.SFM = StateFillingMCB.HasFilled
    End Sub

#End Region

    Private Sub NewGiven_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.SFG = StateFillingGrid.Disposing
            If Not IsNothing(Me.clsAgreementBrand) Then
                Me.clsAgreementBrand.Dispose(True)
            End If
            Me.Dispose(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NewGiven_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            'With Me.mcbAgreement.DropDownList()
            '    .Columns("DISTRIBUTOR_NAME").AutoSize()
            '    .Columns("DISTRIBUTOR_NAME").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCellsAndHeader
            '    .Columns("AGREEMENT_NO").AutoSize()
            '    .Columns("AGREEMENT_NO").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCellsAndHeader
            'End With
            'For Each col As Janus.Windows.GridEX.GridEXColumn In Me.mcbAgreement.DropDownList().Columns
            '    col.AutoSize()
            'Next
            'Me.dtPicEnd.Text = ""
            If Me.mcbAgreement.DropDownList().RecordCount > 1 Then
                Return
            End If
            For i As Integer = 0 To Me.mcbAgreement.DropDownList().RecordCount - 1
                Dim AGREEMENT_NO As String = Me.mcbAgreement.DropDownList().GetValue("AGREEMENT_NO").ToString()
                Dim BrandID As Object = Me.clsAgreementBrand.GetBrandID(Me.BRANDPACK_ID)
                If CStr(BrandID) <> "" Then
                    Me.mcbAgreement.Value = AGREEMENT_NO
                    Me.mcbBrandName.Value = BrandID
                End If
                'If (Me.clsAgreementBrand.GetBrandID(Me.BRANDPACK_ID).ToString()) <> "" Then

                'End If
                'If Me.clsAgreementBrand.GetAgree_Brand_ID(AGREEMENT_NO, Me.BRANDPACK_ID) <> "" Then
                '    'TRAP AGAR EVENT VALUE CHANGE TIDAK DIFIRE

                '    Me.mcbBrandName.Value = Me.clsAgreementBrand.Agree_Brand_ID
                'End If
            Next
            If Me.mcbBrandName.Value Is Nothing Then
                Me.dtPicstart.Value = NufarmBussinesRules.SharedClass.ServerDate()
                Me.dtPicstart.Value = NufarmBussinesRules.SharedClass.ServerDate()
                Me.dtPicstart.Text = ""
                'Me.dtPicEnd.Text = ""
            End If
            'Me.dtPicEnd.MinDate = NufarmBussinesRules.SharedClass.ServerDate
            'Me.dtPicstart.Text = ""
            Me.AcceptButton = Me.ButtonEntry1.btnInsert
            Me.CancelButton = Me.ButtonEntry1.btnClose

        Catch ex As Exception

        Finally
            Me.Hasload = True
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ButtonEntry1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEntry1.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                Case "btnAddNew"
                    Me.mcbBrandName.Value = Nothing
                    Me.txtGiven.Value = CObj(0)
                    Me.txtGivenDescription.Text = ""
                    Me.dtPicstart.Text = ""
                    'Me.dtPicEnd.Text = ""
                    Me.enabledControl()
                    Me.mcbBrandName.ReadOnly = False
                    Me.mcbAgreement.ReadOnly = False
                    Me.dtPicstart.ReadOnly = False
                    Me.Mode = ModeSave.Save
                    Me.ButtonEntry1.btnInsert.Text = "&Insert"
                Case "btnInsert"
                    If Me.Isvalid() = False Then
                        Return
                    End If
                    Dim Given_ID As String = ""
                    If Me.Mode = ModeSave.Save Then
                        Given_ID = Me.mcbAgreement.Value.ToString() + "" + Me.mcbBrandName.Value.ToString() & _
                           Me.dtPicstart.Value.ToShortDateString()
                    Else
                        If Me.GridEX1.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                            Given_ID = Me.GridEX1.GetValue("GIVEN_ID").ToString()
                        Else
                            Me.ShowMessageInfo("Please define Row !")
                        End If
                    End If
                    Dim AGREE_BRAND_ID As String = Me.mcbAgreement.Value.ToString() + "" + Me.mcbBrandName.Value.ToString()
                    Dim Start_Date As Date = CDate(Me.dtPicstart.Value.ToShortDateString())
                    'Dim End_date As Date = CDate(Me.dtPicEnd.Value.ToShortDateString())
                    Dim Disc_Pct As Decimal = Me.txtGiven.Value
                    Dim Given_Description As String = ""
                    If Me.txtGivenDescription.Text <> "" Then
                        Given_Description = Me.txtGivenDescription.Text
                    End If
                    If Me.Mode = ModeSave.Save Then
                        Me.clsAgreementBrand.SaveGivenHistory("Save", Given_ID, AGREE_BRAND_ID, Start_Date, _
                         Disc_Pct, Given_Description)
                    Else
                        Me.clsAgreementBrand.SaveGivenHistory("Update", Given_ID, AGREE_BRAND_ID, Start_Date, _
                        Disc_Pct, Given_Description)
                    End If
                    Me.ShowMessageInfo(Me.MessageSavingSucces)
                    Me.UnabledControl()
                    Me.mcbBrandName.ReadOnly = True
                    Me.mcbAgreement.ReadOnly = True
                    Me.SFG = StateFillingGrid.Filling
                    Me.RefreshData1_BtnClick(Me.RefreshData1.btnRefresh, New System.EventArgs())
                    Me.Mode = ModeSave.Save

                Case "btnClose"
                    Me.SFG = StateFillingGrid.Disposing
                    Me.clsAgreementBrand.Dispose(True)
                    Me.Dispose(True)
            End Select
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ButtonEntry1_btnClick")
        Finally
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mcbAgreement_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbAgreement.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.mcbAgreement.SelectedItem Is Nothing Then
                Return
            End If
            If Me.SelectedItemBrand = ItemSelected.FromGrid Then
                Return
            End If
            Me.clsAgreementBrand.CreateView_Brand_Include(Me.mcbAgreement.Value.ToString())
            Me.BindMulticolumnCombo(Me.mcbBrandName, Me.clsAgreementBrand.ViewBrand())
            Dim END_DATE As Object = Me.clsAgreementBrand.GetAgreementEndDate(Me.mcbAgreement.Value.ToString())
            If Not IsNothing(END_DATE) Then
                'Me.dtPicEnd.MaxDate = CDate(END_DATE)
                Me.dtPicstart.MaxDate = CDate(END_DATE)
            Else
                Me.ShowMessageInfo("AGREEMENT " & Me.mcbAgreement.Value.ToString() & " has no End_date !.")
                'Me.dtPicEnd.Text = ""
                'Me.dtPicEnd.ReadOnly = True
            End If
            Me.mcbBrandName.ReadOnly = False
            'With Me.mcbBrandName.DropDownList()
            '    .Columns("BRAND_NAME").AutoSize()
            '    .Columns("BRAND_NAME").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCellsAndHeader
            '    .Columns("BRAND_ID").AutoSize()
            '    .Columns("BRAND_ID").AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.AllCellsAndHeader
            'End With
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFG = StateFillingGrid.Filling Or Me.SFG = StateFillingGrid.Disposing Then
                Return
            End If
            Me.SelectedItemBrand = ItemSelected.FromGrid
            If Not (Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record) Then
                Me.SelectedItemBrand = ItemSelected.FromGrid
                Me.mcbAgreement.Value = Nothing
                Me.mcbBrandName.Value = Nothing
                Me.dtPicstart.Text = ""
                'Me.dtPicEnd.Text = ""
                Me.txtGiven.Value = 0
                Me.txtGivenDescription.Text = ""
                'Me.ButtonEntry1.Enabled = False
                Return
            End If
            Me.InflateData()
            If Me.clsAgreementBrand.HasReferencedGivenHistory(Me.GridEX1.GetValue("ID").ToString(), False) = True Then
                Me.UnabledControl()
            Else
                Me.enabledControl()
                Dim START_DATE As Object = Nothing
                START_DATE = Me.clsAgreementBrand.GetLatestInsertDate(Me.GridEX1.GetValue("AGREE_BRAND_ID").ToString())
                If Not IsNothing(START_DATE) Then
                    Me.dtPicstart.MinDate = CDate(START_DATE).AddDays(1)
                Else
                    START_DATE = Me.clsAgreementBrand.getAgreementStartDate(Me.GridEX1.GetValue("AGREE_BRAND_ID").ToString())
                    Me.dtPicstart.MinDate = CDate(START_DATE)
                End If
                'Me.dtPicEnd.MinDate = NufarmBussinesRules.SharedClass.ServerDate()
            End If
            Me.mcbBrandName.ReadOnly = True
            Me.mcbAgreement.ReadOnly = True
            Me.dtPicstart.ReadOnly = True
            Me.Mode = ModeSave.Update
            If Me.Hasload = False Then
                Me.ButtonEntry1.btnInsert.Text = "&Insert"
            Else
                Me.ButtonEntry1.btnInsert.Text = "&Update"
            End If
            Me.ButtonEntry1.btnInsert.Enabled = True
        Catch ex As Exception

        Finally
            Me.SelectedItemBrand = ItemSelected.FromMCB
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                Return
            End If
            If Me.clsAgreementBrand.IsExistedGIVEN_IDIN_ADH(Me.GridEX1.GetValue("GIVEN_ID").ToString()) Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                e.Cancel = True
                Me.GridEX1.Refetch()
                Return
            End If
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
            Me.clsAgreementBrand.DeleteGivenStory(Me.GridEX1.GetValue("GIVEN_ID").ToString())
            e.Cancel = False
            Me.GridEX1.UpdateData()
            Me.ButtonEntry1.btnInsert.Text = "&Insert"
            Me.Mode = ModeSave.Save
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ GridEX1_DeletingRecord")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilterAgreement_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterAgreement.btnClick
        Try
            CType(Me.mcbAgreement.DataSource, DataView).RowFilter = "AGREEMENT_NO like '%" & Me.mcbAgreement.Text & "%'"
            Me.mcbAgreement.DropDownList().Refetch()
            Dim itemCount As Integer = Me.mcbAgreement.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFilterBrandName_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterBrandName.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            CType(Me.mcbBrandName.DataSource, DataView).RowFilter = "BRAND_NAME LIKE '%" & Me.mcbBrandName.Text & "%'"
            Me.mcbBrandName.DropDownList().Refetch()
            Dim itemCount As Integer = Me.mcbBrandName.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() & " item(s) found")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub dtPicstart_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtPicstart.DropDown
        Try
            'If Me.dtPicstart.Text = "" Then
            '    Return
            'End If
            If Me.mcbBrandName.SelectedItem Is Nothing Then
                Me.ShowMessageInfo("Please define Agreement Brand")
                Me.dtPicstart.Text = ""
                Me.dtPicstart.ReadOnly = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub dtPicEnd_DropDown(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.dtPicstart.Text = "" Then
    '            Me.ShowMessageInfo("Please define Start_Date !")
    '            Me.dtPicEnd.Text = ""
    '            Me.dtPicEnd.ReadOnly = True
    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub mcbBrandName_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles mcbBrandName.DropDown
        Try
            If Me.mcbAgreement.SelectedItem Is Nothing Then
                Me.ShowMessageInfo("Please define Agreement !")
                Me.mcbBrandName.Value = Nothing
                Me.mcbBrandName.ReadOnly = True
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mcbBrandName_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbBrandName.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFM = StateFillingMCB.Filling Then
                Return
            End If
            If Me.SelectedItemBrand = ItemSelected.FromGrid Then
                Return
            End If
            If Me.mcbBrandName.SelectedItem Is Nothing Then
                Me.GridEX1.SetDataBinding(Nothing, "")
                Return
            End If
            Dim AGREE_BRAND_ID As String = Me.mcbAgreement.Value.ToString() + Me.mcbBrandName.Value.ToString()
            Me.clsAgreementBrand.CreateViewGivenStory(AGREE_BRAND_ID)
            Me.BindGrid()
            If Me.IsRefreshing = True Then
                Return
            End If
            Dim START_DATE As Object = Nothing
            Dim END_DATE As Object = Nothing
            ''CHECK APAKAH ADA GIVEN_MASIH ACTIVE
            ''JIKA ADA DAN MASIH ACTIVE START_DATE DI TAMBAH 1 DARI END_DATE GIVENID
            'END_DATE = Me.clsAgreementBrand.getEndDateGivenAgreeBrandID(Me.mcbBrandName.Value.ToString())
            'If Not IsNothing(END_DATE) Then
            '    Me.dtPicstart.Value = CDate(END_DATE).AddDays(1)
            '    Me.dtPicstart.Text = CDate(END_DATE).ToShortDateString()
            '    Me.dtPicstart.ReadOnly = True
            'Else
            START_DATE = Me.clsAgreementBrand.GetLatestInsertDate(Me.mcbAgreement.Value.ToString() + "" + Me.mcbBrandName.Value.ToString())
            If Not IsNothing(START_DATE) Then
                Me.dtPicstart.MinDate = CDate(START_DATE)
            Else
                START_DATE = Me.clsAgreementBrand.getAgreementStartDate(Me.mcbAgreement.Value.ToString() + "" + Me.mcbBrandName.Value.ToString())
                Me.dtPicstart.MinDate = CDate(START_DATE)
            End If
            '    Me.dtPicstart.ReadOnly = False
            '    Me.dtPicstart.Text = ""
            '    Me.dtPicstart.Value = CDate(START_DATE)
            'End If
            'Me.dtPicEnd.MinDate = Me.dtPicstart.Value
            Dim End_DateAgreement As Object = Me.clsAgreementBrand.GetAgreementEndDate(Me.mcbAgreement.Value.ToString())
            If Not IsNothing(End_DateAgreement) Then
                Me.dtPicstart.MaxDate = CDate(End_DateAgreement)
                'Me.dtPicEnd.MaxDate = CDate(End_DateAgreement)
                'Me.dtPicstart.Text = ""
                Me.dtPicstart.ReadOnly = False
            Else
                Me.dtPicstart.Text = ""
                Me.dtPicstart.ReadOnly = True
            End If
        Catch ex As Exception

        Finally
            Me.SelectedItemBrand = ItemSelected.FromMCB
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub RefreshData1_BtnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshData1.BtnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not IsNothing(Me.mcbBrandName.SelectedItem) Then
                Dim AGREE_BRAND_ID As Object = Me.mcbBrandName.Value
                Me.mcbBrandName.Value = Nothing
                Me.IsRefreshing = True
                Me.mcbBrandName.Value = AGREE_BRAND_ID
            ElseIf Not IsNothing(Me.mcbAgreement.SelectedItem) Then
                Dim AGREEMENT_NO As String = Me.mcbAgreement.Value
                Me.mcbAgreement.Value = Nothing
                Me.mcbAgreement.Value = AGREEMENT_NO
            Else
                Me.LoadData()
                Me.BindMulticolumnCombo(Me.mcbAgreement, Me.clsAgreementBrand.ViewAgreement())
            End If
        Catch ex As Exception
            Me.LogMyEvent(ex.Message, Me.Name + "_RefreshData1_BtnClick")
        Finally
            Me.IsRefreshing = False
            Me.SFG = StateFillingGrid.HasFilled
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub dtPicstart_ReadOnlyChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtPicstart.ReadOnlyChanged
        Try
            If Me.dtPicstart.ReadOnly = True Then
                Me.dtPicstart.BackColor = Color.FromArgb(194, 217, 247)
            Else
                Me.dtPicstart.BackColor = Color.FromName("Window")
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub dtPicEnd_ReadOnlyChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.dtPicEnd.ReadOnly = True Then
    '            Me.dtPicEnd.BackColor = Color.FromArgb(194, 217, 247)
    '        Else
    '            Me.dtPicEnd.BackColor = Color.FromName("Window")
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub txtGiven_ReadOnlyChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGiven.ReadOnlyChanged
        Try
            If Me.txtGiven.ReadOnly = True Then
                Me.txtGiven.BackColor = Color.FromArgb(194, 217, 247)
            Else
                Me.txtGiven.BackColor = Color.FromName("Window")
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub dtPicEnd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.Hasload = False Then
    '            Return
    '        End If
    '        If Me.dtPicEnd.Text = "" Then
    '            Return
    '        End If
    '        If Me.SelectedItemBrand = ItemSelected.FromGrid Then
    '            Return
    '        End If
    '        If Me.dtPicstart.Text = "" Then
    '            Me.ShowMessageInfo("Please define Start_Date !")
    '            Me.dtPicEnd.Text = ""
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub dtPicstart_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtPicstart.TextChanged
        Try
            If Me.Hasload = False Then
                Return
            End If
            If Me.dtPicstart.Text = "" Then
                Return
            End If
            If Me.SelectedItemBrand = ItemSelected.FromGrid Then
                Return
            End If
            If Me.mcbBrandName.SelectedItem Is Nothing Then
                Me.ShowMessageInfo("Please define Brand_Name !")
                Me.dtPicstart.Text = ""
                Return
            End If
            'Me.dtPicEnd.MinDate = Me.dtPicstart.Value
            'Me.dtPicEnd.ReadOnly = False
        Catch ex As Exception

        End Try
    End Sub
End Class