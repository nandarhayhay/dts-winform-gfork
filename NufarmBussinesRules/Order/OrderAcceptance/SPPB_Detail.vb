Imports System.Data.SqlClient
Namespace OrderAcceptance
    Public Class SPPB_Detail
        Inherits SPPB
        Private m_ViewSPPBDetail As DataView
        Private m_ViewOARefNo As DataView
        Public OA_REF_NO As String
        Public OA_DATE As Date
        Public SHIP_TERRITORY As Object
        'Public SPPB_DATE As Object
        Private m_ViewBrandPack As DataView
        Private m_ViewDistributorSPPB As DataView
        Public GON_NO As Object
        Public GON_DATE As Object
        Public ChangedGonNumber As String = ""
        Public ChangedGSNumber As String = ""
        Public ChangedGonDate As DateTime
        Public GT_ID As String = ""
        Public HasStamped As Boolean = False, HasSigned As Boolean = False, GONReceiver As String = "", ReceivedGonDate As DateTime, ReceiverID As String = "", GOnReceiverName As String = "", Remark As String = ""

#Region " Function "

        Public Function SearchOA_REF_NO(ByVal OA_Search As String, ByVal DISTRIBUTOR_ID As String, ByVal mustCloseConnection As Boolean) As DataView
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Search_OA_Ref_NO_SPPB_Entry", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Search_OA_Ref_NO_SPPB_Entry")
                End If
                Me.AddParameter("@SEARCH_OA_REF_NO", SqlDbType.VarChar, OA_Search, 70) ' VARCHAR(70),
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10)
                Dim tbl As New DataTable("OA_REF_NO")
                tbl.Clear()
                Me.OpenConnection() : Me.setDataAdapter(Me.SqlCom).Fill(tbl) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If

                'Me.FillDataTable(tbl)
                Me.m_ViewOARefNo = tbl.DefaultView()
                Me.m_ViewOARefNo.Sort = "OA_REF_NO"
                Return Me.m_ViewOARefNo
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Overloads Function CreateViewOARefNo(ByVal DISTRIBUTOR_ID As String, ByVal ISREGISTERED As Boolean, ByVal mustCloseConnection As Boolean) As DataView
            'Usp_Create_View_OA_Ref_No_SPPB_Entry()
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Create_View_OA_Ref_No_SPPB_Entry", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Create_View_OA_Ref_No_SPPB_Entry")
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                If ISREGISTERED Then
                    Me.AddParameter("@ISREGISTEREDSPPB", SqlDbType.Bit, 1)
                Else
                    Me.AddParameter("@ISREGISTEREDSPPB", SqlDbType.Bit, 0)
                End If
                Dim tblOAREFNo As New DataTable("OA_REF_NO")
                tblOAREFNo.Clear()
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(tblOAREFNo) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If

                Me.ClearCommandParameters()

                'Me.FillDataTable(tblOAREFNo)
                Me.m_ViewOARefNo = tblOAREFNo.DefaultView()
                Me.m_ViewOARefNo.Sort = "OA_REF_NO"
                Return Me.m_ViewOARefNo
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function CreateViewSPPBDetail(ByVal SPPB_NO As String, ByVal mustCloseConnection As Boolean) As DataView
            Try
                ''check apakah data PO Ada yang di rubah
                'dim Query as String  = "";
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Create_View_SPPB_Detail_Entry", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Create_View_SPPB_Detail_Entry")
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 15)
                Dim dtTable As New DataTable("SPPB_Entry")
                dtTable.Clear()
                Me.OpenConnection()
                'Me.FillDataTable(dtTable)
                Me.setDataAdapter(Me.SqlCom).Fill(dtTable) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.baseDataSet = New DataSet
                Me.baseDataSet.Tables.Add(dtTable)
                Me.baseDataSet.AcceptChanges()

                Me.m_ViewSPPBDetail = dtTable.DefaultView()
                Me.m_ViewSPPBDetail.Sort = "SPPB_BRANDPACK_ID"
                Me.m_ViewSPPBDetail.RowStateFilter = DataViewRowState.CurrentRows
                Return Me.m_ViewSPPBDetail
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Sub CreateViewSPPBReport(ByVal FromDate As DateTime, ByVal UntilDate As DateTime)
            Try
                'start counting this 

                '==================    PO HEADER      ==============================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_HEADER_PO_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                "BEGIN DROP TABLE TEMPDB..##T_HEADER_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
                "SELECT PO.PO_REF_NO,PO.DISTRIBUTOR_ID,PO.PO_REF_DATE,PO.PROJ_REF_NO,P.PROJECT_NAME,DR.DISTRIBUTOR_NAME INTO ##T_HEADER_PO_" & Me.ComputerName & " FROM ORDR_PURCHASE_ORDER PO INNER JOIN DIST_DISTRIBUTOR DR " & vbCrLf & _
                " ON PO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID LEFT OUTER JOIN PROJ_PROJECT P ON PO.PROJ_REF_NO = P.PROJ_REF_NO WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                " CREATE CLUSTERED INDEX IX_T_HEADER_PO_" & Me.ComputerName & " ON ##T_HEADER_PO_" & Me.ComputerName & "(PO_REF_NO);"
                Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                Me.OpenConnection()
                Me.AddParameter("@FROM_DATE", SqlDbType.SmallDateTime, FromDate)
                Me.AddParameter("@UNTIL_DATE", SqlDbType.SmallDateTime, UntilDate)
                Me.SqlCom.ExecuteScalar()

                '====================   PO DETAIL     ================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PO_DETAIL_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                "BEGIN DROP TABLE TEMPDB..##T_PO_DETAIL_" & Me.ComputerName & " ; END " & vbCrLf & _
                "SELECT PO.PO_REF_NO,BR.BRAND_NAME,OPB.PO_BRANDPACK_ID,OPB.BRANDPACK_ID,BP.BRANDPACK_NAME,OPB.PO_PRICE_PERQTY AS PRICE,OPB.PO_ORIGINAL_QTY,OPB.PLANTATION_ID,PL.PLANTATION_NAME," & vbCrLf & _
                "OPB.DESCRIPTIONS INTO ##T_PO_DETAIL_" & Me.ComputerName & " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = OPB.BRANDPACK_ID INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BP.BRAND_ID " & vbCrLf & _
                " LEFT OUTER JOIN PLANTATION PL ON PL.PLANTATION_ID = OPB.PLANTATION_ID " & vbCrLf & _
                " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                " CREATE CLUSTERED INDEX IX_T_PO_DETAIL_" & Me.ComputerName & " ON ##T_PO_DETAIL_" & Me.ComputerName & "(PO_REF_NO,PO_BRANDPACK_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()


                '====================   OA BRANDPACK    =================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_BrandPack_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_OA_BrandPack_" & Me.ComputerName & " ; END " & vbCrLf & _
                        "SELECT OA.PO_REF_NO,OA.OA_ID AS OA_REF_NO,OPB.PO_BRANDPACK_ID,OA.OA_QTY,OA.OA_BRANDPACK_ID INTO ##T_R_OA_BrandPack_" & Me.ComputerName & vbCrLf & _
                        " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN (" & vbCrLf & _
                        "                   SELECT OOA.PO_REF_NO,OOA.OA_ID,OOB.OA_BRANDPACK_ID,OOB.OA_ORIGINAL_QTY AS OA_QTY,OOB.PO_BRANDPACK_ID FROM ORDR_ORDER_ACCEPTANCE OOA  " & vbCrLf & _
                        "                   INNER JOIN ORDR_OA_BRANDPACK OOB ON OOA.OA_ID = OOB.OA_ID WHERE OOA.OA_DATE >= @FROM_OA_DATE AND OOA.OA_DATE <= @UNTIL_OA_DATE " & vbCrLf & _
                        "                  )OA " & vbCrLf & _
                        " ON OA.PO_REF_NO = PO.PO_REF_NO AND OA.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                        " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                        " CREATE CLUSTERED INDEX IX_T_R_OA_BrandPack_" & Me.ComputerName & " ON ##T_R_OA_BrandPack_" & Me.ComputerName & "(PO_REF_NO,OA_BRANDPACK_ID,PO_BRANDPACK_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@FROM_OA_DATE", SqlDbType.SmallDateTime, FromDate.AddMonths(-1))
                Me.AddParameter("@UNTIL_OA_DATE", SqlDbType.SmallDateTime, UntilDate.AddMonths(1)) : Me.SqlCom.ExecuteScalar()
                Me.SqlCom.Parameters.RemoveAt("@FROM_OA_DATE")
                Me.SqlCom.Parameters.RemoveAt("@UNTIL_OA_DATE")


                '====================   SPPB    ==========================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_SPPB_" & Me.ComputerName & " ; END " & vbCrLf & _
                        " SELECT SH.SPPB_NO,SH.SPPB_DATE,SB1.OA_BRANDPACK_ID,SB1.SPPB_QTY,SB1.CREATE_DATE,SB1.RELEASE_DATE, DATEDIFF(DAY,SB1.CREATE_DATE,ISNULL(SB1.RELEASE_DATE,SB1.CREATE_DATE)) AS LEAD_TIME," & vbCrLf & _
                        " SB1.GON_1_NO,SB1.GON_1_DATE,SB1.GON_1_QTY,SB1.GON_2_NO,SB1.GON_2_DATE,SB1.GON_2_QTY,SB1.GON_3_NO,SB1.GON_3_DATE,SB1.GON_3_QTY,SB1.GON_4_NO,SB1.GON_4_DATE," & vbCrLf & _
                        " SB1.GON_4_QTY,SB1.GON_5_NO,SB1.GON_5_DATE,SB1.GON_5_QTY,SB1.GON_6_NO,SB1.GON_6_DATE,SB1.GON_6_QTY," & vbCrLf & _
                        " SB1.STATUS,SB1.BALANCE,SB1.ISREVISION,SB1.REMARK INTO ##T_R_SPPB_" & Me.ComputerName & " FROM SPPB_HEADER SH INNER JOIN SPPB_BRANDPACK SB1 ON SH.SPPB_NO = SB1.SPPB_NO " & vbCrLf & _
                        " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = SH.PO_REF_NO " & vbCrLf & _
                        " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                        " CREATE CLUSTERED INDEX IX_T_R_SPPB_" & Me.ComputerName & " ON ##T_R_SPPB_" & Me.ComputerName & "(OA_BRANDPACK_ID,SPPB_DATE) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()


                '=====================   THIRD PARTY ROUND UP ========================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OTP_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_OTP_" & Me.ComputerName & " ; END " & vbCrLf & _
                        " SELECT OOH.OTP_NO,OOH.OTP_DATE,OD.OTP_QTY,OD.CREATE_DATE,OD.OA_BRANDPACK_ID INTO ##T_R_OTP_" & Me.ComputerName & " FROM ORDR_OTP_HEADER OOH INNER JOIN OTP_DETAIL OD " & vbCrLf & _
                        " ON OD.OTP_NO = OOH.OTP_NO INNER JOIN SPPB_HEADER SH ON OOH.SPPB_NO = SH.SPPB_NO INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = SH.PO_REF_NO " & vbCrLf & _
                        " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ; " & vbCrLf & _
                        " CREATE CLUSTERED INDEX IX_T_R_OTP_" & Me.ComputerName & " ON ##T_R_OTP_" & Me.ComputerName & "(OA_BRANDPACK_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()


                '======================   SHIP TO MANAGER    ==========================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_SHIP_TO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN DROP TABLE TEMPDB..##T_R_OA_SHIP_TO_" & Me.ComputerName & " ; END " & vbCrLf & _
                        " SELECT OST.OA_ID,TERR.TERRITORY_AREA,TM.MANAGER INTO ##T_R_OA_SHIP_TO_" & Me.ComputerName & " FROM OA_SHIP_TO OST INNER JOIN SHIP_TO ST ON OST.SHIP_TO_ID = ST.SHIP_TO_ID " & vbCrLf & _
                        " INNER JOIN TERRITORY TERR ON TERR.TERRITORY_ID = ST.TERRITORY_ID INNER JOIN TERRITORY_MANAGER TM ON ST.TM_ID = TM.TM_ID " & vbCrLf & _
                        " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OST.OA_ID = OOA.OA_ID INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OOA.PO_REF_NO " & vbCrLf & _
                        " WHERE PO.PO_REF_DATE >= @FROM_DATE AND PO.PO_REF_DATE <= @UNTIL_DATE ;" & vbCrLf & _
                        " CREATE CLUSTERED INDEX IX_T_R_OA_SHIP_TO_" & Me.ComputerName & " ON ##T_R_OA_SHIP_TO_" & Me.ComputerName & "(OA_ID) ;"
                Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()


                '======================   QUERY REPORT   ===================================================================
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT PO.DISTRIBUTOR_ID,PO.DISTRIBUTOR_NAME,PO.PO_REF_NO,PO.PO_REF_DATE,PO.PROJ_REF_NO,PO.PROJECT_NAME,PB.BRAND_NAME,PB.BRANDPACK_ID,PB.BRANDPACK_NAME," & vbCrLf & _
                        " PB.PO_ORIGINAL_QTY,PB.PRICE,ST.TERRITORY_AREA AS SHIP_TO,ST.MANAGER,OA.OA_REF_NO,OA.OA_QTY,OA.OA_BRANDPACK_ID,SB.SPPB_NO,SB.SPPB_DATE,SB.SPPB_QTY,SB.CREATE_DATE,SB.RELEASE_DATE,SB.LEAD_TIME,SB.GON_1_NO,SB.GON_1_DATE,SB.GON_1_QTY," & vbCrLf & _
                        " SB.GON_2_NO,SB.GON_2_DATE,SB.GON_2_QTY,SB.GON_3_NO,SB.GON_3_DATE,SB.GON_3_QTY,SB.GON_4_NO,SB.GON_4_DATE,SB.GON_4_QTY," & vbCrLf & _
                        " SB.GON_5_NO,SB.GON_5_DATE,SB.GON_5_QTY,SB.GON_6_NO,SB.GON_6_DATE,SB.GON_6_QTY,ISNULL(SB.STATUS,'PENDING')AS STATUS,ISNULL(SB.BALANCE,0)AS BALANCE,SB.ISREVISION,SB.REMARK " & vbCrLf & _
                        " FROM TEMPDB..##T_HEADER_PO_" & Me.ComputerName & " PO LEFT OUTER JOIN ##T_PO_DETAIL_" & Me.ComputerName & " PB ON PO.PO_REF_NO = PB.PO_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN ##T_R_OA_BrandPack_" & Me.ComputerName & " OA ON OA.PO_BRANDPACK_ID = PB.PO_BRANDPACK_ID AND OA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN ##T_R_OA_SHIP_TO_" & Me.ComputerName & " ST ON ST.OA_ID = OA.OA_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN ##T_R_SPPB_" & Me.ComputerName & " SB ON SB.OA_BRANDPACK_ID = OA.OA_BRANDPACK_ID " & vbCrLf & _
                        " LEFT OUTER JOIN ##T_R_OTP_" & Me.ComputerName & " OTP ON OTP.OA_BRANDPACK_ID = OA.OA_BRANDPACK_ID ;"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtSPPB As New DataTable("SPPB_REPORT_DATA")
                dtSPPB.Clear() : setDataAdapter(Me.SqlCom).Fill(dtSPPB) : Me.ClearCommandParameters()

                '---------destroy table -------------
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                  " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_HEADER_PO_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                  " BEGIN DROP TABLE TEMPDB..##T_HEADER_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
                  " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PO_DETAIL_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                  " BEGIN DROP TABLE TEMPDB..##T_PO_DETAIL_" & Me.ComputerName & " ; END " & vbCrLf & _
                  " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_BrandPack_" & Me.ComputerName & "' AND TYPE = 'U')" & vbCrLf & _
                  " BEGIN DROP TABLE TEMPDB..##T_R_OA_BrandPack_" & Me.ComputerName & " ; END " & vbCrLf & _
                  " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_SPPB_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                  " BEGIN DROP TABLE TEMPDB..##T_R_SPPB_" & Me.ComputerName & " ; END " & vbCrLf & _
                  " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OA_SHIP_TO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                  " BEGIN DROP TABLE TEMPDB..##T_R_OA_SHIP_TO_" & Me.ComputerName & " ; END " & vbCrLf & _
                  " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_R_OTP_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                  " BEGIN DROP TABLE TEMPDB..##T_R_OTP_" & Me.ComputerName & " ; END "
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.SqlCom.ExecuteScalar() : Me.CloseConnection() : Me.ClearCommandParameters()
                Me.m_ViewSPPBDetail = dtSPPB.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
       
        Public Function CreateViewSPPBDetail(ByVal CategoryDate As String, ByVal DISTRIBUTOR_ID As Object, ByVal FROM_PO_DATE As Object, ByVal UNTIL_PO_DATE As Object) As DataView
            Try
                Me.CreateCommandSql("Usp_Create_View_SPPB_Detail", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                'If DISTRIBUTOR_ID Is Nothing Then
                '    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DBNull.Value, 10)
                'Else
                '    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                'End If
                Select Case CategoryDate
                    Case "ByPO"
                        Me.AddParameter("@CATEGORY_DATE", SqlDbType.VarChar, "ByPO", 15)
                    Case "BySPPB"
                        Me.AddParameter("@CATEGORY_DATE", SqlDbType.VarChar, "BySPPB", 15)
                End Select
                Me.AddParameter("@FROM_PO_DATE", SqlDbType.DateTime, FROM_PO_DATE)
                Me.AddParameter("@UNTIL_PO_DATE", SqlDbType.DateTime, UNTIL_PO_DATE)
                Dim dtTable As New DataTable("SPPB_DETAIL")
                dtTable.Clear()
                Me.FillDataTable(dtTable)
                Me.m_ViewSPPBDetail = dtTable.DefaultView()
                'Me.m_ViewSPPBDetail.RowStateFilter = DataViewRowState.OriginalRows
                Return Me.m_ViewSPPBDetail
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function getSPPB_QTY(ByVal SPPB_BRANDPACK_ID As String) As Decimal
            Try
                Me.CreateCommandSql("Usp_Get_SPPB_Qty", "")
                Me.AddParameter("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, SPPB_BRANDPACK_ID, 90)
                Return CDec(Me.ExecuteScalar())
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Overloads Function CreateViewBrandPack(ByVal OA_REF_NO As String, ByVal mustCloseConnection As Boolean) As DataView
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Create_View_BrandPack_By_OA_Ref_No", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Create_View_BrandPack_By_OA_Ref_No")
                End If
                Me.AddParameter("@OA_REF_NO", SqlDbType.VarChar, OA_REF_NO, 32)
                Dim tblBrandPack As New DataTable("BRANDPACK")
                tblBrandPack.Clear()
                Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(tblBrandPack) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
                ': Me.FillDataTable(tblBrandPack)

                Me.m_ViewBrandPack = tblBrandPack.DefaultView()
                Me.m_ViewBrandPack.Sort = "BRANDPACK_ID" : Return Me.m_ViewBrandPack
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function GetGonSequenceNo(ByVal SPPB_NO As String, ByVal mustCloseConnection As Boolean) As Integer
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Gon_Sequence_NO", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Gon_Sequence_NO")
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 15)
                Me.AddParameter("@GON_SEQUENCE", SqlDbType.Int, ParameterDirection.Output)

                Me.OpenConnection()
                Me.SqlCom.ExecuteNonQuery()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Dim retInt As Integer = CInt(Me.SqlCom.Parameters()("@GON_SEQUENCE").Value)
                Me.ClearCommandParameters()
                Return retInt
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function IsExistedOnSPPBBrandPack(ByVal SPPB_BRANDPACK_ID As String) As Boolean
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Check_Exixting_SPPB_BrandPack", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Exixting_SPPB_BrandPack")
                End If
                Me.AddParameter("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, SPPB_BRANDPACK_ID, 90)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If Me.GetReturnValueByExecuteScalar("@RETURN_VALUE") > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
                Return False
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function IsHasChildReference(ByVal OA_BRANDPACK_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Usp_Check_Reference_SPPB_BrandPack", "")
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                If CInt(Me.GetReturnValueByExecuteScalar("@RETURN_VALUE")) > 0 Then
                    Me.ClearCommandParameters() : Return True
                End If
                Me.ClearCommandParameters()
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function GetTotalOAQty(ByVal OA_BRANDPACK_ID As String, ByVal IsCheking As Boolean, ByVal mustCloseConnection As Boolean, ByRef Remark As String) As Decimal
            Dim result As Decimal = 0
            Try
                Me.CreateCommandSql("Usp_Get_Total_OA_Qty", "")
                Me.AddParameter("@REMARK", SqlDbType.VarChar, ParameterDirection.Output)
                Me.AddParameter("@ResultQty", SqlDbType.Decimal, ParameterDirection.Output)
                Me.SqlCom.Parameters()("@ResultQty").Scale = 3
                Me.SqlCom.Parameters()("@REMARK").Size = 150
                If IsCheking Then
                    Me.AddParameter("@IsCheking", SqlDbType.Bit, 1)
                Else
                    Me.AddParameter("@IsCheking", SqlDbType.Bit, 0)
                End If
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                Me.OpenConnection() : Me.SqlCom.ExecuteNonQuery()
                Remark = Me.SqlCom.Parameters()("@REMARK").Value.ToString()
                result = Convert.ToDecimal(Me.SqlCom.Parameters()("@ResultQty").Value)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                'Return CDec(Me.ExecuteScalar())
                'Me.FetchDataView_1()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return result
        End Function
        Public Function getTransporter(ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT GT_ID,TRANSPORTER_NAME FROM GON_TRANSPORTER "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Dim tTrans As New DataTable("T_Trans")
                setDataAdapter(Me.SqlCom).Fill(tTrans) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return tTrans
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return Nothing
        End Function

      

        Public Function getGonDate(ByVal SPPB_NO As String, ByVal GS_NO As Integer) As Object
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_Gon_Date_By_GS_No", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Gon_Date_By_GS_No")
                End If
                Me.AddParameter("@GS_NO", SqlDbType.Int, GS_NO)
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 15)
                Dim result As Object = Me.ExecuteScalar()
                Return result
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
        Public Function GetTransporter(ByVal searchTransporter As String, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT TOP 100 GT_ID,TRANSPORTER_NAME FROM GON_TRANSPORTER WHERE TRANSPORTER_NAME LIKE '%" + searchTransporter + "%' ORDER BY CREATE_DATE DESC ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_Transporter") : dtTable.Clear() : Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(dtTable) : Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dtTable
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function CreateViewDistributorOA(ByVal isReGisteredSPPB As Boolean, ByVal mustCloseConnection As Boolean)
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Create_View_Distributor_OA", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Create_View_Distributor_OA")
                End If
                If isReGisteredSPPB Then
                    Me.AddParameter("@ISREGISTERED", SqlDbType.Bit, 1)
                Else
                    Me.AddParameter("@ISREGISTERED", SqlDbType.Bit, 0)
                End If
                Dim dtTable As New DataTable("DISTRIBUTOR_SPPB")
                dtTable.Clear() : Me.OpenConnection()
                Me.setDataAdapter(Me.SqlCom).Fill(dtTable)
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                'Me.FillDataTable(dtTable)
                Me.m_ViewDistributorSPPB = dtTable.DefaultView()

                Return Me.m_ViewDistributorSPPB
            Catch ex As Exception
                Me.ClearCommandParameters() : Me.CloseConnection() : Throw ex
            End Try
        End Function

        Public Function CreateViewDistributorSPPB() As DataView
            Try
                Me.CreateCommandSql("Usp_Create_View_Distributor_SPPB", "")
                Dim dtTable As New DataTable("Distributor")
                dtTable.Clear()
                Me.FillDataTable(dtTable)
                Me.m_ViewDistributorSPPB = dtTable.DefaultView()
                Return Me.m_ViewDistributorSPPB
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function IsGonHasExists(ByVal GON_NO As String, ByVal SPPB_NO As String, ByVal GS_NO As String, ByVal SaveMode As NufarmBussinesRules.common.Helper.SaveMode) As Boolean
            Dim BFInd As Boolean = False
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT GON_1_NO,GON_2_NO,GON_3_NO,GON_4_NO,GON_5_NO,GON_6_NO FROM SPPB_BRANDPACK WHERE SPPB_NO = @SPPB_NO ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO)
                Dim dtTable As New DataTable("T_GON") : dtTable.Clear()
                Me.FillDataTable(dtTable)
                Dim objGON As Object = Nothing
                Select Case SaveMode
                    Case common.Helper.SaveMode.Insert
                        'check tiap rows dengan column gon_1 sampai 7 apakah ada no gon seperti yang di parameter
                        For rowI As Integer = 0 To dtTable.Rows.Count - 1
                            For colRowI As Integer = 0 To dtTable.Columns.Count - 1
                                If Not dtTable.Rows(rowI).IsNull(colRowI) Then
                                    objGON = dtTable.Rows(rowI)(colRowI)
                                    If Not IsNothing(objGON) And Not IsDBNull(objGON) Then
                                        If objGON.ToString() = GON_NO Then
                                            Throw New Exception("GON_NO " & GON_NO & " with SPPB_NO " & SPPB_NO & " has exists .")
                                        End If
                                    End If
                                End If
                            Next
                        Next
                    Case common.Helper.SaveMode.Update
                        'bikin list dengan NO GON seperti parameter diatas
                        'Dim colGONID As New DataColumn("GON_ID", Type.GetType("System.String"))
                        'colGONID.Unique = True
                        'Dim colGONNO As New DataColumn("GON_NO", Type.GetType("System.String"))
                        'Dim colGONS As New DataColumn("GON_S", Type.GetType("System.String"))
                        'Dim dtGON As New DataTable("T_GON") : dtTable.Clear()
                        'Dim DV As DataView = dtGON.DefaultView() : DV.Sort = "GON_ID"
                        Dim Gon_Sec As String = ""
                        For rowI As Integer = 0 To dtTable.Rows.Count - 1
                            For colRowI As Integer = 0 To dtTable.Columns.Count - 1
                                If Not dtTable.Rows(rowI).IsNull(colRowI) Then
                                    objGON = dtTable.Rows(rowI)(colRowI)
                                    If Not IsNothing(objGON) And Not IsDBNull(objGON) Then
                                        If objGON.ToString() = GON_NO Then
                                            If Gon_Sec = "" Then
                                                Gon_Sec = dtTable.Columns(colRowI).Caption.Substring(0, 5)
                                                If Gon_Sec <> GS_NO Then
                                                    BFInd = True
                                                End If
                                            ElseIf GS_NO <> dtTable.Columns(colRowI).Caption.Substring(0, 5) Then
                                                BFInd = True  'Throw New Exception("GON_NO " & GON_NO & " with SPPB_NO " & SPPB_NO & " has existed !.")
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        Next
                End Select
            Catch ex As Exception
                BFInd = True : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return BFInd
        End Function

        Public Function getGOnData(ByVal searchGON As String) As DataView
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        " IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_GON_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        "   BEGIN " & vbCrLf
                If String.IsNullOrEmpty(searchGON) Then
                    Query &= " SELECT TOP 100 GON_NO,SPPB_NO FROM ##T_GON_" & Me.ComputerName & " GON WHERE NOT EXISTS(SELECT GRB_ID FROM GON_RECEIVED_BACK WHERE GON_NO = GON.GON_NO AND SPPB_NO = GON.SPPB_NO) ; "
                Else
                    Query &= " SELECT GON_NO,SPPB_NO FROM ##T_GON_" & Me.ComputerName & " GON WHERE GON_NO LIKE '%" & searchGON & "%' AND NOT EXISTS(SELECT GRB_ID FROM GON_RECEIVED_BACK WHERE GON_NO = GON.GON_NO AND SPPB_NO = GON.SPPB_NO) ;"
                End If
                Query &= " END  "
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Dim dtTable As New DataTable("T_GON") : dtTable.Clear()
                Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function getGonData_1(ByVal SPPBNo As String) As DataView
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT * FROM (" & vbCrLf
                For i As Integer = 1 To 6
                    Query &= " SELECT SH.SPPB_NO,SB.GON_" & i.ToString() & "_NO AS GON_NUMBER FROM SPPB_HEADER SH INNER JOIN SPPB_BRANDPACK SB ON SH.SPPB_NO = SB.SPPB_NO WHERE SH.SPPB_NO = @SPPB_NO  AND GON_" & i.ToString() & "_NO IS NOT NULL " & vbCrLf
                    If i < 6 Then
                        Query &= " UNION  " & vbCrLf
                    End If
                Next
                Query &= "UNION " & vbCrLf & _
                        " SELECT SH.SPPB_NO,GH.GON_NO FROM GON_HEADER GH INNER JOIN SPPB_HEADER SH ON SH.SPPB_NO = GH.SPPB_NO WHERE SH.SPPB_NO = @SPPB_NO " & vbCrLf & _
                        " )GON; "
             
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPBNo, 25)
                Dim dtTable As New DataTable("T_GON") : dtTable.Clear()
                Me.OpenConnection() : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.SqlDat.Fill(dtTable)
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Sub DeleteReturnGONData(ByVal GRB_ID As String)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "DELETE FROM GON_RECEIVED_BACK WHERE GRB_ID = @GRB_ID ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@GRB_ID", SqlDbType.VarChar, GRB_ID, 32)
                Me.OpenConnection() : Me.ExecuteScalar()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function getGONReturnedBackData(ByVal StartDate As DateTime, ByVal EndDate As DateTime, Optional ByVal DistirbutorID As String = "") As DataView
            Try
                'GRB_ID, ID_RECEIVER, SPPB_NO, GON_NO, RETURNED_DATE, IS_SIGNED, IS_STAMPED, GON_ACCEPTED_BY
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT PO.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,PO.PO_REF_NO,GRB.GRB_ID,GRB.GON_ACCEPTED_BY AS ID_RECEIVER,SH.SPPB_NO,GRB.GON_NO, " & vbCrLf & _
                        "GRB.RETURNED_GON_DATE AS RETURNED_DATE,DGR.GON_RECEIVER AS GON_ACCEPTED_BY,GRB.IS_SIGNED,GRB.IS_STAMPED,GRB.DESCRIPTIONS AS REMARK " & vbCrLf & _
                        " FROM ORDR_PURCHASE_ORDER PO INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID " & vbCrLf & _
                        " INNER JOIN SPPB_HEADER SH ON SH.PO_REF_NO = PO.PO_REF_NO INNER JOIN GON_RECEIVED_BACK GRB ON GRB.SPPB_NO = SH.SPPB_NO " & vbCrLf & _
                        " LEFT OUTER JOIN DIST_GON_RECEIVER DGR ON GRB.GON_ACCEPTED_BY = DGR.GR_ID " & vbCrLf & _
                        " WHERE GRB.RETURNED_GON_DATE >= @StartDate AND GRB.RETURNED_GON_DATE <= @EndDate ; "
                If DistributorID <> "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT PO.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,PO.PO_REF_NO,GRB.GRB_ID,GRB.GON_ACCEPTED_BY AS ID_RECEIVER,SH.SPPB_NO,GRB.GON_NO, " & vbCrLf & _
                            "GRB.RETURNED_GON_DATE AS RETURNED_DATE,DGR.GON_RECEIVER AS GON_ACCEPTED_BY,GRB.IS_SIGNED,GRB.IS_STAMPED,GRB.DESCRIPTIONS AS REMARK " & vbCrLf & _
                            " FROM ORDR_PURCHASE_ORDER PO INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID " & vbCrLf & _
                            " INNER JOIN SPPB_HEADER SH ON SH.PO_REF_NO = PO.PO_REF_NO INNER JOIN GON_RECEIVED_BACK GRB ON GRB.SPPB_NO = SH.SPPB_NO " & vbCrLf & _
                            " LEFT OUTER JOIN DIST_GON_RECEIVER DGR ON GRB.GON_ACCEPTED_BY = DGR.GR_ID " & vbCrLf & _
                            " WHERE GRB.RETURNED_GON_DATE >= @StartDate AND GRB.RETURNED_GON_DATE <= @EndDate  AND PO.DISTRIBUTOR_ID = @DistributorID ; "
                End If
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDate)
                Me.OpenConnection() : Dim dtTable As New DataTable("GON_RETURNED_BACK_FROM DISTRIBUTOR") : dtTable.Clear()
                Me.FillDataTable(dtTable)
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function


        Public Function getGonDescription(ByVal SPPB_NO As String, ByVal GON_NO As String, ByRef T_ListReceiver As DataTable, ByRef Trans As Nufarm.Domain.Transporter, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT TOP 1 DR.DISTRIBUTOR_NAME,PO.DISTRIBUTOR_ID,GON.GON_DATE,SH.SPPB_DATE " & vbCrLf & _
                        " FROM DIST_DISTRIBUTOR DR INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID " & vbCrLf & _
                        " INNER JOIN SPPB_HEADER SH ON SH.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                        " INNER JOIN (" & vbCrLf
                For i As Integer = 1 To 6
                    Query &= " SELECT SB.SPPB_NO,SB.GON_" & i.ToString() & "_DATE AS GON_DATE FROM SPPB_BRANDPACK SB WHERE SB.SPPB_NO = @SPPB_NO AND SB.GON_" & i.ToString() & "_NO = @GON_NO " & vbCrLf
                    If i < 6 Then
                        Query &= " UNION  " & vbCrLf
                    End If
                Next
                Query &= "UNION " & vbCrLf & _
                       " SELECT SH.SPPB_NO,GH.GON_DATE FROM SPPB_HEADER SH INNER JOIN GON_HEADER GH ON SH.SPPB_NO = GH.SPPB_NO WHERE SH.SPPB_NO = @SPPB_NO AND GH.GON_NO = @GON_NO " & vbCrLf & _
                        " )GON " & vbCrLf & _
                        " ON GON.SPPB_NO = SH.SPPB_NO WHERE SH.SPPB_NO = @SPPB_NO ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 25)
                Me.AddParameter("@GON_NO", SqlDbType.VarChar, GON_NO, 30)
                Dim dtTable As New DataTable("T_GON") : dtTable.Clear()
                Me.OpenConnection()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                If dtTable.Rows.Count > 0 Then
                    Dim strDistributorID As String = dtTable.Rows(0)("DISTRIBUTOR_ID").ToString()
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT GR_ID AS ID_RECEIVER,GON_RECEIVER AS RECEIVED_BY FROM DIST_GON_RECEIVER WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, strDistributorID, 10)
                    Me.SqlDat.Fill(T_ListReceiver) : Me.ClearCommandParameters()
                End If
                Dim GonHeaderID As String = SPPB_NO & "|" & GON_NO
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                " SELECT TOP 1 GH.GT_ID,TR.TRANSPORTER_NAME FROM GON_TRANSPORTER TR INNER JOIN GON_HEADER GH ON GH.GT_ID = TR.GT_ID WHERE GH.GON_HEADER_ID = @GON_HEADER_ID ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@GON_HEADER_ID", SqlDbType.VarChar, GonHeaderID, 50)
                Me.ExecuteReader()
                If Me.SqlRe.HasRows() Then
                    If IsNothing(Trans) Then
                        Trans = New Nufarm.Domain.Transporter()
                    End If
                    While Me.SqlRe.Read()
                        With Trans
                            .GT_ID = Me.SqlRe.GetString(0)
                            .NameApp = Me.SqlRe.GetString(1)
                        End With
                    End While : Me.SqlRe.Close()
                End If
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return dtTable
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

#End Region

#Region " SUB "

        Public Sub CreateGONData(ByVal StartDateSPPB As DateTime, ByVal EndDateSPPB As DateTime)
            Try
                Dim StartString As String = StartDateSPPB.Day.ToString() + "" + StartDateSPPB.Month.ToString() + "" + StartDateSPPB.Year.ToString()
                Dim EndString As String = EndDateSPPB.Day.ToString() + "" + EndDateSPPB.Month.ToString() + EndDateSPPB.Year.ToString()
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_GON_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " DROP TABLE ##T_GON_" & Me.ComputerName & " ;" & vbCrLf & _
                        " END " & vbCrLf & _
                        " SELECT * INTO ##T_GON_" & Me.ComputerName & " FROM (" & vbCrLf
                For i As Integer = 1 To 6
                    Query &= " SELECT SH.SPPB_NO,SB.GON_" & i.ToString() & "_NO AS GON_NO FROM SPPB_HEADER SH INNER JOIN SPPB_BRANDPACK SB ON SH.SPPB_NO = SB.SPPB_NO WHERE SH.SPPB_DATE >= @StartDate AND SH.SPPB_DATE <= @EndDate AND SB.GON_" & i.ToString() & "_NO IS NOT NULL " & vbCrLf
                    If i < 6 Then
                        Query &= " UNION  " & vbCrLf
                    End If
                Next
                Query &= "UNION " & vbCrLf & _
                " SELECT SH.SPPB_NO,GH.GON_NO FROM SPPB_HEADER SH INNER JOIN GON_HEADER GH ON SH.SPPB_NO = GH.SPPB_NO WHERE SH.SPPB_DATE >= @StartDate AND SH.SPPB_DATE <= @EndDate "
                Query &= " )GON " & vbCrLf & _
                " CREATE CLUSTERED INDEX IX_T_GON ON ##T_GON_" & Me.ComputerName & "(GON_NO); "

                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@StartDate", SqlDbType.SmallDateTime, StartDateSPPB)
                Me.AddParameter("@EndDate", SqlDbType.SmallDateTime, EndDateSPPB)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                'Dim dtTable As New DataTable("T_GON") : dtTable.Clear()
                'Me.OpenConnection() : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                'Me.SqlDat.Fill(dtTable)
                'Return dtTable.DefaultView()

            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        Public Sub GetGONNoAndDate(ByVal SPPB_NO As String, ByVal GS_NO As Integer, ByVal mustCloseConnection As Boolean)
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Get_GON_No_And_Date", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_GON_No_And_Date")
                End If
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO)
                Me.AddParameter("@GS_NO", SqlDbType.Int, GS_NO)
                Me.ExecuteReader()
                While Me.SqlRe.Read()
                    Me.GON_NO = SqlRe("GON_NO")
                    Me.GON_DATE = Me.SqlRe("GON_DATE")
                End While : Me.SqlRe.Close()
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.ClearCommandParameters()
                Me.SqlRe.Close()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Public Sub DeleteSPPPB_BrandPack(ByVal OA_BRANDPACK_ID As String)
            Try
                Me.CreateCommandSql("Usp_Delete_SPPB_BrandPack", "")
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                'Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, SPPB_NO, 70)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Public Sub GetRowOAREf(ByVal OA_REF_NO As String, ByVal mustCloseConnection As Boolean)
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Getrow_By_OA_Ref_No", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Getrow_By_OA_Ref_No")
                End If
                Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_REF_NO, 32)
                Me.ExecuteReader() : While Me.SqlRe.Read()
                    Me.OA_DATE = CDate(Me.SqlRe("OA_DATE"))
                    'Me.SHIP_TERRITORY = Me.SqlRe("TERRITORY_AREA")
                    Me.SPPB_NO = SqlRe("SPPB_NO")
                    Me.SPPBDate = Me.SqlRe("SPPB_DATE")
                End While : Me.SqlRe.Close()
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.ClearCommandParameters() : Me.SqlRe.Close() : Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Function CreatetransporterID(ByVal TransporterName As String) As String
            Dim num As String = "0000"
            Try
                ''check data
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " SELECT 1 WHERE EXISTS(SELECT TRANSPORTER_NAME FROM GON_TRANSPORTER WHERE TRANSPORTER_NAME = @TRANSPORTER_NAME) ; "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@TRANSPORTER_NAME", SqlDbType.VarChar, TransporterName, 100)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    If CInt(retval) > 0 Then
                        Throw New Exception("Data transporter has existed and created previously" & vbCrLf & "You should choose existing data")
                    End If
                End If

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT SUM (row_count) FROM Nufarm.sys.dm_db_partition_stats WHERE object_id=OBJECT_ID('GON_TRANSPORTER') AND (index_id=0 or index_id=1) ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)

                Dim gtRecCount As Integer = 0
                gtRecCount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                gtRecCount += 1
                Dim X As Integer = num.Length - CStr(gtRecCount).Length
                If X <= 0 Then
                    num = ""
                Else
                    num = num.Remove(X - 1, CStr(gtRecCount).Length)
                End If
                num &= gtRecCount.ToString()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try

            Return num
        End Function

        Public Sub SaveReceivedGON(ByVal mode As NufarmBussinesRules.common.Helper.SaveMode, ByVal MustCloseConnection As Boolean)
            Try
                'If Me.PlantGroupID <> "" Then
                '    Query = "SET NOCOUNT ON; " & vbCrLf & _
                '     "SELECT [ROWS] FROM sysindexes  WHERE id = OBJECT_ID('PLANTATION_GROUP') AND INDID < 2 ;"
                '    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                '    RecCountGroupID = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                'End If
                'Dim num As String = "0000"
                'Dim X As Integer = num.Length - CStr(PlantRecCount).Length
                'If X <= 0 Then
                '    num = ""
                'Else
                '    num = num.Remove(X - 1, CStr(PlantRecCount).Length)
                'End If
                'num &= PlantRecCount.ToString() : PlantationID = TerritoryID & "-" & num

                Dim grRecCount As Integer = 0
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                If Me.ReceiverID = "" And Me.GOnReceiverName <> "" Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT COUNT(GR_ID) + 1  FROM  DIST_GON_RECEIVER WHERE DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"
                    If IsNothing(Me.SqlCom) Then
                        Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DistributorID, 10)
                    grRecCount = CInt(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                    Dim num As String = "000"
                    Dim X As Integer = num.Length - CStr(grRecCount).Length
                    If X <= 0 Then
                        num = ""
                    Else
                        num = num.Remove(X - 1, CStr(grRecCount).Length)
                    End If
                    num &= grRecCount.ToString()
                    ReceiverID = Me.DistributorID & "-" & num
                    'INSERT KE GON
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "IF NOT EXISTS(SELECT GR_ID FROM DIST_GON_RECEIVER WHERE GR_ID = @GR_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " INSERT INTO DIST_GON_RECEIVER(GR_ID,DISTRIBUTOR_ID,GON_RECEIVER,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                            " VALUES(@GR_ID,@DISTRIBUTOR_ID,@GON_RECEIVER,@CREATE_BY,@CREATE_DATE) " & vbCrLf & _
                            " END "
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@GR_ID", SqlDbType.VarChar, ReceiverID, 14)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DistributorID, 10)
                    Me.AddParameter("@GON_RECEIVER", SqlDbType.VarChar, Me.GOnReceiverName, 50)
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                Dim GRB_ID As String = Me.SPPB_NO + "|" + Me.GON_NO
                If mode = common.Helper.SaveMode.Insert Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "IF NOT EXISTS(SELECT GRB_ID FROM GON_RECEIVED_BACK WHERE GRB_ID = @GRB_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " INSERT INTO GON_RECEIVED_BACK(GRB_ID,GON_NO,SPPB_NO,RETURNED_GON_DATE,IS_SIGNED,IS_STAMPED,GON_ACCEPTED_BY,DESCRIPTIONS,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                            " VALUES(@GRB_ID,@GON_NO,@SPPB_NO,@RETURNED_GON_DATE,@IS_SIGNED,@IS_STAMPED,@GON_ACCEPTED_BY,@DESCRIPTIONS,@CREATE_BY,@CREATE_DATE); " & vbCrLf & _
                            " END " & vbCrLf & _
                            "ELSE " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " RAISERROR('Data has existed',16,1); RETURN ; " & vbCrLf & _
                            " END  "
                Else
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "IF NOT EXISTS(SELECT GRB_ID FROM GON_RECEIVED_BACK WHERE GRB_ID = @GRB_ID) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " RAISERROR('Data has been deleted',16,1); RETURN ; " & vbCrLf & _
                            " END " & vbCrLf & _
                            " UPDATE GON_RECEIVED_BACK SET RETURNED_GON_DATE = @RETURNED_GON_DATE,IS_SIGNED = @IS_SIGNED, " & vbCrLf & _
                            " IS_STAMPED = @IS_STAMPED,GON_ACCEPTED_BY = @GON_ACCEPTED_BY,DESCRIPTIONS = @DESCRIPTIONS,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE WHERE GRB_ID = @GRB_ID; "
                End If
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@GRB_ID", SqlDbType.VarChar, GRB_ID, 32)
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, Me.SPPB_NO, 15)
                Me.AddParameter("@GON_NO", SqlDbType.VarChar, Me.GON_NO, 15)
                'Me.AddParameter("DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DistributorID, 10)
                Me.AddParameter("@RETURNED_GON_DATE", SqlDbType.SmallDateTime, Me.ReceivedGonDate)
                Me.AddParameter("@IS_SIGNED", SqlDbType.Bit, Me.HasSigned)
                Me.AddParameter("@IS_STAMPED", SqlDbType.Bit, Me.HasStamped)
                Me.AddParameter("@GON_ACCEPTED_BY", SqlDbType.VarChar, Me.ReceiverID, 50)
                Me.AddParameter("@DESCRIPTIONS", SqlDbType.VarChar, Remark, 250)
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CommiteTransaction()
                If MustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        'Public Function GetTransporterByGOnNO(ByVal SPPNO As String, ByVal GON_NO As String) As String
        '    Try
        '        Query = "SET NOCOUNT ON; "
        '    Catch ex As Exception

        '    End Try
        'End Function

        Public Overloads Sub Save(ByVal SaveType As String, ByVal ds As DataSet, Optional ByVal GonSect As Integer = 0)
            Try
                Select Case SaveType
                    Case "Insert"
                        Me.CreateCommandSql("Usp_Insert_SPPB_Header", "")
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) 'VARCHAR(50)
                    Case "Update"
                        Me.CreateCommandSql("Usp_Update_SPPB_Header", "")
                        Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30)
                End Select
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, Me.SPPB_NO, 15) ' VARCHAR(15),
                Me.AddParameter("@SPPB_DATE", SqlDbType.DateTime, Me.SPPBDate) ' DATETIME,
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, Me.PO_REF_NO, 25) 'VARCHAR(25),
                Me.OpenConnection() : Me.BeginTransaction() : Me.ExecuteNonQuery()
                'Me.GetConnection()
                'Me.SqlCom = New SqlCommand() : Me.SqlCom.Connection = Me.SqlConn
                'Me.OpenConnection() : Me.SqlCom.CommandType = CommandType.Text
                'Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlDat = New SqlDataAdapter()
                Dim CommandInsert As SqlCommand = Me.SqlConn.CreateCommand(), CommandUpdate As SqlCommand = Me.SqlConn.CreateCommand(), _
                CommandDelete As SqlCommand = Me.SqlConn.CreateCommand()
                Dim InsertedRows() As DataRow = Nothing
                Dim UpdatedRows() As DataRow = Nothing, DeletedRows() As DataRow = Nothing
                With CommandInsert
                    'Me.grdGoneSequence.SetValue("GON_1_GT_ID", Me.CreateOrGetTransporterID(Me.mcbTransporter.Text.TrimStart().TrimEnd()))
                    'Me.grdGoneSequence.SetValue("GON_1_TRANSPORTER_NAME", Me.mcbTransporter.Text.TrimEnd().TrimStart())

                    .Transaction = Me.SqlTrans
                    .CommandText = "SET NOCOUNT ON; INSERT INTO SPPB_BRANDPACK(SPPB_NO,SPPB_BRANDPACK_ID,OA_BRANDPACK_ID,SPPB_QTY," _
                                   & "GON_1_NO,GON_1_DATE,GON_1_QTY,GON_2_NO,GON_2_DATE,GON_2_QTY,GON_3_NO,GON_3_DATE,GON_3_QTY," _
                                   & "GON_4_NO,GON_4_DATE,GON_4_QTY,GON_5_NO,GON_5_DATE,GON_5_QTY,GON_6_NO,GON_6_DATE,GON_6_QTY," _
                                   & "STATUS,BALANCE,ISREVISION,REMARK,CREATE_BY,CREATE_DATE,RELEASE_DATE," _
                                   & "GON_1_GT_ID,GON_1_TRANSPORTER_NAME,GON_2_GT_ID,GON_2_TRANSPORTER_NAME,GON_3_GT_ID,GON_3_TRANSPORTER_NAME,GON_4_GT_ID,GON_4_TRANSPORTER_NAME," _
                                   & "GON_5_GT_ID,GON_5_TRANSPORTER_NAME,GON_6_GT_ID,GON_6_TRANSPORTER_NAME)" & vbCrLf & _
                                     "VALUES(@SPPB_NO,@SPPB_BRANDPACK_ID,@OA_BRANDPACK_ID,@SPPB_QTY,@GON_1_NO," _
                                   & "@GON_1_DATE,@GON_1_QTY,@GON_2_NO,@GON_2_DATE,@GON_2_QTY,@GON_3_NO,@GON_3_DATE,@GON_3_QTY," _
                                   & "@GON_4_NO,@GON_4_DATE,@GON_4_QTY,@GON_5_NO,@GON_5_DATE,@GON_5_QTY,@GON_6_NO,@GON_6_DATE,@GON_6_QTY," _
                                   & "@STATUS,@BALANCE,@ISREVISION,@REMARK,@CREATE_BY,@CREATE_DATE,@RELEASE_DATE, " _
                                   & "@GON_1_GT_ID,@GON_1_TRANSPORTER_NAME,@GON_2_GT_ID,@GON_2_TRANSPORTER_NAME,@GON_3_GT_ID,@GON_3_TRANSPORTER_NAME, " _
                                   & "@GON_4_GT_ID,@GON_4_TRANSPORTER_NAME,@GON_5_GT_ID,@GON_5_TRANSPORTER_NAME,@GON_6_GT_ID,@GON_6_TRANSPORTER_NAME) ;"
                    .Parameters.Add("@SPPB_NO", SqlDbType.VarChar, 15).SourceColumn = "SPPB_NO"
                    .Parameters.Add("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, 85).SourceColumn = "SPPB_BRANDPACK_ID"
                    .Parameters.Add("@OA_BRANDPACK_ID", SqlDbType.VarChar, 70).SourceColumn = "OA_BRANDPACK_ID"
                    .Parameters.Add("@SPPB_QTY", SqlDbType.Decimal, 0).SourceColumn = "SPPB_QTY"
                    .Parameters.Add("@GON_1_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_1_NO"
                    .Parameters.Add("@GON_1_DATE", SqlDbType.DateTime).SourceColumn = "GON_1_DATE"
                    .Parameters.Add("@GON_1_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_1_QTY"
                    .Parameters.Add("@GON_2_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_2_NO"
                    .Parameters.Add("@GON_2_DATE", SqlDbType.DateTime).SourceColumn = "GON_2_DATE"
                    .Parameters.Add("@GON_2_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_2_QTY"
                    .Parameters.Add("@GON_3_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_3_NO"
                    .Parameters.Add("@GON_3_DATE", SqlDbType.DateTime).SourceColumn = "GON_3_DATE"
                    .Parameters.Add("@GON_3_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_3_QTY"
                    .Parameters.Add("@GON_4_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_4_NO"
                    .Parameters.Add("@GON_4_DATE", SqlDbType.DateTime).SourceColumn = "GON_4_DATE"
                    .Parameters.Add("@GON_4_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_4_QTY"
                    .Parameters.Add("@GON_5_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_5_NO"
                    .Parameters.Add("@GON_5_DATE", SqlDbType.DateTime).SourceColumn = "GON_5_DATE"
                    .Parameters.Add("@GON_5_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_5_QTY"
                    .Parameters.Add("@GON_6_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_6_NO"
                    .Parameters.Add("@GON_6_DATE", SqlDbType.DateTime).SourceColumn = "GON_6_DATE"
                    .Parameters.Add("@GON_6_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_6_QTY"
                    .Parameters.Add("@STATUS", SqlDbType.VarChar, 19).SourceColumn = "STATUS"
                    .Parameters.Add("@BALANCE", SqlDbType.Decimal, 0).SourceColumn = "BALANCE"
                    .Parameters.Add("@ISREVISION", SqlDbType.Bit).SourceColumn = "ISREVISION"
                    .Parameters.Add("@REMARK", SqlDbType.VarChar, 150).SourceColumn = "REMARK"
                    .Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime).SourceColumn = "CREATE_DATE"
                    .Parameters.Add("@RELEASE_DATE", SqlDbType.SmallDateTime).SourceColumn = "RELEASE_DATE"
                    .Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 50).SourceColumn = "CREATE_BY"

                    ''=============================INSERT TRANSPORTER=========================================
                    .Parameters.Add("@GON_1_GT_ID", SqlDbType.VarChar, 16, "GON_1_GT_ID")
                    .Parameters.Add("@GON_1_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_1_TRANSPORTER_NAME")
                    .Parameters.Add("@GON_2_GT_ID", SqlDbType.VarChar, 16, "GON_2_GT_ID")
                    .Parameters.Add("@GON_2_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_2_TRANSPORTER_NAME")
                    .Parameters.Add("@GON_3_GT_ID", SqlDbType.VarChar, 16, "GON_3_GT_ID")
                    .Parameters.Add("@GON_3_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_3_TRANSPORTER_NAME")
                    .Parameters.Add("@GON_4_GT_ID", SqlDbType.VarChar, 16, "GON_4_GT_ID")
                    .Parameters.Add("@GON_4_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_4_TRANSPORTER_NAME")
                    .Parameters.Add("@GON_5_GT_ID", SqlDbType.VarChar, 16, "GON_5_GT_ID")
                    .Parameters.Add("@GON_5_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_5_TRANSPORTER_NAME")
                    .Parameters.Add("@GON_6_GT_ID", SqlDbType.VarChar, 16, "GON_6_GT_ID")
                    .Parameters.Add("@GON_6_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_6_TRANSPORTER_NAME")
                    'MODIFY_DATE()
                    'MODIFY_BY()
                End With
                With CommandUpdate
                    .Transaction = Me.SqlTrans
                    .CommandText = "SET NOCOUNT ON;UPDATE SPPB_BRANDPACK SET SPPB_QTY = @SPPB_QTY,GON_1_NO = @GON_1_NO," _
                                  & "GON_1_DATE = @GON_1_DATE,GON_1_QTY = @GON_1_QTY,GON_2_NO = @GON_2_NO," _
                                  & "GON_2_DATE = @GON_2_DATE,GON_2_QTY = @GON_2_QTY,GON_3_NO = @GON_3_NO," _
                                  & "GON_3_DATE = @GON_3_DATE,GON_3_QTY = @GON_3_QTY,GON_4_NO = @GON_4_NO," _
                                  & "GON_4_DATE = @GON_4_DATE,GON_4_QTY = @GON_4_QTY,GON_5_NO = @GON_5_NO," _
                                  & "GON_5_DATE = @GON_5_DATE,GON_5_QTY = @GON_5_QTY,GON_6_NO = @GON_6_NO," _
                                  & "GON_6_DATE = @GON_6_DATE,GON_6_QTY = @GON_6_QTY,STATUS = @STATUS,BALANCE = @BALANCE," _
                                  & "REMARK = @REMARK,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = GETDATE(),RELEASE_DATE = @RELEASE_DATE, " & vbCrLf _
                                  & "GON_1_GT_ID = @GON_1_GT_ID,GON_1_TRANSPORTER_NAME = @GON_1_TRANSPORTER_NAME,GON_2_GT_ID = @GON_2_GT_ID,GON_2_TRANSPORTER_NAME = @GON_2_TRANSPORTER_NAME, " _
                                  & "GON_3_GT_ID = @GON_3_GT_ID,GON_3_TRANSPORTER_NAME = @GON_3_TRANSPORTER_NAME,GON_4_GT_ID = @GON_4_GT_ID,GON_4_TRANSPORTER_NAME = @GON_4_TRANSPORTER_NAME, " _
                                  & "GON_5_GT_ID = @GON_5_GT_ID,GON_5_TRANSPORTER_NAME = @GON_5_TRANSPORTER_NAME,GON_6_GT_ID = @GON_6_GT_ID,GON_6_TRANSPORTER_NAME = @GON_6_TRANSPORTER_NAME " & vbCrLf _
                                  & " WHERE SPPB_BRANDPACK_ID = @SPPB_BRANDPACK_ID;"
                    .Parameters.Add("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, 85).SourceColumn = "SPPB_BRANDPACK_ID"
                    .Parameters("@SPPB_BRANDPACK_ID").SourceVersion = DataRowVersion.Original
                    .Parameters.Add("@SPPB_QTY", SqlDbType.Decimal, 0).SourceColumn = "SPPB_QTY"
                    .Parameters.Add("@GON_1_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_1_NO"
                    .Parameters.Add("@GON_1_DATE", SqlDbType.DateTime).SourceColumn = "GON_1_DATE"
                    .Parameters.Add("@GON_1_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_1_QTY"
                    .Parameters.Add("@GON_2_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_2_NO"
                    .Parameters.Add("@GON_2_DATE", SqlDbType.DateTime).SourceColumn = "GON_2_DATE"
                    .Parameters.Add("@GON_2_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_2_QTY"
                    .Parameters.Add("@GON_3_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_3_NO"
                    .Parameters.Add("@GON_3_DATE", SqlDbType.DateTime).SourceColumn = "GON_3_DATE"
                    .Parameters.Add("@GON_3_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_3_QTY"
                    .Parameters.Add("@GON_4_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_4_NO"
                    .Parameters.Add("@GON_4_DATE", SqlDbType.DateTime).SourceColumn = "GON_4_DATE"
                    .Parameters.Add("@GON_4_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_4_QTY"
                    .Parameters.Add("@GON_5_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_5_NO"
                    .Parameters.Add("@GON_5_DATE", SqlDbType.DateTime).SourceColumn = "GON_5_DATE"
                    .Parameters.Add("@GON_5_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_5_QTY"
                    .Parameters.Add("@GON_6_NO", SqlDbType.VarChar, 15).SourceColumn = "GON_6_NO"
                    .Parameters.Add("@GON_6_DATE", SqlDbType.DateTime).SourceColumn = "GON_6_DATE"
                    .Parameters.Add("@GON_6_QTY", SqlDbType.Decimal, 0).SourceColumn = "GON_6_QTY"
                    .Parameters.Add("@STATUS", SqlDbType.VarChar, 19).SourceColumn = "STATUS"
                    .Parameters.Add("@BALANCE", SqlDbType.Decimal, 0).SourceColumn = "BALANCE"
                    .Parameters.Add("@ISREVISION", SqlDbType.Bit).SourceColumn = "ISREVISION"
                    .Parameters.Add("@REMARK", SqlDbType.VarChar, 150).SourceColumn = "REMARK"
                    .Parameters.Add("@MODIFY_BY", SqlDbType.VarChar, 50).Value = NufarmBussinesRules.User.UserLogin.UserName
                    .Parameters.Add("@RELEASE_DATE", SqlDbType.SmallDateTime).SourceColumn = "RELEASE_DATE"

                    ''=============================INSERT TRANSPORTER=========================================
                    .Parameters.Add("@GON_1_GT_ID", SqlDbType.VarChar, 16, "GON_1_GT_ID")
                    .Parameters.Add("@GON_1_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_1_TRANSPORTER_NAME")
                    .Parameters.Add("@GON_2_GT_ID", SqlDbType.VarChar, 16, "GON_2_GT_ID")
                    .Parameters.Add("@GON_2_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_2_TRANSPORTER_NAME")
                    .Parameters.Add("@GON_3_GT_ID", SqlDbType.VarChar, 16, "GON_3_GT_ID")
                    .Parameters.Add("@GON_3_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_3_TRANSPORTER_NAME")
                    .Parameters.Add("@GON_4_GT_ID", SqlDbType.VarChar, 16, "GON_4_GT_ID")
                    .Parameters.Add("@GON_4_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_4_TRANSPORTER_NAME")
                    .Parameters.Add("@GON_5_GT_ID", SqlDbType.VarChar, 16, "GON_5_GT_ID")
                    .Parameters.Add("@GON_5_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_5_TRANSPORTER_NAME")
                    .Parameters.Add("@GON_6_GT_ID", SqlDbType.VarChar, 16, "GON_6_GT_ID")
                    .Parameters.Add("@GON_6_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_6_TRANSPORTER_NAME")
                End With
                With CommandDelete
                    .Transaction = Me.SqlTrans
                    .CommandText = "SET NOCOUNT ON;DELETE FROM SPPB_BRANDPACK WHERE SPPB_BRANDPACK_ID = @SPPB_BRANDPACK_ID; " & vbCrLf & _
                                    " IF NOT EXISTS(SELECT SPPB_NO FROM SPPB_BRANDPACK WHERE SPPB_NO = @SPPB_NO) " & vbCrLf & _
                                    " BEGIN " & vbCrLf & _
                                    " DELETE FROM SPPB_HEADER WHERE SPPB_NO = @SPPB_NO ; " & vbCrLf & _
                                    " END"
                    .Parameters.Add("@SPPB_NO", SqlDbType.VarChar, 15).Value = Me.SPPB_NO
                    .Parameters.Add("@SPPB_BRANDPACK_ID", SqlDbType.VarChar, 85).SourceColumn = "SPPB_BRANDPACK_ID"
                    .Parameters("@SPPB_BRANDPACK_ID").SourceVersion = DataRowVersion.Original
                End With
                SqlDat.InsertCommand = CommandInsert : SqlDat.UpdateCommand = CommandUpdate
                SqlDat.DeleteCommand = CommandDelete
                InsertedRows = ds.Tables(0).Select("", "", DataViewRowState.Added)
                DeletedRows = ds.Tables(0).Select("", "", DataViewRowState.Deleted)
                UpdatedRows = ds.Tables(0).Select("", "", DataViewRowState.ModifiedOriginal)

                If (DeletedRows.Length > 0) Then
                    Me.SqlDat.Update(DeletedRows) : CommandDelete.Parameters.Clear()
                End If
                If (InsertedRows.Length > 0) Then
                    Me.SqlDat.Update(InsertedRows) : CommandInsert.Parameters.Clear()
                End If
                If (UpdatedRows.Length > 0) Then
                    Me.SqlDat.Update(UpdatedRows) : CommandUpdate.Parameters.Clear()
                End If
                Me.ClearCommandParameters()
                If Me.ChangedGSNumber = "" Or Me.ChangedGonNumber = "" Then
                    Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Return
                End If
                ''===========================INSERT GON TRANSPORTER==============
                If (UpdatedRows.Length > 0 Or InsertedRows.Length > 0) Then
                    If GonSect > 0 Then
                        Dim tblTrans As DataTable = ds.Tables(0).Copy()
                        Dim rows() As DataRow = tblTrans.Select("", "", DataViewRowState.Unchanged)
                        If rows.Length > 0 Then
                            For Each row As DataRow In rows
                                row.SetAdded()
                            Next
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " IF (@GON_" & GonSect.ToString() & "_GT_ID IS NOT NULL) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " IF NOT EXISTS(SELECT GT_ID FROM GON_TRANSPORTER WHERE GT_ID = @GON_" & GonSect.ToString() & "_GT_ID) " & vbCrLf & _
                            " INSERT INTO GON_TRANSPORTER(GT_ID,TRANSPORTER_NAME,CREATE_DATE,CREATE_BY) " & vbCrLf & _
                            " VALUES(@GON_" & GonSect.ToString() & "_GT_ID,@GON_" & GonSect.ToString() & "_TRANSPORTER_NAME,@CREATE_DATE,@CREATE_BY) " & vbCrLf & _
                            " END "
                            CommandInsert.Parameters.Clear()
                            CommandInsert.CommandText = Query
                            CommandInsert.Parameters.Add("@GON_" & GonSect.ToString() & "_GT_ID", SqlDbType.VarChar, 100, "GON_" & GonSect.ToString() & "_GT_ID")
                            CommandInsert.Parameters.Add("@GON_" & GonSect.ToString() & "_TRANSPORTER_NAME", SqlDbType.VarChar, 100, "GON_" & GonSect.ToString() & "_TRANSPORTER_NAME")
                            CommandInsert.Parameters.Add("@CREATE_DATE", SqlDbType.SmallDateTime).Value = NufarmBussinesRules.SharedClass.ServerDate
                            CommandInsert.Parameters.Add("@CREATE_BY", SqlDbType.VarChar).Value = NufarmBussinesRules.User.UserLogin.UserName
                            SqlDat.InsertCommand = CommandInsert
                            SqlDat.Update(rows) : CommandInsert.Parameters.Clear()
                        End If
                    End If
                End If


                'Query = "SET NOCOUNT ON ;\n" & vbCrLf & _
                '"SELECT TOP 1 TransactionID FROM GON_SMS WHERE SPPB_NO = @SPPB_NO AND GON_NO = @GON_NO AND GS_NO = @GS_NO ;"
                'Me.ResetCommandText(CommandType.Text, Query)
                'Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, Me.SPPB_NO, 15)
                'Me.AddParameter("GON_NO", SqlDbType.VarChar, Me.ChangedGonNumber, 15)
                'Me.AddParameter("@GS_NO", SqlDbType.VarChar, Me.ChangedGSNumber, 2)
                'Dim rtVal As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                'If Not IsNothing(rtVal) And Not IsDBNull(rtVal) Then
                '    If isReleasedDate Then : Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Return : End If
                'End If
                'If SaveType = "Save" Then : Me.CloseConnection() : Me.ClearCommandParameters() : Return : End If

                Dim StatusPending As Boolean = False, StatusPartial As Boolean = False, StatusCompleted As Boolean = False
                Dim StatusGon As String = "", ItemDescription As String = "Items{(", Message As String = "", strGonDate = "", _
                DistributorID As String = "", DistributorName As String = "", Contact_Person As String = "", Mobile As String = ""
                strGonDate = Me.ChangedGonDate.Day.ToString() + "/" & Me.ChangedGonDate.Month.ToString() + "/" & Me.ChangedGonDate.Year.ToString()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT BB.BRANDPACK_NAME,SB.SPPB_BRANDPACK_ID,ISNULL(SB.GON_1_QTY,0) + ISNULL(SB.GON_2_QTY,0) + ISNULL(SB.GON_3_QTY,0) " & vbCrLf & _
                        " + ISNULL(SB.GON_4_QTY,0) + ISNULL(SB.GON_5_QTY,0) + ISNULL(SB.GON_6_QTY,0) AS Qty,SB.STATUS FROM BRND_BRANDPACK BB INNER JOIN ORDR_PO_BRANDPACK OPB " & vbCrLf & _
                        " ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                        " INNER JOIN SPPB_BRANDPACK SB ON OOAB.OA_BRANDPACK_ID = SB.OA_BRANDPACK_ID WHERE SB.SPPB_NO = @SPPB_NO ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, Me.SPPB_NO, 15)
                Dim dtTable As New DataTable("T_SPPB") : dtTable.Clear()
                Me.SqlDat.SelectCommand = Me.SqlCom
                Me.SqlDat.Fill(dtTable) : Me.ClearCommandParameters()
                For i As Integer = 0 To dtTable.Rows.Count - 1
                    If dtTable.Rows(i)("STATUS").ToString() = "PENDING" Then
                        StatusPending = True
                    ElseIf dtTable.Rows(i)("STATUS").ToString() = "PARTIAL" Then
                        StatusPartial = True
                    ElseIf dtTable.Rows(i)("STATUS").ToString() = "COMPLETED" Then
                        StatusCompleted = True
                    End If
                    ItemDescription &= dtTable.Rows(i)("BRANDPACK_NAME").ToString() & ",Qty = " & String.Format("{0:#,##0.000}", Convert.ToDecimal(dtTable.Rows(i)("Qty")))
                    If i < dtTable.Rows.Count - 1 Then
                        ItemDescription &= "),("
                    Else
                        ItemDescription &= ")}"
                    End If
                Next
                If StatusPending = True And StatusPartial = True And StatusCompleted = True Then
                    StatusGon = "PARTIAL"
                ElseIf StatusPending = True And StatusPartial = True And StatusCompleted = False Then
                    StatusGon = "PARTIAL"
                ElseIf StatusPending = True And StatusPartial = False And StatusCompleted = True Then
                    StatusGon = "PARTIAL"
                ElseIf StatusPending = False And StatusPartial = True And StatusCompleted = True Then
                    StatusGon = "PARTIAL"
                ElseIf StatusPending = True And StatusPartial = False And StatusCompleted = False Then
                    StatusGon = ""
                ElseIf StatusPending = False And StatusPartial = True And StatusCompleted = False Then
                    StatusGon = "PARTIAL"
                ElseIf StatusPending = False And StatusPartial = False And StatusCompleted = True Then
                    StatusGon = "COMPLETED"
                ElseIf StatusPending = False And StatusPartial = False And StatusCompleted = False Then
                    StatusGon = ""
                End If

                If StatusGon = "" Then : Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Return
                End If
                Message = "Bpk/Ibu Yth,NO PO:" & Me.PO_REF_NO & " SUDAH TERKIRIM(" & StatusGon & ")NO GON:" & Me.ChangedGonNumber & ",TGL:" & strGonDate & ",Terima kasih-Nufarm"
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "DECLARE @V_DISTRIBUTOR_ID VARCHAR(10); " & vbCrLf & _
                        " SET @V_DISTRIBUTOR_ID = (SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = @PO_REF_NO) ;" & vbCrLf & _
                         "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME,CONTACT AS CONTACT_PERSON,HP AS MOBILE FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID = @V_DISTRIBUTOR_ID ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, Me.PO_REF_NO, 30)
                Me.SqlRe = Me.SqlCom.ExecuteReader() : Me.ClearCommandParameters()
                While Me.SqlRe.Read()
                    DistributorID = Me.SqlRe.GetString(0)
                    DistributorName = Me.SqlRe.GetString(1)
                    If Not Me.SqlRe.IsDBNull(2) Then
                        Contact_Person = Me.SqlRe.GetString(2)
                    End If
                    'Contact_Person = Me.SqlRe.GetString(2)
                    If Not Me.SqlRe.IsDBNull(3) Then
                        Mobile = Me.SqlRe.GetString(3)
                    End If
                End While : Me.SqlRe.Close()

                If Mobile = "" Then
                    Throw New Exception("Contact_Mobile of Distributor is null")
                ElseIf Contact_Person = "" Then
                    Throw New Exception("Contact_Person of Distributor is null")
                End If
                Query = "SET NOCOUNT ON ; " & vbCrLf & _
                        "IF EXISTS(SELECT TransactionID FROM GON_SMS WHERE SPPB_NO = @SPPB_NO AND GON_NO = @GON_NO AND GS_NO = @GS_NO) " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " UPDATE GON_SMS SET STATUS_GON = @STATUS_GON,GON_DATE = @GON_DATE,STATUS_SENT = NULL WHERE (SPPB_NO = @SPPB_NO AND GON_NO = @GON_NO AND GS_NO = @GS_NO) ;" & vbCrLf & _
                        " END " & vbCrLf & _
                        "ELSE " & vbCrLf & _
                        " BEGIN " & vbCrLf & _
                        " INSERT INTO GON_SMS(DISTRIBUTOR_ID,SPPB_NO,PO_REF_NO,GON_NO,GS_NO,STATUS_GON,GON_DATE,SENT_TO,MOBILE,ORIGIN_COMPANY,ITEM_DESCRIPTION,MESSAGE) " & vbCrLf & _
                        " VALUES(@DISTRIBUTOR_ID,@SPPB_NO,@PO_REF_NO,@GON_NO,@GS_NO,@STATUS_GON,@GON_DATE,@SENT_TO,@MOBILE,@ORIGIN_COMPANY,@ITEM_DESCRIPTION,@MESSAGE) ;" & vbCrLf & _
                        " END "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                Me.AddParameter("@SPPB_NO", SqlDbType.VarChar, Me.SPPB_NO, 15)
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, Me.PO_REF_NO, 30)
                Me.AddParameter("GON_NO", SqlDbType.VarChar, Me.ChangedGonNumber, 15)
                Me.AddParameter("@GS_NO", SqlDbType.VarChar, Me.ChangedGSNumber, 2)
                Me.AddParameter("@STATUS_GON", SqlDbType.VarChar, StatusGon, 20)
                Me.AddParameter("@GON_DATE", SqlDbType.DateTime, Me.ChangedGonDate)
                Me.AddParameter("@SENT_TO", SqlDbType.VarChar, Contact_Person, 50)
                Me.AddParameter("@MOBILE", SqlDbType.VarChar, Mobile, 20)
                Me.AddParameter("@ORIGIN_COMPANY", SqlDbType.VarChar, DistributorName, 100)
                Me.AddParameter("@ITEM_DESCRIPTION", SqlDbType.VarChar, ItemDescription, 300)
                Me.AddParameter("@MESSAGE", SqlDbType.VarChar, Message, 120)
                Me.SqlCom.ExecuteScalar() : Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()

                'UpdatedRows = ds.Tables(0).Select("", "", DataViewRowState.ModifiedCurrent)
                'If (UpdatedRows.Length > 0) Then
                '    Me.SqlDat.Update(UpdatedRows)
                'End If
                'Me.SqlCom.CommandText = "SELECT * FROM SPPB_BRANDPACK WHERE SPPB_NO = '" & Me.SPPB_NO & "'"
                'Me.SqlCom.CommandType = CommandType.Text
                'Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                'Me.sqlComb = New SqlCommandBuilder(Me.SqlDat)
                'Dim c As Integer = Me.SqlDat.Update(ds.Tables(0))

            Catch ex As Exception
                If Not Me.SqlRe.IsClosed Then : Me.SqlRe.Close()
                End If : Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

#End Region

#Region " Property "

        Public ReadOnly Property ViewSPPBDetail() As DataView
            Get
                Return Me.m_ViewSPPBDetail
            End Get
        End Property

        Public ReadOnly Property ViewOARefNo() As DataView
            Get
                Return Me.m_ViewOARefNo
            End Get
        End Property
        Public ReadOnly Property GetDataSet() As DataSet
            Get
                Return MyBase.baseDataSet
            End Get
        End Property
        Public Overloads ReadOnly Property ViewBrandPack() As DataView
            Get
                Return Me.m_ViewBrandPack
            End Get
        End Property
        Public ReadOnly Property ViewDistributorSPPB() As DataView
            Get
                Return Me.m_ViewDistributorSPPB
            End Get
        End Property
#End Region

#Region " Constructor & Destructor "

        Public Overloads Sub Dispose(ByVal Disposing As Boolean)
            MyBase.Dispose(Disposing)
            If Not IsNothing(Me.m_ViewSPPBDetail) Then
                Me.m_ViewSPPBDetail.RowStateFilter = DataViewRowState.OriginalRows
                Me.m_ViewSPPBDetail.Dispose()
                Me.m_ViewSPPBDetail = Nothing
            End If
            If Not IsNothing(Me.m_ViewOARefNo) Then
                Me.m_ViewOARefNo.Dispose()
                Me.m_ViewOARefNo = Nothing
            End If
            If Not IsNothing(Me.m_ViewBrandPack) Then
                Me.m_ViewBrandPack.Dispose()
                Me.m_ViewBrandPack = Nothing
            End If
            'If Not IsNothing(Me.m_ViewSPPBDetail) Then
            '    Me.m_ViewSPPBDetail.Dispose()
            '    Me.m_ViewSPPBDetail = Nothing
            'End If
            If Not IsNothing(Me.m_ViewDistributorSPPB) Then
                Me.m_ViewDistributorSPPB.Dispose()
                Me.m_ViewDistributorSPPB = Nothing
            End If
            Me.GON_DATE = Nothing
            Me.SHIP_TERRITORY = Nothing
            Me.SPPBDate = Nothing
            Me.GON_NO = Nothing
        End Sub

        Public Sub New()
            MyBase.New()
            Me.m_ViewSPPBDetail = Nothing
            Me.m_ViewOARefNo = Nothing
            Me.m_ViewBrandPack = Nothing
            Me.GON_NO = Nothing
            Me.GON_DATE = Nothing
        End Sub

#End Region

    End Class
End Namespace

