Imports System.Data
Imports System.Data.SqlClient
Imports NufarmBussinesRules
Namespace DistributorAgreement
    Public Class DPDAchievement
        Inherits NufarmBussinesRules.DistributorAgreement.Target_Agreement
        Public Sub New()
            MyBase.New()
        End Sub
        Private tblDiscProgressive As DataTable = Nothing
        Private tblPrevAchievement As DataTable = Nothing
        Private tblCurAchiement As DataTable = Nothing
        Private IsTransitionTime As Boolean = False
        Private curAgreeStartDate As Object = Nothing
        Private curAgreeEndDate As Object = Nothing
        Private PrevAgreementNo As String = ""
        Private Sub CreateOrRecreatTblAchHeader(ByRef tblAchHeader As DataTable)
            'Dim AchHeader As New DataTable("T_AccrHeader") : AchHeader.Clear()
            If (tblAchHeader.Columns.Count > 0) Then
                tblAchHeader.Rows.Clear() : tblAchHeader.AcceptChanges() : Return
            End If
            If IsNothing(tblAchHeader) Then
                tblAchHeader = New DataTable("T_AcrHeader")
            End If : tblAchHeader.Clear()
            With tblAchHeader.Columns
                ' ACH_HEADER_ID, DISTRIBUTOR_ID, AGREEMENT_NO, BRAND_ID,FLAG
                .Add(New DataColumn("ACH_HEADER_ID", Type.GetType("System.String")))
                .Add(New DataColumn("AGREEMENT_NO", Type.GetType("System.String")))
                .Add(New DataColumn("DISTRIBUTOR_ID", Type.GetType("System.String")))
                .Add(New DataColumn("AGREE_BRAND_ID", Type.GetType("System.String")))
                .Add(New DataColumn("BRAND_ID", Type.GetType("System.String")))
                .Add(New DataColumn("FLAG", Type.GetType("System.String")))
                'TOTAL_TARGET
                .Add(New DataColumn("TOTAL_TARGET", Type.GetType("System.Decimal")))
                .Item("TOTAL_TARGET").DefaultValue = 0

                .Add(New DataColumn("TARGET_VALUE", Type.GetType("System.Decimal")))
                .Item("TARGET_VALUE").DefaultValue = 0

                .Add(New DataColumn("TARGET_FM", Type.GetType("System.Decimal")))
                .Item("TARGET_FM").DefaultValue = 0
                .Add(New DataColumn("TARGET_PL", Type.GetType("System.Decimal")))
                .Item("TARGET_PL").DefaultValue = 0
                'DISPRO
                .Add(New DataColumn("DISPRO", Type.GetType("System.Decimal")))
                .Item("DISPRO").DefaultValue = 0
                'ACH_HEADER_ID, DISTRIBUTOR_ID, AGREEMENT_NO, BRAND_ID, TARGET_FM, TARGET_PL, TARGET_VALUE, 

                'TOTAL_ACTUAL
                .Add(New DataColumn("TOTAL_ACTUAL", Type.GetType("System.Decimal")))
                .Item("TOTAL_ACTUAL").DefaultValue = 0
                'ACH_DISPRO
                .Add(New DataColumn("ACH_DISPRO", Type.GetType("System.Decimal")))
                .Item("ACH_DISPRO").DefaultValue = 0
                'DISC_QTY
                .Add(New DataColumn("DISC_QTY", Type.GetType("System.Decimal")))
                .Item("DISC_QTY").DefaultValue = 0

                'TOTAL_PO,
                .Add(New DataColumn("TOTAL_PO", Type.GetType("System.Decimal")))
                .Item("TOTAL_PO").DefaultValue = 0
                'TOTAL_PO_VALUE, 
                .Add(New DataColumn("TOTAL_PO_VALUE", Type.GetType("System.Decimal")))
                .Item("TOTAL_PO_VALUE").DefaultValue = 0
                'BALANCE, 
                .Add(New DataColumn("BALANCE", Type.GetType("System.Decimal")))
                .Item("BALANCE").DefaultValue = 0
                'TOTAL_PBQ3, 
                .Add(New DataColumn("TOTAL_PBQ3", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBQ3").DefaultValue = 0

                '.Add(New DataColumn("TOTAL_PBS2", Type.GetType("System.Decimal")))
                '.Item("TOTAL_PBS2").DefaultValue = 0
                '.Add(New DataColumn("TOTAL_JOIN_PBS2", Type.GetType("System.Decimal")))
                '.Item("TOTAL_JOIN_PBS2").DefaultValue = 0

                '  TOTAL_CPQ2, TOTAL_PBQ2, 
                .Add(New DataColumn("TOTAL_CPQ2", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ2").DefaultValue = 0

                ' TOTAL_CPQ3,
                .Add(New DataColumn("TOTAL_CPQ3", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ3").DefaultValue = 0

                '.Add(New DataColumn("TOTAL_CPS1", Type.GetType("System.Decimal")))
                '.Item("TOTAL_CPS1").DefaultValue = 0

                '.Add(New DataColumn("TOTAL_JOIN_CPS1", Type.GetType("System.Decimal")))
                '.Item("TOTAL_JOIN_CPS1").DefaultValue = 0

                '.Add(New DataColumn("TOTAL_PBY", Type.GetType("System.Decimal")))
                '.Item("TOTAL_PBY").DefaultValue = 0
                '.Add(New DataColumn("TOTAL_JOIN_PBY", Type.GetType("System.Decimal")))
                '.Item("TOTAL_JOIN_PBY").DefaultValue = 0

                .Add(New DataColumn("DESCRIPTIONS", Type.GetType("System.String")))
                .Item("DESCRIPTIONS").DefaultValue = String.Empty

                .Add(New DataColumn("CreatedBy", Type.GetType("System.String")))
                .Item("CreatedBy").DefaultValue = NufarmBussinesRules.User.UserLogin.UserName
                .Add(New DataColumn("CreatedDate", Type.GetType("System.DateTime")))
                .Item("CreatedDate").DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()
                .Add(New DataColumn("ModifiedBy", Type.GetType("System.String")))
                .Item("ModifiedBy").DefaultValue = NufarmBussinesRules.User.UserLogin.UserName
                .Add(New DataColumn("ModifiedDate", Type.GetType("System.DateTime")))
                .Item("ModifiedDate").DefaultValue = NufarmBussinesRules.SharedClass.ServerDate

                ''untuk perhitungan saja bukan untuk ke database
                .Add(New DataColumn("IsNew", Type.GetType("System.Boolean")))
                .Item("IsNew").DefaultValue = DBNull.Value

                .Add(New DataColumn("IsChanged", Type.GetType("System.Boolean")))
                .Item("IsChanged").DefaultValue = DBNull.Value
                ' TOTAL_CPF1, TOTAL_CPF2, 
                .Add(New DataColumn("TOTAL_CPF1", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPF1").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPF2", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPF2").DefaultValue = 0

                'TOTAL_PBF3
                .Add(New DataColumn("TOTAL_PBF3", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBF3").DefaultValue = 0

                .Add(New DataColumn("GPPBF3", Type.GetType("System.Decimal"))) ''diambil dari progressive discount sebelumnya
                .Item("GPPBF3").DefaultValue = 0

                ''untuk perhitungan saja bukan untuk ke database
                .Add(New DataColumn("GPCPQ1", Type.GetType("System.Decimal")))
                .Item("GPCPQ1").DefaultValue = 0

                .Add(New DataColumn("GPCPQ2", Type.GetType("System.Decimal")))
                .Item("GPCPQ2").DefaultValue = 0

                .Add(New DataColumn("GPCPQ3", Type.GetType("System.Decimal")))
                .Item("GPCPQ3").DefaultValue = 0

                .Add(New DataColumn("GPCPF1", Type.GetType("System.Decimal")))
                .Item("GPCPF1").DefaultValue = 0

                .Add(New DataColumn("GPCPF2", Type.GetType("System.Decimal")))
                .Item("GPCPF2").DefaultValue = 0

                .Add(New DataColumn("GPCPF3", Type.GetType("System.Decimal")))
                .Item("GPCPF3").DefaultValue = 0

                '.Add(New DataColumn("GPPBY", Type.GetType("System.Decimal")))
                ''.Item("GPPBY").DefaultValue = 0

            End With
            ''create primary key
            Dim Key(1) As DataColumn : Key(0) = tblAchHeader.Columns("ACH_HEADER_ID")
            tblAchHeader.PrimaryKey = Key
        End Sub

        Private Sub CreateOrRecreateTblAchDetail(ByRef tblAchDetail As DataTable)
            If (tblAchDetail.Columns.Count > 0) Then
                tblAchDetail.Rows.Clear() : tblAchDetail.AcceptChanges() : Return
            End If
            If IsNothing(tblAchDetail) Then
                tblAchDetail = New DataTable("T_AcrDetail")
            End If : tblAchDetail.Clear()
            With tblAchDetail.Columns
                .Add(New DataColumn("ACH_HEADER_ID", Type.GetType("System.String")))
                .Add(New DataColumn("BRANDPACK_ID", Type.GetType("System.String")))
                .Add(New DataColumn("ACH_DETAIL_ID", Type.GetType("System.String")))

                .Add(New DataColumn("TOTAL_PO", Type.GetType("System.Decimal")))
                .Item("TOTAL_PO").DefaultValue = 0

                .Add(New DataColumn("TOTAL_ACTUAL", Type.GetType("System.Decimal")))
                .Item("TOTAL_ACTUAL").DefaultValue = 0

                .Add(New DataColumn("DISC_QTY", Type.GetType("System.Decimal")))
                .Item("DISC_QTY").DefaultValue = 0

                .Add(New DataColumn("TOTAL_PO_VALUE", Type.GetType("System.Decimal")))
                .Item("TOTAL_PO_VALUE").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPQ1", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ1").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPQ2", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ2").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPQ3", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPQ3").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPF1", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPF1").DefaultValue = 0

                .Add(New DataColumn("TOTAL_CPF2", Type.GetType("System.Decimal")))
                .Item("TOTAL_CPF2").DefaultValue = 0

                .Add(New DataColumn("TOTAL_PBF3", Type.GetType("System.Decimal")))
                .Item("TOTAL_PBF3").DefaultValue = 0

                .Add(New DataColumn("DESCRIPTIONS", Type.GetType("System.String")))
                .Item("DESCRIPTIONS").DefaultValue = String.Empty
                .Add(New DataColumn("CreatedBy", Type.GetType("System.String")))
                .Item("CreatedBy").DefaultValue = NufarmBussinesRules.User.UserLogin.UserName

                .Add(New DataColumn("CreatedDate", Type.GetType("System.DateTime")))
                .Item("CreatedDate").DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()

                .Add(New DataColumn("ModifiedBy", Type.GetType("System.String")))
                .Item("ModifiedBy").DefaultValue = NufarmBussinesRules.User.UserLogin.UserName
                .Add(New DataColumn("ModifiedDate", Type.GetType("System.DateTime")))
                .Item("ModifiedDate").DefaultValue = NufarmBussinesRules.SharedClass.ServerDate()

                .Add(New DataColumn("IsNew", Type.GetType("System.Boolean")))
                .Item("IsNew").DefaultValue = DBNull.Value
                .Add(New DataColumn("IsChanged", Type.GetType("System.Boolean")))
                .Item("IsChanged").DefaultValue = DBNull.Value
            End With
            Dim Key(1) As DataColumn : Key(0) = tblAchDetail.Columns("ACHIEVEMENT_BRANDPACK_ID")
            tblAchDetail.PrimaryKey = Key
        End Sub

        Public Shadows Function GetDistributorAgrement(ByVal Flag As String, Optional ByVal Searchstring As String = "") As DataView
            Try
                'Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                '        "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                '        " WHERE AA.END_DATE >=  GETDATE() AND AA.QS_TREATMENT_FLAG = '" & Flag.Remove(1, 1) & "' AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                '        " AND DR.DISTRIBUTOR_NAME LIKE '%" & Searchstring & "%';"
                Flag = Flag.Remove(1, 1)
                If Flag = "Q" Or Flag = "F" Then
                    If Searchstring = "" Then
                        Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                                "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                " WHERE YEAR(AA.END_DATE) >=  YEAR(@GETDATE) - 1 AND AA.QS_TREATMENT_FLAG = @FLAG AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) "
                    Else
                        'hilangkan end_date karene custom search
                        Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                                 "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                 " WHERE AA.QS_TREATMENT_FLAG = @FLAG AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                                 " AND DR.DISTRIBUTOR_NAME LIKE '%" & Searchstring & "%';"
                    End If
                End If


                If Not Flag = "Y" Then
                    Flag = Flag.Remove(1, 1)
                    If Searchstring = "" Then
                        Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                                "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                " WHERE YEAR(AA.END_DATE) >=  YEAR(@GETDATE) - 1 AND AA.QS_TREATMENT_FLAG = @FLAG AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) "
                    Else
                        'hilangkan end_date karene custom search
                        Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                                 "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                 " WHERE AA.QS_TREATMENT_FLAG = @FLAG AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                                 " AND DR.DISTRIBUTOR_NAME LIKE '%" & Searchstring & "%';"
                    End If
                Else
                    If Searchstring = "" Then
                        Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                                "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                " WHERE YEAR(AA.END_DATE) >=  YEAR(@GETDATE) - 1  AND DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) "
                    Else
                        'hilangkan end_date karene custom search
                        Query = "SET NOCOUNT ON;SELECT DISTRIBUTOR_ID,DISTRIBUTOR_NAME FROM DIST_DISTRIBUTOR DR WHERE EXISTS(" & vbCrLf & _
                                 "SELECT DA.DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                                 " WHERE DA.DISTRIBUTOR_ID = DR.DISTRIBUTOR_ID) " & vbCrLf & _
                                 " AND DR.DISTRIBUTOR_NAME LIKE '%" & Searchstring & "%';"
                    End If
                End If

                Me.CreateCommandSql("", Query)
                Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                Me.AddParameter("@FLAG", SqlDbType.Char, Flag, 1)
                Dim dtTable As New DataTable("T_Distributor")
                dtTable.Clear() : Me.FillDataTable(dtTable)
                Return dtTable.DefaultView()
            Catch ex As Exception
                Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Function
        'get accrued
        Public Shadows Function getAccrued(ByVal Flag As String, Optional ByVal DISTRIBUTOR_ID As String = "", Optional ByVal ListAGREEMENT_NO As List(Of String) = Nothing) As DataSet

            Try
                Dim strAgreementNos As String = "IN('"
                If Not IsNothing(ListAGREEMENT_NO) Then
                    If ListAGREEMENT_NO.Count > 0 Then
                        For I As Integer = 0 To ListAGREEMENT_NO.Count - 1
                            strAgreementNos &= "" & ListAGREEMENT_NO(I) & "'"
                            If I < ListAGREEMENT_NO.Count - 1 Then
                                strAgreementNos &= ",'"
                            End If
                        Next
                    End If
                End If
                strAgreementNos &= ")"
                'ACH_HEADER_ID, DISTRIBUTOR_ID, DISTRIBUTOR_NAME, AGREEMENT_NO, START_DATE, END_DATE, FLAG, BRAND_ID, 
                'BRAND_NAME, AVG_PRICE_FM, AVG_PRICE_PL, TARGET_FM, TARGET_PL, TARGET_VALUE, TOTAL_PO_VALUE, TOTAL_TARGET, TOTAL_PO, TOTAL_ACTUAL, BALANCE,
                'ACHIEVEMENT_DISPRO, DISPRO, DISC_QTY, TOTAL_PBQ1, TOTAL_PBQ2, TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3

                '-----------------------------------Header Query -->achievement header--------------------------
                Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                        "SELECT ACRH.ACH_HEADER_ID,ACRH.DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME,ACRH.AGREEMENT_NO,AA.START_DATE,AA.END_DATE,ACRH.FLAG,ACRH.BRAND_ID,BB.BRAND_NAME," & vbCrLf & _
                        "ISNULL(AP.AVGPRICE,0) AS AVG_PRICE_FM,ISNULL(AP.AVGPRICE_PL,0) AS AVG_PRICE_PL,ACRH.TARGET_FM,ACRH.TARGET_PL,ACRH.TARGET_VALUE,ACRH.TOTAL_PO_VALUE,ACRH.TOTAL_TARGET," & vbCrLf & _
                        " ACRH.TOTAL_PO,ACRH.TOTAL_ACTUAL AS ACTUAL,ACRH.BALANCE,ACRH.ACH_DISPRO / 100 AS ACHIEVEMENT_DISPRO,ACRH.DISPRO / 100 AS DISPRO,  " & vbCrLf & _
                        " ACRH.DISC_QTY, ACRH.TOTAL_PBQ1,ACRH.TOTAL_PBQ2, " & vbCrLf & _
                        " ACRH.TOTAL_PBQ3,ACRH.TOTAL_CPQ2,ACRH.TOTAL_CPQ3, " & vbCrLf & _
                        " ACRH.TOTAL_CPF1,ACRH.TOTAL_CPF2,TOTAL_PBF3,ACRH.[DESCRIPTIONS]" & vbCrLf & _
                        " FROM ACHIEVEMENT_HEADER ACRH INNER JOIN AGREE_AGREEMENT AA ON ACRH.AGREEMENT_NO = AA.AGREEMENT_NO INNER JOIN DIST_DISTRIBUTOR DR ON ACRH.DISTRIBUTOR_ID " & vbCrLf & _
                        " = DR.DISTRIBUTOR_ID INNER JOIN BRND_BRAND BB ON BB.BRAND_ID = ACRH.BRAND_ID " & vbCrLf & _
                        "  LEFT OUTER JOIN BRND_AVGPRICE AP ON AP.IDApp = ACRH.AvgPriceID " & vbCrLf
                If ((DISTRIBUTOR_ID <> "") And (Not IsNothing(ListAGREEMENT_NO))) Then
                    If ListAGREEMENT_NO.Count > 0 Then
                        Query &= " WHERE ACRH.AGREEMENT_NO  " & strAgreementNos & " AND ACRH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                    ElseIf DISTRIBUTOR_ID <> "" Then
                        Query &= " WHERE ACRH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ACRH.AGREEMENT_NO " & vbCrLf & _
                                 " = ANY(SELECT DA.AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA " & vbCrLf & _
                                 "       ON DA.AGREEMENT_NO = AA.AGREEMENT_NO WHERE DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                                 "       AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 2 ) AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                    End If
                ElseIf Not IsNothing(ListAGREEMENT_NO) Then
                    Query &= " WHERE ACRH.AGREEMENT_NO  " & strAgreementNos & " AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN); "
                ElseIf (DISTRIBUTOR_ID <> "") Then
                    Query &= " WHERE ACRH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ACRH.AGREEMENT_NO " & vbCrLf & _
                             " = ANY(SELECT DA.AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA " & vbCrLf & _
                             "       ON DA.AGREEMENT_NO = AA.AGREEMENT_NO WHERE DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                             "       AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 2 ) AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                Else
                    Query &= " WHERE ACRH.AGREEMENT_NO = ANY(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) - 2 " & vbCrLf & _
                             "                               )  AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                End If
                Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                If Not String.IsNullOrEmpty(DISTRIBUTOR_ID) Then
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID)
                End If
                Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate())
                Me.baseDataSet = New DataSet("DSAchievement") : Me.baseDataSet.Clear()
                Dim dtTableHeader As New DataTable("ACHIEVEMENT_HEADER") : dtTableHeader.Clear()
                Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                Me.OpenConnection() : Me.SqlDat.Fill(dtTableHeader)

                'ACH_DETAIL_ID, ACH_HEADER_ID, AGREEMENT_NO,BRANDPACK_ID, BRANDPACK_NAME,  TOTAL_PO, TOTAL_ACTUAL, DISC_QTY, CREATE_BY, TOTAL_CPQ1,
                'TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3, DESCRIPTIONS
                '--------------------------Detail Query -->Achievement detail---------------------------------------
                Query = " SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; SELECT ACD.ACH_DETAIL_ID,ACD.ACH_HEADER_ID,ACRH.AGREEMENT_NO,ACD.BRANDPACK_ID,BP.BRANDPACK_NAME," & vbCrLf & _
                          "ACD.TOTAL_PO,ACD.TOTAL_ACTUAL, ACD.DISC_QTY," & vbCrLf & _
                          " ACD.TOTAL_CPQ1,ACD.TOTAL_CPQ2,ACD.TOTAL_CPQ3,ACD.TOTAL_CPF1,ACD.TOTAL_CPF2,ACD.TOTAL_PBF3,ACD.[DESCRIPTIONS]" & vbCrLf & _
                          " FROM ACHIEVEMENT_DETAIL ACD INNER JOIN ACHIEVEMENT_HEADER ACRH " & vbCrLf & _
                          " ON ACD.ACH_HEADER_ID = ACRH.ACH_HEADER_ID INNER JOIN BRND_BRANDPACK BP " & vbCrLf & _
                          "  ON ACD.BRANDPACK_ID = BP.BRANDPACK_ID " & vbCrLf
                If ((DISTRIBUTOR_ID <> "") And (Not IsNothing(ListAGREEMENT_NO))) Then
                    If ListAGREEMENT_NO.Count > 0 Then
                        Query &= " WHERE ACRH.AGREEMENT_NO  " & strAgreementNos & " AND ACRH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                    ElseIf DISTRIBUTOR_ID <> "" Then
                        Query &= " WHERE ACRH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID  AND ACRH.AGREEMENT_NO " & vbCrLf & _
                                 " = ANY(SELECT DA.AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA " & vbCrLf & _
                                 "       ON DA.AGREEMENT_NO = AA.AGREEMENT_NO WHERE DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                                 "       AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 2) AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                    End If
                ElseIf Not IsNothing(ListAGREEMENT_NO) Then
                    If ListAGREEMENT_NO.Count > 0 Then
                        Query &= " WHERE ACRH.AGREEMENT_NO  " & strAgreementNos & " AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                    Else
                        Query &= " WHERE ACRH.AGREEMENT_NO = ANY(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) - 2 " & vbCrLf & _
                                "         )  AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                    End If

                ElseIf (DISTRIBUTOR_ID <> "") Then
                    Query &= " WHERE ACRH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID  AND ACRH.AGREEMENT_NO " & vbCrLf & _
                             " = ANY(SELECT DA.AGREEMENT_NO FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_AGREEMENT AA " & vbCrLf & _
                             "       ON DA.AGREEMENT_NO = AA.AGREEMENT_NO WHERE DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                             "       AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 2 ) AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                Else
                    Query &= " WHERE ACRH.AGREEMENT_NO = ANY(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT WHERE YEAR(END_DATE) >= YEAR(@GETDATE) - 2 " & vbCrLf & _
                             "                               )  AND ACRH.FLAG = '" & Flag & "' OPTION(KEEP PLAN);"
                End If
                Me.ResetCommandText(CommandType.Text, Query)
                Dim dtTableDetail As New DataTable("ACHIEVEMENT_DETAIL") : dtTableDetail.Clear()
                Me.SqlDat.Fill(dtTableDetail) : Me.ClearCommandParameters()

                Me.baseDataSet.Tables.Add(dtTableHeader) : Me.baseDataSet.Tables.Add(dtTableDetail)
                Dim tblBrand As DataTable = dtTableHeader.Copy()
                tblBrand.TableName = "T_BRAND"
                tblBrand.AcceptChanges()
                Dim cols(tblBrand.Columns.Count - 1) As DataColumn
                For i As Integer = 0 To tblBrand.Columns.Count - 1
                    cols(i) = tblBrand.Columns(i)
                Next
                For Each col As DataColumn In cols
                    If col.ColumnName <> "BRAND_ID" And col.ColumnName <> "BRAND_NAME" Then
                        tblBrand.Columns.Remove(col)
                    End If
                Next
                tblBrand.AcceptChanges()
                Me.baseDataSet.Tables.Add(tblBrand)

            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Try
                    Me.ClearCommandParameters()
                    Me.DisposeTempDB()
                Catch ex1 As Exception
                    'Me.strStartDateFlag = "" : Me.strEndDateFlag = ""
                    Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                    Throw ex1
                End Try
                'Me.strStartDateFlag = "" : Me.strEndDateFlag = ""
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                Throw ex
            End Try
            Return Me.baseDataSet
        End Function
        Public Sub DeleteAchievementHeader(ByVal AchHeaderID As String)
            Try

                Query = "SET NOCOUNT ON;" & vbCrLf & _
                "DELETE FROM ACHIEVEMENT_DETAIL WHERE ACH_HEADER_ID = '" & AchHeaderID & "' ;" & vbCrLf & _
                "DELETE FROM ACHIEVEMENT_HEADER WHERE ACH_HEADER_ID = '" & AchHeaderID & "' ; " & vbCrLf
                'delete year nya juga
                If IsNothing(Me.SqlCom) Then : Me.CreateCommandSql("sp_executesql", "")
                Else : Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
                End If
                Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
                Me.OpenConnection() : Me.BeginTransaction() : Me.SqlCom.Transaction = Me.SqlTrans
                Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()
                Me.CommiteTransaction() : Me.CloseConnection()
            Catch ex As Exception
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters() : Throw ex
            End Try
        End Sub
        Public Function CalculateAchievement(ByVal Flag As String, ByVal tblAgreement As DataTable, Optional ByVal DISTRIBUTOR_ID As String = "") As DataSet
            Try
                Me.MessageError = ""
                'Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateQ1 As DateTime = Nothing, EndDateQ1 As DateTime = Nothing, _
                'StartDateQ2 As DateTime = Nothing, EndDateQ2 As DateTime = Nothing, StartDateQ3 As DateTime = Nothing, EndDateQ3 As DateTime = Nothing, _
                'StartDateQ4 As DateTime = Nothing, EndDateQ4 As DateTime = Nothing, StartDateS1 As DateTime = Nothing, EndDateS1 As DateTime = Nothing, _
                'StartDateS2 As DateTime = Nothing, EndDateS2 As DateTime = Nothing
                Dim StartDate As DateTime = Nothing, EndDate As DateTime = Nothing, StartDateF1 As DateTime = Nothing, StartDateF2 As DateTime = Nothing, StartDateF3 As DateTime = Nothing
                Dim EndDateF1 As DateTime = Nothing, EndDateF2 As DateTime = Nothing, EndDateF3 As DateTime = Nothing

                Dim tblDistAgreement As New DataTable("T_Agreement") : tblDistAgreement.Clear()
                Me.OpenConnection()
                If Not IsNothing(tblAgreement) Then
                    tblDistAgreement = tblAgreement
                ElseIf Not String.IsNullOrEmpty(DISTRIBUTOR_ID) Then
                    'GET AGREEMENT_NO 
                    Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE FROM AGREE_AGREEMENT AA INNER JOIN DISTRIBUTOR_AGREEMENT DA " & vbCrLf & _
                            " ON AA.AGREEMENT_NO = DA.AGREEMENT_NO WHERE DA.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                            " AND YEAR(AA.END_DATE) >= YEAR(@GETDATE) - 1;"
                    Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                    Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DISTRIBUTOR_ID, 10)
                    Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                    Me.SqlDat.Fill(tblDistAgreement) : Me.ClearCommandParameters()
                ElseIf Flag <> "" Then
                    Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                            "SELECT AGREEMENT_NO,START_DATE,END_DATE FROM AGREE_AGREEMENT WHERE END_DATE >= @GETDATE  AND START_DATE < @GETDATE AND QS_TREATMENT_FLAG = '" & Flag.Remove(1, 1) & "' ;"
                    Me.CreateCommandSql(CommandType.Text, Query, ConnectionTo.Nufarm)
                    Me.AddParameter("@GETDATE", SqlDbType.SmallDateTime, NufarmBussinesRules.SharedClass.ServerDate)
                    Me.SqlDat = New SqlDataAdapter(Me.SqlCom)
                    Me.SqlDat.Fill(tblDistAgreement) : Me.ClearCommandParameters()
                End If
                Dim MessageHeader As String = "Progressif Discount for Agreement"
                Dim MessageDetail As String = ""
                Dim tblAchHeader As New DataTable("T_AchHeader") : tblAchHeader.Clear()
                Dim tblAchDetail As New DataTable("T_AchDetail") : tblAchDetail.Clear()
                Dim strDecStartDate As String = "", strDecEndDate As String = ""
                Dim HasTarget As Boolean = True
                For i As Integer = 0 To tblDistAgreement.Rows.Count - 1
                    Me.ClearCommandParameters() : HasTarget = True
                    StartDate = Convert.ToDateTime(tblDistAgreement.Rows(i)("START_DATE"))
                    EndDate = Convert.ToDateTime(tblDistAgreement.Rows(i)("END_DATE"))
                    PrevAgreementNo = ""
                    curAgreeStartDate = StartDate
                    curAgreeEndDate = EndDate
                    StartDateF1 = StartDate
                    EndDateF1 = StartDateF1.AddMonths(4).AddDays(-1)
                    StartDateF2 = EndDateF1.AddDays(1)
                    EndDateF2 = StartDateF2.AddMonths(4).AddDays(-1)
                    StartDateF3 = EndDateF2.AddDays(-1)
                    EndDateF3 = EndDate
                    'start calculating
                    Select Case Flag
                        Case "F1"
                            ''prepare data
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateF1)
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateF1)
                            Me.CreateTempTable(StartDateF1, EndDateF1, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount(Flag, StartDateF1, EndDateF1, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString(), tblAchHeader, tblAchDetail, MessageDetail, HasTarget, EndDate)
                        Case "F2"
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateF2)
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateF2)
                            Me.CreateTempTable(StartDateF2, EndDateF2, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount(Flag, StartDateF2, EndDateF2, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString(), tblAchHeader, tblAchDetail, MessageDetail, HasTarget, EndDate)
                        Case "F3"
                            strDecStartDate = common.CommonClass.getNumericFromDate(StartDateF3)
                            strDecEndDate = common.CommonClass.getNumericFromDate(EndDateF3)
                            Me.CreateTempTable(StartDateF3, EndDateF3, strDecStartDate, strDecEndDate)
                            Me.GenerateDiscount(Flag, StartDateF3, EndDateF3, tblDistAgreement.Rows(i)("AGREEMENT_NO").ToString(), tblAchHeader, tblAchDetail, MessageDetail, HasTarget, EndDate)
                    End Select
                    If Not HasTarget Then : tblDistAgreement.Rows.RemoveAt(i) : i -= 1 : End If
                Next
                If MessageDetail <> "" Then
                    Me.MessageError = MessageHeader & vbCrLf & MessageDetail & vbCrLf & "Has not been defined yet"
                End If
                ''drop table ''tempd db
                Me.ClearCommandParameters()
                Dim ListAgreement As New List(Of String)
                For i As Integer = 0 To tblDistAgreement.Rows.Count - 1
                    ListAgreement.Add(tblDistAgreement.Rows(i)("AGREEMENT_NO"))
                Next
                'Me.strStartDateFlag = strDecStartDate : Me.strEndDateFlag = strDecEndDate
                'If Not ReturnDS Then : Return Nothing : End If
                Dim Ds As DataSet = Me.getAccrued(Flag, DISTRIBUTOR_ID, ListAgreement)
                Return Ds
            Catch ex As Exception
                If Not IsNothing(Me.SqlRe) Then
                    If Not Me.SqlRe.IsClosed Then
                        Me.SqlRe.Close()
                    End If
                End If
                Try
                    Me.ClearCommandParameters()
                    Me.DisposeTempDB()
                Catch ex1 As Exception
                    Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                    Throw ex1
                End Try
                Me.RollbackTransaction() : Me.CloseConnection() : Me.ClearCommandParameters()
                mustRecomputeYear = False
                Throw ex
            Finally
                mustRecomputeYear = False
            End Try

        End Function

        Private Sub GenerateDiscount(ByVal FLAG As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal AgreementNO As String, _
            ByRef tblAchHeader As DataTable, ByRef tblAchDetail As DataTable, ByRef Message As String, ByRef HasTarget As Boolean, ByVal EndDateAgreement As DateTime)

            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT  1 WHERE EXISTS(SELECT AGREEMENT_NO FROM AGREE_AGREEMENT AA INNER JOIN AGREE_PROG_DISC_R APD " & vbCrLf & _
                    "                       ON AA.AGREEMENT_NO = APD.AGREEMENT_NO WHERE AA.AGREEMENT_NO = @AGREEMENT_NO " & vbCrLf & _
                    "                       AND APD.FLAG = @FLAG)"

            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNO, 32)
            Me.AddParameter("@FLAG", SqlDbType.VarChar, FLAG)
            Dim retval As Object = Me.SqlCom.ExecuteScalar()
            If IsNothing(retval) Or IsDBNull(retval) Then : Me.ClearCommandParameters() : Return : End If
            If CInt(retval) <= 0 Then : Me.ClearCommandParameters() : HasTarget = False : Return : End If
            '--------------------------CHECK AGREEMENT_NO DI MANA TARGET DAN PROGRESSIVE DISCOUNT ADA-----------------------------------------------------------------
            '---------------------------------------------------------------------------------
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT COUNT(AGREEMENT_NO) FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO OPTION(KEEP PLAN);"

            Me.ResetCommandText(CommandType.Text, Query)
            '-------------------------BIKIN TABLE BUAT NGINSERT DAN UPDATE DATA-------------------
            'If FLAG <> "Y" Then
            ' Me.getTotalInvoiceByQS(AgreementNO, FLAG, IsTargetGroup, AchHeader, tblAchDetail, ListAgreeBrand, StartDate, EndDate)
            'Else
            '  Me.getTotalInvoiceByYear(AgreementNO, IsTargetGroup, StartDate, EndDate, AchHeader, tblAcrDetail, ListAgreeBrand)
            ' End If
            '-----------------------------------------------------------------------------------------
            Me.getTotalInvoice(AgreementNO, FLAG, tblAchHeader, tblAchDetail, StartDate, EndDate)

            'chek perubahan
            If tblAchHeader.Rows.Count <= 0 Then : Me.ClearCommandParameters() : Return : End If
            '--------------------------------------------------------------------------------
            ' If FLAG <> "Y" Then : Me.UpdateActual(AchHeader, ListAgreeBrand) : End If
            ' Me.SaveToDataBase(AgreementNO, FLAG, ListAgreeBrand, StartDate, EndDate, AchHeader, tblAchDetail, Message, EndDateAgreement, IsTargetGroup)

        End Sub
        Private Sub CalculateHeader(ByVal FLAG As String, ByVal CategoryIsNewOrIsChanged As String, ByRef tblAchHeader As DataTable, _
                ByVal AgreementNO As String, ByRef Message As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime)
            Dim RowsSelect() As DataRow = Nothing, AchievementDispro As Decimal = 0, _
            TTargetSPSG_RPM As Decimal = 0, TTargetBPSG_RPM As Decimal = 0, Percentage_SPSG_RMP As Decimal = 0, Percentage_BPSG_RPM As Decimal = 0, _
            TTargetSPSG_BIO As Decimal = 0, TTargetBPSG_BIO As Decimal = 0, Percentage_SPSG_BIO As Decimal = 0, Percentage_BPSG_BIO As Decimal = 0, _
            TTargetSPSG_TR As Decimal = 0, TTargetBPSG_TR As Decimal = 0, Percentage_SPSG_TR As Decimal = 0, Percentage_BPSG_TR As Decimal = 0, _
            TPO_SPSG_RPM As Decimal = 0, TPO_BPSG_RPM As Decimal = 0, _
            TPO_SPSG_BIO As Decimal = 0, TPO_BPSG_BIO As Decimal = 0, _
            TPO_SPSG_TR As Decimal = 0, TPO_BPSG_TR As Decimal = 0, _
            BonusQty As Decimal = 0, Balance As Decimal = 0, Dispro As Decimal = 0, _
            Row As DataRow = Nothing, Description As String = "", Rows As DataRow() = Nothing, retval As Object = Nothing

            'AMBIL DATA Progressive Discount
            'power max kemasan kecil 00681,00684
            'power max kemasan besar 006820

            'Biosorb kemasan kecil 00601,0060200,00604
            'Biosorb kemasan besar 006020

            'Biosorb kemasan kecil 007801,007804,0078200
            'Biosorb kemasan besar 007820
            'check achievement method dulu
            ''get method if its' by pack size or all packsize
            'Query = "SET NOCOUNT ON;" & vbCrLf & _
            '"SELECT TOP 1 ACH_METHODE FROM AGREE_PROG_DISC_R WHERE AGREEMENT_NO = @AGREEMENT_NO ;"
            'Me.ResetCommandText(CommandType.Text, Query)
            'retval = Me.SqlCom.ExecuteScalar()
            'If IsNothing(retval) Then
            '    Me.ClearCommandParameters() : Me.CloseConnection()
            '    Throw New Exception("ACHIEVEMENT_METHOD IS NOT SET TO PSG OR ALL PACK SIZE" & vbCrLf & "Please tell system administrator")
            'End If
            'If retval.ToString() = "PSG" Then ''untuk sekarang anggap saja semua ke psg(pack size group)

            'Else 'all pack size'nanti saja lagi

            'End If
            'Query = " SET NOCOUNT ON ; SELECT APD.PS_CATEGORY,AGREE_BRAND_ID = CASE " & vbCrLf & _
            '" WHEN ABI.BRAND_ID = '00681' THEN APD.AGREEMENT_NO + ABI.BRAND_ID " & vbCrLf & _
            '" WHEN ABI.BRAND_ID = '00684' THEN APD.AGREEMENT_NO + ABI.BRAND_ID " & vbCrLf & _
            '" WHEN ABI.BRAND_ID = '00601' THEN APD.AGREEMENT_NO + ABI.BRAND_ID " & vbCrLf & _
            '" WHEN ABI.BRAND_ID = '0060200' THEN APD.AGREEMENT_NO + ABI.BRAND_ID " & vbCrLf & _
            '" WHEN ABI.BRAND_ID = '00604' THEN APD.AGREEMENT_NO + ABI.BRAND_ID " & vbCrLf & _
            '" WHEN ABI.BRAND_ID = '007801' THEN APD.AGREEMENT_NO + ABI.BRAND_ID " & vbCrLf & _
            '" WHEN ABI.BRAND_ID = '007804' THEN APD.AGREEMENT_NO + ABI.BRAND_ID " & vbCrLf & _
            '" WHEN ABI.BRAND_ID = '0078200' THEN APD.AGREEMENT_NO + ABI.BRAND_ID " & vbCrLf & _
            '" END," & vbCrLf & _
            '" ,ABD.UP_TO_PCT * 100 AS UP_TO_PCT,ABD.DISC_PCT * 100 AS DISC_PCT,ABD.FLAG FROM AGREE_PROG_DISC_R APD INNER JOIN AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
            '        " ON ABI.AGREE_BRAND_ID = APD.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND APD.PS_CATEGORY = 'S' " & vbCrLf & _
            '" UNION " & vbCrLf & _
            '" SELECT APD.PS_CATEGORY,AGREE_BRAND_ID = CASE " & vbCrLf & _
            '" WHEN ABI.BRAND_ID = '006820' THEN APD.AGREEMENT_NO + ABI.BRAND_ID " & vbCrLf & _
            '" WHEN ABI.BRAND_ID = '006020' THEN APD.AGREEMENT_NO + ABI.BRAND_ID " & vbCrLf & _
            '" WHEN ABI.BRAND_ID = '007820' THEN APD.AGREEMENT_NO + ABI.BRAND_ID " & vbCrLf & _
            '" END," & vbCrLf & _
            '" ,ABD.UP_TO_PCT * 100 AS UP_TO_PCT,ABD.DISC_PCT * 100 AS DISC_PCT,ABD.FLAG FROM AGREE_PROG_DISC_R APD INNER JOIN AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
            '        " ON ABI.AGREE_BRAND_ID = APD.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND APD.PS_CATEGORY = 'B' ;"
            Query = "SET NOCOUNT ON;" & vbCrLf & _
            " SELECT PS_CATEGORY,UP_TO_PCT * 100 AS UP_TO_PCT,DISC_PCT * 100 AS DISC_PCT,FLAG FROM AGREE_PROG_DISC_R " & vbCrLf & _
            " WHERE AGREEMENT_NO = @AGREEMENT_NO;"
            tblDiscProgressive = New DataTable("tblProgressive")
            Me.ResetCommandText(CommandType.Text, Query)
            setDataAdapter(Me.SqlCom).Fill(tblDiscProgressive)

            Me.SqlCom.Parameters.RemoveAt("@START_DATE")
            Me.SqlCom.Parameters.RemoveAt("@END_DATE")

            'GET dispro data from current agreement previous flag 
            Query = "SET NOCOUNT ON;" & vbCrLf
            If Me.IsTransitionTime Then
                Query = "SELECT AGREEMENT_NO + BRAND_ID AS AGREE_BRAND_ID,FLAG,DISPRO FROM ACCRUED_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND FLAG IN('Q2','Q3') " & vbCrLf & _
                " UNION " & vbCrLf
            End If
            Query &= "SELECT AGREEMENT_NO + BRAND_ID AS AGREE_BRAND_ID,FLAG,DISPRO FROM ACHIEVEMENT_HEADER WHERE AGREEMENT_NO = @AGREEMENT_NO AND FLAG IN('F1','F2') "
            Me.ResetCommandText(CommandType.Text, Query)
            Me.tblCurAchiement = New DataTable("tblProgressive")
            setDataAdapter(Me.SqlCom).Fill(Me.tblCurAchiement)

            'GET dispro data from previous agreement agreement previous flag only PBF3
            'get previous agreement no 
            Query = "SET NOCOUNT ON;" & vbCrLf & _
            " DECLARE @V_PREV_AG_NO VARCHAR(25); " & vbCrLf & _
            " SET @V_PREV_AG_NO = (SELECT TOP 1 AA.AGREEMENT_NO FROM AGREE_AGREEMENT AA INNER JOIN DISTRIBUTOR_AGREEMENT DA ON AA.AGREEMENT_NO = DA.AGREEMENT_NO " & vbCrLf & _
            "                       WHERE AA.AGREEMENT_NO != @AGREEMENT_NO AND AA.START_DATE < @CUR_START_DATE ORDER BY AA.START_DATE DESC);  " & vbCrLf & _
            " SELECT AGREEMENT_NO + BRAND_ID AS AGREE_BRAND_ID,FLAG,DISPRO FROM ACHIEVEMENT_HEADER WHERE AGREEMENT_NO = @V_PREV_AG_NO AND FLAG = 'F3' ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.tblPrevAchievement = New DataTable("tblPreviousAchievement")
            setDataAdapter(Me.SqlCom).Fill(Me.tblPrevAchievement)


            'hitung achievement
            '--------------------------------------------------------------------------------------------

            'totalkan target dan actual berdasarkan psg dan merk
            'rowselect = tblAchHeader
            '------biosorb 007801,007804,0078200,006020
            RowsSelect = tblAchHeader.Select("BRAND_ID IN('00601','0060200','00604')")
            If Not IsNothing(RowsSelect) And Not IsDBNull(RowsSelect) Then
                TTargetSPSG_BIO = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('00601','0060200','00604')")
                TPO_SPSG_BIO = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('00601','0060200','00604')")
                Percentage_SPSG_BIO = common.CommonClass.GetPercentage(100, TPO_SPSG_BIO, TTargetSPSG_BIO)
            End If
            RowsSelect = tblAchHeader.Select("BRAND_ID IN('006020')")
            If RowsSelect.Length > 0 Then
                TTargetBPSG_BIO = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('006020')")
                TPO_BPSG_BIO = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('006020')")
                Percentage_BPSG_BIO = common.CommonClass.GetPercentage(100, TPO_BPSG_BIO, TTargetBPSG_BIO)
            End If
            If Percentage_SPSG_BIO > 0 Then
                'hitung Dispro
                Dim DVCurProgress As DataView = tblDiscProgressive.DefaultView()
                DVCurProgress.RowFilter = " FLAG = '" & FLAG & "'"
                DVCurProgress.Sort = "UP_TO_PCT DESC"
                For i As Integer = 0 To DVCurProgress.Count - 1
                    Dim UpToPCT As Decimal = Convert.ToDecimal(DVCurProgress(i)("UP_TO_PCT"))
                    If Percentage_SPSG_BIO >= UpToPCT Then
                        Dispro = Convert.ToDecimal(DVCurProgress(i)("DISC_PCT"))
                        Exit For
                    End If
                Next
                For i As Integer = 0 To RowsSelect.Length - 1
                    Row = RowsSelect(i)
                    Balance = Convert.ToDecimal(Row("TOTAL_PO")) - Convert.ToDecimal(Row("TOTAL_TARGET")) 'TPO_SPSG_BIO - TTargetSPSG_BIO
                    Dim TotalActual = Convert.ToDecimal(Row("TOTAL_ACTUAL"))
                    BonusQty = TotalActual * Dispro
                    'hitung bonus previous year(PBFR) dan CPQ1,CPQ2,CPQ3,dan CPF1,CPF2
                    Row("DISPRO") = Dispro
                    Row("ACHIEVEMENT_DISPRO") = AchievementDispro
                    Row("BONUS_QTY") = BonusQty
                    Row("DISC_BY_VOLUME") = BonusQty
                    Row("BONUS_DISTRIBUTOR") = BonusQty
                    Row("DISC_DIST_BY_VOLUME") = BonusQty
                    'Row("AGREE_ACH_BY") = "VOL"
                    Row("BALANCE") = Balance
                    Row("DESCRIPTION") = Description
                    Row("CombinedWith") = DBNull.Value
                Next
            End If

            '---TRANSORB
            RowsSelect = tblAchHeader.Select("BRAND_ID IN('007801','007804','0078200')")
            If RowsSelect.Length > 0 Then
                TTargetSPSG_TR = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('007801','007804','0078200')")
                TPO_SPSG_TR = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('007801','007804','0078200')")
                Percentage_SPSG_TR = common.CommonClass.GetPercentage(100, TPO_SPSG_TR, TTargetSPSG_TR)
            End If

            RowsSelect = tblAchHeader.Select("BRAND_ID IN('007820')")
            If RowsSelect.Length > 0 Then
                TTargetBPSG_TR = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('007820')")
                TPO_BPSG_TR = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('007820')")
                Percentage_BPSG_TR = common.CommonClass.GetPercentage(100, TPO_BPSG_TR, TTargetBPSG_TR)
            End If

            '--rpm
            RowsSelect = tblAchHeader.Select("BRAND_ID IN('00681','00684')")
            If RowsSelect.Length > 0 Then
                TTargetSPSG_TR = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('00681','00684')")
                TPO_SPSG_TR = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('00681','00684')")
                Percentage_SPSG_TR = common.CommonClass.GetPercentage(100, TPO_SPSG_TR, TTargetSPSG_TR)
            End If

            RowsSelect = tblAchHeader.Select("BRAND_ID IN('006820')")
            If RowsSelect.Length > 0 Then
                TTargetBPSG_TR = tblAchHeader.Compute("SUM(TOTAL_TARGET)", "BRAND_ID IN('006820')")
                TPO_BPSG_TR = tblAchHeader.Compute("SUM(TOTAL_ACTUAL)", "BRAND_ID IN('006820')")
                Percentage_BPSG_TR = common.CommonClass.GetPercentage(100, TPO_BPSG_TR, TTargetBPSG_TR)
            End If

            'If Target <> 0 Then
            '    Percentage = common.CommonClass.GetPercentage(100, TotalPOOriginal, Target)
            'End If
            ''Dim Percentage As Decimal = common.CommonClass.GetPercentage(100, TotalActual, Target)
            ' ''looping cari yang masuk dalam category
            'For i1 As Integer = 0 To DVProgressive.Count - 1
            '    If Percentage > Convert.ToDecimal(DVProgressive(i1)("UP_TO_PCT")) Then
            '        Dispro = Convert.ToDecimal(DVProgressive(i1)("PRGSV_DISC_PCT"))
            '        Exit For
            '    End If
            'Next
            'Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
            '"SELECT DA.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID,FLAG = 'F3',ABI.TARGET_FMP3, ABI.TARGET_" & strFlag.Remove(3, 1) & "_FM" & FLAG.Remove(0, 1) & ", ABI.TARGET_" & strFlag.Remove(3, 1) & "_PL" & FLAG.Remove(0, 1) & vbCrLf & _
            '" FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRAND_INCLUDE ABI ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
            '" WHERE DA.AGREEMENT_NO = @AGREEMENT_NO AND NOT EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER  " & vbCrLf & _
            '" WHERE ACH_HEADER_ID = DA.DISTRIBUTOR_ID + '|' + @AGREEMENT_NO  + '' + ABI.BRAND_ID + '|" & FLAG & "'" & vbCrLf & _
            '" ) OPTION(KEEP PLAN);"
            RowsSelect = tblAchHeader.Select(CategoryIsNewOrIsChanged & " = " & True & " AND FLAG = '" & FLAG & "'")



            'dapatkan percentagenya
            'mulai edit row

            ' End If
            'Next
            ''sekarang edit yang changed
            tblAchHeader.AcceptChanges()

        End Sub
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Flag"></param>
        ''' <param name="RowsSelect">DataRow tblAchHeader yang di filter based on CategoryIsNewOrchanged</param>
        ''' <param name="Distributor">'distributor</param>
        ''' <param name="Description">Description of previous discount to get</param>
        ''' <param name="BonusQty">Disc Qty + all previous discount(TOTAL(CPQ2,CPQ3,CPF2,CPF1,PBF3))</param>
        ''' <remarks>Function to get and calculate Previos Actual PO,& discount qty and descriptions to get</remarks>
        Private Sub CalculteDiscPrevious(ByVal Flag As String, ByVal RowsSelect() As DataRow, ByVal DistributorID As String, ByRef Description As String, ByRef BonusQty As Decimal)

            Dim totalInvoiceBeforeF3 As Decimal = 0, totalInvoiceCurrentF1 As Decimal = 0, totalInvoiceCurrentF2 As Decimal = 0, totalInvoiceCurrentQ2 As Decimal = 0, _
            totalInvoiceCurrentQ3 As Decimal = 0, PrevDisPro As Decimal = 0, DVCurAch As DataView = tblCurAchiement.DefaultView, totalInvoiceCurF1 As Decimal = 0, totalInvoiceCurF2 As Decimal = 0
            Dim DVPrevAch As DataView = Nothing
            If Not IsNothing(Me.tblPrevAchievement) Then
                DVPrevAch = tblPrevAchievement.DefaultView()
            End If
            Dim rowsCheck() As DataRow = Nothing
            Dim Row As DataRow = Nothing
            'AGREE_BRAND_ID,UP_TO_PCT,PRGSV_DISC_PCT
            'DISTRIBUTOR_ID|AGREEMENT_NOBRAND_ID|FLAG
            Dim AchID As String = DistributorID & "|" & RowsSelect(0)("AGREE_BRAND_ID") = "|" & Flag
            Select Case Flag
                Case "F3" 'Q2,Q3,F1,F2
                    If IsTransitionTime Then
                        AchID = DistributorID & "|" & RowsSelect(0)("AGREE_BRAND_ID") = "|Q2"
                        totalInvoiceCurrentQ2 = RowsSelect(0)("TOTAL_CPQ2")
                        If CDec(totalInvoiceCurrentQ2) > 0 Then
                            DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchID & "'" '" & RowsSelect(0)("AGREE_BRAND_ID") & "' AND FLAG = 'Q2'"
                            If DVCurAch.Count > 0 Then
                                PrevDisPro = DVCurAch(0)("DISPRO") 'previouse dipsro Q1/Q2/Q3 percent = 100, bukan 0,1
                                BonusQty += (PrevDisPro / 100) * totalInvoiceCurrentQ2
                                Description &= String.Format("Q2 {0:p}={1:#,##0.000}", PrevDisPro / 100, (PrevDisPro / 100) * totalInvoiceCurrentQ2)
                            End If
                        End If
                        AchID = DistributorID & "|" & RowsSelect(0)("AGREE_BRAND_ID") = "|Q3"
                        totalInvoiceCurrentQ3 = RowsSelect(0)("TOTAL_CPQ3")
                        If CDec(totalInvoiceCurrentQ3) > 0 Then
                            DVCurAch.RowFilter = "ACHIEVEMENT_ID = '" & AchID & "'" '"
                            If DVCurAch.Count > 0 Then
                                PrevDisPro = DVCurAch(0)("DISPRO") 'previouse dipsro Q1/Q2/Q3 percent = 100, bukan 0,1
                                BonusQty += (PrevDisPro / 100) * totalInvoiceCurrentQ3
                                If Description <> "" Then
                                    Description &= ", "
                                End If
                                Description &= String.Format("Q3 {0:p}={1:#,##0.000}", PrevDisPro / 100, (PrevDisPro / 100) * totalInvoiceCurrentQ3)
                                Description &= ", "
                            End If
                        End If
                    End If
                    totalInvoiceCurF2 = RowsSelect(0)("TOTAL_CPF2")
                    If totalInvoiceCurF2 > 0 Then
                        DVCurAch.RowFilter = "ACH_HEADER_ID = '" & DistributorID & "|" & RowsSelect(0)("AGREE_BRAND_ID") = "|F2'"
                        If DVPrevAch.Count > 0 Then
                            PrevDisPro = DVCurAch(0)("DISPRO") 'dispro DIBAGI 100
                            BonusQty += (PrevDisPro / 100) * totalInvoiceCurF2
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("F2 {0:p}={1:#,##0.000}", PrevDisPro, PrevDisPro * totalInvoiceCurF2)
                        End If
                    End If
                    totalInvoiceCurF1 = RowsSelect(0)("TOTAL_CPF1")
                    If totalInvoiceCurF1 > 0 Then
                        DVCurAch.RowFilter = "ACH_HEADER_ID = '" & DistributorID & "|" & RowsSelect(0)("AGREE_BRAND_ID") = "|F1'"
                        If DVPrevAch.Count > 0 Then
                            PrevDisPro = DVCurAch(0)("DISPRO") 'dispro DIBAGI 100
                            BonusQty += (PrevDisPro / 100) * totalInvoiceCurF1
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("F1 {0:p}={1:#,##0.000}", PrevDisPro, PrevDisPro * totalInvoiceCurF1)
                        End If
                    End If
                Case "F2" 'F1, PBF3
                    totalInvoiceCurF1 = RowsSelect(0)("TOTAL_CPF1")
                    If totalInvoiceCurF1 > 0 Then
                        DVCurAch.RowFilter = "ACH_HEADER_ID = '" & DistributorID & "|" & RowsSelect(0)("AGREE_BRAND_ID") = "|F1'"
                        If DVPrevAch.Count > 0 Then
                            PrevDisPro = DVCurAch(0)("DISPRO") 'dispro DIBAGI 100
                            BonusQty += (PrevDisPro / 100) * totalInvoiceCurF1
                            Description &= String.Format("F1 {0:p}={1:#,##0.000}", PrevDisPro, PrevDisPro * totalInvoiceCurF1)
                        End If
                    End If
                    totalInvoiceBeforeF3 = RowsSelect(0)("TOTAL_PBF3")
                    If totalInvoiceBeforeF3 > 0 And Not IsNothing(DVPrevAch) Then
                        DVPrevAch.RowFilter = "ACH_HEADER_ID = '" & DistributorID & "|" & RowsSelect(0)("AGREE_BRAND_ID") = "|F3'"
                        If DVPrevAch.Count > 0 Then
                            PrevDisPro = DVCurAch(0)("DISPRO") 'dispro DIBAGI 100
                            BonusQty += (PrevDisPro / 100) * totalInvoiceBeforeF3
                            If Description <> "" Then
                                Description &= ", "
                            End If
                            Description &= String.Format("F3 {0:p}={1:#,##0.000}", PrevDisPro, PrevDisPro * totalInvoiceBeforeF3)
                        End If
                    End If
                Case "F1"
                    'If totalInvoiceBeforeQ4 > 0 Then
                    '    If (CDec(RowsSelect(0)("GPPBQ4")) > 0) Then
                    '        BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPPBQ4")) / 100) * totalInvoiceBeforeQ4
                    '        Description &= String.Format("{0:p} GP of Q4", Convert.ToDecimal(RowsSelect(0)("GPPBQ4")) / 100)
                    '        Description &= ", "
                    '        Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPPBQ4")
                    '    End If
                    'End If
                    'If totalInvoiceCurrentF1 > 0 Then
                    '    If (CDec(RowsSelect(0)("GPCPQ1")) > 0) Then
                    '        BonusQty += (Convert.ToDecimal(RowsSelect(0)("GPCPQ1")) / 100) * totalInvoiceCurrentF1
                    '        If Description <> "" Then
                    '            Description &= ", "
                    '        End If
                    '        Description &= String.Format("{0:p} GP of Q1", Convert.ToDecimal(RowsSelect(0)("GPCPQ1")) / 100)
                    '        Description &= ", "
                    '        Me.setGivenProgressive(Rows, RowsSelect, RowsSelect1, tblListBonusBefore, "GPCPQ1")
                    '    End If
                    'End If
                    'Description &= " " & DescriptionTotal
            End Select
        End Sub
        Private Sub getTotalInvoice(ByVal AgreementNo As String, ByVal Flag As String, ByRef tblAchHeader As DataTable, _
            ByRef tblAchDetail As DataTable, ByVal StartDate As DateTime, ByVal EndDate As DateTime)
            Query = "SET NOCOUNT ON ;" & vbCrLf & _
            " SELECT TOP 1 START_DATE, END_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO ;"
            Me.ResetCommandText(CommandType.Text, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            Dim StartDatePKD As New DateTime(2019, 8, 1), EndDatePKD As New DateTime(2020, 7, 31)
            While Me.SqlRe.Read()
                StartDatePKD = Me.SqlRe.GetDateTime(0) : EndDatePKD = Me.SqlRe.GetDateTime(1)
            End While : Me.SqlRe.Close()
            IsTransitionTime = StartDatePKD >= New DateTime(2019, 8, 1) And EndDatePKD <= New DateTime(2020, 7, 31)
            '-----------------------------DECLARASI TABLE------------------------------------------------------------
            Dim Row As DataRow = Nothing, RowsSelect() As DataRow = Nothing, _
           IsChangedData As Boolean = False, HasNewData As Boolean = False, _
           ListAchievementChangedData As New List(Of String), ListAchievementNewData As New List(Of String), strFlag As String = ""
            Select Case Flag
                Case "F1" : strFlag = "FMP1"
                Case "F2" : strFlag = "FMP2"
                Case "F3" : strFlag = "FMP3"
            End Select
            Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                    "SELECT DA.DISTRIBUTOR_ID,ABI.AGREE_BRAND_ID,ABI.BRAND_ID,ABI.TARGET_" & strFlag & ", ABI.TARGET_" & strFlag.Remove(3, 1) & "_FM" & Flag.Remove(0, 1) & ", ABI.TARGET_" & strFlag.Remove(3, 1) & "_PL" & Flag.Remove(0, 1) & vbCrLf & _
                    " FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRAND_INCLUDE ABI ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                    " WHERE DA.AGREEMENT_NO = @AGREEMENT_NO AND NOT EXISTS(SELECT ACH_HEADER_ID FROM ACHIEVEMENT_HEADER  " & vbCrLf & _
                    " WHERE ACH_HEADER_ID = DA.DISTRIBUTOR_ID + '|' + @AGREEMENT_NO  + '' + ABI.BRAND_ID + '|" & Flag & "'" & vbCrLf & _
                    " ) OPTION(KEEP PLAN);"
            'ACH_HEADER_ID, DISTRIBUTOR_ID, AGREEMENT_NO, BRAND_ID,AGREE_BRAND_ID,TARGET, TARGET_FM, TARGET_PL, TARGET_VALUE,FLAG,
            'TOTAL_TARGET, TOTAL_PO, TOTAL_PO_VALUE, BALANCE, TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_ACTUAL, ACH_DISPRO, DISPRO,
            'CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsNew, IsChanged, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3, GPPBF3, GPCPQ1, 
            'GPCPQ2, GPCPQ3, GPCPF1, GPCPF2, GPCPF3
            Dim hasCreatedTempTablePO As Boolean = False
            Me.ResetCommandText(CommandType.Text, Query)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read()
                HasNewData = True
                Row = tblAchHeader.NewRow()
                Dim BrandID As String = SqlRe.GetString(2)
                Dim Target As Decimal = SqlRe.GetDecimal(4)
                Dim TargetFM As Decimal = SqlRe.GetDecimal(5)
                Dim TargetPL As Decimal = SqlRe.GetDecimal(6)
                Dim AchievementID As String = SqlRe.GetString(0) + "|" + AgreementNo + BrandID + "|" + Flag
                Row("ACH_HEADER_ID") = AchievementID
                Row("AGREEMENT_NO") = AgreementNo
                Row("DISTRIBUTOR_ID") = SqlRe.GetString(0)
                Row("AGREE_BRAND_ID") = SqlRe.GetString(1)
                Row("BRAND_ID") = BrandID
                Row("FLAG") = Flag
                Row("TOTAL_TARGET") = Target
                Row("TARGET_FM") = TargetFM
                Row("TARGET_PL") = TargetPL
                Row("IsNew") = True
                Row("IsChanged") = False
                Row.EndEdit() : tblAchHeader.Rows.Add(Row)
                If Not ListAchievementNewData.Contains(AchievementID) Then
                    ListAchievementNewData.Add(AchievementID)
                End If
            End While : Me.SqlRe.Close()
            '----------------------------------ISI TABLE---------------------------------------
            '===========================COMMENT THIS AFTER DEBUGGIN==============================================
            'Me.ClearCommandParameters()
            'Query = "SET NOCOUNT ON;" & vbCrLf & _
            '        "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = 'TEMPDB..##T_TEMP_BP' AND TYPE = 'U') " & vbCrLf & _
            '         " BEGIN DROP TABLE TEMPDB..##T_TEMP_BP ; END " & vbCrLf & _
            '        "SELECT PB.*,NEW_BRANDPACK_ID = '00790001LD' INTO TEMPDB..##T_TEMP_BP FROM ORDR_PO_BRANDPACK PB INNER JOIN ORDR_PURCHASE_ORDER PO ON PO.PO_REF_NO = PB.PO_REF_NO " & vbCrLf & _
            '        " WHERE PB.BRANDPACK_ID = '007900000D' AND PO.DISTRIBUTOR_ID = 'ADI006IDR' AND PO.PO_REF_DATE >= '06/01/2015' AND PO.PO_REF_DATE <= '08/30/2016' ; "
            'Me.ResetCommandText(CommandType.StoredProcedure, "sp_executesql")
            'Me.AddParameter("@stmt", SqlDbType.NVarChar, Query)
            'Me.SqlCom.ExecuteScalar() : Me.ClearCommandParameters()

            'Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
            '           "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_MASTER_PO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
            '           " BEGIN DROP TABLE tempdb..##T_MASTER_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
            '           " SELECT PO_REF_NO,PO_REF_DATE,DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID,SPPB_QTY,PO_ORIGINAL_QTY,PO_AMOUNT = PO_ORIGINAL_QTY * PO_PRICE_PERQTY,RUN_NUMBER,IncludeDPD INTO tempdb..##T_MASTER_PO_" & Me.ComputerName & " FROM ( " & vbCrLf & _
            '           "  SELECT PO.PO_REF_NO,PO.PO_REF_DATE,PO.DISTRIBUTOR_ID,ABI.BRAND_ID,ABP.BRANDPACK_ID,OPB.PO_ORIGINAL_QTY,OPB.PO_PRICE_PERQTY,SB.SPPB_QTY,OOA.RUN_NUMBER ," & vbCrLf & _
            '           "  IncludeDPD = CASE WHEN (OPB.ExcludeDPD = 1) THEN 'NO' " & vbCrLf & _
            '           "  WHEN EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PLANTATION_ID = OPB.PLANTATION_ID AND BRANDPACK_ID = OPB.BRANDPACK_ID AND DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID AND PRICE = OPB.PO_PRICE_PERQTY AND IncludeDPD = 1) THEN 'YESS' " & vbCrLf & _
            '           "  WHEN EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PLANTATION_ID = OPB.PLANTATION_ID AND BRANDPACK_ID = OPB.BRANDPACK_ID AND DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID AND PRICE = OPB.PO_PRICE_PERQTY AND IncludeDPD = 0) THEN 'NO' " & vbCrLf & _
            '           "  WHEN EXISTS(SELECT PROJ.PROJ_REF_NO, PB.BRANDPACK_ID FROM PROJ_PROJECT PROJ INNER JOIN PROJ_BRANDPACK PB ON PROJ.PROJ_REF_NO = PB.PROJ_REF_NO WHERE PROJ.PROJ_REF_NO = PO.PROJ_REF_NO AND PB.BRANDPACK_ID = OPB.BRANDPACK_ID AND PROJ.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID) THEN 'NO' " & vbCrLf & _
            '           "  WHEN OPB.PLANTATION_ID IS NULL THEN 'YESS' ELSE 'NO' END " & vbCrLf & _
            '           "  FROM Nufarm.dbo.AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
            '           "  INNER JOIN Nufarm.DBO.AGREE_BRANDPACK_INCLUDE ABP ON ABI.AGREE_BRAND_ID = ABP.AGREE_BRAND_ID" & vbCrLf & _
            '           "  INNER JOIN Nufarm.dbo.ORDR_PO_BRANDPACK OPB ON OPB.BRANDPACK_ID = ABP.BRANDPACK_ID " & vbCrLf & _
            '           "  INNER JOIN Nufarm.dbo.ORDR_PURCHASE_ORDER PO " & vbCrLf & _
            '           "  ON PO.PO_REF_NO = OPB.PO_REF_NO INNER JOIN Nufarm.dbo.ORDR_ORDER_ACCEPTANCE OOA " & vbCrLf & _
            '           "  ON OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
            '           "  INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOA.OA_ID = OOAB.OA_ID AND OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
            '           "  LEFT OUTER JOIN SPPB_BRANDPACK SB ON SB.OA_BRANDPACK_ID = OOAB.OA_BRANDPACK_ID  " & vbCrLf & _
            '           "  WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND OPB.PO_ORIGINAL_QTY > 0 " & vbCrLf & _
            '           "  AND PO.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
            '           "  UNION " & vbCrLf & _
            '           "  SELECT PO.PO_REF_NO,PO.PO_REF_DATE,PO.DISTRIBUTOR_ID,ABI.BRAND_ID,ABP.BRANDPACK_ID,OPB.PO_ORIGINAL_QTY,OPB.PO_PRICE_PERQTY,SB.SPPB_QTY,OOA.RUN_NUMBER ," & vbCrLf & _
            '           "  IncludeDPD = CASE WHEN (OPB.ExcludeDPD = 1) THEN 'NO' " & vbCrLf & _
            '           "  WHEN EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PLANTATION_ID = OPB.PLANTATION_ID AND BRANDPACK_ID = OPB.BRANDPACK_ID AND DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID AND PRICE = OPB.PO_PRICE_PERQTY AND IncludeDPD = 1) THEN 'YESS' " & vbCrLf & _
            '           "  WHEN EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PLANTATION_ID = OPB.PLANTATION_ID AND BRANDPACK_ID = OPB.BRANDPACK_ID AND DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID AND PRICE = OPB.PO_PRICE_PERQTY AND IncludeDPD = 0) THEN 'NO' " & vbCrLf & _
            '           "  WHEN EXISTS(SELECT PROJ.PROJ_REF_NO, PB.BRANDPACK_ID FROM PROJ_PROJECT PROJ INNER JOIN PROJ_BRANDPACK PB ON PROJ.PROJ_REF_NO = PB.PROJ_REF_NO WHERE PROJ.PROJ_REF_NO = PO.PROJ_REF_NO AND PB.BRANDPACK_ID = OPB.BRANDPACK_ID AND PROJ.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID) THEN 'NO' " & vbCrLf & _
            '           "  WHEN OPB.PLANTATION_ID IS NULL THEN 'YESS' ELSE 'NO' END " & vbCrLf & _
            '           "  FROM Nufarm.dbo.AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
            '           "  INNER JOIN Nufarm.DBO.AGREE_BRANDPACK_INCLUDE ABP ON ABI.AGREE_BRAND_ID = ABP.AGREE_BRAND_ID" & vbCrLf & _
            '           "  INNER JOIN TEMPDB..##T_TEMP_BP OPB ON OPB.NEW_BRANDPACK_ID = ABP.BRANDPACK_ID " & vbCrLf & _
            '           "  INNER JOIN Nufarm.dbo.ORDR_PURCHASE_ORDER PO " & vbCrLf & _
            '           "  ON PO.PO_REF_NO = OPB.PO_REF_NO INNER JOIN Nufarm.dbo.ORDR_ORDER_ACCEPTANCE OOA " & vbCrLf & _
            '           "  ON OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
            '           "  INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOA.OA_ID = OOAB.OA_ID AND OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
            '           "  LEFT OUTER JOIN SPPB_BRANDPACK SB ON SB.OA_BRANDPACK_ID = OOAB.OA_BRANDPACK_ID  " & vbCrLf & _
            '           "  WHERE PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND OPB.PO_ORIGINAL_QTY > 0 " & vbCrLf & _
            '           "  AND PO.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
            '           " )B ;" & vbCrLf & _
            '            " CREATE CLUSTERED INDEX IX_T_MASTER_PO ON ##T_MASTER_PO_" & Me.ComputerName & "(PO_REF_DATE,PO_REF_NO,RUN_NUMBER,DISTRIBUTOR_ID,BRANDPACK_ID) ;"
            'Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNo, 25)
            '=========END COMMENT AFTER DEBUGGING=================================================================================
            '----------------------------------------------------------------------------------

            '============================= UNCOMMENT THIS AFTER DEBUGGING =============================================================
            Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                     "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_MASTER_PO_" & Me.ComputerName & "' AND TYPE = 'U') " & vbCrLf & _
                     " BEGIN DROP TABLE tempdb..##T_MASTER_PO_" & Me.ComputerName & " ; END " & vbCrLf & _
                     " SELECT PO_REF_NO,PO_REF_DATE,DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID,SPPB_QTY,PO_ORIGINAL_QTY,PO_AMOUNT = PO_ORIGINAL_QTY * PO_PRICE_PERQTY,RUN_NUMBER,IncludeDPD INTO tempdb..##T_MASTER_PO_" & Me.ComputerName & " FROM ( " & vbCrLf & _
                     "  SELECT PO.PO_REF_NO,PO.PO_REF_DATE,PO.DISTRIBUTOR_ID,ABI.BRAND_ID,ABP.BRANDPACK_ID,OPB.PO_ORIGINAL_QTY,OPB.PO_PRICE_PERQTY,OOAB.QTY_EVEN + ISNULL(SB.TOTAL_DISC_QTY) AS SPPB_QTY,OOA.RUN_NUMBER ," & vbCrLf & _
                     "  IncludeDPD = CASE WHEN (OPB.ExcludeDPD = 0) THEN 'YESS' " & vbCrLf & _
                     "  WHEN EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PLANTATION_ID = OPB.PLANTATION_ID AND BRANDPACK_ID = OPB.BRANDPACK_ID AND DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID AND PRICE = OPB.PO_PRICE_PERQTY AND IncludeDPD = 1) THEN 'YESS' " & vbCrLf & _
                     "  WHEN EXISTS(SELECT PRICE_TAG FROM DIST_PLANT_PRICE WHERE PLANTATION_ID = OPB.PLANTATION_ID AND BRANDPACK_ID = OPB.BRANDPACK_ID AND DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID AND PRICE = OPB.PO_PRICE_PERQTY AND IncludeDPD = 0) THEN 'NO' " & vbCrLf & _
                     "  WHEN EXISTS(SELECT PROJ.PROJ_REF_NO, PB.BRANDPACK_ID FROM PROJ_PROJECT PROJ INNER JOIN PROJ_BRANDPACK PB ON PROJ.PROJ_REF_NO = PB.PROJ_REF_NO WHERE PROJ.PROJ_REF_NO = PO.PROJ_REF_NO AND PB.BRANDPACK_ID = OPB.BRANDPACK_ID AND PROJ.DISTRIBUTOR_ID = PO.DISTRIBUTOR_ID) THEN 'NO' " & vbCrLf & _
                     "  WHEN OPB.PLANTATION_ID IS NULL THEN 'YESS' ELSE 'NO' END " & vbCrLf & _
                     "  FROM Nufarm.dbo.AGREE_BRAND_INCLUDE ABI " & vbCrLf & _
                     "  INNER JOIN Nufarm.DBO.AGREE_BRANDPACK_INCLUDE ABP ON ABI.AGREE_BRAND_ID = ABP.AGREE_BRAND_ID" & vbCrLf & _
                     "  INNER JOIN Nufarm.dbo.ORDR_PO_BRANDPACK OPB ON OPB.BRANDPACK_ID = ABP.BRANDPACK_ID " & vbCrLf & _
                     "  INNER JOIN Nufarm.dbo.ORDR_PURCHASE_ORDER PO " & vbCrLf & _
                     "  ON PO.PO_REF_NO = OPB.PO_REF_NO INNER JOIN Nufarm.dbo.ORDR_ORDER_ACCEPTANCE OOA " & vbCrLf & _
                     "  ON OOA.PO_REF_NO = PO.PO_REF_NO " & vbCrLf & _
                     "  INNER JOIN ORDR_OA_BRANDPACK OOAB ON OOA.OA_ID = OOAB.OA_ID AND OOAB.PO_BRANDPACK_ID = OPB.PO_BRANDPACK_ID " & vbCrLf & _
                     "  LEFT OUTER JOIN (SELECT OOBD.OA_BRANDPACK_ID,ISNULL(SUM(OOBD.DISC_QTY),0) AS TOTAL_DISC_QTY FROM ORDR_OA_BRANDPACK_DISC OOBD " & vbCrLf & _
                     "      WHERE OOBD.OA_BRANDPACK_ID = ANY(  " & vbCrLf & _
                     "      SELECT OA_BRANDPACK_ID FROM ORDR_OA_BRANDPACK " & vbCrLf & _
                     "      WHERE PO_BRANDPACK_ID = ANY(SELECT PO_BRANDPACK_ID FROM ORDR_PO_BRANDPACK WHERE PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE)" & vbCrLf & _
                     "      ) " & vbCrLf & _
                     "      GROUP BY OOBD.OA_BRANDPACK_ID " & vbCrLf & _
                     "  )SB ON SB.OA_BRANDPACK_ID = OOAB.OA_BRANDPACK_ID" & vbCrLf & _
                     "  WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND OPB.PO_ORIGINAL_QTY > 0 " & vbCrLf & _
                     "  AND PO.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                     " )B ;" & vbCrLf & _
                     " CREATE CLUSTERED INDEX IX_T_MASTER_PO ON ##T_MASTER_PO_" & Me.ComputerName & "(PO_REF_DATE,PO_REF_NO,RUN_NUMBER,DISTRIBUTOR_ID,BRANDPACK_ID) ;"
            '============================= END UNCOMMENT THIS AFTER DEBUGGING =============================================================
            Me.ResetCommandText(CommandType.Text, Query)
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate.AddMonths(-8).AddDays(-1))
            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)

            Me.SqlCom.ExecuteScalar()

            ''sum po inovoice_qty dimana po bukan di antara periode flag(periode sebelumnya)
            Me.SqlCom.Parameters.RemoveAt("@START_DATE")
            Me.SqlCom.Parameters.RemoveAt("@END_DATE")
            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate)
            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, EndDate)
            If HasNewData Then
                Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Total_Qty_Brand_By_Invoice")
                Me.AddParameter("@AGREEMENT_NO", SqlDbType.VarChar, AgreementNo, 25)
                Me.AddParameter("@COMPUTERNAME", SqlDbType.VarChar, Me.ComputerName, 100)
                'Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate) ' SMALLDATETIME,
                Me.SqlRe = Me.SqlCom.ExecuteReader() '': Dim tblComb As New DataTable("T_Comb")
                While Me.SqlRe.Read()
                    Dim ACH_HEADER_ID As String = SqlRe.GetString(0) & "|" & AgreementNo & SqlRe.GetString(1) & "|" & Flag
                    'If ACH_HEADER_ID = "SIS001IDR|0223/NI/III/2010.1100096|S1" Then
                    '    Stop
                    'End If
                    'ACH_HEADER_ID, DISTRIBUTOR_ID, AGREEMENT_NO, BRAND_ID,AGREE_BRAND_ID,TARGET, TARGET_FM, TARGET_PL, TARGET_VALUE,FLAG,
                    'TOTAL_TARGET, TOTAL_PO, TOTAL_PO_VALUE, BALANCE, TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_ACTUAL, ACH_DISPRO, DISPRO,
                    'CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsNew, IsChanged, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3, GPPBF3, GPCPQ1, 
                    'GPCPQ2, GPCPQ3, GPCPF1, GPCPF2, GPCPF3
                    RowsSelect = tblAchHeader.Select("ACH_HEADER_ID = '" & ACH_HEADER_ID & "'")
                    If RowsSelect.Length > 0 Then
                        Row = RowsSelect(0) : Row.BeginEdit()
                        Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(2)
                        Row("TOTAL_PO_VALUE") = SqlRe.GetDecimal(3)
                        Row("TOTAL_PO") = SqlRe.GetDecimal(7)
                        ''Row("ACTUAL_AMOUNT_DISTRIBUTOR") = SqlRe.GetDecimal(4)
                        Row.EndEdit()
                    End If
                End While : SqlRe.Close()
                'jika flag = 'y'
            End If
            ''totalkan agree_brand
            ''masukan agree_brand kedalam list
            '------------------------------CHEK TOTAL INVOICE-----------------------------------
            'jika brand_id dengan status new ada dalam brand_id hasil query procedure tsb
            'maka jangan di masukan ke datatab
            ''sekarang ambil data yang sudah ada di accrued header dengan mengecek apakah ada data yang di edit 
            ''berdasarkan last updaetenya oleh user fathul
            '-----------------------------------------------------------------------------------------
            '------------------------------------------------------------------------------------
            ''-----------------------------BIKIN TABLE temporary isinya DISTRIBUTOR_ID,BRAND_ID,SUM(QTY)----------
            Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                    "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_Agreement_Brand_" & Me.ComputerName & "' " & vbCrLf & _
                    "           AND TYPE = 'U') " & vbCrLf & _
                    "BEGIN DROP TABLE ##T_Agreement_Brand_" & Me.ComputerName & "  ; END " & vbCrLf & _
                    " SELECT DISTRIBUTOR_ID,BRAND_ID,SUM(QTY)AS TOTAL_INVOICE,SUM(PO_AMOUNT)AS PO_AMOUNT_DISTRIBUTOR,SUM(INV_AMOUNT)AS ACTUAL_AMOUNT_DISTRIBUTOR,SUM(PO_ORIGINAL_QTY)AS TOTAL_PO INTO ##T_Agreement_Brand_" & Me.ComputerName & " " & vbCrLf & _
                    "  FROM( " & vbCrLf & _
                    "       SELECT PO.DISTRIBUTOR_ID,PO.BRAND_ID,PO.BRANDPACK_ID,(ISNULL(INV.QTY,0)/PO.SPPB_QTY)* PO.PO_ORIGINAL_QTY AS QTY,PO.PO_AMOUNT,ISNULL(INV.INV_AMOUNT,0) AS INV_AMOUNT,PO.PO_ORIGINAL_QTY  " & vbCrLf & _
                    "       FROM tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO " & vbCrLf & _
                    "       INNER JOIN COMPARE_ITEM Tmbp ON PO.BRANDPACK_ID = Tmbp.BRANDPACK_ID_DTS " & vbCrLf & _
                    "       INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON Tmbp.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
                    "       AND ((PO.RUN_NUMBER = INV.REFERENCE) OR (PO.PO_REF_NO = INV.PONUMBER)) " & vbCrLf & _
                    "            WHERE PO.DISTRIBUTOR_ID = SOME( " & vbCrLf & _
                    "       	 SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO " & vbCrLf & _
                    "     		 )" & vbCrLf & _
                    "       AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' " & vbCrLf & _
                    "      )INV1   " & vbCrLf & _
                    " GROUP BY DISTRIBUTOR_ID,BRAND_ID ; " & vbCrLf & _
                    " CREATE CLUSTERED INDEX IX_T_Agreement_Brand ON ##T_Agreement_Brand_" & Me.ComputerName & "(TOTAL_INVOICE,BRAND_ID) ;"
            Me.ResetCommandText(CommandType.Text, Query) : Me.SqlCom.ExecuteScalar()

            Query = " IF EXISTS(SELECT * FROM ( " & vbCrLf & _
                    "    SELECT * FROM Nufarm.DBO.ACHIEVEMENT_HEADER ACH " & vbCrLf & _
                    "    INNER JOIN Tempdb..##T_Agreement_Brand_" & Me.ComputerName & " INV" & vbCrLf & _
                    "    ON ACH.BRAND_ID = INV.BRAND_ID AND ACH.DISTRIBUTOR_ID = INV.DISTRIBUTOR_ID " & vbCrLf & _
                    "    WHERE ACH.AGREEMENT_NO = @AGREEMENT_NO AND ACH.ACTUAL_DISTRIBUTOR <> CAST((ISNULL(INV.TOTAL_INVOICE,0))AS DECIMAL(18,3)) AND ACH.FLAG = @FLAG" & vbCrLf & _
                    " )C)" & vbCrLf & _
                    " BEGIN " & vbCrLf & _
                    "   SELECT ACH.DISTRIBUTOR_ID,ACH.BRAND_ID,ACH.TOTAL_TARGET,ACH.TARGET_VALUE,ACH.TARGET_FM,ACH.TARGET_PL,INV.QTY AS TOTAL_PO,INV.PO_AMOUNT_DISTRIBUTOR,ISNULL(INV.TOTAL_INVOICE,0)AS TOTAL_INVOICE " & vbCrLf & _
                    "   FROM Nufarm.DBO.ACHIEVEMENT_HEADER ACH" & vbCrLf & _
                    "   LEFT OUTER JOIN Tempdb..##T_Agreement_Brand_" & Me.ComputerName & " INV " & vbCrLf & _
                    "   ON ACH.BRAND_ID = INV.BRAND_ID AND ACH.DISTRIBUTOR_ID = INV.DISTRIBUTOR_ID  " & vbCrLf & _
                    "   WHERE ACH.AGREEMENT_NO = @AGREEMENT_NO AND ACH.FLAG = @FLAG ;" & vbCrLf & _
                    " End "
            Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Changed_Invoice_By_Brand_ID")
            'Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, StartDate) ' SMALLDATETIME,
            Me.AddParameter("@FLAG", SqlDbType.VarChar, Flag, 2)
            Me.AddParameter("@COMPUTERNAME", SqlDbType.VarChar, Me.ComputerName, 100)
            Me.SqlRe = Me.SqlCom.ExecuteReader()
            While Me.SqlRe.Read()
                IsChangedData = True : Row = tblAchHeader.NewRow()
                Dim BrandID As String = SqlRe.GetString(1), Target As Decimal = SqlRe.GetDecimal(2), TotalPO As Decimal = SqlRe.GetDecimal(8)

                Dim AchHeaderID As String = SqlRe.GetString(0) & "|" + AgreementNo & SqlRe.GetString(1) & "|" & Flag
                'ACH_HEADER_ID, DISTRIBUTOR_ID, AGREEMENT_NO, BRAND_ID,AGREE_BRAND_ID,TARGET, TARGET_FM, TARGET_PL, TARGET_VALUE,FLAG,
                'TOTAL_TARGET, TOTAL_PO, TOTAL_PO_VALUE, BALANCE, TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_ACTUAL, ACH_DISPRO, DISPRO,
                'CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsNew, IsChanged, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3, GPPBF3, GPCPQ1, 
                'GPCPQ2, GPCPQ3, GPCPF1, GPCPF2, GPCPF3
                If tblAchHeader.Select("ACH_HEADER_ID = '" & AchHeaderID & "'").Length <= 0 Then
                    Row("ACH_HEADER_ID") = AchHeaderID
                    Row("AGREEMENT_NO") = AgreementNo
                    Row("DISTRIBUTOR_ID") = SqlRe.GetString(0)
                    Row("BRAND_ID") = SqlRe.GetString(1)
                    Row("AGREE_BRAND_ID") = AgreementNo + SqlRe.GetString(1)
                    Row("FLAG") = Flag
                    Row("TOTAL_TARGET") = Target
                    Row("TOTAL_PO") = TotalPO
                    Row("IsNew") = False
                    Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(3)
                    Row("TOTAL_PO_VALUE") = SqlRe.GetDecimal(4)
                    'Row("ACTUAL_AMOUNT_DISTRIBUTOR") = SqlRe.GetDecimal(5)
                    Row("TARGET_FM") = SqlRe.GetDecimal(6)
                    Row("TARGET_PL") = SqlRe.GetDecimal(7)
                    Row("IsChanged") = True
                    Row.EndEdit() : tblAchHeader.Rows.Add(Row)
                    If Not ListAchievementChangedData.Contains(AchHeaderID) Then
                        ListAchievementChangedData.Add(AchHeaderID)
                    End If
                End If

            End While : SqlRe.Close()
            '---------------CHECK CHANGED-INVOICE-------------------------------
            Dim tblPO As New DataTable("T_PO")
            '--------------------------------------------------------------------------------------------------
            If HasNewData Then
                ''haduh looping truss...
                For i As Integer = 0 To ListAchievementNewData.Count - 1
                    'query hanya data yang belum ada di database
                    RowsSelect = tblAchHeader.Select("IsNew = " & True & " AND IsChanged = " & False & " AND ACH_HEADER_ID = '" & ListAchievementNewData(i) & "'")
                    If RowsSelect.Length > 0 Then
                        Dim ACH_HEADER_ID As String = ListAchievementNewData(i)
                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON; " & vbCrLf & _
                                " SELECT ABI.BRANDPACK_ID,ABI.ACH_HEADER_ID,ABI.ACH_DETAIL_ID " & vbCrLf & _
                                " FROM ( " & vbCrLf & _
                                "       SELECT ACH_HEADER_ID = ABI.DISTRIBUTOR_ID + '|' + ABI.AGREE_BRAND_ID + '|" & Flag & "'," & vbCrLf & _
                                "       ABI.BRANDPACK_ID,ABI.DISTRIBUTOR_ID + '|' + ABI.AGREE_BRAND_ID + '|" & Flag & "|' + ABI.BRANDPACK_ID AS ACH_DETAIL_ID " & vbCrLf & _
                                "       FROM VIEW_AGREE_BRANDPACK_INCLUDE ABI WHERE DISTRIBUTOR_ID + '|' + AGREE_BRAND_ID + '|" & Flag & "' = '" & ListAchievementNewData(i) & "' " & vbCrLf & _
                                "      )ABI " & vbCrLf & _
                                " WHERE NOT EXISTS(SELECT ACD.ACH_DETAIL_ID FROM ACHIEVEMENT_DETAIL ACD INNER JOIN ACHIEVEMENT_HEADER ACRH " & vbCrLf & _
                                "                   ON ACD.ACH_HEADER_ID = ACRH.ACH_HEADER_ID " & vbCrLf & _
                                "                   WHERE ACRH.ACH_HEADER_ID = '" & ListAchievementNewData(i) & "' AND ACD.ACH_DETAIL_ID = ABI.ACH_DETAIL_ID " & vbCrLf & _
                                "                   AND ACRH.ACH_HEADER_ID = ABI.ACH_HEADER_ID " & vbCrLf & _
                                "                  ) OPTION(KEEP PLAN);"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.SqlRe = Me.SqlCom.ExecuteReader()
                        'ACH_HEADER_ID,BRANDPACK_ID,ACH_DETAIL_ID,TOTAL_PO,TOTAL_ACTUAL,DISC_QTY,TOTAL_CPQ1,TOTAL_CPQ2,TOTAL_CPQ2,
                        'TOTAL_CPQ3,TOTAL_CPF1,TOTAL_CPF2,TOTAL_PBF3,DESCRIPTIONS,CreatedBy,CreatedDate,CreatedDate,ModifiedBy,ModifiedDate,IsNew,IsChanged
                        While Me.SqlRe.Read()
                            Row = tblAchDetail.NewRow()
                            Dim BRANDPACK_ID As String = SqlRe.GetString(0)
                            Row("ACH_HEADER_ID") = ACH_HEADER_ID
                            Row("BRANDPACK_ID") = BRANDPACK_ID
                            Row("ACH_DETAIL_ID") = SqlRe.GetString(2)
                            Row("IsNew") = True
                            Row("IsChanged") = False
                            Row.EndEdit() : tblAchDetail.Rows.Add(Row)
                        End While : SqlRe.Close()
                        ''ambil total actual nya\
                        'If AgreementNo = "013/NI/I/2000.09" Then
                        '    Stop
                        'End If
                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ; " & vbCrLf & _
                                "IF EXISTS(SELECT [NAME] FROM [tempdb].[sys].[objects] WHERE [NAME] = '##T_PO_Original_By_Distributor_" & Me.ComputerName & "' AND Type = 'U') " & vbCrLf & _
                                " BEGIN DROP TABLE ##T_PO_Original_By_Distributor_" & Me.ComputerName & " ; END " & vbCrLf & _
                                " SELECT PO.DISTRIBUTOR_ID,tmbp.BRANDPACK_ID_DTS AS BRANDPACK_ID,(INV.QTY/ISNULL(PO.SPPB_QTY,0)) * ISNULL(PO.PO_ORIGINAL_QTY,0) AS QTY,INV.INV_AMOUNT,PO.PO_ORIGINAL_QTY,PO.PO_AMOUNT " & vbCrLf & _
                                " INTO ##T_PO_Original_By_Distributor_" & Me.ComputerName & " FROM COMPARE_ITEM tmbp " & vbCrLf & _
                                " INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON tmbp.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
                                " INNER JOIN tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO ON PO.BRANDPACK_ID = tmbp.BRANDPACK_ID_DTS " & vbCrLf & _
                                " AND ((PO.RUN_NUMBER = INV.REFERENCE) OR (PO.PO_REF_NO = INV.PONUMBER) ) " & vbCrLf & _
                                " WHERE PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' AND INV.QTY > 0 ; " & vbCrLf & _
                                " CREATE NON CLUSTERED INDEX IX_T_PO_Original_By_Distributor ON ##T_PO_Original_By_Distributor_" & Me.ComputerName & "(QTY,DISTRIBUTOR_ID) ;"
                        Dim DistributorID As String = ListAchievementNewData(i).Remove(ListAchievementNewData(i).IndexOf("|"))
                        Me.ResetCommandText(CommandType.Text, Query)
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10)
                        Me.SqlCom.ExecuteScalar()

                        'hanya untuk mengambil total actual karena actual tidak mengacu pada PKD
                        Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Total_Qty_BrandPack_By_Invoice")
                        Me.AddParameter("@COMPUTERNAME", SqlDbType.VarChar, Me.ComputerName, 100)
                        Me.SqlRe = Me.SqlCom.ExecuteReader()
                        While Me.SqlRe.Read()
                            RowsSelect = tblAchDetail.Select("IsNew = " & True & " AND ACH_DETAIL_ID = '" & ACH_HEADER_ID & "|" & SqlRe.GetString(1) & "'")
                            If RowsSelect.Length > 0 Then
                                Row = RowsSelect(0)
                                Row.BeginEdit()
                                Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(2)
                                'Row("TOTAL_ACTUAL_AMOUNT") = SqlRe.GetDecimal(3)
                                'Row("TOTAL_PO") = SqlRe.GetDecimal(4)
                                Row.EndEdit()
                            End If
                        End While : SqlRe.Close()

                        'hanya mengambil total_PO karena TotalPO mengacu ke PKD
                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ; SELECT ABI.DISTRIBUTOR_ID,ABI.BRANDPACK_ID," & vbCrLf & _
                                " ISNULL(SUM(PO.PO_ORIGINAL_QTY),0)AS TOTAL_PO_ORIGINAL,ISNULL(SUM(PO.PO_AMOUNT),0)AS TOTAL_PO_AMOUNT FROM Nufarm.DBO.VIEW_AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                                " INNER JOIN tempdb..##T_MASTER_PO_" & Me.ComputerName & "  PO ON PO.DISTRIBUTOR_ID = ABI.DISTRIBUTOR_ID AND PO.BRANDPACK_ID = ABI.BRANDPACK_ID " & vbCrLf & _
                                "  WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                                "  AND ABI.DISTRIBUTOR_ID = @DISTRIBUTOR_ID GROUP BY ABI.DISTRIBUTOR_ID,ABI.BRANDPACK_ID OPTION(KEEP PLAN);"
                        Me.ResetCommandText(CommandType.Text, Query)
                        tblPO.Clear()
                        setDataAdapter(Me.SqlCom).Fill(tblPO)
                        If tblPO.Rows.Count > 0 Then
                            For i1 As Integer = 0 To tblPO.Rows.Count - 1
                                Dim BrandPackID As String = tblPO.Rows(i1)("BRANDPACK_ID").ToString()
                                RowsSelect = tblAchDetail.Select("IsNew = " & True & " AND ACH_DETAIL_ID = '" & ACH_HEADER_ID & "|" & BrandPackID & "'")
                                If RowsSelect.Length > 0 Then
                                    Row = RowsSelect(0)
                                    Row.BeginEdit()
                                    Row("TOTAL_PO") = tblPO.Rows(i1)("TOTAL_PO_ORIGINAL")
                                    ' Row("TOTAL_PO_AMOUNT") = tblPO.Rows(i1)("TOTAL_PO_AMOUNT")
                                    Row.EndEdit()
                                End If
                            Next
                        End If
                    End If
                Next

            End If
            '-----------------------------------------------------------------------------
            ''sekarang ambil data yang sudah ada di accrued Detail dengan mengecek apakah ada data yang di edit 
            ''berdasarkan last updaetenya oleh user fathul
            If IsChangedData Then
                ''hiyah looping lagi
                For i As Integer = 0 To ListAchievementChangedData.Count - 1
                    RowsSelect = tblAchHeader.Select("IsChanged = " & True & " AND ACH_HEADER_ID = '" & ListAchievementChangedData(i) & "'")
                    If RowsSelect.Length > 0 Then
                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT  ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                                "IF EXISTS(SELECT [NAME] FROM  [tempdb].[sys].[objects] WHERE [NAME] = '##T_Agreement_BrandPack_" & Me.ComputerName & "'  AND Type = 'U') " & vbCrLf & _
                                " BEGIN DROP TABLE ##T_Agreement_BrandPack_" & Me.ComputerName & " ; END " & vbCrLf & _
                                " SELECT DISTRIBUTOR_ID,BRANDPACK_ID,SUM(QTY)AS TOTAL_INVOICE,SUM(PO_ORIGINAL_QTY)AS TOTAL_PO_ORIGINAL, SUM(ACTUAL_AMOUNT)AS TOTAL_ACTUAL_AMOUNT INTO ##T_Agreement_BrandPack_" & Me.ComputerName & " " & vbCrLf & _
                                " FROM( " & vbCrLf & _
                                "       SELECT PO.DISTRIBUTOR_ID,tmpbp.BRANDPACK_ID_DTS AS BRANDPACK_ID,(ISNULL(INVOICE.QTY,0)/ISNULL(PO.SPPB_QTY,0))* ISNULL(PO.PO_ORIGINAL_QTY,0) AS QTY,ISNULL(INVOICE.INV_AMOUNT,0) AS ACTUAL_AMOUNT,PO.PO_ORIGINAL_QTY " & vbCrLf & _
                                "       FROM COMPARE_ITEM tmpbp INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INVOICE ON INVOICE.BRANDPACK_ID =  tmpbp.BRANDPACK_ID_ACCPAC " & vbCrLf & _
                                "       INNER JOIN tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO ON PO.BRANDPACK_ID = tmpbp.BRANDPACK_ID_DTS " & vbCrLf & _
                                "       AND ((PO.RUN_NUMBER = INVOICE.REFERENCE) OR (PO.PO_REF_NO = INVOICE.PONUMBER)) " & vbCrLf & _
                                "       WHERE PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND PO.PO_REF_DATE >= @START_DATE AND PO.PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' " & vbCrLf & _
                                "      )INV1 " & vbCrLf & _
                                "       GROUP BY DISTRIBUTOR_ID,BRANDPACK_ID ;" & vbCrLf & _
                                " CREATE CLUSTERED INDEX IX_T_Agreement_BrandPack ON ##T_Agreement_BrandPack_" & Me.ComputerName & "(TOTAL_INVOICE,BRANDPACK_ID) ;"
                        Me.ResetCommandText(CommandType.Text, Query)
                        Dim DistributorID As String = ListAchievementChangedData(i).Remove(ListAchievementChangedData(i).IndexOf("|"))
                        Me.AddParameter("@DISTRIBUTOR_ID", SqlDbType.VarChar, DistributorID, 10) ' VARCHAR(10),
                        Me.SqlCom.ExecuteScalar()
                        '-----------------------------------------------------------------------------------------------------------------'

                        'Me.ResetCommandText(CommandType.StoredProcedure, "Usp_Get_Changed_Invoice_By_BrandPack_ID")
                        'Me.AddParameter("@COMPUTERNAME", SqlDbType.VarChar, Me.ComputerName, 100)

                        'hanya untuk mengambil total actual karena actual tidak mengacu pada PKD
                        Query = " SELECT ACD.ACH_DETAIL_ID,ACD.BRANDPACK_ID,ISNULL(INV.TOTAL_INVOICE,0)AS TOTAL_INVOICE,ISNULL(INV.TOTAL_ACTUAL_AMOUNT,0)AS TOTAL_ACTUAL_AMOUNT" & vbCrLf & _
                                " FROM Nufarm.DBO.ACHIEVEMENT_DETAIL ACD INNER JOIN Nufarm.DBO.ACHIEVEMENT_HEADER ACH ON ACD.ACHIEVEMENT_ID = ACH.ACHIEVEMENT_ID " & vbCrLf & _
                                " LEFT OUTER JOIN Tempdb..##T_Agreement_BrandPack_" & Me.ComputerName & " INV " & vbCrLf & _
                                " ON ACD.BRANDPACK_ID = INV.BRANDPACK_ID " & vbCrLf & _
                                " AND ACH.DISTRIBUTOR_ID = INV.DISTRIBUTOR_ID" & vbCrLf & _
                                " WHERE ACH.AGREEMENT_NO = @AGREEMENT_NO" & vbCrLf & _
                                " AND ACH.DISTRIBUTOR_ID = @DISTRIBUTOR_ID AND ACH.FLAG = @FLAG "
                        Me.SqlRe = Me.SqlCom.ExecuteReader()
                        Dim ACH_HEADER_ID As String = ""
                        While Me.SqlRe.Read()
                            ACH_HEADER_ID = ListAchievementChangedData(i)
                            Dim BRANDPACK_ID As String = SqlRe.GetString(1)
                            Dim AchDetailID As String = SqlRe.GetString(0)
                            Dim rows() As DataRow = tblAchDetail.Select("ACH_DETAIL_ID = '" & AchDetailID & "'")
                            If rows.Length <= 0 Then
                                Row = tblAchDetail.NewRow()
                                Row("ACH_HEADER_ID") = ACH_HEADER_ID
                                Row("BRANDPACK_ID") = BRANDPACK_ID
                                Row("ACH_DETAIL_ID") = AchDetailID
                                Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(2) 'TOTAL INVOICE
                                'Row("TOTAL_PO_ORIGINAL") = SqlRe.GetDecimal(8)
                                'Row("TOTAL_ACTUAL_AMOUNT") = SqlRe.GetDecimal(9)
                                Row("IsChanged") = True
                                Row("IsNew") = False
                                Row.EndEdit() : tblAchDetail.Rows.Add(Row)
                            End If
                        End While : SqlRe.Close()

                        Query = "SET DEADLOCK_PRIORITY NORMAL; SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ; SELECT ABI.DISTRIBUTOR_ID,ABI.BRANDPACK_ID," & vbCrLf & _
                              " ISNULL(SUM(PO.PO_ORIGINAL_QTY),0)AS TOTAL_PO_ORIGINAL,ISNULL(SUM(PO.PO_AMOUNT),0) AS TOTAL_PO_AMOUNT FROM Nufarm.DBO.VIEW_AGREE_BRANDPACK_INCLUDE ABI " & vbCrLf & _
                              " INNER JOIN tempdb..##T_MASTER_PO_" & Me.ComputerName & "  PO ON PO.DISTRIBUTOR_ID = ABI.DISTRIBUTOR_ID AND PO.BRANDPACK_ID = ABI.BRANDPACK_ID " & vbCrLf & _
                              " WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO AND PO.DISTRIBUTOR_ID = @DISTRIBUTOR_ID " & vbCrLf & _
                              " AND ABI.DISTRIBUTOR_ID = @DISTRIBUTOR_ID GROUP BY ABI.DISTRIBUTOR_ID,ABI.BRANDPACK_ID OPTION(KEEP PLAN);"
                        Me.ResetCommandText(CommandType.Text, Query)
                        tblPO.Clear()
                        setDataAdapter(Me.SqlCom).Fill(tblPO)
                        ': Me.ClearCommandParameters()

                        If tblPO.Rows.Count > 0 Then
                            For i1 As Integer = 0 To tblPO.Rows.Count - 1
                                Dim BrandPackID As String = tblPO.Rows(i1)("BRANDPACK_ID").ToString()
                                RowsSelect = tblAchDetail.Select("IsChanged = " & True & " AND ACH_DETAIL_ID = '" & ACH_HEADER_ID & "|" & BrandPackID & "'")
                                If RowsSelect.Length > 0 Then
                                    Row = RowsSelect(0)
                                    Row.BeginEdit()
                                    'Row("TOTAL_ACTUAL") = SqlRe.GetDecimal(2)
                                    Row("TOTAL_PO") = tblPO.Rows(i1)("TOTAL_PO_ORIGINAL")
                                    'Row("TOTAL_PO_AMOUNT") = tblPO.Rows(i1)("TOTAL_PO_AMOUNT")
                                    Row.EndEdit()
                                End If
                            Next
                        End If
                    End If
                Next
            End If
            If IsChangedData Or HasNewData Then
                Query = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                "DECLARE @V_START_DATE SMALLDATETIME ;" & vbCrLf & _
                "SET @V_START_DATE = (SELECT START_DATE FROM AGREE_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO); " & vbCrLf & _
                "SELECT TOP 1 AA.AGREEMENT_NO,AA.START_DATE,AA.END_DATE,AA.QS_TREATMENT_FLAG FROM AGREE_AGREEMENT AA INNER JOIN DISTRIBUTOR_AGREEMENT DA ON DA.AGREEMENT_NO = AA.AGREEMENT_NO " & vbCrLf & _
                " WHERE AA.END_DATE < @V_START_DATE AND EXISTS(SELECT AGREEMENT_NO FROM AGREE_BRAND_INCLUDE WHERE AGREEMENT_NO = AA.AGREEMENT_NO AND TARGET_" & strFlag & " > 0 ) " & vbCrLf & _
                " AND DA.DISTRIBUTOR_ID = SOME(SELECT DISTRIBUTOR_ID FROM DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO) ORDER BY AA.START_DATE DESC ;"
                Me.ResetCommandText(CommandType.Text, Query)
                Dim tblStartDate As New DataTable("T_StartDate1")
                ''SET privouse Agreement
                PrevAgreementNo = tblStartDate.Rows(0)("AGREEMENT_NO")
                'TOTAL_PBQ3, TOTAL_CPQ2, TOTAL_CPQ3, TOTAL_ACTUAL, ACH_DISPRO, DISPRO,
                'CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IsNew, IsChanged, TOTAL_CPF1, TOTAL_CPF2, TOTAL_PBF3, GPPBF3, GPCPQ1

                'TOTAL_CPQ1,TOTAL_CPQ2,TOTAL_CPQ2,
                'TOTAL_CPQ3,TOTAL_CPF1,TOTAL_CPF2,TOTAL_PBF3
                tblStartDate.Clear() : Me.setDataAdapter(Me.SqlCom).Fill(tblStartDate)
                'Dim PBQ1 As Object = Nothing, PBEQ1 As Object = Nothing, PBQ2 As Object = Nothing, PBEQ2 As Object = Nothing, _
                '                   PBQ3 As Object = Nothing, PBEQ3 As Object = Nothing, PBQ4 As Object = Nothing, PBEQ4 As Object = Nothing, _
                '                   PBS1 As Object = Nothing, PBES1 As Object = Nothing, PBS2 As Object = Nothing, PBES2 As Object = Nothing, _
                '                   PBStartYear As Object = Nothing, PBEndyear As Object = Nothing
                'Dim CPQ1 As Object = Nothing, CPEQ1 As Object = Nothing, CPQ2 As Object = Nothing, CPEQ2 As Object = Nothing, _
                'CPQ3 As Object = Nothing, CPEQ3 As Object = Nothing, CPS1 As Object = Nothing, CPES1 As Object = Nothing
                Dim CPF1 As Object = Nothing, CPEF1 As Object = Nothing, CPF2 As Object = Nothing, CPEF2 As Object = Nothing, CPQ2 As Object = Nothing, CPQ3 As Object = Nothing, _
                CPEQ2 As Object = Nothing, CPEQ3 As Object = Nothing
                Select Case Flag
                    Case "F2"
                        CPEF1 = StartDate.AddDays(-1)
                        CPF1 = Convert.ToDateTime(CPEF1).AddMonths(-4).AddDays(1)
                        'strFlag
                    Case "F3"
                        If IsTransitionTime Then
                            CPEQ3 = StartDate.AddDays(-1)
                            CPQ3 = Convert.ToDateTime(CPEQ3).AddMonths(-2).AddDays(1)
                            CPEQ2 = Convert.ToDateTime(CPQ3).AddDays(-1)
                            CPQ2 = Convert.ToDateTime(CPEQ2).AddMonths(-3).AddDays(1)
                        Else
                            CPEF2 = StartDate.AddDays(-1)
                            CPF2 = Convert.ToDateTime(CPEF2).AddMonths(-4).AddDays(1)
                            CPEF1 = Convert.ToDateTime(CPF2).AddDays(-1)
                            CPF1 = Convert.ToDateTime(CPEF1).AddMonths(-4).AddDays(1)
                        End If

                End Select
                Dim tblTemp As New DataTable("T_TEMP") : tblTemp.Clear()
                '---------------------QUERY UNTUK MENTOTAL KAN TOTAL invoice QTY BRAND DIANTARA START_DATE AND END_DATE PO grouped by BRAND ---------------------------
                Dim Query1 As String = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                                     " SELECT DISTRIBUTOR_ID,BRAND_ID,ISNULL(SUM(QTY),0) AS TOTAL_INVOICE " & vbCrLf & _
                                     "  FROM( " & vbCrLf & _
                                     "       SELECT PO.DISTRIBUTOR_ID,PO.BRAND_ID,PO.BRANDPACK_ID,(ISNULL(INV.QTY,0)/PO.SPPB_QTY)* PO.PO_ORIGINAL_QTY AS QTY " & vbCrLf & _
                                     "       FROM tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO " & vbCrLf & _
                                     "       INNER JOIN COMPARE_ITEM Tmbp ON PO.BRANDPACK_ID = Tmbp.BRANDPACK_ID_DTS " & vbCrLf & _
                                     "       INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON Tmbp.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
                                     "       AND ((PO.RUN_NUMBER = INV.REFERENCE) OR (PO.PO_REF_NO = INV.PONUMBER)) " & vbCrLf & _
                                     "            WHERE PO.DISTRIBUTOR_ID = SOME( " & vbCrLf & _
                                     "       	    SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO " & vbCrLf & _
                                     "     		)" & vbCrLf & _
                                     "       AND PO.PO_REF_DATE >= @START_DATE AND PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' " & vbCrLf & _
                                     "      )INV1   " & vbCrLf & _
                                     " GROUP BY DISTRIBUTOR_ID,BRAND_ID OPTION(KEEP PLAN); "

                '---------------------QUERY UNTUK MENTOTAL KAN TOTAL_invoice BRANDPACK DIANTARA START_DATE AND END_DATE PO grouped by BRANDPACK---------------------------
                Dim Query2 As String = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                                       " SELECT DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID,ISNULL(SUM(QTY),0) AS TOTAL_INVOICE,ISNULL(SUM(INV_AMOUNT),0)AS CP_AMOUNT  " & vbCrLf & _
                                       "  FROM( " & vbCrLf & _
                                       "       SELECT PO.DISTRIBUTOR_ID,PO.BRAND_ID,PO.BRANDPACK_ID,(ISNULL(INV.QTY,0)/PO.SPPB_QTY)* PO.PO_ORIGINAL_QTY AS QTY " & vbCrLf & _
                                       "       FROM tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO " & vbCrLf & _
                                       "       INNER JOIN COMPARE_ITEM Tmbp ON PO.BRANDPACK_ID = Tmbp.BRANDPACK_ID_DTS " & vbCrLf & _
                                       "       INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON Tmbp.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
                                       "       AND ((PO.RUN_NUMBER = INV.REFERENCE) OR (PO.PO_REF_NO = INV.PONUMBER)) " & vbCrLf & _
                                       "            WHERE PO.DISTRIBUTOR_ID = SOME( " & vbCrLf & _
                                       "       	    SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO " & vbCrLf & _
                                       "     		)" & vbCrLf & _
                                       "       AND PO.PO_REF_DATE >= @START_DATE AND PO_REF_DATE <= @END_DATE AND PO.IncludeDPD = 'YESS' " & vbCrLf & _
                                       "      )INV1   " & vbCrLf & _
                                       " GROUP BY DISTRIBUTOR_ID,BRAND_ID,BRANDPACK_ID OPTION(KEEP PLAN); "

                '----------------QUERY UNTUK MENGECEK APAKAH ADA INVOICE DARI PO SEBELUM PERIODE FLAG USANG
                'Dim Query3 As String = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                '                       " SELECT 1 WHERE EXISTS(SELECT PO.BRANDPACK_ID " & vbCrLf & _
                '                       " FROM tempdb..##T_MASTER_PO_" & Me.ComputerName & " PO " & vbCrLf & _
                '                       " INNER JOIN COMPARE_ITEM Tmbp ON PO.BRANDPACK_ID = Tmbp.BRANDPACK_ID_DTS " & vbCrLf & _
                '                       " INNER JOIN ##T_SELECT_INVOICE_" & Me.ComputerName & " INV ON Tmbp.BRANDPACK_ID_ACCPAC = INV.BRANDPACK_ID " & vbCrLf & _
                '                       " AND ((PO.RUN_NUMBER = INV.REFERENCE) OR (PO.PO_REF_NO = INV.PONUMBER)) " & vbCrLf & _
                '                       " WHERE PO.DISTRIBUTOR_ID = SOME( " & vbCrLf & _
                '                       " SELECT DISTRIBUTOR_ID FROM Nufarm.DBO.DISTRIBUTOR_AGREEMENT WHERE AGREEMENT_NO = @AGREEMENT_NO) " & vbCrLf & _
                '                       " AND ((ISNULL(INV.QTY,0)/ISNULL(PO.SPPB_QTY,0)) * ISNULL(PO.PO_ORIGINAL_QTY,0) > 0 )" & vbCrLf & _
                '                       " AND PO.PO_REF_DATE < @START_DATE AND PO.IncludeDPD = 'YESS') "

                '-----------------QUERY UNTUK MENGECEK GIVEN PROGRESSIVE--- PERIODE SEBELUMNYA
                'Dim Query4 As String = "SET DEADLOCK_PRIORITY NORMAL ;SET NOCOUNT ON ; SET ARITHABORT OFF ; SET ANSI_WARNINGS OFF ;" & vbCrLf & _
                '        "SELECT 1 WHERE EXISTS(SELECT GP.IDApp FROM DISTRIBUTOR_AGREEMENT DA INNER JOIN AGREE_BRAND_INCLUDE ABI ON DA.AGREEMENT_NO = ABI.AGREEMENT_NO " & vbCrLf & _
                '        " INNER JOIN GIVEN_PROGRESSIVE GP ON GP.AGREE_BRAND_ID = ABI.AGREE_BRAND_ID WHERE ABI.AGREEMENT_NO = @AGREEMENT_NO) ;"
                Me.ResetCommandText(CommandType.Text, Query1)
                Select Case Flag
                    Case "F1"
                        If tblStartDate.Rows.Count > 0 Then
                            Dim PBStartDate As Object = Nothing
                            Dim PBEndDate As Object = Nothing, PBFlag As String = ""
                            PBStartDate = Convert.ToDateTime(tblStartDate.Rows(0)("START_DATE"))
                            PBEndDate = Convert.ToDateTime(tblStartDate.Rows(0)("END_DATE"))
                            PBFlag = tblStartDate.Rows(0)("QS_TREATMENT_FLAG").ToString()
                            Dim PBF1 = PBStartDate
                            Dim PBEF1 = Convert.ToDateTime(PBF1).AddMonths(4).AddDays(-1)
                            Dim PBF2 = Convert.ToDateTime(PBEF1).AddDays(1)
                            Dim FBEF2 = Convert.ToDateTime(PBF2).AddMonths(4).AddDays(-1)
                            Dim PBF3 As Object = Convert.ToDateTime(FBEF2).AddDays(-1)
                            Dim PBEF3 As Object = PBEndDate
                            If Not IsNothing(PBF3) Then
                                Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, PBF3)
                                Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(PBEF3))
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "TOTAL_PBF3")
                                    Me.ResetCommandText(CommandType.Text, Query2)
                                    tblTemp = New DataTable("T_TEMP")
                                    tblTemp.Clear()
                                    setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                    If tblTemp.Rows.Count > 0 Then
                                        SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_PBF3")
                                    End If
                                End If
                            End If
                        End If
                    Case "F2"
                        If Not IsNothing(CPF1) Then
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPF1)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPEF1))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "TOTAL_CPF1")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_CPF1")
                                End If
                            End If
                        End If
                    Case "F3"
                        If Not IsNothing(CPF2) Then
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPF2)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPEF2))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "TOTAL_CPF2")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_CPF2")
                                End If
                            End If
                        End If
                        If Not IsNothing(CPF1) Then
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPF1)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPEF1))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "TOTAL_CPF1")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_CPF1")
                                End If
                            End If
                        End If
                        If Not IsNothing(CPQ2) Then
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPQ2)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPQ2))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "TOTAL_CPQ2")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_CPQ2")
                                End If
                            End If
                        End If
                        If Not IsNothing(CPQ3) Then
                            Me.AddParameter("@START_DATE", SqlDbType.SmallDateTime, CPQ3)
                            Me.AddParameter("@END_DATE", SqlDbType.SmallDateTime, Convert.ToDateTime(CPQ3))
                            setDataAdapter(Me.SqlCom).Fill(tblTemp)
                            If tblTemp.Rows.Count > 0 Then
                                SetTotalPeriodBefore(tblTemp, AgreementNo, tblAchHeader, Flag, "TOTAL_CPQ3")
                                Me.ResetCommandText(CommandType.Text, Query2)
                                tblTemp = New DataTable("T_TEMP")
                                tblTemp.Clear()
                                setDataAdapter(Me.SqlCom).Fill(tblTemp)
                                If tblTemp.Rows.Count > 0 Then
                                    SetTotalPeriodeBeforeDetail(tblTemp, tblAchDetail, AgreementNo, Flag, "TOTAL_CPQ3")
                                End If
                            End If
                        End If
                End Select
                tblAchHeader.AcceptChanges()
            End If
        End Sub
        Private Sub SetTotalPeriodeBeforeDetail(ByVal tbltemp As DataTable, ByRef tblAchDetail As DataTable, ByVal AgreementNo As String, ByVal Flag As String, ByVal ColTotalPBFlag As String, Optional ByVal colTotalAmountFlag As String = "")
            Dim rows() As DataRow = Nothing
            For i As Integer = 0 To tbltemp.Rows.Count - 1
                rows = tblAchDetail.Select("ACH_DETAIL_ID = '" & tbltemp.Rows(i)("DISTRIBUTOR_ID").ToString() & "|" & AgreementNo & "" & tbltemp.Rows(i)("BRAND_ID") & "|" & Flag & "|" & tbltemp.Rows(i)("BRANDPACK_ID") & "'")
                If rows.Length > 0 Then
                    rows(0).BeginEdit()
                    rows(0)(ColTotalPBFlag) = Convert.ToDecimal(tbltemp.Rows(i)("TOTAL_INVOICE"))
                    rows(0).EndEdit()
                    rows(0).AcceptChanges()
                End If
            Next
            tblAchDetail.AcceptChanges()
        End Sub
        Private Sub SetTotalPeriodBefore(ByVal tblTemp As DataTable, ByVal AgreementNo As String, ByRef AchHeader As DataTable, ByVal Flag As String, ByVal ColTotalPBFlag As String, Optional ByVal colTotalAmountFlag As String = "")
            For i As Integer = 0 To tblTemp.Rows.Count - 1
                Dim rows As DataRow()
                rows = AchHeader.Select("ACH_HEADER_ID = '" & tblTemp.Rows(i)("DISTRIBUTOR_ID").ToString() & "|" & AgreementNo & tblTemp.Rows(i)("BRAND_ID").ToString() & "|" & Flag & "'")
                If rows.Length > 0 Then
                    rows(0).BeginEdit()
                    rows(0)(ColTotalPBFlag) = Convert.ToDecimal(tblTemp.Rows(i)("TOTAL_INVOICE"))
                    rows(0).EndEdit()
                    rows(0).AcceptChanges()
                End If
            Next
            AchHeader.AcceptChanges()
        End Sub
    End Class
End Namespace

