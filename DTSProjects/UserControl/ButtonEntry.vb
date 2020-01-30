Public Class ButtonEntry
    Private m_btnAdd As Janus.Windows.EditControls.UIButton
    Private m_btnClose As Janus.Windows.EditControls.UIButton
    Private m_btnInsert As Janus.Windows.EditControls.UIButton
    Public Event btnClick(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        RaiseEvent btnClick(Me.btnInsert, e)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        RaiseEvent btnClick(Me.btnClose, e)
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        RaiseEvent btnClick(Me.btnAddNew, e)
    End Sub
End Class
