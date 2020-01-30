Public Class frmManagePM
    Private DV As DataView = Nothing
    Private Sub GridEX1_AddingRecord(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GridEX1.AddingRecord
        Try
            If (Me.GridEX1.GetValue("UserName") Is DBNull.Value) Or (Me.GridEX1.GetValue("UserName") Is Nothing) Then
                Me.GridEX1.CancelCurrentEdit() : Me.GridEX1.MoveToNewRecord()
            Else
                'Me.GridEX1.SetValue("TypeApp", "PM")
                Me.GridEX1.SetValue("CreatedBy", NufarmBussinesRules.User.UserLogin.UserName)
                Me.GridEX1.SetValue("CreatedDate", NufarmBussinesRules.SharedClass.ServerDate)
            End If
            'Me.GridEX1.UpdateData()M
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmManagePM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim Dt As New DataTable("T_PM") : Dt.Clear()
            With Dt
                .Columns.Add(New DataColumn("UserName", Type.GetType("System.String")))
                .Columns.Add(New DataColumn("CreatedBy", Type.GetType("System.String")))
                .Columns.Add(New DataColumn("CreatedDate", Type.GetType("System.String")))
            End With
            DV = Dt.DefaultView
            DV.RowStateFilter = Data.DataViewRowState.CurrentRows
            Me.GridEX1.SetDataBinding(DV, "")
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Overloads Function ShowDialog(ByRef Dv As DataView) As DialogResult
        Dim DgResult As DialogResult = Windows.Forms.DialogResult.Cancel
        If (Me.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Dv = DirectCast(Me.GridEX1.DataSource, DataView)
            If (Dv.Count > 0) Then
                DgResult = Windows.Forms.DialogResult.OK
            End If
        End If
        Return DgResult
    End Function
End Class