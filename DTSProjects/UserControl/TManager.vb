Public Class TManager
    Public Event DtPicFromValueChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event ButonClick(ByVal sender As Object, ByVal e As EventArgs)
    Public Event CmbSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event TetxBoxKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
    Public Event TextBoxKeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
    Private Sub GridEX1_RootTableChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RootTableChanged
        Try
            If GridEX1.DataSource Is Nothing Then
                Me.btnGoFirst.Enabled = False
                Me.btnGoLast.Enabled = False
                Me.btnGoPrevios.Enabled = False
                Me.btnNext.Enabled = False
            End If
        Catch
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        RaiseEvent ButonClick(sender, e)
    End Sub

    Private Sub txtMaxRecord_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMaxRecord.KeyDown
        RaiseEvent TetxBoxKeyDown(sender, e)
    End Sub

    Private Sub cbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCategory.SelectedIndexChanged
        RaiseEvent CmbSelectedIndexChanged(sender, e)
    End Sub

    Private Sub btnGoFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoFirst.Click
        RaiseEvent ButonClick(sender, e)
    End Sub

    Private Sub btnGoPrevios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoPrevios.Click
        RaiseEvent ButonClick(sender, e)
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        RaiseEvent ButonClick(sender, e)
    End Sub

    Private Sub btnGoLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGoLast.Click
        RaiseEvent ButonClick(sender, e)
    End Sub

    Private Sub cbPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPageSize.SelectedIndexChanged
        RaiseEvent CmbSelectedIndexChanged(sender, e)
    End Sub
End Class
