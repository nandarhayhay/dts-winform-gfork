Imports System.Data
Imports System.Data.SqlClient
Namespace PurchaseOrder
    Public Class PORegistering
        Inherits NufarmBussinesRules.PurchaseOrder.PO_BrandPack

#Region " DEKLARASI "
        Private m_ViewPORegistering As DataView
        Private VIEWPurchaseOrder As DataView ' dataview untuk purchase order
        Private m_ViewDistributor As DataView
        Private m_ViewPOBrandPack As DataView
        Private m_ViewAgreement As DataView
        Private m_ViewTargetReaching As DataView
        Private Query As String = ""
        Public DateFilterWith As FilterDatePOWith
#End Region

#Region " Enum "
        Public Enum FilterDatePOWith
            None
            DatePO
            CancelPO
            Plantation
        End Enum
#End Region
#Region " Function "
        Public Function HasExistBrandPack(ByVal PORefNO As String, ByVal mustCloseConnection As Boolean)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "  SELECT 1 WHERE EXISTS(SELECT PO_REF_NO FROM ORDR_PO_BRANDPACK WHERE PO_REF_NO = @PO_REF_NO);"
                If IsNothing(SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                AddParameter("@PO_REF_NO", SqlDbType.VarChar, PORefNO, 30)
                OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                If mustCloseConnection Then : Me.CloseConnection() : End If
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function getViewPOHeader(Optional ByVal StartDate As Object = Nothing, Optional ByVal EndDate As Object = Nothing) As DataSet
            Try

                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT PO_REF_NO,PO_REF_DATE,DISTRIBUTOR_ID,CREATE_DATE,CREATE_BY FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= @StartDate AND PO_REF_DATE <= @EndDate ;" & vbCrLf & _
                        "SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                        "SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= @StartDate AND PO_REF_DATE <= @EndDate AND DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) ;"
                If Not IsNothing(StartDate) And Not IsNothing(EndDate) Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT PO_REF_NO,PO_REF_DATE,DISTRIBUTOR_ID,CREATE_DATE,CREATE_BY FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= @StartDate AND PO_REF_DATE <= @EndDate ;" & vbCrLf & _
                            "SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                            "SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= @StartDate AND PO_REF_DATE <= @EndDate AND DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) ;"
                ElseIf Not IsNothing(StartDate) Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT PO_REF_NO,PO_REF_DATE,DISTRIBUTOR_ID,CREATE_DATE,CREATE_BY FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= @StartDate ;" & vbCrLf & _
                            "SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                            "SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE >= @StartDate AND DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) ;"
                ElseIf Not IsNothing(EndDate) Then
                    Query = "SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT PO_REF_NO,PO_REF_DATE,DISTRIBUTOR_ID,CREATE_DATE,CREATE_BY FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE <= @EndDate ;" & vbCrLf & _
                            "SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                            "SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE PO_REF_DATE <= @EndDate AND DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) ;"
                Else
                    Throw New Exception("You must define StartDate or EndDate to filter data")
                End If
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else
                    Me.ResetCommandText(CommandType.Text, Query)
                End If
                If Not IsNothing(StartDate) Then
                    Me.AddParameter("@StartDate", SqlDbType.DateTime, StartDate)
                End If
                If Not IsNothing(EndDate) Then
                    Me.AddParameter("@EndDate", SqlDbType.DateTime, EndDate)
                End If
                Dim DS As New DataSet("PO_HEADER") : DS.Clear()
                setDataAdapter(Me.SqlCom).Fill(DS)

                Me.CloseConnection() : Me.ClearCommandParameters()
                'Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                'Me.SqlDat.Fill(DS) : Me.ClearCommandParameters()
                'Dim DV As DataView = DS.Tables(0).DefaultView()
                'DVDistributor = DS.Tables(1).DefaultView()
                Return DS
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function CreateViewAgreement(ByVal DISTRIBUTOR_ID As String) As DataView
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                "SELECT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT WHERE DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "' ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                'Me.CreateCommandSql("", "SELECT DISTINCT AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT WHERE DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim tblAgreement As New DataTable("AGREEMENT")
                tblAgreement.Clear() : Me.FillDataTable(tblAgreement)
                Me.ClearCommandParameters() : Me.m_ViewAgreement = tblAgreement.DefaultView()
                Return Me.m_ViewAgreement

            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Overloads Function CreateViewBrandPackByDistributorID(ByVal searchPOREFNO As String, Optional ByVal DistributorID As Object = Nothing) As DataView
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                               "SELECT OPO.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,OPO.PO_REF_NO,OPO.PO_REF_DATE,OPB.PO_BRANDPACK_ID,BB.BRANDPACK_NAME," & vbCrLf & _
                               " OPB.PO_ORIGINAL_QTY AS QUANTITY,OPB.PO_PRICE_PERQTY AS [PRICE/QTY],PL.PLANTATION_NAME," & vbCrLf & _
                               " OPB.PO_ORIGINAL_QTY * OPB.PO_PRICE_PERQTY AS TOTAL," & vbCrLf & _
                               " OPB.BRANDPACK_ID,OPB.DESCRIPTIONS,OPB.DESCRIPTIONS2,OPB.ExcludeDPD, " & vbCrLf & _
                               " OPB.CREATE_DATE, OPB.MODIFY_DATE " & vbCrLf & _
                               " FROM ORDR_PURCHASE_ORDER OPO INNER JOIN DIST_DISTRIBUTOR DR ON OPO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID " & vbCrLf & _
                               " LEFT OUTER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = OPO.PO_REF_NO  " & vbCrLf & _
                               " LEFT OUTER JOIN PLANTATION PL ON PL.PLANTATION_ID = OPB.PLANTATION_ID " & vbCrLf & _
                               " LEFT OUTER JOIN BRND_BRANDPACK BB ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID  " & vbCrLf
                If Not IsNothing(DistributorID) Then
                    Query &= " WHERE OPO.DISTRIBUTOR_ID = @DistributorID  AND OPO.PO_REF_NO LIKE '%' + @PORefNo + '%' ;"
                Else : Query &= " WHERE OPO.PO_REF_NO LIKE '%' + @PORefNo + '%';"
                End If
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                If Not IsNothing(DistributorID) Then
                    Me.AddParameter("@DistributorID", SqlDbType.VarChar, DistributorID, 10)
                End If
                Me.AddParameter("@PORefNO", SqlDbType.VarChar, searchPOREFNO, 30)
                Me.m_ViewPORegistering = Me.FillDataTable(New DataTable("PO_BRANDPACK")).DefaultView
                Me.m_ViewPORegistering.Sort = "PO_REF_DATE DESC"
                Return Me.m_ViewPORegistering
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Function isPOPlantation(ByVal POBrandPackID As String) As Boolean
            Try
                Query = "SET NOCOUNT ON;" & vbCrLf & _
                "SELECT PLANTATION_ID FROM ORDR_PO_BRANDPACK WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, POBrandPackID)
                Me.OpenConnection()
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                Me.ClearCommandParameters()
                'Me.CloseConnection()
                If Not IsNothing(retval) And Not IsDBNull(retval) Then
                    Return (retval.ToString() <> "")
                End If
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        Public Overloads Function CreateViewBrandPackByDistributorID(ByVal FilterDatePO As FilterDatePOWith, Optional ByVal DistributorID As Object = Nothing, Optional ByVal StartDate As Object = Nothing, _
        Optional ByVal EndDate As Object = Nothing) As DataView
            Try
                Query = "SET NOCOUNT ON ;" & vbCrLf & _
                        "SELECT OPO.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,OPO.PO_REF_NO,OPO.PO_REF_DATE,OPO.PROJ_REF_NO,P.PROJECT_NAME,OPB.PO_BRANDPACK_ID,BR.BRAND_ID,BR.BRAND_NAME,BB.BRANDPACK_NAME," & vbCrLf & _
                        " OPB.PO_ORIGINAL_QTY AS QUANTITY,OPB.PO_PRICE_PERQTY AS [PRICE/QTY],PL.PLANTATION_NAME," & vbCrLf & _
                        " OPB.PO_ORIGINAL_QTY * OPB.PO_PRICE_PERQTY AS TOTAL," & vbCrLf & _
                        " OPB.BRANDPACK_ID,OPB.DESCRIPTIONS,OPB.DESCRIPTIONS2, " & vbCrLf & _
                        " OPB.CREATE_DATE, OPB.MODIFY_DATE,OPB.ExcludeDPD " & vbCrLf & _
                        " FROM ORDR_PURCHASE_ORDER OPO INNER JOIN DIST_DISTRIBUTOR DR ON OPO.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID " & vbCrLf & _
                        " LEFT OUTER JOIN PROJ_PROJECT P ON P.PROJ_REF_NO = OPO.PROJ_REF_NO " & vbCrLf & _
                        " LEFT OUTER JOIN ORDR_PO_BRANDPACK OPB ON OPB.PO_REF_NO = OPO.PO_REF_NO  " & vbCrLf & _
                        " LEFT OUTER JOIN PLANTATION PL ON PL.PLANTATION_ID = OPB.PLANTATION_ID " & vbCrLf & _
                        " LEFT OUTER JOIN BRND_BRANDPACK BB ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID  " & vbCrLf & _
                        " LEFT OUTER JOIN BRND_BRAND BR ON BB.BRAND_ID = BR.BRAND_ID " & vbCrLf   'WHERE OPO.PO_REF_DATE >= @StartDate AND OPO.PO_REF_DATE <= @EndDate AND OPO.DISTRIBUTOR_ID = @DistributorID ;"
                Dim HasWhereClause As Boolean = False
                If (Not IsNothing(StartDate) And Not IsNothing(EndDate)) Then
                    Query &= " WHERE OPO.PO_REF_DATE >= @StartDate AND OPO.PO_REF_DATE <= @EndDate " & vbCrLf
                    HasWhereClause = True
                End If
                Select Case FilterDatePO
                    Case FilterDatePOWith.CancelPO
                        If HasWhereClause Then : Query &= " AND OPB.PO_ORIGINAL_QTY = 0 " & vbCrLf
                        Else : Query &= " WHERE OPB.PO_ORIGINAL_QTY = 0 " & vbCrLf : HasWhereClause = True
                        End If
                    Case FilterDatePOWith.DatePO
                    Case FilterDatePOWith.None
                    Case FilterDatePOWith.Plantation
                        If HasWhereClause Then : Query &= " AND OPB.PLANTATION_ID IS NOT NULL AND LEN(OPB.PLANTATION_ID) > 0  AND OPB.PO_ORIGINAL_QTY > 0" & vbCrLf
                        Else : Query &= " WHERE OPB.PLANTATION_ID IS NOT NULL AND LEN(OPB.PLANTATION_ID) > 0  AND OPB.PO_ORIGINAL_QTY > 0" & vbCrLf : HasWhereClause = True
                        End If
                End Select
                If Not IsNothing(DistributorID) Then
                    If HasWhereClause Then : Query &= " AND OPO.DISTRIBUTOR_ID = @DistributorID ;" & vbCrLf
                    Else : Query &= " WHERE OPO.DISTRIBUTOR_ID = @DistributorID ;"
                    End If
                End If

                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else
                    Me.ResetCommandText(CommandType.Text, Query)
                End If
                If Not IsNothing(DistributorID) Then
                    Me.AddParameter("@DistributorID", SqlDbType.VarChar, DistributorID, 15)
                End If
                If Not IsNothing(StartDate) Then
                    Me.AddParameter("@StartDate", SqlDbType.DateTime, StartDate)
                End If
                If Not IsNothing(EndDate) Then
                    Me.AddParameter("@EndDate", SqlDbType.DateTime, EndDate)
                End If
                Me.m_ViewPORegistering = Me.FillDataTable(New DataTable("PO_BRANDPACK")).DefaultView
                Me.m_ViewPORegistering.Sort = "PO_REF_DATE DESC"
                Return Me.m_ViewPORegistering
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Overloads Function CreateViewBrandPackByDistributor(ByVal DistributorID As String) As DataView
            Try
                Query = "SET NOCOUNT ON;SELECT DISTINCT OPB.BRANDPACK_ID,BB.BRANDPACK_NAME FROM ORDR_PO_BRANDPACK OPB " & vbCrLf & _
                " INNER JOIN BRND_BRANDPACK BB ON OPB.BRANDPACK_ID = BB.BRANDPACK_ID INNER JOIN ORDR_PURCHASE_ORDER OPO " _
                & " ON OPO.PO_REF_NO = OPB.PO_REF_NO WHERE OPO.DISTRIBUTOR_ID = '" & DistributorID & "';"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Dim dtTable As New DataTable("T_POBrandPack")
                dtTable.Clear() : Me.FillDataTable(dtTable)
                Me.m_ViewPOBrandPack = dtTable.DefaultView()
                'Me.m_ViewPOBrandPack = Me.baseChekTable.DefaultView()
                Me.m_ViewPOBrandPack.Sort = "BRANDPACK_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewPOBrandPack
        End Function
        Public Function CreateViewDistributorPO() As DataView
            Try
                Query = "SET NOCOUNT ON;SELECT DR.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR" & vbCrLf & _
                           " WHERE EXISTS(SELECT DISTRIBUTOR_ID FROM ORDR_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) ;"
                Me.CreateCommandSql("sp_executesql", "") : Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.m_ViewDistributor = Me.FillDataTable(New DataTable("Distributor")).DefaultView
                Me.m_ViewDistributor.Sort = "DISTRIBUTOR_ID"
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistributor
        End Function
        Public Function getDistributor() As DataView
            Try
                Return MyBase.CreateViewDistributor(False)
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function

        Public Sub Save(ByVal SaveMode As common.Helper.SaveMode, ByRef DS As DataSet, Optional ByVal StartDate As Object = Nothing, Optional ByVal EndDate As Object = Nothing)
            Try
                Me.OpenConnection()
                If SaveMode = common.Helper.SaveMode.Insert Then
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "SELECT 1 WHERE EXISTS(SELECT PO_REF_NO FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO = @PO_REF_NO) ;"
                    If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                    Else : Me.ResetCommandText(CommandType.Text, Query)
                    End If
                    Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, Me.PO_REF_NO, 30)
                    Dim RetVal As Object = Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                    If Not IsNothing(RetVal) Then
                        If Convert.ToInt16(RetVal) > 0 Then
                            Throw New Exception("PO_REF_NO has existed !")
                        End If
                    End If
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            " INSERT INTO ORDR_PURCHASE_ORDER(PO_REF_NO,PO_REF_DATE,DISTRIBUTOR_ID,CREATE_BY,CREATE_DATE) " & vbCrLf & _
                            " VALUES(@PO_REF_NO,@PO_REF_DATE,@DISTRIBUTOR_ID,@CREATE_BY,@CREATE_DATE) ;"
                Else
                    Query = "SET NOCOUNT ON ;" & vbCrLf & _
                            "UPDATE ORDR_PURCHASE_ORDER SET PO_REF_DATE = @PO_REF_DATE,DISTRIBUTOR_ID = @DISTRIBUTOR_ID,MODIFY_BY = @MODIFY_BY,MODIFY_DATE = @MODIFY_DATE " & vbCrLf & _
                            " WHERE PO_REF_NO = @PO_REF_NO ;"
                End If
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else
                    Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, Me.PO_REF_NO, 30)
                Me.AddParameter("@PO_REF_DATE", SqlDbType.DateTime, Me.PO_REF_DATE)
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, Me.DistributorID, 10)
                If SaveMode = common.Helper.SaveMode.Insert Then
                    Me.AddParameter("@CREATE_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.AddParameter("@CREATE_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Else
                    Me.AddParameter("@MODIFY_BY", SqlDbType.VarChar, NufarmBussinesRules.User.UserLogin.UserName, 50)
                    Me.AddParameter("@MODIFY_DATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                End If
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                DS = Me.getViewPOHeader(StartDate, EndDate)
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub

        Public Shadows Function CreateViewDistributor() As DataView
            Try
                'Me.SearcData("", "SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM VIEW_DISTRIBUTOR")
                'Me.m_ViewDistributor = Me.baseChekTable.DefaultView()
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        " SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR ;"
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("sp_executesql", "")
                Else
                    Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.m_ViewDistributor = Me.FillDataTable(New DataTable("T_Distributor")).DefaultView()
                Return Me.m_ViewDistributor
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewDistributor
        End Function
        Public Function CreateViewTargetReaching(ByVal AgreementNO As String, ByVal DistributorID As String, ByVal Flag As String) As DataView
            Try
                Me.CreateCommandSql("Usp_Create_View_Target_Reaching", "")
                Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10) ' VARCHAR(10),
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNO, 25) ' VARCHAR(25),
                Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2) ' VARCHAR(2)
                Dim tblTargetReaching As New DataTable("REACHING_QTY")
                tblTargetReaching.Clear()
                Me.FillDataTable(tblTargetReaching)
                Me.m_ViewTargetReaching = tblTargetReaching.DefaultView()
                Return Me.m_ViewTargetReaching
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Sub SetExecludeDPD(ByVal isExclude As Boolean, ByVal POBrandPackID As String)
            Try
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                " UPDATE ORDR_PO_BRANDPACK SET ExcludeDPD = @ExcludeDPD WHERE PO_BRANDPACK_ID = @PO_BRANDPACK_ID ;"
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@ExcludeDPD", SqlDbType.Bit, isExclude)
                Me.AddParameter("@PO_BRANDPACK_ID", SqlDbType.VarChar, POBrandPackID)
                Me.OpenConnection() : Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters() : Me.CloseConnection()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex

            End Try
        End Sub
        Public Function CreateViewPOByDistributor(ByVal DISTRIBUTOR_ID As String) As DataView
            Try
                Me.SearcData("", "SELECT * FROM VIEW_PURCHASE_ORDER WHERE DISTRIBUTOR_ID = '" & DISTRIBUTOR_ID & "'")
                Me.m_ViewPORegistering = Me.baseChekTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
            Return Me.m_ViewPORegistering
        End Function
        Public Function getFlag(ByVal AgreementNO As String) As Object
            Try
                Query = "SET NOCOUNT ON;SELECT QS_TREATMENT_FLAG FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AgreementNO & "'"
                Me.CreateCommandSql("sp_executesql", "")
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Return Me.ExecuteScalar()
                'Return Me.ExecuteScalar("", "SET NOCOUNT ON;SELECT QS_TREATMENT_FLAG FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = '" & AgreementNO & "'")
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Function

        Public Function getOADateAndSPPBDate(ByVal PoRefNo As String) As List(Of DateTime)
            Dim listDate As New List(Of DateTime)
            Try
                Query = "SET NOCOUNT ON ; " & vbCrLf & _
                         "SELECT OA_DATE FROM ORDR_ORDER_ACCEPTANCE WHERE PO_REF_NO = @PO_REF_NO; "
                If IsNothing(Me.SqlCom) Then
                    Me.CreateCommandSql("", Query)
                Else : Me.ResetCommandText(CommandType.Text, Query)
                End If
                Me.AddParameter("@PO_REF_NO", SqlDbType.VarChar, PoRefNo, 30)
                Me.OpenConnection()
                Me.SqlRe = Me.SqlCom.ExecuteReader()
                While Me.SqlRe.Read()
                    listDate.Add(Me.SqlRe.GetDateTime(0))
                End While : Me.SqlRe.Close()
                Query = "SET NOCOUNT ON; " & vbCrLf & _
                        "SELECT SPPB_DATE FROM SPPB_HEADER WHERE PO_REF_NO = @PO_REF_NO ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Dim retval As Object = Me.SqlCom.ExecuteScalar()
                If Not IsNothing(retval) Then
                    listDate.Add(retval)
                End If : Me.CloseConnection() : Me.ClearCommandParameters()
                Return listDate
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
#End Region

#Region " Sub "
        'Public Sub FetchDataView()
        '    Try
        '        Me.GetConnection()
        '        Me.CreateCommandSql("Sp_GetView_PURCHASE_ORDER_AGREE_STILL_APPLY", "")
        '        Dim tblPurchaseOrder As New DataTable("PURCHASE_ORDER")
        '        tblPurchaseOrder.Clear()
        '        Me.FillDataTable(tblPurchaseOrder)
        '        Me.VIEWPurchaseOrder = tblPurchaseOrder.DefaultView
        '    Catch ex As Exception
        '        Me.CloseConnection()
        '        Throw ex
        '    End Try
        'End Sub

        Public Sub FetchDataView_1()
            Try
                Me.GetConnection()
                'Me.GetdatasetRelation("StoredProcedure", "Sp_Select_PurchaseOrder", "Sp_Select_ORDR_PO_BRANDPACK", "Purchase_Order", "Purchase_Order_BrandPack", "PO_REF_NO", "PO_REF_NO", "PO_POBRANDPACK", "Update")
                'Me.DVMPurchaseOrder = New DataViewManager()
                'Me.DVMPurchaseOrder.DataSet = Me.baseDataSetRelation
                'Me.DVMPurchaseOrder.DataViewSettings("Purchase_Order").Sort = "PO_REF_NO"
                'Me.DVMPurchaseOrder.DataViewSettings("Purchase_Order_BrandPack").Sort = "PO_REF_NO"
                Me.SearcData("", "SELECT * FROM VIEW_PURCHASE_ORDER")
                Me.m_ViewPORegistering = Me.baseChekTable.DefaultView()
                'Me.CreateCommandSql("", "SELECT * FROM VIEW_PURCHASE_ORDER")
                'Dim tblPurchaseOrder As New DataTable("PURCHASE_ORDER")
                'tblPurchaseOrder.Clear()
                'Me.FillDataTable(tblPurchaseOrder)
                'Me.VIEWPurchaseOrder = tblPurchaseOrder.DefaultView
            Catch ex As Exception
                Me.CloseConnection()
                Throw ex
            End Try
        End Sub
#End Region

#Region " Property "

        Public ReadOnly Property ViewTargetReaching() As DataView
            Get
                Return Me.m_ViewTargetReaching
            End Get
        End Property
        Public ReadOnly Property ViewPORegistering() As DataView
            Get
                Return Me.m_ViewPORegistering
            End Get
        End Property
        Public ReadOnly Property GetDataViewForGridEx1() As DataView
            Get
                Return Me.VIEWPurchaseOrder
            End Get
        End Property
        Public Shadows Property ViewDistributor() As DataView

            Get
                Return Me.m_ViewDistributor
            End Get
            Set(ByVal value As DataView)
                Me.m_ViewDistributor = value
            End Set
        End Property
        Public Shadows ReadOnly Property ViewPOBrandPack() As DataView
            Get
                Return Me.m_ViewPOBrandPack
            End Get
        End Property
        Public ReadOnly Property ViewAgreement() As DataView
            Get
                Return Me.m_ViewAgreement
            End Get
        End Property
#End Region

#Region " Constructor / Destructor "
        Public Sub New()
            MyBase.New()
        End Sub
        Public Overloads Sub Dispose(ByVal disposing As Boolean)
            MyBase.Dispose(disposing)
            If Not IsNothing(Me.m_ViewPORegistering) Then
                Me.m_ViewPORegistering.Dispose()
                Me.m_ViewPORegistering = Nothing
            End If
            If Not IsNothing(Me.VIEWPurchaseOrder) Then
                Me.VIEWPurchaseOrder.Dispose()
                Me.VIEWPurchaseOrder = Nothing
            End If
            If Not IsNothing(Me.m_ViewPOBrandPack) Then
                Me.m_ViewPOBrandPack.Dispose()
                Me.m_ViewPOBrandPack = Nothing
            End If
            If Not IsNothing(Me.m_ViewAgreement) Then
                Me.m_ViewAgreement.Dispose()
                Me.m_ViewAgreement = Nothing
            End If
            If Not IsNothing(Me.m_ViewTargetReaching) Then
                Me.m_ViewTargetReaching.Dispose()
                Me.m_ViewTargetReaching = Nothing
            End If
        End Sub
#End Region

    End Class
End Namespace

