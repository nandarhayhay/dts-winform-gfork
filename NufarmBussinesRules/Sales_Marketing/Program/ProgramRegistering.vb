Imports System.Data
Namespace Program
    Public Class ProgramRegistering
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Public PROGAM_ID As String
        Public PROGRAM_NAME As String
        Public PROGRAM_START_DATE As Object
        Public PROGRAM_END_DATE As Object
        Protected m_ViewProgramRegistering As DataView
        Protected drv As DataRowView
        'Public Function ViewProgramRegistering() As DataView
        '    Me.FillDataTable("Sp_Select_ProgramRegistering", "", "T_Program")
        '    Me.m_ViewProgramRegistering = Me.baseDataTable.DefaultView
        '    Return Me.m_ViewProgramRegistering
        'End Function
        Public Function DeleteProgram(ByVal PROGRAM_ID As String) As Integer
            Try
                Me.GetConnection()
                Me.DeleteData("Sp_Delete_MRKT_MARKETING_PROGRAM", "")
                Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, PROGRAM_ID, 15)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
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
        End Function
        Public Function ProgramHasReferencedData(ByVal PROGRAM_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_REFERENCED_MRKT_MARKETING_PROGRAM", "")
                Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, PROGRAM_ID, 15)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return False
        End Function
        Public Function SaveToDataView(ByVal SaveType As String) As DataView
            Try
                With Me.m_ViewProgramRegistering
                    Select Case SaveType
                        Case "Add"
                            drv = .AddNew()
                            drv("PROGRAM_ID") = Me.PROGAM_ID
                            drv("PROGRAM_NAME") = Me.PROGRAM_NAME
                            drv("START_DATE") = Me.PROGRAM_START_DATE
                            drv("END_DATE") = Me.PROGRAM_END_DATE
                            drv.EndEdit()
                        Case "Edit"
                            Dim index As Integer = Me.m_ViewProgramRegistering.Find(Me.PROGAM_ID)
                            If index <> -1 Then
                                Me.m_ViewProgramRegistering(index)("PROGRAM_NAME") = Me.PROGRAM_NAME
                                Me.m_ViewProgramRegistering(index)("START_DATE") = Me.PROGRAM_START_DATE
                                Me.m_ViewProgramRegistering(index)("END_DATE") = Me.PROGRAM_END_DATE
                                Me.m_ViewProgramRegistering(index).EndEdit()
                            End If
                    End Select
                End With
            Catch ex As Exception
                Throw ex
            End Try
            Return Me.m_ViewProgramRegistering
        End Function
        Public Function CreateViewProgramRegistering(ByVal PROGRAM_ID As String, ByVal RF As Data.DataViewRowState) As DataView
            Try
                Me.SearcData("", "SELECT PROGRAM_ID,PROGRAM_NAME,START_DATE,END_DATE FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID  = '" + PROGRAM_ID + "'")
                Me.m_ViewProgramRegistering = Me.baseChekTable.DefaultView()
                Me.m_ViewProgramRegistering.Sort = "PROGRAM_ID"
                Me.m_ViewProgramRegistering.RowStateFilter = RF
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewProgramRegistering
        End Function
        Public Function PROGRAMD_ID_HASEXISTED(ByVal PROGRAM_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_Existing_MRKT_MARKETING_PROGRAM", "")
                Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, PROGRAM_ID, 15)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return False
        End Function
        Public Sub GetdataRowByProgramID(ByVal PROGRAM_ID As String)
            Try
                Me.ExecuteReader("", "SELECT PROGRAM_NAME,START_DATE,END_DATE FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID = '" + PROGRAM_ID + "'")
                While Me.SqlRe.Read()
                    Me.PROGRAM_NAME = Me.SqlRe("PROGRAM_NAME").ToString()
                    Me.PROGRAM_START_DATE = Me.SqlRe("START_DATE")
                    Me.PROGRAM_END_DATE = Me.SqlRe("END_DATE")
                End While
                Me.SqlRe.Close()
                Me.CloseConnection()
            Catch ex As Exception
                Me.SqlRe.Close()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Public ReadOnly Property ViewProgramRegistering() As DataView
            Get
                Return Me.m_ViewProgramRegistering
            End Get
        End Property
        Public Function SaveProgram(ByVal SaveType As String) As Integer
            Try
                Me.GetConnection()
                Select Case SaveType
                    Case "Save"
                        Me.InsertData("Sp_Insert_MRKT_MARKETING_PROGRAM", "")
                        Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, Me.PROGAM_ID, 15) ' VARCHAR(15),
                        Me.AddParameter("@PROGRAM_NAME", SqlDbType.VarChar, Me.PROGRAM_NAME, 30) ' VARCHAR(30),
                        Me.AddParameter("@START_DATE", SqlDbType.DateTime, Me.PROGRAM_START_DATE) ' DATETIME,
                        Me.AddParameter("@END_DATE", SqlDbType.DateTime, Me.PROGRAM_END_DATE) '  DATETIME,
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) 'VARCHAR(30),
                        'Me.AddParameter("@RETURN_VALUE", SqlDbType.VarChar, ParameterDirection.ReturnValue)
                    Case "Update"
                        Me.UpdateData("Sp_Update_MRKT_MARKETING_PROGRAM", "")
                        Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, Me.PROGAM_ID, 15) ' VARCHAR(15),
                        Me.AddParameter("@PROGRAM_NAME", SqlDbType.VarChar, Me.PROGRAM_NAME, 30) ' VARCHAR(30),
                        Me.AddParameter("@START_DATE", SqlDbType.DateTime, Me.PROGRAM_START_DATE) ' DATETIME,
                        Me.AddParameter("@END_DATE", SqlDbType.DateTime, Me.PROGRAM_END_DATE) ' DATETIME,
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)

                End Select
                Me.OpenConnection() : Me.BeginTransaction()
                Me.SqlCom.Transaction = Me.SqlTrans : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                'CHEK DATA IN MRKT_BRANDPACK 
                Me.SqlCom.CommandType = CommandType.Text
                Dim Query As String = "SET NOCOUNT ON;IF EXISTS(SELECT PROGRAM_ID FROM MRKT_BRANDPACK WHERE PROGRAM_ID = @PROGRAM_ID)" & vbCrLf & _
                                      " BEGIN " & vbCrLf & _
                                      " UPDATE MRKT_BRANDPACK SET START_DATE = @START_DATE,END_DATE = @END_DATE " & vbCrLf & _
                                      " WHERE PROGRAM_ID = @PROGRAM_ID " & vbCrLf & _
                                      " END "
                Me.SqlCom.CommandText = Query
                Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, Me.PROGAM_ID, 15)
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, Me.PROGRAM_START_DATE)
                Me.AddParameter("@END_DATE", SqlDbType.DateTime, Me.PROGRAM_END_DATE)
                Me.SqlCom.ExecuteScalar()
                Me.SqlCom.CommandText = "SET NOCOUNT ON;IF EXISTS(SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_ID " & vbCrLf & _
                                        " = ANY(SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK WHERE PROGRAM_ID = @PROGRAM_ID)) " & vbCrLf & _
                                        " BEGIN " & vbCrLf & _
                                        " UPDATE MRKT_BRANDPACK_DISTRIBUTOR SET END_DATE = @END_DATE,START_DATE = @START_DATE " & vbCrLf & _
                                        " WHERE PROG_BRANDPACK_ID = ANY(SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK WHERE PROGRAM_ID = @PROGRAM_ID)" & vbCrLf & _
                                        " END "
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CommiteTransaction()
                Me.CloseConnection()
                If Not IsNothing(Me.m_ViewProgramRegistering) Then
                    If SaveType = "Save" Then
                        Me.SaveToDataView("Add")
                    Else
                        Me.SaveToDataView("Edit")
                    End If
                End If
                Return 1
                'If Me.ExecuteNonQuery() > 0 Then

                'End If
            Catch EX As DBConcurrencyException
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw EX
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
            Return 0
        End Function
        Public Overloads Sub dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewProgramRegistering) Then
                Me.m_ViewProgramRegistering.Dispose()
                Me.m_ViewProgramRegistering = Nothing
            End If
        End Sub

        Public Sub New()
            Me.PROGAM_ID = ""
            Me.PROGRAM_END_DATE = DBNull.Value
            Me.PROGRAM_START_DATE = DBNull.Value
        End Sub
    End Class
End Namespace

