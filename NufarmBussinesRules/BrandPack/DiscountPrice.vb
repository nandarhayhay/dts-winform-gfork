Imports NufarmDataAccesLayer
Imports System.Data.SqlClient
Imports System.Globalization

Namespace Brandpack
    Public Class DiscountPrice : Inherits DataAccesLayer.ADODotNet
        Public Sub New()
            MyBase.New()
        End Sub
        Protected Query As String = ""
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)

            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM TEMPDB.SYS.OBJECTS WHERE NAME = '##T_Get_DD_DR_CBD_" & Me.ComputerName & "'  AND Type = 'U')" & vbCrLf & _
                        " BEGIN DROP TABLE tempdb..##T_Get_DD_DR_CBD_" & Me.ComputerName & "; END "
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                MyBase.Dispose(disposing)
            Catch ex As Exception
                Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        Public Function getBrandPackPrice(ByVal muscloseConnection As Boolean) As DataView
            Dim DV As DataView = Nothing
            Try
                Query = "SELECT BR.BRANDPACK_ID, BP.BRANDPACK_NAME,BR.PRICE AS CURRENT_PRICE_FM " & vbCrLf & _
                        "FROM BRND_BRANDPACK BP INNER JOIN BRND_PRICE_HISTORY BR ON BP.BRANDPACK_ID = BR.BRANDPACK_ID " & vbCrLf & _
                        "WHERE BR.START_DATE = (SELECT MAX(START_DATE) FROM BRND_PRICE_HISTORY WHERE BRANDPACK_ID = BP.BRANDPACK_ID) " & vbCrLf & _
                        "AND EXISTS(SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE WHERE BRANDPACK_ID = BR.BRANDPACK_ID) " & vbCrLf & _
                        "AND BP.IsActive = 1 AND (BP.IsObsolete IS NULL OR BP.IsObsolete = 0); "
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_DiscPrice") : dtTable.Clear()
                Me.FillDataTable(dtTable) : Return dtTable.DefaultView()
                If muscloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return DV
        End Function
        Public Function PopulateQuery(ByVal SearchBy As String, ByVal value As Object, _
       ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef Rowcount As Integer, _
       ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes) As DataView
            Try
                Dim ResolvedCriteria As String = common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT TOP  " & PageSize.ToString() & " * FROM(SELECT ROW_NUMBER() OVER(ORDER BY IDApp DESC) AS ROW_NUM,IDApp,PROGRAM_ID," & vbCrLf & _
                " APPLY_DATE,END_DATE,PROGRAM_DESC,CASE APPLY_TO WHEN 'AL' THEN 'All Distributors' WHEN 'CD' THEN " & vbCrLf & _
                " 'Certain Distributors' WHEN 'GD' THEN 'Group Distributors' ELSE 'UNKNOWN' END AS APPLY_TO,CreatedBy,CreatedDate, DISC_PROG_DESC AS DISCOUNT_PROGRESSIVE " & vbCrLf & _
                " FROM BRND_DISC_HEADER WHERE (" & SearchBy & " " & ResolvedCriteria & " ) )Result" & vbCrLf & _
                " WHERE ROW_NUM >= " & ((PageSize * (PageIndex - 1)) + 1).ToString() & " AND ROW_NUM <= " & (PageSize * PageIndex).ToString()
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tbl As New DataTable("ENTRY_SCHEMA_DD_DR_CBD")
                OpenConnection()
                setDataAdapter(Me.SqlCom).Fill(tbl) : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT COUNT(ROW_NUM) FROM(SELECT ROW_NUMBER() OVER(ORDER BY " & SearchBy & " ASC)AS ROW_NUM FROM BRND_DISC_HEADER WHERE (" & SearchBy & " " & ResolvedCriteria & " ))Result "
                ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Rowcount = CInt(Me.SqlCom.ExecuteScalar())
                Me.CloseConnection() : Me.ClearCommandParameters()
                Return tbl.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getReport(ByVal CategoryDisc As String, ByVal ParColumns As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As DataView
            Try

                If CategoryDisc = "CERTAIN_DISTRIBUTORS" Then
                    Query = "Usp_Get_Report_DD_DR"
                Else
                    Query = "Usp_Get_Report_DD_DR_All"
                End If

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql(Query, "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, Query)
                End If
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Me.AddParameter("@PAR_COLUMNS", SqlDbType.VarChar, ParColumns, 50)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DBNull.Value, 50)
                OpenConnection()
                Dim tblDDDR As New DataTable("REPORT DD_DR_CBD")
                setDataAdapter(Me.SqlCom).Fill(tblDDDR)
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Return tblDDDR.DefaultView()
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function getDS(ByVal IDApp As Integer, ByVal mustCloseConnection As Boolean) As DataSet
            Try
                'get discount progressive
                If IDApp > 0 Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_DISC_PROG_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                            " BEGIN  DROP TABLE tempdb..##T_DISC_PROG_" & Me.ComputerName & " ; END "
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    End If
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT BB.BRAND_ID,BP.BRANDPACK_ID,OOBD.RefOther,OOBD.GQSY_SGT_P_FLAG AS Flag  INTO ##T_DISC_PROG_" & Me.ComputerName & " FROM BRND_BRANDPACK BP " & vbCrLf & _
                            " INNER JOIN ORDR_PO_BRANDPACK PB ON PB.BRANDPACK_ID = BP.BRANDPACK_ID INNER JOIN ORDR_OA_BRANDPACK OOB ON OOB.PO_BRANDPACK_ID = PB.PO_BRANDPACK_ID " & vbCrLf & _
                            " INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD ON OOBD.OA_BRANDPACK_ID = OOB.OA_BRANDPACK_ID " & vbCrLf & _
                            " INNER JOIN BRND_BRAND BB ON BB.BRAND_ID = BP.BRAND_ID " & vbCrLf & _
                            " WHERE OOBD.RefOther = @IDApp OR OOBD.FK_BRND_DISC_PROG = @IDApp; "
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT BDP.*,HasRef = CASE WHEN (BDP.BRANDPACK_ID IS NOT NULL AND EXISTS(SELECT BRANDPACK_ID FROM ##T_DISC_PROG_" & Me.ComputerName & " T_PROG WHERE T_PROG.BRANDPACK_ID = BDP.BRANDPACK_ID AND 'O' + BDP.TypeApp = T_PROG.Flag)) THEN 1 " & vbCrLf & _
                            "   WHEN (BDP.BRAND_ID IS NOT NULL AND EXISTS(SELECT BRAND_ID FROM ##T_DISC_PROG_" & Me.ComputerName & " T_PROG WHERE T_PROG.BRAND_ID = BDP.BRAND_ID AND T_PROG.Flag = 'O' + BDP.TypeApp)) THEN 1 " & vbCrLf & _
                            "   WHEN (EXISTS(SELECT Flag FROM ##T_DISC_PROG_" & Me.ComputerName & " T_PROG WHERE T_PROG.Flag = 'O' + BDP.TypeApp )) THEN 1 " & vbCrLf & _
                            " ELSE 0 END  FROM BRND_DISC_PROG BDP WHERE BDP.FKApp = @IDApp ;"
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT BDP.*,HasRef = 0 FROM BRND_DISC_PROG BDP WHERE BDP.FKApp = @IDApp ; "
                End If

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                OpenConnection()
                Dim tblProgDisc As New DataTable("DISCOUNT_PROGRESSIVE")
                setDataAdapter(Me.SqlCom).Fill(tblProgDisc)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Dim DS As New DataSet("DSDiscPrice")
                DS.Clear()
                DS.Tables.Add(tblProgDisc)
                DS.AcceptChanges()
                Return DS
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function HasReference(ByVal IDApp As Integer, ByVal mustCloseConnection As Boolean) As Boolean
            Dim hasRef As Boolean = False
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT RefOther FROM ORDR_OA_BRANDPACK_DISC WHERE RefOther = @IDApp) " & vbCrLf & _
                "          OR EXISTS(SELECT RefOther FROM ORDR_OA_REMAINDING WHERE RefOther = @IDApp) " & vbCrLf & _
                "          OR EXISTS(SELECT FK_BRND_DISC_PROG FROM ORDR_OA_BRANDPACK_DISC WHERE FK_BRND_DISC_PROG = @IDApp) " & vbCrLf & _
                "          OR EXISTS(SELECT FK_BRND_DISC_PROG FROM ORDR_OA_REMAINDING WHERE FK_BRND_DISC_PROG = @IDApp);"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                OpenConnection()
                Dim retval = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) Then
                    Return (CInt(retval) > 0)
                End If
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
            Return hasRef
        End Function
        Public Function getDistGroupList(ByVal mustCloseConnection As Boolean) As DataView
            Dim DV As DataView = Nothing
            Try
                Query = "SELECT GL.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,GBL.FKGroup AS GRP_ID FROM DIST_GROUP_LIST GL INNER JOIN DIST_DISTRIBUTOR DR " & vbCrLf & _
                    " ON GL.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID INNER JOIN DIST_GROUP_BRND_LIST GBL ON GBL.FKGroup = GL.GRP_ID "
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_DistGroup") : dtTable.Clear()
                Me.OpenConnection()
                Me.FillDataTable(dtTable)
                If mustCloseConnection Then : Me.CloseConnection() : End If : Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return DV
        End Function
        Public Function getDistGroup(ByVal IDApp As Integer, ByRef ListCheckedGroups As List(Of String), ByVal mustCloseConnection As Boolean) As DataView
            Dim DV As DataView = Nothing
            Try
                Query = "SELECT GRP_ID,GRP_NAME FROM DIST_GROUP"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_DiscPrice") : dtTable.Clear()
                Me.OpenConnection()
                Me.FillDataTable(dtTable)
                If IDApp > 0 Then
                    'get listCheckedBrands
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    " SELECT FKGroup AS GRP_ID FROM DIST_GROUP_BRND_LIST WHERE FKDisc = @FkApp ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@FkApp", SqlDbType.Int, IDApp)
                    Me.ExecuteReader()
                    While Me.SqlRe.Read()
                        If Not ListCheckedGroups.Contains(SqlRe.GetString(0)) Then
                            ListCheckedGroups.Add(SqlRe.GetString(0))
                        End If
                    End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If : Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return DV
        End Function
        Public Function getBrandBrandPack(ByVal IDApp As Integer, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal ListBrands As List(Of String), ByRef ListCheckedBrandPacks As List(Of String), ByVal mustCloseConnection As Boolean) As DataView
            Try
                Dim strListBrands As String = "IN('"
                For i As Integer = 0 To ListBrands.Count - 1
                    strListBrands = strListBrands & ListBrands(i).ToString() & "'"
                    If i < ListBrands.Count - 1 Then
                        strListBrands = strListBrands & ",'"
                    End If
                Next
                strListBrands = strListBrands & ")"
                If strListBrands = "IN(')" Then
                    Throw New Exception("Please mark brands")
                End If
                Query = "SELECT BP.BRANDPACK_ID,BP.BRANDPACK_NAME FROM BRND_BRANDPACK BP WHERE EXISTS(" & vbCrLf & _
                        " SELECT ABP.BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE ABP INNER JOIN AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = ABP.AGREEMENT_NO " & vbCrLf & _
                        " WHERE AA.START_DATE <= @StartDate AND AA.END_DATE >= @EndDate AND ABP.BRANDPACK_ID = BP.BRANDPACK_ID)AND BP.BRAND_ID " & strListBrands & " ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Dim dtTable As New DataTable("T_DiscPrice") : dtTable.Clear()
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(dtTable)
                If IDApp > 0 Then
                    'get listCheckedBrands
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    " SELECT BRANDPACK_ID FROM BRND_DISC_LIST_BRANDPACK WHERE FkApp = @FkApp ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@FkApp", SqlDbType.Int, IDApp)
                    Me.ExecuteReader()
                    While Me.SqlRe.Read()
                        If Not ListCheckedBrandPacks.Contains(SqlRe.GetString(0)) Then
                            ListCheckedBrandPacks.Add(SqlRe.GetString(0))
                        End If
                    End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If : Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function getBrand(ByVal IDApp As Integer, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByRef ListCheckedBrands As List(Of String), ByVal mustCloseConnection As Boolean) As DataView
            Try
                Query = "SELECT BR.BRAND_ID,BR.BRAND_NAME FROM BRND_BRAND BR WHERE EXISTS(" & vbCrLf & _
                        " SELECT ABI.BRAND_ID FROM AGREE_BRAND_INCLUDE ABI INNER JOIN AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                        " WHERE AA.START_DATE <= @StartDate AND AA.END_DATE >= @EndDate AND ABI.BRAND_ID = BR.BRAND_ID) ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Dim dtTable As New DataTable("T_DiscPrice") : dtTable.Clear()
                Me.OpenConnection()
                Me.FillDataTable(dtTable)
                If IDApp > 0 Then
                    'get listCheckedBrands
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    " SELECT BRAND_ID FROM BRND_DISC_LIST_BRAND WHERE FkApp = @FkApp ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@FkApp", SqlDbType.Int, IDApp)
                    Me.ExecuteReader()
                    While Me.SqlRe.Read()
                        If Not ListCheckedBrands.Contains(SqlRe.GetString(0)) Then
                            ListCheckedBrands.Add(SqlRe.GetString(0))
                        End If
                    End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If : Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function Delete(ByVal IDApp As Integer, ByVal mustCloseConnection As Boolean) As Boolean
            Dim c As Boolean = True
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " DELETE FROM BRND_DISC_LIST_BRAND WHERE FKApp = @FkApp;" & vbCrLf & _
                        " DELETE FROM DIST_DISC_BRAND_LIST WHERE FKApp = @FKApp;" & vbCrLf & _
                        " DELETE FROM DIST_GROUP_BRND_LIST WHERE FKDisc = @FKApp;" & vbCrLf & _
                        " DELETE FROM BRND_DISC_PROG WHERE FKApp = @FKApp ; " & vbCrLf & _
                        " DELETE FROM BRND_DISC_LIST_BRANDPACK WHERE FKApp = @FKApp; " & vbCrLf & _
                        " DELETE FROM BRND_DISC_HEADER WHERE IDApp = @FKApp ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@FKApp", SqlDbType.Int, IDApp)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar()
                Me.CommiteTransaction() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return False
        End Function
        Public Function SaveData(ByVal domPrice As Nufarm.Domain.DiscPrice, ByVal Mode As NufarmBussinesRules.common.Helper.SaveMode, ByVal mustCloseConnection As Boolean) As Boolean
            Dim commandInsert As SqlCommand = Nothing
            Dim commandUpdate As SqlCommand = Nothing
            Dim commandDelete As SqlCommand = Nothing
            Dim IDAppHeader As Integer = 1

            Dim tblBrand As DataTable = Nothing, tblBrandPack As DataTable = Nothing
            Dim NewRow As DataRow = Nothing
            Dim InsertedRows() As DataRow = Nothing
            Dim retval As Object = Nothing
            Dim PKey As Object = Nothing
            If domPrice.HasChangedBrands Then
                If domPrice.ListBrands.Count > 0 Then
                    tblBrand = New DataTable("T_Temp")
                    tblBrand.Clear()
                    tblBrand.Columns.Add("BRAND_ID", Type.GetType("System.String"))
                    tblBrand.Columns.Add("CreatedDate", Type.GetType("System.DateTime"))
                    tblBrand.Columns.Add("CreatedBy", Type.GetType("System.String"))
                    tblBrand.AcceptChanges()
                    For i As Integer = 0 To domPrice.ListBrands.Count - 1
                        NewRow = tblBrand.NewRow
                        NewRow.BeginEdit()
                        NewRow("BRAND_ID") = domPrice.ListBrands(i)
                        NewRow("CreatedDate") = IIf(Mode = common.Helper.SaveMode.Insert, domPrice.CreatedDate, domPrice.ModifiedDate)
                        NewRow("CreatedBy") = IIf(Mode = common.Helper.SaveMode.Insert, domPrice.CreatedBy, domPrice.ModifiedBy)
                        NewRow.EndEdit()
                        tblBrand.Rows.Add(NewRow)
                    Next
                End If
            End If
            If domPrice.HasChangedBrandPacks Then
                If domPrice.ListBrandPacks.Count > 0 Then
                    tblBrandPack = New DataTable("T_Temp1")
                    tblBrandPack.Clear()
                    tblBrandPack.Columns.Add("BRANDPACK_ID", Type.GetType("System.String"))
                    tblBrandPack.Columns.Add("CreatedBy", Type.GetType("System.String"))
                    tblBrandPack.Columns.Add("CreatedDate", Type.GetType("System.DateTime"))
                    tblBrandPack.AcceptChanges()
                    For i As Integer = 0 To domPrice.ListBrandPacks.Count - 1
                        NewRow = tblBrandPack.NewRow
                        NewRow.BeginEdit()
                        NewRow("BRANDPACK_ID") = domPrice.ListBrandPacks(i)
                        NewRow("CreatedDate") = IIf(Mode = common.Helper.SaveMode.Insert, domPrice.CreatedDate, domPrice.ModifiedDate)
                        NewRow("CreatedBy") = IIf(Mode = common.Helper.SaveMode.Insert, domPrice.CreatedBy, domPrice.ModifiedBy)
                        NewRow.EndEdit()
                        tblBrandPack.Rows.Add(NewRow)
                    Next
                End If
            End If
            Dim tblDistr As DataTable = Nothing
            If domPrice.HasChangedDistr Then
                If domPrice.ListDistributors.Count > 0 Then
                    tblDistr = New DataTable("T_Distr")
                    tblDistr.Clear()
                    With tblDistr
                        .Columns.Add("DISTRIBUTOR_ID", Type.GetType("System.String"))
                        .Columns.Add("CreatedDate", Type.GetType("System.DateTime"))
                        .Columns.Add("CreatedBy", Type.GetType("System.String"))
                        .AcceptChanges()
                    End With
                    For I As Integer = 0 To domPrice.ListDistributors.Count - 1
                        NewRow = tblDistr.NewRow
                        NewRow.BeginEdit()
                        NewRow("DISTRIBUTOR_ID") = domPrice.ListDistributors(I)
                        NewRow("CreatedDate") = IIf(Mode = common.Helper.SaveMode.Insert, domPrice.CreatedDate, domPrice.ModifiedDate)
                        NewRow("CreatedBy") = IIf(Mode = common.Helper.SaveMode.Insert, domPrice.CreatedBy, domPrice.ModifiedBy)
                        NewRow.EndEdit()
                        tblDistr.Rows.Add(NewRow)
                    Next
                End If
            End If
            Dim tblGroups As DataTable = Nothing
            If domPrice.HasChangedGroups Then
                If domPrice.ListGroupDist.Count > 0 Then
                    tblGroups = New DataTable("T_Distr")
                    tblGroups.Clear()
                    With tblGroups
                        .Columns.Add("FKGroup", Type.GetType("System.String"))
                        .Columns.Add("FKDisc", Type.GetType("System.Int32"))
                        .Columns.Add("CreatedBy", Type.GetType("System.String"))
                        .Columns.Add("CreatedDate", Type.GetType("System.DateTime"))
                        .AcceptChanges()
                    End With
                    For I As Integer = 0 To domPrice.ListGroupDist.Count - 1
                        NewRow = tblGroups.NewRow
                        NewRow.BeginEdit()
                        NewRow("FKGroup") = domPrice.ListGroupDist(I)
                        NewRow("CreatedDate") = IIf(Mode = common.Helper.SaveMode.Insert, domPrice.CreatedDate, domPrice.ModifiedDate)
                        NewRow("CreatedBy") = IIf(Mode = common.Helper.SaveMode.Insert, domPrice.CreatedBy, domPrice.ModifiedBy)
                        NewRow.EndEdit()
                        tblGroups.Rows.Add(NewRow)
                    Next
                End If
            End If
            ''set descriptions discount
            Dim BrndProgDesc As String = "["
            Dim rowsMTQ = domPrice.DsProgDesc.Tables(0).Select("", "TypeApp ASC, MoreThanQty ASC")
            For i As Integer = 0 To rowsMTQ.Length - 1
                BrndProgDesc &= String.Format("({0} >= {1:#,##0.000}, Disc = {2:p} )", rowsMTQ(i)("TypeApp"), Convert.ToDecimal(rowsMTQ(i)("MoreThanQty")), Convert.ToDecimal(rowsMTQ(i)("Disc")) / 100)
                If i < rowsMTQ.Length - 1 Then
                    BrndProgDesc &= ","
                End If
            Next
            BrndProgDesc &= "]"
            Try
                'Insert Header
                If Mode = common.Helper.SaveMode.Insert Then
                    ' ''check existing data
                    'Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    '" SELECT TOP 1 IDApp FROM BRND_DISC_HEADER WHERE APPLY_DATE = CONVERT(VARCHAR(100),@ApplyDate,101) AND APPLY_TO = @ApplyTo ;"
                    'If IsNothing(Me.SqlCom) Then
                    '    Me.CreateCommandSql("", Query)
                    'Else : Me.ResetCommandText(CommandType.Text, Query)
                    'End If
                    'Me.AddParameter("@ApplyDate", SqlDbType.SmallDateTime, domPrice.ApplyDate)
                    'Me.AddParameter("@ApplyTo", SqlDbType.VarChar, domPrice.ApplyTo, 2)
                    'Me.OpenConnection()
                    'retval = Me.SqlCom.ExecuteScalar()
                    'If Not IsNothing(retval) Then
                    '    If CInt(retval) > 0 Then
                    '        Throw New Exception("Data with Date apply " & String.Format(New CultureInfo("id-ID"), "{0:dd-MMMM-yyyy}", Convert.ToDateTime(domPrice.ApplyDate)) & " Has existed")
                    '    End If
                    'End If : Me.ClearCommandParameters()

                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "IF EXISTS(SELECT PROGRAM_ID FROM BRND_DISC_HEADER WHERE PROGRAM_ID = @PROGRAM_ID) " & vbCrLf & _
                            " BEGIN RAISERROR('Program ID has existed',16,1); RETURN; END " & vbCrLf & _
                            "INSERT INTO BRND_DISC_HEADER(PROGRAM_ID,APPLY_DATE,END_DATE,APPLY_TO,PROGRAM_DESC,DISC_PROG_DESC,CreatedDate,CreatedBy) " & vbCrLf & _
                            " VALUES(@PROGRAM_ID,@APPLY_DATE,@END_DATE,@APPLY_TO,@PROGRAM_DESC,@DISC_PROG_DESC,@CreatedDate,@CreatedBy);" & vbCrLf & _
                            " SELECT @@IDENTITY ;"
                    If IsNothing(Me.SqlCom) Then
                        Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, domPrice.ProgramID)
                    Me.AddParameter("@APPLY_DATE", SqlDbType.SmallDateTime, domPrice.ApplyDate)
                    Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, domPrice.EndDate)
                    Me.AddParameter("@APPLY_TO", SqlDbType.Char, domPrice.ApplyTo, 2) ' 
                    Me.AddParameter("@PROGRAM_DESC", SqlDbType.VarChar, domPrice.NameApp, 250)
                    Me.AddParameter("@DISC_PROG_DESC", SqlDbType.VarChar, BrndProgDesc, 250)
                    Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, domPrice.CreatedDate)
                    Me.AddParameter("@CreatedBy", SqlDbType.VarChar, domPrice.CreatedBy, 100)
                    Me.OpenConnection()
                    Me.BeginTransaction()
                    Me.SqlCom.Transaction = Me.SqlTrans
                    PKey = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    'Dim listProgs As New List(Of String)
                    'Me.ExecuteReader()
                    'While Me.SqlRe.Read()
                    '    listProgs.Add(SqlRe.GetInt32(0))
                    'End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                    'If listProgs.Count > 0 Then
                    '    Dim strListProgs As String = "IN("
                    '    For i As Integer = 0 To listProgs.Count - 1
                    '        strListProgs = strListProgs & listProgs(i) & ""
                    '        If i < listProgs.Count - 1 Then
                    '            strListProgs = strListProgs & ","
                    '        End If
                    '    Next
                    '    strListProgs = strListProgs & ")"
                    '    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    '    "DECLARE @ApplyTo VARCHAR(2); " & vbCrLf & _
                    '    " SET @ApplyTo = (SELECT TOP 1 APPLY_TO FROM BRND_DISC_HEADER WHERE IDApp"
                    'End If
                ElseIf Mode = common.Helper.SaveMode.Update Then
                    Me.OpenConnection() : Me.BeginTransaction()
                    PKey = domPrice.IDApp
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "IF EXISTS(SELECT IDApp FROM BRND_DISC_HEADER WHERE IDApp = @IDApp) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "  UPDATE BRND_DISC_HEADER SET PROGRAM_ID = @PROGRAM_ID, APPLY_DATE = @APPLY_DATE,END_DATE = @END_DATE,APPLY_TO = @APPLY_TO,PROGRAM_DESC = @PROGRAM_DESC,DISC_PROG_DESC = @DISC_PROG_DESC,ModifiedBy = @ModifiedBy,ModifiedDate = @ModifiedDate " & vbCrLf & _
                            "  WHERE IDApp = @IDApp ;" & vbCrLf & _
                            " END "
                    commandUpdate = SqlConn.CreateCommand()
                    With commandUpdate
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@IDApp", SqlDbType.Int).Value = PKey
                        .Parameters.Add("@PROGRAM_ID", SqlDbType.VarChar).Value = domPrice.ProgramID
                        .Parameters.Add("@APPLY_DATE", SqlDbType.SmallDateTime).Value = domPrice.ApplyDate
                        .Parameters.Add("@END_DATE", SqlDbType.SmallDateTime).Value = domPrice.EndDate
                        .Parameters.Add("@APPLY_TO", SqlDbType.VarChar, 2).Value = domPrice.ApplyTo
                        .Parameters.Add("@PROGRAM_DESC", SqlDbType.VarChar, 200).Value = domPrice.NameApp
                        .Parameters.Add("@DISC_PROG_DESC", SqlDbType.VarChar, 500).Value = BrndProgDesc
                        .Parameters.Add("@ModifiedBy", SqlDbType.VarChar).Value = domPrice.ModifiedBy
                        .Parameters.Add("@ModifiedDate", SqlDbType.SmallDateTime).Value = domPrice.ModifiedDate
                        .Transaction = Me.SqlTrans
                        .ExecuteScalar() : .Parameters.Clear()
                    End With
                    ''delete BRND_DISC_LIST_BRAND,BRND_DISC_PROG,DIST_DISC_BRAND_LIST,DIST_GROUP_BRND_LIST
                    Query = "SET NOCOUNT ON; " & vbCrLf
                    Dim hasQuery = False
                    If domPrice.HasChangedBrands Then
                        Query = Query & " DELETE FROM BRND_DISC_LIST_BRAND WHERE FKApp = @FkApp;" & vbCrLf
                        hasQuery = True
                    End If
                    If domPrice.HasChangedBrandPacks Then
                        Query = Query & " DELETE FROM BRND_DISC_LIST_BRANDPACK WHERE FKApp = @FkApp;" & vbCrLf
                        hasQuery = True
                    End If
                    If domPrice.HasChangedDistr Then
                        Query = Query & " DELETE FROM DIST_DISC_BRAND_LIST WHERE FKApp = @FKApp;" & vbCrLf
                        hasQuery = True
                    End If
                    If domPrice.HasChangedGroups Then
                        Query = Query & " DELETE FROM DIST_GROUP_BRND_LIST WHERE FKDisc = @FKApp;"
                        hasQuery = True
                    End If
                    If hasQuery Then
                        commandDelete = SqlConn.CreateCommand()
                        With commandDelete
                            .CommandType = CommandType.Text
                            .CommandText = Query
                            .Parameters.Add("@FKApp", SqlDbType.Int).Value = PKey
                            .Transaction = Me.SqlTrans
                            .ExecuteScalar() : .Parameters.Clear()
                        End With
                    End If
                End If
                SqlDat = New SqlDataAdapter()
                If domPrice.HasChangedBrands Then
                    'If domPrice.ListBrands.Count <= 0 Then : Throw New Exception("Please mark brand(s)") : End If
                    InsertedRows = tblBrand.Select("", "", DataViewRowState.Added)
                    If InsertedRows.Length > 0 Then
                        'INSERT BRND_DISC_LIST
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                " INSERT INTO BRND_DISC_LIST_BRAND(BRAND_ID,FKApp,CreatedBy,CreatedDate) " & vbCrLf & _
                                " VALUES(@BRAND_ID,@FKApp,@CreatedBy,@CreatedDate);"
                        If IsNothing(commandInsert) Then
                            commandInsert = Me.SqlConn.CreateCommand()
                        End If
                        With commandInsert
                            .CommandText = Query
                            .CommandType = CommandType.Text
                            .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 14, "BRAND_ID")
                            .Parameters.Add("@FKApp", SqlDbType.Int).Value = PKey
                            .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                            .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                            If IsNothing(.Transaction) Then
                                .Transaction = Me.SqlTrans
                            End If
                        End With
                        SqlDat.InsertCommand = commandInsert
                        SqlDat.Update(InsertedRows)
                        SqlDat.InsertCommand = Nothing
                        commandInsert.Parameters.Clear()
                    End If
                End If
                If domPrice.HasChangedBrandPacks Then
                    'If domPrice.ListBrandPacks.Count <= 0 Then : Throw New Exception("Please mark brandpack(s)") : End If
                    If Not IsNothing(tblBrandPack) Then
                        InsertedRows = tblBrandPack.Select("", "", DataViewRowState.Added)
                        If InsertedRows.Length > 0 Then
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " INSERT INTO BRND_DISC_LIST_BRANDPACK(BRANDPACK_ID,FKApp,CreatedBy,CreatedDate) " & vbCrLf & _
                                    " VALUES(@BRANDPACK_ID,@FKApp,@CreatedBy,@CreatedDate);"
                            If IsNothing(commandInsert) Then
                                commandInsert = Me.SqlConn.CreateCommand()
                            End If
                            With commandInsert
                                .CommandText = Query
                                .CommandType = CommandType.Text
                                .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                                .Parameters.Add("@FKApp", SqlDbType.Int).Value = PKey
                                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                                .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                                If IsNothing(.Transaction) Then
                                    .Transaction = Me.SqlTrans
                                End If
                            End With
                            SqlDat.InsertCommand = commandInsert
                            SqlDat.Update(InsertedRows)
                            SqlDat.InsertCommand = Nothing
                            commandInsert.Parameters.Clear()
                        End If
                    End If
                End If
                'insert schema discount
                InsertedRows = domPrice.DsProgDesc.Tables(0).Select("", "", DataViewRowState.Added)
                Dim UpdatedRows() As DataRow = domPrice.DsProgDesc.Tables(0).Select("", "", DataViewRowState.ModifiedOriginal)
                Dim DeletedRows() As DataRow = domPrice.DsProgDesc.Tables(0).Select("", "", DataViewRowState.Deleted)
                If InsertedRows.Length > 0 Then
                    If IsNothing(commandInsert) Then
                        commandInsert = SqlConn.CreateCommand()
                    End If
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " INSERT INTO dbo.BRND_DISC_PROG(MoreThanQty,Disc,TypeApp,FKApp,BRAND_ID,BRANDPACK_ID,CreatedBy,CreatedDate)" & vbCrLf & _
                            " VALUES(@MoreThanQty,@Disc,@TypeApp,@FKApp,@BRAND_ID,@BRANDPACK_ID,@CreatedBy,@CreatedDate) ;"
                    With commandInsert
                        .CommandText = Query
                        .CommandType = CommandType.Text
                        .Parameters.Add("@MoreThanQty", SqlDbType.Decimal, 0, "MoreThanQty")
                        .Parameters.Add("@Disc", SqlDbType.Decimal, 0, "Disc")
                        .Parameters.Add("@TypeApp", SqlDbType.VarChar, 3, "TypeApp")
                        .Parameters.Add("@FKApp", SqlDbType.Int, 0).Value = PKey
                        .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                        .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                        .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                        .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 7, "BRAND_ID")
                        If IsNothing(.Transaction) Then
                            .Transaction = Me.SqlTrans
                        End If
                    End With
                    SqlDat.InsertCommand = commandInsert
                    SqlDat.Update(InsertedRows)
                    SqlDat.InsertCommand = Nothing
                    commandInsert.Parameters.Clear()
                End If
                If UpdatedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                           " UPDATE BRND_DISC_PROG SET MoreThanQty = @MoreThanQty,Disc = @Disc,TypeApp = @TypeApp,BRAND_ID = @BRAND_ID,BRANDPACK_ID = @BRANDPACK_ID,ModifiedBy = @ModifiedBy,ModifiedDate = @ModifiedDate " & vbCrLf & _
                           " WHERE IDApp = @IDApp ; "
                    If IsNothing(commandUpdate) Then
                        commandUpdate = SqlConn.CreateCommand()
                    End If
                    With commandUpdate
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@MoreThanQty", SqlDbType.Decimal, 0, "MoreThanQty")
                        .Parameters.Add("@Disc", SqlDbType.Decimal, 0, "Disc")
                        .Parameters.Add("@TypeApp", SqlDbType.VarChar, 2, "TypeApp")
                        .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                        .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                        .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100, "ModifiedBy")
                        .Parameters.Add("@ModifiedDate", SqlDbType.SmallDateTime, 0, "ModifiedDate")
                        .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                        .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 7, "BRAND_ID")
                        If IsNothing(.Transaction) Then
                            .Transaction = Me.SqlTrans
                        End If
                    End With
                    SqlDat.UpdateCommand = commandUpdate
                    SqlDat.Update(UpdatedRows)
                    commandUpdate.Parameters.Clear()
                    SqlDat.UpdateCommand = Nothing
                End If
                If DeletedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " DELETE FROM BRND_DISC_PROG WHERE IDApp = @IDApp ;"
                    If IsNothing(commandDelete) Then
                        commandDelete = Me.SqlConn.CreateCommand()
                    End If
                    With commandDelete
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                        .Parameters("@IDApp").SourceVersion = DataRowVersion.Original
                        If IsNothing(.Transaction) Then
                            .Transaction = Me.SqlTrans
                        End If
                    End With
                    SqlDat.DeleteCommand = commandDelete
                    SqlDat.Update(DeletedRows)
                    commandDelete.Parameters.Clear()
                    SqlDat.DeleteCommand = Nothing
                End If
                Select Case domPrice.ApplyTo
                    Case "AL"
                        'Query = "SET NOCOUNT ON; " & vbCrLf & _
                        '        " DECLARE @V_LastApplyDate SMALLDATETIME; " & vbCrLf & _
                        '        " SET @V_LastApplyDate (SELECT TOP 1 APPLY_DATE FROM BRND_DISC_HEADER WHERE APPLY_DATE > CONVERT(VARCHAR(100),@ApplyDate,101) ORDER BY APPLY_DATE ASC);" & vbCrLf & _
                        '        " SELECT 1 WHERE EXISTS(SELECT DLB.BRAND_ID FROM BRND_DISC_LIST_BRAND DLB INNER JOIN BRND_DISC_HEADER DH ON DH.IDApp = DLB.FKApp " & vbCrLf & _
                        '        " WHERE DLB.BRAND_ID " & strListBrands & vbCrLf & _
                        '        " AND DH.APPLY_DATE >= CONVERT(VARCHAR(100),@ApplyDate,101) AND DH.APPLY_DATE < @V_LastApplyDate) ; "
                        'Hapus Semua data
                        'Query = "SET NOCOUNT ON;" & VBCRLF & _
                        '"DELETE FROM 
                    Case "CD"
                        If domPrice.ListDistributors.Count <= 0 Then : Throw New Exception("Please mark Distributor(s)") : End If
                        If domPrice.HasChangedDistr Then
                            Query = "SET NOCOUNT ON;" & vbCrLf & _
                                    " INSERT INTO DIST_DISC_BRAND_LIST(DISTRIBUTOR_ID,FKApp,CreatedBy,CreatedDate)" & vbCrLf & _
                                    " VALUES (@DISTRIBUTOR_ID,@FKApp,@CreatedBy, @CreatedDate) ;"
                            If IsNothing(commandInsert) Then
                                commandInsert = Me.SqlConn.CreateCommand()
                            End If
                            With commandInsert
                                .CommandText = Query
                                .CommandType = CommandType.Text
                                .Parameters.Add("@DISTRIBUTOR_ID", SqlDbType.VarChar, 10, "DISTRIBUTOR_ID")
                                .Parameters.Add("@FKApp", SqlDbType.Int).Value = PKey
                                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                                .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                                If IsNothing(.Transaction) Then
                                    .Transaction = Me.SqlTrans
                                End If
                            End With
                            SqlDat.InsertCommand = commandInsert
                            InsertedRows = tblDistr.Select("", "", DataViewRowState.Added)
                            SqlDat.Update(InsertedRows)
                            SqlDat.InsertCommand = Nothing
                            commandInsert.Parameters.Clear()
                        End If
                    Case "GD"
                        'If domPrice.ListGroupDist.Count <= 0 Then : Throw New Exception("Please mark group(s)") : End If
                        'Dim strListGroups As String = "IN('"
                        'For i As Integer = 0 To domPrice.ListGroupDist.Count - 1
                        '    strListGroups = strListGroups & domPrice.ListGroupDist(i) & "'"
                        '    If i < domPrice.ListGroupDist.Count - 1 Then
                        '        strListGroups = strListGroups & ",'"
                        '    End If
                        'Next
                        'strListGroups = strListGroups & ")"
                        'Query = "SET NOCOUNT ON; " & vbCrLf & _
                        '        " DECLARE @V_LastApplyDate SMALLDATETIME; " & vbCrLf & _
                        '       " SET @V_LastApplyDate (SELECT TOP 1 APPLY_DATE FROM BRND_DISC_HEADER WHERE APPLY_DATE > CONVERT(VARCHAR(100),@ApplyDate,101) ORDER BY APPLY_DATE ASC);" & vbCrLf & _
                        '       " SELECT 1 WHERE EXISTS(SELECT DLB.BRAND_ID FROM BRND_DISC_LIST_BRAND DLB INNER JOIN BRND_DISC_HEADER DH ON DH.IDApp = DLB.FKApp  " & vbCrLf & _
                        '       " INNER JOIN DIST_GROUP_BRAND_LIST GBL ON GBL.FKDisc = DH.IDApp " & vbCrLf & _
                        '       " INNER JOIN DIST_GROUP_LIST DGL ON DGL.GRP_ID = GBL.FKGroup " & vbCrLf & _
                        '       " WHERE DLB.BRAND_ID " & strListBrands & vbCrLf & _
                        '       " AND DGL.DISTRIBUTOR_ID " & strListGroups & vbCrLf & _
                        '       " AND DH.APPLY_DATE >= CONVERT(VARCHAR(100),@ApplyDate,101) AND DH.APPLY_DATE < @V_LastApplyDate) ; "
                        If domPrice.HasChangedGroups Then
                            Query = "SET NOCOUNT ON;" & vbCrLf & _
                                    " INSERT INTO DIST_GROUP_BRND_LIST(FKGroup,FKDisc,CreatedBy,CreatedDate)" & vbCrLf & _
                                    " VALUES(@FKGroup,@FKDisc,@CreatedBy,@CreatedDate) ;"
                            If IsNothing(commandInsert) Then
                                commandInsert = Me.SqlConn.CreateCommand()
                            End If
                            With commandInsert
                                .CommandText = Query
                                .CommandType = CommandType.Text
                                .Parameters.Add("@FKGroup", SqlDbType.VarChar, 10, "FKGroup")
                                .Parameters.Add("@FKDisc", SqlDbType.Int).Value = PKey
                                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 100, "CreatedBy")
                                .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                                If IsNothing(.Transaction) Then
                                    .Transaction = Me.SqlTrans
                                End If
                            End With
                            SqlDat.InsertCommand = commandInsert
                            InsertedRows = tblGroups.Select("", "", DataViewRowState.Added)
                            SqlDat.Update(InsertedRows)
                            SqlDat.InsertCommand = Nothing
                            commandInsert.Parameters.Clear()
                        End If
                End Select
                Me.CommiteTransaction() : Me.ClearCommandParameters() : If mustCloseConnection Then : Me.CloseConnection() : End If
                Return True
            Catch ex As Exception
                Me.RollbackTransaction() : Me.ClearCommandParameters()
                Me.CloseConnection()
                If Not IsNothing(commandInsert) Then
                    commandInsert.Dispose() : commandInsert = Nothing
                End If
                If Not IsNothing(commandUpdate) Then
                    commandUpdate.Dispose() : commandUpdate = Nothing
                End If
                If Not IsNothing(commandDelete) Then
                    commandDelete.Dispose() : commandDelete = Nothing
                End If
                Throw ex
            End Try
        End Function
        Public Function getDistributor(ByVal IDApp As Integer, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal ListCheckBrands As List(Of String), ByVal ListCheckBrandPacks As List(Of String), ByRef listCheckedDist As List(Of String), ByVal mustCloseConnection As Boolean) As DataView
            Try
                Dim strListBrands As String = "IN('", strListBrandPacks As String = "IN('"
                For I As Integer = 0 To ListCheckBrands.Count - 1
                    strListBrands = strListBrands + (ListCheckBrands(I) & "'")
                    If I < ListCheckBrands.Count - 1 Then
                        strListBrands &= ",'"
                    End If
                Next
                strListBrands &= ")"
                If ListCheckBrandPacks.Count > 0 Then
                    For I As Integer = 0 To ListCheckBrandPacks.Count - 1
                        strListBrandPacks = strListBrandPacks + (ListCheckBrandPacks(I) & "'")
                        If I < ListCheckBrandPacks.Count - 1 Then
                            strListBrandPacks &= ",'"
                        End If
                    Next
                    strListBrandPacks &= ")"
                    Query = "SET NOCOUNT ON " & vbCrLf & _
                              "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                              "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                              " INNER JOIN AGREE_BRANDPACK_INCLUDE ABP ON ABP.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                              " WHERE AA.START_DATE <= @StartDate AND AA.END_DATE >= @EndDate AND ABP.BRANDPACK_ID " & strListBrandPacks & " AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID);"
                Else
                    Query = "SET NOCOUNT ON " & vbCrLf & _
                            "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                            "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                            " INNER JOIN AGREE_BRAND_INCLUDE ABI ON ABI.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                            " WHERE AA.START_DATE <= @StartDate AND AA.END_DATE >= @EndDate AND ABI.BRAND_ID " & strListBrands & " AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID);"
                End If

                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Dim dtTable As New DataTable("T_DiscPrice") : dtTable.Clear()
                Me.OpenConnection()
                Me.FillDataTable(dtTable)
                If IDApp > 0 Then

                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "SELECT DISTRIBUTOR_ID FROM DIST_DISC_BRAND_LIST WHERE FKApp = @FKApp ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@FkApp", SqlDbType.Int, IDApp)
                    Me.ExecuteReader()
                    While Me.SqlRe.Read()
                        If Not listCheckedDist.Contains(SqlRe.GetString(0)) Then
                            listCheckedDist.Add(SqlRe.GetString(0))
                        End If
                    End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If : Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Sub GetCurrentAgreement(ByRef StartDate As DateTime, ByRef EndDate As DateTime, ByVal mustCloseConnection As Boolean)
            Try
                Dim Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT TOP 1 START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE END_DATE >= CONVERT(VARCHAR(100),GETDATE(),101); "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteReader() : Me.ClearCommandParameters()
                While Me.SqlRe.Read()
                    StartDate = SqlRe.GetDateTime(0)
                    EndDate = SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close()
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        Public Function getDiscount(ByVal TypeApp As String, ByVal BrandPackID As String, ByVal BrandPackName As String, ByVal distributorID As String, ByVal PODate As DateTime, ByVal Qty As Decimal, ByVal OA_BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_DD_DR_CBD", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_DD_DR_CBD")
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID)
                Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PODate)
                Me.AddParameter("@OA_QTY", SqlDbType.Decimal, Qty)
                'Me.SqlCom.Parameters("@OA_QTY").Precision = 3
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID)
                Me.AddParameter("@TypeApp", SqlDbType.VarChar, TypeApp)
                'Me.AddParameter("@BRANDPACK_NAME", SqlDbType.VarChar, BrandPackName)
                Me.AddParameter("@COMP_NAME", SqlDbType.NVarChar, Me.ComputerName)
                Me.OpenConnection()
                Dim dt As DataTable = New DataTable("T_DDDRCBD")
                setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                Dim RetDT As DataTable = dt.Copy()
                RetDT.Clear()
                For Each row As DataRow In dt.Rows
                    Dim B As Boolean = True
                    If Not IsNothing(row("BRANDPACK_ID")) And Not IsDBNull(row("BRANDPACK_ID")) Then
                        If row("BRANDPACK_ID").ToString() <> "" Then
                            If row("BRANDPACK_ID").ToString() <> BrandPackID Then
                                B = False
                            End If
                        End If
                    End If
                    If CDec(row("INC_DISC")) <= 0 Then
                        B = False
                    End If
                    If B Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT 1 WHERE EXISTS(SELECT TOP 1 FK_BRND_DISC_PROG FROM ORDR_OA_BRANDPACK_DISC " & vbCrLf & _
                                " WHERE FK_BRND_DISC_PROG = @FK_BRND_DISC_PROG AND GQSY_SGT_P_FLAG = @TypeApp AND OA_BRANDPACK_ID = @OA_BRANDPACK_ID) ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@FK_BRND_DISC_PROG", SqlDbType.Int, row("FK_BRND_DISC_PROG"))
                        Me.AddParameter("@TypeApp", SqlDbType.VarChar, "O" + row("DISC_TYPE").ToString())
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID)
                        Dim retval As Object = Me.SqlCom.ExecuteScalar()
                        If Not IsNothing(retval) And Not IsDBNull(retval) Then
                        Else
                            RetDT.ImportRow(row)
                        End If
                    End If
                Next
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return RetDT
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Function getDiscount(ByVal OABrandPackID As String, ByVal TypeApp As String, ByVal BrandPackID As String, ByVal distributorID As String, ByVal PODate As DateTime, ByVal Qty As Decimal, ByRef RefOther As Int32, ByRef Info() As String, ByVal mustCloseConnection As Boolean) As Decimal
            Try
                Dim result As Decimal = 0
                Dim IDApp As Integer = 0, ApplyTo As String = "AL", ProgramID As String = "", programDesc As String = "", retval As Object = Nothing, ApplyDate As DateTime = DateTime.Now, EndDate As DateTime = DateTime.Now
                Dim MoreThanQty As Decimal = 0, Disc As Decimal = 0
                Query = "SELECT 1 WHERE EXISTS(SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID AND GQSY_SGT_P_FLAG = @FLAG);"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OABrandPackID, 70) ' VARCHAR(44),
                Me.AddParameter("@FLAG", SqlDbType.VarChar, "O" & TypeApp, 5)
                Me.OpenConnection()
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then
                        If mustCloseConnection Then : Me.CloseConnection() : End If : Me.ClearCommandParameters() : Return 0
                    End If
                End If
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT TOP 1 AA.START_DATE, AA.END_DATE FROM AGREE_AGREEMENT AA  " & vbCrLf & _
                        " INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN AGREE_BRANDPACK_INCLUDE ABP ON ABP.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                        " WHERE AA.END_DATE >= @PO_DATE AND AA.START_DATE <= @PO_DATE AND DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ABP.BRANDPACK_ID = @BRANDPACK_ID ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID, 10)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PODate)
                Dim StartDatePKD As Object = Nothing, EndDatePKD As Object = Nothing
                Me.OpenConnection()
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    StartDatePKD = SqlRe.GetDateTime(0)
                    EndDatePKD = SqlRe.GetDateTime(1)
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                ''AMBIL YANG Apply_to =  al
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT TOP 1 BD.IDApp,ISNULL(PROGRAM_ID,CONVERT(VARCHAR(10),BD.IDApp))AS PROGRAM_ID,BD.PROGRAM_DESC, BD.APPLY_TO,BD.APPLY_DATE,END_DATE FROM BRND_DISC_HEADER BD INNER JOIN BRND_DISC_PROG BDP ON BD.IDApp = BDP.FKApp " & vbCrLf & _
                        " INNER JOIN BRND_DISC_LIST_BRAND BDL ON BDL.FKApp = BD.IDApp INNER JOIN BRND_BRANDPACK BB ON BB.BRAND_ID = BDL.BRAND_ID " & vbCrLf & _
                        " INNER JOIN BRND_DISC_LIST_BRANDPACK BDLB ON BDLB.BRANDPACK_ID = BB.BRANDPACK_ID AND BDLB.FKAPP = BD.IDApp " & vbCrLf & _
                        " WHERE BD.APPLY_DATE <= @PO_DATE  AND END_DATE >= @PO_DATE AND BB.BRANDPACK_ID = @BRANDPACK_ID " & vbCrLf & _
                        " AND BD.APPLY_DATE >= @START_DATE_PKD AND BD.END_DATE <= @END_DATE_PKD AND BDP.TypeApp = @TypeApp " & vbCrLf & _
                        " AND BD.APPLY_TO = 'AL' ORDER BY BD.APPLY_DATE DESC ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PODate)
                Me.AddParameter("@START_DATE_PKD", SqlDbType.DateTime, StartDatePKD)
                Me.AddParameter("@END_DATE_PKD", SqlDbType.DateTime, EndDatePKD)
                Me.AddParameter("@TypeApp", SqlDbType.VarChar, TypeApp)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID, 10)
                Me.ExecuteReader()
                While Me.SqlRe.Read()
                    IDApp = Me.SqlRe.GetInt32(0)
                    ProgramID = Me.SqlRe.GetString(1)
                    programDesc = Me.SqlRe.GetString(2)
                    ApplyTo = Me.SqlRe.GetString(3)
                    ApplyDate = Me.SqlRe.GetDateTime(4)
                    EndDate = Me.SqlRe.GetDateTime(5)
                End While : Me.SqlRe.Close() : Me.ClearCommandParameters()

                If IDApp <= 0 Then
                    ''get data by certain distributor DIRECT BRANPACK
                    Query = "SET NOCOUNT ON;  " & vbCrLf & _
                            " SELECT TOP 1 BD.IDApp,ISNULL(PROGRAM_ID,CONVERT(VARCHAR(10),BD.IDApp))AS PROGRAM_ID,BD.PROGRAM_DESC, BD.APPLY_TO,BD.APPLY_DATE,END_DATE FROM BRND_DISC_HEADER BD INNER JOIN BRND_DISC_PROG BDP ON BD.IDApp = BDP.FKApp  " & vbCrLf & _
                            " INNER JOIN BRND_DISC_LIST_BRAND BDL ON BDL.FKApp = BD.IDApp INNER JOIN BRND_BRANDPACK BB ON BB.BRAND_ID = BDL.BRAND_ID  " & vbCrLf & _
                            " INNER JOIN DIST_DISC_BRAND_LIST DDBL ON DDBL.FKApp = BD.IDApp " & vbCrLf & _
                            " INNER JOIN BRND_DISC_LIST_BRANDPACK BDLB ON BDLB.FKApp = BD.IDApp" & vbCrLf & _
                            " AND BDP.BRANDPACK_ID = BDLB.BRANDPACK_ID and BDLB.BRANDPACK_ID = BB.BRANDPACK_ID " & vbCrLf & _
                            " WHERE BD.APPLY_DATE <= @PO_DATE  AND END_DATE >= @PO_DATE AND BB.BRANDPACK_ID =@BRANDPACK_ID" & vbCrLf & _
                            " and BDP.TypeApp = @TypeApp " & vbCrLf & _
                            " AND DDBL.DISTRIBUTOR_ID = @DISTRIBUTOR_ID ORDER BY BD.APPLY_DATE DESC"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PODate)
                    Me.AddParameter("@START_DATE_PKD", SqlDbType.DateTime, StartDatePKD)
                    Me.AddParameter("@END_DATE_PKD", SqlDbType.DateTime, EndDatePKD)
                    Me.AddParameter("@TypeApp", SqlDbType.VarChar, TypeApp)
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID, 10)
                    Me.ExecuteReader()
                    While Me.SqlRe.Read()
                        IDApp = Me.SqlRe.GetInt32(0)
                        ProgramID = Me.SqlRe.GetString(1)
                        programDesc = Me.SqlRe.GetString(2)
                        ApplyTo = Me.SqlRe.GetString(3)
                        ApplyDate = Me.SqlRe.GetDateTime(4)
                        EndDate = Me.SqlRe.GetDateTime(5)
                    End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                End If
                'get data by certain distributor by brand only
                If IDApp <= 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                           " SELECT TOP 1 BD.IDApp,ISNULL(PROGRAM_ID,CONVERT(VARCHAR(10),BD.IDApp))AS PROGRAM_ID,BD.PROGRAM_DESC, BD.APPLY_TO,BD.APPLY_DATE,END_DATE FROM BRND_DISC_HEADER BD INNER JOIN BRND_DISC_PROG BDP ON BD.IDApp = BDP.FKApp " & vbCrLf & _
                           " INNER JOIN BRND_DISC_LIST_BRAND BDL ON BDL.FKApp = BD.IDApp INNER JOIN BRND_BRANDPACK BB ON BB.BRAND_ID = BDL.BRAND_ID " & vbCrLf & _
                           " INNER JOIN DIST_DISC_BRAND_LIST DDBL ON DDBL.FKApp = BD.IDApp " & vbCrLf & _
                           " WHERE BD.APPLY_DATE <= @PO_DATE  AND END_DATE >= @PO_DATE AND BB.BRANDPACK_ID = @BRANDPACK_ID " & vbCrLf & _
                           " AND BD.APPLY_DATE >= @START_DATE_PKD AND BD.END_DATE <= @END_DATE_PKD AND BDP.TypeApp = @TypeApp " & vbCrLf & _
                           " AND DDBL.DISTRIBUTOR_ID = @DISTRIBUTOR_ID ORDER BY BD.APPLY_DATE DESC ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PODate)
                    Me.AddParameter("@START_DATE_PKD", SqlDbType.DateTime, StartDatePKD)
                    Me.AddParameter("@END_DATE_PKD", SqlDbType.DateTime, EndDatePKD)
                    Me.AddParameter("@TypeApp", SqlDbType.VarChar, TypeApp)
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID, 10)
                    Me.ExecuteReader()
                    While Me.SqlRe.Read()
                        IDApp = Me.SqlRe.GetInt32(0)
                        ProgramID = Me.SqlRe.GetString(1)
                        programDesc = Me.SqlRe.GetString(2)
                        ApplyTo = Me.SqlRe.GetString(3)
                        ApplyDate = Me.SqlRe.GetDateTime(4)
                        EndDate = Me.SqlRe.GetDateTime(5)
                    End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                End If
                If IDApp > 0 Then
                    RefOther = IDApp
                    'check apakah discount ke brandpack atau ke brand
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT 1 WHERE EXISTS(SELECT BRANDPACK_ID FROM BRND_DISC_LIST_BRANDPACK WHERE FKApp = @IDApp)"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If Not IsNothing(retval) Then
                        'discount berdasarkan brandpack
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT 1 WHERE EXISTS(SELECT IDApp FROM BRND_DISC_LIST_BRANDPACK WHERE FKApp = @IDApp AND BRANDPACK_ID =@BRANDPACK_ID)"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                        retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        If IsNothing(retval) Then
                            Me.CloseConnection()
                            Return 0
                        End If
                    End If
                    'check brandnya apakah ada dilist program
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " DECLARE @V_BRAND_ID VARCHAR(7); " & vbCrLf & _
                    " SET @V_BRAND_ID = (SELECT BRAND_ID FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID) ;" & vbCrLf & _
                    " SELECT BRAND_ID FROM BRND_DISC_LIST_BRAND WHERE BRAND_ID = @V_BRAND_ID AND FKApp = @IDApp ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                    Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If IsNothing(retval) Then
                        Me.CloseConnection()
                        Return 0
                    End If
                    Dim BrandID As String = retval.ToString()
                    Select Case ApplyTo
                        Case "AL"
                            Exit Select
                        Case "CD"
                            Query = " SET NOCOUNT ON;" & vbCrLf & _
                                    " DECLARE @V_START_DATE_PKD SMALLDATETIME,@V_END_DATE_PKD SMALLDATETIME ;" & vbCrLf & _
                                    " SELECT TOP 1 @V_START_DATE_PKD = AA.START_DATE,@V_END_DATE_PKD = AA.END_DATE FROM AGREE_AGREEMENT AA " & vbCrLf & _
                                    " INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN AGREE_BRANDPACK_INCLUDE ABP ON ABP.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                    " WHERE AA.END_DATE >= @PODate AND AA.START_DATE <= @PODate AND DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ABP.BRANDPACK_ID = @BRANDPACK_ID ;" & vbCrLf & _
                                    " IF (@APPLY_DATE >=@V_START_DATE_PKD) AND (@END_DATE <=@V_END_DATE_PKD) " & vbCrLf & _
                                    " BEGIN " & vbCrLf & _
                                    "   SELECT DISTRIBUTOR_ID FROM DIST_DISC_BRAND_LIST WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND FKApp = @IDApp; " & vbCrLf & _
                                    " END "
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID, 10)
                            Me.AddParameter("@APPLY_DATE", SqlDbType.Date, ApplyDate)
                            Me.AddParameter("@END_DATE", SqlDbType.Date, EndDate)
                            Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                            Me.AddParameter("@PODate", SqlDbType.SmallDateTime, PODate)
                            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                            retval = Me.SqlCom.ExecuteScalar()
                            If IsNothing(retval) Then
                                Me.CloseConnection() : Me.ClearCommandParameters()
                                Return 0
                            End If
                        Case "GD"
                            Query = "SET NOCOUNT ON;" & vbCrLf & _
                                    " DECLARE @V_START_DATE_PKD SMALLDATETIME,@V_END_DATE_PKD SMALLDATETIME ;" & vbCrLf & _
                                    " SELECT TOP 1 @V_START_DATE_PKD = AA.START_DATE,@V_END_DATE_PKD = AA.END_DATE FROM AGREE_AGREEMENT AA " & vbCrLf & _
                                    " INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN AGREE_BRANDPACK_INCLUDE ABP ON ABP.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                    " WHERE AA.END_DATE >= @PODate AND AA.START_DATE <= @PODate AND DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ABP.BRANDPACK_ID = @BRANDPACK_ID ;" & vbCrLf & _
                                    " IF (@APPLY_DATE >=@V_START_DATE_PKD) AND (@END_DATE <=@V_END_DATE_PKD) " & vbCrLf & _
                                    " BEGIN " & vbCrLf & _
                                    " SELECT DGL.DISTRIBUTOR_ID FROM DIST_GROUP_LIST DGL INNER JOIN DIST_GROUP_BRND_LIST GBL ON GBL.FKGroup = DGL.GRP_ID " & vbCrLf & _
                                    " WHERE GBL.FKDisc = @IDApp AND DGL.DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;" & vbCrLf & _
                                    " END "
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, distributorID, 10)
                            Me.AddParameter("@APPLY_DATE", SqlDbType.Date, ApplyDate)
                            Me.AddParameter("@END_DATE", SqlDbType.Date, EndDate)
                            Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                            Me.AddParameter("@PODate", SqlDbType.SmallDateTime, PODate)
                            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                            retval = Me.SqlCom.ExecuteScalar()
                            If IsNothing(retval) Then
                                Me.CloseConnection() : Me.ClearCommandParameters()
                                Return 0
                            End If
                    End Select
                    'ambil discount nya
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT BRAND_ID,BRANDPACK_ID,MoreThanQty,Disc FROM BRND_DISC_PROG WHERE FKApp = @IDApp AND TypeApp = @TypeApp ORDER BY MoreThanQty DESC,Disc DESC;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                    Me.AddParameter("@TypeApp", SqlDbType.VarChar, TypeApp)

                    Me.ExecuteReader()
                    While Me.SqlRe.Read()
                        Dim OBrandPackID As Object = Me.SqlRe(1)
                        Dim OBrandID As Object = Me.SqlRe(0)
                        MoreThanQty = Me.SqlRe.GetDecimal(2)
                        Disc = Me.SqlRe.GetDecimal(3)
                        If Not IsNothing(OBrandPackID) And Not IsDBNull(OBrandPackID) Then
                            If OBrandPackID.ToString() = BrandPackID Then
                                If Qty >= MoreThanQty Then
                                    result = Qty * (Disc / 100)
                                End If
                                Exit While
                            End If
                        ElseIf Not IsNothing(OBrandID) And Not IsDBNull(OBrandID) Then
                            If OBrandID.ToString() = BrandID Then
                                If Qty >= MoreThanQty Then
                                    result = Qty * (Disc / 100)
                                End If
                                Exit While
                            End If
                        Else
                            If Qty >= MoreThanQty Then
                                result = Qty * (Disc / 100)
                                Exit While
                            End If
                        End If
                    End While : Me.SqlRe.Close() : Me.ClearCommandParameters()

                End If
                If ApplyTo = "AL" Then
                    ApplyTo = "All Distributor(s)" : Else : ApplyTo = "Certain Distributor"
                End If
                If result > 0 Then
                    Dim progDesc As String = "Program ID = " & ProgramID & ", Program Name/Desc = " & programDesc
                    Dim progInfo As String = String.Format(New CultureInfo("id-ID"), "Apply Disc to " & ApplyTo & ", Apply_Date {0:dd-MMMM-yyyy}, End_Date {1:dd-MMMM-yyyy}", ApplyDate, EndDate)
                    Dim discInfo As String = String.Format("{0:#,##0.000} X {1:p}", Qty, Disc / 100)
                    Info = New String() {progDesc, progInfo, discInfo}
                End If
                If mustCloseConnection Then : Me.CloseConnection() : Me.ClearCommandParameters() : End If
                Return result
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
            Return 0
        End Function
    End Class
End Namespace

