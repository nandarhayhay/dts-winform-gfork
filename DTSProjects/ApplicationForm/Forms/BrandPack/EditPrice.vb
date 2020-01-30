Public Class EditPrice
    Friend Event OKClick(ByVal sender As Object, ByVal e As EventArgs)
    Friend StartDate As DateTime = NufarmBussinesRules.SharedClass.ServerDate()
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        RaiseEvent OKClick(sender, e)
    End Sub

    Private Sub EditPrice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.dtPicStartDate.MaxDate = StartDate
        Catch ex As Exception

        End Try
    End Sub
End Class