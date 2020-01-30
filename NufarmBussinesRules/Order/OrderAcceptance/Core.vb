Namespace OrderAcceptance
    Public Class Core
        Inherits NufarmBussinesRules.OrderAcceptance.OADiscount
#Region " Deklarasi "
        Private m_ViewOtherDiscount As DataView
        Private m_ViewOADiscount As DataView
        Private m_ViewDistributor As DataView
        Private m_ViewOABrandPack As DataView
        Private m_Ds As DataSet
        Private m_OAReportView As DataView
        Private m_ViewOAShipTo As DataView
#End Region

#Region " Void "

#End Region

#Region " Function "
        'Public Function CreateViewDistributor()
        '    Try
        '        Me.CreateCommandSql("", "SELECT DISTINCT ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID,DIST_DISTRIBUTOR.DISTRIBUTOR_NAME " & _
        '        " FROM ORDR_PURCHASE_ORDER,DIST_DISTRIBUTOR WHERE ORDR_PURCHASE_ORDER.DISTRIBUTOR_ID = DIST_DISTRIBUTOR.DISTRIBUTOR_ID")
        '        Dim tblDistributor As New DataTable("DISTRIBUTOR")
        '        tblDistributor.Clear()
        '        Me.FillDataTable(tblDistributor)
        '        Me.m_ViewDistributor = tblDistributor.DefaultView()
        '        Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        '    Return Me.m_ViewDistributor
        'End Function
        Public Overloads Function CreateViewAgreementDiscount(ByVal Distributor_ID As String, ByVal SDiscount As TypeAgreementDiscount, _
        ByVal START_DATE As DateTime, ByVal END_DATE As DateTime) As DataView ', ByVal AGREEMENT_NO As String) As DataView
            Try
                Me.CreateCommandSql("Sp_GetView_ORDR_OA_BRANDPACK_DISC_BY_AGREE", "")
                Select Case SDiscount
                    Case TypeAgreementDiscount.Given
                        'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10) ' VARCHAR(10),
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "G", 5) ' VARCHAR(5)
                    Case TypeAgreementDiscount.Quarterly
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "Q", 5) ' VARCHAR(5)
                    Case TypeAgreementDiscount.Semesterly
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "S", 5) ' VARCHAR(5)
                    Case TypeAgreementDiscount.Yearly
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "Y", 5) ' VARCHAR(5)
                End Select
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10) ' VARCHAR(10),
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, START_DATE)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, END_DATE)
                'Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Dim tblAgreementDiscount As New DataTable("AGREEMENT_DISCOUNT_DETAIL")
                tblAgreementDiscount.Clear()
                Me.FillDataTable(tblAgreementDiscount)
                Me.m_ViewOADiscount = tblAgreementDiscount.DefaultView()

                'Me.m_Ds = New DataSet("AGREEMENT_DISCOUNT")
                'Me.m_Ds.Clear()
                'Me.m_Ds.Tables.Add(tblAgreementDiscount)
                'Me.CreateCommandSql("Sp_GetView_ORDR_OA_BRANDPACK_DISC_BY_AGREE", "")
                'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10) ' VARCHAR(10),
                'Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25) ' VARCHAR(25)
                'Select Case SDiscount
                '    Case TypeAgreementDiscount.Given
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "G", 5) ' VARCHAR(5)
                '    Case TypeAgreementDiscount.Quarterly
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "Q", 5) ' VARCHAR(5)
                '    Case TypeAgreementDiscount.Semesterly
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "S", 5) ' VARCHAR(5)
                '    Case TypeAgreementDiscount.Yearly
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "Y", 5) ' VARCHAR(5)
                'End Select
                'Dim tblOABrandPackDiscount As New DataTable("OA_BRANDPACK_DISCOUNT")
                'tblOABrandPackDiscount.Clear()
                'Me.FillDataTable(tblOABrandPackDiscount)
                'Me.m_Ds.Tables.Add(tblOABrandPackDiscount)
                'Me.AddrelationToDataSet(Me.m_Ds, Me.m_Ds.Tables("AGREEMENT_DISCOUNT_DETAIL").Columns("PARENT_ID"), Me.m_Ds.Tables("OA_BRANDPACK_DISCOUNT").Columns("CHILD_ID"), "AGREEMENT_RELATIONS")
                'Me.m_ViewAgreementDiscount = Me.m_Ds.Tables("AGREEMENT_DISCOUNT_DETAIL").DefaultView()
                'Me.m_ViewAgreementDiscount.Sort = "AGREE_DISC_HIST_ID"
                'Me.m_ViewOADiscount = Me.m_Ds.Tables("OA_BRANDPACK_DISCOUNT").DefaultView()
                'Me.m_ViewOADiscount.Sort = "AGREE_DISC_HIST_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewOADiscount
        End Function
        Public Overloads Function CreateViewAgreementDiscount(ByVal Distributor_ID As String, _
        ByVal START_DATE As DateTime, ByVal END_DATE As DateTime) As DataView ', ByVal AGREEMENT_NO As String) As DataView
            Try
                Me.CreateCommandSql("Sp_GetView_ORDR_OA_BRANDPACK_DISC_BY_AGREE", "")
                Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, DBNull.Value, 5) ' VARCHAR(5)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10) ' VARCHAR(10),
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, START_DATE)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, END_DATE)
                'Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25)
                Dim tblAgreementDiscount As New DataTable("AGREEMENT_DISCOUNT_DETAIL")
                tblAgreementDiscount.Clear()
                Me.FillDataTable(tblAgreementDiscount)
                Me.m_ViewOADiscount = tblAgreementDiscount.DefaultView()

                'Me.m_Ds = New DataSet("AGREEMENT_DISCOUNT")
                'Me.m_Ds.Clear()
                'Me.m_Ds.Tables.Add(tblAgreementDiscount)
                'Me.CreateCommandSql("Sp_GetView_ORDR_OA_BRANDPACK_DISC_BY_AGREE", "")
                'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10) ' VARCHAR(10),
                'Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AGREEMENT_NO, 25) ' VARCHAR(25)
                'Select Case SDiscount
                '    Case TypeAgreementDiscount.Given
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "G", 5) ' VARCHAR(5)
                '    Case TypeAgreementDiscount.Quarterly
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "Q", 5) ' VARCHAR(5)
                '    Case TypeAgreementDiscount.Semesterly
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "S", 5) ' VARCHAR(5)
                '    Case TypeAgreementDiscount.Yearly
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "Y", 5) ' VARCHAR(5)
                'End Select
                'Dim tblOABrandPackDiscount As New DataTable("OA_BRANDPACK_DISCOUNT")
                'tblOABrandPackDiscount.Clear()
                'Me.FillDataTable(tblOABrandPackDiscount)
                'Me.m_Ds.Tables.Add(tblOABrandPackDiscount)
                'Me.AddrelationToDataSet(Me.m_Ds, Me.m_Ds.Tables("AGREEMENT_DISCOUNT_DETAIL").Columns("PARENT_ID"), Me.m_Ds.Tables("OA_BRANDPACK_DISCOUNT").Columns("CHILD_ID"), "AGREEMENT_RELATIONS")
                'Me.m_ViewAgreementDiscount = Me.m_Ds.Tables("AGREEMENT_DISCOUNT_DETAIL").DefaultView()
                'Me.m_ViewAgreementDiscount.Sort = "AGREE_DISC_HIST_ID"
                'Me.m_ViewOADiscount = Me.m_Ds.Tables("OA_BRANDPACK_DISCOUNT").DefaultView()
                'Me.m_ViewOADiscount.Sort = "AGREE_DISC_HIST_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewOADiscount
        End Function
        Public Overloads Function CreateViewMarketingDiscount(ByVal Distributor_ID As String, ByVal SDiscount As TypeMarketingDiscount, _
        ByVal START_DATE As DateTime, ByVal END_DATE As DateTime) As DataView ', ByVal AGREEMENT_NO As String) As DataView
            Try
                Me.CreateCommandSql("Sp_GetView_ORDR_OA_BRANDPACK_DISC_BY_MRKT", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, START_DATE)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, END_DATE)
                Select Case SDiscount
                    Case TypeMarketingDiscount.GivenDiscount
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "MG", 5) ' CHAR(1)
                    Case TypeMarketingDiscount.SteppingDiscount
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "MS", 5) ' CHAR(1)
                    Case TypeMarketingDiscount.TargetDiscount
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "MT", 5) ' CHAR(1)
                    Case TypeMarketingDiscount.Given_CP
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "CP", 5)
                    Case TypeMarketingDiscount.Given_CPSD
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "CS", 5)
                    Case TypeMarketingDiscount.Given_DK
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "DK", 5)
                    Case TypeMarketingDiscount.Given_PKPP
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "KP", 5)
                    Case TypeMarketingDiscount.Given_CPR
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "CR", 5)
                    Case TypeMarketingDiscount.Given_CPMRT
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "CD", 5)
                    Case TypeMarketingDiscount.Given_DKN
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "DN", 5)
                    Case TypeMarketingDiscount.Given_CPDAuto
                        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "CA", 5)
                End Select
                Dim tblMarketingDiscount As New DataTable("SALES_DISCOUNT_DETAIL")
                tblMarketingDiscount.Clear()
                Me.FillDataTable(tblMarketingDiscount)
                Me.m_ViewOADiscount = tblMarketingDiscount.DefaultView()

                'Me.m_Ds = New DataSet("MARKETING_DISCOUNT")
                'Me.m_Ds.Clear()
                'Me.m_Ds.Tables.Add(tblMarketingDiscount)

                'Me.CreateCommandSql("Sp_GetView_ORDR_OA_BRANDPACK_DISC_BY_MRKT", "")
                'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10) ' VARCHAR(10),
                'Select Case SDiscount
                '    Case TypeMarketingDiscount.GivenDiscount
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "G", 5) ' VARCHAR(5)
                '    Case TypeMarketingDiscount.SteppingDiscount
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "S", 5) ' VARCHAR(5)
                '    Case TypeMarketingDiscount.TargetDiscount
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "T", 5) ' VARCHAR(5)
                'End Select
                'Dim tblOABrandPackDiscount As New DataTable("OA_BRANDPACK_DISCOUNT")
                'tblOABrandPackDiscount.Clear()
                ''ME.m_ViewOADiscount = tblOABrandPackDisc
                'Me.FillDataTable(tblOABrandPackDiscount)
                'Me.m_Ds.Tables.Add(tblOABrandPackDiscount)
                'Me.AddrelationToDataSet(Me.m_Ds, Me.m_Ds.Tables("MARKETING_DISCOUNT_DETAIL").Columns("PARENT_ID"), _
                'Me.m_Ds.Tables("OA_BRANDPACK_DISCOUNT").Columns("CHILD_ID"), "MARKETING_RELATIONS")

                'Me.m_ViewMarketingDiscount = Me.m_Ds.Tables("MARKETING_DISCOUNT_DETAIL").DefaultView()
                'Me.m_ViewMarketingDiscount.Sort = "MRKT_DISC_HIST_ID"
                'Me.m_ViewOADiscount = Me.m_Ds.Tables("OA_BRANDPACK_DISCOUNT").DefaultView()
                'Me.m_ViewOADiscount.Sort = "MRKT_DISC_HIST_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewOADiscount
        End Function
        Public Shadows Function CreateViewOABrandPack(ByVal DISTRIBUTOR_ID As String, ByVal Flag As Object, _
        ByVal StartDate As DateTime, ByVal EndDate As DateTime) As DataView
            Try
                Me.CreateCommandSql("Sp_Getview_OA_BRANDPACK", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, Flag, 5) ' CHAR(5
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Dim tblOABrandPack As New DataTable("OA_BRANDPACK")
                tblOABrandPack.Clear()
                Me.FillDataTable(tblOABrandPack)
                Me.m_ViewOABrandPack = tblOABrandPack.DefaultView()
                Me.m_ViewOABrandPack.Sort = "OA_BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewOABrandPack
        End Function
        Public Overloads Function CreateViewMarketingDiscount(ByVal Distributor_ID As String, ByVal START_DATE As DateTime, _
        ByVal END_DATE As DateTime) As DataView ', ByVal AGREEMENT_NO As String) As DataView
            Try
                Me.CreateCommandSql("Sp_GetView_ORDR_OA_BRANDPACK_DISC_BY_MRKT", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10)
                'Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, DBNull.Value, 5)
                Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, DBNull.Value, 5) ' CHAR(1)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, START_DATE)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, END_DATE)
                Dim tblMarketingDiscount As New DataTable("SALES_DISCOUNT_DETAIL")
                tblMarketingDiscount.Clear()
                Me.FillDataTable(tblMarketingDiscount)
                Me.m_ViewOADiscount = tblMarketingDiscount.DefaultView()

                'Me.m_Ds = New DataSet("MARKETING_DISCOUNT")
                'Me.m_Ds.Clear()
                'Me.m_Ds.Tables.Add(tblMarketingDiscount)

                'Me.CreateCommandSql("Sp_GetView_ORDR_OA_BRANDPACK_DISC_BY_MRKT", "")
                'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10) ' VARCHAR(10),
                'Select Case SDiscount
                '    Case TypeMarketingDiscount.GivenDiscount
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "G", 5) ' VARCHAR(5)
                '    Case TypeMarketingDiscount.SteppingDiscount
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "S", 5) ' VARCHAR(5)
                '    Case TypeMarketingDiscount.TargetDiscount
                '        Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, "T", 5) ' VARCHAR(5)
                'End Select
                'Dim tblOABrandPackDiscount As New DataTable("OA_BRANDPACK_DISCOUNT")
                'tblOABrandPackDiscount.Clear()
                ''ME.m_ViewOADiscount = tblOABrandPackDisc
                'Me.FillDataTable(tblOABrandPackDiscount)
                'Me.m_Ds.Tables.Add(tblOABrandPackDiscount)
                'Me.AddrelationToDataSet(Me.m_Ds, Me.m_Ds.Tables("MARKETING_DISCOUNT_DETAIL").Columns("PARENT_ID"), _
                'Me.m_Ds.Tables("OA_BRANDPACK_DISCOUNT").Columns("CHILD_ID"), "MARKETING_RELATIONS")

                'Me.m_ViewMarketingDiscount = Me.m_Ds.Tables("MARKETING_DISCOUNT_DETAIL").DefaultView()
                'Me.m_ViewMarketingDiscount.Sort = "MRKT_DISC_HIST_ID"
                'Me.m_ViewOADiscount = Me.m_Ds.Tables("OA_BRANDPACK_DISCOUNT").DefaultView()
                'Me.m_ViewOADiscount.Sort = "MRKT_DISC_HIST_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewOADiscount
        End Function
        Public Shadows Function CreateViewOABrandPack(ByVal DistributorID As String) As DataView
            Try
                Me.CreateCommandSql("Sp_Getview_OA_BRANDPACK_1", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 25)
                'Me.AddParameter("@GQSY_SGT_P_FLAG", SqlDbType.VarChar, Flag, 5) ' CHAR(5
                Dim tblOABrandPack As New DataTable("OA_BRANDPACK")
                tblOABrandPack.Clear()
                Me.FillDataTable(tblOABrandPack)
                Me.m_ViewOABrandPack = tblOABrandPack.DefaultView()
                Me.m_ViewOABrandPack.Sort = "OA_BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewOABrandPack
        End Function
        Public Overloads Function CreateViewProjectDiscount(ByVal Distributor_ID As String) As DataView
            Try
                Me.CreateCommandSql("Sp_GetView_ORDR_OA_BRANDPACK_DISC_BY_PROJ", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10) ' VARCHAR(10)
                Dim tblProjectDiscount As New DataTable("PROJECT_DISCOUNT_DETAIL")
                tblProjectDiscount.Clear()
                Me.FillDataTable(tblProjectDiscount)
                Me.m_ViewOADiscount = tblProjectDiscount.DefaultView()
                'Me.m_Ds = New DataSet("PROJECT_DISCOUNT")
                'Me.m_Ds.Clear()
                'Me.m_Ds.Tables.Add(tblProjectDiscount)

                'Me.CreateCommandSql("Sp_GetView_ORDR_OA_BRANDPACK_DISC_BY_PROJ", "")
                'Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Distributor_ID, 10) ' VARCHAR(10)
                'Dim tblOABrandPackDiscount As New DataTable("OA_BRANDPACK_DISCOUNT")
                'tblOABrandPackDiscount.Clear()
                'Me.FillDataTable(tblOABrandPackDiscount)
                'Me.m_Ds.Tables.Add(tblOABrandPackDiscount)
                'Me.AddrelationToDataSet(Me.m_Ds, Me.m_Ds.Tables("PROJECT_DISCOUNT_DETAIL").Columns("PARENT_ID"), _
                ' Me.m_Ds.Tables("OA_BRANDPACK_DISCOUNT").Columns("CHILD_ID"), "PROJECT_RELATIONS")
                'Me.m_ViewProjectDiscount = Me.M.DefaultView()
                'Me.m_ViewProjectDiscount.Sort = "OA_BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewOADiscount
        End Function
        Public Function CreateViewOtherDiscount(ByVal DISTRIBUTOR_ID As String, ByVal START_DATE As DateTime, ByVal END_DATE As DateTime, ByVal Flag As String) As DataView ', ByVal AGREEMENT_NO As String) As DataView
            Try
                Me.CreateCommandSql("Sp_GetView_OTHER_DISC_HISTORY", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Me.AddParameter("@START_DATE", SqlDbType.DateTime, START_DATE)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, END_DATE)
                Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 5)
                Dim tblOADiscount As New DataTable("OTHER_DISCOUNT")
                tblOADiscount.Clear()
                Me.FillDataTable(tblOADiscount)
                Me.m_ViewOtherDiscount = tblOADiscount.DefaultView()
                Me.m_ViewOtherDiscount.Sort = "OA_BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.m_ViewOtherDiscount
        End Function
        Public Function generateReport(Optional ByVal FROMOADATE As Object = Nothing, Optional ByVal UNTILOADATE As Object = Nothing _
        , Optional ByVal FROMPODATE As Object = Nothing, Optional ByVal UNTILPODATE As Object = Nothing) As DataView
            Try
                Me.CreateCommandSql("Usp_Create_View_OA_Report", "")
                If (Not IsNothing(FROMOADATE)) And (Not IsNothing(UNTILOADATE)) Then
                    Me.AddParameter("@FROM_OADATE", SqlDbType.DateTime, FROMOADATE) ' DATETIME,
                    Me.AddParameter("@UNTIL_OADATE", SqlDbType.DateTime, UNTILOADATE) ' DATETIME,
                    Me.AddParameter("@FROM_PODATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@UNTIL_PODATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME
                ElseIf Not IsNothing(FROMOADATE) Then
                    Me.AddParameter("@FROM_OADATE", SqlDbType.DateTime, FROMOADATE) ' DATETIME,
                    Me.AddParameter("@UNTIL_OADATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@FROM_PODATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@UNTIL_PODATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME
                ElseIf Not IsNothing(UNTILOADATE) Then
                    Me.AddParameter("@FROM_OADATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@UNTIL_OADATE", SqlDbType.DateTime, UNTILOADATE) ' DATETIME,
                    Me.AddParameter("@FROM_PODATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@UNTIL_PODATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME
                ElseIf (Not IsNothing(FROMPODATE)) And (Not IsNothing(UNTILPODATE)) Then
                    Me.AddParameter("@FROM_OADATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@UNTIL_OADATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@FROM_PODATE", SqlDbType.DateTime, FROMPODATE) ' DATETIME,
                    Me.AddParameter("@UNTIL_PODATE", SqlDbType.DateTime, UNTILPODATE) ' DATETIME
                ElseIf Not IsNothing(FROMPODATE) Then
                    Me.AddParameter("@FROM_OADATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@UNTIL_OADATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@FROM_PODATE", SqlDbType.DateTime, FROMPODATE) ' DATETIME,
                    Me.AddParameter("@UNTIL_PODATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME
                ElseIf Not IsNothing(UNTILPODATE) Then
                    Me.AddParameter("@FROM_OADATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@UNTIL_OADATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@FROM_PODATE", SqlDbType.DateTime, DBNull.Value) ' DATETIME,
                    Me.AddParameter("@UNTIL_PODATE", SqlDbType.DateTime, UNTILPODATE) ' DATETIME
                End If
                Dim tblOAreport As New DataTable("OA_REPORT")
                tblOAreport.Clear()
                Me.FillDataTable(tblOAreport)
                Me.m_OAReportView = tblOAreport.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_OAReportView
        End Function

        Public Function CreateViewOAShipTo(ByVal DISTRIBUTOR_ID As String, ByVal START_DATE As DateTime, ByVal END_DATE As DateTime) As DataView
            Try
                Me.CreateCommandSql("Usp_Create_View_OA_Ship_To", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, START_DATE)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, END_DATE)
                Dim tblShipTo As New DataTable("OA_SHIP_TO")
                tblShipTo.Clear()
                Me.FillDataTable(tblShipTo)
                Me.m_ViewOAShipTo = tblShipTo.DefaultView()
                Return Me.m_ViewOAShipTo
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function
#End Region

#Region " Property "
        Public ReadOnly Property ViewOAShipTo() As DataView
            Get
                Return Me.m_ViewOAShipTo
            End Get
        End Property
        Public ReadOnly Property GetDataset() As DataSet
            Get
                Return Me.m_Ds
            End Get
        End Property
        Public Shadows ReadOnly Property ViewOtherDiscount() As DataView
            Get
                Return Me.m_ViewOtherDiscount
            End Get
        End Property
        Public Shadows ReadOnly Property ViewOABrandPack() As DataView
            Get
                Return Me.m_ViewOABrandPack
            End Get
        End Property
        Public Shadows ReadOnly Property ViewOADiscount() As DataView
            Get
                Return Me.m_ViewOADiscount
            End Get
        End Property
        Public ReadOnly Property ViewOAReport() As DataView
            Get
                Return Me.m_OAReportView
            End Get
        End Property
#End Region

#Region " Disposable methode '"
        Public Overloads Sub Discpose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewDistributor) Then
                Me.m_ViewDistributor.Dispose()
                Me.m_ViewDistributor = Nothing
            End If
            If Not IsNothing(Me.m_ViewOABrandPack) Then
                Me.m_ViewOABrandPack.Dispose()
                Me.m_ViewOABrandPack = Nothing
            End If
            If Not IsNothing(Me.m_ViewOAShipTo) Then
                Me.m_ViewOAShipTo.Dispose()
                Me.m_ViewOAShipTo = Nothing
            End If

            'If Not IsNothing(Me.m_ViewAgreementDiscount) Then
            '    Me.m_ViewAgreementDiscount.Dispose()
            '    Me.m_ViewAgreementDiscount = Nothing
            'End If
            'If Not IsNothing(Me.m_ViewMarketingDiscount) Then
            '    Me.m_ViewMarketingDiscount.Dispose()
            '    Me.m_ViewMarketingDiscount = Nothing
            'End If
            'If Not IsNothing(Me.m_ViewProjectDiscount) Then
            '    Me.m_ViewProjectDiscount.Dispose()
            '    Me.m_ViewProjectDiscount = Nothing
            'End If
            If Not IsNothing(Me.m_ViewOtherDiscount) Then
                Me.m_ViewOtherDiscount.Dispose()
                Me.m_ViewOtherDiscount = Nothing
            End If
            If Not IsNothing(Me.m_Ds) Then
                Me.m_Ds.Dispose()
                Me.m_Ds = Nothing
            End If
            If Not IsNothing(Me.m_OAReportView) Then
                Me.m_OAReportView.Dispose()
                Me.m_OAReportView = Nothing
            End If
        End Sub
#End Region

    End Class
End Namespace

