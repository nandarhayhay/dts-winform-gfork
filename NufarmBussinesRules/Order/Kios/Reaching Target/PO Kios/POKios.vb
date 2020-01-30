Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Namespace Kios
    Public Class POKios : Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private DVSalesDate As DataView = Nothing
        Private ViewName As String = ""
        Private ListYearTable As New List(Of String)
        Private ListYear As New List(Of Integer)
        Private OriginalStartDate As Integer = 0, OriginalEndDate As Integer = 0
        Private Query As String = ""
        Private Function CreateTablePO() As DataTable

            Dim DtTable As New DataTable("PO")
            DtTable.Columns.Add(New DataColumn("CodeApp", Type.GetType("System.String")))
            DtTable.Columns.Add(New DataColumn("OwnerApp", Type.GetType("System.String")))
            DtTable.Columns.Add(New DataColumn("NameApp", Type.GetType("System.String")))
            DtTable.Columns.Add(New DataColumn("TypeApp", Type.GetType("System.String")))
            DtTable.Columns.Add(New DataColumn("POCode", Type.GetType("System.String")))
            DtTable.Columns.Add(New DataColumn("KiosCode", Type.GetType("System.String")))
            DtTable.Columns.Add(New DataColumn("DistributorCode", Type.GetType("System.String")))
            DtTable.Columns.Add(New DataColumn("TerritoryCode", Type.GetType("System.String")))
            DtTable.Columns.Add(New DataColumn("SalesDate", Type.GetType("System.DateTime")))
            DtTable.Columns.Add(New DataColumn("CreatedDate", Type.GetType("System.DateTime")))
            DtTable.Columns.Add(New DataColumn("CreatedBy", Type.GetType("System.String")))
            DtTable.Columns.Add(New DataColumn("ModifiedBy", Type.GetType("System.String")))
            DtTable.Columns.Add(New DataColumn("ModifiedDate", Type.GetType("System.DateTime")))
            Return DtTable
        End Function
        Private Sub FilterByYear(ByRef DVResult As DataView, ByVal DVOriginalToSearch As DataView, ByVal year As Integer)
            Dim drv As DataRowView = Nothing
            DVResult.Table.Clear()
            For i As Integer = 0 To DVOriginalToSearch.Count - 1
                If CInt(CDate(DVOriginalToSearch(i)("SalesDate")).Year) = year Then
                    'tambah satu row
                    drv = DVResult.AddNew()
                    drv("CodeApp") = DVOriginalToSearch(i)("CodeApp")
                    drv("TypeApp") = DVOriginalToSearch(i)("TypeApp")
                    drv("NameApp") = DVOriginalToSearch(i)("NameApp")
                    drv("POCode") = DVOriginalToSearch(i)("POCode")
                    drv("KiosCode") = DVOriginalToSearch(i)("KiosCode")
                    drv("DistributorCode") = DVOriginalToSearch(i)("DistributorCode")
                    drv("TerritoryCode") = DVOriginalToSearch(i)("TerritoryCode")
                    drv("SalesDate") = DVOriginalToSearch(i)("SalesDate")
                    drv("CreatedBy") = DVOriginalToSearch(i)("CreatedBy")
                    drv("CreatedDate") = DVOriginalToSearch(i)("CreatedDate")

                    drv.EndEdit()
                End If
            Next
        End Sub

        Public Sub InsertPO(ByVal ds As DataSet)
            Try
                Dim ListTahun As List(Of Integer) = New List(Of Integer)
                Dim DvChildPO As DataView = ds.Tables(1).DefaultView()
                DVSalesDate = ds.Tables(0).DefaultView() : DVSalesDate.Sort = "SalesDate ASC"
                Dim FirstYear As Integer = CInt(CDate(DVSalesDate(0)("SalesDate")).Year)
                ListTahun.Add(FirstYear)

                For i As Integer = 1 To ds.Tables(0).Rows.Count - 1
                    Dim OtherYear As Integer = CInt(CDate(DVSalesDate(i)("SalesDate")).Year)
                    If Not ListTahun.Contains(OtherYear) Then
                        ListTahun.Add(OtherYear)
                    End If
                Next
                'check tahun ke database
                Dim DtSearchData As DataTable = Me.CreateTablePO()
                ListTahun.Sort()
                If ListTahun.Count <= 0 Then
                    Return
                End If
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom = New SqlCommand()
                Me.SqlCom.Connection = Me.SqlConn : Me.SqlCom.Transaction = Me.SqlTrans
                For i As Integer = 0 To ListTahun.Count - 1
                    Dim SearchDataView As DataView = DtSearchData.DefaultView()
                    Me.FilterByYear(SearchDataView, DVSalesDate, ListTahun(i))
                    Query = "SET NOCOUNT ON;IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'base table'" _
                           & " AND TABLE_NAME = 'POHeader_" & ListTahun(i).ToString() & "')" & _
                    vbCrLf & " BEGIN " & _
                    vbCrLf & " CREATE TABLE [dbo].[POHeader_" & ListTahun(i).ToString() & "](" & _
                    vbCrLf & "[IDApp] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL," & _
                    vbCrLf & "[CodeApp] [varchar](34)NULL," & _
                    vbCrLf & "[TypeApp] [varchar](32)NULL," & _
                    vbCrLf & "[NameApp] [varchar](64)NULL," & _
                    vbCrLf & "[POCode] [varchar](32)NULL," & _
                    vbCrLf & "[KiosCode] [varchar](50)NULL," & _
                    vbCrLf & "[DistributorCode] [varchar](16)NULL," & _
                    vbCrLf & "[TerritoryCode] [varchar](16)NULL," & _
                    vbCrLf & "[SalesDate] [smalldatetime] NULL," & _
                    vbCrLf & "[CreatedBy] [varchar](50)NULL," & _
                    vbCrLf & "[CreatedDate] [smalldatetime] NULL," & _
                    vbCrLf & "[ModifiedBy] [varchar](50)NULL," & _
                    vbCrLf & "[ModifiedDate] [smalldatetime] NULL," & _
                    vbCrLf & ") ON [PRIMARY] " & _
                    vbCrLf & " CREATE  CLUSTERED  INDEX [IX_POHeader_" & ListTahun(i).ToString() & "]" & _
                    vbCrLf & " ON [dbo].[POHeader_" & ListTahun(i).ToString() & "]([POCode], [DistributorCode], [KiosCode], [SalesDate]) ON [PRIMARY];" & _
                    vbCrLf & " ALTER TABLE [dbo].[POHeader_" & ListTahun(i).ToString() & "]" & _
                    vbCrLf & " ADD CONSTRAINT [PK_POHeader_" & ListTahun(i).ToString() & "] UNIQUE  NONCLUSTERED " & _
                    vbCrLf & " ( " & _
                    vbCrLf & "  [IDApp] " & _
                    vbCrLf & " )  ON [PRIMARY] " & _
                    vbCrLf & " END "
                    Me.SqlCom.CommandText = "sp_executesql" : Me.SqlCom.CommandType = CommandType.StoredProcedure
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    For i_1 As Integer = 0 To SearchDataView.Count - 1
                        'Me.SqlCom.CommandType = CommandType.Text
                        Query = "SET NOCOUNT ON;IF NOT EXISTS(SELECT POCode FROM POHeader_" & ListTahun(i).ToString() _
                                & " WHERE POCode = @POCode AND TerritoryCode = @TerritoryCode)" & _
                         vbCrLf & " BEGIN " & _
                         vbCrLf & "INSERT INTO POHeader_" & ListTahun(i).ToString() _
                                & "(CodeApp,NameApp,TypeApp,POCode,KiosCode,DistributorCode,TerritoryCode,SalesDate,CreatedBy,CreatedDate)" _
                                & "VALUES(@CodeApp,@NameApp,@TypeApp,@POCode,@KiosCode,@DistributorCode,@TerritoryCode,@SalesDate,@CreatedBy,@CreatedDate)" & _
                         vbCrLf & " END " & _
                         vbCrLf & " ELSE " & _
                         vbCrLf & " BEGIN " & _
                         vbCrLf & " UPDATE POHeader_" & ListTahun(i).ToString() & " SET CodeApp = @CodeApp," _
                                & " NameApp = @NameApp,TypeApp = @TypeApp,KiosCode = @KiosCode,DistributorCode = @DistributorCode," _
                                & " SalesDate = @SalesDate,ModifiedBy = @ModifiedBy,ModifiedDate = @ModifiedDate WHERE POCode = @POCode AND " _
                                & " TerritoryCode = @TerritoryCode" & _
                         vbCrLf & " END "
                        Me.AddParameter("@CodeApp", SqlDbType.VarChar, SearchDataView(i_1)("CodeApp"))
                        Me.AddParameter("@NameApp", SqlDbType.VarChar, SearchDataView(i_1)("NameApp"))
                        Me.AddParameter("@TypeApp", SqlDbType.VarChar, SearchDataView(i_1)("TypeApp"))
                        Me.AddParameter("@POCode", SqlDbType.VarChar, SearchDataView(i_1)("POCode"))
                        Me.AddParameter("@KiosCode", SqlDbType.VarChar, SearchDataView(i_1)("KiosCode"))
                        Me.AddParameter("@DistributorCode", SqlDbType.VarChar, SearchDataView(i_1)("DistributorCode"))
                        Me.AddParameter("@TerritoryCode", SqlDbType.VarChar, SearchDataView(i_1)("TerritoryCode"))
                        Me.AddParameter("@SalesDate", SqlDbType.DateTime, Convert.ToDateTime(SearchDataView(i_1)("SalesDate")))
                        Me.AddParameter("@CreatedBy", SqlDbType.VarChar, SearchDataView(i_1)("CreatedBy"))
                        Me.AddParameter("@CreatedDate", SqlDbType.DateTime, Convert.ToDateTime(SearchDataView(i_1)("CreatedDate")))
                        If SearchDataView(i_1)("ModifiedBy") Is DBNull.Value Then
                            Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, DBNull.Value)
                        Else
                            Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, SearchDataView(i_1)("ModifiedBy"))
                        End If
                        If SearchDataView(i_1)("ModifiedDate") Is DBNull.Value Then
                            Me.AddParameter("@ModifiedDate", SqlDbType.DateTime, DBNull.Value)
                        Else
                            Me.AddParameter("@ModifiedDate", SqlDbType.DateTime, Convert.ToDateTime(SearchDataView(i_1)("ModifiedDate")))
                        End If
                        Me.SqlCom.CommandType = CommandType.Text : Me.SqlCom.CommandText = Query
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        ''check table detail PO
                        Query = " SET NOCOUNT ON;IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'base table'" _
                                 & " AND TABLE_NAME = 'SalesOrdersDetail_" & ListTahun(i).ToString() & "')" _
                        & vbCrLf & " BEGIN " _
                        & vbCrLf & " CREATE TABLE [dbo].[SalesOrdersDetail_" & ListTahun(i).ToString() & "](" _
                        & vbCrLf & "[IDApp] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL," _
                        & vbCrLf & "[CodeApp] [varchar](24) NULL," _
                        & vbCrLf & "[TypeApp] [varchar](32) NULL," _
                        & vbCrLf & "[NameApp] [varchar](64) NULL," _
                        & vbCrLf & "[BrandPackCode] [varchar](16) NULL," _
                        & vbCrLf & "[Quantity] [decimal](16, 3) NULL," _
                        & vbCrLf & "[FKCode] [varchar](50) NULL," _
                        & vbCrLf & "[CreatedBy] [varchar](50) NULL," _
                        & vbCrLf & "[CreatedDate] [smalldatetime] NULL," _
                        & vbCrLf & "[ModifiedBy] [varchar](50) NULL," _
                        & vbCrLf & "[ModifiedDate] [smalldatetime] NULL," _
                        & vbCrLf & ")ON [PRIMARY] " _
                        & vbCrLf & "CREATE  CLUSTERED INDEX [IX_SalesOrdersDetail_" & ListTahun(i).ToString() & "]" _
                        & vbCrLf & " ON [dbo].[SalesOrdersDetail_" & ListTahun(i).ToString() & "]([BrandPackCode], [FKCode]) ON [PRIMARY] " _
                        & vbCrLf & " ALTER TABLE [dbo].[SalesOrdersDetail_" & ListTahun(i).ToString() & "]" _
                        & vbCrLf & " ADD CONSTRAINT [PK_SalesOrdersDetail_" & ListTahun(i).ToString() & "] PRIMARY KEY  NONCLUSTERED " _
                        & vbCrLf & " ( " _
                        & vbCrLf & " [IDApp] " _
                        & vbCrLf & ")  ON [PRIMARY] " _
                        & vbCrLf & " END "
                        Me.SqlCom.CommandType = CommandType.StoredProcedure : Me.SqlCom.CommandText = "sp_executesql"
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                        Query = "SET NOCOUNT ON;" _
                        & vbCrLf & "IF EXISTS(SELECT FkCode FROM  SalesOrdersDetail_" & ListTahun(i).ToString() _
                                 & " WHERE FkCode IN(SELECT CodeApp FROM POHeader_" & ListTahun(i).ToString() _
                                 & " WHERE POCode = @POCode AND TerritoryCode = @TerritoryCode)) " _
                        & vbCrLf & " BEGIN " _
                        & vbCrLf & " DELETE FROM SalesOrdersDetail_" & ListTahun(i).ToString() & " WHERE FkCode IN(SELECT " _
                                 & " CodeApp FROM POHeader_" & ListTahun(i).ToString() & " WHERE POCode = @POCode AND TerritoryCode = @TerritoryCode)" _
                        & vbCrLf & "END"
                        Me.AddParameter("@POCode", SqlDbType.VarChar, SearchDataView(i_1)("POCode"))
                        Me.AddParameter("@TerritoryCode", SqlDbType.VarChar, SearchDataView(i_1)("TerritoryCode"))
                        Me.SqlCom.CommandType = CommandType.Text
                        Me.SqlCom.CommandText = Query : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                        DvChildPO.RowFilter = "FkCode = '" & SearchDataView(i_1)("CodeApp").ToString() & "'"
                        For i_2 As Integer = 0 To DvChildPO.Count - 1
                            Query = "SET NOCOUNT ON;INSERT INTO SalesOrdersDetail_" & ListTahun(i).ToString() & "(" _
                                  & "CodeApp,TypeApp,NameApp,BrandPackCode,Quantity,FkCode,CreatedBy,CreatedDate)" _
                            & vbCrLf & "VALUES(@CodeApp,@TypeApp,@NameApp,@BrandPackCode,@Quantity,@FkCode,@CreatedBy,@CreatedDate)"
                            Me.AddParameter("@CodeApp", SqlDbType.VarChar, DvChildPO(i_2)("CodeApp"))
                            Me.AddParameter("@TypeApp", SqlDbType.VarChar, DvChildPO(i_2)("TypeApp"))
                            Me.AddParameter("@NameApp", SqlDbType.VarChar, DvChildPO(i_2)("NameApp"))
                            Me.AddParameter("@BrandPackCode", SqlDbType.VarChar, DvChildPO(i_2)("BrandPackCode"))
                            Me.AddParameter("@Quantity", SqlDbType.Decimal, Convert.ToDecimal(DvChildPO(i_2)("Quantity")))
                            Me.AddParameter("@FkCode", SqlDbType.VarChar, DvChildPO(i_2)("FkCode"))
                            Me.AddParameter("@CreatedBy", SqlDbType.VarChar, DvChildPO(i_2)("CreatedBy"))
                            Me.AddParameter("@CreatedDate", SqlDbType.DateTime, Convert.ToDateTime(DvChildPO(i_2)("CreatedDate")))
                            Me.SqlCom.CommandText = Query : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Next
                        ''CHECK DATANYA APAKAH SUDAH DI INSERT / BELUM
                    Next

                Next
                Me.CommiteTransaction()
                'Me.RollbackTransaction()
                Me.CloseConnection() : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Function GetListYear() As List(Of Integer)
            Try
                Dim Query As String = "SET NOCOUNT ON; SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'base table'" _
                                     & " AND TABLE_NAME LIKE 'POKios_%' ORDER BY TABLE_NAME DESC"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteReader()
                While Me.SqlRe.Read()
                    ListYear.Add(SqlRe.Item(0))
                End While
                Me.SqlRe.Close()
                'Me.CloseConnection()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return ListYear
        End Function
        Private Function AddQueryYear(ByVal Year As Integer) As String
            Query = "" & vbCrLf & "SELECT SOD.IDApp AS IDSys,SOD.FkCode, PO_" & Year.ToString() & ".TerritoryCode AS TERITORY_ID," _
                                & " TERR.TERRITORY_AREA,PO_" & Year.ToString() & ".DistributorCode AS DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME," _
                                & "PO_" & Year.ToString() & ".KiosCode AS IDKios,K.Kios_Name AS KIOS_NAME," _
                                & "PO_" & Year.ToString() & ".POCode AS PO_REF_NO," _
                                & " PO_" & Year.ToString() & ".SalesDate AS PO_DATE,SOD.BrandPackCode AS BRANDPACK_ID," _
                                & " SOD.NameApp AS BRANDPACK_NAME,SOD.Quantity AS QUANTITY" & vbCrLf _
                                & " FROM POHeader_" & Year.ToString() & " PO_" & Year.ToString() & vbCrLf _
                                & " INNER JOIN Kios K ON K.IDKios = PO_" & Year.ToString() & ".KiosCode  INNER JOIN " _
                                & " DIST_DISTRIBUTOR DR ON PO_" & Year.ToString() & ".DistributorCode = DR.DISTRIBUTOR_ID INNER JOIN" _
                                & " SalesOrdersDetail_" & Year.ToString() & " SOD ON SOD.FkCode = PO_" & Year.ToString() & ".CodeApp" _
                                & " LEFT OUTER JOIN TERRITORY TERR ON PO_" & Year.ToString() & ".TerritoryCode = TERR.TERRITORY_ID"
            Return Query
        End Function
     
        Public Function GetData(ByVal PageIndex As Integer, ByVal PageSize As Integer, _
        ByRef ListYear As List(Of Integer), ByRef StartYear As Integer, ByRef EndYear As Object, ByRef Rowcount As Integer) As DataTable
            Dim tblPO As New DataTable("PURCHASE_ORDER_KIOS") : tblPO.Clear()
            Try
                Dim Query As String = " SET NOCOUNT ON;" & vbCrLf & _
                                      " SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'base table'" _
                                      & " AND TABLE_NAME LIKE 'POHeader_%' ORDER BY TABLE_NAME DESC"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteReader()
                Dim Year As Integer = 0
                While Me.SqlRe.Read()
                    Year = CInt(Right(SqlRe.Item(0), 4))
                    ListYear.Add(Year)
                End While
                Me.SqlRe.Close() : Me.ClearCommandParameters() : ListYear.Sort()
                If (ListYear.Count < 0) Then
                    Return Nothing
                End If
                For i As Integer = 0 To ListYear.Count - 1
                    'ambil default data tahun sebelumnya dan tahun ke belakang
                    If (ListYear(i).Equals(NufarmBussinesRules.SharedClass.ServerDate.Year - 1)) Then
                        StartYear = ListYear(i)
                    ElseIf ListYear(i).Equals(NufarmBussinesRules.SharedClass.ServerDate.Year) Then
                        EndYear = ListYear(i) '= NufarmBussinesRules.SharedClass.ServerDate.Year 
                    End If
                    If i = ListYear.Count - 1 Then
                        If (StartYear > 0) And (EndYear > 0) Then
                            OriginalStartDate = StartYear : OriginalEndDate = EndYear
                        ElseIf StartYear > 0 Then
                            EndYear = StartYear
                        ElseIf EndYear > 0 Then
                            StartYear = EndYear
                        End If
                    End If
                Next
                If (Not StartYear <= 0) Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '" & " ##POKios_" & StartYear.ToString() & "_" & EndYear.ToString() & "' "
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Dim Retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If IsNothing(Retval) Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                "IF OBJECT_ID('Tempdb..##POKios_" & StartYear.ToString & "_" & EndYear.ToString() & "') IS NULL" & vbCrLf _
                              & " BEGIN " & vbCrLf _
                              & " SELECT *, IDENTITY(INT,1,1)AS IDApp INTO ##POKios_" & StartYear.ToString() & "_" & EndYear.ToString() & " FROM("
                        Query &= AddQueryYear(StartYear) & vbCrLf _
                              & " UNION " & vbCrLf
                        Query &= AddQueryYear(EndYear) & vbCrLf _
                              & ")PO; " & vbCrLf _
                              & " CREATE UNIQUE CLUSTERED INDEX IDX_POKIOS ON tempdb..##POKIOS_" & StartYear.ToString() & "_" & EndYear.ToString() & "(IDApp);" & vbCrLf _
                              & " CREATE NONCLUSTERED INDEX IDX_POKIOS_1 ON tempdb..##POKIOS_" & StartYear.ToString() & "_" & EndYear.ToString() & "(PO_DATE);" & vbCrLf _
                              & " SELECT TOP " & PageSize.ToString() & " * FROM TEMPDB..##POKios_" & StartYear.ToString & "_" & EndYear.ToString() _
                              & " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " _
                              & " FROM TEMPDB..##POKios_" & StartYear.ToString & "_" & EndYear.ToString() _
                              & " WHERE YEAR(PO_DATE) >= @StartYear  AND YEAR(PO_DATE) <= @EndYear ORDER BY IDApp ASC)" _
                              & " AND YEAR(PO_DATE) >= @StartYear  AND YEAR(PO_DATE) <= @EndYear ORDER BY IDApp ASC;" _
                              & " END "
                    Else
                        Query = "SET NOCOUNT ON ;" & vbCrLf _
                          & " SELECT TOP " & PageSize.ToString() & " * FROM TEMPDB..##POKios_" & StartYear.ToString & "_" & EndYear.ToString() _
                          & " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " _
                          & " FROM TEMPDB..##POKios_" & StartYear.ToString & "_" & EndYear.ToString() _
                          & " WHERE YEAR(PO_DATE) >= @StartYear  AND YEAR(PO_DATE) <= @EndYear ORDER BY IDApp ASC)" _
                          & " AND YEAR(PO_DATE) >= @StartYear  AND YEAR(PO_DATE) <= @EndYear ORDER BY IDApp ASC OPTION(KEEP PLAN);"

                    End If
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@StartYear", SqlDbType.Int, StartYear)
                    Me.AddParameter("@EndYear", SqlDbType.Int, EndYear)
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(tblPO) : Me.ClearCommandParameters()
                    'Me.AddParameter("@O_ROWCOUNT", SqlDbType.Int, ParameterDirection.Output)

                    'Me.SqlCom.CommandText = "SET NOCOUNT ON;SELECT COUNT(IDApp) FROM TEMPDB..##POKios_" & StartYear.ToString & "_" & EndYear.ToString() _
                    '      & " WHERE YEAR(PO_DATE) >= @StartYear  AND YEAR(PO_DATE) <= @EndYear  OPTION(KEEP PLAN);"

                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT SUM (row_count) FROM tempdb.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('" & "TEMPDB..##POKios_" & StartYear.ToString & "_" & EndYear.ToString() & "')  AND (index_id=0 or index_id=1) ;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                    'Me.CloseConnection()
                    If (tblPO.Rows.Count > 0) Then
                    Else
                        Rowcount = 0 : Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return tblPO
        End Function

        Public Function PopulateQuery(ByVal searchString As String, ByVal SearchBy As String, ByVal PageIndex As Integer, _
        ByVal PageSize As String, ByVal StartYear As Integer, ByVal Endyear As Integer, ByRef Rowcount As Integer) As DataView
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                         "SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '" & "##POKios_" & StartYear.ToString() & "_" & Endyear.ToString() & "' "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.OpenConnection()
                Dim Retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                If IsNothing(Retval) Then
                    Dim ListYear As New List(Of Integer)()
                    ListYear.Add(StartYear)
                    Dim OtherYear As Integer = StartYear
                    While OtherYear <> Endyear
                        OtherYear += 1
                        If OtherYear < Endyear Then
                            ListYear.Add(OtherYear)
                        End If
                    End While
                    ListYear.Add(Endyear)
                    Query = " SET NOCOUNT ON;IF OBJECT_ID('Tempdb..##POKios_" & StartYear.ToString & "_" & Endyear.ToString() & "') IS NULL " & vbCrLf _
                                & "BEGIN " & vbCrLf _
                                & " SELECT *, IDENTITY(INT,1,1)AS IDApp INTO ##POKios_" & StartYear.ToString() & "_" & Endyear.ToString() & " FROM("
                    For i As Integer = 0 To ListYear.Count - 1
                        Query &= AddQueryYear(ListYear(i)) & vbCrLf
                        If i <> ListYear.Count - 1 Then
                            Query &= " UNION " & vbCrLf
                        End If
                    Next
                    Query &= ")PO; " & vbCrLf _
                           & " CREATE UNIQUE CLUSTERED INDEX IDX_POKIOS ON tempdb..##POKIOS_" & StartYear.ToString() & "_" & Endyear.ToString() & "(IDApp);" & vbCrLf _
                           & " CREATE NONCLUSTERED INDEX IDX_POKIOS_1 ON tempdb..##POKIOS_" & StartYear.ToString() & "_" & Endyear.ToString() & "(PO_DATE);" & vbCrLf _
                           & " SELECT TOP " & PageSize.ToString() & " * FROM TEMPDB..##POKios_" & StartYear.ToString & "_" & Endyear.ToString() _
                           & " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " _
                           & " FROM TEMPDB..##POKios_" & StartYear.ToString & "_" & Endyear.ToString() _
                           & " WHERE (" & SearchBy & " LIKE '%' + @SearchString + '%') ORDER BY IDApp ASC)" _
                           & " AND " & SearchBy & " LIKE '%' + @SearchString + '%' ORDER BY IDApp ASC;" & vbCrLf _
                           & "END "
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT TOP " & PageSize.ToString() & " * FROM TEMPDB.." & "##POKios_" & StartYear.ToString & "_" & Endyear.ToString() & vbCrLf & _
                            " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " & vbCrLf & _
                            " FROM TEMPDB..##POKios_" & StartYear.ToString & "_" & Endyear.ToString() & vbCrLf & _
                            " WHERE (" & SearchBy & " LIKE '%' + @SearchString + '%') ORDER BY IDApp ASC)" & vbCrLf & _
                            " AND " & SearchBy & " LIKE '%' + @SearchString + '%' ORDER BY IDApp ASC OPTION(KEEP PLAN);"
                End If
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@SearchString", SqlDbType.VarChar, searchString)
                'Me.AddParameter("@O_ROWCOUNT", SqlDbType.Int, ParameterDirection.Output)
                Dim TblPO As New DataTable("PURCHASE_ORDER_KIOS") : TblPO.Clear()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(TblPO) : Me.ClearCommandParameters()
                If searchString = "" Then

                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT SUM (row_count) FROM tempdb.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('" & "TEMPDB..##POKios_" & StartYear.ToString & "_" & Endyear.ToString() & "') AND (index_id=0 or index_id=1) ;"
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Else
                    Query = " SELECT COUNT(IDApp) FROM TEMPDB..##POKios_" & StartYear.ToString() _
                      & "_" & Endyear.ToString() & " WHERE " & SearchBy & " LIKE '%' + @SearchString + '%'  OPTION(KEEP PLAN);"
                    Me.ResetCommandText(CommandType.Text, Query) : Me.AddParameter("@SearchString", SqlDbType.VarChar, searchString)

                End If
                Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                'Me.CloseConnection()
                If (TblPO.Rows.Count > 0) Then
                    'Rowcount = CInt(Me.SqlDat.GetFillParameters().GetValue(1))
                    'Rowcount = CInt(Me.SqlCom.Parameters("@O_ROWCOUNT").Value)
                Else
                    Rowcount = 0
                    Return Nothing
                End If
                Return TblPO.DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function PopulateQuery(ByVal SearchString As String, ByVal SearchBy As String, ByVal PageIndex As Integer, _
        ByVal PageSize As Integer, ByVal StartYear As Integer, ByVal EndYear As Integer, ByVal StartDatePO As String, _
       ByVal EndDatePO As String, ByRef Rowcount As Integer) As DataView
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                         "SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '" & "##POKios_" & StartYear.ToString() & "_" & EndYear.ToString() & "' "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.OpenConnection()
                Dim Retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                If IsNothing(Retval) Then
                    Dim ListYear As New List(Of Integer)()
                    ListYear.Add(StartYear)
                    Dim OtherYear As Integer = StartYear
                    While OtherYear <> EndYear
                        OtherYear += 1
                        If OtherYear < EndYear Then
                            ListYear.Add(OtherYear)
                        End If
                    End While
                    ListYear.Add(EndYear)
                    Query = " SET NOCOUNT ON;" & vbCrLf & _
                            " IF OBJECT_ID('Tempdb..##POKios_" & StartYear.ToString & "_" & EndYear.ToString() & "') IS NULL " & vbCrLf _
                             & "BEGIN " & vbCrLf _
                               & " SELECT *, IDENTITY(INT,1,1)AS IDApp INTO ##POKios_" & StartYear.ToString() & "_" & EndYear.ToString() & " FROM("
                    For i As Integer = 0 To ListYear.Count - 1
                        Query &= AddQueryYear(ListYear(i)) & vbCrLf
                        If i <> ListYear.Count - 1 Then
                            Query &= " UNION " & vbCrLf
                        End If
                    Next
                    Query &= ")PO; " & vbCrLf _
                          & " CREATE UNIQUE CLUSTERED INDEX IDX_POKIOS ON tempdb..##POKIOS_" & StartYear.ToString() & "_" & EndYear.ToString() & "(IDApp);" & vbCrLf _
                          & " CREATE NONCLUSTERED INDEX IDX_POKIOS_1 ON tempdb..##POKIOS_" & StartYear.ToString() & "_" & EndYear.ToString() & "(PO_DATE);" & vbCrLf _
                          & " SELECT TOP " & PageSize.ToString() & " * FROM TEMPDB." & ".##POKios_" & StartYear.ToString & "_" & EndYear.ToString() _
                          & " WHERE IDApp > ALL(SELECT TOP " & (PageSize * (PageIndex - 1)).ToString() & " IDApp " _
                          & " FROM TEMPDB..##POKios_" & StartYear.ToString & "_" & EndYear.ToString() _
                          & " WHERE (PO_DATE >= @StartDatePO AND PO_Date <= @EndDatePO AND " & SearchBy _
                          & " LIKE '%' + @SearchString + '%') ORDER BY IDApp ASC)" _
                          & " AND PO_DATE >= @StartDatePO AND PO_DATE <= @EndDatePO " _
                          & " AND " & SearchBy & " LIKE '%' + @SearchString + '%' ORDER BY IDApp ASC;" & vbCrLf _
                          & " END "
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT TOP " & PageSize.ToString() & " * FROM TEMPDB." & ".##POKios_" & StartYear.ToString & "_" & EndYear.ToString() _
                            & " WHERE IDApp > ALL(SELECT TOP " + (PageSize * (PageIndex - 1)).ToString() & " IDApp " _
                            & " FROM TEMPDB..##POKios_" & StartYear.ToString & "_" & EndYear.ToString() _
                            & " WHERE (PO_DATE >= @StartDatePO AND PO_DATE <= @EndDatePO AND " & SearchBy _
                            & " LIKE '%' + @SearchString + '%') ORDER BY IDApp ASC)" _
                            & " AND PO_DATE >= @StartDatePO AND PO_DATE <= @EndDatePO AND " & SearchBy _
                            & " LIKE '%' + @SearchString + '%' ORDER BY IDApp ASC OPTION(KEEP PLAN);"
                End If
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@StartDatePO", SqlDbType.VarChar, StartDatePO)
                Me.AddParameter("@EndDatePO", SqlDbType.VarChar, EndDatePO)
                Me.AddParameter("@SearchString", SqlDbType.VarChar, SearchString)
                'Me.AddParameter("@O_ROWCOUNT", SqlDbType.Int, ParameterDirection.Output)
                Dim TblPO As New DataTable("PURCHASE_ORDER_KIOS") : TblPO.Clear()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(TblPO)
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT COUNT(IDApp) FROM TEMPDB..##POKios_" & StartYear.ToString() _
                       & "_" & EndYear.ToString() & " WHERE (PO_DATE >= @StartDatePO AND PO_DATE <= @EndDatePO AND " & SearchBy _
                       & " LIKE '%' + @SearchString + '%')  OPTION(KEEP PLAN);"

                Me.ResetCommandText(CommandType.Text, Query)
                Rowcount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                'Me.CloseConnection()
                If (TblPO.Rows.Count > 0) Then
                    'Rowcount = CInt(Me.SqlDat.GetFillParameters().GetValue(3))
                    'Rowcount = CInt(Me.SqlCom.Parameters("@O_ROWCOUNT").Value)
                Else
                    Rowcount = 0
                    Return Nothing
                End If
                Return TblPO.DefaultView
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Sub New()
            MyBase.New()
        End Sub
        Public Overloads Sub dispose(ByVal disposing As Boolean)
            If Not IsNothing(DVSalesDate) Then
                DVSalesDate.Dispose() : DVSalesDate = Nothing
            End If
            MyBase.Dispose(disposing)
        End Sub
        Public Sub DisposeTempDB()
            Try
                If Me.ListYear.Count > 0 Then
                    Me.CreateCommandSql(CommandType.StoredProcedure, "sp_executesql", ConnectionTo.Nufarm)
                    Me.OpenConnection()
                    For i As Integer = 0 To ListYear.Count - 1
                        Dim ListTable As New List(Of Integer)
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] LIKE '##POKIOS_" & ListYear(i) & "%';"
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Me.SqlRe = Me.SqlCom.ExecuteReader()
                        While Me.SqlRe.Read()
                            ListTable.Add(SqlRe.GetString(0))
                        End While : SqlRe.Close() : Me.ClearCommandParameters()
                        If ListTable.Count > 0 Then
                            For i1 As Integer = 0 To ListTable.Count - 1
                                Query = "SET NOCOUNT ON;" & vbCrLf & _
                                        " DROP TABLE tempdb.." & ListTable(i1) & ";"
                                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Next
                        End If
                    Next
                    Me.CloseConnection() : Me.ClearCommandParameters()
                End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Sub DeletePOKios(ByVal FkCode As String, ByVal PO_Code As String, ByVal StartYear As Integer, ByVal EndYear As Integer)
            Try
                Dim Query As String = "SET NOCOUNT ON;IF EXISTS(SELECT FkCode FROM SalesOrdersDetail_" & StartYear.ToString() & " WHERE FkCode = @FkCode)" & vbCrLf & _
                                      " BEGIN " & vbCrLf & _
                                      "     DELETE FROM SalesOrdersDetail_" & StartYear.ToString() & " WHERE FkCode = @FkCode;" & vbCrLf & _
                                      "     DELETE FROM TEMPDB..##POKIOS_" & StartYear.ToString() & "_" & EndYear.ToString() & " WHERE FkCode = @FkCode;" & vbCrLf & _
                                      " END " & vbCrLf & _
                                      " IF NOT EXISTS(SELECT FkCode FROM SalesOrdersDetail_" & StartYear.ToString() & " WHERE FKCode IN(SELECT CodeApp " & vbCrLf & _
                                      " FROM POHeader_" & StartYear.ToString() & " WHERE POCode = @POCode))" & vbCrLf & _
                                      " BEGIN " & vbCrLf & _
                                      "     DELETE FROM POHeader_" & StartYear.ToString() & " WHERE POCode = @POCode;" & vbCrLf & _
                                      " END " & vbCrLf & _
                                      " IF EXISTS(SELECT FkCode FROM SalesOrdersDetail_" & EndYear.ToString() & " WHERE FkCode = @FkCode)" & vbCrLf & _
                                      " BEGIN " & vbCrLf & _
                                      "     DELETE FROM SalesOrdersDetail_" & EndYear.ToString() & " WHERE FkCode = @FkCode;" & vbCrLf & _
                                      " END" & vbCrLf & _
                                      " IF NOT EXISTS(SELECT FkCode FROM SalesOrdersDetail_" & EndYear.ToString() & " WHERE FKCode IN(SELECT CodeApp " & vbCrLf & _
                                      " FROM POHeader_" & EndYear.ToString() & " WHERE POCode = @POCode))" & vbCrLf & _
                                      " BEGIN " & vbCrLf & _
                                      "     DELETE FROM POHeader_" & EndYear.ToString() & " WHERE POCode = @POCode;" & vbCrLf & _
                                      " END "
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@POCode", SqlDbType.VarChar, PO_Code)
                Me.AddParameter("@FkCode", SqlDbType.VarChar, FkCode)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar() : Me.CommiteTransaction() : Me.ClearCommandParameters() ': Me.CloseConnection()

            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
    End Class
End Namespace
