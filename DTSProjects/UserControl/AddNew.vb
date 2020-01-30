Public Class AddNew
    Friend Event PicClick(ByVal sender As Object, ByVal e As EventArgs)
    'Public Enum EditMode
    '    Add
    '    Edit
    'End Enum
    'Public Mode As EditMode
    Public isEditMode As Boolean = False
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Try
            RaiseEvent PicClick(sender, e)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub AddNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.PictureBox1.BackgroundImage = Me.ImageList1.Images(0)
        Me.PictureBox1.BackgroundImage.Tag = "Add"
    End Sub

    Private Sub PictureBox1_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseHover

        If Not Me.isEditMode Then
            Me.ToolTip1.Show("Click this button to add Item", Me.PictureBox1, 2500)
        Else
            Me.ToolTip1.Show("Click this button to edit Item", Me.PictureBox1, 2500)
        End If
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.UseFading = True
        Me.ToolTip1.ToolTipTitle = "Add or Edit Item."
        Me.ToolTip1.UseAnimation = True
        Me.ToolTip1.BackColor = Me.ParentForm.BackColor
        Me.ToolTip1.ToolTipIcon = ToolTipIcon.Info
    End Sub

    'Private Sub PictureBox1_BackgroundImageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.BackgroundImageChanged
    '    If Me.PictureBox1.BackgroundImage.Tag = "Add" Then
    '        Me.Mode = EditMode.Add
    '    Else
    '        Me.Mode = EditMode.Edit
    '    End If
    'End Sub
End Class
