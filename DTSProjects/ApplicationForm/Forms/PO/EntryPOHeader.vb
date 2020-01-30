Public Class EntryPOHeader
    Friend Enum ModeSave
        Save
        Update
    End Enum
    Private Enum StateFilingMCB
        Filling
        HasFilled
    End Enum
    Private Enum StateFillingGrid
        HasFilled
        Filling
    End Enum
    Private isChoosingDate As Boolean = True
    Private SFM As StateFilingMCB, SFG As StateFillingGrid, Mode As ModeSave
    Private m_clsPO As NufarmBussinesRules.PurchaseOrder.PORegistering
    Friend frmParent As PO = Nothing
    Private Property clsPO() As NufarmBussinesRules.PurchaseOrder.PORegistering
        Get
            If (IsNothing(Me.m_clsPO)) Then
                Me.m_clsPO = New NufarmBussinesRules.PurchaseOrder.PORegistering()
            End If
            Return Me.m_clsPO
        End Get
        Set(ByVal value As NufarmBussinesRules.PurchaseOrder.PORegistering)
            Me.m_clsPO = value
        End Set
    End Property
    Private Function IsValidEntry() As Boolean
        Dim B As Boolean = True
        If Me.txtPOReference.Text = "" Then
            Me.baseTooltip.Show("Please define PO_REF_NO", Me.txtPOReference, 2500)
            Me.txtPOReference.Focus() : B = False
        ElseIf Me.dtPicRefDate.Text = "" Then
            Me.baseTooltip.Show("Please define PO_Date", Me.dtPicRefDate, 2500)
            Me.dtPicRefDate.Focus() : Me.dtPicRefDate.DroppedDown = True : B = False
        ElseIf Me.mcbDistributor.Value Is Nothing Or Me.mcbDistributor.SelectedIndex <= -1 Then
            Me.baseTooltip.Show("Please Difine Distributor", Me.mcbDistributor, 2500)
            Me.mcbDistributor.Focus() : Me.mcbDistributor.DroppedDown = True : B = False
        End If
        Return B
    End Function
    Private Sub unabledEntry()
        Me.dtPicRefDate.ReadOnly = True : Me.mcbDistributor.Enabled = False
        Me.btnInsert.Enabled = False
    End Sub
    Private Sub enabledEntry()
        Me.dtPicRefDate.ReadOnly = False : Me.mcbDistributor.Enabled = True
        Me.btnInsert.Enabled = True
    End Sub
    Private Sub ClearInforDistributor()
        Me.txtRegionalArea.Text = ""
        Me.txtTerritoryArea.Text = ""
        Me.txtDistributorContact.Text = ""
        Me.txtDistributorPhone.Text = ""
    End Sub
    Private Sub ClearEntry()
        Me.dtPicRefDate.Text = ""
        Me.mcbDistributor.Value = Nothing
        Me.txtPOReference.Text = ""
    End Sub
    Private Sub BindMultiColumnCombo(ByVal DV As DataView)
        Me.SFM = StateFilingMCB.Filling
        Me.mcbDistributor.SetDataBinding(DV, "")
        Me.mcbDistributor.DropDownList.RetrieveStructure()
        Me.mcbDistributor.DroppedDown = True
        For Each item As Janus.Windows.GridEX.GridEXColumn In Me.mcbDistributor.DropDownList.Columns
            If item.DataMember = "DISTRIBUTOR_ID" Or item.DataMember = "DISTRIBUTOR_NAME" Then
                item.Visible = True : item.AutoSize()
            Else
                item.Visible = False
            End If
        Next
        Me.mcbDistributor.DroppedDown = False
        Me.SFM = StateFilingMCB.HasFilled
    End Sub
    Private Sub InflateData()
        'check tanggal oa dan sppb kalo munggin ada
        'jika tanggal oa/sppb < tanggal po cancel
        Me.isChoosingDate = False
        Me.txtPOReference.Text = Me.grdPO.GetValue("PO_REF_NO").ToString()
        Me.txtPOReference.Enabled = False
        Me.dtPicRefDate.Value = Me.grdPO.GetValue("PO_REF_DATE")
        Me.dtPicRefDate.Text = Convert.ToDateTime(Me.grdPO.GetValue("PO_REF_DATE")).ToShortDateString()
        Me.mcbDistributor.Value = Nothing
        Me.mcbDistributor.DroppedDown = True
        If Me.clsPO.HasExistBrandPack(Me.grdPO.GetValue("PO_REF_NO").ToString(), False) Then
            Me.unabledEntry()
        Else
            Me.enabledEntry()
        End If

        Dim DV As DataView = CType(Me.mcbDistributor.DataSource, DataView)
        DV.RowFilter = "" : Me.BindMultiColumnCombo(DV)
        Me.mcbDistributor.Value = Me.grdPO.GetValue("DISTRIBUTOR_ID")
        Me.btnInsert.Text = "&Update"
        Me.mcbDistributor.DroppedDown = False
        Me.isChoosingDate = True
    End Sub
    Friend Sub InitializeData()
        Me.dtPicFrom.Value = NufarmBussinesRules.SharedClass.ServerDate.AddDays(-1)
        Me.dtpicUntil.Value = NufarmBussinesRules.SharedClass.ServerDate
        'bind distributor
        Dim Dv As DataView = Me.clsPO.getDistributor()
        Me.BindMultiColumnCombo(Dv)
    End Sub
    Private Sub BindGrid(ByVal DS As DataSet)
        Dim DV As DataView = DS.Tables(0).DefaultView
        DV.Sort = "CREATE_DATE DESC"
        Me.grdPO.SetDataBinding(DV, "")
        Me.grdPO.DropDowns("DISTRIBUTOR").SetDataBinding(DS.Tables(1).DefaultView, "")
        Me.grdPO.DropDowns("DISTRIBUTOR").AutoSizeColumns()
    End Sub
    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rowFilter As String = "DISTRIBUTOR_NAME LIKE '%" & Me.mcbDistributor.Text & "%'"
            Dim DV As DataView = CType(Me.mcbDistributor.DataSource, DataView)
            DV.RowFilter = rowFilter : Me.BindMultiColumnCombo(DV)
            Dim item As Integer = Me.mcbDistributor.DropDownList.RecordCount()
            Me.ShowMessageInfo(item.ToString() & " item(s) found")
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mcbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mcbDistributor.ValueChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ClearInforDistributor()
            If Me.SFM = StateFilingMCB.Filling Then : Return : End If
            If Me.mcbDistributor.SelectedIndex <= -1 Then : Return : End If
            If IsNothing(Me.mcbDistributor.Value) Then : Return : End If
            Dim DV As DataView = CType(Me.mcbDistributor.DataSource, DataView)
            DV.Sort = "DISTRIBUTOR_ID DESC"
            Dim Index As Integer = DV.Find(Me.mcbDistributor.Value)
            If Index <> -1 Then
                Me.txtDistributorContact.Text = DV(Index)("CONTACT").ToString()
                Me.txtDistributorPhone.Text = DV(Index)("PHONE").ToString()
                Me.txtRegionalArea.Text = DV(Index)("REGIONAL_AREA").ToString()
                Me.txtTerritoryArea.Text = DV(Index)("TERRITORY_AREA").ToString()
            End If

        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtPOReference_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPOReference.TextChanged
        If Me.txtPOReference.Text = "" And Me.txtPOReference.Enabled Then
            Me.unabledEntry()
        Else
            Me.enabledEntry()
        End If
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Me.ClearEntry() : Me.Mode = ModeSave.Save : Me.btnInsert.Text = "&Insert"
        Me.txtPOReference.Text = "" : Me.txtPOReference.Enabled = True : Me.txtPOReference.Focus()
    End Sub

    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Not Me.IsValidEntry Then : Return : End If
            If Me.Mode = ModeSave.Update Then
                Me.clsPO.PO_REF_NO = Me.txtPOReference.Text
            Else
                Me.clsPO.PO_REF_NO = RTrim(Me.txtPOReference.Text.TrimStart())
            End If
            Me.clsPO.PO_REF_DATE = Convert.ToDateTime(Me.dtPicRefDate.Value.ToShortDateString())
            Me.clsPO.DistributorID = Me.mcbDistributor.Value.ToString()
            Dim DS As DataSet : DS = New DataSet()
            Dim StartDate As Object = Nothing, EndDate As Object = Nothing
            If Me.dtPicFrom.Text <> "" Then
                StartDate = Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString())
            End If
            If Me.dtpicUntil.Text <> "" Then
                EndDate = Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString())
            End If
            If Me.Mode = ModeSave.Save Then
                Me.clsPO.Save(NufarmBussinesRules.common.Helper.SaveMode.Insert, DS, StartDate, EndDate)
            Else
                Me.clsPO.Save(NufarmBussinesRules.common.Helper.SaveMode.Update, DS, StartDate, EndDate)
            End If
            Me.BindGrid(DS) : Me.btnAddNew_Click(Me.btnAddNew, New EventArgs())
            Me.txtPOReference.Focus()
            If Not IsNothing(Me.frmParent) Then
                frmParent.MustReload = True
            End If
        Catch ex As Exception
            Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnChekExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChekExisting.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.clsPO.IsExisted(RTrim(Me.txtPOReference.Text)) Then
                Me.ShowMessageInfo(Me.MessageDataHasExisted)
            Else
                Me.ShowMessageInfo(Me.MessageDataSaveToAdd)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtPicFrom_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Me.dtPicFrom.Text = ""
    End Sub

    Private Sub dtpicUntil_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpicUntil.KeyDown
        Me.dtpicUntil.Text = ""
    End Sub

    Private Sub btnAplyFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyFilter.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.SFG = StateFillingGrid.Filling
            Dim DS As DataSet = Nothing
            If Me.dtPicFrom.Text <> "" And Me.dtpicUntil.Text <> "" Then
                DS = Me.clsPO.getViewPOHeader(Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
            ElseIf Me.dtPicFrom.Text <> "" And Me.dtpicUntil.Text = "" Then
                DS = Me.clsPO.getViewPOHeader(Convert.ToDateTime(Me.dtPicFrom.Value.ToShortDateString()))
            ElseIf Me.dtpicUntil.Text <> "" Then
                DS = Me.clsPO.getViewPOHeader(, Convert.ToDateTime(Me.dtpicUntil.Value.ToShortDateString()))
            Else
                Me.ShowMessageInfo("You must define StartDate or EndDate")
            End If
            Me.BindGrid(DS) : Me.SFG = StateFillingGrid.HasFilled
        Catch ex As Exception
            Me.SFG = StateFillingGrid.HasFilled : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub EntryPOHeader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.btnAplyFilter_Click(Me.btnAplyFilter, New EventArgs())
            Me.dtPicRefDate.Text = ""
            Me.btnInsert.Text = "&Insert" : Me.Mode = ModeSave.Save
            Me.AcceptButton = Me.btnInsert : Me.CancelButton = Me.btnClose
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdPO_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPO.CurrentCellChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.SFG = StateFillingGrid.Filling Then : Return : End If
            If Me.grdPO.SelectedItems Is Nothing Then : Return : End If

            If Not Me.grdPO.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.ClearEntry()
            Else
                Me.InflateData()
                ''chek reference pO 
                'If Me.clsPO.HasReferencedData(Me.grdPO.GetValue("PO_REF_NO").ToString()) Then
                '    Me.unabledEntry()
                'Else
                '    Me.enabledEntry()
                'End If
                Me.Mode = ModeSave.Update
            End If

        Catch ex As Exception
            'Me.ShowMessageInfo(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub pnlPencapaian_ExpandedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles pnlPencapaian.ExpandedChanged
        If Me.pnlPencapaian.Expanded Then
            Me.Size = New Size(729, 533)
        Else
            Me.Size = New Size(729, 236)
        End If
    End Sub

    Private Sub grdPO_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdPO.DeletingRecord
        Try
            If Me.ShowConfirmedMessage(Me.ConfirmDeleteMessage) = Windows.Forms.DialogResult.No Then
                e.Cancel = True : Return : End If
            Me.Cursor = Cursors.WaitCursor
            Me.SFG = StateFillingGrid.Filling
            Me.clsPO.DeletePO(Me.grdPO.GetValue("PO_REF_NO").ToString(), True)
        Catch ex As Exception
            e.Cancel = True : Me.ShowMessageInfo(ex.Message)
        Finally
            Me.SFG = StateFillingGrid.HasFilled : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub dtPicRefDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtPicRefDate.ValueChanged
        Try
            If Not Me.isChoosingDate Then : Return : End If
            If Me.txtPOReference.Text = "" Then
                Me.baseTooltip.Show("Plese enter PO NO first", Me.txtPOReference, 250) : Me.txtPOReference.Focus() : Return
            End If
            'check tanggal oa dan sppb kalo munggin ada
            'jika tanggal oa/sppb < tanggal po cancel
            Me.Cursor = Cursors.WaitCursor
            Dim listDate As List(Of DateTime) = Me.clsPO.getOADateAndSPPBDate(RTrim(Me.txtPOReference.Text))
            For i As Integer = 0 To listDate.Count - 1
                If (Convert.ToDateTime(Me.dtPicRefDate.Value.ToShortDateString()) > listDate(i)) Then
                    Me.ShowMessageInfo("can not change PO DATE " & vbCrLf & _
                                        "OA or SPPB DATE < PO DATE ")
                    Me.isChoosingDate = False
                    If Me.Mode = ModeSave.Save Then
                        Me.dtPicRefDate.Text = ""
                    ElseIf Me.grdPO.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        Me.dtPicRefDate.Value = Me.grdPO.GetValue("PO_REF_DATE")
                    Else
                        Me.dtPicRefDate.Text = ""
                    End If
                    Me.isChoosingDate = True : Return
                End If
            Next
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class