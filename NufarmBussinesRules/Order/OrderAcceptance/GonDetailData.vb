Imports System.Data
Imports System.Data.SqlClient
Namespace OrderAcceptance
    Public Class GonDetailData
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        Protected Query As String
        Public Sub New()
            MyBase.New()
        End Sub
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Me.DisposeTempDB()
            MyBase.Dispose(disposing)
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
                "SELECT PO.PO_REF_NO,OPB.PO_BRANDPACK_ID,OPB.PROJ_BRANDPACK_ID,OPB.BRANDPACK_ID, " & vbCrLf & _
                " BP.BRANDPACK_NAME,BP.DEVIDED_QUANTITY,OPB.PO_PRICE_PERQTY AS PRICE,OPB.PO_ORIGINAL_QTY,OPB.PLANTATION_ID,PL.PLANTATION_NAME,OPB.DESCRIPTIONS2 " & vbCrLf & _
                " INTO ##T_PO_DETAIL_" & Me.ComputerName & " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = OPB.BRANDPACK_ID INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID " & vbCrLf & _
                " LEFT OUTER JOIN PLANTATION PL ON PL.PLANTATION_ID = OPB.PLANTATION_ID " & vbCrLf
            Query &= QueryCriteria & vbCrLf
            Query &= " --CREATE NONCLUSTERED INDEX IX_T_PO_DETAIL_" & Me.ComputerName & " ON ##T_PO_DETAIL_" & Me.ComputerName & "(PO_REF_NO,PO_BRANDPACK_ID) ;"
            Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()
            '-----------------------------------------------------------------------------------------------------------------------

            '=====================  OA BRANDPACK    =================================================================
            Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_BrandPack_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                    " BEGIN DROP TABLE TEMPDB..##T_R_OA_BrandPack_" & Me.ComputerName & " ; END " & vbCrLf & _
                    "SELECT OA.PO_REF_NO,OA.OA_ID,OPB.PO_BRANDPACK_ID,ISNULL(T_DISC.TOTAL_DISC,0)AS TOTAL_DISC,OA.OA_BRANDPACK_ID INTO ##T_R_OA_BrandPack_" & Me.ComputerName & vbCrLf & _
                    " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                    " LEFT OUTER JOIN (" & vbCrLf & _
                    "                   SELECT OOA.PO_REF_NO,OOA.OA_ID,OOB.OA_BRANDPACK_ID,OOB.OA_ORIGINAL_QTY AS OA_QTY,OOB.PO_BRANDPACK_ID FROM ORDR_ORDER_ACCEPTANCE OOA  " & vbCrLf & _
                    "                   INNER JOIN ORDR_OA_BRANDPACK OOB ON OOA.OA_ID = OOB.OA_ID   " & vbCrLf & _
                    "                  )OA " & vbCrLf & _
                    " ON OA.PO_REF_NO = PO.PO_REF_NO AND OA.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                    " LEFT OUTER JOIN( " & vbCrLf & _
                    " SELECT OA_BRANDPACK_ID,ISNULL(SUM(DISC_QTY),0)AS TOTAL_DISC FROM ORDR_OA_BRANDPACK_DISC " & vbCrLf & _
                    " WHERE GQSY_SGT_P_FLAG != 'RMOA'" & vbCrLf & _
                    " GROUP BY OA_BRANDPACK_ID " & vbCrLf & _
                    " )T_DISC ON OA.OA_BRANDPACK_ID = T_DISC.OA_BRANDPACK_ID "
            Query &= QueryCriteria & vbCrLf
            Query &= " --CREATE NONCLUSTERED INDEX IX_T_R_OA_BrandPack_" & Me.ComputerName & " ON ##T_R_OA_BrandPack_" & Me.ComputerName & "(PO_REF_NO,OA_BRANDPACK_ID,PO_BRANDPACK_ID) ;"
            Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()


            '====================   SPPB HEADER And Detail   ==========================================================================

            Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                    " BEGIN DROP TABLE TEMPDB..##T_R_SPPB_" & Me.ComputerName & " ; END " & vbCrLf & _
                    " SELECT SH.SPPB_NO,SH.SPPB_DATE,SH.CREATE_DATE,SB1.OA_BRANDPACK_ID,SB1.SPPB_BRANDPACK_ID,SB1.SPPB_QTY," & vbCrLf & _
                    " SB1.STATUS,BALANCE = SB1.SPPB_QTY - ISNULL(GON.TOTAL_GON_QTY,0),GON.TOTAL_GON,GON.TOTAL_GON_QTY AS TOTAL_GON_QTY,GON.LAST_GON_DATE," & vbCrLf & _
                    " SB1.REMARK,SB1.CREATE_BY INTO ##T_R_SPPB_" & Me.ComputerName & " FROM SPPB_HEADER SH INNER JOIN SPPB_BRANDPACK SB1 ON SH.SPPB_NO = SB1.SPPB_NO " & vbCrLf & _
                    " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = SH.PO_REF_NO " & vbCrLf & _
                    " LEFT OUTER JOIN (SELECT GD.SPPB_BRANDPACK_ID,SUM(GD.GON_QTY)AS TOTAL_GON_QTY,COUNT(SPPB_BRANDPACK_ID)AS TOTAL_GON,MAX(GH.GON_DATE)AS LAST_GON_DATE " & vbCrLf & _
                    " FROM GON_DETAIL GD INNER JOIN GON_HEADER GH " & vbCrLf & _
                    " ON GD.GON_HEADER_ID = GH.GON_HEADER_ID GROUP BY GD.SPPB_BRANDPACK_ID)GON " & vbCrLf & _
                    " ON GON.SPPB_BRANDPACK_ID = SB1.SPPB_BRANDPACK_ID  "
            Query &= QueryCriteria & vbCrLf
            Query &= " --CREATE NONCLUSTERED INDEX IX_T_R_SPPB_" & Me.ComputerName & " ON ##T_R_SPPB_" & Me.ComputerName & "(OA_BRANDPACK_ID,SPPB_DATE) ;"
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
            If MasterCategory = "ByGON" Then
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
                Me.AddParameter("@UNTIL_DATE", SqlDbType.SmallDateTime, CDate(UntilDate).AddMonths(3))
                Me.SqlCom.ExecuteScalar()
                '----------------------------------------------------------------------------------------------

                '======================  PO DETAIL     ================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PO_DETAIL_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                " BEGIN DROP TABLE TEMPDB..##T_PO_DETAIL_" & Me.ComputerName & " ; END " & vbCrLf & _
                " SELECT PO.PO_REF_NO,OPB.PO_BRANDPACK_ID,OPB.PROJ_BRANDPACK_ID,OPB.BRANDPACK_ID," & vbCrLf & _
                " BP.BRANDPACK_NAME,BP.DEVIDED_QUANTITY,OPB.PO_PRICE_PERQTY AS PRICE,OPB.PO_ORIGINAL_QTY,OPB.PLANTATION_ID,PL.PLANTATION_NAME,OPB.DESCRIPTIONS2 " & vbCrLf & _
                " INTO ##T_PO_DETAIL_" & Me.ComputerName & " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = OPB.BRANDPACK_ID " & vbCrLf & _
                " LEFT OUTER JOIN PLANTATION PL ON PL.PLANTATION_ID = OPB.PLANTATION_ID " & vbCrLf & _
                " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ; " & vbCrLf & _
                " --CREATE NONCLUSTERED INDEX IX_T_PO_DETAIL_" & Me.ComputerName & " ON ##T_PO_DETAIL_" & Me.ComputerName & "(PO_REF_NO,PO_BRANDPACK_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()
                '-----------------------------------------------------------------------------------------------------------------------

                '=====================  OA BRANDPACK    =================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_BrandPack_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_OA_BrandPack_" & Me.ComputerName & " ; END " & vbCrLf & _
                        "SELECT OA.PO_REF_NO,OPB.PO_BRANDPACK_ID,OA.OA_ID,OA.OA_BRANDPACK_ID,ISNULL(T_DISC.TOTAL_DISC,0)AS TOTAL_DISC INTO ##T_R_OA_BrandPack_" & Me.ComputerName & vbCrLf & _
                        " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN (" & vbCrLf & _
                        "                   SELECT OOA.PO_REF_NO,OOA.OA_ID,OOB.OA_BRANDPACK_ID,OOB.OA_ORIGINAL_QTY AS OA_QTY,OOB.PO_BRANDPACK_ID FROM ORDR_ORDER_ACCEPTANCE OOA  " & vbCrLf & _
                        "                   INNER JOIN ORDR_OA_BRANDPACK OOB ON OOA.OA_ID = OOB.OA_ID WHERE OOA.OA_DATE >= @FROM_DATE  " & vbCrLf & _
                        "                  )OA " & vbCrLf & _
                        " ON OA.PO_REF_NO = PO.PO_REF_NO AND OA.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                        " LEFT OUTER JOIN(" & vbCrLf & _
                        " SELECT OA_BRANDPACK_ID,ISNULL(SUM(DISC_QTY),0)AS TOTAL_DISC FROM ORDR_OA_BRANDPACK_DISC " & vbCrLf & _
                        " WHERE GQSY_SGT_P_FLAG != 'RMOA' " & vbCrLf & _
                        " GROUP BY OA_BRANDPACK_ID " & vbCrLf & _
                        ")T_DISC ON OA.OA_BRANDPACK_ID = T_DISC.OA_BRANDPACK_ID " & vbCrLf & _
                        " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                        " --CREATE NONCLUSTERED INDEX IX_T_R_OA_BrandPack_" & Me.ComputerName & " ON ##T_R_OA_BrandPack_" & Me.ComputerName & "(PO_REF_NO,OA_BRANDPACK_ID,PO_BRANDPACK_ID) ;"
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
                '====================   SPPB HEADER And Detail   ==========================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_SPPB_" & Me.ComputerName & " ; END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT SH.SPPB_NO,SH.SPPB_DATE,SB1.OA_BRANDPACK_ID,SB1.SPPB_BRANDPACK_ID,SB1.SPPB_QTY," & vbCrLf & _
                 " SB1.STATUS,BALANCE = SB1.SPPB_QTY - ISNULL(GON.TOTAL_GON_QTY,0),GON.TOTAL_GON,GON.TOTAL_GON_QTY AS TOTAL_GON_QTY,GON.LAST_GON_DATE," & vbCrLf & _
                 " SB1.REMARK,SB1.CREATE_DATE,SB1.CREATE_BY INTO ##T_R_SPPB_" & Me.ComputerName & " FROM SPPB_HEADER SH INNER JOIN SPPB_BRANDPACK SB1 ON SH.SPPB_NO = SB1.SPPB_NO " & vbCrLf & _
                 " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = SH.PO_REF_NO " & vbCrLf & _
                 " LEFT OUTER JOIN (SELECT GD.SPPB_BRANDPACK_ID,SUM(GD.GON_QTY)AS TOTAL_GON_QTY,COUNT(SPPB_BRANDPACK_ID)AS TOTAL_GON,MAX(GH.GON_DATE)AS LAST_GON_DATE " & vbCrLf & _
                 " FROM GON_DETAIL GD INNER JOIN GON_HEADER GH " & vbCrLf & _
                 " ON GD.GON_HEADER_ID = GH.GON_HEADER_ID WHERE GH.GON_DATE >= @FROM_DATE AND GH.GON_DATE <= @UNTIL_DATE GROUP BY GD.SPPB_BRANDPACK_ID)GON " & vbCrLf & _
                 " ON GON.SPPB_BRANDPACK_ID = SB1.SPPB_BRANDPACK_ID " & vbCrLf & _
                 " --WHERE GON.GON_DATE >= @FROM_DATE AND GH.GON_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                 " --CREATE NONCLUSTERED INDEX IX_T_R_SPPB_" & Me.ComputerName & " ON ##T_R_SPPB_" & Me.ComputerName & "(OA_BRANDPACK_ID,SPPB_DATE) ;"
                Me.AddParameter("@FROM_DATE", SqlDbType.SmallDateTime, FromDate)
                Me.AddParameter("@UNTIL_DATE", SqlDbType.SmallDateTime, UntilDate)
                'Me.AddParameter("@GON_UNTIL_DATE", SqlDbType.SmallDateTime, CDate(UntilDate).AddMonths(2))
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

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
                Me.AddParameter("@UNTIL_DATE", SqlDbType.SmallDateTime, CDate(UntilDate).AddMonths(3))
                Me.SqlCom.ExecuteScalar()
                '-------------------------------------------------------------------------------------------------------------------------------

                '======================== PO DETAIL  ================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PO_DETAIL_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                "BEGIN DROP TABLE TEMPDB..##T_PO_DETAIL_" & Me.ComputerName & " ; END " & vbCrLf & _
                "SELECT PO.PO_REF_NO,OPB.PO_BRANDPACK_ID,OPB.PROJ_BRANDPACK_ID,OPB.BRANDPACK_ID,BP.BRANDPACK_NAME,BP.DEVIDED_QUANTITY,OPB.PO_PRICE_PERQTY AS PRICE,OPB.PO_ORIGINAL_QTY,OPB.PLANTATION_ID,PL.PLANTATION_NAME,OPB.DESCRIPTIONS2 " & vbCrLf & _
                "INTO ##T_PO_DETAIL_" & Me.ComputerName & " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = OPB.BRANDPACK_ID INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID " & vbCrLf & _
                " LEFT OUTER JOIN PLANTATION PL ON PL.PLANTATION_ID = OPB.PLANTATION_ID " & vbCrLf & _
                " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ; " & vbCrLf & _
                " --CREATE NONCLUSTERED INDEX IX_T_PO_DETAIL_" & Me.ComputerName & " ON ##T_PO_DETAIL_" & Me.ComputerName & "(PO_REF_NO,PO_BRANDPACK_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()
                '-----------------------------------------------------------------------------------------------------------------------

                '======================= OA BRANDPACK =================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_BrandPack_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_OA_BrandPack_" & Me.ComputerName & " ; END " & vbCrLf & _
                        "SELECT OA.PO_REF_NO,OPB.PO_BRANDPACK_ID,OA.OA_ID,OA.OA_BRANDPACK_ID,ISNULL(T_DISC.TOTAL_DISC,0)AS TOTAL_DISC INTO ##T_R_OA_BrandPack_" & Me.ComputerName & vbCrLf & _
                        " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN (" & vbCrLf & _
                        "                   SELECT OOA.PO_REF_NO,OOA.OA_ID,OOB.OA_BRANDPACK_ID,OOB.OA_ORIGINAL_QTY AS OA_QTY,OOB.PO_BRANDPACK_ID FROM ORDR_ORDER_ACCEPTANCE OOA  " & vbCrLf & _
                        "                   INNER JOIN ORDR_OA_BRANDPACK OOB ON OOA.OA_ID = OOB.OA_ID WHERE OOA.OA_DATE >= @FROM_DATE  " & vbCrLf & _
                        "                  )OA " & vbCrLf & _
                        " ON OA.PO_REF_NO = PO.PO_REF_NO AND OA.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                        " LEFT OUTER JOIN( " & vbCrLf & _
                        " SELECT OA_BRANDPACK_ID,ISNULL(SUM(DISC_QTY),0)AS TOTAL_DISC FROM ORDR_OA_BRANDPACK_DISC " & vbCrLf & _
                        " WHERE GQSY_SGT_P_FLAG != 'RMOA'" & vbCrLf & _
                        " GROUP BY OA_BRANDPACK_ID " & vbCrLf & _
                        ")T_DISC ON OA.OA_BRANDPACK_ID = T_DISC.OA_BRANDPACK_ID " & vbCrLf & _
                        " WHERE PO.PO_REF_DATE >= @FROM_DATE  ;" & vbCrLf & _
                        " --CREATE NONCLUSTERED INDEX IX_T_R_OA_BrandPack_" & Me.ComputerName & " ON ##T_R_OA_BrandPack_" & Me.ComputerName & "(PO_REF_NO,OA_BRANDPACK_ID,PO_BRANDPACK_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()
                '----------------------------------------------------------------------------------------------
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

                '========================   SPPB    ==========================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_SPPB_" & Me.ComputerName & " ; END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT SH.SPPB_NO,SH.SPPB_DATE,SH.CREATE_DATE,SB1.OA_BRANDPACK_ID,SB1.SPPB_BRANDPACK_ID,SB1.SPPB_QTY," & vbCrLf & _
                        " SB1.STATUS,BALANCE = SB1.SPPB_QTY - ISNULL(GON.TOTAL_GON_QTY,0),GON.TOTAL_GON,GON.TOTAL_GON_QTY AS TOTAL_GON_QTY,GON.LAST_GON_DATE," & vbCrLf & _
                        " SB1.REMARK,SB1.CREATE_BY INTO ##T_R_SPPB_" & Me.ComputerName & " FROM SPPB_HEADER SH INNER JOIN SPPB_BRANDPACK SB1 ON SH.SPPB_NO = SB1.SPPB_NO " & vbCrLf & _
                        " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = SH.PO_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN (SELECT GD.SPPB_BRANDPACK_ID,SUM(GD.GON_QTY)AS TOTAL_GON_QTY,COUNT(GD.SPPB_BRANDPACK_ID)AS TOTAL_GON,MAX(GH.GON_DATE)AS LAST_GON_DATE " & vbCrLf & _
                        " FROM GON_DETAIL GD INNER JOIN GON_HEADER GH " & vbCrLf & _
                        " ON GD.GON_HEADER_ID = GH.GON_HEADER_ID WHERE GH.GON_DATE >= @FROM_DATE  AND GH.GON_DATE <= @GON_UNTIL_DATE GROUP BY GD.SPPB_BRANDPACK_ID)GON " & vbCrLf & _
                        " ON GON.SPPB_BRANDPACK_ID = SB1.SPPB_BRANDPACK_ID  " & vbCrLf & _
                        " WHERE SH.SPPB_DATE >= @FROM_DATE AND SH.SPPB_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                        " --CREATE NONCLUSTERED INDEX IX_T_R_SPPB_" & Me.ComputerName & " ON ##T_R_SPPB_" & Me.ComputerName & "(OA_BRANDPACK_ID,SPPB_DATE) ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@FROM_DATE", SqlDbType.SmallDateTime, FromDate)
                Me.AddParameter("@UNTIL_DATE", SqlDbType.SmallDateTime, UntilDate)
                Me.AddParameter("@GON_UNTIL_DATE", SqlDbType.SmallDateTime, CDate(UntilDate).AddMonths(2))
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

            End If

        End Sub

        
        Public Function getDTSPPBGON(ByVal MasterCategory As String, ByVal CustomCategory As String, ByVal PONumber As String, ByVal SPPBNO As Object, ByVal GONNUmber As Object, ByVal FromDate As Object, ByVal UntilDate As Object, ByVal MustReloadData As Boolean, ByRef TblConv As DataTable) As DataTable
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
                              "                WHEN (PB.PROJ_BRANDPACK_ID IS NOT NULL) THEN 'PROJECT' ELSE 'FREE MARKET' END,PB.BRANDPACK_ID,PB.BRANDPACK_NAME,PB.DEVIDED_QUANTITY," & vbCrLf & _
                              " PB.PO_ORIGINAL_QTY,OA.TOTAL_DISC,PB.PRICE,ST.REGIONAL_AREA AS SHIP_TO_REGIONAL,ST.TERRITORY_AREA AS SHIP_TO_TERRITORY,ST.MANAGER AS SALES_PERSON,PB.DESCRIPTIONS2 AS CSE_REMARK,OA.OA_BRANDPACK_ID,SB.SPPB_BRANDPACK_ID,SB.SPPB_NO,SB.SPPB_DATE,SB.CREATE_DATE,SB.SPPB_QTY,SB.BALANCE,SB.STATUS,SB.TOTAL_GON,SB.TOTAL_GON_QTY,SB.LAST_GON_DATE,SB.CREATE_BY,SB.REMARK " & vbCrLf & _
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
                        " SELECT TFR.SPPB_NO+TFR.BRANDPACK_ID AS [ID],TFR.DISTRIBUTOR_NAME,GH.GON_HEADER_ID,TFR.PO_CATEGORY,TFR.PO_REF_NO,TFR.PO_REF_DATE,TFR.SHIP_TO_REGIONAL,TFR.SHIP_TO_TERRITORY,TFR.PRICE,TFR.CSE_REMARK,TFR.SPPB_NO,TFR.SPPB_DATE,TFR.CREATE_DATE,TFR.STATUS,TFR.BALANCE,GH.GON_NO,TFR.BRANDPACK_ID,TFR.BRANDPACK_NAME,GH.GON_DATE," & vbCrLf & _
                        " E_T_A = CASE WHEN GA.DAYS_RECEIPT IS NULL THEN NULL ELSE DATEADD(DAY,(1 + GA.DAYS_RECEIPT),GH.GON_DATE) END, " & vbCrLf & _
                        " GA.AREA,GT.TRANSPORTER_NAME, " & vbCrLf & _
                        " TFR.DEVIDED_QUANTITY,TFR.PO_ORIGINAL_QTY,TFR.TOTAL_DISC,TFR.SPPB_QTY,GD.GON_QTY,TFR.TOTAL_GON," & vbCrLf & _
                        " GON_PO = CASE WHEN (TOTAL_GON = 1) THEN TFR.PO_ORIGINAL_QTY " & vbCrLf & _
                        "               WHEN (TOTAL_GON > 1) THEN 0 ELSE 0 END," & vbCrLf & _
                        " GON_DISC_INC = CASE WHEN (TOTAL_GON = 1) THEN TFR.TOTAL_DISC " & vbCrLf & _
                        " WHEN (TOTAL_GON > 1) THEN 0 ELSE 0 END,GD.BatchNo,GH.WARHOUSE,GH.POLICE_NO_TRANS,GH.DRIVER_TRANS," & vbCrLf & _
                        " GD.CreatedBy,GD.CreatedDate " & vbCrLf & _
                        " FROM TEMPDB..##T_F_R_SPPB_" & Me.ComputerName & " TFR INNER JOIN GON_HEADER GH ON GH.SPPB_NO = TFR.SPPB_NO INNER JOIN GON_DETAIL GD ON GH.GON_HEADER_ID = GD.GON_HEADER_ID AND GD.SPPB_BRANDPACK_ID = TFR.SPPB_BRANDPACK_ID " & vbCrLf & _
                        " LEFT OUTER JOIN GON_AREA GA ON GA.GON_ID_AREA = GH.GON_ID_AREA " & vbCrLf & _
                        " LEFT OUTER JOIN GON_TRANSPORTER GT ON GT.GT_ID = GH.GT_ID " & vbCrLf & _
                        " END "
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.setDataAdapter(Me.SqlCom).Fill(tblGON) : Me.ClearCommandParameters()
                'don't close the connection until form is closed also
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                                   "SELECT BRANDPACK_ID,UNIT1,VOL1,UNIT2,VOL2,INACTIVE FROM BRND_PROD_CONV;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                setDataAdapter(Me.SqlCom).Fill(TblConv)
                Me.ClearCommandParameters()
                Return tblGON

            Catch ex As Exception
                Me.OpenConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
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

