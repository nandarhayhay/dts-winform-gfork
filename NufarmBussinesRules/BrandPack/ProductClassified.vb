Imports System.Data
Imports System.Data.SqlClient
Namespace Brandpack
    Public Class ProductClassified
        Inherits Brand
        Public Sub New()
            MyBase.New()
        End Sub
        Public Function getBrand(ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                      "SELECT BRAND_ID,BRAND_NAME FROM BRND_BRAND WHERE IsActive = 1 AND IsObsolete = 0;"

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                OpenConnection()
                Dim tbl As New DataTable("BRAND")
                tbl.Clear()
                setDataAdapter(SqlCom).Fill(tbl)
                ClearCommandParameters()
                If mustCloseConnection Then
                    CloseConnection()
                End If
                Return tbl
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub SaveBrandClass(ByVal tblBrandClass As DataTable, ByVal mustCloseConnection As Boolean)
            Try
                Dim InsertedRows() As DataRow = tblBrandClass.Select("", "", DataViewRowState.Added)
                Dim UpdatedRows() As DataRow = tblBrandClass.Select("", "", DataViewRowState.ModifiedOriginal)
                Dim DeletedRows() As DataRow = tblBrandClass.Select("", "", DataViewRowState.Deleted)
                Dim CommandInsert As SqlCommand = Nothing, CommandUpdate As SqlCommand = Nothing, CommandDelete As SqlCommand = Nothing
                If ((InsertedRows.Length > 0) Or (UpdatedRows.Length > 0) Or (DeletedRows.Length > 0)) Then
                    OpenConnection()
                    Me.BeginTransaction()
                    Me.SqlDat = New SqlDataAdapter()

                Else
                    Throw New Exception("NO Changes have occured" & vbCrLf & "No Need Saving")
                End If
                If InsertedRows.Length > 0 Then
                    CommandInsert = Me.SqlConn.CreateCommand()
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "INSERT INTO PRODUCT_CLASS (BRAND_CLASS_ID,BRAND_ID,CLASS_ID,CLASS_NAME, INACTIVE, CREATE_BY, CREATE_DATE)" & vbCrLf & _
                            " VALUES(@BRAND_CLASS_ID,@BRAND_ID,@CLASS_ID,@CLASS_NAME, @INACTIVE, @CREATE_BY, @CREATE_DATE) ;"
                    CommandInsert.Transaction = Me.SqlTrans : CommandInsert.CommandType = CommandType.Text : CommandInsert.CommandText = Query
                    With CommandInsert
                        .Parameters.Add("@BRAND_CLASS_ID", SqlDbType.VarChar, 20, "BRAND_CLASS_ID")
                        .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 20, "BRAND_ID")
                        .Parameters.Add("@CLASS_ID", SqlDbType.VarChar, 10, "CLASS_ID")
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .Parameters.Add("@CLASS_NAME", SqlDbType.VarChar, 100, "CLASS_NAME")
                        .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50, "CREATE_BY")
                        .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_DATE")
                        Me.SqlDat.InsertCommand = CommandInsert
                        Me.SqlDat.Update(InsertedRows) : CommandInsert.Parameters.Clear()
                    End With
                End If
                If UpdatedRows.Length > 0 Then
                    CommandUpdate = SqlConn.CreateCommand()
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " UPDATE PRODUCT_CLASS SET INACTIVE = @INACTIVE WHERE BRAND_CLASS_ID = @BRAND_CLASS_ID;"
                    With CommandUpdate
                        .CommandText = Query
                        .CommandType = CommandType.Text
                        .Transaction = Me.SqlTrans
                        .Parameters.Add("@INACTIVE", SqlDbType.Bit, 0, "INACTIVE")
                        .Parameters.Add("@BRAND_CLASS_ID", SqlDbType.VarChar, 20, "BRAND_CLASS_ID")
                        .Parameters()("@BRAND_CLASS_ID").SourceVersion = DataRowVersion.Original
                        Me.SqlDat.UpdateCommand = CommandUpdate
                        Me.SqlDat.Update(UpdatedRows)
                        CommandUpdate.Parameters.Clear()
                    End With
                End If
                If DeletedRows.Length > 0 Then
                    CommandDelete = SqlConn.CreateCommand()
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " IF EXISTS(SELECT BRAND_CLASS_ID FROM PRODUCT_CLASS WHERE BRAND_CLASS_ID = @BRAND_CLASS_ID) " & vbCrLf & _
                    " BEGIN DELETE FROM PRODUCT_CLASS WHERE BRAND_CLASS_ID = @BRAND_CLASS_ID ; END"
                    With CommandDelete
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Transaction = SqlTrans
                        .Parameters.Add("@BRAND_CLASS_ID", SqlDbType.VarChar, 20, "BRAND_CLASS_ID")
                        .Parameters()("@BRAND_CLASS_ID").SourceVersion = DataRowVersion.Original
                        SqlDat.DeleteCommand = CommandDelete
                        SqlDat.Update(DeletedRows)
                        CommandDelete.Parameters.Clear()
                    End With
                End If
                Me.CommiteTransaction()
                If mustCloseConnection Then
                    CloseConnection()
                End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function getBrandClass(ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT * FROM PRODUCT_CLASS ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                OpenConnection()
                Dim tbl As New DataTable("BRAND")
                tbl.Clear()
                setDataAdapter(SqlCom).Fill(tbl)
                ClearCommandParameters()
                If mustCloseConnection Then
                    CloseConnection()
                End If
                Return tbl
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function HasExistsBrandClassID(ByVal BrandClassID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT TOP 1 BRAND_CLASS_ID FROM PRODUCT_CLASS WHERE BRAND_CLASS_ID = @BRAND_CLASS_ID;"
                CreateCommandSql("", Query)
                AddParameter("@BRAND_CLASS_ID", SqlDbType.VarChar, BrandClassID, 20)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then
                    CloseConnection()
                End If
                If ((Not IsNothing(retval)) And (Not IsDBNull(retval))) Then
                    Return True
                End If
                Return False
            Catch ex As Exception

            End Try
        End Function
    End Class
End Namespace

