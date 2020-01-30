Public Class Form1

    Private Sub ItemPanel1_ButtonCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemPanel1.ButtonCheckedChanged
        Dim btn As DevComponents.DotNetBar.ButtonItem = CType(sender, DevComponents.DotNetBar.ButtonItem)
        btn.Checked = Not btn.Checked

    End Sub
End Class