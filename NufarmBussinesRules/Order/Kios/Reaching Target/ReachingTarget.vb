Imports System.Data.SqlClient
Imports System.Data
Namespace Kios
    Public Class ReachingTarget : Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Private Query As String = ""
        Public Sub InsertReaching(ByVal Ds As DataSet)
            Try
                If (Not IsNothing(Ds)) Then
                    Dim DvDetail As DataView = Ds.Tables(1).DefaultView()
                    Me.GetConnection() : Me.OpenConnection() : Me.BeginTransaction()
                    Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; " & vbCrLf & _
                            "IF EXISTS(SELECT SpCode FROM BrandReaching WHERE Spcode = @SpCode) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "   DELETE FROM DetailReaching WHERE FkCode = ANY(SELECT CodeApp FROM BrandReaching " & vbCrLf & _
                            "   WHERE SpCode = @SpCode) ; " & vbCrLf & _
                            "   DELETE FROM BrandReaching WHERE SpCode = @SpCode; " & vbCrLf & _
                            " END "
                    Me.CreateCommandSql("", Query) : Me.AddParameter("@SpCode", SqlDbType.VarChar, Ds.Tables(0).Rows(0)("SpCode"), 15)
                    Me.SqlCom.Connection = Me.SqlConn : Me.SqlCom.Transaction = Me.SqlTrans
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                        Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; " & vbCrLf _
                                & "INSERT INTO BrandReaching(CodeApp,TerritoryCode,SpCode,KiosCode," _
                                & "BrandCode,Target,TotalActual,Dispro,DisproPercentage,BonusQty,IsBundled,CreatedBy,CreatedDate)" & vbCrLf _
                                & "VALUES(@CodeApp,@TerritoryCode,@SpCode,@KiosCode,@BrandCode,@Target," _
                                & "@TotalActual,@Dispro,@DisproPercentage,@BonusQty,@IsBundled,@CreatedBy,@CreatedDate) ;"
                        Me.AddParameter("@CodeApp", SqlDbType.VarChar, Ds.Tables(0).Rows(i)("CodeApp"))
                        Me.AddParameter("@TerritoryCode", SqlDbType.VarChar, Ds.Tables(0).Rows(i)("TerritoryCode"))
                        Me.AddParameter("@SpCode", SqlDbType.VarChar, Ds.Tables(0).Rows(i)("SpCode"))
                        Me.AddParameter("@KiosCode", SqlDbType.VarChar, Ds.Tables(0).Rows(i)("KiosCode"))
                        Me.AddParameter("@BrandCode", SqlDbType.VarChar, Ds.Tables(0).Rows(i)("BrandCode"))
                        Me.AddParameter("@Target", SqlDbType.Decimal, Convert.ToDecimal(Ds.Tables(0).Rows(i)("Target")))
                        Me.AddParameter("@TotalActual", SqlDbType.Decimal, Ds.Tables(0).Rows(i)("TotalActual"))
                        Me.AddParameter("@Dispro", SqlDbType.Decimal, Convert.ToDecimal(Ds.Tables(0).Rows(i)("Dispro")))
                        Me.AddParameter("@DisproPercentage", SqlDbType.Decimal, Convert.ToDecimal(Ds.Tables(0).Rows(i)("DisproPercentage")))
                        Me.AddParameter("@BonusQty", SqlDbType.Decimal, Convert.ToDecimal(Ds.Tables(0).Rows(i)("BonusQty")))
                        Me.AddParameter("@IsBundled", SqlDbType.Bit, Ds.Tables(0).Rows(i)("IsBundled"))
                        Me.AddParameter("@CreatedBy", SqlDbType.VarChar, Ds.Tables(0).Rows(i)("CreatedBy"))
                        Me.AddParameter("@CreatedDate", SqlDbType.DateTime, Convert.ToDateTime(Ds.Tables(0).Rows(i)("CreatedDate")))
                        Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        DvDetail.RowFilter = "FkCode = '" & Ds.Tables(0).Rows(i)("CodeApp").ToString() & "'"
                        If (DvDetail.Count > 0) Then
                            Me.SqlCom.CommandText = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; " & vbCrLf _
                                                  & " INSERT INTO DetailReaching(CodeApp,BrandPackCode," _
                                                  & "TotalQty,DiscQty,FkCode,CreatedBy,CreatedDate)" & vbCrLf _
                                                  & " VALUES(@CodeApp,@BrandPackCode,@TotalQty,@DiscQty,@FkCode" _
                                                  & ",@CreatedBy,@CreatedDate) ;"
                            For i_1 As Integer = 0 To DvDetail.Count - 1
                                Me.AddParameter("CodeApp", SqlDbType.VarChar, DvDetail(i_1)("CodeApp"))
                                Me.AddParameter("@BrandPackCode", SqlDbType.VarChar, DvDetail(i_1)("BrandPackCode"))
                                Me.AddParameter("@TotalQty", SqlDbType.Decimal, Convert.ToDecimal(DvDetail(i_1)("TotalQty")))
                                Me.AddParameter("@DiscQty", SqlDbType.Decimal, Convert.ToDecimal(DvDetail(i_1)("DiscQty")))
                                Me.AddParameter("@FkCode", SqlDbType.VarChar, DvDetail(i_1)("FkCode"))
                                Me.AddParameter("@CreatedBy", SqlDbType.VarChar, DvDetail(i_1)("CreatedBy"))
                                Me.AddParameter("@CreatedDate", SqlDbType.DateTime, Convert.ToDateTime(DvDetail(i_1)("CreatedDate")))
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Next
                        End If
                    Next
                    Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT BrandCode,SUM(Target)AS TotalCoverage,ISNULL(SUM(TotalActual),0)AS TotalActual,ISNULL(SUM(BonusQty),0) AS TotalDisc " & vbCrLf & _
                            " FROM BrandReaching WHERE SpCode = @SpCode GROUP BY BrandCode "
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@SpCode", SqlDbType.VarChar, Ds.Tables(0).Rows(0)("SpCode"), 16)


                    Dim dtTable As New DataTable() : dtTable.Clear() : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                    Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                    dtTable.Columns("BrandCode").Unique = True
                    Dim Key(1) As DataColumn : Key(0) = dtTable.Columns("BrandCode")
                    dtTable.PrimaryKey = Key
                    Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; " & vbCrLf & _
                           "IF EXISTS(SELECT BrandCode FROM RecapNDPRD WHERE SpCode = @SpCode AND BrandCode = @BrandCode) " & vbCrLf & _
                           " BEGIN " & vbCrLf & _
                           "  UPDATE RecapNDPRD SET TotalCoverage = @TotalCoverage,TotalActual = @TotalActual,TotalDisc = @TotalDisc, " & vbCrLf & _
                           "  ModifiedBy = @ModifiedBy,ModifiedDate = @ModifiedDate WHERE SpCode = @SpCode AND BrandCode = @BrandCode ; " & vbCrLf & _
                           "  END " & vbCrLf & _
                           " ELSE " & vbCrLf & _
                           " BEGIN " & vbCrLf & _
                           " INSERT INTO RecapNDPRD(SPCode,BrandCode,TerritoryCode,BudgetTerritory,BudgetDispro,TotalCoverage," & vbCrLf & _
                           " TotalActual,TotalDisc,StartDate,EndDate,CreatedBy,CreatedDate) " & vbCrLf & _
                           " VALUES(@SpCode,@BrandCode,@TerritoryCode,@BudgetTerritory,@BudgetDispro,@TotalCoverage,@TotalActual, " & vbCrLf & _
                           " @TotalDisc,@StartDate,@EndDate,@CreatedBy,@CreatedDate) ;" & vbCrLf & _
                           " END "
                    Me.ResetCommandText(CommandType.Text, Query)
                    Dim TerritoryCode As String = Ds.Tables(3).Rows(0)("TerritoryCode").ToString()
                    Dim StartDate As DateTime = Convert.ToDateTime(Ds.Tables(3).Rows(0)("StartDate"))
                    Dim EndDate As DateTime = Convert.ToDateTime(Ds.Tables(3).Rows(0)("EndDate"))
                    Dim SpCode As String = Ds.Tables(0).Rows(0)("SpCode").ToString()
                    Dim CreatedBy As Object = Ds.Tables(0).Rows(0)("CreatedBy")
                    Dim CreatedDate As Object = Ds.Tables(0).Rows(0)("CreatedDate")
                    Dim ModifiedBy As Object = Ds.Tables(0).Rows(0)("ModifiedBy")
                    Dim ModifiedDate As Object = Ds.Tables(0).Rows(0)("ModifiedDate")
                    For i As Integer = 0 To Ds.Tables(3).Rows.Count - 1
                        Dim BudgetDispro As Decimal = 0, BudgetTerritory As Decimal = 0
                        Dim foundRow As DataRow = dtTable.Rows.Find(Ds.Tables(3).Rows(i)("BrandCode").ToString())
                        If Not IsNothing(foundRow) Then
                            BudgetDispro = Convert.ToDecimal(Ds.Tables(3).Rows(i)("BudgetDispro"))
                            BudgetTerritory = Convert.ToDecimal(Ds.Tables(3).Rows(i)("BudgetTerritory"))
                            Me.AddParameter("SpCode", SqlDbType.VarChar, SpCode, 16)
                            Me.AddParameter("@BrandCode", SqlDbType.VarChar, foundRow("BrandCode"), 16)
                            Me.AddParameter("@TerritoryCode", SqlDbType.VarChar, TerritoryCode, 16)
                            Me.AddParameter("@BudgetTerritory", SqlDbType.Decimal, BudgetTerritory)
                            Me.AddParameter("@BudgetDispro", SqlDbType.Decimal, BudgetDispro)
                            Me.AddParameter("@TotalCoverage", SqlDbType.Decimal, foundRow("TotalCoverage"))
                            Me.AddParameter("@TotalActual", SqlDbType.Decimal, foundRow("TotalActual"))
                            Me.AddParameter("@TotalDisc", SqlDbType.Decimal, foundRow("TotalDisc"))
                            Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                            Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                            Me.AddParameter("@CreatedBy", SqlDbType.VarChar, CreatedBy, 50)
                            Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, CreatedDate)
                            Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, ModifiedBy, 50)
                            Me.AddParameter("@ModifiedDate", SqlDbType.SmallDateTime, ModifiedDate)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        End If
                    Next
                    Me.CommiteTransaction() : Me.CloseConnection()
                End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Throw ex
            End Try
        End Sub

        Public Sub InsertAchievementDPRDM(ByVal DS As DataSet)
            Try
                If Not IsNothing(DS) Then
                    Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON ; " & vbCrLf & _
                            "IF EXISTS(SELECT SpCode FROM BrandAccomplishment WHERE SpCode = @SpCode) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " DELETE FROM BrandAccomplishment WHERE SpCode = @SpCode; " & vbCrLf & _
                            " END "
                    Me.GetConnection() : Me.OpenConnection() : Me.BeginTransaction()
                    Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                    Me.AddParameter("@SpCode", SqlDbType.VarChar, DS.Tables(0).Rows(0)("SpCode"), 16)
                    Me.SqlCom.Transaction = Me.SqlTrans : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON ;" & vbCrLf & _
                            "INSERT INTO BrandAccomplishment(CodeApp,TerritoryCode,SpCode,KiosCode,BrandCode,Target,TotalActual, " & vbCrLf & _
                            " DisproValue,BonusPercentage,BonusValue,CreatedBy,CreatedDate) " & vbCrLf & _
                            " VALUES(@CodeApp,@TerritoryCode,@SpCode,@KiosCode,@BrandCode,@Target,@TotalActual, " & vbCrLf & _
                            " @DisproValue,@BonusPercentage,@BonusValue,@CreatedBy,@CreatedDate);"
                    Me.ResetCommandText(CommandType.Text, Query)
                    For I As Integer = 0 To DS.Tables(0).Rows.Count - 1
                        Me.AddParameter("@CodeApp", SqlDbType.VarChar, DS.Tables(0).Rows(I)("CodeApp"), 124)
                        Me.AddParameter("@TerritoryCode", SqlDbType.VarChar, DS.Tables(0).Rows(I)("TerritoryCode"), 16)
                        Me.AddParameter("@SpCode", SqlDbType.VarChar, DS.Tables(0).Rows(I)("SpCode"), 16)
                        Me.AddParameter("@KiosCode", SqlDbType.VarChar, DS.Tables(0).Rows(I)("KiosCode"), 70)
                        Me.AddParameter("@BrandCode", SqlDbType.VarChar, DS.Tables(0).Rows(I)("BrandCode"), 16)
                        Me.AddParameter("@Target", SqlDbType.Decimal, DS.Tables(0).Rows(I)("Target"))
                        Me.AddParameter("@TotalActual", SqlDbType.Decimal, DS.Tables(0).Rows(I)("TotalActual"))
                        Me.AddParameter("@DisproValue", SqlDbType.Decimal, DS.Tables(0).Rows(I)("DisproValue"))
                        Me.AddParameter("@BonusPercentage", SqlDbType.Decimal, DS.Tables(0).Rows(I)("BonusPercentage"))
                        Me.AddParameter("@BonusValue", SqlDbType.Decimal, DS.Tables(0).Rows(I)("BonusValue"))
                        Me.AddParameter("@CreatedBy", SqlDbType.VarChar, DS.Tables(0).Rows(I)("CreatedBy"), 50)
                        Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, DS.Tables(0).Rows(I)("CreatedDate"))
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next
                    Dim dtNRecap As New DataTable("T_RecapDPRDM") : dtNRecap.Clear()

                    Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; " & vbCrLf & _
                             "SELECT BrandCode,SUM(Target)AS TotalCoverage,ISNULL(SUM(TotalActual),0)AS TotalActual,ISNULL(SUM(BonusValue),0) AS TotalBonus " & vbCrLf & _
                             " FROM BrandAccomplishment WHERE SpCode = @SpCode GROUP BY BrandCode ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@SpCode", SqlDbType.VarChar, DS.Tables(0).Rows(0)("SpCode"), 16)
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtNRecap) : Me.ClearCommandParameters()
                    dtNRecap.Columns("BrandCode").Unique = True
                    Dim Key(1) As DataColumn : Key(0) = dtNRecap.Columns("BrandCode")
                    dtNRecap.PrimaryKey = Key
                    Query = "SET DEADLOCK_PRIORITY LOW; SET NOCOUNT ON; " & vbCrLf & _
                            "IF EXISTS(SELECT BrandCode FROM RecapDPRDM WHERE SpCode = @SpCode AND BrandCode = @BrandCode) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "  UPDATE RecapDPRDM SET TotalCoverage = @TotalCoverage,TotalActual = @TotalActual,TotalBonus = @TotalBonus, " & vbCrLf & _
                            "  ModifiedBy = @ModifiedBy,ModifiedDate = @ModifiedDate WHERE SpCode = @SpCode AND BrandCode = @BrandCode ; " & vbCrLf & _
                            "  END " & vbCrLf & _
                            " ELSE " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " INSERT INTO RecapDPRDM(SPCode,BrandCode,TerritoryCode,BudgetTerritory,BudgetDispro,TotalCoverage," & vbCrLf & _
                            " TotalActual,TotalBonus,StartDate,EndDate,CreatedBy,CreatedDate) " & vbCrLf & _
                            " VALUES(@SpCode,@BrandCode,@TerritoryCode,@BudgetTerritory,@BudgetDispro,@TotalCoverage,@TotalActual, " & vbCrLf & _
                            " @TotalBonus,@StartDate,@EndDate,@CreatedBy,@CreatedDate) ;" & vbCrLf & _
                            " END "
                    Me.ResetCommandText(CommandType.Text, Query)
                    Dim TerritoryCode As String = DS.Tables(1).Rows(0)("TerritoryCode").ToString()
                    Dim StartDate As DateTime = Convert.ToDateTime(DS.Tables(1).Rows(0)("StartDate"))
                    Dim EndDate As DateTime = Convert.ToDateTime(DS.Tables(1).Rows(0)("EndDate"))
                    Dim SpCode As String = DS.Tables(0).Rows(0)("SpCode").ToString()
                    Dim CreatedBy As Object = DS.Tables(0).Rows(0)("CreatedBy")
                    Dim CreatedDate As Object = DS.Tables(0).Rows(0)("CreatedDate")
                    Dim ModifiedBy As Object = DS.Tables(0).Rows(0)("ModifiedBy")
                    Dim ModifiedDate As Object = DS.Tables(0).Rows(0)("ModifiedDate")
                    For i As Integer = 0 To DS.Tables(1).Rows.Count - 1
                        Dim BudgetDispro As Decimal = 0, BudgetTerritory As Decimal = 0
                        Dim foundRow As DataRow = dtNRecap.Rows.Find(DS.Tables(1).Rows(i)("BrandCode").ToString())
                        If Not IsNothing(foundRow) Then
                            BudgetDispro = Convert.ToDecimal(DS.Tables(1).Rows(i)("BudgetDispro"))
                            BudgetTerritory = Convert.ToDecimal(DS.Tables(1).Rows(i)("BudgetTerritory"))
                            Me.AddParameter("@SpCode", SqlDbType.VarChar, SpCode, 16)
                            Me.AddParameter("@BrandCode", SqlDbType.VarChar, foundRow("BrandCode"), 16)
                            Me.AddParameter("@TerritoryCode", SqlDbType.VarChar, TerritoryCode, 16)
                            Me.AddParameter("@BudgetTerritory", SqlDbType.Decimal, BudgetTerritory)
                            Me.AddParameter("@BudgetDispro", SqlDbType.Decimal, BudgetDispro)
                            Me.AddParameter("@TotalCoverage", SqlDbType.Decimal, foundRow("TotalCoverage"))
                            Me.AddParameter("@TotalActual", SqlDbType.Decimal, foundRow("TotalActual"))
                            Me.AddParameter("@TotalBonus", SqlDbType.Decimal, foundRow("TotalBonus"))
                            Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                            Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                            Me.AddParameter("@CreatedBy", SqlDbType.VarChar, CreatedBy, 50)
                            Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, CreatedDate)
                            Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, ModifiedBy, 50)
                            Me.AddParameter("@ModifiedDate", SqlDbType.SmallDateTime, ModifiedDate)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        End If
                    Next
                    Me.CommiteTransaction() : Me.CloseConnection()
                End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function GetDistributorIncluded(ByVal SpCode As String, ByRef StartDate As DateTime, ByRef EndDate As DateTime) As DataView
            Try
                Dim Query As String = "SET NOCOUNT ON;SELECT DISTINCT MBD.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM MRKT_BRANDPACK_DISTRIBUTOR MBD" & _
                " INNER JOIN DIST_DISTRIBUTOR DR ON MBD.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID WHERE MBD.DISTRIBUTOR_ID = ANY(" & _
                " SELECT DISTINCT DISTRIBUTOR_ID FROM MARKETING_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_ID = ANY(SELECT PROG_BRANDPACK_ID" & _
                " FROM MRKT_BRANDPACK WHERE PROGRAM_ID = @SpCode)) OPTION(KEEP PLAN);"
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@SpCode", SqlDbType.VarChar, SpCode)
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Dim DtDistributor As New DataTable("T_Distributor") : DtDistributor.Clear()
                Me.SqlDat.Fill(DtDistributor)
                Me.SqlCom.CommandText = "SELECT START_DATE, END_DATE FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID = @SpCode OPTION(KEEP PLAN);"
                Me.SqlCom.CommandType = CommandType.Text : Me.ExecuteReader()
                While Me.SqlRe.Read()
                    StartDate = Me.SqlRe.GetDateTime(0) : EndDate = Me.SqlRe.GetDateTime(1)
                End While
                Me.SqlRe.Close() : Me.CloseConnection() : Me.ClearCommandParameters()
                Dim DtView As DataView = DtDistributor.DefaultView
                Return DtView
            Catch ex As Exception
                Me.CloseConnection() : Throw ex
            End Try
        End Function
     
        Public Function GetSpCode(ByVal SearchSpCode As String) As List(Of String)
            Dim ListSpCode As New List(Of String)
            Try
                Dim Query As String = ""
                If (String.IsNullOrEmpty(SearchSpCode)) Then
                    Query = "SET NOCOUNT ON;SELECT TOP 50 PROGRAM_ID FROM MRKT_MARKETING_PROGRAM MMP WHERE EXISTS(" & _
                    "SELECT PROGRAM_ID FROM MRKT_BRANDPACK MB WHERE MMP.PROGRAM_ID = MB.PROGRAM_ID AND EXISTS(" & _
                    "SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK_DISTRIBUTOR MBD WHERE MBD.PROG_BRANDPACK_ID = MB.PROG_BRANDPACK_ID" & _
                    " AND MBD.ISRPK = 1))ORDER BY MMP.START_DATE DESC OPTION(KEEP PLAN); "
                Else
                    Query = "SET NOCOUNT ON;SELECT TOP 50 PROGRAM_ID FROM MRKT_MARKETING_PROGRAM MMP WHERE EXISTS(" & _
                           "SELECT PROGRAM_ID FROM MRKT_BRANDPACK MB WHERE MMP.PROGRAM_ID = MB.PROGRAM_ID AND EXISTS(" & _
                           "SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK_DISTRIBUTOR MBD WHERE MBD.PROG_BRANDPACK_ID = MB.PROG_BRANDPACK_ID" & _
                           " AND MBD.ISRPK = 1)) AND MMP.PROGRAM_ID LIKE '%" & SearchSpCode & "%'" & " ORDER BY MMP.START_DATE DESC OPTION(KEEP PLAN); "
                    'Query = "SELECT TOP 50 PROGRAM_ID FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID LIKE '%" & SearchSpCode & "%'"
                End If
                Me.CreateCommandSql("sp_executesql", "") : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteReader() : While Me.SqlRe.Read() : ListSpCode.Add(SqlRe(0).ToString()) : End While
                Me.SqlRe.Close() : Return ListSpCode
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Me.SqlRe.IsClosed = False Then : Me.SqlRe.Close() : End If
                End If
                Me.CloseConnection() : Throw ex
            End Try
        End Function
        Public Function getSPAndCreaterSP(ByVal TypeApp As String, ByVal SearchSPCode As String) As DataView
            Try
                If String.IsNullOrEmpty(SearchSPCode.ToString()) Then
                    If TypeApp = "DPRDS" Then
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                               "SELECT DISTINCT TOP 100 SpCode AS PROGRAM_ID,CreatedBy AS CREATE_BY,CreatedDate FROM RecapNDPRD ORDER BY CreatedDate DESC;"
                    Else
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT DISTINCT TOP 100 SpCode AS PROGRAM_ID,CreatedBy AS CREATE_BY,CreatedDate FROM RecapDPRDM ORDER BY CreatedDate DESC;"
                    End If
                Else
                    If TypeApp = "DPRDS" Then
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT DISTINCT TOP 100 SpCode AS PROGRAM_ID,CreatedBy AS CREATE_BY,CreatedDate FROM BrandReaching  WHERE SpCode LIKE '%" & SearchSPCode & "%' ORDER BY CreatedDate DESC;"
                    Else
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT DISTINCT TOP 100 SpCode AS PROGRAM_ID,CreatedBy AS CREATE_BY,CratedDate FROM BrandAccomplishment  WHERE SpCode LIKE '%" & SearchSPCode & "%' ORDER BY CreatedDate DESC;"
                    End If
                End If
                If Not IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Else
                    Me.CreateCommandSql("sp_executesql", "")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_SpCode") : dtTable.Clear()
                Me.FillDataTable(dtTable)
                If Not IsNothing(dtTable) Then : dtTable.Columns.Remove("CreatedDate") : End If
                Return dtTable.DefaultView()
                'Return Me.FillDataTable(New DataTable("T_Program")).DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetDPRD(ByVal SearchSpCode As String) As DataView
            Try
                Dim Query As String = ""
                If (String.IsNullOrEmpty(SearchSpCode)) Then
                    Query = "SET NOCOUNT ON;SELECT DISTINCT TOP 100 MP.PROGRAM_ID,MP.PROGRAM_NAME FROM MRKT_MARKETING_PROGRAM MP " & _
                    "INNER JOIN MRKT_BRANDPACK MB ON MP.PROGRAM_ID = MB.PROGRAM_ID INNER JOIN " & _
                    " MRKT_BRANDPACK_DISTRIBUTOR MBD ON MBD.PROG_BRANDPACK_ID = MB.PROG_BRANDPACK_ID " & _
                    " WHERE MBD.ISRPK = 1 AND YEAR(MP.START_DATE) >= YEAR(GETDATE()) -1 ORDER BY MP.PROGRAM_ID ASC OPTION(KEEP PLAN); "
                Else
                    Query = "SET NOCOUNT ON;SELECT DISTINCT TOP 100 MP.PROGRAM_ID,MP.PROGRAM_NAME FROM MRKT_MARKETING_PROGRAM MP " & _
                   "INNER JOIN MRKT_BRANDPACK MB ON MP.PROGRAM_ID = MB.PROGRAM_ID INNER JOIN " & _
                   " MRKT_BRANDPACK_DISTRIBUTOR MBD ON MBD.PROG_BRANDPACK_ID = MB.PROG_BRANDPACK_ID " & _
                   " WHERE MBD.ISRPK = 1 AND MP.PROGRAM_NAME LIKE '%" & SearchSpCode & "%'AND YEAR(MP.START_DATE) >= YEAR(GETDATE()) -1 ORDER BY MP.PROGRAM_ID ASC OPTION(KEEP PLAN); "
                    'Query = "SELECT TOP 50 PROGRAM_ID FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID LIKE '%" & SearchSpCode & "%'"
                End If
                Me.CreateCommandSql("sp_executesql", "") : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.baseDataView = Me.FillDataTable(New DataTable("DPRD")).DefaultView
                Return Me.baseDataView
            Catch ex As Exception
                Me.CloseConnection() : Throw ex
            End Try
        End Function

        Public ReadOnly Property ViewTarget() As DataView
            Get
                Return Me.baseDataView
            End Get
        End Property

        Private Function AddQueryYear(ByVal Year As Integer) As String
            Dim query As String = "" & vbCrLf & "SELECT SOD.IDApp, PO_" & Year.ToString() & ".TerritoryCode AS TERITORY_ID," _
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
            Return query
        End Function

        Public Function GetTargetReaching(ByVal SpCode As String, ByVal TypeApp As String, ByRef DescriptionSP As DataTable) As DataView
            Try
                Dim Query As String = ""
                If TypeApp = "DPRDS" Then
                    Query = "SET NOCOUNT ON;IF NOT EXISTS(SELECT PROGRAM_ID FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID = @SpCode)" & vbCrLf _
                           & " BEGIN " & vbCrLf _
                           & " RAISERROR('the program has not been registered in Sales Program',16,1); " & vbCrLf _
                           & " RETURN " & vbCrLf _
                           & " END " & vbCrLf _
                           & " SELECT TERR.TERRITORY_AREA,BRch.BrandCode AS BRAND_ID,BR.BRAND_NAME,BRch.KiosCode AS IDKios," & vbCrLf _
                           & " K.Kios_Name AS KIOS_NAME,BRch.Target AS TARGET,BRch.TotalActual AS TOTAL_ACTUAL,((BRch.TotalActual / BRch.Target) * 100)/100 AS [%ACHIEVEMENT]," & vbCrLf _
                           & " BrCh.Dispro AS SCHEMA_DISPRO," _
                           & " BRch.DisproPercentage AS [% BONUS],BRch.BonusQty AS BONUS_QTY,IsBundled AS BNL FROM BrandReaching BRch INNER JOIN" & vbCrLf _
                           & " Kios K ON K.IDKios = BRch.KiosCode INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BRch.BrandCode INNER JOIN " & vbCrLf _
                           & " TERRITORY TERR ON TERR.TERRITORY_ID = BRch.TerritoryCode " & vbCrLf _
                           & " WHERE BRch.SpCode = @SpCode ;"
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT TERR.TERRITORY_AREA,BA.BrandCode AS BRAND_ID,BR.BRAND_NAME,BA.KiosCode AS IDKios," & vbCrLf & _
                            " K.Kios_Name AS KIOS_NAME,BA.Target AS TARGET,BA.TotalActual AS TOTAL_ACTUAL,((BA.TotalActual / BA.Target ) * 100)/100 AS [%ACHIEVEMENT], " & vbCrLf & _
                            " BA.DisproValue AS SCHEMA_DISPRO,BA.BonusValue AS BONUS_VALUE FROM BrandAccomplishment BA INNER JOIN BRND_BRAND BR " & vbCrLf & _
                            " ON BR.BRAND_ID = BA.BrandCode INNER JOIN TERRITORY TERR ON TERR.TERRITORY_ID = BA.TerritoryCode INNER JOIN Kios K ON K.IDKios = BA.KiosCode " & vbCrLf & _
                            " WHERE BA.SpCode = @SpCode ;"
                End If

                Me.CreateCommandSql("", Query) : Me.AddParameter("@SPCode", SqlDbType.VarChar, SpCode)
                Dim tblReaching As New DataTable("ACHIEVEMENT_DPRD") : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection() : Me.SqlDat.Fill(tblReaching) : Me.baseDataView = tblReaching.DefaultView()
                If TypeApp = "DPRDS" Then
                    Query = "SET NOCOUNT ON;SELECT PROGRAM_NAME,P.START_DATE,P.END_DATE,(SELECT TOP 1 CreatedBy FROM BrandReaching " & vbCrLf & _
                            " WHERE SpCode = P.PROGRAM_ID)AS CreatedBy FROM MRKT_MARKETING_PROGRAM P WHERE PROGRAM_ID = @SpCode ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT TOP 1 'PROGRAM_NAME' = 'DPRDM',StartDate AS START_DATE,EndDate AS END_DATE,CreatedBy " & vbCrLf & _
                            " FROM RecapDPRDM WHERE SpCode = @SpCode ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.SqlDat.Fill(DescriptionSP) : Me.CloseConnection() : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection() : Throw ex
            End Try
            Return Me.baseDataView
        End Function

        Public Sub GetTargetReaching(ByVal SpCode As String, ByRef DVBrand As DataView, ByRef DVDetailReaching As DataView, ByRef DVKios As DataView)
            Try
                Me.CreateCommandSql("Usp_Target_Reaching_Kios", "")
                Me.AddParameter("@SpCode", SqlDbType.VarChar, SpCode, 16)
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.baseDataSet = New DataSet("DStargetReaching")
                Me.OpenConnection() : Me.SqlDat.Fill(Me.baseDataSet)
                If (Me.baseDataSet.Tables.Count > 0) Then
                    DVBrand = Me.baseDataSet.Tables(0).DefaultView : DVDetailReaching = Me.baseDataSet.Tables(1).DefaultView
                    DVKios = Me.baseDataSet.Tables(2).DefaultView
                End If
            Catch ex As Exception
                Me.CloseConnection() : Throw ex
            End Try
        End Sub

        Private Function BuildSP(ByVal TableKios As String) As String
            Dim Query As String = ""
            If TableKios <> "" Then
                Query = "SET NOCOUNT ON;SELECT OPO.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,OPO.PO_REF_DATE,OPO.PO_REF_NO,BR.BRAND_ID,BR.BRAND_NAME,OPB.BRANDPACK_ID," & vbCrLf & _
                       " BP.BRANDPACK_NAME,OPB.PO_ORIGINAL_QTY,ISNULL(OA_DISC.DISC_QTY,0)AS DISC_QTY " & vbCrLf & _
                       " FROM ORDR_PURCHASE_ORDER OPO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                       " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = OPB.BRANDPACK_ID " & vbCrLf & _
                       " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID " & vbCrLf & _
                       " LEFT OUTER JOIN " & vbCrLf & _
                       "           (" & vbCrLf & _
                       "             SELECT OO.PO_BRANDPACK_ID,ISNULL(SUM(OO.DISC_QTY),0) AS DISC_QTY FROM " & vbCrLf & _
                       "	          (" & vbCrLf & _
                       "	            SELECT OOAB.PO_BRANDPACK_ID,OOAB.OA_BRANDPACK_ID,ISNULL(SUM(OOABD.DISC_QTY),0) AS DISC_QTY" & vbCrLf & _
                       "	            FROM ORDR_OA_BRANDPACK OOAB INNER JOIN ORDR_OA_BRANDPACK_DISC OOABD " & vbCrLf & _
                       "	            ON OOAB.OA_BRANDPACK_ID = OOABD.OA_BRANDPACK_ID " & vbCrLf & _
                       "                WHERE OOABD.GQSY_SGT_P_FLAG = 'MG' " & vbCrLf & _
                       "                GROUP BY OOAB.PO_BRANDPACK_ID,OOAB.OA_BRANDPACK_ID " & vbCrLf & _
                       "               )OO GROUP BY PO_BRANDPACK_ID " & vbCrLf & _
                       "	        )OA_DISC ON OPB.PO_BRANDPACK_ID = OA_DISC.PO_BRANDPACK_ID " & vbCrLf & _
                       " INNER JOIN DIST_DISTRIBUTOR DR ON OPO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID " & vbCrLf & _
                       " WHERE OPO.PO_REF_DATE >= @START_DATE " & vbCrLf & _
                       " AND OPO.PO_REF_DATE <= @END_DATE " & vbCrLf & _
                       " AND OPO.DISTRIBUTOR_ID = ANY(SELECT DISTRIBUTOR_ID FROM TEMPDB..##DISTRIBUTOR) " & vbCrLf & _
                       " AND EXISTS(SELECT BRANDPACK_ID FROM MRKT_BRANDPACK WHERE PROGRAM_ID = @SpCode AND BRANDPACK_ID = OPB.BRANDPACK_ID) OPTION(KEEP PLAN); " & vbCrLf & _
                       " SELECT OO.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,BR.BRAND_ID,BR.BRAND_NAME,OO.BRANDPACK_ID," & vbCrLf & _
                       " BP.BRANDPACK_NAME,ISNULL(OO.TOTAL_PO,0)AS TOTAL_PO,ISNULL(OA.DISC_QTY,0)AS DISC_QTY,ISNULL(POKIOS.Actual,0)AS [Actual PO Kios]  " & vbCrLf & _
                       " FROM " & vbCrLf & _
                       "    (" & vbCrLf & _
                       "     SELECT OPO.DISTRIBUTOR_ID,OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO " & vbCrLf & _
                       " 	 FROM ORDR_PURCHASE_ORDER OPO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                       " 	 WHERE OPO.PO_REF_DATE >= @START_DATE " & vbCrLf & _
                       "	 AND OPO.PO_REF_DATE <= @END_DATE " & vbCrLf & _
                       "	 GROUP BY OPO.DISTRIBUTOR_ID,OPB.BRANDPACK_ID " & vbCrLf & _
                       "    )OO " & vbCrLf & _
                       " LEFT OUTER JOIN" & vbCrLf & _
                       "		        (" & vbCrLf & _
                       " 		          SELECT OO.DISTRIBUTOR_ID,OO.BRANDPACK_ID,ISNULL(SUM(DISC_QTY),0)AS DISC_QTY " & vbCrLf & _
                       "			      FROM (" & vbCrLf & _
                       "			             SELECT OPO.DISTRIBUTOR_ID,OPB.BRANDPACK_ID,ISNULL(OO.DISC_QTY,0)AS DISC_QTY " & vbCrLf & _
                       " 			             FROM ORDR_PURCHASE_ORDER OPO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                       "                         INNER JOIN " & vbCrLf & _
                       "                      			   (" & vbCrLf & _
                       "                                    SELECT OO.PO_BRANDPACK_ID,ISNULL(SUM(OO.DISC_QTY),0) AS DISC_QTY FROM " & vbCrLf & _
                       "			                         (" & vbCrLf & _
                       "                        			   SELECT OOAB.PO_BRANDPACK_ID,OOAB.OA_BRANDPACK_ID,ISNULL(SUM(OOABD.DISC_QTY),0) AS DISC_QTY " & vbCrLf & _
                       "			                           FROM ORDR_OA_BRANDPACK OOAB INNER JOIN ORDR_OA_BRANDPACK_DISC OOABD " & vbCrLf & _
                       "  			                           ON OOAB.OA_BRANDPACK_ID = OOABD.OA_BRANDPACK_ID " & vbCrLf & _
                       "			                           WHERE OOABD.GQSY_SGT_P_FLAG = 'MG' " & vbCrLf & _
                       "			                           GROUP BY OOAB.PO_BRANDPACK_ID,OOAB.OA_BRANDPACK_ID " & vbCrLf & _
                       "                          			  )OO GROUP BY PO_BRANDPACK_ID " & vbCrLf & _
                       "                      			    )OO ON OO.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                       "			                         WHERE OPO.PO_REF_DATE >= @START_DATE " & vbCrLf & _
                       "                      			     AND OPO.PO_REF_DATE <= @END_DATE " & vbCrLf & _
                       "              			)OO GROUP BY OO.DISTRIBUTOR_ID,OO.BRANDPACK_ID " & vbCrLf & _
                       "		          )OA " & vbCrLf & _
                       "        		   ON OO.DISTRIBUTOR_ID = OA.DISTRIBUTOR_ID AND OO.BRANDPACK_ID = OA.BRANDPACK_ID " & vbCrLf & _
                       " LEFT OUTER JOIN " & vbCrLf & _
                       "                (" & vbCrLf & _
                       "		          SELECT PK.DISTRIBUTOR_ID,PK.BRANDPACK_ID,ISNULL(SUM(PK.QUANTITY),0)AS Actual FROM " & TableKios & vbCrLf & _
                       "   		          PK WHERE PK.PO_DATE >= @START_DATE " & vbCrLf & _
                       "		          AND PK.PO_DATE <= @END_DATE " & vbCrLf & _
                       "		          GROUP BY PK.DISTRIBUTOR_ID,PK.BRANDPACK_ID " & vbCrLf & _
                       "		        )POKIOS " & vbCrLf & _
                       "		         ON POKIOS.DISTRIBUTOR_ID = OO.DISTRIBUTOR_ID AND POKIOS.BRANDPACK_ID = OO.BRANDPACK_ID " & vbCrLf & _
                       " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = OO.BRANDPACK_ID " & vbCrLf & _
                       " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID " & vbCrLf & _
                       " INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = OO.DISTRIBUTOR_ID " & vbCrLf & _
                       " WHERE OO.DISTRIBUTOR_ID = ANY(SELECT DISTRIBUTOR_ID FROM TEMPDB..##DISTRIBUTOR) " & vbCrLf & _
                       " AND EXISTS(SELECT BRANDPACK_ID FROM MRKT_BRANDPACK WHERE PROGRAM_ID = @SpCode AND BRANDPACK_ID = OO.BRANDPACK_ID) OPTION(KEEP PLAN);"
            Else
                Query = "SET NOCOUNT ON;SELECT OPO.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,OPO.PO_REF_DATE,OPO.PO_REF_NO,BR.BRAND_ID,BR.BRAND_NAME,OPB.BRANDPACK_ID," & vbCrLf & _
                       " BP.BRANDPACK_NAME,OPB.PO_ORIGINAL_QTY,ISNULL(OA_DISC.DISC_QTY,0)AS DISC_QTY " & vbCrLf & _
                       " FROM ORDR_PURCHASE_ORDER OPO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                       " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = OPB.BRANDPACK_ID " & vbCrLf & _
                       " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID " & vbCrLf & _
                       " LEFT OUTER JOIN " & vbCrLf & _
                       "		        (" & vbCrLf & _
                       "	              SELECT OO.PO_BRANDPACK_ID,ISNULL(SUM(OO.DISC_QTY),0) AS DISC_QTY FROM " & vbCrLf & _
                       "	               (" & vbCrLf & _
                       "	                 SELECT OOAB.PO_BRANDPACK_ID,OOAB.OA_BRANDPACK_ID,ISNULL(SUM(OOABD.DISC_QTY),0) AS DISC_QTY " & vbCrLf & _
                       "	                 FROM ORDR_OA_BRANDPACK OOAB INNER JOIN ORDR_OA_BRANDPACK_DISC OOABD " & vbCrLf & _
                       "                     ON OOAB.OA_BRANDPACK_ID = OOABD.OA_BRANDPACK_ID " & vbCrLf & _
                       "	                 WHERE OOABD.GQSY_SGT_P_FLAG = 'MG' " & vbCrLf & _
                       "	                 GROUP BY OOAB.PO_BRANDPACK_ID,OOAB.OA_BRANDPACK_ID " & vbCrLf & _
                       "	                )OO GROUP BY PO_BRANDPACK_ID " & vbCrLf & _
                       "	             )OA_DISC ON OPB.PO_BRANDPACK_ID = OA_DISC.PO_BRANDPACK_ID " & vbCrLf & _
                       " INNER JOIN DIST_DISTRIBUTOR DR ON OPO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID " & vbCrLf & _
                       " WHERE OPO.PO_REF_DATE >= @START_DATE " & vbCrLf & _
                       " AND OPO.PO_REF_DATE <= @END_DATE " & vbCrLf & _
                       " AND OPO.DISTRIBUTOR_ID = ANY(SELECT DISTRIBUTOR_ID FROM TEMPDB..##DISTRIBUTOR) " & vbCrLf & _
                       " AND EXISTS(SELECT BRANDPACK_ID FROM MRKT_BRANDPACK WHERE PROGRAM_ID = @SpCode AND BRANDPACK_ID = OPB.BRANDPACK_ID)OPTION(KEEP PLAN); " & vbCrLf & _
                       " SELECT OO.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,BR.BRAND_ID,BR.BRAND_NAME,OO.BRANDPACK_ID,BP.BRANDPACK_NAME," & vbCrLf & _
                       " ISNULL(OO.TOTAL_PO,0) AS TOTAL_PO,ISNULL(OA.DISC_QTY,0) AS DISC_QTY " & vbCrLf & _
                       " FROM " & vbCrLf & _
                       "	 (" & vbCrLf & _
                       "	   SELECT OPO.DISTRIBUTOR_ID,OPB.BRANDPACK_ID,ISNULL(SUM(OPB.PO_ORIGINAL_QTY),0) AS TOTAL_PO " & vbCrLf & _
                       " 	   FROM ORDR_PURCHASE_ORDER OPO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                       " 	   WHERE OPO.PO_REF_DATE >= @START_DATE " & vbCrLf & _
                       "       AND OPO.PO_REF_DATE <= @END_DATE " & vbCrLf & _
                       "	   GROUP BY OPO.DISTRIBUTOR_ID,OPB.BRANDPACK_ID " & vbCrLf & _
                       "	 )OO " & vbCrLf & _
                       " LEFT OUTER JOIN " & vbCrLf & _
                       "		   (" & vbCrLf & _
                       "		    SELECT OO.DISTRIBUTOR_ID,OO.BRANDPACK_ID,ISNULL(SUM(DISC_QTY),0) AS DISC_QTY " & vbCrLf & _
                       "			FROM " & vbCrLf & _
                       "                (" & vbCrLf & _
                       "			     SELECT OPO.DISTRIBUTOR_ID,OPB.BRANDPACK_ID,ISNULL(OO.DISC_QTY,0)AS DISC_QTY " & vbCrLf & _
                       " 			     FROM ORDR_PURCHASE_ORDER OPO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                       "                 INNER JOIN " & vbCrLf & _
                       "			               (" & vbCrLf & _
                       "                             SELECT OO.PO_BRANDPACK_ID,ISNULL(SUM(OO.DISC_QTY),0) AS DISC_QTY FROM " & vbCrLf & _
                       "			                  (" & vbCrLf & _
                       "			                    SELECT OOAB.PO_BRANDPACK_ID,OOAB.OA_BRANDPACK_ID,ISNULL(SUM(OOABD.DISC_QTY),0) AS DISC_QTY " & vbCrLf & _
                       "			                    FROM ORDR_OA_BRANDPACK OOAB INNER JOIN ORDR_OA_BRANDPACK_DISC OOABD " & vbCrLf & _
                       "			                    ON OOAB.OA_BRANDPACK_ID = OOABD.OA_BRANDPACK_ID " & vbCrLf & _
                       "			                    WHERE OOABD.GQSY_SGT_P_FLAG = 'MG' " & vbCrLf & _
                       "			                    GROUP BY OOAB.PO_BRANDPACK_ID,OOAB.OA_BRANDPACK_ID " & vbCrLf & _
                       "			                   )OO GROUP BY PO_BRANDPACK_ID " & vbCrLf & _
                       "			                )OO ON OO.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                       "			                 WHERE OPO.PO_REF_DATE >= @START_DATE " & vbCrLf & _
                       "			                 AND OPO.PO_REF_DATE <= @END_DATE " & vbCrLf & _
                       "			     )OO GROUP BY OO.DISTRIBUTOR_ID,OO.BRANDPACK_ID " & vbCrLf & _
                       "		     )OA " & vbCrLf & _
                       "		      ON OO.DISTRIBUTOR_ID = OA.DISTRIBUTOR_ID AND OO.BRANDPACK_ID = OA.BRANDPACK_ID " & vbCrLf & _
                       " INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = OO.DISTRIBUTOR_ID " & vbCrLf & _
                       " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = OO.BRANDPACK_ID " & vbCrLf & _
                       " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID " & vbCrLf & _
                       " WHERE OO.DISTRIBUTOR_ID = ANY(SELECT DISTRIBUTOR_ID FROM TEMPDB..##DISTRIBUTOR) " & vbCrLf & _
                       " AND EXISTS(SELECT BRANDPACK_ID FROM MRKT_BRANDPACK WHERE PROGRAM_ID = @SpCode AND BRANDPACK_ID = OO.BRANDPACK_ID) OPTION(KEEP PLAN); "
            End If
            Return Query
        End Function

        Private Function GetPercentage(ByVal persen As Integer, ByVal TotalPO As Decimal, ByVal TotalBonus As Int64) As Decimal
            If (TotalBonus <= 0) Then
                Return 0
            End If
            Return Convert.ToDecimal((TotalPO * persen * 1) / TotalBonus)
        End Function

        Public Sub GetPOTargetReaching(ByVal SpCode As String, ByVal START_DATE As DateTime, ByVal END_DATE As DateTime, ByVal isAllDistributor As Boolean, _
         ByVal ListDistributor As List(Of String), ByRef DVHeader As DataView, ByRef DVDetail As DataView)
            Try
                Dim StartYear As String = "" : Dim EndYear As String = ""
                ''bikin po_kios
                Dim Query As String = ""
                Query = "SET NOCOUNT ON;SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'base table'" _
                           & " AND TABLE_NAME = 'POHeader_" & START_DATE.Year.ToString & "' OPTION(KEEP PLAN); "
                Me.CreateCommandSql("sp_executesql", "") : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                If Not IsNothing(Me.SqlCom.ExecuteScalar()) Then
                    StartYear = START_DATE.Year.ToString()
                End If : Me.ClearCommandParameters()
                Query = "SET NOCOUNT ON;SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'base table'" _
                      & " AND TABLE_NAME = 'POHeader_" & END_DATE.Year.ToString & "' OPTION(KEEP PLAN); "
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                If Not IsNothing(Me.SqlCom.ExecuteScalar()) Then
                    EndYear = END_DATE.Year.ToString()
                End If : Me.ClearCommandParameters()
                Dim TableKios As String = ""
                If (StartYear <> "") And (EndYear <> "") Then
                    TableKios = "Tempdb..##POKios_" & StartYear & "_" & EndYear
                ElseIf (StartYear <> "") Then
                    TableKios = "Tempdb..##POKios_" & StartYear
                End If
                If TableKios <> "" Then
                    Query = "SET NOCOUNT ON;IF OBJECT_ID('" & TableKios & "') IS NULL " & vbCrLf _
                            & "BEGIN " & vbCrLf _
                            & " SELECT * INTO " & TableKios & " FROM("
                    Query &= AddQueryYear(StartYear) & vbCrLf
                    If EndYear <> "" Then
                        Query &= " UNION " & vbCrLf
                        Query &= AddQueryYear(EndYear) & vbCrLf
                    End If
                    Query &= ")PO OPTION(KEEP PLAN);" & vbCrLf _
                          & " END"
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Else
                    Me.CloseConnection() : Me.ClearCommandParameters() : Throw New Exception("Data PO kios is still empty.")
                End If
                Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                If (Not isAllDistributor) Then
                    Query = "SET NOCOUNT ON; CREATE TABLE ##DISTRIBUTOR(DISTRIBUTOR_ID VARCHAR(10))"
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Me.SqlCom.CommandType = CommandType.Text
                    For i As Integer = 0 To ListDistributor.Count - 1
                        Query = "SET NOCOUNT ON;INSERT INTO TEMPDB..##DISTRIBUTOR(DISTRIBUTOR_ID)VALUES(@DISTRIBUTOR_ID)"
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, ListDistributor(i).ToString(), 10)
                        Me.SqlCom.CommandText = Query : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next
                Else
                    Query = "SET NOCOUNT ON;SELECT DISTINCT DISTRIBUTOR_ID INTO TEMPDB..##DISTRIBUTOR FROM DIST_DISTRIBUTOR DR " & vbCrLf & _
                    " WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM MRKT_BRANDPACK_DISTRIBUTOR " & vbCrLf & _
                            " WHERE PROG_BRANDPACK_ID IN(SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK " & vbCrLf & _
                            " WHERE PROGRAM_ID = @SpCode) AND DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) OPTION(KEEP PLAN);"
                    Me.SqlCom.CommandType = CommandType.Text : Me.SqlCom.CommandText = Query
                    Me.AddParameter("@SpCode", SqlDbType.NVarChar, SpCode, 15)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "IF NOT EXISTS(SELECT PROGRAM_ID FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID = @SpCode) " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "      RAISERROR('Can not Find such Program ID',16,1); " & vbCrLf & _
                        "      RETURN; " & vbCrLf & _
                        " END"
                Me.SqlCom.CommandText = Query
                Me.AddParameter("@SpCode", SqlDbType.VarChar, SpCode, 15)
                Me.SqlCom.ExecuteScalar() : Query = BuildSP(TableKios)
                Me.SqlCom.CommandText = Query
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, START_DATE)
                Me.AddParameter("@END_DATE", SqlDbType.DateTime, END_DATE)
                Dim Ds As New DataSet("DSPOTargetKios") : Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(Ds)
                Me.ClearCommandParameters() : Me.CommiteTransaction()
                'remove table distributor di tempdb
                Query = "SET NOCOUNT ON;DROP TABLE TEMPDB..##DISTRIBUTOR"
                Me.SqlCom.CommandType = CommandType.StoredProcedure : Me.SqlCom.CommandText = "sp_executesql"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                'looping data untuk ngambil bonus disc perbrandpack
                If Ds.Tables.Count > 0 Then
                    'ambil totalBonusqty untuk brandpack dimana brandpacknya adalah brandpack dari dvheader
                    'bila dapat maka dapatkan percentase berapa percentase dari totalpoqty dengan total bonusqty
                    'hasil dari percentase tersebut dikalikan dengan total bonus
                    'hasi dari kali totalqty yang di bebankan  buat distributor tsb
                    'hasil = total disc_qty -  totalqty yang di bebankan buat distributor tsb
                    DVHeader = Ds.Tables(1).DefaultView() : DVDetail = Ds.Tables(0).DefaultView
                    Dim colTotalBonus As New DataColumn("TotalBonusKios", Type.GetType("System.Decimal"))
                    colTotalBonus.DefaultValue = 0
                    Dim colLeftMoreBonus As New DataColumn("Balance", Type.GetType("System.Decimal"))
                    colLeftMoreBonus.DefaultValue = 0
                    DVHeader.Table.Columns.Add(colTotalBonus) : DVHeader.Table.Columns.Add(colLeftMoreBonus)
                    If (Not DVHeader.Table.Columns.Contains("Actual PO Kios")) Then
                        DVHeader.Table.Columns.Add(New DataColumn("Actual PO Kios", Type.GetType("System.Decimal")))
                    End If
                    Query = "SET NOCOUNT ON;SELECT ISNULL(SUM(DiscQty),0)AS TotalBonus FROM DetailReaching WHERE BrandPackCode = @BrandPackCode " & _
                            " AND FkCode IN(SELECT CodeApp FROM BrandReaching WHERE SpCode = @SpCode) OPTION(KEEP PLAN);"
                    Me.SqlCom.CommandType = CommandType.Text : Me.SqlCom.CommandText = Query
                    For i As Integer = 0 To DVHeader.Count - 1
                        Me.AddParameter("@BrandPackCode", SqlDbType.VarChar, DVHeader(i)("BRANDPACK_ID"), 14)
                        Me.AddParameter("@SpCode", SqlDbType.VarChar, SpCode, 15)
                        Dim TBonusBrandPackQty As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
                        Me.ClearCommandParameters() : Dim BonusPercentage As Decimal = 0, Balance As Decimal = 0
                        If (Not IsDBNull(DVHeader(i)("Actual PO Kios")) And Not IsNothing(DVHeader(i)("Actual PO Kios"))) Then
                            BonusPercentage = Me.GetPercentage(100, Convert.ToDecimal(DVHeader(i)("Actual PO Kios")), TBonusBrandPackQty)
                        End If
                        Dim TExpenseBonusQty As Decimal = (BonusPercentage * TBonusBrandPackQty) / 100
                        If Not IsDBNull(DVHeader(i)("DISC_QTY")) And Not IsNothing(DVHeader(i)("DISC_QTY")) Then
                            Balance = Convert.ToDecimal(DVHeader(i)("DISC_QTY")) - TExpenseBonusQty
                        Else : Balance = -TExpenseBonusQty
                        End If
                        DVHeader(i).BeginEdit() : DVHeader(i)("TotalBonusKios") = TExpenseBonusQty
                        If IsDBNull(DVHeader(i)("Actual PO Kios")) Or IsNothing(DVHeader(i)("Actual PO Kios")) Then
                            DVHeader(i)("Actual PO Kios") = 0
                        End If
                        DVHeader(i)("Balance") = Balance : DVHeader(i).EndEdit()
                    Next
                    DVHeader.Table.Columns("DISC_QTY").Caption = "GIVEN_DISC"
                    Me.CloseConnection()
                End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Throw ex
            End Try
        End Sub

        Public Function getRecapDPRD(ByVal reportDPRDType As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As DataView
            Try
                ''ambil data di table achievement yang mungkin belum di masukan 
                ''
                Me.CreateCommandSql("Usp_Get_RecapDPRD_National", "")
                Me.AddParameter("@ReportType", SqlDbType.VarChar, reportDPRDType, 5)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Return Me.FillDataTable(New DataTable("RECAP DPRD")).DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getRecapSummaryDPRD(ByVal ReportDPRDType As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime)
            Try
                Me.CreateCommandSql("Usp_Get_Summary_RecapDPRD_National", "")
                Me.AddParameter("@ReportType", SqlDbType.VarChar, ReportDPRDType, 5)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Return Me.FillDataTable(New DataTable("RECAP DPRD")).DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
        End Sub
    End Class

End Namespace

