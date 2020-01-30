Imports System.Threading
Public Class GonReturnedBackManager
    Public CMain As Main = Nothing
    Public WithEvents frmParent As SPPB = Nothing
    Private m_clsDetail As NufarmBussinesRules.OrderAcceptance.SPPB_Detail
    Private isLoadingRow As Boolean = False
    Private LD As Loading
    Private ThreadProgress As Thread = Nothing
    Private StatProg As StatusProgress = StatusProgress.None
    Private Enum StatusProgress
        None
        Processing
    End Enum
    Private Sub ShowProceed()
        Me.LD = New Loading() : Me.LD.Show() : Me.LD.TopMost = True
        Application.DoEvents()
        While Not Me.StatProg = StatusProgress.None
            Me.LD.Refresh() : Thread.Sleep(100) : Application.DoEvents()
        End While
        Thread.Sleep(100) : Me.LD.Close() : Me.LD = Nothing
    End Sub
    Private Sub ReadAccess()
        If Not CMain.IsSystemAdministrator Then
            frmParent.btnShowReturningGON.Visible = NufarmBussinesRules.User.Privilege.ALLOW_VIEW.GONReceivedBack
            frmParent.btnAddNew.Visible = NufarmBussinesRules.User.Privilege.ALLOW_INSERT.GONReceivedBack
            frmParent.btnEditSPPB.Visible = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.GONReceivedBack
            If NufarmBussinesRules.User.Privilege.ALLOW_DELETE.GONReceivedBack Then
                Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True
            Else : Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False
            End If
        End If
    End Sub
    Private ReadOnly Property clsDetail() As NufarmBussinesRules.OrderAcceptance.SPPB_Detail
        Get
            If IsNothing(Me.m_clsDetail) Then
                Me.m_clsDetail = New NufarmBussinesRules.OrderAcceptance.SPPB_Detail()
            End If
            Return Me.m_clsDetail
        End Get
    End Property
    Private Sub cmbDistributor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmParent.ComboboxValue_Changed
        Try
            Me.Cursor = Cursors.WaitCursor

            Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Me.ShowMessageInfo(ex.Message)
            'Me.LogMyEvent(ex.Message, Me.Name + "_cmbDistributor_ValueChanged")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnFilteDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilteDate.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.StatProg = StatusProgress.Processing
            Me.ThreadProgress = New Thread(AddressOf Me.ShowProceed)
            Me.ThreadProgress.Start()
            Me.isLoadingRow = True
            Dim DistributorID As String = ""
            If frmParent.cmbDistributor.SelectedIndex <> -1 Then
                DistributorID = frmParent.cmbDistributor.Value.ToString()
            End If
            Dim DV As DataView = Me.clsDetail.getGONReturnedBackData(Convert.ToDateTime(Me.dtpicFrom.Value.ToShortDateString()), Convert.ToDateTime(Me.dtPicUntil.Value.ToShortDateString()), DistributorID)
            Me.GridEX1.SetDataBinding(DV, "")
            Me.GridEX1.MoveToNewRecord()
            Me.StatProg = StatusProgress.None
        Catch ex As Exception
            Me.StatProg = StatusProgress.None : MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.isLoadingRow = False : Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            If Me.isLoadingRow Then : Return : End If
            If IsNothing(Me.GridEX1.DataSource) Then : frmParent.btnEditSPPB.Enabled = False : Return : End If
            If Me.GridEX1.RecordCount <= 0 Then : frmParent.btnEditSPPB.Enabled = False : Return : End If
            If Me.GridEX1.SelectedItems.Count <= 0 Then : frmParent.btnEditSPPB.Enabled = False : Return : End If
            If Not Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then : frmParent.btnEditSPPB.Enabled = False : Return : End If
            If Me.GridEX1.GetRow().RowType = Janus.Windows.GridEX.RowType.Record Then
                If frmParent.btnEditSPPB.Visible Then
                    If Not (NufarmBussinesRules.User.UserLogin.IsAdmin) Then
                        frmParent.btnEditSPPB.Enabled = NufarmBussinesRules.User.Privilege.ALLOW_UPDATE.GONReceivedBack
                    Else : frmParent.btnEditSPPB.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If MessageBox.Show("Are you sure you want to delete data ?" & vbCrLf & "Operation can not be undone", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                e.Cancel = True
            End If
            Me.Cursor = Cursors.WaitCursor
            Me.clsDetail.DeleteReturnGONData(Me.GridEX1.GetValue("GRB_ID").ToString())
            Me.GridEX1.UpdateData()
            Me.GridEX1.MoveToNewRecord()
        Catch ex As Exception
            e.Cancel = True : MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GonReturnedBackManager_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.clsDetail.Dispose(True)
            RemoveHandler frmParent.ComboboxValue_Changed, AddressOf cmbDistributor_ValueChanged
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GonReturnedBackManager_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Me.frmParent Is Nothing Then : Return : End If
            Me.Cursor = Cursors.WaitCursor
            frmParent.btnEditSPPB.Enabled = False
            'Me.btnFilteDate_Click(Me.btnFilteDate, New EventArgs())
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Me.ReadAccess() : Me.frmParent.cmbDistributor.ReadOnly = False : Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
