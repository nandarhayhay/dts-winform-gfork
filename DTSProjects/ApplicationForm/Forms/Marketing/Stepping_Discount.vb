Public Class Stepping_Discount

#Region " Deklarasi "
    Private clsStepingDiscount As NufarmBussinesRules.Program.SteppingDiscount
    Friend Mode As ModeSave
    Private SFM As StateFillingMCB
    Friend PROG_BRANDPACK_DIST_ID As String
    Private UpdateState As StateUpdate
    Private SFG As StateFillingGrid
    Private HasLeaved As Boolean
    Private HasLoad As Boolean
#End Region

#Region " Enum "
    Private Enum StateUpdate
        Updating
        Updated
    End Enum
    Friend Enum ModeSave
        Save
        Update
    End Enum
    Private Enum StateFillingMCB
        Filling
        HasFilled
    End Enum
    Private Enum StateFillingGrid
        Filling
        HasFilled
    End Enum
#End Region

#Region " Sub "
    Friend Sub InitializeData()
        Me.RefreshData()
    End Sub
    Private Sub RefreshData()
        Me.SFG = StateFillingMCB.Filling
        Me.SFM = StateFillingMCB.Filling
        Me.UpdateState = StateUpdate.Updating
        Me.clsStepingDiscount = New NufarmBussinesRules.Program.SteppingDiscount()
        If Me.Mode = ModeSave.Save Then
            Me.clsStepingDiscount.GetViewDistributor()
        Else
            Me.clsStepingDiscount.GetViewDistributor_1()
        End If
        Me.BindMulticolumnCombo(Me.clsStepingDiscount.ViewDistStepping(), "")
        If Me.Mode = ModeSave.Save Then
            Me.clsStepingDiscount.CreateViewSteppingDiscount()
        Else
            Me.clsStepingDiscount.CreateViewSteppingDiscount(Me.PROG_BRANDPACK_DIST_ID)
            If Me.clsStepingDiscount.HasReferencedDataMRKT_BRANDPCK_DISTRIBUTOR_1(Me.PROG_BRANDPACK_DIST_ID) = True Then
                Me.UnabledControl()
            Else
                'Me.clsStepingDiscount.CreateViewSteppingDiscount()
                Me.EnabledControl()
                Me.MultiColumnCombo3.Enabled = False
            End If
        End If
        Me.BindGrid()
    End Sub
    Private Sub UnabledControl()
        Me.GridEX1.Enabled = False
        Me.ButtonEntry1.btnInsert.Enabled = False
        Me.MultiColumnCombo3.Enabled = False
    End Sub
    Private Sub EnabledControl()
        Me.GridEX1.Enabled = True
        Me.ButtonEntry1.btnInsert.Enabled = True
        Me.MultiColumnCombo3.Enabled = True
    End Sub
    Private Sub BindGrid()
        Me.GridEX1.SetDataBinding(Me.clsStepingDiscount.ViewSteppingDiscount(), "")
    End Sub
    Private Sub BindMulticolumnCombo(ByVal dtview As DataView, ByVal rowFilter As String)
        If Not IsNothing(dtview) Then
            dtview.RowFilter = rowFilter
        End If
        Me.MultiColumnCombo3.SetDataBinding(dtview, "")
    End Sub
    Private Sub SaveSchanges()
        Me.clsStepingDiscount.SaveChanges(Me.clsStepingDiscount.GetDataset().GetChanges())
        Me.clsStepingDiscount.GetDataset().AcceptChanges()
        Me.ShowMessageInfo(Me.MessageSavingSucces)
        Me.SFG = StateFillingGrid.Filling
        Me.BindGrid()
        Me.SFG = StateFillingGrid.HasFilled
        Me.MultiColumnCombo3.Enabled = False
    End Sub
#End Region

#Region "Function "
    Private Function IsValid() As Boolean
        If Me.MultiColumnCombo3.Value Is Nothing Then
            Me.baseTooltip.SetToolTip(Me.MultiColumnCombo3, "Distributor is Null !." & vbCrLf & "Please Defined distributor name.")
            Me.baseTooltip.Show(Me.baseTooltip.GetToolTip(Me.MultiColumnCombo3), Me.MultiColumnCombo3, 2000)
            'Me.baseBallonSmall.SetBalloonCaption(Me.MultiColumnCombo3, "Distributor is NULL !.")
            'Me.baseBallonSmall.SetBalloonText(Me.MultiColumnCombo3, "Disributor must not be NULL.")
            'Me.baseBallonSmall.ShowBalloon(Me.MultiColumnCombo3)
            Me.MultiColumnCombo3.Focus()
            Return False
        End If
        Return True
    End Function
#End Region

#Region " Event Procedure "

#Region " Grid Ex "
    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        Try
            If Me.PROG_BRANDPACK_DIST_ID = "" Then
                Return
            End If
            If Me.GridEX1.GetValue("MORE_THAN_QTY") Is Nothing Then
                Me.ShowMessageInfo("PERCENTAGE is NULL !.")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Me.GridEX1.MoveToNewRecord()
            ElseIf Me.GridEX1.GetValue("MORE_THAN_QTY") Is DBNull.Value Then
                Me.ShowMessageInfo("PERCENTAGE is NULL !.")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Me.GridEX1.MoveToNewRecord()
            ElseIf Me.GridEX1.GetValue("STEPPING_DISC_PCT") Is Nothing Then
                Me.ShowMessageInfo("DISCOUNT is NULL !.")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Me.GridEX1.MoveToNewRecord()
            ElseIf Me.GridEX1.GetValue("STEPPING_DISC_PCT") Is DBNull.Value Then
                Me.ShowMessageInfo("DISCOUNT is NULL !.")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Me.GridEX1.MoveToNewRecord()
            End If
            Me.GridEX1.SetValue("PROG_BRANDPACK_DIST_ID", Me.PROG_BRANDPACK_DIST_ID)
            Me.GridEX1.SetValue("CREATE_DATE", NufarmBussinesRules.SharedClass.ServerDate())
            Me.GridEX1.SetValue("CREATE_BY", NufarmBussinesRules.User.UserLogin.UserName)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        Try
            If Me.SFG = StateFillingGrid.Filling Then
                Return
            End If
            If Me.UpdateState = StateUpdate.Updating Then
                Return
            End If
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.NewRecord Then
                If CInt(Me.GridEX1.GetValue("MORE_THAN_QTY")) > 100 Then
                    Me.ShowMessageInfo("Value Exceeds from 100")
                    Me.UpdateState = StateUpdate.Updating
                    Me.GridEX1.SetValue("MORE_THAN_QTY", 0)
                    Me.UpdateState = StateUpdate.Updated
                ElseIf CDec(Me.GridEX1.GetValue("STEPPING_DISC_PCT")) > 100 Then
                    Me.ShowMessageInfo("Value Exceeds from 100")
                    Me.UpdateState = StateUpdate.Updating
                    Me.GridEX1.SetValue("STEPPING_DISC_PCT", 0)
                    Me.UpdateState = StateUpdate.Updated
                End If
                Return
            End If
            If CInt(Me.GridEX1.GetValue("MORE_THAN_QTY")) > 100 Then
                Me.ShowMessageInfo("Value Exceeds from 100")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Return
            ElseIf CDec(Me.GridEX1.GetValue("STEPPING_DISC_PCT")) > 100 Then
                Me.ShowMessageInfo("Value Exceeds from 100")
                Me.GridEX1.CancelCurrentEdit()
                Me.GridEX1.Refetch()
                Return
            End If
            If Me.Mode = ModeSave.Update Then
                Me.UpdateState = StateUpdate.Updating
                Me.GridEX1.SetValue("MODIFY_DATE", NufarmBussinesRules.SharedClass.ServerDate())
                Me.GridEX1.SetValue("MODIFY_BY", NufarmBussinesRules.User.UserLogin.UserName)
                Me.UpdateState = StateUpdate.Updated
            End If
            Me.GridEX1.UpdateData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If Me.clsStepingDiscount.HasReferencedDataMRKT_BRANDPCK_DISTRIBUTOR_1(Me.PROG_BRANDPACK_DIST_ID) = True Then
                Me.ShowMessageInfo(Me.MessageCantDeleteData)
                e.Cancel = True
            Else
                e.Cancel = False
                Me.GridEX1.UpdateData()
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region " Multicolum Combo "
  
#End Region

#Region " Button "
   
#End Region

#End Region

    Private Sub Stepping_Discount_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not IsNothing(Me.clsStepingDiscount) Then
                If Me.clsStepingDiscount.GetDataset().HasChanges Then
                    If Me.HasLeaved = True Then
                        Return
                    End If
                    If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.Yes Then
                        Me.clsStepingDiscount.SaveChanges(Me.clsStepingDiscount.GetDataset().GetChanges())
                        Me.ShowMessageInfo(Me.MessageSavingSucces)
                    End If
                End If
            End If
            Me.clsStepingDiscount.Dispose()
            'Me.Dispose(True)
        Catch ex As DBConcurrencyException
            Me.ShowMessageInfo(Me.MessageDBCOncurency)
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_Stepping_Discount_FormClosing")
        End Try
    End Sub

    Private Sub Stepping_Discount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.AcceptButton = Me.ButtonEntry1.btnInsert
            Me.CancelButton = Me.ButtonEntry1.btnClose
            Me.ButtonEntry1.btnInsert.Text = "&Save Changes"
        Catch ex As Exception

        Finally
            Me.SFG = StateFillingMCB.HasFilled
            Me.SFM = StateFillingMCB.HasFilled
            Me.UpdateState = StateUpdate.Updated
        End Try

    End Sub

    Private Sub btnFilterDistributor_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDistributor.btnClick
        Try
            Me.SFM = StateFillingMCB.Filling
            Me.BindMulticolumnCombo(Me.clsStepingDiscount.ViewDistStepping, "DISTRIBUTOR_NAME LIKE '%" + Me.MultiColumnCombo3.Text + "%'")
            Dim itemCount As Integer = Me.MultiColumnCombo3.DropDownList().RecordCount()
            Me.ShowMessageInfo(itemCount.ToString() + " item(s) found")
            Me.SFM = StateFillingMCB.HasFilled
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.Enter
        Me.HasLeaved = False
    End Sub

    Private Sub GridEX1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.Leave
        Try
            If Not IsNothing(Me.clsStepingDiscount) Then
                If Me.clsStepingDiscount.GetDataset().HasChanges() Then
                    If Me.HasLeaved = True Then
                        Return
                    End If
                    If Me.IsValid = False Then
                        Return
                    End If
                    If Me.ShowConfirmedMessage(Me.MessageDataHasChanged) = Windows.Forms.DialogResult.No Then
                        Me.HasLeaved = True
                        Me.MultiColumnCombo3.Enabled = False
                        Me.ButtonEntry1.btnInsert.Enabled = True
                    Else
                        Me.SaveSchanges()
                    End If
                End If
            End If
        Catch ex As DBConcurrencyException
            Me.ShowMessageInfo(Me.MessageDBCOncurency)
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_GridEX1_Leave")
        End Try
    End Sub

    Private Sub ButtonEntry1_btnClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEntry1.btnClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case CType(sender, Janus.Windows.EditControls.UIButton).Name
                Case "btnAddNew"
                    Me.MultiColumnCombo3.Text = ""
                    Me.GridEX1.Enabled = False
                    Me.grpEdit.Size = New System.Drawing.Size(377, 41)
                    Me.Mode = ModeSave.Save
                    Me.clsStepingDiscount.GetViewDistributor()
                    Me.BindMulticolumnCombo(Me.clsStepingDiscount.ViewDistStepping(), "")
                    Me.clsStepingDiscount.CreateViewSteppingDiscount()
                    Me.BindGrid()
                    Me.MultiColumnCombo3.Enabled = True
                Case "btnInsert"
                    If Me.IsValid() = False Then
                        Return
                    End If
                    If Me.clsStepingDiscount.GetDataset().HasChanges() Then
                        Me.SaveSchanges()
                    End If
                Case "btnClose"
                    Me.Close()
            End Select
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ButtonEntry1_btnClick")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub MultiColumnCombo3_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MultiColumnCombo3.ValueChanged
        Try
            If Me.Mode = ModeSave.Save Then
                If Me.SFG = StateFillingGrid.Filling Then
                    Return
                End If
            End If
            Me.Cursor = Cursors.WaitCursor
            If Me.Mode = ModeSave.Save Then
                If (Me.MultiColumnCombo3.Value Is Nothing) Or (Me.MultiColumnCombo3.SelectedIndex = -1) Then
                    Me.SFG = StateFillingGrid.Filling
                    If Not IsNothing(Me.clsStepingDiscount.ViewSteppingDiscount()) Then
                        Me.clsStepingDiscount.ViewSteppingDiscount().Table.Clear()
                        Me.BindGrid()
                        Me.GridEX1.Enabled = False
                        Me.SFG = StateFillingGrid.HasFilled
                    End If
                    Me.grpEdit.Size = New System.Drawing.Size(377, 41)
                    Return
                End If
            ElseIf Me.MultiColumnCombo3.Value Is Nothing Then
                If Not IsNothing(Me.clsStepingDiscount.ViewSteppingDiscount()) Then
                    Me.SFG = StateFillingGrid.Filling
                    Me.clsStepingDiscount.ViewSteppingDiscount().Table.Clear()
                    Me.BindGrid()
                    Me.GridEX1.Enabled = False
                    Me.SFG = StateFillingGrid.HasFilled
                End If
                Me.grpEdit.Size = New System.Drawing.Size(377, 41)
                Return
            End If
            If Me.Mode = ModeSave.Update Then
                Me.clsStepingDiscount.CreateViewSteppingDiscount(Me.MultiColumnCombo3.Value.ToString())
            Else
                Me.clsStepingDiscount.CreateViewSteppingDiscount()
            End If
            Me.SFG = StateFillingGrid.Filling
            Me.BindGrid()
            Me.GridEX1.Enabled = True
            Me.SFG = StateFillingGrid.HasFilled
            Me.grpEdit.Size = New System.Drawing.Size(377, 96) '77; 41
            Me.PROG_BRANDPACK_DIST_ID = Me.MultiColumnCombo3.Value.ToString()
            If Me.Mode = ModeSave.Update Then
                If Me.clsStepingDiscount.GetRowByPROG_BRANDPACK_DIST_ID(Me.PROG_BRANDPACK_DIST_ID) <> -1 Then
                    Me.txtProgramName.Text = Me.clsStepingDiscount.PROGRAM_NAME
                    Me.txtBrandPackName.Text = Me.clsStepingDiscount.BRANDPACK_NAME
                Else
                    Me.grpEdit.Size = New System.Drawing.Size(377, 41)
                End If
            Else
                Me.txtBrandPackName.Text = Me.MultiColumnCombo3.DropDownList().GetValue("BRANDPACK_NAME").ToString()
                Me.txtProgramName.Text = Me.MultiColumnCombo3.DropDownList().GetValue("PROGRAM_NAME").ToString()
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_Error(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ErrorEventArgs) Handles GridEX1.Error
        Me.ShowMessageInfo("Data Can not be Updated due to " & e.Exception.Message)
        Me.GridEX1.CancelCurrentEdit()
        Me.GridEX1.Refetch()
    End Sub
End Class
