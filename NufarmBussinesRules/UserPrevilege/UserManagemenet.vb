Imports System.Data.SqlClient
Namespace User
    Public Class UserManagemenet
        Inherits NufarmBussinesRules.User.Login
        Private m_UserView As DataView
        Public USER_ID As String
        Public Password As String
        Private m_dsForm As DataSet
        Private m_DsViewUser As DataSet
        Private m_FormTable As DataTable
        Private Query As String = ""
        Public Function IsLogin(ByVal USER_ID As String) As Boolean
            Try
                If CBool(Me.ExecuteScalar("", "SELECT STATUS FROM SYST_USERNAME WHERE USER_ID = '" & USER_ID & "'")) = True Then
                    Return True
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function IsExisted(ByVal UserID As String) As Boolean
            Try
                If CInt(Me.ExecuteScalar("", "SELECT COUNT(USER_ID) FROM SYST_USERNAME WHERE USER_ID = '" & UserID & "'")) > 0 Then
                    Return True
                End If
            Catch ex As Exception

            End Try

        End Function
        Public Function CreateViewUser() As DataSet
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_GetViewUser", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_GetViewUser")
                End If
                OpenConnection()
                Dim tblUser As New DataTable("LIST_USER")
                tblUser.Clear()
                setDataAdapter(Me.SqlCom).Fill(tblUser)
                'Me.FillDataTable(tblUser)
                Me.m_DsViewUser = New DataSet("VIEW_USER")
                Me.m_DsViewUser.Clear()
                Me.m_DsViewUser.Tables.Add(tblUser)
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                       " SELECT SYST_PRIVILEGE.[USER_ID],SYST_MENU.FORM_ID,SYST_MENU.FORM_NAME,SYST_PRIVILEGE.ALLOW_VIEW,SYST_PRIVILEGE.ALLOW_INSERT,SYST_PRIVILEGE.ALLOW_DELETE," & _
                                "SYST_PRIVILEGE.ALLOW_UPDATE FROM SYST_MENU,SYST_PRIVILEGE WHERE SYST_PRIVILEGE.FORM_ID = SYST_MENU.FORM_ID"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.SearcData("", "SELECT SYST_PRIVILEGE.[USER_ID],SYST_MENU.FORM_ID,SYST_MENU.FORM_NAME,SYST_PRIVILEGE.ALLOW_VIEW,SYST_PRIVILEGE.ALLOW_INSERT,SYST_PRIVILEGE.ALLOW_DELETE," & _
                '                "SYST_PRIVILEGE.ALLOW_UPDATE FROM SYST_MENU,SYST_PRIVILEGE WHERE SYST_PRIVILEGE.FORM_ID = SYST_MENU.FORM_ID")
                Dim tblPriviledge As New DataTable("PRIVILEDGE") : tblPriviledge.Clear() : setDataAdapter(Me.SqlCom).Fill(tblPriviledge)
                'Me.baseChekTable.TableName = "PRIVILEDGE"
                Me.m_DsViewUser.Tables.Add(tblPriviledge)
                Me.m_UserView = tblUser.DefaultView()
                Me.m_UserView.Sort = "USER_ID"
                Me.AddrelationToDataSet(Me.m_DsViewUser, Me.m_DsViewUser.Tables(0).Columns("USER_ID"), Me.m_DsViewUser.Tables(1).Columns("USER_ID"), "User_Privilege")
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_DsViewUser
        End Function
        Public Sub UpdateSystPriviledge(ByVal UserID As String, ByVal INActive As Boolean, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " UPDATE SYST_USERNAME SET INActive = @INActive WHERE USER_ID = @UserID "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("y", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@UserID", SqlDbType.VarChar, UserID)
                Me.AddParameter("@INActive", SqlDbType.Bit, INActive)
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Sub UpdateSystPriviledge1(ByVal UserID As String, ByVal IsAdmin As Boolean, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " UPDATE SYST_USERNAME SET ISADMIN = @IsAdmin WHERE USER_ID = @UserID "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("y", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@UserID", SqlDbType.VarChar, UserID)
                Me.AddParameter("@IsAdmin", SqlDbType.Bit, IsAdmin)
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Function CreateViewDSPrivilege(ByVal USER_ID As String) As DataSet
            Try
                Me.FilDataSet("", "SELECT * FROM SYST_PRIVILEGE WHERE [USER_ID] = '" & USER_ID & "'", "USER_PRIVILEGE")
                Me.m_dsForm = Me.baseDataSet
                Me.m_UserView = Me.m_dsForm.Tables(0).DefaultView()
                Me.m_UserView.RowStateFilter = DataViewRowState.CurrentRows
                Me.m_UserView.Sort = "FORM_ID"
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_dsForm
        End Function
        Public Function CreateFormTable() As DataTable
            Try
                Me.CreateCommandSql("", "SELECT FORM_ID,FORM_NAME FROM SYST_MENU")
                Dim tblForm As New DataTable("FORM")
                tblForm.Clear()
                Me.FillDataTable(tblForm)
                Me.m_FormTable = tblForm
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_FormTable
        End Function
        Public Function CreateDSPrivilege() As DataView
            Try
                Me.FilDataSet("", "SELECT * FROM SYST_PRIVILEGE WHERE [USER_ID] = ''", "USER_PRIVILEGE")
                Me.m_dsForm = Me.baseDataSet
                Me.m_UserView = Me.m_dsForm.Tables(0).DefaultView()
                Me.m_UserView.RowStateFilter = DataViewRowState.CurrentRows
                Me.m_UserView.Sort = "FORM_ID"
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_UserView
        End Function
        'Public ReadOnly Property Privile() As DataView
        '    Get
        '        Return Me.m_UserView
        '    End Get
        'End Property
        Public ReadOnly Property GetDataset() As DataSet
            Get
                Return Me.m_dsForm
            End Get
        End Property
        Public ReadOnly Property TableForm() As DataTable
            Get
                Return Me.m_FormTable
            End Get
        End Property
        Public ReadOnly Property ViewUser() As DataView
            Get
                Return Me.m_UserView
            End Get
        End Property
        Public ReadOnly Property GetDataSet_1() As DataSet
            Get
                Return Me.m_DsViewUser
            End Get
        End Property
        Public Sub Delete_Priviledge(ByVal USER_ID As String, ByVal FORM_ID As String)
            Try
                Me.CreateCommandSql("", "DELETE FROM SYST_PRIVILEGE WHERE USER_ID = '" & USER_ID & "' AND FORM_ID = '" & FORM_ID & "'")
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Sub SaveUser(ByVal SaveType As String, ByVal dt As DataTable, ByVal dsHaschanged As Boolean)
            Try
                Select Case SaveType

                    Case "Save"
                        Me.CreateCommandSql("Sp_Insert_SYST_USERNAME", "")
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30),

                        'Me.AddParameter("@O_MESSAGE", SqlDbType.VarChar, ParameterDirection.Output) ' VARCHAR(100)OUTPUT
                        Me.SqlCom.Parameters.Add("@O_MESSAGE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output
                        'Me.SqlCom.Parameters()("@O_MESSAGE").Size = 100
                    Case "Update"
                        Me.CreateCommandSql("Sp_Update_SYST_USERNAME", "")
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30),
                End Select
                Me.AddParameter("@USER_ID", SqlDbType.VarChar, Me.USER_ID, 15) ' VARCHAR(15),
                Me.AddParameter("@PASSWORD", SqlDbType.VarChar, MyBase.Enkrip(Me.Password), 30) ' VARCHAR(30),
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteNonQuery()
                If SaveType = "Save" Then
                    Dim Message As String = Me.SqlCom.Parameters()("@O_MESSAGE").Value.ToString()
                    If Message <> "" Then
                        Me.RollbackTransaction()
                        Me.CloseConnection()
                        Me.ClearCommandParameters()
                        Throw New System.Exception(Message)
                    End If
                End If
                If dsHaschanged = True Then
                    Me.SqlCom.CommandText = "SELECT * FROM SYST_PRIVILEGE"
                    Me.SqlCom.CommandType = CommandType.Text
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                    Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                    Me.SqlDat.Update(dt)
                End If
                Me.CommiteTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
            Catch ex As DBConcurrencyException
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw New System.Exception(Me.MessageDBConcurency)
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Sub DeleteUser(ByVal User_ID As String)
            Try
                If Me.IsLogin(User_ID) = True Then
                    Throw New System.Exception("User is currently logging")
                End If
                Me.CreateCommandSql("", "DELETE FROM SYST_PRIVILEGE WHERE USER_ID = '" & User_ID & "'")
                Me.OpenConnection()

                'Me.AddParameter("@USER_ID", SqlDbType.VarChar, User_ID, 30) ' VARCHAR(30),
                'Me.AddParameter("@O_MESSAGE", SqlDbType.VarChar, ParameterDirection.Output) ' VARCHAR(100) OUTPUT
                'Me.SqlCom.Parameters()("@O_MESSAGE").Size = 100

                'Me.BeginTransaction()
                'Me.SqlCom.Transaction = Me.SqlTrans
                'Me.SqlCom.set()
                Me.SqlCom.ExecuteNonQuery()
                Me.SqlCom.CommandText = "DELETE FROM SYST_USERNAME WHERE USER_ID = '" & User_ID & "'"
                Me.SqlCom.CommandType = CommandType.Text
                Me.SqlCom.ExecuteNonQuery()
                'If Me.SqlCom.Parameters()("@O_MESSAGE").Value.ToString <> "" Then
                '    Me.RollbackTransaction()
                '    Me.CloseConnection()
                '    Me.ClearCommandParameters()
                '    Throw New System.Exception(Me.SqlCom.Parameters()("@O_MESSAGE").Value.ToString())
                'End If
                'Me.CommiteTransaction()
                Me.CloseConnection()
                'Me.ClearCommandParameters()
            Catch ex As DBConcurrencyException
                'Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw New System.Exception(Me.MessageDBConcurency)
            Catch ex As Exception
                'Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Overloads Sub Dispose()
            If Not IsNothing(Me.m_FormTable) Then
                Me.m_FormTable.Dispose()
                Me.m_FormTable = Nothing
            End If
            If Not IsNothing(Me.m_dsForm) Then
                Me.m_dsForm.Dispose()
                Me.m_dsForm = Nothing
            End If
            If Not IsNothing(Me.m_DsViewUser) Then
                Me.m_DsViewUser.Dispose()
                Me.m_DsViewUser = Nothing
            End If
            If Not IsNothing(Me.m_UserView) Then
                Me.m_UserView.Dispose()
                Me.m_UserView = Nothing
            End If
        End Sub
    End Class

End Namespace
