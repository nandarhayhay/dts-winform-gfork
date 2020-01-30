Imports System.Data
Namespace DistributorRegistering
    Public Class DistrHistory
        Inherits NufarmDataAccesLayer.DataAccesLayer.ADODotNet
        'Private m_PurchaseOrder As NufarmBussinesRules.PurchaseOrder.PORegistering
        'Private m_MarketingProgram As NufarmBussinesRules.Program.ProgramRegistering
        'Private m_AgreementBrand As NufarmBussinesRules.DistributorAgreement.Agreement
        'Private m_OrderAcceptance As NufarmBussinesRules.OrderAcceptance.OARegistering
        'Private m_Project As NufarmBussinesRules.DistributorProject.ProjectRegistering
        'Private m_Distributor As NufarmBussinesRules.DistributorAgreement.DistrAGreement
        Private m_ViewProject As DataView
        Private m_ViewPurchaseOrder As DataView
        Private m_ViewMarketingProgram As DataView
        Private m_ViewAgreementBrand As DataView
        Private m_ViewOtherProgram As DataView
        Private m_ViewHK As DataView
        'Private m_ViewOrderAcceptance As DataView
        Private m_ViewDistributor As DataView
        Private m_viewOThers As DataView
        Private m_viewOthersDetail As DataView
        Public ReadOnly Property ViewOthersDetail() As DataView
            Get
                Return Me.m_viewOthersDetail
            End Get
        End Property
        Public ReadOnly Property ViewAgreement() As DataView
            Get
                Return Me.m_ViewAgreementBrand
            End Get
            'Set(ByVal value As DataView)
            '    Me.m_ViewAgreementBrand = value
            'End Set
        End Property
        Public ReadOnly Property ViewPurchaseOrder() As DataView
            Get
                Return Me.m_ViewPurchaseOrder
            End Get
            'Set(ByVal value As DataView)
            '    Me.m_ViewPurchaseOrder = value
            'End Set
        End Property
        Public ReadOnly Property ViewMarketingProgram() As DataView
            Get
                Return Me.m_ViewMarketingProgram
            End Get
            'Set(ByVal value As DataView)
            '    Me.m_ViewMarketingProgram = value
            'End Set
        End Property
        'Public Property ViewOrderAcceptance() As DataView
        '    Get
        '        Return Me.m_ViewOrderAcceptance
        '    End Get
        '    Set(ByVal value As DataView)
        '        Me.m_ViewOrderAcceptance = value
        '    End Set
        'End Property
        Public ReadOnly Property ViewProject() As DataView
            Get
                Return Me.m_ViewProject
            End Get
            'Set(ByVal value As DataView)
            '    Me.m_ViewProject = value
            'End Set
        End Property
        Public ReadOnly Property ViewDistributor() As DataView
            Get
                Return Me.m_ViewDistributor
            End Get
        End Property
        Public ReadOnly Property ViewOtherProgram() As DataView
            Get
                Return Me.m_ViewOtherProgram
            End Get
        End Property
        Public ReadOnly Property ViewHKProgram() As DataView
            Get
                Return Me.m_ViewHK
            End Get
        End Property
        Public ReadOnly Property viewOthers() As DataView
            Get
                Return Me.m_viewOThers
            End Get
        End Property
        Public Sub GetDataView(ByVal DISTRIBUTOR_ID As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime)
            Try

                Dim Query As String = "SET NOCOUNT ON;SELECT PO.PO_REF_NO,PO.PO_REF_DATE,BP.BRANDPACK_NAME,OPB.PO_ORIGINAL_QTY AS QUANTITY," & vbCrLf & _
                                      "OPB.PO_PRICE_PERQTY AS [PRICE/QTY],OPB.PO_ORIGINAL_QTY * OPB.PO_PRICE_PERQTY AS TOTAL,OPB.DESCRIPTIONS,OPB.DESCRIPTIONS2 AS CSE_REMARK" & vbCrLf & _
                                      " FROM ORDR_PURCHASE_ORDER PO INNER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = PO.PO_REF_NO INNER JOIN BRND_BRANDPACK BP ON OPB.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf & _
                                      " AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)

                'Me.CreateCommandSql("", "SELECT PO_REF_NO,PO_REF_DATE,BRANDPACK_NAME,QUANTITY,[PRICE/QTY],TOTAL " & _
                '" FROM VIEW_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'")
                Dim tblPO As New DataTable("PURCHASE_ORDER")
                tblPO.Clear() : Me.SqlDat = New SqlClient.SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection() : Me.SqlDat.Fill(tblPO) : Me.ClearCommandParameters()

                'Me.FillDataTable(tblPO)
                Me.m_ViewPurchaseOrder = tblPO.DefaultView()
                Me.m_ViewPurchaseOrder.Sort = "PO_REF_DATE"
                'MARKETING PROGRAM
                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Create_View_Sales_Program_Distributor_History")
                Me.SqlDat.SelectCommand = Me.SqlCom
                'Me.SqlCom.CommandType = CommandType.StoredProcedure
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Dim tblMarketing As New DataTable("SALES_PROGRAM")
                tblMarketing.Clear() : Me.SqlDat.Fill(tblMarketing)
                Me.m_ViewMarketingProgram = tblMarketing.DefaultView()
                Me.m_ViewMarketingProgram.Sort = "PROGRAM_ID"

                'AGREEMENT_BRAND
                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Create_View_Agreement_Program_Distributor_History")
                'Me.SqlCom.CommandType = CommandType.StoredProcedure
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Dim tblAgreement As New DataTable("AGREEMENT_BRAND")
                tblAgreement.Clear() : Me.SqlDat.SelectCommand = Me.SqlCom : Me.SqlDat.Fill(tblAgreement)
                Me.m_ViewAgreementBrand = tblAgreement.DefaultView()
                Me.m_ViewAgreementBrand.Sort = "AGREEMENT_NO"
                Me.ClearCommandParameters()
                'Me.m_MarketingProgram = New NufarmBussinesRules.Program.ProgramRegistering()
                'Me.ViewMarketingProgram = Me.m_MarketingProgram.ViewProgramRegistering()
                '            Me.m_AgreementBrand = New NufarmBussinesRules.DistributorAgreement.Agreement()
                '           Me.ViewAgreement = Me.m_AgreementBrand.ViewAgreement()

                'PROJECT
                'BESOK LAGI
                'Me.m_Distributor = New NufarmBussinesRules.DistributorAgreement.DistrAGreement()
                'Me.m_Distributor.GetDataViewDistributor()

                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        " SELECT P.PROJ_REF_NO,P.PROJECT_NAME,P.PROJ_REF_DATE,PB.BRANDPACK_ID,BP.BRANDPACK_NAME,PB.PRICE" & _
                        " FROM PROJ_PROJECT P INNER JOIN PROJ_BRANDPACK PB ON PB.PROJ_REF_NO = P.PROJ_REF_NO INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = PB.BRANDPACK_ID " & vbCrLf & _
                        " WHERE P.PROJ_REF_DATE >= @START_DATE AND P.PROJ_REF_DATE <= @END_DATE AND P.DISTRIBUTOR_ID = @DISTRIBUTOR_ID ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Dim tblProject As New DataTable("PROJECT_DISTRIBUTOR") : tblProject.Clear()
                Me.SqlDat.Fill(tblProject) : Me.ClearCommandParameters()
                Me.m_ViewProject = tblProject.DefaultView()

                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Report_DD_DR_History")

                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Dim tblHist As New DataTable("OTHERS_DISTRIBUTOR_HISTORY")
                setDataAdapter(Me.SqlCom).Fill(tblHist) : Me.ClearCommandParameters()
                Me.m_viewOThers = tblHist.DefaultView()
                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Schema_Distributor_History_Other_Detail")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                Dim tblHistDetail As New DataTable("OTHERS_DISTRIBUTOR_HISTORY_DETAIL")
                setDataAdapter(Me.SqlCom).Fill(tblHistDetail)
                Me.m_viewOthersDetail = tblHistDetail.DefaultView()

                Me.CloseConnection()
                Me.ClearCommandParameters()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function CreateViewDistributor() As DataView
            Try
                Dim Query As String = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim TblDistributor As New DataTable("DISTRIBUTOR")
                TblDistributor.Clear() : Me.FillDataTable(TblDistributor)
                Me.m_ViewDistributor = TblDistributor.DefaultView()
                Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistributor
        End Function
        'Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable

#Region " IDisposable Support "
        '' This code added by Visual Basic to correctly implement the disposable pattern.
        'Public Sub Dispose() Implements IDisposable.Dispose
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(True)
        '    GC.SuppressFinalize(Me)
        'End Sub

        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            'DISPOSE OBJECT CLASS
            'If Not IsNothing(Me.m_OrderAcceptance) Then
            '    Me.m_OrderAcceptance.Dispose(True)
            '    Me.m_OrderAcceptance = Nothing
            'End If
            'If Not IsNothing(Me.m_AgreementBrand) Then
            '    Me.m_AgreementBrand.Dispose(False)
            '    Me.m_MarketingProgram = Nothing
            'End If
            'If Not IsNothing(Me.m_MarketingProgram) Then
            '    Me.m_MarketingProgram.Dispose(False)
            'End If
            'If Not IsNothing(Me.m_PurchaseOrder) Then
            '    Me.m_PurchaseOrder.Dispose(False)
            '    Me.m_PurchaseOrder = Nothing
            'End If
            'If Not IsNothing(Me.m_Project) Then
            '    Me.m_Project.Dispose(False)
            '    Me.m_Project = Nothing
            'End If
            'If Not IsNothing(Me.m_Distributor) Then
            '    Me.m_Distributor.Dispose()
            '    Me.m_Distributor = Nothing
            'End If
            'DISPOSE DATAVIEW
            If Not IsNothing(Me.m_ViewAgreementBrand) Then
                Me.m_ViewAgreementBrand.Dispose()
                Me.m_ViewAgreementBrand = Nothing
            End If
            If Not IsNothing(Me.m_ViewMarketingProgram) Then
                Me.m_ViewMarketingProgram.Dispose()
                Me.m_ViewMarketingProgram = Nothing
            End If
            'If Not IsNothing(Me.m_ViewOrderAcceptance) Then
            '    Me.m_ViewOrderAcceptance.Dispose()
            '    Me.m_ViewOrderAcceptance = Nothing
            'End If
            If Not IsNothing(Me.m_ViewProject) Then
                Me.m_ViewProject.Dispose()
                Me.m_ViewProject = Nothing
            End If
            If Not IsNothing(Me.m_ViewPurchaseOrder) Then
                Me.m_ViewPurchaseOrder.Dispose()
                Me.m_ViewPurchaseOrder = Nothing
            End If
            If Not IsNothing(Me.m_ViewDistributor) Then
                Me.m_ViewDistributor.Dispose()
                Me.m_ViewDistributor = Nothing
            End If
            If Not IsNothing(Me.m_ViewOtherProgram) Then
                Me.m_ViewOtherProgram.Dispose()
                Me.m_ViewOtherProgram = Nothing
            End If
            If Not IsNothing(Me.m_ViewHK) Then
                Me.m_ViewHK.Dispose()
                Me.m_ViewHK = Nothing
            End If
            If Not IsNothing(Me.m_viewOThers) Then
                Me.m_viewOThers.Dispose()
                Me.m_viewOThers = Nothing
            End If
            '' TODO: free unmanaged resources when explicitly called
            'Me.disposedValue = True
            '' TODO: free shared unmanaged resources

        End Sub


#End Region

    End Class

End Namespace

