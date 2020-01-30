Public Class Manager
    Public Event DtPicFromValueChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event ButonClick(ByVal sender As Object, ByVal e As EventArgs)
    Public Event CmbSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event TetxBoxKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
    Public Event TextBoxKeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
    Public Event DeleteRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs)

    Private Sub GridEX1_RootTableChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.RootTableChanged
        Try
            If GridEX1.DataSource Is Nothing Then
                Me.btnGoFirst.Enabled = False
                Me.btnGoLast.Enabled = False
                Me.btnGoPrevios.Enabled = False
                Me.btnNext.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub
   
    Public Function generateFromDate() As String
        If (Me.cmbFromYear.Text = "") Then
            Return ""
        End If
        If (Me.cmbFromMonth.Text = "") Then
            Return ""
        End If
        If (Me.cmbFromDate.Text = "") Then
            Return ""
        End If
        Return Me.cmbFromMonth.Text + "/" + Me.cmbFromDate.Text + "/" + Me.cmbFromYear.Text
    End Function
    Public Function generateUntilDate() As Object
        If (Me.cmbUntilYear.Text = "") Then
            Return ""
        End If
        If (Me.cmbUntilMonth.Text = "") Then
            Return ""
        End If
        If (Me.cmbUntilDate.Text = "") Then
            Return ""
        End If
        Return Me.cmbUntilMonth.Text + "/" + Me.cmbUntilDate.Text + "/" + Me.cmbUntilYear.Text
    End Function

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
    Private Sub cmbFromMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFromMonth.SelectedIndexChanged
        RaiseEvent DtPicFromValueChanged(sender, e)
    End Sub

    Private Sub cmbUntilMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUntilMonth.SelectedIndexChanged
        RaiseEvent DtPicFromValueChanged(sender, e)
    End Sub

    Private Sub cmbUntilYear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUntilYear.SelectedIndexChanged
        If (Me.cmbUntilYear.SelectedIndex <= -1) Then
            Throw New Exception("Can not find data" & vbCrLf & "Data only available from combo")
        End If
        RaiseEvent DtPicFromValueChanged(sender, e)
    End Sub

    Private Sub cmbUntilDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUntilDate.SelectedIndexChanged
        RaiseEvent DtPicFromValueChanged(sender, e)
    End Sub

    Private Sub cmbFromDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFromDate.SelectedIndexChanged
        RaiseEvent DtPicFromValueChanged(sender, e)
    End Sub

    Private Sub cmbFromYear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFromYear.SelectedIndexChanged
        If (Me.cmbFromYear.SelectedIndex <= -1) Then
            Throw New Exception("Can not find data" & vbCrLf & "Data only available from combo")
        End If
        RaiseEvent DtPicFromValueChanged(sender, e)
    End Sub

   
    Private Sub txtMaxRecord_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxRecord.KeyPress
        RaiseEvent TextBoxKeyPress(sender, e)
    End Sub

    Private Sub GridEX1_DeletingRecord(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles GridEX1.DeletingRecord
        Try
            If MessageBox.Show("Are you sure you want to delete data ?" & vbCrLf & "Operation can not be undone", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                e.Cancel = True
                Return
            End If
            RaiseEvent DeleteRow(sender, e)

        Catch ex As Exception

        End Try
    End Sub
End Class
