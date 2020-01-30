Public Class AlternateMessage
    Friend Event Acceptmessage(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            RaiseEvent Acceptmessage(sender, e)
            'Me.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class
