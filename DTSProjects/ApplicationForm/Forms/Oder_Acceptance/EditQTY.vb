Public Class EditQTY
    Friend Event OKClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    Friend Event CancelClick(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    Public OrgDecimalValue As Decimal = 0
    Friend Event QTYChanged(ByVal sender As Object, ByVal e As EventArgs)
    Private Sub lnkOK_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkOK.LinkClicked
        Try
            If CDec(Me.txtQTY.Value <= OrgDecimalValue) Then
                RaiseEvent OKClick(sender, e)
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub lnkCancel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCancel.LinkClicked
        Try
            RaiseEvent CancelClick(sender, e)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub EditQTY_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.AcceptButton = Me.lnkOK
            Me.CancelButton = Me.lnkCancel
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtQTY_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQTY.ValueChanged
        Try
            If CDec(Me.txtQTY.Value) > Me.OrgDecimalValue Then
                Me.txtQTY.Text = Me.txtQTY.Text.Remove(Me.txtQTY.Text.Length - 1, 1)
                Return
            End If
            RaiseEvent QTYChanged(sender, e)
        Catch ex As Exception

        End Try
    End Sub
End Class