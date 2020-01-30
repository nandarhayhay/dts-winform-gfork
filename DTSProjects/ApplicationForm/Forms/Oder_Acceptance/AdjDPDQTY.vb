Public Class AdjDPDQTY
    Public MaxQty As Decimal = 0
    Public Event lnkClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    Public Event CancelClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)

    Private Sub lnkOK_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkOK.LinkClicked
        RaiseEvent lnkClick(sender, e)
    End Sub

    Private Sub lnkCancel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCancel.LinkClicked
        RaiseEvent CancelClick(sender, e)
    End Sub
End Class