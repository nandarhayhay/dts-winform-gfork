Public Class SavingChanges
    Public Event btnSaveClick(ByVal sender As Object, ByVal e As EventArgs)
    Public Event btnCloseClick(ByVal sender As Object, ByVal e As EventArgs)
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        RaiseEvent btnSaveClick(sender, e)
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        RaiseEvent btnCloseClick(sender, e)
    End Sub
End Class
