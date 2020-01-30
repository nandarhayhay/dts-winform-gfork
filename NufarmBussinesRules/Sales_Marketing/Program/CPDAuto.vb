Imports System.Data.SqlClient
Imports Nufarm.Domain
Namespace Program
    Public Class BRCPDAuto : Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Protected Query As String = ""
        Public Function getDefaultStartDate(ByVal mustCloseConnection As Boolean) As Object()
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "SELECT MAX(START_DATE),MAX(END_DATE) FROM AGREE_AGREEMENT ; "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                OpenConnection()
                Me.SqlRe = Me.SqlCom.ExecuteReader() : Me.ClearCommandParameters()
                Dim Periode(1) As Object

                While Me.SqlRe.Read()
                    Periode(0) = IIf((Not IsNothing(Me.SqlRe(0)) And Not IsDBNull(Me.SqlRe(0))), SqlRe.GetDateTime(0), New DateTime(DateTime.Now.Year, 8, 1))
                    Periode(1) = IIf((Not IsNothing(Me.SqlRe(1)) And Not IsDBNull(Me.SqlRe(1))), SqlRe.GetDateTime(1), New DateTime(DateTime.Now.AddYears(1).Year, 7, 31))
                End While : Me.SqlRe.Close()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return Periode
            Catch ex As Exception
                If Not Me.SqlRe.IsClosed Then
                    Me.SqlRe.Close()
                End If
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Sub SaveData(ByVal DCPDAuto As Nufarm.Domain.DCPDAuto, ByVal Mode As NufarmBussinesRules.common.Helper.SaveMode, ByVal mustCloseConnection As Boolean)
            Dim commandInsert As SqlCommand = Nothing
            Dim commandUpdate As SqlCommand = Nothing
            Dim commandDelete As SqlCommand = Nothing

            Try
                ''INSERT/UPADate CPDHeader 
                Dim IDAppHeader As Integer = 1

                Dim rowsDiscProg() As DataRow = Nothing
                Dim rowsTermDesc() As DataRow = Nothing
                Dim tblDiscProg As DataTable = DCPDAuto.TDiscProgress
                Dim tblTermDisc As DataTable = DCPDAuto.TDiscTerms
                Dim tblProduct As DataTable = DCPDAuto.TProduct
                'For i As Integer = 0 To tbls.Length - 1
                '    Select Case tbls(i).TableName
                '        Case "DISCOUNT_PROGRESSIVE"
                '            tblDiscProg = tbls(i).Copy()
                '        Case "TERMS_OF_PO"
                '            tblTermDisc = tbls(i).Copy()
                '        Case "PRODUCT_INCLUDED"
                '            tblProduct = tbls(i).Copy()
                '    End Select
                'Next
                Dim DiscProgDesc As String = "["
                Dim TermDiscDesc As String = "["
                rowsDiscProg = tblDiscProg.Select("", "UP_TO_PCT ASC")
                rowsTermDesc = tblTermDisc.Select("", "MAX_TO_DATE ASC")
                For i As Integer = 0 To rowsDiscProg.Length - 1
                    DiscProgDesc &= String.Format("( >= {0:p}, Disc = {1:p} )", Convert.ToDecimal(rowsDiscProg(i)("UP_TO_PCT")), Convert.ToDecimal(rowsDiscProg(i)("DISCOUNT")))
                    If i < rowsDiscProg.Length - 1 Then
                        DiscProgDesc &= ", "
                    End If
                Next
                DiscProgDesc &= "]"

                For i As Integer = 0 To rowsTermDesc.Length - 1
                    If i < rowsTermDesc.Length - 1 Then
                        TermDiscDesc &= String.Format("( >={0:G} && <= {1:G}, Disc = {2:p}), ", Convert.ToInt32(rowsTermDesc(i)("MAX_TO_DATE")), Convert.ToInt32(rowsTermDesc(i + 1)("MAX_TO_DATE")), Convert.ToDecimal(rowsTermDesc(i)("DISCOUNT")))
                    Else
                        TermDiscDesc &= String.Format("( >={0:G}, Disc = {1:p})", Convert.ToInt32(rowsTermDesc(i)("MAX_TO_DATE")), Convert.ToDecimal(rowsTermDesc(i)("DISCOUNT")))
                    End If
                Next

                Dim retval As Object = Nothing
                TermDiscDesc &= "]"
                Me.OpenConnection()
                If Mode = common.Helper.SaveMode.Insert Then
                    'SA.StartDate <= @EndDate AND SA.StartDate >= @StartDate) OR(SA.EndDate >= @StartDate AND SA.EndDate <= @EndDate)
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                               " SELECT 1 WHERE EXISTS(SELECT TOP 1 IDApp FROM SALES_CPDAUTO_HEADER WHERE (START_PERIODE <= CONVERT(VARCHAR(100),@START_PERIODE,101) AND START_PERIODE =  CONVERT(VARCHAR(100),@END_PERIODE,101))  " & vbCrLf & _
                               " OR (END_PERIODE >= CONVERT(VARCHAR(100),@START_PERIODE,101) AND END_PERIODE <= CONVERT(VARCHAR(100),@END_PERIODE,101) ));"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@START_PERIODE", SqlDbType.SmallDateTime, DCPDAuto.StartPeriode)
                    Me.AddParameter("@END_PERIODE", SqlDbType.SmallDateTime, DCPDAuto.EndPeriode)
                    retval = Me.SqlCom.ExecuteScalar()
                    If Not IsNothing(retval) And Not IsDBNull(retval) Then
                        Throw New Exception("Periode CPD has exists")
                    End If
                    Me.ClearCommandParameters()
                End If
                If Mode = common.Helper.SaveMode.Insert Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " INSERT INTO SALES_CPDAUTO_HEADER (START_PERIODE, END_PERIODE, PROGRAM_DESC, DISC_PROG_DESC, DATE_TERMS_DESC, CreatedBy, CreatedDate) " & vbCrLf & _
                    " VALUES(@START_PERIODE, @END_PERIODE, @PROGRAM_DESC, @DISC_PROG_DESC, @DATE_TERMS_DESC, @CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101)) ;" & vbCrLf & _
                    " SELECT @@IDENTITY ;"
                ElseIf Mode = common.Helper.SaveMode.Update Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " IF EXISTS(SELECT IDApp FROM SALES_CPDAUTO_HEADER WHERE IDApp = @IDApp) " & vbCrLf & _
                    " UPDATE SALES_CPDAUTO_HEADER SET START_PERIODE = @START_PERIODE, END_PERIODE = @END_PERIODE, PROGRAM_DESC = @PROGRAM_DESC, DISC_PROG_DESC = @DISC_PROG_DESC, " & vbCrLf & _
                    " DATE_TERMS_DESC = @DATE_TERMS_DESC, ModifiedBy = @ModifiedBy, ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),101) WHERE IDApp = @IDApp ;"
                End If
                Me.ResetCommandText(CommandType.Text, Query)
                Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.AddParameter("@START_PERIODE", SqlDbType.SmallDateTime, DCPDAuto.StartPeriode)
                Me.AddParameter("@END_PERIODE", SqlDbType.SmallDateTime, DCPDAuto.EndPeriode)
                Me.AddParameter("@PROGRAM_DESC", SqlDbType.VarChar, DCPDAuto.DescriptionApp, 150)
                Me.AddParameter("@DISC_PROG_DESC", SqlDbType.VarChar, DiscProgDesc, 200)
                Me.AddParameter("@DATE_TERMS_DESC", SqlDbType.VarChar, TermDiscDesc, 200)
                If Mode = common.Helper.SaveMode.Insert Then
                    Me.AddParameter("@CreatedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                Else
                    Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    IDAppHeader = DCPDAuto.IDApp
                    Me.AddParameter("@IDApp", SqlDbType.SmallInt, IDAppHeader)
                End If
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Mode = common.Helper.SaveMode.Insert Then
                    IDAppHeader = Convert.ToInt32(retval)
                End If
                SqlDat = New SqlDataAdapter()
                Dim InsertedRows() As DataRow = tblProduct.Select("", "", DataViewRowState.Added)
                If InsertedRows.Length > 0 Then
                    commandInsert = SqlConn.CreateCommand()
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "INSERT INTO SALES_CPDAUTO_PRODUCT(FKCode, BRANDPACK_ID, CreatedDate, CreatedBy) " & vbCrLf & _
                            " VALUES(@FKCode,@BRANDPACK_ID,CONVERT(VARCHAR(100),GETDATE(),101),@CreatedBy) ;"
                    With commandInsert
                        .Transaction = Me.SqlTrans
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@FKCode", SqlDbType.SmallInt, 0).Value = IDAppHeader
                        .Parameters.Add("@BRANDPACK_ID", SqlDbType.VarChar, 14, "BRANDPACK_ID")
                        .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                    End With
                    SqlDat.InsertCommand = commandInsert
                    SqlDat.Update(InsertedRows)
                    SqlDat.InsertCommand = Nothing
                    commandInsert.Parameters.Clear()
                End If
                InsertedRows = tblDiscProg.Select("", "", DataViewRowState.Added)
                If InsertedRows.Length > 0 Then
                    If IsNothing(commandInsert) Then
                        commandInsert = SqlConn.CreateCommand()
                    End If
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " INSERT INTO SALES_CPDAUTO_PROG_DISC(UP_TO_PCT, DISCOUNT, FKCode, CreatedDate, CreatedBy) " & vbCrLf & _
                            " VALUES(@UP_TO_PCT, @DISCOUNT, @FKCode, CONVERT(VARCHAR(100),GETDATE(),101), @CreatedBy) ;"
                    With commandInsert
                        If IsNothing(.Transaction) Then : .Transaction = Me.SqlTrans : End If
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal, 0, "UP_TO_PCT")
                        .Parameters.Add("@DISCOUNT", SqlDbType.Decimal, 0, "DISCOUNT")
                        .Parameters.Add("@FKCode", SqlDbType.SmallInt, 0).Value = IDAppHeader
                        .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                    End With
                    SqlDat.InsertCommand = commandInsert
                    SqlDat.Update(InsertedRows)
                    commandInsert.Parameters.Clear()
                    SqlDat.InsertCommand = Nothing
                End If
                InsertedRows = tblTermDisc.Select("", "", DataViewRowState.Added)
                If InsertedRows.Length > 0 Then
                    If IsNothing(commandInsert) Then
                        commandInsert = SqlConn.CreateCommand()
                    End If
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " INSERT INTO SALES_CPDAUTO_TERMS(MAX_TO_DATE, DISCOUNT, FKCode, CreatedDate, CreatedBy) " & vbCrLf & _
                    " VALUES(@MAX_TO_DATE, @DISCOUNT, @FKCode, CONVERT(VARCHAR(100),GETDATE(),101), @CreatedBy);"
                    With commandInsert
                        If IsNothing(.Transaction) Then : .Transaction = Me.SqlTrans : End If
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@MAX_TO_DATE", SqlDbType.TinyInt, 0, "MAX_TO_DATE")
                        .Parameters.Add("@DISCOUNT", SqlDbType.Decimal, 0, "DISCOUNT")
                        .Parameters.Add("@FKCode", SqlDbType.SmallInt, 0).Value = IDAppHeader
                        .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                    End With
                    SqlDat.InsertCommand = commandInsert
                    SqlDat.Update(InsertedRows)
                    commandInsert.Parameters.Clear()
                    SqlDat.InsertCommand = Nothing
                End If
                ''TblProduct tidak ada updatean
                'Dim UpdatedRows() As DataRow = tblProduct.Select("", "", DataViewRowState.ModifiedOriginal)
                'If UpdatedRows.Length > 0 Then
                '    commandUpdate = SqlConn.CreateCommand()
                '    Query = "UPDATE SALES_CPDAUTO_PRODUCT SET "
                'End If

                Dim UpdatedRows() As DataRow = tblDiscProg.Select("", "", DataViewRowState.ModifiedOriginal)
                If UpdatedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " UPDATE SALES_CPDAUTO_PROG_DISC SET UP_TO_PCT = @UP_TO_PCT, DISCOUNT = @DISCOUNT, ModifiedBy = @ModifiedBy, ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),10) " & vbCrLf & _
                    " WHERE IDApp = @IDApp ;"
                    commandUpdate = Me.SqlConn.CreateCommand()
                    With commandUpdate
                        .Transaction = Me.SqlTrans
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@UP_TO_PCT", SqlDbType.Decimal, 0, "UP_TO_PCT")
                        .Parameters.Add("@DISCOUNT", SqlDbType.Decimal, 0, "DISCOUNT")
                        .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                        .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp").SourceVersion = DataRowVersion.Original
                    End With
                    SqlDat.UpdateCommand = commandUpdate
                    SqlDat.Update(UpdatedRows)
                    commandUpdate.Parameters.Clear()
                    SqlDat.UpdateCommand = Nothing
                End If
                UpdatedRows = tblTermDisc.Select("", "", DataViewRowState.ModifiedOriginal)
                If UpdatedRows.Length > 0 Then
                    Query = " SET NOCOUNT ON;" & vbCrLf & _
                            "UPDATE SALES_CPDAUTO_TERMS SET MAX_TO_DATE = @MAX_TO_DATE, DISCOUNT = @DISCOUNT, ModifiedDate = CONVERT(VARCHAR(100),GETDATE(),101), ModifiedBy = @ModifiedBy " & vbCrLf & _
                            " WHERE IDApp = @IDApp ;"
                    If IsNothing(commandUpdate) Then : commandUpdate = Me.SqlConn.CreateCommand() : End If
                    With commandUpdate
                        If IsNothing(.Transaction) Then : .Transaction = Me.SqlTrans : End If
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@MAX_TO_DATE", SqlDbType.Int, 0, "MAX_TO_DATE")
                        .Parameters.Add("@DISCOUNT", SqlDbType.Decimal, 0, "DISCOUNT")
                        .Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                        .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp").SourceVersion = DataRowVersion.Original
                    End With
                    SqlDat.UpdateCommand = commandUpdate
                    SqlDat.Update(UpdatedRows)
                    commandUpdate.Parameters.Clear()
                    SqlDat.UpdateCommand = Nothing
                End If
                ''delete rows
                Dim DeletedRows() As DataRow = tblProduct.Select("", "", DataViewRowState.Deleted)
                If DeletedRows.Length > 0 Then
                    Query = " SET NOCOUNT ON; " & vbCrLf & _
                    "DELETE FROM SALES_CPDAUTO_PRODUCT WHERE IDApp = @IDApp ;"
                    commandDelete = Me.SqlConn.CreateCommand()
                    With commandDelete
                        .Transaction = Me.SqlTrans
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp").SourceVersion = DataRowVersion.Original
                    End With
                    SqlDat.DeleteCommand = commandDelete
                    SqlDat.Update(DeletedRows)
                    commandDelete.Parameters.Clear()
                    SqlDat.DeleteCommand = Nothing
                End If
                DeletedRows = tblDiscProg.Select("", "", DataViewRowState.Deleted)
                If DeletedRows.Length > 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " DELETE FROM SALES_CPDAUTO_PROG_DISC WHERE IDApp = @IDApp ;"
                    If IsNothing(commandDelete) Then : commandDelete = Me.SqlConn.CreateCommand() : End If
                    With commandDelete
                        If IsNothing(.Transaction) Then : .Transaction = Me.SqlTrans : End If
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp").SourceVersion = DataRowVersion.Original
                    End With
                    SqlDat.DeleteCommand = commandDelete
                    SqlDat.Update(DeletedRows)
                    commandDelete.Parameters.Clear()
                    SqlDat.DeleteCommand = Nothing
                End If
                DeletedRows = tblTermDisc.Select("", "", DataViewRowState.Deleted)
                If DeletedRows.Length > 0 Then
                    Query = " SET NOCOUNT ON; " & vbCrLf & _
                            " DELETE FROM SALES_CPDAUTO_TERMS WHERE IDApp = @IDApp ;"
                    If IsNothing(commandDelete) Then : commandDelete = Me.SqlConn.CreateCommand() : End If
                    With commandDelete
                        If IsNothing(.Transaction) Then : .Transaction = Me.SqlTrans : End If
                        .CommandType = CommandType.Text
                        .CommandText = Query
                        .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp").SourceVersion = DataRowVersion.Original
                    End With
                    SqlDat.DeleteCommand = commandDelete
                    SqlDat.Update(DeletedRows)
                    commandDelete.Parameters.Clear()
                    SqlDat.DeleteCommand = Nothing
                End If
                Me.CommiteTransaction()
                Me.ClearCommandParameters()
                tblDiscProg.AcceptChanges() : tblProduct.AcceptChanges() : tblTermDisc.AcceptChanges()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.ClearCommandParameters()
                Me.CloseConnection()
                Me.ClearCommandParameters()
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
        End Sub
        Public Sub DeleteCPDAuto(ByVal IDApp As Integer, ByVal MustCloseConnection As Boolean)
            Try
                'delete data SALES_CPDAUTO_PRODUCT
                'delete data SALES_CPDAUTO_PROG_DISC
                'delete data SALES_CPDAUTO_TERMS
                'delete data SALES_CPDAUTO_HEADER
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " DELETE FROM SALES_CPDAUTO_PRODUCT WHERE FKCode = @IDApp ;" & vbCrLf & _
                " DELETE FROM SALES_CPDAUTO_PROG_DISC WHERE FKCode = @IDApp ;" & vbCrLf & _
                " DELETE FROM SALES_CPDAUTO_TERMS WHERE FKCode = @IDApp ;" & vbCrLf & _
                " DELETE FROM SALES_CPDAUTO_HEADER WHERE IDApp = @IDApp ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                AddParameter("@IDApp", SqlDbType.SmallInt, IDApp)
                OpenConnection() : BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CommiteTransaction()
                If MustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.ClearCommandParameters() : Me.CloseConnection() : Throw ex
            End Try
        End Sub
        Public Function getActiveProduct(ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT DISTINCT ABI.BRANDPACK_ID,BB.BRANDPACK_NAME FROM BRND_BRANDPACK BB INNER JOIN AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                    " ON ABI.BRANDPACK_ID = BB.BRANDPACK_ID INNER JOIN AGREE_AGREEMENT AA ON AA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                    " WHERE AA.START_DATE <= @GETDATE AND AA.END_DATE >= @GETDATE "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                OpenConnection()
                Dim tblProduct As New DataTable("PRODUCT")
                Me.setDataAdapter(SqlCom).Fill(tblProduct)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return tblProduct
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function getDS(ByVal IDApp As Integer, ByVal mustCloseConnection As Boolean) As DataSet
            Try
                'get Table CPDProduct
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "SELECT BB.BRANDPACK_NAME,SCP.*  FROM SALES_CPDAUTO_PRODUCT SCP INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = SCP.BRANDPACK_ID WHERE SCP.FKCode = @IDApp ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@IDApp", SqlDbType.SmallInt, IDApp)
                OpenConnection()
                Dim tblProduct As New DataTable("PRODUCT_INCLUDED")
                setDataAdapter(Me.SqlCom).Fill(tblProduct)

                'get discount progressive
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "SELECT * FROM SALES_CPDAUTO_PROG_DISC WHERE FKCode = @IDApp ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Dim tblProgDisc As New DataTable("DISCOUNT_PROGRESSIVE")
                setDataAdapter(Me.SqlCom).Fill(tblProgDisc)
                'get Terms Of Disc
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT * FROM SALES_CPDAUTO_TERMS WHERE FKCode = @IDApp; "
                Me.ResetCommandText(CommandType.Text, Query)
                Dim tblTerms As New DataTable("TERMS_OF_PO")
                setDataAdapter(Me.SqlCom).Fill(tblTerms)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Dim DS As New DataSet("DSCPDAuto")
                DS.Clear()
                DS.Tables.AddRange(New DataTable() {tblProduct, tblProgDisc, tblTerms})
                DS.AcceptChanges()
                Return DS
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function PopulateQuery(ByVal SearchBy As String, ByVal value As Object, ByVal PageIndex As Integer, ByVal PageSize As Integer, ByRef RowCount As Integer, _
           ByVal Criteria As common.Helper.CriteriaSearch, ByVal DataType As common.Helper.DataTypes) As DataView
            Try
                Dim ResolvedCriteria As String = common.CommonClass.ResolveCriteria(Criteria, DataType, value)
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT TOP " & PageSize.ToString() & " * FROM(SELECT ROW_NUMBER() OVER(ORDER BY IDApp ASC) AS ROW_NUM,IDApp, START_PERIODE,END_PERIODE,PROGRAM_DESC,DISC_PROG_DESC AS DISCOUNT_PROGRESSIVE,DATE_TERMS_DESC AS [TERMS OF PO], " & vbCrLf & _
                        " CreatedDate,CreatedBy FROM SALES_CPDAUTO_HEADER  " & vbCrLf & _
                        " WHERE (" & SearchBy & " " & ResolvedCriteria & " ) " & vbCrLf
                Query &= ")Result WHERE ROW_NUM >= " & ((PageSize * (PageIndex - 1)) + 1).ToString() & " AND ROW_NUM <= " & (PageSize * PageIndex).ToString()
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblCPD As New DataTable("CPD AUTO")
                OpenConnection()
                setDataAdapter(Me.SqlCom).Fill(tblCPD) : Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT COUNT(ROW_NUM) FROM(SELECT ROW_NUMBER() OVER(ORDER BY " & SearchBy & " ASC)AS ROW_NUM FROM SALES_CPDAUTO_HEADER WHERE (" & SearchBy & " " & ResolvedCriteria & " ))Result "
                ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                RowCount = CInt(Me.SqlCom.ExecuteScalar())
                Me.CloseConnection() : Me.ClearCommandParameters()
                Return tblCPD.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function hasGeneratedDisc(ByVal BrandPackID As String, ByVal FKCode As Integer, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                If Not String.IsNullOrEmpty(BrandPackID) Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT 1 WHERE EXISTS(SELECT TOP 1 MDH.MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY MDH WHERE MDH.FKCodeCPDAuto = @FKCodeCPDAuto AND EXISTS( " & vbCrLf & _
                            "(SELECT TOP 1 OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE PO_BRANDPACK_ID = (SELECT TOP 1 PO_BRANDPACK_ID FROM ORDR_PO_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID) AND OA_BRANDPACK_ID = MDH.OA_BRANDPACK_ID))) ;"
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT 1 WHERE EXISTS(SELECT MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE FKCodeCPDAuto = @FKCodeCPDAuto) ;"
                End If

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                If Not String.IsNullOrEmpty(BrandPackID) Then
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BrandPackID, 14)
                End If
                Me.AddParameter("@FKCodeCPDAuto", SqlDbType.SmallInt, FKCode)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return (CInt(retval) > 0)
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class

End Namespace
