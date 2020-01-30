Public Class Quantity
    Public Event lnkClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    Public Event lnkCancelClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    Public Event QtyChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event SetAdjustment(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public dt As DataTable = Nothing

    Private Sub Quantity_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'check Adjustment DPD dimana Qty LeftQty > 0
        Me.CancelButton = Me.lnkCancel
        Me.AcceptButton = Me.lnkOK
    End Sub

    Private Sub lnkCancel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCancel.LinkClicked

        RaiseEvent lnkCancelClick(sender, e)
    End Sub

    Private Sub lnkOK_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkOK.LinkClicked
        RaiseEvent lnkClick(sender, e)
    End Sub
    Private Sub NumericEditBox1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericEditBox1.ValueChanged
        Try
            RaiseEvent QtyChanged(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkSetAdjustment_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSetAdjustment.CheckedChanged
        Try
            RaiseEvent SetAdjustment(sender, e)

        Catch ex As Exception

        End Try
    End Sub
End Class