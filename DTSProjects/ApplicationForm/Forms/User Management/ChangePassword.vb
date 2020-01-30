Public Class ChangePassword

    Private Sub XpLoginEntry1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XpLoginEntry1.Click
        Me.EditBox2.Visible = Not Me.EditBox2.Visible
        If Me.EditBox2.Visible = False Then
            Me.XpLoginEntry1.HelpString = ""
        Else
            Me.XpLoginEntry1.HelpString = "Please type your new password"
        End If
    End Sub

    'Private Sub XpLoginEntry2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XpLoginEntry2.Click
    '    Me.XpLoginEntry2.Visible = Not Me.XpLoginEntry2.Visible
    'End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub XpLoginEntry2_EnterPassword(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XpLoginEntry2.EnterPassword
        Me.XpLoginEntry2.HelpString = "Please type confirm passsword"
    End Sub

    Private Sub XpLoginEntry2_Login(ByVal sender As System.Object, ByVal e As SteepValley.Windows.Forms.LoginEventArgs) Handles XpLoginEntry2.Login
        Try
            If Me.EditBox2.Text = "" Or e.Password = "" Then
                Me.ShowMessageInfo("Please Enter Password and Match password") : Return
            End If

            If Me.EditBox2.Text <> e.Password.ToString() Then
                Me.lblInfo.Text = "Password didn't match"
                Return
            End If
            'chek user_id
            Dim User_id As String = NufarmBussinesRules.User.UserLogin.UserName
            Dim Password As String = e.Password.ToString()
            Dim clsLogin As New NufarmBussinesRules.User.Login
            clsLogin.ChangePassword(User_id, Password)
            Me.ShowMessageInfo("Password Succesfully changed.")
            Me.Close()
        Catch ex As Exception
            Me.ShowMessageError(ex.Message)
            Me.LogMyEvent(ex.Message, Me.Name + "_ XpLoginEntry2_Login")
        End Try
    End Sub

    Private Sub XpLoginEntry2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XpLoginEntry2.Leave
        Try
            Me.XpLoginEntry2.HelpString = ""
        Catch ex As Exception

        End Try
    End Sub
End Class
