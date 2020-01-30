Public Class FrmSetOtherStatus
    Friend Event OKClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    Friend Event CancelClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    Friend FrmParent As Form
    Friend RefControl As ReferencedControl = ReferencedControl.None
    Friend tblStatus As DataTable
    Private IsLoadingRow As Boolean = False
    Public Enum ReferencedControl
        Grid
        Combo
        None
    End Enum
    Private Sub FrmSetOtherStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            IsLoadingRow = True
            If Not IsNothing(Me.tblStatus) Then
                Me.GridEX1.SetDataBinding(tblStatus.DefaultView(), "")
            End If
            IsLoadingRow = False
        Catch ex As Exception

            IsLoadingRow = False
        End Try
    End Sub

    Private Sub lnkOK_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkOK.LinkClicked
        RaiseEvent OKClick(sender, e)
    End Sub

    Private Sub lnkCancel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCancel.LinkClicked
        RaiseEvent CancelClick(sender, e)
    End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        If Me.GridEX1.DataSource Is Nothing Then : Return : End If
        If Me.GridEX1.RecordCount <= 0 Then : Return : End If
        Me.GridEX1.Update()
    End Sub
End Class