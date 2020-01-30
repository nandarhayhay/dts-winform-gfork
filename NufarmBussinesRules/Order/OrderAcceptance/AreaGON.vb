Imports System.Data.SqlClient
Imports System.Data
Namespace OrderAcceptance
    Public Class AreaGON
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private Query As String
        Public Sub SaveData(ByVal DS As DataSet, ByVal mustReloadData As Boolean, ByRef tblGonArea As DataTable, ByVal mustCloseConnection As Boolean)
            Try
                Dim InsertedRows() As DataRow = Nothing, UpdatedRows() As DataRow = Nothing, DeletedRows() As DataRow = Nothing
                InsertedRows = DS.Tables(0).Select("", "", DataViewRowState.Added)
                UpdatedRows = DS.Tables(0).Select("", "", DataViewRowState.ModifiedOriginal)
                DeletedRows = DS.Tables(0).Select("", "", DataViewRowState.Deleted)
                If InsertedRows.Length <= 0 And UpdatedRows.Length <= 0 And DeletedRows.Length <= 0 Then
                    Throw New Exception("NO need saving" & vbCrLf & "no change's been made")
                End If
                Me.OpenConnection() : Me.BeginTransaction()
                Me.SqlDat = New SqlDataAdapter()
                If InsertedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " INSERT INTO GON_AREA(GON_ID_AREA,AREA,DAYS_RECEIPT,TRANSPORTATION_BY,CreatedBy,CreatedDate) " & vbCrLf & _
                    " VALUES(@GON_ID_AREA,@AREA,@DAYS_RECEIPT,@TRANSPORTATION_BY,@CreatedBy,@CreatedDate) ;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.SqlCom.Parameters.AddRange(getSqlParameters())
                    Me.SqlCom.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                    Me.SqlCom.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 100, "CreatedDate")
                    Me.SqlCom.Transaction = Me.SqlTrans
                    Me.SqlDat.InsertCommand = Me.SqlCom
                    Me.SqlDat.Update(InsertedRows) : Me.SqlCom.Parameters.Clear()
                End If
                If UpdatedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "IF EXISTS(SELECT GON_ID_AREA FROM GON_AREA WHERE GON_ID_AREA = @GON_ID_AREA) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " UPDATE GON_AREA SET AREA = @AREA,DAYS_RECEIPT = @DAYS_RECEIPT,TRANSPORTATION_BY = @TRANSPORTATION_BY, " & vbCrLf & _
                            " ModifiedBy = @ModifiedBy,ModifiedDate = @ModifiedDate WHERE GON_ID_AREA = @GON_ID_AREA ;" & vbCrLf & _
                            " END "
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.SqlCom.Parameters.AddRange(getSqlParameters())
                    Me.SqlCom.Parameters()("@GON_ID_AREA").SourceVersion = DataRowVersion.Original
                    Me.SqlCom.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100, "ModifiedBy")
                    Me.SqlCom.Parameters.Add("@ModifiedDate", SqlDbType.SmallDateTime, 100, "ModifiedDate")
                    If IsNothing(Me.SqlCom.Transaction) Then : Me.SqlCom.Transaction = Me.SqlTrans : End If
                    Me.SqlDat.UpdateCommand = Me.SqlCom
                    Me.SqlDat.Update(UpdatedRows) : Me.SqlCom.Parameters.Clear()
                End If
                If DeletedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " IF EXISTS(SELECT GON_ID_AREA FROM GON_HEADER WHERE GON_ID_AREA = @GON_ID_AREA) " & vbCrLf & _
                    " BEGIN RAISERROR('Can not delete data,Data has been used in GON',16,1) ; RETURN ;  END " & vbCrLf & _
                    " DELETE FROM GON_AREA WHERE GON_ID_AREA = @GON_ID_AREA ;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.SqlCom.Parameters.Add("@GON_ID_AREA", SqlDbType.VarChar, 20, "GON_ID_AREA")
                    Me.SqlCom.Parameters()("@GON_ID_AREA").SourceVersion = DataRowVersion.Original
                    If IsNothing(Me.SqlCom.Transaction) Then : Me.SqlCom.Transaction = Me.SqlTrans : End If
                    Me.SqlDat.DeleteCommand = Me.SqlCom
                    Me.SqlDat.Update(DeletedRows) : Me.SqlCom.Parameters.Clear()
                End If
                Me.CommiteTransaction() : If mustReloadData Then : tblGonArea = Me.GetDataTable(mustCloseConnection) : End If

            Catch ex As Exception
                Me.RollbackTransaction() : Me.ClearCommandParameters() : Me.CloseConnection() : Throw ex
            End Try
        End Sub
        Public Function CreateAreaGONID(ByVal area As String, ByRef RecCount As Integer, ByVal mustCloseConnection As Boolean) As String
            Dim num As String = "000"
            Try
                ''check data
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT AREA FROM GON_AREA WHERE AREA = @AREA) ; "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@AREA", SqlDbType.VarChar, area, 100)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    If CInt(retval) > 0 Then
                        Throw New Exception("Data AREA has existed and created previously" & vbCrLf & "You should choose existing data")
                    End If
                End If

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('GON_AREA') AND (index_id=0 or index_id=1)  ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)

                Dim gtRecCount As Integer = 0
                gtRecCount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                gtRecCount += 1
                Dim X As Integer = num.Length - CStr(gtRecCount).Length
                If X <= 0 Then
                    num = ""
                Else
                    num = num.Remove(X - 1, CStr(gtRecCount).Length)
                End If
                num &= gtRecCount.ToString()
                RecCount = gtRecCount
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try

            Return num
        End Function
        Public Function HasExistedData(ByVal AREA As String, ByVal GONIDArea As Object, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                If IsNothing(GONIDArea) Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            " SELECT 1 WHERE EXISTS(SELECT AREA FROM GON_AREA WHERE AREA = @AREA) ;"
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT 1 WHERE EXISTS(SELECT GON_ID_AREA FROM GON_AREA WHERE GON_ID_AREA = @GON_ID_AREA) ;"
                End If

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                If IsNothing(GONIDArea) Then
                    Me.AddParameter("@AREA", SqlDbType.NVarChar, AREA, 100)
                Else : Me.AddParameter("@GON_ID_AREA", SqlDbType.VarChar, GONIDArea, 20)
                End If

                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then
                        Return True
                    End If
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Private Function getSqlParameters() As SqlParameter()
            Dim sqlPars(3) As SqlParameter
            Dim sqlPar1 As New SqlParameter()
            With sqlPar1
                .ParameterName = "@GON_ID_AREA"
                .SqlDbType = SqlDbType.VarChar
                .Size = 20
                .SourceColumn = "GON_ID_AREA"
            End With
            sqlPars(0) = sqlPar1

            sqlPar1 = New SqlParameter
            With sqlPar1
                .ParameterName = "@AREA"
                .SqlDbType = SqlDbType.VarChar
                .Size = 100
                .SourceColumn = "AREA"
            End With
            sqlPars(1) = sqlPar1

            'Me.SqlCom.Parameters.Add("@TRANSPORTATION_BY", SqlDbType.VarChar, 50, "TRANSPORTATION_BY")
            'Me.SqlCom.Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_BY")
            sqlPar1 = New SqlParameter
            With sqlPar1
                .ParameterName = "@DAYS_RECEIPT"
                .SqlDbType = SqlDbType.SmallInt
                .SourceColumn = "DAYS_RECEIPT"
            End With
            sqlPars(2) = sqlPar1
            'Me.SqlCom.Parameters.Add("@TRANSPORTATION_BY", SqlDbType.VarChar, 50, "TRANSPORTATION_BY")
            'Me.SqlCom.Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime, 0, "CREATE_BY")
            sqlPar1 = New SqlParameter
            With sqlPar1
                .ParameterName = "@TRANSPORTATION_BY"
                .SqlDbType = SqlDbType.VarChar
                .Size = 50
                .SourceColumn = "TRANSPORTATION_BY"
            End With
            sqlPars(3) = sqlPar1
            Return sqlPars
        End Function
        Public Overloads Function GetDataTable(ByVal mustcloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                " SELECT * FROM GON_AREA ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim dtTable As New DataTable("GON_AREA") : dtTable.Clear()
                Me.setDataAdapter(Me.SqlCom).Fill(dtTable) : Me.ClearCommandParameters()
                If mustcloseConnection Then : Me.CloseConnection() : End If
                Return dtTable
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

    End Class
End Namespace

