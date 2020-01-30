Imports System.Data
Imports System.Data.SqlClient
Namespace Kios
    Public Class ManageKios
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Public Function PopulateQuery(ByVal SearchBy As String, ByVal SearchString As String, _
        ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer) As DataView
            Try
                Dim Query As String = "SET NOCOUNT ON; SELECT TOP " & PageSize.ToString() & " * FROM Uv_Kios " _
                                     & " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " _
                                     & " FROM Uv_Kios WHERE (" & SearchBy & " LIKE '%' + @SearchString + '%') ORDER BY IDApp ASC)" _
                                     & " AND " & SearchBy & " LIKE '%' + @SearchString + '%' ORDER BY IDApp ASC;"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@SearchString", SqlDbType.VarChar, SearchString)
                'Me.AddParameter("@O_ROWCOUNT", SqlDbType.Int, ParameterDirection.InputOutput)
                Dim Dt As New DataTable("REGISTERED_KIOS") : Dt.Clear() : Me.OpenConnection()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(Dt) : Me.ClearCommandParameters()
                If SearchString = "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('Kios') AND (index_id=0 or index_id=1) ;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Else
                    Query = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM Uv_Kios WHERE " & SearchBy & " LIKE " _
                                              & " + '%' + @SearchString + '%' ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@SearchString", SqlDbType.VarChar, SearchString)
                End If
                Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters() : Me.CloseConnection()
                If (Dt.Rows.Count > 0) Then
                    ''Rowcount = CInt(Me.SqlCom.Parameters()("@O_ROWCOUNT").Value)
                    'Rowcount = CInt(Me.SqlDat.GetFillParameters().GetValue(0))
                Else : Return Nothing
                End If
                Return Dt.DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Throw ex
            End Try
        End Function
        Private Function ValueParam(ByVal row As DataRow, ByVal ColumnName As String) As Object
            Dim val As Object = Nothing
            If (IsDBNull(row(ColumnName))) Then
                val = DBNull.Value
            Else
                val = row(ColumnName)
            End If
            Return val
        End Function
        Public Sub InsertKios(ByVal DS As DataSet)
            Try
                If Not IsNothing(DS) Then
                    Me.GetConnection() : Me.OpenConnection() : Me.BeginTransaction()
                    Me.SqlCom = New SqlCommand() : Me.SqlCom.CommandType = CommandType.Text
                    Me.SqlCom.Connection = Me.SqlConn : Me.SqlCom.Transaction = Me.SqlTrans
                    'Me.SqlCom.CommandText = "SET NOCOUNT ON;" : Me.SqlCom.ExecuteScalar()
                    For i As Integer = 0 To DS.Tables(4).Rows.Count - 1
                        Me.SqlCom.CommandText = "SET NOCOUNT ON;IF NOT EXISTS(SELECT IDKios FROM Kios WHERE IDKios = @IDKios)" & vbCrLf _
                                              & " BEGIN " & vbCrLf _
                                              & " INSERT INTO Kios(IDKios,TerritoryCode,Kios_Name,Kios_Owner,NPWP,IDCard," _
                                              & "Address,State,City,District1,District2,PostalCode,PhoneNumber," _
                                              & "ContactMobile,CustomerType,Email,Fax,CreatedBy,CreatedDate)" _
                                              & " VALUES(@IDKios,@TerritoryCode,@Kios_Name,@Kios_Owner,@NPWP,@IDCard, " _
                                              & "@Address,@State,@City,@District1,@District2,@PostalCode,@PhoneNumber," _
                                              & "@ContactMobile,@CustomerType,@Email,@Fax,@CreatedBy,@CreatedDate)" & vbCrLf _
                                              & " END " & vbCrLf _
                                              & " ELSE " & vbCrLf _
                                              & " BEGIN " & vbCrLf _
                                              & " UPDATE Kios SET TerritoryCode = @TerritoryCode,Kios_Name = @Kios_Name,Kios_Owner = @Kios_Owner," _
                                              & " NPWP = @NPWP,IDCard = @IDCard,Address =@Address,State = @State," _
                                              & " City = @City,District1 = @District1,District2 = @District2,PostalCode = @PostalCode,PhoneNumber= @PhoneNumber," _
                                              & " ContactMobile = @ContactMobile,CustomerType = @CustomerType,Email = @Email,Fax = @Fax WHERE IDKios = @IDKios" & vbCrLf _
                                              & " END "
                        Me.AddParameter("@IDKios", SqlDbType.VarChar, DS.Tables(4).Rows(i)("IDKios"))
                        Me.AddParameter("@TerritoryCode", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "TerritoryCode"))
                        Me.AddParameter("@Kios_Name", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "Kios_Name"))
                        Me.AddParameter("@Kios_Owner", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "Kios_Owner"))
                        Me.AddParameter("@NPWP", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "NPWP"))
                        Me.AddParameter("@IDCard", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "IDCard"))
                        Me.AddParameter("@Address", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "Address"))
                        'Me.AddParameter("@Country", SqlDbType.VarChar, DS.Tables(4).Rows(i)("Country"))
                        Me.AddParameter("@State", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "State"))
                        Me.AddParameter("@City", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "City"))
                        Me.AddParameter("@District1", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "District1"))
                        Me.AddParameter("@District2", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "District2"))
                        Me.AddParameter("@PostalCode", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "PostalCode"))
                        Me.AddParameter("@PhoneNumber", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "PhoneNumber"))
                        Me.AddParameter("@ContactMobile", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "ContactMobile"))
                        Me.AddParameter("@CustomerType", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "CustomerType"))
                        Me.AddParameter("@Email", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "Email"))
                        Me.AddParameter("@Fax", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "Fax"))
                        Me.AddParameter("@CreatedBy", SqlDbType.VarChar, ValueParam(DS.Tables(4).Rows(i), "CreatedBy"))
                        Dim CreatedDate As Object = ValueParam(DS.Tables(4).Rows(i), "CreatedDate")
                        If (CreatedDate Is DBNull.Value Or CreatedDate Is Nothing) Then : Me.AddParameter("@CreatedDate", SqlDbType.DateTime, DBNull.Value)
                        Else : Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, Convert.ToDateTime(CreatedDate)) : End If
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next
                End If
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection()
                System.Windows.Forms.MessageBox.Show(ex.Message, "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            End Try
        End Sub
    End Class
End Namespace
