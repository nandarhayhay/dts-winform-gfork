Imports System.Data
Imports System.Data.SqlClient
Namespace OrderAcceptance
    Public Class SPPBGONManager
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Protected Query As String
        Public Sub New()
            MyBase.New()
        End Sub
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Me.DisposeTempDB()
            MyBase.Dispose(disposing)
        End Sub

        Public Sub DeleteSPPBBrandPack(ByVal OA_BrandPackID As String)
            '==========================================OLD PROCESS===================================================
            'Try
            '    Me.CreateCommandSql("Usp_Delete_SPPB_BrandPack", "")
            '    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
            '    'Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 70)
            '    Me.OpenConnection()
            '    Me.BeginTransaction()
            '    Me.ExecuteNonQuery()
            '    Me.CommiteTransaction()
            '    Me.CloseConnection()
            'Catch ex As Exception
            '    Me.RollbackTransaction()
            '    Me.CloseConnection()
            '    Throw ex
            'End Try
            '========================================================================================
            Try
                ''check if data has any gon referenced
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT 1 WHERE EXISTS(SELECT GON_DETAIL_ID FROM GON_DETAIL WHERE SPPB_BRANDPACK_ID = ANY(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK " & vbCrLf & _
                        " WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID)); "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BrandPackID, 90)
                Me.OpenConnection() : Me.BeginTransaction()
                Me.SqlCom.Transaction = Me.SqlTrans
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    If CInt(retval) > 0 Then
                        Me.ClearCommandParameters()
                        Throw New Exception("Can not delete data " & vbCrLf & "Data has a referenced-child data")
                    End If
                End If
                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Delete_SPPB_BrandPack")
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                ''reset command Text
                Me.CommiteTransaction()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.OpenConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function GetSalesReport(ByVal StartDate As DateTime, ByVal EndDate As DateTime) As DataTable
            Try
                Query = " SET NOCOUNT ON ; SET ARITHABORT OFF; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                         " SELECT CLASS_NAME,BRAND_NAME,ISNULL(SUM(CASE Type WHEN 'PURCHASE_ORDER' THEN TOTAL ELSE 0 END),0) AS PURCHASE_ORDER," & vbCrLf & _
                         " ISNULL(SUM(CASE Type WHEN 'SPPB' THEN TOTAL ELSE 0 END),0) AS SPPB,ISNULL(SUM(CASE Type WHEN 'INVOICE' THEN TOTAL ELSE 0 END),0) AS INVOICE," & vbCrLf & _
                         " TOTAL_IN = ISNULL(SUM(CASE Type WHEN 'PURCHASE_ORDER' THEN TOTAL WHEN 'SPPB' THEN TOTAL WHEN 'INVOICE' THEN TOTAL ELSE 0 END),0)  FROM( " & vbCrLf & _
                         " SELECT ISNULL(PC.CLASS_NAME,'UNDEFINED_CLASS') AS CLASS_NAME,TSR.BRAND_NAME, Type = 'INVOICE'," & vbCrLf & _
                         " TOTAL = SUM(CASE WHEN (TSR.GON_QTY IS NOT NULL) THEN ((ISNULL(TSR.GON_QTY,0)/ISNULL(TSR.SPPB_QTY,0)) * TSR.PO_ORIGINAL_QTY * TSR.PRICE) ELSE 0 END) " & vbCrLf & _
                         " FROM TEMPDB..##T_SALES_REPORT_" & Me.ComputerName & "  TSR INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = TSR.BRANDPACK_ID " & vbCrLf & _
                         " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID LEFT OUTER JOIN PRODUCT_CLASS PC ON PC.BRAND_ID = BR.BRAND_ID " & vbCrLf & _
                         " WHERE TSR.GON_DATE >= @StartDate AND TSR.GON_DATE <= @EndDate " & vbCrLf & _
                         " GROUP BY PC.CLASS_NAME,TSR.BRAND_NAME " & vbCrLf & _
                         " UNION " & vbCrLf & _
                         " SELECT ISNULL(PC.CLASS_NAME,'UNDEFINED_CLASS') AS CLASS_NAME,TSR.BRAND_NAME, Type = 'SPPB'," & vbCrLf & _
                         " TOTAL = SUM(CASE WHEN (TSR.SPPB_QTY IS NOT NULL) THEN ((TSR.PO_ORIGINAL_QTY * TSR.PRICE) - ((ISNULL(TSR.GON_QTY,0)/ISNULL(TSR.SPPB_QTY,0)) * TSR.PO_ORIGINAL_QTY) * TSR.PRICE) ELSE 0 END) " & vbCrLf & _
                         " FROM TEMPDB..##T_SALES_REPORT_" & Me.ComputerName & "  TSR INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = TSR.BRANDPACK_ID " & vbCrLf & _
                         " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID LEFT OUTER JOIN  " & vbCrLf & _
                         " PRODUCT_CLASS PC ON PC.BRAND_ID = BR.BRAND_ID  " & vbCrLf & _
                         " WHERE TSR.SPPB_DATE >= @StartDate AND TSR.SPPB_DATE <= @EndDate AND (TSR.SPPB_NO IS NOT NULL AND TSR.STATUS <> 'SHIPPED') " & vbCrLf & _
                         " GROUP BY PC.CLASS_NAME,TSR.BRAND_NAME " & vbCrLf & _
                         " UNION " & vbCrLf & _
                         " SELECT ISNULL(PC.CLASS_NAME,'UNDEFINED_CLASS') AS CLASS_NAME,TSR.BRAND_NAME, Type = 'PURCHASE_ORDER'," & vbCrLf & _
                         " TOTAL = SUM(CASE WHEN (TSR.SPPB_QTY IS NULL) THEN (TSR.PO_ORIGINAL_QTY * TSR.PRICE) ELSE 0 END) " & vbCrLf & _
                         " FROM TEMPDB..##T_SALES_REPORT_" & Me.ComputerName & " TSR INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = TSR.BRANDPACK_ID " & vbCrLf & _
                         " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID LEFT OUTER JOIN  " & vbCrLf & _
                         " PRODUCT_CLASS PC ON PC.BRAND_ID = BR.BRAND_ID  WHERE TSR.PO_REF_DATE >= @StartDate AND TSR.PO_REF_DATE <= @EndDate AND TSR.SPPB_NO IS NULL " & vbCrLf & _
                         " GROUP BY PC.CLASS_NAME,TSR.BRAND_NAME " & vbCrLf & _
                         " )G GROUP BY CLASS_NAME,BRAND_NAME ; "
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : ResetCommandText(CommandType.Text, Query)
                End If
                AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                OpenConnection()
                Dim tblSalesReport As New DataTable("T_SalesReport")
                tblSalesReport.Clear()
                setDataAdapter(Me.SqlCom).Fill(tblSalesReport) : Me.ClearCommandParameters()
                Return tblSalesReport
            Catch ex As Exception
                Me.OpenConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try

        End Function
        Public Sub DeleteGON(ByVal GON_DETAIL_ID As String)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " DECLARE @V_GON_HEADER_ID VARCHAR(40),@V_TOTAL_GON DECIMAL(18,3),@V_SPPB_BRANDPACK_ID VARCHAR(90),@V_SPPB_QTY DECIMAL(18,3),@V_STATUS VARCHAR(100); " & vbCrLf & _
                        " SET @V_GON_HEADER_ID = (SELECT TOP 1 GON_HEADER_ID FROM GON_DETAIL WHERE GON_DETAIL_ID = @GON_DETAIL_ID );" & vbCrLf & _
                        " SET @V_SPPB_BRANDPACK_ID = (SELECT TOP 1 SPPB_BRANDPACK_ID FROM GON_DETAIL WHERE GON_DETAIL_ID = @GON_DETAIL_ID) ;" & vbCrLf & _
                        " SET @V_SPPB_QTY = (SELECT TOP 1 SPPB_QTY FROM SPPB_BRANDPACK WHERE SPPB_BRANDPACK_ID = @V_SPPB_BRANDPACK_ID) " & vbCrLf & _
                        " DELETE FROM GON_DETAIL WHERE GON_DETAIL_ID = @GON_DETAIL_ID ;" & vbCrLf & _
                        " SET @V_TOTAL_GON = (SELECT ISNULL(SUM(GON_QTY),0) FROM GON_DETAIL WHERE SPPB_BRANDPACK_ID = @V_SPPB_BRANDPACK_ID) ;" & vbCrLf & _
                        " IF @V_TOTAL_GON >= @V_SPPB_QTY " & vbCrLf & _
                        " BEGIN SET @V_STATUS = 'SHIPPED' ; END " & vbCrLf & _
                        " ELSE IF @V_TOTAL_GON <= 0 " & vbCrLf & _
                        " BEGIN SET @V_STATUS = '--NEWSPPB--' ; END " & vbCrLf & _
                        " ELSE " & vbCrLf & _
                        " BEGIN SET @V_STATUS = 'UNKNOWN' ;END " & vbCrLf & _
                        " UPDATE SPPB_BRANDPACK SET STATUS = @V_STATUS WHERE SPPB_BRANDPACK_ID = @V_SPPB_BRANDPACK_ID " & vbCrLf & _
                        " IF NOT EXISTS(SELECT GON_HEADER_ID FROM GON_DETAIL WHERE GON_HEADER_ID = @V_GON_HEADER_ID) " & vbCrLf & _
                        " BEGIN DELETE FROM GON_HEADER WHERE GON_HEADER_ID = @V_GON_HEADER_ID ;  " & vbCrLf & _
                        "       DELETE FROM GON_RECEIVED_BACK WHERE GRB_ID = @V_GON_HEADER_ID ; END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@GON_DETAIL_ID", SqlDbType.VarChar, GON_DETAIL_ID, 140)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CommiteTransaction()
            Catch ex As Exception
                Me.RollbackTransaction() : Me.OpenConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Private Sub CreateTempTableByCustomCategory(ByVal QueryCriteria As String, ByVal ParameterCriteria As String, ByVal valueCriteria As String)
            Query = "SET NOCOUNT ON ;" & vbCrLf & _
                      "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_HEADER_PO_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                      "BEGIN DROP TABLE TEMPDB..##T_HEADER_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
                      "SELECT PO.PO_REF_NO,PO.DISTRIBUTOR_ID,PO.PO_REF_DATE,PO.PROJ_REF_NO,P.PROJECT_NAME,DR.DISTRIBUTOR_NAME INTO ##T_HEADER_PO_" & Me.ComputerName & " FROM ORDR_PURCHASE_ORDER PO INNER JOIN DIST_DISTRIBUTOR DR " & vbCrLf & _
                      " ON PO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID LEFT OUTER JOIN PROJ_PROJECT P ON PO.PROJ_REF_NO = P.PROJ_REF_NO " & vbCrLf
            Query &= QueryCriteria
            Query &= " --CREATE CLUSTERED INDEX IX_T_HEADER_PO_" & Me.ComputerName & " ON ##T_HEADER_PO_" & Me.ComputerName & "(PO_REF_NO);"
            If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
            Else : Me.ResetCommandText(CommandType.Text, Query)
            End If

            Me.OpenConnection()
            Me.AddParameter(ParameterCriteria, SqlDbType.VarChar, valueCriteria, valueCriteria.Length)
            Me.SqlCom.ExecuteScalar()
            '----------------------------------------------------------------------------------------------

            '======================  PO DETAIL     ================================================================
            Query = "SET NOCOUNT ON ;" & vbCrLf & _
            "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PO_DETAIL_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                "BEGIN DROP TABLE TEMPDB..##T_PO_DETAIL_" & Me.ComputerName & " ; END " & vbCrLf & _
                "SELECT PO.PO_REF_NO,ISNULL(CLS.CLASS_NAME,'UNDEFINED_CLASS')AS CLASS_NAME,BR.BRAND_NAME,OPB.PO_BRANDPACK_ID,OPB.PROJ_BRANDPACK_ID,OPB.BRANDPACK_ID, " & vbCrLf & _
                " BP.BRANDPACK_NAME,OPB.PO_PRICE_PERQTY AS PRICE,OPB.PO_ORIGINAL_QTY,OPB.PLANTATION_ID,PL.PLANTATION_NAME,OPB.DESCRIPTIONS2 " & vbCrLf & _
                " INTO ##T_PO_DETAIL_" & Me.ComputerName & " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = OPB.BRANDPACK_ID INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID " & vbCrLf & _
                " LEFT OUTER JOIN PRODUCT_CLASS CLS ON (CLS.BRAND_ID = BR.BRAND_ID AND CLS.INACTIVE = 0) " & vbCrLf & _
                " LEFT OUTER JOIN PLANTATION PL ON PL.PLANTATION_ID = OPB.PLANTATION_ID " & vbCrLf
            Query &= QueryCriteria & vbCrLf
            Query &= " --CREATE NONCLUSTERED INDEX IX_T_PO_DETAIL_" & Me.ComputerName & " ON ##T_PO_DETAIL_" & Me.ComputerName & "(PO_REF_NO,PO_BRANDPACK_ID) ;"
            Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()
            '-----------------------------------------------------------------------------------------------------------------------

            '=====================  OA BRANDPACK    =================================================================
            Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_BrandPack_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                    " BEGIN DROP TABLE TEMPDB..##T_R_OA_BrandPack_" & Me.ComputerName & " ; END " & vbCrLf & _
                    "SELECT OA.PO_REF_NO,OA.OA_ID,OPB.PO_BRANDPACK_ID,OA.OA_BRANDPACK_ID INTO ##T_R_OA_BrandPack_" & Me.ComputerName & vbCrLf & _
                    " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                    " LEFT OUTER JOIN (" & vbCrLf & _
                    "                   SELECT OOA.PO_REF_NO,OOA.OA_ID,OOB.OA_BRANDPACK_ID,OOB.OA_ORIGINAL_QTY AS OA_QTY,OOB.PO_BRANDPACK_ID FROM ORDR_ORDER_ACCEPTANCE OOA  " & vbCrLf & _
                    "                   INNER JOIN ORDR_OA_BRANDPACK OOB ON OOA.OA_ID = OOB.OA_ID   " & vbCrLf & _
                    "                  )OA " & vbCrLf & _
                    " ON OA.PO_REF_NO = PO.PO_REF_NO AND OA.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf
            Query &= QueryCriteria & vbCrLf
            Query &= " --CREATE NONCLUSTERED INDEX IX_T_R_OA_BrandPack_" & Me.ComputerName & " ON ##T_R_OA_BrandPack_" & Me.ComputerName & "(PO_REF_NO,OA_BRANDPACK_ID,PO_BRANDPACK_ID) ;"
            Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()


            '====================   SPPB HEADER And Detail   ==========================================================================

            Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                    " BEGIN DROP TABLE TEMPDB..##T_R_SPPB_" & Me.ComputerName & " ; END " & vbCrLf & _
                    " SELECT SH.SPPB_NO,SH.SPPB_DATE,SH.CREATE_DATE,SB1.OA_BRANDPACK_ID,SB1.SPPB_BRANDPACK_ID,SB1.SPPB_QTY," & vbCrLf & _
                    " SB1.STATUS,BALANCE = SB1.SPPB_QTY - ISNULL(GON.TOTAL_GON_QTY,0),GON.TOTAL_GON,GON.TOTAL_GON_QTY AS TOTAL_GON_QTY,GON.LAST_GON_DATE,GON.LAST_GON_RETDATE," & vbCrLf & _
                    " SB1.REMARK,SB1.CREATE_BY,SB1.IsUpdatedBySystem INTO ##T_R_SPPB_" & Me.ComputerName & " FROM SPPB_HEADER SH INNER JOIN SPPB_BRANDPACK SB1 ON SH.SPPB_NO = SB1.SPPB_NO " & vbCrLf & _
                    " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = SH.PO_REF_NO " & vbCrLf & _
                    " LEFT OUTER JOIN (SELECT GD.SPPB_BRANDPACK_ID,SUM(GD.GON_QTY)AS TOTAL_GON_QTY,COUNT(SPPB_BRANDPACK_ID)AS TOTAL_GON,MAX(GH.GON_DATE)AS LAST_GON_DATE,MAX(GR.RETURNED_GON_DATE)AS LAST_GON_RETDATE " & vbCrLf & _
                    " FROM GON_DETAIL GD INNER JOIN GON_HEADER GH " & vbCrLf & _
                    " ON GD.GON_HEADER_ID = GH.GON_HEADER_ID LEFT OUTER JOIN GON_RECEIVED_BACK GR ON GR.GRB_ID = GH.GON_HEADER_ID GROUP BY GD.SPPB_BRANDPACK_ID)GON " & vbCrLf & _
                    " ON GON.SPPB_BRANDPACK_ID = SB1.SPPB_BRANDPACK_ID  "
            Query &= QueryCriteria & vbCrLf
            Query &= " CREATE NONCLUSTERED INDEX IX_T_R_SPPB_" & Me.ComputerName & " ON ##T_R_SPPB_" & Me.ComputerName & "(OA_BRANDPACK_ID,SPPB_DATE) ;"
            Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()

            Query = "SET NOCOUNT ON ;" & vbCrLf & _
                       "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_SHIP_TO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                       " BEGIN DROP TABLE TEMPDB..##T_R_OA_SHIP_TO_" & Me.ComputerName & " ; END " & vbCrLf & _
                       " SELECT OST.OA_ID,REG.REGIONAL_AREA,TERR.TERRITORY_AREA,TM.MANAGER INTO ##T_R_OA_SHIP_TO_" & Me.ComputerName & " FROM OA_SHIP_TO OST INNER JOIN SHIP_TO ST ON OST.SHIP_TO_ID = ST.SHIP_TO_ID " & vbCrLf & _
                       " INNER JOIN TERRITORY TERR ON TERR.TERRITORY_ID = ST.TERRITORY_ID INNER JOIN TERRITORY_MANAGER TM ON ST.TM_ID = TM.TM_ID " & vbCrLf & _
                       " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OST.OA_ID = OOA.OA_ID INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OOA.PO_REF_NO INNER JOIN DIST_REGIONAL REG ON REG.REGIONAL_ID = TERR.REGIONAL_ID "
            Query &= QueryCriteria & vbCrLf
            Query &= " --CREATE NONCLUSTERED INDEX IX_T_R_OA_SHIP_TO_" & Me.ComputerName & " ON ##T_R_OA_SHIP_TO_" & Me.ComputerName & "(OA_ID) ;"
            Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()


        End Sub

        Private Sub CreateTempTableByMasterCategory(ByVal FromDate As Object, ByVal UntilDate As Object, ByVal MasterCategory As String)
           

            Dim StrColCategory As String = "PO.PO_REF_DATE"
            If MasterCategory = "BySPPB" Then : StrColCategory = "SH.SPPB_DATE" : End If
            'check datediff
            If DateDiff(DateInterval.Month, Convert.ToDateTime(FromDate), Convert.ToDateTime(UntilDate)) > 24 Then 'lebih besar dari 2 tahun
                Throw New Exception("the resource of data you are trying to show" & vbCrLf & "Is too big, system can not accomodate it")
            End If
            If MasterCategory = "ByPO" Then
                '==================    PO HEADER      ==============================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                      "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_HEADER_PO_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                      "BEGIN DROP TABLE TEMPDB..##T_HEADER_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
                      "SELECT PO.PO_REF_NO,PO.DISTRIBUTOR_ID,PO.PO_REF_DATE,PO.PROJ_REF_NO,P.PROJECT_NAME,DR.DISTRIBUTOR_NAME INTO ##T_HEADER_PO_" & Me.ComputerName & " FROM ORDR_PURCHASE_ORDER PO INNER JOIN DIST_DISTRIBUTOR DR " & vbCrLf & _
                      " ON PO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID LEFT OUTER JOIN PROJ_PROJECT P ON PO.PROJ_REF_NO = P.PROJ_REF_NO WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                      " --CREATE CLUSTERED INDEX IX_T_HEADER_PO_" & Me.ComputerName & " ON ##T_HEADER_PO_" & Me.ComputerName & "(PO_REF_NO);"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If

                Me.OpenConnection()
                Me.AddParameter("@FROM_DATE", SqlDbType.SmallDateTime, FromDate)
                Me.AddParameter("@UNTIL_DATE", SqlDbType.SmallDateTime, UntilDate)
                Me.SqlCom.ExecuteScalar()
                '----------------------------------------------------------------------------------------------

                '======================  PO DETAIL     ================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PO_DETAIL_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                " BEGIN DROP TABLE TEMPDB..##T_PO_DETAIL_" & Me.ComputerName & " ; END " & vbCrLf & _
                " SELECT PO.PO_REF_NO,ISNULL(CLS.CLASS_NAME,'UNDEFINED_CLASS') AS CLASS_NAME,BR.BRAND_NAME,OPB.PO_BRANDPACK_ID,OPB.PROJ_BRANDPACK_ID,OPB.BRANDPACK_ID," & vbCrLf & _
                " BP.BRANDPACK_NAME,OPB.PO_PRICE_PERQTY AS PRICE,OPB.PO_ORIGINAL_QTY,OPB.PLANTATION_ID,PL.PLANTATION_NAME,OPB.DESCRIPTIONS2 " & vbCrLf & _
                " INTO ##T_PO_DETAIL_" & Me.ComputerName & " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = OPB.BRANDPACK_ID INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID " & vbCrLf & _
                " LEFT OUTER JOIN PLANTATION PL ON PL.PLANTATION_ID = OPB.PLANTATION_ID " & vbCrLf & _
                " LEFT OUTER JOIN PRODUCT_CLASS CLS ON (CLS.BRAND_ID = BR.BRAND_ID AND CLS.INACTIVE = 0) " & vbCrLf & _
                " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ; " & vbCrLf & _
                " --CREATE NONCLUSTERED INDEX IX_T_PO_DETAIL_" & Me.ComputerName & " ON ##T_PO_DETAIL_" & Me.ComputerName & "(PO_REF_NO,PO_BRANDPACK_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()
                '-----------------------------------------------------------------------------------------------------------------------

                '=====================  OA BRANDPACK    =================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_BrandPack_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_OA_BrandPack_" & Me.ComputerName & " ; END " & vbCrLf & _
                        "SELECT OA.PO_REF_NO,OPB.PO_BRANDPACK_ID,OA.OA_ID,OA.OA_BRANDPACK_ID INTO ##T_R_OA_BrandPack_" & Me.ComputerName & vbCrLf & _
                        " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN (" & vbCrLf & _
                        "                   SELECT OOA.PO_REF_NO,OOA.OA_ID,OOB.OA_BRANDPACK_ID,OOB.OA_ORIGINAL_QTY AS OA_QTY,OOB.PO_BRANDPACK_ID FROM ORDR_ORDER_ACCEPTANCE OOA  " & vbCrLf & _
                        "                   INNER JOIN ORDR_OA_BRANDPACK OOB ON OOA.OA_ID = OOB.OA_ID WHERE OOA.OA_DATE >= @FROM_DATE  " & vbCrLf & _
                        "                  )OA " & vbCrLf & _
                        " ON OA.PO_REF_NO = PO.PO_REF_NO AND OA.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                        " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                        " --CREATE NONCLUSTERED INDEX IX_T_R_OA_BrandPack_" & Me.ComputerName & " ON ##T_R_OA_BrandPack_" & Me.ComputerName & "(PO_REF_NO,OA_BRANDPACK_ID,PO_BRANDPACK_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()


                '====================   SPPB HEADER And Detail   ==========================================================================

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_SPPB_" & Me.ComputerName & " ; END " & vbCrLf & _
                        " SELECT SH.SPPB_NO,SH.SPPB_DATE,SB1.OA_BRANDPACK_ID,SB1.SPPB_BRANDPACK_ID,SB1.SPPB_QTY," & vbCrLf & _
                        " SB1.STATUS,BALANCE = SB1.SPPB_QTY - ISNULL(GON.TOTAL_GON_QTY,0),GON.TOTAL_GON,GON.TOTAL_GON_QTY AS TOTAL_GON_QTY,GON.LAST_GON_DATE,GON.LAST_GON_RETDATE," & vbCrLf & _
                        " SB1.REMARK,SB1.CREATE_DATE,SB1.CREATE_BY,SB1.IsUpdatedBySystem INTO ##T_R_SPPB_" & Me.ComputerName & " FROM SPPB_HEADER SH INNER JOIN SPPB_BRANDPACK SB1 ON SH.SPPB_NO = SB1.SPPB_NO " & vbCrLf & _
                        " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = SH.PO_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN (SELECT GD.SPPB_BRANDPACK_ID,SUM(GD.GON_QTY)AS TOTAL_GON_QTY,COUNT(SPPB_BRANDPACK_ID)AS TOTAL_GON,MAX(GH.GON_DATE)AS LAST_GON_DATE,MAX(GR.RETURNED_GON_DATE)AS LAST_GON_RETDATE " & vbCrLf & _
                        " FROM GON_DETAIL GD INNER JOIN GON_HEADER GH " & vbCrLf & _
                        " ON GD.GON_HEADER_ID = GH.GON_HEADER_ID LEFT OUTER JOIN GON_RECEIVED_BACK GR ON GR.GRB_ID = GH.GON_HEADER_ID WHERE GH.GON_DATE >= @FROM_DATE GROUP BY GD.SPPB_BRANDPACK_ID)GON " & vbCrLf & _
                        " ON GON.SPPB_BRANDPACK_ID = SB1.SPPB_BRANDPACK_ID " & vbCrLf & _
                        " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                        " --CREATE NONCLUSTERED INDEX IX_T_R_SPPB_" & Me.ComputerName & " ON ##T_R_SPPB_" & Me.ComputerName & "(OA_BRANDPACK_ID,SPPB_DATE) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()

                '========================SHIP TO MANAGER AND DISTRIBUTOR==========================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                         "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_SHIP_TO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                         " BEGIN DROP TABLE TEMPDB..##T_R_OA_SHIP_TO_" & Me.ComputerName & " ; END " & vbCrLf & _
                         " SELECT OST.OA_ID,REG.REGIONAL_AREA,TERR.TERRITORY_AREA,TM.MANAGER INTO ##T_R_OA_SHIP_TO_" & Me.ComputerName & " FROM OA_SHIP_TO OST INNER JOIN SHIP_TO ST ON OST.SHIP_TO_ID = ST.SHIP_TO_ID " & vbCrLf & _
                         " INNER JOIN TERRITORY TERR ON TERR.TERRITORY_ID = ST.TERRITORY_ID INNER JOIN TERRITORY_MANAGER TM ON ST.TM_ID = TM.TM_ID " & vbCrLf & _
                         " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OST.OA_ID = OOA.OA_ID INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OOA.PO_REF_NO INNER JOIN DIST_REGIONAL REG ON REG.REGIONAL_ID = TERR.REGIONAL_ID " & vbCrLf & _
                         " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                         " --CREATE NONCLUSTERED INDEX IX_T_R_OA_SHIP_TO_" & Me.ComputerName & " ON ##T_R_OA_SHIP_TO_" & Me.ComputerName & "(OA_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                ''Created GON Data

            ElseIf MasterCategory = "BySPPB" Then
                '==================    PO HEADER      ==============================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_HEADER_PO_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                        "BEGIN DROP TABLE TEMPDB..##T_HEADER_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
                        "SELECT PO.PO_REF_NO,PO.DISTRIBUTOR_ID,PO.PO_REF_DATE,PO.PROJ_REF_NO,P.PROJECT_NAME,DR.DISTRIBUTOR_NAME INTO ##T_HEADER_PO_" & Me.ComputerName & " FROM ORDR_PURCHASE_ORDER PO INNER JOIN DIST_DISTRIBUTOR DR " & vbCrLf & _
                        " ON PO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID LEFT OUTER JOIN PROJ_PROJECT P ON PO.PROJ_REF_NO = P.PROJ_REF_NO WHERE PO.PO_REF_DATE >= @FROM_DATE ;" & vbCrLf & _
                        " --CREATE CLUSTERED INDEX IX_T_HEADER_PO_" & Me.ComputerName & " ON ##T_HEADER_PO_" & Me.ComputerName & "(PO_REF_NO);"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If

                Me.OpenConnection()
                Me.AddParameter("@FROM_DATE", SqlDbType.SmallDateTime, FromDate)
                Me.AddParameter("@UNTIL_DATE", SqlDbType.SmallDateTime, UntilDate)
                Me.SqlCom.ExecuteScalar()
                '-------------------------------------------------------------------------------------------------------------------------------

                '======================== PO DETAIL  ================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PO_DETAIL_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                "BEGIN DROP TABLE TEMPDB..##T_PO_DETAIL_" & Me.ComputerName & " ; END " & vbCrLf & _
                "SELECT PO.PO_REF_NO,ISNULL(CLS.CLASS_NAME,'UNDEFINED_CLASS') AS CLASS_NAME,BR.BRAND_NAME,OPB.PO_BRANDPACK_ID,OPB.PROJ_BRANDPACK_ID,OPB.BRANDPACK_ID,BP.BRANDPACK_NAME,OPB.PO_PRICE_PERQTY AS PRICE,OPB.PO_ORIGINAL_QTY,OPB.PLANTATION_ID,PL.PLANTATION_NAME,OPB.DESCRIPTIONS2 " & vbCrLf & _
                "INTO ##T_PO_DETAIL_" & Me.ComputerName & " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = OPB.BRANDPACK_ID INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID " & vbCrLf & _
                " LEFT OUTER JOIN PRODUCT_CLASS CLS ON (CLS.BRAND_ID = BR.BRAND_ID AND CLS.INACTIVE = 0) " & vbCrLf & _
                " LEFT OUTER JOIN PLANTATION PL ON PL.PLANTATION_ID = OPB.PLANTATION_ID " & vbCrLf & _
                " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ; " & vbCrLf & _
                " --CREATE NONCLUSTERED INDEX IX_T_PO_DETAIL_" & Me.ComputerName & " ON ##T_PO_DETAIL_" & Me.ComputerName & "(PO_REF_NO,PO_BRANDPACK_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()
                '-----------------------------------------------------------------------------------------------------------------------

                '======================= OA BRANDPACK =================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_BrandPack_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_OA_BrandPack_" & Me.ComputerName & " ; END " & vbCrLf & _
                        "SELECT OA.PO_REF_NO,OPB.PO_BRANDPACK_ID,OA.OA_ID,OA.OA_BRANDPACK_ID INTO ##T_R_OA_BrandPack_" & Me.ComputerName & vbCrLf & _
                        " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN (" & vbCrLf & _
                        "                   SELECT OOA.PO_REF_NO,OOA.OA_ID,OOB.OA_BRANDPACK_ID,OOB.OA_ORIGINAL_QTY AS OA_QTY,OOB.PO_BRANDPACK_ID FROM ORDR_ORDER_ACCEPTANCE OOA  " & vbCrLf & _
                        "                   INNER JOIN ORDR_OA_BRANDPACK OOB ON OOA.OA_ID = OOB.OA_ID WHERE OOA.OA_DATE >= @FROM_DATE  " & vbCrLf & _
                        "                  )OA " & vbCrLf & _
                        " ON OA.PO_REF_NO = PO.PO_REF_NO AND OA.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                        " WHERE PO.PO_REF_DATE >= @FROM_DATE  ;" & vbCrLf & _
                        " --CREATE NONCLUSTERED INDEX IX_T_R_OA_BrandPack_" & Me.ComputerName & " ON ##T_R_OA_BrandPack_" & Me.ComputerName & "(PO_REF_NO,OA_BRANDPACK_ID,PO_BRANDPACK_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()
                '----------------------------------------------------------------------------------------------


                '========================   SPPB    ==========================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_SPPB_" & Me.ComputerName & " ; END " & vbCrLf & _
                        " SELECT SH.SPPB_NO,SH.SPPB_DATE,SH.CREATE_DATE,SB1.OA_BRANDPACK_ID,SB1.SPPB_BRANDPACK_ID,SB1.SPPB_QTY," & vbCrLf & _
                        " SB1.STATUS,BALANCE = SB1.SPPB_QTY - ISNULL(GON.TOTAL_GON_QTY,0),GON.TOTAL_GON,GON.TOTAL_GON_QTY AS TOTAL_GON_QTY,GON.LAST_GON_DATE,GON.LAST_GON_RETDATE," & vbCrLf & _
                        " SB1.REMARK,SB1.CREATE_BY,SB1.IsUpdatedBySystem INTO ##T_R_SPPB_" & Me.ComputerName & " FROM SPPB_HEADER SH INNER JOIN SPPB_BRANDPACK SB1 ON SH.SPPB_NO = SB1.SPPB_NO " & vbCrLf & _
                        " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = SH.PO_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN (SELECT GD.SPPB_BRANDPACK_ID,SUM(GD.GON_QTY)AS TOTAL_GON_QTY,COUNT(GD.SPPB_BRANDPACK_ID)AS TOTAL_GON,MAX(GH.GON_DATE)AS LAST_GON_DATE,MAX(GR.RETURNED_GON_DATE)AS LAST_GON_RETDATE " & vbCrLf & _
                        " FROM GON_DETAIL GD INNER JOIN GON_HEADER GH " & vbCrLf & _
                        " ON GD.GON_HEADER_ID = GH.GON_HEADER_ID LEFT OUTER JOIN GON_RECEIVED_BACK GR ON GR.GRB_ID = GH.GON_HEADER_ID WHERE GH.GON_DATE >= @FROM_DATE GROUP BY GD.SPPB_BRANDPACK_ID)GON " & vbCrLf & _
                        " ON GON.SPPB_BRANDPACK_ID = SB1.SPPB_BRANDPACK_ID  " & vbCrLf & _
                        " WHERE SH.SPPB_DATE >= @FROM_DATE AND SH.SPPB_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                        " --CREATE NONCLUSTERED INDEX IX_T_R_SPPB_" & Me.ComputerName & " ON ##T_R_SPPB_" & Me.ComputerName & "(OA_BRANDPACK_ID,SPPB_DATE) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()

                '========================SHIP TO MANAGER AND DISTRIBUTOR==========================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                         "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_SHIP_TO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                         " BEGIN DROP TABLE TEMPDB..##T_R_OA_SHIP_TO_" & Me.ComputerName & " ; END " & vbCrLf & _
                         " SELECT OST.OA_ID,REG.REGIONAL_AREA,TERR.TERRITORY_AREA,TM.MANAGER INTO ##T_R_OA_SHIP_TO_" & Me.ComputerName & " FROM OA_SHIP_TO OST INNER JOIN SHIP_TO ST ON OST.SHIP_TO_ID = ST.SHIP_TO_ID " & vbCrLf & _
                         " INNER JOIN TERRITORY TERR ON TERR.TERRITORY_ID = ST.TERRITORY_ID INNER JOIN TERRITORY_MANAGER TM ON ST.TM_ID = TM.TM_ID " & vbCrLf & _
                         " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OST.OA_ID = OOA.OA_ID INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OOA.PO_REF_NO INNER JOIN DIST_REGIONAL REG ON REG.REGIONAL_ID = TERR.REGIONAL_ID " & vbCrLf & _
                         " WHERE PO.PO_REF_DATE >= @FROM_DATE ;" & vbCrLf & _
                         " --CREATE NONCLUSTERED INDEX IX_T_R_OA_SHIP_TO_" & Me.ComputerName & " ON ##T_R_OA_SHIP_TO_" & Me.ComputerName & "(OA_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            End If

        End Sub
      
        Public Function getDSSPPBGON(ByVal MasterCategory As String, ByVal CustomCategory As String, ByVal distributorID As Object, _
        ByVal SPPBNO As Object, ByVal PONUmber As Object, ByVal GONNUmber As Object, ByVal FromDate As Object, ByVal UntilDate As Object, ByVal MustReloadData As Boolean) As DataSet
            ''BUAT TABLE Temporary untuk SPPB dan GON

            Dim DS As New DataSet("DSSPPBGON")
            Try
                'untuk category custom
                ''master temporary data hanya ship-to manager
                Select Case MasterCategory
                    Case "ByPO", "BySPPB"
                        If MustReloadData Then
                            Me.CreateTempTableByMasterCategory(Convert.ToDateTime(FromDate), Convert.ToDateTime(UntilDate), MasterCategory)
                        End If
                    Case "ByCustom"
                        Dim QueryCriteria As String = ""
                        Select Case CustomCategory
                            Case "DISTRIBUTOR"
                                If MustReloadData Then
                                    QueryCriteria = " WHERE PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf
                                    Me.CreateTempTableByCustomCategory(QueryCriteria, "@DISTRIBUTOR_ID", distributorID)
                                End If
                            Case "PO_NUMBER"
                                If MustReloadData Then
                                    QueryCriteria = " WHERE PO.PO_REF_NO LIKE '%'+@PO_REF_NO+'%' " & vbCrLf
                                    Me.CreateTempTableByCustomCategory(QueryCriteria, "@PO_REF_NO", PONUmber)
                                End If
                            Case "SPPB_NUMBER"
                                If MustReloadData Then
                                    QueryCriteria = " WHERE PO.PO_REF_NO = ANY(SELECT SH.PO_REF_NO FROM SPPB_HEADER SH WHERE EXISTS(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE SPPB_NO = SH.SPPB_NO) AND SH.SPPB_NO LIKE'%'+@SPPB_NO+'%') "
                                    Me.CreateTempTableByCustomCategory(QueryCriteria, "@SPPB_NO", SPPBNO)
                                End If
                            Case "GON_NUMBER"
                                If MustReloadData Then
                                    QueryCriteria = " WHERE PO.PO_REF_NO = ANY(SELECT SH.PO_REF_NO FROM SPPB_HEADER SH INNER JOIN GON_HEADER GH ON GH.SPPB_NO = SH.SPPB_NO WHERE EXISTS(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE SPPB_NO = SH.SPPB_NO) AND GH.GON_NO LIKE'%'+@GON_NO+'%') "
                                    Me.CreateTempTableByCustomCategory(QueryCriteria, "@GON_NO", GONNUmber)
                                End If
                        End Select
                End Select

                ''CREATE QUERY SPPB

                '======================   QUERY REPORT SPPB   ===================================================================
                If MustReloadData Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                              "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_F_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                              "BEGIN DROP TABLE " & " TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " ; END " & vbCrLf & _
                              "SELECT PO.DISTRIBUTOR_ID,PO.DISTRIBUTOR_NAME,PO.PO_REF_NO,PO.PO_REF_DATE," & vbCrLf & _
                              " PO_CATEGORY = CASE WHEN (PB.PLANTATION_ID IS NOT NULL) THEN 'PLANTATION' " & vbCrLf & _
                              "                WHEN (PB.PROJ_BRANDPACK_ID IS NOT NULL) THEN 'PROJECT' ELSE 'FREE MARKET' END,PB.CLASS_NAME,PB.BRAND_NAME,PB.BRANDPACK_ID,PB.BRANDPACK_NAME," & vbCrLf & _
                              " PB.PO_ORIGINAL_QTY,PB.PRICE,ST.REGIONAL_AREA AS SHIP_TO_REGIONAL,ST.TERRITORY_AREA AS SHIP_TO_TERRITORY,ST.MANAGER AS SALES_PERSON,PB.DESCRIPTIONS2 AS CSE_REMARK,OA.OA_BRANDPACK_ID,SB.SPPB_BRANDPACK_ID,SB.SPPB_NO,SB.SPPB_DATE,SB.CREATE_DATE,SB.SPPB_QTY,SB.BALANCE,SB.STATUS,SB.TOTAL_GON,SB.TOTAL_GON_QTY,SB.LAST_GON_DATE,SB.LAST_GON_RETDATE,SB.CREATE_BY,SB.REMARK,SB.IsUpdatedBySystem " & vbCrLf & _
                              " INTO TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " FROM TEMPDB..##T_HEADER_PO_" & Me.ComputerName & " PO INNER JOIN ##T_PO_DETAIL_" & Me.ComputerName & " PB ON PO.PO_REF_NO = PB.PO_REF_NO " & vbCrLf & _
                              " LEFT OUTER JOIN ##T_R_OA_BrandPack_" & Me.ComputerName & " OA ON OA.PO_BRANDPACK_ID = PB.PO_BRANDPACK_ID AND OA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                              " LEFT OUTER JOIN ##T_R_OA_SHIP_TO_" & Me.ComputerName & " ST ON ST.OA_ID = OA.OA_ID " & vbCrLf & _
                              " LEFT OUTER JOIN ##T_R_SPPB_" & Me.ComputerName & " SB ON SB.OA_BRANDPACK_ID = OA.OA_BRANDPACK_ID " & vbCrLf & _
                              " --CREATE CLUSTERED INDEX IX_T_F_R_SPPB_" & Me.ComputerName & " ON ##T_F_R_SPPB_" & Me.ComputerName & "(SPPB_BRANDPACK_ID,SPPB_DATE,SPPB_NO) ;"

                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                         "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_F_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                         " BEGIN " & vbCrLf & _
                         " SELECT * FROM TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & vbCrLf & _
                         " END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)

                Dim tblSPPB As New DataTable("SPPB_BRANDPACK_INFO")
                tblSPPB.Clear() : Me.setDataAdapter(Me.SqlCom).Fill(tblSPPB) : Me.ClearCommandParameters()

                'CREATE table GON
                Dim tblGON As New DataTable("GON_DETAIL_INFO")
                tblGON.Clear()
                Query = "SET NOCOUNT ON; SET ARITHABORT OFF; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                         "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_F_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                         " BEGIN " & vbCrLf & _
                        " SELECT TFR.DISTRIBUTOR_NAME,GH.GON_HEADER_ID,TFR.PO_CATEGORY,TFR.PO_REF_NO,TFR.PO_REF_DATE,TFR.SHIP_TO_REGIONAL,TFR.SHIP_TO_TERRITORY,TFR.PRICE,TFR.CSE_REMARK,TFR.SPPB_NO,TFR.SPPB_DATE,TFR.CREATE_DATE,TFR.STATUS,TFR.SPPB_QTY,TFR.BALANCE,GH.GON_NO,TFR.BRANDPACK_ID,TFR.BRANDPACK_NAME,GH.GON_DATE," & vbCrLf & _
                        " E_T_A = CASE WHEN GA.DAYS_RECEIPT IS NULL THEN NULL ELSE DATEADD(DAY,(1 + GA.DAYS_RECEIPT),GH.GON_DATE) END, " & vbCrLf & _
                        " GT.GT_ID,GA.GON_ID_AREA,GA.AREA,GT.TRANSPORTER_NAME,TFR.SPPB_BRANDPACK_ID,GD.GON_DETAIL_ID,GD.GON_QTY, " & vbCrLf & _
                        " (ISNULL(GD.GON_QTY,0)/ISNULL(TFR.SPPB_QTY,0)) * PO_ORIGINAL_QTY AS SALES_QTY,GD.BatchNo,GD.UnitOfMeasure,GD.UNIT1,GD.VOL1,GD.UNIT2,GD.VOL2,GD.IsOpen,GH.WARHOUSE,GH.POLICE_NO_TRANS,GH.DRIVER_TRANS," & vbCrLf & _
                        " IsCompleted = CONVERT(BIT,(CASE WHEN (GRB.GRB_ID IS NOT NULL) THEN 1 ELSE 0 END))," & vbCrLf & _
                        " GRB.RETURNED_GON_DATE,GAP = CASE WHEN (GRB.GRB_ID IS NOT NULL) THEN (DATEDIFF(DAY,GH.GON_DATE,GRB.RETURNED_GON_DATE)) ELSE NULL END, GD.CreatedBy,GD.CreatedDate,GD.IsUpdatedBySystem " & vbCrLf & _
                        " FROM TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " TFR INNER JOIN GON_HEADER GH ON GH.SPPB_NO = TFR.SPPB_NO INNER JOIN GON_DETAIL GD ON GH.GON_HEADER_ID = GD.GON_HEADER_ID AND GD.SPPB_BRANDPACK_ID = TFR.SPPB_BRANDPACK_ID " & vbCrLf & _
                        " LEFT OUTER JOIN GON_AREA GA ON GA.GON_ID_AREA = GH.GON_ID_AREA " & vbCrLf & _
                        " LEFT OUTER JOIN GON_TRANSPORTER GT ON GT.GT_ID = GH.GT_ID " & vbCrLf & _
                        " LEFT OUTER JOIN GON_RECEIVED_BACK GRB ON GRB.GRB_ID = GH.GON_HEADER_ID " & vbCrLf & _
                        " END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.setDataAdapter(Me.SqlCom).Fill(tblGON) : Me.ClearCommandParameters()
                'don't close the connection until form is closed also
                DS.Tables.Add(tblSPPB) : DS.Tables.Add(tblGON)
                DS.AcceptChanges()
            Catch ex As Exception
                Me.OpenConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return DS
        End Function
        Public Function getDTSPPBGON(ByVal MasterCategory As String, ByVal CustomCategory As String, ByVal PONumber As String, ByVal SPPBNO As Object, ByVal GONNUmber As Object, ByVal FromDate As Object, ByVal UntilDate As Object, ByVal MustReloadData As Boolean) As DataTable
            ''BUAT TABLE Temporary untuk SPPB dan GON
            Try
                'untuk category custom
                ''master temporary data hanya ship-to manager
                Select Case MasterCategory
                    Case "ByGON", "BySPPB"
                        If MustReloadData Then
                            Me.CreateTempTableByMasterCategory(Convert.ToDateTime(FromDate), Convert.ToDateTime(UntilDate), MasterCategory)
                        End If
                    Case "ByCustom"
                        Dim QueryCriteria As String = ""
                        Select Case CustomCategory
                            Case "PO_NUMBER"
                                If MustReloadData Then
                                    QueryCriteria = " WHERE PO.PO_REF_NO LIKE '%'+@PO_REF_NO+'%' " & vbCrLf
                                    Me.CreateTempTableByCustomCategory(QueryCriteria, "@PO_REF_NO", PONumber)
                                End If
                            Case "SPPB_NUMBER"
                                If MustReloadData Then
                                    QueryCriteria = " WHERE PO.PO_REF_NO = ANY(SELECT SH.PO_REF_NO FROM SPPB_HEADER SH WHERE EXISTS(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE SPPB_NO = SH.SPPB_NO) AND SH.SPPB_NO LIKE '%'+@SPPB_NO+'%') "
                                    Me.CreateTempTableByCustomCategory(QueryCriteria, "@SPPB_NO", SPPBNO)
                                End If
                            Case "GON_NUMBER"
                                If MustReloadData Then
                                    QueryCriteria = " WHERE PO.PO_REF_NO = ANY(SELECT SH.PO_REF_NO FROM SPPB_HEADER SH INNER JOIN GON_HEADER GH ON GH.SPPB_NO = SH.SPPB_NO WHERE EXISTS(SELECT SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE SPPB_NO = SH.SPPB_NO) AND GH.GON_NO LIKE '%'+@GON_NO+'%') "
                                    Me.CreateTempTableByCustomCategory(QueryCriteria, "@GON_NO", GONNUmber)
                                End If
                        End Select
                End Select

                ''CREATE QUERY SPPB

                '======================   QUERY REPORT SPPB   ===================================================================
                If MustReloadData Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                              "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_F_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                              "BEGIN DROP TABLE " & " TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " ; END " & vbCrLf & _
                              "SELECT PO.DISTRIBUTOR_ID,PO.DISTRIBUTOR_NAME,PO.PO_REF_NO,PO.PO_REF_DATE," & vbCrLf & _
                              " PO_CATEGORY = CASE WHEN (PB.PLANTATION_ID IS NOT NULL) THEN 'PLANTATION' " & vbCrLf & _
                              "                WHEN (PB.PROJ_BRANDPACK_ID IS NOT NULL) THEN 'PROJECT' ELSE 'FREE MARKET' END,PB.CLASS_NAME,PB.BRAND_NAME,PB.BRANDPACK_ID,PB.BRANDPACK_NAME," & vbCrLf & _
                              " PB.PO_ORIGINAL_QTY,PB.PRICE,ST.REGIONAL_AREA AS SHIP_TO_REGIONAL,ST.TERRITORY_AREA AS SHIP_TO_TERRITORY,ST.MANAGER AS SALES_PERSON,PB.DESCRIPTIONS2 AS CSE_REMARK,OA.OA_BRANDPACK_ID,SB.SPPB_BRANDPACK_ID,SB.SPPB_NO,SB.SPPB_DATE,SB.CREATE_DATE,SB.SPPB_QTY,SB.BALANCE,SB.STATUS,SB.TOTAL_GON,SB.TOTAL_GON_QTY,SB.LAST_GON_DATE,SB.LAST_GON_RETDATE,SB.CREATE_BY,SB.REMARK,SB.IsUpdatedBySystem " & vbCrLf & _
                              " INTO TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " FROM TEMPDB..##T_HEADER_PO_" & Me.ComputerName & " PO INNER JOIN ##T_PO_DETAIL_" & Me.ComputerName & " PB ON PO.PO_REF_NO = PB.PO_REF_NO " & vbCrLf & _
                              " LEFT OUTER JOIN ##T_R_OA_BrandPack_" & Me.ComputerName & " OA ON OA.PO_BRANDPACK_ID = PB.PO_BRANDPACK_ID AND OA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                              " LEFT OUTER JOIN ##T_R_OA_SHIP_TO_" & Me.ComputerName & " ST ON ST.OA_ID = OA.OA_ID " & vbCrLf & _
                              " LEFT OUTER JOIN ##T_R_SPPB_" & Me.ComputerName & " SB ON SB.OA_BRANDPACK_ID = OA.OA_BRANDPACK_ID " & vbCrLf & _
                              " --CREATE CLUSTERED INDEX IX_T_F_R_SPPB_" & Me.ComputerName & " ON ##T_F_R_SPPB_" & Me.ComputerName & "(SPPB_BRANDPACK_ID,SPPB_DATE,SPPB_NO) ;"

                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If


                'CREATE table GON
                Dim tblGON As New DataTable("GON_DETAIL_INFO")
                tblGON.Clear()
                Query = "SET NOCOUNT ON; SET ARITHABORT OFF; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                         "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_F_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                         " BEGIN " & vbCrLf & _
                        " SELECT TFR.DISTRIBUTOR_NAME,GH.GON_HEADER_ID,TFR.PO_CATEGORY,TFR.PO_REF_NO,TFR.PO_REF_DATE,TFR.SHIP_TO_REGIONAL,TFR.SHIP_TO_TERRITORY,TFR.PRICE,TFR.CSE_REMARK,TFR.SPPB_NO,TFR.SPPB_DATE,TFR.CREATE_DATE,TFR.STATUS,TFR.SPPB_QTY,TFR.BALANCE,GH.GON_NO,TFR.BRANDPACK_ID,TFR.BRANDPACK_NAME,GH.GON_DATE," & vbCrLf & _
                        " E_T_A = CASE WHEN GA.DAYS_RECEIPT IS NULL THEN NULL ELSE DATEADD(DAY,(1 + GA.DAYS_RECEIPT),GH.GON_DATE) END, " & vbCrLf & _
                        " GA.AREA,GT.TRANSPORTER_NAME,GD.GON_QTY, " & vbCrLf & _
                        " (ISNULL(GD.GON_QTY,0)/ISNULL(TFR.SPPB_QTY,0)) * PO_ORIGINAL_QTY AS SALES_QTY,GD.BatchNo,GH.WARHOUSE,GH.POLICE_NO_TRANS,GH.DRIVER_TRANS," & vbCrLf & _
                        " GD.CreatedBy,GD.CreatedDate " & vbCrLf & _
                        " FROM TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " TFR INNER JOIN GON_HEADER GH ON GH.SPPB_NO = TFR.SPPB_NO INNER JOIN GON_DETAIL GD ON GH.GON_HEADER_ID = GD.GON_HEADER_ID AND GD.SPPB_BRANDPACK_ID = TFR.SPPB_BRANDPACK_ID " & vbCrLf & _
                        " LEFT OUTER JOIN GON_AREA GA ON GA.GON_ID_AREA = GH.GON_ID_AREA " & vbCrLf & _
                        " LEFT OUTER JOIN GON_TRANSPORTER GT ON GT.GT_ID = GH.GT_ID " & vbCrLf & _
                        " END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.setDataAdapter(Me.SqlCom).Fill(tblGON) : Me.ClearCommandParameters()
                'don't close the connection until form is closed also
                Return tblGON

            Catch ex As Exception
                Me.OpenConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function getWeelySalesReportByClass(ByVal IsForEntrySPPB As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_SALES_REPORT_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                "BEGIN " & vbCrLf & _
                " DROP TABLE TEMPDB..##T_SALES_REPORT_" & Me.ComputerName & " ;" & vbCrLf & _
                " END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If


                'ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                SqlCom.ExecuteScalar() : ClearCommandParameters()

                If Not IsForEntrySPPB Then
                    'CREATE GON_HEADER TEMPORARY
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " IF OBJECT_ID('TEMPDB..##T_GH_SPPB_" & Me.ComputerName & "') IS NOT NULL " & vbCrLf & _
                    " BEGIN DROP TABLE TEMPDB..##T_GH_SPPB_" & Me.ComputerName & " ; END " & vbCrLf & _
                    " SELECT GH1.GON_HEADER_ID,GH1.GON_ID_AREA,GH1.GT_ID,GH1.SPPB_NO,GH1.GON_NO,GH1.GON_DATE,GH1.WARHOUSE,GH1.POLICE_NO_TRANS,GH1.DRIVER_TRANS,GD.SPPB_BRANDPACK_ID,GD.GON_DETAIL_ID,GD.GON_QTY,GD.BatchNo,GD.UnitOfMeasure,GD.UNIT1,GD.VOL1,GD.UNIT2,GD.VOL2,GD.IsOpen,GD.CreatedBy,GD.CreatedDate,GD.IsUpdatedBySystem INTO TEMPDB..##T_GH_SPPB_" & Me.ComputerName & " FROM GON_HEADER GH1 INNER JOIN GON_DETAIL GD ON GD.GON_HEADER_ID = GH1.GON_HEADER_ID " & vbCrLf & _
                    " INNER JOIN TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " TFR ON (TFR.SPPB_BRANDPACK_ID = GD.SPPB_BRANDPACK_ID AND GH1.SPPB_NO = TFR.SPPB_NO);" & vbCrLf & _
                    " --CREATE CLUSTERED INDEX IX_T_GH_SPPB_" & Me.ComputerName & " ON tempdb..##T_GH_SPPB_" & Me.ComputerName & "(SPPB_NO,SPPB_BRANDPACK_ID) ;"
                    ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    SqlCom.ExecuteScalar() : ClearCommandParameters()

                    Query = "SET NOCOUNT ON; SET ARITHABORT OFF; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                              "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_F_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                              " BEGIN " & vbCrLf & _
                             " SELECT TFR.DISTRIBUTOR_NAME,GH.GON_HEADER_ID,TFR.PO_CATEGORY,TFR.PO_REF_NO,TFR.PO_REF_DATE,TFR.SHIP_TO_REGIONAL,TFR.SHIP_TO_TERRITORY,TFR.BRANDPACK_ID,TFR.CLASS_NAME,TFR.BRAND_NAME,TFR.BRANDPACK_NAME,TFR.PO_ORIGINAL_QTY,TFR.PRICE,TFR.CSE_REMARK,TFR.SPPB_NO,TFR.SPPB_DATE,TFR.CREATE_DATE,TFR.STATUS,TFR.SPPB_QTY,TFR.BALANCE, " & vbCrLf & _
                             " GH.GON_NO,GH.GON_DATE, E_T_A = CASE WHEN GA.DAYS_RECEIPT IS NULL THEN NULL ELSE DATEADD(DAY,(1 + GA.DAYS_RECEIPT),GH.GON_DATE) END,GT.GT_ID,GA.GON_ID_AREA, GA.AREA,GT.TRANSPORTER_NAME,TFR.SPPB_BRANDPACK_ID,GH.GON_DETAIL_ID,GH.GON_QTY, " & vbCrLf & _
                             "  SALES_QTY = CASE WHEN GH.GON_QTY IS NULL THEN TFR.PO_ORIGINAL_QTY ELSE (ISNULL(GH.GON_QTY,0)/ISNULL(TFR.SPPB_QTY,0)) * TFR.PO_ORIGINAL_QTY END,GH.BatchNo,GH.UnitOfMeasure,GH.UNIT1,GH.VOL1,GH.UNIT2,GH.VOL2,GH.IsOpen,GH.WARHOUSE,GH.POLICE_NO_TRANS,GH.DRIVER_TRANS," & vbCrLf & _
                             " IsCompleted = CONVERT(BIT,(CASE WHEN (GRB.GRB_ID IS NOT NULL) THEN 1 ELSE 0 END))," & vbCrLf & _
                             " GRB.RETURNED_GON_DATE,GAP = CASE WHEN (GRB.GRB_ID IS NOT NULL) THEN (DATEDIFF(DAY,GH.GON_DATE,GRB.RETURNED_GON_DATE)) ELSE NULL END, GH.CreatedBy,GH.CreatedDate,GH.IsUpdatedBySystem " & vbCrLf & _
                             " INTO TEMPDB..##T_SALES_REPORT_" & Me.ComputerName & " FROM TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " TFR LEFT OUTER JOIN TEMPDB..##T_GH_SPPB_" & Me.ComputerName & " GH ON (GH.SPPB_NO = TFR.SPPB_NO AND GH.SPPB_BRANDPACK_ID = TFR.SPPB_BRANDPACK_ID) " & vbCrLf & _
                             " LEFT OUTER JOIN GON_AREA GA ON GA.GON_ID_AREA = GH.GON_ID_AREA " & vbCrLf & _
                             " LEFT OUTER JOIN GON_TRANSPORTER GT ON GT.GT_ID = GH.GT_ID " & vbCrLf & _
                             " LEFT OUTER JOIN GON_RECEIVED_BACK GRB ON GRB.GRB_ID = GH.GON_HEADER_ID " & vbCrLf & _
                             " END "
                Else
                    Query = "SET NOCOUNT ON; SET ARITHABORT OFF; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                             "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_F_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                             " BEGIN " & vbCrLf & _
                            " SELECT TFR.DISTRIBUTOR_NAME,GH.GON_HEADER_ID,TFR.PO_CATEGORY,TFR.PO_REF_NO,TFR.PO_REF_DATE,TFR.SHIP_TO_REGIONAL,TFR.SHIP_TO_TERRITORY,TFR.BRANDPACK_NAME,TFR.PRICE,TFR.CSE_REMARK,TFR.SPPB_NO,TFR.STATUS,TFR.SPPB_QTY,TFR.BALANCE,GH.GON_NO,TFR.BRANDPACK_ID,GH.GON_DATE, " & vbCrLf & _
                            " E_T_A = CASE WHEN GA.DAYS_RECEIPT IS NULL THEN NULL ELSE DATEADD(DAY,(1 + GA.DAYS_RECEIPT),GH.GON_DATE) END,GT.GT_ID,GA.GON_ID_AREA, GA.AREA,GT.TRANSPORTER_NAME,TFR.SPPB_BRANDPACK_ID,GD.GON_DETAIL_ID,GD.GON_QTY," & vbCrLf & _
                            " SALES_QTY = CASE WHEN (GD.GON_QTY IS NULL) THEN TFR.PO_ORIGINAL_QTY ELSE (ISNULL(GD.GON_QTY,0)/ISNULL(TFR.SPPB_QTY,0)) * TFR.PO_ORIGINAL_QTY END, GD.BatchNo,GD.UnitOfMeasure,GD.UNIT1,GD.VOL1,GD.UNIT2,GD.VOL2,GD.IsOpen,GH.WARHOUSE,GH.POLICE_NO_TRANS,GH.DRIVER_TRANS," & vbCrLf & _
                            " IsCompleted = CONVERT(BIT,(CASE WHEN (GRB.GRB_ID IS NOT NULL) THEN 1 ELSE 0 END))," & vbCrLf & _
                            " GRB.RETURNED_GON_DATE,GAP = CASE WHEN (GRB.GRB_ID IS NOT NULL) THEN (DATEDIFF(DAY,GH.GON_DATE,GRB.RETURNED_GON_DATE)) ELSE NULL END, GD.CreatedBy,GD.CreatedDate,GD.IsUpdatedBySystem " & vbCrLf & _
                            " INTO TEMPDB..##T_SALES_REPORT_" & Me.ComputerName & "  FROM TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " TFR INNER JOIN GON_HEADER GH ON GH.SPPB_NO = TFR.SPPB_NO INNER JOIN GON_DETAIL GD ON GH.GON_HEADER_ID = GD.GON_HEADER_ID AND GD.SPPB_BRANDPACK_ID = TFR.SPPB_BRANDPACK_ID " & vbCrLf & _
                            " LEFT OUTER JOIN GON_AREA GA ON GA.GON_ID_AREA = GH.GON_ID_AREA " & vbCrLf & _
                            " LEFT OUTER JOIN GON_TRANSPORTER GT ON GT.GT_ID = GH.GT_ID " & vbCrLf & _
                            " LEFT OUTER JOIN GON_RECEIVED_BACK GRB ON GRB.GRB_ID = GH.GON_HEADER_ID " & vbCrLf & _
                            " END "
                    'don't close the connection until form is closed also
                End If
                ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                AddParameter("@stmt", SqlDbType.NVarChar, Query)
                SqlCom.ExecuteScalar() : ClearCommandParameters()

                Query = " SET NOCOUNT ON SET ARITHABORT OFF; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                          " SELECT CLASS_NAME,BRAND_NAME,ISNULL(SUM(CASE Type WHEN 'PURCHASE_ORDER' THEN TOTAL ELSE 0 END),0) AS PURCHASE_ORDER," & vbCrLf & _
                          " ISNULL(SUM(CASE Type WHEN 'SPPB' THEN TOTAL ELSE 0 END),0) AS SPPB,ISNULL(SUM(CASE Type WHEN 'INVOICE' THEN TOTAL ELSE 0 END),0) AS INVOICE," & vbCrLf & _
                          " TOTAL_IN = ISNULL(SUM(CASE Type WHEN 'PURCHASE_ORDER' THEN TOTAL WHEN 'SPPB' THEN TOTAL WHEN 'INVOICE' THEN TOTAL ELSE 0 END),0)  FROM( " & vbCrLf & _
                          " SELECT ISNULL(PC.CLASS_NAME,'UNDEFINED_CLASS') AS CLASS_NAME,TSR.BRAND_NAME, Type = 'INVOICE'," & vbCrLf & _
                          " TOTAL = SUM(CASE WHEN (TSR.GON_QTY IS NOT NULL) THEN ((ISNULL(TSR.GON_QTY,0)/ISNULL(TSR.SPPB_QTY,0)) * TSR.PO_ORIGINAL_QTY * TSR.PRICE) ELSE 0 END) " & vbCrLf & _
                          " FROM TEMPDB..##T_SALES_REPORT_" & Me.ComputerName & "  TSR INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = TSR.BRANDPACK_ID " & vbCrLf & _
                          " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID LEFT OUTER JOIN PRODUCT_CLASS PC ON PC.BRAND_ID = BR.BRAND_ID " & vbCrLf & _
                          " GROUP BY PC.CLASS_NAME,TSR.BRAND_NAME " & vbCrLf & _
                          " UNION " & vbCrLf & _
                          " SELECT ISNULL(PC.CLASS_NAME,'UNDEFINED_CLASS') AS CLASS_NAME,TSR.BRAND_NAME, Type = 'SPPB'," & vbCrLf & _
                          " TOTAL = SUM(CASE WHEN (TSR.SPPB_QTY IS NOT NULL) THEN ((TSR.PO_ORIGINAL_QTY * TSR.PRICE) - ((ISNULL(TSR.GON_QTY,0)/ISNULL(TSR.SPPB_QTY,0)) * TSR.PO_ORIGINAL_QTY) * TSR.PRICE) ELSE 0 END) " & vbCrLf & _
                          " FROM TEMPDB..##T_SALES_REPORT_" & Me.ComputerName & "  TSR INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = TSR.BRANDPACK_ID " & vbCrLf & _
                          " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID LEFT OUTER JOIN  " & vbCrLf & _
                          " PRODUCT_CLASS PC ON PC.BRAND_ID = BR.BRAND_ID WHERE (TSR.SPPB_NO IS NOT NULL AND TSR.STATUS <> 'SHIPPED') " & vbCrLf & _
                          " GROUP BY PC.CLASS_NAME,TSR.BRAND_NAME  " & vbCrLf & _
                          " UNION " & vbCrLf & _
                          " SELECT ISNULL(PC.CLASS_NAME,'UNDEFINED_CLASS') AS CLASS_NAME,TSR.BRAND_NAME, Type = 'PURCHASE_ORDER'," & vbCrLf & _
                          " TOTAL = SUM(CASE WHEN (TSR.SPPB_QTY IS NULL) THEN (TSR.PO_ORIGINAL_QTY * TSR.PRICE) ELSE 0 END) " & vbCrLf & _
                          " FROM TEMPDB..##T_SALES_REPORT_" & Me.ComputerName & " TSR INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = TSR.BRANDPACK_ID " & vbCrLf & _
                          " INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID LEFT OUTER JOIN  " & vbCrLf & _
                          " PRODUCT_CLASS PC ON PC.BRAND_ID = BR.BRAND_ID WHERE TSR.SPPB_NO IS NULL " & vbCrLf & _
                          " GROUP BY PC.CLASS_NAME,TSR.BRAND_NAME " & vbCrLf & _
                          " )G GROUP BY CLASS_NAME,BRAND_NAME ; "
                ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                AddParameter("@stmt", SqlDbType.NVarChar, Query)

                Dim tblSales As New DataTable("SUMMARY OF SALES REPORT BY CLASS")
                setDataAdapter(Me.SqlCom).Fill(tblSales)
                ClearCommandParameters()
                Return tblSales
            Catch ex As Exception
                Me.OpenConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Function getReportSales(ByVal IsForEntrySPPB As Boolean) As DataTable

            Dim tblGon As New DataTable("GON_DETAIL_INFO")
            Try
                If Not IsForEntrySPPB Then
                    'CREATE GON_HEADER TEMPORARY
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " IF OBJECT_ID('TEMPDB..##T_GH_SPPB_" & Me.ComputerName & "') IS NOT NULL " & vbCrLf & _
                    " BEGIN DROP TABLE TEMPDB..##T_GH_SPPB_" & Me.ComputerName & " END " & vbCrLf & _
                    " SELECT GH1.GON_HEADER_ID,GH1.GON_ID_AREA,GH1.GT_ID,GH1.SPPB_NO,GH1.GON_NO,GH1.GON_DATE,GH1.WARHOUSE,GH1.POLICE_NO_TRANS,GH1.DRIVER_TRANS, GD.SPPB_BRANDPACK_ID,GD.GON_DETAIL_ID,GD.GON_QTY,GD.BatchNo,GD.UnitOfMeasure,GD.UNIT1,GD.VOL1,GD.UNIT2,GD.VOL2,GD.IsOpen,GD.CreatedBy,GD.CreatedDate,GD.IsUpdatedBySystem INTO TEMPDB..##T_GH_SPPB_" & Me.ComputerName & " FROM GON_HEADER GH1 INNER JOIN GON_DETAIL GD ON GD.GON_HEADER_ID = GH1.GON_HEADER_ID " & vbCrLf & _
                    " INNER JOIN TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " TFR ON (TFR.SPPB_BRANDPACK_ID = GD.SPPB_BRANDPACK_ID AND GH1.SPPB_NO = TFR.SPPB_NO);" & vbCrLf & _
                    " --CREATE CLUSTERED INDEX IX_T_GH_SPPB_" & Me.ComputerName & " ON tempdb..##T_GH_SPPB_" & Me.ComputerName & "(SPPB_NO,SPPB_BRANDPACK_ID) ;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                    Else : ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    End If

                    'ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                    AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    Me.OpenConnection()
                    SqlCom.ExecuteScalar() : ClearCommandParameters()

                    Query = "SET NOCOUNT ON; SET ARITHABORT OFF; SET ANSI_WARNINGS OFF ;  " & vbCrLf & _
                              "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_F_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                              " BEGIN " & vbCrLf & _
                             " SELECT TFR.DISTRIBUTOR_NAME,GH.GON_HEADER_ID,TFR.PO_CATEGORY,TFR.PO_REF_NO,TFR.PO_REF_DATE,TFR.SHIP_TO_REGIONAL,TFR.SHIP_TO_TERRITORY,TFR.BRANDPACK_ID,TFR.CLASS_NAME,TFR.BRAND_NAME,TFR.BRANDPACK_NAME,TFR.PO_ORIGINAL_QTY,TFR.PRICE,TFR.CSE_REMARK,TFR.SPPB_NO,TFR.SPPB_DATE,TFR.CREATE_DATE,TFR.STATUS,TFR.SPPB_QTY,TFR.BALANCE, " & vbCrLf & _
                             " GH.GON_NO,GH.GON_DATE, E_T_A = CASE WHEN GA.DAYS_RECEIPT IS NULL THEN NULL ELSE DATEADD(DAY,(1 + GA.DAYS_RECEIPT),GH.GON_DATE) END,GT.GT_ID,GA.GON_ID_AREA, GA.AREA,GT.TRANSPORTER_NAME,TFR.SPPB_BRANDPACK_ID,GH.GON_DETAIL_ID,GH.GON_QTY, " & vbCrLf & _
                             " SALES_QTY = CASE WHEN GH.GON_QTY IS NULL THEN TFR.PO_ORIGINAL_QTY ELSE (ISNULL(GH.GON_QTY,0)/ISNULL(TFR.SPPB_QTY,0)) * TFR.PO_ORIGINAL_QTY END,GH.BatchNo,GH.UnitOfMeasure,GH.UNIT1,GH.VOL1,GH.UNIT2,GH.VOL2,GH.IsOpen,GH.WARHOUSE,GH.POLICE_NO_TRANS,GH.DRIVER_TRANS," & vbCrLf & _
                             " TOTAL_SALES_VALUE = (CASE WHEN (GH.GON_QTY IS NULL) THEN TFR.PO_ORIGINAL_QTY ELSE (ISNULL(GH.GON_QTY,0)/ISNULL(TFR.SPPB_QTY,0)) * TFR.PO_ORIGINAL_QTY END) * TFR.PRICE,IsCompleted = CONVERT(BIT,(CASE WHEN (GRB.GRB_ID IS NOT NULL) THEN 1 ELSE 0 END))," & vbCrLf & _
                             " GRB.RETURNED_GON_DATE,GAP = CASE WHEN (GRB.GRB_ID IS NOT NULL) THEN (DATEDIFF(DAY,GH.GON_DATE,GRB.RETURNED_GON_DATE)) ELSE NULL END, GH.CreatedBy,GH.CreatedDate,GH.IsUpdatedBySystem " & vbCrLf & _
                             " FROM TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " TFR LEFT OUTER JOIN TEMPDB..##T_GH_SPPB_" & Me.ComputerName & " GH ON (GH.SPPB_NO = TFR.SPPB_NO AND GH.SPPB_BRANDPACK_ID = TFR.SPPB_BRANDPACK_ID) " & vbCrLf & _
                             " LEFT OUTER JOIN GON_AREA GA ON GA.GON_ID_AREA = GH.GON_ID_AREA " & vbCrLf & _
                             " LEFT OUTER JOIN GON_TRANSPORTER GT ON GT.GT_ID = GH.GT_ID " & vbCrLf & _
                             " LEFT OUTER JOIN GON_RECEIVED_BACK GRB ON GRB.GRB_ID = GH.GON_HEADER_ID " & vbCrLf & _
                             " END "
                Else
                    Query = "SET NOCOUNT ON; SET ARITHABORT OFF; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                             "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_F_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                             " BEGIN " & vbCrLf & _
                            " SELECT TFR.DISTRIBUTOR_NAME,GH.GON_HEADER_ID,TFR.PO_CATEGORY,TFR.PO_REF_NO,TFR.PO_REF_DATE,TFR.SHIP_TO_REGIONAL,TFR.SHIP_TO_TERRITORY,TFR.BRANDPACK_NAME,TFR.PRICE,TFR.CSE_REMARK,TFR.SPPB_NO,TFR.STATUS,TFR.SPPB_QTY,TFR.BALANCE,GH.GON_NO,TFR.BRANDPACK_ID," & vbCrLf & _
                            " E_T_A = CASE WHEN GA.DAYS_RECEIPT IS NULL THEN NULL ELSE DATEADD(DAY,(1 + GA.DAYS_RECEIPT),GH.GON_DATE) END,GT.GT_ID,GA.GON_ID_AREA, GA.AREA,GT.TRANSPORTER_NAME,TFR.SPPB_BRANDPACK_ID,GD.GON_DETAIL_ID,GD.GON_QTY,GH.GON_DATE,GH.WARHOUSE,GH.POLICE_NO_TRANS,GH.DRIVER_TRANS, " & vbCrLf & _
                            " SALES_QTY = CASE WHEN (GD.GON_QTY IS NULL) THEN TFR.PO_ORIGINAL_QTY ELSE (ISNULL(GD.GON_QTY,0)/ISNULL(TFR.SPPB_QTY,0)) * TFR.PO_ORIGINAL_QTY END,GD.BatchNo,GD.UnitOfMeasure,GD.UNIT1,GD.VOL1,GD.UNIT2,GD.VOL2,GD.IsOpen," & vbCrLf & _
                            " TOTAL_SALES_VALUE = (CASE WHEN (GD.GON_QTY IS NULL) THEN TFR.PO_ORIGINAL_QTY ELSE (ISNULL(GD.GON_QTY,0)/ISNULL(TFR.SPPB_QTY,0)) * TFR.PO_ORIGINAL_QTY END) * TFR.PRICE,IsCompleted = CONVERT(BIT,(CASE WHEN (GRB.GRB_ID IS NOT NULL) THEN 1 ELSE 0 END))," & vbCrLf & _
                            " GRB.RETURNED_GON_DATE,GAP = CASE WHEN (GRB.GRB_ID IS NOT NULL) THEN (DATEDIFF(DAY,GH.GON_DATE,GRB.RETURNED_GON_DATE)) ELSE NULL END, GD.CreatedBy,GD.CreatedDate,GD.IsUpdatedBySystem " & vbCrLf & _
                            " FROM TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " TFR INNER JOIN GON_HEADER GH ON GH.SPPB_NO = TFR.SPPB_NO INNER JOIN GON_DETAIL GD ON GH.GON_HEADER_ID = GD.GON_HEADER_ID AND GD.SPPB_BRANDPACK_ID = TFR.SPPB_BRANDPACK_ID " & vbCrLf & _
                            " LEFT OUTER JOIN GON_AREA GA ON GA.GON_ID_AREA = GH.GON_ID_AREA " & vbCrLf & _
                            " LEFT OUTER JOIN GON_TRANSPORTER GT ON GT.GT_ID = GH.GT_ID " & vbCrLf & _
                            " LEFT OUTER JOIN GON_RECEIVED_BACK GRB ON GRB.GRB_ID = GH.GON_HEADER_ID " & vbCrLf & _
                            " END "
                    'don't close the connection until form is closed also
                End If
                CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.setDataAdapter(Me.SqlCom).Fill(tblGon) : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.OpenConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return tblGon

        End Function
        Public Sub DisposeTempDB()
            Try
                'TEMPDB..##T_HEADER_PO_" & Me.ComputerName
                '##T_PO_DETAIL_" & Me.ComputerName &
                '##T_R_OA_BrandPack_" & Me.ComputerName
                '##T_R_OA_SHIP_TO_" & Me.ComputerName
                '##T_R_SPPB_" & Me.ComputerName

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "IF OBJECT_ID('TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & "') IS NOT NULL " & vbCrLf & _
                " BEGIN DROP TABLE TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & ";  END " & vbCrLf & _
                " IF OBJECT_ID('TEMPDB..##T_HEADER_PO_" & Me.ComputerName & "') IS NOT NULL " & vbCrLf & _
                " BEGIN DROP TABLE TEMPDB..##T_HEADER_PO_" & Me.ComputerName & ";  END " & vbCrLf & _
                " IF OBJECT_ID('TEMPDB..##T_PO_DETAIL_" & Me.ComputerName & "') IS NOT NULL " & vbCrLf & _
                " BEGIN DROP TABLE ##T_PO_DETAIL_" & Me.ComputerName & ";  END " & vbCrLf & _
                " IF OBJECT_ID('TEMPDB..##T_R_OA_BrandPack_" & Me.ComputerName & "') IS NOT NULL " & vbCrLf & _
                " BEGIN DROP TABLE ##T_R_OA_BrandPack_" & Me.ComputerName & ";  END " & vbCrLf & _
                " IF OBJECT_ID('TEMPDB..##T_R_OA_SHIP_TO_" & Me.ComputerName & "') IS NOT NULL " & vbCrLf & _
                " BEGIN DROP TABLE ##T_R_OA_SHIP_TO_" & Me.ComputerName & ";  END " & vbCrLf & _
                " IF OBJECT_ID('TEMPDB..##T_R_SPPB_" & Me.ComputerName & "') IS NOT NULL " & vbCrLf & _
                " BEGIN DROP TABLE ##T_R_SPPB_" & Me.ComputerName & ";  END "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
    End Class
End Namespace

