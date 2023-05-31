Imports System.Data.SqlClient
Imports NufarmBussinesRules.SharedClass
Namespace PurchaseOrder
    Public Class Invoice
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet : Implements IDisposable
        Private Query As String = ""
        Public Sub New()
            MyBase.New()
            '--==============UNCOMMENT THIS AFTER NEEDED ================
            'DBInvoiceTo = CurrentInvToUse.NI109
        End Sub
        Public Function hasReservedInvoice(ByRef userName As String) As Boolean
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        "  IF EXISTS(SELECT [NAME] FROM tempdb.sys.columns WHERE [NAME] = 'UserName' AND object_id=OBJECT_ID('tempdb..##T_START_DATE_" & Me.ComputerName & "')) " & vbCrLf & _
                        "   BEGIN " & vbCrLf & _
                        "       SELECT [UserName] FROM tempdb..##T_START_DATE_" & Me.ComputerName & " WHERE [UserName] != @UserName  ;" & vbCrLf & _
                        "   END " & vbCrLf & _
                        " END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@UserName", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    If Not retval.ToString() Is String.Empty Then
                        userName = retval.ToString() : Return True
                    End If
                End If
                Return False
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        'Public Sub CreateTempTable()
        '    Try
        '        Query = "SET NOCOUNT ON;" & vbCrLf & _
        '               "IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
        '               " BEGIN " & vbCrLf & _
        '               " EXEC Usp_Create_Temp_Date_Invoice @I_START_DATE = @START_DATE,@I_END_DATE = @END_DATE; " & vbCrLf & _
        '               " END " & vbCrLf & _
        '               " IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_BRANDPACK' AND TYPE = 'U') " & vbCrLf & _
        '               " BEGIN  EXEC Usp_Create_Temp_Table_BrandPack; END "
        '        Me.CreateCommandSql("", Query)
        '        Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
        '        Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
        '        Me.OpenConnection()
        '        Me.SqlCom.ExecuteScalar()
        '        Me.CloseConnection()
        '        Me.ClearCommandParameters()
        '    Catch ex As Exception
        '        Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
        '    End Try
        'End Sub
        Public Function GetDistributor(ByVal SearchString As String) As DataView
            Try
                If (String.IsNullOrEmpty(SearchString)) Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                           " SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR " & vbCrLf & _
                           " WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID); "
                Else
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                           " SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR " & vbCrLf & _
                           " WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                           " AND DISTRIBUTOR_NAME LIKE '%" & SearchString & "%';"
                End If

                Dim tblDist As New DataTable("T_Distributor") : tblDist.Clear()
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom : End If : Me.OpenConnection()
                SqlDat.Fill(tblDist) : Me.ClearCommandParameters() : Return tblDist.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        ''' <summary>
        ''' prepare startDate and end_date processing
        ''' </summary>
        ''' <param name="StartDate">datetime data</param>
        ''' <param name="EndDate">datetime data</param>
        ''' <param name="StrDecStartDate">varchar in server = numeric</param>
        ''' <param name="strDecEndDate">varchar in server = numeric</param>
        ''' <remarks></remarks>
        Public Sub CreateTempTable(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal StrDecStartDate As String, ByVal strDecEndDate As String)
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                   "SELECT 1 WHERE EXISTS(SELECT NAME FROM [tempdb].[sys].[objects]  WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U');"
            If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            Else : Me.CreateCommandSql("sp_executesql", "") : End If
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            Me.OpenConnection()
            Dim retval As Object = Me.SqlCom.ExecuteScalar()
            Me.ClearCommandParameters() : Dim BTempStartDate As Boolean = False
            If Not IsNothing(retval) And Not IsDBNull(retval) Then
                If CInt(retval) > 0 Then
                    BTempStartDate = True
                End If
            End If
            Dim StrStartDate As String = "", strEndDate As String = ""
            If Not BTempStartDate Then
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                         "SELECT START_DATE = @START_DATE,END_DATE = @END_DATE,UserName = @UserName INTO  ##T_START_DATE_" & Me.ComputerName & " ;"
                Me.ResetCommandText(CommandType.Text, Query)
                StrStartDate = Month(StartDate).ToString() + "/" + Day(StartDate).ToString() + "/" + Year(StartDate).ToString()
                strEndDate = Month(EndDate).ToString() + "/" + Day(EndDate).ToString() + "/" + Year(EndDate).ToString()

                Me.AddParameter("@START_DATE", SqlDbType.VarChar, StrStartDate, 20)
                Me.AddParameter("@END_DATE", SqlDbType.VarChar, strEndDate, 20)
                Me.AddParameter("@UserName", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            End If

            'Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            Dim retvalStartDate As String = "", retvalEndDate As String = ""
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                    "SELECT START_DATE,END_DATE FROM tempdb..##T_START_DATE_" & Me.ComputerName & " ;"
            Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read() : retvalStartDate = SqlRe.GetString(0) : retvalEndDate = SqlRe.GetString(1) : End While
            Me.SqlRe.Close() : Me.ClearCommandParameters()
            Dim StoredProcNI87 As String = "Usp_Create_Temp_Invoice_Table", StoredProcNI109 = "Usp_Create_Temp_Invoice_Table_NI109"
            Dim StoredProcToUse As String = StoredProcNI87
            If DBInvoiceTo = CurrentInvToUse.NI109 Then
                StoredProcToUse = StoredProcNI109
            End If
            If Not ((StrStartDate.Equals(retvalStartDate)) Or (strEndDate.Equals(retvalEndDate))) Then
                'bikin baru
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON;" & vbCrLf & _
                        "IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN UPDATE tempdb..##T_START_DATE_" & Me.ComputerName & " SET START_DATE = @D_START_DATE,END_DATE = @D_END_DATE;  END " & vbCrLf & _
                        " ELSE " & vbCrLf & _
                        " BEGIN SELECT START_DATE = @D_START_DATE,END_DATE = @D_END_DATE,UserName = @UserName INTO  ##T_START_DATE_" & Me.ComputerName & " ; END " & vbCrLf & _
                        " IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_SELECT_INVOICE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN  DROP TABLE tempdb..##T_SELECT_INVOICE_" & Me.ComputerName & " ; END " & vbCrLf & _
                        " EXEC " & StoredProcToUse & " @DEC_START_DATE = @D_START_DATE,@DEC_END_DATE = @D_END_DATE,@COMPUTERNAME = @C_NAME ; "
            Else
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON;" & vbCrLf & _
                        "IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_SELECT_INVOICE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN  EXEC " & StoredProcToUse & " @DEC_START_DATE = @D_START_DATE,@DEC_END_DATE = @D_END_DATE,@COMPUTERNAME = @C_NAME ; END " '& vbCrLf & _
                '" IF NOT EXISTS(SELECT NAME FROM tempdb..SYSOBJECTS WHERE NAME = '##T_BRANDPACK' AND TYPE = 'U') " & vbCrLf & _
                '" BEGIN  EXEC Usp_Create_Temp_Table_BrandPack; END "
            End If
            Me.ResetCommandText(CommandType.Text, Query)
            'Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            'Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
            Me.AddParameter("@UserName", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)
            Me.AddParameter("@D_START_DATE", SqlDbType.VarChar, StrDecStartDate)
            Me.AddParameter("@D_END_DATE", SqlDbType.VarChar, strDecEndDate)
            Me.AddParameter("@C_NAME", SqlDbType.VarChar, Me.ComputerName, 100)
            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
        End Sub
        Public Function GetInvoice(ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal isRefDateByPO As Boolean, ByVal IsDateChanged As Boolean, ByRef t2 As DataTable, Optional ByVal DistributorID As String = "") As DataView
            Try
                Dim LeadTimeStart As DateTime = StartDate.AddMonths(-6)
                Dim LeadTimeEnd As DateTime = EndDate.AddMonths(6)
                Dim strDecEndDate As String = common.CommonClass.getNumericFromDate(EndDate)
                Dim strDecStartDate As String = ""
                If Not isRefDateByPO Then
                    strDecStartDate = common.CommonClass.getNumericFromDate(StartDate.AddMonths(-3))
                    Me.CreateTempTable(StartDate.AddMonths(-3), EndDate, strDecStartDate, strDecEndDate)
                Else
                    strDecStartDate = common.CommonClass.getNumericFromDate(StartDate)
                    Me.CreateTempTable(StartDate, EndDate, strDecStartDate, strDecEndDate)
                End If
                'AMBIL DATA LEADTIMEENDATE UNTUK perbandingan di po apakah jadi nol/gaknya
                'Dim leadTimemin1 As DateTime = LeadTimeStart.AddDays(-1)
                'If IsDateChanged Then
                '    Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ;" & vbCrLf & _
                '              "IF EXISTS(SELECT [NAME] FROM tempdb.DBO.SysObjects WHERE [NAME] = '##T_Disc_OA' " & vbCrLf & _
                '              "           AND TYPE = 'U') " & vbCrLf & _
                '              " BEGIN " & vbCrLf & _
                '              " DROP TABLE ##T_DISC_OA; END " & vbCrLf & _
                '              " SELECT OPB.PO_BRANDPACK_ID,OOAB.OA_BRANDPACK_ID,OOAB.OA_ORIGINAL_QTY + ISNULL(OOBD.DISC_QTY,0)AS TOTAL_QTY INTO ##T_Disc_OA " & vbCrLf & _
                '              " FROM ORDR_OA_BRANDPACK OOAB LEFT OUTER JOIN " & vbCrLf & _
                '              " ( " & vbCrLf & _
                '              "  SELECT OA_BRANDPACK_ID,ISNULL(SUM(DISC_QTY),0)AS DISC_QTY FROM ORDR_OA_BRANDPACK_DISC WHERE CREATE_DATE >= @StartDate  " & vbCrLf & _
                '              "  AND GQSY_SGT_P_FLAG IN('G','MG','KP','CP','CR','DK','O','S1','S2','Q1','Q2','Q3','Q4','Y') GROUP BY OA_BRANDPACK_ID " & vbCrLf & _
                '              "  )OOBD ON OOAB.OA_BRANDPACK_ID = OOBD.OA_BRANDPACK_ID " & vbCrLf & _
                '              " INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_BRANDPACK_ID = OOAB.PO_BRANDPACK_ID " & vbCrLf & _
                '              " WHERE (OOAB.OA_ORIGINAL_QTY + ISNULL(OOBD.DISC_QTY,0)) > 0  AND OOAB.CREATE_DATE >= @StartDate ; " & vbCrLf & _
                '              " CREATE CLUSTERED INDEX IX_T_Disc_OA ON ##T_Disc_OA(PO_BRANDPACK_ID) ;"
                '    If IsNothing(Me.SqlCom) Then
                '        Me.CreateCommandSql("", Query)
                '    Else : Me.ResetCommandText(CommandType.Text, Query)
                '    End If
                '    Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                '    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                'End If
                'crete table temporari table po based on  LeadTimeStart and LeadTimeEnd
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                      " IF NOT EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SHIP_TO_TM' AND TYPE = 'U') " & vbCrLf & _
                      " BEGIN " & vbCrLf & _
                      "   SELECT OST.OA_ID,ST.SHIP_TO_ID,TER.TERRITORY_ID,TER.TERRITORY_AREA,TM.MANAGER AS TERRITORY_MANAGER INTO TEMPDB..##T_SHIP_TO_TM FROM TERRITORY TER INNER JOIN SHIP_TO ST ON ST.TERRITORY_ID = TER.TERRITORY_ID " & vbCrLf & _
                      "   INNER JOIN TERRITORY_MANAGER TM ON TM.TM_ID = ST.TM_ID WHERE ST.INACTIVE = 0 ;" & vbCrLf & _
                      " CREATE CLUSTERED INDEX IX_T_SHIP_TO_TM ON ##T_SHIP_TO_TM(SHIP_TO_ID,TERRITORY_ID) ; " & vbCrLf & _
                      " END "
                If IsDateChanged Then

                    Query = "SET NOCOUNT ON; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                            "IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_Invoce_PO_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                            " BEGIN DROP TABLE TEMPDB..##T_Invoce_PO_" & Me.ComputerName & " ; END" & vbCrLf & _
                            "IF EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_INVOICE_2_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                            " BEGIN DROP TABLE TEMPDB..##T_INVOICE_2_" & Me.ComputerName & " ; END "
                    If IsNothing(Me.SqlCom) Then
                        Me.CreateCommandSql("sp_executesql", "")
                    Else
                        Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    End If

                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " SELECT SEGMENT = CASE WHEN (PL.PLANTATION_ID IS NOT NULL AND PL.PLANTATION_ID != '') THEN 'PLANTATION' WHEN (P.PROJ_REF_NO IS NOT NULL AND P.PROJ_REF_NO != '') THEN 'PROJECT' ELSE 'FREE MARKET' END," & vbCrLf & _
                            " REG.REGIONAL_AREA,TER.REGIONAL_ID,TER.TERRITORY_AREA,DR.TERRITORY_ID,TM.MANAGER AS TERRITORY_MANAGER,PO.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,PO.PO_REF_NO,PO.PO_REF_DATE AS PO_DATE," & vbCrLf & _
                            " PO.PROJ_REF_NO,P.PROJECT_NAME,PL.PLANTATION_NAME,OOA.RUN_NUMBER,OOA.OA_ID,OOAB.OA_BRANDPACK_ID,OPB.PO_BRANDPACK_ID,BB.BRAND_ID,BR.BRAND_NAME,OPB.BRANDPACK_ID,BB.BRANDPACK_NAME," & vbCrLf & _
                            " OPB.PO_ORIGINAL_QTY,OPB.PO_PRICE_PERQTY AS PRICE,(OPB.PO_PRICE_PERQTY * OPB.PO_ORIGINAL_QTY) AS PO_AMOUNT, OPB.CREATE_BY AS CREATED_PO_BY " & vbCrLf & _
                            " INTO TEMPDB..##T_Invoce_PO_" & Me.ComputerName & " FROM Nufarm.dbo.ORDR_PURCHASE_ORDER PO INNER JOIN Nufarm.dbo.ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                            " INNER JOIN Nufarm.dbo.ORDR_ORDER_ACCEPTANCE OOA ON OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                            " INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOAB.OA_ID = OOA.OA_ID AND OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                            " INNER JOIN Nufarm.dbo.BRND_BRANDPACK BB ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID " & vbCrLf & _
                            " INNER JOIN Nufarm.dbo.DIST_DISTRIBUTOR DR ON PO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID " & vbCrLf & _
                            " INNER JOIN TERRITORY TER ON TER.TERRITORY_ID = DR.TERRITORY_ID " & vbCrLf & _
                            " INNER JOIN DIST_REGIONAL REG ON REG.REGIONAL_ID = TER.REGIONAL_ID " & vbCrLf & _
                            " INNER JOIN OA_SHIP_TO OS ON OS.OA_ID = OOAB.OA_ID INNER JOIN SHIP_TO ST ON OS.SHIP_TO_ID = ST.SHIP_TO_ID " & vbCrLf & _
                            " INNER JOIN TERRITORY_MANAGER TM ON TM.TM_ID = ST.TM_ID " & vbCrLf & _
                            " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BB.BRAND_ID " & vbCrLf & _
                            " LEFT OUTER JOIN PLANTATION PL ON OPB.PLANTATION_ID = PL.PLANTATION_ID " & vbCrLf & _
                            " LEFT OUTER JOIN PROJ_PROJECT P ON P.PROJ_REF_NO = PO.PROJ_REF_NO " & vbCrLf & _
                            " WHERE PO.PO_REF_DATE >= @LeadStartDate AND PO.PO_REF_DATE <= @LeadEndDate ;" & vbCrLf & _
                            " CREATE NONCLUSTERED INDEX IDX_T_PO ON ##T_Invoce_PO_" & Me.ComputerName & "(PO_DATE,OA_BRANDPACK_ID,PROJ_REF_NO,BRANDPACK_ID,PO_REF_NO);"
                    If IsNothing(Me.SqlCom) Then
                        Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@LeadStartDate", SqlDbType.SmallDateTime, LeadTimeStart)
                    Me.AddParameter("@LeadEndDate", SqlDbType.SmallDateTime, LeadTimeEnd)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    ''BIKIN TABLE TEMPORARI KE 2
                    Query = "SET NOCOUNT ON; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                            " SELECT INV.PONUMBER,INV.INVNUMBER,INV.REFERENCE,Tmbp.BRANDPACK_ID_DTS AS BRANDPACK_ID,INV.QTY,INV.UNITPRICE,INV.INV_AMOUNT,INV.AUDTUSER " & vbCrLf & _
                            " ,CAST( '' + SUBSTRING(CAST(INV.INVDATE AS VARCHAR(20)),5,2) + '/' + RIGHT(CAST(INV.INVDATE AS VARCHAR(20)),2) +  '/' + LEFT(CAST(INV.INVDATE AS VARCHAR(20)),4)AS SMALLDATETIME)AS INVDATE " & vbCrLf & _
                            " INTO TEMPDB..##T_INVOICE_2_" & Me.ComputerName & " FROM COMPARE_ITEM Tmbp INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV " & vbCrLf & _
                            " ON Tmbp.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
                            " WHERE(INV.QTY Is Not NULL And INV.QTY > 0) ;" & vbCrLf & _
                            " CREATE NONCLUSTERED INDEX IDX_T_INV ON ##T_INVOICE_2_" & Me.ComputerName & "(INVDATE,PONUMBER,REFERENCE,BRANDPACK_ID);"
                    'If IsNothing(Me.SqlCom) Then
                    '    Me.CreateCommandSql("Usp_Get_Invoice_by_Invoice_Date", "")
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    'End If
                End If
                If isRefDateByPO Then
                    'Me.CreateCommandSql("Usp_Get_Invoice_by_Invoice_Date", "")
                    Query = "SET NOCOUNT ON; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                             "IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_INVOICE_2_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                             " BEGIN RAISERROR('Could not find table temporary ##T_INVOICE_2_" & Me.ComputerName & "',16,1) ; RETURN ; END " & vbCrLf & _
                             "ELSE IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_Invoce_PO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                             " BEGIN RAISERROR('Could not find table temporary ##T_Invoce_PO_" & Me.ComputerName & "',16,1) ; RETURN ; END " & vbCrLf & _
                             "ELSE " & vbCrLf & _
                             "   BEGIN " & vbCrLf & _
                             "       SELECT PO.SEGMENT,PO.REGIONAL_ID,PO.REGIONAL_AREA,PO.TERRITORY_ID,PO.TERRITORY_AREA,PO.TERRITORY_MANAGER,PO.DISTRIBUTOR_ID,PO.DISTRIBUTOR_NAME,PO.PO_REF_NO,PO.PO_DATE,PO.OA_ID AS OA_REF_NO," & vbCrLf & _
                             "       PO.PROJ_REF_NO,PO.PROJECT_NAME,PO.PLANTATION_NAME,PO.PO_BRANDPACK_ID,PO.BRAND_ID,PO.BRAND_NAME,PO.BRANDPACK_ID,PO.BRANDPACK_NAME,PO.PO_ORIGINAL_QTY,PO.PRICE,PO.PO_AMOUNT,(INV.QTY/ISNULL(OOBD.SPPB_QTY,0)) * PO.PO_ORIGINAL_QTY AS INVQTY," & vbCrLf & _
                             "       INV.UNITPRICE AS INV_PRICE,((INV.QTY/ISNULL(OOBD.SPPB_QTY,0)) * PO.PO_ORIGINAL_QTY) * INV.UNITPRICE AS INV_AMOUNT,PO.CREATED_PO_BY," & vbCrLf & _
                             "       INV.INVNUMBER AS INVOICE_NUMBER,INV.INVDATE,FLAG = CASE WHEN((MONTH(INV.INVDATE) >= 8) AND (MONTH(INV.INVDATE) <= 12))THEN 'S1' " & vbCrLf & _
                             "       WHEN (MONTH(INV.INVDATE) = 1) THEN 'S1' " & vbCrLf & _
                             "       WHEN((MONTH(INV.INVDATE) >= 2) AND (MONTH(INV.INVDATE) <= 7)) THEN 'S2' ELSE '' END, " & vbCrLf & _
                             "       YEAR_PERIODE = CASE WHEN ((MONTH(INV.INVDATE) >= 8) AND (MONTH(INV.INVDATE) <= 12))  THEN (CAST(YEAR(INV.INVDATE) AS VARCHAR(4)) + '-' + CAST((YEAR(INV.INVDATE) + 1) AS VARCHAR(4)))  " & vbCrLf & _
                             "                      WHEN (MONTH(INV.INVDATE) = 1) THEN (CAST((YEAR(INV.INVDATE) - 1) AS VARCHAR(4)) + '-' + CAST(YEAR(INV.INVDATE) AS VARCHAR(4)))  " & vbCrLf & _
                             "                      ELSE (CAST((YEAR(INV.INVDATE) - 1) AS VARCHAR(4)) + '-' + CAST(YEAR(INV.INVDATE) AS VARCHAR(4))) END, " & vbCrLf & _
                             "       INV.QTY AS TOTAL_SHIPMENT,INV.INV_AMOUNT AS TOTAL_SHIPMENT_AMOUNT,INV.AUDTUSER AS CREATED_INVOICE_BY,DATENAME(MONTH,INV.INVDATE)AS [MONTH] " & vbCrLf & _
                             "       FROM  TEMPDB..##T_Invoce_PO_" & Me.ComputerName & " PO LEFT OUTER JOIN TEMPDB..##T_INVOICE_2_" & Me.ComputerName & " INV ON INV.BRANDPACK_ID = PO.BRANDPACK_ID AND ((INV.REFERENCE = PO.RUN_NUMBER) OR (INV.PONUMBER = PO.PO_REF_NO)) " & vbCrLf & _
                             "       LEFT OUTER JOIN SPPB_BRANDPACK OOBD ON OOBD.OA_BRANDPACK_ID = PO.OA_BRANDPACK_ID " & vbCrLf & _
                             "       WHERE PO.PO_DATE >= @START_DATE AND PO.PO_DATE <= @END_DATE "
                    If Not String.IsNullOrEmpty(DistributorID) Then
                        Query &= vbCrLf
                        Query &= " AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID "
                    End If
                    Query &= vbCrLf
                    Query &= " AND PO.PO_ORIGINAL_QTY > 0;" & vbCrLf & _
                       "    END "
                Else
                    Query = "SET NOCOUNT ON; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                            "IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_INVOICE_2_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                            " BEGIN RAISERROR('Could not find table temporary ##T_INVOICE_2_" & Me.ComputerName & "',16,1) ; RETURN ; END " & vbCrLf & _
                            "ELSE IF NOT EXISTS(SELECT NAME FROM [tempdb].[sys].[objects] WHERE NAME = '##T_Invoce_PO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                            " BEGIN RAISERROR('Could not find table temporary ##T_Invoce_PO_" & Me.ComputerName & "',16,1) ; RETURN ; END " & vbCrLf & _
                            "ELSE " & vbCrLf & _
                            "   BEGIN " & vbCrLf & _
                            "       SELECT PO.SEGMENT,PO.REGIONAL_ID,PO.REGIONAL_AREA,PO.TERRITORY_ID,PO.TERRITORY_AREA,PO.TERRITORY_MANAGER,PO.DISTRIBUTOR_ID,PO.DISTRIBUTOR_NAME,PO.PO_REF_NO,PO.PO_DATE,PO.OA_ID AS OA_REF_NO," & vbCrLf & _
                            "       PO.PROJ_REF_NO,PO.PROJECT_NAME,PO.PLANTATION_NAME,PO.PO_BRANDPACK_ID,PO.BRAND_ID,PO.BRAND_NAME,PO.BRANDPACK_ID,PO.BRANDPACK_NAME,PO.PO_ORIGINAL_QTY,PO.PRICE,PO.PO_AMOUNT,(INV.QTY/ISNULL(OOBD.SPPB_QTY,0)) * PO.PO_ORIGINAL_QTY AS INVQTY," & vbCrLf & _
                            "       INV.UNITPRICE AS INV_PRICE,((INV.QTY/ISNULL(OOBD.SPPB_QTY,0)) * PO.PO_ORIGINAL_QTY) * INV.UNITPRICE AS INV_AMOUNT,PO.CREATED_PO_BY," & vbCrLf & _
                            "       INV.INVNUMBER AS INVOICE_NUMBER,INV.INVDATE,FLAG = CASE WHEN((MONTH(INV.INVDATE) >= 8) AND (MONTH(INV.INVDATE) <= 12))THEN 'S1' " & vbCrLf & _
                            "       WHEN (MONTH(INV.INVDATE) = 1) THEN 'S1' " & vbCrLf & _
                            "       WHEN((MONTH(INV.INVDATE) >= 2) AND (MONTH(INV.INVDATE) <= 7)) THEN 'S2' ELSE '' END, " & vbCrLf & _
                            "       YEAR_PERIODE = CASE WHEN ((MONTH(INV.INVDATE) >= 8) AND (MONTH(INV.INVDATE) <= 12))  THEN (CAST(YEAR(INV.INVDATE) AS VARCHAR(4)) + '-' + CAST((YEAR(INV.INVDATE) + 1) AS VARCHAR(4)))  " & vbCrLf & _
                            "                      WHEN (MONTH(INV.INVDATE) = 1) THEN (CAST((YEAR(INV.INVDATE) - 1) AS VARCHAR(4)) + '-' + CAST(YEAR(INV.INVDATE) AS VARCHAR(4)))  " & vbCrLf & _
                            "                      ELSE (CAST((YEAR(INV.INVDATE) - 1) AS VARCHAR(4)) + '-' + CAST(YEAR(INV.INVDATE) AS VARCHAR(4))) END, " & vbCrLf & _
                            "        INV.QTY AS TOTAL_SHIPMENT,INV.INV_AMOUNT AS TOTAL_SHIPMENT_AMOUNT,INV.AUDTUSER AS CREATED_INVOICE_BY,DATENAME(MONTH,INV.INVDATE)AS [MONTH] " & vbCrLf & _
                            "       FROM  TEMPDB..##T_Invoce_PO_" & Me.ComputerName & " PO INNER JOIN TEMPDB..##T_INVOICE_2_" & Me.ComputerName & " INV ON INV.BRANDPACK_ID = PO.BRANDPACK_ID AND ((INV.REFERENCE = PO.RUN_NUMBER) OR (INV.PONUMBER = PO.PO_REF_NO)) " & vbCrLf & _
                            "       INNER JOIN SPPB_BRANDPACK OOBD ON OOBD.OA_BRANDPACK_ID = PO.OA_BRANDPACK_ID " & vbCrLf & _
                            "       WHERE INV.INVDATE >= @START_DATE AND INV.INVDATE <= @END_DATE "
                    If Not String.IsNullOrEmpty(DistributorID) Then
                        Query &= vbCrLf
                        Query &= " AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID "
                    End If
                    Query &= vbCrLf
                    Query &= " AND PO.PO_ORIGINAL_QTY > 0;" & vbCrLf & _
                       "    END "
                End If
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                If DistributorID = "" Then
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DBNull.Value)
                Else
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                End If
                If Not IsNothing(Me.SqlDat) Then
                    Me.SqlDat.SelectCommand = Me.SqlCom
                Else
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                End If
                Dim tblInvoice As New DataTable("INVOICE REPORT DATA") : tblInvoice.Clear()
                'Me.OpenConnection()
                Me.SqlDat.Fill(tblInvoice) : Me.ClearCommandParameters()

                If Not isRefDateByPO Then
                    Query = "SET NOCOUNT ON; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                              "SELECT PO.PO_REF_NO,PO.PO_DATE,PO.PO_BRANDPACK_ID,PO.PO_ORIGINAL_QTY,INV.INVNUMBER " & vbCrLf & _
                              "FROM  TEMPDB..##T_Invoce_PO_" & Me.ComputerName & " PO INNER JOIN TEMPDB..##T_INVOICE_2_" & Me.ComputerName & " INV ON INV.BRANDPACK_ID = PO.BRANDPACK_ID AND ((INV.REFERENCE = PO.RUN_NUMBER) OR (INV.PONUMBER = PO.PO_REF_NO)) " & vbCrLf & _
                              "INNER JOIN SPPB_BRANDPACK OOBD ON OOBD.OA_BRANDPACK_ID = PO.OA_BRANDPACK_ID " & vbCrLf & _
                              "WHERE INV.INVDATE >= @START_DATE AND INV.INVDATE <= @END_DATE "
                    'Query = " SET NOCOUNT ON; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                    '         "SELECT PO_REF_NO,PO_DATE,PO_BRANDPACK_ID,PO_ORIGINAL_QTY FROM ##T_Invoce_PO WHERE PO_DATE < @START_DATE ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@START_DATE", SqlDbType.VarChar, common.CommonClass.getNumericFromDate(StartDate.AddMonths(-3)))
                    Me.AddParameter("@END_DATE", SqlDbType.VarChar, common.CommonClass.getNumericFromDate(StartDate))
                    Me.SqlDat.Fill(t2) : Me.ClearCommandParameters()
                End If

                '===================UNCOMMENT THIS AFTER DEBUGGING========================
                'drop table invoice
                'Query = "SET NOCOUNT ON ;" & vbCrLf & _
                '        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SELECT_INVOICE_" & Me.ComputerName & "' AND Type = 'U') " & vbCrLf & _
                '        " BEGIN DROP TABLE ##T_SELECT_INVOICE_" & Me.ComputerName & " ; END " & vbCrLf & _
                '        " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_INVOICE_2_" & Me.ComputerName & "' AND Type = 'U') " & vbCrLf & _
                '        " BEGIN DROP TABLE ##T_INVOICE_2_" & Me.ComputerName & " ; END " & vbCrLf & _
                '        " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Invoce_PO_" & Me.ComputerName & "' AND Type = 'U') " & vbCrLf & _
                '        " BEGIN DROP TABLE ##T_Invoce_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
                '        " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_START_DATE_" & Me.ComputerName & "' AND Type = 'U') " & vbCrLf & _
                '        " BEGIN DROP TABLE ##T_START_DATE_" & Me.ComputerName & " ; END "
                'Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                'Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.SqlCom.ExecuteScalar()

                '===============================================================================
                Me.ClearCommandParameters()
                tblInvoice.Columns.Add("IDApp", Type.GetType("System.Int32"))
                tblInvoice.AcceptChanges()
                Dim newRow As DataRow = Nothing
                For i As Integer = 0 To tblInvoice.Rows.Count - 1
                    newRow = tblInvoice.Rows(i)
                    newRow.BeginEdit()
                    newRow("IDApp") = i + 1
                    newRow.EndEdit()
                Next
                tblInvoice.AcceptChanges()
                Return tblInvoice.DefaultView()
            Catch ex As Exception
                Try
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                              "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_START_DATE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                              " BEGIN  DROP TABLE  tempdb..##T_START_DATE_" & Me.ComputerName & " ; END " & vbCrLf & _
                              " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_MASTER_PO_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                              " BEGIN DROP TABLE tempdb..##T_MASTER_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
                              " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Agreement_Brand_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                              " BEGIN DROP TABLE tempdb..##T_Agreement_Brand_" & Me.ComputerName & " ; END " & vbCrLf & _
                              " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PO_Original_By_Distributor_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                              " BEGIN DROP TABLE tempdb..##T_PO_Original_By_Distributor_" & Me.ComputerName & " ; END " & vbCrLf & _
                              " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Agreement_BrandPack_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                              " BEGIN DROP TABLE tempdb..##T_Agreement_BrandPacK_" & Me.ComputerName & " ; END " & vbCrLf & _
                              " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SELECT_INVOICE_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                              " BEGIN DROP TABLE Tempdb..##T_SELECT_INVOICE_" & Me.ComputerName & " ; END "
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Catch ex1 As Exception
                    Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex1
                End Try
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try

        End Function
        Public Sub DisposeTempDB()
            Try
                'If Not Me.hasReservedInvoice(NufarmBussinesRules.User.UserLogin.UserName) Then
                'End If
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SELECT_INVOICE_" & Me.ComputerName & "' AND Type = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE ##T_SELECT_INVOICE_" & Me.ComputerName & " ; END " & vbCrLf & _
                        " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_INVOICE_2_" & Me.ComputerName & "' AND Type = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE ##T_INVOICE_2_" & Me.ComputerName & " ; END " & vbCrLf & _
                        " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Invoce_PO_" & Me.ComputerName & "' AND Type = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE ##T_Invoce_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
                        " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_START_DATE_" & Me.ComputerName & "' AND Type = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE ##T_START_DATE_" & Me.ComputerName & " ; END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar()
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.ExecuteScalar() : Me.ClearCommandParameters() : Me.CloseConnection()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Private disposedValue As Boolean = False        ' To detect redundant calls
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
                Me.DisposeTempDB()
                Dispose(True)
                GC.SuppressFinalize(Me)
            End If
            Me.disposedValue = True
        End Sub
    End Class
End Namespace

