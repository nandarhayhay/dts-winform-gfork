Public Class ButtonSearch
    Public Event btnClick(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        RaiseEvent btnClick(sender, e)
    End Sub

    Private Sub ButtonSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.PictureBox1.Image = Me.ImageList1.Images(1)
    End Sub

    Private Sub PictureBox1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseHover
        Me.PictureBox1.Image = Me.ImageList1.Images(0)
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "Click this button" & vbCrLf & "Data in combo will be filtered based on the text on combo")
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.UseFading = True
        Me.ToolTip1.ToolTipTitle = "Filter Data."
        Me.ToolTip1.UseAnimation = True
        Me.ToolTip1.BackColor = Me.ParentForm.BackColor
        Me.ToolTip1.ToolTipIcon = ToolTipIcon.Info
    End Sub

    Private Sub PictureBox1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseLeave
        Me.PictureBox1.Image = Me.ImageList1.Images(1)
    End Sub
End Class
