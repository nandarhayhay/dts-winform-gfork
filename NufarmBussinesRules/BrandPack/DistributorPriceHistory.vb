Imports System.Data.SqlClient
Namespace Brandpack
    Public Class DistributorPriceHistory
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Public BRANDPACK_ID As String = "", DISTRIBUTOR_ID As String = ""
        Public PRICE As Decimal = 0, START_DATE As Object = Nothing, END_DATE As Object = Nothing
        Public BRANDPACK_NAME As String = ""
        Public PLANTATION_ID As String = ""
        Public PLANTATION_NAME As String = ""
        Private Query As String = ""
        Public MustIncludeDPD As Boolean = False

        Public Function GetDistributorsByBrandPack(ByVal BRANDPACK_ID As String, ByVal PLANTATION_ID As String, ByVal StartDate As Object, ByVal EndDate As Object) As Object()
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT DISTINCT DISTRIBUTOR_ID FROM DIST_PLANT_PRICE WHERE BRANDPACK_ID = @BRANDPACK_ID " & vbCrLf & _
                        " AND PLANTATION_ID = @PLANTATION_ID AND START_DATE = @START_DATE "
                If Not IsNothing(EndDate) And Not IsDBNull(EndDate) Then
                    Query &= " AND END_DATE = @END_DATE "
                End If
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@PLANTATION_ID", SqlDbType.VarChar, PLANTATION_ID, 50)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Dim dtTable As New DataTable() : Me.FillDataTable(dtTable)
                If dtTable.Rows.Count > 0 Then
                    Dim RetVal(dtTable.Rows.Count - 1) As Object
                    For i As Integer = 0 To dtTable.Rows.Count - 1
                        RetVal(i) = dtTable.Rows(i)("DISTRIBUTOR_ID")
                    Next
                    Return RetVal
                End If
                Return Nothing
            Catch ex As Exception
                If Not Me.SqlRe.IsClosed Then : Me.SqlRe.Close() : End If : Me.CloseConnection()
                Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function HasReferencedDataPO(ByVal PlantationID As String, ByVal DistributorID As String, ByVal BrandPackID As String, ByVal StartDate As DateTime, Optional ByVal EndDate As Object = Nothing) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT 1 WHERE EXISTS(SELECT BRANDPACK_ID FROM " & vbCrLf & _
                        "           (" & vbCrLf & _
                        "             SELECT TOP 1 DPP.BRANDPACK_ID,DPP.DISTRIBUTOR_ID FROM DIST_PLANT_PRICE DPP " & vbCrLf & _
                        "             INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.BRANDPACK_ID = DPP.BRANDPACK_ID " & vbCrLf & _
                        "             INNER JOIN ORDR_PURCHASE_ORDER PO ON OPB.PO_REF_NO = PO.PO_REF_NO AND PO.DISTRIBUTOR_ID = DPP.DISTRIBUTOR_ID " & vbCrLf & _
                        "             WHERE DPP.BRANDPACK_ID = @BRANDPACK_ID AND DPP.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OPB.PLANTATION_ID = @PlantationID " & vbCrLf & _
                        "             AND PO.PO_REF_DATE >= @START_DATE " & vbCrLf
                If (Not IsNothing(EndDate)) Then
                    Query &= " AND PO.PO_REF_DATE <= @END_DATE " & vbCrLf
                End If
                Query &= "           )P); "

                Me.CreateCommandSql("", Query)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@PlantationID", SqlDbType.VarChar, PlantationID, 50)
                If (Not IsNothing(EndDate)) Then
                    Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                End If
                Me.OpenConnection() : Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CloseConnection()
                If Not IsNothing(retval) Then
                    If Convert.ToInt32(retval) > 0 Then
                        Return True
                    End If
                End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function DeletePlantationPrice(ByVal PriceTag As String, ByVal DistributorID As String, ByVal BrandPackID As String, ByVal PlantationID As String, ByVal StartDate As DateTime, ByVal EndDate As Object) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT 1 WHERE EXISTS(SELECT BRANDPACK_ID FROM " & vbCrLf & _
                        "           (" & vbCrLf & _
                        "             SELECT TOP 1 DPP.BRANDPACK_ID,DPP.DISTRIBUTOR_ID FROM DIST_PLANT_PRICE DPP " & vbCrLf & _
                        "             INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.BRANDPACK_ID = DPP.BRANDPACK_ID " & vbCrLf & _
                        "             INNER JOIN ORDR_PURCHASE_ORDER PO ON OPB.PO_REF_NO = PO.PO_REF_NO AND PO.DISTRIBUTOR_ID = DPP.DISTRIBUTOR_ID " & vbCrLf & _
                        "             WHERE DPP.BRANDPACK_ID = @BRANDPACK_ID AND DPP.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OPB.PLANTATION_ID = @PlantationID " & vbCrLf & _
                        "             AND PO.PO_REF_DATE >= @START_DATE " & vbCrLf
                If (Not IsNothing(EndDate)) Then
                    Query &= " AND PO.PO_REF_DATE <= @END_DATE " & vbCrLf
                End If
                Query &= "           )P); "

                Me.CreateCommandSql("", Query)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                Me.AddParameter("@PRICE_TAG", SqlDbType.VarChar, PriceTag, 100)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@PlantationID", SqlDbType.VarChar, PlantationID, 50)
                If (Not IsNothing(EndDate)) Then
                    Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                End If
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    If Convert.ToInt32(retval) > 0 Then
                        'If System.Windows.Forms.MessageBox.Show("PO that brandpack with StartDate <= " & StartDate.ToLongDateString & vbCrLf & "Perhaps already Used" & vbCrLf & _
                        '"If you confirm to delete data click Yes", "Confirmed to delete data ?", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        '    Me.ResetCommandText(CommandType.Text, Query)
                        '    Query = "SET NOCOUNT ON ; " & vbCrLf & _
                        '                    "DELETE FROM DIST_PLANT_PRICE WHERE PRICE_TAG = @PRICE_TAG;"
                        '    Me.ResetCommandText(CommandType.Text, Query)
                        '    Me.AddParameter("@PRICE_TAG", SqlDbType.VarChar, PriceTag, 100)
                        '    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        'End If
                        System.Windows.Forms.MessageBox.Show("PO that brandpack with StartDate <= " & StartDate.ToLongDateString & vbCrLf & "Perhaps already Used" & vbCrLf & "You can not delete data", "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                        Return False
                    Else
                        Me.ResetCommandText(CommandType.Text, Query)
                        Query = "SET NOCOUNT ON ; " & vbCrLf & _
                                        "DELETE FROM DIST_PLANT_PRICE WHERE PRICE_TAG = @PRICE_TAG;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@PRICE_TAG", SqlDbType.VarChar, PriceTag, 100)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    End If
                Else
                    Me.ResetCommandText(CommandType.Text, Query)
                    Query = "SET NOCOUNT ON ; " & vbCrLf & _
                                    "DELETE FROM DIST_PLANT_PRICE WHERE PRICE_TAG = @PRICE_TAG;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PRICE_TAG", SqlDbType.VarChar, PriceTag, 100)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                Me.CloseConnection() : Return True
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetBrandPack(ByVal SearchString As String) As DataView
            Try
                Dim Query As String = "SET NOCOUNT ON;SELECT BRANDPACK_ID,BRANDPACK_NAME FROM BRND_BRANDPACK " & vbCrLf & _
                    " WHERE BRANDPACK_NAME LIKE '%" & SearchString & "%' AND IsActive = 1 AND (IsObsolete = 0 or IsObsolete IS NULL);"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_BrandPack")
                dtTable.Clear() : Me.FillDataTable(dtTable)
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetPlantation(ByVal ListDistributors As List(Of String)) As DataView
            Try
                Dim ListPlants As New List(Of String)
                For i As Integer = 0 To ListDistributors.Count - 1
                    ListPlants.Add(Left(ListDistributors(i), 2))
                Next
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT TER.TERRITORY_AREA,DPP.PLANTATION_ID,PL.PLANTATION_NAME,GROUP_NAME = CASE WHEN PL.PLANT_GROUP_ID IS NULL THEN PL.PLANTATION_NAME ELSE PG.PLANT_GROUP_NAME END FROM " & vbCrLf & _
                        " ( " & vbCrLf & _
                        "SELECT DISTINCT PLANTATION_ID FROM DIST_PLANT_PRICE DL WHERE EXISTS(" & vbCrLf & _
                        "SELECT TOP 1 DISTRIBUTOR_ID FROM DIST_PLANT_PRICE WHERE DISTRIBUTOR_ID = '"
                For i As Integer = 0 To ListDistributors.Count - 1
                    Query &= ListDistributors(i).ToString()
                    Query &= "' AND PLANTATION_ID = DL.PLANTATION_ID) "
                    If i < ListDistributors.Count - 1 Then
                        Query &= " AND EXISTS(SELECT TOP 1 DISTRIBUTOR_ID FROM DIST_PLANT_PRICE WHERE DISTRIBUTOR_ID = '"
                    End If
                Next
                Query &= ")DPP INNER JOIN PLANTATION PL ON PL.PLANTATION_ID = DPP.PLANTATION_ID " & vbCrLf & _
                " LEFT OUTER JOIN PLANTATION_GROUP PG ON PL.PLANT_GROUP_ID = PG.PLANT_GROUP_ID LEFT OUTER JOIN TERRITORY TER ON TER.TERRITORY_ID = PL.TERRITORY_ID "
                '" UNION " & vbCrLf & _
                '" SELECT PLANTATION_ID,PLANTATION_NAME FROM PLANTATION WHERE "
                'For i As Integer = 0 To ListPlants.Count - 1
                '    Query &= " PLANTATION_ID LIKE '" & ListPlants(i) & "%' "
                '    If i < ListPlants.Count - 1 Then
                '        Query &= " OR " & vbCrLf
                '    End If
                'Next
                Query &= " OPTION(KEEP PLAN) ;"
                Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                'Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_Plantation") : dtTable.Clear()
                Me.FillDataTable(dtTable) : Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getDistributor(ByVal SearchString As String, ByVal StartDate As DateTime, ByVal EndDate As Object, ByVal BrandPackID As String) As DataView
            Try
                Dim Query As String = ""
                If String.IsNullOrEmpty(SearchString) Then
                    'Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR ;"
                    Query = "SET NOCOUNT ON " & vbCrLf & _
                        "SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                        "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                        " INNER JOIN AGREE_BRANDPACK_INCLUDE ABI ON ABI.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                        " WHERE AA.START_DATE <= @StartDate AND AA.END_DATE >= @EndDate AND ABI.BRANDPACK_ID = @BrandPackID AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) ;"
                Else
                    Query = "SET NOCOUNT ON " & vbCrLf & _
                        "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                        "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                        " INNER JOIN AGREE_BRANDPACK_INCLUDE ABI ON ABI.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                        " WHERE AA.START_DATE <= @StartDate AND AA.END_DATE >= @EndDate AND ABI.BRANDPACK_ID = @BrandPackID  AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID)AND DR.DISTRIBUTOR_NAME LIKE '%' +@SearchString+ '%' ;"
                    'Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_NAME LIKE '%" & SearchString & "%';"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SearchString", SqlDbType.NVarChar, SearchString, 100)
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                If (EndDate Is Nothing) Then
                    Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, StartDate)
                Else
                    Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                End If
                Me.AddParameter("BrandPackID", SqlDbType.VarChar, BrandPackID, 14)
                Dim dtTable As New DataTable("T_Distributor")
                dtTable.Clear() : Me.FillDataTable(dtTable)
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function getDistributor(ByVal StartDate As DateTime, ByVal EndDate As Object, ByVal BrandPackID As String) As DataView

            Try
                Query = "SET NOCOUNT ON " & vbCrLf & _
                "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR WHERE EXISTS(" & vbCrLf & _
                "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEEMENT AA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                " INNER JOIN AGREE_BRANDPACK_INCLUDE ABI ON ABI.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                " WHERE AA.START_DATE <= @StartDate AND AA.EndDate >= @StartDate AND ABI.BRANDPACK_ID = @BrandPackID) ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                If (EndDate Is Nothing) Then
                    Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, StartDate)
                Else
                    Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                End If
                Me.AddParameter("@BrandPackID", SqlDbType.VarChar, BrandPackID, 14)
                Dim dtTable As New DataTable("T_Distributor") : dtTable.Clear()
                Me.FillDataTable(dtTable) : Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function PopulateQuery(ByVal SearchBy As String, ByVal value As Object, _
               ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer, _
               ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes) As DataView
            Try

                'create DefaultStartDate and DefaultEndDate
                'string[] strSplitDate = DefaultStartDate.ToString().Split("/".ToCharArray());
                '   int SDate = Convert.ToInt16(strSplitDate[0]);
                '   int SMonth = Convert.ToInt16(strSplitDate[1]);
                '   strSplitDate = DefaultEndDate.ToString().Split("/".ToCharArray());
                '   int EDate = Convert.ToInt16(strSplitDate[0]);
                '   int EMonth = Convert.ToInt16(strSplitDate[1]);
                '   int CurrentMonth = CurrentDate.Month;
                '   if ((CurrentMonth >= 8) && (CurrentMonth <= 12))
                '   {
                '       StartDate = new DateTime(CurrentDate.Year, SMonth, SDate);
                '       EndDate = new DateTime(CurrentDate.Year + 1, EMonth, EDate);
                '   }
                '   else if ((CurrentMonth >= 1) && (CurrentMonth <= 7))
                '   {
                '       StartDate = new DateTime(CurrentDate.Year - 1, SMonth, SDate);
                '       EndDate = new DateTime(CurrentDate.Year, EMonth, EDate);
                '   }

                'create temporary table
                Dim StartDate As Object = DBNull.Value
                Dim EndDate As Object = DBNull.Value
                Dim CurrentDate As Date = NufarmBussinesRules.SharedClass.ServerDate
                Dim currentMonth As Integer = NufarmBussinesRules.SharedClass.ServerDate.Month
                If currentMonth >= 8 And CurrentMonth <= 12 Then
                    StartDate = New DateTime(CurrentDate.Year, 8, 1)
                    EndDate = New DateTime(CurrentDate.Year + 1, 7, 31)
                Else
                    StartDate = New DateTime(CurrentDate.Year - 1, 8, 1)
                    EndDate = New DateTime(CurrentDate.Year, 7, 31)
                End If
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " IF NOT EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Price_Distributor' AND Type = 'U') " & vbCrLf & _
                " BEGIN " & vbCrLf & _
                " SELECT DPP.IDApp,DPP.PRICE_TAG,DPP.BRANDPACK_ID,BP.BRANDPACK_NAME,DPP.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME," & vbCrLf & _
                " DPP.PLANTATION_ID,PL.PLANTATION_NAME,'PLANTATION_AREA' = CASE WHEN TER0.TERRITORY_AREA IS NOT NULL THEN TER0.TERRITORY_AREA ELSE DR2.TERRITORY_AREA END, " & vbCrLf & _
                " DPP.PRICE, DPP.START_DATE, DPP.END_DATE, DPP.IncludeDPD, DPP.CREATE_DATE, IsNew = CASE " & vbCrLf & _
                " WHEN (DPP.START_DATE >= @StartDate AND DPP.END_DATE <= @EndDate) THEN 'YESS' " & vbCrLf & _
                " WHEN (DPP.END_DATE >= @EndDate) THEN 'YESS' ELSE 'NO' END INTO TEMPDB..##T_Price_Distributor FROM DIST_PLANT_PRICE DPP INNER JOIN BRND_BRANDPACK BP ON DPP.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                " INNER JOIN DIST_DISTRIBUTOR DR ON DPP.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID INNER JOIN PLANTATION PL ON PL.PLANTATION_ID = DPP.PLANTATION_ID " & vbCrLf & _
                " LEFT OUTER JOIN(SELECT TER1.TERRITORY_AREA,PL1.PLANTATION_ID FROM TERRITORY TER1 INNER JOIN PLANTATION PL1 ON PL1.TERRITORY_ID = TER1.TERRITORY_ID)TER0 " & vbCrLf & _
                " ON TER0.PLANTATION_ID = PL.PLANTATION_ID LEFT OUTER JOIN(SELECT DR1.DISTRIBUTOR_ID,TER2.TERRITORY_AREA FROM DIST_DISTRIBUTOR DR1 INNER JOIN TERRITORY TER2 ON TER2.TERRITORY_ID = DR1.TERRITORY_ID)DR2 " & vbCrLf & _
                " ON DR2.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID ;" & vbCrLf & _
                " CREATE CLUSTERED INDEX IX_T_Plantation ON TEMPDB..##T_Price_Distributor(CREATE_DATE,IDApp) ;" & vbCrLf & _
                " END "
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar() : ClearCommandParameters()

                Query = "SET NOCOUNT ON; SELECT TOP " & PageSize & " * " & _
                        " FROM TEMPDB..##T_Price_Distributor " & vbCrLf & _
                        " WHERE IDApp < ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & _
                        " FROM TEMPDB..##T_Price_Distributor WHERE (" & SearchBy
                Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query &= ") ORDER BY IDApp DESC)"
                Query &= " AND " & SearchBy
                Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query &= " ORDER BY IDApp DESC OPTION(KEEP PLAN);"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)

                'Me.AddParameter("@O_ROWCOUNT", SqlDbType.Int, ParameterDirection.InputOutput)
                Dim Dt As New DataTable("LIST_PRICE_DISTRIBUTOR") : Dt.Clear() : Me.OpenConnection()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(Dt) : Me.ClearCommandParameters()
                If value = "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('DIST_PLANT_PRICE') AND (index_id=0 or index_id=1) ;"
                Else
                    Query = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM Uv_Price_Distributor WHERE " & SearchBy
                    Query &= common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                End If
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters() : Me.CloseConnection()
                'If (Dt.Rows.Count > 0) Then : Else : Return Nothing : End If
                Dim dv As DataView = Dt.DefaultView()
                'dv.Sort = "CREATE_DATE DESC"
                Return dv
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub UpdateIncludedDPD(ByVal ListPRICE_TAG As List(Of String), ByVal IsIncludedDPD As Boolean)
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                "UPDATE DIST_PLANT_PRICE SET IncludeDPD = @IsIncludeDPD WHERE PRICE_TAG = @PRICE_TAG ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                For i As Integer = 0 To ListPRICE_TAG.Count - 1
                    Me.AddParameter("@PRICE_TAG", SqlDbType.VarChar, ListPRICE_TAG(i), 100)
                    Me.AddParameter("@IsIncludeDPD", SqlDbType.Bit, IsIncludedDPD)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Next
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        Public Sub SaveData(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal Mode As NufarmBussinesRules.common.Helper.SaveMode, ByVal ListDistributors As List(Of String), Optional ByVal ListOriginalPriceTag As List(Of String) = Nothing)
            Try
                'Me.CreateCommandSql("Usp_Save_Brnd_Price_Distributor", "")
                'delete dulu listOriginalPricetag
                Me.OpenConnection() : Me.BeginTransaction()
                If Mode = common.Helper.SaveMode.Update Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                             "DELETE FROM DIST_PLANT_PRICE WHERE PRICE_TAG = @PRICE_TAG ;"
                    Me.CreateCommandSql("", Query) : Me.SqlCom.Transaction = Me.SqlTrans
                    For i As Integer = 0 To ListOriginalPriceTag.Count - 1
                        Me.AddParameter("@PRICE_TAG", SqlDbType.VarChar, ListOriginalPriceTag(i), 100)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next
                End If
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT TOP 1 PLANTATION_ID FROM(SELECT PRICE_TAG,PLANTATION_ID FROM DIST_PLANT_PRICE WHERE BRANDPACK_ID = @BRANDPACK_ID AND DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                        " AND((START_DATE <= @END_DATE AND START_DATE >= @START_DATE) OR(END_DATE >= @START_DATE AND END_DATE <= @END_DATE) OR (START_DATE <= @START_DATE AND END_DATE >= @END_DATE)) AND PLANTATION_ID = " & vbCrLf & _
                        " ANY(SELECT PLANTATION_ID FROM PLANTATION WHERE RTRIM(LTRIM(PLANTATION_NAME)) = @PLANTATION_NAME))C OPTION(KEEP PLAN) ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else
                    Me.ResetCommandText(CommandType.Text, Query)
                    If (Me.SqlCom.Transaction Is Nothing) Then
                        Me.SqlCom.Transaction = Me.SqlTrans
                    End If
                End If
                For i As Integer = 0 To ListDistributors.Count - 1
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, Me.BRANDPACK_ID, 14)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, ListDistributors(i), 14)
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                    Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                    Me.AddParameter("@PLANTATION_NAME", SqlDbType.VarChar, Me.PLANTATION_NAME, 100)
                    Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If Not IsNothing(retval) And Not IsDBNull(retval) Then
                        Dim PlantID As String = retval.ToString()
                        Dim DistributorName As String = "", PlantationName As String = ""
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                "SELECT DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, ListDistributors(i), 10)
                        Me.ResetCommandText(CommandType.Text, Query)
                        retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        If Not IsNothing(retval) Then
                            DistributorName = retval.ToString()
                        End If
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT PLANTATION_NAME FROM PLANTATION WHERE PLANTATION_ID = @PLANTATION_ID;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@PLANTATION_ID", SqlDbType.VarChar, PlantID, 50)
                       
                        retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        If Not IsNothing(retval) Then
                            PlantationName = retval.ToString()
                        End If
                        Throw New Exception(DistributorName & " has hold Plantation " & PlantationName & vbCrLf & "Where the periode " & vbCrLf & "is the same/between the periode you'll save.")
                    End If
                Next
                'check keberadaan plantation di mana periode
                Query = " SET NOCOUNT ON ;" & vbCrLf & _
                        " IF EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PRICE_TAG = @PRICE_TAG)" & vbCrLf & _
                        " BEGIN DELETE FROM DIST_PLANT_PRICE WHERE PRICE_TAG = @PRICE_TAG END " & vbCrLf & _
                        " INSERT INTO DIST_PLANT_PRICE(PRICE_TAG,BRANDPACK_ID,DISTRIBUTOR_ID,PLANTATION_ID," & vbCrLf & _
                        " PRICE,START_DATE,END_DATE,IncludeDPD,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                        " VALUES(@PRICE_TAG,@BRANDPACK_ID,@DISTRIBUTOR_ID,@PLANTATION_ID,@PRICE,@START_DATE,@END_DATE,@IncludeDPD,@CREATE_BY,@CREATE_DATE); "
                Me.ResetCommandText(CommandType.Text, Query)
                For i As Integer = 0 To ListDistributors.Count - 1
                    Dim PRICE_TAG As String = ""
                    If Not IsNothing(Me.END_DATE) Then
                        PRICE_TAG = BRANDPACK_ID + "|" + Me.START_DATE + "|" + Me.END_DATE + "|" + Me.PLANTATION_ID + "|" + ListDistributors(i).ToString()
                    Else
                        PRICE_TAG = BRANDPACK_ID + "|" + Me.START_DATE + "|" + ListDistributors(i).ToString() + "|" + Me.PLANTATION_ID
                    End If
                    'Query = "SET NOCOUNT ON; \n" +
                    Me.AddParameter("@PRICE_TAG", SqlDbType.VarChar, PRICE_TAG, 100)
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, Me.BRANDPACK_ID, 14)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, ListDistributors(i), 14)
                    Me.AddParameter("@PLANTATION_ID", SqlDbType.VarChar, Me.PLANTATION_ID, 50)
                    Me.AddParameter("@PRICE", SqlDbType.Decimal, Me.PRICE)
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                    Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.AddParameter("@IncludeDPD", SqlDbType.Bit, Me.MustIncludeDPD)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Next
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Function GetDistributor(ByVal BrandPackID As String) As DataView
            Try
                Query = "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM DIST_PLANT_PRICE WHERE BRANDPACK_ID = @BRANDPACK_ID AND DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID ) ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If

                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                Dim dtTable As New DataTable("T_Distributor") : dtTable.Clear()
                Me.OpenConnection()
                Me.FillDataTable(dtTable) : Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getPrice(ByVal ListDistributorIDs As List(Of String), ByVal BrandPackID As String, ByVal PlantationID As String)
            Try
                Dim strDistributorIDs As String = "IN('"
                For I As Integer = 0 To ListDistributorIDs.Count - 1
                    strDistributorIDs &= ListDistributorIDs(I) + "'"
                    If (I < ListDistributorIDs.Count - 1) Then
                        strDistributorIDs &= ",'"
                    End If
                Next
                strDistributorIDs &= ")"
                Query = "SET NOCOUNT ON " & vbCrLf & _
                "SELECT TOP 1 PRICE FROM DIST_PLANT_PRICE WHERE BRANDPACK_ID = @BRANDPACK_ID AND DISTRIBUTOR_ID " & strDistributorIDs & vbCrLf & _
                "AND PLANTATION_ID = @PLANTATION_ID ORDER BY START_DATE DESC ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                Me.AddParameter("@PLANTATION_ID", SqlDbType.VarChar, 50)
                Dim retval As Object = Me.ExecuteScalar()
                If Not IsNothing(retval) Then
                    Return Convert.ToDecimal(retval)
                End If
                Return 0
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace

