Imports System.Data.SqlClient
Namespace Brandpack
    Public Class PriceHistory
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private Query As String = ""
        Public Enum Category
            Plantation
            FreeMarket
        End Enum
        Public Function PopulateQuery(ByVal Cat As Category, ByVal SearchBy As String, ByVal value As Object, _
               ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer, _
               ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes) As DataView
            Try
                Dim dtTable As New DataTable("PRICE_HISTORY") : dtTable.Clear()
                Select Case Cat
                    Case Category.Plantation
                        Query = "SET NOCOUNT ON; SELECT TOP " & PageSize & " * " & vbCrLf & _
                        " FROM Uv_Price_Distributor " & vbCrLf & _
                        " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & _
                        " FROM Uv_Price_Distributor WHERE (" & SearchBy
                        Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                        Query &= ") ORDER BY IDApp ASC)"
                        Query &= " AND " & SearchBy
                        Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                        Query &= " ORDER BY IDApp ASC OPTION(KEEP PLAN);"

                        Me.CreateCommandSql("sp_executesql", "")
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Me.OpenConnection()
                        Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                        If value = "" Then
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    "SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('DIST_PLANT_PRICE') AND (index_id=0 or index_id=1) ;"
                            Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Else
                            Query = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM Uv_Price_Distributor WHERE " & SearchBy
                            Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        End If
                        Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters() : Me.CloseConnection()
                        If (dtTable.Rows.Count > 0) Then : Else : Return Nothing : End If
                    Case Category.FreeMarket
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "SELECT TOP " & PageSize & " * FROM Uv_Price " & vbCrLf & _
                               " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & _
                        " FROM Uv_Price WHERE (" & SearchBy
                        Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                        Query &= ") ORDER BY IDApp ASC)"
                        Query &= " AND " & SearchBy
                        Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                        Query &= " ORDER BY IDApp ASC OPTION(KEEP PLAN);"

                        Me.CreateCommandSql("sp_executesql", "")
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Me.OpenConnection()
                        Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                        If value = "" Then
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    "SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('BRND_PRICE_HISTORY') AND (index_id=0 or index_id=1) ;"
                            Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Else
                            Query = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM Uv_Price WHERE " & SearchBy
                            Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        End If
                        Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters() : Me.CloseConnection()
                        If (dtTable.Rows.Count > 0) Then : Else : Return Nothing : End If
                End Select
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getTerritory(ByVal distributorID, ByVal PlantationID) As String
            Try
                Query = "SET NOCOUNT ON " & vbCrLf & _
                "SELECT TERRITORY_ID FROM Plantation WHERE PLANTATION_ID = @PlantationID ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PlantationID", SqlDbType.VarChar, PlantationID, 50)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If retval Is Nothing Or IsDBNull(retval) Then
                    Query = "SET NOCOUNT ON " & vbCrLf & _
                    "SELECT TERRITORY_ID FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID, 15)
                    retval = Me.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                Me.CloseConnection()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return retval.ToString()
                End If
                Return ""
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getPlantation(ByVal cat As Category, ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal StartDateString As String, Optional ByVal SearchPlantationName As String = "") As DataTable
            Try
                If cat = Category.Plantation Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "SELECT TOP 10 DPP.PRICE_TAG, DPP.BRANDPACK_ID,BP.BRANDPACK_NAME,PL.PLANTATION_NAME,DPP.PLANTATION_ID,DPP.PRICE,DPP.START_DATE,DPP.END_DATE " & vbCrLf & _
                            " FROM DIST_PLANT_PRICE DPP INNER JOIN BRND_BRANDPACK BP ON DPP.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                            " INNER JOIN PLANTATION PL ON DPP.PLANTATION_ID = PL.PLANTATION_ID " & vbCrLf & _
                            " WHERE DPP.BRANDPACK_ID = '" & BRANDPACK_ID & "' AND DPP.DISTRIBUTOR_ID = '" & _
                            DISTRIBUTOR_ID & "' AND DPP.START_DATE <= '" & StartDateString & "' AND DPP.END_DATE >= '" & StartDateString + "'" & vbCrLf
                    If (Not String.IsNullOrEmpty(SearchPlantationName)) Then
                        Query &= " AND PL.PLANTATION_NAME LIKE '%" & SearchPlantationName & "%'" & vbCrLf
                    End If
                    Query &= " ORDER BY START_DATE DESC ;"
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT TOP 1 BPH.BRANDPACK_ID,BP.BRANDPACK_NAME,BPH.PRICE,BPH.PRICE_TAG,BPH.START_DATE" & vbCrLf & _
                            " FROM BRND_PRICE_HISTORY BPH INNER JOIN BRND_BRANDPACK BP ON BPH.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                            " WHERE BPH.START_DATE <= '" & StartDateString & "' AND BPH.BRANDPACK_ID = '" & BRANDPACK_ID & "' ORDER BY START_DATE DESC;"
                End If
                Me.CreateCommandSql("sp_executesql", "") : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_Price") : dtTable.Clear()
                Me.FillDataTable(dtTable) : Return dtTable
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace
