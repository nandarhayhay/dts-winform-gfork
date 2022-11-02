Imports System.Data.SqlClient
Namespace OrderAcceptance
    Public Class OADiscount
        Inherits NufarmBussinesRules.OrderAcceptance.MarAgrProjDiscount

#Region " Deklarasi "
        Private m_ViewGivenDiscount As DataView
        Private m_ViewOtherDiscount As DataView
        Private m_ViewSemesterlyDiscount As DataView
        Private m_ViewQuarterlyDiscount As DataView
        Private m_ViewYearlyDiscount As DataView
        Private M_ViewMarketingDiscount As DataView
        Private m_ViewProjectDiscount As DataView
        Private m_ViewDistributor As DataView
        Private m_ViewOADiscount As DataView
        Private m_ViewRemaindingDisc As DataView
        Public GDiscount As GivenDiscount
        Public FlagSemesterly As SemesterlyFlag
        Public FlagQuarterly As QuarterlyFlag
        Public QuantityType As TypeQuantity
        Public Agreement_Discount As AD
        Public Marketing_Discount As MD
        Public Project_Discount As PD
        Public OA_BRANDPACK_DISCOUNT As OA_BD
        Private DRView As DRowView
        Private StrGivenDiscount As GD
        Public ST As SaveType
        Public DiscQty As DiscountQTY
        Public TOTAL_QTY As Decimal = 0
        Public TOTAL_PRICE_DISTRIBUTOR As Decimal = 0
        Public MAX_DISC_PER_PO As Decimal = 0

        ''Public IsGivenGenerated As Boolean
        Public GeneratedDiscountGiven As Decimal = 0
        Public OA_Remainding As OA_RM
        Public IsgeneratedAgreement As GeneratedAgreement
        Public isGenerateSales As GenerateSales
        Public IsHasSales As HasSales
        Public IsHasProject As Boolean
        Public IsHasOtherDisc As HasOtherDisc
        'Private Query As String = ""
#End Region

#Region " Enum "

        Public Enum SaveType
            Save
            Update
        End Enum

        Public Enum SelectedDiscount
            AgreementDiscount
            MarketingDiscount
            ProjectDiscount
            OtherDiscount
            None
        End Enum

        Public Enum TypeQuantity
            ReleaseQuantity
            LeftQuantity
        End Enum

        Public Enum TypeAgreementDiscount
            Given
            Semesterly
            Quarterly
            Yearly
        End Enum

        Public Enum TypeMarketingDiscount
            GivenDiscount
            TargetDiscount
            SteppingDiscount
            Given_CP
            Given_CPR
            Given_PKPP
            Given_DK
            Given_CPSD
            Given_CPMRT
            Given_DKN
            Given_CPDAuto
        End Enum

        Public Enum GivenDiscount
            AgreementDiscount
            MarketingDiscount
        End Enum

        Public Enum SemesterlyFlag
            S1
            S2
        End Enum

        Public Enum QuarterlyFlag
            Q1
            Q2
            Q3
            Q4
        End Enum

#End Region

#Region " Structure "

        Public Structure DiscountQTY
            Public AGreeGivenDiscQTY As Decimal
            Public AgreeDiscQtyQ1 As Decimal
            Public AgreeDiscQtyQ2 As Decimal
            Public AgreeDiscQtyQ3 As Decimal
            Public AgreeDiscQtyQ4 As Decimal
            Public AGreeDiscQtyS1 As Decimal
            Public AGreeDiscQtyS2 As Decimal
            Public YearlyDiscQty As Decimal

            Public MarketingGivenDisQty As Decimal
            Public MarketingDiscQty As Decimal
            Public ProjectDiscQty As Decimal
        End Structure

        Public Structure AD
            Public AGREE_DISC_HIST_ID As String 'VARCHAR(71),
            Public AGREE_BRANDPACK_ID As String ' VARCHAR(39),
            Public OA_BRANDPACK_ID As String 'VARCHAR(44),
            Public AGREE_OA_QTY As Decimal  'INT,
            Public AGREE_OA_DISC_PCT As Decimal  ' VARCHAR(6),
            Public AGREE_DISC_QTY As Decimal  'INT,
            Public AGREE_RELEASE_QTY As Decimal  'INT,
            Public AGREE_LEFT_QTY As Decimal  ' INT,
            Public GQSY_FLAG As Decimal  ' VARCHAR(5),

            Public BRANDPACK_ID As String
            Public BRANDPACK_NAME As String
            Public PO_REF_NO As String
            Public PO_REF_DATE As Date

        End Structure

        Private Structure GD
            Public AGREE_DISC_HIST_ID As String
            Public BRANDPACK_ID As String
            Public BRANDPACK_NAME As String
            Public PO_REF_DATE As Date
            Public PO_REF_NO As String
            Public AGREE_BRANDPACK_ID As String
            Public OA_BRANDPACK_ID As String
            Public AGREE_OA_QTY As Decimal
            Public AGREE_OA_DISC_PCT As Decimal
            Public AGREE_DISC_QTY As Decimal
            Public AGREE_RELEASE_QTY As Decimal
            Public AGREE_LEFT_QTY As Decimal
            Public GQSY_FLAG As String

            Public MRKT_DISC_HIST_ID As String ' VARCHAR(66),
            Public PROG_BRANDPACK_DIST_ID As String 'VARCHAR(35),
            Public MRKT_OA_QTY As Decimal  ' INT,
            Public MRKT_DISC_QTY As Decimal  'INT,
            Public MRKT_RELEASE_QTY As Decimal  'INT,
            Public MRKT_LEFT_QTY As Decimal  'INT,
            Public SGT_FLAG As String 'CHAR(1),
            Public GIVEN_DISC_PCT As Decimal

        End Structure

        Public Structure MD
            Public MRKT_DISC_HIST_ID As String ' VARCHAR(66),
            Public OA_BRANDPACK_ID As String 'VARCHAR(44),
            Public PROG_BRANDPACK_DIST_ID As String 'VARCHAR(35),
            Public MRKT_OA_QTY As Decimal  ' INT,
            Public MRKT_DISC_PCT As Decimal
            Public MRKT_DISC_QTY As Decimal  'INT,
            Public MRKT_RELEASE_QTY As Decimal  'INT,
            Public MRKT_LEFT_QTY As Decimal  'INT,
            Public SGT_FLAG As String 'CHAR(1),

            Public BRANDPACK_ID As String
            Public BRANDPACK_NAME As String
            Public PO_REF_NO As String
            Public PO_REF_DATE As Date
            Public TARGET_DISC_PCT As Decimal

        End Structure

        Public Structure PD
            Public BRANDPACK_ID As String
            Public BRANDPACK_NAME As String
            Public PROJ_REF_NO As String
            Public APPROVED_DISC_PCT As Decimal

            Public PO_REF_NO As String
            Public PO_REF_DATE As Date

            Public PROJ_DISC_HIST_ID As String 'VARCHAR(66),
            Public PROJ_BRANDPACK_ID As String 'VARCHAR(25),
            Public OA_BRANDPACK_ID As String 'VARCHAR(44),
            Public PROJ_OA_QTY As Decimal   'INT,
            Public PROJ_DISC_QTY As Decimal  ' INT,
            Public PROJ_RELEASE_QTY As Decimal  ' INT,
            Public PROJ_LEFT_QTY As Decimal  'INT,
            Public END_DATE As Date

        End Structure

        Public Structure OA_BD
            Public OA_BRANDPACK_DISC_ID As Int32
            Public OA_BRANDPACK_ID As String 'VARCHAR(44),
            Public GQSY_SGT_P_FLAG As String 'VARCHAR(5),
            Public DISC_QUANTITY As Decimal  ' 
            Public PRICE_PRQTY As Decimal   'VARCHAR(10),
            Public AGREE_DISC_HIST_ID As Object  'VARCHAR(71),
            Public MRKT_DISC_HIST_ID As Object  'VARCHAR(66),
            Public PROJ_DISC_HIST_ID As Object  'VARCHAR(66),
            Public MRK_M_S_ID As Object
            Public BRND_B_S_ID As Object
            Public BRANDPACK_ID As String
            Public OA_RM_ID As Object
            Public BRANDPACK_NAME As String
            Public PO_REF_NO As String
            Public PO_REF_DATE As Date
            Public TOTAL As Decimal
            Public ACHIEVEMENT_BRANDPACK_ID As Object
            Public AdjustQty As Decimal
            Public RefOther As Integer
        End Structure

        Private Structure DRowView
            Public DataRowViewGiven As DataRowView
            Public DataRowViewQuarterly As DataRowView
            Public DataRowViewSemesterly As DataRowView
            Public DataRowViewYearly As DataRowView
            Public DataRowViewMarketing As DataRowView
            Public DataRowViewProject As DataRowView
            Public DataRowViewOther As DataRowView
            Public DataRowViewOADiscount As DataRowView

            Public Sub Dispose()
                If Not IsNothing(Me.DataRowViewGiven) Then
                    Me.DataRowViewGiven = Nothing
                End If
                If Not IsNothing(Me.DataRowViewMarketing) Then
                    Me.DataRowViewMarketing = Nothing
                End If
                If Not IsNothing(Me.DataRowViewOther) Then
                    Me.DataRowViewOther = Nothing
                End If
                If Not IsNothing(Me.DataRowViewProject) Then
                    Me.DataRowViewProject = Nothing
                End If
                If Not IsNothing(Me.DataRowViewQuarterly) Then
                    Me.DataRowViewQuarterly = Nothing
                End If
                If Not IsNothing(Me.DataRowViewSemesterly) Then
                    Me.DataRowViewSemesterly = Nothing
                End If
                If Not IsNothing(Me.DataRowViewYearly) Then
                    Me.DataRowViewYearly = Nothing
                End If
            End Sub

        End Structure

        Public Structure OA_RM
            Public AGREE_DISC_HIST_ID As Object
            Public BRND_B_S_ID As Object
            Public ACHIEVMENT_BRANDPACK_ID As Object
            Public MRKT_DISC_HISt_ID As Object
            Public MRKT_M_S_ID As Object
            Public PROJ_DISC_HIST_ID As Object
            Public FLAG As String
            Public RM_OA_ID As Object
            Public OA_BRANDPACK_ID As String
            Public PRICE_PRQTY As Decimal
            Public tblAdjustment As DataTable
            Public RefOther As Integer
        End Structure

        Public Structure GeneratedAgreement
            Public Q1 As Boolean
            Public Q2 As Boolean
            Public Q3 As Boolean
            Public Q4 As Boolean
            Public S1 As Boolean
            Public S2 As Boolean
            Public Y As Boolean
        End Structure

        Public Structure GenerateSales
            Public MT As Boolean
            Public MS As Boolean
        End Structure

        Public Structure HasSales
            Public DK As Boolean
            Public PKPP As Boolean
            Public CPR As Boolean
            ''' <summary>
            ''' Target CPD hanya ke distributor
            ''' bonus di berikan bila sudah tercapai target
            ''' </summary>
            ''' <remarks></remarks>
            Public CPD As Boolean 'target to Distributor
            Public MG As Boolean
            ''' <summary>
            ''' Target ke distributor saja
            ''' bonus di berikan setiap ada PO ,sampai periode berakhir atau tercapai target
            ''' </summary>
            ''' <remarks></remarks>
            Public SCPD As Boolean ' target to distributor
            ''' <summary>
            ''' target CPD to distributor and TM
            ''' bonus di berikan bila sudah tercapai target
            ''' </summary>
            ''' <remarks></remarks>
            Public CPD_TM_Distributor As Boolean 'target to distributor and TM
            ''' <summary>
            ''' target CPDS to distributor and TM
            ''' bonus di berikan setiap ada PO ,sampai periode berakhir atau tercapai target
            ''' </summary>
            ''' <remarks></remarks>
            Public CPDS_TM_Distributor As Boolean 'target to Distributor and TM
            Public CPMRT_DIST As Boolean 'target to Distributor only
            Public CPMRT_DIST_TM As Boolean 'target to Distributor and TM
            Public DK_N As Boolean
            Public CPDAuto As Boolean
        End Structure

        Public Structure HasOtherDisc
            Public O As Boolean
            Public OCBD As Boolean
            Public ODD As Boolean
            Public ODR As Boolean
            Public ODK As Boolean
        End Structure
#End Region

#Region " Function "

#Region " OA_BRANDPACK DISCOUNT "

        Public Function CreateViewOABrandPackDiscount(ByVal OA_BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As DataView
            Try
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_CreateViewOA_DISCOUNT", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_CreateViewOA_DISCOUNT")
                End If
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                Dim tblOADiscount As New DataTable("OA_BRANDPACK_DISCOUNT")
                Me.OpenConnection()
                If Me.SqlDat Is Nothing Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                Me.SqlDat.Fill(tblOADiscount) : Me.ClearCommandParameters()
                If tblOADiscount.Rows.Count <= 0 Then
                    Me.m_ViewOADiscount = Nothing
                    Return Me.m_ViewOADiscount
                End If
                Dim ColDiscFrom As New DataColumn("DISCOUNT_FROM", Type.GetType("System.String"), "")
                tblOADiscount.Columns.Add(ColDiscFrom)
                Dim ColIsRemainding As New DataColumn("ISREMAINDING", Type.GetType("System.Boolean"))
                tblOADiscount.Columns.Add(ColIsRemainding)
                Me.m_ViewOADiscount = tblOADiscount.DefaultView()
                Me.m_ViewOADiscount.Sort = "OA_BRANDPACK_DISC_ID ASC"
                Me.m_ViewOADiscount.RowStateFilter = DataViewRowState.CurrentRows
                With Me.m_ViewOADiscount
                    If Me.m_ViewOADiscount.Table.Rows.Count > 0 Then
                        'Me.drv = .AddNew()
                        For i As Integer = 0 To Me.m_ViewOADiscount.Table.Rows.Count - 1
                            Dim flag As Object = DBNull.Value
                            Select Case Me.m_ViewOADiscount(i)("GQSY_SGT_P_FLAG").ToString()
                                Case "S1" : flag = "SEMESTERLY 1"
                                Case "S2" : flag = "SEMESTERLY 2"
                                Case "Q1" : flag = "QUARTERLY 1"
                                Case "Q2" : flag = "QUARTERLY 2"
                                Case "Q3" : flag = "QUARTERLY 3"
                                Case "Q4" : flag = "QUARTERLY 4"
                                Case "Y" : flag = "YEARLY DISC"
                                Case "DK" : flag = "DK"
                                Case "KP" : flag = "PKPP"
                                Case "MG" : flag = "DPRD"
                                Case "O" : flag = "OTHER DISC"
                                Case "OCBD" : flag = "OTHER DISC(CBD)"
                                Case "ODD" : flag = "OTHER DISC(DD)"
                                Case "ODR" : flag = "OTHER DISC(DR)"
                                Case "ODK" : flag = "OTHER DISC(DK)"
                                Case "CR" : flag = "CP(RD)"
                                Case "G" : flag = "GIVEN PKD"
                                Case "CP" : flag = "CP(D)_DIST"
                                Case "TD" : flag = "CP(D)_DIST_TM"
                                Case "CS" : flag = "CP(D)S_DIST"
                                Case "TS" : flag = "CP(D)S_DIST_TM"
                                Case "CD" : flag = "CP(R M/T)_DIST"
                                Case "CT" : flag = "CP(R M/T)_DIST_TM"
                                Case "RMOA" : flag = "REMAINDING_OA"
                                Case "DN" : flag = "DK(N)"
                                Case "CA" : flag = "CP(D)AUTO"
                            End Select
                            If Not IsDBNull(Me.m_ViewOADiscount(i)("OA_RM_ID")) Then
                                .Item(i)("ISREMAINDING") = True
                            Else
                                .Item(i)("ISREMAINDING") = False
                            End If
                            .Item(i)("DISCOUNT_FROM") = flag
                            .Item(i).EndEdit()
                        Next
                    End If
                End With
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewOADiscount
        End Function

        Public Function CreateViewGivenDiscount(ByVal OA_ID As String, ByVal BRANDPACK_ID As String, ByVal Given As GivenDiscount, ByVal Type As TypeQuantity, ByVal DISTRIBUTOR_ID As String, ByVal FLAG As String, ByVal MustCloseConnection As Boolean) As DataView
            Try
                Select Case Given
                    Case GivenDiscount.AgreementDiscount
                        Select Case Type
                            Case TypeQuantity.LeftQuantity
                                If Me.SqlCom Is Nothing Then : Me.CreateCommandSql("Usp_Select_LEFTGIVENROW", "")
                                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Select_LEFTGIVENROW")
                                End If
                                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(35),
                                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(30)
                                Me.AddParameter("@GQSY_FLAG", SqlDbType.VarChar, FLAG, 5)
                                Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 32)
                            Case TypeQuantity.ReleaseQuantity

                        End Select
                        If Me.SqlDat Is Nothing Then
                            Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                        Else : Me.SqlDat.SelectCommand = Me.SqlCom
                        End If
                        Dim tblAgreementGivenDiscount As New DataTable("AGREEEMENT_GIVEN_DISCOUNT")
                        tblAgreementGivenDiscount.Clear()
                        Me.OpenConnection()
                        Me.SqlDat.Fill(tblAgreementGivenDiscount)
                        'Me.FillDataTable(tblAgreementGivenDiscount)
                        Me.m_ViewGivenDiscount = tblAgreementGivenDiscount.DefaultView()
                        Me.m_ViewGivenDiscount.Sort = "AGREE_DISC_HIST_ID"
                        Me.m_ViewGivenDiscount.RowStateFilter = DataViewRowState.CurrentRows
                    Case GivenDiscount.MarketingDiscount
                        If FLAG = "DN" Then
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    " DECLARE @V_OA_DATE SMALLDATETIME; " & vbCrLf & _
                                    " SET @V_OA_DATE = (SELECT OA_DATE FROM ORDR_ORDER_ACCEPTANCE WHERE OA_ID = @OA_ID); " & vbCrLf & _
                                    " SELECT MDH.MRKT_DISC_HIST_ID,PROPOSED_BY = '(Internal SOP)',PROGRAM_ID = 'DK(N)/' + MDH.SDS_PERIODE,OPB.BRANDPACK_ID,BB.BRANDPACK_NAME,OPB.PO_REF_NO,PO.PO_REF_DATE," & vbCrLf & _
                                    " MDH.OA_BRANDPACK_ID,MDH.PROG_BRANDPACK_DIST_ID,MDH.MRKT_OA_QTY,MDH.MRKT_DISC_PCT," & vbCrLf & _
                                    " MDH.MRKT_DISC_QTY, MDH.MRKT_RELEASE_QTY, MDH.MRKT_LEFT_QTY, MDH.SGT_FLAG " & vbCrLf & _
                                    " FROM MRKT_DISC_HISTORY MDH INNER JOIN ORDR_OA_BRANDPACK OOA ON OOA.OA_BRANDPACK_ID = MDH.OA_BRANDPACK_ID " & vbCrLf & _
                                    " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOA.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                                    " INNER JOIN BRND_BRANDPACK BB ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID " & vbCrLf & _
                                    " INNER JOIN ORDR_PURCHASE_ORDER PO ON OPB.PO_REF_NO = PO.PO_REF_NO INNER JOIN ORDR_ORDER_ACCEPTANCE OOC " & vbCrLf & _
                                    " ON OOC.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                                    " WHERE OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                                    " AND MDH.SGT_FLAG = @SGT_FLAG AND OOC.OA_DATE <= @V_OA_DATE " & vbCrLf & _
                                    " AND MDH.MRKT_LEFT_QTY > 0 " & vbCrLf & _
                                    " AND YEAR(OOC.OA_DATE) >= YEAR(GETDATE()) -1;"
                            If Me.SqlCom Is Nothing Then
                                Me.CreateCommandSql("", Query)
                            Else : Me.ResetCommandText(CommandType.Text, Query)
                            End If
                        ElseIf FLAG = "CA" Then
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                  " DECLARE @V_OA_DATE SMALLDATETIME; " & vbCrLf & _
                                  " SET @V_OA_DATE = (SELECT OA_DATE FROM ORDR_ORDER_ACCEPTANCE WHERE OA_ID = @OA_ID); " & vbCrLf & _
                                  " SELECT MDH.MRKT_DISC_HIST_ID,PROPOSED_BY = '(Internal SOP)',PROGRAM_ID = 'CP_D(AUTO)/' + MDH.SDS_PERIODE,OPB.BRANDPACK_ID,BB.BRANDPACK_NAME,OPB.PO_REF_NO,PO.PO_REF_DATE," & vbCrLf & _
                                  " MDH.OA_BRANDPACK_ID,MDH.PROG_BRANDPACK_DIST_ID,MDH.MRKT_OA_QTY,MDH.MRKT_DISC_PCT," & vbCrLf & _
                                  " MDH.MRKT_DISC_QTY, MDH.MRKT_RELEASE_QTY, MDH.MRKT_LEFT_QTY, MDH.SGT_FLAG,MDH.DESCRIPTION " & vbCrLf & _
                                  " FROM MRKT_DISC_HISTORY MDH INNER JOIN ORDR_OA_BRANDPACK OOA ON OOA.OA_BRANDPACK_ID = MDH.OA_BRANDPACK_ID " & vbCrLf & _
                                  " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOA.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                                  " INNER JOIN BRND_BRANDPACK BB ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID " & vbCrLf & _
                                  " INNER JOIN ORDR_PURCHASE_ORDER PO ON OPB.PO_REF_NO = PO.PO_REF_NO INNER JOIN ORDR_ORDER_ACCEPTANCE OOC " & vbCrLf & _
                                  " ON OOC.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                                  " WHERE OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                                  " AND MDH.SGT_FLAG = @SGT_FLAG AND OOC.OA_DATE <= @V_OA_DATE " & vbCrLf & _
                                  " AND MDH.MRKT_LEFT_QTY > 0 " & vbCrLf & _
                                  " AND YEAR(OOC.OA_DATE) >= YEAR(GETDATE()) -1;"
                            If Me.SqlCom Is Nothing Then
                                Me.CreateCommandSql("", Query)
                            Else : Me.ResetCommandText(CommandType.Text, Query)
                            End If
                        Else
                            'Select Case Type
                            '    Case TypeQuantity.LeftQuantity

                            '    Case TypeQuantity.ReleaseQuantity
                            'End Select
                            If Me.SqlCom Is Nothing Then
                                Me.CreateCommandSql("Usp_Select_LEFT_GIVEN_MARKETING", "")
                            Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Select_LEFT_GIVEN_MARKETING")
                            End If
                        End If
                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                        Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, FLAG, 2) ' CHAR(1)
                        Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 32)
                        If Me.SqlDat Is Nothing Then
                            Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                        Else : Me.SqlDat.SelectCommand = Me.SqlCom
                        End If
                        Dim tblMRKTGivenDiscount As New DataTable("SALES_GIVEN_DISCOUNT")
                        tblMRKTGivenDiscount.Clear()
                        'Me.FillDataTable(tblMRKTGivenDiscount)
                        Me.OpenConnection()
                        Me.SqlDat.Fill(tblMRKTGivenDiscount)
                        Me.m_ViewGivenDiscount = tblMRKTGivenDiscount.DefaultView()
                        Me.m_ViewGivenDiscount.Sort = "MRKT_DISC_HIST_ID"
                        Me.m_ViewGivenDiscount.RowStateFilter = DataViewRowState.CurrentRows
                End Select
                If MustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewGivenDiscount
        End Function

        Public Function CreateViewQuarterlyDiscount(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal Type As TypeQuantity, ByVal Flag As QuarterlyFlag, ByVal PO_DATE As Object, ByVal mustCloseConnection As Boolean) As DataView
            Try
                Select Case Type
                    Case TypeQuantity.LeftQuantity
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Select_Left_Qty_Agreement_Saving", "")
                        Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Select_Left_Qty_Agreement_Saving")
                        End If

                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10)
                        Me.AddParameter("@DISC_AGREE_FROM", SqlDbType.VarChar, NufarmBussinesRules.SharedClass.DISC_AGREE_FROM, 20) ' VARCHAR(5),
                        Me.AddParameter("@PO_DATE", SqlDbType.DateTime, PO_DATE)
                        Select Case Flag
                            Case QuarterlyFlag.Q1
                                Me.AddParameter("@FLAG", SqlDbType.VarChar, "Q1", 5)
                            Case QuarterlyFlag.Q2
                                Me.AddParameter("@FLAG", SqlDbType.VarChar, "Q2", 5)
                            Case QuarterlyFlag.Q3
                                Me.AddParameter("@FLAG", SqlDbType.VarChar, "Q3", 5)
                            Case QuarterlyFlag.Q4
                                Me.AddParameter("@FLAG", SqlDbType.VarChar, "Q4", 5)
                        End Select
                        'Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                        'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10)
                    Case TypeQuantity.ReleaseQuantity

                End Select

                Dim TblQuarterlyDiscount As New DataTable("QUARTERLY_DISCOUNT")
                TblQuarterlyDiscount.Clear()
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                Me.OpenConnection() : Me.SqlDat.Fill(TblQuarterlyDiscount)
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
                Me.m_ViewQuarterlyDiscount = TblQuarterlyDiscount.DefaultView
                'Me.m_ViewQuarterlyDiscount.Sort = "BRND_B_S_ID"
                Me.m_ViewQuarterlyDiscount.RowStateFilter = DataViewRowState.CurrentRows
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewQuarterlyDiscount
        End Function

        Public Function CreateViewYearlyDiscount(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal Type As TypeQuantity, ByVal PO_DATE As Object, ByVal mustCloseConnection As Boolean) As DataView
            Try
                Select Case Type
                    Case TypeQuantity.LeftQuantity
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Select_Left_Qty_Agreement_Saving", "")
                        Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Select_Left_Qty_Agreement_Saving")
                        End If
                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10)
                        Me.AddParameter("@DISC_AGREE_FROM", SqlDbType.VarChar, NufarmBussinesRules.SharedClass.DISC_AGREE_FROM, 20) ' VARCHAR(5),
                        Me.AddParameter("@FLAG", SqlDbType.VarChar, "Y")
                        Me.AddParameter("@PO_DATE", SqlDbType.DateTime, PO_DATE)

                        Dim tblYearlyDiscount As New DataTable("YEARLY_DISCOUNT")
                        tblYearlyDiscount.Clear()
                        If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                        Else : Me.SqlDat.SelectCommand = Me.SqlCom
                        End If
                        Me.OpenConnection() : Me.SqlDat.Fill(tblYearlyDiscount)
                        If mustCloseConnection Then : Me.CloseConnection() : End If
                        Me.ClearCommandParameters()
                        'Me.FillDataTable(tblYearlyDiscount)
                        Me.m_ViewYearlyDiscount = tblYearlyDiscount.DefaultView
                        ''Me.m_ViewYearlyDiscount.Sort = "BRND_B_S_ID"
                        Me.m_ViewYearlyDiscount.RowStateFilter = DataViewRowState.CurrentRows
                    Case TypeQuantity.ReleaseQuantity

                End Select

            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewYearlyDiscount
        End Function

        Public Function CreateViewSemesterlyDiscount(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal Type As TypeQuantity, ByVal Flag As SemesterlyFlag, ByVal PO_DATE As Object, ByVal mustCloseConnectionDB As Boolean) As DataView
            Try
                Select Case Type
                    Case TypeQuantity.LeftQuantity
                        If IsNothing(Me.SqlCom) Then
                            Me.CreateCommandSql("Usp_Select_Left_Qty_Agreement_Saving", "")
                        Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Select_Left_Qty_Agreement_Saving")
                        End If
                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10)
                        Me.AddParameter("@DISC_AGREE_FROM", SqlDbType.VarChar, NufarmBussinesRules.SharedClass.DISC_AGREE_FROM, 20) ' VARCHAR(5),
                        Me.AddParameter("@PO_DATE", SqlDbType.DateTime, PO_DATE)
                        Select Case Flag
                            Case SemesterlyFlag.S1
                                Me.AddParameter("@FLAG", SqlDbType.VarChar, "S1", 5)
                            Case SemesterlyFlag.S2
                                Me.AddParameter("@FLAG", SqlDbType.VarChar, "S2", 5)
                        End Select
                        'Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                        'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10)
                    Case TypeQuantity.ReleaseQuantity

                End Select
                Dim tblSemesterlyDiscount As New DataTable("SEMESTERLY_DISCOUNT")
                tblSemesterlyDiscount.Clear()
                If Me.SqlDat Is Nothing Then
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Else : Me.SqlDat.SelectCommand = Me.SqlCom
                End If
                Me.OpenConnection() : Me.SqlDat.Fill(tblSemesterlyDiscount)
                'Me.FillDataTable(tblSemesterlyDiscount)
                If mustCloseConnectionDB Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()

                Me.m_ViewSemesterlyDiscount = tblSemesterlyDiscount.DefaultView
                'Me.m_ViewSemesterlyDiscount.Sort = "BRND_B_S_ID"
                Me.m_ViewSemesterlyDiscount.RowStateFilter = DataViewRowState.CurrentRows
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
            Return Me.m_ViewSemesterlyDiscount
        End Function

        Public Function CreateViewMarketingDiscount(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal TypeMarketing As TypeMarketingDiscount, ByVal Type As TypeQuantity) As DataView
            Try
                Select Case Type
                    Case TypeQuantity.LeftQuantity
                        Select Case TypeMarketing
                            Case TypeMarketingDiscount.SteppingDiscount
                                Me.CreateCommandSql("Sp_Select_LEFT_STEPPING_MARKETING", "")

                            Case TypeMarketingDiscount.TargetDiscount
                                Me.CreateCommandSql("Sp_Select_LEFT_TARGET_MARKETING", "")
                        End Select
                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10)

                    Case TypeQuantity.ReleaseQuantity

                End Select
                Dim tblMarketingDiscount As New DataTable("MARKETING_DISCOUNT")
                tblMarketingDiscount.Clear()
                Me.FillDataTable(tblMarketingDiscount)
                Me.M_ViewMarketingDiscount = tblMarketingDiscount.DefaultView
                Me.M_ViewMarketingDiscount.ApplyDefaultSort() = True
                Me.M_ViewMarketingDiscount.RowStateFilter = DataViewRowState.CurrentRows
                'ME.M_ViewMarketingDiscount.AL
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.M_ViewMarketingDiscount
        End Function

        Public Function CreateViewProjectDiscount(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal Type As TypeQuantity) As DataView
            Try
                Select Case Type
                    Case TypeQuantity.LeftQuantity
                        Me.CreateCommandSql("Sp_Select_LEFT_PROJECT_DISCOUNT", "")
                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10)
                    Case TypeQuantity.ReleaseQuantity

                End Select
                Dim tblProjectDiscount As New DataTable("PROJECT_DISCOUNT")
                tblProjectDiscount.Clear()
                Me.FillDataTable(tblProjectDiscount)
                Me.m_ViewProjectDiscount = tblProjectDiscount.DefaultView
                Me.m_ViewProjectDiscount.Sort = "PROJ_DISC_HIST_ID"
                Me.m_ViewProjectDiscount.RowStateFilter = DataViewRowState.CurrentRows

            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewProjectDiscount
        End Function

        Public Function HasReferencedDataAGREE_DISC_HISTORY(ByVal AGREE_DISC_HIST_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_REFERENCED_AGREE_DISC_HISTORY", "")
                Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, AGREE_DISC_HIST_ID, 115) ' VARCHAR(71)
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

        Public Function HasReferencedDataMRKT_DISC_HISTORY(ByVal MRKT_DISC_HIST_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_REFERENCED_MRKT_DISC_HISTORY", "")
                Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
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

        Public Function HasReferencedPROJ_DISC_HISTORY(ByVal PROJ_DISC_HIST_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_Check_REFERENCED_PROJ_DISC_HISTORY", "")
                Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, PROJ_DISC_HIST_ID, 66)
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

        Public Function HasGeneratedDiscountAgreement(ByVal OA_BRANDPACK_ID As String, ByVal IsQuarterly As Boolean, _
        ByVal isSemesterly As Boolean, ByVal mustCloseConnection As Boolean, Optional ByVal FlagQuarterly As QuarterlyFlag = Nothing, _
        Optional ByVal FlagSemesterly As SemesterlyFlag = Nothing, Optional ByVal IsGiven As Boolean = False, Optional ByVal IsYearlyDiscount As Boolean = False) As Boolean
            Try
                '@OA_BRANDPACK_ID VARCHAR(44),
                '@GQSY_FLAG VARCHAR(5)
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Sp_HasGeneratad_AGREE_OA_BRANDPACK_ID", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_HasGeneratad_AGREE_OA_BRANDPACK_ID")
                End If
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 70)
                If isSemesterly = True Then
                    Select Case FlagSemesterly
                        Case SemesterlyFlag.S1
                            Me.AddParameter("@GQSY_FLAG", SqlDbType.VarChar, "S1", 5)
                        Case SemesterlyFlag.S2
                            Me.AddParameter("@GQSY_FLAG", SqlDbType.VarChar, "S2", 5)
                    End Select
                ElseIf IsQuarterly = True Then
                    Select Case FlagQuarterly
                        Case QuarterlyFlag.Q1
                            Me.AddParameter("@GQSY_FLAG", SqlDbType.VarChar, "Q1", 5)
                        Case QuarterlyFlag.Q2
                            Me.AddParameter("@GQSY_FLAG", SqlDbType.VarChar, "Q2", 5)
                        Case QuarterlyFlag.Q3
                            Me.AddParameter("@GQSY_FLAG", SqlDbType.VarChar, "Q3", 5)
                        Case QuarterlyFlag.Q4
                            Me.AddParameter("@GQSY_FLAG", SqlDbType.VarChar, "Q4", 5)
                    End Select
                ElseIf IsGiven = True Then
                    Me.AddParameter("@GQSY_FLAG", SqlDbType.VarChar, "G", 5)
                ElseIf IsYearlyDiscount = True Then
                    Me.AddParameter("@GQSY_FLAG", SqlDbType.VarChar, "Y", 5)
                End If
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar()
                Dim retval As Object = Me.SqlCom.Parameters()("@RETURN_VALUE").Value
                If Not IsNothing(retval) Then
                    Me.CloseConnection()
                    Me.ClearCommandParameters()
                    Return (CInt(retval) > 0)
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters() : Return False

            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function
        Public Function hasGenerateDiscountOthers(ByVal OA_BRANDPACK_ID As String, ByVal Flag As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SELECT 1 WHERE EXISTS(SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID AND GQSY_SGT_P_FLAG = @FLAG);"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 70) ' VARCHAR(44),
                Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                Me.OpenConnection() : Dim retval As Object = Me.SqlCom.ExecuteScalar()
                If Not IsNothing(retval) Then
                    If CInt(retval) > 0 Then
                        If mustCloseConnection Then : Me.CloseConnection() : End If : Me.ClearCommandParameters() : Return True
                    End If
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If : Me.ClearCommandParameters()
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Public Function HasGeneratedDiscountMarketing(ByVal OA_BRANDPACK_ID As String, ByVal FLAG As String, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("Sp_HasGenerated_MRK_OA_BRANDPACK_ID", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "Sp_HasGenerated_MRK_OA_BRANDPACK_ID")
                End If
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 70) ' VARCHAR(44),
                Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, FLAG, 5)
                'Me.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.ReturnValue)
                Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar()
                Dim retval As Object = Me.SqlCom.Parameters()("@RETURN_VALUE").Value
                If Not IsNothing(retval) Then
                    If CInt(retval > 0) Then
                        Me.ClearCommandParameters() : Me.CloseConnection() : Return True
                    End If
                End If
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return False
                'Me.CloseConnection()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return False
        End Function

        Public Function HasGeneratedDiscountProject(ByVal OA_BRANDPACK_ID As String) As Boolean
            Try
                Me.CreateCommandSql("Sp_HasGenerated_PROJ_OA_BRANDPACK_ID", "")
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) '
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

        Public Function CreateViewDistributor()
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR " & vbCrLf & _
                        " WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID); "
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                'Me.CreateCommandSql("", "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR WHERE DISTRIBUTOR_ID" & _
                '                        " IN(SELECT DISTINCT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER)")
                Dim tblDistributor As New DataTable("DISTRIBUTOR")
                tblDistributor.Clear()
                Me.FillDataTable(tblDistributor)
                Me.m_ViewDistributor = tblDistributor.DefaultView()
                Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistributor
        End Function

        Public Function isExistedOA_BRANDPACK_DISC_ID(ByVal OA_BRANDPACK_ID As String, _
        Optional ByVal AGREE_DISC_HIST_ID As String = "", Optional ByVal BRND_B_S_ID As String = "", _
        Optional ByVal MRKT_DISC_HIST_ID As String = "", Optional ByVal MRKT_M_S_ID As String = "", Optional ByVal PROJ_DISC_HIST_ID As String = "") As Boolean
            Try
                If Not AGREE_DISC_HIST_ID <> "" Then
                    If Not IsNothing(Me.ExecuteScalar("", "SELECT OA_BRANDPACK_DISC_ID FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_ID  '" & OA_BRANDPACK_ID & _
                    "' AND AGREE_DISC_HIST_ID = '" & AGREE_DISC_HIST_ID & "'")) Then
                        Return True
                    End If
                ElseIf BRND_B_S_ID <> "" Then
                    If Not IsNothing(Me.ExecuteScalar("", "SELECT OA_BRANDPACK_DISC_ID FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_ID  '" & OA_BRANDPACK_ID & _
                     "' AND BRND_B_S_ID = '" & BRND_B_S_ID & "'")) Then
                        Return True
                    End If
                ElseIf MRKT_DISC_HIST_ID <> "" Then
                    If Not IsNothing(Me.ExecuteScalar("", "SELECT OA_BRANDPACK_DISC_ID FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_ID  '" & OA_BRANDPACK_ID & _
                     "' AND MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "'")) Then
                        Return True
                    End If
                ElseIf MRKT_M_S_ID <> "" Then
                    If Not IsNothing(Me.ExecuteScalar("", "SELECT OA_BRANDPACK_DISC_ID FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_ID  '" & OA_BRANDPACK_ID & _
                     "' AND MRKT_M_S_ID = '" & MRKT_M_S_ID & "'")) Then
                        Return True
                    End If
                ElseIf Not PROJ_DISC_HIST_ID <> "" Then
                    If Not IsNothing(Me.ExecuteScalar("", "SELECT OA_BRANDPACK_DISC_ID FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_ID  '" & OA_BRANDPACK_ID & _
                        "' AND PROJ_DISC_HIST__ID = '" & PROJ_DISC_HIST_ID & "'")) Then
                        Return True
                    End If
                End If
                Return False
            Catch ex As Exception

            End Try
        End Function

        Public Function GetQTYRemainding(ByVal OA_RM_ID As String) As Decimal
            Try
                Dim QTYDecimal As Object = Me.ExecuteScalar("", "SELECT QTY FROM ORDR_OA_REMAINDING" & _
                " WHERE OA_RM_ID = '" & OA_RM_ID & "'")
                If (IsNothing(QTYDecimal)) Or (IsDBNull(QTYDecimal)) Then
                    Throw New Exception("can not find data." & vbCrLf & "Please reselect Brandpack.")
                End If
                Return Convert.ToDecimal(QTYDecimal)
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function GetTotalPriceOAGiven(ByVal OA_ID As String, ByVal mustCloseConnection As Boolean) As Object
            Try
                Me.CreateCommandSql("Sp_GetSumPrice_Given_by_OA", "")
                Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 32)
                'Me.AddParameter("@SUMPRICE", SqlDbType.Float, ParameterDirection.Output)
                Me.SqlCom.Parameters.Add("@SUMPRICE", SqlDbType.Decimal, 0).Direction = ParameterDirection.Output
                Me.OpenConnection()
                Me.SqlCom.ExecuteNonQuery()
                Dim retval As Object = Me.SqlCom.Parameters()("@SUMPRICE").Value
                Me.ClearCommandParameters()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return retval
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function

        Public Function GetOtherDiscCPD(ByVal PROG_BRANDPACK_DIST_ID As String, ByVal MRKT_DIST_HIST_ID As String, ByVal discQty As Decimal, ByVal mustCloseConnection As Boolean) As Boolean
            Try
                Query = "SET NOCOUNT ON " & vbCrLf & _
                    "SELECT 1 WHERE EXISTS(SELECT MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID " & vbCrLf & _
                    " AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY >= 0 AND MRKT_DISC_QTY < @DiscQty );"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@DiscQty", SqlDbType.Decimal, discQty)
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, PROG_BRANDPACK_DIST_ID, 40)
                Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DIST_HIST_ID)
                Me.OpenConnection() : Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    If (CInt(retval) > 0) Then
                        Me.CloseConnection() : Return True
                    End If
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Function GetAdjustmentData(ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal PODate As DateTime, ByVal mustCloseConnection As Boolean) As DataTable
            Try
                ''check if it is Group Distributor
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT TOP 1 AH.IDApp,AH.TypeApp,AH.LEFT_QTY FROM ADJUSTMENT_DPD AH INNER JOIN ADJUSTMENT_DPD_DIST AD ON AH.IDApp = AD.FKApp " & vbCrLf & _
                        " WHERE AH.BRANDPACK_ID = @BRANDPACK_ID AND AD.DISTRIBUTOR_ID = " & vbCrLf & _
                        " @DISTRIBUTOR_ID AND AH.START_DATE <= @PO_DATE AND AH.LEFT_QTY > 0 AND AH.IsGroup = 1 ORDER BY AH.END_DATE DESC "
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID)
                Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PODate)
                Me.OpenConnection()
                'jika ada di group yang bukan group abaikan saja
                Dim dt As New DataTable("T_Adj")
                setDataAdapter(Me.SqlCom).Fill(dt)
                If dt.Rows.Count <= 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT TOP 1 IDApp,TypeApp,LEFT_QTY FROM ADJUSTMENT_DPD WHERE BRANDPACK_ID = @BRANDPACK_ID AND DISTRIBUTOR_ID = " & vbCrLf & _
                            " @DISTRIBUTOR_ID AND START_DATE <= @PO_DATE AND END_DATE >= @PO_DATE AND LEFT_QTY > 0 AND IsGroup = 0 ORDER BY END_DATE DESC ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    setDataAdapter(Me.SqlCom).Fill(dt)
                    If dt.Rows.Count > 0 Then
                        If mustCloseConnection Then : Me.CloseConnection() : End If : Me.ClearCommandParameters() : Return dt
                    End If
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                           "SELECT TOP 1 IDApp,TypeApp,LEFT_QTY FROM ADJUSTMENT_DPD WHERE BRANDPACK_ID = @BRANDPACK_ID AND DISTRIBUTOR_ID = " & vbCrLf & _
                           " @DISTRIBUTOR_ID AND START_DATE <= @PO_DATE AND LEFT_QTY > 0 AND IsGroup = 0 ORDER BY END_DATE DESC ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    setDataAdapter(Me.SqlCom).Fill(dt)
                    If dt.Rows.Count > 0 Then
                        If mustCloseConnection Then : Me.CloseConnection() : End If : Me.ClearCommandParameters() : Return dt
                    End If

                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT TOP 1 IDApp,TypeApp,LEFT_QTY FROM ADJUSTMENT_DPD WHERE BRANDPACK_ID = @BRANDPACK_ID AND DISTRIBUTOR_ID = " & vbCrLf & _
                            " @DISTRIBUTOR_ID AND LEFT_QTY > 0 ORDER BY END_DATE DESC ;"
                    dt.Clear()
                    setDataAdapter(Me.SqlCom).Fill(dt)
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If : Me.ClearCommandParameters() : Return dt
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

#End Region

#End Region

#Region " SUB / Void "

#Region " OA_BRANDPACK DISCOUNT "

        Public Function hasExistedInSPPBBrandPack(ByVal OA_BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean) As Boolean

            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                " SELECT TOP 1 SPPB_BRANDPACK_ID FROM SPPB_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID ;"
                Me.CreateCommandSql("", Query)
                AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If mustCloseConnection Then
                    Me.CloseConnection()
                End If
                If ((Not IsNothing(retval)) And (Not IsDBNull(retval))) Then
                    Return (retval.ToString <> "")
                End If
                Return False
            Catch ex As Exception
                Me.ClearCommandParameters() : Me.CloseConnection() : Throw ex
            End Try
        End Function

        Public Sub getTargetDiscCPD(ByVal PROG_BRANDPACK_DIST_ID As String, ByVal Is_T_TM_Dist As Boolean, ByRef Target As Decimal, ByRef GIvenDisc As Decimal, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON " & vbCrLf & _
                        "SELECT GIVEN_CP,TARGET_CP FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE ISCP = 1 AND ISSCPD = 0 AND IS_T_TM_DIST = @IS_T_TM_DIST;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@IS_T_TM_DIST", SqlDbType.Bit, Is_T_TM_Dist)
                Me.OpenConnection() : Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    Target = SqlRe.GetDecimal(1)
                    GIvenDisc = SqlRe.GetDecimal(0)
                End While : Me.SqlRe.Close()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        Public Sub Update_OABRANDPACK_DISC(ByVal OA_BRANDPACK_DISC_ID As Integer, _
        ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal QTY As Decimal, ByVal FLAG As String, Optional ByVal OA_RM_ID As String = "", _
        Optional ByVal AGREE_DISC_HIST_ID As String = "", Optional ByVal BRND_B_S_ID As String = "", Optional ByVal ACHIEVEMENT_BRANDPACK_ID As String = "", _
        Optional ByVal MRKT_DISC_HIST_ID As String = "", Optional ByVal MRKT_M_S_ID As String = "", _
        Optional ByVal PROJ_DISC_HIST_ID As String = "", Optional ByVal IsOtherDiscount As Boolean = False)
            Try

                'Dim Divided_QTy As Decimal = Me.GetDevided_QTY(BRANDPACK_ID, False)
                'UPDATE OA_BRANDPACK DISC
                Dim Query As String = "SET NOCOUNT ON;" & vbCrLf & _
                                      " DECLARE @V_FKCodeAdjust INT, @V_DISC_QTY DECIMAL(18,3) ;" & vbCrLf & _
                                      " SET @V_FKCodeAdjust = (SELECT TOP 1 FKApp FROM ADJUSTMENT_TRANS WHERE OA_BRANDPACK_DISC_ID = @OA_BRANDPACK_DISC_ID) ;" & vbCrLf & _
                                      " SET @V_DISC_QTY = (SELECT DISC_QTY FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_DISC_ID = @OA_BRANDPACK_DISC_ID);  " & vbCrLf & _
                                      " UPDATE ORDR_OA_BRANDPACK_DISC SET DISC_QTY = (@V_DISC_QTY - @QTY), MODIFY_BY = @MODIFY_BY, MODIFY_DATE = GETDATE() WHERE OA_BRANDPACK_DISC_ID = @OA_BRANDPACK_DISC_ID ;" & vbCrLf & _
                                      " SELECT FKCodeAdjust = @V_FKCodeAdjust ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@QTY", SqlDbType.Decimal, QTY)
                Me.AddParameter("@OA_BRANDPACK_DISC_ID", SqlDbType.Int, OA_BRANDPACK_DISC_ID)
                Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Dim FkCodeAdjust As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                'Me.SqlCom.CommandType = CommandType.StoredProcedure
                If OA_RM_ID <> "" Then
                    'JADIKAN QTY REMAINDING
                    'Dim QtyRemainding As Decimal = Decimal.Round(QTY * Divided_QTy, 3)
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " UPDATE ORDR_OA_REMAINDING SET LEFT_QTY = CAST((" _
                            & "SELECT LEFT_QTY FROM ORDR_OA_REMAINDING WHERE OA_RM_ID = '" & OA_RM_ID & "') AS DECIMAL(10,3)) + @QTY " _
                            & ",RELEASE_QTY = CAST((SELECT RELEASE_QTY FROM ORDR_OA_REMAINDING WHERE OA_RM_ID = '" & _
                                OA_RM_ID & "') AS DECIMAL(10,3)) - @QTY,MODIFY_BY = '" & NufarmBussinesRules.User.UserLogin.UserName & "',MODIFY_DATE = GETDATE() WHERE OA_RM_ID = '" & OA_RM_ID & "'"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@QTY", SqlDbType.Decimal, QTY) : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Select Case FLAG
                        Case "G", "Q1", "Q2", "Q3", "Q4", "S1", "S2", "Y"
                            'RESET COMMAND TEXT
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                            Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                            Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Agreement_Discount", 20) ' VARCHAR(20)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            ''DELETE ADJUSTMENT_TRANS
                            If Not IsNothing(FkCodeAdjust) And Not IsDBNull(FkCodeAdjust) Then
                                Query = "SET NOCOUNT ON; " & vbCrLf & _
                                        "UPDATE ADJUSTMENT_TRANS SET ADJ_DISC_QTY = ADJ_DISC_QTY - @QTY,ModifiedBy = @ModifiedBy," & vbCrLf & _
                                        " ModifiedDate = GETDATE() WHERE OA_BRANDPACK_DISC_ID = @OA_BRANDPACK_DISC_ID ;"
                                Me.ResetCommandText(CommandType.Text, Query)
                                Me.AddParameter("@OA_BRANDPACK_DISC_ID", SqlDbType.Int, OA_BRANDPACK_DISC_ID)
                                Me.AddParameter("@QTY", SqlDbType.Decimal, QTY)
                                Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)
                                Me.SqlCom.ExecuteScalar()

                                Query = " SET NOCOUNT ON; " & vbCrLf & _
                                        " DECLARE @V_LEFT_QTY DECIMAL(18,3),@V_DISC_QTY DECIMAL(18,3),@SUM_RELEASE_QTY DECIMAL(18,3),@V_ADJUST_QTY DECIMAL(18,3) ;" & vbCrLf & _
                                        " SET @V_DISC_QTY = (SELECT ISNULL(QTY,0) FROM ADJUSTMENT_DPD WHERE IDApp = @FKApp ); " & vbCrLf & _
                                        " SET @SUM_RELEASE_QTY = (SELECT ISNULL(SUM(ADJ_DISC_QTY),0) FROM ADJUSTMENT_TRANS WHERE FKApp = @FKApp ) + (SELECT ISNULL(SUM(LEFT_QTY),0) FROM ORDR_OA_REMAINDING WHERE FkCodeAdjust = @FKApp); " & vbCrLf & _
                                        " UPDATE ADJUSTMENT_DPD SET RELEASE_QTY = @SUM_RELEASE_QTY,LEFT_QTY = (@V_DISC_QTY - @SUM_RELEASE_QTY),ModifiedBy  = @ModifiedBy, ModifiedDate = GETDATE() WHERE IDApp = @FKApp ;"
                                Me.ResetCommandText(CommandType.Text, Query)
                                Me.AddParameter("@FKApp", SqlDbType.Int, FkCodeAdjust)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            End If
                        Case "MG", "KP", "CP", "CR", "DK", "MS", "MT", "CS", "TS", "TD", "CD", "CT", "DN", "CA"
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                            Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                            Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Sales_Discount", 20) ' VARCHAR(20)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Case "O", "OCBD", "ODD", "ODR", "ODK"
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                            Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                            Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Other_Discount", 20) ' VARCHAR(20)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Case "P"
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                            Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                            Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Project_Discount", 20) ' VARCHAR(20)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    End Select
                    
                ElseIf AGREE_DISC_HIST_ID <> "" Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Disc_Qty_AGREE_DISC_HIST_ID")
                    Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, AGREE_DISC_HIST_ID, 115)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    'RESET COMMAND TEXT
                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                    Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Agreement_Discount", 20) ' VARCHAR(20)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                ElseIf ACHIEVEMENT_BRANDPACK_ID <> "" Then
                    If Not IsNothing(FkCodeAdjust) And Not IsDBNull(FkCodeAdjust) Then
                        'UPDATE Adjustment Trans'
                        'UPDATE ADJUSTMENT_DPD (dikurangi QTY)
                        Query = " SET NOCOUNT ON; " & vbCrLf & _
                                " UPDATE ADJUSTMENT_TRANS SET ADJ_DISC_QTY = ADJ_DISC_QTY - @QTY ,ModifiedBy  = @ModifiedBy, ModifiedDate = GETDATE() WHERE OA_BRANDPACK_DISC_ID = @OA_BRANDPACK_DISC_ID ; "
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@OA_BRANDPACK_DISC_ID", SqlDbType.Int, OA_BRANDPACK_DISC_ID)
                        Me.AddParameter("@QTY", SqlDbType.Decimal, QTY)
                        Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                        Me.SqlCom.ExecuteScalar()

                        Query = " SET NOCOUNT ON; " & vbCrLf & _
                                " DECLARE @V_LEFT_QTY DECIMAL(18,3),@V_DISC_QTY DECIMAL(18,3),@SUM_RELEASE_QTY DECIMAL(18,3),@V_ADJUST_QTY DECIMAL(18,3) ;" & vbCrLf & _
                                " SET @V_DISC_QTY = (SELECT ISNULL(QTY,0) FROM ADJUSTMENT_DPD WHERE IDApp = @FKApp ); " & vbCrLf & _
                                " SET @SUM_RELEASE_QTY = (SELECT ISNULL(SUM(ADJ_DISC_QTY),0) FROM ADJUSTMENT_TRANS WHERE FKApp = @FKApp ) + (SELECT ISNULL(SUM(LEFT_QTY),0) FROM ORDR_OA_REMAINDING WHERE FkCodeAdjust = @FKApp); " & vbCrLf & _
                                " UPDATE ADJUSTMENT_DPD SET RELEASE_QTY = @SUM_RELEASE_QTY,LEFT_QTY = (@V_DISC_QTY - @SUM_RELEASE_QTY),ModifiedBy  = @ModifiedBy, ModifiedDate = GETDATE() WHERE IDApp = @FKApp ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@FKApp", SqlDbType.Int, FkCodeAdjust)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    End If

                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Matching_Disc_Qty_Agreement_Saving")
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                    Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_B_S_ID, 60)
                    Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, ACHIEVEMENT_BRANDPACK_ID, 70) ' VARCHAR(45),
                    Me.AddParameter("@DISC_FROM", SqlDbType.VarChar, NufarmBussinesRules.SharedClass.DISC_AGREE_FROM, 20)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                    Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Agreement_Discount", 20) ' VARCHAR(20)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                ElseIf MRKT_DISC_HIST_ID <> "" Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Disc_Qty_MRKT_DISC_HISTORY")
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                    Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Sales_Discount", 20) ' VARCHAR(20)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                ElseIf MRKT_M_S_ID <> "" Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Matching_Disc_Qty_MRKT_M_S_ID")
                    Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, MRKT_M_S_ID, 60)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                    Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Sales_Discount", 20) ' VARCHAR(20)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                ElseIf PROJ_DISC_HIST_ID <> "" Then
                    'Me.SqlCom.CommandType = CommandType.Text
                    Query = "SET NOCOUNT ON;UPDATE PROJ_DISC_HISTORY SET PROJ_LEFT_QTY = CAST((SELECT PROJ_LEFT_QTY FROM PROJ_DISC_HISTORY" & _
                           " WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "') AS DECIMAL(10,3)) + @QTY,PROJ_RELEASE_QTY =" & _
                           " CAST((SELECT PROJ_RELEASE_QTY FROM PROJ_DISC_HISTORY WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "') AS DECIMAL(10,3)) - @QTY " & _
                           ",MODIFY_BY = '" & NufarmBussinesRules.User.UserLogin.UserName & "',MODIFY_DATE = GETDATE() " & _
                           " WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "'"
                    Me.ResetCommandText(CommandType.Text, Query) : Me.AddParameter("@QTY", SqlDbType.Decimal, QTY)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                    Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Project_Discount", 20) ' VARCHAR(20)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                ElseIf IsOtherDiscount = True Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                    Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Other_Discount", 20) ' VARCHAR(20)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Public Sub DeleteAGREE_DISC_HISTORY(ByVal AGREE_DISC_HIST_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal AGREE_DISC_QTY As Integer)
            Try
                Me.CreateCommandSql("Sp_Delete_AGREE_DISC_HISTORY", "")
                Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, AGREE_DISC_HIST_ID, 115)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
            Catch ex As DBConcurrencyException
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw New System.Exception(Me.MessageDBConcurency)
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub DeleteMRKT_DIST_HISTORY(ByVal MRKT_DISC_HIST_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal MRKT_DISC_QTY As Integer, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " DELETE FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = @MRKT_DISC_HIST_ID ;"
                If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                Else : Me.CreateCommandSql("", Query)
                End If
                'Me.CreateCommandSql("Sp_Delete_MRKT_DISC_HISTORY", "")
                Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(44)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
            Catch EX As DBConcurrencyException
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

        Public Sub DeletePROJ_DISC_HISTORY(ByVal PROJ_DISC_HIST_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal PROJ_DISC_QTY As Integer)
            Try
                Me.CreateCommandSql("Sp_Delete_PROJ_DISC_HISTORY", "")
                Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, PROJ_DISC_HIST_ID, 66) ' VARCHAR(66)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()
                Me.CommiteTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
            Catch ex As DBConcurrencyException
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw New System.Exception(Me.MessageDBConcurency)
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub DeleteOA_BRANDPACK_DISC(ByVal OA_BRANDPACK_DISC_ID As Integer, ByVal SDiscount As SelectedDiscount, _
        ByVal OA_BRANDPACK_DISC_QTY As Decimal, Optional ByVal AGREE_DISC_HIST_ID As String = "", Optional ByVal MRKT_DISC_HIST_ID As String = "" _
        , Optional ByVal PROJ_DISC_HIST_ID As String = "", Optional ByVal IsOtherDiscount As Boolean = False, Optional ByVal OA_BRANDPACK_ID As String = "", _
        Optional ByVal BRND_B_S_ID As String = "", Optional ByVal ACHIEVEMENT_BRANDPACK_ID As String = "", Optional ByVal MRKT_M_S_ID As String = "", Optional ByVal OA_RM_ID As String = "")
            Try
                ''ambil discQty nya 
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT TOP 1 DISC_QTY FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_DISC_ID = @OA_BRANDPACK_DISC_ID; "
                If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                Else : Me.CreateCommandSql("", Query)
                End If
                Me.AddParameter("@OA_BRANDPACK_DISC_ID", SqlDbType.Int, OA_BRANDPACK_DISC_ID)
                Me.OpenConnection() : Dim oDiscQty As Object = Me.SqlCom.ExecuteScalar()
                ''query delete

                Query = "SET NOCOUNT ON; DECLARE @V_FKCodeAdjust INT ;" & vbCrLf & _
                " SET @V_FKCOdeAdjust = (SELECT TOP 1 FKApp FROM ADJUSTMENT_TRANS WHERE OA_BRANDPACK_DISC_ID = @OA_BRANDPACK_DISC_ID);" & vbCrLf & _
                " DELETE FROM ORDR_OA_BRANDPACK_DISC WHERE OA_BRANDPACK_DISC_ID = @OA_BRANDPACK_DISC_ID ;" & vbCrLf & _
                " SELECT FKCodeAdjust = @V_FKCodeAdjust ;"
                'Me.CreateCommandSql("Sp_Delete_ORDR_OA_BRANDPACK_DISC", "")
                Me.ResetCommandText(CommandType.Text, Query)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Dim FKCodeAdjust As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
               
                Select Case SDiscount
                    Case SelectedDiscount.AgreementDiscount
                        'RESET COMMAND TEXT
                        If AGREE_DISC_HIST_ID <> "" Then
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Disc_Qty_AGREE_DISC_HIST_ID")
                            Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, AGREE_DISC_HIST_ID, 115)
                        ElseIf ACHIEVEMENT_BRANDPACK_ID <> "" Then
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Matching_Disc_Qty_Agreement_Saving")
                            Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_B_S_ID, 60)
                            Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, ACHIEVEMENT_BRANDPACK_ID, 70) ' VARCHAR(45),
                            Me.AddParameter("@DISC_FROM", SqlDbType.VarChar, NufarmBussinesRules.SharedClass.DISC_AGREE_FROM, 20) ' VARCHAR(20)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                            If Not IsNothing(FKCodeAdjust) And Not IsDBNull(FKCodeAdjust) Then
                                'UPDATE Adjustment Trans'
                                Query = "SET NOCOUNT ON;" & vbCrLf & _
                                         " UPDATE ADJUSTMENT_TRANS SET ADJ_DISC_QTY = 0,ModifiedBy  = @ModifiedBy, ModifiedDate = GETDATE() WHERE OA_BRANDPACK_DISC_ID = @OA_BRANDPACK_DISC_ID ; "
                                Me.ResetCommandText(CommandType.Text, Query)
                                Me.AddParameter("@OA_BRANDPACK_DISC_ID", SqlDbType.Int, OA_BRANDPACK_DISC_ID)
                                Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                                Me.SqlCom.ExecuteScalar()

                                'UPDATE ADJUSTMENT_DPD
                                Query = "SET NOCOUNT ON; " & vbCrLf & _
                                       " DECLARE @V_LEFT_QTY DECIMAL(18,3),@V_DISC_QTY DECIMAL(18,3),@SUM_RELEASE_QTY DECIMAL(18,3) ;" & vbCrLf & _
                                       " SET @V_DISC_QTY = (SELECT ISNULL(QTY,0) FROM ADJUSTMENT_DPD WHERE IDApp = @FKApp ); " & vbCrLf & _
                                       " SET @SUM_RELEASE_QTY = (SELECT ISNULL(SUM(ADJ_DISC_QTY),0) FROM ADJUSTMENT_TRANS WHERE FKApp = @FKApp) + (SELECT ISNULL(SUM(LEFT_QTY),0) FROM ORDR_OA_REMAINDING WHERE FkCodeAdjust = @FKApp); " & vbCrLf & _
                                       " UPDATE ADJUSTMENT_DPD SET RELEASE_QTY = @SUM_RELEASE_QTY,LEFT_QTY = (@V_DISC_QTY - @SUM_RELEASE_QTY), ModifiedBy  = @ModifiedBy, ModifiedDate = GETDATE() WHERE IDApp = @FKApp ;"
                                Me.ResetCommandText(CommandType.Text, Query)
                                Me.AddParameter("@FKApp", SqlDbType.Int, FKCodeAdjust)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            End If
                        End If


                        Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                        Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Agreement_Discount", 20) ' VARCHAR(20)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Case SelectedDiscount.MarketingDiscount
                        'RESET COMMAND TEXT
                        If MRKT_DISC_HIST_ID <> "" Then
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Disc_Qty_MRKT_DISC_HISTORY")
                            Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                        ElseIf MRKT_M_S_ID <> "" Then
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Matching_Disc_Qty_MRKT_M_S_ID")
                            Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, MRKT_M_S_ID, 60)
                        End If
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                        Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                        Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Sales_Discount", 20) ' VARCHAR(20)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Case SelectedDiscount.OtherDiscount
                        'RESET COMMAND TEXT
                        If IsOtherDiscount = True Then
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                            Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                            Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Other_Discount", 20) ' VARCHAR(20)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Dim Query As String = "SET NOCOUNT ON; DELETE FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID AND FLAG = @Flag ;"
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.NVarChar, OA_BRANDPACK_ID, 75)
                            Me.AddParameter("@Flag", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG, 5)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        End If
                    Case SelectedDiscount.ProjectDiscount
                        'RESET COMMAND TEXT
                        Dim Query As String = "SET NOCOUNT ON;UPDATE PROJ_DISC_HISTORY SET PROJ_LEFT_QTY = CAST((SELECT PROJ_LEFT_QTY FROM PROJ_DISC_HISTORY" & _
                        " WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "') AS DECIMAL(10,3)) + @QTY,PROJ_RELEASE_QTY = " & _
                        " CAST((SELECT PROJ_RELEASE_QTY FROM PROJ_DISC_HISTORY WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "') AS DECIMAL(10,3)) - " & _
                        " @QTY,MODIFY_BY = '" & NufarmBussinesRules.User.UserLogin.UserName & "',MODIFY_DATE = GETDATE() " & _
                        " WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "'"
                        Me.ResetCommandText(CommandType.Text, Query) : Me.AddParameter("@QTY", SqlDbType.Decimal, OA_BRANDPACK_DISC_QTY)
                        Me.SqlCom.CommandType = CommandType.Text
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                        Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                        Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Project_Discount", 20) ' VARCHAR(20)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Case SelectedDiscount.None

                        Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathing_Qty_ORDDR_OA_REMAINDING")
                        Me.AddParameter("@OA_RM_ID", SqlDbType.VarChar, OA_RM_ID, 150)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                        Select Case Me.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG
                            Case "G", "Q1", "Q2", "Q3", "Q4", "S1", "S2", "Y"
                                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Agreement_Discount", 20) ' VARCHAR(20)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                                If Not IsNothing(FKCodeAdjust) And Not IsDBNull(FKCodeAdjust) Then
                                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                                             "UPDATE ADJUSTMENT_TRANS SET ADJ_DISC_QTY = ADJ_DISC_QTY - @QTY,ModifiedBy = @ModifiedBy," & vbCrLf & _
                                             " ModifiedDate = GETDATE() WHERE OA_BRANDPACK_DISC_ID = @OA_BRANDPACK_DISC_ID ;"
                                    Me.ResetCommandText(CommandType.Text, Query)
                                    Me.AddParameter("@OA_BRANDPACK_DISC_ID", SqlDbType.Int, OA_BRANDPACK_DISC_ID)
                                    Me.AddParameter("@QTY", SqlDbType.Decimal, Convert.ToDecimal(oDiscQty))
                                    Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)
                                    Me.SqlCom.ExecuteScalar()

                                    'UPDATE ADJUSTMENT_DPD
                                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                                           " DECLARE @V_LEFT_QTY DECIMAL(18,3),@V_DISC_QTY DECIMAL(18,3),@SUM_RELEASE_QTY DECIMAL(18,3) ;" & vbCrLf & _
                                           " SET @V_DISC_QTY = (SELECT ISNULL(QTY,0) FROM ADJUSTMENT_DPD WHERE IDApp = @FKApp ); " & vbCrLf & _
                                           " SET @SUM_RELEASE_QTY = (SELECT ISNULL(SUM(ADJ_DISC_QTY),0) FROM ADJUSTMENT_TRANS WHERE FKApp = @FKApp) + (SELECT ISNULL(SUM(LEFT_QTY),0) FROM ORDR_OA_REMAINDING WHERE FKCodeAdjust = @FKApp); " & vbCrLf & _
                                           " UPDATE ADJUSTMENT_DPD SET RELEASE_QTY = @SUM_RELEASE_QTY,LEFT_QTY = (@V_DISC_QTY - @SUM_RELEASE_QTY), ModifiedBy  = @ModifiedBy, ModifiedDate = GETDATE() WHERE IDApp = @FKApp ;"
                                    Me.ResetCommandText(CommandType.Text, Query)
                                    Me.AddParameter("@FKApp", SqlDbType.Int, FKCodeAdjust)
                                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                                End If
                            Case "MG", "KP", "CP", "CR", "DK", "MT", "MS", "CS", "CT", "CD", "TD", "TS", "DN", "CA"
                                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Sales_Discount", 20) ' VARCHAR(20)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Case "P"
                                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Project_Discount", 20) ' VARCHAR(20)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Case "O", "OCBD", "ODD", "ODR"
                                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Other_Discount", 20) ' VARCHAR(20)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        End Select
                End Select
                Me.CommiteTransaction()
            Catch es As DBConcurrencyException
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw New System.Exception(Me.MessageDBConcurency)
                'me.CreateCommandSql("
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub
        'SUB PROCEDURE UNTUK MENGINSERT OA_BRANDPACK_DISC DENGAN OTHER_QTY
        Public Sub InsertOA_BRANDPACK_DISC(ByVal DecResultQTY As Decimal, ByVal DecLeftQTY As Decimal, ByVal OA_BRANDPACK_ID As String, ByVal mustCloseConnection As Boolean)
            Try
                If Me.OA_Remainding.RefOther > 0 Then ''DISCOUNT DD DR
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT 1 WHERE EXISTS(SELECT IDApp FROM BRND_DISC_HEADER WHERE IDApp = @RefOther)"
                    If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                    Else : Me.CreateCommandSql("", Query)
                    End If
                    Me.AddParameter("@RefOther", SqlDbType.Int, Me.OA_Remainding.RefOther)
                    Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If IsNothing(retval) Or IsDBNull(retval) Then
                        Me.CloseConnection()
                        Throw New Exception("Reference program DR/DD/CBD can not be found" & vbCrLf & _
                        "Perhaps has been deleted by User (SAE)")
                    End If
                End If

                'INSERT DULU KE OA_BRANDPACK_DISC
                If (DecResultQTY > 0) And (DecLeftQTY > 0) Then
                    ''Me.CreateCommandSql("Usp_Insert_ORDR_OA_BRANDPACK_DISCOUNT", "")
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                      " INSERT INTO ORDR_OA_BRANDPACK_DISC(OA_BRANDPACK_ID,GQSY_SGT_P_FLAG,DISC_QTY," & vbCrLf & _
                      " PRICE_PRQTY,AGREE_DISC_HIST_ID,MRKT_DISC_HIST_ID,PROJ_DISC_HIST_ID,BRND_B_S_ID,ACHIEVEMENT_BRANDPACK_ID,MRKT_M_S_ID,OA_RM_ID,RefOther, " & vbCrLf & _
                      " CREATE_BY,CREATE_DATE) " & vbCrLf & _
                      " VALUES(@OA_BRANDPACK_ID,@GQSY_SGT_P_FLAG,@DISC_QTY,@PRICE_PRQTY,@AGREE_DISC_HIST_ID," & vbCrLf & _
                      " @MRKT_DISC_HIST_ID,@PROJ_DISC_HIST_ID,@BRND_B_S_ID,@ACHIEVEMENT_BRANDPACK_ID,@MRKT_M_S_ID,@OA_RM_ID,@RefOther,@CREATE_BY,GETDATE()) ;"
                    If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                    Else : Me.CreateCommandSql("", Query)
                    End If

                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_Remainding.OA_BRANDPACK_ID, 75) '(44),
                    Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, Me.OA_Remainding.FLAG, 5) ' VARCHAR(5),
                    Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DecResultQTY) ' INT,
                    Me.AddParameter("@PRICE_PRQTY", SqlDbType.Decimal, Me.OA_Remainding.PRICE_PRQTY) '  VARCHAR(10),
                    Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_Remainding.AGREE_DISC_HIST_ID, 115) ' VARCHAR(71),
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_Remainding.MRKT_DISC_HISt_ID, 115) ' VARCHAR(66),
                    Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_Remainding.PROJ_DISC_HIST_ID, 66) ' VARCHAR(66),
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName) ' VARCHAR(30)
                    Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, Me.OA_Remainding.BRND_B_S_ID, 60)
                    Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, Me.OA_Remainding.MRKT_M_S_ID, 60) ' VARCHAR(60),
                    Me.AddParameter("@OA_RM_ID", SqlDbType.VarChar, Me.OA_Remainding.RM_OA_ID, 150)
                    Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_Remainding.ACHIEVMENT_BRANDPACK_ID, 70)
                    Me.AddParameter("@RefOther", SqlDbType.Int, Me.OA_Remainding.RefOther)
                    Me.OpenConnection() : Me.BeginTransaction()
                    Me.SqlCom.Transaction = Me.SqlTrans : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Me.SqlCom.CommandText = "Usp_Check_Sum_Mathching_Disc_Qty"
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                    Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Other_Discount", 20) ' VARCHAR(20)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    'INSERT OA_REMAINDING
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "INSERT INTO ORDR_OA_REMAINDING(OA_BRANDPACK_ID,AGREE_DISC_HIST_ID,BRND_B_S_ID,ACHIEVEMENT_BRANDPACK_ID,MRKT_DISC_HIST_ID," & vbCrLf & _
                            "MRKT_M_S_ID,PROJ_DISC_HIST_ID,FLAG,QTY,RELEASE_QTY,LEFT_QTY,RefOther,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                            "VALUES(@OA_BRANDPACK_ID,@AGREE_DISC_HIST_ID,@BRND_B_S_ID,@ACHIEVEMENT_BRANDPACK_ID,@MRKT_DISC_HIST_ID,@MRKT_M_S_ID," & vbCrLf & _
                            "@PROJ_DISC_HIST_ID,@FLAG,@QTY,@RELEASE_QTY,@LEFT_QTY,@RefOther,@CREATE_BY,GETDATE())"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 115) ' VARCHAR(110),
                    Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, DBNull.Value, 60) ' VARCHAR(60),
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 115) ' VARCHAR(110),
                    Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, DBNull.Value, 60) ' VARCHAR(60),
                    Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 66) ' VARCHAR(66),
                    Me.AddParameter("@FLAG", SqlDbType.VarChar, Me.OA_Remainding.FLAG, 5) ' VARCHAR(5),
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                    Me.AddParameter("@QTY", SqlDbType.Decimal, DecLeftQTY) ' VARCHAR(13),
                    Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' CObj("0"), 13)
                    Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DecLeftQTY)
                    Me.AddParameter("@RefOther", SqlDbType.Int, Me.OA_Remainding.RefOther)
                    Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, DBNull.Value, 70)
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                ElseIf (DecResultQTY > 0) And DecLeftQTY = 0 Then
                    'Me.CreateCommandSql("Usp_Insert_ORDR_OA_BRANDPACK_DISCOUNT", "")
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " INSERT INTO ORDR_OA_BRANDPACK_DISC(OA_BRANDPACK_ID,GQSY_SGT_P_FLAG,DISC_QTY," & vbCrLf & _
                            " PRICE_PRQTY,AGREE_DISC_HIST_ID,MRKT_DISC_HIST_ID,PROJ_DISC_HIST_ID,BRND_B_S_ID,ACHIEVEMENT_BRANDPACK_ID,MRKT_M_S_ID,OA_RM_ID,RefOther, " & vbCrLf & _
                            " CREATE_BY,CREATE_DATE) " & vbCrLf & _
                            " VALUES(@OA_BRANDPACK_ID,@GQSY_SGT_P_FLAG,@DISC_QTY,@PRICE_PRQTY,@AGREE_DISC_HIST_ID," & vbCrLf & _
                            " @MRKT_DISC_HIST_ID,@PROJ_DISC_HIST_ID,@BRND_B_S_ID,@ACHIEVEMENT_BRANDPACK_ID,@MRKT_M_S_ID,@OA_RM_ID,@RefOther,@CREATE_BY,GETDATE()) ;"
                    If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                    Else : Me.CreateCommandSql("", Query)
                    End If
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_Remainding.OA_BRANDPACK_ID, 75) '(44),
                    Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, Me.OA_Remainding.FLAG, 5) ' VARCHAR(5),
                    Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, DecResultQTY) ' INT,
                    Me.AddParameter("@PRICE_PRQTY", SqlDbType.Decimal, Me.OA_Remainding.PRICE_PRQTY) '  VARCHAR(10),
                    Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_Remainding.AGREE_DISC_HIST_ID, 115) ' VARCHAR(71),
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_Remainding.MRKT_DISC_HISt_ID, 115) ' VARCHAR(66),
                    Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_Remainding.PROJ_DISC_HIST_ID, 66) ' VARCHAR(66),
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName) ' VARCHAR(30)
                    Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, Me.OA_Remainding.BRND_B_S_ID, 60)
                    Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, Me.OA_Remainding.MRKT_M_S_ID, 60) ' VARCHAR(60),
                    Me.AddParameter("@OA_RM_ID", SqlDbType.VarChar, Me.OA_Remainding.RM_OA_ID, 200)
                    Me.AddParameter("@RefOther", SqlDbType.Int, Me.OA_Remainding.RefOther)
                    Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_Remainding.ACHIEVMENT_BRANDPACK_ID, 70)
                    Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                    Me.SqlCom.CommandText = "Usp_Check_Sum_Mathching_Disc_Qty"
                    Me.SqlCom.CommandType = CommandType.StoredProcedure
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                    Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Other_Discount", 20) ' VARCHAR(20)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                ElseIf (DecResultQTY = 0) And (DecLeftQTY > 0) Then
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            "INSERT INTO ORDR_OA_REMAINDING(OA_BRANDPACK_ID,AGREE_DISC_HIST_ID,BRND_B_S_ID,ACHIEVEMENT_BRANDPACK_ID,MRKT_DISC_HIST_ID," & vbCrLf & _
                            "MRKT_M_S_ID,PROJ_DISC_HIST_ID,FLAG,QTY,RELEASE_QTY,LEFT_QTY,RefOther,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                            "VALUES(@OA_BRANDPACK_ID,@AGREE_DISC_HIST_ID,@BRND_B_S_ID,@ACHIEVEMENT_BRANDPACK_ID,@MRKT_DISC_HIST_ID,@MRKT_M_S_ID," & vbCrLf & _
                            "@PROJ_DISC_HIST_ID,@FLAG,@QTY,@RELEASE_QTY,@LEFT_QTY,@RefOther,@CREATE_BY,GETDATE())"
                    If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                    Else : Me.CreateCommandSql("", Query)
                    End If
                    Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 115) ' VARCHAR(110),
                    Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, DBNull.Value, 60) ' VARCHAR(60),
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 115) ' VARCHAR(110),
                    Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, DBNull.Value, 60) ' VARCHAR(60),
                    Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, DBNull.Value, 66) ' VARCHAR(66),
                    Me.AddParameter("@FLAG", SqlDbType.VarChar, Me.OA_Remainding.FLAG, 5) ' VARCHAR(5),
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                    Me.AddParameter("@QTY", SqlDbType.Decimal, DecLeftQTY) ' VARCHAR(13),
                    Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' CObj("0"), 13)
                    Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, DecLeftQTY)
                    Me.AddParameter("@RefOther", SqlDbType.Int, Me.OA_Remainding.RefOther)
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) '
                    Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, DBNull.Value, 70)
                    Me.OpenConnection() : Me.BeginTransaction() : Me.ExecuteNonQuery()
                End If

                Me.CommiteTransaction() : If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

        Public Function InsertOA_BRANDPACK_DISC(ByVal SDiscount As SelectedDiscount, ByVal OA_BRANDPACK_DISC_QTY As Decimal, Optional ByVal AGREE_DISC_HIST_ID As String = "", Optional ByVal MRKT_DISC_HIST_ID As String = "" _
        , Optional ByVal PROJ_DISC_HIST_ID As String = "", Optional ByVal IsOtherDiscount As Boolean = False, Optional ByVal OA_BRANDPACK_ID As String = "", Optional ByVal hasChildTrans As Boolean = False) As Integer
            Dim RetIdentity As Integer = 0
            Try
                Dim retval As Object = Nothing
                If Me.OA_BRANDPACK_DISCOUNT.RefOther > 0 Then ''DISCOUNT DD DR
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT 1 WHERE EXISTS(SELECT IDApp FROM BRND_DISC_HEADER WHERE IDApp = @RefOther)"
                    If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                    Else : Me.CreateCommandSql("", Query)
                    End If
                    Me.AddParameter("@RefOther", SqlDbType.Int, Me.OA_BRANDPACK_DISCOUNT.RefOther)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If IsNothing(retval) Or IsDBNull(retval) Then
                        Me.CloseConnection()
                        Throw New Exception("Reference program DR/DD/CBD can not be found" & vbCrLf & _
                        "Perhaps has been deleted by User (SAE)")
                    End If
                End If
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " INSERT INTO ORDR_OA_BRANDPACK_DISC(OA_BRANDPACK_ID,GQSY_SGT_P_FLAG,DISC_QTY," & vbCrLf & _
                        " PRICE_PRQTY,AGREE_DISC_HIST_ID,MRKT_DISC_HIST_ID,PROJ_DISC_HIST_ID,BRND_B_S_ID,ACHIEVEMENT_BRANDPACK_ID,MRKT_M_S_ID,OA_RM_ID,RefOther, " & vbCrLf & _
                        " CREATE_BY,CREATE_DATE) " & vbCrLf & _
                        " VALUES(@OA_BRANDPACK_ID,@GQSY_SGT_P_FLAG,@DISC_QTY,@PRICE_PRQTY,@AGREE_DISC_HIST_ID," & vbCrLf & _
                        " @MRKT_DISC_HIST_ID,@PROJ_DISC_HIST_ID,@BRND_B_S_ID,@ACHIEVEMENT_BRANDPACK_ID,@MRKT_M_S_ID,@OA_RM_ID,@RefOther,@CREATE_BY,GETDATE()) ;" & vbCrLf & _
                        " SELECT RetIdentity = @@Identity ;"
                If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                Else : Me.CreateCommandSql("", Query)
                End If
                'Me.CreateCommandSql("Usp_Insert_ORDR_OA_BRANDPACK_DISCOUNT", "")
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.OA_BRANDPACK_ID, 75) '(44),
                Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG, 5) ' VARCHAR(5),
                Me.AddParameter("@DISC_QTY", SqlDbType.Decimal, OA_BRANDPACK_DISC_QTY) ' INT,
                Me.AddParameter("@PRICE_PRQTY", SqlDbType.Decimal, Me.OA_BRANDPACK_DISCOUNT.PRICE_PRQTY) '  VARCHAR(10),
                Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.AGREE_DISC_HIST_ID, 115) ' VARCHAR(71),
                Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.PROJ_DISC_HIST_ID, 66) ' VARCHAR(66),
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName) ' VARCHAR(30)
                Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.BRND_B_S_ID, 60)
                Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.MRK_M_S_ID, 60) ' VARCHAR(60),
                Me.AddParameter("@OA_RM_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.OA_RM_ID, 150)
                Me.AddParameter("@RefOther", SqlDbType.Int, Me.OA_BRANDPACK_DISCOUNT.RefOther)
                Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.ACHIEVEMENT_BRANDPACK_ID, 70)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    RetIdentity = CInt(retval)
                End If
                Select Case SDiscount
                    Case SelectedDiscount.AgreementDiscount
                        'RESET COMMAND TEXT
                        If Not AGREE_DISC_HIST_ID = "" Then
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Disc_Qty_AGREE_DISC_HIST_ID")
                            Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, AGREE_DISC_HIST_ID, 115)
                        ElseIf (Not IsDBNull(Me.OA_BRANDPACK_DISCOUNT.BRND_B_S_ID)) Or (Not IsDBNull(Me.OA_BRANDPACK_DISCOUNT.ACHIEVEMENT_BRANDPACK_ID)) Then
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Matching_Disc_Qty_Agreement_Saving")
                            Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.ACHIEVEMENT_BRANDPACK_ID, 70) ' VARCHAR(45),
                            Me.AddParameter("@DISC_FROM", SqlDbType.VarChar, NufarmBussinesRules.SharedClass.DISC_AGREE_FROM, 20)
                            Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.BRND_B_S_ID, 60)
                        End If
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                        ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                        Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Agreement_Discount", 20) ' VARCHAR(20)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Case SelectedDiscount.MarketingDiscount
                        'RESET COMMAND TEXT
                        If Not IsDBNull(Me.OA_BRANDPACK_DISCOUNT.MRKT_DISC_HIST_ID) Then
                            ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Disc_Qty_MRKT_DISC_HISTORY")
                            Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                        ElseIf Not IsDBNull(Me.OA_BRANDPACK_DISCOUNT.MRK_M_S_ID) Then
                            ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Matching_Disc_Qty_MRKT_M_S_ID")
                            Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.MRK_M_S_ID, 60)
                        End If
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                        ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                        Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Sales_Discount", 20) ' VARCHAR(20)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Case SelectedDiscount.OtherDiscount
                        'RESET COMMAND TEXT
                        If IsOtherDiscount = True Then
                            ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                            Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                            Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Other_Discount", 20) ' VARCHAR(20)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        End If
                    Case SelectedDiscount.None 'BERARTI REMAINDING QTY
                        ''CHECK Apakah disc ini dari Adjustment
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                " SELECT FKCodeAdjust FROM ORDR_OA_REMAINDING WHERE OA_RM_ID = @OA_RM_ID ;"
                        ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@OA_RM_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.OA_RM_ID, 200)
                        retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Dim FKCodeAdjust As Integer = 0
                        If Not IsNothing(retval) And Not IsDBNull(retval) Then
                            FKCodeAdjust = CInt(retval)
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                   " DECLARE @V_PO_REF_NO VARCHAR(25),@V_ADJ_DISC_QTY DECIMAL(18,3);" & vbCrLf & _
                                   " SET @V_PO_REF_NO = (SELECT TOP 1 PO_REF_NO FROM ORDR_PO_BRANDPACK WHERE PO_BRANDPACK_ID = (SELECT TOP 1 PO_BRANDPACK_ID FROM ORDR_OA_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID));" & vbCrLf & _
                                   " SET @V_ADJ_DISC_QTY = (SELECT TOP 1 LEFT_QTY FROM ORDR_OA_REMAINDING WHERE OA_RM_ID = @OA_RM_ID) ; " & vbCrLf & _
                                   " INSERT INTO ADJUSTMENT_TRANS (PO_REF_NO, FKApp, OA_BRANDPACK_DISC_ID, ADJ_DISC_QTY, CreatedBy, CreatedDate) " & vbCrLf & _
                                   " VALUES(@V_PO_REF_NO, @FKApp, @OA_BRANDPACK_DISC_ID, @V_ADJ_DISC_QTY, @CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101) ;"
                            Me.AddParameter("@FKApp", SqlDbType.Int, FKCodeAdjust)
                            Me.AddParameter("@OA_BRANDPACK_DISC_ID", SqlDbType.Int, RetIdentity)
                            Me.AddParameter("@CreatedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 100)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        End If

                        ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathing_Qty_ORDDR_OA_REMAINDING")
                        Me.SqlCom.CommandType = CommandType.StoredProcedure
                        Me.AddParameter("@OA_RM_ID", SqlDbType.VarChar, Me.OA_BRANDPACK_DISCOUNT.OA_RM_ID, 200)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                        'UPDATE OA_BRANDPACK_NYA
                        Select Case Me.OA_BRANDPACK_DISCOUNT.GQSY_SGT_P_FLAG
                            'RESET COMMAND TEXT 
                            Case "G", "Q1", "Q2", "Q3", "Q4", "S1", "S2", "Y"

                                ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Agreement_Discount", 20) ' VARCHAR(20)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                                'InsertedRows = Me.OA_Remainding.tblAdjustment.Select("ABID = ''", "", DataViewRowState.Added)
                                'If InsertedRows.Length > 0 Then
                                '    Me.SqlCom.Parameters.Add("@PO_REF_NO", SqlDbType.VarChar, 25, "PO_REF_NO")
                                '    Me.SqlCom.Parameters.Add("@FKApp", SqlDbType.Int, 0, "FKCodeAdjust")
                                '    Me.SqlCom.Parameters.Add("@OA_BRANDPACK_DISC_ID", SqlDbType.Int).Value = PrimaryIdentity
                                '    Me.SqlCom.Parameters.Add("@ADJ_DISC_QTY", SqlDbType.Decimal, 0, "ReleaseQty")
                                '    Me.SqlCom.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100).Value = NufarmBussinesRules.User.UserLogin.UserName
                                'End If
                                'Me.SqlDat.InsertCommand = Me.SqlCom
                                'Me.SqlDat.Update(InsertedRows) : Me.ClearCommandParameters()
                                'Me.SqlDat.InsertCommand = Nothing

                            Case "MG", "KP", "CP", "CR", "DK", "MT", "MS", "TD", "TS", "CS", "CD", "CT", "DN", "CA"
                                ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Sales_Discount", 20) ' VARCHAR(20)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Case "O", "OCBD", "ODD", "ODR"
                                ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Other_Discount", 20) ' VARCHAR(20)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            Case "P"
                                ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Project_Discount", 20) ' VARCHAR(20)
                                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        End Select
                    Case SelectedDiscount.ProjectDiscount
                        'RESET COMMAND TEXT
                        Dim Query As String = "SET NOCOUNT ON;UPDATE PROJ_DISC_HISTORY SET PROJ_LEFT_QTY = CAST((SELECT PROJ_LEFT_QTY FROM PROJ_DISC_HISTORY" & _
                       " WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "') AS DECIMAL(10,3)) - " & OA_BRANDPACK_DISC_QTY & ",PROJ_RELEASE_QTY =" & _
                       " CAST((SELECT PROJ_RELEASE_QTY FROM PROJ_DISC_HISTORY WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "') AS DECIMAL(10,3)) + " & _
                       OA_BRANDPACK_DISC_QTY & ",MODIFY_BY = '" & NufarmBussinesRules.User.UserLogin.UserName & "',MODIFY_DATE = GETDATE() " & _
                       " WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "'"
                        Me.SqlCom.CommandText = "sp_executesql"
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                        ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Mathching_Disc_Qty")
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                        Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Project_Discount", 20) ' VARCHAR(20)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End Select
                'update data oa_brandpack_id for disc_quantity
                If Not hasChildTrans Then : Me.CommiteTransaction() : End If
            Catch es As DBConcurrencyException
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
            Return RetIdentity
        End Function


#Region "===SUB generating sales discount==="

        Private Sub generateDiscDPRDS(ByVal tblMrkt As DataTable, ByVal i As Integer, ByVal OA_ORIGINAL_QTY As Decimal, ByRef retval As Object, ByRef message As String)
            If Not tblMrkt.Rows(i).IsNull("GIVEN_DISC_PCT") Then
                Dim GIVEN_PCT As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("GIVEN_DISC_PCT"))
                Dim MRKT_DISC_QTY As Decimal = ((GIVEN_PCT) / 100) * OA_ORIGINAL_QTY
                'NOW IS THE TIME TO INSERT INTO TBL_MRKT_DISC_HISTORY
                Me.DiscQty.MarketingGivenDisQty = MRKT_DISC_QTY
                Dim MRKT_DISC_HIST_ID As String = tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString() & "MG" & OA_BRANDPACK_ID
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "' OPTION (KEEP PLAN);"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Me.DiscQty.MarketingGivenDisQty > 0 Then
                    If IsNothing(retval) Then
                        Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_MRKT_DISC_HISTORY")
                        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40) ' VARCHAR(35),
                        Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                        Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, GIVEN_PCT) ' VARCHAR(6),
                        Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                        Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "MG", 2) ' CHAR(1),
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        message &= "Datarow " & (i + 1).ToString() + " Generated with > 0 qty" & vbCrLf
                        Me.GeneratedDiscountGiven += 1
                    End If
                End If
            End If
        End Sub

        Private Sub generatePKPPDisc(ByVal tblMrkt As DataTable, ByVal i As Integer, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal PO_DATE As DateTime, ByVal FLAG As String, ByRef retval As Object, ByRef message As String)
            If Not tblMrkt.Rows(i).IsNull("GIVEN_PKPP") Then
                Dim isTargetTMDist As Boolean = False
                Dim MRKT_DISC_HIST_ID As String = tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString() & "KP" & OA_BRANDPACK_ID
                Dim GIVEN_PCT As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("GIVEN_PKPP"))
                Dim TARGET_PKPP As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("TARGET_PKPP"))
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "' OPTION (KEEP PLAN);"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Dim hasData As Boolean = False
                If Not IsNothing(retval) Then : hasData = True : End If
                If Not hasData Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                      "SELECT TOP 1 SHIP_TO_ID FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID ; "
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If Not IsNothing(retval) And Not IsDBNull(retval) Then
                        If Not String.IsNullOrEmpty(retval.ToString()) Then
                            isTargetTMDist = True
                        End If
                    End If
                    If Not isTargetTMDist Then
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                              "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                              " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                              " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                              " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                              " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                    Else
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                 "SELECT TOP 1 SHIP_TO_ID FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID ; "
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                        retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        If IsNothing(retval) Or IsDBNull(retval) Then
                            Me.CloseConnection() : Throw New Exception("CPD with TM_Dist but Ship to TM has not been defined yet")
                        ElseIf (String.IsNullOrEmpty(retval.ToString())) Then
                            Me.CloseConnection() : Throw New Exception("CPD with TM_Dist but Ship to TM has not been defined yet")
                        End If
                        ''GET SHIP_TO_ID BY OA
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT TOP 1 SHIP_TO_ID FROM OA_SHIP_TO WHERE OA_ID = (SELECT TOP 1 OA_ID FROM ORDR_OA_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID);"
                        Me.ResetCommandText(CommandType.Text, Query)
                        AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                        retval = SqlCom.ExecuteScalar() : ClearCommandParameters()
                        Dim ShipToID As String = retval.ToString()
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                 "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                                 " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                                 " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                                 " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OOA.OA_ID = OOB.OA_ID AND OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                                 " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                                 " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OOA.OA_ID = ANY( " & vbCrLf & _
                                 " SELECT OA_ID FROM OA_SHIP_TO WHERE SHIP_TO_ID = @SHIP_TO_ID) AND OPB.PROJ_BRANDPACK_ID IS NULL ;  "
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, ShipToID, 25)
                    End If
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(tblMrkt.Rows(i)("START_DATE")))
                    Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 14)
                    Dim SUMPO_QTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
                    Me.ClearCommandParameters()
                    Dim DiscMustQty As Decimal = 0, TotalDiscQty As Decimal = 0
                    'ambil total disc yang seharusnya di berikan misal 30 qty
                    DiscMustQty = (GIVEN_PCT / 100) * TARGET_PKPP
                    'bila di generate
                    Dim MRKT_DISC_QTY As Decimal = 0, lEFT_QTY As Decimal = 0
                    lEFT_QTY = TARGET_PKPP - SUMPO_QTY
                    If OA_ORIGINAL_QTY + SUMPO_QTY <= TARGET_PKPP Then
                        'AMBIL SISANYA
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                    Else
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * lEFT_QTY
                    End If
                    'check disc yang sudah diambil sudah berapa
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                    'jika hasil mrkt_disc_qty + disc yang sudah di berikan masih lebih kecil dari disc_seharusnya 
                    'lanjut 
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    TotalDiscQty = Convert.ToDecimal(retval)
                    'case 1 MRKT_DISC_QTY >=0

                    If (MRKT_DISC_QTY + TotalDiscQty) >= DiscMustQty Then 'tetapi jika hasil mrkt_disc_qty + disc yang sudah di berikan > disc seharusnya
                        MRKT_DISC_QTY = DiscMustQty - TotalDiscQty 'berarti ambil sisa discount yanb bisa diterima 
                        'discmust_qty -  disc yang sudah di berikan
                    ElseIf MRKT_DISC_QTY < 0 Then
                        MRKT_DISC_QTY = DiscMustQty - TotalDiscQty
                    End If
                    If MRKT_DISC_QTY > 0 Then
                        Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_MRKT_DISC_HISTORY")
                        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40) ' VARCHAR(35),
                        Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                        Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, GIVEN_PCT) ' VARCHAR(6),
                        Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                        Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "KP", 2) ' CHAR(1),
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        'Me.IsGivenGenerated = True
                        message &= "Datarow " & (i + 1).ToString() + " Generated with > 0 qty" & vbCrLf
                        Me.GeneratedDiscountGiven += 1
                    End If
                    'NOW IS THE TIME TO INSERT INTO TBL_MRKT_DISC_HISTORY
                    Me.DiscQty.MarketingGivenDisQty = MRKT_DISC_QTY
                End If
            End If
        End Sub

        Private Sub generateCPRDDisc(ByVal tblMrkt As DataTable, ByVal i As Integer, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal PO_DATE As DateTime, ByVal FLAG As String, ByRef retval As Object, ByRef message As String)
            If Not tblMrkt.Rows(i).IsNull("GIVEN_CPR") Then

                Dim MRKT_DISC_HIST_ID As String = tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString() & "CR" & OA_BRANDPACK_ID
                Dim GIVEN_PCT As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("GIVEN_CPR"))
                Dim TARGET_CPR As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("TARGET_CPR"))
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "' OPTION (KEEP PLAN);"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Dim hasData As Boolean = False
                If Not IsNothing(retval) Then : hasData = True : End If
                If Not hasData Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM  ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                                " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                                " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                                " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                                " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OPB.PROJ_BRANDPACK_ID IS NULL ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(tblMrkt.Rows(i)("START_DATE")))
                    Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 14)
                    Dim SUMPO_QTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
                    Me.ClearCommandParameters()
                    Dim DiscMustQty As Decimal = 0, TotalDiscQty As Decimal = 0
                    'ambil total disc yang seharusnya di berikan misal 30 qty
                    DiscMustQty = (GIVEN_PCT / 100) * TARGET_CPR
                    'bila di generate
                    Dim MRKT_DISC_QTY As Decimal = 0, lEFT_QTY As Decimal = 0
                    lEFT_QTY = TARGET_CPR - SUMPO_QTY
                    If OA_ORIGINAL_QTY + SUMPO_QTY <= TARGET_CPR Then
                        'AMBIL SISANYA
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                    Else
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * lEFT_QTY
                    End If
                    'check disc yang sudah diambil sudah berapa
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                    'jika hasil mrkt_disc_qty + disc yang sudah di berikan masih lebih kecil dari disc_seharusnya 
                    'lanjut 
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    TotalDiscQty = Convert.ToDecimal(retval)
                    If (MRKT_DISC_QTY + TotalDiscQty) >= DiscMustQty Then 'tetapi jika hasil mrkt_disc_qty + disc yang sudah di berikan > disc seharusnya
                        MRKT_DISC_QTY = DiscMustQty - TotalDiscQty 'berarti ambil sisa discount yanb bisa diterima
                    ElseIf MRKT_DISC_QTY < 0 Then
                        MRKT_DISC_QTY = DiscMustQty - TotalDiscQty
                    End If 'discmust_qty -  disc yang sudah di berikan
                    If MRKT_DISC_QTY > 0 Then
                        Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_MRKT_DISC_HISTORY")
                        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40) ' VARCHAR(35),
                        Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                        Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, GIVEN_PCT) ' VARCHAR(6),
                        Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                        Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "CR", 2) ' CHAR(1),
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        'Me.IsGivenGenerated = True
                        message &= "Datarow " & (i + 1).ToString() + " Generated with > 0 qty" & vbCrLf
                        Me.GeneratedDiscountGiven += 1
                    End If
                    'NOW IS THE TIME TO INSERT INTO TBL_MRKT_DISC_HISTORY
                    Me.DiscQty.MarketingGivenDisQty = MRKT_DISC_QTY
                End If
            End If
        End Sub

        Private Sub GenerateDKDisc(ByVal tblMrkt As DataTable, ByVal i As Integer, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal PO_DATE As DateTime, ByVal FLAG As String, ByRef retval As Object, ByRef message As String)
            If Not tblMrkt.Rows(i).IsNull("GIVEN_DK") Then
                'CHECK TARGET SALES
                Dim MRKT_DISC_HIST_ID As String = tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString() & "DK" & OA_BRANDPACK_ID
                Dim GIVEN_PCT As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("GIVEN_DK"))
                Dim TARGET_DK As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("TARGET_DK"))
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "' OPTION (KEEP PLAN);"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Dim hasData As Boolean = False
                If Not IsNothing(retval) Then : hasData = True : End If
                If Not hasData Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                                " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                                " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                                " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                                " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OPB.PROJ_BRANDPACK_ID IS NULL ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(tblMrkt.Rows(i)("START_DATE")))
                    Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 14)
                    Dim SUMPO_QTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
                    Me.ClearCommandParameters()
                    Dim DiscMustQty As Decimal = 0, TotalDiscQty As Decimal = 0
                    'ambil total disc yang seharusnya di berikan misal 30 qty
                    DiscMustQty = (GIVEN_PCT / 100) * TARGET_DK
                    'bila di generate
                    Dim MRKT_DISC_QTY As Decimal = 0, lEFT_QTY As Decimal = 0
                    lEFT_QTY = TARGET_DK - SUMPO_QTY
                    If OA_ORIGINAL_QTY + SUMPO_QTY <= TARGET_DK Then
                        'AMBIL SISANYA
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                    Else
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * lEFT_QTY
                    End If
                    'check disc yang sudah diambil sudah berapa
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                    'jika hasil mrkt_disc_qty + disc yang sudah di berikan masih lebih kecil dari disc_seharusnya 
                    'lanjut 
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    TotalDiscQty = Convert.ToDecimal(retval)
                    If (MRKT_DISC_QTY + TotalDiscQty) >= DiscMustQty Then 'tetapi jika hasil mrkt_disc_qty + disc yang sudah di berikan > disc seharusnya
                        MRKT_DISC_QTY = DiscMustQty - TotalDiscQty 'berarti ambil sisa discount yanb bisa diterima
                    ElseIf MRKT_DISC_QTY < 0 Then
                        MRKT_DISC_QTY = DiscMustQty - TotalDiscQty
                    End If 'discmust_qty -  disc yang sudah di berikan
                    If MRKT_DISC_QTY > 0 Then
                        Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_MRKT_DISC_HISTORY")
                        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40) ' VARCHAR(35),
                        Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                        Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, GIVEN_PCT) ' VARCHAR(6),
                        Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                        Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "DK", 2) ' CHAR(1),
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        'Me.IsGivenGenerated = True
                        message &= "Datarow " & (i + 1).ToString() + " Generated with > 0 qty" & vbCrLf
                        Me.GeneratedDiscountGiven += 1
                    End If
                    'NOW IS THE TIME TO INSERT INTO TBL_MRKT_DISC_HISTORY
                    Me.DiscQty.MarketingGivenDisQty = MRKT_DISC_QTY
                End If
            End If
        End Sub

        ''' <summary>
        ''' CPD target ke distributor tanpa melihat siapa TM/SE nya
        ''' </summary>
        ''' <param name="tblMrkt"></param>
        ''' <param name="i"></param>
        ''' <param name="PO_DATE"></param>
        ''' <param name="retval"></param>
        ''' <param name="message"></param>
        ''' <remarks></remarks>
        Private Sub generateCPDDisc1(ByVal tblMrkt As DataTable, ByVal i As Integer, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal PO_DATE As DateTime, ByVal FLAG As String, ByRef retval As Object, ByRef message As String)
            'CHECK TARGET SALES 
            If Not tblMrkt.Rows(i).IsNull("GIVEN_CP") Then
                Dim MRKT_DISC_HIST_ID As String = tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString() & "CP" & OA_BRANDPACK_ID
                'Dim TargetCP As Decimal = Convert.ToDecimal(Me.ExecuteScalar("", "SELECT TARGET_CP FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_DIST_ID = '" & _
                'tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString() & "'"
                'CASE JIKA TOTAL PO >= TARGET_QTY, BONUS = PO_ORIGINAL_QTY * DISCOUNT
                'CASE JIKA TOTAL_PO + PO_ORIGINAL_QTY >= TARGET BONUS, BONUS = TOTAL_PO + PO_ORIGINAL_QTY * DISCOUNT
                'CASE JIKA TOTAL PO + PO_ORIGINAL_QTY < TARGET, BONUS = 0
                Dim hasData As Boolean = False
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "' OPTION (KEEP PLAN);"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then : hasData = True : End If
                If Not hasData Then
                    Dim GIVEN_PCT As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("GIVEN_CP"))
                    Dim TARGET_CP As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("TARGET_CP"))
                    Dim DiscMustQty As Decimal = (GIVEN_PCT / 100) * TARGET_CP
                    Dim MRKT_DISC_QTY As Decimal = 0
                    '--TOTALKAN SETIAP OA_ORIGINAL_QTY APAKAH SUDAH MENCAPAI TARGET BELUM
                    '  --JIKA SUM_POQTY + OA_ORIGINAL_QTY BELUM MECAPAI BONUS NILAI = 0
                    '  --JIKA SUM_POQTY + OA_ORIGINAL_QTY >= TARGET_QTY MAKA DISC% * SUMPO_QTY + OA_ORIGINAL_QTY
                    '  -- JIKA SUDAH ADA DISCOUNT DI GENERATE HITUNG JUMLAH DISCOUNT SEHARUSNYA TARGET * DISC%
                    '  --JIKA JUMLAH DISC < DISCOUNT SEHARUSNYA MAKA JUMLAH DISCOUNT 
                    '---KALO BONUS SUDAH DI GENERATE MAKA TOTALKAN BONUS YANG SUDAH DI BERIKAN APAKAH 
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                                "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                                " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                                " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                                " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                                " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OPB.PROJ_BRANDPACK_ID IS NULL AND PLANTATION_ID IS NULL;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(tblMrkt.Rows(i)("START_DATE")))
                    Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 14)
                    Dim SUMPO_QTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
                    Me.ClearCommandParameters()
                    If SUMPO_QTY >= TARGET_CP Then
                        'AMBIL SISANYA
                        ' MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                        'CHEK DULU APAKAH ADA DISC_CP YANG SUDAH DI GENERATE / BELUM 
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                    "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                        retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        If IsNothing(retval) Then
                            MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                        ElseIf Convert.ToDecimal(retval) <= 0 Then
                            MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                        ElseIf Convert.ToDecimal(retval) >= DiscMustQty Then
                            MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                        ElseIf Convert.ToDecimal(retval) < DiscMustQty And CDec(retval) > 0 Then
                            MRKT_DISC_QTY = ((GIVEN_PCT / 100) * OA_ORIGINAL_QTY) + (DiscMustQty - Convert.ToDecimal(retval))
                        End If
                    ElseIf SUMPO_QTY + OA_ORIGINAL_QTY >= TARGET_CP Then
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                               "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                        retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        If IsNothing(retval) Then
                            MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                        ElseIf Convert.ToDecimal(retval) <= 0 Then
                            MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                        ElseIf Convert.ToDecimal(retval) >= DiscMustQty Then
                            MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                        ElseIf Convert.ToDecimal(retval) < DiscMustQty And CDec(retval) > 0 Then
                            MRKT_DISC_QTY = ((GIVEN_PCT / 100) * OA_ORIGINAL_QTY) + (DiscMustQty - Convert.ToDecimal(retval))
                        End If
                    End If
                    'NOW IS THE TIME TO INSERT INTO TBL_MRKT_DISC_HISTORY
                    Me.DiscQty.MarketingGivenDisQty = MRKT_DISC_QTY
                    If MRKT_DISC_QTY > 0 Then
                        Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_MRKT_DISC_HISTORY")
                        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40) ' VARCHAR(35),
                        Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                        Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, GIVEN_PCT) ' VARCHAR(6),
                        Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                        Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "CP", 2) ' CHAR(1),
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        'Me.IsGivenGenerated = True
                        message &= "Datarow " & (i + 1).ToString() + " Generated with > 0 qty" & vbCrLf
                        Me.GeneratedDiscountGiven += 1
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' CPDK target ke distributor tanpa melihat siapa TM/SE nya
        ''' </summary>
        ''' <param name="tblMrkt"></param>
        ''' <param name="i"></param>
        ''' <param name="PO_DATE"></param>
        ''' <param name="retval"></param>
        ''' <param name="message"></param>
        ''' <remarks></remarks>
        Private Sub generateCPDKDisc1(ByVal tblMrkt As DataTable, ByVal i As Integer, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal PO_DATE As DateTime, ByVal FLAG As String, ByRef retval As Object, ByRef message As String)
            If Not tblMrkt.Rows(i).IsNull("GIVEN_CPSD") Then
                'CHECK TARGET SALES
                Dim MRKT_DISC_HIST_ID As String = tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString() & "CS" & OA_BRANDPACK_ID
                Dim GIVEN_PCT As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("GIVEN_CPSD"))
                Dim TARGET_CP As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("TARGET_CP"))
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "' OPTION (KEEP PLAN);"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Dim hasData As Boolean = False
                If Not IsNothing(retval) Then : hasData = True : End If
                If Not hasData Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                                " INNER JOIN  ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                                " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                                " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                                " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OPB.PROJ_BRANDPACK_ID IS NULL AND PLANTATION_ID IS NULL ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(tblMrkt.Rows(i)("START_DATE")))
                    Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 14)
                    Dim SUMPO_QTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
                    Me.ClearCommandParameters()
                    Dim DiscMustQty As Decimal = 0, TotalDiscQty As Decimal = 0
                    'ambil total disc yang seharusnya di berikan misal 30 qty
                    DiscMustQty = (GIVEN_PCT / 100) * TARGET_CP
                    'bila di generate
                    Dim MRKT_DISC_QTY As Decimal = 0, lEFT_QTY As Decimal = 0
                    lEFT_QTY = TARGET_CP - SUMPO_QTY
                    If OA_ORIGINAL_QTY + SUMPO_QTY <= TARGET_CP Then
                        'AMBIL SISANYA
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                    Else
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * lEFT_QTY
                    End If
                    'check disc yang sudah diambil sudah berapa
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                    'jika hasil mrkt_disc_qty + disc yang sudah di berikan masih lebih kecil dari disc_seharusnya 
                    'lanjut 
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    TotalDiscQty = Convert.ToDecimal(retval)
                    If (MRKT_DISC_QTY + TotalDiscQty) >= DiscMustQty Then 'tetapi jika hasil mrkt_disc_qty + disc yang sudah di berikan > disc seharusnya
                        MRKT_DISC_QTY = DiscMustQty - TotalDiscQty 'berarti ambil sisa discount yanb bisa diterima
                    ElseIf MRKT_DISC_QTY < 0 Then
                        MRKT_DISC_QTY = DiscMustQty - TotalDiscQty
                    End If 'discmust_qty -  disc yang sudah di berikan
                    If MRKT_DISC_QTY > 0 Then
                        Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_MRKT_DISC_HISTORY")
                        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40) ' VARCHAR(35),
                        Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                        Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, GIVEN_PCT) ' VARCHAR(6),
                        Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                        Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "CS", 2) ' CHAR(1),
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        'Me.IsGivenGenerated = True
                        message &= "Datarow " & (i + 1).ToString() + " Generated with > 0 qty" & vbCrLf
                        Me.GeneratedDiscountGiven += 1
                    End If
                    'NOW IS THE TIME TO INSERT INTO TBL_MRKT_DISC_HISTORY
                    Me.DiscQty.MarketingGivenDisQty = MRKT_DISC_QTY
                End If
            End If
        End Sub
        ''' <summary>
        ''' CPD target ke distributor dan TM/SE 
        ''' </summary>
        ''' <param name="tblMrkt"></param>
        ''' <param name="i"></param>
        ''' <param name="PO_DATE"></param>
        ''' <param name="retval"></param>
        ''' <param name="message"></param>
        ''' <remarks></remarks>
        Private Sub generateCPDDisc2(ByVal tblMrkt As DataTable, ByVal i As Integer, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal PO_DATE As DateTime, ByVal FLAG As String, ByRef retval As Object, ByRef message As String)
            ''GET TM_ID WITH THE PROGRAM PROG_BRANDPACK_DIST_ID
            Dim MRKT_DISC_HIST_ID As String = tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString() & "TD" & OA_BRANDPACK_ID
            Dim GIVEN_PCT As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("GIVEN_CP"))
            Dim TARGET_CP As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("TARGET_CP"))
            Dim DiscMustQty As Decimal = 0, TotalDiscQty As Decimal = 0
            'ambil total disc yang seharusnya di berikan misal 30 qty
            DiscMustQty = (GIVEN_PCT / 100) * TARGET_CP
            'bila di generate
            Dim MRKT_DISC_QTY As Decimal = 0, lEFT_QTY As Decimal = 0
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "' OPTION (KEEP PLAN);"
            Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            Dim hasData As Boolean = False
            If Not IsNothing(retval) Then : hasData = True : End If
            If Not hasData Then
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT TOP 1 SHIP_TO_ID FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID ; "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If IsNothing(retval) Or IsDBNull(retval) Then
                    Me.CloseConnection() : Throw New Exception("CPD with TM_Dist but Ship to TM has not been defined yet")
                ElseIf (String.IsNullOrEmpty(retval.ToString())) Then
                    Me.CloseConnection() : Throw New Exception("CPD with TM_Dist but Ship to TM has not been defined yet")
                End If
                'Query = "SET NOCOUNT ON; " & vbCrLf & _
                '       "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                '       " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                '       " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                '       " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                '       " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OPB.PROJ_BRANDPACK_ID IS NULL ;"
                'Me.ResetCommandText(CommandType.Text, Query)
                'Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                'Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, tblMrkt.Rows(i)("START_DATE"))
                'Me.AddParameter("PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                'Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)

                ''GET SHIP_TO_ID BY OA
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT TOP 1 SHIP_TO_ID FROM OA_SHIP_TO WHERE OA_ID  = (SELECT TOP 1 OA_ID FROM ORDR_OA_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID);"
                Me.ResetCommandText(CommandType.Text, Query)
                AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                retval = SqlCom.ExecuteScalar() : ClearCommandParameters()
                Dim ShipToID As String = retval.ToString()
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                         "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                         " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                         " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                         " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OOA.OA_ID = OOB.OA_ID AND OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                         " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                         " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OOA.OA_ID = ANY( " & vbCrLf & _
                         " SELECT OA_ID FROM OA_SHIP_TO WHERE SHIP_TO_ID = @SHIP_TO_ID) AND OPB.PROJ_BRANDPACK_ID IS NULL AND PLANTATION_ID IS NULL ;  "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, tblMrkt.Rows(i)("START_DATE"))
                Me.AddParameter("PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, ShipToID, 25)
                'retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Dim SUMPO_QTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar()) : Me.ClearCommandParameters()
                If SUMPO_QTY >= TARGET_CP Then
                    'AMBIL SISANYA
                    ' MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                    'CHEK DULU APAKAH ADA DISC_CP YANG SUDAH DI GENERATE / BELUM 
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If IsNothing(retval) Then
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                    ElseIf Convert.ToDecimal(retval) <= 0 Then
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                    ElseIf Convert.ToDecimal(retval) >= DiscMustQty Then
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                    ElseIf Convert.ToDecimal(retval) < DiscMustQty And CDec(retval) > 0 Then
                        MRKT_DISC_QTY = ((GIVEN_PCT / 100) * OA_ORIGINAL_QTY) + (DiscMustQty - Convert.ToDecimal(retval))
                    End If
                ElseIf SUMPO_QTY + OA_ORIGINAL_QTY >= TARGET_CP Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If IsNothing(retval) Then
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                    ElseIf Convert.ToDecimal(retval) <= 0 Then
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                    ElseIf Convert.ToDecimal(retval) >= DiscMustQty Then
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                    ElseIf Convert.ToDecimal(retval) < DiscMustQty And CDec(retval) > 0 Then
                        MRKT_DISC_QTY = ((GIVEN_PCT / 100) * OA_ORIGINAL_QTY) + (DiscMustQty - Convert.ToDecimal(retval))
                    End If
                End If

                'NOW IS THE TIME TO INSERT INTO TBL_MRKT_DISC_HISTORY
                Me.DiscQty.MarketingGivenDisQty = MRKT_DISC_QTY
                If MRKT_DISC_QTY > 0 Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_MRKT_DISC_HISTORY")
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40) ' VARCHAR(35),
                    Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                    Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, GIVEN_PCT) ' VARCHAR(6),
                    Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                    Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                    Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                    Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "TD", 2) ' CHAR(1),
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    'Me.IsGivenGenerated = True
                    message &= "Datarow " & (i + 1).ToString() + " Generated with > 0 qty" & vbCrLf
                    Me.GeneratedDiscountGiven += 1
                End If
                '--------------------------procedure dulu---------------------------------------------
                'Query = "SET NOCOUNT ON ;" & vbCrLf & _
                '        "SELECT TOP 1 SHIP_TO_ID FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID ; "
                'Me.ResetCommandText(CommandType.Text, Query)
                'Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                'retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                'If IsNothing(retval) Then
                '    System.Windows.Forms.MessageBox.Show("CPD with TM_Dist but Ship to TM has not been defined", "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                'Else
                '    'ambil PO-PO DI ORDR_ORDER_ACCEPTANCE DIMANA TM_ID = TM_ID HASIL query diatas dengan range waktu >= @START_DATE <= @PO_DATE DAN PO MESTI SUDAH Ada di OA
                '    Dim ShipToID As String = retval.ToString()
                '    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                '             "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                '             " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                '             " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                '             " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OOA.OA_ID = OOB.OA_ID AND OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                '             " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                '             " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OOA.OA_ID = ANY( " & vbCrLf & _
                '             " SELECT OA_ID FROM OA_SHIP_TO WHERE SHIP_TO_ID = @SHIP_TO_ID) AND OPB.PROJ_BRANDPACK_ID IS NULL ;  "
                '    Me.ResetCommandText(CommandType.Text, Query)
                '    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                '    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, tblMrkt.Rows(i)("START_DATE"))
                '    Me.AddParameter("PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                '    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                '    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                '    Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, ShipToID, 25)
                '    'retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                '    Dim SUMPO_QTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
                '    Me.ClearCommandParameters()
                '    If SUMPO_QTY >= TARGET_CP Then
                '        'AMBIL SISANYA
                '        ' MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                '        'CHEK DULU APAKAH ADA DISC_CP YANG SUDAH DI GENERATE / BELUM 
                '        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                '                "SELECT SUM(MRKT_DISC_QTY) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                '        Me.ResetCommandText(CommandType.Text, Query)
                '        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                '        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                '        retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                '        If IsNothing(retval) Then
                '            MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                '        ElseIf Convert.ToDecimal(retval) <= 0 Then
                '            MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                '        ElseIf Convert.ToDecimal(retval) >= DiscMustQty Then
                '            MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                '        ElseIf Convert.ToDecimal(retval) < DiscMustQty And CDec(retval) > 0 Then
                '            MRKT_DISC_QTY = ((GIVEN_PCT / 100) * OA_ORIGINAL_QTY) + (DiscMustQty - Convert.ToDecimal(retval))
                '        End If
                '    ElseIf SUMPO_QTY + OA_ORIGINAL_QTY >= TARGET_CP Then
                '        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                '                "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                '        Me.ResetCommandText(CommandType.Text, Query)
                '        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                '        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                '        retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                '        If IsNothing(retval) Then
                '            MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                '        ElseIf Convert.ToDecimal(retval) <= 0 Then
                '            MRKT_DISC_QTY = (GIVEN_PCT / 100) * (SUMPO_QTY + OA_ORIGINAL_QTY)
                '        ElseIf Convert.ToDecimal(retval) >= DiscMustQty Then
                '            MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                '        ElseIf Convert.ToDecimal(retval) < DiscMustQty And CDec(retval) > 0 Then
                '            MRKT_DISC_QTY = ((GIVEN_PCT / 100) * OA_ORIGINAL_QTY) + (DiscMustQty - Convert.ToDecimal(retval))
                '        End If
                '    End If

                '    'NOW IS THE TIME TO INSERT INTO TBL_MRKT_DISC_HISTORY
                '    Me.DiscQty.MarketingGivenDisQty = MRKT_DISC_QTY
                '    If MRKT_DISC_QTY > 0 Then
                '        Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_MRKT_DISC_HISTORY")
                '        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                '        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                '        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40) ' VARCHAR(35),
                '        Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                '        Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, GIVEN_PCT) ' VARCHAR(6),
                '        Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                '        Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                '        Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                '        Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "TD", 2) ' CHAR(1),
                '        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 30) ' VARCHAR(30)
                '        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                '        'Me.IsGivenGenerated = True
                '        Message &= "Datarow " & (i + 1).ToString() + " Generated with > 0 qty" & vbCrLf
                '        Me.GeneratedDiscountGiven += 1
                '    End If
                'End If
            End If
        End Sub
        Private Sub generateCPDKDisc2(ByVal tblMrkt As DataTable, ByVal i As Integer, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal PO_DATE As DateTime, ByVal FLAG As String, ByRef retval As Object, ByRef message As String)
            Dim MRKT_DISC_HIST_ID As String = tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString() & "TS" & OA_BRANDPACK_ID
            Dim GIVEN_PCT As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("GIVEN_CPSD"))
            Dim TARGET_CP As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("TARGET_CP"))
            Dim DiscMustQty As Decimal = 0, TotalDiscQty As Decimal = 0
            'ambil total disc yang seharusnya di berikan misal 30 qty
            'DiscMustQty = (GIVEN_PCT / 100) * TARGET_CP
            'bila di generate
            Dim MRKT_DISC_QTY As Decimal = 0, lEFT_QTY As Decimal = 0
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "' OPTION (KEEP PLAN);"
            Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            Dim hasData As Boolean = False
            If Not IsNothing(retval) Then : hasData = True : End If
            If Not hasData Then
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT TOP 1 SHIP_TO_ID FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID ; "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If IsNothing(retval) Or IsDBNull(retval) Then
                    Me.CloseConnection() : Throw New Exception("CPD with TM_Dist but Ship to TM has not been defined yet")
                ElseIf (String.IsNullOrEmpty(retval.ToString())) Then
                    Me.CloseConnection() : Throw New Exception("CPD with TM_Dist but Ship to TM has not been defined yet")
                End If

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                       "SELECT TOP 1 SHIP_TO_ID FROM OA_SHIP_TO WHERE OA_ID = (SELECT TOP 1 OA_ID FROM ORDR_OA_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID);"
                Me.ResetCommandText(CommandType.Text, Query)
                AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                retval = SqlCom.ExecuteScalar() : ClearCommandParameters()
                Dim ShipToID As String = retval.ToString()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                         "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                         " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                         " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                         " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OOA.OA_ID = OOB.OA_ID AND OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                         " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                         " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                         " AND OPB.PROJ_BRANDPACK_ID IS NULL  AND PLANTATION_ID IS NULL" & vbCrLf & _
                         " AND OOA.OA_ID = ANY( " & vbCrLf & _
                         " SELECT OA_ID FROM OA_SHIP_TO WHERE SHIP_TO_ID = @SHIP_TO_ID); "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, tblMrkt.Rows(i)("START_DATE"))
                Me.AddParameter("PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, ShipToID, 25)
                'retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Dim SUMPO_QTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
                Me.ClearCommandParameters()
                'ambil total disc yang seharusnya di berikan misal 30 qty
                DiscMustQty = (GIVEN_PCT / 100) * TARGET_CP
                'bila di generate
                lEFT_QTY = TARGET_CP - SUMPO_QTY
                If OA_ORIGINAL_QTY + SUMPO_QTY <= TARGET_CP Then
                    'AMBIL SISANYA
                    'chek apakah OA_INI ship_to nya sesuiai dengan ship_to_id di prog_branpack_dist_id
                    'Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    '        "SELECT SHIP_TO_ID FROM FROM OA_SHIP_TO WHERE OA_ID = (SELECT TOP OA_ID FROM ORDR_OA_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID "
                    MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                Else
                    MRKT_DISC_QTY = (GIVEN_PCT / 100) * lEFT_QTY
                End If
                'check disc yang sudah diambil sudah berapa
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                          "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                'jika hasil mrkt_disc_qty + disc yang sudah di berikan masih lebih kecil dari disc_seharusnya 
                'lanjut 
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                TotalDiscQty = Convert.ToDecimal(retval)
                If (MRKT_DISC_QTY + TotalDiscQty) >= DiscMustQty Then 'tetapi jika hasil mrkt_disc_qty + disc yang sudah di berikan > disc seharusnya
                    MRKT_DISC_QTY = DiscMustQty - TotalDiscQty 'berarti ambil sisa discount yanb bisa diterima
                ElseIf MRKT_DISC_QTY < 0 Then
                    MRKT_DISC_QTY = DiscMustQty - TotalDiscQty
                End If 'discmust_qty -  disc yang sudah di berikan
                If MRKT_DISC_QTY > 0 Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_MRKT_DISC_HISTORY")
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40) ' VARCHAR(35),
                    Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                    Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, GIVEN_PCT) ' VARCHAR(6),
                    Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                    Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                    Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                    Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "TS", 2) ' CHAR(1),
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    'Me.IsGivenGenerated = True
                    message &= "Datarow " & (i + 1).ToString() + " Generated with > 0 qty" & vbCrLf
                    Me.GeneratedDiscountGiven += 1
                End If
                'NOW IS THE TIME TO INSERT INTO TBL_MRKT_DISC_HISTORY
                Me.DiscQty.MarketingGivenDisQty = MRKT_DISC_QTY
            End If
        End Sub
        ''' <summary>
        ''' CPRMT dengan target ke distributor saja
        ''' </summary>
        ''' <param name="tblMrkt"></param>
        ''' <param name="i"></param>
        ''' <param name="PO_DATE"></param>
        ''' <param name="retval"></param>
        ''' <param name="message"></param>
        ''' <remarks></remarks>
        Private Sub generateCPRMTDisc1(ByVal tblMrkt As DataTable, ByVal i As Integer, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal PO_DATE As DateTime, ByVal FLAG As String, ByRef retval As Object, ByRef message As String)
            Dim MRKT_DISC_HIST_ID As String = tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString() & "CT" & OA_BRANDPACK_ID
            Dim GIVEN_PCT As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("GIVEN_CPMRT"))
            Dim TARGET_CP As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("TARGET_CPMRT"))
            Dim DiscMustQty As Decimal = 0, TotalDiscQty As Decimal = 0
            'ambil total disc yang seharusnya di berikan misal 30 qty
            'DiscMustQty = (GIVEN_PCT / 100) * TARGET_CP
            'bila di generate
            Dim MRKT_DISC_QTY As Decimal = 0, lEFT_QTY As Decimal = 0
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "' OPTION (KEEP PLAN);"
            Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
            Dim hasData As Boolean = False
            If Not IsNothing(retval) Then : hasData = True : End If
            If Not hasData Then
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT TOP 1 SHIP_TO_ID FROM MRKT_BRANDPACK_DISTRIBUTOR WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID ; "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If IsNothing(retval) Or IsDBNull(retval) Then
                    Me.CloseConnection() : Throw New Exception("CP(R M/T) with TM_Dist but Ship to TM has not been defined yet")
                ElseIf (String.IsNullOrEmpty(retval.ToString())) Then
                    Me.CloseConnection() : Throw New Exception("CP(R M/T) with TM_Dist but Ship to TM has not been defined yet")
                End If

                ''GET SHIP_TO_ID BY OA
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT TOP 1 SHIP_TO_ID FROM OA_SHIP_TO WHERE OA_ID = (SELECT TOP 1 OA_ID FROM ORDR_OA_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID);"
                Me.ResetCommandText(CommandType.Text, Query)
                AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75)
                retval = SqlCom.ExecuteScalar() : ClearCommandParameters()
                Dim ShipToID As String = retval.ToString()
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                         "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                         " INNER JOIN ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                         " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                         " INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OOA.OA_ID = OOB.OA_ID AND OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                         " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                         " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                         " AND OPB.PROJ_BRANDPACK_ID IS NULL AND PLANTATION_ID IS NULL  " & vbCrLf & _
                         " AND OOA.OA_ID = ANY( " & vbCrLf & _
                         " SELECT OA_ID FROM OA_SHIP_TO WHERE SHIP_TO_ID = @SHIP_TO_ID); "
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, tblMrkt.Rows(i)("START_DATE"))
                Me.AddParameter("PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Me.AddParameter("@SHIP_TO_ID", SqlDbType.VarChar, ShipToID, 25)
                'retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Dim SUMPO_QTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
                Me.ClearCommandParameters()
                'ambil total disc yang seharusnya di berikan misal 30 qty
                DiscMustQty = (GIVEN_PCT / 100) * TARGET_CP
                'bila di generate
                lEFT_QTY = TARGET_CP - SUMPO_QTY
                If OA_ORIGINAL_QTY + SUMPO_QTY <= TARGET_CP Then
                    'AMBIL SISANYA
                    'chek apakah OA_INI ship_to nya sesuiai dengan ship_to_id di prog_branpack_dist_id
                    'Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    '        "SELECT SHIP_TO_ID FROM FROM OA_SHIP_TO WHERE OA_ID = (SELECT TOP OA_ID FROM ORDR_OA_BRANDPACK WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID "
                    MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                Else
                    MRKT_DISC_QTY = (GIVEN_PCT / 100) * lEFT_QTY
                End If
                'check disc yang sudah diambil sudah berapa
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                          "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                'jika hasil mrkt_disc_qty + disc yang sudah di berikan masih lebih kecil dari disc_seharusnya 
                'lanjut 
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                TotalDiscQty = Convert.ToDecimal(retval)
                If (MRKT_DISC_QTY + TotalDiscQty) >= DiscMustQty Then 'tetapi jika hasil mrkt_disc_qty + disc yang sudah di berikan > disc seharusnya
                    MRKT_DISC_QTY = DiscMustQty - TotalDiscQty 'berarti ambil sisa discount yanb bisa diterima
                ElseIf MRKT_DISC_QTY < 0 Then
                    MRKT_DISC_QTY = DiscMustQty - TotalDiscQty
                End If 'discmust_qty -  disc yang sudah di berikan
                If MRKT_DISC_QTY > 0 Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_MRKT_DISC_HISTORY")
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                    Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40) ' VARCHAR(35),
                    Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                    Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, GIVEN_PCT) ' VARCHAR(6),
                    Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                    Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                    Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                    Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "CT", 2) ' CHAR(1),
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    'Me.IsGivenGenerated = True
                    message &= "Datarow " & (i + 1).ToString() + " Generated with > 0 qty" & vbCrLf
                    Me.GeneratedDiscountGiven += 1
                End If
                'NOW IS THE TIME TO INSERT INTO TBL_MRKT_DISC_HISTORY
                Me.DiscQty.MarketingGivenDisQty = MRKT_DISC_QTY
            End If
        End Sub
        ''' <summary>
        ''' CPRMT dengan target distributor dan TM/SE
        ''' </summary>
        ''' <param name="tblMrkt"></param>
        ''' <param name="i"></param>
        ''' <param name="PO_DATE"></param>
        ''' <param name="retval"></param>
        ''' <param name="message"></param>
        ''' <remarks></remarks>
        Private Sub generateCPRMTDisc2(ByVal tblMrkt As DataTable, ByVal i As Integer, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal PO_DATE As DateTime, ByVal FLAG As String, ByRef retval As Object, ByRef message As String)
            If Not tblMrkt.Rows(i).IsNull("GIVEN_CPMRT") Then
                'CHECK TARGET SALES
                Dim MRKT_DISC_HIST_ID As String = tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString() & "CD" & OA_BRANDPACK_ID
                Dim GIVEN_PCT As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("GIVEN_CPMRT"))
                Dim TARGET_CP As Decimal = Convert.ToDecimal(tblMrkt.Rows(i)("TARGET_CPMRT"))
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "' OPTION (KEEP PLAN);"
                Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Dim hasData As Boolean = False
                If Not IsNothing(retval) Then : hasData = True : End If
                If Not hasData Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                "SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                                " INNER JOIN  ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                                " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                                " WHERE OPB.PO_REF_NO != @PO_REF_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                                " AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OPB.PROJ_BRANDPACK_ID IS NULL AND PLANTATION_ID IS NULL ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 30)
                    Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(tblMrkt.Rows(i)("START_DATE")))
                    Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 14)
                    Dim SUMPO_QTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
                    Me.ClearCommandParameters()
                    Dim DiscMustQty As Decimal = 0, TotalDiscQty As Decimal = 0
                    'ambil total disc yang seharusnya di berikan misal 30 qty
                    DiscMustQty = (GIVEN_PCT / 100) * TARGET_CP
                    'bila di generate
                    Dim MRKT_DISC_QTY As Decimal = 0, lEFT_QTY As Decimal = 0
                    lEFT_QTY = TARGET_CP - SUMPO_QTY
                    If OA_ORIGINAL_QTY + SUMPO_QTY <= TARGET_CP Then
                        'AMBIL SISANYA
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY
                    Else
                        MRKT_DISC_QTY = (GIVEN_PCT / 100) * lEFT_QTY
                    End If
                    'check disc yang sudah diambil sudah berapa
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                "SELECT ISNULL(SUM(MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY WHERE PROG_BRANDPACK_DIST_ID = @PROG_BRANDPACK_DIST_ID AND MRKT_DISC_HIST_ID != @MRKT_DISC_HIST_ID AND MRKT_DISC_QTY > 0 AND SGT_FLAG = '" & FLAG & "' ;"
                    'jika hasil mrkt_disc_qty + disc yang sudah di berikan masih lebih kecil dari disc_seharusnya 
                    'lanjut 
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40)
                    Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    TotalDiscQty = Convert.ToDecimal(retval)
                    If (MRKT_DISC_QTY + TotalDiscQty) >= DiscMustQty Then 'tetapi jika hasil mrkt_disc_qty + disc yang sudah di berikan > disc seharusnya
                        MRKT_DISC_QTY = DiscMustQty - TotalDiscQty 'berarti ambil sisa discount yanb bisa diterima
                    ElseIf MRKT_DISC_QTY < 0 Then
                        MRKT_DISC_QTY = DiscMustQty - TotalDiscQty
                    End If 'discmust_qty -  disc yang sudah di berikan
                    If MRKT_DISC_QTY > 0 Then
                        Me.ResetCommandText(CommandType.StoredProcedure, "Sp_Insert_MRKT_DISC_HISTORY")
                        Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                        Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                        Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, tblMrkt.Rows(i)("PROG_BRANDPACK_DIST_ID").ToString(), 40) ' VARCHAR(35),
                        Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                        Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, GIVEN_PCT) ' VARCHAR(6),
                        Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                        Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, MRKT_DISC_QTY) ' INT,
                        Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "CD", 2) ' CHAR(1),
                        Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        'Me.IsGivenGenerated = True
                        message &= "Datarow " & (i + 1).ToString() + " Generated with > 0 qty" & vbCrLf
                        Me.GeneratedDiscountGiven += 1
                    End If
                    'NOW IS THE TIME TO INSERT INTO TBL_MRKT_DISC_HISTORY
                    Me.DiscQty.MarketingGivenDisQty = MRKT_DISC_QTY
                End If
            End If
        End Sub
        Private Sub generateDKNDisct(ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal PO_DATE As DateTime, ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal FLAG As String, ByRef retval As Object, ByRef message As String)


            Query = "SET NOCOUNT ON; " & vbCrLf & _
                       " DECLARE @V_BRAND_ID VARCHAR(7) ;" & vbCrLf & _
                       " SET @V_BRAND_ID = (SELECT TOP 1 BRAND_ID FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID)" & vbCrLf & _
                       " IF EXISTS(SELECT BRAND_ID FROM SALES_DKN_PROGRESSIVE SDP INNER JOIN SALES_DKN_SCHEMA SDS ON SDS.IDApp = SDP.FKApp_SDS WHERE BRAND_ID = @V_BRAND_ID " & vbCrLf & _
                       "           AND (SDS.START_DATE <= @PO_DATE AND SDS.END_DATE >= @PO_DATE)) " & vbCrLf & _
                       " BEGIN " & vbCrLf & _
                       " SELECT SDS.START_DATE,SDS.END_DATE,SDP.MIN_TO_DATE,SDP.DISCOUNT FROM SALES_DKN_PROGRESSIVE SDP INNER JOIN SALES_DKN_SCHEMA SDS ON SDS.IDApp = SDP.FKApp_SDS " & vbCrLf & _
                       " WHERE SDP.BRAND_ID = @V_BRAND_ID AND (SDS.START_DATE <= @PO_DATE AND SDS.END_DATE >= @PO_DATE) ORDER BY SDP.MIN_TO_DATE ASC ;" & vbCrLf & _
                       " END " & vbCrLf & _
                       " ELSE " & vbCrLf & _
                       " BEGIN " & vbCrLf & _
                       " SELECT SDS.START_DATE,SDS.END_DATE, SDP.MIN_TO_DATE,SDP.DISCOUNT FROM SALES_DKN_PROGRESSIVE SDP INNER JOIN SALES_DKN_SCHEMA SDS ON SDS.IDApp = SDP.FKApp_SDS " & vbCrLf & _
                       " WHERE (SDS.START_DATE <= @PO_DATE AND SDS.END_DATE >= @PO_DATE) ORDER BY SDP.MIN_TO_DATE ASC; " & vbCrLf & _
                       " END "
            If IsNothing(Me.SqlCom) Then
                Me.CreateCommandSql("", Query)
            Else : Me.ResetCommandText(CommandType.Text, Query)
            End If
            If IsNothing(Me.SqlCom.Transaction) Then
                Me.SqlCom.Transaction = Me.SqlTrans
            End If
            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 15)
            Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
            Me.OpenConnection()
            Dim dt As New DataTable("SalesGiven") : dt.Clear()
            setDataAdapter(Me.SqlCom).Fill(dt)
            If dt.Rows.Count > 0 Then
                ''define what the date today is
                Dim datePODate = PO_DATE.Day
                Dim Periode As String = String.Format("{0:dd/MM/yyyy}-{1:dd/MM/yyyy}", Convert.ToDateTime(dt.Rows(0)("START_DATE")), Convert.ToDateTime(dt.Rows(0)("END_DATE")))
                Dim discount As Decimal = 0
                For Each row As DataRow In dt.Rows
                    If Not row.IsNull("MIN_TO_DATE") And Not IsNothing(row("MIN_TO_DATE")) Then
                        If datePODate <= Convert.ToDecimal(row("MIN_TO_DATE")) Then
                            discount = Convert.ToDecimal(row("DISCOUNT"))
                            Exit For
                        End If
                    End If
                Next
                If discount > 0 Then
                    Dim discQty As Decimal = OA_ORIGINAL_QTY * (discount / 100)
                    'insert ke database
                    If discQty > 0 Then
                        'create MRKT_DISC_HIST_ID
                        'MRKT_DISC_HIST_ID = PROG_BRANDPACK_DIST_ID + FLAG+ OA_BRANDPACK_ID
                        'PROG_BRANDPACK_DIST_ID = Periode
                        ''chek MRKT_DISC_HIST_ID
                        Dim MRKT_DISC_HIST_ID As String = Periode + "|DN|" + OA_BRANDPACK_ID
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = @MrktDiscHistID ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@MrktDiscHistID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                        retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        Dim hasData As Boolean = False
                        If Not IsNothing(retval) Then : hasData = True : End If
                        If Not hasData Then
                            Query = "SET NOCOUNT ON; " & vbCrLf & _
                                    "INSERT INTO MRKT_DISC_HISTORY(MRKT_DISC_HIST_ID,OA_BRANDPACK_ID,PROG_BRANDPACK_DIST_ID,SDS_PERIODE," & vbCrLf & _
                                    "MRKT_OA_QTY,MRKT_DISC_PCT,MRKT_DISC_QTY,MRKT_RELEASE_QTY,MRKT_LEFT_QTY,SGT_FLAG,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                                    "VALUES(@MRKT_DISC_HIST_ID,@OA_BRANDPACK_ID,@PROG_BRANDPACK_DIST_ID,@SDS_PERIODE,@MRKT_OA_QTY,@MRKT_DISC_PCT," & vbCrLf & _
                                    "@MRKT_DISC_QTY,@MRKT_RELEASE_QTY,@MRKT_LEFT_QTY,@SGT_FLAG,@CREATE_BY,GETDATE())"
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                            Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                            Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, DBNull.Value, 40) ' VARCHAR(35),
                            Me.AddParameter("@SDS_PERIODE", SqlDbType.VarChar, Periode, 40) ' VARCHAR(35)
                            Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                            Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, discount) ' VARCHAR(6),
                            Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, discQty) ' INT,
                            Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                            Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, discQty) ' INT,
                            Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "DN", 2) ' CHAR(1),
                            Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)

                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                            'Me.IsGivenGenerated = True
                            message &= "Discount Generated with > 0 qty" & vbCrLf
                            Me.GeneratedDiscountGiven += 1
                        End If

                    End If
                End If
            End If
        End Sub

        Private Sub GenerateCPDAuto(ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal PO_DATE As DateTime, ByVal BRANDPACK_ID As String, ByVal OA_BRANDPACK_ID As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal FLAG As String, ByRef retval As Object, ByRef message As String)
            ''ambil data periode apakah sudah di Q1/Q2
            Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " SELECT  TOP 1 AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE,AA.QS_TREATMENT_FLAG FROM AGREE_AGREEMENT AA INNER JOIN AGREE_BRANDPACK_INCLUDE ABI ON ABI.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                    " INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO WHERE AA.START_DATE <= @PO_DATE AND AA.END_DATE >= @PO_DATE AND DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                    " AND ABI.BRANDPACK_ID = @BRANDPACK_ID ORDER BY AA.END_DATE DESC ;"
            ''jumlahkan Quantity PO dari tanggal
            If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
            Else : Me.ResetCommandText(CommandType.Text, Query)
            End If
            Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 14)
            If IsNothing(Me.SqlCom.Transaction) Then
                Me.SqlCom.Transaction = Me.SqlTrans
            End If
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            Dim StartDate As Object = Nothing, EndDate As Object = Nothing
            Dim FlagAgreement As String = ""
            Dim AgreementNo As String = ""
            While Me.SqlRe.Read()
                StartDate = Me.SqlRe("START_DATE") : EndDate = Me.SqlRe("END_DATE")
                FlagAgreement = IIf((IsNothing(Me.SqlRe("QS_TREATMENT_FLAG")) Or IsDBNull(Me.SqlRe("QS_TREATMENT_FLAG"))), "", SqlRe.GetString(3))
                AgreementNo = IIf((IsNothing(Me.SqlRe("AGREEMENT_NO")) Or IsDBNull(Me.SqlRe("AGREEMENT_NO"))), "", SqlRe.GetString(0))
            End While : Me.SqlRe.Close()
            If IsNothing(StartDate) Or String.IsNullOrEmpty(AgreementNo) Or String.IsNullOrEmpty(FlagAgreement) Then
                Throw New Exception("Can not find periode of PKD in system database")
            End If
            Dim StartDateQ1 As DateTime = Convert.ToDateTime(StartDate)
            Dim EndDateQ1 = StartDate.AddMonths(3).AddDays(-1)
            Dim StartDateQ2 = EndDateQ1.AddDays(1)
            Dim EndDateQ2 = StartDateQ2.AddMonths(3).AddDays(-1)
            Dim StartDateQ3 = EndDateQ2.AddDays(1)
            Dim EndDateQ3 = StartDateQ3.AddMonths(3).AddDays(-1)
            Dim StartDateQ4 = EndDateQ3.AddDays(1)
            Dim EndDateQ4 = Convert.ToDateTime(EndDate)
            ''check current berada di periode mana
            Dim TargetPKD As Decimal = 0
            Dim TargetFlag As String = "TARGET_S1"
            Dim VStartDate As Object = Nothing
            Dim Description As String = "Discount = ("
            If StartDateQ1 <= PO_DATE And EndDateQ1 >= PO_DATE Then
                VStartDate = StartDateQ1
                If FlagAgreement = "S" Then
                    TargetFlag = "TARGET_S1_FM"
                Else
                    TargetFlag = "TARGET_Q1_FM"
                End If
            ElseIf StartDateQ2 <= PO_DATE And EndDateQ2 >= PO_DATE Then
                VStartDate = StartDateQ2
                If FlagAgreement = "S" Then
                    TargetFlag = "TARGET_S1_FM"
                Else
                    TargetFlag = "TARGET_Q2_FM"
                End If
            ElseIf StartDateQ3 <= PO_DATE And EndDateQ3 >= PO_DATE Then
                VStartDate = StartDateQ3
                If FlagAgreement = "S" Then
                    TargetFlag = "TARGET_S2_FM"
                Else
                    TargetFlag = "TARGET_Q3_FM"
                End If
            ElseIf StartDateQ4 <= PO_DATE And EndDateQ4 >= PO_DATE Then
                VStartDate = StartDateQ4
                If FlagAgreement = "S" Then
                    TargetFlag = "TARGET_S2_FM"
                Else
                    TargetFlag = "TARGET_Q4_FM"
                End If
            End If
            Dim Periode As String = String.Format("{0:dd/MM/yyyy}-{1:dd/MM/yyyy}", Convert.ToDateTime(StartDateQ1), _
            Convert.ToDateTime(EndDate)) ''StartDateQ1 = StartDate , Nilai StartDate udah berubah, jadi yang di pake adalah StartDateQ1

            Query = "SET NOCOUNT ON;" & vbCrLf & _
                       " SELECT TOP 1 " & TargetFlag & " FROM AGREE_BRAND_INCLUDE ABI INNER JOIN AGREE_BRANDPACK_INCLUDE ABP ON ABP.AGREE_BRAND_ID = ABI.AGREE_BRAND_ID " & vbCrLf & _
                       " WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND ABP.BRANDPACK_ID = @BRANDPACK_ID;  "
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNo)
            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
            retval = Me.SqlCom.ExecuteScalar()
            If IsNothing(retval) Or IsDBNull(retval) Then
                'check combinedBrandID
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " DECLARE @V_CombinedBrandID VARCHAR(100), @V_BRAND_ID VARCHAR(14),@V_AGREE_BRAND_ID VARCHAR(100);" & vbCrLf & _
                    " SET @V_BRAND_ID = (SELECT TOP 1 BRAND_ID FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID);" & vbCrLf & _
                    " SET @V_AGREE_BRAND_ID = (@AGREEMENT_NO + @V_BRAND_ID); " & vbCrLf & _
                    " SET @V_CombinedBrandID = (SELECT TOP 1 COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = @V_AGREE_BRAND_ID);  " & vbCrLf & _
                    " IF (@V_CombinedBrandID IS NOT NULL) " & vbCrLf & _
                    " BEGIN SELECT TOP 1 " & TargetFlag & " FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = @V_CombinedBrandID ; END "
                Me.ResetCommandText(CommandType.Text, Query)
                retval = Me.SqlCom.ExecuteScalar()
            Else
                If Convert.ToDecimal(retval) <= 0 Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " DECLARE @V_CombinedBrandID VARCHAR(100), @V_BRAND_ID VARCHAR(14),@V_AGREE_BRAND_ID VARCHAR(100);" & vbCrLf & _
                    " SET @V_BRAND_ID = (SELECT TOP 1 BRAND_ID FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID);" & vbCrLf & _
                    " SET @V_AGREE_BRAND_ID = (@AGREEMENT_NO + @V_BRAND_ID); " & vbCrLf & _
                    " SET @V_CombinedBrandID = (SELECT TOP 1 COMB_AGREE_BRAND_ID FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = @V_AGREE_BRAND_ID);  " & vbCrLf & _
                    " IF (@V_CombinedBrandID IS NOT NULL) " & vbCrLf & _
                    " BEGIN SELECT TOP 1 " & TargetFlag & " FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = @V_CombinedBrandID ; END "
                    Me.ResetCommandText(CommandType.Text, Query)
                    retval = Me.SqlCom.ExecuteScalar()
                End If
            End If
            If IsNothing(retval) Or IsDBNull(retval) Then
                Throw New Exception("Can not find " & TargetFlag & " on PKD" & vbCrLf & "Or " & TargetFlag & " is zero") : Return
            Else
                If Convert.ToDecimal(retval) <= 0 Then
                    Throw New Exception("Can not find " & TargetFlag & " on PKD" & vbCrLf & "Or " & TargetFlag & " is zero") : Return
                End If
            End If
            Select Case TargetFlag
                Case "TARGET_S1_FM", "TARGET_S2_FM"
                    TargetPKD = Convert.ToDecimal(retval) / 2
                Case "TARGET_Q1_FM", "TARGET_Q2_FM", "TARGET_Q3_FM", "TARGET_Q4_FM"
                    TargetPKD = Convert.ToDecimal(retval)
            End Select
            Me.ClearCommandParameters()

            Query = "SET NOCOUNT ON ;" & vbCrLf & _
                    " DECLARE @V_BRAND_ID VARCHAR(10); " & vbCrLf & _
                    " SET @V_BRAND_ID = (SELECT TOP 1 BRAND_ID FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID) ;" & vbCrLf & _
                    " SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                    " INNER JOIN  ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                    " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                    " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = OPB.BRANDPACK_ID " & vbCrLf & _
                    " WHERE PO.PO_REF_DATE >= @START_PERIODE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                    " AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OPB.PROJ_BRANDPACK_ID IS NULL AND OPB.PLANTATION_ID IS NULL " & vbCrLf & _
                    " AND BB.BRAND_ID = @V_BRAND_ID AND PO.PO_REF_NO != @PO_REF_NO ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@START_PERIODE", SqlDbType.SmallDateTime, VStartDate)
            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
            Me.AddParameter("BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
            Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25) ' VARCHAR(25),
            Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
            Dim SUMPO_QTY As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
            'Me.ClearCommandParameters()

            'Jumlahkan Query untuk PO ini berdasarkan brand
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                   " DECLARE @V_BRAND_ID VARCHAR(10); " & vbCrLf & _
                   " SET @V_BRAND_ID = (SELECT TOP 1 BRAND_ID FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID) ;" & vbCrLf & _
                   " SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                   " INNER JOIN  ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                   " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                   " INNER JOIN BRND_BRANDPACK BB ON BB.BRANDPACK_ID = OPB.BRANDPACK_ID " & vbCrLf & _
                   " WHERE PO.PO_REF_NO = @PO_REF_NO " & vbCrLf & _
                   " AND BB.BRAND_ID = @V_BRAND_ID ;"
            Me.ResetCommandText(CommandType.Text, Query)
            SUMPO_QTY += Convert.ToDecimal(Me.SqlCom.ExecuteScalar())
            Me.ClearCommandParameters()
            ''ambil data Discount progressive
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " DECLARE @V_IDAppHeader INT ;" & vbCrLf & _
                    " SET @V_IDAppHeader = (SELECT TOP 1 IDApp FROM SALES_CPDAUTO_HEADER WHERE START_PERIODE = CONVERT(VARCHAR(100),@START_PERIODE,101) AND END_PERIODE = CONVERT(VARCHAR(100),@END_PERIODE,101) ); " & vbCrLf & _
                     "SELECT * FROM SALES_CPDAUTO_PROG_DISC WHERE FKCode = @V_IDAppHeader ORDER BY UP_TO_PCT DESC ;"
            ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@START_PERIODE", SqlDbType.SmallDateTime, StartDate)
            Me.AddParameter("@END_PERIODE", SqlDbType.SmallDateTime, EndDate)
            Dim tblProgDisc As New DataTable("PROGRESSIVE_DISCOUNT")
            setDataAdapter(Me.SqlCom).Fill(tblProgDisc)

            If tblProgDisc.Rows.Count <= 0 Then
                Throw New Exception("Can not find Progressive discount for current CPD") : Return
            End If
            Dim Percentage As Decimal = SUMPO_QTY / TargetPKD
            Dim Dispro As Decimal = 0
            For i As Integer = 0 To tblProgDisc.Rows.Count - 1
                If Not IsDBNull(tblProgDisc.Rows(i)("UP_TO_PCT")) And Not IsNothing(tblProgDisc.Rows(i)("UP_TO_PCT")) Then
                    If Percentage >= Convert.ToDecimal(tblProgDisc.Rows(i)("UP_TO_PCT")) Then
                        Dispro = Convert.ToDecimal(tblProgDisc.Rows(i)("DISCOUNT"))
                        Exit For
                    End If
                End If
            Next
            If Dispro <= 0 Then
                message = String.Format("computed 0 dispro, % Achievement is {0:p}", Convert.ToDecimal(Percentage))
                Return
            End If
            'check apakah data sudah di generate

            Dim MRKT_DISC_HIST_ID As String = Periode + "|CA|" + OA_BRANDPACK_ID
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                    " SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE MRKT_DISC_HIST_ID = @MrktDiscHistID ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@MrktDiscHistID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
            retval = Me.SqlCom.ExecuteScalar()
            If Not IsNothing(retval) And Not IsDBNull(retval) Then
                Throw New Exception("Discount has already been computed previously")
            End If
            'get table terms po PO
            Query = "SET NOCOUNT ON;" & vbCrLf & _
                     " DECLARE @V_IDAppHeader INT ;" & vbCrLf & _
                     " SET @V_IDAppHeader = (SELECT TOP 1 IDApp FROM SALES_CPDAUTO_HEADER WHERE START_PERIODE = CONVERT(VARCHAR(100),@START_PERIODE,101) AND END_PERIODE = CONVERT(VARCHAR(100),@END_PERIODE,101) ); " & vbCrLf & _
                     " SELECT * FROM SALES_CPDAUTO_TERMS WHERE FKCode = @V_IDAppHeader ORDER BY MAX_TO_DATE DESC ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Dim tblTermsDisc As New DataTable("TERMP_OF_PO")
            setDataAdapter(Me.SqlCom).Fill(tblTermsDisc)

            Dim FkCode As Object = DBNull.Value
            If tblProgDisc.Rows.Count > 0 Then
                FkCode = CInt(tblProgDisc.Rows(0)("FKCode"))
            ElseIf tblTermsDisc.Rows.Count > 0 Then
                FkCode = CInt(tblTermsDisc.Rows(0)("FKCode"))
            End If
            'Me.ClearCommandParameters()
            'jumlahkan totalPO by BrandPackID
            Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " SELECT ISNULL(SUM(OOB.OA_ORIGINAL_QTY),0) FROM ORDR_OA_BRANDPACK OOB " & vbCrLf & _
                    " INNER JOIN  ORDR_PO_BRANDPACK OPB ON OOB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                    " INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO " & vbCrLf & _
                    " WHERE PO.PO_REF_DATE >= @START_PERIODE AND PO.PO_REF_DATE <= @PO_DATE " & vbCrLf & _
                    " AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OPB.PROJ_BRANDPACK_ID IS NULL AND OPB.PLANTATION_ID IS NULL " & vbCrLf & _
                    " AND OPB.BRANDPACK_ID = @BRANDPACK_ID ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@START_PERIODE", SqlDbType.SmallDateTime, VStartDate)
            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
            Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
            SUMPO_QTY = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())

            'Totalkan Discount yang sudah diberikan/generate
            Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " SELECT ISNULL(SUM(MDH.MRKT_DISC_QTY),0) FROM MRKT_DISC_HISTORY MDH INNER JOIN ORDR_OA_BRANDPACK OOB ON OOB.OA_BRANDPACK_ID = MDH.OA_BRANDPACK_ID " & vbCrLf & _
                    " INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_BRANDPACK_ID = OOB.PO_BRANDPACK_ID INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO WHERE OPB.BRANDPACK_ID = @BRANDPACK_ID AND OOB.OA_BRANDPACK_ID != @OA_BRANDPACK_ID AND MDH.SGT_FLAG = 'CA' " & vbCrLf & _
                    " AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND PO.PO_REF_DATE >= @START_PERIODE AND PO.PO_REF_DATE <= @PO_DATE"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID)
            Dim GeneratedDisc As Decimal = Convert.ToDecimal(Me.SqlCom.ExecuteScalar())

            ''check ke database apaka sebelumya discount untuk brandpack yang sama sudah ada ?
            ''bila sebelumnya sudah ada , itu berarti perhitungan recehan historynya sudah di hitung 
            ''perhitunga menjadi langsung oa_original * dispro * term_of_po
            Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " SELECT 1 WHERE EXISTS(SELECT TOP 1 MDH.MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY MDH INNER JOIN ORDR_OA_BRANDPACK OOB ON OOB.OA_BRANDPACK_ID = MDH.OA_BRANDPACK_ID " & vbCrLf & _
                     " INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_BRANDPACK_ID = OOB.PO_BRANDPACK_ID INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = OPB.PO_REF_NO WHERE OPB.BRANDPACK_ID = @BRANDPACK_ID AND OOB.OA_BRANDPACK_ID != @OA_BRANDPACK_ID AND MDH.SGT_FLAG = 'CA' " & vbCrLf & _
                    " AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND PO.PO_REF_DATE >= @START_PERIODE AND PO.PO_REF_DATE <= @PO_DATE ORDER BY PO.PO_REF_DATE DESC);"
            Me.ResetCommandText(CommandType.Text, Query)
            retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

            Dim DatePO As Integer = 0 ' Convert.ToDateTime(tblHistoryPO.Rows(i)("PO_DATE")).Day
            Dim DisproTerm As Decimal = 0
            Dim DiscQty As Decimal = 0
            Dim DiscMustQty As Decimal = 0
            Dim tblHistoryPO As New DataTable("PO_HISTORY")

            If IsDBNull(retval) Or IsNothing(retval) Then
                ''check history PO
                If tblTermsDisc.Rows.Count > 0 Then
                    ''sekarang check setiap date PO nya
                    'Query ngambil setingan terms of po
                    'ambil data-data PO nya
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " SELECT PO.PO_REF_DATE,OPB.BRANDPACK_ID,OOB.OA_ORIGINAL_QTY,DISPRO_TERM = CONVERT(DECIMAL(10,3),0) FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_ORDER_ACCEPTANCE OOA ON OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                            " INNER JOIN ORDR_OA_BRANDPACK OOB ON OOA.OA_ID = OOB.OA_ID INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_BRANDPACK_ID = OOB.PO_BRANDPACK_ID " & vbCrLf & _
                            " AND OPB.PO_REF_NO = PO.PO_REF_NO WHERE PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND OPB.BRANDPACK_ID = @BRANDPACK_ID AND PO.PO_REF_DATE >= @START_PERIODE AND PO.PO_REF_DATE <= @PO_DATE AND PO.PO_REF_NO != @PO_REF_NO ORDER BY PO.PO_REF_DATE DESC ; "
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@START_PERIODE", SqlDbType.SmallDateTime, VStartDate)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25) ' VARCHAR(25),
                    Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                    setDataAdapter(Me.SqlCom).Fill(tblHistoryPO)
                    Me.ClearCommandParameters()
                    If tblHistoryPO.Rows.Count > 0 Then
                        For i As Integer = 0 To tblHistoryPO.Rows.Count - 1
                            DatePO = Convert.ToDateTime(tblHistoryPO.Rows(i)("PO_REF_DATE")).Day
                            'ambil disproTerms
                            DisproTerm = 0
                            For i1 As Integer = 0 To tblTermsDisc.Rows.Count - 1
                                If DatePO >= CInt(tblTermsDisc.Rows(i1)("MAX_TO_DATE")) Then
                                    DisproTerm = Convert.ToDecimal(tblTermsDisc.Rows(i1)("DISCOUNT")) * Dispro
                                    Description &= String.Format("{0:p} x {1:#,##0.000}", DisproTerm, Convert.ToDecimal(tblHistoryPO.Rows(i)("OA_ORIGINAL_QTY")))
                                    If i < tblHistoryPO.Rows.Count - 1 Then
                                        Description &= ", "
                                    End If
                                    Exit For
                                End If
                            Next
                            tblHistoryPO.Rows(i).BeginEdit()
                            tblHistoryPO.Rows(i)("DISPRO_TERM") = DisproTerm
                            tblHistoryPO.Rows(i).EndEdit()
                        Next
                        tblHistoryPO.AcceptChanges()
                        'hitung disc
                        For i As Integer = 0 To tblHistoryPO.Rows.Count - 1 'jika ada history PO maka kan masuk ke loop ini
                            DiscQty += Convert.ToDecimal(tblHistoryPO.Rows(i)("OA_ORIGINAL_QTY") * Convert.ToDecimal(tblHistoryPO.Rows(i)("DISPRO_TERM")))
                        Next
                    End If

                    ''hitung untuk disc current po
                    DatePO = PO_DATE.Day
                    'ambil disproTerms
                    DisproTerm = 0
                    For i1 As Integer = 0 To tblTermsDisc.Rows.Count - 1
                        If DatePO >= CInt(tblTermsDisc.Rows(i1)("MAX_TO_DATE")) Then
                            DisproTerm = Convert.ToDecimal(tblTermsDisc.Rows(i1)("DISCOUNT")) * Dispro
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("{0:p} * {1:#,##0.000}", DisproTerm, OA_ORIGINAL_QTY)
                            Exit For
                        End If
                    Next
                    Description &= ")"
                    DiscQty += DisproTerm * OA_ORIGINAL_QTY
                    DiscMustQty = DiscQty
                Else
                    'langsung saja OA_Original X dispro
                    DiscMustQty = Dispro * SUMPO_QTY
                    Description = String.Format("Discount = {0:p}", Dispro)
                End If
            Else
                If tblTermsDisc.Rows.Count > 0 Then
                    ''hitung untuk disc current po
                    DatePO = PO_DATE.Day
                    'ambil disproTerms
                    DisproTerm = 0
                    For i1 As Integer = 0 To tblTermsDisc.Rows.Count - 1
                        If DatePO >= CInt(tblTermsDisc.Rows(i1)("MAX_TO_DATE")) Then
                            DisproTerm = Convert.ToDecimal(tblTermsDisc.Rows(i1)("DISCOUNT")) * Dispro
                            Description = String.Format("Discount = {0:p}", DisproTerm)
                            Exit For
                        End If
                    Next
                    DiscMustQty = DisproTerm * SUMPO_QTY
                Else
                    'langsung saja OA_Original X dispro
                    DiscMustQty = Dispro * SUMPO_QTY
                    Description = String.Format("Discount = {0:p}", Dispro)
                End If
                DiscQty = DiscMustQty - GeneratedDisc
            End If
            Me.DiscQty.MarketingDiscQty = DiscQty
            If Me.DiscQty.MarketingDiscQty > 0 Then
                'insert ke database
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                       "INSERT INTO MRKT_DISC_HISTORY(MRKT_DISC_HIST_ID,OA_BRANDPACK_ID,PROG_BRANDPACK_DIST_ID,SDS_PERIODE," & vbCrLf & _
                       "MRKT_OA_QTY,MRKT_DISC_PCT,MRKT_DISC_QTY,MRKT_RELEASE_QTY,MRKT_LEFT_QTY,SGT_FLAG,DESCRIPTION,FKCodeCPDAuto,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                       "VALUES(@MRKT_DISC_HIST_ID,@OA_BRANDPACK_ID,@PROG_BRANDPACK_DIST_ID,@SDS_PERIODE,@MRKT_OA_QTY,@MRKT_DISC_PCT," & vbCrLf & _
                       "@MRKT_DISC_QTY,@MRKT_RELEASE_QTY,@MRKT_LEFT_QTY,@SGT_FLAG,@DESCRIPTION,@FKCodeCPDAuto,@CREATE_BY,GETDATE())"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115) ' VARCHAR(66),
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                Me.AddParameter("@PROG_BRANDPACK_DIST_ID", SqlDbType.VarChar, DBNull.Value, 40) ' VARCHAR(35),
                Me.AddParameter("@SDS_PERIODE", SqlDbType.VarChar, Periode, 40) ' VARCHAR(35)
                Me.AddParameter("@MRKT_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) ' INT,
                Me.AddParameter("@MRKT_DISC_PCT", SqlDbType.Decimal, IIf((tblHistoryPO.Rows.Count > 0), 0, Dispro * 100)) ' VARCHAR(6),
                Me.AddParameter("@MRKT_DISC_QTY", SqlDbType.Decimal, DiscQty)
                Me.AddParameter("@MRKT_RELEASE_QTY", SqlDbType.Decimal, 0) '
                Me.AddParameter("@MRKT_LEFT_QTY", SqlDbType.Decimal, DiscQty)
                Me.AddParameter("@SGT_FLAG", SqlDbType.VarChar, "CA", 2) ' CHAR(1),
                Me.AddParameter("@DESCRIPTION", SqlDbType.VarChar, Description, 200)
                Me.AddParameter("@FKCodeCPDAuto", SqlDbType.Int, FkCode)
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                'Me.IsGivenGenerated = True
                message &= "Discount Generated with > 0 qty" & vbCrLf
                Me.GeneratedDiscountGiven += 1
            End If
        End Sub

        Public Sub GenerateSalesGivenDiscount(ByVal OA_BRANDPACK_ID As String, ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, _
                ByVal PO_REF_NO As String, ByVal FLAG As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal PO_DATE As DateTime, ByVal mustCloseConnection As Boolean)
            Try
                Me.GeneratedDiscountGiven = 0 : Dim retval As Object = Nothing
                Dim Message As String = "" : Me.OpenConnection() : Me.BeginTransaction()
                If FLAG = "DN" Then
                    Me.generateDKNDisct(DISTRIBUTOR_ID, PO_REF_NO, PO_DATE, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, FLAG, retval, Message)
                ElseIf FLAG = "CA" Then
                    Me.GenerateCPDAuto(DISTRIBUTOR_ID, PO_REF_NO, PO_DATE, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, FLAG, retval, Message)
                Else
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Generate_Sales_Discount_Stage_1", "")
                    Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Generate_Sales_Discount_Stage_1")
                    End If
                    Me.SqlCom.Transaction = Me.SqlTrans
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                    Me.AddParameter("BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25) ' VARCHAR(25),
                    Me.AddParameter("@FLAG", SqlDbType.VarChar, FLAG, 2) ' VARCHAR(2)
                    Dim tblMrkt As New DataTable("SalesGiven") : tblMrkt.Clear()
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom) : Me.OpenConnection()
                    Me.SqlDat.Fill(tblMrkt) : Me.ClearCommandParameters()
                    If tblMrkt.Rows.Count > 0 Then
                        For i As Integer = 0 To tblMrkt.Rows.Count - 1
                            Select Case FLAG
                                Case "MG"
                                    generateDiscDPRDS(tblMrkt, i, OA_ORIGINAL_QTY, retval, Message)
                                Case "KP"
                                    generatePKPPDisc(tblMrkt, i, DISTRIBUTOR_ID, PO_REF_NO, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, PO_DATE, FLAG, retval, Message)
                                Case "CR"
                                    generateCPRDDisc(tblMrkt, i, DISTRIBUTOR_ID, PO_REF_NO, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, PO_DATE, FLAG, retval, Message)
                                Case "DK"
                                    GenerateDKDisc(tblMrkt, i, DISTRIBUTOR_ID, PO_REF_NO, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, PO_DATE, FLAG, retval, Message)
                                Case "CP"
                                    generateCPDDisc1(tblMrkt, i, DISTRIBUTOR_ID, PO_REF_NO, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, PO_DATE, FLAG, retval, Message)
                                Case "CS"
                                    generateCPDKDisc1(tblMrkt, i, DISTRIBUTOR_ID, PO_REF_NO, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, PO_DATE, FLAG, retval, Message)
                                Case "TD"
                                    generateCPDDisc2(tblMrkt, i, DISTRIBUTOR_ID, PO_REF_NO, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, PO_DATE, FLAG, retval, Message)
                                Case "TS"
                                    generateCPDKDisc2(tblMrkt, i, DISTRIBUTOR_ID, PO_REF_NO, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, PO_DATE, FLAG, retval, Message)
                                Case "CT"
                                    generateCPRMTDisc1(tblMrkt, i, DISTRIBUTOR_ID, PO_REF_NO, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, PO_DATE, FLAG, retval, Message)
                                Case "CD"
                                    generateCPRMTDisc2(tblMrkt, i, DISTRIBUTOR_ID, PO_REF_NO, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, PO_DATE, FLAG, retval, Message)
                                Case "DN"
                                    Me.generateDKNDisct(DISTRIBUTOR_ID, PO_REF_NO, PO_DATE, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, FLAG, retval, Message)
                                Case "CA"
                                    Me.GenerateCPDAuto(DISTRIBUTOR_ID, PO_REF_NO, PO_DATE, BRANDPACK_ID, OA_BRANDPACK_ID, OA_ORIGINAL_QTY, FLAG, retval, Message)
                            End Select
                        Next
                    Else
                        System.Windows.Forms.MessageBox.Show("There's no Sales Program discount.")
                    End If
                End If
                Me.CommiteTransaction()
                If Message <> "" Then
                    System.Windows.Forms.MessageBox.Show(Message, "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                Else
                    System.Windows.Forms.MessageBox.Show("can not calculte discount." & vbCrLf & "System can not collect valid data to calculate")
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub GenerateAgreementGivenDiscount(ByVal OA_BRANDPACK_ID As String, ByVal BRANDPACK_ID As String, ByVal DISTRIBUTOR_ID As String, ByVal PO_REF_NO As String, ByVal OA_ORIGINAL_QTY As Decimal, ByVal mustCloseConnection As Boolean)
            Try
                Dim AGREE_BRANDPACK_ID As String = ""
                Dim AGREE_BRAND_ID As String = ""
                Dim GIVEN_PCT As Object = Nothing
                Dim GIVEN_ID As Object = DBNull.Value
                Me.CreateCommandSql("Usp_Generetate_AGREE_GIVEN_DISCOUNT_STAGE_1", "") 'generating langsung di stored procedure panjang dan pusing
                'maka mesti di bagi dalam beberapa tahap saja
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                Me.AddParameter("PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25) ' VARCHAR(25),

                'Me.AddParameter("@O_AGREE_BRANDPACK_ID", SqlDbType.VarChar, ParameterDirection.Output) ' VARCHAR(39)OUTPUT,
                Me.SqlCom.Parameters.Add("@O_AGREE_BRANDPACK_ID", SqlDbType.VarChar, 39).Direction = ParameterDirection.Output
                'Me.SqlCom.Parameters()("@O_AGREE_BRANDPACK_ID").Size = 39

                'Me.AddParameter("@O_PO_REF_DATE", SqlDbType.DateTime, ParameterDirection.Output) ' DATETIME OUTPUT,
                Me.SqlCom.Parameters.Add("@O_PO_REF_DATE", SqlDbType.SmallDateTime, 0).Direction = ParameterDirection.Output

                'Me.AddParameter("@O_AGREEMENT_NO", SqlDbType.VarChar, ParameterDirection.Output) ' VARCHAR(25)OUTPUT,
                Me.SqlCom.Parameters.Add("@O_AGREEMENT_NO", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output
                'Me.SqlCom.Parameters()("@O_AGREEMENT_NO").Size = 25

                'Me.AddParameter("@O_AGREE_BRAND_ID", SqlDbType.VarChar, ParameterDirection.Output)
                Me.SqlCom.Parameters.Add("@O_AGREE_BRAND_ID", SqlDbType.VarChar, 32).Direction = ParameterDirection.Output
                'Me.SqlCom.Parameters()("@O_AGREE_BRAND_ID").Size = 32
                Me.OpenConnection() : Me.SqlCom.ExecuteNonQuery()

                If (IsNothing(Me.SqlCom.Parameters()("@O_AGREE_BRANDPACK_ID").Value)) Or (IsDBNull(Me.SqlCom.Parameters()("@O_AGREE_BRANDPACK_ID").Value)) Then
                    Throw New System.Exception("Couldn't find Transaction between periods" & vbCrLf & " or Agreement may already be closed")
                End If
                AGREE_BRANDPACK_ID = Me.SqlCom.Parameters()("@O_AGREE_BRANDPACK_ID").Value.ToString()
                AGREE_BRAND_ID = Me.SqlCom.Parameters()("@O_AGREE_BRAND_ID").Value.ToString()
                Dim AGREEMENT_NO As String = Me.SqlCom.Parameters()("@O_AGREEMENT_NO").Value.ToString()
                Dim PO_REF_DATE As Date = CDate(Me.SqlCom.Parameters()("@O_PO_REF_DATE").Value)
                Me.ClearCommandParameters()

                Dim strPO_Ref_Date As String = "" & Month(PO_REF_DATE).ToString() & "/" & Day(PO_REF_DATE).ToString() & "/" & Year(PO_REF_DATE).ToString()
                Dim Query As String = "SET NOCOUNT ON;SELECT TOP 1 AGREE_BRAND_ID FROM GIVEN_STORY WHERE AGREE_BRAND_ID = '" & AGREE_BRAND_ID & _
                                      "' AND START_DATE <= '" & strPO_Ref_Date & "' ORDER BY START_DATE DESC;"
                Dim Res As Object = Nothing : Me.SqlCom.CommandText = "sp_executesql"
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Res = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

                If Not IsNothing(Res) Then
                    Query = "SET NOCOUNT ON;SELECT TOP 1 DISC_PCT,GIVEN_ID,START_DATE FROM GIVEN_STORY WHERE AGREE_BRAND_ID = '" & AGREE_BRAND_ID & _
                           "' AND START_DATE <= '" & strPO_Ref_Date & "' ORDER BY START_DATE DESC;"
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query) : Me.ExecuteReader()
                    While Me.SqlRe.Read()
                        GIVEN_ID = Me.SqlRe("GIVEN_ID") : GIVEN_PCT = Me.SqlRe("DISC_PCT")
                    End While
                    Me.SqlRe.Close() : Me.ClearCommandParameters()
                Else
                    Query = "SET NOCOUNT ON;SELECT GIVEN_DISC_PCT FROM AGREE_BRAND_INCLUDE WHERE AGREE_BRAND_ID = '" & AGREE_BRAND_ID & "';"
                    Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                    GIVEN_PCT = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If

                If IsDBNull(GIVEN_PCT) Or IsNothing(GIVEN_PCT) Then
                    Me.CloseConnection() : Throw New System.Exception("GIVEN % FOR AGREEMENT " & AGREEMENT_NO & " IS NULL")
                End If
                GIVEN_PCT = Convert.ToDecimal(GIVEN_PCT)
                Dim AGREE_DISC_QTY As Decimal = (GIVEN_PCT / 100) * OA_ORIGINAL_QTY 'CInt(RP_Z / PRICE_OA_REF_DATE)
                If AGREE_DISC_QTY <= 0 Then
                    Me.CloseConnection()
                    System.Windows.Forms.MessageBox.Show("0 discount_qty obtained !") '& vbCrLf & "Save data to database!?" & vbCrLf & _
                    '"If you save data,you won't be able to delete again !", "Confirmation") ', Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Return
                End If
                'NOW INSERT INTO AGREE_DISC_QTY BY GQSY_FLAG
                Dim AGREE_DISC_HIST_ID As String = AGREE_BRANDPACK_ID + "G" + PO_REF_NO
                Me.SqlCom.CommandText = "Sp_Insert_AGREE_DISC_HISTORY"
                Me.SqlCom.CommandType = CommandType.StoredProcedure
                'Me.CreateCommandSql("Sp_Insert_AGREE_DISC_HISTORY", "")
                Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, AGREE_DISC_HIST_ID, 115) ' VARCHAR(71),
                Me.AddParameter("@AGREE_BRANDPACK_ID", SqlDbType.VarChar, AGREE_BRANDPACK_ID, 39) ' VARCHAR(39),
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(44),
                Me.AddParameter("@GIVEN_ID", SqlDbType.VarChar, GIVEN_ID, 44)
                Me.AddParameter("@AGREE_OA_QTY", SqlDbType.Decimal, OA_ORIGINAL_QTY) '
                Me.AddParameter("@AGREE_OA_DISC_PCT", SqlDbType.Decimal, Convert.ToDecimal(GIVEN_PCT)) ' VARCHAR(6),
                Me.AddParameter("@AGREE_DISC_QTY", SqlDbType.Decimal, AGREE_DISC_QTY) ' INT,
                Me.AddParameter("@AGREE_RELEASE_QTY", SqlDbType.Decimal, 0) ' INT,
                Me.AddParameter("@AGREE_LEFT_QTY", SqlDbType.Decimal, AGREE_DISC_QTY)
                Me.AddParameter("@GQSY_FLAG", SqlDbType.VarChar, "G", 5) ' VARCHAR(5),
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(30)
                Me.ExecuteNonQuery() : If mustCloseConnection Then : Me.CloseConnection() : End If
                If AGREE_DISC_QTY <= 0 Then
                    System.Windows.Forms.MessageBox.Show("Data generated succesfully !." & vbCrLf & "But has no discount quantity")
                Else
                    System.Windows.Forms.MessageBox.Show("Data generated succesfully.!" & vbCrLf & "To see its detail " & vbCrLf & "Please select brandpack on inserted brandpack")
                End If
                'SAVE IN VARIABLE STRUCTURE TO SAVE INTO DATAVIEW OA_BRANDPACK
                Me.DiscQty.AGreeGivenDiscQTY = AGREE_DISC_QTY
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

        Public Sub getTotalPrice(ByVal DISTRIBUTOR_ID As String, ByVal OA_ID As String, ByVal PO_REF_NO As String)
            Try
                Me.CreateCommandSql("Usp_GetTotal_price_for_DISTRIBUTOR", "")
                Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 32)
                '@OA_ID VARCHAR(30),
                'Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PO_REF_NO, 25) ' VARCHAR(25),
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
                'Me.AddParameter("@O_TOTAL_QTY", SqlDbType.Float, ParameterDirection.Output) ' INT OUTPUT,
                'Me.AddParameter("@O_MAX_DISCOUNT", SqlDbType.Decimal, ParameterDirection.Output) ' INT OUTPUT,
                Me.SqlCom.Parameters.Add("@O_MAX_DISCOUNT", SqlDbType.Decimal, 0).Direction = ParameterDirection.Output
                Me.SqlCom.Parameters()("@O_MAX_DISCOUNT").Scale = 3

                'Me.AddParameter("@O_TOTAL_PRICE", SqlDbType.Decimal, ParameterDirection.Output) ' FLOAT OUTPUT
                Me.SqlCom.Parameters.Add("@O_TOTAL_PRICE", SqlDbType.Decimal, 0).Direction = ParameterDirection.Output
                Me.SqlCom.Parameters()("@O_TOTAL_PRICE").Scale = 2

                Me.OpenConnection()
                Me.SqlCom.ExecuteNonQuery()
                Me.CloseConnection()

                Me.TOTAL_PRICE_DISTRIBUTOR = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_TOTAL_PRICE").Value)
                Me.MAX_DISC_PER_PO = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_MAX_DISCOUNT").Value)
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Function HasGeneratedCPD(ByVal OA_BRANDPACK_ID As String, ByVal flag As String) As Boolean
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT 1 WHERE EXISTS(SELECT TOP 1 MRKT_DISC_HIST_ID FROM MRKT_DISC_HISTORY WHERE OA_BRANDPACK_ID = @OA_BRANDPACK_ID AND SGT_FLAG = @FLAG) ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID)
                Me.AddParameter("@FLAG", SqlDbType.VarChar, flag)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return (CInt(retval) > 0)
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
        End Function
        Private Sub ChekAvailabilityDiscOther(ByVal IDApp As Integer, ByVal ApplyTo As String, ByVal ApplyDate As DateTime, ByVal EndDate As DateTime, ByVal PO_Date As DateTime, ByVal DISTRIBUTOR_ID As String, ByVal BRANDPACK_ID As String, ByVal OAQty As Decimal)
            Dim IsCBD As Boolean = False
            Dim IsODD As Boolean = False
            Dim IsODR As Boolean = False
            Dim IsODK As Boolean = False
            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Availability_Disc_Other")
            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
            Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)

            'Me.AddParameter("@O_DEVIDED_QTY", SqlDbType.Decimal, ParameterDirection.Output)
            Me.SqlCom.Parameters.Add("@O_DEVIDED_QTY", SqlDbType.Decimal).Direction = ParameterDirection.Output
            Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Scale = 3

            'Me.AddParameter("@O_DEVIDE_FACTOR", SqlDbType.Decimal, ParameterDirection.Output)
            Me.SqlCom.Parameters.Add("@O_DEVIDE_FACTOR", SqlDbType.Decimal).Direction = ParameterDirection.Output
            Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Scale = 3

            'Me.AddParameter("@O_UNIT", SqlDbType.VarChar, ParameterDirection.Output) ' VARCHAR(15) OUTPUT,
            Me.SqlCom.Parameters.Add("@O_UNIT", SqlDbType.VarChar, 15).Direction = ParameterDirection.Output
            'Me.SqlCom.Parameters()("@O_UNIT").Size = 15
            Me.SqlCom.Parameters.Add("@O_DD", SqlDbType.Bit).Direction = ParameterDirection.Output
            Me.SqlCom.Parameters.Add("@O_DR", SqlDbType.Bit).Direction = ParameterDirection.Output
            Me.SqlCom.Parameters.Add("@O_CBD", SqlDbType.Bit).Direction = ParameterDirection.Output
            Me.SqlCom.Parameters.Add("@O_DK", SqlDbType.Bit).Direction = ParameterDirection.Output
            Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_Date)
            Me.SqlCom.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            Me.SqlCom.ExecuteNonQuery()
            Dim retval = Me.SqlCom.Parameters("@RETURN_VALUE").Value
            If Not IsNothing(Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Value) Then
                Me.Devide_Factor = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Value)
            Else
                Throw New System.Exception("Devide_Factor has not been set yet!")
            End If
            If Not IsNothing(Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Value) Then
                Me.Devided_Qty = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Value)
            End If
            If Not IsNothing(Me.SqlCom.Parameters()("@O_UNIT").Value) Then
                Me.UNIT = CStr(Me.SqlCom.Parameters()("@O_UNIT").Value)
            End If
            If Not IsNothing(retval) Then
                If CInt(retval) > 0 Then
                    IsCBD = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_CBD").Value)
                    IsODD = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_DD").Value)
                    IsODR = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_DR").Value)
                    IsODK = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_DK").Value)
                    Dim result As Decimal = 0
                    'Query = "SET NOCOUNT ON;" & vbCrLf & _
                    '        " SELECT TOP 1 IDApp,APPLY_TO,APPLY_DATE FROM BRND_DISC_HEADER WHERE APPLY_DATE <= @PODate ORDER BY APPLY_DATE DESC ;"
                    'If IsNothing(Me.SqlCom) Then
                    '    Me.CreateCommandSql("", Query)
                    'Else : Me.ResetCommandText(CommandType.Text, Query)
                    'End If
                    'Me.AddParameter("@PODate", SqlDbType.SmallDateTime, PO_Date)
                    'Me.OpenConnection()

                    'Me.ExecuteReader()
                    'While Me.SqlRe.Read()
                    '    IDApp = Me.SqlRe.GetInt32(0)
                    '    ApplyTo = Me.SqlRe.GetString(1)
                    '    ApplyDate = Me.SqlRe.GetDateTime(2)
                    'End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                    'check brandnya apakah ada dilist program
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                    " DECLARE @V_BRAND_ID VARCHAR(7); " & vbCrLf & _
                    " SET @V_BRAND_ID = (SELECT BRAND_ID FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID) ;" & vbCrLf & _
                    " SELECT BRAND_ID FROM BRND_DISC_LIST_BRAND WHERE BRAND_ID = @V_BRAND_ID AND FKApp = @IDApp ;"
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                    Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)

                    retval = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If IsNothing(retval) Then
                        'Me.CloseConnection()
                        Return
                    End If
                    Dim brandID As String = retval.ToString()
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
                            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                            Me.AddParameter("@APPLY_DATE", SqlDbType.Date, ApplyDate)
                            Me.AddParameter("@END_DATE", SqlDbType.Date, EndDate)
                            Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                            Me.AddParameter("@PODate", SqlDbType.SmallDateTime, PO_Date)
                            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                            retval = Me.SqlCom.ExecuteScalar()
                            If IsNothing(retval) Then
                                'Me.CloseConnection()
                                Me.ClearCommandParameters()
                                Return
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
                            Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                            Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                            Me.AddParameter("@PODate", SqlDbType.SmallDateTime, PO_Date)
                            Me.AddParameter("@END_DATE", SqlDbType.Date, EndDate)
                            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                            retval = Me.SqlCom.ExecuteScalar()
                            If IsNothing(retval) Then
                                Me.CloseConnection() : Me.ClearCommandParameters()
                                Return
                            End If
                    End Select
                    'ambil discount nya
                    Dim OBrandID As Object = Nothing
                    Dim OBrandPackID As Object = Nothing
                    If IsCBD Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT BRAND_ID,BRANDPACK_ID,MoreThanQty,Disc FROM BRND_DISC_PROG WHERE FKApp = @IDApp AND TypeApp = @TypeApp ORDER BY MoreThanQty DESC,Disc DESC;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                        Me.AddParameter("@TypeApp", SqlDbType.VarChar, "CBD")
                        Dim MoreThanQty As Decimal = 0, Disc As Decimal = 0
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()

                            If Not IsDBNull(Me.SqlRe(0)) And Not IsNothing(Me.SqlRe(0)) Then
                                OBrandID = Me.SqlRe.GetString(0)
                            End If
                            If Not IsNothing(Me.SqlRe(1)) And Not IsDBNull(Me.SqlRe(1)) Then
                                OBrandPackID = Me.SqlRe.GetString(1)
                            End If
                            MoreThanQty = Me.SqlRe.GetDecimal(2)
                            Disc = Me.SqlRe.GetDecimal(3)
                            If Not IsNothing(OBrandPackID) And Not IsDBNull(OBrandPackID) Then
                                If OBrandPackID.ToString() = BRANDPACK_ID Then
                                    If OAQty >= MoreThanQty Then
                                        result = OAQty * (Disc / 100)
                                    End If
                                    Exit While
                                End If
                            ElseIf Not IsNothing(OBrandID) And Not IsDBNull(OBrandID) Then
                                If OBrandID.ToString() = brandID Then
                                    If OAQty >= MoreThanQty Then
                                        result = OAQty * (Disc / 100)
                                    End If
                                    Exit While
                                End If
                            End If

                            If OAQty >= MoreThanQty Then
                                result = OAQty * (Disc / 100)
                                Exit While
                            End If
                        End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                    End If
                    If Not Me.IsHasOtherDisc.OCBD Then
                        Me.IsHasOtherDisc.OCBD = result > 0
                    End If
                    result = 0 'reset result
                    If IsODR Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT BRAND_ID,BRANDPACK_ID,MoreThanQty,Disc FROM BRND_DISC_PROG WHERE FKApp = @IDApp AND TypeApp = @TypeApp ORDER BY MoreThanQty DESC,Disc DESC;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                        Me.AddParameter("@TypeApp", SqlDbType.VarChar, "DR")
                        Dim MoreThanQty As Decimal = 0, Disc As Decimal = 0
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            OBrandID = Me.SqlRe(0)
                            OBrandPackID = Me.SqlRe(1)
                            MoreThanQty = Me.SqlRe.GetDecimal(2)
                            Disc = Me.SqlRe.GetDecimal(3)
                            If Not IsNothing(OBrandPackID) And Not IsDBNull(OBrandPackID) Then
                                If OBrandPackID.ToString() = BRANDPACK_ID Then
                                    If OAQty >= MoreThanQty Then
                                        result = OAQty * (Disc / 100)
                                    End If
                                    Exit While
                                End If
                            ElseIf Not IsNothing(OBrandID) And Not IsDBNull(OBrandID) Then
                                If OBrandID.ToString() = brandID Then
                                    If OAQty >= MoreThanQty Then
                                        result = OAQty * (Disc / 100)
                                    End If
                                    Exit While
                                End If
                            Else
                                If OAQty >= MoreThanQty Then
                                    result = OAQty * (Disc / 100)
                                    Me.IsHasOtherDisc.ODR = result > 0
                                    Exit While
                                End If
                            End If

                        End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                    End If
                    If Not Me.IsHasOtherDisc.ODR Then
                        Me.IsHasOtherDisc.ODR = result > 0
                    End If
                    result = 0 'reset result
                    If IsODD Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT BRAND_ID,BRANDPACK_ID,MoreThanQty,Disc FROM BRND_DISC_PROG WHERE FKApp = @IDApp AND TypeApp = @TypeApp ORDER BY MoreThanQty DESC,Disc DESC;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                        Me.AddParameter("@TypeApp", SqlDbType.VarChar, "DD")
                        Dim MoreThanQty As Decimal = 0, Disc As Decimal = 0
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            OBrandID = Me.SqlRe(0)
                            OBrandPackID = Me.SqlRe(1)
                            MoreThanQty = Me.SqlRe.GetDecimal(2)
                            Disc = Me.SqlRe.GetDecimal(3)
                            If Not IsNothing(OBrandPackID) And Not IsDBNull(OBrandPackID) Then
                                If OBrandPackID.ToString() = BRANDPACK_ID Then
                                    If OAQty >= MoreThanQty Then
                                        result = OAQty * (Disc / 100)
                                    End If
                                    Exit While
                                End If
                            ElseIf Not IsNothing(OBrandID) And Not IsDBNull(OBrandID) Then
                                If OBrandID.ToString() = brandID Then
                                    If OAQty >= MoreThanQty Then
                                        result = OAQty * (Disc / 100)
                                    End If
                                    Exit While
                                End If
                            Else
                                If OAQty >= MoreThanQty Then
                                    result = OAQty * (Disc / 100)
                                    Exit While
                                End If
                            End If
                        End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                    End If
                    If Not Me.IsHasOtherDisc.ODD Then
                        Me.IsHasOtherDisc.ODD = result > 0
                    End If
                    result = 0 'reset result
                    If IsODK Then
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT BRAND_ID,BRANDPACK_ID,MoreThanQty,Disc FROM BRND_DISC_PROG WHERE FKApp = @IDApp AND TypeApp = @TypeApp ORDER BY MoreThanQty DESC,Disc DESC;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@IDApp", SqlDbType.Int, IDApp)
                        Me.AddParameter("@TypeApp", SqlDbType.VarChar, "DK")
                        Dim MoreThanQty As Decimal = 0, Disc As Decimal = 0
                        Me.ExecuteReader()
                        While Me.SqlRe.Read()
                            OBrandID = Me.SqlRe(0)
                            OBrandPackID = Me.SqlRe(1)
                            MoreThanQty = Me.SqlRe.GetDecimal(2)
                            Disc = Me.SqlRe.GetDecimal(3)
                            If Not IsNothing(OBrandPackID) And Not IsDBNull(OBrandPackID) Then
                                If OBrandPackID.ToString() = BRANDPACK_ID Then
                                    If OAQty >= MoreThanQty Then
                                        result = OAQty * (Disc / 100)
                                    End If
                                    Exit While
                                End If
                            ElseIf Not IsNothing(OBrandID) And Not IsDBNull(OBrandID) Then
                                If OBrandID.ToString() = brandID Then
                                    If OAQty >= MoreThanQty Then
                                        result = OAQty * (Disc / 100)
                                    End If
                                    Exit While
                                End If
                            Else
                                If OAQty >= MoreThanQty Then
                                    result = OAQty * (Disc / 100)
                                    Exit While
                                End If
                            End If
                        End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                    End If
                    If Not Me.IsHasOtherDisc.ODK Then
                        Me.IsHasOtherDisc.ODK = result > 0
                    End If
                End If
            End If
            Me.ClearCommandParameters()
        End Sub

        Public Sub CheckAvailabilityDisc(ByVal PO_DATE As DateTime, ByVal DISTRIBUTOR_ID As String, ByVal BRANDPACK_ID As String, ByVal OAQTy As Decimal, ByVal SDiscount As SelectedDiscount, ByVal mustCloseConnection As Boolean)
            Try
                Select Case SDiscount
                    Case SelectedDiscount.AgreementDiscount
                        If IsNothing(Me.SqlCom) Then
                            Me.CreateCommandSql("Usp_Check_Availability_Disc_Agreement", "")
                        Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Availability_Disc_Agreement")
                        End If
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)

                        'Me.AddParameter("@O_DEVIDED_QTY", SqlDbType.Decimal, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_DEVIDED_QTY", SqlDbType.Decimal, 0).Direction = ParameterDirection.Output
                        Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Scale = 3

                        'Me.AddParameter("@O_DEVIDE_FACTOR", SqlDbType.Decimal, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_DEVIDE_FACTOR", SqlDbType.Decimal, 0).Direction = ParameterDirection.Output
                        Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Scale = 3

                        'Me.AddParameter("@O_UNIT", SqlDbType.VarChar, ParameterDirection.Output) ' VARCHAR(15) OUTPUT,
                        ' Me.SqlCom.Parameters()("@O_UNIT").Size = 15
                        Me.SqlCom.Parameters.Add("@O_UNIT", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output

                        Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)

                        'Me.AddParameter("@O_COUNTQTYQ1", SqlDbType.Bit, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_COUNTQTYQ1", SqlDbType.Bit, 0).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_COUNTQTYQ2", SqlDbType.Bit, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_COUNTQTYQ2", SqlDbType.Bit, 0).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_COUNTQTYQ3", SqlDbType.Bit, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_COUNTQTYQ3", SqlDbType.Bit, 0).Direction = ParameterDirection.Output
                        'Me.AddParameter("@O_COUNTQTYQ4", SqlDbType.Bit, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_COUNTQTYQ4", SqlDbType.Bit, 0).Direction = ParameterDirection.Output
                        'Me.AddParameter("@O_COUNTQTYS1", SqlDbType.Bit, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_COUNTQTYS1", SqlDbType.Bit, 0).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_COUNTQTYS2", SqlDbType.Bit, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_COUNTQTYS2", SqlDbType.Bit, 0).Direction = ParameterDirection.Output
                        'Me.AddParameter("@O_COUNTQTYY", SqlDbType.Bit, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_COUNTQTYY", SqlDbType.Bit, 0).Direction = ParameterDirection.Output

                        Me.OpenConnection() : Me.SqlCom.ExecuteScalar()
                        Me.IsgeneratedAgreement.Q1 = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTQTYQ1").Value)
                        Me.IsgeneratedAgreement.Q2 = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTQTYQ2").Value)
                        Me.IsgeneratedAgreement.Q3 = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTQTYQ3").Value)
                        Me.IsgeneratedAgreement.Q4 = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTQTYQ4").Value)
                        Me.IsgeneratedAgreement.S1 = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTQTYS1").Value)
                        Me.IsgeneratedAgreement.S2 = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTQTYS2").Value)
                        Me.IsgeneratedAgreement.Y = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTQTYY").Value)

                        If Not IsNothing(Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Value) Then
                            Me.Devide_Factor = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Value)
                        Else
                            Throw New System.Exception("Devide_Factor has not been set yet!")
                        End If
                        If Not IsNothing(Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Value) Then
                            Me.Devided_Qty = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Value)
                        End If
                        'If CInt(Me.SqlCom.Parameters("@O_COUNTCPSD").Value) > 0 Then
                        '    Me.IsHasSales.SCPD = True
                        'End If
                        If Not IsNothing(Me.SqlCom.Parameters()("@O_UNIT").Value) Then
                            Me.UNIT = CStr(Me.SqlCom.Parameters()("@O_UNIT").Value)
                        End If
                        Me.ClearCommandParameters()
                    Case SelectedDiscount.MarketingDiscount
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("Usp_Check_Availability_Disc_Sales", "")
                        Else : Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Availability_Disc_Sales")
                        End If
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)

                        'Me.AddParameter("@O_DEVIDED_QTY", SqlDbType.Decimal, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_DEVIDED_QTY", SqlDbType.Decimal).Direction = ParameterDirection.Output
                        Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Scale = 3

                        'Me.AddParameter("@O_DEVIDE_FACTOR", SqlDbType.Decimal, ParameterDirection.Output)
                        Me.SqlCom.Parameters.Add("@O_DEVIDE_FACTOR", SqlDbType.Decimal).Direction = ParameterDirection.Output
                        Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Scale = 3

                        'Me.AddParameter("@O_UNIT", SqlDbType.VarChar, ParameterDirection.Output) ' VARCHAR(15) OUTPUT,
                        Me.SqlCom.Parameters.Add("@O_UNIT", SqlDbType.VarChar, 15).Direction = ParameterDirection.Output
                        'Me.SqlCom.Parameters()("@O_UNIT").Size = 15

                        Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)

                        'Me.AddParameter("@O_COUNTMG", SqlDbType.Bit, ParameterDirection.Output) ' BIT OUTPUT,
                        Me.SqlCom.Parameters.Add("@O_COUNTMG", SqlDbType.Bit).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_COUNTCPR", SqlDbType.Bit, ParameterDirection.Output) ' BIT OUTPUT,
                        Me.SqlCom.Parameters.Add("@O_COUNTCPR", SqlDbType.Bit).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_COUNTCPD", SqlDbType.Bit, ParameterDirection.Output) ' BIT OUTPUT,
                        Me.SqlCom.Parameters.Add("@O_COUNTCPD", SqlDbType.Bit).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_COUNTCPSD", SqlDbType.Bit, ParameterDirection.Output) ' BIT OUTPUT,
                        Me.SqlCom.Parameters.Add("@O_COUNTCPSD", SqlDbType.Bit).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_COUNTKP", SqlDbType.Bit, ParameterDirection.Output) ' BIT OUTPUT,
                        Me.SqlCom.Parameters.Add("@O_COUNTKP", SqlDbType.Bit).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_COUNTDK", SqlDbType.Bit, ParameterDirection.Output) ' BIT OUTPUT,
                        Me.SqlCom.Parameters.Add("@O_COUNTDK", SqlDbType.Bit).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_CountCPSD_TM", SqlDbType.Bit, ParameterDirection.Output) ' BIT OUTPUT,
                        Me.SqlCom.Parameters.Add("@O_CountCPSD_TM", SqlDbType.Bit).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_CountCPD_TM", SqlDbType.Bit, ParameterDirection.Output) ' BIT OUTPUT
                        Me.SqlCom.Parameters.Add("@O_CountCPD_TM", SqlDbType.Bit).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_CountCPMRT_TM", SqlDbType.Bit, ParameterDirection.Output) ' BIT OUTPUT,
                        Me.SqlCom.Parameters.Add("@O_CountCPMRT_TM", SqlDbType.Bit).Direction = ParameterDirection.Output

                        'Me.AddParameter("@O_CountCPMRT_D", SqlDbType.Bit, ParameterDirection.Output) ' BIT OUTPUT
                        Me.SqlCom.Parameters.Add("@O_CountCPMRT_D", SqlDbType.Bit).Direction = ParameterDirection.Output
                        Me.OpenConnection() : Me.SqlCom.ExecuteScalar()

                        Me.IsHasSales.CPD = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTCPD").Value)
                        Me.IsHasSales.SCPD = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTCPSD").Value)
                        Me.IsHasSales.CPD_TM_Distributor = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_CountCPD_TM").Value)
                        Me.IsHasSales.CPDS_TM_Distributor = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_CountCPSD_TM").Value)
                        Me.IsHasSales.CPR = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTCPR").Value)
                        Me.IsHasSales.DK = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTDK").Value)
                        Me.IsHasSales.MG = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTMG").Value)
                        Me.IsHasSales.PKPP = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTKP").Value)
                        Me.IsHasSales.CPMRT_DIST = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_CountCPMRT_D").Value)
                        Me.IsHasSales.CPMRT_DIST_TM = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_CountCPMRT_TM").Value)
                        If Not IsNothing(Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Value) Then
                            Me.Devide_Factor = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Value)
                        Else
                            Throw New System.Exception("Devide_Factor has not been set yet!")
                        End If
                        If Not IsNothing(Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Value) Then
                            Me.Devided_Qty = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Value)
                        End If
                        'If CInt(Me.SqlCom.Parameters("@O_COUNTCPSD").Value) > 0 Then
                        '    Me.IsHasSales.SCPD = True
                        'End If
                        If Not IsNothing(Me.SqlCom.Parameters()("@O_UNIT").Value) Then
                            Me.UNIT = CStr(Me.SqlCom.Parameters()("@O_UNIT").Value)
                        End If
                        Me.ClearCommandParameters()
                        ''check CPDAuto
                        Query = "SET NOCOUNT ON ;" & vbCrLf & _
                                " SELECT  1 WHERE EXISTS( SELECT SCP.BRANDPACK_ID FROM SALES_CPDAUTO_PRODUCT SCP INNER JOIN SALES_CPDAUTO_HEADER SCH ON SCH.IDApp = SCP.FKCode " & vbCrLf & _
                                " WHERE SCH.START_PERIODE <= @PO_DATE AND SCH.END_PERIODE >= @PO_DATE AND SCP.BRANDPACK_ID = @BRANDPACK_ID);"
                        Me.ResetCommandText(CommandType.Text, Query)
                        AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                        AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                        Dim retval As Object = Me.SqlCom.ExecuteScalar()
                        If Not IsNothing(retval) And Not IsDBNull(retval) Then
                            Me.IsHasSales.CPDAuto = (CInt(retval) > 0)
                        End If
                        Me.ClearCommandParameters()
                    Case SelectedDiscount.None
                    Case SelectedDiscount.OtherDiscount
                        Me.IsHasOtherDisc.OCBD = False
                        Me.IsHasOtherDisc.ODD = False
                        Me.IsHasOtherDisc.ODR = False
                        Me.IsHasOtherDisc.ODK = False

                        ''check program2 apakah lebih dari pada satu
                        ''GET StartDate PKD
                        Query = "SET NOCOUNT ON;" & vbCrLf & _
                                " SELECT TOP 1 AA.START_DATE, AA.END_DATE FROM AGREE_AGREEMENT AA  " & vbCrLf & _
                                " INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN AGREE_BRANDPACK_INCLUDE ABP ON ABP.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                " WHERE AA.END_DATE >= @PO_DATE AND AA.START_DATE <= @PO_DATE AND DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ABP.BRANDPACK_ID = @BRANDPACK_ID ;"
                        If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                        Else : Me.ResetCommandText(CommandType.Text, Query)
                        End If
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                        Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                        Dim StartDatePKD As Object = Nothing, EndDatePKD As Object = Nothing
                        Me.OpenConnection()
                        Me.SqlRe = Me.SqlCom.ExecuteReader()
                        While Me.SqlRe.Read()
                            StartDatePKD = SqlRe.GetDateTime(0)
                            EndDatePKD = SqlRe.GetDateTime(1)
                        End While : Me.SqlRe.Close() : Me.ClearCommandParameters()


                        Dim IDApp As Integer = 0, ApplyTo As String = "AL", ApplyDate As DateTime = DateTime.Now, EndDate As DateTime = DateTime.Now
                        Query = "SET NOCOUNT ON; " & vbCrLf & _
                                  " SELECT IDApp,APPLY_TO,APPLY_DATE,END_DATE FROM BRND_DISC_HEADER WHERE APPLY_DATE <= @PO_DATE  AND END_DATE >= @PO_DATE " & vbCrLf & _
                                  " AND APPLY_DATE >= @START_DATE_PKD AND END_DATE <= @END_DATE_PKD ORDER BY APPLY_DATE DESC ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@START_DATE_PKD", SqlDbType.DateTime, StartDatePKD)
                        Me.AddParameter("@END_DATE_PKD", SqlDbType.DateTime, EndDatePKD)
                        Me.AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                        Dim tbl As New DataTable("T_Temp")
                        setDataAdapter(Me.SqlCom).Fill(tbl) : Me.ClearCommandParameters()
                        If tbl.Rows.Count > 0 Then
                            For i As Integer = 0 To tbl.Rows.Count - 1
                                ''ambil data OCBD ODD ODR
                                IDApp = CInt(tbl.Rows(i)("IDApp"))
                                ApplyTo = CStr(tbl.Rows(i)("APPLY_TO"))
                                ApplyDate = Convert.ToDateTime(tbl.Rows(i)("APPLY_DATE"))
                                EndDate = Convert.ToDateTime(tbl.Rows(i)("END_DATE"))
                                Me.ChekAvailabilityDiscOther(IDApp, ApplyTo, ApplyDate, EndDate, PO_DATE, DISTRIBUTOR_ID, BRANDPACK_ID, OAQTy)
                                If Me.IsHasOtherDisc.OCBD = True And Me.IsHasOtherDisc.ODD = True And Me.IsHasOtherDisc.ODR = True And Me.IsHasOtherDisc.ODK = True Then
                                    Exit For
                                End If
                            Next
                        Else
                            ''get devide factor
                            Query = "DECLARE @V_DEVIDED_QTY DECIMAL(18,3),@V_DEVIDE_FACTOR DECIMAL(18,3),@V_UNIT VARCHAR(100);" & vbCrLf & _
                                    " SET @V_DEVIDED_QTY = (SELECT DEVIDED_QUANTITY FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID ); " & vbCrLf & _
                                    " SET @V_DEVIDE_FACTOR = (SELECT TOP 1 DEVIDE_FACTOR FROM BRND_PACK WHERE PACK_ID = (SELECT TOP 1 PACK_ID FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID)); " & vbCrLf & _
                                    " SET @V_UNIT = (SELECT UNIT FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID ); " & vbCrLf & _
                                    " SELECT DEVIDED_QTY = @V_DEVIDED_QTY,DEVIDE_FACTOR = @V_DEVIDE_FACTOR,UNIT = @V_UNIT ;"
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                            Me.SqlRe = Me.SqlCom.ExecuteReader()
                            While Me.SqlRe.Read()
                                Me.Devided_Qty = Me.SqlRe.GetDecimal(0)
                                Me.Devide_Factor = Me.SqlRe.GetDecimal(1)
                                Me.UNIT = Me.SqlRe.GetString(2)
                            End While : Me.SqlRe.Close() : Me.ClearCommandParameters()
                        End If

                    Case SelectedDiscount.ProjectDiscount
                End Select

                Me.ClearCommandParameters()
                If SDiscount = SelectedDiscount.MarketingDiscount Then
                    'Me.IsHasSales.SCPD = Convert.ToBoolean(Me.SqlCom.Parameters()("@O_COUNTCPSD").Value)
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            " DECLARE @V_BRAND_ID VARCHAR(7) ;" & vbCrLf & _
                            " SET @V_BRAND_ID = (SELECT TOP 1 BRAND_ID FROM BRND_BRANDPACK WHERE BRANDPACK_ID = @BRANDPACK_ID) ;" & vbCrLf & _
                            " SELECT TOP 1 BRAND_ID FROM SALES_DKN_PROGRESSIVE SDP INNER JOIN SALES_DKN_SCHEMA SDS ON SDS.IDApp = SDP.FKApp_SDS WHERE BRAND_ID = @V_BRAND_ID " & vbCrLf & _
                            "           AND (SDS.START_DATE <= @PO_DATE AND SDS.END_DATE >= @PO_DATE) ;"
                    ResetCommandText(CommandType.Text, Query)
                    AddParameter("@PO_DATE", SqlDbType.SmallDateTime, PO_DATE)
                    AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14)
                    Dim retval As Object = Me.SqlCom.ExecuteScalar()
                    Me.IsHasSales.DK_N = ((Not IsNothing(retval)) And (Not IsDBNull(retval)))
                    Me.ClearCommandParameters()
                End If
                If mustCloseConnection Then : Me.CloseConnection() : End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        'Public Sub CheckAvailabilityDiscSalesAgreement(ByVal OA_ID As String, ByVal PO_DATE As Object, _
        '    ByVal DISTRIBUTOR_ID As String, ByVal BRANDPACK_ID As String)
        '    Try
        '        Me.CreateCommandSql("Usp_Check_Availability_Disc_Qty_Sales_Agreement", "")
        '        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10) ' VARCHAR(10),
        '        Me.AddParameter("@BRANDPACK_ID", SqlDbType.VarChar, BRANDPACK_ID, 14) ' VARCHAR(14),
        '        Me.AddParameter("@OA_ID", SqlDbType.VarChar, OA_ID, 32) ' VARCHAR(30),
        '        Me.AddParameter("@PO_DATE", SqlDbType.DateTime, PO_DATE) ' DATETIME,

        '        Me.AddParameter("@DISC_AGREE_FROM", SqlDbType.VarChar, NufarmBussinesRules.SharedClass.DISC_AGREE_FROM, 20)
        '        Me.AddParameter("@O_COUNTQTYQ1", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTQTYQ2", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTQTYQ3", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTQTYQ4", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTQTYS1", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTQTYS2", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTQTYY", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTMG", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTCPR", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTCPSD", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTCPD", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTKP", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTDK", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNLEFQTYMS", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTLEFTQTYMT", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT,
        '        Me.AddParameter("@O_COUNTPROJECT", SqlDbType.Int, ParameterDirection.Output) ' INT OUTPUT
        '        Me.AddParameter("@O_DEVIDED_QTY", SqlDbType.Decimal, ParameterDirection.Output) 'DECIMAL OUTPUT,
        '        Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Scale = 3
        '        'Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Size = 20
        '        Me.AddParameter("@O_DEVIDE_FACTOR", SqlDbType.Decimal, ParameterDirection.Output) ' DECIMAL OUTPUT,
        '        Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Scale = 3
        '        Me.AddParameter("@O_UNIT", SqlDbType.VarChar, ParameterDirection.Output) ' VARCHAR(15) OUTPUT
        '        Me.SqlCom.Parameters()("@O_UNIT").Size = 15 : Me.OpenConnection()
        '        Me.SqlCom.ExecuteNonQuery() : Me.CloseConnection()
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTQTYQ1").Value) > 0 Then
        '            Me.IsgeneratedAgreement.Q1 = True
        '        Else
        '            Me.IsgeneratedAgreement.Q1 = False
        '        End If
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTQTYQ2").Value) > 0 Then
        '            Me.IsgeneratedAgreement.Q2 = True
        '        Else
        '            Me.IsgeneratedAgreement.Q2 = False
        '        End If
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTQTYQ3").Value) > 0 Then
        '            Me.IsgeneratedAgreement.Q3 = True
        '        Else
        '            Me.IsgeneratedAgreement.Q3 = False
        '        End If
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTQTYQ4").Value) > 0 Then
        '            Me.IsgeneratedAgreement.Q4 = True
        '        Else
        '            Me.IsgeneratedAgreement.Q4 = False
        '        End If
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTQTYS1").Value) > 0 Then
        '            Me.IsgeneratedAgreement.S1 = True
        '        Else
        '            Me.IsgeneratedAgreement.S1 = False
        '        End If
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTQTYS2").Value) > 0 Then
        '            Me.IsgeneratedAgreement.S2 = True
        '        Else
        '            Me.IsgeneratedAgreement.S2 = False
        '        End If
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTQTYY").Value) > 0 Then
        '            Me.IsgeneratedAgreement.Y = True
        '        Else
        '            Me.IsgeneratedAgreement.Y = False
        '        End If
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTMG").Value) > 0 Then
        '            Me.IsHasSales.MG = True
        '        Else
        '            Me.IsHasSales.MG = False
        '        End If
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTCPR").Value) > 0 Then
        '            Me.IsHasSales.CPR = True
        '        Else
        '            Me.IsHasSales.CPR = False
        '        End If
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTCPD").Value) > 0 Then
        '            Me.IsHasSales.CPD = True
        '        Else
        '            Me.IsHasSales.CPD = False
        '        End If
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTKP").Value) > 0 Then
        '            Me.IsHasSales.PKPP = True
        '        Else
        '            Me.IsHasSales.PKPP = False
        '        End If
        '        If CInt(Me.SqlCom.Parameters()("@O_COUNTDK").Value) > 0 Then
        '            Me.IsHasSales.DK = True
        '        Else
        '            Me.IsHasSales.DK = False
        '        End If
        '        If Not IsNothing(Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Value) Then
        '            Me.Devide_Factor = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_DEVIDE_FACTOR").Value)
        '        Else
        '            Throw New System.Exception("Devide_Factor has not been set yet!")
        '        End If
        '        If Not IsNothing(Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Value) Then
        '            Me.Devided_Qty = Convert.ToDecimal(Me.SqlCom.Parameters()("@O_DEVIDED_QTY").Value)
        '        End If
        '        If CInt(Me.SqlCom.Parameters("@O_COUNTCPSD").Value) > 0 Then
        '            Me.IsHasSales.SCPD = True
        '        End If
        '        If Not IsNothing(Me.SqlCom.Parameters()("@O_UNIT").Value) Then
        '            Me.UNIT = CStr(Me.SqlCom.Parameters()("@O_UNIT").Value)
        '        End If
        '        Me.ClearCommandParameters()
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        'End Sub

        Public Sub FixUnmatchedDiscount(ByVal OA_BRANDPACK_ID As String)
            Try

                Me.CreateCommandSql("Usp_Check_Sum_Mathching_Disc_Qty", "")
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Agreement_Discount", 20) ' VARCHAR(20)
                Me.OpenConnection()
                Me.BeginTransaction()
                Me.ExecuteNonQuery()

                Me.SqlCom.CommandText = "Usp_Check_Sum_Mathching_Disc_Qty"
                Me.SqlCom.CommandType = CommandType.StoredProcedure
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Sales_Discount", 20) ' VARCHAR(20)
                Me.SqlCom.ExecuteNonQuery()

                Me.ClearCommandParameters()
                Me.SqlCom.CommandText = "Usp_Check_Sum_Mathching_Disc_Qty"
                Me.SqlCom.CommandType = CommandType.StoredProcedure
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Other_Discount", 20) ' VARCHAR(20)
                Me.SqlCom.ExecuteNonQuery()


                Me.ClearCommandParameters()
                Me.SqlCom.CommandText = "Usp_Check_Sum_Mathching_Disc_Qty"
                Me.SqlCom.CommandType = CommandType.StoredProcedure
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                Me.AddParameter("@DISCOUNT_TYPE", SqlDbType.VarChar, "Project_Discount", 20) ' VARCHAR(20)
                Me.SqlCom.ExecuteNonQuery() : Me.ClearCommandParameters()

                Me.CommiteTransaction()
                Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

#End Region


#End Region

#Region " REMAINDING QTY"
        Public Function IsExistedOARMID(ByVal OA_BRANDPACK_ID As String, Optional ByVal AGREE_DISC_HIST_ID As String = "", _
                Optional ByVal BRND_B_S_ID As String = "", Optional ByVal MRKT_DISC_HIST_ID As String = "", Optional ByVal MRKT_M_S_ID As String = "", _
                Optional ByVal PROJ_DISC_HIST_ID As String = "") As Boolean
            Try
                If Not AGREE_DISC_HIST_ID <> "" Then
                    If Not IsNothing(Me.ExecuteScalar("", "SELECT OA_RM_ID FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID '" & OA_BRANDPACK_ID & _
                    "' AND AGREE_DISC_HIST_ID = '" & AGREE_DISC_HIST_ID & "'")) Then
                        Return True
                    End If
                ElseIf BRND_B_S_ID <> "" Then
                    If Not IsNothing(Me.ExecuteScalar("", "SELECT OA_RM_ID FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID  '" & OA_BRANDPACK_ID & _
                     "' AND BRND_B_S_ID = '" & BRND_B_S_ID & "'")) Then
                        Return True
                    End If
                ElseIf MRKT_DISC_HIST_ID <> "" Then
                    If Not IsNothing(Me.ExecuteScalar("", "SELECT OA_RM_ID FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID  '" & OA_BRANDPACK_ID & _
                     "' AND MRKT_DISC_HIST_ID = '" & MRKT_DISC_HIST_ID & "'")) Then
                        Return True
                    End If
                ElseIf MRKT_M_S_ID <> "" Then
                    If Not IsNothing(Me.ExecuteScalar("", "SELECT OA_RM_ID FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID  '" & OA_BRANDPACK_ID & _
                     "' AND MRKT_M_S_ID = '" & MRKT_M_S_ID & "'")) Then
                        Return True
                    End If
                ElseIf Not PROJ_DISC_HIST_ID <> "" Then
                    If Not IsNothing(Me.ExecuteScalar("", "SELECT OA_RM_ID FROM ORDR_OA_REMAINDING WHERE OA_BRANDPACK_ID  '" & OA_BRANDPACK_ID & _
                        "' AND PROJ_DISC_HIST__ID = '" & PROJ_DISC_HIST_ID & "'")) Then
                        Return True
                    End If
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' Insert RemainAdjustment khusus dari PKD dan bila ada dengan adjustmentnya
        ''' </summary>
        ''' <param name="isPendingTrans"></param>
        ''' <param name="EndTrans">is ending transaction</param>
        ''' <param name="PrimaryIdentity">OA_BRANDPACK_DISC_ID</param>
        ''' <remarks></remarks>
        Public Sub InsertAdjustment(ByVal IsInsertRemainding As Boolean, ByVal isPendingTrans As Boolean, ByVal EndTrans As Boolean, ByVal PrimaryIdentity As Integer)
            Try
                Dim InsertedRows() As DataRow = Nothing
                If IsInsertRemainding Then
                    'insert Remainding
                    Query = "SET NOCOUNT ON;" & vbCrLf & _
                            " IF ((@ACHIEVEMENT_BRANDPACK_ID IS NOT NULL) AND (@ACHIEVEMENT_BRANDPACK_ID != '')) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "   INSERT INTO ORDR_OA_REMAINDING(OA_BRANDPACK_ID,ACHIEVEMENT_BRANDPACK_ID,FLAG, QTY, LEFT_QTY, CREATE_BY, CREATE_DATE) " & vbCrLf & _
                            "   VALUES(@OA_BRANDPACK_ID,@ACHIEVEMENT_BRANDPACK_ID, @FLAG,@QTY, @LEFT_QTY, @CREATE_BY, CONVERT(VARCHAR(100),GETDATE(),101)) ;" & vbCrLf & _
                            " END " & vbCrLf & _
                            " ELSE " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            "  INSERT INTO ORDR_OA_REMAINDING(OA_BRANDPACK_ID,FLAG, QTY, LEFT_QTY, CREATE_BY, CREATE_DATE, FKCodeAdjust) " & vbCrLf & _
                            "  VALUES(@OA_BRANDPACK_ID, @FLAG,@QTY, @LEFT_QTY, @CREATE_BY, CONVERT(VARCHAR(100),GETDATE(),101), @FKCodeAdjust) ;" & vbCrLf & _
                            " END "
                    If IsNothing(Me.SqlCom) Then
                        Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If

                    If Not isPendingTrans Then
                        Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                    End If
                    InsertedRows = Me.OA_Remainding.tblAdjustment.Select("IsRemain = " & True, "", DataViewRowState.Added)
                    If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter() : End If
                    SqlDat.AcceptChangesDuringUpdate = True
                    Me.SqlCom.Parameters.Add("@OA_BRANDPACK_ID", SqlDbType.VarChar, 75).Value = Me.OA_Remainding.OA_BRANDPACK_ID
                    Me.SqlCom.Parameters.Add("@QTY", SqlDbType.Decimal, 0, "ReleaseQty")
                    Me.SqlCom.Parameters.Add("@LEFT_QTY", SqlDbType.Decimal, 0, "ReleaseQty")
                    Me.SqlCom.Parameters.Add("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, 70, "ABID")
                    Me.SqlCom.Parameters.Add("@CREATE_BY", SqlDbType.VarChar, 70).Value = NufarmBussinesRules.User.UserLogin.UserName
                    Me.SqlCom.Parameters.Add("@FKCodeAdjust", SqlDbType.Int, 0, "IDApp")
                    Me.SqlCom.Parameters.Add("FLAG", SqlDbType.VarChar, 5).Value = Me.OA_Remainding.FLAG
                    Me.SqlDat.InsertCommand = Me.SqlCom
                    SqlDat.Update(InsertedRows) : Me.SqlDat.InsertCommand = Nothing : Me.ClearCommandParameters()
                End If

                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If

                If Not isPendingTrans Then
                    Me.OpenConnection()
                    If IsNothing(Me.SqlTrans) Then : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans : End If
                End If

                'InsertedRows = Me.OA_Remainding.tblAdjustment.Select("", "", DataViewRowState.Added)
                If IsNothing(Me.SqlDat) Then : Me.SqlDat = New SqlDataAdapter() : End If
                SqlDat.AcceptChangesDuringUpdate = False
                ''Insert ADJUSTMENT_TRANS
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " INSERT INTO ADJUSTMENT_TRANS (PO_REF_NO, FKApp, OA_BRANDPACK_DISC_ID, ADJ_DISC_QTY, CreatedBy, CreatedDate) " & vbCrLf & _
                        " VALUES(@PO_REF_NO, @FKApp, @OA_BRANDPACK_DISC_ID, @ADJ_DISC_QTY, @CreatedBy,CONVERT(VARCHAR(100),GETDATE(),101)) ;"
                InsertedRows = Me.OA_Remainding.tblAdjustment.Select("ABID = ''", "", DataViewRowState.Added)
                If InsertedRows.Length > 0 Then
                    Me.ResetCommandText(CommandType.Text, Query)
                    Me.SqlCom.Parameters.Add("@PO_REF_NO", SqlDbType.VarChar, 25, "PO_REF_NO")
                    Me.SqlCom.Parameters.Add("@FKApp", SqlDbType.Int, 0, "IDApp")
                    Me.SqlCom.Parameters.Add("@OA_BRANDPACK_DISC_ID", SqlDbType.Int).Value = PrimaryIdentity
                    Me.SqlCom.Parameters.Add("@ADJ_DISC_QTY", SqlDbType.Decimal, 0, "ReleaseQty")
                    Me.SqlCom.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100).Value = NufarmBussinesRules.User.UserLogin.UserName
                    Me.SqlDat.InsertCommand = Me.SqlCom
                    Me.SqlDat.Update(InsertedRows) : Me.ClearCommandParameters()
                    Me.SqlDat.InsertCommand = Nothing
                End If

                ''UPDATE AcruedDetail
                Dim DPDRows() As DataRow = Me.OA_Remainding.tblAdjustment.Select("ABID <> ''", "", DataViewRowState.Added)
                If DPDRows.Length > 0 Then
                    Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Matching_Disc_Qty_Agreement_Saving")
                    Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, DBNull.Value)
                    Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, DPDRows(0)("ABID"), 70) ' VARCHAR(45),
                    Me.AddParameter("@DISC_FROM", SqlDbType.VarChar, "Invoice", 20) ' VARCH
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End If
                ''update Saldo Adjustment
                Dim AdjustRows() As DataRow = Me.OA_Remainding.tblAdjustment.Select("ABID = ''")
                If AdjustRows.Length > 0 Then

                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "   IF (@FKApp IS NOT NULL) " & vbCrLf & _
                            " BEGIN " & vbCrLf & _
                            " DECLARE @V_LEFT_QTY DECIMAL(18,3),@V_DISC_QTY DECIMAL(18,3),@SUM_RELEASE_QTY DECIMAL(18,3) ;" & vbCrLf & _
                            " SET @V_DISC_QTY = (SELECT ISNULL(QTY,0) FROM ADJUSTMENT_DPD WHERE IDApp = @FKApp ); " & vbCrLf & _
                            " SET @SUM_RELEASE_QTY = (SELECT ISNULL(SUM(ADJ_DISC_QTY),0) FROM ADJUSTMENT_TRANS WHERE FKApp = @FKApp ) + (SELECT ISNULL(SUM(QTY),0) FROM ORDR_OA_REMAINDING WHERE FkCodeAdjust = @FKApp); " & vbCrLf & _
                            " UPDATE ADJUSTMENT_DPD SET RELEASE_QTY = @SUM_RELEASE_QTY,LEFT_QTY = (@V_DISC_QTY - @SUM_RELEASE_QTY),ModifiedBy  = @ModifiedBy, ModifiedDate = GETDATE() WHERE IDApp = @FKApp ;" & vbCrLf & _
                            " END "
                    Me.ResetCommandText(CommandType.Text, Query)
                    For i As Integer = 0 To AdjustRows.Length - 1
                        Me.AddParameter("@FKApp", SqlDbType.Int, AdjustRows(i)("IDApp"))
                        Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Next
                End If
                If isPendingTrans Then
                    If EndTrans Then
                        Me.CommiteTransaction()
                    End If
                End If
                Me.SqlDat.InsertCommand = Nothing
                Me.SqlDat.UpdateCommand = Nothing
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
        Public Sub InsertOARemainding(ByVal SDiscount As SelectedDiscount, ByVal LEFT_QTY As Decimal, ByVal FLAG As String, ByVal BRANDPACK_ID As String, _
        Optional ByVal AGREE_DISC_HIST_ID As String = "", Optional ByVal MRKT_DISC_HIST_ID As String = "", Optional ByVal MRKT_M_S_ID As String = "" _
        , Optional ByVal PROJ_DISC_HIST_ID As String = "", Optional ByVal IsOtherDiscount As Boolean = False, Optional ByVal OA_BRANDPACK_ID As String = "", Optional ByVal MustCommitTrans As Boolean = True)
            Try
                'INSERT OA_REMAINDING
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                        "INSERT INTO ORDR_OA_REMAINDING(OA_BRANDPACK_ID,AGREE_DISC_HIST_ID,BRND_B_S_ID,ACHIEVEMENT_BRANDPACK_ID,MRKT_DISC_HIST_ID," & vbCrLf & _
                        "MRKT_M_S_ID,PROJ_DISC_HIST_ID,FLAG,QTY,RELEASE_QTY,LEFT_QTY,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                        "VALUES(@OA_BRANDPACK_ID,@AGREE_DISC_HIST_ID,@BRND_B_S_ID,@ACHIEVEMENT_BRANDPACK_ID,@MRKT_DISC_HIST_ID,@MRKT_M_S_ID," & vbCrLf & _
                        "@PROJ_DISC_HIST_ID,@FLAG,@QTY,@RELEASE_QTY,@LEFT_QTY,@CREATE_BY,GETDATE())"
                Me.ResetCommandText(CommandType.Text, Query)
                If Not IsNothing(Me.SqlCom) Then : Me.ResetCommandText(CommandType.Text, Query)
                Else : Me.CreateCommandSql("", Query)
                End If
                Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_Remainding.AGREE_DISC_HIST_ID, 115) ' VARCHAR(110),
                Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, Me.OA_Remainding.BRND_B_S_ID, 60) ' VARCHAR(60),
                Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_Remainding.MRKT_DISC_HISt_ID, 115) ' VARCHAR(110),
                Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, Me.OA_Remainding.MRKT_M_S_ID, 60) ' VARCHAR(60),
                Me.AddParameter("@PROJ_DISC_HIST_ID", SqlDbType.VarChar, OA_Remainding.PROJ_DISC_HIST_ID, 66) ' VARCHAR(66),
                Me.AddParameter("@FLAG", SqlDbType.VarChar, FLAG, 5) ' VARCHAR(5),
                Me.AddParameter("@OA_BRANDPACK_ID", SqlDbType.VarChar, OA_BRANDPACK_ID, 75) ' VARCHAR(70),
                Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_Remainding.ACHIEVMENT_BRANDPACK_ID, 70)
                Me.AddParameter("@QTY", SqlDbType.Decimal, LEFT_QTY)
                Me.AddParameter("@RELEASE_QTY", SqlDbType.Decimal, 0) ' CObj("0"), 13)
                Me.AddParameter("@LEFT_QTY", SqlDbType.Decimal, LEFT_QTY)
                Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50) ' VARCHAR(50)
                Me.OpenConnection()
                If Not MustCommitTrans Then
                    Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                End If
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                'JADIKAN original decimal value dulu
                Select Case SDiscount
                    Case SelectedDiscount.AgreementDiscount
                        If Not IsDBNull(Me.OA_Remainding.AGREE_DISC_HIST_ID) Then
                            'INSERT DATA
                            Me.SqlCom.CommandText = "Usp_Check_Sum_Disc_Qty_AGREE_DISC_HIST_ID"
                            Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_Remainding.AGREE_DISC_HIST_ID, 115)
                        Else
                            Me.SqlCom.CommandText = "Usp_Check_Sum_Matching_Disc_Qty_Agreement_Saving"
                            Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, Me.OA_Remainding.BRND_B_S_ID, 60)
                            Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, Me.OA_Remainding.ACHIEVMENT_BRANDPACK_ID, 70) ' VARCHAR(45),
                            Me.AddParameter("@DISC_FROM", SqlDbType.VarChar, NufarmBussinesRules.SharedClass.DISC_AGREE_FROM, 20)
                        End If
                        Me.SqlCom.CommandType = CommandType.StoredProcedure
                    Case SelectedDiscount.MarketingDiscount
                        If Not IsDBNull(Me.OA_Remainding.MRKT_DISC_HISt_ID) Then
                            Me.SqlCom.CommandText = "Usp_Check_Sum_Disc_Qty_MRKT_DISC_HISTORY"
                            Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, Me.OA_Remainding.MRKT_DISC_HISt_ID, 115)
                        ElseIf Not IsDBNull(Me.OA_Remainding.MRKT_M_S_ID) Then
                            Me.SqlCom.CommandText = "Usp_Check_Sum_Matching_Disc_Qty_MRKT_M_S_ID"
                            Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, Me.OA_Remainding.MRKT_M_S_ID, 60)
                        End If
                        Me.SqlCom.CommandType = CommandType.StoredProcedure
                    Case SelectedDiscount.OtherDiscount

                    Case SelectedDiscount.ProjectDiscount
                        Dim Query As String = "SET NOCOUNT ON;UPDATE PROJ_DISC_HISTORY SET PROJ_LEFT_QTY = CAST((SELECT PROJ_LEFT_QTY FROM PROJ_DISC_HISTORY" & _
                                              " WHERE PROJ_DISC_HIST_ID = '" & OA_Remainding.PROJ_DISC_HIST_ID & "') AS DECIMAL(10,3)) - " & LEFT_QTY & ",PROJ_RELEASE_QTY =" & _
                                              " CAST((SELECT PROJ_RELEASE_QTY FROM PROJ_DISC_HISTORY WHERE PROJ_DISC_HIST_ID = '" & OA_Remainding.PROJ_DISC_HIST_ID & "') AS DECIMAL(10,3)) + " & _
                                              LEFT_QTY & ",MODIFY_BY = '" & NufarmBussinesRules.User.UserLogin.UserName & "',MODIFY_DATE = GETDATE() " & _
                                              " WHERE PROJ_DISC_HIST_ID = '" & OA_Remainding.PROJ_DISC_HIST_ID & "'"
                        Me.SqlCom.CommandText = "sp_executesql"
                        Me.SqlCom.CommandType = CommandType.Text
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        'Me.SqlCom.ExecuteNonQuery()
                    Case SelectedDiscount.None
                End Select
                If (Not SDiscount = SelectedDiscount.OtherDiscount) Or (Not SDiscount = SelectedDiscount.None) Then
                    Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Else
                    Me.RollbackTransaction()
                    Me.CloseConnection()
                    Throw New Exception("Unknown error")
                End If
                If MustCommitTrans Then
                    Me.CommiteTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                End If

            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
        End Sub

        Public Sub Delete_OA_Remainding(ByVal OA_RM_ID As String, ByVal SDiscount As SelectedDiscount, _
        ByVal QTY As Decimal, Optional ByVal AGREE_DISC_HIST_ID As String = "", Optional ByVal MRKT_DISC_HIST_ID As String = "" _
        , Optional ByVal PROJ_DISC_HIST_ID As String = "", Optional ByVal IsOtherDiscount As Boolean = False, Optional ByVal OA_BRANDPACK_ID As String = "", _
        Optional ByVal BRND_B_S_ID As String = "", Optional ByVal ACHIEVEMENT_BRANDPACK_ID As String = "", Optional ByVal MRKT_M_S_ID As String = "")
            Try
                Query = "SET NOCOUNT ON;SELECT OA_RM_ID FROM ORDR_OA_BRANDPACK_DISC WHERE OA_RM_ID = '" & OA_RM_ID & "'"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection()
                If Not IsNothing(Me.SqlCom.ExecuteScalar()) Then
                    Me.ClearCommandParameters()
                    Me.CloseConnection() : Throw New Exception(Me.MessageCantDeleteData) : Return
                End If
                Me.ClearCommandParameters()

                Query = "SET NOCOUNT ON;DECLARE @V_FKCodeAdjust INT; " & vbCrLf & _
                        " SET @V_FKCodeAdjust = (SELECT TOP 1 FKCodeAdjust FROM ORDR_OA_REMAINDING WHERE OA_RM_ID = @OA_RM_ID) ; " & vbCrLf & _
                        " DELETE FROM ORDR_OA_REMAINDING WHERE OA_RM_ID = @OA_RM_ID ;" & vbCrLf & _
                        " SELECT FKCodeAdjust = @V_FKCodeAdjust ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@OA_RM_ID", SqlDbType.NVarChar, OA_RM_ID)
                Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Dim FKCodeAdjust As Object = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()

                Select Case SDiscount
                    Case SelectedDiscount.AgreementDiscount
                        If Not AGREE_DISC_HIST_ID = "" Then
                            Me.SqlCom.CommandText = "Usp_Check_Sum_Disc_Qty_AGREE_DISC_HIST_ID"
                            Me.SqlCom.CommandType = CommandType.StoredProcedure
                            Me.AddParameter("@AGREE_DISC_HIST_ID", SqlDbType.VarChar, AGREE_DISC_HIST_ID, 115)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        ElseIf ACHIEVEMENT_BRANDPACK_ID <> "" Then
                            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Check_Sum_Matching_Disc_Qty_Agreement_Saving")
                            Me.AddParameter("@BRND_B_S_ID", SqlDbType.VarChar, BRND_B_S_ID, 60)
                            Me.AddParameter("@ACHIEVEMENT_BRANDPACK_ID", SqlDbType.VarChar, ACHIEVEMENT_BRANDPACK_ID, 70) ' VARCHAR(45),
                            Me.AddParameter("@DISC_FROM", SqlDbType.VarChar, NufarmBussinesRules.SharedClass.DISC_AGREE_FROM, 20) ' VARCHAR(20)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        End If
                        If Not IsNothing(FKCodeAdjust) And Not IsDBNull(FKCodeAdjust) Then
                            Query = " SET NOCOUNT ON; " & vbCrLf & _
                                       " DECLARE @V_LEFT_QTY DECIMAL(18,3),@V_DISC_QTY DECIMAL(18,3),@SUM_RELEASE_QTY DECIMAL(18,3),@V_ADJUST_QTY DECIMAL(18,3) ;" & vbCrLf & _
                                       " SET @V_DISC_QTY = (SELECT ISNULL(QTY,0) FROM ADJUSTMENT_DPD WHERE IDApp = @FKApp ); " & vbCrLf & _
                                       " SET @SUM_RELEASE_QTY = (SELECT ISNULL(SUM(ADJ_DISC_QTY),0) FROM ADJUSTMENT_TRANS WHERE FKApp = @FKApp ) + (SELECT ISNULL(SUM(LEFT_QTY),0) FROM ORDR_OA_REMAINDING WHERE FKCodeAdjust = @FKApp); " & vbCrLf & _
                                       " UPDATE ADJUSTMENT_DPD SET RELEASE_QTY = @SUM_RELEASE_QTY,LEFT_QTY = (@V_DISC_QTY - @SUM_RELEASE_QTY),ModifiedBy  = @ModifiedBy, ModifiedDate = GETDATE() WHERE IDApp = @FKApp ;"
                            Me.ResetCommandText(CommandType.Text, Query)
                            Me.AddParameter("@FKApp", SqlDbType.Int, FKCodeAdjust)
                            Me.AddParameter("@ModifiedBy", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        End If
                    Case SelectedDiscount.MarketingDiscount
                        If MRKT_DISC_HIST_ID <> "" Then
                            Me.SqlCom.CommandText = "Usp_Check_Sum_Disc_Qty_MRKT_DISC_HISTORY"
                            Me.SqlCom.CommandType = CommandType.StoredProcedure
                            Me.AddParameter("@MRKT_DISC_HIST_ID", SqlDbType.VarChar, MRKT_DISC_HIST_ID, 115)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        ElseIf MRKT_M_S_ID <> "" Then
                            Me.SqlCom.CommandText = "Usp_Check_Sum_Matching_Disc_Qty_MRKT_M_S_ID"
                            Me.SqlCom.CommandType = CommandType.StoredProcedure
                            Me.AddParameter("@MRKT_M_S_ID", SqlDbType.VarChar, MRKT_M_S_ID, 60)
                            Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                        End If
                    Case SelectedDiscount.None
                        'Me.SqlCom.CommandText = "UPDATE AGREE"
                    Case SelectedDiscount.OtherDiscount
                        Query = "SET NOCOUNT ON;DELETE FROM ORDR_OA_REMAINDING WHERE OA_RM_ID = '" & OA_RM_ID & "'"
                        Me.SqlCom.CommandText = "sp_executesql"
                        Me.SqlCom.CommandType = CommandType.StoredProcedure
                        Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    Case SelectedDiscount.ProjectDiscount
                        'RESET COMMAND TEXT
                        Query = "SET NOCOUNT ON;UPDATE PROJ_DISC_HISTORY SET PROJ_LEFT_QTY = CAST((SELECT PROJ_LEFT_QTY FROM PROJ_DISC_HISTORY" & _
                        " WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "') AS DECIMAL(10,3)) + @QTY,PROJ_RELEASE_QTY = " & _
                        " CAST((SELECT PROJ_RELEASE_QTY FROM PROJ_DISC_HISTORY WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "') AS DECIMAL(10,3)) - " & _
                        " @QTY,MODIFY_BY = '" & NufarmBussinesRules.User.UserLogin.UserName & "',MODIFY_DATE = GETDATE() " & _
                        " WHERE PROJ_DISC_HIST_ID = '" & PROJ_DISC_HIST_ID & "'"
                        Me.SqlCom.CommandText = Query : Me.SqlCom.CommandType = CommandType.Text
                        Me.AddParameter("@QTY", SqlDbType.Decimal, QTY)
                        'Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                        ''RESET COMMAND TEXT 
                        Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                End Select
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction()
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub

#End Region

#End Region

#Region " Property "

        Public ReadOnly Property ViewDistributor() As DataView
            Get
                Return Me.m_ViewDistributor
            End Get
        End Property

        Public ReadOnly Property ViewOADiscount() As DataView
            Get
                Return Me.m_ViewOADiscount
            End Get
        End Property

        Public ReadOnly Property ViewGivenDiscount() As DataView
            Get
                Return Me.m_ViewGivenDiscount
            End Get
        End Property

        Public ReadOnly Property ViewOtherDiscount() As DataView
            Get
                Return Me.m_ViewOtherDiscount
            End Get
        End Property

        Public ReadOnly Property ViewSemesterlyDiscount() As DataView
            Get
                Return Me.m_ViewSemesterlyDiscount
            End Get
        End Property

        Public ReadOnly Property ViewQuarterlyDiscount() As DataView
            Get
                Return Me.m_ViewQuarterlyDiscount
            End Get
        End Property

        Public ReadOnly Property ViewYearlyDiscount() As DataView
            Get
                Return Me.m_ViewYearlyDiscount
            End Get
        End Property

        Public ReadOnly Property ViewMarketingDiscount() As DataView
            Get
                Return Me.M_ViewMarketingDiscount
            End Get
        End Property

        Public ReadOnly Property ViewProjectDiscount() As DataView
            Get
                Return Me.m_ViewProjectDiscount
            End Get
        End Property
        Public ReadOnly Property ViewOALeftDiscount() As DataView
            Get
                Return Me.m_ViewRemaindingDisc
            End Get
        End Property

#End Region

#Region " Dispose Metode "
        Public Overloads Sub Dispose(ByVal Disposing As Boolean)
            MyBase.Dispose(Disposing)
            If Not IsNothing(Me.m_ViewGivenDiscount) Then
                Me.m_ViewGivenDiscount.Dispose()
                Me.m_ViewGivenDiscount = Nothing
            End If
            If Not IsNothing(Me.M_ViewMarketingDiscount) Then
                Me.M_ViewMarketingDiscount.Dispose()
                Me.M_ViewMarketingDiscount = Nothing
            End If
            If Not IsNothing(Me.m_ViewOADiscount) Then
                Me.m_ViewOADiscount.Dispose()
                Me.m_ViewOADiscount = Nothing
            End If
            If Not IsNothing(Me.m_ViewOtherDiscount) Then
                Me.m_ViewOtherDiscount.Dispose()
                Me.m_ViewOtherDiscount = Nothing
            End If
            If Not IsNothing(Me.m_ViewProjectDiscount) Then
                Me.m_ViewProjectDiscount.Dispose()
                Me.m_ViewProjectDiscount = Nothing
            End If
            If Not IsNothing(Me.m_ViewQuarterlyDiscount) Then
                Me.m_ViewQuarterlyDiscount.Dispose()
                Me.m_ViewQuarterlyDiscount = Nothing
            End If
            If Not IsNothing(Me.m_ViewSemesterlyDiscount) Then
                Me.m_ViewSemesterlyDiscount.Dispose()
                Me.m_ViewSemesterlyDiscount = Nothing
            End If
            If Not IsNothing(Me.m_ViewYearlyDiscount) Then
                Me.m_ViewYearlyDiscount.Dispose()
                Me.m_ViewYearlyDiscount = Nothing
            End If
            If Not IsNothing(Me.m_ViewDistributor) Then
                Me.m_ViewDistributor.Dispose()
                Me.m_ViewDistributor = Nothing
            End If
            If Not IsNothing(Me.m_ViewRemaindingDisc) Then
                Me.m_ViewRemaindingDisc.Dispose()
                Me.m_ViewRemaindingDisc = Nothing
            End If
            If Not IsNothing(CType(Me.DRView, Object)) Then
                Me.DRView.Dispose()
                Me.DRView = Nothing
            End If

        End Sub
#End Region

    End Class

End Namespace

