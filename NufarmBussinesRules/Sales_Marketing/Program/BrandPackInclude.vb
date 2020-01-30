Imports System.Data.SqlClient
Imports Nufarm.Domain
Namespace Program
    Public Class BrandPackInclude
        Inherits NufarmBussinesRules.Program.ProgramRegistering

#Region " deklarasi "
        Protected Query As String = ""
        Private m_ViewBrandPack As DataView
        Private m_ViewMarketingBrandPack As DataView
        Private m_ViewOriginalBrandPack As DataView
        Protected m_ViewProgram As DataView
        Private m_ViewMRK_BRANDPACK_DISTRIBUTOR As DataView
        Private m_ViewDistributor As DataView

        Public BRANDPACK_ID As String
        Public START_DATE As Object
        Public END_DATE As Object
        Public PROG_BRANDPACK_ID As String
        Public BRANDPACK_NAME As String
        Public ItemChanges As System.Collections.Specialized.NameValueCollection
        Public PROG_BRANDPACK_DIST_ID As String
        Public AGREE_BRANDPACK_ID As String
        Public DISTRIBUTOR_ID As String
        Public GIVEN_DISC_PCT As Decimal = 0
        Public TARGET_QTY As Decimal = 0
        Public TARGET_DISC_PCT As Decimal = 0
        Public STEPPING As Int16
        Public DISTRIBUTOR_NAME As String
        Public ISRPK As Boolean
        Public ISPKPP As Boolean
        Public GIVEN_PKPP As Decimal = 0
        Public TARGET_PKPP As Decimal = 0
        Public ISCP As Boolean
        Public ISCPMRT As Boolean = False
        Public isSCPD As Boolean = False

        Public GIVEN_CP As Decimal = 0
        Public GIVEN_CPRMT As Decimal = 0

        Public TARGET_CP As Decimal = 0
        Public TARGET_CPMRT As Decimal = 0
        Public ISDK As Boolean
        Public GIVEN_DK As Decimal = 0
        Public TARGET_DK As Decimal = 0
        Public ISHK As Boolean
        Public TARGET_HK As Decimal = 0
        Public PRICE_HK As Decimal = 0
        Public ISCPR As Boolean
        Public GIVEN_CPR As Decimal = 0
        Public TARGET_CPR As Decimal = 0

        Public BONUS_VALUE As Decimal = 0
        Public SHIP_TO_ID As String = ""
        Public IS_T_TM_DIST As Boolean = False
        Public DESCRIPTIONS As String = ""
#End Region

#Region " Sub "
        Public Sub GetDateFromProgram(ByVal ProgramID As String)
            Try
                Me.ExecuteReader("", "SELECT START_DATE,END_DATE FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID = '" + ProgramID + "'")
                While Me.SqlRe.Read()
                    Me.START_DATE = Me.SqlRe(0)
                    Me.END_DATE = Me.SqlRe(1)
                End While
                Me.SqlRe.Close()
                Me.CloseConnection()
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Me.SqlRe.IsClosed = False Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Sub DeletePROG_BRANDPACK(ByVal PROG_BRANDPACK_ID As String)
            Try
                Me.GetConnection()
                Me.DeleteData("Sp_Delete_MRKT_BRANDPACK", "")
                Me.AddParameter("@PROG_BRANDPACK_ID", SqlDbType.VarChar, PROG_BRANDPACK_ID, 30)
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
        End Sub

        Public Sub GetDateFromBrandPack(ByVal PROG_BRANDPACK_ID As String)
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "IF NOT EXISTS(SELECT START_DATE,END_DATE FROM MRKT_BRANDPACK WHERE PROG_BRANDPACK_ID = '" & PROG_BRANDPACK_ID & "') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "  RAISERROR('BrandPack for Program does not exist.',16,1) ;RETURN;" & vbCrLf & _
                        " END " & vbCrLf & _
                        " SELECT START_DATE,END_DATE FROM MRKT_BRANDPACK WHERE PROG_BRANDPACK_ID = '" + PROG_BRANDPACK_ID + "';"
                Me.CreateCommandSql("sp_executesql", "") : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteReader()
                While Me.SqlRe.Read()
                    Me.START_DATE = Me.SqlRe("START_DATE")
                    Me.END_DATE = Me.SqlRe("END_DATE")
                End While : Me.SqlRe.Close() : Me.CloseConnection() : Me.ClearCommandParameters()
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Me.SqlRe.IsClosed = False Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Sub DeletePROG_BRANDPACK_DIST_ID(ByVal PROG_BRANDPACK_DIST_ID As String)
            Try
                Me.GetConnection()
                Me.DeleteData("Sp_Delete_MRKT_BRANDPACK_DISTRIBUTOR", "")
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID, 40)
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
        End Sub

        Public Sub DeletePROG_BRANDPACK_DIST_ID_1(ByVal PROG_BRANDPACK_DIST_ID As String)
            Try
                Me.GetConnection()
                Me.DeleteData("Sp_Delete_STEPPING_DISCOUNT", "")
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID, 40)
                Me.OpenConnection()
                Me.BeginTransaction()

                Me.ExecuteNonQuery()

                Me.DeleteData("Sp_Delete_MRKT_BRANDPACK_DISTRIBUTOR", "")
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID, 40)

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
        End Sub
#End Region

#Region " Function "

#Region " function Global "
        Private Function RepLaceDotWithComa(ByVal text As String) As String
            Dim l As Integer = Len(Trim(text))
            Dim w As Integer = 1
            Dim s As String = ""
            Dim a As String = ""
            Do Until w = l + 1
                s = Mid(Trim(text), w, 1)
                If s = "." Then
                    s = ","
                End If
                a = a & s
                w += 1
            Loop
            Return a
        End Function
        Public Sub DismisReminder(ByVal listProgram As List(Of String), ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " UPDATE MRKT_BRANDPACK_DISTRIBUTOR SET DR = 1 WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID ;"
                If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                Else : Me.CreateCommandSql("", Query)
                End If
                OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = SqlTrans
                For i As Integer = 0 To listProgram.Count - 1
                    AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, listProgram(i), 100)
                    Me.SqlCom.ExecuteScalar() : ClearCommandParameters()
                Next
                Me.CommiteTransaction()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

#Region "====SALES DKN===="

        Public Function getEndDateCurrentAgreement(ByVal mustCloseConnection As Boolean) As Object
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT MAX(END_DATE) FROM AGREE_AGREEMENT WHERE END_DATE >= GETDATE(); "
                If IsNothing(Me.SqlCom) Then : CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return retval
            Catch ex As Exception
                Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getDataBrand(ByVal StartPeriode As DateTime, ByVal EndPeriode As DateTime, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT BR.BRAND_ID,BR.BRAND_NAME FROM BRND_BRAND BR WHERE EXISTS(SELECT AB.BRAND_ID FROM AGREE_BRAND_INCLUDE AB INNER JOIN AGREE_AGREEMENT AA " & vbCrLf & _
                        " ON AA.AGREEMENT_NO = AB.AGREEMENT_NO WHERE AA.START_DATE <= @StartDate AND AA.END_DATE >= @EndDate AND AB.BRAND_ID = BR.BRAND_ID); "
                If IsNothing(Me.SqlCom) Then : CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                AddParameter("@StartDate", SqlDbType.SmallDateTime, StartPeriode)
                AddParameter("@EndDate", SqlDbType.SmallDateTime, EndPeriode)
                Dim dtBrand As New DataTable("T_Brand")
                OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(dtBrand)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dtBrand
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getDataBrand(ByVal SDSIDApp As Integer, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT SDB.*, BR.BRAND_NAME FROM SALES_DKN_BRAND SDB INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = SDB.BRAND_ID " & vbCrLf & _
                        " WHERE SDB.FKApp = @FKApp_SDS "
                If IsNothing(Me.SqlCom) Then : CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                AddParameter("@FKApp_SDS", SqlDbType.Int, SDSIDApp)
                OpenConnection()
                Dim dtBrand As New DataTable("T_BrandGrid")
                setDataAdapter(Me.SqlCom).Fill(dtBrand)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dtBrand
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function getDataProgressive(ByVal SDSIDApp As Integer, ByVal MustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT * FROM SALES_DKN_PROGRESSIVE WHERE FKApp_SDS = @FKApp_SDS ORDER BY MIN_TO_DATE ASC ;"
                If IsNothing(Me.SqlCom) Then : CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                AddParameter("@FKApp_SDS", SqlDbType.Int, SDSIDApp)
                OpenConnection()
                Dim dtProg As New DataTable("T_Progressive")
                Me.setDataAdapter(Me.SqlCom).Fill(dtProg)
                Me.ClearCommandParameters()
                If MustCloseConnection Then : Me.CloseConnection() : End If
                Return dtProg
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function hasDataBrand(ByVal BrandID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As Boolean
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT SDB.BRAND_ID FROM SALES_DKN_BRAND SDB INNER JOIN SALES_DKN_SCHEMA SDS ON SDB.FKApp = SDS.IDApp " & vbCrLf & _
                " WHERE SDB.BRAND_ID = @BrandID " & vbCrLf & _
                " AND ((SDS.START_DATE <= @EndDate AND SDS.START_DATE >= @StartDate) OR(SDS.END_DATE >= @StartDate AND SDS.END_DATE <= @EndDate)));"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                AddParameter("@BrandID", SqlDbType.VarChar, BrandID, 7)
                AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Me.CloseConnection()
                    Return True
                End If
                Me.CloseConnection()
                Return False
            Catch ex As Exception
                Me.CloseConnection() : ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub SaveDKN(ByRef DS As DataSet, ByVal Mode As NufarmBussinesRules.common.Helper.SaveMode, ByVal ODKN As Nufarm.Domain.DKN, ByVal MustCloseConnection As Boolean)
            Try
                Me.OpenConnection()
                Dim InsertedRows() As DataRow = Nothing
                Dim UpdatedRows() As DataRow = Nothing
                Dim DeletedRows() As DataRow = Nothing
                Me.BeginTransaction()
                Dim CommandInsert As SqlCommand = Nothing
                Dim CommandUpdate As SqlCommand = Nothing
                Dim CommandDelete As SqlCommand = Nothing
                SqlDat = New SqlDataAdapter()
                Dim retval = Nothing
                Dim IDAppSDS As Integer = 0
                Dim HasInsertedProg As Boolean = False
                If Mode = common.Helper.SaveMode.Insert Then
                    'insert header
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " IF EXISTS(SELECT IDApp FROM SALES_DKN_SCHEMA WHERE START_DATE = @StartDate AND END_DATE = @EndDate) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " RAISERROR('Periode has exists',16,1); RETURN ;" & vbCrLf & _
                            " END " & vbCrLf & _
                            " INSERT INTO [Nufarm].[dbo].[SALES_DKN_SCHEMA]([START_DATE], [END_DATE], [ProductToGive], [ProductRule], [CreatedDate], [CreatedBy])" & vbCrLf & _
                            " VALUES(@StartDate, @EndDate,@ProductToGive, @ProductRule,@CreatedDate,@CreatedBy) ;" & vbCrLf & _
                            " SELECT @@IDENTITY ;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, ODKN.StartDate)
                    Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, ODKN.EndDate)
                    Me.AddParameter("@ProductToGive", SqlDbType.Char, IIf(ODKN.ProductToGive = "All Products", "A", "C"), 1)
                    Me.AddParameter("@ProductRule", SqlDbType.Char, IIf(ODKN.ProductRule = "All Products", "A", "C"), 1)
                    Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.AddParameter("@CreatedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.SqlCom.Transaction = Me.SqlTrans
                    retval = Me.SqlCom.ExecuteScalar()
                    If retval Is Nothing Or retval Is DBNull.Value Then
                        Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                        Throw New Exception("Unknown error" & vbCrLf & "Transaction Affects 0 record")
                    End If
                    Me.ClearCommandParameters()
                    'inserted
                    IDAppSDS = Convert.ToInt32(retval)
                ElseIf Mode = common.Helper.SaveMode.Update Then
                    IDAppSDS = ODKN.IDApp
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                              " IF EXISTS(SELECT IDApp FROM SALES_DKN_SCHEMA WHERE START_DATE = @StartDate AND END_DATE = @EndDate) " & vbCrLf & _
                              " BEGIN " & vbCrLf & _
                              " UPDATE SALES_DKN_SCHEMA SET START_DATE = @StartDate,END_DATE = @EndDate,ProductToGive = @ProductToGive,ProductRule = @ProductRule,ModifiedDate = @ModifiedDate,ModifiedBy = @ModifiedBy WHERE IDApp = @IDApp ;" & vbCrLf & _
                              " END "
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, ODKN.StartDate)
                    Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, ODKN.EndDate)
                    Me.AddParameter("@ProductToGive", SqlDbType.Char, IIf(ODKN.ProductToGive = "All Products", "A", "C"), 1)
                    Me.AddParameter("@ProductRule", SqlDbType.Char, IIf(ODKN.ProductRule = "All Products", "A", "C"), 1)
                    Me.AddParameter("@ModifiedDate", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.AddParameter("@IDApp", SqlDbType.Int, IDAppSDS)
                    Me.SqlCom.Transaction = Me.SqlTrans : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                'insert brand
                If DS.Tables.Contains("T_BrandGrid") Then
                    InsertedRows = DS.Tables("T_BrandGrid").Select("", "", DataViewRowState.Added)
                    If InsertedRows.Length > 0 Then
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                               " IF NOT EXISTS(SELECT IDApp FROM SALES_DKN_BRAND WHERE BRAND_ID = @BrandID AND FKApp = @FKApp) " & vbCrLf & _
                               " BEGIN " & vbCrLf & _
                               " INSERT INTO [Nufarm].[dbo].[SALES_DKN_BRAND]([FKApp], [BRAND_ID], [CreatedDate], [CreatedBy])" & vbCrLf & _
                               " VALUES(@FKApp, @BrandID, @CreatedDate,@CreatedBy)" & vbCrLf & _
                               " END " & vbCrLf & _
                               " SELECT @@IDENTITY ;"
                        CommandInsert = SqlConn.CreateCommand()
                        With CommandInsert
                            .Transaction = Me.SqlTrans
                            .CommandType = CommandType.Text
                            .CommandText = Query
                            For Each row As DataRow In InsertedRows
                                .Parameters.Add("@FKApp", SqlDbType.Int, 0).Value = IDAppSDS
                                .Parameters.Add("@BrandID", SqlDbType.VarChar, 7).Value = row("BRAND_ID")
                                .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = row("CreatedDate")
                                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = row("CreatedBy")
                                retval = .ExecuteScalar() : .Parameters.Clear()
                                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                                    If ODKN.ProductRule = "Certain Products" Then
                                        Dim IDAppSDB As Integer = Convert.ToInt32(retval)
                                        Dim InsertedRowsProg() As DataRow = Nothing
                                        InsertedRowsProg = DS.Tables("T_Progressive").Select("BRAND_ID = '" & row("BRAND_ID").ToString() & "'", "MIN_TO_DATE ASC", DataViewRowState.Added)
                                        If InsertedRowsProg.Length > 0 Then
                                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                                    " INSERT INTO SALES_DKN_PROGRESSIVE(FKApp_SDB,FKApp_SDS,BRAND_ID,MIN_TO_DATE,DISCOUNT,CreatedDate,CreatedBy) " & vbCrLf & _
                                                    " VALUES(@FKApp_SDB,@FKApp_SDS,@BRAND_ID,@MIN_TO_DATE,@DISCOUNT,@CreatedDate,@CreatedBy) ;" 
                                            Dim CommandInsertProg As SqlCommand = Me.SqlConn.CreateCommand()
                                            With CommandInsertProg
                                                .Transaction = Me.SqlTrans
                                                .CommandType = CommandType.Text
                                                .CommandText = Query
                                                For Each rowProg As DataRow In InsertedRowsProg
                                                    .Parameters.Add("@FKApp_SDS", SqlDbType.Int, 0).Value = IDAppSDS
                                                    .Parameters.Add("@FKApp_SDB", SqlDbType.Int, 0).Value = IDAppSDB
                                                    .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 7).Value = rowProg("BRAND_ID")
                                                    .Parameters.Add("@MIN_TO_DATE", SqlDbType.Int, 0).Value = rowProg("MIN_TO_DATE")
                                                    .Parameters.Add("@DISCOUNT", SqlDbType.Decimal, 0).Value = rowProg("DISCOUNT")
                                                    .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = rowProg("CreatedBy")
                                                    .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0).Value = rowProg("CreatedDate")
                                                    .ExecuteScalar() : .Parameters.Clear()
                                                Next
                                            End With
                                            HasInsertedProg = True
                                        End If
                                    End If
                                End If
                            Next
                        End With
                    End If
                    InsertedRows = DS.Tables("T_Progressive").Select("", "MIN_TO_DATE ASC", DataViewRowState.Added)
                    If ODKN.ProductRule = "All Products" Then
                        If InsertedRows.Length > 0 Then
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                   " INSERT INTO SALES_DKN_PROGRESSIVE(FKApp_SDB,FKApp_SDS,BRAND_ID,MIN_TO_DATE,DISCOUNT,CreatedDate,CreatedBy) " & vbCrLf & _
                                   " VALUES(@FKApp_SDB,@FKApp_SDS,@BRAND_ID,@MIN_TO_DATE,@DISCOUNT,@CreatedDate,@CreatedBy) ;"
                            If IsNothing(CommandInsert) Then
                                CommandInsert = Me.SqlConn.CreateCommand()
                                CommandInsert.Transaction = Me.SqlTrans
                            End If
                            With CommandInsert
                                .CommandType = CommandType.Text
                                .CommandText = Query
                                .Parameters.Add("@FKApp_SDS", SqlDbType.Int, 0).Value = IDAppSDS
                                .Parameters.Add("@FKApp_SDB", SqlDbType.Int, 0).Value = DBNull.Value
                                .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 7).Value = DBNull.Value
                                .Parameters.Add("@MIN_TO_DATE", SqlDbType.Int, 0, "MIN_TO_DATE")
                                .Parameters.Add("@DISCOUNT", SqlDbType.Decimal, 0, "DISCOUNT")
                                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50, "CreatedBy")
                                .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                            End With
                            SqlDat.InsertCommand = CommandInsert
                            SqlDat.Update(InsertedRows) : CommandInsert.Parameters.Clear()
                        End If
                    ElseIf Not HasInsertedProg Then
                        If InsertedRows.Length > 0 Then
                            For Each rowProg As DataRow In InsertedRows
                                Dim BrandID As String = rowProg("BRAND_ID").ToString()
                                Query = "SET NOCOUNT ON;" & vbCrLf & _
                                        " SELECT TOP 1 IDApp FROM SALES_DKN_BRAND WHERE FKApp = @FKApp AND BRAND_ID = @BRAND_ID ;"
                                Me.ResetCommandText(CommandType.Text, Query)
                                AddParameter("@FKApp", SqlDbType.Int, IDAppSDS)
                                AddParameter("@BRAND_ID", SqlDbType.VarChar, BrandID, 7)
                                retval = SqlCom.ExecuteScalar() : ClearCommandParameters()
                                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                                          " INSERT INTO SALES_DKN_PROGRESSIVE(FKApp_SDB,FKApp_SDS,BRAND_ID,MIN_TO_DATE,DISCOUNT,CreatedDate,CreatedBy) " & vbCrLf & _
                                          " VALUES(@FKApp_SDB,@FKApp_SDS,@BRAND_ID,@MIN_TO_DATE,@DISCOUNT,@CreatedDate,@CreatedBy) ;"
                                    ResetCommandText(CommandType.Text, Query)
                                    With Me.SqlCom
                                        .Parameters.Add("@FKApp_SDS", SqlDbType.Int, 0).Value = IDAppSDS
                                        .Parameters.Add("@FKApp_SDB", SqlDbType.Int, 0).Value = Convert.ToInt32(retval)
                                        .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 7).Value = rowProg("BRAND_ID")
                                        .Parameters.Add("@MIN_TO_DATE", SqlDbType.Int, 0).Value = rowProg("MIN_TO_DATE")
                                        .Parameters.Add("@DISCOUNT", SqlDbType.Decimal, 0).Value = rowProg("DISCOUNT")
                                        .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = rowProg("CreatedBy")
                                        .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0).Value = rowProg("CreatedDate")
                                        .ExecuteScalar() : .Parameters.Clear()
                                    End With
                                End If
                            Next
                        End If
                    End If
                    'deleted brand modified brand tidak ada
                    DeletedRows = DS.Tables("T_BrandGrid").Select("", "", DataViewRowState.Deleted)
                    If DeletedRows.Length > 0 Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " DELETE FROM SALES_DKN_BRAND WHERE IDApp = @IDApp; " & vbCrLf & _
                                " DELETE FROM SALES_DKN_PROGRESSIVE WHERE FKApp_SDB = @IDApp AND FKApp_SDS = @FKApp_SDS;"
                        If IsNothing(CommandDelete) Then
                            CommandDelete = Me.SqlConn.CreateCommand()
                            CommandDelete.Transaction = Me.SqlTrans
                        End If
                        With CommandDelete
                            .CommandText = Query
                            .CommandType = CommandType.Text
                            .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp").SourceVersion = DataRowVersion.Original
                            .Parameters.Add("@FKApp_SDS", SqlDbType.Int, 0).Value = ODKN.IDApp
                        End With
                        SqlDat.DeleteCommand = CommandDelete
                        SqlDat.Update(DeletedRows) : CommandDelete.Parameters.Clear()
                        SqlDat.DeleteCommand = Nothing
                    End If
                Else
                    InsertedRows = DS.Tables("T_Progressive").Select("", "", DataViewRowState.Added)
                    If InsertedRows.Length > 0 Then
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                " IF NOT EXISTS(SELECT IDApp FROM SALES_DKN_PROGRESSIVE WHERE FKApp_SDS = @FKApp_SDS AND FKApp_SDB = @FKApp_SDB) " & vbCrLf & _
                                " BEGIN " & vbCrLf & _
                                " INSERT INTO SALES_DKN_PROGRESSIVE(FKApp_SDB,FKApp_SDS,BRAND_ID,MIN_TO_DATE,DISCOUNT,CreatedDate,CreatedBy) " & vbCrLf & _
                                " VALUES(@FKApp_SDB,@FKApp_SDS,@BRAND_ID,@MIN_TO_DATE,@DISCOUNT,@CreatedDate,@CreatedBy) ;" & vbCrLf & _
                                " END "
                        If IsNothing(CommandInsert) Then
                            CommandInsert = Me.SqlConn.CreateCommand()
                            CommandInsert.Transaction = Me.SqlTrans
                        End If
                        With CommandInsert
                            .CommandType = CommandType.Text
                            .CommandText = Query
                            .Parameters.Add("@FKApp_SDS", SqlDbType.Int, 0).Value = IDAppSDS
                            .Parameters.Add("@FKApp_SDB", SqlDbType.Int, 0).Value = DBNull.Value
                            .Parameters.Add("@BRAND_ID", SqlDbType.VarChar, 7).Value = DBNull.Value
                            .Parameters.Add("@MIN_TO_DATE", SqlDbType.Int, 0, "MIN_TO_DATE")
                            .Parameters.Add("@DISCOUNT", SqlDbType.Decimal, 0, "DISCOUNT")
                            .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50, "CreatedBy")
                            .Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime, 0, "CreatedDate")
                        End With
                        SqlDat.InsertCommand = CommandInsert
                        SqlDat.Update(InsertedRows) : CommandInsert.Parameters.Clear()
                    End If
                End If

                ''updatedRows Prog and DeletedRowsProg
                DeletedRows = DS.Tables("T_Progressive").Select("", "", DataViewRowState.Deleted)
                If DeletedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " IF EXISTS(SELECT IDApp FROM SALES_DKN_PROGRESSIVE WHERE IDApp = @IDApp)" & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " DELETE FROM SALES_DKN_PROGRESSIVE WHERE IDApp = @IDApp ;" & vbCrLf & _
                            " END "
                    If IsNothing(CommandDelete) Then
                        CommandDelete = SqlConn.CreateCommand()
                        CommandDelete.Transaction = Me.SqlTrans
                    End If
                    With CommandDelete
                        .CommandText = Query
                        .CommandType = CommandType.Text
                        .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                        .Parameters()("@IDApp").SourceVersion = DataRowVersion.Original
                    End With
                    SqlDat.DeleteCommand = CommandDelete
                    SqlDat.Update(DeletedRows) : CommandDelete.Parameters.Clear()
                End If
                UpdatedRows = DS.Tables("T_Progressive").Select("", "", DataViewRowState.ModifiedOriginal)
                If UpdatedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " UPDATE [Nufarm].[dbo].[SALES_DKN_PROGRESSIVE]" & vbCrLf & _
                            " SET MIN_TO_DATE = @MIN_TO_DATE,DISCOUNT = @DISCOUNT,ModifiedDate = @ModifiedDate,ModifiedBy = @ModifiedBy " & vbCrLf & _
                            " WHERE IDApp = @IDApp ;"
                    If IsNothing(CommandUpdate) Then
                        CommandUpdate = Me.SqlConn.CreateCommand()
                        CommandUpdate.Transaction = Me.SqlTrans
                    End If
                    With CommandUpdate
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@MIN_TO_DATE", SqlDbType.Int, 0, "MIN_TO_DATE")
                        .Parameters.Add("@DISCOUNT", SqlDbType.Decimal, 0, "DISCOUNT")
                        .Parameters.Add("@ModifiedDate", SqlDbType.DateTime, 0, "ModifiedDate")
                        .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 100, "ModifiedBy")
                        .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                        .Parameters()("@IDApp").SourceVersion = DataRowVersion.Original
                    End With
                    SqlDat.UpdateCommand = CommandUpdate
                    SqlDat.Update(UpdatedRows) : CommandUpdate.Parameters.Clear()
                End If
                Me.CommiteTransaction() : Me.ClearCommandParameters()
                If MustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.RollbackTransaction() : ClearCommandParameters()
                Me.CloseConnection() : Throw ex
            End Try
        End Sub
        ''' <summary>
        ''' check if DKN data has been used in OA
        ''' </summary>
        ''' <param name="Periode">dalam bentuk string tanggal/bulan/tahun-tanggal/bulan/tahun</param>
        ''' <param name="BrandID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function hasDataUsed(ByVal Periode As String, ByVal BrandID As String, ByVal mustCloseConnection As Boolean)

            Try
                If BrandID <> "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT TOP 1 OA_BRANDPACK_ID FROM MRKT_DISC_HISTORY WHERE SGT_FLAG = 'DN' AND SDS_PERIODE = @Periode " & vbCrLf & _
                            " AND OA_BRANDPACK_ID = ANY(SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = ANY(SELECT PO_BRANDPACK_ID FROM ORDR_PO_BRANDPACK " & vbCrLf & _
                            " WHERE BRANDPACK_ID = ANY(SELECT BRANDPACK_ID FROM BRND_BRANDPACK WHERE BRAND_ID = @BRAND_ID))); "
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT TOP 1 OA_BRANDPACK_ID FROM MRKT_DISC_HISTORY WHERE SGT_FLAG = 'DN' AND SDS_PERIODE = @Periode ;"
                End If
                CreateCommandSql("", Query)
                If BrandID <> "" Then
                    AddParameter("@BRAND_ID", SqlDbType.VarChar, BrandID, 14)
                End If
                AddParameter("@Periode", SqlDbType.VarChar, Periode, 40)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : ClearCommandParameters()
                If Not IsNothing(retval) Then
                    If retval.ToString() <> "" Then
                        If mustCloseConnection Then : CloseConnection() : End If
                        Return True
                    End If
                End If
                If mustCloseConnection Then : CloseConnection() : End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : ClearCommandParameters() : Throw ex
            End Try

        End Function
        Public Sub deleteDK(ByVal SDSIDApp As Integer, Optional ByVal SDBIDApp As Integer = 0)
            Try
                Query = " SET NOCOUNT ON;" & vbCrLf & _
                        " IF (@FKApp_SDB > 0) " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "   DELETE FROM SALES_DKN_PROGRESSIVE WHERE FKApp_SDB = @FKApp_SDB ;" & vbCrLf & _
                        "   DELETE FROM SALES_DKN_BRAND WHERE IDApp = @FKApp_SDB ;" & vbCrLf & _
                        "   IF NOT EXISTS(SELECT FKApp_SDS FROM SALES_DKN_PROGRESSIVE WHERE FKApp_SDS = @FKApp_SDS)" & vbCrLf & _
                        "      AND NOT EXISTS(SELECT FKApp FROM SALES_DKN_BRAND WHERE FKApp = @FKApp_SDS) " & vbCrLf & _
                        "   BEGIN " & vbCrLf & _
                        "       DELETE FROM SALES_DKN_SCHEMA WHERE IDApp = @FKApp_SDS ;" & vbCrLf & _
                        "   END " & vbCrLf & _
                        "   ELSE IF NOT EXISTS(SELECT BRAND_ID FROM SALES_DKN_PROGRESSIVE WHERE FKApp_SDS = @FKApp_SDS AND BRAND_ID IS NOT NULL) " & vbCrLf & _
                        "   BEGIN " & vbCrLf & _
                        "       IF NOT EXISTS(SELECT BRAND_ID FROM SALES_DKN_BRAND WHERE FKApp = @FKApp_SDS) " & vbCrLf & _
                        "       BEGIN " & vbCrLf & _
                        "           DELETE FROM SALES_DKN_SCHEMA WHERE IDApp = @FKApp_SDS ; " & vbCrLf & _
                        "           DELETE FROM SALES_DKN_PROGRESSIVE WHERE FKApp_SDS = @FKApp_SDS ;" & vbCrLf & _
                        "       END " & vbCrLf & _
                        "   END " & vbCrLf & _
                        " END " & vbCrLf & _
                        " ELSE " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "   DELETE FROM SALES_DKN_PROGRESSIVE WHERE FKApp_SDS = @FKApp_SDS ;" & vbCrLf & _
                        "   DELETE FROM SALES_DKN_SCHEMA WHERE IDApp = @FKApp_SDS ;" & vbCrLf & _
                        " END "
                CreateCommandSql("", Query)
                AddParameter("@FKApp_SDB", SqlDbType.Int, SDBIDApp)
                AddParameter("@FKApp_SDS", SqlDbType.Int, SDSIDApp)
                OpenConnection() : BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar() : ClearCommandParameters() : CommiteTransaction()
            Catch ex As Exception
                Me.RollbackTransaction() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function PopulateQuery(ByVal SearchBy As String, ByVal value As Object, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef RowCount As Integer, _
           ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes, ByVal IsChangedCriteriaSearch As Boolean, ByVal mustReloadQuery As Boolean) As DataView
            Try
                Dim ResolvedCriteria As String = common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " IF (@MustReloadQuery = 1) " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "   IF OBJECT_ID('TEMPDB..##T_SDS') IS NOT NULL" & vbCrLf & _
                        "       BEGIN DROP TABLE TEMPDB..##T_SDS ; END " & vbCrLf & _
                        " END " & vbCrLf & _
                        " IF NOT EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SDS' AND Type = 'U') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " SELECT IDENTITY(int, 1,1) AS RowNumber,SDS.IDApp AS FKApp_SDS,SDB1.FKApp_SDB, PERIODE = dbo.FormatTanggal(SDS.START_DATE) + ' - ' + dbo.FormatTanggal(SDS.END_DATE),SDS.START_DATE,SDS.END_DATE,CASE SDS.ProductToGive WHEN 'A' THEN 'All Products' ELSE 'Certain Products' END AS APPLY_FOR_PRODUCTS, " & vbCrLf & _
                        " CASE SDS.ProductRule WHEN 'A' THEN 'All Products' ELSE 'Certain Products' END AS APPLY_DISCOUNT_RULE, ISNULL(SDB1.BRAND_NAME,'ALL_BRAND') AS BRAND_NAME " & vbCrLf & _
                        " INTO ##T_SDS FROM SALES_DKN_SCHEMA SDS LEFT OUTER JOIN(" & vbCrLf & _
                        " SELECT SDB.FKApp,SDB.IDApp AS FKApp_SDB, BR.BRAND_NAME FROM SALES_DKN_BRAND SDB INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = SDB.BRAND_ID)SDB1  " & vbCrLf & _
                        " ON SDB1.FKApp = SDS.IDApp ;" & vbCrLf & _
                        " CREATE CLUSTERED INDEX IX_T_SDS ON TEMPDB..##T_SDS(RowNumber,START_DATE,END_DATE);" & vbCrLf & _
                        " END "
                '" END "
                '" WHERE IDApp < (SELECT TOP " & (PageSize * (PageIndex - 1)).ToString() & " FROM SALES_DKN_"
                If IsNothing(Me.SqlCom) Then
                    CreateCommandSql("", Query)
                Else : ResetCommandText(CommandType.Text, Query)
                End If
                AddParameter("@MustReloadQuery", SqlDbType.Bit, mustReloadQuery)
                OpenConnection() : SqlCom.ExecuteScalar() : ClearCommandParameters()
                Query = "SELECT TOP " & PageSize & " FKApp_SDS,FKApp_SDB,PERIODE, START_DATE,END_DATE,APPLY_FOR_PRODUCTS,APPLY_DISCOUNT_RULE,BRAND_NAME FROM ##T_SDS " & vbCrLf & _
                        " WHERE RowNumber < ALL(SELECT TOP " & (PageSize * (PageIndex - 1)).ToString() & " RowNumber FROM TEMPDB..##T_SDS  " & vbCrLf & _
                        " WHERE (" & SearchBy & " " & ResolvedCriteria & " ) ORDER BY RowNumber DESC) " & vbCrLf & _
                        " AND " & SearchBy & " " & ResolvedCriteria & " ORDER BY RowNumber DESC OPTION(KEEP PLAN); "
                ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dt As New DataTable("T_Data")
                setDataAdapter(SqlCom).Fill(dt) : ClearCommandParameters()

                Query = "SET NOCOUNT ON; \n" & vbCrLf & _
                        "SELECT COUNT(ROW_NUM) FROM(SELECT ROW_NUMBER() OVER(ORDER BY " & SearchBy & " ASC)AS ROW_NUM FROM  TEMPDB..##T_SDS  WHERE (" & SearchBy & " " & ResolvedCriteria & " ))Result "

                Return dt.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

#End Region
        Public Function getSalesHistoryReminder() As DataTable
            Try

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                       "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SALES_BY_TM' AND TYPE = 'U') " & vbCrLf & _
                       " BEGIN  DROP TABLE tempdb..##T_SALES_BY_TM ; END " & vbCrLf & _
                       "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SHIP_TO_TM' AND TYPE = 'U') " & vbCrLf & _
                       " BEGIN  DROP TABLE tempdb..##T_SHIP_TO_TM ; END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        " IF NOT EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SHIP_TO_TM' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "   SELECT ST.SHIP_TO_ID,TER.TERRITORY_ID,TER.TERRITORY_AREA + '  ' + ISNULL(ST.SHIP_TO_DESCRIPTIONS,'') AS TERRITORY_AREA ,TM.MANAGER AS TERRITORY_MANAGER INTO TEMPDB..##T_SHIP_TO_TM FROM TERRITORY TER INNER JOIN SHIP_TO ST ON ST.TERRITORY_ID = TER.TERRITORY_ID " & vbCrLf & _
                        "   INNER JOIN TERRITORY_MANAGER TM ON TM.TM_ID = ST.TM_ID WHERE ST.INACTIVE = 0 ;" & vbCrLf & _
                        " CREATE CLUSTERED INDEX IX_T_SHIP_TO_TM ON ##T_SHIP_TO_TM(SHIP_TO_ID,TERRITORY_ID) ; " & vbCrLf & _
                        " END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If

                'CREATE Temporary tables based

                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "SELECT PROGRAM_DESCRIPTIONS = CASE WHEN (MBI.ISCP = 1 AND MBI.ISSCPD = 0) THEN ('PROGRAM_NAME : CPD ' + DR.DISTRIBUTOR_NAME + ', PERIODE ' + CONVERT(VARCHAR(100),MBI.START_DATE,106) + ' - ' + CONVERT(VARCHAR(100),MBI.END_DATE,106)) " & vbCrLf & _
                            " WHEN (MBI.ISCP = 1 AND MBI.ISSCPD = 1) THEN ('PROGRAM_NAME : CPD(K) ' + DR.DISTRIBUTOR_NAME + ', PERIODE ' + CONVERT(VARCHAR(100),MBI.START_DATE,106) + ' - ' + CONVERT(VARCHAR(100),MBI.END_DATE,106)) " & vbCrLf & _
                            " WHEN (MBI.ISCPR = 1 ) THEN ('PROGRAM_NAME : CP(R/D) ' + DR.DISTRIBUTOR_NAME + ', PERIODE ' + CONVERT(VARCHAR(1000),MBI.START_DATE,106) + ' - ' + CONVERT(VARCHAR(1000),MBI.END_DATE,106)) " & vbCrLf & _
                            " WHEN (MBI.ISDK = 1) THEN ('PROGRAM_NAME : DK ' + DR.DISTRIBUTOR_NAME + ', PERIODE ' + CONVERT(VARCHAR(1000),MBI.START_DATE,106) + ' - ' + CONVERT(VARCHAR(1000),MBI.END_DATE,106)) " & vbCrLf & _
                            " WHEN (MBI.ISCPMRT = 1) THEN ('PROGRAM_NAME : CPR(M/T) ' + DR.DISTRIBUTOR_NAME + ', PERIODE ' + CONVERT(VARCHAR(1000),MBI.START_DATE,106) + ' - ' + CONVERT(VARCHAR(1000),MBI.END_DATE,106)) " & vbCrLf & _
                            " WHEN (MBI.ISPKPP = 1) THEN ('PROGRAM_NAME : PKPP ' + DR.DISTRIBUTOR_NAME + ', PERIODE ' + CONVERT(VARCHAR(1000),MBI.START_DATE,106) + ' - ' + CONVERT(VARCHAR(1000),MBI.END_DATE,106)) " & vbCrLf & _
                            " WHEN (MBI.ISHK = 1) THEN ('PROGRAM_NAME : HK ' + DR.DISTRIBUTOR_NAME + ', PERIODE ' + CONVERT(VARCHAR(1000),MBI.START_DATE,106) + ' - ' + CONVERT(VARCHAR(1000),MBI.END_DATE,106)) ELSE 'UNKNOWN' END, " & vbCrLf & _
                            " TERRITORY_MANAGER = CASE WHEN MBI.SHIP_TO_ID = '' THEN '' WHEN MBI.SHIP_TO_ID IS NULL THEN '' ELSE ISNULL(ST1.TERRITORY_MANAGER,'') END, " & vbCrLf & _
                            " TERRITORY_AREA = CASE WHEN MBI.SHIP_TO_ID = '' THEN ISNULL(ST.TERRITORY_AREA,'') WHEN MBI.SHIP_TO_ID IS NULL THEN ISNULL(ST.TERRITORY_AREA,'') ELSE ISNULL(ST1.TERRITORY_AREA,'') END," & vbCrLf & _
                            " MP.PROGRAM_ID,MP.PROGRAM_NAME,MBI.PROG_BRANDPACK_DIST_ID,MBI.START_DATE,MBI.END_DATE,MBI.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,BB.BRANDPACK_NAME," & vbCrLf & _
                            " GIVEN = CASE WHEN (MBI.ISCP = 1 AND MBI.ISSCPD = 0) THEN MBI.GIVEN_CP WHEN (MBI.ISCP = 1 AND MBI.ISSCPD = 1) THEN MBI.GIVEN_CP " & vbCrLf & _
                            " WHEN (MBI.ISCPR = 1 ) THEN MBI.GIVEN_CPR WHEN (MBI.ISDK = 1) THEN MBI.GIVEN_DK WHEN (MBI.ISCPMRT = 1) THEN MBI.GIVEN_CPMRT WHEN (MBI.ISPKPP = 1) THEN MBI.GIVEN_PKPP  ELSE 0 END," & vbCrLf & _
                            " TARGET =  CASE WHEN (MBI.ISCP = 1 AND MBI.ISSCPD = 0) THEN MBI.TARGET_CP WHEN (MBI.ISCP = 1 AND MBI.ISSCPD = 1) THEN MBI.TARGET_CP " & vbCrLf & _
                            " WHEN (MBI.ISCPR = 1 ) THEN MBI.TARGET_CPR WHEN (MBI.ISDK = 1) THEN MBI.TARGET_DK WHEN (MBI.ISCPMRT = 1) THEN MBI.TARGET_CPMRT WHEN (MBI.ISPKPP = 1) THEN MBI.TARGET_PKPP WHEN (MBI.ISHK = 1) THEN MBI.TARGET_HK ELSE 0 END," & vbCrLf & _
                            " ISNULL(MD.TOTAL_DISC,0)AS ACTUAL_DISC,ISNULL(MD.TOTAL_RELEASE,0) AS ACTUAL_RELEASE,ISNULL(MD.TOTAL_REMAIN,0) AS ACTUAL_REMAIN,MBI.DESCRIPTIONS,MBI.CREATE_BY,MBI.CREATE_DATE " & vbCrLf & _
                            " FROM MRKT_MARKETING_PROGRAM MP INNER JOIN MRKT_BRANDPACK MB ON MB.PROGRAM_ID = MP.PROGRAM_ID INNER JOIN MRKT_BRANDPACK_DISTRIBUTOR MBI ON MBI.PROG_BRANDPACK_ID = MB.PROG_BRANDPACK_ID " & vbCrLf & _
                            " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = MB.BRANDPACK_ID INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = MBI.DISTRIBUTOR_ID " & vbCrLf & _
                            " LEFT OUTER JOIN (" & vbCrLf & _
                            "                   SELECT MDH.PROG_BRANDPACK_DIST_ID,SUM(MDH.MRKT_DISC_QTY) AS TOTAL_DISC,(ISNULL(SUM(OOBD.DISC_QTY),0) + ISNULL(SUM(REM.DISC_QTY),0)) AS TOTAL_RELEASE," & vbCrLf & _
                            "                   SUM(MDH.MRKT_DISC_QTY) - (ISNULL(SUM(REM.DISC_QTY),0) + ISNULL(SUM(OOBD.DISC_QTY),0)) AS TOTAL_REMAIN FROM MRKT_DISC_HISTORY MDH " & vbCrLf & _
                            "                   INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD " & vbCrLf & _
                            "                   ON MDH.MRKT_DISC_HIST_ID = OOBD.MRKT_DISC_HIST_ID " & vbCrLf & _
                            "                   LEFT OUTER JOIN " & vbCrLf & _
                            "                   (SELECT OOR.MRKT_DISC_HIST_ID,ISNULL(SUM(OOBD.DISC_QTY),0)AS DISC_QTY FROM ORDR_OA_REMAINDING OOR " & vbCrLf & _
                            "                   INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD ON OOBD.OA_RM_ID = OOR.OA_RM_ID WHERE OOBD.GQSY_SGT_P_FLAG " & vbCrLf & _
                            "                   IN('KP', 'CP', 'CR', 'DK', 'MS', 'CS', 'CD', 'CT') GROUP BY OOR.MRKT_DISC_HIST_ID " & vbCrLf & _
                            "                    )REM " & vbCrLf & _
                            "                   ON REM.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID " & vbCrLf & _
                            "                   WHERE MDH.SGT_FLAG IN('CD','CT') AND OOBD.GQSY_SGT_P_FLAG  " & vbCrLf & _
                            "                   IN('KP', 'CP', 'CR', 'DK', 'MS', 'CS', 'CD', 'CT') AND OOBD.MRKT_DISC_HIST_ID IS NOT NULL GROUP BY MDH.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                            "                  )MD " & vbCrLf & _
                            " ON MD.PROG_BRANDPACK_DIST_ID = MBI.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                            " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST1 ON ST1.SHIP_TO_ID = MBI.SHIP_TO_ID " & vbCrLf & _
                            " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST ON ST.TERRITORY_ID = DR.TERRITORY_ID " & vbCrLf & _
                            " WHERE DATEDIFF(DAY,@GETDATE,MBI.END_DATE) <= 14 AND DATEDIFF(DAY,@GETDATE,MBI.END_DATE) > 0 AND (MBI.DR IS NULL OR MBI.DR = 0);"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Dim tblHeader As New DataTable("SALES_PROGRAM_HISTORY_REMINDER")
                Me.setDataAdapter(Me.SqlCom).Fill(tblHeader)
                ''DELETE Data row di header yang memiliki data double
                Dim RecCount As Integer = tblHeader.Rows.Count
                Dim listProg As New List(Of String)
                For i As Integer = 0 To RecCount - 1
                    If i >= RecCount Then
                        Exit For
                    End If
                    If Not listProg.Contains(tblHeader.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString()) Then
                        listProg.Add(tblHeader.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString())
                    Else
                        tblHeader.Rows.RemoveAt(i)
                        tblHeader.AcceptChanges()
                        RecCount -= 1
                        If i >= RecCount - 1 Then
                            If listProg.Contains(tblHeader.Rows(RecCount - 1)("PROG_BRANDPACK_DIST_ID").ToString()) Then
                                tblHeader.Rows.RemoveAt(RecCount - 1)
                                tblHeader.AcceptChanges()
                            End If
                            Exit For
                        End If
                        i -= 1
                    End If
                Next
                Me.ClearCommandParameters()
                Return tblHeader
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return Nothing
        End Function
        Public Function hasDataReminder() As Boolean
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT TOP 1 PROG_BRANDPACK_DIST_ID FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE DATEDIFF(DAY,GETDATE(),END_DATE) <= 14 AND DATEDIFF(DAY,GETDATE(),END_DATE) > 0 AND (DR IS NULL OR DR = 0)); "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    Me.CloseConnection() : Return (CInt(retval) > 0)
                End If : Me.CloseConnection()
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getSalesHistory(ByVal flag As String, ByVal StartDate As DateTime, Optional ByVal EndDate As Object = Nothing) As DataSet
            Try
                'bikin header dan detail

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                       "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SALES_BY_TM' AND TYPE = 'U') " & vbCrLf & _
                       " BEGIN  DROP TABLE tempdb..##T_SALES_BY_TM ; END " & vbCrLf & _
                       "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SALES_BY_TM' AND TYPE = 'U') " & vbCrLf & _
                       " BEGIN  DROP TABLE tempdb..##T_SHIP_TO_TM ; END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        " IF NOT EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SHIP_TO_TM' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "   SELECT ST.SHIP_TO_ID,TER.TERRITORY_ID,TER.TERRITORY_AREA + '  ' + ISNULL(ST.SHIP_TO_DESCRIPTIONS,'') AS TERRITORY_AREA ,TM.MANAGER AS TERRITORY_MANAGER INTO TEMPDB..##T_SHIP_TO_TM FROM TERRITORY TER INNER JOIN SHIP_TO ST ON ST.TERRITORY_ID = TER.TERRITORY_ID " & vbCrLf & _
                        "   INNER JOIN TERRITORY_MANAGER TM ON TM.TM_ID = ST.TM_ID WHERE ST.INACTIVE = 0 ;" & vbCrLf & _
                        " CREATE CLUSTERED INDEX IX_T_SHIP_TO_TM ON ##T_SHIP_TO_TM(SHIP_TO_ID,TERRITORY_ID) ; " & vbCrLf & _
                        " END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                Me.baseDataSet = New DataSet() : Me.baseDataSet.Clear()
                Select Case flag
                    Case "CPD"
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                  "SELECT TERRITORY_MANAGER = CASE WHEN MBI.SHIP_TO_ID = '' THEN '' WHEN MBI.SHIP_TO_ID IS NULL THEN '' ELSE ISNULL(ST1.TERRITORY_MANAGER,'') END, " & vbCrLf & _
                                  " TERRITORY_AREA = CASE WHEN MBI.SHIP_TO_ID = '' THEN ISNULL(ST.TERRITORY_AREA,'') WHEN MBI.SHIP_TO_ID IS NULL THEN ISNULL(ST.TERRITORY_AREA,'') ELSE ISNULL(ST1.TERRITORY_AREA,'') END," & vbCrLf & _
                                  " MP.PROGRAM_ID,MP.PROGRAM_NAME,MBI.PROG_BRANDPACK_DIST_ID,MBI.START_DATE,MBI.END_DATE,MBI.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,MB.BRANDPACK_ID,BB.BRANDPACK_NAME,MBI.ISSCPD AS CPDK,MBI.TARGET_CP,MBI.GIVEN_CP/100 AS GIVEN_CP," & vbCrLf & _
                                  " ISNULL(T_DISC.TOTAL_DISC,0)AS ACTUAL_DISC,(ISNULL(REM.TOTAL_RELEASE,0) + ISNULL(T_DISC.TOTAL_RELEASE,0)) AS ACTUAL_RELEASE," & vbCrLf & _
                                  " ISNULL(T_DISC.TOTAL_DISC,0) - (ISNULL(REM.TOTAL_RELEASE,0) + ISNULL(T_DISC.TOTAL_RELEASE,0)) AS ACTUAL_REMAIN,MBI.DESCRIPTIONS " & vbCrLf & _
                                  " FROM MRKT_MARKETING_PROGRAM MP INNER JOIN MRKT_BRANDPACK MB ON MB.PROGRAM_ID = MP.PROGRAM_ID INNER JOIN MRKT_BRANDPACK_DISTRIBUTOR MBI ON MBI.PROG_BRANDPACK_ID = MB.PROG_BRANDPACK_ID  " & vbCrLf & _
                                  " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = MB.BRANDPACK_ID INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = MBI.DISTRIBUTOR_ID  " & vbCrLf & _
                                  " LEFT OUTER JOIN ( " & vbCrLf & _
                                  "                   SELECT MDH.PROG_BRANDPACK_DIST_ID,ISNULL(SUM(MDH.MRKT_DISC_QTY),0) AS TOTAL_DISC, ISNULL(SUM(OOBD.DISC_QTY),0) AS TOTAL_RELEASE " & vbCrLf & _
                                  "                   FROM MRKT_DISC_HISTORY MDH  LEFT OUTER JOIN ORDR_OA_BRANDPACK_DISC OOBD  " & vbCrLf & _
                                  "                   ON MDH.MRKT_DISC_HIST_ID = OOBD.MRKT_DISC_HIST_ID " & vbCrLf & _
                                  "                   WHERE MDH.SGT_FLAG IN('CP','CS','TD','TS')  " & vbCrLf & _
                                  "                   GROUP BY MDH.PROG_BRANDPACK_DIST_ID  " & vbCrLf & _
                                  "                   )T_DISC ON T_DISC.PROG_BRANDPACK_DIST_ID = MBI.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                                  " LEFT OUTER JOIN ( " & vbCrLf & _
                                  "                   SELECT MDH.PROG_BRANDPACK_DIST_ID,ISNULL(SUM(OOBD.DISC_QTY),0)AS TOTAL_RELEASE FROM " & vbCrLf & _
                                  "                   MRKT_DISC_HISTORY MDH INNER JOIN ORDR_OA_REMAINDING OOR ON OOR.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID  " & vbCrLf & _
                                  "                   INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD ON OOBD.OA_RM_ID = OOR.OA_RM_ID  " & vbCrLf & _
                                  "                   WHERE OOBD.GQSY_SGT_P_FLAG IN('CP','CS','TD','TS') GROUP BY MDH.PROG_BRANDPACK_DIST_ID  " & vbCrLf & _
                                  "                   )REM ON REM.PROG_BRANDPACK_DIST_ID = MBI.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                                  "                     AND REM.PROG_BRANDPACK_DIST_ID = T_DISC.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                                  " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST1 ON ST1.SHIP_TO_ID = MBI.SHIP_TO_ID " & vbCrLf & _
                                  " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST ON ST.TERRITORY_ID = DR.TERRITORY_ID " & vbCrLf & _
                                  " WHERE MBI.ISCP = 1 AND MBI.START_DATE >= @START_DATE "
                    Case "CPRMT"
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                "SELECT TERRITORY_MANAGER = CASE WHEN MBI.SHIP_TO_ID = '' THEN '' WHEN MBI.SHIP_TO_ID IS NULL THEN '' ELSE ISNULL(ST1.TERRITORY_MANAGER,'') END, " & vbCrLf & _
                                " TERRITORY_AREA = CASE WHEN MBI.SHIP_TO_ID = '' THEN ISNULL(ST.TERRITORY_AREA,'') WHEN MBI.SHIP_TO_ID IS NULL THEN ISNULL(ST.TERRITORY_AREA,'') ELSE ISNULL(ST1.TERRITORY_AREA,'') END," & vbCrLf & _
                                " MP.PROGRAM_ID,MP.PROGRAM_NAME,MBI.PROG_BRANDPACK_DIST_ID,MBI.START_DATE,MBI.END_DATE,MBI.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,MB.BRANDPACK_ID,BB.BRANDPACK_NAME,MBI.ISCPMRT AS [CP(RMT)],MBI.TARGET_CPMRT AS [TARGET_CP(RMT)],MBI.GIVEN_CPMRT/100 AS [GIVEN_CP(RMT)], " & vbCrLf & _
                                " ISNULL(T_DISC.TOTAL_DISC,0)AS ACTUAL_DISC,(ISNULL(REM.TOTAL_RELEASE,0) + ISNULL(T_DISC.TOTAL_RELEASE,0)) AS ACTUAL_RELEASE," & vbCrLf & _
                                " ISNULL(T_DISC.TOTAL_DISC,0) - (ISNULL(REM.TOTAL_RELEASE,0) + ISNULL(T_DISC.TOTAL_RELEASE,0)) AS ACTUAL_REMAIN,MBI.DESCRIPTIONS " & vbCrLf & _
                                " FROM MRKT_MARKETING_PROGRAM MP INNER JOIN MRKT_BRANDPACK MB ON MB.PROGRAM_ID = MP.PROGRAM_ID INNER JOIN MRKT_BRANDPACK_DISTRIBUTOR MBI ON MBI.PROG_BRANDPACK_ID = MB.PROG_BRANDPACK_ID  " & vbCrLf & _
                                " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = MB.BRANDPACK_ID INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = MBI.DISTRIBUTOR_ID  " & vbCrLf & _
                                " LEFT OUTER JOIN ( " & vbCrLf & _
                                "                   SELECT MDH.PROG_BRANDPACK_DIST_ID,ISNULL(SUM(MDH.MRKT_DISC_QTY),0) AS TOTAL_DISC, ISNULL(SUM(OOBD.DISC_QTY),0) AS TOTAL_RELEASE " & vbCrLf & _
                                "                   FROM MRKT_DISC_HISTORY MDH  LEFT OUTER JOIN ORDR_OA_BRANDPACK_DISC OOBD  " & vbCrLf & _
                                "                   ON MDH.MRKT_DISC_HIST_ID = OOBD.MRKT_DISC_HIST_ID " & vbCrLf & _
                                "                   WHERE MDH.SGT_FLAG IN('CD','CT')  " & vbCrLf & _
                                "                   GROUP BY MDH.PROG_BRANDPACK_DIST_ID  " & vbCrLf & _
                                "                   )T_DISC ON T_DISC.PROG_BRANDPACK_DIST_ID = MBI.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                                " LEFT OUTER JOIN ( " & vbCrLf & _
                                "                   SELECT MDH.PROG_BRANDPACK_DIST_ID,ISNULL(SUM(OOBD.DISC_QTY),0)AS TOTAL_RELEASE FROM " & vbCrLf & _
                                "                   MRKT_DISC_HISTORY MDH INNER JOIN ORDR_OA_REMAINDING OOR ON OOR.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID  " & vbCrLf & _
                                "                   INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD ON OOBD.OA_RM_ID = OOR.OA_RM_ID  " & vbCrLf & _
                                "                   WHERE OOBD.GQSY_SGT_P_FLAG IN('CD','CT') GROUP BY MDH.PROG_BRANDPACK_DIST_ID  " & vbCrLf & _
                                "                   )REM ON REM.PROG_BRANDPACK_DIST_ID = MBI.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                                " AND REM.PROG_BRANDPACK_DIST_ID = T_DISC.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                                " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST1 ON ST1.SHIP_TO_ID = MBI.SHIP_TO_ID " & vbCrLf & _
                                " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST ON ST.TERRITORY_ID = DR.TERRITORY_ID " & vbCrLf & _
                                " WHERE MBI.ISCPMRT = 1 AND MBI.START_DATE >= @START_DATE "
 
                    Case "CPRD"
                    
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                              "SELECT TERRITORY_MANAGER = CASE WHEN MBI.SHIP_TO_ID = '' THEN '' WHEN MBI.SHIP_TO_ID IS NULL THEN '' ELSE ISNULL(ST1.TERRITORY_MANAGER,'') END, " & vbCrLf & _
                              " TERRITORY_AREA = CASE WHEN MBI.SHIP_TO_ID = '' THEN ISNULL(ST.TERRITORY_AREA,'') WHEN MBI.SHIP_TO_ID IS NULL THEN ISNULL(ST.TERRITORY_AREA,'') ELSE ISNULL(ST1.TERRITORY_AREA,'') END," & vbCrLf & _
                              " MP.PROGRAM_ID,MP.PROGRAM_NAME,MBI.PROG_BRANDPACK_DIST_ID,MBI.START_DATE,MBI.END_DATE,MBI.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,MB.BRANDPACK_ID,BB.BRANDPACK_NAME,MBI.TARGET_CPR AS TARGET_CPRD,MBI.GIVEN_CPR/100 AS GIVEN_CPRD," & vbCrLf & _
                              " ISNULL(T_DISC.TOTAL_DISC,0)AS ACTUAL_DISC,(ISNULL(REM.TOTAL_RELEASE,0) + ISNULL(T_DISC.TOTAL_RELEASE,0)) AS ACTUAL_RELEASE," & vbCrLf & _
                              " ISNULL(T_DISC.TOTAL_DISC,0) - (ISNULL(REM.TOTAL_RELEASE,0) + ISNULL(T_DISC.TOTAL_RELEASE,0)) AS ACTUAL_REMAIN,MBI.DESCRIPTIONS " & vbCrLf & _
                              " FROM MRKT_MARKETING_PROGRAM MP INNER JOIN MRKT_BRANDPACK MB ON MB.PROGRAM_ID = MP.PROGRAM_ID INNER JOIN MRKT_BRANDPACK_DISTRIBUTOR MBI ON MBI.PROG_BRANDPACK_ID = MB.PROG_BRANDPACK_ID  " & vbCrLf & _
                              " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = MB.BRANDPACK_ID INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = MBI.DISTRIBUTOR_ID  " & vbCrLf & _
                              " LEFT OUTER JOIN ( " & vbCrLf & _
                              "                   SELECT MDH.PROG_BRANDPACK_DIST_ID,ISNULL(SUM(MDH.MRKT_DISC_QTY),0) AS TOTAL_DISC, ISNULL(SUM(OOBD.DISC_QTY),0) AS TOTAL_RELEASE " & vbCrLf & _
                              "                   FROM MRKT_DISC_HISTORY MDH  LEFT OUTER JOIN ORDR_OA_BRANDPACK_DISC OOBD  " & vbCrLf & _
                              "                   ON MDH.MRKT_DISC_HIST_ID = OOBD.MRKT_DISC_HIST_ID " & vbCrLf & _
                              "                   WHERE MDH.SGT_FLAG = @FLAG   " & vbCrLf & _
                              "                   GROUP BY MDH.PROG_BRANDPACK_DIST_ID  " & vbCrLf & _
                              "                   )T_DISC ON T_DISC.PROG_BRANDPACK_DIST_ID = MBI.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                              " LEFT OUTER JOIN ( " & vbCrLf & _
                              "                   SELECT MDH.PROG_BRANDPACK_DIST_ID,ISNULL(SUM(OOBD.DISC_QTY),0)AS TOTAL_RELEASE FROM " & vbCrLf & _
                              "                   MRKT_DISC_HISTORY MDH INNER JOIN ORDR_OA_REMAINDING OOR ON OOR.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID  " & vbCrLf & _
                              "                   INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD ON OOBD.OA_RM_ID = OOR.OA_RM_ID  " & vbCrLf & _
                              "                   WHERE OOBD.GQSY_SGT_P_FLAG = @FLAG GROUP BY MDH.PROG_BRANDPACK_DIST_ID  " & vbCrLf & _
                              "                   )REM ON REM.PROG_BRANDPACK_DIST_ID = MBI.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                              " AND REM.PROG_BRANDPACK_DIST_ID = T_DISC.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                              " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST1 ON ST1.SHIP_TO_ID = MBI.SHIP_TO_ID " & vbCrLf & _
                              " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST ON ST.TERRITORY_ID = DR.TERRITORY_ID " & vbCrLf & _
                              " WHERE MBI.ISCPR = 1 AND MBI.START_DATE >= @START_DATE "
                    Case "PKPP"
                   
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                             "SELECT TERRITORY_MANAGER = CASE WHEN MBI.SHIP_TO_ID = '' THEN '' WHEN MBI.SHIP_TO_ID IS NULL THEN '' ELSE ISNULL(ST1.TERRITORY_MANAGER,'') END, " & vbCrLf & _
                             " TERRITORY_AREA = CASE WHEN MBI.SHIP_TO_ID = '' THEN ISNULL(ST.TERRITORY_AREA,'') WHEN MBI.SHIP_TO_ID IS NULL THEN ISNULL(ST.TERRITORY_AREA,'') ELSE ISNULL(ST1.TERRITORY_AREA,'') END," & vbCrLf & _
                             " MP.PROGRAM_ID,MP.PROGRAM_NAME,MBI.PROG_BRANDPACK_DIST_ID,MBI.START_DATE,MBI.END_DATE,MBI.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,MB.BRANDPACK_ID,BB.BRANDPACK_NAME,MBI.TARGET_PKPP,MBI.GIVEN_PKPP/100 AS GIVEN_PKPP," & vbCrLf & _
                             " ISNULL(T_DISC.TOTAL_DISC,0)AS ACTUAL_DISC,(ISNULL(REM.TOTAL_RELEASE,0) + ISNULL(T_DISC.TOTAL_RELEASE,0)) AS ACTUAL_RELEASE," & vbCrLf & _
                             " ISNULL(T_DISC.TOTAL_DISC,0) - (ISNULL(REM.TOTAL_RELEASE,0) + ISNULL(T_DISC.TOTAL_RELEASE,0)) AS ACTUAL_REMAIN,MBI.DESCRIPTIONS " & vbCrLf & _
                             " FROM MRKT_MARKETING_PROGRAM MP INNER JOIN MRKT_BRANDPACK MB ON MB.PROGRAM_ID = MP.PROGRAM_ID INNER JOIN MRKT_BRANDPACK_DISTRIBUTOR MBI ON MBI.PROG_BRANDPACK_ID = MB.PROG_BRANDPACK_ID  " & vbCrLf & _
                             " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = MB.BRANDPACK_ID INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = MBI.DISTRIBUTOR_ID  " & vbCrLf & _
                             " LEFT OUTER JOIN ( " & vbCrLf & _
                             "                   SELECT MDH.PROG_BRANDPACK_DIST_ID,ISNULL(SUM(MDH.MRKT_DISC_QTY),0) AS TOTAL_DISC, ISNULL(SUM(OOBD.DISC_QTY),0) AS TOTAL_RELEASE " & vbCrLf & _
                             "                   FROM MRKT_DISC_HISTORY MDH  LEFT OUTER JOIN ORDR_OA_BRANDPACK_DISC OOBD  " & vbCrLf & _
                             "                   ON MDH.MRKT_DISC_HIST_ID = OOBD.MRKT_DISC_HIST_ID " & vbCrLf & _
                             "                   WHERE MDH.SGT_FLAG = @FLAG   " & vbCrLf & _
                             "                   GROUP BY MDH.PROG_BRANDPACK_DIST_ID  " & vbCrLf & _
                             "                   )T_DISC ON T_DISC.PROG_BRANDPACK_DIST_ID = MBI.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                             " LEFT OUTER JOIN ( " & vbCrLf & _
                             "                   SELECT MDH.PROG_BRANDPACK_DIST_ID,ISNULL(SUM(OOBD.DISC_QTY),0)AS TOTAL_RELEASE FROM " & vbCrLf & _
                             "                   MRKT_DISC_HISTORY MDH INNER JOIN ORDR_OA_REMAINDING OOR ON OOR.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID  " & vbCrLf & _
                             "                   INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD ON OOBD.OA_RM_ID = OOR.OA_RM_ID  " & vbCrLf & _
                             "                   WHERE OOBD.GQSY_SGT_P_FLAG = @FLAG GROUP BY MDH.PROG_BRANDPACK_DIST_ID  " & vbCrLf & _
                             "                   )REM ON REM.PROG_BRANDPACK_DIST_ID = MBI.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                             " AND REM.PROG_BRANDPACK_DIST_ID = T_DISC.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                             " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST1 ON ST1.SHIP_TO_ID = MBI.SHIP_TO_ID " & vbCrLf & _
                             " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST ON ST.TERRITORY_ID = DR.TERRITORY_ID " & vbCrLf & _
                             " WHERE MBI.ISPKPP = 1 AND MBI.START_DATE >= @START_DATE "

                    Case "DK"
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "SELECT TERRITORY_MANAGER = CASE WHEN MBI.SHIP_TO_ID = '' THEN '' WHEN MBI.SHIP_TO_ID IS NULL THEN '' ELSE ISNULL(ST1.TERRITORY_MANAGER,'') END, " & vbCrLf & _
                            " TERRITORY_AREA = CASE WHEN MBI.SHIP_TO_ID = '' THEN ISNULL(ST.TERRITORY_AREA,'') WHEN MBI.SHIP_TO_ID IS NULL THEN ISNULL(ST.TERRITORY_AREA,'') ELSE ISNULL(ST1.TERRITORY_AREA,'') END," & vbCrLf & _
                            " MP.PROGRAM_ID,MP.PROGRAM_NAME,MBI.PROG_BRANDPACK_DIST_ID,MBI.START_DATE,MBI.END_DATE,MBI.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,MB.BRANDPACK_ID,BB.BRANDPACK_NAME,MBI.TARGET_DK,MBI.GIVEN_DK/100 AS GIVEN_DK," & vbCrLf & _
                            " ISNULL(T_DISC.TOTAL_DISC,0)AS ACTUAL_DISC,(ISNULL(REM.TOTAL_RELEASE,0) + ISNULL(T_DISC.TOTAL_RELEASE,0)) AS ACTUAL_RELEASE," & vbCrLf & _
                            " ISNULL(T_DISC.TOTAL_DISC,0) - (ISNULL(REM.TOTAL_RELEASE,0) + ISNULL(T_DISC.TOTAL_RELEASE,0)) AS ACTUAL_REMAIN,MBI.DESCRIPTIONS " & vbCrLf & _
                            " FROM MRKT_MARKETING_PROGRAM MP INNER JOIN MRKT_BRANDPACK MB ON MB.PROGRAM_ID = MP.PROGRAM_ID INNER JOIN MRKT_BRANDPACK_DISTRIBUTOR MBI ON MBI.PROG_BRANDPACK_ID = MB.PROG_BRANDPACK_ID  " & vbCrLf & _
                            " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = MB.BRANDPACK_ID INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = MBI.DISTRIBUTOR_ID  " & vbCrLf & _
                            " LEFT OUTER JOIN ( " & vbCrLf & _
                            "                   SELECT MDH.PROG_BRANDPACK_DIST_ID,ISNULL(SUM(MDH.MRKT_DISC_QTY),0) AS TOTAL_DISC, ISNULL(SUM(OOBD.DISC_QTY),0) AS TOTAL_RELEASE " & vbCrLf & _
                            "                   FROM MRKT_DISC_HISTORY MDH  LEFT OUTER JOIN ORDR_OA_BRANDPACK_DISC OOBD  " & vbCrLf & _
                            "                   ON MDH.MRKT_DISC_HIST_ID = OOBD.MRKT_DISC_HIST_ID " & vbCrLf & _
                            "                   WHERE MDH.SGT_FLAG = @FLAG   " & vbCrLf & _
                            "                   GROUP BY MDH.PROG_BRANDPACK_DIST_ID  " & vbCrLf & _
                            "                   )T_DISC ON T_DISC.PROG_BRANDPACK_DIST_ID = MBI.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                            " LEFT OUTER JOIN ( " & vbCrLf & _
                            "                   SELECT MDH.PROG_BRANDPACK_DIST_ID,ISNULL(SUM(OOBD.DISC_QTY),0)AS TOTAL_RELEASE FROM " & vbCrLf & _
                            "                   MRKT_DISC_HISTORY MDH INNER JOIN ORDR_OA_REMAINDING OOR ON OOR.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID  " & vbCrLf & _
                            "                   INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD ON OOBD.OA_RM_ID = OOR.OA_RM_ID  " & vbCrLf & _
                            "                   WHERE OOBD.GQSY_SGT_P_FLAG = @FLAG GROUP BY MDH.PROG_BRANDPACK_DIST_ID  " & vbCrLf & _
                            "                   )REM ON REM.PROG_BRANDPACK_DIST_ID = MBI.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                            " AND REM.PROG_BRANDPACK_DIST_ID = T_DISC.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                            " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST1 ON ST1.SHIP_TO_ID = MBI.SHIP_TO_ID " & vbCrLf & _
                            " LEFT OUTER JOIN TEMPDB..##T_SHIP_TO_TM ST ON ST.TERRITORY_ID = DR.TERRITORY_ID " & vbCrLf & _
                            " WHERE MBI.ISDK = 1 AND MBI.START_DATE >= @START_DATE "
                    Case "SLSTM"
                        Query = " IF NOT EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SALES_BY_TM' AND TYPE = 'U') " & vbCrLf & _
                                " BEGIN " & vbCrLf & _
                                " SELECT TERR.TERRITORY_AREA + ' ' + ISNULL(ST.SHIP_TO_DESCRIPTIONS,'') AS TERRITORY_AREA,TM.MANAGER AS TERRITORY_MANAGER,DR.DISTRIBUTOR_NAME,PO.PO_REF_NO,OPB.BRANDPACK_ID,BB.BRANDPACK_NAME,OOA.OA_ORIGINAL_QTY AS PO_QTY,OOA.OA_PRICE_PERQTY AS PRICE,OOA.OA_ORIGINAL_QTY * OOA.OA_PRICE_PERQTY AS [TOTAL] " & vbCrLf & _
                                " INTO ##T_SALES_BY_TM FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                                 " INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID " & vbCrLf & _
                                 "INNER JOIN ORDR_OA_BRANDPACK OOA ON OOA.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                                " INNER JOIN ORDR_ORDER_ACCEPTANCE OOR ON OOR.OA_ID = OOA.OA_ID AND PO.PO_REF_NO = OOR.PO_REF_NO INNER JOIN OA_SHIP_TO OS ON OS.OA_ID = OOR.OA_ID INNER JOIN SHIP_TO ST ON ST.SHIP_TO_ID = OS.SHIP_TO_ID INNER JOIN TERRITORY_MANAGER TM ON TM.TM_ID = ST.TM_ID " & vbCrLf & _
                                " INNER JOIN TERRITORY TERR ON TERR.TERRITORY_ID = ST.TERRITORY_ID " & vbCrLf & _
                                " INNER JOIN BRND_BRANDPACK BB ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID WHERE PO.PO_REF_DATE >= @START_DATE  ;" & vbCrLf
                        If Not IsNothing(EndDate) Then
                            Query = Query.Replace(";", " ")
                            Query &= " AND PO.PO_REF_DATE <= @END_DATE ; " & vbCrLf
                        End If
                        Query &= " END "
                End Select
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                If Not IsNothing(EndDate) Then
                    Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(EndDate))
                End If
                Dim SFlag As String = ""
                If flag = "SLSTM" Then
                    'AND PO.PO_REF_DATE <= @END_DATE

                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "SELECT RTRIM(TERRITORY_AREA) AS TERRITORY_AREA,TERRITORY_MANAGER,BRANDPACK_ID,BRANDPACK_NAME,SUM(PO_QTY)AS TOTAL_PO,SUM(TOTAL) AS TOTAL_SALES_VALUE FROM tempdb..##T_SALES_BY_TM GROUP BY RTRIM(TERRITORY_AREA),TERRITORY_MANAGER,BRANDPACK_ID,BRANDPACK_NAME "
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Else
                    If Not IsNothing(EndDate) Then
                        Query = Query.Replace(";", " ")
                        Query &= " AND MBI.END_DATE <= @END_DATE ;"
                    End If
                    If flag = "DK" Then
                        SFlag = "DK"
                    ElseIf flag = "CPRD" Then
                        SFlag = "CR"
                    ElseIf flag = "PKPP" Then
                        SFlag = "KP"
                    End If
                    Me.AddParameter("@FLAG", SqlDbType.VarChar, SFlag, 2)
                End If
                'Me.OpenConnection()
                Dim tblHeader As New DataTable("SUMMARY_OF_PROGRAM")
                tblHeader.Clear()
                setDataAdapter(Me.SqlCom).Fill(tblHeader)
                ''bikin detailnya
                If flag = "DK" Or flag = "PKPP" Or flag = "CPRD" Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                      "SELECT DR.DISTRIBUTOR_NAME,PO.PO_REF_NO,PO.PO_REF_DATE,OPB.BRANDPACK_ID,BB.BRANDPACK_NAME,OOB.OA_ORIGINAL_QTY AS PO_ORIGINAL_QTY,SD.DISC_QTY,SD.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                      "FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                      " INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID " & vbCrLf & _
                      " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = OPB.BRANDPACK_ID " & vbCrLf & _
                      " INNER JOIN ORDR_OA_BRANDPACK OOB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID INNER JOIN " & vbCrLf & _
                      "(" & vbCrLf & _
                      " SELECT MDH.PROG_BRANDPACK_DIST_ID,MDH.OA_BRANDPACK_ID,ISNULL(SUM(OOB.DISC_QTY),0) + ISNULL(SUM(REM.DISC_QTY),0) AS DISC_QTY " & vbCrLf & _
                      " FROM MRKT_DISC_HISTORY MDH LEFT OUTER JOIN " & vbCrLf & _
                      "  (" & vbCrLf & _
                      "   SELECT MRKT_DISC_HIST_ID,DISC_QTY FROM ORDR_OA_BRANDPACK_DISC  " & vbCrLf & _
                      "   WHERE GQSY_SGT_P_FLAG = @FLAG" & vbCrLf & _
                      "   )OOB ON OOB.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID " & vbCrLf & _
                      " LEFT OUTER JOIN ( " & vbCrLf & _
                      "                  SELECT OOR.MRKT_DISC_HIST_ID,OOBD.OA_BRANDPACK_ID,ISNULL(OOBD.DISC_QTY,0)AS DISC_QTY FROM ORDR_OA_REMAINDING OOR " & vbCrLf & _
                      "                  INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD ON OOR.OA_RM_ID = OOBD.OA_RM_ID WHERE OOBD.GQSY_SGT_P_FLAG = @FLAG" & vbCrLf & _
                      "                  )REM " & vbCrLf & _
                      "                 ON (REM.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID) " & vbCrLf & _
                      " WHERE MDH.SGT_FLAG = @FLAG GROUP BY MDH.PROG_BRANDPACK_DIST_ID, MDH.OA_BRANDPACK_ID " & vbCrLf & _
                      ")SD " & vbCrLf & _
                      " ON SD.OA_BRANDPACK_ID = OOB.OA_BRANDPACK_ID  WHERE PO.PO_REF_DATE >= @START_DATE ; "

                    Me.AddParameter("@FLAG", SqlDbType.VarChar, SFlag, 2)
                ElseIf flag = "CPD" Then

                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                           "SELECT DR.DISTRIBUTOR_NAME,PO.PO_REF_NO,PO.PO_REF_DATE,OPB.BRANDPACK_ID,BB.BRANDPACK_NAME,OOB.OA_ORIGINAL_QTY AS PO_ORIGINAL_QTY,SD.DISC_QTY,SD.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                           "FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                           " INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID " & vbCrLf & _
                           " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = OPB.BRANDPACK_ID " & vbCrLf & _
                           " INNER JOIN ORDR_OA_BRANDPACK OOB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID INNER JOIN " & vbCrLf & _
                           "(" & vbCrLf & _
                           " SELECT MDH.PROG_BRANDPACK_DIST_ID,MDH.OA_BRANDPACK_ID,ISNULL(SUM(OOB.DISC_QTY),0) + ISNULL(SUM(REM.DISC_QTY),0) AS DISC_QTY " & vbCrLf & _
                           " FROM MRKT_DISC_HISTORY MDH LEFT OUTER JOIN " & vbCrLf & _
                           "  (" & vbCrLf & _
                           "   SELECT MRKT_DISC_HIST_ID,DISC_QTY FROM ORDR_OA_BRANDPACK_DISC  " & vbCrLf & _
                           "   WHERE GQSY_SGT_P_FLAG IN('CP','CS','TS','TD')" & vbCrLf & _
                           "   )OOB ON OOB.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID " & vbCrLf & _
                           " LEFT OUTER JOIN ( " & vbCrLf & _
                           "                  SELECT OOR.MRKT_DISC_HIST_ID,OOBD.OA_BRANDPACK_ID,ISNULL(OOBD.DISC_QTY,0)AS DISC_QTY FROM ORDR_OA_REMAINDING OOR " & vbCrLf & _
                           "                  INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD ON OOR.OA_RM_ID = OOBD.OA_RM_ID WHERE OOBD.GQSY_SGT_P_FLAG IN('CP','CS','TS','TD')" & vbCrLf & _
                           "                  )REM " & vbCrLf & _
                           "                 ON (REM.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID) " & vbCrLf & _
                           " WHERE MDH.SGT_FLAG IN('CP','CS','TS','TD') GROUP BY MDH.PROG_BRANDPACK_DIST_ID, MDH.OA_BRANDPACK_ID " & vbCrLf & _
                           ")SD " & vbCrLf & _
                           " ON SD.OA_BRANDPACK_ID = OOB.OA_BRANDPACK_ID  WHERE PO.PO_REF_DATE >= @START_DATE ; "
                ElseIf flag = "CPRMT" Then

                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                              "SELECT DR.DISTRIBUTOR_NAME,PO.PO_REF_NO,PO.PO_REF_DATE,OPB.BRANDPACK_ID,BB.BRANDPACK_NAME,OOB.OA_ORIGINAL_QTY AS PO_ORIGINAL_QTY,SD.DISC_QTY,SD.PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                              "FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                              " INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID " & vbCrLf & _
                              " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = OPB.BRANDPACK_ID " & vbCrLf & _
                              " INNER JOIN ORDR_OA_BRANDPACK OOB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID INNER JOIN " & vbCrLf & _
                              "(" & vbCrLf & _
                              " SELECT MDH.PROG_BRANDPACK_DIST_ID,MDH.OA_BRANDPACK_ID,ISNULL(SUM(OOB.DISC_QTY),0) + ISNULL(SUM(REM.DISC_QTY),0) AS DISC_QTY " & vbCrLf & _
                              " FROM MRKT_DISC_HISTORY MDH LEFT OUTER JOIN " & vbCrLf & _
                              "  (" & vbCrLf & _
                              "   SELECT MRKT_DISC_HIST_ID,DISC_QTY FROM ORDR_OA_BRANDPACK_DISC  " & vbCrLf & _
                              "   WHERE GQSY_SGT_P_FLAG IN('CD','CT')" & vbCrLf & _
                              "   )OOB ON OOB.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID " & vbCrLf & _
                              " LEFT OUTER JOIN ( " & vbCrLf & _
                              "                  SELECT OOR.MRKT_DISC_HIST_ID,OOBD.OA_BRANDPACK_ID,ISNULL(OOBD.DISC_QTY,0)AS DISC_QTY FROM ORDR_OA_REMAINDING OOR " & vbCrLf & _
                              "                  INNER JOIN ORDR_OA_BRANDPACK_DISC OOBD ON OOR.OA_RM_ID = OOBD.OA_RM_ID WHERE OOBD.GQSY_SGT_P_FLAG IN('CD','CT')" & vbCrLf & _
                              "                  )REM " & vbCrLf & _
                              "                 ON (REM.MRKT_DISC_HIST_ID = MDH.MRKT_DISC_HIST_ID) " & vbCrLf & _
                              " WHERE MDH.SGT_FLAG IN('CD','CT') GROUP BY MDH.PROG_BRANDPACK_DIST_ID, MDH.OA_BRANDPACK_ID " & vbCrLf & _
                              ")SD " & vbCrLf & _
                              " ON SD.OA_BRANDPACK_ID = OOB.OA_BRANDPACK_ID  WHERE PO.PO_REF_DATE >= @START_DATE ; "

                ElseIf flag = "SLSTM" Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "SELECT * FROM tempdb..##T_SALES_BY_TM ;"
                End If
                If flag <> "SLSTM" Then
                    If Not IsNothing(EndDate) Then
                        Query = Query.Replace(";", " ")
                        Query &= " AND PO.PO_REF_DATE <= @END_DATE ;"
                    End If
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                    If Not IsNothing(EndDate) Then
                        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(EndDate))
                    End If
                Else
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                End If
                Dim tblDetail As New DataTable("DETAIL_PO_BY_SALES")
                Me.setDataAdapter(Me.SqlCom).Fill(tblDetail) : Me.ClearCommandParameters()
                'CLEAR temporary table 
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                       "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SALES_BY_TM' AND TYPE = 'U') " & vbCrLf & _
                       " BEGIN  DROP TABLE tempdb..##T_SALES_BY_TM ; END " & vbCrLf & _
                       "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SALES_BY_TM' AND TYPE = 'U') " & vbCrLf & _
                       " BEGIN  DROP TABLE tempdb..##T_SHIP_TO_TM ; END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters() : Me.CloseConnection()

                If flag <> "SLSTM" Then
                    ''DELETE Data row di header yang memiliki data double
                    Dim RecCount As Integer = tblHeader.Rows.Count
                    Dim listProg As New List(Of String)
                    For i As Integer = 0 To RecCount - 1
                        If i >= RecCount Then
                            Exit For
                        End If
                        If Not listProg.Contains(tblHeader.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString()) Then
                            listProg.Add(tblHeader.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString())
                        Else
                            tblHeader.Rows.RemoveAt(i)
                            tblHeader.AcceptChanges()
                            RecCount -= 1
                            If i >= RecCount - 1 Then
                                If listProg.Contains(tblHeader.Rows(RecCount - 1)("PROG_BRANDPACK_DIST_ID").ToString()) Then
                                    tblHeader.Rows.RemoveAt(RecCount - 1)
                                    tblHeader.AcceptChanges()
                                End If
                                Exit For
                            End If
                            i -= 1
                        End If
                    Next
                    tblHeader.AcceptChanges()
                End If
                Me.baseDataSet.Tables.AddRange(New DataTable() {tblHeader, tblDetail})
                Return Me.baseDataSet
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function CreateViewDistributor(ByVal PROG_BRANDPACK_ID As String, ByVal HasInlcudedMRKT As Boolean, ByVal mustCloseConnection As Boolean) As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "DECLARE @V_START_DATE SMALLDATETIME,@V_END_DATE SMALLDATETIME;" & vbCrLf & _
                        " SET @V_START_DATE = (SELECT START_DATE FROM MRKT_BRANDPACK WHERE PROG_BRANDPACK_ID = @ProgBrandPackID);" & vbCrLf & _
                        " SET @V_END_DATE = (SELECT END_DATE FROM MRKT_BRANDPACK WHERE PROG_BRANDPACK_ID = @ProgBrandPackID); " & vbCrLf & _
                        " SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR " & vbCrLf
                If Not HasInlcudedMRKT Then
                    Query &= " WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                             " ON ABI.AGREEMENT_NO = DA.AGREEMENT_NO INNER JOIN AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
                             " AND ABI.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN MRKT_BRANDPACK MB ON MB.BRANDPACK_ID = ABI.BRANDPACK_ID " & vbCrLf & _
                             " WHERE AA.START_DATE <= @V_START_DATE AND AA.END_DATE >= @V_END_DATE AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID AND MB.PROG_BRANDPACK_ID = @ProgBrandPackID) " & vbCrLf & _
                             " AND NOT EXISTS(SELECT DISTRIBUTOR_ID FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID " & vbCrLf & _
                             " AND PROG_BRANDPACK_ID = @ProgBrandPackID);"
                Else
                    Query &= " WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID " & vbCrLf & _
                             " AND PROG_BRANDPACK_ID = @ProgBrandPackID); "
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@ProgBrandPackID", SqlDbType.VarChar, PROG_BRANDPACK_ID, 30)
                Dim tblDistributor As New DataTable("DISTRIBUTOR") : tblDistributor.Clear()
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                Me.OpenConnection() : Me.SqlDat.Fill(tblDistributor) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                'Me.FillDataTable(tblDistributor)
                Me.m_ViewDistributor = tblDistributor.DefaultView()
                Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
            Catch ex As Exception
                Me.CloseConnection() : Throw ex
            End Try
            Return Me.m_ViewDistributor
        End Function
        Public Function SearchProgram(ByVal SearchString As String) As DataView
            Try
                Query = " SET COUNT ON;" & vbCrLf & _
                "SELECT TOP 100 PROGRAM_ID,PROGRAM_NAME FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID LIKE '%" & SearchString & "%' ;"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.m_ViewProgram = Me.FillDataTable(New DataTable("T_Program")).DefaultView
                Return Me.m_ViewProgram
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function CreateViewProgram(ByVal SaveTYpe As common.Helper.SaveMode, ByVal mustCloseConnection As Boolean) As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf
                If SaveTYpe = common.Helper.SaveMode.Update Then
                    Query &= " SELECT TOP 1000 PROGRAM_ID,PROGRAM_NAME FROM MRKT_MARKETING_PROGRAM ORDER BY END_DATE DESC;"
                Else
                    Query &= " SELECT TOP 500 PROGRAM_ID,PROGRAM_NAME FROM MRKT_MARKETING_PROGRAM ORDER BY END_DATE DESC;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.CreateCommandSql(CommandType.StoredProcedure, "sp_executesql", ConnectionTo.Nufarm)
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.CreateCommandSql("", "SELECT PROGRAM_ID,PROGRAM_NAME FROM MRKT_MARKETING_PROGRAM WHERE END_DATE >= " & NufarmBussinesRules.SharedClass.ShortGetDate())
                Dim tblProgram As New DataTable("T_Program")
                tblProgram.Clear()
                If IsNothing(Me.SqlDat) Then
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                Me.OpenConnection() : Me.SqlDat.Fill(tblProgram) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                'Me.FillDataTable(tblProgram)
                Me.m_ViewProgram = tblProgram.DefaultView()
                Me.m_ViewProgram.Sort = "PROGRAM_ID"
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return Me.m_ViewProgram
        End Function
        Public Function CreateViewBrandPack(ByVal PROGRAM_ID As String, ByVal HasInlcudedMRKT As Boolean, ByVal mustCloseConnection As Boolean) As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "DECLARE @V_START_DATE SMALLDATETIME,@V_END_DATE SMALLDATETIME;" & vbCrLf & _
                        " SET @V_START_DATE = (SELECT START_DATE FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID = @ProgramID);" & vbCrLf & _
                        " SET @V_END_DATE = (SELECT END_DATE FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID = @ProgramID);" & vbCrLf & _
                        "SELECT BRANDPACK_ID,BRANDPACK_NAME FROM BRND_BRANDPACK BB " & vbCrLf & _
                        " WHERE EXISTS(SELECT BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE ABI INNER JOIN AGREE_AGREEMENT AA " & vbCrLf & _
                        "               ON ABI.AGREEMENT_NO = AA.AGREEMENT_NO WHERE AA.START_DATE <= @V_START_DATE " & vbCrLf & _
                        "               AND AA.END_DATE >= @V_END_DATE " & vbCrLf & _
                        "               AND ABI.BRANDPACK_ID = BB.BRANDPACK_ID " & vbCrLf & _
                        "              ) " & vbCrLf
                If HasInlcudedMRKT Then
                    Query &= " AND EXISTS(SELECT BRANDPACK_ID FROM MRKT_BRANDPACK WHERE PROGRAM_ID = @ProgramID AND BRANDPACK_ID = BB.BRANDPACK_ID);"
                Else
                    'Query &= " AND BB.IsActive = 1 AND (BB.IsObsolete = 0 OR BB.IsObsolete IS NULL) " & vbCrLf
                    Query &= " AND NOT EXISTS(SELECT BRANDPACK_ID FROM MRKT_BRANDPACK WHERE PROGRAM_ID = @ProgramID AND BRANDPACK_ID = BB.BRANDPACK_ID);"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@ProgramID", SqlDbType.VarChar, PROGRAM_ID, 15)
                Dim tblBrandPack As New DataTable("T_BrandPack") : tblBrandPack.Clear()
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                Me.OpenConnection() : Me.SqlDat.Fill(tblBrandPack) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                'Me.FillDataTable(tblBrandPack)
                Me.m_ViewBrandPack = tblBrandPack.DefaultView()
                Me.m_ViewBrandPack.Sort = "BRANDPACK_ID"
                Me.m_ViewOriginalBrandPack = tblBrandPack.DefaultView()
                Me.m_ViewOriginalBrandPack.Sort = "BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewBrandPack
        End Function
#End Region

#Region " Function for MRKT_BRANDPACK "
        Public Function getlastPODate(ByVal DISTRIBUTOR_ID As String, ByVal BRANDPACK_ID As String, ByVal START_DATE As String) As Object
            Try
                Dim LPO As Object = Me.ExecuteScalar("", "SELECT TOP 1 PO_REF_DATE FROM ORDR_PURCHASE_ORDER,ORDR_PO_BRANDPACK" & _
                " WHERE ORDR_PO_BRANDPACK.PO_REF_NO = ORDR_PURCHASE_ORDER.PO_REF_NO AND ORDR_PO_BRANDPACK.BRANDPACK_ID = '" & _
                BRANDPACK_ID & "' AND ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "' AND ORDR_PURCHASE_ORDER.PO_REF_DATE >= '" & _
                START_DATE & "' ORDER BY ORDR_PURCHASE_ORDER.PO_REF_DATE DESC")
                Return LPO
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function HasgeneratedDiscount(ByVal PROG_BRANDPACK_DIST_IDS As Collection, ByVal FLAG As String) As Boolean
            Try
                If Not PROG_BRANDPACK_DIST_IDS.Count <= 0 Then
                    'Dim SearchString As String = PROG_BRANDPACK_DIST_IDS(I).ToString() + "" + FLAG
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "SELECT 1 WHERE EXISTS(SELECT MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY " & vbCrLf & _
                            " WHERE PROG_BRANDPACK_DIST_ID IN('" & vbCrLf
                    For I As Integer = 1 To PROG_BRANDPACK_DIST_IDS.Count
                        Query &= PROG_BRANDPACK_DIST_IDS(I).ToString() & "'"
                        If I = PROG_BRANDPACK_DIST_IDS.Count - 1 Then : Query &= ",'" : End If
                    Next
                    Query &= " ));" : Me.CreateCommandSql("sp_executesql", "")
                    Me.AddParameter("stmt", SqlDbType.NVarChar, Query)
                    Dim Retval As Object = Me.ExecuteScalar()
                    If Not IsNothing(Retval) And (Not IsDBNull(Retval)) Then
                        If Convert.ToInt16(Retval) > 0 Then : Return True : End If
                    End If
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function IsExisted(ByVal PROG_BRANDPACK_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_Existing_MRKT_BRANDPACK", "")
                Me.AddParameter("@PROG_BRANDPACK_ID", SqlDbType.VarChar, PROG_BRANDPACK_ID, 30)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function

        Public Function HasReferencedData(ByVal PROG_BRANDPACK_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_REFERENCED_MRKT_BRANDPACK", "")
                Me.AddParameter("@PROG_BRANDPACK_ID", SqlDbType.VarChar, PROG_BRANDPACK_ID, 30)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.VarChar, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function

        Public Function CreateNewItemChangesCollection() As System.Collections.Specialized.NameValueCollection
            Try
                Me.ItemChanges = New System.Collections.Specialized.NameValueCollection()
                Me.ItemChanges.Clear()
            Catch ex As Exception
                Throw ex
            End Try
            Return Me.ItemChanges
        End Function
        Public Function SearchBrandPack(ByVal SearchString As String) As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT BB.BRANDPACK_ID,BB.BRANDPACK_NAME FROM BRND_BRANDPACK BB WHERE IsActive = 1 " & vbCrLf & _
                        " AND EXISTS(SELECT ABI.BRANDPACK_ID FROM AGREE_BRANDPACK_INCLUDE ABI INNER JOIN AGREE_AGREEMENT AA " & vbCrLf & _
                        "            ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO WHERE ABI.BRANDPACK_ID = BB.BRANDPACK_ID AND AA.END_DATE >= @GETDATE  " & vbCrLf & _
                        "            ) AND BB.BRANDPACK_NAME LIKE '%" & SearchString & "%';"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Me.m_ViewBrandPack = Me.FillDataTable(New DataTable("T_BrandPack")).DefaultView
                Return Me.m_ViewBrandPack
                Me.m_ViewOriginalBrandPack = Me.m_ViewBrandPack
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function GetProgramID(ByVal SearchProgramID As String) As DataView
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT TOP 100 PROGRAM_ID,PROGRAM_NAME FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID LIKE '%" + SearchProgramID + "%' " & vbCrLf & _
                " ORDER BY END_DATE DESC;"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.m_ViewProgram = Me.FillDataTable(New DataTable("T_Program")).DefaultView()
                Return Me.m_ViewProgram
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function CreateViewMRKT_BRANDPACK(ByVal PROGRAM_ID As String) As DataView
            Try
                Me.CreateCommandSql("", "SELECT MRKT_MARKETING_PROGRAM.PROGRAM_ID,MRKT_MARKETING_PROGRAM.PROGRAM_NAME,BRND_BRANDPACK.BRANDPACK_NAME,MRKT_BRANDPACK.START_DATE " & _
                "AS BRANDPACK_START_DATE,MRKT_BRANDPACK.PROG_BRANDPACK_ID AS [ID],MRKT_BRANDPACK.BRANDPACK_ID,MRKT_BRANDPACK.END_DATE AS BRANDPACK_END_DATE FROM BRND_BRANDPACK," & _
                "MRKT_MARKETING_PROGRAM,MRKT_BRANDPACK WHERE BRND_BRANDPACK.BRANDPACK_ID = MRKT_BRANDPACK.BRANDPACK_ID " & _
                "AND MRKT_BRANDPACK.PROGRAM_ID = MRKT_MARKETING_PROGRAM.PROGRAM_ID AND MRKT_MARKETING_PROGRAM.PROGRAM_ID = '" & PROGRAM_ID & "'")
                Dim tblMRKT As New DataTable("MRKT_BRANDPACK")
                tblMRKT.Clear()
                Me.FillDataTable(tblMRKT)
                Me.m_ViewMarketingBrandPack = tblMRKT.DefaultView()
                Me.m_ViewMarketingBrandPack.RowStateFilter = DataViewRowState.CurrentRows
                Me.m_ViewMarketingBrandPack.Sort = "ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewMarketingBrandPack
        End Function

        Public Function Save_MRKT_BRANDPACK(ByVal SaveType As String) As Integer
            Dim retVal As Integer
            Try
                Me.GetConnection()
                Select Case SaveType
                    Case "Save"
                        Me.InsertData("Sp_Insert_MRKT_BRANDPACK", "")
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30)
                    Case "Update"
                        Me.UpdateData("Sp_Update_MRKT_BRANDPACK", "")
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30)
                End Select
                Me.AddParameter("@PROG_BRANDPACK_ID", SqlDbType.VarChar, Me.PROG_BRANDPACK_ID, 30) ' VARCHAR(25),
                Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, Me.PROGAM_ID, 15) ' VARCHAR(15),
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, Me.BRANDPACK_ID, 14)
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, Convert.ToDateTime(Me.START_DATE)) ' DATETIME,
                Me.AddParameter("@END_DATE", SqlDbType.DateTime, Convert.ToDateTime(Me.END_DATE)) ' DATETIME,
                Me.OpenConnection()
                If Me.ExecuteNonQuery() > 0 Then
                    retVal = 1
                    Me.CloseConnection()
                    If Not IsNothing(Me.m_ViewMarketingBrandPack) Then
                        Me.SaveToDataView(SaveType)
                    End If
                Else
                    Me.CloseConnection() : Me.ClearCommandParameters()
                    Throw New System.Exception(Me.MessageDBConcurency)
                End If
                'me.m_ViewMarketingBrandPack(0).
            Catch ex As DBConcurrencyException
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                retVal = 0
                Throw New System.Exception(Me.MessageDBConcurency)
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                retVal = 0
                Throw ex
            End Try
            Return retVal
        End Function

        Public Shadows Function SaveToDataView(ByVal SaveType As String) As DataView
            Try
                With Me.m_ViewMarketingBrandPack
                    Select Case SaveType
                        Case "Save"
                            drv = .AddNew()
                            drv("ID") = Me.PROG_BRANDPACK_ID
                            drv("PROGRAM_ID") = Me.PROGAM_ID
                            drv("PROGRAM_NAME") = Me.PROGRAM_NAME
                            Dim indexBrandPack As Integer = Me.ViewBrandPack.Find(Me.BRANDPACK_ID)
                            If indexBrandPack <> -1 Then
                                Me.BRANDPACK_NAME = Me.ViewBrandPack(indexBrandPack)("BRANDPACK_NAME").ToString()
                            End If
                            drv("BRANDPACK_ID") = Me.BRANDPACK_ID
                            drv("BRANDPACK_NAME") = Me.BRANDPACK_NAME
                            drv("BRANDPACK_START_DATE") = Me.START_DATE
                            drv("BRANDPACK_END_DATE") = Me.END_DATE
                            drv.EndEdit()
                            If Not IsNothing(Me.ItemChanges) Then
                                Me.ItemChanges.Add(Me.BRANDPACK_ID, Me.BRANDPACK_NAME)
                            End If
                        Case "Update"
                            Dim index As Integer = Me.m_ViewMarketingBrandPack.Find(Me.PROG_BRANDPACK_ID)
                            If index <> -1 Then
                                Me.m_ViewMarketingBrandPack(index)("ID") = Me.PROG_BRANDPACK_ID
                                Me.m_ViewMarketingBrandPack(index)("PROGRAM_ID") = Me.PROGAM_ID
                                Me.m_ViewMarketingBrandPack(index)("PROGRAM_NAME") = Me.PROGRAM_NAME
                                Me.m_ViewMarketingBrandPack(index)("BRANDPACK_ID") = Me.BRANDPACK_ID
                                Dim indexBrandPack As Integer = Me.ViewBrandPack.Find(Me.BRANDPACK_ID)
                                If indexBrandPack <> -1 Then
                                    Me.BRANDPACK_NAME = Me.ViewBrandPack(indexBrandPack)("BRANDPACK_NAME").ToString()
                                End If
                                Me.m_ViewMarketingBrandPack(index)("BRANDPACK_NAME") = Me.BRANDPACK_NAME
                                Me.m_ViewMarketingBrandPack(index)("BRANDPACK_START_DATE") = Me.START_DATE
                                Me.m_ViewMarketingBrandPack(index)("BRANDPACK_END_DATE") = Me.END_DATE
                                Me.m_ViewMarketingBrandPack(index).EndEdit()
                            End If
                    End Select

                End With
            Catch ex As Exception

            End Try
            Return Me.m_ViewMarketingBrandPack
        End Function

        Public Function getPriceValue(ByVal ListDistributorID As List(Of String), ByVal BRANDPACK_ID As String, ByVal START_D As String, ByVal END_D As String) As Decimal
            Dim retval As Object = Nothing
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                       " SELECT TOP 1 PRICE FROM DIST_PLANT_PRICE DPP  " & vbCrLf & _
                        "           WHERE BRANDPACK_ID = @BRANDPACK_ID AND START_DATE >= '" & START_D & "' " & vbCrLf & _
                        "           AND START_DATE <= '" & END_D & "' " & vbCrLf & _
                        "           AND EXISTS(SELECT DISTRIBUTOR_ID FROM (SELECT '"
                For i As Integer = 0 To ListDistributorID.Count - 1
                    Query &= ListDistributorID(i) & "' AS DISTRIBUTOR_ID "
                    If i < ListDistributorID.Count - 1 Then
                        Query &= " UNION ALL SELECT '"
                    End If
                Next
                Query &= ")C  WHERE DISTRIBUTOR_ID = DPP.DISTRIBUTOR_ID) ORDER BY START_DATE DESC; "
                Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, 14)
                Me.OpenConnection()
                retval = Me.SqlCom.ExecuteScalar()
                If (Not IsNothing(retval)) And (Not IsDBNull(retval)) Then
                    Return Convert.ToDecimal(retval)
                End If
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT TOP 1 PRICE FROM BRND_PRICE_HISTORY WHERE START_DATE >= '" & START_D & "' " & vbCrLf & _
                        " AND START_DATE <= '" & END_D & "' AND BRANDPACK_ID = @BRANDPACK_ID ORDER BY START_DATE DESC ;"
                Me.ResetCommandText(CommandType.Text, Query)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If (Not IsNothing(retval)) And (Not IsDBNull(retval)) Then
                    retval = Convert.ToDecimal(retval) : Else : retval = 0 : End If
                Return Convert.ToDecimal(retval)
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

#End Region

#Region " FunCtion  for MRKT_BRANDPACK_DISTRIBUTOR "

        Public Function CreateViewDistributor(ByVal BRANDPACK_ID As String) As DataView
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                         " SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME " & vbCrLf & _
                         " FROM DIST_DISTRIBUTOR DR WHERE EXISTS " & vbCrLf & _
                         "(SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN " & vbCrLf & _
                         " AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN " & vbCrLf & _
                         " AGREE_BRANDPACK_INCLUDE ABI ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                         " WHERE AA.END_DATE >= GETDATE() AND ABI.BRANDPACK_ID = '" & BRANDPACK_ID & "'" & vbCrLf & _
                         " AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) OPTION(KEEP PLAN);"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblDistributor As New DataTable("DISTRIBUTOR")
                tblDistributor.Clear()
                Me.FillDataTable(tblDistributor)
                Me.m_ViewDistributor = Me.CreateDataView(tblDistributor)
                Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistributor
        End Function
        Public Function CreateViewDistributor(ByVal PROGRAM_ID As String, ByVal BRANDPACK_ID As String, ByVal START_DATE As String, ByVal END_DATE As String) As DataView
            Try
                Dim PROG_BRANDPACK_ID As String = PROGRAM_ID + "" + BRANDPACK_ID
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                         " SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME " & vbCrLf & _
                         " FROM DIST_DISTRIBUTOR DR WHERE EXISTS " & vbCrLf & _
                         "(SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN " & vbCrLf & _
                         " AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN " & vbCrLf & _
                         " AGREE_BRANDPACK_INCLUDE ABI ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                         " WHERE AA.END_DATE >= '" & END_DATE & "' AND AA.START_DATE <= '" & START_DATE & "' AND ABI.BRANDPACK_ID = '" & BRANDPACK_ID & "'" & vbCrLf & _
                         " AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) AND NOT EXISTS(SELECT DISTRIBUTOR_ID " & vbCrLf & _
                         " FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_ID = '" & PROG_BRANDPACK_ID & "' AND DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                         " OPTION(KEEP PLAN);"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblDistributor As New DataTable("DISTRIBUTOR")
                tblDistributor.Clear()
                Me.FillDataTable(tblDistributor)
                Me.m_ViewDistributor = Me.CreateDataView(tblDistributor)
                Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistributor
        End Function
        'Public Function CreateViewDistributor_1(ByVal BRANDPACK_ID As String) As DataView
        '    Try
        '        Me.CreateCommandSql("", "SET NOCOUNT ON;SELECT DISTINCT DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID,DIST_DISTRIBUTOR.DISTRIBUTOR_NAME" & _
        '        " FROM AGREE_BRANDPACK_INCLUDE,AGREE_AGREEMENT,DISTRIBUTOR_AGREEMENT,DIST_DISTRIBUTOR WHERE DISTRIBUTOR_AGREEMENT.DISTRIBUTOR_ID = DIST_DISTRIBUTOR.DISTRIBUTOR_ID" & _
        '        " AND AGREE_AGREEMENT.AGREEMENT_NO = DISTRIBUTOR_AGREEMENT.AGREEMENT_NO AND AGREE_BRANDPACK_INCLUDE.AGREEMENT_NO = AGREE_AGREEMENT.AGREEMENT_NO AND AGREE_AGREEMENT.END_DATE >= " & NufarmBussinesRules.SharedClass.ShortGetDate() & " AND AGREE_BRANDPACK_INCLUDE.BRANDPACK_ID = '" + BRANDPACK_ID + "'")
        '        Dim tblDistributor As New DataTable("DISTRIBUTOR")
        '        tblDistributor.Clear()
        '        Me.FillDataTable(tblDistributor)
        '        Me.m_ViewDistributor = Me.CreateDataView(tblDistributor)
        '        Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        '    Return Me.m_ViewDistributor
        'End Function
        Public Function CreateViewBrandPack(ByVal PROGRAM_ID As String) As DataView
            Try
                Me.CreateCommandSql("", "SET NOCOUNT ON;SELECT MRKT_BRANDPACK.BRANDPACK_ID,BRND_BRANDPACK.BRANDPACK_NAME FROM " & _
                " MRKT_BRANDPACK INNER JOIN BRND_BRANDPACK ON MRKT_BRANDPACK.BRANDPACK_ID = BRND_BRANDPACK.BRANDPACK_ID " & _
                " WHERE MRKT_BRANDPACK.PROGRAM_ID = '" + PROGRAM_ID + "'")
                Dim tblBrandPack As New DataTable("BRANDPACK")
                tblBrandPack.Clear()
                Me.FillDataTable(tblBrandPack)
                If Not IsNothing(Me.m_ViewBrandPack) Then
                    Me.m_ViewBrandPack.Table.Clear()
                End If
                Me.m_ViewBrandPack = tblBrandPack.DefaultView()
                Me.m_ViewBrandPack.Sort = "BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewBrandPack
        End Function
        Public Function SaveToDataViewMBD(ByVal SaveType As String) As DataView
            Try
                With Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR
                    Select Case SaveType
                        Case "Save"
                            drv = .AddNew()
                            drv("PROG_BRANDPACK_DIST_ID") = Me.PROG_BRANDPACK_DIST_ID
                            drv("PROG_BRANDPACK_ID") = Me.PROG_BRANDPACK_ID
                            drv("PROGRAM_NAME") = Me.PROGRAM_NAME
                            drv("PROGRAM_ID") = Me.PROGAM_ID
                            drv("BRANDPACK_ID") = Me.BRANDPACK_ID
                            drv("BRANDPACK_NAME") = Me.BRANDPACK_NAME
                            drv("DISTRIBUTOR_NAME") = Me.DISTRIBUTOR_NAME
                            drv("DISTRIBUTOR_ID") = Me.DISTRIBUTOR_ID
                            drv("START_DATE") = Me.START_DATE
                            drv("END_DATE") = Me.END_DATE
                            drv("GIVEN_DISC_PCT") = Me.GIVEN_DISC_PCT
                            drv("TARGET_DISC_PCT") = Me.TARGET_DISC_PCT
                            drv("TARGET_QTY") = Me.TARGET_QTY
                            drv("STEPPING_FLAG") = Me.STEPPING
                            drv("ISRPK") = Me.ISRPK
                            drv("ISPKPP") = Me.ISPKPP
                            drv("GIVEN_PKPP") = Me.GIVEN_PKPP
                            drv("TARGET_PKPP") = Me.TARGET_PKPP
                            drv("ISCP") = Me.ISCP
                            drv("GIVEN_CP") = Me.GIVEN_CP
                            drv("TARGET_CP") = Me.TARGET_CP
                            drv("SCPD") = Me.isSCPD
                            drv("SHIP_TO_ID") = Me.SHIP_TO_ID
                            drv("IS_T_TM_DIST") = Me.IS_T_TM_DIST
                            drv("ISDK") = Me.ISDK
                            drv("GIVEN_DK") = Me.GIVEN_DK
                            drv("TARGET_DK") = Me.TARGET_DK
                            drv("ISHK") = Me.ISHK
                            drv("TARGET_HK") = Me.TARGET_HK
                            drv("PRICE_HK") = Me.PRICE_HK
                            drv("ISCPR") = Me.ISCPR
                            drv("GIVEN_CPR") = Me.GIVEN_CPR
                            drv("TARGET_CPR") = Me.TARGET_CPR
                            drv("BONUS_VALUE") = Me.BONUS_VALUE
                            drv("DESCRIPTIONS") = Me.DESCRIPTIONS
                            drv.EndEdit()
                        Case "Update"
                            Dim index As Integer = Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR.Find(Me.PROG_BRANDPACK_DIST_ID)
                            If index <> -1 Then
                                .Item(index)("PROG_BRANDPACK_DIST_ID") = Me.PROG_BRANDPACK_DIST_ID
                                .Item(index)("PROG_BRANDPACK_ID") = Me.PROG_BRANDPACK_ID
                                .Item(index)("PROGRAM_NAME") = Me.PROGRAM_NAME
                                .Item(index)("PROGRAM_ID") = Me.PROGAM_ID
                                .Item(index)("BRANDPACK_ID") = Me.BRANDPACK_ID
                                .Item(index)("BRANDPACK_NAME") = Me.BRANDPACK_NAME
                                .Item(index)("DISTRIBUTOR_NAME") = Me.DISTRIBUTOR_NAME
                                .Item(index)("DISTRIBUTOR_ID") = Me.DISTRIBUTOR_ID
                                .Item(index)("START_DATE") = Me.START_DATE
                                .Item(index)("END_DATE") = Me.END_DATE
                                .Item(index)("GIVEN_DISC_PCT") = Me.GIVEN_DISC_PCT
                                .Item(index)("TARGET_DISC_PCT") = Me.TARGET_DISC_PCT
                                .Item(index)("TARGET_QTY") = Me.TARGET_QTY
                                .Item(index)("STEPPING_FLAG") = Me.STEPPING
                                .Item(index)("ISRPK") = Me.ISRPK
                                .Item(index)("ISPKPP") = Me.ISPKPP
                                .Item(index)("GIVEN_PKPP") = Me.GIVEN_PKPP
                                .Item(index)("TARGET_PKPP") = Me.TARGET_PKPP
                                .Item(index)("ISCP") = Me.ISCP
                                .Item(index)("GIVEN_CP") = Me.GIVEN_CP
                                .Item(index)("TARGET_CP") = Me.TARGET_CP
                                .Item(index)("SCPD") = Me.isSCPD
                                .Item(index)("SHIP_TO_ID") = Me.SHIP_TO_ID
                                .Item(index)("IS_T_TM_DIST") = Me.IS_T_TM_DIST
                                .Item(index)("ISCPR") = Me.ISCPR
                                .Item(index)("GIVEN_CPR") = Me.GIVEN_CPR
                                .Item(index)("TARGET_CPR") = Me.TARGET_CPR
                                .Item(index)("ISDK") = Me.ISDK
                                .Item(index)("GIVEN_DK") = Me.GIVEN_DK
                                .Item(index)("TARGET_DK") = Me.TARGET_DK
                                .Item(index)("ISHK") = Me.ISHK
                                .Item(index)("TARGET_HK") = Me.TARGET_HK
                                .Item(index)("PRICE_HK") = Me.TARGET_HK
                                .Item(index)("BONUS_VALUE") = Me.BONUS_VALUE
                                .Item(index)("DESCRIPTIONS") = Me.DESCRIPTIONS
                                .Item(index).EndEdit()
                            End If
                    End Select
                End With

            Catch ex As Exception

            End Try
            Return Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR
        End Function

        Public Function IsExistedPROG_BRANDACK_DIST_ID(ByVal PROG_BRANDPACK_DIST_ID As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Sp_Check_Existing_PROG_BRANDPACK_DIST_ID", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Check_Existing_PROG_BRANDPACK_DIST_ID")
                End If

                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID, 40)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                Me.OpenConnection() : Dim retval As Object = Me.SqlCom.ExecuteScalar()
                If Not IsNothing(Me.SqlCom.Parameters("@RETURN_VALUE").Value) Then
                    If (CInt(Me.SqlCom.Parameters("@RETURN_VALUE").Value) > 0) Then
                        Me.CloseConnection() : Me.ClearCommandParameters()
                        Return True
                    End If
                End If
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return False
                'If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                '    Return True
                'End If
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function

        Public Function HasReferencedDataMRKT_BRANDPCK_DISTRIBUTOR(ByVal PROG_BRANDPACK_DIST_ID As String) As Boolean
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Sp_Check_REFERENCED_MRKT_BRANDPACK_DISTRIBUTOR", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Check_REFERENCED_MRKT_BRANDPACK_DISTRIBUTOR")
                End If
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID, 40)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function
        Public Function HasReferencedDataMRKT_BRANDPCK_DISTRIBUTOR_1(ByVal PROG_BRANDPACK_DIST_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_REFERENCED_MRKT_BRANDPACK_DISTRIBUTOR_1", "")
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID, 40)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function
        Public Function CreateViewMRK_BRANDPACK_DISTRIBUTOR() As DataView
            Try
                Me.CreateCommandSql("Sp_CREATE_VIEW_MRKT_BRANDPACK", "")
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, "", 40)
                Dim tblMBD As New DataTable("MBD")
                tblMBD.Clear()
                Me.FillDataTable(tblMBD)
                Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR = tblMBD.DefaultView()
                Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR.Sort = "PROG_BRANDPACK_DIST_ID"
                Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR.RowStateFilter = DataViewRowState.CurrentRows
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR
        End Function

        Public Sub SaveMRKT_BRANDPACK_DISTRIBUTOR(ByVal SaveType As String, ByVal mustCloseConnection As Boolean)
            Try
                'Me.GetConnection()
                Select Case SaveType
                    Case "Save"
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Insert_MRKT_BRANDPACK_DISTRIBUTOR", "")
                        Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Insert_MRKT_BRANDPACK_DISTRIBUTOR")
                        End If
                        'Me.InsertData("Usp_Insert_MRKT_BRANDPACK_DISTRIBUTOR", "")
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30)
                    Case "Update"
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Update_MRKT_BRANDPACK_DISTRIBUTOR", "")
                        Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Update_MRKT_BRANDPACK_DISTRIBUTOR")
                        End If
                        'Me.UpdateData("Usp_Update_MRKT_BRANDPACK_DISTRIBUTOR", "")
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                End Select

                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, Me.PROG_BRANDPACK_DIST_ID, 40)
                Me.AddParameter("@PROG_BRANDPACK_ID", SqlDbType.VarChar, Me.PROG_BRANDPACK_ID, 30)
                Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, Me.AGREE_BRANDPACK_ID, 39)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DISTRIBUTOR_ID, 10)
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, Me.START_DATE)
                Me.AddParameter("@END_DATE", SqlDbType.DateTime, Me.END_DATE)
                Me.AddParameter("@GIVEN_DISC_PCT", SqlDbType.Decimal, Me.GIVEN_DISC_PCT)
                Me.AddParameter("@TARGET_QTY", SqlDbType.Decimal, Me.TARGET_QTY)
                Me.AddParameter("@TARGET_DISC_PCT", SqlDbType.Decimal, Me.TARGET_DISC_PCT)
                Me.AddParameter("@STEPPING_FLAG", SqlDbType.Bit, Me.STEPPING)
                Me.AddParameter("@ISRPK", SqlDbType.Bit, Me.ISRPK)
                Me.AddParameter("@ISPKPP", SqlDbType.Bit, Me.ISPKPP)
                Me.AddParameter("@GIVEN_PKPP", SqlDbType.Decimal, Me.GIVEN_PKPP)
                Me.AddParameter("@TARGET_PKPP", SqlDbType.Decimal, Me.TARGET_PKPP)
                Me.AddParameter("@BONUS_VALUE", SqlDbType.Decimal, Me.BONUS_VALUE)
                Me.AddParameter("@ISCP", SqlDbType.Bit, Me.ISCP) ' BIT,
                Me.AddParameter("@GIVEN_CP", SqlDbType.Decimal, Me.GIVEN_CP)
                Me.AddParameter("@TARGET_CP", SqlDbType.Decimal, Me.TARGET_CP)
                Me.AddParameter("@ISSCPD", SqlDbType.Bit, Me.isSCPD)

                Me.AddParameter("@ISCPMRT", SqlDbType.Bit, Me.ISCPMRT) ' BIT,
                Me.AddParameter("@GIVEN_CPMRT", SqlDbType.Decimal, Me.GIVEN_CPRMT)
                Me.AddParameter("@TARGET_CPMRT", SqlDbType.Decimal, Me.TARGET_CPMRT)

                Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, Me.SHIP_TO_ID, 25)
                Me.AddParameter("@IS_T_TM_DIST", SqlDbType.Bit, Me.IS_T_TM_DIST)
                Me.AddParameter("@ISCPR", SqlDbType.Bit, Me.ISCPR)
                Me.AddParameter("@GIVEN_CPR", SqlDbType.Decimal, Me.GIVEN_CPR)
                Me.AddParameter("@TARGET_CPR", SqlDbType.Decimal, Me.TARGET_CPR)
                Me.AddParameter("@ISDK", SqlDbType.Bit, Me.ISDK) ' BIT,
                Me.AddParameter("@GIVEN_DK", SqlDbType.Decimal, Me.GIVEN_DK)
                Me.AddParameter("@TARGET_DK", SqlDbType.Decimal, Me.TARGET_DK)
                Me.AddParameter("@ISHK", SqlDbType.Bit, Me.ISHK) ' BIT,
                Me.AddParameter("@TARGET_HK", SqlDbType.Decimal, Me.TARGET_HK)
                Me.AddParameter("@PRICE_HK", SqlDbType.Decimal, Me.PRICE_HK)
                Me.AddParameter("@DESCRIPTIONS", SqlDbType.VarChar, Me.DESCRIPTIONS, 200)
                Me.OpenConnection()
                If IsNothing(Me.SqlTrans) Then
                    Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                End If
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                'If Me.STEPPING = 0 Then
                '    Query = "SET NOCOUNT ON;" & vbCrLf & _
                '            "IF EXISTS(SELECT PROG_BRANDPACK_DIST_ID FROM MRKT_STEPPING_DISCOUNT WHERE PROG_BRANDPACK_DIST_ID = '" & Me.PROG_BRANDPACK_DIST_ID & "') " & vbCrLf & _
                '            " BEGIN " & vbCrLf & _
                '            " DELETE FROM MRKT_STEPPING_DISCOUNT WHERE PROG_BRANDPACK_DIST_ID = '" & Me.PROG_BRANDPACK_DIST_ID & "';" & vbCrLf & _
                '            " END "
                '    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                '    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                '    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                'End If

                If mustCloseConnection Then : Me.CommiteTransaction() : Me.CloseConnection() : End If
                If Not IsNothing(Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR) Then
                    Me.SaveToDataViewMBD(SaveType)
                End If
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
        End Sub
        'FUNCTION UNTUK MENGECEK APAKAH DISTRIBUTOR YANG MAU DI INSERT ADA DALAM AGREEMENT_UNTUK BRANDPAC YANG DI PILIH
        'ATAU TIDAK

        Public Function IsExistedSameBrandPackDistributor(ByVal DISTRIBUTOR_ID As String, ByVal BRANDPACK_ID As String, _
        ByVal START_DATE As Object) As Boolean
            Try
                Me.CreateCommandSql("Sp_Chek_DISTRIBUTOR_BRANDPACK_WITH_SAME_PERIOD", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 35) ' VARCHAR(35)
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, START_DATE) ' DATETIME,
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If Me.GetReturnValueByExecuteScalar("@RETURN_VALUE") > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function
        Public Function GetAGREE_BRANDPACK_ID(ByVal BRANDPACK_ID As String, ByVal DistributorID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal mustCloseConnection As Boolean) As String
            Dim retVal As Object = Nothing
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Usp_GetAgreeBrandPack_ID_FOR_MRKT_BRANDPACK", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_GetAgreeBrandPack_ID_FOR_MRKT_BRANDPACK")
                End If
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.DateTime, EndDate)
                Me.OpenConnection() : retVal = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If retVal Is Nothing Then
                    Return ""
                ElseIf retVal Is DBNull.Value Then
                    Return ""
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return retVal.ToString()
        End Function

        Public Sub InsertAgreementDPRD(ByVal ds As DataSet)
            Try
                Dim UnregisteredBrandPacks As New List(Of BrandPackDistributor)()
                Dim result As Integer = 0
                ''INSERT DULU KE TABLE PROGRAM
                Dim PROGRAM_ID As String = ds.Tables(0).Rows(0)("PROGRAM_ID").ToString()
                Dim Query As String = "SET NOCOUNT ON;SELECT 1 WHERE EXISTS(SELECT PROGRAM_ID FROM MRKT_MARKETING_PROGRAM" _
                & " WHERE PROGRAM_ID = @PROGRAM_ID) option(keep plan)"
                Me.CreateCommandSql("", Query)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, PROGRAM_ID, 15)
                result = CInt(Me.SqlCom.ExecuteScalar()) : Me.SqlCom.Parameters.Clear()
                If (result < 1) Then ''jika sudah ada
                    'check program brandpacknya
                    Me.SqlCom.CommandText = "SET NOCOUNT ON;INSERT INTO MRKT_MARKETING_PROGRAM(PROGRAM_ID,PROGRAM_NAME,START_DATE,END_DATE,CREATE_BY,CREATE_DATE) " _
                     & " VALUES(@PROGRAM_ID,@PROGRAM_NAME,@START_DATE,@END_DATE,@CREATE_BY,@CREATE_DATE)"
                    Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, ds.Tables(0).Rows(0)("PROGRAM_ID"))
                    Me.AddParameter("@PROGRAM_NAME", SqlDbType.VarChar, ds.Tables(0).Rows(0)("PROGRAM_NAME"))
                    Me.AddParameter("@START_DATE", SqlDbType.DateTime, Convert.ToDateTime(ds.Tables(0).Rows(0)("START_DATE")))
                    Me.AddParameter("@END_DATE", SqlDbType.DateTime, Convert.ToDateTime(ds.Tables(0).Rows(0)("END_DATE")))
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, ds.Tables(0).Rows(0)("CREATE_BY"))
                    Me.AddParameter("@CREATE_DATE", SqlDbType.DateTime, Convert.ToDateTime(ds.Tables(0).Rows(0)("CREATE_DATE")))
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Else
                    ''UPDATE DATA
                    Me.SqlCom.CommandText = "SET NOCOUNT ON;UPDATE MRKT_MARKETING_PROGRAM SET PROGRAM_NAME = @PROGRAM_NAME,START_DATE =@START_DATE," _
                    & "END_DATE = @END_DATE,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE WHERE PROGRAM_ID = @PROGRAM_ID"
                    Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, ds.Tables(0).Rows(0)("PROGRAM_ID"))
                    Me.AddParameter("@PROGRAM_NAME", SqlDbType.VarChar, ds.Tables(0).Rows(0)("PROGRAM_NAME"))
                    Me.AddParameter("@START_DATE", SqlDbType.DateTime, Convert.ToDateTime(ds.Tables(0).Rows(0)("START_DATE")))
                    Me.AddParameter("@END_DATE", SqlDbType.DateTime, Convert.ToDateTime(ds.Tables(0).Rows(0)("END_DATE")))
                    Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                Dim DvBrandPackDistributor As DataView = ds.Tables(2).DefaultView()
                For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    'CHECK DULU DATANYA
                    Me.SqlCom.CommandText = "SET NOCOUNT ON;SELECT 1 WHERE EXISTS(SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK WHERE " _
                    & " PROG_BRANDPACK_ID =@PROG_BRANDPACK_ID) option(keep plan)"
                    Me.AddParameter("@PROG_BRANDPACK_ID", SqlDbType.VarChar, ds.Tables(1).Rows(i)("PROG_BRANDPACK_ID"))
                    result = CInt(Me.SqlCom.ExecuteScalar())
                    Me.ClearCommandParameters()
                    If (result < 1) Then
                        Me.SqlCom.CommandText = "SET NOCOUNT ON;INSERT INTO MRKT_BRANDPACK(PROG_BRANDPACK_ID,PROGRAM_ID,BRANDPACK_ID," _
                                           & "START_DATE,END_DATE,CREATE_BY,CREATE_DATE) " _
                                           & "VALUES(@PROG_BRANDPACK_ID,@PROGRAM_ID,@BRANDPACK_ID,@START_DATE,@END_DATE,@CREATE_BY,@CREATE_DATE)"

                        Me.AddParameter("@PROG_BRANDPACK_ID", SqlDbType.VarChar, ds.Tables(1).Rows(i)("PROG_BRANDPACK_ID"))
                        Me.AddParameter("@PROGRAM_ID", SqlDbType.VarChar, ds.Tables(1).Rows(i)("PROGRAM_ID"))
                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, ds.Tables(1).Rows(i)("BRANDPACK_ID"))
                        'Me.AddParameter("@PROGRAM_NAME", SqlDbType.VarChar, ds.Tables(1).Rows(i)("PROGRAM_NAME"))
                        Me.AddParameter("START_DATE", SqlDbType.DateTime, Convert.ToDateTime(ds.Tables(1).Rows(i)("START_DATE")))
                        Me.AddParameter("@END_DATE", SqlDbType.DateTime, Convert.ToDateTime((ds.Tables(1).Rows(i)("END_DATE"))))
                        Me.AddParameter("@CREATE_DATE", SqlDbType.DateTime, Convert.ToDateTime(ds.Tables(1).Rows(i)("CREATE_DATE")))
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, ds.Tables(1).Rows(i)("CREATE_BY"))
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        'filter progbrandpack_idnya
                        ''INSERT LANGSUNG DITRIBUTOR_NYA
                        DvBrandPackDistributor.RowFilter = "PROG_BRANDPACK_ID = '" + ds.Tables(1).Rows(i)("PROG_BRANDPACK_ID").ToString() + "'"
                        'INSERT  MRKT_BRANDPACK_DISTRIBUTOR BY LOOPING
                        For i_1 As Integer = 0 To DvBrandPackDistributor.Count - 1
                            Me.SqlCom.CommandText = "Usp_GetAgreeBrandPack_ID_FOR_MRKT_BRANDPACK"
                            Me.SqlCom.CommandType = CommandType.StoredProcedure
                            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, ds.Tables(1).Rows(i)("BRANDPACK_ID"))
                            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DvBrandPackDistributor(i_1)("DISTRIBUTOR_ID"))
                            Dim res As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            If (Not IsNothing(res)) Then
                                Dim AGREE_BRANDPACK_ID As String = res.ToString()
                                Me.SqlCom.CommandType = CommandType.Text
                                ''BILA ADA AMBIL STORED PROCEDURE USP_GET_AGREE_BRANDPACK_ID UNTUK MENDAPA
                                Me.SqlCom.CommandText = "SET NOCOUNT ON;INSERT INTO MRKT_BRANDPACK_DISTRIBUTOR(PROG_BRANDPACK_DIST_ID,PROG_BRANDPACK_ID," _
                                & "AGREE_BRANDPACK_ID,DISTRIBUTOR_ID,START_DATE,END_DATE,ISRPK,GIVEN_DISC_PCT,CREATE_BY,CREATE_DATE)" _
                                & "VALUES(@PROG_BRANDPACK_DIST_ID,@PROG_BRANDPACK_ID,@AGREE_BRANDPACK_ID,@DISTRIBUTOR_ID,@START_DATE,@END_DATE," _
                                & "1,@GIVEN_DISC_PCT,@CREATE_BY,@CREATE_DATE)"
                                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, DvBrandPackDistributor(i_1)("PROG_BRANDPACK_DIST_ID"))
                                Me.AddParameter("@PROG_BRANDPACK_ID", SqlDbType.VarChar, DvBrandPackDistributor(i_1)("PROG_BRANDPACK_ID"))
                                Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID)
                                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DvBrandPackDistributor(i_1)("DISTRIBUTOR_ID"))
                                Me.AddParameter("@START_DATE", SqlDbType.DateTime, Convert.ToDateTime(DvBrandPackDistributor(i_1)("START_DATE")))
                                Me.AddParameter("@END_DATE", SqlDbType.DateTime, Convert.ToDateTime(DvBrandPackDistributor(i_1)("END_DATE")))
                                Me.AddParameter("@GIVEN_DISC_PCT", SqlDbType.Decimal, Convert.ToDecimal(DvBrandPackDistributor(i_1)("GIVEN_DISC_PCT")), 0)
                                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, DvBrandPackDistributor(i_1)("CREATE_BY"))
                                Me.AddParameter("@CREATE_DATE", SqlDbType.DateTime, Convert.ToDateTime(DvBrandPackDistributor(i_1)("CREATE_DATE")))
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Else
                                Dim URB As New BrandPackDistributor()
                                Me.SqlCom.CommandText = "SET NOCOUNT ON;SELECT BRANDPACK_NAME FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID"
                                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, ds.Tables(1).Rows(i)("BRANDPACK_ID")) ' + "';"
                                Me.SqlCom.CommandType = CommandType.Text
                                URB.BrandPackName = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                                Me.SqlCom.CommandText = "SELECT DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID"
                                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DvBrandPackDistributor(i_1)("DISTRIBUTOR_ID"))
                                URB.DistributorName = Me.SqlCom.ExecuteScalar() : UnregisteredBrandPacks.Add(URB)
                                Me.ClearCommandParameters()
                                'Me.SqlCom.CommandType = CommandType.Text
                                'Me.SqlRe = Me.SqlCom.ExecuteReader()
                            End If
                        Next
                    Else
                        Me.SqlCom.CommandText = "SET NOCOUNT ON;UPDATE MRKT_BRANDPACK SET START_DATE = @START_DATE,END_DATE = @END_DATE,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " _
                                             & " WHERE PROG_BRANDPACK_ID = @PROG_BRANDPACK_ID"
                        Me.SqlCom.CommandType = CommandType.Text
                        Me.AddParameter("@START_DATE", SqlDbType.DateTime, Convert.ToDateTime(ds.Tables(1).Rows(i)("START_DATE")))
                        Me.AddParameter("@END_DATE", SqlDbType.DateTime, Convert.ToDateTime(ds.Tables(1).Rows(i)("END_DATE")))
                        Me.AddParameter("@PROG_BRANDPACK_ID", SqlDbType.VarChar, ds.Tables(1).Rows(i)("PROG_BRANDPACK_ID"))
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                        Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        DvBrandPackDistributor.RowFilter = "PROG_BRANDPACK_ID = '" + ds.Tables(1).Rows(i)("PROG_BRANDPACK_ID").ToString() + "'"
                        For i_1 As Integer = 0 To DvBrandPackDistributor.Count - 1
                            ''BILA ADA AMBIL STORED PROCEDURE USP_GET_AGREE_BRANDPACK_ID UNTUK MENDAPA
                            Me.SqlCom.CommandText = "Usp_GetAgreeBrandPack_ID_FOR_MRKT_BRANDPACK"
                            Me.SqlCom.CommandType = CommandType.StoredProcedure
                            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, ds.Tables(1).Rows(i)("BRANDPACK_ID"))
                            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DvBrandPackDistributor(i_1)("DISTRIBUTOR_ID"))
                            Dim res As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            If (Not IsNothing(res)) Then
                                Dim AGREE_BRANDPACK_ID As String = res.ToString()
                                Dim PROG_BRANDPACK_DIST_ID As String = DvBrandPackDistributor(i_1)("PROG_BRANDPACK_DIST_ID").ToString()
                                Me.SqlCom.CommandType = CommandType.Text
                                Me.SqlCom.CommandText = "SET NOCOUNT ON;SELECT 1 WHERE EXISTS(SELECT PROG_BRANDPACK_DIST_ID FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_DIST_ID = " _
                                                        & " @PROG_BRANDPACK_DIST_ID)"
                                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID)

                                res = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                                If (res Is Nothing) Then
                                    ''INSERT DATA
                                    Me.SqlCom.CommandText = "SET NOCOUNT ON;INSERT INTO MRKT_BRANDPACK_DISTRIBUTOR(PROG_BRANDPACK_DIST_ID,PROG_BRANDPACK_ID," _
                                                         & "AGREE_BRANDPACK_ID,DISTRIBUTOR_ID,START_DATE,END_DATE,ISRPK,GIVEN_DISC_PCT,CREATE_BY,CREATE_DATE)" _
                                                         & "VALUES(@PROG_BRANDPACK_DIST_ID,@PROG_BRANDPACK_ID,@AGREE_BRANDPACK_ID,@DISTRIBUTOR_ID,@START_DATE,@END_DATE," _
                                                         & "1,@GIVEN_DISC_PCT,@CREATE_BY,@CREATE_DATE)"
                                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID)
                                    Me.AddParameter("@PROG_BRANDPACK_ID", SqlDbType.VarChar, DvBrandPackDistributor(i_1)("PROG_BRANDPACK_ID"))
                                    Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID)
                                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DvBrandPackDistributor(i_1)("DISTRIBUTOR_ID"))
                                    Me.AddParameter("@START_DATE", SqlDbType.DateTime, Convert.ToDateTime(DvBrandPackDistributor(i_1)("START_DATE")))
                                    Me.AddParameter("@END_DATE", SqlDbType.DateTime, Convert.ToDateTime(DvBrandPackDistributor(i_1)("END_DATE")))
                                    Me.AddParameter("@GIVEN_DISC_PCT", SqlDbType.Decimal, Convert.ToDecimal(DvBrandPackDistributor(i_1)("GIVEN_DISC_PCT")), 0)
                                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, DvBrandPackDistributor(i_1)("CREATE_BY"))
                                    Me.AddParameter("@CREATE_DATE", SqlDbType.DateTime, Convert.ToDateTime(DvBrandPackDistributor(i_1)("CREATE_DATE")))
                                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                                Else
                                    Me.SqlCom.CommandText = "SET NOCOUNT ON;UPDATE MRKT_BRANDPACK_DISTRIBUTOR SET START_DATE = @START_DATE,END_DATE = @END_DATE," _
                                                            & "ISRPK = 1,GIVEN_DISC_PCT = @GIVEN_DISC_PCT,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID "
                                    Me.AddParameter("@START_DATE", SqlDbType.DateTime, Convert.ToDateTime(DvBrandPackDistributor(i_1)("START_DATE")))
                                    Me.AddParameter("@END_DATE", SqlDbType.DateTime, Convert.ToDateTime(DvBrandPackDistributor(i_1)("END_DATE")))
                                    Me.AddParameter("@GIVEN_DISC_PCT", SqlDbType.Decimal, Convert.ToDecimal(DvBrandPackDistributor(i_1)("GIVEN_DISC_PCT")))
                                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID)
                                    Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                                    Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                                End If
                            Else
                                Dim URB As New BrandPackDistributor()
                                Me.SqlCom.CommandText = "SET NOCOUNT ON;SELECT BRANDPACK_NAME FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID"
                                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, ds.Tables(1).Rows(i)("BRANDPACK_ID")) ' + "';"
                                Me.SqlCom.CommandType = CommandType.Text
                                URB.BrandPackName = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                                Me.SqlCom.CommandText = "SELECT DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID"
                                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DvBrandPackDistributor(i_1)("DISTRIBUTOR_ID"))
                                URB.DistributorName = Me.SqlCom.ExecuteScalar() : UnregisteredBrandPacks.Add(URB)
                                Me.ClearCommandParameters()
                            End If
                        Next
                    End If
                Next
                'CHECK recapDPRDNasional
                Me.SqlCom.CommandText = "SET NOCOUNT ON; " & vbCrLf & _
                                        "IF EXISTS(SELECT SpCode FROM RecapNDPRD WHERE SpCode = @SpCode AND BrandCode = @BrandCode) " & vbCrLf & _
                                        " BEGIN " & vbCrLf & _
                                        "  UPDATE RecapNDPRD SET StartDate = @StartDate,EndDate = @EndDate,BudgetTerritory = @BudgetTerritory,BudgetDispro = @BudgetDispro, " & vbCrLf & _
                                        "  TotalCoverage = @TotalCoverage,ModifiedBy = @ModifiedBy,ModifiedDate = @ModifiedDate WHERE SpCode = @SpCode AND BrandCode = @BrandCode " & vbCrLf & _
                                        " END " & vbCrLf & _
                                        " ELSE " & vbCrLf & _
                                        " BEGIN " & vbCrLf & _
                                        " INSERT INTO RecapNDPRD(SpCode,BrandCode,TerritoryCode,BudgetTerritory,BudgetDispro,TotalCoverage,StartDate,EndDate,CreatedBy,CreatedDate) " & vbCrLf & _
                                        " VALUES(@SpCode,@BrandCode,@TerritoryCode,@BudgetTerritory,@BudgetDispro,@TotalCoverage,@StartDate,@EndDate,@CreatedBy,@CreatedDate) ;" & vbCrLf & _
                                        " END "
                For i As Integer = 0 To ds.Tables(4).Rows.Count - 1
                    Me.AddParameter("@SpCode", SqlDbType.VarChar, ds.Tables(0).Rows(0)("PROGRAM_ID"))
                    Me.AddParameter("START_DATE", SqlDbType.DateTime, Convert.ToDateTime(ds.Tables(4).Rows(i)("StartDate")))
                    Me.AddParameter("@END_DATE", SqlDbType.DateTime, Convert.ToDateTime((ds.Tables(4).Rows(i)("EndDate"))))
                    Me.AddParameter("@TypeApp", SqlDbType.VarChar, ds.Tables(4).Rows(i)("TypeApp"), 32)
                    Me.AddParameter("@BrandCode", SqlDbType.VarChar, ds.Tables(4).Rows(i)("BrandCode"), 16)
                    Me.AddParameter("@TerritoryCode", SqlDbType.VarChar, ds.Tables(4).Rows(i)("TerritoryCode"), 16)
                    Me.AddParameter("@BudgetTerritory", SqlDbType.Decimal, Convert.ToDecimal(ds.Tables(4).Rows(i)("BudgetTerritory")))
                    Me.AddParameter("@BudgetDispro", SqlDbType.Decimal, ds.Tables(4).Rows(i)("BudgetDispro"))
                    Me.AddParameter("@TotalCoverage", SqlDbType.Decimal, Convert.ToDecimal(ds.Tables(4).Rows(i)("TotalCoverage")))
                    Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, ds.Tables(4).Rows(i)("StartDate"))
                    Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, ds.Tables(4).Rows(i)("EndDate"))
                    Me.AddParameter("@CreatedDate", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.AddParameter("@CreatedBy", SqlDbType.VarChar, ds.Tables(0).Rows(0)("CREATE_BY"), 50)
                    Me.AddParameter("@ModifiedBY", SqlDbType.VarChar, ds.Tables(0).Rows(0)("CREATE_BY"), 50)
                    Me.AddParameter("@ModifiedDate", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Next
                Me.CommiteTransaction() : Me.CloseConnection()

                If UnregisteredBrandPacks.Count > 0 Then
                    Me.BuildMessageSavingAttachement(UnregisteredBrandPacks)
                Else
                    Throw New Exception("Data(s) saved succesfully" & vbCrLf & "You can check them in Sales program manager")
                End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Throw ex
            Finally

            End Try
        End Sub
        Private Sub BuildMessageSavingAttachement(ByVal UnregisteredBrandPacks As List(Of BrandPackDistributor))
            Dim Message As String = "List of Uncommite BrandPack-Distributor." & vbCrLf
            For i As Integer = 0 To UnregisteredBrandPacks.Count - 1
                Message &= "BrandPack : " & UnregisteredBrandPacks(i).BrandPackName & " Distributor : " & UnregisteredBrandPacks(i).DistributorName & vbCrLf
            Next
            'Dim E As New Exception(Message)
            'E.Source = "my Error"
            'System.Windows.Forms.MessageBox.Show(Message,"Information",Windows.Forms.MessageBoxButtons.OK,Windows.Forms.MessageBoxIcon.Information);
            'Throw (E)
        End Sub
        Public Function GetAgree_BrandPackID_1(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String) As String
            Dim retVal As Object = Nothing
            Try
                Me.CreateCommandSql("Usp_GetAgreeBrandPack_ID_FOR_MRKT_BRANDPACK", "")
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                retVal = Me.ExecuteScalar()
                If retVal Is Nothing Then
                    Return ""
                ElseIf retVal Is DBNull.Value Then
                    Return ""
                End If
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return retVal.ToString()
        End Function

        Public Function getPKPPValue(ByVal DistributorID As String, ByVal BrandPackID As String, ByVal minStartDate As DateTime, Optional ByVal EndDate As Object = Nothing) As DataView
            Try
                If Not IsNothing(EndDate) Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                           "SELECT MP.PROGRAM_ID,MBD.TARGET_PKPP,MBD.BONUS_VALUE AS GIVEN_VALUE,MBD.PROG_BRANDPACK_DIST_ID AS IDSYSTEM " & vbCrLf & _
                           " FROM MRKT_MARKETING_PROGRAM MP INNER JOIN MRKT_BRANDPACK MB ON MP.PROGRAM_ID = MB.PROGRAM_ID " & vbCrLf & _
                           " INNER JOIN MRKT_BRANDPACK_DISTRIBUTOR MBD ON MB.PROG_BRANDPACK_ID = MBD.PROG_BRANDPACK_ID " & vbCrLf & _
                           " WHERE MB.BRANDPACK_ID = @BRANDPACK_ID AND MBD.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                           " AND MBD.START_DATE >= @START_DATE AND MBD.END_DATE <= @END_DATE AND MBD.ISPKPP = 1 AND MBD.BONUS_VALUE > 0 " & vbCrLf & _
                           " AND NOT EXISTS(SELECT PROG_BRANDPACK_DIST_ID FROM ORDR_PO_BRANDPACK WHERE PROG_BRANDPACK_DIST_ID = MBD.PROG_BRANDPACK_DIST_ID) ;"
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                          "SELECT MP.PROGRAM_ID,MBD.TARGET_PKPP,MBD.BONUS_VALUE AS GIVEN_VALUE,MBD.PROG_BRANDPACK_DIST_ID AS IDSYSTEM " & vbCrLf & _
                          " FROM MRKT_MARKETING_PROGRAM MP INNER JOIN MRKT_BRANDPACK MB ON MP.PROGRAM_ID = MB.PROGRAM_ID " & vbCrLf & _
                          " INNER JOIN MRKT_BRANDPACK_DISTRIBUTOR MBD ON MB.PROG_BRANDPACK_ID = MBD.PROG_BRANDPACK_ID " & vbCrLf & _
                          " WHERE MB.BRANDPACK_ID = @BRANDPACK_ID AND MBD.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                          " AND MBD.START_DATE >= @START_DATE AND MBD.ISPKPP = 1 AND MBD.BONUS_VALUE > 0 " & vbCrLf & _
                          " AND NOT EXISTS(SELECT PROG_BRANDPACK_DIST_ID FROM ORDR_PO_BRANDPACK WHERE PROG_BRANDPACK_DIST_ID = MBD.PROG_BRANDPACK_DIST_ID) ;"
                End If
                Me.CreateCommandSql("", Query)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 15)
                If Not IsNothing(EndDate) Then
                    Me.AddParameter("@END_DATE", SqlDbType.DateTime, EndDate)
                End If
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, minStartDate)
                Dim dtTable As New DataTable() : dtTable.Clear()
                Me.FillDataTable(dtTable) : Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getEndDateAgreement(ByVal DISTRIBUTOR_ID As String, ByVal BRANDPACK_ID As String, ByVal ProposedMinDate As DateTime) As DateTime
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT TOP 1 AGREE.END_DATE FROM AGREE_AGREEMENT AGREE INNER JOIN DISTRIBUTOR_AGREEMENT DA " & vbCrLf & _
                        " ON AGREE.AGREEMENT_NO = DA.AGREEMENT_NO INNER JOIN AGREE_BRANDPACK_INCLUDE ABI ON AGREE.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                        " WHERE DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ABI.BRANDPACK_ID = @BRANDPACK_ID AND AGREE.END_DATE >= @ProposedDate AND AGREE.START_DATE <= @ProposedDate ;"
                If Not IsNothing(Me.SqlCom) Then
                    Me.ResetCommandText(CommandType.Text, Query)
                Else
                    Me.CreateCommandSql("", Query)
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 14)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@ProposedDate", SqlDbType.DateTime, ProposedMinDate)
                Dim retval As Object = Me.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) Then
                    Return Convert.ToDateTime(retval)
                End If
                Return NufarmBussinesRules.SharedClass.ServerDate
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getTM(ByVal mustCloseConnection As Boolean) As DataView
            Try
                'Me.CreateCommandSql("Usp_Get_TM_by_Distributor_ID", "")
                'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Query = "SET NOCOUNT ON ; " & vbCrLf & _
                        "SELECT ST.SHIP_TO_ID,TER.TERRITORY_AREA,TM.MANAGER " & vbCrLf & _
                        " FROM TERRITORY TER INNER JOIN SHIP_TO ST ON TER.TERRITORY_ID = ST.TERRITORY_ID " & vbCrLf & _
                        " INNER JOIN TERRITORY_MANAGER TM ON ST.TM_ID = TM.TM_ID  WHERE ST.INACTIVE = 0 ;"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblTM As New DataTable("TM")
                tblTM.Clear() : Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(tblTM) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return tblTM.DefaultView()
                'Me.FillDataTable(tblTM)
                'Return Me.m_ViewTM
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
#End Region

#End Region

#Region " Property "
        Public ReadOnly Property ViewBrandPack() As DataView
            Get
                Return Me.m_ViewBrandPack
            End Get
        End Property
        Public ReadOnly Property ViewMarketingBrandPack() As DataView
            Get
                Return Me.m_ViewMarketingBrandPack
            End Get
        End Property
        Public ReadOnly Property ViewProgram() As DataView
            Get
                Return Me.m_ViewProgram
            End Get
        End Property
        Public ReadOnly Property ViewOriginalBrandPack() As DataView
            Get
                Return Me.m_ViewOriginalBrandPack
            End Get
        End Property
        Public ReadOnly Property ViewDistributor() As DataView
            Get
                Return Me.m_ViewDistributor
            End Get
        End Property
        Public ReadOnly Property ViewMRK_BRANDPACK_DISTRIBUTOR() As DataView
            Get
                Return Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR
            End Get
        End Property
#End Region

#Region " Constructor & Destructor "
        Public Sub New()
            MyBase.New()
            Me.BRANDPACK_ID = ""
            'Me.START_DATE = Nothing
            'Me.END_DATE = Nothing
            Me.ItemChanges = Nothing
        End Sub
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewBrandPack) Then
                Me.m_ViewBrandPack.Dispose()
                Me.m_ViewBrandPack = Nothing
            End If
            If Not IsNothing(Me.m_ViewMarketingBrandPack) Then
                Me.m_ViewMarketingBrandPack.Dispose()
                Me.m_ViewMarketingBrandPack = Nothing
            End If
            If Not IsNothing(Me.m_ViewProgram) Then
                Me.m_ViewProgram.Dispose()
                Me.m_ViewProgram = Nothing
            End If
            If Not IsNothing(Me.m_ViewOriginalBrandPack) Then
                Me.m_ViewOriginalBrandPack.Dispose()
                Me.m_ViewOriginalBrandPack = Nothing
            End If
            If Not IsNothing(Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR) Then
                Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR.Dispose()
                Me.m_ViewMRK_BRANDPACK_DISTRIBUTOR = Nothing
            End If
            If Not IsNothing(Me.m_ViewDistributor) Then
                Me.m_ViewDistributor.Dispose()
                Me.m_ViewDistributor = Nothing
            End If
        End Sub
#End Region

    End Class
    Class BrandPackDistributor
        Public BrandPackName As String
        Public DistributorName As String
    End Class
End Namespace

