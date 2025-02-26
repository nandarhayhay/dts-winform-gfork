Imports System.Data.SqlClient
Namespace Brandpack
    Public Class PriceHistory
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private Query As String = ""
        Public Enum Category
            SpecialPlantation
            GeneralPricePlantation
            FreeMarket
        End Enum
        Public Function PopulateQuery(ByVal Cat As Category, ByVal SearchBy As String, ByVal value As Object, _
               ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer, _
               ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes) As DataView
            Try
                Dim dtTable As New DataTable("PRICE_HISTORY") : dtTable.Clear()
                Select Case Cat
                    Case Category.SpecialPlantation
                        'Query = "SET NOCOUNT ON; SELECT TOP " & PageSize & " * " & vbCrLf & _
                        '" FROM Uv_Price_Distributor " & vbCrLf & _
                        '" WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & _
                        '" FROM Uv_Price_Distributor WHERE (" & SearchBy
                        'Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                        'Query &= ") ORDER BY IDApp ASC)"
                        'Query &= " AND " & SearchBy
                        'Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                        'Query &= " ORDER BY IDApp ASC OPTION(KEEP PLAN);"
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT TOP " & PageSize.ToString() & " * FROM(SELECT ROW_NUMBER() OVER(ORDER BY IDApp DESC) AS ROW_NUM,IDApp,PRICE_TAG,BRANDPACK_ID,BRANDPACK_NAME,DISTRIBUTOR_ID,DISTRIBUTOR_NAME,PLANTATION_ID,PLANTATION_NAME," & vbCrLf & _
                                "PLANTATION_AREA,PRICE,START_DATE,END_DATE,IncludeDPD,CREATE_DATE FROM Uv_Price_Distributor  " & vbCrLf & _
                                " WHERE (" & SearchBy & " " & common.CommonClass.ResolveCriteria(Criteria, DataType, value) & " ) " & vbCrLf
                        Query &= ")Result WHERE ROW_NUM >= " & ((PageSize * (PageIndex - 1)) + 1).ToString() & " AND ROW_NUM <= " & (PageSize * PageIndex).ToString()

                        Me.CreateCommandSql("sp_executesql", "")
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Me.OpenConnection()
                        Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                        'Query = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM Uv_Price_Distributor WHERE " & SearchBy
                        'Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT COUNT(ROW_NUM) FROM(SELECT ROW_NUMBER() OVER(ORDER BY " & SearchBy & " DESC)AS ROW_NUM FROM Uv_Price_Distributor WHERE (" & SearchBy & " " & common.CommonClass.ResolveCriteria(Criteria, DataType, value) & " ))Result "

                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters() : Me.CloseConnection()
                        If (dtTable.Rows.Count > 0) Then : Else : Return Nothing : End If
                    Case Category.GeneralPricePlantation
                        'Query = "SET NOCOUNT ON;" & vbCrLf & _
                        '        "SELECT TOP " & PageSize & " * FROM Uv_Price_General " & vbCrLf & _
                        '       " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & _
                        '" FROM Uv_Price_General WHERE (" & SearchBy
                        'Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                        'Query &= ") ORDER BY IDApp ASC)"
                        'Query &= " AND " & SearchBy
                        'Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                        'Query &= " ORDER BY IDApp ASC OPTION(KEEP PLAN);"
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT TOP " & PageSize.ToString() & " * FROM(SELECT ROW_NUMBER() OVER(ORDER BY IDApp DESC) AS ROW_NUM,IDApp,PRICE_TAG,BRANDPACK_ID,BRANDPACK_NAME,PRICE,START_DATE,IncludeDPD FROM Uv_Price_General  " & vbCrLf & _
                                " WHERE (" & SearchBy & " " & common.CommonClass.ResolveCriteria(Criteria, DataType, value) & " ) " & vbCrLf
                        Query &= ")Result WHERE ROW_NUM >= " & ((PageSize * (PageIndex - 1)) + 1).ToString() & " AND ROW_NUM <= " & (PageSize * PageIndex).ToString()


                        Me.CreateCommandSql("sp_executesql", "")
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Me.OpenConnection()
                        Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                        If value = "" Then
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    "SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('GEN_PLANT_PRICE') AND (index_id=0 or index_id=1) ;"
                            Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Else
                            'Query = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM Uv_Price_General WHERE " & SearchBy
                            'Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    "SELECT COUNT(ROW_NUM) FROM(SELECT ROW_NUMBER() OVER(ORDER BY " & SearchBy & " DESC)AS ROW_NUM FROM Uv_Price_General WHERE (" & SearchBy & " " & common.CommonClass.ResolveCriteria(Criteria, DataType, value) & " ))Result "

                            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        End If
                        Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters() : Me.CloseConnection()
                        If (dtTable.Rows.Count > 0) Then : Else : Return Nothing : End If
                    Case Category.FreeMarket
                        'Query = "SET NOCOUNT ON;" & vbCrLf & _
                        '        "SELECT TOP " & PageSize & " * FROM Uv_Price " & vbCrLf & _
                        '       " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & _
                        '" FROM Uv_Price WHERE (" & SearchBy
                        'Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                        'Query &= ") ORDER BY IDApp ASC)"
                        'Query &= " AND " & SearchBy
                        'Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                        'Query &= " ORDER BY IDApp ASC OPTION(KEEP PLAN);"
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                   "SELECT TOP " & PageSize.ToString() & " * FROM(SELECT ROW_NUMBER() OVER(ORDER BY IDApp DESC) AS ROW_NUM,IDApp,PRICE_TAG,BRANDPACK_ID,BRANDPACK_NAME,PRICE,START_DATE FROM Uv_Price " & vbCrLf & _
                                   " WHERE (" & SearchBy & " " & common.CommonClass.ResolveCriteria(Criteria, DataType, value) & " ) " & vbCrLf
                        Query &= ")Result WHERE ROW_NUM >= " & ((PageSize * (PageIndex - 1)) + 1).ToString() & " AND ROW_NUM <= " & (PageSize * PageIndex).ToString()


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
                            'Query = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM Uv_Price WHERE " & SearchBy
                            'Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    "SELECT COUNT(ROW_NUM) FROM(SELECT ROW_NUMBER() OVER(ORDER BY " & SearchBy & " DESC)AS ROW_NUM FROM Uv_Price WHERE (" & SearchBy & " " & common.CommonClass.ResolveCriteria(Criteria, DataType, value) & " ))Result "

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
                Dim retval As Object = Nothing
                Me.OpenConnection()
                If Not String.IsNullOrEmpty(PlantationID) Then
                    Query = "SET NOCOUNT ON " & vbCrLf & _
                    "SELECT TERRITORY_ID FROM Plantation WHERE PLANTATION_ID = @PlantationID ;"
                    If IsNothing(Me.SqlCom) Then
                        Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@PlantationID", SqlDbType.VarChar, PlantationID, 50)

                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                If retval Is Nothing Or IsDBNull(retval) Then
                    Query = "SET NOCOUNT ON " & vbCrLf & _
                    "SELECT TERRITORY_ID FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"
                    If IsNothing(Me.SqlCom) Then
                        Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
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
                If cat = Category.SpecialPlantation Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "SELECT TOP 10 DPP.PRICE_TAG, DPP.BRANDPACK_ID,BP.BRANDPACK_NAME,PL.PLANTATION_NAME,DPP.PLANTATION_ID,DPP.PRICE,DPP.START_DATE,DPP.END_DATE " & vbCrLf & _
                            " FROM DIST_PLANT_PRICE DPP INNER JOIN BRND_BRANDPACK BP ON DPP.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                            " INNER JOIN PLANTATION PL ON DPP.PLANTATION_ID = PL.PLANTATION_ID " & vbCrLf & _
                            " WHERE DPP.BRANDPACK_ID = @BRANDPACK_ID AND DPP.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND DPP.START_DATE <= '" & StartDateString & "' AND DPP.END_DATE >= '" & StartDateString + "'" & vbCrLf
                    If (Not String.IsNullOrEmpty(SearchPlantationName)) Then
                        Query &= " AND PL.PLANTATION_NAME LIKE '%'+@SearchPlant+'%'" & vbCrLf
                    End If
                    Query &= " ORDER BY DPP.START_DATE DESC ;"
                ElseIf cat = Category.GeneralPricePlantation Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT TOP 10 GPL.PRICE_TAG,GPL.BRANDPACK_ID,BP.BRANDPACK_NAME,GPL.PRICE,GPL.START_DATE,IncludeDPD " & vbCrLf & _
                    " FROM GEN_PLANT_PRICE GPL INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = GPL.BRANDPACK_ID " & vbCrLf & _
                    " WHERE GPL.BRANDPACK_ID = @BRANDPACK_ID AND GPL.START_DATE <= '" & StartDateString & "' ORDER BY GPL.START_DATE DESC ;"
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT TOP 1 BPH.BRANDPACK_ID,BP.BRANDPACK_NAME,BPH.PRICE,BPH.PRICE_TAG,BPH.START_DATE" & vbCrLf & _
                            " FROM BRND_PRICE_HISTORY BPH INNER JOIN BRND_BRANDPACK BP ON BPH.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                            " WHERE BPH.START_DATE <= '" & StartDateString & "' AND BPH.BRANDPACK_ID = @BRANDPACK_ID ORDER BY START_DATE DESC;"
                End If
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID)
                If cat = Category.SpecialPlantation Then
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID)
                    If (Not String.IsNullOrEmpty(SearchPlantationName)) Then
                        Me.AddParameter("@SearchPlant", SqlDbType.VarChar, SearchPlantationName)
                    End If
                    'ElseIf cat = Category.GeneralPricePlantation Then
                    '    If (Not String.IsNullOrEmpty(SearchPlantationName)) Then
                    '        Me.AddParameter("@SearchPlant", SqlDbType.VarChar, SearchPlantationName)
                    '    End If
                End If
                Dim dtTable As New DataTable("T_Price") : dtTable.Clear()
                Me.FillDataTable(dtTable) : Return dtTable
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace
