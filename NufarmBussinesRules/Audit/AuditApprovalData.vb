Imports System.Data.SqlClient
Namespace Audit
    Public Class AuditApprovalData
        Inherits MasterData
        Public Sub New()
            MyBase.New()
        End Sub
        Dim Query As String = ""
        Public Function getApprovalAdjustment(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal OnlyApproved As Boolean, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                If OnlyApproved Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                           " SELECT AD.IDApp,ISNULL(AD.IsApproved,0)AS IsApproved, AD.CodeApp,TER.TERRITORY_AREA,DR.DISTRIBUTOR_NAME,BR.BRANDPACK_NAME,AD.QTY AS QUANTITY,ADJUSTMENT_FOR = CASE WHEN (AD.TypeApp = 'DPD') THEN 'DPD' ELSE 'RETAILER PROGRAM' END," & vbCrLf & _
                           " AD.START_DATE AS START_PERIODE, AD.END_DATE AS END_PERIODE,AD.NameApp AS ADJ_DESCRIPTION,AD.LEFT_QTY,AD.RELEASE_QTY,AD.IsGroup,AD.CreatedBy AS CREATE_BY,AD.CreatedDate AS CREATE_DATE,AD.ApprovedBy,AD.ApprovedDate,AD.ApprovedDesc " & vbCrLf & _
                           " FROM ADJUSTMENT_DPD AD INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = AD.DISTRIBUTOR_ID INNER JOIN BRND_BRANDPACK BR ON BR.BRANDPACK_ID = AD.BRANDPACK_ID INNER JOIN TERRITORY TER ON TER.TERRITORY_ID = DR.TERRITORY_ID WHERE AD.START_DATE >= @StartDate AND AD.END_DATE <= @EndDate AND AD.IsApproved = 1 " & vbCrLf & _
                           " AND AD.CreatedBy IN(SELECT [USER_ID] FROM SYST_USERNAME WHERE INACTIVE = 0 AND JOB_LEVEL <= @Level) ORDER BY AD.CreatedDate DESC  ;"
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT AD.IDApp,ISNULL(AD.IsApproved,0)AS IsApproved, AD.CodeApp,TER.TERRITORY_AREA,DR.DISTRIBUTOR_NAME,BR.BRANDPACK_NAME,AD.QTY AS QUANTITY,ADJUSTMENT_FOR = CASE WHEN (AD.TypeApp = 'DPD') THEN 'DPD' ELSE 'RETAILER PROGRAM' END," & vbCrLf & _
                            " AD.START_DATE AS START_PERIODE, AD.END_DATE AS END_PERIODE,AD.NameApp AS ADJ_DESCRIPTION,AD.LEFT_QTY,AD.RELEASE_QTY,AD.IsGroup,AD.CreatedBy AS CREATE_BY,AD.CreatedDate AS CREATE_DATE,AD.ApprovedBy,AD.ApprovedDate,AD.ApprovedDesc " & vbCrLf & _
                            " FROM ADJUSTMENT_DPD AD INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = AD.DISTRIBUTOR_ID INNER JOIN BRND_BRANDPACK BR ON BR.BRANDPACK_ID = AD.BRANDPACK_ID " & vbCrLf & _
                            " INNER JOIN TERRITORY TER ON TER.TERRITORY_ID = DR.TERRITORY_ID WHERE AD.START_DATE >= @StartDate AND AD.END_DATE <= @EndDate AND (AD.IsApproved IS NULL OR AD.IsApproved = 0) " & vbCrLf & _
                            " AND AD.CreatedBy IN(SELECT [USER_ID] FROM SYST_USERNAME WHERE INACTIVE = 0 AND JOB_LEVEL < @Level) ORDER BY AD.CreatedDate DESC  ;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Me.AddParameter("@Level", SqlDbType.Int, NufarmBussinesRules.User.UserLogin.Level)
                Me.OpenConnection()
                Dim dt As New DataTable("ADJUSTMENT_DPD")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub UpdateApproval(ByRef tblToUpdate As DataTable, ByVal PrimaryKeyValue As Object, ByVal mustCloseConnection As Boolean)
            Try
                'tblToUpdate.AcceptChanges()
                Me.GetConnection() : OpenConnection()
                Dim commandUpdate As SqlCommand = Me.SqlConn.CreateCommand()
                Dim UpdatedRows() As DataRow = Nothing
                With commandUpdate
                    .CommandType = CommandType.Text
                    .Transaction = Me.SqlTrans
                    .Parameters.Add("@IsApproved", SqlDbType.Bit, 0, "IsApproved")
                    .Parameters.Add("@ApprovedBy", SqlDbType.VarChar, 100, "ApprovedBy")
                    .Parameters.Add("@ApprovedDate", SqlDbType.SmallDateTime, 0, "ApprovedDate")
                    .Parameters.Add("@ApprovedDesc", SqlDbType.VarChar, 200, "ApprovedDesc")
                    Select Case tblToUpdate.TableName
                        Case "MRKT_BRANDPACK_DISTRIBUTOR"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE MRKT_BRANDPACK_DISTRIBUTOR SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc  " & vbCrLf & _
                                    " WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID ;"
                            .Parameters.Add("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, 40, "PROG_BRANDPACK_DIST_ID")
                            UpdatedRows = tblToUpdate.Select("PROG_BRANDPACK_DIST_ID = '" & PrimaryKeyValue & "'")
                          
                        Case "AGREE_BRAND_INCLUDE"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE AGREE_BRAND_INCLUDE SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    " WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID ;"
                            .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32, "AGREE_BRAND_ID")
                            UpdatedRows = tblToUpdate.Select("AGREE_BRAND_ID = '" & PrimaryKeyValue.ToString() & "'")
                        Case "BRND_AVGPRICE"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE BRND_AVGPRICE SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    " WHERE IDApp = @IDApp"
                            .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            UpdatedRows = tblToUpdate.Select("IDApp = " & PrimaryKeyValue.ToString())
                        Case "BRND_PRICE_HISTORY"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE BRND_PRICE_HISTORY SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    " WHERE IDApp = @IDApp "
                            .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            UpdatedRows = tblToUpdate.Select("IDApp = " & PrimaryKeyValue.ToString())
                        Case "ACCRUED_DETAIL"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    "UPDATE ACCRUED_DETAIL SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    "WHERE ACHIEVEMENT_BRANDPACK_ID = @ACHIEVEMENT_BRANDPACK_ID ;"
                            .Parameters.Add("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, 100, "ACHIEVEMENT_BRANDPACK_ID")
                            UpdatedRows = tblToUpdate.Select("ACHIEVEMENT_BRANDPACK_ID  = '" & PrimaryKeyValue.ToString() & "'")
                        Case "ADJUSTMENT_DPD"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    "UPDATE ADJUSTMENT_DPD SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    " WHERE IDApp = @IDApp ;"
                            .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            UpdatedRows = tblToUpdate.Select("IDApp = " & PrimaryKeyValue.ToString())
                        Case "DIST_PLANT_PRICE"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE DIST_PLANT_PRICE SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    " WHERE IDApp = @IDApp ;"
                            .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            UpdatedRows = tblToUpdate.Select("IDApp = " & PrimaryKeyValue.ToString())
                        Case "CPD_AUTO"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE SALES_CPDAUTO_HEADER SET IsApproved = @IsApproved, ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc WHERE IDApp = @IDApp ;"
                            .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                            UpdatedRows = tblToUpdate.Select("IDApp = " & PrimaryKeyValue.ToString())
                    End Select
                    .CommandText = Query
                    If UpdatedRows.Length > 0 Then
                        UpdatedRows(0).SetModified()
                    End If
                    Me.SqlDat = New SqlDataAdapter()
                    SqlDat.UpdateCommand = commandUpdate
                    Me.SqlDat.Update(UpdatedRows)
                    commandUpdate.Parameters.Clear()
                    If mustCloseConnection Then
                        Me.CloseConnection()
                    End If
                End With
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Sub SaveData(ByVal tblToUpdate As DataTable, ByVal mustCloseConnection As Boolean)
            Try
                Dim UpdatedRows() As DataRow = tblToUpdate.Select("")
                If UpdatedRows.Length <= 0 Then : Return : End If
                Me.GetConnection() : OpenConnection() : BeginTransaction()
                Dim commandUpdate As SqlCommand = Me.SqlConn.CreateCommand()

                With commandUpdate
                    .CommandType = CommandType.Text
                    .Transaction = Me.SqlTrans
                    .Parameters.Add("@IsApproved", SqlDbType.Bit, 0, "IsApproved")
                    .Parameters.Add("@ApprovedBy", SqlDbType.VarChar, 100, "ApprovedBy")
                    .Parameters.Add("@ApprovedDate", SqlDbType.SmallDateTime, 0, "ApprovedDate")
                    .Parameters.Add("@ApprovedDesc", SqlDbType.VarChar, 200, "ApprovedDesc")
                    Select Case tblToUpdate.TableName
                        Case "MRKT_BRANDPACK_DISTRIBUTOR"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE MRKT_BRANDPACK_DISTRIBUTOR SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc  " & vbCrLf & _
                                    " WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID ;"
                            .Parameters.Add("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, 40, "PROG_BRANDPACK_DIST_ID")
                        Case "AGREE_BRAND_INCLUDE"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE AGREE_BRAND_INCLUDE SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    " WHERE AGREE_BRAND_ID = @AGREE_BRAND_ID ;"
                            .Parameters.Add("@AGREE_BRAND_ID", SqlDbType.VarChar, 32, "AGREE_BRAND_ID")
                        Case "BRND_AVGPRICE"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE BRND_AVGPRICE SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    " WHERE IDApp = @IDApp"
                            .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                        Case "BRND_PRICE_HISTORY"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE BRND_PRICE_HISTORY SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    " WHERE IDApp = @IDApp "
                            .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                        Case "ACCRUED_DETAIL"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    "UPDATE ACCRUED_DETAIL SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    "WHERE ACHIEVEMENT_BRANDPACK_ID = @ACHIEVEMENT_BRANDPACK_ID ;"
                            .Parameters.Add("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, 100, "ACHIEVEMENT_BRANDPACK_ID")
                        Case "ADJUSTMENT_DPD"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    "UPDATE ADJUSTMENT_DPD SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    " WHERE IDApp = @IDApp ;"
                            .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                        Case "DIST_PLANT_PRICE"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE DIST_PLANT_PRICE SET IsApproved = @IsApproved,ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc " & vbCrLf & _
                                    " WHERE IDApp = @IDApp ;"
                            .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                        Case "CPD_AUTO"
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " UPDATE SALES_CPDAUTO_HEADER SET IsApproved = @IsApproved, ApprovedBy = @ApprovedBy,ApprovedDate = @ApprovedDate,ApprovedDesc = @ApprovedDesc WHERE IDApp = @IDApp ;"
                            .Parameters.Add("@IDApp", SqlDbType.Int, 0, "IDApp")
                    End Select
                    .CommandText = Query
                End With
                Me.SqlDat = New SqlDataAdapter()
                SqlDat.UpdateCommand = commandUpdate
                Me.SqlDat.Update(UpdatedRows)
                Me.CommiteTransaction() : commandUpdate.Parameters.Clear()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function getApprovalPriceFM(ByVal OnlyApproved As Boolean, ByVal mustCloseConnection As Boolean) As DataTable
            Try

                If OnlyApproved Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                           " SELECT BPP.IDApp,ISNULL(BPP.IsApproved,0)AS IsApproved, BPP.BRANDPACK_ID, BP.BRANDPACK_NAME, BPP.PRICE, BPP.START_DATE,BPP.CREATE_BY,BPP.CREATE_DATE,BPP.ApprovedBy,BPP.ApprovedDate,BPP.ApprovedDesc " & vbCrLf & _
                           " FROM BRND_PRICE_HISTORY BPP INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = BPP.BRANDPACK_ID" & vbCrLf & _
                           " WHERE YEAR(BPP.START_DATE) >= (YEAR(GETDATE())- 3) AND BPP.IsApproved = 1 AND BPP.CREATE_BY IN(SELECT [USER_ID] FROM SYST_USERNAME WHERE INACTIVE = 0 AND JOB_LEVEL <= @Level) ;"
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT BPP.IDApp,ISNULL(BPP.IsApproved,0)AS IsApproved, BPP.BRANDPACK_ID, BP.BRANDPACK_NAME, BPP.PRICE, BPP.START_DATE,BPP.CREATE_BY,BPP.CREATE_DATE,BPP.ApprovedBy,BPP.ApprovedDate,BPP.ApprovedDesc " & vbCrLf & _
                            " FROM BRND_PRICE_HISTORY BPP INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = BPP.BRANDPACK_ID" & vbCrLf & _
                            " WHERE YEAR(BPP.START_DATE) >= (YEAR(GETDATE())- 3) AND ((BPP.IsApproved IS NULL) OR(BPP.IsApproved = 0)) AND BPP.CREATE_BY IN(SELECT [USER_ID] FROM SYST_USERNAME WHERE INACTIVE = 0 AND JOB_LEVEL < @Level) ;"
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@Level", SqlDbType.Int, NufarmBussinesRules.User.UserLogin.Level)
                Me.OpenConnection()
                Dim dt As New DataTable("BRND_PRICE_HISTORY")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getApprovalDPD(ByVal Flag As String, ByVal StartDate As DateTime, ByVal endDate As DateTime, ByVal OnlyApproved As Boolean, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Approval_Audit_DPD", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Approval_Audit_DPD")
                End If
                Me.AddParameter("@OnlyApproved", SqlDbType.Bit, OnlyApproved)
                Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2) ' VARCHAR(2),
                Me.AddParameter("@START_PERIODE", SqlDbType.SmallDateTime, StartDate) ' SMALLDATETIME,
                Me.AddParameter("@END_PERIODE", SqlDbType.SmallDateTime, endDate) ' SMALLDATETIME
                Me.AddParameter("@Level", SqlDbType.Int, NufarmBussinesRules.User.UserLogin.Level)
                Me.OpenConnection()
                Dim dt As New DataTable("ACCRUED_DETAIL")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        'Public Function getApprovalAVPrice(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal mustCloseConnection As Boolean) As DataTable
        '    Try
        '        Query = "SET NOCOUNT ON; " & vbCrLf & _
        '                " SELECT AVG.IDApp,AVG.IsApproved, AVG.BRAND_ID,BR.BRAND_NAME,AVG.AVGPRICE AS AVGPRICE_FM,AVG.AVGPRICE_PL,AVG.START_PERIODE,AVG.CreatedDate AS CREATE_DATE " & vbCrLf & _
        '                " ,AVG.ApprovedBy,AVG.ApprovedDate FROM BRND_AVGPRICE AVG INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = AVG.BRAND_ID " & vbCrLf & _
        '                " WHERE AVG.START_PERIODE >= @StartDate AND AVG.START_PERIODE <= @EndDate ;"
        '        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
        '        Else : Me.ResetCommandText(CommandType.Text, Query)
        '        End If
        '        Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
        '        Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)

        '        Me.OpenConnection()
        '        Dim dt As New DataTable("AVERAGE_PRICE_APPROVAL")
        '        dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
        '        Me.ClearCommandParameters()
        '        If mustCloseConnection Then : Me.CloseConnection() : End If
        '        Return dt
        '    Catch ex As Exception
        '        Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
        '    End Try
        'End Function
        Public Function getApprovalSalesProgram(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal OnlyApproved As Boolean, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Approval_Audit_SalesProgram", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Approval_Audit_SalesProgram")
                End If
                Me.AddParameter("@START_PERIODE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_PERIODE", SqlDbType.SmallDateTime, EndDate)
                Me.AddParameter("@Level", SqlDbType.Int, NufarmBussinesRules.User.UserLogin.Level)
                Me.AddParameter("@OnlyApproved", SqlDbType.Bit, OnlyApproved)
                Me.OpenConnection()
                Dim dt As New DataTable("MRKT_BRANDPACK_DISTRIBUTOR")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getApprovalAVGPrice(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal OnlyApproved As Boolean, ByVal mustCloseConnection As Boolean) As DataTable
            Try

                If OnlyApproved Then
                    Query = " SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT AVG.IDApp, ISNULL(AVG.IsApproved,0)AS IsApproved,AVG.BRAND_ID,BR.BRAND_NAME,AVG.AVGPRICE AS AVGPRICE_FM,AVG.AVGPRICE_PL,AVG.START_PERIODE,AVG.CreatedDate AS CREATE_DATE,AVG.CreatedBy AS CREATE_BY,AVG.ApprovedBy,AVG.ApprovedDate,AVG.ApprovedDesc " & vbCrLf & _
                            " FROM BRND_AVGPRICE AVG INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = AVG.BRAND_ID " & vbCrLf & _
                            " WHERE IsApproved = 1  AND (AVG.START_PERIODE >= @StartDate AND AVG.START_PERIODE <= @EndDate) AND AVG.CreatedBy IN(SELECT [USER_ID] FROM SYST_USERNAME WHERE INACTIVE = 0 AND JOB_LEVEL <= @Level) ;"
                Else
                    Query = " SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT AVG.IDApp, ISNULL(AVG.IsApproved,0)AS IsApproved,AVG.BRAND_ID,BR.BRAND_NAME,AVG.AVGPRICE AS AVGPRICE_FM,AVG.AVGPRICE_PL,AVG.START_PERIODE,AVG.CreatedDate AS CREATE_DATE,AVG.CreatedBy AS CREATE_BY,AVG.ApprovedBy,AVG.ApprovedDate,AVG.ApprovedDesc " & vbCrLf & _
                            " FROM BRND_AVGPRICE AVG INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = AVG.BRAND_ID " & vbCrLf & _
                            " WHERE (AVG.IsApproved IS NULL OR AVG.IsApproved = 0) AND (AVG.START_PERIODE >= @StartDate AND AVG.START_PERIODE <= @EndDate) AND AVG.CreatedBy IN(SELECT [USER_ID] FROM SYST_USERNAME WHERE INACTIVE = 0 AND JOB_LEVEL < @Level) ;"
                End If

                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Me.AddParameter("@Level", SqlDbType.Int, NufarmBussinesRules.User.UserLogin.Level)

                Me.OpenConnection()
                Dim dt As New DataTable("BRND_AVGPRICE")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getApprovalTargetPKD(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal OnlyApproved As Boolean, ByVal mustCloseConnection As Boolean) As DataTable
            'Usp_Get_Approval_Audit_TargetPKD()
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Approval_Audit_TargetPKD", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Approval_Audit_TargetPKD")
                End If
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Me.AddParameter("@Level", SqlDbType.Int, NufarmBussinesRules.User.UserLogin.Level)
                Me.AddParameter("@OnlyApproved", SqlDbType.Bit, OnlyApproved)
                Me.OpenConnection()
                Dim dt As New DataTable("AGREE_BRAND_INCLUDE")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getApprovalCPDAuto(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal OnlyApproved As Boolean, ByVal MustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT [IDApp],ISNULL([IsApproved],0)AS IsApproved,[START_PERIODE],[END_PERIODE],[PROGRAM_DESC],[DISC_PROG_DESC],[DATE_TERMS_DESC],[CreatedBy],[CreatedDate],[ApprovedBy],[ApprovedDate],ApprovedDesc " & vbCrLf & _
                        " FROM [Nufarm].[dbo].[SALES_CPDAUTO_HEADER] "
                If OnlyApproved Then
                    Query &= " WHERE START_PERIODE >= @StartDate AND END_PERIODE <= @EndDate AND IsApproved = 1 AND CreatedBy IN(SELECT [USER_ID] FROM SYST_USERNAME WHERE INACTIVE = 0 AND JOB_LEVEL <= @Level)  "
                Else
                    Query &= " WHERE START_PERIODE >= @StartDate AND END_PERIODE <= @EndDate AND (IsApproved IS NULL OR IsApproved = 0) AND CreatedBy IN(SELECT [USER_ID] FROM SYST_USERNAME WHERE INACTIVE = 0 AND JOB_LEVEL < @Level)  "
                End If
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Me.AddParameter("@Level", SqlDbType.Int, NufarmBussinesRules.User.UserLogin.Level)

                Me.OpenConnection()
                Dim dt As New DataTable("CPD_AUTO")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If MustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getApprovalPricePL(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal OnlyApproved As Boolean, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Approval_Audit_Price_PL", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Approval_Audit_Price_PL")
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Me.AddParameter("@Level", SqlDbType.Int, NufarmBussinesRules.User.UserLogin.Level)
                Me.AddParameter("@OnlyApproved", SqlDbType.Bit, OnlyApproved)
                Me.OpenConnection()
                Dim dt As New DataTable("DIST_PLANT_PRICE")
                dt.Clear() : setDataAdapter(Me.SqlCom).Fill(dt)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
    End Class
End Namespace

