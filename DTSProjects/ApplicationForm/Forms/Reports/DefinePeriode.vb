Public Class DefinePeriode
    Public Event ApplyButtonClick(ByVal sender As Object, ByVal e As EventArgs)

    Private Sub btnAplyRange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAplyRange.Click
        RaiseEvent ApplyButtonClick(sender, e)
    End Sub
End Class