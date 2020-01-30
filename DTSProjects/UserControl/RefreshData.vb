Public Class RefreshData
    Public Event BtnClick(ByVal sender As Object, ByVal e As EventArgs)

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        RaiseEvent BtnClick(sender, e)
    End Sub
End Class
