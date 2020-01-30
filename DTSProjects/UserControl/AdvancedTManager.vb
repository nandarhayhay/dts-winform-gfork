Public Class AdvancedTManager
    Public Event DtPicFromValueChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event ButonClick(ByVal sender As Object, ByVal e As EventArgs)
    Public Event CmbSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event TetxBoxKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
    Public Event TextBoxKeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
    Public Event ChangesCriteria(ByVal sender As Object, ByVal e As EventArgs)
    Public Event GridCurrentCell_Changed(ByVal sender As Object, ByVal e As EventArgs)
    Public Event DeleteGridRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs)
    Public Event CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
    Public Event gridKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Event ChekAllDataChandge(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event GridDoubleClicked(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private Sub GridEX1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        Try
            RaiseEvent gridKeyDown(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_RootTableChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RootTableChanged
        If GridEX1.DataSource Is Nothing Then
            Me.btnGoFirst.Enabled = False
            Me.btnGoLast.Enabled = False
            Me.btnGoPrevios.Enabled = False
            Me.btnNext.Enabled = False
        End If
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
    Private Sub btnCriteria_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCriteria.TextChanged
        RaiseEvent ChangesCriteria(sender, e)
    End Sub

    Private Sub GridEX1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.CurrentCellChanged
        Try
            RaiseEvent GridCurrentCell_Changed(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            RaiseEvent DeleteGridRecord(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellUpdated
        Try
            RaiseEvent CellUpdated(sender, e)
        Catch ex As Exception

        End Try
    End Sub

 
    Private Sub CHKSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKSelectAll.CheckedChanged
        Try
            RaiseEvent ChekAllDataChandge(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        RaiseEvent GridDoubleClicked(sender, e)
    End Sub
End Class
