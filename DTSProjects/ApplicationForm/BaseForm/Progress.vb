Public Class Progress
    Public Overloads Sub Show(ByVal Message As String)
        Application.DoEvents()
        Me.Label1.Text = Message : Me.Show() : Me.PictureBox1.Refresh()
        Me.Refresh()
        Application.DoEvents()
    End Sub
End Class